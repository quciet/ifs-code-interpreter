Imports System.Data.Entity.Migrations
Imports System.Data.SQLite
Imports System.IO
Imports ExcelDataReader

Partial Class Frm_ImportXLSNewProvinces
    Inherits Page

    Dim xlsFile As String
    Protected WithEvents MyExcel As System.Diagnostics.Process
    Dim ErrorText As String
    Dim AllYear$

    Private Sub Frm_ImportXLSNew_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        'jrs 2012/01/17
        'jrs 2013/11/21 can't have accessorkeycode when needing the enter key in texbox to send postback through javascript
        'cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        'AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler


        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_MainMenu.aspx"
        cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        If Not (Session("ImportXLSfuOpenFile") Is Nothing) Then
            fuOpenFile = Session("ImportXLSfuOpenFile")
        End If


        If Not Session("xlsFile") = Nothing Then
            xlsFile = Session("xlsFile")
        End If

        If Not Page.IsPostBack Then
            RefreshSheets()

            'jrs 2023/11/01
            If OptSeriesType5.Checked Then
                Session("SeriesType") = 5
            Else
                Session("SeriesType") = 2
            End If
            'Load Aggregations
            cmbaggregation.Items.Add("None")
            cmbaggregation.Items.Add("POP")
            cmbaggregation.Items.Add("GDP")
            cmbaggregation.Items.Add("LND")
            cmbaggregation.Items.Add("AVG")
            cmbaggregation.Items.Add("SUM")
            'Load Decimals
            cmbDecimals.Items.Add("Keep as is")
            For i = 0 To 5
                cmbDecimals.Items.Add(i.ToString)
            Next

            'Load DataDict info
            Dim rs As DataTable
            SetDataTablefromSQLite(Session("PutFdrvRun") + "DataDict.db", "SELECT [Variable], Definition, Aggregation, [Decimal Places] FROM DataDict ORDER BY [Variable]", "", rs)
            lstVariablelist.Items.Add("New")
            For i = 0 To rs.Rows.Count - 1
                lstVariablelist.Items.Add(rs.Rows(i).ItemArray(0))
            Next
            lstVariablelist.Items(0).Selected = True
            Session("rsDataDict") = rs

            lstVariablelist_SelectedIndexChanged(Me, New EventArgs)

            'jrs 2026/07/23
            If Not (Session("txtDatabaseFile") Is Nothing) Then
                txtDatabaseFile.Text = Session("txtDatabaseFile")
            End If

            If Session("Accepted") = True Then
                CmdImport.Text = "View Imported Data"

                'mti 08/18/2017 start
                'jrs 2023/05/10 Dim sMsg = "Table " + txtTable.Text + " successfully added to " + DataFileDir$ + txtDatabaseFile.Text + ". Data dictionary values saved inside the same database."
                Dim sMsg = "Table " + Session("txtTable") + " successfully added to " + Session("PutFdrvRun") + txtDatabaseFile.Text + ". Data dictionary values saved inside the same database."
                'Session("ImportXLSVarName")
                'Session("DataDictRow")

                'jrs 20223/05/10 Response.Write(sMsg)
                lblMessages.Text = sMsg
                lblMessages.Visible = True

            End If

            Session("Prov_Country_MDB") = Session("PutFdrvRun") & "Provinces.db"
            LoadCountries()
        Else 'jrs 2023/05/10
            lblMessages.Visible = False
        End If

    End Sub

    Private Sub RefreshSheets()
        If fuOpenFile.HasFile Then
            Session("ImportXLSfuOpenFile") = fuOpenFile

            Dim MyFileCollection As HttpFileCollection
            Dim MyFile As HttpPostedFile
            Dim FileLen As Integer
            Dim MyString As String
            Dim MyStream As System.IO.Stream
            MyFileCollection = Request.Files
            If MyFileCollection.Count > 0 AndAlso MyFileCollection(0).FileName > "" Then
                Session("ImportXLSMyFileCollection") = MyFileCollection
                MyFile = MyFileCollection(0)
                xlsFile = System.IO.Path.GetFileName(MyFile.FileName)
                MyFile.SaveAs(Session("PutFdrvRun") & "tmp\" & xlsFile)
            Else
                MyFileCollection = Session("ImportXLSMyFileCollection")
                MyFile = MyFileCollection(0)
                xlsFile = System.IO.Path.GetFileName(MyFile.FileName)
            End If

            Session("xlsFile") = xlsFile
            txtFileSelected.Text = xlsFile

            Dim reader As IExcelDataReader
            Dim stream = File.Open(Session("PutFdrvRun") & "tmp\" & xlsFile, FileMode.Open, FileAccess.Read)
            reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream)
            Dim conf = New ExcelDataSetConfiguration With {
                .ConfigureDataTable = Function(__) New ExcelDataTableConfiguration With {
                .UseHeaderRow = False
                    }
                }

            Dim dataSet = reader.AsDataSet(conf)
            lstSheets.Items.Clear()

            For i = 0 To dataSet.Tables.Count - 1
                lstSheets.Items.Add(dataSet.Tables(i).TableName)
            Next

            reader.Close()
            stream.Close()

            lstSheets.Items(0).Selected = True
            lstSheets_SelectedIndexChanged(Me, New EventArgs)

        End If
    End Sub

    Private Sub lstSheets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSheets.SelectedIndexChanged
        If lstSheets.SelectedIndex >= 0 Then
            Dim reader As IExcelDataReader
            Dim stream = File.Open(Session("PutFdrvRun") & "tmp\" & xlsFile, FileMode.Open, FileAccess.Read)
            reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream)
            Dim conf = New ExcelDataSetConfiguration With {
                .ConfigureDataTable = Function(__) New ExcelDataTableConfiguration With {
                .UseHeaderRow = False
                }
            }

            Dim dataSet = reader.AsDataSet(conf)
            Dim dataTable = dataSet.Tables(lstSheets.SelectedIndex)
            Session("dataTable") = dataTable

            Dim rr As Integer, cc As Integer
            rr = 0
            cc = 0

            Dim bFindFirstYear, bFindFirstCountry As Boolean
            bFindFirstCountry = False
            bFindFirstYear = False
            Dim sCell As String = ""
            Dim iVal As Integer = 0

            CmbYearCol.SelectedIndex = -1
            CmbYearRow.SelectedIndex = -1
            CmbCountryCol.SelectedIndex = -1
            CmbCountryRow.SelectedIndex = -1
            'jrs 2023/03/27
            CmbYearCol.Items.Clear()
            CmbYearRow.Items.Clear()
            CmbCountryCol.Items.Clear()
            CmbCountryRow.Items.Clear()
            'jrs 2024/01/08
            cmbDataCol.SelectedIndex = -1
            cmbDataRow.SelectedIndex = -1
            cmbDataCol.Items.Clear()
            cmbDataRow.Items.Clear()

            'Load Columns
            Dim sColName As String = ""
            Dim cc2 As Integer
            For cc = 0 To dataTable.Columns.Count - 1
                If cc > 25 Then
                    cc2 = (cc \ 26) + 64
                    sColName = Chr(cc2)
                Else
                    sColName = ""
                End If
                sColName = sColName & Chr((cc Mod 26) + 65)
                CmbYearCol.Items.Add(sColName)
                CmbCountryCol.Items.Add(sColName)
                'jrs 2024/01/08
                cmbDataCol.Items.Add(sColName)
            Next

            'Load Years
            For rr = 1 To dataTable.Rows.Count
                CmbYearRow.Items.Add(rr)
                CmbCountryRow.Items.Add(rr)
                'jrs 2024/01/08
                cmbDataRow.Items.Add(rr)
            Next

            reader.Close()
            stream.Close()

            'Depending on The format selected only check the first row, and the first column for Countries and Years
            Select Case True
                Case OptSeriesType2.Checked
                    'Looking for years
                    cc = 1
                    For rr = 0 To dataTable.Rows.Count - 1
                        If IsDBNull(dataTable.Rows(rr).ItemArray(cc)) Then
                            sCell = ""
                        Else
                            sCell = dataTable.Rows(rr).ItemArray(cc)
                        End If
                        If IsNumeric(sCell) Then
                            iVal = CInt(sCell)
                            If iVal > 1000 And iVal < 3000 Then
                                CmbYearCol.SelectedIndex = cc
                                CmbYearRow.SelectedIndex = rr
                                Exit For
                            End If
                        End If
                    Next
                    'Looking for countries
                    For rr = 0 To dataTable.Rows.Count - 1
                        If IsDBNull(dataTable.Rows(rr).ItemArray(0)) Then
                            sCell = ""
                        Else
                            sCell = dataTable.Rows(rr).ItemArray(0)
                        End If
                        If IFsRegs.Item(sCell).Index > 0 Then
                            CmbCountryCol.SelectedIndex = 0
                            CmbCountryRow.SelectedIndex = rr
                            Exit For
                        End If
                    Next
                    'jrs 2024/01/08 
                    'Looking for Data
                    cc = 2
                    For rr = 0 To dataTable.Rows.Count - 1
                        If IsDBNull(dataTable.Rows(rr).ItemArray(cc)) Then
                            sCell = ""
                        Else
                            sCell = dataTable.Rows(rr).ItemArray(cc)
                        End If
                        If IsNumeric(sCell) Then
                            cmbDataCol.SelectedIndex = cc
                            cmbDataRow.SelectedIndex = rr
                            Exit For
                        End If
                    Next

                    'jrs 2024/01/09
                    YearsUniqueFilterNSort()
                Case OptSeriesType5.Checked
                    'Looking for years
                    For cc = 0 To dataTable.Columns.Count - 1
                        'jrs 2023/05/03
                        If IsDBNull(dataTable.Rows(0).ItemArray(cc)) Then
                            sCell = ""
                        Else
                            sCell = dataTable.Rows(0).ItemArray(cc)
                        End If
                        If IsNumeric(sCell) Then
                            iVal = CInt(sCell)
                            If iVal > 1000 And iVal < 3000 Then
                                CmbYearCol.SelectedIndex = cc
                                CmbYearRow.SelectedIndex = 0
                                Exit For
                            End If
                        End If
                    Next
                    'Looking for countries
                    For rr = 0 To dataTable.Rows.Count - 1
                        'jrs 2023/05/03
                        If IsDBNull(dataTable.Rows(rr).ItemArray(0)) Then
                            sCell = ""
                        Else
                            sCell = dataTable.Rows(rr).ItemArray(0)
                        End If
                        If IFsRegs.Item(sCell).Index > 0 Then
                            CmbCountryCol.SelectedIndex = 0
                            CmbCountryRow.SelectedIndex = rr
                            Exit For
                        End If
                    Next

                    'jrs 2023/04/27
                    If CmbYearCol.SelectedIndex = 0 Then
                        'Try format 2 second
                        OptSeriesType2.Checked = True
                        OptSeriesType5.Checked = False
                        OptSeriesType2_CheckedChanged(Me, New EventArgs)
                        lstSheets_SelectedIndexChanged(Me, New EventArgs)
                        Exit Sub
                    End If
            End Select

            If Not (Session("CmbCountryCol.SelectedIndex") = Nothing) Then
                CmbCountryCol.SelectedIndex = Session("CmbCountryCol.SelectedIndex")
            End If

            If Not (Session("CmbCountryRow.SelectedIndex") = Nothing) Then
                CmbCountryRow.SelectedIndex = Session("CmbCountryRow.SelectedIndex")
            End If

            If CmbCountryCol.SelectedIndex >= 0 And CmbCountryRow.SelectedIndex >= 0 Then
                CmbCountryCol_SelectedIndexChanged(Me, New EventArgs)
            End If

            If Not (Session("CmbYearCol.SelectedIndex") = Nothing) Then
                CmbYearCol.SelectedIndex = Session("CmbYearCol.SelectedIndex")
            End If

            If Not (Session("CmbYearRow.SelectedIndex") = Nothing) Then
                CmbYearRow.SelectedIndex = Session("CmbYearRow.SelectedIndex")
            End If

            If CmbYearCol.SelectedIndex >= 0 And CmbYearRow.SelectedIndex >= 0 Then
                CmbYearCol_SelectedIndexChanged(Me, New EventArgs)
            End If

            'jrs 2024/02/14
            If cmbDataCol.SelectedIndex >= 0 And cmbDataRow.SelectedIndex >= 0 Then
                cmbDataCol_SelectedIndexChanged(Me, New EventArgs)
            End If

            Session("lstSheets.SelectedIndex") = lstSheets.SelectedIndex
            'jrs 2023/11/01
            If Page.IsPostBack Then
                CmdImport.Text = "Import"
                Session("Accepted") = False
                Session("ORDecPlaces") = ""
                Session("ORYearsCovered") = ""
                Session("ORAggRule") = ""
                Session("ORVarDefn") = ""
                Session("ORVarName") = ""
                Session("DataDictRow") = ""
            End If
        End If
    End Sub

    Private Sub CmbCountryCol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCountryCol.SelectedIndexChanged
        If CmbCountryCol.SelectedIndex >= 0 And CmbCountryRow.SelectedIndex >= 0 Then
            Dim dataTable As DataTable
            dataTable = Session("dataTable")
            If IsDBNull(dataTable.Rows(CmbCountryRow.SelectedIndex).ItemArray(CmbCountryCol.SelectedIndex)) Then
                TxtCountryFR.Text = ""
            Else
                TxtCountryFR.Text = dataTable.Rows(CmbCountryRow.SelectedIndex).ItemArray(CmbCountryCol.SelectedIndex)
            End If
            Session("CmbCountryCol.SelectedIndex") = CmbCountryCol.SelectedIndex
        End If
    End Sub

    Private Sub CmbCountryRow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCountryRow.SelectedIndexChanged
        If CmbCountryCol.SelectedIndex >= 0 And CmbCountryRow.SelectedIndex >= 0 Then
            Dim dataTable As DataTable
            dataTable = Session("dataTable")
            If IsDBNull(dataTable.Rows(CmbCountryRow.SelectedIndex).ItemArray(CmbCountryCol.SelectedIndex)) Then
                TxtCountryFR.Text = ""
            Else
                TxtCountryFR.Text = dataTable.Rows(CmbCountryRow.SelectedIndex).ItemArray(CmbCountryCol.SelectedIndex)
            End If
            Session("CmbCountryRow.SelectedIndex") = CmbCountryRow.SelectedIndex
        End If
    End Sub

    Private Sub CmbYearCol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbYearCol.SelectedIndexChanged
        If CmbYearCol.SelectedIndex >= 0 And CmbYearRow.SelectedIndex >= 0 Then
            Dim dataTable As DataTable
            dataTable = Session("dataTable")
            If IsDBNull(dataTable.Rows(CmbYearRow.SelectedIndex).ItemArray(CmbYearCol.SelectedIndex)) Then
                TxtYearFR.Text = ""
            Else
                TxtYearFR.Text = dataTable.Rows(CmbYearRow.SelectedIndex).ItemArray(CmbYearCol.SelectedIndex)
            End If
            Session("CmbYearCol.SelectedIndex") = CmbYearCol.SelectedIndex
            'jrs 2024/03/01
            YearsUniqueFilterNSort()
        End If
    End Sub

    Private Sub lstVariablelist_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstVariablelist.SelectedIndexChanged
        If lstVariablelist.SelectedIndex > 0 Then
            TxtVarName.Text = Session("rsDataDict").rows(lstVariablelist.SelectedIndex - 1).item(0)
            TxtVarDesc.Text = Session("rsDataDict").rows(lstVariablelist.SelectedIndex - 1).item(1)
            'jrs 2024/02/20 cmbaggregation.Enabled = False
            For i = 0 To cmbaggregation.Items.Count - 1
                If cmbaggregation.Items(i).Text = Session("rsDataDict").rows(lstVariablelist.SelectedIndex - 1).item(2) Then
                    cmbaggregation.SelectedIndex = i
                    Exit For
                End If
            Next
            'jrs 2024/02/20 cmbDecimals.Enabled = False
            For i = 0 To cmbDecimals.Items.Count - 1
                If cmbDecimals.Items(i).Text = Session("rsDataDict").rows(lstVariablelist.SelectedIndex - 1).item(3).ToString Then
                    cmbDecimals.SelectedIndex = i
                    Exit For
                End If
            Next
            txtTable.Text = "Series" + TxtVarName.Text
        Else
            If Session("ImportXLSVarName") = Nothing Then
                TxtVarName.Text = ""
                TxtVarDesc.Text = ""
                cmbaggregation.Enabled = True
                cmbaggregation.SelectedIndex = 0
                cmbDecimals.Enabled = True
                cmbDecimals.SelectedIndex = 0
            Else
                TxtVarName.Text = Session("ImportXLSVarName")
                TxtVarDesc.Text = Session("ImportXLSVarDesc")
                cmbaggregation.Enabled = True
                cmbaggregation.SelectedIndex = Session("ImportXLSAggr")
                cmbDecimals.Enabled = True
                cmbDecimals.SelectedIndex = Session("ImportXLSDecimals")
                txtTable.Text = "Series" + TxtVarName.Text
            End If
        End If
    End Sub

    Protected Function IsValidVar() As Boolean
        ErrorText = ""
        If TxtCountryFR.Text = "" Then
            ErrorText = "<script>alert('Please specify the start row and column for Countries.');</script>"
            Return False
        End If

        If TxtYearFR.Text = "" Then
            ErrorText = "<script>alert('Please specify the start row and column for Years.');</script>"
            Return False
        End If

        If TxtVarName.Text = "" Then
            ErrorText = "<script>alert('Please type a variable name.');</script>"
            Return False
        End If

        If TxtVarName.Text.Contains(" ") Then
            ErrorText = "<script>alert('Please don't use spaces in variable name.');</script>"
            Return False
        End If

        If TxtVarDesc.Text = "" Then
            ErrorText = "<script>alert('Please specify variable description.');</script>"
            Return False
        End If


        Return True
    End Function


    Private Sub CmdImport_Click(sender As Object, e As EventArgs) Handles CmdImport.Click
        If CmdImport.Text = "Import" Then
            If IsValidVar() = False Then
                'jrs 2023/05/10 Response.Write(ErrorText)
                lblMessages.Text = ErrorText
                lblMessages.Visible = True

                Exit Sub
            End If

            Session("ImportSuccess") = False

            'Reset file
            'jrs 2023/05/10 FileCopy(DataFileDir & txtDatabaseFile.Text, Session("PutFdrvRun") & txtDatabaseFile.Text)
            'jrs 2026/07/23
            If Not File.Exists(Session("PutFdrvRun") & txtDatabaseFile.Text) Then
                FileCopy(DataFileDir & txtDatabaseFile.Text, Session("PutFdrvRun") & txtDatabaseFile.Text)
            End If

            Select Case Session("SeriesType")
                Case 0  'Muliple Series Down, Regions Across, Time Down
                    'ImportZero
                Case 1
                    'ImportOne
                Case 2
                    ImportTwo()
                Case 3
                    'ImportThree
                Case 4
                    'ImportFour
                Case 5
                    ImportFive()
            End Select

            If Not Session("ImportSuccess") Then Exit Sub

            ShowDataDict(True, AllYear, "NEWDATE")  'save allowed
        End If

        'jrs 2026/07/23
        Session("ImportParent") = "Frm_ImportXLSNewProvinces.aspx"

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ViewData.aspx")

    End Sub

    Private Sub ImportTwo()
        '------------------------------------------------------------------------------------------
        'Multiple Series Across, Time Within Region

        'Change by MTI on 27 Nov 2001
        Dim TVarVal

        'Dim tdef As TableDef
        'Dim idxCountry As Index
        Dim field_names As String
        Dim field_name As String
        Dim pos As Integer
        Dim i As Integer
        Dim j As Integer
        Dim TableAppended As Boolean

        Dim XLLastCountry_name As String
        Dim XLCountry_name As String
        Dim Adddata As Boolean
        Dim Country_Name As String
        Dim YearFld_Name As String
        Dim VarVal As Single?
        Dim qsql As String
        'Dim temprange As Range
        Dim Numrows As Integer
        Dim numcols As Integer
        'Dim Datarray()
        Dim TmpSeries As String
        Dim SeriesName As String
        Dim db1 As SQLiteConnection
        Dim UpdTable As SQLiteCommand
        Dim valtblCmd As SQLiteCommand
        Dim valtbl As SQLiteDataReader
        Dim Status$, StatusHead$


        Status$ = ""
        On Error GoTo NotCreated


        'Screen.MousePointer = 11

        'Check if this variable/table already Exists
        db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & txtDatabaseFile.Text & ";", String)) '& "New=true;"
        db1.Open()

        Session("txtDatabaseFile") = txtDatabaseFile.Text

        UpdTable = db1.CreateCommand
        valtblCmd = db1.CreateCommand

        Using cmd2 = db1.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        'jrs 2023/08/14 *** start ***
        'Check if no DataDict, then it's probably an empty file, copy the one from DATA
        valtblCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE name = 'DataDict'"
        valtbl = valtblCmd.ExecuteReader
        valtbl.Read()

        If valtbl(0) > 0 Then
            'jrs 2024/02/20 this was reversed and it was initializing the file
            valtbl.Close()
        Else
            valtbl.Close()
            db1.Close()
            FileCopy(DataFileDir & txtDatabaseFile.Text, Session("PutFdrvRun") & txtDatabaseFile.Text)
            db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & txtDatabaseFile.Text & ";", String)) '& "New=true;"
            db1.Open()

            UpdTable = db1.CreateCommand
            valtblCmd = db1.CreateCommand

            Using cmd2 = db1.CreateCommand()
                cmd2.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
                cmd2.ExecuteNonQuery()
            End Using
        End If
        'jrs 2023/08/14 *** end ***

        valtblCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE name = '" & txtTable.Text & "'"
        valtbl = valtblCmd.ExecuteReader
        valtbl.Read()

        If valtbl(0) > 0 Then
            valtbl.Close()
            'jrs 2024/03/01 added []
            'Delete existing table
            UpdTable.CommandText = "DROP TABLE [" & txtTable.Text & "]"
            UpdTable.ExecuteNonQuery()
        Else
            valtbl.Close()
        End If



        'mti February 2011 Infarstructure
        StatusHead$ = ErrHeader("Multiple Series Across, Time Within Region")
        Status$ = StatusHead$ + "Adding country and year columns " & vbCrLf
        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Creating table, " + vbCrLf


        ' Create a new TableDef object.
        Dim sCreate As String = "CREATE TABLE """ & txtTable.Text & """ (""Country"" VARCHAR(255), ""FIPS_CODE"" VARCHAR(255)"

        Session("txtTable") = txtTable.Text

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Reading year columns from Excel, " + vbCrLf


        'mti December 2004 for data dictionary
        AllYear$ = ""
        Dim LastYearFld%, YearIn%

        'mti June 2009 start
        '    'mti 28 November 2001 - Start
        '    Dim CYear%
        '    CYear = Year(Date)
        'mti June 2009 end

        Dim YearCol = CmbYearCol.SelectedIndex
        Dim YearRow = CmbYearRow.SelectedIndex

        'jrs 2023/01/23
        Dim EmptyFields As List(Of Integer) = New List(Of Integer)

        For i = 1 To Session("NumYears")   'Val(CmbYearRow.Text)
            'mti June 2006 New type -end
            'jrs 2023/04/27
            'mti June 20, 2013 -start
            'field_name = Trim(Str(excel_app.ActiveWorkbook.ActiveSheet.Cells(YearRow, i).Value))
            field_name = Session("lstYears")(i - 1).ToString
            'mti June 20, 2013 - end


            Status$ = Status$ + "Adding year field " + field_name & vbCrLf
            If field_name <> "0" And field_name <> "" _
                And Val(field_name) > 0 And Val(field_name) <= (Year(Now()) + 100) Then
                'jrs 2023/01/23
                'Detect Empty fields
                'For jj = CmbCountryRow.SelectedIndex + 1 To Numrows
                '    VarVal = Session("dataTable").rows(jj).items(i)
                '    If VarVal.HasValue Then
                '        Exit For
                '    End If
                '    If jj = Numrows Then
                '        EmptyFields.Add(CInt(field_name))
                '    End If
                'Next jj

                sCreate = sCreate & ", """ & field_name & """ DOUBLE(53)"
                'mti December 2004 for data dictionary
                If AllYear$ = "" Then
                    AllYear$ = field_name
                    YearIn% = 1
                    LastYearFld = Val(field_name)
                Else
                    If Val(field_name) - 1 = LastYearFld Then
                        '                FirstYearfld = field_name
                        LastYearFld = Val(field_name)
                        YearIn% = YearIn% + 1
                    Else
                        If YearIn% > 1 Then
                            AllYear$ = AllYear$ + "-" + Trim(Str(LastYearFld))
                            AllYear$ = AllYear$ + "," + field_name
                            YearIn% = 1
                            LastYearFld = Val(field_name)
                        Else
                            AllYear$ = AllYear$ + "," + field_name
                            YearIn% = 1
                            LastYearFld = Val(field_name)
                        End If
                    End If
                End If
            Else
                'Exit For 'mti June 23, 2006
            End If
        Next i
        'mti December 2004 for data dictionary -write the last one
        If YearIn% > 1 Then
            AllYear$ = AllYear$ + "-" + Trim(Str(LastYearFld))
        Else
            AllYear$ = AllYear$ '+ "-" + Trim(Str(LastYearFld))
        End If

        'mti 28 November 2001 - End

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Creating Earliest and Most Recent Columns, " + vbCrLf


        'mti February 2011 Infarstructure - start
        sCreate = sCreate & ", ""Earliest"" DOUBLE(53)"
        sCreate = sCreate & ", ""MostRecent"" DOUBLE(53));"
        'mti February 2011 Infarstructure - end

        ' Add the TableDef to the database.
        UpdTable.CommandText = sCreate
        UpdTable.ExecuteNonQuery()

        TableAppended = True

        'Excel Data Being Taken in Array
        '--------------------------------
        Status$ = StatusHead$ + "Reading Excel Data Range in an Array" & vbCrLf

        Numrows = Session("dataTable").Rows.Count - 1
        numcols = Session("dataTable").Columns.Count - 1


        'mti August 22, 2011 -start
        Status$ = StatusHead$ + "Adding temporary index to Country Translation table" & vbCrLf



        'create Index on Country
        'sCreate = "CREATE INDEX Country ON " & txtTable.Text & " (Country);"
        'UpdTable.CommandText = sCreate
        'UpdTable.ExecuteNonQuery()


        Status$ = StatusHead$ + "Adding Country rows in new IFs data Table" & vbCrLf
        'mti August 22, 2011 - end
        'Insert the Countries into the New Table

        Dim dbIfs As SQLiteConnection
        dbIfs = New SQLiteConnection(CType("DataSource=" + Session("Prov_Country_MDB") & ";", String))
        dbIfs.Open()
        Dim CTcmd As SQLiteCommand
        CTcmd = dbIfs.CreateCommand
        CTcmd.CommandText = "SELECT Province, FIPS_CODE FROM [Provinces] WHERE Country = """ & Session("Country4Provinces") & """ ORDER BY Province"
        Dim rsCT As SQLiteDataReader
        rsCT = CTcmd.ExecuteReader

        Do While rsCT.Read
            qsql = " INSERT INTO [" + txtTable.Text +
                 "] (Country, FIPS_CODE) " +
                 "VALUES(""" & rsCT(0) & """, """ & rsCT(1) & """);"

            UpdTable.CommandText = qsql
            UpdTable.ExecuteNonQuery()
        Loop
        rsCT.Close()

        'Status$ = StatusHead$ + "Setting Temporary Index to Country Translation " & vbCrLf

        '------------------------
        '    qsql = " CREATE INDEX Idx_Countries ON [Country Translation] ([" _
        '            + CmbCountryList.Text + "]);"
        '    db1.Execute qsql   'db = IfsHistSeries.mdb
        '
        '    Set valtbl = db1.OpenRecordset("Country Translation")
        '    'valtbl.Index = CmbCountryList.Text
        '    valtbl.Index = "Idx_Countries"



        '--------------------

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Reading data from Excel, " + vbCrLf

        Status$ = StatusHead$ + "Beginning to import data " & vbCrLf

        'jrs 2023/08/10
        'Open new table for update
        qsql = "UPDATE [" & txtTable.Text & "] SET "

        'jrs 2007/12/18 removing FIPS_CODE index UpdTable.Index = "FIPS_CODE"

        XLLastCountry_name = ""

        'Start Taking Excel DAta from Array


        'mti 28 November 2001
        On Error Resume Next

        'mti August 22, 2011 - start
        'mismatched countries
        Dim MMC() As String
        ReDim MMC(50)
        Dim MMCCntr%
        MMCCntr = 0
        'mti August 22, 2011 - end

        'jrs 2023/05/04 - start
        'matched countries
        Dim MMC2(,) As String
        ReDim MMC2(NR * 2, 2) 'jrs 2024/02/14
        Dim MMCCntr2%
        MMCCntr2 = 0
        'jrs 2023/05/04 - end

        'jrs 2023/05/10
        Dim MMCcol, MMC2col As List(Of String)
        MMCcol = New List(Of String)
        MMC2col = New List(Of String)


        Dim sWHERE As String = ""
        Dim FldNames As String
        Dim Earliest As Dictionary(Of String, Single) = New Dictionary(Of String, Single)
        Dim MostRecent As Dictionary(Of String, Single) = New Dictionary(Of String, Single)


        For j = CmbYearRow.SelectedIndex To Numrows


            XLCountry_name = Trim(Session("dataTable").Rows(j).Item(CmbCountryCol.SelectedIndex))

            CTcmd.CommandText = "Select Province FROM Provinces WHERE Province = """ & XLCountry_name & """"
            rsCT = CTcmd.ExecuteReader

            If Not rsCT.HasRows Then
                rsCT.Close()
                Adddata = False

                'jrs 2023/05/10 addeed and not contains
                'mti August 22, 2011 - start
                If MMCCntr < 50 And Not MMCcol.Contains(XLCountry_name) Then
                    MMC(MMCCntr) = XLCountry_name
                    MMCCntr = MMCCntr + 1
                    'jrs 2023/05/10
                    MMCcol.Add(XLCountry_name)
                End If
                'mti August 22, 2011 - end

                'jrs 2023/04/27
                'Keep advancing rows until last row of non-existing country
                Do While Session("dataTable").Rows(j).Item(CmbCountryCol.SelectedIndex) = Session("dataTable").Rows(j + 1).Item(CmbCountryCol.SelectedIndex)
                    j = j + 1
                Loop
            Else
                'jrs 2007/12/18 removing FIPS_CODE index Country_Name = valtbl.Fields("FIPS_CODE")
                rsCT.Read()

                Country_Name = rsCT(0)
                rsCT.Close()

                'jrs 2023/05/10
                If Not MMC2col.Contains(XLCountry_name) Then
                    'jrs 2023/05/04
                    If MMCCntr2 > UBound(MMC2, 1) Then
                        ReDim Preserve MMC2(MMCCntr2, 2)
                    End If
                    MMC2(MMCCntr2, 1) = XLCountry_name
                    MMC2(MMCCntr2, 2) = Country_Name
                    MMCCntr2 = MMCCntr2 + 1
                    'jrs 2023/05/10
                    MMC2col.Add(XLCountry_name)
                End If


                sWHERE = " WHERE Country = """ & Country_Name & """"

                Status$ = StatusHead$ + "Attempting to import data for " + Country_Name & vbCrLf

                Dim iLastIdx As Integer = -1

                i = YearCol

                YearFld_Name = Session("dataTable").rows(j).item(i)

                'jrs 2024/01/09 i = 2
                i = cmbDataCol.SelectedIndex

                'jrs 2023/10/17
                If IsDBNull(Session("dataTable").rows(j).item(i)) Then
                    VarVal = Nothing
                Else
                    VarVal = CSng(Session("dataTable").rows(j).item(i))
                End If
                'Change by MTI on 27 Nov 2001 -Start
                If TypeName(VarVal) = "String" Then
                    'make sure the string contains a number, not some text
                    TVarVal = Val(VarVal)
                    If Trim(Str(TVarVal)) = VarVal Then
                        VarVal = TVarVal
                    End If
                End If
                'Change by MTI on 27 Nov 2001 -End

                'jrs 2023/10/17
                If YearFld_Name.ToUpper <> "EARLIEST" And YearFld_Name.ToUpper <> "MOSTRECENT" Then
                    If TypeName(VarVal) <> "Error" And TypeName(VarVal) <> "String" Then

                        'If Not MisVal(VarVal) Then
                        If VarVal.HasValue Then
                            iLastIdx = j
                            If FldNames > "" Then
                                FldNames = FldNames & ", "
                            Else
                                Earliest.Add(Country_Name, PrepValue(VarVal))
                            End If
                            FldNames = FldNames & "[" & YearFld_Name & "] = " & PrepValue(VarVal)

                            '                Select Case Trim(CmbScaleOp.Text)
                            '                Case "+"
                            '                  UpdTable.Fields(YearFld_Name) = VarVal + Val(TxtScaleFactor.Text)
                            '                Case "-"
                            '                  UpdTable.Fields(YearFld_Name) = VarVal - Val(TxtScaleFactor.Text)
                            '                Case "*"
                            '                  UpdTable.Fields(YearFld_Name) = VarVal * Val(TxtScaleFactor.Text)
                            '                Case "/"
                            '                  UpdTable.Fields(YearFld_Name) = VarVal / (Val(TxtScaleFactor.Text))
                            '                Case Else
                            '                  UpdTable.Fields(YearFld_Name) = VarVal
                            '                End Select
                        End If   'Missing Value
                    End If
                End If
                'jrs 2023/10/17

                'Check also if it's last row of the same country
                If FldNames > "" And Session("dataTable").Rows(j).Item(CmbCountryCol.SelectedIndex) <> Session("dataTable").Rows(j + 1).Item(CmbCountryCol.SelectedIndex) Then
                    'jrs 2024/03/12 added PrepValue to round MostRecent
                    MostRecent.Add(Country_Name, PrepValue(Session("dataTable").rows(iLastIdx).item(i)))
                    UpdTable.CommandText = qsql & FldNames & sWHERE
                    UpdTable.ExecuteNonQuery()
                    FldNames = ""
                End If
            End If
        Next j

        Session("MMC") = MMC
        Session("MMCCntr") = MMCCntr
        'jrs 2023/05/04
        Session("MMC2") = MMC2
        Session("MMCCntr2") = MMCCntr2

        'mti 28 November 2001
        On Error GoTo NotCreated

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Populating Earliest and Most Recent, " + vbCrLf

        Status$ = StatusHead$ + "Calculating Earliest" & vbCrLf
        'mti February 2011 Infarstructure - start
        'AddEarliestData UpdTable, tdef
        For Each item In Earliest
            FldNames = " Earliest = " & item.Value.ToString
            sWHERE = " WHERE Country = """ & item.Key & """"
            UpdTable.CommandText = qsql & FldNames & sWHERE
            UpdTable.ExecuteNonQuery()
        Next

        Status$ = StatusHead$ + "Calculating MostRecent" & vbCrLf
        'AddMostRecentData UpdTable, tdef

        For Each item In MostRecent
            FldNames = " MostRecent = " & item.Value.ToString
            sWHERE = " WHERE Country = """ & item.Key & """"
            UpdTable.CommandText = qsql & FldNames & sWHERE
            UpdTable.ExecuteNonQuery()
        Next

        'mti February 2011 Infarstructure - end


        'drop temporary index built in Country Translation ------------
        '    qsql = " DROP INDEX Idx_Countries ON [Country Translation];"
        '    db1.Execute qsql   'db = IfsHistSeries.mdb

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Dropping Temporary Translation Index, " + vbCrLf

        'Status$ = StatusHead$ + "Deleting Temporary Country Translation Index<br/>" 

        '        OpenNIndexRecordset db1, valtbl, "Idx_Countries",
        '    "Country Translation", lstCountrylist.Text, True

        '--------------------

        db1.Close()
        dbIfs.Close()


        'Screen.MousePointer = 0

        'mti June 20, 2013
        Session("ImportSuccess") = True

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Showing Imported Data, " + vbCrLf

        '    'mti February 2011 Infarstructure
        '    'Show the Imported Data in the Table
        '    Status$ = StatusHead$ + "Going to display data" + vbCrLf
        '    Frm_ViewData.DisplayTopic = "IMPORTEDDATA"
        '
        '    'mti Sep 17, 2012 - comply - start
        '    ImportDataTblName$ = txtTable.Text
        '    'mti Sep 17, 2012 - end
        '
        '    Frm_ViewData.Show vbModal

        '    If Accepted Then
        '        'db.Close
        '
        '        'update data dictionary
        '        frm_Datadict.cmdOK.Caption = "Accept"
        '        frm_Datadict.cmdCancel.Caption = "Cancel"
        '        frm_Datadict.Show vbModal
        '
        '        If Accepted Then
        '            'Write the table in IFSHIStSeries.mdb in the Run Files Directory
        '            db.Close
        '            Set db = Nothing
        '            Kill PutFdrvRun$ + "IFsHistSeries.mdb"
        '            FileCopy DataFileDir$ + "IFsHistSeries.mdb", PutFdrvRun$ + "IFsHistSeries.mdb"
        '            Set db = OpenDatabase(DataFileDir$ + txtDatabaseFile.Text, False, False, DataPW$)
        '        Else
        '            db.TableDefs.Delete txtTable.Text
        '        End If
        '
        '    Else  'Delete the New Table
        '            db.TableDefs.Delete txtTable.Text
        '
        '    End If


        Exit Sub


NotCreated:
        'mti February 2011 Infarstructure - start
        Status$ = Status$ + vbCrLf + vbCrLf +
           "Specific Error Message: " + vbCrLf +
            Err.Description + vbCrLf + vbCrLf


        On Error Resume Next
        i = FreeFile()
        FileOpen(i, DataFileDir$ + "ImportXLSProvincesError.Txt", OpenMode.Output)
        PrintLine(i, Status)
        FileClose(i)

        Dim Msg As String
        Msg = "<script>alert('Error creating table. \n" +
           "Please make sure everything is chosen properly.\n\n" +
            "An error log is saved at " + DataFileDir$ + "ImportXLSProvincesError.Txt');</script>"

        'mti February 2011 Infarstructure - end

        UpdTable.CommandText = "DROP TABLE " & txtTable.Text
        UpdTable.ExecuteNonQuery()

        db1.Close()
        dbIfs.Close()

        Response.Write(Msg)

    End Sub

    Private Sub ImportFive()
        '------------------------------------------------------------------------------------------
        'Single Series Regions Down Time Across

        'Change by MTI on 27 Nov 2001
        Dim TVarVal

        'Dim tdef As TableDef
        'Dim idxCountry As Index
        Dim field_names As String
        Dim field_name As String
        Dim pos As Integer
        Dim i As Integer
        Dim j As Integer
        Dim TableAppended As Boolean

        Dim XLLastCountry_name As String
        Dim XLCountry_name As String
        Dim Adddata As Boolean
        Dim Country_Name As String
        Dim YearFld_Name As String
        Dim VarVal As Single?
        Dim qsql As String
        'Dim temprange As Range
        Dim Numrows As Integer
        Dim numcols As Integer
        'Dim Datarray()
        Dim TmpSeries As String
        Dim SeriesName As String
        Dim db1 As SQLiteConnection
        Dim UpdTable As SQLiteCommand
        Dim valtblCmd As SQLiteCommand
        Dim valtbl As SQLiteDataReader
        Dim Status$, StatusHead$


        Status$ = ""
        On Error GoTo NotCreated

        'jrs 2024/02/14
        Dim MMCcol, MMC2col As List(Of String)
        MMCcol = New List(Of String)
        MMC2col = New List(Of String)

        'Screen.MousePointer = 11

        'Check if this variable/table already Exists
        db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & txtDatabaseFile.Text & ";", String)) '& "New=true;"
        db1.Open()

        Session("txtDatabaseFile") = txtDatabaseFile.Text

        UpdTable = db1.CreateCommand
        valtblCmd = db1.CreateCommand

        Using cmd2 = db1.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        'jrs 2023/08/14 *** start ***
        'Check if no DataDict, then it's probably an empty file, copy the one from DATA
        valtblCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE name = 'DataDict'"
        valtbl = valtblCmd.ExecuteReader
        valtbl.Read()

        If valtbl(0) > 0 Then
            valtbl.Close()
        Else
            valtbl.Close()
            db1.Close()
            FileCopy(DataFileDir & txtDatabaseFile.Text, Session("PutFdrvRun") & txtDatabaseFile.Text)
            db1 = New SQLiteConnection(CType("DataSource=" + Session("PutFdrvRun") & txtDatabaseFile.Text & ";", String)) '& "New=true;"
            db1.Open()

            UpdTable = db1.CreateCommand
            valtblCmd = db1.CreateCommand

            Using cmd2 = db1.CreateCommand()
                cmd2.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
                cmd2.ExecuteNonQuery()
            End Using
        End If
        'jrs 2023/08/14 *** end ***

        valtblCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE name = '" & txtTable.Text & "'"
        valtbl = valtblCmd.ExecuteReader
        valtbl.Read()

        If valtbl(0) > 0 Then
            valtbl.Close()
            'jrs 2023/08/10
            'Delete existing table
            UpdTable.CommandText = "DROP TABLE [" & txtTable.Text & "]"
            UpdTable.ExecuteNonQuery()
        Else
            valtbl.Close()
        End If



        'mti February 2011 Infarstructure
        StatusHead$ = ErrHeader("Single Series Regions Down Time Across")
        Status$ = StatusHead$ + "Adding country and year columns " & vbCrLf
        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Creating table, " + vbCrLf


        ' Create a new TableDef object.
        Dim sCreate As String = "CREATE TABLE """ & txtTable.Text & """ (""Country"" VARCHAR(255), ""FIPS_CODE"" VARCHAR(255)"

        Session("txtTable") = txtTable.Text

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Reading year columns from Excel, " + vbCrLf


        'mti December 2004 for data dictionary
        AllYear$ = ""
        Dim LastYearFld%, YearIn%

        'mti June 2009 start
        '    'mti 28 November 2001 - Start
        '    Dim CYear%
        '    CYear = Year(Date)
        'mti June 2009 end

        'mti June 2006 New type -start
        'If chkUseEndYear.value = 0 Or YearEndCol <= 0 Then
        'jrs 2023/01/23 dim YearEndCol = 255 YearCol includes country and fips_code too
        Dim YearEndCol As Integer = CmbYearCol.Items.Count - 1
        'End If
        Dim YearCol = CmbYearCol.SelectedIndex
        Dim YearRow = CmbYearRow.SelectedIndex

        'jrs 2023/01/23
        Dim EmptyFields As List(Of Integer) = New List(Of Integer)

        For i = YearCol To YearEndCol ' 255  'Years in Column Headers
            'mti June 2006 New type -end

            'mti June 20, 2013 -start
            'field_name = Trim(Str(excel_app.ActiveWorkbook.ActiveSheet.Cells(YearRow, i).Value))
            field_name = Session("dataTable").rows(YearRow).item(i).ToString.Trim
            'mti June 20, 2013 - end


            Status$ = Status$ + "Adding year field " + field_name & vbCrLf
            If field_name <> "0" And field_name <> "" _
                And Val(field_name) > 0 And Val(field_name) <= (Year(Now()) + 100) Then
                'jrs 2023/01/23
                'Detect Empty fields
                'For jj = CmbCountryRow.SelectedIndex + 1 To Numrows
                '    VarVal = Session("dataTable").rows(jj).items(i)
                '    If VarVal.HasValue Then
                '        Exit For
                '    End If
                '    If jj = Numrows Then
                '        EmptyFields.Add(CInt(field_name))
                '    End If
                'Next jj

                sCreate = sCreate & ", """ & field_name & """ DOUBLE(53)"
                'mti December 2004 for data dictionary
                If AllYear$ = "" Then
                    AllYear$ = field_name
                    YearIn% = 1
                    LastYearFld = Val(field_name)
                Else
                    If Val(field_name) - 1 = LastYearFld Then
                        '                FirstYearfld = field_name
                        LastYearFld = Val(field_name)
                        YearIn% = YearIn% + 1
                    Else
                        If YearIn% > 1 Then
                            AllYear$ = AllYear$ + "-" + Trim(Str(LastYearFld))
                            AllYear$ = AllYear$ + "," + field_name
                            YearIn% = 1
                            LastYearFld = Val(field_name)
                        Else
                            AllYear$ = AllYear$ + "," + field_name
                            YearIn% = 1
                            LastYearFld = Val(field_name)
                        End If
                    End If
                End If
            Else
                'Exit For 'mti June 23, 2006
            End If
        Next i
        'mti December 2004 for data dictionary -write the last one
        If YearIn% > 1 Then
            AllYear$ = AllYear$ + "-" + Trim(Str(LastYearFld))
        Else
            AllYear$ = AllYear$ '+ "-" + Trim(Str(LastYearFld))
        End If

        'mti 28 November 2001 - End

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Creating Earliest and Most Recent Columns, " + vbCrLf


        'mti February 2011 Infarstructure - start
        sCreate = sCreate & ", ""Earliest"" DOUBLE(53)"
        sCreate = sCreate & ", ""MostRecent"" DOUBLE(53));"
        'mti February 2011 Infarstructure - end

        ' Add the TableDef to the database.
        UpdTable.CommandText = sCreate
        UpdTable.ExecuteNonQuery()

        TableAppended = True

        'Excel Data Being Taken in Array
        '--------------------------------
        Status$ = StatusHead$ + "Reading Excel Data Range in an Array" & vbCrLf

        Numrows = Session("dataTable").Rows.Count - 1
        numcols = Session("dataTable").Columns.Count - 1


        'mti August 22, 2011 -start
        Status$ = StatusHead$ + "Adding temporary index to Country Translation table" & vbCrLf



        'create Index on Country
        'sCreate = "CREATE INDEX Country ON " & txtTable.Text & " (Country);"
        'UpdTable.CommandText = sCreate
        'UpdTable.ExecuteNonQuery


        Status$ = StatusHead$ + "Adding Country rows in new IFs data Table" & vbCrLf
        'mti August 22, 2011 - end
        'Insert the Countries into the New Table

        Dim dbIfs As SQLiteConnection
        dbIfs = New SQLiteConnection(CType("DataSource=" + Session("Prov_Country_MDB") & ";", String))
        dbIfs.Open()
        Dim CTcmd As SQLiteCommand
        CTcmd = dbIfs.CreateCommand
        CTcmd.CommandText = "SELECT Province, FIPS_CODE FROM [Provinces] WHERE Country = """ & Session("Country4Provinces") & """ ORDER BY Province"
        Dim rsCT As SQLiteDataReader
        rsCT = CTcmd.ExecuteReader

        Do While rsCT.Read
            qsql = " INSERT INTO [" + txtTable.Text +
                 "] (Country, FIPS_CODE) " +
                 "VALUES(""" & rsCT(0) & """, """ & rsCT(1) & """);"

            UpdTable.CommandText = qsql
            UpdTable.ExecuteNonQuery()
        Loop
        rsCT.Close()

        'Status$ = StatusHead$ + "Setting Temporary Index To Country Translation " & vbCrLf

        '------------------------
        '    qsql = " CREATE INDEX Idx_Countries On [Country Translation] ([" _
        '            + CmbCountryList.Text + "]);"
        '    db1.Execute qsql   'db = IfsHistSeries.mdb
        '
        '    Set valtbl = db1.OpenRecordset("Country Translation")
        '    'valtbl.Index = CmbCountryList.Text
        '    valtbl.Index = "Idx_Countries"



        '--------------------

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Reading data from Excel, " + vbCrLf

        Status$ = StatusHead$ + "Beginning To import data " & vbCrLf

        'jrs 2023/08/10
        'Open new table for update
        qsql = "UPDATE [" & txtTable.Text & "] Set "

        'jrs 2007/12/18 removing FIPS_CODE index UpdTable.Index = "FIPS_CODE"

        XLLastCountry_name = ""

        'Start Taking Excel DAta from Array


        'mti 28 November 2001
        On Error Resume Next

        'mti August 22, 2011 - start
        'mismatched countries
        Dim MMC() As String
        ReDim MMC(50)
        Dim MMCCntr%
        MMCCntr = 0
        'mti August 22, 2011 - end

        'jrs 2023/05/04 - start
        'matched countries
        Dim MMC2(,) As String
        ReDim MMC2(NR * 2, 2) 'jrs 2024/02/14
        Dim MMCCntr2%
        MMCCntr2 = 0
        'jrs 2023/05/04 - end

        Dim sWHERE As String = ""
        Dim FldNames As String
        Dim Earliest As Dictionary(Of String, Single) = New Dictionary(Of String, Single)
        Dim MostRecent As Dictionary(Of String, Single) = New Dictionary(Of String, Single)

        For j = CmbCountryRow.SelectedIndex To Numrows


            XLCountry_name = Trim(Session("dataTable").Rows(j).Item(CmbCountryCol.SelectedIndex))
            FldNames = ""

            CTcmd.CommandText = "Select Province FROM Provinces WHERE Province = """ & XLCountry_name & """"
            rsCT = CTcmd.ExecuteReader

            If Not rsCT.HasRows Then
                rsCT.Close()
                Adddata = False

                'jrs 2024/02/14 added and not contains
                'mti August 22, 2011 - start
                If MMCCntr < 50 And Not MMCcol.Contains(XLCountry_name) Then
                    MMC(MMCCntr) = XLCountry_name
                    MMCCntr = MMCCntr + 1
                    'jrs 2024/02/14
                    MMCcol.Add(XLCountry_name)
                End If
                'mti August 22, 2011 - end
            Else
                'jrs 2007/12/18 removing FIPS_CODE index Country_Name = valtbl.Fields("FIPS_CODE")
                rsCT.Read()

                Country_Name = rsCT(0)
                rsCT.Close()

                'jrs 2024/02/14
                If Not MMC2col.Contains(XLCountry_name) Then
                    'jrs 2023/05/04
                    If MMCCntr2 > UBound(MMC2, 1) Then
                        ReDim Preserve MMC2(MMCCntr2, 2)
                    End If

                    'jrs 2023/05/04
                    MMC2(MMCCntr2, 1) = XLCountry_name
                    MMC2(MMCCntr2, 2) = Country_Name
                    MMCCntr2 = MMCCntr2 + 1
                    'jrs 2024/02/14
                    MMC2col.Add(XLCountry_name)
                End If


                sWHERE = " WHERE Country = """ & Country_Name & """"

                Status$ = StatusHead$ + "Attempting To import data For " + Country_Name & vbCrLf

                Dim iLastIdx As Integer = -1
                For i = YearCol To numcols 'number of yearflds

                    YearFld_Name = Session("dataTable").rows(YearRow).item(i)

                    'jrs 2023/10/17
                    If YearFld_Name.ToUpper <> "EARLIEST" And YearFld_Name.ToUpper <> "MOSTRECENT" Then
                        'jrs 2023/10/17
                        If IsDBNull(Session("dataTable").rows(j).item(i)) Then
                            VarVal = Nothing
                        Else
                            VarVal = CSng(Session("dataTable").rows(j).item(i))
                        End If
                        'Change by MTI on 27 Nov 2001 -Start
                        If TypeName(VarVal) = "String" Then
                            'make sure the string contains a number, not some text
                            TVarVal = Val(VarVal)
                            If Trim(Str(TVarVal)) = VarVal Then
                                VarVal = TVarVal
                            End If
                        End If
                        'Change by MTI on 27 Nov 2001 -End


                        If TypeName(VarVal) <> "Error" And TypeName(VarVal) <> "String" Then

                            'If Not MisVal(VarVal) Then
                            If VarVal.HasValue Then
                                iLastIdx = i
                                If FldNames > "" Then
                                    FldNames = FldNames & ", "
                                Else
                                    Earliest.Add(Country_Name, PrepValue(VarVal))
                                End If
                                FldNames = FldNames & "[" & YearFld_Name & "] = " & PrepValue(VarVal)

                                '                Select Case Trim(CmbScaleOp.Text)
                                '                Case "+"
                                '                  UpdTable.Fields(YearFld_Name) = VarVal + Val(TxtScaleFactor.Text)
                                '                Case "-"
                                '                  UpdTable.Fields(YearFld_Name) = VarVal - Val(TxtScaleFactor.Text)
                                '                Case "*"
                                '                  UpdTable.Fields(YearFld_Name) = VarVal * Val(TxtScaleFactor.Text)
                                '                Case "/"
                                '                  UpdTable.Fields(YearFld_Name) = VarVal / (Val(TxtScaleFactor.Text))
                                '                Case Else
                                '                  UpdTable.Fields(YearFld_Name) = VarVal
                                '                End Select
                            End If   'Missing Value
                        End If
                    End If
                Next i
                If FldNames > "" Then
                    'jrs 2024/03/12 added PrepValue to round MostRecent
                    MostRecent.Add(Country_Name, PrepValue(Session("dataTable").rows(j).item(iLastIdx)))
                    UpdTable.CommandText = qsql & FldNames & sWHERE
                    UpdTable.ExecuteNonQuery()
                End If
            End If
        Next j

        Session("MMC") = MMC
        Session("MMCCntr") = MMCCntr
        'jrs 2023/05/04
        Session("MMC2") = MMC2
        Session("MMCCntr2") = MMCCntr2

        'mti 28 November 2001
        On Error GoTo NotCreated

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Populating Earliest And Most Recent, " + vbCrLf

        Status$ = StatusHead$ + "Calculating Earliest" & vbCrLf
        'mti February 2011 Infarstructure - start
        'AddEarliestData UpdTable, tdef
        For Each item In Earliest
            FldNames = " Earliest = " & item.Value.ToString
            sWHERE = " WHERE Country = """ & item.Key & """"
            UpdTable.CommandText = qsql & FldNames & sWHERE
            UpdTable.ExecuteNonQuery()
        Next

        Status$ = StatusHead$ + "Calculating MostRecent" & vbCrLf
        'AddMostRecentData UpdTable, tdef

        For Each item In MostRecent
            FldNames = " MostRecent = " & item.Value.ToString
            sWHERE = " WHERE Country = """ & item.Key & """"
            UpdTable.CommandText = qsql & FldNames & sWHERE
            UpdTable.ExecuteNonQuery()
        Next

        'mti February 2011 Infarstructure - end


        'drop temporary index built in Country Translation ------------
        '    qsql = " DROP INDEX Idx_Countries On [Country Translation];"
        '    db1.Execute qsql   'db = IfsHistSeries.mdb

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Dropping Temporary Translation Index, " + vbCrLf

        'Status$ = StatusHead$ + "Deleting Temporary Country Translation Index<br/>" 

        '        OpenNIndexRecordset db1, valtbl, "Idx_Countries",
        '    "Country Translation", lstCountrylist.Text, True

        '--------------------

        db1.Close()
        dbIfs.Close()


        'Screen.MousePointer = 0

        'mti June 20, 2013
        Session("ImportSuccess") = True

        'mti February 2011 Infarstructure
        'Status$ = Status$ + "Showing Imported Data, " + vbCrLf

        '    'mti February 2011 Infarstructure
        '    'Show the Imported Data in the Table
        '    Status$ = StatusHead$ + "Going To display data" + vbCrLf
        '    Frm_ViewData.DisplayTopic = "IMPORTEDDATA"
        '
        '    'mti Sep 17, 2012 - comply - start
        '    ImportDataTblName$ = txtTable.Text
        '    'mti Sep 17, 2012 - end
        '
        '    Frm_ViewData.Show vbModal

        '    If Accepted Then
        '        'db.Close
        '
        '        'update data dictionary
        '        frm_Datadict.cmdOK.Caption = "Accept"
        '        frm_Datadict.cmdCancel.Caption = "Cancel"
        '        frm_Datadict.Show vbModal
        '
        '        If Accepted Then
        '            'Write the table in IFSHIStSeries.mdb in the Run Files Directory
        '            db.Close
        '            Set db = Nothing
        '            Kill PutFdrvRun$ + "IFsHistSeries.mdb"
        '            FileCopy DataFileDir$ + "IFsHistSeries.mdb", PutFdrvRun$ + "IFsHistSeries.mdb"
        '            Set db = OpenDatabase(DataFileDir$ + txtDatabaseFile.Text, False, False, DataPW$)
        '        Else
        '            db.TableDefs.Delete txtTable.Text
        '        End If
        '
        '    Else  'Delete the New Table
        '            db.TableDefs.Delete txtTable.Text
        '
        '    End If


        Exit Sub


NotCreated:
        'mti February 2011 Infarstructure - start
        Status$ = Status$ + vbCrLf + vbCrLf +
           "Specific Error Message: " + vbCrLf +
            Err.Description + vbCrLf + vbCrLf


        On Error Resume Next
        i = FreeFile()
        FileOpen(i, DataFileDir$ + "ImportXLSProvincesError.Txt", OpenMode.Output)
        PrintLine(i, Status)
        FileClose(i)

        Dim Msg As String
        Msg = "<script>alert('Error creating table. \n" +
           "Please make sure everything is chosen properly.\n\n" +
            "An error log is saved at " + DataFileDir$ + "\ImportXLSProvincesError.Txt');</script>"

        'mti February 2011 Infarstructure - end

        'jrs 2023/05/03
        'UpdTable.CommandText = "DROP TABLE " & txtTable.Text
        'UpdTable.ExecuteNonQuery()

        db1.Close()
        If Not (dbIfs Is Nothing) Then
            dbIfs.Close()
        End If

        Response.Write(Msg)

    End Sub

    Private Sub YearsUniqueFilterNSort()
        'This procedure identifies unique years to import

        'In case of Polity Type Stacked: Filter and Sort Years
        '---------------------------------------------------
        If Session("SeriesType") = 2 Or Session("SeriesType") = 3 Then
            Dim TRString As String

            TRString = CmbYearCol.Text + ":" + CmbYearCol.Text


            Dim lstYears As List(Of Integer) = New List(Of Integer)
            Dim colYear As Integer = 0
            If Session("SeriesType") = 2 Then
                'jrs 2024/03/01 colYear = 1
                colYear = CmbYearCol.SelectedIndex
            End If
            For i = 1 To Session("dataTable").rows.count - 1
                Dim sYear As String
                sYear = Session("dataTable").rows(i).item(colYear).ToString.Trim
                If IsNumeric(sYear) Then
                    Dim iYear As Integer
                    iYear = CInt(sYear)
                    If Not lstYears.Contains(iYear) Then
                        lstYears.Add(iYear)
                    End If
                End If
            Next
            'jrs 2023/02/14
            lstYears.Sort()
            Session("NumYears") = lstYears.Count
            Session("lstYears") = lstYears
        End If
    End Sub

    Private Sub TxtVarName_TextChanged(sender As Object, e As EventArgs) Handles TxtVarName.TextChanged
        txtTable.Text = "Series" + TxtVarName.Text
    End Sub

    Private Function ErrHeader(ImportType$) As String
        'mti February 2011 Infarstructure

        ErrHeader = ImportType$ + vbCrLf + vbCrLf
        ErrHeader = ErrHeader + "File: " + txtFileSelected.Text + vbCrLf + vbCrLf
        ErrHeader = ErrHeader + "Table: " + txtTable.Text + vbCrLf + vbCrLf + vbCrLf
    End Function

    Private Function PrepValue(VarVal)

        'Select Case Trim(CmbScaleOp.Text)
        '    Case "+"
        '        PrepValue = VarVal + Val(TxtScaleFactor.Text)
        '    Case "-"
        '        PrepValue = VarVal - Val(TxtScaleFactor.Text)
        '    Case "*"
        '        PrepValue = VarVal * Val(TxtScaleFactor.Text)
        '    Case "/"
        '        PrepValue = VarVal / (Val(TxtScaleFactor.Text))
        '    Case Else
        PrepValue = VarVal
        'End Select

        If cmbDecimals.SelectedIndex > 0 Then
            'mti April 2011
            PrepValue = Math.Round(PrepValue, cmbDecimals.SelectedIndex - 1)
        End If


    End Function

    Private Function IsValidVar2() As Boolean
        ErrorText = ""

        If TxtVarName.Text = "" Then
            ErrorText = "<script>alert('Please Enter Variable Name');</script>"
            'TxtVarName.SetFocus
            Return False
        End If

        If TxtVarDesc.Text = "" Then
            ErrorText = "<script>alert('Please Enter Variable Definition');</script>"
            'TxtVarDesc.SetFocus
            Return False
        End If

        Return True
    End Function

    Private Sub ShowDataDict(Optional SaveAllowed As Boolean = False, Optional YearColumn$ = "", Optional LastUpd$ = "")
        'mti ImportXLSBatch Nov 2012 -all proc
        If IsValidVar2() = False Then
            'jrs 2023/05/10 Response.Write(ErrorText)
            lblMessages.Text = ErrorText
            lblMessages.Visible = True

            Exit Sub
        End If



        'frm_Datadict.txtVariable.Text = TxtVarName.Text
        'frm_Datadict.txtDefinition.Text = TxtVarDesc.Text
        '
        'If cmbDecimals.ListIndex > 0 Then
        '    frm_Datadict.txtDecimal.Text = cmbDecimals.Text
        'End If
        'frm_Datadict.txtAggregation.Text = cmbaggregation.Text
        '
        'frm_Datadict.UpdateYears = YearColumn$  'AllYear  'years to be picked from datadict

        'mti ImportXLSBatch Nov 2012
        Session("ViewDataDictMDB") = DataFileDir$ + "Datadict.db"
        Session("SaveDataDictMDB") = DataFileDir$ + txtDatabaseFile.Text

        If lstVariablelist.SelectedIndex = 0 Then 'new table
            Session("DataDictRow") = ""
        Else 'updating existing
            Session("DataDictRow") = TxtVarName.Text
        End If

        Session("ORVarName") = TxtVarName.Text
        Session("ORVarDefn") = TxtVarDesc.Text
        Session("ORAggRule") = cmbaggregation.Text
        If cmbDecimals.SelectedIndex = 0 Then
            Session("ORDecPlaces") = "" 'keep as is
        Else
            Session("ORDecPlaces") = cmbDecimals.Text
        End If
        'frm_Datadict.ORCountryList = lstCountrylist.Text
        Session("ORYearsCovered") = YearColumn$
        Session("ORLastIFsUpDate") = LastUpd$  '"NEWDATE"

        Session("ORProvinces") = "NO"

        'Public ORAggRule$, ORYearsCovered$, ORDecPlaces$, ORCountryList$, ORVarName$, ORVarDefn$, ORLastIFsUpDate$


        'If SaveAllowed Then
        '    frm_Datadict.cmdOK.Visible = True
        '    frm_Datadict.cmdCancel.Caption = "Cancel"
        'Else
        '    frm_Datadict.cmdOK.Visible = False
        '    frm_Datadict.cmdCancel.Caption = "Exit"
        'End If

        'Support.FormManager.Show(Me, "frm_Datadict.aspx")


    End Sub

    Protected Sub cmdDatadict_Click(sender As Object, e As EventArgs) Handles cmdDatadict.Click
        ShowDataDict()
        Session("frm_DatadictParent") = "frm_ImportXLSNew"

        Session("ImportXLSVarName") = TxtVarName.Text
        Session("ImportXLSVarDesc") = TxtVarDesc.Text
        Session("ImportXLSAggr") = cmbaggregation.SelectedIndex
        Session("ImportXLSDecimals") = cmbDecimals.SelectedIndex

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Datadict.aspx")
    End Sub



    Protected Sub OptSeriesType4_CheckedChanged1(sender As Object, e As EventArgs) Handles OptSeriesType4.CheckedChanged
        If OptSeriesType4.Checked Then
            PicSeriesType.ImageUrl = "~/Images/SSCAYD.jpg"
            Session("SeriesType") = 4
        End If
    End Sub

    Protected Sub OptSeriesType5_CheckedChanged1(sender As Object, e As EventArgs) Handles OptSeriesType5.CheckedChanged
        If OptSeriesType5.Checked Then
            PicSeriesType.ImageUrl = "~/Images/SSCDYA.jpg"
            Session("SeriesType") = 5
            'jrs 2026/07/23
            ''jrs 2024/02/20
            'cmbDataCol.Enabled = False
            'cmbDataRow.Enabled = False
            txtDataFR.Enabled = False
        End If
    End Sub

    Protected Sub OptSeriesType2_CheckedChanged(sender As Object, e As EventArgs) Handles OptSeriesType2.CheckedChanged
        If OptSeriesType2.Checked Then
            PicSeriesType.ImageUrl = "~/Images/MSAYWC.jpg"
            Session("SeriesType") = 2
            'jrs 2024/02/20
            cmbDataCol.Enabled = True
            cmbDataRow.Enabled = True
            txtDataFR.Enabled = True

            'Read Years in case series type changed
            If CmbYearCol.SelectedIndex > -1 And CmbYearRow.SelectedIndex > -1 Then
                YearsUniqueFilterNSort()
            End If  'YearRow, Yearcol>0
        End If
    End Sub


    Protected Sub CmbYearRow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbYearRow.SelectedIndexChanged
        If CmbYearCol.SelectedIndex >= 0 And CmbYearRow.SelectedIndex >= 0 Then
            Dim dataTable As DataTable
            dataTable = Session("dataTable")
            If IsDBNull(dataTable.Rows(CmbYearRow.SelectedIndex).ItemArray(CmbYearCol.SelectedIndex)) Then
                TxtYearFR.Text = ""
            Else
                TxtYearFR.Text = dataTable.Rows(CmbYearRow.SelectedIndex).ItemArray(CmbYearCol.SelectedIndex)
            End If
            Session("CmbYearRow.SelectedIndex") = CmbYearRow.SelectedIndex
        End If
    End Sub

    Private Sub cmdRefreshSheets_Click(sender As Object, e As EventArgs) Handles cmdRefreshSheets.Click
        Session("CmbCountryCol.SelectedIndex") = Nothing
        Session("CmbCountryRow.SelectedIndex") = Nothing
        Session("CmbYearCol.SelectedIndex") = Nothing
        Session("CmbYearRow.SelectedIndex") = Nothing
        'jrs 2024/01/09
        Session("CmbDataCol.SelectedIndex") = Nothing
        Session("CmbDataRow.SelectedIndex") = Nothing

        'jrs 2023/05/10
        CmdImport.Text = "Import"
        Session("Accepted") = False
        Session("ORDecPlaces") = ""
        Session("ORYearsCovered") = ""
        Session("ORAggRule") = ""
        Session("ORVarDefn") = ""
        Session("ORVarName") = ""
        Session("DataDictRow") = ""

        RefreshSheets()

    End Sub

    'jrs 2024/01/08
    Private Sub OptSeriesType1_CheckedChanged(sender As Object, e As EventArgs) Handles OptSeriesType1.CheckedChanged
        If OptSeriesType1.Checked Then
            PicSeriesType.ImageUrl = "~/Images/MSDCDPA.jpg"
            Session("SeriesType") = 1
            'Read Years in case series type changed
            If CmbYearCol.SelectedIndex > -1 And CmbYearRow.SelectedIndex > -1 Then
                YearsUniqueFilterNSort()
            End If  'YearRow, Yearcol>0
        End If
    End Sub

    Private Sub cmbDataCol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDataCol.SelectedIndexChanged
        If cmbDataCol.SelectedIndex >= 0 And cmbDataRow.SelectedIndex >= 0 Then
            Dim dataTable As DataTable
            dataTable = Session("dataTable")
            If IsDBNull(dataTable.Rows(cmbDataRow.SelectedIndex).ItemArray(cmbDataCol.SelectedIndex)) Then
                txtDataFR.Text = ""
            Else
                txtDataFR.Text = dataTable.Rows(cmbDataRow.SelectedIndex).ItemArray(cmbDataCol.SelectedIndex)
            End If
            Session("CmbDataCol.SelectedIndex") = cmbDataCol.SelectedIndex
        End If
    End Sub

    Private Sub cmbDataRow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDataRow.SelectedIndexChanged
        If cmbDataCol.SelectedIndex >= 0 And cmbDataRow.SelectedIndex >= 0 Then
            Dim dataTable As DataTable
            dataTable = Session("dataTable")
            If IsDBNull(dataTable.Rows(cmbDataRow.SelectedIndex).ItemArray(cmbDataCol.SelectedIndex)) Then
                txtDataFR.Text = ""
            Else
                txtDataFR.Text = dataTable.Rows(cmbDataRow.SelectedIndex).ItemArray(cmbDataCol.SelectedIndex)
            End If
            Session("CmbDataRow.SelectedIndex") = cmbDataRow.SelectedIndex
        End If
    End Sub

    Protected Sub lstCountry4Province_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCountry4Province.SelectedIndexChanged
        'jrs 2026/07/22
        Session("Country4Provinces") = lstCountry4Province.SelectedItem.Text
        txtDatabaseFile.Text = "IFsHistSeries" & Session("Country4Provinces") & ".db"
        Session("txtDatabaseFile") = txtDatabaseFile.Text
    End Sub

    Private Sub LoadCountries()
        Dim dbIfs As SQLiteConnection
        dbIfs = New SQLiteConnection(CType("DataSource=" + Session("Prov_Country_MDB") & ";", String))
        dbIfs.Open()
        Dim CTcmd As SQLiteCommand
        CTcmd = dbIfs.CreateCommand
        CTcmd.CommandText = "SELECT Distinct Country FROM [Provinces] ORDER BY Country"
        Dim rsCT As SQLiteDataReader
        rsCT = CTcmd.ExecuteReader

        lstCountry4Province.Items.Clear()
        Do While rsCT.Read
            lstCountry4Province.Items.Add(rsCT(0))
            If rsCT(0) = Session("Country4Provinces") Then
                lstCountry4Province.SelectedIndex = lstCountry4Province.Items.Count - 1
            End If
        Loop
        rsCT.Close()
        dbIfs.Close()
    End Sub
End Class

