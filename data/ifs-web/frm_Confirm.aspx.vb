'===========================================================================
' This file was modified as part of an ASP.NET 2.0 Web project conversion.
' The class name was changed and the class modified to inherit from the abstract base class 
' in file 'App_Code\Migrated\Stub_frm_Confirm_aspx_vb.vb'.
' During runtime, this allows other classes in your web application to bind and access 
' the code-behind page using the abstract base class.
' The associated content page 'frm_Confirm.aspx' was also modified to refer to the new class name.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
'Namespace IFsWeb

'Partial Class frm_Confirm
Partial Class Migrated_frm_Confirm

        Inherits frm_Message

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
            'jrs 2005/11/11
            If Not Session("ConFirmThen") Is Nothing Then
                Dim s As String = Session("ConFirmThen")
                Session("ConFirmThen") = Nothing
                If s = "SAVEFORMULATODATA" Then
                    SAVEFORMULATODATA()
                    Session("bPlsExitForm") = True
                ElseIf s = "SAVEFORMULA_NON_GRAVITY" Then
                    Session("ContinueSaveNonGravity") = True
                    Support.FormManager.Close(Me)
                End If

            End If

            If Session("MsgParent") = Nothing Then
                Session("ConfirmResult") = "Y"
                Support.FormManager.Close(Me)
            Else
                Dim s As String
                s = Session("MsgParent")
                Session("MsgParent") = Nothing
                Session("ConfirmResult") = "Y"
                Support.FormManager.Show(Me, s)
            End If
        End Sub
        Private Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNO.Click
            If Not Session("ConFirmThen") Is Nothing Then
                Dim s As String = Session("ConFirmThen")
                Session("ConFirmThen") = Nothing
                If s = "SAVEFORMULATODATA" Then
                    Session("bPlsExitForm") = True
                ElseIf s = "SAVEFORMULA_NON_GRAVITY" Then
                    Support.FormManager.Close(Me)
                End If
            End If

            If Session("MsgParent") = Nothing Then
                Session("ConfirmResult") = "N"
                Support.FormManager.Close(Me)
            Else
                Dim s As String
                s = Session("MsgParent")
                Session("MsgParent") = Nothing
                Session("ConfirmResult") = "N"
                Support.FormManager.Show(Me, s)
            End If
        End Sub
        Private Sub SAVEFORMULATODATA()
            FileCopyCheck(Session("PutFdrvRun") & "DataDictDyadic.mdb", DataFileDir$ & "DataDictDyadic.mdb")
        End Sub
    End Class

'End Namespace
