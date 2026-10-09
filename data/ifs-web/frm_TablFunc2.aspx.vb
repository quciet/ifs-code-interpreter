Partial Class frm_TableFunc2
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        If Not Page.IsPostBack Then
            'jrs 2013/11/18
            Session("NameIdxs") = New Collection

            '   Load Table Function List Box
            '   ----------------------------
            Dim FuncDispCount As Integer
            Dim EleLoop As Integer

            Session("TotalGraphs") = 0


            Lst_Names.Items.Clear()
            FuncDispCount = 0
            ReDim FuncDisp(TFCount)
            For EleLoop = 1 To TFCount
                'jrs 2013/11/21
                If bFilter(EleLoop) Then
                    Lst_Names.Items.Add(FuncName(EleLoop))
                    'FuncDispCount = FuncDispCount + 1
                    'FuncDisp(FuncDispCount) = EleLoop
                    'jrs 2013/11/18
                    CType(Session("NameIdxs"), Collection).Add(EleLoop, FuncName(EleLoop))
                End If
            Next EleLoop

            '    If Session("FuncInd")% = 0 Then Session("FuncInd")% = 1
            'If Session("FuncInd") Is Nothing Or Session("FuncInd") = 0 Then
            '    Session("FuncInd") = 1
            'End If
            'JRSVBNET CreateGraph(Session("FuncInd"))
            Lst_Names.SelectedIndex = -1 '0 'Session("FuncInd") - 1
            Session("TableTitle") = Lst_Names.SelectedValue

            ReDim Session("FuncSelected")(TFCount)
            For EleLoop = 1 To TFCount
                Session("FuncSelected")(EleLoop) = False
            Next


            '   Show Initial Points
            '   -------------------

            'PtInd = 1
            'Session("TblFnPtInd") = PtInd
            'Session("TblFnFuncDisp") = FuncDisp
            'txt_x.Text = Str(Session("XPoints")(Session("FuncInd"), PtInd))
            'txt_y.Text = Str(Session("YPoints")(Session("FuncInd"), PtInd))
            Session("TablFuncType") = 1

            lbl_names.Text = "Select one or more functions and 'Draw Graph' from Menu"

            Dim dbcol As TMSWebControls.Column
            dbcol = dbgFunctions.Columns(0)
            dbcol.Width = 70

            dbcol = dbgFunctions.Columns(1)
            dbcol.Width = 30

            'jrs 2011/10/07
            Session("FuncInd") = Nothing
            Session("FuncSelected")(0) = True
            Lst_Names.SelectedIndex = 0
            mnuDrawGraph_Click(Me, New EventArgs)

        Else
            'PtInd = Session("TblFnPtInd")
            FuncDisp = Session("TblFnFuncDisp")

        End If

    End Sub

    Dim PtInd
    Dim FuncDisp()

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
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
        'jrs 2012/01/17
        'jrs 2013/11/21 can't have accessorkeycode when needing the enter key in texbox to send postback through javascript
        'cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler


        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_MainMenu.aspx"
        cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Graph"
        cswmGroup1.ID = "Group_fb60"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Draw"

        'jrs 2011/10/07
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Print"
        'cswmItem.Enabled = False

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Save"
        'cswmItem.Enabled = False

        'jrs 11/11/04
        lblMsg.Visible = False

        'jrs 2006/12/22
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16052);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jrs 2021/02/22
        ''jrs 2011/09/20
        'Dim sJscript2 As String
        'sJscript2 = "<script type=""text/javascript"" language=""javascript"">" & vbCr
        'sJscript2 = sJscript2 & "var chart = new AnyChart('./swf/AnyChart.swf');" & vbCr
        'sJscript2 = sJscript2 & "chart.width = 800;" & vbCr
        'sJscript2 = sJscript2 & "chart.height = 400;" & vbCr
        'sJscript2 = sJscript2 & "chart.wMode='opaque';" & vbCr

        'sJscript2 = sJscript2 & "//Set text data to chart" & vbCr
        'sJscript2 = sJscript2 & "chart.setXMLFile('AnyChartDataforFunc.aspx');" & vbCr

        'sJscript2 = sJscript2 & "chart.write();" & vbCr
        'sJscript2 = sJscript2 & "</script>"

        'lblJS.Text = sJscript2
        PackJS()

        'jrs 2013/11/18
        txtFilter.Attributes.Add("onkeypress", "return clickButton(event,'" + chkOrdByTime.ClientID + "')")

    End Sub




    Private Sub chk_ExtendList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_ExtendList.CheckedChanged

        If chk_ExtendList.Checked Then
            Lst_Names.Height = New Web.UI.WebControls.Unit(400)
            Lst_Names.Rows = 16

        Else
            Lst_Names.Height = New Web.UI.WebControls.Unit(100)
            Lst_Names.Rows = 4
        End If
        'jrs 10/11/04 PegoNet1.Visible = Not (chk_ExtendList.Checked)
    End Sub





    Private Sub Lst_Names_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lst_Names.SelectedIndexChanged
        'Session("FuncInd") = FuncDisp(Lst_Names.SelectedIndex + 1)

        ''JRSVBNET CreateGraph(Session("FuncInd"))
        'PtInd = 1
        'Session("TblFnPtInd") = PtInd
        ''If Analytic(Session("FuncInd")) Then
        ''    txt_x.Text = Str(0)
        ''    txt_y.Text = Str(0)
        ''Else
        ''    txt_x.Text = Str(Session("XPoints")(Session("FuncInd"), PtInd))
        ''    txt_y.Text = Str(Session("YPoints")(Session("FuncInd"), PtInd))
        ''End If

        'Lst_Names.Height = New System.Web.UI.WebControls.Unit(100)
        'chk_ExtendList.Checked = False
        'Session("TableTitle") = Lst_Names.SelectedValue
        'PegoNet1.Visible = True

    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Draw"
                mnuDrawGraph_Click(objSender, e)
            Case "Print"

            Case "Save"

        End Select
    End Sub

    Public Sub mnuDrawGraph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) 'Handles mnuDrawGraph.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'ReDim Session("FuncSelected")(TFCount)
        Dim li As ListItem
        'JRS 07/01/03
        Dim sSQL As String
        Dim i As Short
        Dim bFirstTime As Boolean
        Dim RSqr As DataTable


        'CreateGraph((1))
        Lst_Names.Height = New Web.UI.WebControls.Unit(100)
        Lst_Names.Rows = 4
        chk_ExtendList.Checked = False



        For i = 1 To TFCount
            Session("FuncSelected")(i) = False
        Next
        'jrs 2013/11/20 added StandardError
        sSQL = "SELECT [Function Name] AS Name, RSquared, a,b1,b2,b3,b4,b5,b6,b7,b8,Analytic, StdDev FROM TablFunc WHERE [Function Name] = """
        bFirstTime = True
        i = 0
        Session("TotalGraphs") = 0
        For Each li In Lst_Names.Items
            'jrs 2013/11/21 i = i + 1
            i = CType(Session("NameIdxs"), Collection).Item(li.Text)
            If li.Selected = True Then
                Session("FuncSelected")(i) = True
                Session("TotalGraphs") = Session("TotalGraphs") + 1
                If bFirstTime Then
                    sSQL = sSQL & li.Text & """"
                    bFirstTime = False
                Else
                    sSQL = sSQL & " OR [Function Name] = """ & li.Text & """"
                End If
            Else
                Session("FuncSelected")(i) = False
            End If

        Next li
        sSQL = sSQL & " ORDER BY [Function Name]"

        'jrs 11/11/04
        If Session("TotalGraphs") > 10 Then
            lblMsg.Text = "You can select only 10 variables or less!!!"
            lblMsg.Visible = True
            Exit Sub
        End If
        SetDataTablefromAccess(Session("PutFdrvRun") & "TablFunc.mdb", sSQL, "", RSqr)

        'UPGRADE_ISSUE: Data property dataFunctions.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'dataFunctions.RecordSource = sSQL

        'dataFunctions.Refresh()
        'dbgFunctions.CtlRefresh()
        dbgFunctions.RowCount = 0
        dbgFunctions.RowCount = RSqr.Rows.Count

        'jrs 2008/10/07
        Dim TmpVal As Object, TmpText As String
        Dim TmpVal1 As Object, FuncType As String

        For i = 1 To RSqr.Rows.Count
            dbgFunctions(0, i - 1) = RSqr.Rows(i - 1).Item(0)
            If Not IsDBNull(RSqr.Rows(i - 1).Item(1)) Then
                dbgFunctions(2, i - 1) = Math.Round(RSqr.Rows(i - 1).Item(1), 4)
            Else
                dbgFunctions(2, i - 1) = ""
            End If
            'jrs 2013/11/20
            If Not IsDBNull(RSqr.Rows(i - 1).Item("StdDev")) Then
                dbgFunctions(3, i - 1) = Math.Round(RSqr.Rows(i - 1).Item("StdDev"), 4)
            Else
                dbgFunctions(3, i - 1) = ""
            End If

            'jrs 2008/10/07
            TmpVal = RSqr.Rows(i - 1).Item("Analytic")
            If TmpVal = 1 Then 'Analytic function
                FuncType = ""
                TmpText = "" '" Y = "
                TmpVal = RSqr.Rows(i - 1).Item("a")
                If Not IsDBNull(TmpVal) Then
                    TmpVal = Math.Round(TmpVal, 4)
                    If TmpVal <> 0 Then
                        TmpText = TmpText + Trim(Str(TmpVal))
                    End If
                End If
                TmpVal = RSqr.Rows(i - 1).Item("b1")
                If Not IsDBNull(TmpVal) Then
                    TmpVal = Math.Round(TmpVal, 4)
                    If TmpVal > 0 Then
                        TmpText = TmpText + IIf(Len(TmpText) = 0, "", " + ") + Trim(Str(Math.Abs(TmpVal))) + " X"
                    ElseIf TmpVal < 0 Then
                        TmpText = TmpText + " - " + Trim(Str(Math.Abs(TmpVal))) + " X"
                    End If
                    If TmpVal <> 0 Then
                        FuncType = "(Linear) "
                    End If
                End If

                TmpVal = RSqr.Rows(i - 1).Item("b2")
                If Not IsDBNull(TmpVal) Then
                    TmpVal = Math.Round(TmpVal, 4)
                    If TmpVal > 0 Then
                        TmpText = TmpText + IIf(Len(TmpText) = 0, "", " + ") + Trim(Str(Math.Abs(TmpVal))) + " X^2"
                    ElseIf TmpVal < 0 Then
                        TmpText = TmpText + " - " + Trim(Str(Math.Abs(TmpVal))) + " X^2"
                    End If
                    If TmpVal <> 0 Then
                        FuncType = "(Quadratic) "
                    End If
                End If
                TmpVal = RSqr.Rows(i - 1).Item("b3")
                If Not IsDBNull(TmpVal) Then
                    TmpVal = Math.Round(TmpVal, 4)
                    If TmpVal > 0 Then
                        TmpText = TmpText + IIf(Len(TmpText) = 0, "", " + ") + Trim(Str(Math.Abs(TmpVal))) + " X^3"
                    ElseIf TmpVal < 0 Then
                        TmpText = TmpText + " - " + Trim(Str(Math.Abs(TmpVal))) + " X^3"
                    End If
                    If TmpVal <> 0 Then
                        FuncType = "(Cubic) "
                    End If
                End If
                TmpVal = RSqr.Rows(i - 1).Item("b4")
                If Not IsDBNull(TmpVal) Then
                    TmpVal = Math.Round(TmpVal, 4)
                    'jrs 2007/12/12 change LOG to LN
                    If TmpVal > 0 Then
                        TmpText = TmpText + IIf(Len(TmpText) = 0, "", " + ") + Trim(Str(Math.Abs(TmpVal))) + " LN(X)"
                    ElseIf TmpVal < 0 Then
                        TmpText = TmpText + " - " + Trim(Str(Math.Abs(TmpVal))) + " LN(X)"
                    End If
                    If TmpVal <> 0 Then
                        FuncType = "(Logarithmic) "
                    End If
                End If

                TmpVal = RSqr.Rows(i - 1).Item("b5")
                TmpVal1 = RSqr.Rows(i - 1).Item("b6")
                If Not IsDBNull(TmpVal) And Not IsDBNull(TmpVal1) Then
                    TmpVal = Math.Round(TmpVal, 4)
                    TmpVal1 = Math.Round(TmpVal1, 4)
                    If TmpVal > 0 Then
                        TmpText = TmpText + IIf(Len(TmpText) = 0, "", " + ") + Trim(Str(Math.Abs(TmpVal))) + " X^(" + Trim(Str(TmpVal1)) + ")"
                    ElseIf TmpVal < 0 Then
                        TmpText = TmpText + " - " + Trim(Str(Math.Abs(TmpVal))) + " X^(" + Trim(Str(TmpVal1)) + ")"
                    End If
                    If TmpVal <> 0 Then
                        FuncType = "(Power) "
                    End If
                End If

                TmpVal = RSqr.Rows(i - 1).Item("b7")
                TmpVal1 = RSqr.Rows(i - 1).Item("b8")
                If Not IsDBNull(TmpVal) And Not IsDBNull(TmpVal1) Then
                    TmpVal = Math.Round(TmpVal, 4)
                    TmpVal1 = Math.Round(TmpVal1, 4)
                    If TmpVal > 0 Then
                        TmpText = TmpText + IIf(Len(TmpText) = 0, "", " + ") + Trim(Str(Math.Abs(TmpVal))) + " exp(" + Trim(Str(TmpVal1)) + " X)"
                    ElseIf TmpVal < 0 Then
                        TmpText = TmpText + " - " + Trim(Str(Math.Abs(TmpVal))) + " exp(" + Trim(Str(TmpVal1)) + " X)"
                    End If
                    If TmpVal <> 0 Then
                        FuncType = "(Exponential) "
                    End If
                End If
                'TmpText = FuncType + " Y = " + TmpText
                TmpText = " Y = " + TmpText
                dbgFunctions(1, i - 1) = TmpText
            Else 'TmpVal = 0 ;Tablefunction
                dbgFunctions(1, i - 1) = "Table Function"
            End If

        Next i

        'jrs 2021/02/22 lblJS.Visible = True
        PackJS()

    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAll.CheckedChanged
        If chkAll.Checked Then
            chkRun.Checked = True
            chkRunHis.Checked = True
            chkPre.Checked = True
            chkPreHis.Checked = True
            chkNotUsed.Checked = True
            chkAll.Enabled = False
        End If
        chkOrdByTime_CheckedChanged(Me, New EventArgs)
    End Sub

    Protected Sub chkOrdByTime_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkOrdByTime.CheckedChanged
        Lst_Names.Items.Clear()
        Dim FuncDispCount As Integer
        FuncDispCount = 0
        Dim bFound As Boolean
        If chkOrdByTime.Checked Then
            Dim db As String
            db = DataFileDir & "TablFunc.mdb"
            Dim rs As DataTable
            Dim sqlWhere As String
            sqlWhere = sFilterSql()

            If sqlWhere > "" Then
                sqlWhere = "WHERE " & sqlWhere
            End If
            SetDataTablefromAccess(db, "SELECT [Function Name] FROM TablFunc " & sqlWhere & " ORDER BY ChangeDate DESC, [Function Name] ASC", "", rs)
            Dim rr As Integer = 0
            ReDim FuncDisp(TFCount)
            Do While Not rr = rs.Rows.Count
                If txtFilter.Text > "" Then
                    bFound = bSearchInListItem(txtFilter.Text, rs.Rows(rr).Item("Function Name"))
                Else
                    bFound = True
                End If
                If bFound Then
                    Lst_Names.Items.Add(rs.Rows(rr).Item("Function Name"))
                    FuncDispCount = FuncDispCount + 1
                    FuncDisp(FuncDispCount%) = CType(Session("NameIdxs"), Collection).Item(rs.Rows(rr).Item("Function Name"))
                End If

                rr = rr + 1
            Loop
        Else
            'jrs 2021/02/22
            ReDim FuncDisp(TFCount)

            For EleLoop = 1 To TFCount
                If bFilter(CInt(EleLoop)) Then
                    If txtFilter.Text > "" Then
                        bFound = bSearchInListItem(txtFilter.Text, FuncName$(EleLoop))
                    Else
                        bFound = True
                    End If

                    If bFound Then
                        Lst_Names.Items.Add(FuncName$(EleLoop))
                        FuncDispCount% = FuncDispCount% + 1
                        FuncDisp(FuncDispCount%) = CType(Session("NameIdxs"), Collection).Item(FuncName$(EleLoop))
                    End If
                End If
            Next EleLoop
        End If
        If Lst_Names.Items.Count > 0 Then
            Lst_Names.Items(0).Selected = True
            'Lst_Names_SelectedIndexChanged(Me, New EventArgs)
            mnuDrawGraph_Click(Me, New EventArgs)
        Else
            LoadMessage("No Functions found, please change your filter", Session("Message"))
            Session("MsgParent") = "frm_TablFunc.aspx"
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

    End Sub

    'jrs 2013/11/14
    Private Function bNoneChecked() As Boolean
        If chkRun.Checked Or chkRunHis.Checked Or chkPre.Checked Or chkPreHis.Checked Or chkNotUsed.Checked Then
            bNoneChecked = False
        Else
            bNoneChecked = True
        End If
    End Function

    'jrs 2013/11/14
    Private Function bAllChecked() As Boolean
        If chkRun.Checked And chkRunHis.Checked And chkPre.Checked And chkPreHis.Checked And chkNotUsed.Checked Then
            bAllChecked = True
        Else
            bAllChecked = False
        End If
    End Function

    'jrs 2013/11/14
    Private Function bFilter(idx As Integer) As Boolean
        If chkRun.Checked And Session("UsedRun")(idx) = "Yes" Then
            bFilter = True
        ElseIf chkRunHis.Checked And Session("UsedHisRun")(idx) = "Yes" Then
            bFilter = True
        ElseIf chkPre.Checked And Session("UsedPre")(idx) = "Yes" Then
            bFilter = True
        ElseIf chkPreHis.Checked And Session("UsedPreHis")(idx) = "Yes" Then
            bFilter = True
        ElseIf chkNotUsed.Checked And Session("UsedRun")(idx) = "No" And Session("UsedHisRun")(idx) = "No" And Session("UsedPre")(idx) = "No" And Session("UsedPreHis")(idx) = "No" Then
            bFilter = True
        Else
            bFilter = False
        End If
    End Function

    'jrs 2013/11/14
    Private Function sFilterSql() As String
        sFilterSql = ""
        If chkAll.Checked Then
            Exit Function
        End If

        If chkRun.Checked Then
            sFilterSql = sFilterSql & "UsedRun = 'Yes'"
        End If

        If chkRunHis.Checked Then
            If sFilterSql > "" Then
                sFilterSql = sFilterSql & " OR "
            End If
            sFilterSql = sFilterSql & "UsedHisRun = 'Yes'"
        End If

        If chkPre.Checked Then
            If sFilterSql > "" Then
                sFilterSql = sFilterSql & " OR "
            End If
            sFilterSql = sFilterSql & "UsedPre = 'Yes'"
        End If

        If chkPreHis.Checked Then
            If sFilterSql > "" Then
                sFilterSql = sFilterSql & " OR "
            End If
            sFilterSql = sFilterSql & "UsedHisPre = 'Yes'"
        End If

        If chkNotUsed.Checked Then
            If sFilterSql > "" Then
                sFilterSql = sFilterSql & " OR "
            End If
            sFilterSql = sFilterSql & "(UsedRun = 'No' AND UsedHisRun = 'No' AND UsedPre = 'No' AND UsedHisPre = 'Yes')"
        End If
    End Function


    Protected Sub chkNotUsed_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkNotUsed.CheckedChanged
        If Not chkNotUsed.Checked Then
            chkAll.Checked = False
            chkAll.Enabled = True
            If bNoneChecked() Then
                chkNotUsed.Checked = True

                LoadMessage("You need to have at least one option selected.", Session("Message"))
                Session("MsgParent") = "frm_TablFunc.aspx"
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
        Else
            If bAllChecked() Then
                chkAll.Checked = True
            End If
        End If
        chkOrdByTime_CheckedChanged(Me, New EventArgs)

    End Sub

    Protected Sub chkPre_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkPre.CheckedChanged
        If Not chkPre.Checked Then
            chkAll.Checked = False
            chkAll.Enabled = True
            If bNoneChecked() Then
                chkPre.Checked = True

                LoadMessage("You need to have at least one option selected.", Session("Message"))
                Session("MsgParent") = "frm_TablFunc.aspx"
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        Else
            If bAllChecked() Then
                chkAll.Checked = True
            End If
        End If
        chkOrdByTime_CheckedChanged(Me, New EventArgs)

    End Sub

    Protected Sub chkPreHis_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkPreHis.CheckedChanged
        If Not chkPreHis.Checked Then
            chkAll.Checked = False
            chkAll.Enabled = True
            If bNoneChecked() Then
                chkPreHis.Checked = True

                LoadMessage("You need to have at least one option selected.", Session("Message"))
                Session("MsgParent") = "frm_TablFunc.aspx"
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        Else
            If bAllChecked() Then
                chkAll.Checked = True
            End If
        End If
        chkOrdByTime_CheckedChanged(Me, New EventArgs)
    End Sub

    Protected Sub chkRun_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkRun.CheckedChanged
        If Not chkRun.Checked Then
            chkAll.Checked = False
            chkAll.Enabled = True
            If bNoneChecked() Then
                chkRun.Checked = True

                LoadMessage("You need to have at least one option selected.", Session("Message"))
                Session("MsgParent") = "frm_TablFunc.aspx"
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        Else
            If bAllChecked() Then
                chkAll.Checked = True
            End If
        End If
        chkOrdByTime_CheckedChanged(Me, New EventArgs)
    End Sub

    Protected Sub chkRunHis_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkRunHis.CheckedChanged
        If Not chkRunHis.Checked Then
            chkAll.Checked = False
            chkAll.Enabled = True
            If bNoneChecked() Then
                chkRunHis.Checked = True

                LoadMessage("You need to have at least one option selected.", Session("Message"))
                Session("MsgParent") = "frm_TablFunc.aspx"
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        Else
            If bAllChecked() Then
                chkAll.Checked = True
            End If
        End If
        chkOrdByTime_CheckedChanged(Me, New EventArgs)

    End Sub

    Private Sub PackJS()

        'jrs 2011/09/20
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/11 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        sJscript2 = sJscript2 & "marginBottom: 100" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sFormula As String = ""
        If Session("TotalGraphs") = Nothing Then
            sFormula = CreateFormulaText(Session("iDevProFunIdx"), Session("BPoints"), Session("APoint"))
        End If

        sJscript2 = sJscript2 & "title: {" & vbCr
        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then
            sJscript2 = sJscript2 & "text:       '" & sFormula & "'," & vbCr
        Else
            sJscript2 = sJscript2 & "text:       ''," & vbCr
        End If
        sJscript2 = sJscript2 & "x: -20, //center" & vbCr
        sJscript2 = sJscript2 & "y: -20, //center" & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'bottom'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'sJscript2 = sJscript2 & "xAxis: {" & vbCr
        'sJscript2 = sJscript2 & "title: {" & vbCr
        'sJscript2 = sJscript2 & "text:       ''" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & ", min: " & Session("FirstYear") & vbCr
        'sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("DisYear") - 1) & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        'sJscript2 = sJscript2 & "title: {" & vbCr
        'sJscript2 = sJscript2 & "text:       '" '" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr


        'jrs 2015/09/08
        sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
        sJscript2 = sJscript2 & "align:      'center'," & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
        sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then
            'Function
            sJscript2 = sJscript2 & PlotPairToJSSeries(Session("iDevProFunIdx"), 0)

        Else
            'TableFunc2
            If Session("FuncInd") = Nothing Then
                Dim i As Integer, count As Integer
                count = 0
                For i = 1 To TFCount
                    If Session("FuncSelected")(i) = True Then
                        'Function
                        If count > 0 Then
                            sJscript2 = sJscript2 & ","
                        End If
                        count = count + 1
                        sJscript2 = sJscript2 & PlotPairToJSSeries(i, count)
                    End If
                Next i
            Else
                sJscript2 = sJscript2 & PlotPairToJSSeries(Session("FuncInd"), 1)
            End If
        End If

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr
        sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".css({"
        sJscript2 = sJscript2 & "cursor: 'pointer'"
        sJscript2 = sJscript2 & "})"
        sJscript2 = sJscript2 & ".add();" & vbCr


        sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.65;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.60;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub

    Private Function PlotPairToJSSeries(ByVal i As Integer, ByVal count As Integer) As String
        Dim idx As Integer = Session("DisYear")
        Dim iNumDecY As Integer = 2

        Dim iNumDecX As Integer = 2

        Dim sJscript As String = "", sdataLabelsEnabled As String = ""

        If i > 0 Then
            sJscript = sJscript & "{name: """ & getSeriesName(i) & """, color: " & getColor(count) & ", zIndex: " & i.ToString & "," & vbCr
        Else
            sJscript = sJscript & "{name: """ & Session("DevProReg") & " - " & Session("DevProYr") & """, type: ""column"", color: ""Blue"", zIndex: 0," & vbCr
        End If
        sJscript = sJscript & "data: ["

        Dim bFirst As Boolean = True
        Dim DimCount As Integer, bPoints As Boolean = False
        bPoints = Session("TFuncPoints")(i) > 0
        If bPoints Then
            DimCount = Session("TFuncPoints")(i)
        Else
            DimCount = 25
        End If

        Dim j As Integer, Xval As Single, Yval As Single

        If i > 0 Then
            For j = 1 To DimCount
                If bPoints Then
                    If IsNumeric(Session("XPoints")(i, j)) And IsNumeric(Session("YPoints")(i, j)) Then
                        If Not bFirst Then
                            sJscript = sJscript & ","
                        Else
                            bFirst = False
                        End If

                        sJscript = sJscript & "{y:" & CStr(Math.Round(Session("YPoints")(i, j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("XPoints")(i, j), iNumDecX)) & "}" & vbCr
                    End If
                Else
                    If Not IsNumeric(Session("MinPoint")(i)) Or Not IsNumeric(Session("MaxPoint")(i)) Then
                        LoadMessage("Minimum or maximum point on table function must be non-null", Session("Message"))
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If
                    'JRS 10/01/02 ERROR HANDLING
                    'we were getting a log(0) error
                    If Session("MinPoint")(i) = Session("MaxPoint")(i) Then
                        Session("MaxPoint")(i) = Session("MinPoint")(i) + 25
                    End If
                    'JRS 08/12/03 it used to be 100, changed to 25
                    Xval = Session("MinPoint")(i) + (Session("MaxPoint")(i) - Session("MinPoint")(i)) / 25 * j
                    Yval = Session("APoint")(i) + Session("BPoints")(i, 1) * Xval _
                            + Session("BPoints")(i, 2) * Xval ^ 2 + Session("BPoints")(i, 3) * Xval ^ 3 _
                            + Session("BPoints")(i, 4) * Math.Log(Xval) + Session("BPoints")(i, 5) * Xval ^ Session("BPoints")(i, 6) _
                            + Session("BPoints")(i, 7) * Math.Exp(Session("BPoints")(i, 8) * Xval)

                    If Not bFirst Then
                        sJscript = sJscript & ","
                    Else
                        bFirst = False
                    End If

                    sJscript = sJscript & "{y:" & CStr(Math.Round(Yval, iNumDecY)) & ", x: " & CStr(Math.Round(Xval, iNumDecX)) & "}" & vbCr
                End If
            Next j
        Else
            sJscript = sJscript & "{y:" & CStr(Math.Round(Session("DPFunVal"), iNumDecY)) & ", x: " & CStr(Math.Round(Session("DPGDPPCP"), iNumDecX)) & "}" & vbCr
        End If
        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "} " & vbCr

        PlotPairToJSSeries = sJscript
    End Function

    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String = ""
        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then
            Legend = Session("sDevProFun")
        Else
            Legend = FuncName(ii)
        End If

        getSeriesName = Legend
    End Function

    Private Function getColor(ByVal i As Integer) As String
        Dim sColor As String = ""
        If Session("TotalGraphs") = Nothing Then
            sColor = " color=""Lime"""
        Else
            If i < 9 Then
                Select Case i
                    Case 1
                        sColor = " color=""Lime"""
                    Case 2
                        sColor = " color=""Blue"""
                    Case 3
                        sColor = " color=""Red"""
                    Case 4 'blue violet 'jrs 2024/06/18
                        sColor = " color=""RGB(138,43,226)"""
                    Case 5 'aqua 
                        sColor = " color=""RGB(0,255,255)"""
                    Case 6 'deep pink
                        sColor = " color=""RGB(255,20,147)"""
                    Case 7 'medium spring green
                        sColor = " color=""RGB(0,250,154)"""
                    Case Else
                        sColor = """RGB(" & (RandD Mod 256).ToString & "," & (RandD Mod 256).ToString & "," & (RandD Mod 256).ToString & ")"""
                End Select
            End If
        End If
        getColor = sColor
    End Function

End Class

