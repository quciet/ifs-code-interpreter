Partial Class frm_LongVarSearch
    Inherits Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'jrs 2006/03/23 not using Load State (Artinsoft)
        If Support.FormManager.LoadState Then
            Support.FormManager.StateRestore(Me)
            Support.FormManager.LoadState = False

        End If
        'jrs 2012/01/16
        txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdSearch.ClientID + "')")
        'jrs 2013/09/04
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
            ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)

    End Sub


    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        lstSearch.Items.Clear()
        lstVarIdx.Items.Clear()

        'jrs 2008/05/30
        txtSearch.Text = Trim(txtSearch.Text)

        Dim Message As String

        If txtSearch.Text = "" Then
            Message = "Please type a search criteria"
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Dim i As Integer
        Dim valtbl As DataTable
        Dim idx As Integer = 0
        SetDataTablefromAccess(Session("PutFdrvRun") & "datadict.MDB", "SELECT * FROM datadict ORDER BY Variable", "Variable", valtbl) 'jrs 2008/01/03 added ORDER BY Variable
        Do
            If valtbl.Rows(idx).Item("Series") = "Yes" Or valtbl.Rows(idx).Item("Cohort") = "Yes" Then
                If valtbl.Rows(idx).Item("CovaTrA") = "No" Or (valtbl.Rows(idx).Item("CovaTrA") = "Yes" And UCase(ModelLoad) = "LOADCOVATRA") Then
                    'jrs 2008/06/05 If InStr(UCase(valtbl.Rows(idx).Item("Variable")), UCase(txtSearch.Text)) > 0 Or InStr(UCase(valtbl.Rows(idx).Item("Definition")), UCase(txtSearch.Text)) > 0 Then
                    If bSearchInVarIdx(txtSearch.Text, idx, True, valtbl) Then
                        lstSearch.Items.Add(valtbl.Rows(idx).Item("Variable") & " - " &
                            Replace(Replace(valtbl.Rows(idx).Item("Definition"), "   ", " "), "  ", " ") &
                            " - " & valtbl.Rows(idx).Item("Units")) 'jrs 2008/01/03 added units
                    End If
                End If
            End If
            idx = idx + 1
        Loop While (valtbl.Rows.Count > idx)

        If lstSearch.Items.Count > 0 Then
            lstSearch.SelectedIndex = 0
            'cmdSearch.Default = False
            'cmdLoad.Default = True
            'lstSearch.SetFocus()
        Else
            Message = "No parameters or descriptions found -- please try a different search string."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
    End Sub

    Private Sub cmdContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        Support.FormManager.Close(Me)
    End Sub

    Private Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        If lstSearch.SelectedIndex = -1 Then
            Dim Message As String
            Message = "Please select one of the results."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
            Exit Sub
        End If
        Session("LoadSearchVar") = lstSearch.Items(lstSearch.SelectedIndex).Text 'jrs 2006/01/31 lstSearch.SelectedValue

        Support.FormManager.Close(Me)
    End Sub

    'jrs 2008/05/20
    'Function created to search alternatively in all words in the search string (OR like)
    'Considering only the entire search string when using double quotation marks (AND like)
    Private Function bSearchInVarIdx(ByVal sSearch As String, ByVal idx As Integer,
        ByVal bHistory As Boolean, ByVal valtbl As DataTable) As Boolean
        'bHistory indicates if looking on emprical variables
        'valtbl contains empirical variable data

        bSearchInVarIdx = False
        Dim X As Object
        X = Split(sSearch, " ")
        Dim i As Integer, j As Integer
        Dim bOpen As Boolean, bClose As Boolean, bQuot As Boolean
        Dim Target() As String
        ReDim Target(0)
        j = 0
        'Create Target Array
        For i = 0 To UBound(X)
            bQuot = (InStr(X(i), """") > 0)
            If bQuot Then
                If bOpen Then
                    bClose = True
                Else
                    bOpen = True
                End If
            End If

            If Not bOpen Then
                ReDim Preserve Target(j)
                Target(j) = X(i)
                j = j + 1
            Else
                If j > UBound(Target) Then
                    ReDim Preserve Target(j)
                End If
                Target(j) = Target(j) & " " & X(i)
                If bClose Then
                    Target(j) = Replace(LTrim(Target(j)), """", "")
                    j = j + 1
                    bClose = False
                    bOpen = False
                End If
            End If
        Next i

        'Search for each target
        Dim k As Integer
        Dim iSearchCount As Integer = 0

        If Not bHistory Then
            For k = 0 To UBound(Target)
                If InStr(UCase(V(idx)), UCase(Target(k))) > 0 Or
                    InStr(UCase(VDef(idx)), UCase(Target(k))) > 0 Or
                    InStr(UCase(VExpl(idx)), UCase(Target(k))) > 0 Then
                    iSearchCount = iSearchCount + 1
                End If
            Next k
        Else
            For k = 0 To UBound(Target)
                If InStr(UCase(valtbl.Rows(idx).Item("Variable")), UCase(Target(k))) > 0 Or
                    InStr(UCase(valtbl.Rows(idx).Item("Definition")), UCase(Target(k))) > 0 Then
                    iSearchCount = iSearchCount + 1
                End If
            Next k
        End If

        'if all words where find, then set it to true, other wise too many matches
        If iSearchCount = UBound(Target) + 1 Then
            bSearchInVarIdx = True
        End If

    End Function

    Protected Sub cmdDataAnalysis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDataAnalysis.Click
        'jrs 2012/01/19
        If lstSearch.SelectedIndex = -1 Then
            Dim Message As String
            Message = "Please select one of the results."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
            Exit Sub
        End If
        Dim strData As String
        strData = lstSearch.Items(lstSearch.SelectedIndex).Text 'jrs 2006/01/31 lstSearch.SelectedValue
        strData = Trim(strData.Substring(0, InStr(strData, "-") - 1))

        Session("AvailabilityParent") = "frm_LongVarSearch.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & UCase(strData))

    End Sub
End Class
