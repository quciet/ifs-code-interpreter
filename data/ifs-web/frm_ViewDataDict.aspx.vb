Imports System.Data.SQLite

Partial Class frm_ViewDataDict
    Inherits Page

    Private Sub frm_ViewDataDict_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim db1 As SqliteConnection
            Dim db1Cmd As SqliteCommand

            db1 = New SqliteConnection("DataSource=" + DataFileDir$ + "DATADICT.DB;")
            db1.Open()

            db1Cmd = db1.CreateCommand
            db1Cmd.CommandText = "SELECT * FROM [DataDict] ORDER BY [Variable]"

            Dim db1Rdr As SqliteDataReader
            db1Rdr = db1Cmd.ExecuteReader

            Dim tbl As DataTable = New DataTable

            Dim dsAll As DataSet = New DataSet
            dsAll.EnforceConstraints = False
            dsAll.Tables.Add(tbl)
            tbl.Load(db1Rdr)

            GridView1.DataSource = tbl
            GridView1.DataBind()
            Session("ViewDataTbl") = tbl

            db1Cmd.Dispose()

            db1.Close()
            db1.Dispose()
        End If
    End Sub

    Private Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        If Not (Session("ViewDataTbl") Is Nothing) Then
            Dim tbl As DataTable
            tbl = Session("ViewDataTbl")

            'By default, set the sort direction to ascending.
            Dim SortDirection As String = "ASC"
            If Session("ViewDataSortDirection") = Nothing Then
                Session("ViewDataSortDirection") = "ASC"
            Else
                'change it only if it's the same Sort Expression
                If Session("VieDataSortExpression") = e.SortExpression Then
                    If Session("ViewDataSortDirection") = "ASC" Then
                        Session("ViewDataSortDirection") = "DESC"
                    Else
                        Session("ViewDataSortDirection") = "ASC"
                    End If
                End If
            End If

            Session("VieDataSortExpression") = e.SortExpression

            tbl.DefaultView.Sort = e.SortExpression + " " + Session("ViewDataSortDirection")

            GridView1.DataSource = tbl
            GridView1.DataBind()
        End If
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        Session("Accepted") = False
        Support.FormManager.Close(Me)
    End Sub

End Class
