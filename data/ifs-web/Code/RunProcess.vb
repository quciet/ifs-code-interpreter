Imports System.IO
Imports NLog

Public NotInheritable Class RunProcess

    Private Shared _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

    Private Property SessionID As String
    Private Property IsStandAlone As Boolean

    Private Property UseSQlite As Boolean

    Private Property UseDevMode As Boolean

    Private Property DemandFile As String

    Public ReadOnly Property RunYears As Short

    Private Property IncludeHist As Boolean

    Private ReadOnly Property ExeDir As String = "net8"

    Public ReadOnly Property ProgressFilePath As String

    Private ReadOnly Property Process As Integer

    Public ReadOnly ModelProcess As Process = New Process

    Public Sub New(SessionID As String, DemandFile As String, RunYears As String, IsStandAlone As Boolean, IncludeHist As Boolean, ProgressFilePath As String, Process As Integer, Optional UseSQlite As Boolean = True, Optional UseDevMode As Boolean = False)
        _logger.Debug($"RunProcess - SessionID:{SessionID}, DemandFile:{DemandFile}, RunYears:{RunYears}, IsStandAlone:{IsStandAlone}, IncludeHist:{IncludeHist}, UseSQLite:{UseSQlite}, UseDevMode:{UseDevMode}, ProgressFilePath:{ProgressFilePath}, Process:{Process}")
        Me.SessionID = SessionID
        Me.DemandFile = DemandFile
        Me.RunYears = Convert.ToInt16(RunYears)
        Me.IsStandAlone = IsStandAlone
        Me.IncludeHist = IncludeHist
        Me.UseSQlite = UseOnlySQLite
        Me.UseDevMode = UseDevMode
        Me.ProgressFilePath = ProgressFilePath
        Me.Process = Process

        SetWorkDir()
        SetFileName()
        SetArguments()
        'jrs 2026/06/02 ModelProcess.StartInfo.UseShellExecute = False
        'Try to use Parallel processing, this makes the process completly independent of IIS, using True didn't work, it stills hangs after 2026
        ModelProcess.StartInfo.UseShellExecute = False
    End Sub

    Private Sub SetWorkDir()
        'jrs 2024/05/01 check that the folder exists to run in development mode even without the code
        If IsStandAlone And UseDevMode And File_Exists(StartDirec & "Code.Ifs-Translation\", True) Then
            ModelProcess.StartInfo.WorkingDirectory = StartDirec & "Code.Ifs-Translation\"
        Else
            ModelProcess.StartInfo.WorkingDirectory = StartDirec & ExeDir
        End If
        If Not File_Exists(ModelProcess.StartInfo.WorkingDirectory, True) Then
            Throw New ArgumentException($"The working directory {ModelProcess.StartInfo.WorkingDirectory} does not exist")
        End If
        _logger.Debug($"SetWorkDir: '{ModelProcess.StartInfo.WorkingDirectory}'")
    End Sub

    Private Sub SetFileName()
        'jrs 2024/05/01 check that the folder exists to run in development mode even without the code
        If IsStandAlone And UseDevMode And File_Exists(StartDirec & "Code.Ifs-Translation\", True) Then
            ModelProcess.StartInfo.FileName = StartDirec & "Code.Ifs-Translation\bin\Debug\" & ExeDir & "\" & DemandFile
        Else
            ModelProcess.StartInfo.FileName = StartDirec & ExeDir & "\" & DemandFile
        End If
        If Not File_Exists(ModelProcess.StartInfo.FileName) Then
            Throw New ArgumentException($"The file name {ModelProcess.StartInfo.FileName} does not exist")
        End If
        _logger.Debug($"SetFileName: '{ModelProcess.StartInfo.FileName}'")
    End Sub

    Private Sub SetArguments()
        If IsStandAlone Then
            If UseOnlySQLite Then
                If UseDevMode Then
                    ModelProcess.StartInfo.Arguments = " 5 " & Trim(Me.RunYears) & " " & Me.Process & " " & " False False 1 " & IncludeHist & " --log ifslog.txt"
                    'Trim(Me.SessionID)

                Else
                    ModelProcess.StartInfo.Arguments = " 5 " & Trim(RunYears) & " " & Me.Process & " " & " False False 1 " & IncludeHist & " --log ifslog.txt"
                    'Trim(Me.SessionID)
                End If
            Else
                ModelProcess.StartInfo.Arguments = " 5 " & Trim(RunYears) & " " & Me.Process & " " & " False False 1 " & IncludeHist & " --log ifslog.txt"
                'Trim(Me.SessionID)
            End If
        Else
            If UseOnlySQLite Then
                If UseDevMode Then
                    ModelProcess.StartInfo.Arguments = " 5 " & Trim(RunYears) & " " & Me.Process & " False False 1 "
                    ' Trim(Me.SessionID)
                Else
                    ModelProcess.StartInfo.Arguments = " 5 " & Trim(RunYears) & " " & Me.Process & " False False 1 " & IncludeHist
                    'Trim(Me.SessionID)
                End If
            Else
                ModelProcess.StartInfo.Arguments = " 5 " & Trim(RunYears) & " " & Me.Process & " False"
            End If
        End If
        _logger.Debug($"SetUrguments: '{ModelProcess.StartInfo.Arguments}'")
    End Sub

    Public Overrides Function ToString() As String
        Return $"RunProcess{{[SessionId={Me.SessionID}][IsStandAlone={Me.IsStandAlone}][UseSQlite={Me.UseSQlite}][UseDevMode={Me.UseDevMode}]" &
            $"[DemandFile={Me.DemandFile}][RunYears={Me.RunYears}][IncludeHist={Me.IncludeHist}][ExeDir={Me.ExeDir}][ProgressFilePath={ProgressFilePath}]}}"
    End Function
End Class


