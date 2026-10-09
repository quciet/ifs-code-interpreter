'Namespace IFsWeb.Support
Namespace Support
    Public Class ControlManager

        Private Const location = 15

        Public Shared Function GetControlImage(ByVal WebControl As System.Web.UI.WebControls.WebControl)
            Dim ls_name As String
            ls_name = WebControl.Style.Item("BACKGROUND-IMAGE")
            ls_name = ls_name.Remove(0, 4)
            ls_name = ls_name.Remove(ls_name.Length - 1, 1)
            Return ls_name
        End Function

        Public Shared Sub SetControlImage(ByVal WebControl As System.Web.UI.WebControls.WebControl, ByRef ImageUrl As String)
            WebControl.Style.Add("BACKGROUND-IMAGE", "url(" + ImageUrl + ")")
            WebControl.Style.Add("BACKGROUND-REPEAT", "no-repeat")
            'WebControl.Style.Add("BACKGROUND-POSITION", "center 50%")
            WebControl.Style.Add("POSITION", "absolute")
            If (WebControl.Style.Item("HEIGHT") Is Nothing) And (WebControl.Style.Item("WIDTH") Is Nothing) Then
                WebControl.Attributes.Add("src", "ArtinSoft.WinFormsToWeb.Empty.gif")
                'Default Values
                WebControl.Style.Add("HEIGHT", "50px")
                WebControl.Style.Add("WIDTH", "100px")
            End If
        End Sub

        Public Shared Sub SetControlSize(ByVal WebControl As System.Web.UI.WebControls.WebControl, ByVal newSize As System.Drawing.Size)
            If Not WebControl Is Nothing Then
                WebControl.Height = System.Web.UI.WebControls.Unit.Parse(newSize.Height)
                WebControl.Width = System.Web.UI.WebControls.Unit.Parse(newSize.Width)
            End If
        End Sub

        Public Shared Function GetControlSize(ByVal WebControl As System.Web.UI.WebControls.WebControl) As System.Drawing.Size
            Dim size As New System.Drawing.Size()
            If Not WebControl Is Nothing Then
                size.Height = WebControl.Height.Value
                size.Width = WebControl.Width.Value
            End If
            Return size
        End Function

        Public Shared Sub SetControlLocation(ByVal WebControl As System.Web.UI.WebControls.WebControl, ByVal newLocation As System.Drawing.Point)
            If Not WebControl Is Nothing Then
                WebControl.Style.Add("LEFT", newLocation.X.ToString())
                WebControl.Style.Add("TOP", (newLocation.Y + location).ToString())
                WebControl.Style.Add("POSITION", "absolute")
            End If
        End Sub

        Public Shared Function GetControlLocation(ByVal WebControl As System.Web.UI.WebControls.WebControl) As System.Drawing.Point
            Dim location As New System.Drawing.Point()
            If Not WebControl Is Nothing Then
                location.X = System.Web.UI.WebControls.Unit.Parse(WebControl.Style("LEFT")).Value()
                location.Y = System.Web.UI.WebControls.Unit.Parse(WebControl.Style("TOP")).Value()
            End If
            Return location
        End Function

        Public Shared Sub SetControlFont(ByRef webFont As System.Web.UI.WebControls.FontInfo, ByVal winFont As System.Web.UI.WebControls.FontInfo)
            If Not webFont Is Nothing And Not winFont Is Nothing Then
                webFont.Bold = winFont.Bold
                webFont.Italic = winFont.Italic
                webFont.Name = winFont.Name
                webFont.Size = winFont.Size
                webFont.Strikeout = winFont.Strikeout
                webFont.Underline = winFont.Underline
            End If
        End Sub
    End Class

End Namespace
