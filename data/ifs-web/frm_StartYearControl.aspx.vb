Option Strict Off
Option Explicit On
'CDV 2021/10/22 ****START****
Partial Class frm_StartYearControl
    Inherits Page
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        lbl_FirstYear.Text = "Enter New Start Year Below:"
        lbl_FirstYear.Visible = True
        If Session("OriginalFirstYear") Is Nothing Then
            'jrs 2023/10/18 added int to session("FirstYear")
            For i = Int(Session("FirstYear")) To Int(Session("DisplayLastYear"))
                lst_FirstYear.Items.Add(i)
            Next
        Else
            For i = Session("OriginalFirstYear") To Int(Session("DisplayLastYear"))
                lst_FirstYear.Items.Add(i)
            Next
        End If

        If Session("UserFirstYear") Is Nothing = False Then
            lst_FirstYear.Items.FindByValue(Session("UserFirstYear")).Selected = True
        End If

        lst_FirstYear.Visible = True
        txtStartYear.Focus()
    End Sub

    Protected Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        frm_StartYearControl_Closed()
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, Session("DisplayControlParent"))
    End Sub

    Private Sub frm_StartYearControl_Closed()
        If Session("UserFirstYear") = Nothing Then
            Session("OriginalFirstYear") = Session("FirstYear")
        End If
        Dim year_to_use As String
        If txtStartYear.Text.Length > 0 Then
            If Not IsNumeric(txtStartYear.Text) Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))
            Else
                If Session("OriginalFirstYear") = FirstYearForecast Then
                    If CInt(txtStartYear.Text) > Session("DisplayLastYear") Or CInt(txtStartYear.Text) < FirstYearForecast Then
                        LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("DisplayLastYear"), Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))
                    Else
                        year_to_use = txtStartYear.Text
                    End If
                ElseIf Session("OriginalFirstYear") = FirstYearHistory Then
                    If CInt(txtStartYear.Text) > Session("DisplayLastYear") Or CInt(txtStartYear.Text) < FirstYearHistory Then
                        LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & Session("DisplayLastYear"), Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))
                    Else
                        year_to_use = txtStartYear.Text
                    End If
                Else 'jrs 2024/11/28
                    year_to_use = txtStartYear.Text
                End If
            End If
        Else
            year_to_use = lst_FirstYear.Text
        End If
        Session("UserFirstYear") = year_to_use
        Session("FirstYear") = year_to_use

        If Session("UserFirstYear") <> Session("OriginalFirstYear") Then
            Session("RowAdjustment") = Session("UserFirstYear") - Session("OriginalFirstYear")
        Else
            Session("RowAdjustment") = 0
        End If

        'CONVERSION_ISSUE: Property 'IFsGUI.frm_DisplayMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'jrs 2005/06/07 Session("frm_DisplayMenutxt_TableTitle") = txt_TableTitle.Text
        'jrs 2005/06/07
        'jrs 2023/10/18 Session("PYear") = CInt(lst_FirstYear.Text) - Session("FirstYear") + 1
        Session("PYear") = CInt(year_to_use) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

    End Sub

    Protected Sub lst_FirstYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lst_FirstYear.SelectedIndexChanged
        frm_StartYearControl_Closed()
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, Session("DisplayControlParent"))
    End Sub

    Protected Sub cmdStartYear_Click(sender As Object, e As EventArgs) Handles cmdStartYear.Click
        frm_StartYearControl_Closed()
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, Session("DisplayControlParent"))
    End Sub
End Class
'CDV 2021/10/22 ****END****
