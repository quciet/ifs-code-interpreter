'--------------------------------------------------------------------------------
'Product
'		AWS FilePicker 2.1
'
'Authors
'		Alex Avrutin (alex@awsystems.biz)
'       Juan C. Lopez (jlopez@awsystems.biz)
'
'Company
'		AW/Systems, LLC (http:www.awsystems.biz)
'				  Off-shore software outsourcing
'       Interested in reducing cost of web development? 
'       Then why not delegate some work to AW/Systems? 
'       We offer .NET web development services at only 
'       $3,400 per man-month or $30 per hour.
'
'Version 
'		2.1
'
'This file:
'       Utilities

'Namespace IFsWeb

Imports System.Reflection

Public Class Utils

    Private Sub New()
        'Constructor is private -- forbid instantiating this class 
    End Sub

    Public Shared Function AddCharInTheEndIfNotPresent(ByVal ch As String, ByVal str As String) As String
        'Adds a character in the end of a string if such character hasn't 
        'already presented

        If (str.LastIndexOf(ch) <> str.Length - 1) Then str += ch.ToString
        Return (str)

    End Function

    Public Shared Function AddSlashInTheEndIfNotPresent(ByVal str As String) As String

        Return (AddCharInTheEndIfNotPresent("/", str))
    End Function

    Public Shared Function AddBackSlashInTheEndIfNotPresent(ByVal str As String) As String

        Return (AddCharInTheEndIfNotPresent("\", str))
    End Function

    Public Shared Function GetParentDirectory(ByVal RelativePath As String) As String

        'This function works for /main/db/test/ as well as /main/db/test.aspx
        If (RelativePath = "") Then
            Return ("") ' can't go higher than root

        Else
            'Remove trailing "/" at end of path
            If (RelativePath.LastIndexOf("/") = RelativePath.Length - 1) Then
                RelativePath = RelativePath.Remove(RelativePath.LastIndexOf("/"), (RelativePath.Length - RelativePath.LastIndexOf("/")))
            End If

            Try
                'Remove the characters after the last occurence of / in the string. => parent directory
                RelativePath = RelativePath.Remove(RelativePath.LastIndexOf("/"), (RelativePath.Length - RelativePath.LastIndexOf("/")))

                Return (RelativePath)

            Catch
                Return ("") ' default to root;
            End Try
        End If
    End Function

    Public Shared Function FormatFileSize(ByVal FileSize As Long, ByVal rm As System.Resources.ResourceManager) As String

        Dim Size As Integer
        Size = Convert.ToInt32(FileSize / 1024)
        If Size = 0 Then
            Return ("1 " & rm.GetString("LOC_LABEL_KB"))

        Else
            Return (String.Format("{0:###,###,###}", Size) & " " & rm.GetString("LOC_LABEL_KB"))
        End If
    End Function

    Public Shared Function GetVersionTag() As String

#If DEBUG Then

        Return DateTime.Now.ToFileTimeUtc()

#Else
        
Try
            Dim t As Type = GetType(Utils)
            Dim assemFromType As Assembly = t.Assembly

            Return $"v{assemFromType.GetName().Version.ToString()}"
        Catch
            Return DateTime.Now.ToFileTimeUtc()
        End Try        
#End If
    End Function

    Public Shared Function GetRequestAuthority(req As HttpRequest) As String
        Return req.Url.Authority
    End Function

    Public Shared Function GetRequestApplicationPath(req As HttpRequest) As String
        Return req.ApplicationPath
    End Function

    Public Shared Function GetWSUri(req As HttpRequest, ctxPath As String) As String
        If ctxPath.StartsWith("/") Then
            ctxPath = ctxPath.Substring(1)
        End If

        'gvdb 2024/09/18 Return "ws:/" & GetRequestAuthority(req) & GetRequestApplicationPath(req) & "/" & ctxPath
        If "https".Equals(req.Url.Scheme) Then
            Return "wss:/" & GetRequestAuthority(req) & GetRequestApplicationPath(req) & "/" & ctxPath
        Else
            Return "ws:/" & GetRequestAuthority(req) & GetRequestApplicationPath(req) & "/" & ctxPath
        End If

    End Function
End Class

'End Namespace
