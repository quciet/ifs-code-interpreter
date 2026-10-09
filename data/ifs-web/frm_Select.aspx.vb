'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''          jrs 2016/03/25 Hist. Empirical variable selection
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   Copyright by Barry B. Hughes 1988-2003
'   Model code available for review and education only
'   Reproduction or incorporation in other software by permission only
'   Last revision by BBH on July 17, 2002 - added MTI's validation code
Option Strict Off
Option Explicit On

Partial Class frm_Select
    Inherits Page


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim i As Integer
            optSelect1.Text = Session("HEVars")(0)
            optSelect1.Checked = True
            optSelect2.Text = Session("HEVars")(1)
            If Session("HEVarsCount") > 2 Then
                optSelect3.Text = Session("HEVars")(2)
                optSelect3.Visible = True
            End If
            If Session("HEVarsCount") > 3 Then
                optSelect4.Text = Session("HEVars")(3)
                optSelect4.Visible = True
            End If
            If Session("HEVarsCount") > 4 Then
                optSelect5.Text = Session("HEVars")(4)
                optSelect5.Visible = True
            End If
        End If
    End Sub


    Protected Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Dim i As Integer, sVar As String
        Select Case True
            Case optSelect1.Checked
                sVar = optSelect1.Text
            Case optSelect2.Checked
                sVar = optSelect2.Text
            Case optSelect3.Checked
                sVar = optSelect3.Text
            Case optSelect4.Checked
                sVar = optSelect4.Text
            Case optSelect5.Checked
                sVar = optSelect5.Text
        End Select
        Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & sVar)

    End Sub
End Class

