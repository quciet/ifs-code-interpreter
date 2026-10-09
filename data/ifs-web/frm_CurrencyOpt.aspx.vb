Option Strict Off
Option Explicit On

Partial Class frm_CurrencyOpt
    Inherits Page

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

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        'cswmGroup1.ID = "Group_ab50"

        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("CurrencyParent") & "';"
        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save and Continue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        If Not Page.IsPostBack Then
            cboCountry.Items.Add("")
            cboCountry.Text = Session("CurrencyCountry")
            If Session("CurrencySym") > "" Then
                txtCurrencySymbol.Text = Session("CurrencySym")
            Else
                txtCurrencySymbol.Text = "$"
            End If
            If Not IsNumeric(Session("CurrencyVal")) Then
                txtCurrencyValue.Text = "1"
            Else
                txtCurrencyValue.Text = String.Format(Math.Round(Val(Session("CurrencyVal")), 5), "#,###.#####")
            End If
            'Load Combo box
            Dim rs As DataTable
            SetDataTablefromAccess(Session("FdrvRun") + "IFS.MDB", "SELECT * FROM [Currency] ORDER BY Country", "", rs)
            If rs.Rows.Count > 0 Then
                ReDim Preserve Session("sSimbols")(rs.Rows.Count)
                ReDim Preserve Session("vVals")(rs.Rows.Count)
                'jrs 2012/12/07
                ReDim Preserve Session("sCurrName")(rs.Rows.Count)

                Dim i As Integer = 0
                Do While Not (i = rs.Rows.Count)
                    cboCountry.Items.Add(rs.Rows(i).Item("Country"))
                    i = i + 1
                    If Not IsDBNull(rs.Rows(i - 1).Item("Symbol")) Then
                        Session("sSimbols")(i) = rs.Rows(i - 1).Item("Symbol")
                    End If
                    If Not IsDBNull(rs.Rows(i - 1).Item("Currency Value")) Then
                        Session("vVals")(i) = rs.Rows(i - 1).Item("Currency Value")
                    Else
                        Session("vVals")(i) = 1
                    End If
                    'jrs 2012/12/07
                    If Not IsDBNull(rs.Rows(i - 1).Item("Currency Name")) Then
                        Session("sCurrName")(i) = rs.Rows(i - 1).Item("Currency Name")
                    End If
                Loop
            End If
        End If
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                Support.FormManager.Show(Me, Session("CurrencyParent"))
            Case "Save and Continue"
                Session("CurrencyCountry") = cboCountry.Text
                Session("CurrencySym") = txtCurrencySymbol.Text
                'jrs 2012/12/07
                Session("CurrencyName") = txtCurrencyName.Text

                If Not IsNumeric(txtCurrencyValue.Text) Then
                    Session("CurrencyVal") = 1
                Else
                    Session("CurrencyVal") = Val(txtCurrencyValue.Text)
                End If

                If optValsinCurrency.Checked Then
                    Session("CurrencyVal") = 1 / Val(Session("CurrencyVal"))
                End If

                Support.FormManager.Show(Me, Session("CurrencyParent"))
        End Select
    End Sub

    Protected Sub optValsinCurrency_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optValsinCurrency.CheckedChanged
        txtCurrencyValue.Text = String.Format(CStr(Math.Round(1 / Val(txtCurrencyValue.Text), 5)), "#,###.#####")
    End Sub

    Protected Sub optValsInDollars_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optValsInDollars.CheckedChanged
        txtCurrencyValue.Text = String.Format(CStr(Math.Round(1 / Val(txtCurrencyValue.Text), 5)), "#,###.#####")
    End Sub

    Protected Sub cboCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
        'cboCountry.Text = cboCountry.List(cboCountry.ListIndex)
        If cboCountry.SelectedIndex > 0 Then
            txtCurrencySymbol.Text = Session("sSimbols")(cboCountry.SelectedIndex)
            'jrs 2012/12/07
            txtCurrencyName.Text = Session("sCurrName")(cboCountry.SelectedIndex)
            If optValsInDollars.Checked Then
                txtCurrencyValue.Text = String.Format(Session("vVals")(cboCountry.SelectedIndex), "#,###.#####")
            Else
                txtCurrencyValue.Text = String.Format(1 / Session("vVals")(cboCountry.SelectedIndex), "#,###.#####")
            End If
        Else
            txtCurrencySymbol.Text = "$"
            txtCurrencyValue.Text = "1"
        End If
    End Sub
End Class
