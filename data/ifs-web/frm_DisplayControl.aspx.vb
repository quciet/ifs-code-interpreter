Option Strict Off
Option Explicit On

Partial Class frm_DisplayControl
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
        If Not Page.IsPostBack Then
            lbl_interval.Text = "Edit table display interval:"
            If Session("Interval") Is Nothing Then
                Session("Interval") = 1
            End If
            If Session("PYear") Is Nothing Then
                Session("PYear") = 1
            End If

            Txt_Interval.Text = CStr(Session("Interval"))

            lbl_PieYear.Text = "Edit year for pie, scatterplot, or map:"
            lbl_PieYear.Visible = True
            Txt_PieYear.Text = Str(Session("FirstYear") + Session("PYear") - 1)
            Txt_PieYear.Visible = True

            lbl_TableTitle.Text = "Edit title for tables and graphs:"
            txt_TableTitle.Text = Session("TableTitle")
            lbl_TableTitle.Visible = True
            txt_TableTitle.Visible = True

            lbl_YAxisLabel.Text = "Edit Y-Axis title for graphs:"
            txt_YAxisLabel.Text = Session("YAxisLabel")

            'jrs 2006/11/28
            lbl_XAxisLabel.Text = "Edit X-Axis title for graphs:"
            txt_XAxisLabel.Text = Session("XAxisLabel")

            'jrs 2024/01/31
            cboGridLineWidth.Items.Add("0")
            cboGridLineWidth.Items.Add("2")
            cboGridLineWidth.Items.Add("4")
            If Session("gridLineWidth") > "" Then
                cboGridLineWidth.Text = Session("gridLineWidth")
            End If
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_DisplayControlPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_DisplayControl
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_DisplayControl()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    'JRS 5/6/02
    'Display Control for Graphics

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        frm_DisplayControl_Closed()
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, Session("DisplayControlParent"))

    End Sub

    'CONVERSION_NOTE: The code of Event 'frm_DisplayControl.frm_DisplayControl_Load' was moved to Method 'frm_DisplayControl.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_DisplayControl_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    End Sub


    Private Sub frm_DisplayControl_Closed()
        Session("Interval") = Int(CDbl(Txt_Interval.Text))
        Session("frm_DisplayMenuTxt_PieYear") = Txt_PieYear.Text
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_DisplayMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'jrs 2005/06/07 Session("frm_DisplayMenutxt_TableTitle") = txt_TableTitle.Text
        'jrs 2005/06/07
        Session("TableTitle") = txt_TableTitle.Text
        'JRS 2006/05/09
        Session("YAxisLabel") = txt_YAxisLabel.Text
        'jrs 2006/11/28
        Session("XAxisLabel") = txt_XAxisLabel.Text
        Session("bDisplayControl") = True
        'jrs 2019/07/29
        Session("YAxisLabelOrg") = Session("YAxisLabel")

        Session("PYear") = CInt(Txt_PieYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        'jrs 2024/01/17
        If txt_YMin.Text > "" Then
            Session("YAxisMin") = txt_YMin.Text
        Else
            Session("YAxisMin") = Nothing
        End If
        If txt_YMax.Text > "" Then
            Session("YAxisMax") = txt_YMax.Text
        Else
            Session("YAxisMax") = Nothing
        End If
        'jrs 2024/01/31
        If cboGridLineWidth.Text > "" Then
            Session("gridLineWidth") = cboGridLineWidth.Text
        Else
            Session("gridLineWidth") = Nothing
        End If
        If txtHorizontalLineValue.Text > "" AndAlso IsNumeric(txtHorizontalLineValue.Text) Then
            Session("HorizontalLineValue") = txtHorizontalLineValue.Text
        Else
            Session("HorizontalLineValue") = Nothing
        End If
        If txtHorizontalLineText.Text > "" Then
            Session("HorizontalLineText") = txtHorizontalLineText.Text
        Else
            Session("HorizontalLineText") = Nothing
        End If
    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.TextBox.Leave' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Leave.html'.
    Private Sub Txt_Interval_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Not IsNumeric(Txt_Interval.Text) Then
            Txt_Interval.Text = "1"
        End If
    End Sub



End Class

