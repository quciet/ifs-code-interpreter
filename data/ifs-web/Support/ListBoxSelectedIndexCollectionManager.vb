'Namespace IFsWeb.Support
Namespace Support
    Public Class ListBoxSelectedIndexCollectionManager

        Shared Function GetText(ByVal ListBox As System.Web.UI.WebControls.ListBox) As String
            If Not ListBox Is Nothing Then
                If Not ListBox.SelectedItem Is Nothing Then
                    Return ListBox.SelectedItem.Text
                Else
                    Return ""
                End If
            End If
        End Function

        Shared Sub SetText(ByVal ListBox As System.Web.UI.WebControls.ListBox, ByVal Text As String)
            If Not ListBox Is Nothing Then
                If Not ListBox.SelectedItem Is Nothing Then
                    ListBox.SelectedItem.Text = Text
                End If
            End If
        End Sub

        Shared Function SelectedIndices(ByVal list_p As System.Web.UI.WebControls.ListBox) As System.Web.UI.WebControls.ListItemCollection
            Dim vlistweb As New System.Web.UI.WebControls.ListItemCollection()
            Dim i As System.Web.UI.WebControls.ListItem
            Dim j As Integer = 0

            For Each i In list_p.Items
                If i.Selected Then
                    vlistweb.Add(j)
                End If
                j = j + 1
            Next
            Return vlistweb
        End Function

        Shared Function SelectedItems(ByVal ListBox As System.Web.UI.WebControls.ListBox) As System.Web.UI.WebControls.ListItemCollection
            Dim vlistweb As New System.Web.UI.WebControls.ListItemCollection()
            Dim item As System.Web.UI.WebControls.ListItem

            For Each item In ListBox.Items
                If item.Selected Then
                    vlistweb.Add(item)
                End If
            Next
            Return vlistweb
        End Function

        Shared Function SelectedItem(ByVal list As System.Web.UI.WebControls.ListItemCollection, ByVal index As Integer) As Integer
            Dim result As Integer = -1
            If Not list Is Nothing Then
                Try
                    result = list(index).Value
                Catch
                End Try
            End If
            Return result
        End Function

    End Class
End Namespace
