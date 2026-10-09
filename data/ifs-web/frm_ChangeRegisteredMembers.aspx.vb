
'cdv 2022/05/19 ****START****
Imports System.Data.SQLite

'JAM 2020-08-12
Partial Class frm_ChangeRegisteredMembers
    Inherits Page
    Dim rsL As DataTable
    Dim allowEditing As Boolean
    Dim ErrorText As String
    Dim ComputationrsL As DataTable
    'Dim Timer As UI.Timer = Timer1
    Dim extension As String
    Private Sub frm_ChangeRegisteredMembers_Load(sender As Object, e As EventArgs) Handles Me.Load
        If UseOnlySQLite Then
            extension = ".db"
        Else
            extension = ".mdb"
        End If
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.GeneratedPrefix = "cswmWebMenu"
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
            allowEditing = False

            LoadGroups()

        End If
    End Sub

    Private Sub LoadGroups()
        If OnlyCoreGroups.Checked Then
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", "SELECT * FROM GroupIDNewFull WHERE Core = 1 ORDER BY GroupName", "", rsL)
        Else
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", "SELECT * FROM GroupIDNewFull ORDER BY GroupName", "", rsL)
        End If
        Dim i As Integer
        i = 0
        ListGroups.Items.Clear()

        Do While (Not i = rsL.Rows.Count)
            ListGroups.Items.Add(New ListItem(rsL.Rows(i).Item("GroupName")))
            'jrs 2023/05/11
            If Session("ImportedGroup") > "" Then
                If ListGroups.Items(i).Value = Session("ImportedGroup") Then
                    ListGroups.Items(i).Selected = True
                End If
            End If

            i = i + 1
        Loop

    End Sub

    Public Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e
        Select Case objEA.Item.Caption
            Case "Continue"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_MainMenu.aspx")
            Case "Import"
                'jrs 2023/05/11 ImportGroupFile()
                Dim sImp As String = ImportGroups(True)
                Session("ImportedGroup") = sImp

                If Session("ImportedGroup") > "" Then
                    OnlyCoreGroups.Checked = False
                End If

                LoadGroups()

                If Session("ImportedGroup") > "" Then
                    ListGroups_SelectedIndexChanged(Me, New EventArgs)
                End If

            Case "Export"
                ExportListsToTransmission()

        End Select
    End Sub
    Private Sub OnlyCoreGroups_CheckedChanged(sender As Object, e As EventArgs) Handles OnlyCoreGroups.CheckedChanged
        ListGroups.Items.Clear()
        If OnlyCoreGroups.Checked Then
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GroupIDNewFull WHERE Core = 1 ORDER BY GroupName", "", rsL)
        Else
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GroupIDNewFull ORDER BY GroupName", "", rsL)
        End If
        Dim i As Integer
        i = 0
        Do While (Not i = rsL.Rows.Count)
            ListGroups.Items.Add(New ListItem(rsL.Rows(i).Item("GroupName")))
            i = i + 1
        Loop
    End Sub

    Private Sub ListGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListGroups.SelectedIndexChanged
        ListNationsInGroup.Items.Clear()
        ListNationsNotInGroup.Items.Clear()
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT Region FROM GroupingFull WHERE GroupNo in (SELECT GroupNum FROM GroupIDNewFUll WHERE GroupName = '" & ListGroups.SelectedValue & "') ORDER BY Region", "", rsL)
        Dim i As Integer
        i = 0
        Do While (Not i = rsL.Rows.Count)
            ListNationsInGroup.Items.Add(New ListItem(rsL.Rows(i).Item("Region")))
            i = i + 1
        Loop
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT DISTINCT Region FROM GroupingFull WHERE Region NOT IN (SELECT Region FROM GroupingFull WHERE GroupNo in (SELECT GroupNum FROM GroupIDNewFUll WHERE GroupName = '" & ListGroups.SelectedValue & "')) ORDER BY Region", "", rsL)
        i = 0
        Do While (Not i = rsL.Rows.Count)
            ListNationsNotInGroup.Items.Add(New ListItem(rsL.Rows(i).Item("Region")))
            i = i + 1
        Loop
    End Sub

    Private Sub AddNation_Click(sender As Object, e As EventArgs) Handles AddNation.Click
        If ListNationsNotInGroup.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
        Dim objConn As New SqliteConnection(strConnection)
        Dim objCommand As New SqliteCommand()
        Dim i = 0
        While i < ListNationsNotInGroup.Items.Count
            If ListNationsNotInGroup.Items(i).Selected Then
                Dim FormulaInsert As String
                FormulaInsert = "INSERT INTO GroupingFull VALUES('" & ListNationsNotInGroup.Items(i).Value.Replace("'", "''") & "', (SELECT DISTINCT RegionNo FROM GroupingFull WHERE Region = '" & ListNationsNotInGroup.Items(i).Value.Replace("'", "''") & "'),(SELECT DISTINCT GroupNum FROM GroupIDNewFull WHERE GroupName = '" & ListGroups.SelectedValue & "'));"
                objCommand = New SqliteCommand(FormulaInsert, objConn)
                objConn.Open()
                objCommand.ExecuteNonQuery()
                objConn.Close()
                ListNationsInGroup.Items.Add(ListNationsNotInGroup.Items(i).Value)
                ListNationsNotInGroup.Items.Remove(ListNationsNotInGroup.Items(i))
            Else
                i = i + 1
            End If
        End While
        Dim list As List(Of String)
        list = New List(Of String)()
        For Each item As ListItem In ListNationsInGroup.Items
            list.Add(item.Value)
        Next
        list.Sort()
        ListNationsInGroup.Items.Clear()
        ListNationsInGroup.DataSource = list
        ListNationsInGroup.DataBind()
        ListNationsInGroup.Items.Item(ListNationsInGroup.Items.Count - 1).Selected = False

    End Sub

    Private Sub RemoveNation_Click(sender As Object, e As EventArgs) Handles RemoveNation.Click
        If ListNationsInGroup.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
        Dim objConn As New SqliteConnection(strConnection)
        Dim objCommand As New SqliteCommand()
        Dim i = 0
        While i < ListNationsInGroup.Items.Count
            If ListNationsInGroup.Items(i).Selected Then
                Dim FormulaDelete As String
                FormulaDelete = "DELETE FROM GroupingFull WHERE Region = '" & ListNationsInGroup.Items(i).Value.Replace("'", "''") & "' AND GroupNo = (SELECT DISTINCT GroupNum FROM GroupIDNewFull WHERE GroupName = '" & ListGroups.SelectedValue & "');"
                objCommand = New SqliteCommand(FormulaDelete, objConn)
                objConn.Open()
                objCommand.ExecuteNonQuery()
                objConn.Close()
                ListNationsNotInGroup.Items.Add(ListNationsInGroup.Items(i).Value)
                ListNationsInGroup.Items.Remove(ListNationsInGroup.Items(i))
            Else
                i = i + 1
            End If
        End While
        Dim list As List(Of String)
        list = New List(Of String)()
        For Each item As ListItem In ListNationsNotInGroup.Items
            list.Add(item.Value)
        Next
        list.Sort()
        ListNationsNotInGroup.Items.Clear()
        ListNationsNotInGroup.DataSource = list
        ListNationsNotInGroup.DataBind()
        ListNationsNotInGroup.Items.Item(ListNationsNotInGroup.Items.Count - 1).Selected = False
    End Sub


    Protected Sub CommitChanges_Click(sender As Object, e As EventArgs) Handles CommitChanges.Click
        Dim inFile As String
        Dim OutFile As String
        inFile = Session("PutFdrvRun") & "IFs.db"
        OutFile = DataFileDir & "IFs.db"
        FileCopyCheck(inFile, OutFile)
        'jrs 2023/05/19
        Session("JSNeedsUpdate") = True
        RefreshGroups()
        SubVersionNumber = SubVersionNumber + 1
        UpdateIfsInit()
        'jrs 2023/06/09
        IFsTables = New Collection
    End Sub

    'jrs 2023/05/19
    Private Sub UpdateIfsInit()
        Dim sIP As String
        sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")
        Dim sSQL As String = "UPDATE LoadFull SET [Value] = " & SubVersionNumber.ToString & " WHERE [Variable] = 'SubVersionNumber'"
        updateAccessWith1Command(sIP, sSQL)
    End Sub


    'jrs 2023/05/19
    Private Sub RefreshGroups()
        Dim valtbl As DataTable
        'jrs 2022/07/19
        If UseOnlySQLite Then
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " ORDER BY UPPER(GroupName)", "", valtbl)
        Else
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "", valtbl)
        End If
        'jrs 2008/12/08
        ReDim Preserve GroupNames(valtbl.Rows.Count + 1)
        ReDim Preserve GroupCodes(valtbl.Rows.Count + 1)

        Dim eleloop As Integer = 0
        Do
            eleloop = eleloop + 1
            GroupNames(eleloop) = valtbl.Rows(eleloop - 1).Item("GroupName")
            GroupCodes(eleloop) = valtbl.Rows(eleloop - 1).Item("GroupNum")
            If eleloop = valtbl.Rows.Count Then Exit Do
        Loop
        GCount = eleloop
        GroupNames(eleloop + 1) = "ALL"
        'cdv 2021/08/20
        'jrs 2023/06/08 added UPPER
        SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " WHERE Core = TRUE ORDER BY UPPER(GroupName)", "", valtbl)
        'jrs 2022/05/31 added for compatibility with old files
        If Not IsNothing(valtbl) Then
            ReDim Preserve CoreGroupNames(valtbl.Rows.Count + 1)
            eleloop = 0
            Do
                eleloop += 1
                CoreGroupNames(eleloop) = valtbl.Rows(eleloop - 1).Item("GroupName")
                If eleloop = valtbl.Rows.Count Then Exit Do
            Loop
            CGCount = eleloop
            CoreGroupNames(eleloop + 1) = "ALL"
        End If
    End Sub

    Private Sub ChangeCoreGroups_Click(sender As Object, e As EventArgs) Handles ChangeCoreGroups.Click
        'Placeholder
    End Sub

    Private Sub cswmWebMenu_ServerItemClick(sender As Object, e As EventArgs) Handles cswmWebMenu.ServerItemClick
        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e
        Select Case objEA.Item.Caption
            Case "Continue"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_MainMenu.aspx")
        End Select
    End Sub

    Protected Sub AddGroup_Click(sender As Object, e As EventArgs) Handles AddGroup.Click
        If IsValidGroup() = False Then
            Response.Write(ErrorText)
            Exit Sub
        End If
        Dim getID = "SELECT MAX(GroupNum) as GroupNum FROM GroupIDNewFull"
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", getID, "", rsL)
        Dim ID = rsL.Rows(0).Item("GroupNum") + 1
        Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
        Dim objConn As New SqliteConnection(strConnection)
        Dim objCommand As New SqliteCommand()
        Dim InsertSQL = "INSERT INTO GroupIDNewFull (GroupName, GroupNum, Core) VALUES('" & GroupText.Text & "', " & CStr(ID) & ", " & CInt(OnlyCoreGroups.Checked) * -1 & ")"
        objCommand = New SqliteCommand(InsertSQL, objConn)
        objConn.Open()
        objCommand.ExecuteNonQuery()
        objConn.Close()
        ListGroups.Items.Clear()
        If OnlyCoreGroups.Checked Then
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GroupIDNewFull WHERE Core = 1 ORDER BY GroupName", "", rsL)
        Else
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GroupIDNewFull ORDER BY GroupName", "", rsL)
        End If
        Dim i As Integer
        i = 0
        Do While (Not i = rsL.Rows.Count)
            ListGroups.Items.Add(New ListItem(rsL.Rows(i).Item("GroupName")))
            i = i + 1
        Loop
        ListGroups.Items.FindByText(GroupText.Text).Selected = True
        ListGroups_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub CopyGroup_Click(sender As Object, e As EventArgs) Handles CopyGroup.Click
        If IsValidGroup() = False Then
            Response.Write(ErrorText)
            Exit Sub
        End If
        Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
        Dim objConn As New SqliteConnection(strConnection)
        Dim objCommand As New SqliteCommand()
        Dim getID = "SELECT MAX(GroupNum) as GroupNum FROM GroupIDNewFull"
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", getID, "", rsL)
        Dim ID = rsL.Rows(0).Item("GroupNum") + 1
        Dim InsertSQL = "INSERT INTO GroupIDNewFull (GroupName, GroupNum, Core) VALUES('" & GroupText.Text & "', " & CStr(ID) & ", " & CInt(OnlyCoreGroups.Checked) * -1 & ")"
        objCommand = New SqliteCommand(InsertSQL, objConn)
        objConn.Open()
        objCommand.ExecuteNonQuery()
        InsertSQL = "INSERT INTO GroupingFull (Region, RegionNo, GroupNo) SELECT Region,RegionNo,'" & CStr(ID) & "' AS GroupNo FROM GroupingFull WHERE GroupNo = (SELECT DISTINCT GroupNum FROM GroupIDNewFull WHERE GroupName = '" & ListGroups.SelectedValue & "');"

        objCommand = New SqliteCommand(InsertSQL, objConn)
        objCommand.ExecuteNonQuery()
        objConn.Close()
        ListGroups.Items.Clear()
        If OnlyCoreGroups.Checked Then
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GroupIDNewFull WHERE Core = 1 ORDER BY GroupName", "", rsL)
        Else
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GroupIDNewFull ORDER BY GroupName", "", rsL)
        End If
        Dim i As Integer
        i = 0
        Do While (Not i = rsL.Rows.Count)
            ListGroups.Items.Add(New ListItem(rsL.Rows(i).Item("GroupName")))
            i = i + 1
        Loop
        ListGroups.Items(0).Selected = True
        ListGroups_SelectedIndexChanged(sender, e)
    End Sub
    Protected Function IsValidGroup() As Boolean
        ErrorText = ""
        If GroupText.Text = "" Then
            ErrorText = "<script>alert('Please enter a name for the group');</script>"
            Return False
        End If
        If GroupText.Text.Length > 35 Then
            ErrorText = "<script>alert('That group name is too long, please shorten it');</script>"
            Return False
        End If
        If GroupText.Text.Contains(",") Then
            ErrorText = "<script>alert('Group Names can\'t contain commas');</script>"
            Return False
        End If
        If GroupText.Text.Contains("""") Then
            ErrorText = "<script>alert('Group Names can\'t contain double quotes');</script>"
            Return False
        End If
        'anf 2022/10/19 ****START****
        If GroupText.Text.Contains("'") Then
            ErrorText = "<script>alert('Group Names can\'t contain single quotes');</script>"
            Return False
        End If
        Dim firstChar = GroupText.Text.Chars(0)
        If (firstChar = " ") Then 'first char - space
            ErrorText = "<script>alert('Group Names can\'t begin with space');</script>"
            Return False
        End If
        If Char.IsSymbol(firstChar) Or firstChar = "!" Or firstChar = "@" Or firstChar = "#" Or firstChar = "<" Or firstChar = ">" Or firstChar = "?" Or firstChar = "%" Or firstChar = "&" Or firstChar = "*" Then 'first char - symbol
            ErrorText = "<script>alert('Group Names can\'t begin with symbols');</script>"
            Return False
        End If
        If Char.IsDigit(firstChar) Then 'first char - numeric, remining - empty
            Dim i As Integer
            For i = 1 To GroupText.Text.Length - 1
                If Char.IsLetter(GroupText.Text.Chars(i)) Then
                    Exit For
                End If
            Next
            If i = GroupText.Text.Length And Char.IsLetter(GroupText.Text.Chars(i - 1)) = False Then
                ErrorText = "<script>alert('Please enter atleast one letter.');</script>"
                Return False
            End If
        End If
        'anf 2022/10/19 ****END****
        'jrs 2025/09/23 SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT GroupName FROM GroupIDNewFull WHERE GroupName = '" & GroupText.Text & "'", "", rsL)
        'Taylor was having problems because trying to import groups with same name but different casing
        'Need to block that
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT UPPER(GroupName) FROM GroupIDNewFull WHERE UPPER(GroupName) = '" & GroupText.Text.ToUpper & "'", "", rsL)
        If rsL.Rows.Count > 0 Then
            ErrorText = "<script>alert('Please change the name to a unique name');</script>"
            Return False
        End If
        Return True
    End Function
    Protected Sub DeleteGroup_Click(sender As Object, e As EventArgs) Handles DeleteGroup.Click
        If ListGroups.SelectedValue <> "" Then
            Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFs.db;"
            Dim objConn As New SqliteConnection(strConnection)
            Dim objCommand As New SqliteCommand()
            Dim FormulaDelete = "DELETE FROM GroupingFull WHERE GroupNo = (SELECT DISTINCT GroupNum FROM GroupIDNewFull WHERE GroupName = '" & ListGroups.SelectedValue & "');"
            objCommand = New SqliteCommand(FormulaDelete, objConn)
            objConn.Open()
            objCommand.ExecuteNonQuery()
            FormulaDelete = "DELETE FROM GroupIDNewFull WHERE GroupName = '" & ListGroups.SelectedValue & "';"
            objCommand = New SqliteCommand(FormulaDelete, objConn)
            objCommand.ExecuteNonQuery()
            objConn.Close()
            ListGroups.Items.Clear()
            If OnlyCoreGroups.Checked Then
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GroupIDNewFull WHERE Core = 1 ORDER BY GroupName", "", rsL)
            Else
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.db", "SELECT * FROM GroupIDNewFull ORDER BY GroupName", "", rsL)
            End If
            Dim i As Integer
            i = 0
            Do While (Not i = rsL.Rows.Count)
                ListGroups.Items.Add(New ListItem(rsL.Rows(i).Item("GroupName")))
                i = i + 1
            Loop
            ListGroups.Items(0).Selected = True
            ListGroups_SelectedIndexChanged(sender, e)
        End If


    End Sub

    'cdv 2022/12/12 ****START****
    Public Sub ExportListsToTransmission()
        If ListGroups.SelectedValue = Nothing Then
            Response.Write("<script>alert('Please Select Groups to Export');</script>")
            Exit Sub
        End If

        Dim i As Integer
        Dim groupNum As Integer
        Dim GroupList As DataTable
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs" & extension, "SELECT * FROM GroupIDNewFull WHERE GroupName = '" & ListGroups.SelectedValue & "'", "", GroupList)
        If GroupList.Rows.Count = 0 Then
            Response.Write("<script>alert('The selected group wasn't found, please double check your ifs.mdb');</script>")
            Exit Sub
        End If
        groupNum = GroupList.Rows(0).Item("GroupNum")
        AddListItemToTransmission(groupNum)
        Response.Write("<script>alert('Successfully Exported Selected Values to VarListTransmission.db in your RUNFILES folder');</script>")
    End Sub
    Sub AddListItemToTransmission(ItemToAdd As String)
        Dim GroupList As DataTable
        Dim CountryList As DataTable
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs" & extension, "SELECT * FROM GroupIDNewFull WHERE GroupNum = '" & ItemToAdd & "'", "", GroupList)
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs" & extension, "SELECT * FROM GroupingFull WHERE GroupNo = '" & ItemToAdd & "'", "", CountryList)
        If CountryList.Rows.Count = 0 Then
            ThrowError("There were no countries found in the selected group. There maybe an issue with your IFs.mdb")
            Exit Sub
        End If

        Dim TargetConnectionStr As String = "Data Source=" & Session("PutFdrvRun") & "VarListTransmission" & extension
        Dim TargetConnection As New SqliteConnection(TargetConnectionStr)
        TargetConnection.Open()

        Dim sqlCommand As SqliteCommand
        Dim SQLInsertTarget As String
        For Each Row As DataRow In CountryList.Rows
            SQLInsertTarget = "INSERT INTO GroupingFull VALUES ('" & Row.Item("Region").ToString.Replace("'", "''") & "', '" & Row.Item("RegionNo") & "', '" & Row.Item("GroupNo") & "')"
            sqlCommand = New SqliteCommand(SQLInsertTarget, TargetConnection)
            sqlCommand.ExecuteNonQuery()
        Next

        SQLInsertTarget = "INSERT INTO GroupIDNewFull VALUES ('" & GroupList.Rows(0).Item("GroupName") & "', '" & GroupList.Rows(0).Item("GroupNum") & "', '" & GroupList.Rows(0).Item("LongGroupName") & "', '" & GroupList.Rows(0).Item("Alias") & "', '" & GroupList.Rows(0).Item("Core") & "')"
        sqlCommand = New SqliteCommand(SQLInsertTarget, TargetConnection)
        sqlCommand.ExecuteNonQuery()

        TargetConnection.Close()
    End Sub

    Sub ThrowError(msg)
        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    Sub ImportGroupFile()
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

                    SQLAttach = "ATTACH DATABASE '" & Session("PutFdrvRun") & "IFs" & extension & "' AS targetDB"
                    sqlCommand = New SqliteCommand(SQLAttach, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    SQLInsert = "INSERT INTO targetDB.GroupIDNewFull SELECT * FROM GroupIDNewFull WHERE GroupNum NOT IN (SELECT GroupNum FROM targetDB.GroupIDNewFull) GROUP BY GroupNum"
                    sqlCommand = New SqliteCommand(SQLInsert, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    SQLInsert = "INSERT INTO targetDB.GroupingFull SELECT * FROM GroupingFull WHERE GroupNo NOT IN (SELECT GroupNo FROM targetDB.GroupingFull) GROUP BY GroupNo, RegionNo"
                    sqlCommand = New SqliteCommand(SQLInsert, Connection2)
                    sqlCommand.ExecuteNonQuery()

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
                    'jrs 2023/05/09 Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
                Catch exception As Exception
                    Response.Write("<script>alert('There was the following issue: " & exception.Message.ToString() & "')</script>")
                End Try
            Else
                Response.Write("<script>alert('Your file must be a VarListsTransmission')</script>")
            End If
        End If
    End Sub
    'cdv 2022/12/12 ****END****

    'jrs 2023/05/11
    'jrs 2013/11/12 procedure to Import Groups, returns name of first group loaded
    Public Function ImportGroups(bOverWriteWOAsk As Boolean) As String
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
                    'jrs 2023/10/10 check for repeated Groups
                    Dim sql As String = "SELECT GroupName FROM GroupIDNewFull GROUP BY GroupName HAVING COUNT(*) > 1"
                    Dim cmdSourceRdr As SqliteCommand = Connection2.CreateCommand
                    cmdSourceRdr.CommandText = sql
                    Dim readerSource As SqliteDataReader
                    readerSource = cmdSourceRdr.ExecuteReader

                    If readerSource.HasRows Then
                        Session("ImportedValues") = "False"
                        readerSource.Read()
                        Response.Write("<script> alert('Group " & readerSource(0) & " is repeated. Please fix file to continue.') </script>")
                    Else
                        readerSource.Close()
                        sql = "SELECT GroupName FROM GroupIDNewFull"
                        cmdSourceRdr.CommandText = sql
                        readerSource = cmdSourceRdr.ExecuteReader

                        'and fill it with selection
                        Do While readerSource.Read()
                            ImportGroup(readerSource(0).ToString, Connection2, bOverWriteWOAsk)
                            If bFirst Then
                                ImportGroups = readerSource(0).ToString
                                bFirst = False
                            End If
                        Loop

                        Session("ImportedValues") = "True"
                        Response.Write("<script> alert('values Successfully Imported') </script>")


                    End If

                    'Close Source and Target database
                    Connection2.Close()
                Catch exception As Exception
                    Response.Write("<script>alert('There was the following issue: " & exception.Message.ToString() & "')</script>")
                End Try
            Else
                Response.Write("<script>alert('Your file must be a VarListsTransmission')</script>")
            End If

            'jrs 2023/10/10
            If Session("ImportedValues") = "True" Then
                Response.Write("<script>alert('Transmission File has been loaded into RUNFILES, remember to Commit your changes to make them permanent.')</script>")
            End If
        End If

    End Function

    Private Sub ImportGroup(sGroupName As String, dbSource As SqliteConnection, bOverWriteAll As Boolean)

        If sGroupName = "" Then
            Response.Write("<script>alert('Please provide a valid group name.')</script>")
            Exit Sub
        End If

        Dim sql As String
        Dim rs As SqliteDataReader
        sql = "SELECT COUNT(*) FROM targetDB." & GroupIDNew
        sql = sql & " WHERE GroupName = """
        sql = sql & sGroupName & """"
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
            'ans = MsgBox("The group " & sGroupName & " already exists. Do you want to replace it.", vbYesNo, "Copy Group")
            'Else
            'ans = vbYes
            'End If
            'If ans = vbNo Then
            'Exit Sub
            'Else
            'Delete existing list
            'jrs 2025/09/23 sql = "DELETE FROM targetDB." & Grouping & " WHERE GroupNo IN (SELECT GroupNo FROM targetDB." & Grouping & " F, targetDB." & GroupIDNew & " L WHERE L.GroupName = """ & sGroupName & """ AND L.GroupNum = F.GroupNo)"
            'Avoid having same group names with different casing
            sql = "DELETE FROM targetDB." & Grouping & " WHERE GroupNo IN (SELECT GroupNo FROM targetDB." & Grouping & " F, targetDB." & GroupIDNew & " L WHERE UPPER(L.GroupName) = """ & sGroupName.ToUpper & """ AND L.GroupNum = F.GroupNo)"
            cmdT2.CommandText = sql
            cmdT2.ExecuteNonQuery()

            'jrs 2025/09/23 sql = "DELETE FROM targetDB." & GroupIDNew & " WHERE GroupName = """ & sGroupName & """"
            'Avoid having same group names with different casing
            sql = "DELETE FROM targetDB." & GroupIDNew & " WHERE UPPER(GroupName) = """ & sGroupName.ToUpper & """"
            cmdT2.CommandText = sql
            cmdT2.ExecuteNonQuery()

            'End If
        Else
            rs.Close()
        End If

        'jrs 2014/10/17
        Dim GroupId As Integer
        Dim rs2 As SqliteDataReader
        sql = "SELECT MAX(GroupNum) FROM targetDB." & GroupIDNew
        cmdT.CommandText = sql
        rs2 = cmdT.ExecuteReader

        'jrs 2015/09/09
        If rs2.Read Then
            GroupId = rs2(0) + 1
        Else
            GroupId = 1
        End If
        rs2.Close()


        sql = "INSERT INTO targetDB.GroupIDNewFull SELECT GroupName, " & GroupId & ", LongGroupName, Alias, Core FROM GroupIDNewFull WHERE GroupName = """ & sGroupName & """"

        cmdT2.CommandText = sql
        cmdT2.ExecuteNonQuery()

        sql = "INSERT INTO targetDB.GroupingFull SELECT Region, RegionNo, " & GroupId & " FROM GroupingFull WHERE GroupNo IN (SELECT GroupNum FROM GroupIDNewFull WHERE GroupName = """ & sGroupName & """)"
        cmdT2.CommandText = sql
        cmdT2.ExecuteNonQuery()

        rs.Close()

        Exit Sub

sqlhandler:
        'jrs 2013/10/30
        'MsgBox Err.Description
        Dim sErr As String = Err.Description

        Response.Write("<script>alert('" & sGroupName & " NOT Copied! " & sErr & "')</script>")
    End Sub

End Class