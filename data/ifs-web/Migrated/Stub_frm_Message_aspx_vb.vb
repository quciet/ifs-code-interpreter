'===========================================================================
' This file was generated as part of an ASP.NET 2.0 Web project conversion.
' This code file 'App_Code\Migrated\Stub_frm_Message_aspx_vb.vb' was created and contains an abstract class 
' used as a base class for the class 'Migrated_frm_Message' in file 'frm_Message.aspx.vb'.
' This allows the the base class to be referenced by all code files in your project.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================

Public Class frm_Message
    Inherits System.Web.UI.Page

    Public Sub LoadMessage(ByVal s As String)
        Session("Message") = s
    End Sub

    'Public MustOverride Sub LoadMessage(ByVal s As String)
    'Public MustOverride Sub LoadParent(ByVal s As String)

    Public Sub LoadParent(ByVal s As String)
        Session("MsgParent") = s
    End Sub

End Class
