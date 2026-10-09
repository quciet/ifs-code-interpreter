' jam 2025/04/21 - Fix ActiveCaption color via HttpModule
' jam 2026/04/29 - Skip static files (dual path check) to avoid ERR_CONTENT_DECODING_FAILED

Imports System.Web

Public Class PageBarColorModule
    Implements IHttpModule

    ' jam 2026/04/29 - Exceptions for PageBarColorFilter
    '   Images    : png, jpg, jpeg, gif, bmp, ico, svg, webp
    '   Scripts/CSS : js, css, map
    '   Sources     : woff, woff2, ttf, eot
    '   ASP.NET     : axd (WebResource.axd, ScriptResource.axd), ashx (handlers JSON/XML)
    '   Files    : pdf, zip, exe, msi, cab
    Private Shared ReadOnly StaticExtensions As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".ico", ".svg", ".webp",
        ".css", ".js", ".map",
        ".woff", ".woff2", ".ttf", ".eot",
        ".axd", ".ashx",
        ".pdf", ".zip", ".exe", ".msi", ".cab"
    }

    Public Sub Init(context As HttpApplication) Implements IHttpModule.Init
        AddHandler context.BeginRequest, AddressOf OnBeginRequest
    End Sub

    Private Sub OnBeginRequest(sender As Object, e As EventArgs)
        Dim app As HttpApplication = CType(sender, HttpApplication)
        Dim ext As String = String.Empty


        Try
            ext = IO.Path.GetExtension(app.Request.PhysicalPath)
        Catch ex As ArgumentException

        End Try

        ' jam 2026/04/29 - Fallback: AppRelativeCurrentExecutionFilePath

        If String.IsNullOrEmpty(ext) Then
            Try
                Dim relativePath As String = app.Request.AppRelativeCurrentExecutionFilePath
                If Not String.IsNullOrEmpty(relativePath) Then
                    ext = IO.Path.GetExtension(relativePath)
                End If
            Catch ex As Exception

            End Try
        End If


        If Not String.IsNullOrEmpty(ext) AndAlso StaticExtensions.Contains(ext) Then Return

        app.Response.Filter = New PageBarColorFilter(app.Response.Filter)
    End Sub

    Public Sub Dispose() Implements IHttpModule.Dispose
    End Sub
End Class
