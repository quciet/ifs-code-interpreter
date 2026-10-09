Imports System.Drawing
Imports System.Data.SQLite
Imports ExcelDataReader
Imports System.IO

Partial Class frm_ViewDataBatch
    Inherits Page

    Dim ImpExstRowNoTranslation(250) As Integer

    Private Sub frm_ViewDataBatch_Load(sender As Object, e As EventArgs) Handles Me.Load

        cmdImportYears.Attributes.Add("onClick", "return checkToImportYears();")
        cmdImportCells.Attributes.Add("onClick", "return checkToImportCells();")

        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add() '==
        cswmGroup1.Caption = "Continue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = cswmWebMenu.Groups.Add() '==
        cswmGroup1.Caption = "File"

        cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Open Database with Imported Tables"
        cswmItem.Caption = "Merge Selected Tables to IFs Database"
        cswmItem.OnClick = "checkMergeToMaster();"

        cmdMarkTransientsNew.Attributes.Add("class", "wrap")
        cmdMarkTransientsOld.Attributes.Add("class", "wrap")
        cmdMarkBigChanges.Attributes.Add("class", "wrap")

        If Not Page.IsPostBack Then
            LoadGrdTableList()

            'jrs 2023/05/12
            chkMarkAll.Checked = True
            chkMarkAll_CheckedChanged(Me, New EventArgs)
        End If
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_MainMenu.aspx")
                    'Case "Open Database with Imported Tables"
            Case "Merge Selected Tables to IFs Database"
                mnuMergetoMaster_Click()
        End Select

    End Sub

    Private Sub GridViewNew_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridViewNew.Sorting
        If Not (Session("ViewDataBatchTblNewDD") Is Nothing) Then
            Dim tbl As DataTable
            tbl = Session("ViewDataBatchTblNewDD")

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

            'jrs 2023/07/28
            tbl.Columns(8).ColumnName = "______________________________________Definition______________________________________"
            tbl.Columns(9).ColumnName = "______________________________Extended_Source_Definition______________________________"
            tbl.Columns(12).ColumnName = "___Years___"
            tbl.Columns(13).ColumnName = "________________________________________Source________________________________________"
            tbl.Columns(14).ColumnName = "___________________________________Original_Source____________________________________"
            tbl.Columns(21).ColumnName = "___________________________________Name_in_Source_____________________________________"
            tbl.Columns(27).ColumnName = "___Country_Concordance___"
            tbl.Columns(29).ColumnName = "_____________________________________DisplayNotes_____________________________________"

            GridViewNew.DataSource = tbl
            GridViewNew.DataBind()

            'jrs 2023/08/01
            Dim colIdxs As List(Of Integer) = New List(Of Integer)
            colIdxs.Add(10) 'Definition
            colIdxs.Add(11) 'Extended Source Definition
            colIdxs.Add(15) 'Source
            colIdxs.Add(16) 'Original Source
            colIdxs.Add(23) 'Name in Source
            colIdxs.Add(31) 'Display Notes
            For i = 0 To GridViewNew.Rows.Count - 1
                For Each colIdx In colIdxs
                    GridViewNew.Rows(i).Cells(colIdx).ToolTip = GridViewNew.Rows(i).Cells(colIdx).Text
                    If GridViewNew.Rows(i).Cells(colIdx).Text.Length > 100 Then
                        GridViewNew.Rows(i).Cells(colIdx).Text = GridViewNew.Rows(i).Cells(colIdx).Text.Substring(0, 100)
                    End If
                Next
            Next
            If chkMarkAll.Checked Then
                chkMarkAll_CheckedChanged(Me, New EventArgs)
            End If
        End If
    End Sub


    Protected Sub GridViewNew_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridViewNew.SelectedIndexChanged
        If GridViewNew.Rows.Count = 0 Then Exit Sub
        'Display Content
        Session("TblNameImported") = GridViewNew.Rows(GridViewNew.SelectedIndex).Cells(4).Text
        'jrs 2024/11/14
        Session("TblNameImported") = Session("TblNameImported").replace("&amp;", "&")
        DisplayImportedData()
        Session("TblNew") = True
        'Display DataDict equivalent
        If GridViewNew.Rows(GridViewNew.SelectedIndex).Cells(3).Text.Replace("&nbsp;", "") > "" Then
            DisplayExistingData()
            Session("TblNew") = False
            PrepCountryGrid()
            PrepYearGrid()
        Else
            dbgridexistingdata.Visible = False
        End If
    End Sub

    Private Sub PrepCountryGrid()

        'list of tables with Code Values filled
        Dim db1 As SQLiteConnection
        Dim db1Cmd As SQLiteCommand

        db1 = New SQLiteConnection("DataSource=" + DataFileDir$ + "IFs.db;")
        db1.Open()

        db1Cmd = db1.CreateCommand


        'mti 01/25/2013 - Fix Import - start
        'Set ddlist = db1.OpenRecordset("Select Variable, [Code in Source], Source, [Decimal Places] From DataDict Where [Code in Source] <> '' ORDER BY Variable")

        Dim tblcnt As Integer
        Dim OpenCntry As Boolean
        OpenCntry = False


        '    For tblcnt = 0 To db1.TableDefs.Count - 1
        '        If UCase(db1.TableDefs(tblcnt).Name) = ImportConcordanceTblName$ Then
        'Set cntrylist = db1.OpenRecordset(ImportConcordanceTblName$)
        'OpenCntry = True
        '            Exit For
        '        End If
        '    Next

        Dim cntrylist As SQLiteDataReader
        If OpenCntry = False Then
            db1Cmd.CommandText = "SELECT Country FROM [Country Translation] ORDER BY Country"
            cntrylist = db1Cmd.ExecuteReader
        End If

        Dim tbl As DataTable = New DataTable

        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(tbl)
        tbl.Load(cntrylist)

        grd_Country.DataSource = tbl
        grd_Country.DataBind()

        'mti 01/25/2013 - Fix Import - end

        'If cntrylist.EOF And cntrylist.BOF Then
        '    MsgBox("Country Translation Table Empty")
        '    Exit Sub
        'End If


        Dim RecCn%

        RecCn% = 0


        'LenSource = Len(Trim(BatchSource))




        cntrylist.Close()
        cntrylist.Dispose()
        db1.Close()
        db1.Dispose()

        chkSelCountry.Checked = False


    End Sub


    Private Sub PrepYearGrid()
        'mti 01/25/2013 - Fix Import - start



        'list of tables with Code Values filled



        chkSelYear.Checked = False

        Dim Ycnt As Integer
        Dim TotYrCol As Integer

        TotYrCol = dbgridexistingdata.Rows(0).Cells.Count - 3 '

        Dim Years() As Integer
        ReDim Years(TotYrCol - 1)


        For Ycnt = 2 To TotYrCol - 1
            Years(Ycnt - 2) = CInt(Session("ExistingCols")(Ycnt).ToString)
        Next


        grd_Year.DataSource = Years
        grd_Year.DataBind()


    End Sub

    Protected Sub GridViewDet_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridViewDet.RowEditing
        'NewEditIndex property used to determine the index of the row being edited.  
        GridViewDet.EditIndex = e.NewEditIndex
        DisplayImportedData()
    End Sub

    Protected Sub GridViewDet_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewDet.RowUpdating
        'Finding the controls from Gridview for the row which Is going to update  
        Dim Ctry As String = GridViewDet.DataKeys(e.RowIndex).Values(0)
        Dim sql As String
        sql = "UPDATE [" & Session("TblNameImported") & "] SET "
        Dim sEarliest, sMostRecent As String
        sEarliest = "null"
        sMostRecent = "null"

        For i = 3 To GridViewDet.Rows(e.RowIndex).Cells.Count - 3
            If i > 3 Then
                sql = sql & ", "
            End If
            sql = sql & "[" & Session("ImportedCols")(i - 1).ToString & "] = "
            If TryCast(GridViewDet.Rows(e.RowIndex).Cells(i).Controls(0), TextBox).Text > "" Then
                sql = sql & TryCast(GridViewDet.Rows(e.RowIndex).Cells(i).Controls(0), TextBox).Text
                If sEarliest = "null" Then
                    sEarliest = TryCast(GridViewDet.Rows(e.RowIndex).Cells(i).Controls(0), TextBox).Text
                End If
                sMostRecent = TryCast(GridViewDet.Rows(e.RowIndex).Cells(i).Controls(0), TextBox).Text
            Else
                sql = sql & "null"
            End If
        Next
        sql = sql & ", Earliest = " & sEarliest
        sql = sql & ", MostRecent = " & sMostRecent
        sql = sql & " WHERE Country = """ & Ctry & """"

        Dim db1 As SQLiteConnection
        Dim db1Cmd As SQLiteCommand

        db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & "IFsDataImport.db;", String))
        db1.Open()

        db1Cmd = db1.CreateCommand
        db1Cmd.CommandText = sql

        db1Cmd.ExecuteNonQuery()

        db1Cmd.Dispose()
        db1.Close()
        db1.Dispose()

        'Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GridViewDet.EditIndex = -1
        DisplayImportedData()
    End Sub
    Private Sub DisplayImportedData()
        Dim db1 As SQLiteConnection
        Dim db1Cmd As SQLiteCommand

        db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & "IFsDataImport.db;", String))
        db1.Open()

        db1Cmd = db1.CreateCommand
        'jrs 2023/07/27 added []
        db1Cmd.CommandText = "SELECT * FROM [" & Session("TblNameImported") & "] ORDER BY Country"

        Dim db1Rdr As SQLiteDataReader
        db1Rdr = db1Cmd.ExecuteReader

        Dim tbl As DataTable = New DataTable

        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(tbl)
        tbl.Load(db1Rdr)

        GridViewDet.Visible = True
        GridViewDet.DataSource = tbl
        GridViewDet.DataBind()

        Session("ImportedCols") = tbl.Columns
        Session("ViewDataBatchTbl") = tbl

        db1Rdr.Close()
        db1Cmd.Dispose()
        db1.Close()
        db1.Dispose()
    End Sub

    Private Sub DisplayExistingData()
        Dim db1 As SQLiteConnection
        Dim db1Cmd As SQLiteCommand

        db1 = New SQLiteConnection("DataSource=" + DataFileDir$ & "IFsHistSeries.db;")
        db1.Open()

        db1Cmd = db1.CreateCommand
        db1Cmd.CommandText = "SELECT * FROM [" & Session("TblNameImported") & "] ORDER BY Country"

        Dim db1Rdr As SQLiteDataReader
        db1Rdr = db1Cmd.ExecuteReader

        Dim tbl As DataTable = New DataTable

        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(tbl)
        tbl.Load(db1Rdr)

        dbgridexistingdata.Visible = True
        dbgridexistingdata.DataSource = tbl
        dbgridexistingdata.DataBind()

        Session("ExistingCols") = tbl.Columns
        Session("ViewDataBatchExTbl") = tbl

        db1Rdr.Close()
        db1Cmd.Dispose()
        db1.Close()
        db1.Dispose()
    End Sub

    Protected Sub GridViewDet_RowCancelingEdit(sender As Object, e As EventArgs) Handles GridViewDet.RowCancelingEdit
        GridViewDet.EditIndex = -1
        DisplayImportedData()
    End Sub

    Protected Sub cmdUpdateDatadict_Click(sender As Object, e As EventArgs) Handles cmdUpdateDatadict.Click
        Session("frm_DatadictParent") = "frm_ViewDataBatch"


        Dim cbcol As Long

        If Session("MDBFileName") = "" Then Exit Sub

        If Session("TblNameImported") = "" Then Exit Sub

        If UCase(Mid(Session("TblNameImported"), 1, 6)) = "SERIES" Then
            Session("DataDictRow") = Mid(Session("TblNameImported"), 7) 'datadict form uses variable name for query; Cut off "Series" prefix
        Else
            Session("DataDictRow") = Session("TblNameImported")
        End If

        'frm_Datadict.UpdateYears = "" 'AllYear(dbgridimporteddata)
        Session("ORVarName") = ""
        Session("ORVarDefn") = ""
        Session("ORAggRule") = ""
        Session("ORDecPlaces") = "" 'keep as is
        'frm_Datadict.ORCountryList = ""
        Session("ORYearsCovered$") = ""
        Session("ORLastIFsUpDate") = ""

        Session("ORProvinces") = "NO"


        Session("ViewDataDictMDB") = Session("MDBFileName")
        Session("SaveDataDictMDB") = Session("MDBFileName")

        'Frm_ViewData.ImportFormVersion = ""

        'currentrow = grdTableList.RowSel
        'cbcol = grdTableList.CellBackColor

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Datadict.aspx")
    End Sub

    Protected Sub cmdDeleteZero_Click(sender As Object, e As EventArgs) Handles cmdDeleteZero.Click
        If Session("MDBFileName") = "" Then Exit Sub

        If Not (Session("TblNameImported") Is Nothing) Then
            Dim sql As String

            Dim db1 As SQLiteConnection
            Dim db1Cmd As SQLiteCommand

            db1 = New SQLiteConnection(CType("DataSource=" + Session("MDBFileName") & ";", String))
            db1.Open()

            db1Cmd = db1.CreateCommand

            'For each year
            For i = 3 To GridViewDet.Rows(0).Cells.Count - 3
                'Change 0s into Nulls
                sql = "UPDATE [" & Session("TblNameImported") & "] SET [" & Session("ImportedCols")(i - 1).ToString & "] = null WHERE [" & Session("ImportedCols")(i - 1).ToString & "] = 0"
                db1Cmd.CommandText = sql
                db1Cmd.ExecuteNonQuery()
            Next

            sql = "UPDATE [" & Session("TblNameImported") & "] SET [Earliest] = null WHERE [Earliest] = 0"
            db1Cmd.CommandText = sql
            db1Cmd.ExecuteNonQuery()

            sql = "UPDATE [" & Session("TblNameImported") & "] SET [MostRecent] = null WHERE [MostRecent] = 0"
            db1Cmd.CommandText = sql
            db1Cmd.ExecuteNonQuery()

            db1Cmd.Dispose()
            'Refresh table
            DisplayImportedData()
        End If
    End Sub


    Protected Sub cmdDeleteNegative_Click(sender As Object, e As EventArgs) Handles cmdDeleteNegative.Click
        If Session("MDBFileName") = "" Then Exit Sub

        If Not (Session("TblNameImported") Is Nothing) Then
            Dim sql As String

            Dim db1 As SQLiteConnection
            Dim db1Cmd As SQLiteCommand

            db1 = New SQLiteConnection(CType("DataSource=" + Session("MDBFileName") & ";", String))
            db1.Open()

            db1Cmd = db1.CreateCommand

            'For each year
            For i = 3 To GridViewDet.Rows(0).Cells.Count - 3
                'Change 0s into Nulls
                sql = "UPDATE [" & Session("TblNameImported") & "] SET [" & Session("ImportedCols")(i - 1).ToString & "] = null WHERE [" & Session("ImportedCols")(i - 1).ToString & "] < 0"
                db1Cmd.CommandText = sql
                db1Cmd.ExecuteNonQuery()
            Next

            sql = "UPDATE [" & Session("TblNameImported") & "] SET [Earliest] = null WHERE [Earliest] < 0"
            db1Cmd.CommandText = sql
            db1Cmd.ExecuteNonQuery()

            sql = "UPDATE [" & Session("TblNameImported") & "] SET [MostRecent] = null WHERE [MostRecent] < 0"
            db1Cmd.CommandText = sql
            db1Cmd.ExecuteNonQuery()

            db1Cmd.Dispose()
            'Refresh table
            DisplayImportedData()
        End If
    End Sub


    Protected Sub cmdMarkTransientsNew_Click(sender As Object, e As EventArgs) Handles cmdMarkTransientsNew.Click

        If Session("MDBFileName") = "" Then Exit Sub

        MarkTransients(GridViewDet)

    End Sub

    Private Sub MarkTransients(dgrid As GridView)
        Dim rcnt%, ccnt%
        Dim testVal$



        If dgrid.Rows(0).Cells.Count <= 6 Then Exit Sub 'only one year of data

        Dim MThreshold = Val(txtMarkThreshold.Text)
        If MThreshold = 0 Then MThreshold = 10
        txtMarkThreshold.Text = MThreshold

        For rcnt = 0 To dgrid.Rows.Count - 1
            For ccnt = 3 To dgrid.Rows(0).Cells.Count - 4 'exclude year, FIPScode. earliest, MostRecent and stop at 1 less than last col

                Dim OldVal = Val(Trim(dgrid.Rows(rcnt).Cells(ccnt).Text))

                Dim NewVal = Val(Trim(dgrid.Rows(rcnt).Cells(ccnt + 1).Text))

                Dim DiffVal As Single
                If OldVal = 0 Or NewVal = 0 Then
                    DiffVal = 0
                Else
                    If optPercent.Checked = True Then 'percentage
                        DiffVal = Math.Abs(100 * (NewVal - OldVal) / OldVal)
                    Else 'absolute
                        DiffVal = Math.Abs(NewVal - OldVal)
                    End If
                End If

                Dim PrevCol As Color
                If DiffVal > MThreshold Then
                    Select Case dgrid.Rows(rcnt).Cells(ccnt).BackColor
                        Case Color.LightPink
                            'do nothing
                            'dgrid.CellBackColor = QBColor(10)
                            PrevCol = Color.LightPink
                        Case Color.LightCyan
                            'do nothing
                            PrevCol = Color.LightCyan
                        Case Else
                            'dgrid.CellBackColor = QBColor(10)
                            PrevCol = Color.Black
                    End Select

                    Select Case PrevCol
                        Case Color.LightPink
                            dgrid.Rows(rcnt).Cells(ccnt + 1).BackColor = Color.LightCyan
                        Case Color.LightCyan
                            dgrid.Rows(rcnt).Cells(ccnt + 1).BackColor = Color.LightPink
                        Case Color.Black
                            dgrid.Rows(rcnt).Cells(ccnt + 1).BackColor = Color.LightCyan
                    End Select
                Else 'jrs 2024/02/16
                    dgrid.Rows(rcnt).Cells(ccnt + 1).BackColor = Color.White
                End If
            Next
        Next

    End Sub

    Private Sub MarkCrossingsAbove(dgrid As GridView)
        'mti 2015/09/11 data import
        Dim rcnt%, ccnt%
        Dim testVal$

        If dgrid.Rows.Count = 0 Then Exit Sub

        If dgrid.Rows(0).Cells.Count <= 6 Then Exit Sub 'only one year of data

        Dim MThreshold = Val(txtMarkThreshold.Text)
        If MThreshold = 0 Then MThreshold = 10
        txtMarkThreshold.Text = MThreshold

        For rcnt = 0 To dgrid.Rows.Count - 1
            For ccnt = 3 To dgrid.Rows(0).Cells.Count - 4 '4 'exclude year, FIPScode. earliest, MostRecent and stop at 1 less than last col

                Dim DatVal = Val(Trim(dgrid.Rows(rcnt).Cells(ccnt).Text))

                '        dgrid.col = ccnt + 1
                '        NewVal = Val(Trim(dgrid.Text))



                '        If OldVal = 0 Or NewVal = 0 Then
                '            DiffVal = 0
                '        Else
                '            If Opt_Marking(0).Value = True Then 'percentage
                '                DiffVal = Abs(100 * (NewVal - OldVal) / OldVal)
                '            Else 'absolute
                '                DiffVal = Abs(NewVal - OldVal)
                '            End If
                '        End If

                Dim PrevCol As Color

                If DatVal > MThreshold Then
                    Select Case dgrid.Rows(rcnt).Cells(ccnt).BackColor
                        Case Color.LightPink
                            'do nothing
                            'dgrid.CellBackColor = QBColor(10)
                            PrevCol = Color.LightPink
                        Case Color.LightCyan
                            'do nothing
                            PrevCol = Color.LightCyan
                        Case Else
                            'dgrid.CellBackColor = QBColor(10)
                            PrevCol = Color.Black
                    End Select

                    Select Case PrevCol
                        Case Color.LightPink
                            dgrid.Rows(rcnt).Cells(ccnt).BackColor = Color.LightCyan
                        Case Color.LightCyan
                            dgrid.Rows(rcnt).Cells(ccnt).BackColor = Color.LightPink
                        Case Color.Black
                            dgrid.Rows(rcnt).Cells(ccnt).BackColor = Color.LightCyan
                    End Select
                Else 'jrs 2024/02/16
                    dgrid.Rows(rcnt).Cells(ccnt).BackColor = Color.White
                End If
            Next
        Next
    End Sub

    Protected Sub cmdMarkTransientsOld_Click(sender As Object, e As EventArgs) Handles cmdMarkTransientsOld.Click
        If Session("MDBFileName") = "" Then Exit Sub
        If dbgridexistingdata.Rows.Count = 0 Then Exit Sub

        MarkTransients(dbgridexistingdata)

    End Sub

    Protected Sub cmdMarkAbove_Click(sender As Object, e As EventArgs) Handles cmdMarkAbove.Click

        If Session("MDBFileName") = "" Then Exit Sub
        'If TblNew Then Exit Sub

        MarkCrossingsAbove(GridViewDet)
        MarkCrossingsAbove(dbgridexistingdata)


    End Sub

    Private Sub MarkCrossingsBelow(dgrid As GridView)
        'mti 2015/09/11 data import
        Dim rcnt%, ccnt%
        Dim testVal$

        If dgrid.Rows.Count = 0 Then Exit Sub

        If dgrid.Rows(0).Cells.Count <= 6 Then Exit Sub 'only one year of data

        Dim MThreshold = Val(txtMarkThreshold.Text)
        If MThreshold = 0 Then MThreshold = 10
        txtMarkThreshold.Text = MThreshold

        For rcnt = 0 To dgrid.Rows.Count - 1
            For ccnt = 3 To dgrid.Rows(0).Cells.Count - 4 ' 4 'exclude year, FIPScode. earliest, MostRecent and stop at 1 less than last col

                Dim DatVal = Val(Trim(dgrid.Rows(rcnt).Cells(ccnt).Text))

                '        dgrid.col = ccnt + 1
                '        NewVal = Val(Trim(dgrid.Text))



                '        If OldVal = 0 Or NewVal = 0 Then
                '            DiffVal = 0
                '        Else
                '            If Opt_Marking(0).Value = True Then 'percentage
                '                DiffVal = Abs(100 * (NewVal - OldVal) / OldVal)
                '            Else 'absolute
                '                DiffVal = Abs(NewVal - OldVal)
                '            End If
                '        End If

                Dim PrevCol As Color

                If DatVal < MThreshold Then
                    Select Case dgrid.Rows(rcnt).Cells(ccnt).BackColor
                        Case Color.LightPink
                            'do nothing
                            'dgrid.CellBackColor = QBColor(10)
                            PrevCol = Color.LightPink
                        Case Color.LightCyan
                            'do nothing
                            PrevCol = Color.LightCyan
                        Case Else
                            'dgrid.CellBackColor = QBColor(10)
                            PrevCol = Color.Black
                    End Select

                    Select Case PrevCol
                        Case Color.LightPink
                            dgrid.Rows(rcnt).Cells(ccnt).BackColor = Color.LightCyan
                        Case Color.LightCyan
                            dgrid.Rows(rcnt).Cells(ccnt).BackColor = Color.LightPink
                        Case Color.Black
                            dgrid.Rows(rcnt).Cells(ccnt).BackColor = Color.LightCyan
                    End Select
                Else 'jrs 2024/02/16
                    dgrid.Rows(rcnt).Cells(ccnt).BackColor = Color.White
                End If

            Next
        Next
    End Sub

    Protected Sub cmdMarkBelow_Click(sender As Object, e As EventArgs) Handles cmdMarkBelow.Click
        If Session("MDBFileName") = "" Then Exit Sub
        'If TblNew Then Exit Sub

        MarkCrossingsBelow(GridViewDet)
        MarkCrossingsBelow(dbgridexistingdata)
    End Sub

    Private Sub GridTranslation()
        'mti 01/25/2013 - Fix Import - new proc

        Dim Rowcnt%, ExstRowCnt%
        Dim ImpExstMatch As Boolean

        If Session("ViewDataBatchSortDirection") = "DESC" Then
            Dim e As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            GridViewDet_Sorting(Me, e)
        End If

        If Session("ViewDataBatchExSortDirection") = "DESC" Then
            Dim e As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            dbgridexistingdata_Sorting(Me, e)
        End If



        For Rowcnt = 0 To GridViewDet.Rows.Count - 1

            ImpExstMatch = False
            For ExstRowCnt = 0 To dbgridexistingdata.Rows.Count - 1
                'jrs 2024/02/15 changed from 1 to 2, because we have added an Edit column at the beginin
                If Trim(GridViewDet.Rows(Rowcnt).Cells(2).Text) = Trim(dbgridexistingdata.Rows(ExstRowCnt).Cells(1).Text) Then
                    ImpExstRowNoTranslation(Rowcnt) = ExstRowCnt
                    ImpExstMatch = True
                    Exit For
                End If
            Next
            If Not ImpExstMatch Then
                ImpExstRowNoTranslation(Rowcnt) = -1 'jrs 2024/02/15 changed from 0 to -1
            End If
        Next
    End Sub

    Private Sub GridViewDet_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridViewDet.Sorting
        If Not (Session("ViewDataBatchTbl") Is Nothing) Then
            Dim tbl As DataTable
            tbl = Session("ViewDataBatchTbl")

            'By default, set the sort direction to ascending.
            Dim SortDirection As String = "ASC"
            If Session("ViewDataBatchSortDirection") = Nothing Then
                Session("ViewDataBatchSortDirection") = "ASC"
            Else
                'change it only if it's the same Sort Expression
                If Session("VieDataBatchSortExpression") = e.SortExpression Then
                    If Session("ViewDataBatchSortDirection") = "ASC" Then
                        Session("ViewDataBatchSortDirection") = "DESC"
                    Else
                        Session("ViewDataBatchSortDirection") = "ASC"
                    End If
                End If
            End If

            Session("VieDataBatchSortExpression") = e.SortExpression

            tbl.DefaultView.Sort = e.SortExpression + " " + Session("ViewDataBatchSortDirection")

            GridViewDet.DataSource = tbl
            GridViewDet.DataBind()
        End If
    End Sub

    Private Sub dbgridexistingdata_Sorting(sender As Object, e As GridViewSortEventArgs) Handles dbgridexistingdata.Sorting
        If Not (Session("ViewDataBatchExTbl") Is Nothing) Then
            Dim tbl As DataTable
            tbl = Session("ViewDataBatchExTbl")

            'By default, set the sort direction to ascending.
            Dim SortDirection As String = "ASC"
            If Session("ViewDataBatchExSortDirection") = Nothing Then
                Session("ViewDataBatchExSortDirection") = "ASC"
            Else
                'change it only if it's the same Sort Expression
                If Session("VieDataBatchExSortExpression") = e.SortExpression Then
                    If Session("ViewDataBatchExSortDirection") = "ASC" Then
                        Session("ViewDataBatchExSortDirection") = "DESC"
                    Else
                        Session("ViewDataBatchExSortDirection") = "ASC"
                    End If
                End If
            End If

            Session("VieDataBatchExSortExpression") = e.SortExpression

            tbl.DefaultView.Sort = e.SortExpression + " " + Session("ViewDataBatchExSortDirection")

            dbgridexistingdata.DataSource = tbl
            dbgridexistingdata.DataBind()
        End If

    End Sub

    Private Sub MarkInconsistency()



        Dim MThreshold = Val(txtMarkThreshold.Text)
        If MThreshold = 0 Then MThreshold = 10

        If Session("ViewDataBatchSortDirection") = "DESC" Then
            Dim e As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            GridViewDet_Sorting(Me, e)
        End If

        If Session("ViewDataBatchExSortDirection") = "DESC" Then
            Dim e As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            dbgridexistingdata_Sorting(Me, e)
        End If


        Dim CurrColorNo = Color.Gray
        Dim exsccnt = 3  'start at after FIPS Code
        'jrs 2024/02/15 changed the start from 3 to 4, we have an extra Edit column here
        For Impccnt = 4 To GridViewDet.Rows(0).Cells.Count - 2 'jrs 2023/08/28 changed from -1 to -2
            'jrs 2024/02/15 Dim ImpTblFld$ = Session("ImportedCols")(Impccnt - 1).ToString
            Dim ImpTblFld$ = Session("ImportedCols")(Impccnt - 2).ToString
            Dim ccnt = 0
            For ccnt = exsccnt To dbgridexistingdata.Rows(0).Cells.Count - 1
                Dim ExsTblFld$ = Session("ExistingCols")(ccnt - 1).ToString

                If UCase(Trim(ExsTblFld)) = UCase(Trim(ImpTblFld)) Then

                    'make sure adjacent rows don't have same coloring scheme
                    If CurrColorNo = Color.Gray Then
                        CurrColorNo = Color.Cyan
                    Else
                        CurrColorNo = Color.Gray
                    End If

                    'Fields match - start comparison
                    For Rowcnt = 0 To GridViewDet.Rows.Count - 1


                        'mti 01/25/2013 - Fix Import - start
                        If ImpExstRowNoTranslation(Rowcnt) <> -1 Then 'jrs 2024/02/15 changed from 0 to -1
                            'mti 01/25/2013 - Fix Import - end

                            Dim NewVal = Val(GridViewDet.Rows(Rowcnt).Cells(Impccnt).Text)
                            Dim OldVal = Val(dbgridexistingdata.Rows(ImpExstRowNoTranslation(Rowcnt)).Cells(ccnt).Text)

                            Dim DiffVal As Single
                            If OldVal = 0 Or NewVal = 0 Then
                                DiffVal = 0
                            Else
                                If optPercent.Checked = True Then 'percentage
                                    DiffVal = Math.Abs(100 * (NewVal - OldVal) / OldVal)
                                Else  'absolute
                                    DiffVal = Math.Abs(NewVal - OldVal)
                                End If
                            End If

                            If DiffVal > MThreshold Then
                                GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = CurrColorNo
                                dbgridexistingdata.Rows(ImpExstRowNoTranslation(Rowcnt)).Cells(ccnt).BackColor = CurrColorNo
                            Else 'jrs 2024/02/16
                                GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = Color.White
                                dbgridexistingdata.Rows(ImpExstRowNoTranslation(Rowcnt)).Cells(ccnt).BackColor = Color.White
                            End If

                            'mti 01/25/2013 - Fix Import - start
                        End If
                        'mti 01/25/2013 - Fix Import - end

                    Next
                End If
                If Val(ExsTblFld) > Val(ImpTblFld) Then
                    'column with a later year
                    Exit For
                End If

            Next
            exsccnt = ccnt
            'tdef.Fields.Append tdef.CreateField(ImpTblFld$, dbDouble)
        Next





    End Sub


    Protected Sub cmdMarkBigChanges_Click(sender As Object, e As EventArgs) Handles cmdMarkBigChanges.Click
        If Session("MDBFileName") = "" Then Exit Sub
        If Session("TblNew") Then Exit Sub
        'mti 01/25/2013 - Fix Import - start
        'If dbgridimporteddata.Rows <> dbgridexistingdata.Rows Then
        '    MsgBox "Data Tables Not Comparable."
        '    Exit Sub
        'End If

        Call GridTranslation()
        'mti 01/25/2013 - Fix Import - end
        MarkInconsistency()

    End Sub

    'Procedure to convert to JavaScript format to load HighCharts
    Private Sub PackJS()
        Dim pCOUNTE As Integer = 0
        Dim oldVals As List(Of Integer) = New List(Of Integer)
        For r = 0 To dbgridexistingdata.Rows.Count - 1
            If TryCast(dbgridexistingdata.Rows(r).Cells(0).Controls(1), CheckBox).Checked Then
                pCOUNTE = pCOUNTE + 1
                oldVals.Add(r)
            End If
        Next
        Dim newVals As List(Of Integer) = New List(Of Integer)
        For r = 0 To GridViewDet.Rows.Count - 1
            If TryCast(GridViewDet.Rows(r).Cells(1).Controls(1), CheckBox).Checked Then
                pCOUNTE = pCOUNTE + 1
                newVals.Add(r)
            End If
        Next

        'jrs 2011/09/20
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr
        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''," & vbCr
        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim DimCount As Integer = 0

        Dim sAxisXLabel As String
        sAxisXLabel = "Year"
        Dim sAxisYLabel As String
        sAxisYLabel = ""

        'sJscript2 = sJscript2 & "subtitle: {" & vbCr
        'sJscript2 = sJscript2 & "text:       'Source: WorldClimate.com'," & vbCr
        'sJscript2 = sJscript2 & "x: -20" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'Load Years
        '----------
        'sJscript2 = sJscript2 & "categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'," & vbCr
        'sJscript2 = sJscript2 & "'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']" & vbCr
        'jrs 2015/09/11 removing categories from Bar chart, problematic for multiple series
        'it wasn't showing all bars in the first and last categories
        sJscript2 = sJscript2 & ", categories: ["
        Dim i, iFirstYear, iLastYear As Integer
        iFirstYear = 9999
        If newVals.Count > 0 Then
            iFirstYear = CInt(Session("ImportedCols")(2).ToString)
        End If
        If oldVals.Count > 0 AndAlso CInt(Session("ExistingCols")(2).ToString) < iFirstYear Then
            iFirstYear = CInt(Session("ExistingCols")(2).ToString)
        End If
        iLastYear = -9999
        If newVals.Count > 0 Then
            iLastYear = CInt(Session("ImportedCols")(GridViewDet.Rows(0).Cells.Count - 5).ToString)
        End If
        If oldVals.Count > 0 AndAlso CInt(Session("ExistingCols")(dbgridexistingdata.Rows(0).Cells.Count - 5).ToString) > iLastYear Then
            iLastYear = CInt(Session("ExistingCols")(dbgridexistingdata.Rows(0).Cells.Count - 5).ToString)
        End If

        Session("FirstYear") = iFirstYear
        Session("DisYear") = iLastYear - iFirstYear + 1

        For i = iFirstYear To iLastYear
            If i > iFirstYear Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & "'" & i.ToString & "'"
        Next
        sJscript2 = sJscript2 & "]" & vbCr

        If iLastYear - iFirstYear + 1 > 21 Then
            sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr
        End If


        sJscript2 = sJscript2 & ", min: " & iFirstYear.ToString & vbCr
        sJscript2 = sJscript2 & ", max: " & iLastYear.ToString & vbCr

        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        'needed to have first series at the bottom, for example in Education Atteinment Display, Non-Primary at the bottom
        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr
        'sJscript2 = sJscript2 & "text:       '" & getYAxisLabel() & "'," & vbCr
        'sJscript2 = sJscript2 & "style: {" & vbCr
        'sJscript2 = sJscript2 & "fontSize:   '20px'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr


        'jrs 2015/09/08
        sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
        sJscript2 = sJscript2 & "align:      'center'," & vbCr
        'cdv 2021/02/02 flip legend to match intended order
        sJscript2 = sJscript2 & "reversed:      true," & vbCr
        If pCOUNTE >= 120 Then
            sJscript2 = sJscript2 & "enabled:        false," & vbCr 'cdv 2022/03/23 Hide legend when we have more than 120 subsets. This makes our functionality match VB6
        End If
        sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
        sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "y: 40, " & vbCr 'jrs 2016/04/26 changed from 20 to 40 'avoid overlap 
        sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim sZeros As String = ""
        For i = 1 To pCOUNTE
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            If i <= oldVals.Count Then
                sJscript2 = sJscript2 & PlotColToJSSeries(dbgridexistingdata, oldVals(i - 1))
            Else
                sJscript2 = sJscript2 & PlotColToJSSeries(GridViewDet, newVals(i - oldVals.Count - 1))
            End If
        Next
        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        'jam 2023/11/01
        ''sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr



        sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".css({"
        sJscript2 = sJscript2 & "cursor: 'pointer'"
        sJscript2 = sJscript2 & "})"
        sJscript2 = sJscript2 & ".add();" & vbCr



        sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub

    Private Function PlotColToJSSeries(ByRef grd As GridView, ByVal i As Integer) As String

        Dim sJscript As String = ""
        sJscript = sJscript & "{name: '" & getSeriesName(grd, i) & "'," & vbCr

        Dim ii As Integer
        'jrs 2023/05/10
        If grd.ID = "dbgridexistingdata" Then
            ii = i + 5
        Else
            ii = i
        End If

        Dim sColor As String = ""
        Select Case ii Mod 9
            Case 0
                sColor = "Black"
            Case 1
                sColor = "Lime"
            Case 2
                sColor = "Blue"
            Case 3
                sColor = "Red"
            Case 4
                sColor = "Orange"
            Case 5 'blue violet
                sColor = "RGB(138,43,226)"
            Case 6 'aqua 
                sColor = "RGB(0,255,255)"
            Case 7 'deep pink
                sColor = "RGB(255,20,147)"
            Case 8 'medium spring green
                sColor = "RGB(0,250,154)"
        End Select
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim Diff As Single, Min As Single, Max As Single
        Min = 10 ^ 10
        Max = -10 ^ 10
        Dim iNumDec As Integer = 0

        Dim idx As Integer = grd.Rows(i).Cells.Count - 1
        If IsNumeric(grd.Rows(i).Cells(idx).Text) Then
            If Math.Abs(CType(grd.Rows(i).Cells(idx).Text, Double)) < 1 Then 'cdv 2022/06/30
                iNumDec = 3
            ElseIf Math.Abs(Val(grd.Rows(i).Cells(idx).Text)) < 10 Then
                iNumDec = 2
            ElseIf Math.Abs(Val(grd.Rows(i).Cells(idx).Text)) < 100 Then
                iNumDec = 1
            End If
        Else
            iNumDec = 2
        End If

        'jrs 2016/03/23
        Dim iStart As Integer, iFirstYear As Integer, Diff2 As Integer
        If Session("FirstYear") Mod 5 = 0 Then
            iFirstYear = Session("FirstYear")
            iStart = 4
        Else
            Diff2 = Session("FirstYear") Mod 5
            iFirstYear = Session("FirstYear") + 5 - Diff2
            iStart = 5 - Diff2 + 4
        End If

        'jrs 2023/04/28
        Dim ExtraCols As Integer = 3
        If grd.ID = "dbgridexistingdata" Then
            iStart = iStart - 1
            ExtraCols = 2
        End If

        For j = iStart To Session("DisYear") + ExtraCols
            'jrs 2024/02/16 added j < grd.rows(i).Cells.Count-2
            If j < grd.Rows(i).Cells.Count - 2 AndAlso IsNumeric(grd.Rows(i).Cells(j).Text) Then
                sJscript = sJscript & "[" & CStr(Session("FirstYear") + j - iStart) & ", "
                sJscript = sJscript & CStr(Math.Round(CType(grd.Rows(i).Cells(j).Text, Double), iNumDec)) & "]" 'cdv 2022/06/30

                If j < Session("DisYear") + 3 Then
                    sJscript = sJscript & ", "
                End If
                'ElseIf Session("GType") = "BAR" Then
                '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
            End If
            'CDV 2021/10/22 ****END****
        Next


        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Private Function getSeriesName(ByRef grd As GridView, ByVal ii As Integer) As String
        Dim d1, Legend As String
        d1 = LTrim(RTrim(grd.Rows(ii).Cells(2).Text))
        'jrs 2023/04/28
        If d1.Length = 3 Then
            d1 = LTrim(RTrim(grd.Rows(ii).Cells(1).Text))
        End If

        Legend = d1
        If grd.ID = "GridViewDet" Then
            Legend = Legend & " (New)"
        Else
            Legend = Legend & " (Old)"
        End If

        getSeriesName = PrepareForHTML(Legend) 'jrs 2016/01/04 added PrepareForHTML
    End Function

    Protected Sub cmdPlot_Click(sender As Object, e As EventArgs) Handles cmdPlot.Click
        If cmdPlot.Text = "Plot Selected" Then
            PackJS()
            lblJS.Visible = True
            GridViewNew.Visible = False
            dbgridexistingdata.Visible = False
            'jrs 2023/04/28
            divExistingData.Style.Value = "height:0px;"
            divYear.Style.Value = "height:0px;"
            divCountry.Style.Value = "height:0px;"
            'jrs 2023/07/31
            divNewData.Style.Value = "height:0px;"
            divNew.Style.Value = "height:0px;"
            chkSelYear.Visible = False
            chkSelCountry.Visible = False
            ChkSelMissingYears.Visible = False
            chkSelMissingCountries.Visible = False

            grd_Year.Visible = False
            grd_Country.Visible = False
            GridViewDet.Visible = False
            pnlMarks.Visible = False
            Panel3.Visible = False
            cmdImportYears.Visible = False
            cmdImportCells.Visible = False
            cmdPlot.Text = "Hide Plot"
            cmdUpdateDatadict.Visible = False
            cmdDeleteZero.Visible = False
            cmdDeleteNegative.Visible = False
        Else
            lblJS.Visible = False
            GridViewNew.Visible = True
            dbgridexistingdata.Visible = True
            'jrs 2023/04/28
            divExistingData.Style.Value = "height:250px; overflow:scroll;"
            divYear.Style.Value = "height:115px; overflow:scroll;"
            divCountry.Style.Value = "height:115px; overflow:scroll;"
            'jrs 2023/07/31
            divNewData.Style.Value = "height:250px; overflow:scroll;"
            divNew.Style.Value = "height:250px; overflow:scroll;"
            chkSelYear.Visible = True
            chkSelCountry.Visible = True
            ChkSelMissingYears.Visible = True
            chkSelMissingCountries.Visible = True

            grd_Year.Visible = True
            grd_Country.Visible = True
            GridViewDet.Visible = True
            pnlMarks.Visible = True
            Panel3.Visible = True
            cmdImportYears.Visible = True
            cmdImportCells.Visible = True
            cmdPlot.Text = "Plot Selected"
            cmdUpdateDatadict.Visible = True
            cmdDeleteZero.Visible = True
            cmdDeleteNegative.Visible = True
        End If
    End Sub

    Protected Sub cmdDisplayMissing_Click(sender As Object, e As EventArgs) Handles cmdDisplayMissing.Click
        If Session("MDBFileName") = "" Then Exit Sub
        If Session("TblNew") Then Exit Sub
        'mti 01/25/2013 - Fix Import - start
        'If dbgridimporteddata.Rows <> dbgridexistingdata.Rows Then
        '    MsgBox "Data Tables Not Comparable."
        '    Exit Sub
        'End If

        Call GridTranslation()
        'mti 01/25/2013 - Fix Import - end
        MarkMissing()


        'cmdDisplayMissing.BackColor = &H80000002 ' &H8000000F


    End Sub

    Private Sub MarkMissing()

        Dim MThreshold = Val(txtMarkThreshold.Text)
        If MThreshold = 0 Then MThreshold = 10

        If Session("ViewDataBatchSortDirection") = "DESC" Then
            Dim e As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            GridViewDet_Sorting(Me, e)
        End If

        If Session("ViewDataBatchExSortDirection") = "DESC" Then
            Dim e As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            dbgridexistingdata_Sorting(Me, e)
        End If

        Dim CurrColorNo = Color.Gray
        Dim exsccnt = 3  'start at after FIPS Code
        For Impccnt = 3 To GridViewDet.Rows(0).Cells.Count - 1 - 2 'jrs 2024/08/16 removing the 2 extra columns for edit and select checkbox
            Dim ImpTblFld$ = Session("ImportedCols")(Impccnt - 1).ToString
            Dim ccnt = 0
            For ccnt = exsccnt To dbgridexistingdata.Rows(0).Cells.Count - 1
                Dim ExsTblFld$ = Session("ExistingCols")(ccnt - 1).ToString

                If UCase(Trim(ExsTblFld)) = UCase(Trim(ImpTblFld)) Then

                    'make sure adjacent rows don't have same coloring scheme
                    If CurrColorNo = Color.Gray Then
                        CurrColorNo = Color.LightYellow
                    Else
                        CurrColorNo = Color.Gray
                    End If

                    'Fields match - start comparison
                    For Rowcnt = 0 To GridViewDet.Rows.Count - 1


                        'mti 01/25/2013 - Fix Import - start
                        If ImpExstRowNoTranslation(Rowcnt) <> 0 Then
                            'mti 01/25/2013 - Fix Import - end

                            If chkMarkNonZeroOnlysMissing.Checked Then

                                If GridViewDet.Rows(Rowcnt).Cells(Impccnt).Text = "" And Val(Trim(dbgridexistingdata.Rows(ImpExstRowNoTranslation(Rowcnt)).Cells(ccnt).Text)) <> 0 Then
                                    GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = CurrColorNo
                                Else 'jrs 2024/02/16
                                    GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = Color.White
                                End If

                            Else 'mark zero also as missing

                                If GridViewDet.Rows(Rowcnt).Cells(Impccnt).Text = "" And Trim(dbgridexistingdata.Rows(ImpExstRowNoTranslation(Rowcnt)).Cells(ccnt).Text) <> "" Then
                                    GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = CurrColorNo
                                Else 'jrs 2024/02/16
                                    GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = Color.White
                                End If

                            End If

                            'mti 01/25/2013 - Fix Import - start
                        End If
                        'mti 01/25/2013 - Fix Import - end

                    Next

                End If
                If Val(ExsTblFld) > Val(ImpTblFld) Then
                    'column with a later year
                    Exit For
                End If

            Next
            exsccnt = ccnt
            'tdef.Fields.Append tdef.CreateField(ImpTblFld$, dbDouble)
        Next
    End Sub
    Private Sub MarkNew()



        Dim MThreshold = Val(txtMarkThreshold.Text)
        If MThreshold = 0 Then MThreshold = 10

        If Session("ViewDataBatchSortDirection") = "DESC" Then
            Dim e As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            GridViewDet_Sorting(Me, e)
        End If

        If Session("ViewDataBatchExSortDirection") = "DESC" Then
            Dim e As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            dbgridexistingdata_Sorting(Me, e)
        End If

        Dim CurrColorNo = Color.Gray
        Dim exsccnt = 3  'start at after FIPS Code
        For Impccnt = 3 To GridViewDet.Rows(0).Cells.Count - 1
            Dim ImpTblFld$ = Session("ImportedCols")(Impccnt - 1).ToString
            Dim ccnt = 0
            For ccnt = exsccnt To dbgridexistingdata.Rows(0).Cells.Count - 1
                Dim ExsTblFld$ = Session("ExistingCols")(ccnt - 1).ToString

                If UCase(Trim(ExsTblFld)) = UCase(Trim(ImpTblFld)) Then

                    'make sure adjacent rows don't have same coloring scheme
                    If CurrColorNo = Color.Gray Then
                        CurrColorNo = Color.LightYellow
                    Else
                        CurrColorNo = Color.Gray
                    End If

                    'Fields match - start comparison
                    For Rowcnt = 0 To GridViewDet.Rows.Count - 1

                        'mti 01/25/2013 - Fix Import - start
                        If ImpExstRowNoTranslation(Rowcnt) <> 0 Then
                            'dbgridexistingdata.row = ImpExstRowNoTranslation(Rowcnt) ' rowcnt
                            'mti 01/25/2013 - Fix Import - end

                            If chkMarkNonZeroOnlysMissing.Checked Then

                                If dbgridexistingdata.Rows(ImpExstRowNoTranslation(Rowcnt)).Cells(ccnt).Text = "" And Val(Trim(GridViewDet.Rows(Rowcnt).Cells(Impccnt).Text)) <> 0 Then
                                    GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = CurrColorNo
                                Else 'jrs 2024/02/16
                                    GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = Color.White
                                End If

                            Else 'mark zero also as missing

                                If dbgridexistingdata.Rows(ImpExstRowNoTranslation(Rowcnt)).Cells(ccnt).Text = "" And Trim(GridViewDet.Rows(Rowcnt).Cells(Impccnt).Text) <> "" Then
                                    GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = CurrColorNo
                                Else 'jrs 2024/02/16
                                    GridViewDet.Rows(Rowcnt).Cells(Impccnt).BackColor = Color.White
                                End If
                            End If

                            'mti 01/25/2013 - Fix Import - start
                        End If
                        'mti 01/25/2013 - Fix Import - end

                    Next

                End If
                If Val(ExsTblFld) > Val(ImpTblFld) Then
                    'column with a later year
                    Exit For
                End If

            Next
            exsccnt = ccnt
            'tdef.Fields.Append tdef.CreateField(ImpTblFld$, dbDouble)
        Next

    End Sub


    Protected Sub cmdDisplayNew_Click(sender As Object, e As EventArgs) Handles cmdDisplayNew.Click
        If Session("MDBFileName") = "" Then Exit Sub
        If Session("TblNew") Then Exit Sub
        'mti 01/25/2013 - Fix Import - start
        'If dbgridimporteddata.Rows <> dbgridexistingdata.Rows Then
        '    MsgBox "Data Tables Not Comparable."
        '    Exit Sub
        'End If

        Call GridTranslation()
        'mti 01/25/2013 - Fix Import - end
        MarkNew()


        'cmdDisplayMissing.BackColor = &H80000002 ' &H8000000F



    End Sub

    Protected Sub cmdImportYears_Click(sender As Object, e As EventArgs) Handles cmdImportYears.Click
        Dim existingfldname$



        'mti 5/20/2013 - 186 table : backup - start
        Dim idxCountry As Index
        'mti 5/20/2013 - 186 table : backup - end



        If Session("MDBFileName") = "" Then Exit Sub

        Dim Ttl2Merge = 0
        For rcnt = 0 To grd_Year.Rows.Count - 1
            If TryCast(grd_Year.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked Then
                Ttl2Merge = Ttl2Merge + 1
            End If
        Next

        If Ttl2Merge = 0 Then

            Dim sMsg = "<script>alert('Nothing Selected.');</script>"

            Response.Write(sMsg)

            Exit Sub
        End If

        Call GridTranslation()

        Dim db As SQLiteConnection
        Dim dbCmd As SQLiteCommand

        db = New SQLiteConnection(CType("DataSource=" + Session("MDBFileName") & ";", String))
        db.Open()

        dbCmd = db.CreateCommand

        'Delete all fields but Country and FIPS Code  - backup table here - just in case there is an error
        dbCmd.CommandText = "CREATE TABLE tmp1 AS SELECT Country, FIPS_CODE FROM [" & Session("TblNameImported") & "];"
        dbCmd.ExecuteNonQuery()

        dbCmd.CommandText = "DROP TABLE [" & Session("TblNameImported") & "];"
        dbCmd.ExecuteNonQuery()

        dbCmd.CommandText = "ALTER TABLE tmp1 RENAME TO [" & Session("TblNameImported") & "];"
        dbCmd.ExecuteNonQuery()

        'mti 2013/07/08
        '    If Build186 Then

        '        'mti 5/20/2013 - 186 table : MDBFileName + "186" - start
        '        sqlstr = "Select * INTO " + TblNameImported + "186_backup From " + TblNameImported + "186;"
        '        db.Execute sqlstr

        'Set tdef = db.TableDefs(TblNameImported + "186_backup")
        'With tdef
        '    Set idxCountry = .CreateIndex("Country")
        '    With idxCountry
        '                .Fields.Append.CreateField("Country")
        '            End With
        '            .Indexes.Append idxCountry
        '    Set idxCountry = Nothing
        '    .Indexes.Refresh
        '        End With
        'Set tdef = Nothing
        'Set tdef186 = db.TableDefs(TblNameImported + "186") 'db.CreateTableDef(TblName)

        ''Delete all fields but Country and FIPS Code
        'FldCount% = tdef186.Fields.Count
        '        For FCount% = FldCount% To 3 Step -1
        '            tdef186.Fields.DELETE tdef186.Fields(FCount% - 1).Name
        'Next

        '    End If


        Dim sql As String
        sql = "ALTER TABLE [" & Session("TblNameImported") & "] ADD COLUMN "

        'column count of existing data grid; Impccnt is column count of imported (new) data grid; 3 is the first column containing years
        Dim exsccnt = 3
        Dim Fields As List(Of String) = New List(Of String)
        Fields.Add("Country")
        Fields.Add("FIPS_CODE")
        For ccnt = 0 To grd_Year.Rows.Count - 1

            If TryCast(grd_Year.Rows(ccnt).Cells(0).Controls(1), CheckBox).Checked Then
                Dim YearNo = Val(grd_Year.Rows(ccnt).Cells(1).Text)  'this is the year to blend

                For Impccnt = exsccnt To GridViewDet.Rows(0).Cells.Count - 1

                    If Val(Session("ImportedCols")(Impccnt - 1).ToString) > 0 And Val(Session("ImportedCols")(Impccnt - 1).ToString) < YearNo Then
                        dbCmd.CommandText = sql & "[" & Session("ImportedCols")(Impccnt - 1).ToString & "] DOUBLE(53)"
                        dbCmd.ExecuteNonQuery()
                        Fields.Add(Session("ImportedCols")(Impccnt - 1).ToString)
                        'If Build186 Then tdef186.Fields.Append tdef186.CreateField(Trim(GridViewDet.Text), dbDouble)

                        'mti 2015/06/25
                        exsccnt = Impccnt + 1 'should advance imported column pointer as imported column is added

                    ElseIf Val(Session("ImportedCols")(Impccnt - 1).ToString) > 0 Then  'YearNo or later Year
                        dbCmd.CommandText = sql & "[" & Session("ImportedCols")(Impccnt - 1).ToString * "] DOUBLE(53)"
                        dbCmd.ExecuteNonQuery()
                        Fields.Add(Session("ImportedCols")(Impccnt - 1).ToString)
                        'If Build186 Then tdef186.Fields.Append tdef186.CreateField(Trim(grd_Year.Text), dbDouble)

                        'mti 2015/06/25
                        'exsccnt = Impccnt + 1 'should not advance imported column pointer as imported column is not taken
                        If Val(Session("ImportedCols")(Impccnt - 1).ToString) = YearNo Then exsccnt = Impccnt + 1 'advance pointer for an overlap

                        Exit For
                    End If

                Next

            End If

        Next

        'add remaining year columns (if any from new data)
        For Impccnt = exsccnt To GridViewDet.Rows(0).Cells.Count - 1
            If Val(Session("ImportedCols")(Impccnt - 1).ToString) > 0 Then  'YearNo or later Year
                dbCmd.CommandText = sql & "[" & Session("ImportedCols")(Impccnt - 1).ToString * "] DOUBLE(53)"
                dbCmd.ExecuteNonQuery()
                Fields.Add(Session("ImportedCols")(Impccnt - 1).ToString)
                'If Build186 Then tdef186.Fields.Append tdef186.CreateField(Trim(GridViewDet.Text), dbDouble)
            End If

        Next

        dbCmd.CommandText = sql & "Earliest DOUBLE(53)"
        dbCmd.ExecuteNonQuery()
        dbCmd.CommandText = sql & "MostRecent DOUBLE(53)"
        dbCmd.ExecuteNonQuery()
        Fields.Add("Earliest")
        Fields.Add("MostRecent")

        'mti 2013/07/08
        'If Build186 Then
        '    tdef186.Fields.Append tdef186.CreateField("Earliest", dbDouble)
        '    tdef186.Fields.Append tdef186.CreateField("MostRecent", dbDouble)
        'End If

        'Set valtbl = db.OpenRecordset(TblNameImported)
        'valtbl.Index = "Country"

        'mti 2013/07/08
        'If Build186 Then

        '    Set ValTbl186 = db.OpenRecordset(TblNameImported + "186")
        '    ValTbl186.Index = "Country"

        '    Set ValTbl186_backup = db.OpenRecordset(TblNameImported + "186_backup")
        '    ValTbl186_backup.Index = "Country"

        'End If

        If Session("ViewDataBatchSortDirection") = "DESC" Then
            Dim ee As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            GridViewDet_Sorting(Me, ee)
        End If

        If Session("ViewDataBatchExSortDirection") = "DESC" Then
            Dim ee As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            dbgridexistingdata_Sorting(Me, ee)
        End If


        'mti 2013/07/08
        'If Build186 Then
        '    ValTbl186.MoveFirst
        '    ValTbl186_backup.MoveFirst
        'End If

        Dim tblRowCnt = 1
        Dim Earliest As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
        Dim MostRecent As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
        Dim sCountryName As String

        Do While tblRowCnt <= GridViewDet.Rows.Count
            If ImpExstRowNoTranslation(tblRowCnt) <> 0 Then 'row matches

                For fldcnt = 2 To Fields.Count - 3

                    Dim FldName = Fields(fldcnt)
                    Dim DatVal$ = ""

                    Dim MergeYearFld = ""

                    'Code needs to be like this because of country name matching issues
                    For YearCnt = 1 To grd_Year.Rows.Count - 1
                        If TryCast(grd_Year.Rows(YearCnt).Cells(0).Controls(1), CheckBox).Checked Then
                            If FldName = Trim(grd_Year.Rows(YearCnt).Cells(1).Text) Then
                                MergeYearFld = FldName
                                Exit For
                            End If

                        End If
                    Next

                    If MergeYearFld = FldName Then 'take from existing

                        For ccnt = 3 To dbgridexistingdata.Columns.Count - 3

                            Dim ExsTblFld$ = Session("ExistingCols")(ccnt - 1).ToString

                            If Val(ExsTblFld$) > Val(FldName) Then 'past database year field
                                Exit For
                            End If

                            If ExsTblFld$ = FldName Then 'column from existing table

                                'mti 01/25/2013 - Fix Import - start

                                'dbgridexistingdata.row = tblRowCnt

                                DatVal$ = dbgridexistingdata.Rows(ImpExstRowNoTranslation(tblRowCnt)).Cells(ccnt).Text
                                sCountryName = dbgridexistingdata.Rows(ImpExstRowNoTranslation(tblRowCnt)).Cells(1).Text
                                Exit For
                            End If

                        Next



                    Else  'take from new/imported table

                        For Impccnt = 3 To GridViewDet.Rows(0).Cells.Count - 3
                            Dim ImpTblFld$ = Session("ImportedCols")(Impccnt - 1).ToString
                            If Val(ImpTblFld$) > Val(FldName) Then 'past database year field
                                Exit For
                            End If
                            If ImpTblFld$ = FldName Then 'column in new table; take that data

                                'mti 4/9/2013 - Import Batch Changes - start
                                '                    DatVal$ = Val(GridViewDet.Text)  'mti 4/9/2013 - 0s are going from this
                                DatVal$ = GridViewDet.Rows(tblRowCnt).Cells(Impccnt).Text  'mti 4/9/2013 - 0s are going from this
                                sCountryName = GridViewDet.Rows(tblRowCnt).Cells(1).Text
                                'mti 4/9/2013 - Import Batch Changes - end

                                Exit For
                            End If

                        Next

                    End If


                    'If Not YearFound Then ' take from existing
                    'End If

                    'valtbl.Edit
                    'ValTbl186.Edit

                    sql = "UPDATE [" & Session("TblNameImported") & "] SET [" & Fields(fldcnt) & "] = "

                    If DatVal = "" Then
                        sql = sql & "Null"
                        'ValTbl186.Fields(fldcnt) = Null
                    Else
                        sql = sql & DatVal
                        If Not Earliest.ContainsKey(sCountryName) Then
                            Earliest.Add(sCountryName, Val(DatVal))
                            MostRecent.Add(sCountryName, Val(DatVal))
                        Else
                            MostRecent(sCountryName) = Val(DatVal)
                        End If
                        'ValTbl186.Fields(fldcnt) = Val(DatVal)
                    End If

                    dbCmd.CommandText = sql & " WHERE Country = """ & sCountryName & """"
                    dbCmd.ExecuteNonQuery()
                    'ValTbl186.Update

                    'mti 2013/07/08
                    'If Build186 Then
                    '    ValTbl186.Edit
                    '    If DatVal = "" Then
                    '        ValTbl186.Fields(fldcnt) = Null
                    '    Else
                    '        ValTbl186.Fields(fldcnt) = Val(DatVal)
                    '    End If
                    '    ValTbl186.Update

                    'End If

                Next

                'valtbl.MoveNext

                'mti 2013/07/08
                'If Build186 Then
                '    ValTbl186.MoveNext
                '    ValTbl186_backup.MoveNext


                '    If Not valtbl.EOF Then
                '        Do While Trim(UCase(valtbl.Fields(0))) <> Trim(UCase(ValTbl186.Fields(0)))  'check if country name matches

                '            'mti 5/20/2013 - 186 table : backup - start
                '            If Trim(UCase(ValTbl186_backup.Fields(0))) = Trim(UCase(ValTbl186.Fields(0))) Then
                '                'bring 186 data from backup ; if row matches
                '                ValTbl186.Edit
                '                LastFldUpdated = 2
                '                For fld186backcnt = 2 To ValTbl186_backup.Fields.Count - 3
                '                    Fld186backName = ValTbl186_backup.Fields(fld186backcnt).Name
                '                    'Fld186backValue = ValTbl186_backup.Fields(fld186backcnt).Value
                '                    For fld186cnt = LastFldUpdated To ValTbl186.Fields.Count - 3
                '                        If Fld186backName = ValTbl186.Fields(fld186cnt).Name Then
                '                            ValTbl186.Fields(fld186cnt).value = ValTbl186_backup.Fields(fld186backcnt).value
                '                            LastFldUpdated = fld186cnt + 1
                '                            Exit For
                '                        End If
                '                    Next
                '                Next
                '                ValTbl186.Update
                '            End If

                '            ValTbl186_backup.MoveNext
                '            'mti 5/20/2013 - 186 table : backup - end

                '            ValTbl186.MoveNext ' do not need to move more than 1 in reality; also expecting first counry is same in 183 and 186

                '            'note May 16 2013 - this means 3 countries are losing taken data- need to change this



                '        Loop
                '    End If

                'End If ' BUILD186

            End If
            tblRowCnt = tblRowCnt + 1

            If Earliest.ContainsKey(sCountryName) Then
                sql = "UPDATE [" & Session("TblNameImported") & "] SET Earliest = " & Earliest(sCountryName).ToString
                dbCmd.CommandText = sql & " WHERE Country = """ & sCountryName & """"
                dbCmd.ExecuteNonQuery()
            End If

            If MostRecent.ContainsKey(sCountryName) Then
                sql = "UPDATE [" & Session("TblNameImported") & "] SET MostRecent = " & MostRecent(sCountryName).ToString
                dbCmd.CommandText = sql & " WHERE Country = """ & sCountryName & """"
                dbCmd.ExecuteNonQuery()
            End If
        Loop

        'mti 2013/07/08
        'If Build186 Then
        '    AddEarliestData ValTbl186, tdef186
        '    AddMostRecentData ValTbl186, tdef186
        '    Set tdef186 = Nothing
        '    ValTbl186.Close
        '    Set ValTbl186 = Nothing

        'End If


        db.Close()
        db.Dispose()





        DisplayImportedData()


        Dim CurrColorNo As Color = Color.Gray

        'Code needs to be like this because of country name matching issues
        For YearCnt = 1 To grd_Year.Rows.Count - 1
            If TryCast(grd_Year.Rows(YearCnt).Cells(0).Controls(1), CheckBox).Checked Then

                For gcnt = 1 To GridViewDet.Rows(0).Cells.Count - 1

                    If Session("ImportedCols")(gcnt - 1).ToString = grd_Year.Rows(YearCnt).Cells(1).Text Then

                        If CurrColorNo = Color.LightGreen Then
                            CurrColorNo = Color.LightYellow
                        Else
                            CurrColorNo = Color.LightGreen
                        End If

                        For rcnt = 1 To GridViewDet.Rows.Count - 1
                            GridViewDet.Rows(rcnt).Cells(gcnt).BackColor = CurrColorNo
                        Next

                        Exit For
                    End If
                Next

            End If
        Next

        If Trim(GridViewNew.Rows(0).Cells(3).Text) <> "" Then
            DisplayExistingData()
        End If


    End Sub


    Protected Sub cmdImportCells_Click(sender As Object, e As EventArgs) Handles cmdImportCells.Click
        'mti 4/9/2013 - whole procedure

        'assumptions:

        'expecting first country is same in 183 and 186
        'expecting both grids have same number of rows (provinces won't match)

        Dim existingfldname$




        If Session("MDBFileName") = "" Then Exit Sub

        Dim Ttl2Merge = 0
        For rcnt = 1 To grd_Country.Rows.Count - 1
            If TryCast(grd_Country.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked Then
                Ttl2Merge = Ttl2Merge + 1
            End If
        Next

        If Ttl2Merge = 0 Then
            Dim sMsg = "<script>alert('Nothing Selected.');</script>"

            Response.Write(sMsg)
            Exit Sub
        End If



        Call GridTranslation()

        'showing blended points in highlight
        MarkMissing()


        If Session("ViewDataBatchSortDirection") = "DESC" Then
            Dim ee As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            GridViewDet_Sorting(Me, ee)
        End If

        If Session("ViewDataBatchExSortDirection") = "DESC" Then
            Dim ee As GridViewSortEventArgs = New GridViewSortEventArgs("Country", "ASC")
            dbgridexistingdata_Sorting(Me, ee)
        End If

        'mti 2013/07/08
        '    If Build186 Then
        '        sqlstr = "Select * INTO " + TblNameImported + "186_backup From " + TblNameImported + "186;"
        '        db.Execute sqlstr

        'Set tdef = db.TableDefs(TblNameImported + "186_backup")
        'With tdef
        '    Set idxCountry = .CreateIndex("Country")
        '    With idxCountry
        '                .Fields.Append.CreateField("Country")
        '            End With
        '            .Indexes.Append idxCountry
        '    Set idxCountry = Nothing
        '    .Indexes.Refresh
        '        End With
        'Set tdef = Nothing

        'Set ValTbl186 = db.OpenRecordset(TblNameImported + "186")
        'ValTbl186.Index = "Country"

        'Set ValTbl186_backup = db.OpenRecordset(TblNameImported + "186_backup")
        'ValTbl186_backup.Index = "Country"

        '        ValTbl186.MoveFirst
        '        ValTbl186_backup.MoveFirst
        '    End If

        Dim db As SQLiteConnection
        Dim dbCmd As SQLiteCommand

        db = New SQLiteConnection(CType("DataSource=" + Session("MDBFileName") & ";", String))
        db.Open()

        dbCmd = db.CreateCommand

        'point blending won't overwrite existing data (column blending will)

        Dim tblRowCnt = 1
        Dim Earliest As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
        Dim MostRecent As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
        Dim EarliestNew As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
        Dim MostRecentNew As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
        Dim sql As String

        Do While tblRowCnt <= GridViewDet.Rows.Count

            'jrs 2024/08/16 Dim CntryName$ = GridViewDet.Rows(tblRowCnt).Cells(1).Text
            Dim CntryName$ = GridViewDet.Rows(tblRowCnt - 1).Cells(2).Text
            Dim MergeCountry = ""
            For rcnt = 1 To grd_Country.Rows.Count - 1
                If UCase(Trim(grd_Country.Rows(rcnt - 1).Cells(1).Text)) = UCase(Trim(CntryName$)) Then
                    If TryCast(grd_Country.Rows(rcnt - 1).Cells(0).Controls(1), CheckBox).Checked Then
                        MergeCountry = Trim(grd_Country.Rows(rcnt - 1).Cells(1).Text)
                        Exit For
                    End If
                End If
            Next

            If MergeCountry <> "" Then


                If ImpExstRowNoTranslation(tblRowCnt) <> 0 Then 'row matches
                    For fldcnt = 4 To GridViewDet.Rows(0).Cells.Count - 4
                        Dim FldName = Session("ImportedCols")(fldcnt - 2).ToString
                        Dim DatValExst$ = ""
                        Dim DatValImp$ = ""
                        For ccnt = 3 To dbgridexistingdata.Rows(0).Cells.Count - 1
                            'jrs 2024/08/16 Dim ExsTblFld$ = Session("ExistingCols")(ccnt - 3).ToString
                            Dim ExsTblFld$ = Session("ExistingCols")(ccnt - 1).ToString
                            If ExsTblFld$ = FldName Then 'column from existing table

                                'jrs 2024/08/16 DatValExst$ = dbgridexistingdata.Rows(ImpExstRowNoTranslation(tblRowCnt)).Cells(ccnt).Text
                                DatValExst$ = dbgridexistingdata.Rows(ImpExstRowNoTranslation(tblRowCnt) - 1).Cells(ccnt).Text
                                'jrs 2024/08/16
                                If DatValExst$ = "&nbsp;" Then DatValExst$ = ""

                                Exit For
                            End If
                        Next
                        Dim ImpTblFld$
                        For Impccnt = 4 To GridViewDet.Rows(0).Cells.Count - 1
                            'jrs 2024/08/16 ImpTblFld$ = Session("ImportedCols")(Impccnt - 3).ToString
                            ImpTblFld$ = Session("ImportedCols")(Impccnt - 2).ToString
                            If Val(ImpTblFld$) > Val(FldName) Then 'past database year field
                                Exit For
                            End If
                            If ImpTblFld$ = FldName Then 'column in new table; take that data

                                'mti 4/9/2013 - Import Batch Changes - start
                                '                    DatVal$ = Val(gridviewdet.Text)  'mti 4/9/2013 - 0s are going from this
                                'jrs 2024/08/16 DatValImp$ = GridViewDet.Rows(tblRowCnt).Cells(Impccnt).Text  'mti 4/9/2013 - 0s are going from this
                                DatValImp$ = GridViewDet.Rows(tblRowCnt - 1).Cells(Impccnt).Text  'mti 4/9/2013 - 0s are going from this
                                'mti 4/9/2013 - Import Batch Changes - end
                                'jrs 2024/08/16
                                If DatValImp$ = "&nbsp;" Then DatValImp$ = ""
                                Exit For
                            End If
                        Next

                        'ValTbl186.Edit

                        sql = "UPDATE [" & Session("TblNameImported") & "] SET [" & ImpTblFld$ & "] = "

                        If DatValImp = "" Then
                            If DatValExst = "" Then
                                sql = sql & "Null"
                                'ValTbl186.Fields(fldcnt) = Null
                            Else
                                sql = sql & DatValExst
                                'ValTbl186.Fields(fldcnt) = Val(DatValExst)
                                GridViewDet.Rows(tblRowCnt - 1).Cells(fldcnt).Text = DatValExst
                                If Not Earliest.ContainsKey(MergeCountry) Then
                                    Earliest.Add(MergeCountry, Val(DatValExst))
                                    'jrs 2024/08/16
                                    GridViewDet.Rows(tblRowCnt - 1).Cells(GridViewDet.Rows(0).Cells.Count - 2).Text = DatValExst
                                    MostRecent.Add(MergeCountry, Val(DatValExst))
                                Else
                                    MostRecent(MergeCountry) = Val(DatValExst)
                                    If MostRecentNew.ContainsKey(MergeCountry) Then
                                        MostRecentNew.Remove(MergeCountry)
                                    End If
                                End If
                            End If
                        Else
                            sql = sql & DatValImp
                            If Not Earliest.ContainsKey(MergeCountry) Then
                                Earliest.Add(MergeCountry, Val(DatValImp))
                                'jrs 2024/08/16
                                GridViewDet.Rows(tblRowCnt - 1).Cells(GridViewDet.Rows(0).Cells.Count - 2).Text = DatValExst
                                MostRecent.Add(MergeCountry, Val(DatValImp))
                                EarliestNew.Add(MergeCountry, tblRowCnt)
                                MostRecentNew.Add(MergeCountry, tblRowCnt)
                            Else
                                MostRecent(MergeCountry) = Val(DatValImp)
                                If MostRecentNew.ContainsKey(MergeCountry) Then
                                    MostRecentNew(MergeCountry) = tblRowCnt
                                Else
                                    MostRecentNew.Add(MergeCountry, tblRowCnt)
                                End If
                            End If

                            'ValTbl186.Fields(fldcnt) = Val(DatValImp)
                        End If

                        dbCmd.CommandText = sql & " WHERE Country = """ & MergeCountry & """"
                        dbCmd.ExecuteNonQuery()
                        'ValTbl186.Update

                        'mti 2013/07/08
                        'If Build186 Then
                        '    ValTbl186.Edit
                        '    If DatValImp = "" Then
                        '        If DatValExst = "" Then
                        '            ValTbl186.Fields(fldcnt) = Null
                        '        Else
                        '            ValTbl186.Fields(fldcnt) = Val(DatValExst)
                        '        End If
                        '    Else
                        '        ValTbl186.Fields(fldcnt) = Val(DatValImp)
                        '    End If
                        '    ValTbl186.Update
                        'End If

                    Next

                    'valtbl.MoveNext

                    'mti 2013/07/08
                    'If Build186 Then
                    '    ValTbl186.MoveNext
                    '    ValTbl186_backup.MoveNext



                    '    If Not valtbl.EOF Then
                    '        Do While Trim(UCase(valtbl.Fields(0))) <> Trim(UCase(ValTbl186.Fields(0)))  'check if country name matches

                    '            'mti 5/20/2013 - 186 table : backup - start
                    '            If Trim(UCase(ValTbl186_backup.Fields(0))) = Trim(UCase(ValTbl186.Fields(0))) Then
                    '                'bring 186 data from backup ; if row matches
                    '                ValTbl186.Edit
                    '                LastFldUpdated = 2
                    '                For fld186backcnt = 2 To ValTbl186_backup.Fields.Count - 3
                    '                    Fld186backName = ValTbl186_backup.Fields(fld186backcnt).Name
                    '                    'Fld186backValue = ValTbl186_backup.Fields(fld186backcnt).Value
                    '                    For fld186cnt = LastFldUpdated To ValTbl186.Fields.Count - 3
                    '                        If Fld186backName = ValTbl186.Fields(fld186cnt).Name Then
                    '                            ValTbl186.Fields(fld186cnt).value = ValTbl186_backup.Fields(fld186backcnt).value
                    '                            LastFldUpdated = fld186cnt + 1
                    '                            Exit For
                    '                        End If
                    '                    Next
                    '                Next
                    '                ValTbl186.Update
                    '            End If

                    '            ValTbl186_backup.MoveNext
                    '            'mti 5/20/2013 - 186 table : backup - end

                    '            ValTbl186.MoveNext ' do not need to move more than 1 in reality; also expecting first counry is same in 183 and 186

                    '            'note May 16 2013 - this means 3 countries are losing taken data- need to change this



                    '        Loop
                    '    End If

                    'End If  'build186


                End If

            Else
                'valtbl.MoveNext
            End If
            tblRowCnt = tblRowCnt + 1

            If Earliest.ContainsKey(MergeCountry) Then
                sql = "UPDATE [" & Session("TblNameImported") & "] SET Earliest = " & Earliest(MergeCountry).ToString
                dbCmd.CommandText = sql & " WHERE Country = """ & MergeCountry & """"
                dbCmd.ExecuteNonQuery()
            End If

            If MostRecent.ContainsKey(MergeCountry) Then
                sql = "UPDATE [" & Session("TblNameImported") & "] SET MostRecent = " & MostRecent(MergeCountry).ToString
                dbCmd.CommandText = sql & " WHERE Country = """ & MergeCountry & """"
                dbCmd.ExecuteNonQuery()
            End If

        Loop

        'mti 5/21/2013 - Do not merge colummns with blending missing points - start

        'Set tdef = db.TableDefs(TblNameImported) 'db.CreateTableDef(TblName)


        'mti 5/21/2013 - Do not merge colummns with blending missing points - end


        'mti 2013/07/08
        '        If Build186 Then

        '    Set tdef186 = db.TableDefs(TblNameImported + "186") 'db.CreateTableDef(TblName)
        '    AddEarliestData ValTbl186, tdef186
        '    AddMostRecentData ValTbl186, tdef186

        'End If



        'Mark earliest/most recent changes
        tblRowCnt = 1

        For Each ctry In EarliestNew
            GridViewDet.Rows(ctry.Value - 1).Cells(GridViewDet.Rows(0).Cells.Count - 2).BackColor = Color.LightGreen
        Next

        For Each ctry In MostRecentNew
            GridViewDet.Rows(ctry.Value - 1).Cells(GridViewDet.Rows(0).Cells.Count - 1).BackColor = Color.LightYellow
        Next

        'Do While Not valtbl.EOF

        '    GridViewDet.row = tblRowCnt
        '    GridViewDet.col = 1

        '    If Trim(GridViewDet.Text) = Trim(valtbl.Fields(0).value) Then

        '        GridViewDet.col = GridViewDet.cols - 2 'earliest
        '        If Not IsNull(valtbl.Fields(valtbl.Fields.Count - 2).value) Then
        '            If GridViewDet.Text <> Trim(CStr(valtbl.Fields(valtbl.Fields.Count - 2).value)) Then
        '                GridViewDet.Text = valtbl.Fields(valtbl.Fields.Count - 2).value
        '                GridViewDet.CellBackColor = QBColor(10)
        '            End If
        '        End If

        '        GridViewDet.col = GridViewDet.cols - 1 'most recent
        '        If Not IsNull(valtbl.Fields(valtbl.Fields.Count - 1).value) Then
        '            If GridViewDet.Text <> Trim(CStr(valtbl.Fields(valtbl.Fields.Count - 1).value)) Then
        '                GridViewDet.Text = valtbl.Fields(valtbl.Fields.Count - 1).value
        '                GridViewDet.CellBackColor = QBColor(14)
        '            End If
        '        End If

        '    End If



        '    tblRowCnt = tblRowCnt + 1

        'Loop



        'mti 2013/07/08
        'If Build186 Then
        '    Set tdef186 = Nothing
        '    ValTbl186.Close
        '    Set ValTbl186 = Nothing

        '    'mti 5/20/2013 - 186 table : backup - start
        '    ValTbl186_backup.Close
        '    Set ValTbl186_backup = Nothing
        '    'delete backup
        '    db.TableDefs.DELETE TblNameImported + "186_backup"
        '    'mti 5/20/2013 - 186 table : backup - end


        '        End If


        db.Close()
        db.Dispose()



        'DisplayImportedData

        'DisplayExistingData

        If Trim(GridViewNew.Rows(0).Cells(3).Text) <> "" Then
            DisplayExistingData()
        End If


    End Sub

    Protected Sub chkSelYear_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelYear.CheckedChanged

        ' If OptCountrySel(0).Value = True Then Exit Sub 'importing all countries
        If grd_Year.Rows.Count <= 2 Then
            chkSelYear.Checked = False
            Exit Sub
        End If

        If Not chkSelYear.Checked Then
            For rcnt = 1 To grd_Year.Rows.Count - 1
                TryCast(grd_Year.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = False
            Next
        Else
            For rcnt = 1 To grd_Year.Rows.Count - 1
                TryCast(grd_Year.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = True
            Next
        End If
    End Sub

    Protected Sub ChkSelMissingYears_CheckedChanged(sender As Object, e As EventArgs) Handles ChkSelMissingYears.CheckedChanged
        Dim rcnt%, excnt%

        ' If OptCountrySel(0).Value = True Then Exit Sub 'importing all countries
        If grd_Year.Rows.Count <= 2 Then
            ChkSelMissingYears.Checked = False
            Exit Sub
        End If


        If Not ChkSelMissingYears.Checked Then
            For rcnt = 1 To grd_Year.Rows.Count - 1
                TryCast(grd_Year.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = False
            Next
        Else

            For rcnt = 1 To grd_Year.Rows.Count - 1
                TryCast(grd_Year.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = True

                For excnt = 1 To GridViewDet.Rows(0).Cells.Count - 1
                    If Session("ImportedCols")(excnt - 1).ToString = grd_Year.Rows(rcnt).Cells(1).Text Then
                        TryCast(grd_Year.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = False
                        Exit For
                    End If
                Next

            Next
        End If

    End Sub


    Protected Sub chkSelCountry_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelCountry.CheckedChanged
        ' If OptCountrySel(0).Value = True Then Exit Sub 'importing all countries
        If grd_Country.Rows.Count <= 2 Then
            chkSelCountry.Checked = False
            Exit Sub
        End If

        If Not chkSelCountry.Checked Then
            For rcnt = 1 To grd_Country.Rows.Count - 1
                TryCast(grd_Country.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = False
            Next
        Else
            For rcnt = 1 To grd_Country.Rows.Count - 1
                TryCast(grd_Country.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = True
            Next
        End If

    End Sub

    Protected Sub chkSelMissingCountries_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelMissingCountries.CheckedChanged
        Dim rcnt%, excnt%, excolcnt%
        Dim HasData As Boolean

        ' If OptCountrySel(0).Value = True Then Exit Sub 'importing all countries
        If grd_Country.Rows.Count <= 2 Then
            chkSelMissingCountries.Checked = False
            Exit Sub
        End If


        If Not chkSelMissingCountries.Checked Then
            For rcnt = 1 To grd_Country.Rows.Count - 1
                TryCast(grd_Country.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = False
            Next
        Else

            For rcnt = 1 To grd_Country.Rows.Count - 1
                TryCast(grd_Country.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = False


                For excnt = 1 To GridViewDet.Rows.Count - 1

                    'If grd_Country.Text = "Bahrain" Then Stop

                    If GridViewDet.Rows(excnt).Cells(2).Text = grd_Country.Rows(rcnt).Cells(1).Text Then

                        HasData = False

                        For excolcnt = 3 To GridViewDet.Rows(0).Cells.Count - 1

                            If Trim(GridViewDet.Rows(excnt).Cells(excolcnt).Text) <> "" Then
                                HasData = True
                                Exit For
                            End If

                        Next

                        If Not HasData Then
                            TryCast(grd_Country.Rows(rcnt).Cells(0).Controls(1), CheckBox).Checked = True
                        End If

                        Exit For
                    End If
                Next

            Next
        End If


    End Sub

    Private Sub mnuMergetoMaster_Click()

        If txtConfirmMerge.Value = "False" Then Exit Sub

        Dim TableImport As String
        Dim TableCreated$, TableRenamed$
        Dim Rowcnt As Integer

        Dim dbSource As SQLiteConnection
        Dim dbDest As SQLiteConnection
        Dim dbDestDatadict As SQLiteConnection

        Dim dbSourceCmd, dbSourceCmd2 As SQLiteCommand
        Dim dbDestCmd, dbDestCmd2 As SQLiteCommand
        Dim dbDestDatadictCmd As SQLiteCommand
        'jrs 2024/08/10
        Dim dbDestDatadictRdrCmd As SQLiteCommand


        Dim ValTblSource As SQLiteDataReader
        Dim DicTblDest As SQLiteDataReader
        Dim DicTblSource As SQLiteDataReader
        Dim tdef As DataTable

        'Dim Mergeto186 As Boolean
        'Dim dbDest186 As Database
        'Dim ValTblSource186 As Recordset
        'Dim tdef186 As TableDef

        'Mergeto186 = True

        Dim tcnt%

        If Session("MDBFileName") = "" Then Exit Sub

        Try
            dbSource = New SQLiteConnection(CType("DataSource=" + Session("MDBFileName") & ";", String))
            dbSource.Open()

            dbSourceCmd = dbSource.CreateCommand
            dbSourceCmd2 = dbSource.CreateCommand
            'jrs 2024/08/09 added order by variable, assumes grid is also sorted
            dbSourceCmd.CommandText = "SELECT * FROM DataDict ORDER BY [Variable]"
            DicTblSource = dbSourceCmd.ExecuteReader

            dbDest = New SQLiteConnection("DataSource=" + DataFileDir$ + "IFsHistSeries.db;")
            dbDest.Open()
            dbDestCmd = dbDest.CreateCommand
            dbDestCmd2 = dbDest.CreateCommand

            'If Build186 Then Set dbDest186 = DBEngine(0).OpenDatabase(DataFileDir$ + "IFsHistSeries186.Mdb", False, False)


            'Set dbDest = DBEngine(0).CreateDatabase


            dbDestDatadict = New SQLiteConnection("DataSource=" + DataFileDir$ + "Datadict.db;")
            dbDestDatadict.Open()
            dbDestDatadictCmd = dbDestDatadict.CreateCommand
            'jrs 2024/08/10
            dbDestDatadictRdrCmd = dbDestDatadict.CreateCommand

            Dim Updcnt% = 0
            Dim InsertCnt% = 0

            'jrs 2024/08/10
            Dim TableList4Reset As List(Of String) = New List(Of String)
            'jrs 2025/03/10
            Dim sFieldsNotToChange As List(Of String) = New List(Of String)
            sFieldsNotToChange.Add("UsedInPreprocessor")
            sFieldsNotToChange.Add("UsedInPreprocessorFileName")
            sFieldsNotToChange.Add("UsedInHistAnalog")
            sFieldsNotToChange.Add("UsedInFunctions")

            For Rowcnt = 0 To GridViewNew.Rows.Count - 1

                If TryCast(GridViewNew.Rows(Rowcnt).Cells(1).Controls(1), CheckBox).Checked Then


                    'mti 2015/09/08 -start
                    TableRenamed$ = ""
                    TableCreated$ = ""
                    'mti 2015/09/08 -end

                    If Mid(UCase(Trim(GridViewNew.Rows(Rowcnt).Cells(4).Text)), 1, 6) = "SERIES" Then
                        'jrs 2023/04/18 removing selection option
                        'jrs 2023/04/18 If TryCast(GridViewNew.Rows(Rowcnt).Cells(0).Controls(1), CheckBox).Checked Then 'It is selected for merging?

                        TableImport = GridViewNew.Rows(Rowcnt).Cells(4).Text

                        'Set ValTblSource = dbSource.OpenRecordset("Series" & TableImport)
                        dbSourceCmd2.CommandText = "SELECT * FROM [" & TableImport & "]"
                        ValTblSource = dbSourceCmd2.ExecuteReader


                        If Trim(GridViewNew.Rows(Rowcnt).Cells(3).Text) <> "&nbsp;" Then

                            'delete existing table from Master Database
                            'dbDest.TableDefs.DELETE ("Series" + Trim(GridViewNew.Text))

                            dbDestCmd.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND UPPER(name) = 'SERIES" & GridViewNew.Rows(Rowcnt).Cells(2).Text.Trim.ToUpper & "'"
                            Dim rddbDestTblDefs = dbDestCmd.ExecuteReader

                            If rddbDestTblDefs.Read Then
                                'must be a formula denominator table (may be for WDI)

                                'mti 2015/09/08 -start
                                'dbDest.TableDefs.DELETE Trim(GridViewNew.Text) 'tdef.Name

                                TableRenamed$ = "Series" & Trim(GridViewNew.Rows(Rowcnt).Cells(3).Text) + "Temp"
                                dbDestCmd2.CommandText = "ALTER TABLE [Series" & GridViewNew.Rows(Rowcnt).Cells(3).Text & "] RENAME TO [" & TableRenamed$ & "]"
                                dbDestCmd2.ExecuteNonQuery()
                                rddbDestTblDefs.Close()
                                'mti 2015/09/08 - end

                            End If


                            'dbDest.TableDefs.DELETE (Trim(GridViewNew.Text))



                            'mti 2015/09/08 -start
                            'delete existing row from Master DataDadict
                            'dbDestDatadict.Execute ("Delete from Datadict where Variable = '" + Trim(GridViewNew.Text) + "';")

                            'dbDestDatadict.Execute ("Delete from Datadict where Table = '" + Trim(GridViewNew.Text) + "';")
                            'mti 2015/09/08 - end

                            Updcnt% = Updcnt% + 1
                        Else
                            InsertCnt% = InsertCnt% + 1
                        End If

                        'Set tdef = dbDest.CreateTableDef("Series" & TableImport)
                        Dim sCreate As String = "CREATE TABLE """ & TableImport & """ (""Country"" VARCHAR(255), ""FIPS_CODE"" VARCHAR(255)"

                        'mti 2015/09/08 - start
                        'this was taking all but first two and last two fields; that will be a problem when Earliest and Most Recent are not yet added
                        'For i2 = 2 To ValTblSource.Fields.Count - 3
                        For i2 = 2 To ValTblSource.FieldCount - 3
                            If Val(ValTblSource.GetName(i2)) > 0 Then  'non zero year value
                                sCreate = sCreate & ", """ & ValTblSource.GetName(i2) & """ DOUBLE(53)"
                            End If
                        Next
                        'mti 2015/09/08 - end

                        sCreate = sCreate & ", ""Earliest"" DOUBLE(53)"
                        sCreate = sCreate & ", ""MostRecent"" DOUBLE(53)"

                        'mti February 2011 Infarstructure - end
                        sCreate = sCreate & ")"

                        dbDestCmd2.CommandText = sCreate
                        dbDestCmd2.ExecuteNonQuery()

                        TableCreated$ = TableImport



                        'qsql = " INSERT INTO [Series" + TableImport + "] SELECT * From [Series" + TableImport + "] In '" + MDBFileName + "';"
                        'jrs 2023/06/30 only attach the source once
                        Dim qsql As String
                        If Updcnt% + InsertCnt% < 2 Then
                            qsql = "ATTACH DATABASE '" & Session("MDBFileName") & "' AS source_db;"
                            dbDestCmd.CommandText = qsql
                            dbDestCmd.ExecuteNonQuery()
                        End If

                        qsql = " INSERT INTO [" + TableImport + "] SELECT * From source_db.[" + TableImport + "];"
                        dbDestCmd.CommandText = qsql
                        dbDestCmd.ExecuteNonQuery()


                        '    If Build186 Then
                        '        'Set ValTblSource186 = dbSource.OpenRecordset(TableImport + "186")

                        '        For tcnt = 0 To dbDest186.TableDefs.Count - 1
                        '            If UCase(dbDest186.TableDefs(tcnt).Name) = UCase(TableImport + "186") Then
                        '                dbDest186.TableDefs.DELETE(TableImport + "186")
                        '                dbDest186.TableDefs.Refresh
                        '                Exit For
                        '            End If
                        '        Next

                        '        qsql = "Select * INTO [" + TableImport + "186] From [" + TableImport + "186] In '" + MDBFileName + "';"
                        '        dbDest186.Execute qsql
                        'Set tdef186 = dbDest.CreateTableDef(TableImport + "186")
                        ''create Index on Country
                        'With tdef186
                        '    Set idxCountry = .CreateIndex("Country")
                        '    With idxCountry
                        '                .Fields.Append.CreateField("Country")
                        '            End With
                        '            .Indexes.Append idxCountry
                        '    Set idxCountry = Nothing
                        '    .Indexes.Refresh
                        '        End With

                        '    End If  'merge to 186

                        '--- add datadict row -----------
                        While DicTblSource.Read()
                            'If UCase(DicTblSource.Fields(0).Value) = UCase(TableImport) Then
                            If UCase(DicTblSource(1).ToString) = UCase(TableImport) Then

                                'add values to Master Datadict
                                'jrs 2024/08/10 replacing dbDestDatadictCmd
                                dbDestDatadictRdrCmd.CommandText = "SELECT * FROM Datadict WHERE UPPER([Table]) = """ & TableImport.ToUpper & """"
                                DicTblDest = dbDestDatadictRdrCmd.ExecuteReader

                                'mti 2015/09/08 -start
                                Dim EditRow = False
                                If DicTblDest.HasRows Then
                                    EditRow = True
                                End If
                                DicTblDest.Close()
                                'jrs 2024/08/09
                                DicTblDest = Nothing
                                dbDestDatadict.ReleaseMemory()


                                'mti 2015/09/08 - end
                                Dim sIns, sIns2, sUPd As String
                                sUPd = "UPDATE Datadict SET "
                                sIns = "INSERT INTO Datadict ("
                                sIns2 = " VALUES("

                                For i2 = 0 To DicTblSource.FieldCount - 1
                                    Dim DDFName = DicTblSource.GetName(i2)
                                    'jrs 2025/03/10
                                    If Not sFieldsNotToChange.Contains(DDFName) Then
                                        'jrs 2023/10/15 Dim DDFVal = DicTblSource(i2).ToString
                                        Dim DDFVal As String
                                        If Not IsDBNull(DicTblSource(i2)) Then
                                            DDFVal = DicTblSource(i2).ToString
                                        Else
                                            DDFVal = "Null"
                                        End If
                                        'jrs 2023/08/10
                                        DDFVal = DDFVal.Replace("""", """""")
                                        If EditRow Then
                                            'jrs 2024/09/12 changed to > 1 to avoid chaing table name
                                            If i2 > 1 Then
                                                'jrs 2024/09/12 changed to > 2 to avoid chaing table name
                                                If i2 > 2 Then
                                                    sUPd = sUPd & ","
                                                End If
                                                'jrs 2023/10/15 sUPd = sUPd & "[" & DDFName & "] = """ & DDFVal & """"
                                                If DDFVal <> "Null" Then
                                                    sUPd = sUPd & "[" & DDFName & "] = """ & DDFVal & """"
                                                Else
                                                    sUPd = sUPd & "[" & DDFName & "] = Null"
                                                End If
                                            End If
                                        Else
                                            If i2 > 0 Then
                                                sIns = sIns & ", "
                                                sIns2 = sIns2 & ", "
                                            End If
                                            sIns = sIns & "[" & DDFName & "]"
                                            'jrs 2023/10/15 sIns2 = sIns2 & """" & DDFVal & """"
                                            If DDFVal <> "Null" Then
                                                sIns2 = sIns2 & """" & DDFVal & """"
                                            Else
                                                sIns2 = sIns2 & "Null"
                                            End If
                                        End If
                                    End If
                                Next

                                If EditRow Then
                                    sUPd = sUPd & " WHERE UPPER([Table]) = """ & TableImport.ToUpper & """"
                                    dbDestDatadictCmd.CommandText = sUPd
                                Else
                                    dbDestDatadictCmd.CommandText = sIns & ") " & sIns2 & ")"
                                End If
                                dbDestDatadictCmd.ExecuteNonQuery()
                                'jrs 2024/08/09 it was finishing the Reader
                                Exit While
                            End If
                        End While

                        ValTblSource.Close()
                        'jrs 2024/08/09
                        ValTblSource = Nothing
                        dbSource.ReleaseMemory()
                        dbDest.ReleaseMemory()

                        'mti 2015/09/08 - start
                        If TableRenamed$ <> "" Then
                            dbDestCmd.CommandText = "DROP TABLE [" & TableRenamed$ & "]"
                            dbDestCmd.ExecuteNonQuery()
                            TableRenamed$ = ""
                        End If
                        'mti 2015/09/08 - end

                        'jrs 2024/01/09 Added this option in case Mohammod wants to test and later share file with others.
                        If chkResetFileWhenMerging.Checked Then
                            'jrs 2024/08/10
                            ''jrs 2023/05/12 Delete table from Source file
                            'dbDestCmd.CommandText = "DROP TABLE source_db.[" & TableImport & "]"
                            'dbDestCmd.ExecuteNonQuery()
                            ''jrs 2023/05/12 Delete row from DataDict
                            'dbDestCmd.CommandText = "DELETE FROM source_db.DataDict WHERE [Table] = """ & TableImport & """"
                            'dbDestCmd.ExecuteNonQuery()
                            TableList4Reset.Add(TableImport)
                        End If
                        'jrs 2023/04/18 End If 'it is selected for merging?
                    End If ' series = yes
                End If 'selected
            Next


            DicTblSource.Close()


            DicTblSource = Nothing

            dbDest.Close()
            dbDestDatadict.Close()

            dbDest.Dispose()
            dbDestDatadict.Dispose()

            'jrs 2024/08/10 move reseting source file here to avoid locks
            If chkResetFileWhenMerging.Checked Then
                Dim dbSourceCmdReset As SQLiteCommand
                dbSourceCmdReset = dbSource.CreateCommand
                For Each TblName In TableList4Reset
                    dbSourceCmdReset.CommandText = "DROP TABLE [" & TblName & "]"
                    dbSourceCmdReset.ExecuteNonQuery()
                    'Delete row from DataDict
                    dbSourceCmdReset.CommandText = "DELETE FROM DataDict WHERE [Table] = """ & TblName & """"
                    dbSourceCmdReset.ExecuteNonQuery()
                Next
            End If

            dbSource.Close()
            dbSource.Dispose()


            '            If Build186 Then
            '                dbDest186.Close
            '    Set dbDest186 = Nothing
            'End If

            Dim msg$ = "<script>alert('Successfully merged imported data table and data dictionary information."
            msg$ = msg$ + "\n\nTables Updated: " + Trim(CStr(Updcnt%))
            msg$ = msg$ + "\nTables Inserted: " + Trim(CStr(InsertCnt%)) + "');</script>"

            'MsgBox "Successfully merged imported data table and data dictionary information.", , "IFs - Compare Imported Data"

            'jrs 2023/12/08
            FileCopyCheck(DataFileDir$ + "IFsHistSeries.db", Session("PutFdrvRun") + "IFsHistSeries.db")

            Response.Write(msg)

        Catch ex As Exception
            'jrs 2024/08/09 added .Replace("vbReturn", "\n")
            Dim sMsg = "<script>alert('Error merging imported data." + IIf(TableImport <> "", "(" + TableImport + ").", "") + "\nError Message: " + Err.Description.Replace("'", "").Replace(vbCrLf, "\n") + "');</script>"
            Response.Write(sMsg)

            'mti 2015/09/08 - start
            If TableCreated <> "" Then
                dbDestCmd.CommandText = "DROP TABLE [" & TableCreated & "]"
                dbDestCmd.ExecuteNonQuery()
            End If
            If TableRenamed$ <> "" Then
                dbDestCmd.CommandText = "ALTER TABLE [" & TableRenamed$ & "] RENAME TO [Series" & GridViewNew.Rows(Rowcnt).Cells(2).Text & "]"
                dbDestCmd.ExecuteNonQuery()
                dbDest.Close()
                TableRenamed$ = ""
            End If
            'mti 2015/09/08 - end


        End Try


        LoadGrdTableList()

    End Sub

    Private Sub LoadGrdTableList()
        Dim db1 As SQLiteConnection
        Dim db1Cmd As SQLiteCommand

        Session("MDBFileName") = Session("PutFdrvRun") & "IFsDataImport.db"

        db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & "IFsDataImport.db;", String))
        db1.Open()

        db1Cmd = db1.CreateCommand
        db1Cmd.CommandText = "SELECT * FROM [DataDict] ORDER BY [Variable]"

        Dim db1Rdr As SQLiteDataReader
        db1Rdr = db1Cmd.ExecuteReader

        Dim tbl As DataTable = New DataTable

        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(tbl)
        tbl.Load(db1Rdr)

        'Read all vars
        Dim sVars As String = ""
        For i = 0 To tbl.Rows.Count - 1
            If i > 0 Then
                sVars = sVars & ", "
            End If
            sVars = sVars & """" & tbl.Rows(i).Item("Variable") & """"
        Next

        'Loading DataDict
        Dim db2 As SQLiteConnection
        Dim db2Cmd As SQLiteCommand

        db2 = New SQLiteConnection("DataSource=" + DataFileDir$ & "DataDict.db;")
        db2.Open()
        db2Cmd = db2.CreateCommand
        db2Cmd.CommandText = "SELECT [Variable] FROM [DataDict] WHERE [Variable] IN (" & sVars & ") ORDER BY [Variable]"

        Dim db2Rdr As SQLiteDataReader
        db2Rdr = db2Cmd.ExecuteReader

        Dim newCol As DataColumn
        newCol = New DataColumn("Current", tbl.Columns(0).DataType)
        tbl.Columns.Add(newCol)
        tbl.Columns("Current").SetOrdinal(1)

        If db2Rdr.HasRows Then
            Dim tblRow As Integer = 0
            While db2Rdr.Read
                If tbl.Rows(tblRow).Item("Variable") = db2Rdr(0) Then
                    tbl.Rows(tblRow).Item("Current") = db2Rdr(0)
                    tblRow = tblRow + 1
                    If tblRow = tbl.Rows.Count Then Exit While
                ElseIf tbl.Rows(tblRow).Item("Variable") < db2Rdr(0) Then
                    'jrs 2023/12/08
                    Do While tbl.Rows(tblRow).Item("Variable") < db2Rdr(0)
                        tblRow = tblRow + 1
                        If tblRow = tbl.Rows.Count Then Exit Do
                    Loop

                    If tblRow = tbl.Rows.Count Then Exit While

                    'jrs 2023/12/08
                    If tbl.Rows(tblRow).Item("Variable") = db2Rdr(0) Then
                        tbl.Rows(tblRow).Item("Current") = db2Rdr(0)
                        tblRow = tblRow + 1
                        If tblRow = tbl.Rows.Count Then Exit While
                    End If
                End If
            End While
        End If

        'jrs 2023/07/28
        tbl.Columns(8).ColumnName = "______________________________________Definition______________________________________"
        tbl.Columns(9).ColumnName = "______________________________Extended_Source_Definition______________________________"
        tbl.Columns(12).ColumnName = "___Years___"
        tbl.Columns(13).ColumnName = "________________________________________Source________________________________________"
        tbl.Columns(14).ColumnName = "___________________________________Original_Source____________________________________"
        tbl.Columns(21).ColumnName = "___________________________________Name_in_Source_____________________________________"
        tbl.Columns(27).ColumnName = "___Country_Concordance___"
        tbl.Columns(29).ColumnName = "_____________________________________DisplayNotes_____________________________________"

        GridViewNew.DataSource = tbl
        GridViewNew.DataBind()

        'jrs 2023/08/01
        Dim colIdxs As List(Of Integer) = New List(Of Integer)
        colIdxs.Add(10) 'Definition
        colIdxs.Add(11) 'Extended Source Definition
        colIdxs.Add(15) 'Source
        colIdxs.Add(16) 'Original Source
        colIdxs.Add(23) 'Name in Source
        colIdxs.Add(31) 'Display Notes
        For i = 0 To GridViewNew.Rows.Count - 1
            For Each colIdx In colIdxs
                GridViewNew.Rows(i).Cells(colIdx).ToolTip = GridViewNew.Rows(i).Cells(colIdx).Text
                If GridViewNew.Rows(i).Cells(colIdx).Text.Length > 100 Then
                    GridViewNew.Rows(i).Cells(colIdx).Text = GridViewNew.Rows(i).Cells(colIdx).Text.Substring(0, 100)
                End If
            Next
        Next

        Session("ViewDataBatchTblNewDD") = tbl

        db1Cmd.Dispose()
        db2Cmd.Dispose()

        db1.Close()
        db1.Dispose()
        db2.Close()
        db2.Dispose()

        If GridViewNew.Rows.Count > 0 Then
            GridViewNew.SelectedIndex = 0
            GridViewNew_SelectedIndexChanged(Me, New EventArgs)
        Else
            dbgridexistingdata.Visible = False
            GridViewDet.Visible = False
        End If
    End Sub

    Private Sub chkMarkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkMarkAll.CheckedChanged
        If GridViewNew.Rows.Count <= 0 Then
            chkMarkAll.Checked = False
            Exit Sub
        End If

        If Not chkMarkAll.Checked Then
            For rcnt = 0 To GridViewNew.Rows.Count - 1
                TryCast(GridViewNew.Rows(rcnt).Cells(1).Controls(1), CheckBox).Checked = False
            Next
        Else
            For rcnt = 0 To GridViewNew.Rows.Count - 1
                TryCast(GridViewNew.Rows(rcnt).Cells(1).Controls(1), CheckBox).Checked = True
            Next
        End If

    End Sub

    Private Sub cmdReloadFile_Click(sender As Object, e As EventArgs) Handles cmdReloadFile.Click
        If fuOpenFile.HasFile Then
            Session("ImportXLSfuOpenFile") = fuOpenFile

            Dim MyFileCollection As HttpFileCollection
            Dim MyFile As HttpPostedFile
            MyFileCollection = Request.Files
            If MyFileCollection.Count > 0 AndAlso MyFileCollection(0).FileName > "" Then
                MyFile = MyFileCollection(0)
                Dim dbFile = System.IO.Path.GetFileName(MyFile.FileName)
                MyFile.SaveAs(Session("PutFdrvRun") & "IFsDataImport.db")

                LoadGrdTableList()

                chkMarkAll.Checked = True
                chkMarkAll_CheckedChanged(Me, New EventArgs)

            End If
        Else
            Response.Write("<script>alert('Need to select a file first to Reload it.');</script>")
        End If

    End Sub
End Class
