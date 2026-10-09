Option Strict Off
Option Explicit On

Partial Class frm_MapImage
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rs As DataTable
        Dim path As String

        Dim img As System.Drawing.Image
        Dim fname As String = ""
        If Not Request.QueryString.Get("cname") Is Nothing Then
            'jrs 2011/06/23
            Dim ISOcode As String = Request.QueryString.Get("cname")
            If ISOcode = "GL" Then
                ISOcode = "DK"
            ElseIf Len(ISOcode) > 2 Then
                ISOcode = Left(ISOcode, 2)
            End If
            SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT FIPS_CODE FROM [Country Translation] WHERE [ISO3166-2 Codes]=""" & ISOcode & """", "", rs)
            'SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT FIPS_CODE FROM [ESRICountry] where [NAME]=""" & Session("sESRICountry") & """", "", rs)
            fname = rs.Rows(0).Item("FIPS_CODE") + ".gif"
        End If
        path = "~/Maps/" & fname
        img = System.Drawing.Image.FromFile(Server.MapPath(path))
        txtDataHolder.Value = img.Width + 5 & ";" & img.Height + 10
        Image1.ImageUrl = path
        Image1.Width = img.Width
        Image1.Height = img.Height

        'Response.ContentType = "image/gif"
        'Response.WriteFile(Server.MapPath(path))
    End Sub
End Class
