'jrs 2009/03/13
'Web translation of vb6 version of VarList Explain

Option Strict Off
Option Explicit On
'jam 2023/04/07

Imports System.Data.SQLite
Imports Microsoft.Ajax.Utilities

Partial Class frm_PackagedDisplays_Exp
    Inherits Page

    'jam 2023/04/07
    Dim vSQLiteCon As SQLiteConnection
    Dim vSQLiteCon2 As SQLiteConnection
    Dim vSQLiteCommand As SQLiteCommand
    Dim vSQLiteReader As SQLiteDataReader
    Dim vConnString As String
    Dim vConnString2 As String
    Dim vSQLiteCommand2 As SQLiteCommand
    Dim vSQLiteReader2 As SQLiteDataReader
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'Do not modify it using the code editor.
        InitializeComponent()

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If



        End If
        'jam 2023/04/07
        'jam 2023/04/16
        'dsVarList.DataFile = Session("PutFdrvRun") & "ifsVar.mdb"
        'dsVarList.SelectCommand = Session("VarList_SQL")

        'dsFormulas.DataFile = Session("PutFdrvRun") & "ifsVar.mdb"
        'sqlF = "SELECT CodeVar, VarName, '' as DEFINITION, '' as Historic_Analog, DIM2, DIM3 FROM VarListsFormulas WHERE FormulaId = "
        sqlF = "SELECT CodeVar, VarName, '' as DEFINITION, '' as Historic_Analog, Dim2, Dim3 FROM VarListsFormulas WHERE FormulaId = "


        'jrs 2013/02/04
        'dsHistAnalogFormulas.DataFile = Session("PutFdrvRun") & "ifsVar.mdb"

        'jrs 2009/03/17
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & Session("VarList_Title")
    End Sub

    Dim sqlF As String

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region




    Private Sub RefreshDisplay()
        grdVarList.DataBind()
        grdVarList.SelectedIndex = 0
        grdVarList_SelectedIndexChanged(Me, New EventArgs)
    End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'jam 2023/04/07
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsVar.db;"
                vSQLiteCon = New SQLiteConnection(vConnString)
                vSQLiteCon.Open()
            End If
        End If


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
        cswmGroup1.ID = "Group_fa05"
        'jrs 2011/05/04
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("ExplainListParent") & "';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Data Information"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show"


        If Not Page.IsPostBack Then

            'jam 2023/04/07
            If Not Session("mnuSQLite") Is Nothing Then
                If Session("mnuSQLite") Then

                    LoadInitVarList()

                End If

            End If


            RefreshDisplay()
        End If
    End Sub


    Protected Sub grdVarList_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVarList.PageIndexChanged
        grdVarList.SelectedIndex = 0
        grdVarList_SelectedIndexChanged(Me, New EventArgs)
    End Sub

    Protected Sub grdVarList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVarList.SelectedIndexChanged

        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = grdVarList.SelectedRow
        Dim rr As Integer
        'jrs 2013/02/13
        Dim bFormulaCheckingHistAnal As Boolean = False

        If CType(row.Cells(4).FindControl("txtFormulaId"), TextBox).Text > "0" Then
            Dim sql As String
            'Index 4 is FormulaId
            'Index 5 is FormulaDetHist
            sql = sqlF & CType(row.Cells(4).FindControl("txtFormulaId"), TextBox).Text
            If row.Cells(5).Text <> "&nbsp;" Then
                sql = sql & " OR FormulaId = " & CStr(CInt(CType(row.Cells(4).FindControl("txtFormulaId"), TextBox).Text) + 1)
            End If

            'jam 2023/04/07
            'dsFormulas.SelectCommand = sql
            'grdFormulas.DataBind()

            LoadInitFormulas(sql)

            Dim sVar As String, iPos As Integer, idx As Integer
            Dim sDef As String
            rr = 0
            'jrs 2011/07/11
            Dim sHistAn As String = ""

            Do While rr < grdFormulas.Rows.Count
                'Cell 1 is VarName
                iPos = InStr(grdFormulas.Rows(rr).Cells(2).Text, "[")
                If iPos > 0 Then
                    sVar = Mid(grdFormulas.Rows(rr).Cells(2).Text, 1, iPos - 1)
                Else
                    sVar = grdFormulas.Rows(rr).Cells(1).Text
                End If
                idx = IfsVars.Item(sVar).Index
                If idx > 0 Then
                    sDef = VDef(idx)
                    'Cell 3 is DEFINITION
                    grdFormulas.Rows(rr).Cells(3).Text = sDef
                    'jrs 2011/07/11
                    sHistAn = IIf(IsDBNull(VHistoricAnalog(idx)), "", VHistoricAnalog(idx))
                    'Cell 4 is Hist Analog
                    grdFormulas.Rows(rr).Cells(4).Text = sHistAn
                Else 'jrs 2022/02/15
                    sDef = ""
                    'Open DataDict
                    If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then
                        vConnString2 = "DataSource=" + Session("PutFdrvRun") & "DataDict.db;"
                        vSQLiteCon2 = New SQLiteConnection(vConnString2)
                        vSQLiteCon2.Open()
                        vSQLiteCommand2 = vSQLiteCon2.CreateCommand()
                        vSQLiteCommand2.CommandText = "SELECT Definition FROM DataDict WHERE Variable = """ & sVar & """"
                        vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()




                        While (vSQLiteReader2.Read())

                            grdFormulas.Rows(rr).Cells(3).Text = vSQLiteReader2("Definition")


                        End While


                        vSQLiteCon2.Close()

                    Else
                        Dim rsDD As DataTable
                        SetDataTablefromAccess(Session("PutFdrvRun") & "DataDict.mdb", "SELECT Definition FROM DataDict WHERE Variable = """ & sVar & """", "", rsDD)

                        'Search for variable
                        If rsDD.Rows.Count > 0 Then
                            sDef = rsDD.Rows(0).Item("Definition")
                            grdFormulas.Rows(rr).Cells(3).Text = sDef
                        End If

                    End If



                End If

                rr = rr + 1
            Loop

            grdFormulas.Visible = True

            grdFormulas.SelectedIndex = 0
            grdFormulas_SelectedIndexChanged(Me, New EventArgs)
            bFormulaCheckingHistAnal = True

        Else
            grdFormulas.Visible = False
        End If

        'jrs 2011/07/11
        'For Regular Variables with Formulas in Historic Analog
        If UCase(row.Cells(6).Text) = "FORMULA" Then
            Dim Sql As String, sqlHAF As String, sqlHAFDet As String
            'jrs 2011/07/11
            'Members of Historic Analog Formula
            sqlHAF = "SELECT 0&Dim2&Dim3 as ListOrder, Dim2 as CodeVar, Dim3 as VarName, Formula FROM IFSVARHISTANAFORMULAS WHERE NAME = """
            sqlHAFDet = "SELECT 0&F.Dim2&F.Dim3&CodeVar as ListOrder, CodeVar, VarName, '' as Formula FROM IFSVARHISTANAFORMULAS F, IFSVARHISTANAFORMULASDET D WHERE D.ForVarName = F.Name AND (F.Dim2 = D.Dim2 OR F.Dim2 is Null) AND (F.Dim3 = D.Dim3 OR F.Dim3 is Null) AND F.Name = """

            'jrs 2013/02/04
            '"VarName" is in cells(2)
            Sql = sqlHAF & CStr(row.Cells(2).Text) & """"
            Sql = Sql & " UNION " & sqlHAFDet & CStr(row.Cells(2).Text) & """"


            'jam 2023/07/04

            'dsHistAnalogFormulas.SelectCommand = Sql

            LoadInitAnalogFormulas(Sql)

            grdHistAnalogFormulas.DataBind()
            grdHistAnalogFormulas.SelectedIndex = 1

            grdHistAnalogFormulas.Visible = True
        ElseIf Not bFormulaCheckingHistAnal Then 'jrs 2013/02/13
            grdHistAnalogFormulas.Visible = False
        End If

        'jrs 2024/06/26 for regular variables with historic analog stored in table IFSVARHISTANA
        If UCase(row.Cells(6).Text) = "IFSVARHISTANA" Then
            '"VarName" is in cells(2)
            Dim SQL As String = "SELECT Dim2, Dim3, [Historic Analog] FROM IFSVARHISTANA WHERE VarName = """
            SQL = SQL & CStr(row.Cells(2).Text) & """"

            LoadInitAnalogDet(SQL)

            grdHistAnalogDet.SelectedIndex = 0
        End If
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Show"
                mnuDataInf_Click()
        End Select
    End Sub

    'jrs 2013/02/12
    Protected Sub grdFormulas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFormulas.SelectedIndexChanged
        Dim row As GridViewRow = grdFormulas.SelectedRow
        'jrs 2011/07/11
        'For Variables inside Formulas with Formulas in Historic Analog
        If UCase(row.Cells(4).Text) = "FORMULA" Then
            Dim sql As String, sqlHAF As String, sqlHAFDet As String
            sqlHAF = "SELECT 0&Dim2&Dim3 as ListOrder, Dim2 as CodeVar, Dim3 as VarName, Formula FROM IFSVARHISTANAFORMULAS WHERE NAME = """
            sqlHAFDet = "SELECT 0&F.Dim2&F.Dim3&CodeVar as ListOrder, CodeVar, VarName, '' as Formula FROM IFSVARHISTANAFORMULAS F, IFSVARHISTANAFORMULASDET D WHERE D.ForVarName = F.Name AND (F.Dim2 = D.Dim2 OR F.Dim2 is Null) AND (F.Dim3 = D.Dim3 OR F.Dim3 is Null) AND F.Name = """

            Dim svar As String, pos As Integer
            svar = row.Cells(2).Text
            pos = InStr(svar, "[")
            If pos > 0 Then
                svar = Left(svar, pos - 1)
            End If
            sql = sqlHAF & svar & """"
            sql = sql & " UNION " & sqlHAFDet & svar & """"

            'jam 2023/04/07
            'dsHistAnalogFormulas.SelectCommand = sql


            LoadInitAnalogFormulas(sql)


            grdHistAnalogFormulas.DataBind()
            grdHistAnalogFormulas.SelectedIndex = 1

            grdHistAnalogFormulas.Visible = True
        Else
            grdHistAnalogFormulas.Visible = False
        End If
    End Sub

    'jrs 2011/07/12
    Private Sub mnuDataInf_Click()
        Dim row As GridViewRow = grdVarList.SelectedRow
        Dim idx As Integer
        Session("AvailabilityParent") = "frm_PackagedDisplays_Exp.aspx"
        Dim db As String, Valtbl As DataTable







        'jam 2023/04/07
        If Not (Session("mnuSQLite") Is Nothing) And Session("mnuSQLite") Then
            Dim Newtable As New DataTable("DataDictTable")
            vConnString2 = "DataSource=" + Session("PutFdrvRun") & "DataDict.db;"
            vSQLiteCon2 = New SQLiteConnection(vConnString2)
            vSQLiteCon2.Open()
            vSQLiteCommand2 = vSQLiteCon2.CreateCommand()
            vSQLiteCommand2.CommandText = "Select * from DataDict order by Variable"
            vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()



            ''Newtable.Load(vSQLiteReader2)

            'While (vSQLiteReader2.Read())

            '    grdFormulas.Rows(rr).Cells(3).Text = vSQLiteReader("Definition")


            'End While

            ''Session("DataDictVars") = Newtable

            Session("DataDictVars") = vSQLiteReader2

            vSQLiteCon2.Close()

        Else
            db = Session("PutFdrvRun") & "datadict.MDB"
            SetDataTablefromAccess(db, "Select * from DataDict order by Variable", "", Valtbl)
            Session("DataDictVars") = Valtbl
        End If



        'jrs 2024/06/26 and grdHistanalogDet.Visible = false
        If grdFormulas.Visible = False And grdHistAnalogFormulas.Visible = False And grdHistAnalogDet.Visible = False Then
            'jrs 2011/07/14 Historic Analog
            If row.Cells(6).Text > "" Then
                'jrs 2016/03/25 need some logic, in case multiple options (with commas or table)
                Dim xVars() As String, sVar As String
                If InStr(row.Cells(6).Text, ",") > 0 Then
                    xVars = Split(row.Cells(6).Text, ",")
                    Session("HEVarsCount") = UBound(xVars) + 1
                    ReDim Session("HEVars")(Session("HEVarsCount"))
                    Dim i As Integer
                    For i = 0 To UBound(xVars)
                        Session("HEVars")(i) = xVars(i)
                    Next

                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Select.aspx")
                ElseIf row.Cells(6).Text = "IFSVARHISTANA" Then
                    Dim rsV As DataTable, sqlV As String

                    'jam 2023/04/07
                    If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then
                        Dim iv As Integer
                        vConnString2 = "DataSource=" + Session("PutFdrvRun") & "ifsVar.db;"
                        vSQLiteCon2 = New SQLiteConnection(vConnString2)
                        vSQLiteCon2.Open()
                        vSQLiteCommand2 = vSQLiteCon2.CreateCommand()
                        vSQLiteCommand2.CommandText = "SELECT [Historic Analog] as Historic_Analog FROM IFSVARHISTANA WHERE VarName = """ & row.Cells(2).Text & """"
                        vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()

                        iv = 0
                        While (vSQLiteReader2.Read())

                            iv = iv + 1


                        End While
                        Session("HEVarsCount") = iv
                        ReDim Session("HEVars")(Session("HEVarsCount"))

                        iv = 0
                        While (vSQLiteReader2.Read())

                            Session("HEVars")(iv) = vSQLiteReader("Historic_Analog")

                            iv = iv + 1
                        End While


                        vSQLiteCon2.Close()

                    Else
                        sqlV = "SELECT [Historic Analog] FROM IFSVARHISTANA WHERE VarName = """ & row.Cells(2).Text & """"
                        SetDataTablefromAccess(Session("PutFdrvRun") & "ifsVar.mdb", sqlV, "", rsV, "")
                        Session("HEVarsCount") = rsV.Rows.Count


                        ReDim Session("HEVars")(Session("HEVarsCount"))
                        Dim i As Integer
                        For i = 0 To rsV.Rows.Count - 1
                            Session("HEVars")(i) = rsV.Rows(i).Item("Historic Analog")
                        Next

                    End If



                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Select.aspx")

                Else
                    sVar = row.Cells(6).Text
                End If
                'jam 2023/04/29 Saving in global the chosen variable 
                Session("ChosenVariable") = sVar

                Support.FormManager.StateStore(Me)
                'jrs 2016/03/25 Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & row.Cells(6).Text)
                Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & sVar)
            End If
        Else
            If grdHistAnalogFormulas.Visible Then
                row = grdHistAnalogFormulas.SelectedRow
                If row.Cells(3).Text = "" Then
                    idx = grdHistAnalogFormulas.SelectedIndex
                    grdHistAnalogFormulas.SelectedIndex = idx + 1
                    row = grdHistAnalogFormulas.SelectedRow
                End If

                'jam 2023/04/29 Saving in global the chosen variable 
                Session("ChosenVariable") = row.Cells(3).Text

                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & row.Cells(3).Text)

            ElseIf grdFormulas.Visible Then
                row = grdFormulas.SelectedRow
                If row.Cells(4).Text > "" Then

                    'jam 2023/04/29 Saving in global the chosen variable 
                    Session("ChosenVariable") = row.Cells(4).Text
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & row.Cells(4).Text)
                End If
            ElseIf grdHistAnalogDet.Visible Then 'jrs 2024/06/26
                row = grdHistAnalogDet.SelectedRow
                If row.Cells(3).Text > "" Then

                    'jam 2023/04/29 Saving in global the chosen variable 
                    Session("ChosenVariable") = row.Cells(3).Text
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & row.Cells(3).Text)
                End If

            End If
        End If
    End Sub
    '' <summary>
    '' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' 
    ''    JAM 2023/04/07 Unloading Form
    '' 
    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    'Private Sub frm_PackagedDisplays_Exp_Unload(sender As Object, e As EventArgs) Handles Me.Unload

    '    'jam 2023/04/07
    '    If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then
    '        vSQLiteCon.Close()
    '    End If
    'End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''
    ''                 'jam 2023/04/07 Initializing component
    ''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub LoadInitVarList()

        Dim table As New DataTable("VarListTable")


        Dim column As DataColumn
        Dim row As DataRow
        Dim vdataSet As DataSet
        Dim vsql2 As String
        Dim sqlWhere As String


        ' Create first column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = "ListOrder"
        column.Caption = "ListOrder"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create second column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "VarName"
        column.Caption = "VarName"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create third column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Definition"
        column.AutoIncrement = False
        column.Caption = "Definition"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create fourth column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = "FormulaId"
        column.AutoIncrement = False
        column.Caption = "FormulaId"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create fifth column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "FormulaDetHis"
        column.AutoIncrement = False
        column.Caption = "FormulaDetHis"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create sixth column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Historic_Analog"
        column.AutoIncrement = False
        column.Caption = "Historic_Analog"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create seventh column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Param2"
        column.AutoIncrement = False
        column.Caption = "Param2"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create eight column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Param3"
        column.AutoIncrement = False
        column.Caption = "Param3"
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



        vsql2 = Session("VarList_SQL")
        vsql2 = vsql2.Replace("False", "0").Replace("True", "1")

        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = vsql2
        vSQLiteReader = vSQLiteCommand.ExecuteReader()




        While (vSQLiteReader.Read())



            row = table.NewRow
            row("ListOrder") = vSQLiteReader("ListOrder")
            row("VarName") = vSQLiteReader("VarName")
            row("Definition") = vSQLiteReader("DEFINITION")
            row("FormulaId") = vSQLiteReader("FormulaId")
            row("FormulaDetHis") = vSQLiteReader("FormulaDetHis")
            row("Historic_Analog") = vSQLiteReader("Historic_Analog")
            row("Param2") = vSQLiteReader("Param2")
            row("Param3") = vSQLiteReader("Param3")

            table.Rows.Add(row)


        End While



        grdVarList.Visible = True



        grdVarList.DataSource = table

        grdVarList.DataBind()





    End Sub
    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' '
    '' '                 'jam 2023/04/07 Initializing Formula component
    '' '
    '' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub LoadInitFormulas(varsql As String)

        Dim table As New DataTable("FormulaTable")


        Dim column As DataColumn
        Dim row As DataRow
        Dim vdataSet As DataSet
        Dim vsql2 As String
        Dim sqlWhere As String


        ' Create first column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "CodeVar"
        column.Caption = "CodeVar"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create second column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "VarName"
        column.Caption = "VarName"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create third column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "DEFINITION"
        column.AutoIncrement = False
        column.Caption = "DEFINITION"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create fourth column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Historic_Analog"
        column.AutoIncrement = False
        column.Caption = "Historic_Analog"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)



        ' Create fifth column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "DIM2"
        column.AutoIncrement = False
        column.Caption = "DIM2"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Create sixth column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "DIM3"
        column.AutoIncrement = False
        column.Caption = "DIM3"
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



        vsql2 = varsql

        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = vsql2
        vSQLiteReader = vSQLiteCommand.ExecuteReader()




        While (vSQLiteReader.Read())



            row = table.NewRow
            'jam 2023/06/11
            row("CodeVar") = vSQLiteReader("CodeVar")
            row("VarName") = vSQLiteReader("VarName")
            row("DEFINITION") = vSQLiteReader("DEFINITION")
            row("Historic_Analog") = vSQLiteReader("Historic_Analog")
            row("DIM2") = vSQLiteReader("Dim2")
            row("DIM3") = vSQLiteReader("Dim3")

            table.Rows.Add(row)


        End While



        grdFormulas.Visible = True



        grdFormulas.DataSource = table

        grdFormulas.DataBind()





    End Sub
    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' '
    '' '                 'jam 2023/04/07 Initializing Analog Formula component
    '' '
    '' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub LoadInitAnalogFormulas(varsql As String)

        Dim table As New DataTable("AnalogFormulaTable")


        Dim column As DataColumn
        Dim row As DataRow
        Dim vdataSet As DataSet
        Dim vsql2 As String



        ' Create first column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "CodeVar"
        column.Caption = "CodeVar"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create second column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "VarName"
        column.Caption = "VarName"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create third column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Formula"
        column.AutoIncrement = False
        column.Caption = "Formula"
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



        vsql2 = varsql

        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = vsql2
        vSQLiteReader = vSQLiteCommand.ExecuteReader()




        While (vSQLiteReader.Read())



            row = table.NewRow
            row("CodeVar") = vSQLiteReader("CodeVar")
            row("VarName") = vSQLiteReader("VarName")
            row("Formula") = vSQLiteReader("FORMULA")


            table.Rows.Add(row)


        End While



        grdHistAnalogFormulas.Visible = True



        grdHistAnalogFormulas.DataSource = table

        grdHistAnalogFormulas.DataBind()





    End Sub

    'jrs 2024/06/26
    Protected Sub LoadInitAnalogDet(varsql As String)

        Dim table As New DataTable("AnalogDetTable")


        Dim column As DataColumn
        Dim row As DataRow
        Dim vdataSet As DataSet
        Dim vsql2 As String



        ' Create first column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Dim2"
        column.Caption = "Dim2"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create second column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Dim3"
        column.Caption = "Dim3"
        column.ReadOnly = False
        column.Unique = False
        column.AutoIncrement = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create third column.
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Historic Analog"
        column.AutoIncrement = False
        column.Caption = "Historic Analog"
        column.ReadOnly = False
        column.Unique = False

        ' Add the column to the table.
        table.Columns.Add(column)


        ' Instantiate the DataSet variable.
        vdataSet = New DataSet()

        ' Add the new DataTable to the DataSet.
        vdataSet.Tables.Add(table)



        vsql2 = varsql

        vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = vsql2
        vSQLiteReader = vSQLiteCommand.ExecuteReader()




        While (vSQLiteReader.Read())



            row = table.NewRow
            row("Dim2") = vSQLiteReader("Dim2")
            row("Dim3") = vSQLiteReader("Dim3")
            row("Historic Analog") = vSQLiteReader("Historic Analog")


            table.Rows.Add(row)


        End While



        grdHistAnalogDet.Visible = True



        grdHistAnalogDet.DataSource = table

        grdHistAnalogDet.DataBind()





    End Sub

End Class
