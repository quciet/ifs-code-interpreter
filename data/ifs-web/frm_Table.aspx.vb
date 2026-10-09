Imports System.Drawing

Partial Class frm_Table
    Inherits Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Session("Table.Caption") > "" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Session("Table.Caption")
        End If
        Dim i As Integer, j As Integer
        Dim rr As TableRow
        'Headers
        For i = 1 To Session("Table.FixedRows")
            rr = New TableRow
            rr.BackColor = Color.Gray
            rr.BorderColor = Color.White
            For j = 1 To Session("Table.cols")
                Dim cc As TableHeaderCell = New TableHeaderCell
                'cc.ID = "C" & (j - 1).ToString
                rr.Cells.Add(cc)
            Next
            grd_TableDisplay.Rows.Add(rr)
            j = Session("Table.cols") - 1
            grd_TableDisplay.Rows(i - 1).Cells(j).ID = "C" & j.ToString
        Next
        'Load Array to display
        For i = 1 To Session("Table.rows")
            rr = New TableRow
            For j = 1 To Session("Table.cols")
                Dim tc As TableCell = New TableCell
                If j <= Session("Table.FixedCols") Then
                    tc.BackColor = Color.Gray
                    tc.BorderColor = Color.White
                Else
                    If i - 1 = Session("Table.RowSel") Then
                        tc.BackColor = Session("Table.CellBackColor")
                    End If
                End If
                tc.Text = Session("Table.Array")(i - 1, j - 1)
                'tc.ID = "C" & (j - 1).ToString
                rr.Cells.Add(tc)
            Next
            grd_TableDisplay.Rows.Add(rr)
            j = Session("Table.cols") - 1
            grd_TableDisplay.Rows(i + Session("Table.FixedRows") - 1).Cells(j).ID = "R" & (i + Session("TableFixedRows") - 1).ToString & "C" & j.ToString
        Next

        If Not IsNothing(Session("Table.RowSel")) Then
            Dim sJs As String
            j = Session("Table.cols") - 1
            If Session("Table.RowSel") < 10 Then
                sJs = "location.hash = 'C" & CStr(j) & "';"
            Else
                sJs = "location.hash = 'R" & CStr(Session("Table.RowSel")) & "C" & CStr(j) & "';"
            End If
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "FocusCell", sJs, True)
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

        'AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("TableDisplayParent") & "';"
    End Sub
End Class
