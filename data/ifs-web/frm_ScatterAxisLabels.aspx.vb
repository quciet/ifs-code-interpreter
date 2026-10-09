Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'      JAM 2010-01-30      Parameter form for Graph Labels Costumization
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Partial Class frm_ScatterAxisLabels
    Inherits Page

#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ' InitializeComponent()

        If Not Page.IsPostBack Then
            txt_XAxisLabel.Text = Session("XAxisLabel")
            txt_YAxisLabel.Text = Session("YAxisLabel")
            txt_TitleLabel.Text = Session("TitleLabel")
            txt_SubTitleLabel.Text = Session("SubTitleLabel")

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

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        frm_ScatterAxisLabels_Closed()
        'Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, Session("AxisLabelsParent"))
        'Support.FormManager.Show(Me, Session("DisplayControlParent"))

    End Sub

    Private Sub frm_ScatterAxisLabels_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub frm_ScatterAxisLabels_Closed()


        If (Session("XAxisLabel") <> txt_XAxisLabel.Text) Or (Session("YAxisLabel") <> txt_YAxisLabel.Text) Or (Session("TitleLabel") <> txt_TitleLabel.Text) Or (Session("SubTitleLabel") <> txt_SubTitleLabel.Text) Then
            Session("LabelChanged") = True
        Else
            Session("LabelChanged") = False
        End If

        If Not (txt_XAxisLabel.Text Is Nothing) Then
            If Session("XAxisLabel") <> txt_XAxisLabel.Text Then
                Session("XAxisLabel") = txt_XAxisLabel.Text
            End If
        End If
        If Not (txt_YAxisLabel.Text Is Nothing) Then
            If Session("YAxisLabel") <> txt_YAxisLabel.Text Then
                Session("YAxisLabel") = txt_YAxisLabel.Text
            End If
        End If
        If Not (txt_TitleLabel.Text Is Nothing) Then
            If Session("TitleLabel") <> txt_TitleLabel.Text Then
                Session("TitleLabel") = txt_TitleLabel.Text
            End If
        End If
        If Not (txt_SubTitleLabel.Text Is Nothing) Then
            If Session("SubTitleLabel") <> txt_SubTitleLabel.Text Then
                Session("SubTitleLabel") = txt_SubTitleLabel.Text
            End If
        End If






        'If Session("VarperG") = Session("ItemsbySet") Then
        '    Session("IsChangedNum") = False
        'Else
        '    Session("IsChangedNum") = True
        'End If

    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.TextBox.Leave' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Leave.html'.
    Private Sub txt_XAxisLabel_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Not IsNumeric(txt_XAxisLabel.Text) Then
            txt_XAxisLabel.Text = ""
        End If
    End Sub
End Class
