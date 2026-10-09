Imports Microsoft.VisualBasic

<Serializable()>
Public Class MySession

    Private Const MySessionKey As String = "__MySession__"

    ' Private constructor (use MySession.Current to access the current instance).
    Private Sub New()
    End Sub

    ' Gets the current session.
    Public Shared ReadOnly Property Current() As MySession
        Get
            Dim session As MySession = TryCast(HttpContext.Current.Session(MySessionKey), MySession)
            If session Is Nothing Then
                session = New MySession()
                HttpContext.Current.Session(MySessionKey) = session
            End If
            Return session
        End Get
    End Property

    ' My session data goes here:
    Public MyString As String
    Public MyFlag As Boolean
    Public MyNumber As Integer
End Class

