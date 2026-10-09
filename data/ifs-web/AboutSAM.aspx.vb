Option Strict Off
Option Explicit On

Partial Class frm_AboutSAM
    Inherits Page

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        If Not Page.IsPostBack Then
            Dim Msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "About This Software"

            Carret = "<br>" & Chr(10)
            carret2 = Carret + Carret

            Msg = "The Social Accounting Matrix (SAM) representation in IFs owes much to the data of " + Carret
            Msg = Msg & "     the Global Trade Analysis Project (GTAP) and " + Carret
            Msg = Msg & "     the World Bank's World Development Indicators." & carret2
            Msg = Msg & "Neither organization has any responsibility for the implementation of the SAM." + Carret

            lbl_header.Text = Msg
            Msg = ""

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cswmWebMenu.UserData = "jsolorza::976739975"

        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True

        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_fa05"
        cswmGroup1.OnMenuBarClick = "location.href='frm_SAM.aspx';"

    End Sub
End Class
