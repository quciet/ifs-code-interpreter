Partial Class frm_SearchVarBilat
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
        'jrs 2006/03/23 not using Load State (Artinsoft)
        If Support.FormManager.LoadState Then
            Support.FormManager.LoadState = False
        End If

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        'Put user code to initialize the page here
        'context menu using WebMenu
        'End If 'mti Augut 2004 3 ading If page.isposback
        'mti WebForm Changes to be taken by Jose
        'wmContext.UserData = "jsolorza::976739975"

        'wmContext.ClearPixelImage = "blank.gif"
        'wmContext.PopupIcon = "./images/Popup.gif"
        'wmContext.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmContext.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmContext.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmContext.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmContext.SelectedPopupIcon = "./images/SelectedPopup.gif"
        'wmContext.ScrollEnabled = True
        'wmContext.Visible = True

        'cswmGroup1 = wmContext.Groups.Add()
        'cswmGroup1.ID = "Group_0bd4"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Define"
        'cswmItem.OnClick() = "showDefinition()"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "New Item"
        'cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Block Diagram (Pop-Up)"
        'cswmItem.OnClick() = "showExplain()"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "New Item"
        'cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Equations (Pop-Up)"
        'cswmItem.OnClick = "showEquation()"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "New Item"
        'cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

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
            Exit Sub
        End If

        'jrs 2014/07/18
        Dim sJscript As String = ""
        sJscript = sJscript & vbCr & "   var IFsVarHelpPath=[];" & vbCr
        sJscript = sJscript & vbCr & "   var IFsVarHelpEqnPath=[];" & vbCr

        Dim i As Integer
        Dim idx As Integer = 0
        'If Not CBool(Session("DoingHistory")) Then 'jrs 2005/12/05 change from Session("HistoricBase")
        '    'Do not use old
        '    'ForeCast
        '    Dim valtbl As DataTable = New DataTable
        '    Dim db = Session("PutFdrvRun") & "DataDictDyadicForecast.mdb"
        '    SetDataTablefromAccess(db, "SELECT Variable, Definition, Units, Aggregation FROM DataDict ORDER BY Variable", "", valtbl)
        '    idx = 0
        '    Do
        '        If bSearchInVarIdx(txtSearch.Text, idx, True, valtbl) Then
        '            lstSearch.Items.Add(valtbl.Rows(idx).Item("Variable") & " - " & valtbl.Rows(idx).Item("Definition") & " - " & valtbl.Rows(idx).Item("Units")) 'jrs 2008/01/03 added units
        '        End If
        '        idx = idx + 1
        '    Loop While (valtbl.Rows.Count > idx)
        'Else
        '    idx = 0
        '    Dim db As String = Session("PutFdrvRun") & "DataDictDyadic.mdb" '\RUNFILES\Data... zzNot copied to there!
        '    Dim valtbl As DataTable = New DataTable
        '    SetDataTablefromAccess(db, "SELECT Variable, Definition, Units, Aggregation FROM DataDict ORDER BY Variable", "", valtbl)
        '    Do
        '        If bSearchInVarIdx(txtSearch.Text, idx, True, valtbl) Then
        '            lstSearch.Items.Add(valtbl.Rows(idx).Item("Variable") & " - " & valtbl.Rows(idx).Item("Definition") & " - " & valtbl.Rows(idx).Item("Units")) 'jrs 2008/01/03 added units
        '        End If
        '        idx = idx + 1
        '    Loop While (valtbl.Rows.Count > idx)
        'End If
        '=======
        Select Case Session("OrigDyadic")
            Case "FC"
                Dim valtbl As DataTable = New DataTable
                Dim db = Session("PutFdrvRun") & "DataDictDyadicForecast.mdb"
                SetDataTablefromAccess(db, "SELECT Variable, Definition, Units, Aggregation FROM DataDict ORDER BY Variable", "", valtbl)
                idx = 0
                Do
                    If bSearchInVarIdx(txtSearch.Text, idx, True, valtbl) Then
                        lstSearch.Items.Add(valtbl.Rows(idx).Item("Variable") & " - " & valtbl.Rows(idx).Item("Definition") & " - " & valtbl.Rows(idx).Item("Units")) 'jrs 2008/01/03 added units
                    End If
                    idx = idx + 1
                Loop While (valtbl.Rows.Count > idx)
            Case "MA", "MP"
                Dim dbDD = Session("PutFdrvRun") & "DataDict.mdb"
                Dim rsDD As DataTable = New DataTable
                SetDataTablefromAccess(dbDD, "SELECT Variable, Definition, Units, Aggregation FROM DataDict WHERE Series = ""Yes"" ORDER BY Variable", "", rsDD)
                Dim sUnits = ""
                idx = 0
                For Each r As DataRow In rsDD.Rows
                    If bSearchInVarIdx(txtSearch.Text, idx, True, rsDD) Then
                        If r.Item("Units") Is DBNull.Value Then
                            sUnits = ""
                        Else
                            sUnits = r.Item("Units")
                        End If
                        lstSearch.Items.Add(r.Item("Variable") & " - " & r.Item("Definition") & " - " & sUnits)
                    End If
                    idx = idx + 1
                Next
            Case Else '"DY" and Saved zz tmp
                idx = 0
                Dim db As String = Session("PutFdrvRun") & "DataDictDyadic.mdb" '\RUNFILES\Data... zzNot copied to there!
                Dim valtbl As DataTable = New DataTable
                SetDataTablefromAccess(db, "SELECT Variable, Definition, Units, Aggregation FROM DataDict ORDER BY Variable", "", valtbl)
                Do
                    If bSearchInVarIdx(txtSearch.Text, idx, True, valtbl) Then
                        lstSearch.Items.Add(valtbl.Rows(idx).Item("Variable") & " - " & valtbl.Rows(idx).Item("Definition") & " - " & valtbl.Rows(idx).Item("Units")) 'jrs 2008/01/03 added units
                    End If
                    idx = idx + 1
                Loop While (valtbl.Rows.Count > idx)
        End Select

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

        'jrs 2014/07/18
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadHelpLists", sJscript, True)

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

        'jrs 2005/08/22
        'zzTemp Comm
        'If Not CBool(Session("HistoricBase")) Then
        '    Session("LoadSearchVar") = Session("LoadSearchVar") & " - " & VUnits(lstVarIdx.Items(lstSearch.SelectedIndex).Value)
        'End If

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
                    'jrs 2012/02/27
                    If CBool(Session("SensitivityFlag")) <> True Or VType(idx) = 1 Then
                        iSearchCount = iSearchCount + 1
                    End If
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

End Class
