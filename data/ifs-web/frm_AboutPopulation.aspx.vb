Option Strict Off
Option Explicit On

Partial Class frm_AboutPopulation
    Inherits Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim msg As String
            Carret = "<br>" & Chr(10)
            carret2 = Carret + Carret

            msg$ = "The population representation of IFs is a cohort-component structure " + Carret
            msg$ = msg$ + "that uses data from the United Nations Population Division (2024 Revision) " + Carret 'jrs 2011/09/13 changed from 2006 to 2010
            msg$ = msg$ + "to initialize the model." 'jrs 2009/02/02 We greatly " + Carret
            'jrs 2009/02/02 msg$ = msg$ + "appreciate its generosity in providing these data (2004 Revision)." + Carret

            If Session("AboutParent") = "frm_EdnCohoDisp.aspx" Then
                msg$ = msg$ + Carret
                msg$ = msg$ + Carret
                msg$ = msg$ + "The education data is initialized from the Barro-Lee dataset on education. " + Carret
                msg$ = msg$ + "Graduates by education level are mutually exclusive. " + Carret
                msg$ = msg$ + Carret
                msg$ = msg$ + "For example, secondary graduates do not include either of those who could" + Carret
                msg$ = msg$ + " not complete beyond primary or those who have completed all the way " + Carret
                msg$ = msg$ + "to the tertiary."
            End If

            lbl_header.Text = msg

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
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_fa05"
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("AboutParent") & "';"

    End Sub

End Class
