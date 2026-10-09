Option Strict Off
Option Explicit On

Public Class frm_ImportPopData
    Inherits Page

    Private Sub frm_ImportPopData_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblExplanation.Text = "The base year is now " & CStr(FirstYearForecast) & ", the population data will be loaded based on this year, if you want to change it you must change the base year in ifsInit.mdb"

    End Sub

    Protected Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub


    'Private Sub AvgFrom2ExcelSheets(ByVal Msg As String, ByVal xlsFile As String, ByVal PopTbl As String,
    'ByVal rr1 As Long, ByVal cc1 As Integer, ByVal RepCols As Integer,
    'ByVal sFilterYr As String, ByVal DataCols As Integer, ByVal rscc1 As Integer, ByRef db As SqliteConnection,
    'ByVal sFilterSex As String, ByVal sWorksheet As String,
    'ByVal CtryCol As Integer, ByVal dd1 As Integer, ByVal sWorksheet2 As String,
    'ByVal sFilterYr2 As String, ByVal rr2 As Integer, ByVal sFilterYrBreak As String)
    '    'rr1 is the initial row where to start reading the file
    '    'cc1 is the column for the years
    '    'RepCols is the # of columns to repit, right at the end (and filled with last value)
    '    'sFilterYr is the year to be used
    '    'DataCols is the # of columns to load
    '    'rscc1 is the position in the recordset of the first column with data
    '    'sFilterSex is the sex to be used
    '    'CtryCol Column where the country is stored
    '    'dd1 is the column where the data starts
    '    'sWorksheet2 is the worksheet for forecast values
    '    'sFilterYr2 is the 2nd range in the average
    '    'rr2 is the inital row where to start reading file 2
    '    'sFilterYrBreak is the last range in the Estimates worksheet

    '    Dim bDiffSheet As Boolean
    '    bDiffSheet = False
    '    If sFilterYr <= sFilterYrBreak And sFilterYr2 > sFilterYrBreak Then
    '        bDiffSheet = True
    '        'Go Read First sheet
    '        ReadFromExcel(Msg, xlsFile, PopTbl, rr1, cc1, RepCols, sFilterYr, DataCols, rscc1, db, sFilterSex, sWorksheet, CtryCol, dd1)
    '    End If

    '    'lblProgress.Caption = "Processing " & Msg
    '    Dim reader As IExcelDataReader
    '    Dim stream = File.Open(Session("PutFdrvRun") & xlsFile, FileMode.Open, FileAccess.Read)
    '    reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream)
    '    Dim conf = New ExcelDataSetConfiguration With {
    '    .ConfigureDataTable = Function(__) New ExcelDataTableConfiguration With {
    '        .UseHeaderRow = True
    '    }
    '}
    '    Dim dataSet = reader.AsDataSet(conf)
    '    Dim dataTable = dataSet.Tables(sWorksheet)

    '    If bDiffSheet Then
    '        dataTable = dataSet.Tables(sWorksheet2)
    '    Else
    '        If sFilterYr > sFilterYrBreak Then
    '            dataTable = dataSet.Tables(sWorksheet2)
    '        End If
    '    End If

    '    'Dim rs As Recordset
    '    'Set rs = db.OpenRecordset(PopTbl)
    '    'rs.Index = "Country"

    '    Dim rr As Integer, cc As Integer
    '    Dim vv As Single, yy As String
    '    Dim sCtryUN As String, sCtry As String
    '    Dim rrB As Integer, yyB As String
    '    'jrs 2009/01/05
    '    Dim bFilter As Boolean
    '    Dim gg As String

    '    If sFilterYr > sFilterYrBreak Or bDiffSheet Then
    '        rr = rr2 - 2
    '    Else
    '        rr = rr1 - 2
    '    End If

    '    'jrs 2011/07/26
    '    Dim mm As Integer

    '    Dim vSQLiteCommand As SqliteCommand
    '    Dim vSQLiteReader As SqliteDataReader

    '    Do While True
    '        yy = dataTable.Rows(rr).ItemArray(cc1)
    '        'jrs 2011/07/26
    '        If yy = "" Then
    '            yy = dataTable.Rows(rr - 1).ItemArray(cc1)
    '            If yy = "" Then
    '                yy = dataTable.Rows(rr - 2).ItemArray(cc1)
    '            End If
    '        End If
    '        'jrs 2009/01/05
    '        If bDiffSheet Then
    '            bFilter = (yy = sFilterYr2)
    '        Else
    '            bFilter = (yy = sFilterYr)
    '        End If
    '        If bFilter And sFilterSex > "" Then
    '            gg = Trim(dataTable.Rows(rr).ItemArray(cc1 + 1))
    '            bFilter = bFilter And (gg = sFilterSex)
    '        End If
    '        If bFilter Then
    '            'read country
    '            sCtryUN = dataTable.Rows(rr).ItemArray(CtryCol)
    '            'jrs 2011/07/26
    '            mm = 1
    '            Do While sCtryUN = "" And rr > mm
    '                sCtryUN = dataTable.Rows(rr - mm).ItemArray(CtryCol)
    '                mm = mm + 1
    '            Loop
    '            'translate
    '            vSQLiteCommand = db.CreateCommand()
    '            vSQLiteCommand.CommandText = "SELECT Country FROM [Country Translation] WHERE [UN Pop Countries] = """ & sCtryUN & """"
    '            vSQLiteReader = vSQLiteCommand.ExecuteReader()

    '            If vSQLiteReader.HasRows Then
    '                vSQLiteReader.Read()
    '                sCtry = vSQLiteReader(0)
    '            Else
    '                sCtry = sCtryUN
    '            End If
    '            'check country
    '            vSQLiteReader.Close()
    '            vSQLiteCommand.CommandText = "SELECT * FROM " & PopTbl & " WHERE Country = """ & sCtry & """"
    '            vSQLiteReader = vSQLiteCommand.ExecuteReader()

    '            If vSQLiteReader.HasRows Then
    '                vSQLiteReader.Close()
    '                Dim sUpd As String = "UPDATE " & PopTbl & " SET "
    '                'Load all cohorts
    '                For cc = dd1 To dd1 + DataCols - 1
    '                    vv = dataTable.Rows(rr).ItemArray(cc)
    '                    If cc > dd1 Then
    '                        sUpd = sUpd & ", "
    '                    End If
    '                    If bDiffSheet Then
    '                        sUpd = sUpd & "Cohort" & (cc - (dd1 - rscc1)).ToString & " = MAX((Cohort" & (cc - (dd1 - rscc1)).ToString & " + " & (vv).ToString & ")/2, 0.002)"
    '                    Else
    '                        sUpd = sUpd & "Cohort" & (cc - (dd1 - rscc1)).ToString & " = " & (AMAX((vv + dataTable.Rows(rr + 1).ItemArray(cc)) / 2, 0.002)).ToString
    '                    End If
    '                Next cc
    '                'remaining cohorts 85+
    '                For cc = dd1 + DataCols To dd1 + DataCols + RepCols - 1
    '                    If bDiffSheet Then
    '                        sUpd = sUpd & ", Cohort" & (cc - (dd1 - rscc1)).ToString & " = MAX((Cohort" & (cc - (dd1 - rscc1)).ToString & " + " & (vv).ToString & ")/2, 0.002)"
    '                    Else
    '                        sUpd = sUpd & ", Cohort" & (cc - (dd1 - rscc1)).ToString & " = " & AMAX((vv + dataTable.Rows(rr + 1).ItemArray(cc)) / 2, 0.002).ToString
    '                    End If
    '                Next cc
    '                'WHERE statement
    '                sUpd = sUpd & " WHERE Country = """ & sCtry & """"

    '                vSQLiteCommand.CommandText = sUpd
    '                vSQLiteCommand.ExecuteReader()
    '            End If

    '        End If
    '        rr = rr + 1
    '        'jrs 2011/07/26
    '        'If dataTable.Rows(rr).ItemArray(cc1) = "" And dataTable.Rows(rr + 1).ItemArray(cc1) = "" And dataTable.Rows(rr + 2).ItemArray(cc1) = "" Then Exit Do
    '        If dataTable.Rows.Count = rr Then Exit Do
    '    Loop

    '    'wb.Close()

    '    'rs.Close

    '    reader.Close()
    '    stream.Close()

    'End Sub

    'Private Sub AddCtryAgeRows(ByVal Msg As String, ByVal xlsFile As String, ByVal PopTbl As String,
    'ByRef xlApp As Microsoft.Office.Interop.Excel.Application, ByVal rr1 As Long,
    'ByVal DataCols As Integer, ByRef db As SqliteConnection,
    'ByVal sWorksheet As String,
    'ByVal CtryCol As Integer, ByVal dd1 As Integer, ByVal iYear As Integer)
    '    'rr1 is the initial row where to start reading the file
    '    'RepCols is the # of columns to repit, right at the end (and filled with last value)
    '    'sFilterYr is the year to be used
    '    'DataCols is the # of columns to load
    '    'CtryCol Column where the country is stored
    '    'dd1 is the column where the data starts

    '    Dim wb As Workbook
    '    Dim ws As Worksheet

    '    'lblProgress.Caption = "Processing " & Msg
    '    wb = xlApp.Workbooks.Open(Session("PutFdrvRun") & xlsFile)
    '    ws = wb.Worksheets(sWorksheet)

    '    'Delete Migration table
    '    '----------------------
    '    Dim vSQLiteCommand As SqliteCommand
    '    Dim vSQLiteReader As SqliteDataReader

    '    vSQLiteCommand = db.CreateCommand()
    '    vSQLiteCommand.CommandText = "DELETE * FROM " & PopTbl & " WHERE Year = " & CStr(iYear)
    '    vSQLiteCommand.ExecuteReader()

    '    'Dim rs As Recordset
    '    'Set rs = db.OpenRecordset(PopTbl)

    '    Dim rr As Integer, cc As Integer
    '    Dim vv As Single, yy As String
    '    Dim sCtryUN As String, sCtry As String
    '    Dim gg As String

    '    rr = rr1
    '    'jrs 2011/07/26
    '    Dim mm As Integer

    '    Do While True
    '        'read country
    '        sCtryUN = ws.Cells(rr, CtryCol)
    '        'jrs 2011/07/26
    '        mm = 1
    '        Do While sCtryUN = "" And rr > mm
    '            sCtryUN = ws.Cells(rr - mm, CtryCol)
    '            mm = mm + 1
    '        Loop
    '        'translate
    '        vSQLiteCommand = db.CreateCommand()
    '        vSQLiteCommand.CommandText = "SELECT Country FROM [Country Translation] WHERE [UN Pop Countries] = """ & sCtryUN & """"
    '        vSQLiteReader = vSQLiteCommand.ExecuteReader()

    '        If vSQLiteReader.HasRows Then
    '            vSQLiteReader.Read()
    '            sCtry = vSQLiteReader(0)

    '            'Load all cohorts
    '            Dim sIns As String = "INSERT INTO PopTbl (Year, Ncohort, Country) Values(" & iYear.ToString & ", "
    '            For cc = dd1 To dd1 + DataCols - 1
    '                vSQLiteCommand.CommandText = sIns & ws.Cells(16, cc) & ", " & sCtry & ")"
    '                vSQLiteCommand.ExecuteReader()
    '            Next cc
    '        End If
    '        rr = rr + 1
    '        If ws.Cells(rr, CtryCol) = "" Then Exit Do
    '    Loop

    '    wb.Close()

    'End Sub

    'Private Sub ReadFromExcelMig(ByVal Msg As String, ByVal xlsFile As String, ByVal PopTbl As String,
    'ByRef xlApp As Microsoft.Office.Interop.Excel.Application, ByVal rr1 As Long,
    'ByVal DataCols As Integer, ByRef db As SqliteConnection,
    'ByVal sWorksheet As String,
    'ByVal CtryCol As Integer, ByVal dd1 As Integer, ByVal iYear As Integer, ByVal sFldName As String)
    '    'rr1 is the initial row where to start reading the file
    '    'RepCols is the # of columns to repit, right at the end (and filled with last value)
    '    'sFilterYr is the year to be used
    '    'DataCols is the # of columns to load
    '    'rscc1 is the position in the recordset of the first column with data
    '    'CtryCol Column where the country is stored
    '    'dd1 is the column where the data starts

    '    Dim wb As Workbook
    '    Dim ws As Worksheet

    '    'lblProgress.Caption = "Processing " & Msg
    '    wb = xlApp.Workbooks.Open(Session("PutFdrvRun") & xlsFile)
    '    ws = wb.Worksheets(sWorksheet)

    '    'Dim rs As Recordset
    '    'Set rs = db.OpenRecordset(PopTbl)
    '    'rs.Index = "PrimaryKey"

    '    Dim rr As Integer, cc As Integer
    '    Dim vv As Single, yy As String
    '    Dim sCtryUN As String, sCtry As String
    '    Dim gg As String

    '    rr = rr1
    '    'jrs 2011/07/26
    '    Dim mm As Integer
    '    Dim sAge As String

    '    Dim vSQLiteCommand As SqliteCommand
    '    Dim vSQLiteReader As SqliteDataReader

    '    Do While True
    '        'read country
    '        sCtryUN = ws.Cells(rr, CtryCol)
    '        'jrs 2011/07/26
    '        mm = 1
    '        Do While sCtryUN = "" And rr > mm
    '            sCtryUN = ws.Cells(rr - mm, CtryCol)
    '            mm = mm + 1
    '        Loop
    '        'translate
    '        vSQLiteCommand = db.CreateCommand()
    '        vSQLiteCommand.CommandText = "SELECT Country FROM [Country Translation] WHERE [UN Pop Countries] = """ & sCtryUN & """"
    '        vSQLiteReader = vSQLiteCommand.ExecuteReader()

    '        If vSQLiteReader.HasRows Then
    '            vSQLiteReader.Read()
    '            sCtry = vSQLiteReader(0)

    '            'Load all cohorts
    '            For cc = dd1 To dd1 + DataCols - 1
    '                sAge = ws.Cells(16, cc)
    '                'check row
    '                'vSQLiteCommand.CommandText = "SELECT * FROM " & PopTbl & " WHERE Country = """ & sCtry & """"
    '                'vSQLiteReader = vSQLiteCommand.ExecuteReader()

    '                'If vSQLiteReader.HasRows Then
    '                Dim sUpd As String = "UPDATE " & PopTbl & " SET "
    '                Dim sWHERE As String = " WHERE Year = " & iYear.ToString & " AND NCohorts = """ & sAge & """ AND Country = """ & sCtry & """"

    '                If ws.Cells(rr, cc) <> ".." Then
    '                    vv = ws.Cells(rr, cc)
    '                    vSQLiteCommand.CommandText = sUpd & sFldName & " = " & vv.ToString & sWHERE
    '                    vSQLiteCommand.ExecuteReader()
    '                End If
    '            Next cc
    '        End If

    '        rr = rr + 1
    '        If ws.Cells(rr, CtryCol) = "" Then Exit Do
    '    Loop

    '    wb.Close()
    '    'rs.Close

    'End Sub

    Public Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        cmdLoad.Enabled = False
        cmdExit.Enabled = False

        LoadImpPopData(Session("IFs_MDB2Active"), Session("PutFdrvRun"))

        cmdExit.Enabled = True
        cmdExit.Text = "Continue"
        lblExplanation.Text = "Process completed."

    End Sub
End Class

