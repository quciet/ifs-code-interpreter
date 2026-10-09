Option Strict Off
Option Explicit On
'mti August 2004 5 -start
Imports System.Data 'mtiwebforms April 2004 ADO.Net
Imports System.Data.OleDb 'mtiwebforms April 2004 ADO.Net
'jrs 2022/07/14
Imports System.Data.SQLite
Imports System.Globalization
Imports Microsoft.VisualBasic.Compatibility
Imports NLog

'Namespace IFsWeb

'mti August 2004 5 -end



Public Module FUNCTION_Renamed

    Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

    Dim WinFormsToWeb_Messagebox As ArtinSoft.WinFormsToWeb.WebControls.MessageBox
    '
    '------------------------------------------Function.Bas-------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last changed:  August 21, 2000

    Dim ICount As Short


    '=============================Function.BAS  =======================
    Function AMAX(ByRef Value As Object, ByRef limit As Object) As Object ' returns the larger of value or limit
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRS 01/27/04
        If IsDBNull(Value) Then
            Value = limit - 1
        End If
        If IsDBNull(limit) Then
            limit = Value - 1
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object limit. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Value >= limit Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object AMAX. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            AMAX = Value
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object limit. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object AMAX. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            AMAX = limit
        End If

    End Function
    '=============================Function.BAS  =======================
    Function Amin(ByRef Value As Object, ByRef limit As Object) As Object ' returns the smaller of value or limit
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Couldn't resolve default property of object limit. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Value > limit Then
            'UPGRADE_WARNING: Couldn't resolve default property of object limit. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Amin. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Amin = limit
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Amin. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Amin = Value
        End If

    End Function

    '=============================  Function.Bas  ========================
    'jrs 2005/11/14 function not been used
    '    Sub SelectNewFile(ByRef Filename As String, ByRef RunFiles As Object, ByRef ActiveBase As String, ByRef FilesSelected As Short)
    '        Dim frm_msg As New frm_Message
    '        Dim msg As String
    '        Dim firstyearfile As Short
    '        Dim Number As Single
    '        Dim i2 As Object
    '        Dim RECORD As Long
    '        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '        For i2 = 1 To FilesSelected
    '            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            If InStr(Filename, RunFiles(i2)) Then GoTo AlreadySelected
    '        Next i2
    '        'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    '        If ActiveBase = "IFSBASE.RUN" And InStr(UCase(Filename), "IFSBASE.RUN") > 0 Then GoTo AlreadySelected
    '        'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    '        If ActiveBase = "IFSBASEHIST.RUN" And InStr(UCase(Filename), "IFSBASEHIST.RUN") > 0 Then GoTo AlreadySelected

    '        'jrs 2005/02/12
    '        Dim fNum As Integer
    '        fNum = FreeFile()
    '        FileOpen(fNum, Filename, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
    '        'UPGRADE_WARNING: Couldn't resolve default property of object PosModYearI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        'UPGRADE_WARNING: Couldn't resolve default property of object VarPoint(PosModYearI). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        'UPGRADE_WARNING: Couldn't resolve default property of object RECORD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        RECORD = VarPoint(PosModYearI)
    '        'UPGRADE_WARNING: Couldn't resolve default property of object RECORD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        RECORD = RECORD * 4 - 3
    '        'UPGRADE_WARNING: Couldn't resolve default property of object RECORD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
    '        FileGet(fNum, Number, RECORD)
    '        firstyearfile = Number 'FirstYearForecast%
    '        FileClose(fNum)
    '        'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    '        If ActiveBase = "IFSBASE.RUN" And firstyearfile <> FirstYearForecast Then
    '            msg = "You are working with forecast files and this is not a forecast file."
    '            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

    '            frm_msg.LoadMessage(msg)
    '            Support.FormManager.StateStore(me)
    '            Support.FormManager.Show(me, "frm_Message.aspx")

    '        End If
    '        'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    '        If ActiveBase = "IFSBASEHIST.RUN" And firstyearfile <> FirstYearHistory Then
    '            msg = "You are working with history files and this is not a history file."
    '            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

    '            frm_msg.LoadMessage(msg)
    '            Support.FormManager.StateStore(me)
    '            Support.FormManager.Show(me, "frm_Message.aspx")

    '        End If

    '        FilesSelected = FilesSelected + 1
    '        If FilesSelected > 10 Then FilesSelected = 10
    '        RunFiles(FilesSelected) = Filename
    '        msg = "You have now selected '" & Filename
    '        msg = msg & "' as file number " & Str(FilesSelected)
    '        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

    '        frm_msg.LoadMessage(msg)
    '        Support.FormManager.StateStore(me)
    '        Support.FormManager.Show(me, "frm_Message.aspx")


    'AlreadySelected:
    '        msg = "The file indicated has already been selected for display"
    '        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

    '        frm_msg.LoadMessage(msg)
    '        Support.FormManager.StateStore(me)
    '        Support.FormManager.Show(me, "frm_Message.aspx")

    '    End Sub


    '=============================  Function.Bas  ========================
    Sub ClearPlot(ByRef COUNTE As Integer, ByRef PLOT As Object, ByRef VARN As Object, ByRef GR1N As Object, ByRef GR2N As Object,
    ByRef GR3N As Object, ByRef GR4N As Object, ByRef FirstYear As Integer, ByRef LastYear As Integer, ByRef LastYearForecast As Integer,
    ByRef FDNG1 As Object, ByRef FDNG2 As Object, ByRef FDNG3 As Object, ByRef FDNG4 As Object, ByRef FinYear As Object, ByRef FinYearHis As Object,
    ByVal buseAllHis As Boolean, ByRef YAxisLabel As String, ByRef XAxisLabel As String, ByRef StaYearHis As Object, ByRef sUsrDefCats As Object)
        Dim ILOOP As Short
        Dim i2 As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        COUNTE = 0
        'jrs 2011/07/22 in case session variables are lost
        'For i2 = 1 To LastYear - FirstYear + 1
        '    For ICount = 1 To MaxVariables
        '        PLOT(i2, ICount) = ""
        '    Next ICount
        'Next i2
        ''jrs 2010/08/20
        ''Clean the rest of the plot in case of using all available history
        'If LastYear = LastYearHistory And buseAllHis Then
        '    For i2 = LastYear% - FirstYear% + 2 To LastYearForecast - FirstYear + 1
        '        For ICount = 1 To MaxVariables
        '            PLOT(i2, ICount) = ""
        '        Next ICount
        '    Next i2
        'End If

        'For ILOOP = 1 To MaxVariables
        '    VARN(1, ILOOP) = ""
        '    VARN(2, ILOOP) = ""
        '    VARN(3, ILOOP) = "" 'Help ID code
        '    GR1N(ILOOP) = ""
        '    GR2N(ILOOP) = ""
        '    GR3N(ILOOP) = ""
        '    GR4N(ILOOP) = "" 'jrs 2006/05/16 Dim3

        '    'jrs 10/27/04 Flexible Dimension Names for Graphs
        '    FDNG1(ILOOP) = ""
        '    FDNG2(ILOOP) = ""
        '    FDNG3(ILOOP) = ""
        '    FDNG4(ILOOP) = ""
        '    'mti May 2005 HistoryplusForecast Dim 2-start
        '    FinYear(ILOOP) = ""
        '    'mti May 2005 HistoryplusForecast Dim 2 -end

        '    'jrs 2006/09/22
        '    FinYearHis(ILOOP) = 0

        'Next ILOOP

        'jrs 2010/11/08
        YAxisLabel = ""
        XAxisLabel = ""

        'jrs 2011/07/22 this readim statements were added in case PLOT and others are gone from the Session array
        'ReDim FileNumbr(MaxVariables)
        ReDim VARN(6, MaxVariables)
        ReDim GR1N(MaxVariables + 1)
        ReDim GR2N(MaxVariables + 1)
        ReDim GR3N(MaxVariables + 1)
        ReDim GR4N(MaxVariables + 1) 'jrs 2006/06/16
        ReDim PLOT(MaxYears, MaxVariables)
        'ReDim GeoRegion(MaxVariables), GeoValue(MaxVariables)
        ReDim FinYear(MaxVariables)
        ReDim FinYearHis(MaxVariables)
        ReDim StaYearHis(MaxVariables)
        'ReDim VL(MaxVariables)
        'ReDim PLOTComp(MaxYears)
        'ReDim PLOTAll(MaxYears, MaxVariables)
        'ReDim aFormulas(1)

        ReDim FDNG1(MaxVariables + 1)
        ReDim FDNG2(MaxVariables + 1)
        ReDim FDNG3(MaxVariables + 1)
        ReDim FDNG4(MaxVariables + 1) 'jrs 2006/06/16
        'jrs 2023/05/29
        ReDim sUsrDefCats(MaxVariables)
    End Sub



    '=============================Function.BAS  =======================
    Function ConvergeOverTime(ByRef Base As Object, ByRef Target As Object, ByRef YearsToConverge As Object, ByRef IY As Short) As Object
        Dim TimeFac As Object
        ' converges to target (often predicted) value over specified years
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Couldn't resolve default property of object AMAX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object YearsToConverge. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        YearsToConverge = AMAX(YearsToConverge, 1)
        'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object TimeFac. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TimeFac = Amin(IY, YearsToConverge)
        'UPGRADE_WARNING: Couldn't resolve default property of object YearsToConverge. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object TimeFac. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Target. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Base. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object ConvergeOverTime. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ConvergeOverTime = (Base * (YearsToConverge - TimeFac) + Target * TimeFac) / YearsToConverge

    End Function

    'JRS 2/17/03
    Sub InitializePlot(ByRef COUNTE As Integer, ByRef PLOT As Object, ByRef FirstYear As Integer, ByRef LastYear As Integer)
        Dim i2 As Object
        COUNTE = 0
        For i2 = 1 To LastYear - FirstYear + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For ICount = 1 To MaxVariables
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object PLOT(i2, ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                PLOT(i2, ICount) = 0
            Next ICount
        Next i2
    End Sub

    'JRS 04/24/04 ADO
    Sub SetDataTablefromAccess(ByVal AccessDBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable, Optional ByVal DatabasePassword As String = "", Optional ByVal reqOptimJournal As Boolean = False) 'mtiwebforms April 2004 ADO.Net
        'jam 2025/10/30 Adding optional parameter for journal_mode optimization
        'Sub SetDataTablefromAccess(ByVal AccessDBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable, Optional ByVal DatabasePassword As String = "") 'mtiwebforms April 2004 ADO.Net



        _logger.Debug("SetDataTablefromAccess: " + AccessDBPath + " - " + strSQL)

        'jrs 2022/07/15
        If UseOnlySQLite Then
            'jam 2025/10/30 Opening databases in default mode
            If Not reqOptimJournal Then
                SetDataTablefromSQLite(AccessDBPath, strSQL, IndexCol, DT, DatabasePassword)
            Else
                LoadDatafromSQLite(AccessDBPath, strSQL, IndexCol, DT, DatabasePassword)
            End If
            Return
        End If

        Dim cnnStr As String
        If DatabasePassword <> "" Then
            'cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
            cnnStr = "Provider=Microsoft.ACE.OLEDB.16.0;" &
               "Data Source=" & AccessDBPath &
               ";Jet OLEDB:Database Password=" & DatabasePassword
        Else
            'cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
            cnnStr = "Provider=Microsoft.ACE.OLEDB.16.0;" &
               "Data Source=" & AccessDBPath
        End If

        Dim objAdapter As New OleDb.OleDbDataAdapter(Replace(strSQL, "UPPER(", "UCASE("), cnnStr)

        'mti August 2004 5 -start
        'Dim objDataSet As New DataSet

        '' Copy the selected data into a dataset.
        'objAdapter.Fill(objDataSet)
        'DT = objDataSet.Tables(0)


        'Dim objDataSet As New DataSet

        '' Copy the selected data into a dataset.
        'mti August 2004 5
        DT = New DataTable
        'jrs 11/11/04
        On Error GoTo AccessHandler
        Dim rc As Integer = objAdapter.Fill(DT)
        'DT = objDataSet.Tables(0)
        'mti August 2004 5 end

        If IndexCol <> "" Then
            'DT.PrimaryKey = New DataColumn() {DT.Columns(0)}
            'Else
            DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
        End If

        'dt.Rows(0).Item(resultfieldname))
        'jrs 11/11/04
        Return
AccessHandler:
        Dim str As String
        _logger.Error(Err.GetException, "SetDataTablefromAccess - exception")
        str = Err.Description
        DT = Nothing
        Return
    End Sub

    'jrs 2022/07/15
    Sub SetDataTablefromSQLite(ByVal DBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable, Optional ByVal DatabasePassword As String = "")
        'SqliteConnection conn = New SqliteConnection("Data Source=MyDatabase.sqlite;Version=3;");
        'conn.SetPassword("password");
        'conn.Open();

        'conn = New SqliteConnection("Data Source=MyDatabase.sqlite;Version=3;Password=[REDACTED];");
        'conn.Open();

        _logger.Debug("SetDataTablefromSQLite:" + DBPath + " - '" + strSQL)


        DBPath = DBPath.Replace(".mdb", ".db")
        DBPath = DBPath.Replace(".MDB", ".db")

        Dim cnnStr As String
        'jrs 2022/08/02
        cnnStr = "DataSource=" & DBPath & ";"
        'If DatabasePassword <> "" Then
        '    cnnStr = "DataSource=" & DBPath & ";Password=" & DatabasePassword & ";"
        'Else
        '    cnnStr = "DataSource=" & DBPath & ";"
        'End If

        'jrs 2022/07/19
        'SQLite can't handle booleans, instead using 1 as true, 0 as false
        strSQL = strSQL.Replace(" = TRUE", " = 1")
        strSQL = strSQL.Replace(" = True", " = 1")
        strSQL = strSQL.Replace(" = FALSE", " = 0")
        strSQL = strSQL.Replace(" = False", " = 0")
        'Dim objAdapter As New OleDb.OleDbDataAdapter(strSQL, cnnStr)
        Dim vSQLiteCon As SQLiteConnection
        Dim vSQLiteCommand As SQLiteCommand
        Dim vSQLiteReader As SQLiteDataReader

        vSQLiteCon = New SQLiteConnection(cnnStr) '& "New=true;"
        vSQLiteCon.Open()

        DT = New DataTable
        On Error GoTo ErrHandler
        'objAdapter.Fill(DT)
        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = strSQL
        vSQLiteReader = vSQLiteCommand.ExecuteReader()

        If Not IsDBNull(vSQLiteReader) Then
            Dim dtSchema As DataTable
            dtSchema = New DataTable
            dtSchema = vSQLiteReader.GetSchemaTable
            For Each schemarow In dtSchema.Rows
                'jrs 2022/08/15 DT.Columns.Add(schemarow.ItemArray(0).ToString(), schemarow.ItemArray(0).GetType())
                DT.Columns.Add(schemarow.ItemArray(0).ToString(), schemarow.ItemArray(12))
            Next
            While (vSQLiteReader.Read())
                Dim ColArray As Object() = New Object(vSQLiteReader.FieldCount - 1) {}

                For i = 0 To vSQLiteReader.FieldCount - 1
                    If Not IsDBNull(vSQLiteReader(i)) Then
                        ColArray(i) = vSQLiteReader(i)
                    End If
                Next
                DT.LoadDataRow(ColArray, True)
            End While
            vSQLiteReader.Close()
        End If


        'DT = objDataSet.Tables(0)
        'mti August 2004 5 end

        If IndexCol <> "" Then
            'DT.PrimaryKey = New DataColumn() {DT.Columns(0)}
            'Else
            DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
        End If

        'dt.Rows(0).Item(resultfieldname))
        'jrs 11/11/04
        'jrs 2024/02/15 Thanks Oleg
        vSQLiteCon.Close()

        Return
ErrHandler:
        Dim str As String
        str = Err.Description
        _logger.Error(Err.GetException, "SetDataTablefromSQLite - exception")

        DT = Nothing
        'jrs 2024/02/15 Thanks Oleg
        vSQLiteCon.Close()
        Return
    End Sub

    Function SeekValue(ByVal DT As DataTable, ByVal LookupValue As Object, ByVal ResultFieldName As String) As String
        'mtiwebforms April 2004 ADO.Net
        Dim row As DataRow = DT.Rows.Find(LookupValue)
        SeekValue = ""
        If Not row Is Nothing Then
            If Not row.HasErrors Then
                'mti WebForm Changes From June 26, 2004 -start
                If Not IsDBNull(row.Item(ResultFieldName)) Then
                    SeekValue = CType(row.Item(ResultFieldName), String)
                End If
                'mti WebForm Changes From June 26, 2004 - end 
            End If
        End If

    End Function

    'jrs 2013/12/02
    Function SeekValueNoIdx(ByRef DT As DataTable, ByVal LookUpField As String, ByVal LookUpValue As String, ByVal ResultFieldName As String) As String
        Dim rws As Object
        'assuming a string
        rws = DT.Select(LookUpField & "='" & LookUpValue & "'")
        If UBound(rws) > -1 Then
            SeekValueNoIdx = rws(0).item(ResultFieldName)
        Else
            SeekValueNoIdx = ""
        End If
    End Function

    'jrs 2006/06/14
    'Function to seek the entire row for a given value
    Function SeekRow(ByVal DT As DataTable, ByVal LookupValue As Object) As DataRow
        Dim row As DataRow = DT.Rows.Find(LookupValue)
        SeekRow = Nothing
        If Not row Is Nothing Then
            If Not row.HasErrors Then
                SeekRow = row
            End If
        End If

    End Function

    Sub updateAccessWith1Command(ByVal AccessDBPath As String, ByVal strSQLUpdate As String)

        'jrs 2022/08/10
        If UseOnlySQLite Then
            updateSQLiteWith1Command(AccessDBPath, strSQLUpdate)
            Return
        End If

        'declare all variables

        'gvdb 2024/04/12
        ' update access driver to ACE
        'Dim strConnection As String = "Provider=Microsoft.Jet.OLEDB.4.0;" &
        '       "Data Source=" & AccessDBPath
        Dim strConnection As String = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" & AccessDBPath

        Dim objConn As New OleDb.OleDbConnection(strConnection)

        'strSQLQuery = "UPDATE [Fall] SET [event_date]=@EventDate, [event]=@Event, [event_time]=@EventTime, [location]=@Location WHERE ([Fall].[id] = @ID)"

        Dim objCommand As New OleDb.OleDbCommand(strSQLUpdate, objConn)

        ' Add parameters that our SQL update command needs:

        'ojCommand.Parameters.Add(New OleDbParameter("@EventDate", OleDbType.Date, 25))
        'objCommand.Parameters("@EventDate").Value = txtEventDate.Text

        'objCommand.Parameters.Add(New OleDbParameter("@Event", OleDbType.VarChar, 100))
        'objCommand.Parameters("@Event").Value = txtEvent.Text

        'objCommand.Parameters.Add(New OleDbParameter("@EventTime", OleDbType.VarChar, 49))
        'objCommand.Parameters("@EventTime").Value = txtEventTime.Text

        'objCommand.Parameters.Add(New OleDbParameter("@Location", OleDbType.VarChar, 49))
        'objCommand.Parameters("@Location").Value = txtLocation.Text

        'intID = dgDates.DataKeys(e.Item.ItemIndex)
        'objCommand.Parameters.Add(New OleDbParameter("@ID", OleDbType.Integer))
        'objCommand.Parameters("@ID").Value = intID

        ' Open the connection, execute the command, and close the connection.
        objConn.Open()
        objCommand.ExecuteNonQuery()
        objConn.Close()
    End Sub

    'jrs 2022/08/10
    Sub updateSQLiteWith1Command(ByVal DBPath As String, ByVal strSQL As String)
        'declare all variables

        DBPath = DBPath.Replace(".mdb", ".db")
        DBPath = DBPath.Replace(".MDB", ".db")

        Dim cnnStr As String
        cnnStr = "DataSource=" & DBPath & ";"

        strSQL = strSQL.Replace(" = TRUE", " = 1")
        strSQL = strSQL.Replace(" = True", " = 1")
        strSQL = strSQL.Replace(" = FALSE", " = 0")
        strSQL = strSQL.Replace(" = False", " = 0")
        'Dim objAdapter As New OleDb.OleDbDataAdapter(strSQL, cnnStr)
        Dim vSQLiteCon As SQLiteConnection
        Dim vSQLiteCommand As SQLiteCommand

        vSQLiteCon = New SQLiteConnection(cnnStr) '& "New=true;"
        vSQLiteCon.Open()

        'objAdapter.Fill(DT)
        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = strSQL
        vSQLiteCommand.ExecuteNonQuery()

    End Sub

    'mti WebForm Changes to be taken by Jose - start

    'mtiwebform June 2004 Longitudinal - bring longplotPE sub to Function
    Public Sub LoadPlotVec(ByRef V As Object, ByRef i As Short, ByRef v2 As Object)
        '------------------------------------------------------------------------------------------
        'Dim PlotLoad(UBound(V), UBound(V, 2)) As Object
        'ReDim PlotLabelsPE(UBound(v2), UBound(v2, 2))
        ''UPGRADE_WARNING: Couldn't resolve default property of object V. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'PlotLongPE = V
        ''UPGRADE_WARNING: Couldn't resolve default property of object v2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'PlotLabelsPE = v2
        'PlotSizePE = i
    End Sub
    Sub DropAccessTable(ByVal AccessDBPath As String, ByVal tblname As String)
        'declare all variables

        Dim strConnection As String = "Provider=Microsoft.Jet.OLEDB.4.0;" &
               "Data Source=" & AccessDBPath
        Dim objConn As New OleDb.OleDbConnection(strConnection)

        Dim objAdapter As New OleDb.OleDbDataAdapter

        objConn.Open()
        Dim objDataSet As New DataSet("DS")
        objAdapter.Fill(objDataSet)
        Dim x
        Dim DT As New DataTable(tblname)
        objDataSet.Tables.Remove(DT)

        'objDataSet.Tables.Remove()

        ' Open the connection, execute the command, and close the connection.
        'objConn.Open()
        'objCommand.ExecuteNonQuery()
        objConn.Close()
    End Sub
    'mti WebForm Changes to be taken by Jose- end

    'JRS 06/22/04
    Function CreateConnection(ByVal AccessDBPath As String) As OleDb.OleDbConnection
        'declare all variables

        Dim strConnection As String = "Provider=Microsoft.Jet.OLEDB.4.0;" &
               "Data Source=" & AccessDBPath
        Return New OleDb.OleDbConnection(strConnection)
    End Function

    'jrs 2023/08/23
    Function CreateConnectionSQLite(ByVal SqliteDBPath As String) As SQLiteConnection
        'declare all variables

        Dim strConnection As String = "DataSource=" & SqliteDBPath & ";"
        Return New SQLiteConnection(strConnection)
    End Function

    'JRS 06/22/04
    'Sub ExecuteSql(ByRef objconn As OleDb.OleDbConnection, ByVal strSQLUpdate As String)
    '    'declare all variables


    '    Dim objCommand As New OleDb.OleDbCommand(strSQLUpdate, objconn)

    '    objCommand.ExecuteNonQuery()
    'End Sub

    'jrs 2023/08/24
    Sub ExecuteSql(ByRef objconn As SQLiteConnection, ByVal strSQLUpdate As String)

        Dim objCommand As SQLiteCommand
        objCommand = objconn.CreateCommand
        objCommand.CommandText = strSQLUpdate

        objCommand.ExecuteNonQuery()
    End Sub

    Sub WriteDataTable2Access(ByVal AccessDBPath As String, ByVal strSQL As String, ByVal DT As DataTable, ByVal WriteType As String, Optional ByVal strSQLDelete As String = "", Optional ByVal DatabasePassword As String = "") 'mtiwebforms April 2004 ADO.Net
        'mti August 2004 5 - new proc

        'for non primary-key table open two datatables via SetDataTableforUpdate routine and then this proc with Writetype=Insert
        'for example please see Var_Names table update in scatvars page :cmd_stattistics_Click routine 

        'not tested for (primary-key-ed) update or delete

        Dim cnnStr As String
        If DatabasePassword <> "" Then
            cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
               "Data Source=" & AccessDBPath &
               ";Jet OLEDB:Database Password=" & DatabasePassword
        Else
            cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
               "Data Source=" & AccessDBPath
        End If

        'If WriteType = "Update" Then
        '    Dim objConn As New OleDb.OleDbConnection(cnnStr)
        '    objConn.Open()
        '    Dim objCommand As New OleDb.OleDbCommand(strSQLDelete, objConn)

        '    objCommand.ExecuteNonQuery()
        '    objConn.Close()
        'End If

        Dim DA As New OleDb.OleDbDataAdapter(strSQL, cnnStr)
        'DA = New OleDb.OleDbDataAdapter(strSQL, cnnStr)

        Dim oBuild As OleDbCommandBuilder
        oBuild = New OleDbCommandBuilder(DA)
        Select Case WriteType
            Case "Insert"
                DA.InsertCommand = oBuild.GetInsertCommand
            Case "Update" 'work for select with primary key only

                DA.UpdateCommand = oBuild.GetUpdateCommand
                    'DA.InsertCommand = oBuild.GetInsertCommand
            Case "Delete"
                DA.DeleteCommand = oBuild.GetDeleteCommand
        End Select


        DA.Update(DT)




    End Sub
    Sub SetDataTableforUpdate(ByVal AccessDBPath As String, ByVal strSQL As String, ByVal strSQLDelete As String, ByRef DT As DataTable, ByRef NewDT As DataTable, Optional ByVal DatabasePassword As String = "") 'mtiwebforms April 2004 ADO.Net
        'mti August 2004 5 -new proc
        'open two tables for update of non-primar key table 
        'for non primary-key table open two datatables via this SetDataTableforUpdate routine
        '  insert updated rows from the full datatable to blank datatable
        'and then use WriteDataTable2Access with Writetype=Insert
        'for example please see Var_Names table update in scatvars page :cmd_stattistics_Click routine 

        Dim cnnStr As String
        If DatabasePassword <> "" Then
            cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
               "Data Source=" & AccessDBPath &
               ";Jet OLEDB:Database Password=" & DatabasePassword
        Else
            cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
               "Data Source=" & AccessDBPath
        End If

        Dim objConn As New OleDb.OleDbConnection(cnnStr)
        objConn.Open()

        Dim objAdapter As New OleDb.OleDbDataAdapter(strSQL, objConn)


        DT = New DataTable
        objAdapter.Fill(DT)
        'DT = objDataSet.Tables(0)
        'mti August 2004 5 end


        Dim objCommand As New OleDb.OleDbCommand(strSQLDelete, objConn)

        objCommand.ExecuteNonQuery()

        NewDT = New DataTable

        objAdapter.Fill(NewDT)




        objConn.Close()
        'dt.Rows(0).Item(resultfieldname))

    End Sub

    'jrs 2006/07/14
    Public Function ADO_OleDB_CreateConnection(ByVal AccessDBPath As String, Optional ByVal DatabasePassword As String = "") As OleDbConnection
        Dim cnnStr As String
        If DatabasePassword <> "" Then
            cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
               "Data Source=" & AccessDBPath &
               ";Jet OLEDB:Database Password=" & DatabasePassword
        Else
            cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
               "Data Source=" & AccessDBPath
        End If
        ADO_OleDB_CreateConnection = New OleDbConnection(cnnStr)
    End Function

    'jrs 2006/07/14
    Public Sub ADO_OleDB_ReadDataTable(ByRef Conn As OleDbConnection, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable)

        Dim objAdapter As New OleDb.OleDbDataAdapter(strSQL, Conn)
        DT.Clear()
        On Error GoTo AccessHandler
        objAdapter.Fill(DT)

        If IndexCol <> "" Then
            DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
        End If

        Exit Sub
AccessHandler:
        _logger.Error(Err.GetException, "ADO_OleDB_ReadDataTable - error when reading from database")
        DT = Nothing

    End Sub

    'jrs 2006/07/14
    Public Function ADO_OleDB_GetReader(ByRef myCMD As OleDbCommand, ByVal strSQL As String) As OleDbDataReader
        myCMD.CommandText = strSQL

        ADO_OleDB_GetReader = myCMD.ExecuteReader()

    End Function

    'jrs 2025/07/03 rename to old
    'jrs 2012/01/27 added parameter to decide if commas are wanted (not useful when saving to csv)
    'jrs 2009/03/04 added b4Decs parameter to indicate
    'if 4 decimals should be used instead of regular formatting
    'jrs 2008/03/12 moved from TableDisplay and made public
    'JRS 09/29/03
    Public Function ValueScaleOld(ByVal Value As Double, ByVal b4Decs As Boolean, ByVal bUseCommas As Boolean) As String
        Dim ValtoShow As New VB6.FixedLengthString(15)

        'jrs 2009/03/04
        If b4Decs Then
            If bUseCommas Then
                ValtoShow.Value = RSet(VB6.Format(Value, "###,##0.0000"), Len(ValtoShow.Value))
            Else
                ValtoShow.Value = RSet(VB6.Format(Value, "#####0.0000"), Len(ValtoShow.Value))
            End If
            ValueScaleOld = ValtoShow.Value
            Exit Function
        End If

        If bUseCommas Then
            Select Case Value
                Case 100 To 1000
                    ValtoShow.Value = RSet(VB6.Format(Value, "##,###0.0"), Len(ValtoShow.Value))
                Case 10 To 100
                    ValtoShow.Value = RSet(VB6.Format(Value, "###,##0.00"), Len(ValtoShow.Value))
                Case -10 To 10
                    ValtoShow.Value = RSet(VB6.Format(Value, "###,##0.000"), Len(ValtoShow.Value))
                Case -100 To -10
                    ValtoShow.Value = RSet(VB6.Format(Value, "###,##0.00"), Len(ValtoShow.Value))
                Case -1000 To -100
                    ValtoShow.Value = RSet(VB6.Format(Value, "###,##0.0"), Len(ValtoShow.Value))
                Case Else
                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###,###,##0"), Len(ValtoShow.Value))
            End Select
        Else
            Select Case Value
                Case 100 To 1000
                    ValtoShow.Value = RSet(VB6.Format(Value, "#####0.0"), Len(ValtoShow.Value))
                Case 10 To 100
                    ValtoShow.Value = RSet(VB6.Format(Value, "#####0.00"), Len(ValtoShow.Value))
                Case -10 To 10
                    ValtoShow.Value = RSet(VB6.Format(Value, "#####0.000"), Len(ValtoShow.Value))
                Case -100 To -10
                    ValtoShow.Value = RSet(VB6.Format(Value, "#####0.00"), Len(ValtoShow.Value))
                Case -1000 To -100
                    ValtoShow.Value = RSet(VB6.Format(Value, "#####0.0"), Len(ValtoShow.Value))
                Case Else
                    ValtoShow.Value = RSet(VB6.Format(Value, "###########0"), Len(ValtoShow.Value))
            End Select
        End If
        ValueScaleOld = ValtoShow.Value
    End Function

    'jrs 2025/07/03 new function using Copilot

    Public Function ValueScale(ByVal value As Double, ByVal b4Decs As Boolean, ByVal bUseCommas As Boolean) As String
        Dim format As String

        If b4Decs Then
            format = If(bUseCommas, "###,##0.0000", "#####0.0000")
        Else
            If bUseCommas Then
                Select Case value
                    Case 100 To 1000
                        format = "##,###0.0"
                    Case 10 To 100
                        format = "###,##0.00"
                    Case -10 To 10
                        format = "###,##0.000"
                    Case -100 To -10
                        format = "###,##0.00"
                    Case -1000 To -100
                        format = "###,##0.0"
                    Case Else
                        format = "###,###,###,##0"
                End Select
            Else
                Select Case value
                    Case 100 To 1000
                        format = "#####0.0"
                    Case 10 To 100
                        format = "#####0.00"
                    Case -10 To 10
                        format = "#####0.000"
                    Case -100 To -10
                        format = "#####0.00"
                    Case -1000 To -100
                        format = "#####0.0"
                    Case Else
                        format = "###########0"
                End Select
            End If
        End If

        ' Format using invariant culture to ensure "." as decimal separator
        Dim formattedValue As String = value.ToString(format, CultureInfo.InvariantCulture)

        ' Pad left to 15 characters
        Return formattedValue.PadLeft(15)
    End Function

    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''     ''' 
    '''     JAM 2025/05/30 Managing connection with Journal Mode = WAF
    ''' 
    ''' 
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="pconStr">Database path</param>

    Function OpenJournalSQLite(ByVal pconStr As String) As SQLiteConnection


        Dim vSQLiteConF As SQLiteConnection

        On Error GoTo ErrorHandlerLite

        vSQLiteConF = New SQLiteConnection(pconStr)

        If vSQLiteConF.State <> ConnectionState.Open Then

            vSQLiteConF.Open()
        End If
        Using command As New SQLiteCommand("PRAGMA journal_mode=WAL;", vSQLiteConF)
            Dim result = command.ExecuteScalar()
        End Using

        OpenJournalSQLite = vSQLiteConF

        Exit Function

ErrorHandlerLite:
        Dim str As String
        str = Err.Description
        _logger.Error(Err.GetException, "SetDataTablefromSQLite - exception")
        vSQLiteConF.Close()

    End Function
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''     ''' 
    '''     JAM 2025/05/31 Managing connection with Journal Mode = WAL
    '''     JAM 2025/09/30 Improving Database Opening mode 
    ''' 
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="DBPath">Database path</param>
    ''' <param name="strSQL">Database path</param>
    ''' <param name="IndexCol">Database path</param>
    ''' <param name="DT">Database path</param>
    ''' <param name="DatabasePassword">Database path</param>
    ''' Based on function SetDataTablefromSQLite developed by jrs 
    '    Sub LoadDatafromSQLite(ByVal DBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable, Optional ByVal DatabasePassword As String = "")

    '        _logger.Debug("SetDataTablefromSQLite:" + DBPath + " - '" + strSQL)


    '        DBPath = DBPath.Replace(".mdb", ".db")
    '        DBPath = DBPath.Replace(".MDB", ".db")

    '        Dim cnnStr As String

    '        cnnStr = "DataSource=" & DBPath & ";"


    '        'jrs 2022/07/19
    '        'SQLite can't handle booleans, instead using 1 as true, 0 as false
    '        strSQL = strSQL.Replace(" = TRUE", " = 1")
    '        strSQL = strSQL.Replace(" = True", " = 1")
    '        strSQL = strSQL.Replace(" = FALSE", " = 0")
    '        strSQL = strSQL.Replace(" = False", " = 0")
    '        'Dim objAdapter As New OleDb.OleDbDataAdapter(strSQL, cnnStr)
    '        Dim vSQLiteCon As SQLiteConnection
    '        Dim vSQLiteCommand As SQLiteCommand
    '        Dim vSQLiteReader As SQLiteDataReader

    '        vSQLiteCon = New SQLiteConnection(cnnStr) '& "New=true;"
    '        vSQLiteCon.Open()

    '        'jam 2025/05/31
    '        Using command As New SQLiteCommand("PRAGMA journal_mode=WAL;", vSQLiteCon)
    '            Dim result = command.ExecuteScalar()
    '        End Using



    '        DT = New DataTable
    '        On Error GoTo ErrHandler
    '        'objAdapter.Fill(DT)
    '        vSQLiteCommand = vSQLiteCon.CreateCommand()
    '        vSQLiteCommand.CommandText = strSQL
    '        vSQLiteReader = vSQLiteCommand.ExecuteReader()

    '        If Not IsDBNull(vSQLiteReader) Then
    '            Dim dtSchema As DataTable
    '            dtSchema = New DataTable
    '            dtSchema = vSQLiteReader.GetSchemaTable
    '            For Each schemarow In dtSchema.Rows
    '                'jrs 2022/08/15 DT.Columns.Add(schemarow.ItemArray(0).ToString(), schemarow.ItemArray(0).GetType())
    '                DT.Columns.Add(schemarow.ItemArray(0).ToString(), schemarow.ItemArray(12))
    '            Next
    '            While (vSQLiteReader.Read())
    '                Dim ColArray As Object() = New Object(vSQLiteReader.FieldCount - 1) {}

    '                For i = 0 To vSQLiteReader.FieldCount - 1
    '                    If Not IsDBNull(vSQLiteReader(i)) Then
    '                        ColArray(i) = vSQLiteReader(i)
    '                    End If
    '                Next
    '                DT.LoadDataRow(ColArray, True)
    '            End While
    '            vSQLiteReader.Close()
    '        End If


    '        'DT = objDataSet.Tables(0)
    '        'mti August 2004 5 end

    '        If IndexCol <> "" Then
    '            'DT.PrimaryKey = New DataColumn() {DT.Columns(0)}
    '            'Else
    '            DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
    '        End If

    '        'dt.Rows(0).Item(resultfieldname))
    '        'jrs 11/11/04
    '        'jrs 2024/02/15 Thanks Oleg
    '        vSQLiteCon.Close()

    '        Return
    'ErrHandler:
    '        Dim str As String
    '        str = Err.Description
    '        _logger.Error(Err.GetException, "SetDataTablefromSQLite - exception")

    '        DT = Nothing
    '        'jrs 2024/02/15 Thanks Oleg
    '        vSQLiteCon.Close()
    '        Return
    '    End Sub
    ''' <summary>
    '''
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''      
    '''     jam 2025/05/31 Managing connection with Journal Mode = WAL
    '''     jam 2025/09/30 Improving Database Opening mode 
    '''     jam 2025/10/25 Optimizing journal mode= DELETE for opening SQLITE databases
    '''     jam 2025/11/30 Optimizing journal mode= DELETE for opening SQLITE large databases
    '''     jam 2026/04/09 Changing cache size from 50 MB to 5 MB and improve Garbage Collecto management
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub LoadDatafromSQLite(ByVal DBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable, Optional ByVal DatabasePassword As String = "")
        _logger.Debug("SetDataTablefromSQLite:" + DBPath + " - '" + strSQL)
        DBPath = DBPath.Replace(".mdb", ".db")
        DBPath = DBPath.Replace(".MDB", ".db")

        'jam 2025/10/30
        strSQL = strSQL.Replace(" = TRUE", " = 1")
        strSQL = strSQL.Replace(" = True", " = 1")
        strSQL = strSQL.Replace(" = FALSE", " = 0")
        strSQL = strSQL.Replace(" = False", " = 0")

        'jam 2025/11/30 Optimized connection string
        Dim cnnStr As String = "Data Source=" & DBPath & ";" &
              "Journal Mode=DELETE;" &
              "Pooling=True;" &
              "Cache=Shared;" &
              "Busy Timeout=90000;"

        If Not String.IsNullOrEmpty(DatabasePassword) Then
            cnnStr &= "Password=" & DatabasePassword & ";"
        End If

        DT = New DataTable
        Dim maxRetries As Integer = 3
        Dim retryDelay As Integer = 300
        Dim currentRetry As Integer = 0

        While currentRetry < maxRetries
            Try
                Using vSQLiteCon As New SQLiteConnection(cnnStr)
                    vSQLiteCon.Open()

                    Using pragmaCmd As New SQLiteCommand(vSQLiteCon)
                        pragmaCmd.CommandText = "PRAGMA synchronous=NORMAL;"
                        pragmaCmd.ExecuteNonQuery()

                        pragmaCmd.CommandText = "PRAGMA temp_store=MEMORY;"
                        pragmaCmd.ExecuteNonQuery()

                        'jam 2026/04/09 Reducing from -50000 (50MB) to -5000 (5MB) per session
                        pragmaCmd.CommandText = "PRAGMA cache_size=-5000;"
                        pragmaCmd.ExecuteNonQuery()

                        pragmaCmd.CommandText = "PRAGMA busy_timeout=60000;"
                        pragmaCmd.ExecuteNonQuery()

                        pragmaCmd.CommandText = "PRAGMA locking_mode=NORMAL;"
                        pragmaCmd.ExecuteNonQuery()
                    End Using

                    Using vSQLiteCommand As New SQLiteCommand(strSQL, vSQLiteCon)
                        vSQLiteCommand.CommandTimeout = 90

                        Using vSQLiteReader As SQLiteDataReader = vSQLiteCommand.ExecuteReader()
                            If vSQLiteReader.HasRows Then
                                Dim dtSchema As DataTable = vSQLiteReader.GetSchemaTable()
                                For Each schemarow As DataRow In dtSchema.Rows
                                    Dim columnName As String = schemarow("ColumnName").ToString()
                                    Dim dataType As Type = DirectCast(schemarow("DataType"), Type)
                                    DT.Columns.Add(columnName, dataType)
                                Next

                                DT.BeginLoadData()
                                While vSQLiteReader.Read()
                                    Dim ColArray As Object() = New Object(vSQLiteReader.FieldCount - 1) {}
                                    For i As Integer = 0 To vSQLiteReader.FieldCount - 1
                                        If Not IsDBNull(vSQLiteReader(i)) Then
                                            ColArray(i) = vSQLiteReader(i)
                                        End If
                                    Next
                                    DT.LoadDataRow(ColArray, True)
                                End While
                                DT.EndLoadData()
                            End If
                        End Using
                    End Using
                End Using

                If Not String.IsNullOrEmpty(IndexCol) AndAlso DT.Columns.Contains(IndexCol) Then
                    DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
                End If

                Exit While

            Catch ex As OutOfMemoryException
                'jam 2026/04/09 Cleaning DT and forcing GC before retrying and spreading
                _logger.Error(ex, $"LoadDatafromSQLite - OutOfMemory leyendo {DBPath}")
                If Not DT Is Nothing Then
                    DT.Clear()
                    DT.Dispose()
                    DT = Nothing
                End If
                GC.Collect(2, GCCollectionMode.Forced, blocking:=True)
                GC.WaitForPendingFinalizers()
                DT = New DataTable
                Throw

            Catch ex As SQLiteException When (ex.ResultCode = SQLiteErrorCode.Locked OrElse ex.ResultCode = SQLiteErrorCode.Busy) AndAlso currentRetry < maxRetries - 1
                currentRetry += 1
                _logger.Warn($"LoadDataTablefromSQLite - Database locked, retry {currentRetry}/{maxRetries}: {DBPath}")
                Threading.Thread.Sleep(retryDelay * currentRetry)

            Catch ex As SQLiteException When ex.ResultCode = SQLiteErrorCode.Locked OrElse ex.ResultCode = SQLiteErrorCode.Busy
                _logger.Error(ex, $"LoadDataTablefromSQLite - Database locked after {maxRetries} retries: {DBPath}")
                DT = Nothing
                Throw

            Catch ex As Exception
                _logger.Error(ex, "SetDataTablefromSQLite - exception")
                DT = Nothing
                Throw
            End Try
        End While
    End Sub
    'jam 2026/04/09 Commented because Out of Memory persists
    'Sub LoadDatafromSQLite(ByVal DBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable, Optional ByVal DatabasePassword As String = "")
    '    _logger.Debug("SetDataTablefromSQLite:" + DBPath + " - '" + strSQL)
    '    DBPath = DBPath.Replace(".mdb", ".db")
    '    DBPath = DBPath.Replace(".MDB", ".db")

    '    'jam 2025/10/30
    '    'SQLite can't handle booleans, instead using 1 as true, 0 as false
    '    strSQL = strSQL.Replace(" = TRUE", " = 1")
    '    strSQL = strSQL.Replace(" = True", " = 1")
    '    strSQL = strSQL.Replace(" = FALSE", " = 0")
    '    strSQL = strSQL.Replace(" = False", " = 0")

    '    'jam 2025/11/30 Optimized connection string
    '    Dim cnnStr As String = "Data Source=" & DBPath & ";" &
    '              "Journal Mode=DELETE;" &
    '              "Pooling=True;" &
    '              "Cache=Shared;" &
    '              "Busy Timeout=90000;"

    '    If Not String.IsNullOrEmpty(DatabasePassword) Then
    '        cnnStr &= "Password=" & DatabasePassword & ";"
    '    End If

    '    DT = New DataTable
    '    Dim maxRetries As Integer = 3
    '    Dim retryDelay As Integer = 300 'ms
    '    Dim currentRetry As Integer = 0

    '    While currentRetry < maxRetries
    '        Try
    '            Using vSQLiteCon As New SQLiteConnection(cnnStr)
    '                vSQLiteCon.Open()

    '                Using pragmaCmd As New SQLiteCommand(vSQLiteCon)
    '                    'jam 2025/10/25 Speeder than default mode (FULL)
    '                    pragmaCmd.CommandText = "PRAGMA synchronous=NORMAL;"
    '                    pragmaCmd.ExecuteNonQuery()

    '                    'jam 2025/10/25 Temporary tables saved in RAM memory
    '                    pragmaCmd.CommandText = "PRAGMA temp_store=MEMORY;"
    '                    pragmaCmd.ExecuteNonQuery()


    '                    'jam 2025/10/25 -50000 = 50MB cache memory (negative = KB, positive = memory pages)
    '                    'jai 2025/10/25  50MB cache memory
    '                    pragmaCmd.CommandText = "PRAGMA cache_size=-50000;"
    '                    pragmaCmd.ExecuteNonQuery()


    '                    'jam 2025/10/25  Reforced connection string timeout
    '                    pragmaCmd.CommandText = "PRAGMA busy_timeout=60000;"
    '                    pragmaCmd.ExecuteNonQuery()

    '                    'jam 2025/10/25 release locks in optimal way
    '                    pragmaCmd.CommandText = "PRAGMA locking_mode=NORMAL;"
    '                    pragmaCmd.ExecuteNonQuery()


    '                End Using

    '                Using vSQLiteCommand As New SQLiteCommand(strSQL, vSQLiteCon)
    '                    vSQLiteCommand.CommandTimeout = 90

    '                    Using vSQLiteReader As SQLiteDataReader = vSQLiteCommand.ExecuteReader()
    '                        If vSQLiteReader.HasRows Then
    '                            'Build schema
    '                            Dim dtSchema As DataTable = vSQLiteReader.GetSchemaTable()
    '                            For Each schemarow As DataRow In dtSchema.Rows
    '                                Dim columnName As String = schemarow("ColumnName").ToString()
    '                                Dim dataType As Type = DirectCast(schemarow("DataType"), Type)
    '                                DT.Columns.Add(columnName, dataType)
    '                            Next

    '                            'jam 2025/11/30 Optimize loading for large datasets
    '                            DT.BeginLoadData()

    '                            While vSQLiteReader.Read()
    '                                Dim ColArray As Object() = New Object(vSQLiteReader.FieldCount - 1) {}
    '                                For i As Integer = 0 To vSQLiteReader.FieldCount - 1
    '                                    If Not IsDBNull(vSQLiteReader(i)) Then
    '                                        ColArray(i) = vSQLiteReader(i)
    '                                    End If
    '                                Next
    '                                DT.LoadDataRow(ColArray, True)
    '                            End While

    '                            DT.EndLoadData()
    '                        End If
    '                    End Using
    '                End Using
    '            End Using

    '                If Not String.IsNullOrEmpty(IndexCol) AndAlso DT.Columns.Contains(IndexCol) Then
    '                DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
    '            End If

    '            'Success - exit retry loop
    '            Exit While

    '        Catch ex As SQLiteException When (ex.ResultCode = SQLiteErrorCode.Locked OrElse ex.ResultCode = SQLiteErrorCode.Busy) AndAlso currentRetry < maxRetries - 1
    '            currentRetry += 1
    '            _logger.Warn($"LoadDataTablefromSQLite - Database locked, retry {currentRetry}/{maxRetries}: {DBPath}")
    '            Threading.Thread.Sleep(retryDelay * currentRetry)

    '        Catch ex As SQLiteException When ex.ResultCode = SQLiteErrorCode.Locked OrElse ex.ResultCode = SQLiteErrorCode.Busy
    '            _logger.Error(ex, $"LoadDataTablefromSQLite - Database locked after {maxRetries} retries: {DBPath}")
    '            DT = Nothing
    '            Throw

    '        Catch ex As Exception
    '            _logger.Error(ex, "SetDataTablefromSQLite - exception")
    '            DT = Nothing
    '            Throw
    '        End Try
    '    End While
    'End Sub

    'Sub LoadDatafromSQLite(ByVal DBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable, Optional ByVal DatabasePassword As String = "")
    '    _logger.Debug("SetDataTablefromSQLite:" + DBPath + " - '" + strSQL)
    '    DBPath = DBPath.Replace(".mdb", ".db")
    '    DBPath = DBPath.Replace(".MDB", ".db")

    '    'jam 2025/10/30
    '    'SQLite can't handle booleans, instead using 1 as true, 0 as false
    '    strSQL = strSQL.Replace(" = TRUE", " = 1")
    '    strSQL = strSQL.Replace(" = True", " = 1")
    '    strSQL = strSQL.Replace(" = FALSE", " = 0")
    '    strSQL = strSQL.Replace(" = False", " = 0")




    '    'jam 2025/10/25 Optimizing connection
    '    Dim cnnStr As String = "Data Source=" & DBPath & ";" &
    '               "Journal Mode=delete;" &
    '               "Pooling=True;" &
    '               "Cache=Shared;" &
    '               "Busy Timeout=60000;"          'jam 2025/10/25 resized to 60s 

    '    If Not String.IsNullOrEmpty(DatabasePassword) Then
    '        cnnStr &= "Password=" & DatabasePassword & ";"
    '    End If



    '    DT = New DataTable

    '    Try
    '        Using vSQLiteCon As New SQLiteConnection(cnnStr)
    '            vSQLiteCon.Open()

    '            Using pragmaCmd As New SQLiteCommand(vSQLiteCon)
    '                'jam 2025/10/25 Speeder than default mode (FULL)
    '                pragmaCmd.CommandText = "PRAGMA synchronous=NORMAL;"
    '                pragmaCmd.ExecuteNonQuery()

    '                'jam 2025/10/25 Temporary tables saved in RAM memory
    '                pragmaCmd.CommandText = "PRAGMA temp_store=MEMORY;"
    '                pragmaCmd.ExecuteNonQuery()


    '                'jam 2025/10/25 -50000 = 50MB cache memory (negative = KB, positive = memory pages)
    '                'jai 2025/10/25  50MB cache memory
    '                pragmaCmd.CommandText = "PRAGMA cache_size=-50000;"
    '                pragmaCmd.ExecuteNonQuery()


    '                'jam 2025/10/25  Reforced connection string timeout
    '                pragmaCmd.CommandText = "PRAGMA busy_timeout=60000;"
    '                pragmaCmd.ExecuteNonQuery()

    '                'jam 2025/10/25 release locks in optimal way
    '                pragmaCmd.CommandText = "PRAGMA locking_mode=NORMAL;"
    '                pragmaCmd.ExecuteNonQuery()


    '            End Using

    '            Using vSQLiteCommand As New SQLiteCommand(strSQL, vSQLiteCon)
    '                'jam 2025/10/25 It must be the same defined for Busy Timeout in Connection opening and PRAGMA
    '                vSQLiteCommand.CommandTimeout = 60

    '                Using vSQLiteReader As SQLiteDataReader = vSQLiteCommand.ExecuteReader()
    '                    If vSQLiteReader.HasRows Then

    '                        Dim dtSchema As DataTable = vSQLiteReader.GetSchemaTable()
    '                        For Each schemarow As DataRow In dtSchema.Rows

    '                            Dim columnName As String = schemarow("ColumnName").ToString()
    '                            Dim dataType As Type = DirectCast(schemarow("DataType"), Type)
    '                            DT.Columns.Add(columnName, dataType)
    '                        Next


    '                        While vSQLiteReader.Read()
    '                            Dim ColArray As Object() = New Object(vSQLiteReader.FieldCount - 1) {}
    '                            For i As Integer = 0 To vSQLiteReader.FieldCount - 1
    '                                If Not IsDBNull(vSQLiteReader(i)) Then
    '                                    ColArray(i) = vSQLiteReader(i)
    '                                End If
    '                            Next
    '                            DT.LoadDataRow(ColArray, True)
    '                        End While
    '                    End If
    '                End Using
    '            End Using
    '        End Using

    '        If Not String.IsNullOrEmpty(IndexCol) AndAlso DT.Columns.Contains(IndexCol) Then
    '            DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
    '        End If

    '    Catch ex As SQLiteException When ex.ResultCode = SQLiteErrorCode.Locked OrElse ex.ResultCode = SQLiteErrorCode.Busy

    '        _logger.Error(ex, $"LoadDataTablefromSQLite - Database locked: {DBPath}")

    '        DT = Nothing
    '        Throw

    '    Catch ex As Exception
    '        _logger.Error(ex, "SetDataTablefromSQLite - exception")
    '        DT = Nothing
    '        Throw
    '    End Try
    'End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''     ''' 
    '''     JAM 2025/05/31 Managing connection with Journal Mode = WAF
    ''' 
    ''' 
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="AccessDBPath">Database path</param>
    ''' <param name="strSQL">Database path</param>
    ''' <param name="IndexCol">Database path</param>
    ''' <param name="DT">Database path</param>
    ''' <param name="DatabasePassword">Database path</param>
    ''' Based on function SetDataTablefromAccess developed by jrs

    Sub SetDataTablefromAccessJournal(ByVal AccessDBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable, Optional ByVal DatabasePassword As String = "") 'mtiwebforms April 2004 ADO.Net
        _logger.Debug("SetDataTablefromAccess: " + AccessDBPath + " - " + strSQL)

        'jrs 2022/07/15
        If UseOnlySQLite Then
            LoadDatafromSQLite(AccessDBPath, strSQL, IndexCol, DT, DatabasePassword)
            Return
        End If

        Dim cnnStr As String
        If DatabasePassword <> "" Then
            'cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
            cnnStr = "Provider=Microsoft.ACE.OLEDB.16.0;" &
               "Data Source=" & AccessDBPath &
               ";Jet OLEDB:Database Password=" & DatabasePassword
        Else
            'cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" &
            cnnStr = "Provider=Microsoft.ACE.OLEDB.16.0;" &
               "Data Source=" & AccessDBPath
        End If

        Dim objAdapter As New OleDb.OleDbDataAdapter(Replace(strSQL, "UPPER(", "UCASE("), cnnStr)

        'mti August 2004 5 -start
        'Dim objDataSet As New DataSet

        '' Copy the selected data into a dataset.
        'objAdapter.Fill(objDataSet)
        'DT = objDataSet.Tables(0)


        'Dim objDataSet As New DataSet

        '' Copy the selected data into a dataset.
        'mti August 2004 5
        DT = New DataTable
        'jrs 11/11/04
        On Error GoTo AccessHandler
        Dim rc As Integer = objAdapter.Fill(DT)
        'DT = objDataSet.Tables(0)
        'mti August 2004 5 end

        If IndexCol <> "" Then
            'DT.PrimaryKey = New DataColumn() {DT.Columns(0)}
            'Else
            DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
        End If

        'dt.Rows(0).Item(resultfieldname))
        'jrs 11/11/04
        Return
AccessHandler:
        Dim str As String
        _logger.Error(Err.GetException, "SetDataTablefromAccess - exception")
        str = Err.Description
        DT = Nothing
        Return
    End Sub


End Module

'End Namespace
