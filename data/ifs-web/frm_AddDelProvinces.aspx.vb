Option Strict Off
Option Explicit On
Imports System.Data.SQLite
Imports MarkItUp.WebControls
Imports System.IO
Imports EO.Web.Internal
Imports Microsoft.VisualStudio.Shell.Interop

Partial Class frm_AddDelProvinces
    Inherits Page

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.IfsVB60 = New System.Diagnostics.Process

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        Dim sFileExists As String
        If ModelRunningWith <> "Base" Then
            If File_Exists(PutDataFileDir$ & "ProvinceSwitch\" & ModelRunningWith, True) Then
                sFileExists = "True"
            Else
                sFileExists = "False"
            End If
        Else
            If File_Exists(PutDataFileDir$ & "ProvinceSwitch\Base", True) = False Then
                sFileExists = "False"
            Else
                sFileExists = "True"
            End If
        End If

        cmdSave.Attributes.Add("onClick", "return checkFolderToReSave('" & sFileExists & "','" & ModelRunningWith & "');")
        cmdAdd.Attributes.Add("onClick", "return checkToAdd('" & ModelRunningWith & "');")
        cmdRemove.Attributes.Add("onClick", "return CheckDelete();")
        'jrs 2022/11/14
        cmdSwitch.Attributes.Add("onClick", "return checkFolderToReSaveB4Switch('" & sFileExists & "','" & ModelRunningWith & "');")
        cmdDelete.Attributes.Add("onClick", "return checkB4Delete();")

        If Not Page.IsPostBack Then
            Timer1.Visible = False
            Session("NeedFileWrite") = False
            Session("bLoadRebuildBase") = False
            Dim InFile$ = DataFileDir$ + "Ifs.db"
            Dim OutFile$ = Session("PutFdrvRun") + "Ifs.db"
            FileCopyCheck(InFile$, OutFile$)
            InFile$ = DataFileDir$ + "IfsHistSeries.db"
            OutFile$ = Session("PutFdrvRun") + "IfsHistSeries.db"
            FileCopyCheck(InFile$, OutFile$)
            InFile$ = DataFileDir$ + "IFsWVSCohort.db"
            OutFile$ = Session("PutFdrvRun") + "IFsWVSCohort.db"
            FileCopyCheck(InFile$, OutFile$)
            InFile$ = DataFileDir$ + "IFsCoVaTra.db"
            OutFile$ = Session("PutFdrvRun") + "IFsCoVaTra.db"
            FileCopyCheck(InFile$, OutFile$)
            InFile$ = DataFileDir$ + "IFsCoVaTraSeries.db"
            OutFile$ = Session("PutFdrvRun") + "IFsCoVaTraSeries.db"
            FileCopyCheck(InFile$, OutFile$)


            'jrs 2007/01/22 load countries list box
            Dim rs As DataTable
            SetDataTablefromSQLite(Session("FdrvRun") + "Provinces.db", "SELECT Country FROM AllProvinces ORDER BY Country", "", rs)
            Dim idx As Integer = 0
            Do While idx < rs.Rows.Count
                lstCountries.Items.Add(rs.Rows(idx).Item("Country"))
                idx = idx + 1
            Loop
            rs = Nothing
            If lstCountries.Items.Count > 0 Then
                lstCountries.Items(0).Selected = True
                lstCountries_SelectedIndexChanged(Me, New EventArgs)
            End If

            LoadCountryProvinces()

            'Set the flag for the copy msg to false
            Session("NeedFileWrite") = False
            If File_Exists(PutDataFileDir$ & "ProvinceSwitch", True) = False Then
                MkDir(PutDataFileDir$ & "ProvinceSwitch")
            End If

            'jrs 2016/06/15
            'FullArrayColors

            'Refreshes Switch list
            cmdRefresh_Click(Me, New EventArgs)
        End If

        'jrs 2022/12/08
        If bDeletingProcess.Value <> "True" Then
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadlstSwitchHdn",
                Session("lstSwitchHdn"), True)
        Else
            bDeletingProcess.Value = ""
        End If

    End Sub

    Protected Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub

    Private Sub LoadCountryProvinces()

        lstCtryProv.Items.Clear()

        If ModelRunningWith <> "Base" Then
            lstCtryProv.Items.Add(ModelRunningWith)
        End If

    End Sub


    Protected Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        lstSwitch.Items.Clear()
        FillList(lstSwitch, PutDataFileDir$ & "ProvinceSwitch")
        Dim lstSwitchHdn As String = "var lstSwitchHdn = [];"
        Dim i As Integer
        For i = 0 To lstSwitch.Items.Count - 1
            If lstSwitch.Items(i).Value = "Base" Then
                lstSwitch.Items(i).Value = "No State/Province"
                lstSwitch.Items(i).Text = "No State/Province"
                'Exit For
            End If
            lstSwitchHdn = lstSwitchHdn & vbCr & "lstSwitchHdn[" & i.ToString & "] = """ & lstSwitch.Items(i).Value & """;"
        Next i

        Session("lstSwitchHdn") = lstSwitchHdn
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadlstSwitchHdn",
            Session("lstSwitchHdn"), True)

    End Sub

    Public Sub FillList(ByRef cmbList As ListBox, strDrive As String)

        Const ArrTop As Integer = 300 ' limit to 300 files, and that is way too many
        ' list box row source property is limited to
        ' 2048 characters so at 300 files that is
        ' about 7 characters per name
        Dim MyArray(ArrTop) As String

        Dim file_name As String
        Dim strRowSource As String ' Build row source string here to get all files
        Dim i As Integer ' MyArray index for fill
        Dim j As Integer ' MyArray index for move to list box

        On Error Resume Next
        'list the names of all files in the specified directory

        file_name = Dir$(strDrive & "\*.*", vbDirectory)
        i = 1
        Do While (Len(file_name) > 0) And (i < 101)

            ' See if we should skip this file.
            If Not (file_name = ".") Or (file_name = "..") Then
                MyArray(i) = file_name
                i = i + 1
            End If

            ' Get the next file.
            file_name = Dir$()
        Loop

        If i > 1 Then


            QuickSort(MyArray, 1, i - 1)
            'use sorted array to refill listbox
            cmbList.Items.Clear()  'clears the list
            For j = 1 To i
                If MyArray(j) <> ModelRunningWith Then
                    cmbList.Items.Add(MyArray(j))
                End If
            Next j

        Else
            cmbList.Items.Add(MyArray(1))
        End If

    End Sub




    Protected Sub lstCountries_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCountries.SelectedIndexChanged
        'load regions
        lstRegions.Items.Clear()

        Dim tblD As DataTable
        SetDataTablefromSQLite(Session("FdrvRun") + "Provinces.db", "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'", "", tblD)
        Dim idx As Integer = 0
        Do While idx < tblD.Rows.Count
            'jrs 2012/02/16 removing spaces from country name (South Africa)
            If InStr(UCase(tblD.Rows(idx).Item(0)), "REGIONALIZATION" & Replace(UCase(lstCountries.Text), " ", "")) > 0 Then
                lstRegions.Items.Add(Replace(UCase(tblD.Rows(idx).Item(0)), "REGIONALIZATION", ""))
            End If
            idx = idx + 1
        Loop


        If lstRegions.Items.Count > 0 Then
            lstRegions.Items(0).Selected = True
        End If

    End Sub


    Private Sub EnableDisable(Val As Boolean)
        'jrs 2014/11/03
        '    'jrs 2008/09/10
        '    If Val = True And lstCtryProv.ListCount = 0 Then
        '        cmdAdd.Enabled = True
        '    Else
        '        cmdAdd.Enabled = False
        '    End If
        cmdAdd.Enabled = Val

        cmdDelete.Enabled = Val
        cmdExit.Enabled = Val
        cmdRefresh.Enabled = Val
        cmdSwitch.Enabled = Val
        lstCountries.Enabled = Val
        'cswmWebMenu.Groups("mnuScc").ShowMenuBarItem = False
        cmdSave.Enabled = Val
        'mnuScc.Enabled = Val
        'cswmWebMenu.Groups("mnuSwp").ShowMenuBarItem = False
        cmdShowSwitch.Enabled = Val
        'mnuSwp.Enabled = Val
        cmdRemove.Enabled = Val

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sw As New SwitchingHandler(Session("PutFdrvRun"))
        EnableDisable(False)
        sw.SaveCurrentSetup()
        EnableDisable(True)
    End Sub

    Private Sub cmdShowSwitch_Click(sender As Object, e As EventArgs) Handles cmdShowSwitch.Click
        If lstSwitch.Visible Then
            lstSwitch.Visible = False
            lblAvailableRuns.Visible = False
            cmdRefresh.Visible = False
            cmdRemove.Visible = False
            cmdSwitch.Visible = False
        Else
            lstSwitch.Visible = True
            lblAvailableRuns.Visible = True
            cmdRefresh.Visible = True
            cmdRemove.Visible = True
            cmdSwitch.Visible = True
        End If
        cmdRefresh_Click(Me, New EventArgs)
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If lstSwitch.Visible = False Then
            If lstRegions.Text <> "" Then
                'jrs 2015/08/17
                If ModelRunningWith = "Base" Then
                    If CheckAlreadyAdded(lstRegions.Text) = False Then
                        Exit Sub
                    End If
                End If
            End If
        End If

        Dim sw As New SwitchingHandler(Session("PutFdrvRun"))
        sw.SaveCurrentSetup()

        EnableDisable(False)
        SetDefaultRegion()

        AddProvinces(lstCountries.Text)
        ''jrs 2014/11/04 need to check if adding provinces with other provinces already loaded
        '        Dim sRegions As String
        '        sRegions = lstRegions.Text
        '        If lstCtryProv.ListCount > 0 Then
        '            sRegions = sRegions & lstCtryProv.List(0)
        '        End If
        '        UpdateCurrentlyRunningModel sRegions
        'EnableDisable True
        'RebuildBase
        '        lblProgress.Caption = "All the necessary files have been saved successfully."
        '        cmdExit_Click()
    End Sub

    Private Function CheckAlreadyAdded(rname As String) As Boolean
        Dim i As Integer
        For i = 0 To lstSwitch.Items.Count - 1
            If UCase(lstSwitch.Items(i).Value) = UCase(rname) Then
                CheckAlreadyAdded = False

                Dim txt = rname & " already exists in the switch list, please remove it " &
                    "in order to build it again or use the switch procedure to load this setup."

                LoadMessage(txt, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Function
            End If
        Next i

        CheckAlreadyAdded = True
    End Function

    Private Sub cmdRemove_Click(sender As Object, e As EventArgs) Handles cmdRemove.Click
        EnableDisable(False)
        Dim sw As New SwitchingHandler(Session("PutFdrvRun"))
        sw.CompletelyErase(lstSwitch.Text)
        EnableDisable(True)

        cmdRefresh_Click(Me, New EventArgs)
        'lblProgress.Caption = "Removed Successfully."

    End Sub

    Private Sub SetDefaultRegion()
        Dim sql As String
        '1. Delete country regionalization from Target table
        sql = "DELETE FROM Regionalization"
        If lstCountries.Text <> "" Then
            sql = sql & " WHERE Country = """ & lstCountries.Text & """"
        End If

        'updateSQLiteWith1Command(Session("FdrvRun") + "Provinces.db", sql)
        Dim vSQLiteCon As SQLiteConnection
        Dim vSQLiteCommand As SQLiteCommand

        vSQLiteCon = New SQLiteConnection(CType("DataSource=" & Session("FdrvRun") + "Provinces.db;", String))
        vSQLiteCon.Open()

        'objAdapter.Fill(DT)
        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = sql
        vSQLiteCommand.ExecuteNonQuery()

        '2. Insert new regionalization to Target Table
        sql = "INSERT INTO Regionalization SELECT * FROM Regionalization" & lstRegions.Text
        If lstCountries.Text <> "" Then
            sql = sql & " WHERE Country = """ & lstCountries.Text & """"
        End If
        vSQLiteCommand.CommandText = sql
        vSQLiteCommand.ExecuteNonQuery()
        vSQLiteCon.Close()
    End Sub

    Protected WithEvents IfsVB60 As System.Diagnostics.Process

    Private Sub AddProvinces(sCountry As String)
        '-------------------------------------------------------------------
        'Set the cursor
        'Screen.MousePointer = vbHourglass
        'Me.MousePointer = vbHourglass
        'DoEvents

        Dim dbProv As SQLiteConnection
        dbProv = New SQLiteConnection(CType("DataSource=" & Session("FdrvRun") + "Provinces.db;", String))
        dbProv.Open()

        Session("Message") = ""

        Dim bContinue As Boolean
        'jrs 2007/01/30 check for required data in each province
        bContinue = CheckProvinceData(dbProv, sCountry)
        If Not bContinue Then
            Session("Message") = Session("Message") & "<br/> Process Failed!"
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
        'jrs 2007/01/10 Set rs = daoDB36.OpenRecordset("SELECT * FROM Provinces WHERE Country = '" & sCountry & "'")
        Dim dbProvCommand As SQLiteCommand
        Dim rs As DataTable = New DataTable
        Dim rd As SQLiteDataReader

        dbProvCommand = dbProv.CreateCommand
        dbProvCommand.CommandText = "SELECT * FROM Provinces P, AllProvinces A WHERE P.Country = A.Country AND P.Country = """ & sCountry & """ ORDER BY Province" 'jrs 2012/05/15 added order by Province
        rd = dbProvCommand.ExecuteReader
        'jrs 2022/10/06
        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(rs)
        rs.Load(rd)

        Dim dbProvCommand2 As SQLiteCommand
        Dim rsReg As SQLiteDataReader

        dbProvCommand2 = dbProv.CreateCommand
        dbProvCommand2.CommandText = "SELECT Distinct Region, Country FROM Regionalization WHERE Country = """ & sCountry & """"
        rsReg = dbProvCommand2.ExecuteReader
        Dim tblReg As DataTable = New DataTable

        'jrs 2022/10/06
        dsAll.Tables.Add(tblReg)

        tblReg.Load(rsReg)

        '   Check to see if the country name exists.
        '   --------------------------------------
        If rs.Rows.Count = 0 Then
            Session("Message") = Session("Message") & "<br/>This country doesn't have Sub-Regions associated with it."
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If tblReg.Rows.Count > MAXCOM(1) - MaxVal(1) Then
            Session("Message") = Session("Message") & "<br/>There are too many provinces, you will need to increase the number of MaxReg in the Load table of IFsInit.mdb and Rebuild the Common." &
                "<br/>MaxReg is currently " & MAXCOM(1) & " and needs to be at least " & CStr(tblReg.Rows.Count + MaxVal(1)) & " in order to proceed."
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        'Check if the Province Name already exists as a Region
        Dim RegIdx, tblRegIdx As Integer
        tblRegIdx = 0
        While tblRegIdx < tblReg.Rows.Count
            RegIdx = IFsRegs.Item(tblReg.Rows(tblRegIdx)("Region")).Index
            If RegIdx > 0 Then
                Session("Message") = Session("Message") & "<br/>The Province " & tblReg.Rows(tblRegIdx)("Region") & " already exists as a Region."
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            tblRegIdx = tblRegIdx + 1
        End While

        cmdAdd.Text = "In Process ..."
        cmdAdd.Enabled = False
        cmdDelete.Enabled = False
        lstCountries.Enabled = False
        cmdExit.Enabled = False

        'jrs 2007/01/16
        'Check special Variables
        '-----------------------
        If Not CheckSpecialVariables(rs) Then
            Exit Sub
        End If

        'jrs 2007/02/15
        Dim sProvs As String
        sProvs = BuildProvincesStr(rs)
        'Apply Factor to Tables
        ApplyFactorTable(sCountry, sProvs)

        dbProv.Close()

        'jrs 2022/11/11
        'Call a ifs.exe with parameters 9 "South Africa"
        Dim sFileName, sWorkDir As String
        'jrs 2022/08/03
        Dim sExeDir As String
        sExeDir = "net8"

        'jrs 2026/03/03
        'If DevelopmentMode = True And Dir(StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\") > "" Then
        '    sFileName = DemandFile
        '    sWorkDir = StartDirec & "Code.Ifs-Translation\"
        'Else
        '    sWorkDir = StartDirec & sExeDir & "\"
        '    sFileName = DemandFile
        'End If
        If DevelopmentMode = True And Dir(StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\") > "" Then
            sFileName = StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\" & DemandFile
            sWorkDir = StartDirec & "Code.Ifs-Translation\"
        Else
            sWorkDir = StartDirec & sExeDir & "\"
            sFileName = sWorkDir & DemandFile
        End If


        IfsVB60.StartInfo.FileName = sFileName
        IfsVB60.StartInfo.WorkingDirectory = sWorkDir
        IfsVB60.StartInfo.Arguments = "9 """ & sCountry & """"
        'IfsVB60.StartInfo.FileName = StartDirec & "Notepad.exe"

        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Kill(Session("PutFdrvRun") & "progress.txt")
        End If
        IfsVB60.StartInfo.UseShellExecute = False
        IfsVB60.Start()
        Session("IfsVB60") = IfsVB60
        Timer1.Visible = True
        lblJSReb.Visible = True
        lblJSReb.Text = "Adding provinces has begun - Please Wait"

    End Sub

    Private Function CheckProvinceData(dbP As SQLiteConnection, sCountry As String) As Boolean
        'jrs 2007/04/27
        Dim bContinue As Boolean
        'Check Population
        bContinue = CheckProvinceDatainTable(dbP, "SeriesPopulation", sCountry)
        If Not bContinue Then
            CheckProvinceData = False
            Exit Function
        End If
        'Check PopulationUNFilled
        '    bContinue = CheckProvinceDatainTable(dbP, "SeriesPopulationUNFilled", sCountry)
        '    If Not bContinue Then
        '        CheckProvinceData = False
        '        Exit Function
        '    End If
        'Check GDP      
        'jrs 2026/02/27 adding ConstantDollarYear
        bContinue = CheckProvinceDatainTable(dbP, "SeriesGDP" & ConstantDollarsYear.ToString, sCountry)
        If Not bContinue Then
            CheckProvinceData = False
            Exit Function
        End If
        'Check GDPPC
        bContinue = CheckProvinceDatainTable(dbP, "SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP", sCountry)
        If Not bContinue Then
            CheckProvinceData = False
            Exit Function
        End If
        'jrs 2013/12/23
        'Check Land
        bContinue = CheckProvinceDatainTable(dbP, "SeriesLandArea", sCountry)
        If Not bContinue Then
            CheckProvinceData = False
            Exit Function
        End If

        'jrs 2008/01/17
        CheckProvinceRegionalization(dbP, sCountry)

        CheckProvinceData = True
    End Function

    Private Function CheckProvinceDatainTable(dbP As SQLiteConnection, sTbl As String, sCountry As String) As Boolean
        'Returns True if successful
        '1. dbP, Province Database
        '2. sTbl, Name of the Table to check
        '3. Country to check


        'Read provinces to check
        '=======================
        Dim vSQLiteCommand As SQLiteCommand
        Dim dbPCommand As SQLiteCommand
        Dim rsP As SQLiteDataReader

        vSQLiteCommand = dbP.CreateCommand()
        dbPCommand = dbP.CreateCommand()
        vSQLiteCommand.CommandText = "SELECT * FROM Provinces WHERE Country = """ & sCountry & """ ORDER BY Country, Province"
        rsP = vSQLiteCommand.ExecuteReader()

        'Check every Province
        '====================
        Dim bData As Boolean
        Dim dbD As SQLiteConnection
        Dim dbDCommand As SQLiteCommand
        Dim TblRdr As SQLiteDataReader
        Dim Tbl As DataTable
        'Open Data Database
        dbD = New SQLiteConnection("DataSource=" & DataFileDir$ + "IfsHistSeries" & sCountry & ".db;")
        dbD.Open()

        'Check fields in Target DB too
        '=============================
        Dim dbT As SQLiteConnection
        Dim dbTCommand As SQLiteCommand
        Dim TblTRdr As SQLiteDataReader
        Dim tblT As DataTable
        Dim bDataT As Boolean
        dbT = New SQLiteConnection("DataSource=" & DataFileDir$ + "IfsHistSeries.db;")
        dbT.Open()

        'jrs 2022/10/05
        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False

        'Open Table to check
        '===================
        On Error GoTo ErrHdlr
        dbDCommand = dbD.CreateCommand()
        dbDCommand.CommandText = "SELECT * FROM " & sTbl
        TblRdr = dbDCommand.ExecuteReader()
        Tbl = New DataTable
        dsAll.Tables.Add(Tbl)
        Tbl.Load(TblRdr)

        dbTCommand = dbT.CreateCommand()
        dbTCommand.CommandText = "SELECT * FROM " & sTbl
        TblTRdr = dbTCommand.ExecuteReader()
        tblT = New DataTable
        dsAll.Tables.Add(tblT)
        tblT.Load(TblTRdr)

        Dim tblTRows = tblT.Select("Country = '" & sCountry.Replace("'", "''") & "'")

        While (rsP.Read())

            bData = False
            Dim TblRows = Tbl.Select("Country = '" & rsP("Province").replace("'", "''") & "'")
            'Check that there's data for this province
            bData = (TblRows.Count > 0)
            If bData Then
                bData = False
                bDataT = False

                Dim i As Integer
                For i = Tbl.Columns.Count - 1 To 1 Step -1
                    If IsNumeric(TblRows(0).Item(i)) And IsNumeric(Tbl.Columns(i).ColumnName) Then
                        'Check that the field exists in the target DB
                        On Error GoTo TargetFldNotFound
                        bDataT = IsNumeric(tblTRows(0).Item(Tbl.Columns(i).ColumnName))
                        On Error GoTo ErrHdlr
                        If bDataT Then
                            bData = True
                            Exit For
                        End If
                    End If
                Next i
            End If
            If Not bData Then
                'Send a message
                Session("Message") = Session("Message") & "<br/>Sub-Region " & rsP("Province") & " will not be used, because it doesn't have data in table " & sTbl            'Delete the province from runfiles
                dbPCommand.CommandText = "DELETE * FROM Provinces WHERE Province = """ & rsP("Province") & """"
                dbPCommand.ExecuteNonQuery()
                dbPCommand.CommandText = "DELETE * FROM Regionalization WHERE Province = """ & rsP("Province") & """"
                dbPCommand.ExecuteNonQuery()
            End If
        End While
        Tbl = Nothing
        dbD.Close()
        rsP.Close()

        tblT = Nothing
        dbT.Close()

        CheckProvinceDatainTable = True
        Exit Function
ErrHdlr:
        Session("Message") = Session("Message") & "<br/>Error Trapped when checking Data for " & sTbl & "<br/>" & Err.Description
        CheckProvinceDatainTable = False
        Exit Function

TargetFldNotFound:
        bDataT = False
        Resume Next
    End Function

    'Deletes Provinces that are not in the Provinces table
    '(for example in the case of incomplete change of name, referential integrity problem)
    Private Sub CheckProvinceRegionalization(db As SQLiteConnection, sCountry As String)
        Dim sql As String
        sql = "SELECT R.* FROM Regionalization" &
             " as R WHERE NOT EXISTS (SELECT * FROM Provinces as P WHERE P.Province = R.Province AND P.Country = R.Country)"
        sql = sql & " AND Country = """ & sCountry & """"
        Dim rs As SQLiteDataReader
        Dim vSQLiteCommand As SQLiteCommand
        Dim vSQLiteCommand2 As SQLiteCommand
        vSQLiteCommand = db.CreateCommand()
        vSQLiteCommand2 = db.CreateCommand()
        Dim sMsg As String
        vSQLiteCommand.CommandText = sql
        rs = vSQLiteCommand.ExecuteReader

        While rs.Read
            sMsg = "Sub-Region " & rs("Province") &
                " doesn't exist in the Province table and it will be removed from the Regionalization table."
            Session("Message") = Session("Message") & sMsg
            vSQLiteCommand2.CommandText = "DELETE * FROM Regionalization WHERE Province = """ & rs("Province") &
                """ AND Country = """ & rs("Country") & """"
            vSQLiteCommand2.ExecuteNonQuery()
        End While

        rs.Close()
    End Sub

    Private Function CheckSpecialVariables(rsP As DataTable) As Boolean
        'Checks the structure of the tables and normalizes the data.

        'jrs 2007/01/17
        'if selected, copy original file from DATA
        '-----------------------------------------
        Dim db0 As String, db1 As String, db2 As String
        db0 = DataFileDir$ + "IFsHistSeries" + rsP.Rows(0).Item("Country") + ".db"
        db1 = Session("FdrvRun") + "IFsHistSeries" + rsP.Rows(0).Item("Country") + ".db"
        db2 = Session("FdrvRun") + "IFsHistSeries.db"

        'jrs 2007/01/22 removed the check box, since I don't think it's necessary to ever re use the province data in RUNFILES
        FileCopyCheck(db0, db1)

        CheckSpecialVariables = False

        Dim db As SQLiteConnection, dbT As SQLiteConnection, dbD As SQLiteConnection
        db = New SQLiteConnection("DataSource=" & db1 & ";")
        db.Open()
        dbT = New SQLiteConnection("DataSource=" & db2 & ";")
        dbT.Open()
        dbD = New SQLiteConnection("DataSource=" & db0 & ";")
        dbD.Open()

        'jrs 2012/09/14 replacing GDP2000 with GDP2011
        'GDP2011
        'jrs 2026/02/27 changing to constant dollars year
        If Not CheckTable("SeriesGDP" & ConstantDollarsYear.ToString, db, dbT, dbD, rsP) Then
            Exit Function
        End If
        'Population
        If Not CheckTable("SeriesPopulation", db, dbT, dbD, rsP) Then
            Exit Function
        End If
        'jrs 2011/11/28 no longer used
        '    'Population UN Filled
        '    If Not CheckTable("SeriesPopulationUNFilled", db, dbT, dbD, rsP) Then
        '        Exit Function
        '    End If

        'jrs 2011/11/28
        'GDP PC PPP
        'If Not CheckTable("SeriesGDP2000PCPPP", db, dbT, dbD, rsP) Then
        'jrs 2026/02/27 changing to constant dollars year
        If Not CheckTable("SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP", db, dbT, dbD, rsP) Then
            Exit Function
        End If
        'jrs 2013/12/23
        'Land
        If Not CheckTable("SeriesLandArea", db, dbT, dbD, rsP) Then
            Exit Function
        End If

        db.Close()
        dbT.Close()
        dbD.Close()
        CheckSpecialVariables = True
    End Function

    Private Function CheckTable(sTbl As String, db As SQLiteConnection, dbT As SQLiteConnection, dbD As SQLiteConnection, rsP As DataTable) As Boolean
        'This function Checks the structure of the table and normalizes the data
        'sTbl is the table to Check
        'db is the database with the provinces data
        'dbT is the target database with the country data
        'dbD is the original database with provinces data (from DATA folder)
        'rsP is the list of provinces
        CheckTable = False
        Dim dbCommand As SQLiteCommand
        Dim rd As SQLiteDataReader
        Dim rs As DataTable = New DataTable

        dbCommand = db.CreateCommand
        dbCommand.CommandText = "SELECT * FROM " & sTbl
        rd = dbCommand.ExecuteReader

        'jrs 2022/10/06
        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(rs)

        rs.Load(rd)

        Dim dbDCommand As SQLiteCommand
        Dim rsD As DataTable = New DataTable
        Dim rdD As SQLiteDataReader

        dbDCommand = dbD.CreateCommand
        dbDCommand.CommandText = "SELECT * FROM " & sTbl
        rdD = dbDCommand.ExecuteReader

        'jrs 2022/10/06
        dsAll.Tables.Add(rsD)

        rsD.Load(rdD)

        Dim rsPidx = 0
        Dim Total As Single, Factor As Single
        Total = 0
        Dim bAll As Boolean
        bAll = rsP.Rows(0).Item("AllProvinces")
        Dim sBaseYear As String
        sBaseYear = CStr(FirstYearForecast)
        Dim bMostRec As Boolean
        bMostRec = (sBaseYear = rs.Columns(rs.Columns.Count - 3).ColumnName)
        Dim iYr As Integer
        'jrs 2007/01/29 GDPPC
        Dim rsPop As DataTable = New DataTable
        'jrs 2026/02/27 changing to constant dollars year
        If sTbl = "SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP" Then

            Dim dbCommand2 As SQLiteCommand
            Dim rdPop As SQLiteDataReader

            dbCommand2 = db.CreateCommand
            dbCommand2.CommandText = "SELECT * FROM SeriesPopulation"
            rdPop = dbCommand2.ExecuteReader
            'jrs 2022/10/06
            dsAll.Tables.Add(rsPop)
            rsPop.Load(rdPop)
        End If

        Dim rsT As DataTable = New DataTable
        Dim dbTCommand2 As SQLiteCommand
        Dim rdT As SQLiteDataReader

        dbTCommand2 = dbT.CreateCommand
        dbTCommand2.CommandText = "SELECT * FROM " & sTbl
        rdT = dbTCommand2.ExecuteReader
        'jrs 2022/10/06
        dsAll.Tables.Add(rsT)
        rsT.Load(rdT)

        Dim rsTRows = rsT.Select("Country = '" & rsP.Rows(0).Item("Country").ToString.Replace("'", "''") & "'")

        'jrs 2009/04/13
        'check order of fields in DATA folder
        '====================================
        CheckFieldOrderinTable(rsD, dbD, sTbl)

        'Check the structure of the Table
        '(should be same as in IfsHistSeries.mdb)
        '====================================
        'jrs 2007/02/08
        CheckTableStructure(rs, rsT, db, dbT, sTbl, "")

        'Load BaseYear
        '=============
        rsPidx = 0
        Do While rsPidx < rsP.Rows.Count
            Dim rsRows = rs.Select("Country = '" & rsP.Rows(rsPidx).Item("Province").ToString.Replace("'", "''") & "'")
            'Check that there is data in BaseYear
            If Not IsNumeric(rsRows(0).Item(sBaseYear)) Then
                'Look for closest year to fill in BaseYear
                iYr = LookForClosestYear(CInt(sBaseYear), rs, rsRows, CInt(rs.Columns(rs.Columns.Count - 3).ColumnName), CInt(rs.Columns(2).ColumnName))
                Dim dbCommand3 As SQLiteCommand
                dbCommand3 = db.CreateCommand
                dbCommand3.CommandText = "UPDATE " & sTbl & " SET [" & sBaseYear & "] = " & rsRows(0).Item(iYr.ToString) & " WHERE Country = """ & rsP.Rows(rsPidx).Item("Province") & """"
                dbCommand3.ExecuteNonQuery()
            End If
            'jrs 2007/01/29 GDPPC
            'jrs 2026/02/27 changing to constant dollars year
            If sTbl = "SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP" Then
                Dim rsPopRows = rsPop.Select("Country = '" & rsP.Rows(rsPidx).Item("Province").ToString.Replace("'", "''") & "'")
                If IsNumeric(rsRows(0).Item(sBaseYear)) And IsNumeric(rsPopRows(0).Item(sBaseYear)) Then
                    Total = Total + rsRows(0).Item(sBaseYear) * rsPopRows(0).Item(sBaseYear)
                End If
            Else
                Total = Total + rsRows(0).Item(sBaseYear)
            End If
            'jrs 2007/01/17
            If rsRows(0).Item(sBaseYear) < 0.1 And Not bAll And Not sTbl = "SeriesLandArea" Then 'jrs 2014/11/06 added and not LandArea
                Session("Message") = Session("Message") & "<br/>If not already doing so, the user should probably put the province " & rsP(rsPidx).Item("Province") &
                    " into a region because province is too small to produce good model results."
            End If
            rsPidx = rsPidx + 1
        Loop
        rsPidx = 0
        'jrs 2007/01/29 GDPPC
        Dim rsTPOP As DataTable = New DataTable
        'jrs 2026/02/27 changing to constant dollars year
        If sTbl = "SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP" Then
            Dim dbTCommand3 As SQLiteCommand
            Dim rdT2 As SQLiteDataReader

            dbTCommand3 = dbT.CreateCommand
            dbTCommand3.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsP.Rows(rsPidx).Item("Country") & """"
            rdT2 = dbTCommand3.ExecuteReader
            'jrs 2022/10/06
            dsAll.Tables.Add(rsTPOP)
            rsTPOP.Load(rdT2)
        End If

        'Normalize BaseYear (if using All Provinces)
        '===========================================
        'jrs 2012/05/21
        'This flag is set to true, when Normalization most be avoided to force the use of province data
        Dim bUseProvData As Boolean
        Dim rsPD As DataTable = New DataTable
        Dim dbCommandPD As SQLiteCommand
        dbCommandPD = db.CreateCommand
        dbCommandPD.CommandText = "SELECT UseProvData FROM DataDict WHERE [Table] = """ & sTbl & """"
        Dim rdPD As SQLiteDataReader
        rdPD = dbCommandPD.ExecuteReader
        rsPD.Load(rdPD)

        If rsPD.Rows.Count > 0 Then
            If Not IsDBNull(rsPD.Rows(0).Item("UseProvData")) Then
                bUseProvData = rsPD.Rows(0).Item("UseProvData")
            End If
        End If

        If bAll And Not bUseProvData Then 'jrs 2012/05/21 added and not bUseProvData
            'jrs 2007/01/29 GDPPC
            'jrs 2026/02/27 changing to constant dollars year
            If sTbl = "SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP" Then
                Factor = rsTRows(0).Item(sBaseYear) * rsTPOP.Rows(0).Item(sBaseYear) / Total
            Else
                Factor = rsTRows(0).Item(sBaseYear) / Total
            End If

            If Factor <> 1 Then
                Dim dbCommand4 As SQLiteCommand
                dbCommand4 = db.CreateCommand

                Do While rsPidx < rsP.Rows.Count
                    Dim sUpd As String
                    sUpd = "UPDATE " & sTbl & " SET [" & sBaseYear & "] = [" & sBaseYear & "] * " & Factor.ToString
                    If bMostRec Then
                        sUpd = sUpd & ", MostRecent = [" & sBaseYear & "]"
                    End If
                    sUpd = sUpd & " WHERE Country = """ & rsP.Rows(rsPidx).Item("Province") & """"
                    'jrs 2017/07/11 removing warning
                    '                'jrs 2007/01/17
                    '                If rs.Fields(sBaseYear) < 0.1 And Not sTbl = "SeriesLandArea" Then 'jrs 2014/11/06 added and not LandArea
                    '                    MsgBox "If not already doing so, the user should probably put the province " & rsP.Fields("Province") & _
                    '                    " into a region because province is too small to produce good model results.", vbInformation, "Add Pronvinces"
                    '                End If
                    dbCommand4.CommandText = sUpd
                    dbCommand4.ExecuteNonQuery()
                    rsPidx = rsPidx + 1
                Loop
                rsPidx = 0
            End If
        End If

        Dim f As Integer
        'jrs 2007/09/27 C for Country, P for Province
        Dim iYr2 As Integer
        Dim slope_C As Single, slope_P As Single
        Dim point_C As Single, point_P As Single
        Dim diff_C As Single

        'Check that there is data in other years
        'if not use proportion of BaseYear
        'this procedure assumes the same structure in Source (rsT) and Target (rs) tables
        '===========================================
        'jrs 2012/07/13
        'Don't fill other years when Use Province Data flag is on
        If Not bUseProvData Then
            'jrs 2026/03/05 
            Dim bRefresh As Boolean = False
            Do While rsPidx < rsP.Rows.Count
                'jrs 2026/02/27 Dim rsRows = rs.Select("= """ & rsP.Rows(rsPidx).Item("Province") & """")
                Dim rsRows = rs.Select("Country = '" & rsP.Rows(rsPidx).Item("Province").ToString.Replace("'", "''") & "'")
                'Dim rsDRows = rsD.Select("= """ & rsP.Rows(rsPidx).Item("Province") & """")
                Dim rsDRows = rsD.Select("Country = '" & rsP.Rows(rsPidx).Item("Province").ToString.Replace("'", "''") & "'")

                Dim dbCommandrs As SQLiteCommand
                dbCommandrs = db.CreateCommand
                Dim sUpd As String = ""
                For f = 0 To rs.Columns.Count - 1
                    If rs.Columns(f).ColumnName <> "Country" And rs.Columns(f).ColumnName <> "FIPS_CODE" And
                        rs.Columns(f).ColumnName <> sBaseYear And Not (rs.Columns(f).ColumnName = "MostRecent" And bMostRec And bAll) Then
                        If Not IsNumeric(rsRows(0).Item(f)) And IsNumeric(rs.Columns(f).ColumnName) Then 'jrs 2007/02/06 added And IsNumeric(rs.Fields(f).Name)
                            iYr = LookForClosestYear(CInt(rs.Columns(f).ColumnName), rsD, rsDRows, CInt(rsT.Columns(rsT.Columns.Count - 3).ColumnName), CInt(rsT.Columns(2).ColumnName))
                            iYr2 = LookForClosestYear2(CInt(rs.Columns(f).ColumnName), iYr, rsD, rsDRows, CInt(rsT.Columns(rsT.Columns.Count - 3).ColumnName), CInt(rsT.Columns(2).ColumnName))
                            If sUpd > "" Then
                                sUpd = sUpd & ", "
                            End If
                            If iYr2 = 0 Then
                                sUpd = sUpd & "[" & rs.Columns(f).ColumnName & "] = " & CStr(rsTRows(0).Item(rs.Columns(f).ColumnName) * rsRows(0).Item(CStr(iYr)) / rsTRows(0).Item(CStr(iYr)))
                            Else
                                slope_C = (rsTRows(0).Item(CStr(iYr2)) - rsTRows(0).Item(CStr(iYr))) / (iYr2 - iYr)
                                point_C = slope_C * (CInt(rs.Columns(f).ColumnName) - iYr) + rsTRows(0).Item(CStr(iYr))
                                diff_C = (rsTRows(0).Item(rs.Columns(f).ColumnName) - point_C) / point_C
                                slope_P = (rsRows(0).Item(CStr(iYr2)) - rsRows(0).Item(CStr(iYr))) / (iYr2 - iYr)
                                point_P = slope_P * (CInt(rs.Columns(f).ColumnName) - iYr) + rsRows(0).Item(CStr(iYr))
                                sUpd = sUpd & "[" & rs.Columns(f).ColumnName & "] = " & CStr(point_P * (1 + diff_C))
                            End If
                        End If
                    End If
                Next f

                'jrs 2026/03/02
                If sUpd > "" Then
                    sUpd = "UPDATE " & sTbl & " SET " & sUpd & " WHERE Country = """ & rsP.Rows(rsPidx).Item("Province") & """"
                    dbCommandrs.CommandText = sUpd
                    dbCommandrs.ExecuteNonQuery()
                    'jrs 2026/03/05 now need to reload rs to be included in Totals
                    bRefresh = True
                End If
                rsPidx = rsPidx + 1
            Loop
            'jrs 2026/03/05
            If bRefresh Then
                rd.Close()
                rd = dbCommand.ExecuteReader
                If rd.FieldCount = rs.Columns.Count Then
                    rs.Dispose()
                    rs = Nothing
                    rs = New DataTable
                    rs.Load(rd)
                End If
            End If
        End If

        'Normalize other years
        '===========================================

        If bAll And Not bUseProvData Then 'jrs 2012/05/21 added and not bUseProvData
            Dim Totals() As Single
            ReDim Totals(rs.Columns.Count - 1)
            For f = 2 To rs.Columns.Count - 1
                If IsNumeric(rs.Columns(f).ColumnName) Then 'jrs 2007/02/06
                    'jrs 2026/02/27 changing to constant dollars year
                    'jrs 2014/07/16
                    If sTbl = "SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP" Then
                        If Val(rs.Columns(f).ColumnName) > Val(rsPop.Columns(rsPop.Columns.Count - 3).ColumnName) Then 'jrs 2016/12/12 added .name to rsPop
                            Exit For 'if Pop goes only to 2010 and GDP2011PCPPP goes to 2012
                        End If
                    End If

                    rsPidx = 0
                    Do While rsPidx < rsP.Rows.Count
                        'jrs 2026/02/27 
                        'Dim rsRows = rs.Select("= """ & rsP.Rows(rsPidx).Item("Province") & """")
                        Dim rsRows = rs.Select("Country = '" & rsP.Rows(rsPidx).Item("Province").ToString.Replace("'", "''") & "'")
                        'jrs 2007/01/29 GDPPC
                        'jrs 2026/02/27 changing to constant dollars year
                        If sTbl = "SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP" Then
                            'jrs 2026/02/27 Dim rsPopRows = rsPop.Select("= """ & rsP.Rows(rsPidx).Item("Province") & """")
                            Dim rsPopRows = rsPop.Select("Country = '" & rsP.Rows(rsPidx).Item("Province").ToString.Replace("'", "''") & "'")
                            'jrs 2019/09/11 added IIF(isNull(rsPop.Fields(rs.Fields(f).Name)
                            'jrs 2026/03/02
                            If Not IsDBNull(rsRows(0).Item(f)) Then
                                Totals(f) = Totals(f) + rsRows(0).Item(f) * IIf(IsDBNull(rsPopRows(0).Item(rs.Columns(f).ColumnName)), 0, rsPopRows(0).Item(rs.Columns(f).ColumnName))
                            End If
                        Else
                            'jrs 2026/03/02
                            If Not IsDBNull(rsRows(0).Item(f)) Then
                                Totals(f) = Totals(f) + rsRows(0).Item(f)
                            End If
                        End If
                        rsPidx = rsPidx + 1
                    Loop
                End If
            Next f

            rsPidx = 0
            Do While rsPidx < rsP.Rows.Count
                'jrs 2026/02/27 
                'Dim rsRows = rs.Select("= """ & rsP.Rows(rsPidx).Item("Province") & """")
                Dim rsRows = rs.Select("Country = '" & rsP.Rows(rsPidx).Item("Province").ToString.Replace("'", "''") & "'")
                Dim dbCommandrs As SQLiteCommand
                dbCommandrs = db.CreateCommand
                Dim sUpd As String = ""
                'jrs 2026/03/05
                Dim sUpd2 As String = ""
                For f = 2 To rs.Columns.Count - 1
                    If IsNumeric(rs.Columns(f).ColumnName) And Totals(f) > 0 Then 'jrs 2007/02/06 'jrs 2014/07/16 added and Totals(f) > 0
                        If sUpd > "" Then
                            sUpd = sUpd & ", "
                        End If
                        sUpd = sUpd & "[" & rs.Columns(f).ColumnName & "] = "
                        'jrs 2007/01/29 GDPPC
                        'jrs 2026/02/27 changing to constant dollars year
                        If sTbl = "SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP" Then
                            'jrs 2026/03/02
                            If Not IsDBNull(rsRows(0).Item(f)) Then
                                sUpd = sUpd & CStr(rsRows(0).Item(f) * rsTRows(0).Item(rs.Columns(f).ColumnName) * rsTPOP(0).Item(rs.Columns(f).ColumnName) / Totals(f))
                            Else
                                sUpd = sUpd & "Null"
                            End If
                        Else
                            'jrs 2026/03/02
                            If Not IsDBNull(rsRows(0).Item(f)) And Not IsDBNull(rsTRows(0).Item(f)) And Totals(f) > 0 Then
                                sUpd = sUpd & CStr(rsRows(0).Item(f) * rsTRows(0).Item(f) / Totals(f))
                            Else
                                sUpd = sUpd & "Null"
                            End If
                        End If
                        'jrs 2015/02/04
                        'Updating Earliest and Most Recent fields too
                        If f = rs.Columns.Count - 3 Then
                            'jrs 2026/03/05
                            'sUpd = sUpd & ", MostRecent = " & rsRows(0).Item(f)
                            'sUpd = sUpd & ", Earliest = " & rsRows(0).Item(2)
                            sUpd2 = "MostRecent = [" & rs.Columns(f).ColumnName
                            sUpd2 = sUpd2 & "], Earliest = [" & rs.Columns(2).ColumnName & "]"
                        End If
                    End If
                Next f

                'jrs 2026/03/02
                If sUpd > "" Then
                    sUpd = "UPDATE " & sTbl & " SET " & sUpd & " WHERE Country = """ & rsP.Rows(rsPidx).Item("Province") & """"
                    dbCommandrs.CommandText = sUpd
                    dbCommandrs.ExecuteNonQuery()
                    'jrs 2026/03/05
                    If sUpd2 > "" Then
                        sUpd2 = "UPDATE " & sTbl & " SET " & sUpd2 & " WHERE Country = """ & rsP.Rows(rsPidx).Item("Province") & """"
                        dbCommandrs.CommandText = sUpd2
                        dbCommandrs.ExecuteNonQuery()
                    End If
                End If
                rsPidx = rsPidx + 1
            Loop
        End If

        CheckTable = True
    End Function

    Private Sub CheckFieldOrderinTable(rs As DataTable, db As SqliteConnection, sTbl As String)
        'I don't know how this was possible but SeriesPopulation was created with a the last field as 2006
        'but when using .fields(.count-1) showing 2005, which is second to last
        'Solution is to recreate the table with the right order

        Dim i As Integer, CurrYr As Integer, bFlag As Boolean
        CurrYr = 0
        bFlag = False 'True when fields out of order (eventhough they might look ordered).
        For i = 2 To rs.Columns.Count - 1
            If IsNumeric(rs.Columns(i).ColumnName) Then
                If CurrYr < CInt(rs.Columns(i).ColumnName) Then
                    CurrYr = CInt(rs.Columns(i).ColumnName)
                Else
                    bFlag = True
                    Exit For
                End If
            End If
        Next i

        'if out of order recreate
        If bFlag Then
            'Load field names
            Dim AllYrs() As String, TotYrs As Long
            If rs.Columns(rs.Columns.Count - 1).ColumnName = "MostRecent" Then
                TotYrs = rs.Columns.Count - 4
            Else
                TotYrs = rs.Columns.Count - 2
            End If
            ReDim AllYrs(TotYrs)
            For i = 2 To TotYrs + 1
                AllYrs(i - 1) = rs.Columns(i).ColumnName
            Next i
            QuickSort(AllYrs, 1, TotYrs)
            'Create insert string
            Dim sCreate As String = "CREATE TABLE """ & sTbl & """ (""Country"" VARCHAR(255), ""FIPS_CODE"" VARCHAR(255)"
            Dim sIns As String = "INSERT INTO [" & sTbl & "]("
            Dim sFlds As String
            sCreate = sCreate & ", """ & AllYrs(1) & """ DOUBLE(53)"
            sFlds = "Country, FIPS_CODE, [" & AllYrs(1) & "]"
            For i = 2 To TotYrs
                sCreate = sCreate & ", """ & AllYrs(i) & """ DOUBLE(53)"
                sFlds = sFlds & ", [" & AllYrs(i) & "]"
            Next i
            sCreate = sCreate & ")"

            'Sort them
            rs = Nothing
            'Rename current table
            Dim dbCommand As SqliteCommand
            dbCommand = db.CreateCommand
            dbCommand.CommandText = "ALTER TABLE [" & sTbl & "] RENAME TO [tmp" & sTbl & "]"
            dbCommand.ExecuteNonQuery()

            'Load Table Definition
            dbCommand.CommandText = sCreate
            dbCommand.ExecuteNonQuery()

            'Reinsert into copy
            dbCommand.CommandText = sIns & sFlds & ") SELECT " & sFlds & " FROM [tmp" & sTbl & "]"
            dbCommand.ExecuteNonQuery()

            'Create Index if it exists
            Dim k As Integer
            '        If tblD.Indexes.Count > 0 Then
            '            db.TableDefs.Refresh
            '            Dim idx As Index
            '    Set idx = db.TableDefs(sTbl).CreateIndex(tblD.Indexes(0).Name)
            '    For k = 0 To tblD.Indexes(0).Fields.Count - 1
            '                idx.Fields.append idx.CreateField(tblD.Indexes(0).Fields(k).Name)
            '    Next k
            '            db.TableDefs(sTbl).Indexes.append idx
            'End If

            'Remove original table
            dbCommand.CommandText = "DROP TABLE [tmp" & sTbl & "]"
            dbCommand.ExecuteNonQuery()



            'Reopen the table
            Dim rd As SqliteDataReader
            dbCommand.CommandText = "SELECT * FROM [" & sTbl & "]"
            rd = dbCommand.ExecuteReader
            rs.Load(rd)

        End If
    End Sub

    'Checks that Table has same structure than target, if not recreates table
    Private Sub CheckTableStructure(ByRef rsS As DataTable, rsT As DataTable, dbs As SqliteConnection, dbT As SqliteConnection, sTbl As String, sProv As String)
        Dim bFlag As Boolean
        If rsS.Columns.Count <> rsT.Columns.Count Then
            bFlag = True
        End If
        If Not bFlag Then
            Dim i As Integer
            For i = rsS.Columns.Count - 1 To 0 Step -1
                If rsS.Columns(i).ColumnName <> rsT.Columns(i).ColumnName Then
                    bFlag = True
                    Exit For
                End If
            Next i
        End If
        If bFlag Then
            'Recreate the table with the same structure
            'rsS = Nothing
            Dim dbSCommand As SQLiteCommand
            dbSCommand = dbs.CreateCommand
            dbSCommand.CommandText = "ALTER TABLE [" & sTbl & "] RENAME TO [tmp" & sTbl & "]"
            dbSCommand.ExecuteNonQuery()

            'Dim tblS As TableDef, tblO As TableDef
            'Set tblS = dbT.TableDefs(sTbl)
            'Set tblO = dbs.TableDefs("TMP" & sTbl)

            'dbs.TableDefs.append dbs.CreateTableDef(sTbl & "tmp", tblS.Attributes, sTbl, ";DATABASE=" & dbT.Name)
            'dbs.TableDefs.Refresh
            'dbs.Execute("SELECT * INTO [" & sTbl & "] FROM [" & sTbl & "tmp] WHERE " & tblS.Fields(0).Name & " = NULL")
            'dbs.TableDefs.Delete(tblS.Name & "tmp")
            Dim sCreate As String = "CREATE TABLE """ & sTbl & """ (""Country"" VARCHAR(255), ""FIPS_CODE"" VARCHAR(255)"

            'Create Index if it exists
            Dim k As Integer
            '        If tblS.Indexes.Count > 0 Then
            '            dbs.TableDefs.Refresh
            '            Dim idx As Index
            '           Set idx = dbs.TableDefs(tblS.Name).CreateIndex(tblS.Indexes(0).Name)
            '           For k = 0 To tblS.Indexes(0).Fields.Count - 1
            '                idx.Fields.append idx.CreateField(tblS.Indexes(0).Fields(k).Name)
            '           Next k
            '            dbs.TableDefs(tblS.Name).Indexes.append idx
            '        End If

            'jrs 2014/05/13
            Dim bEarliest As Boolean, bMostRecent As Boolean
            'Insert Data into new table
            Dim j As Integer, sFields As String
            k = 0
            For j = 0 To rsT.Columns.Count - 1
                'jrs 2022/10/06
                If rsT.Columns(j).ColumnName <> "Country" And rsT.Columns(j).ColumnName <> "FIPS_CODE" And
                        rsT.Columns(j).ColumnName <> "Earliest" And rsT.Columns(j).ColumnName <> "MostRecent" Then
                    sCreate = sCreate & ", """ & rsT.Columns(j).ColumnName & """ DOUBLE(53)"
                End If

                'jrs 2014/05/13 unfortunately I can't count on this tables having their fields sorted
                'I'll need to do a double search
                '            If tblS.Fields(j).Name > tblO.Fields(k).Name Then
                '                Do While tblS.Fields(j).Name > tblO.Fields(k).Name
                '                    k = k + 1
                '                    If k >= tblO.Fields.Count Then Exit For
                '                Loop
                '            End If
                For k = 0 To rsS.Columns.Count - 1
                    If rsT.Columns(j).ColumnName = rsS.Columns(k).ColumnName Then
                        If rsT.Columns(j).ColumnName = "Earliest" Then
                            bEarliest = True
                        ElseIf rsT.Columns(j).ColumnName = "MostRecent" Then
                            bMostRecent = True
                        Else
                            If sFields = "" Then
                                sFields = "[" & rsT.Columns(j).ColumnName & "]"
                            Else
                                sFields = sFields & ",[" & rsT.Columns(j).ColumnName & "]"
                            End If
                        End If
                        'jrs 2014/05/13 k = k + 1
                    End If
                    'jrs 2014/05/13 If k >= tblO.Fields.Count Then Exit For
                Next k
            Next j

            'jrs 2014/05/13
            If bEarliest Then
                sFields = sFields & ",[Earliest]"
                sCreate = sCreate & ", ""Earliest"" DOUBLE(53)"
            End If
            If bMostRecent Then
                sFields = sFields & ",[MostRecent]"
                sCreate = sCreate & ", ""MostRecent"" DOUBLE(53)"
            End If
            sCreate = sCreate & ")"

            dbSCommand.CommandText = sCreate
            dbSCommand.ExecuteNonQuery()

            Dim sIns As String
            sIns = "INSERT INTO [" & sTbl & "](" & sFields & ") SELECT " & sFields & " FROM [tmp" & sTbl & "]"
            dbSCommand.CommandText = sIns
            dbSCommand.ExecuteNonQuery()

            'jrs 2026/03/05
            dbs.Close()
            dbs.Open()

            Using dropCmd As SQLiteCommand = dbs.CreateCommand()
                dropCmd.CommandText = "DROP TABLE [tmp" & sTbl & "]"
                dropCmd.ExecuteNonQuery()
            End Using


            'jrs 2026/03/05
            'Delete Original table
            'dbSCommand.CommandText = "DROP TABLE [tmp" & sTbl & "]"
            'dbSCommand.ExecuteNonQuery()
            dbSCommand = dbs.CreateCommand

            'Reopen the table
            'jrs 2012/05/15
            rsS = Nothing
            rsS = New DataTable
            If sProv > "" Then
                dbSCommand.CommandText = "SELECT * FROM [" & sTbl & "] WHERE Country = """ & sProv & """"
            Else
                dbSCommand.CommandText = "SELECT * FROM " & sTbl
            End If
            Dim rdS As SQLiteDataReader
            rdS = dbSCommand.ExecuteReader
            'jrs 2022/10/06
            Dim dsAll As DataSet = New DataSet
            dsAll.EnforceConstraints = False
            dsAll.Tables.Add(rsS)

            rsS.Load(rdS)

        End If
    End Sub

    Private Function LookForClosestYear(iYear As Integer, Tbl As DataTable, TblRow() As DataRow, MaxYear As Integer, MinYear As Integer) As Integer
        'jrs 2007/04/19
        Dim iMaxYrPos As Integer
        'jrs 2007/10/08
        iMaxYrPos = Tbl.Columns.Count - 1

        Do While Not IsNumeric(Tbl.Columns(iMaxYrPos).ColumnName) And iMaxYrPos > 0
            iMaxYrPos = iMaxYrPos - 1
        Loop

        Do While Tbl.Columns(iMaxYrPos).ColumnName > MaxYear And IsNumeric(Tbl.Columns(iMaxYrPos).ColumnName)
            iMaxYrPos = iMaxYrPos - 1
        Loop

        'jrs 2007/09/27
        Dim iMinYrPos As Integer
        iMinYrPos = 2
        Do While Tbl.Columns(iMinYrPos).ColumnName < MinYear And IsNumeric(Tbl.Columns(iMinYrPos).ColumnName)
            iMinYrPos = iMinYrPos + 1
        Loop

        Dim i As Integer

        'jrs 2007/09/25
        'Check if iYear exists in table
        Dim iYear2 As Integer
        iYear2 = iYear
        'jrs 2026/03/02
        'On Error GoTo FindClosestFld
        'i = Tbl.Columns(CStr(iYear)).Ordinal
        'On Error GoTo 0
        If Tbl.Columns.Contains(CStr(iYear)) Then
            i = Tbl.Columns(CStr(iYear)).Ordinal
        Else
            iYear2 = CInt(Tbl.Columns(iMaxYrPos).ColumnName)
            i = iMaxYrPos - 1
            Do While i >= iMinYrPos 'jrs 2007/10/08 > 1
                If Math.Abs(iYear2 - iYear) > Math.Abs(CInt(Tbl.Columns(i).ColumnName) - iYear) Then
                    iYear2 = CInt(Tbl.Columns(i).ColumnName)
                End If
                If iYear > CInt(Tbl.Columns(i).ColumnName) Then Exit Do
                i = i - 1
            Loop
        End If

        'Check for the closest greater year
        'jrs 2007/08/08 OrdinalPosition starts at 1 not at 0, then I removed the + 1 -> WRONG
        'it was ignoring the next field
        'For i = tbl.Fields(CStr(iYear)).OrdinalPosition + 1 To iMaxYrPos
        'CORRECTION:
        'I just realized that SeriesPopulation starts it's OrdinalPosition at 1
        'the copy from RUNFILES, probably because it was created on the fly
        'other tables start at 0

        'jrs 2019/09/11
        Dim iStart2 As Integer
        iStart2 = iMaxYrPos
        For i = iMaxYrPos To iMinYrPos Step -1
            If Tbl.Columns(i).ColumnName = CStr(iYear2) Then
                iStart2 = i
                Exit For
            End If
        Next i

        'jrs 2019/09/11 For i = Tbl.Fields(CStr(iYear2)).OrdinalPosition To iMaxYrPos
        For i = iStart2 To iMaxYrPos
            If IsNumeric(TblRow(0).Item(i)) And IsNumeric(Tbl.Columns(i).ColumnName) Then
                LookForClosestYear = CInt(Tbl.Columns(i).ColumnName)
                Exit For
            End If
        Next i
        'Final Year to check
        Dim fYr As Integer
        If LookForClosestYear = 0 Or (2 * iYear2 - LookForClosestYear + 1 < iMinYrPos) Then
            fYr = 2
        Else
            'jrs 2026/03/02
            ''jrs 2007/08/08
            'On Error GoTo NoFldHdlr
            'fYr = Tbl.Columns(CStr(2 * iYear2 - LookForClosestYear + 1)).Ordinal
            'On Error GoTo 0
            If Tbl.Columns.Contains(CStr(2 * iYear2 - LookForClosestYear + 1)) Then
                fYr = Tbl.Columns(CStr(2 * iYear2 - LookForClosestYear + 1)).Ordinal
            Else
                If Amin(Tbl.Columns(CStr(iYear2)).Ordinal, iMaxYrPos) - 1 >= iMinYrPos Then
                    fYr = Tbl.Columns(CStr(iYear2)).Ordinal - 1
                End If

                'jrs 2007/10/08 it used to start at 2 now at iMinYrPos
                'jrs 2019/09/11 added Amin
                For i = iMinYrPos To Amin(Tbl.Columns(CStr(iYear2)).Ordinal, iMaxYrPos) - 1
                    If Val(Tbl.Columns(i).ColumnName) > 2 * iYear - LookForClosestYear + 1 Then
                        fYr = i
                        Exit For
                    End If
                Next i
            End If
            'jrs 2007/10/09
            If fYr = 0 Or fYr < iMinYrPos Then  'jrs 2014/10/29 added or fYr < iMinYrPos
                fYr = iMinYrPos
            End If
        End If
        'Check for the closest smaller year
        'jrs 2019/09/11 For i = Tbl.Fields(CStr(iYear2)).OrdinalPosition - 1 To fYr Step -1
        For i = iStart2 - 1 To fYr Step -1
            If IsNumeric(TblRow(0).Item(i)) And IsNumeric(Tbl.Columns(i).ColumnName) Then
                LookForClosestYear = CInt(Tbl.Columns(i).ColumnName)
                Exit For
            End If
        Next i

        Exit Function

        'NoFldHdlr:
        '        'jrs 2007/08/08
        '        'if the field is not found
        '        'look for the first year available
        '        'jrs 2007/10/08 added if statement to check value of fYr
        '        'jrs 2019/09/11 added Amin
        '        If Amin(Tbl.Columns(CStr(iYear2)).Ordinal, iMaxYrPos) - 1 >= iMinYrPos Then
        '            fYr = Tbl.Columns(CStr(iYear2)).Ordinal - 1
        '        End If

        '        'jrs 2007/10/08 it used to start at 2 now at iMinYrPos
        '        'jrs 2019/09/11 added Amin
        '        For i = iMinYrPos To Amin(Tbl.Columns(CStr(iYear2)).Ordinal, iMaxYrPos) - 1
        '            If Val(Tbl.Columns(i).ColumnName) > 2 * iYear - LookForClosestYear + 1 Then
        '                fYr = i
        '                Exit For
        '            End If
        '        Next i
        '        Resume Next

        'FindClosestFld:
        '        'jrs 2007/09/25
        '        iYear2 = CInt(Tbl.Columns(iMaxYrPos).ColumnName)
        '        i = iMaxYrPos - 1
        '        Do While i >= iMinYrPos 'jrs 2007/10/08 > 1
        '            If Math.Abs(iYear2 - iYear) > Math.Abs(CInt(Tbl.Columns(i).ColumnName) - iYear) Then
        '                iYear2 = CInt(Tbl.Columns(i).ColumnName)
        '            End If
        '            If iYear > CInt(Tbl.Columns(i).ColumnName) Then Exit Do
        '            i = i - 1
        '        Loop
        '        'Back to procedure
        '        Resume Next
    End Function

    Private Function LookForClosestYear2(iYear As Integer, iYear1 As Integer, Tbl As DataTable, TblRow() As DataRow, MaxYear As Integer, MinYear As Integer) As Integer
        'This function returns the closest to iYear, in the opossite direction of iYear1

        'jrs 2007/04/19
        Dim iMaxYrPos As Integer
        'jrs 2007/10/08
        iMaxYrPos = Tbl.Columns.Count - 1

        Do While Not IsNumeric(Tbl.Columns(iMaxYrPos).ColumnName) And iMaxYrPos > 0
            iMaxYrPos = iMaxYrPos - 1
        Loop

        Do While Tbl.Columns(iMaxYrPos).ColumnName > MaxYear And IsNumeric(Tbl.Columns(iMaxYrPos).ColumnName)
            iMaxYrPos = iMaxYrPos - 1
        Loop

        'jrs 2007/09/27
        Dim iMinYrPos As Integer
        iMinYrPos = 2
        Do While Tbl.Columns(iMinYrPos).ColumnName < MinYear And IsNumeric(Tbl.Columns(iMinYrPos).ColumnName)
            iMinYrPos = iMinYrPos + 1
        Loop

        Dim i As Integer

        'jrs 2007/09/25
        'Check if iYear exists in table
        Dim iYear2 As Integer
        iYear2 = iYear
        'jrs 2026/03/02
        'On Error GoTo FindClosestFld
        'i = Tbl.Columns(CStr(iYear)).Ordinal
        'On Error GoTo 0
        If Tbl.Columns.Contains(CStr(iYear)) Then
            i = Tbl.Columns(CStr(iYear)).Ordinal
        Else
            iYear2 = CInt(Tbl.Columns(iMaxYrPos).ColumnName)
            i = iMaxYrPos - 1
            Do While i >= iMinYrPos
                If Math.Abs(iYear2 - iYear) > Math.Abs(CInt(Tbl.Columns(i).ColumnName) - iYear) Then
                    iYear2 = CInt(Tbl.Columns(i).ColumnName)
                End If
                If iYear > CInt(Tbl.Columns(i).ColumnName) Then Exit Do
                i = i - 1
            Loop
        End If

        'jrs 2019/09/11
        Dim iStart2 As Integer
        iStart2 = iMaxYrPos
        For i = iMaxYrPos To iMinYrPos Step -1
            If Tbl.Columns(i).ColumnName = CStr(iYear2) Then
                iStart2 = i
                Exit For
            End If
        Next i

        Dim iDir As Integer
        'Look for smaller years
        If iYear1 > iYear Then
            iDir = -1
        Else 'look for greater years
            iDir = 1
        End If
        Dim str As Integer

        If iDir > 0 Then
            'Check for the closest greater year
            'jrs 2007/08/08 OrdinalPosition starts at 1 not at 0, then I removed the + 1 -> WRONG
            'it was ignoring the next field
            'For i = tbl.Fields(CStr(iYear)).OrdinalPosition + 1 To iMaxYrPos
            'CORRECTION:
            'I just realized that SeriesPopulation starts it's OrdinalPosition at 1
            'the copy from RUNFILES, probably because it was created on the fly
            'other tables start at 0
            'jrs 2007/09/28
            'jrs 2019/09/11 str = Tbl.Fields(CStr(iYear2)).OrdinalPosition
            str = iStart2
            If iYear2 <> iYear Then
                str = str + 1
            End If

            For i = str To iMaxYrPos
                If IsNumeric(TblRow(0).Item(i)) And IsNumeric(Tbl.Columns(i).ColumnName) Then
                    LookForClosestYear2 = CInt(Tbl.Columns(i).ColumnName)
                    Exit For
                End If
            Next i
        Else
            'Check for the closest smaller year
            'jrs 2019/09/11 For i = Tbl.Fields(CStr(iYear2)).OrdinalPosition - 1 To iMinYrPos Step -1
            For i = iStart2 - 1 To iMinYrPos Step -1
                If IsNumeric(TblRow(0).Item(i)) And IsNumeric(Tbl.Columns(i).ColumnName) Then
                    LookForClosestYear2 = CInt(Tbl.Columns(i).ColumnName)
                    Exit For
                End If
            Next i
        End If


        Exit Function

        'FindClosestFld:
        '        'jrs 2007/09/25
        '        iYear2 = CInt(Tbl.Columns(iMaxYrPos).ColumnName)
        '        i = iMaxYrPos - 1
        '        Do While i >= iMinYrPos
        '            If Math.Abs(iYear2 - iYear) > Math.Abs(CInt(Tbl.Columns(i).ColumnName) - iYear) Then
        '                iYear2 = CInt(Tbl.Columns(i).ColumnName)
        '            End If
        '            If iYear > CInt(Tbl.Columns(i).ColumnName) Then Exit Do
        '            i = i - 1
        '        Loop
        '        'Back to procedure
        '        Resume Next
    End Function

    Private Function BuildProvincesStr(rs As DataTable) As String
        Dim s As String
        Dim rsIdx As Integer = 0
        Do While rsIdx < rs.Rows.Count
            If s = "" Then
                s = """" & rs.Rows(rsIdx).Item("Province") & """"
            Else
                s = s & ",""" & rs.Rows(rsIdx).Item("Province") & """"
            End If
            rsIdx = rsIdx + 1
        Loop
        BuildProvincesStr = s
    End Function

    Private Sub ApplyFactorTable(sCountry As String, sProvs As String)
        'Open Province Database
        Dim db As SQLiteConnection
        db = New SQLiteConnection(CType("DataSource=" & Session("FdrvRun") + "IFsHistSeries" + sCountry + ".db;", String))
        db.Open()

        'Open Country Database
        Dim dbC As SQLiteConnection
        dbC = New SQLiteConnection(CType("DataSource=" & Session("FdrvRun") + "IFsHistSeries.db;", String))
        dbC.Open()
        'If DataDict doesn't exist just exit
        On Error GoTo CloseandLeave
        'Most Recent Process
        CheckMostRecent(db)
        'Compute Factor
        ComputeFactor(db, dbC, sCountry, sProvs)
        'Retrieve Tables to Apply Factor to
        Dim rs As SqliteDataReader
        Dim dbCmdrs, dbCmd, dbCCmdrsC As SqliteCommand
        dbCmdrs = db.CreateCommand
        dbCmd = db.CreateCommand
        dbCCmdrsC = dbC.CreateCommand
        dbCmdrs.CommandText = "SELECT [Table], Multiplier, ApplyMultAll, UseProvData FROM DataDict WHERE Multiplier IS NOT NULL AND UseProvData = 0"
        rs = dbCmdrs.ExecuteReader
        Dim i As Integer, Tbl As SqliteDataReader, iMRYr As Integer ', tblC As TableDef, fld As Field
        Dim dbCmdTbl, dbCmdP As SqliteCommand
        dbCmdTbl = db.CreateCommand
        dbCmdP = db.CreateCommand
        'jrs 2007/05/28
        Dim rsC, rsP As SqliteDataReader
        'jrs 2022/10/07
        Dim tblP As DataTable = New DataTable
        Dim tblC As DataTable = New DataTable
        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(tblP)
        dsAll.Tables.Add(tblC)

        Dim ProvinceLabel As String
        'For each Table Apply Multiplier
        While rs.Read
            'jrs 2007/04/27
            On Error GoTo ContinueWNextTbl
            'jrs 2007/05/18
TopofTheLoop:
            dbCmdTbl.CommandText = "SELECT * FROM [" & rs("Table") & "]"
            Tbl = dbCmdTbl.ExecuteReader
            If rs("ApplyMultAll") Then
                For i = 0 To Tbl.GetSchemaTable.Rows.Count - 1
                    If IsNumeric(Tbl.GetName(i)) Or Tbl.GetName(i) = "MostRecent" Then
                        dbCmd.CommandText = "UPDATE [" & rs("Table") & "] SET [" & Tbl.GetName(i) & "] = [" & Tbl.GetName(i) & "] * " & rs("Multiplier")
                        dbCmd.ExecuteNonQuery()
                    End If
                Next i
            Else
                'jrs 2007/05/28 Set tblC = dbC.TableDefs(rs.Fields("Table"))
                'Set the Province year to use
                dbCmdP.CommandText = "SELECT * FROM [" & rs("Table") & "] WHERE MostRecent IS NOT NULL"
                rsP = dbCmdP.ExecuteReader
                tblP.Load(rsP)

                If tblP.Columns.Contains(FirstYearForecast.ToString) Then
                    ProvinceLabel = CStr(FirstYearForecast)
                Else
                    ProvinceLabel = "MostRecent"
                End If
                rsP.Close()

                dbCCmdrsC.CommandText = "SELECT * FROM [" & rs("Table") & "] WHERE Country = """ & sCountry & """"
                rsC = dbCCmdrsC.ExecuteReader
                tblC.Load(rsC)

                If tblC.Columns.Contains(FirstYearForecast.ToString) Then
                    iMRYr = FirstYearForecast
                Else
                    'Find Most Recent Year for the Country
                    'jrs 2007/05/28 iMRYr = CInt(tblC.Fields(tblC.Fields.Count - 3).Name)
                    i = tblC.Columns.Count - 3
                    iMRYr = CInt(tblC.Columns(i).ColumnName)
                    Do While Not IsNumeric(rsC(CStr(iMRYr)))
                        i = i - 1
                        If i < 0 Then Exit Do
                        iMRYr = CInt(tblC.Columns(i).ColumnName)
                    Loop
                End If
                rsC.Close()

                'jrs 2022/09/28 Not necessary anymore, we have previously checked structure of the table
                'Check if it doesn't exist to create it in Provinces file
                'For i = Tbl.Fields.Count - 2 To 0 Step -1
                '    If IsNumeric(Tbl.Fields(i).Name) Then
                '        If Tbl.Fields(i).Name = CStr(iMRYr) Then
                '            Exit For
                '        ElseIf Tbl.Fields(i).Name < CStr(iMRYr) Then
                '    'Create the field
                '    Set fld = Tbl.CreateField(CStr(iMRYr), dbDouble)
                '    fld.OrdinalPosition = Tbl.Fields(i).OrdinalPosition + 1
                '            Tbl.Fields.append fld
                '    Exit For
                '        End If
                '    End If
                'Next i

                'if not found and all years are greater than most recent
                'If i <= 0 Then
                '    For i = 0 To Tbl.GetSchemaTable.Columns.Count - 1
                '        If IsNumeric(Tbl.GetSchemaTable.Columns(i).ColumnName) Then
                '    'Create the field
                '    Set fld = Tbl.CreateField(CStr(iMRYr), dbDouble)
                '    fld.OrdinalPosition = Tbl.Fields(i).OrdinalPosition
                '            Tbl.Fields.append fld
                '    Exit For
                '        End If
                '    Next i
                'End If

                'Apply the factor only to that Year
                dbCmd.CommandText = "UPDATE [" & rs("Table") & "] SET [" & CStr(iMRYr) & "] = [" & ProvinceLabel & "] * " & rs("Multiplier")
                dbCmd.ExecuteNonQuery()

                'jrs 2007/05/28
                'if we didn't save to base year, but provinces have base year
                If iMRYr <> FirstYearForecast And ProvinceLabel = CStr(FirstYearForecast) Then
                    'We should remove data from base year, or it will be picked up by the preprocessor
                    'without being affected by the factor
                    dbCmd.CommandText = "UPDATE [" & rs("Table") & "] SET [" & CStr(FirstYearForecast) & "] = NULL"
                    dbCmd.ExecuteNonQuery()
                End If
            End If
ContinueWNextTbl:
            'jrs 2022/10/07
            Tbl.Close()
            'jrs 2007/05/18
            If Err.Number > 0 Then
                Err.Clear()
                Resume TopofTheLoop
            End If
        End While
        'Close Database
        rs.Close()
CloseandLeave:
        dbC.Close()
        db.Close()
    End Sub

    Private Sub CheckMostRecent(dbT As SqliteConnection)
        Dim Tbl As SqliteDataReader
        Dim dbTCommand, dbTCommandrs, dbTCommandrsT, dbTCommand2 As SqliteCommand
        dbTCommand = dbT.CreateCommand
        dbTCommand2 = dbT.CreateCommand
        dbTCommandrs = dbT.CreateCommand
        dbTCommandrsT = dbT.CreateCommand
        dbTCommand.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'"
        Tbl = dbTCommand.ExecuteReader

        Dim rs As SqliteDataReader
        Dim rsT As SqliteDataReader
        Dim i As Integer
        While Tbl.Read
            'jrs 2012/09/18 SeriesPopMigration is a special case, it doesn't have "MostRecent",
            'but don't need it since it uses province data with no normalization
            'added AND UseProvData=False
            dbTCommandrs.CommandText = "SELECT 1 FROM DataDict WHERE [Table] = '" & Tbl("TABLE_NAME") & "' AND UseProvData=0"
            rs = dbTCommandrs.ExecuteReader

            If rs.HasRows Then
                dbTCommandrsT.CommandText = "SELECT * FROM [" & Tbl("TABLE_NAME") & "]"
                rsT = dbTCommandrsT.ExecuteReader

                'Check for MostRecent field
                If rsT.GetName(rsT.GetSchemaTable.Rows.Count - 1) <> "MostRecent" Then
                    'need to add field
                    dbTCommand2.CommandText = "ALTER TABLE " & Tbl("TABLE_NAME") & " ADD COLUMN ""MostRecent"" DOUBLE(53);"
                    dbTCommand2.ExecuteNonQuery()
                End If
                'Check Values for All provinces
                rsT.Close()
                rsT = dbTCommandrsT.ExecuteReader
                While rsT.Read
                    If IsDBNull(rsT("MostRecent")) Then
                        For i = rsT.GetSchemaTable.Rows.Count - 2 To 1 Step -1
                            If IsNumeric(rsT.GetName(i)) And IsNumeric(rsT(i)) Then
                                dbTCommand2.CommandText = "UPDATE [" & Tbl("TABLE_NAME") & "] SET [MostRecent] = " & rsT(i) & " WHERE Country = """ & rsT("Country") & """"
                                dbTCommand2.ExecuteNonQuery()
                                Exit For
                            End If
                        Next i
                    End If
                End While
                rsT.Close()
            End If
            rs.Close()
        End While
    End Sub

    Private Sub ComputeFactor(db As SqliteConnection, dbC As SqliteConnection, sCountry As String, sProvs As String)
        Dim sAgg As String
        Dim rsProv As DataTable = New DataTable
        Dim rs As SqliteDataReader
        Dim rsC As DataTable = New DataTable
        Dim rdC, rdProv As SqliteDataReader
        Dim sSQL As String
        Dim Tbl As SqliteDataReader
        Dim dbCmdTbl, dbCmdrs, dbCCmdrsC, dbCmdrsProv, dbCmd As SqliteCommand
        dbCmdTbl = db.CreateCommand
        dbCmdrs = db.CreateCommand
        dbCCmdrsC = dbC.CreateCommand
        dbCmdrsProv = db.CreateCommand
        dbCmd = db.CreateCommand
        dbCmdTbl.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'"
        Tbl = dbCmdTbl.ExecuteReader

        'jrs 2007/03/30
        Dim dbDD As SQLiteConnection
        dbDD = New SQLiteConnection(CType("DataSource=" & Session("PutFdrvRun") & "DataDict.db;", String))
        dbDD.Open()

        Dim rdDD As SqliteDataReader
        Dim dbDDCmdrsDD As SqliteCommand
        dbDDCmdrsDD = dbDD.CreateCommand
        dbDDCmdrsDD.CommandText = "SELECT * FROM DataDict"
        rdDD = dbDDCmdrsDD.ExecuteReader

        Dim rsDD As DataTable = New DataTable
        'jrs 2022/10/07
        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(rsDD)
        dsAll.Tables.Add(rsC)
        rsDD.Load(rdDD)

        'jrs 2007/05/25
        Dim CountryValue As Single
        'jrs 2007/05/28
        Dim ProvinceLabel As String

        On Error GoTo TableNotFound

        'For each Table in the file
        While Tbl.Read
            'jrs 2015/07/14 added SeriesLandArea
            'jrs 2012/09/18 added AND UseProvData=False
            'jrs 2026/02/27 using ConstantDollarsYear
            dbCmdrs.CommandText = "SELECT Multiplier, Aggregation FROM DataDict WHERE [Table] = '" & Tbl("TABLE_NAME") &
                "' AND [Table] NOT IN ('SeriesGDP" & ConstantDollarsYear.ToString & "', 'SeriesPopulation', 'SeriesGDP" & ConstantDollarsYear.ToString & "PCPPP', 'SeriesLandArea') AND UseProvData=0"
            rs = dbCmdrs.ExecuteReader
            If rs.HasRows Then
                'Read Country Value
                'jrs 2007/05/25 it used to be MostRecnt, now we'll check for Base Year first
                'Set rsC = dbC.OpenRecordset("SELECT MostRecent FROM [" & tbl.Name & "] WHERE Country = '" & sCountry & "'")
                dbCCmdrsC.CommandText = "SELECT * FROM [" & Tbl("TABLE_NAME") & "] WHERE Country = """ & sCountry & """"
                rdC = dbCCmdrsC.ExecuteReader
                rsC.Load(rdC)
                CountryValue = getYearOrMstRct(rsC, FirstYearForecast, 0)

                If IsDBNull(rs("Aggregation")) Then
                    'jrs 2026/02/27 changing format
                    'jrs 2007/03/30
                    sAgg = UCase(rsDD.Select("TABLE = '" & Tbl("TABLE_NAME"))(0).Item("Aggregation") & "'") 'jrs 2014/11/13 added ucase
                    'sAgg = "POP"
                Else
                    sAgg = UCase(rs("Aggregation")) 'jrs 2014/11/13 added ucase
                End If

                'jrs 2007/05/28
                dbCmdrsProv.CommandText = "SELECT * FROM [" & Tbl("TABLE_NAME") & "] WHERE MostRecent IS NOT NULL AND Country IN (" & sProvs & ")"
                rdProv = dbCmdrsProv.ExecuteReader
                dsAll.Tables.Add(rsProv)
                rsProv.Load(rdProv)
                If checkField(rsProv, FirstYearForecast) Then
                    ProvinceLabel = CStr(FirstYearForecast)
                Else
                    ProvinceLabel = "MostRecent"
                End If
                rdProv.Close()
                rsProv = New DataTable

                'Compute Province Value
                Select Case sAgg
                    Case "SUM"
                        sSQL = "SELECT SUM([" & ProvinceLabel & "]) AS PROVAL FROM [" & Tbl("TABLE_NAME") & "] WHERE MostRecent IS NOT NULL AND Country IN (" & sProvs & ")"
                    Case "AVG"
                        sSQL = "SELECT AVG([" & ProvinceLabel & "]) AS PROVAL FROM [" & Tbl("TABLE_NAME") & "] WHERE MostRecent IS NOT NULL AND Country IN (" & sProvs & ")"
                    Case "POP"
                        sSQL = "SELECT SUM(T.[" & ProvinceLabel & "] * P.[" & CStr(FirstYearForecast) & "])/SUM(P.[" & CStr(FirstYearForecast) & "]) AS PROVAL FROM [" & Tbl("TABLE_NAME") & "] AS T, SeriesPopulation as P WHERE T.Country = P.Country AND T.MostRecent IS NOT NULL AND T.Country IN (" & sProvs & ")"
                    Case "GDP"
                        sSQL = "SELECT SUM(T.[" & ProvinceLabel & "] * G.[" & CStr(FirstYearForecast) & "])/SUM(G.[" & CStr(FirstYearForecast) & "]) AS PROVAL FROM [" & Tbl("TABLE_NAME") & "] AS T, SeriesGDP" & ConstantDollarsYear.ToString & " as G WHERE T.Country = G.Country AND T.MostRecent IS NOT NULL AND T.Country IN (" & sProvs & ")"
                    Case "LND" 'jrs 2012/11/16
                        sSQL = "SELECT SUM(T.[" & ProvinceLabel & "] * l.[" & CStr(FirstYearForecast) & "])/SUM(L.[" & CStr(FirstYearForecast) & "]) AS PROVAL FROM [" & Tbl("TABLE_NAME") & "] AS T, SeriesLandArea as L WHERE T.Country = L.Country AND T.MostRecent IS NOT NULL AND T.Country IN (" & sProvs & ")"
                    Case Else 'jrs 2012/11/16
                        sSQL = ""
                End Select

                'jrs 2012/11/16
                If sSQL > "" Then
                    dbCmdrsProv.CommandText = sSQL
                    rdProv = dbCmdrsProv.ExecuteReader
                    rsProv.Load(rdProv)
                    If rsProv.Rows(0).Item("PROVAL") <> 0 Then
                        dbCmd.CommandText = "UPDATE DataDict SET [Multiplier] = " & CStr(CountryValue / rsProv.Rows(0).Item("PROVAL")) & " WHERE [Table] = '" & Tbl("TABLE_NAME") & "'"
                        dbCmd.ExecuteNonQuery()
                    End If
                End If

                rdProv.Close()
                rsProv = New DataTable
            End If
NextTable:
            rs.Close()
        End While

        'jrs 2007/03/30
        rdDD.Close()
        dbDD.Close()

        Exit Sub

TableNotFound:
        Err.Clear()
        Resume NextTable
    End Sub


    'This function checks if the file is not there and then copies it
    Private Sub FileCopy_NotThere(ByRef InFile$, ByRef OutFile$)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        On Error GoTo copy
        Dim FDT1 = FileDateTime(OutFile$)

        Exit Sub

copy:
        If Dir(InFile$) > "" Then
            FileCopy(InFile$, OutFile$)
        Else
            Session("Message") = Session("Message") & "<br/>File " + InFile$ + " doesn't exist, procceding without it."
            OutFile$ = ""
        End If

    End Sub

    'This function returns true if it finds the file, false if it doesn't
    Private Function myFileCheck(sFileName) As Boolean
        myFileCheck = True
        On Error GoTo FailedCheck
        Dim FDT1 = FileDateTime(sFileName)
        Exit Function

FailedCheck:
        myFileCheck = False
    End Function

    'This function returns true if it finds the table, false if it doesn't
    Private Function myTablecheck(ByRef dbT As SqliteConnection, ByRef sTbl As String, ByRef rsT As SqliteDataReader, ByVal sProv As String, ByRef dbT1 As SqliteConnection, ByRef rsT1 As SqliteDataReader, ByRef dbTCmdrsT1 As SqliteCommand) As Boolean
        'dbT is the database for provinces
        'dbT1 is the original database for countries

        'rsT is the table for provinces
        'rsT1 is the table for full countries
        'If sTbl = "SeriesICTBroadbandMobileSubsPer100" Then Stop
        'jrs 2007/02/06
        If sTbl = "SFAllBlanks" Then
            myTablecheck = False
            Exit Function
        End If
        On Error GoTo FailedCheck
        Dim dbTCmd As SqliteCommand
        dbTCmd = dbT.CreateCommand
        dbTCmd.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name = """ & sTbl & """"
        Dim rsT2 As SqliteDataReader
        rsT2 = dbTCmd.ExecuteReader
        Dim dbTCmdrsT As SqliteCommand
        If rsT2.HasRows Then
            dbTCmdrsT = dbT.CreateCommand
            dbTCmdrsT.CommandText = "SELECT * FROM [" & sTbl & "] WHERE Country = """ & sProv & """"
            rsT = dbTCmdrsT.ExecuteReader
            myTablecheck = rsT.HasRows
        Else
            myTablecheck = False
        End If
        'jrs 2007/02/08
        If myTablecheck Then
            'jrs 2007/02/15
            AddMissingFieldsWData(rsT, rsT1, dbT, dbT1, sTbl)

            Dim dtT, dtT1 As DataTable
            dtT = New DataTable
            'jrs 2022/10/11
            Dim ds As DataSet = New DataSet
            ds.EnforceConstraints = False
            ds.Tables.Add(dtT)
            dtT.Load(rsT)
            rsT = dbTCmdrsT.ExecuteReader
            dtT1 = New DataTable
            ds.Tables.Add(dtT1)
            dtT1.Load(rsT1)
            'jrs 2022/10/12
            rsT1 = dbTCmdrsT1.ExecuteReader

            CheckTableStructure(dtT, dtT1, dbT, dbT1, sTbl, sProv) 'jrs 2012/05/15
        End If

        Exit Function

FailedCheck:
        myTablecheck = False
    End Function

    Private Sub AddMissingFieldsWData(rsS As SqliteDataReader, rsT As SqliteDataReader, dbs As SqliteConnection, dbT As SqliteConnection, sTbl As String)
        'rsS is the table for Provinces
        'rsT is the table for full countries
        'If sTbl = "SeriesICTBroadbandMobileSubsPer100" Then Stop

        'Determine if there is a missing field with data
        Dim i As Integer, j As Integer
        j = 0
        Dim Fields As List(Of String) = New List(Of String)
        Dim FieldsT As List(Of String) = New List(Of String)
        For i = 0 To rsT.GetSchemaTable.Rows.Count - 1
            FieldsT.Add(rsT.GetName(i))
        Next
        'Find if there are missing fields
        For i = 0 To rsS.GetSchemaTable.Rows.Count - 1
            If Not FieldsT.Contains(rsS.GetName(i)) Then
                Fields.Add(rsS.GetName(i))
            End If
        Next i
        'Check if they have data
        Dim rsData As SqliteDataReader
        Dim dbSCmdrsData As SqliteCommand
        dbSCmdrsData = dbs.CreateCommand
        Dim Tbl As SqliteDataReader
        Dim dbTCmdTbl, dbTCmd As SqliteCommand
        dbTCmdTbl = dbT.CreateCommand
        dbTCmd = dbT.CreateCommand
        dbTCmdTbl.CommandText = "SELECT * FROM [" & sTbl & "]"
        Tbl = dbTCmdTbl.ExecuteReader
        If Fields.Count > 0 Then
            'Close Target Recordset
            rsT.Close()

            j = 0
            Do While Not IsNumeric(Tbl.GetName(j))
                j = j + 1
                If j >= Tbl.GetSchemaTable.Rows.Count - 1 Then Exit Do
            Loop
        End If

        'jrs 2014/05/13
        Dim bEarliest As Boolean, bMostRecent As Boolean
        'If Tbl.GetName(Tbl.GetSchemaTable.Rows.Count - 1) = "MostRecent" Then bMostRecent = True
        'If Tbl.GetName(Tbl.GetSchemaTable.Rows.Count - 2) = "Earliest" Then bEarliest = True
        'Dim fld As New Field
        'On Error GoTo Hdl
        For Each f In Fields
            dbSCmdrsData.CommandText = "SELECT COUNT(*) as C FROM [" & sTbl & "] WHERE [" & f & "] IS NOT NULL"
            rsData = dbSCmdrsData.ExecuteReader
            'There's Data
            If rsData("C") > 0 Then
                'Add them to the Target Table
                Do While f > Tbl.GetName(j)
                    j = j + 1
                    If j >= Tbl.GetSchemaTable.Rows.Count Then Exit Do
                Loop
                dbTCmd.CommandText = "ALTER TABLE " & sTbl & " ADD COLUMN """ & f & """ DOUBLE(53);"
                'jrs 2014/05/13 move the ordinal position of the "Earliest" and "MostRecent"
                'If bEarliest Then Tbl.Fields("Earliest").OrdinalPosition = Tbl.Fields("Earliest").OrdinalPosition + 1
                'If bMostRecent Then Tbl.Fields("MostRecent").OrdinalPosition = Tbl.Fields("MostRecent").OrdinalPosition + 1
            End If
        Next

        'Reopen Target Recordset
        If Fields.Count > 0 Then
            rsT = dbTCmdTbl.ExecuteReader
        End If

        '    Exit Sub
        'Hdl:
        '    MsgBox Err.Description
        '    Resume Next
    End Sub

    'jrs 2007/01/16 added parameter sDisAgg
    Private Function AlternativeEstimation(sFile As String, sTable As String, sDisAgg As String, dbAE As SqliteConnection) As Boolean
        AlternativeEstimation = False
        Select Case UCase(sFile)
            Case "IFS"
                If InStr(UCase(sTable), "COHORT") > 0 Then
                    If InStr(UCase(sTable), "COUNTRY") > 0 Then
                        AlternativeEstimation = True
                    End If
                ElseIf InStr(UCase(sTable), "EDUCATTAIN") > 0 Then 'jrs 2020/02/26
                    If InStr(UCase(sTable), "COUNTRY") > 0 Then
                        AlternativeEstimation = True
                    End If
                ElseIf InStr(UCase(sTable), "POVRATEAGE") > 0 Then 'jrs 2020/03/02
                    If InStr(UCase(sTable), "COUNTRY") > 0 Then
                        AlternativeEstimation = True
                    End If
                ElseIf sTable = "CountryParameters" Then
                    AlternativeEstimation = True
                ElseIf sTable = "Country Translation" Then
                    AlternativeEstimation = True
                ElseIf InStr(UCase(sTable), "HEALTHDETAILEDDEATHSCTRY") > 0 Then 'jrs 2012/02/17
                    AlternativeEstimation = True
                ElseIf sTable = "HealthLongevityAlphaBetaCountry" Then 'jrs 2012/10/26
                    AlternativeEstimation = True
                ElseIf sTable = "Currency" Then
                    AlternativeEstimation = True
                ElseIf sTable = "HistMigration" Then 'jrs 2016/02/19
                    AlternativeEstimation = True
                ElseIf sTable = "CountryWasColonyOf" Then 'jrs 2016/02/22
                    AlternativeEstimation = True
                ElseIf sTable = "TariffRatePerSectorCountry" Then 'jrs 2019/10/11
                    AlternativeEstimation = True
                End If
            Case "IFSHISTSERIES"
                Dim rsAE As SqliteDataReader
                Dim dbAECmdrsAE As SqliteCommand
                dbAECmdrsAE = dbAE.CreateCommand
                'jrs 2007/01/16 added Aggregation and DisAggregation
                dbAECmdrsAE.CommandText = "SELECT Aggregation, Disaggregation, UsedInPreprocessor FROM DataDict WHERE Table = '" & sTable & "'"
                rsAE = dbAECmdrsAE.ExecuteReader
                While rsAE.Read
                    If rsAE("UsedInPreprocessor") Then
                        AlternativeEstimation = True
                        'mti 2022/03/06 start
                        '                    If UCase(rsAE.Fields("Aggregation")) = "SUM" Then
                        '                        sDisAgg = rsAE.Fields("Disaggregation")
                        '                    Else
                        '
                        '                        sDisAgg = ""
                        '                    End If

                        sDisAgg = UCase(rsAE("Aggregation"))
                        If sDisAgg = "SUM" Then
                            sDisAgg = rsAE("Disaggregation")
                        Else
                            sDisAgg = rsAE("Disaggregation")
                            If sDisAgg = "EST" Then
                                AlternativeEstimation = False
                            Else
                                sDisAgg = ""
                            End If
                        End If

                        'mti 2022/03/06 End
                    End If
                End While

                rsAE.Close()
        End Select
    End Function

    Private Function getCohortVal(sMigCohort As String, rsCoh As SqliteDataReader) As Single
        Dim i As Integer
        Select Case sMigCohort
            Case "0-4"
                getCohortVal = rsCoh(3)
            Case "65+"
                getCohortVal = 0
                For i = rsCoh.GetSchemaTable.Rows.Count - 1 To 16 Step -1
                    If Not IsDBNull(rsCoh(i)) Then
                        getCohortVal = getCohortVal + rsCoh(i)
                    End If
                Next i
            Case Else
                Dim sFst As String
                Dim pos As Integer
                pos = InStr(sMigCohort, "-")
                sFst = Mid(sMigCohort, 1, pos - 1)
                i = Val(sFst) / 5 + 3
                getCohortVal = rsCoh(i)
        End Select
    End Function

    Private Sub UpdateDistFldxProv(dbTarget As SqliteConnection, sProv As String)
        'For each year
        Dim rsYrs As SqliteDataReader, rsTotals As SqliteDataReader
        Dim dbTargetCmdrsYrs, dbTargetCmdrsTotals, dbTargetCmd As SqliteCommand
        dbTargetCmdrsYrs = dbTarget.CreateCommand
        dbTargetCmdrsTotals = dbTarget.CreateCommand
        dbTargetCmd = dbTarget.CreateCommand
        dbTargetCmdrsYrs.CommandText = "SELECT DISTINCT Year FROM HistMigration WHERE Country = """ & sProv & """"
        rsYrs = dbTargetCmdrsYrs.ExecuteReader
        While rsYrs.Read
            'Read Total for the Province
            dbTargetCmdrsTotals.CommandText = "SELECT SUM(MigrantsMales) as M, SUM(MigrantsFemales) as F FROM HistMigration WHERE Country = """ & sProv & """ AND Year = " & rsYrs("Year")
            rsTotals = dbTargetCmdrsTotals.ExecuteReader
            'Loop through each age
            Dim sUpd As String = "UPDATE HistMigration SET MigPctDistMales = MigrantsMales * 100 / " & rsTotals("M").ToString
            sUpd = sUpd & ", MigPctDistFemales = MigrantsFemales * 100 / " & rsTotals("F").ToString
            sUpd = sUpd & ", MigPctDistTotal = (MigrantsMales + MigrantsFemales) * 100 / " & (rsTotals("M") + rsTotals("F")).ToString
            sUpd = sUpd & " WHERE Country = """ & sProv & """ AND Year = " & rsYrs("Year")

            dbTargetCmd.CommandText = sUpd
            dbTargetCmd.ExecuteNonQuery()
            rsTotals.Close()
        End While
        rsYrs.Close()
    End Sub

    Private Sub frm_AddDelProvinces_Init(sender As Object, e As EventArgs) Handles Me.Init
        InitializeComponent()
    End Sub

    Private Sub Timer1_Elapsed(sender As Object, e As TimerElapsedEventArgs) Handles Timer1.Elapsed
        IfsVB60 = Session("IfsVB60")

        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")

        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Dim sLine, prevLine As String

            Try
                Using reader As StreamReader = New StreamReader(CStr(Session("PutFdrvRun") & "progress.txt"))
                    ' Read last line from file
                    sLine = reader.ReadLine
                    prevLine = sLine
                    Do While Not sLine = Nothing
                        prevLine = sLine
                        sLine = reader.ReadLine
                    Loop

                    lblJSReb.Text = prevLine
                End Using
            Catch ex As Exception
                lblJSReb.Text = prevLine
            End Try

        End If
        'mti WebForm Changes From June 21, 2004  -end commented for testing

        If IfsVB60.HasExited() Then
            lblJSReb.Text = "VERY IMPORTANT: PLEASE READ. Adding Provinces is complete.<br>"
            lblJSReb.Text = lblJSReb.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;You should Rebuild the Base to recompute <br>"
            lblJSReb.Text = lblJSReb.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;all calculated variables.<br>"

            Dim sRegions As String
            sRegions = lstRegions.Text
            If lstCtryProv.Items.Count > 0 Then
                sRegions = sRegions & lstCtryProv.Items(0).Text
            End If
            UpdateCurrentlyRunningModel(sRegions)
            EnableDisable(True)
            LoadCountryProvinces()
            Session("NeedFileWrite") = True
            RebuildBase()
            lblJSReb.Text = lblJSReb.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;All the necessary files have been saved successfully."
            Timer1.Visible = False
            cmdRebuildBase.Visible = True
            cmdAdd.Text = "(A)dd"
        End If
    End Sub


    Private Sub RebuildBase()
        If Session("NeedFileWrite") Then
            'jrs 2007/04/04
            Session("bLoadRebuildBase") = True

            Dim Msg$ = "<br>IMPORTANT WARNING:  IFs will copy IFs.DB, IFsHistSeries.DB, "
            Msg$ = Msg$ + "IFsCoVaTra.DB, IFsCoVaTraSeries.DB and IFsWVSCohort.DB from "
            Msg$ = Msg$ + "the local drive ('Runfiles' directory) to the Data directory. "
            Msg$ = Msg$ + "You may need help and/or permissions to do so.  "
            Msg$ = Msg$ + "Failure to do so will cause you to lose changes in "
            Msg$ = Msg$ + "Countries upon restarting IFs.<br>"
            lblJSReb.Text = lblJSReb.Text & Msg
            'Screen.MousePointer = vbHourglass


            'jrs 2008/04/08 remove CountryProvinces folder
            'jrs 2007/03/14
            '        Dim sCtrys As New Collection
            '        readCountryWAllProvinces sCtrys

            '        If sCtrys.Count > 0 Then
            '            'jrs 2007/04/24
            On Error GoTo CountryProvincesHdlr
            '            Dim bCreateDir As Boolean
            '
            '            If Dir(DataFileDir$ + "CountryProvinces", vbDirectory) = "" Then
            '                'Create the directory
            '                MkDir DataFileDir$ + "CountryProvinces"
            '                bCreateDir = True
            '            End If
            '            InFile$ = PutFdrvRun$ + "Ifs.mdb"
            '            OutFile$ = DataFileDir$ + "CountryProvinces\Ifs.mdb"
            '            FileCopy InFile$, OutFile$
            '            InFile$ = PutFdrvRun$ + "IfsHistSeries.mdb"
            '            OutFile$ = DataFileDir$ + "CountryProvinces\IfsHistSeries.mdb"
            '            FileCopy InFile$, OutFile$
            '            InFile$ = PutFdrvRun$ + "IFsWVSCohort.mdb"
            '            OutFile$ = DataFileDir$ + "CountryProvinces\IFsWVSCohort.mdb"
            '            FileCopy InFile$, OutFile$
            '            InFile$ = PutFdrvRun$ + "IFsCoVaTra.mdb"
            '            OutFile$ = DataFileDir$ + "CountryProvinces\IFsCoVaTra.mdb"
            '            FileCopy InFile$, OutFile$
            '            InFile$ = PutFdrvRun$ + "IFsCoVaTraSeries.mdb"
            '            OutFile$ = DataFileDir$ + "CountryProvinces\IFsCoVaTraSeries.mdb"
            '            FileCopy InFile$, OutFile$
            '        Else
            '            If Dir(DataFileDir$ + "CountryProvinces", vbDirectory) > "" Then
            '                'Remove the directory
            '                Kill DataFileDir$ + "CountryProvinces\Ifs.mdb"
            '                Kill DataFileDir$ + "CountryProvinces\IfsHistSeries.mdb"
            '                Kill DataFileDir$ + "CountryProvinces\IFsWVSCohort.mdb"
            '                Kill DataFileDir$ + "CountryProvinces\IFsCoVaTra.mdb"
            '                Kill DataFileDir$ + "CountryProvinces\IFsCoVaTraSeries.mdb"
            '                RmDir DataFileDir$ + "CountryProvinces"
            '            End If
            '            'Copy Files from Runfiles to DATA
            '            InFile$ = PutFdrvRun$ + "Ifs.mdb"
            '            OutFile$ = DataFileDir$ + "Ifs.mdb"
            '            FileCopy InFile$, OutFile$
            '            InFile$ = PutFdrvRun$ + "IfsHistSeries.mdb"
            '            OutFile$ = DataFileDir$ + "IfsHistSeries.mdb"
            '            FileCopy InFile$, OutFile$
            '            InFile$ = PutFdrvRun$ + "IFsWVSCohort.mdb"
            '            OutFile$ = DataFileDir$ + "IFsWVSCohort.mdb"
            '            FileCopy InFile$, OutFile$
            '            InFile$ = PutFdrvRun$ + "IFsCoVaTra.mdb"
            '            OutFile$ = DataFileDir$ + "IFsCoVaTra.mdb"
            '            FileCopy InFile$, OutFile$
            '            InFile$ = PutFdrvRun$ + "IFsCoVaTraSeries.mdb"
            '            OutFile$ = DataFileDir$ + "IFsCoVaTraSeries.mdb"
            '            FileCopy InFile$, OutFile$
            '        End If

            'jrs 2008/04/08 this used to be outside the if Need Write File
            '------------------------------------------------------------

            'If using All provinces delete country data in RUNFILES
            'DeleteCountriesFromRUNFILES
            Dim InFile$ = Session("PutFdrvRun") & "Ifs.db"
            Dim OutFile$ = DataFileDir$ & "Ifs.db"
            FileCopy(InFile$, OutFile$)
            InFile$ = Session("PutFdrvRun") & "IfsHistSeries.db"
            OutFile$ = DataFileDir$ & "IfsHistSeries.db"
            FileCopy(InFile$, OutFile$)
            InFile$ = Session("PutFdrvRun") & "IFsWVSCohort.db"
            OutFile$ = DataFileDir$ & "IFsWVSCohort.db"
            FileCopy(InFile$, OutFile$)
            InFile$ = Session("PutFdrvRun") & "IFsCoVaTra.db"
            OutFile$ = DataFileDir$ & "IFsCoVaTra.db"
            FileCopy(InFile$, OutFile$)
            InFile$ = Session("PutFdrvRun") & "IFsCoVaTraSeries.db"
            OutFile$ = DataFileDir$ & "IFsCoVaTraSeries.db"
            FileCopy(InFile$, OutFile$)
            'jrs 2026/03/25
            InFile$ = Session("PutFdrvRun") & "IfsHistSeriesDyadic.db"
            OutFile$ = DataFileDir$ & "IfsHistSeriesDyadic.db"
            If Dir(InFile$) > "" Then
                FileCopy(InFile$, OutFile$)
            End If


            'jrs 2007/03/19
            'If using All provinces we should also save the final version
            'without countries in DATA, and not only the backup version in
            'DATA\CountryProvinces
            'jrs 2008/04/08 If sCtrys.Count > 0 Then
            'jrs 2008/04/08 End If
        End If

        Exit Sub

CountryProvincesHdlr:
        lblJSReb.Text = "There was an error trying to save your changes, " & Chr(13) & Err.Description
        '    If sCtrys.Count > 0 Then
        '        'The directory was just created
        '        If bCreateDir Then
        '            'Remove the directory
        '            If Dir(DataFileDir$ + "CountryProvinces\Ifs.mdb") > "" Then
        '                Kill DataFileDir$ + "CountryProvinces\Ifs.mdb"
        '            End If
        '            If Dir(DataFileDir$ + "CountryProvinces\IfsHistSeries.mdb") > "" Then
        '                Kill DataFileDir$ + "CountryProvinces\IfsHistSeries.mdb"
        '            End If
        '            If Dir(DataFileDir$ + "CountryProvinces\IFsWVSCohort.mdb") > "" Then
        '                Kill DataFileDir$ + "CountryProvinces\IFsWVSCohort.mdb"
        '            End If
        '            If Dir(DataFileDir$ + "CountryProvinces\IFsCoVaTra.mdb") > "" Then
        '                Kill DataFileDir$ + "CountryProvinces\IFsCoVaTra.mdb"
        '            End If
        '            If Dir(DataFileDir$ + "CountryProvinces\IFsCoVaTraSeries.mdb") > "" Then
        '                Kill DataFileDir$ + "CountryProvinces\IFsCoVaTraSeries.mdb"
        '            End If
        '            RmDir DataFileDir$ + "CountryProvinces"
        '        End If
        '    End If
        '    daoDB36.Close

    End Sub


    Protected Sub cmdSwitch_Click(sender As Object, e As EventArgs) Handles cmdSwitch.Click
        If lstSwitch.Text = "" Then
            Session("Message") = "No Data Selected."
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        Else
            'jrs 2022/02/21
            'If not switching to the base case and currently using no countries
            If lstCtryProv.Items.Count = 0 Then
                'then need to check if the base case is up to date in case we want to go back
                CheckAndSaveBaseIfNeeded()
            End If

            EnableDisable(False)
            lblJSReb.Text = "Switching in progress. Please Wait....."
            Dim sw As New SwitchingHandler(Session("PutFdrvRun"))

            sw.PerformSwitching(lstSwitch.Items(lstSwitch.SelectedIndex).ToString, txtSaveB4Switch.Value)
            lstCtryProv.Items.Clear()

            If lstSwitch.Items(lstSwitch.SelectedIndex).ToString <> "No State/Province" Then
                lstCtryProv.Items.Add(lstSwitch.Items(lstSwitch.SelectedIndex).ToString)
            End If
            cmdRefresh_Click(Me, New EventArgs)
            sw.Reinitialize(Session("IFs_MDB2"))
            lblJSReb.Text = ""
            EnableDisable(True)

            Session("FileForDisplay0") = Nothing
            Session("FileForDisplay1") = Nothing
            Session("MMVarName") = Nothing
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            DeleteDFInCache(0, Session)
            DeleteDFInCache(1, Session)
        End If
    End Sub

    Private Sub CheckAndSaveBaseIfNeeded()
        Dim bCondition As Boolean
        bCondition = False

        'Check any difference in all files
        Dim sw As New SwitchingHandler(Session("PutFdrvRun"))
        bCondition = sw.bAnyDiff

        If bCondition Then
            cmdSave_Click(Me, New EventArgs)
        End If

    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Dim sw As New SwitchingHandler(Session("PutFdrvRun"))
        'If lstCtryProv.Items.Count = 0 Then
        '            MsgBox "Model is running without state/province data. There is nothing to delete."
        'Else
        'jrs 2008/09/10
        If lstCtryProv.Items.Count = 1 And lstCtryProv.SelectedIndex < 0 Then
            lstCtryProv.SelectedIndex = 0
        End If

        '      If lstCtryProv.ListIndex < 0 Then
        '              MsgBox "Nothing is selected from country subregion list box to delete."
        '      Else
        EnableDisable(False)
        sw.PerformPredeleteActions(lstCtryProv.Items(lstCtryProv.SelectedIndex).Text)
        FeedCurrentlyRunningModel()
        'jrs 2012/05/18 EnableDisable True
        lstCtryProv.Items.Clear()
        'jrs 2012/05/18
        EnableDisable(True)
        lblJSReb.Text = "Delete operation finished."
        cmdRefresh_Click(Me, New EventArgs)
        '    End If
        'End If
    End Sub

    Private Sub cmdRebuildBase_Click(sender As Object, e As EventArgs) Handles cmdRebuildBase.Click
        ReLoadRegionsandGroups(Session("IFs_MDB2"))
        Support.FormManager.Show(Me, "frm_RebuildBase.aspx")
    End Sub
End Class

