'Namespace IFsWeb.Support
Namespace Support
    Public Class ComboboxManager

        Shared Function getText(ByVal objectIn As System.Web.UI.WebControls.DropDownList) As System.String
            Dim stringOut As System.String
            If (objectIn.SelectedIndex >= 0) Then
                stringOut = objectIn.Items(objectIn.SelectedIndex).Text
            Else
                stringOut = ""
            End If
            Return stringOut
        End Function

        Shared Function getSelectedItem(ByVal objectIn As System.Web.UI.WebControls.DropDownList) As System.String
            Dim stringOut As System.String
            If objectIn.SelectedItem Is Nothing Then
                stringOut = ""
            Else
                stringOut = objectIn.SelectedItem.value
            End If
            Return stringOut
        End Function
    End Class

End Namespace
