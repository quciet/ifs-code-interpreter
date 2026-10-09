Imports System.IO
Imports System.Net.Sockets
Imports System.Net.WebSockets
Imports System.Threading
Imports System.Web.WebSockets
Imports Accord.Statistics.Kernels
Imports EO.Web.Internal
Imports Newtonsoft.Json
Imports NLog

Public Class RebuildHttpHandler
    Implements IHttpHandler, IReadOnlySessionState

    Private Shared _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

    ''' <summary>
    ''' Configuration property read from IFsInit.db on key 'RunModelProcessRetryCount'
    ''' which is used to repeatedly check for the presence of the invoked process.
    ''' The default value of 10 is used when no value is found in the IFsInit.db and
    ''' a warning is written to the log.
    ''' </summary>
    Private RunModelProcessRetryCount As Int16 = 10

    ''' <summary>
    ''' Configuration property read from IFsInit.db on key 'RunModelProgressFileRetryCount'
    ''' which is used to repeatedly check for the presence of progress.txt file.
    ''' The default value of 10 is used when no value is found in the IFsInit.db and
    ''' a warning is written to the log.
    ''' </summary>
    Private RunModelProgressFileRetryCount As Int16 = 10

    ''' <summary>
    ''' Configuration property read from IFsInit.db on key 'RunModelWaitForExitRetryCount'
    ''' which is used to repeatedly check for the presence of executing model process before exit.
    ''' The retry period is set to 500 msecs.
    ''' The default value of 10 is used when no value is found in the IFsInit.db and
    ''' a warning is written to the log.
    ''' </summary>
    Private RunModelWaitForExitRetryCount As Int16 = 10

    Private Sub IHttpHandler_ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest

        _logger.Debug($"SessionID:{context.Session.SessionID}")

        Dim sVal As String

        Dim tab = context.Session("valtblInit")

        sVal = SeekValue(context.Session("valtblInit"), "RunModelProcessRetryCount", "Value")
        If sVal IsNot "" Then
            RunModelProcessRetryCount = Convert.ToInt16(sVal)

        Else
            _logger.Warn($"The variable RunModelProcessRetryCount was not found is IFsInit.db - using the default value of 10")
        End If

        sVal = SeekValue(context.Session("valtblInit"), "RunModelProgressFileRetryCount", "Value")
        If sVal IsNot "" Then
            RunModelProgressFileRetryCount = Convert.ToInt16(sVal)
        Else
            _logger.Warn($"The variable RunModelProgressFileRetryCount was not found is IFsInit.db - using the default value of 10")
        End If

        sVal = SeekValue(context.Session("valtblInit"), "RunModelWaitForExitRetryCount", "Value")
        If sVal IsNot "" Then
            RunModelWaitForExitRetryCount = Convert.ToInt16(sVal)
        Else
            _logger.Warn($"The variable RunModelWaitForExitRetryCount was not found is IFsInit.db - using the default value of 10")
        End If

        If context.IsWebSocketRequest Then
            context.AcceptWebSocketRequest(Async Function(wsContext As AspNetWebSocketContext)

                                               Dim sID As String
                                               Dim status As RunStatus
                                               Dim outBuf As ArraySegment(Of Byte)
                                               Dim json As String

                                               Dim ws As WebSocket = wsContext.WebSocket
                                               If ws.State = WebSocketState.Open Then
                                                   _logger.Debug($"RunHttpHandler - websocket open")

                                                   Dim bytes(128) As Byte
                                                   outBuf = New ArraySegment(Of Byte)(bytes)

                                                   Dim receiveResult As WebSocketReceiveResult = Await ws.ReceiveAsync(outBuf, CancellationToken.None)

                                                   Dim obj As SessInit = JsonConvert.DeserializeObject(Of SessInit)(System.Text.Encoding.UTF8.GetString(outBuf.ToArray()))

                                                   sID = obj.SessionID
                                                   _logger.Debug($"RunHttpHandler - websocket open for sessionID:{sID}")

                                               End If

                                               Dim hasError As Boolean = False
                                               Dim hasErrorFile As Boolean = False
                                               Dim progressFile As String
                                               Dim errorFile As String
                                               Dim fs As FileStream
                                               Dim sr As StreamReader
                                               Dim retryCount As Integer = 0

                                               Try


                                                   ' We wait 10 times for the process to start running. If this is found too short depending on the environment, just increase
                                                   While Not RunManager.Instance.IsRunning(sID) And retryCount < RunModelProcessRetryCount
                                                       _logger.Debug($"unHttpHandler - Not yet running. Waiting 1000 msecs and retry.")
                                                       Thread.Sleep(1000)
                                                       retryCount += 1
                                                   End While

                                                   Dim runProcess As RunProcess = RunManager.Instance.GetRunProcess(sID)
                                                   _logger.Debug($"HttpHandler - runProcess for SessionID:{sID} - runProcess:{runProcess}")


                                                   ' If no running process can be found then we abort.
                                                   If runProcess Is Nothing Then
                                                       _logger.Info($"No running process was found for Session {sID} after all retries. Closing down web socket.")
                                                       status = New RunStatus With {
                                                        .Timestamp = DateTime.Now,
                                                        .IsRunning = False,
                                                        .HasError = True,
                                                        .Status = StatusReason.NOT_STARTED
                                                       }
                                                       json = JsonConvert.SerializeObject(status)
                                                       outBuf = New ArraySegment(Of Byte)(Encoding.UTF8.GetBytes(json))
                                                       Await ws.SendAsync(outBuf, WebSocketMessageType.Text, True, CancellationToken.None)
                                                       Await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "desc", CancellationToken.None)
                                                       Return Nothing
                                                   End If

                                                   Dim year As Short = 2000
                                                   progressFile = runProcess.ProgressFilePath + "progress.txt"
                                                   errorFile = runProcess.ProgressFilePath + "ErrorSce.txt"

                                                   retryCount = 0
                                                   While Not File.Exists(progressFile) And retryCount < RunModelProgressFileRetryCount
                                                       _logger.Info($"HttpHandler - Waiting for progressFile to become available.")
                                                       status = New RunStatus With {
                                                                    .Timestamp = DateTime.Now,
                                                                    .IsRunning = False,
                                                                    .HasError = False,
                                                                    .Status = StatusReason.WAITING_PROGRESS_FILE
                                                                   }
                                                       json = JsonConvert.SerializeObject(status)
                                                       outBuf = New ArraySegment(Of Byte)(Encoding.UTF8.GetBytes(json))
                                                       Await ws.SendAsync(outBuf, WebSocketMessageType.Text, True, CancellationToken.None)
                                                       retryCount += 1

                                                       Thread.Sleep(5000)
                                                   End While

                                                   If Not File.Exists(progressFile) Then
                                                       _logger.Error($"No progressFile has become available after several retries Closing down web socket.")
                                                       status = New RunStatus With {
                                                                        .Timestamp = DateTime.Now,
                                                                        .IsRunning = False,
                                                                        .HasError = True,
                                                                        .Status = StatusReason.NO_PROGRESS_FILE
                                                                    }
                                                       json = JsonConvert.SerializeObject(status)
                                                       outBuf = New ArraySegment(Of Byte)(Encoding.UTF8.GetBytes(json))
                                                       Await ws.SendAsync(outBuf, WebSocketMessageType.Text, True, CancellationToken.None)
                                                       Await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "desc", CancellationToken.None)
                                                       Return Nothing
                                                   Else
                                                       _logger.Debug($"HttpHandler - ProgressFile found.")
                                                   End If

                                                   ' Here the running process was found so we start looking for the progress file
                                                   fs = New FileStream(progressFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                                                   sr = New StreamReader(fs)

                                                   While ws.State = WebSocketState.Open AndAlso year < runProcess.RunYears

                                                       If File.Exists(errorFile) Then
                                                           Throw New RunErrorFileException()
                                                       End If

                                                       Dim line As String = sr.ReadLine()

                                                       If line Is Nothing Then
                                                           Thread.Sleep(500)
                                                           Continue While
                                                       End If

                                                       Dim valueArray() As String = Split(line, ",")

                                                       status = New RunStatus
                                                       status.Timestamp = DateTime.Now
                                                       status.IsRunning = True
                                                       status.RunValues = New List(Of RunData)
                                                       status.RunValues.Add(New RunData _
                                                   With {.Year = Convert.ToInt16(valueArray(0)), .PopValue = Convert.ToSingle(valueArray(1)), .GdpValue = Convert.ToSingle(valueArray(2)), .FstValue = Convert.ToSingle(valueArray(3)), .Co2Value = Convert.ToSingle(valueArray(4))})

                                                       year = Convert.ToInt16(valueArray(0))

                                                       json = JsonConvert.SerializeObject(status)
                                                       outBuf = New ArraySegment(Of Byte)(Encoding.UTF8.GetBytes(json))
                                                       Await ws.SendAsync(outBuf, WebSocketMessageType.Text, True, CancellationToken.None)
                                                       _logger.Debug($"HttpHandler - send {json}")
                                                   End While
                                               Catch ex As RunErrorFileException
                                                   _logger.Error(ex, $"Errorfile detected in HttpRunHandler")
                                                   hasErrorFile = True
                                               Catch ex As Exception
                                                   _logger.Error(ex, $"Unexpected error in HttpRunHandler")
                                                   hasError = True
                                               Finally
                                                   If Not IsNothing(sr) Then
                                                       sr.Close()
                                                       sr.Dispose()
                                                   End If
                                                   If Not IsNothing(fs) Then
                                                       fs.Close()
                                                       fs.Dispose()
                                                   End If
                                               End Try

                                               ' This awaited function can not be called in the Exception handler so this is done using a status variable
                                               If hasError Then
                                                   status = New RunStatus With {
                                                        .Timestamp = DateTime.Now,
                                                        .IsRunning = False,
                                                        .HasError = True,
                                                        .Status = StatusReason.UNEXPECTED
                                                       }
                                                   json = JsonConvert.SerializeObject(status)
                                                   outBuf = New ArraySegment(Of Byte)(Encoding.UTF8.GetBytes(json))
                                                   Await ws.SendAsync(outBuf, WebSocketMessageType.Text, True, CancellationToken.None)
                                               ElseIf hasErrorFile Then

                                                   Dim errorLine As String
                                                   Using efs As New FileStream(errorFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                                                       Using esr As StreamReader = New StreamReader(efs)

                                                           While Not sr.EndOfStream
                                                               errorLine = esr.ReadLine()
                                                           End While
                                                       End Using
                                                   End Using

                                                   ' legacy error message handling
                                                   If errorLine = "Cannot store 'NaN' values." Then
                                                       errorLine += " NaN values are Not a Number, like 0/0, or values produced by doing a division by 0 or by a very small number."
                                                   End If

                                                   status = New RunStatus With {
                                                        .Timestamp = DateTime.Now,
                                                        .IsRunning = False,
                                                        .HasError = True,
                                                        .Status = StatusReason.ERROR_FILE,
                                                        .StatusMessage = "Error in Scenario: " & errorLine
                                                       }
                                                   json = JsonConvert.SerializeObject(status)
                                                   outBuf = New ArraySegment(Of Byte)(Encoding.UTF8.GetBytes(json))
                                                   Await ws.SendAsync(outBuf, WebSocketMessageType.Text, True, CancellationToken.None)
                                               Else
                                                   'we wait until the model run process has written all data to the file and exits
                                                   retryCount = 0
                                                   While RunManager.Instance.IsRunning(sID) And retryCount < RunModelWaitForExitRetryCount
                                                       Thread.Sleep(500)
                                                   End While

                                                   status = New RunStatus With {
                                                        .Timestamp = DateTime.Now,
                                                        .IsRunning = False,
                                                        .HasError = False,
                                                        .Status = StatusReason.COMPLETED
                                                       }
                                                   json = JsonConvert.SerializeObject(status)
                                                   outBuf = New ArraySegment(Of Byte)(Encoding.UTF8.GetBytes(json))
                                                   Await ws.SendAsync(outBuf, WebSocketMessageType.Text, True, CancellationToken.None)

                                               End If

                                               RunManager.Instance.RemoveSession(sID)
                                               _logger.Debug($"HttpHandler - End of run")
                                               Await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "desc", CancellationToken.None)

                                               Return Nothing
                                           End Function)
        End If
    End Sub

    Private ReadOnly Property IHttpHandler_IsReusable As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Private Class RunStatus
        Public Property IsRunning As Boolean

        Public Property HasError As Boolean

        Public Property Status As StatusReason

        Public Property RunValues As List(Of RunData)

        Public Property Timestamp As DateTime

        Public Property StatusMessage As String

    End Class

    Private Class RunData
        Public Property Year As Short

        Public Property PopValue As Single

        Public Property GdpValue As Single

        Public Property FstValue As Single

        Public Property Co2Value As Single
    End Class

    Private Enum StatusReason
        NOT_STARTED = 0
        UNEXPECTED = 1
        WAITING_PROGRESS_FILE = 2
        NO_PROGRESS_FILE = 3
        ERROR_FILE = 4
        COMPLETED = 5
    End Enum

    Private Class SessInit
        Public Property SessionID As String
    End Class

End Class

