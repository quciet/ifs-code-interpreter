Imports System.Data
Imports System.Data.SqlClient


'Namespace IFsWeb


Public Class ExceptionLogger
    Private _connectionString As String     'Database connection string
    Private _source As String               'Exception source page
    Private _type As String                 'Exception type
    Private _message As String              'Exception message
    Private _stackTrace As String           'Exception stack trace
    Private _ExceptionID As Integer         'Exception log ID
    Private _sAddr As String                'Ip address

    Public Sub New(ByVal connectionString As String, ByVal inputException As Exception, ByVal sAddr As String)
        _connectionString = connectionString
        _source = inputException.Source
        _type = inputException.GetType.ToString
        _message = inputException.Message
        _stackTrace = inputException.StackTrace
        _sAddr = sAddr
    End Sub

    Public Function LogException() As Integer
        _ExceptionID = DBLog(_connectionString, _source, _type, _message, _stackTrace)
        Return _ExceptionID
    End Function


    Private Function DBLog(ByVal connectionString As String, _
    ByVal source As String, ByVal type As String, ByVal message As String, ByVal stackTrace As String) As Integer
        'Dim pConnection As SqlConnection
        'Dim pCommand As SqlCommand
        Dim exceptionID As Integer
        'Dim paramExceptionID As SqlParameter
        Dim sql As String

        Try
            'pConnection = New SqlConnection(connectionString)
            'pConnection.Open()
            'pCommand = New SqlCommand("AddException")
            'pCommand.Connection = pConnection
            'pCommand.CommandType = CommandType.StoredProcedure
            'pCommand.Parameters.Add("@Source", SqlDbType.VarChar, 255).Value = source
            'pCommand.Parameters.Add("@Type", SqlDbType.VarChar, 255).Value = type
            'pCommand.Parameters.Add("@Message", SqlDbType.VarChar, 255).Value = message
            'pCommand.Parameters.Add("@StackTrace", SqlDbType.VarChar, 3000).Value = stackTrace
            'paramExceptionID = pCommand.Parameters.Add("@ExceptionID", SqlDbType.Int)
            'paramExceptionID.Direction = ParameterDirection.Output
            'pCommand.ExecuteNonQuery()
            'exceptionID = paramExceptionID.Value

                'jrs 2008/05/08 error when message uses ': sql = "INSERT INTO ExceptionLog Values('" & source & "', '" & type & "', '" & message & "', '" & stackTrace & "', '" & CStr(Now) & "', '" & CStr(_sAddr) & "')"
                sql = "INSERT INTO ExceptionLog Values(""" & source & """, """ & type & """, """ & message & """, """ & stackTrace & """, """ & CStr(Now) & """, """ & CStr(_sAddr) & """)"
            updateAccessWith1Command(connectionString, sql)
            exceptionID = 0
        Catch ex As Exception
            'Returns -1 if the exception logging fails.
            exceptionID = -1
            'Finally
            '    If pConnection.State = ConnectionState.Open Then
            '        pConnection.Close()
            '    End If
        End Try
        Return exceptionID
    End Function

End Class

'End Namespace
