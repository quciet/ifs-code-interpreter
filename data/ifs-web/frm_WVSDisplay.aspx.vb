Option Strict Off
Option Explicit On
'Mohammod .Net Win Forms Feb (3) 2004
Imports IFs.Data
'jam 2021/07/18
Imports Microsoft.AspNet.FriendlyUrls

'Namespace IFsWeb

Partial Class frm_WVSDisplay
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer.
        'Do not modify it using the code editor.
        'CONVERSION_NOTE:  'Code' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
        'If m_vb6FormDefInstance Is Nothing Then 
        'If m_InitializingDefInstance Then 
        'm_vb6FormDefInstance = Me
        'Else 
        'Try 
        ''For the start-up form, the first instance created is the default instance.
        'If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then 
        'm_vb6FormDefInstance = Me
        'End If 
        'Catch 
        'End Try 
        'End If 
        'End If  
        InitializeComponent()

        'jrs 2011/05/04
        If Session("GraphType") = Nothing Then
            Session("GraphType") = 3 'BarGraphTyp
        End If
        If Session("GType") = Nothing Then
            Session("GType") = "COHORT"
        End If
        If Session("PYear") = Nothing Then
            Session("PYear") = 1
        End If
        If Session("TableTitle") = Nothing Then
            Session("TableTitle") = "" 'txt_TableTitle
        End If


        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim iPopSel As Object
            Dim iGDPSel As Object
            Dim FileNum As Object
            '------------------------------------------------------------


            'mti Validation changes 19 August, 2002
            Session("WVS_sInterval") = Session("Interval")
            'mti Validation changes 19 August, 2002

            'From ChartHist
            '--------------------------------

            '   Set form and graph dimensions
            '   -----------------------------

            Session("WVS_pCOUNTE") = Session("COUNTE")


            'JRS 04-17-01 BEGIN
            Session("WVS_CommonPlot") = "False"
            'JRS 04-17-01 END

            'jrs 2011/05/06
            If Session("WVS_ bGroups") = True Then
                LoadGroups()
            Else
                'JRS 10/03/02 WVS
                LoadRegions()
            End If
            LoadVariables()
            'JRS 10/15/02
            'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'jrs 2026/01/26 FileNum = 0
            FileNum = 1

            'LoadFiles_Org()

            'JRS 12/01/02 FILES
            LoadFiles(gRetrieveFiles(Session("PutFdrvRun2"), Session("WorkingFile"), Session("RunFiles"), Session("FirstYear"), Session("FilesSelected"), Session("ShortSce"), Session("IFsRunFiles")), cboFiles)
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If cboFiles.Items.Count > 0 Then
                'jrs 2026/01/26 changed the default to base
                cboFiles.SelectedIndex = 1
                'jrs 2006/11/15
                'jrs 2011/09/22 cboFiles_SelectedIndexChanged(Me, New EventArgs)
            End If

            'jrs 2011/05/06
            If Session("WVS_ bGroups") = Nothing Then
                Session("WVS_ bGroups") = False
                Session("IsGroup") = False
            End If

            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If cmb_Country.Items.Count > 0 Then
                cmb_Country.SelectedIndex = 0 ' Display First Item
            End If
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If cmbVariable.Items.Count > 0 Then
                'jrs 2011/09/22 cmbVariable.SelectedIndex = 0 ' Display First Item
                If V(Session("SEL")) = cmbVariable.Items(1).Text Then
                    cmbVariable.SelectedIndex = 1
                ElseIf V(Session("SEL")) = cmbVariable.Items(2).Text Then
                    cmbVariable.SelectedIndex = 2
                Else
                    cmbVariable.SelectedIndex = 0
                End If
            End If

            'LoadVarPlot((Support.ComboboxManager.getText(cmb_Country)), (Support.ComboboxManager.getText(cmbVariable)))
            LoadVarPlot(cmb_Country.SelectedValue(), cmbVariable.SelectedValue())

            'Setting pointers to calculate aggregate values
            iGDPSel = 0
            iPopSel = 0
            Dim x As Short
            For x = 1 To NumbVars
                Select Case V(x)
                    Case "GDP"
                        iGDPSel = x
                    Case "POP"
                        iPopSel = x
                End Select
                If iGDPSel > 0 And iPopSel > 0 Then Exit For
            Next x
            'jrs 2005/03/14

        End If

        Dim fNum As Integer
        Dim finam As String
        'cdv 2021/03/11 System to get the last year based off needing to read a .run.db or .run
        If Session("mnuSQLite") = "True" Then
            finam = Session("PutFdrvRun") & "Working.run.db"
            Dim working As IStorage
            Dim storageSvc As IStorageService = New StorageService(finam)
            working = storageSvc.Open()
            Session("LastYear") = working.LastYear
        Else
            Dim RECORD As Object
            Dim Number As Single
            finam = Session("PutFdrvRun") & "Working.run"
            fNum = FreeFile()
            FileOpen(fNum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            RECORD = VarPoint(PosModYear) * 4 - 3
            FileGet(fNum, Number, RECORD)
            Session("LastYear") = Number
            FileClose(fNum)
        End If
        'jrs 2005/03/14
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

    'Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    'If Disposing Then 
    'If Not components Is Nothing Then 
    'components.Dispose()
    'End If 
    'End If 
    'MyBase.Dispose(Disposing)
    'End Sub  
    'CONVERSION_NOTE: Field 'components' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Private components As System.ComponentModel.IContainer 
    'CONVERSION_NOTE: Class 'System.Windows.Forms.ToolTip' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public ToolTip1 As System.Windows.Forms.ToolTip 
    'CONVERSION_NOTE: Class 'AxMSComDlg.AxCommonDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents cmd_ScriptFile As AxMSComDlg.AxCommonDialog 
    Public mnuContinue As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuSave As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPrint As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuCommon As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuAdvance As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuRegress As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuStatistics As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuGroups As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public AboutWVS As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'CONVERSION_NOTE: Class 'Gigasoft.ProEssentials.Pego' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Friend WithEvents PegoNet1 As Gigasoft.ProEssentials.Pego 
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_ProEssentialWVSPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_ProEssentialWVS
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_ProEssentialWVS()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    '
    '------------------------------------------LineDisp.Frm------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last revision by BBH on July 17, 2002 - added MTI's validation code


    'mti Validation changes 19 August, 2002
    'Dim sInterval As Short

    'Dim CommonPlot As String
    'mti Validation changes
    'Dim pCOUNTE As Short
    Dim VH(100) As Object
    Dim VF(100) As Object

    'new for web form -brought from VarSel- check if this is right
    'Dim FlagGroup As Boolean
    'Public DimNum As Short
    'Dim SDimNum As Short
    'Dim GroupOn As Boolean
    'mti Validation changes-replace all pCOUNTE with pCOUNTE for this module
    'JRS 10/05/02 WVS
    'Dim bGroups As Boolean





    ''CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'Public Sub AboutWVS_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AboutWVS.Popup
    '    AboutWVS_Click(eventSender, eventArgs)
    'End Sub
    Public Sub AboutWVS_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.

        'jam 2021/07/18
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'mti August 2004 5
        Session("AboutParent") = "frm_WVSDisplay.aspx"
        'mti August 2004 5 -end 

        'jam 2021/07/18
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "AboutModel.aspx")

        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/AboutModel.aspx")
        vurl = FriendlyUrl.Href("~/AboutModel")
        Response.Redirect(vurl)


        'frm_AboutModel.Show() )
        'frm_AboutWVS.DefInstance.Show() 
    End Sub

    'UPGRADE_WARNING: Event cboFiles.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cboFiles_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFiles.SelectedIndexChanged
        Dim FinalYear As Short
        Dim Number As Single
        Dim finam As String
        Dim FileNum As Object
        Dim DefaultFile As Object
        'JRS 3/19/02 DISPLAY
        'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        DefaultFile = cboFiles.SelectedIndex
        'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        FileNum = DefaultFile
        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If FileNum > 0 Then
            finam = Session("PutFdrvRun") & Mid(cboFiles.SelectedValue, InStr(cboFiles.SelectedValue, "-") + 2, Len(cboFiles.SelectedValue) - 1 - InStr(cboFiles.SelectedValue, "-"))
        Else
            finam = Session("PutFdrvRun") & "Working.run.db"
        End If
        'jrs 2005/02/14
        'Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
        ''RECORD = VarPoint(PosModYear)
        ''RECORD = RECORD * 4 - 3
        ''UPGRADE_WARNING: Couldn't resolve default property of object PosModYear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Couldn't resolve default property of object VarPoint(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Couldn't resolve default property of object RECORD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Session("RECORD") = VarPoint(PosModYear) * 4 - 3
        ''UPGRADE_WARNING: Couldn't resolve default property of object RECORD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'FileGet(fNum, Number, Session("RECORD"))
        'FinalYear = Number
        'FileClose(fNum)
        Dim working As IStorage
        Dim storageSvc As IStorageService = New StorageService(finam)
        working = storageSvc.Open()
        FinalYear = working.LastYear

        If InStr(StrConv(finam, VbStrConv.Uppercase), "IFSBASEHIST.RUN") > 0 Then
            'In case of History file change First Year
            Session("FirstYear") = FirstYearHistory
            Session("LastYear") = FinalYear 'jrs 2005/03/14 LastYearHistory
        Else
            Session("FirstYear") = FirstYearForecast
            Session("LastYear") = FinalYear 'jrs 2005/03/14 LastYearForecast
        End If


        cmb_Country_SelectedIndexChanged(cmb_Country, New System.EventArgs)

    End Sub

    'UPGRADE_WARNING: Event cmb_Country.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    'UPGRADE_WARNING: ComboBox event cmb_Country.Change was upgraded to cmb_Country.TextChanged which has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2074"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ComboBox.TextChanged' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.TextChanged.html'.
    'Private Sub cmb_Country_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_Country.TextChanged
    '    'JRS 10/05/02 WVS
    '    If Support.ComboboxManager.getText(cmb_Country) > "" And Support.ComboboxManager.getText(cmbVariable) > "" Then
    '        LoadVarPlot((Support.ComboboxManager.getText(cmb_Country)), (Support.ComboboxManager.getText(cmbVariable)))
    '        Call DisplayGraph()
    '    End If
    'End Sub

    'UPGRADE_WARNING: Event cmb_Country.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cmb_Country_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
        'JRS 10/05/02 WVS
        'If Support.ComboboxManager.getText(cmb_Country) > "" And Support.ComboboxManager.getText(cmbVariable) > "" Then
        If cmb_Country.SelectedValue() <> "" And cmbVariable.SelectedValue() <> "" Then
            'LoadVarPlot((Support.ComboboxManager.getText(cmb_Country)), (Support.ComboboxManager.getText(cmbVariable)))
            LoadVarPlot(cmb_Country.SelectedValue(), cmbVariable.SelectedValue())
            'Call DisplayGraph()
        End If
    End Sub


    Private Sub cmbVariable_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbVariable.SelectedIndexChanged
        'JRS 10/05/02 WVS
        'If Support.ComboboxManager.getText(cmb_Country) > "" And Support.ComboboxManager.getText(cmbVariable) > "" Then
        If cmb_Country.SelectedValue() <> "" And cmbVariable.SelectedValue() <> "" Then
            'LoadVarPlot((Support.ComboboxManager.getText(cmb_Country)), (Support.ComboboxManager.getText(cmbVariable)))
            LoadVarPlot(cmb_Country.SelectedValue(), cmbVariable.SelectedValue())
            'Call DisplayGraph()
        End If
    End Sub

    'jrs 2008/09/08
    Dim sDefGrp As String
    Dim sDefReg As String

    '==========================frm_ProEssentialWVS===================
    'CONVERSION_NOTE: The code of Event 'frm_ProEssentialWVS.frm_ProEssentialWVS_Load' was moved to Method 'frm_ProEssentialWVS.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_WVSDisplay_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'jam 2021/07/18
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim othYear As String
        Dim othFile As String
        Dim qtyPAram As Integer



        'jrs 2008/09/08
        sDefGrp = "World" 'jrs 2015/10/21 "UNESCO S&W Asia"
        sDefReg = "Afghanistan"

        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        'jam 2021/07/18
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"


        'jam 2021/07/8
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")
        cswmWebMenu.ScrollEnabled = True
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"


        'jam 2021/07/18
        If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Request.QueryString("Group")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                othCountry = ""

            End If




            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            'jam 2021/07/19
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_WVSDisplay.aspx")


            If othCountry <> "" Then
                'jam 2021/07/19
                vurl = FriendlyUrl.Href("~/frm_WVSDisplay", WSPrepareForFriendlyURL(othCountry))

                Response.Redirect(vurl)


            End If


        End If





        Dim cswmGroup1 As Coalesys.WebMenu.Group

        Dim cswmItem As Coalesys.WebMenu.Item


        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_f0c4"
        'jam 2021/07/18
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_MainMenu.aspx"
        'cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        'jam 2021/007/20
        'cswmGroup1.OnMenuBarClick = "location.href='" + Page.ResolveUrl("~/frm_MainMenu.aspx") + "';"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"



        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Save"
        'cswmGroup1.ID = "Group_6cf5"

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Print"
        'cswmGroup1.ID = "Group_e926"
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Time"
        cswmGroup1.ID = "Group_Time"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Advance"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regress"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Set Group or Country"
        cswmGroup1.ID = "Group_c57e"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Groups"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'cswmItem.IconImage = "blank.gif"

        If Session("WVS_ bGroups") = True Then
            'jam 2021/07/18
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/07/18
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Countries"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"


        If Session("WVS_ bGroups") = True Then
            'jam 2021/07/18
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Else
            'jam 2021/07/18
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        End If
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "About WVS"
        cswmGroup1.ID = "Group_d9ea"
        Session("AboutParent") = "frm_WVSDisplay.aspx"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "AboutModel.aspx"
        'jrs 2025/02/04 cswmGroup1.OnMenuBarClick = "location.href='AboutModel.aspx';"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "About WVS"

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 20213/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16006);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler


        'jam 2021/07/18
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String

        If segments.Count = 1 Then

            vCountry = segments(0)

        ElseIf segments.Count > 1 Then 'jrs 2025/02/04
            vCountry = segments(0)
            Session("vCountry") = vCountry

        End If

        'jrs 2025/02/04
        If segments.Count > 0 Then
            Session("vCountry") = vCountry
        Else 'jrs 2025/02/05
            vCountry = Session("vCountry")
        End If

        'jam 2021/07/18
        If Session("IsGroup") = Nothing Then

            Session("IsGroup") = False

        End If





        If Not Page.IsPostBack Then
            ReDim Session("WVS_MaxValC")(MaxVariables), Session("WVS_MinValC")(MaxVariables), Session("WVS_diffvalc")(MaxVariables) 'Used in Common Display
            Session("WVS_CommonPlot") = "False"
        End If
        'jam 2021/07/18
        'jrs 2011/05/06
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(vCountry)) And Not Session("IsGroup") Then
            'jam 2021/07/18
            'jrs 2011/09/22
            'cswmWebMenu.Groups("Group_d9ea").OnMenuBarClick = "location.href='AboutModel.aspx?Country=" & PrepareForURL(Request.QueryString("Country")) & "';"

            cswmWebMenu.Groups("Group_d9ea").OnMenuBarClick = "location.href='AboutModel.aspx?Country=" & PrepareForURL(vCountry) & "';"
            'jam 2021/07/18
            If Session("MMCountry") <> vCountry Or Not Page.IsPostBack Then
                'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
                'jam 2021/07/18
                'Session("MMCountry") = Request.QueryString("Country")

                Session("MMCountry") = vCountry

                'jrs 2012/08/15 Session("WVS_ bGroups") = False
                If Session("WVS_ bGroups") = True Then
                    'jam 2021/07/18
                    cswmWebMenu.Groups("Set Group or Country").Items("Countries").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    cswmWebMenu.Groups("Set Group or Country").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions()
                    Session("WVS_ bGroups") = False
                End If

                'Validate
                Dim rs As DataTable
                SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT NewRegion FROM [Country Translation] as c, " &
                Regionalization & " as r where c.Country = r.Country AND [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs)
                If rs.Rows.Count = 0 Then
                    Session("MMCountry") = ""
                    rs = Nothing
                Else
                    Session("cNumber") = rs.Rows(0).Item("NewRegion")
                End If
            End If
            'jam 2021/07/18
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(vCountry)) And Session("IsGroup") Then
            'jrs 2011/09/22
            'cswmWebMenu.Groups("Group_d9ea").OnMenuBarClick = "location.href='AboutModel.aspx?Group=" & PrepareForURL(Request.QueryString("Group")) & "';"
            'jam 2021/07/18
            cswmWebMenu.Groups("Group_d9ea").OnMenuBarClick = "location.href='AboutModel.aspx?Group=" & PrepareForURL(vCountry) & "';"

            'jam 2021/07/18
            'If cmb_Country.SelectedValue <> Request.QueryString("Group") Then
            If cmb_Country.SelectedValue <> vCountry Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/07/18
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(vCountry, False, Me)
                If gIdx > 0 Then
                    cmb_Country.SelectedIndex = gIdx - 1
                    'jam 2021/07/18
                    'Session("MMGroup") = Request.QueryString("Group")
                    Session("MMGroup") = vCountry
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("WVS_ bGroups") = True Then
                    cmb_Country_SelectedIndexChanged(cmb_Country, New System.EventArgs)
                Else
                    mnuGroups_Click(cswmWebMenu, New Coalesys.WebMenu.ServerItemClickEventArgs(cswmWebMenu.Groups("Group_c57e").Items(0)))
                End If
            End If
        End If

        'KIA 63008 ADD THE FOLLOWING IF BLOCK
        If Not Session("cNumber") Is Nothing Then
            cmb_Country.SelectedIndex = CInt(Session("cNumber")) - 1
            Session.Remove("cNumber")
            cmb_Country_SelectedIndexChanged(Me, New System.EventArgs)
        Else
            PackJScritpBuilder()
        End If

    End Sub



    '=============================LineDisp.Frm  =======================
    Private Function GraphScaleLabel(ByRef V As String, ByRef Rescaler As Object) As String
        Dim GSLabel As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If Rescaler = 0 Then
            GSLabel = ""
        Else
            GSLabel = "10^" & LTrim(Str(Rescaler))
        End If

        GraphScaleLabel = GSLabel

    End Function

    '=============================frm_ProEssentialWVS.frm  =======================


    Public Sub mnuAdvance_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("PYear") += 1
        'jrs 2026/04/27 added Session("FirstYear") +
        'jrs 2025/02/05
        While (Session("FirstYear") + Session("PYear") - 1) Mod 5 <> 0
            Session("PYear") = Session("PYear") + 1
        End While
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        PackJScritpBuilder()
        'Call DisplayGraph()

        'Pego1.PEactions = 2 'resetimage
        'PegoNet1.PeFunction.Reinitialize()

    End Sub


    '==========================frm_ProEssentialWVS===================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'Public Sub mnuContinue_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuContinue.Popup
    '    mnuContinue_Click(eventSender, eventArgs)
    'End Sub
    Public Sub mnuContinue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'jam 2021/07/20
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Session.Remove("WVS_ bGroups")
        Session.Remove("WVS_CommonPlot")
        Session.Remove("IsGroup")


        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_MainMenu.aspx")
        vurl = FriendlyUrl.Href("~/frm_MainMenu")

        Response.Redirect(vurl)




        'Support.FormManager.Close(Me)

    End Sub

    'JRS 10/05/02 WVS
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'Public Sub mnuGroups_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGroups.Popup
    '    mnuGroups_Click(eventSender, eventArgs)
    'End Sub
    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        'mti Augut 2004 3
        Dim SaveGroupInfo As Boolean

        'jam 2021/07/18
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        SaveGroupInfo = Session("WVS_ bGroups")

        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        Dim grpIdx As Integer = 0

        If objEA.Item.Caption = "Groups" Then
            'jam 2021/07/18
            objSender.Groups("Set Group or Country").Items("Groups").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            objSender.Groups("Set Group or Country").Items("Countries").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("WVS_ bGroups") = True
            'jam 2021/07/18
            Session("IsGroup") = True

            If SaveGroupInfo <> CBool(Session("WVS_ bGroups")) Then
                LoadGroups()
            End If
            'jrs 2008/09/05
            'Search for default group
            Dim i As Integer
            For i = 0 To cmb_Country.Items.Count - 1
                If cmb_Country.Items(i).Text = sDefGrp Then
                    cmb_Country.Items(i).Selected = True
                    grpIdx = i
                    Exit For
                End If
            Next i
        Else
            'jam 2021/07/18
            objSender.Groups("Set Group or Country").Items("Countries").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            objSender.Groups("Set Group or Country").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("WVS_ bGroups") = False

            'jam 2021/07/18
            Session("IsGroup") = False
            If SaveGroupInfo <> CBool(Session("WVS_ bGroups")) Then
                LoadRegions()
            End If
        End If
        If SaveGroupInfo <> CBool(Session("WVS_ bGroups")) Then
            If cmb_Country.Items.Count > 0 Then
                cmb_Country.SelectedIndex = grpIdx 'jrs 2011/09/22 0 ' Display First Item
            End If
            If cmbVariable.Items.Count > 0 Then
                'jrs 2011/09/22 cmbVariable.SelectedIndex = 0 ' Display First Item
            End If
            If cmb_Country.SelectedValue() <> "" And cmbVariable.SelectedValue() <> "" Then
                LoadVarPlot(cmb_Country.SelectedValue(), cmbVariable.SelectedValue())
                'Call DisplayGraph()                    
            End If
        End If
        '---------------
        'If mnuGroups.Text = "Use &Groups" Then
        '    mnuGroups.Text = "Use &Regions"
        '    lbl_countries.Text = "Groups"
        '    LoadGroups()
        '    Session("WVS_ bGroups") = True
        'Else
        '    mnuGroups.Text = "Use &Groups"
        '    lbl_countries.Text = "Countries or Regions"
        '    LoadRegions()
        '    Session("WVS_ bGroups") = False
        'End If
        'cmb_Country.SelectedIndex = 0 ' Display First Item

        'jrs 2011/05/06
        If Session("WVS_ bGroups") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                'jam 2021/07/18
                'Support.FormManager.Show(Me, "frm_WVSDisplay.aspx?Group=" & PrepareForURL(cmb_Country.SelectedValue))

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_WVSDisplay.aspx")


                vurl = FriendlyUrl.Href("~/frm_WVSDisplay", WSPrepareForFriendlyURL(cmb_Country.SelectedValue))

                Response.Redirect(vurl)



            Else
                'jam 2021/07/18
                'Support.FormManager.Show(Me, "frm_WVSDisplay.aspx?Group=" & PrepareForURL(Session("MMGroup")))

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_WVSDisplay.aspx")


                vurl = FriendlyUrl.Href("~/frm_WVSDisplay", WSPrepareForFriendlyURL(Session("MMGroup")))

                Response.Redirect(vurl)


            End If
        Else
            'jam 2021/07/18
            'Support.FormManager.Show(Me, "frm_WVSDisplay.aspx?Country=AF")

            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_WVSDisplay.aspx")


            vurl = FriendlyUrl.Href("~/frm_WVSDisplay", "AF")

            Response.Redirect(vurl)


        End If

    End Sub

    Public Sub mnuRegress_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2026/04/27 added Session("FirstYear") +
        'jrs 2025/02/05
        If (Session("FirstYear") + Session("PYear") - 1) Mod 5 = 0 Then
            Session("PYear") = Session("PYear") - 5
        Else
            'jrs 2026/04/27 added Session("FirstYear") +
            While (Session("FirstYear") + Session("PYear") - 1) Mod 5 <> 0
                Session("PYear") = Session("PYear") - 1
            End While
        End If
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        PackJScritpBuilder()
        'Call DisplayGraph()

    End Sub


    Public Sub mnuStatistics_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("Statistics_Type") = 0
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.

        'jam 2021/07/18

        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_statistics.aspx")
        'jam 2021/07/18
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String



        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_statistics.aspx")


        vurl = FriendlyUrl.Href("~/frm_statistics")

        Response.Redirect(vurl)


        'frm_statistics.DefInstance.Show() 
    End Sub

    Private Sub LoadRegions()
        Dim EleLoop As Object
        Dim DimNum As Object
        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        cmb_Country.Items.Clear()
        DimNum = 1

        For EleLoop = 1 To MaxVal(DimNum) - 1 'Exclude 'ALL'
            cmb_Country.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
        Next EleLoop
    End Sub

    Private Sub LoadVariables()
        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        cmbVariable.Items.Clear()

        cmbVariable.Items.Add(New ListItem("MATPOSTR"))
        cmbVariable.Items.Add(New ListItem("SURVSE"))
        cmbVariable.Items.Add(New ListItem("TRADSRAT"))
    End Sub

    Private Sub LoadVarPlot(ByRef sRegion As String, ByRef sVarName As String)
        Dim DimPoint As Short
        Dim ii As Short
        Dim FileNum As Integer = 0
        Dim GroupPoint As Short
        Dim SDimPoint As Short
        Dim EleLoop As Object
        Dim SDimNum As Short
        Dim DimNum As Short 'Object
        'ClearPlot()
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        Dim var_idx As Short
        var_idx = IfsVars.Item(sVarName).Index
        Session("SEL") = var_idx
        'jrs 2006/04/25
        Dim idxDim1 As Integer
        If Not Session("WVS_ bGroups") Then
            idxDim1 = gGetDimIdx(1, sRegion, True, Me)
        Else
            idxDim1 = gGetGroupIdx(sRegion, True, Me)
        End If
        'jrs 2006/05/17 Dim3
        Dim jj As Integer, dim3Size As Integer
        If VDim3(Session("SEL")) > 0 Then
            dim3Size = MAXCOM(VDim3(Session("SEL"))) - 1
        Else
            dim3Size = 1
        End If

        Dim sDim_1 As String, sDim_2 As String, sDim_3 As String
        'jrs 2011/09/22
        sDim_1 = sRegion
        If cboFiles.SelectedIndex > 0 Then
            FileNum = cboFiles.SelectedIndex
        End If

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For ii = 1 To MaxVal(VDim2(var_idx)) - 2
            For jj = 1 To dim3Size
                Session("COUNTE") = Session("COUNTE") + 1
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(sVarName, var_idx, sRegion, idxDim1, sDim_2, ii, sDim_3, jj, "", FileNum, ii = 1, ii = MaxVal(VDim2(var_idx)) - 2, Session("WVS_ bGroups"), 1, Session("PLOT"), Session("COUNTE"),
                1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                Else
                    gLoadForDisplay(sVarName, var_idx, sRegion, idxDim1, sDim_2, ii, sDim_3, jj, "", FileNum, ii = 1, ii = MaxVal(VDim2(var_idx)) - 2, Session("WVS_ bGroups"), 1, Session("PLOT"), Session("COUNTE"),
                    1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If
                gLoadForDisplaySaveLbls(Session("COUNTE"), Session("SEL"), sDim_1, sDim_2, sDim_3, Session("WVS_ bGroups"), FileNum, "",
                Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                sDim_2 = ""
                sDim_3 = ""
            Next jj
        Next ii
        Session("WVS_pCOUNTE") = Session("COUNTE")
        PackJScritpBuilder()
    End Sub

    Private Sub LoadGroups()
        cmb_Country.Items.Clear()
        Dim db As String 'DAO.Database
        Dim valtbl As DataTable 'DAO.Recordset
        Dim i As Short
        'db = DAODBEngine_definst.OpenDatabase(IFs_MDB2Active)
        db = Session("IFs_MDB2Active")
        'valtbl = db.OpenRecordset(GroupIDNew)
        'jrs 2025/07/30 added UPPER which makes a difference now that we're using SQLite
        SetDataTablefromAccess(db, "Select * from " & GroupIDNew & " Order by UPPER(GroupName)", "", valtbl)
        'valtbl.Index = "GroupName"
        'valtbl.MoveFirst()
        'While Not valtbl.EOF
        For i = 0 To valtbl.Rows.Count - 1
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_Country.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
            'valtbl.MoveNext()
        Next
        'End While
        'valtbl.Close()
        'db.Close()
    End Sub

    Private Sub LoadFiles_Org()
        'JRS 10/15/02
        'Dim fs 'As New Scripting.FileSystemObject
        'Dim BASE 'As Scripting.File
        'Dim currRun 'As Scripting.File
        ''Dim fRunFile As Folder
        'fs = CreateObject("Scripting.FileSystemObject")
        ''Set fRunFile = fs.GetFolder(PutFdrvRun2$)
        ''fRunFile.Files returns collection of files
        'If Session("FirstYear") = FirstYearHistory Then
        '    BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run")
        'Else
        '    BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run")
        'End If
        ''CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cboFiles.Items.Clear()
        'FilesSelected = 1
        'Dim sWorkingFile As String
        'sWorkingFile = VB.Right(WorkingFile, Len(WorkingFile) - InStrRev(WorkingFile, "\"))
        ''CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cboFiles.Items.Add(New ListItem("0 - " & "Working File, based on " & sWorkingFile))
        ''CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cboFiles.Items(cboFiles.SelectedIndex).Text = "0 - " & "Working File, based on " & sWorkingFile
        ''CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cboFiles.Items.Add(New ListItem("1 - " & BASE.Name))
        'FilesSelected = 1

        'Dim sRunFile As String
        '' Returns filename with specified extension. If more than one *.ini
        '' file exists, the first file found is returned.
        ''UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'sRunFile = Dir(Session("PutFdrvRun2") & "\*.run")

        'Do While (sRunFile <> "")
        '    If UCase(sRunFile) <> UCase(BASE.Name) And UCase(sRunFile) <> UCase(sWorkingFile) And UCase(sRunFile) <> "WORKING.RUN" Then 'JRS 11/08/09
        '        currRun = fs.GetFile(Session("PutFdrvRun2") & "\" & sRunFile)
        '        'UPGRADE_WARNING: Couldn't resolve default property of object currRun.size. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '        'UPGRADE_WARNING: Couldn't resolve default property of object BASE.size. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '        If BASE.Size = currRun.Size Then
        '            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '            cboFiles.Items.Add(New ListItem(Trim(Str(cboFiles.Items.Count)) & " - " & sRunFile))
        '            FilesSelected = FilesSelected + 1
        '            'JRS RUNFILES 5/11/02
        '            ReDim Preserve RunFiles(FilesSelected)
        '            RunFiles(FilesSelected) = Session("PutFdrvRun2") & "\" & sRunFile
        '        End If
        '    End If
        '    ' Call Dir again without arguments to return the next *.INI file in the
        '    ' same directory.
        '    'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        '    sRunFile = Dir()
        'Loop

    End Sub


    Private Sub LoadFiles(ByRef FileList() As String, ByRef cbo As System.Web.UI.WebControls.DropDownList)
        Dim i As Short
        For i = 0 To UBound(FileList)
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cbo.Items.Add(New ListItem(FileList(i)))
        Next i
    End Sub
    '=============================Chart_Hist.Frm  =======================
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e



        'Choose the correct function
        Select Case objEA.Item.Caption
                'Case "Continue"
                '    mnuContinue_Click(sender, e)
                'Case "Save", "Print"
                '    Case "Common", "Scale" '"Delete Selected Driver/Dimension"
                '        mnuCommon_Click(sender, e)
            Case "Advance"
                mnuAdvance_Click(sender, e)
            Case "Regress"
                mnuRegress_Click(sender, e)
            Case "Groups", "Countries"
                mnuGroups_Click(sender, e)
            Case "Continue" 'jam 2021/07/20
                mnuContinue_Click(sender, e)
            Case "About WVS" 'jrs 2025/02/04 
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "~\AboutModel.aspx")
        End Select
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    'jrs 2015/10/20
    'I tried using 3d bar, but lots of complications found
    'options3d: {enabled: true,alpha: 15,beta: 15,depth: 50,viewDistance: 25}},
    '1. links need to be absolute, probably the version I have is not compatible with the 3d file needed
    '<script src="http://code.highcharts.com/highcharts-3d.js"></script>
    '2. Need to play with margin bottom to show full graph if using some rotation angle for the graph
    '3. Can't change the color of the bars to show different faces of the bar (3D effect), had to remove the color
    Private Sub PackJScritpBuilder()
        If Session("WVS_ bGroups") = False Then
            Dim sJscript As String, i As Integer
            sJscript = "    var RegCode = [];" & vbCr
            For i = 1 To NR
                sJscript = sJscript & "    RegCode[" & i.ToString & "] = """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """;" & vbCr
            Next

            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadRegCode", sJscript, True)

            cmb_Country.Attributes.Add("onchange", "cntrylist_click();")
        Else
            cmb_Country.Attributes.Add("onchange", "lstGroups_click();")
        End If

        Dim Ages() As String
        ReDim Ages(5)
        Ages(5) = "65+" 'jrs 2013/02/22 "100+"

        'jrs 2026/04/23 
        'Dim l As Integer
        'For l = 1 To 4
        '    Ages(l) = Str((l + 4) * 5) & "-" & Str((l + 5) * 5 - 1)
        'Next l
        Ages(4) = "55-64"
        Ages(3) = "45-54"
        Ages(2) = "35-44"
        Ages(1) = "25-34"

        Ages(0) = "18-24"

        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "            var mycategories = [" & vbCr
        For l = 0 To 5
            If l > 0 Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & "'" & Ages(l) & "'"
        Next
        sJscript2 = sJscript2 & "];" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/11 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "type: 'bar'," & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        sJscript2 = sJscript2 & "events: {"
        sJscript2 = sJscript2 & "load: function(event) {"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.visibility = 'hidden';"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.height = 0;"
        sJscript2 = sJscript2 & "}}"
        sJscript2 = sJscript2 & "}," & vbCr

        Dim graphtitle As String = ""
        graphtitle = getSeriesName(1, 1, Nothing)

        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr
        sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        'jrs 2025/07/30
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Age Categories'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "categories: mycategories, " & vbCr
        sJscript2 = sJscript2 & "reversed: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxis As String = ""
        sAxis = "Year = " & Str(Session("FirstYear") + Session("PYear") - 1)

        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        'jrs 2025/07/30
        If cmbVariable.SelectedValue = "SURVSE" Then
            sJscript2 = sJscript2 & "min: -2," & vbCr
            sJscript2 = sJscript2 & "max: 2," & vbCr
        End If

        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "x: -50," & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxis & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<b>' + this.series.name + ', age ' + this.point.category + '</b><br/>' +"
        sJscript2 = sJscript2 & "'Population: ' + Highcharts.numberFormat(Math.abs(this.point.y), 2);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        sJscript2 = sJscript2 & PlotColToJSSeries(Session("PYear"), Session("PLOT"), True, 1, Nothing, 0, NCohorts, True)

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        'jam 2024/08/17 Bug fix related to GET parameters
        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr

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

        If Session("bCompare") Then
            sJscript2 = sJscript2 & "myWidth = myWidth * 0.49;" & vbCr
            sJscript2 = sJscript2 & "myHeight = myHeight * 0.49;" & vbCr
        End If

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2
    End Sub

    Private Function getSeriesName(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Legend As String
        If Session("AboutParent") = "frm_PopCohortDisp.aspx" Then
            If ii = 1 And idx <> 2 Then
                Legend = "Males"
            Else
                Legend = "Females"
            End If
        ElseIf Session("AboutParent") = "frm_CohoInfDisp.aspx" Then 'jrs 2012/06/22
            If idx <> 2 Then
                If ii = 1 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            Else
                Legend = ""
            End If
        ElseIf Session("AboutParent") = "frm_EdnCohoDisp.aspx" Then
            If idx = 0 Or idx > 4 Then
                Select Case ii
                    Case 1, 6
                        Legend = "Children"
                    Case 2, 7
                        Legend = "No education or incomplete primary education"
                    Case 3, 8
                        Legend = "Completed primary education only"
                    Case 4, 9
                        Legend = "Completed through secondary education"
                    Case 5, 10
                        Legend = "Completed through tertiary education"
                End Select
                If ii > 5 Then
                    Legend = Legend & "(F)"
                End If
            ElseIf idx < 5 Then
                If ii < 6 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            End If
        ElseIf Session("AboutParent") = "frm_MortCohorts.aspx" Then 'Health
            If idx = 0 Then
                Select Case ii
                    Case 1
                        Legend = "Communicable Disease"
                    Case 2
                        Legend = "Non Communicable Disease"
                    Case 3
                        Legend = "Injury"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            Else
                If ii < gphType.NumSubTypes / 2 + 1 Then
                    Legend = gphType.getSubTypeName(ii * 2 - 1)
                Else
                    Legend = gphType.getSubTypeName((ii - gphType.NumSubTypes / 2) * 2 - 1) & "(F)"
                End If
            End If
        ElseIf Session("AboutParent") = "frm_MorbCohorts.aspx" Then 'jrs 2013/02/22
            If idx = 0 Then
                Select Case ii
                    Case 1
                        Legend = "Communicable Disease"
                    Case 2
                        Legend = "Non Communicable Disease"
                    Case 3
                        Legend = "Injury"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            End If
        Else 'WVS
            Dim VN As String, D1 As String
            '           Get legend components
            '           ---------------------

            VN = LTrim(RTrim(Session("VARN")(0, ii)))
            'jrs 10/27/04 Flexible Dimension Names for Graphs
            D1 = LTrim(RTrim(Session("FDNG1")(ii)))

            '          Compose legend and assign legend
            '           --------------------------------

            Legend = VN
            If Len(D1) > 0 Then
                Legend = Legend & "(" & D1 & ")"
            End If
        End If
        getSeriesName = Legend
    End Function

    Private Function PlotColToJSSeries(ByVal i As Integer, ByRef Vals As Object, ByVal bShow100 As Boolean, ByVal Idx As Integer,
                         ByVal gphType As MortType, ByVal CompIdx As Integer, ByVal iNCohortsWD As Integer,
                         ByVal bAllZero As Boolean) As String
        Dim iNCohorts As Integer
        iNCohorts = 6

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i, Idx, gphType) & """, showInLegend: false, color: ""Lime"", data: [" & vbCr

        Dim bLeftPyramid As Boolean = False, colIdx As Integer, iLowCohort As Integer
        colIdx = i
        iLowCohort = 1

        Dim j As Integer
        Dim bFirst As Boolean = True
        For j = iLowCohort To iNCohorts
            'jrs 2013/01/03
            If bFirst Then
                bFirst = False
            Else
                sJscript = sJscript & "," & vbCr
            End If
            sJscript = sJscript & CStr(Vals(colIdx, j))
        Next j

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/07/18
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function WSPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        WSPrepareForFriendlyURL = tmp
    End Function

End Class
