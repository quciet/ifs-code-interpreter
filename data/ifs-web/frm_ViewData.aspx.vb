Imports System.Drawing
Imports System.Data.SQLite

Partial Class frm_ViewData
    Inherits Page

    Private Sub frm_ViewData_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            RefreshData()
        End If
    End Sub

    Private Sub RefreshData()
        Dim db1 As SqliteConnection
        Dim db1Cmd As SQLiteCommand

        db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & Session("txtDatabaseFile") & ";", String))
        db1.Open()

        db1Cmd = db1.CreateCommand
        db1Cmd.CommandText = "SELECT * FROM [" & Session("txtTable") & "]"

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

        If Session("Accepted") = True Then
            CmdAccept.Visible = False
            CmdCancel.Text = "Close"
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

    Private Sub cmdDeleteZeroes_Click(sender As Object, e As EventArgs) Handles cmdDeleteZeroes.Click
        If Not (Session("ViewDataTbl") Is Nothing) Then
            Dim tbl As DataTable
            tbl = Session("ViewDataTbl")
            Dim sql As String

            Dim db1 As SqliteConnection
            Dim db1Cmd As SQLiteCommand

            db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & Session("txtDatabaseFile") & ";", String))
            db1.Open()

            db1Cmd = db1.CreateCommand

            'For each year
            For c = 2 To tbl.Columns.Count - 1
                'Change 0s into Nulls
                sql = "UPDATE [" & Session("txtTable") & "] SET [" & tbl.Columns(c).ColumnName & "] = null WHERE [" & tbl.Columns(c).ColumnName & "] = 0"
                db1Cmd.CommandText = sql
                db1Cmd.ExecuteNonQuery()
            Next

            sql = "UPDATE [" & Session("txtTable") & "] SET [Earliest] = null WHERE [Earliest] = 0"
            db1Cmd.CommandText = sql
            db1Cmd.ExecuteNonQuery()

            sql = "UPDATE [" & Session("txtTable") & "] SET [MostRecent] = null WHERE [MostRecent] = 0"
            db1Cmd.CommandText = sql
            db1Cmd.ExecuteNonQuery()

            db1Cmd.Dispose()
            'Refresh table
            db1Cmd = db1.CreateCommand
            db1Cmd.CommandText = "SELECT * FROM [" & Session("txtTable") & "]"

            Dim db1Rdr As SqliteDataReader
            db1Rdr = db1Cmd.ExecuteReader

            tbl = New DataTable
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

    Private Sub cmdDeleteNegatives_Click(sender As Object, e As EventArgs) Handles cmdDeleteNegatives.Click
        If Not (Session("ViewDataTbl") Is Nothing) Then
            Dim tbl As DataTable
            tbl = Session("ViewDataTbl")
            Dim sql As String

            Dim db1 As SqliteConnection
            Dim db1Cmd As SQLiteCommand

            db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & Session("txtDatabaseFile") & ";", String))
            db1.Open()

            db1Cmd = db1.CreateCommand

            'For each year
            For c = 2 To tbl.Columns.Count - 1
                'Change 0s into Nulls
                sql = "UPDATE [" & Session("txtTable") & "] SET [" & tbl.Columns(c).ColumnName & "] = null WHERE [" & tbl.Columns(c).ColumnName & "] < 0"
                db1Cmd.CommandText = sql
                db1Cmd.ExecuteNonQuery()
            Next

            db1Cmd.Dispose()
            'Refresh table
            db1Cmd = db1.CreateCommand
            db1Cmd.CommandText = "SELECT * FROM [" & Session("txtTable") & "]"

            Dim db1Rdr As SqliteDataReader
            db1Rdr = db1Cmd.ExecuteReader

            tbl = New DataTable
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

    Private Sub cmdMarkTransients_Click(sender As Object, e As EventArgs) Handles cmdMarkTransients.Click
        If Not (Session("ViewDataTbl") Is Nothing) Then
            Dim tbl As DataTable
            tbl = Session("ViewDataTbl")

            If tbl.Columns.Count <= 6 Then Exit Sub

            Dim MThreshold = Val(txtMarkThreshold.Text)
            If MThreshold = 0 Then MThreshold = 10

            For rcnt = 0 To GridView1.Rows.Count - 1
                For ccnt = 2 To GridView1.Rows(0).Cells.Count - 4 'exclude year, FIPScode. earliest, MostRecent and stop at 1 less than last col
                    Dim OldVal = Val(Trim(GridView1.Rows(rcnt).Cells(ccnt).Text))
                    Dim NewVal = Val(Trim(GridView1.Rows(rcnt).Cells(ccnt + 1).Text))

                    Dim DiffVal As Single
                    If OldVal = 0 Or NewVal = 0 Then
                        DiffVal = 0
                    Else
                        DiffVal = Math.Abs(100 * (NewVal - OldVal) / OldVal)
                    End If

                    If DiffVal > MThreshold Then
                        GridView1.Rows(rcnt).Cells(ccnt).BackColor = Color.Red
                        GridView1.Rows(rcnt).Cells(ccnt + 1).BackColor = Color.Red
                    End If

                Next
            Next
        End If
    End Sub

    Private Sub cmdViewMissing_Click(sender As Object, e As EventArgs) Handles cmdViewMissing.Click
        If cmdViewMissing.Text = "View Excluded" Then
            GridView1.Visible = False
            GrdShowMMC.Visible = True
            Dim Exc As List(Of String) = New List(Of String)
            For i = 0 To Session("MMCCntr") - 1
                Exc.Add(Session("MMC")(i))
            Next
            GrdShowMMC.DataSource = Exc
            GrdShowMMC.DataBind()
            'jrs 2023/05/04
            Dim Incl As Dictionary(Of String, String) = New Dictionary(Of String, String)
            For i = 0 To Session("MMCCntr2") - 1
                Incl.Add(Session("MMC2")(i, 1), Session("MMC2")(i, 2))
            Next
            GrdShowMMC2.DataSource = Incl
            GrdShowMMC2.DataBind()

            cmdViewMissing.Text = "Hide Excluded"
        Else
            GridView1.Visible = True
            GrdShowMMC.Visible = False
            cmdViewMissing.Text = "View Excluded"
            RefreshData()
        End If
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        'jrs 2024/03/01 
        Dim db1 As SqliteConnection
        Dim UpdTable As SqliteCommand
        Dim valtblCmd As SqliteCommand
        Dim valtbl As SqliteDataReader

        db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & Session("txtDatabaseFile") & ";", String)) '& "New=true;"
        db1.Open()

        UpdTable = db1.CreateCommand
        valtblCmd = db1.CreateCommand

        Using cmd2 = db1.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        valtblCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE name = '" & Session("txtTable") & "'"
        valtbl = valtblCmd.ExecuteReader
        valtbl.Read()

        If valtbl(0) > 0 Then
            valtbl.Close()
            'jrs 2024/03/01 added []
            'Delete existing table
            UpdTable.CommandText = "DROP TABLE [" & Session("txtTable") & "]"
            UpdTable.ExecuteNonQuery()
        Else
            valtbl.Close()
        End If

        db1.Close()

        Session("Accepted") = False

        'jrs 2026/07/23
        'Support.FormManager.Show(Me, "frm_ImportXLSNew.aspx")
        Support.FormManager.Show(Me, Session("ImportParent"))
    End Sub

    Protected Sub CmdAccept_Click(sender As Object, e As EventArgs) Handles CmdAccept.Click
        Session("Accepted") = True
        Session("frm_DatadictParent") = "frm_ViewData"
        'Support.FormManager.Show(Me, "frm_ImportXLSNew.aspx")
        Support.FormManager.Show(Me, "frm_Datadict.aspx")
    End Sub
End Class
