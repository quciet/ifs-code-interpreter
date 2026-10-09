'===========================================================================
' This file was modified as part of an ASP.NET 2.0 Web project conversion.
' The class name was changed and the class modified to inherit from the abstract base class 
' in file 'App_Code\Migrated\Stub_frm_Message_aspx_vb.vb'.
' During runtime, this allows other classes in your web application to bind and access 
' the code-behind page using the abstract base class.
' The associated content page 'frm_Message.aspx' was also modified to refer to the new class name.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================

Partial Class frm_Message
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'jrs 2004/11/23
        If Not Page.IsPostBack Then
            lblMsg.Text = Session("Message")
            'jrs 2014/01/02
            'Clear any previously loaded list (Flex Displays)
            Session("ListLoaded") = Nothing
        End If
    End Sub

    Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        'jrs 2023/08/24
        'jrs 2023/09/22 too many messages that require to continue and not restart Session.Abandon()
        'jrs 2005/11/11
        If Session("MsgParent") = Nothing Then
            Support.FormManager.Close(Me)
        Else
            Dim s As String
            s = Session("MsgParent")
            Session("MsgParent") = Nothing
            Support.FormManager.Show(Me, s)
        End If
    End Sub

End Class
