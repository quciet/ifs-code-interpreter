Option Strict Off
Option Explicit On

Partial Class frm_FilterParameters
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
        'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.
        Me.mnuExit = New ArtinSoft.WinFormsToWeb.WebControls.MenuItem()
        Me.mnuExit.Name = "mnuExit"
        Me.mnuSave = New ArtinSoft.WinFormsToWeb.WebControls.MenuItem()
        Me.mnuSave.Name = "mnuSave"
        Me.mnuExit.Text = "Exit"
        Me.mnuExit.Enabled = True
        Me.mnuExit.Visible = True
        Me.mnuSave.Text = "Save"
        Me.mnuSave.Enabled = True
        Me.mnuSave.Visible = True
        Me.mnuExit.Index = 0
        Me.mnuSave.Index = 1
        If Not Page.IsPostBack Then
            'load variables
            txtMinimumLevel.Text = Trim(Str(Session("Fil_Min_Level")))
            txtMaximumLevel.Text = Trim(Str(Session("Fil_Max_Level")))
            txtRateChange.Text = Trim(Str(Session("Fil_Rate")))
            txtRangefrom.Text = Trim(Str(Session("Fil_Range_Min")))
            txtRangeto.Text = Trim(Str(Session("Fil_Range_Max")))
            'jrs 2012/01/16
            chkLoadedValues.Checked = Session("bFiltLoadedValues")
            chkDisplayCount.Checked = Session("bFiltDisplayCount")
        End If
    End Sub
    'CONVERSION_NOTE: Method 'Dispose' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
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
    Public mnuExit As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuSave As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Protected WithEvents MainMenu1 As ArtinSoft.WinFormsToWeb.WebControls.MainMenu
    Protected WithEvents ToolTip1 As System.Web.UI.WebControls.Label
    Protected WithEvents WebMenu1 As Coalesys.WebMenu.WebMenu
    Protected WithEvents WinFormsToWeb_MenuManager As ArtinSoft.WinFormsToWeb.WebControls.MenuManager
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_FilterParametersPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_FilterParameters
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_FilterParameters()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    'JRS 12/10/02 FILTER

    'CONVERSION_NOTE: The code of Event 'frm_FilterParameters.frm_FilterParameters_Load' was moved to Method 'frm_FilterParameters.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_FilterParameters_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Save & Exit"
        cswmGroup1.ID = "Group_8df1"

        Dim cswmItem As Coalesys.WebMenu.Item
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Save"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save & Exit"

    End Sub

    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'save and exit
        mnuSave_Click(mnuSave, New System.EventArgs)
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
    End Sub

    Public Sub mnuSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'save variables
        Session("Fil_Min_Level") = Val(txtMinimumLevel.Text)
        Session("Fil_Max_Level") = Val(txtMaximumLevel.Text)
        Session("Fil_Rate") = Val(txtRateChange.Text)
        Session("Fil_Range_Min") = Val(txtRangefrom.Text)
        Session("Fil_Range_Max") = Val(txtRangeto.Text)
        'jrs 2012/01/16
        Session("bFiltLoadedValues") = chkLoadedValues.Checked
        Session("bFiltDisplayCount") = chkDisplayCount.Checked
    End Sub

    'JRS 03/21/04
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            'Case "Save"
            '    mnuSave_Click(sender, e)
            Case "Save & Exit"
                mnuExit_Click(sender, e)
        End Select
    End Sub
End Class
