Imports System.IO
Imports System.Text

Public Class PageBarColorFilter
    Inherits Stream

    Private ReadOnly _inner As Stream
    Private ReadOnly _css As String =
        "<style>#WinFormsToWeb_PageBar tr{background-color:#4572A7!important;color:white!important;}</style>"

    Public Sub New(inner As Stream)
        _inner = inner
    End Sub

    Public Overrides Sub Write(buffer As Byte(), offset As Integer, count As Integer)
		Dim html As String = Encoding.UTF8.GetString(buffer, offset, count)

		' jam 2025/04/21 - Fix: usar Replace case-insensitive porque el tag es </HEAD> en mayusculas
		Dim idx As Integer = html.IndexOf("</head>", StringComparison.OrdinalIgnoreCase)
		If idx >= 0 Then
			html = html.Substring(0, idx) & _css & html.Substring(idx)
		End If

		Dim bytes = Encoding.UTF8.GetBytes(html)
		_inner.Write(bytes, 0, bytes.Length)
	End Sub
    ' Stream overrides requeridos
    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property Length As Long
        Get
            Return 0
        End Get
    End Property
    Public Overrides Property Position As Long
        Get
            Return 0
        End Get
        Set(value As Long)
        End Set
    End Property
    Public Overrides Function Read(buffer As Byte(), offset As Integer, count As Integer) As Integer
        Return 0
    End Function
    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return 0
    End Function
    Public Overrides Sub SetLength(value As Long)
    End Sub
    Public Overrides Sub Flush()
        _inner.Flush()
    End Sub
End Class