Imports System.IO
'jrs 2022/08/24
Imports Microsoft.Data.Sqlite
'jrs 2022/10/18
Imports System.Data
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Runtime.CompilerServices
Imports IFs.Data
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports System.Runtime.InteropServices.JavaScript.JSType
Imports System.Globalization
Imports System.Text
Imports System.Reflection.Emit
Imports IFs.Data.Migrations
Imports Microsoft.SqlServer
Imports System.Configuration

Public Module IFsFunctions

    Public StartDirec As String, PutFdrvRun As String, PutDataFileDir As String
    Public BaseYear As Integer
    'jrs 2024/06/18
    Public ConstantDollarsYear As Integer
    Public PastConstantDollarsYears As List(Of Integer)

    Public GRPS(,) As String
    Public RegCoeffs As List(Of String)
    'jrs 2022/10/18
    Public ModelLoad As String

    'mti October - December 2009 -start
    'mti February 28, 2012 - start
    'Dim LeftSide(9000) As String 'Left side of an eqn
    'Dim Rightside(9000) As String 'Right Side
    'Dim CodeBlock(9000) 'blocks obtained by parsing modules
    'Dim ModuleName(9000) 'keeps track of module name for each block

    'jrs 2023/01/03 moved from 15k to 20k

    'mti 2025/07/14 start
    'Dim LeftSide(20000) As String 'Left side of an eqn
    'Dim Rightside(20000) As String 'Right Side
    'Dim CodeBlock(20000) As String 'blocks obtained by parsing modules
    'Dim ModuleName(20000) As String 'keeps track of module name for each block

    'mti 2025/08/07 start
    'Dim LeftSide(30000) As String 'Left side of an eqn
    'Dim Rightside(30000) As String 'Right Side
    'Dim CodeBlock(30000) As String 'blocks obtained by parsing modules
    'Dim ModuleName(30000) As String 'keeps track of module name for each block

    Dim LeftSide(40000) As String 'Left side of an eqn
    Dim Rightside(40000) As String 'Right Side
    Dim CodeBlock(40000) As String 'blocks obtained by parsing modules
    Dim ModuleName(40000) As String 'keeps track of module name for each block


    Dim CommentText(40000) As String 'keeps track of function nameetc
    'mti 2025/08/07 end
    'mti 2025/07/14 end

    'mti February 28, 2012 - end

    Dim CodeArray(3000) As String
    'this is the most important array saving relevant codeblock() pointers
    'this is sorted while being taken, so that vbcode is in proper order
    Dim CodeArray2Block(3000) As Integer  'this savesline numbers of code where the driversof a variable is found   
    Dim VCLn%, UVCLn% 'VCLN (UVCLn) keeps track of codearray() upper limit  'mti 2025/07/14
    Dim ModName(20) As String, ModStart(20), ModEnd(20) As Integer
    'As the equations for a IFSVar variable(in the Left Hand Side) is sought
    'CodeArray(1), CodeArray(2) etc. are filled with Codeblock() populated from ParseCodeFile
    'VCLn is the upper limit of CodeArray when the search ends
    'Driver Code Changes June 2003 - End

    Dim codelines As Integer
    Dim vbKW() As String    ' vb Key Words
    Public vbKWCount As Integer
    Dim Tempright(3000, 1) As String

    'mti October - December 2009 -end

    'mti 2025/10/30 start
    Dim DimNamesExcluded() As String
    'mti 2025/10/30 end

    'mti 2025/12/08 start
    Dim VarNamesExcluded() As String
    'mti 2025/12/08 end

    Dim NumbVars As Integer
    Dim V() As String
    'jrs 2023/07/26

    'mti 2025/08/07 Start dimensions
    Dim VDim1() As Integer, VDim2() As Integer, VDim3() As Integer
    'mti 2025/08/07 end

    Dim VNames As List(Of String)

    Friend Sub FileCopyCheck(InFile$, OutFile$)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'System.IO.File.co(context.DataFileDir$ + "IFsForDyadic.mdb")
        Dim FDT1 As Date, FDT2 As Date

        Try
            FDT1 = System.IO.File.GetCreationTime(OutFile$)
            FDT2 = System.IO.File.GetCreationTime(InFile$)

            If FDT1 = FDT2 Then
                Exit Sub
            End If
        Catch ex As Exception
        Finally
            System.IO.File.Copy(InFile$, OutFile$, True)
        End Try

    End Sub

    Friend Sub FileCopy(InFile$, OutFile$)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'System.IO.File.co(context.DataFileDir$ + "IFsForDyadic.mdb")
        Dim FDT1 As Date, FDT2 As Date

        Try
            FDT1 = System.IO.File.GetCreationTime(OutFile$)
            FDT2 = System.IO.File.GetCreationTime(InFile$)

        Catch ex As Exception
        Finally
            System.IO.File.Copy(InFile$, OutFile$, True)
        End Try

    End Sub

    'This function returns true if it finds the file, false if it doesn't
    Private Function myFileCheck(sFileName As String) As Boolean
        myFileCheck = System.IO.File.Exists(sFileName)
    End Function

    'This function checks if the file is not there and then copies it
    Private Sub FileCopy_NotThere(ByRef InFile$, ByRef OutFile$)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2026/03/24
        'this function is not really working
        If Not System.IO.File.Exists(OutFile$) Then
            If System.IO.File.Exists(InFile$) Then
                FileCopy(InFile$, OutFile$)
            Else
                Dim sLine As String = "File " + InFile$ + " doesn't exist, procceding without it."
                Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine(sLine)
                objWriter.Close()

                OutFile$ = ""
            End If
        End If
        'Try
        '    Dim FDT1 = System.IO.File.GetCreationTime(OutFile$)
        'Catch ex As Exception
        '    If System.IO.File.Exists(InFile$) Then
        '        FileCopy(InFile$, OutFile$)
        '    Else
        '        Dim sLine As String = "File " + InFile$ + " doesn't exist, procceding without it."
        '        Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
        '        objWriter.WriteLine(sLine)
        '        objWriter.Close()

        '        OutFile$ = ""
        '    End If
        'End Try

    End Sub
    'jrs 2007/08/06 made sub public
    Public Sub ReadSceFiles(ByVal sDir As String, ByRef FilesSelected As Integer, ByRef fileList() As String)
        If sDir.Substring(sDir.Length - 2) = "\" Then
            sDir = sDir.Substring(0, sDir.Length - 1)
        End If

        Dim sRunFile() As String
        ' Returns filename with specified extension. If more than one *.ini
        ' file exists, the first file found is returned.
        sRunFile = System.IO.Directory.GetFiles(sDir, "*.sce")
        Dim RunFileIdx As Integer = 0

        Do While (RunFileIdx < sRunFile.Length)
            If sRunFile(RunFileIdx).ToUpper <> "WORKING.SCE" And sRunFile(RunFileIdx).ToUpper <> "WORKING FILE.SCE" And sRunFile(RunFileIdx).ToUpper <> "IFSBASE.SCE" Then
                FilesSelected = FilesSelected + 1
                ReDim Preserve fileList(FilesSelected)
                fileList(FilesSelected) = sRunFile(RunFileIdx)
            End If
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            RunFileIdx = RunFileIdx + 1
        Loop
        'Look for all folders within
        Dim counter As Integer
        counter = -1
        Dim DirList() As String

        sRunFile = System.IO.Directory.GetDirectories(sDir)
        RunFileIdx = 0
        Do While (RunFileIdx < sRunFile.Length)
            If Not sRunFile(RunFileIdx).Contains("Session") And sRunFile(RunFileIdx) <> "." And sRunFile(RunFileIdx) <> ".." Then
                'If (GetAttr(sDir & "\" & sRunFile) And vbDirectory) = vbDirectory Then
                counter = counter + 1
                ReDim Preserve DirList(counter)
                DirList(counter) = sRunFile(RunFileIdx)
                'End If
            End If
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            RunFileIdx = RunFileIdx + 1
        Loop

        Dim i As Integer
        For i = 0 To counter
            ReadSceFiles(DirList(i), FilesSelected, fileList)
        Next i
    End Sub

    'jrs 2007/01/15
    Private Sub SearchTablesinFile(sFile As String, FileDeclarations As List(Of String), StartDirec As String, PutFdrvRun As String)
        'Build path

        Dim fileOut As System.IO.StreamWriter
        fileOut = New System.IO.StreamWriter(StartDirec & "docs\" & sFile & ".txt", False)
        fileOut.WriteLine("List of Tables not found:")
        fileOut.WriteLine("=========================")
        fileOut.Close()

        fileOut = New System.IO.StreamWriter(StartDirec & "docs\" & sFile & ".txt", True)

        Dim vSQLiteCon As SqliteConnection
        vSQLiteCon = New SqliteConnection("DataSource=" & PutFdrvRun$ & "Datadict.db;") '& "New=true;"
        vSQLiteCon.Open()

        Dim vSQLiteCommand As SqliteCommand
        vSQLiteCommand = vSQLiteCon.CreateCommand()

        Dim sLine As String, sTbl As String
        Dim xLine As String()
        Dim i As Integer

        'Add full path to declaration files
        For i = 0 To FileDeclarations.Count - 1
            FileDeclarations(i) = StartDirec & "src\IFs.Data\Databases\" & FileDeclarations(i)
        Next

        'Open File
        Using reader As StreamReader = New StreamReader(StartDirec & "src\IFs.Core\Models\" & sFile)
            ' Read one line from file
            sLine = reader.ReadLine
            'Array to look back
            Dim sLinesPast As List(Of String) = New List(Of String)
            'jrs 2026/05/07 just to keep track of aggregators
            Dim sLinesPastAgg As List(Of String) = New List(Of String)
            'Loop through file
            Do While Not reader.EndOfStream
                'jrs 2026/05/07
                'search for aggregators
                If sLine.Contains("AggregatorType:=GetType") Then
                    sLinesPastAgg.Add(sLine)
                End If
                'search for OpenRecordset
                'jrs 2012/02/17 include comments with '* there are some special tables in DATAENV
                'jrs 2012/01/18 try to ignore commented lines
                If (sLine.Contains(".GetSlice") Or sLine.Contains(".GetDice")) AndAlso
                (sLine.Trim.Substring(0, 1) <> "'" Or (sLine.IndexOf("'") > sLine.IndexOf(".GetSlice") And sLine.Contains(".GetSlice")) _
                Or (sLine.IndexOf("'") > sLine.IndexOf(".GetDice") And sLine.Contains(".GetDice")) Or sLine.Trim.Substring(0, 2) = "'*") Then
                    If sLine.Contains("(""") Then
                        'split by string using " as divider
                        xLine = sLine.Split(CChar(""""))
                        sLinesPast.Clear()
                    Else
                        'Find variable
                        Dim sVar As String
                        Dim pos As Integer
                        If sLine.Contains(".GetSlice") Then
                            pos = sLine.IndexOf(".GetSlice") + 10
                        ElseIf sLine.Contains(".GetDice") Then
                            pos = sLine.IndexOf(".GetDice") + 9
                        End If
                        sVar = sLine.Substring(pos)
                        pos = sVar.IndexOf(",")
                        sVar = sVar.Substring(0, pos)
                        'need to go back in the file to look for variable declaration
                        xLine = {}
                        ' gvdb 2024/04/05
                        ' unused variable
                        'Dim sSeries As String
                        For i = sLinesPast.Count - 1 To 0 Step -1
                            If sLinesPast(i).Contains(sVar & " = ") Or sLinesPast(i).Contains(sVar & " As String = ") Or
                                sLinesPast(i).Contains(sVar & "$ = ") Then
                                xLine = sLinesPast(i).Split(CChar(""""))
                                xLine(0) = ".GetSlice"
                                sLinesPast.Clear()
                                Exit For
                            ElseIf sLinesPast(i).Contains("Class ") And sLinesPast(i).Contains("Aggregator") Then 'jrs 2026/05/07
                                'search which variables are using this aggregator
                                Dim sAgg As String = sLinesPast(i)
                                sAgg = sAgg.Replace("Class ", "")
                                sAgg = sAgg.Trim
                                For j = 0 To sLinesPastAgg.Count-1
                                    If sLinesPastAgg(j).Contains("AggregatorType:=GetType(" & sAgg) Then
                                        xLine = sLinesPastAgg(j).Split(CChar(""""))
                                        sTbl = xLine(1)
                                        sTbl = FindFullTableName(FileDeclarations, 0, sTbl)
                                        If sTbl > "" Then
                                            'jrs 2013/10/23 added UCASE
                                            'update it in data dict
                                            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInPreprocessor = 1 WHERE UPPER([Table]) = '" & sTbl.ToUpper & "'"
                                            Dim recordAffectd As Integer = vSQLiteCommand.ExecuteNonQuery()

                                            'check for cases that don't work
                                            If recordAffectd = 0 Then
                                                fileOut.WriteLine(sTbl)
                                            End If

                                            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInPreprocessorFileName = UsedInPreprocessorFileName || "", " & sFile.ToUpper.Replace(".REBUILD.VB", "") & """ WHERE UPPER([Table]) = '" & sTbl.ToUpper & "' AND NOT (UsedInPreprocessorFileName is Null) AND NOT [UsedInPreprocessorFileName] LIKE ""%" & sFile.ToUpper.Replace(".REBUILD.VB", "") & "%"""
                                            vSQLiteCommand.ExecuteNonQuery()
                                            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInPreprocessorFileName = """ & sFile.ToUpper.Replace(".REBUILD.VB", "") & """ WHERE UPPER([Table]) = '" & sTbl.ToUpper & "' AND UsedInPreprocessorFileName is Null"
                                            vSQLiteCommand.ExecuteNonQuery()
                                        End If
                                    End If
                                Next
                                xLine = {}
                                sLinesPast.Clear()
                                Exit For
                            End If
                        Next
                    End If
                    'build table name
                    sTbl = ""
                    For i = 0 To xLine.Length - 1
                        If xLine(i).Contains(".GetSlice") Or xLine(i).Contains(".GetDice") Then
                            sTbl = xLine(i + 1)
                            Exit For
                        End If
                    Next i
                    If sTbl = "" Then
                        If sLine.Contains("""") Then
                            'split by string using ( as divider
                            xLine = sLine.Split(CChar("("))
                        Else
                            'need to go back in the file to look for variable declaration
                            xLine = {}
                        End If
                        'build table name
                        For i = 0 To xLine.Length - 1
                            If xLine(i).Contains(".GetSlice") Or xLine(i).Contains(".GetDice") Then
                                sTbl = xLine(i + 1).Substring(0, xLine(i + 1).IndexOf(")"))
                                If sTbl = "" Then
                                    Console.WriteLine("Table not found:" & sTbl)
                                End If
                                fileOut.WriteLine("Variable: " & sTbl)
                                'jrs 2009/02/20 Stop
                                Exit For
                            End If
                        Next i
                    Else
                        'If sTbl = "AGEOFMARRIAGE_Female" Then Stop
                        'jrs 2022/08/23 Find Full Table Name in declaration File
                        sTbl = FindFullTableName(FileDeclarations, 0, sTbl)
                        If sTbl > "" Then
                            'jrs 2013/10/23 added UCASE
                            'update it in data dict
                            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInPreprocessor = 1 WHERE UPPER([Table]) = '" & sTbl.ToUpper & "'"
                            Dim recordAffectd As Integer = vSQLiteCommand.ExecuteNonQuery()

                            'check for cases that don't work
                            If recordAffectd = 0 Then
                                fileOut.WriteLine(sTbl)
                            End If

                            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInPreprocessorFileName = UsedInPreprocessorFileName || "", " & sFile.ToUpper.Replace(".REBUILD.VB", "") & """ WHERE UPPER([Table]) = '" & sTbl.ToUpper & "' AND NOT (UsedInPreprocessorFileName is Null) AND NOT [UsedInPreprocessorFileName] LIKE ""%" & sFile.ToUpper.Replace(".REBUILD.VB", "") & "%"""
                            vSQLiteCommand.ExecuteNonQuery()
                            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInPreprocessorFileName = """ & sFile.ToUpper.Replace(".REBUILD.VB", "") & """ WHERE UPPER([Table]) = '" & sTbl.ToUpper & "' AND UsedInPreprocessorFileName is Null"
                            vSQLiteCommand.ExecuteNonQuery()
                        End If
                    End If
                Else
                    'if it's not a line to check add it to the list of past lists
                    sLinesPast.Add(sLine)
                End If

                ' Read one line from file
                sLine = reader.ReadLine
                'If sLine.Contains("CONTRUSE = ") Then Stop
            Loop

        End Using
        'Close File
        fileOut.Close()
        vSQLiteCon.Close()
    End Sub

    Private Function FindFullTableName(FileDeclarations As List(Of String), Listidx As Integer, sTbl As String) As String
        FindFullTableName = sTbl

        Using reader As StreamReader = New StreamReader(FileDeclarations(Listidx))
            ' Read entire file
            Dim sAllTxt As String
            sAllTxt = reader.ReadToEnd
            'Search for original table
            Dim iPosEnd As Integer
            iPosEnd = sAllTxt.IndexOf("> " & sTbl & " { ")

            'It's possible declaration is in another file
            If iPosEnd > 0 Then
                Dim iPosStart As Integer
                For i = iPosEnd To 0 Step -1
                    If sAllTxt.Substring(i, 1) = "<" Then
                        iPosStart = i
                        Exit For
                    End If
                Next
                Dim sTbl2 As String
                sTbl2 = sAllTxt.Substring(iPosStart + 1, iPosEnd - iPosStart - 1)

                Dim iPosStart2, iPosEnd2 As Integer
                iPosEnd2 = sAllTxt.IndexOf("internal partial class " & sTbl2 & " : Series") - 10
                'It's possible the table is declared in a 2nd file, and it only has a reference here
                'like the case of AIDSDTHS
                If iPosEnd2 > 0 Then
                    For i = iPosEnd2 To 0 Step -1
                        If sAllTxt.Substring(i, 1) = """" Then
                            iPosStart2 = i
                            Exit For
                        End If
                    Next

                    Dim sTbl3 As String
                    sTbl3 = sAllTxt.Substring(iPosStart2 + 1, iPosEnd2 - iPosStart2)
                    'if sTbl3 = ")" there could be comments in declaration
                    If sTbl3 = ")" Then
                        iPosEnd2 = iPosEnd2 - 2
                        For i = iPosEnd2 To 0 Step -1
                            If sAllTxt.Substring(i, 1) = """" Then
                                iPosStart2 = i
                                sTbl3 = sAllTxt.Substring(iPosStart2 + 1, iPosEnd2 - iPosStart2)
                                Exit For
                            End If
                        Next
                    End If

                    FindFullTableName = sTbl3
                Else
                    FindFullTableName = sTbl2
                End If

            Else
                'Advance to next list
                Listidx = Listidx + 1
                'check a 2nd file
                If FileDeclarations.Count > Listidx Then
                    FindFullTableName = FindFullTableName(FileDeclarations, Listidx, sTbl)
                Else
                    FindFullTableName = sTbl
                End If
            End If

        End Using
    End Function

    'jrs 2022/08/25
    Public Sub SearchRebuildFiles()
        If StartDirec.Contains("Code.Ifs-Translation") Then
            'Initialize all tables
            Dim vSQLiteCon As SqliteConnection
            vSQLiteCon = New SqliteConnection("DataSource=" & PutFdrvRun$ & "Datadict.db;") '& "New=true;"
            vSQLiteCon.Open()

            Dim vSQLiteCommand As SqliteCommand
            vSQLiteCommand = vSQLiteCon.CreateCommand()

            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInPreprocessor = 0, UsedInPreprocessorFileName = Null"
            vSQLiteCommand.ExecuteNonQuery()
            vSQLiteCon.Close()

            Console.WriteLine("")
            Console.WriteLine("Looking for Series in:")
            Console.WriteLine("---------------------")
            Console.WriteLine("Agriculture")
            SearchTablesinFile("Agriculture.Rebuild.vb", {"IFsHistSeriesEntitiesAGRI.cs", "IFsHistSeriesEntities.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Automobile")
            SearchTablesinFile("Automobile.Rebuild.vb", {"IFsHistSeriesEntitiesAUT.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Economy")
            SearchTablesinFile("Economy.Rebuild.vb", {"IFsHistSeriesEntitiesECO.cs", "IFsHistSeriesEntities.cs", "IFsHistSeriesEntitiesGOVCON.cs", "IFsHistSeriesEntitiesINFRA.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Education")
            SearchTablesinFile("Education.Rebuild.vb", {"IFsHistSeriesEntitiesEDU.cs", "IFsHistSeriesEntities.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Energy")
            SearchTablesinFile("Energy.Rebuild.vb", {"IFsHistSeriesEntitiesENE.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Environment")
            SearchTablesinFile("Environment.Rebuild.vb", {"IFsHistSeriesEntitiesENV.cs", "IFsHistSeriesEntities.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Health")
            SearchTablesinFile("Health.Rebuild.vb", {"IFsHistSeriesEntitiesHEA.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Infrastructure")
            SearchTablesinFile("Infrastructure.Rebuild.vb", {"IFsHistSeriesEntitiesINFRA.cs", "IFsHistSeriesEntities.cs", "IFsHistSeriesEntitiesECO.cs", "IFsHistSeriesEntitiesAGRI.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("IntlConf")
            SearchTablesinFile("IntlConf.Rebuild.vb", {"IFsHistSeriesEntitiesINTCNF.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Population")
            SearchTablesinFile("Population.Rebuild.vb", {"IFsHistSeriesEntities.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("SocioPol")
            SearchTablesinFile("SocioPol.Rebuild.vb", {"IFsHistSeriesEntitiesSOCPOL.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("StFailure")
            SearchTablesinFile("StFailure.Rebuild.vb", {"IFsHistSeriesEntitiesSTFL.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Technology")
            SearchTablesinFile("Technology.Rebuild.vb", {"IFsHistSeriesEntitiesTEC.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("ValuesData")
            SearchTablesinFile("ValuesData.Rebuild.vb", {"IFsHistSeriesEntitiesVALDAT.cs"}.ToList(), StartDirec, PutFdrvRun)
            Console.WriteLine("Water")
            SearchTablesinFile("Water.Rebuild.vb", {"IFsHistSeriesEntitiesWAT.cs"}.ToList(), StartDirec, PutFdrvRun)

            'If MsgBox("Please copy DataDict.mdb from RUNFILES to DATA. Do you want the system to do it for you?", vbYesNo, "Mark historical tables") = vbYes Then
            FileCopy(PutFdrvRun$ & "Datadict.db", PutDataFileDir & "Datadict.db")
            Console.WriteLine("Process completed")
            Console.WriteLine("")
            'End If
        End If
    End Sub

    'jrs 2024/08/29
    Public Sub MarkVarsUsedAsHistAnalog()
        'Initialize all tables
        Dim vSQLiteCon As SqliteConnection
        vSQLiteCon = New SqliteConnection("DataSource=" & PutFdrvRun$ & "Datadict.db;") '& "New=true;"
        vSQLiteCon.Open()

        Dim ifsVarConn As SqliteConnection
        ifsVarConn = New SqliteConnection("DataSource=" & PutFdrvRun$ & "IFsVar.db;") '& "New=true;"
        ifsVarConn.Open()

        Dim vSQLiteCommand As SqliteCommand
        vSQLiteCommand = vSQLiteCon.CreateCommand()

        vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInHistAnalog = 0"
        vSQLiteCommand.ExecuteNonQuery()

        Console.WriteLine("")
        Console.WriteLine("Marking Historic Analogs")

        'Open IFsVar and mark 4 types
        '----------------------------
        Dim vSQLiteCommandRdr As SqliteCommand, vSQLiteCommandRdrForm As SqliteCommand, vSQLiteCommandRdrTbl As SqliteCommand
        Dim vSQLiteReader As SqliteDataReader, vSQLiteReaderForm As SqliteDataReader, vSQLiteReaderTbl As SqliteDataReader
        Dim sVarName As String

        vSQLiteCommandRdr = ifsVarConn.CreateCommand()
        'jrs 2025/05/05 added Aggregation
        Dim vSQLiteCommandRdr2 As SqliteCommand, vSQLiteReader2 As SqliteDataReader
        vSQLiteCommandRdr2 = vSQLiteCon.CreateCommand()

        vSQLiteCommandRdr.CommandText = "SELECT [Historic Analog], [Name], AGGREGATION FROM IFsVar WHERE [Historic Analog] > ''"
        vSQLiteReader = vSQLiteCommandRdr.ExecuteReader()
        Do While vSQLiteReader.Read()
            sVarName = vSQLiteReader(0).ToString
            Select Case sVarName
                Case "FORMULA" 'Type 4 is Formulas
                    'Read content of Formulas
                    vSQLiteCommandRdrForm = ifsVarConn.CreateCommand
                    vSQLiteCommandRdrForm.CommandText = "SELECT [VarName] FROM IFSVARHISTANAFORMULASDET WHERE ForVarName = '" & vSQLiteReader(1).ToString & "'"
                    vSQLiteReaderForm = vSQLiteCommandRdrForm.ExecuteReader
                    Do While vSQLiteReaderForm.Read()
                        vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInHistAnalog = 1 WHERE upper([Variable]) = '" & vSQLiteReaderForm(0).ToString.ToUpper & "'"
                        vSQLiteCommand.ExecuteNonQuery()
                    Loop
                    vSQLiteReaderForm.Close()
                Case "IFSVARHISTANA" 'Type 3 is variables saved in historic analog table
                    'Read from HISTANA table
                    vSQLiteCommandRdrTbl = ifsVarConn.CreateCommand
                    vSQLiteCommandRdrTbl.CommandText = "SELECT [Historic Analog] FROM IFSVARHISTANA WHERE VarName = '" & vSQLiteReader(1).ToString & "'"
                    vSQLiteReaderTbl = vSQLiteCommandRdrTbl.ExecuteReader
                    Do While vSQLiteReaderTbl.Read()
                        vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInHistAnalog = 1 WHERE upper([Variable]) = '" & vSQLiteReaderTbl(0).ToString.ToUpper & "'"
                        vSQLiteCommand.ExecuteNonQuery()
                        'jrs 2025/05/05
                        'vSQLiteCommandRdr2.CommandText = "SELECT Aggregation FROM DataDict WHERE upper([Variable]) = '" & vSQLiteReaderTbl(0).ToString.ToUpper & "'"
                        'vSQLiteReader2 = vSQLiteCommandRdr2.ExecuteReader()
                        'vSQLiteReader2.Read()
                        'If vSQLiteReader2.HasRows Then
                        '    If vSQLiteReader2(0).ToString.ToUpper <> vSQLiteReader(2).ToString.ToUpper Then
                        '        Console.WriteLine(vSQLiteReader(1).ToString & "(" & vSQLiteReader(2).ToString & "), " & vSQLiteReaderTbl(0).ToString & "(" & vSQLiteReader2(0).ToString & ")")
                        '    End If
                        'Else
                        '    Console.WriteLine(vSQLiteReader(1).ToString & "(" & vSQLiteReader(2).ToString & "), " & vSQLiteReaderTbl(0).ToString & "(DOESN'T EXIST)")
                        'End If
                        'vSQLiteReader2.Close()
                    Loop
                    vSQLiteReaderTbl.Close()
                Case Else
                    If sVarName.IndexOf(",") > 0 Then
                        'Type 2, comma separated variables
                        Dim x = sVarName.Split(",")
                        For i = 0 To x.Length - 1
                            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInHistAnalog = 1 WHERE upper([Variable]) = '" & x(i).Trim.ToUpper & "'"
                            vSQLiteCommand.ExecuteNonQuery()
                            'jrs 2025/05/05
                            'vSQLiteCommandRdr2.CommandText = "SELECT Aggregation FROM DataDict WHERE upper([Variable]) = '" & x(i).Trim.ToUpper & "'"
                            'vSQLiteReader2 = vSQLiteCommandRdr2.ExecuteReader()
                            'vSQLiteReader2.Read()
                            'If vSQLiteReader2(0).ToString.ToUpper <> vSQLiteReader(2).ToString.ToUpper Then
                            '    Console.WriteLine(vSQLiteReader(1).ToString & "(" & vSQLiteReader(2).ToString & "), " & x(i).ToString & "(" & vSQLiteReader2(0).ToString & ")")
                            'End If
                            'vSQLiteReader2.Close()
                        Next
                    Else
                        'Type 1, straight variable
                        vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInHistAnalog = 1 WHERE upper([Variable]) = '" & sVarName.Trim.ToUpper & "'"
                        vSQLiteCommand.ExecuteNonQuery()
                        'jrs 2025/05/05
                        'vSQLiteCommandRdr2.CommandText = "SELECT Aggregation FROM DataDict WHERE upper([Variable]) = '" & sVarName.Trim.ToUpper & "'"
                        'vSQLiteReader2 = vSQLiteCommandRdr2.ExecuteReader()
                        'vSQLiteReader2.Read()
                        'If vSQLiteReader2.HasRows Then
                        '    If vSQLiteReader2(0).ToString.ToUpper <> vSQLiteReader(2).ToString.ToUpper Then
                        '        Console.WriteLine(vSQLiteReader(1).ToString & "(" & vSQLiteReader(2).ToString & "), " & sVarName & "(" & vSQLiteReader2(0).ToString & ")")
                        '    End If
                        'Else
                        '    Console.WriteLine(vSQLiteReader(1).ToString & "(" & vSQLiteReader(2).ToString & "), " & sVarName & "(DOESN'T EXIST)")
                        'End If
                        'vSQLiteReader2.Close()
                    End If
            End Select
        Loop
        vSQLiteReader.Close()
        ifsVarConn.Close()
        vSQLiteCon.Close()

        FileCopy(PutFdrvRun$ & "Datadict.db", PutDataFileDir & "Datadict.db")
        Console.WriteLine("Mark Historic Analogs Process completed")
        Console.WriteLine("")

    End Sub

    'jrs 2024/08/29
    Public Sub MarkVarsUsedInFx()
        'Initialize all tables
        Dim vSQLiteCon As SqliteConnection
        vSQLiteCon = New SqliteConnection("DataSource=" & PutFdrvRun$ & "Datadict.db;") '& "New=true;"
        vSQLiteCon.Open()

        Dim TblFuncConn As SqliteConnection
        TblFuncConn = New SqliteConnection("DataSource=" & PutFdrvRun$ & "TablFunc.db;") '& "New=true;"
        TblFuncConn.Open()

        Dim vSQLiteCommand As SqliteCommand
        vSQLiteCommand = vSQLiteCon.CreateCommand()

        vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInFunctions = 0"
        vSQLiteCommand.ExecuteNonQuery()

        Console.WriteLine("")
        Console.WriteLine("Marking Data Variables Used in Functions")

        'Open TablFunc and mark 
        '----------------------------
        Dim vSQLiteCommandRdr As SqliteCommand
        Dim vSQLiteReader As SqliteDataReader
        Dim sVarName As String
        Dim pos As Integer

        vSQLiteCommandRdr = TblFuncConn.CreateCommand()
        vSQLiteCommandRdr.CommandText = "SELECT XVariable, YVariable FROM Tablfunc WHERE UsedRun = 'Yes' OR UsedHisRun = 'Yes' OR UsedPre = 'Yes' OR UsedHisPre = 'Yes'"
        vSQLiteReader = vSQLiteCommandRdr.ExecuteReader()
        Do While vSQLiteReader.Read()
            'XVar
            sVarName = vSQLiteReader(0).ToString
            'Remove Year
            pos = sVarName.IndexOf("(")
            If pos > 0 Then
                sVarName = sVarName.Substring(0, pos)
            End If
            'Remove Division
            pos = sVarName.IndexOf("/")
            If pos > 0 Then
                sVarName = sVarName.Substring(0, pos)
            End If

            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInFunctions = 1 WHERE upper([Variable]) = '" & sVarName.Trim.ToUpper & "'"
            vSQLiteCommand.ExecuteNonQuery()

            'YVar
            sVarName = vSQLiteReader(1).ToString
            'Remove Year
            pos = sVarName.IndexOf("(")
            If pos > 0 Then
                sVarName = sVarName.Substring(0, pos)
            End If
            'Remove Division
            pos = sVarName.IndexOf("/")
            If pos > 0 Then
                sVarName = sVarName.Substring(0, pos)
            End If

            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInFunctions = 1 WHERE upper([Variable]) = '" & sVarName.Trim.ToUpper & "'"
            vSQLiteCommand.ExecuteNonQuery()
        Loop
        vSQLiteReader.Close()

        Dim AnalFuncConn As SqliteConnection
        AnalFuncConn = New SqliteConnection("DataSource=" & PutFdrvRun$ & "AnalFunc.db;") '& "New=true;"
        AnalFuncConn.Open()

        'Open AnalFunc and mark 
        '----------------------------
        vSQLiteCommandRdr = AnalFuncConn.CreateCommand()
        vSQLiteCommandRdr.CommandText = "SELECT XVariable, YVariable FROM AnalFunc WHERE UsedRun = 'Yes' OR UsedHisRun = 'Yes' OR UsedPre = 'Yes' OR UsedHisPre = 'Yes'"
        vSQLiteReader = vSQLiteCommandRdr.ExecuteReader()
        Do While vSQLiteReader.Read()
            'XVar
            sVarName = vSQLiteReader(0).ToString
            'Remove Year
            pos = sVarName.IndexOf("(")
            If pos > 0 Then
                sVarName = sVarName.Substring(0, pos)
            End If
            'Remove Division
            pos = sVarName.IndexOf("/")
            If pos > 0 Then
                sVarName = sVarName.Substring(0, pos)
            End If

            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInFunctions = 1 WHERE upper([Variable]) = '" & sVarName.Trim.ToUpper & "'"
            vSQLiteCommand.ExecuteNonQuery()

            'YVar
            sVarName = vSQLiteReader(1).ToString
            'Remove Year
            pos = sVarName.IndexOf("(")
            If pos > 0 Then
                sVarName = sVarName.Substring(0, pos)
            End If
            'Remove Division
            pos = sVarName.IndexOf("/")
            If pos > 0 Then
                sVarName = sVarName.Substring(0, pos)
            End If

            vSQLiteCommand.CommandText = "UPDATE DataDict SET UsedInFunctions = 1 WHERE upper([Variable]) = '" & sVarName.Trim.ToUpper & "'"
            vSQLiteCommand.ExecuteNonQuery()
        Loop
        vSQLiteReader.Close()

        AnalFuncConn.Close()

        vSQLiteCon.Close()

        FileCopy(PutFdrvRun$ & "Datadict.db", PutDataFileDir & "Datadict.db")
        Console.WriteLine("Mark Historic Variables Used in Functions Process completed")
        Console.WriteLine("")

    End Sub

    'jrs 2013/11/13
    Private Sub UpdateFuncFile(sFile As String, bOpenHis As Boolean, bOpenForecast As Boolean, db As SqliteConnection,
                               sFxName As String, fN2 As System.IO.StreamWriter)
        Dim sTableName As String
        If db.ConnectionString.Contains("AnalFunc.db") Then
            sTableName = "AnalFunc"
        Else
            sTableName = "TablFunc"
        End If

        Dim vSQLiteCommand As SqliteCommand
        vSQLiteCommand = db.CreateCommand()

        'update in TableFunc
        If sFile.Contains(".Rebuild") Then
            If Not bOpenHis And Not bOpenForecast Then
                vSQLiteCommand.CommandText = "UPDATE " & sTableName & " SET UsedHisPre = ""Yes"", UsedPre = ""Yes"" WHERE UPPER([Function Name]) = '" & sFxName.ToUpper & "'"
            Else
                If bOpenHis Then
                    vSQLiteCommand.CommandText = "UPDATE " & sTableName & " SET UsedHisPre = ""Yes"" WHERE UPPER([Function Name]) = '" & sFxName.ToUpper & "'"
                Else
                    vSQLiteCommand.CommandText = "UPDATE " & sTableName & " SET UsedPre = ""Yes"" WHERE UPPER([Function Name]) = '" & sFxName.ToUpper & "'"
                End If
            End If
        Else
            If Not bOpenHis And Not bOpenForecast Then
                vSQLiteCommand.CommandText = "UPDATE " & sTableName & " SET UsedHisRun = ""Yes"", UsedRun = ""Yes"" WHERE UPPER([Function Name]) = '" & sFxName.ToUpper & "'"
            Else
                If bOpenHis Then
                    vSQLiteCommand.CommandText = "UPDATE " & sTableName & " SET UsedHisRun = ""Yes"" WHERE UPPER([Function Name]) = '" & sFxName.ToUpper & "'"
                Else
                    vSQLiteCommand.CommandText = "UPDATE " & sTableName & " SET UsedRun = ""Yes"" WHERE UPPER([Function Name]) = '" & sFxName.ToUpper & "'"
                End If
            End If
        End If
        Dim RecordsAffected As Integer = vSQLiteCommand.ExecuteNonQuery()

        'check for cases that don't work
        If RecordsAffected = 0 Then
            fN2.WriteLine(sFxName)
            'Stop
        End If
    End Sub

    'jrs 2013/11/13
    Public Sub SearchModelFiles4Fx()
        If StartDirec.Contains("Code.Ifs-Translation") Then
            'jrs 2007/12/14
            'Initialize all tables
            Dim db As SqliteConnection
            db = New SqliteConnection("DataSource=" & PutFdrvRun$ & "TablFunc.db;") '& "New=true;"
            db.Open()

            Dim vSQLiteCommand As SqliteCommand
            vSQLiteCommand = db.CreateCommand()

            vSQLiteCommand.CommandText = "UPDATE TablFunc SET UsedHisPre = ""No"", UsedPre = ""No"", UsedHisRun = ""No"", UsedRun = ""No"""
            vSQLiteCommand.ExecuteNonQuery()
            db.Close()

            db = New SqliteConnection("DataSource=" & PutFdrvRun$ & "AnalFunc.db;") '& "New=true;"
            db.Open()

            vSQLiteCommand = db.CreateCommand()
            vSQLiteCommand.CommandText = "UPDATE AnalFunc SET UsedHisPre = ""No"", UsedPre = ""No"", UsedHisRun = ""No"", UsedRun = ""No"""
            vSQLiteCommand.ExecuteNonQuery()

            db.Close()

            Console.WriteLine("")
            SearchFunctionsinFile("Agriculture.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Automobile.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Economy.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Education.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Energy.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Environment.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Health.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Infrastructure.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("IntlConf.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Population.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("SocioPol.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("StFailure.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Technology.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("ValuesData.Rebuild.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Water.Rebuild.vb", "XYTABL", "TablFunc.db")

            SearchFunctionsinFile("Agriculture.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Automobile.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Economy.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Education.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Energy.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Environment.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Health.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Infrastructure.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("IntlConf.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Population.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("SocioPol.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("StFailure.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Technology.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("ValuesData.Rebuild.vb", ".GetRegression", "TablFunc.db")
            SearchFunctionsinFile("Water.Rebuild.vb", ".GetRegression", "TablFunc.db")

            'jrs 2026/03/26
            SearchFunctionsinFile("Education.Rebuild.vb", "getFunctionNameYrOrMstRct", "TablFunc.db")
            SearchFunctionsinFile("Infrastructure.Rebuild.vb", "getFunctionNameYrOrMstRct", "TablFunc.db")

            SearchFunctionsinFile("Agriculture.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Automobile.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Economy.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Education.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Energy.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Environment.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Health.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Infrastructure.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("IntlConf.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Population.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("SocioPol.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("StFailure.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Technology.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("ValuesData.Forecast.vb", "XYTABL", "TablFunc.db")
            SearchFunctionsinFile("Water.Forecast.vb", "XYTABL", "TablFunc.db")

            'SearchFunctionsinFile("POPULAT.Rebuild.vb", "EstimateFromMultiFunction"
            SearchFunctionsinFile("Population.Rebuild.vb", "TbName1$ =", "TablFunc.db")
            SearchFunctionsinFile("Population.Rebuild.vb", "TbName$ =", "TablFunc.db")

            SearchFunctionsinFile("Agriculture.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Automobile.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Economy.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Education.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Energy.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Environment.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Health.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Infrastructure.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("IntlConf.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Population.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("SocioPol.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("StFailure.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Technology.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("ValuesData.Rebuild.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Water.Rebuild.vb", "AnalFunc", "AnalFunc.db")

            SearchFunctionsinFile("Agriculture.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Automobile.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Economy.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Education.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Energy.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Environment.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Health.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Infrastructure.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("IntlConf.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Population.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("SocioPol.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("StFailure.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Technology.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("ValuesData.Rebuild.vb", ".GetRegression", "AnalFunc.db")
            SearchFunctionsinFile("Water.Rebuild.vb", ".GetRegression", "AnalFunc.db")

            SearchFunctionsinFile("Agriculture.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Automobile.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Economy.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Education.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Energy.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Environment.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Health.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Infrastructure.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("IntlConf.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Population.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("SocioPol.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("StFailure.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Technology.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("ValuesData.Forecast.vb", "AnalFunc", "AnalFunc.db")
            SearchFunctionsinFile("Water.Forecast.vb", "AnalFunc", "AnalFunc.db")

            'jrs 2007/12/14
            'If MsgBox("Please copy TablFunc.mdb and AnalFunc.mdb from RUNFILES to DATA. Do you want the system to do it for you?", vbYesNo, "Mark Functions used") = vbYes Then
            FileCopy(PutFdrvRun$ & "TablFunc.db", PutDataFileDir & "TablFunc.db")
            FileCopy(PutFdrvRun$ & "AnalFunc.db", PutDataFileDir & "AnalFunc.db")
            Console.WriteLine("")
            Console.WriteLine("Process Completed.")
            Console.WriteLine("")
            'End If
        End If
    End Sub

    'jrs 2013/11/13
    Private Sub SearchFunctionsinFile(sFile As String, sKeyWord As String, sTargetFile As String)
        Console.WriteLine("Processing: " & sFile & ", " & sKeyWord)
        'Open File
        Dim fn As System.IO.StreamReader, fN2 As System.IO.StreamWriter

        fn = New System.IO.StreamReader(StartDirec & "src\IFs.Core\Models\" & sFile)

        ' gvdb 2024/04/05
        ' unused variable
        Dim db As SqliteConnection ', dbM As SqliteConnection
        db = New SqliteConnection("DataSource=" & PutFdrvRun$ & sTargetFile & ";") '& "New=true;"
        db.Open()

        If sKeyWord = "XYTABL" Then
            fN2 = New System.IO.StreamWriter(StartDirec & "docs\" & sFile & "Fx.txt", False)
            fN2.WriteLine("List of Functions not found:")
            fN2.WriteLine("=============================")
            fN2.Close()
        End If
        fN2 = New System.IO.StreamWriter(StartDirec & "docs\" & sFile & "Fx.txt", True)

        Dim sLine As String, sFxName As String
        Dim xLine() As String
        Dim i As Integer
        Dim AllLines() As String, LineCounter As Integer, j As Integer, bFxNameYrOrMstRct As Boolean 'jrs 2021/07/08 changed LineCounter and jto Long
        Dim bOpenHis As Boolean, CountIFs As Integer, pos As Integer, sElse As String, bOpenForecast As Boolean
        'jrs 2024/06/20
        Dim bGradeLoop As Boolean
        'jrs 2025/10/28
        Dim bLaborCoeffFx As Boolean = False
        Dim sLaborCoeffFx As String = ""
        'Loop through file
        Do While Not fn.EndOfStream
            sLine = fn.ReadLine
            'If sLine.Contains(") Versus Impaired Glucose Tolerance (") And sKeyWord = ".GetRegression" Then Stop
            If sLine.Contains("Versus Labor Coeff(lab/$mil)") And
            sFile = "Economy.Forecast.vb" And sKeyWord = "XYTABL" And sTargetFile = "TablFunc.db" Then
                bLaborCoeffFx = True
                pos = sLine.IndexOf("=")
                sLaborCoeffFx = sLine.Substring(pos + 1).Trim
                sLaborCoeffFx = sLaborCoeffFx.Replace(""" & ConstantDollarsYear.ToString & """, ConstantDollarsYear.ToString)
                sLaborCoeffFx = sLaborCoeffFx.Replace("""", "")
            End If
            LineCounter = LineCounter + 1
            ReDim Preserve AllLines(LineCounter)
            AllLines(LineCounter) = sLine
            pos = sLine.IndexOf("If cntx.HistoricMode Then")
            If pos = -1 Then
                pos = sLine.IndexOf("If context.HistoricMode Then")
            End If
            If pos = -1 Then
                pos = sLine.IndexOf("If context.HistoricMode = True Then")
            End If
            If pos = -1 Then
                pos = sLine.IndexOf("If cntx.HistoricMode = True Then")
            End If
            If pos >= 0 AndAlso (sLine.Trim.Substring(0, 1) <> "'" Or sLine.Trim.IndexOf("'") > pos) Then
                bOpenHis = True
                CountIFs = 0
            Else
                If bOpenHis Then
                    pos = sLine.IndexOf("If")
                    'Discard one line ifs
                    If pos >= 0 AndAlso (sLine.Trim.Substring(0, 1) <> "'" Or sLine.IndexOf("'") > pos) _
                    And Not sLine.Contains("End If") And Not sLine.Contains("ElseIf") Then
                        pos = sLine.IndexOf("Then")
                        If pos >= 0 Then
                            sElse = sLine.Substring(pos + 4)
                            pos = sElse.IndexOf("'")
                            If pos >= 0 Then
                                sElse = sElse.Substring(1, pos - 1)
                            End If
                            If sElse.Trim = "" Then
                                CountIFs = CountIFs + 1
                            End If
                        Else
                            sElse = ""
                        End If
                    Else
                        pos = sLine.IndexOf("Else")
                        If pos >= 0 AndAlso (sLine.Trim.Substring(0, 1) <> "'" Or sLine.IndexOf("'") > pos) And CountIFs = 0 Then
                            bOpenForecast = True
                            bOpenHis = False
                        Else
                            pos = sLine.IndexOf("End If")
                            If pos >= 0 AndAlso (sLine.Trim.Substring(0, 1) <> "'" Or sLine.IndexOf("'") > pos) Then
                                If CountIFs = 0 Then
                                    bOpenHis = False
                                    bOpenForecast = False
                                Else
                                    CountIFs = CountIFs - 1
                                End If
                            End If
                        End If
                    End If
                Else 'check for not history
                    pos = sLine.IndexOf("If Not cntx.HistoricMode Then")
                    If pos = -1 Then
                        pos = sLine.IndexOf("If Not context.HistoricMode Then")
                    End If

                    If pos >= 0 AndAlso (sLine.Trim.Substring(0, 1) <> "'" Or sLine.IndexOf("'") > pos) Then
                        bOpenForecast = True
                        CountIFs = 0
                    Else
                        If bOpenForecast Then
                            pos = sLine.IndexOf("If")
                            'Discard one line ifs
                            If pos >= 0 AndAlso (sLine.Trim.Substring(0, 1) <> "'" Or sLine.IndexOf("'") > pos) And Not sLine.Contains("End If") Then
                                pos = sLine.IndexOf("Then")
                                If pos >= 0 Then
                                    sElse = sLine.Substring(pos + 4)
                                    pos = sElse.IndexOf("'")
                                    If pos >= 0 Then
                                        sElse = sElse.Substring(1, pos - 1)
                                    End If
                                Else
                                    sElse = ""
                                End If
                                If sElse.Trim = "" Then
                                    CountIFs = CountIFs + 1
                                End If
                            Else
                                pos = sLine.IndexOf("Else")
                                If pos >= 0 AndAlso (sLine.Trim.Substring(0, 1) <> "'" Or sLine.IndexOf("'") > pos) And CountIFs = 0 Then
                                    bOpenHis = True
                                    bOpenForecast = False
                                Else
                                    pos = sLine.IndexOf("End If")
                                    If pos >= 0 AndAlso (sLine.Trim.Substring(0, 1) <> "'" Or sLine.IndexOf("'") > pos) Then
                                        If CountIFs = 0 Then
                                            bOpenHis = False
                                            bOpenForecast = False
                                        Else
                                            CountIFs = CountIFs - 1
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            'search for KeyWord
            'jrs 2012/02/17 include comments with '* there are some special tables in DATAENV
            'jrs 2012/01/18 try to ignore commented lines
            If sLine.IndexOf(sKeyWord) >= 0 AndAlso
            (sLine.Trim.Substring(0, 1) <> "'" Or sLine.IndexOf("'") > sLine.IndexOf(sKeyWord) _
            Or sLine.Trim.Substring(0, 2) = "'*") Then
                'build table name
                sFxName = ""
                pos = sLine.IndexOf(sKeyWord)
                If sLine.Substring(pos + sKeyWord.Length + 1, 1) = """" Then
                    'split by string using " as divider
                    xLine = sLine.Split(CChar(""""))
                    For i = 0 To xLine.Length - 1
                        If xLine(i).IndexOf(sKeyWord) >= 0 Then
                            sFxName = xLine(i + 1)
                            If xLine.Length - 1 >= i + 2 Then
                                If xLine(i + 2).IndexOf("BaseYear") >= 0 Then
                                    sFxName = sFxName & CStr(BaseYear) & xLine(i + 3)
                                    'jrs 2024/06/18
                                    Dim sPPP As String = ""
                                    If sFxName.Contains("(PPP " & ConstantDollarsYear & ")") Then
                                        sPPP = "(PPP " & ConstantDollarsYear & ")"
                                    End If

                                    sFxName = getFunctionNameYrOrMstRct(sFxName, BaseYear, sPPP)
                                End If
                            End If
                            Exit For
                        End If
                    Next i
                End If
                If sFxName = "" Then
                    'split by string using ( as divider
                    xLine = sLine.Split(CChar("("))
                    'build table name
                    For i = 0 To xLine.Length - 1
                        If xLine(i).IndexOf(sKeyWord) >= 0 And i < xLine.Length - 1 Then
                            Dim sChar As String
                            If sKeyWord = ".GetRegression" Then
                                sChar = ")"
                            Else
                                sChar = ","
                            End If
                            pos = xLine(i + 1).IndexOf(sChar)
                            If pos >= 0 Then
                                sFxName = xLine(i + 1).Substring(0, xLine(i + 1).IndexOf(sChar))
                            Else
                                sFxName = xLine(i + 1)
                                If sFxName.Contains("getFunctionNameYrOrMstRct") Then
                                    If sLine.Contains("""") Then
                                        Dim xLine2 = sLine.Split(CChar(""""))
                                        For ii = 0 To xLine2.Length - 1
                                            If xLine2(ii).Contains("getFunctionNameYrOrMstRct") Then
                                                sFxName = xLine2(ii + 1)
                                                If xLine2(ii + 2).Contains("BaseYear") Then
                                                    sFxName = sFxName & CStr(BaseYear) & xLine2(ii + 3)
                                                    'jrs 2024/06/18
                                                    Dim sPPP As String = ""
                                                    If sFxName.Contains("(PPP " & ConstantDollarsYear & ")") Then
                                                        sPPP = "(PPP " & ConstantDollarsYear & ")"
                                                    End If
                                                    sFxName = getFunctionNameYrOrMstRct(sFxName, BaseYear, sPPP)
                                                End If
                                                Exit For
                                            End If
                                        Next ii
                                    Else
                                        sFxName = xLine(i + 2)
                                    End If
                                End If
                            End If
                            pos = sFxName.IndexOf("""")
                            If pos >= 0 Then
                                pos = sFxName.Substring(pos + 1).IndexOf("""")
                                If pos = -1 Then
                                    sFxName = xLine(i + 1)
                                    For j = i + 2 To xLine.Length - 1
                                        sFxName = sFxName & "(" & xLine(j)
                                        If xLine(j).IndexOf("""") >= 0 Then
                                            sFxName = sFxName.Substring(1, sFxName.Substring(1).IndexOf(""""))
                                            Exit For
                                        End If
                                    Next j
                                End If
                            End If
                            pos = sFxName.IndexOf("'")
                            If pos >= 0 Then
                                sFxName = sFxName.Substring(0, pos - 1)
                            End If
                            sFxName = sFxName.Replace("""", "")

                            'jrs 2026/03/26
                            If sKeyWord = "getFunctionNameYrOrMstRct" Then
                                bFxNameYrOrMstRct = True
                            End If
                            'jrs 2026/03/26 added AllLines(j).IndexOf(sFxName & " = ") >= 0 And 
                            'jrs 2024/06/18 added As String = 
                            'This finds variable name where Fx Name is stored
                            For j = LineCounter - 1 To 1 Step -1
                                If (AllLines(j).Replace("$", "").Contains(sFxName.Replace("$", "") & " = ") Or
                                    AllLines(j).Replace("$", "").Contains(sFxName.Replace("$", "") & " As String = ")) AndAlso
                                    (AllLines(j).Trim.Substring(0, 1) <> "'" Or (AllLines(j).IndexOf(sFxName & " = ") >= 0 And AllLines(j).IndexOf("'") > AllLines(j).IndexOf(sFxName & " = "))) Then
                                    If AllLines(j).Contains("getFunctionNameYrOrMstRct") Then
                                        bFxNameYrOrMstRct = True
                                    Else
                                        If bFxNameYrOrMstRct Then
                                            'jrs 2026/03/26
                                            Dim sVarName4Fx As String
                                            sVarName4Fx = sFxName

                                            sFxName = AllLines(j).Replace("$", "").Replace(sFxName.Replace("$", "") & " = ", "")
                                            'jrs 2021/07/08
                                            sFxName = sFxName.Replace("If gndr% = 2 Then", "")
                                            'jrs 2024/06/20
                                            sFxName = sFxName.Replace("If gndr% = 1 Then", "")

                                            'jrs 2026/03/27 not working for Education (Both Sexes, 25+) Vs Married % Diff, 20_24 and 15_19, Women (
                                            'can't find why is needed
                                            'removing
                                            'pos = sFxName.IndexOf("_")
                                            'Do While pos >= 0
                                            '    j = j + 1
                                            '    sFxName = sFxName.Replace("_", "").Trim & AllLines(j)
                                            '    pos = sFxName.IndexOf("_")
                                            'Loop
                                            pos = sFxName.IndexOf("'")
                                            If pos >= 0 Then
                                                sFxName = sFxName.Substring(1, pos - 1)
                                            End If

                                            'jrs 2024/06/20
                                            If sFxName.Contains(" & grd%.ToString.Trim & ") Then
                                                bGradeLoop = True
                                            End If
                                            'jrs 2021/07/08
                                            sFxName = sFxName.Replace(" & grd%.ToString.Trim & ", "1")

                                            sFxName = sFxName.Replace("""", "")
                                            sFxName = sFxName.Replace("( & CStr(BaseYear) & )", "(" & CStr(BaseYear) & ")")
                                            sFxName = sFxName.Replace("( & CStr(cntx.BaseYear) & )", "(" & CStr(BaseYear) & ")")
                                            sFxName = sFxName.Replace("( & CStr(context.BaseYear) & )", "(" & CStr(BaseYear) & ")")
                                            'jrs 2026/03/26
                                            sFxName = sFxName.Replace("( & CStr(context.FirstYear) & )", "(" & CStr(BaseYear) & ")")
                                            sFxName = sFxName.Replace("( & BaseYear.ToString & )", "(" & CStr(BaseYear) & ")")
                                            'jrs 2024/06/20
                                            sFxName = sFxName.Replace("( & BaseYear & )", "(" & CStr(BaseYear) & ")")
                                            If sFxName.Contains(" & CStr(") Then
                                                Dim pos1 As Integer = sFxName.IndexOf(" & CStr(") + 8
                                                Dim pos2 As Integer = sFxName.IndexOf(") & )") + 6
                                                Dim YearVar As String = sFxName.Substring(pos1, sFxName.Length - pos2)
                                                sFxName = sFxName.Replace("( & CStr(" & YearVar & ") & )", "(" & CStr(BaseYear) & ")")
                                            End If

                                            sFxName = sFxName.Trim
                                            sFxName = sFxName.Replace("Dim ", "")
                                            'jrs 2026/03/26
                                            sFxName = sFxName.Replace(sVarName4Fx & " As String = ", "")
                                            'jrs 2024/06/18
                                            sFxName = sFxName.Replace(" As String = ", "")
                                            sFxName = sFxName.Replace(" & ConstantDollarsYear & ", ConstantDollarsYear.ToString)
                                            Dim sPPP As String = ""
                                            If sFxName.Contains("(PPP " & ConstantDollarsYear & ")") Then
                                                sPPP = "(PPP " & ConstantDollarsYear & ")"
                                            End If
                                            sFxName = getFunctionNameYrOrMstRct(sFxName, BaseYear, sPPP)
                                            bFxNameYrOrMstRct = False
                                        Else
                                            sFxName = AllLines(j).Replace("$", "").Replace(sFxName.Replace("$", "") & " = ", "")
                                            sFxName = sFxName.Replace("Dim ", "")
                                            pos = sFxName.IndexOf("_")
                                            Do While pos >= 0
                                                j = j + 1
                                                sFxName = sFxName.Replace("_", "").Trim & AllLines(j)
                                                pos = sFxName.IndexOf("_")
                                            Loop
                                            pos = sFxName.IndexOf("'")
                                            If pos > 0 Then
                                                sFxName = sFxName.Substring(1, pos - 1)
                                            ElseIf pos = 0 Then
                                                sFxName = ""
                                            End If
                                            sFxName = sFxName.Replace("""", "")
                                            sFxName = sFxName.Trim
                                            pos = sFxName.IndexOf("CStr(BaseYear)")
                                            If pos >= 0 Then
                                                sFxName = sFxName.Replace(" & CStr(BaseYear) & ", CStr(BaseYear))
                                                'jrs 2024/06/18
                                                sFxName = sFxName.Replace(""" & ConstantDollarsYear & """, ConstantDollarsYear.ToString)
                                                sFxName = getFunctionNameYrOrMstRct(sFxName, BaseYear)
                                            End If
                                        End If
                                        Exit For
                                    End If
                                End If
                            Next j

                            'If sFxName = "" Then
                            '    Console.WriteLine("Function not found:" & sFxName)
                            '    fN2.WriteLine("Function: " & sFxName)
                            'End If

                            If sFxName.Contains(".GetDimension(11)") Then
                                sFxName = sFxName.Replace(" + cntx.GetDimension(11).Buckets(CShort(cc%))", "")
                                sFxName = sFxName.Replace("""", "")
                                sFxName = sFxName.Trim
                                For j = 1 To 11
                                    UpdateFuncFile(sFile, bOpenHis, bOpenForecast, db, sFxName & GRPS(11, j), fN2)
                                Next j
                            ElseIf sFxName.Contains("GRPS(11") Then
                                sFxName = sFxName.Replace(" + GRPS(11, cc%)", "")
                                sFxName = sFxName.Replace("""", "")
                                sFxName = sFxName.Trim
                                For j = 1 To 11
                                    UpdateFuncFile(sFile, bOpenHis, bOpenForecast, db, sFxName & GRPS(11, j), fN2)
                                Next j
                            ElseIf sFxName.Contains(".GetDimension(12)") Then
                                sFxName = sFxName.Replace(" + cntx.GetDimension(12).Buckets(CShort(cc%))", "")
                                sFxName = sFxName.Replace("""", "")
                                sFxName = sFxName.Trim
                                For j = 1 To 12
                                    UpdateFuncFile(sFile, bOpenHis, bOpenForecast, db, sFxName & GRPS(12, j), fN2)
                                Next j
                            ElseIf sFxName.Contains("GRPS(12") Then
                                sFxName = sFxName.Replace(" + GRPS(12, cc%)", "")
                                sFxName = sFxName.Replace("""", "")
                                sFxName = sFxName.Trim
                                For j = 1 To 12
                                    UpdateFuncFile(sFile, bOpenHis, bOpenForecast, db, sFxName & GRPS(12, j), fN2)
                                Next j
                            ElseIf bGradeLoop Then 'jrs 2024/06/20
                                Dim sFxNameUp As String = ""
                                For j = 1 To 6
                                    sFxNameUp = sFxName.Replace("Grade 1", "Grade " & j.ToString)
                                    UpdateFuncFile(sFile, bOpenHis, bOpenForecast, db, sFxNameUp, fN2)
                                    sFxNameUp = sFxNameUp.Replace("Female", "Male")
                                    UpdateFuncFile(sFile, bOpenHis, bOpenForecast, db, sFxNameUp, fN2)
                                Next
                                bGradeLoop = False
                            ElseIf bLaborCoeffFx Then 'jrs 2025/10/28
                                For j = 1 To 6
                                    sFxName = sLaborCoeffFx & " (" & GRPS(2, j) & ")(Unskilled) (" & BaseYear.ToString & ") (Power)"
                                    If sFxName.Length > 90 Then sFxName = sFxName.Substring(0, 90)
                                    UpdateFuncFile(sFile, False, True, db, sFxName, fN2)
                                    sFxName = sLaborCoeffFx & " (" & GRPS(2, j) & ")(Skilled) (" & BaseYear.ToString & ") (Power)"
                                    If sFxName.Length > 90 Then sFxName = sFxName.Substring(0, 90)
                                    UpdateFuncFile(sFile, False, True, db, sFxName, fN2)
                                    sFxName = sLaborCoeffFx & " (" & GRPS(2, j) & ")(Unskilled) (1960) (Power)"
                                    If sFxName.Length > 90 Then sFxName = sFxName.Substring(0, 90)
                                    UpdateFuncFile(sFile, True, False, db, sFxName, fN2)
                                    sFxName = sLaborCoeffFx & " (" & GRPS(2, j) & ")(Skilled) (1960) (Power)"
                                    If sFxName.Length > 90 Then sFxName = sFxName.Substring(0, 90)
                                    UpdateFuncFile(sFile, True, False, db, sFxName, fN2)
                                Next
                                bLaborCoeffFx = False
                            Else
                                UpdateFuncFile(sFile, bOpenHis, bOpenForecast, db, sFxName, fN2)
                            End If

                            Exit For
                        End If
                    Next i
                Else
                    'Update Func File
                    UpdateFuncFile(sFile, bOpenHis, bOpenForecast, db, sFxName, fN2)
                End If
            End If
        Loop
        'Close File
        fn.Close()
        fN2.Close()
        db.Close()
    End Sub



    'jrs 2022/08/30
    Public Sub InitVars()
        'Initialize Global Variables
        ' gvdb 2024/04/05
        ' update path for Net8
        StartDirec = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\Debug\net8\", "")

        'jrs 2022/10/18 
        'Development mode
        If StartDirec.Contains("Code.Ifs-Translation\") Then
            PutFdrvRun = StartDirec.Replace("Code.Ifs-Translation\", "RUNFILES\")
            PutDataFileDir = StartDirec.Replace("Code.Ifs-Translation\", "DATA\")
        Else
            ' gvdb 2024/04/05
            ' update path for Net8
            PutFdrvRun = StartDirec.Replace("net8\", "RUNFILES\")
            PutDataFileDir = StartDirec.Replace("net8\", "DATA\")
        End If
        'Open ifsInit.db
        Dim vSQLiteCon As SqliteConnection
        'jrs 2023/06/22
        If StartDirec.Contains("Code.Ifs-Translation\") Then
            vSQLiteCon = New SqliteConnection("DataSource=" & StartDirec.Replace("Code.Ifs-Translation\", "") & "ifsInit.db;")
        Else
            ' gvdb 2024/04/05
            ' update path for Net8
            vSQLiteCon = New SqliteConnection("DataSource=" & StartDirec.Replace("net8\", "") & "ifsInit.db;")
        End If
        vSQLiteCon.Open()

        Dim vSQLiteCommand As SqliteCommand
        Dim vSQLiteReader As SqliteDataReader

        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = "SELECT * FROM IFsInit WHERE [Variable] = ""FirstYearForecast%"""
        vSQLiteReader = vSQLiteCommand.ExecuteReader()
        vSQLiteReader.Read()
        BaseYear = CInt(vSQLiteReader(1))
        vSQLiteReader.Close()
        'jrs 2026/03/03
        ModelLoad = "LoadFull"

        'jrs 2024/06/18
        vSQLiteCommand.CommandText = "SELECT * FROM IFsInit WHERE [Variable] = ""ConstantDollarsYear"""
        vSQLiteReader = vSQLiteCommand.ExecuteReader()
        vSQLiteReader.Read()
        ConstantDollarsYear = CInt(vSQLiteReader(1))
        vSQLiteReader.Close()

        vSQLiteCon.Close()

        PastConstantDollarsYears = New List(Of Integer)
        PastConstantDollarsYears.Add(2011)
        PastConstantDollarsYears.Add(2005)
        PastConstantDollarsYears.Add(2000)


        'Open TablFunc.db to load Regression names
        vSQLiteCon = New SqliteConnection("DataSource=" & PutFdrvRun & "TablFunc.db;")
        vSQLiteCon.Open()

        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = "SELECT [Function Name] FROM TablFunc"
        vSQLiteReader = vSQLiteCommand.ExecuteReader()

        RegCoeffs = New List(Of String)
        While (vSQLiteReader.Read())
            RegCoeffs.Add(vSQLiteReader(0).ToString)
        End While

        vSQLiteReader.Close()
        vSQLiteCon.Close()

        'Open ifs.grp
        Using reader As StreamReader = New StreamReader(PutDataFileDir & "ifs.grp")
            Dim sLine As String
            sLine = reader.ReadLine
            Dim iDims As Integer = CInt(sLine)
            ReDim GRPS$(iDims, 186)
            Dim i As Integer = 1
            Do While Not reader.EndOfStream
                'Dimension Id
                i = i + 1
                'Load Line
                sLine = reader.ReadLine
                'Split into dimensions
                Dim xDims = sLine.Split(CChar(","))
                'Load Dimensions
                For j = 0 To xDims.Length - 1
                    GRPS(i, j) = xDims(j).Trim
                Next
            Loop
        End Using

        'jrs 2022/12/23
        vSQLiteCon = New SqliteConnection("DataSource=" & PutFdrvRun & "ifsVar.db;")
        vSQLiteCon.Open()

        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = "SELECT COUNT(*) FROM IFsVar"
        vSQLiteReader = vSQLiteCommand.ExecuteReader()

        vSQLiteReader.Read()

        NumbVars = CInt(vSQLiteReader.Item(0))

        vSQLiteReader.Close()

        ReDim V(NumbVars)

        'mti 2025/08/07 Start dimensions
        ReDim VDim1(NumbVars)
        ReDim VDim2(NumbVars)
        ReDim VDim3(NumbVars)

        'vSQLiteCommand.CommandText = "SELECT [NAME] FROM IFsVar ORDER BY UPPER([NAME])"
        vSQLiteCommand.CommandText = "SELECT [NAME], [DIMENSION1], [DIMENSION2], [DIMENSION3] " &
                             "FROM IFsVar ORDER BY UPPER([NAME])"
        'mti 2025/08/07 End

        vSQLiteReader = vSQLiteCommand.ExecuteReader()

        'jrs 2023/07/26
        VNames = New List(Of String)

        Dim ii As Integer = 0
        Do While vSQLiteReader.Read
            ii = ii + 1
            V(ii) = vSQLiteReader.Item(0).ToString
            'jrs 2023/07/26
            VNames.Add(V(ii))

            'mti 2025/08/07 Start dimensions
            VDim1(ii) = If(vSQLiteReader.IsDBNull(1), 0, Convert.ToInt32(vSQLiteReader.GetValue(1)))
            VDim2(ii) = If(vSQLiteReader.IsDBNull(2), 0, Convert.ToInt32(vSQLiteReader.GetValue(2)))
            VDim3(ii) = If(vSQLiteReader.IsDBNull(3), 0, Convert.ToInt32(vSQLiteReader.GetValue(3)))
            'mti 2025/08/07 End

        Loop

        vSQLiteReader.Close()
        vSQLiteCon.Close()

    End Sub

    'jrs 2024/06/18 addedd , Optional sYrConstantDollars As String = ""
    Friend Function getFunctionNameYrOrMstRct(sFuncName As String, iYr As Integer, Optional sYrConstantDollars As String = "") As String
        'If cntx.GetRegression(sFuncName).Count > 0 Then
        If sFuncName > "" Then
            If RegCoeffs.Contains(sFuncName) Then
                Return sFuncName
            Else
                'jrs 2024/06/18
                Dim PastCstYrIdx As Integer = 0
                Dim PrevConstantYear As Integer = ConstantDollarsYear

                Do While True
                    Dim sFuncNameSearch As String, iYrSearch As Integer
                    'searching forward
                    iYrSearch = iYr + 1
                    Do While iYrSearch <= DateTime.Now.Year
                        sFuncNameSearch = sFuncName.Replace(iYr.ToString, iYrSearch.ToString)
                        'If cntx.GetRegression(sFuncNameSearch).Count > 0 Then
                        If RegCoeffs.Contains(sFuncNameSearch) Then
                            Return sFuncNameSearch
                        End If
                        iYrSearch = iYrSearch + 1
                    Loop

                    sFuncNameSearch = sFuncName.Replace(iYr.ToString, "MostRecent")
                    'If cntx.GetRegression(sFuncNameSearch).Count > 0 Then
                    If RegCoeffs.Contains(sFuncNameSearch) Then
                        Return sFuncNameSearch
                    End If
                    sFuncNameSearch = sFuncName.Replace(iYr.ToString, "MOSTRECENT")
                    'If cntx.GetRegression(sFuncNameSearch).Count > 0 Then
                    If RegCoeffs.Contains(sFuncNameSearch) Then
                        Return sFuncNameSearch
                    End If

                    'searching backward
                    iYrSearch = iYr - 1
                    Dim iMax As Integer
                    'jrs 2022/03/01 Need to go further back when rebasing to 2020 (GDP/Capita (PPP) Versus Primary Net Enrollment Male (1999)) iMax = AMAX(iYr - 20, 1960)
                    iMax = System.Math.Max(iYr - 30, 1960)
                    Do While iYrSearch >= iMax
                        sFuncNameSearch = sFuncName.Replace(iYr.ToString, iYrSearch.ToString)
                        'If cntx.GetRegression(sFuncNameSearch).Count > 0 Then
                        If RegCoeffs.Contains(sFuncNameSearch) Then
                            Return sFuncNameSearch
                        End If
                        iYrSearch = iYrSearch - 1
                    Loop

                    If sYrConstantDollars = "" Then
                        Exit Do
                    Else
                        If PastCstYrIdx >= PastConstantDollarsYears.Count Then
                            If PastCstYrIdx > PastConstantDollarsYears.Count Then
                                Exit Do
                            Else
                                'Checking option (PPP)

                                'mti 2025/08/07 start
                                'Dim sYrConstantDollarsNew As String = sYrConstantDollars.Replace(" " & PrevConstantYear.ToString, "")
                                'sFuncName = sFuncName.Replace(sYrConstantDollars, sYrConstantDollarsNew)

                                'this is Jose's function;the lines above do not work for the purpose (building function name onlywith PPP,noyear;
                                'I replaced the two lines above with a code that works(used regex: (?i) case insensitive; \s* → any spaces (including none) between PPP and the year;\d{4} → exactly four digits (the year) etc
                                'If this is harming JRS function I can write another;Jose will have to uncomment the two previous lines and comment the next (and,let me know please)

                                sFuncName = Regex.Replace(sFuncName, "(?i)\bPPP\s*\d{4}\b", "PPP")
                                'mti 2025/08/07 end

                                PastCstYrIdx = PastCstYrIdx + 1
                            End If
                        Else
                            Dim sYrConstantDollarsNew As String = sYrConstantDollars.Replace(PrevConstantYear.ToString, PastConstantDollarsYears(PastCstYrIdx).ToString)
                            sFuncName = sFuncName.Replace(sYrConstantDollars, sYrConstantDollarsNew)
                            sYrConstantDollars = sYrConstantDollarsNew
                            PrevConstantYear = PastConstantDollarsYears(PastCstYrIdx)
                            PastCstYrIdx = PastCstYrIdx + 1
                        End If
                    End If
                Loop

            End If
        End If
        Return ""
    End Function

    'jrs 2023/06/13
    Public Sub AddCountry(sCountry As String, sFIPSCD As String)
        '-------------------------------------------------------------------

        If sCountry = "" Or sFIPSCD = "" Then
            Exit Sub
        End If

        '   Check to see if the country name exists.
        '   --------------------------------------
        'Done in the GUI

        'Check for the IFsCOVATRA file
        'CheckCOVATRA
        '   Add the new country in the set of Tables.
        '   ------------------------------------------
        AddCountryMult("IFs", sCountry, sFIPSCD)
        AddCountryMult("IFsHistSeries", sCountry, sFIPSCD)
        AddCountryMult("IFsCoVaTra", sCountry, sFIPSCD)
        AddCountryMult("IFsCoVaTraSeries", sCountry, sFIPSCD)
        AddCountryMult("IFsWVSCohort", sCountry, sFIPSCD)

    End Sub

    'jrs 2023/06/13
    Public Sub UpdCountry(sCountry As String, sNew As String, sFIPSCD As String)
        '-------------------------------------------------------------------

        If sCountry = "" Or sNew = "" Then
            Exit Sub
        End If

        '   Check to see if the country name exists.
        '   --------------------------------------
        'Done in the GUI

        'Check for the IFsCOVATRA file
        'CheckCOVATRA
        '   Add the new country in the set of Tables.
        '   ------------------------------------------
        UpdCountryMult("IFs", sCountry, sNew, sFIPSCD)
        UpdCountryMult("IFsHistSeries", sCountry, sNew, sFIPSCD)
        UpdCountryMult("IFsCoVaTra", sCountry, sNew, sFIPSCD)
        UpdCountryMult("IFsCoVaTraSeries", sCountry, sNew, sFIPSCD)
        UpdCountryMult("IFsWVSCohort", sCountry, sNew, sFIPSCD)

    End Sub

    'jrs 2023/06/13
    Public Sub DelCountry(sCountry As String)
        '-------------------------------------------------------------------

        If sCountry = "" Then
            Exit Sub
        End If

        '   Check to see if the country name exists.
        '   --------------------------------------
        'Done in the GUI

        'Check for the IFsCOVATRA file
        'CheckCOVATRA
        '   Add the new country in the set of Tables.
        '   ------------------------------------------
        DelCountryMult("IFs", sCountry)
        DelCountryMult("IFsHistSeries", sCountry)
        DelCountryMult("IFsCoVaTra", sCountry)
        DelCountryMult("IFsCoVaTraSeries", sCountry)
        DelCountryMult("IFsWVSCohort", sCountry)

    End Sub

    'jrs 2023/06/12
    Private Sub AddCountryMult(db As String, sCountryM As String, sFIPSCDM As String)
        '-------------------------------------------------------------------
        'JRS 7/25/02 IF file doesn't exist copy it from DATA\
        Dim InFile$ = PutDataFileDir + db + ".db"
        Dim OutFile$ = PutFdrvRun + db + ".db"
        FileCopy_NotThere(InFile$, OutFile$)
        If OutFile$ = "" Then Exit Sub
        'Connect to the Database
        Dim daoDB36 As SqliteConnection
        daoDB36 = New SqliteConnection("DataSource=" & PutFdrvRun + db + ".db;")
        daoDB36.Open()

        Using cmd2 = daoDB36.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        'Add All the countries of the Database
        Dim rdTblDefs As SqliteDataReader
        Dim dbCmdTblDefs As SqliteCommand, dbCmdTDs As SqliteCommand
        dbCmdTblDefs = daoDB36.CreateCommand
        dbCmdTDs = daoDB36.CreateCommand
        Dim stmt As SqliteCommand = daoDB36.CreateCommand()

        dbCmdTblDefs.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'"
        rdTblDefs = dbCmdTblDefs.ExecuteReader
        Dim rdTDs As SqliteDataReader
        Dim tblCount As Integer = 0

        While rdTblDefs.Read
            'jrs 2023/02/23 *** start ***
            If rdTblDefs("TABLE_NAME").ToString <> "HistMigration" Then
                Dim bAdd = False
                Dim bAddFipsCD = False
                dbCmdTDs.CommandText = "SELECT * FROM [" & rdTblDefs("TABLE_NAME").ToString & "]"
                rdTDs = dbCmdTDs.ExecuteReader

                For l = 0 To rdTDs.FieldCount - 1
                    Select Case rdTDs.GetName(l)
                        Case "Country"
                            bAdd = True
                        Case "FIPS_CODE"
                            bAddFipsCD = True
                    End Select
                Next l
                If bAdd Then
                    tblCount = tblCount + 1
                    Dim sLine As String = "Adding Table: " & db & "." & rdTblDefs("TABLE_NAME").ToString
                    'jrs 2022/12/07 adding Try
                    Try
                        Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                        objWriter.WriteLine(sLine)
                        objWriter.Close()
                    Catch ex As Exception
                        'Do nothing
                    End Try

                    Using stmt
                        Dim sIns As String = "INSERT INTO [" & rdTblDefs("TABLE_NAME").ToString & "] (Country"
                        Dim sVals As String = ") VALUES(""" & sCountryM
                        If bAddFipsCD Then
                            sIns = sIns & ", FIPS_CODE"
                            sVals = sVals & """, """ & sFIPSCDM
                        End If
                        sVals = sVals & """)"
                        stmt.CommandText = sIns & sVals
                        stmt.ExecuteNonQuery()
                    End Using
                End If
                rdTDs.Close()
            End If 'not HistMigration
        End While
        rdTblDefs.Close()
        daoDB36.Close()
        daoDB36.Dispose()
    End Sub

    'jrs 2023/06/13
    Private Sub UpdCountryMult(db As String, sCountryM As String, sNew As String, sFIPSCDM As String)
        '-------------------------------------------------------------------
        'JRS 7/25/02 IF file doesn't exist copy it from DATA\
        Dim InFile$ = PutDataFileDir + db + ".db"
        Dim OutFile$ = PutFdrvRun + db + ".db"
        FileCopy_NotThere(InFile$, OutFile$)
        If OutFile$ = "" Then Exit Sub
        'Connect to the Database
        Dim daoDB36 As SqliteConnection
        daoDB36 = New SqliteConnection("DataSource=" & PutFdrvRun + db + ".db;")
        daoDB36.Open()

        Using cmd2 = daoDB36.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        'Add All the countries of the Database
        Dim rdTblDefs As SqliteDataReader
        Dim dbCmdTblDefs As SqliteCommand, dbCmdTDs As SqliteCommand
        dbCmdTblDefs = daoDB36.CreateCommand
        dbCmdTDs = daoDB36.CreateCommand
        Dim stmt As SqliteCommand = daoDB36.CreateCommand()

        dbCmdTblDefs.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'"
        rdTblDefs = dbCmdTblDefs.ExecuteReader
        Dim rdTDs As SqliteDataReader
        Dim tblCount As Integer = 0

        While rdTblDefs.Read
            Dim bAdd = False
            Dim bAddFipsCD = False
            dbCmdTDs.CommandText = "SELECT * FROM [" & rdTblDefs("TABLE_NAME").ToString & "]"
            rdTDs = dbCmdTDs.ExecuteReader

            For l = 0 To rdTDs.FieldCount - 1
                Select Case rdTDs.GetName(l)
                    Case "Country"
                        bAdd = True
                    Case "FIPS_CODE"
                        bAddFipsCD = True
                End Select
            Next l
            If bAdd Then
                tblCount = tblCount + 1
                Dim sLine As String = "Updating Table: " & db & "." & rdTblDefs("TABLE_NAME").ToString
                'jrs 2022/12/07 adding Try
                Try
                    Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                    objWriter.WriteLine(sLine)
                    objWriter.Close()
                Catch ex As Exception
                    'Do nothing
                End Try

                Using stmt
                    Dim sUpd As String = "UPDATE [" & rdTblDefs("TABLE_NAME").ToString & "] SET Country = """ & sNew
                    Dim sWhere As String = """ WHERE Country = """ & sCountryM & """"
                    If bAddFipsCD Then
                        sUpd = sUpd & """, FIPS_CODE = """ & sFIPSCDM
                    End If
                    stmt.CommandText = sUpd & sWhere
                    stmt.ExecuteNonQuery()
                End Using
            End If
            rdTDs.Close()
        End While
        rdTblDefs.Close()
        daoDB36.Close()
        daoDB36.Dispose()
    End Sub

    'jrs 2023/06/13
    Private Sub DelCountryMult(db As String, sCountryM As String)
        '-------------------------------------------------------------------
        'JRS 7/25/02 IF file doesn't exist copy it from DATA\
        Dim InFile$ = PutDataFileDir + db + ".db"
        Dim OutFile$ = PutFdrvRun + db + ".db"
        FileCopy_NotThere(InFile$, OutFile$)
        If OutFile$ = "" Then Exit Sub
        'Connect to the Database
        Dim daoDB36 As SqliteConnection
        daoDB36 = New SqliteConnection("DataSource=" & PutFdrvRun + db + ".db;")
        daoDB36.Open()

        Using cmd2 = daoDB36.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        'Add All the countries of the Database
        Dim rdTblDefs As SqliteDataReader
        Dim dbCmdTblDefs As SqliteCommand, dbCmdTDs As SqliteCommand
        dbCmdTblDefs = daoDB36.CreateCommand
        dbCmdTDs = daoDB36.CreateCommand
        Dim stmt As SqliteCommand = daoDB36.CreateCommand()

        dbCmdTblDefs.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'"
        rdTblDefs = dbCmdTblDefs.ExecuteReader
        Dim rdTDs As SqliteDataReader
        Dim tblCount As Integer = 0

        While rdTblDefs.Read
            Dim bAdd = False
            dbCmdTDs.CommandText = "SELECT * FROM [" & rdTblDefs("TABLE_NAME").ToString & "]"
            rdTDs = dbCmdTDs.ExecuteReader

            For l = 0 To rdTDs.FieldCount - 1
                Select Case rdTDs.GetName(l)
                    Case "Country"
                        bAdd = True
                End Select
            Next l
            If bAdd Then
                tblCount = tblCount + 1
                Dim sLine As String = "Deleting from Table: " & db & "." & rdTblDefs("TABLE_NAME").ToString
                'jrs 2022/12/07 adding Try
                Try
                    Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                    objWriter.WriteLine(sLine)
                    objWriter.Close()
                Catch ex As Exception
                    'Do nothing
                End Try

                Using stmt
                    Dim sDel As String = "DELETE FROM [" & rdTblDefs("TABLE_NAME").ToString & "]"
                    Dim sWhere As String = " WHERE Country = """ & sCountryM & """"
                    stmt.CommandText = sDel & sWhere
                    stmt.ExecuteNonQuery()
                End Using
            End If
            rdTDs.Close()
        End While
        rdTblDefs.Close()
        daoDB36.Close()
        daoDB36.Dispose()
    End Sub

    'jrs 2022/10/18
    Public Sub AddProvinces(sCountry As String)
        Dim dbProv As SqliteConnection
        dbProv = New SqliteConnection("DataSource=" & PutFdrvRun + "Provinces.db;")
        dbProv.Open()

        Dim dbProvCommand As SqliteCommand
        Dim rs As DataTable = New DataTable
        Dim rd As SqliteDataReader

        dbProvCommand = dbProv.CreateCommand
        dbProvCommand.CommandText = "SELECT * FROM Provinces P, AllProvinces A WHERE P.Country = A.Country AND P.Country = """ & sCountry & """ ORDER BY Province" 'jrs 2012/05/15 added order by Province
        rd = dbProvCommand.ExecuteReader
        'jrs 2022/10/06
        Dim dsAll As DataSet = New DataSet
        dsAll.EnforceConstraints = False
        dsAll.Tables.Add(rs)
        rs.Load(rd)

        Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=False)
        objWriter.Close()

        '   Add the new country in the set of Tables.
        '   ------------------------------------------
        AddProvincesMult("IFs", rs)
        AddProvincesMult("IFsHistSeries", rs)
        AddProvincesMult("IFsCoVaTra", rs)
        AddProvincesMult("IFsCoVaTraSeries", rs)
        AddProvincesMult("IFsWVSCohort", rs)
        AddProvincesDat(sCountry)
        'jrs 2026/03/24
        AddProvincesBilateral(rs)

        'Adding to RegionIDNew$
        '----------------------
        Dim bFirst As Boolean = True
        Dim rsReg As SqliteDataReader
        Dim dbProvCmdReg As SqliteCommand = dbProv.CreateCommand
        Dim dbProvCmdCtry As SqliteCommand = dbProv.CreateCommand
        dbProvCmdReg.CommandText = "SELECT Distinct Region, Country FROM Regionalization WHERE Country = """ & sCountry & """"
        rsReg = dbProvCmdReg.ExecuteReader

        Dim dbR As SqliteConnection
        dbR = New SqliteConnection("DataSource=" & PutFdrvRun + "ifs.db;")
        dbR.Open()

        Dim RCount As Integer
        Dim dbRCmd As SqliteCommand = dbR.CreateCommand
        dbRCmd.CommandText = "SELECT COUNT(*) FROM [RegionIDNewFull]"
        Dim rsRCount As SqliteDataReader = dbRCmd.ExecuteReader
        rsRCount.Read()
        RCount = CInt(rsRCount(0))
        rsRCount.Close()

        Dim dbRcmdGN As SqliteCommand
        dbRcmdGN = dbR.CreateCommand
        dbRcmdGN.CommandText = "SELECT MAX(GroupNum) as GN FROM GroupIDNewFull"
        Dim valtblGN As SqliteDataReader
        valtblGN = dbRcmdGN.ExecuteReader
        valtblGN.Read()
        Dim GN As Integer
        GN = CInt(valtblGN("GN")) + 1
        valtblGN.Close()

        Dim rsCtry As SqliteDataReader
        Dim stmt2 As SqliteCommand = dbR.CreateCommand
        Dim stmt3 As SqliteCommand = dbR.CreateCommand
        Dim stmt4 As SqliteCommand = dbR.CreateCommand

        'jrs 2022/10/27 for some reason BeginTransaction avoids saving the results
        'Using trn = dbR.BeginTransaction()
        Using stmt = dbR.CreateCommand()
            Dim sIns As String = "INSERT INTO [RegionIDNewFull] (RegionName, RegionNum) VALUES($RegionName, $RegionNum)"
            AddParameter(stmt, "$RegionName", SqliteType.Text)
            AddParameter(stmt, "$RegionNum", SqliteType.Integer)
            stmt.CommandText = sIns

            Dim sUpd2 As String = "UPDATE RegionalizationFull SET NewRegion = $NewRegion WHERE Country = $Country"
            AddParameter(stmt2, "$NewRegion", SqliteType.Integer)
            AddParameter(stmt2, "$Country", SqliteType.Text)
            stmt2.CommandText = sUpd2

            sIns = "INSERT INTO [GroupIDNewFull] (GroupName, GroupNum, LongGroupName, Alias, Core) VALUES($GroupName, $GroupNum, $LongGroupName, $Alias, 1)"
            AddParameter(stmt3, "$GroupName", SqliteType.Text)
            AddParameter(stmt3, "$GroupNum", SqliteType.Integer)
            AddParameter(stmt3, "$LongGroupName", SqliteType.Text)
            AddParameter(stmt3, "$Alias", SqliteType.Text)
            stmt3.CommandText = sIns

            sIns = "INSERT INTO [GroupingFull] (Region, RegionNo, GroupNo) VALUES($Region, $RegionNo, $GroupNo)"
            AddParameter(stmt4, "$Region", SqliteType.Text)
            AddParameter(stmt4, "$RegionNo", SqliteType.Integer)
            AddParameter(stmt4, "$GroupNo", SqliteType.Integer)
            stmt4.CommandText = sIns

            'dbRCmd.Transaction = trn

            While rsReg.Read
                stmt.Parameters(0).Value = rsReg("Region")
                stmt.Parameters(1).Value = RCount + 1
                stmt.ExecuteNonQuery()

                'Read Countries in each Region
                dbProvCmdCtry.CommandText = "SELECT * FROM Regionalization WHERE Region = """ & rsReg("Region").ToString & """"
                rsCtry = dbProvCmdCtry.ExecuteReader
                While rsCtry.Read
                    'Updating Regionalization
                    stmt2.Parameters(0).Value = RCount + 1
                    stmt2.Parameters(1).Value = rsCtry("Province").ToString
                    stmt2.ExecuteNonQuery()
                End While
                rsCtry.Close()

                If CInt(rs.Rows(0)("AllProvinces")) = 1 Then
                    If bFirst Then
                        bFirst = False
                        'Add a new GroupId
                        stmt3.Parameters(0).Value = getRegionFromCtry(rs.Rows(0)("Country").ToString, PutDataFileDir) 'jrs 2014/11/06 added conversion to region
                        stmt3.Parameters(1).Value = GN
                        stmt3.Parameters(2).Value = rs.Rows(0)("Country").ToString
                        'jrs 2012/05/17
                        stmt3.Parameters(3).Value = rs.Rows(0)("Country").ToString

                        stmt3.ExecuteNonQuery()
                        'if using ALL provinces
                        'remove the country from regions
                        dbRCmd.CommandText = "DELETE FROM [RegionalizationFull] WHERE Country = """ & rs.Rows(0)("Country").ToString & """"
                        dbRCmd.ExecuteNonQuery()

                        'jrs 2022/10/24 assuming Region Names = Country Names when not using provinces
                        'Dim RegId As Integer
                        'RegId = IFsRegs.Item(getRegionFromCtry(rs.Fields("P.Country"))).Index 'jrs 2014/10/23 added conversion to region

                        dbRCmd.CommandText = "DELETE FROM [RegionIDNewFull] WHERE RegionName = """ & rs.Rows(0)("Country").ToString & """"
                        dbRCmd.ExecuteNonQuery()
                    End If
                    'Add Grouping Full
                    stmt4.Parameters(0).Value = rsReg("Region").ToString
                    stmt4.Parameters(1).Value = RCount + 1
                    stmt4.Parameters(2).Value = GN

                    stmt4.ExecuteNonQuery()

                End If

                RCount = RCount + 1
            End While
        End Using
        'End Using

        'Alphabetize the new regions
        Alphabetize(dbR)

        Dim sReg As String
        sReg = getRegionFromCtry(sCountry, PutDataFileDir)

        'if using ALL provinces
        'remove the country from groups
        'and add all of its regions to the same groups
        dbRCmd = dbR.CreateCommand
        If Not bFirst Then
            Dim sSQL As String
            sSQL = "CREATE TABLE TMP1 AS SELECT * FROM GroupingFull WHERE Region = """ + sReg + """" 'jrs 2014/11/06 converted to regions
            dbRCmd.CommandText = sSQL
            dbRCmd.ExecuteNonQuery()
            sSQL = "DELETE FROM GroupingFull WHERE Region = """ + sReg + """" 'jrs 2014/11/06 converted to regions
            dbRCmd.CommandText = sSQL
            dbRCmd.ExecuteNonQuery()
            sSQL = "INSERT INTO GroupingFull SELECT G.Region, G.RegionNo, T.GroupNo FROM TMP1 T, " +
            "GroupingFull G WHERE G.GroupNo = " + GN.ToString
            dbRCmd.CommandText = sSQL
            dbRCmd.ExecuteNonQuery()
            dbR.Close()
            dbR.Open()
            dbRCmd = dbR.CreateCommand
            dbRCmd.CommandText = "DROP TABLE TMP1"
            dbRCmd.ExecuteNonQuery()
        End If

        'Update Grouping Table with new RegionIDs
        Dim sUpd As String
        sUpd = "CREATE TABLE TMP1 AS SELECT G.Region, R.RegionNum as RegionNo, G.GroupNo " +
        "FROM GroupingFull as G INNER JOIN RegionIDNewFull as R ON G.Region = R.RegionName"
        dbRCmd.CommandText = sUpd
        dbRCmd.ExecuteNonQuery()
        sUpd = "DELETE FROM GroupingFull"
        dbRCmd.CommandText = sUpd
        dbRCmd.ExecuteNonQuery()
        sUpd = "INSERT INTO GroupingFull SELECT * FROM TMP1"
        dbRCmd.CommandText = sUpd
        dbRCmd.ExecuteNonQuery()
        sUpd = "DROP TABLE TMP1"
        dbRCmd.CommandText = sUpd
        dbRCmd.ExecuteNonQuery()

        'Adding G-List
        Dim rsGLID As SqliteDataReader
        dbRCmd.CommandText = "SELECT MAX(Code) as MaxCode FROM GListId"
        rsGLID = dbRCmd.ExecuteReader
        'jrs 2026/03/03
        rsGLID.Read()

        Dim Newid As Integer
        Newid = CInt(rsGLID("MaxCode")) + 1
        rsGLID.Close()
        'Create the header
        dbRCmd.CommandText = "INSERT INTO GListId VALUES(" & CStr(Newid) & ",'" & sCountry & " SubRegions')"
        dbRCmd.ExecuteNonQuery()
        'jrs 2016/06/17
        Dim ColorIdx As Integer
        ColorIdx = 0
        'Create the detail
        rsReg.Close()
        rsReg = dbProvCmdReg.ExecuteReader

        'Using trn = dbR.BeginTransaction()
        Using stmt = dbR.CreateCommand()
            Dim sIns As String = "INSERT INTO GList (GListCode, Region, [Group], Decomposable, MemberOrder, LineFeed, GroupTitle, ColorValue, LightColorValue) VALUES(" &
            CStr(Newid) & ",$Region, 0, 0, 0, 0, '', 0, 0)"
            AddParameter(stmt, "$Region", SqliteType.Text)
            stmt.CommandText = sIns
            While rsReg.Read
                'jam 2009/04/28 added color fields
                stmt.Parameters(0).Value = rsReg("Region")
                stmt.ExecuteNonQuery()

                'ColorIdx = ColorIdx + 1
            End While
        End Using
        'End Using


        'jrs 2007/04/27
        'If using all provinces
        'Update G-Lists using Country as Group
        If Not bFirst Then
            dbRCmd.CommandText = "UPDATE GList SET [Group] = 1 WHERE Region = """ & getRegionFromCtry(sCountry, PutDataFileDir) & """"
            dbRCmd.ExecuteNonQuery()
        End If

        dbR.Close()

        dbProv.Close()
    End Sub

    'jrs 2022/10/18
    Private Sub AddProvincesMult(ByRef db As String, ByRef rsProv As DataTable)
        '-------------------------------------------------------------------
        'JRS 7/25/02 IF file doesn't exist copy it from DATA\

        ''mti 2026/03/11 logging start
        Debug.WriteLine("Provinces Split | Start AddProvincesMult | db=" & db)
        'mti 2026/03/11 logging end

        Dim InFile$ = PutDataFileDir & db & ".db"
        Dim OutFile$ = PutFdrvRun & db & ".db"
        FileCopy_NotThere(InFile$, OutFile$)
        If OutFile$ = "" Then Exit Sub
        'Connect to the Database
        Dim dbP As SqliteConnection
        dbP = New SqliteConnection("DataSource=" & PutFdrvRun & db & ".db;")
        dbP.Open()

        Dim rsP As SqliteDataReader
        Dim dbPCmd, dbPCmdrsP, dbPCmdrsP2, dbPCmdrsP3, dbPCmdrsP5 As SqliteCommand
        dbPCmd = dbP.CreateCommand
        dbPCmdrsP = dbP.CreateCommand
        dbPCmdrsP2 = dbP.CreateCommand
        dbPCmdrsP3 = dbP.CreateCommand
        dbPCmdrsP5 = dbP.CreateCommand

        'jrs 2022/11/04
        Using cmd2 = dbP.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        Dim bCountryFile As Boolean
        Dim sFileName As String
        Dim rsProvIdx As Integer = 0
        sFileName = PutFdrvRun & db & rsProv.Rows(rsProvIdx)("Country").ToString & ".db"
        bCountryFile = myFileCheck(sFileName)

        'mti 2026/03/11 logging start
        'Debug.WriteLine("Provinces Split Issue Logging | Post country file copy check | db=" & db &
        '                          " | Country=" & rsProv.Rows(rsProvIdx)("Country").ToString &
        '                          " | sFileName=" & sFileName &
        '                          " | bCountryFile=" & bCountryFile.ToString())
        'mti 2026/03/11 logging end

        'jrs 2009/04/17
        'For other files, should copy from DATA to RUNFILES
        If Not (db.ToUpper = "IFSHISTSERIES") Then
            InFile$ = PutDataFileDir & db & rsProv.Rows(rsProvIdx)("Country").ToString & ".db"
            If myFileCheck(InFile) Then
                FileCopyCheck(InFile$, sFileName)
                bCountryFile = myFileCheck(sFileName)
            End If
        End If

        'jrs 2012/06/28
        Dim bCurrency As Boolean
        'jrs 2026/03/04
        Dim bCurrencyFirst As Boolean = True
        'jrs 2022/10/18
        Dim TryResult As Integer

        Dim dbP2 As SqliteConnection
        If bCountryFile Then
            dbP2 = New SqliteConnection("DataSource=" & sFileName & ";")
            dbP2.Open()
            'jrs 2022/11/04
            Using cmd2 = dbP2.CreateCommand()
                cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                cmd2.ExecuteNonQuery()
            End Using
        End If

        'jrs 2016/02/19
        Dim dbPH As SqliteConnection, dbP2H As SqliteConnection
        Dim dbPHCmdrsP3, dbPHCmdrsP5, dbP2HCmdrsP4, dbP2HCmdrsP6 As SqliteCommand
        Dim rsP5 As SqliteDataReader, rsP6 As SqliteDataReader
        Dim bMigMales As Boolean, bMigFemales As Boolean
        If db.ToUpper = "IFS" Then
            dbPH = New SqliteConnection("DataSource=" & PutFdrvRun & "ifsHistSeries.db;")
            dbPH.Open()
            dbPHCmdrsP3 = dbPH.CreateCommand
            dbPHCmdrsP5 = dbPH.CreateCommand

            If myFileCheck(PutFdrvRun & "ifsHistSeries" & rsProv.Rows(rsProvIdx)("Country").ToString & ".db") Then
                dbP2H = New SqliteConnection("DataSource=" & PutFdrvRun & "ifsHistSeries" & rsProv.Rows(rsProvIdx)("Country").ToString & ".db;")
                dbP2H.Open()
                dbP2HCmdrsP4 = dbP2H.CreateCommand
                dbP2HCmdrsP6 = dbP2H.CreateCommand
            End If
        End If

        Dim bTable As Boolean
        Dim rsP2 As SqliteDataReader
        Dim bUseAltEst As Boolean
        'jrs 2007/01/16
        Dim sDisAgg As String
        Dim rsP3 As SqliteDataReader, rsP4 As SqliteDataReader
        Dim rsP4Name As String
        Dim dbP2CmdrsP4 As SqliteCommand
        If bCountryFile Then
            dbP2CmdrsP4 = dbP2.CreateCommand
        End If
        'jrs 2007/12/18
        Dim dbAE As SqliteConnection
        dbAE = New SqliteConnection("DataSource=" & PutFdrvRun & "DataDict.db")
        dbAE.Open()

        'Check all tables of the Database
        Dim rddbPTblDefs As SqliteDataReader
        Dim dbPCmdTblDefs As SqliteCommand
        dbPCmdTblDefs = dbP.CreateCommand
        'Equivalent to reading from TableDefs
        dbPCmdTblDefs.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%' ORDER BY UPPER(name)"
        rddbPTblDefs = dbPCmdTblDefs.ExecuteReader
        Dim dbPCCmdTDs As SqliteCommand
        dbPCCmdTDs = dbP.CreateCommand
        Dim rdTDs As SqliteDataReader
        'jrs 2022/11/16
        Dim tblCount As Integer = 0

        While rddbPTblDefs.Read
            'If rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then Stop
            'If k = 1176 Then Stop
            'jrs 2012/02/16
            'Exceptions
            If db.ToUpper = "IFS" Then
                If rddbPTblDefs("TABLE_NAME").ToString = "Country Google" Then
                    If Not rddbPTblDefs.Read() Then
                        Exit While
                    End If
                End If
            End If
            Dim bAdd = False
            Dim bAddFipsCD = False
            sDisAgg = "" 'jrs 2007/01/16
            dbPCCmdTDs.CommandText = "SELECT * FROM [" & rddbPTblDefs("TABLE_NAME").ToString & "]"
            rdTDs = dbPCCmdTDs.ExecuteReader
            For l = 0 To rdTDs.FieldCount - 1
                Select Case rdTDs.GetName(l).ToUpper 'jrs 2008/01/07 added ucase
                    Case "COUNTRY"
                        bAdd = True
                        If bAddFipsCD Then Exit For
                    Case "FIPS_CODE"
                        bAddFipsCD = True
                        If bAdd Then Exit For
                End Select
            Next l
            If bAdd Then
                'If rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then Stop
                'jrs 2007/02/13
                'lblProgress.Caption = "Adding Table: " & db & "." & rddbPTblDefs("TABLE_NAME")

                'mti 2026/03/11 logging start
                'Debug.WriteLine("Provinces Split Issue Logging | Processing table | db=" & db &
                '                  " | Table=" & rddbPTblDefs("TABLE_NAME").ToString)

                If db = "IFsHistSeries" Then
                    Debug.WriteLine("Provinces Split Issue Logging | START table | db=" & db &
                        " | Table=" & rddbPTblDefs("TABLE_NAME").ToString)
                End If
                'mti 2026/03/11 logging end

                tblCount = tblCount + 1
                Dim sLine As String = "Adding Table: " & db & "." & rddbPTblDefs("TABLE_NAME").ToString
                'jrs 2022/12/07 adding Try
                Try
                    Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                    objWriter.WriteLine(sLine)
                    objWriter.Close()
                Catch ex As Exception
                    'Do nothing
                End Try

                'DoEvents
                'If rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then Stop
                rsProvIdx = 0
                dbPCmdrsP.CommandText = "SELECT * FROM [" + rddbPTblDefs("TABLE_NAME").ToString + "] ORDER BY Country"
                rsP = dbPCmdrsP.ExecuteReader
                'jrs 2022/11/04
                Dim stmt As SqliteCommand = dbP.CreateCommand()
                Dim bFirstStmt As Boolean = True
                Dim rT As Integer
                'jrs 2017/09/25 moved out to avoid repeating message for every province
                'jrs 2017/08/18
                Dim bSkipMsgDissAgg As Boolean
                bSkipMsgDissAgg = False
                Do While rsProvIdx < rsProv.Rows.Count
                    If bCountryFile Then
                        bTable = myTablecheck(dbP2, rddbPTblDefs("TABLE_NAME").ToString, rsP2, rsProv.Rows(rsProvIdx)("Province").ToString, dbP, rsP, dbPCmdrsP)
                    Else
                        bTable = False
                    End If
                    If bTable Then
                        bUseAltEst = False
                        'mti 2026/03/11 logging
                        'Debug.WriteLine("Provinces Split Issue Logging | Existing province table/row found, skipping alternative estimation | db=" & db &
                        '          " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                        '          " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString)
                    Else
                        bUseAltEst = AlternativeEstimation(db, rddbPTblDefs("TABLE_NAME").ToString, sDisAgg, dbAE)
                        'mti 2026/03/11 logging  SeriesWaterAnRenResourcesOld
                        'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesWaterAnRenResourcesOld" AndAlso rsProv.Rows(rsProvIdx)("Province").ToString = "Alabama" Then
                        '    Debug.WriteLine("Provinces Split Issue Logging | Branch decision | db=" & db &
                        '          " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                        '          " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                        '          " | Country=" & rsProv.Rows(rsProvIdx)("Country").ToString &
                        '          " | bTable=" & bTable.ToString() &
                        '          " | bUseAltEst=" & bUseAltEst.ToString() &
                        '          " | sDisAgg=" & If(String.IsNullOrEmpty(sDisAgg), "<blank>", sDisAgg))
                        '    Stop
                        'End If
                    End If



                    If bUseAltEst Then
                        dbPCmdrsP2.CommandText = "SELECT * FROM [" + rddbPTblDefs("TABLE_NAME").ToString + "] WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                        rsP2 = dbPCmdrsP2.ExecuteReader

                        'mti 2026/03/11 logging
                        'Debug.WriteLine("Provinces Split Issue Logging | Alternative estimation active | db=" & db &
                        '          " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                        '          " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                        '          " | CountryRowHasRows=" & rsP2.HasRows.ToString() &
                        '          " | sDisAgg=" & If(String.IsNullOrEmpty(sDisAgg), "<blank>", sDisAgg))

                        'jrs 2007/01/16 using dbP2 in case Province data hasn't been loaded yet
                        If sDisAgg = "GDP" Then
                            'jrs 2024/05/14 replacing GDP2011 with GDP2017
                            'jrs 2012/09/14 replacing GDP2000 with GDP2011
                            'mti 2026/03/11 start
                            'dbPCmdrsP3.CommandText = "SELECT * FROM SeriesGDP2017 WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                            'rsP3 = dbPCmdrsP3.ExecuteReader
                            'dbP2CmdrsP4.CommandText = "SELECT * FROM SeriesGDP2017 WHERE Country = """ & rsProv.Rows(rsProvIdx)("Province").ToString & """"
                            'rsP4 = dbP2CmdrsP4.ExecuteReader
                            'rsP4Name = "SeriesGDP2017"

                            dbPCmdrsP3.CommandText = "SELECT * FROM SeriesGDP" & ConstantDollarsYear.ToString & " WHERE Country = '" & rsProv.Rows(rsProvIdx)("Country").ToString & "'"
                            rsP3 = dbPCmdrsP3.ExecuteReader
                            dbP2CmdrsP4.CommandText = "SELECT * FROM SeriesGDP" & ConstantDollarsYear.ToString & " WHERE Country = '" & rsProv.Rows(rsProvIdx)("Province").ToString & "'"
                            rsP4 = dbP2CmdrsP4.ExecuteReader
                            rsP4Name = "SeriesGDP2021"
                            'mti 2026/03/11 end

                            'mti 2026/03/11 logging
                            'Debug.WriteLine("Provinces Split Issue Logging | Entered GDP branch | db=" & db &
                            '      " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                            '      " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                            '      " | CountryGDPHasRows=" & rsP3.HasRows.ToString() &
                            '      " | ProvinceGDPHasRows=" & rsP4.HasRows.ToString() &
                            '      " | AuxTable=" & rsP4Name)

                            Dim bCountryGDPRow As Boolean = rsP3.Read()
                            Dim bProvGDPRow As Boolean = rsP4.Read()

                        ElseIf sDisAgg = "POP" Then
                            'mti 2026/03/11 start

                            'dbPCmdrsP3.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                            'rsP3 = dbPCmdrsP3.ExecuteReader
                            'dbP2CmdrsP4.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsProv.Rows(rsProvIdx)("Province").ToString & """"
                            'rsP4 = dbP2CmdrsP4.ExecuteReader
                            'rsP4Name = "SeriesPopulation"

                            dbPCmdrsP3.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = '" & rsProv.Rows(rsProvIdx)("Country").ToString & "'"
                            rsP3 = dbPCmdrsP3.ExecuteReader
                            dbP2CmdrsP4.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = '" & rsProv.Rows(rsProvIdx)("Province").ToString & "'"
                            rsP4 = dbP2CmdrsP4.ExecuteReader
                            rsP4Name = "SeriesPopulation"

                            Dim bCountryPOPRow As Boolean = rsP3.Read()
                            Dim bProvPOPRow As Boolean = rsP4.Read()

                            'mti 2026/03/11 end

                            'mti 2026/03/11 logging
                            'Debug.WriteLine("Provinces Split Issue Logging | Entered POP branch | db=" & db &
                            '      " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                            '      " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                            '      " | CountryPOPHasRows=" & rsP3.HasRows.ToString() &
                            '      " | ProvincePOPHasRows=" & rsP4.HasRows.ToString() &
                            '      " | AuxTable=" & rsP4Name
                            '
                            'If rddbPTblDefs("TABLE_NAME").ToString = "SeriesWaterAnRenResourcesOld" Then
                            'Debug.WriteLine("Provinces Split Issue Logging | Entered POP branch | Table=" &
                            '                rddbPTblDefs("TABLE_NAME").ToString &
                            '                " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                            '                " | CountryGDPRow=" & bCountryPOPRow.ToString() &
                            '                " | ProvinceGDPRow=" & bProvPOPRow.ToString())

                            '    If bCountryPOPRow AndAlso bProvPOPRow AndAlso rsProv.Rows(rsProvIdx)("Province").ToString = "Alabama" Then
                            '        Debug.WriteLine("Provinces Split Issue Logging | POP sample values | Province=Alabama" &
                            '                    " | EarliestProv=" & rsP4("Earliest").ToString() &
                            '                    " | EarliestCountry=" & rsP3("Earliest").ToString() &
                            '                    " | MostRecentProv=" & rsP4("MostRecent").ToString() &
                            '                    " | MostRecentCountry=" & rsP3("MostRecent").ToString())
                            '    End If
                            'End If
                            'mti 2026/03/11 logging end

                        ElseIf rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then 'jrs 2016/02/19
                            'First check if there's cohort by provinces, if not simply use country population
                            dbPCmdrsP3.CommandText = "SELECT * FROM PopAgeCohortCountryFemale WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                            rsP3 = dbPCmdrsP3.ExecuteReader
                            bMigFemales = myTablecheck(dbP2, "PopAgeCohortCountryFemale", rsP4, rsProv.Rows(rsProvIdx)("Province").ToString, dbP, rsP3, dbPCmdrsP3)
                            'jrs 2022/11/03
                            Dim brsP4 As Boolean
                            'jrs 2026/03/02
                            If IsNothing(rsP4) Then
                                brsP4 = False
                            Else
                                brsP4 = rsP4.HasRows
                            End If

                            If Not bMigFemales Then
                                'Open the right database, these are ifs.mdb and ifsXXX.mdb
                                dbPHCmdrsP3.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                                If Not rsP3 Is Nothing Then rsP3.Close()
                                rsP3 = dbPHCmdrsP3.ExecuteReader
                                dbP2HCmdrsP4.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsProv.Rows(rsProvIdx)("Province").ToString & """"
                                If Not rsP4 Is Nothing Then rsP4.Close()
                                rsP4 = dbP2HCmdrsP4.ExecuteReader
                                rsP4Name = "SeriesPopulation"
                            End If
                            dbPCmdrsP5.CommandText = "SELECT * FROM PopAgeCohortCountryMale WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                            rsP5 = dbPCmdrsP5.ExecuteReader
                            'jrs 2026/03/03
                            rsP5.Read()

                            bMigMales = myTablecheck(dbP2, "PopAgeCohortCountryMale", rsP6, rsProv.Rows(rsProvIdx)("Province").ToString, dbP, rsP5, dbPCmdrsP5)
                            If Not bMigMales Then
                                'Open the right database, these are ifs.mdb and ifsXXX.mdb
                                dbPHCmdrsP5.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                                If Not rsP5 Is Nothing Then rsP5.Close()
                                rsP5 = dbPHCmdrsP5.ExecuteReader
                                'jrs 2026/03/03
                                rsP5.Read()

                                dbP2HCmdrsP6.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsProv.Rows(rsProvIdx)("Province").ToString & """"
                                If Not rsP6 Is Nothing Then rsP6.Close()
                                rsP6 = dbP2HCmdrsP6.ExecuteReader
                                'jrs 2026/03/03
                                rsP6.Read()
                            End If
                            'jrs 2022/11/03
                            If brsP4 And rsP4.IsClosed Then
                                'need to reload rsP4, because connection was closed and re-open to make it faster
                                dbP2CmdrsP4.CommandText = "SELECT * FROM [PopAgeCohortCountryFemale] WHERE Country = """ & rsProv.Rows(rsProvIdx)("Province").ToString & """"
                                rsP4 = dbP2CmdrsP4.ExecuteReader
                            End If
                        End If
                    End If
                    'jrs 2022/10/11 added and Not rsP.IsClosed
                    'jrs 2008/01/07 added if rsP.recordcount > 0 then
                    If rsP.HasRows And Not rsP.IsClosed Then
                        While rsP.Read()
                        End While
                    ElseIf rsP.IsClosed Then
                        'jrs 2022/10/11
                        rsP = dbPCmdrsP.ExecuteReader
                    End If
                    'jrs 2012/10/16
                    Dim f As Integer
                    Dim m As Integer
                    If bTable Or bUseAltEst Then
                        'jrs 2022/11/04 transactions are not saving results
                        'jrs 2022/10/13
                        'Using trn = dbP.BeginTransaction()
                        Using stmt 'jrs 2022/11/04 = dbP.CreateCommand()
                            'If rddbPTblDefs("TABLE_NAME").ToString = "Copy Of HealthDetailedDeathsCtryPre20180310" Then Stop
                            'jrs 2022/11/04
                            If bFirstStmt Then
                                Dim sIns As String = "INSERT INTO [" & rddbPTblDefs("TABLE_NAME").ToString & "] (" & rsP.GetName(0)
                                Dim sVals As String = ") VALUES($" & rsP.GetName(0)
                                If Integer.TryParse(rsP.GetName(0), TryResult) Then
                                    AddParameter(stmt, "$" & rsP.GetName(0), SqliteType.Real)
                                Else
                                    AddParameter(stmt, "$" & rsP.GetName(0), SqliteType.Text)
                                End If

                                rT = rsP.FieldCount - 1
                                For f = 1 To rT
                                    sIns = sIns & ",[" & rsP.GetName(f) & "]"
                                    sVals = sVals & ", $" & rsP.GetName(f).Replace(" ", "_").Replace("(", "").Replace(")", "").Replace("-", "_").Replace("/", "_")
                                    If Integer.TryParse(rsP.GetName(f), TryResult) Then
                                        AddParameter(stmt, "$" & rsP.GetName(f), SqliteType.Real)
                                    Else
                                        AddParameter(stmt, "$" & rsP.GetName(f).Replace(" ", "_").Replace("(", "").Replace(")", "").Replace("-", "_").Replace("/", "_"), SqliteType.Text)
                                    End If
                                Next
                                sVals = sVals & ")"
                                stmt.CommandText = sIns & sVals
                                stmt.Prepare()
                                bFirstStmt = False
                            End If


                            While rsP2.Read
                                'we're assuming all fields in target table are already in province table
                                Dim MaleValsPop, FemaleValsPop As Single
                                Dim MaleValsMig, FemaleValsMig As Single

                                For f = 0 To rT
                                    'If rsP.GetName(f) = "Earliest" And rddbPTblDefs("TABLE_NAME").ToString = "SeriesAgBovineMeatProductionFAO" Then Stop
                                    'JRS 04/09/03 If Integer.TryParse(rs.Fields(f).Name) Then
                                    If rsP.GetName(f) = "Country" Then
                                        stmt.Parameters(f).Value = rsProv.Rows(rsProvIdx)("Province")
                                    ElseIf rsP.GetName(f) = "FIPS_CODE" Then
                                        stmt.Parameters(f).Value = rsProv.Rows(rsProvIdx)("FIPS_CODE")
                                    ElseIf rsP.GetName(f) = "Earliest" Then
                                        stmt.Parameters(f).Value = stmt.Parameters(2).Value
                                        For m = 2 To rsP.FieldCount - 2
                                            If Integer.TryParse(rsP.GetName(m), TryResult) AndAlso stmt.Parameters(m).Value.ToString > "" Then
                                                stmt.Parameters(f).Value = stmt.Parameters(m).Value
                                                Exit For
                                            End If
                                        Next m
                                    ElseIf rsP.GetName(f) = "MostRecent" Then
                                        stmt.Parameters(f).Value = stmt.Parameters(2).Value
                                        For m = rsP.FieldCount - 3 To 2 Step -1
                                            If Integer.TryParse(rsP.GetName(m), TryResult) AndAlso stmt.Parameters(m).Value.ToString > "" Then
                                                stmt.Parameters(f).Value = stmt.Parameters(m).Value
                                                Exit For
                                            End If
                                        Next m
                                    Else
                                        If rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then 'jrs 2016/02/19
                                            Select Case rsP.GetName(f)
                                                Case "Year", "NCohorts", "MigPctPopMales", "MigPctPopFemales", "MigPctPopTotal"
                                                    stmt.Parameters(f).Value = rsP2(rsP.GetName(f))
                                                Case "MigrantsMales", "MigrantsFemales", "PopMales", "PopFemales"
                                                    If (bMigMales And (rsP.GetName(f) = "MigrantsMales" Or rsP.GetName(f) = "PopMales")) Or
                                                            (bMigFemales And (rsP.GetName(f) = "MigrantsFemales" Or rsP.GetName(f) = "PopFemales")) Then
                                                        Select Case rsP.GetName(f)
                                                            Case "MigrantsMales"
                                                                MaleValsMig = CSng(rsP2(rsP.GetName(f))) * getCohortVal(rsP2("NCohorts").ToString, rsP4) / getCohortVal(rsP2("NCohorts").ToString, rsP3)
                                                                stmt.Parameters(f).Value = MaleValsMig
                                                            Case "MigrantsFemales"
                                                                FemaleValsMig = CSng(rsP2(rsP.GetName(f))) * getCohortVal(rsP2("NCohorts").ToString, rsP4) / getCohortVal(rsP2("NCohorts").ToString, rsP3)
                                                                stmt.Parameters(f).Value = FemaleValsMig
                                                            Case "PopMales"
                                                                MaleValsPop = CSng(rsP2(rsP.GetName(f))) * getCohortVal(rsP2("NCohorts").ToString, rsP4) / getCohortVal(rsP2("NCohorts").ToString, rsP3)
                                                                stmt.Parameters(f).Value = MaleValsPop
                                                            Case "PopFemales"
                                                                FemaleValsPop = CSng(rsP2(rsP.GetName(f))) * getCohortVal(rsP2("NCohorts").ToString, rsP4) / getCohortVal(rsP2("NCohorts").ToString, rsP3)
                                                                stmt.Parameters(f).Value = FemaleValsPop
                                                        End Select
                                                    Else
                                                        stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP6(CStr(BaseYear))) / CSng(rsP5(CStr(BaseYear)))
                                                    End If
                                                Case "MigrantsTotal", "PopTotal"
                                                    Select Case rsP.GetName(f)
                                                        Case "MigrantsTotal"
                                                            stmt.Parameters(f).Value = MaleValsMig + FemaleValsMig
                                                        Case "PopTotal"
                                                            stmt.Parameters(f).Value = MaleValsPop + FemaleValsPop
                                                    End Select
                                                Case Else
                                                    'jrs 2022/10/13
                                                    stmt.Parameters(f).Value = DBNull.Value
                                            End Select
                                        ElseIf sDisAgg = "" Then 'jrs 2007/01/16  this is true always when adding data from Table, since we don't do Alt. Estimation

                                            'mti 2026/03/11 logging
                                            'If f = 2 Then
                                            '    Debug.WriteLine("Provinces Split Issue Logging | sDisAgg blank, copying country values | db=" & db &
                                            '                        " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                            '                        " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString)
                                            'End If
                                            'mti 2026/03/11 logging end

                                            'jrs 2014/11/05
                                            If rsP.GetName(f) = "UN Pardee Countries" Then
                                                stmt.Parameters(f).Value = rsProv.Rows(rsProvIdx)("Province")
                                            Else
                                                stmt.Parameters(f).Value = rsP2(rsP.GetName(f))
                                            End If
                                            'jrs 2012/06/28
                                            If rddbPTblDefs("TABLE_NAME").ToString = "Currency" And rsP.GetName(f) = "Currency Flag" Then
                                                'jrs 2026/03/02 needs to read from rsP2 which is reading from country
                                                If rsP2("Currency Flag").ToString > "" AndAlso CInt(rsP2("Currency Flag")) = 1 Then
                                                    bCurrency = True
                                                    'jrs 2026/03/04
                                                    'Only the first state should be marked as currency country
                                                    If Not bCurrencyFirst Then
                                                        stmt.Parameters(f).Value = 0
                                                    End If
                                                    bCurrencyFirst = False
                                                End If
                                            End If
                                        Else 'use proportions of country for estimation

                                            'mti 2026/03/11 code change start
                                            'If CInt(rsP.GetName(f)) < CInt(rsP3.GetName(2)) Then
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("Earliest")) / CSng(rsP3("Earliest"))
                                            'ElseIf CInt(rsP.GetName(f)) > CInt(rsP3.GetName(rsP3.FieldCount - 3)) Then
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("MostRecent")) / CSng(rsP3("MostRecent"))
                                            'ElseIf Integer.TryParse(rsP.GetName(f), TryResult) Then

                                            '    'jrs 2017/09/25 changed to check last 30 to only the last 20 years
                                            '    'jrs 2017/08/18
                                            '    If rsP4(rsP.GetName(f)).ToString > "" And Not bSkipMsgDissAgg And CInt(rsP.GetName(f)) > BaseYear - 20 And CInt(rsP.GetName(f)) <= BaseYear Then
                                            '        sLine = "Auxiliary table " & rsP4Name & " has Nulls in field " & rsP.GetName(f) & ". This will produce nulls for " & rddbPTblDefs("TABLE_NAME").ToString
                                            '        'jrs 2022/12/07
                                            '        Try
                                            '            Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                                            '            objWriter.WriteLine(sLine)
                                            '            objWriter.Close()
                                            '        Catch ex As Exception
                                            '            'Do nothing
                                            '        End Try
                                            '        bSkipMsgDissAgg = True
                                            '    End If
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4(rsP.GetName(f))) / CSng(rsP3(rsP.GetName(f)))
                                            'End If

                                            Dim iFldYear As Integer
                                            Dim iFirstAuxYear As Integer
                                            Dim iLastAuxYear As Integer

                                            If Integer.TryParse(rsP.GetName(f), iFldYear) AndAlso Integer.TryParse(rsP3.GetName(2), iFirstAuxYear) AndAlso
                                                               iFldYear < iFirstAuxYear Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | Earliest fallback branch | db=" & db &
                                                '            " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '            " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '            " | Field=" & rsP.GetName(f))
                                                'End If
                                                'mti 2026/03/11 logging end

                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("Earliest")) / CSng(rsP3("Earliest"))
                                                'nulls can raise exceptions/errors - safe conversion
                                                Dim vTarget As Single
                                                Dim vProvEarliest As Single
                                                Dim vCountryEarliest As Single

                                                If Not IsDBNull(rsP2(rsP.GetName(f))) AndAlso
                                                   Not IsDBNull(rsP4("Earliest")) AndAlso
                                                   Not IsDBNull(rsP3("Earliest")) AndAlso
                                                   Single.TryParse(rsP2(rsP.GetName(f)).ToString(), vTarget) AndAlso
                                                   Single.TryParse(rsP4("Earliest").ToString(), vProvEarliest) AndAlso
                                                   Single.TryParse(rsP3("Earliest").ToString(), vCountryEarliest) AndAlso
                                                   vCountryEarliest <> 0 Then

                                                    stmt.Parameters(f).Value = vTarget * vProvEarliest / vCountryEarliest
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If

                                            ElseIf Integer.TryParse(rsP.GetName(f), iFldYear) AndAlso
                                                   Integer.TryParse(rsP3.GetName(rsP3.FieldCount - 3), iLastAuxYear) AndAlso
                                                   iFldYear > iLastAuxYear Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | MostRecent fallback branch | db=" & db &
                                                '            " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '            " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '            " | Field=" & rsP.GetName(f))
                                                'End If
                                                'mti 2026/03/11 logging end

                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("MostRecent")) / CSng(rsP3("MostRecent"))
                                                Dim vTarget As Single
                                                Dim vProvMostRecent As Single
                                                Dim vCountryMostRecent As Single

                                                If Not IsDBNull(rsP2(rsP.GetName(f))) AndAlso
                                                   Not IsDBNull(rsP4("MostRecent")) AndAlso
                                                   Not IsDBNull(rsP3("MostRecent")) AndAlso
                                                   Single.TryParse(rsP2(rsP.GetName(f)).ToString(), vTarget) AndAlso
                                                   Single.TryParse(rsP4("MostRecent").ToString(), vProvMostRecent) AndAlso
                                                   Single.TryParse(rsP3("MostRecent").ToString(), vCountryMostRecent) AndAlso
                                                   vCountryMostRecent <> 0 Then

                                                    stmt.Parameters(f).Value = vTarget * vProvMostRecent / vCountryMostRecent
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If

                                            ElseIf Integer.TryParse(rsP.GetName(f), iFldYear) Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | Exact-year branch | db=" & db &
                                                '    " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '    " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '    " | Year=" & rsP.GetName(f))

                                                'End If
                                                'mti 2026/03/11 logging end


                                                Dim iFldYear1 As Integer
                                                Dim sFldName As String = rsP.GetName(f)
                                                Dim bNullOrBlank As Boolean = IsDBNull(rsP4(sFldName)) OrElse rsP4(sFldName).ToString().Trim() = ""

                                                If Integer.TryParse(sFldName, iFldYear1) AndAlso
                                                           bNullOrBlank AndAlso
                                                           Not bSkipMsgDissAgg AndAlso
                                                           iFldYear1 > BaseYear - 20 AndAlso
                                                           iFldYear1 <= BaseYear Then

                                                    sLine = "Auxiliary table " & rsP4Name & " has Nulls in field " & sFldName &
                                                                ". This will produce nulls for " & rddbPTblDefs("TABLE_NAME").ToString

                                                    Try
                                                        Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                                                        objWriter.WriteLine(sLine)
                                                        objWriter.Close()
                                                    Catch ex As Exception
                                                        'Do nothing
                                                    End Try

                                                    bSkipMsgDissAgg = True
                                                End If

                                                'write Null safe version   
                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4(rsP.GetName(f))) / CSng(rsP3(rsP.GetName(f)))
                                                Dim vTarget As Single
                                                Dim vProvYear As Single
                                                Dim vCountryYear As Single
                                                'Dim sFldName As String = rsP.GetName(f)

                                                If Not IsDBNull(rsP2(sFldName)) AndAlso
                                                           Not IsDBNull(rsP4(sFldName)) AndAlso
                                                           Not IsDBNull(rsP3(sFldName)) AndAlso
                                                           Single.TryParse(rsP2(sFldName).ToString(), vTarget) AndAlso
                                                           Single.TryParse(rsP4(sFldName).ToString(), vProvYear) AndAlso
                                                           Single.TryParse(rsP3(sFldName).ToString(), vCountryYear) AndAlso
                                                           vCountryYear <> 0 Then

                                                    'mti 2026/03/11 logging start
                                                    'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                    '    Debug.WriteLine("Provinces Split Issue Logging | Geothermal exact-year values | Province=" &
                                                    '                    rsProv.Rows(rsProvIdx)("Province").ToString &
                                                    '                    " | Year=" & sFldName &
                                                    '                    " | TargetIsNull=" & IsDBNull(rsP2(sFldName)).ToString() &
                                                    '                    " | ProvAuxIsNull=" & IsDBNull(rsP4(sFldName)).ToString() &
                                                    '                    " | CountryAuxIsNull=" & IsDBNull(rsP3(sFldName)).ToString() &
                                                    '                    " | TargetVal=" & If(IsDBNull(rsP2(sFldName)), "<DBNull>", rsP2(sFldName).ToString()) &
                                                    '                    " | ProvAuxVal=" & If(IsDBNull(rsP4(sFldName)), "<DBNull>", rsP4(sFldName).ToString()) &
                                                    '                    " | CountryAuxVal=" & If(IsDBNull(rsP3(sFldName)), "<DBNull>", rsP3(sFldName).ToString()))
                                                    'End If
                                                    'mti 2026/03/11 logging end

                                                    stmt.Parameters(f).Value = vTarget * vProvYear / vCountryYear
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If


                                            End If

                                            'mti 2026/03/11 code change end

                                        End If
                                    End If
                                Next f
                                stmt.ExecuteNonQuery()
                            End While 'rsP2.Read, for each province

                        End Using
                        'End Using
                        'jrs 2022/10/11
                        rsP2.Close()
                        'If rddbPTblDefs("TABLE_NAME").ToString = "Copy Of HealthDetailedDeathsCtryPre20180310" Then Stop
                        'jrs 2022/10/11
                        If Not rsP3 Is Nothing Then rsP3.Close()
                        If Not rsP4 Is Nothing Then rsP4.Close()
                        If Not rsP5 Is Nothing Then rsP5.Close()
                        If Not rsP6 Is Nothing Then rsP6.Close()

                        'jrs 2016/02/19
                        'if it's HistMigration update % of Distribution field per Province
                        If rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then
                            UpdateDistFldxProv(dbP, rsProv.Rows(rsProvIdx)("Province").ToString)
                        End If

                    Else
                        'mti 2026/03/11 logging
                        'Debug.WriteLine("Provinces Split Issue Logging | No province data and no alternative estimation, inserting shell row only | db=" & db &
                        '          " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                        '          " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString)

                        Dim rsPIns As String = "INSERT INTO [" & rddbPTblDefs("TABLE_NAME").ToString & "] (Country"
                        Dim rsPInsVals As String = ") VALUES(""" & rsProv.Rows(rsProvIdx)("Province").ToString & """"

                        If bAddFipsCD Then
                            rsPIns = rsPIns & ", FIPS_CODE"
                            rsPInsVals = rsPInsVals & ", """ & rsProv.Rows(rsProvIdx)("FIPS_CODE").ToString & """"
                        End If

                        'For f = 0 To rsP.FieldCount - 1
                        '    'jrs 2012/06/28 added Ucase
                        '    If rsP.GetSchemaTable.Rows(f)(0) <> "Country" And UCase(rsP.GetSchemaTable.Rows(f)(0)) <> "FIPS_CODE" Then
                        '        'Other variations
                        '        rsP.Fields(f) = Null
                        '    End If
                        'Next f

                        rsPInsVals = rsPInsVals & ")"
                        dbPCmd.CommandText = rsPIns & rsPInsVals
                        dbPCmd.ExecuteNonQuery()
                    End If

                    rsProvIdx = rsProvIdx + 1
                Loop

                'mti 2026/03/11 logging start
                If db = "IFsHistSeries" Then
                    Debug.WriteLine("Provinces Split Issue Logging | END table | db=" & db &
                    " | Table=" & rddbPTblDefs("TABLE_NAME").ToString)
                End If
                'mti 2026/03/11 logging end

                rsP.Close()
                'jrs 2007/01/10
                '            rsProv.MoveLast
                '            If rsProv.Rows(rsProvIdx)("AllProvinces") Then
                '                dbP.Execute ("DELETE * FROM [" + rddbPTblDefs("TABLE_NAME") + "] WHERE Country = '" & rsProv.Rows(rsProvIdx)("P.Country") & "'")
                '            End If


            End If
            'jrs 2022/10/10
            rdTDs.Close()
        End While

        'mti 2026/03/11 logging
        'Debug.WriteLine("Provinces Split Issue Logging | End AddProvincesMult | db=" & db &
        '                          " | TablesProcessed=" & tblCount.ToString())
        dbP.Close()
        'jrs 2016/02/19
        If bCountryFile Then
            dbP2.Close()
        End If
        If db.ToUpper = "IFS" Then
            dbPH.Close()
            dbP2H.Close()
        End If

        'jrs 2007/12/18
        dbAE.Close()
        'jrs 2012/06/28
        If bCurrency Then
            'Update ifsInit
            rsProvIdx = 0

            Dim sIP As String
            If StartDirec.Contains("Code.Ifs-Translation\") Then
                sIP = StartDirec.Replace("Code.Ifs-Translation\", "")
            Else
                ' gvdb 2024/04/05
                ' update path for Net8
                sIP = StartDirec.Replace("net8\", "")
            End If

            'jrs 2004/12/02 remove the need of 2 IfsInit, just leave 1 in root
            sIP = sIP & "IFsInit.db"
            Dim dbCurr As SqliteConnection
            dbCurr = New SqliteConnection("DataSource=" & sIP & ";")
            dbCurr.Open()

            Dim sUpd As String
            sUpd = "UPDATE [" & ModelLoad$ & "] SET [Value] = """ & rsProv.Rows(rsProvIdx)("Province").ToString & """ WHERE [Variable] = 'CurrencyCountry$'"
            Dim dbCurrCmd As SqliteCommand
            dbCurrCmd = dbCurr.CreateCommand
            dbCurrCmd.CommandText = sUpd
            dbCurrCmd.ExecuteNonQuery()
            dbCurr.Close()
        End If
    End Sub

    'jrs 2026/03/20 Bilateral Version
    Private Sub AddProvincesBilateral(ByRef rsProv As DataTable)
        '-------------------------------------------------------------------
        'JRS 7/25/02 IF file doesn't exist copy it from DATA\
        Dim db As String = "IFsHistSeriesDyadic"

        ''mti 2026/03/11 logging start
        Debug.WriteLine("Provinces Split | Start AddProvincesBilateral | db=" & db)
        'mti 2026/03/11 logging end


        Dim InFile$
        Dim OutFile$
        If Dir(PutDataFileDir & "Bilateral\" & db & ".db") > "" Then
            'jrs 2026/03/25 Only if Bilateral is loaded
            Exit Sub
            InFile$ = PutDataFileDir & "Bilateral\" & db & ".db"
            OutFile$ = PutFdrvRun & db & ".db"
        Else
            InFile$ = PutFdrvRun & db & ".db"
            OutFile$ = PutDataFileDir & db & ".db"
        End If


        FileCopy_NotThere(InFile$, OutFile$)
        If OutFile$ = "" Then Exit Sub
        'Connect to the Database
        Dim dbP As SqliteConnection
        dbP = New SqliteConnection("DataSource=" & PutFdrvRun & db & ".db;")
        dbP.Open()

        Dim dbPAux As SqliteConnection
        dbPAux = New SqliteConnection("DataSource=" & PutFdrvRun & "ifsHistSeries.db;")
        dbPAux.Open()

        Dim rsP As SqliteDataReader
        Dim dbPCmd, dbPCmdrsP, dbPCmdrsP2, dbPCmdrsP3, dbPCmdrsP5 As SqliteCommand
        Dim dbPCmdrsP2P As SqliteCommand
        dbPCmd = dbP.CreateCommand
        dbPCmdrsP = dbP.CreateCommand
        dbPCmdrsP2 = dbP.CreateCommand
        dbPCmdrsP2P = dbP.CreateCommand
        dbPCmdrsP3 = dbPAux.CreateCommand
        dbPCmdrsP5 = dbP.CreateCommand

        'jrs 2022/11/04
        Using cmd2 = dbP.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        Dim bCountryFile As Boolean
        Dim sFileName As String
        Dim rsProvIdx As Integer = 0
        sFileName = PutFdrvRun & db & rsProv.Rows(rsProvIdx)("Country").ToString & ".db"
        bCountryFile = myFileCheck(sFileName)

        'mti 2026/03/11 logging start
        'Debug.WriteLine("Provinces Split Issue Logging | Post country file copy check | db=" & db &
        '                          " | Country=" & rsProv.Rows(rsProvIdx)("Country").ToString &
        '                          " | sFileName=" & sFileName &
        '                          " | bCountryFile=" & bCountryFile.ToString())
        'mti 2026/03/11 logging end

        'jrs 2012/06/28
        Dim bCurrency As Boolean
        'jrs 2026/03/04
        Dim bCurrencyFirst As Boolean = True
        'jrs 2022/10/18
        Dim TryResult As Integer

        Dim dbP2 As SqliteConnection
        If bCountryFile Then
            dbP2 = New SqliteConnection("DataSource=" & sFileName & ";")
            dbP2.Open()
            'jrs 2022/11/04
            Using cmd2 = dbP2.CreateCommand()
                cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                cmd2.ExecuteNonQuery()
            End Using
        End If
        'jrs 2026/03/20
        Dim dbP2Aux As SqliteConnection
        dbP2Aux = New SqliteConnection("DataSource=" & PutFdrvRun & "ifsHistSeries" & rsProv.Rows(rsProvIdx)("Country").ToString & ".db" & ";")
        dbP2Aux.Open()

        'jrs 2016/02/19
        Dim dbPH As SqliteConnection, dbP2H As SqliteConnection
        Dim dbPHCmdrsP3, dbPHCmdrsP5, dbP2HCmdrsP4, dbP2HCmdrsP6 As SqliteCommand
        Dim rsP5 As SqliteDataReader, rsP6 As SqliteDataReader
        Dim bMigMales As Boolean, bMigFemales As Boolean

        Dim bTable As Boolean
        Dim rsP2 As SqliteDataReader
        Dim rsP2P As SqliteDataReader
        Dim bUseAltEst As Boolean
        'jrs 2007/01/16
        Dim sDisAgg As String
        Dim rsP3 As SqliteDataReader, rsP4 As SqliteDataReader
        Dim rsP4Name As String
        Dim dbP2CmdrsP4 As SqliteCommand
        'If bCountryFile Then
        dbP2CmdrsP4 = dbP2Aux.CreateCommand
        'End If

        'jrs 2007/12/18
        Dim dbAE As SqliteConnection
        If Dir(PutDataFileDir & "Bilateral\DataDictDyadic.db") > "" Then
            dbAE = New SqliteConnection("DataSource=" & PutDataFileDir & "Bilateral\DataDictDyadic.db")
        Else
            dbAE = New SqliteConnection("DataSource=" & PutDataFileDir & "DataDictDyadic.db")
        End If
        dbAE.Open()

        'Check all tables of the Database
        Dim rddbPTblDefs As SqliteDataReader
        Dim dbPCmdTblDefs As SqliteCommand
        dbPCmdTblDefs = dbP.CreateCommand
        'Equivalent to reading from TableDefs
        dbPCmdTblDefs.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%' ORDER BY UPPER(name)"
        rddbPTblDefs = dbPCmdTblDefs.ExecuteReader
        Dim dbPCCmdTDs As SqliteCommand
        dbPCCmdTDs = dbP.CreateCommand
        Dim rdTDs As SqliteDataReader
        'jrs 2022/11/16
        Dim tblCount As Integer = 0

        While rddbPTblDefs.Read
            'If rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then Stop
            Dim bAdd = True
            Dim bAddFipsCD = True
            sDisAgg = "" 'jrs 2007/01/16
            dbPCCmdTDs.CommandText = "SELECT * FROM [" & rddbPTblDefs("TABLE_NAME").ToString & "]"
            rdTDs = dbPCCmdTDs.ExecuteReader
            If bAdd Then
                'If rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then Stop
                'jrs 2007/02/13
                'lblProgress.Caption = "Adding Table: " & db & "." & rddbPTblDefs("TABLE_NAME")

                'mti 2026/03/11 logging start
                'Debug.WriteLine("Provinces Split Issue Logging | Processing table | db=" & db &
                '                  " | Table=" & rddbPTblDefs("TABLE_NAME").ToString)

                Debug.WriteLine("Provinces Split Issue Logging | START table | db=" & db &
                    " | Table=" & rddbPTblDefs("TABLE_NAME").ToString)
                'mti 2026/03/11 logging end

                tblCount = tblCount + 1
                Dim sLine As String = "Adding Table: " & db & "." & rddbPTblDefs("TABLE_NAME").ToString
                'jrs 2022/12/07 adding Try
                Try
                    Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                    objWriter.WriteLine(sLine)
                    objWriter.Close()
                Catch ex As Exception
                    'Do nothing
                End Try

                'DoEvents
                'If rddbPTblDefs("TABLE_NAME").ToString = "HistMigration" Then Stop
                rsProvIdx = 0
                dbPCmdrsP.CommandText = "SELECT * FROM [" + rddbPTblDefs("TABLE_NAME").ToString + "] ORDER BY Actor, Partner"
                rsP = dbPCmdrsP.ExecuteReader
                'jrs 2022/11/04
                Dim stmt As SqliteCommand = dbP.CreateCommand()
                Dim bFirstStmt As Boolean = True
                Dim rT As Integer
                'jrs 2017/09/25 moved out to avoid repeating message for every province
                'jrs 2017/08/18
                Dim bSkipMsgDissAgg As Boolean
                bSkipMsgDissAgg = False
                Do While rsProvIdx < rsProv.Rows.Count
                    If bCountryFile Then
                        'jrs 2026/03/20
                        'This one will probably need something new to consider actors too
                        bTable = myTablecheck(dbP2, rddbPTblDefs("TABLE_NAME").ToString, rsP2, rsProv.Rows(rsProvIdx)("Province").ToString, dbP, rsP, dbPCmdrsP)
                    Else
                        bTable = False
                    End If
                    If bTable Then
                        bUseAltEst = False
                        'mti 2026/03/11 logging
                        'Debug.WriteLine("Provinces Split Issue Logging | Existing province table/row found, skipping alternative estimation | db=" & db &
                        '          " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                        '          " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString)
                    Else
                        bUseAltEst = AlternativeEstimation(db, rddbPTblDefs("TABLE_NAME").ToString, sDisAgg, dbAE)
                        'mti 2026/03/11 logging  SeriesWaterAnRenResourcesOld
                        'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesWaterAnRenResourcesOld" AndAlso rsProv.Rows(rsProvIdx)("Province").ToString = "Alabama" Then
                        '    Debug.WriteLine("Provinces Split Issue Logging | Branch decision | db=" & db &
                        '          " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                        '          " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                        '          " | Country=" & rsProv.Rows(rsProvIdx)("Country").ToString &
                        '          " | bTable=" & bTable.ToString() &
                        '          " | bUseAltEst=" & bUseAltEst.ToString() &
                        '          " | sDisAgg=" & If(String.IsNullOrEmpty(sDisAgg), "<blank>", sDisAgg))
                        '    Stop
                        'End If
                    End If



                    If bUseAltEst Then
                        dbPCmdrsP2.CommandText = "SELECT * FROM [" + rddbPTblDefs("TABLE_NAME").ToString + "] WHERE Actor = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                        rsP2 = dbPCmdrsP2.ExecuteReader

                        dbPCmdrsP2P.CommandText = "SELECT * FROM [" + rddbPTblDefs("TABLE_NAME").ToString + "] WHERE Partner = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                        rsP2P = dbPCmdrsP2P.ExecuteReader

                        'mti 2026/03/11 logging
                        'Debug.WriteLine("Provinces Split Issue Logging | Alternative estimation active | db=" & db &
                        '          " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                        '          " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                        '          " | CountryRowHasRows=" & rsP2.HasRows.ToString() &
                        '          " | sDisAgg=" & If(String.IsNullOrEmpty(sDisAgg), "<blank>", sDisAgg))

                        'jrs 2007/01/16 using dbP2 in case Province data hasn't been loaded yet
                        If sDisAgg = "GDP" Then
                            'jrs 2024/05/14 replacing GDP2011 with GDP2017
                            'jrs 2012/09/14 replacing GDP2000 with GDP2011
                            'mti 2026/03/11 start
                            'dbPCmdrsP3.CommandText = "SELECT * FROM SeriesGDP2017 WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                            'rsP3 = dbPCmdrsP3.ExecuteReader
                            'dbP2CmdrsP4.CommandText = "SELECT * FROM SeriesGDP2017 WHERE Country = """ & rsProv.Rows(rsProvIdx)("Province").ToString & """"
                            'rsP4 = dbP2CmdrsP4.ExecuteReader
                            'rsP4Name = "SeriesGDP2017"

                            dbPCmdrsP3.CommandText = "SELECT * FROM SeriesGDP" & ConstantDollarsYear.ToString & " WHERE Country = '" & rsProv.Rows(rsProvIdx)("Country").ToString & "'"
                            rsP3 = dbPCmdrsP3.ExecuteReader
                            dbP2CmdrsP4.CommandText = "SELECT * FROM SeriesGDP" & ConstantDollarsYear.ToString & " WHERE Country = '" & rsProv.Rows(rsProvIdx)("Province").ToString & "'"
                            rsP4 = dbP2CmdrsP4.ExecuteReader
                            rsP4Name = "SeriesGDP2021"
                            'mti 2026/03/11 end

                            'mti 2026/03/11 logging
                            'Debug.WriteLine("Provinces Split Issue Logging | Entered GDP branch | db=" & db &
                            '      " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                            '      " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                            '      " | CountryGDPHasRows=" & rsP3.HasRows.ToString() &
                            '      " | ProvinceGDPHasRows=" & rsP4.HasRows.ToString() &
                            '      " | AuxTable=" & rsP4Name)

                            Dim bCountryGDPRow As Boolean = rsP3.Read()
                            Dim bProvGDPRow As Boolean = rsP4.Read()

                        ElseIf sDisAgg = "POP" Then
                            'mti 2026/03/11 start

                            'dbPCmdrsP3.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsProv.Rows(rsProvIdx)("Country").ToString & """"
                            'rsP3 = dbPCmdrsP3.ExecuteReader
                            'dbP2CmdrsP4.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = """ & rsProv.Rows(rsProvIdx)("Province").ToString & """"
                            'rsP4 = dbP2CmdrsP4.ExecuteReader
                            'rsP4Name = "SeriesPopulation"

                            dbPCmdrsP3.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = '" & rsProv.Rows(rsProvIdx)("Country").ToString & "'"
                            rsP3 = dbPCmdrsP3.ExecuteReader
                            dbP2CmdrsP4.CommandText = "SELECT * FROM SeriesPopulation WHERE Country = '" & rsProv.Rows(rsProvIdx)("Province").ToString & "'"
                            rsP4 = dbP2CmdrsP4.ExecuteReader
                            rsP4Name = "SeriesPopulation"

                            Dim bCountryPOPRow As Boolean = rsP3.Read()
                            Dim bProvPOPRow As Boolean = rsP4.Read()

                            'mti 2026/03/11 end

                            'mti 2026/03/11 logging
                            'Debug.WriteLine("Provinces Split Issue Logging | Entered POP branch | db=" & db &
                            '      " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                            '      " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                            '      " | CountryPOPHasRows=" & rsP3.HasRows.ToString() &
                            '      " | ProvincePOPHasRows=" & rsP4.HasRows.ToString() &
                            '      " | AuxTable=" & rsP4Name
                            '
                            'If rddbPTblDefs("TABLE_NAME").ToString = "SeriesWaterAnRenResourcesOld" Then
                            'Debug.WriteLine("Provinces Split Issue Logging | Entered POP branch | Table=" &
                            '                rddbPTblDefs("TABLE_NAME").ToString &
                            '                " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                            '                " | CountryGDPRow=" & bCountryPOPRow.ToString() &
                            '                " | ProvinceGDPRow=" & bProvPOPRow.ToString())

                            '    If bCountryPOPRow AndAlso bProvPOPRow AndAlso rsProv.Rows(rsProvIdx)("Province").ToString = "Alabama" Then
                            '        Debug.WriteLine("Provinces Split Issue Logging | POP sample values | Province=Alabama" &
                            '                    " | EarliestProv=" & rsP4("Earliest").ToString() &
                            '                    " | EarliestCountry=" & rsP3("Earliest").ToString() &
                            '                    " | MostRecentProv=" & rsP4("MostRecent").ToString() &
                            '                    " | MostRecentCountry=" & rsP3("MostRecent").ToString())
                            '    End If
                            'End If
                            'mti 2026/03/11 logging end

                        End If
                    End If
                    'jrs 2022/10/11 added and Not rsP.IsClosed
                    'jrs 2008/01/07 added if rsP.recordcount > 0 then
                    If rsP.HasRows And Not rsP.IsClosed Then
                        While rsP.Read()
                        End While
                    ElseIf rsP.IsClosed Then
                        'jrs 2022/10/11
                        rsP = dbPCmdrsP.ExecuteReader
                    End If
                    'jrs 2012/10/16
                    Dim f As Integer
                    Dim m As Integer
                    If bTable Or bUseAltEst Then
                        'jrs 2022/11/04 transactions are not saving results
                        'jrs 2022/10/13
                        'Using trn = dbP.BeginTransaction()
                        Using stmt 'jrs 2022/11/04 = dbP.CreateCommand()
                            'If rddbPTblDefs("TABLE_NAME").ToString = "Copy Of HealthDetailedDeathsCtryPre20180310" Then Stop
                            'jrs 2022/11/04
                            If bFirstStmt Then
                                Dim sIns As String = "INSERT INTO [" & rddbPTblDefs("TABLE_NAME").ToString & "] (" & rsP.GetName(0)
                                Dim sVals As String = ") VALUES($" & rsP.GetName(0)
                                If Integer.TryParse(rsP.GetName(0), TryResult) Then
                                    AddParameter(stmt, "$" & rsP.GetName(0), SqliteType.Real)
                                Else
                                    AddParameter(stmt, "$" & rsP.GetName(0), SqliteType.Text)
                                End If

                                rT = rsP.FieldCount - 1
                                For f = 1 To rT
                                    sIns = sIns & ",[" & rsP.GetName(f) & "]"
                                    sVals = sVals & ", $" & rsP.GetName(f).Replace(" ", "_").Replace("(", "").Replace(")", "").Replace("-", "_").Replace("/", "_")
                                    If Integer.TryParse(rsP.GetName(f), TryResult) Then
                                        AddParameter(stmt, "$" & rsP.GetName(f), SqliteType.Real)
                                    Else
                                        AddParameter(stmt, "$" & rsP.GetName(f).Replace(" ", "_").Replace("(", "").Replace(")", "").Replace("-", "_").Replace("/", "_"), SqliteType.Text)
                                    End If
                                Next
                                sVals = sVals & ")"
                                stmt.CommandText = sIns & sVals
                                stmt.Prepare()
                                bFirstStmt = False
                            End If


                            While rsP2.Read
                                'we're assuming all fields in target table are already in province table
                                For f = 0 To rT
                                    'If rsP.GetName(f) = "Earliest" And rddbPTblDefs("TABLE_NAME").ToString = "SeriesAgBovineMeatProductionFAO" Then Stop
                                    'JRS 04/09/03 If Integer.TryParse(rs.Fields(f).Name) Then
                                    If rsP.GetName(f) = "Actor" Then
                                        stmt.Parameters(f).Value = rsProv.Rows(rsProvIdx)("Province")
                                    ElseIf rsP.GetName(f) = "Actor_FIPS" Then
                                        stmt.Parameters(f).Value = rsProv.Rows(rsProvIdx)("FIPS_CODE")
                                        'jrs 2026/03/24 need to add Partner and Partner_FIPS from original table
                                    ElseIf rsP.GetName(f) = "Partner" Then
                                        stmt.Parameters(f).Value = rsP2(rsP.GetName(f))
                                    ElseIf rsP.GetName(f) = "Partner_FIPS" Then
                                        stmt.Parameters(f).Value = rsP2(rsP.GetName(f))
                                    ElseIf rsP.GetName(f) = "Earliest" Then
                                        stmt.Parameters(f).Value = stmt.Parameters(2).Value
                                        For m = 2 To rsP.FieldCount - 2
                                            If Integer.TryParse(rsP.GetName(m), TryResult) AndAlso stmt.Parameters(m).Value.ToString > "" Then
                                                stmt.Parameters(f).Value = stmt.Parameters(m).Value
                                                Exit For
                                            End If
                                        Next m
                                    ElseIf rsP.GetName(f) = "MostRecent" Then
                                        stmt.Parameters(f).Value = stmt.Parameters(2).Value
                                        For m = rsP.FieldCount - 3 To 2 Step -1
                                            If Integer.TryParse(rsP.GetName(m), TryResult) AndAlso stmt.Parameters(m).Value.ToString > "" Then
                                                stmt.Parameters(f).Value = stmt.Parameters(m).Value
                                                Exit For
                                            End If
                                        Next m
                                    Else
                                        If sDisAgg = "" Then 'jrs 2007/01/16  this is true always when adding data from Table, since we don't do Alt. Estimation

                                            'mti 2026/03/11 logging
                                            'If f = 2 Then
                                            '    Debug.WriteLine("Provinces Split Issue Logging | sDisAgg blank, copying country values | db=" & db &
                                            '                        " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                            '                        " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString)
                                            'End If
                                            'mti 2026/03/11 logging end

                                            stmt.Parameters(f).Value = rsP2(rsP.GetName(f))
                                        Else 'use proportions of country for estimation

                                            'mti 2026/03/11 code change start
                                            'If CInt(rsP.GetName(f)) < CInt(rsP3.GetName(2)) Then
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("Earliest")) / CSng(rsP3("Earliest"))
                                            'ElseIf CInt(rsP.GetName(f)) > CInt(rsP3.GetName(rsP3.FieldCount - 3)) Then
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("MostRecent")) / CSng(rsP3("MostRecent"))
                                            'ElseIf Integer.TryParse(rsP.GetName(f), TryResult) Then

                                            '    'jrs 2017/09/25 changed to check last 30 to only the last 20 years
                                            '    'jrs 2017/08/18
                                            '    If rsP4(rsP.GetName(f)).ToString > "" And Not bSkipMsgDissAgg And CInt(rsP.GetName(f)) > BaseYear - 20 And CInt(rsP.GetName(f)) <= BaseYear Then
                                            '        sLine = "Auxiliary table " & rsP4Name & " has Nulls in field " & rsP.GetName(f) & ". This will produce nulls for " & rddbPTblDefs("TABLE_NAME").ToString
                                            '        'jrs 2022/12/07
                                            '        Try
                                            '            Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                                            '            objWriter.WriteLine(sLine)
                                            '            objWriter.Close()
                                            '        Catch ex As Exception
                                            '            'Do nothing
                                            '        End Try
                                            '        bSkipMsgDissAgg = True
                                            '    End If
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4(rsP.GetName(f))) / CSng(rsP3(rsP.GetName(f)))
                                            'End If

                                            Dim iFldYear As Integer
                                            Dim iFirstAuxYear As Integer
                                            Dim iLastAuxYear As Integer

                                            If Integer.TryParse(rsP.GetName(f), iFldYear) AndAlso Integer.TryParse(rsP3.GetName(2), iFirstAuxYear) AndAlso
                                                               iFldYear < iFirstAuxYear Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | Earliest fallback branch | db=" & db &
                                                '            " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '            " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '            " | Field=" & rsP.GetName(f))
                                                'End If
                                                'mti 2026/03/11 logging end

                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("Earliest")) / CSng(rsP3("Earliest"))
                                                'nulls can raise exceptions/errors - safe conversion
                                                Dim vTarget As Single
                                                Dim vProvEarliest As Single
                                                Dim vCountryEarliest As Single

                                                If Not IsDBNull(rsP2(rsP.GetName(f))) AndAlso
                                                   Not IsDBNull(rsP4("Earliest")) AndAlso
                                                   Not IsDBNull(rsP3("Earliest")) AndAlso
                                                   Single.TryParse(rsP2(rsP.GetName(f)).ToString(), vTarget) AndAlso
                                                   Single.TryParse(rsP4("Earliest").ToString(), vProvEarliest) AndAlso
                                                   Single.TryParse(rsP3("Earliest").ToString(), vCountryEarliest) AndAlso
                                                   vCountryEarliest <> 0 Then

                                                    stmt.Parameters(f).Value = vTarget * vProvEarliest / vCountryEarliest
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If

                                            ElseIf Integer.TryParse(rsP.GetName(f), iFldYear) AndAlso
                                                   Integer.TryParse(rsP3.GetName(rsP3.FieldCount - 3), iLastAuxYear) AndAlso
                                                   iFldYear > iLastAuxYear Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | MostRecent fallback branch | db=" & db &
                                                '            " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '            " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '            " | Field=" & rsP.GetName(f))
                                                'End If
                                                'mti 2026/03/11 logging end

                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("MostRecent")) / CSng(rsP3("MostRecent"))
                                                Dim vTarget As Single
                                                Dim vProvMostRecent As Single
                                                Dim vCountryMostRecent As Single

                                                If Not IsDBNull(rsP2(rsP.GetName(f))) AndAlso
                                                   Not IsDBNull(rsP4("MostRecent")) AndAlso
                                                   Not IsDBNull(rsP3("MostRecent")) AndAlso
                                                   Single.TryParse(rsP2(rsP.GetName(f)).ToString(), vTarget) AndAlso
                                                   Single.TryParse(rsP4("MostRecent").ToString(), vProvMostRecent) AndAlso
                                                   Single.TryParse(rsP3("MostRecent").ToString(), vCountryMostRecent) AndAlso
                                                   vCountryMostRecent <> 0 Then

                                                    stmt.Parameters(f).Value = vTarget * vProvMostRecent / vCountryMostRecent
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If

                                            ElseIf Integer.TryParse(rsP.GetName(f), iFldYear) Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | Exact-year branch | db=" & db &
                                                '    " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '    " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '    " | Year=" & rsP.GetName(f))

                                                'End If
                                                'mti 2026/03/11 logging end


                                                Dim iFldYear1 As Integer
                                                Dim sFldName As String = rsP.GetName(f)
                                                Dim bNullOrBlank As Boolean = IsDBNull(rsP4(sFldName)) OrElse rsP4(sFldName).ToString().Trim() = ""

                                                If Integer.TryParse(sFldName, iFldYear1) AndAlso
                                                           bNullOrBlank AndAlso
                                                           Not bSkipMsgDissAgg AndAlso
                                                           iFldYear1 > BaseYear - 20 AndAlso
                                                           iFldYear1 <= BaseYear Then

                                                    sLine = "Auxiliary table " & rsP4Name & " has Nulls in field " & sFldName &
                                                                ". This will produce nulls for " & rddbPTblDefs("TABLE_NAME").ToString

                                                    Try
                                                        Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                                                        objWriter.WriteLine(sLine)
                                                        objWriter.Close()
                                                    Catch ex As Exception
                                                        'Do nothing
                                                    End Try

                                                    bSkipMsgDissAgg = True
                                                End If

                                                'write Null safe version   
                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4(rsP.GetName(f))) / CSng(rsP3(rsP.GetName(f)))
                                                Dim vTarget As Single
                                                Dim vProvYear As Single
                                                Dim vCountryYear As Single
                                                'Dim sFldName As String = rsP.GetName(f)

                                                If Not IsDBNull(rsP2(sFldName)) AndAlso
                                                           Not IsDBNull(rsP4(sFldName)) AndAlso
                                                           Not IsDBNull(rsP3(sFldName)) AndAlso
                                                           Single.TryParse(rsP2(sFldName).ToString(), vTarget) AndAlso
                                                           Single.TryParse(rsP4(sFldName).ToString(), vProvYear) AndAlso
                                                           Single.TryParse(rsP3(sFldName).ToString(), vCountryYear) AndAlso
                                                           vCountryYear <> 0 Then

                                                    'mti 2026/03/11 logging start
                                                    'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                    '    Debug.WriteLine("Provinces Split Issue Logging | Geothermal exact-year values | Province=" &
                                                    '                    rsProv.Rows(rsProvIdx)("Province").ToString &
                                                    '                    " | Year=" & sFldName &
                                                    '                    " | TargetIsNull=" & IsDBNull(rsP2(sFldName)).ToString() &
                                                    '                    " | ProvAuxIsNull=" & IsDBNull(rsP4(sFldName)).ToString() &
                                                    '                    " | CountryAuxIsNull=" & IsDBNull(rsP3(sFldName)).ToString() &
                                                    '                    " | TargetVal=" & If(IsDBNull(rsP2(sFldName)), "<DBNull>", rsP2(sFldName).ToString()) &
                                                    '                    " | ProvAuxVal=" & If(IsDBNull(rsP4(sFldName)), "<DBNull>", rsP4(sFldName).ToString()) &
                                                    '                    " | CountryAuxVal=" & If(IsDBNull(rsP3(sFldName)), "<DBNull>", rsP3(sFldName).ToString()))
                                                    'End If
                                                    'mti 2026/03/11 logging end

                                                    stmt.Parameters(f).Value = vTarget * vProvYear / vCountryYear
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If


                                            End If

                                            'mti 2026/03/11 code change end

                                        End If
                                    End If
                                Next f
                                stmt.ExecuteNonQuery()
                            End While 'rsP2.Read, for each province

                            'Now Add the partner side
                            While rsP2P.Read
                                'we're assuming all fields in target table are already in province table
                                For f = 0 To rT
                                    'If rsP.GetName(f) = "Earliest" And rddbPTblDefs("TABLE_NAME").ToString = "SeriesAgBovineMeatProductionFAO" Then Stop
                                    'JRS 04/09/03 If Integer.TryParse(rs.Fields(f).Name) Then
                                    If rsP.GetName(f) = "Partner" Then
                                        stmt.Parameters(f).Value = rsProv.Rows(rsProvIdx)("Province")
                                    ElseIf rsP.GetName(f) = "Partner_FIPS" Then
                                        stmt.Parameters(f).Value = rsProv.Rows(rsProvIdx)("FIPS_CODE")
                                    ElseIf rsP.GetName(f) = "Actor" Then
                                        stmt.Parameters(f).Value = rsP2P(rsP.GetName(f))
                                    ElseIf rsP.GetName(f) = "Actor_FIPS" Then
                                        stmt.Parameters(f).Value = rsP2P(rsP.GetName(f))
                                    ElseIf rsP.GetName(f) = "Earliest" Then
                                        stmt.Parameters(f).Value = stmt.Parameters(2).Value
                                        For m = 2 To rsP.FieldCount - 2
                                            If Integer.TryParse(rsP.GetName(m), TryResult) AndAlso stmt.Parameters(m).Value.ToString > "" Then
                                                stmt.Parameters(f).Value = stmt.Parameters(m).Value
                                                Exit For
                                            End If
                                        Next m
                                    ElseIf rsP.GetName(f) = "MostRecent" Then
                                        stmt.Parameters(f).Value = stmt.Parameters(2).Value
                                        For m = rsP.FieldCount - 3 To 2 Step -1
                                            If Integer.TryParse(rsP.GetName(m), TryResult) AndAlso stmt.Parameters(m).Value.ToString > "" Then
                                                stmt.Parameters(f).Value = stmt.Parameters(m).Value
                                                Exit For
                                            End If
                                        Next m
                                    Else
                                        If sDisAgg = "" Then 'jrs 2007/01/16  this is true always when adding data from Table, since we don't do Alt. Estimation

                                            'mti 2026/03/11 logging
                                            'If f = 2 Then
                                            '    Debug.WriteLine("Provinces Split Issue Logging | sDisAgg blank, copying country values | db=" & db &
                                            '                        " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                            '                        " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString)
                                            'End If
                                            'mti 2026/03/11 logging end

                                            stmt.Parameters(f).Value = rsP2P(rsP.GetName(f))
                                        Else 'use proportions of country for estimation

                                            'mti 2026/03/11 code change start
                                            'If CInt(rsP.GetName(f)) < CInt(rsP3.GetName(2)) Then
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("Earliest")) / CSng(rsP3("Earliest"))
                                            'ElseIf CInt(rsP.GetName(f)) > CInt(rsP3.GetName(rsP3.FieldCount - 3)) Then
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("MostRecent")) / CSng(rsP3("MostRecent"))
                                            'ElseIf Integer.TryParse(rsP.GetName(f), TryResult) Then

                                            '    'jrs 2017/09/25 changed to check last 30 to only the last 20 years
                                            '    'jrs 2017/08/18
                                            '    If rsP4(rsP.GetName(f)).ToString > "" And Not bSkipMsgDissAgg And CInt(rsP.GetName(f)) > BaseYear - 20 And CInt(rsP.GetName(f)) <= BaseYear Then
                                            '        sLine = "Auxiliary table " & rsP4Name & " has Nulls in field " & rsP.GetName(f) & ". This will produce nulls for " & rddbPTblDefs("TABLE_NAME").ToString
                                            '        'jrs 2022/12/07
                                            '        Try
                                            '            Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                                            '            objWriter.WriteLine(sLine)
                                            '            objWriter.Close()
                                            '        Catch ex As Exception
                                            '            'Do nothing
                                            '        End Try
                                            '        bSkipMsgDissAgg = True
                                            '    End If
                                            '    stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4(rsP.GetName(f))) / CSng(rsP3(rsP.GetName(f)))
                                            'End If

                                            Dim iFldYear As Integer
                                            Dim iFirstAuxYear As Integer
                                            Dim iLastAuxYear As Integer

                                            If Integer.TryParse(rsP.GetName(f), iFldYear) AndAlso Integer.TryParse(rsP3.GetName(2), iFirstAuxYear) AndAlso
                                                               iFldYear < iFirstAuxYear Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | Earliest fallback branch | db=" & db &
                                                '            " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '            " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '            " | Field=" & rsP.GetName(f))
                                                'End If
                                                'mti 2026/03/11 logging end

                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("Earliest")) / CSng(rsP3("Earliest"))
                                                'nulls can raise exceptions/errors - safe conversion
                                                Dim vTarget As Single
                                                Dim vProvEarliest As Single
                                                Dim vCountryEarliest As Single

                                                If Not IsDBNull(rsP2P(rsP.GetName(f))) AndAlso
                                                   Not IsDBNull(rsP4("Earliest")) AndAlso
                                                   Not IsDBNull(rsP3("Earliest")) AndAlso
                                                   Single.TryParse(rsP2P(rsP.GetName(f)).ToString(), vTarget) AndAlso
                                                   Single.TryParse(rsP4("Earliest").ToString(), vProvEarliest) AndAlso
                                                   Single.TryParse(rsP3("Earliest").ToString(), vCountryEarliest) AndAlso
                                                   vCountryEarliest <> 0 Then

                                                    stmt.Parameters(f).Value = vTarget * vProvEarliest / vCountryEarliest
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If

                                            ElseIf Integer.TryParse(rsP.GetName(f), iFldYear) AndAlso
                                                   Integer.TryParse(rsP3.GetName(rsP3.FieldCount - 3), iLastAuxYear) AndAlso
                                                   iFldYear > iLastAuxYear Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | MostRecent fallback branch | db=" & db &
                                                '            " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '            " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '            " | Field=" & rsP.GetName(f))
                                                'End If
                                                'mti 2026/03/11 logging end

                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4("MostRecent")) / CSng(rsP3("MostRecent"))
                                                Dim vTarget As Single
                                                Dim vProvMostRecent As Single
                                                Dim vCountryMostRecent As Single

                                                If Not IsDBNull(rsP2P(rsP.GetName(f))) AndAlso
                                                   Not IsDBNull(rsP4("MostRecent")) AndAlso
                                                   Not IsDBNull(rsP3("MostRecent")) AndAlso
                                                   Single.TryParse(rsP2P(rsP.GetName(f)).ToString(), vTarget) AndAlso
                                                   Single.TryParse(rsP4("MostRecent").ToString(), vProvMostRecent) AndAlso
                                                   Single.TryParse(rsP3("MostRecent").ToString(), vCountryMostRecent) AndAlso
                                                   vCountryMostRecent <> 0 Then

                                                    stmt.Parameters(f).Value = vTarget * vProvMostRecent / vCountryMostRecent
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If

                                            ElseIf Integer.TryParse(rsP.GetName(f), iFldYear) Then

                                                'mti 2026/03/11 logging start
                                                'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                '    Debug.WriteLine("Provinces Split Issue Logging | Exact-year branch | db=" & db &
                                                '    " | Table=" & rddbPTblDefs("TABLE_NAME").ToString &
                                                '    " | Province=" & rsProv.Rows(rsProvIdx)("Province").ToString &
                                                '    " | Year=" & rsP.GetName(f))

                                                'End If
                                                'mti 2026/03/11 logging end


                                                Dim iFldYear1 As Integer
                                                Dim sFldName As String = rsP.GetName(f)
                                                Dim bNullOrBlank As Boolean = IsDBNull(rsP4(sFldName)) OrElse rsP4(sFldName).ToString().Trim() = ""

                                                If Integer.TryParse(sFldName, iFldYear1) AndAlso
                                                           bNullOrBlank AndAlso
                                                           Not bSkipMsgDissAgg AndAlso
                                                           iFldYear1 > BaseYear - 20 AndAlso
                                                           iFldYear1 <= BaseYear Then

                                                    sLine = "Auxiliary table " & rsP4Name & " has Nulls in field " & sFldName &
                                                                ". This will produce nulls for " & rddbPTblDefs("TABLE_NAME").ToString

                                                    Try
                                                        Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                                                        objWriter.WriteLine(sLine)
                                                        objWriter.Close()
                                                    Catch ex As Exception
                                                        'Do nothing
                                                    End Try

                                                    bSkipMsgDissAgg = True
                                                End If

                                                'write Null safe version   
                                                'stmt.Parameters(f).Value = CSng(rsP2(rsP.GetName(f))) * CSng(rsP4(rsP.GetName(f))) / CSng(rsP3(rsP.GetName(f)))
                                                Dim vTarget As Single
                                                Dim vProvYear As Single
                                                Dim vCountryYear As Single
                                                'Dim sFldName As String = rsP.GetName(f)

                                                If Not IsDBNull(rsP2P(sFldName)) AndAlso
                                                           Not IsDBNull(rsP4(sFldName)) AndAlso
                                                           Not IsDBNull(rsP3(sFldName)) AndAlso
                                                           Single.TryParse(rsP2P(sFldName).ToString(), vTarget) AndAlso
                                                           Single.TryParse(rsP4(sFldName).ToString(), vProvYear) AndAlso
                                                           Single.TryParse(rsP3(sFldName).ToString(), vCountryYear) AndAlso
                                                           vCountryYear <> 0 Then

                                                    'mti 2026/03/11 logging start
                                                    'If db = "IFsHistSeries" AndAlso rddbPTblDefs("TABLE_NAME").ToString = "SeriesForecastMigrantFlowIn" Then
                                                    '    Debug.WriteLine("Provinces Split Issue Logging | Geothermal exact-year values | Province=" &
                                                    '                    rsProv.Rows(rsProvIdx)("Province").ToString &
                                                    '                    " | Year=" & sFldName &
                                                    '                    " | TargetIsNull=" & IsDBNull(rsP2(sFldName)).ToString() &
                                                    '                    " | ProvAuxIsNull=" & IsDBNull(rsP4(sFldName)).ToString() &
                                                    '                    " | CountryAuxIsNull=" & IsDBNull(rsP3(sFldName)).ToString() &
                                                    '                    " | TargetVal=" & If(IsDBNull(rsP2(sFldName)), "<DBNull>", rsP2(sFldName).ToString()) &
                                                    '                    " | ProvAuxVal=" & If(IsDBNull(rsP4(sFldName)), "<DBNull>", rsP4(sFldName).ToString()) &
                                                    '                    " | CountryAuxVal=" & If(IsDBNull(rsP3(sFldName)), "<DBNull>", rsP3(sFldName).ToString()))
                                                    'End If
                                                    'mti 2026/03/11 logging end

                                                    stmt.Parameters(f).Value = vTarget * vProvYear / vCountryYear
                                                Else
                                                    stmt.Parameters(f).Value = DBNull.Value
                                                End If


                                            End If

                                            'mti 2026/03/11 code change end

                                        End If
                                    End If
                                Next f
                                stmt.ExecuteNonQuery()
                            End While 'rsP2.Read, for each province
                        End Using
                        'End Using
                        'jrs 2022/10/11
                        rsP2.Close()
                        rsP2P.Close()
                        'If rddbPTblDefs("TABLE_NAME").ToString = "Copy Of HealthDetailedDeathsCtryPre20180310" Then Stop
                        'jrs 2022/10/11
                        If Not rsP3 Is Nothing Then rsP3.Close()
                        If Not rsP4 Is Nothing Then rsP4.Close()
                        If Not rsP5 Is Nothing Then rsP5.Close()
                        If Not rsP6 Is Nothing Then rsP6.Close()

                    End If

                    rsProvIdx = rsProvIdx + 1
                Loop

                Debug.WriteLine("Provinces Split Issue Logging | END table | db=" & db &
                    " | Table=" & rddbPTblDefs("TABLE_NAME").ToString)

                rsP.Close()
                'jrs 2007/01/10
                '            rsProv.MoveLast
                '            If rsProv.Rows(rsProvIdx)("AllProvinces") Then
                '                dbP.Execute ("DELETE * FROM [" + rddbPTblDefs("TABLE_NAME") + "] WHERE Country = '" & rsProv.Rows(rsProvIdx)("P.Country") & "'")
                '            End If


            End If
            'jrs 2022/10/10
            rdTDs.Close()
        End While

        'mti 2026/03/11 logging
        'Debug.WriteLine("Provinces Split Issue Logging | End AddProvincesMult | db=" & db &
        '                          " | TablesProcessed=" & tblCount.ToString())
        dbP.Close()
        'jrs 2016/02/19
        If bCountryFile Then
            dbP2.Close()
        End If
        If db.ToUpper = "IFS" Then
            dbPH.Close()
            dbP2H.Close()
        End If

        'jrs 2007/12/18
        dbAE.Close()
        'jrs 2012/06/28
    End Sub

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

        'jrs 2026/03/02
        If IsNothing(dbT) Then
            myTablecheck = False
            Exit Function
        End If

        Try
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

                rsT.Close()
                rsT = dbTCmdrsT.ExecuteReader

                CheckTableStructure(rsT, rsT1, dbT, dbT1, sTbl, sProv) 'jrs 2012/05/15
            End If
        Catch ex As Exception
            myTablecheck = False
        End Try

    End Function

    'jrs 2007/01/16 added parameter sDisAgg
    Private Function AlternativeEstimation(sFile As String, sTable As String, ByRef sDisAgg As String, dbAE As SqliteConnection) As Boolean
        'mti 2026/03/11 declaration changed to By Ref

        AlternativeEstimation = False
        Select Case sFile.ToUpper
            Case "IFS"
                If sTable.ToUpper.Contains("COHORT") Then
                    If sTable.ToUpper.Contains("COUNTRY") Then
                        AlternativeEstimation = True
                    End If
                ElseIf sTable.ToUpper.Contains("EDUCATTAIN") Then 'jrs 2020/02/26
                    If sTable.ToUpper.Contains("COUNTRY") Then
                        AlternativeEstimation = True
                    End If
                ElseIf sTable.ToUpper.Contains("POVRATEAGE") Then 'jrs 2020/03/02
                    If sTable.ToUpper.Contains("COUNTRY") Then
                        AlternativeEstimation = True
                    End If
                ElseIf sTable = "CountryParameters" Then
                    AlternativeEstimation = True
                ElseIf sTable = "Country Translation" Then
                    AlternativeEstimation = True
                ElseIf sTable.ToUpper.Contains("HEALTHDETAILEDDEATHSCTRY") Then 'jrs 2012/02/17
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
                ElseIf sTable = "HealthMortRatios" Then 'jrs 2026/03/04
                    AlternativeEstimation = True
                End If
            Case "IFSHISTSERIES"
                Dim rsAE As SqliteDataReader
                Dim dbAECmdrsAE As SqliteCommand
                dbAECmdrsAE = dbAE.CreateCommand
                'jrs 2007/01/16 added Aggregation and DisAggregation
                dbAECmdrsAE.CommandText = "SELECT Aggregation, Disaggregation, UsedInPreprocessor FROM DataDict WHERE [Table] = '" & sTable & "'"
                rsAE = dbAECmdrsAE.ExecuteReader
                While rsAE.Read
                    If CInt(rsAE("UsedInPreprocessor")) = 1 Then
                        AlternativeEstimation = True
                        'mti 2022/03/06 start
                        '                    If UCase(rsAE.Fields("Aggregation")) = "SUM" Then
                        '                        sDisAgg = rsAE.Fields("Disaggregation")
                        '                    Else
                        '
                        '                        sDisAgg = ""
                        '                    End If
                        'mti 2026/03/11 code change start
                        'sDisAgg = rsAE("Aggregation").ToString.ToUpper
                        sDisAgg = If(IsDBNull(rsAE("Aggregation")), "", rsAE("Aggregation").ToString().Trim().ToUpper())

                        If sDisAgg = "SUM" Then
                            'sDisAgg = rsAE("Disaggregation").ToString
                            sDisAgg = If(IsDBNull(rsAE("Disaggregation")), "", rsAE("Disaggregation").ToString().Trim())
                        Else
                            'sDisAgg = rsAE("Disaggregation").ToString
                            sDisAgg = If(IsDBNull(rsAE("Disaggregation")), "", rsAE("Disaggregation").ToString().Trim())
                            If sDisAgg = "EST" Then
                                AlternativeEstimation = False
                            Else
                                sDisAgg = ""
                            End If
                        End If

                        'mti 2026/03/11 code change end

                        'mti 2022/03/06 End
                    End If
                End While

                rsAE.Close()
            Case "IFSHISTSERIESDYADIC" 'jrs 2026/03/24
                Dim rsAE As SqliteDataReader
                Dim dbAECmdrsAE As SqliteCommand
                dbAECmdrsAE = dbAE.CreateCommand
                'jrs 2007/01/16 added Aggregation and DisAggregation
                dbAECmdrsAE.CommandText = "SELECT Aggregation, Disaggregation, UsedInPreprocessor FROM DataDict WHERE [Table] = '" & sTable & "'"
                rsAE = dbAECmdrsAE.ExecuteReader
                While rsAE.Read
                    'jrs 2026/03/24 Not being Marked for now
                    'If CInt(rsAE("UsedInPreprocessor")) = 1 Then
                    AlternativeEstimation = True
                        'mti 2022/03/06 start
                        '                    If UCase(rsAE.Fields("Aggregation")) = "SUM" Then
                        '                        sDisAgg = rsAE.Fields("Disaggregation")
                        '                    Else
                        '
                        '                        sDisAgg = ""
                        '                    End If
                        'mti 2026/03/11 code change start
                        'sDisAgg = rsAE("Aggregation").ToString.ToUpper
                        sDisAgg = If(IsDBNull(rsAE("Aggregation")), "", rsAE("Aggregation").ToString().Trim().ToUpper())

                        If sDisAgg = "SUM" Then
                            'sDisAgg = rsAE("Disaggregation").ToString
                            sDisAgg = If(IsDBNull(rsAE("Disaggregation")), "", rsAE("Disaggregation").ToString().Trim())
                        Else
                            'sDisAgg = rsAE("Disaggregation").ToString
                            sDisAgg = If(IsDBNull(rsAE("Disaggregation")), "", rsAE("Disaggregation").ToString().Trim())
                            If sDisAgg = "EST" Then
                                AlternativeEstimation = False
                            Else
                                sDisAgg = ""
                            End If
                        End If

                    'mti 2026/03/11 code change end

                    'mti 2022/03/06 End
                    'End If
                End While

                rsAE.Close()
        End Select
    End Function


    Private Function getCohortVal(sMigCohort As String, rsCoh As SqliteDataReader) As Single
        Dim i As Integer
        Select Case sMigCohort
            Case "0-4"
                getCohortVal = CSng(rsCoh(3))
            Case "65+"
                getCohortVal = 0
                For i = rsCoh.FieldCount - 1 To 16 Step -1
                    If rsCoh(i).ToString > "" Then
                        getCohortVal = getCohortVal + CSng(rsCoh(i))
                    End If
                Next i
            Case Else
                Dim sFst As String
                Dim pos As Integer
                pos = sMigCohort.IndexOf("-")
                sFst = sMigCohort.Substring(0, pos)
                i = CInt(CInt(sFst) / 5 + 3)
                getCohortVal = CSng(rsCoh(i))
        End Select
    End Function

    Public Function AddParameter(ByRef stmt As SqliteCommand, ByVal name As String, ByRef type As SqliteType) As SqliteParameter
        Dim param = stmt.CreateParameter()
        param.ParameterName = name
        param.SqliteType = type
        Return stmt.Parameters.Add(param)
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
        For i = 0 To rsT.FieldCount - 1
            FieldsT.Add(rsT.GetName(i))
        Next
        'Find if there are missing fields
        For i = 0 To rsS.FieldCount - 1
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
            Dim TryResult As Integer
            Do While Not Integer.TryParse(Tbl.GetName(j), TryResult)
                j = j + 1
                If j >= Tbl.FieldCount - 1 Then Exit Do
            Loop
        End If

        'jrs 2014/05/13
        ' gvdb 2024/04/05
        ' unused variables
        'Dim bEarliest As Boolean, bMostRecent As Boolean
        'If Tbl.GetName(Tbl.FieldCount - 1) = "MostRecent" Then bMostRecent = True
        'If Tbl.GetName(Tbl.FieldCount - 2) = "Earliest" Then bEarliest = True
        'Dim fld As New Field
        'On Error GoTo Hdl
        For Each f In Fields
            dbSCmdrsData.CommandText = "SELECT COUNT(*) as C FROM [" & sTbl & "] WHERE [" & f & "] IS NOT NULL"
            rsData = dbSCmdrsData.ExecuteReader
            'There's Data
            If CSng(rsData("C")) > 0 Then
                'Add them to the Target Table
                Do While f > Tbl.GetName(j)
                    j = j + 1
                    If j >= Tbl.FieldCount Then Exit Do
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
            dbTargetCmdrsTotals.CommandText = "SELECT SUM(MigrantsMales) as M, SUM(MigrantsFemales) as F FROM HistMigration WHERE Country = """ & sProv & """ AND Year = " & rsYrs("Year").ToString
            rsTotals = dbTargetCmdrsTotals.ExecuteReader
            'jrs 2026/03/03
            rsTotals.Read()
            'Loop through each age
            Dim sUpd As String = "UPDATE HistMigration SET MigPctDistMales = MigrantsMales * 100 / " & rsTotals("M").ToString
            sUpd = sUpd & ", MigPctDistFemales = MigrantsFemales * 100 / " & rsTotals("F").ToString
            sUpd = sUpd & ", MigPctDistTotal = (MigrantsMales + MigrantsFemales) * 100 / " & (CSng(rsTotals("M")) + CSng(rsTotals("F"))).ToString
            sUpd = sUpd & " WHERE Country = """ & sProv & """ AND Year = " & rsYrs("Year").ToString

            dbTargetCmd.CommandText = sUpd
            dbTargetCmd.ExecuteNonQuery()
            rsTotals.Close()
        End While
        rsYrs.Close()
    End Sub

    'Checks that Table has same structure than target, if not recreates table
    Private Sub CheckTableStructure(ByRef rsS As SqliteDataReader, rsT As SqliteDataReader, dbS As SqliteConnection, dbT As SqliteConnection, sTbl As String, sProv As String)
        Dim bFlag As Boolean
        If rsS.FieldCount <> rsT.FieldCount Then
            bFlag = True
        End If
        If Not bFlag Then
            Dim i As Integer
            For i = rsS.FieldCount - 1 To 0 Step -1
                If rsS.GetName(i) <> rsT.GetName(i) Then
                    bFlag = True
                    Exit For
                End If
            Next i
        End If
        If bFlag Then
            'Recreate the table with the same structure
            'rsS = Nothing
            Dim dbSCommand As SqliteCommand
            dbSCommand = dbS.CreateCommand
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
            For j = 0 To rsT.FieldCount - 1
                'jrs 2022/10/06
                If rsT.GetName(j) <> "Country" And rsT.GetName(j) <> "FIPS_CODE" And
                        rsT.GetName(j) <> "Earliest" And rsT.GetName(j) <> "MostRecent" Then
                    sCreate = sCreate & ", """ & rsT.GetName(j) & """ DOUBLE(53)"
                End If

                'jrs 2014/05/13 unfortunately I can't count on this tables having their fields sorted
                'I'll need to do a double search
                '            If tblS.Fields(j).Name > tblO.Fields(k).Name Then
                '                Do While tblS.Fields(j).Name > tblO.Fields(k).Name
                '                    k = k + 1
                '                    If k >= tblO.Fields.Count Then Exit For
                '                Loop
                '            End If
                For k = 0 To rsS.FieldCount - 1
                    If rsT.GetName(j) = rsS.GetName(k) Then
                        If rsT.GetName(j) = "Earliest" Then
                            bEarliest = True
                        ElseIf rsT.GetName(j) = "MostRecent" Then
                            bMostRecent = True
                        Else
                            If sFields = "" Then
                                sFields = "[" & rsT.GetName(j) & "]"
                            Else
                                sFields = sFields & ",[" & rsT.GetName(j) & "]"
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
            'jrs 2012/05/15
            rsS.Close()
            'jrs 2022/11/03 *** start ***
            dbS.Close()
            dbS.Open()
            'jrs 2022/11/04
            Using cmd2 = dbS.CreateCommand()
                cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                cmd2.ExecuteNonQuery()
            End Using
            dbSCommand = dbS.CreateCommand
            'jrs 2022/11/03 *** end ***

            'Delete Original table
            dbSCommand.CommandText = "DROP TABLE [tmp" & sTbl & "]"
            dbSCommand.ExecuteNonQuery()
            'Reopen the table
            If sProv > "" Then
                dbSCommand.CommandText = "SELECT * FROM [" & sTbl & "] WHERE Country = """ & sProv & """"
            Else
                dbSCommand.CommandText = "SELECT * FROM " & sTbl
            End If
            rsS = dbSCommand.ExecuteReader

        End If
    End Sub

    'jrs 2012/09/24
    'Procedure to copy initialization factors from provinces to regular file (ifs.dat)
    Private Sub AddProvincesDat(sCountry As String)
        If IO.File.Exists(PutDataFileDir$ & "IFS" & sCountry & ".DAT") Then
            Dim WriteStr As String = ""
            Dim objReader = New StreamReader(PutDataFileDir$ & "IFS" & sCountry & ".DAT")
            Dim objWriter = New System.IO.StreamWriter(PutDataFileDir$ + "IFS.DAT", append:=True)

            Do While Not WriteStr = Nothing
                WriteStr = objReader.ReadLine
                objWriter.WriteLine(WriteStr)
            Loop
            objReader.Close()
            objWriter.Close()
        End If
    End Sub

    'Function to Retrieve a Region based on a Country Name
    Public Function getRegionFromCtry(sCtry As String, sDir As String) As String

        Dim dbR As SqliteConnection
        Dim dbRcmd As SqliteCommand
        Dim valtblR As SqliteDataReader

        dbR = New SqliteConnection("DataSource=" & sDir & "ifs.db;")
        dbR.Open()

        'jrs 2008/02/11
        'Try a translation first
        dbRcmd = dbR.CreateCommand
        dbRcmd.CommandText = "SELECT [Country] FROM [Country Translation] WHERE [ESRI Countries]=""" + sCtry + """"
        valtblR = dbRcmd.ExecuteReader
        Dim sTCtry As String
        If valtblR.HasRows Then
            valtblR.Read()
            sTCtry = If(valtblR(0).ToString = "", sCtry, valtblR(0).ToString)
        Else
            sTCtry = sCtry
        End If
        valtblR.Close()

        dbRcmd.CommandText = "SELECT RegionName FROM [RegionalizationFull]" &
        " AS R, RegionIDNewFull AS I Where I.RegionNum = R.NewRegion AND R.Country = """ &
        sTCtry & """"
        valtblR = dbRcmd.ExecuteReader

        getRegionFromCtry = ""
        While valtblR.Read
            getRegionFromCtry = valtblR("RegionName").ToString
        End While

        If getRegionFromCtry = "" Then
            ''2008/02/11
            ''check for groups (in the case of provinces loaded)
            'If bCheckAllProvinces Then
            '    If gGetGroupIdx(sTCtry, False) > -1 Then
            '        getRegionFromCtry = sTCtry
            '    End If
            'End If
            ''jrs 2014/11/06 when adding provinces and country <> than region, like the case of USA
            'If getRegionFromCtry = "" Then
            '    valtblR.Close()
            '                'check the forecast name
            'Set valtblR = dbR.OpenRecordset("SELECT [IFs Countries Forecast] FROM [Country Translation] WHERE [Country]=""" + sTCtry + """")
            'If valtblR.RecordCount > 0 Then
            '        getRegionFromCtry = IIf(IsNull(valtblR.Fields(0)), sCtry, valtblR.Fields(0))
            '    End If
            'End If
        End If

        valtblR.Close()

        dbR.Close()
    End Function

    Private Sub Alphabetize(dbR As SqliteConnection)
        Dim rsRO As SqliteDataReader
        Dim dbRCmdrsRO As SqliteCommand
        dbRCmdrsRO = dbR.CreateCommand
        'jrs 2026/03/04 added UPPER
        dbRCmdrsRO.CommandText = "SELECT * FROM RegionIDNewFull ORDER BY UPPER(RegionName)"
        rsRO = dbRCmdrsRO.ExecuteReader
        Dim rsMax As SqliteDataReader
        Dim dbRCmdrsMax As SqliteCommand
        dbRCmdrsMax = dbR.CreateCommand
        dbRCmdrsMax.CommandText = "SELECT Max(RegionNum) as MaxRegNum FROM RegionIDNewFull"
        rsMax = dbRCmdrsMax.ExecuteReader
        'jrs 2026/03/03
        rsMax.Read()
        Dim OldRegId As Integer
        Dim counter As Integer
        Dim bFlag As Boolean
        Dim RegIds() As Integer
        ReDim RegIds(CInt(rsMax("MaxRegNum")))
        'gvdb2024/04/05
        ' unused variable
        'Dim OldRegMembs As Integer
        counter = 1

        'jrs 2022/10/27 Transactions would now allow anything to run, and no error message
        'Using trn = dbR.BeginTransaction()
        Using stmt = dbR.CreateCommand()

            Dim sUpd As String = "UPDATE RegionIDNewFull SET RegionNum = $RegionNum WHERE RegionName = $RegionName"
            AddParameter(stmt, "$RegionName", SqliteType.Text)
            AddParameter(stmt, "$RegionNum", SqliteType.Integer)
            stmt.CommandText = sUpd

            While rsRO.Read
                OldRegId = CInt(rsRO("RegionNum"))
                bFlag = (OldRegId <> counter)

                stmt.Parameters(0).Value = rsRO("RegionName")
                stmt.Parameters(1).Value = counter + 1000 'this is necessary to avoid duplicates
                stmt.ExecuteNonQuery()

                'RegName(counter) = rsRO.Fields("RegionName")
                'OldRegMembs = RegMembs(counter)
                'RegMembs(counter) = RegMembs(OldRegId)
                'RegMembs(OldRegId) = OldRegMembs
                RegIds(OldRegId) = counter + 1000
                'GRPS$(1, counter) = RegName(counter)

                'If bFlag Then
                '    IFsRegs.Item(rsRO.Fields("RegionName")).Index = counter
                'End If

                counter = counter + 1
            End While
            'GRPS$(1, counter) = "ALL"
        End Using

        Using stmt2 = dbR.CreateCommand()
            stmt2.CommandText = "UPDATE RegionIDNewFull SET RegionNum = RegionNum - 1000"
            stmt2.ExecuteNonQuery()

            stmt2.CommandText = "UPDATE RegionalizationFull SET NewRegion = $NewRegion WHERE NewRegion = $OldRegion"
            AddParameter(stmt2, "$NewRegion", SqliteType.Integer)
            AddParameter(stmt2, "$OldRegion", SqliteType.Integer)

            For i = 1 To CInt(rsMax("MaxRegNum"))
                stmt2.Parameters(0).Value = RegIds(i)
                stmt2.Parameters(1).Value = i
                stmt2.ExecuteNonQuery()
            Next
        End Using

        Using stmt3 = dbR.CreateCommand()
            stmt3.CommandText = "UPDATE RegionalizationFull SET NewRegion = NewRegion - 1000"
            stmt3.ExecuteNonQuery()
        End Using
        'End Using

    End Sub

    Private Sub DeleteCountryFromFile(sCountry As String, sFile As String)
        '-------------------------------------------------------------------
        'Connect to the Database
        Dim dbP As SqliteConnection
        dbP = New SqliteConnection("DataSource=" & PutFdrvRun & sFile & ".db;")
        dbP.Open()

        'gvdb 2024/04/05
        ' unused variable
        'Dim rsP As SqliteDataReader
        Dim dbPCmd As SqliteCommand
        dbPCmd = dbP.CreateCommand

        'jrs 2022/11/04
        Using cmd2 = dbP.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        'gvdb 2024/04/05
        ' unused variable
        'Dim bCurrency As Boolean
        'Dim TryResult As Integer


        'Check all tables of the Database
        Dim rddbPTblDefs As SqliteDataReader
        Dim dbPCmdTblDefs As SqliteCommand
        dbPCmdTblDefs = dbP.CreateCommand
        dbPCmdTblDefs.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'"
        rddbPTblDefs = dbPCmdTblDefs.ExecuteReader
        Dim dbPCCmdTDs As SqliteCommand
        dbPCCmdTDs = dbP.CreateCommand
        Dim rdTDs As SqliteDataReader

        'Delete the country from all tables in this database
        While rddbPTblDefs.Read
            'If rddbPTblDefs("TABLE_NAME") = "SeriesForecastNetMigrationUNPD" Then Stop
            'If k = 1176 Then Stop
            'jrs 2012/02/16

            'Exceptions
            If sFile.ToUpper = "IFS" Then
                'Skip Country Translation table to avoid loosing translation
                If rddbPTblDefs("TABLE_NAME").ToString = "Country Translation" Then
                    If Not rddbPTblDefs.Read() Then
                        Exit While
                    End If
                End If
            End If
            Dim bDel As Boolean = False
            dbPCCmdTDs.CommandText = "SELECT * FROM [" & rddbPTblDefs("TABLE_NAME").ToString & "]"
            rdTDs = dbPCCmdTDs.ExecuteReader
            For l = 0 To rdTDs.FieldCount - 1
                If rdTDs.GetName(l).ToUpper = "COUNTRY" Then
                    bDel = True
                    Exit For
                End If
            Next l
            rdTDs.Close()
            If bDel Then
                'jrs 2022/11/04
                Dim stmt As SqliteCommand = dbP.CreateCommand()
                Dim bFirstStmt As Boolean = True
                'gvdb 2024/04/05
                ' unused variable
                'Dim rT As Integer

                Dim rsPDEl As String = "DELETE FROM [" & rddbPTblDefs("TABLE_NAME").ToString & "] WHERE Country = """ & sCountry & """"
                dbPCmd.CommandText = rsPDEl
                dbPCmd.ExecuteNonQuery()

            End If
        End While

        dbP.Close()
    End Sub

    'jrs 2026/03/25
    Private Sub DeleteCountryFromBilateralFile(sCountry As String, sFile As String)
        '-------------------------------------------------------------------
        'Check the file exists
        If Not System.IO.File.Exists(PutFdrvRun & sFile & ".db") Then
            Exit Sub
        End If
        'Connect to the Database
        Dim dbP As SqliteConnection
        dbP = New SqliteConnection("DataSource=" & PutFdrvRun & sFile & ".db;")
        dbP.Open()

        'gvdb 2024/04/05
        ' unused variable
        'Dim rsP As SqliteDataReader
        Dim dbPCmd As SqliteCommand
        dbPCmd = dbP.CreateCommand

        'jrs 2022/11/04
        Using cmd2 = dbP.CreateCommand()
            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
            cmd2.ExecuteNonQuery()
        End Using

        'gvdb 2024/04/05
        ' unused variable
        'Dim bCurrency As Boolean
        'Dim TryResult As Integer


        'Check all tables of the Database
        Dim rddbPTblDefs As SqliteDataReader
        Dim dbPCmdTblDefs As SqliteCommand
        dbPCmdTblDefs = dbP.CreateCommand
        dbPCmdTblDefs.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'"
        rddbPTblDefs = dbPCmdTblDefs.ExecuteReader
        Dim dbPCCmdTDs As SqliteCommand
        dbPCCmdTDs = dbP.CreateCommand
        Dim rdTDs As SqliteDataReader

        'Delete the country from all tables in this database
        While rddbPTblDefs.Read
            'If rddbPTblDefs("TABLE_NAME") = "SeriesForecastNetMigrationUNPD" Then Stop
            'If k = 1176 Then Stop
            'jrs 2012/02/16

            Dim bDel As Boolean = False
            dbPCCmdTDs.CommandText = "SELECT * FROM [" & rddbPTblDefs("TABLE_NAME").ToString & "]"
            rdTDs = dbPCCmdTDs.ExecuteReader
            For l = 0 To rdTDs.FieldCount - 1
                If rdTDs.GetName(l).ToUpper = "ACTOR" Or rdTDs.GetName(l).ToUpper = "PARTNER" Then
                    bDel = True
                    Exit For
                End If
            Next l
            rdTDs.Close()
            If bDel Then
                'jrs 2022/11/04
                Dim stmt As SqliteCommand = dbP.CreateCommand()
                Dim bFirstStmt As Boolean = True
                'gvdb 2024/04/05
                ' unused variable
                'Dim rT As Integer

                Dim rsPDEl As String = "DELETE FROM [" & rddbPTblDefs("TABLE_NAME").ToString & "] WHERE Actor = """ & sCountry & """"
                dbPCmd.CommandText = rsPDEl
                dbPCmd.ExecuteNonQuery()

                rsPDEl = "DELETE FROM [" & rddbPTblDefs("TABLE_NAME").ToString & "] WHERE Partner = """ & sCountry & """"
                dbPCmd.CommandText = rsPDEl
                dbPCmd.ExecuteNonQuery()
            End If
        End While

        dbP.Close()
    End Sub

    Public Sub DeleteCountriesFromRUNFILES(Ctry As String)
        'jrs 2022/12/08
        'The vb6 version used to check for breakups that were partial,
        'that is included only parts of the country and would leave the full country besides thoses parts
        'That's not implemented here
        Dim sCtrys As New List(Of String)
        'readCountryWAllProvinces(sCtrys)
        'If sCtrys.Count > 0 Then
        'For Each Ctry In sCtrys
        'Delete it from these access files
        DeleteCountryFromFile(Ctry, "IFs")
        DeleteCountryFromFile(Ctry, "IFsHistSeries")
        DeleteCountryFromFile(Ctry, "IFsWVSCohort")
        DeleteCountryFromFile(Ctry, "IFsCoVaTra")
        DeleteCountryFromFile(Ctry, "IFsCoVaTraSeries")
        'jrs 2026/03/25
        DeleteCountryFromBilateralFile(Ctry, "IFsHistSeriesDyadic")
        '   Next
        'End If

        'bCheckAllProvinces = (sCtrys.Count > 0)
    End Sub

    Public Sub readCountryWAllProvinces(ByRef ctrys As List(Of String))
        Dim db As SqliteConnection
        db = New SqliteConnection("DataSource=" & PutFdrvRun & "Provinces.db;")
        db.Open()

        Dim rs As SqliteDataReader
        Dim rsCmd As SqliteCommand
        rsCmd = db.CreateCommand
        rsCmd.CommandText = "SELECT * FROM AllProvinces Where AllProvinces = True"
        rs = rsCmd.ExecuteReader

        Do While rs.Read
            'jrs 2014/10/22 If IFsRegs.Item(rs.Fields("Country")).Index < 0 Then
            'If IFsRegs.Item(getRegionFromCtry(rs.Fields("Country"))).Index < 0 Then
            '    ctrys.Add(rs("Country").ToString)
            '    gLoadGroupMembers getRegionFromCtry(rs.Fields("Country").value), POPidx, -1, -1 'jrs 2014/11/06 convert to regions
            '    sReg1AllProv = gGroupMembrs(1)
            'End If
        Loop
        rs.Close()
        db.Close()
    End Sub

    Private Sub HandleStructure(EqnNo%, START%, cBlock As String, strcode$, LenLastAddition%)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'Driver Code Changes June 2003 - whole proc

        'Handles VB structures found in VB Code
        'merges everything in the block if structure less than certain length
        'otherwise discards end of structure and puts marker at the start of structure

        Dim i%
        Dim LastCBlock As String
        Dim SaveCBlock As String


        If EqnNo - START <= 10 Then
            'complete Structure if <= 10 lines

            'add last part of the structure
            SaveCBlock = cBlock
            'CodeBlock(EqnNo) = CodeBlock(EqnNo) + cBlock
            cBlock = ""
            'merge structure in same block,
            'duplication taken care of during building vbcode
            'merge all blocks from the beginning to the end of structure
            LastCBlock = ""
            For i = START To EqnNo '- 1
                If CodeBlock(i) <> LastCBlock Then 'check duplicate blocks
                    cBlock = cBlock + CodeBlock(i)
                    LastCBlock = CodeBlock(i)
                End If
            Next i
            cBlock = cBlock + SaveCBlock
            'assign (same) merged code block to each eqn within the structure
            For i = START To EqnNo '- 1
                CodeBlock(i) = cBlock
            Next i
        Else

            'unfinished structures arise when structure > 10 lines
            'a marker saying complete structure at the designated line is added to the startblock
            'it is reparsed while taking VBCode for IFSVar variables

            cBlock = cBlock.Substring(0, cBlock.Length - LenLastAddition)
            CodeBlock(EqnNo) = CodeBlock(EqnNo) + cBlock
            CodeBlock(START) = "COMPLETE STRUCTURE:" + strcode + ":" + CStr(EqnNo).Trim + "|" + CodeBlock(START)
            cBlock = ""

        End If



    End Sub


    Function SplitArguments(input As String) As List(Of String)
        'mti 2025/07/09 new function
        Dim args As New List(Of String)
        Dim currentArg As String = ""
        Dim parenDepth As Integer = 0
        Dim inString As Boolean = False

        For Each ch As Char In input
            If ch = """"c Then
                inString = Not inString
            End If

            If Not inString Then
                If ch = "("c Then parenDepth += 1
                If ch = ")"c Then parenDepth -= 1
                If ch = ","c AndAlso parenDepth = 0 Then
                    SplitAndAddExpressions(args, currentArg.Trim)
                    currentArg = ""
                    Continue For
                End If
            End If

            currentArg &= ch
        Next

        If currentArg <> "" Then
            SplitAndAddExpressions(args, currentArg)
        End If

        Return args
    End Function

    Private Sub SplitAndAddExpressions(args As List(Of String), expression As String)
        'mti 2025/07/09 new function
        Dim current As String = ""
        Dim parenDepth As Integer = 0
        Dim inString As Boolean = False
        If expression.Length >= 2 AndAlso expression(0) = "("c AndAlso expression(expression.Length - 1) = ")"c Then
            expression = expression.Substring(1, expression.Length - 2).Trim
        End If
        For Each ch As Char In expression
            If ch = """"c Then
                inString = Not inString
            End If

            If Not inString Then
                If ch = "("c Then parenDepth += 1
                If ch = ")"c Then parenDepth -= 1

                If (ch = "+"c OrElse ch = "-"c OrElse ch = "*"c OrElse ch = "/"c) AndAlso parenDepth = 0 Then
                    AddIfNotNumeric(args, current)
                    current = ""
                    Continue For
                End If
            End If

            current &= ch
        Next

        If current <> "" Then
            AddIfNotNumeric(args, current)
        End If
    End Sub

    Private Sub AddIfNotNumeric(args As List(Of String), arg As String)
        'mti 2025/07/09 new function
        Dim trimmedArg As String = arg.Trim()
        Dim dummy As Double
        If Not Double.TryParse(trimmedArg, dummy) Then
            args.Add(trimmedArg)
        End If
    End Sub








    Private Function StripAmaxAmin(strCode As String) As String
        'mti 2025/06/24 new routine

        Dim ReadStr As String
        Dim VarStr As String

        Dim leftParenTot As Integer
        Dim RightParenTot As Integer
        Dim CharAsc As Integer

        Dim FuncEnd As Boolean
        Dim VarEnd As Boolean
        Dim SkipChar As Boolean

        Dim pos As Integer, pos1 As Integer

        strCode = strCode.Trim

        'mti 2025/06/24 start  
        'parsing of AMAX or Amin inside XYTABL; ; earlier code missed XYTabl parsing when both AMAX and Amin are used on same variable
        'strcode = "Call XYTABL(TbName$, Amin(50, GDPPCP(R%)), FormValue, context)"

        Do While True 'pos > -1 Or pos1 > -1

            VarStr = "" : ReadStr = ""
            leftParenTot = 0 : RightParenTot = 0 : CharAsc = 0
            FuncEnd = False : VarEnd = False : SkipChar = False

            pos = strCode.IndexOf("Amin(")
            pos1 = strCode.IndexOf("AMAX(")
            If pos >= 0 And pos1 >= 0 Then
                pos = Math.Min(pos, pos1) 'both exists, take the first one first
            Else
                If pos1 >= 0 Then 'if only latter one exists, copy the pointer to pos 
                    pos = pos1  'pos must have been -1 (not found); or this will go to above branch
                    pos1 = -1
                End If
                'if pos1 is -1; pos will be used automatically whether it's -1 or >=0r
            End If

            If pos >= 0 Then
                ReadStr = strCode.Substring(pos, 5)
                pos = pos + 5
                leftParenTot = 1
                Do 'While True 'Not FuncEnd
                    CharAsc = AscW(strCode.Substring(pos, 1))
                    ReadStr = ReadStr + Chr(CharAsc)

                    If CharAsc = 40 Then '(
                        leftParenTot = leftParenTot + 1
                    ElseIf CharAsc = 41 Then ')
                        RightParenTot = RightParenTot + 1
                        If RightParenTot = leftParenTot Then
                            FuncEnd = True
                            SkipChar = True
                        End If
                    ElseIf CharAsc = 44 Then ',
                        If RightParenTot = leftParenTot - 1 Then

                            'first argument of amin ' VarStrName1
                            'mti 2025/07/09 start 'pattern to check if string is number, or number plus VB Type suffix: Matches: 123, 123.45, 0.2#, .5E4!, 99%, 5@, 2&
                            Dim pattern As String = "^[+-]?(\d+(\.\d*)?|\.\d+)([eE][+-]?\d+)?[%&!#@]?$"

                            'If VarStr.All(AddressOf Char.IsDigit) Then 'it's a number 'CInt(VarStrName) = 0 Then  'text - variable name
                            If Regex.IsMatch(VarStr.Trim(), pattern) Then 'it's a number 'CInt(VarStrName) = 0 Then  'text - variable name
                                'mti 2025/07/09 end
                                VarStr = ""  'number, read the next
                                SkipChar = True 'skip separating comma
                            Else
                                'mti 2025/07/09 start 'commented the following - need to read the second var
                                'VarEnd = True 
                                'mti 2025/07/09 end
                            End If
                        End If

                    End If
                    If Not VarEnd And Not SkipChar Then VarStr = VarStr + Chr(CharAsc)
                    SkipChar = False

                    pos = pos + 1
                Loop Until FuncEnd
                strCode = strCode.Replace(ReadStr, VarStr.Trim)
                pos = -1

            End If

            If pos < 0 And pos1 < 0 Then Exit Do  'both done by now

        Loop

        StripAmaxAmin = strCode

    End Function


    Private Function ReOrgCodeFromCallsNew(TmpCodeFolder As String) As Boolean

        'mti 2026/04/21 Revised entire procedure (replace yours)

        Try
            Dim CoreFolder$, CodeFolder$
            CoreFolder$ = StartDirec + "src\IFs.Core"
            CodeFolder$ = StartDirec + "src\IFs.Core\Models"

            ' Step a: Create TmpCode folder, deleting if exists
            If Directory.Exists(TmpCodeFolder$) Then Directory.Delete(TmpCodeFolder$, True)
            Directory.CreateDirectory(TmpCodeFolder$)

            ' Load file containing IFs model calls
            Dim filePath As String = Path.Combine(CoreFolder$, "ForecastService.Forecast.vb")
            If Not File.Exists(filePath) Then
                Console.WriteLine("File with IFs model calls not found: " & filePath)
                Console.Beep(2000, 1000)
                Return False
            End If

            ' Step: Collect all called routine names
            Dim matchedNames As New List(Of String)

            'mti 2025/08/09 start : change regex to capture IFs calls
            'Dim callPattern As String = "\b(?:Call\s+)?(ifs\w+)\s*\(\s*context\s*\)"

            'Simplified RegEx (ignoring arguments inside parentheses):
            Dim callPattern As String = "\b(?:Call\s+)?(ifs\w+)\s*\([^)]*\)"
            'Explanation:
            '\b — word boundary
            '(?:Call\s+)? — optionally match the Call keyword followed by space(s)
            '(ifs\w+) — capture function name that starts with ifs
            '\s*\( — match optional whitespace And the opening parenthesis
            '[^)]* — match anything except the closing parenthesis (i.e., function arguments, but we don't care what's inside)
            '\) — match closing parenthesis

            'mti 2025/08/09 end : change regex to capture IFs calls

            Dim callRegex As New Regex(callPattern, RegexOptions.IgnoreCase)
            Dim inModelBlock As Boolean = False

            '--- Begin - Calls for year = 1 -------------
            Dim strBeginModel As String = ("Begin IFs model firstYear").ToUpper
            Dim strEndModel As String = ("End IFs model firstYear").ToUpper
            For Each line As String In File.ReadLines(filePath)
                Dim trimmedLine As String = line.Trim()
                If trimmedLine.ToUpper.Contains(strBeginModel) Then
                    inModelBlock = True
                    Continue For
                ElseIf trimmedLine.ToUpper.Contains(strEndModel) Then
                    Exit For
                End If
                If inModelBlock Then
                    Dim match As Match = callRegex.Match(trimmedLine)
                    If match.Success Then
                        Dim name As String = match.Groups(1).Value
                        If Not matchedNames.Contains(name, StringComparer.OrdinalIgnoreCase) Then
                            matchedNames.Add(name)
                        End If
                    End If
                End If
            Next
            '--- End - Calls for year = 1 -------------

            '--- Begin - Calls for year > 1 -------------
            inModelBlock = False
            strBeginModel = ("Begin IFs Model Calls").ToUpper
            strEndModel = ("End IFs Model Calls").ToUpper
            For Each line As String In File.ReadLines(filePath)
                Dim trimmedLine As String = line.Trim()
                If trimmedLine.ToUpper.Contains(strBeginModel) Then
                    inModelBlock = True
                    Continue For
                ElseIf trimmedLine.ToUpper.Contains(strEndModel) Then
                    Exit For
                End If
                If inModelBlock Then
                    Dim match As Match = callRegex.Match(trimmedLine)
                    If match.Success Then
                        Dim name As String = match.Groups(1).Value
                        If Not matchedNames.Contains(name, StringComparer.OrdinalIgnoreCase) Then
                            matchedNames.Add(name)
                        End If
                    End If
                End If
            Next
            '--- End - Calls for year > 1 -------------

            'Map Ifs call routine to the appropriate .forecast.vb file, so that only one forecast.vb is searched for each model call routine
            Dim routineFileMap As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            For Each codeFilePath As String In Directory.GetFiles(CodeFolder$, "*.forecast.vb", SearchOption.TopDirectoryOnly)
                Dim allLines = File.ReadAllLines(codeFilePath)
                For Each name In matchedNames
                    If routineFileMap.ContainsKey(name) Then Continue For
                    If allLines.Any(Function(line) Regex.IsMatch(line, $"^\s*(Public|Private)?\s*(Sub|Function)\s+{name}\b", RegexOptions.IgnoreCase)) Then
                        routineFileMap(name) = codeFilePath
                    End If
                Next
            Next

            'Loop through code routines and the mapped.forecast.vb files and extract relevant routines and code to copy them in temporary files
            Dim NonIFsCallSub As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

            ' Defensive guard against writing the same top-level IFs routine twice to the same .calls file
            Dim writtenCallsByFile As New Dictionary(Of String, HashSet(Of String))(StringComparer.OrdinalIgnoreCase)

            For Each matchedName In matchedNames
                If routineFileMap.ContainsKey(matchedName) Then

                    Dim codeFilePath As String = routineFileMap(matchedName)
                    Dim allLines As List(Of String) = File.ReadAllLines(codeFilePath).ToList()
                    Dim fileNameOnly As String = Path.GetFileName(codeFilePath)
                    Dim callsPath As String = Path.Combine(TmpCodeFolder$, fileNameOnly & ".calls")
                    Dim subsPath As String = Path.Combine(TmpCodeFolder$, fileNameOnly & ".subs")

                    Dim allSubNames As List(Of String) = allLines.
                        Where(Function(line) Regex.IsMatch(line, "^\s*(Public|Private|Protected|Friend)?\s*(Sub|Function)\s+\w+", RegexOptions.IgnoreCase)).
                        Select(Function(line) Regex.Match(line, "\b(Sub|Function)\s+(\w+)", RegexOptions.IgnoreCase).Groups(2).Value).
                        ToList()

                    'build a list of non-IFs model Call subroutines to write in .subs later
                    For Each subName In allSubNames
                        'If Not subName.Equals(matchedName, StringComparison.OrdinalIgnoreCase) Then 'AndAlso Regex.IsMatch(l, $"\b{subName}\b", RegexOptions.IgnoreCase) Then
                        If Not matchedNames.Contains(subName) Then 'AndAlso Regex.IsMatch(l, $"\b{subName}\b", RegexOptions.IgnoreCase) Then
                            If Not NonIFsCallSub.ContainsKey(subName) Then
                                NonIFsCallSub(subName) = codeFilePath  ' fileNameOnly should be defined earlier
                            End If
                        End If
                    Next

                    Dim routineStartIdx As Integer = -1
                    For i As Integer = 0 To allLines.Count - 1
                        If Regex.IsMatch(allLines(i), $"^\s*(Public|Private)?\s*(Sub|Function)\s+{matchedName}\b", RegexOptions.IgnoreCase) Then
                            routineStartIdx = i
                            Exit For
                        End If
                    Next

                    If routineStartIdx >= 0 Then
                        ' Found the routine
                        Dim routineLines As New List(Of String)
                        Dim nesting As Integer = 0
                        For i As Integer = routineStartIdx To allLines.Count - 1
                            Dim l = allLines(i)
                            routineLines.Add(l)

                            'For Each subName In allSubNames
                            '    If Not subName.Equals(matchedName, StringComparison.OrdinalIgnoreCase) AndAlso
                            '        Regex.IsMatch(l, $"\b{subName}\b", RegexOptions.IgnoreCase) Then
                            '        If Not NonIFsCallSub.ContainsKey(subName) Then
                            '            NonIFsCallSub(subName) = codeFilePath  ' fileNameOnly should be defined earlier
                            '        End If
                            '    End If
                            'Next

                            If Regex.IsMatch(l, "^\s*(Public|Private)?\s*(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                                nesting += 1
                            ElseIf Regex.IsMatch(l, "^\s*End\s+(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                                nesting -= 1
                                If nesting <= 0 Then Exit For
                            End If
                        Next

                        If Not writtenCallsByFile.ContainsKey(callsPath) Then
                            writtenCallsByFile(callsPath) = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                        End If

                        If writtenCallsByFile(callsPath).Add(matchedName) Then
                            File.AppendAllLines(callsPath, routineLines)
                        End If
                    End If   'call is found in file : routineStartIdx >= 0
                End If  'call is mapped to a file: routineFileMap.ContainsKey(matchedName)
            Next  'iterate to next IFs calls

            ' After all matchedNames have been processed, handle NonIFsCallSub routine; i.e., subroutines in each .forecast.vb that are used by model calls in that .forecast.vb; the list is built above, while reading IFs model calls
            For Each subName In NonIFsCallSub.Keys
                Dim srcFilePath As String = NonIFsCallSub(subName)
                If File.Exists(srcFilePath) Then
                    Dim allLines As List(Of String) = File.ReadAllLines(srcFilePath).ToList()
                    Dim subStartIdx As Integer = -1
                    For i As Integer = 0 To allLines.Count - 1
                        If Regex.IsMatch(allLines(i), $"^\s*(Public|Private)?\s*(Sub|Function)\s+{subName}\b", RegexOptions.IgnoreCase) Then
                            subStartIdx = i
                            Exit For
                        End If
                    Next

                    If subStartIdx >= 0 Then
                        Dim subLines As New List(Of String)
                        Dim nesting As Integer = 0
                        For i As Integer = subStartIdx To allLines.Count - 1
                            Dim l As String = allLines(i)
                            subLines.Add(l)
                            If Regex.IsMatch(l, "^\s*(Public|Private)?\s*(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                                nesting += 1
                            ElseIf Regex.IsMatch(l, "^\s*End\s+(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                                nesting -= 1
                                If nesting <= 0 Then Exit For
                            End If
                        Next

                        Dim fileNameOnly As String = Path.GetFileName(srcFilePath)
                        Dim subsPath As String = Path.Combine(TmpCodeFolder$, fileNameOnly & ".subs")
                        File.AppendAllLines(subsPath, subLines)
                    End If
                End If
            Next

            ' Final step: Inline local subroutine calls in .calls files using corresponding .subs files

            'mti 2025/08/07 start
            'Pattern for opening and ending sub/function - stricter form
            ' Declaration: real Sub/Function header (not a lambda)
            Dim declPattern As String = "^\s*(?:<[^>]*>\s*)?" &
            "(?:(?:Public|Private|Friend|Protected|Protected\s+Friend|Shared|Static|Overloads|Overrides|MustOverride|Partial|Async|Iterator|Shadows)\s+)*" &
            "(?:Sub|Function)\b(?!\s*\()" &
            "\s+[A-Za-z_]\w*" &
            "(?:\s*Of\s*\([^)]*\))?" &
            "\s*\("

            ' End line (allow trailing comment, but nothing else)
            Dim endPattern As String = "^\s*End\s+(?:Sub|Function)\b(?:\s*'|$)"
            'mti 2025/08/07 End

            'From TmpFolder "*.Forecast.vb.calls" files will go into "*.Forecast.vb" and
            'then relevant lines from ".subs" added 'mti 2025/12/16 Victory Day added comment
            For Each callsFile In Directory.GetFiles(TmpCodeFolder$, "*.calls", SearchOption.TopDirectoryOnly)
                Dim mergedFile As String = Path.ChangeExtension(callsFile, "")
                Dim subsFile As String = Path.ChangeExtension(callsFile, ".subs")

                If Not File.Exists(subsFile) Then
                    File.Copy(callsFile, mergedFile)
                    Continue For
                End If

                Dim subsLines As List(Of String) = File.ReadAllLines(subsFile).ToList()
                Dim allSubs As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase) 'helper subs in the same .forecast .vb; not IFs calls)

                'ComputeFactorsNormScalShift(mordstDet, popgdr, Ntypes, context)
                'ApplyFactors
                'If subsFile.Contains("Health.Forecast.vb.subs") Then Stop

                ' Parse all sub/function blocks from .subs file
                Dim i As Integer = 0
                While i < subsLines.Count
                    If Regex.IsMatch(subsLines(i), "^\s*(Public|Private)?\s*(Sub|Function)\s+(\w+)\b", RegexOptions.IgnoreCase) Then
                        Dim match = Regex.Match(subsLines(i), "^\s*(Public|Private)?\s*(Sub|Function)\s+(\w+)\b", RegexOptions.IgnoreCase)
                        Dim subName As String = match.Groups(3).Value
                        Dim subBlock As New List(Of String)
                        Dim nesting As Integer = 0

                        'If subName = "Health_Forecast_Smoking_InStages" Then Stop

                        While i < subsLines.Count
                            subBlock.Add(subsLines(i))
                            'mti 2025/08/07 start
                            'If Regex.IsMatch(subsLines(i), "^\s*(Public|Private)?\s*(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                            If Regex.IsMatch(subsLines(i), declPattern, RegexOptions.IgnoreCase) Then
                                nesting += 1
                                'ElseIf Regex.IsMatch(subsLines(i), "^\s*End\s+(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                            ElseIf Regex.IsMatch(subsLines(i), endPattern, RegexOptions.IgnoreCase) Then
                                'mti 2025/08/07 end
                                nesting -= 1
                                If nesting <= 0 Then
                                    i += 1
                                    Exit While
                                End If
                            End If
                            i += 1
                        End While
                        allSubs(subName) = subBlock
                    Else
                        i += 1
                    End If
                End While

                ' Process .calls file and inline subroutine blocks recursively
                Dim inputLines As List(Of String) = File.ReadAllLines(callsFile).ToList()
                Dim outputLines As List(Of String) = InlineCodeLines(inputLines, allSubs, 0, New List(Of String)())
                File.WriteAllLines(mergedFile, outputLines)
            Next

            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
            Return False
        End Try
    End Function
    Private Function ReOrgCodeFromCalls(TmpCodeFolder As String) As Boolean

        'mti 2025/07/14 New procedure

        Try
            Dim CoreFolder$, CodeFolder$
            CoreFolder$ = StartDirec + "src\IFs.Core"
            CodeFolder$ = StartDirec + "src\IFs.Core\Models"

            ' Step a: Create TmpCode folder, deleting if exists
            If Directory.Exists(TmpCodeFolder$) Then Directory.Delete(TmpCodeFolder$, True)
            Directory.CreateDirectory(TmpCodeFolder$)

            ' Load file containing IFs model calls
            Dim filePath As String = Path.Combine(CoreFolder$, "ForecastService.Forecast.vb")
            If Not File.Exists(filePath) Then
                Console.WriteLine("File with IFs model calls not found: " & filePath)
                Console.Beep(2000, 1000)
                Return False
            End If

            ' Step: Collect all called routine names
            Dim matchedNames As New List(Of String)

            'mti 2025/08/09 start : change regex to capture IFs calls
            'Dim callPattern As String = "\b(?:Call\s+)?(ifs\w+)\s*\(\s*context\s*\)"

            'Simplified RegEx (ignoring arguments inside parentheses):
            Dim callPattern As String = "\b(?:Call\s+)?(ifs\w+)\s*\([^)]*\)"
            'Explanation:
            '\b — word boundary
            '(?Call\ s +)? — optionally match the Call keyword followed by space(s)
            '(ifs\w+) — capture function name that starts with ifs
            '\s*\( — match optional whitespace And the opening parenthesis
            '[^)]* — match anything except the closing parenthesis (i.e., function arguments, but we don't care what's inside)
            '\) — match closing parenthesis

            'mti 2025/08/09 end : change regex to capture IFs calls

            Dim callRegex As New Regex(callPattern, RegexOptions.IgnoreCase)
            Dim inModelBlock As Boolean = False


            '--- Begin - Calls for year = 1 -------------
            Dim strBeginModel As String = ("Begin IFs model firstYear").ToUpper
            Dim strEndModel As String = ("End IFs model firstYear").ToUpper
            For Each line As String In File.ReadLines(filePath)

                'mti 2026/04/21 start - trim space and comments
                If line.ToUpper.Contains(strBeginModel) Then
                    inModelBlock = True
                    Continue For
                ElseIf Line.ToUpper.Contains(strEndModel) Then
                    Exit For
                End If

                'Dim trimmedLine As String = line.Trim()
                Dim trimmedLine As String = GetActiveVBCode(line).Trim()
                'mti 2026/04/21 end - trim space and comments

                If inModelBlock Then
                    Dim match As Match = callRegex.Match(trimmedLine)
                    If match.Success Then
                        Dim name As String = match.Groups(1).Value
                        If Not matchedNames.Contains(name, StringComparer.OrdinalIgnoreCase) Then
                            matchedNames.Add(name)
                        End If
                    End If
                End If
            Next
            '--- End - Calls for year = 1 -------------

            '--- Begin - Calls for year > 1 -------------
            inModelBlock = False  'mti 2026/04/21 - needs to reset

            strBeginModel = ("Begin IFs Model Calls").ToUpper
            strEndModel = ("End IFs Model Calls").ToUpper
            For Each line As String In File.ReadLines(filePath)

                'mti 2026/04/21 start - trim space and comments
                If line.ToUpper.Contains(strBeginModel) Then
                    inModelBlock = True
                    Continue For
                ElseIf line.ToUpper.Contains(strEndModel) Then
                    Exit For
                End If
                'Dim trimmedLine As String = line.Trim()
                Dim trimmedLine As String = GetActiveVBCode(line).Trim()
                'mti 2026/04/21 end - trim space and comments

                If inModelBlock Then
                    Dim match As Match = callRegex.Match(trimmedLine)
                    If match.Success Then
                        Dim name As String = match.Groups(1).Value
                        If Not matchedNames.Contains(name, StringComparer.OrdinalIgnoreCase) Then
                            matchedNames.Add(name)
                        End If
                    End If
                End If
            Next
            '--- End - Calls for year > 1 -------------



            'Map Ifs call routine to the appropriate .forecast.vb file, so that only one forecast.vb is searched for each model call routine
            Dim routineFileMap As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            For Each codeFilePath As String In Directory.GetFiles(CodeFolder$, "*.forecast.vb", SearchOption.TopDirectoryOnly)
                Dim allLines = File.ReadAllLines(codeFilePath)
                For Each name In matchedNames
                    If routineFileMap.ContainsKey(name) Then Continue For
                    If allLines.Any(Function(line) Regex.IsMatch(line, $"^\s*(Public|Private)?\s*(Sub|Function)\s+{name}\b", RegexOptions.IgnoreCase)) Then
                        routineFileMap(name) = codeFilePath
                    End If
                Next
            Next

            'Loop through code routines and the mapped.forecast.vb files and extract relevant routines and code to  copy  them in  temporary files
            Dim NonIFsCallSub As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

            For Each matchedName In matchedNames

                'If matchedName = "IFsInfra3AllbutFirstYear" Then Stop

                If routineFileMap.ContainsKey(matchedName) Then

                    Dim codeFilePath As String = routineFileMap(matchedName)

                    'If codeFilePath.Contains("Infrastructure.Forecast.vb") Then Stop

                    Dim allLines As List(Of String) = File.ReadAllLines(codeFilePath).ToList()
                    Dim fileNameOnly As String = Path.GetFileName(codeFilePath)
                    Dim callsPath As String = Path.Combine(TmpCodeFolder$, fileNameOnly & ".calls")
                    Dim subsPath As String = Path.Combine(TmpCodeFolder$, fileNameOnly & ".subs")


                    Dim allSubNames As List(Of String) = allLines.
                            Where(Function(line) Regex.IsMatch(line, "^\s*(Public|Private|Protected|Friend)?\s*(Sub|Function)\s+\w+", RegexOptions.IgnoreCase)).
                            Select(Function(line) Regex.Match(line, "\b(Sub|Function)\s+(\w+)", RegexOptions.IgnoreCase).Groups(2).Value).
                            ToList()

                    'build a list of non-IFs model Call subroutines to write in .subs later
                    For Each subName In allSubNames

                        'mti 2026/04/21 start  - exclude HandleExtraFundNonWatSan  also excluded INFRABUDDEMNEW and MNT
                        'If Not subName.Equals(matchedName, StringComparison.OrdinalIgnoreCase) Then 'AndAlso Regex.IsMatch(l, $"\b{subName}\b", RegexOptions.IgnoreCase) Then
                        'HandleExtraFundNonWatSan
                        'If Not matchedNames.Contains(subName) Then 'AndAlso Regex.IsMatch(l, $"\b{subName}\b", RegexOptions.IgnoreCase) Then

                        'mti 2026/05/13 start - after review, it's better to exclude all HandleExtraFundNonWatSan and all CalcInfraUseExtraBudget calls
                        'theses reallocate budgget based on fund surplus and makes too many links among various fndings of different dimensions

                        'InfraBudgetAllocationWithShortage
                        'If Not matchedNames.Contains(subName) And subName <> "HandleExtraFundNonWatSan" Then 'AndAlso Regex.IsMatch(l, $"\b{subName}\b", RegexOptions.IgnoreCase) Then
                        If Not matchedNames.Contains(subName) AndAlso subName <> "HandleExtraFundNonWatSan" AndAlso Not subName.ToUpper.Contains("CalcInfraUseExtraBudget".ToUpper) AndAlso Not subName.ToUpper.Contains("InfraBudgetAllocationWithShortage".ToUpper) Then 'AndAlso Regex.IsMatch(l, $"\b{subName}\b", RegexOptions.IgnoreCase) Then
                            'mti 2026/05/13 end
                            'mti 2026/04/21 End

                            If Not NonIFsCallSub.ContainsKey(subName) Then
                                NonIFsCallSub(subName) = codeFilePath  ' fileNameOnly should be defined earlier
                            End If
                        End If
                    Next

                    Dim routineStartIdx As Integer = -1
                    For i As Integer = 0 To allLines.Count - 1
                        If Regex.IsMatch(allLines(i), $"^\s*(Public|Private)?\s*(Sub|Function)\s+{matchedName}\b", RegexOptions.IgnoreCase) Then
                            routineStartIdx = i
                            Exit For
                        End If
                    Next


                    If routineStartIdx >= 0 Then
                        ' Found the routine
                        Dim routineLines As New List(Of String)
                        Dim nesting As Integer = 0
                        For i As Integer = routineStartIdx To allLines.Count - 1
                            Dim l = allLines(i)
                            routineLines.Add(l)


                            'mti 2026/04/21 start

                            Dim declPattern1 As String =
                                        "^\s*" &
                                        "(?:(?:Public|Private|Friend|Protected|Protected\s+Friend|Private\s+Protected|" &
                                        "Shared|Overloads|Overrides|Overridable|NotOverridable|MustOverride|" &
                                        "Partial|Async|Iterator|Shadows)\s+)*" &
                                        "(Sub|Function)\s+" &
                                        "[A-Za-z_]\w*" &
                                        "(?:\s*Of\s*\([^)]*\))?" &
                                        "\s*\("

                            Dim endPattern1 As String = "^\s*End\s+(Sub|Function)\b(?:\s*'|$)"

                            If Regex.IsMatch(l, declPattern1, RegexOptions.IgnoreCase) Then
                                nesting += 1
                            ElseIf Regex.IsMatch(l, endPattern1, RegexOptions.IgnoreCase) Then
                                nesting -= 1
                                If nesting <= 0 Then Exit For
                            End If
                            'mti 2026/04/21 end

                        Next
                        File.AppendAllLines(callsPath, routineLines)
                    End If   'callis found in file :routineStartIdx >= 0 
                End If  'callis mapped to a file: routineFileMap.ContainsKey(matchedName)
            Next  'iterate to mext IFs calls
            'Next

            ' After all matchedNames have been processed, handle NonIFsCallSub routine; i.e., subroutines in each .forecast.vb that are used by model calls in that .forecast.vb; the list is built above, while reading IFs model calls
            For Each subName In NonIFsCallSub.Keys
                Dim srcFilePath As String = NonIFsCallSub(subName)
                If File.Exists(srcFilePath) Then
                    Dim allLines As List(Of String) = File.ReadAllLines(srcFilePath).ToList()
                    Dim subStartIdx As Integer = -1
                    For i As Integer = 0 To allLines.Count - 1
                        If Regex.IsMatch(allLines(i), $"^\s*(Public|Private)?\s*(Sub|Function)\s+{subName}\b", RegexOptions.IgnoreCase) Then
                            subStartIdx = i
                            Exit For
                        End If
                    Next

                    If subStartIdx >= 0 Then
                        Dim subLines As New List(Of String)
                        Dim nesting As Integer = 0
                        For i As Integer = subStartIdx To allLines.Count - 1
                            Dim l As String = allLines(i)
                            subLines.Add(l)
                            If Regex.IsMatch(l, "^\s*(Public|Private)?\s*(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                                nesting += 1
                            ElseIf Regex.IsMatch(l, "^\s*End\s+(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                                nesting -= 1
                                If nesting <= 0 Then Exit For
                            End If
                        Next

                        Dim fileNameOnly As String = Path.GetFileName(srcFilePath)
                        Dim subsPath As String = Path.Combine(TmpCodeFolder$, fileNameOnly & ".subs")
                        File.AppendAllLines(subsPath, subLines)
                    End If
                End If
            Next

            ' Final step: Inline local subroutine calls in .calls files using corresponding .subs files

            'mti 2025/08/07 start
            'Pattern for opening and ending sub/function - stricter form
            ' Declaration: real Sub/Function header (not a lambda)
            Dim declPattern As String = "^\s*(?:<[^>]*>\s*)?" &
                "(?:(?:Public|Private|Friend|Protected|Protected\s+Friend|Shared|Static|Overloads|Overrides|MustOverride|Partial|Async|Iterator|Shadows)\s+)*" &
                "(?:Sub|Function)\b(?!\s*\()" & _              ' reject: Function(...
                "\s+[A-Za-z_]\w*" & _                          ' name
                "(?:\s*Of\s*\([^)]*\))?" & _                   ' optional generics
                "\s*\("                                         ' opening paren of parameter list

            ' End line (allow trailing comment, but nothing else)
            Dim endPattern As String = "^\s*End\s+(?:Sub|Function)\b(?:\s*'|$)"
            'mti 2025/08/07 End

            'From TmpFolder "*.Forecast.vb.calls" files will  go into "*.Forecast.vb" and
            'then relevant lines from ".subs" added 'mti 2025/12/16 Victory Day added comment
            For Each callsFile In Directory.GetFiles(TmpCodeFolder$, "*.calls", SearchOption.TopDirectoryOnly)
                Dim mergedFile As String = Path.ChangeExtension(callsFile, "")
                Dim subsFile As String = Path.ChangeExtension(callsFile, ".subs")

                If Not File.Exists(subsFile) Then
                    File.Copy(callsFile, mergedFile)
                    Continue For
                End If

                Dim subsLines As List(Of String) = File.ReadAllLines(subsFile).ToList()
                Dim allSubs As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase) 'helper subs in the same .forecast .vb; not IFs calls)

                'ComputeFactorsNormScalShift(mordstDet, popgdr, Ntypes, context)
                'ApplyFactors
                'If subsFile.Contains("Health.Forecast.vb.subs") Then Stop

                ' Parse all sub/function blocks from .subs file
                Dim i As Integer = 0
                While i < subsLines.Count
                    If Regex.IsMatch(subsLines(i), "^\s*(Public|Private)?\s*(Sub|Function)\s+(\w+)\b", RegexOptions.IgnoreCase) Then
                        Dim match = Regex.Match(subsLines(i), "^\s*(Public|Private)?\s*(Sub|Function)\s+(\w+)\b", RegexOptions.IgnoreCase)
                        Dim subName As String = match.Groups(3).Value
                        Dim subBlock As New List(Of String)
                        Dim nesting As Integer = 0

                        'If subName = "Health_Forecast_Smoking_InStages" Then Stop

                        While i < subsLines.Count
                            subBlock.Add(subsLines(i))
                            'mti 2025/08/07 start
                            'If Regex.IsMatch(subsLines(i), "^\s*(Public|Private)?\s*(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                            If Regex.IsMatch(subsLines(i), declPattern, RegexOptions.IgnoreCase) Then
                                nesting += 1
                                'ElseIf Regex.IsMatch(subsLines(i), "^\s*End\s+(Sub|Function)\b", RegexOptions.IgnoreCase) Then
                            ElseIf Regex.IsMatch(subsLines(i), endPattern, RegexOptions.IgnoreCase) Then
                                'mti 2025/08/07 end
                                nesting -= 1
                                If nesting <= 0 Then
                                    i += 1
                                    Exit While
                                End If
                            End If
                            i += 1
                        End While
                        allSubs(subName) = subBlock
                    Else
                        i += 1
                    End If
                End While

                ' Process .calls file and inline subroutine blocks recursively
                Dim inputLines As List(Of String) = File.ReadAllLines(callsFile).ToList()
                'Dim outputLines As List(Of String) = InlineCodeLines(inputLines, allSubs, 0, New List(Of String)())
                Dim outputLines As List(Of String) = InlineCodeLines(inputLines, allSubs, 0, New List(Of String)())
                File.WriteAllLines(mergedFile, outputLines)
            Next

            Return True
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function InlineCodeLines(lines As List(Of String),
                                 allSubs As Dictionary(Of String, List(Of String)),
                                 depth As Integer,
                                 callStack As List(Of String)) As List(Of String)

        'mti 2025/12/16 Victory Day, Take Entire Procedure: Proc Updated for:
        '  a. function call handling
        '  b. declaration parsing using paren-balance (multiline + comments safe)
        '  c. physical->logical line building strips per-line trailing comments and "_" tokens

        Try
            Dim outputLines As New List(Of String)

            Dim continuedLine As String = ""
            Dim inContinuation As Boolean = False
            Dim indent As String = New String(" "c, depth * 4)
            Dim tempLines As New List(Of String)

            'mti 2025/12/16 Victory Day Start
            'Precompute which routines are Functions (vs Subs)
            Dim isFunction As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)
            For Each k In allSubs.Keys
                isFunction(k) = IsFunctionBlock(allSubs(k))
            Next
            'mti 2025/12/16 Victory Day End

            ' ---- helpers ----
            Dim addPossiblySplit As Action(Of String) =
        Sub(l As String)
            If l.TrimStart().StartsWith("'") Then
                outputLines.Add(l)
                Return
            End If
            Dim split As List(Of String) = TrySplitSingleLineIf(l)
            If split IsNot Nothing Then
                outputLines.AddRange(split)
            Else
                outputLines.Add(l)
            End If
        End Sub
            ' ---- helpers end ----

            For Each line As String In lines

                'mti 2025/12/16 Victory Day Start
                'Handle continued lines (physical -> logical) safely:
                ' - remove per-line trailing comments
                ' - remove "_" tokens
                ' - join with spaces
                If inContinuation Then
                    tempLines.Add(line)
                Else
                    tempLines.Clear()
                    tempLines.Add(line)
                End If

                continuedLine = BuildLogicalLineFromTemp(tempLines)

                'Continuation decision should be based on the normalized physical line,
                'NOT the raw one (because raw may contain trailing comments)
                Dim lastNorm As String = NormalizePhysicalLineForJoin(line)
                inContinuation = LineLikelyContinued(lastNorm)

                If inContinuation Then Continue For
                'mti 2025/12/16 Victory Day End

                Dim matched As Boolean = False

                If Not continuedLine.TrimStart().StartsWith("'") Then
                    For Each subName In allSubs.Keys

                        'continuedLine already has comments stripped per physical line,
                        'but keep this for safety if any quotes/comments slipped through
                        Dim codePart As String = continuedLine.Split("'"c)(0).Trim()

                        If Regex.IsMatch(codePart, $"\b{Regex.Escape(subName)}\s*\(", RegexOptions.IgnoreCase) Then

                            'mti 2025/12/16 Victory Day Start
                            'If this is a Function, do NOT inline; rewrite dependency expression
                            If isFunction.ContainsKey(subName) AndAlso isFunction(subName) Then
                                Dim argsPassed() As String = ExtractArguments(codePart, subName)
                                Dim fnBlock As List(Of String) = allSubs(subName)
                                Dim rewritten As String = RewriteLineForFunctionCall(continuedLine, subName, argsPassed, fnBlock)

                                addPossiblySplit(rewritten)
                                matched = True
                                Exit For
                            End If
                            'mti 2025/12/16 Victory Day End

                            ' Subs: inline (avoid recursion loops)
                            If Not callStack.Contains(subName, StringComparer.OrdinalIgnoreCase) Then
                                matched = True

                                addPossiblySplit(continuedLine)

                                Dim newCallStack = New List(Of String)(callStack) From {subName}
                                Dim levelDesc = String.Join(":", newCallStack)
                                outputLines.Add($"{indent}' ---- Begin Inlined Level {depth + 1}: {levelDesc}")

                                Dim subBlock As List(Of String) = allSubs(subName)

                                'mti 2025/12/16 Victory Day Start
                                'Multiline-safe declaration parsing (paren-balance + comment-safe)
                                Dim declLine As String = GetLogicalDeclarationLine(subBlock)
                                Dim subParams() As String = ExtractParamNames(declLine)
                                'mti 2025/12/16 Victory Day End

                                Dim argsPassed() As String = ExtractArguments(codePart, subName)

                                Dim paramMap As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

                                For argCnt As Integer = 0 To Math.Min(subParams.Length, argsPassed.Length) - 1

                                    Dim paramName As String = subParams(argCnt).Trim()
                                    Dim passedValue As String = argsPassed(argCnt).Trim()

                                    If passedValue <> "" AndAlso Not passedValue.Equals("Not Passed", StringComparison.OrdinalIgnoreCase) Then


                                        'mti 2026/05/13  start
                                        'Special handling for gender-like parameters.
                                        '
                                        'If the parameter name contains Gender or gndr,
                                        'then the passed argument should be normalized.
                                        '
                                        'Accepted values:
                                        'Male, Female, Total, 1, 2, 3
                                        '
                                        'Also convert:
                                        'M -> Male
                                        'F -> Female
                                        '
                                        'For anything else, keep the parameter name itself
                                        'as the passed value. This avoids incorrectly replacing
                                        'a gender parameter with an unrelated local variable.

                                        If IsGenderParameterName(paramName) Then

                                            If passedValue.Equals("Male", StringComparison.OrdinalIgnoreCase) OrElse
                                               passedValue.Equals("Female", StringComparison.OrdinalIgnoreCase) OrElse
                                               passedValue.Equals("Total", StringComparison.OrdinalIgnoreCase) OrElse
                                               passedValue = "1" OrElse
                                               passedValue = "2" OrElse
                                               passedValue = "3" Then

                                                'Already valid; keep as passed.
                                                paramMap(paramName) = passedValue

                                            ElseIf passedValue.Equals("M", StringComparison.OrdinalIgnoreCase) Then

                                                paramMap(paramName) = "Male"

                                            ElseIf passedValue.Equals("F", StringComparison.OrdinalIgnoreCase) Then

                                                paramMap(paramName) = "Female"

                                            Else

                                                'Anything else should not replace the gender parameter.
                                                'Use the parameter name itself.
                                                paramMap(paramName) = paramName

                                            End If

                                        Else

                                            'Regular non-gender parameter.
                                            paramMap(paramName) = passedValue

                                        End If

                                        'paramMap(paramName) = passedValue
                                        'mti 2026/05/13 end

                                    End If
                                Next

                                Dim inlinedBlock As List(Of String) =
                                InlineCodeLines(subBlock, allSubs, depth + 1, newCallStack)

                                Dim inDeclaration As Boolean = True

                                'mti 2025/12/16 Victory Day Start
                                'Track declaration lines (comment-stripped) and end header using paren-balance
                                Dim declParts As New List(Of String)()
                                'mti 2025/12/16 Victory Day End

                                For i = 0 To inlinedBlock.Count - 1
                                    Dim rawLine As String = inlinedBlock(i)
                                    Dim indentedLine As String = indent & rawLine

                                    If i = inlinedBlock.Count - 1 Then
                                        outputLines.Add("' " & indentedLine.TrimStart())
                                        Continue For
                                    End If

                                    If inDeclaration Then
                                        outputLines.Add("' " & indentedLine.TrimStart())

                                        'mti 2025/12/16 Victory Day Start
                                        declParts.Add(RemoveCommentOutsideQuotes(rawLine))
                                        If DeclarationHeaderComplete(declParts) Then
                                            inDeclaration = False
                                        End If
                                        'mti 2025/12/16 Victory Day End
                                    Else
                                        Dim substitutedLine As String = rawLine
                                        For Each kvp In paramMap
                                            substitutedLine =
                                            Regex.Replace(substitutedLine,
                                                          $"\b{Regex.Escape(kvp.Key)}\b",
                                                          kvp.Value)
                                        Next
                                        addPossiblySplit(indent & substitutedLine)
                                    End If
                                Next

                                outputLines.Add($"{indent}' ---- End Inlined Level {depth + 1}: {levelDesc}")
                                Exit For
                            End If
                        End If
                    Next
                End If

                If Not matched Then
                    'tempLines are physical lines; write them as-is (not normalized)
                    For Each tl In tempLines
                        addPossiblySplit(tl)
                    Next
                End If

                tempLines.Clear()
                continuedLine = ""
            Next

            Return outputLines

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
            Stop
            Return New List(Of String)
        End Try
    End Function
    Private Function IsGenderParameterName(ByVal paramName As String) As Boolean
        'mti 2026/05/13 new procedure
        If paramName Is Nothing Then Return False

        If paramName.IndexOf("Gender", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return True
        End If

        If paramName.IndexOf("gndr", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return True
        End If

        Return False

    End Function
    Private Function GetActiveVBCode(line As String) As String
        'mti 2026/04/16 start - new procedure
        If String.IsNullOrEmpty(line) Then Return String.Empty

        Dim inString As Boolean = False
        Dim i As Integer = 0

        While i < line.Length
            Dim ch As Char = line(i)

            If ch = """"c Then
                ' Handle doubled quotes inside VB strings: ""
                If inString AndAlso i + 1 < line.Length AndAlso line(i + 1) = """"c Then
                    i += 2
                    Continue While
                End If

                inString = Not inString
                i += 1
                Continue While
            End If

            If Not inString Then
                ' Apostrophe comment
                If ch = "'"c Then
                    Return line.Substring(0, i)
                End If

                ' REM comment
                If String.Compare(line, i, "Rem", 0, 3, True) = 0 Then
                    Dim atStart As Boolean = (i = 0 OrElse Char.IsWhiteSpace(line(i - 1)) OrElse line(i - 1) = ":"c)
                    Dim properEnd As Boolean = (i + 3 = line.Length OrElse Char.IsWhiteSpace(line(i + 3)))

                    If atStart AndAlso properEnd Then
                        Return line.Substring(0, i)
                    End If
                End If
            End If

            i += 1
        End While

        Return line
    End Function

    'mti 2025/12/16 Victory Day Deleted Routine InlineCodeLines_12172025
    Private Function NormalizePhysicalLineForJoin(physicalLine As String) As String
        'mti 2025/12/16 Victory Day - new procedure
        'Normalize a single physical line for logical-line joining:
        '  - strip trailing comment outside quotes
        '  - remove explicit line-continuation "_" token (and any whitespace around it)
        '  - trim ends (but keep inner spaces)

        If physicalLine Is Nothing Then Return ""

        Dim s As String = RemoveCommentOutsideQuotes(physicalLine).TrimEnd()

        'Remove explicit continuation underscore at end: " _" or "_" (with optional whitespace)
        s = Regex.Replace(s, "\s*_\s*$", "", RegexOptions.None).TrimEnd()

        Return s
    End Function
    Private Function BuildLogicalLineFromTemp(tempLines As List(Of String)) As String
        'mti 2025/12/16 Victory Day - new procedure
        'Build a logical line by joining normalized physical lines with spaces.

        Dim parts As New List(Of String)
        For Each pl In tempLines
            Dim norm As String = NormalizePhysicalLineForJoin(pl)
            If norm.Trim().Length = 0 Then Continue For
            parts.Add(norm.Trim())
        Next

        Return String.Join(" ", parts).Trim()
    End Function
    Private Function GetLogicalDeclarationLine(block As List(Of String)) As String
        'mti 2025/12/16 Victory Day - new procedure
        'Combine multiline Sub/Function declaration safely even if each continued line has a trailing comment.

        Dim parts As New List(Of String)

        For Each l In block
            If l Is Nothing Then Continue For

            Dim codeLine As String = RemoveCommentOutsideQuotes(l).TrimEnd()
            If codeLine.Trim().Length = 0 Then Continue For

            parts.Add(codeLine)

            If DeclarationHeaderComplete(parts) Then Exit For
        Next

        Return String.Join(" ", parts).Trim()
    End Function

    Private Function DeclarationHeaderComplete(parts As List(Of String)) As Boolean
        'mti 2025/12/16 Victory Day - new procedure
        'Returns True when the Sub/Function header's parameter list is fully closed.
        'Robust to commas, array ranks like (,,), and comments already stripped per-line.

        Dim s As String = String.Join(" ", parts)

        Dim inStr As Boolean = False
        Dim seenFirstParen As Boolean = False
        Dim depth As Integer = 0

        Dim i As Integer = 0
        While i < s.Length
            Dim ch As Char = s(i)

            If ch = """"c Then
                ' handle escaped quotes ""
                If i + 1 < s.Length AndAlso s(i + 1) = """"c Then
                    i += 2
                    Continue While
                Else
                    inStr = Not inStr
                    i += 1
                    Continue While
                End If
            End If

            If Not inStr Then
                If ch = "("c Then
                    seenFirstParen = True
                    depth += 1
                ElseIf ch = ")"c Then
                    If seenFirstParen Then
                        depth -= 1
                        If depth = 0 Then Return True
                    End If
                End If
            End If

            i += 1
        End While

        ' If no "(" was ever seen, fallback to heuristic (rare for subs/functions)
        If Not seenFirstParen Then
            Return Not LineLikelyContinued(s)
        End If

        Return False
    End Function
    Private Function IsFunctionBlock(subBlock As List(Of String)) As Boolean
        'mti 2025/12/16 Victory Day - new procedure
        'This determines whether the given block is a Function (vs Sub)
        If subBlock Is Nothing OrElse subBlock.Count = 0 Then Return False
        Dim decl As String = GetLogicalDeclarationLine(subBlock)
        Return Regex.IsMatch(decl, "\bFunction\b", RegexOptions.IgnoreCase)
    End Function

    Private Function RewriteLineForFunctionCallOld(originalLine As String,
                                            fnName As String,
                                            argsPassed() As String,
                                            fnBlock As List(Of String)) As String
        'mti 2025/12/16 Victory Day - new procedure
        'This rewrites a line containing a function call putting function parameters in the equation
        'and saving the original line with @@ markers at  the end of the rewritten line
        'also identifies any IFsVar or parameter used in the function body and adds them to the dependency expression

        Dim originalFull As String = originalLine.TrimEnd()

        Dim idxComment As Integer = IndexOfCommentMarkerOutsideQuotes(originalLine)
        Dim codePart As String = If(idxComment >= 0,
                                originalLine.Substring(0, idxComment),
                                originalLine)
        Dim commentPart As String = If(idxComment >= 0,
                                   originalLine.Substring(idxComment).TrimEnd(),
                                   "")

        Dim m = Regex.Match(codePart, $"\b{Regex.Escape(fnName)}\s*\(",
                        RegexOptions.IgnoreCase)
        If Not m.Success Then Return originalLine

        Dim repStart As Integer, repEnd As Integer
        If Not TryGetInvocationRangeWithWrappers(codePart, m.Index, repStart, repEnd) Then
            Return originalLine
        End If

        'Build a set of var/param names already covered by passed arguments
        Dim alreadyIncluded As HashSet(Of String) = GetModelVarsFoundInArgs(argsPassed)

        'Find additional live IFs variables/parameters used inside the function body
        Dim extras As List(Of String) = GetModelVarsUsedInFunctionBlock(fnBlock, alreadyIncluded)

        'Create the dependency expression: args + extras
        Dim depParts As New List(Of String)

        depParts.AddRange(argsPassed.Select(Function(a) a.Trim()).
                              Where(Function(a) a <> "" AndAlso
                                               Not a.Equals("Not Passed", StringComparison.OrdinalIgnoreCase)))

        'Add extras as plain names (avoid duplicates)
        depParts.AddRange(extras)

        Dim argExpr As String = String.Join(" + ", depParts)

        Dim opPos As Integer = FindLeadingPlusMinus(codePart, repStart)
        If opPos >= 0 Then
            repStart = opPos
            argExpr = " + " & argExpr
        End If

        Dim newCode =
        codePart.Substring(0, repStart) &
        argExpr &
        codePart.Substring(repEnd + 1)

        newCode = newCode.TrimEnd()

        Dim rewritten As String = newCode
        If commentPart <> "" Then rewritten &= " " & commentPart
        rewritten &= " @@" & originalFull & "@@"

        Return rewritten
    End Function
    Private Function RewriteLineForFunctionCall(originalLine As String,
                                           fnName As String,
                                           argsPassed() As String,
                                           fnBlock As List(Of String)) As String
        'mti 2026/05/13 new procedure
        'mti 2025/12/16 Victory Day - new procedure
        'This rewrites a line containing a function call putting function parameters in the equation
        'and saving the original line with @@ markers at  the end of the rewritten line
        'also identifies any IFsVar or parameter used in the function body and adds them to the dependency expression

        Dim originalFull As String = originalLine.TrimEnd()

        'If originalFull.Contains("LABCOEFFS(R%, S%, Unskilled) = LaborF(R%, Unskilled, S%, context)") Then Stop

        Dim idxComment As Integer = IndexOfCommentMarkerOutsideQuotes(originalLine)
        Dim codePart As String = If(idxComment >= 0,
                            originalLine.Substring(0, idxComment),
                            originalLine)
        Dim commentPart As String = If(idxComment >= 0,
                               originalLine.Substring(idxComment).TrimEnd(),
                               "")

        Dim m = Regex.Match(codePart, $"\b{Regex.Escape(fnName)}\s*\(",
                    RegexOptions.IgnoreCase)
        If Not m.Success Then Return originalLine

        Dim repStart As Integer, repEnd As Integer
        If Not TryGetInvocationRangeWithWrappers(codePart, m.Index, repStart, repEnd) Then
            Return originalLine
        End If


        'mti 2026/05/13 start
        'Do not treat quoted string arguments as IFs variables.
        '
        'Example:
        'PAFCOOKSTDD(rrCOOKSTSTDD, "M", sAgeRange, R%, COOKSTOVESDD)
        '
        'The argument "M" means Male. It should not be interpreted as
        'the model variable M, which may stand for imports.
        '
        'So build a cleaned argument list for dependency detection.
        Dim cleanArgsForVarScan As New List(Of String)

        For Each a As String In argsPassed

            Dim cleanArg As String = CleanFunctionCallArgForDependency(a)

            If cleanArg <> "" Then
                cleanArgsForVarScan.Add(cleanArg)
            End If

        Next
        'mti 2026/05/13 end


        'Build a set of var/param names already covered by passed arguments
        'mti 2026/05/13 updated: use cleaned args, not raw argsPassed
        Dim alreadyIncluded As HashSet(Of String) =
        GetModelVarsFoundInArgs(cleanArgsForVarScan.ToArray())


        'Find additional live IFs variables/parameters used inside the function body
        Dim extras As List(Of String) = GetModelVarsUsedInFunctionBlock(fnBlock, alreadyIncluded)

        'Create the dependency expression: args + extras
        Dim depParts As New List(Of String)


        'mti 2026/05/13 start
        'Add passed arguments, but skip quoted string constants.
        '
        'This prevents:
        '"M" -> M
        '"F" -> F
        '
        'from becoming variable dependencies.
        For Each a As String In argsPassed

            Dim cleanArg As String = CleanFunctionCallArgForDependency(a)

            If cleanArg <> "" AndAlso
           Not cleanArg.Equals("Not Passed", StringComparison.OrdinalIgnoreCase) Then

                depParts.Add(cleanArg)

            End If

        Next
        'mti 2026/05/13 end


        'Add extras as plain names (avoid duplicates)
        depParts.AddRange(extras)

        Dim argExpr As String = String.Join(" + ", depParts)

        Dim opPos As Integer = FindLeadingPlusMinus(codePart, repStart)
        If opPos >= 0 Then
            repStart = opPos
            argExpr = " + " & argExpr
        End If

        Dim newCode =
    codePart.Substring(0, repStart) &
    argExpr &
    codePart.Substring(repEnd + 1)

        newCode = newCode.TrimEnd()

        Dim rewritten As String = newCode
        If commentPart <> "" Then rewritten &= " " & commentPart
        rewritten &= " @@" & originalFull & "@@"

        Return rewritten

    End Function

    Private Function CleanFunctionCallArgForDependency(ByVal argText As String) As String
        'mti 2026/05/13 new procedure
        'Do not treat constants or scalar string parameters as model-variable dependencies.
        '
        'Examples skipped:
        '
        '"M"        -> ""
        '"F"        -> ""
        '"Male"     -> ""
        '1          -> ""
        '2          -> ""
        'sAgeRange  -> ""
        '
        'Examples kept:
        '
        'rrCOOKSTSTDD       -> rrCOOKSTSTDD
        'R%                 -> R%
        'COOKSTOVESDD       -> COOKSTOVESDD
        'sSomething(R%)     -> sSomething(R%)
        '
        'The string-variable rule is intentionally conservative:
        'it skips simple scalar variables that look like sName or strName,
        'but it does not skip dimensioned/array expressions with parentheses.

        If argText Is Nothing Then Return ""

        Dim x As String = argText.Trim

        If x = "" Then Return ""

        If x.Equals("Not Passed", StringComparison.OrdinalIgnoreCase) Then Return ""

        'Skip normal quoted strings.
        If x.Length >= 2 AndAlso x.StartsWith("""") AndAlso x.EndsWith("""") Then
            Return ""
        End If

        'Skip character-style quoted values, just in case they occur.
        If x.Length >= 2 AndAlso x.StartsWith("'") AndAlso x.EndsWith("'") Then
            Return ""
        End If

        'Skip integer literals.
        'These usually represent sub-dimension indices.
        Dim tmpInt As Integer = 0
        If Integer.TryParse(x, tmpInt) Then
            Return ""
        End If

        'Skip simple scalar string variables, but not arrays/dimensioned references.
        '
        'Examples skipped:
        'sAgeRange
        'strAgeRange
        '
        'Examples not skipped:
        'sAgeRange(R%)
        'strName(j)
        If Not x.Contains("(") AndAlso IsLikelyScalarStringVariable(x) Then
            Return ""
        End If

        Return x

    End Function
    'mti 2026/05/13 end

    Private Function IsLikelyScalarStringVariable(ByVal argText As String) As Boolean
        'mti 2026/05/13 new procedure
        'Conservative string-variable naming check.
        '
        'This catches local scalar string parameters such as:
        'sAgeRange
        'sGender
        'strAgeRange
        '
        'It avoids catching uppercase model variables such as:
        'SVDTHSCHILTOT
        '
        'because those do not start with lowercase s/str in the original code text.

        If argText Is Nothing Then Return False

        Dim x As String = argText.Trim

        If x = "" Then Return False

        'Only simple variable names.
        If Not Regex.IsMatch(x, "^[A-Za-z_][A-Za-z0-9_]*[$%&!#@]?$") Then
            Return False
        End If

        'Remove VB type suffix if present.
        x = x.TrimEnd("$"c, "%"c, "&"c, "!"c, "#"c, "@"c)

        'sAgeRange style.
        If Regex.IsMatch(x, "^s[A-Z][A-Za-z0-9_]*$") Then
            Return True
        End If

        'strAgeRange style.
        If Regex.IsMatch(x, "^str[A-Z][A-Za-z0-9_]*$") Then
            Return True
        End If

        Return False

    End Function

    Private Function GetModelVarsFoundInArgs(argsPassed() As String) As HashSet(Of String)
        'mti 2025/12/16 Victory Day - new procedure
        'This builds a set of IFs variable/parameter names found in the passed arguments
        Dim setVars As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        If argsPassed Is Nothing Then Return setVars

        Dim argsText As String = " " & String.Join(" ", argsPassed.Where(Function(a) a IsNot Nothing)) & " "

        For i As Integer = 1 To NumbVars
            Dim vname As String = If(V$(i), "").Trim()
            If vname = "" Then Continue For

            If Regex.IsMatch(argsText, $"\b{Regex.Escape(vname)}\b", RegexOptions.IgnoreCase) Then
                setVars.Add(vname)
            End If
        Next

        Return setVars
    End Function
    Private Function GetModelVarsUsedInFunctionBlockOld(fnBlock As List(Of String),
                                                alreadyIncluded As HashSet(Of String)) As List(Of String)
        'mti 2025/12/16 Victory Day - new procedure
        'this finds IFs variable/parameter names used inside the function body that are not already in alreadyIncluded set

        Dim found As New List(Of String)
        If fnBlock Is Nothing OrElse fnBlock.Count = 0 Then Return found
        If alreadyIncluded Is Nothing Then alreadyIncluded = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        'Build “live code” text from function body (exclude declaration + End Function)
        Dim bodyText As String = GetLiveFunctionBodyText(fnBlock)

        For i As Integer = 1 To NumbVars
            Dim vname As String = If(V$(i), "").Trim()
            If vname = "" Then Continue For
            If alreadyIncluded.Contains(vname) Then Continue For

            If Regex.IsMatch(bodyText, $"\b{Regex.Escape(vname)}\b", RegexOptions.IgnoreCase) Then
                alreadyIncluded.Add(vname)
                found.Add(vname)
            End If
        Next

        Return found
    End Function
    Private Function GetModelVarsUsedInFunctionBlock(fnBlock As List(Of String),
                                            alreadyIncluded As HashSet(Of String)) As List(Of String)
        'mti 2026/05/13 Updated procedure
        'mti 2025/12/16 Victory Day - new procedure
        'this finds IFs variable/parameter names used inside the function body that are not already in alreadyIncluded set

        Dim found As New List(Of String)
        If fnBlock Is Nothing OrElse fnBlock.Count = 0 Then Return found
        If alreadyIncluded Is Nothing Then alreadyIncluded = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        'Build “live code” text from function body (exclude declaration + End Function)
        Dim bodyText As String = GetLiveFunctionBodyText(fnBlock)

        'mti 2026/05/13 start
        'Remove quoted string literals and comments before searching for model variables.
        '
        'This avoids false links such as:
        '
        'func_name = func_name + " (Power)"
        '
        'where "Power" is text, not the model variable POWER.
        bodyText = RemoveVBStringsAndCommentsForVarSearch(bodyText)
        'mti 2026/05/13 end

        For i As Integer = 1 To NumbVars
            Dim vname As String = If(V$(i), "").Trim()
            If vname = "" Then Continue For
            If alreadyIncluded.Contains(vname) Then Continue For

            If Regex.IsMatch(bodyText, $"\b{Regex.Escape(vname)}\b", RegexOptions.IgnoreCase) Then
                alreadyIncluded.Add(vname)
                found.Add(vname)
            End If
        Next

        Return found

    End Function

    Private Function RemoveVBStringsAndCommentsForVarSearch(ByVal codeText As String) As String
        'mti 2026/05/13 new helper procedure
        'Removes VB string literals and comments before searching for model variables.
        '
        'Examples removed:
        '
        '"Power"
        '"Agriculture"
        '"GDP/Capita"
        '
        'Comments beginning with apostrophe are also removed, but only when
        'the apostrophe is outside a quoted string.
        '
        'This prevents literal text from being interpreted as IFs model variables.

        If codeText Is Nothing Then Return ""

        Dim out As New System.Text.StringBuilder

        Dim lines() As String = codeText.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Split(vbLf)

        For Each oneLine As String In lines

            Dim inString As Boolean = False
            Dim i As Integer = 0

            Do While i < oneLine.Length

                Dim ch As Char = oneLine(i)

                If inString Then

                    If ch = """"c Then

                        'Handle doubled quote inside a VB string.
                        'Example: "He said ""Hello"""
                        If i + 1 < oneLine.Length AndAlso oneLine(i + 1) = """"c Then
                            i += 2
                            Continue Do
                        Else
                            inString = False
                            out.Append(" "c)
                        End If

                    Else

                        'Inside string literal: replace text with space.
                        out.Append(" "c)

                    End If

                Else

                    If ch = "'"c Then

                        'Comment starts outside a string.
                        'Ignore the rest of this line.
                        Exit Do

                    ElseIf ch = """"c Then

                        'String starts.
                        inString = True
                        out.Append(" "c)

                    Else

                        'Real code character.
                        out.Append(ch)

                    End If

                End If

                i += 1

            Loop

            out.AppendLine()

        Next

        Return out.ToString()

    End Function

    Private Function GetLiveFunctionBodyText(fnBlock As List(Of String)) As String
        'mti 2025/12/16 Victory Day - new procedure
        'This builds a single string containing the live code of the function body,

        Dim sb As New System.Text.StringBuilder()

        Dim inDecl As Boolean = True
        Dim declParts As New List(Of String)

        For Each raw In fnBlock
            If raw Is Nothing Then Continue For

            Dim trimmed As String = raw.TrimStart()
            If trimmed.StartsWith("'") Then
                'Skip full-line comments
                Continue For
            End If

            Dim codeLine As String = RemoveCommentOutsideQuotes(raw).TrimEnd()
            If codeLine = "" Then Continue For

            'Skip End Function line
            If Regex.IsMatch(codeLine, "^\s*End\s+Function\b", RegexOptions.IgnoreCase) Then
                Continue For
            End If

            If inDecl Then
                declParts.Add(codeLine)
                If Not LineLikelyContinued(String.Join("", declParts)) Then
                    inDecl = False 'declaration finished
                End If
                Continue For
            End If

            sb.AppendLine(codeLine)
        Next

        Return sb.ToString()
    End Function
    Private Function FindEndOfParenGroup(s As String, openParenIdx As Integer) As Integer
        'mti 2025/12/16 Victory Day - new procedure
        'This finds the index of the matching closing parenthesis for the opening parenthesis at openParenIdx
        If openParenIdx < 0 OrElse openParenIdx >= s.Length OrElse s(openParenIdx) <> "("c Then Return -1
        Dim depth As Integer = 0
        For i As Integer = openParenIdx To s.Length - 1
            If s(i) = "("c Then depth += 1
            If s(i) = ")"c Then
                depth -= 1
                If depth = 0 Then Return i
            End If
        Next
        Return -1
    End Function
    Private Function FindLeadingPlusMinus(code As String, startIdx As Integer) As Integer
        'mti 2025/12/16 Victory Day - new procedure
        'This finds if there is a leading + or - operator before the expression starting at startIdx
        Dim i As Integer = startIdx - 1
        While i >= 0 AndAlso Char.IsWhiteSpace(code(i))
            i -= 1
        End While
        If i >= 0 AndAlso (code(i) = "+"c OrElse code(i) = "-"c) Then Return i
        Return -1
    End Function
    Private Function TryGetInvocationRangeWithWrappers(code As String,
                                                   fnNameIdx As Integer,
                                                   ByRef startIdx As Integer,
                                                   ByRef endIdx As Integer) As Boolean
        'mti 2025/12/16 Victory Day - new procedure
        'This finds the full range of a function invocation (call) including any wrapping parentheses

        Dim openFn = code.IndexOf("("c, fnNameIdx)
        If openFn < 0 Then Return False

        Dim closeFn = FindEndOfParenGroup(code, openFn)
        If closeFn < 0 Then Return False

        startIdx = fnNameIdx
        endIdx = closeFn

        Dim expanded As Boolean
        Do
            expanded = False

            For i As Integer = startIdx - 1 To 0 Step -1
                If code(i) = "("c Then
                    Dim closeOuter = FindEndOfParenGroup(code, i)
                    If closeOuter >= endIdx Then
                        Dim insideLeft = code.Substring(i + 1, startIdx - (i + 1)).Trim()
                        Dim insideRight = code.Substring(endIdx + 1, closeOuter - (endIdx + 1)).Trim()

                        If insideLeft = "" AndAlso insideRight = "" Then
                            Dim j = i - 1
                            While j >= 0 AndAlso Char.IsWhiteSpace(code(j))
                                j -= 1
                            End While
                            While j >= 0 AndAlso (Char.IsLetterOrDigit(code(j)) OrElse code(j) = "_"c)
                                j -= 1
                            End While

                            startIdx = j + 1
                            endIdx = closeOuter
                            expanded = True
                            Exit For
                        End If
                    End If
                End If
            Next

        Loop While expanded

        Return True
    End Function
    ' ---- helpers (place in the same module/class) ----

    ' Splits a single-line If ... Then <stmt> (no Else, no End If on the line) into 3 lines, preserving indent.
    ' Returns Nothing if not a single-line If eligible for splitting.
    Private Function TrySplitSingleLineIf(line As String) As List(Of String)
        'mti 2025/12/16 Victory Day comment added
        'This splits single-line If ... Then statements into multi-line form

        If String.IsNullOrWhiteSpace(line) Then Return Nothing
        If line.TrimStart().StartsWith("'") Then Return Nothing

        ' Separate code and trailing comment (simple, assumes no unmatched quotes in code)
        Dim code As String = line
        Dim commentTail As String = ""
        Dim idx As Integer = IndexOfCommentMarkerOutsideQuotes(line)
        If idx >= 0 Then
            code = line.Substring(0, idx)
            commentTail = line.Substring(idx) ' includes "'"
        End If

        Dim m = Regex.Match(code, "^(?<indent>\s*)If\s+(?<cond>.+?)\s+Then\s+(?<stmt>.+?)\s*$", RegexOptions.IgnoreCase)
        If Not m.Success Then Return Nothing

        ' Disallow lines that contain Else or End If on the same physical line
        If Regex.IsMatch(code, "\bElse\b", RegexOptions.IgnoreCase) Then Return Nothing
        If Regex.IsMatch(code, "\bEnd\s+If\b", RegexOptions.IgnoreCase) Then Return Nothing

        Dim baseIndent As String = m.Groups("indent").Value
        Dim cond As String = m.Groups("cond").Value.Trim()
        Dim stmt As String = m.Groups("stmt").Value.Trim()
        If stmt.Length = 0 Then Return Nothing

        Dim innerIndent As String = baseIndent & New String(" "c, 4)

        Dim result As New List(Of String)
        result.Add($"{baseIndent}If {cond} Then")
        result.Add($"{innerIndent}{stmt}{If(commentTail <> "", " " & commentTail, "")}".TrimEnd())
        result.Add($"{baseIndent}End If")
        Return result
    End Function

    ' Finds the index of a single-quote that starts a comment, ignoring quotes inside string literals.
    Private Function IndexOfCommentMarkerOutsideQuotes(s As String) As Integer
        Dim inStr As Boolean = False
        Dim i As Integer = 0
        While i < s.Length
            Dim ch = s(i)
            If ch = """"c Then
                ' toggle string state; handle escaped quotes ("")
                If i + 1 < s.Length AndAlso s(i + 1) = """"c Then
                    i += 2 : Continue While
                Else
                    inStr = Not inStr
                End If
            ElseIf ch = "'"c AndAlso Not inStr Then
                Return i
            End If
            i += 1
        End While
        Return -1
    End Function
    Private Function LineLikelyContinued(line As String) As Boolean
        'mti 2025/12/16 Victory Day Revised Procedure

        If line Is Nothing Then Return False

        Dim codeOnly As String = Regex.Replace(line, "'[^\r\n]*$", "").TrimEnd()
        If String.IsNullOrWhiteSpace(codeOnly) Then Return False

        'Explicit underscore continuation
        If Regex.IsMatch(codeOnly, "_\s*$") Then Return True

        If Regex.IsMatch(codeOnly,
                     "(\b(AndAlso|OrElse|And|Or|Xor)\b|\+|\-|\*|/|\\|=|<>|<|>|<=|>=|,|\.\s*)$",
                     RegexOptions.IgnoreCase) Then
            Return True
        End If

        If codeOnly.EndsWith("(") OrElse codeOnly.EndsWith("{") OrElse codeOnly.EndsWith(".") Then
            Return True
        End If

        Return False
    End Function

    Private Function InlineCodeLines_Latest(lines As List(Of String), allSubs As Dictionary(Of String, List(Of String)), depth As Integer, callStack As List(Of String)) As List(Of String)
        'mti 2025/12/16 Victory Day - Do not need it anymore
        'mti 2025/08/07 Revised procedure forreplacing parameters with arguments passedwhileinlining
        'mti 2025/07/14 Enhanced to replace parameters with arguments in inlined code
        Try
            Dim outputLines As New List(Of String)

            Dim continuedLine As String = ""
            Dim inContinuation As Boolean = False

            Dim indent As String = New String(" "c, depth * 4)

            Dim tempLines As New List(Of String)

            For Each line As String In lines
                ' Handle continued lines
                If inContinuation Then
                    tempLines.Add(line)
                Else
                    tempLines.Clear()
                    tempLines.Add(line)
                End If

                continuedLine = String.Join("", tempLines)
                inContinuation = LineLikelyContinued(continuedLine)
                If inContinuation Then Continue For

                ' Process complete logical line
                Dim matched As Boolean = False
                If Not continuedLine.TrimStart().StartsWith("'") Then
                    For Each subName In allSubs.Keys
                        Dim codePart = continuedLine.Split("'"c)(0).Trim()
                        If Regex.IsMatch(codePart, $"\b{subName}\s*\(", RegexOptions.IgnoreCase) AndAlso Not callStack.Contains(subName, StringComparer.OrdinalIgnoreCase) Then
                            matched = True
                            outputLines.Add(continuedLine)

                            Dim newCallStack = New List(Of String)(callStack) From {subName}
                            Dim levelDesc = String.Join(":", newCallStack)
                            outputLines.Add($"{indent}' ---- Begin Inlined Level {depth + 1}: {levelDesc}")

                            Dim subBlock = allSubs(subName)
                            Dim subParams() As String = ExtractParamNames(subBlock.First())
                            Dim argsPassed() As String = ExtractArguments(codePart, subName)

                            ' === mti change: build param-to-arg map instead of writing comment block ===
                            Dim paramMap As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                            For argCnt As Integer = 0 To Math.Min(subParams.Length, argsPassed.Length) - 1  'argsPassed.Length should be aleways less; will replace code accordingly,later 
                                Dim paramName As String = subParams(argCnt).Trim()
                                Dim passedValue As String = argsPassed(argCnt).Trim()
                                If passedValue <> "" AndAlso Not passedValue.Equals("Not Passed", StringComparison.OrdinalIgnoreCase) Then
                                    paramMap(paramName) = passedValue
                                End If
                            Next
                            ' === mti change end ===

                            ' Inline the subroutine
                            Dim inlinedBlock = InlineCodeLines(subBlock, allSubs, depth + 1, newCallStack)
                            Dim inDeclaration As Boolean = True

                            For i = 0 To inlinedBlock.Count - 1
                                Dim rawLine As String = inlinedBlock(i)
                                Dim indentedLine As String = indent & rawLine

                                ' Always comment out the final line (End Sub/Function)
                                If i = inlinedBlock.Count - 1 Then
                                    outputLines.Add("' " & indentedLine.TrimStart())
                                    Continue For
                                End If

                                If inDeclaration Then
                                    outputLines.Add("' " & indentedLine.TrimStart())
                                    If Not LineLikelyContinued(String.Join("", inlinedBlock.Take(i + 1))) Then
                                        inDeclaration = False
                                    End If
                                Else
                                    ' === mti change: substitute parameters with passed arguments ===
                                    Dim substitutedLine As String = rawLine
                                    For Each kvp In paramMap
                                        substitutedLine = Regex.Replace(substitutedLine, $"\b{Regex.Escape(kvp.Key)}\b", kvp.Value)
                                    Next
                                    outputLines.Add(indent & substitutedLine)
                                    ' === mti change end ===
                                End If
                            Next

                            outputLines.Add($"{indent}' ---- End Inlined Level {depth + 1}: {levelDesc}")
                            Exit For
                        End If
                    Next
                End If

                If Not matched Then outputLines.AddRange(tempLines)
                tempLines.Clear()
                continuedLine = ""
            Next

            Return outputLines

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
            Stop
            Return New List(Of String)
        End Try
    End Function
    'mti 2025/12/16 Victory Day Deleted InlineCodeLine_08072025

    Private Function ExtractParamNames(declarationLine As String) As String()
        'mti 2025/08/07 Revised procedure
        ' Strip trailing comment (outside quotes)
        Dim code As String = RemoveCommentOutsideQuotes(declarationLine)

        ' Find the last top-level (...) block: this is the parameter list
        Dim openIdx As Integer = -1
        Dim closeIdx As Integer = -1
        Dim depth As Integer = 0
        Dim lastOpen As Integer = -1

        For i As Integer = 0 To code.Length - 1
            Dim ch = code(i)
            If ch = "("c Then
                depth += 1
                If depth = 1 Then lastOpen = i
            ElseIf ch = ")"c Then
                depth -= 1
                If depth = 0 Then
                    openIdx = lastOpen
                    closeIdx = i
                    ' Keep going to capture the *last* top-level pair
                End If
            End If
        Next

        If openIdx < 0 OrElse closeIdx <= openIdx Then
            Return Array.Empty(Of String) ' no params
        End If

        Dim paramSection As String = code.Substring(openIdx + 1, closeIdx - openIdx - 1)

        ' Split params by commas at top level only
        Dim parts As New List(Of String)
        Dim current As New System.Text.StringBuilder()
        depth = 0
        For Each ch In paramSection
            If ch = "("c Then
                depth += 1
                current.Append(ch)
            ElseIf ch = ")"c Then
                depth -= 1
                current.Append(ch)
            ElseIf ch = ","c AndAlso depth = 0 Then
                parts.Add(current.ToString())
                current.Clear()
            Else
                current.Append(ch)
            End If
        Next
        If current.Length > 0 Then parts.Add(current.ToString())

        ' Extract just the parameter names
        Dim names = parts.
        Select(Function(p) ExtractParamName(p)).
        Where(Function(n) Not String.IsNullOrWhiteSpace(n)).
        ToArray()

        Return names
    End Function

    ' --- helpers ---

    Private Function ExtractParamName(paramDecl As String) As String
        'mti 2025/08/07 Revised procedure
        Dim s As String = paramDecl.Trim()
        If s = "" Then Return ""

        ' Remove leading attributes like <Out()> <MarshalAs(...)>
        Do While s.StartsWith("<"c)
            Dim gt = s.IndexOf(">"c)
            If gt < 0 Then Exit Do
            s = s.Substring(gt + 1).Trim()
        Loop

        ' Remove modifiers (can be multiple)
        Dim changed As Boolean = True
        While changed
            Dim before = s
            s = Regex.Replace(s, "^(ByVal|ByRef|Optional|ParamArray)\b", "", RegexOptions.IgnoreCase).Trim()
            changed = (s <> before)
        End While

        ' Now s should start with: Name[(...)] [As ...] [= ...]
        ' Capture a VB identifier as the name (ignore any array dims that follow)
        Dim m = Regex.Match(s, "^(?<name>[A-Za-z_]\w*)", RegexOptions.IgnoreCase)
        If m.Success Then
            Return m.Groups("name").Value
        End If
        Return ""
    End Function

    Private Function RemoveCommentOutsideQuotes(s As String) As String
        'mti 2025/08/07 New/Revised procedure
        Dim inStr As Boolean = False
        For i As Integer = 0 To s.Length - 1
            Dim ch = s(i)
            If ch = """"c Then
                ' handle doubled quotes ""
                If i + 1 < s.Length AndAlso s(i + 1) = """"c Then
                    i += 0 ' skip only one; loop will advance next
                Else
                    inStr = Not inStr
                End If
            ElseIf ch = "'"c AndAlso Not inStr Then
                Return s.Substring(0, i)
            End If
        Next
        Return s
    End Function
    Private Function ExtractParamNames_latest(declarationLine As String) As String()
        'mti 2025/07/14 New procedure

        'does not seem to  handle  as etc, like the following
        'Private Sub ComputeFactorsNormScalShift(mordstDet(,,) As Double, popgdr(,) As Double, Ntypes As Integer, context As IForecastContext)

        Dim paramNames As New List(Of String)

        ' Find the parentheses enclosing the parameter list
        Dim openParenIndex As Integer = declarationLine.IndexOf("(")
        Dim closeParenIndex As Integer = declarationLine.LastIndexOf(")")

        If openParenIndex >= 0 AndAlso closeParenIndex > openParenIndex Then
            Dim paramSection As String = declarationLine.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1)

            ' Split respecting nested () so commas inside arrays/types don't break the split
            Dim current As String = ""
            Dim parenDepth As Integer = 0

            For Each ch As Char In paramSection
                If ch = "("c Then
                    parenDepth += 1
                ElseIf ch = ")"c Then
                    parenDepth -= 1
                ElseIf ch = ","c AndAlso parenDepth = 0 Then
                    paramNames.Add(ExtractParamName(current))
                    current = ""
                    Continue For
                End If
                current &= ch
            Next

            If current.Trim() <> "" Then paramNames.Add(ExtractParamName(current))
        End If

        Return paramNames.Where(Function(p) Not String.IsNullOrWhiteSpace(p)).ToArray()
    End Function


    Private Function ExtractParamName_lates(paramDeclaration As String) As String
        'mti 2025/07/14 New procedure
        ' Helper function: strip keywords and return just the name
        Dim keywords = {"ByVal", "ByRef", "Optional", "ParamArray"}
        Dim tokens = paramDeclaration.Trim().Split({" "c}, StringSplitOptions.RemoveEmptyEntries)

        For Each token In tokens
            Dim clean = token.Trim()
            If Not keywords.Contains(clean, StringComparer.OrdinalIgnoreCase) Then
                ' Assume first non-keyword is the parameter name
                If clean.Contains(":") Then clean = clean.Split(":"c)(0) ' Remove type hint like "x: Integer"
                If clean.Contains("(") Then clean = clean.Substring(0, clean.IndexOf("(")) ' Remove array indicators
                If clean.Contains("=") Then clean = clean.Split("="c)(0) ' Remove default value assignment
                Return clean.Trim()
            End If
        Next

        Return ""
    End Function

    Private Function ExtractArguments(callLine As String, subName As String) As String()
        'mti 2025/07/14 New procedure
        Dim arguments As New List(Of String)
        Dim patternParen = $"\b{subName}\s*\((.*)\)"
        Dim patternNoParen = $"\b{subName}\s+(.+)$"

        ' Remove trailing comment
        Dim codePart = callLine.Split("'"c)(0).Trim()

        ' Try parenthesis-wrapped call first
        Dim mParen = Regex.Match(codePart, patternParen, RegexOptions.IgnoreCase)
        If mParen.Success Then
            Dim argString = mParen.Groups(1).Value
            Return SplitArgumentsRespectingNesting(argString)
        End If

        ' Try space-separated argument call
        Dim mNoParen = Regex.Match(codePart, patternNoParen, RegexOptions.IgnoreCase)
        If mNoParen.Success Then
            Dim argString = mNoParen.Groups(1).Value
            Return SplitArgumentsRespectingNesting(argString)
        End If

        Return arguments.ToArray()
    End Function

    Private Function SplitArgumentsRespectingNesting(argString As String) As String()
        'mti 2025/07/14 New procedure
        Dim args As New List(Of String)
        Dim currentArg As String = ""
        Dim nesting As Integer = 0
        Dim inQuote As Boolean = False

        For i As Integer = 0 To argString.Length - 1
            Dim ch As Char = argString(i)

            ' Handle quoted strings (e.g., "some, text")
            If ch = """"c Then
                inQuote = Not inQuote
                currentArg &= ch
                Continue For
            End If

            If inQuote Then
                currentArg &= ch
                Continue For
            End If

            Select Case ch
                Case "("c
                    nesting += 1
                    currentArg &= ch
                Case ")"c
                    nesting -= 1
                    currentArg &= ch
                Case ","c
                    If nesting = 0 Then
                        If currentArg.Trim() = "" Then
                            args.Add("Not Passed")
                        Else
                            args.Add(currentArg.Trim())
                        End If
                        currentArg = ""
                    Else
                        currentArg &= ch
                    End If
                Case Else
                    currentArg &= ch
            End Select
        Next

        ' Handle last argument or trailing comma
        If currentArg.Trim() = "" Then
            args.Add("ByPassed")
        Else
            args.Add(currentArg.Trim())
        End If

        Return args.ToArray()
    End Function

    Function StripTypeSuffix(name As String) As String
        'mti 2025/07/14 New procedure
        Return If(String.IsNullOrWhiteSpace(name), "", If(name.Length > 1 AndAlso "+-!@#$%&".Contains(name.Last()), name.Substring(0, name.Length - 1), name))
    End Function
    Private Function IsStartOfLinqBlock(line As String) As Boolean
        'mti 2025/07/14 New procedure
        Dim trimmed = line.TrimStart()
        If trimmed.StartsWith("'") Then Return False ' full-line comment

        line = line.ToUpperInvariant()
        Return line.Contains("FROM ") AndAlso line.Contains(" IN ") 'AndAlso line.Contains("(")
    End Function

    Private Function IsInLinqBlock(line As String) As Boolean
        'mti 2025/07/14 New procedure
        Dim trimmed = line.TrimStart()
        If trimmed.StartsWith("'") Then Return False ' full-line comment

        line = line.ToUpperInvariant()
        Return line.Contains("WHERE ") OrElse line.Contains("SELECT ") OrElse line.Contains("ORDER BY")
    End Function

    Private Function SkipLinqBlock(lines As List(Of String), startIndex As Integer) As Integer
        'mti 2025/07/14 New procedure
        ' Skip LINQ block that starts at current index
        Dim parenDepth As Integer = 0
        Dim i As Integer = startIndex

        Do While i < lines.Count
            Dim line = lines(i)
            parenDepth += line.Count(Function(c) c = "(") - line.Count(Function(c) c = ")")
            i += 1
            If parenDepth <= 0 Then Exit Do
        Loop

        Return i ' index of line after the skipped 
    End Function
    Private Sub ParseCodeFile(Filename$, ModName$)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti 2025/12/16 Victory Day Delete Unnecessary Comments; added code - Take entire procedure

        Dim ACnt%
        Dim strcode$  'why outside try -  that's the reason not showing in error msg

        Try
            Dim IFCondition(20) As String
            Dim IFAcnt%, ia%

            Dim tstrcode$, pos%
            Dim cBlock As String
            Dim ISVBStructure As Boolean 'Structure: IF...THEN, FOR..NEXT etc
            Dim i%

            Dim StartFor(100) As Integer
            Dim StartIf(100) As Integer
            Dim StartDo(100) As Integer
            Dim StartWhile(100) As Integer
            Dim StartSelect(100) As Integer
            Dim StartUsing(100) As Integer 'mti 2025/07/14

            Dim Start_IF_ACnt(100) As Integer  'mti 2025/08/07

            'mti 2025/12/16 Victory Day start
            Dim SelectExpression(99) As String
            'mti 2025/12/16 Victory Day End

            'mti 2017/02/20 End

            Dim StartRoutine%

            Dim Added2CBlock%

            'keeps track of nesting within structures
            Dim ForNest%, IFNest%, DoNest%, WhileNest%, SelectNest%
            Dim UsingNest% 'mti 2025/07/14

            Dim pos1%, pos2%, pos3% 'jrs 2022/12/28 added pos3%

            Dim TChar%
            Dim IFStarting As Boolean

            For i = 0 To 99  ''mti 2025/07/1410
                StartFor(i) = 0
                StartIf(i) = 0
                StartDo(i) = 0
                StartWhile(i) = 0
                StartSelect(i) = 0
                StartUsing(i) = 0 'mti 2025/07/14
                Start_IF_ACnt(i) = 0 'mti 2025/08/07

                'mti 2025/12/16 Victory Day start
                SelectExpression(99) = ""
                'mti 2025/12/16 Victory Day End

            Next i

            'IFAcntTot = 0 'mti 2025/08/07

            StartRoutine = 0

            ForNest = 0
            IFNest = 0
            DoNest = 0
            WhileNest = 0
            SelectNest = 0
            IFAcnt = 0

            'mti 2026/05/13 start - For..Next loop parsing support

            Dim ForAcnt As Integer = 0

            Dim Start_For_ACnt(99) As Integer
            Dim ForCounter(99) As String
            Dim ForStartVal(99) As Integer
            Dim ForEndVal(99) As Integer
            Dim ForStepVal(99) As Integer
            Dim ForExpand(99) As Boolean

            'mti 2026/05/13 end



            'ACnt is the counter for the main array where parsed code is taken
            'Leftside() of the eqn., Rightside() of the eqn., and the corresponding Codeblock()
            ACnt = codelines

            'If InStr(Filename, "SOCIOPOL.bas") <> 0 Then Stop

            cBlock = "" 'holds code block temporarily as it is built

            'jrs 2023/07/20
            Dim SubParametersCall As Dictionary(Of String, Dictionary(Of Integer, String)) = New Dictionary(Of String, Dictionary(Of Integer, String))
            Dim SubParametersDeclare As Dictionary(Of String, Dictionary(Of Integer, String())) = New Dictionary(Of String, Dictionary(Of Integer, String()))
            Dim sCurrSubName As String = ""

            'On Error GoTo jrsaHdlr
            Using reader As StreamReader = New StreamReader(Filename)
                strcode = reader.ReadLine

                Do While Not reader.EndOfStream

                    strcode = strcode.Trim

                    'If strcode.Contains("Dim SanitImproved = Amin(100, AMAX(0, 100 - (sSanitHH(R%, OpenDefecation, National) + sSanitHH(R%, Unimproved, National))))") Then Stop

                    cBlock = cBlock + vbCr + strcode
                    Added2CBlock = strcode.Length + 1
                    'Added2CBlock this is used for the length of the latest string
                    'in case it needs to be subtracted from the block

                    'If strcode.Contains("swingeffects(swingee%) =") Then Stop
                    'If strcode.Contains("Next Actor") Then Stop

                    If strcode.Length > 0 Then
                        'jrs 2023/07/05 no continuation on commented lines
                        'jrs 2023/07/05 another continuation character could be +
                        'jrs 2023/07/05 another continuation character could be And
                        'jrs 2023/07/13 another continuation character could be AndAlso
                        'jrs 2022/12/28 another continuation character could be *
                        'jrs 2022/12/28 added continuation not only _ in vb6 but also , and other signs in .NET


                        'If strcode.Contains("Log of Emigration (Migrants out, percent of population) (2026)") Then Stop

                        'mti 2026/04/21 test string
                        'If strcode.Contains("ICTMOBIL(R%) = CSng(PhysStckDmndThou / POP(R%) / 10)") Then Stop

                        'mti 2025/07/14 start
                        'this is treating lines ending with variables like "actor" as line continuation; need revision


                        'mti 2026/04/21 start - handling new coding patterns - line continuation with comments in continued line; e.g., Call AnalFunc("Log of Emigration (Migrants out, percent of population) (2026)", 'dsb 2026/1/27 New monadic regression model
                        'check for trailing comment
                        'strcode = "                        Call AnalFunc(Log Of Emigration (Migrants out, percent Of population) (2026), 'dsb 2026/1/27 New monadic regression model"
                        pos = strcode.Trim.IndexOf("'")
                        'If pos > 0 Then strcode = strcode.Substring(0, pos - 1).Trim
                        If pos > 0 Then strcode = StripTrailingComments(strcode).Trim 'trim only trailing comments , not starting comment (where pos=0)
                        'mti 2026/04/21 start

                        'mti 2025/12/16 Victory Day Stat  - Was missing "=" as line continuation character
                        Do While Not strcode.Contains("'") AndAlso (strcode.Substring(strcode.Length - 1) = "_" Or strcode.Substring(strcode.Length - 1) = "," _
                            Or strcode.Substring(strcode.Length - 1) = "=" _
                            OrElse strcode.Substring(strcode.Length - 1) = "*" _
                            OrElse strcode.Substring(strcode.Length - 1) = "+" _
                            OrElse strcode.Substring(strcode.Length - 1) = "-" _
                            OrElse strcode.Substring(strcode.Length - 1) = "/" _
                            OrElse (strcode.Length > 2 AndAlso strcode.Substring(strcode.Length - 3).Trim.ToUpper = "OR") _
                            OrElse (strcode.Length > 6 AndAlso strcode.Substring(strcode.Length - 7).Trim.ToUpper = "ORELSE") _
                            OrElse (strcode.Length > 3 AndAlso strcode.Substring(strcode.Length - 4).Trim.ToUpper = "AND") _
                            OrElse (strcode.Length > 7 AndAlso strcode.Substring(strcode.Length - 8).Trim.ToUpper = "ANDALSO")) 'handle line continuation in VB.NET code

                            tstrcode = reader.ReadLine
                            If tstrcode Is Nothing Then Exit Do ''mti 2025/07/14  end of file

                            strcode = strcode + " " + tstrcode.Trim

                            cBlock = cBlock + vbCr + tstrcode
                            Added2CBlock = Added2CBlock + tstrcode.Length + 1
                        Loop



                    End If

                    'If strcode.Contains("(CoalMaxProd - enppot(R%, e%)") Then Stop '24241


                    If strcode.Length > 0 AndAlso Not strcode.TrimStart().StartsWith("'") Then

                        'LinQ query block will not be parsed skip; code will be added in block
                        'Dim rs = (From PD In db.HealthProxDrivRRsHH
                        '          Where PD.ProxDriver = sProxDriv And PD.DiseaseType = sDisType
                        '          Select Key = PD.DistributionIdx, Value = PD.RRValue)
                        If IsStartOfLinqBlock(strcode) Then
                            Do
                                tstrcode = reader.ReadLine
                                If strcode Is Nothing Then Exit Do ' end of file

                                cBlock = cBlock + vbCr + tstrcode
                                Added2CBlock = Added2CBlock + tstrcode.Length + 1

                                strcode = strcode + " " + tstrcode.Trim

                            Loop While IsInLinqBlock(tstrcode) 'skip parsing
                        End If

                        ''Database file call - skip parsing after adding code in block
                        'If strcode.ToUpper.Contains(".DB") Then  ' OrElse strcode.ToUpper.Contains(" DB ") :: will mess up using DB - so took off
                        '    strcode = "" 'skip parsing
                        'End If

                    End If



                    If strcode <> "" Then  '                       '



                        pos = strcode.Trim.ToUpper.IndexOf(" ")  'will need it for multiword structures

                        'mti 2025/07/14 syart
                        'the following line is not working with Vb.net as VB.net code can be sin gle line with just "Next" as loop termination, 
                        'unlike VB6 .net allows not adding conters in loop termination line, to make less verbose code
                        'If pos > 1 Then
                        Dim firstWord As String = strcode.Trim.ToUpper.Split(" "c)(0)  'take first word
                        'mti 2025/07/14  end



                        Select Case firstWord 'strcode.Trim.ToUpper.Substring(0, pos)
                            Case "PRIVATE", "PUBLIC", "SUB", "FUNCTION", "STATIC"
                                ISVBStructure = True
                                StartRoutine = ACnt + 1
                                'jrs 2023/07/20 *** start ***
                                sCurrSubName = ""
                                If strcode.Trim.ToUpper.Substring(0, pos) = "SUB" Or strcode.Trim.ToUpper.Substring(0, pos) = "FUNCTION" Then
                                    Dim xPars = strcode.Split(CChar(","))
                                    Dim sSubName As String
                                    sSubName = xPars(0)
                                    sSubName = sSubName.ToUpper.Replace("SUB", "")
                                    sSubName = sSubName.ToUpper.Replace("FUNCTION", "")
                                    pos = sSubName.IndexOf("(")
                                    If pos > 0 Then
                                        sSubName = sSubName.Substring(0, pos)
                                    End If
                                    sSubName = sSubName.Trim

                                    sCurrSubName = sSubName

                                    If Not SubParametersDeclare.ContainsKey(sSubName) Then
                                        SubParametersDeclare.Add(sSubName, New Dictionary(Of Integer, String()))
                                        pos = xPars(0).IndexOf("(") + 1
                                        pos2 = xPars(0).IndexOf(" As ") + 1
                                        If pos2 > pos Then
                                            pos3 = xPars(0).IndexOf("()")
                                            If pos3 > pos And pos3 < pos2 Then
                                                pos2 = pos3
                                            End If
                                            SubParametersDeclare(sSubName).Add(1, New String() {xPars(0).Substring(pos, pos2 - pos).Trim, ""})
                                        Else
                                            SubParametersDeclare(sSubName).Add(1, New String() {xPars(0).Substring(pos).Trim, ""})
                                        End If
                                        Dim idx As Integer = 1
                                        For i = 1 To xPars.Length - 1
                                            pos = xPars(i).IndexOf(" As ")
                                            pos2 = xPars(i).IndexOf("()")
                                            If pos2 > 0 And pos2 < pos Then
                                                pos = pos2
                                            End If
                                            If pos = -1 Then
                                                pos = xPars(i).Length
                                            End If
                                            If xPars(i) <> ")" And xPars(i).Length > 0 Then
                                                SubParametersDeclare(sSubName).Add(idx + 1, New String() {xPars(i).Substring(0, pos).Trim, ""})
                                                idx = idx + 1
                                            End If
                                        Next

                                        pos2 = SubParametersDeclare(sSubName)(idx)(0).IndexOf(")")
                                        If pos2 > 0 Then
                                            SubParametersDeclare(sSubName)(idx)(0) = SubParametersDeclare(sSubName)(idx)(0).Substring(0, pos2)
                                        End If
                                    End If
                                End If
                                    'jrs 2023/07/20 *** end ***
                            Case "IF"

                                IFStarting = False
                                pos1 = strcode.Trim.IndexOf("Then")

                                'see if IF..THEN structure is completed in the same line
                                If strcode.Trim.Length > pos1 + 4 Then
                                    TChar = AscW(strcode.Trim.Substring(pos1 + 5, 1))
                                    If TChar = 32 Or TChar = 39 Then '" " or "'"
                                        IFStarting = True

                                    Else 'mti 2025/07/09 - IF Then in same line could be an equation
                                        strcode = strcode.Trim.Substring(pos1 + 5, strcode.Trim.Length - pos1 - 5)
                                    End If
                                Else
                                    IFStarting = True
                                End If

                                If IFStarting Then
                                    ISVBStructure = True
                                    IFNest = IFNest + 1
                                    StartIf(IFNest) = ACnt + 1
                                Else 'same line If..Then
                                    ISVBStructure = False
                                End If

                            Case "FOR"

                                ISVBStructure = True
                                ForNest = ForNest + 1

                                If ForNest >= 100 Then Stop

                                StartFor(ForNest) = ACnt + 1
                            Case "SELECT"
                                'add code with current block
                                'If strcode.Contains("Select Case clpccvr(R%)") Then Stop
                                ISVBStructure = True
                                SelectNest = SelectNest + 1
                                StartSelect(SelectNest) = ACnt + 1

                                'mti 2025/12/16 Victory Day start
                                SelectExpression(SelectNest) = GetSelectCaseExpression(strcode.Trim)
                                'mti 2025/12/16 Victory Day End

                            Case "DO"

                                ISVBStructure = True
                                DoNest = DoNest + 1
                                StartDo(DoNest) = ACnt + 1

                            Case "WHILE"

                                ISVBStructure = True
                                WhileNest = WhileNest + 1
                                StartWhile(WhileNest) = ACnt + 1

                            Case "USING"  'mti 2025/07/14 - .Net has Using .. End Using

                                ISVBStructure = True
                                UsingNest = UsingNest + 1
                                StartUsing(UsingNest) = ACnt + 1

                            Case "ELSE", "ELSEIF", "CASE" 'continue taking structure

                                ISVBStructure = True

            'below are ends of structures

                            Case "END"
                                If strcode.Trim.ToUpper.Substring(0, 6) = "END IF" Then
                                    HandleStructure(ACnt, StartIf(IFNest), cBlock, strcode$, Added2CBlock)
                                    IFNest = IFNest - 1

                                    If IFNest < 0 Then IFNest = 0 'mti 2025/07/14

                                    cBlock = ""
                                    ISVBStructure = True
                                ElseIf strcode.Length > 9 AndAlso strcode.Trim.ToUpper.Substring(0, 10) = "END SELECT" Then
                                    HandleStructure(ACnt, StartSelect(SelectNest), cBlock, strcode$, Added2CBlock)

                                    'mti 2025/12/16 Victory Day start
                                    For i = StartSelect(SelectNest) To ACnt
                                        If Rightside(i) IsNot Nothing AndAlso Rightside(i).Trim() <> "" Then
                                            'modify Rightside(i) to add Select Expression
                                            Rightside(i) = Rightside(i).Trim + ", " + SelectExpression(SelectNest)
                                        End If
                                    Next
                                    SelectExpression(SelectNest) = ""
                                    'mti 2025/12/16 Victory Day End

                                    SelectNest = SelectNest - 1

                                    If SelectNest < 0 Then SelectNest = 0 'mti 2025/07/14



                                    cBlock = ""
                                    ISVBStructure = True
                                ElseIf strcode.Length > 9 AndAlso strcode.Trim.ToUpper.Substring(0, 10) = "End Using" Then

                                    HandleStructure(ACnt, StartUsing(UsingNest), cBlock, strcode$, Added2CBlock)
                                    UsingNest = UsingNest - 1

                                    If UsingNest < 0 Then UsingNest = 0 'mti 2025/07/14

                                    cBlock = ""
                                    ISVBStructure = True
                                Else

                                    If strcode.Trim.ToUpper.Substring(0, 7) = "End Sub" Or (strcode.Length > 11 AndAlso strcode.Trim.ToUpper.Substring(0, 12) = "End Function") Then 'end sub, end function etc.
                                        'proc/function completed

                                        HandleStructure(ACnt, StartRoutine, cBlock, strcode$, Added2CBlock)
                                        cBlock = ""

                                        For i = 0 To 10
                                            StartFor(i) = 0
                                            StartIf(i) = 0
                                            StartDo(i) = 0
                                            StartWhile(i) = 0
                                            StartSelect(i) = 0
                                        Next i
                                        StartRoutine = 0

                                        ForNest = 0
                                        IFNest = 0
                                        DoNest = 0
                                        WhileNest = 0
                                        SelectNest = 0

                                        ISVBStructure = True



                                    End If
                                End If
                            Case "NEXT"  '"Next"  'for.next  'mti 2025/12/16 Victory Day Capitalze NEXT

                                'If ACnt = 6178 Then Stop

                                HandleStructure(ACnt, StartFor(ForNest), cBlock, strcode$, Added2CBlock)
                                ForNest = ForNest - 1

                                If ForNest < 0 Then ForNest = 0  'mti 2025/07/14 

                                cBlock = ""
                                ISVBStructure = True

                            Case "LOOP"   '"Loop"  'do..loop  'mti 2025/12/16 Victory Day Capitalze

                                HandleStructure(ACnt, StartDo(DoNest), cBlock, strcode$, Added2CBlock)
                                DoNest = DoNest - 1

                                If DoNest < 0 Then DoNest = 0 'mti 2025/07/14

                                cBlock = ""
                                ISVBStructure = True

                            Case "WEND"   '"Wend" 'while..wend  'mti 2025/12/16 Victory Day Capitalze

                                HandleStructure(ACnt, StartWhile(WhileNest), cBlock, strcode$, Added2CBlock)
                                WhileNest = WhileNest - 1

                                If WhileNest < 0 Then WhileNest = 0 'mti 2025/07/14

                                cBlock = ""
                                ISVBStructure = True
                            Case Else
                                ISVBStructure = False
                        End Select

                        'mti 2025/07/14 start firsword
                        'End If
                        'mti 2025/07/14 end

                        If Not ISVBStructure Then
                            '    If InStr(strcode, "ECONFREE(R%) =") > 0 Then Stop
                            'ECONFREE(R%) = (AMAX(0.01, Amin(10, econfreeri(R%) * Prediction * econfreem(R%))))
                            'mti added March 2006
                            'jrs 2022/12/27
                            If strcode.Trim.Length > 4 Then
                                If strcode.Trim.Substring(0, 5) = "'Doc " Then 'dummy code for helping documentation
                                    strcode = strcode.Trim.Substring(5).Trim
                                End If
                            End If
                            'mti added March 2006
                            'mti 2020/06/19 2020 start
                            'If mid(Trim(strcode), 1, 1) <> "'" Then  'omit comment line

                            'mti 2025/07/14 start
                            If strcode.Trim.Substring(0, 1) <> "'" Then 'And Not strcode.ToUpper.Contains("DYAD") Then 'skip comment lines, also skip dyadic for now
                                'mti 2025/07/14 end

                                'mti 2020/06/19 2020 End


                                If strcode.Contains("Call XYTABL(") Then

                                    'new block
                                    ACnt = ACnt + 1
                                    CodeBlock(ACnt) = cBlock 'strcode
                                    cBlock = ""

                                    Dim Savestrcode As String = strcode
                                    strcode = StripAmaxAmin(strcode)

                                    Dim leftParenTot As Integer
                                    Dim RightParenTot As Integer

                                    pos = strcode.IndexOf("'")
                                    If pos >= 0 Then
                                        strcode = strcode.Substring(0, pos - 1)
                                        strcode = strcode.Trim
                                    End If
                                    pos = "Call XYTABL(".Length  '0 'mti 2024/05/17 
                                    pos1 = "Call XYTABL(".Length  '0 'mti 2024/05/17 
                                    'pos2 = 0 'mti 2024/05/17 
                                    Dim posLP As Integer
                                    Dim posRP As Integer

                                    leftParenTot = 0
                                    RightParenTot = 0
                                    Do While True
                                        posLP = strcode.IndexOf("(", pos + 1)  'check for left parentheses in between 
                                        posRP = strcode.IndexOf(")", pos + 1)  'check for left parentheses in between 'Call XYTABL(TbName$, CLPC(R%, 4), FormValue, context)

                                        pos = strcode.IndexOf(",", pos + 1)

                                        'mti 2024/05/17 start handle parentheses

                                        'Call XYTABL(TbName$, CLPC(R%, 4), FormValue, context)

                                        If posLP < pos And posLP > -1 Then
                                            leftParenTot = leftParenTot + 1
                                        End If


                                        If posRP < pos And posRP > -1 Then
                                            RightParenTot = RightParenTot + 1
                                        End If



                                        If pos = -1 Then
                                            Exit Do
                                        Else
                                            If RightParenTot = leftParenTot Then
                                                pos3 = pos2 'jrs 2022/12/28
                                                pos2 = pos1
                                                pos1 = pos
                                                leftParenTot = 0
                                                RightParenTot = 0
                                            Else
                                                'pos1 = pos
                                            End If
                                        End If
                                    Loop

                                    LeftSide(ACnt) = strcode.Substring(pos2 + 1) 'jrs 2022/12/28 changed from pos1 to pos2
                                    'cut down the last ")"
                                    LeftSide(ACnt) = LeftSide(ACnt).Substring(0, pos1 - pos2 - 1).Trim  'mti 2024/05/17  'jrs 2022/12/28 changed from using Length to using pos1-pos2

                                    Rightside(ACnt) = strcode.Substring(pos3 + 1, pos2 - pos3 - 1).Trim 'jrs 2022/12/28 changed from pos2 to pos3 and from pos1 to pos2


                                    'mti 07 Feb 03
                                    ModuleName(ACnt) = ModName

                                    'mti 2025/08/07 start
                                    CommentText(ACnt) = "TABLFUNC," & GetRegressionFunctionName(Savestrcode, ACnt, LeftSide, Rightside, BaseYear, ConstantDollarsYear)
                                    'mti 2025/08/07 end

                                    'If CommentText(ACnt) = "TABLFUNC," Then Stop   'mti 2025/09/17 start

                                    'some exp functions e.g., Call XYTABL(TblNameHealthExp, GDPPCP(R%), gkcomp(Health), context) is used in Sociopol, declared in Economy, coded in Populat

                                    'If strcode.Contains("Call XYTABL(TbName$, GDPPC(R%) * (1 + igdpr(R%) - POPR(R%) / 100), pppcomp2, context)") Then Stop

                                    'Build Documentation 11/11/2003
                                ElseIf strcode.Contains("Call ADJSTR(") Then
                                    'parse call ADJSTR function
                                    'Call ADJSTR(wstbase, Diff1, Diff2, elenpr, elenpr2, mulwst)
                                    '---------------------------------right-------------, left(mul)
                                    'new block

                                    'mti 2025/07/09 commenting old handling of ADJSTR - start
                                    'ACnt = ACnt + 1
                                    'CodeBlock(ACnt) = cBlock 'strcode
                                    'cBlock = ""
                                    'mti 2025/07/09 commenting old handling of ADJSTR - end

                                    'If strcode.Contains("Call ADJSTR(1, Diff1, Diff2, eleniprof, eleniprof2,") Then Stop


                                    'check for trailing comment
                                    pos = strcode.Trim.IndexOf("'")
                                    If pos >= 0 Then
                                        strcode = strcode.Substring(0, pos - 1)
                                    End If

                                    'If strcode.Trim = "Call ADJSTR(1, caput_1, caput_1 - SCaputErr(R%, S%), elcaput1, elcaput2, Mul)" Then Stop

                                    'mti 2025/07/09 New handling of ADJSTR - start
                                    Dim match As Match = Regex.Match(strcode.Trim, "ADJSTR\s*\((.*)\)")
                                    If match.Success Then
                                        Dim argsString As String = match.Groups(1).Value

                                        ' Split arguments while accounting for nested parentheses
                                        Dim args As List(Of String) = SplitArguments(argsString)
                                        Dim args1 As String() = args.Distinct().ToArray()

                                        ACnt = ACnt + 1
                                        CodeBlock(ACnt) = cBlock 'strcode
                                        cBlock = "" 'will blank out code for next iterations in loop; so code is not written repeatedly
                                        LeftSide(ACnt) = args1(args1.Count - 1)
                                        Rightside(ACnt) = StripAmaxAmin(args1(0))
                                        'format: Call ADJSTR(wstbase, Diff1, Diff2, elenpr, elenpr2, mulwst)
                                        'LeftSide(ACnt) = args1(args1.Count - 1)  '.Trim()  'last argument to the function call; computed multiplier, Mul 
                                        For argcnt As Integer = 1 To args1.Count - 2
                                            Rightside(ACnt) = Rightside(ACnt) + " + " + StripAmaxAmin(args1(argcnt))   '.Trim()
                                        Next
                                        ModuleName(ACnt) = ModName



                                        'mti 2025/08/07 start
                                        CommentText(ACnt) = "PID"
                                        'mti 2025/08/07 end

                                    End If

                                    'mti 2025/07/09 New handling of ADJSTR - end


                                ElseIf strcode.Contains("Call AnalFunc(") Or strcode.Contains("Call AnalFuncSE(") Then
                                    'mti 2025/06/24 start also handle AnalFuncSE here

                                    'parse Call AnalFunc function
                                    'Call AnalFunc("World Bank Loans as % of Recipient GDP", _
                                    '    Array(GDPPCP(R%), GINIDOM(R%), AIDPERGDP(R%)), Prediction)
                                    '---------------------------------right-------------, left(mul)


                                    'If strcode.Contains("Total Fertilty Rate (2005)") Then Stop


                                    'new block
                                    ACnt = ACnt + 1
                                    CodeBlock(ACnt) = cBlock 'strcode
                                    '                    If ACnt = 523 Then Stop
                                    cBlock = ""

                                    'mti 2021/04/19 start
                                    strcode = strcode.Trim
                                    'mti 2021/04/19 End



                                    'check for trailing comment
                                    pos = strcode.Trim.IndexOf("'")
                                    If pos > 0 Then

                                        'mti 2013/12/01 - driver fix ECONFREE - start
                                        'was doing trim in calculating position; but not in chopping off string; needed to correct this
                                        'also needed to trim after chopping off- else it might come with spaces before comment
                                        'and keep one/more extra character (e.g., parenthesis close) with the LeftSide(Acnt)

                                        'strcode = mid(strcode, 1, pos - 1)
                                        strcode = strcode.Trim.Substring(0, pos - 1).Trim

                                        'mti 2013/12/01 - driver fix ECONFREE - end

                                    End If


                                    'Call AnalFunc("World Bank Loans as % of Recipient GDP", _
                                    '    Array(GDPPCP(R%), GINIDOM(R%), AIDPERGDP(R%)), Prediction)

                                    'mti 2021/04/19 Start
                                    pos = 0 'InStr(strcode, """,") + 1 'search for ",  '0
                                    pos1 = 0 ' InStr(strcode, """,") + 1 '0
                                    'mti 2021/04/19 End

                                    Rightside(ACnt) = ""

                                    'mti 2025/06/24 start 
                                    'Call AnalFunc("AgeOfMarraigeMaleWB V. GDPPC (Log) & EdYears15Male (Log) (All Most Recent)",
                                    '  New Single() {GDPPCP(R%), EDYRSAG15(R%, Male)}, ageofmarriagecomp(gndr%),
                                    '  New String() {"GDPPC (Log)", "EdYearsAge15Male(MOSTRECENT) (Log)"}, context)
                                    'Call AnalFunc(""MunDemandPC (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log) AND Connections"",New Single() {GDPPCP(R%), UrbanPercent(R%), WATSAFE(R%, 3)}, Prediction,New String() {""GDP2011PCPPP(MOSTRECENT) (Log)"", ""PopulationUrban%(MOSTRECENT) (Log)"", ""WSSJMPWaterTotal%Piped(MOSTRECENT) (Log)""}, context)


                                    Dim PosNewSingle_Start = strcode.IndexOf("New Single()")
                                    Dim PosNewString_Start = strcode.IndexOf("New String()")
                                    Dim posstart = PosNewSingle_Start + Len("New Single()")
                                    Dim strlen = PosNewString_Start - posstart - 1


                                    'If strcode.Contains("Call AnalFuncSE(") Then Stop
                                    '"Call AnalFuncSE(""Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)"",New Single() {SGEM(R%), YTHBULGE(R%), energydepend(R%)}, Prediction, SE,New String() {""GEM[0](0) (Linear)"", ""YTHBULGE[0](0) (Linear)"", ""EnExValPerGDP(0) (Linear)""}, context)"
                                    'mti 2025/08/07 start
                                    Dim Savestrcode As String = strcode
                                    strlen = strlen - 1 'to avoid picking upthe comma before "New String": New Single() {GDPPCP(R%), UrbanPercent(R%), WATSAFE(R%, 3)}, Prediction, New String()
                                    'mti 2025/08/07 end
                                    If strcode.Contains("Call AnalFuncSE(") Then strlen = strlen - 4 'takes out , SE, 
                                    If posstart > 0 And Len(strcode) > posstart + strlen Then
                                        strcode = strcode.Substring(posstart, strlen).Trim
                                    Else
                                        strcode = ""
                                    End If

                                    If strcode > "" Then



                                        Dim PosCurlyOpen = strcode.IndexOf("{")
                                        Dim PosCurlyClose = strcode.IndexOf("}")
                                        LeftSide(ACnt) = strcode.Substring(PosCurlyClose + 3).Trim
                                        posstart = PosCurlyOpen + 1
                                        strlen = PosCurlyClose - PosCurlyOpen - 1
                                        strcode = strcode.Substring(posstart, strlen).Trim
                                        Rightside(ACnt) = StripAmaxAmin(strcode)

                                        'mti 2025/08/07 start
                                        ModuleName(ACnt) = ModName
                                        'Console.WriteLine(Savestrcode)

                                        Rightside(ACnt) = CleanString(Rightside(ACnt))  'remove trailing comments and space, if any -  added 'mti 2025/09/17 start

                                        CommentText(ACnt) = "ANALFUNC," & GetRegressionFunctionName(Savestrcode, ACnt, LeftSide, Rightside, BaseYear, ConstantDollarsYear)
                                        'mti 2025/08/07 end
                                    End If
                                    'end new code on AnalFunc
                                    'mti 2025/06/24 end



                                    'mti 07 Feb 03
                                    ModuleName(ACnt) = ModName ''mti 2025/08/07 start comment it later



                                    'End Build Documentation 11/11/2003

                                    'jrs 2023/07/20 *** start ***
                                    If sCurrSubName > "" Then
                                        If SubParametersDeclare.ContainsKey(sCurrSubName) Then
                                            If SubParametersDeclare(sCurrSubName).Count > 0 Then
                                                Dim sLeft As String = LeftSide(ACnt)
                                                pos = sLeft.IndexOf("(")
                                                If pos > 0 Then
                                                    sLeft = sLeft.Substring(0, pos)
                                                End If

                                                For i = 1 To SubParametersDeclare(sCurrSubName).Count
                                                    If SubParametersDeclare(sCurrSubName)(i)(0) = sLeft And SubParametersDeclare(sCurrSubName)(i)(1) = "" Then
                                                        SubParametersDeclare(sCurrSubName)(i)(1) = Rightside(ACnt)
                                                        pos = SubParametersDeclare(sCurrSubName)(i)(1).IndexOf("(")
                                                        If pos > 0 Then
                                                            SubParametersDeclare(sCurrSubName)(i)(1) = SubParametersDeclare(sCurrSubName)(i)(1).Substring(0, pos)
                                                        End If
                                                        'if call exists already, assign
                                                        If SubParametersCall.ContainsKey(sCurrSubName) Then
                                                            Dim tmpACnt = CInt(SubParametersCall(sCurrSubName)(0))
                                                            tmpACnt = tmpACnt + i
                                                            CodeBlock(tmpACnt) = cBlock 'strcode

                                                            LeftSide(tmpACnt) = SubParametersCall(sCurrSubName)(i)
                                                            Rightside(tmpACnt) = Rightside(ACnt)

                                                            ModuleName(tmpACnt) = ModName$
                                                        End If
                                                        Exit For
                                                    ElseIf Rightside(ACnt).Contains(SubParametersDeclare(sCurrSubName)(i)(0)) Then
                                                        If SubParametersDeclare(sCurrSubName)(i)(1) = "" Then
                                                            SubParametersDeclare(sCurrSubName)(i)(1) = SubParametersDeclare(sCurrSubName)(i)(0)
                                                        End If
                                                        SubParametersDeclare(sCurrSubName)(i)(0) = LeftSide(ACnt)
                                                        pos = SubParametersDeclare(sCurrSubName)(i)(0).IndexOf("(")
                                                        If pos > 0 Then
                                                            SubParametersDeclare(sCurrSubName)(i)(0) = SubParametersDeclare(sCurrSubName)(i)(0).Substring(0, pos)
                                                        End If
                                                        If SubParametersCall.ContainsKey(sCurrSubName) Then
                                                            Dim tmpACnt = CInt(SubParametersCall(sCurrSubName)(0))
                                                            tmpACnt = tmpACnt + i
                                                            CodeBlock(tmpACnt) = cBlock 'strcode

                                                            LeftSide(tmpACnt) = LeftSide(ACnt)
                                                            Rightside(tmpACnt) = SubParametersCall(sCurrSubName)(i)

                                                            ModuleName(tmpACnt) = ModName$
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If
                                    'End If
                                    'jrs 2023/07/20 *** end ***


                                    'mti 2025/06/24 branch merged with AnalFunc end

                                    'mti 2025/12/16 Victory Day Start Bilateral Writes from XDYAD to Bilateral Variables
                                ElseIf strcode.IndexOf("SaveBilatToCommon(", StringComparison.OrdinalIgnoreCase) >= 0 _
                                                 OrElse strcode.IndexOf("SaveBilatRateToCommon(", StringComparison.OrdinalIgnoreCase) >= 0 Then


                                    'Handle dyadic writers to common block: SaveBilatToCommon / SaveBilatRateToCommon

                                    strcode = strcode.Trim()
                                    pos = strcode.IndexOf("'"c)

                                    'If a comment character was found...
                                    If pos = 0 Then strcode = ""
                                    If pos > 0 Then strcode = strcode.Substring(0, pos) 'strip trailing comment (starting comment gives empty)

                                    If strcode.Length > 0 Then

                                        Dim isRate As Boolean = (strcode.IndexOf("SaveBilatRateToCommon(", StringComparison.OrdinalIgnoreCase) >= 0)
                                        Dim procName As String = If(isRate, "SaveBilatRateToCommon", "SaveBilatToCommon")

                                        'Parse to arguments portion
                                        Dim openParenIndex As Integer = strcode.IndexOf("("c)
                                        Dim closeParenIndex As Integer = strcode.LastIndexOf(")"c)

                                        If openParenIndex > -1 AndAlso closeParenIndex > openParenIndex Then

                                            Dim allArgs As String = strcode.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1)

                                            'Use existing splitter (handles nesting)
                                            Dim arguments As String() = SplitArgumentsRespectingNesting(allArgs)

                                            'Expected patterns:
                                            '  (1) SaveBilatToCommon(BilatVar(,), CommonVar(,))
                                            '  (2) SaveBilatToCommon(BilatVar(,,), idx, CommonVar(,))
                                            '  (3) SaveBilatToCommon(BilatVar(,), CommonVar(,), ConvFactor)
                                            '  (4) SaveBilatRateToCommon(BilatVar(,,), idx, CommonVar(,))

                                            Dim dyad2D As String = "(act%, tar%)"

                                            If arguments.Length >= 2 Then

                                                Dim bilatVar As String = ""
                                                Dim commonVar As String = ""
                                                Dim idxArg As String = ""

                                                If arguments.Length = 2 Then
                                                    ' (BilatVar, CommonVar)
                                                    bilatVar = arguments(0).Trim()
                                                    commonVar = arguments(1).Trim()

                                                ElseIf arguments.Length >= 3 Then
                                                    Dim arg1 As String = arguments(0).Trim()
                                                    Dim arg2 As String = arguments(1).Trim()
                                                    Dim arg3 As String = arguments(2).Trim()

                                                    'mti 2025/12/16 Victory Day Start
                                                    'Use simple numeric check for idx (positive integer)
                                                    Dim idxValue As Integer
                                                    Dim looksLikeIdx As Boolean = Not String.IsNullOrWhiteSpace(arg2) AndAlso
                                                                                        Integer.TryParse(arg2.Trim(), idxValue) AndAlso
                                                                                        idxValue >= 0
                                                    'mti 2025/12/16 Victory Day End

                                                    If looksLikeIdx Then
                                                        ' (BilatVar, idx, CommonVar)
                                                        bilatVar = arg1
                                                        idxArg = arg2.Trim()
                                                        commonVar = arg3
                                                    Else
                                                        ' (BilatVar, CommonVar, ConvFactor)  -> convfactor doesn't affect linkage
                                                        bilatVar = arg1
                                                        commonVar = arg2
                                                    End If
                                                End If

                                                If bilatVar <> "" AndAlso commonVar <> "" Then

                                                    'Advance counter and create codeblock
                                                    ACnt = ACnt + 1
                                                    CodeBlock(ACnt) = cBlock
                                                    cBlock = ""

                                                    'Equation form
                                                    LeftSide(ACnt) = commonVar & dyad2D

                                                    If idxArg <> "" Then
                                                        Rightside(ACnt) = bilatVar & "(act%, tar%, " & idxArg & ")"
                                                    Else
                                                        Rightside(ACnt) = bilatVar & dyad2D
                                                    End If

                                                    ModuleName(ACnt) = ModName$

                                                    CommentText(ACnt) = If(isRate, "SAVE BILAT RATE TO COMMON", "SAVE BILAT TO COMMON")
                                                End If

                                            End If
                                        End If
                                    End If

                                    'mti 2025/12/16 Victory Day End

                                    'mti 2025/08/07 Start Handle Array.copy
                                ElseIf strcode.Contains("Array.Copy(") Then

                                    'Here is a sample: Array.Copy(slvhrd, LVHERD, NR + 1)
                                    'If strcode.Contains("Array.Copy(slvhrd, LVHERD,") Then Stop
                                    strcode = strcode.Trim
                                    pos = strcode.IndexOf("'")

                                    '    If a comment character was found...
                                    If pos = 0 Then strcode = ""
                                    If pos > 0 Then strcode = strcode.Substring(0, pos) 'strip trailing/starting comment (starting comment will give empty string ) 

                                    If strcode.Length > 0 Then

                                        'Parse to arguments portion
                                        Dim openParenIndex As Integer = strcode.IndexOf("(")
                                        Dim closeParenIndex As Integer = strcode.LastIndexOf(")") 'arguments of Array.Cop do not have dimensions

                                        If openParenIndex > -1 AndAlso closeParenIndex > -1 Then
                                            Dim allArgs As String = strcode.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1)
                                            Dim arguments As String() = allArgs.Split(","c)

                                            If arguments.Length >= 2 Then

                                                'Advance counter and create codeblock 
                                                ACnt = ACnt + 1
                                                CodeBlock(ACnt) = cBlock 'strcode
                                                cBlock = "" 'will blank out code for next iterations in loop; so code is not written repeatedly

                                                'Place arguments into the target arrays.
                                                LeftSide(ACnt) = arguments(1).Trim()
                                                Rightside(ACnt) = arguments(0).Trim()

                                                ModuleName(ACnt) = ModName$

                                                'mti 2025/08/07 start
                                                CommentText(ACnt) = "ARRAY COPY"
                                                'mti 2025/08/07 end

                                            End If
                                        End If

                                    End If
                                    'mti 2025/08/07 End Handle Array.copy

                                ElseIf strcode.Length > 2 AndAlso strcode.Trim.ToUpper.Substring(0, 3) = "IF " Then 'jrs 2022/01/03
                                    'take care of single line IF..Then
                                    pos1 = strcode.IndexOf("Then")
                                    pos1 = pos1 + 5 'eqn starts here
                                    'jrs 2022/12/27
                                    If strcode.Length > pos1 Then
                                        pos = strcode.IndexOf(" = ", pos1)
                                    Else
                                        pos = -1
                                    End If
                                    If pos > 0 Then
                                        ACnt = ACnt + 1
                                        CodeBlock(ACnt) = cBlock 'strcode
                                        cBlock = ""
                                        LeftSide(ACnt) = strcode.Substring(pos1, pos - pos1)
                                        Rightside(ACnt) = strcode.Substring(pos + 3)

                                        'check for trailing comment
                                        pos = Rightside(ACnt).Trim.IndexOf("'")
                                        If pos > 0 Then
                                            Rightside(ACnt) = Rightside(ACnt).Substring(0, pos - 1)
                                        End If

                                        'mti 2013/12/01 start - also take code before If then as rightside
                                        pos1 = strcode.IndexOf("Then")
                                        Rightside(ACnt) = Rightside(ACnt) + " , " + strcode.Substring(0, pos1 - 1).Trim
                                        'mti 2013/12/01 -end

                                        ModuleName(ACnt) = ModName$

                                        'mti 2025/08/07 start
                                        CommentText(ACnt) = "IF..STRUCTURE"
                                        'mti 2025/08/07 end

                                    End If
                                Else 'all other cases (non structure)

                                    'If ACnt >= 13906 Then Stop

                                    pos = strcode.IndexOf(" = ")
                                    If pos > 0 Then

                                        'mti 2017/10/19 start

                                        'everything before the '=' sign (usually 1 variable)
                                        Dim Tmp_LeftSide$ = strcode.Substring(0, pos)

                                        'everything after the '=' sign, may be more than 1 variable
                                        Dim Tmp_RightSide$ = strcode.Substring(pos + 3)

                                        'check for trailing comment
                                        pos = Tmp_RightSide.Trim.IndexOf("'")
                                        If pos > 0 Then
                                            Tmp_RightSide$ = Tmp_RightSide$.Substring(0, pos - 1)
                                        End If


                                        '                                If Trim(Rightside(ACnt)) = "0" Or Val(Trim(Rightside(ACnt))) > 0 Then Stop
                                        Dim Val As Single
                                        'jrs 2023/07/06
                                        'Include numeric values being assing, for example:
                                        'ConvTime = 85
                                        'if not we keep looking for assignment of this variable up
                                        'and sometimes is found somewhere else incorrectly
                                        'like the case for NEONATMOR, that's how it's connecting to CLPC
                                        'this is causing problems if there's an if statement, where the first part assigns a variable, and the else asigns a 0
                                        'like CalTarSupCrop = 0 for CLPC in Agriculture
                                        'Disable numbers only in the else branch
                                        'If Not Single.TryParse(Tmp_RightSide$.Trim, Val) Then
                                        'Not (Numeric and in an else branch) 
                                        Dim bCond As Boolean
                                        bCond = Not Single.TryParse(Tmp_RightSide$.Trim, Val)
                                        'if it's numeric check if it's in the else branch to only skip in that case
                                        If Not bCond Then
                                            bCond = True
                                            If IFNest > 0 Then
                                                Dim posElse As Integer
                                                posElse = cBlock.IndexOf("Else")
                                                If posElse > -1 Then
                                                    bCond = False
                                                End If
                                            End If
                                        End If
                                        If bCond Then
                                            ACnt = ACnt + 1
                                            CodeBlock(ACnt) = cBlock 'strcode

                                            LeftSide(ACnt) = Tmp_LeftSide$.Trim
                                            Rightside(ACnt) = StripAmaxAmin(Tmp_RightSide$.Trim) 'mti 2025/07/09

                                            'mti 2024/05/17 start
                                            'If LeftSide(ACnt).Contains("Dim WaterImproved") Then Stop
                                            pos = LeftSide(ACnt).IndexOf("Dim")
                                            If pos >= 0 Then
                                                pos1 = pos + 4
                                                LeftSide(ACnt) = LeftSide(ACnt).Substring(pos1)
                                                pos = LeftSide(ACnt).IndexOf("As")
                                                If pos >= 0 Then
                                                    LeftSide(ACnt) = LeftSide(ACnt).Substring(0, pos - 1)
                                                End If
                                            End If
                                            'mti 2024/05/17 end

                                            'mti 07 Feb 03
                                            ModuleName(ACnt) = ModName$

                                            'mti 2025/08/07 start
                                            CommentText(ACnt) = ""
                                            'mti 2025/08/07 end

                                            'jrs 2023/07/20 *** start ***
                                            If sCurrSubName > "" Then
                                                If SubParametersDeclare.ContainsKey(sCurrSubName) Then
                                                    If SubParametersDeclare(sCurrSubName).Count > 0 Then
                                                        Dim sLeft As String = LeftSide(ACnt)
                                                        pos = sLeft.IndexOf("(")
                                                        If pos > 0 Then
                                                            sLeft = sLeft.Substring(0, pos)
                                                        End If

                                                        For i = 1 To SubParametersDeclare(sCurrSubName).Count
                                                            If SubParametersDeclare(sCurrSubName)(i)(0) = sLeft And SubParametersDeclare(sCurrSubName)(i)(1) = "" Then
                                                                SubParametersDeclare(sCurrSubName)(i)(1) = Rightside(ACnt)
                                                                pos = SubParametersDeclare(sCurrSubName)(i)(1).IndexOf("(")
                                                                If pos > 0 Then
                                                                    SubParametersDeclare(sCurrSubName)(i)(1) = SubParametersDeclare(sCurrSubName)(i)(1).Substring(0, pos)
                                                                End If
                                                                'if call exists already, assign
                                                                If SubParametersCall.ContainsKey(sCurrSubName) Then
                                                                    Dim tmpACnt = CInt(SubParametersCall(sCurrSubName)(0))
                                                                    tmpACnt = tmpACnt + i
                                                                    CodeBlock(tmpACnt) = cBlock 'strcode

                                                                    LeftSide(tmpACnt) = SubParametersCall(sCurrSubName)(i)
                                                                    Rightside(tmpACnt) = Rightside(ACnt)
                                                                    ModuleName(tmpACnt) = ModName$
                                                                End If
                                                                Exit For
                                                            ElseIf Rightside(ACnt).Contains(SubParametersDeclare(sCurrSubName)(i)(0)) Then
                                                                If SubParametersDeclare(sCurrSubName)(i)(1) = "" Then
                                                                    SubParametersDeclare(sCurrSubName)(i)(1) = SubParametersDeclare(sCurrSubName)(i)(0)
                                                                End If
                                                                SubParametersDeclare(sCurrSubName)(i)(0) = LeftSide(ACnt)
                                                                pos = SubParametersDeclare(sCurrSubName)(i)(0).IndexOf("(")
                                                                If pos > 0 Then
                                                                    SubParametersDeclare(sCurrSubName)(i)(0) = SubParametersDeclare(sCurrSubName)(i)(0).Substring(0, pos)
                                                                End If

                                                                If SubParametersCall.ContainsKey(sCurrSubName) Then
                                                                    Dim tmpACnt = CInt(SubParametersCall(sCurrSubName)(0))
                                                                    tmpACnt = tmpACnt + i
                                                                    CodeBlock(tmpACnt) = cBlock 'strcode
                                                                    Dim bUpdLeft As Boolean
                                                                    bUpdLeft = (LeftSide(tmpACnt) = "")
                                                                    'If tmpACnt = 13907 Then Stop
                                                                    If Not bUpdLeft Then
                                                                        'Check if existing Left is a full variable
                                                                        pos = LeftSide(tmpACnt).IndexOf("(")
                                                                        Dim sVar As String
                                                                        If pos > 0 Then
                                                                            sVar = LeftSide(tmpACnt).Substring(0, pos).Trim
                                                                        Else
                                                                            sVar = LeftSide(tmpACnt).Trim
                                                                        End If
                                                                        If Not VNames.Contains(sVar) Then
                                                                            bUpdLeft = True
                                                                        End If
                                                                    End If
                                                                    If bUpdLeft Then
                                                                        LeftSide(tmpACnt) = LeftSide(ACnt)
                                                                    End If
                                                                    Rightside(tmpACnt) = SubParametersCall(sCurrSubName)(i)

                                                                    ModuleName(tmpACnt) = ModName$

                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                    'jrs 2023/07/20 *** end ***
                                                End If
                                            End If
                                            'jrs 2023/07/20 *** end ***
                                        End If
                                        cBlock = ""
                                        'mti 2017/10/19 End
                                    Else 'jrs 2023/07/20 check if it's the call of a sub or function
                                        pos = strcode.IndexOf("(")
                                        If pos > 0 Then
                                            pos2 = strcode.LastIndexOf(")")
                                            pos3 = strcode.LastIndexOf("'")
                                            If pos2 = strcode.Length - 1 Or pos3 = pos2 + 2 Then
                                                If strcode.Substring(0, 4) <> "Dim " And strcode.Substring(0, 6) <> "ReDim " And (strcode.Length > 9 AndAlso strcode.Substring(0, 10) <> "Array.Copy") And (strcode.Length > 5 AndAlso strcode.Substring(0, 6) <> "Debug.") Then
                                                    Dim xPars = strcode.Substring(0, pos2 - 1).Split(CChar(","))
                                                    Dim sSubName As String
                                                    sSubName = xPars(0).Substring(0, pos)
                                                    sSubName = sSubName.ToUpper.Replace("CALL", "")
                                                    sSubName = sSubName.Trim
                                                    If Not sSubName.Contains(" ") And Not sSubName.Contains(".ADD") Then
                                                        If SubParametersDeclare.ContainsKey(sSubName) Then
                                                            For i = 0 To xPars.Length - 1
                                                                ACnt = ACnt + 1
                                                                CodeBlock(ACnt) = cBlock 'strcode
                                                                'This assumes parameters are being used to drive others
                                                                'it could be the other way around if they're byRef
                                                                If i = 0 Then
                                                                    Rightside(ACnt) = xPars(0).Substring(pos + 1)
                                                                Else
                                                                    Rightside(ACnt) = xPars(i)
                                                                End If
                                                                LeftSide(ACnt) = SubParametersDeclare(sSubName)(i + 1)(0)
                                                                ModuleName(ACnt) = ModName$
                                                                'mti 2025/08/07 start
                                                                CommentText(ACnt) = "SUB CALL"
                                                                'mti 2025/08/07 end
                                                            Next
                                                        Else
                                                            If pos2 - pos > 1 Then
                                                                If Not SubParametersCall.ContainsKey(sSubName) Then
                                                                    SubParametersCall.Add(sSubName, New Dictionary(Of Integer, String))
                                                                    SubParametersCall(sSubName).Add(0, ACnt.ToString)
                                                                    SubParametersCall(sSubName).Add(1, xPars(0).Substring(pos + 1))
                                                                    Dim idx As Integer = 1
                                                                    For i = 1 To xPars.Length - 1
                                                                        If xPars(i).Trim <> ")" And xPars(i).Trim > "" Then
                                                                            SubParametersCall(sSubName).Add(idx + 1, xPars(i).Trim)
                                                                            idx = idx + 1
                                                                        End If
                                                                    Next

                                                                    ACnt = ACnt + idx
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If 'pos > 0
                                End If   'InStr(strcode, "Call XYTABL(") = 1
                            Else  ''Mid(strcode, 1, 1) <> "'" comment
                                'comment line keeps on adding up
                            End If 'Mid(strcode, 1, 1) <> "'"


                            'If InStr(strcode, "SDYADPOPFOREIGNFORCED(rsD.Fields(") > 0 Then Stop

                            'mti 2013/12/01 - driver fix -start
                        Else 'is structure - handle If; elseif

                            'mti 2026/05/13 start - add handling of for; update handling of If - avoid code like "ifshealth"



                            'mti 2026/05/13 start - add handling of for
                            Dim TmpForCounter As String = ""
                            Dim TmpForStart As String = ""
                            Dim TmpForEnd As String = ""
                            Dim TmpForStep As String = ""

                            If TryReadForLine(strcode, TmpForCounter, TmpForStart, TmpForEnd, TmpForStep) Then

                                'A For loop has started.
                                'Keep track of where this loop begins in Leftside()/Rightside().
                                ForAcnt = ForAcnt + 1

                                Start_For_ACnt(ForAcnt) = ACnt
                                ForCounter(ForAcnt) = TmpForCounter.Trim

                                Dim TmpStartNum As Integer
                                Dim TmpEndNum As Integer
                                Dim TmpStepNum As Integer = 1
                                Dim StepOK As Boolean = True

                                If TmpForStep.Trim <> "" Then
                                    StepOK = Integer.TryParse(TmpForStep.Trim, TmpStepNum)
                                End If

                                'Do not expand full dimension loops such as:
                                'For j = 0 To NCohorts
                                'For R = 1 To NR
                                'For R = 1 To NR%
                                If IsNTotalLoop(TmpForStart, TmpForEnd) Then

                                    ForExpand(ForAcnt) = False
                                    ForStepVal(ForAcnt) = 1

                                ElseIf Integer.TryParse(TmpForStart.Trim, TmpStartNum) AndAlso
                                                       Integer.TryParse(TmpForEnd.Trim, TmpEndNum) AndAlso
                                                       StepOK AndAlso TmpStepNum <> 0 Then

                                    'Only expand loops where the start and end are numeric.
                                    'Example: For G = 1 To 2
                                    '
                                    'However, do not expand loops with a large numeric range.
                                    'This avoids creating too many extra Leftside()/Rightside() rows.
                                    '
                                    'Example:
                                    'For G = 1 To 2      expands
                                    'For k = 1 To 6      expands
                                    'For k = 1 To 20     does not expand

                                    If Math.Abs(TmpEndNum - TmpStartNum) > 10 Then

                                        'The loop is numeric, but the range is too large.
                                        'So keep the counter as a regular loop/index variable
                                        'and do not create extra Leftside()/Rightside() rows.
                                        ForExpand(ForAcnt) = False
                                        ForStepVal(ForAcnt) = 1

                                    Else

                                        'The loop is numeric and small enough to expand.
                                        'Example:
                                        'For G = 1 To 2
                                        ForExpand(ForAcnt) = True
                                        ForStartVal(ForAcnt) = TmpStartNum
                                        ForEndVal(ForAcnt) = TmpEndNum
                                        ForStepVal(ForAcnt) = TmpStepNum

                                    End If

                                Else

                                    'If the loop cannot be safely computed, keep it as an index loop.
                                    'This avoids writing incorrect extra rows.
                                    ForExpand(ForAcnt) = False
                                    ForStepVal(ForAcnt) = 1

                                End If


                            ElseIf TryReadNextLine(strcode, TmpForCounter) Then

                                'A Next line has been reached.
                                'If the loop is expandable, materialize its rows now.
                                If ForAcnt > 0 Then

                                    If TmpForCounter.Trim <> "" Then
                                        If Not SameCounterName(TmpForCounter, ForCounter(ForAcnt)) Then
                                            'Optional debugging check.
                                            'Stop
                                        End If
                                    End If

                                    If ForExpand(ForAcnt) Then

                                        ExpandForBlock(
                                                            LeftSide,
                                                            Rightside,
                                                            ModuleName,
                                                            CommentText,
                                                            CodeBlock,
                                                            ACnt,
                                                            Start_For_ACnt(ForAcnt),
                                                            ForCounter(ForAcnt),
                                                            ForStartVal(ForAcnt),
                                                            ForEndVal(ForAcnt),
                                                            ForStepVal(ForAcnt)
                                                        )

                                    End If

                                    'Clear this For level.
                                    Start_For_ACnt(ForAcnt) = 0
                                    ForCounter(ForAcnt) = ""
                                    ForStartVal(ForAcnt) = 0
                                    ForEndVal(ForAcnt) = 0
                                    ForStepVal(ForAcnt) = 0
                                    ForExpand(ForAcnt) = False

                                    ForAcnt = ForAcnt - 1
                                    If ForAcnt < 0 Then ForAcnt = 0

                                End If



                                'mti 2026/05/13 end  add handling of for


                                'mti 2026/05/13 start - update handling of If - avoid code like "ifshealth"; also changed if to elseif for this branch to accommodate a branch of "for"handling is added 
                                'If strcode.Length > 1 AndAlso (strcode.Trim.ToUpper.Substring(0, 2) = "IF" Or (strcode.Length > 6 AndAlso strcode.Trim.ToUpper.Substring(0, 7) = "ELSEIF ")) Then
                            ElseIf strcode.Length > 1 AndAlso (strcode.Trim.ToUpper.Substring(0, 3) = "IF " Or (strcode.Length > 6 AndAlso strcode.Trim.ToUpper.Substring(0, 7) = "ELSEIF ")) Then
                                'mti 2026/05/13 end

                                'collect variables, if any from conditions

                                'mti 2025/08/07 start  'variables inside the IF condition will be treated as right variable of previous left variable, or for dimension determination

                                'If strcode.Contains("if .. code") Then Stop

                                If strcode.Trim.ToUpper.Substring(0, 2) = "IF" Then   'ElseIf should not increase counter
                                    IFAcnt = IFAcnt + 1
                                    If IFAcnt > 1 Then 'nested If, append If to  prior lines
                                        For i = Start_IF_ACnt(IFAcnt - 1) + 1 To ACnt
                                            'If Rightside(i) IsNot Nothing AndAlso Rightside(i).IndexOf("HLMALARIAMORT") > -1 Then Stop
                                            'If Rightside(i) IsNot Nothing AndAlso Rightside(i).IndexOf("MalMortR") > -1 Then Stop
                                            'mti 2026/05/13  start the following writing not needed as the condition string will be built fresh for each ElseIf and If,
                                            'and appended to lines in the branch when ElseIf or Else or End If is hit,
                                            'so no need to append to prior lines here when nested, as it will be done when branch ends
                                            'Rightside(i) = Rightside(i) + ", " + IFCondition(IFAcnt - 1)
                                            'mti 2026/05/13  end
                                        Next i
                                    End If
                                Else     '"ElseIf.." should not increase counter, and append If to  prior lines
                                    For i = Start_IF_ACnt(IFAcnt) + 1 To ACnt
                                        Rightside(i) = Rightside(i) + ", " + IFCondition(IFAcnt)
                                    Next i
                                End If

                                'reset IFs model code line counter from  where If.. condition appending will start next time
                                Start_IF_ACnt(IFAcnt) = ACnt

                                'extracting IF condition text
                                Dim Tmp_IFsStr$ = strcode.Trim
                                'remove trailing comment if any
                                pos = Tmp_IFsStr$.IndexOf("'")
                                If pos > 0 Then
                                    Tmp_IFsStr$ = Tmp_IFsStr$.Substring(0, pos - 1)
                                End If
                                Tmp_IFsStr$ = Tmp_IFsStr$.Replace("=", " // ")  ''replace = sign, if any
                                IFCondition(IFAcnt) = Tmp_IFsStr$   'Else IF will right a fresh string


                                'mti 2026/05/13 start this is not neded as all conidtions are written one at a time when ElseIf or Else or End If is hit,
                                'and built fresh for each If and ElseIf, so no need to build nested condition here when If is hit, as it will be done when branch ends

                                ''build nested If condition text
                                'Tmp_IFsStr$ = IFCondition(1)
                                'For i = 2 To IFAcnt
                                '    Tmp_IFsStr$ = Tmp_IFsStr$ + ", " + IFCondition(i)
                                'Next
                                'IFCondition(IFAcnt) = Tmp_IFsStr$

                                'mti 2026/05/13 end

                            Else        'check   for "END IF" or "ELSE"
                                'If ACnt > 24730 Then Stop
                                'mti 2025/08/27b start
                                'If strcode.Length > 5 AndAlso (strcode.Trim.ToUpper.Substring(0, 6) = "END IF" Or (strcode.Length > 3 AndAlso strcode.Trim.ToUpper.Substring(0, 4) = "ELSE")) Then
                                If (strcode.Length > 5 AndAlso (strcode.Trim.ToUpper.Substring(0, 6) = "END IF") Or (strcode.Length > 3 AndAlso strcode.Trim.ToUpper.Substring(0, 4) = "ELSE")) Then
                                    'mti 2025/08/27b end

                                    'if branched or ended; add conditions with previous leftside  var

                                    ''mti 2025/08/07 start
                                    If strcode.Trim.Length > 5 AndAlso strcode.Trim.ToUpper.Substring(0, 6) = "END IF" Then

                                        'mti 2026/05/13 start
                                        'End If reached. First append the current IF condition
                                        'to all rows created inside this IF branch.
                                        '
                                        'Then expand rows when the IF condition restricts a common
                                        'index to a specific list of numeric elements.
                                        '
                                        'Example:
                                        'If (k // 27 Or k // 28) Or (k // 9 Or k // 10) Then
                                        '
                                        'will create separate rows for:
                                        'k = 27, 28, 9, 10

                                        Dim Tmp_IF_BranchStart As Integer = Start_IF_ACnt(IFAcnt) + 1
                                        Dim Tmp_IF_BranchEnd As Integer = ACnt

                                        For i = Tmp_IF_BranchStart To Tmp_IF_BranchEnd
                                            Rightside(i) = Rightside(i) + ", " + IFCondition(IFAcnt)
                                        Next i

                                        'Expand only the rows from this IF branch.
                                        'Tmp_IF_BranchEnd is the old ACnt, so newly appended rows
                                        'are not recursively expanded in the same pass.
                                        ExpandRowsForSpecificIfEquals(LeftSide, Rightside, ModuleName, CommentText, CodeBlock, ACnt, Tmp_IF_BranchStart, Tmp_IF_BranchEnd)
                                        'mti 2026/05/13 end

                                        'For i = Start_IF_ACnt(IFAcnt) + 1 To ACnt
                                        '    'If i = 24749 Then Stop
                                        '    'If i = 24748 Then Stop   '?RightSide(24748)  "GDPPCP(R%), SEDYRSAG15v2(R%, Male)"; leftside->"ageofmarriagecomp(gndr%)"
                                        '    'If i = 24750 Then Stop
                                        '    'If Rightside(i) IsNot Nothing AndAlso Rightside(i).IndexOf("HLMALARIAMORT") > -1 Then Stop
                                        '    'If Rightside(i) IsNot Nothing AndAlso Rightside(i).IndexOf("MalMortR") > -1 Then Stop
                                        '    Rightside(i) = Rightside(i) + ", " + IFCondition(IFAcnt)
                                        'Next i
                                        'mti 2026/05/13 end

                                        'mti 2026/05/13  start empty out condition text of this and inside if when If is closed, to avoid incorrect appending to next If when nested
                                        IFCondition(IFAcnt) = ""  'other nside if?
                                        'mti 2026/05/13  end

                                        IFAcnt = IFAcnt - 1
                                        If IFAcnt = -1 Then IFAcnt = 0

                                        'IFAcntTot = IFAcntTot - 1
                                        'If IFAcntTot = -1 Then IFAcntTot = 0

                                        'mti 2025/08/27b start
                                    Else

                                        'mti 2026/05/13 start
                                        'Else branch started.
                                        '
                                        'First close the previous IF branch by appending the current
                                        'IF condition to rows already written in that branch.
                                        '
                                        'Then expand rows when the IF condition restricts a common
                                        'index to a specific list of numeric elements.
                                        '
                                        'After expansion, reset Start_IF_ACnt(IFAcnt) to the new ACnt,
                                        'because the Else branch starts after all expanded IF-branch rows.

                                        Dim Tmp_IF_BranchStart As Integer = Start_IF_ACnt(IFAcnt) + 1
                                        Dim Tmp_IF_BranchEnd As Integer = ACnt

                                        For i = Tmp_IF_BranchStart To Tmp_IF_BranchEnd
                                            Rightside(i) = Rightside(i) + ", " + IFCondition(IFAcnt)
                                        Next i

                                        ExpandRowsForSpecificIfEquals(LeftSide, Rightside, ModuleName, CommentText, CodeBlock, ACnt, Tmp_IF_BranchStart, Tmp_IF_BranchEnd)

                                        'now change the leftside, rightside pointer for the start of the Else branch
                                        Start_IF_ACnt(IFAcnt) = ACnt

                                        'next change the condition string to indicate it is an Else branch
                                        'mti 2026/05/13  End

                                        'you have towriteif branch here first andchange start locatipon
                                        '"ELSE"Branchincode
                                        IFCondition(IFAcnt) = IFCondition(IFAcnt) + " Else Branch"
                                        'mti 2025/08/27b end

                                    End If

                                    If IFAcnt = 0 Then   'mti 2026/05/13  - check - needs to empty out when each If is closed 
                                        For i = 0 To 99  ''mti 2025/07/1410
                                            Start_IF_ACnt(i) = 0
                                        Next i
                                        For i = 0 To 19
                                            IFCondition(i) = ""
                                        Next i
                                        'IFAcnt = 0
                                    End If
                                    ''mti 2025/08/07 end

                                End If  'checking "End If" or "Else" in code line



                            End If    'End of checking If. Else  If, Else, End etc




                        End If  'not isvbStructure



                    End If 'strcode <> ""

                    '    If ACnt = 3328 Then Stop

                    '    If InStr(LeftSide(ACnt), "tef") > 0 Then Stop
                    '    If InStr(LeftSide(ACnt), "MFPGro") > 0 Then Stop
                    'If ACnt = 2458 Then Stop
                    strcode = reader.ReadLine
                    'If strcode.Contains("CoalMaxProd = ENDEM(R%) * EnpCoalDemRI(R%)") Then Stop
                    'If strcode.Contains("GDSInfraBox(R%) = CSng(AMAX(0F, GDSInfraBox(R%) + AddtoBox - GetFromBox))") Then Stop
                    'If ACnt = 1314 And ModName$ = "SOCIOPOL.FORECAST.VB" Then Stop
                    'If strcode.Contains("MunDemandPC (Linear) versus GDP2011PCPPP(MOSTRECENT)") Then Stop
                Loop
            End Using


            'codelines = ACnt ' + 1 'mti 2024/05/17 start

        Catch ex As Exception
            'mti 2025/07/14 start
            Console.WriteLine(ex.Message & vbCr & "Error in parsing " & ModName$ & " : " & strcode)
            Dim ErrLogPath As String = Path.GetDirectoryName(Filename) + "\ParsingErrorLog.txt"
            Dim LB1 As String = Environment.NewLine
            Dim LB2 As String = Environment.NewLine & Environment.NewLine
            Dim Dsh As String = LB2 & New String("-"c, 40) & LB1
            Dim DblDsh As String = LB1 & New String("="c, 40) & LB1


            File.AppendAllText(ErrLogPath, LB1 & "Error in parsing " & ModName$ & DblDsh & "Error Msg: " & LB2 & ex.Message & Dsh & "Probable Parsing Code Location: " & LB2 & ex.StackTrace & Dsh & "IFs Model Code Location: " & LB2 & strcode & Dsh & "IFs Model Code Block (" & ACnt & "): " & LB2 & CodeBlock(ACnt) & LB2 & DblDsh)


            'Debug.Print(ex.Message + vbCr + "Error in parsing line " + strcode + " in " + ModName$ + vbCr + CodeBlock(ACnt))

            'Finally
            '   Close #1

            'this helps debug

            'Dim FN$, spos%
            '
            'FN$ = Mid(Filename, Len(PutFdrv$ + "Code\") + 1, 5) + ".txt"
            '
            'Open "C:\My Documents\" + FN$ For Output As #2
            'For i2 = 206 To 696 'replace these
            'Print #2, "==== " + Trim(Str(i2)) + " =========================="
            'Print #2, LeftSide(i2) + " = " + Rightside(i2)
            'Print #2, CodeBlock(i2)
            'Next
            'Close #2

            'Exit Sub
            '
            'jrsaHdlr:
            '    MsgBox Err.Description
            '    Resume Next


        End Try
        codelines = ACnt ' + 1 'mti 2024/05/17 start





    End Sub
    'mti 2026/05/13 start new procedures and class for if..condition (multiple) handling and expansion

    Private Class IfEqualsExpansionGroup
        'mti 2026/05/13 new class for if..condition (multiple) handling and expansion
        Public CounterName As String = ""
        Public Values As New List(Of String)

    End Class


    Private Sub ExpandRowsForSpecificIfEquals(
    ByRef Leftside() As String,
    ByRef Rightside() As String,
    ByVal ModuleName() As String,
    ByVal CommentText() As String,
    ByVal CodeBlock() As String,
    ByRef ACnt As Integer,
    ByVal branchStart As Integer,
    ByVal branchEnd As Integer
)

        'mti 2026/05/13 new procedure
        'Expands rows when an appended IF condition restricts a common
        'dimension/index to specific numeric values.
        '
        'Example:
        '
        'Leftside:
        'modmordstdet(j, R%, k)
        '
        'Rightside:
        'modmordstdet(j, R%, k) * (1 + PM2pt5Diff),
        'If (k // 27 Or k // 28) Or (k // 9 Or k // 10) Then
        '
        'The row becomes multiple rows:
        '
        'modmordstdet(j, R%, 27)
        'modmordstdet(j, R%, 28)
        'modmordstdet(j, R%, 9)
        'modmordstdet(j, R%, 10)
        '
        'Only equality-style // conditions are used.
        'Conditions such as k <> 21 are ignored.
        '
        'Only numeric values or numeric expressions are used.
        'Conditions such as k // NCohorts are ignored.

        If branchStart <= 0 Then Exit Sub
        If branchEnd < branchStart Then Exit Sub

        Dim oldLeft(branchEnd - branchStart) As String
        Dim oldRight(branchEnd - branchStart) As String

        Dim OldModuleName(branchEnd - branchStart) As String
        Dim OldCommentText(branchEnd - branchStart) As String
        Dim OldCodeBlock(branchEnd - branchStart) As String

        Dim i As Integer

        'Save original branch rows before adding new rows.
        For i = branchStart To branchEnd
            oldLeft(i - branchStart) = Leftside(i)
            oldRight(i - branchStart) = Rightside(i)
            OldModuleName(i - branchStart) = ModuleName(i)
            OldCommentText(i - branchStart) = CommentText(i)
            OldCodeBlock(i - branchStart) = CodeBlock(i)
        Next i


        For i = 0 To oldLeft.Length - 1

            Dim oneLeft As String = oldLeft(i)
            Dim oneRight As String = oldRight(i)

            Dim groups As List(Of IfEqualsExpansionGroup) =
            GetIfEqualsExpansionGroups(oneLeft, oneRight)

            If groups.Count = 0 Then
                Continue For
            End If

            Dim combos As New List(Of Dictionary(Of String, String))(0)
            BuildIfExpansionCombinations(groups, 0, New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase), combos)

            If combos.Count = 0 Then
                Continue For
            End If

            'Use the original row as the first expanded case.
            Leftside(branchStart + i) = ReplaceIfExpansionValues(oneLeft, combos(0))
            Rightside(branchStart + i) = ReplaceIfExpansionValues(oneRight, combos(0))

            'Append additional cases.
            For c As Integer = 1 To combos.Count - 1

                ACnt = ACnt + 1

                EnsureLeftRightCapacity(Leftside, Rightside, ACnt)

                Leftside(ACnt) = ReplaceIfExpansionValues(oneLeft, combos(c))
                Rightside(ACnt) = ReplaceIfExpansionValues(oneRight, combos(c))

                CommentText(ACnt) = OldCommentText(i)
                ModuleName(ACnt) = OldModuleName(i)
                CodeBlock(ACnt) = OldCodeBlock(i)

            Next c

        Next i

    End Sub


    Private Function GetIfEqualsExpansionGroups(
    ByVal leftText As String,
    ByVal rightText As String
) As List(Of IfEqualsExpansionGroup)

        'mti 2026/05/13 new procedure
        'Finds equality conditions of the form:
        '
        'k // 27
        'k // 28
        'k // (24 + 3)
        '
        'It ignores:
        '
        'k <> 21
        'k > 6
        'k // NCohorts
        '
        'The candidate counter must appear in the left side or in the main
        'right-side expression before the appended IF condition. This avoids
        'expanding rows for ordinary switches such as hlpm2pt5sw // 0.

        Dim out As New List(Of IfEqualsExpansionGroup)

        If rightText Is Nothing Then Return out

        Dim mainRight As String = GetRightExpressionBeforeIfCondition(rightText)
        Dim searchableMainText As String = Convert.ToString(leftText) & ", " & mainRight

        'This finds equality-style conditions using //.
        '
        'The value side must be numeric or a numeric expression.
        'The lookahead stops before Or, OrElse, And, AndAlso, Then, comma,
        'closing parenthesis, or end of string.
        Dim pattern As String =
        "(?<![<>=])\b([A-Za-z_][A-Za-z0-9_]*%?)\s*//\s*([0-9\+\-\*\/\(\)\s]+?)(?=\s*(?:\)|,|Then\b|Or\b|OrElse\b|And\b|AndAlso\b|$))"

        Dim matches As MatchCollection =
        Regex.Matches(rightText, pattern, RegexOptions.IgnoreCase)

        For Each m As Match In matches

            Dim counterName As String = m.Groups(1).Value.Trim
            Dim valueExpression As String = m.Groups(2).Value.Trim

            If counterName = "" OrElse valueExpression = "" Then Continue For

            'Do not use a condition unless the counter actually appears
            'in the variable/link expression. This avoids expanding on switches.
            If Not ContainsIndexToken(searchableMainText, counterName) Then
                Continue For
            End If

            Dim valueInt As Integer = 0

            If Not TryEvaluateIntegerExpression(valueExpression, valueInt) Then
                Continue For
            End If

            AddIfExpansionValue(out, counterName, CStr(valueInt))

        Next

        'Keep only groups that have at least one value.
        'Usually there will be one group, such as k = 27, 28, 9, 10, 5, 6.
        For i As Integer = out.Count - 1 To 0 Step -1
            If out(i).Values.Count = 0 Then
                out.RemoveAt(i)
            End If
        Next

        Return out

    End Function


    Private Sub AddIfExpansionValue(
    ByRef groups As List(Of IfEqualsExpansionGroup),
    ByVal counterName As String,
    ByVal counterValue As String
)

        'mti 2026/05/13 new procedure
        'Adds one value to a counter group, avoiding duplicates.
        '
        'Example:
        'k // 27 Or k // 27
        '
        'should only add 27 once.

        Dim cleanCounter As String = CleanIndexToken(counterName)

        If cleanCounter = "" Then Exit Sub

        Dim targetGroup As IfEqualsExpansionGroup = Nothing

        For Each g As IfEqualsExpansionGroup In groups

            If String.Equals(CleanIndexToken(g.CounterName), cleanCounter, StringComparison.OrdinalIgnoreCase) Then
                targetGroup = g
                Exit For
            End If

        Next

        If targetGroup Is Nothing Then

            targetGroup = New IfEqualsExpansionGroup
            targetGroup.CounterName = cleanCounter
            groups.Add(targetGroup)

        End If

        If Not targetGroup.Values.Contains(counterValue) Then
            targetGroup.Values.Add(counterValue)
        End If

    End Sub


    Private Sub BuildIfExpansionCombinations(
    ByVal groups As List(Of IfEqualsExpansionGroup),
    ByVal groupIndex As Integer,
    ByVal current As Dictionary(Of String, String),
    ByRef combos As List(Of Dictionary(Of String, String))
)

        'mti 2026/05/13 new procedure
        'Builds combinations if more than one index is restricted.
        '
        'Example:
        'k = 1, 2
        'g = 1, 2
        '
        'creates:
        'k=1, g=1
        'k=1, g=2
        'k=2, g=1
        'k=2, g=2

        If groupIndex >= groups.Count Then

            Dim oneCombo As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

            For Each kvp As KeyValuePair(Of String, String) In current
                oneCombo(kvp.Key) = kvp.Value
            Next

            combos.Add(oneCombo)
            Exit Sub

        End If

        Dim g As IfEqualsExpansionGroup = groups(groupIndex)

        For Each v As String In g.Values

            current(g.CounterName) = v
            BuildIfExpansionCombinations(groups, groupIndex + 1, current, combos)

        Next

        If current.ContainsKey(g.CounterName) Then
            current.Remove(g.CounterName)
        End If

    End Sub


    Private Function ReplaceIfExpansionValuesOld(
    ByVal textIn As String,
    ByVal values As Dictionary(Of String, String)
) As String

        'mti 2026/05/13 new procedure
        'Replaces each index token with its selected numeric value.

        If textIn Is Nothing Then Return textIn

        Dim outText As String = textIn

        For Each kvp As KeyValuePair(Of String, String) In values

            outText = ReplaceIndexToken(outText, kvp.Key, kvp.Value)

        Next

        Return outText

    End Function
    Private Function ReplaceIfExpansionValues(
    ByVal textIn As String,
    ByVal values As Dictionary(Of String, String)
) As String

        'mti 2026/05/13 updated procedure
        'Replaces index values only inside variable dimension lists.
        '
        'Important:
        'This does NOT replace index names inside appended IF conditions.
        '
        'Example:
        '
        'modmordstdet(j, R%, k) * x, If k // 27 Then
        '
        'becomes:
        '
        'modmordstdet(j, R%, 27) * x, If k // 27 Then
        '
        'not:
        '
        'modmordstdet(j, R%, 27) * x, If 27 // 27 Then
        '
        'Also handles dimension expressions:
        '
        'VAR(R%, k - 1), when k = 27
        '
        'becomes:
        '
        'VAR(R%, 26)

        If textIn Is Nothing Then Return textIn
        If values Is Nothing OrElse values.Count = 0 Then Return textIn

        Dim mainPart As String = textIn
        Dim conditionPart As String = ""

        'The appended IF conditions start after ", If ".
        'Only the main expression before that should be changed.
        Dim pos As Integer =
        textIn.IndexOf(", If ", StringComparison.OrdinalIgnoreCase)

        If pos >= 0 Then
            mainPart = textIn.Substring(0, pos)
            conditionPart = textIn.Substring(pos)
        End If

        mainPart = ReplaceIndexValuesInsideVariableDimensionsOnly(mainPart, values)

        Return mainPart & conditionPart

    End Function


    Private Function ReplaceIndexValuesInsideVariableDimensionsOnly(
    ByVal textIn As String,
    ByVal values As Dictionary(Of String, String)
) As String

        'mti 2026/05/13 new procedure
        'Finds simple IFs variable references with dimension lists and replaces
        'index values only inside the parentheses.
        '
        'Examples:
        '
        'modmordstdet(j, R%, k)
        'modmordstdet(j, R%, k - 1)
        'EDPRICR(Male)
        '
        'This avoids changing ordinary text outside variable dimensions.

        If textIn Is Nothing Then Return textIn

        Dim pattern As String =
        "\b([A-Za-z_][A-Za-z0-9_]*)\s*\(([^()]*)\)"

        Dim outText As String =
        Regex.Replace(
            textIn,
            pattern,
            Function(m As Match)

                Dim varName As String = m.Groups(1).Value.Trim
                Dim dimText As String = m.Groups(2).Value

                Dim newDimText As String =
                    ReplaceIndexValuesInsideOneDimensionList(dimText, values)

                Return varName & "(" & newDimText & ")"

            End Function,
            RegexOptions.IgnoreCase
        )

        Return outText

    End Function


    Private Function ReplaceIndexValuesInsideOneDimensionList(
    ByVal dimText As String,
    ByVal values As Dictionary(Of String, String)
) As String

        'mti 2026/05/13 new procedure
        'Replaces index values only inside one comma-separated dimension list.
        '
        'Example:
        '
        'j, R%, k
        '
        'with k = 27 becomes:
        '
        'j, R%, 27
        '
        'Example:
        '
        'j, R%, k - 1
        '
        'with k = 27 becomes:
        '
        'j, R%, 26

        If dimText Is Nothing Then Return ""

        Dim parts() As String = dimText.Split(","c)
        Dim newParts As New List(Of String)

        For Each part As String In parts

            Dim oneDim As String = part.Trim

            If oneDim = "" Then
                Continue For
            End If

            Dim newDim As String = oneDim

            'Replace only standalone index tokens inside this one dimension.
            For Each kvp As KeyValuePair(Of String, String) In values
                newDim = ReplaceIndexToken(newDim, kvp.Key, kvp.Value)
            Next

            'mti 2026/05/13 start
            'If the replacement created a numeric expression, compute it.
            '
            'Examples:
            'k - 1, when k = 27, first becomes 27 - 1,
            'then becomes 26.
            '
            '2 * k, when k = 27, first becomes 2 * 27,
            'then becomes 54.
            '
            'If the dimension is still not fully numeric, keep it as text.
            'Examples:
            'R%
            'j
            'NCohorts
            'mti 2026/05/13 end

            Dim computedValue As Integer = 0

            If TryEvaluateIntegerExpression(newDim, computedValue) Then
                newDim = CStr(computedValue)
            End If

            newParts.Add(newDim)

        Next

        Return String.Join(", ", newParts.ToArray())

    End Function


    Private Function ReplaceIndexToken(
    ByVal textIn As String,
    ByVal indexToken As String,
    ByVal replacementValue As String
) As String

        'mti 2026/05/13 new procedure
        'Replaces a standalone index token.
        '
        'Examples:
        'k       -> 27
        'k%      -> 27
        '
        'But it does not replace k inside a longer variable name.

        If textIn Is Nothing Then Return textIn

        Dim cleanToken As String = CleanIndexToken(indexToken)

        If cleanToken = "" Then Return textIn

        Dim pattern As String =
        "(?<![A-Za-z0-9_])" & Regex.Escape(cleanToken) & "%?(?![A-Za-z0-9_])"

        Return Regex.Replace(textIn, pattern, replacementValue, RegexOptions.IgnoreCase)

    End Function


    Private Function ContainsIndexToken(
    ByVal textIn As String,
    ByVal indexToken As String
) As Boolean

        'mti 2026/05/13 new procedure
        'Checks whether a standalone index token appears in text.
        '
        'Example:
        'k is found in modmordstdet(j, R%, k)
        'k is not found inside a longer name.

        If textIn Is Nothing Then Return False

        Dim cleanToken As String = CleanIndexToken(indexToken)

        If cleanToken = "" Then Return False

        Dim pattern As String =
        "(?<![A-Za-z0-9_])" & Regex.Escape(cleanToken) & "%?(?![A-Za-z0-9_])"

        Return Regex.IsMatch(textIn, pattern, RegexOptions.IgnoreCase)

    End Function


    Private Function CleanIndexToken(ByVal indexToken As String) As String

        'mti 2026/05/13 new procedure
        'Cleans VB-style index names.
        '
        'Example:
        'k% -> k
        'R% -> R

        If indexToken Is Nothing Then Return ""

        Dim x As String = indexToken.Trim

        If x.EndsWith("%") Then
            x = x.Substring(0, x.Length - 1)
        End If

        Return x

    End Function


    Private Function GetRightExpressionBeforeIfCondition(ByVal rightText As String) As String

        'mti 2026/05/13 new procedure
        'Returns the main right-side expression before appended IF conditions.
        '
        'Example:
        '
        'modmordstdet(j,R%,k) * x, If k // 27 Then
        '
        'returns:
        '
        'modmordstdet(j,R%,k) * x

        If rightText Is Nothing Then Return ""

        Dim pos As Integer =
        rightText.IndexOf(", If ", StringComparison.OrdinalIgnoreCase)

        If pos < 0 Then
            Return rightText
        End If

        Return rightText.Substring(0, pos)

    End Function


    Private Function TryEvaluateIntegerExpression(
    ByVal expr As String,
    ByRef valueOut As Integer
) As Boolean

        'mti 2026/05/13 new procedure
        'Evaluates small numeric expressions safely.
        '
        'Allowed:
        '27
        '24 + 3
        '(24 + 3)
        '30 - 2
        '3 * 9
        '
        'Not allowed:
        'NCohorts
        '24 + G
        'R%
        '
        'This prevents expansion when the IF condition points to an
        'entire dimension size, usually variables beginning with N.

        valueOut = 0

        If expr Is Nothing Then Return False

        Dim x As String = expr.Trim

        If x = "" Then Return False

        'Only allow digits, spaces, and arithmetic operators.
        If Not Regex.IsMatch(x, "^[0-9\+\-\*\/\(\)\s]+$") Then
            Return False
        End If

        Try

            Dim dt As New DataTable
            Dim result As Object = dt.Compute(x, "")

            If result Is Nothing OrElse result Is DBNull.Value Then Return False

            Dim d As Double = Convert.ToDouble(result)

            If Math.Abs(d - Math.Round(d)) > 0.0000001 Then Return False

            valueOut = CInt(Math.Round(d))

            Return True

        Catch

            Return False

        End Try

    End Function


    Private Sub EnsureLeftRightCapacity(
    ByRef Leftside() As String,
    ByRef Rightside() As String,
    ByVal neededIndex As Integer
)

        'mti 2026/05/13 new procedure
        'Makes sure Leftside() and Rightside() are large enough.
        '
        'If your existing code already manages array size elsewhere,
        'you can remove this routine and the call to it.

        If neededIndex <= UBound(Leftside) AndAlso neededIndex <= UBound(Rightside) Then
            Exit Sub
        End If

        Dim newSize As Integer = Math.Max(UBound(Leftside), UBound(Rightside))

        Do While neededIndex > newSize
            newSize = newSize + 5000
        Loop

        ReDim Preserve Leftside(newSize)
        ReDim Preserve Rightside(newSize)

    End Sub

    'mti 2026/05/13 end

    'mti 2026/05/13 new procedures for for..loop expansion and related handling of If conditions within loops
    '---------- for loop helpers start

    Private Function TryReadForLine(
    ByVal codeLine As String,
    ByRef counterName As String,
    ByRef startText As String,
    ByRef endText As String,
    ByRef stepText As String
) As Boolean

        'mti 2026/05/13 new procedure

        counterName = ""
        startText = ""
        endText = ""
        stepText = ""

        Dim tmp As String = RemoveTrailingVBComment(codeLine)

        If tmp = "" Then Return False

        'Reads simple VB.NET For lines such as:
        'For G = 1 To 2
        'For j = 0 To NCohorts
        'For k = 10 To 1 Step -1
        Dim m As Match = Regex.Match(
        tmp,
        "^\s*For\s+([A-Za-z_][A-Za-z0-9_]*%?)\s*=\s*(.+?)\s+To\s+(.+?)(?:\s+Step\s+(.+?))?\s*$",
        RegexOptions.IgnoreCase
    )

        If Not m.Success Then Return False

        counterName = m.Groups(1).Value.Trim
        startText = m.Groups(2).Value.Trim
        endText = m.Groups(3).Value.Trim

        If m.Groups(4).Success Then
            stepText = m.Groups(4).Value.Trim
        Else
            stepText = ""
        End If

        Return True

    End Function

    Private Function TryReadNextLine(
    ByVal codeLine As String,
    ByRef counterName As String
) As Boolean

        'mti 2026/05/13 new procedure

        counterName = ""

        Dim tmp As String = RemoveTrailingVBComment(codeLine)

        If tmp = "" Then Return False

        'Reads:
        'Next
        'Next j
        'Next G
        Dim m As Match = Regex.Match(
        tmp,
        "^\s*Next(?:\s+([A-Za-z_][A-Za-z0-9_]*%?))?\s*$",
        RegexOptions.IgnoreCase
    )

        If Not m.Success Then Return False

        If m.Groups(1).Success Then
            counterName = m.Groups(1).Value.Trim
        End If

        Return True

    End Function


    Private Function IsNTotalLoop(
    ByVal startText As String,
    ByVal endText As String
) As Boolean
        'mti 2026/05/13 new procedure

        'Returns True for loops such as:
        'For j = 0 To NCohorts
        'For R = 1 To NR
        'For R = 1 To NR%
        '
        'These are treated as full-dimension index loops
        'and are not expanded into additional Leftside()/Rightside() rows.

        Dim startNum As Integer

        If Not Integer.TryParse(startText.Trim, startNum) Then Return False

        If startNum <> 0 AndAlso startNum <> 1 Then Return False

        Dim endVar As String = endText.Trim

        If endVar.EndsWith("%") Then
            endVar = endVar.Substring(0, endVar.Length - 1)
        End If

        If endVar.Length <= 1 Then Return False

        If Not endVar.StartsWith("N", StringComparison.OrdinalIgnoreCase) Then Return False

        If Not Regex.IsMatch(endVar, "^[A-Za-z_][A-Za-z0-9_]*$") Then Return False

        Return True

    End Function

    Private Function CleanCounterName(ByVal counterName As String) As String
        'mti 2026/05/13 new procedure
        Dim tmp As String = counterName.Trim

        If tmp.EndsWith("%") Then
            tmp = tmp.Substring(0, tmp.Length - 1)
        End If

        Return tmp

    End Function

    Private Function SameCounterName(
    ByVal counter1 As String,
    ByVal counter2 As String
) As Boolean
        'mti 2026/05/13 new procedure
        Return String.Equals(
        CleanCounterName(counter1),
        CleanCounterName(counter2),
        StringComparison.OrdinalIgnoreCase
    )

    End Function

    Private Function ReplaceCounterLiteral(
    ByVal codeText As String,
    ByVal counterName As String,
    ByVal counterValue As String
) As String
        'mti 2026/05/13 new procedure
        If codeText Is Nothing Then Return codeText

        Dim cleanCounter As String = CleanCounterName(counterName)

        If cleanCounter = "" Then Return codeText

        'Replace only the standalone counter.
        '
        'Example:
        'G is replaced in agedst(j, R, G)
        'G is not replaced inside GDPPCP or agedst.
        '
        'The %? also allows typed VB counters such as G%.
        Dim pattern As String =
        "(?<![A-Za-z0-9_])" & Regex.Escape(cleanCounter) & "%?(?![A-Za-z0-9_])"

        Dim outText As String = Regex.Replace(
        codeText,
        pattern,
        counterValue,
        RegexOptions.IgnoreCase
    )

        'Optional but useful:
        'turns 24 + 1 into 25, 24 + 2 into 26, etc.
        outText = SimplifySmallIntegerExpressions(outText)

        Return outText

    End Function

    Private Function SimplifySmallIntegerExpressions(ByVal codeText As String) As String
        'mti 2026/05/13 new procedure
        If codeText Is Nothing Then Return codeText

        Dim oldText As String = ""

        Do While oldText <> codeText

            oldText = codeText

            'Simplify number + number
            codeText = Regex.Replace(
            codeText,
            "(?<![A-Za-z0-9_])(-?\d+)\s*\+\s*(-?\d+)(?![A-Za-z0-9_])",
            Function(m As Match)
                Return CStr(CInt(m.Groups(1).Value) + CInt(m.Groups(2).Value))
            End Function
        )

            'Simplify number - number
            codeText = Regex.Replace(
            codeText,
            "(?<![A-Za-z0-9_])(-?\d+)\s*-\s*(-?\d+)(?![A-Za-z0-9_])",
            Function(m As Match)
                Return CStr(CInt(m.Groups(1).Value) - CInt(m.Groups(2).Value))
            End Function
        )

        Loop

        Return codeText

    End Function

    Private Sub ExpandForBlock(
    ByRef Leftside() As String,
    ByRef Rightside() As String,
    ByRef ModuleName() As String,
    ByRef CommentText() As String,
    ByRef CodeBlock() As String,
    ByRef ACnt As Integer,
    ByVal startAt As Integer,
    ByVal counterName As String,
    ByVal startVal As Integer,
    ByVal endVal As Integer,
    ByVal stepVal As Integer
)
        'mti 2026/05/13 new procedure
        'The For loop began when ACnt was startAt.
        'The loop body is therefore:
        'startAt + 1 To ACnt

        Dim blockStart As Integer = startAt + 1
        Dim blockEnd As Integer = ACnt

        If blockStart > blockEnd Then Exit Sub
        If stepVal = 0 Then Exit Sub

        If stepVal > 0 AndAlso startVal > endVal Then Exit Sub
        If stepVal < 0 AndAlso startVal < endVal Then Exit Sub

        Dim nLines As Integer = blockEnd - blockStart + 1

        Dim OldLeft(nLines - 1) As String
        Dim OldRight(nLines - 1) As String

        Dim OldModuleName(nLines - 1) As String
        Dim OldCommentText(nLines - 1) As String
        Dim OldCodeBlock(nLines - 1) As String

        Dim i As Integer

        'First save the original loop-body lines.
        'This is important because we will overwrite the original lines
        'with the first loop value.
        For i = 0 To nLines - 1
            OldLeft(i) = Leftside(blockStart + i)
            OldRight(i) = Rightside(blockStart + i)
            OldModuleName(i) = ModuleName(blockStart + i)
            OldCommentText(i) = CommentText(blockStart + i)
            OldCodeBlock(i) = CodeBlock(blockStart + i)
        Next i

        'Use the original rows as the first iteration.
        'Example:
        'For G = 1 To 2
        'The existing rows become G = 1.
        For i = 0 To nLines - 1
            Leftside(blockStart + i) =
            ReplaceCounterLiteral(OldLeft(i), counterName, CStr(startVal))

            Rightside(blockStart + i) =
            ReplaceCounterLiteral(OldRight(i), counterName, CStr(startVal))
        Next i

        'Append the later iterations.
        Dim v As Integer = startVal + stepVal

        Do While (stepVal > 0 AndAlso v <= endVal) OrElse
             (stepVal < 0 AndAlso v >= endVal)

            For i = 0 To nLines - 1

                ACnt = ACnt + 1

                'Make sure Leftside()/Rightside() are dimensioned large enough.
                'If not, add ReDim Preserve before this point.

                Leftside(ACnt) =
                ReplaceCounterLiteral(OldLeft(i), counterName, CStr(v))

                Rightside(ACnt) =
                ReplaceCounterLiteral(OldRight(i), counterName, CStr(v))

                ModuleName(ACnt) = OldModuleName(i)
                CommentText(ACnt) = OldCommentText(i)
                CodeBlock(ACnt) = OldCodeBlock(i)

            Next i

            v = v + stepVal

        Loop

    End Sub
    Private Function RemoveTrailingVBComment(ByVal codeLine As String) As String
        'mti 2026/05/13  new procedure
        If codeLine Is Nothing Then Return ""

        Dim pos As Integer = codeLine.IndexOf("'")

        If pos >= 0 Then
            Return codeLine.Substring(0, pos).Trim
        Else
            Return codeLine.Trim
        End If

    End Function
    '---------- for loop helpers end
    'mti 2026/05/13 

    Function GetSelectCaseExpression(line As String) As String
        'mti 2025/12/16 Victory Day - new procedure
        'Extracts expression after "Select Case",
        'removes trailing comments, and replaces "=" with "//"

        If String.IsNullOrWhiteSpace(line) Then Return ""

        Dim trimmed As String = line.TrimStart()

        If Not trimmed.StartsWith("Select Case ", StringComparison.OrdinalIgnoreCase) Then
            Return ""
        End If

        ' Extract text after "Select Case "
        Dim expr As String = trimmed.Substring(12)

        ' Remove trailing comment (outside quotes)
        expr = RemoveCommentOutsideQuotes(expr).Trim()

        ' Replace equals with //
        expr = expr.Replace("=", "//")

        Return expr
    End Function

    Public Function GetRegressionFunctionName(callLine As String,
                                       ACntNo As Integer,
                                       LeftSide() As String,
                                       RightSide() As String,
                                       Optional baseYearVal As Integer = Nothing,                 ' <-- pass your BaseYear here
                                       Optional constantDollarsYearVal As Integer = Nothing) As String ' <-- pass ConstantDollarsYear here
        'mti 2025/09/17 - update procedure

        'mti 2025/08/07 new procedure



        If String.IsNullOrWhiteSpace(callLine) Then Return String.Empty

        callLine = StripTrailingComments(callLine).Trim()

        ' Case 1: literal first argument in the call itself
        Dim literalFromCall = GetFirstArgLiteralFromCall(callLine)
        If literalFromCall.Length > 0 Then Return literalFromCall

        ' Case 2: first argument is an identifier (e.g., TbName$)
        Dim argName As String = ParseFirstArgIdentifier(callLine)
        If argName = String.Empty Then Return String.Empty

        ' Resolve identifier fully by scanning backward
        Dim value = ResolveIdentifierValue(argName, ACntNo, LeftSide, RightSide,
                                           New HashSet(Of String)(StringComparer.OrdinalIgnoreCase),
                                           baseYearVal, constantDollarsYearVal)
        Return value
    End Function


    Private Function ResolveIdentifierValue(identifier As String,
                                        startIndex As Integer,
                                        LeftSide() As String,
                                        RightSide() As String,
                                        resolving As HashSet(Of String),
                                        baseYearVal As Integer,
                                        constantDollarsYearVal As Integer) As String
        'mti 2025/09/17 proc changed

        'mti 2025/08/07 new procedure
        ' - Loops back up to 100 lines (or to start of current module via ModuleName())
        ' - Final RHS must be either a pure string or a getFunctionNameYrOrMstRct(...) call
        ' - If that call's first arg is a variable, we keep looping using that as the new identifier
        ' - Concats inside RHS are evaluated only for known scalars (BaseYear, ConstantDollarsYear)
        Dim i As Integer = startIndex
        Try

            ' In case  an empty identifier is passed 
            If String.IsNullOrWhiteSpace(identifier) Then Return String.Empty

            ' We’ll keep these to support the case where we hit getFunctionNameYrOrMstRct(...)
            Dim pendingHasFunc As Boolean = False
            Dim pendingBaseYearExpr As String = Nothing
            Dim pendingSuffixExpr As String = Nothing

            ' Module boundary controls
            Dim currentModule As String = ModuleName(startIndex) ' Nothing
            'If ModuleName IsNot Nothing AndAlso startIndex >= 0 AndAlso startIndex < ModuleName.Length Then
            '    currentModule = ModuleName(startIndex)
            'End If

            ' Resolve only known scalars via this local resolver (no recursion)
            Dim resolveKnown As Func(Of String, String) =
            Function(name As String) As String
                Dim k = KnownScalarToString(name, baseYearVal, constantDollarsYearVal)
                If k IsNot Nothing Then Return k
                Return String.Empty
            End Function

            ' Scan window
            Dim floor As Integer = Math.Max(0, startIndex - 1000)  'mti 2025/09/17 was 100
            'Dim i As Integer = startIndex
            Dim currentId As String = identifier

            ' CHANGED: regex to detect a single identifier RHS (incl. optional .ToString())
            Dim singleIdentRx As New Regex("^\s*([A-Za-z_]\w*\$?)(?:\s*\.\s*ToString\s*\(\s*\))?\s*$", RegexOptions.IgnoreCase)


            While i >= floor
                'If i <= 4082 Then Stop

                'mti 2025/09/17 start
                If LeftSide(i) Is Nothing OrElse RightSide(i) Is Nothing Then
                    i -= 1
                    Continue While
                End If
                'mti 2025/09/17 end

                ' Stop if we crossed into a different module
                If Not String.Equals(ModuleName(i), currentModule, StringComparison.OrdinalIgnoreCase) Then Exit While

                Dim lhs As String = StripTrailingComments(SafeTrim(LeftSide, i)).Trim()
                If NamesEqual(lhs, currentId) Then
                    Dim rhs As String = StripTrailingComments(SafeTrim(RightSide, i)).Trim()

                    'mti 2025/09/17 start
                    'strip trailing If statements, if collected any while parsing, by parsing code
                    Dim pos1 As Integer = rhs.Trim.ToUpper.IndexOf(", IF")
                    If pos1 > 0 Then rhs = rhs.Trim.Substring(0, pos1)

                    rhs = CleanString(rhs)  'remove trailing comments and space, if any -  added 'mti 2025/09/17 star

                    Dim strChk As String
                    'mti 2025/09/17 end

                    ' CASE A: pure quoted string -> we have a baseName now
                    If rhs.StartsWith("""") AndAlso rhs.EndsWith("""") Then
                        Dim baseName As String = Unquote(rhs)

                        'mti 2025/09/07 start
                        '"GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Life Expectancy (" & BaseYear & ")"


                        'strChk = """ & CStr(BaseYear) & """
                        'baseName = ReplaceVarWithVal(baseName, strChk, baseYearVal.ToString.Trim)
                        strChk = """" & " & CStr(BaseYear) & " & """"
                        baseName = baseName.Replace(strChk, baseYearVal.ToString().Trim())

                        'strChk = """ & BaseYear & """
                        'baseName = ReplaceVarWithVal(baseName, strChk, baseYearVal.ToString.Trim)
                        strChk = """" & " & BaseYear & " & """"
                        baseName = baseName.Replace(strChk, baseYearVal.ToString().Trim())


                        'strChk = """ & CStr(ConstantDollarsYear) & """
                        'baseName = ReplaceVarWithVal(baseName, strChk, constantDollarsYearVal.ToString.Trim)
                        strChk = """" & " & CStr(ConstantDollarsYear) & " & """"
                        baseName = baseName.Replace(strChk, constantDollarsYearVal.ToString().Trim())


                        'strChk = """ & ConstantDollarsYear & """
                        'baseName = ReplaceVarWithVal(baseName, strChk, constantDollarsYearVal.ToString.Trim)
                        strChk = """" & " & ConstantDollarsYear & " & """"
                        baseName = baseName.Replace(strChk, constantDollarsYearVal.ToString().Trim())


                        'mti 2025/09/07 end

                        If pendingHasFunc Then
                            'mti 2025/09/07 start
                            ' Build a call expression and let your evaluator do the rest (keeps "context" handling consistent)
                            'Dim expr As String = $"getFunctionNameYrOrMstRct({QuoteForVB(baseName)}, {If(String.IsNullOrWhiteSpace(pendingBaseYearExpr), "BaseYear", pendingBaseYearExpr)}, context, {If(String.IsNullOrWhiteSpace(pendingSuffixExpr), """""""", pendingSuffixExpr)})"
                            'Return EvaluateVBStringExpression(expr, resolveKnown, baseYearVal, constantDollarsYearVal)
                            Return getFunctionNameYrOrMstRct(baseName, baseYearVal, constantDollarsYearVal.ToString)
                            'mti 2025/09/07 end
                        Else
                            Return baseName
                        End If
                    End If

                    ' CASE B: try to detect getFunctionNameYrOrMstRct(...)
                    Dim m = Regex.Match(rhs, "getFunctionNameYrOrMstRct\s*\((.+)\)", RegexOptions.IgnoreCase)
                    If m.Success Then

                        'mti 2025/09/17 start
                        'rhs = ExtractFunctionCall(rhs)
                        'mti 2025/09/17 end

                        Dim args = SplitTopLevel(m.Groups(1).Value, ","c)


                        If args.Count >= 3 Then  'mti 2025/09/17 start   was If args.Count >= 3 Then
                            Dim baseArg As String = args(0).Trim()
                            pendingBaseYearExpr = args(1).Trim()  'this might not be needed

                            'mti 2025/09/17 start
                            'pendingSuffixExpr = args(3).Trim()
                            If args.Count >= 4 Then pendingSuffixExpr = args(3).Trim()  'this might not be needed
                            'mti 2025/09/17 end

                            pendingHasFunc = True

                            ' If baseArg is already a literal, we can finish right now
                            If baseArg.StartsWith("""") AndAlso baseArg.EndsWith("""") Then

                                'mti 2025/09/17 start
                                'Dim expr As String = $"getFunctionNameYrOrMstRct({baseArg}, {pendingBaseYearExpr}, context, {pendingSuffixExpr})"
                                'Dim expr As String
                                'If args.Count >= 4 Then
                                '    expr = $"getFunctionNameYrOrMstRct({baseArg}, {pendingBaseYearExpr}, context, {pendingSuffixExpr})"
                                'Else
                                '    expr = $"getFunctionNameYrOrMstRct({baseArg}, {pendingBaseYearExpr}, context)"
                                'End If


                                'Return EvaluateVBStringExpression(expr, resolveKnown, baseYearVal, constantDollarsYearVal)

                                'strChk = """ & CStr(BaseYear) & """
                                'baseArg = ReplaceVarWithVal(baseArg, strChk, baseYearVal.ToString.Trim)
                                strChk = """" & " & CStr(BaseYear) & " & """"
                                baseArg = baseArg.Replace(strChk, baseYearVal.ToString().Trim())

                                'strChk = """ & BaseYear & """
                                'baseArg = ReplaceVarWithVal(baseArg, strChk, baseYearVal.ToString.Trim)
                                strChk = """" & " & BaseYear & " & """"
                                baseArg = baseArg.Replace(strChk, baseYearVal.ToString().Trim())

                                'strChk = """ & CStr(ConstantDollarsYear) & """
                                'baseArg = ReplaceVarWithVal(baseArg, strChk, constantDollarsYearVal.ToString.Trim)
                                strChk = """" & " & CStr(ConstantDollarsYear) & " & """"
                                baseArg = baseArg.Replace(strChk, constantDollarsYearVal.ToString().Trim())

                                'strChk = """ & ConstantDollarsYear & """
                                'baseArg = ReplaceVarWithVal(baseArg, strChk, constantDollarsYearVal.ToString.Trim)
                                strChk = """" & " & ConstantDollarsYear & " & """"
                                baseArg = baseArg.Replace(strChk, constantDollarsYearVal.ToString().Trim())

                                Return getFunctionNameYrOrMstRct(baseArg, baseYearVal, constantDollarsYearVal.ToString)
                                'mti 2025/09/17 end

                            Else
                                ' Otherwise we must resolve that variable by continuing the loop
                                currentId = baseArg
                                i -= 1
                                Continue While
                            End If
                        End If

                        ' If malformed, fall through to treat RHS as a concat expression
                    End If

                    ' handle assignments like: Title$ = TFNameTotCarsPC
                    Dim mm = singleIdentRx.Match(rhs)
                    If mm.Success Then
                        currentId = mm.Groups(1).Value
                        i -= 1
                        Continue While
                    End If

                    ' CASE C: treat as a concatenation expression -> compute a string
                    ' (supports BaseYear / ConstantDollarsYear tokens only)
                    'this is missed automobile : Title$ = "CarsTot Versus Cars" + GRPS$(11, cc%)
                    'Thisis missed: func_name = "GDP/Capita (PPP " & ConstantDollarsYear.ToString & ") Versus Labor Coeff(lab/$mil)" (skip-> func_name = func_name.Substring(0, 90))
                    Dim computed As String = EvaluateVBStringExpression(rhs, resolveKnown, baseYearVal, constantDollarsYearVal)
                    If pendingHasFunc Then
                        Dim expr As String = $"getFunctionNameYrOrMstRct({QuoteForVB(computed)}, {pendingBaseYearExpr}, context, {pendingSuffixExpr})"
                        Return EvaluateVBStringExpression(expr, resolveKnown, baseYearVal, constantDollarsYearVal)
                    Else
                        Return computed
                    End If
                End If
                'End If

                i -= 1

            End While

            Return String.Empty

        Catch ex As Exception
            Console.WriteLine(ex.Message & ": Parsing Line " & i.ToString & ": Left: " & LeftSide(i) & ": Right: " & RightSide(i))
            Stop

            'restorepointer:
            'Me.Caption = sFormTitle$
            'Screen.MousePointer = 0

        End Try
    End Function
    Function ReplaceVarWithVal(strExpression As String, strVarName As String, strVarVal As String) As String
        'mti 2025/09/17 new procedure
        Return Regex.Replace(strExpression, strVarName, strVarVal, RegexOptions.IgnoreCase)
    End Function
    Function ExtractFunctionCall(input As String) As String
        'mti 2025/09/17 newprocedure
        Dim startIdx As Integer = input.IndexOf("("c)
        If startIdx = -1 Then Return String.Empty

        ' Find function name start
        Dim nameStart As Integer = input.LastIndexOfAny(" ,".ToCharArray(), startIdx)
        If nameStart = -1 Then nameStart = 0 Else nameStart += 1

        Dim depth As Integer = 0
        Dim endIdx As Integer = -1

        For i As Integer = startIdx To input.Length - 1
            If input(i) = "("c Then
                depth += 1
            ElseIf input(i) = ")"c Then
                depth -= 1
                If depth = 0 Then
                    endIdx = i
                    Exit For
                End If
            End If
        Next

        If endIdx = -1 Then Return String.Empty

        Return input.Substring(nameStart, endIdx - nameStart + 1).Trim()
    End Function

    ' Helper to quote a VB string literal (doubles internal quotes)
    Private Function QuoteForVB(s As String) As String
        'mti 2025/08/07 new procedure
        If s Is Nothing Then Return """"""""
        Return """" & s.Replace("""", """""") & """"
    End Function


    'Private Function ResolveIdentifierValue_old(identifier As String,
    '                                        startIndex As Integer,
    '                                        LeftSide() As String,
    '                                        RightSide() As String,
    '                                        resolving As HashSet(Of String),
    '                                        baseYearVal As Integer?,
    '                                        constantDollarsYearVal As Integer?) As String
    '    'mti 2025/09/17 delete
    '    'mti 2025/08/07 new procedure

    '    ' Resolve when function name is built through variables

    '    If resolving.Contains(identifier) Then Return String.Empty
    '    resolving.Add(identifier)

    '    Dim assignIdx = FindLastAssignmentIndex(identifier, startIndex, LeftSide)
    '    If assignIdx < 0 Then
    '        ' Not assigned in window; try vars
    '        Dim obj As Object = Nothing
    '        'If vars IsNot Nothing AndAlso vars.TryGetValue(identifier, obj) Then
    '        '    resolving.Remove(identifier)
    '        '    Return Convert.ToString(obj, CultureInfo.InvariantCulture)
    '        'End If
    '        resolving.Remove(identifier)
    '        Return String.Empty
    '    End If

    '    Dim rhs As String = StripTrailingComments(SafeTrim(RightSide, assignIdx)).Trim()

    '    If rhs.StartsWith("""") AndAlso rhs.EndsWith("""") Then
    '        resolving.Remove(identifier)
    '        Return Unquote(rhs)
    '    End If

    '    ' Resolver for any identifiers encountered during evaluation
    '    Dim resolveVar As Func(Of String, String) =
    '        Function(name As String) As String
    '            ' 1) explicit dictionary
    '            'Dim v As Object = Nothing
    '            'If vars IsNot Nothing AndAlso vars.TryGetValue(name, v) Then
    '            '    Return Convert.ToString(v, CultureInfo.InvariantCulture)
    '            'End If
    '            ' 2) known scalars (BaseYear / ConstantDollarsYear)
    '            Dim known = KnownScalarToString(name, baseYearVal, constantDollarsYearVal)
    '            If known IsNot Nothing Then Return known
    '            ' 3) recursively resolve from earlier code lines
    '            Return ResolveIdentifierValue(name, assignIdx - 1, LeftSide, RightSide, resolving,
    '                                          baseYearVal, constantDollarsYearVal)
    '            'Return ResolveIdentifierValue(name, assignIdx - 1, LeftSide, RightSide, vars, context, resolving,
    '            'baseYearVal, constantDollarsYearVal)
    '        End Function

    '    'Dim result = EvaluateVBStringExpression(rhs, vars, context, resolveVar, baseYearVal, constantDollarsYearVal)
    '    Dim result = EvaluateVBStringExpression(rhs, resolveVar, baseYearVal, constantDollarsYearVal)
    '    resolving.Remove(identifier)
    '    Return result
    'End Function

    Private Function FindLastAssignmentIndex(identifier As String,
                                             startIdx As Integer,
                                             LeftSide() As String) As Integer
        'mti 2025/08/07 new procedure
        Dim floor As Integer = Math.Max(0, startIdx - 100)
        For i As Integer = startIdx To floor Step -1
            Dim lhs As String = StripTrailingComments(SafeTrim(LeftSide, i)).Trim()
            If NamesEqual(lhs, identifier) Then Return i
        Next
        Return -1
    End Function



    Private Function EvaluateVBStringExpression(expr As String,
                                                resolveVar As Func(Of String, String),
                                                baseYearVal As Integer?,
                                                constantDollarsYearVal As Integer?) As String

        'vars As IDictionary(Of String, Object),
        '                                        context As Object,

        'mti 2025/08/07 new procedure

        ' EXPRESSION EVALUATION
        ' Evaluates:
        '  - "literal"
        '  - "a" & Var & Var.ToString() & "b"
        '  - getFunctionNameYrOrMstRct(BaseNameVar, BaseYearVar, context, "suffix" & Var ...)

        If String.IsNullOrWhiteSpace(expr) Then Return String.Empty

        Dim e = StripTrailingComments(expr).Trim()

        If e.StartsWith("""") AndAlso e.EndsWith("""") Then
            Return Unquote(e)
        End If

        ' getFunctionNameYrOrMstRct(...)
        Dim fnMatch = Regex.Match(e, "getFunctionNameYrOrMstRct\s*\((.+)\)", RegexOptions.IgnoreCase)
        If fnMatch.Success Then
            Dim args = SplitTopLevel(fnMatch.Groups(1).Value, ","c)
            If args.Count >= 4 Then
                'Dim baseName As String = ResolveTokenToString(args(0), vars, resolveVar, baseYearVal, constantDollarsYearVal)
                'Dim baseYear As Integer = CInt(Val(ResolveTokenToString(args(1), vars, resolveVar, baseYearVal, constantDollarsYearVal)))
                'Dim suffix As String = ResolveTokenToString(args(3), vars, resolveVar, baseYearVal, constantDollarsYearVal)
                'Return getFunctionNameYrOrMstRct(baseName, baseYear, context, suffix)
                Console.WriteLine(args(0))

                'mti 2025/09/07 start
                'Dim baseName As String = ResolveTokenToString(args(0), resolveVar, baseYearVal, constantDollarsYearVal)
                Dim baseName As String = ResolveBaseNameFromArg(args(0), baseYearVal, constantDollarsYearVal)
                Dim baseYear As Integer = CInt(Val(ResolveTokenToString(args(1), resolveVar, baseYearVal, constantDollarsYearVal)))
                Dim suffix As String = ResolveTokenToString(args(3), resolveVar, baseYearVal, constantDollarsYearVal)

                baseName = args(0).Replace("CStr(BaseYear)", baseYear.ToString()).Replace("BaseYear", baseYear.ToString())
                expr = expr.Replace("""""", """")
                baseName = baseName.Replace(""" & ", "")
                baseName = baseName.Replace(" & """, "")

                'mti 2025/09/07 end

                Return getFunctionNameYrOrMstRct(baseName, baseYear, constantDollarsYearVal.ToString)
            End If
        End If

        ' Concatenation with &
        Dim parts = SplitTopLevel(e, "&"c)
        Dim sb As New StringBuilder()
        For Each raw In parts
            Dim token = raw.Trim()
            sb.Append(ResolveTokenToString(token, resolveVar, baseYearVal, constantDollarsYearVal))
        Next
        Return sb.ToString()
    End Function
    Private Function ResolveBaseNameFromArg(arg As String,
                                        baseYearVal As Integer?,
                                        constantDollarsYearVal As Integer?) As String
        arg = If(arg, String.Empty).Trim()
        If arg.Length = 0 Then Return String.Empty

        Dim s As String = arg
        Dim sb As New System.Text.StringBuilder()
        Dim i As Integer = 0
        Dim n As Integer = s.Length

        ' local helper: find next non-whitespace char without advancing
        Dim nextNonWs As Func(Of Integer, Char?) =
        Function(idx As Integer) As Char?
            Dim k = idx
            While k < n AndAlso Char.IsWhiteSpace(s(k)) : k += 1 : End While
            If k < n Then Return s(k) Else Return Nothing
        End Function

        While i < n
            Dim ch As Char = s(i)

            ' ===== concat operator start: & or +
            If ch = "&"c OrElse ch = "+"c Then
                Dim delim As Char = ch
                Dim j As Integer = i + 1

                ' skip spaces after operator
                While j < n AndAlso Char.IsWhiteSpace(s(j)) : j += 1 : End While

                ' parse identifier
                Dim nameStart As Integer = j
                If j < n AndAlso (Char.IsLetter(s(j)) OrElse s(j) = "_"c) Then
                    j += 1
                    While j < n AndAlso (Char.IsLetterOrDigit(s(j)) OrElse s(j) = "_"c)
                        j += 1
                    End While
                    If j < n AndAlso s(j) = "$"c Then j += 1 ' optional $

                    ' optional .ToString()
                    Dim k As Integer = j
                    While k < n AndAlso Char.IsWhiteSpace(s(k)) : k += 1 : End While
                    If k < n AndAlso s(k) = "."c Then
                        Dim save = k : k += 1
                        While k < n AndAlso Char.IsWhiteSpace(s(k)) : k += 1 : End While
                        If k + 7 < n AndAlso String.Compare(s, k, "ToString", 0, 8, True, Globalization.CultureInfo.InvariantCulture) = 0 Then
                            k += 8
                            While k < n AndAlso Char.IsWhiteSpace(s(k)) : k += 1 : End While
                            If k < n AndAlso s(k) = "("c Then
                                k += 1
                                While k < n AndAlso Char.IsWhiteSpace(s(k)) : k += 1 : End While
                                If k < n AndAlso s(k) = ")"c Then
                                    k += 1
                                Else
                                    k = save
                                End If
                            Else
                                k = save
                            End If
                        Else
                            k = save
                        End If
                    End If

                    ' skip spaces before closing delimiter
                    While k < n AndAlso Char.IsWhiteSpace(s(k)) : k += 1 : End While

                    ' require matching closing & or +
                    If k < n AndAlso s(k) = delim Then
                        Dim name As String = s.Substring(nameStart, j - nameStart)

                        ' choose replacement
                        Dim replacement As String = name
                        If name.IndexOf("constant", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso constantDollarsYearVal.HasValue Then
                            replacement = constantDollarsYearVal.Value.ToString(Globalization.CultureInfo.InvariantCulture)
                        ElseIf name.IndexOf("base", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso baseYearVal.HasValue Then
                            replacement = baseYearVal.Value.ToString(Globalization.CultureInfo.InvariantCulture)
                        End If

                        ' append replacement
                        sb.Append(replacement)

                        ' advance past closing delimiter
                        i = k + 1

                        ' skip spaces AFTER the &var& block if next non-ws is a quote (syntactic only)
                        While i < n AndAlso Char.IsWhiteSpace(s(i))
                            Dim nn = nextNonWs(i)
                            If nn.HasValue AndAlso nn.Value = """"c Then
                                i += 1
                            Else
                                Exit While
                            End If
                        End While

                        Continue While
                    End If
                End If

                ' not a &var& pattern: just skip the operator (syntactic)
                i += 1
                Continue While
            End If

            ' ===== whitespace outside quotes:
            If Char.IsWhiteSpace(ch) Then
                ' if the next non-ws char is & or + or " (syntactic), skip this space
                Dim nn = nextNonWs(i + 1)
                'If nn.HasValue AndAlso (nn.Value = "&"c OrElse nn.Value = "+"c OrElse nn.Value = """"c) Then
                If nn.HasValue AndAlso (nn.Value = "&"c OrElse nn.Value = "+"c) Then '
                    i += 1
                    Continue While
                End If
                ' otherwise keep one space
                sb.Append(" "c)
                ' collapse any run of spaces to one
                While i + 1 < n AndAlso Char.IsWhiteSpace(s(i + 1)) : i += 1 : End While
                i += 1
                Continue While
            End If

            ' ===== drop all quote chars (including doubled quotes)
            If ch = """"c Then
                If i + 1 < n AndAlso s(i + 1) = """"c Then
                    i += 2 ' skip doubled ""
                Else
                    i += 1 ' skip single "
                End If
                Continue While
            End If

            ' ===== copy any other character
            sb.Append(ch)
            i += 1
        End While

        Return sb.ToString().Trim()
    End Function
    Private Function ResolveTokenToStringNew(token As String,
                                      resolveVar As Func(Of String, String),
                                      baseYearVal As Integer?,
                                      constantDollarsYearVal As Integer?) As String
        token = If(token, String.Empty).Trim()

        If token.Length = 0 Then Return String.Empty

        ' Handle top-level concatenation like: ... & ConstantDollarsYear & ...
        ' IMPORTANT: ignore parentheses here because they may be part of textual content.
        Dim parts = SplitTopLevelNew(token, "&"c, respectParens:=False)
        If parts.Count > 1 Then
            Dim sb As New System.Text.StringBuilder()
            For Each p In parts
                sb.Append(ResolveTokenToString(p.Trim(), resolveVar, baseYearVal, constantDollarsYearVal))
            Next
            Return sb.ToString()
        End If

        ' Entire token is a quoted literal
        If token.StartsWith("""") AndAlso token.EndsWith("""") Then
            Return Unquote(token)
        End If

        ' Parenthesized inner expression
        If token.StartsWith("("c) AndAlso token.EndsWith(")"c) Then
            Return ResolveTokenToString(token.Substring(1, token.Length - 2), resolveVar,
                                    baseYearVal, constantDollarsYearVal)
        End If

        ' Single identifier (optionally .ToString())
        Dim m = Regex.Match(token, "^\s*([A-Za-z_]\w*\$?)(?:\s*\.\s*ToString\s*\(\s*\))?\s*$")
        If m.Success Then
            Dim name = m.Groups(1).Value

            ' Known scalars: BaseYear / ConstantDollarsYear
            Dim known As String = KnownScalarToString(name, baseYearVal, constantDollarsYearVal)
            If Not String.IsNullOrEmpty(known) Then Return known

            ' Caller resolver (back-scan)
            If resolveVar IsNot Nothing Then
                Dim resolved As String = resolveVar(name)
                If resolved IsNot Nothing Then Return resolved
            End If

            ' Fallback to the identifier name as text
            Return name
        End If

        ' Mixed literal fragments (e.g., leading text + """" inside the same token)
        If token.IndexOf(""""c) >= 0 Then
            Return CollapseMixedLiteral(token)
        End If

        ' Fallback: treat as plain text
        Return token
    End Function
    Private Function ResolveTokenToStringLatest(token As String,
                                      resolveVar As Func(Of String, String),
                                      baseYearVal As Integer?,
                                      constantDollarsYearVal As Integer?) As String
        'mti 2025/08/07 new procedure
        token = If(token, String.Empty).Trim()
        If token.Length = 0 Then Return String.Empty

        ' Handle top-level concatenation like: "..." & ConstantDollarsYear & "..."
        Dim parts = SplitTopLevel(token, "&"c)
        If parts.Count > 1 Then
            Dim sb As New System.Text.StringBuilder()
            For Each p In parts
                sb.Append(ResolveTokenToString(p.Trim(), resolveVar, baseYearVal, constantDollarsYearVal))
            Next
            Return sb.ToString()
        End If

        ' Quoted literal
        If token.StartsWith("""") AndAlso token.EndsWith("""") Then
            Return Unquote(token)
        End If

        ' Parenthesized inner expression
        If token.StartsWith("("c) AndAlso token.EndsWith(")"c) Then
            Return ResolveTokenToString(token.Substring(1, token.Length - 2), resolveVar,
                                    baseYearVal, constantDollarsYearVal)
        End If

        ' Var or Var.ToString()
        Dim m = Regex.Match(token, "^\s*([A-Za-z_]\w*\$?)(?:\s*\.\s*ToString\s*\(\s*\))?\s*$")
        If m.Success Then
            Dim name = m.Groups(1).Value

            ' Known scalars: BaseYear / ConstantDollarsYear
            Dim known As String = KnownScalarToString(name, baseYearVal, constantDollarsYearVal)
            If known IsNot Nothing Then Return known

            ' Caller resolver (back-scan)
            If resolveVar IsNot Nothing Then
                Dim resolved = resolveVar(name)
                If resolved IsNot Nothing Then Return resolved
            End If

            ' Fallback to variable name
            Return name
        End If

        ' Fallback: return token as-is
        Return token
    End Function
    Private Function ResolveTokenToString(token As String,
                                          resolveVar As Func(Of String, String),
                                          baseYearVal As Integer?,
                                          constantDollarsYearVal As Integer?) As String
        'vars As IDictionary(Of String, Object),
        'mti 2025/08/07 new procedure
        If token.Length = 0 Then Return String.Empty

        ' Quoted literal
        If token.StartsWith("""") Then Return Unquote(token)

        ' Parenthesized inner expression
        If token.StartsWith("("c) AndAlso token.EndsWith(")"c) Then
            Return ResolveTokenToString(token.Substring(1, token.Length - 2), resolveVar,
                                        baseYearVal, constantDollarsYearVal)
        End If

        ' Var or Var.ToString()
        Dim m = Regex.Match(token, "^\s*([A-Za-z_]\w*\$?)(?:\s*\.\s*ToString\s*\(\s*\))?\s*$")
        If m.Success Then
            Dim name = m.Groups(1).Value

            ' 1) explicit dictionary
            Dim value As Object = Nothing
            'If vars IsNot Nothing AndAlso vars.TryGetValue(name, value) Then
            '    Return Convert.ToString(value, CultureInfo.InvariantCulture)
            'End If

            ' 2) known scalars: BaseYear / ConstantDollarsYear
            Dim known = KnownScalarToString(name, baseYearVal, constantDollarsYearVal)
            If known IsNot Nothing Then Return known

            ' 3) caller resolver (back-scan)
            If resolveVar IsNot Nothing Then
                Return resolveVar(name)
            End If

            Return name
        End If

        ' Fallback
        Return token
    End Function

    ' Convert known scalar identifiers to strings (no external config needed)
    Private Function KnownScalarToString(name As String,
                                         baseYearVal As Integer?,
                                         constantDollarsYearVal As Integer?) As String
        'mti 2025/08/07 new procedure
        If name.Equals("BaseYear", StringComparison.OrdinalIgnoreCase) AndAlso baseYearVal.HasValue Then
            Return baseYearVal.Value.ToString(CultureInfo.InvariantCulture)
        End If
        If name.Equals("ConstantDollarsYear", StringComparison.OrdinalIgnoreCase) AndAlso constantDollarsYearVal.HasValue Then
            Return constantDollarsYearVal.Value.ToString(CultureInfo.InvariantCulture)
        End If
        Return Nothing
    End Function


    Private Function GetFirstArgLiteralFromCall(callLine As String) As String
        'Dim m = Regex.Match(callLine, "Call\s+XYTABL\s*\((.*)\)", RegexOptions.IgnoreCase)
        Dim m = Regex.Match(callLine, "Call\s+(?:XYTABL|AnalFunc|AnalFuncSE)\s*\((.*)\)", RegexOptions.IgnoreCase)
        'mti 2025/08/07 new procedure
        'Call  line parsing
        If Not m.Success Then Return String.Empty

        Dim args = SplitTopLevel(m.Groups(1).Value, ","c)
        If args.Count = 0 Then Return String.Empty

        Dim arg1 = args(0).Trim()
        If arg1.StartsWith("""") AndAlso arg1.EndsWith("""") Then
            Return Unquote(arg1)
        End If
        Return String.Empty
    End Function

    Private Function ParseFirstArgIdentifier(s As String) As String
        'mti 2025/08/07 new procedure
        Dim m = Regex.Match(s, "Call\s+XYTABL\s*\(\s*([A-Za-z_]\w*\$?)\s*,", RegexOptions.IgnoreCase)
        If m.Success Then Return m.Groups(1).Value
        Return String.Empty
    End Function


    Private Function SafeTrim(arr As String(), idx As Integer) As String
        'mti 2025/08/07 new procedure
        If arr Is Nothing OrElse idx < 0 OrElse idx >= arr.Length Then Return String.Empty
        Return If(arr(idx), String.Empty).Trim()
    End Function

    Private Function NamesEqual(a As String, b As String) As Boolean
        'mti 2025/08/07 new procedure
        Return String.Equals(a.Replace(" ", ""), b.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function Unquote(s As String) As String
        'mti 2025/08/07 new procedure
        s = s.Trim()
        If s.StartsWith("""") AndAlso s.EndsWith("""") AndAlso s.Length >= 2 Then
            Return s.Substring(1, s.Length - 2).Replace("""""", """")
        End If
        Return s
    End Function
    Private Function SplitTopLevelNew(s As String, delim As Char, Optional respectParens As Boolean = True) As List(Of String)
        ' Split by a delimiter that is NOT inside quotes and (optionally) not inside parentheses

        Dim parts As New List(Of String)()
        Dim sb As New StringBuilder()
        Dim depth As Integer = 0
        Dim inQuotes As Boolean = False

        For i As Integer = 0 To s.Length - 1
            Dim c = s(i)

            If c = """"c Then
                If inQuotes AndAlso i + 1 < s.Length AndAlso s(i + 1) = """"c Then
                    ' doubled quote inside string literal
                    sb.Append("""""") : i += 1 : Continue For
                End If
                inQuotes = Not inQuotes : sb.Append(c)

            ElseIf Not inQuotes AndAlso respectParens AndAlso c = "("c Then
                depth += 1 : sb.Append(c)

            ElseIf Not inQuotes AndAlso respectParens AndAlso c = ")"c Then
                depth = Math.Max(0, depth - 1) : sb.Append(c)

            ElseIf Not inQuotes AndAlso (Not respectParens OrElse depth = 0) AndAlso c = delim Then
                parts.Add(sb.ToString()) : sb.Clear()

            Else
                sb.Append(c)
            End If
        Next

        parts.Add(sb.ToString())
        Return parts
    End Function
    Private Function CollapseMixedLiteral(s As String) As String
        Dim sb As New System.Text.StringBuilder()
        Dim inQuotes As Boolean = False
        Dim i As Integer = 0
        While i < s.Length
            Dim c = s(i)
            If c = """"c Then
                If inQuotes Then
                    If i + 1 < s.Length AndAlso s(i + 1) = """"c Then
                        ' doubled quote inside string -> literal "
                        sb.Append(""""c)
                        i += 2
                        Continue While
                    Else
                        inQuotes = False
                        i += 1
                        Continue While
                    End If
                Else
                    inQuotes = True
                    i += 1
                    Continue While
                End If
            Else
                sb.Append(c)
                i += 1
            End If
        End While
        Return sb.ToString()
    End Function


    Private Function SplitTopLevel(s As String, delim As Char) As List(Of String)
        'mti 2025/08/07 new procedure
        ' Split by a delimiter that is NOT inside quotes or parentheses

        Dim parts As New List(Of String)()
        Dim sb As New StringBuilder()
        Dim depth As Integer = 0
        Dim inQuotes As Boolean = False

        For i As Integer = 0 To s.Length - 1
            Dim c = s(i)

            If c = """"c Then
                If inQuotes AndAlso i + 1 < s.Length AndAlso s(i + 1) = """"c Then
                    sb.Append("""""") : i += 1 : Continue For
                End If
                inQuotes = Not inQuotes : sb.Append(c)

            ElseIf Not inQuotes AndAlso c = "("c Then
                depth += 1 : sb.Append(c)

            ElseIf Not inQuotes AndAlso c = ")"c Then
                depth = Math.Max(0, depth - 1) : sb.Append(c)

            ElseIf Not inQuotes AndAlso depth = 0 AndAlso c = delim Then
                parts.Add(sb.ToString()) : sb.Clear()

            Else
                sb.Append(c)
            End If
        Next

        parts.Add(sb.ToString())
        Return parts
    End Function


    Private Function StripTrailingComments(line As String) As String
        'mti 2025/08/07 new procedure
        ' Remove trailing comments outside quotes: "'" and REM (at stmt start or after whitespace/:)
        If String.IsNullOrEmpty(line) Then Return String.Empty

        Dim inQuotes As Boolean = False
        Dim i As Integer = 0
        While i < line.Length
            Dim c = line(i)

            If c = """"c Then
                If inQuotes Then
                    If i + 1 < line.Length AndAlso line(i + 1) = """"c Then
                        i += 2 : Continue While
                    Else
                        inQuotes = False
                    End If
                Else
                    inQuotes = True
                End If

            ElseIf Not inQuotes Then
                If c = "'"c Then
                    Return line.Substring(0, i).TrimEnd()
                End If
                If (c = "R"c OrElse c = "r"c) AndAlso i + 2 < line.Length Then
                    If String.Compare(line, i, "REM", 0, 3, True, CultureInfo.InvariantCulture) = 0 Then
                        Dim atStmtStart As Boolean =
                            i = 0 OrElse Char.IsWhiteSpace(line(i - 1)) OrElse line(i - 1) = ":"c
                        If atStmtStart Then
                            Return line.Substring(0, i).TrimEnd()
                        End If
                    End If
                End If
            End If

            i += 1
        End While

        Return line.TrimEnd()
    End Function
    ''================= delete from here mti Aug 13 2025
    'Public Function ResolveXYTABLTitle_old(callLine As String,
    '                                   ACntNo As Integer,
    '                                   LeftSide() As String,
    '                                   RightSide() As String,
    '                                   vars As IDictionary(Of String, Object),
    '                                   Optional context As Object = Nothing) As String
    '    'mti 2025/08/07 new procedure
    '    ' Main: resolve the title passed as the 1st arg to Call XYTABL(...)

    '    If String.IsNullOrWhiteSpace(callLine) Then Return String.Empty

    '    ' Case 1: literal first argument inside the call itself
    '    'Dim literal As String = ExtractFirstQuoted(callLine)  
    '    Dim literal As String = ExtractFunctionNameFromQuotes(callLine)
    '    If literal.Length > 0 Then Return literal

    '    ' Case 2: identifier as first argument (e.g., TbName$)
    '    Dim argName As String = ParseFirstArgIdentifier(callLine)
    '    If argName = String.Empty Then Return String.Empty

    '    ' Scan backward up to ~100 lines to find assignment to that identifier
    '    Dim startIdx As Integer = Math.Max(0, ACntNo - 100)
    '    For i As Integer = ACntNo To startIdx Step -1
    '        Dim lhs As String = SafeTrim(LeftSide, i)
    '        If NamesEqual(lhs, argName) Then
    '            Dim rhs As String = SafeTrim(RightSide, i)
    '            ' from the actual call's first arg; if empty, evaluate the RHS expression.
    '            Dim lit = ExtractFunctionNameFromQuotes(rhs)
    '            If lit.Length > 0 Then
    '                Return lit
    '            Else
    '                ' Handles: plain "literal", concatenations, getFunctionNameYrOrMstRct(...), etc.
    '                Return EvaluateVBStringExpression(rhs, vars, context)
    '            End If
    '        End If
    '    Next

    '    Return String.Empty
    'End Function


    'Private Function ExtractFirstQuoted_old(s As String) As String
    '    'mti 2025/08/07 new procedure
    '    Dim m = Regex.Match(s, """([^""]+)""")
    '    If m.Success Then Return m.Groups(1).Value
    '    Return String.Empty
    'End Function

    'Private Function ParseFirstArgIdentifier_old(s As String) As String
    '    'mti 2025/08/07 new procedure
    '    ' Matches: Call XYTABL(   Arg1   ,
    '    Dim m = Regex.Match(s, "Call\s+XYTABL\s*\(\s*([A-Za-z_]\w*\$?)\s*,", RegexOptions.IgnoreCase)
    '    If m.Success Then Return m.Groups(1).Value
    '    Return String.Empty
    'End Function

    'Private Function SafeTrim_old(arr As String(), idx As Integer) As String
    '    'mti 2025/08/07 new procedure
    '    If arr Is Nothing OrElse idx < 0 OrElse idx >= arr.Length Then Return String.Empty
    '    Return If(arr(idx), String.Empty).Trim()
    'End Function

    'Private Function NamesEqual_old(a As String, b As String) As Boolean
    '    'mti 2025/08/07 new procedure
    '    Return String.Equals(a.Replace(" ", ""), b.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)
    'End Function

    'Private Function Unquote_old(s As String) As String
    '    'mti 2025/08/07 new procedure
    '    s = s.Trim()
    '    If s.StartsWith("""") AndAlso s.EndsWith("""") AndAlso s.Length >= 2 Then
    '        Return s.Substring(1, s.Length - 2).Replace("""""", """") ' VB "" escape
    '    End If
    '    Return s
    'End Function

    '' Evaluates simple VB-like string expressions:
    ''   "literal" & Var & Var.ToString & "literal"
    ''   getFunctionNameYrOrMstRct(BaseNameVar, BaseYearVar, context, "suffix" & Var ...)
    'Private Function EvaluateVBStringExpression_old(expr As String,
    '                                            vars As IDictionary(Of String, Object),
    '                                            context As Object) As String
    '    'mti 2025/08/07 new procedure
    '    expr = expr.Trim()

    '    ' Detect direct call to getFunctionNameYrOrMstRct(...)
    '    Dim fnMatch = Regex.Match(expr, "getFunctionNameYrOrMstRct\s*\((.+)\)", RegexOptions.IgnoreCase)
    '    If fnMatch.Success Then
    '        Dim args = SplitTopLevel(fnMatch.Groups(1).Value, ","c)
    '        If args.Count >= 4 Then
    '            Dim baseName As String = ResolveTokenToString(args(0), vars)
    '            Dim baseYear As Integer = CInt(Val(ResolveTokenToString(args(1), vars)))
    '            ' args(2) is 'context' placeholder; we pass the provided context object
    '            Dim suffix As String = ResolveTokenToString(args(3), vars)
    '            ' Directly call your project function
    '            Return getFunctionNameYrOrMstRct(baseName, baseYear, context, suffix)
    '        End If
    '    End If

    '    ' Otherwise treat as concatenation with &
    '    Dim parts = SplitTopLevel(expr, "&"c)
    '    Dim sb As New Text.StringBuilder()
    '    For Each raw In parts
    '        Dim token = raw.Trim()
    '        sb.Append(ResolveTokenToString(token, vars))
    '    Next
    '    Return sb.ToString()
    'End Function

    'Private Function ResolveTokenToString_old(token As String,
    '                                      vars As IDictionary(Of String, Object)) As String
    '    'mti 2025/08/07 new procedure
    '    If token.Length = 0 Then Return String.Empty

    '    ' Quoted literal
    '    If token.StartsWith("""") Then Return Unquote(token)

    '    ' Parenthesized inner expression
    '    If token.StartsWith("("c) AndAlso token.EndsWith(")"c) Then
    '        Return ResolveTokenToString(token.Substring(1, token.Length - 2), vars)
    '    End If

    '    ' Var or Var.ToString()
    '    Dim m = Regex.Match(token, "^\s*([A-Za-z_]\w*\$?)(?:\s*\.\s*ToString\s*\(\s*\))?\s*$")
    '    If m.Success Then
    '        Dim name = m.Groups(1).Value
    '        Dim value As Object = Nothing
    '        If vars IsNot Nothing AndAlso vars.TryGetValue(name, value) Then
    '            Return Convert.ToString(value, CultureInfo.InvariantCulture)
    '        End If
    '        ' Unknown variable: return name or empty; choose empty if you prefer stricter behavior
    '        Return name
    '    End If

    '    ' Fallback
    '    Return token
    'End Function

    '' Split by delimiter not inside quotes or parentheses
    'Private Function SplitTopLevel_old(s As String, delim As Char) As List(Of String)
    '    'mti 2025/08/07 new procedure
    '    Dim parts As New List(Of String)()
    '    Dim sb As New Text.StringBuilder()
    '    Dim depth As Integer = 0
    '    Dim inQuotes As Boolean = False

    '    For i As Integer = 0 To s.Length - 1
    '        Dim c = s(i)

    '        If c = """"c Then
    '            inQuotes = Not inQuotes
    '            sb.Append(c)
    '        ElseIf Not inQuotes AndAlso c = "("c Then
    '            depth += 1 : sb.Append(c)
    '        ElseIf Not inQuotes AndAlso c = ")"c Then
    '            depth = Math.Max(0, depth - 1) : sb.Append(c)
    '        ElseIf Not inQuotes AndAlso depth = 0 AndAlso c = delim Then
    '            parts.Add(sb.ToString())
    '            sb.Clear()
    '        Else
    '            sb.Append(c)
    '        End If
    '    Next
    '    parts.Add(sb.ToString())
    '    Return parts
    'End Function
    'Function ExtractFunctionNameFromQuotes_old(callLine As String) As String
    '    'mti 2025/08/07 new procedure
    '    ' Match the full arg list inside parentheses after Call XYTABL
    '    Dim m = Regex.Match(callLine, "Call\s+XYTABL\s*\((.*)\)", RegexOptions.IgnoreCase)
    '    If Not m.Success Then Return String.Empty

    '    ' Split top-level args by comma (respecting quotes/parentheses)
    '    Dim args = SplitTopLevel(m.Groups(1).Value, ","c)
    '    If args.Count = 0 Then Return String.Empty

    '    Dim arg1 = args(0).Trim()
    '    If arg1.StartsWith("""") AndAlso arg1.EndsWith("""") Then
    '        Return Unquote(arg1) ' robust unquoting that handles doubled quotes
    '    End If

    '    Return String.Empty
    'End Function
    '================= delete till here mti Aug 13 2025

    'mti Documentation - whole proc
    ' loads the keywordlist at the start of the program
    Public Sub GetKeyWordList()
        'gvdb 2024/04/05
        ' unused variable
        'Dim ch As Long
        Dim txt As String
        Dim file As String
        Dim k As String
        Dim Part As String = ""
        Dim P As Integer, i As Integer
        ReDim vbKW(10)
        file = StartDirec & "\VBKeyW.txt"
        Using reader As StreamReader = New StreamReader(file)
            txt = reader.ReadToEnd
        End Using

        For P = 0 To txt.Length - 1
            k = txt.Substring(P, 1)
            Select Case k
                Case ","
                    If Part <> "" Then
                        vbKW(i) = Part.Trim.ToUpper  'mti 2025/08/07
                        If i = vbKWCount Then vbKWCount = vbKWCount + 10 : ReDim Preserve vbKW(vbKWCount)
                        i = i + 1
                        Part = ""
                    End If
                Case Is < " "
                Case Else
                    Part = Part & k
            End Select
        Next P
        vbKWCount = i
    End Sub


    Public Sub GetDimNamesExcluded()
        'mti 2025/10/30
        ' Reads IFsDimEx.grp from StartDirec and builds DimNamesExcluded() list
        Dim txt As String
        Dim file As String = StartDirec & "\IFsDimEx.txt"
        Dim part As String = ""
        Dim k As String
        Dim P As Integer, i As Integer

        ' Initialize array
        ReDim DimNamesExcluded(10)

        ' Check file existence
        If Not IO.File.Exists(file) Then
            MsgBox("Missing file: " & file)
            Exit Sub
        End If

        ' Read entire file
        Using reader As New IO.StreamReader(file)
            txt = reader.ReadToEnd()
        End Using

        ' Parse values separated by comma or newline
        For P = 0 To txt.Length - 1
            k = txt.Substring(P, 1)
            Select Case k
                Case ",", vbCr, vbLf
                    If part.Trim() <> "" Then
                        Dim val As String = part.Trim().ToUpper()
                        DimNamesExcluded(i) = val
                        If i = UBound(DimNamesExcluded) Then
                            ReDim Preserve DimNamesExcluded(i + 10)
                        End If
                        i += 1
                        part = ""
                    End If
                Case Else
                    part &= k
            End Select
        Next

        ' Handle last entry if no delimiter at end
        If part.Trim() <> "" Then
            Dim val As String = part.Trim().ToUpper()
            DimNamesExcluded(i) = val
            i += 1
        End If

        ' Final trim: remove trailing empty slots
        If i > 0 Then
            ReDim Preserve DimNamesExcluded(i - 1)
        Else
            ReDim DimNamesExcluded(0)
        End If
    End Sub

    Public Sub GetVarNamesExcluded()
        'mti 2025/12/08 new proc
        ' Reads IFsDimEx.grp from StartDirec and builds VarNamesExcluded() list
        Dim txt As String
        Dim file As String = StartDirec & "\IFsVarEx.txt"
        Dim part As String = ""
        Dim k As String
        Dim P As Integer, i As Integer

        ' Initialize array
        ReDim VarNamesExcluded(10)

        ' Check file existence
        If Not IO.File.Exists(file) Then
            MsgBox("Missing file: " & file)
            Exit Sub
        End If

        ' Read entire file
        Using reader As New IO.StreamReader(file)
            txt = reader.ReadToEnd()
        End Using

        ' Parse values separated by comma or newline
        For P = 0 To txt.Length - 1
            k = txt.Substring(P, 1)
            Select Case k
                Case ",", vbCr, vbLf
                    If part.Trim() <> "" Then
                        Dim val As String = part.Trim()  '.ToUpper() 'mti 2025/12/16 Victory Day - keep casing same to include parameters also in the exclusion list
                        VarNamesExcluded(i) = val
                        If i = UBound(VarNamesExcluded) Then
                            ReDim Preserve VarNamesExcluded(i + 10)
                        End If
                        i += 1
                        part = ""
                    End If
                Case Else
                    part &= k
            End Select
        Next

        ' Handle last entry if no delimiter at end
        If part.Trim() <> "" Then
            Dim val As String = part.Trim()  '.ToUpper() 'mti 2025/12/16 Victory Day - keep casing same to include parameters also in the exclusion list
            VarNamesExcluded(i) = val
            i += 1
        End If

        ' Final trim: remove trailing empty slots
        If i > 0 Then
            ReDim Preserve VarNamesExcluded(i - 1)
        Else
            ReDim VarNamesExcluded(0)
        End If
    End Sub

    Private Sub InitVarLink(db1Cmd As SqliteCommand)
        'mti 2025/10/30 - whole proc

        'mti Nov 2010 Driver Changes - whole proc
        'empty varlink table
        db1Cmd.CommandText = "DELETE FROM VarLink"
        db1Cmd.ExecuteNonQuery()

        'mti 2025/10/30 start
        'empty varlinkdim table
        db1Cmd.CommandText = "DELETE FROM VarLinkDim"
        db1Cmd.ExecuteNonQuery()

        'empty varlinkdimdetail table
        db1Cmd.CommandText = "DELETE FROM VarLinkDimDetail"
        db1Cmd.ExecuteNonQuery()
        'mti 2025/10/30 end

        'Code column in IFsVar 
        'delete code lines from ifsvar table
        db1Cmd.CommandText = "UPDATE IFsVar SET vbcode = "" """
        db1Cmd.ExecuteNonQuery()

        'mti 2025/10/30 start
        'delete code lines from ifsvarDim table if any
        db1Cmd.CommandText = "UPDATE IFsVarDim SET vbcode = "" """
        db1Cmd.ExecuteNonQuery()
        'mti 2025/10/30 end

    End Sub

    Private Function ExtractBaseName(s As String) As String
        'mti2025/08/07 New procedure 
        ' Get the base name before any "(" — e.g., "xyz(r$,y%)" -> "xyz"
        If String.IsNullOrEmpty(s) Then Return ""
        Dim p = s.IndexOf("("c)
        Dim name = If(p >= 0, s.Substring(0, p), s)
        Return name.Trim()
    End Function


    Private Function ContainsVarRef(rhs As String, candidate As String) As Boolean
        'mti2025/08/07 New procedure 
        ' Case-insensitive, token-aware match:
        ' - Matches base name only (no subdimensions)
        ' - Requires non-identifier boundary before the name
        ' - Requires "(", end, or non-identifier after the name

        If String.IsNullOrEmpty(rhs) OrElse String.IsNullOrEmpty(candidate) Then Return False

        Dim baseName = ExtractBaseName(candidate)
        If baseName = "" Then Return False

        ' Avoid short-name false positives by enforcing identifier boundaries
        ' Identifiers considered: letters, digits, underscore
        Dim pattern As String =
        $"(?i)(?<![A-Za-z0-9_]){Regex.Escape(baseName)}\s*(?=\(|$|[^A-Za-z0-9_])"

        Return Regex.IsMatch(rhs, pattern)
    End Function
    Function MatchIFSVarInString(strLine$, IFSVar$) As Boolean
        '-----------------------------------------------------------------
        'mti 2025/08/07 revise proc
        If IsNothing(strLine$) OrElse strLine$.Trim = "" Then Return False
        'mti Documentation - whole proc

        Dim pos1% = -1
        Dim endchar%, startchar%
        MatchIFSVarInString = False
        strLine = strLine.Trim.ToUpper
        'For ICount% = 1 To NumbVars
        pos1 = strLine.IndexOf(IFSVar)
        If pos1 >= 0 Then
            If pos1 >= 1 Then
                startchar = AscW(strLine.Substring(pos1 - 1, 1))
            Else
                startchar = 32
            End If
            'If strLine.Contains("DIM") Then Stop
            If startchar = 32 Then
                If strLine.Length + 1 = pos1 + 1 + IFSVar$.Trim.Length Then
                    endchar = 32
                ElseIf pos1 + 1 + IFSVar$.Trim.Length < strLine.Length Then 'jrs 2023/01/09 check to avoid blow up
                    endchar = AscW(strLine.Substring(pos1 + IFSVar.Length, 1))
                End If
                If endchar = 32 Or endchar = 40 Then  'space or (
                    'ifsvar found
                    MatchIFSVarInString = True
                    'Exit For
                End If
            End If
        End If
        'Next ICount%

    End Function
    Private Sub TakeSorted(CurLineNo%, ByRef LinesTaken%)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Sept 04, 2009 - Take whole procedure

        'Driver Code Changes June 2003 - whole proc

        'sorts the array CodeArray2Block() while populating it
        'array poulated as seek (and find) of an IFSVAR variable in the parsed VBcode goes on
        'array sorted so that VBcode is written in proper order

        Dim j2%, k2%, sLine%
        sLine = CurLineNo
        If LinesTaken >= 3000 - 1 Then Exit Sub
        LinesTaken = LinesTaken + 1
        If LinesTaken = 1 Then
            CodeArray2Block(1) = CurLineNo
            Exit Sub
        End If
        For j2 = 1 To LinesTaken - 1
            If CodeArray2Block(j2) > CurLineNo Then
                sLine = CodeArray2Block(j2)
                CodeArray2Block(j2) = CurLineNo
                Exit For
            End If
        Next j2
        For k2 = LinesTaken To j2 + 2 Step -1
            CodeArray2Block(k2) = CodeArray2Block(k2 - 1)
        Next k2

        If j2 + 1 <= LinesTaken Then
            CodeArray2Block(j2 + 1) = sLine
        End If

    End Sub


    'mti Documentation -whole proc
    Public Function IsKeyWord(ByVal txt As String) As Boolean
        Dim i As Integer
        For i = 0 To vbKWCount
            If txt = vbKW(i) Then IsKeyWord = True : Exit Function
        Next i
    End Function

    Function cVarnames(strExpression As String, Optional LeftVar As String = "") As List(Of String)
        'mti 2020/06/19 - take entire routine
        'mti 2017/10/19 - procedure changed - take whole procedure
        'mti Documentation - whole proc
        Dim thisVar As String
        Dim i As Integer
        Dim varStart As Integer = -1
        'gvdb 2024/04/05
        ' unused variable
        'Dim varWord As String
        'Dim res As String

        Dim ArrayIndx As Boolean
        'If InStr(strExpression, "DYADPOPFOREIGNFORCED") > 0 Then Stop

        'i = strExpression.IndexOf("GDPPCP(R%), AMAX(2, TFR(R%))")
        'If i >= 0 Then Stop

        'If strExpression = "Amin(100, AMAX(0, 100 - (sWaterHH(R%, Surface, National) + sWaterHH(R%, Unimproved, National))))" Then Stop
        'If strExpression = "Amin(100, AMAX(0, 100 - (WATERHH(R%, Surface, National) + WATERHH(R%, Unimproved, National))))" Then Stop
        'If strExpression.Contains("Amin(100, AMAX(0, 100 - (sWaterHH(R%, Surface, National) + sWaterHH(R%, Unimproved, National))))") Then Stop


        cVarnames = New List(Of String)

        ' extract all words from the text
        For i = 0 To strExpression.Length - 1
            'If i > 17 Then Stop
            Select Case AscW(strExpression.Substring(i, 1))
                Case 65 To 90, 97 To 122
                    ' variable always start with an alpha char
                    If varStart = -1 Then varStart = i
                Case 48 To 57, 95 '95="_"
                    ' digits can be suffix of a Varname
                Case Else
                    If varStart >= 0 Then
                        ' variable name will be extracted now,
                        'automatically exclude array index etc
                        thisVar = strExpression.Substring(varStart, i - varStart).ToUpper   'mti 2025/08/26, stops at "(" at 19,, ACII 40; and counts 13 characters from 6 : : CSng((AGEOFMARRIAGE(R%, Male) * MalePop + 

                        '                    If thisVar = "FAGEDST" And LeftVar = "POP" Then Stop
                        '                    If thisVar = "AGEDST" And LeftVar = "POP" Then Stop

                        'mti 2020/06/19 2020 Start
                        ArrayIndx = False
                        If thisVar = "I" And strExpression.Substring(i, 1) <> "(" Then ArrayIndx = True 'i is not investment, array index , skip
                        If thisVar = "C" And strExpression.Substring(i, 1) <> "(" Then ArrayIndx = True  'c is not consumption, array index , skip
                        If Not ArrayIndx Then
                            'mti 2020/06/19 2020 End
                            'check if vb keyword
                            If Not IsKeyWord(thisVar) Then
                                ' add to collection, ignore if already there

                                'mti Nov 2010 Driver Changes

                                'GDPR growth rate correction picks up POPR, take it off
                                If thisVar = "IGDPRCOR" Then thisVar = "IGDPR"  'IGDPRCOR should be counted as IGDPR (to avoid getting POPR)
                                'population related intermediate variables in IFsEduc should be converted to POP
                                'mti 2017/10/19 start
                                If (thisVar = "PRIPOP" Or thisVar = "SECLOWRPOP" Or thisVar = "SECUPPRPOP" _
                                Or thisVar = "SECPOP" Or thisVar = "TERPOP" _
                                     Or thisVar = "AGEDST" Or thisVar = "FAGEDST") And (LeftVar <> "POP") Then thisVar = "POP"
                                'mti 2017/10/19 End
                                'mti Nov 2010 Driver Changes

                                ' ignore duplicate words - or duplication of collection keys
                                Try
                                    If Not cVarnames.Contains(thisVar) Then
                                        cVarnames.Add(thisVar)
                                        'mti 2025/08/26 dimensioned names here by checkingforopen and close paren add here 
                                        'check if the character at which it stopped is 
                                    End If
                                Catch ex As Exception

                                End Try
                            End If
                            'mti 2020/06/19 2020 Start
                        End If
                        'mti 2020/06/19 2020 End
                        ' reset varstart
                        varStart = -1
                    End If
            End Select
        Next

        ' account for the last var, if any
        If varStart >= 0 Then
            ' extract the word
            thisVar = strExpression.Substring(varStart, i - varStart).ToUpper
            If Not IsKeyWord(thisVar) Then
                ' add to collection, ignore if already there
                Try
                    cVarnames.Add(thisVar)
                Catch ex As Exception

                End Try
            End If
        End If

    End Function

    Sub AddRow2VarLink(rstTemp As SqliteCommand,
    ByVal strVar As String, ByVal strUsesVar As String, Optional intDistance As Integer = 9999, Optional strComment As String = "")
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti 2025/08/07 Take procedure(adding comment text that holds function names etc)

        'mti 2020/06/19 take whole procedure including declarations

        'mti Nov 2010 Driver Changes - whole proc
        'mti Documentation February 2006 - whole proc
        'mti Documentation - whole proc
        Dim vbcode As String
        Dim strVarFound As Boolean
        Dim strUsesVarFound As Boolean

        'mti dec 18 '02 - take care to check duplication
        'If (UCase(strVar) = "SAGDEM" Or UCase(strVar) = "AGDEM") And (UCase(strUsesVar) = "CALMEATM" Or UCase(strUsesVar) = "SCALMEATM") Then Stop

        vbcode = ""
        'check for vcln>0
        If VCLn = UVCLn Then
            'Driver Code Changes June 2003
            vbcode = vbcode + CodeArray(VCLn) + vbCr
        Else
            For i2 = VCLn To UVCLn + 1 Step -1
                'Driver Code Changes June 2003
                vbcode = vbcode + CodeArray(i2) + vbCr
            Next i2
        End If

        ' Adds a new record to a Recordset using the data passed
        ' by the calling procedure. The new record is then made
        ' the current record.

        'If (UCase(strVar) = "SAGDEM" Or UCase(strVar) = "AGDEM") And (UCase(strUsesVar) = "CALMEATM" Or UCase(strUsesVar) = "SCALMEATM") Then Stop


        Dim SavedVarName = False

        If strVar <> strUsesVar Then

            For i2 = 1 To NumbVars
                'mti 23 jan, 2003
                If V$(i2).ToUpper = strVar.ToUpper Then
                    strVar = V$(i2)
                    strVarFound = True
                    Exit For
                End If
                If "S" + V$(i2).ToUpper = strVar.ToUpper Then
                    'mti Documentation February 2006
                    'trying to capture the S(aved) var under main var
                    'mti 2020/06/19 2020 start
                    strVarFound = True
                    For j2 = 1 To NumbVars
                        If V$(j2).ToUpper = strVar.ToUpper Then  '"S" + UCase$(V$(i2)) Then  'another variable exists with "S" prefix to this; e.g., SFDEM and FDEM
                            strVarFound = False
                            Exit For
                        End If
                    Next j2

                    'strVar = "S" + V$(i2)
                    If strVarFound Then
                        strVar = V$(i2)
                        SavedVarName = True
                        Exit For
                    End If
                    'mti 2020/06/19 2020 End
                    'mti Documentation February 2006 -end

                End If
            Next i2

            For i2 = 1 To NumbVars
                'mti 23 jan, 2003
                If V$(i2).ToUpper = strUsesVar.ToUpper Then
                    strUsesVar = V$(i2)
                    strUsesVarFound = True
                    Exit For
                End If
                If "S" + V$(i2).ToUpper = strUsesVar.ToUpper Then

                    'mti Documentation February 2006
                    'trying to capture the S(aved) var under main var
                    'mti 2020/06/19 2020 Start
                    strUsesVarFound = True
                    For j2 = 1 To NumbVars
                        If V$(j2).ToUpper = strUsesVar.ToUpper Then  '"S" + UCase$(V$(i2)) Then  'another variable exists with "S" prefix to this; e.g., SFDEM and FDEM
                            strUsesVarFound = False
                            Exit For
                        End If
                    Next j2

                    'strUsesVar = "S" + V$(i2)
                    If strUsesVarFound Then
                        strUsesVar = V$(i2)
                        SavedVarName = True
                        Exit For
                    End If
                    'mti Documentation February 2006 -end

                    '                strUsesVarFound = True
                    '                Exit For
                    'mti 2020/06/19 2020 End
                End If
            Next i2

            'mti Documentation February 2006
            If strVarFound And strUsesVarFound And (strVar <> strUsesVar) Then

                '            If UCase(strVar) = "AGDEM" And UCase(strUsesVar) = "CALMEATM" Then Stop

                Dim New_Driver = True

                'jrs 2023/01/10 using "" to insert " was giving a problem so I replaced it with """"
                vbcode = vbcode.Replace("""", """""")

                'mti 2025/08/07 start
                strComment = strComment.Replace("""", """""")
                'mti 2025/08/07 end

                'If strVar = "AGLOSSPROD" And strUsesVar = "GDPPCP" Then Stop



                Dim sql As String
                'mti 2025/09/17 start
                'sql = "UPDATE VarLinkNoDim SET Code = """ & vbcode & vbCr & """ + Code WHERE Variable = """ & strVar & """ AND UsesVariable = """ & strUsesVar & """"
                'sql = "UPDATE VarLinkNoDim SET Code = """ & vbcode & vbCr & """ + Code,  Comment = """ & strComment & """ + Comment WHERE Variable = """ & strVar & """ AND UsesVariable = """ & strUsesVar & """"
                'sqLite concatenation is ||, not +
                ''sql = "UPDATE VarLinkNoDim " &
                ''          "SET Code = """ & vbcode & vbCr & """ || Code, " &
                ''          "    Comment = CASE " &
                ''          "                 WHEN Comment = '' THEN """ & strComment & """ " &
                ''          "                 ELSE """ & strComment & "," & """ || Comment " &
                ''          "               End " &
                ''          "WHERE Variable = '" & strVar & "' " &
                ''          "  AND UsesVariable = '" & strUsesVar & "'"


                ''sql = "UPDATE VarLinkNoDim " &
                ''          "SET Code = '" & vbcode & vbCr & "' || Code, " &
                ''          "    Comment = CASE " &
                ''          "                 WHEN Comment = '' THEN '" & strComment & "' " &
                ''          "                 ELSE '" & strComment & ", ' || Comment " &
                ''          "               END " &
                ''          "WHERE Variable = '" & strVar & "' " &
                ''          "  AND UsesVariable = '" & strUsesVar & "'"

                Dim funcName As String = ExtractFuncName(strComment, "TABLFUNC,", "ANALFUNC,")


                'mti 2025/10/30 start
                sql = "UPDATE VarLink " &
                          "SET Distance = " & intDistance.ToString() & " " &
                          "WHERE Variable = '" & strVar & "' " &
                          "  AND UsesVariable = '" & strUsesVar & "'"

                rstTemp.CommandText = sql

                Dim nrowsupdated = rstTemp.ExecuteNonQuery()

                If nrowsupdated > 0 Then
                    New_Driver = False
                Else
                    sql = "INSERT INTO VarLink(Variable, UsesVariable, Distance) VALUES(""" & strVar & """, """ &
                        strUsesVar & """, " & intDistance.ToString() & ")"   'mti 2025/08/07 
                    rstTemp.CommandText = sql
                    rstTemp.ExecuteNonQuery()
                End If

                'mti 2025/10/30 end

                'mti 2025/10/30 start
                'the fllowing tables are not needed anymore

                'sql = "UPDATE VarLinkNoDim " &
                '          "Set Code = """ & vbcode & vbCr & """ || Code, " &
                '          "    Comment = Case " &
                '          "                 When """ & strComment & """ = '' THEN Comment " &
                '          "                 WHEN INSTR(Comment, """ & funcName & """) > 0 THEN Comment " &
                '          "                 WHEN Comment = '' THEN """ & strComment & """ " &
                '          "                 ELSE """ & strComment & "," & """ || Comment " &
                '          "               END " &
                '          "WHERE Variable = '" & strVar & "' " &
                '          "  AND UsesVariable = '" & strUsesVar & "'"

                ''sql = "UPDATE VarLinkNoDim " &
                ''          "SET Code = '" & vbcode & vbCr & "' || Code, " &
                ''          "    Comment = CASE " &
                ''          "                 WHEN '" & strComment & "' = '' THEN Comment " &
                ''          "                 WHEN Comment = '' THEN '" & strComment & "' " &
                ''          "                 ELSE '" & strComment & ", ' || Comment " &
                ''          "               END " &
                ''          "WHERE Variable = '" & strVar & "' " &
                ''          "  AND UsesVariable = '" & strUsesVar & "'"
                ''mti 2025/09/17 end

                'rstTemp.CommandText = sql

                'Dim nrowsupdated = rstTemp.ExecuteNonQuery()   'mti 2024/05/17 



                ''jrs 2023/07/26
                ''If rstTemp.UpdatedRowSource > 0 Then
                ''mti 2024/05/17 start
                ''If CBool(rstTemp.UpdatedRowSource) Then
                'If nrowsupdated > 0 Then
                '    'mti 2024/05/17 end
                '    New_Driver = False
                'Else
                '    sql = "INSERT INTO VarLinkNoDim(Variable, UsesVariable, Distance, Code, Comment) VALUES(""" & strVar & """, """ &
                '        strUsesVar & """, " & intDistance.ToString & " , """ & vbcode & """,  """ & strComment & """)"   'mti 2025/08/07 
                '    rstTemp.CommandText = sql
                '    rstTemp.ExecuteNonQuery()
                'End If


                ''mti 2025/08/28 start
                'sql = "UPDATE VarLinkOld SET Code = """ & vbcode & vbCr & """ + Code WHERE Variable = """ & strVar & """ AND UsesVariable = """ & strUsesVar & """"
                'rstTemp.CommandText = sql

                'nrowsupdated = rstTemp.ExecuteNonQuery()   'mti 2024/05/17 
                'If nrowsupdated > 0 Then
                '    New_Driver = False
                'Else
                '    sql = "INSERT INTO VarLinkOld(Variable, UsesVariable, Distance, Code) VALUES(""" & strVar & """, """ &
                '        strUsesVar & """, " & intDistance.ToString & " , """ & vbcode & """)"   'mti 2025/08/07 
                '    rstTemp.CommandText = sql
                '    rstTemp.ExecuteNonQuery()

                'End If
                'mti 2025/08/28 end

                'mti 2025/10/30 end


                UVCLn = VCLn
            End If
        End If

    End Sub

    ' Extract the function name that follows "TABLFUNC,"
    Private Function ExtractFuncName(strComment As String, strPref1 As String, strPref2 As String) As String
        'mti 2025/09/17 New
        Dim tag As String, i As Integer
        If String.IsNullOrWhiteSpace(strComment) Then Return String.Empty

        tag = strPref1 '"TABLFUNC,"
        i = strComment.IndexOf(tag, StringComparison.OrdinalIgnoreCase)
        If i >= 0 Then Return strComment.Substring(i + tag.Length).Trim()

        tag = strPref2 '"ANALFUNC,"
        i = strComment.IndexOf(tag, StringComparison.OrdinalIgnoreCase)
        If i >= 0 Then Return strComment.Substring(i + tag.Length).Trim()

        Return strComment.Trim()
    End Function
    Function UsesVariableExists(db1Cmd As SqliteCommand, variableName As String) As Boolean
        'mti 2025/12/16 Victory Day Revise Procedure for VarLinkDim
        'mti 2025/08/07 New
        'Checks if a variable is already recorded in VarLink.UsesVariable (case-insensitive)

        db1Cmd.CommandText =
        "SELECT 1 FROM VarLinkDim WHERE UsesVariable = @varName COLLATE NOCASE LIMIT 1"

        db1Cmd.Parameters.Clear()
        db1Cmd.Parameters.Add("@varName", SqliteType.Text).Value = variableName

        Return db1Cmd.ExecuteScalar() IsNot Nothing
    End Function
    Sub EnsureVarLinkDimIndexes(db1Cmd As SqliteCommand)
        'mti 2025/12/16 Victory Day - new procedure
        'Creates index needed for fast case-insensitive lookup on UsesVariable.

        Dim oldSql As String = db1Cmd.CommandText

        Try
            db1Cmd.Parameters.Clear()
            db1Cmd.CommandText =
            "CREATE INDEX IF NOT EXISTS idx_VarLinkDim_UsesVariable " &
            "ON VarLinkDim (UsesVariable COLLATE NOCASE);"
            db1Cmd.ExecuteNonQuery()

        Finally
            'Restore prior command text (important if caller reuses db1Cmd)
            db1Cmd.CommandText = oldSql
            db1Cmd.Parameters.Clear()
        End Try
    End Sub

    Function VariableExists(db1Cmd As SqliteCommand, variableName As String) As Boolean
        'mti 2025/08/07 New
        ' This function checks if a variable is already recorded in the UsesVariable column (case-insensitive match)

        db1Cmd.CommandText = "SELECT 1 FROM VarLink WHERE LOWER(Variable) = LOWER(@varName) LIMIT 1"
        db1Cmd.Parameters.Clear()
        db1Cmd.Parameters.AddWithValue("@varName", variableName)

        Using reader As SqliteDataReader = db1Cmd.ExecuteReader()
            Dim exists As Boolean = reader.Read()
            reader.Close() ' Optional, but explicit close is good practice if reusing the command
            Return exists
        End Using
    End Function

    Private Sub InitVarLinkWithDim(db1Cmd As SqliteCommand)
        'mti 2025/08/07 New proc
        'recreate varlink table
        db1Cmd.CommandText = "DELETE FROM VarLinkDim"
        db1Cmd.ExecuteNonQuery()

        'mti 2025/08/28 Added
        db1Cmd.CommandText = "DELETE FROM VarLinkNoDim"
        db1Cmd.ExecuteNonQuery()

        'mti 2025/08/28 Added
        db1Cmd.CommandText = "DELETE FROM VarLinkOld"
        db1Cmd.ExecuteNonQuery()

    End Sub

    Sub AddRow2VarLinkWithDim(rstTemp As SqliteCommand,
                          ByVal strVar As String,
                          ByVal strVarDim1 As String, ByVal strVarDim2 As String, ByVal strVarDim3 As String,
                          ByVal strUsesVar As String,
                          ByVal strUsesVarDim1 As String, ByVal strUsesVarDim2 As String, ByVal strUsesVarDim3 As String,
                          Optional intDistance As Integer = 9999, Optional strComment As String = "")
        'mti 2025/12/08 - many changes; take entire proedure
        'mti 2025/08/07 New
        '-----------------------------------------------------------------------------------
        ' Add a VarLinkDim row (or update Code if exact row already exists)
        ' VarLinkDim schema (columns):
        '   Variable, VariableDim1, VariableDim2, VariableDim3,
        '   UsesVariable, UsesVariableDim1, UsesVariableDim2, UsesVariableDim3,
        '   Distance (SMALLINT), Code (LONG TEXT)
        ' Exact-duplicate definition here = all fields except Code match.
        '-----------------------------------------------------------------------------------

        'call pattern
        '    AddRow2VarLinkWithDim(db1Cmd, LeftVar, LDim1, LDim2, LDim3, RightVar, RDim1, RDim2, RDim3, Distance%)
        '-----------------------------------------------------------------------------------

        Dim vbcode As String
        Dim strVarFound As Boolean
        Dim strUsesVarFound As Boolean
        Dim i2 As Integer, j2 As Integer

        Dim RowUpdated As Boolean   'mti 2025/09/17 -  added this declaration


        'Dim VarNo As Integer = 0, UsesVarNo As Integer = 0  'mti 2025/08/28 


        Try

            'If strVar <> strUsesVar Then Stop

            'If strVar = "CARS" Then Stop
            Dim VarNo As Integer = 0, UsesVarNo As Integer = 0  'mti 2025/08/28 

            ' Normalize Nothing -> empty strings for dims
            If strVarDim1 Is Nothing Then strVarDim1 = "" : If strVarDim2 Is Nothing Then strVarDim2 = "" : If strVarDim3 Is Nothing Then strVarDim3 = ""
            If strUsesVarDim1 Is Nothing Then strUsesVarDim1 = "" : If strUsesVarDim2 Is Nothing Then strUsesVarDim2 = "" : If strUsesVarDim3 Is Nothing Then strUsesVarDim3 = ""

            vbcode = ""
            ' Build code block exactly as in AddRow2VarLink
            If VCLn = UVCLn Then
                vbcode = vbcode + CodeArray(VCLn) + vbCr
            Else
                For i2 = VCLn To UVCLn + 1 Step -1
                    vbcode = vbcode + CodeArray(i2) + vbCr
                Next i2
            End If

            'If strVar <> strUsesVar Then  'mti 2025/08/27 a

            'mti 2025/09/17 start
            vbcode = "" 'Nothing   'temporary  measure to  save  space

            ' Escape quotes in vbcode (match your existing approach)
            vbcode = vbcode.Replace("""", """""")


            strComment = strComment.Replace("""", """""")
            'mti 2025/09/17 end

            'mti 2025/08/27 start

            ' find strVar against V$() (including Saved "S" prefix handling)
            Dim SavedVarName As Boolean = False


            For i2 = 1 To NumbVars
                If V$(i2).ToUpper = strVar.ToUpper Then
                    strVar = V$(i2) : strVarFound = True : VarNo = i2 : Exit For
                ElseIf ("S" + V$(i2)).ToUpper = strVar.ToUpper Then
                    strVarFound = True
                    For j2 = 1 To NumbVars
                        If V$(j2).ToUpper = strVar.ToUpper Then
                            strVarFound = False : Exit For
                        End If
                    Next j2
                    If strVarFound Then
                        strVar = V$(i2) : VarNo = i2 : SavedVarName = True : Exit For  'mti 2025/08/27 
                    End If
                End If
            Next

            For i2 = 1 To NumbVars

                If V$(i2).ToUpper = strUsesVar.ToUpper Then
                    strUsesVar = V$(i2) : strUsesVarFound = True : UsesVarNo = i2 : Exit For
                ElseIf ("S" + V$(i2)).ToUpper = strUsesVar.ToUpper Then
                    strUsesVarFound = True
                    For j2 = 1 To NumbVars
                        If V$(j2).ToUpper = strUsesVar.ToUpper Then
                            strUsesVarFound = False : Exit For
                        End If
                    Next j2
                    If strUsesVarFound Then
                        strUsesVar = V$(i2) : UsesVarNo = i2 : SavedVarName = True : Exit For   'mti 2025/08/27 
                    End If
                End If

            Next i2

            'mti 2025/08/27 end

            'mti 2025/10/30 start
            'mti 2025/08/28 start
            'take out age
            'If strVarDim1.ToUpper.Contains("AGE") Then strVarDim1 = ""
            'If strVarDim2.ToUpper.Contains("AGE") Then strVarDim2 = ""
            'If strVarDim3.ToUpper.Contains("AGE") Then strVarDim3 = ""
            'If strUsesVarDim1.ToUpper.Contains("AGE") Then strUsesVarDim1 = ""
            'If strUsesVarDim2.ToUpper.Contains("AGE") Then strUsesVarDim2 = ""
            'If strUsesVarDim3.ToUpper.Contains("AGE") Then strUsesVarDim3 = ""

            'mti 2025/08/28 end
            If IsAllLowercase(V$(VarNo)) Then 'variable is a parameter
                strVarDim1 = "" : strVarDim2 = "" : strVarDim3 = ""
            End If

            If IsAllLowercase(V$(UsesVarNo)) Then 'Uses variable is a parameter
                strUsesVarDim1 = "" : strUsesVarDim2 = "" : strUsesVarDim3 = ""
            End If

            Dim ex As HashSet(Of String)

            If DimNamesExcluded IsNot Nothing Then
                ex = New HashSet(Of String)(DimNamesExcluded, StringComparer.OrdinalIgnoreCase)
            Else
                ex = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            End If

            strVarDim1 = FilterDim(strVarDim1, ex)
            strVarDim2 = FilterDim(strVarDim2, ex)
            strVarDim3 = FilterDim(strVarDim3, ex)



            strUsesVarDim1 = FilterDim(strUsesVarDim1, ex)
            strUsesVarDim2 = FilterDim(strUsesVarDim2, ex)
            strUsesVarDim3 = FilterDim(strUsesVarDim3, ex)

            'mti 2025/10/30 end

            If strVarFound And strUsesVarFound And Not (strVar = strUsesVar And strVarDim1 = strUsesVarDim1 And strVarDim2 = strUsesVarDim2 And strVarDim3 = strUsesVarDim3) Then 'mti 2025/08/27 a

                Dim NewRow As Boolean = True

                '' Escape quotes in vbcode (match your existing approach)
                'vbcode = vbcode.Replace("""", """""")

                'strComment = strComment.Replace("""", """""")

                'mti 2025/09/17 start
                ' Try UPDATE first: if exact row exists, prepend new code to existing Code
                Dim sql As String '=
                '"UPDATE VarLinkDim SET Code = """ & vbcode & vbCr & """ + Code " &
                '"WHERE Variable = """ & strVar & """ " &
                '"AND VariableDim1 = """ & strVarDim1 & """ AND VariableDim2 = """ & strVarDim2 & """ AND VariableDim3 = """ & strVarDim3 & """ " &
                '"AND UsesVariable = """ & strUsesVar & """ " &
                '"AND UsesVariableDim1 = """ & strUsesVarDim1 & """ AND UsesVariableDim2 = """ & strUsesVarDim2 & """ AND UsesVariableDim3 = """ & strUsesVarDim3 & """ "



                Dim funcName As String = ExtractFuncName(strComment, "TABLFUNC,", "ANALFUNC,")


                'mti 2025/10/30 start  - changed VarLinkDim to VarLinkDimDetail
                sql = "UPDATE VarLinkDimDetail " &
                          "SET Code = """ & vbcode & vbCr & """ || Code, " &
                          "    Comment = CASE " &
                          "                 WHEN """ & strComment & """ = '' THEN Comment " &
                          "                 WHEN INSTR(Comment, """ & funcName & """) > 0 THEN Comment " &
                          "                 WHEN Comment = '' THEN """ & strComment & """ " &
                          "                 ELSE """ & strComment & "," & """ || Comment " &
                          "               END " &
                          "WHERE Variable = """ & strVar & """ " &
                          "AND VariableDim1 = """ & strVarDim1 & """ AND VariableDim2 = """ & strVarDim2 & """ AND VariableDim3 = """ & strVarDim3 & """ " &
                          "AND UsesVariable = """ & strUsesVar & """ " &
                          "AND UsesVariableDim1 = """ & strUsesVarDim1 & """ AND UsesVariableDim2 = """ & strUsesVarDim2 & """ AND UsesVariableDim3 = """ & strUsesVarDim3 & """ "


                'mti 2025/09/17 end

                'Console.WriteLine(sql)

                rstTemp.CommandText = sql


                Dim nrowsupdated As Integer = rstTemp.ExecuteNonQuery()

                If nrowsupdated > 0 Then
                    NewRow = False
                Else
                    ' INSERT new exact row with all 10 fields
                    sql =
                        "INSERT INTO VarLinkDimDetail(" &
                        "Variable, VariableDim1, VariableDim2, VariableDim3, " &
                        "UsesVariable, UsesVariableDim1, UsesVariableDim2, UsesVariableDim3, " &
                        "Distance, Code, Comment) " &
                        "VALUES(" &
                        """" & strVar & """, " &
                        """" & strVarDim1 & """, " &
                        """" & strVarDim2 & """, " &
                        """" & strVarDim3 & """, " &
                        """" & strUsesVar & """, " &
                        """" & strUsesVarDim1 & """, " &
                        """" & strUsesVarDim2 & """, " &
                        """" & strUsesVarDim3 & """, " &
                        intDistance.ToString & ", " &
                        """" & vbcode & """, " &
                        """" & strComment & """)"
                    rstTemp.CommandText = sql
                    rstTemp.ExecuteNonQuery()
                End If
                'mti 2025/10/30 end


                'mti 2025/10/30 start
                ''mti 2025/08/28 start - enter a generalrow for dimensioned linkages - so variables seleceted without dimension shows as before
                'If strVarFound And strUsesVarFound And (strVar <> strUsesVar) Then
                '    'RowUpdated = WriteVarLinkDim(rstTemp, strVar, strUsesVar, intDistance, vbcode, strComment)
                '    RowUpdated = WriteVarLinkDim(rstTemp, strVar, strUsesVar, intDistance)
                'End If
                ''mti 2025/08/28 end updating concatenated dim in VarLink
                'mti 2025/10/30 end


                'Dim VarWithDim As New List(Of String)()
                'Dim UsesVarWithDim As New List(Of String)()
                Dim TmpVar As String, TmpUsesVar As String


                'mti 2025/08/28 start updating concatenated dim in VarLink
                TmpVar = strVar
                TmpUsesVar = strUsesVar


                'If Not IsAllLowercase(V$(VarNo)) Then  'mti 2025/12/08 parameters excluded from dimensionality
                Select Case VDim1(VarNo)
                    Case 0    'no  dimensions 
                    Case 1 'country/region

                        If strVarDim2 <> "" And strVarDim3 <> "" Then  'mti 2025/12/08 added if..end if
                            If strVarDim2 <> "" Then TmpVar &= If(TmpVar.Contains("("), ", ", "(") & strVarDim2
                            If strVarDim3 <> "" Then TmpVar &= If(TmpVar.Contains("("), ", ", "(") & strVarDim3
                            If TmpVar <> strVar Then TmpVar = TmpVar + ")"

                        End If  'mti 2025/12/08 added if..end if


                    Case Else
                        If strVarDim1 <> "" And strVarDim2 <> "" And strVarDim3 <> "" Then  'mti 2025/12/08 added if..end if
                            If strVarDim1 <> "" Then TmpVar = TmpVar + "(" + strVarDim1
                            If strVarDim2 <> "" Then TmpVar &= If(TmpVar.Contains("("), ", ", "(") & strVarDim2
                            If strVarDim3 <> "" Then TmpVar &= If(TmpVar.Contains("("), ", ", "(") & strVarDim3
                            If TmpVar <> strVar Then TmpVar = TmpVar + ")"
                        End If  'mti 2025/12/08 added if..end if

                End Select
                'End If 'mti 2025/12/08 parameters excluded from dimensionality

                'If Not IsAllLowercase(V$(UsesVarNo)) Then  'mti 2025/12/08 parameters excluded from dimensionality
                Select Case VDim1(UsesVarNo)
                    Case 0    ' no  dimensions 
                    Case 1 'country/region

                        If strUsesVarDim2 <> "" And strUsesVarDim3 <> "" Then  'mti 2025/12/08 added if..end if
                            If strUsesVarDim2 <> "" Then TmpUsesVar &= If(TmpUsesVar.Contains("("), ", ", "(") & strUsesVarDim2
                            If strUsesVarDim3 <> "" Then TmpUsesVar &= If(TmpUsesVar.Contains("("), ", ", "(") & strUsesVarDim3
                            If TmpUsesVar <> strUsesVar Then TmpUsesVar = TmpUsesVar + ")"
                        End If  'mti 2025/12/08 added if..end if


                    Case Else

                        If strUsesVarDim1 <> "" And strUsesVarDim2 <> "" And strUsesVarDim3 <> "" Then  'mti 2025/12/08 added if..end if
                            If strUsesVarDim1 <> "" Then TmpUsesVar = TmpUsesVar + "(" + strUsesVarDim1
                            If strUsesVarDim2 <> "" Then TmpUsesVar &= If(TmpUsesVar.Contains("("), ", ", "(") & strUsesVarDim2
                            If strUsesVarDim3 <> "" Then TmpUsesVar &= If(TmpUsesVar.Contains("("), ", ", "(") & strUsesVarDim3

                            If TmpUsesVar <> strUsesVar Then TmpUsesVar = TmpUsesVar + ")"
                        End If  'mti 2025/12/08 added if..end if
                End Select
                'End If  'mti 2025/12/08 parameters excluded from dimensionality

                strVar = TmpVar
                strUsesVar = TmpUsesVar

                'Temporary measure for AGEOFMARRIAGE — will revisit later. This appears to be an If...Then...Else branching issue  with Male or Female as condition  while constructing LeftSide And RightSide.
                'mti 2025/12/08 start - deleted a space
                If strVar.Trim.ToUpper = "AGEOFMARRIAGE" And strUsesVar.Trim.ToUpper = "EDYRSAG15(MALE)" Then strUsesVar = "EDYRSAG15"
                'mti 2025/12/08 end deleted a space

                If strVarFound And strUsesVarFound And (strVar <> strUsesVar) Then 'mti 2025/12/08: no new code; deleted comments only
                    RowUpdated = WriteVarLinkDim(rstTemp, strVar, strUsesVar, intDistance)  ', vbcode, strComment
                End If


                ''mti 2025/09/17 start 'add varlink for non-dim variable (might not need the one above then)
                Dim AddVar As New List(Of String)()
                Dim AddUsesVar As New List(Of String)()


                If Not IsAllLowercase(V$(VarNo)) Then  'mti 2025/12/08  parameters excluded from dimensional handling (just setting strUsesVar ="" won't ork; as this handles "" dims to build list)
                    'mti 2025/10/30 start  'exclude Parameters And Not IsAllLowercase(V$(VarNo))
                    If Not strVar.Contains("(") And VDim1(VarNo) > 0 Then
                        'If Not strVar.Contains("(") And VDim1(VarNo) > 0 Then  'isParameter 
                        'mti 2025/10/30 end
                        'extra rows when no dimension info present for a dimensioned variable 

                        'FilterDim(strVarDim1, ex)

                        'mti 2025/10/30 start
                        'Variable -  additional rows if any
                        If VDim1(VarNo) > 1 Then  'non-region first dim exists
                            'mti 2025/12/08 start
                            If strVarDim1 = "" Then
                                'mti 2025/12/08 end
                                For i% = 1 To 188
                                    strVarDim1 = GRPS(VDim1(VarNo), i)
                                    If FilterDim(strVarDim1.Trim, ex) = "" Then Exit For
                                    AddVar.Add(strVar + "(" + strVarDim1)
                                Next

                                'mti 2025/12/08 start
                            Else
                                AddVar.Add(strVar + "(" + strVarDim1)
                            End If
                            'mti 2025/12/08 end
                        End If
                        Dim FirstDimAdded As Integer = AddVar.Count
                        Dim DimPrefix1st(FirstDimAdded) As String
                        If VDim2(VarNo) > 1 Then  'second dim exists and not region (canbe region for ALLY)
                            'mti 2025/12/08 start
                            If strVarDim2 = "" Then
                                'mti 2025/12/08 end
                                For i% = 1 To 186

                                    strVarDim2 = GRPS(VDim2(VarNo), i)
                                    If FilterDim(strVarDim2.Trim, ex) = "" Then Exit For
                                    If FirstDimAdded = 0 Then
                                        AddVar.Add(strVar + "(" + strVarDim2)
                                    Else
                                        If i% > 1 Then
                                            For j = 1 To FirstDimAdded
                                                AddVar.Add(DimPrefix1st(j - 1) & ", " & strVarDim2)
                                            Next
                                        Else
                                            For j = 1 To FirstDimAdded
                                                DimPrefix1st(j - 1) = AddVar.Item(j - 1)
                                                AddVar.Item(j - 1) = AddVar.Item(j - 1) & ", " & strVarDim2
                                            Next
                                        End If
                                    End If
                                Next
                                'mti 2025/12/08 start
                            Else
                                If FirstDimAdded = 0 Then
                                    AddVar.Add(strVar + "(" + strVarDim2)
                                Else
                                    For j = 1 To FirstDimAdded
                                        AddVar.Item(j - 1) = AddVar.Item(j - 1) & ", " & strVarDim2
                                    Next
                                End If
                            End If
                            'mti 2025/12/08 end
                        End If
                        Dim SecondDimAdded As Integer = AddVar.Count
                        Dim DimPrefix2nd(SecondDimAdded) As String 'mti 2025/12/08
                        If VDim3(VarNo) > 1 Then  'third dim exists and not region (can be region for ALLY)
                            'mti 2025/12/08 start
                            If strVarDim3 = "" Then
                                'mti 2025/12/08 end
                                For i% = 1 To 186
                                    strVarDim3 = GRPS(VDim3(VarNo), i)
                                    If FilterDim(strVarDim3.Trim, ex) = "" Then Exit For
                                    If SecondDimAdded = 0 Then
                                        AddVar.Add(strVar + "(" + strVarDim3)
                                    Else
                                        If i% > 1 Then
                                            For j = 1 To SecondDimAdded
                                                'AddVar.Add(strVar + "(" + strVarDim3)
                                                'AddVar.Item(SecondDimAdded * (i - 1) + j - 1) = DimPrefixSec(j - 1) & ", " & strVarDim3
                                                AddVar.Add(DimPrefix2nd(j - 1) & ", " & strVarDim3)
                                            Next
                                        Else
                                            For j = 1 To SecondDimAdded
                                                DimPrefix2nd(j - 1) = AddVar.Item(j - 1)
                                                AddVar.Item(j - 1) = AddVar.Item(j - 1) & ", " & strVarDim3
                                            Next
                                        End If
                                        'For j = 1 To SecondDimAdded
                                        '    'AddVar.Item(j - 1) = AddVar.Item(j - 1) & ", " & strVarDim3
                                        '    AddVar.Item(SecondDimAdded * (i - 1) + j - 1) = DimPrefixSec(j - 1) & ", " & strVarDim3
                                        '    'mt 2025/12/09 changecounter
                                        'Next
                                    End If
                                Next
                                'mti 2025/12/08 start
                            Else
                                If SecondDimAdded = 0 Then
                                    AddVar.Add(strVar + "(" + strVarDim3)
                                Else
                                    For j = 1 To SecondDimAdded
                                        AddVar.Item(j - 1) = AddVar.Item(j - 1) & ", " & strVarDim3
                                    Next
                                End If
                            End If
                            'mti 2025/12/08 end
                        End If

                    End If 'Not strVar.Contains("(") And VDim1(VarNo) > 0 Then
                End If  'mti 2025/12/08  parameters excluded from dimensional handling

                If Not IsAllLowercase(V$(UsesVarNo)) Then  'mti 2025/12/08  parameters excluded from dimensional handling (just setting strUsesVar ="" won't ork; as this handles "" dims to build list)
                    'mti 2025/10/30 start  'exclude Parameters And Not IsAllLowercase(V$(VarNo))
                    If Not strUsesVar.Contains("(") And VDim1(UsesVarNo) > 0 Then
                        'mti 2025/10/30 end 
                        'extra rows when no dimension info present for a dimensioned variable 

                        'UsesVariable -  additional rows if any
                        If VDim1(UsesVarNo) > 1 Then  'non-region first dim exists
                            'mti 2025/12/08 start
                            If strUsesVarDim1 = "" Then
                                'mti 2025/12/08 end
                                For i% = 1 To 186
                                    strUsesVarDim1 = GRPS(VDim1(UsesVarNo), i)
                                    If FilterDim(strUsesVarDim1.Trim, ex) = "" Then Exit For
                                    AddUsesVar.Add(strUsesVar + "(" + strUsesVarDim1)
                                Next
                                'mti 2025/12/08 start
                            Else
                                AddUsesVar.Add(strUsesVar + "(" + strUsesVarDim1)
                            End If
                            'mti 2025/12/08 end

                        End If
                        Dim UsesFirstDimAdded As Integer = AddUsesVar.Count
                        'mti 2025/12/08 start
                        Dim UsesDimPrefix1st(UsesFirstDimAdded) As String
                        If VDim2(UsesVarNo) > 1 Then  'second dim exists and not region (canbe region for ALLY)

                            If strUsesVarDim2 = "" Then
                                'mti 2025/12/08 end
                                For i% = 1 To 186

                                    strUsesVarDim2 = GRPS(VDim2(UsesVarNo), i)
                                    If FilterDim(strUsesVarDim2.Trim, ex) = "" Then Exit For

                                    'If strUsesVarDim2.Trim = "" Then Exit For

                                    'If strUsesVarDim2.Trim.ToUpper = "TOTAL" Then Exit For
                                    'If strUsesVarDim2.Trim.ToUpper = "ALL" Then Exit For

                                    If UsesFirstDimAdded = 0 Then
                                        AddUsesVar.Add(strUsesVar + "(" + strUsesVarDim2)
                                    Else
                                        'For j = 1 To UsesFirstDimAdded
                                        '    AddUsesVar.Item(j - 1) = AddUsesVar.Item(j - 1) & ", " & strUsesVarDim2
                                        'Next
                                        If i% > 1 Then
                                            For j = 1 To UsesFirstDimAdded
                                                AddUsesVar.Add(UsesDimPrefix1st(j - 1) & ", " & strUsesVarDim2)
                                            Next
                                        Else
                                            For j = 1 To UsesFirstDimAdded
                                                UsesDimPrefix1st(j - 1) = AddUsesVar.Item(j - 1)
                                                AddUsesVar.Item(j - 1) = AddUsesVar.Item(j - 1) & ", " & strUsesVarDim2
                                            Next
                                        End If
                                    End If

                                Next
                                'mti 2025/12/08 start
                            Else
                                If UsesFirstDimAdded = 0 Then
                                    AddUsesVar.Add(strUsesVar + "(" + strUsesVarDim2)
                                Else
                                    For j = 1 To UsesFirstDimAdded
                                        AddUsesVar.Item(j - 1) = AddUsesVar.Item(j - 1) & ", " & strUsesVarDim2
                                    Next
                                End If
                            End If
                            'mti 2025/12/08 end
                        End If
                        Dim UsesSecondDimAdded As Integer = AddUsesVar.Count
                        Dim UsesDimPrefix2nd(UsesSecondDimAdded) As String
                        If VDim3(UsesVarNo) > 1 Then  'third dim exists and not region (canbe region for ALLY)
                            'mti 2025/12/08 start
                            If strUsesVarDim3 = "" Then
                                'mti 2025/12/08 end

                                For i% = 1 To 186
                                    strUsesVarDim3 = GRPS(VDim3(UsesVarNo), i)
                                    If FilterDim(strUsesVarDim3.Trim, ex) = "" Then Exit For
                                    'If strUsesVarDim3.Trim = "" Then Exit For
                                    'If strUsesVarDim3.Trim.ToUpper = "TOTAL" Then Exit For
                                    'If strUsesVarDim3.Trim.ToUpper = "ALL" Then Exit For
                                    If UsesSecondDimAdded = 0 Then
                                        AddUsesVar.Add(strUsesVar + "(" + strUsesVarDim3)
                                    Else
                                        If i% > 1 Then
                                            For j = 1 To UsesSecondDimAdded
                                                'AddVar.Add(strVar + "(" + strVarDim3)
                                                'AddVar.Item(SecondDimAdded * (i - 1) + j - 1) = DimPrefixSec(j - 1) & ", " & strVarDim3
                                                AddUsesVar.Add(UsesDimPrefix2nd(j - 1) & ", " & strUsesVarDim3)
                                            Next
                                        Else
                                            For j = 1 To UsesSecondDimAdded
                                                UsesDimPrefix2nd(j - 1) = AddUsesVar.Item(j - 1)
                                                AddUsesVar.Item(j - 1) = AddUsesVar.Item(j - 1) & ", " & strUsesVarDim3
                                            Next
                                        End If
                                    End If
                                Next
                                'mti 2025/12/08 start
                            Else
                                If UsesSecondDimAdded = 0 Then
                                    AddUsesVar.Add(strUsesVar + "(" + strUsesVarDim3)
                                Else
                                    For j = 1 To UsesSecondDimAdded
                                        AddUsesVar.Item(j - 1) = AddUsesVar.Item(j - 1) & ", " & strUsesVarDim3
                                    Next
                                End If
                            End If
                            'mti 2025/12/08 end
                        End If
                        'mti 2025/10/30 end
                    End If  'Not strUsesVar.Contains("(") And VDim1(UsesVarNo) > 0 Then
                End If 'mti 2025/12/08  parameters excluded from dimensional handling


                'mti 2025/12/08 start
                'mti 2025/10/30 start
                'Dim VarDimText As String = ExtractInParens(TmpVar)
                'Dim UsesVarDimText As String = ExtractInParens(TmpUsesVar) 'mti 2025/12/10 - ExtractBaseName

                Dim VarDimText As String = ExtractBaseName(TmpVar)
                Dim UsesVarDimText As String = ExtractBaseName(TmpUsesVar) 'mti 2025/12/10 - ExtractBaseName
                'mti 2025/12/08 end

                If AddVar.Count > 0 Then
                    'for Variable: delete the non-dimensioned addition, if done, if added; when concatenated dimensioned will be added
                    rstTemp.CommandText = "DELETE FROM VarLinkDim WHERE Variable = @var AND UsesVariable = @use"
                    rstTemp.Parameters.Clear()
                    rstTemp.Parameters.AddWithValue("@var", VarDimText)
                    rstTemp.Parameters.AddWithValue("@use", TmpUsesVar)

                    rstTemp.ExecuteNonQuery()
                End If
                If AddUsesVar.Count > 0 Then
                    'for Uses Variable: delete the non-dimensioned addition, if done, if added; when concatenated dimensioned will be added
                    rstTemp.CommandText = "DELETE FROM VarLinkDim WHERE Variable = @var AND UsesVariable = @use"
                    rstTemp.Parameters.Clear()
                    rstTemp.Parameters.AddWithValue("@var", TmpVar)
                    rstTemp.Parameters.AddWithValue("@use", UsesVarDimText)
                    rstTemp.ExecuteNonQuery()
                End If


                If AddVar.Count = 0 Then  'adding uses var rows, if any 'mti 2025/10/30 add comment
                    TmpVar = strVar
                    For j = 0 To AddUsesVar.Count - 1
                        TmpUsesVar = AddUsesVar.Item(j) & ")"
                        If strVarFound And strUsesVarFound And (TmpVar <> TmpUsesVar) Then
                            'mti 2025/10/30 start
                            'RowUpdated = WriteVarLinkDim(rstTemp, TmpVar, TmpUsesVar, intDistance, vbcode, strComment)
                            RowUpdated = WriteVarLinkDim(rstTemp, TmpVar, TmpUsesVar, intDistance)
                            'mti 2025/10/30 end
                        End If
                    Next
                End If

                VarDimText = ""
                UsesVarDimText = ""
                'mti 2025/10/30 end
                For i = 0 To AddVar.Count - 1
                    TmpVar = AddVar.Item(i) & ")"
                    If AddUsesVar.Count = 0 Then
                        TmpUsesVar = strUsesVar
                        If strVarFound And strUsesVarFound And (TmpVar <> TmpUsesVar) Then
                            'mti 2025/10/30 start
                            RowUpdated = WriteVarLinkDim(rstTemp, TmpVar, TmpUsesVar, intDistance)  ', vbcode, strComment
                            'mti 2025/10/30 end
                        End If
                    End If

                    'look for matching dim
                    VarDimText = ExtractInParens(TmpVar)

                    For j = 0 To AddUsesVar.Count - 1
                        TmpUsesVar = AddUsesVar.Item(j) & ")"
                        UsesVarDimText = ExtractInParens(TmpUsesVar)
                        If VarDimText = UsesVarDimText Then Exit For
                    Next



                    If VarDimText = UsesVarDimText Then
                        If strVarFound And strUsesVarFound And (TmpVar <> TmpUsesVar) Then
                            'mti 2025/10/30 start
                            RowUpdated = WriteVarLinkDim(rstTemp, TmpVar, TmpUsesVar, intDistance)  ', vbcode, strComment
                            'mti 2025/10/30 end
                        End If
                    Else
                        For j = 0 To AddUsesVar.Count - 1
                            TmpUsesVar = AddUsesVar.Item(j) & ")"
                            If strVarFound And strUsesVarFound And (TmpVar <> TmpUsesVar) Then
                                'mti 2025/10/30 start
                                RowUpdated = WriteVarLinkDim(rstTemp, TmpVar, TmpUsesVar, intDistance)  ', vbcode, strComment
                                'mti 2025/10/30 end
                            End If
                        Next
                    End If
                Next



                'mti 2025/09/17 end

                ' Keep driver line pointers consistent with original routine
                UVCLn = VCLn

            End If    'strVarFound And strUsesVarFound And Not (strVar = strUsesVar
            'End If 'mti 2025/08/27 a
        Catch ex As Exception
            'Catch
            Console.WriteLine(strVar & ", Dim1 " & strVarDim1 & ", Dim2 " & strVarDim2 & ", Dim3 " & strVarDim3 & ", driven by: " & strUsesVar & ", Dim1 " & strUsesVarDim1 & ", Dim2 " & strUsesVarDim2 & ", Dim3 " & strUsesVarDim3)

            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)

            Stop
        End Try
    End Sub

    Private Function FilterDim(val As String,
                           ex As HashSet(Of String)) As String
        'mti 2025/10/30 new procedure
        If String.IsNullOrEmpty(val) Then Return val
        If ex.Contains(val) Then Return ""
        Return val
    End Function

    Private Function WriteVarLinkDim(cmd As SqliteCommand,
                               variable As String, usesVariable As String,
                               distance As Integer) As Boolean  ', vbcode As String, commentText As String
        'mti 2025/10/30  whole proc
        'mti 2025/09/17 new procedure
        ' Returns True if UPDATE happened, False if INSERT happened.

        'mti 2025/12/08  start - variable not driving same; even of dims are different
        If ExtractBaseName(variable).Trim.ToUpper = ExtractBaseName(usesVariable).Trim.ToUpper Then Return False
        'mti 2025/12/08  end

        ' UPDATE first (prepend code)
        cmd.Parameters.Clear()  'mti commented

        'cmd.CommandText =
        '"UPDATE VarLink " &
        '"SET Code = @code || char(13) || Code " &
        '"WHERE Variable = @v AND UsesVariable = @uv"
        'mti 2025/10/30 start
        'Dim funcName As String = ExtractFuncName(commentText, "TABLFUNC,", "ANALFUNC,")
        'funcName = funcName.Replace("""", """""")

        '"SET Distance = " & intDistance.ToString() & " " &
        cmd.CommandText =
                        "UPDATE VarLinkDim " &
                        "SET Distance = " & distance.ToString() & " " &
                        "WHERE Variable = @v AND UsesVariable = @uv"
        'cmd.CommandText =
        '                "UPDATE VarLinkDim " &
        '                "SET Code = @code || char(13) || Code, " &
        '                  "    Comment = CASE " &
        '                  "                 WHEN @comment  = '' THEN Comment " &
        '                  "                 WHEN INSTR(@comment, @fname) > 0 THEN Comment " &
        '                  "                 WHEN Comment = '' THEN @comment " &
        '                  "                 ELSE @comment || Char(44)  || Comment " &
        '                  "               END " &
        '                "WHERE Variable = @v AND UsesVariable = @uv"



        'cmd.Parameters.AddWithValue("@code", vbcode)
        cmd.Parameters.AddWithValue("@v", variable)
        cmd.Parameters.AddWithValue("@uv", usesVariable)
        'cmd.Parameters.AddWithValue("@comment", commentText)
        'cmd.Parameters.AddWithValue("@fname", funcName)
        'mti 2025/10/30 end

        Dim n As Integer = cmd.ExecuteNonQuery()
        If n > 0 Then Return True

        ' ---- Before INSERT: require both Variable and UsesVariable to exist in IFsVarDim ----
        '   (here we check in IFsVarDim; change tableName if you want IFsVar instead)
        If Not (CheckIfVarExists(cmd, "IFsVarDim", variable) AndAlso
            CheckIfVarExists(cmd, "IFsVarDim", usesVariable)) Then
            ' No update, and insert is disallowed -> nothing written
            Return False
        End If

        '--INSERT---
        cmd.Parameters.Clear()
        'mti 2025/10/30 start chaged varlink to varlinkdim: 
        cmd.CommandText =
        "INSERT INTO VarLinkDim (Variable, UsesVariable, Distance) " &
        "VALUES (@v, @uv, @dist)"
        'cmd.CommandText =
        '"INSERT INTO VarLinkDim (Variable, UsesVariable, Distance, Code, Comment) " &
        '"VALUES (@v, @uv, @dist, @code, @comment)"


        cmd.Parameters.AddWithValue("@v", variable)
        cmd.Parameters.AddWithValue("@uv", usesVariable)
        cmd.Parameters.AddWithValue("@dist", distance)
        'cmd.Parameters.AddWithValue("@code", vbcode)
        'cmd.Parameters.AddWithValue("@comment", commentText)
        'mti 2025/10/30 end

        cmd.ExecuteNonQuery()
        Return True  'False
    End Function

    Private Function CheckIfVarExists(baseCmd As SqliteCommand,
                                 tableName As String,
                                 varName As String) As Boolean
        'mti 2025/10/30 new proc
        Dim sql As String =
        $"SELECT 1 FROM {tableName} WHERE NAME = @name COLLATE NOCASE LIMIT 1;"

        Using checkCmd As New SqliteCommand(sql, baseCmd.Connection, baseCmd.Transaction)
            checkCmd.Parameters.AddWithValue("@name", varName)
            Dim result = checkCmd.ExecuteScalar()
            Return result IsNot Nothing
        End Using

    End Function

    ' ============================================================================
    ' BuildIFsVarDim
    ' - Copies rows from IFsVar to IFsVarDim using a schema-robust approach.
    ' - Discovers columns at runtime via PRAGMA table_info.
    ' - Uses the INTERSECTION of columns present in both source (IFsVar)
    '   and destination (IFsVarDim).
    ' - Assigns values by COLUMN NAME (not ordinals), avoiding breakage when
    '   columns are added/removed/renamed.
    ' - Preserves your special logic:
    '     * Parameters (all-lowercase NAME) are copied verbatim (no expansion).
    '     * Variables (otherwise) expand across DIMENSION1..3 using GRPS labels.
    '     * NAME and DEFINITION are overridden to include the dimension suffix.
    ' - Keeps durability tradeoffs explicit (fast PRAGMAs) and contained in one txn.
    ' ============================================================================

    Public Sub BuildIFsVarDim(sqlitePath As String)
        ' Open a single connection for the full operation
        'mti 2025/10/30 new proc
        Using SQLiteCon As New SqliteConnection($"Data Source={sqlitePath};")
            SQLiteCon.Open()

            ' --------------------------------------------------------------------
            ' Performance PRAGMAs (fast-but-less-durable). Use with care:
            '  - foreign_keys OFF: defer enforcement while bulk-loading
            '  - ignore_check_constraints ON: skip non-critical checks
            '  - journal_mode MEMORY + synchronous OFF: faster, but risk on crash
            ' --------------------------------------------------------------------
            Using SQLiteCmd = SQLiteCon.CreateCommand()
                SQLiteCmd.CommandText =
                "PRAGMA foreign_keys = OFF;" &
                "PRAGMA ignore_check_constraints = ON;" &
                "PRAGMA journal_mode = MEMORY;" &
                "PRAGMA synchronous = OFF;"
                SQLiteCmd.ExecuteNonQuery()
            End Using

            ' Begin a single transaction to wrap the whole copy (+ expansions)
            Using trnx = SQLiteCon.BeginTransaction()
                ' 1) Clear the destination table (idempotent rebuild)
                Using SQLiteCmd As New SqliteCommand("DELETE FROM [IFsVarDim];", SQLiteCon, trnx)
                    SQLiteCmd.ExecuteNonQuery()
                End Using

                ' 2) Get list of column names and datatypes from IFsVar (source) and IFsVarDim (destination)
                Dim srcCols = GetColumns(SQLiteCon, "IFsVar")     ' List(Of (Name, Type))
                Dim dstCols = GetColumns(SQLiteCon, "IFsVarDim")  ' List(Of (Name, Type))

                ' If the destination has auto-IDs or computed columns, exclude here
                Dim exclude As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
                "ID" ' add more if applicable
            }

                ' Build the list of COMMON columns, in the order of the destination
                ' (keeps INSERT/SELECT readable and stable)
                Dim cols = MakeCommonColumnListPreferDestOrder(srcCols, dstCols, exclude)
                If cols.Count = 0 Then
                    Throw New InvalidOperationException("No common columns between IFsVar and IFsVarDim.")
                End If


                'Some columns(Like "Display Name" Or "Help Path") contain spaces Or special characters.
                'SQLite(And SQL Server) require such identifiers To be quoted In square brackets [ ].
                ' Quote identifiers (many column names have spaces) using [] to wrap them
                'result colListSql = "[NAME],[DISPLAY NAME],[SPECIALIZED DISPLAY NAME],[SCENARIO NAME],[DIAGRAM NAME],[DEFINITION],[TYPE],[TYPE NAME],... etc
                Dim colListSql = String.Join(",", cols.Select(Function(c) $"[{c.Name}]"))


                'mti 2025/12/08 start - Exclude variables and parameters using file:
                '' 3) Prepare SELECT from IFsVar using the common columns only
                'Dim selSql = $"SELECT {colListSql} FROM [IFsVar];"
                ' ----------------------------------------------------------------
                ' Build SELECT from IFsVar, excluding VarNamesExcluded (by NAME)
                ' VarNamesExcluded is a global string() and may be Nothing/empty.
                ' ----------------------------------------------------------------
                Dim cleanedExclusions As String() = {}
                Dim whereClause As String = ""

                If VarNamesExcluded IsNot Nothing Then
                    cleanedExclusions =
                    VarNamesExcluded.
                        Where(Function(s) Not String.IsNullOrWhiteSpace(s)).
                        Distinct(StringComparer.OrdinalIgnoreCase).
                        ToArray()
                End If

                If cleanedExclusions.Length > 0 Then
                    ' Build parameter placeholders: @ex0, @ex1, ...
                    Dim paramNames As New List(Of String)()
                    For i = 0 To cleanedExclusions.Length - 1
                        paramNames.Add($"@ex{i}")
                    Next
                    whereClause = " WHERE [NAME] NOT IN (" & String.Join(",", paramNames) & ")"
                End If

                ' 3) Prepare SELECT from IFsVar using the common columns only (+ optional WHERE)
                Dim selSql = $"SELECT {colListSql} FROM [IFsVar]{whereClause};"
                'mti 2025/12/08 end

                ' Build parameter names per column (sanitized to be safe as @p_* tokens)
                Dim pNameByCol = cols.ToDictionary(Function(c) c.Name,
                                               Function(c) SanitizeParamName(c.Name),
                                               StringComparer.OrdinalIgnoreCase)

                ' 4) Prepare INSERT into IFsVarDim with named parameters built from pNamebyCol list
                Dim valuesList = String.Join(",", cols.Select(Function(c) pNameByCol(c.Name)))
                Dim insSql = $"INSERT INTO [IFsVarDim] ({colListSql}) VALUES ({valuesList});"

                ' Destination type dictionary to infer DbType for each parameter
                'For each element d in the collection dstCols, use d.Item1 as the key and d.Item2 as the value.
                Dim dstTypeByName = dstCols.ToDictionary(Function(d) d.Item1, Function(d) d.Item2,
                                                     StringComparer.OrdinalIgnoreCase)

                Using SQLiteCmd As New SqliteCommand(selSql, SQLiteCon, trnx)

                    'mti 2025/12/08 start
                    ' Bind exclusion parameters (excluded IFsVar variables/parameters) only if we actually have exclusions
                    If cleanedExclusions.Length > 0 Then
                        For i = 0 To cleanedExclusions.Length - 1
                            SQLiteCmd.Parameters.AddWithValue($"@ex{i}", cleanedExclusions(i))
                        Next
                    End If
                    'mti 2025/12/08 end

                    Using rdr = SQLiteCmd.ExecuteReader()
                        ' Prepare a reusable INSERT command with parameters
                        Using SQLiteCmdInsert As New SqliteCommand(insSql, SQLiteCon, trnx)
                            ' Create parameters once, bind DbTypes from destination affinity
                            For Each c In cols
                                'Dim dbt = ToDbType(If(dstTypeByName.ContainsKey(c.Name), dstTypeByName(c.Name), c.Type))
                                Dim dbt As SqliteType = ConvertDbTypeToSqliteType(ToDbType(If(dstTypeByName.ContainsKey(c.Name),
                                          dstTypeByName(c.Name),
                                          c.Type)))
                                SQLiteCmdInsert.Parameters.Add(pNameByCol(c.Name), dbt)
                            Next
                            SQLiteCmdInsert.Prepare()

                            ' Map column name -> ordinal for the current reader
                            Dim ordByName = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
                            For i = 0 To rdr.FieldCount - 1
                                ordByName(rdr.GetName(i)) = i
                            Next

                            ' 5) Row loop
                            While rdr.Read()
                                ' Read the fields we override or inspect for logic
                                Dim name As String = Convert.ToString(SafeGet(rdr, ordByName("NAME")), CultureInfo.InvariantCulture)
                                Dim defn As String = Convert.ToString(SafeGet(rdr, ordByName("DEFINITION")), CultureInfo.InvariantCulture)

                                ' DIMENSION1..3 can be numeric, text, or empty—coerce robustly
                                Dim d1 As Integer? = CoerceNullableInt(SafeGet(rdr, ordByName("DIMENSION1")))
                                Dim d2 As Integer? = CoerceNullableInt(SafeGet(rdr, ordByName("DIMENSION2")))
                                Dim d3 As Integer? = CoerceNullableInt(SafeGet(rdr, ordByName("DIMENSION3")))

                                ' Your logic: parameters = all-lowercase name
                                Dim isParameter As Boolean = IsAllLowercase(name)



                                ' Build dimension name (e.g., Ag, Energy, Material...) vectors (v1 to v3) for DIMENSION columns 1 to 3 (only if dim no > 1); needs to check for age-group etc
                                Dim vecs As New List(Of List(Of String))()
                                If Not isParameter Then
                                    Dim v1 = GetDimVector(d1)
                                    Dim v2 = GetDimVector(d2)
                                    Dim v3 = GetDimVector(d3)
                                    If v1 IsNot Nothing Then vecs.Add(v1)
                                    If v2 IsNot Nothing Then vecs.Add(v2)
                                    If v3 IsNot Nothing Then vecs.Add(v3)
                                End If

                                If isParameter OrElse vecs.Count = 0 Then
                                    ' Insert only row (for parameter or no dimension expansion needed ones) -----
                                    AssignParamsGeneric(SQLiteCmdInsert, rdr, cols, ordByName, pNameByCol)
                                    SetOverrides(SQLiteCmdInsert, pNameByCol, name, defn, d1, d2, d3, Nothing)


                                    SQLiteCmdInsert.ExecuteNonQuery()

                                Else

                                    ' Add dimensioned rows for : Cartesian expansion across all present dimensions
                                    For Each combo In Cartesian(vecs)
                                        Dim dimSuffix As String = "(" & String.Join(", ", combo) & ")"

                                        ' Compose the new NAME and DEFINITION with suffix
                                        Dim newName As String = name & dimSuffix
                                        Dim newDef As String = AppendDimToDefinition(defn, dimSuffix)

                                        ' Copy all base values from source row, then override the few fields
                                        AssignParamsGeneric(SQLiteCmdInsert, rdr, cols, ordByName, pNameByCol)
                                        SetOverrides(SQLiteCmdInsert, pNameByCol, newName, newDef, d1, d2, d3, dimSuffix)


                                        SQLiteCmdInsert.ExecuteNonQuery()

                                    Next

                                End If
                            End While
                        End Using
                    End Using
                End Using

                ' Commit the full batch
                trnx.Commit()
            End Using
        End Using
    End Sub
    Public Sub FlagParametersInIFsVarDim(sqlitePath As String)
        'mti 2025/12/08 - Take this procedure if you have not taken it earlier
        'mti 2025/12/03 post-process IFsVarDim to set IsParameter = Yes/No based on NAME (uppercase,lowercase)

        Using SQLiteCon As New SqliteConnection($"Data Source={sqlitePath};")
            SQLiteCon.Open()

            Using trnx = SQLiteCon.BeginTransaction()
                ' 1) Get the distinct NAME values so we only compute once per name
                Dim selectSql As String = "SELECT DISTINCT [NAME] FROM [IFsVarDim];"

                Using cmdSelect As New SqliteCommand(selectSql, SQLiteCon, trnx)
                    Using rdr = cmdSelect.ExecuteReader()

                        ' 2) Prepare an UPDATE command that updates all rows with a given NAME
                        Dim updateSql As String =
                        "UPDATE [IFsVarDim] " &
                        "SET [IsParameter] = @IsParameter " &
                        "WHERE [NAME] = @NAME;"

                        Using cmdUpdate As New SqliteCommand(updateSql, SQLiteCon, trnx)
                            cmdUpdate.Parameters.Add("@IsParameter", SqliteType.Text)
                            cmdUpdate.Parameters.Add("@NAME", SqliteType.Text)

                            While rdr.Read()
                                Dim name As String =
                                If(rdr.IsDBNull(0),
                                   String.Empty,
                                   rdr.GetString(0))

                                ' existing logic: parameters = all-lowercase name
                                Dim isParameter As Boolean = IsAllLowercase(name)
                                Dim isParamValue As String = If(isParameter, "Yes", "No")

                                cmdUpdate.Parameters("@IsParameter").Value = isParamValue
                                cmdUpdate.Parameters("@NAME").Value = name
                                cmdUpdate.ExecuteNonQuery()
                            End While
                        End Using
                    End Using
                End Using

                trnx.Commit()
            End Using
        End Using
    End Sub

    Private Function WriteVarLink(cmd As SqliteCommand,
                               variable As String, usesVariable As String,
                               distance As Integer, vbcode As String, commentText As String) As Boolean
        'mti 2025/09/17 new procedure
        ' Returns True if UPDATE happened, False if INSERT happened.

        ' UPDATE first (prepend code)
        cmd.Parameters.Clear()  'mti commented

        'cmd.CommandText =
        '"UPDATE VarLink " &
        '"SET Code = @code || char(13) || Code " &
        '"WHERE Variable = @v AND UsesVariable = @uv"

        Dim funcName As String = ExtractFuncName(commentText, "TABLFUNC,", "ANALFUNC,")
        funcName = funcName.Replace("""", """""")
        cmd.CommandText =
                        "UPDATE VarLink " &
                        "SET Code = @code || char(13) || Code, " &
                          "    Comment = CASE " &
                          "                 WHEN @comment  = '' THEN Comment " &
                          "                 WHEN INSTR(@comment, @fname) > 0 THEN Comment " &
                          "                 WHEN Comment = '' THEN @comment " &
                          "                 ELSE @comment || Char(44)  || Comment " &
                          "               END " &
                        "WHERE Variable = @v AND UsesVariable = @uv"


        cmd.Parameters.AddWithValue("@code", vbcode)
        cmd.Parameters.AddWithValue("@v", variable)
        cmd.Parameters.AddWithValue("@uv", usesVariable)
        cmd.Parameters.AddWithValue("@comment", commentText)
        cmd.Parameters.AddWithValue("@fname", funcName)

        Dim n As Integer = cmd.ExecuteNonQuery()
        If n > 0 Then Return True

        ' Else INSERT
        cmd.Parameters.Clear()
        cmd.CommandText =
        "INSERT INTO VarLink (Variable, UsesVariable, Distance, Code, Comment) " &
        "VALUES (@v, @uv, @dist, @code, @comment)"

        cmd.Parameters.AddWithValue("@v", variable)
        cmd.Parameters.AddWithValue("@uv", usesVariable)
        cmd.Parameters.AddWithValue("@dist", distance)
        cmd.Parameters.AddWithValue("@code", vbcode)
        cmd.Parameters.AddWithValue("@comment", commentText)

        cmd.ExecuteNonQuery()
        Return False
    End Function

    Private Function ConvertDbTypeToSqliteType(dbType As DbType) As SqliteType
        'mti 2025/10/30 new proc
        Select Case dbType
            Case DbType.Int16, DbType.Int32, DbType.Int64, DbType.Boolean
                Return SqliteType.Integer
            Case DbType.Single, DbType.Double, DbType.Decimal
                Return SqliteType.Real
            Case DbType.String, DbType.AnsiString, DbType.Date, DbType.DateTime
                Return SqliteType.Text
            Case DbType.Binary
                Return SqliteType.Blob
            Case Else
                Throw New NotSupportedException($"Unsupported DbType: {dbType}")
        End Select
    End Function
    ' ============================================================================
    ' OVERRIDE / ASSIGNMENT HELPERS
    ' ============================================================================

    ' Assigns all common columns from the data reader to the INSERT parameters,
    ' by COLUMN NAME (no reliance on fixed ordinals). This is the generic copy step.
    Private Sub AssignParamsGeneric(SQLiteCmdInsert As SqliteCommand,
                                rdr As SqliteDataReader,
                                cols As List(Of (Name As String, Type As String)),
                                ordByName As Dictionary(Of String, Integer),
                                pNameByCol As Dictionary(Of String, String))
        'mti 2025/10/30 new proc
        For Each c In cols
            Dim ord As Integer = ordByName(c.Name) ' exists by construction
            Dim val As Object = SafeGet(rdr, ord)  ' DBNull.Value if NULL
            SQLiteCmdInsert.Parameters(pNameByCol(c.Name)).Value = val
        Next
    End Sub

    ' Applies dimension names on top of the generic copy:
    '  - NAME: always set to newName (optionally suffixed)
    '  - DEFINITION: set to newDef (optionally suffixed) or NULL if blank
    '  - DIMENSION1..3: ensure NULL when missing
    '  - dimSuffix is passed for convenience (not used directly here except for newName/newDef)
    Private Sub SetOverrides(SQLiteCmdInsert As SqliteCommand,
                         pNameByCol As Dictionary(Of String, String),
                         newName As String,
                         newDef As String,
                         d1 As Integer?, d2 As Integer?, d3 As Integer?,
                         dimSuffix As String)

        ' Override NAME
        'mti 2025/10/30 new proc
        If pNameByCol.ContainsKey("NAME") Then
            SQLiteCmdInsert.Parameters(pNameByCol("NAME")).Value = newName
        End If

        ' Override DEFINITION (NULL if blank/whitespace)
        If pNameByCol.ContainsKey("DEFINITION") Then
            Dim v As Object = If(String.IsNullOrWhiteSpace(newDef), CType(DBNull.Value, Object), newDef)
            SQLiteCmdInsert.Parameters(pNameByCol("DEFINITION")).Value = v
        End If

        ' Coerce DIMENSION1..3 (make sure they are NULL when missing)
        If pNameByCol.ContainsKey("DIMENSION1") Then
            SQLiteCmdInsert.Parameters(pNameByCol("DIMENSION1")).Value = If(d1.HasValue, CObj(d1.Value), CObj(DBNull.Value))
        End If
        If pNameByCol.ContainsKey("DIMENSION2") Then
            SQLiteCmdInsert.Parameters(pNameByCol("DIMENSION2")).Value = If(d2.HasValue, CObj(d2.Value), CObj(DBNull.Value))
        End If
        If pNameByCol.ContainsKey("DIMENSION3") Then
            SQLiteCmdInsert.Parameters(pNameByCol("DIMENSION3")).Value = If(d3.HasValue, CObj(d3.Value), CObj(DBNull.Value))
        End If
    End Sub

    Private Function SafeGetString(r As IDataRecord, idx As Integer) As String
        'mti 2025/10/30 new proc
        ' Null-safe string read by ordinal
        If r.IsDBNull(idx) Then Return String.Empty
        Return Convert.ToString(r.GetValue(idx))
    End Function

    Private Function SafeGetNullableInt(r As IDataRecord, idx As Integer) As Integer?
        'mti 2025/10/30 new proc
        ' Null-safe integer read by ordinal (strict int)
        If r.IsDBNull(idx) Then Return Nothing
        Dim v = Convert.ToInt32(r.GetValue(idx))
        Return v
    End Function

    Private Function CoerceNullableInt(v As Object) As Integer?
        'mti 2025/10/30 new proc
        ' Coerces a loosely-typed SQLite value to nullable Integer:
        '  - Handles NULL/DBNull
        '  - Handles strings like "" or "3" or "3.0"
        If v Is Nothing OrElse v Is DBNull.Value Then Return Nothing
        Dim s As String = Convert.ToString(v, CultureInfo.InvariantCulture).Trim()
        If s = "" Then Return Nothing
        Dim n As Integer
        If Integer.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, n) Then Return n
        Dim d As Double
        If Double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, d) Then Return CInt(Math.Truncate(d))
        Return Nothing
    End Function

    Private Function IsAllLowercase(s As String) As Boolean
        'mti 2025/10/30 new proc
        ' Parameter detection: a "parameter" has an all-lowercase NAME
        If String.IsNullOrEmpty(s) Then Return False
        For Each ch In s
            If Char.IsLetter(ch) AndAlso Not Char.IsLower(ch) Then Return False
        Next
        Return True
    End Function
    Private Function GetDimVector(idx As Integer?) As List(Of String)
        'mti 2025/10/30 new proc
        ' Given a DIMENSION index (>1), pull the list of labels from GRPS(row, col)
        ' Returns Nothing if no real dimension (<=1) or if no labels are found.
        ' NOTE: Assumes GRPS is available in the containing scope (your existing codebase).
        If Not idx.HasValue Then Return Nothing
        If idx.Value <= 1 Then Return Nothing ' 0/1/null => no dimension

        Dim list As New List(Of String)()
        Dim row As Integer = idx.Value
        Dim col As Integer = 1

        ' Walk columns 1..N for this row until GRPS throws or returns empty
        Do
            Dim label As String = Nothing
            Try
                label = GRPS(row, col)
            Catch ex As Exception
                Exit Do
            End Try

            If String.IsNullOrWhiteSpace(label) Then Exit Do

            ' Trim and normalize label
            Dim lbl As String = label.Trim()

            ' Skip if excluded
            Dim skip As Boolean = False
            If DimNamesExcluded IsNot Nothing Then
                For Each exName As String In DimNamesExcluded
                    If String.Equals(lbl, exName, StringComparison.OrdinalIgnoreCase) Then
                        skip = True
                        Exit For
                    End If
                Next
            End If

            ' Add only if not excluded
            If Not skip Then
                list.Add(lbl)
            End If

            col += 1
        Loop

        If list.Count = 0 Then Return Nothing
        Return list
    End Function

    Private Iterator Function Cartesian(vectors As List(Of List(Of String))) As IEnumerable(Of List(Of String))
        'mti 2025/10/30 new proc
        ' Cartesian product utility used for dimension expansion
        Dim acc As New List(Of List(Of String)) From {New List(Of String)()}
        For Each vec In vectors
            Dim nextAcc As New List(Of List(Of String))()
            For Each prefix In acc
                For Each item In vec
                    Dim copy = New List(Of String)(prefix) From {item}
                    nextAcc.Add(copy)
                Next
            Next
            acc = nextAcc
        Next
        For Each combo In acc
            Yield combo
        Next
    End Function

    Private Function AppendDimToDefinition(defn As String, dimSuffix As String) As String
        'mti 2025/10/30 new proc
        ' Append a dimension suffix like "(Male)" to the definition text.
        If String.IsNullOrWhiteSpace(defn) Then Return defn
        Return defn.TrimEnd() & " " & dimSuffix
    End Function


    Private Function GetColumns(SQLiteCon As SqliteConnection, tableName As String) As List(Of (Name As String, Type As String))
        'mti 2025/10/30 new proc
        ' Returns List of (Name, Type) for a given table via PRAGMA table_info.
        ' Type is the declared SQLite type text (may be empty; we still map it loosely).
        Dim cols As New List(Of (String, String))
        Using SQLiteCmd = SQLiteCon.CreateCommand()
            SQLiteCmd.CommandText = $"PRAGMA table_info([{tableName}]);"
            Using r = SQLiteCmd.ExecuteReader()
                While r.Read()
                    ' table_info columns: cid, name, type, notnull, dflt_value, pk
                    Dim nm = r.GetString(1)
                    Dim tp = If(r.IsDBNull(2), "", r.GetString(2))
                    cols.Add((nm, tp))
                End While
            End Using
        End Using
        Return cols
    End Function


    Private Function MakeCommonColumnListPreferDestOrder(srcCols As List(Of (String, String)),
                                                     dstCols As List(Of (String, String)),
                                                     exclude As HashSet(Of String)) As List(Of (Name As String, Type As String))
        'mti 2025/10/30 new proc
        ' Build the common column list, preferring destination order for readability/stability.
        ' Uses source type for read affinity hints; excludes any named in "exclude".
        Dim srcByName = srcCols.ToDictionary(Function(c) c.Item1, Function(c) c.Item2, StringComparer.OrdinalIgnoreCase)
        Dim out As New List(Of (String, String))
        For Each d In dstCols
            Dim nm = d.Item1
            If exclude IsNot Nothing AndAlso exclude.Contains(nm) Then Continue For
            If srcByName.ContainsKey(nm) Then
                ' Keep the source's declared type; order follows destination
                out.Add((nm, srcByName(nm)))
            End If
        Next
        Return out
    End Function

    Private Function SanitizeParamName(col As String) As String
        ' Convert a column name like "[DISPLAY NAME]" into a safe parameter name "@p_DISPLAYNAME".
        ' Removes spaces/brackets/slashes/dashes; prefixes with "@p_" to avoid collisions.
        'mti 2025/10/30 new proc
        Dim p = col.Replace(" ", "").Replace("[", "").Replace("]", "").Replace("/", "").Replace("-", "")
        Return "@p_" & p
    End Function

    Private Function ToDbType(sqliteType As String) As DbType
        ' Loose mapping from SQLite declared type to DbType for parameter creation.
        ' It’s coarse by design; need to refine if you have strict typing in IFsVarDim.
        'mti 2025/10/30 new proc
        Dim t = If(sqliteType, "").Trim().ToUpperInvariant()
        If t.Contains("INT") Then Return DbType.Int64
        If t.Contains("REAL") OrElse t.Contains("FLOA") OrElse t.Contains("DOUB") Then Return DbType.Double
        If t.Contains("NUM") OrElse t.Contains("DEC") Then Return DbType.Decimal
        If t.Contains("DATE") OrElse t.Contains("TIME") Then Return DbType.String ' store ISO text unless you parse to DateTime
        If t.Contains("BLOB") Then Return DbType.Binary
        Return DbType.String ' default TEXT
    End Function


    Private Function SafeGet(r As SqliteDataReader, i As Integer) As Object
        'value fetched from the row r column (i) od sqLite data table (safe with nulls - returns DBNull.Value if NULL)
        'mti 2025/10/30 new proc
        If r.IsDBNull(i) Then Return DBNull.Value
        Return r.GetValue(i)
    End Function

    Function ExtractInParens(input As String) As String
        'mti 2025/09/17 new procedure
        Dim m As Match = Regex.Match(input, "\(([^)]+)\)")
        If m.Success Then
            Return m.Groups(1).Value.Trim()
        Else
            Return String.Empty
        End If
    End Function
    Public Function ReadPublicConsts(fileName As String) As String(,)
        If Not File.Exists(fileName) Then Throw New FileNotFoundException(fileName)
        'mti 2025/08/07 New
        ' Returns a 2‑D array (rows = constants, columns = {name, value}) from a file.

        Dim tempList As New List(Of String())

        For Each line As String In File.ReadAllLines(fileName)
            Dim trimmed As String = line.Trim()
            ' Skip blank lines and full-line comments
            If String.IsNullOrEmpty(trimmed) OrElse trimmed.StartsWith("'") Then Continue For

            ' Remove any inline comment (after a single quote)
            Dim codePart As String = trimmed.Split("'"c)(0).Trim()

            ' Only process lines that begin with "Public Const"
            If codePart.StartsWith("Public Const", StringComparison.OrdinalIgnoreCase) Then
                ' Remove "Public Const" and split at "=" Let JRS Know:mti 2025/08/26 start
                Dim remainder As String = codePart.Substring("Public Const".Length).Trim()
                Dim parts() As String = remainder.Split({"="c}, 2)

                If parts.Length = 2 Then
                    Dim namePart As String = parts(0).Trim()
                    Dim valuePart As String = parts(1).Trim()
                    Dim number As Integer

                    'mti 2025/08/27  start
                    'ignore age groups
                    'Public Const TotPovAGCount = 11 ' number of agegroups in poverty data
                    'Public Const AG_0_4 = 1
                    'Public Const AG_5_9 = 2
                    'Public Const AG_10_14 = 3
                    'Public Const AG_15_24 = 4
                    'Public Const AG_25_34 = 5
                    'Public Const AG_35_39 = 6
                    'Public Const AG_40_49 = 7
                    'Public Const AG_50_54 = 8
                    'Public Const AG_55_59 = 9
                    'Public Const AG_60_Over = 10
                    'Public Const AG_AllAges = 11
                    'mti 2021 / 12 / 9 End

                    'jrs 2022 / 11 / 28 start
                    'Public Const Age15_19 = 1
                    'Public Const Age20_24 = 2
                    'mti 2025/08/27  end

                    'How about Total, National etc

                    'don't want Total as a dimension,as it can sum  dimensions
                    'poverty
                    If namePart.Trim.ToUpper = "AG_0_4" Then Continue For
                    If namePart.Trim.ToUpper = "AG_5_9" Then Continue For
                    If namePart.Trim.ToUpper = "AG_10_14" Then Continue For
                    If namePart.Trim.ToUpper = "AG_15_24" Then Continue For
                    If namePart.Trim.ToUpper = "AG_25_34" Then Continue For
                    If namePart.Trim.ToUpper = "AG_35_39" Then Continue For
                    If namePart.Trim.ToUpper = "AG_40_49" Then Continue For
                    If namePart.Trim.ToUpper = "AG_50_54" Then Continue For
                    If namePart.Trim.ToUpper = "AG_55_59" Then Continue For
                    If namePart.Trim.ToUpper = "AG_60_Over" Then Continue For
                    If namePart.Trim.ToUpper = "AG_ALLAGES" Then Continue For

                    'age of marriage
                    If namePart.Trim.ToUpper = "AGE15_19" Then Continue For
                    If namePart.Trim.ToUpper = "AGE20_24" Then Continue For


                    ' Only keep numeric initializers
                    If Integer.TryParse(valuePart, number) Then
                        tempList.Add(New String() {namePart, number.ToString()})
                    End If
                End If
            End If
        Next

        ' Convert the list to a 2‑D array (rows x 2 columns)
        Dim result(tempList.Count - 1, 1) As String
        For i As Integer = 0 To tempList.Count - 1
            result(i, 0) = tempList(i)(0) ' index name
            result(i, 1) = tempList(i)(1) ' index number
        Next

        Return result
    End Function

    Public Function ParseVarWithDims(ByVal VarNameWithDim As String,
                                 ByVal GRPSEnum(,) As String,
                                 Optional ByVal DimNo1 As Integer = 0,
                                 Optional ByVal DimNo2 As Integer = 0,
                                 Optional ByVal DimNo3 As Integer = 0) As List(Of String)

        'mti 2025/08/07 New

        ''' <summary>
        ''' Parse a variable possibly with up to 3 subscripts and map each to its true
        ''' dimension element name via GRPSEnum (literal→index) and GRPS (group,index→name).
        ''' Returns List(Of String) with 4 entries: BaseVar, Dim1Name, Dim2Name, Dim3Name.
        ''' Missing/unknown dims are returned as "".
        ''' </summary>
        ''' 
        Dim baseName As String = VarNameWithDim
        Dim dimNames(2) As String : dimNames(0) = "" : dimNames(1) = "" : dimNames(2) = ""

        If String.IsNullOrWhiteSpace(VarNameWithDim) Then
            Return New List(Of String) From {"", "", "", ""}
        End If

        ' Split base and the "(...)" part, if present
        Dim lp As Integer = VarNameWithDim.IndexOf("("c)
        If lp = -1 Then
            ' No dims at all
            Return New List(Of String) From {VarNameWithDim.Trim(), "", "", ""}
        End If

        baseName = VarNameWithDim.Substring(0, lp).Trim()
        Dim rp As Integer = VarNameWithDim.LastIndexOf(")"c)
        Dim inside As String = If(rp > lp, VarNameWithDim.Substring(lp + 1, rp - lp - 1), "").Trim()

        ' Tokenize up to 3 dims
        Dim parts As String() = If(String.IsNullOrEmpty(inside),
                               Array.Empty(Of String)(),
                               inside.Split(New Char() {","c}, StringSplitOptions.None))

        ' Resolve each token -> index -> name using the provided dimension numbers
        Dim dimNos() As Integer = {DimNo1, DimNo2, DimNo3}

        For i As Integer = 0 To Math.Min(2, parts.Length - 1)
            Dim token As String = CleanToken(parts(i))

            If String.IsNullOrEmpty(token) OrElse dimNos(i) <= 0 Then
                dimNames(i) = ""
                Continue For
            End If

            Dim idx As Integer = 0
            ' Try numeric index first
            If Not Integer.TryParse(token, Globalization.NumberStyles.Integer,
                                Globalization.CultureInfo.InvariantCulture, idx) Then
                ' Map literal -> index using GRPSEnum
                idx = EnumIndexFromLiteral(token, GRPSEnum)
            End If

            If idx > 0 Then
                dimNames(i) = GroupNameFromGRPS(dimNos(i), idx)
            Else
                dimNames(i) = ""
            End If
        Next

        Return New List(Of String) From {baseName, dimNames(0), dimNames(1), dimNames(2)}
    End Function

    Private Function CleanToken(s As String) As String
        'mti 2025/08/07 New
        If s Is Nothing Then Return ""
        Dim t As String = s.Trim()

        ' Strip surrounding quotes if present
        If t.Length >= 2 AndAlso ((t(0) = """"c AndAlso t(t.Length - 1) = """"c) OrElse
                              (t(0) = "'"c AndAlso t(t.Length - 1) = "'"c)) Then
            t = t.Substring(1, t.Length - 2).Trim()
        End If

        ' Remove trailing VB type character (%, &, !, #, $) if present
        If t.Length > 0 Then
            Dim last As Char = t(t.Length - 1)
            If "%&!#$".IndexOf(last) >= 0 Then
                t = t.Substring(0, t.Length - 1).Trim()
            End If
        End If

        Return t
    End Function

    Private Function EnumIndexFromLiteral(literal As String, ByVal GRPSEnum(,) As String) As Integer
        'mti 2025/08/07 New
        If GRPSEnum Is Nothing Then Return 0
        Dim lo0 As Integer = GRPSEnum.GetLowerBound(0)
        Dim hi0 As Integer = GRPSEnum.GetUpperBound(0)

        ' Expect at least two columns: [*, 0]=literal, [*, 1]=order
        If GRPSEnum.GetUpperBound(1) < 1 Then Return 0

        For r As Integer = lo0 To hi0
            Dim lit As String = If(GRPSEnum(r, 0), String.Empty)
            If String.Equals(lit, literal, StringComparison.OrdinalIgnoreCase) Then
                Dim ordStr As String = If(GRPSEnum(r, 1), String.Empty)
                Dim ord As Integer = 0
                If Integer.TryParse(ordStr, Globalization.NumberStyles.Integer,
                                Globalization.CultureInfo.InvariantCulture, ord) Then
                    Return ord
                End If
                Exit For
            End If
        Next
        Return 0
    End Function

    Private Function GroupNameFromGRPS(DimNo As Integer, idx As Integer) As String
        'mti 2025/08/07 New
        If GRPS Is Nothing Then Return ""
        If DimNo < GRPS.GetLowerBound(0) OrElse DimNo > GRPS.GetUpperBound(0) Then Return ""
        If idx < GRPS.GetLowerBound(1) OrElse idx > GRPS.GetUpperBound(1) Then Return ""
        Return If(GRPS(DimNo, idx), String.Empty)
    End Function
    Public Sub MergeVarLink()
        'mti 2025/08/07 new procedure

        'db1 = New SqliteConnection("DataSource=" & PutFdrvRun$ & "IFsVar.db;") '& "New=true;"
        'db1.Open()
        'db1Cmd = db1.CreateCommand

        'Dim dbPath As String = PutFdrvRun$ & "IFsVar.db"
        Dim connStr As String = "DataSource=" & PutFdrvRun$ & "IFsVar.db;"
        Using conn As New SqliteConnection(connStr)
            conn.Open()

            ' Insert rows from VarLinkManual into VarLink
            ' - Only if that Variable/UsesVariable pair does not already exist
            ' - Choose a deterministic row per pair: lowest rowid in VarLinkManual
            ' - Modify Comment column to always include 'MANUAL'
            Using cmdInsertManual As New SqliteCommand("
            INSERT INTO VarLink (Variable, UsesVariable, Distance, Code, Comment)
            SELECT 
                m.Variable, 
                m.UsesVariable, 
                m.Distance, 
                m.Code,

                -- CASE branch: sets the Comment value for the inserted row
                CASE 
                    -- Branch 1: if Comment is NULL or blank → just 'MANUAL'
                    WHEN m.Comment IS NULL OR TRIM(m.Comment) = '' 
                        THEN 'MANUAL'
                    
                    -- Branch 2: if Comment has text → append ', MANUAL'
                    ELSE m.Comment || ', MANUAL'
                END AS Comment

            FROM VarLinkManual m

            -- Condition: skip if a matching Variable/UsesVariable already exists in VarLink
            WHERE NOT EXISTS (
                SELECT 1 FROM VarLink v
                WHERE v.Variable = m.Variable
                  AND v.UsesVariable = m.UsesVariable
            )

            -- Condition: only pick one row per Variable/UsesVariable group — the one with the smallest rowid
            AND m.rowid = (
                SELECT MIN(mm.rowid)
                FROM VarLinkManual mm
                WHERE mm.Variable = m.Variable
                  AND mm.UsesVariable = m.UsesVariable
            );
        ", conn)
                cmdInsertManual.ExecuteNonQuery()
            End Using

            conn.Close()
        End Using
    End Sub

    Public Sub MergeManualEntries()
        'mti 2025/10/30 new procedure replacing MergeVarLink

        Dim connStr As String = "DataSource=" & PutFdrvRun$ & "IFsVar.db;"
        Using conn As New SqliteConnection(connStr)
            conn.Open()

            ' -----------------------------
            ' 1) Merge into VarLink
            ' -----------------------------
            Dim sqlVarLink As String =
            "INSERT INTO VarLink (Variable, UsesVariable, Distance) " & vbCrLf &
            "SELECT m.Variable, m.UsesVariable, m.Distance " & vbCrLf &
            "FROM VarLinkManual m " & vbCrLf &
            "WHERE " & vbCrLf &
            "  EXISTS ( " & vbCrLf &
            "      SELECT 1 FROM IFsVar v " & vbCrLf &
            "      WHERE v.NAME = m.Variable COLLATE NOCASE " & vbCrLf &
            "  ) " & vbCrLf &
            "  AND EXISTS ( " & vbCrLf &
            "      SELECT 1 FROM IFsVar v2 " & vbCrLf &
            "      WHERE v2.NAME = m.UsesVariable COLLATE NOCASE " & vbCrLf &
            "  ) " & vbCrLf &
            "  AND NOT EXISTS ( " & vbCrLf &
            "      SELECT 1 FROM VarLink vl " & vbCrLf &
            "      WHERE vl.Variable = m.Variable COLLATE NOCASE " & vbCrLf &
            "        AND vl.UsesVariable = m.UsesVariable COLLATE NOCASE " & vbCrLf &
            "  );"

            Using cmdVarLink As New SqliteCommand(sqlVarLink, conn)
                cmdVarLink.ExecuteNonQuery()
            End Using

            ' -----------------------------
            ' 2) Merge into VarLinkDim
            ' -----------------------------
            Dim sqlVarLinkDim As String =
            "INSERT INTO VarLinkDim (Variable, UsesVariable, Distance) " & vbCrLf &
            "SELECT m.Variable, m.UsesVariable, m.Distance " & vbCrLf &
            "FROM VarLinkManual m " & vbCrLf &
            "WHERE " & vbCrLf &
            "  EXISTS ( " & vbCrLf &
            "      SELECT 1 FROM IFsVarDim d " & vbCrLf &
            "      WHERE d.NAME = m.Variable COLLATE NOCASE " & vbCrLf &
            "  ) " & vbCrLf &
            "  AND EXISTS ( " & vbCrLf &
            "      SELECT 1 FROM IFsVarDim d2 " & vbCrLf &
            "      WHERE d2.NAME = m.UsesVariable COLLATE NOCASE " & vbCrLf &
            "  ) " & vbCrLf &
            "  AND NOT EXISTS ( " & vbCrLf &
            "      SELECT 1 FROM VarLinkDim vd " & vbCrLf &
            "      WHERE vd.Variable = m.Variable COLLATE NOCASE " & vbCrLf &
            "        AND vd.UsesVariable = m.UsesVariable COLLATE NOCASE " & vbCrLf &
            "  );"

            Using cmdVarLinkDim As New SqliteCommand(sqlVarLinkDim, conn)
                cmdVarLinkDim.ExecuteNonQuery()
            End Using

            conn.Close()
        End Using
    End Sub
    Private Sub WriteLeftRightToCsv(csvFileName As String, startIndex As Integer, endIndex As Integer)

        'mti 2026/04/21 new procedure to write LeftSide and RightSide arrays to a CSV file for inspection

        Using sw As New System.IO.StreamWriter(csvFileName, False, System.Text.Encoding.UTF8)
            sw.WriteLine("Index,LeftSide,RightSide")

            For i As Integer = startIndex To endIndex
                Dim leftVal As String = ""
                Dim rightVal As String = ""

                If LeftSide(i) IsNot Nothing Then leftVal = LeftSide(i).ToString()
                If Rightside(i) IsNot Nothing Then rightVal = Rightside(i).ToString()

                ' Escape quotes for CSV
                leftVal = """" & leftVal.Replace("""", """""") & """"
                rightVal = """" & rightVal.Replace("""", """""") & """"

                sw.WriteLine(i & "," & leftVal & "," & rightVal)
            Next
        End Using

    End Sub
    Public Sub BuildDocumentation()

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti 2026/04/21  - take entire procedure(most changes are in this proc)
        'mti 2025/12/08 - take entire routine (change in several places)

        'mti Documentation February 2006 - whole procedure
        'Driver Code Changes June 2003 - take whole proc

        'jrs 2023/01/09
        Dim strcode$, LeftVar$, RightVar$
        Dim StartAt As Integer
        Dim GoBackUpto As Integer

        'mti 2025/08/07 start
        Dim LeftVarWithDim$
        Dim RightVarWithDim$
        'Dim GRPSEnum(,) As String = ReadPublicConsts(fileName As String) 
        'mti 2025/08/07 end 

        Try

            Dim ParsedFile$
            ParsedFile$ = ""

            Dim TStruct(50) As String
            Dim TStructNo(50) As Integer
            'mti 2025/07/14 end

            Dim UnfinishedNo%
            Dim TCBlock As String

            Dim AddCode As Boolean

            Dim TmpVar$
            Dim CurModule$
            Dim MStart%, MEnd%
            Dim varfound(500) As String
            Dim vbcode As String

            Dim VFCounter%, pos%, pos1%
            Dim CodeFileDir$, CodeFileSubDir$, Filename$

            Dim db1 As SqliteConnection
            Dim db1Cmd As SqliteCommand

            Dim j2%, k2%, l2%, RightCnt% ' , txtcnt%

            Dim UpLimit%, DownLimit%, m2%

            Dim cVarNames1 As List(Of String)
            'Dim cVarNames2 As Collection
            Dim ModCount%

            'mti 2025/08/07 start
            Dim cVarNames1WithDim As List(Of String)
            Dim LeftVarList As List(Of String)
            Dim RightVarList As List(Of String)
            'mti 2025/08/07 end 

            'need to uncomment the line after
            'Dummy line from Mohammod to help documentation parsing process
            'for the variable POP in Populat.Forecast.vb
            'If MsgBox("You need to uncomment the (two) places after ['Dummy line from Mohammod..]" + vbCr + " in IfsPop and IfsEduc in populat.Forecast.vb before code parsing. Proceed?", vbYesNo) = vbNo Then
            '    exit sub
            'End If


            Erase ModName
            ReDim ModName(20)

            Erase ModStart
            ReDim ModStart(20)

            Erase ModEnd
            ReDim ModEnd(20)

            Erase CodeBlock

            'mti 2025/07/14 start
            'mti 2025/08/09 start
            ReDim CodeBlock(40000) '30000  '20000
            'mti 2025/08/09 end
            'mti 2025/07/14 end

            Erase CodeArray
            ReDim CodeArray(3000)

            Erase CodeArray2Block
            ReDim CodeArray2Block(3000)


            vbKWCount = 0
            GetKeyWordList()

            'mti 2025/10/30 start
            'Read the (to be) excluded dim names from a file to an array of string (share file with JSR)
            GetDimNamesExcluded()
            'mti 2025/10/30 end

            'mti 2025/12/08 start
            'Read the (to be) excluded dim names from a file to an array of string (share file with JSR)
            GetVarNamesExcluded()
            'mti 2025/12/08 end

            codelines = 0

            'mti  2025/09/17  start
            Dim startTime As DateTime = DateTime.Now
            Console.WriteLine(vbCrLf & "Start date and time: " & startTime.ToString() & vbCrLf)
            'mti  2025/09/17  end

            Dim ParseThoroughCallSequence As Boolean = True

            'mti 2025/08/07 Start Dimensionality and Functions(TABL/ANAL)
            Dim CurComment As String
            Dim GRPSEnum(,) As String = ReadPublicConsts(StartDirec + "src\IFs.Core\Models\Commons.vb")

            'mti 2025/08/07 end

            If ParseThoroughCallSequence Then

                CodeFileDir$ = Path.Combine(StartDirec, "TmpCode")  'this is where reorganized code files will be written,with same names  

                'mti 2025/12/16 Victory Day  (note MTI -> JRS) - If commented, the following line needs uncommenting for the final version

                'mti 2026/05/13 commented
                ReOrgCodeFromCalls(CodeFileDir$)


            Else

                CodeFileSubDir$ = "src\IFs.Core\Models"
                CodeFileDir$ = StartDirec + CodeFileSubDir$

            End If

            'mti  2025/09/17  start
            'Dim startTime As DateTime = DateTime.Now
            Console.WriteLine(vbCrLf & "Current date and time: " & DateTime.Now.ToString() & vbCrLf & "TmpFile creation  time: " & (DateTime.Now - startTime).ToString & vbCrLf)
            startTime = DateTime.Now
            'Console.WriteLine(vbCrLf & "Current date and time: " & DateTime.Now.ToString() & vbCrLf)   'mti  2025/09/17  start
            'mti  2025/09/17  end


            '--- mti 2025/07/14 end parsing through call sequence

            '----------parse the model code and take them in one array----
            '----Leftside() of eqn., Rightside() of eqn., corresponding codeblock()--

            ModCount = 0
            ModName(ModCount) = "AUTOMOBILE.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "AUTOMOBILE.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "AUTOMOBILE.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + "AUTOMOBILE.FORECAST.VB"

            'mti October - December 2009 -start
            ModCount = ModCount + 1
            ModName(ModCount) = "WATER.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "WATER.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "WATER.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", WATER.FORECAST.VB"
            'mti October - December 2009 -end

            ModCount = ModCount + 1
            ModName(ModCount) = "ENVIRONMENT.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "ENVIRONMENT.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "ENVIRONMENT.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", ENVIRONMENT.FORECAST.VB"


            ModCount = ModCount + 1
            ModName(ModCount) = "SOCIOPOL.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "SOCIOPOL.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "SOCIOPOL.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", SOCIOPOL.FORECAST.VB"

            ModCount = ModCount + 1
            ModName(ModCount) = "ENERGY.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "energy.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "ENERGY.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines
            ModCount = ModCount + 1

            ParsedFile$ = ParsedFile$ + ", ENERGY.FORECAST.VB"

            ModName(ModCount) = "AGRICULTURE.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "agriculture.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "AGRICULTURE.FORECAST.VB")  'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", AGRICULTURE.FORECAST.VB"



            ModCount = ModCount + 1
            ModName(ModCount) = "ECONOMY.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "ECONOMY.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "ECONOMY.FORECAST.VB")    'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            'mti 2026/04/21 start - write all leftsides and rightsides of this module to a CSV
            WriteLeftRightToCsv(Filename + ".CSV", ModStart(ModCount), ModEnd(ModCount))
            'mti 2026/04/21 end

            ParsedFile$ = ParsedFile$ + ", ECONOMY.FORECAST.VB"

            ModCount = ModCount + 1
            ModName(ModCount) = "EDUCATION.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "education.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "EDUCATION.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            'mti 2026/04/21 start - write all leftsides and rightsides of this module to a CSV
            'WriteLeftRightToCsv(Filename + ".CSV", ModStart(ModCount), ModEnd(ModCount))
            'mti 2026/04/21 end

            ParsedFile$ = ParsedFile$ + ", EDUCATION.FORECAST.VB"



            ModCount = ModCount + 1
            ModName(ModCount) = "HEALTH.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "health.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "HEALTH.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            'mti 2026/05/13 start
            'WriteLeftRightToCsv(Filename + ".CSV", ModStart(ModCount), ModEnd(ModCount))
            'mti 2026/05/13 end

            ParsedFile$ = ParsedFile$ + ", HEALTH.FORECAST.VB"

            ModCount = ModCount + 1
            ModName(ModCount) = "POPULATION.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "population.Forecast.vb"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "POPULATION.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", POPULATION.FORECAST.VB"



            'mti Nov 2010 Driver Changes
            ModCount = ModCount + 1
            ModName(ModCount) = "INFRASTRUCTURE.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "INFRASTRUCTURE.FORECAST.VB"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "INFRASTRUCTURE.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            'mti 2026/04/21 start - write all leftsides and rightsides of this module to a CSV
            'WriteLeftRightToCsv(Filename + ".CSV", ModStart(ModCount), ModEnd(ModCount))
            'mti 2026/04/21 end

            ParsedFile$ = ParsedFile$ + ", INFRASTRUCTURE.FORECAST.VB"



            'mti Nov 2010 Driver Changes
            ModCount = ModCount + 1
            ModName(ModCount) = "VALUESDATA.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "VALUESDATA.FORECAST.VB"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "VALUESDATA.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", VALUESDATA.FORECAST.VB"

            'mti Nov 2010 Driver Changes

            ModCount = ModCount + 1
            ModName(ModCount) = "INTLCONF.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "INTLCONF.FORECAST.VB"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "INTLCONF.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", INTLCONF.FORECAST.VB"

            ModCount = ModCount + 1
            ModName(ModCount) = "STFAILURE.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "STFAILURE.FORECAST.VB"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "STFAILURE.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", STFAILURE.FORECAST.VB"

            ModCount = ModCount + 1
            ModName(ModCount) = "TECHNOLOGY.FORECAST.VB"
            ModStart(ModCount) = codelines + 1
            Filename = CodeFileDir + "\" + "TECHNOLOGY.FORECAST.VB"
            Console.WriteLine("IFs - Parsing " + ModName(ModCount))
            Call ParseCodeFile(Filename, "TECHNOLOGY.FORECAST.VB") 'add file into array
            If Filename$ = "Error" Then Exit Sub
            ModEnd(ModCount) = codelines

            ParsedFile$ = ParsedFile$ + ", TECHNOLOGY.FORECAST.VB"

            ModCount = ModCount + 1


            'mti 2025/08/07 Start Dimensionality
            ' Assume LeftSide(), RightSide() are populated and same length
            Dim sLeftSide() As String = Nothing
            Dim sRightSide() As String = Nothing


            'mti 2026/05/13 - if mapping of DimNo in If conditions is needed, this procedure will transform LeftSide and RightSide to handle that, and backup the original strings in sLeftSide and sRightSide
            ApplyIfDimMapsAll(LeftSide, Rightside, sLeftSide, sRightSide)

            ' Now:
            ' - sLeftSide/sRightSide contain backups of the originals
            ' - LeftSide/RightSide contain the transformed strings(handling  DimNo in If condistions)
            'mti 2025/08/07 end Dimensionality 

            Console.WriteLine("Parsed " + CStr(ModCount) + " modules: " + ParsedFile + vbCr + "Will update the variable drivers now. It will take a while.")



            'mti  2025/09/17  start
            'Dim startTime As DateTime = DateTime.Now
            Console.WriteLine(vbCrLf & "Current date and time: " & DateTime.Now.ToString() & vbCrLf & "Parsing Tmpfiles time: " & (DateTime.Now - startTime).ToString & vbCrLf)
            startTime = DateTime.Now
            'mti  2025/09/17  end


            'mti 2025/10/30 start
            'Build dimensions IFsVar table - IFsVarDim 
            'JRS - MTI - this needs uncommenting for final version
            BuildIFsVarDim(PutFdrvRun$ & "IFsVar.db")

            'mti 2025/12/08 Take this line ifyou have not taken it earlier
            'mti 2025/12/03 post-process IFsVarDim table (dimensioned copy of IFsVar) to set IsParameter = Yes/No based on NAME (uppercase,lowercase)
            FlagParametersInIFsVarDim(PutFdrvRun$ & "IFsVar.db")


            Console.WriteLine(vbCrLf & "Current date and time: " & DateTime.Now.ToString() & vbCrLf & "IFsVarDim Creation time: " & (DateTime.Now - startTime).ToString & vbCrLf)
            startTime = DateTime.Now

            'mti 2025/10/30 end

            db1 = New SqliteConnection("DataSource=" & PutFdrvRun$ & "IFsVar.db;") '& "New=true;"
            db1.Open()
            db1Cmd = db1.CreateCommand

            db1Cmd.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
            db1Cmd.ExecuteNonQuery()

            'recreate varlink table

            Console.WriteLine("IFs - Clearing Previously Written Documentation.")
            Call InitVarLink(db1Cmd)


            VFCounter = 0

            CodeArray2Block(0) = 0

            'mti 2025/08/07 dimension
            Dim LeftDim1%, LeftDim2%, LeftDim3%, LeftVarNo%



            'mti 2025/08/07 start For Debugging:
            Dim StartVarNo As Integer = 1
            Dim EndVarNo As Integer = NumbVars * 2  'mti 2025/08/26

            'mti 2026/05/13 
            'mti 2025/08/26  start
            'To go directly to a variable
            'For ICount% = 1 To NumbVars
            '    If V$(ICount%).ToUpper = "INFRAINVESTNEWPUB".ToUpper Then
            '        StartVarNo = (ICount% - 1) * 2 + 1
            '        EndVarNo = StartVarNo + 1
            '        Stop
            '        Exit For
            '    End If
            'Next

            'mti 2025/09/17 testing
            'StartVarNo = 1
            'EndVarNo = 1600
            'End for debugging
            ''mti 2025/08/26   end



            '--------Look for IFSVar variables in the array built above by parsecodefile(s)
            For ICount% = StartVarNo To EndVarNo  'NumbVars * 2  'mti 2025/08/26  ' 1  '2175 To 2176 POP; 2455 VADD; DeathCat = 271
                'mti 2025/08/07 end For Debugging:

                Dim Thoroughbuild = False

                'debugging help

                'mti 2020/06/19 2020 start
                MEnd = codelines
                If ICount% Mod 2 = 1 Then
                    LeftVarNo% = ICount% \ 2 + 1
                    LeftVar$ = V$(LeftVarNo%).ToUpper
                    'If LeftVar$ = "AGLOSSPROD" Then Stop
                Else
                    LeftVarNo% = CInt(ICount% / 2)
                    LeftVar$ = "S" + V$(LeftVarNo%).ToUpper
                    For i2 = 1 To NumbVars
                        If V$(i2).ToUpper = LeftVar$ Then
                            'another variable exists with "S" prefix; e.g., SFDEM and FDEM
                            MEnd = 0  'will skip processing parsed code for this S-prefixed var by setting end lines to 0
                            LeftVarNo% = i2
                            Exit For
                        End If
                    Next i2
                End If
                'mti 2020/06/19 2020 End

                'If IsAllLowercase(V$(LeftVarNo%)) Then Continue For  'mti 2026/04/21 paramters need not be checked on the leftside
                'Stop
                '    Continue For  'mti 2026/04/21 paramters need not be checked on the leftside
                'End If

                'mti 2025/08/27 start
                Dim IsIFsParameter As Boolean = (V$(LeftVarNo%).ToUpper <> V$(LeftVarNo%))
                Dim IsIFsParameter_S = IsIFsParameter AndAlso (LeftVar$ = "S" + V$(LeftVarNo%).ToUpper)
                If IsIFsParameter_S Then Continue For
                'mti 2025/08/27 end 

                LeftDim1 = VDim1(LeftVarNo%)
                LeftDim2 = VDim2(LeftVarNo%)
                LeftDim3 = VDim3(LeftVarNo%)

                'mti 2025/08/07 end


                Console.WriteLine("IFs - Building Drivers For " + LeftVar$)
                'If LeftVar$.Trim = "WOMENINLEADERSHIP" Then Stop

                VCLn = 0
                UVCLn = 0
                Erase CodeArray
                ReDim CodeArray(3000)
                Erase CodeArray2Block
                ReDim CodeArray2Block(3000)


                'Main loop for codelines
                CurModule = ""
                MStart = 1

                'debugging help
                '    If LeftVar$ <> "GOVRISK" Then
                '        MEnd = 0
                '    End If

                'LeftVar$ = "MALNCHP" 'Then Stop


                'mti 2025/08/09 start
                Dim FoundOnLeft As Boolean = False
                CurComment = ""
                'mti 2025/08/09 end





                'If LeftVar$ = "AGLOSSPROD" Then Stop

                'start searching all members of the array (built from parsecode)
                For i2 = MEnd To MStart Step -1 'UBound(LeftSide) - 1
                    '  If i2 = 530 Then Stop

                    'should not terminate this loop even if ifsvar found
                    'even if dimensions done separately

                    'jrs 2023/07/25
                    If LeftSide(i2) = Nothing Then LeftSide(i2) = ""




                    If MatchIFSVarInString(LeftSide(i2), LeftVar$) Then

                        'mti 2026/04/21 start 'Make sure the match is not a a non-dimensioned (likely intermediate variable) for example" C in helath module for smoking computation
                        If LeftDim1 > 0 And LeftSide(i2).IndexOf("(") < 0 Then
                            'Stop
                            Continue For
                        End If
                        'mti 2026/04/21 end

                        'If IsAllLowercase(V$(LeftVarNo%)) Then 'Continue For  'mti 2026/04/21 paramters need not be checked on the leftside
                        '    Stop
                        '    'Continue For  'mti 2026/04/21 paramters need not be checked on the leftside
                        'End If

                        'If i2 = 2430 Then Stop
                        'If i2 = 2432 Then Stop
                        'mti 2025/08/07 start
                        FoundOnLeft = True
                        ' accumulate comments
                        If CurComment = "" Then
                            CurComment = CommentText(i2)
                        Else
                            If CommentText(i2).Trim <> "" Then CurComment = CurComment & "," & CommentText(i2).Trim
                        End If
                        'mti 2025/08/07 end


                        'IFSVar variable found in the left hand side of the i-th equation within the array of equations obtained from parsing all modules
                        'Save the line number (or, array index)
                        Dim LeftVarLineNo = i2

                        'Find out the (.Forecast.vb) module that contains this equation
                        CurModule = ModuleName(i2)



                        'If i2 <= 2570 Then Stop

                        'Find out where this module's equations start and end (within the entire array of equations for all modules combined)
                        'Subsequent search will be within this module only
                        For k2 = 0 To ModCount - 1
                            If ModName(k2).Trim = CurModule.Trim Then
                                MStart = ModStart(k2)
                                MEnd = ModEnd(k2)
                                Exit For
                            End If
                        Next k2

                        'Read the right side of the equation
                        strcode = Rightside(i2)

                        '        If LeftVar = "VADD" Then Stop

                        'take associated codeblock (and block pointer in a sorted array)
                        TakeSorted(CInt(i2), VCLn) 'routine increases VCLn
                        If VCLn < 3000 Then CodeArray(VCLn) = CodeBlock(i2) 'LeftSide(i2) + " = " + Rightside(i2)

                        'Variable names with dimensions
                        LeftVarWithDim = LeftSide(i2)
                        cVarNames1WithDim = cVarnamesWithDims(strcode, LeftVar) 'can return same  variable with different dim, chances are low though
                        'LeftVarWithDim = LeftSide(i2)  'mti 2026/04/21 - code repeated; commenting  
                        'mti 2025/08/07 end


                        RightCnt = 0 'mti 2017/10/19
                        Erase Tempright 'mti 2017/10/19

                        'mti 2025/08/07 start Dimensions
                        'ReDim Tempright(3000, 1)

                        'mti 2026/05/13 start
                        'mti 2026/04/21 start
                        'ReDim Tempright(3000, 2)
                        'ReDim Tempright(3000, 3)
                        ReDim Tempright(3000, 4)
                        'mti 2026/04/21 end
                        'mti 2026/05/13 end


                        'mti 2026/04/21 - various comments added LeftVarWithDim
                        'save variables collected from the rightside of this equation (RightSide(i2)) in a temporary array, Tempright with 3 columns:
                        '1) base variable name without dimension; 2) line number (or array index) of the equation where this right variable is found; 3) variable name with dimension (if any)

                        'For j2 = 0 To cVarNames1.Count - 1
                        For j2 = 0 To cVarNames1WithDim.Count - 1

                            'TmpVar$ = cVarNames1.Item(j2).ToString
                            TmpVar$ = ExtractBaseName(cVarNames1WithDim.Item(j2).ToString)
                            'mti 2025/08/07 End

                            'Avoid taking same variable and the SHIFT Factors (need "S" prefixed variables here?)
                            'jrs 2023/01/04 added TmpVar.Length < LeftVar$.Length + 5 OrElse 
                            'mti 2025/07/09 start  'did this increase time?
                            'If TmpVar$ <> LeftVar$ And (TmpVar.Length < LeftVar$.Length + 5 OrElse TmpVar$.Substring(0, LeftVar$.Length + 5) <> LeftVar$ + "SHIFT") Then  'save right hand side variables and the current codeline pointer in an array
                            If (TmpVar.Length < LeftVar$.Length + 5 OrElse TmpVar$.Substring(0, LeftVar$.Length + 5) <> LeftVar$ + "SHIFT") Then  'save right hand side variables and the current codeline pointer in an array
                                'mti 2025/07/09 end

                                'mti 2025/08/27 start  - added to avoid putting in duplicates 
                                Dim TmpRWD As String = cVarNames1WithDim.Item(j2).ToString
                                Dim IsInTempright As Boolean = False
                                'RightVarList = ParseVarWithDims(TmpRWD, GRPSEnum, VDim1(k2), VDim2(k2), VDim3(k2)) 'k2 unknwon;notneeded here
                                For tri As Integer = 0 To Tempright.GetUpperBound(0)
                                    If Tempright(tri, 2) = TmpRWD Then
                                        IsInTempright = True
                                        Exit For
                                    End If
                                    If String.IsNullOrWhiteSpace(Tempright(tri, 2)) Then Exit For
                                Next


                                'mti 2026/05/13 start - emty strings should not be added to tempright as that causes issue in the loop later when checking if tempright is exhausted
                                If Not IsInTempright AndAlso Not String.IsNullOrWhiteSpace(TmpVar) Then
                                    'If Not IsInTempright Then
                                    'mti 2026/05/13 end
                                    'mti 2025/08/27 end

                                    'mti Nov 2010 Driver Changes - end
                                    Tempright(RightCnt, 0) = TmpVar$
                                    Tempright(RightCnt, 1) = i2.ToString  'pointer to current code line,  containing this rightvariable

                                    'mti 2025/08/07 start Dimensions
                                    Tempright(RightCnt, 2) = cVarNames1WithDim.Item(j2).ToString
                                    'mti 2025/08/07 End

                                    'mti 2026/05/13 start
                                    Tempright(RightCnt, 4) = LeftVarWithDim
                                    'mti 2026/05/13 end

                                    RightCnt = RightCnt + 1 'total number of variables on the temporary array holding the right variables: Tempright
                                    'If RightCnt > 5 Then Stop

                                End If  'mti 2025/08/27
                            End If
                        Next j2
                        'RightCnt = j2

                        'mti 2025/07/14 start
                        'This parsing algorithm block traverses code from current line (where fsVar is found) to all the way up to the beginning of this .forecast.vb
                        'For variables that  uses computationd from previous year, it need to be repeated starting from last line of .forcast.vb  (saved in Leftside/Righhtside) etc to the current line

                        'Proceed to check the rightside variables to see if they are IFs variables or intermediate variables
                        For DirCnt = 1 To 1  '2 mti 2026/04/21 same dir (for infra try only 1; with excluded proc off: see; then think) 'direction counter: current to top;bottom to current

                            l2 = 0

                            Do While True
                                'Read the rightside (of the equation) variable (and it's lineno) one at a time from the temporary array


                                RightVar = Tempright(l2, 0)

                                'mti 2026/05/13 start Moved up
                                If RightVar = "" Then Exit Do  'tempright() exhausted
                                'mti 2026/05/13 end

                                Dim RightVarLineNo = CInt(Tempright(l2, 1))
                                'If LeftVar = "AGM" And RightVar = "AGMM" Then Stop

                                'mti 2025/08/07 start Dimensions
                                RightVarWithDim = Tempright(l2, 2)

                                'mti 2025/08/07 End

                                'mti 2026/05/13 start Moved up
                                'If RightVar = "" Then Exit Do  'tempright() exhausted
                                'mti 2026/05/13 end

                                'mti 2026/05/13 start -If leftside found later while traversing codelines upward has separate sub-dimensions, that needs to be captured now (fixes LabCoeffs -> ylm going to all sectors
                                'If ExtractBaseName(LeftSide(RightVarLineNo).Trim).ToUpper = LeftVar.ToUpper Then LeftVarWithDim = LeftSide(RightVarLineNo).Trim
                                LeftVarWithDim = Tempright(l2, 4).Trim
                                'mti 2026/05/13 end

                                Dim MatchIFSVar = False


                                'mti 2026/04/21 - if same variable is found - that means there is further computation in the back; unless there is run to run error

                                'Check if the right variable is a variable in IFsVar (or a "s" prefixed variable saving previous year's values)
                                For k2 = 1 To NumbVars
                                    'If LeftVar = "AGM" And RightVar = "AGMM" And k2 = 16 Then Stop 'Debug.Print(k2.ToString)
                                    'mti 2020/06/19 2020 start
                                    '                If UCase$(V$(k2)) = UCase$(RightVar) _
                                    '                   Or "S" + UCase$(V$(k2)) = UCase$(RightVar) Then
                                    '
                                    '                    'this distance is supposed to be distance in terms of codelines; it's no good though
                                    '                    Distance% = LeftVarLineNo - RightVarLineNo
                                    '                    AddRow2VarLink valtbl, LeftVar, RightVar, Distance%
                                    '
                                    '                    MatchIFSVar = True
                                    '                    Exit For
                                    '                End If

                                    'mti 2026/04/21 start - keeping casing in var/parameter name comparison
                                    If V$(k2) = RightVar Then
                                        MatchIFSVar = True
                                    ElseIf "S" + V$(k2).ToUpper = RightVar.ToUpper Then
                                        'added check for "S" prefix variable as well as check to avoid matching both FDEM And SFDEM
                                        MatchIFSVar = True
                                        For j2 = 1 To NumbVars
                                            If V$(j2) = RightVar Then  'another variable exists with "S" prefix; e.g., SFDEM and FDEM
                                                MatchIFSVar = False
                                                Exit For
                                            End If
                                        Next j2
                                    End If

                                    'commenting upper case check
                                    'If V$(k2).ToUpper = RightVar.ToUpper Then
                                    '    MatchIFSVar = True
                                    'ElseIf "S" + V$(k2).ToUpper = RightVar.ToUpper Then
                                    '    MatchIFSVar = True
                                    '    For j2 = 1 To NumbVars
                                    '        If V$(j2).ToUpper = RightVar.ToUpper Then  'another variable exists with "S" prefix; e.g., SFDEM and FDEM
                                    '            MatchIFSVar = False
                                    '            Exit For
                                    '        End If
                                    '    Next j2
                                    'End If
                                    'mti 2026/04/21 end

                                    If MatchIFSVar = True Then
                                        'this distance is supposed to be distance in terms of codelines; it's no good though
                                        Dim Distance% = LeftVarLineNo - RightVarLineNo
                                        'If i2 = 13907 Then Stop

                                        'If RightVarLineNo = 4154 Then Stop
                                        'If RightVarLineNo = 4152 Then Stop
                                        CurComment = CommentText(RightVarLineNo)  'mti 2025/09/17 line added to makesure regression functions etc are at  right place


                                        'If RightVarLineNo = 4472 Then Stop


                                        'If RightVar.ToUpper = "WATSAFECOST" Then Stop

                                        'mti 2025/08/07 start
                                        AddRow2VarLink(db1Cmd, LeftVar, RightVar, Distance%, CurComment)  'by ref by default; changed to by val now
                                        'mti 2025/08/07 end

                                        'mti 2026/04/21 comments to check values
                                        'If RightVarWithDim.Contains("INFRAINVESTMAINTPUB(WasteWater)") Then Stop
                                        'If RightVarWithDim.Contains("INFRAINVESTNEWPUB(WasteWater)") Then Stop
                                        'If RightVarWithDim.Contains("WATWASTETREATCOST") Then Stop

                                        'mti 2025/08/07 start dimensionality
                                        RightVarList = ParseVarWithDims(RightVarWithDim, GRPSEnum, VDim1(k2), VDim2(k2), VDim3(k2))
                                        LeftVarList = ParseVarWithDims(LeftVarWithDim, GRPSEnum, LeftDim1, LeftDim2, LeftDim3)
                                        AddRow2VarLinkWithDim(db1Cmd, LeftVarList(0).ToString, LeftVarList(1).ToString, LeftVarList(2).ToString, LeftVarList(3).ToString,
                                                                          RightVarList(0).ToString, RightVarList(1).ToString, RightVarList(2).ToString, RightVarList(3).ToString, Distance%, CurComment)

                                        CurComment = ""
                                        'mti 2025/08/07 end

                                        Exit For
                                    End If
                                    'mti 2020/06/19 2020 End
                                Next k2

                                'mti 2026/04/21 start - if same variable is found - that means there is further computation upward that needs to be traced; unless there is run to run error
                                If MatchIFSVar And LeftVarWithDim.Trim.ToUpper = RightVarWithDim.Trim.ToUpper Then MatchIFSVar = False
                                'mti 2026/04/21 End

                                If Not MatchIFSVar Then
                                    'If LeftVar = "CLPC" Then Stop
                                    'Rightside Variable is not a IFSVar variable: it is an intermediate variable

                                    'Proceed to search for this intermediate variable in other parsed lines starting from current code line backwards (in most cases)


                                    'mti 2025/07/14 start
                                    'This parsing algorithm block traverses code from current line (where fsVar is found) to all the way up to the beginning of this .forecast.vb
                                    'For variables that  uses computation from previous year, it need to be repeated starting from last line of .forcast.vb  (saved in Leftside/Righhtside) etc to the current line

                                    'search only within this module (or till the beginning line of this module)
                                    'mti 2025/07/14 start
                                    If DirCnt = 1 Then
                                        'current line to top 
                                        GoBackUpto = MStart
                                        'If Not Thoroughbuild Then
                                        '    'For most variables start from the line where this equation was found
                                        StartAt = CInt(Tempright(l2, 1)) - 1
                                        'Else
                                        '    'For some variables (e.g., ENP) search allthe equations in this module
                                        '    StartAt = MEnd
                                        'End If

                                    Else
                                        'bottom of module to current line
                                        GoBackUpto = CInt(Tempright(l2, 1)) + 1
                                        StartAt = MEnd
                                    End If
                                    'mti 2025/07/14 End

                                    'If LeftVar = "AGM" And RightVar = "AGMM" Then Stop

                                    'Search for intermediate variable (found in the right side) in other parsed lines of equation
                                    'For k2 = Tempright(l2, 1) - 1 To GoBackUpto Step -1 'MEnd
                                    For k2 = StartAt To GoBackUpto Step -1 'MEnd
                                        'If LeftVar = "NEONATMOR" And RightVar = "CONVTIME" Then Stop

                                        'If k2 = 2430 Then Stop
                                        'If k2 = 2432 Then Stop

                                        If RightVar = "ACTREAON" Then Stop

                                        'mti 2026/04/21 start - was matching non-dimensioned right variable from earlier equation in a new 'equation; changed to match variable with dimension (if any)
                                        'If MatchIFSVarInString(LeftSide(k2), RightVar) Then
                                        If IsNothing(LeftSide(k2)) Then Continue For
                                        If LeftSide(k2).Trim.ToUpper = RightVarWithDim.Trim.ToUpper OrElse DimensionedVarsMatch(LeftSide(k2).Trim.ToUpper, RightVarWithDim.Trim.ToUpper, GRPSEnum) Then

                                            'If LeftSide(k2).Trim.ToUpper = "AGP(MEAT)" Then Stop

                                            Tempright(l2, 3) = k2.ToString  'the line (Acnt) where this right variable is found as a left variable in another equation;
                                            'this will be used to prevent repeated inclusion in Tempright unless needed; added as the 4th column in Tempright array;
                                            'mti 2026/04/21 end

                                            'If k2 = 25948 Then Stop  Deppcnt(Mobile)



                                            'mti 2025/08/07 start

                                            ' accumulate comments
                                            If CurComment = "" Then
                                                CurComment = CommentText(k2)
                                            Else
                                                If CommentText(k2).Trim <> "" Then CurComment = CurComment & "," & CommentText(k2).Trim
                                            End If
                                            'mti 2025/08/07 end

                                            'If LeftSide(k2).Contains("(") Then Stop

                                            'intermediate variable found in leftside:
                                            'take codeblock
                                            TakeSorted(k2, VCLn) 'routine increases VCLn
                                            If VCLn < 3000 Then CodeArray(VCLn) = CodeBlock(k2) 'LeftSide(k2) + " = " + Rightside(k2)

                                            'Parse the rightside of this (other) equation into variables and put them in cVarNames collection
                                            strcode = Rightside(k2)


                                            'mti 2025/08/07 start
                                            'If strcode.Contains(", If e% // 3 AndAlso") Then Stop
                                            'strcode = ReplaceSubDimsFromIfMaps(strcode)
                                            'mti 2025/08/07 end

                                            'If LeftVar = "NEONATMOR" And strcode = "NEONATMOR(R%) - CalNeoNatMor, If hlsmokingstsw  //  0 Then, " Then Stop
                                            'mti 2017/10/19 start
                                            cVarNames1 = cVarnames(strcode, LeftVar)  'LeftSide(k2)
                                            'mti 2017/10/19 End

                                            'mti 2025/08/07 start
                                            cVarNames1WithDim = cVarnamesWithDims(strcode, LeftVar) 'can return same  variable with different dim, chances are low though
                                            'LeftVarWithDim = LeftSide(i2)
                                            'mti 2025/08/07 end

                                            'If cVarNames1.Count =0 Then do nothing

                                            'Check these newly found rightside variables to the ones already held in the temporary array holdinng the rightside variables
                                            'mti 2025/08/07 start
                                            Dim TmpVarWithDim$
                                            'UpLimit = RightCnt + cVarNames1.Count - 1
                                            UpLimit = RightCnt + cVarNames1WithDim.Count - 1
                                            'mti 2025/08/07 end
                                            DownLimit = RightCnt '+ 1
                                            For j2 = DownLimit To UpLimit
                                                'Read the new (or additional) rightside variable
                                                'jrs 2023/01/10 TmpVar$ = cVarNames1.Item(j2 - DownLimit + 1).ToString

                                                'mti 2025/08/07 start

                                                'mti 2025/08/26 start
                                                'TmpVar$ = cVarNames1.Item(j2 - DownLimit).ToString  'uncommented 'mti 2025/08/26

                                                'TmpVar$ = cVarNames1.Item(j2).ToString
                                                TmpVarWithDim$ = cVarNames1WithDim.Item(j2 - DownLimit).ToString
                                                TmpVar$ = ExtractBaseName(TmpVarWithDim$) 'commented 'mti 2025/08/26
                                                'mti 2025/08/07 End

                                                'mti 2025/08/26 end
                                                'mti 2025/09/17 - if an IFsVar variable is found here, like GDPPCP (4154) why it is not matched? (maybe done uptop)

                                                'check if intermediate/ifs variable already listed?

                                                'Check this newfound right variable with the right variables we are already working on
                                                'mti 2017/10/19 start - the whole loop can be bypassed now? - may be we should do that just for LeftVar = POP and GDP

                                                'For m2 = 0 To j2 - 1  'DownLimit - 1   ?
                                                For m2 = l2 + 1 To RightCnt - 1 ' j2 - 1 'DownLimit - 1   'mti 2026/04/21 counter wrong
                                                    'mti 2017/10/19 end

                                                    'mti 2026/04/21 start - fixing check if the newly found right variable is already in the list of right variables (Tempright) we are working on
                                                    If TmpVar.ToUpper <> RightVar.ToUpper Then 'Rightvar is the one we are trying to find in the leftside of other equations tracing backward;

                                                        'If TmpVar$ = Tempright(m2, 0) Then
                                                        If TmpVarWithDim$.ToUpper = Tempright(m2, 2).ToUpper Then  'Tempright (x,0) is base variable name; Tempright(x,2) is variable with dim;
                                                            'need to compare with variable with dim to make sure they are the same; otherwise, may miss some variables with same base name but different dimensions

                                                            'if it is not the one we are checking now, but one that is already in the list of all right variables to check
                                                            'then make sure it is not processed (below) at this time - as it will be processed afterwards
                                                            TmpVar$ = ""
                                                            Exit For   'mti 2025/09/17 - it  needs to be flagged, we can make TmpVar$=""
                                                        End If

                                                    Else
                                                        'if the new right variable is the same as the one we are trying to find, then add to the list so we can trace it trace further upward
                                                        'takes care of x=x+1, includes again
                                                        'If newly found rightside variable is the same rightside variable that we started with do nothing here and process further (below)
                                                        Exit For  'mti 2017/10/19  - this branch (TmpVar = RightVar) is very important: it makes sure that, there is a X = X + Y +.. line and X needs to be tracked further
                                                    End If
                                                    'mti 2026/04/21 end
                                                Next m2

                                                'mti 2026/04/21 start  - l2 is the index of the current rightvar being analyzed from the array of rightvars built gradually
                                                For m2 = 0 To l2 'check if earlier it was taken and it's computation is further up
                                                    If TmpVar.ToUpper <> RightVar.ToUpper Then 'Rightvar is the one we are trying to find in the leftside of other equations tracing backward;
                                                        If TmpVarWithDim$ = Tempright(m2, 2) Then  'Tempright (x,0) is base variable name; Tempright(x,2) is variable with dim;
                                                            'already analyzed rightvar
                                                            If CInt(Tempright(m2, 3)) < k2 Or IsNothing(Tempright(m2, 3)) Then  '< changed to >  'it's already found someplace further up on the left side, already, and put in tempright already
                                                                TmpVar$ = ""
                                                                Exit For  'm2
                                                            Else
                                                                Exit For
                                                            End If

                                                        End If
                                                    Else
                                                        Exit For  'need to look for this on the left side further upward: X = X + Y +.. line and X needs to be tracked
                                                    End If
                                                Next m2
                                                'mti 2026/04/21 end

                                                'mti 2026/05/13 start
                                                If Not String.IsNullOrWhiteSpace(TmpVar) Then
                                                    'If Not TmpVar.Length > 0 Then
                                                    'mti 2026/05/13 end
                                                    'add in the array of rightside driver variables atthelast position (rightcnt) - 'mti  2025/09/27 - comment  added
                                                    Tempright(RightCnt, 0) = TmpVar$ 'cVarNames1.Item(j2 - (DownLimit - 1))
                                                    Tempright(RightCnt, 1) = CStr(k2)  'index of code lines  array; leftside,  rightside
                                                    'mti 2025/08/07 start
                                                    Tempright(RightCnt, 2) = TmpVarWithDim$

                                                    'mti 2026/05/13 start 'saving leftside info, 
                                                    Tempright(RightCnt, 4) = Tempright(l2, 4)  'same as leftside info of the right variable being serached for, i.e., first occurrence of leftvar up top
                                                    'unless a new occurrence of the original left (IFs) variable is found, potentially with a different dim info
                                                    If ExtractBaseName(LeftSide(k2).Trim).ToUpper = LeftVar.ToUpper Then Tempright(RightCnt, 4) = LeftSide(k2).Trim
                                                    'mti 2026/05/13 end

                                                    'mti 2025/08/07 End
                                                    RightCnt = RightCnt + 1  'add variable to the list of right side
                                                End If
                                            Next j2  'j2 is a variable counter for  rightside variables held in array of  all drivers

                                            'mti 2020/06/19 2020 Start
                                            'need to continue here few more lines if structure Instr(trim(CodeBlock(k2))," Case ")
                                            'Exit For 'added 01/04/03 : For k2 = Tempright(l2, 1) To 1 Step -1

                                            'mti 2026/04/21 - the following might not be needed any more; it is from way back; we now face If etc with some algorithm
                                            If Not CodeBlock(k2).Trim.Contains(" Case ") And Not CodeBlock(k2).Trim.Contains(" If ") Then

                                                'mti 2026/05/13 start: will need to continue when dimensions not same
                                                ''e.g., InfraIndTran:: needs to contonue for other dim InfraIndTranComp(Road_Paved)	CSng(Math.Log(INFRAROADPAVEDPCNT(R%))), If IY > 1 Then

                                                'if dimension is different, need to continue to search further up for the same variable with the same dimension;
                                                'otherwise, may miss some variables with same base name but different dimensions
                                                If LeftSide(k2).Trim.ToUpper = RightVarWithDim.ToUpper OrElse DimensionIsCommonIndex(LeftSide(k2).Trim.ToUpper, GRPSEnum) Then Exit For
                                                'Exit For 'added 01/04/03 : For k2 = Tempright(l2, 1) To 1 Step -1

                                                'mti 2026/05/13 end
                                            End If
                                            'mti 2020/06/19 2020 End

                                            'RightCnt = j2
                                        End If  'MatchIFSVarInString(LeftSide(k2), RightVar)


                                    Next k2  'k2 is codeline marker


                                    'mti 2025/07/14 start scroll from bottom to this line.

                                End If 'Not MatchIFSVar

                                l2 = l2 + 1

                                If RightCnt > 1000 Then Exit Do 'increase forlarger modules with repeated inlining?

                            Loop

                            'mti 2025/08/09 start
                            'codeline traversing direction changed, reinitialie comment text(contains TABLFUNC and ANALFUNC etc 
                            CurComment = ""
                            'mti 2025/08/09 end

                        Next DirCnt    'directon counter for the for loop with 2 counters; current line - 1 to top and bottom to current line + 1
                        'mti  2025/07/14 end
                        'This parsing algorithm block traverses code from current line (where fsVar is found) to all the way up to the beginning of this .forecast.vb
                        'For variables that  uses computationd from previous year, it need to be repeated starting from last line of .forcast.vb  (saved in Leftside/Righhtside) etc to the current line
                        'mti 2025/07/14 end block

                    End If 'MatchIFSVarInString(LeftSide(i2)

                    'do not exit for loop here, will miss 2nd of the 2nd sub dim(e.g., crop vs meat)

                Next i2

                'mti 2025/08/09 End  Take this code for writing codelines to IFsVar(?) to a new procedure

                'Add the Code to the Access Table
                If VCLn > 0 Then
                    vbcode = PrepVBCode4IFsVar(LeftVar$, CurModule$, VCLn%)
                    db1Cmd.CommandText = "UPDATE IFsVar SET vbcode = vbcode || """ & vbcode & """ WHERE [NAME] = """ & LeftVar$ & """"
                    db1Cmd.ExecuteNonQuery()
                End If

            Next ICount%



            'Now check right side for those not found on left,especially switches and other parameters
            'EndVarNo = 0  'uncommenting this will skip  right side check 

            'mti 2025/12/16 Victory Day Start
            If StartVarNo = 1 Then
                EndVarNo = NumbVars     'go one pass only on right side
            Else  'may be testing one or more variale
                StartVarNo = 0
                EndVarNo = -1
            End If
            'mti 2025/12/16 Victory Day End

            For ICount% = StartVarNo To EndVarNo  'NumbVars * 2  'mti 2025/08/26    ' 1  '2175 To 2176 POP; 2455 VADD; DeathCat = 271

                Dim FoundOnRight As Boolean = False
                Dim LeftVarOnRightList As List(Of String)
                Dim LeftVarOnRight As String

                Dim IntermediateLeftVarWithDim As String
                Dim IntermediateLeftVarList As List(Of String)
                Dim IntermediateLeftVar As String

                'mti 2025/08/07 start
                CurComment = "" ' XYTABL, ANALFUNC etc should not occur in the next block; ADJSTR -> PID may
                'mti 2025/08/07 end

                Dim Thoroughbuild = False

                'debugging help

                'mti 2020/06/19 2020 start
                MEnd = codelines

                'mti 2025/12/16 Victory Day Start
                LeftVar$ = V$(ICount%)
                If Not IsAllLowercase(LeftVar$) Then Continue For  'work with parameters only

                'If ICount% Mod 2 = 1 Then
                '    LeftVarNo% = ICount% \ 2 + 1
                '    LeftVar$ = V$(LeftVarNo%).ToUpper
                '    'If LeftVar$ = "AGLOSSPROD" Then Stop
                'Else
                '    LeftVarNo% = CInt(ICount% / 2)
                '    LeftVar$ = "S" + V$(LeftVarNo%).ToUpper
                '    For i2 = 1 To NumbVars
                '        If V$(i2).ToUpper = LeftVar$ Then
                '            'another variable exists with "S" prefix; e.g., SFDEM and FDEM
                '            MEnd = 0  'will skip processing parsed code for this S-prefixed var by setting end lines to 0
                '            LeftVarNo% = i2
                '            Exit For
                '        End If
                '    Next i2

                'End If


                ''mti 2025/08/27 start

                'Dim IsIFsParameter As Boolean = (V$(LeftVarNo%).ToUpper <> V$(LeftVarNo%))
                'Dim IsIFsParameter_S As Boolean = IsIFsParameter AndAlso (LeftVar$ = "S" + V$(LeftVarNo%).ToUpper)

                'Dim IsVariable_S As Boolean = (LeftVar$ = "S" + V$(LeftVarNo%).ToUpper) AndAlso Not IsIFsParameter

                'If IsIFsParameter_S Then Continue For
                'If IsVariable_S Then Continue For

                'mti 2025/08/27 end 


                'mti 2025/12/16 Victory Day End


                Dim LeftVar4DBSearch$ = V$(LeftVarNo%).ToUpper

                'mti 2020/06/19 2020 End

                LeftDim1 = VDim1(LeftVarNo%)
                LeftDim2 = VDim2(LeftVarNo%)
                LeftDim3 = VDim3(LeftVarNo%)

                'mti 2025/08/07 end


                Console.WriteLine("IFs - Building Drivers For " + LeftVar$)
                'If LeftVar$.Trim = "WOMENINLEADERSHIP" Then Stop

                VCLn = 0
                UVCLn = 0
                Erase CodeArray
                ReDim CodeArray(3000)
                Erase CodeArray2Block
                ReDim CodeArray2Block(3000)

                'Main loop for codelines
                CurModule = ""
                MStart = 1
                'mti 2025/08/09 start
                Dim FoundOnLeft As Boolean = False

                'mti 2025/12/16 Victory Day Start
                EnsureVarLinkDimIndexes(db1Cmd)
                FoundOnLeft = UsesVariableExists(db1Cmd, LeftVar$)
                'FoundOnLeft = UsesVariableExists(db1Cmd,  LeftVar4DBSearch$)  'mti  2025/08/27    'checks if Variable already exists in VarLink
                'If Not FoundOnLeft Then FoundOnLeft = VariableExists(db1Cmd, LeftVar4DBSearch$)

                'mti 2025/12/16 Victory Day end

                CurComment = ""
                'mti 2025/08/09 end


                If Not FoundOnLeft Then   'AndAlso Not UsesVariableExists(db1Cmd, LeftVar$) Then
                    ' Look for LeftVar on the right-hand side in any line
                    For i2 = MEnd To MStart Step -1

                        'If i2 = 18353 Then Stop
                        'If i2 = 14816 Then Stop

                        If Rightside(i2) Is Nothing OrElse Rightside(i2).Trim.Length = 0 Then Continue For
                        If LeftSide(i2) Is Nothing OrElse LeftSide(i2).Trim.Length = 0 Then Continue For



                        ' build the list with dimensionality from the right-hand side
                        cVarNames1WithDim = cVarnamesWithDims(Rightside(i2))
                        'cVarNames1WithDim = cVarNamesWithDims(ReplaceSubDimsFromIfMaps(Rightside(i2)))

                        For j2 = 0 To cVarNames1WithDim.Count - 1
                            LeftVarWithDim = cVarNames1WithDim.Item(j2).ToString
                            If ExtractBaseName(LeftVarWithDim).Trim.ToUpper = LeftVar.Trim.ToUpper Then
                                ' The variable occurs on the right
                                FoundOnRight = True

                                'mti 2026/04/21 comments to check values
                                'If LeftVarWithDim.Contains("INFRAINVESTMAINTPUB(WasteWater)") Then Stop
                                'If LeftVarWithDim.Contains("INFRAINVESTNEWPUB(WasteWater)") Then Stop
                                'If LeftVarWithDim.Contains("WATWASTETREATCOST") Then Stop

                                LeftVarOnRightList = ParseVarWithDims(LeftVarWithDim, GRPSEnum, LeftDim1, LeftDim2, LeftDim3)
                                LeftVarOnRight = LeftVar
                                Exit For ' mti 2025/08/14
                            End If
                        Next j2

                        If FoundOnRight Then
                            ' seed the intermediate from the left side on this line
                            IntermediateLeftVarWithDim = LeftSide(i2)
                            cVarNames1 = cVarnames(LeftSide(i2))  'need to use  cVarnamesWithDims, and extract base name? okay as is -> need match in V$()
                            IntermediateLeftVar = cVarNames1.Item(0).ToString ' should be only one item on the left

                            'Dim FirstIntermediateLeftVar As String = IntermediateLeftVar 'this will next be searched forward ;not needed -CurrentIntermediateLeftVar willdo

                            Dim FoundInModule As String = ModuleName(i2)

                            ' Identify module boundaries
                            For k2 = 0 To ModCount - 1
                                If ModName(k2).Trim = FoundInModule.Trim Then
                                    MStart = ModStart(k2)
                                    MEnd = ModEnd(k2)
                                    Exit For
                                End If
                            Next k2

                            ' accumulate comments
                            If CurComment = "" Then
                                CurComment = CommentText(i2)
                            Else
                                If CommentText(i2).Trim <> "" Then CurComment = CurComment & "," & CommentText(i2).Trim
                            End If

                            'Check if IntermediateLeftVar is already an IFs variable (direct link)
                            Dim MatchIFSVar As Boolean = False
                            For k2 = 1 To NumbVars
                                If V$(k2).ToUpper = IntermediateLeftVar.ToUpper Then
                                    MatchIFSVar = True
                                    Exit For
                                ElseIf "S" & V$(k2).ToUpper = IntermediateLeftVar.ToUpper Then
                                    MatchIFSVar = True
                                    For jTmp = 1 To NumbVars
                                        If V$(jTmp).ToUpper = IntermediateLeftVar.ToUpper Then
                                            MatchIFSVar = False
                                            Exit For
                                        End If
                                    Next jTmp
                                    If MatchIFSVar Then Exit For
                                End If
                            Next k2

                            If MatchIFSVar Then
                                ' Add direct link: LeftVar → IntermediateLeftVar
                                Dim Distance% = 0
                                AddRow2VarLink(db1Cmd, IntermediateLeftVar, LeftVarOnRight, Distance%, CurComment)

                                IntermediateLeftVarList = ParseVarWithDims(IntermediateLeftVarWithDim, GRPSEnum, VDim1(k2), VDim2(k2), VDim3(k2))

                                AddRow2VarLinkWithDim(db1Cmd,
                                       IntermediateLeftVarList(0).ToString, IntermediateLeftVarList(1).ToString,
                                       IntermediateLeftVarList(2).ToString, IntermediateLeftVarList(3).ToString,
                                       LeftVarOnRightList(0).ToString, LeftVarOnRightList(1).ToString,
                                       LeftVarOnRightList(2).ToString, LeftVarOnRightList(3).ToString,
                                       Distance%, CurComment)
                                CurComment = ""
                                Exit For ' done with this i2
                            End If

                            ' ===== Tracing block (two-phase: build, then drain) =====

                            ' Phase 1: collect unique intermediates (HashSet) and preserve discovery order (List)
                            Dim IntermediateLeftVars As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                            Dim discoveryOrder As New List(Of String)()

                            Dim j2Max = Math.Min(i2 + 1000, MEnd)
                            Dim CurrentIntermediateVar As String = IntermediateLeftVar

                            ' seed entry
                            IntermediateLeftVars.Add(IntermediateLeftVar)
                            discoveryOrder.Add(IntermediateLeftVar)

                            Dim MatchFoundDownstream As Boolean = False
                            Dim lastFoundVar As String = IntermediateLeftVar
                            Dim lastFoundLine As Integer = i2

                            ' -------- Phase 1: build until j2Max --------
                            For j2 = i2 + 1 To j2Max
                                If Rightside(j2) Is Nothing OrElse Rightside(j2).Trim.Length = 0 Then Continue For

                                If ContainsVarRef(Rightside(j2), CurrentIntermediateVar) Then
                                    ' parse left-side var (with and without dims)
                                    IntermediateLeftVarWithDim = LeftSide(j2)
                                    cVarNames1 = cVarnames(LeftSide(j2))
                                    IntermediateLeftVar = cVarNames1.Item(0).ToString

                                    ' accumulate comments
                                    If CurComment = "" Then
                                        CurComment = CommentText(j2)
                                    Else
                                        If CommentText(j2).Trim <> "" Then CurComment = CurComment & "," & CommentText(j2).Trim
                                    End If

                                    ' If this left-side is an IFs var, write links and stop Phase 1
                                    For k2 = 1 To NumbVars
                                        If MatchIFSVarInString(IntermediateLeftVar, V$(k2)) Then
                                            IntermediateLeftVar = V$(k2)
                                            Dim Distance% = j2 - i2

                                            AddRow2VarLink(db1Cmd, IntermediateLeftVar, LeftVarOnRight, Distance%, CurComment)

                                            IntermediateLeftVarList = ParseVarWithDims(IntermediateLeftVarWithDim, GRPSEnum, VDim1(k2), VDim2(k2), VDim3(k2))
                                            AddRow2VarLinkWithDim(db1Cmd,
                                                   IntermediateLeftVarList(0).ToString, IntermediateLeftVarList(1).ToString,
                                                   IntermediateLeftVarList(2).ToString, IntermediateLeftVarList(3).ToString,
                                                   LeftVarOnRightList(0).ToString, LeftVarOnRightList(1).ToString,
                                                   LeftVarOnRightList(2).ToString, LeftVarOnRightList(3).ToString,
                                                   Distance%, CurComment)
                                            CurComment = ""
                                            MatchFoundDownstream = True
                                            Exit For
                                        End If
                                    Next k2

                                    If MatchFoundDownstream Then
                                        Exit For ' Done tracing in Phase 1
                                    Else
                                        ' keep following the chain; add only if new (HashSet)
                                        CurrentIntermediateVar = IntermediateLeftVar
                                        If IntermediateLeftVars.Add(CurrentIntermediateVar) Then
                                            discoveryOrder.Add(CurrentIntermediateVar) ' preserve order
                                        End If
                                        ' regardless of dedupe, update "last seen" markers when we see it on the left
                                        lastFoundVar = CurrentIntermediateVar
                                        lastFoundLine = j2
                                    End If
                                End If
                            Next j2

                            If Not MatchFoundDownstream Then
                                ' -------- Phase 2: drain (NO new additions) --------
                                ' Start from just after the line where the LAST member was found on the left
                                Dim startLine As Integer = Math.Min(lastFoundLine + 1, j2Max)
                                Dim j2End As Integer = j2Max

                                ' Build stack from all BUT the last discovered item (since the last was already checked)
                                If discoveryOrder.Count > 1 Then
                                    Dim initialList As List(Of String) = discoveryOrder.GetRange(0, discoveryOrder.Count - 1)
                                    Dim varStack As New Stack(Of String)(initialList) ' top = last of initialList

                                    While varStack.Count > 0
                                        Dim seedVar As String = varStack.Pop()
                                        Dim followVar As String = seedVar
                                        Dim localMatch As Boolean = False

                                        For j2 = startLine To j2End
                                            If Rightside(j2) Is Nothing OrElse Rightside(j2).Trim.Length = 0 Then Continue For

                                            If ContainsVarRef(Rightside(j2), followVar) Then
                                                ' parse left-side var  
                                                IntermediateLeftVarWithDim = LeftSide(j2)
                                                cVarNames1 = cVarnames(LeftSide(j2))
                                                IntermediateLeftVar = cVarNames1.Item(0).ToString

                                                '' accumulate comments
                                                'If CurComment = "" Then
                                                '    CurComment = CommentText(j2)
                                                'Else
                                                '    CurComment = CurComment & "," & CommentText(j2)
                                                'End If

                                                ' accumulate comments
                                                If CurComment = "" Then
                                                    CurComment = CommentText(j2)
                                                Else
                                                    If CommentText(j2).Trim <> "" Then CurComment = CurComment & "," & CommentText(j2).Trim
                                                End If

                                                ' If IFs var, write links
                                                For k2 = 1 To NumbVars
                                                    If MatchIFSVarInString(IntermediateLeftVar, V$(k2)) Then
                                                        IntermediateLeftVar = V$(k2)
                                                        Dim Distance% = j2 - i2

                                                        AddRow2VarLink(db1Cmd, IntermediateLeftVar, LeftVarOnRight, Distance%, CurComment)

                                                        IntermediateLeftVarList = ParseVarWithDims(IntermediateLeftVarWithDim, GRPSEnum, VDim1(k2), VDim2(k2), VDim3(k2))
                                                        AddRow2VarLinkWithDim(db1Cmd,
                                                               IntermediateLeftVarList(0).ToString, IntermediateLeftVarList(1).ToString,
                                                               IntermediateLeftVarList(2).ToString, IntermediateLeftVarList(3).ToString,
                                                               LeftVarOnRightList(0).ToString, LeftVarOnRightList(1).ToString,
                                                               LeftVarOnRightList(2).ToString, LeftVarOnRightList(3).ToString,
                                                               Distance%, CurComment)
                                                        CurComment = ""
                                                        localMatch = True
                                                        MatchFoundDownstream = True
                                                        Exit For
                                                    End If
                                                Next k2

                                                If localMatch Then
                                                    Exit For
                                                Else
                                                    ' keep following chain inline, no additions
                                                    followVar = IntermediateLeftVar
                                                End If
                                            End If
                                        Next j2
                                        If localMatch Then Exit While
                                    End While
                                End If   'discoveryOrder.Count > 1 
                            End If  'NotMatchFoundStream

                            'FoundOnLeft = UsesVariableExists(db1Cmd, LeftVar4DBSearch$)

                            'mti 2025/08/08 start
                            'Try traversing  backward Scroll from line found to top
                            If Not MatchFoundDownstream Then
                                Dim MatchFound As Boolean
                                For j2 = i2 - 1 To MStart Step -1
                                    If Rightside(j2) Is Nothing OrElse Rightside(j2).Trim.Length = 0 Then Continue For
                                    'If j2 = 4388 Then Stop
                                    'If MatchIFSVarInString(Rightside(j2), CurrentIntermediateVar) Then
                                    'If Rightside(j2).Contains(CurrentIntermediateVar) Then
                                    If ContainsVarRef(Rightside(j2), CurrentIntermediateVar) Then

                                        IntermediateLeftVarWithDim = LeftSide(j2)
                                        cVarNames1 = cVarnames(LeftSide(j2))
                                        IntermediateLeftVar = cVarNames1.Item(0).ToString 'should be only one itemon the left 

                                        If IntermediateLeftVars.Contains(IntermediateLeftVar) Then Continue For

                                        'mti 2025/08/07 start
                                        If CurComment = "" Then
                                            CurComment = CommentText(j2)
                                        Else
                                            CurComment = CurComment + "," + CommentText(j2)
                                        End If
                                        'mti 2025/08/07 end

                                        'MatchFound = False
                                        'note mti: need to be collecting codelines to code block 
                                        ' Check if left side is an IFs variable
                                        For k2 = 1 To NumbVars
                                            If MatchIFSVarInString(IntermediateLeftVar, V$(k2)) Then
                                                IntermediateLeftVar = V$(k2)
                                                Dim Distance% = j2 - i2
                                                'AddRow2VarLink(db1Cmd, LeftVar, DownstreamVar, Distance%)
                                                'mti 2025/08/07 start
                                                AddRow2VarLink(db1Cmd, IntermediateLeftVar, LeftVarOnRight, Distance%, CurComment) 'the IFs variable being searched(LeftVar) is found on the right side of the equation
                                                IntermediateLeftVarList = ParseVarWithDims(IntermediateLeftVarWithDim, GRPSEnum, VDim1(k2), VDim2(k2), VDim3(k2))
                                                AddRow2VarLinkWithDim(db1Cmd, IntermediateLeftVarList(0).ToString, IntermediateLeftVarList(1).ToString, IntermediateLeftVarList(2).ToString, IntermediateLeftVarList(3).ToString,
                                                            LeftVarOnRightList(0).ToString, LeftVarOnRightList(1).ToString, LeftVarOnRightList(2).ToString, LeftVarOnRightList(3).ToString, Distance%, CurComment)
                                                CurComment = ""
                                                'mti 2025/08/07 end
                                                MatchFound = True
                                                Exit For
                                            End If
                                        Next k2

                                        If MatchFound Then
                                            Exit For ' Done tracing
                                        Else
                                            ' Keep following the chain
                                            CurrentIntermediateVar = IntermediateLeftVar ' LeftSide(j2)
                                            IntermediateLeftVars.Add(CurrentIntermediateVar)  'check
                                        End If
                                    End If
                                Next j2


                            End If  'FoundRight

                            Exit For ' finished processing this i2 (either direct link or tracing done)
                        End If

                    Next i2
                End If
                'revision of entire block above->FoundOnLeft = False 2025/08/14 end of new block
                'mti 2025/08/09 end


                'Driver Code Changes June 2003


                ''mti 2025/08/09 start  Take this code for writing codelines to IFsVar(?) to a new procedure
                'Write Code for driver: code is displayed at 'View Code' Menu

                'CodeArray(VCLn + 1) = "" 'insert a dummy blank, for loop convenience
                'CodeArray2Block(VCLn + 1) = 5001

                ''CodeArray(VCLn + 1) = 5001 'dummy upper limit
                'vbcode = ""

                'vbcode = vbcode + "'----Module " + CurModule + " ---------" + vbCr + vbCr


                ''The following are the arrays used to complete unfinished structures
                ''unfinished structures arise when structure > 10 lines
                ''a marker saying complete structure at the designated line is added to the startblock
                ''please see handlestructure routine
                'Erase TStructNo
                'ReDim TStructNo(20)
                'Erase TStruct
                'ReDim TStruct(20)

                ''If LeftVar$.Trim = "AGDEM" Then Stop
                ''If LeftVar$ = "AGLOSSTRANS" Then Stop

                'CodeArray2Block(0) = 0
                'UnfinishedNo = 0
                'For i2 = 1 To VCLn ' To 1 Step -1

                '    'mti2025/07/14 start
                '    If CodeArray2Block(i2) = 0 Then Continue For
                '    'mti2025/07/14 end


                '    AddCode = True
                '    If CodeBlock(CodeArray2Block(i2)) = CodeBlock(CodeArray2Block(i2 - 1)) Then
                '        AddCode = False
                '    End If

                '    If AddCode Then
                '        'COMPLETE STRUCTURE:    Next tar%:477|COMPLETE STRUCTURE:        Next act%:477|


                '        TCBlock = CodeBlock(CodeArray2Block(i2))

                '        pos1 = 0
                '        'jrs 2023/01/10
                '        If Not (TCBlock Is Nothing) Then
                '            pos = TCBlock.IndexOf("COMPLETE STRUCTURE:")

                '            Do While pos >= 0
                '                UnfinishedNo = UnfinishedNo + 1
                '                pos = TCBlock.IndexOf(":", pos + 1)
                '                pos1 = TCBlock.IndexOf(":", pos + 1)
                '                TStruct(UnfinishedNo) = TCBlock.Substring(pos + 1, pos1 - pos - 1)  'identifies the structure that needs completion
                '                pos = TCBlock.IndexOf("|", pos1) '"|"=End of marker |,will contain line number where thestruct will end
                '                TStructNo(UnfinishedNo) = CInt(TCBlock.Substring(pos1 + 1, pos - pos1 - 1).Trim)  'gives the line number of end of structure


                '                pos1 = pos 'save end of marker

                '                pos = TCBlock.IndexOf("COMPLETE STRUCTURE:", pos + 1)
                '            Loop

                '            'discard complete structure marker , if any
                '            'jrs 2023/07/26
                '            If CodeBlock(CodeArray2Block(i2)) > "" Then
                '                vbcode = vbcode + CodeBlock(CodeArray2Block(i2)).Substring(pos1 + 1) + vbCr '+ vbCr
                '            Else
                '                vbcode = vbcode + vbCr '+ vbCr
                '            End If


                '            Do While UnfinishedNo > 0
                '                If CodeArray2Block(i2) >= TStructNo(UnfinishedNo) Then
                '                    vbcode = vbcode + vbCr + TStruct(UnfinishedNo)
                '                    UnfinishedNo = UnfinishedNo - 1
                '                Else
                '                    Exit Do
                '                End If
                '            Loop
                '        End If

                '    End If

                'Next i2

                'Do While UnfinishedNo > 0
                '    vbcode = vbcode + vbCr + TStruct(UnfinishedNo)
                '    UnfinishedNo = UnfinishedNo - 1
                'Loop

                'mti 2025/08/09 End  Take this code for writing codelines to IFsVar(?) to a new procedure

                'Add the Code to the Access Table
                If VCLn > 0 Then
                    vbcode = PrepVBCode4IFsVar(LeftVar$, CurModule$, VCLn%)
                    db1Cmd.CommandText = "UPDATE IFsVar SET vbcode = vbcode || """ & vbcode & """ WHERE [NAME] = """ & LeftVar$ & """"
                    db1Cmd.ExecuteNonQuery()
                End If




            Next ICount%

            'mti 2026/05/13 start
            Console.WriteLine("=================================")
            'VarLinkDim has been filled, now we consoidate male and female
            Dim VarLinkDimRows As Long = CountVarLinkDimRows(db1)
            Console.WriteLine("IFs - VarLinkDim: " & VarLinkDimRows & " rows written") ' now going to consolidate pairs")
            Console.WriteLine("=================================")
            'ConsolidateMaleFemaleVarLinkDim(db1)

            'VarLinkDimRows = CountVarLinkDimRows(db1)
            'Console.WriteLine("IFs - VarLinkDim: " & VarLinkDimRows & " rows remain after consolidattion of Male and Female")

            'ConsolidateStandardPairsInVarLinkAndIFsVarDim(db1)
            'mti 2026/05/13 end


            Console.WriteLine("IFs - Preparing to copy data file from run folder to Data folder")
            'valtbl1.Update
            db1.Close()

            'mti  2025/09/17  start
            'Dim startTime As DateTime = DateTime.Now
            Console.WriteLine(vbCrLf & "Current date and time: " & DateTime.Now.ToString() & vbCrLf & "Variables Parsing Time: " & (DateTime.Now - startTime).ToString & vbCrLf)
            'startTime = DateTime.Now
            'mti  2025/09/17  end

            'Console.WriteLine("Variabled Done: Will Merge Varlink" & vbCrLf & "Current date and time: " & DateTime.Now.ToString() & vbCrLf)   'mti  2025/09/17  start

            'mti 2025/10/30 start
            'mti 2025/08/07 start
            'MergeVarLink()
            MergeManualEntries()
            'mti 2025/08/07 end
            'mti 2025/10/30 End

            'msgbox here

            '    InFile$ = DataFileDir$ + "IFsVar.mdb"
            '    OutFile$ = PutFdrvRun$ + "IFsVar.mdb"

            'mti 2020/06/19 2020 start
            '    Set db1 = OpenDatabase(DataFileDir$ + "IFsVar.mdb")
            '    Set ValTbl1 = db1.OpenRecordset("IFSVar")
            '    'recreate varlink table
            '    Call InitVarLink(db1, ValTbl1)
            '    'Set valtbl = db1.OpenRecordset("VarLink")

            'Screen.MousePointer = 0

            'If MsgBox("Copy documentation from work to data folder?", vbYesNo) = vbYes Then
            'Screen.MousePointer = 11



            Console.WriteLine("IFs - Copying data file from run folder to Data folder")

            'jrs 2022/12/23 Just copy the file instead
            FileCopy(PutFdrvRun$ + "IFsVar.db", PutDataFileDir + "IFsVar.db")

            'Set db1 = OpenDatabase(DataFileDir$ + "IFsVar.mdb")
            'Set ValTbl1 = db1.OpenRecordset("IFSVar")
            ''recreate varlink table
            'Call InitVarLink(db1, ValTbl1)



            '            qsql = " INSERT INTO " + "VarLink" +
            '     " (Variable, UsesVariable, Distance, Code) " +
            '     "SELECT Variable, UsesVariable, Distance, Code From " _
            '     + "VarLink In '" _
            '     + PutFdrvRun$ + "IFsVar.mdb" + "';"
            '            'mti Nov 2010 Driver Changes -end

            '            db1.Execute qsql   'db = IfsHistSeries.mdb




            'Set db2 = OpenDatabase(PutFdrvRun$ + "IFsVar.mdb")
            ''debug the following line: change db1 to db2
            'Set valtbl = db2.OpenRecordset("IFSVar")


            'With ValTbl1
            '                .MoveFirst
            '                valtbl.MoveFirst
            '                Do While Not .EOF
            '                    'If UCase(valtbl.Fields("name").Value) = "AGDEM" Then Stop
            '                    .Edit
            '                    !vbcode = valtbl.Fields("VBCODE").value
            '                    .Update
            '                    .MoveNext
            '                    valtbl.MoveNext
            '                Loop
            '                '.Index = "NAME"
            '            End With

            '            'Close the files in the \RunFiles Folder
            '            valtbl.Close()
            '            db2.Close()

            'Set db2 = Nothing
            ''Screen.MousePointer = 0

            ''Now close the files in the \Data Folder
            'ValTbl1.Close()
            '            db1.Close()

            Console.WriteLine("Build of documentation is complete." + vbCr + "IFsVar.db is copied from Runfiles to Data.")

            'Else  'didn't save data in the data folder
            '                MsgBox "Build of documentation is complete." + vbCr + "IFsVar.Mdb is NOT copied from Runfiles to Data.", vbOKOnly, "IFs Documentation"
            'End If

            'ValTbl1.Close
            'db1.Close
            'Set db1 = Nothing

            'MsgBox "Build of documentation is complete." + vbCr + "IFsVar.Mdb is copied from Runfiles to Data.", vbOKOnly, "IFs Documentation"
            'mti 2020/06/19 2020 End

            'Exit Sub
            'exit sub
            'Debug.Print Err.Description, Err.Number, Err.Source, Err.HelpFile
            'For l2 = 1 To 2999
            '   Debug.Print Tempright(l2, 0)
            'Next l2
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Stop

            'restorepointer:
            'Me.Caption = sFormTitle$
            'Screen.MousePointer = 0

        End Try

    End Sub
    '============Consolidation Generic by Pair ==============
    'mti 2026/05/13 start




    'mti 2026/05/13
    Private Class VarLinkDimRow
        Public Property RowId As Long
        Public Property Variable As String
        Public Property UsesVariable As String
        Public Property Distance As Object
    End Class

    'mti 2026/05/13
    Private Class NormalizedLinkRow
        Public Property Variable As String
        Public Property UsesVariable As String
        Public Property Distance As Object
    End Class

    'mti 2026/05/13
    Private Class IFsVarDimRow
        Public Property RowId As Long
        Public Property Name As String
        Public Property Definition As String
    End Class

    'mti 2026/05/13
    Private Class VarLinkPairEditPlan

        Public Sub New()
            LinksToInsert = New List(Of NormalizedLinkRow)()
            RowIdsToDelete = New HashSet(Of Long)()
            OriginalToNormalizedNames = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            VarLinkDimPairRemovedNames = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End Sub

        Public Property LinksToInsert As List(Of NormalizedLinkRow)
        Public Property RowIdsToDelete As HashSet(Of Long)

        'mti 2026/05/13
        'Includes original names changed on either the Variable side or UsesVariable side.
        Public Property OriginalToNormalizedNames As Dictionary(Of String, String)

        Public Property VarLinkDimPairRemovedNames As SortedSet(Of String)

        Public Property GroupsConsolidated As Integer

    End Class

    'mti 2026/05/13
    Private Class ConsolidationResult

        Public Sub New()
            VarLinkDimPairRemovedNames = New List(Of String)()
            IFsVarDimEditedRowNames = New List(Of String)()
            IFsVarDimDeletedRowNames = New List(Of String)()
            IFsVarDimSkippedStillUsedNames = New List(Of String)()
        End Sub

        Public Property PairValue1 As String
        Public Property PairValue2 As String

        Public Property VarLinkDimRowsBefore As Integer
        Public Property VarLinkDimRowsAfter As Integer

        Public Property VariableSideGroupsConsolidated As Integer
        Public Property UsesVariableSideGroupsConsolidated As Integer

        Public Property RowsInserted As Integer
        Public Property RowsDeleted As Integer
        Public Property DuplicatesRemoved As Integer

        Public Property VarLinkDimPairRemovedNames As List(Of String)

        Public Property IFsVarDimRowsUpdated As Integer
        Public Property IFsVarDimRowsDeleted As Integer
        Public Property IFsVarDimRowsSkippedStillUsedInVarLinkDim As Integer

        Public Property IFsVarDimEditedRowNames As List(Of String)
        Public Property IFsVarDimDeletedRowNames As List(Of String)
        Public Property IFsVarDimSkippedStillUsedNames As List(Of String)

    End Class

    'mti 2026/05/13
    Private Class IFsVarDimConsolidationResult

        Public Sub New()
            EditedRowNames = New List(Of String)()
            DeletedRowNames = New List(Of String)()
            SkippedStillUsedNames = New List(Of String)()
        End Sub

        Public Property RowsUpdated As Integer
        Public Property RowsDeleted As Integer
        Public Property RowsSkippedStillUsedInVarLinkDim As Integer

        Public Property EditedRowNames As List(Of String)
        Public Property DeletedRowNames As List(Of String)
        Public Property SkippedStillUsedNames As List(Of String)

    End Class

    'mti 2026/05/13
    Public Sub ConsolidateStandardPairsInVarLinkAndIFsVarDim(ByVal conn As SqliteConnection)

        ConsolidateAndPrintOnePair(conn, "Male", "Female")
        ConsolidateAndPrintOnePair(conn, "Skilled", "Unskilled")

    End Sub

    'mti 2026/05/13
    Private Sub ConsolidateAndPrintOnePair(
        ByVal conn As SqliteConnection,
        ByVal pairValue1 As String,
        ByVal pairValue2 As String
    )

        Dim result As ConsolidationResult =
            ConsolidateCommonSubDimensionPair(
                conn,
                pairValue1,
                pairValue2,
                "VarLinkDim",
                "IFsVarDim",
                True
            )

        PrintConsolidationResult(result)

    End Sub

    'mti 2026/05/13
    Private Function ConsolidateCommonSubDimensionPair(
        ByVal conn As SqliteConnection,
        ByVal pairValue1 As String,
        ByVal pairValue2 As String,
        Optional ByVal varLinkDimTableName As String = "VarLinkDim",
        Optional ByVal ifsVarDimTableName As String = "IFsVarDim",
        Optional ByVal compareDistance As Boolean = True
    ) As ConsolidationResult

        If conn Is Nothing Then Throw New ArgumentNullException(NameOf(conn))
        If conn.State <> Global.System.Data.ConnectionState.Open Then conn.Open()

        Dim result As New ConsolidationResult()
        result.PairValue1 = pairValue1
        result.PairValue2 = pairValue2

        Using tx As SqliteTransaction = conn.BeginTransaction()

            result.VarLinkDimRowsBefore = CountRows(conn, tx, varLinkDimTableName)

            'mti 2026/05/13
            'Pass 1: consolidate pair values on the Variable side.
            Dim rowsBeforeVariablePass As List(Of VarLinkDimRow) =
                LoadVarLinkDimRows(conn, tx, varLinkDimTableName)

            Dim variableSidePlan As VarLinkPairEditPlan =
                BuildVariableSideEditPlan(
                    rowsBeforeVariablePass,
                    pairValue1,
                    pairValue2,
                    compareDistance
                )

            ApplyVarLinkEditPlan(
                conn,
                tx,
                varLinkDimTableName,
                variableSidePlan,
                result
            )

            result.VariableSideGroupsConsolidated = variableSidePlan.GroupsConsolidated

            result.DuplicatesRemoved +=
                DeleteDuplicateVarLinkDimRows(conn, tx, varLinkDimTableName)

            'mti 2026/05/13
            'Pass 2: re-read and consolidate pair values on the UsesVariable side.
            Dim rowsBeforeUsesVariablePass As List(Of VarLinkDimRow) =
                LoadVarLinkDimRows(conn, tx, varLinkDimTableName)

            Dim usesVariableSidePlan As VarLinkPairEditPlan =
                BuildUsesVariableSideEditPlan(
                    rowsBeforeUsesVariablePass,
                    pairValue1,
                    pairValue2,
                    compareDistance
                )

            ApplyVarLinkEditPlan(
                conn,
                tx,
                varLinkDimTableName,
                usesVariableSidePlan,
                result
            )

            result.UsesVariableSideGroupsConsolidated = usesVariableSidePlan.GroupsConsolidated

            result.DuplicatesRemoved +=
                DeleteDuplicateVarLinkDimRows(conn, tx, varLinkDimTableName)

            'mti 2026/05/13
            'Combine mappings from both passes for IFsVarDim consistency.
            Dim combinedOriginalToNormalized As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

            MergeNameMappings(combinedOriginalToNormalized, variableSidePlan.OriginalToNormalizedNames)
            MergeNameMappings(combinedOriginalToNormalized, usesVariableSidePlan.OriginalToNormalizedNames)

            For Each s As String In variableSidePlan.VarLinkDimPairRemovedNames
                result.VarLinkDimPairRemovedNames.Add(s)
            Next

            For Each s As String In usesVariableSidePlan.VarLinkDimPairRemovedNames
                result.VarLinkDimPairRemovedNames.Add(s)
            Next

            result.VarLinkDimPairRemovedNames =
                result.VarLinkDimPairRemovedNames.
                Distinct(StringComparer.OrdinalIgnoreCase).
                OrderBy(Function(x) x, StringComparer.OrdinalIgnoreCase).
                ToList()

            'mti 2026/05/13
            'Final consistency step for IFsVarDim after both VarLinkDim passes.
            Dim finalVarLinkRows As List(Of VarLinkDimRow) =
                LoadVarLinkDimRows(conn, tx, varLinkDimTableName)

            Dim ifsResult As IFsVarDimConsolidationResult =
                MirrorVarLinkEditsIntoIFsVarDim(
                    conn,
                    tx,
                    ifsVarDimTableName,
                    pairValue1,
                    pairValue2,
                    combinedOriginalToNormalized,
                    finalVarLinkRows
                )

            result.IFsVarDimRowsUpdated = ifsResult.RowsUpdated
            result.IFsVarDimRowsDeleted = ifsResult.RowsDeleted
            result.IFsVarDimRowsSkippedStillUsedInVarLinkDim =
                ifsResult.RowsSkippedStillUsedInVarLinkDim

            result.IFsVarDimEditedRowNames = ifsResult.EditedRowNames
            result.IFsVarDimDeletedRowNames = ifsResult.DeletedRowNames
            result.IFsVarDimSkippedStillUsedNames = ifsResult.SkippedStillUsedNames

            result.VarLinkDimRowsAfter = CountRows(conn, tx, varLinkDimTableName)

            tx.Commit()

        End Using

        Return result

    End Function
    'mti 2026/05/13
    Private Function BuildVariableSideEditPlan(
    ByVal allRows As List(Of VarLinkDimRow),
    ByVal pairValue1 As String,
    ByVal pairValue2 As String,
    ByVal compareDistance As Boolean
) As VarLinkPairEditPlan

        Dim plan As New VarLinkPairEditPlan()

        'mti 2026/05/13
        'Consolidate when the pair appears in the Variable column.
        '
        'Old exact-match rule:
        '   X(Male)   -> Z
        '   X(Female) -> Z
        'becomes:
        '   X -> Z
        '
        'New same-pair rule:
        '   X(Male)   -> Y(Male)
        '   X(Female) -> Y(Female)
        'becomes:
        '   X -> Y
        '
        'New cross-pair rule:
        '   X(Male)   -> Y(Female)
        '   X(Female) -> Y(Male)
        'becomes:
        '   X -> Y

        Dim candidateGroups =
        allRows.
        Where(Function(r)
                  Return TextContainsExactSubDim(r.Variable, pairValue1) OrElse
                         TextContainsExactSubDim(r.Variable, pairValue2)
              End Function).
        GroupBy(Function(r)
                    Return NormalizeForCompare(
                        NormalizeCommonSubDim(
                            r.Variable,
                            pairValue1,
                            pairValue2
                        )
                    )
                End Function,
                StringComparer.OrdinalIgnoreCase)

        For Each grp In candidateGroups

            Dim side1Rows As List(Of VarLinkDimRow) =
            grp.Where(Function(r) RowBelongsToPair(r.Variable, pairValue1, pairValue2) = 1).ToList()

            Dim side2Rows As List(Of VarLinkDimRow) =
            grp.Where(Function(r) RowBelongsToPair(r.Variable, pairValue1, pairValue2) = 2).ToList()

            If side1Rows.Count = 0 OrElse side2Rows.Count = 0 Then Continue For

            Dim consolidationMode As String =
            GetPairConsolidationMode(
                side1Rows,
                side2Rows,
                Function(r) r.Variable,
                Function(r) r.UsesVariable,
                pairValue1,
                pairValue2,
                compareDistance
            )

            If consolidationMode.Length = 0 Then Continue For

            Dim normalizedRows As New Dictionary(Of String, NormalizedLinkRow)(StringComparer.OrdinalIgnoreCase)

            For Each srcRow As VarLinkDimRow In side1Rows.Concat(side2Rows)

                Dim normalizedVariable As String =
                NormalizeCommonSubDim(srcRow.Variable, pairValue1, pairValue2)

                Dim newUsesVariable As String

                If consolidationMode = "PAIRWISE" Then

                    'mti 2026/05/13
                    'Pairwise same/cross match:
                    'remove the pair from UsesVariable too.
                    newUsesVariable =
                    NormalizeCommonSubDim(srcRow.UsesVariable, pairValue1, pairValue2)

                Else

                    'mti 2026/05/13
                    'Exact-match mode:
                    'keep UsesVariable exactly as it was.
                    newUsesVariable = srcRow.UsesVariable

                End If

                plan.RowIdsToDelete.Add(srcRow.RowId)

                AddTouchedNameToPlan(
                plan,
                "Variable",
                srcRow.Variable,
                normalizedVariable,
                pairValue1,
                pairValue2
            )

                If consolidationMode = "PAIRWISE" Then

                    AddTouchedNameToPlan(
                    plan,
                    "UsesVariable",
                    srcRow.UsesVariable,
                    newUsesVariable,
                    pairValue1,
                    pairValue2
                )

                End If

                AddNormalizedRow(
                normalizedRows,
                normalizedVariable,
                newUsesVariable,
                srcRow.Distance
            )

            Next

            For Each nr As NormalizedLinkRow In normalizedRows.Values
                plan.LinksToInsert.Add(nr)
            Next

            plan.GroupsConsolidated += 1

        Next

        Return plan

    End Function
    'mti 2026/05/13
    Private Function BuildVariableSideEditPlanCor(
        ByVal allRows As List(Of VarLinkDimRow),
        ByVal pairValue1 As String,
        ByVal pairValue2 As String,
        ByVal compareDistance As Boolean
    ) As VarLinkPairEditPlan

        Dim plan As New VarLinkPairEditPlan()

        Dim candidateGroups =
            allRows.
            Where(Function(r)
                      Return TextContainsExactSubDim(r.Variable, pairValue1) OrElse
                             TextContainsExactSubDim(r.Variable, pairValue2)
                  End Function).
            GroupBy(Function(r) NormalizeCommonSubDim(r.Variable, pairValue1, pairValue2),
                    StringComparer.OrdinalIgnoreCase)

        For Each grp In candidateGroups

            Dim side1Rows As List(Of VarLinkDimRow) =
                grp.Where(Function(r) RowBelongsToPair(r.Variable, pairValue1, pairValue2) = 1).ToList()

            Dim side2Rows As List(Of VarLinkDimRow) =
                grp.Where(Function(r) RowBelongsToPair(r.Variable, pairValue1, pairValue2) = 2).ToList()

            If side1Rows.Count = 0 OrElse side2Rows.Count = 0 Then Continue For

            Dim sig1 As String =
                BuildNormalizedUsesSignature(side1Rows, pairValue1, pairValue2, compareDistance)

            Dim sig2 As String =
                BuildNormalizedUsesSignature(side2Rows, pairValue1, pairValue2, compareDistance)

            If Not String.Equals(sig1, sig2, StringComparison.OrdinalIgnoreCase) Then Continue For

            Dim normalizedVariable As String = grp.Key.Trim()

            Dim normalizedRows As New Dictionary(Of String, NormalizedLinkRow)(StringComparer.OrdinalIgnoreCase)

            For Each srcRow As VarLinkDimRow In side1Rows.Concat(side2Rows)

                Dim normalizedUsesVariable As String =
                    NormalizeCommonSubDim(srcRow.UsesVariable, pairValue1, pairValue2)

                plan.RowIdsToDelete.Add(srcRow.RowId)

                AddTouchedNameToPlan(
                    plan,
                    "Variable",
                    srcRow.Variable,
                    normalizedVariable,
                    pairValue1,
                    pairValue2
                )

                AddTouchedNameToPlan(
                    plan,
                    "UsesVariable",
                    srcRow.UsesVariable,
                    normalizedUsesVariable,
                    pairValue1,
                    pairValue2
                )

                AddNormalizedRow(
                    normalizedRows,
                    normalizedVariable,
                    normalizedUsesVariable,
                    srcRow.Distance
                )

            Next

            For Each nr As NormalizedLinkRow In normalizedRows.Values
                plan.LinksToInsert.Add(nr)
            Next

            plan.GroupsConsolidated += 1

        Next

        Return plan

    End Function
    'mti 2026/05/13
    Private Function BuildUsesVariableSideEditPlanInc(
    ByVal allRows As List(Of VarLinkDimRow),
    ByVal pairValue1 As String,
    ByVal pairValue2 As String,
    ByVal compareDistance As Boolean
) As VarLinkPairEditPlan

        Dim plan As New VarLinkPairEditPlan()

        'mti 2026/05/13
        'This is the reverse-side consolidation.
        '
        'Earlier version held Variable completely fixed. This updated version
        'also handles rows where both Variable and UsesVariable contain the pair.
        '
        'Allowed same-pair mapping:
        '   X(Male)   -> Y(Male)
        '   X(Female) -> Y(Female)
        '
        'Allowed cross-pair mapping:
        '   X(Male)   -> Y(Female)
        '   X(Female) -> Y(Male)
        '
        'Both become:
        '   X -> Y
        '
        'But this is not consolidated:
        '   X(Male)   -> Y(Male)
        '   X(Female) -> Y(Male)

        Dim candidateGroups =
        allRows.
        Where(Function(r)
                  Return TextContainsExactSubDim(r.UsesVariable, pairValue1) OrElse
                         TextContainsExactSubDim(r.UsesVariable, pairValue2)
              End Function).
        GroupBy(Function(r)
                    Return NormalizeForCompare(
                        NormalizeCommonSubDim(
                            r.UsesVariable,
                            pairValue1,
                            pairValue2
                        )
                    )
                End Function,
                StringComparer.OrdinalIgnoreCase)

        For Each grp In candidateGroups

            Dim side1Rows As List(Of VarLinkDimRow) =
            grp.Where(Function(r) RowBelongsToPair(r.UsesVariable, pairValue1, pairValue2) = 1).ToList()

            Dim side2Rows As List(Of VarLinkDimRow) =
            grp.Where(Function(r) RowBelongsToPair(r.UsesVariable, pairValue1, pairValue2) = 2).ToList()

            If side1Rows.Count = 0 OrElse side2Rows.Count = 0 Then Continue For

            Dim sig1 As String =
            BuildUsesVariableSideSignatureInc(
                side1Rows,
                pairValue1,
                pairValue2,
                compareDistance
            )

            Dim sig2 As String =
            BuildUsesVariableSideSignatureInc(
                side2Rows,
                pairValue1,
                pairValue2,
                compareDistance
            )

            If Not String.Equals(sig1, sig2, StringComparison.OrdinalIgnoreCase) Then Continue For

            Dim normalizedRows As New Dictionary(Of String, NormalizedLinkRow)(StringComparer.OrdinalIgnoreCase)

            For Each srcRow As VarLinkDimRow In side1Rows.Concat(side2Rows)

                Dim normalizedVariable As String =
                NormalizeCommonSubDim(srcRow.Variable, pairValue1, pairValue2)

                Dim normalizedUsesVariable As String =
                NormalizeCommonSubDim(srcRow.UsesVariable, pairValue1, pairValue2)

                plan.RowIdsToDelete.Add(srcRow.RowId)

                AddTouchedNameToPlan(
                plan,
                "Variable",
                srcRow.Variable,
                normalizedVariable,
                pairValue1,
                pairValue2
            )

                AddTouchedNameToPlan(
                plan,
                "UsesVariable",
                srcRow.UsesVariable,
                normalizedUsesVariable,
                pairValue1,
                pairValue2
            )

                AddNormalizedRow(
                normalizedRows,
                normalizedVariable,
                normalizedUsesVariable,
                srcRow.Distance
            )

            Next

            For Each nr As NormalizedLinkRow In normalizedRows.Values
                plan.LinksToInsert.Add(nr)
            Next

            plan.GroupsConsolidated += 1

        Next

        Return plan

    End Function
    'mti 2026/05/13
    Private Function BuildUsesVariableSideEditPlanCor(
        ByVal allRows As List(Of VarLinkDimRow),
        ByVal pairValue1 As String,
        ByVal pairValue2 As String,
        ByVal compareDistance As Boolean
    ) As VarLinkPairEditPlan

        Dim plan As New VarLinkPairEditPlan()

        'mti 2026/05/13
        'This is the reverse-side consolidation.
        'Here Variable is held fixed, and UsesVariable is normalized.
        '
        'Example:
        '   X(R) -> DEATHCAT(R, Male)
        '   X(R) -> DEATHCAT(R, Female)
        '
        'becomes:
        '   X(R) -> DEATHCAT(R)
        Dim candidateGroups =
            allRows.
            Where(Function(r)
                      Return TextContainsExactSubDim(r.UsesVariable, pairValue1) OrElse
                             TextContainsExactSubDim(r.UsesVariable, pairValue2)
                  End Function).
            GroupBy(Function(r)
                        Return NormalizeForCompare(r.Variable) & "||" &
                               NormalizeForCompare(
                                   NormalizeCommonSubDim(
                                       r.UsesVariable,
                                       pairValue1,
                                       pairValue2
                                   )
                               )
                    End Function,
                    StringComparer.OrdinalIgnoreCase)

        For Each grp In candidateGroups

            Dim side1Rows As List(Of VarLinkDimRow) =
                grp.Where(Function(r) RowBelongsToPair(r.UsesVariable, pairValue1, pairValue2) = 1).ToList()

            Dim side2Rows As List(Of VarLinkDimRow) =
                grp.Where(Function(r) RowBelongsToPair(r.UsesVariable, pairValue1, pairValue2) = 2).ToList()

            If side1Rows.Count = 0 OrElse side2Rows.Count = 0 Then Continue For

            Dim sig1 As String =
                BuildUsesVariableSideSignatureCor(side1Rows, compareDistance)

            Dim sig2 As String =
                BuildUsesVariableSideSignatureCor(side2Rows, compareDistance)

            If Not String.Equals(sig1, sig2, StringComparison.OrdinalIgnoreCase) Then Continue For

            Dim normalizedRows As New Dictionary(Of String, NormalizedLinkRow)(StringComparer.OrdinalIgnoreCase)

            For Each srcRow As VarLinkDimRow In side1Rows.Concat(side2Rows)

                Dim normalizedUsesVariable As String =
                    NormalizeCommonSubDim(srcRow.UsesVariable, pairValue1, pairValue2)

                plan.RowIdsToDelete.Add(srcRow.RowId)

                AddTouchedNameToPlan(
                    plan,
                    "UsesVariable",
                    srcRow.UsesVariable,
                    normalizedUsesVariable,
                    pairValue1,
                    pairValue2
                )

                AddNormalizedRow(
                    normalizedRows,
                    srcRow.Variable,
                    normalizedUsesVariable,
                    srcRow.Distance
                )

            Next

            For Each nr As NormalizedLinkRow In normalizedRows.Values
                plan.LinksToInsert.Add(nr)
            Next

            plan.GroupsConsolidated += 1

        Next

        Return plan

    End Function
    'mti 2026/05/13
    Private Function BuildUsesVariableSideSignatureInc(
    ByVal rows As IEnumerable(Of VarLinkDimRow),
    ByVal pairValue1 As String,
    ByVal pairValue2 As String,
    ByVal compareDistance As Boolean
) As String

        Dim pieces As New List(Of String)

        For Each r As VarLinkDimRow In rows

            Dim normalizedVariable As String =
            NormalizeForCompare(
                NormalizeCommonSubDim(
                    r.Variable,
                    pairValue1,
                    pairValue2
                )
            )

            'mti 2026/05/13
            'Here UsesVariable is the primary side, because this is the
            'UsesVariable-side consolidation pass.
            '
            'So we compare whether Variable has the SAME or CROSS relationship
            'relative to UsesVariable.

            Dim relationTag As String =
            PairRelationTag(
                r.UsesVariable,
                r.Variable,
                pairValue1,
                pairValue2
            )

            Dim piece As String =
            normalizedVariable & "|REL=" & relationTag

            If compareDistance Then
                piece &= "|D=" & NormalizeForCompare(DistanceToString(r.Distance))
            End If

            pieces.Add(piece)

        Next

        Return String.Join(
        vbLf,
        pieces.
            Distinct(StringComparer.OrdinalIgnoreCase).
            OrderBy(Function(x) x, StringComparer.OrdinalIgnoreCase)
    )

    End Function
    'mti 2026/05/13
    Private Function BuildUsesVariableSideEditPlan(
    ByVal allRows As List(Of VarLinkDimRow),
    ByVal pairValue1 As String,
    ByVal pairValue2 As String,
    ByVal compareDistance As Boolean
) As VarLinkPairEditPlan

        Dim plan As New VarLinkPairEditPlan()

        'mti 2026/05/13
        'Consolidate when the pair appears in the UsesVariable column.
        '
        'Old exact-match rule:
        '   Z -> X(Male)
        '   Z -> X(Female)
        'becomes:
        '   Z -> X
        '
        'New same-pair rule:
        '   Y(Male)   -> X(Male)
        '   Y(Female) -> X(Female)
        'becomes:
        '   Y -> X
        '
        'New cross-pair rule:
        '   Y(Female) -> X(Male)
        '   Y(Male)   -> X(Female)
        'becomes:
        '   Y -> X

        Dim candidateGroups =
        allRows.
        Where(Function(r)
                  Return TextContainsExactSubDim(r.UsesVariable, pairValue1) OrElse
                         TextContainsExactSubDim(r.UsesVariable, pairValue2)
              End Function).
        GroupBy(Function(r)
                    Return NormalizeForCompare(
                        NormalizeCommonSubDim(
                            r.Variable,
                            pairValue1,
                            pairValue2
                        )
                    ) &
                    "||" &
                    NormalizeForCompare(
                        NormalizeCommonSubDim(
                            r.UsesVariable,
                            pairValue1,
                            pairValue2
                        )
                    )
                End Function,
                StringComparer.OrdinalIgnoreCase)

        For Each grp In candidateGroups

            Dim side1Rows As List(Of VarLinkDimRow) =
            grp.Where(Function(r) RowBelongsToPair(r.UsesVariable, pairValue1, pairValue2) = 1).ToList()

            Dim side2Rows As List(Of VarLinkDimRow) =
            grp.Where(Function(r) RowBelongsToPair(r.UsesVariable, pairValue1, pairValue2) = 2).ToList()

            If side1Rows.Count = 0 OrElse side2Rows.Count = 0 Then Continue For

            Dim consolidationMode As String =
            GetPairConsolidationMode(
                side1Rows,
                side2Rows,
                Function(r) r.UsesVariable,
                Function(r) r.Variable,
                pairValue1,
                pairValue2,
                compareDistance
            )

            If consolidationMode.Length = 0 Then Continue For

            Dim normalizedRows As New Dictionary(Of String, NormalizedLinkRow)(StringComparer.OrdinalIgnoreCase)

            For Each srcRow As VarLinkDimRow In side1Rows.Concat(side2Rows)

                Dim newVariable As String

                If consolidationMode = "PAIRWISE" Then

                    'mti 2026/05/13
                    'Pairwise same/cross match:
                    'remove the pair from Variable too.
                    newVariable =
                    NormalizeCommonSubDim(srcRow.Variable, pairValue1, pairValue2)

                Else

                    'mti 2026/05/13
                    'Exact-match mode:
                    'keep Variable exactly as it was.
                    newVariable = srcRow.Variable

                End If

                Dim normalizedUsesVariable As String =
                NormalizeCommonSubDim(srcRow.UsesVariable, pairValue1, pairValue2)

                plan.RowIdsToDelete.Add(srcRow.RowId)

                If consolidationMode = "PAIRWISE" Then

                    AddTouchedNameToPlan(
                    plan,
                    "Variable",
                    srcRow.Variable,
                    newVariable,
                    pairValue1,
                    pairValue2
                )

                End If

                AddTouchedNameToPlan(
                plan,
                "UsesVariable",
                srcRow.UsesVariable,
                normalizedUsesVariable,
                pairValue1,
                pairValue2
            )

                AddNormalizedRow(
                normalizedRows,
                newVariable,
                normalizedUsesVariable,
                srcRow.Distance
            )

            Next

            For Each nr As NormalizedLinkRow In normalizedRows.Values
                plan.LinksToInsert.Add(nr)
            Next

            plan.GroupsConsolidated += 1

        Next

        Return plan

    End Function
    'mti 2026/05/13
    Private Function BuildUsesVariableSideSignatureCor(
        ByVal rows As IEnumerable(Of VarLinkDimRow),
        ByVal compareDistance As Boolean
    ) As String

        Dim pieces As New List(Of String)

        For Each r As VarLinkDimRow In rows

            Dim piece As String = NormalizeForCompare(r.Variable)

            If compareDistance Then
                piece &= "|" & NormalizeForCompare(DistanceToString(r.Distance))
            End If

            pieces.Add(piece)

        Next

        Return String.Join(
            vbLf,
            pieces.
                Distinct(StringComparer.OrdinalIgnoreCase).
                OrderBy(Function(x) x, StringComparer.OrdinalIgnoreCase)
        )

    End Function
    'mti 2026/05/13
    Private Function GetPairConsolidationMode(
    ByVal side1Rows As List(Of VarLinkDimRow),
    ByVal side2Rows As List(Of VarLinkDimRow),
    ByVal primarySelector As Func(Of VarLinkDimRow, String),
    ByVal otherSelector As Func(Of VarLinkDimRow, String),
    ByVal pairValue1 As String,
    ByVal pairValue2 As String,
    ByVal compareDistance As Boolean
) As String

        'mti 2026/05/13
        'First preserve the old behavior:
        'the other column must match exactly for the pairValue1 rows and pairValue2 rows.
        '
        'Example:
        '   X(Male)   -> Z
        '   X(Female) -> Z
        '
        'or reverse:
        '   Z -> X(Male)
        '   Z -> X(Female)

        Dim exactSig1 As String =
        BuildExactOtherColumnSignature(side1Rows, otherSelector, compareDistance)

        Dim exactSig2 As String =
        BuildExactOtherColumnSignature(side2Rows, otherSelector, compareDistance)

        If String.Equals(exactSig1, exactSig2, StringComparison.OrdinalIgnoreCase) Then
            Return "EXACT"
        End If


        'mti 2026/05/13
        'If exact matching does not work, try the new pairwise rule.
        '
        'Allowed:
        '   SAME:
        '      X(Male)   -> Y(Male)
        '      X(Female) -> Y(Female)
        '
        '   CROSS:
        '      X(Male)   -> Y(Female)
        '      X(Female) -> Y(Male)

        Dim pairSig1 As String =
        BuildPairwiseOtherColumnSignature(
            side1Rows,
            primarySelector,
            otherSelector,
            pairValue1,
            pairValue2,
            compareDistance
        )

        Dim pairSig2 As String =
        BuildPairwiseOtherColumnSignature(
            side2Rows,
            primarySelector,
            otherSelector,
            pairValue1,
            pairValue2,
            compareDistance
        )

        If pairSig1 IsNot Nothing AndAlso
       pairSig2 IsNot Nothing AndAlso
       String.Equals(pairSig1, pairSig2, StringComparison.OrdinalIgnoreCase) Then

            Return "PAIRWISE"

        End If

        Return ""

    End Function

    'mti 2026/05/13
    Private Function BuildExactOtherColumnSignature(
    ByVal rows As IEnumerable(Of VarLinkDimRow),
    ByVal otherSelector As Func(Of VarLinkDimRow, String),
    ByVal compareDistance As Boolean
) As String

        Dim pieces As New List(Of String)

        For Each r As VarLinkDimRow In rows

            Dim piece As String =
            NormalizeForCompare(otherSelector(r))

            If compareDistance Then
                piece &= "|D=" & NormalizeForCompare(DistanceToString(r.Distance))
            End If

            pieces.Add(piece)

        Next

        Return String.Join(
        vbLf,
        pieces.
            Distinct(StringComparer.OrdinalIgnoreCase).
            OrderBy(Function(x) x, StringComparer.OrdinalIgnoreCase)
    )

    End Function
    'mti 2026/05/13
    Private Function BuildPairwiseOtherColumnSignature(
    ByVal rows As IEnumerable(Of VarLinkDimRow),
    ByVal primarySelector As Func(Of VarLinkDimRow, String),
    ByVal otherSelector As Func(Of VarLinkDimRow, String),
    ByVal pairValue1 As String,
    ByVal pairValue2 As String,
    ByVal compareDistance As Boolean
) As String

        Dim pieces As New List(Of String)

        For Each r As VarLinkDimRow In rows

            Dim relationTag As String =
            PairRelationTag(
                primarySelector(r),
                otherSelector(r),
                pairValue1,
                pairValue2
            )

            'mti 2026/05/13
            'Pairwise mode is only for SAME or CROSS mappings.
            'NONE belongs to the old exact-match rule.
            'AMBIG should not be consolidated by pairwise logic.
            If relationTag <> "SAME" AndAlso relationTag <> "CROSS" Then
                Return Nothing
            End If

            Dim normalizedOther As String =
            NormalizeForCompare(
                NormalizeCommonSubDim(
                    otherSelector(r),
                    pairValue1,
                    pairValue2
                )
            )

            Dim piece As String =
            normalizedOther & "|REL=" & relationTag

            If compareDistance Then
                piece &= "|D=" & NormalizeForCompare(DistanceToString(r.Distance))
            End If

            pieces.Add(piece)

        Next

        Return String.Join(
        vbLf,
        pieces.
            Distinct(StringComparer.OrdinalIgnoreCase).
            OrderBy(Function(x) x, StringComparer.OrdinalIgnoreCase)
    )

    End Function
    'mti 2026/05/13
    Private Function PairRelationTag(
    ByVal primaryName As String,
    ByVal otherName As String,
    ByVal pairValue1 As String,
    ByVal pairValue2 As String
) As String

        'mti 2026/05/13
        'primaryName is the column being consolidated.
        '
        'For Variable-side consolidation:
        '   primaryName = Variable
        '   otherName   = UsesVariable
        '
        'For UsesVariable-side consolidation:
        '   primaryName = UsesVariable
        '   otherName   = Variable
        '
        'Returns:
        '   SAME:
        '      Male   with Male
        '      Female with Female
        '
        '   CROSS:
        '      Male   with Female
        '      Female with Male
        '
        '   NONE:
        '      otherName does not contain either pair value
        '
        '   AMBIG:
        '      either side contains both pair values, or the relationship
        '      cannot be cleanly identified.

        Dim primaryHas1 As Boolean = TextContainsExactSubDim(primaryName, pairValue1)
        Dim primaryHas2 As Boolean = TextContainsExactSubDim(primaryName, pairValue2)

        Dim otherHas1 As Boolean = TextContainsExactSubDim(otherName, pairValue1)
        Dim otherHas2 As Boolean = TextContainsExactSubDim(otherName, pairValue2)

        If primaryHas1 AndAlso primaryHas2 Then Return "AMBIG"
        If otherHas1 AndAlso otherHas2 Then Return "AMBIG"

        If Not primaryHas1 AndAlso Not primaryHas2 Then Return "AMBIG"

        If Not otherHas1 AndAlso Not otherHas2 Then Return "NONE"

        If primaryHas1 AndAlso otherHas1 Then Return "SAME"
        If primaryHas2 AndAlso otherHas2 Then Return "SAME"

        If primaryHas1 AndAlso otherHas2 Then Return "CROSS"
        If primaryHas2 AndAlso otherHas1 Then Return "CROSS"

        Return "AMBIG"

    End Function
    'mti 2026/05/13
    Private Function PairRelationTagInc(
    ByVal primaryName As String,
    ByVal oppositeName As String,
    ByVal pairValue1 As String,
    ByVal pairValue2 As String
) As String

        'mti 2026/05/13
        'Returns the relationship of pair values across the two columns.
        '
        'For Variable-side consolidation:
        '   primaryName  = Variable
        '   oppositeName = UsesVariable
        '
        'For UsesVariable-side consolidation:
        '   primaryName  = UsesVariable
        '   oppositeName = Variable
        '
        'Results:
        '   NONE      opposite side does not contain either pair value
        '   SAME      primary and opposite use the same pair value
        '   CROSS     primary and opposite use the opposite pair value
        '   AMBIG     one side contains both pair values or cannot be resolved

        Dim primaryHas1 As Boolean = TextContainsExactSubDim(primaryName, pairValue1)
        Dim primaryHas2 As Boolean = TextContainsExactSubDim(primaryName, pairValue2)

        Dim oppositeHas1 As Boolean = TextContainsExactSubDim(oppositeName, pairValue1)
        Dim oppositeHas2 As Boolean = TextContainsExactSubDim(oppositeName, pairValue2)

        If primaryHas1 AndAlso primaryHas2 Then Return "AMBIG"
        If oppositeHas1 AndAlso oppositeHas2 Then Return "AMBIG"

        If Not oppositeHas1 AndAlso Not oppositeHas2 Then Return "NONE"

        If Not primaryHas1 AndAlso Not primaryHas2 Then Return "AMBIG"

        If primaryHas1 AndAlso oppositeHas1 Then Return "SAME"
        If primaryHas2 AndAlso oppositeHas2 Then Return "SAME"

        If primaryHas1 AndAlso oppositeHas2 Then Return "CROSS"
        If primaryHas2 AndAlso oppositeHas1 Then Return "CROSS"

        Return "AMBIG"

    End Function
    'mti 2026/05/13
    Private Sub ApplyVarLinkEditPlan(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal tableName As String,
        ByVal plan As VarLinkPairEditPlan,
        ByVal result As ConsolidationResult
    )

        For Each link As NormalizedLinkRow In plan.LinksToInsert

            If InsertLinkIfMissing(
                conn,
                tx,
                tableName,
                link.Variable,
                link.UsesVariable,
                link.Distance
            ) Then
                result.RowsInserted += 1
            End If

        Next

        result.RowsDeleted +=
            DeleteRowsByRowId(
                conn,
                tx,
                tableName,
                plan.RowIdsToDelete
            )

    End Sub

    'mti 2026/05/13
    Private Sub AddTouchedNameToPlan(
        ByVal plan As VarLinkPairEditPlan,
        ByVal columnName As String,
        ByVal originalName As String,
        ByVal normalizedName As String,
        ByVal pairValue1 As String,
        ByVal pairValue2 As String
    )

        If String.IsNullOrWhiteSpace(originalName) Then Exit Sub
        If String.IsNullOrWhiteSpace(normalizedName) Then Exit Sub

        If NormalizeForCompare(originalName) = NormalizeForCompare(normalizedName) Then Exit Sub

        If Not TextContainsExactSubDim(originalName, pairValue1) AndAlso
           Not TextContainsExactSubDim(originalName, pairValue2) Then
            Exit Sub
        End If

        If Not plan.OriginalToNormalizedNames.ContainsKey(originalName.Trim()) Then
            plan.OriginalToNormalizedNames.Add(originalName.Trim(), normalizedName.Trim())
        End If

        plan.VarLinkDimPairRemovedNames.Add(
            columnName & ": " & originalName.Trim() & "  ->  " & normalizedName.Trim()
        )

    End Sub

    'mti 2026/05/13
    Private Function MirrorVarLinkEditsIntoIFsVarDim(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal ifsVarDimTableName As String,
        ByVal pairValue1 As String,
        ByVal pairValue2 As String,
        ByVal originalToNormalizedNames As Dictionary(Of String, String),
        ByVal finalVarLinkRows As List(Of VarLinkDimRow)
    ) As IFsVarDimConsolidationResult

        Dim result As New IFsVarDimConsolidationResult()

        If originalToNormalizedNames Is Nothing OrElse originalToNormalizedNames.Count = 0 Then
            Return result
        End If

        Dim ifsRows As List(Of IFsVarDimRow) =
            LoadIFsVarDimRows(conn, tx, ifsVarDimTableName)

        Dim groupedByNormalizedName =
            originalToNormalizedNames.
            GroupBy(Function(kvp) NormalizeForCompare(kvp.Value))

        For Each grp In groupedByNormalizedName

            Dim normalizedName As String = grp.First().Value.Trim()

            Dim originalNames As List(Of String) =
                grp.Select(Function(kvp) kvp.Key.Trim()).
                Distinct(StringComparer.OrdinalIgnoreCase).
                ToList()

            Dim candidateRows As List(Of IFsVarDimRow) =
                ifsRows.
                Where(Function(r)

                          If originalNames.Any(Function(n) SameName(r.Name, n)) Then
                              Return True
                          End If

                          If TextContainsExactSubDim(r.Name, pairValue1) OrElse
                             TextContainsExactSubDim(r.Name, pairValue2) Then

                              Dim normalizedIFsName As String =
                                  NormalizeCommonSubDim(r.Name, pairValue1, pairValue2)

                              If SameName(normalizedIFsName, normalizedName) Then
                                  Return True
                              End If

                          End If

                          Return False

                      End Function).
                OrderBy(Function(r) r.RowId).
                ToList()

            If candidateRows.Count = 0 Then Continue For

            Dim eligibleRows As New List(Of IFsVarDimRow)

            For Each r As IFsVarDimRow In candidateRows

                'mti 2026/05/13
                'Do not change IFsVarDim if the original name is still needed
                'anywhere in final VarLinkDim.
                If NameStillExistsInFinalVarLinkDim(r.Name, finalVarLinkRows) Then

                    result.RowsSkippedStillUsedInVarLinkDim += 1
                    result.SkippedStillUsedNames.Add(r.Name)

                Else

                    eligibleRows.Add(r)

                End If

            Next

            If eligibleRows.Count = 0 Then Continue For

            Dim existingNormalizedRows As List(Of IFsVarDimRow) =
                ifsRows.
                Where(Function(r) SameName(r.Name, normalizedName)).
                OrderBy(Function(r) r.RowId).
                ToList()

            Dim normalizedDefinitions As List(Of String) =
                eligibleRows.
                Select(Function(r)
                           Return NormalizeCommonSubDim(
                               If(r.Definition, ""),
                               pairValue1,
                               pairValue2
                           ).Trim()
                       End Function).
                Where(Function(x) x.Length > 0).
                Distinct(StringComparer.OrdinalIgnoreCase).
                ToList()

            Dim normalizedDefinition As String = ""

            If normalizedDefinitions.Count > 0 Then
                normalizedDefinition = normalizedDefinitions.First()
            Else
                normalizedDefinition =
                    NormalizeCommonSubDim(
                        If(eligibleRows.First().Definition, ""),
                        pairValue1,
                        pairValue2
                    ).Trim()
            End If

            Dim keeperRow As IFsVarDimRow = Nothing

            If existingNormalizedRows.Count > 0 Then

                keeperRow = existingNormalizedRows.First()

                If String.IsNullOrWhiteSpace(keeperRow.Definition) AndAlso
                   Not String.IsNullOrWhiteSpace(normalizedDefinition) Then

                    UpdateIFsVarDimRow(
                        conn,
                        tx,
                        ifsVarDimTableName,
                        keeperRow.RowId,
                        keeperRow.Name,
                        normalizedDefinition
                    )

                    result.RowsUpdated += 1
                    result.EditedRowNames.Add(
                        keeperRow.Name & "  ->  " & keeperRow.Name
                    )

                End If

            Else

                keeperRow = eligibleRows.First()

                UpdateIFsVarDimRow(
                    conn,
                    tx,
                    ifsVarDimTableName,
                    keeperRow.RowId,
                    normalizedName,
                    normalizedDefinition
                )

                result.RowsUpdated += 1
                result.EditedRowNames.Add(
                    keeperRow.Name & "  ->  " & normalizedName
                )

            End If

            Dim rowsToDelete As List(Of IFsVarDimRow) =
                eligibleRows.
                Where(Function(r) r.RowId <> keeperRow.RowId).
                OrderBy(Function(r) r.RowId).
                ToList()

            For Each r As IFsVarDimRow In rowsToDelete
                result.DeletedRowNames.Add(r.Name)
            Next

            result.RowsDeleted +=
                DeleteRowsByRowId(
                    conn,
                    tx,
                    ifsVarDimTableName,
                    rowsToDelete.Select(Function(r) r.RowId).ToList()
                )

        Next

        Return result

    End Function

    'mti 2026/05/13
    Private Sub MergeNameMappings(
        ByVal target As Dictionary(Of String, String),
        ByVal source As Dictionary(Of String, String)
    )

        If source Is Nothing Then Exit Sub

        For Each kvp In source

            If Not target.ContainsKey(kvp.Key) Then
                target.Add(kvp.Key, kvp.Value)
            End If

        Next

    End Sub

    'mti 2026/05/13
    Private Function BuildNormalizedUsesSignature(
        ByVal rows As IEnumerable(Of VarLinkDimRow),
        ByVal pairValue1 As String,
        ByVal pairValue2 As String,
        ByVal compareDistance As Boolean
    ) As String

        Dim pieces As New List(Of String)

        For Each r As VarLinkDimRow In rows

            Dim normalizedUses As String =
                NormalizeForCompare(
                    NormalizeCommonSubDim(
                        r.UsesVariable,
                        pairValue1,
                        pairValue2
                    )
                )

            Dim distancePart As String = ""

            If compareDistance Then
                distancePart = "|" & NormalizeForCompare(DistanceToString(r.Distance))
            End If

            pieces.Add(normalizedUses & distancePart)

        Next

        Return String.Join(
            vbLf,
            pieces.
                Distinct(StringComparer.OrdinalIgnoreCase).
                OrderBy(Function(x) x, StringComparer.OrdinalIgnoreCase)
        )

    End Function
    'mti 2026/05/13
    Private Function BuildNormalizedUsesSignatureInc(
    ByVal rows As IEnumerable(Of VarLinkDimRow),
    ByVal pairValue1 As String,
    ByVal pairValue2 As String,
    ByVal compareDistance As Boolean
) As String

        Dim pieces As New List(Of String)

        For Each r As VarLinkDimRow In rows

            Dim normalizedUses As String =
            NormalizeForCompare(
                NormalizeCommonSubDim(
                    r.UsesVariable,
                    pairValue1,
                    pairValue2
                )
            )

            'mti 2026/05/13
            'When both Variable and UsesVariable contain the pair, do not just
            'remove Male/Female or Skilled/Unskilled.
            '
            'Keep the relationship first:
            '   SAME  = X(Male) -> Y(Male), X(Female) -> Y(Female)
            '   CROSS = X(Male) -> Y(Female), X(Female) -> Y(Male)
            '   NONE  = UsesVariable does not contain the pair
            '
            'This prevents incorrect consolidation such as:
            '   X(Male) -> Y(Male)
            '   X(Female) -> Y(Male)

            Dim relationTag As String =
            PairRelationTagInc(
                r.Variable,
                r.UsesVariable,
                pairValue1,
                pairValue2
            )

            Dim piece As String =
            normalizedUses & "|REL=" & relationTag

            If compareDistance Then
                piece &= "|D=" & NormalizeForCompare(DistanceToString(r.Distance))
            End If

            pieces.Add(piece)

        Next

        Return String.Join(
        vbLf,
        pieces.
            Distinct(StringComparer.OrdinalIgnoreCase).
            OrderBy(Function(x) x, StringComparer.OrdinalIgnoreCase)
    )

    End Function
    'mti 2026/05/13
    Private Sub AddNormalizedRow(
        ByVal normalizedRows As Dictionary(Of String, NormalizedLinkRow),
        ByVal normalizedVariable As String,
        ByVal normalizedUsesVariable As String,
        ByVal distance As Object
    )

        Dim key As String =
            NormalizeForCompare(normalizedVariable) & "|" &
            NormalizeForCompare(normalizedUsesVariable)

        If Not normalizedRows.ContainsKey(key) Then

            normalizedRows(key) = New NormalizedLinkRow With {
                .Variable = normalizedVariable,
                .UsesVariable = normalizedUsesVariable,
                .Distance = distance
            }

        Else

            Dim existing As NormalizedLinkRow = normalizedRows(key)
            existing.Distance = ChoosePreferredDistance(existing.Distance, distance)

        End If

    End Sub

    'mti 2026/05/13
    Private Function LoadVarLinkDimRows(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal tableName As String
    ) As List(Of VarLinkDimRow)

        Dim rows As New List(Of VarLinkDimRow)

        Dim sql As String =
            $"SELECT rowid, Variable, UsesVariable, Distance FROM {tableName};"

        Using cmd As New SqliteCommand(sql, conn, tx)
            Using rdr As SqliteDataReader = cmd.ExecuteReader()

                While rdr.Read()

                    rows.Add(New VarLinkDimRow With {
                        .RowId = CLng(rdr("rowid")),
                        .Variable = If(rdr("Variable") Is DBNull.Value, "", CStr(rdr("Variable"))),
                        .UsesVariable = If(rdr("UsesVariable") Is DBNull.Value, "", CStr(rdr("UsesVariable"))),
                        .Distance = If(rdr("Distance") Is DBNull.Value, DBNull.Value, rdr("Distance"))
                    })

                End While

            End Using
        End Using

        Return rows

    End Function

    'mti 2026/05/13
    Private Function LoadIFsVarDimRows(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal tableName As String
    ) As List(Of IFsVarDimRow)

        Dim rows As New List(Of IFsVarDimRow)

        Dim sql As String =
            $"SELECT rowid, [Name], [Definition] FROM {tableName};"

        Using cmd As New SqliteCommand(sql, conn, tx)
            Using rdr As SqliteDataReader = cmd.ExecuteReader()

                While rdr.Read()

                    rows.Add(New IFsVarDimRow With {
                        .RowId = CLng(rdr("rowid")),
                        .Name = If(rdr("Name") Is DBNull.Value, "", CStr(rdr("Name"))),
                        .Definition = If(rdr("Definition") Is DBNull.Value, "", CStr(rdr("Definition")))
                    })

                End While

            End Using
        End Using

        Return rows

    End Function

    'mti 2026/05/13
    Private Function CountRows(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal tableName As String
    ) As Integer

        Dim sql As String = $"SELECT COUNT(*) FROM {tableName};"

        Using cmd As New SqliteCommand(sql, conn, tx)

            Dim obj As Object = cmd.ExecuteScalar()

            If obj Is Nothing OrElse obj Is DBNull.Value Then Return 0

            Return Convert.ToInt32(obj, CultureInfo.InvariantCulture)

        End Using

    End Function

    'mti 2026/05/13
    Private Function InsertLinkIfMissing(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal tableName As String,
        ByVal variableName As String,
        ByVal usesVariableName As String,
        ByVal distance As Object
    ) As Boolean

        Dim existsSql As String =
            $"SELECT rowid FROM {tableName} " &
            "WHERE UPPER(TRIM(Variable)) = UPPER(TRIM(@v)) " &
            "AND UPPER(TRIM(UsesVariable)) = UPPER(TRIM(@u)) " &
            "LIMIT 1;"

        Using checkCmd As New SqliteCommand(existsSql, conn, tx)

            checkCmd.Parameters.AddWithValue("@v", variableName)
            checkCmd.Parameters.AddWithValue("@u", usesVariableName)

            Dim existing As Object = checkCmd.ExecuteScalar()

            If existing IsNot Nothing AndAlso existing IsNot DBNull.Value Then Return False

        End Using

        Dim insertSql As String =
            $"INSERT INTO {tableName} (Variable, UsesVariable, Distance) " &
            "VALUES (@v, @u, @d);"

        Using insertCmd As New SqliteCommand(insertSql, conn, tx)

            insertCmd.Parameters.AddWithValue("@v", variableName)
            insertCmd.Parameters.AddWithValue("@u", usesVariableName)

            If distance Is Nothing OrElse distance Is DBNull.Value Then
                insertCmd.Parameters.AddWithValue("@d", DBNull.Value)
            Else
                insertCmd.Parameters.AddWithValue("@d", distance)
            End If

            insertCmd.ExecuteNonQuery()

        End Using

        Return True

    End Function

    'mti 2026/05/13
    Private Function DeleteRowsByRowId(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal tableName As String,
        ByVal rowIds As IEnumerable(Of Long)
    ) As Integer

        If rowIds Is Nothing Then Return 0

        Dim deleted As Integer = 0

        Dim sql As String =
            $"DELETE FROM {tableName} WHERE rowid = @rowid;"

        Using cmd As New SqliteCommand(sql, conn, tx)

            Dim p As SqliteParameter = cmd.Parameters.Add("@rowid", SqliteType.Integer)

            For Each rowId As Long In rowIds.Distinct()

                p.Value = rowId
                deleted += cmd.ExecuteNonQuery()

            Next

        End Using

        Return deleted

    End Function

    'mti 2026/05/13
    Private Sub UpdateIFsVarDimRow(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal tableName As String,
        ByVal rowId As Long,
        ByVal normalizedName As String,
        ByVal normalizedDefinition As String
    )

        Dim sql As String =
            $"UPDATE {tableName} " &
            "SET [Name] = @name, [Definition] = @definition " &
            "WHERE rowid = @rowid;"

        Using cmd As New SqliteCommand(sql, conn, tx)

            cmd.Parameters.AddWithValue("@name", normalizedName)
            cmd.Parameters.AddWithValue("@definition", If(normalizedDefinition Is Nothing, "", normalizedDefinition))
            cmd.Parameters.AddWithValue("@rowid", rowId)

            cmd.ExecuteNonQuery()

        End Using

    End Sub

    'mti 2026/05/13
    Private Function DeleteDuplicateVarLinkDimRows(
        ByVal conn As SqliteConnection,
        ByVal tx As SqliteTransaction,
        ByVal tableName As String
    ) As Integer

        Dim sql As String =
            $"DELETE FROM {tableName} " &
            "WHERE rowid NOT IN (" &
            $"  SELECT MIN(rowid) FROM {tableName} " &
            "  GROUP BY UPPER(TRIM(Variable)), UPPER(TRIM(UsesVariable))" &
            ");"

        Using cmd As New SqliteCommand(sql, conn, tx)
            Return cmd.ExecuteNonQuery()
        End Using

    End Function

    'mti 2026/05/13
    Private Function RowBelongsToPair(
        ByVal txt As String,
        ByVal pairValue1 As String,
        ByVal pairValue2 As String
    ) As Integer

        Dim has1 As Boolean = TextContainsExactSubDim(txt, pairValue1)
        Dim has2 As Boolean = TextContainsExactSubDim(txt, pairValue2)

        If has1 AndAlso Not has2 Then Return 1
        If has2 AndAlso Not has1 Then Return 2

        Return 0

    End Function

    'mti 2026/05/13
    Private Function NormalizeCommonSubDim(
        ByVal txt As String,
        ByVal pairValue1 As String,
        ByVal pairValue2 As String
    ) As String

        If txt Is Nothing Then Return ""

        Dim result As String = txt.Trim()

        result = Regex.Replace(
            result,
            "\(([^()]*)\)",
            Function(m As Match)

                Dim inside As String = m.Groups(1).Value

                Dim parts As List(Of String) =
                    inside.Split(","c).
                    Select(Function(x) x.Trim()).
                    Where(Function(x)
                              Return x.Length > 0 AndAlso
                                     Not SameToken(x, pairValue1) AndAlso
                                     Not SameToken(x, pairValue2)
                          End Function).
                    ToList()

                If parts.Count = 0 Then Return ""

                Return "(" & String.Join(", ", parts) & ")"

            End Function
        )

        result = RemoveTrailingSubDimToken(result, pairValue1)
        result = RemoveTrailingSubDimToken(result, pairValue2)

        Return result.Trim()

    End Function

    'mti 2026/05/13
    Private Function TextContainsExactSubDim(
        ByVal txt As String,
        ByVal subDimValue As String
    ) As Boolean

        If String.IsNullOrWhiteSpace(txt) OrElse String.IsNullOrWhiteSpace(subDimValue) Then Return False

        For Each m As Match In Regex.Matches(txt, "\(([^()]*)\)")

            Dim inside As String = m.Groups(1).Value

            For Each part As String In inside.Split(","c)

                If SameToken(part.Trim(), subDimValue) Then Return True

            Next

        Next

        Dim escaped As String = Regex.Escape(subDimValue.Trim())

        Dim pattern As String =
            "(?i)(^|[_\.\-\s])" & escaped & "($|[_\.\-\s])"

        Return Regex.IsMatch(txt, pattern)

    End Function

    'mti 2026/05/13
    Private Function RemoveTrailingSubDimToken(
        ByVal txt As String,
        ByVal subDimValue As String
    ) As String

        If String.IsNullOrWhiteSpace(txt) OrElse String.IsNullOrWhiteSpace(subDimValue) Then Return txt

        Dim escaped As String = Regex.Escape(subDimValue.Trim())

        Dim pattern As String =
            "(?i)([_\.\-\s]+)" & escaped & "$"

        Return Regex.Replace(txt, pattern, "")

    End Function

    'mti 2026/05/13
    Private Function NameStillExistsInFinalVarLinkDim(
        ByVal originalName As String,
        ByVal finalVarLinkRows As List(Of VarLinkDimRow)
    ) As Boolean

        If String.IsNullOrWhiteSpace(originalName) Then Return False

        For Each r As VarLinkDimRow In finalVarLinkRows

            If SameName(r.Variable, originalName) Then Return True
            If SameName(r.UsesVariable, originalName) Then Return True

        Next

        Return False

    End Function

    'mti 2026/05/13
    Private Function SameName(
        ByVal a As String,
        ByVal b As String
    ) As Boolean

        Return String.Equals(
            If(a, "").Trim(),
            If(b, "").Trim(),
            StringComparison.OrdinalIgnoreCase
        )

    End Function

    'mti 2026/05/13
    Private Function SameToken(
        ByVal a As String,
        ByVal b As String
    ) As Boolean

        Return String.Equals(
            If(a, "").Trim(),
            If(b, "").Trim(),
            StringComparison.OrdinalIgnoreCase
        )

    End Function

    'mti 2026/05/13
    Private Function NormalizeForCompare(ByVal txt As String) As String

        If txt Is Nothing Then Return ""

        Return Regex.Replace(txt.Trim(), "\s+", "").ToUpperInvariant()

    End Function

    'mti 2026/05/13
    Private Function DistanceToString(ByVal distance As Object) As String

        If distance Is Nothing OrElse distance Is DBNull.Value Then Return ""

        Return Convert.ToString(distance, CultureInfo.InvariantCulture)

    End Function

    'mti 2026/05/13
    Private Function ChoosePreferredDistance(
        ByVal existingDistance As Object,
        ByVal candidateDistance As Object
    ) As Object

        If existingDistance Is Nothing OrElse existingDistance Is DBNull.Value Then Return candidateDistance
        If candidateDistance Is Nothing OrElse candidateDistance Is DBNull.Value Then Return existingDistance

        Dim d1 As Double
        Dim d2 As Double

        If Double.TryParse(DistanceToString(existingDistance), NumberStyles.Any, CultureInfo.InvariantCulture, d1) AndAlso
           Double.TryParse(DistanceToString(candidateDistance), NumberStyles.Any, CultureInfo.InvariantCulture, d2) Then

            If d2 < d1 Then Return candidateDistance

            Return existingDistance

        End If

        Return existingDistance

    End Function

    'mti 2026/05/13
    Private Sub PrintConsolidationResult(ByVal result As ConsolidationResult)

        Console.WriteLine("")
        Console.WriteLine("============================================================")
        Console.WriteLine("Pair consolidated: " & result.PairValue1 & " / " & result.PairValue2)
        Console.WriteLine("============================================================")

        Console.WriteLine("VarLinkDim rows before: " & result.VarLinkDimRowsBefore.ToString())
        Console.WriteLine("VarLinkDim rows after:  " & result.VarLinkDimRowsAfter.ToString())
        Console.WriteLine("Variable-side groups consolidated: " & result.VariableSideGroupsConsolidated.ToString())
        Console.WriteLine("UsesVariable-side groups consolidated: " & result.UsesVariableSideGroupsConsolidated.ToString())
        Console.WriteLine("VarLinkDim rows inserted: " & result.RowsInserted.ToString())
        Console.WriteLine("VarLinkDim rows deleted: " & result.RowsDeleted.ToString())
        Console.WriteLine("VarLinkDim duplicate rows removed: " & result.DuplicatesRemoved.ToString())

        Console.WriteLine("")
        Console.WriteLine("VarLinkDim names where pair was removed:")

        If result.VarLinkDimPairRemovedNames.Count = 0 Then
            Console.WriteLine("  none")
        Else
            For Each s As String In result.VarLinkDimPairRemovedNames
                Console.WriteLine("  " & s)
            Next
        End If

        Console.WriteLine("")
        Console.WriteLine("IFsVarDim edited row names:")

        If result.IFsVarDimEditedRowNames.Count = 0 Then
            Console.WriteLine("  none")
        Else
            For Each s As String In result.IFsVarDimEditedRowNames
                Console.WriteLine("  " & s)
            Next
        End If

        Console.WriteLine("")
        Console.WriteLine("IFsVarDim deleted row names:")

        If result.IFsVarDimDeletedRowNames.Count = 0 Then
            Console.WriteLine("  none")
        Else
            For Each s As String In result.IFsVarDimDeletedRowNames
                Console.WriteLine("  " & s)
            Next
        End If

        Console.WriteLine("")
        Console.WriteLine("IFsVarDim rows skipped because the original name still exists in final VarLinkDim:")

        If result.IFsVarDimSkippedStillUsedNames.Count = 0 Then
            Console.WriteLine("  none")
        Else
            For Each s As String In result.IFsVarDimSkippedStillUsedNames
                Console.WriteLine("  " & s)
            Next
        End If

    End Sub

    'mti 2026/05/13 end

    'mti 2026/05/13 end
    '============Consolidation Male/Female only ==============



    'mti 2026/05/13 start
    'Call this after VarLinkDim has been fully populated:
    '
    'ConsolidateMaleFemaleVarLinkDim(db1)
    '
    'This routine checks VarLinkDim for variables that have Male/Female
    'as one of their sub-dimensions in the Variable column.
    '
    'If the Male and Female versions of the same variable, with the same
    'other sub-dimensions, have the same UsesVariable drivers, then Male/Female
    'is removed from the Variable column.
    '
    'Example:
    'DEATHCAT(Malaria, Male),   HLMALARIAMORT
    'DEATHCAT(Malaria, Female), HLMALARIAMORT
    '
    'becomes one row:
    'DEATHCAT(Malaria), HLMALARIAMORT
    '
    'UsesVariable is not changed.
    'Distance is not changed.
    'mti 2026/05/13 end


    'mti 2026/05/13 start

    Private Class VarLinkRow
        'mti 2026/05/13 new class to hold a row in the VarLinkDim table
        Public RowID As Long
        Public VariableText As String = ""
        Public UsesVariableText As String = ""

        Public ParsedVariable As ParsedDimmedVariable = Nothing
        Public ParsedUsesVariable As ParsedDimmedVariable = Nothing

        Public GenderValue As String = ""
        Public UsesVariableGenderValue As String = ""

        Public VariableWithoutGender As String = ""
        Public UsesVariableWithoutGender As String = ""

    End Class
    Private Class VarLinkRowOld
        'mti 2026/05/13 new class to hold a row in the VarLinkDim table
        Public RowID As Long
        Public VariableText As String = ""
        Public UsesVariableText As String = ""

        Public ParsedVariable As ParsedDimmedVariable = Nothing

        Public GenderValue As String = ""
        Public VariableWithoutGender As String = ""

    End Class


    Private Class ParsedDimmedVariable
        'mti 2026/05/13 new class to hold the parsed components of a variable with dimensions,
        'for easier processing in the consolidation routine.
        Public BaseName As String = ""
        Public Dimensions As New List(Of String)

    End Class


    Private Function CountVarLinkDimRows(ByVal db1 As SqliteConnection) As Long
        'mti 2026/05/13 new procedure to count rows in VarLinkDim, for logging purposes before/after consolidation
        Using cmd As SqliteCommand = db1.CreateCommand()

            cmd.CommandText = "SELECT COUNT(*) FROM VarLinkDim"

            Return CLng(cmd.ExecuteScalar())

        End Using

    End Function
    'mti 2026/05/13 end
    Private Sub ConsolidateMaleFemaleVarLinkDim(ByVal db1 As SqliteConnection)
        'mti 2026/05/13 new procedure to consolidate male and female variables in VarLinkDim, as described in the comments above.
        'mti 2026/05/13 start
        'This routine checks VarLinkDim for variables that have Male/Female
        'as one of their sub-dimensions in the Variable column.
        '
        'If the Male and Female versions of the same variable, with the same
        'other sub-dimensions, have the same gender-collapsed UsesVariable
        'drivers, then Male/Female is removed from both:
        '
        '1. Variable
        '2. UsesVariable
        '
        'Examples:
        '
        'DEATHCAT(Malaria, Male),   HLMALARIAMORT
        'DEATHCAT(Malaria, Female), HLMALARIAMORT
        '
        'becomes:
        '
        'DEATHCAT(Malaria), HLMALARIAMORT
        '
        '
        'EDPRICR(Male),   EDPRISUR(Male)
        'EDPRICR(Female), EDPRISUR(Female)
        '
        'becomes:
        '
        'EDPRICR, EDPRISUR
        '
        '
        'EDSECLOWRDRPGRD(Male),   EDSECLOWRENRGGRD(Male)
        'EDSECLOWRDRPGRD(Female), EDSECLOWRENRGGRD(Male)
        'EDSECLOWRDRPGRD(Male),   EDSECLOWRENRGGRD(Female)
        'EDSECLOWRDRPGRD(Female), EDSECLOWRENRGGRD(Female)
        '
        'becomes one collapsed relationship:
        '
        'EDSECLOWRDRPGRD, EDSECLOWRENRGGRD
        '
        'Distance is not changed.
        'Duplicate rows created by collapsing are deleted.
        'mti 2026/05/13 end

        If db1 Is Nothing Then Exit Sub

        If db1.State <> ConnectionState.Open Then
            db1.Open()
        End If


        Dim allRows As New List(Of VarLinkRow)

        'mti 2026/05/13 start
        'Read VarLinkDim into memory.
        'Use rowid so exact rows can be updated and deleted later.
        'mti 2026/05/13 end
        Using cmd As SqliteCommand = db1.CreateCommand()

            cmd.CommandText =
            "SELECT rowid, [Variable], UsesVariable " &
            "FROM VarLinkDim"

            Using rdr As SqliteDataReader = cmd.ExecuteReader()

                While rdr.Read()

                    Dim oneRow As New VarLinkRow

                    oneRow.RowID = CLng(rdr("rowid"))
                    oneRow.VariableText = Convert.ToString(rdr("Variable")).Trim
                    oneRow.UsesVariableText = Convert.ToString(rdr("UsesVariable")).Trim

                    'mti 2026/05/13 start
                    'Parse the left-side variable.
                    'Example:
                    'EDSECLOWRDRPGRD(Male)
                    'mti 2026/05/13 end
                    oneRow.ParsedVariable = ParseDimmedVariable(oneRow.VariableText)

                    If oneRow.ParsedVariable IsNot Nothing Then

                        oneRow.GenderValue =
                        GetGenderDimValue(oneRow.ParsedVariable.Dimensions)

                        If oneRow.GenderValue <> "" Then
                            oneRow.VariableWithoutGender =
                            BuildVariableWithoutGender(oneRow.ParsedVariable)
                        End If

                    End If


                    'mti 2026/05/13 start
                    'Parse the right-side driver variable.
                    '
                    'Examples:
                    'POP
                    'EDSECLOWRENRGGRD(Male)
                    'EDSECLOWRENRGGRD(Female)
                    'mti 2026/05/13 end
                    oneRow.ParsedUsesVariable = ParseDimmedVariable(oneRow.UsesVariableText)

                    If oneRow.ParsedUsesVariable IsNot Nothing Then

                        oneRow.UsesVariableGenderValue =
                        GetGenderDimValue(oneRow.ParsedUsesVariable.Dimensions)

                        oneRow.UsesVariableWithoutGender =
                        BuildVariableWithoutGender(oneRow.ParsedUsesVariable)

                    Else

                        oneRow.UsesVariableWithoutGender = oneRow.UsesVariableText

                    End If

                    allRows.Add(oneRow)

                End While

            End Using

        End Using


        'mti 2026/05/13 start
        'Group rows by the Variable name after removing Male/Female.
        '
        'Examples:
        'DEATHCAT(Malaria, Male)
        'DEATHCAT(Malaria, Female)
        '
        'both belong to:
        'DEATHCAT(Malaria)
        '
        'EDPRICR(Male)
        'EDPRICR(Female)
        '
        'both belong to:
        'EDPRICR
        'mti 2026/05/13 end

        Dim groups As New Dictionary(Of String, List(Of VarLinkRow))(StringComparer.OrdinalIgnoreCase)

        For Each oneRow As VarLinkRow In allRows

            If oneRow.GenderValue <> "" AndAlso oneRow.VariableWithoutGender <> "" Then

                If Not groups.ContainsKey(oneRow.VariableWithoutGender) Then
                    groups(oneRow.VariableWithoutGender) = New List(Of VarLinkRow)
                End If

                groups(oneRow.VariableWithoutGender).Add(oneRow)

            End If

        Next


        'mti 2026/05/13 start
        'Decide which rows can be collapsed.
        '
        'The comparison uses UsesVariableWithoutGender, not raw UsesVariable.
        '
        'So:
        'EDPRISUR(Male)
        'EDPRISUR(Female)
        '
        'both compare as:
        'EDPRISUR
        'mti 2026/05/13 end

        Dim rowsToUpdate As New List(Of VarLinkRow)

        For Each kvp As KeyValuePair(Of String, List(Of VarLinkRow)) In groups

            Dim groupRows As List(Of VarLinkRow) = kvp.Value

            Dim maleDrivers As New List(Of String)
            Dim femaleDrivers As New List(Of String)


            'mti 2026/05/13 update start
            'This is the updated logic.
            '
            'Earlier, the code rejected cross-gender relationships such as:
            '
            'Male   -> Driver(Female)
            'Female -> Driver(Male)
            '
            'That was too strict.
            '
            'Some IFs code calculates all-gender averages or shared quantities,
            'so both Male and Female rows may depend on both Male and Female
            'versions of the same driver.
            '
            'Therefore, we now collapse gender in UsesVariable first, then compare
            'the full driver lists.
            '
            'Examples:
            '
            'EDSECLOWRDRPGRD(Male),   EDSECLOWRENRGGRD(Male)
            'EDSECLOWRDRPGRD(Female), EDSECLOWRENRGGRD(Male)
            'EDSECLOWRDRPGRD(Male),   EDSECLOWRENRGGRD(Female)
            'EDSECLOWRDRPGRD(Female), EDSECLOWRENRGGRD(Female)
            '
            'After removing Male/Female from UsesVariable, both sides have:
            '
            'EDSECLOWRENRGGRD
            'EDSECLOWRENRGGRD
            '
            'So the group can be collapsed.
            '
            'SameDriverList is count-sensitive, so duplicate collapsed drivers
            'still matter during the comparison.
            'mti 2026/05/13 update end

            For Each oneRow As VarLinkRow In groupRows

                If oneRow.GenderValue.Equals("Male", StringComparison.OrdinalIgnoreCase) Then

                    maleDrivers.Add(oneRow.UsesVariableWithoutGender)

                ElseIf oneRow.GenderValue.Equals("Female", StringComparison.OrdinalIgnoreCase) Then

                    femaleDrivers.Add(oneRow.UsesVariableWithoutGender)

                End If

            Next


            'mti 2026/05/13 start
            'Only collapse when both Male and Female exist and have
            'the same gender-collapsed UsesVariable driver list.
            '
            'These match:
            'A, B, B
            'B, A, B
            '
            'These do not match:
            'A, B, B
            'A, B
            'mti 2026/05/13 end
            If maleDrivers.Count > 0 AndAlso femaleDrivers.Count > 0 Then

                If SameDriverList(maleDrivers, femaleDrivers) Then

                    For Each oneRow As VarLinkRow In groupRows
                        rowsToUpdate.Add(oneRow)
                    Next

                End If

            End If

        Next


        'mti 2026/05/13 start
        'If nothing can be collapsed, exit.
        'mti 2026/05/13 end
        If rowsToUpdate.Count = 0 Then Exit Sub


        'mti 2026/05/13 start
        'Update Variable and UsesVariable, then delete duplicate rows.
        '
        'Distance is not changed.
        'mti 2026/05/13 end

        Using trans As SqliteTransaction = db1.BeginTransaction()

            'mti 2026/05/13 start
            'Temporary table to remember affected collapsed variables.
            'This lets us delete duplicates only for variables touched
            'by this routine.
            'mti 2026/05/13 end
            Using cmdTmp As SqliteCommand = db1.CreateCommand()

                cmdTmp.Transaction = trans

                cmdTmp.CommandText =
                "DROP TABLE IF EXISTS TmpCollapsedVars"
                cmdTmp.ExecuteNonQuery()

                cmdTmp.CommandText =
                "CREATE TEMP TABLE TmpCollapsedVars (" &
                "[Variable] TEXT PRIMARY KEY)"
                cmdTmp.ExecuteNonQuery()

            End Using


            'mti 2026/05/13 start
            'Update both Variable and UsesVariable.
            '
            'Examples:
            '
            'Variable:
            'EDPRICR(Male) -> EDPRICR
            '
            'UsesVariable:
            'EDPRISUR(Male) -> EDPRISUR
            '
            '
            'Variable:
            'DEATHCAT(Malaria, Male) -> DEATHCAT(Malaria)
            '
            'UsesVariable:
            'HLMALARIAMORT -> HLMALARIAMORT
            'mti 2026/05/13 end
            Using cmdUpdate As SqliteCommand = db1.CreateCommand()

                cmdUpdate.Transaction = trans

                cmdUpdate.CommandText =
                "UPDATE VarLinkDim " &
                "SET [Variable] = @NewVariable, " &
                "    UsesVariable = @NewUsesVariable " &
                "WHERE rowid = @RowID"

                Dim pNewVariable = cmdUpdate.Parameters.Add("@NewVariable", SqliteType.Text)
                Dim pNewUsesVariable = cmdUpdate.Parameters.Add("@NewUsesVariable", SqliteType.Text)
                Dim pRowID = cmdUpdate.Parameters.Add("@RowID", SqliteType.Integer)

                For Each oneRow As VarLinkRow In rowsToUpdate

                    pNewVariable.Value = oneRow.VariableWithoutGender
                    pNewUsesVariable.Value = oneRow.UsesVariableWithoutGender
                    pRowID.Value = oneRow.RowID

                    cmdUpdate.ExecuteNonQuery()

                Next

            End Using


            'mti 2026/05/13 start
            'Save affected collapsed variable names.
            '
            'Examples:
            'DEATHCAT(Malaria)
            'EDPRICR
            'EDSECLOWRDRPGRD
            'mti 2026/05/13 end
            Using cmdInsTmp As SqliteCommand = db1.CreateCommand()

                cmdInsTmp.Transaction = trans

                cmdInsTmp.CommandText =
                "INSERT OR IGNORE INTO TmpCollapsedVars ([Variable]) " &
                "VALUES (@Variable)"

                Dim pVar = cmdInsTmp.Parameters.Add("@Variable", SqliteType.Text)

                For Each oneRow As VarLinkRow In rowsToUpdate

                    pVar.Value = oneRow.VariableWithoutGender
                    cmdInsTmp.ExecuteNonQuery()

                Next

            End Using


            'mti 2026/05/13 start
            'Delete duplicate rows created by collapsing.
            '
            'Duplicates are identified by:
            'Variable, UsesVariable, Distance
            '
            'Distance is included so it is kept as-is.
            '
            'If two rows have the same Variable and UsesVariable but different
            'Distance values, both rows are kept.
            '
            'If you want to delete duplicates even when Distance differs,
            'remove Distance from the GROUP BY line below.
            'mti 2026/05/13 end
            Using cmdDeleteDup As SqliteCommand = db1.CreateCommand()

                cmdDeleteDup.Transaction = trans

                cmdDeleteDup.CommandText =
                "DELETE FROM VarLinkDim " &
                "WHERE [Variable] IN (SELECT [Variable] FROM TmpCollapsedVars) " &
                "AND rowid NOT IN (" &
                "    SELECT MIN(rowid) " &
                "    FROM VarLinkDim " &
                "    WHERE [Variable] IN (SELECT [Variable] FROM TmpCollapsedVars) " &
                "    GROUP BY [Variable], UsesVariable, [Distance]" &
                ")"

                cmdDeleteDup.ExecuteNonQuery()

            End Using


            'mti 2026/05/13 start
            'Clean up temporary table.
            'mti 2026/05/13 end
            Using cmdDropTmp As SqliteCommand = db1.CreateCommand()

                cmdDropTmp.Transaction = trans

                cmdDropTmp.CommandText =
                "DROP TABLE IF EXISTS TmpCollapsedVars"

                cmdDropTmp.ExecuteNonQuery()

            End Using

            trans.Commit()

        End Using

    End Sub

    Private Sub ConsolidateMaleFemaleVarLinkDimSecond(ByVal db1 As SqliteConnection)
        'mti 2026/05/13 new procedure to consolidate male and female variables in VarLinkDim, as described in the comments above.
        'mti 2026/05/13 start
        'This routine checks VarLinkDim for variables that have Male/Female
        'as one of their sub-dimensions in the Variable column.
        '
        'It now also handles symmetric Male/Female drivers in UsesVariable.
        '
        'Example:
        'EDPRICR(Male),   EDPRISUR(Male)
        'EDPRICR(Female), EDPRISUR(Female)
        '
        'becomes one row:
        'EDPRICR, EDPRISUR
        '
        'The group is collapsed only when:
        '1. Male and Female rows both exist.
        '2. Every Male row has a corresponding Female row.
        '3. UsesVariable relationships match after removing symmetric
        '   Male/Female dimensions.
        '4. A Male variable is not driven by a Female-specific driver,
        '   and a Female variable is not driven by a Male-specific driver.
        '
        'Distance is not changed.
        'Duplicate rows created by collapsing are deleted.
        'mti 2026/05/13 end

        Dim allRows As New List(Of VarLinkRow)

        'mti 2026/05/13 start
        'Read VarLinkDim into memory.
        'Use rowid so exact rows can be updated and deleted later.
        'mti 2026/05/13 end
        Using cmd As SqliteCommand = db1.CreateCommand()

            cmd.CommandText =
            "SELECT rowid, [Variable], UsesVariable " &
            "FROM VarLinkDim"

            Using rdr As SqliteDataReader = cmd.ExecuteReader()

                While rdr.Read()

                    Dim oneRow As New VarLinkRow

                    oneRow.RowID = CLng(rdr("rowid"))
                    oneRow.VariableText = Convert.ToString(rdr("Variable")).Trim
                    oneRow.UsesVariableText = Convert.ToString(rdr("UsesVariable")).Trim

                    'Parse the left-side variable.
                    oneRow.ParsedVariable = ParseDimmedVariable(oneRow.VariableText)

                    If oneRow.ParsedVariable IsNot Nothing Then

                        oneRow.GenderValue =
                        GetGenderDimValue(oneRow.ParsedVariable.Dimensions)

                        If oneRow.GenderValue <> "" Then
                            oneRow.VariableWithoutGender =
                            BuildVariableWithoutGender(oneRow.ParsedVariable)
                        End If

                    End If

                    'Parse the right-side driver variable.
                    oneRow.ParsedUsesVariable = ParseDimmedVariable(oneRow.UsesVariableText)

                    If oneRow.ParsedUsesVariable IsNot Nothing Then

                        oneRow.UsesVariableGenderValue =
                        GetGenderDimValue(oneRow.ParsedUsesVariable.Dimensions)

                        oneRow.UsesVariableWithoutGender =
                        BuildVariableWithoutGender(oneRow.ParsedUsesVariable)

                    Else

                        oneRow.UsesVariableWithoutGender = oneRow.UsesVariableText

                    End If

                    allRows.Add(oneRow)

                End While

            End Using

        End Using


        'mti 2026/05/13 start
        'Group rows by the Variable name after removing Male/Female.
        '
        'Examples:
        'DEATHCAT(Malaria, Male)
        'DEATHCAT(Malaria, Female)
        '
        'both belong to:
        'DEATHCAT(Malaria)
        '
        'EDPRICR(Male)
        'EDPRICR(Female)
        '
        'both belong to:
        'EDPRICR
        'mti 2026/05/13 end

        Dim groups As New Dictionary(Of String, List(Of VarLinkRow))(StringComparer.OrdinalIgnoreCase)

        For Each oneRow As VarLinkRow In allRows

            If oneRow.GenderValue <> "" AndAlso oneRow.VariableWithoutGender <> "" Then

                If Not groups.ContainsKey(oneRow.VariableWithoutGender) Then
                    groups(oneRow.VariableWithoutGender) = New List(Of VarLinkRow)
                End If

                groups(oneRow.VariableWithoutGender).Add(oneRow)

            End If

        Next


        'mti 2026/05/13 start
        'Decide which rows can be collapsed.
        '
        'The comparison uses UsesVariableWithoutGender, not the raw UsesVariable.
        '
        'Therefore:
        'EDPRISUR(Male) and EDPRISUR(Female)
        '
        'are treated as the same driver:
        'EDPRISUR
        'mti 2026/05/13 end

        Dim rowsToUpdate As New List(Of VarLinkRow)

        For Each kvp As KeyValuePair(Of String, List(Of VarLinkRow)) In groups

            Dim groupRows As List(Of VarLinkRow) = kvp.Value

            Dim maleDrivers As New List(Of String)
            Dim femaleDrivers As New List(Of String)

            Dim groupOK As Boolean = True

            For Each oneRow As VarLinkRow In groupRows

                'mti 2026/05/13 start
                'Reject cross-gender driver relationships.
                '
                'Allowed:
                'EDPRICR(Male)   -> EDPRISUR(Male)
                'EDPRICR(Female) -> EDPRISUR(Female)
                '
                'Also allowed:
                'EDPRICR(Male)   -> POP
                'EDPRICR(Female) -> POP
                '
                'Not allowed:
                'EDPRICR(Male)   -> EDPRISUR(Female)
                'EDPRICR(Female) -> EDPRISUR(Male)
                'mti 2026/05/13 end

                If oneRow.GenderValue.Equals("Male", StringComparison.OrdinalIgnoreCase) AndAlso
               oneRow.UsesVariableGenderValue.Equals("Female", StringComparison.OrdinalIgnoreCase) Then

                    groupOK = False
                    Exit For

                End If

                If oneRow.GenderValue.Equals("Female", StringComparison.OrdinalIgnoreCase) AndAlso
               oneRow.UsesVariableGenderValue.Equals("Male", StringComparison.OrdinalIgnoreCase) Then

                    groupOK = False
                    Exit For

                End If

                If oneRow.GenderValue.Equals("Male", StringComparison.OrdinalIgnoreCase) Then

                    maleDrivers.Add(oneRow.UsesVariableWithoutGender)

                ElseIf oneRow.GenderValue.Equals("Female", StringComparison.OrdinalIgnoreCase) Then

                    femaleDrivers.Add(oneRow.UsesVariableWithoutGender)

                End If

            Next


            If groupOK Then

                'mti 2026/05/13 start
                'Only collapse when both Male and Female exist and have
                'the same gender-collapsed UsesVariable drivers.
                'mti 2026/05/13 end
                If maleDrivers.Count > 0 AndAlso femaleDrivers.Count > 0 Then

                    If SameDriverList(maleDrivers, femaleDrivers) Then

                        For Each oneRow As VarLinkRow In groupRows
                            rowsToUpdate.Add(oneRow)
                        Next

                    End If

                End If

            End If

        Next


        'mti 2026/05/13 start
        'If nothing can be collapsed, exit.
        'mti 2026/05/13 end
        If rowsToUpdate.Count = 0 Then Exit Sub


        'mti 2026/05/13 start
        'Update Variable and UsesVariable, then delete duplicate rows.
        '
        'Distance is not changed.
        'mti 2026/05/13 end

        Using trans As SqliteTransaction = db1.BeginTransaction()

            'mti 2026/05/13 start
            'Temporary table to remember affected collapsed variables.
            'mti 2026/05/13 end
            Using cmdTmp As SqliteCommand = db1.CreateCommand()

                cmdTmp.Transaction = trans

                cmdTmp.CommandText =
                "DROP TABLE IF EXISTS TmpCollapsedVars"
                cmdTmp.ExecuteNonQuery()

                cmdTmp.CommandText =
                "CREATE TEMP TABLE TmpCollapsedVars (" &
                "Variable TEXT PRIMARY KEY)"
                cmdTmp.ExecuteNonQuery()

            End Using


            'mti 2026/05/13 start
            'Update both Variable and UsesVariable.
            '
            'Examples:
            'Variable:     EDPRICR(Male)     -> EDPRICR
            'UsesVariable: EDPRISUR(Male)    -> EDPRISUR
            '
            'Variable:     DEATHCAT(Malaria, Male) -> DEATHCAT(Malaria)
            'UsesVariable: HLMALARIAMORT           -> HLMALARIAMORT
            'mti 2026/05/13 end
            Using cmdUpdate As SqliteCommand = db1.CreateCommand()

                cmdUpdate.Transaction = trans

                cmdUpdate.CommandText =
                "UPDATE VarLinkDim " &
                "SET [Variable] = @NewVariable, " &
                "    UsesVariable = @NewUsesVariable " &
                "WHERE rowid = @RowID"

                Dim pNewVariable = cmdUpdate.Parameters.Add("@NewVariable", SqliteType.Text)
                Dim pNewUsesVariable = cmdUpdate.Parameters.Add("@NewUsesVariable", SqliteType.Text)
                Dim pRowID = cmdUpdate.Parameters.Add("@RowID", SqliteType.Integer)

                For Each oneRow As VarLinkRow In rowsToUpdate

                    pNewVariable.Value = oneRow.VariableWithoutGender
                    pNewUsesVariable.Value = oneRow.UsesVariableWithoutGender
                    pRowID.Value = oneRow.RowID

                    cmdUpdate.ExecuteNonQuery()

                Next

            End Using


            'mti 2026/05/13 start
            'Save affected collapsed variable names.
            'mti 2026/05/13 end
            Using cmdInsTmp As SqliteCommand = db1.CreateCommand()

                cmdInsTmp.Transaction = trans

                cmdInsTmp.CommandText =
                "INSERT OR IGNORE INTO TmpCollapsedVars (Variable) " &
                "VALUES (@Variable)"

                Dim pVar = cmdInsTmp.Parameters.Add("@Variable", SqliteType.Text)

                For Each oneRow As VarLinkRow In rowsToUpdate

                    pVar.Value = oneRow.VariableWithoutGender
                    cmdInsTmp.ExecuteNonQuery()

                Next

            End Using


            'mti 2026/05/13 start
            'Delete duplicate rows created by collapsing.
            '
            'Duplicates are identified by:
            'Variable, UsesVariable, Distance
            '
            'Distance is included so it is kept as-is.
            'If two rows have the same Variable and UsesVariable but different
            'Distance values, both rows are kept.
            '
            'If you want to delete duplicates even when Distance differs,
            'remove Distance from the GROUP BY line below.
            'mti 2026/05/13 end
            Using cmdDeleteDup As SqliteCommand = db1.CreateCommand()

                cmdDeleteDup.Transaction = trans

                cmdDeleteDup.CommandText =
                "DELETE FROM VarLinkDim " &
                "WHERE [Variable] IN (SELECT Variable FROM TmpCollapsedVars) " &
                "AND rowid NOT IN (" &
                "    SELECT MIN(rowid) " &
                "    FROM VarLinkDim " &
                "    WHERE [Variable] IN (SELECT Variable FROM TmpCollapsedVars) " &
                "    GROUP BY [Variable], UsesVariable, Distance" &
                ")"

                cmdDeleteDup.ExecuteNonQuery()

            End Using


            'mti 2026/05/13 start
            'Clean up temporary table.
            'mti 2026/05/13 end
            Using cmdDropTmp As SqliteCommand = db1.CreateCommand()

                cmdDropTmp.Transaction = trans

                cmdDropTmp.CommandText =
                "DROP TABLE IF EXISTS TmpCollapsedVars"

                cmdDropTmp.ExecuteNonQuery()

            End Using

            trans.Commit()

        End Using

    End Sub

    Private Sub ConsolidateMaleFemaleVarLinkDimOld(ByVal db1 As SqliteConnection)
        'mti 2026/05/13 new procedure to consolidate male and female variables in VarLinkDim, as described in the comments above.
        'mti 2026/05/13 start
        'Read VarLinkDim into memory first.
        'Use rowid so we can update/delete exact rows later.
        'mti 2026/05/13 end

        Dim allRows As New List(Of VarLinkRow)

        Using cmd As SqliteCommand = db1.CreateCommand()

            cmd.CommandText =
            "SELECT rowid, [Variable], UsesVariable " &
            "FROM VarLinkDim"

            Using rdr As SqliteDataReader = cmd.ExecuteReader()

                While rdr.Read()

                    Dim oneRow As New VarLinkRow

                    oneRow.RowID = CLng(rdr("rowid"))
                    oneRow.VariableText = Convert.ToString(rdr("Variable")).Trim
                    oneRow.UsesVariableText = Convert.ToString(rdr("UsesVariable")).Trim

                    oneRow.ParsedVariable = ParseDimmedVariable(oneRow.VariableText)

                    If oneRow.ParsedVariable IsNot Nothing Then

                        oneRow.GenderValue = GetGenderDimValue(oneRow.ParsedVariable.Dimensions)

                        If oneRow.GenderValue <> "" Then
                            oneRow.VariableWithoutGender =
                            BuildVariableWithoutGender(oneRow.ParsedVariable)
                        End If

                    End If

                    allRows.Add(oneRow)

                End While

            End Using

        End Using


        'mti 2026/05/13 start
        'Group rows by the variable name after removing Male/Female.
        '
        'Example:
        'DEATHCAT(Malaria, Male)
        'DEATHCAT(Malaria, Female)
        '
        'both belong to group:
        'DEATHCAT(Malaria)
        'mti 2026/05/13 end

        Dim groups As New Dictionary(Of String, List(Of VarLinkRow))(StringComparer.OrdinalIgnoreCase)

        For Each oneRow As VarLinkRow In allRows

            If oneRow.GenderValue <> "" AndAlso oneRow.VariableWithoutGender <> "" Then

                If Not groups.ContainsKey(oneRow.VariableWithoutGender) Then
                    groups(oneRow.VariableWithoutGender) = New List(Of VarLinkRow)
                End If

                groups(oneRow.VariableWithoutGender).Add(oneRow)

            End If

        Next


        'mti 2026/05/13 start
        'Decide which rows can be collapsed.
        '
        'For a collapsed group, the Male rows and Female rows must have
        'the same UsesVariable driver list.
        '
        'If one side has a different driver, or if one side is missing,
        'the group is not collapsed.
        'mti 2026/05/13 end

        Dim rowsToUpdate As New List(Of VarLinkRow)

        For Each kvp As KeyValuePair(Of String, List(Of VarLinkRow)) In groups

            Dim groupRows As List(Of VarLinkRow) = kvp.Value

            Dim maleDrivers As New List(Of String)
            Dim femaleDrivers As New List(Of String)

            For Each oneRow As VarLinkRow In groupRows

                If oneRow.GenderValue.Equals("Male", StringComparison.OrdinalIgnoreCase) Then
                    maleDrivers.Add(oneRow.UsesVariableText)
                ElseIf oneRow.GenderValue.Equals("Female", StringComparison.OrdinalIgnoreCase) Then
                    femaleDrivers.Add(oneRow.UsesVariableText)
                End If

            Next

            'mti 2026/05/13 start
            'Only collapse when both Male and Female exist and have
            'the same UsesVariable drivers.
            'mti 2026/05/13 end
            If maleDrivers.Count > 0 AndAlso femaleDrivers.Count > 0 Then

                If SameDriverList(maleDrivers, femaleDrivers) Then

                    For Each oneRow As VarLinkRow In groupRows
                        rowsToUpdate.Add(oneRow)
                    Next

                End If

            End If

        Next


        'mti 2026/05/13 start
        'If nothing can be collapsed, exit.
        'mti 2026/05/13 end
        If rowsToUpdate.Count = 0 Then Exit Sub


        'mti 2026/05/13 start
        'Update the Variable column and delete duplicate rows.
        '
        'The duplicate deletion keeps the row with the smallest rowid.
        'UsesVariable and Distance are not modified.
        'mti 2026/05/13 end

        Using trans As SqliteTransaction = db1.BeginTransaction()

            'mti 2026/05/13 start
            'Temporary table to remember which collapsed variable names
            'were affected. This lets us remove duplicates only for
            'variables touched by this routine.
            'mti 2026/05/13 end
            Using cmdTmp As SqliteCommand = db1.CreateCommand()

                cmdTmp.Transaction = trans

                cmdTmp.CommandText =
                "DROP TABLE IF EXISTS TmpCollapsedVars"
                cmdTmp.ExecuteNonQuery()

                cmdTmp.CommandText =
                "CREATE TEMP TABLE TmpCollapsedVars (" &
                "Variable TEXT PRIMARY KEY)"
                cmdTmp.ExecuteNonQuery()

            End Using


            'mti 2026/05/13 start
            'Update only the Variable column.
            'mti 2026/05/13 end
            Using cmdUpdate As SqliteCommand = db1.CreateCommand()

                cmdUpdate.Transaction = trans

                cmdUpdate.CommandText =
                "UPDATE VarLinkDim " &
                "SET [Variable] = @NewVariable " &
                "WHERE rowid = @RowID"

                Dim pNewVariable = cmdUpdate.Parameters.Add("@NewVariable", SqliteType.Text)
                Dim pRowID = cmdUpdate.Parameters.Add("@RowID", SqliteType.Integer)

                For Each oneRow As VarLinkRow In rowsToUpdate

                    pNewVariable.Value = oneRow.VariableWithoutGender
                    pRowID.Value = oneRow.RowID

                    cmdUpdate.ExecuteNonQuery()

                Next

            End Using


            'mti 2026/05/13 start
            'Save the affected collapsed variable names.
            '
            'Examples:
            'DEATHCAT(Malaria)
            'DEATHCAT(AIDS)
            'DEATHCAT(IntInj)
            'mti 2026/05/13 end
            Using cmdInsTmp As SqliteCommand = db1.CreateCommand()

                cmdInsTmp.Transaction = trans

                cmdInsTmp.CommandText =
                "INSERT OR IGNORE INTO TmpCollapsedVars (Variable) " &
                "VALUES (@Variable)"

                Dim pVar = cmdInsTmp.Parameters.Add("@Variable", SqliteType.Text)

                For Each oneRow As VarLinkRow In rowsToUpdate

                    pVar.Value = oneRow.VariableWithoutGender
                    cmdInsTmp.ExecuteNonQuery()

                Next

            End Using


            'mti 2026/05/13 start
            'Delete duplicate rows created by removing Male/Female.
            '
            'Duplicates are identified by:
            'Variable, UsesVariable, Distance
            '
            'Distance is included so it is kept as-is.
            'If two rows have the same Variable and UsesVariable but different
            'Distance values, both rows are kept.
            '
            'If you want to delete duplicates even when Distance differs,
            'remove Distance from the GROUP BY line below.
            'mti 2026/05/13 end
            Using cmdDeleteDup As SqliteCommand = db1.CreateCommand()

                cmdDeleteDup.Transaction = trans

                cmdDeleteDup.CommandText =
                "DELETE FROM VarLinkDim " &
                "WHERE [Variable] IN (SELECT Variable FROM TmpCollapsedVars) " &
                "AND rowid NOT IN (" &
                "    SELECT MIN(rowid) " &
                "    FROM VarLinkDim " &
                "    WHERE [Variable] IN (SELECT Variable FROM TmpCollapsedVars) " &
                "    GROUP BY [Variable], UsesVariable, Distance" &
                ")"

                cmdDeleteDup.ExecuteNonQuery()

            End Using


            'mti 2026/05/13 start
            'Clean up temporary table.
            'mti 2026/05/13 end
            Using cmdDropTmp As SqliteCommand = db1.CreateCommand()

                cmdDropTmp.Transaction = trans

                cmdDropTmp.CommandText =
                "DROP TABLE IF EXISTS TmpCollapsedVars"

                cmdDropTmp.ExecuteNonQuery()

            End Using

            trans.Commit()

        End Using

    End Sub


    Private Function ParseDimmedVariable(ByVal variableText As String) As ParsedDimmedVariable
        'mti 2026/05/13 new procedure to parse a variable reference that may include dimensions,
        ''and break it into base name and dimension list.
        If variableText Is Nothing Then Return Nothing

        Dim tmp As String = variableText.Trim

        If tmp = "" Then Return Nothing

        Dim result As New ParsedDimmedVariable

        Dim p1 As Integer = tmp.IndexOf("("c)
        Dim p2 As Integer = tmp.LastIndexOf(")"c)

        If p1 < 0 OrElse p2 < 0 OrElse p2 <= p1 Then

            'mti 2026/05/13 start
            'No dimensions.
            'Example:
            'GDP
            'mti 2026/05/13 end
            result.BaseName = tmp
            Return result

        End If

        'mti 2026/05/13 start
        'Base variable name.
        'Example:
        'DEATHCAT(Malaria, Male) -> DEATHCAT
        'mti 2026/05/13 end
        result.BaseName = tmp.Substring(0, p1).Trim

        'mti 2026/05/13 start
        'Dimension text.
        'Example:
        'DEATHCAT(Malaria, Male) -> Malaria, Male
        'mti 2026/05/13 end
        Dim dimText As String = tmp.Substring(p1 + 1, p2 - p1 - 1)

        Dim parts() As String = dimText.Split(","c)

        For Each part As String In parts

            Dim oneDim As String = part.Trim

            If oneDim <> "" Then
                result.Dimensions.Add(oneDim)
            End If

        Next

        Return result

    End Function


    Private Function GetGenderDimValue(ByVal dims As List(Of String)) As String
        'mti 2026/05/13 new procedure to get the gender dimension value
        If dims Is Nothing Then Return ""

        For Each oneDim As String In dims

            If oneDim.Equals("Male", StringComparison.OrdinalIgnoreCase) Then
                Return "Male"
            End If

            If oneDim.Equals("Female", StringComparison.OrdinalIgnoreCase) Then
                Return "Female"
            End If

        Next

        Return ""

    End Function


    Private Function BuildVariableWithoutGender(ByVal parsedVar As ParsedDimmedVariable) As String
        'mti 2026/05/13 new procedure to build a variable reference without gender dimensions
        If parsedVar Is Nothing Then Return ""

        Dim newDims As New List(Of String)

        For Each oneDim As String In parsedVar.Dimensions

            If Not oneDim.Equals("Male", StringComparison.OrdinalIgnoreCase) AndAlso
           Not oneDim.Equals("Female", StringComparison.OrdinalIgnoreCase) Then

                newDims.Add(oneDim)

            End If

        Next

        If newDims.Count = 0 Then

            'mti 2026/05/13 start
            'Example:
            'SomeVariable(Male) -> SomeVariable
            'mti 2026/05/13 end
            Return parsedVar.BaseName

        Else

            'mti 2026/05/13 start
            'Example:
            'DEATHCAT(Malaria, Male) -> DEATHCAT(Malaria)
            'DEATHCAT(Male, Malaria) -> DEATHCAT(Malaria)
            'mti 2026/05/13 end
            Return parsedVar.BaseName & "(" & String.Join(", ", newDims.ToArray()) & ")"

        End If

    End Function


    Private Function SameDriverList(ByVal list1 As List(Of String), ByVal list2 As List(Of String)) As Boolean
        'mti 2026/05/13 new procedure to compare two driver lists
        If list1 Is Nothing OrElse list2 Is Nothing Then Return False

        If list1.Count <> list2.Count Then Return False

        Dim a() As String = list1.ToArray()
        Dim b() As String = list2.ToArray()

        'mti 2026/05/13 start
        'Sort first so the comparison is not affected by row order.
        '
        'This uses case-insensitive comparison because IFs/VB variable names
        'are generally case-insensitive.
        '
        'If you want strict case-sensitive comparison, replace
        'StringComparer.OrdinalIgnoreCase with StringComparer.Ordinal,
        'and StringComparison.OrdinalIgnoreCase with StringComparison.Ordinal.
        'mti 2026/05/13 end
        Array.Sort(a, StringComparer.OrdinalIgnoreCase)
        Array.Sort(b, StringComparer.OrdinalIgnoreCase)

        For i As Integer = 0 To a.Length - 1

            If Not String.Equals(a(i), b(i), StringComparison.OrdinalIgnoreCase) Then
                Return False
            End If

        Next

        Return True

    End Function



    'mti 2026/05/13 end code and functions for consolidating male and female
    Private Function DimensionedVarsMatch(ByVal var1 As String,
                                      ByVal var2 As String,
                                      ByVal GRPSEnum(,) As String) As Boolean
        'mti 2026/04/21 new procedure  
        'Compares two IFs variable references that may or may not include dimensions.
        'Examples:
        '   AGEOFMARRIAGE(R%, gndr%)
        '   AGEOFMARRIAGE(R%, Female)
        '   MARRIEDFEMSHR(R%, Age20_24)
        '
        'Returns True only when:
        '   1. The base variable names match, and
        '   2. The full dimensioned names match, OR
        '   3. Each dimension is compatible.
        '
        'Compatibility rule:
        '   If var1 has a generic/common dimension token, it can match a named
        '   dimension token in var2. A token is considered generic/common, when it is not a named element of that dimension
        '   i.e., when EnumIndexFromLiteral(token, GRPSEnum) returns 0
        '
        '   If var1 has a named dimension member and var2 also has a named
        '   dimension member, they must be identical. Otherwise, it is not a match.

        Dim base1 As String = ""
        Dim base2 As String = ""
        Dim dims1 As List(Of String) = Nothing
        Dim dims2 As List(Of String) = Nothing

        'Break each variable reference into base name and dimension list.
        ParseDimmedVar(var1, base1, dims1)
        ParseDimmedVar(var2, base2, dims2)

        'Base variable names must match.
        If Not String.Equals(base1, base2, StringComparison.OrdinalIgnoreCase) Then Return False

        'Exact full-name match is always valid.
        If String.Equals(var1.Trim(), var2.Trim(), StringComparison.OrdinalIgnoreCase) Then Return True

        'If one has dimensions and the other does not, or dimension counts differ, do not match.
        If dims1.Count <> dims2.Count Then Return False

        'This routine supports up to three sub-dimensions.
        If dims1.Count > 3 Then Return False

        'Compare each dimension position one by one.
        For i As Integer = 0 To dims1.Count - 1

            Dim d1 As String = dims1(i).Trim()
            Dim d2 As String = dims2(i).Trim()

            'Same dimension token matches immediately.
            If String.Equals(d1, d2, StringComparison.OrdinalIgnoreCase) Then Continue For

            ''A return value of 0 means the token is generic/common, not a named member.
            'Dim idx1 As Integer = EnumIndexFromLiteral(d1, GRPSEnum)
            'Dim idx2 As Integer = EnumIndexFromLiteral(d2, GRPSEnum)

            Dim idx1 As Integer = 0
            Dim idx2 As Integer = 0

            'If d1/d2 are positive integers, use them directly.
            'Otherwise, treat them as enum member names.
            If Not Integer.TryParse(d1, idx1) OrElse idx1 <= 0 Then
                idx1 = EnumIndexFromLiteral(d1, GRPSEnum)
            End If

            If Not Integer.TryParse(d2, idx2) OrElse idx2 <= 0 Then
                idx2 = EnumIndexFromLiteral(d2, GRPSEnum)
            End If


            'If the first variable has a generic/common dimension, accept it as a match.
            If idx1 = 0 Then Continue For

            'mti 2026/05/13 start (corection for ONFRAINDTRAN:: InfraIndTranComp(Road_Paved)	CSng(Math.Log(INFRAROADPAVEDPCNT(R%))), If IY > 1 Then
            If idx2 = 0 AndAlso Not base1.ToUpper.Contains("DEATH") Then Continue For  'some issuewith multidimensions?
            'If idx1 = idx2 Then Continue For
            'mti 2026/05/13 end

            'If both are named members, they must have matched exactly above.
            'Since they did not, this is not a match.
            If idx1 <> 0 AndAlso idx2 <> 0 Then Return False

            'If var1 has a named member but var2 is generic/common, do not match.
            Return False

        Next

        'All dimensions matched or were compatible.
        Return True

    End Function
    Private Function DimensionIsCommonIndex(
    ByVal varText As String,
    ByVal GRPSEnum(,) As String
) As Boolean

        'mti 2026/05/13 new procedure
        'Checks whether all dimensions of one IFs variable reference are
        'common/generic index dimensions rather than named dimension members.
        '
        'Examples:
        '
        'AGEOFMARRIAGE(R%, gndr%)       -> True
        'AGEOFMARRIAGE(R%, Female)      -> False
        'MARRIEDFEMSHR(R%, Age20_24)    -> False
        'INFRAROADPAVEDPCNT(R%)         -> True
        'GDPPCP                         -> True
        '
        'Rule:
        'A dimension token is treated as common/generic when:
        '
        '   EnumIndexFromLiteral(token, GRPSEnum) returns 0
        '
        'A positive numeric dimension is treated as a named dimension index,
        'and therefore is not considered common/generic.

        If varText Is Nothing Then Return True

        Dim baseName As String = ""
        Dim dims As List(Of String) = Nothing

        'Break the variable reference into base name and dimension list.
        ParseDimmedVar(varText, baseName, dims)

        'If there are no dimensions, there are no named dimensions.
        'So treat it as common/generic for this test.
        If dims Is Nothing OrElse dims.Count = 0 Then Return True

        'This routine follows the same practical limit used in
        'DimensionedVarsMatch.
        If dims.Count > 3 Then Return False

        For i As Integer = 0 To dims.Count - 1

            Dim d As String = dims(i).Trim()

            If d = "" Then Continue For

            Dim idx As Integer = 0

            'mti 2026/05/13 start
            'If the dimension token is a positive integer, treat it as a
            'specific named dimension index, not as a common index.
            '
            'Examples:
            '1 -> named index
            '2 -> named index
            '
            'So return False.
            'mti 2026/05/13 end
            If Integer.TryParse(d, idx) AndAlso idx > 0 Then
                Return False
            End If

            'mti 2026/05/13 start
            'If not a positive integer, check whether it is a named enum member.
            '
            'idx = 0 means common/generic index, such as:
            'R%
            'gndr%
            'j
            'k
            '
            'idx > 0 means named dimension member, such as:
            'Male
            'Female
            'Age20_24
            'Road_Paved
            'mti 2026/05/13 end
            idx = EnumIndexFromLiteral(d, GRPSEnum)

            If idx > 0 Then
                Return False
            End If

        Next

        'If no dimension was found to be a named enum member,
        'then all dimensions are common/generic indices.
        Return True

    End Function

    Private Sub ParseDimmedVar(ByVal fullVar As String,
                           ByRef baseName As String,
                           ByRef dims As List(Of String))
        'mti 2026/04/21 new procedure
        'Parses an IFs variable reference into:
        '   baseName: variable name before the opening parenthesis
        '   dims:     comma-separated dimension tokens inside parentheses
        '
        'Examples:
        '   AGEOFMARRIAGE(R%, Female)
        '       baseName = AGEOFMARRIAGE
        '       dims = R%, Female
        '
        '   POP(R%)
        '       baseName = POP
        '       dims = R%
        '   WGDP
        '       baseName = WGDP
        '       dims = empty lst

        dims = New List(Of String)

        If String.IsNullOrWhiteSpace(fullVar) Then
            baseName = ""
            Exit Sub
        End If

        Dim s As String = fullVar.Trim()

        'Find opening parenthesis. If absent, variable is non-dimensioned.
        Dim p1 As Integer = s.IndexOf("("c)

        If p1 < 0 Then
            baseName = s.Trim()
            Exit Sub
        End If

        'Everything before the opening parenthesis is the base variable name.
        baseName = s.Substring(0, p1).Trim()

        'Find closing parenthesis.
        Dim p2 As Integer = s.LastIndexOf(")"c)
        If p2 < p1 Then Exit Sub

        'Extract text between parentheses.
        Dim dimText As String = s.Substring(p1 + 1, p2 - p1 - 1)

        'Split dimensions by comma, trim spaces, ignore blanks, and keep up to three.
        dims = dimText.Split(","c).
                   Select(Function(x) x.Trim()).
                   Where(Function(x) x <> "").
                   Take(3).
                   ToList()

    End Sub
    Private Function PrepVBCode4IFsVar(LeftVar$, CurModule$, VCLn%) As String
        'mti 2025/08/09 start  Take this code for writing codelines to IFsVar(?) to a new procedure
        'Write Code for driver: code is displayed at 'View Code' Menu

        Dim TStruct(20) As String   '50
        Dim TStructNo(20) As Integer   '50
        Dim AddCode As Boolean
        Dim pos%, pos1%
        Dim vbcode$


        Dim UnfinishedNo%
        Dim TCBlock As String

        CodeArray(VCLn + 1) = "" 'insert a dummy blank, for loop convenience
        CodeArray2Block(VCLn + 1) = 5001

        'CodeArray(VCLn + 1) = 5001 'dummy upper limit
        vbcode = ""

        vbcode = vbcode + "'----Module " + CurModule + " ---------" + vbCr + vbCr


        'The following are the arrays used to complete unfinished structures
        'unfinished structures arise when structure > 10 lines
        'a marker saying complete structure at the designated line is added to the startblock
        'please see handlestructure routine
        'Erase TStructNo
        'ReDim TStructNo(20)
        'Erase TStruct
        'ReDim TStruct(20)

        'If LeftVar$.Trim = "AGDEM" Then Stop
        'If LeftVar$ = "AGLOSSTRANS" Then Stop

        CodeArray2Block(0) = 0
        UnfinishedNo = 0
        For i2 = 1 To VCLn ' To 1 Step -1

            'mti2025/07/14 start
            If CodeArray2Block(i2) = 0 Then Continue For
            'mti2025/07/14 end


            AddCode = True
            If CodeBlock(CodeArray2Block(i2)) = CodeBlock(CodeArray2Block(i2 - 1)) Then
                AddCode = False
            End If

            If AddCode Then
                'COMPLETE STRUCTURE:    Next tar%:477|COMPLETE STRUCTURE:        Next act%:477|


                TCBlock = CodeBlock(CodeArray2Block(i2))

                pos1 = 0
                'jrs 2023/01/10
                If Not (TCBlock Is Nothing) Then
                    pos = TCBlock.IndexOf("COMPLETE STRUCTURE:")

                    Do While pos >= 0
                        UnfinishedNo = UnfinishedNo + 1
                        pos = TCBlock.IndexOf(":", pos + 1)
                        pos1 = TCBlock.IndexOf(":", pos + 1)
                        TStruct(UnfinishedNo) = TCBlock.Substring(pos + 1, pos1 - pos - 1)  'identifies the structure that needs completion
                        pos = TCBlock.IndexOf("|", pos1) '"|"=End of marker |,will contain line number where thestruct will end
                        TStructNo(UnfinishedNo) = CInt(TCBlock.Substring(pos1 + 1, pos - pos1 - 1).Trim)  'gives the line number of end of structure


                        pos1 = pos 'save end of marker

                        pos = TCBlock.IndexOf("COMPLETE STRUCTURE:", pos + 1)
                    Loop

                    'discard complete structure marker , if any
                    'jrs 2023/07/26
                    If CodeBlock(CodeArray2Block(i2)) > "" Then
                        vbcode = vbcode + CodeBlock(CodeArray2Block(i2)).Substring(pos1 + 1) + vbCr '+ vbCr
                    Else
                        vbcode = vbcode + vbCr '+ vbCr
                    End If


                    Do While UnfinishedNo > 0
                        If CodeArray2Block(i2) >= TStructNo(UnfinishedNo) Then
                            vbcode = vbcode + vbCr + TStruct(UnfinishedNo)
                            UnfinishedNo = UnfinishedNo - 1
                        Else
                            Exit Do
                        End If
                    Loop
                End If

            End If

        Next i2

        Do While UnfinishedNo > 0
            vbcode = vbcode + vbCr + TStruct(UnfinishedNo)
            UnfinishedNo = UnfinishedNo - 1
        Loop

        'jrs 2023/01/10 using "" to insert " was giving a problem so I replaced it with """"
        vbcode = vbcode.Replace("""", """""")

        Return vbcode

        ''Add the Code to the Access Table
        'If VCLn > 0 Then
        '    'If LeftVar = "AGEOFMARRIAGE" And RightVar = Nothing Then Stop
        '    'jrs 2023/01/10 using "" to insert " was giving a problem so I replaced it with """"
        '    vbcode = vbcode.Replace("""", """""")

        '    db1Cmd.CommandText = "UPDATE IFsVar SET vbcode = vbcode || """ & vbcode & """ WHERE [NAME] = """ & LeftVar$ & """"
        '    db1Cmd.ExecuteNonQuery()

        'End If

    End Function
    Function cVarnamesWithDims(strExpression As String, Optional LeftVar As String = "") As List(Of String)
        'mti 2026/04/21 Take entire proc
        'mti 2025/08/26 - new routine
        'mti 2017/10/19 - procedure changed - take whole procedure
        'mti Documentation - whole proc
        Dim thisVar As String
        Dim i As Integer
        Dim varStart As Integer = -1
        'gvdb 2024/04/05
        ' unused variable
        'Dim varWord As String
        'Dim res As String

        Dim ArrayIndx As Boolean
        'If InStr(strExpression, "DYADPOPFOREIGNFORCED") > 0 Then Stop

        'i = strExpression.IndexOf("GDPPCP(R%), AMAX(2, TFR(R%))")
        'If i >= 0 Then Stop

        'If strExpression = "Amin(100, AMAX(0, 100 - (sWaterHH(R%, Surface, National) + sWaterHH(R%, Unimproved, National))))" Then Stop
        'If strExpression = "Amin(100, AMAX(0, 100 - (WATERHH(R%, Surface, National) + WATERHH(R%, Unimproved, National))))" Then Stop
        'If strExpression.Contains("Amin(100, AMAX(0, 100 - (sWaterHH(R%, Surface, National) + sWaterHH(R%, Unimproved, National))))") Then Stop


        cVarnamesWithDims = New List(Of String)

        ' extract all words from the text
        For i = 0 To strExpression.Length - 1
            'If i > 17 Then Stop
            Select Case AscW(strExpression.Substring(i, 1))
                Case 65 To 90, 97 To 122
                    ' variable always start with an alpha char
                    If varStart = -1 Then varStart = i
                Case 48 To 57, 95 '95="_"
                    ' digits can be suffix of a Varname
                Case Else
                    If varStart >= 0 Then
                        ' variable name will be extracted now,
                        'automatically exclude array index etc

                        'mti 2026/04/21 start - keeping original casing
                        'thisVar = strExpression.Substring(varStart, i - varStart).ToUpper   'mti 2025/08/26, stops at "(" at 19,, ACII 40; and counts 13 characters from 6 : : CSng((AGEOFMARRIAGE(R%, Male) * MalePop + 
                        thisVar = strExpression.Substring(varStart, i - varStart)

                        ArrayIndx = False
                        'If thisVar = "I" And strExpression.Substring(i, 1) <> "(" Then ArrayIndx = True 'i is not investment, array index , skip
                        'If thisVar = "C" And strExpression.Substring(i, 1) <> "(" Then ArrayIndx = True  'c is not consumption, array index , skip

                        If thisVar.ToUpper = "I" And strExpression.Substring(i, 1) <> "(" Then ArrayIndx = True 'i is not investment, array index , skip
                        If thisVar.ToUpper = "C" And strExpression.Substring(i, 1) <> "(" Then ArrayIndx = True  'c is not consumption, array index , skip



                        If Not ArrayIndx Then
                            'mti 2020/06/19 2020 End
                            'check if vb keyword
                            If Not IsKeyWord(thisVar.ToUpper) Then
                                'If Not IsKeyWord(thisVar) Then

                                ' add to collection, ignore if already there

                                'mti Nov 2010 Driver Changes

                                'GDPR growth rate correction picks up POPR, take it off
                                If thisVar.ToUpper = "IGDPRCOR" Then thisVar = "IGDPR"  'IGDPRCOR should be counted as IGDPR (to avoid getting POPR)
                                'population related intermediate variables in IFsEduc should be converted to POP
                                'mti 2017/10/19 start
                                If (thisVar.ToUpper = "PRIPOP" Or thisVar.ToUpper = "SECLOWRPOP" Or thisVar.ToUpper = "SECUPPRPOP" _
                                Or thisVar.ToUpper = "SECPOP" Or thisVar.ToUpper = "TERPOP" _
                                     Or thisVar.ToUpper = "AGEDST" Or thisVar.ToUpper = "FAGEDST") And (LeftVar <> "POP") Then thisVar = "POP"
                                'mti 2017/10/19 End
                                'mti Nov 2010 Driver Changes


                                'mti 2026/04/21 start
                                If thisVar.ToUpper = "D" And LeftVar = "POP" Then thisVar = "DEATHS"  'DEATHS(R%) = CSng(DEATHS(R%) + d(j%, R%, 1) + d(j%, R%, 2)) 'Later will be * 1000
                                'mti 2026/04/21 end

                                'mti 2026/05/13 start
                                'handle DeathCat via modmordstdet
                                'If thisVar.ToUpper = "MODMORDSTDET" Then thisVar = "DEATHCAT"
                                'mti 2026/05/13 end

                                If strExpression.Substring(i, 1) = "(" Then
                                    Dim LeftParen As Integer = 1
                                    Dim RightParen As Integer = 0
                                    Dim InParen As String = "("

                                    For j = i + 1 To strExpression.Length - 1

                                        Select Case strExpression.Substring(j, 1)
                                            Case "("
                                                LeftParen = LeftParen + 1
                                            Case ")"
                                                RightParen = RightParen + 1
                                            Case Else

                                        End Select
                                        InParen = InParen + strExpression.Substring(j, 1)
                                        If LeftParen = RightParen Then
                                            thisVar = thisVar + InParen
                                            i = j
                                            Exit For
                                        End If
                                    Next j


                                End If

                                ' ignore duplicate words - or duplication of collection keys
                                Try
                                    If Not cVarnamesWithDims.Contains(thisVar) Then
                                        cVarnamesWithDims.Add(thisVar)
                                        'mti 2025/08/26 dimensioned names here by checkingforopen and close paren add here 
                                        'check if the character at which it stopped is 
                                    End If
                                Catch ex As Exception

                                End Try
                            End If
                            'mti 2020/06/19 2020 Start
                        End If
                        'mti 2020/06/19 2020 End
                        ' reset varstart
                        varStart = -1
                    End If
            End Select
        Next

        ' account for the last var, if any
        If varStart >= 0 Then

            ' extract the word
            'thisVar = strExpression.Substring(varStart, i - varStart).ToUpper
            thisVar = strExpression.Substring(varStart, i - varStart)

            If Not IsKeyWord(thisVar.ToUpper) Then
                ' add to collection, ignore if already there
                Try
                    If Not cVarnamesWithDims.Contains(thisVar) Then cVarnamesWithDims.Add(thisVar)
                Catch ex As Exception

                End Try
            End If
        End If

    End Function
    Function cVarNamesWithDimsOld(strExpression As String, Optional LeftVar As String = "") As List(Of String)
        'mti 2025/08/07 new proc
        Dim results As New List(Of String)
        ParseExpr(strExpression, LeftVar, results)
        Return results
    End Function
    Public Sub ApplyIfDimMapsAll(ByRef LeftSide() As String,
                             ByRef RightSide() As String,
                             ByRef sLeftSide() As String,
                             ByRef sRightSide() As String)

        'mti 2025/08/07 new proc
        If LeftSide Is Nothing OrElse RightSide Is Nothing Then Exit Sub
        Dim n As Integer = Math.Min(LeftSide.Length, RightSide.Length)
        If n = 0 Then Exit Sub

        ' Ensure backups have the right size
        If sLeftSide Is Nothing OrElse sLeftSide.Length <> n Then ReDim sLeftSide(n - 1)
        If sRightSide Is Nothing OrElse sRightSide.Length <> n Then ReDim sRightSide(n - 1)

        For i As Integer = 0 To n - 1
            If IsNothing(LeftSide(i)) OrElse LeftSide(i) = "" Then
                sLeftSide(i) = LeftSide(i) ' Nothing
                sRightSide(i) = RightSide(i)
                Continue For
            End If

            'If i = 3644 Then Stop

            ' 1) Backup originals
            sLeftSide(i) = LeftSide(i)
            sRightSide(i) = RightSide(i)

            ' 2) Transform this pair without mutating arrays during parsing
            Dim newLeft As String = LeftSide(i)
            Dim newRight As String = RightSide(i)

            'If LeftSide(i) = "LABCOEFFS(R%, S%, sk%)" Then Stop

            'If i = 4794 Then Stop
            'If i = 7966 Then Stop  '7966

            'mti 2026/05/13 start replacing mortality distribution with Deathcat on the left sde with dimensionality fixed to R and k/i, and on the right side with dimensionality preserved (e.g., R and 2*k or 2*i+1)
            'newLeft = ReplaceModMordStdEtOld(newLeft)  'the if mapping is done next; however, if values of k are not properly converted yet; it only convertes expression to sigle letter for dimension  
            'newRight = ReplaceModMordStdEtOld(newRight)  'if k=10, should be converted to if k=(10+1)\2 here

            'If i = 22487 Then Stop
            'If i = 22488 Then Stop

            Dim replaced As Tuple(Of String, String) = ReplaceModMordStdEt(newLeft, newRight)

            newLeft = replaced.Item1
            newRight = replaced.Item2

            'mti 2026/05/13  - end

            TransformPairByIfMap(newLeft, newRight)


            'If i = 4095 Then Stop
            'If i = 4098 Then Stop

            'mti 2025/09/17 start - clean, e.g., remove trailing comma and space
            newLeft = CleanString(newLeft)
            newRight = CleanString(newRight)
            'mti 2025/09/17 end

            ' 3) Write back transformed values

            LeftSide(i) = newLeft
            RightSide(i) = newRight

            'If sLeftSide(i).Trim <> LeftSide(i).Trim Then Stop
            'If sRightSide(i).Trim <> RightSide(i).Trim Then Stop
        Next
    End Sub
    Function CleanString(s As String) As String
        'mti 2025/09/17 new procedure
        If String.IsNullOrEmpty(s) Then Return s
        While s.EndsWith(",") Or s.EndsWith(" ")
            s = s.Substring(0, s.Length - 1)
        End While
        Return s
    End Function
    ' Holds information found from the right-side If..Then expression.
    Private Class ModMordIndexInfoOld
        'mti 2026/05/13 new class
        ' Example: If k // 9 Then
        ' means k = 9 after CleanString changed "=" to "//".
        Public DirectAssignments As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

        ' Example: If k <> 13 Then Else Branch
        ' means that, in the Else Branch, k = 13.
        Public ElseBranchValues As New Dictionary(Of String, List(Of Integer))(StringComparer.OrdinalIgnoreCase)

    End Class

    Private Class ModMordIndexInfo
        'mti 2026/05/13 new class
        ' Stores inferred numeric values for index variables.
        '
        ' Examples:
        '   If k // 5 Then
        '       stores k = 5
        '
        '   If k <> 13 And k <> 14 Then Else Branch
        '       stores k = 13
        '       because the Else branch implies k = 13 Or k = 14,
        '       and we keep the first value when ambiguous.
        Public IndexValues As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

    End Class

    Private Function ReplaceModMordStdEt(ByVal leftSide As String,
                                      ByVal rightSide As String) As Tuple(Of String, String)
        'mti 2026/05/13 new procedure

        ' First read the right side to see whether it gives us values
        ' for indices such as k or i.
        Dim indexInfo As ModMordIndexInfo = GetModMordIndexInfo(rightSide)

        ' Then apply the same replacement logic to both sides.
        Dim newLeft As String = ReplaceModMordStdEtInOneString(leftSide, indexInfo)
        Dim newRight As String = ReplaceModMordStdEtInOneString(rightSide, indexInfo)

        Return Tuple.Create(newLeft, newRight)

    End Function


    Private Function ReplaceModMordStdEtInOneString(ByVal codeLine As String,
                                                 ByVal indexInfo As ModMordIndexInfo) As String
        'mti 2026/05/13 new procedure

        ' Note: this intentionally does NOT allow a space between
        ' modmordstdet and the opening parenthesis.
        '
        ' Captures:
        '   modmordstdet(dim1, dim2, dim3)
        '
        ' dim1 is discarded.
        ' dim2 becomes the first dimension of DEATHCAT.
        ' dim3 is transformed into the second dimension of DEATHCAT.
        Dim pattern As String =
        "\bmodmordstdet\(\s*([^,\)]+)\s*,\s*([^,\)]+)\s*,\s*([^,\)]+)\s*\)"

        Return Regex.Replace(
        codeLine,
        pattern,
        Function(m As Match)

            Dim dim1 As String = m.Groups(1).Value.Trim()
            Dim dim2 As String = m.Groups(2).Value.Trim()
            Dim dim3 As String = m.Groups(3).Value.Trim()

            ' Convert the old third dimension into the new DEATHCAT
            ' second dimension.
            '
            ' Example:
            '   If dim3 is k + 1, and the right side says k = 9,
            '   then dim3 becomes 10.
            '
            '   Then DEATHCAT second dimension becomes:
            '       (10 + 1) \ 2 = 5
            Dim deathCatDim2 As String = BuildDeathCatSecondDimension(dim3, indexInfo)

            Return "DEATHCAT(" & dim2 & ", " & deathCatDim2 & ", gndr)"

        End Function,
        RegexOptions.IgnoreCase
    )

    End Function
    Private Function ReplaceModMordStdEtOld(ByVal codeLine As String) As String
        'mti 2026/05/13 new procedure

        ' Replaces references to modmordstdet(dim1, dim2, dim3)
        ' with DEATHCAT(dim2, extracted dim3 index, gndr).
        '
        ' The first dimension of modmordstdet (age cohort) is discarded.
        ' The second dimension (country) becomes the first dimension of DEATHCAT.
        ' The third dimensio (disease index) becomes the second dimension of DEATHCAT.
        ' If the third dimension is a formula, such as 2*k or 2*i+1,
        ' only the index variable, such as k or i, is retained.
        'for third dimension of DEATHCAT the code insert "gndr" for gender

        Dim pattern As String =
        "\bmodmordstdet\s*\(\s*([^,\)]+)\s*,\s*([^,\)]+)\s*,\s*([^,\)]+)\s*\)"

        Return Regex.Replace(
        codeLine,
        pattern,
        Function(m As Match)

            Dim dim1 As String = m.Groups(1).Value.Trim() ' e.g., j
            Dim dim2 As String = m.Groups(2).Value.Trim() ' e.g., R
            Dim dim3 As String = m.Groups(3).Value.Trim() ' e.g., k, 2*k, 2*i+1

            ' If dim3 is a formula such as 2*k or 2*i+1,
            ' extract only the index variable: k or i.
            Dim newDim2 As String = ExtractIndexVariable(dim3)

            ' modmordstdet(j, R, k)       -> DEATHCAT(R, k, gndr)
            ' modmordstdet(j, R, 2*k)     -> DEATHCAT(R, k, gndr)
            ' modmordstdet(j, R, 2*i + 1) -> DEATHCAT(R, i, gndr)
            Return "DEATHCAT(" & dim2 & ", " & newDim2 & ", gndr)"

        End Function,
        RegexOptions.IgnoreCase
    )

    End Function


    Private Function ExtractIndexVariableOld(ByVal dimensionExpression As String) As String

        'mti 2026/05/13 new procedure
        Dim expr As String = dimensionExpression.Trim()

        ' Find the first variable-like token.
        ' Examples:
        '   k       -> k
        '   2*k     -> k
        '   2*i+1   -> i
        '   2 * age -> age
        Dim match As Match = Regex.Match(expr, "\b[A-Za-z_][A-Za-z0-9_]*\b")

        If match.Success Then
            Return match.Value
        End If

        ' Fallback: if no variable is found, return the original expression.
        Return expr

    End Function
    Private Function GetModMordIndexInfo(ByVal rightSide As String) As ModMordIndexInfo
        'mti 2026/05/13 new procedure
        Dim info As New ModMordIndexInfo()

        If String.IsNullOrWhiteSpace(rightSide) Then Return info

        ' Split the right side into comma-separated parts.
        '
        ' Part 0 is the real right-side expression.
        ' Parts 1 onward are If..Then branch conditions.
        '
        ' Example:
        '   DEATHTRPV(R) * VEHICFLPC(R) / 1000,
        '   If k < 19 Then,
        '   If (k <> 13 And k <> 14) Or hlvehsw // 0 Then Else Branch,
        '   If k < 19 Then
        Dim parts() As String = rightSide.Split(","c)

        If parts.Length <= 1 Then Return info

        ' Read all branch parts after the first comma, in order.
        For p As Integer = 1 To parts.Length - 1

            Dim branchText As String = parts(p).Trim()

            If branchText = "" Then Continue For

            ' Only process pieces that look like If..Then branches.
            If Not Regex.IsMatch(branchText, "\bIf\b", RegexOptions.IgnoreCase) Then
                Continue For
            End If

            Dim hasElse As Boolean =
            Regex.IsMatch(branchText, "\bElse\b", RegexOptions.IgnoreCase)

            If hasElse Then

                ' Example:
                '   If k <> 13 And k <> 14 Then Else Branch
                '
                ' Else branch means:
                '   k = 13 Or k = 14
                '
                ' We keep the first one: k = 13.
                AddValuesFromElseBranch(branchText, info)

            Else

                ' Example:
                '   If k // 5 Then
                '
                ' Treat // as equality, so:
                '   k = 5
                AddValuesFromDirectIfBranch(branchText, info)

            End If

        Next

        Return info

    End Function
    Private Sub AddValuesFromDirectIfBranch(ByVal branchText As String,
                                         ByVal info As ModMordIndexInfo)
        'mti 2026/05/13 new procedure
        ' Extract only the condition between If and Then.
        '
        ' Example:
        '   If k // 5 Then
        '
        ' conditionText becomes:
        '   k // 5
        Dim conditionText As String = ExtractConditionText(branchText)

        If conditionText = "" Then Exit Sub

        ' Match direct equality only.
        '
        ' Allows:
        '   k // 5
        '   k = 5
        '   k // 2 * 4 + 1
        '
        ' Does not treat <>, <=, or >= as equality.
        Dim directPattern As String =
        "\b([A-Za-z]\w*)\s*(?://|(?<![<>=])=(?![=]))\s*([-+]?\d+(?:\s*[\+\-\*/]\s*[-+]?\d+)*)"

        For Each m As Match In Regex.Matches(conditionText, directPattern, RegexOptions.IgnoreCase)

            Dim varName As String = m.Groups(1).Value.Trim()
            Dim valueText As String = m.Groups(2).Value.Trim()

            Dim value As Integer

            If TryEvaluateConstantExpression(valueText, value) Then

                ' Keep the first value found for this variable.
                ' This preserves the branch order.
                If Not info.IndexValues.ContainsKey(varName) Then
                    info.IndexValues(varName) = value
                End If

            End If

        Next

    End Sub
    Private Sub AddValuesFromElseBranch(ByVal branchText As String,
                                     ByVal info As ModMordIndexInfo)
        'mti 2026/05/13 new procedure
        ' Extract only the condition between If and Then.
        '
        ' Example:
        '   If (k <> 13 And k <> 14) Or hlvehsw // 0 Then Else Branch
        '
        ' conditionText becomes:
        '   (k <> 13 And k <> 14) Or hlvehsw // 0
        Dim conditionText As String = ExtractConditionText(branchText)

        If conditionText = "" Then Exit Sub

        ' In an Else Branch:
        '
        '   If k <> 13 Then Else Branch
        '
        ' means:
        '
        '   k = 13
        '
        ' And:
        '
        '   If k <> 13 And k <> 14 Then Else Branch
        '
        ' means:
        '
        '   k = 13 Or k = 14
        '
        ' We keep the first possible value.
        Dim notEqualPattern As String =
        "\b([A-Za-z]\w*)\s*<>\s*([-+]?\d+(?:\s*[\+\-\*/]\s*[-+]?\d+)*)"

        For Each m As Match In Regex.Matches(conditionText, notEqualPattern, RegexOptions.IgnoreCase)

            Dim varName As String = m.Groups(1).Value.Trim()
            Dim valueText As String = m.Groups(2).Value.Trim()

            Dim value As Integer

            If TryEvaluateConstantExpression(valueText, value) Then

                ' Keep the first value found for this variable.
                '
                ' Example:
                '   k <> 13 And k <> 14
                '
                ' stores only:
                '   k = 13
                If Not info.IndexValues.ContainsKey(varName) Then
                    info.IndexValues(varName) = value
                End If

            End If

        Next

    End Sub
    Private Function ExtractConditionText(ByVal branchText As String) As String
        'mti 2026/05/13 new procedure

        ' Takes:
        '   If (k <> 13 And k <> 14) Or hlvehsw // 0 Then Else Branch
        '
        ' Returns:
        '   (k <> 13 And k <> 14) Or hlvehsw // 0

        Dim m As Match =
        Regex.Match(branchText,
                    "\bIf\b(?<cond>.*?)(?:\bThen\b|$)",
                    RegexOptions.IgnoreCase)

        If m.Success Then
            Return m.Groups("cond").Value.Trim()
        End If

        Return ""

    End Function
    Private Function BuildDeathCatSecondDimension(ByVal dim3 As String,
                                               ByVal indexInfo As ModMordIndexInfo) As String
        'mti 2026/05/13 new procedure
        dim3 = dim3.Trim()

        ' If dim3 is already numeric, apply:
        '
        '   new dimension = (dim3 + 1) \ 2
        '
        ' Example:
        '   dim3 = 13
        '   result = 7
        Dim numericDim3 As Integer

        If TryEvaluateConstantExpression(dim3, numericDim3) Then
            Return ((numericDim3 + 1) \ 2).ToString()
        End If

        ' Find the index variable inside dim3.
        '
        ' Examples:
        '   k       -> k
        '   k + 1   -> k
        '   2 * k   -> k
        '   2*i + 1 -> i
        Dim indexVar As String = ExtractIndexVariable(dim3)

        If indexVar <> "" AndAlso indexInfo.IndexValues.ContainsKey(indexVar) Then

            Dim values As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

            values(indexVar) = indexInfo.IndexValues(indexVar)

            Dim evaluatedDim3 As Integer

            If TryEvaluateExpressionWithValues(dim3, values, evaluatedDim3) Then

                ' Final required transformation:
                '
                '   DEATHCAT second dimension = (thirdDimNo + 1) \ 2
                Return ((evaluatedDim3 + 1) \ 2).ToString()

            End If

        End If

        ' If no numeric value can be inferred, keep it symbolic.
        '
        ' Example:
        '   dim3 = k
        '
        ' result:
        '   ((k + 1) \ 2)
        Return "((" & dim3 & " + 1) \ 2)"

    End Function


    Private Function BuildConditionalDeathCatDimension(ByVal dim3 As String,
                                                   ByVal indexVar As String,
                                                   ByVal possibleValues As List(Of Integer)) As String
        'mti 2026/05/13 new procedure
        ' Fallback if none of the specific values matches.
        ' In a true Else Branch case, this fallback should usually not be reached,
        ' but keeping it makes the generated expression safer.
        Dim result As String = "((" & dim3.Trim() & " + 1) \ 2)"

        ' Build nested VB.NET If expressions from the back forward.
        For i As Integer = possibleValues.Count - 1 To 0 Step -1

            Dim thisIndexValue As Integer = possibleValues(i)

            Dim values As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
            values(indexVar) = thisIndexValue

            Dim evaluatedDim3 As Integer

            If TryEvaluateExpressionWithValues(dim3, values, evaluatedDim3) Then

                Dim mappedValue As Integer = (evaluatedDim3 + 1) \ 2

                result =
                "If(" & indexVar & " = " & thisIndexValue.ToString() &
                ", " & mappedValue.ToString() &
                ", " & result & ")"

            End If

        Next

        Return result

    End Function


    Private Function ExtractIndexVariable(ByVal expressionText As String) As String
        'mti 2026/05/13 new procedure
        ' Return the first variable-like token.
        '
        ' Examples:
        '   k       -> k
        '   k + 1   -> k
        '   2 * k   -> k
        '   2*i + 1 -> i
        Dim m As Match = Regex.Match(expressionText, "\b[A-Za-z]\w*\b")

        If m.Success Then
            Return m.Value.Trim()
        End If

        Return ""

    End Function


    Private Function TryEvaluateExpressionWithValues(ByVal expressionText As String,
                                                  ByVal values As Dictionary(Of String, Integer),
                                                  ByRef result As Integer) As Boolean
        'mti 2026/05/13 new procedure
        Dim expr As String = expressionText.Trim()

        ' Replace known variables with their numeric values.
        For Each kvp As KeyValuePair(Of String, Integer) In values

            expr = Regex.Replace(
            expr,
            "\b" & Regex.Escape(kvp.Key) & "\b",
            kvp.Value.ToString(),
            RegexOptions.IgnoreCase
        )

        Next

        ' If any variable remains, we cannot evaluate it numerically.
        If Regex.IsMatch(expr, "\b[A-Za-z]\w*\b") Then
            Return False
        End If

        Return TryEvaluateConstantExpression(expr, result)

    End Function


    Private Function TryEvaluateConstantExpression(ByVal expressionText As String,
                                                ByRef result As Integer) As Boolean
        'mti 2026/05/13 new procedure
        Dim expr As String = expressionText.Trim()

        If expr = "" Then Return False

        ' Only allow simple arithmetic.
        ' This avoids accidentally evaluating non-arithmetic text.
        If Not Regex.IsMatch(expr, "^[0-9\.\+\-\*/\(\)\s]+$") Then
            Return False
        End If

        Try
            Dim dt As New DataTable()
            Dim rawValue As Object = dt.Compute(expr, Nothing)

            ' Convert to integer. The model indices should be whole numbers.
            result = CInt(Math.Truncate(Convert.ToDouble(rawValue)))

            Return True

        Catch
            Return False
        End Try

    End Function
    Private Function GetFirstBranchCondition(ByVal rightSide As String) As String
        'mti 2026/05/13 new procedure
        ' Return only the text between the first and second comma.
        '
        ' Example:
        '   Expr, If k < 19 Then, If k <> 13 Then Else Branch
        '
        ' Returns:
        '   If k < 19 Then

        Dim firstComma As Integer = rightSide.IndexOf(","c)

        If firstComma < 0 Then
            Return ""
        End If

        Dim secondComma As Integer = rightSide.IndexOf(","c, firstComma + 1)

        If secondComma >= 0 Then

            Return rightSide.Substring(
            firstComma + 1,
            secondComma - firstComma - 1
        ).Trim()

        Else

            ' If there is no second comma, use everything after the first comma.
            '
            ' Example:
            '   Expr, If k // 5 Then
            '
            ' Returns:
            '   If k // 5 Then
            Return rightSide.Substring(firstComma + 1).Trim()

        End If

    End Function
    Private Sub TransformPairByIfMap(ByRef leftStr As String, ByRef rightStr As String)
        'mti 2025/12/16 Victory Day Start - revised procedure
        'Evaluate complexity per comma-separated If..Then branch.
        'If one or more SIMPLE (Token // Value) branches exist, apply those mappings.
        'Ignore complex branches (do not block simple ones).
        'mti 2025/12/16 Victory Day End

        If String.IsNullOrEmpty(rightStr) Then Exit Sub

        ' Only process when the line does NOT start with "If" or "ElseIf"
        Dim trimmed As String = rightStr.TrimStart()
        If trimmed.StartsWith("If", StringComparison.OrdinalIgnoreCase) _
       OrElse trimmed.StartsWith("ElseIf", StringComparison.OrdinalIgnoreCase) Then Exit Sub

        ' Find earliest "If" or "ElseIf" before a trailing apostrophe comment
        Dim idxApostrophe As Integer = rightStr.IndexOf("'"c)
        Dim searchLimit As Integer = If(idxApostrophe >= 0, idxApostrophe, rightStr.Length)

        Dim mGate As Match = Regex.Match(rightStr, "\b(ElseIf|If)\b", RegexOptions.IgnoreCase)
        If Not mGate.Success OrElse mGate.Index >= searchLimit Then Exit Sub

        ' Split into: expression part and condition part
        Dim gateIdx As Integer = mGate.Index
        Dim exprPart As String = rightStr.Substring(0, gateIdx)
        Dim condPart As String = rightStr.Substring(gateIdx)

        ' Work only with the "condition core" (between If/ElseIf and Then)
        Dim condCore As String = ExtractConditionCore(condPart)
        If String.IsNullOrWhiteSpace(condCore) Then Exit Sub

        ' Split condition core by comma (your branches are comma-separated)
        ' Use nesting-aware split so commas inside (...) do not break branches
        Dim branches As String() = SplitArgumentsRespectingNesting(condCore)

        ' Collect SIMPLE mappings found in any branch
        Dim maps As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)


        For Each brRaw As String In branches
            Dim br As String = If(brRaw, "").Trim()
            If br = "" Then Continue For


            'mti 2026/04/21 start: split on And, Or etc
            'Split complex branch into smaller condition pieces.
            'Example:
            'S% // 1 And ylm(R%) <> 1
            'becomes:
            'S% // 1
            'ylm(R%) <> 1
            Dim subBranches() As String = Regex.Split(br,
                    "\s+\b(?:OrElse|AndAlso|Or|And)\b\s+",
                    RegexOptions.IgnoreCase)

            For Each subRaw As String In subBranches

                Dim subBr As String = If(subRaw, "").Trim()
                If subBr = "" Then Continue For

                'Only process substitution-style equality: 'Token // Value;  'Token // -1;  '(Token // Value) 'only handling equals to (replaced earlier by //); not handling, <, >, <>, >=, <=
                Dim simple = Regex.Match(subBr, "^\s*\(?\s*(?<tok>[A-Za-z_]\w*%?)\s*//\s*(?<val>[A-Za-z_]\w*%?|-?\d+)\s*\)?\s*$", RegexOptions.IgnoreCase)

                If simple.Success Then
                    Dim tok As String = simple.Groups("tok").Value.Trim()
                    Dim val As String = simple.Groups("val").Value.Trim()

                    If tok <> "" AndAlso val <> "" Then
                        maps(tok) = val   'last wins
                    End If
                End If

            Next
            'mti 2026/04/21 end

            '' Decide complexity PER branch
            'Dim isComplexBranch As Boolean =
            'Regex.IsMatch(br, "\b(OrElse|AndAlso|Or|And)\b", RegexOptions.IgnoreCase) OrElse
            'Regex.IsMatch(br, "[<>]=?", RegexOptions.IgnoreCase)

            'If isComplexBranch Then
            '    'Complex branch: ignore it (do not substitute)
            '    Continue For
            'End If

            '' SIMPLE branch: must be a clean "(Token // Value)" or "Token // Value"
            'Dim simple = Regex.Match(br,
            '                     "^\s*\(?\s*(?<tok>[A-Za-z_]\w*%?)\s*//\s*(?<val>[A-Za-z_]\w*%?|\-?\d+)\s*\)?\s*$",
            '                     RegexOptions.IgnoreCase)

            'If simple.Success Then
            '    Dim tok As String = simple.Groups("tok").Value.Trim()
            '    Dim val As String = simple.Groups("val").Value.Trim()
            '    If tok <> "" AndAlso val <> "" Then
            '        maps(tok) = val  'last wins (same token repeated in multiple simple branches)
            '    End If
            'End If
        Next

        ' If we found at least one SIMPLE mapping, apply it
        If maps.Count = 0 Then Exit Sub

        Dim newExpr As String = exprPart
        Dim newLeft As String = leftStr

        For Each kvp In maps
            Dim tok As String = kvp.Key
            Dim val As String = kvp.Value
            Dim pat As String = "(?<=\(|,)\s*" & Regex.Escape(tok) & "\s*(?=,|\))"

            newExpr = Regex.Replace(newExpr, pat, val, RegexOptions.IgnoreCase)

            If Not String.IsNullOrEmpty(newLeft) Then
                newLeft = Regex.Replace(newLeft, pat, val, RegexOptions.IgnoreCase)
            End If
        Next

        rightStr = newExpr & condPart
        leftStr = newLeft
    End Sub
    Private Function ExtractConditionCore(condPart As String) As String
        'mti 2025/12/16 Victory Day Start - helper for revised procedure
        Dim mIf As Match = Regex.Match(condPart, "^\s*(ElseIf|If)\b", RegexOptions.IgnoreCase)
        If Not mIf.Success Then Return ""

        Dim mThen As Match = Regex.Match(condPart, "\bThen\b", RegexOptions.IgnoreCase)
        If Not mThen.Success Then Return ""

        Dim core As String = condPart.Substring(mIf.Length, mThen.Index - mIf.Length)
        Return core.Trim()
    End Function

    Private Sub TransformPairByIfMap_org(ByRef leftStr As String, ByRef rightStr As String)
        'mti 2025/12/16 revised this procedure: saving the old just in case
        'mti 2025/08/07 new proc

        ' Transforms a single (Left, Right) pair in-place using token // value mappings parsed
        ' from the condition part of Right (after the first real "If" or "ElseIf" found before any apostrophe comment).
        ' If the line starts with "If" or "ElseIf", or no usable mapping is found, the pair is left unchanged.

        If String.IsNullOrEmpty(rightStr) Then Exit Sub

        ' Only process when the line does NOT start with "If" or "ElseIf"
        Dim trimmed As String = rightStr.TrimStart()
        If trimmed.StartsWith("If", StringComparison.OrdinalIgnoreCase) _
       OrElse trimmed.StartsWith("ElseIf", StringComparison.OrdinalIgnoreCase) Then Exit Sub

        ' Find earliest "If" or "ElseIf" before a trailing apostrophe comment
        Dim idxApostrophe As Integer = rightStr.IndexOf("'"c)
        Dim searchLimit As Integer = If(idxApostrophe >= 0, idxApostrophe, rightStr.Length)

        Dim m As Match = Regex.Match(rightStr, "\b(ElseIf|If)\b", RegexOptions.IgnoreCase)
        If Not m.Success OrElse m.Index >= searchLimit Then Exit Sub

        ' Split right side into: expression (before If/ElseIf) and condition (from If/ElseIf onward)
        Dim gateIdx As Integer = m.Index
        Dim exprPart As String = rightStr.Substring(0, gateIdx)  'mti 2025/12/16 - change to gateidx -1; to avoid taking comma
        Dim condPart As String = rightStr.Substring(gateIdx)

        ' Parse mappings of the form: <token> // <value> (e.g., e% // 3 or e% // Hydro)
        Dim maps As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

        Dim scan As Integer = 0
        While True
            Dim idxDblSlash As Integer = condPart.IndexOf("//", scan, StringComparison.Ordinal)
            If idxDblSlash = -1 Then Exit While

            ' ----- token on the left of // -----
            Dim leftEnd As Integer = idxDblSlash - 1
            While leftEnd >= 0 AndAlso Char.IsWhiteSpace(condPart(leftEnd))
                leftEnd -= 1
            End While
            Dim leftStart As Integer = leftEnd
            While leftStart >= 0 AndAlso (Char.IsLetterOrDigit(condPart(leftStart)) OrElse condPart(leftStart) = "%"c OrElse condPart(leftStart) = "_"c)
                leftStart -= 1
            End While
            leftStart += 1

            Dim token As String = ""
            If leftStart <= leftEnd AndAlso leftStart >= 0 AndAlso leftEnd < condPart.Length Then
                token = condPart.Substring(leftStart, leftEnd - leftStart + 1)
            End If

            ' ----- value on the right of // -----
            Dim rightStart As Integer = idxDblSlash + 2
            While rightStart < condPart.Length AndAlso Char.IsWhiteSpace(condPart(rightStart))
                rightStart += 1
            End While
            Dim rightEnd As Integer = rightStart
            ' Allow letters/digits/underscore/%/dot/minus (e.g., 3, -1, e%, grp_1, Hydro)
            While rightEnd < condPart.Length AndAlso
              (Char.IsLetterOrDigit(condPart(rightEnd)) OrElse condPart(rightEnd) = "_"c OrElse
               condPart(rightEnd) = "%"c OrElse condPart(rightEnd) = "."c OrElse condPart(rightEnd) = "-"c)
                rightEnd += 1
            End While

            Dim value As String = ""
            If rightStart < rightEnd Then
                value = condPart.Substring(rightStart, rightEnd - rightStart).Trim()
            End If

            If token.Length > 0 AndAlso value.Length > 0 Then
                maps(token) = value   ' last mapping wins for same token
            End If

            scan = Math.Max(rightEnd, idxDblSlash + 2)
        End While

        If maps.Count = 0 Then Exit Sub

        ' Replace only when the token is a standalone argument inside parentheses:
        ' pattern: (?<=\(|,)\s*TOK\s*(?=,|\))
        Dim newExpr As String = exprPart
        Dim newLeft As String = leftStr

        For Each kvp In maps
            Dim tok As String = kvp.Key
            Dim val As String = kvp.Value
            Dim pat As String = "(?<=\(|,)\s*" & Regex.Escape(tok) & "\s*(?=,|\))"

            newExpr = Regex.Replace(newExpr, pat, val, RegexOptions.IgnoreCase)

            If Not String.IsNullOrEmpty(newLeft) Then
                newLeft = Regex.Replace(newLeft, pat, val, RegexOptions.IgnoreCase)
            End If
        Next

        rightStr = newExpr & condPart
        leftStr = newLeft
    End Sub


    Private Sub TransformPairByIfMapOld(ByRef leftStr As String, ByRef rightStr As String)
        'mti 2025/08/07 new proc
        If String.IsNullOrEmpty(rightStr) Then Exit Sub

        ' Only process when the line does NOT start with "If"
        Dim trimmed As String = rightStr.TrimStart()
        If trimmed.StartsWith("If", StringComparison.OrdinalIgnoreCase) Then Exit Sub

        ' Find the first real "If" (ignore one that appears after an apostrophe comment)
        Dim idxApostrophe As Integer = rightStr.IndexOf("'"c)
        Dim mIf As Match = Regex.Match(rightStr, "\bIf\b", RegexOptions.IgnoreCase)
        If Not mIf.Success Then Exit Sub
        If idxApostrophe >= 0 AndAlso mIf.Index > idxApostrophe Then Exit Sub ' "If" is inside trailing comment

        ' Split right side into: expression (before If) and condition (from If onward)
        Dim exprPart As String = rightStr.Substring(0, mIf.Index)
        Dim condPart As String = rightStr.Substring(mIf.Index)

        ' Parse mappings of the form: <token> // <value> (e.g., e% // 3)
        Dim maps As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

        Dim scan As Integer = 0
        While True
            Dim idxDblSlash As Integer = condPart.IndexOf("//", scan, StringComparison.Ordinal)
            If idxDblSlash = -1 Then Exit While

            ' ----- token on the left of // -----
            Dim leftEnd As Integer = idxDblSlash - 1
            While leftEnd >= 0 AndAlso Char.IsWhiteSpace(condPart(leftEnd))
                leftEnd -= 1
            End While
            Dim leftStart As Integer = leftEnd
            While leftStart >= 0 AndAlso (Char.IsLetterOrDigit(condPart(leftStart)) OrElse condPart(leftStart) = "%"c OrElse condPart(leftStart) = "_"c)
                leftStart -= 1
            End While
            leftStart += 1

            Dim token As String = ""
            If leftStart <= leftEnd AndAlso leftStart >= 0 AndAlso leftEnd < condPart.Length Then
                token = condPart.Substring(leftStart, leftEnd - leftStart + 1)
            End If

            ' ----- value on the right of // -----
            Dim rightStart As Integer = idxDblSlash + 2
            While rightStart < condPart.Length AndAlso Char.IsWhiteSpace(condPart(rightStart))
                rightStart += 1
            End While
            Dim rightEnd As Integer = rightStart
            ' Allow letters/digits/underscore/%/dot/minus (e.g., 3, -1, e%, grp_1)
            While rightEnd < condPart.Length AndAlso
              (Char.IsLetterOrDigit(condPart(rightEnd)) OrElse condPart(rightEnd) = "_"c OrElse
               condPart(rightEnd) = "%"c OrElse condPart(rightEnd) = "."c OrElse condPart(rightEnd) = "-"c)
                rightEnd += 1
            End While

            Dim value As String = ""
            If rightStart < rightEnd Then
                value = condPart.Substring(rightStart, rightEnd - rightStart).Trim()
            End If

            If token.Length > 0 AndAlso value.Length > 0 Then
                maps(token) = value   ' last mapping wins for same token
            End If

            scan = Math.Max(rightEnd, idxDblSlash + 2)
        End While

        If maps.Count = 0 Then Exit Sub

        ' Replace only when the token is a standalone argument inside parentheses:
        ' pattern: (?<=\(|,)\s*TOK\s*(?=,|\))
        Dim newExpr As String = exprPart
        Dim newLeft As String = leftStr

        For Each kvp In maps
            Dim tok As String = kvp.Key
            Dim val As String = kvp.Value
            Dim pat As String = "(?<=\(|,)\s*" & Regex.Escape(tok) & "\s*(?=,|\))"

            newExpr = Regex.Replace(newExpr, pat, val, RegexOptions.IgnoreCase)

            If Not String.IsNullOrEmpty(newLeft) Then
                newLeft = Regex.Replace(newLeft, pat, val, RegexOptions.IgnoreCase)
            End If
        Next

        rightStr = newExpr & condPart
        leftStr = newLeft
    End Sub
    Public Function GetDimMappedPair(ByVal LeftSide() As String,
                                 ByVal RightSide() As String,
                                 ByVal i As Integer) As (Left As String, Right As String)

        'mti 2025/08/07 new proc
        ' Pull originals safely (do not mutate arrays)
        Dim origLeft As String = If(LeftSide IsNot Nothing AndAlso i >= 0 AndAlso i < LeftSide.Length, LeftSide(i), Nothing)
        Dim origRight As String = If(RightSide IsNot Nothing AndAlso i >= 0 AndAlso i < RightSide.Length, RightSide(i), Nothing)

        ' Default: return originals if anything is off
        If String.IsNullOrEmpty(origRight) Then Return (origLeft, origRight)

        ' Only process when the line does NOT start with "If"
        Dim trimmed As String = origRight.TrimStart()
        If trimmed.StartsWith("If", StringComparison.OrdinalIgnoreCase) Then
            Return (origLeft, origRight)
        End If

        ' Locate the first real "If" (ignore one that appears after a VB apostrophe comment)
        Dim idxApostrophe As Integer = origRight.IndexOf("'"c)
        Dim mIf As Match = Regex.Match(origRight, "\bIf\b", RegexOptions.IgnoreCase)
        If Not mIf.Success Then
            Return (origLeft, origRight)
        End If
        If idxApostrophe >= 0 AndAlso mIf.Index > idxApostrophe Then
            ' "If" appears only in a trailing comment -> do nothing
            Return (origLeft, origRight)
        End If

        ' Split RightSide(i) into expression part (left of If) and condition part (from If onward)
        Dim exprPart As String = origRight.Substring(0, mIf.Index)
        Dim condPart As String = origRight.Substring(mIf.Index)

        ' Parse mappings: <token> // <value> from the condition
        Dim maps As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

        Dim scan As Integer = 0
        While True
            Dim idxDblSlash As Integer = condPart.IndexOf("//", scan, StringComparison.Ordinal)
            If idxDblSlash = -1 Then Exit While

            ' ---- left token (word before //) ----
            Dim leftEnd As Integer = idxDblSlash - 1
            While leftEnd >= 0 AndAlso Char.IsWhiteSpace(condPart(leftEnd))
                leftEnd -= 1
            End While
            Dim leftStart As Integer = leftEnd
            While leftStart >= 0 AndAlso (Char.IsLetterOrDigit(condPart(leftStart)) OrElse condPart(leftStart) = "%"c OrElse condPart(leftStart) = "_"c)
                leftStart -= 1
            End While
            leftStart += 1

            Dim leftTok As String = ""
            If leftStart <= leftEnd AndAlso leftStart >= 0 AndAlso leftEnd < condPart.Length Then
                leftTok = condPart.Substring(leftStart, leftEnd - leftStart + 1)
            End If

            ' ---- right value (word after //) ----
            Dim rightStart As Integer = idxDblSlash + 2
            While rightStart < condPart.Length AndAlso Char.IsWhiteSpace(condPart(rightStart))
                rightStart += 1
            End While
            Dim rightEnd As Integer = rightStart
            ' allow letters/digits/underscore/%/dot/minus
            While rightEnd < condPart.Length AndAlso
              (Char.IsLetterOrDigit(condPart(rightEnd)) OrElse condPart(rightEnd) = "_"c OrElse
               condPart(rightEnd) = "%"c OrElse condPart(rightEnd) = "."c OrElse condPart(rightEnd) = "-"c)
                rightEnd += 1
            End While

            Dim rightVal As String = ""
            If rightStart < rightEnd Then
                rightVal = condPart.Substring(rightStart, rightEnd - rightStart).Trim()
            End If

            If leftTok.Length > 0 AndAlso rightVal.Length > 0 Then
                maps(leftTok) = rightVal   ' last one wins for same token
            End If

            scan = Math.Max(rightEnd, idxDblSlash + 2)
        End While

        ' If no maps, return originals
        If maps.Count = 0 Then
            Return (origLeft, origRight)
        End If

        ' Replacement pattern: replace only standalone args in parentheses lists:
        ' (?<=\(|,)\s*TOKEN\s*(?=,|\))
        Dim newExpr As String = exprPart
        Dim newLeft As String = origLeft

        For Each kvp In maps
            Dim token As String = kvp.Key
            Dim value As String = kvp.Value
            Dim pat As String = "(?<=\(|,)\s*" & Regex.Escape(token) & "\s*(?=,|\))"

            ' Apply to the IFs/expression part (before If) of RightSide(i)
            newExpr = Regex.Replace(newExpr, pat, value, RegexOptions.IgnoreCase)

            ' Apply to LeftSide(i) inside its argument list(s)
            If Not String.IsNullOrEmpty(newLeft) Then
                newLeft = Regex.Replace(newLeft, pat, value, RegexOptions.IgnoreCase)
            End If
        Next

        ' Recombine the right side
        Dim newRight As String = newExpr & condPart

        Return (newLeft, newRight)
    End Function
    Public Function ReplaceSubDimsFromIfMaps(line As String) As String
        'mti 2025/08/07 new proc
        If line Is Nothing Then Return line

        ' 1) Only process lines where "If" occurs later (not at the beginning)
        Dim trimmed = line.TrimStart()
        If trimmed.StartsWith("If", StringComparison.OrdinalIgnoreCase) Then
            Return line
        End If

        ' 2) Ignore "If" that appears only inside a trailing VB comment (')
        Dim idxComment As Integer = line.IndexOf("'"c)
        Dim mIf As Match = Regex.Match(line, "\bIf\b", RegexOptions.IgnoreCase)
        If Not mIf.Success Then
            Return line
        End If
        If idxComment >= 0 AndAlso mIf.Index > idxComment Then
            ' The first "If" is inside a comment—do nothing
            Return line
        End If

        ' 3) Split into expression (left) and condition (right starting at "If")
        Dim exprPart As String = line.Substring(0, mIf.Index)
        Dim condPart As String = line.Substring(mIf.Index)

        ' 4) From the condition part, collect mappings of the form: <token> // <value>
        '    We parse words around '//' without relying on complex regex.
        Dim maps As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

        Dim i As Integer = 0
        While True
            Dim idxDblSlash As Integer = condPart.IndexOf("//", i, StringComparison.Ordinal)
            If idxDblSlash = -1 Then Exit While

            ' ----- left token (word before //) -----
            Dim leftEnd As Integer = idxDblSlash - 1
            While leftEnd >= 0 AndAlso Char.IsWhiteSpace(condPart(leftEnd))
                leftEnd -= 1
            End While

            Dim leftStart As Integer = leftEnd
            While leftStart >= 0 AndAlso (Char.IsLetterOrDigit(condPart(leftStart)) OrElse condPart(leftStart) = "%"c OrElse condPart(leftStart) = "_"c)
                leftStart -= 1
            End While
            leftStart += 1

            Dim leftTok As String = ""
            If leftStart <= leftEnd AndAlso leftStart >= 0 AndAlso leftEnd < condPart.Length Then
                leftTok = condPart.Substring(leftStart, leftEnd - leftStart + 1)
            End If

            ' ----- right token (word after //) -----
            Dim rightStart As Integer = idxDblSlash + 2
            While rightStart < condPart.Length AndAlso Char.IsWhiteSpace(condPart(rightStart))
                rightStart += 1
            End While

            Dim rightEnd As Integer = rightStart
            ' allow letters/digits/underscore/%/dot/minus in value
            While rightEnd < condPart.Length AndAlso (Char.IsLetterOrDigit(condPart(rightEnd)) OrElse condPart(rightEnd) = "_"c OrElse condPart(rightEnd) = "%"c OrElse condPart(rightEnd) = "."c OrElse condPart(rightEnd) = "-"c)
                rightEnd += 1
            End While

            Dim rightVal As String = ""
            If rightStart < rightEnd Then
                rightVal = condPart.Substring(rightStart, rightEnd - rightStart).Trim()
            End If

            If leftTok.Length > 0 AndAlso rightVal.Length > 0 Then
                maps(leftTok) = rightVal   ' later pairs override earlier ones for the same token
            End If

            i = Math.Max(rightEnd, idxDblSlash + 2)
        End While

        If maps.Count = 0 Then
            ' Nothing to replace
            Return line
        End If

        ' 5) Apply replacements ONLY inside parentheses argument lists in exprPart.
        '    For each token K, replace occurrences that are standalone args between ( or , and , or )
        '    Pattern: (?<=\(|,)\s*K\s*(?=,|\))
        Dim resultExpr As String = exprPart

        For Each kvp In maps
            Dim dimTok As String = kvp.Key
            Dim value As String = kvp.Value

            Dim pat As String = "(?<=\(|,)\s*" & Regex.Escape(dimTok) & "\s*(?=,|\))"
            resultExpr = Regex.Replace(resultExpr, pat, value, RegexOptions.IgnoreCase)
        Next

        ' 6) Recombine
        Return resultExpr & condPart
    End Function
    Private Sub ParseExpr(expr As String, LeftVar As String, results As List(Of String))
        'mti 2025/08/07 new proc
        Dim i As Integer = 0
        Dim n As Integer = expr.Length

        While i < n
            Dim ch As Char = expr(i)

            ' Start of identifier (letter or underscore)
            If Char.IsLetter(ch) OrElse ch = "_"c Then
                Dim start As Integer = i
                i += 1
                While i < n AndAlso (Char.IsLetterOrDigit(expr(i)) OrElse expr(i) = "_"c)
                    i += 1
                End While

                Dim baseName As String = expr.Substring(start, i - start)
                Dim baseUpper As String = baseName.ToUpperInvariant()

                ' Skip whitespace after the name
                While i < n AndAlso Char.IsWhiteSpace(expr(i))
                    i += 1
                End While

                Dim hasParen As Boolean = (i < n AndAlso expr(i) = "("c)

                If hasParen Then
                    ' Parse full parenthesis block with nesting
                    Dim depth As Integer = 0
                    Dim parenStart As Integer = i
                    Do
                        If expr(i) = "("c Then
                            depth += 1
                        ElseIf expr(i) = ")"c Then
                            depth -= 1
                        End If
                        i += 1
                        If i >= n Then Exit Do
                    Loop While depth > 0

                    Dim fullName As String = expr.Substring(start, i - start)

                    If Not IsKeyWord(baseUpper) Then
                        ' Treat as a variable with dimensions
                        If String.IsNullOrEmpty(LeftVar) OrElse Not baseUpper.Equals(LeftVar.ToUpperInvariant) Then
                            If Not results.Contains(fullName) Then
                                results.Add(fullName)
                            End If
                        End If
                    Else
                        ' For keywords, recurse into the argument list
                        Dim inner As String = expr.Substring(parenStart + 1, i - parenStart - 2)
                        ParseExpr(inner, LeftVar, results)
                    End If

                Else
                    ' No parentheses: standalone name
                    If Not IsKeyWord(baseUpper) Then
                        If String.IsNullOrEmpty(LeftVar) OrElse Not baseUpper.Equals(LeftVar.ToUpperInvariant) Then
                            If Not results.Contains(baseUpper) Then
                                results.Add(baseUpper)
                            End If
                        End If
                    End If
                End If

            Else
                i += 1
            End If
        End While
    End Sub
    'Default arguments: 5 2100 -1 true true 1 false --log jrs.txt
    'jrs 2022/08/30
    Public Function CreateConsoleMenu() As String
        Console.WriteLine("IFs Menu")
        Console.WriteLine("--------")
        Console.WriteLine("(1) Mark Historical Variables used in Rebuild Base")
        Console.WriteLine("(2) Mark Functions used in Model")
        Console.WriteLine("(3) Build Documentation")
        Console.WriteLine("(x) Exit")
        Console.WriteLine("")
        Dim opt = Console.ReadLine()
        CreateConsoleMenu = opt
    End Function

End Module
