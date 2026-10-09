Imports System
Imports System.IO

' Command-line arguments
Public Interface IArguments
    ' execution mode
    ReadOnly Property Mode() As Integer
    ' time horizon
    ReadOnly Property Horizon() As Integer
    ' session ID
    ReadOnly Property Session() As Integer
    ' monitor flag
    ReadOnly Property Monitor() As Boolean
    ' verbose flag
    ReadOnly Property Verbose() As Boolean
    ' process to invoke
    ReadOnly Property Process() As Integer
    ' historic mode flag
    ReadOnly Property Historic() As Boolean
    ' trace log file
    ReadOnly Property Log() As FileInfo

    'gvdb 2024/04/12
    ' web session ID from GUI
    ReadOnly Property WebSessionID() As String
End Interface
