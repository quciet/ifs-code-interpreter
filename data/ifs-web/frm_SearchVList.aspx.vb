'jrs 2023/01/04
Imports System.Data.SQLite

Partial Class frm_SearchVList
    Inherits Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'jrs 2006/03/23 not using Load State (Artinsoft)
        If Support.FormManager.LoadState Then
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

        Dim x As Object
        x = Split(txtSearch.Text, " ")
        Dim i As Integer, j As Integer, pos As Integer
        Dim rr As DataRow
        Dim bOpen As Boolean, bClose As Boolean, bQuot As Boolean
        Dim Target() As String
        ReDim Target(0)

        j = 0
        'Create Target Array
        For i = 0 To UBound(x)
            bQuot = (InStr(x(i), """") > 0)
            If bQuot Then
                If bOpen Then
                    bClose = True
                Else
                    bOpen = True
                End If
            End If

            If Not bOpen Then
                ReDim Preserve Target(j)
                Target(j) = x(i)
                j = j + 1
            Else
                If j > UBound(Target) Then
                    ReDim Preserve Target(j)
                End If
                Target(j) = Target(j) & " " & x(i)
                If bClose Then
                    Target(j) = Replace(LTrim(Target(j)), """", "")
                    j = j + 1
                    bClose = False
                    bOpen = False
                End If
            End If
        Next i

        Dim bFound As Boolean
        Dim idx As Integer

        'jrs 2023/01/04
        Dim strSQL As String
        Dim vrlDS As DataSet
        Dim varLstRel As DataRelation
        Dim forLstRel As DataRelation
        Dim hisvarLstRel As DataRelation
        Dim hisvarDDRel As DataRelation
        Dim hisforLstRel As DataRelation
        Dim hisforDDRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow, ccRow, dRow, ddRow As DataRow

        Dim sql As String, sql2 As String
        sql = "SELECT DISTINCT L.ListName FROM VarLists as L WHERE L.Type = 'V'"

        Dim Conn As OleDb.OleDbConnection
        Dim IfsConn As OleDb.OleDbConnection

        Dim ConnS As SqliteConnection
        Dim IFsConnS As SqliteConnection

        If UseOnlySQLite Then
            If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                sql2 = " AND HistoryplusForecast = 0"
            Else
                sql2 = " AND HisCompEmpFor = 0"
            End If
            sql = sql & sql2

            ConnS = New SQLiteConnection(CType("DataSource=" & Session("PutFdrvRun") & "datadict.db;", String))
            ConnS.Open()

            IFsConnS = New SQLiteConnection(CType("DataSource=" & Session("PutFdrvRun") & "IFSVAR.db;", String))
            IFsConnS.Open()

            Dim rdr As SqliteDataReader
            Dim myCMD As SqliteCommand = IFsConnS.CreateCommand
            Dim myCMD2 As SqliteCommand = ConnS.CreateCommand

            vrlDS = New DataSet
            vrlDS.EnforceConstraints = False

            myCMD.CommandText = sql
            Dim lstDA As SqliteDataReader = myCMD.ExecuteReader()
            Dim rs As DataTable = New DataTable
            rs.TableName = "Lists"
            vrlDS.Tables.Add(rs)
            rs.Load(lstDA)
            lstDA.Close()

            'jrs 2026/05/21 added ListName to the ORDER BY
            sql = "SELECT * FROM VarLists WHERE History = 0 AND Formula = 0 AND Type = 'V' " & sql2 & " ORDER BY ListName, ListOrder"
            myCMD.CommandText = sql
            Dim vrsDA As SqliteDataReader = myCMD.ExecuteReader()
            Dim rs2 As DataTable = New DataTable
            rs2.TableName = "Vars"
            vrlDS.Tables.Add(rs2)
            rs2.Load(vrsDA)
            vrsDA.Close()

            'jrs 2026/05/21 added ListName to the ORDER BY
            sql = "SELECT * FROM VarLists as v, VarListsFormulas as f " &
            "WHERE v.FormulaId = f.FormulaId AND v.History = 0 AND v.Formula = 1 AND Type = 'V' " & sql2 & " ORDER BY v.ListName, v.ListOrder"
            myCMD.CommandText = sql
            Dim frmDA As SqliteDataReader = myCMD.ExecuteReader()
            Dim rs3 As DataTable = New DataTable
            rs3.TableName = "Formulas"
            vrlDS.Tables.Add(rs3)
            rs3.Load(frmDA)
            frmDA.Close()

            'jrs 2026/05/21 added ListName to the ORDER BY
            sql = "SELECT * FROM VarLists WHERE History = 1 AND Formula = 0 AND Type = 'V' " & sql2 & " ORDER BY ListName, ListOrder"
            myCMD.CommandText = sql
            Dim hisvrsDA As SqliteDataReader = myCMD.ExecuteReader()
            Dim rs4 As DataTable = New DataTable
            rs4.TableName = "HisVars"
            vrlDS.Tables.Add(rs4)
            rs4.Load(hisvrsDA)
            hisvrsDA.Close()

            sql = "select * from datadict"
            myCMD2.CommandText = sql
            Dim hisvrsDDDA As SqliteDataReader = myCMD2.ExecuteReader()
            Dim rs5 As DataTable = New DataTable
            rs5.TableName = "HisVarsDD"
            vrlDS.Tables.Add(rs5)
            rs5.Load(hisvrsDDDA)
            hisvrsDDDA.Close()

            'jrs 2026/05/21 added ListName to the ORDER BY
            sql = "SELECT SUBSTR(f.VarName, 1, INSTR(f.VarName, '[-1]')-1) as hisvarname, * FROM VarLists as v, VarListsFormulas as f " &
                            "WHERE v.FormulaId = f.FormulaId AND v.History = 1 AND v.Formula = 1 AND Type = 'V' " & sql2 & " ORDER BY v.ListName, v.ListOrder"
            myCMD.CommandText = sql
            Dim hisfrmDA As SqliteDataReader = myCMD.ExecuteReader()
            Dim rs6 As DataTable = New DataTable
            rs6.TableName = "HisFormulas"
            vrlDS.Tables.Add(rs6)
            rs6.Load(hisfrmDA)
            hisfrmDA.Close()
        Else
            If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                sql2 = " AND HistoryplusForecast = False"
            Else
                sql2 = " AND HisCompEmpFor = False"
            End If
            sql = sql & sql2

            Conn = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & "datadict.MDB")
            Conn.Open()
            IfsConn = ADO_OleDB_CreateConnection(Session("PutFdrvRun") + "IFSVAR.mdb")
            Dim rdr As OleDb.OleDbDataReader
            Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
            myCMD.Connection = Conn

            IfsConn.Open()

            Dim lstDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, IfsConn)
            sql = "SELECT * FROM VarLists WHERE History = False AND Formula = False AND Type = 'V' " & sql2 & " ORDER BY ListOrder"
            Dim vrsDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, IfsConn)
            sql = "SELECT * FROM VarLists as v, VarListsFormulas as f " &
        "WHERE v.FormulaId = f.FormulaId AND v.History = False AND v.Formula = True AND Type = 'V' " & sql2 & " ORDER BY v.ListOrder"
            Dim frmDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, IfsConn)
            sql = "SELECT * FROM VarLists WHERE History = True AND Formula = False AND Type = 'V' " & sql2 & " ORDER BY ListOrder"
            Dim hisvrsDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, IfsConn)
            Dim hisvrsDDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("select * from datadict", Conn)
            sql = "SELECT mid(f.VarName, 1, instr(f.VarName, '[-1]')-1) as hisvarname, * FROM VarLists as v, VarListsFormulas as f " &
                            "WHERE v.FormulaId = f.FormulaId AND v.History = True AND v.Formula = True AND Type = 'V' " & sql2 & " ORDER BY v.ListOrder"
            Dim hisfrmDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, IfsConn)

            vrlDS = New DataSet
            lstDA.Fill(vrlDS, "Lists")
            vrsDA.Fill(vrlDS, "Vars")
            frmDA.Fill(vrlDS, "Formulas")
            hisvrsDA.Fill(vrlDS, "HisVars")
            hisvrsDDDA.Fill(vrlDS, "HisVarsDD")
            hisfrmDA.Fill(vrlDS, "HisFormulas")
        End If

        varLstRel = vrlDS.Relations.Add("VarLists",
                                             vrlDS.Tables("Lists").Columns("ListName"),
                                             vrlDS.Tables("Vars").Columns("ListName"))
        forLstRel = vrlDS.Relations.Add("ForLists",
                                             vrlDS.Tables("Lists").Columns("ListName"),
                                             vrlDS.Tables("Formulas").Columns("ListName"))
        hisvarLstRel = vrlDS.Relations.Add("HisVarLists",
                                             vrlDS.Tables("Lists").Columns("ListName"),
                                             vrlDS.Tables("HisVars").Columns("ListName"))
        hisvarDDRel = vrlDS.Relations.Add("HisVarDD",
                                             vrlDS.Tables("HisVarsDD").Columns("Variable"),
                                             vrlDS.Tables("HisVars").Columns("VarName"))
        hisforLstRel = vrlDS.Relations.Add("HisForLists",
                                             vrlDS.Tables("Lists").Columns("ListName"),
                                             vrlDS.Tables("HisFormulas").Columns("ListName"))
        hisforDDRel = vrlDS.Relations.Add("ForHisVarDD",
                                             vrlDS.Tables("HisVarsDD").Columns("Variable"),
                                             vrlDS.Tables("HisFormulas").Columns("hisvarname"))


        'Check all lists
        Dim sList As String
        Dim valtbl As DataTable

        'jrs 2026/05/21 added sorting by ListName to make sure the order is the same as in the display of lists
        Dim dv As New DataView(vrlDS.Tables("Lists"))
        dv.Sort = "ListName ASC"

        For Each drv As DataRowView In dv
            pppRow = drv.Row

            bFound = False
            sList = pppRow.Item("ListName")
            'If sList = "Poverty(LT $1.25/Day, Percent CS) Historic Forecast and Empirical" Then Stop
            'Check List Name
            bFound = bSearchInVarIdx(Target, -1, "S", Nothing, sList)
            'if all words were found, then set it to true, otherwise too many matches
            If bFound Then
                lstSearch.Items.Add(sList)
                lstVarIdx.Items.Add(i)
            Else 'if not keep loking in Variable Name, Definition and Explanation
                'Look in Forecast Variables
                ' Now your relationship works
                For Each ppRow In pppRow.GetChildRows(varLstRel)
                    ' process child rows
                    idx = IfsVars.Item(ppRow.Item("VarName")).Index
                    If idx > 0 Then
                        bFound = bSearchInVarIdx(Target, idx, "F", Nothing, "")
                    End If
                    If bFound Then
                        Exit For
                    End If
                Next

                If bFound Then
                    lstSearch.Items.Add(sList)
                    lstVarIdx.Items.Add(i)
                Else
                    'Look in Formulas with Forecast Variables
                    j = 0
                    For Each pRow In pppRow.GetChildRows(forLstRel)
                        If j = 0 Then
                            'first check in the formula name
                            bFound = bSearchInVarIdx(Target, -1, "S", Nothing, pRow.Item("FormulaName"))
                            j = 1
                        End If
                        If bFound Then
                            Exit For
                        End If
                        'jrs 2023/01/05
                        If UseOnlySQLite Then
                            pos = InStr(pRow.Item("VarName"), "[")
                        Else
                            pos = InStr(pRow.Item("f.VarName"), "[")
                        End If
                        If pos = 0 Then
                            'jrs 2023/01/05
                            If UseOnlySQLite Then
                                pos = Len(pRow.Item("VarName"))
                            Else
                                pos = Len(pRow.Item("f.VarName"))
                            End If
                        Else
                            pos = pos - 1
                        End If
                        'jrs 2023/01/05
                        If UseOnlySQLite Then
                            idx = IfsVars.Item(pRow.Item("VarName").ToString.Substring(0, pos)).Index
                        Else
                            idx = IfsVars.Item(pRow.Item("f.VarName").ToString.Substring(0, pos)).Index
                        End If
                        If idx > 0 Then
                            bFound = bSearchInVarIdx(Target, idx, "F", Nothing, "")
                        End If
                    Next pRow

                    If bFound Then
                        lstSearch.Items.Add(sList)
                        lstVarIdx.Items.Add(i)
                    Else
                        'Look in Historic Variables
                        For Each cRow In pppRow.GetChildRows(hisvarLstRel)
                            ccRow = cRow.GetParentRow(hisvarDDRel)
                            'If ccRow Is Nothing Then Stop
                            'If cRow(1) = "IncBelow1Dollar%" Then Stop
                            bFound = bSearchInVarIdx(Target, -1, "H", ccRow, "")
                            If bFound Then
                                Exit For
                            End If
                        Next cRow

                        If bFound Then
                            lstSearch.Items.Add(sList)
                            lstVarIdx.Items.Add(i)
                        Else
                            'Look in Formulas with Historic Variables
                            j = 0
                            For Each dRow In pppRow.GetChildRows(hisforLstRel)
                                If j = 0 Then
                                    'first check in the formula name
                                    bFound = bSearchInVarIdx(Target, -1, "S", Nothing, dRow.Item("FormulaName"))
                                    j = 1
                                End If
                                ddRow = dRow.GetParentRow(hisforDDRel)
                                'If ddRow Is Nothing Then Stop
                                bFound = bSearchInVarIdx(Target, -1, "H", ddRow, "")
                                If bFound Then
                                    Exit For
                                End If
                            Next dRow

                            If bFound Then
                                lstSearch.Items.Add(sList)
                                lstVarIdx.Items.Add(i)
                            End If 'not found in historic formulas
                        End If 'not found in historic variables
                    End If 'not found in forecast formulas
                End If 'not found in forecast formulas
            End If 'not found in variable list name
        Next

        'For i = 0 To rsL.Rows.Count - 1

        If UseOnlySQLite Then
            IFsConnS.Close()
            ConnS.Close()
        Else
            IfsConn.Close()
            Conn.Close()
        End If

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
        'jrs 2013/07/19 Support.FormManager.Close(Me)
        Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
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

        'jrs 2013/07/19
        Session.Remove("PDFlexCategoriesIdxs")

        Session.Remove("PDFlexPacksIdxs")
        Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx?ListNames=" & lstSearch.Items(lstSearch.SelectedIndex).Text)

    End Sub

    'jrs 2008/05/20
    'Function created to search alternatively in all words in the search string (OR like)
    'Considering only the entire search string when using double quotation marks (AND like)
    Private Function bSearchInVarIdx(ByRef Target As Object, ByVal idx As Integer,
    ByVal sSearchType As String, ByVal valtbl As Object, ByVal SCHECK As String) As Boolean
        'Target is the array of strings to search
        'sSearchType: F - Forecast variable, using index, H - Historic Variable, using table, S - string
        'bHistory indicates if looking on emprical variables
        'valtbl contains empirical variable data
        'sCheck is the string to check when using type S

        bSearchInVarIdx = False
        'Search for each target
        Dim k As Integer
        Dim iSearchCount As Integer
        iSearchCount = 0

        If sSearchType = "F" Then
            For k = 0 To UBound(Target)
                If InStr(UCase(V(idx)), UCase(Target(k))) > 0 Or
                InStr(UCase(VDef(idx)), UCase(Target(k))) > 0 Or
                InStr(UCase(VExpl(idx)), UCase(Target(k))) > 0 Then
                    iSearchCount = iSearchCount + 1
                End If
            Next k
        ElseIf sSearchType = "H" Then
            For k = 0 To UBound(Target)
                If InStr(UCase(valtbl.Item("Variable")), UCase(Target(k))) > 0 Or
                InStr(UCase(valtbl.Item("Definition")), UCase(Target(k))) > 0 Then
                    iSearchCount = iSearchCount + 1
                End If
            Next k
        ElseIf sSearchType = "S" Then
            For k = 0 To UBound(Target)
                If InStr(UCase(SCHECK), UCase(Target(k))) > 0 Then
                    iSearchCount = iSearchCount + 1
                End If
            Next k
        End If

        'if all words were found, then set it to true, otherwise too many matches
        If iSearchCount = UBound(Target) + 1 Then
            bSearchInVarIdx = True
        End If

    End Function

End Class
