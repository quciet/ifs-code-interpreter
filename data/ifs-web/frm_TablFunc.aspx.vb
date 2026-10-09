Partial Class frm_TableFunc
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MsgBox As ArtinSoft.WinFormsToWeb.WebControls.MessageBox


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
            Lst_Names.Items.Clear()
            FuncDispCount = 0
            ReDim FuncDisp(TFCount)
            For EleLoop = 1 To TFCount
                'jrs 2013/11/18 If Session("UsedRun")(EleLoop) = "Yes" Or Session("UsedHisRun")(EleLoop) = "Yes" Then
                If bFilter(EleLoop) Then
                    Lst_Names.Items.Add(FuncName(EleLoop))
                    FuncDispCount = FuncDispCount + 1
                    FuncDisp(FuncDispCount) = EleLoop
                    'jrs 2013/11/18
                    CType(Session("NameIdxs"), Collection).Add(EleLoop, FuncName(EleLoop))
                End If
            Next EleLoop

            '    If Session("FuncInd")% = 0 Then Session("FuncInd")% = 1
            If Session("FuncInd") Is Nothing Or Session("FuncInd") = 0 Then
                Session("FuncInd") = 1
                'jrs 2005/11/14
                Session("FuncIdx") = 0
            End If
            'JRSVBNET CreateGraph(Session("FuncInd"))
            Lst_Names.SelectedIndex = Session("FuncIdx")  'jrs 2005/11/14 it was Session("FuncInd") - 1
            Session("TableTitle") = Lst_Names.SelectedValue

            '   Show Initial Points
            '   -------------------

            PtInd = 1
            Session("TblFnPtInd") = PtInd
            Session("TblFnFuncDisp") = FuncDisp
            txt_x.Text = Str(Session("XPoints")(Session("FuncInd"), PtInd))
            txt_y.Text = Str(Session("YPoints")(Session("FuncInd"), PtInd))

            'used in TablFunc2
            'set to nothing to identify in anychart xml file we're doing FuncGraph (and not TablFunc2)
            Session("TotalGraphs") = Nothing

        Else
            PtInd = Session("TblFnPtInd")
            FuncDisp = Session("TblFnFuncDisp")
        End If
        'jrs 2005/11/22
        'Dim cookRefreshGraph As New HttpCookie("RefreshGraph")
        'cookRefreshGraph.Value = Session("RefreshGraph")
        'Session("RefreshGraph") = "false"
        ''add the cookie
        'Response.Cookies.Add(cookRefreshGraph)
        'jrs 09/28/04
        'WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

    Dim PtInd
    Dim FuncDisp()

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblJS.Visible = True


        'JAM 2018-04-15
        PackJScriptBuilder()



        'jrs 2013/09/04
        ''ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS", _
        ''ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)
        'jrs 2013/11/18
        txtFilter.Attributes.Add("onkeypress", "return clickButton(event,'" + chkOrdByTime.ClientID + "')")



    End Sub

    Private Sub but_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_add.Click
        Dim iloop As Integer
        Dim msg As String
        If Session("TFuncPoints")(Session("FuncInd")) + 1 <= 10 Then 'Cannot Have more than 10 points in TablFunc
            For iloop = 1 To Session("TFuncPoints")(Session("FuncInd")) + 1
                If Not IsDBNull(Session("XPoints")(Session("FuncInd"), iloop)) Then
                    If Session("XPoints")(Session("FuncInd"), iloop) = Val(txt_x.Text) Then 'Cannot add existing x
                        msg = "You may not add a point with an existing x-axis value;"
                        msg = msg + " use ALTER instead."
                        LoadMessage(msg, Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If
                End If
            Next iloop
            Session("TFuncPoints")(Session("FuncInd")) = Session("TFuncPoints")(Session("FuncInd")) + 1
            Session("XPoints")(Session("FuncInd"), Session("TFuncPoints")(Session("FuncInd"))) = Val(txt_x.Text)
            Session("YPoints")(Session("FuncInd"), Session("TFuncPoints")(Session("FuncInd"))) = Val(txt_y.Text)
        Else
            msg = "You may not have more than 10 points in a table function"
            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        '   Sort Values
        '   -----------
        Dim i2 As Integer
        Dim SaveX As Object
        Dim SaveY As Object
        For iloop = 1 To Session("TFuncPoints")(Session("FuncInd")) - 1
            For i2 = iloop + 1 To Session("TFuncPoints")(Session("FuncInd"))
                If Session("XPoints")(Session("FuncInd"), i2) < Session("XPoints")(Session("FuncInd"), iloop) Then
                    SaveX = Session("XPoints")(Session("FuncInd"), iloop)
                    Session("XPoints")(Session("FuncInd"), iloop) = Session("XPoints")(Session("FuncInd"), i2)
                    Session("XPoints")(Session("FuncInd"), i2) = SaveX
                    SaveY = Session("YPoints")(Session("FuncInd"), iloop)
                    Session("YPoints")(Session("FuncInd"), iloop) = Session("YPoints")(Session("FuncInd"), i2)
                    Session("YPoints")(Session("FuncInd"), i2) = SaveY
                End If
            Next i2
        Next iloop

        'jrs 2006/01/03
        Dim db As String, sql As String, i As Integer, VarName As String, counter As Integer = 0
        db = Session("PutFdrvRun") & "TablFunc.mdb"
        sql = "UPDATE Tablfunc SET "
        For i = 1 To 10
            If IsNumeric(Session("XPoints")(Session("FuncInd"), i)) And IsNumeric(Session("YPoints")(Session("FuncInd"), i)) Then
                If counter > 0 Then
                    sql = sql & ", "
                End If
                counter = counter + 1
                VarName = "X" & Trim(Str(i)) & " = " & Session("XPoints")(Session("FuncInd"), i)
                sql = sql & VarName & ", "
                VarName = "Y" & Trim(Str(i)) & " = " & Session("YPoints")(Session("FuncInd"), i)
                sql = sql & VarName
            End If
        Next
        sql = sql & " WHERE [Function Name] = """ & FuncName(Session("FuncInd")) & """"
        updateAccessWith1Command(db, sql)


        '   Redisplay Graph
        '   ---------------

        'JRSVBNET CreateGraph(Session("FuncInd"))

        '   Show Initial Points
        '   -------------------

        PtInd = 1
        Session("TblFnPtInd") = PtInd
        txt_x.Text = Str$(Session("XPoints")(Session("FuncInd"), PtInd))
        txt_y.Text = Str$(Session("YPoints")(Session("FuncInd"), PtInd))

        'JAM 2018-05-30
        PackJScriptBuilder()
        lblJS.Enabled = True
        lblJS.Visible = True


    End Sub

    Private Sub but_Alter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_Alter.Click
        Dim iloop As Integer
        'jrs 2005/04/27
        'Verify that X value is not there yet
        For iloop = 1 To Session("TFuncPoints")(Session("FuncInd")) - 1
            If Val(txt_x.Text) = Session("XPoints")(Session("FuncInd"), iloop) And iloop <> PtInd Then
                LoadMessage("Repeated values for X are not allowed.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            If Val(txt_x.Text) < Session("XPoints")(Session("FuncInd"), iloop) Then
                Exit For
            End If
        Next iloop

        'jrs 09/15/04
        'If Session("TFuncPoints")(Session("FuncInd")) > 0 And Session("TFuncPoints")(Session("FuncInd")) > Val(txt_x.Text) And PtInd <> Val(txt_x.Text) Then
        '    PtInd = Val(txt_x.Text)
        '    Session("TblFnPtInd") = PtInd
        'End If
        Session("XPoints")(Session("FuncInd"), PtInd) = Val(txt_x.Text)
        Session("YPoints")(Session("FuncInd"), PtInd) = Val(txt_y.Text)


        '   Convert from analytic function to single point table function
        '   -------------------------------------------------------------

        If Session("TFuncPoints")(Session("FuncInd")) = 0 Then Session("TFuncPoints")(Session("FuncInd")) = 1

        '   Sort Values
        '   -----------
        Dim i2 As Integer
        Dim SaveX As Object
        Dim SaveY As Object
        For iloop = 1 To Session("TFuncPoints")(Session("FuncInd")) - 1
            For i2 = iloop + 1 To Session("TFuncPoints")(Session("FuncInd"))
                If Session("XPoints")(Session("FuncInd"), i2) < Session("XPoints")(Session("FuncInd"), iloop) Then
                    SaveX = Session("XPoints")(Session("FuncInd"), iloop)
                    Session("XPoints")(Session("FuncInd"), iloop) = Session("XPoints")(Session("FuncInd"), i2)
                    Session("XPoints")(Session("FuncInd"), i2) = SaveX
                    SaveY = Session("YPoints")(Session("FuncInd"), iloop)
                    Session("YPoints")(Session("FuncInd"), iloop) = Session("YPoints")(Session("FuncInd"), i2)
                    Session("YPoints")(Session("FuncInd"), i2) = SaveY
                End If
            Next i2
        Next iloop

        'jrs 2005/11/15
        Dim db As String, sql As String, i As Integer, VarName As String, counter As Integer = 0
        db = Session("PutFdrvRun") & "TablFunc.mdb"
        sql = "UPDATE Tablfunc SET "
        For i = 1 To 10
            If IsNumeric(Session("XPoints")(Session("FuncInd"), i)) And IsNumeric(Session("YPoints")(Session("FuncInd"), i)) Then
                If counter > 0 Then
                    sql = sql & ", "
                End If
                counter = counter + 1
                VarName = "X" & Trim(Str(i)) & " = " & Session("XPoints")(Session("FuncInd"), i)
                sql = sql & VarName & ", "
                VarName = "Y" & Trim(Str(i)) & " = " & Session("YPoints")(Session("FuncInd"), i)
                sql = sql & VarName
            End If
        Next
        sql = sql & " WHERE [Function Name] = """ & FuncName(Session("FuncInd")) & """"
        updateAccessWith1Command(db, sql)

        'jrs 2023/08/01
        Dim dbWork As String = Session("PutFdrvRun") & "Working.run.db"
        Dim tbl As DataTable
        sql = "SELECT * FROM ifs_reg_point WHERE RegressionName = """ & FuncName(Session("FuncInd")) & """"
        SetDataTablefromSQLite(dbWork, sql, "", tbl)
        Dim Points As List(Of Integer) = New List(Of Integer)
        For i = 1 To tbl.Rows.Count
            Points.Add(tbl.Rows(i - 1).Item("Seq"))
        Next

        For i = 1 To 10
            If IsNumeric(Session("XPoints")(Session("FuncInd"), i)) And IsNumeric(Session("YPoints")(Session("FuncInd"), i)) Then
                If Points.Contains(i) Then
                    sql = "UPDATE [ifs_reg_point] SET Y = " & Session("YPoints")(Session("FuncInd"), i)
                    sql = sql & ", X = " & Session("XPoints")(Session("FuncInd"), i)
                    sql = sql + " WHERE  [RegressionName] = """ & FuncName(Session("FuncInd")) & """ AND Seq = " & i.ToString
                Else
                    sql = "INSERT INTO [ifs_reg_point] VALUES(""" & FuncName(Session("FuncInd")) & """, 1, " & i.ToString & "," & Session("XPoints")(Session("FuncInd"), i) & "," & Session("YPoints")(Session("FuncInd"), i) & ")"
                End If
                updateSQLiteWith1Command(dbWork, sql)
            Else
                sql = "DELETE FROM ifs_reg_point"
                sql = sql + " WHERE  [RegressionName] = """ & FuncName(Session("FuncInd")) & """ AND Seq = " & i.ToString
                updateSQLiteWith1Command(dbWork, sql)
            End If
        Next
        Session("ChangingFunctions") = True

        '   Redisplay Graph
        '   ---------------

        'JRSVBNET CreateGraph(Session("FuncInd"))

        '   Show Initial Points
        '   -------------------

        PtInd = 1
        Session("TblFnPtInd") = PtInd
        txt_x.Text = Str(Session("XPoints")(Session("FuncInd"), PtInd))
        txt_y.Text = Str(Session("YPoints")(Session("FuncInd"), PtInd))

        'JAM 2018-05-30
        PackJScriptBuilder()
        lblJS.Enabled = True
        lblJS.Visible = True


    End Sub

    Private Sub but_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_delete.Click
        Dim Msg As String
        If Session("TFuncPoints")(Session("FuncInd")) - 1 >= 2 Then 'Cannot Have fewer than 2 points in TablFunc
            Session("XPoints")(Session("FuncInd"), PtInd) = 99999999
        Else
            Msg = "You may not have fewer than 2 points in a table function"
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        '   Sort Values
        '   -----------
        Dim iloop As Integer
        Dim i2 As Integer
        Dim SaveX As Object
        Dim SaveY As Object
        For iloop = 1 To Session("TFuncPoints")(Session("FuncInd")) - 1
            For i2 = iloop + 1 To Session("TFuncPoints")(Session("FuncInd"))
                If Session("XPoints")(Session("FuncInd"), i2) < Session("XPoints")(Session("FuncInd"), iloop) Then
                    SaveX = Session("XPoints")(Session("FuncInd"), iloop)
                    Session("XPoints")(Session("FuncInd"), iloop) = Session("XPoints")(Session("FuncInd"), i2)
                    Session("XPoints")(Session("FuncInd"), i2) = SaveX
                    SaveY = Session("YPoints")(Session("FuncInd"), iloop)
                    Session("YPoints")(Session("FuncInd"), iloop) = Session("YPoints")(Session("FuncInd"), i2)
                    Session("YPoints")(Session("FuncInd"), i2) = SaveY
                End If
            Next i2
        Next iloop

        '   Throw Away Top Point
        '   --------------------

        Session("TFuncPoints")(Session("FuncInd")) = Session("TFuncPoints")(Session("FuncInd")) - 1

        'jrs 2006/01/03
        Dim db As String, sql As String, i As Integer, VarName As String, counter As Integer = 0
        db = Session("PutFdrvRun") & "TablFunc.mdb"
        sql = "UPDATE Tablfunc SET "
        For i = 1 To 10
            If IsNumeric(Session("XPoints")(Session("FuncInd"), i)) And IsNumeric(Session("YPoints")(Session("FuncInd"), i)) Then
                If counter > 0 Then
                    sql = sql & ", "
                End If
                counter = counter + 1
                VarName = "X" & Trim(Str(i)) & " = " & Session("XPoints")(Session("FuncInd"), i)
                sql = sql & VarName & ", "
                VarName = "Y" & Trim(Str(i)) & " = " & Session("YPoints")(Session("FuncInd"), i)
                sql = sql & VarName
            End If
        Next
        sql = sql & " WHERE [Function Name] = """ & FuncName(Session("FuncInd")) & """"
        updateAccessWith1Command(db, sql)
        sql = "UPDATE Tablfunc SET X" & CStr(Session("TFuncPoints")(Session("FuncInd")) + 1) & " = NULL,"
        sql = sql & "Y" & CStr(Session("TFuncPoints")(Session("FuncInd")) + 1) & " = NULL"
        sql = sql & " WHERE [Function Name] = """ & FuncName(Session("FuncInd")) & """"
        updateAccessWith1Command(db, sql)

        '   Redisplay Graph
        '   ---------------

        'JRSVBNET CreateGraph(Session("FuncInd"))

        '   Show Initial Points
        '   -------------------

        PtInd = 1
        Session("TblFnPtInd") = PtInd
        txt_x.Text = Str$(Session("XPoints")(Session("FuncInd"), PtInd))
        txt_y.Text = Str$(Session("YPoints")(Session("FuncInd"), PtInd))

        'JAM 2018-05-30
        PackJScriptBuilder()
        lblJS.Enabled = True
        lblJS.Visible = True


    End Sub

    Private Sub but_next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_next.Click
        If PtInd + 1 <= Session("TFuncPoints")(Session("FuncInd")) Then
            PtInd = PtInd + 1
            Session("TblFnPtInd") = PtInd
            txt_x.Text = Str$(Session("XPoints")(Session("FuncInd"), PtInd))
            txt_y.Text = Str$(Session("YPoints")(Session("FuncInd"), PtInd))

            'JAM 2018-05-30
            PackJScriptBuilder()
            lblJS.Enabled = True
            lblJS.Visible = True


        End If

    End Sub

    Private Sub but_Previous_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_Previous.Click
        If PtInd - 1 >= 1 Then
            PtInd = PtInd - 1
            Session("TblFnPtInd") = PtInd
            txt_x.Text = Str$(Session("XPoints")(Session("FuncInd"), PtInd))
            txt_y.Text = Str$(Session("YPoints")(Session("FuncInd"), PtInd))


            'JAM 2018-05-30
            PackJScriptBuilder()
            lblJS.Enabled = True
            lblJS.Visible = True


        End If

    End Sub

    Private Sub but_reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_reset.Click
        'jrs 2006/01/03
        FileCopyCheck(PutDataFileDir & "TablFunc.mdb", Session("PutFdrvRun") & "TablFunc.mdb")
        'jrs 2023/08/02
        Session("ChangingFunctions") = Nothing


        '   Read Table Function Data File
        '   -----------------------------

        Dim valtbl6 As DataTable
        SetDataTablefromAccess(Session("PutFdrvRun") & "TablFunc.mdb", "Select * from TablFunc ORDER BY [Function Name]", "", valtbl6)
        Dim tfrecount As Integer
        Dim TLoop As Integer
        Dim i As Integer
        Dim iloop As Integer
        Dim VarName As String
        i = 0
        tfrecount = 0
        For TLoop = 1 To Session("TFCountFull")  'Probably should replace this with logic that separates
            'CoVaTrA table functions into separate file
            'Now TFCountFull includes CoVaTrA functions; TFCount may exclude
            If UCase(ModelLoad) = UCase$("LoadCoVaTrA") Or valtbl6.Rows(i).Item("CoVaTrA") = "No" Then
                tfrecount = tfrecount + 1
                Session("TFuncPoints")(tfrecount) = valtbl6.Rows(i).Item("TFuncPoints")
                For iloop = 1 To 10
                    VarName = Trim("X" + Trim(Str(iloop)))
                    Session("XPoints")(tfrecount, iloop) = valtbl6.Rows(i).Item(VarName)
                Next iloop
                For iloop = 1 To 10
                    VarName = Trim("Y" + Trim(Str(iloop)))
                    Session("YPoints")(tfrecount, iloop) = valtbl6.Rows(i).Item(VarName)
                Next iloop
            End If
            i = i + 1
        Next TLoop

        '   Recreate List Box and Graph
        '   ---------------------------

        Lst_Names.SelectedIndex = 0
        Session("FuncInd") = 1
        PtInd = 1
        Session("TblFnPtInd") = PtInd
        txt_x.Text = Str(Session("XPoints")(Session("FuncInd"), PtInd))
        txt_y.Text = Str(Session("YPoints")(Session("FuncInd"), PtInd))

        'JAM 2018-05-30
        PackJScriptBuilder()
        lblJS.Enabled = True
        lblJS.Visible = True


    End Sub

    Private Sub chk_ExtendList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_ExtendList.CheckedChanged
        If chk_ExtendList.Checked Then
            'jrs 2013/11/18 Lst_Names.Height = New Web.UI.WebControls.Unit(400)
            Lst_Names.Height = New Web.UI.WebControls.Unit(1500)
            Lst_Names.Rows = 60 'jrs 2013/11/18 16
        Else
            'jrs 2013/11/18 Lst_Names.Height = New Web.UI.WebControls.Unit(100)
            Lst_Names.Height = New Web.UI.WebControls.Unit(350)
            Lst_Names.Rows = 15 'jrs 2013/11/18 4
        End If
        'pnl_graph.Visible = Not (chk_ExtendList.Checked)
    End Sub

    Private Sub cmd_Exit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_Exit2.Click
        'jrs 2013/11/18
        Session.Remove("NameIdxs")

        'JAM 2018-06-03
        Session.Remove("TblFnPtInd")
        Session.Remove("TblFnFuncDisp")

        Dim msg As String
        msg = "Informational Warning:  Unlike parameter changes, "
        msg = msg + "table function changes are not saved with a scenario run "
        msg = msg + "file.  Thus any table function changes that you build into a "
        msg = msg + "scenario run must be reproduced manually in later sessions of "
        msg = msg + "model use.  Table function changes will remain active until "
        msg = msg + "you either reverse changes to original values or exit from IFs."

        LoadMessage(msg, Session("Message"))
        'jrs 2005/11/11
        Session("MsgParent") = "frm_MainMenu.aspx"
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    Private Sub cmd_Fit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_Fit.Click
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Analytic.aspx")
    End Sub

    Private Sub Lst_Names_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lst_Names.SelectedIndexChanged
        Session("FuncInd") = FuncDisp(Lst_Names.SelectedIndex + 1)
        'jrs 2005/11/14
        Session("FuncIdx") = Lst_Names.SelectedIndex

        'JRSVBNET CreateGraph(Session("FuncInd"))
        PtInd = 1
        Session("TblFnPtInd") = PtInd
        If Analytic(Session("FuncInd")) Then
            txt_x.Text = Str(0)
            txt_y.Text = Str(0)
            'jrs 2013/11/18
            lblRSqr.Text = "R^2 = " & Session("TFuncR2")(Session("FuncInd"))
            lblSE.Text = "SE = " & Session("TFuncSE")(Session("FuncInd"))
        Else
            txt_x.Text = Str(Session("XPoints")(Session("FuncInd"), PtInd))
            txt_y.Text = Str(Session("YPoints")(Session("FuncInd"), PtInd))

            'jrs 2013/11/18
            lblRSqr.Text = ""
            lblSE.Text = ""
        End If
        ''jrs 2005/11/22
        'If chk_ExtendList.Checked Then
        '    Session("RefreshGraph") = "true"
        'End If

        Lst_Names.Height = New System.Web.UI.WebControls.Unit(350)
        Lst_Names.Rows = 15
        chk_ExtendList.Checked = False
        Session("TableTitle") = Lst_Names.SelectedValue
        pnl_graph.Visible = True

        'JAM 2018-05-19
        PackJScriptBuilder()
        ''PackJScriptBuilderTest2()

        lblJS.Enabled = True
        lblJS.Visible = True


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
            Lst_Names_SelectedIndexChanged(Me, New EventArgs)

            'JAM 2018-06-03 Adding Filter to Global Variables
            Session("TblFnPtInd") = 1
            Session("TblFnFuncDisp") = FuncDisp

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
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '        JAM 2018-04-10 Building HighCharts Graph
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub PackJScriptBuilder()

        On Error GoTo PackJScritpBuilder_Error 'JAM 2016-11-24


        'Dim vgraphtitle As String
        Dim sFormula As String = ""



        If Session("TotalGraphs") = Nothing Then
            sFormula = CreateFormulaText(Session("iDevProFunIdx"), Session("BPoints"), Session("APoint"))
        End If


        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then
            Session("TableTitle") = sFormula
        Else
            Session("TableTitle") = ""
        End If

        'JAM 2018-05-19
        'Session("TableTitle") = "Graph"


        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr

        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr
        sJscript2 = sJscript2 & "chart: {" & vbCr

        sJscript2 = sJscript2 & "type: 'scatter'," & vbCr

        ''If Session("bBubble") = True Then
        ''    sJscript2 = sJscript2 & "type: 'bubble'," & vbCr
        ''Else
        ''    sJscript2 = sJscript2 & "type: 'scatter'," & vbCr
        ''End If



        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        If Session("TableTitle") > "" And Session("TableTitle") <> "Title" Then
            sJscript2 = sJscript2 & "text:       '" & Session("TableTitle") & "'," & vbCr
        Else
            sJscript2 = sJscript2 & "text:       ''," & vbCr
        End If
        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxisXLabel As String
        sAxisXLabel = getXAxisLabel()
        Dim sAxisYLabel As String
        sAxisYLabel = getYAxisLabel()

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<span style=""color:' + this.point.fillColor + '"">●</span>' + ' ' + this.point.name + '<br>" & sAxisXLabel & ": ' + this.point.x + '<br>" & sAxisYLabel & ": ' + this.point.y"
        sJscript2 = sJscript2 & ";" & vbCr

        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "enabled:     false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "plotOptions: {" & vbCr
        sJscript2 = sJscript2 & "   series: {" & vbCr
        sJscript2 = sJscript2 & "       point: {" & vbCr
        sJscript2 = sJscript2 & "           events: {" & vbCr
        sJscript2 = sJscript2 & "               click: function () {" & vbCr
        sJscript2 = sJscript2 & "                   this.options.dataLabels.enabled = !this.options.dataLabels.enabled;" & vbCr
        sJscript2 = sJscript2 & "                   this.update();" & vbCr
        sJscript2 = sJscript2 & "               }" & vbCr
        sJscript2 = sJscript2 & "           }" & vbCr
        sJscript2 = sJscript2 & "       }" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim sZeros As String = ""
        'sJscript2 = sJscript2 & PlotPairToJSSeries(Session("CCount"))


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'JAM 2018-04-24 Plotting points

        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then

            sJscript2 = sJscript2 & PlotPairToJSSeries(Session("iDevProFunIdx"), 0)

            'Country Point
            sJscript2 = sJscript2 & PlotPairToJSSeries(0, 0)

            'jrs 2013/12/02
            'Second Country Point for compare
            If Not Session("DevProReg1") = Nothing Then
                sJscript2 = sJscript2 & PlotPairToJSSeriesCompare()
            End If
        Else
            'TableFunc2
            If Session("FuncInd") = Nothing Then
                Dim i As Integer, count As Integer
                count = 0
                For i = 1 To TFCount
                    If Session("FuncSelected")(i) = True Then
                        'Function
                        count = count + 1
                        sJscript2 = sJscript2 & PlotPairToJSSeries(i, count)
                    End If
                Next i
            Else
                sJscript2 = sJscript2 & PlotPairToJSSeries(Session("FuncInd"), 1)
            End If
        End If






        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''






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

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90;" & vbCr

        sJscript2 = sJscript2 & "myWidth = 600;" & vbCr
        sJscript2 = sJscript2 & "myHeight = 300;" & vbCr


        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2


        Exit Sub
PackJScritpBuilder_Error:
        'MsgBox("Error PackJScritpBuilder_click() routine")
        Exit Sub

    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''    JAM 2018-04-10 Getting  Y-Axis Label
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>


    Private Function getYAxisLabel() As String

        On Error GoTo getYAxisLabel_Error 'JAM 2016-11-24

        'JAM 2018-05-30
        If Not IsNothing(YVariable(Session("FuncInd"))) Then
            getYAxisLabel = YVariable(Session("FuncInd"))
        Else
            getYAxisLabel = "Y-Axis"
        End If



        Exit Function
getYAxisLabel_Error:

        Exit Function

    End Function
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''    JAM 2018-04-10 Getting  X-Axis Label
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function getXAxisLabel() As String

        On Error GoTo getXAxisLabel


        'JAM 2018-05-30
        If Not IsNothing(XVariable(Session("FuncInd"))) Then
            getXAxisLabel = XVariable(Session("FuncInd"))
        Else
            getXAxisLabel = "X-Axis"
        End If




        Exit Function
getXAxisLabel:
        'MsgBox("Error getYAxisLabel() routine")
        Exit Function

    End Function
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''       JAM 2018-04-15 Plotting points
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' 
    ''' <returns></returns>
    ''' <remarks></remarks>


    Private Function PlotPairToJSSeries(ByVal i As Integer, ByVal count As Integer) As String

        On Error GoTo PlotPairToJSSeries_Error

        Dim Xval As Single, Yval As Single
        Dim j As Integer
        Dim bFirst As Boolean = True
        Dim sColorRGB As String = "", sName As String = ""


        Dim iNumDecY As Integer = 0

        Dim iNumDecX As Integer = 0


        Dim sJscript As String = "", sdataLabelsEnabled As String = ""


        If i > 0 Then
            'Check
            ''sJscript = sJscript & "<series name=""" & getSeriesName(i) & _
            ''""" type=""Line"" " & getColor(count) & ">" & vbCr 'style=""MySelected""

            sJscript = sJscript & "{name: """ & Session("DevProReg") & " - " & Session("DevProYr") & """," & vbCr


        Else
            'Check
            ''sJscript = sJscript & "<series id=""" & Session("DevProReg") & " - " & Session("DevProYr") & _
            ''""" name=""" & Session("DevProReg") & " - " & Session("DevProYr") & _
            ''""" type=""Bar"" color=""Blue"">" & vbCr 'style=""MySelected""


            sJscript = sJscript & "{name: """ & getSeriesName(i) & """," & vbCr
        End If
        Dim DimCount As Integer, bPoints As Boolean = False
        bPoints = Session("TFuncPoints")(i) > 0
        If bPoints Then
            DimCount = Session("TFuncPoints")(i)
        Else
            DimCount = 25
        End If

        sJscript = sJscript & "dataLabels: {" & vbCr

        sJscript = sJscript & "enabled: false," & vbCr
        sdataLabelsEnabled = "enabled: false"



        sJscript = sJscript & "format: '{point.name}'" & vbCr
        sJscript = sJscript & "}," & vbCr
        sJscript = sJscript & "data: ["

        sColorRGB = getColor(count)

        If i > 0 Then
            For j = 1 To DimCount
                'JAM 2018-04-28
                If bPoints Then
                    If IsNumeric(Session("XPoints")(i, j)) And IsNumeric(Session("YPoints")(i, j)) Then

                        If Not bFirst Then
                            sJscript = sJscript & ","
                        Else
                            bFirst = False
                        End If


                        If IsNumeric(Session("YPoints")(i, j)) Then
                            If Math.Abs(Session("YPoints")(i, j)) < 1 Then
                                iNumDecY = 3
                            ElseIf Math.Abs(Session("YPoints")(i, j)) < 10 Then
                                iNumDecY = 2
                            ElseIf Math.Abs(Session("YPoints")(i, j)) < 100 Then
                                iNumDecY = 1
                            End If
                        Else
                            iNumDecY = 2
                        End If

                        If IsNumeric(Session("XPoints")(i, j)) Then
                            If Math.Abs(Session("XPoints")(i, j)) < 1 Then
                                iNumDecX = 3
                            ElseIf Math.Abs(Session("XPoints")(i, j)) < 10 Then
                                iNumDecX = 2
                            ElseIf Math.Abs(Session("XPoints")(i, j)) < 100 Then
                                iNumDecX = 1
                            End If
                        Else
                            iNumDecX = 2
                        End If



                        sJscript = sJscript & "{name: """ & sName &
                            """, y: " & CStr(Math.Round(Session("YPoints")(i, j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("XPoints")(i, j), iNumDecX)) &
                            ", fillColor: '" & sColorRGB & "', color: '" & sColorRGB & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorRGB & "'}}}}" & vbCr
                    End If
                Else

                    If Not bFirst Then
                        sJscript = sJscript & ","
                    Else
                        bFirst = False
                    End If


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

                    If IsNumeric(Yval) Then
                        If Math.Abs(Yval) < 1 Then
                            iNumDecY = 3
                        ElseIf Math.Abs(Yval) < 10 Then
                            iNumDecY = 2
                        ElseIf Math.Abs(Yval) < 100 Then
                            iNumDecY = 1
                        End If
                    Else
                        iNumDecY = 2
                    End If

                    If IsNumeric(Xval) Then
                        If Math.Abs(Xval) < 1 Then
                            iNumDecX = 3
                        ElseIf Math.Abs(Xval) < 10 Then
                            iNumDecX = 2
                        ElseIf Math.Abs(Xval) < 100 Then
                            iNumDecX = 1
                        End If
                    Else
                        iNumDecX = 2
                    End If




                    sJscript = sJscript & "{name: """ & sName &
                          """, y: " & CStr(Math.Round(Yval, iNumDecY)) & ", x: " & CStr(Math.Round(Xval, iNumDecX)) &
                          ", fillColor: '" & sColorRGB & "', color: '" & sColorRGB & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorRGB & "'}}}}" & vbCr

                End If



            Next j

        Else

            sJscript = sJscript & "{name: """ & Session("DevProReg") & " - " & Session("DevProYr") &
                                """, y: " & CStr(Math.Round(Session("DPFunVal"), iNumDecY)) & ", x: " & CStr(Math.Round(Session("DPGDPPCP"), iNumDecX)) &
                                ", fillColor: '" & "RGB(0, 0, 150)" & "', color: '" & "RGB(0, 0, 150)" & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & "RGB(0, 0, 150)" & "'}}}}" & vbCr


        End If

        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "} " & vbCr

        PlotPairToJSSeries = sJscript

        Exit Function
PlotPairToJSSeries_Error:
        ' MsgBox("Error PlotPairToJSSeries() routine")
        Exit Function

    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''      JAM 2018-04-24  Series Name
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="ii"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>


    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String = ""
        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then
            Legend = Session("sDevProFun")
        Else
            Legend = FuncName(ii)
        End If

        getSeriesName = Legend
    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''   JAM 2018-04-30 Getting Point Color
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getColor(ByVal i As Integer) As String
        Dim sColor As String = ""
        If Session("TotalGraphs") = Nothing Then
            sColor = " RGB(0, 150, 0) "
        Else
            If i < 9 Then
                Select Case i
                    Case 1
                        sColor = " RGB(0, 150, 0) "
                    Case 2
                        sColor = " RGB(0, 0, 150) "
                    Case 3
                        sColor = " RGB(255, 0, 0) "
                    Case 5 'blue violet
                        sColor = " RGB(138,43,226) "
                    Case 6 'aqua 
                        sColor = " RGB(0,255,255) "
                    Case 7 'deep pink
                        sColor = "  RGB(255,20,147) "
                    Case 8 'medium spring green
                        sColor = " RGB(0,250,154) "
                End Select
            End If
        End If
        getColor = sColor
    End Function
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''     JAM 2018-04-30 Plotting Data Comparison
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PlotPairToJSSeriesCompare() As String
        Dim sJscript As String = ""

        'Check
        ''sJscript = sJscript & "<series id=""" & Session("DevProReg1") & " - " & Session("DevProYr1") & _
        ''    """ name=""" & Session("DevProReg1") & " - " & Session("DevProYr1") & _
        ''    """ type=""Bar"" color=""Red"">" & vbCr 'style=""MySelected""


        sJscript = sJscript & "<point y=""" & Session("DPFunVal1") & """ x =""" & Session("DPGDPPCP1") &
            """ />" & vbCr
        sJscript = sJscript & "</series>" & vbCr

        sJscript = sJscript & "{name: """ & Session("DevProReg1") & " - " & Session("DevProYr1") &
                """, y: " & CStr(Session("DPFunVal1")) & ", x: " & CStr(Session("DPGDPPCP1")) &
                ", fillColor: '" & "RGB(255, 0, 0)" & "', color: '" & "RGB(255, 0, 0)" & "', dataLabels: {" & "enabled: false" & "}, marker: {states: {hover: {fillColor: '" & "RGB(255, 0, 0)" & "'}}}}" & vbCr




        PlotPairToJSSeriesCompare = sJscript
    End Function




End Class

