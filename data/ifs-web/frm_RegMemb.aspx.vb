Option Strict Off
Option Explicit On

Partial Class frm_RegMemb
    Inherits Page

    Protected Sub optUsingRegs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optUsingRegs.CheckedChanged
        lbl_regional.Visible = True
        lbl_Names.Visible = True
        lbl_Groups.Visible = False
        lbl_GroupReg.Visible = False
        LoadRegions()
    End Sub

    Protected Sub optUsingGroups_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optUsingGroups.CheckedChanged
        lbl_regional.Visible = False
        lbl_Names.Visible = False
        lbl_Groups.Visible = True
        lbl_GroupReg.Visible = True
        LoadGroups()
    End Sub

    Private Sub LoadGroups()
        Dim db As String
        Dim valtbl As DataTable
        Dim rr As Integer

        '   Load Group Names List Box
        '   -------------------------
        lst_Names.Items.Clear()
        db = Session("IFs_MDB2Active")
        SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew$ & " ORDER BY GroupName", "", valtbl)
        If Not (valtbl.Rows.Count = 0) Then
            rr = 0
            While Not (rr = valtbl.Rows.Count)
                lst_Names.Items.Add(valtbl.Rows(rr).Item("GroupName"))
                rr = rr + 1
            End While
        End If
        lst_Names.SelectedIndex = 0
        LoadGroupMembrs()
    End Sub

    Private Sub LoadRegions()
        Dim DimNum As Integer
        Dim EleLoop As Integer

        lst_Names.Items.Clear()
        '   Load Region Names List Box
        '   --------------------------
        DimNum = 1
        For EleLoop = 1 To MaxVal(DimNum) - 1
            lst_Names.Items.Add(GRPS$(DimNum, EleLoop))
        Next EleLoop
        lst_Names.SelectedIndex = 0
        LoadRegMembrs()
    End Sub

    Private Sub LoadRegMembrs()
        Dim db As String
        Dim valtbl As DataTable = Nothing
        Dim rr As Integer

        '   Load Group Names List Box
        '   -------------------------
        lst_Regional.Items.Clear()
        db = Session("IFs_MDB2Active")
        SetDataTablefromAccess(db, "SELECT * FROM " & Regionalization$ & " WHERE NewRegion = " & lst_Names.SelectedIndex + 1 & " ORDER BY Country", "", valtbl)
        If Not (valtbl.Rows.Count = 0) Then
            rr = 0
            While Not (rr = valtbl.Rows.Count)
                lst_Regional.Items.Add(valtbl.Rows(rr).Item("Country"))
                rr = rr + 1
            End While
        End If

    End Sub

    Private Sub LoadGroupMembrs()
        Dim db As String
        Dim valtbl As DataTable = Nothing
        Dim rr As Integer

        '   Load Group Names List Box
        '   -------------------------
        lst_Regional.Items.Clear()
        db = Session("IFs_MDB2Active")
        SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew$ & ", " &
        Grouping$ & " WHERE GroupNum = GroupNo AND GroupName = """ & lst_Names.SelectedValue & """ ORDER BY Region", "", valtbl)
        If Not (valtbl.Rows.Count = 0) Then
            rr = 0
            While Not (rr = valtbl.Rows.Count)
                lst_Regional.Items.Add(valtbl.Rows(rr).Item("Region"))
                rr = rr + 1
            End While
        End If

    End Sub

    Protected Sub lst_Names_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lst_Names.SelectedIndexChanged
        If optUsingGroups.Checked Then
            LoadGroupMembrs()
        Else
            LoadRegMembrs()
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            LoadGroups()
        End If
        'WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")

        'jrs 2013/09/04
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
        ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)

    End Sub

    Protected Sub cmdExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Support.FormManager.Close(Me)
    End Sub
End Class
