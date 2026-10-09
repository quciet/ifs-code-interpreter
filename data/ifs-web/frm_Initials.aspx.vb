'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'              JAM 2017-09-17 frm_Initials: Intials Form
'
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Option Strict Off
Option Explicit On
Partial Class frm_Initials
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'Do not modify it using the code editor.
        InitializeComponent()

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If

        End If
        dsVarList.DataFile = Session("PutFdrvRun") & "ifsVar.mdb"
        dsVarList.SelectCommand = "SELECT InitialID, InitialDescription FROM Initials"


        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & Session("VarList_Title")
    End Sub

    Dim sqlF As String

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region




    Private Sub RefreshDisplay()
        grdVarList.DataBind()
        grdVarList.SelectedIndex = 0
        'grdVarList_SelectedIndexChanged(Me, New EventArgs)
    End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cswmWebMenu.UserData = "jsolorza::976739975"

        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.AccessorKeyCode = 27
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_fa05"

        cswmGroup1.OnMenuBarClick = "location.href='" & Session("InitialsParent") & "';"

        If Not Page.IsPostBack Then
            RefreshDisplay()
        End If
    End Sub


    Protected Sub grdVarList_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVarList.PageIndexChanged
        grdVarList.SelectedIndex = 0

    End Sub



    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

    End Sub
End Class
