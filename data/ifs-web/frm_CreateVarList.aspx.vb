
'cdv 2022/05/19 ****START****
Partial Class frm_CreateVarList
    Inherits Page


    Protected Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Support.FormManager.Show(Me, "frm_Varsel.aspx")
    End Sub

    Protected Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        Session("sNameList") = TextBox1.Text
        If Session("sNameList") = "" Then
            LoadMessage("You have to specify a Name", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        ElseIf Len(Session("sNameList")) > 65 Then
            LoadMessage("Please use a shorter name", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        ElseIf InStr(Session("sNameList"), "-") > 0 Then
            LoadMessage("A Variable list cannot have a ""-""", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
        Session("sNameList") = CType(Session("sNameList"), String).Replace("'", "''")
        Session("fromSelfManaged") = "True"
        Support.FormManager.Show(Me, "frm_VariableListEdit.aspx")
    End Sub

End Class
'cdv 2022/05/19 ****END****
