Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'                   JAM 2011-01-24      Parameter form to define the Power Degree
'                                       
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Partial Class frm_ScatterPower
    Inherits Page
    Dim InitialPower
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ' InitializeComponent()
        If Not Page.IsPostBack Then
            InitialPower = Session("PowerDegree")
            txtPowerDegree.Text = Session("PowerDegree")
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
    '<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    'End Sub
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
        frm_ScatterPower_Closed()
        'Support.FormManager.StateStore(Me)
        'JAM 2009-07-03 commented to return to main form
        Support.FormManager.Show(Me, Session("ScatterPower"))
        'Support.FormManager.Show(Me, Session("DisplayControlParent"))

    End Sub

    'CONVERSION_NOTE: The code of Event 'frm_DisplayControl.frm_DisplayControl_Load' was moved to Method 'frm_DisplayControl.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_ScatterPower_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'jrs 2012/05/31
        txtPowerDegree.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdExit.ClientID + "')")
    End Sub

    Private Sub frm_ScatterPower_Closed()
        Dim a As Double

        If Not IsNumeric(txtPowerDegree.Text) Then

            Session("PowerDegree") = InitialPower

        Else
            a = CDbl(txtPowerDegree.Text)
            Session("PowerDegree") = CDbl(txtPowerDegree.Text)


        End If

        If InitialPower = Session("PowerDegree") Then
            Session("IsChangedNum") = False
        Else
            Session("IsChangedNum") = True
        End If
        Session("SourcePower") = "PowerDegree"
        'Session("bDisplayControl") = True
    End Sub

    Private Sub Txt_ScatterPower_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Not IsNumeric(txtPowerDegree.Text) Then
            txtPowerDegree.Text = InitialPower
        End If
    End Sub
End Class
