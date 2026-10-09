Option Strict Off
Option Explicit On
Imports System.Data.SQLite


Partial Class frm_AnalFunc
    Inherits Page


    'jam 2023/02/26
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vConnString As String
    Dim vSQLiteCommand2 As SqliteCommand
    Dim vSQLiteReader2 As SqliteDataReader
    'jrs 2023/08/01
    Dim vSQLiteConWork As SqliteConnection


#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'Do not modify it using the code editor.
        'jam 2023/03/05 Due sqlite migration
        'InitializeComponent()
        'jam 2023/03/05
        'Me.OleDbConnection1.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database L" & "ocking Mode=0;Jet OLEDB:Database Password=;Data Source=""" & Session("PutFdrvRun") & "AnalFunc.mdb"";Password=;Jet OLEDB:Engine Type=4;Jet OLEDB:Global Bulk Transactio" & "ns=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP" & "=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password" & "=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=" & "False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Enc" & "rypt Database=False"
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            lbl_Eqn.Text = "Y = a + b1*X + b2*X^2 + b3* X^3 + b4*ln(X) "
            lbl_Eqn.Text = lbl_Eqn.Text & "+ b5*X^b6 + b7*e^(b8 * X)"
        End If

        'jrs 2009/03/17
        'WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

    Protected WithEvents OleDbSelectCommand1 As System.Data.OleDb.OleDbCommand
    Protected WithEvents OleDbConnection1 As System.Data.OleDb.OleDbConnection
    Protected WithEvents OleDbDAAnalFunc As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents DsAnalFunc1 As dsAnalFunc
    Protected WithEvents OleDbDAIndepVar As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents OleDbSelectCommand2 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DsIndepVar1 As dsIndepVar

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.OleDbDAAnalFunc = New System.Data.OleDb.OleDbDataAdapter
        Me.OleDbSelectCommand1 = New System.Data.OleDb.OleDbCommand
        Me.OleDbConnection1 = New System.Data.OleDb.OleDbConnection
        Me.DsAnalFunc1 = New dsAnalFunc
        Me.OleDbDAIndepVar = New System.Data.OleDb.OleDbDataAdapter
        Me.OleDbSelectCommand2 = New System.Data.OleDb.OleDbCommand
        Me.DsIndepVar1 = New dsIndepVar
        CType(Me.DsAnalFunc1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsIndepVar1, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'OleDbDAAnalFunc
        '
        Me.OleDbDAAnalFunc.SelectCommand = Me.OleDbSelectCommand1
        Me.OleDbDAAnalFunc.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "AnalFunc", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Function Name", "Function Name"), New System.Data.Common.DataColumnMapping("a", "a")})})
        '
        'OleDbSelectCommand1
        '

        'jrs 2013/11/22 added where and order
        Dim sql As String = "SELECT DISTINCT ChangeDate, [Function Name], round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE FROM AnalFunc"
        If txtFilter.Text > "" Then
            sql = sql & " WHERE [Function Name] LIKE '%" & txtFilter.Text & "%' "
        End If

        Dim sqlWhere As String
        sqlWhere = sFilterSql()
        If sqlWhere > "" Then
            If txtFilter.Text > "" Then
                sql = sql & " AND " & sqlWhere
            Else
                sql = sql & " WHERE " & sqlWhere
            End If
        End If

        If chkOrdByTime.Checked Then
            sql = sql & " ORDER BY ChangeDate DESC, [Function Name] ASC"
        Else
            sql = sql & " ORDER BY [Function Name] ASC"
        End If

        Me.OleDbSelectCommand1.CommandText = sql
        Me.OleDbSelectCommand1.Connection = Me.OleDbConnection1
        '
        'OleDbConnection1
        '
        Me.OleDbConnection1.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database L" &
        "ocking Mode=0;Data Source=""" & Session("PutFdrvRun") & "\AnalFunc.mdb"";Jet OLEDB:" &
        "Engine Type=4;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet " &
        "OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny" &
        " None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Je" &
        "t OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Rep" &
        "air=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1"
        '
        'DsAnalFunc1
        '
        Me.DsAnalFunc1.DataSetName = "dsAnalFunc"
        Me.DsAnalFunc1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDAIndepVar
        '
        Me.OleDbDAIndepVar.SelectCommand = Me.OleDbSelectCommand2
        Me.OleDbDAIndepVar.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "AnalFunc", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Indep Var", "Indep Var"), New System.Data.Common.DataColumnMapping("b1", "b1"), New System.Data.Common.DataColumnMapping("b2", "b2"), New System.Data.Common.DataColumnMapping("b3", "b3"), New System.Data.Common.DataColumnMapping("b4", "b4"), New System.Data.Common.DataColumnMapping("b5", "b5"), New System.Data.Common.DataColumnMapping("b6", "b6"), New System.Data.Common.DataColumnMapping("b7", "b7"), New System.Data.Common.DataColumnMapping("b8", "b8")})})
        '
        'OleDbSelectCommand2
        '
        Me.OleDbSelectCommand2.CommandText = "SELECT XVariable AS [Indep Var], b1, b2, b3, b4, b5, b6, b7, b8 FROM AnalFunc WHE" &
        "RE ([Function Name] = ?)"
        Me.OleDbSelectCommand2.Connection = Me.OleDbConnection1
        Me.OleDbSelectCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Function_Name", System.Data.OleDb.OleDbType.VarWChar, 80, "Function Name"))
        '
        'DsIndepVar1
        '
        Me.DsIndepVar1.DataSetName = "dsIndepVar"
        Me.DsIndepVar1.Locale = New System.Globalization.CultureInfo("en-US")
        CType(Me.DsAnalFunc1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsIndepVar1, System.ComponentModel.ISupportInitialize).EndInit()








    End Sub
#End Region




    Private Sub RefreshDisplay()

        'jam 2023/03/05
        If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then
            LoadInitAnalFunc()
        Else

            DsAnalFunc1.Clear()
            OleDbDAAnalFunc.Fill(DsAnalFunc1)
            dbgAnalFun.DataBind()
        End If

    End Sub

    Private Sub RefreshDisplayDet()

        'jam 2023/03/05

        If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then

            RefreshIndVar(lblFunName.Text)
        Else
            OleDbDAIndepVar.SelectCommand.Parameters(0).Value = lblFunName.Text
            DsIndepVar1.Clear()
            OleDbDAIndepVar.Fill(DsIndepVar1)
            dbgIndepVar.DataBind()
        End If


    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'jam 2023/03/05
        dbgIndepVar.Visible = False
        dbgAnalFun.Visible = False

        'jam 2023/02/26
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                vConnString = "DataSource=" + Session("PutFdrvRun") & "AnalFunc.db;"
                vSQLiteCon = New SqliteConnection(vConnString)
                vSQLiteCon.Open()
                'jrs 2023/08/01
                vConnString = "DataSource=" + Session("PutFdrvRun") & "Working.run.db;"
                vSQLiteConWork = New SqliteConnection(vConnString)
                vSQLiteConWork.Open()
            End If
        End If




        If Not Page.IsPostBack Then
            'jam 2023/02/26
            'RefreshDisplay()
            If Not Session("mnuSQLite") Is Nothing Then
                If Session("mnuSQLite") Then

                    LoadInitAnalFunc()
                Else

                    RefreshDisplay()

                End If
            Else
                RefreshDisplay()
            End If


        End If
        'jrs 2013/09/03
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
    ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)
        'jrs 2013/11/22
        txtFilter.Attributes.Add("onkeypress", "return clickButton(event,'" + chkOrdByTime.ClientID + "')")
        'jrs 2014/04/14
        txtIndepVar.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdContinue.ClientID + "')")
        txta.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdContinue.ClientID + "')")
    End Sub


    Private Sub dbgAnalFun_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dbgAnalFun.ItemCommand

        lblFunName.Text = e.Item.Cells(2).Text
        txta.Text = e.Item.Cells(1).Text
        txta.Enabled = (e.CommandName = "a")

        If txta.Enabled Then
            lblInstructions.Visible = True
        Else
            Dim sUpd As String
            'jrs 2023/08/24 Dim connAnal As OleDb.OleDbConnection
            Dim connAnal As SqliteConnection
            connAnal = CreateConnectionSQLite(Session("PutFdrvRun") & "AnalFunc.db")
            connAnal.Open()
            sUpd = "UPDATE [AnalFunc] SET b1 = 0 WHERE [Function Name] = """
            sUpd = sUpd + lblFunName.Text + """ AND b1 is Null"
            ExecuteSql(connAnal, sUpd)
            sUpd = "UPDATE [AnalFunc] SET b2 = 0 WHERE [Function Name] = """
            sUpd = sUpd + lblFunName.Text + """ AND b2 is Null"
            ExecuteSql(connAnal, sUpd)
            sUpd = "UPDATE [AnalFunc] SET b3 = 0 WHERE [Function Name] = """
            sUpd = sUpd + lblFunName.Text + """ AND b3 is Null"
            ExecuteSql(connAnal, sUpd)
            sUpd = "UPDATE [AnalFunc] SET b4 = 0 WHERE [Function Name] = """
            sUpd = sUpd + lblFunName.Text + """ AND b4 is Null"
            ExecuteSql(connAnal, sUpd)
            sUpd = "UPDATE [AnalFunc] SET b5 = 0 WHERE [Function Name] = """
            sUpd = sUpd + lblFunName.Text + """ AND b5 is Null"
            ExecuteSql(connAnal, sUpd)
            sUpd = "UPDATE [AnalFunc] SET b6 = 0 WHERE [Function Name] = """
            sUpd = sUpd + lblFunName.Text + """ AND b6 is Null"
            ExecuteSql(connAnal, sUpd)
            sUpd = "UPDATE [AnalFunc] SET b7 = 0 WHERE [Function Name] = """
            sUpd = sUpd + lblFunName.Text + """ AND b7 is Null"
            ExecuteSql(connAnal, sUpd)
            sUpd = "UPDATE [AnalFunc] SET b8 = 0 WHERE [Function Name] = """
            sUpd = sUpd + lblFunName.Text + """ AND b8 is Null"
            ExecuteSql(connAnal, sUpd)
            connAnal.Close()
            RefreshDisplayDet()
            dbgIndepVar.Visible = True
        End If
        lblFunName.Visible = True
        txta.Visible = True
        dbgAnalFun.Visible = False
        dbgAnalFun.SelectedIndex = e.Item.DataSetIndex
        'jrs 2013/11/22
        pnlFilters.Visible = dbgAnalFun.Visible

    End Sub

    Private Sub cmdContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        Dim sUpd As String
        Dim old As Integer
        'jam 2023/03/05
        'If dbgAnalFun.Visible Then
        If GviewAnalFun.Visible Then

            'jam 2023/03/05
            If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then
                vSQLiteCon.Close()
                'jrs 2023/08/01
                vSQLiteConWork.Close()
            End If

            Support.FormManager.Close(Me)
        Else
            'jam 2023/03/05
            'If (txta.Enabled And txta.Visible) Or dbgIndepVar.Visible Then
            If (txta.Enabled And txta.Visible) Or GviewIndepVar.Visible Then
                If txta.Enabled And IsNumeric(txta.Text) Then
                    If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then
                        sUpd = "UPDATE [AnalFunc] SET a = " & txta.Text
                        sUpd = sUpd + " WHERE  [Function Name] = """ & lblFunName.Text & """"
                        vExecuteSQL(sUpd)
                        'jrs 2023/08/01
                        sUpd = "UPDATE [ifs_reg_coeff] SET [Value] = " & txta.Text
                        sUpd = sUpd + " WHERE  [RegressionName] = """ & lblFunName.Text & """ AND [Name]=""a"""
                        vExecuteSQLWork(sUpd)
                        Session("ChangingFunctions") = True

                        old = GviewAnalFun.SelectedIndex
                        RefreshDisplay()
                        GviewAnalFun.SelectedIndex = old

                    Else
                        sUpd = "UPDATE [AnalFunc] SET a = " & txta.Text
                        sUpd = sUpd + " WHERE  [Function Name] = """ & lblFunName.Text & """"
                        updateAccessWith1Command(Session("PutFdrvRun") & "AnalFunc.mdb", sUpd)
                        old = dbgAnalFun.SelectedIndex
                        RefreshDisplay()
                        dbgAnalFun.SelectedIndex = old

                    End If

                End If
                lblFunName.Visible = False
                txta.Visible = False
                'jam 2023/03/05
                'dbgAnalFun.Visible = True
                'dbgIndepVar.Visible = False

                GviewAnalFun.Visible = True
                GviewIndepVar.Visible = False
                lblInstructions.Visible = False
                'jrs 2013/11/22
                pnlFilters.Visible = dbgAnalFun.Visible
            Else
                If IsNumeric(txtIndepVar.Text) Then
                    If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then
                        sUpd = "UPDATE [AnalFunc] SET " & lblb.Text & " = " + txtIndepVar.Text
                        sUpd = sUpd + " WHERE  [Function Name] = """ + lblFunName.Text + """ AND XVariable = """
                        sUpd = sUpd + lblIndepVar.Text + """"
                        vExecuteSQL(sUpd)
                        'jrs 2023/08/01
                        sUpd = "UPDATE [ifs_reg_coeff] SET [Value] = " & txtIndepVar.Text
                        sUpd = sUpd + " WHERE  [RegressionName] = """ & lblFunName.Text & """ AND [Name]=""" & lblb.Text & """ AND [RegressionSeq]=(SELECT Seq FROM [ifs_reg] WHERE [name] = ifs_reg_coeff.RegressionName and InputName = """ & lblIndepVar.Text & """)"
                        vExecuteSQLWork(sUpd)
                        Session("ChangingFunctions") = True

                        old = GviewIndepVar.SelectedIndex
                        RefreshDisplayDet()
                        GviewIndepVar.SelectedIndex = old


                    Else
                        sUpd = "UPDATE [AnalFunc] SET " & lblb.Text & " = " + txtIndepVar.Text
                        sUpd = sUpd + " WHERE  [Function Name] = """ + lblFunName.Text + """ AND XVariable = """
                        sUpd = sUpd + lblIndepVar.Text + """"
                        updateAccessWith1Command(Session("PutFdrvRun") & "AnalFunc.mdb", sUpd)
                        old = dbgIndepVar.SelectedIndex
                        RefreshDisplayDet()
                        dbgIndepVar.SelectedIndex = old
                    End If

                End If
                lblIndepVar.Visible = False
                lblb.Visible = False
                txtIndepVar.Visible = False
                'jam 2023/03/05
                'dbgIndepVar.Visible = True
                GviewIndepVar.Visible = True
            End If
        End If
    End Sub


    Private Sub dbgIndepVar_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dbgIndepVar.ItemCommand
        lblIndepVar.Text = e.Item.Cells(0).Text
        lblb.Text = e.CommandName
        dbgIndepVar.SelectedIndex = e.Item.DataSetIndex
        Select Case e.CommandName
            Case "b1"
                txtIndepVar.Text = e.Item.Cells(1).Text
            Case "b2"
                txtIndepVar.Text = e.Item.Cells(2).Text
            Case "b3"
                txtIndepVar.Text = e.Item.Cells(3).Text
            Case "b4"
                txtIndepVar.Text = e.Item.Cells(4).Text
            Case "b5"
                txtIndepVar.Text = e.Item.Cells(5).Text
            Case "b6"
                txtIndepVar.Text = e.Item.Cells(6).Text
            Case "b7"
                txtIndepVar.Text = e.Item.Cells(7).Text
            Case "b8"
                txtIndepVar.Text = e.Item.Cells(8).Text
        End Select
        dbgIndepVar.Visible = False
        lblIndepVar.Visible = True
        lblb.Visible = True
        txtIndepVar.Visible = True
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
        'jam 2023/03/05
        If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then
            LoadInitAnalFunc()
        Else
            Dim sql As String = "SELECT DISTINCT ChangeDate, [Function Name], round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE FROM AnalFunc"
            If txtFilter.Text > "" Then
                sql = sql & " WHERE [Function Name] LIKE '%" & txtFilter.Text & "%' "
            End If

            Dim sqlWhere As String
            sqlWhere = sFilterSql()
            If sqlWhere > "" Then
                If txtFilter.Text > "" Then
                    sql = sql & " AND " & sqlWhere
                Else
                    sql = sql & " WHERE " & sqlWhere
                End If
            End If

            If chkOrdByTime.Checked Then
                sql = sql & " ORDER BY ChangeDate DESC, [Function Name] ASC"
            Else
                sql = sql & " ORDER BY [Function Name] ASC"
            End If

            Me.OleDbSelectCommand1.CommandText = sql

            RefreshDisplay()

        End If

        'jam 2023/03/05
        'If DsAnalFunc1.AnalFunc.Count = 0 Then
        '    LoadMessage("No Functions found, please change your filter", Session("Message"))
        '    Session("MsgParent") = "frm_AnalFunc.aspx"
        '    Support.FormManager.Show(Me, "frm_Message.aspx")
        'End If
        If GviewAnalFun.Rows.Count() = 0 Then
            LoadMessage("No Functions found, please change your filter", Session("Message"))
            Session("MsgParent") = "frm_AnalFunc.aspx"
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
    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' '
    '' '                 'jam 2023/02/26 Initializing component
    '' '
    '' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub LoadInitAnalFunc() '(sender As Object, e As System.EventArgs) Handles chkOrdByTime.CheckedChanged

        Dim table As New DataTable("AnalFuncParentTable")


        Dim column As DataColumn
        Dim row As DataRow
        Dim vdataSet As DataSet
        Dim vsql2 As String
        Dim sqlWhere As String


        ' Create first column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "function"
        column.Caption = "function"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create second column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "Intercept"
        column.AutoIncrement = False
        column.Caption = "Intercept"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create second column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "R2"
        column.AutoIncrement = False
        column.Caption = "R2"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create third column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "StandardError"
        column.AutoIncrement = False
        column.Caption = "StandardError"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create fourth column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "functionNoVis"
        column.AutoIncrement = False
        column.Caption = "functionNoVis"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)




        '' Make the ID column the primary key column.
        'Dim PrimaryKeyColumns(0) As DataColumn
        'PrimaryKeyColumns(0) = table.Columns("id")
        'table.PrimaryKey = PrimaryKeyColumns

        ' Instantiate the DataSet variable.
        vdataSet = New DataSet()

        ' Add the new DataTable to the DataSet.
        vdataSet.Tables.Add(table)



        vsql2 = "SELECT DISTINCT ChangeDate, [Function Name] as functionName, round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE FROM AnalFunc "
        If txtFilter.Text > "" Then
            vsql2 = vsql2 & " WHERE [Function Name] LIKE '%" & txtFilter.Text & "%' "
        End If


        sqlWhere = sFilterSql()
        If sqlWhere > "" Then
            If txtFilter.Text > "" Then
                vsql2 = vsql2 & " AND " & sqlWhere
            Else
                vsql2 = vsql2 & " WHERE " & sqlWhere
            End If
        End If

        If chkOrdByTime.Checked Then
            vsql2 = vsql2 & " ORDER BY ChangeDate DESC, [Function Name] ASC"
        Else
            vsql2 = vsql2 & " ORDER BY [Function Name] ASC"
        End If






        vSQLiteCommand = vSQLiteCon.CreateCommand()
        'vSQLiteCommand.CommandText = "SELECT DISTINCT ChangeDate, [Function Name] as functionName, round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE FROM AnalFunc "
        vSQLiteCommand.CommandText = vsql2
        vSQLiteReader = vSQLiteCommand.ExecuteReader()




        While (vSQLiteReader.Read())



            row = table.NewRow
            row("function") = vSQLiteReader("functionName")
            row("Intercept") = vSQLiteReader("a4")
            row("R2") = vSQLiteReader("R2")
            row("StandardError") = vSQLiteReader("SE")
            row("functionNoVis") = vSQLiteReader("functionName")
            table.Rows.Add(row)


        End While

        'Dim a As Integer
        'a = table.Rows.Count

        'table.AcceptChanges()


        'dbgAnalFun.DataSource = table

        GviewAnalFun.Visible = True

        ' GviewAnalFun.DataSource = vbEmpty


        GviewAnalFun.DataSource = table

        GviewAnalFun.DataBind()



        'dbgAnalFun.DataBind()




    End Sub

    Private Sub GviewAnalFun_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GviewAnalFun.RowCommand

        Dim sUpd As String
        Dim vindex As Integer
        Dim vrow As GridViewRow
        Dim vitem As New ListItem()
        Dim vitem2 As New ListItem()
        Dim vitem3 As New ListItem()
        Dim vindex2 As Integer
        Dim vfunName As String
        Dim selectedRow As GridViewRow
        Dim vfuncell As TableCell
        Dim myfunction As String
        Dim TB1 As TextBox
        Dim sFunction As String
        Dim sIntercept As String

        On Error GoTo GViewFunError


        sFunction = ""
        sIntercept = ""

        If e.CommandName = "FunName" Or e.CommandName = "a" Then



            vindex2 = CInt(e.CommandArgument)
            selectedRow = GviewAnalFun.Rows(vindex2)

            vfuncell = selectedRow.Cells(1)
            vfunName = vfuncell.Text



            'TB1 = GviewAnalFun.Rows(vindex2).FindControl("FunName")
            'myfunction = TB1.Text

            'function name
            Dim lbtnFunction As LinkButton = TryCast(GviewAnalFun.Rows(e.CommandArgument).Cells(0).Controls(0), LinkButton)

            sFunction = lbtnFunction.Text

            'intercept
            Dim lbtnIntercept As LinkButton = TryCast(GviewAnalFun.Rows(e.CommandArgument).Cells(1).Controls(0), LinkButton)

            sIntercept = lbtnIntercept.Text

        End If



        ' If e.CommandName = "FunName" Then
        vindex = CInt(e.CommandArgument)
        vrow = GviewAnalFun.Rows(vindex)
        'End If

        vitem.Text = Server.HtmlDecode(vrow.Cells(2).Text)

        vitem2.Text = Server.HtmlDecode(vrow.Cells(3).Text)

        vitem3.Text = Server.HtmlDecode(vrow.Cells(4).Text)

        'jam 2023/04/03
        'lblFunName.Text = vitem.Text
        'txta.Text = vitem2.Text

        lblFunName.Text = sFunction
        txta.Text = sIntercept



        'lblFunName.Text = e.row.Cells(2).Text
        'txta.Text = e.Item.Cells(1).Text
        txta.Enabled = (e.CommandName = "a")

        'jam 2023/03/05
        If txta.Enabled Then
            lblInstructions.Visible = True
        Else

            sUpd = "UPDATE [AnalFunc] SET b1 = 0 WHERE [Function Name] = """
            sUpd = sUpd + sFunction + """ AND b1 is Null"
            vExecuteSQL(sUpd)
            sUpd = "UPDATE [AnalFunc] SET b2 = 0 WHERE [Function Name] = """
            sUpd = sUpd + sFunction + """ AND b2 is Null"
            vExecuteSQL(sUpd)
            sUpd = "UPDATE [AnalFunc] SET b3 = 0 WHERE [Function Name] = """
            sUpd = sUpd + sFunction + """ AND b3 is Null"
            vExecuteSQL(sUpd)
            sUpd = "UPDATE [AnalFunc] SET b4 = 0 WHERE [Function Name] = """
            sUpd = sUpd + sFunction + """ AND b4 is Null"
            vExecuteSQL(sUpd)
            sUpd = "UPDATE [AnalFunc] SET b5 = 0 WHERE [Function Name] = """
            sUpd = sUpd + sFunction + """ AND b5 is Null"
            vExecuteSQL(sUpd)
            sUpd = "UPDATE [AnalFunc] SET b6 = 0 WHERE [Function Name] = """
            sUpd = sUpd + sFunction + """ AND b6 is Null"
            vExecuteSQL(sUpd)
            sUpd = "UPDATE [AnalFunc] SET b7 = 0 WHERE [Function Name] = """
            sUpd = sUpd + sFunction + """ AND b7 is Null"
            vExecuteSQL(sUpd)
            sUpd = "UPDATE [AnalFunc] SET b8 = 0 WHERE [Function Name] = """
            sUpd = sUpd + sFunction + """ AND b8 is Null"
            vExecuteSQL(sUpd)
            'connAnal.Close()
            RefreshDisplayDet()
            'jam 2023/04/04 
            GviewIndepVar.Visible = True


        End If
        lblFunName.Visible = True
        txta.Visible = True
        GviewAnalFun.Visible = False
        GviewAnalFun.SelectedIndex = vindex


        pnlFilters.Visible = GviewAnalFun.Visible

        Exit Sub


GViewFunError:
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        Session("Message") = ""
        LoadMessage("Error updating data values", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")



    End Sub

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' '
    '' '          JAM 2023/03/05  Procedure to Execute SQL
    '' '
    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub vExecuteSQL(vsql As String)
        vSQLiteCommand = vSQLiteCon.CreateCommand()
        'vSQLiteCommand.CommandText = "SELECT DISTINCT ChangeDate, [Function Name] as functionName, round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE FROM AnalFunc "
        vSQLiteCommand.CommandText = vsql
        vSQLiteCommand.ExecuteNonQuery()
    End Sub

    'jrs 2023/08/01
    Protected Sub vExecuteSQLWork(vsql As String)
        vSQLiteCommand = vSQLiteConWork.CreateCommand()
        'vSQLiteCommand.CommandText = "SELECT DISTINCT ChangeDate, [Function Name] as functionName, round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE FROM AnalFunc "
        vSQLiteCommand.CommandText = vsql
        vSQLiteCommand.ExecuteNonQuery()
    End Sub

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' '
    '' ' 
    '' '             JAM 2023/03/05 Refreshing Independent Variables
    '' '
    '' '
    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub RefreshIndVar(vfunction As String) '(sender As Object, e As System.EventArgs) Handles chkOrdByTime.CheckedChanged

        Dim table As New DataTable("IndVarTable")


        Dim column As DataColumn
        Dim row As DataRow
        Dim vdataSet As DataSet
        Dim vsql2 As String


        ' Create b01 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "XVariable"
        column.Caption = "XVAriable"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create b1 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "b1"
        column.Caption = "b1"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create b2 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "b2"
        column.Caption = "b2"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create b3 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "b3"
        column.Caption = "b3"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create b4 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "b4"
        column.Caption = "b4"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the column to the table.
        table.Columns.Add(column)

        ' Create b5 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "b5"
        column.Caption = "b5"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the column to the table.
        table.Columns.Add(column)

        ' Create b6 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "b6"
        column.Caption = "b6"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create b7 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "b7"
        column.Caption = "b7"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the column to the table.
        table.Columns.Add(column)

        ' Create b8 column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Double")
        column.ColumnName = "b8"
        column.Caption = "b8"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False


        ' Add the column to the table.
        table.Columns.Add(column)

        ' Instantiate the DataSet variable.
        vdataSet = New DataSet()

        ' Add the new DataTable to the DataSet.
        vdataSet.Tables.Add(table)



        'jrs 2025/06/25 shouldn't be using LIKE here should be = vsql2 = "SELECT DISTINCT ChangeDate, [Function Name] as functionName, round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE,XVariable,b1,b2,b3,b4,b5,b6,b7,b8 FROM AnalFunc WHERE [Function Name] LIKE '%" & vfunction & "%' "
        vsql2 = "SELECT DISTINCT ChangeDate, [Function Name] as functionName, round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE,XVariable,b1,b2,b3,b4,b5,b6,b7,b8 FROM AnalFunc WHERE [Function Name] = '" & vfunction & "' "


        vSQLiteCommand = vSQLiteCon.CreateCommand()
        'vSQLiteCommand.CommandText = "SELECT DISTINCT ChangeDate, [Function Name] as functionName, round(a, 4) as a4, round(RSquared, 4) as R2, round(StandardError, 4) as SE FROM AnalFunc "
        vSQLiteCommand.CommandText = vsql2
        vSQLiteReader = vSQLiteCommand.ExecuteReader()


        While (vSQLiteReader.Read())



            row = table.NewRow
            row("XVariable") = vSQLiteReader("XVariable")
            row("b1") = vSQLiteReader("b1")
            row("b2") = vSQLiteReader("b2")
            row("b3") = vSQLiteReader("b3")
            row("b4") = vSQLiteReader("b4")
            row("b5") = vSQLiteReader("b5")
            row("b6") = vSQLiteReader("b6")
            row("b7") = vSQLiteReader("b7")
            row("b8") = vSQLiteReader("b8")

            table.Rows.Add(row)


        End While

        GviewIndepVar.Visible = True


        GviewIndepVar.DataSource = table

        GviewIndepVar.DataBind()




    End Sub

    Private Sub GviewIndepVar_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GviewIndepVar.RowCommand

        Dim vindex As Integer
        Dim vrow As GridViewRow
        Dim vitem As New ListItem()
        Dim vitem2 As New ListItem()


        Dim vfuncell As TableCell
        Dim myfunction As String
        Dim TB1 As TextBox
        Dim sB1 As String
        Dim sB2 As String
        Dim sB3 As String
        Dim sB4 As String
        Dim sB5 As String
        Dim sB6 As String
        Dim sB7 As String
        Dim sB8 As String

        sB1 = ""
        sB2 = ""
        sB3 = ""
        sB4 = ""
        sB5 = ""
        sB6 = ""
        sB7 = ""
        sB8 = ""

        ' If e.CommandName = "FunName" Then
        vindex = CInt(e.CommandArgument)
        vrow = GviewIndepVar.Rows(vindex)
        'End If

        vitem.Text = Server.HtmlDecode(vrow.Cells(2).Text)

        vitem2.Text = Server.HtmlDecode(vrow.Cells(3).Text)

        'jam 2023/03/05
        'lblIndepVar.Text = e.Item.Cells(0).Text
        lblIndepVar.Text = Server.HtmlDecode(vrow.Cells(0).Text)
        lblb.Text = e.CommandName


        'jam 2023/04/05
        'B1

        If e.CommandName = "b1" Then

            Dim lbtnB1 As LinkButton = TryCast(GviewIndepVar.Rows(e.CommandArgument).Cells(9).Controls(0), LinkButton)
            sB1 = lbtnB1.Text

        End If

        'B2
        If e.CommandName = "b2" Then
            Dim lbtnB2 As LinkButton = TryCast(GviewIndepVar.Rows(e.CommandArgument).Cells(10).Controls(0), LinkButton)
            sB2 = lbtnB2.Text
        End If
        'B3
        If e.CommandName = "b3" Then
            Dim lbtnB3 As LinkButton = TryCast(GviewIndepVar.Rows(e.CommandArgument).Cells(11).Controls(0), LinkButton)
            sB3 = lbtnB3.Text
        End If
        'B4
        If e.CommandName = "b4" Then
            Dim lbtnB4 As LinkButton = TryCast(GviewIndepVar.Rows(e.CommandArgument).Cells(12).Controls(0), LinkButton)
            sB4 = lbtnB4.Text
        End If

        'B5
        If e.CommandName = "b5" Then
            Dim lbtnB5 As LinkButton = TryCast(GviewIndepVar.Rows(e.CommandArgument).Cells(13).Controls(0), LinkButton)
            sB5 = lbtnB5.Text
        End If


        'B6
        If e.CommandName = "b6" Then
            Dim lbtnB6 As LinkButton = TryCast(GviewIndepVar.Rows(e.CommandArgument).Cells(14).Controls(0), LinkButton)
            sB6 = lbtnB6.Text
        End If


        'B7
        If e.CommandName = "b7" Then
            Dim lbtnB7 As LinkButton = TryCast(GviewIndepVar.Rows(e.CommandArgument).Cells(15).Controls(0), LinkButton)
            sB7 = lbtnB7.Text
        End If

        'B8
        If e.CommandName = "b8" Then
            Dim lbtnB8 As LinkButton = TryCast(GviewIndepVar.Rows(e.CommandArgument).Cells(16).Controls(0), LinkButton)
            sB8 = lbtnB8.Text
        End If


        ' If e.CommandName = "FunName" Then
        vindex = CInt(e.CommandArgument)
        vrow = GviewIndepVar.Rows(vindex)
        'End If

        vitem.Text = Server.HtmlDecode(vrow.Cells(2).Text)

        vitem2.Text = Server.HtmlDecode(vrow.Cells(3).Text)

        'jam 2023/03/05
        'lblIndepVar.Text = e.Item.Cells(0).Text
        lblIndepVar.Text = Server.HtmlDecode(vrow.Cells(0).Text)
        lblb.Text = e.CommandName


        'jam 2023/03/05
        GviewIndepVar.SelectedIndex = vindex
        Select Case e.CommandName
            Case "b1"
                txtIndepVar.Text = sB1' Server.HtmlDecode(vrow.Cells(1).Text)
            Case "b2"
                txtIndepVar.Text = sB2' Server.HtmlDecode(vrow.Cells(2).Text)
            Case "b3"
                txtIndepVar.Text = sB3' Server.HtmlDecode(vrow.Cells(3).Text)
            Case "b4"
                txtIndepVar.Text = sB4' Server.HtmlDecode(vrow.Cells(4).Text)
            Case "b5"
                txtIndepVar.Text = sB5' Server.HtmlDecode(vrow.Cells(5).Text)
            Case "b6"
                txtIndepVar.Text = sB6' Server.HtmlDecode(vrow.Cells(6).Text)
            Case "b7"
                txtIndepVar.Text = sB7' Server.HtmlDecode(vrow.Cells(7).Text)
            Case "b8"
                txtIndepVar.Text = sB8 ' Server.HtmlDecode(vrow.Cells(8).Text)
        End Select
        GviewIndepVar.Visible = False
        lblIndepVar.Visible = True
        lblb.Visible = True
        txtIndepVar.Visible = True


    End Sub

End Class

