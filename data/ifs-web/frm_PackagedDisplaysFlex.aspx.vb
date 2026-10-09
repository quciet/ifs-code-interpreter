Imports IFs.Data
Imports NLog
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_PackagedDisplaysFlex
    Inherits Page

#Region " Web Form Designer Generated Code "

    Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Listbox1 As System.Web.UI.WebControls.ListBox


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.

        'jam 2022/07/09
        If IsNothing(Session("ReloadbyTabs")) Then
            Session("ReloadbyTabs") = False
        End If
        InitializeComponent()

        'jrs 2005/04/22
        If Not Page.IsPostBack Then
            'jam 2022/07/07
            'If Not Page.IsPostBack Or Session("ReloadbyTabs") Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
        End If

        'jrs 11/08/04
        'jrs 2011/05/03 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

    'jrs 10/31/04
    'Prepackaged Displays, by Jose Solorzano
    Dim rs As DataTable
    Dim iGDPSel As Integer, iPopSel As Integer

#End Region

    'jrs 2020/08/03
    Dim FilesCol As New Collection
    Dim dfCol As New Collection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load 'jrs 2011/02/11 this was causing a 2nd call to Load, Me.Load
        'cdv 2021/02/02 bring our global var to a hidden input called MaleFirst on the page
        If bMaleFirst Then
            MaleFirst.Value = "true"
        Else
            MaleFirst.Value = "false"
        End If



        'jrs 2005/10/13
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
        'jrs 2008/03/28
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

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use estimation to complete data"
        cswmItem.ID = "mnuEstimation"
        If CBool(Session("bFillHis")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2011/03/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use estimation to complete data (only holes no extrapolation)"
        cswmItem.ID = "mnuEstimationOnlyHoles"
        If CBool(Session("bFillHisOnlyHoles")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2006/09/26
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use all available Historical Data"
        cswmItem.ID = "mnuuseAllHis"
        If CBool(Session("buseAllHis")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2013/05/09
        If CBool(DevelopmentMode) Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Create JavaScript File for Flex Displays Variables"
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2024/11/25
        'If CBool(DevelopmentMode) Then
        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Save Selections directly to File"
        '    cswmItem.IconImage = "blank.gif"
        'End If


        'jrs 2006/01/18
        'jrs 2011/05/03 cswmGroup1 = cswmWebMenu.Groups.Add()
        'jrs 2008/09/03
        'jrs 2011/05/03 cswmGroup1.Caption = "Grouping Opitons"
        'jrs 2011/05/03 cswmGroup1.ID = "mnuGroups"
        'jrs 2012/05/02
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Other Grouping Options"
        cswmGroup1.ID = "mnuGroups"


        If Session("bUseReg") Is Nothing Then
            Session("bUseReg") = True
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
            'jrs 2010/11/19
            Session("bIntercept") = False
            'jrs 2016/09/30
            Session("bUseCtryAndGroup") = False
            'jrs 2025/05/30
            Session("bUseCtryAndGroupDec") = False
        End If

        If Session("bCheckHideNonCoreGroups") Is Nothing Then
            Session("bCheckHideNonCoreGroups") = bCoreGroupCheck 'cdv 2021/09/01
        End If
        'jrs 2011/05/03 
        ''jrs 2008/09/04
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Using Countries/Regions"
        'cswmItem.ID = "mnuReg"
        'If Session("bUseReg") Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Using Groups"
        'cswmItem.ID = "mnuGroup"
        'If Session("bUseGroup") Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If
        ''jrs 2011/05/03
        ''cswmItem.OnClick = "mnuGroup_Click();"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Using Decomposed Groups"
        'cswmItem.ID = "mnuGroupDec"
        'If Session("bUseGroupDec") Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Using G-Lists"
        'cswmItem.ID = "mnuGList"
        'If Session("bUseGList") Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If

        'jrs 2010/11/19
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Intersection for Decomposed Groups or G-Lists"
        cswmItem.ID = "mnuIntercept"
        If Session("bIntercept") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2023/06/14
        If StandAloneMode Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Grouping Aggregations"
            cswmItem.IconImage = "blank.gif"
        End If

        'jam 2022/07/07
        If Not Page.IsPostBack Then
            ' If Not Page.IsPostBack Or Session("ReloadbyTabs") Then
            optReg.Checked = Session("bUseReg")
            optGroup.Checked = Session("bUseGroup")
            optGroupDec.Checked = Session("bUseGroupDec")
            optGList.Checked = Session("bUseGList")
            'chkIntercept.Checked = Session("bIntercept")
            'jrs 2016/09/30
            optCtryAndGrp.Checked = Session("bUseCtryAndGroup")
            'jrs 2025/05/30
            optCtryAndGrpDec.Checked = Session("bUseCtryAndGroupDec")
            'jrs 2025/09/16
            If Session("bCheckHideNonCoreGroups") Is Nothing Then
                Session("bCheckHideNonCoreGroups") = 1
            End If
            If Session("bCheckViewHistoryForecasts") Is Nothing Then
                Session("bCheckViewHistoryForecasts") = 1
            End If

            chkHideNonCoreGroups.Checked = Session("bCheckHideNonCoreGroups") 'cdv 2021/09/01
            chkViewHistoryForecasts.Checked = Session("bCheckViewHistoryForecasts") 'dsb 2025/08/11

        End If
        optGroup.Attributes.Add("onClick", "javascript:optGroup_Click()")
        optGroupDec.Attributes.Add("onClick", "javascript:optGroupDec_Click()")
        optGList.Attributes.Add("onClick", "javascript:optGList_Click()")
        optReg.Attributes.Add("onClick", "javascript:optReg_Click()")
        'jrs 2016/09/30
        optCtryAndGrp.Attributes.Add("onClick", "javascript:optCtryAndGrp_Click()")
        'jrs 2025/05/30
        optCtryAndGrpDec.Attributes.Add("onClick", "javascript:optCtryAndGrpDec_Click()")
        'jrs 2019/03/27
        cboFilter.Attributes.Add("onChange", "javascript:cboFilter_Click()")

        chkHideNonCoreGroups.Attributes.Add("onChange", "javascript:chkHideNonCoreGroups_Click()") 'cdv 2021/08/23
        'chkViewHistoryForecasts.Attributes.Add("onChange", "javascript:chkViewHistoryForecasts_Click()") 'dsb 2025/08/11

        'JAM 2009-05-30
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Other Graphs"
        cswmGroup1.ID = "mnuOtherGraphs"

        'jrs 2011/09/02
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Pie"
        cswmItem.ID = "mnuPie"

        'JAM 2009-05-30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Radial"
        cswmItem.ID = "mnuRadial"
        'cswmItem.IconImage = "blank.gif"

        'JAM 2017-06-11
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Stacked Bar"
        cswmItem.ID = "mnuStackedBar"

        'jrs 2018/05/16
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Stacked Area"
        cswmItem.ID = "mnuStackedArea"

        'jrs 2011/09/06
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Scatter Plot"
        cswmItem.ID = "mnuScatter"

        'jrs 2024/12/06
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Motion Chart (Gap Minder By Google)"
        'cswmItem.ID = "mnuMotionChart"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Bar"
        cswmItem.ID = "mnuBar"

        'jrs 2009/03/17
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Explain"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain List"
        'jrs 2023/02/15
        'cswmItem.Enabled = False

        'jrs 2026/07/06 remove
        ''JAM 2017-09-15 Adding Explain Initials
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Explain Initials"

        'jrs 2025/09/04
        If StandAloneMode Then
            cswmGroup1 = cswmWebMenu.Groups.Add() ' cdv 2022/06/24
            cswmGroup1.Caption = "Edit Lists"
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Edit Lists"
        End If

        'jrs 2012/01/20
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Search Lists"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Search"

        'jrs 2006/12/18
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2009/02/12 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16002);"
        'jrs 2013/09/03 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16002);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")
        'jrs 2012/05/08
        'dsb 2026/05/26 Added saveDimsBeforeDisplay(); to the onClick of all the menu items that open a graph or table to ensure dimensions are saved before the display opens
        cmdTable.Attributes.Add("onClick", "saveDimsBeforeDisplay(); showhg(cmdTable);")
        cmdLine.Attributes.Add("onClick", "saveDimsBeforeDisplay(); showhg(cmdLine);")
        cmdMap.Attributes.Add("onClick", "saveDimsBeforeDisplay(); showhg(cmdMap);")
        cmdExport.Attributes.Add("onClick", "showhg(cmdExport);")
        cmdAnnotation.Attributes.Add("onClick", "showhg(cmdAnnotation);")

        'Put user code to initialize the page here
        'jam 2022/07/07
        If Not Page.IsPostBack Then
            'If Not Page.IsPostBack Or Session("ReloadbyTabs") Then
            'jrs 2006/08/23
            Session("RecordSourceDef") = "Map"
            Session("mapParent") = "frm_PackagedDisplaysFlex.aspx"

            Session("TableDisplayParent") = "frm_PackagedDisplaysFlex.aspx"
            'jrs 2011/08/25 Session("ProEssentialParent") = "frm_PackagedDisplaysFlex.aspx"
            Session("GraphDisplayParent") = "frm_PackagedDisplaysFlex.aspx"
            'jrs 2023/06/15
            Session("GroupAggrParent") = "frm_PackagedDisplaysFlex.aspx"
            'db = DBEngine.OpenDatabase(PutFdrvRun$ + "IFSVAR.mdb")
            'jrs 10/31/04
            iGDPSel = IfsVars.Item("GDP").Index
            iPopSel = IfsVars.Item("POP").Index
            'jrs 2006/11/28
            If Session("bDisplayControl") Is Nothing Then
                'jrs 2005/11/29
                Session("TableTitle") = ""
            Else
                Session("bDisplayControl") = Nothing
            End If
            'jrs 2026/04/27
            Session("RSQR") = ""

            'jrs 2006/01/18
            Session("lstDimTag") = -1
            Session("lstDim2Tag") = -1

            'jrs 2019/03/27
            'jrs 2019/03/27
            'Open Filter Categories
            Dim rsFiltCat As DataTable
            SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT * FROM FilterCategories", "", rsFiltCat)
            Dim FiltCatRow As Integer
            For FiltCatRow = 0 To rsFiltCat.Rows.Count - 1
                'Load Filter
                cboFilter.Items.Add(rsFiltCat.Rows(FiltCatRow).Item("FilterId") & " - " & rsFiltCat.Rows(FiltCatRow).Item("Description"))
                'jrs 2019/07/26
                If DefaultFlexDisplayFilter = rsFiltCat.Rows(FiltCatRow).Item("Description") And Session("cboFilterIdx") Is Nothing Then
                    Session("cboFilterIdx") = FiltCatRow
                End If
            Next
            If Session("cboFilterIdx") Is Nothing Then
                cboFilter.SelectedIndex = 0
            Else
                cboFilter.SelectedIndex = Session("cboFilterIdx")
            End If
            'jrs 2021/10/20
            Session("DefaultFlexDisplayFilterOrg") = DefaultFlexDisplayFilter

            LoadTopCategories() 'dsb 2025/08/08
            LoadCategories()
            'Load Files
            LoadFiles(Session("VarSelCheck"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("ActiveBase"),
                Session("VarSelCheckHis"), Session("PutFdrvRun2"), Session("WorkingFile"), Session("FilesSelected"), Session("IFsRunFiles"),
                Session("RunFiles"), Session("ShortSce"), lstFiles)
            Session("iGDPSel") = iGDPSel
            Session("iPopSel") = iPopSel
            'jrs 2005/03/15
            Session("AnnotationFile") = Nothing
            'jrs 2005/04/19
            ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)
            If Not (Session("PDFilesIdxs")(0) Is Nothing) Then
                Dim fileIdx As Integer
                For fileIdx = 0 To lstFiles.Items.Count - 1
                    lstFiles.Items(fileIdx).Selected = Session("PDFilesIdxs")(fileIdx)
                Next fileIdx
            End If

            RefreshIdxS()

            'jrs 2011/11/04
            If Not Session("txtFiles") Is Nothing Then
                txtFiles.Value = Session("txtFiles")
            End If
            'jrs 2012/12/05
            If Not Session("txtDims") Is Nothing Then
                txtDims.Value = Session("txtDims")
            End If
            'jrs 2023/09/20
            If Not Session("txtDims2") Is Nothing Then
                txtDims2.Value = Session("txtDims2")
            End If

            'jrs 2017/03/24
            If Not Session("txtPacks") Is Nothing Then
                txtPacks.Value = Session("txtPacks")
            End If

            'jrs 2005/11/11
            If Not (Session("oldLastYear") Is Nothing) Then
                Session("LastYear") = Session("oldLastYear")
                Session("oldLastYear") = Nothing
            End If
            If Not (Session("oldFirstYear") Is Nothing) Then
                Session("FirstYear") = Session("oldFirstYear")
                Session("oldFirstYear") = Nothing
                'jrs 2024/09/04
                If Session("mnuUseStartYrDivby5") Then
                    Session("mnuUseStartYrDivby5") = False
                    Session("StartYrIdx") = 1
                End If
            End If
            If Session("DisplayLastYear") = 0 Then
                Session("DisplayLastYear") = Session("LastYear")
            End If

            'jrs 2016/03/23
            Dim iFirstYear As Integer
            If Session("FirstYear") Mod 5 = 0 Then
                iFirstYear = Session("FirstYear")
            Else
                Dim Diff As Integer
                Diff = Session("FirstYear") Mod 5
                iFirstYear = Session("FirstYear") + 5 - Diff
                lstRunHorizon.Items.Add(CStr(Session("FirstYear")))
            End If
            'jrs 2012/04/12
            'Load Horizon years
            Dim i As Integer
            For i = iFirstYear To Session("LastYear") Step 5 'jrs 2016/03/26 Session("FirstYear"")
                lstRunHorizon.Items.Add(CStr(i))
                If Session("DisplayLastYear") = i Then
                    lstRunHorizon.Items(lstRunHorizon.Items.Count - 1).Selected = True
                End If
            Next i


        Else
            iGDPSel = Session("iGDPSel")
            iPopSel = Session("iPopSel")
            rs = Session("PackagedDispRs")

            'jrs 2023/05/24 *** start ***
            ''if you select 2 scenarios, draw a graph, come back select only the 2nd one, draw a graph, when you come back it's back to the 2 again
            ''jrs 2013/05/22
            'If Not Session("txtDims") Is Nothing And txtDims.Value = "" And InStr(Session("txtDims"), CStr(lstDim.SelectedIndex)) > 0 Then
            '    txtDims.Value = Session("txtDims")
            'End If


            ''jam 2022/05/08
            'If Not Session("txtFiles") Is Nothing And txtFiles.Value = "" And InStr(Session("txtFiles"), CStr(lstFiles.SelectedIndex)) > 0 Then
            '    txtFiles.Value = Session("txtFiles")
            'End If
            'jrs 2023/05/24 *** end ***

        End If

        'jrs 11/8/04
        Session("RefreshGraph") = "true"
        'jrs 2005/12/10
        Session("TableHis") = Nothing
        'jrs 2020/08/03
        If Session("FilesCol") Is Nothing Then
            FilesCol = New Collection
        Else
            FilesCol = Session("FilesCol")
        End If
        If Session("dfCol") Is Nothing Then
            dfCol = New Collection
        Else
            dfCol = Session("dfCol")
        End If
        'jrs 2021/02/08
        Session("bAlias") = False
        'jrs 2024/01/17
        Session("YAxisMax") = Nothing
        Session("YAxisMin") = Nothing
        'jrs 2024/01/31
        Session("gridLineWidth") = Nothing
        Session("HorizontalLineValue") = Nothing
        Session("HorizontalLineText") = Nothing

        'jrs 2021/06/23
        If Not Page.IsPostBack Then
            WriteFormControlCaption(Me, Session("PutFdrvRun"), sLanguage)
        End If
        'jrs 2023/07/14
        Dim IsPostBack4JS As String = "var IsPostBack4JS="
        If Page.IsPostBack Then
            IsPostBack4JS = IsPostBack4JS & "1;"
        Else
            IsPostBack4JS = IsPostBack4JS & "0;"
        End If
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "IsPostBack", IsPostBack4JS, True)

        'jrs 2023/12/08
        If Session("Message") > "" Then
            Dim sMsg = "<script>alert('" & Session("Message") & "');</script>"
            Session("Message") = ""
            Response.Write(sMsg)
        End If

        'jrs 2025/06/19
        imgLogoNEPAD.Visible = (Session("VersionName") = "NEPAD")
        imgRwandaFlag.Visible = imgLogoNEPAD.Visible
        'jrs 2026/08/10
        Session.Remove("Rank_Year")

    End Sub

    'jrs 2011/02/11
    Private Sub RefreshIdxS()
        'jrs 2006/01/18
        'jrs 2013/05/09
        If lstDim.Items.Count > 0 Then
            ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
        Else
            ReDim Session("DimIdxs")(1)
        End If

        If Not (Session("DimIdxs")(0) Is Nothing) Then
            Dim dimIdx As Integer
            For dimIdx = 0 To lstDim.Items.Count - 1
                lstDim.Items(dimIdx).Selected = Session("DimIdxs")(dimIdx)
            Next dimIdx
        End If
        'jrs 2006/08/22
        'jrs 2013/05/09
        If lstDim2.Items.Count > 0 Then
            ReDim Preserve Session("DimIdxs2")(lstDim2.Items.Count - 1)
        Else
            ReDim Session("DimIdxs2")(1)
        End If

        If Not (Session("DimIdxs2")(0) Is Nothing) Then
            Dim dimIdx2 As Integer
            For dimIdx2 = 0 To lstDim2.Items.Count - 1
                lstDim2.Items(dimIdx2).Selected = Session("DimIdxs2")(dimIdx2)
            Next dimIdx2
        End If
        'jrs 2011/01/20
        'jrs 2013/05/09
        If lstDim3.Items.Count > 0 Then
            ReDim Preserve Session("DimIdxs3")(lstDim3.Items.Count - 1)
        Else
            ReDim Session("DimIdxs3")(1)
        End If
        If Not (Session("DimIdxs3")(0) Is Nothing) Then
            Dim dimIdx3 As Integer
            For dimIdx3 = 0 To lstDim3.Items.Count - 1
                lstDim3.Items(dimIdx3).Selected = Session("DimIdxs3")(dimIdx3)
            Next dimIdx3
        End If
    End Sub


    Private Sub cmdBar_Click()
        'jam 2022/07/10
        Dim randTabs As Integer
        Dim rnd As New Random()

        'jrs 2011/04/11
        ''jrs 2005/11/11
        'Session("oldLastYear") = Session("LastYear")
        'Session("oldFirstYear") = Session("FirstYear")

        'jrs 2016/01/04
        'Turn off regression
        If Not (Session("RegressType") Is Nothing) Then
            If Session("RegressType") > "" Then
                Session("RegressType") = ""
                Session("TableTitle") = ""
                Session("mnuLogistic") = False
            End If
        End If

        LoadPackage()
        If Session("COUNTE") = 0 Then
            Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstPacks.SelectedValue

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If


        On Error GoTo DefaultInterval
        'JRS 2/13/02 LABELS If CInt(Txt_Interval.Text) < 1 Then Txt_Interval.Text = Str$(1)
        'JRS 2/13/02 LABELS Interval% = CInt(Txt_Interval.Text)
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("GType") = "BAR"

        'jrs 2011/08/30 Support.FormManager.Show(Me, "frm_ProEssential.aspx")
        'Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")


        If Not Session("mnuTabs") Then 'jam 2022/07/02
            'jrs 2011/08/30 Support.FormManager.Show(Me, "frm_ProEssential.aspx")
            Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
        Else 'jam 2022/07/02


            'jam 2022/07/10

            randTabs = rnd.Next(61, 80)

            PackJScritpBuilder()

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow", " window.open('frm_GraphicalDisplay.aspx','_newtab" + CStr(randTabs) + " '); ", True)





        End If





        Exit Sub

DefaultInterval:
        Session("Interval") = 1
        Resume Next

    End Sub

    Private Sub LoadVariables()
        lstPacks.Items.Clear()
        Dim rsL As DataTable
        Dim str As String
        Dim sql As String
        'jrs 2016/04/21 Added Display Units
        sql = "SELECT DISTINCT L.ListName, L.DisplayUnits FROM VarLists as L"
        'sql = "SELECT I.Definition, L.VarName, L.Param1, L.Param2, L.DisplayTitle FROM VarLists as L, IFsVar as I WHERE L.VarName = I.Name AND L.ListName = 'PackagedDisplays'"
        'jrs 2011/04/15
        'If lstCategories.SelectedIndex > 0 Then
        '    'sql = sql + " AND L.Category = '" & lstCategories.List(lstCategories.ListIndex) & "'"
        '    sql = sql + " WHERE L.Category = '" & lstCategories.SelectedValue & "'"
        '    'jrs 2005/11/09
        '    If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
        '        sql = sql + " AND L.HistoryplusForecast = False"
        '    Else 'jrs 2005/11/16
        '        sql = sql + " AND L.HisCompEmpFor = False" 'L.ListName NOT IN (SELECT V1.ListName FROM VarLists as V1, VarLists as V2 WHERE V1.ListName = V2.ListName AND V1.History = True AND V2.History = False)"
        '    End If
        'Else
        'jrs 2005/11/09
        If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            sql = sql + " WHERE L.HistoryplusForecast = False"
        Else 'jrs 2005/11/16
            sql = sql + " WHERE L.HisCompEmpFor = False" 'L.ListName NOT IN (SELECT V1.ListName FROM VarLists as V1, VarLists as V2 WHERE V1.ListName = V2.ListName AND V1.History = True AND V2.History = False)"
        End If
        'End If
        'jrs 2006/01/12
        sql = sql + " AND L.Type = 'V'"
        'jrs 2022/07/19
        If UseOnlySQLite Then
            sql = sql & " ORDER BY UPPER(L.ListName)"
        End If

        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rsL)
        Dim i As Integer
        i = 0

        'jrs 2016/04/21
        Dim ListsLoaded As New MyVariables, ListsLoadedWUnits As New MyVariables, ListIdx As Integer

        Do While (Not rsL.Rows.Count = i)
            'jrs 2016/04/21
            ListIdx = ListsLoaded.Item(rsL.Rows(i).Item("ListName")).Index
            If ListIdx = -1 Then
                'jrs 2025/02/03 added and also displayUnits>""
                If Not IsDBNull(rsL.Rows(i).Item("DisplayUnits")) AndAlso rsL.Rows(i).Item("DisplayUnits") > "" Then
                    lstPacks.Items.Add(rsL.Rows(i).Item("ListName") & " - " & rsL.Rows(i).Item("DisplayUnits"))
                    ListsLoadedWUnits.Add(rsL.Rows(i).Item("ListName"), lstPacks.Items.Count - 1)
                Else
                    lstPacks.Items.Add(rsL.Rows(i).Item("ListName"))
                End If
                ListsLoaded.Delete(rsL.Rows(i).Item("ListName"))
                ListsLoaded.Add(rsL.Rows(i).Item("ListName"), lstPacks.Items.Count - 1)
            Else
                ListIdx = ListsLoadedWUnits.Item(rsL.Rows(i).Item("ListName")).Index
                If ListIdx > 0 Then
                    'Search and remove list with units
                    lstPacks.Items.RemoveAt(ListIdx)
                    ListsLoadedWUnits.Delete(rsL.Rows(i).Item("ListName"))
                    'And Add list with No units
                    lstPacks.Items.Add(rsL.Rows(i).Item("ListName"))
                End If
            End If

            i = i + 1
        Loop
        'ReDim Preserve Session("PDFlexPacksIdxs")(lstPacks.Items.Count)
        If lstPacks.Items.Count > 0 Then
            'jrs 2005/04/20
            If Not (Session("PDFlexPacksIdxs") Is Nothing) And Not Page.IsPostBack Then
                'jrs 2008/12/18 lstPacks.SelectedIndex = Session("PDFlexPacksIdx")
                If lstPacks.Items.Count <= Session("PDFlexPacksIDxs").length() Then
                    For i = 0 To lstPacks.Items.Count - 1
                        lstPacks.Items(i).Selected = Session("PDFlexPacksIdxs")(i)
                    Next
                End If
            ElseIf Not Request.QueryString("ListNames") Is Nothing And Not Page.IsPostBack Then
                'jrs 2011/04/12
                URLToLstPacks(Request.QueryString("ListNames"))
            Else
                lstPacks.SelectedIndex = 0
            End If
            Call lstPacks_SelectedIndexChanged(Me, New EventArgs)
            LoadAllDimLists()
            'jrs 2010/12/10
            PackJScritpBuilder()
        End If
    End Sub

    'jrs 2011/04/12
    Private Sub URLToLstPacks(ByVal sListNames)
        Dim x As Object
        x = Split(sListNames, ",,")
        Dim i As Integer, j As Integer
        j = 0
        'jrs 2017/07/04
        Dim SelList As String = "", pos As Integer
        For i = 0 To UBound(x)
            'jrs 2017/07/04 Do While lstPacks.Items(j).Text <> x(i)
            Do While True
                'jrs 2017/07/04
                SelList = lstPacks.Items(j).Value
                If InStr(SelList, x(i)) > 0 Then
                    pos = InStr(SelList, " - ")
                    If pos > 0 Then
                        'jrs 2016/06/02
                        If InStr(pos + 1, SelList, " - ") = 0 Then
                            Dim rs2 As DataTable
                            SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT count(*) FROM VarLists WHERE ListName = """ & Mid(SelList, 1, pos - 1) & """", "", rs2)
                            If rs2.Rows(0).Item(0) > 0 Then
                                SelList = Mid(SelList, 1, pos - 1)
                            End If
                        Else
                            pos = InStr(pos + 1, SelList, " - ")
                            SelList = Mid(SelList, 1, pos - 1)
                        End If
                    End If
                End If

                If SelList = x(i) Then
                    'jrs 2026/05/13
                    'Need to make sure the found list is in the filtering option
                    If cboFilter.SelectedIndex > 0 Then
                        Dim rs3 As DataTable
                        Dim sFilter As String = cboFilter.SelectedValue
                        pos = InStr(sFilter, " - ")
                        If pos > 0 Then
                            sFilter = sFilter.Substring(0, pos - 1)
                        End If
                        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT count(*) FROM VarLists WHERE PriorityList LIKE '%" & sFilter & "%'  AND ListName = """ & SelList & """", "", rs3)
                        If rs3.Rows(0).Item(0) = 0 Then
                            cboFilter.SelectedIndex = 0
                            cboFilter_SelectedIndexChanged(Me, New EventArgs)
                        End If
                    End If

                    Exit Do
                Else
                    j = j + 1
                    If j = lstPacks.Items.Count Then
                        Exit For
                    End If
                End If
            Loop
            lstPacks.Items(j).Selected = True
        Next
        'jrs 2011/11/03
        If lstPacks.SelectedIndex = -1 And lstPacks.Items.Count > 0 Then
            lstPacks.Items(0).Selected = True
        End If
    End Sub


    'dsb 2025/07/29
    Private Sub LoadTopCategories()
        lstTopCategories.Items.Clear()
        lstTopCategories.Items.Add("ALL")
        Dim rsL As DataTable
        Dim str As String
        str = "SELECT DISTINCT TopCategory FROM TopCat"
        'If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
        '    str = str + " WHERE HisCompEmpFor = False AND Type = 'V'"
        'Else
        '    str = str + " WHERE HistoryplusForecast = False AND Type = 'V'"
        'End If
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", str, "", rsL)
        Dim TopCatCol As New Collection
        Dim x() As String
        Dim i As Integer, j As Integer

        j = 0
        Do While Not rsL.Rows.Count = j
            x = Split(rsL.Rows(j).Item("TopCategory").ToString(), ",")
            For i = 0 To UBound(x)
                If Not TopCatCol.Contains(Trim(x(i))) Then
                    TopCatCol.Add(Trim(x(i)), Trim(x(i)))
                End If
            Next i
            j = j + 1
        Loop
        Dim TopCatArr() As String
        ReDim TopCatArr(TopCatCol.Count)
        i = 0
        For Each item In TopCatCol
            i = i + 1
            TopCatArr(i) = item
        Next
        QuickSort(TopCatArr, 1, TopCatCol.Count)
        'For i = 1 To TopCatCol.Count
        '    lstTopCategories.Items.Add(CStr(TopCatArr(i)))
        'Next
        i = 0
        Dim bReloadTopCats As Boolean
        bReloadTopCats = (Not Session("PDFlexTopCategoriesIdxs") Is Nothing)
        If Not bReloadTopCats Then
            ReDim Session("PDFlexTopCategoriesIdxs")(TopCatCol.Count)
            Session("TopCategoryIds") = New Collection
        Else
            lstTopCategories.Items(0).Selected = CBool(Session("PDFlexTopCategoriesIdxs")(0))
            If TopCatCol.Count > UBound(Session("PDFlexTopCategoriesIdxs")) Then
                ReDim Preserve Session("PDFlexTopCategoriesIdxs")(TopCatCol.Count)
            End If
        End If

        For i = 1 To TopCatCol.Count
            lstTopCategories.Items.Add(CStr(TopCatArr(i)))

            lstTopCategories.Items(i).Selected = CBool(Session("PDFlexTopCategoriesIdxs")(i)) Or CStr(TopCatArr(i)) = Session("PrevTopCat")
            If Not bReloadTopCats Then
                CType(Session("TopCategoryIds"), Collection).Add(i, CStr(TopCatArr(i)))
            End If
        Next i
        If lstTopCategories.Items.Count > 0 Then
            If Not bReloadTopCats And (Session("PrevTopCat") = Nothing Or Session("PrevTopCat") = "ALL") Then
                lstTopCategories.Items(0).Selected = True
                Session("PDFlexTopCategoriesIdxs")(0) = True
            End If

            Dim sT As String
            sT = CStr(Session("TableTitle"))
            Call lstTopCategories_SelectedIndexChanged(Me, New System.EventArgs)
            If sT > "" Then
                Session("TableTitle") = sT
            End If
        End If

    End Sub


    Private Sub LoadCategories()
        'jrs 2005/04/28
        lstCategories.Items.Clear()
        lstCategories.Items.Add("ALL")

        Dim rsL As DataTable
        Dim str As String

        'jrs 2025/08/29
        Dim MesoToMacro As Dictionary(Of String, String)
        MesoToMacro = New Dictionary(Of String, String)
        str = "SELECT TopCategory, Category, UserViewCategory FROM TopCat"
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", str, "", rsL)
        Dim m = 0
        Do While Not rsL.Rows.Count = m
            MesoToMacro.Add(rsL.Rows(m).Item("Category"), rsL.Rows(m).Item("TopCategory"))
            m = m + 1
        Loop
        'dsb 2025/09/03
        Dim CatToUserCat As Dictionary(Of String, String)
        CatToUserCat = New Dictionary(Of String, String)
        Dim s = 0
        Do While Not rsL.Rows.Count = s
            CatToUserCat.Add(rsL.Rows(s).Item("Category"), rsL.Rows(s).Item("UserViewCategory"))
            s = s + 1
        Loop


        str = "SELECT DISTINCT v.Category As Category, t.TopCategory As TopCategory, t.UserViewCategory As UserViewCategory FROM VarLists v LEFT JOIN TopCat t ON t.Category = v.Category" 'dsb 2025/08/18 Joined two tables, more columns from table, table names
        'JRS 2005/11/16
        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            str = str + " WHERE v.HisCompEmpFor = False AND v.Type = 'V'" 'jrs 2006/01/18 'ListName NOT IN (SELECT V1.ListName FROM VarLists as V1, VarLists as V2 WHERE V1.ListName = V2.ListName AND V1.History = True AND V2.History = False)"
        Else
            str = str + " WHERE v.HistoryplusForecast = False AND v.Type = 'V'" 'jrs 2013/09/06 added and HistoryPlusForecast = False
        End If

        'jrs 2020/12/03 SQL is not working with LIKE ""*" & sFilter & "*"""
        Dim sFilter As String = ""
        'jrs 2019/03/27
        If cboFilter.SelectedIndex > 0 Then
            sFilter = CStr(cboFilter.SelectedIndex)
            If Len(sFilter) < 10 Then
                sFilter = "0" & sFilter
            End If
            'JRS 2020/12/03 str = str + " AND PriorityList LIKE ""*" & sFilter & "*"""
            str = str.Replace("DISTINCT v.Category As Category", "DISTINCT v.Category As Category, v.PriorityList As PriorityList") 'dsb 2025/08/20 SQL query with table names
        End If

        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", str, "", rsL)

        'jrs 2012/10/26
        Dim CatCol As Collection
        CatCol = New Collection
        Dim x As Object, topCatAll As Object, i As Integer, j As Integer 'dsb 2025/08/18 Added topCatAll As Object
        Dim newTopCat As String 'dsb 2025/08/18
        Dim userViewCat As String 'dsb 2025/08/18
        'dsb 2025/08/19
        Dim CatIgnoreCol As Collection
        CatIgnoreCol = New Collection
        Do While (Not rsL.Rows.Count = j)
            'jrs 2020/12/03
            If sFilter = "" OrElse rsL.Rows(j).Item("PriorityList").ToString.Contains(sFilter) Then
                str = IIf(IsDBNull(rsL.Rows(j).Item("Category")), "", rsL.Rows(j).Item("Category"))
                x = Split(rsL.Rows(j).Item("Category"), ",")
                'On Error Resume Next
                For i = 0 To UBound(x)
                    If Not CatCol.Contains(Trim(x(i))) Then
                        CatCol.Add(Trim(x(i)), Trim(x(i)))
                        'If Not IsDBNull(rsL.Rows(j).Item("UserViewCategory")) Then 'dsb 2025/08/18 *** start *** checks if the fields in row are not null and adds them to CatCol if different name from Category , to address security issue
                        'userViewCat = rsL.Rows(j).Item("UserViewCategory")
                        userViewCat = CatToUserCat(Trim(x(i))) 'dsb 2025/09/03
                        If Not CatCol.Contains(Trim(userViewCat)) Then
                                'dsb 2025/08/20 *** start ***
                                If Not CatIgnoreCol.Contains(Trim(userViewCat)) Then
                                    'CatCol.Add(Trim(userViewCat), Trim(userViewCat))
                                    CatIgnoreCol.Add(Trim(userViewCat), Trim(userViewCat)) 'dsb 2025/08/19
                                End If
                                'dsb 2025/08/20 *** end ***
                            End If
                        'End If
                        'dsb 2025/08/18 *** end ***
                    End If
                Next i
                If Not IsDBNull(rsL.Rows(j).Item("TopCategory")) Then 'dsb 2025/08/18 *** start *** Adds "ALL - {TopCategory}", to address security issue
                    topCatAll = rsL.Rows(j).Item("TopCategory")
                    newTopCat = "ALL - " & Trim(topCatAll)
                    If Not CatCol.Contains(newTopCat) Then
                        If Not CatIgnoreCol.Contains(newTopCat) Then 'dsb 2025/08/20
                            'CatCol.Add(newTopCat, newTopCat)
                            CatIgnoreCol.Add(newTopCat, newTopCat) 'dsb 2025/08/19
                        End If
                    End If
                Else 'jrs 2025/08/29
                    'It's probably because there's a "'" in the Category field
                    If UBound(x) > 0 Then
                        For i = 0 To UBound(x)
                            'Get the Top for each element
                            'Trim(x(i))
                            topCatAll = MesoToMacro(Trim(x(i)))

                            newTopCat = "ALL - " & Trim(topCatAll)
                            If Not CatCol.Contains(newTopCat) Then
                                If Not CatIgnoreCol.Contains(newTopCat) Then 'dsb 2025/08/20
                                    'CatCol.Add(newTopCat, newTopCat)
                                    CatIgnoreCol.Add(newTopCat, newTopCat) 'dsb 2025/08/19
                                End If
                            End If
                        Next
                    End If
                End If 'dsb 2025/08/18 *** end ***
                'On Error GoTo 0
                If UBound(x) = -1 Then
                    CatCol.Add(Trim(str), Trim(str))
                End If
            End If
            j = j + 1
        Loop
        'CatCol.Add("All Categories Available", "All Categories Available") 'dsb 2025/08/18, to address security issue
        CatIgnoreCol.Add("All Categories Available", "All Categories Available") 'dsb 2025/08/19

        'jrs 2012/10/26 need to sort categories
        Dim Cat As Object
        Dim CatArr() As String
        ReDim CatArr(CatCol.Count)
        i = 0
        For Each Cat In CatCol
            i = i + 1
            CatArr(i) = Cat
        Next Cat
        'lstCategories.Sorted = True
        QuickSort(CatArr, 1, CatCol.Count)

        i = 0
        'jrs 2012/03/29
        Dim bReloadCats As Boolean
        bReloadCats = (Not Session("PDFlexCategoriesIdxs") Is Nothing)
        If Not bReloadCats Then
            'jrs 2013/03/20 ReDim Session("PDFlexCategoriesIdxs")(rsL.Rows.Count)
            ReDim Session("PDFlexCategoriesIdxs")(CatCol.Count + CatIgnoreCol.Count) 'dsb 2025/08/20 CatIgnoreCol.Count
            Session("CategoryIds") = New Collection
        Else
            lstCategories.Items(0).Selected = CBool(Session("PDFlexCategoriesIdxs")(0))
            'jrs 2013/05/10
            If CatCol.Count > UBound(Session("PDFlexCategoriesIdxs")) Then
                ReDim Preserve Session("PDFlexCategoriesIdxs")(CatCol.Count + CatIgnoreCol.Count) 'dsb 2025/08/20 CatIgnoreCol.Count
            End If
        End If

        'jrs 2012/12/06
        'Do While (Not rsL.Rows.Count = i)
        Dim k = 1
        For i = 1 To CatCol.Count
            'jrs 2012/12/06
            'str = rsL.Rows(i).Item("Category")
            'lstCategories.Items.Add(str)
            'i = i + 1
            lstCategories.Items.Add(CStr(CatArr(i)))

            'jrs 2012/03/27
            lstCategories.Items(i).Selected = CBool(Session("PDFlexCategoriesIdxs")(i)) Or CStr(CatArr(i)) = Session("PrevCat") 'jrs 2020/12/03 added Or
            If Not bReloadCats Then
                If Not CatIgnoreCol.Contains(CatArr(i)) Then 'dsb 2025/08/19
                    'jrs 2012/12/06 CType(Session("CategoryIds"), Collection).Add(i, str)
                    CType(Session("CategoryIds"), Collection).Add(k, CStr(CatArr(i))) 'dsb 2025/08/18 changed i to k
                    k = k + 1
                End If
                ''jrs 2012/12/06 CType(Session("CategoryIds"), Collection).Add(i, str)
                'CType(Session("CategoryIds"), Collection).Add(i, CStr(CatArr(i)))
            End If
            'Loop
        Next i
        'dsb 2025/08/20
        Dim CatIgnore As Object
        Dim CatIgnoreArr() As String
        ReDim CatIgnoreArr(CatIgnoreCol.Count)
        i = 0
        For Each CatIgnore In CatIgnoreCol
            i = i + 1
            CatIgnoreArr(i) = CatIgnore
        Next CatIgnore
        Dim ki = lstCategories.Items.Count - 1
        For i = 1 To CatIgnoreCol.Count
            lstCategories.Items.Add(CStr(CatIgnoreArr(i)))
            lstCategories.Items(ki + i).Selected = CBool(Session("PDFlexCategoriesIdxs")(ki + i)) Or CStr(CatIgnoreArr(i)) = Session("PrevCat")
        Next i

        If lstCategories.Items.Count > 0 Then
            'jrs 2012/03/29
            ''jrs 2005/04/20
            'If Not (Session("PDFlexCategoriesIdx") Is Nothing) Then
            '    lstCategories.SelectedIndex = Session("PDFlexCategoriesIdx")
            'Else
            '    lstCategories.SelectedIndex = 0
            'End If
            If Not bReloadCats And (Session("PrevCat") = Nothing Or Session("PrevCat") = "ALL") Then 'jrs 2020/12/04 added and PrevCat = Nothing or "ALL"
                lstCategories.Items(0).Selected = True
                Session("PDFlexCategoriesIdxs")(0) = True
            End If

            'jrs 2005/10/13
            Dim sT As String
            sT = CStr(Session("TableTitle"))
            'jrs 2023/02/02 this turns on the all category all the time, even if it was de-selected
            'lstCategories.Items(0).Selected = True 'cdv 2022/07/15
            'Session("PDFlexCategoriesIdxs")(0) = True

            Call lstCategories_SelectedIndexChanged(Me, New System.EventArgs)
            If sT > "" Then
                Session("TableTitle") = sT
            End If
        End If
    End Sub

    Private Sub LoadPackage()

        _logger.Debug("LoadPackage start")
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

        'jrs 2017/08/10 I put this because there was a weird error when selecting too many options, we got the message
        'but when hitting ok, and trying to come back to the page by closing the message page then it sent a message about
        'not allowing multiselection of lists
        Session("MsgParent") = "frm_PackagedDisplaysFlex.aspx"

        'JAM 2020-07-18
        'gLoadPackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
        'Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, lstPacks.SelectedValue, Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
        'Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
        'Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
        'Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
        'Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
        'Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
        'Session("PopMales"), Session("PopFemales"), Session("ListLoaded"), txtFiles.Value, txtDims.Value, txtPacks.Value, lstPacks)

        _logger.Debug("LoadPackage gLoadPackage")

        gLoadPackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, lstPacks.SelectedValue, Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
            Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("bUseCtryAndGroupDec"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
            Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
            Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
            Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
            Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
            Session("PopMales"), Session("PopFemales"), Session("ListLoaded"), Session("sUsrDefCats"), txtFiles.Value, txtDims.Value, txtDims2.Value, txtPacks.Value, lstPacks,
            dfCol, FilesCol, vmnuSQLITE:=Session("mnuSQLite"), OnlyCoreGroups:=Session("bCheckHideNonCoreGroups"),
            IFsRunFiles:=Session("IFsRunFiles")) 'cdv 2021/09/01
        'CDV 2021/10/22 ****START****
        If Session("UserFirstYear") <> Nothing Then
            If Session("FirstYear") <= FirstYearHistory And Session("UserFirstYear") > FirstYearForecast Then
                Session("OriginalFirstYear") = Session("FirstYear")
                Session("UserFirstYear") = Session("FirstYear")
            ElseIf Session("FirstYear") = FirstYearForecast And Session("UserFirstYear") <= FirstYearForecast Then
                Session("OriginalFirstYear") = Session("FirstYear")
            End If

            If Session("UserFirstYear") = Session("FirstYear") Then
                Session("RowAdjustment") = 0
            Else
                Session("RowAdjustment") = Session("UserFirstYear") - Session("FirstYear")
                If Session("RowAdjustment") > 0 Then
                    'jrs 2023/10/18
                    If Session("OriginalFirstYear") <> Session("FirstYear") Then
                        Session("OriginalFirstYear") = Session("FirstYear")
                    End If
                    Session("FirstYear") = Session("UserFirstYear")
                Else
                    Session("RowAdjustment") = 0
                End If
            End If
        End If
        'CDV 2021/10/22 ****END****
        'jrs 2020/08/03
        Session("dfCol") = dfCol
        Session("FilesCol") = FilesCol

        'jrs 2012/07/03
        Session.Remove("bVARN0Loaded")

        'jrs 2014/04/16
        If Session("COUNTE") > 0 Then
            If Session("StaYearHis")(1) > 0 Then
                If Session("StaYearHis")(1) < FirstYearHistory Then 'jrs 2017/12/07
                    If Not IsNumeric(Session("PLOT")(1, 1)) Then
                        CleanStartPLOTWallAvHis(Session("COUNTE"), Session("StaYearHis"), Session("PLOT"), Session("FinYear"))
                    End If
                ElseIf Session("FirstYear") = FirstYearForecast Then
                    If Not IsNumeric(Session("PLOT")(1, 1)) Then
                        CleanStartPLOTWCompareForecast(Session("COUNTE"), Session("StaYearHis"), Session("PLOT"), Session("FinYear"))
                    End If
                End If
            End If
        End If
    End Sub

    'jrs 2024/11/25
    Private Sub SavePackage()

        _logger.Debug("SavePackage start")
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

        'jrs 2017/08/10 I put this because there was a weird error when selecting too many options, we got the message
        'but when hitting ok, and trying to come back to the page by closing the message page then it sent a message about
        'not allowing multiselection of lists
        Session("MsgParent") = "frm_PackagedDisplaysFlex.aspx"

        'JAM 2020-07-18
        'gLoadPackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
        'Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, lstPacks.SelectedValue, Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
        'Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
        'Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
        'Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
        'Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
        'Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
        'Session("PopMales"), Session("PopFemales"), Session("ListLoaded"), txtFiles.Value, txtDims.Value, txtPacks.Value, lstPacks)

        _logger.Debug("LoadPackage gLoadPackage")

        'jrs 2025/09/16
        Dim oldFirstYear As Integer
        oldFirstYear = Session("FirstYear")

        Dim DisYear As Short
        If Session("DisplayTime") = 0 Then
            DisYear = Session("LastYear") - Session("FirstYear") + 1
        Else
            DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        End If

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        Context.Response.Write(",,,,,,")
        'jrs 2025/03/10
        'jrs 2025/09/16 If lstPacks.SelectedValue.ToUpper.Contains("HISTORY") AndAlso lstPacks.SelectedValue.ToUpper.Contains("FORECAST") Then
        If chkViewHistoryForecasts.Checked Then
            For i = FirstYearHistory To Session("DisplayLastYear") - 1
                Context.Response.Write(i.ToString & ",")
            Next
        Else
            For i = Session("FirstYear") To Session("DisplayLastYear") - 1
                Context.Response.Write(i.ToString & ",")
            Next
        End If
        Context.Response.Write(Session("DisplayLastYear"))
        Context.Response.Write(Environment.NewLine)

        gSavePackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, lstPacks.SelectedValue, Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
            Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("bUseCtryAndGroupDec"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
            Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
            Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
            Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
            Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
            Session("PopMales"), Session("PopFemales"), Session("ListLoaded"), Session("sUsrDefCats"), txtFiles.Value, txtDims.Value, txtDims2.Value, txtPacks.Value, lstPacks,
            dfCol, FilesCol, vmnuSQLITE:=Session("mnuSQLite"), OnlyCoreGroups:=Session("bCheckHideNonCoreGroups"),
            IFsRunFiles:=Session("IFsRunFiles"), DisYear:=DisYear, Context:=Context) 'cdv 2021/09/01

        Response.End()

        Session("dfCol") = dfCol
        Session("FilesCol") = FilesCol

        Session.Remove("bVARN0Loaded")

        'jrs 2025/09/16
        Session("FirstYear") = oldFirstYear

    End Sub


    Private Sub cmdLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLine.Click
        'jam 2022/07/10
        Dim randTabs As Integer
        Dim rnd As New Random()

        _logger.Debug("cmdLine_Click")

        'jrs 2011/04/11
        ''jrs 2005/11/11
        'Session("oldLastYear") = Session("LastYear")
        'Session("oldFirstYear") = Session("FirstYear")

        'jrs 2016/01/04
        'Turn off regression
        If Not (Session("RegressType") Is Nothing) Then
            If Session("RegressType") > "" Then
                Session("RegressType") = ""
                Session("TableTitle") = ""
                Session("mnuLogistic") = False
            End If
        End If

        LoadPackage()
        If Session("COUNTE") = 0 Then
            Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstPacks.SelectedValue

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GType") = "LINE"

        'jrs 2011/04/05
        Dim TargetUrl As String
        TargetUrl = "frm_GraphicalDisplay.aspx"
        'jrs 2022/12/14 added and sLanguage = ""
        If txtListNames.Value > "" And sLanguage = "" Then
            TargetUrl = TargetUrl & "?ListNames=" & PrepareForURL(txtListNames.Value)
            TargetUrl = TargetUrl & "&HistFor=" & txtHistFor.Value
            'jrs 2011/04/11
            TargetUrl = TargetUrl & "&GrpOp="
            Select Case True
                Case Session("bUseGroup") 'Groups 1
                    TargetUrl = TargetUrl & "1"
                Case Session("bUseGroupDec") 'Dec Groups 2
                    TargetUrl = TargetUrl & "2"
                Case Session("bUseGList") 'G-List 3
                    TargetUrl = TargetUrl & "3"
                Case Session("bUseCtryAndGroup") 'Countries and Groups 4
                    TargetUrl = TargetUrl & "4"
                Case Session("bUseCtryAndGroupDec") 'Countries and Dec Groups 5
                    TargetUrl = TargetUrl & "5"
                Case Else 'Regions 0
                    TargetUrl = TargetUrl & "0"
            End Select
            'parameter for dimensions
            'jrs 2013/10/08
            Session("Dim1sLoaded") = DimLstToURL(lstDim, 1)
            TargetUrl = TargetUrl & Session("Dim1sLoaded")
            'jrs 2013/10/08
            Session("Dim1sLoaded") = Replace(Session("Dim1sLoaded"), "&Dim1=", "")
            TargetUrl = TargetUrl & DimLstToURL(lstDim2, 2)
            TargetUrl = TargetUrl & DimLstToURL(lstDim3, 3)
            TargetUrl = TargetUrl & FileLstToURL(lstFiles, txtFiles.Value)
            'jrs 2013/01/18
            TargetUrl = TargetUrl & "&DisplayLastYear=" & Session("DisplayLastYear")
        End If
        'jrs 2023/06/21
        If TargetUrl.Length > 300 Then
            TargetUrl = "frm_GraphicalDisplay.aspx"
        End If

        'jam 2022/07/02
        'Support.FormManager.Show(Me, TargetUrl)

        If Not Session("mnuTabs") Then 'jam 2022/07/02
            'jrs 2005/06/07
            'jrs2012/09/26 Session("TableTitle") = lstPacks.SelectedValue
            Support.FormManager.Show(Me, TargetUrl)
            'frm_TableDisplay.Caption = lstPacks.List(lstPacks.ListIndex)
        Else 'jam 2022/07/02
            'Support.FormManager.Show(blank, "frm_TableDisplay.aspx")

            'Dim pageurl As String
            'pageurl = "frm_TableDisplay.aspx"
            'Response.Write("<script> window.open('" + pageurl + "','_blank'); </script>")
            'Response.Redirect("frm_TableDisplay.aspx")

            'jam 2022/07/10

            randTabs = rnd.Next(41, 60)

            PackJScritpBuilder()

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow", " window.open('" + TargetUrl + "','_newtab" + CStr(randTabs) + " '); ", True)




        End If



    End Sub



    Private Sub cmdTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        'jrs 2005/11/11
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        'jam 2022/07/09
        Dim randTabs As Integer
        Dim rnd As New Random()

        'jam 2022/07/08
        Session("ReloadbyTabs") = False

        LoadPackage()
        If Session("COUNTE") > 0 Then
            If Not Session("mnuTabs") Then 'jam 2022/07/02
                'jam 2022/07/07
                Session("ReloadbyTabs") = False
                'jrs 2005/06/07
                'jrs2012/09/26 Session("TableTitle") = lstPacks.SelectedValue
                Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
                'frm_TableDisplay.Caption = lstPacks.List(lstPacks.ListIndex)
            Else 'jam 2022/07/02
                'Support.FormManager.Show(blank, "frm_TableDisplay.aspx")

                'Dim pageurl As String
                'pageurl = "frm_TableDisplay.aspx"
                'Response.Write("<script> window.open('" + pageurl + "','_newtab1'); </script>")
                'Response.Redirect("frm_TableDisplay.aspx")
                'jam 2022/07/09
                Session("ReloadbyTabs") = True
                randTabs = rnd.Next(21, 40)

                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow", "window.open('frm_TableDisplay.aspx','_newtab1');", True)



                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "OpenWindow", " window.open('frm_PackagedDisplaysFlex.aspx');", True)

                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow", "lstDim3.style.visibility = 'hidden'; window.open('frm_TableDisplay.aspx','_newtab" + CStr(randTabs) + " '); ", True)

                PackJScritpBuilder()

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "OpenWindow", "window.open('frm_TableDisplay.aspx','_newtab" + CStr(randTabs) + " '); ", True)



            End If
        End If
    End Sub

    'dsb 2025/08/04
    Private Sub lstTopCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTopCategories.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To lstTopCategories.Items.Count - 1
            If lstTopCategories.Items(i) IsNot Nothing Then 'dsb 2025/08/07
                Session("PDFlexTopCategoriesIdxs")(i) = lstTopCategories.Items(i).Selected
            End If
        Next

        If Not Page.IsPostBack Then
            LoadCategories()
        End If
    End Sub

    Private Sub lstCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCategories.SelectedIndexChanged
        'jrs 2012/03/29
        ''jrs 2005/04/19
        'Session("PDFlexCategoriesIdx") = lstCategories.SelectedIndex
        Dim i As Integer
        For i = 0 To lstCategories.Items.Count - 1
            Session("PDFlexCategoriesIdxs")(i) = lstCategories.Items(i).Selected
        Next

        'jrs 2011/04/15
        If Not Page.IsPostBack Then
            LoadVariables()
            'jrs 2011/02/11
            RefreshIdxS()
        End If
    End Sub

    Private Sub lstPacks_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPacks.SelectedIndexChanged
        'jrs 2005/04/19
        'jrs 2008/12/18 Session("PDFlexPacksIdx") = lstPacks.SelectedIndex
        ReDim Session("PDFlexPacksIdxs")(lstPacks.Items.Count - 1)

        'jrs 2008/12/17 check if using multiple lists
        'Find variable to load dimensions
        Dim idx As Integer, bDim2 As Boolean, bDim3 As Boolean

        Dim pp As Integer
        Dim idxM As Integer, bDim2M As Boolean, bDim3M As Boolean
        Dim sListNames As String, sHistFor As String
        'jrs 2012/10/25
        'sHistFor = "False"
        Dim sCompFor As String

        'jrs 2016/04/21
        Dim SelList As String, pos As Integer

        For pp = 0 To lstPacks.Items.Count - 1
            Session("PDFlexPacksIdxs")(pp) = lstPacks.Items(pp).Selected
            'jrs 2023/01/06 added and SelList = ""
            If lstPacks.Items(pp).Selected Or (pp = lstPacks.Items.Count - 1 And SelList = "") Then
                If lstPacks.Items(pp).Selected Then
                    SelList = lstPacks.Items(pp).Value
                ElseIf SelList = "" Then
                    lstPacks.Items(0).Selected = True
                    SelList = lstPacks.Items(0).Value
                End If
                'jrs 2016/04/21
                SelList = lstPacks.Items(pp).Value
                pos = InStr(SelList, " - ")
                If pos > 0 Then
                    'jrs 2016/06/02
                    If InStr(pos + 1, SelList, " - ") = 0 Then
                        Dim rs2 As DataTable
                        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "Select count(*) FROM VarLists WHERE ListName = """ & Mid(SelList, 1, pos - 1) & """", "", rs2)
                        If rs2.Rows(0).Item(0) > 0 Then
                            SelList = Mid(SelList, 1, pos - 1)
                        End If
                    Else
                        pos = InStr(pos + 1, SelList, " - ")
                        SelList = Mid(SelList, 1, pos - 1)
                    End If
                End If

                SearchDimstoLoadforVList(SelList, idxM, bDim2M, bDim3M)
                If idx = 0 Then
                    idx = idxM
                    bDim2 = bDim2M
                    bDim3 = bDim3M
                Else
                    'check for compatibility of dimensions
                    If VDim1(idxM) > 0 And VDim1(idx) > 0 Then
                        If VDim1(idxM) <> VDim1(idx) Then
                            DisableButtons()
                            LoadMessage("Incompatible Variable Lists, please select a different set.", Session("Message"))
                            Support.FormManager.StateStore(Me)
                            Support.FormManager.Show(Me, "frm_Message.aspx")
                        End If
                        'check 2nd dim
                        If VDim2(idxM) > 0 And VDim2(idx) > 0 Then
                            If VDim2(idxM) <> VDim2(idx) Then
                                DisableButtons()
                                LoadMessage("Incompatible Variable Lists, please select a different set.", Session("Message"))
                                Support.FormManager.StateStore(Me)
                                Support.FormManager.Show(Me, "frm_Message.aspx")
                            End If
                            'if one of them needs the selection allow it
                            If Not bDim2 And bDim2M Then
                                bDim2 = bDim2M
                            End If

                            'check 3rd dim
                            If VDim3(idxM) > 0 And VDim3(idx) > 0 Then
                                If VDim3(idxM) <> VDim3(idx) Then
                                    DisableButtons()
                                    LoadMessage("Incompatible Variable Lists, please select a different set.", Session("Message"))
                                    Support.FormManager.StateStore(Me)
                                    Support.FormManager.Show(Me, "frm_Message.aspx")
                                End If
                                'if one of them needs the selection allow it
                                If Not bDim3 And bDim3M Then
                                    bDim3 = bDim3M
                                End If
                            ElseIf VDim3(idxM) > 0 Then 'if saved var has no dim 3, save the current one (M)
                                idx = idxM
                                bDim2 = bDim2M
                                bDim3 = bDim3M
                            End If
                        ElseIf VDim2(idxM) > 0 Then 'if saved var has no dim 2, save the current one (M)
                            idx = idxM
                            bDim2 = bDim2M
                            bDim3 = bDim3M
                        End If
                    ElseIf VDim1(idxM) > 0 Then 'if saved var has no dims, save the current one (M)
                        idx = idxM
                        bDim2 = bDim2M
                        bDim3 = bDim3M
                    End If
                End If 'checking dimensions
                If sListNames > "" Then
                    'jrs 2010/10/08 sListNames = sListNames & ",'" & Replace(lstPacks.Items(pp).Value, "'", "''") & "'"
                    'jrs 2016/04/21 sListNames = sListNames & ",""" & lstPacks.Items(pp).Value & """"
                    sListNames = sListNames & ",""" & SelList & """"
                Else
                    'jrs 2010/10/08 sListNames = "'" & Replace(lstPacks.Items(pp).Value, "'", "''") & "'"
                    'jrs 2016/04/21 sListNames = """" & lstPacks.Items(pp).Value & """"
                    sListNames = """" & SelList & """"
                End If

                'dsb 2025/08/12 chkViewHistoryForecasts
                'sHistFor = 

                'jrs 2012/12/06
                'jrs 2016/10/10 If sHistFor <> "False" Or sCompFor = "True" Then
                If sHistFor <> "True" Or sCompFor = "True" Then
                    SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT HistoryplusForecast, CompareOtherForecast FROM VarLists WHERE ListName = '" &
                        Replace(SelList, "'", "''") & "'", "", rs) 'jrs 2016/04/21 replace lstPacks.items(pp).value with SelList
                    If rs.Rows(0).Item(0) Then
                        sHistFor = "True"
                    Else
                        'jrs 2012/10/25
                        'sHistFor = "False"
                        If sCompFor <> "True" Then
                            'jrs 2013/07/12
                            If sHistFor = "" Then
                                sHistFor = "False"
                            End If
                            If rs.Rows(0).Item(1) Then
                                sCompFor = "True"
                            Else
                                sCompFor = "False"
                            End If
                        End If
                    End If
                End If
            End If 'if selected
        Next pp

        'dsb 2025/08/12  ***start***
        Dim bHPF As Boolean = False
        If chkViewHistoryForecasts IsNot Nothing Then
            bHPF = chkViewHistoryForecasts.Checked
        ElseIf Session("bCheckViewHistoryForecasts") IsNot Nothing Then
            bHPF = CBool(Session("bCheckViewHistoryForecasts"))
        End If
        sHistFor = If(bHPF, "True", "False")
        'dsb 2025/08/12 ***end***

        'jrs 2011/04/05
        txtListNames.Value = sListNames
        txtHistFor.Value = sHistFor
        'jrs 2022/07/19
        If UseOnlySQLite Then
            If sHistFor = "True" Then
                sHistFor = "1"
            Else
                sHistFor = "0"
            End If
        End If
        rs = getVarLists(sListNames, sHistFor, Session("PutFdrvRun"))
        'jrs 2011/10/05
        If rs Is Nothing Then
            Exit Sub
        End If

        'jrs 2011/06/30
        Session("ListLoaded") = sListNames

        Session("PackagedDispRs") = rs
        'jrs 2005/10/13
        'jrs 2012/04/12 Session("TableTitle") = lstPacks.SelectedValue
        If InStr(sListNames, """,""") > 0 Then
            'Multiple lists
            'jrs 2012/09/26 Session("TableTitle") = sListNames
            Session("TableTitle") = Replace(sListNames, """", "")
        Else
            'Single list
            Session("TableTitle") = lstPacks.SelectedValue
        End If
        'jrs 2005/10/18
        'YAxisLabel = ""
        'jrs 2006/02/28
        If rs.Rows.Count = 0 Then
            DisableButtons()
            LoadMessage("This List has been disabled.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        Else
            cmdExport.Enabled = True
            cmdLine.Enabled = True
            cmdTable.Enabled = True
        End If

        'jrs 2008/01/30
        'jrs 2011/01/20 LoadDims(V(idx), bDim2, bDim3)
        Session("SelIdx") = idx

        'jrs 2009/04/24
        'Diable selecting Gender for Death Rates since Population is not gender specific.
        If lstPacks.Items(lstPacks.SelectedIndex).Text = "Death Rates (per Thousand) by Category of Cause" Or
                lstPacks.Items(lstPacks.SelectedIndex).Text = "Death Rates (per Thousand) by Main ICD Categories" Then
            If lstDim3.Items.Count = 3 Then
                lstDim3.Items.RemoveAt(0)
                lstDim3.Items.RemoveAt(0)
                lstDim3.Items(0).Selected = True
                Session("lstDim3Tag") = 0
            End If
        End If

        'jrs 2006/01/12
        'If rs.Rows(0).Item("Formula") Then
        '    Dim dbComp As String
        '    dbComp = Session("PutFdrvRun") + "IFSVAR.MDB"
        '    Dim rsComp As DataTable
        '    'jrs 2006/08/21 added Dim2, Dim3 to check if Dimensions should be flexible
        '    SetDataTablefromAccess(dbComp, "SELECT VarName, Dim2, Dim3 FROM VarListsFormulas WHERE FormulaId = " & CStr(rs.Rows(0).Item("FormulaId")), "", rsComp)
        '    Dim idxPos As Integer
        '    'jrs 2006/08/21 when formulas with doubly dimension variables are loaded check if second dimension should be flexible
        '    Dim sVar As String, bPar2 As Boolean, bPar3 As Boolean, sPar2 As String, sPar3 As String, _
        '    idx As Integer, rr As Integer, sFlexVar As String, idxFlex As Integer
        '    bPar2 = True
        '    bPar3 = True
        '    rr = 0
        '    Do While Not rsComp.Rows.Count = rr
        '        idxPos = InStr(1, rsComp.Rows(rr).Item("VarName"), "[")
        '        sVar = Left(rsComp.Rows(rr).Item("VarName"), idxPos - 1)
        '        idx = IfsVars.Item(sVar).Index
        '        If idx < 0 Then
        '            idx = 0
        '        End If
        '        If sFlexVar = "" Then
        '            sFlexVar = sVar
        '            idxFlex = idx
        '        Else
        '            If VDim1(idx) > 0 And VDim1(idxFlex) = 0 Then
        '                sFlexVar = sVar
        '                idxFlex = idx
        '            End If
        '            If VDim2(idx) > 0 And VDim2(idxFlex) = 0 Then
        '                sFlexVar = sVar
        '                idxFlex = idx
        '            End If
        '            If VDim3(idx) > 0 And VDim3(idxFlex) = 0 Then
        '                sFlexVar = sVar
        '                idxFlex = idx
        '            End If
        '        End If
        '        If bPar2 And VDim2(idx) > 0 And Not IsDBNull(rsComp.Rows(rr).Item("Dim2")) Then
        '            If rsComp.Rows(rr).Item("Dim2") > "" Then
        '                If sPar2 = "" Then
        '                    sPar2 = rsComp.Rows(rr).Item("Dim2")
        '                Else
        '                    If sPar2 <> rsComp.Rows(rr).Item("Dim2") Then
        '                        bPar2 = False
        '                    End If
        '                End If
        '            End If
        '        End If
        '        If bPar3 And VDim3(idx) > 0 And Not IsDBNull(rsComp.Rows(rr).Item("Dim3")) Then
        '            If rsComp.Rows(rr).Item("Dim3") > "" Then
        '                If sPar3 = "" Then
        '                    sPar3 = rsComp.Rows(rr).Item("Dim3")
        '                Else
        '                    If sPar3 <> rsComp.Rows(rr).Item("Dim3") Then
        '                        bPar3 = False
        '                    End If
        '                End If
        '            End If
        '        End If
        '        rr = rr + 1
        '    Loop

        '    LoadDims(sFlexVar, bPar2, bPar3)
        'Else
        '    LoadDims(rs.Rows(0).Item("VarName"), IIf(IsDBNull(rs.Rows(0).Item("Param2")), "", rs.Rows(0).Item("Param2")) = "", _
        '    IIf(IsDBNull(rs.Rows(0).Item("Param3")), "", rs.Rows(0).Item("Param3")) = "")
        'End If

        'jrs 2017/03/24
        If Page.IsPostBack Then
            Session("txtPacks") = txtPacks.Value
        End If
    End Sub

    'jrs 2008/12/17
    Private Sub DisableButtons()
        cmdExport.Enabled = False
        cmdLine.Enabled = False
        cmdTable.Enabled = False
    End Sub

    Private Sub cmdContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        If StandAloneMode = True Then 'cdv 2021/12/14
            UpdateIfsInit()
        Else 'jrs 2026/08/07
            Context.Session.Abandon()
            Support.FormManager.Show(Me, "https://korbel.du.edu/pardee/access-ifs/")
        End If
        'jrs 2005/08/24
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        'jrs 2005/11/09
        Session("TableTitle") = ""
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
                Session("OriginalFirstYear") = FirstYearForecast
                'jrs 2006/06/20
                Session("LastYear") = Session("LastYearForecast")
            End If
        End If
        'jrs 2008/09/04
        Session.Remove("bUseReg")
        Session.Remove("bUseGroup")
        Session.Remove("bUseGroupDec")
        Session.Remove("bUseGList")
        'jrs 2010/11/19
        Session.Remove("bIntercept")
        'jrs 2008/12/18
        Session.Remove("PDFlexPacksIdxs")
        Session.Remove("PDFlexTopCategoriesIdxs") 'dsb 2025/08/04
        'jrs 2012/02/07
        Session.Remove("PDFlexCategoriesIdxs")
        Session.Remove("PDFilesIdxs")
        Session.Remove("DimIdxs")
        Session.Remove("DimIdxs2")
        'jrs 2023/09/21
        Session.Remove("DimIdxs3")
        'jrs 2011/01/20
        Session.Remove("AllDimsIdx")
        Session.Remove("SelIdx")
        'jrs 2011/07/04
        Session.Remove("bAlias")
        Session.Remove("PE_CommonMenu")
        Session.Remove("PE_CommonPlot")
        Session.Remove("oGR1N")
        'jrs 2020/12/22
        Session.Remove("bVARN0Loaded")
        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If
        'jrs 2023/09/21
        Session.Remove("txtFiles")
        Session.Remove("txtDims")
        Session.Remove("txtDims2")

        Support.FormManager.Show(Me, Session("DisplayParent"))
    End Sub
    'cdv 2021/12/14
    Private Sub UpdateIfsInit()
        Dim sIP As String
        sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")
        Dim sSQL As String = "UPDATE LoadFull SET [Value] = """ & Session("bCheckHideNonCoreGroups") & """ WHERE [Variable] = 'CoreGroups$'"
        updateAccessWith1Command(sIP, sSQL)
    End Sub
    'jrs 2005/03/15
    Private Sub cmdAnnotation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnotation.Click
        'jrs 2011/02/11
        SaveDimIdxs()
        'jrs 2005/04/22
        Dim i As Integer
        For i = 0 To lstFiles.Items.Count - 1
            Session("PDFilesIdxs")(i) = lstFiles.Items(i).Selected
        Next

        Dim sDirSess As String = "\Session"
        If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
            sDirSess = sDirSess & "0"
        End If
        sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sFileName As String
        Select Case lstFiles.SelectedIndex
            Case 0
                'jrs 2005/03/24
                sFileName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text) & "WorkingAnnotation.txt"
            Case 1
                sFileName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text) & "IFsBase.sce"
            Case Else
                'jrs 2005/04/12
                sFileName = FindFile(Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text), Replace(Mid(lstFiles.SelectedValue, InStr(lstFiles.SelectedValue, "-") + 2, Len(lstFiles.SelectedValue) - 1 - InStr(lstFiles.SelectedValue, "-")), ".run.db", ".sce", 1, -1, vbTextCompare), "sce")
        End Select

        Session("AnnotationFile") = sFileName

        If sFileName = "" Then
            Session("AnnotationFile") = Nothing
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "Notepad.aspx")
    End Sub

    'jrs 2005/10/13
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
                'jrs 2011/02/11
                SaveDimIdxs()
                'jrs 2008/05/09
                Session("CurrencyParent") = "frm_PackagedDisplaysFlex.aspx"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_CurrencyOpt.aspx")
            Case "Set Title, Display Interval, or Year"
                'jrs 2011/02/11
                SaveDimIdxs()
                mnuDispCtrl_Click(sender, e)
            Case "Display/Run Horizon"
                'jrs 2011/02/11
                SaveDimIdxs()

                mnuDisplayRunHorizon_Click(sender, e)
                'jrs 2011/02/11
                PackJScritpBuilder()
            Case "Using Countries/Regions" 'jrs 2008/09/04
                mnuReg_Click()
            Case "Using Groups" 'jrs 2008/09/04
                mnuGroup_Click()
            Case "Using Decomposed Groups" 'jrs 2008/09/04
                mnuGroupDec_Click()
            Case "Using G-Lists" 'jrs 2008/09/04
                mnuGList_Click()
            Case "Using Intersection for Decomposed Groups or G-Lists" 'jrs 2010/11/19
                Session("bIntercept") = Not Session("bIntercept")
                If Session("bIntercept") Then
                    cswmWebMenu.Groups("mnuGroups").Items("mnuIntercept").IconImage = "checkbox.gif"
                Else
                    cswmWebMenu.Groups("mnuGroups").Items("mnuIntercept").IconImage = "blank.gif"
                End If
                Dim AuxIdx As Integer, DimIdx As Integer
                If Session("bUseGroupDec") Then
                    For DimIdx = 0 To lstDim.Items.Count - 1
                        If lstDim.Items(DimIdx).Selected Then
                            AuxIdx = gGetGroupIdx(lstDim.Items(DimIdx).Text, False, Me)
                            If AuxIdx > 0 Then
                                Session("DimIdxs")(AuxIdx - 1) = True
                                lstDim.Items(DimIdx).Selected = False
                                lstDim.Items(AuxIdx - 1).Selected = True
                            End If
                        End If
                    Next
                ElseIf Session("bUseGList") Then
                    For DimIdx = 0 To lstDim.Items.Count - 1
                        If lstDim.Items(DimIdx).Selected Then
                            AuxIdx = gGetGListIdx(lstDim.Items(DimIdx).Text, False, Me)
                            If AuxIdx > 0 Then
                                Session("DimIdxs")(AuxIdx - 1) = True
                                lstDim.Items(DimIdx).Selected = False
                                lstDim.Items(AuxIdx - 1).Selected = True
                            End If
                        End If
                    Next
                Else
                    For DimIdx = 0 To lstDim.Items.Count - 1
                        If lstDim.Items(DimIdx).Selected Then
                            If lstDim.Items(DimIdx).Text = GRPS(VDim1(Session("SelIdx")), Session("AllDimsIdx")(DimIdx) + 1) Then 'jrs 2012/12/14 added + 1
                                AuxIdx = Session("AllDimsIdx")(DimIdx) + 1
                            Else
                                For AuxIdx = MAXCOM(VDim1(Session("SelIdx"))) - 1 To 1 Step -1
                                    If lstDim.Items(DimIdx).Text = GRPS(VDim1(Session("SelIdx")), AuxIdx) Then
                                        Exit For
                                    End If
                                Next AuxIdx
                            End If
                            If AuxIdx > 0 Then
                                Session("DimIdxs")(AuxIdx - 1) = True
                                lstDim.Items(DimIdx).Selected = False
                                lstDim.Items(AuxIdx - 1).Selected = True
                            End If
                        End If
                    Next
                End If
                PackJScritpBuilder()
            Case "Grouping Aggregations" 'jrs 2023/06/14
                mnuGroupAgg_Click()
            Case "Use estimation to complete data"
                'jrs 2011/02/11
                SaveDimIdxs()
                'jrs 2023/09/19 this was disabling selection in Javascript
                txtDirtyDim.Value = ""

                mnuEstimation_Click()
                'jrs 2011/02/11
                PackJScritpBuilder()
            Case "Use estimation to complete data (only holes no extrapolation)"
                'jrs 2011/03/28
                SaveDimIdxs()
                'jrs 2023/09/19 this was disabling selection in Javascript
                txtDirtyDim.Value = ""

                mnuEstimationOnlyHoles_Click()
                PackJScritpBuilder()
            Case "Use all available Historical Data" 'jrs 2006/09/26
                'jrs 2011/02/11
                SaveDimIdxs()
                'jrs 2023/09/19 this was disabling selection in Javascript
                txtDirtyDim.Value = ""

                mnuUseallHis_Click()
                'jrs 2011/02/11
                PackJScritpBuilder()
            Case "Shortest Run File Horizon" 'jrs 2008/03/28
                'jrs 2011/02/11
                SaveDimIdxs()

                'jrs 2023/09/19 this was disabling selection in Javascript
                txtDirtyDim.Value = ""

                Session("DisplayTime") = 0
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuShort").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuLong").IconImage = "blank.gif"

                'jrs 2011/02/11
                PackJScritpBuilder()

            Case "Longest Run File Horizon"
                'jrs 2011/02/11
                SaveDimIdxs()

                'jrs 2023/09/19 this was disabling selection in Javascript
                txtDirtyDim.Value = ""

                Session("DisplayTime") = 1
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuLong").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuShort").IconImage = "blank.gif"

                'jrs 2011/02/11
                PackJScritpBuilder()

            Case "Create JavaScript File for Flex Displays Variables" 'jrs 2013/05/09
                'ReadFormControlCaption(Me, Session("PutFdrvRun"))
                'jrs 2025/11/25 need to switch to complete list to run this procedure
                If cboFilter.SelectedIndex > 0 Then
                    cboFilter.SelectedIndex = 0
                    cboFilter_SelectedIndexChanged(Me, New EventArgs())
                End If

                CreateJSforFlexDisplays(Session("ActiveBase"), Session("PutFdrvRun"), Session("CategoryIds"))
            'Case "Save Selections directly to File" 'jrs 2024/11/25
            '    SavePackage()
            Case "Explain List" 'jrs 2009/03/17
                'jrs 2011/02/11
                SaveDimIdxs()

                mnuExplain_Click()
            Case "Explain Initials" 'JAM 2017-09-15
                mnuInitials_Click()
            Case "Edit Lists" ' cdv 2022/06/24
                If lstPacks.GetSelectedIndices.Length > 0 Then
                    Dim SelectedLists As String() = {}
                    For i = 0 To lstPacks.Items.Count - 1
                        If lstPacks.Items(i).Selected Then
                            ReDim Preserve SelectedLists(SelectedLists.Length)
                            Dim text = lstPacks.Items(i).Value
                            While text.Contains("-")
                                text = Left(text, Len(text) - 1)
                            End While
                            text = text.Trim(" ") 'cdv 2022/07/14
                            SelectedLists(SelectedLists.Length - 1) = text
                        End If

                    Next
                    Session("SelectedLists") = SelectedLists
                End If
                Session("fromSelfManaged") = "False" 'cdv 2022/07/15
                Support.FormManager.Show(Me, "frm_VariableListEdit.aspx")
            Case "Radial" 'JAM 2009-05-30
                mnuRadial_Click()
            Case "Pie" 'jrs 2011/09/02
                mnuPIe_Click()
            Case "Scatter Plot" 'jrs 2011/09/06
                mnuScatterPlot_Click()
            Case "Stacked Bar" 'JAM 2017-06-11
                mnuStackedBar_Click()
            Case "Stacked Area" 'jrs 2018/05/17
                mnuStackedArea_Click()
            Case "Motion Chart (Gap Minder By Google)"
                LoadMovingChart()
            Case "Bar" 'jrs 2024/12/06
                cmdBar_Click()
            Case "Search" 'jrs 2012/01/20
                'jrs 2013/07/19 Support.FormManager.StateStore(Me) Continue with no selection was giving error
                Support.FormManager.Show(Me, "frm_SearchVList.aspx")
            Case Else 'jrs 2011/04/19
                SaveDimIdxs()
                PackJScritpBuilder()
        End Select
    End Sub
    'JAM 2009-05-30     Adding Radial Graph Option
    Private Sub mnuRadial_Click()


        'jrs 2005/11/11
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        LoadPackage()
        If Session("COUNTE") = 0 Then
            Exit Sub
        End If

        'JAM 2009-06-02 Adding validation and PYear Calculation
        If Session("COUNTE") <= 2 Then
            lblMsg.Text = "You must select at least three variables before you create a radial chart"
            lblMsg.Visible = True
            Exit Sub
        End If


        Session("PYear") = 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then
            Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        End If

        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)



        'jrs 2005/10/13 Session("TableTitle") = lstPacks.SelectedValue



        Session("GType") = "RADIAL"
        Session("SourceRadial") = "Package"

        Session("BaseRadialParent") = "frm_PackagedDisplaysFlex.aspx"

        'jrs 2012/05/17
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_BaseRadial.aspx")

        Dim TargetUrl As String
        TargetUrl = "frm_BaseRadial.aspx"
        If txtListNames.Value > "" Then
            TargetUrl = TargetUrl & "?ListNames=" & PrepareForURL(txtListNames.Value)
            TargetUrl = TargetUrl & "&HistFor=" & txtHistFor.Value
            'jrs 2011/04/11
            TargetUrl = TargetUrl & "&GrpOp="
            Select Case True
                Case Session("bUseGroup") 'Groups 1
                    TargetUrl = TargetUrl & "1"
                Case Session("bUseGroupDec") 'Dec Groups 2
                    TargetUrl = TargetUrl & "2"
                Case Session("bUseGList") 'G-List 3
                    TargetUrl = TargetUrl & "3"
                Case Else 'Regions 0
                    TargetUrl = TargetUrl & "0"
            End Select
            'parameter for dimensions
            TargetUrl = TargetUrl & DimLstToURL(lstDim, 1)
            TargetUrl = TargetUrl & DimLstToURL(lstDim2, 2)
            TargetUrl = TargetUrl & DimLstToURL(lstDim3, 3)
            TargetUrl = TargetUrl & FileLstToURL(lstFiles, txtFiles.Value)
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, TargetUrl)

    End Sub
    'jrs 2009/03/17
    Private Sub mnuExplain_Click()
        Session("ExplainListParent") = "frm_PackagedDisplaysFlex.aspx"
        Session("VarList_SQL") = ""
        Session("VarList_Title") = ""
        Dim pp As Integer
        'jrs 2016/04/21
        Dim SelList As String, pos As Integer

        For pp = 0 To lstPacks.Items.Count - 1
            If lstPacks.Items(pp).Selected Then
                'jrs 2016/04/25
                SelList = lstPacks.Items(pp).Text
                pos = InStr(SelList, " - ")
                If pos > 0 Then
                    'jrs 2016/06/02
                    If InStr(pos + 1, SelList, " - ") = 0 Then
                        Dim rs2 As DataTable
                        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT count(*) FROM VarLists WHERE ListName = """ & Mid(SelList, 1, pos - 1) & """", "", rs2)
                        If rs2.Rows(0).Item(0) > 0 Then
                            SelList = Mid(SelList, 1, pos - 1)
                        End If
                    Else
                        pos = InStr(pos + 1, SelList, " - ")
                        SelList = Mid(SelList, 1, pos - 1)
                    End If
                End If

                AddVarList(SelList, Session("VarList_SQL"), Session("VarList_Title"))
            End If
        Next
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_PackagedDisplays_Exp.aspx")
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''           JAM 2017-09-15 Calling Initials Form
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub mnuInitials_Click()
        Session("InitialsParent") = "frm_PackagedDisplaysFlex.aspx"

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Initials.aspx")
    End Sub
    'jrs 2009/03/17
    Public Sub AddVarList(ByVal sListName As String, ByRef sqlV As String, ByRef sTitle As String)
        If sqlV > "" Then
            sqlV = sqlV & " UNION "
            sTitle = sTitle & ", " & sListName
        Else
            sTitle = sTitle & " " & sListName
        End If
        'Forecast Variables with no formulas
        sqlV = sqlV & "SELECT ListOrder, VarName, DEFINITION, FormulaId, FormulaDetHis, Param2, Param3, [Historic Analog] as Historic_Analog " &
            "FROM VarLists, IFSVAR WHERE Varname = [NAME] AND Formula = False AND " &
            "History = False AND ListName = """ & sListName & """"
        'Historic Variables with no formulas
        sqlV = sqlV & " UNION SELECT ListOrder, VarName, '' as DEFINITION, FormulaId, FormulaDetHis, Param2, Param3, '' as Historic_Analog " &
            "FROM VarLists WHERE Formula = False AND History = True AND ListName = """ &
            sListName & """"
        'Rows with formulas
        sqlV = sqlV & " UNION SELECT ListOrder, VarName, FormulaDet as DEFINITION, FormulaId, FormulaDetHis, Param2, Param3, '' as Historic_Analog " &
            "FROM VarLists WHERE Formula = True AND ListName = """ & sListName & """"
        'jrs 2011/07/12
        'Poverty Level Variables
        sqlV = sqlV & " UNION SELECT ListOrder, VarName, '' as DEFINITION, FormulaId, FormulaDetHis, Param2, Param3, '' as Historic_Analog " &
            "FROM VarLists WHERE NOT EXISTS (SELECT * FROM IFsVar WHERE Varname = [NAME]) AND Formula = False AND " &
            "History = False AND ListName = """ & sListName & """"

    End Sub


    'jrs 2005/10/13
    Public Sub mnuDispCtrl_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("DisplayControlParent") = "frm_PackagedDisplaysFlex.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
        'frm_DisplayControl.DefInstance.Show() 
    End Sub

    'jrs 2005/10/13
    Public Sub mnuDisplayRunHorizon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If CBool(Session("HistoricBase")) = True Then
            Msg = "You cannot change the horizon for historical analysis."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
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
        'jrs 2008/12/18
        cmb_Horizon.Items.Add(Session("FirstYear"))

        For ICount = Session("FirstYear") + Session("Interval") To Session("LastYearForecast") Step Session("Interval") 'jrs 2005/12/11 added step interval
            cmb_Horizon.Items.Add(New ListItem(CStr(ICount)))
        Next ICount
        'jrs 2006/02/03
        'jrs 2011/04/19 lstDim.Visible = False

        'jrs 2012/03/09
        txtHorizon.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdHorizon.ClientID + "')")
        cswmWebMenu.Visible = False

    End Sub

    'JRS 2005/10/13
    Private Sub cmb_Horizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged
        'jrs 2012/02/27
        lstRunHorizon.SelectedIndex = -1

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
        'jrs 2006/02/03
        lstDim.Visible = True
        'jrs 2011/02/11
        PackJScritpBuilder()

        'jrs 2012/04/12
        If Session("DisplayLastYear") Mod 5 = 0 Then
            Dim i As Integer
            For i = 0 To lstRunHorizon.Items.Count - 1
                If Session("DisplayLastYear") = lstRunHorizon.Items(i).Value Then
                    lstRunHorizon.Items(i).Selected = True
                    Exit For
                End If
            Next i
        End If

        cswmWebMenu.Visible = True
    End Sub

    'jrs 2006/01/12
    Private Sub LoadDims(ByVal sVar As String, ByVal bDim2 As Boolean, ByVal bDim3 As Boolean)

        Dim idx As Integer
        Dim i As Integer
        Dim j As Integer
        Dim db As String
        Dim valtbl As DataTable

        idx = IfsVars.Item(sVar).Index
        If idx = -1 Then
            'jrs 2007/10/19
            Dim bPov As Boolean, bFind As Boolean, bPct As Boolean
            'jrs 2014/10/21
            Dim b2005PPP As Boolean
            'jrs 2011/06/17
            Dim bIncR As Boolean
            checkPovertyVarName(sVar, bPov, bPct, bFind, 0, bIncR, 0, b2005PPP)
            If bPov Or bPct Or bFind Or bIncR Then 'jrs 2011/06/17
                idx = IfsVars.Item("INCOMELT190LN").Index 'jrs 2016/10/12 changed to 190
            Else
                'jrs 2010/05/14
                Dim bMortChl As Boolean, bMortAdl As Boolean
                checkMortProbName(sVar, bMortChl, bMortAdl)
                If bMortChl Or bMortAdl Then
                    idx = IfsVars.Item("DEATHCAT").Index
                Else
                    idx = 0
                End If
            End If
        End If

        'jrs 2006/01/21
        Dim DimNum As Integer
        If idx > 0 Then
            DimNum = VDim1(idx)
        Else
            DimNum = 1
        End If

        If DimNum <> Val(Session("lstDimTag")) Then
            'jrs 2006/08/22
            If Val(Session("lstDimTag")) = 0 And DimNum > 0 Then
                lstDim.Visible = True
                lblDim.Visible = True
            End If

            lstDim.Items.Clear()
            Session("lstDimTag") = DimNum

            If DimNum = 1 And (Session("bUseGroup") Or Session("bUseGroupDec")) Then 'jrs 2008/09/04 added GroupDec
                db = Session("IFs_MDB2Active")
                j = 0
                While valtbl.Rows.Count > j
                    lstDim.Items.Add(valtbl.Rows(j).Item("GroupName"))
                    j = j + 1
                End While
            ElseIf DimNum = 1 And Session("bUseGList") Then 'jrs 2008/09/04 added GroupDec 'jrs 2006/01/17
                db = Session("IFs_MDB2Active")
                SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)
                j = 0
                While valtbl.Rows.Count > j
                    lstDim.Items.Add(valtbl.Rows(j).Item("Description"))
                    j = j + 1
                End While
            ElseIf DimNum = 0 Then 'jrs 2006/08/22 handle Variables with no Dimensions (WGDP)
                lstDim.Items.Add("")
                'jrs 2011/01/19 lstDim.Visible = False
                'jrs 2011/01/19 lblDim.Visible = False
            Else
                For i = 1 To MaxVal(DimNum) - 1
                    lstDim.Items.Add(GRPS$(DimNum, i))
                Next i
                'jrs 2009/01/21
                If UCase(lstDim.Items(lstDim.Items.Count - 1).Text) <> "TOTAL" And DimNum > 1 Then
                    lstDim.Items.Add("Total")
                End If
            End If

            If lstDim.Items.Count > 0 Then
                lstDim.SelectedIndex = 0
                ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
            End If

        End If
        'jrs 2006/06/20 added Or Session("lstDim2Tag") = "" Or lstDim2.Visible <> bDim2
        'jrs 2011/01/19 If VDim2(idx) <> Val(Session("lstDim2Tag")) Or CStr(Session("lstDim2Tag")) = "" Or lstDim2.Visible <> bDim2 Then
        If VDim2(idx) <> Val(Session("lstDim2Tag")) Or CStr(Session("lstDim2Tag")) = "" Or
            (Session("lstDim2Tag") > "0") <> bDim2 Or lstDim2.Items.Count = 0 Then
            lstDim2.Visible = True 'jrs 2011/01/19 (VDim2(idx) > 0 And bDim2)
            lblDim2.Visible = True 'jrs 2011/01/19 (VDim2(idx) > 0 And bDim2)
            lstDim2.Items.Clear()
            If VDim2(idx) > 0 And bDim2 Then
                Session("lstDim2Tag") = VDim2(idx)
                For i = 1 To MaxVal(VDim2(idx)) - 1
                    lstDim2.Items.Add(GRPS$(VDim2(idx), i))
                Next i
                'jrs 2009/01/21
                If UCase(lstDim2.Items(lstDim2.Items.Count - 1).Text) <> "TOTAL" And VDim2(idx) > 1 Then
                    lstDim2.Items.Add("Total")
                End If
            Else
                Session("lstDim2Tag") = "0"
                lstDim2.Items.Add("")
            End If
            lstDim2.SelectedIndex = 0
            'jrs 2006/08/22
            ReDim Preserve Session("DimIdxs2")(lstDim2.Items.Count - 1)
        End If

        'jrs 2006/05/17 Dim3
        'jrs 2007/05/29 I added Cstr, no it seems like session stores other data types which are not string, 
        'and thus giving an error when comparing with "", dim2 had it, so maybe this showed before and didn't change it for dim3
        'jrs 2011/01/19 If VDim3(idx) <> Val(Session("lstDim3Tag")) Or CStr(Session("lstDim3Tag")) = "" Or lstDim3.Visible <> bDim3 Then
        If VDim3(idx) <> Val(Session("lstDim3Tag")) Or CStr(Session("lstDim3Tag")) = "" Or
            (Session("lstDim3Tag") > "0") <> bDim3 Or lstDim3.Items.Count = 0 Then
            lstDim3.Visible = True 'jrs 2011/01/19 (VDim3(idx) > 0 And bDim3)
            lblDim3.Visible = True 'jrs 2011/01/19 (VDim3(idx) > 0 And bDim3) 'jrs 2006/02/06
            lstDim3.Items.Clear()
            If VDim3(idx) > 0 And bDim3 Then
                Session("lstDim3Tag") = VDim3(idx)
                For i = 1 To MaxVal(VDim3(idx)) - 1
                    lstDim3.Items.Add(GRPS$(VDim3(idx), i))
                Next i
                'jrs 2009/01/21
                If UCase(lstDim3.Items(lstDim3.Items.Count - 1).Text) <> "TOTAL" And VDim3(idx) > 1 Then
                    lstDim3.Items.Add("Total")
                End If
            Else
                Session("lstDim3Tag") = "0"
                lstDim3.Items.Add("")
            End If
            lstDim3.SelectedIndex = 0
        End If

        'jrs 2008/03/06
        If lstDim3.Visible Then
            If lstDim2.Visible Then
                lstDim3.Style.Item("Left") = "1104px"
                lblDim3.Style.Item("Left") = "1104px"
            Else
                lstDim3.Style.Item("Left") = "904px"
                lblDim3.Style.Item("Left") = "904px"
            End If
        End If
    End Sub


    Private Sub LoadAllDimLists()
        LoadAllDims(lstDim, Session("AllDimsIdx"))
        LoadAllDims(lstDim2, Session("AllDimsIdx"))
        LoadAllDims(lstDim3, Session("AllDimsIdx"))

        ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
        ReDim Preserve Session("DimIdxs2")(lstDim2.Items.Count - 1)
        ReDim Preserve Session("DimIdxs3")(lstDim3.Items.Count - 1)
    End Sub

    'jrs 2008/09/04
    Private Sub mnuReg_Click()
        Session("bUseReg") = True
        Session("bUseGroup") = False
        Session("bUseGroupDec") = False
        Session("bUseGList") = False

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        ChangeGroupingOption()
        'jrs 2011/02/11
        lstDim.SelectedIndex = -1
        lstDim.Items(0).Selected = True
        ReDim Session("DimIdxs")(lstDim.Items.Count - 1)
        RefreshIdxS()
        PackJScritpBuilder()

    End Sub

    'jrs 2008/09/04
    Private Sub mnuGroup_Click()
        'jrs 2008/11/13
        Dim bSkip As Boolean
        bSkip = Session("bUseGroupDec")

        Session("bUseReg") = False
        Session("bUseGroup") = True
        Session("bUseGroupDec") = False
        Session("bUseGList") = False
        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        If Not bSkip Then
            ChangeGroupingOption()
            'jrs 2011/02/11
            lstDim.SelectedIndex = -1
            lstDim.Items(0).Selected = True
            ReDim Session("DimIdxs")(lstDim.Items.Count - 1)
        Else
            Dim AuxIdx As Integer, DimIdx As Integer
            For DimIdx = 0 To lstDim.Items.Count - 1
                If lstDim.Items(DimIdx).Selected Then
                    AuxIdx = gGetGroupIdx(lstDim.Items(DimIdx).Text, False, Me)
                    Session("DimIdxs")(AuxIdx - 1) = True
                    lstDim.Items(DimIdx).Selected = False
                    lstDim.Items(AuxIdx - 1).Selected = True
                End If
            Next
        End If
        RefreshIdxS()
        'jrs 2010/12/10
        PackJScritpBuilder()

    End Sub

    'jrs 2008/09/04
    Private Sub mnuGroupDec_Click()
        'jrs 2008/11/13
        Dim bSkip As Boolean
        bSkip = Session("bUseGroup")

        Session("bUseReg") = False
        Session("bUseGroup") = False
        Session("bUseGroupDec") = True
        Session("bUseGList") = False
        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        If Not bSkip Then
            ChangeGroupingOption()
            'jrs 2011/02/11
            lstDim.SelectedIndex = -1
            lstDim.Items(0).Selected = True
            ReDim Session("DimIdxs")(lstDim.Items.Count - 1)
        Else
            Dim AuxIdx As Integer, DimIdx As Integer
            For DimIdx = 0 To lstDim.Items.Count - 1
                If lstDim.Items(DimIdx).Selected Then
                    AuxIdx = gGetGroupIdx(lstDim.Items(DimIdx).Text, False, Me)
                    Session("DimIdxs")(AuxIdx - 1) = True
                    lstDim.Items(DimIdx).Selected = False
                    lstDim.Items(AuxIdx - 1).Selected = True
                End If
            Next
        End If
        RefreshIdxS()
        'jrs 2010/12/10
        PackJScritpBuilder()
    End Sub

    'jrs 2008/09/04
    Private Sub mnuGList_Click()
        Session("bUseReg") = False
        Session("bUseGroup") = False
        Session("bUseGroupDec") = False
        Session("bUseGList") = True
        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "checkbox.gif"

        ChangeGroupingOption()
        'jrs 2011/02/11
        lstDim.SelectedIndex = -1
        lstDim.Items(0).Selected = True
        ReDim Session("DimIdxs")(lstDim.Items.Count - 1)
        RefreshIdxS()
        'jrs 2010/12/10
        PackJScritpBuilder()
    End Sub

    'jrs 2008/09/04
    Private Sub ChangeGroupingOption()

        Session("lstDimTag") = -1
        If rs.Rows(0).Item("Formula") Then
            Dim dbComp As String
            dbComp = Session("PutFdrvRun") + "IFSVAR.MDB"
            Dim rsComp As DataTable
            'jrs 2006/08/21 added Dim2, Dim3 to check if Dimensions should be flexible
            SetDataTablefromAccess(dbComp, "SELECT VarName, Dim2, Dim3 FROM VarListsFormulas WHERE FormulaId = " & CStr(rs.Rows(0).Item("FormulaId")), "", rsComp)
            Dim idxPos As Integer
            'jrs 2006/08/21 when formulas with doubly dimension variables are loaded check if second dimension should be flexible
            Dim sVar As String, bPar2 As Boolean, bPar3 As Boolean, sPar2 As String, sPar3 As String,
                idx As Integer, rr As Integer, sFlexVar As String, idxFlex As Integer
            bPar2 = True
            bPar3 = True
            rr = 0
            Do While Not rsComp.Rows.Count = rr
                idxPos = InStr(1, rsComp.Rows(rr).Item("VarName"), "[")
                sVar = Left(rsComp.Rows(rr).Item("VarName"), idxPos - 1)
                idx = IfsVars.Item(sVar).Index
                If idx < 0 Then
                    idx = 0
                End If
                If sFlexVar = "" Then
                    sFlexVar = sVar
                    idxFlex = idx
                Else
                    If VDim1(idx) > 0 And VDim1(idxFlex) = 0 Then
                        sFlexVar = sVar
                        idxFlex = idx
                    End If
                    If VDim2(idx) > 0 And VDim2(idxFlex) = 0 Then
                        sFlexVar = sVar
                        idxFlex = idx
                    End If
                    If VDim3(idx) > 0 And VDim3(idxFlex) = 0 Then
                        sFlexVar = sVar
                        idxFlex = idx
                    End If
                End If
                If bPar2 And VDim2(idx) > 0 And Not IsDBNull(rsComp.Rows(rr).Item("Dim2")) Then
                    If rsComp.Rows(rr).Item("Dim2") > "" Then
                        If sPar2 = "" Then
                            sPar2 = rsComp.Rows(rr).Item("Dim2")
                        Else
                            If sPar2 <> rsComp.Rows(rr).Item("Dim2") Then
                                bPar2 = False
                            End If
                        End If
                    End If
                End If
                If bPar3 And VDim3(idx) > 0 And Not IsDBNull(rsComp.Rows(rr).Item("Dim3")) Then
                    If rsComp.Rows(rr).Item("Dim3") > "" Then
                        If sPar3 = "" Then
                            sPar3 = rsComp.Rows(rr).Item("Dim3")
                        Else
                            If sPar3 <> rsComp.Rows(rr).Item("Dim3") Then
                                bPar3 = False
                            End If
                        End If
                    End If
                End If
                rr = rr + 1
            Loop

            'jrs 2011/02/09 LoadDims(sFlexVar, bPar2, bPar3)
        Else
            'jrs 2011/02/09 LoadDims(rs.Rows(0).Item("VarName"), CStr(IIf(IsDBNull(rs.Rows(0).Item("Param2")), "", rs.Rows(0).Item("Param2"))) = "", _
            'CStr(IIf(IsDBNull(rs.Rows(0).Item("Param3")), "", rs.Rows(0).Item("Param3"))) = "")
        End If


        'jrs 2009/04/24
        'Diable selecting Gender for Death Rates since Population is not gender specific.
        If lstPacks.Items(lstPacks.SelectedIndex).Text = "Death Rates (per Thousand) by Category of Cause" Or
            lstPacks.Items(lstPacks.SelectedIndex).Text = "Death Rates (per Thousand) by Main ICD Categories" Then
            If lstDim3.Items.Count = 3 Then
                lstDim3.Items.RemoveAt(0)
                lstDim3.Items.RemoveAt(0)
                lstDim3.Items(0).Selected = True
                Session("lstDim3Tag") = 0
            End If
        End If

    End Sub

    Private Sub mnuEstimation_Click()
        'jrs 2006/06/09
        Session("bFillHis") = Not CBool(Session("bFillHis"))
        If CBool(Session("bFillHis")) Then
            cswmWebMenu.Groups("Display Format").Items("mnuEstimation").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("Display Format").Items("mnuEstimation").IconImage = "blank.gif"
        End If
        'jrs 2011/03/28
        If Session("bFillHis") Then
            cswmWebMenu.Groups("Display Format").Items("mnuEstimationOnlyHoles").IconImage = "blank.gif"
            Session("bFillHisOnlyHoles") = False
        End If
    End Sub

    'jrs 2011/03/28
    Private Sub mnuEstimationOnlyHoles_Click()
        Session("bFillHisOnlyHoles") = Not CBool(Session("bFillHisOnlyHoles"))
        If CBool(Session("bFillHisOnlyHoles")) Then
            cswmWebMenu.Groups("Display Format").Items("mnuEstimationOnlyHoles").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("Display Format").Items("mnuEstimationOnlyHoles").IconImage = "blank.gif"
        End If
        If Session("bFillHisOnlyHoles") Then
            cswmWebMenu.Groups("Display Format").Items("mnuEstimation").IconImage = "blank.gif"
            Session("bFillHis") = False
        End If
    End Sub

    'jrs 2006/08/23
    'Procedure to Load Map
    Private Sub LoadMap()
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

        Dim sDef As String, sUn As String

        'jrs 2006/04/18
        Dim bGrp As Object, FinAll As Integer

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        If rs.Rows.Count = 0 Then
            lblMsg.Text = "Variable doesn't exist!"
            lblMsg.Visible = True
            Session("COUNTE") = 0
            Exit Sub
        End If

        Dim fileIdx As Integer
        Dim DimIdx As Integer 'jrs 2006/01/12
        Dim DimIdx2 As Integer 'jrs 2006/01/15
        Dim DimIdx3 As Integer 'jrs 2006/05/17 Dim3
        Dim dim1 As String
        Dim Dim2 As String
        Dim Dim3 As String 'jrs 2006/05/17 Dim3
        Dim sVar As String
        Dim sTitle As String
        Dim bFirst As Boolean
        bFirst = True
        Dim idx = 0
        Dim idxVar As Integer
        'jrs 2010/03/22 long Formulas (Indices)
        Dim sFormulaDet As String
        Dim rsFormulaDet As DataTable
        Dim sqlFDet As String

        If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And rs.Rows(0).Item("HistoryplusForecast") Then
            LoadMessage("Can't Load Package for History Plus Forecast when using History.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        'jrs 2006/06/06 set up years
        If rs.Rows(idx).Item("HistoryplusForecast") Then
            Session("FirstYear") = FirstYearHistory
            Session("OriginalFirstYear") = FirstYearHistory
        ElseIf rs.Rows(idx).Item("History") Then
            Session("FirstYear") = FirstYearHistory
            Session("OriginalFirstYear") = FirstYearHistory
            Session("LastYear") = LastYearHistory
        End If

        sVar = rs.Rows(idx).Item("VarName")
        'jrs 2007/10/19
        Dim bPov As Boolean, bPovPct As Boolean, bPovFind As Boolean, PovLevel As Single
        'jrs 2011/06/17
        Dim bIncR As Boolean, PovLevel2 As Single
        'jrs 2014/10/21
        Dim b2005PPP As Boolean
        If IfsVars.Item(sVar).Index = -1 And Not rs.Rows(idx).Item("Formula") Then
            checkPovertyVarName(sVar, bPov, bPovPct, bPovFind, PovLevel, bIncR, PovLevel2, b2005PPP)
        End If

        'jrs 2010/05/14
        Dim bMortChl As Boolean, bMortAdl As Boolean
        bMortChl = (InStr(sVar, "POPCHLDMOR") > 0)
        bMortAdl = (InStr(sVar, "POPADLTMOR") > 0)

        For DimIdx = 1 To NR
            For DimIdx2 = 0 To lstDim2.Items.Count - 1
                Session("DimIdxs2")(DimIdx2) = lstDim2.Items(DimIdx2).Selected
                If lstDim2.Items(DimIdx2).Selected Then
                    'jrs 2006/05/17 Dim3
                    For DimIdx3 = 0 To lstDim3.Items.Count - 1
                        'jrs 2011/01/20
                        Session("DimIdxs3")(DimIdx3) = lstDim3.Items(DimIdx3).Selected
                        If lstDim3.Items(DimIdx3).Selected Then
                            dim1 = GRPS(1, DimIdx)

                            If IfsVars.Item(sVar).Index > -1 Then
                                If VDim2(IfsVars.Item(sVar).Index) > 0 Then
                                    If IIf(IsDBNull(rs.Rows(idx).Item("Param2")), "", rs.Rows(idx).Item("Param2")) > "" Then
                                        Dim2 = rs.Rows(idx).Item("Param2")
                                    Else
                                        Dim2 = lstDim2.Items(DimIdx2).Text
                                    End If
                                    'jrs 2006/05/17 Dim3
                                    If VDim3(IfsVars.Item(sVar).Index) > 0 Then
                                        If IIf(IsDBNull(rs.Rows(idx).Item("Param3")), "", rs.Rows(idx).Item("Param3")) > "" Then
                                            Dim3 = rs.Rows(idx).Item("Param3")
                                        Else
                                            Dim3 = lstDim3.Items(DimIdx3).Text
                                        End If
                                    Else
                                        Dim3 = ""
                                    End If
                                Else
                                    Dim2 = ""
                                    Dim3 = ""
                                End If
                            Else
                                'jrs 2006/08/21
                                If lstDim2.Visible Then
                                    Dim2 = lstDim2.Items(DimIdx2).Text
                                Else
                                    Dim2 = ""
                                End If
                                If lstDim3.Visible Then
                                    Dim3 = lstDim3.Items(DimIdx3).Text
                                Else
                                    Dim3 = ""
                                End If
                            End If

                            'jrs 2010/05/14
                            If bMortChl Or bMortAdl Then
                                If rs.Rows(idx).Item("Param2") > "" Then
                                    Dim2 = rs.Rows(idx).Item("Param2")
                                Else
                                    Dim2 = lstDim2.Items(DimIdx2).Text
                                End If
                                If rs.Rows(idx).Item("Param3") > "" Then
                                    Dim3 = rs.Rows(idx).Item("Param3")
                                Else
                                    Dim3 = lstDim3.Items(DimIdx3).Text
                                End If
                            End If

                            If Not IsDBNull(rs.Rows(idx).Item("DisplayTitle")) Then
                                sTitle = rs.Rows(idx).Item("DisplayTitle")
                            Else
                                sTitle = ""
                            End If

                            For fileIdx = 0 To lstFiles.Items.Count - 1
                                Session("PDFilesIdxs")(fileIdx) = lstFiles.Items(fileIdx).Selected
                                If lstFiles.Items(fileIdx).Selected Then
                                    If rs.Rows(idx).Item("Formula") Then
                                        If bFirst Then
                                            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                                                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                                        End If

                                        If IIf(IsDBNull(rs.Rows(idx).Item("FormulaDetHis")), "", rs.Rows(idx).Item("FormulaDetHis")) > "" Then 'jrs 2005/11/03
                                            'jrs 2012/03/13
                                            gLoadCompHisFor(rs, dim1, Dim2, Dim3, fileIdx, 1, Session("bFillHis"), Session("buseAllHis"),
                                                Session("FinYearHis")(Session("COUNTE")), Session("COUNTE"), Session("aFormulas"),
                                                Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me,
                                                Session("FinYear"), Session("VARN"), Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"), Nothing,
                                                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                                        Else
                                            'jrs 2010/03/22 check for big formulas (Indices like Sofi)
                                            'Memo field gets truncated when using UNION
                                            If Len(rs.Rows(idx).Item("FormulaDet")) > 254 Then
                                                'jrs 2010/03/01 sqlFDet = "SELECT L.FormulaDet FROM VarLists as L WHERE L.Formula = True AND L.FormulaId = " & _
                                                'CStr(rs.Rows(idx).Item("FormulaId")) & " AND L.ListName = '" & _
                                                'Replace(lstPacks.SelectedValue, "'", "''") & "'"
                                                sqlFDet = "SELECT L.FormulaDet FROM VarLists as L WHERE L.Formula = True AND L.FormulaId = " &
                                                    CStr(rs.Rows(idx).Item("FormulaId")) & " AND L.ListName IN (" &
                                                    Session("ListLoaded") & ")"
                                                SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sqlFDet, "", rsFormulaDet)
                                                sFormulaDet = rsFormulaDet.Rows(0).Item("FormulaDet")
                                            Else
                                                sFormulaDet = rs.Rows(idx).Item("FormulaDet")
                                            End If

                                            Session("COUNTE") = Session("COUNTE") + 1
                                            gLoadComp(Session("COUNTE"), sFormulaDet, sVar, dim1, Dim2, Dim3,
                                                "", rs.Rows(idx).Item("FormulaId"), Nothing, fileIdx, rs.Rows(idx).Item("HistoryplusForecast"),
                                                False, Nothing, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                                False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                                Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
                                        End If
                                        FormatFormAfterLoad(fileIdx, Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
                                            IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
                                            Session("MaxFinYearHis"))
                                    Else
                                        bGrp = Nothing
                                        If bFirst Then
                                            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                                                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                                        End If
                                        Session("COUNTE") = Session("COUNTE") + 1

                                        If rs.Rows(idx).Item("History") Then
                                            gLoadForDisplayHis(sVar, dim1, DimIdx, Dim2, -1, Dim3, -1,
                                                bGrp, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory,
                                                False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                                                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                            gLoadForDisplayHisSaveLbls(Session("COUNTE"), sVar, dim1, bGrp, sDef, sUn, sTitle, Session("FileNumbr"), Session("VARN"),
                                                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                                        Else
                                            If bIncR Then
                                                gLoadIncomeRange(PovLevel, PovLevel2, dim1, fileIdx, bGrp, bPovPct, b2005PPP,
                                                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                                    Session("SensitivityFlag"), Session("mnuSQLite"))
                                                'jrs 2007/10/19
                                            ElseIf bPov Or bPovPct Then
                                                gLoadPovertyDisp(PovLevel, dim1, fileIdx, bGrp, bPovPct, b2005PPP,
                                                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                                    Session("SensitivityFlag"), Session("mnuSQLite"))
                                            ElseIf bPovFind Then
                                                gFindPovertyLevel(dim1, fileIdx, bGrp, PovLevel, b2005PPP,
                                                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                                                'jrs 2010/05/14
                                            ElseIf bMortChl Then
                                                'JAM 2020-07-18
                                                'gLoadMortProbChldL5(Session("COUNTE"), "", Nothing, True, True, dim1, -1, bGrp, Dim2, Dim3,
                                                'Session("PLOT"), Session("GR1N")(Session("COUNTE")), Session("GR1N")(Session("COUNTE")),
                                                'Session("GR2N")(Session("COUNTE")), Session("GR3N")(Session("COUNTE")), Session("VARN")(1, Session("COUNTE")),
                                                'Session("VARN")(0, Session("COUNTE")), Session("GR4N")(Session("COUNTE")), "", "", fileIdx, False, True,
                                                'Session("PutFdrvRun"), Session("IFs_MDB2Active"), Session("RunFiles"), Me, Session("LastYear"), Session("FirstYear"),
                                                'Session("PopMales"), Session("PopFemales"), Session("FinYear"))


                                                gLoadMortProbChldL5(Session("COUNTE"), "", Nothing, True, True, dim1, -1, bGrp, Dim2, Dim3,
                                                    Session("PLOT"), Session("GR1N")(Session("COUNTE")), Session("GR1N")(Session("COUNTE")),
                                                    Session("GR2N")(Session("COUNTE")), Session("GR3N")(Session("COUNTE")), Session("VARN")(1, Session("COUNTE")),
                                                    Session("VARN")(0, Session("COUNTE")), Session("GR4N")(Session("COUNTE")), "", "", fileIdx, False, True,
                                                    Session("PutFdrvRun"), Session("IFs_MDB2Active"), Session("RunFiles"), Me, Session("LastYear"), Session("FirstYear"),
                                                    Session("PopMales"), Session("PopFemales"), Session("FinYear"), Session("mnuSQLite"))


                                                'jrs 2010/05/14
                                            ElseIf bMortAdl Then
                                                gLoadMortProbAdlt(Session("COUNTE"), "", Nothing, True, True, dim1, -1, bGrp, Dim2, Dim3,
                                                    Session("PLOT"), Session("GR1N")(Session("COUNTE")), Session("GR1N")(Session("COUNTE")),
                                                    Session("GR2N")(Session("COUNTE")), Session("GR3N")(Session("COUNTE")), Session("VARN")(1, Session("COUNTE")),
                                                    Session("VARN")(0, Session("COUNTE")), Session("GR4N")(Session("COUNTE")), "", "", fileIdx, False, True,
                                                    Session("PutFdrvRun"), Session("IFs_MDB2Active"), Session("RunFiles"), Me, Session("LastYear"), Session("FirstYear"),
                                                    Session("PopMales"), Session("PopFemales"), Session("FinYear"), Session("mnuSQLite"))
                                            Else
                                                Dim iRowPosition As Integer, iFirstYear As Integer
                                                If rs.Rows(idx).Item("HistoryplusForecast") Then
                                                    iRowPosition = LastYearHistory - FirstYearHistory + 1
                                                    iFirstYear = FirstYearForecast
                                                Else
                                                    iRowPosition = 1
                                                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then 'JRS 2006/05/22
                                                        iFirstYear = FirstYearForecast
                                                        'jrs 2006/06/06 session("FirstYear") = FirstYearForecast
                                                    Else
                                                        iFirstYear = FirstYearHistory
                                                    End If
                                                End If
                                                idxVar = -1
                                                gLoadForDisplayNET(sVar, idxVar, dim1, DimIdx, Dim2, -1, Dim3, -1, "", fileIdx, (DimIdx = 1), (DimIdx = NR), bGrp, 1, Session("PLOT"), Session("COUNTE"),
                                                    iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                                                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"),
                                                    MyFileforDisplay, MyDF)
                                                'gLoadForDisplay(sVar, idxVar, dim1, DimIdx, Dim2, -1, Dim3, -1, "", fileIdx, (DimIdx = 1), (DimIdx = NR), bGrp, 1, Session("PLOT"), Session("COUNTE"),
                                                'iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), 
                                                'Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                                                'Session("IFs_MDB2Active"), Session("CurrencyVal"))

                                                gLoadForDisplaySaveLbls(Session("COUNTE"), idxVar, dim1, Dim2, Dim3, bGrp, fileIdx, sTitle,
                                                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                                            End If
                                        End If
                                    End If
                                    bFirst = False
                                    If rs.Rows(idx).Item("HistoryplusForecast") And Not rs.Rows(idx).Item("Formula") And Not rs.Rows(idx).Item("History") Then 'jrs 2018/08/14 added and not rs.Rows(idx).Item("History")
                                        FinAll = Session("FinYear")(Session("COUNTE"))
                                        'jrs 2006/06/06 FirstYear = FirstYearHistory
                                        'jrs 2006/06/08 lastyearhistory - 1 in order to keep forecasted values in year 2000 instead of historic ones
                                        gLoadForDisplayHis(sVar, dim1, DimIdx, Dim2, -1, Dim3, -1,
                                            bGrp, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                                            False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                                            Session("LastYearForecast"), Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                                            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                        Session("FinYear")(Session("COUNTE")) = LastYearHistory - FirstYearHistory + FinAll 'jrs 2006/09/22
                                    End If
                                    'jrs 2006/08/23 out to next country/region
                                    fileIdx = lstFiles.Items.Count - 1
                                    DimIdx3 = lstDim3.Items.Count - 1
                                    DimIdx2 = lstDim2.Items.Count - 1
                                End If
                            Next fileIdx
                        End If 'if dim3 selected
                    Next DimIdx3
                End If 'if dim2 selected
            Next DimIdx2
        Next DimIdx 'jrs 2006/01/12


    End Sub


    Private Sub cmdMap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMap.Click
        'jrs 2006/08/23
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        Session("PYear") = 1

        LoadMap()
        If Session("COUNTE") > 0 Then
            Session("MapChosenTable") = "Map"
            Session("MapFieldName") = "StoreData"
            Session("MapVar") = Session("VARN")(1, 1)
            'NumBreaks = 6
            Support.FormManager.Show(Me, "frm_mapViewer.aspx")
        End If

    End Sub

    'jrs 2006/09/26
    Private Sub mnuUseallHis_Click()
        Session("buseAllHis") = Not CBool(Session("buseAllHis"))
        If CBool(Session("buseAllHis")) Then
            cswmWebMenu.Groups("Display Format").Items("mnuuseAllHis").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("Display Format").Items("mnuuseAllHis").IconImage = "blank.gif"
        End If

    End Sub

    'jrs 2008/01/30
    'loop throught the V-List searching for the dimensions to be loaded (originally from VarSelect)
    Private Sub SearchDimstoLoadforVList(ByVal sVar As String, ByRef svIdx As Object, ByRef bDim2 As Boolean, ByRef bDim3 As Boolean)
        Dim idx As Integer
        Dim sDim2 As String
        Dim sDim3 As String
        Dim rsList As DataTable
        Dim rsListIdx As Integer = 0
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT * FROM VarLists WHERE ListName = """ & sVar & """ ORDER BY ListOrder", "", rsList)
        Dim rsListComp As DataTable
        Dim rsListCompIdx As Integer
        bDim2 = True
        bDim3 = True
        svIdx = -1
        Dim MaxDimNum As Integer
        MaxDimNum = 0
        'jrs 2007/10/19
        Dim bPov As Boolean, bPovPct As Boolean, bPovFind As Boolean, PovLevel As Single
        'jrs 2016/03/21
        Dim b2005PPP As Boolean
        'jrs 2011/06/17
        Dim bIncR As Boolean, PovLevel2 As Single
        'jrs 2010/05/14
        Dim bMortChl As Boolean, bMortAdl As Boolean

        'loop each variable
        Do While Not (rsList.Rows.Count = rsListIdx)
            'read index
            idx = ReadIdx(rsList.Rows(rsListIdx).Item("VarName"))
            'jrs 2008/04/07 added in 2009/07/06
            If svIdx = -1 Then
                svIdx = idx
            End If

            'check dimensions
            If VDim1(idx) > 0 And MaxDimNum = 0 Then
                MaxDimNum = 1
                svIdx = idx
            End If
            CheckDims(bDim2, VDim2(idx), idx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param2")), "", rsList.Rows(rsListIdx).Item("Param2")), sDim2, MaxDimNum, svIdx, "", 2, False)
            CheckDims(bDim3, VDim3(idx), idx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param3")), "", rsList.Rows(rsListIdx).Item("Param3")), sDim3, MaxDimNum, svIdx, "", 3, False)

            'if formula loop each member
            If rsList.Rows(rsListIdx).Item("Formula") Then
                SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT * FROM VarListsFormulas WHERE FormulaId = " & rsList.Rows(rsListIdx).Item("FormulaId") & " ORDER BY [CodeVar]", "", rsListComp)
                'jrs 2009/01/16
                rsListCompIdx = 0
                'check dimensions
                Do While Not (rsListComp.Rows.Count = rsListCompIdx)
                    idx = ReadIdx(rsListComp.Rows(rsListCompIdx).Item("VarName"))
                    If VDim1(idx) > 0 And MaxDimNum = 0 Then
                        MaxDimNum = 1
                        svIdx = idx
                    End If
                    CheckDims(bDim2, VDim2(idx), idx, IIf(IsDBNull(rsListComp.Rows(rsListCompIdx).Item("Dim2")), "", rsListComp.Rows(rsListCompIdx).Item("Dim2")),
                        sDim2, MaxDimNum, svIdx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param2")), "", rsList.Rows(rsListIdx).Item("Param2")), 2, True)
                    CheckDims(bDim3, VDim3(idx), idx, IIf(IsDBNull(rsListComp.Rows(rsListCompIdx).Item("Dim3")), "", rsListComp.Rows(rsListCompIdx).Item("Dim3")),
                        sDim3, MaxDimNum, svIdx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param3")), "", rsList.Rows(rsListIdx).Item("Param3")), 3, True)
                    rsListCompIdx = rsListCompIdx + 1
                Loop
            Else 'jrs 2007/10/19
                If idx < 1 Then
                    'jrs 2023/06/15
                    PovLevel = 0
                    PovLevel2 = 0
                    checkPovertyVarName(rsList.Rows(rsListIdx).Item("VarName"), bPov, bPovPct, bPovFind, PovLevel, bIncR, PovLevel2, b2005PPP) 'jrs 2011/06/17
                    If bPov Or bPovPct Or bPovFind Or bIncR Then 'jrs 2011/06/17
                        idx = IfsVars.Item("INCOMELT190LN").Index 'jrs 2016/10/12 changed to 190
                    Else 'jrs 2010/05/13 check Mortality Prob
                        checkMortProbName(rsList.Rows(rsListIdx).Item("VarName"), bMortChl, bMortAdl)
                        If bMortChl Or bMortAdl Then
                            idx = IfsVars.Item("DEATHCAT").Index
                        End If
                    End If

                    If idx > 0 Then
                        'check dimensions
                        If VDim1(idx) > 0 And MaxDimNum = 0 Then
                            MaxDimNum = 1
                            svIdx = idx
                        End If
                        CheckDims(bDim2, VDim2(idx), idx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param2")), "", rsList.Rows(rsListIdx).Item("Param2")), sDim2, MaxDimNum, svIdx, "", 2, False)
                        CheckDims(bDim3, VDim3(idx), idx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param3")), "", rsList.Rows(rsListIdx).Item("Param3")), sDim3, MaxDimNum, svIdx, "", 3, False)
                    End If
                End If
            End If
            rsListIdx = rsListIdx + 1
        Loop
        'if dimensions are different don't Load them, use the ones saved (except on Dim1).
    End Sub



    'jrs 2010/12/10
    'Procedure to build array with dimensions to be loaded for each packaged list
    Private Sub PackJScritpBuilder()
        'Dim rsL As DataTable
        'Dim sql As String
        'sql = "SELECT Dim1, Dim2, Dim3, bDim2, bDim3 FROM VarListsHDR"
        'If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
        '    sql = sql & " WHERE EXISTS (SELECT * FROM VarLists as L WHERE L.ListName = VarListsHDR.ListName " & _
        '    "AND L.HistoryplusForecast = False)"
        'Else
        '    sql = sql & " WHERE EXISTS (SELECT * FROM VarLists as L WHERE L.ListName = VarListsHDR.ListName " & _
        '    "AND L.HisCompEmpFor = False)"
        'End If

        'sql = sql & " ORDER BY ListName"
        'SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rsL)

        Dim sJscript As String, i As Integer

        'jrs 2023/05/19
        Dim sVerNum As String, sSubVerNum As String
        sVerNum = ModelVersion.Replace("Version ", "")
        'jrs 2023/09/11
        Dim pos As Integer
        pos = sVerNum.IndexOf(" ")
        If pos >= 0 Then
            sVerNum = sVerNum.Substring(0, pos)
        End If

        sSubVerNum = SubVersionNumber.ToString
        If SubVersionNumber > 0 Then
            sVerNum = sVerNum & "." & sSubVerNum
        End If
        sVerNum = sVerNum & ".js"

        'dynamically load and add this .js file
        If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            sJscript = "   loadjsfile(""Scripts/FlexDisplaysVarsH." & sVerNum & """);" & vbCr
        Else
            sJscript = "   loadjsfile(""Scripts/FlexDisplaysVars." & sVerNum & """);" & vbCr
        End If

        'sJscript = "    var DimXList = [];" & vbCr
        'For i = 0 To rsL.Rows.Count - 1
        '    sJscript = sJscript & "    DimXList[" & i.ToString & "] = new Array(" & DimStrBuilder(i, rsL) & ");" & vbCr
        'Next
        ''jrs 2011/04/15 adding Categories
        'sJscript = sJscript & vbCr & "    var CatXList = [];" & vbCr
        'sql = "SELECT DISTINCT ListName, Category FROM VarLists"
        'If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
        '    sql = sql & " WHERE HistoryplusForecast = False"
        'Else
        '    sql = sql & " WHERE HisCompEmpFor = False"
        'End If
        'sql = sql & " AND Type = 'V'  ORDER BY ListName"
        'SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rsL)
        ''jrs 2012/12/06
        ''For i = 0 To rsL.Rows.Count - 1
        ''    sJscript = sJscript & "    CatXList[" & i.ToString & "] = new Array(""" & rsL.Rows(i).Item("ListName") & _
        ''    """,""" & rsL.Rows(i).Item("Category") & """," & _
        ''    CType(Session("CategoryIds"), Collection).Item(rsL.Rows(i).Item("Category")).ToString & ");" & vbCr
        ''Next
        'i = 0
        'Dim str As String, catrow As String, j As String, x As Object
        'j = 0
        'Do While True
        '    If j = rsL.Rows.Count Then Exit Do
        '    Str = IIf(IsDBNull(rsL.Rows(j).Item("Category")), "", rsL.Rows(j).Item("Category"))
        '    x = Split(rsL.Rows(j).Item("Category"), ",")
        '    'On Error Resume Next
        '    catrow = ""
        '    For i = 0 To UBound(x)
        '        If CType(Session("CategoryIds"), Collection).Contains(Trim(x(i))) Then
        '            If catrow > "" Then
        '                catrow = catrow & ","
        '            End If
        '            catrow = catrow & CType(Session("CategoryIds"), Collection).Item(Trim(x(i))).ToString
        '        End If
        '    Next i
        '    'On Error GoTo 0
        '    If UBound(x) = -1 Then
        '        catrow = CType(Session("CategoryIds"), Collection).Item(Trim(str)).ToString
        '    End If

        '    sJscript = sJscript & "    CatXList[" & j.ToString & "] = new Array(""" & rsL.Rows(j).Item("ListName") & _
        '    """,""" & rsL.Rows(j).Item("Category") & """,""" & _
        '    catrow & """);" & vbCr

        '    j = j + 1
        'Loop

        'sJscript = sJscript & vbCr & "    var listCount=" & rsL.Rows.Count.ToString & ";" & vbCr
        'sJscript = sJscript & "    var MapToList = [];" & vbCr
        'sJscript = sJscript & "    var FilterListCount=" & rsL.Rows.Count.ToString & ";" & vbCr

        ''3 other variables for the tag
        'sJscript = sJscript & vbCr & "    var currDim1=0;" & vbCr
        'sJscript = sJscript & "    var currDim2=0;" & vbCr
        'sJscript = sJscript & "    var currDim3=0;" & vbCr

        'Grouping variables
        sJscript = sJscript & vbCr & "   var bUseReg=" & LCase(Session("bUseReg")) & ";" & vbCr
        sJscript = sJscript & vbCr & "   var bUseGroup=" & LCase(Session("bUseGroup")) & ";" & vbCr
        sJscript = sJscript & "   var bUseGroupDec=" & LCase(Session("bUseGroupDec")) & ";" & vbCr
        sJscript = sJscript & "   var bUseGList=" & LCase(Session("bUseGList")) & ";" & vbCr
        'jrs 2016/09/30
        sJscript = sJscript & "   var bUseCtryAndGrp=" & LCase(Session("bUseCtryAndGroup")) & ";" & vbCr
        'jrs 2025/05/30
        sJscript = sJscript & "   var bUseCtryAndGrpDec=" & LCase(Session("bUseCtryAndGroupDec")) & ";" & vbCr

        ''Dimension arrays
        'sJscript = sJscript & vbCr & "   var MaxVal=[];" & vbCr
        'For i = 1 To UBound(MaxVal)
        '    sJscript = sJscript & "    MaxVal[" & i.ToString & "] = " & MaxVal(i).ToString & ";" & vbCr
        'Next
        'sJscript = sJscript & vbCr & "   var GRPS=[];" & vbCr
        'For i = 1 To UBound(GRPS, 1)
        '    sJscript = sJscript & "    GRPS[" & i.ToString & "] = new Array(" & DimStrBuilder(i) & ");" & vbCr
        'Next
        'sJscript = sJscript & vbCr & "   var Groups=[];" & vbCr
        'For i = 1 To GCount
        '    sJscript = sJscript & "    Groups[" & i.ToString & "] = """ & GroupNames$(i) & """;" & vbCr
        'Next
        'sJscript = sJscript & "    var iGroupCount=" & GCount.ToString & ";" & vbCr
        'sJscript = sJscript & vbCr & "   var GLists=[];" & vbCr
        'For i = 1 To GListCount
        '    sJscript = sJscript & "    GLists[" & i.ToString & "] = """ & GListNames(i) & """;" & vbCr
        'Next
        'sJscript = sJscript & "    var iGListCount=" & GListCount.ToString & ";" & vbCr

        'jam 2022/07/07
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadDimXList",
            sJscript, True)

        'jam 2022/07/07
        'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadDimXList",
        'sJscript, True)

        lstPacks.Attributes.Add("onchange", "lstPacks_click();")

        'jrs 2011/04/15
        lstCategories.Attributes.Add("onchange", "lstCategories_click();")

        'dsb 2025/07/30
        lstTopCategories.Attributes.Add("onchange", "lstTopCategories_click();")

        'jrs 2012/12/06 need argument event to make it work in FireFox, also the Drag doesn't work in Chrome and Firefox so they need onchange
        'jrs 2011/11/03
        lstFiles.Attributes.Add("onclick", "lstFiles_Click(event);")
        lstFiles.Attributes.Add("onmousedown", "lstFiles_ClickDown(event);")
        lstFiles.Attributes.Add("onmouseup", "lstFiles_ClickUp(event);")
        If Request.Browser.Browser = "Chrome" Then
            lstFiles.Attributes.Add("onchange", "lstFiles_Change(event);")
        ElseIf Request.Browser.Browser = "Firefox" Then
            lstFiles.Attributes.Add("onchange", "lstFiles_Click(event);")
        End If

        'jrs 2012/12/05
        lstDim.Attributes.Add("onclick", "lstDim_Click(event);")
        lstDim.Attributes.Add("onmousedown", "lstDim_ClickDown(event);")
        lstDim.Attributes.Add("onmouseup", "lstDim_ClickUp(event);")
        If Request.Browser.Browser = "Chrome" Then
            lstDim.Attributes.Add("onchange", "lstDim_Change(event);")
        ElseIf Request.Browser.Browser = "Firefox" Then
            lstDim.Attributes.Add("onchange", "lstDim_Click(event);")
        End If

        'jrs 2023/09/20
        lstDim2.Attributes.Add("onclick", "lstDim2_Click(event);")
        lstDim2.Attributes.Add("onmousedown", "lstDim2_ClickDown(event);")
        lstDim2.Attributes.Add("onmouseup", "lstDim2_ClickUp(event);")
        If Request.Browser.Browser = "Chrome" Then
            lstDim2.Attributes.Add("onchange", "lstDim2_Change(event);")
        ElseIf Request.Browser.Browser = "Firefox" Then
            lstDim2.Attributes.Add("onchange", "lstDim2_Click(event);")
        End If

        'jrs 2017/03/24
        lstPacks.Attributes.Add("onclick", "lstPacks_Click(event);")
        lstPacks.Attributes.Add("onmousedown", "lstPacks_ClickDown(event);")
        lstPacks.Attributes.Add("onmouseup", "lstPacks_ClickUp(event);")
        If Request.Browser.Browser = "Chrome" Then
            lstPacks.Attributes.Add("onchange", "lstPacks_Change(event);")
        ElseIf Request.Browser.Browser = "Firefox" Then
            lstPacks.Attributes.Add("onchange", "lstPacks_Click(event);")
        End If
    End Sub

    'jrs 2013/05/09 moved to Function2
    ''jrs 2010/12/10
    ''Procedure to build a string with the dimensions to be used when selecting a package
    'Private Function DimStrBuilder(ByVal i As Integer, ByRef rs As DataTable) As String
    '    'i is the index of the list to be used
    '    DimStrBuilder = rs.Rows(i).Item("Dim1").ToString & "," & rs.Rows(i).Item("Dim2").ToString & "," & _
    '    rs.Rows(i).Item("Dim3").ToString & "," & Convert.ToInt32(rs.Rows(i).Item("bDim2")).ToString & "," & _
    '    Convert.ToInt32(rs.Rows(i).Item("bDim3")).ToString
    'End Function

    'jrs 2013/05/09 moved to Function2
    'Private Function DimStrBuilder(ByVal i As Integer) As String
    '    'i is the index of the list to be used
    '    DimStrBuilder = """"""
    '    Dim j As Integer
    '    For j = 1 To MaxVal(i) - 1
    '        DimStrBuilder = DimStrBuilder & ",""" & GRPS(i, j).ToString & """"
    '    Next
    'End Function

    'jrs 2011/01/19
    'Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
    '    Page.ClientScript.RegisterForEventValidation(lstDim3.UniqueID, "")
    '    MyBase.Render(writer)
    'End Sub

    'jrs 2011/02/11
    Private Sub SaveDimIdxs()
        'bRefresh is true when the page will be refreshed immediately (without going to another page)
        Dim DimIdx As Integer, AuxIdx As Integer
        'First Dimension
        If VDim1(Session("SelIdx")) > 0 Then
            For DimIdx = 0 To lstDim.Items.Count - 1
                If lstDim.Items(DimIdx).Selected Then
                    'jrs 2023/06/15 Saved indices should be in AllDim format now
                    'If Session("bUseGroup") Or Session("bUseGroupDec") Then
                    '    AuxIdx = gGetGroupIdx(lstDim.Items(DimIdx).Text, False, Me)
                    'ElseIf Session("bUseGList") Then
                    '    AuxIdx = gGetGListIdx(lstDim.Items(DimIdx).Text, False, Me)
                    'ElseIf Session("bUseCtryAndGroup") And Session("AllDimsIdx")(DimIdx) + 1 > NR Then 'jrs 2017/06/30
                    '    AuxIdx = gGetGroupIdx(lstDim.Items(DimIdx).Text, False, Me) + NR
                    'ElseIf lstDim.Items(DimIdx).Text = GRPS(VDim1(Session("SelIdx")), Session("AllDimsIdx")(DimIdx) + 1) Then 'jrs 2012/12/14 added + 1
                    '    AuxIdx = Session("AllDimsIdx")(DimIdx) + 1
                    'Else
                    '    For AuxIdx = MAXCOM(VDim1(Session("SelIdx"))) - 1 To 1 Step -1
                    '        If lstDim.Items(DimIdx).Text = GRPS(VDim1(Session("SelIdx")), AuxIdx) Then
                    '            Exit For
                    '        End If
                    '    Next AuxIdx
                    'End If
                    AuxIdx = DimIdx + 1
                    If AuxIdx = 0 Then
                        AuxIdx = 1
                    End If
                    Session("DimIdxs")(AuxIdx - 1) = True
                    lstDim.Items(DimIdx).Selected = False
                    lstDim.Items(AuxIdx - 1).Selected = True
                    'jrs 2017/07/03
                    txtDirtyDim.Value = "Y"
                End If
            Next DimIdx
        End If
        'Second Dimension
        If VDim2(Session("SelIdx")) > 0 Then
            For DimIdx = 0 To lstDim2.Items.Count - 1
                If lstDim2.Items(DimIdx).Selected Then
                    'jrs 2023/06/15 Saved indices should be in AllDim format now
                    'If lstDim2.Items(DimIdx).Text = GRPS(VDim2(Session("SelIdx")), Session("AllDimsIdx")(DimIdx) + 1) Then 'jrs 2012/12/14 added + 1
                    '    AuxIdx = Session("AllDimsIdx")(DimIdx) + 1
                    'Else
                    '    For AuxIdx = MAXCOM(VDim2(Session("SelIdx"))) - 1 To 1 Step -1
                    '        If lstDim2.Items(DimIdx).Text = GRPS(VDim2(Session("SelIdx")), AuxIdx) Then
                    '            Exit For
                    '        End If
                    '    Next AuxIdx
                    'End If
                    AuxIdx = DimIdx + 1

                    If AuxIdx = 0 Then
                        AuxIdx = 1
                    End If
                    Session("DimIdxs2")(AuxIdx - 1) = True
                    lstDim2.Items(DimIdx).Selected = False
                    lstDim2.Items(AuxIdx - 1).Selected = True
                End If
            Next DimIdx
        End If
        'Third Dimension
        If VDim3(Session("SelIdx")) > 0 Then
            For DimIdx = 0 To lstDim2.Items.Count - 1
                If lstDim3.Items(DimIdx).Selected Then
                    'jrs 2023/06/15 Saved indices should be in AllDim format now
                    'If lstDim3.Items(DimIdx).Text = GRPS(VDim3(Session("SelIdx")), Session("AllDimsIdx")(DimIdx) + 1) Then 'jrs 2012/12/14 added + 1
                    '    AuxIdx = Session("AllDimsIdx")(DimIdx) + 1
                    'Else
                    '    For AuxIdx = MAXCOM(VDim3(Session("SelIdx"))) - 1 To 1 Step -1
                    '        If lstDim3.Items(DimIdx).Text = GRPS(VDim3(Session("SelIdx")), AuxIdx) Then
                    '            Exit For
                    '        End If
                    '    Next AuxIdx
                    'End If
                    AuxIdx = DimIdx + 1

                    If AuxIdx = 0 Then
                        AuxIdx = 1
                    End If
                    Session("DimIdxs3")(AuxIdx - 1) = True
                    lstDim3.Items(DimIdx).Selected = False
                    lstDim3.Items(AuxIdx - 1).Selected = True
                End If
            Next DimIdx
        End If
    End Sub

    'jrs 2011/05/03
    Protected Sub optReg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optReg.CheckedChanged
        If optReg.Checked Then
            Session("bUseReg") = True
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
            'jrs 2016/09/30
            Session("bUseCtryAndGroup") = False
            chkHideNonCoreGroups.Enabled = False 'cdv 2021/08/23
            chkViewHistoryForecasts.Enabled = True 'dsb 2025/08/11 questions check verify
            'jrs 2025/05/30
            Session("bUseCtryAndGroupDec") = False
        End If
    End Sub

    Protected Sub optGList_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGList.CheckedChanged
        If optGList.Checked Then
            Session("bUseReg") = False
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = True
            'jrs 2016/09/30
            Session("bUseCtryAndGroup") = False
            chkHideNonCoreGroups.Enabled = False 'cdv 2021/08/23
            chkViewHistoryForecasts.Enabled = True 'dsb 2025/08/11 questions check and verify
            'jrs 2025/05/30
            Session("bUseCtryAndGroupDec") = False
        End If
    End Sub


    Protected Sub optGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGroup.CheckedChanged
        If optGroup.Checked Then
            Session("bUseReg") = False
            Session("bUseGroup") = True
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
            'jrs 2016/09/30
            Session("bUseCtryAndGroup") = False
            chkHideNonCoreGroups.Enabled = True 'cdv 2021/08/23
            chkViewHistoryForecasts.Enabled = True 'dsb 2025/08/11 questions check and verify
            'jrs 2025/05/30
            Session("bUseCtryAndGroupDec") = False
        End If
    End Sub

    Protected Sub optGroupDec_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGroupDec.CheckedChanged
        If optGroupDec.Checked Then
            Session("bUseReg") = False
            Session("bUseGroup") = False
            Session("bUseGroupDec") = True
            Session("bUseGList") = False
            'jrs 2016/09/30
            Session("bUseCtryAndGroup") = False
            chkHideNonCoreGroups.Enabled = True 'cdv 2021/08/23
            chkViewHistoryForecasts.Enabled = True 'dsb 2025/08/11 Questions check and verify
            'jrs 2025/05/30
            Session("bUseCtryAndGroupDec") = False
        End If
    End Sub

    'Protected Sub chkIntercept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIntercept.CheckedChanged
    '    Session("bIntercept") = chkIntercept.Checked
    'End Sub

    Private Sub mnuPIe_Click()
        'jrs 2011/04/11
        ''jrs 2005/11/11
        'Session("oldLastYear") = Session("LastYear")
        'Session("oldFirstYear") = Session("FirstYear")

        LoadPackage()
        If Session("COUNTE") = 0 Then
            Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstPacks.SelectedValue

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GType") = "PIE"

        If Session("Interval") < 1 Then Session("Interval") = 1
        If Session("PYear") Is Nothing Then
            Session("PYear") = 1
        ElseIf Session("PYear") = 0 Then
            Session("PYear") = 1
        End If
        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")

    End Sub

    'jrs 2011/09/06
    Private Sub mnuScatterPlot_Click()
        Dim MinLen As Single
        Dim SD1 As String
        Dim VNP As String
        Dim SVN As String
        Dim D2 As String
        Dim d1 As String
        Dim VN As String
        Dim ii As Short
        Dim Msg As String

        LoadPackage()

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If

        'jrs 2011/04/07
        'moved out from if statement
        'jrs 2008/10/21
        Dim Vars As New Collection
        Dim Dim1s As New Collection
        Dim i As Integer
        Dim D As Object, v As Object
        'jrs 2010/10/19
        'allow using different scenarios
        Dim sVar As String, pos As Integer
        'Generates error when Item not found, then add it
        On Error Resume Next
        For i = 1 To Session("COUNTE")
            pos = InStr(Session("VARN")(1, i), "[")
            If pos > 0 Then
                sVar = Mid(Session("VARN")(1, i), 1, pos - 1)
            Else
                sVar = Session("VARN")(1, i)
            End If
            v = Nothing
            'jrs 2010/10/19 v = Vars.Item(VARN(1, i))
            v = Vars.Item(sVar)
            If (v Is Nothing) Then
                'jrs 2010/10/19 Vars.Add i, VARN(1, i)
                Vars.Add(i, sVar)
            End If

            D = Nothing
            D = Dim1s.Item(Session("GR1N")(i))
            If (D Is Nothing) Then
                Dim1s.Add(i, Session("GR1N")(i))
            End If
        Next
        'Back to original handler
        On Error GoTo 0
        Session("VarsCount") = Vars.Count
        'jrs 2012/06/01
        Session("Dim1Count") = Dim1s.Count
        'jrs 2012/07/23
        If Dim1s.Count = NR Then
            Session.Remove("IsaGlist")
            Session.Remove("IsaGroup")
        End If

        Session("TableTitle") = ""

        '   Divert to statistics form if more than two variables selected
        '   -------------------------------------------------------------

        If Vars.Count <= 3 Then 'Must alter to check for regionalized variables
            'Have candidate for scattergram; check it out

            If Vars.Count * Dim1s.Count > Session("COUNTE") Or
                (Vars.Count = 1 And Dim1s.Count = Session("COUNTE")) Or
                Vars.Count > 3 Or
                Session("COUNTE") Mod Dim1s.Count <> 0 Then 'jrs 2010/11/04 added COUNTE MOD DIM1S.COUNT <> 0
                'msg = "To display a motion chart you must select 2 or 3 Variables across the same set of Regions:  dependent, independent, and bubble sizing (optional)."
                'lblMsg.text = msg
                'lblMsg.Visible = True
                Call ScatterError()
                Exit Sub
            End If

            Dim jj As Integer
            For jj = 1 To Vars.Count
                SVN$ = LTrim$(RTrim$(Session("VARN")(1, (jj - 1) * Dim1s.Count + 1)))
                'jrs 2010/11/08
                pos = InStr(SVN$, "[")
                If pos > 0 Then
                    SVN$ = Mid(SVN$, 1, pos - 1)
                End If

                For ii = 1 To Dim1s.Count 'jrs 2010/09/14 COUNTE / 2

                    VN$ = LTrim$(RTrim$(Session("VARN")(1, (jj - 1) * Dim1s.Count + ii)))
                    d1$ = LTrim$(RTrim$(Session("GR1N")((jj - 1) * Dim1s.Count + ii)))
                    D2$ = LTrim$(RTrim$(Session("GR2N")((jj - 1) * Dim1s.Count + ii)))
                    'jrs 2010/11/08
                    pos = InStr(VN$, "[")
                    If pos > 0 Then
                        VN$ = Mid(VN$, 1, pos - 1)
                    End If

                    If VN$ <> SVN$ Then 'Is the first set complete/consistent?
                        Call ScatterError()
                        Exit Sub
                    End If


                Next ii
            Next jj

            'jrs 2013/04/18
            'Check if the order is fine: Sce1 for all countries and then Sce2 for all countries
            'Flex Displays generates Country1 for all Sces, and then Country2 for all Sces
            If Vars.Count = 1 Then
                If Session("GR1N")(1) = Session("GR1N")(2) Then
                    'Reorder to have all countries in Sce1 first
                    ReOrderForScatterPlot(Dim1s.Count)
                    Session("VarsCount") = 2
                End If
            End If

            Session("bBubble") = (Vars.Count > 2)
            Session("BDCheck") = Nothing

            'ScatGraphStyl = 0
            'ScatGraphTyp = 9
            'Session("GraphStyle") = ScatGraphStyl
            'Session("GraphType") = ScatGraphTyp
            Session("GType") = "SCAT"
            Session("PYear") = FirstYearForecast - Session("FirstYear") + 1
            If Session("PYear") < 1 Then Session("PYear") = 1
            If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
            'jrs 2008/07/01
            If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

            Support.FormManager.StateStore(Me)
            'jrs 2011/09/05 Support.FormManager.Show(Me, "frm_ProEssential.aspx")
            Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
            'frm_ProEssential.DefInstance.Show() 
        Else 'Divert to statistics form

            If Session("COUNTE") Mod NR = 0 Then 'only if Session("COUNTE") is even multiplier of NR%
                Session("Statistics_Type") = 0
                Session("PYear") = FirstYearForecast - Session("FirstYear") + 1
                If Session("PYear") < 1 Then Session("PYear") = 1
                If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
                'jrs 2008/07/01
                If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)
                SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
                        Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
                        Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
                        Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
                        Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
                        Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
                        Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))

                'Hide() 
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "Statistics.aspx")
                'frm_statistics.DefInstance.Show() 
            Else
                Msg = "You must pick two or more variables across all regions for a scattergram."
                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
                lblMsg.Text = Msg
                lblMsg.Visible = True
                Exit Sub
            End If
        End If

    End Sub

    Private Sub ScatterError()
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Msg = "For a scattergram you must select all instances "
        Msg = Msg & "of the first variable before selecting the "
        Msg = Msg & "identical instances of the second variable.  "
        Msg = Msg & "Moreover, the sequence of instances "
        Msg = Msg & "for the second variable must be "
        Msg = Msg & "identical to the sequence for the first variable.  "
        Msg = Msg & "E.g. GDP(US), GDP(EU), GDP(JAPAN), POP(US), "
        Msg = Msg & "POP(EU), POP(JAPAN)"
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
        lblMsg.Text = Msg
        lblMsg.Visible = True

    End Sub


    'jrs 2008/08/12
    Private Sub LoadMovingChart()
        LoadPackage()

        Dim Msg As String

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'jrs 2008/10/21
        Dim Vars As New Collection
        Dim Dim1s As New Collection
        Dim i As Integer
        For i = 1 To Session("COUNTE")
            If Not Vars.Contains(Session("VARN")(1, i)) Then
                Vars.Add(i, Session("VARN")(1, i))
            End If
            If Not Dim1s.Contains(Session("GR1N")(i)) Then
                Dim1s.Add(i, Session("GR1N")(i))
            End If
        Next

        If Vars.Count * Dim1s.Count > Session("COUNTE") Or
            (Vars.Count = 1 And Dim1s.Count = Session("COUNTE")) Or
            Vars.Count > 3 Then
            'Msg = "You must select 2 or 3 Variables across the same set of Regions"
            Msg = "To display a motion chart you must select 2 or 3 Variables across the same set of Regions:  dependent, independent, and bubble sizing (optional)."
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        If Session("Interval") = Nothing Then Session("Interval") = 1

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_MotionChart.aspx")
    End Sub

    Protected Sub lstFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        'jrs 2011/11/04
        Session("txtFiles") = txtFiles.Value
    End Sub

    'jrs 2012/03/09
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

    Protected Sub lstRunHorizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRunHorizon.SelectedIndexChanged
        'jrs 2012/04/12
        If lstRunHorizon.SelectedValue > "" Then
            Session("DisplayLastYear") = CInt(lstRunHorizon.SelectedValue)
        End If

    End Sub

    Protected Sub lstDim_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDim.SelectedIndexChanged
        'jrs 2012/12/05
        Session("txtDims") = txtDims.Value
    End Sub

    'jrs 2013/04/18
    Private Sub ReOrderForScatterPlot(ByVal DimCount As Integer)
        'Copy source
        Dim pCopy As Object, VARNCopy As Object, gr1Copy As Object, gr2Copy As Object,
            gr3Copy As Object, gr4Copy As Object, fCopy As Object,
            FileNumbrCopy As Object, FinYearCopy As Object, FinYearHisCopy As Object,
            bNotDecomposablesCopy As Object, bLineFeedsCopy As Object,
            sGroupTitlesCopy As Object, sUsrDefCatsCopy As Object, DispDecimalsCopy As Object
        'splot and focus
        Dim FDNG1Copy As Object, FDNG2Copy As Object, FDNG3Copy As Object, FDNG4Copy As Object

        pCopy = VB6.Support.CopyArray(Session("PLOT"))
        VARNCopy = VB6.Support.CopyArray(Session("VARN"))
        gr1Copy = VB6.Support.CopyArray(Session("GR1N"))
        gr2Copy = VB6.Support.CopyArray(Session("GR2N"))
        gr3Copy = VB6.Support.CopyArray(Session("GR3N"))
        gr4Copy = VB6.Support.CopyArray(Session("GR4N"))
        fCopy = VB6.Support.CopyArray(Session("aFormulas"))

        FDNG1Copy = VB6.Support.CopyArray(Session("FDNG1"))
        FDNG2Copy = VB6.Support.CopyArray(Session("FDNG2"))
        FDNG3Copy = VB6.Support.CopyArray(Session("FDNG3"))
        FDNG4Copy = VB6.Support.CopyArray(Session("FDNG4"))

        FileNumbrCopy = VB6.CopyArray(Session("FileNumbr"))
        FinYearCopy = VB6.CopyArray(Session("FinYear"))
        FinYearHisCopy = VB6.CopyArray(Session("FinYearHis"))

        'Reset Target
        Dim j As Integer, k As Integer
        Dim focus As Integer
        For i = 1 To Session("COUNTE")
            'source index
            If i <= Session("COUNTE") / 2 Then
                k = 2 * (i - 1) + 1
            Else
                k = 2 * (i - DimCount - 1) + 2
            End If
            'copy to target
            For j = 0 To UBound(Session("PLOT"), 1)
                Session("PLOT")(j, i) = pCopy(j, k)
            Next j

            For j = 0 To UBound(Session("VARN"), 1)
                Session("VARN")(j, i) = VARNCopy(j, k)
            Next j
            Session("GR1N")(i) = gr1Copy(k)
            Session("GR2N")(i) = gr2Copy(k)
            Session("GR3N")(i) = gr3Copy(k)
            Session("GR4N")(i) = gr4Copy(k)
            If k <= UBound(fCopy) Then
                If i > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(i)
                End If
                Session("aFormulas")(i) = fCopy(k)
            End If
            Session("FDNG1")(i) = FDNG1Copy(k)
            Session("FDNG2")(i) = FDNG2Copy(k)
            Session("FDNG3")(i) = FDNG3Copy(k)
            Session("FDNG4")(i) = FDNG4Copy(k)
            Session("FileNumbr")(i) = FileNumbrCopy(k)
            Session("FinYear")(i) = FinYearCopy(k)
            Session("FinYearHis")(i) = FinYearHisCopy(k)
        Next i

    End Sub

    'jrs 2016/09/30
    Protected Sub optCtryAndGrp_CheckedChanged(sender As Object, e As EventArgs) Handles optCtryAndGrp.CheckedChanged
        If optCtryAndGrp.Checked Then
            Session("bUseReg") = False
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
            Session("bUseCtryAndGroup") = True
            'jrs 2025/05/30
            Session("bUseCtryAndGroupDec") = False
            chkHideNonCoreGroups.Enabled = True
            chkViewHistoryForecasts.Enabled = True 'dsb 2025/08/11
        End If
    End Sub

    'jrs 2025/05/30
    Protected Sub optCtryAndGrpDec_CheckedChanged(sender As Object, e As EventArgs) Handles optCtryAndGrpDec.CheckedChanged
        If optCtryAndGrpDec.Checked Then
            Session("bUseReg") = False
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
            Session("bUseCtryAndGroup") = False
            Session("bUseCtryAndGroupDec") = True
            chkHideNonCoreGroups.Enabled = True
            chkViewHistoryForecasts.Enabled = True 'dsb 2025/08/11
        End If
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '           JAM 2017-06-11     Adding Stacked Bar Graph Option
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuStackedBar_Click()

        'jrs 2016/01/04
        'Turn off regression
        If Not (Session("RegressType") Is Nothing) Then
            If Session("RegressType") > "" Then
                Session("RegressType") = ""
                Session("TableTitle") = ""
                Session("mnuLogistic") = False
            End If
        End If

        LoadPackage()
        If Session("COUNTE") = 0 Then
            Exit Sub
        End If

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If


        On Error GoTo DefaultInterval
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("GType") = "BAR"
        Session("PatternSel") = Nothing 'JAM 2017-08-14
        Support.FormManager.Show(Me, "frm_StackedBar.aspx")

        Exit Sub

DefaultInterval:
        Session("Interval") = 1
        Resume Next


    End Sub

    'jrs 2018/05/17
    Private Sub mnuStackedArea_Click()

        If Not (Session("RegressType") Is Nothing) Then
            If Session("RegressType") > "" Then
                Session("RegressType") = ""
                Session("TableTitle") = ""
                Session("mnuLogistic") = False
            End If
        End If

        LoadPackage()
        If Session("COUNTE") = 0 Then
            Exit Sub
        End If

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GType") = "STACKEDAREA"

        Dim TargetUrl As String
        TargetUrl = "frm_GraphicalDisplay.aspx"
        If txtListNames.Value > "" Then
            TargetUrl = TargetUrl & "?ListNames=" & PrepareForURL(txtListNames.Value)
            TargetUrl = TargetUrl & "&HistFor=" & txtHistFor.Value
            'jrs 2011/04/11
            TargetUrl = TargetUrl & "&GrpOp="
            Select Case True
                Case Session("bUseGroup") 'Groups 1
                    TargetUrl = TargetUrl & "1"
                Case Session("bUseGroupDec") 'Dec Groups 2
                    TargetUrl = TargetUrl & "2"
                Case Session("bUseGList") 'G-List 3
                    TargetUrl = TargetUrl & "3"
                Case Session("bUseCtryAndGroup") 'Countries and Groups 4
                    TargetUrl = TargetUrl & "4"
                Case Session("bUseCtryAndGroupDec") 'Countries and Dec Groups 5
                    TargetUrl = TargetUrl & "5"
                Case Else 'Regions 0
                    TargetUrl = TargetUrl & "0"
            End Select
            'parameter for dimensions
            'jrs 2013/10/08
            Session("Dim1sLoaded") = DimLstToURL(lstDim, 1)
            TargetUrl = TargetUrl & Session("Dim1sLoaded")
            'jrs 2013/10/08
            Session("Dim1sLoaded") = Replace(Session("Dim1sLoaded"), "&Dim1=", "")
            TargetUrl = TargetUrl & DimLstToURL(lstDim2, 2)
            TargetUrl = TargetUrl & DimLstToURL(lstDim3, 3)
            TargetUrl = TargetUrl & FileLstToURL(lstFiles, txtFiles.Value)
            'jrs 2013/01/18
            TargetUrl = TargetUrl & "&DisplayLastYear=" & Session("DisplayLastYear")
        End If

        Support.FormManager.Show(Me, TargetUrl)


    End Sub

    Private Sub cboFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFilter.SelectedIndexChanged
        Session("cboFilterIdx") = cboFilter.SelectedIndex
        'Session("PDFlexTopCategoriesIdxs") = Nothing 'dsb 2025/08/06
        'jrs 2020/12/03 reset categories
        Session("PDFlexCategoriesIdxs") = Nothing
        Session("CategoryIds") = Nothing
        Session("PrevCat") = lstCategories.SelectedValue
        'jrs 2021/10/20
        Dim pos As Integer
        pos = InStr(cboFilter.SelectedValue, "-")
        If pos > 0 Then
            Session("DefaultFlexDisplayFilterNew") = Mid(cboFilter.SelectedValue, pos + 2)
        End If
        'jrs 2024/06/24
        LoadCategories()
        Session("PrevCat") = Nothing
        PackJScritpBuilder()
    End Sub
    'cdv 2021/09/01 ****START****
    Private Sub chkHideNonCoreGroups_CheckedChanged(sender As Object, e As EventArgs) Handles chkHideNonCoreGroups.CheckedChanged
        If Session("bCheckHideNonCoreGroups") = 0 Then
            Session("bCheckHideNonCoreGroups") = 1
        Else
            Session("bCheckHideNonCoreGroups") = 0
        End If
    End Sub
    'cdv 2021/09/01 ****END****

    'dsb 2025/08/11
    Private Sub chkViewHistoryForecasts_CheckedChanged(sender As Object, e As EventArgs) Handles chkViewHistoryForecasts.CheckedChanged
        If Session("bCheckViewHistoryForecasts") = 0 Then
            Session("bCheckViewHistoryForecasts") = 1
        Else
            Session("bCheckViewHistoryForecasts") = 0
        End If
        Call lstPacks_SelectedIndexChanged(Me, New EventArgs)
    End Sub

    'jrs 2023/06/14
    Private Sub mnuGroupAgg_Click()
        Dim sVar As String, SEL As Integer, DoingHistory As Boolean
        sVar = rs.Rows(0).Item("VarName")
        SEL = IfsVars.Item(sVar).Index
        Session("SEL") = SEL
        DoingHistory = rs.Rows(0).Item("History")
        Session("DoingHistory") = DoingHistory
        Session("sVar") = sVar

        If sVar = "" And (SEL = 0 Or Not DoingHistory) Then

            'jrs 2023/12/08
            'Dim sMsg = "<script>alert('Please select a variable first.');</script>"
            'Response.Write(sMsg)

            LoadMessage("Please select a variable first.", Session("Message"))
            'Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'jrs 2023/12/08
        If SEL = -1 And Not DoingHistory Then
            'Dim sMsg = "<script>alert('This option is not implemented for Formulas.');</script>"
            'Response.Write(sMsg)

            LoadMessage("This option is not implemented for Formulas.", Session("Message"))
            'Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'jrs 2011/01/18 changed form
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ForecastGroupAggregation.aspx")

    End Sub

    'jrs 2023/09/20
    Private Sub lstDim2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDim2.SelectedIndexChanged
        Session("txtDims2") = txtDims2.Value
    End Sub

    'jrs 2024/12/02
    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        SavePackage()
    End Sub

    Private Sub chkHideNonCoreGroups_Unload(sender As Object, e As EventArgs) Handles chkHideNonCoreGroups.Unload

    End Sub

    Private Sub chkHideNonCoreGroups_PreRender(sender As Object, e As EventArgs) Handles chkHideNonCoreGroups.PreRender

    End Sub

    Private Sub chkHideNonCoreGroups_Load(sender As Object, e As EventArgs) Handles chkHideNonCoreGroups.Load

    End Sub

    Private Sub chkHideNonCoreGroups_Init(sender As Object, e As EventArgs) Handles chkHideNonCoreGroups.Init

    End Sub

    Private Sub chkHideNonCoreGroups_Disposed(sender As Object, e As EventArgs) Handles chkHideNonCoreGroups.Disposed

    End Sub

    Private Sub chkHideNonCoreGroups_DataBinding(sender As Object, e As EventArgs) Handles chkHideNonCoreGroups.DataBinding

    End Sub

    Private Sub lstPacks_Load(sender As Object, e As EventArgs) Handles lstPacks.Load

    End Sub

    Private Sub lstCategories_Load(sender As Object, e As EventArgs) Handles lstCategories.Load

    End Sub
End Class

