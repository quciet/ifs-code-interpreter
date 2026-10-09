Partial Class frm_SearchParam
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
        wmContext.UserData = "jsolorza::976739975"

        wmContext.ClearPixelImage = "blank.gif"
        wmContext.PopupIcon = "./images/Popup.gif"
        wmContext.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmContext.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmContext.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmContext.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmContext.SelectedPopupIcon = "./images/SelectedPopup.gif"
        wmContext.ScrollEnabled = True
        'wmContext.Visible = False

        cswmGroup1 = wmContext.Groups.Add()
        cswmGroup1.ID = "Group_0bd4"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Define"
        cswmItem.OnClick() = "showDefinition()"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "New Item"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Block Diagram (Pop-Up)"
        cswmItem.OnClick() = "showExplain()"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "New Item"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Equations (Pop-Up)"
        cswmItem.OnClick = "showEquation()"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "New Item"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2012/01/16
        txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdSearch.ClientID + "')")
        'jrs 2013/09/04
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
            ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)

    End Sub


    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        lstSearch.Items.Clear()
        lstVarIdx.Items.Clear()

        'jrs 2008/06/04
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
        For i = 1 To NumbVars
            'jrs 2019/07/11 added and not VHidePrivateVars
            If (VType(i) = 2 Or VType(i) = 3) And VScenario(i) > "" And Not IsDBNull(VScenCat(i)) And Not VHidePrivateVars(i) Then
                'jrs 2008/06/04 If InStr(UCase(V(i)), UCase(txtSearch.Text)) > 0 Or InStr(UCase(VDef(i)), UCase(txtSearch.Text)) > 0 Or InStr(UCase(VExpl(i)), UCase(txtSearch.Text)) > 0 Then
                If bSearchInVarIdx(txtSearch.Text, i) Then
                    'jrs 2026/04/20 added V(i) & " - " & before VExpl to avoid problems with duplicated explanations
                    lstSearch.Items.Add(New ListItem(V(i) & " - " & VDef(i), CStr(VHelpID(i)) + "/" + CStr(VHelpIDEqn(i)) + "-" + IIf(VExpl(i) > "", V(i) & " - " & VExpl(i), V(i) & " - " & VDef(i))))
                    lstVarIdx.Items.Add(i)

                    'jrs 2025/01/28 added -1 to match javascript where we use lstSearch.SelectedIndex
                    'jrs 2014/07/18
                    sJscript = sJscript & "    IFsVarHelpPath[" & (lstSearch.Items.Count - 1).ToString & "] = '" & Replace(Replace(LoadIFsVarHelpJS(VHelpID(i)), "window.open('", ""), "');", "") & "';" & vbCr
                    sJscript = sJscript & "    IFsVarHelpEqnPath[" & (lstSearch.Items.Count - 1).ToString & "] = '" & Replace(Replace(LoadIFsVarHelpEqnJS(VHelpIDEqn(i)), "window.open('", ""), "');", "") & "';" & vbCr

                End If
            End If
        Next

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
        Session("LoadSearchIdx") = lstVarIdx.Items(lstSearch.SelectedIndex).Value
        Support.FormManager.Close(Me)
    End Sub


    'jrs 2008/05/20
    'Function created to search alternatively in all words in the search string (OR like)
    'Considering only the entire search string when using double quotation marks (AND like)
    Private Function bSearchInVarIdx(ByVal sSearch As String, ByVal idx As Integer) As Boolean
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

        For k = 0 To UBound(Target)
            If InStr(UCase(V(idx)), UCase(Target(k))) > 0 Or
                InStr(UCase(VDef(idx)), UCase(Target(k))) > 0 Or
                InStr(UCase(VExpl(idx)), UCase(Target(k))) > 0 Then
                iSearchCount = iSearchCount + 1
            End If
        Next k

        'if all words where find, then set it to true, other wise too many matches
        If iSearchCount = UBound(Target) + 1 Then
            bSearchInVarIdx = True
        End If

    End Function


End Class
