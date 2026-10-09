
Public Class ErrorHandler
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
        ' Display error. if any.
        If Session("Error") <> Nothing Then
            lblError.Text = "The following error occurred:<br><br>" +
                Session("Error") + "<br><br>"
            '2005/01/15 if timeout make e-mail link invisible
            Dim sMail As String = ""
            'jrs 2005/03/7
            Dim sError As String = ""
            sError = Replace(Session("Error"), "&", "")
            sError = Replace(sError, ">", "")
            'jrs 2011/04/05 If Not Session("FirstYear") = Nothing Then
            If Session("bTimeOut") = False Then
                'jrs 2008/04/10 sMail = "Please report to <a href='mailto:bhughes@du.edu?body=" + sError + "'>Barry Hughes</a>.<br><br>"
                'jrs 2021/12/14 sMail = "The IFs project will pay $25 to the first person who discovers a previously unreported error in either the web-based " & _
                '"or stand-alone version of IFs and reports a reproducible path for generating the error (please provide details clearly) to " &
                '"<a href='mailto:jsolorza@du.edu?body=" + sError + "'>jsolorza@du.edu</a>.  Please also provide your contact information so we can notify you concerning our reproduction of it and arrange payment.  " &
                '"Thank you for using IFs and making it better.<br><br>"
                sMail = "The IFs project will appreciate and thank users that report any errors they might find, in either the web-based " &
                "or stand-alone version of IFs including a reproducible path for generating the error (please provide details clearly) to " &
                "<a href='mailto:jsolorza@du.edu?body=" + sError + "'>jsolorza@du.edu</a>.  " &
                "Thank you for using IFs and making it better.<br><br>"
            Else
                'jrs 2011/05/16
                Session("bTimeOut") = False
            End If

            lblError.Text = Replace(lblError.Text, "at ", "<br>at ")
            lblError.Text = lblError.Text + sMail
            ' Clear the Session state variable.
            Session("Error") = Nothing
            'jrs 2014/02/01
            'Clear any previously loaded list (Flex Displays)
            Session("ListLoaded") = Nothing
        End If

    End Sub


    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Dim bCyberDashboard As Boolean = False
        If Session("bError4CyberDashboard") = True Then
            bCyberDashboard = True
        End If

        Session.Abandon()

        'jrs 2015/08/25 added CyberDashboard condition
        If bCyberDashboard Then
            Support.FormManager.Show(Me, "frm_CyberDashboard.aspx")
        Else
            Support.FormManager.Show(Me, "frm_MainMenu.aspx")
        End If
    End Sub
End Class
