Imports CommandLine
Imports CommandLine.Text
Imports System
Imports System.IO
Imports System.Linq
Imports IFs.Core

' Command-Line Arguments
public Class Arguments
    Implements IArguments

    'Created by LIU 2020/04/22
    Private _mode As Integer
    Private _horizon As Integer
    Private _session As Integer
    Private _monitor As Boolean
    Private _verbose As Boolean
    Private _process As Integer
    Private _historic As Boolean
    Private _log As FileInfo
    ' gvdb 2024/04/12
    ' add web sessionid for log
    Private _websessionid As String

    Public ReadOnly Property Mode As Integer Implements IArguments.Mode
        Get
            Return Me._mode
        End Get
    End Property

    Public ReadOnly Property Horizon As Integer Implements IArguments.Horizon
        Get
            Return Me._horizon
        End Get
    End Property

    Public ReadOnly Property Session As Integer Implements IArguments.Session
        Get
            Return Me._session
        End Get
    End Property

    Public ReadOnly Property Monitor As Boolean Implements IArguments.Monitor
        Get
            Return Me._monitor
        End Get
    End Property

    Public ReadOnly Property Verbose As Boolean Implements IArguments.Verbose
        Get
            Return Me._verbose
        End Get
    End Property

    Public ReadOnly Property Process As Integer Implements IArguments.Process
        Get
            Return Me._process
        End Get
    End Property

    Public ReadOnly Property Historic As Boolean Implements IArguments.Historic
        Get
            Return Me._historic
        End Get
    End Property

    Public ReadOnly Property Log As FileInfo Implements IArguments.Log
        Get
            Return Me._log
        End Get
    End Property

    Public ReadOnly Property WebSessionID As String Implements IArguments.WebSessionID
        Get
            Return Me._webSessionId
        End Get
    End Property

    <CommandLine.Value(0, Required:=True, MetaName:="Mode", HelpText:="execution mode")>
    Public WriteOnly Property ModeValue As Integer
        Set(value As Integer)
            If value < 5 Or value > 6 Then
                Throw New ArgumentException($"Invalid input: mode={value}.")
            End If
            Me._mode = value
            Debug.WriteLine($"- mode: {Me.Mode}")
        End Set
    End Property

    <CommandLine.Value(1, Required:=True, MetaName:="Horizon", HelpText:="time horizon")>
    Public WriteOnly Property HorizonValue As Integer
        Set(value As Integer)
            If value < 0 OrElse value > 9999 Then
                Throw New ArgumentException($"Invalid input: horizon={value}.")
            End If
            Me._horizon = value
            Debug.WriteLine($"- horizon: {Me.Horizon}")
        End Set
    End Property

    <CommandLine.Value(2, Required:=True, MetaName:="Session", HelpText:="session ID")>
    Public WriteOnly Property SessionValue As Integer
        Set(value As Integer)
            If value <> -1 AndAlso value < 0 Then
                Throw New ArgumentException($"Invalid input: session={value}.")
            End If
            Me._session = value
            Debug.WriteLine($"- session: {Me.Session}")
        End Set
    End Property

    <CommandLine.Value(3, Required:=True, MetaName:="Monitor", HelpText:="monitor flag")>
    Public WriteOnly Property MonitorValue As Nullable(Of Boolean)
        Set(value As Nullable(Of Boolean))
            'jrs 2022/03/14
            'If value.HasValue AndAlso value.Value Then
            '    Throw New ArgumentException("Invalid input: monitor not implemented.")
            'End If
            If Not value.HasValue Then
                Throw New ArgumentException("Invalid input.")
            End If
            Me._monitor = value.Value
            Debug.WriteLine($"- monitor: {Me.Monitor}")
        End Set
    End Property

    <CommandLine.Value(4, Required := True, MetaName := "Verbose", HelpText := "verbose flag")>
    Public WriteOnly Property VerboseValue As Nullable(Of Boolean)
        Set(value As Nullable(Of Boolean))
            If value.HasValue Then
                Me._verbose = value.Value
            Else
                Me._verbose = False
            End If
            Debug.WriteLine($"- verbose: {Me.Verbose}")
        End Set
    End Property

    <CommandLine.Value(5, Required:=True, MetaName:="Process", HelpText:="process to invoke")>
    Public WriteOnly Property ProcessValue As Integer
        Set(value As Integer)
            'jrs 2022/08/25 
            'jrs 2022/05/31 added process 3 to create empty base files to help with translation of old vb6 files
            'If value <> 0 AndAlso value <> 1 AndAlso value <> 2 AndAlso value <> 3 Then
            If value > 3 Or value < 0 Then
                Throw New ArgumentException($"Invalid input: process={value}.")
            End If
            Me._process = value
            Debug.WriteLine($"- process: {Me.Process}")
        End Set
    End Property

    <CommandLine.Value(6, Default := False, MetaName := "Historic", HelpText := "historic mode flag")>
    Public WriteOnly Property HistoricValue As Nullable(Of Boolean)
        Set(value As Nullable(Of Boolean))
            If value.HasValue Then
                Me._historic = value.Value
            Else
                Me._historic = False
            End If
            Debug.WriteLine($"- historic: {Me.Historic}")
        End Set
    End Property

    <CommandLine.Option("log", Default := Nothing, HelpText := "save debug traces to file")>
    Public WriteOnly Property LogOption As String
        Set(value As String)
            If value Is Nothing Then
                Return
            End If
            Me._log = New FileInfo(value)
            Debug.WriteLine($"- log: {Me.Log}")
        End Set
    End Property

    <CommandLine.Option("websessionid", Default:=Nothing, HelpText:="session ID from GUI for log file")>
    Public WriteOnly Property WebSessionIDOption As String
        Set(value As String)
            If value Is Nothing Then
                Return
            End If
            Me._websessionid = value
            Debug.WriteLine($"- webSessionID: {Me.WebSessionID}")
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return $"{{mode={Me.Mode};horizon={Me.Horizon};session={Me.Session};" &
            $"monitor={Me.Monitor};verbose={Me.Verbose};process={Me.Process};" &
            $"historic={Me.Historic};log='{Me.Log}';webSessionID='{Me.WebSessionID}'}}"
    End Function

    Public Shared Function Parse(args As String(), Optional showUsage As Boolean = True) As Arguments
        ' parse command line arguments
        Debug.WriteLine("parsing arguments")
        Debug.Indent()
        Dim parser As Parser = new Parser( _
            Sub (w)
                w.HelpWriter = Nothing
            End Sub)
        Dim result As ParserResult(Of Arguments) = parser _
            .ParseArguments(Of Arguments)(args)
        Debug.WriteLine("parsed arguments")
        Debug.Unindent()
        ' return parsed arguments on success
        If result.Tag = ParserResultType.Parsed Then
            Return DirectCast(result, Parsed(Of Arguments)).Value
        End If
        ' show error messages
        If showUsage Then
            Dim help As HelpText = HelpText.AutoBuild(result, _
                Function (h)
                    h.AutoHelp = False
                    h.AutoVersion = False
                    Return HelpText.DefaultParsingErrorsHandler(result, h)
                End Function, _
                Function (e)
                    Return e
                End Function)
            Console.WriteLine(help)
        End If
        ' on failure, inspect cause and throw appropriate exception
        Dim failure As NotParsed(Of Arguments) = DirectCast(result, NotParsed(Of Arguments))
        Dim cause As CommandLine.Error = failure.Errors.FirstOrDefault()
        If cause.Tag = ErrorType.SetValueExceptionError Then
            Throw DirectCast(cause, SetValueExceptionError).Exception
        End If
        Throw New ArgumentException($"failed to parse arguments due to {cause.Tag}")
    End Function

End Class
