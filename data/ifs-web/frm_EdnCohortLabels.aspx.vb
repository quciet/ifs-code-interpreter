Option Strict Off
Option Explicit On
Imports Microsoft.AspNet.FriendlyUrls


Imports System.Security.Policy


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'      jam 2026/02/27      frm_EdnCohoDisp.aspx labels costumization
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Partial Class frm_EdnCohortLabels
    Inherits Page

#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ' InitializeComponent()

        'If Not Page.IsPostBack Then
        '    'txt_XAxisLabel.Text = Session("XAxisLabel")
        '    'txt_YAxisLabel.Text = Session("YAxisLabel")
        '    txt_TitleLabel.Text = Session("TitleLabel")
        '    txt_SubTitleLabel.Text = Session("SubTitleLabel")

        'End If
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

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String



        'If txt_XAxisLabel.Text <> Session("EdnXAxisLabel") Then
        '    Session("EdnXAxisLabel") = txt_XAxisLabel.Text
        'End If

        'If txt_YAxisLabel.Text <> Session("EdnYAxisLabel") Then
        '    Session("EdnYAxisLabel") = txt_YAxisLabel.Text
        'End If

        'If Session("EdnTitleLabel") <> txt_TitleLabel.Text Then
        '    Session("EdnTitleLabel") = txt_TitleLabel.Text
        'End If

        'If Session("EdnSubTitleLabel") <> txt_SubTitleLabel.Text Then
        '    Session("EdnSubTitleLabel") = txt_SubTitleLabel.Text
        'End If

        If (Session("EdnTitleLabel") <> txt_TitleLabel.Text) Or (Session("EdnSubTitleLabel") <> txt_SubTitleLabel.Text) Then
            Session("EdnLabelChanged") = True
        Else
            Session("EdnLabelChanged") = False
        End If

        'If Not (txt_XAxisLabel.Text Is Nothing) Then
        '    If Session("EdnXAxisLabel") <> txt_XAxisLabel.Text Then
        '        Session("EdnXAxisLabel") = txt_XAxisLabel.Text
        '    End If
        'End If
        'If Not (txt_YAxisLabel.Text Is Nothing) Then
        '    If Session("EdnYAxisLabel") <> txt_YAxisLabel.Text Then
        '        Session("EdnYAxisLabel") = txt_YAxisLabel.Text
        '    End If
        'End If
        If Not (txt_TitleLabel.Text Is Nothing) Then
            If Session("EdnTitleLabel") <> txt_TitleLabel.Text Then
                Session("EdnTitleLabel") = txt_TitleLabel.Text
            End If
        End If
        If Not (txt_SubTitleLabel.Text Is Nothing) Then
            If Session("EdnSubTitleLabel") <> txt_SubTitleLabel.Text Then
                Session("EdnSubTitleLabel") = txt_SubTitleLabel.Text
            End If
        End If





        'Support.FormManager.Show(Me, Session("EdnLabelsParent"))

        'jam 2026/02/27
        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
        vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp")
        Response.Redirect(vurl)

    End Sub

    Private Sub frm_EdnCohortLabels_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'txt_XAxisLabel.Text = Session("EdnXAxisLabel")
        'txt_YAxisLabel.Text = Session("EdnYAxisLabel")

        If Not Page.IsPostBack Then
            txt_TitleLabel.Text = Session("EdnTitleLabel")
            txt_SubTitleLabel.Text = Session("EdnSubTitleLabel")
        End If
    End Sub


    'CONVERSION_ISSUE: Event 'System.Windows.Forms.TextBox.Leave' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Leave.html'.
    'Private Sub txt_XAxisLabel_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    If Not IsNumeric(txt_XAxisLabel.Text) Then
    '        txt_XAxisLabel.Text = ""
    '    End If
    'End Sub
End Class
