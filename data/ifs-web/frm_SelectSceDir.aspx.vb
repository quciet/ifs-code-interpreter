Option Strict Off
Option Explicit On

Imports MarkItUp.WebControls
Imports NLog
Partial Class frm_SelectSceDir
    Inherits Page

    Private Sub frm_SelectSceDir_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        InitializeComponent()

        If Not Page.IsPostBack Then
            Timer1.Visible = False
            txtYear.Text = Session("runTimeHorizon")
            LoadMenuFiles(Session("ScenarioFileDir"), Nothing, treeRunSce, "")
        End If
    End Sub

    Protected WithEvents IfsVB60 As System.Diagnostics.Process

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.IfsVB60 = New System.Diagnostics.Process

    End Sub

    Private Sub cmdContinue_Click(sender As Object, e As EventArgs) Handles cmdContinue.Click
        IfsVB60 = Session("IfsVB60")
        'jrs 2021/02/24 added if not is nothing
        If Not (IfsVB60 Is Nothing) AndAlso Not IfsVB60.HasExited Then
            IfsVB60.Kill()
        End If

        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Kill(Session("PutFdrvRun") & "progress.txt")
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        'jrs 2007/08/06 passing the folder where to look
        cmdSelect.Enabled = False
        cmdSelect.Visible = False
        cmdContinue.Text = "Interrupt"
        Dim sceNode As TreeNode
        Dim sceDir As String = ""
        sceNode = treeRunSce.SelectedNode.Parent
        Do While Not sceNode Is Nothing
            sceDir = sceNode.Text & "\" & sceDir
            sceNode = sceNode.Parent
        Loop

        Dim sDirName = Session("ScenarioFileDir") & sceDir & treeRunSce.SelectedNode.Text
        Session("runTimeHorizon") = txtYear.Text

        If sDirName = "" Then Exit Sub

        'Scenarioflag = True
        'frm_MainMenu.IFsGUIctrl1.cSensitivityFlag = False
        'bMonitor = True

        'Load list of Scnearios
        'Dim session("fileList")() As String

        Session("FilesSelected") = -1

        ReadSceFiles(sDirName, Session("FilesSelected"), Session("fileList"))
        Dim X As String()
        Dim P As Integer

        'jrs 2005/03/21
        lblRunYear.Visible = False
        txtYear.Visible = False
        treeRunSce.Visible = False
        lblInst2.Visible = True
        lblInst3.Visible = True

        'frm_InvisibleModeMonitor.Show
        TitleCell.Text = "Scenario Batch Monitor"
        lblInst1.Text = "International Futures is running one or more scenarios/cases."

        'Load frm_Run
        'frm_Run.txt_RunYears = runTimeHorizon

        'Process each Scneario
        Dim i As Integer
        'jrs 2008/05/06
        Dim sFilesExist As String = ""

        'For i = 0 To FilesSelected
        '    X = Split(fileList(i), "\")
        '    'if file already exists check if it should be replaced
        '    If Dir(Session("PutFdrvRun") & Left(X(UBound(X)), Len(X(UBound(X))) - 6) & "run.db") > "" Then
        '        If UCase(Left(X(UBound(X)), Len(X(UBound(X))) - 6)) & "RUN.DB" <> "IFSBASE.RUN.DB" Then
        '            sFilesExist = sFilesExist & "<br>" & Left(X(UBound(X)), Len(X(UBound(X))) - 6) & "run.db"
        '        End If
        '    End If
        'Next i

        'If sFilesExist > "" Then
        '    If MsgBox("Are you sure you want to replace these files: <br>" & sFilesExist, vbYesNo, "Batch Run") = vbNo Then
        '        Exit Sub
        '    End If
        'End If

        'jrs 2010/09/09
        'StopRun = False

        'jrs 2023/06/26 added = so that it can also run when there's only 1 file selected 
        If Session("FilesSelected") >= 0 Then
            Session("BatchRunIdx") = 0
            i = 0
            X = Split(Session("fileList")(i), "\")
            If UCase(Left(X(UBound(X)), Len(X(UBound(X))) - 6)) & "RUN.DB" = "IFSBASE.RUN.DB" Then
                i = i + 1
            End If
            Session("SceFileName") = Left(X(UBound(X)), Len(X(UBound(X))) - 3)
            'jrs 2005/03/21
            lblRunNumber.Text = Trim(Str(i + 1)) + " of " + Trim(Str(Session("FilesSelected") + 1))

            'jrs 2020/07/03
            Dim bHist As Boolean = False
            'jrs 2020/07/02 added .db to base.run and basehist.run
            If Session("HistoricBase") = "True" Then
                FileCopyCheck(Session("PutFdrvRun") + "IFsBaseHist.Run.db", Session("PutFdrvRun") + "working.run.db")
                bHist = True
            Else
                FileCopyCheck(Session("PutFdrvRun") + "IFsBase.Run.db", Session("PutFdrvRun") + "working.run.db")
            End If
            FileCopyCheck(PutDataFileDir + "SAMBase.db", Session("PutFdrvRun") + "SAMWorking.db")
            FileCopy(Session("fileList")(i), Session("ScenarioFileDir") + "Working.sce")

            'jrs 2017/01/06
            Dim InFile$ = DataFileDir$ + "IFsForDyadic.db"
            If Dir(InFile$) > "" Then
                Dim OutFile$ = Session("PutFdrvRun") + "IFsForDyadicWork.db"
                FileCopyCheck(InFile$, OutFile$)
            End If

            'jrs 2021/12/01
            Dim sFileName, sWorkDir As String

            'jrs 2022/08/03
            Dim sExeDir As String
            If UseOnlySQLite Then
                sExeDir = "net8"
            Else
                sExeDir = "net471"
            End If

            If DevelopmentMode = True And Dir(StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\") > "" Then
                sFileName = StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\" & DemandFile
                sWorkDir = StartDirec & "Code.Ifs-Translation\"
            Else
                sWorkDir = StartDirec & sExeDir & "\" 'jrs 2024/05/01
                sFileName = sWorkDir & DemandFile
            End If
            'sWorkDir = StartDirec & "net471\"
            IfsVB60.StartInfo.FileName = sFileName
            IfsVB60.StartInfo.WorkingDirectory = sWorkDir
            IfsVB60.StartInfo.Arguments = "5 " & Trim(Session("runTimeHorizon")) & " " & Trim(Session("SessionIdx")) & " False False 1 " & bHist.ToString & " --log ifslog.txt"

            Dim fs As Object
            fs = CreateObject("Scripting.FileSystemObject")
            If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
                Kill(Session("PutFdrvRun") & "progress.txt")
            End If

            If fs.FileExists(Session("PutFdrvRun") & "ErrorSce.txt") Then
                Kill(Session("PutFdrvRun") & "ErrorSce.txt")
            End If

            'jrs 2025/10/10
            Dim _logger As NLog.Logger = LogManager.GetCurrentClassLogger()
            _logger.Debug("Before Starting the batch run process")
            _logger.Debug("FileName: " & IfsVB60.StartInfo.FileName)
            _logger.Debug("Working Directory: " & IfsVB60.StartInfo.WorkingDirectory)
            _logger.Debug("aRguments: " & IfsVB60.StartInfo.Arguments)
            IfsVB60.StartInfo.UseShellExecute = False

            IfsVB60.Start()
            Session("IfsVB60") = IfsVB60
            Timer1.Visible = True

            _logger.Debug("After Starting the batch run process")
        End If

    End Sub

    Private Sub Timer1_Elapsed(sender As Object, e As TimerElapsedEventArgs) Handles Timer1.Elapsed
        IfsVB60 = Session("IfsVB60")

        Session("RunYears") = Session("runTimeHorizon")

        'mti WebForm Changes From June 21, 2004  - inserted for testing 
        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")

        If fs.FileExists(Session("PutFdrvRun") & "ErrorSce.txt") Then
            If Not IfsVB60.HasExited Then
                IfsVB60.Kill()
            End If

            Timer1.Visible = False

            Dim intfile As Integer, sLine As String = ""
            intfile = FreeFile()
            FileOpen(intfile, Session("PutFdrvRun") & "ErrorSce.txt", OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
            Do While Not EOF(intfile)
                sLine = LineInput(intfile)
            Loop
            FileClose(intfile)

            lblInst1.Text = "Error in Scenario: " & sLine
            cmdContinue.Text = "(C)ontinue"
            Exit Sub
        End If

        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Dim intfile As Integer
            Dim sLine(Val(Session("runTimeHorizon")) - Session("FirstYear") + 1) As String
            Dim idx As Integer
            idx = 0
            intfile = FreeFile()
            FileOpen(intfile, Session("PutFdrvRun") & "progress.txt", OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
            Do While Not EOF(intfile)
                sLine(idx) = LineInput(intfile)
                idx = idx + 1
            Loop
            FileClose(intfile)
            Session("RunningYear") = idx ' - 1
            lblYearRun.Text = Trim(Str(idx - 1 + Session("FirstYear")))
        End If
        'mti WebForm Changes From June 21, 2004  -end commented for testing

        If IfsVB60.HasExited() And Session("RunningYear") = Session("RunYears") - Session("FirstYear") + 1 Then
            FileCopy(Session("PutFdrvRun") + "working.run.db", Session("PutFdrvRun") & Session("SceFileName") & "run.db")
            FileCopy(Session("PutFdrvRun") + "SAMWorking.db", Session("PutFdrvRun") + "SAM" + Session("SceFileName") + "db")
            FileCopy(Session("PutFdrvRun") & "IFsPopWork.db", Session("PutFdrvRun") + "IFsPop" + Session("SceFileName") + "db")
            If Dir(Session("PutFdrvRun") + "IFsForDyadicWork.db") <> "" Then
                Dim InFile As String, OutFile As String
                InFile = Session("PutFdrvRun") & "IFsForDyadicWork.db"
                OutFile = Session("PutFdrvRun") & "IFsForDyadic" & Session("SceFileName") + "db"
                FileCopy(InFile$, OutFile$) 'Save in DATA
            End If
            'jrs 2022/05/24
            Session("IFsRunFiles").Add(Session("SceFileName") & "run.db", True)

            Session("BatchRunIdx") = Session("BatchRunIdx") + 1
            If Session("BatchRunIdx") <= Session("FilesSelected") Then
                Dim i As Integer
                i = Session("BatchRunIdx")
                Dim X As String()
                X = Split(Session("fileList")(i), "\")
                If UCase(Left(X(UBound(X)), Len(X(UBound(X))) - 6)) & "RUN.DB" = "IFSBASE.RUN.DB" Then
                    i = i + 1
                End If
                Session("SceFileName") = Left(X(UBound(X)), Len(X(UBound(X))) - 3)
                lblRunNumber.Text = Trim(Str(i + 1)) + " of " + Trim(Str(Session("FilesSelected") + 1))
                lblYearRun.Text = ""

                If Session("HistoricBase") = "True" Then
                    FileCopyCheck(Session("PutFdrvRun") + "IFsBaseHist.Run.db", Session("PutFdrvRun") + "working.run.db")
                Else
                    FileCopyCheck(Session("PutFdrvRun") + "IFsBase.Run.db", Session("PutFdrvRun") + "working.run.db")
                End If
                FileCopyCheck(PutDataFileDir + "SAMBase.db", Session("PutFdrvRun") + "SAMWorking.db")
                FileCopy(Session("fileList")(i), Session("ScenarioFileDir") + "Working.sce")

                'jrs 2017/01/06
                Dim InFile$ = DataFileDir$ + "IFsForDyadic.db"
                If Dir(InFile$) > "" Then
                    Dim OutFile$ = Session("PutFdrvRun") + "IFsForDyadicWork.db"
                    FileCopyCheck(InFile$, OutFile$)
                End If

                'IfsVB60.StartInfo.FileName = StartDirec & "net471\" & DemandFile
                'IfsVB60.StartInfo.WorkingDirectory = StartDirec & "net471\"
                'IfsVB60.StartInfo.Arguments = "5 " & Trim(Session("runTimeHorizon")) & " " & Trim(Session("SessionIdx")) & " False False 1"

                fs = CreateObject("Scripting.FileSystemObject")
                If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
                    Kill(Session("PutFdrvRun") & "progress.txt")
                End If

                IfsVB60.Start()
                Session("IfsVB60") = IfsVB60

            Else
                Timer1.Visible = False
                lblInst1.Text = "International Futures has finished running one or more scenarios/cases."
                cmdContinue.Text = "(C)ontinue"

                'jrs 2023/07/03
                Dim sIP As String
                Dim Files As DataTable
                sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")

                If IsNothing(Session("VersionName")) Then
                    SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r, IFsInit as i WHERE r.VersionName = i.Value AND i.Variable = 'VersionName' ORDER BY FileName", "", Files)
                Else
                    SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r WHERE r.VersionName = """ & Session("VersionName") & """ ORDER BY FileName", "", Files)
                End If

                Session("IFsRunFiles") = New MyFiles
                LoadRunFiles(Session("IFsRunFiles"), Session("PutFdrvRun2"), Files, Session("mnuSQLite"))

                'jrs 2023/11/02
                Session("dfCol") = Nothing
                Session("FilesCol") = Nothing

                    'jrs 2024/03/05
                    Dim FileIdx As Integer
                    For FileIdx = 0 To Session("IFsRunFiles").Count - 1
                        'Reset all dataframes from Working file
                        For i = 1 To NumbVars
                            Session("df" & V(i) & FileIdx.ToString) = Nothing
                        Next
                        Session("FileForDisplay" & FileIdx.ToString) = Nothing
                    Next
            End If
        End If
    End Sub

End Class
