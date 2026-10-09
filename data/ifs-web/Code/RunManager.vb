
Imports System.Collections.Specialized.BitVector32
Imports NLog

Public NotInheritable Class RunManager
    Private Shared ReadOnly _instance As New Lazy(Of RunManager)(Function() New RunManager(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication)

    Private Shared _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

    Private Const MaxSimultRuns As Integer = 4

    Private SessionList As Dictionary(Of String, RunProcess) = New Dictionary(Of String, RunProcess)

    Private Sub New()
    End Sub

    ''' <summary>
    ''' Instance property handle to have a single instance
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property Instance() As RunManager
        Get
            Return _instance.Value
        End Get
    End Property

    Public Function CanStartRun(SessionId As String) As Boolean
        SyncLock SessionList
            If SessionList.Count < MaxSimultRuns And Not SessionList.ContainsKey(SessionId) Then
                Return True
            Else
                _logger.Info($"CanStartRun False - SessionId:{SessionId}, count={SessionList.Count}")
                Return False
            End If

        End SyncLock

        Return False
    End Function

    Public Function StartRun(SessionID As String) As Boolean
        SyncLock SessionList
            If SessionList.ContainsKey(SessionID) Then
                If Not SessionList(SessionID).ModelProcess.Start() Then
                    _logger.Error($"StartRun failed with exit code: {SessionList(SessionID).ModelProcess.ExitCode}")
                    'SessionList(SessionID).ModelProcess.StandardError
                    Return False
                Else
                    _logger.Debug($"StartRun - {SessionList(SessionID).ModelProcess?.StartInfo?.FileName} with args:{SessionList(SessionID).ModelProcess?.StartInfo?.Arguments}")
                End If
                Return True
            End If
            Return False
        End SyncLock
    End Function

    Public Function PrepareRun(SessionID As String, DemandFile As String, RunYears As String, IsStandAlone As Boolean, IncludeHist As Boolean, ProgressFilePath As String, Process As Integer, Optional UseSQlite As Boolean = True, Optional UseDevMode As Boolean = False) As Boolean
        Dim Proc As RunProcess = New RunProcess(SessionID, DemandFile, RunYears, IsStandAlone, IncludeHist, ProgressFilePath, Process, UseSQlite, UseDevMode)
        SyncLock SessionList
            If SessionList.Count < MaxSimultRuns And Not SessionList.ContainsKey(SessionID) Then
                SessionList.Add(SessionID, Proc)
                Return True
            End If
            Return False
        End SyncLock
    End Function

    Public Function IsRunning(SessionID As String) As Boolean
        SyncLock SessionList
            If SessionList.ContainsKey(SessionID) Then
                Return SessionList(SessionID).ModelProcess.Id <> 0 And Not SessionList(SessionID).ModelProcess.HasExited
            Else
                Return False
            End If
        End SyncLock
    End Function

    Public Function ClearAll() As Boolean
        SyncLock SessionList
            For Each pair As KeyValuePair(Of String, RunProcess) In SessionList
                pair.Value.ModelProcess.Kill()
                _logger.Debug($"ClearAll - kill process for SessionID:{pair.Key}")
            Next
            SessionList.Clear()
            Return True
        End SyncLock
    End Function

    Public Function RemoveSession(SessionID As String) As Boolean
        SyncLock SessionList
            If SessionList.ContainsKey(SessionID) Then
                Dim p As RunProcess = SessionList(SessionID)
                If Not p.ModelProcess.HasExited Then
                    p.ModelProcess.Kill()
                    _logger.Debug($"RemoveSession - kill process for sessionID:{SessionID}")
                End If
                SessionList.Remove(SessionID)
                _logger.Info($"RFemoveSession - session {SessionID} removed")
            End If
            Return True
        End SyncLock
    End Function

    Public Function GetRunProcess(SessionID As String) As RunProcess
        SyncLock SessionList
            If SessionList.ContainsKey(SessionID) Then
                Return SessionList(SessionID)
            Else
                Return Nothing
            End If
        End SyncLock
    End Function
End Class
