'cdv 2022/05/19 ****START****
Imports System.Data.SQLite

Partial Class frm_CreaGList
    Inherits Page

    Dim rsL As DataTable
    Dim allowEditing As Boolean
    Dim ErrorText As String
    Dim ComputationrsL As DataTable
    'Dim Timer As UI.Timer = Timer1
    Dim extension As String
    Dim PairColor(,) As Integer

    Private Sub frm_CreaGList_Load(sender As Object, e As EventArgs) Handles Me.Load
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Session("frm_CreaGLIstCaption")
        If UseOnlySQLite Then
            extension = ".db"
        Else
            extension = ".mdb"
        End If
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.GeneratedPrefix = "cswmWebMenu"
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        'cswmWebMenu.MenuBar.ShowOnMouseOver = True
        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Continue"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Import/Export"
        cswmGroup1.ID = "Import/Export"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Import"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Export"
        If Not Page.IsPostBack Then
            Session("IndexColor") = 0
            Call FullArrayColors(PairColor, Session("PairColor"))

            allowEditing = False
            'jrs 2023/06/26
            LoadGList()
        Else
            PairColor = Session("PairColor")
        End If
    End Sub

    Private Sub LoadGList()
        SetDataTablefromSQLite(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GListId ORDER BY UPPER(Description)", "", rsL)
        Dim i As Integer, idx As Integer
        i = 0
        idx = 0
        lstGListIdDesc.Items.Clear()
        lstGListId.Items.Clear()
        Do While (Not i = rsL.Rows.Count)
            If Not (Session("ImportedGList") = Nothing) AndAlso Session("ImportedGList") = rsL.Rows(i).Item("Description").ToString Then
                idx = i
                'jrs 2023/07/12
            ElseIf Not (Session("ImportedGListCode") = Nothing) AndAlso Session("ImportedGListCode") = rsL.Rows(i).Item("Code").ToString Then
                idx = i
            End If
            lstGListIdDesc.Items.Add(New ListItem(rsL.Rows(i).Item("Description")))
            lstGListId.Items.Add(New ListItem(rsL.Rows(i).Item("Code")))
            i = i + 1
        Loop
        lstGListIdDesc.SelectedIndex = idx
        Session("ImportedGListCode") = Nothing
        Session("ImportedGList") = Nothing
        lstGListIdDesc_SelectedIndexChanged(Me, New EventArgs)
    End Sub

    Public Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e
        Select Case objEA.Item.Caption
            Case "Continue"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_VarSel.aspx")
            Case "Import"
                'jrs 2023/06/23 ImportGListFile()
                Dim sImp As String = ImportGLists(True)
                Session("ImportedGList") = sImp

                LoadGList()

            Case "Export"
                ExportListsToTransmission()
        End Select
    End Sub

    Private Sub lstGListIdDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstGListIdDesc.SelectedIndexChanged
        txtGList.Text = lstGListIdDesc.Text
        lstNationsInGroup.Items.Clear()
        'jrs 2006/11/06
        SetDataTablefromSQLite(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GList WHERE GListCode = " &
        lstGListId.Items(lstGListIdDesc.SelectedIndex).ToString & " ORDER BY MemberOrder, Region", "", rsL)

        Dim rr As Integer = 0
        Do While rr < rsL.Rows.Count
            lstNationsInGroup.Items.Add(rsL.Rows(rr).Item("Region").ToString)
            rr = rr + 1
        Loop

        lstNationsNotInGroup.Items.Clear()
        If optRegion.Checked Then
            For rr = 1 To NR
                If Not lstNationsInGroup.Items.Contains(New ListItem(GRPS(1, rr))) Then
                    lstNationsNotInGroup.Items.Add(GRPS(1, rr))
                End If
            Next
        Else
            For rr = 1 To GCount
                If Not lstNationsInGroup.Items.Contains(New ListItem(GroupNames(rr))) Then
                    lstNationsNotInGroup.Items.Add(GroupNames(rr))
                End If
            Next
        End If
    End Sub

    Private Sub optGroups_CheckedChanged(sender As Object, e As EventArgs) Handles optGroups.CheckedChanged
        lstNationsNotInGroup.Items.Clear()
        For rr = 1 To GCount
            If Not lstNationsInGroup.Items.Contains(New ListItem(GroupNames(rr))) Then
                lstNationsNotInGroup.Items.Add(GroupNames(rr))
            End If
        Next
    End Sub

    Private Sub optRegion_CheckedChanged(sender As Object, e As EventArgs) Handles optRegion.CheckedChanged
        lstNationsNotInGroup.Items.Clear()
        For rr = 1 To NR
            If Not lstNationsInGroup.Items.Contains(New ListItem(GRPS(1, rr))) Then
                lstNationsNotInGroup.Items.Add(GRPS(1, rr))
            End If
        Next
    End Sub

    Private Sub cmdRemoveMember_Click(sender As Object, e As EventArgs) Handles cmdRemoveMember.Click
        If lstNationsInGroup.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
        Dim objConn As New SqliteConnection(strConnection)
        Dim objCommand As New SqliteCommand()
        Dim i = 0
        While i < lstNationsInGroup.Items.Count
            If lstNationsInGroup.Items(i).Selected Then
                Dim FormulaDelete As String
                FormulaDelete = "DELETE FROM GList WHERE GListCode = " & lstGListId.Items(lstGListIdDesc.SelectedIndex).ToString &
                    " AND Region = '" & lstNationsInGroup.Items(i).Value.Replace("'", "''") & "' AND (MemberOrder = " & CStr(lstNationsInGroup.SelectedIndex + 1) &
                    " OR MemberOrder = 0);" 'jrs 2007/12/04 added or MemberOrder = 0 in order to delete old GLists with no MemberOrder
                objCommand = New SqliteCommand(FormulaDelete, objConn)
                objConn.Open()
                objCommand.ExecuteNonQuery()
                Dim Sql = "UPDATE GList SET MemberOrder = MemberOrder - 1 WHERE GListCode = " &
                lstGListId.Items(lstGListIdDesc.SelectedIndex).ToString &
                " AND MemberOrder > " & CStr(lstNationsInGroup.SelectedIndex + 1)
                objCommand.CommandText = Sql
                objCommand.ExecuteNonQuery()
                objConn.Close()
                Dim bIsaCountry As Boolean
                bIsaCountry = (IFsRegs.Item(lstNationsInGroup.Items(i).Value).Index > 0)
                If (bIsaCountry And optRegion.Checked) Or (Not bIsaCountry And optGroups.Checked) Then
                    lstNationsNotInGroup.Items.Add(lstNationsInGroup.Items(i).Value)
                End If
                lstNationsInGroup.Items.Remove(lstNationsInGroup.Items(i))
            Else
                i = i + 1
            End If
        End While
        Dim list As List(Of String)
        list = New List(Of String)()
        For Each item As ListItem In lstNationsNotInGroup.Items
            list.Add(item.Value)
        Next
        list.Sort()
        lstNationsNotInGroup.Items.Clear()
        lstNationsNotInGroup.DataSource = list
        lstNationsNotInGroup.DataBind()
        lstNationsNotInGroup.Items.Item(lstNationsNotInGroup.Items.Count - 1).Selected = False

    End Sub

    Private Sub cmdAddMember_Click(sender As Object, e As EventArgs) Handles cmdAddMember.Click
        If lstNationsNotInGroup.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim sGroup As String
        If optRegion.Checked Then
            sGroup = "0"
        Else
            sGroup = "1"
        End If

        Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
        Dim objConn As New SqliteConnection(strConnection)
        Dim objCommand As New SqliteCommand()
        Dim i = 0
        Dim idx As Integer = 0
        While i < lstNationsNotInGroup.Items.Count
            If lstNationsNotInGroup.Items(i).Selected Then
                Dim FormulaInsert As String
                FormulaInsert = "INSERT INTO GList VALUES(" & lstGListId.Items(lstGListIdDesc.SelectedIndex).ToString & ", """ & lstNationsNotInGroup.SelectedValue.Replace("'", "''") &
                    """, " & sGroup & ", " & sGroup & ", " & CStr(lstNationsInGroup.Items.Count + 1) &
                    ", 0, """"," & Session("PairColor")(Session("IndexColor"), 0) & "," & Session("PairColor")(Session("IndexColor"), 1) & ")"
                objCommand = New SqliteCommand(FormulaInsert, objConn)
                objConn.Open()
                objCommand.ExecuteNonQuery()
                objConn.Close()
                idx = lstNationsNotInGroup.SelectedIndex
                lstNationsInGroup.Items.Add(lstNationsNotInGroup.Items(i).Value)
                lstNationsNotInGroup.Items.Remove(lstNationsNotInGroup.Items(i))
            Else
                i = i + 1
            End If
        End While
        Dim list As List(Of String)
        list = New List(Of String)()
        For Each item As ListItem In lstNationsInGroup.Items
            list.Add(item.Value)
        Next
        'list.Sort()
        lstNationsInGroup.Items.Clear()
        lstNationsInGroup.DataSource = list
        lstNationsInGroup.DataBind()
        lstNationsInGroup.Items.Item(lstNationsInGroup.Items.Count - 1).Selected = False

        If idx < lstNationsNotInGroup.Items.Count - 1 Then
            lstNationsNotInGroup.SelectedIndex = idx
        Else
            lstNationsNotInGroup.SelectedIndex = lstNationsNotInGroup.Items.Count - 1
        End If

        If Session("IndexColor") > 8 Then
            Session("IndexColor") = 0
        Else
            Session("IndexColor") = Session("IndexColor") + 1
        End If

    End Sub


    Private Sub cmdAddGList_Click(sender As Object, e As EventArgs) Handles cmdAddGList.Click
        AddGList(ErrorText, txtGList.Text, Session("PutFdrvRun"), Response)
        SetDataTablefromSQLite(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GListId ORDER BY UPPER(Description)", "", rsL)
        Dim i As Integer
        i = 0
        lstGListIdDesc.Items.Clear()
        lstGListId.Items.Clear()

        Dim idx As Integer = 0
        Do While (Not i = rsL.Rows.Count)
            If rsL.Rows(i).Item("Description") = txtGList.Text Then
                idx = i
            End If
            lstGListIdDesc.Items.Add(New ListItem(rsL.Rows(i).Item("Description")))
            lstGListId.Items.Add(New ListItem(rsL.Rows(i).Item("Code")))
            i = i + 1
        Loop
        lstGListIdDesc.SelectedIndex = idx
        lstGListIdDesc_SelectedIndexChanged(Me, New EventArgs)

    End Sub

    Private Sub cmdCopyGList_Click(sender As Object, e As EventArgs) Handles cmdCopyGList.Click
        AddGList(ErrorText, txtGList.Text, Session("PutFdrvRun"), Response)

        Dim InsertSQL = "INSERT INTO GList (GListCode, Region, [Group], Decomposable, MemberOrder, LineFeed, GroupTitle, ColorValue, LightColorValue) SELECT " & ErrorText &
            ", Region, [Group], Decomposable, MemberOrder, LineFeed, GroupTitle, ColorValue, LightColorValue FROM GList WHERE GListCode = (SELECT DISTINCT Code FROM GListId WHERE Description = '" & lstGListIdDesc.SelectedValue & "');"

        Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
        Dim objConn As New SqliteConnection(strConnection)
        objConn.Open()

        Dim objCommand = New SqliteCommand(InsertSQL, objConn)
        objCommand.ExecuteNonQuery()
        objConn.Close()

        SetDataTablefromSQLite(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GListId ORDER BY UPPER(Description)", "", rsL)
        Dim i As Integer
        i = 0
        lstGListIdDesc.Items.Clear()
        lstGListId.Items.Clear()

        Dim idx As Integer = 0
        Do While (Not i = rsL.Rows.Count)
            If rsL.Rows(i).Item("Description") = txtGList.Text Then
                idx = i
            End If
            lstGListIdDesc.Items.Add(New ListItem(rsL.Rows(i).Item("Description")))
            lstGListId.Items.Add(New ListItem(rsL.Rows(i).Item("Code")))
            i = i + 1
        Loop
        lstGListIdDesc.SelectedIndex = idx
        lstGListIdDesc_SelectedIndexChanged(Me, New EventArgs)

    End Sub

    Private Sub cmdDeleteGList_Click(sender As Object, e As EventArgs) Handles cmdDeleteGList.Click
        If lstGListIdDesc.SelectedValue <> "" Then
            Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
            Dim objConn As New SqliteConnection(strConnection)
            Dim objCommand As New SqliteCommand()
            Dim FormulaDelete = "DELETE FROM GList WHERE GListCode = (SELECT DISTINCT Code FROM GListId WHERE Description = '" & lstGListIdDesc.SelectedValue & "');"
            objCommand = New SqliteCommand(FormulaDelete, objConn)
            objConn.Open()
            objCommand.ExecuteNonQuery()
            FormulaDelete = "DELETE FROM GListId WHERE Description = '" & lstGListIdDesc.SelectedValue & "';"
            objCommand = New SqliteCommand(FormulaDelete, objConn)
            objCommand.ExecuteNonQuery()
            objConn.Close()

            Dim idx As Integer
            idx = lstGListIdDesc.SelectedIndex

            SetDataTablefromSQLite(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GListId ORDER BY UPPER(Description)", "", rsL)
            Dim i As Integer
            i = 0
            lstGListIdDesc.Items.Clear()
            lstGListId.Items.Clear()


            Do While (Not i = rsL.Rows.Count)
                lstGListIdDesc.Items.Add(New ListItem(rsL.Rows(i).Item("Description")))
                lstGListId.Items.Add(New ListItem(rsL.Rows(i).Item("Code")))
                i = i + 1
            Loop
            lstGListIdDesc.SelectedIndex = idx
            lstGListIdDesc_SelectedIndexChanged(Me, New EventArgs)

        End If

    End Sub

    Private Sub cmdCommitChanges_Click(sender As Object, e As EventArgs) Handles cmdCommitChanges.Click
        Dim inFile As String
        Dim OutFile As String
        inFile = Session("PutFdrvRun") & "IFs.db"
        OutFile = DataFileDir & "IFs.db"
        FileCopyCheck(inFile, OutFile)
        'jrs 2023/05/19
        Session("JSNeedsUpdate") = True
        RefreshGLists()
        SubVersionNumber = SubVersionNumber + 1
        UpdateIfsInit()

    End Sub

    'jrs 2023/05/19
    Private Sub UpdateIfsInit()
        Dim sIP As String
        sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")
        Dim sSQL As String = "UPDATE LoadFull SET [Value] = " & SubVersionNumber.ToString & " WHERE [Variable] = 'SubVersionNumber'"
        updateAccessWith1Command(sIP, sSQL)
    End Sub

    'jrs 2023/05/19
    Private Sub RefreshGLists()
        Dim valtbl As DataTable
        SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM GListId ORDER BY Description", "", valtbl)
        ReDim Preserve GListNames(valtbl.Rows.Count + 1)

        Dim eleloop As Integer = 0
        Do
            eleloop = eleloop + 1
            GListNames(eleloop) = valtbl.Rows(eleloop - 1).Item("Description")
            If eleloop = valtbl.Rows.Count Then Exit Do
        Loop
        GListCount = eleloop

    End Sub

    Sub ImportGListFile()
        If ImportList.HasFile = False Then
            Response.Write("<script>alert('please select a file')</script>")
        Else
            Dim MyFileCollection As HttpFileCollection
            Dim MyFile As HttpPostedFile
            Dim FileLen As Integer
            Dim MyString As String
            Dim MyStream As System.IO.Stream
            Dim FileName As String
            MyFileCollection = Request.Files
            MyFile = MyFileCollection(0)
            FileName = System.IO.Path.GetFileName(MyFile.FileName)
            If FileName.Contains("VarListTransmission") = True Then
                Try
                    MyFile.SaveAs(Session("PutFdrvRun") & FileName)

                    Dim ConnectionStr2 As String = "Data Source=" & Session("PutFdrvRun") & "\" & FileName
                    Dim Connection2 As New SqliteConnection(ConnectionStr2)

                    Connection2.Open()

                    Dim sqlCommand As SqliteCommand
                    Dim SQLInsert As String
                    Dim SQLAttach As String

                    SQLAttach = "ATTACH DATABASE '" & Session("PutFdrvRun") & "IFs.db' AS targetDB"
                    sqlCommand = New SqliteCommand(SQLAttach, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    SQLInsert = "INSERT INTO targetDB.GListID SELECT * FROM GListID WHERE Code NOT IN (SELECT Code FROM targetDB.GListID) GROUP BY Code"
                    sqlCommand = New SqliteCommand(SQLInsert, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    SQLInsert = "INSERT INTO targetDB.GList SELECT * FROM GList WHERE GListCode NOT IN (SELECT GListCode FROM targetDB.GList)"
                    sqlCommand = New SqliteCommand(SQLInsert, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    Dim sql As String = "SELECT Code FROM GListID"
                    sqlCommand = New SqliteCommand(sql, Connection2)
                    Dim rd As SqliteDataReader = sqlCommand.ExecuteReader()
                    rd.Read()
                    Session("ImportedGListCode") = rd.Item("Code").ToString

                    Session("ImportedValues") = "True"
                    Response.Write("<script> alert('values Successfully Imported') </script>")
                    'UserMadeChanges()
                    'Dim SQLSelect As String
                    'SQLSelect = "SELECT ListName FROM VarLists ORDER BY ListName LIMIT 1"
                    'sqlCommand = New SqliteCommand(SQLSelect, Connection2)
                    'Dim SQLreader = sqlCommand.ExecuteReader()
                    'While SQLreader.Read()
                    '    Session("SelectedLists") = New String() {}
                    '    Try
                    '        ReDim Session("SelectedLists")(1)
                    '        Session("SelectedLists")(0) = SQLreader("ListName").ToString()
                    '    Catch exeception As Exception
                    '        Exit Try
                    '    End Try
                    'End While
                    Connection2.Close()
                    Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
                Catch exception As Exception
                    Response.Write("<script>alert('There was the following issue: " & exception.Message.ToString() & "')</script>")
                End Try
            Else
                Response.Write("<script>alert('Your file must be a VarListsTransmission')</script>")
            End If
        End If
    End Sub

    Public Sub ExportListsToTransmission()
        If lstGListIdDesc.SelectedIndex = -1 Then
            Response.Write("<script>alert('Please Select G-Lists to Export');</script>")
            Exit Sub
        End If

        Dim i As Integer
        Dim groupNum As Integer
        Dim GroupList As DataTable
        SetDataTablefromSQLite(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GListId WHERE Code = " & lstGListId.Items(lstGListIdDesc.SelectedIndex).ToString, "", GroupList)
        If GroupList.Rows.Count = 0 Then
            Response.Write("<script>alert('The selected group wasn't found, please double check your ifs.db');</script>")
            Exit Sub
        End If
        groupNum = GroupList.Rows(0).Item("Code")
        AddListItemToTransmission(groupNum)
        Response.Write("<script>alert('Successfully Exported Selected Values to VarListTransmission.db in your RUNFILES folder');</script>")
    End Sub

    Sub AddListItemToTransmission(ItemToAdd As String)
        Dim tblGList As DataTable
        Dim tblGListMembers As DataTable
        SetDataTablefromSQLite(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GListID WHERE Code = " & ItemToAdd, "", tblGList)
        SetDataTablefromSQLite(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GList WHERE GListCode = " & ItemToAdd, "", tblGListMembers)
        If tblGListMembers.Rows.Count = 0 Then
            ThrowError("There were no members found in the selected G-List. There maybe an issue with your IFs.db")
            Exit Sub
        End If

        Dim TargetConnectionStr As String = "Data Source=" & Session("PutFdrvRun") & "VarListTransmission.db"
        Dim TargetConnection As New SqliteConnection(TargetConnectionStr)
        TargetConnection.Open()

        Dim sqlCommand As SqliteCommand
        Dim SQLInsertTarget As String

        SQLInsertTarget = "INSERT INTO GListID VALUES (" & tblGList.Rows(0).Item("Code") & ", '" & tblGList.Rows(0).Item("Description").ToString.Replace("'", "''") & "')"
        sqlCommand = New SqliteCommand(SQLInsertTarget, TargetConnection)
        sqlCommand.ExecuteNonQuery()

        For Each Row As DataRow In tblGListMembers.Rows
            SQLInsertTarget = "INSERT INTO GList VALUES (" & Row.Item(0).ToString
            For i = 1 To Row.ItemArray.Length - 1
                If i = 1 Or i = 6 Then
                    SQLInsertTarget = SQLInsertTarget & ", """
                Else
                    SQLInsertTarget = SQLInsertTarget & ", "
                End If
                SQLInsertTarget = SQLInsertTarget & Row.Item(i).ToString.Replace("'", "''")
                If i = 1 Or i = 6 Then
                    SQLInsertTarget = SQLInsertTarget & """"
                End If
            Next
            SQLInsertTarget = SQLInsertTarget & ")"
            sqlCommand = New SqliteCommand(SQLInsertTarget, TargetConnection)
            sqlCommand.ExecuteNonQuery()
        Next

        TargetConnection.Close()
    End Sub

    Sub ThrowError(msg)
        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    'jrs 2023/06/26
    Private Sub ImportGList(sGListName As String, dbSource As SqliteConnection, bOverWriteAll As Boolean)

        If sGListName = "" Then
            Response.Write("<script>alert('Please provide a valid G-List name.')</script>")
            Exit Sub
        End If

        Dim sql As String
        Dim rs As SQLiteDataReader
        'jrs 2025/09/23
        'adding UPPER to make case insensitive
        sql = "SELECT COUNT(*) FROM targetDB.GListId"
        sql = sql & " WHERE UPPER(Description) = """
        sql = sql & sGListName.ToUpper & """"
        Dim cmdT, cmdT2 As SqliteCommand
        cmdT = dbSource.CreateCommand
        cmdT2 = dbSource.CreateCommand
        cmdT.CommandText = sql

        rs = cmdT.ExecuteReader

        rs.Read()

        On Error GoTo sqlhandler

        If rs(0) > 0 Then
            rs.Close()
            'Dim ans As Integer
            'If Not bOverWriteAll Then
            'ans = MsgBox("The group " & sGListName & " already exists. Do you want to replace it.", vbYesNo, "Copy Group")
            'Else
            'ans = vbYes
            'End If
            'If ans = vbNo Then
            'Exit Sub
            'Else
            'Delete existing list
            sql = "DELETE FROM targetDB.GList WHERE GListCode IN (SELECT GListCode FROM targetDB.GList F, targetDB.GListID L WHERE UPPER(L.Description) = """ & sGListName.ToUpper & """ AND L.Code = F.GListCode)"
            cmdT2.CommandText = sql
            cmdT2.ExecuteNonQuery()

            sql = "DELETE FROM targetDB.GListID WHERE UPPER(Description) = """ & sGListName.ToUpper & """"
            cmdT2.CommandText = sql
            cmdT2.ExecuteNonQuery()

            'End If
        Else
            rs.Close()
        End If

        'jrs 2014/10/17
        Dim GListId As Integer
        Dim rs2 As SqliteDataReader
        sql = "SELECT MAX(Code) FROM targetDB.GListID"
        cmdT.CommandText = sql
        rs2 = cmdT.ExecuteReader

        'jrs 2015/09/09
        If rs2.Read Then
            GListId = rs2(0) + 1
        Else
            GListId = 1
        End If
        rs2.Close()


        sql = "INSERT INTO targetDB.GListID SELECT " & GListId & ", Description FROM GListID WHERE Description = """ & sGListName & """"

        cmdT2.CommandText = sql
        cmdT2.ExecuteNonQuery()

        sql = "INSERT INTO targetDB.GList SELECT " & GListId & ", Region, [Group], Decomposable, MemberOrder, LineFeed, GroupTitle, ColorValue, LightColorValue FROM GList WHERE GListCode IN (SELECT Code FROM GListId WHERE Description = """ & sGListName & """)"
        cmdT2.CommandText = sql
        cmdT2.ExecuteNonQuery()

        rs.Close()

        Exit Sub

sqlhandler:
        'jrs 2013/10/30
        'MsgBox Err.Description
        Dim sErr As String = Err.Description

        Response.Write("<script>alert('" & sGListName & " NOT Copied! " & sErr & "')</script>")
    End Sub

    Public Function ImportGLists(bOverWriteWOAsk As Boolean) As String
        If ImportList.HasFile = False Then
            Response.Write("<script>alert('please select a file')</script>")
        Else
            Dim bFirst As Boolean
            bFirst = True

            Dim MyFileCollection As HttpFileCollection
            Dim MyFile As HttpPostedFile
            Dim FileLen As Integer
            Dim MyString As String
            Dim MyStream As System.IO.Stream
            Dim FileName As String
            MyFileCollection = Request.Files
            MyFile = MyFileCollection(0)
            FileName = System.IO.Path.GetFileName(MyFile.FileName)
            If FileName.Contains("VarListTransmission") = True Then
                Try
                    'Open Source Database
                    MyFile.SaveAs(Session("PutFdrvRun") & FileName)

                    Dim ConnectionStr2 As String = "Data Source=" & Session("PutFdrvRun") & "\" & FileName
                    Dim Connection2 As New SqliteConnection(ConnectionStr2)

                    Connection2.Open()

                    Dim sqlCommand As SqliteCommand
                    Dim SQLAttach As String

                    'Open Target Database
                    SQLAttach = "ATTACH DATABASE '" & Session("PutFdrvRun") & "IFs" & extension & "' AS targetDB"
                    sqlCommand = New SqliteCommand(SQLAttach, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    'Open Source list of Groups to import
                    Dim sql As String = "SELECT Description FROM GListID"
                    Dim cmdSourceRdr As SqliteCommand = Connection2.CreateCommand
                    cmdSourceRdr.CommandText = sql
                    Dim readerSource As SqliteDataReader
                    readerSource = cmdSourceRdr.ExecuteReader

                    'and fill it with selection
                    Do While readerSource.Read()
                        ImportGList(readerSource(0).ToString, Connection2, bOverWriteWOAsk)
                        If bFirst Then
                            ImportGLists = readerSource(0).ToString
                            bFirst = False
                        End If
                    Loop

                    Session("ImportedValues") = "True"
                    Response.Write("<script> alert('values Successfully Imported') </script>")

                    'Close Source and Target database
                    Connection2.Close()
                Catch exception As Exception
                    Response.Write("<script>alert('There was the following issue: " & exception.Message.ToString() & "')</script>")
                End Try
            Else
                Response.Write("<script>alert('Your file must be a VarListsTransmission')</script>")
            End If

            Response.Write("<script>alert('Transmission File has been loaded.')</script>")
        End If

    End Function

End Class
