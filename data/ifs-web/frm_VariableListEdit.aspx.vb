'cdv 2022/05/19 ****START****
Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.Drawing
Imports System.Xml
Imports Accord.IO
Imports TMSWebControls
Partial Class frm_VariableListEdit
    Inherits Page
    Dim rsL As DataTable
    Dim ComputationrsL As DataTable
    Dim Timer As UI.Timer = Timer1
    Dim extension As String

    Private Sub frm_VariableListEdit_Load(sender As Object, e As EventArgs) Handles Me.Load
        If UseOnlySQLite Then
            extension = ".db"
        Else
            extension = ".mdb"
        End If
        'dsb 2025/10/21 Initialize priority list options --- Start --- 
        If Not IsPostBack Then
            chkPriorityList.Items.Clear()
            chkPriorityList.Items.Add(New ListItem("01 - Core"))
            chkPriorityList.Items.Add(New ListItem("02 - UNDP Training"))
            chkPriorityList.Items.Add(New ListItem("03 - AUDA - NEPAD Training"))
            chkPriorityList.Items.Add(New ListItem("04 - Legacy Core"))
            chkPriorityList.Items.Add(New ListItem("05 - Legacy Full"))
            chkPriorityList.Items.Add(New ListItem("06 - Parameters Only"))
        End If
        '--- End ---
        If Session("clickExit") = "True" Then
            Session("clickExit") = "False"
            Exit_Click(sender, e)
        End If
        Dim isNewList As Boolean = False
        If IsPostBack = False Or Session("DeletedValues") = "True" Or Session("ImportedValues") = "True" Then
            Session("DeletedValues") = "False"
            Session("ImportedValues") = "False"
            LstListName.Items.Clear()
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT DISTINCT ListName, Type FROM VarLists ORDER BY ListName", "", rsL)
            rsL = rsL.Select("", "ListName").CopyToDataTable
            Dim i = 0
            Do While (Not i = rsL.Rows.Count)
                LstListName.Items.Add(New ListItem(rsL.Rows(i).Item("ListName")))
                i = i + 1
            Loop
            If Session("SelectedLists") Is Nothing Then
                Session("SelectedLists") = New String() {}
            End If
            Try
                For i = 0 To CType(Session("SelectedLists"), Array).Length - 1
                    If Not (LstListName.Items.FindByValue(Session("SelectedLists")(i)) Is Nothing) Then
                        LstListName.Items.FindByValue(Session("SelectedLists")(i)).Selected = True
                        txtListName.Text = Session("SelectedLists")(i)
                    End If
                Next
            Catch exeception As Exception
                Exit Try
            End Try
            If Session("sNameList") <> "" Then
                For i = 0 To LstListName.Items.Count - 1
                    If LstListName.Items.Item(i).Text = Session("sNameList") Then
                        LstListName.Items.Item(i).Selected = True
                        Exit For
                    End If
                Next i
                If i = LstListName.Items.Count Then
                    txtListName.Text = Session("sNameList")
                    SaveListName()
                    isNewList = True 'DSB 2025/11/20 When it's a new list 
                End If
            End If
            DisplayListVariables()
            LoadPriorityList() 'dsb 2025/12/9 Load priority for the list when importing
            'dsb 2025/10/14 Default suggestion based on the list --- Start ---
            Try
                'DSB 2025/11/20 Only run suggestions logic when it's a new list
                If isNewList AndAlso LstVariableList.Items.Count > 0 Then
                    Dim firstVar As String = LstVariableList.Items(0).Text
                    Dim segTable As DataTable
                    'Find the suggested category to be displayed from TopCat table
                    SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT DISTINCT t.Category AS segmentName FROM IFSVAR I LEFT JOIN TopCat T ON I.[Segment Name] = T.UserViewCategory AND I.[Submodule Name] = T.TopCategory WHERE I.Name = '" & firstVar & "'", "", segTable)
                    'In case of formula, try to get the first variable (A, numerator) in the formula
                    If segTable.Rows.Count = 0 Then
                        'dsb 2025/11/17 Was using Formula Name before, now using Formula ID to get the formula
                        Dim formulaIdTable As DataTable
                        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT FormulaId FROM VarLists WHERE ListName = '" & txtListName.Text & "' AND VarName = '" & firstVar & "' AND Formula = 1", "", formulaIdTable)
                        If formulaIdTable.Rows.Count > 0 Then
                            Dim formulaId As Integer = CInt(formulaIdTable.Rows(0).Item("FormulaId"))
                            Dim formulaTable As DataTable
                            SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT v.CodeVar, v.VarName FROM VarListsFormulas v WHERE v.FormulaId = '" & formulaId & "' ORDER BY v.CodeVar", "", formulaTable)
                            If formulaTable.Rows.Count > 0 Then
                                firstVar = formulaTable.Rows(0).Item("VarName").ToString().Split("[")(0)
                                SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT DISTINCT t.Category AS segmentName FROM IFSVAR I LEFT JOIN TopCat T ON I.[Segment Name] = T.UserViewCategory AND I.[Submodule Name] = T.TopCategory WHERE I.Name = '" & firstVar & "'", "", segTable)
                            End If
                        End If
                    End If
                    'Select the suggested category if found
                    If segTable.Rows.Count > 0 Then
                        Dim suggestedCat As String = segTable.Rows(0).Item("segmentName").ToString()
                        If suggestedCat <> "" Then
                            If lstCategoryList.Items.FindByText(suggestedCat) IsNot Nothing Then
                                lstCategoryList.ClearSelection()
                                lstCategoryList.Items.FindByText(suggestedCat).Selected = True
                            Else
                                lstCategoryList.Items.Add(New ListItem(suggestedCat))
                                lstCategoryList.Items.FindByText(suggestedCat).Selected = True
                            End If
                            txtCategory.Text = lstCategoryList.SelectedValue
                            btnUpdateCategory_Click(btnUpdateCategory, EventArgs.Empty)
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try '--- End ---
            ChangesAlert.Visible = Session("UserHasUnsavedChanges")
            'jrs 2023/08/18 Session("SelectedLists") = ""
        End If
    End Sub
    Sub SaveListName()
        'jrs 2025/09/23 SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT ListName FROM VarLists WHERE ListName = '" & txtListName.Text & "'", "", rsL)
        'Make it case insensitive
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT ListName FROM VarLists WHERE UPPER(ListName) = '" & txtListName.Text.ToUpper & "'", "", rsL)
        If rsL.Rows.Count > 0 Then
            LoadMessage("That name is already in the variable list", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        Else
            LstListName.Items.Add(txtListName.Text)
            Dim list As List(Of String)
            list = New List(Of String)()
            For Each item As ListItem In LstListName.Items
                list.Add(item.Value)
            Next
            list.Sort()
            LstListName.Items.Clear()
            LstListName.DataSource = list
            LstListName.DataBind()
            LstListName.Items.Item(LstListName.Items.Count - 1).Selected = False
            LstListName.Items.FindByText(txtListName.Text).Selected = True

            Dim VarName As String
            Dim Dim1Name As String
            Dim Dim2Name As String
            Dim Dim3Name As String
            Dim BracketLocation As Integer
            Dim VariablersL As DataTable
            Dim FormularsL As DataTable
            Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFsVar" & extension & ";"
            Dim objConn As New SQLiteConnection(strConnection)
            Dim objCommand As New SQLiteCommand()
            Dim isFormula As Boolean
            Dim formulaID As Integer = 0
            'jrs 2024/12/19
            Dim DisplayUnits As String = ""

            'jrs 2023/10/10
            Dim bSkipDim2, bSkipDim3 As Boolean
            'jrs 2024/08/23
            Dim bDim2FormConsistent, bDim3FormConsistent As Boolean
            Dim sDim2Form, sDim3Form As String
            'jrs 2025/12/08
            Dim bDimFormConsistent As Boolean
            Dim sDimForm As String

            If Session("COUNTE") = 1 Then
                bSkipDim2 = True
                bSkipDim3 = True
                'jrs 2026/03/20 why not? this is blocking selection in formulas like INTSFLOWS(Country,Sec1,Sec2)/POP(Country)
                ''jrs 2025/12/08
                ''Better not skip if it's a formula
                'If Session("aFormulas")(1) Is Nothing = False AndAlso Not String.IsNullOrEmpty(If(Session("aFormulas")(1).Name, "")) AndAlso Session("aFormulas")(1).Name = Session("VARN")(1, 1) Then
                '    bSkipDim2 = False
                '    bSkipDim3 = False
                'End If
            Else
                'jrs 2024/08/23 if they're different shouldn't skip it
                bSkipDim2 = True
                bSkipDim3 = True
                For i = 1 To Session("COUNTE") - 1
                    If Session("GR2N")(i) <> Session("GR2N")(i + 1) Then
                        'jrs 2024/08/23 changed from true to false
                        bSkipDim2 = False
                        bSkipDim3 = False
                        Exit For
                    End If
                    If Session("GR3N")(i) <> Session("GR3N")(i + 1) Then
                        'jrs 2024/08/23 changed from true to false
                        bSkipDim3 = False
                        Exit For
                    End If
                Next
            End If

            For i = 1 To Session("COUNTE")

                VarName = Session("VARN")(1, i)
                BracketLocation = VarName.IndexOf("[")
                If BracketLocation <> -1 Then
                    VarName = VarName.Remove(BracketLocation, VarName.Length - BracketLocation)
                End If
                If Session("GR1N")(i) = "" Then
                    Dim1Name = ""
                Else
                    Dim1Name = Session("GR1N")(i)
                End If

                'jrs 2023/10/10 added  Or bSkipDim2
                If Session("GR2N")(i) = "" Or bSkipDim2 Then
                    Dim2Name = ""
                Else
                    Dim2Name = Session("GR2N")(i)
                End If

                'jrs 2023/10/10 added  Or bSkipDim3
                If Session("GR3N")(i) = "" Or bSkipDim3 Then
                    Dim3Name = ""
                Else
                    Dim3Name = Session("GR3N")(i)
                End If

                'jrs 2024/12/19
                If Session("GR4N")(i) = "" Then
                    DisplayUnits = ""
                Else
                    DisplayUnits = Session("GR4N")(i)
                End If

                isFormula = False
                Dim SQLinsert As String
                SQLinsert = ""
                Try
                    If Session("aFormulas")(i) Is Nothing = False AndAlso Not String.IsNullOrEmpty(If(Session("aFormulas")(i).Name, "")) AndAlso Session("aFormulas")(i).Name = VarName Then 'dsb 2025/11/18 Added condition to check for null or empty and avoid carrying over previous formula to the next non computed list
                        isFormula = True
                        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT * FROM VarListsFormulas", "", FormularsL)
                        formulaID = FormularsL.Compute("Max(FormulaId)", "") + 1
                        'jrs 2025/12/08
                        bDimFormConsistent = bCheckConsistentDimsInFormula(Session("aFormulas")(i), 1)
                        'jrs 2024/08/23
                        'Check for different dimensions in the same formula, ex. ENP(OthrRenew)/ENP(Total)
                        bDim2FormConsistent = bCheckConsistentDimsInFormula(Session("aFormulas")(i), 2)
                        'jrs 2025/12/08
                        'Check if not Bilateral vs Monadic
                        If bDim2FormConsistent Then
                            If bCheckFormulaBilatVsMonadic(Session("aFormulas")(i)) Then
                                bDim2FormConsistent = False
                            End If
                        End If

                        bDim3FormConsistent = bCheckConsistentDimsInFormula(Session("aFormulas")(i), 3)

                        For j = 1 To (Session("aFormulas")(i).get_size() - 1)
                            'jrs 2025/12/08
                            If bDimFormConsistent Then
                                sDimForm = Dim1Name
                            Else
                                sDimForm = Session("aFormulas")(i).disVar.displayDet1(j)
                            End If
                            'jrs 2024/08/23
                            If bDim2FormConsistent Then
                                sDim2Form = Dim2Name
                            Else
                                sDim2Form = Session("aFormulas")(i).disVar.displayDet2(j)
                            End If
                            If bDim3FormConsistent Then
                                sDim3Form = Dim3Name
                            Else
                                sDim3Form = Session("aFormulas")(i).disVar.displayDet3(j)
                            End If

                            Dim FormulaInsert As String
                            'jrs 2024/08/23 changed to use sDim2Form and sDim3Form instead of directly Dim2Name and Dim3Name
                            FormulaInsert = "INSERT INTO VarListsFormulas(FormulaId, FormulaName, CodeVar, VarName, Dim1, Dim2, Dim3)" _
                            & " VALUES ('" & formulaID & "'," _
                                       & "'" & VarName & "'," _
                                       & "'" & Session("aFormulas")(i).disVar.display(j) & "'," _
                                       & "'" & Session("aFormulas")(i).disVar.displayDet(j) & "'," _
                                       & "'" & sDimForm & "'," _
                                       & "'" & sDim2Form & "'," _
                                       & "'" & sDim3Form & "');"

                            objCommand = New SQLiteCommand(FormulaInsert, objConn)
                            objConn.Open()
                            objCommand.ExecuteNonQuery()
                            objConn.Close()
                            UserMadeChanges()
                        Next
                    End If
                Catch exception As IndexOutOfRangeException
                    'This is purely to fix that aFormulas populates differently from COUNTE when there's no computation.
                    'Other errors will still be shown
                End Try
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT * FROM IFSVAR WHERE NAME = '" & VarName & "'", "", VariablersL)
                If isFormula Then
                    'jrs 2024/08/27 added , CreationDate, ChangeDate
                    SQLinsert = "INSERT INTO VarLists (ListName,VarName, [Type], [Index], Dim1, Dim2, Dim3, Param1, Param2, Param3, " _
                        & "History, Category, DisplayTitle, Formula, FormulaId, FormulaDet, HistoryplusForecast," _
                        & "HisCompEmpFor, ListOrder, CompareOtherForecast, NumDecimals, CreationDate, ChangeDate)" _
                        & " VALUES ('" & LstListName.SelectedValue & "'," _
                                    & "'" & VarName & "'," _
                                    & "'" & "V" & "'," _
                                    & "'" & 0 & "'," _
                                    & "'" & 0 & "'," _
                                    & "'" & 0 & "'," _
                                    & "'" & 0 & "'," _
                                    & "'" & Dim1Name & "'," _
                                    & "'" & Dim2Name & "'," _
                                    & "'" & Dim3Name & "'," _
                                    & "'" & If(Session("HistoricBase") = "True", 1, 0) & "'," _
                                    & "'" & "" & "'," _
                                    & "'" & "" & "'," _
                                    & "'" & If(isFormula, 1, 0) & "'," _
                                    & "'" & formulaID & "'," _
                                    & "'" & Session("aFormulas")(i).get_formula_as_string() & "'," _
                                    & "'" & If(Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast", 1, 0) & "'," _
                                    & "'" & If(Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar", 1, 0) & "'," _
                                    & "'" & 1 & "'," _
                                    & "'" & CType(0, String) & "'," _
                                    & "'" & 0 & "','" _
                                    & System.DateTime.Now().ToString("yyyy-MM-dd HH:mm:ss") & "','" _
                                    & System.DateTime.Now().ToString("yyyy-MM-dd HH:mm:ss") _
                                    & "');"
                Else
                    'jrs 2023/10/10 Priority List  = 'No' it was 0
                    'jrs 2023/09/18 removing VARID
                    'jrs 2024/08/23 added , CreationDate, ChangeDate
                    'jrs 2024/12/18 added display units
                    SQLinsert = "INSERT INTO VarLists (ListName,VarName, [Type], [Index], Dim1, Dim2, Dim3, Param1, Param2, Param3, " _
                        & "History, Category, DisplayTitle, Formula, FormulaId, FormulaDet, HistoryplusForecast," _
                        & "HisCompEmpFor, ListOrder, CompareOtherForecast, NumDecimals, PriorityList, DisplayUnits, CreationDate, ChangeDate )" _
                        & " VALUES ('" & CType(LstListName.SelectedValue, String) & "'," _
                                    & "'" & CType(VarName, String) & "'," _
                                    & "'" & "V" & "'," _
                                    & "'0'," _
                                    & "'" & CType(VariablersL.Rows(0).Item("DIMENSION1"), String) & "'," _
                                    & "'" & CType(VariablersL.Rows(0).Item("DIMENSION2"), String) & "'," _
                                    & "'" & CType(VariablersL.Rows(0).Item("DIMENSION3"), String) & "'," _
                                    & "'" & CType(Dim1Name, String) & "'," _
                                    & "'" & CType(Dim2Name, String) & "'," _
                                    & "'" & CType(Dim3Name, String) & "'," _
                                    & "'" & If(Session("HistoricBase") = "True", 1, 0) & "'," _
                                    & "'" & "" & "'," _
                                    & "'" & "" & "'," _
                                    & "'" & If(isFormula, 1, 0) & "'," _
                                    & "'" & CType(formulaID, String) & "'," _
                                    & "'" & "" & "'," _
                                    & "'" & If(Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast", 1, 0) & "'," _
                                    & "'" & If(Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar", 1, 0) & "'," _
                                    & "'" & CType(1, String) & "'," _
                                    & "'" & CType(0, String) & "'," _
                                    & "'" & CType(0, String) & "'," _
                                    & "'No', '" & DisplayUnits & "', '" _
                                    & System.DateTime.Now().ToString("yyyy-MM-dd HH:mm:ss") & "','" _
                                    & System.DateTime.Now().ToString("yyyy-MM-dd HH:mm:ss") _
                                    & "');"

                End If
                objConn.Open()
                objCommand = New SQLiteCommand(SQLinsert, objConn)
                objCommand.ExecuteNonQuery()
                objConn.Close()
                UserMadeChanges()

            Next
            txtListName.Text = LstListName.SelectedValue
            Session("sNameList") = ""
        End If

    End Sub

    'jrs 2024/08/23
    Private Function bCheckConsistentDimsInFormula(f As Formula, DimNum As Integer) As Boolean
        bCheckConsistentDimsInFormula = True
        Dim i As Integer
        Dim sDim As String = ""
        For i = 1 To f.disVar.disSize
            'jrs 2025/12/08 added dimNum = 1 case, for Bilateral vs Partner logic
            If DimNum = 1 Then
                If sDim = "" Then
                    sDim = f.disVar.displayDet1(i)
                ElseIf sDim <> f.disVar.displayDet1(i) And f.disVar.displayDet1(i) > "" Then
                    bCheckConsistentDimsInFormula = False
                    Exit Function
                End If
            ElseIf DimNum = 2 Then
                If sDim = "" Then
                    sDim = f.disVar.displayDet2(i)
                ElseIf sDim <> f.disVar.displayDet2(i) And f.disVar.displayDet2(i) > "" Then
                    bCheckConsistentDimsInFormula = False
                    Exit Function
                End If
            ElseIf DimNum = 3 Then
                If sDim = "" Then
                    sDim = f.disVar.displayDet3(i)
                ElseIf sDim <> f.disVar.displayDet3(i) And f.disVar.displayDet3(i) > "" Then
                    bCheckConsistentDimsInFormula = False
                    Exit Function
                End If
            End If
        Next
    End Function

    'js 2025/12/10
    Private Function bCheckFormulaBilatVsMonadic(f As Formula) As Boolean
        bCheckFormulaBilatVsMonadic = False
        Dim sVar As String = ""
        Dim sDim1 As String = ""
        If f.disVar.disSize > 1 Then
            sVar = f.disVar.displayDet(1)
            'remove []
            Dim pos As Integer
            pos = sVar.IndexOf("[")
            sVar = sVar.Remove(pos, sVar.Length - pos)
            'check if bilateral
            Dim idxVar As Integer
            idxVar = IfsVars.Item(sVar).Index
            If VDim1(idxVar) = 1 And VDim2(idxVar) = 1 Then
                sVar = f.disVar.displayDet(2)
                pos = sVar.IndexOf("[")
                sVar = sVar.Remove(pos, sVar.Length - pos)
                idxVar = IfsVars.Item(sVar).Index
                If VDim1(idxVar) = 1 And VDim2(idxVar) = 0 Then
                    bCheckFormulaBilatVsMonadic = True
                End If
            End If
        End If
    End Function

    Protected Sub DisplayListVariables()
        LstVariableList.Items.Clear()
        'jrs 2025/12/16 added replace ' with '' to avoid SQL errors
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT DISTINCT * FROM VarLists WHERE ListName = '" & Replace(txtListName.Text, "'", "''") & "' ORDER BY ListOrder", "", rsL)
        Dim i = 0
        Do While i < rsL.Rows.Count
            LstVariableList.Items.Add(New ListItem(rsL.Rows(i).Item("VarName")))
            i += 1
        Loop
        If LstVariableList.Items.Count <> 0 Then
            LstVariableList.Items.Item(LstVariableList.Items.Count - 1).Selected = True
            DisplayComputations()
        End If
        DisplayListCategory()
    End Sub

    Protected Sub DisplayListCategory()
        Dim VariableCategory As DataTable
        Dim CategoryList As DataTable
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT DISTINCT Category FROM VarLists ORDER BY Category", "", CategoryList)
        'jrs 2025/12/16 added replace ' with '' to avoid SQL errors
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT DISTINCT Category FROM VarLists WHERE ListName = '" & Replace(txtListName.Text, "'", "''") & "' ORDER BY Category", "", VariableCategory)
        Dim i = 0
        Dim newList = False
        Do While i < CategoryList.Rows.Count
            If lstCategoryList.Items.FindByText(CategoryList.Rows(i).Item("Category")) Is Nothing Then
                lstCategoryList.Items.Add(New ListItem(CategoryList.Rows(i).Item("Category")))
            End If
            If VariableCategory.Rows.Count = 0 Then
                newList = True
            End If
            i = i + 1
        Loop
        If Not newList Then
            lstCategoryList.SelectedItem.Selected = False
            lstCategoryList.Items.FindByText(VariableCategory.Rows(0).Item("Category")).Selected = True
        Else
            'lstCategoryList.Items.FindByText("Agriculture").Selected = True 'dsb 2025/10/21 No category named Agriculture in new meso categories
            lstCategoryList.Items.FindByText("Agriculture Demand").Selected = True
        End If
        txtCategory.Text = lstCategoryList.SelectedValue
    End Sub

    'dsb 2025/12/9 Hanlder to call the unsaved changes message when priority list is changed
    Protected Sub chkPriorityList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkPriorityList.SelectedIndexChanged
        If LstListName.SelectedIndex >= 0 Then
            UserMadeChanges()
        End If
    End Sub

    'dsb 2025/12/9 To also work when importing a new list
    Private Sub LoadPriorityList()
        'dsb 2025/10/21 Update priority list selections based on saved values --- Start ---
        Dim dt As New DataTable
        'jrs 2025/12/16 added replace ' with '' to avoid SQL errors
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT PriorityList FROM VarLists WHERE ListName = '" & Replace(txtListName.Text, "'", "''") & "' LIMIT 1", "", dt)
        If dt.Rows.Count > 0 Then
            'dsb 2025/12/9 To properly trim the priorities when there are multiple
            Dim priorities As String = dt.Rows(0).Item("PriorityList").ToString()
            Dim savedPriorities As String() = priorities.Split(","c).Select(Function(s) s.Trim()).Where(Function(s) s <> "").ToArray()
            'Dim savedPriorities As String() = dt.Rows(0).Item("PriorityList").ToString().Split(","c)
            For Each item As ListItem In chkPriorityList.Items
                If savedPriorities.Contains(item.Text.Split("-"c)(0).Trim()) Then
                    item.Selected = True
                Else
                    item.Selected = False
                End If
            Next
        Else
            For Each item As ListItem In chkPriorityList.Items
                item.Selected = False
            Next
        End If
        '--- End ---
    End Sub

    Public Sub LstListName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstListName.SelectedIndexChanged
        txtListName.Text = LstListName.SelectedValue
        DisplayListVariables()
        'jrs 2023/02/05 DisplayComputations()
        LoadPriorityList() 'dsb 2025/12/9
    End Sub

    Protected Sub Exit_Click(sender As Object, e As EventArgs) Handles [Exit].Click
        'jrs 2023/07/19
        Session("PDFlexCategoriesIdxs") = Nothing

        If Session("JSNeedsUpdate") = True Then
            LoadCategories()
            CreateJSforFlexDisplays("IFSBASE.RUN", Session("PutFdrvRun"), Session("CategoryIds"))

            'jrs 2025/07/10
            GenerateVarsforVarSelList(Session("PutFdrvRun"))
            GenerateVarsforVarSelListH(Session("PutFdrvRun"))
            GenerateVarsforVarSelListHF(Session("PutFdrvRun"))

            Session("JSNeedsUpdate") = False
            Session("clickExit") = "True"
        Else
            If Session("fromSelfManaged") = "True" Then
                Session("fromSelfManaged") = "False"
                Response.Redirect("frm_Varsel.aspx")
            Else
                Session("fromSelfManaged") = "False"
                Response.Redirect("frm_PackagedDisplaysFlex.aspx")
            End If
        End If
    End Sub
    Protected Sub DisplayComputations()
        'jrs 2025/12/16 added replace ' with '' to avoid SQL errors
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT * FROM VarLists WHERE ListName = '" & Replace(txtListName.Text, "'", "''") & "' AND VarName = '" & LstVariableList.SelectedValue & "'", "", ComputationrsL)
        If ComputationrsL.Rows.Item(0)("Formula") = True Then
            LstComputationList.Items.Clear()
            Dim Formularsl As DataTable
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT * FROM VarListsFormulas WHERE FormulaId = " & ComputationrsL.Rows(0).Item("FormulaId"), "", Formularsl)
            Dim i As Integer
            i = 0
            Do While i <> Formularsl.Rows.Count
                Dim BracketLocation As Integer
                Dim VarName As String
                VarName = Formularsl.Rows(i).Item("VarName")
                BracketLocation = VarName.IndexOf("[")
                If BracketLocation <> -1 Then
                    VarName = VarName.Remove(BracketLocation, VarName.Length - BracketLocation)
                End If
                VarName = UCase(Formularsl.Rows(i).Item("CodeVar")) & " = " & VarName
                LstComputationList.Items.Add(VarName)
                i = i + 1
            Loop
            FormulaID.Text = "Formula: " & UCase(ComputationrsL.Rows.Item(0)("FormulaDet"))
            FormulaID.Visible = True
            LstComputationList.Visible = True
        Else
            FormulaID.Visible = False
            LstComputationList.Visible = False
        End If

    End Sub


    Protected Sub LstVariableList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstVariableList.SelectedIndexChanged
        DisplayComputations()
    End Sub

    Protected Sub lstCategoryList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCategoryList.SelectedIndexChanged
        txtCategory.Text = lstCategoryList.SelectedValue
    End Sub

    Protected Sub btnUpdateCategory_Click(sender As Object, e As EventArgs) Handles btnUpdateCategory.Click
        Dim SqlUpdate As String
        For list = 0 To LstListName.Items.Count - 1
            If LstListName.Items(list).Selected Then
                SqlUpdate = "UPDATE VarLists SET Category = '" & txtCategory.Text & "' WHERE ListName = '" & LstListName.Items(list).Text & "'"
                Dim strConnection As String = "Data Source=" & Session("PutFdrvRun") & "IFsVar" & extension
                Dim objConn As New SQLiteConnection(strConnection)
                Dim objCommand As New SQLiteCommand(SqlUpdate, objConn)
                objConn.Open()
                objCommand.ExecuteNonQuery()
                objConn.Close()
                UserMadeChanges()
            End If
        Next
    End Sub

    Public Sub UserMadeChanges()
        Session("UserHasUnsavedChanges") = True
        ChangesAlert.Text = "You Have Unsaved Changes. If you close IFs now, all unsaved changes will be lost."
        ChangesAlert.ForeColor = Color.Red
        Timer1.Enabled = False
        ChangesAlert.Visible = True

    End Sub

    Public Sub UndoUserMadeChanges()
        Session("UserHasUnsavedChanges") = False
        ChangesAlert.Text = "Changes Successfully Saved"
        ChangesAlert.ForeColor = Color.Black

        Timer1.Interval() = 5000
        Timer1.Enabled = True
    End Sub
    Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ' Write the SignalTime.
        ChangesAlert.Text = "You Have Unsaved Changes. If you close IFs now, all unsaved changes will be lost."
        ChangesAlert.ForeColor = Color.Red
        ChangesAlert.Visible = False
        Timer1.Enabled = False
    End Sub
    Protected Sub Save_Changes()
        'dsb 2025/10/14 Validate category names before saving --- Start ---
        Dim TopCatTable As DataTable
        Dim sqlTopCat As String
        'Find all categories that are too general and have a suggested specific category
        sqlTopCat = "SELECT DISTINCT Category, UserViewCategory FROM TopCat WHERE UserViewCategory <> Category"

        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, sqlTopCat, "", TopCatTable)

        'Dim preventCategories As New List(Of String) From {"Demand", "Finance", "General", "Investment", "Mortality", "Production", "Security"}

        Dim catName As String = txtCategory.Text.Trim()
        Dim preventCatRows As New List(Of DataRow)
        If TopCatTable IsNot Nothing AndAlso TopCatTable.Rows.Count > 0 Then
            For Each r As DataRow In TopCatTable.Rows
                If LCase(r("UserViewCategory").ToString().Trim()) = LCase(catName) Or r("UserViewCategory").ToString().Trim() = catName Then
                    preventCatRows.Add(r)
                    'Exit For
                End If
            Next
        End If
        'Dislay the message if the category name is too general
        If preventCatRows.Count > 0 Then
            Dim suggestions As New List(Of String)
            For Each r As DataRow In preventCatRows
                suggestions.Add(r("Category").ToString().Trim())
            Next
            Dim suggestionText As String = String.Join("' or '", suggestions.Distinct())
            LoadMessage("The category name '" & catName & "' is too general and cannot be used. Please choose a specific name like '" & suggestionText & "'", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
            Exit Sub
        End If
        '--- End ---
        'dsb 2025/10/21 Save selected priority list options --- Start ---
        Dim selectedPriorities As New List(Of String)
        For Each item As ListItem In chkPriorityList.Items
            If item.Selected Then
                selectedPriorities.Add(item.Text.Split("-"c)(0).Trim())
            End If
        Next
        Dim selectedPrioritiesStr As String = String.Join(",", selectedPriorities)
        'jrs 2025/12/16 added replace ' with '' to avoid SQL errors
        Dim sqlUpdatePriority As String = "UPDATE VarLists SET PriorityList = '" & selectedPrioritiesStr & "' WHERE ListName = '" & Replace(txtListName.Text, "'", "''") & "'"
        Dim strConnectionPriority As String = "Data Source=" & Session("PutFdrvRun") & "IFsVar" & extension
        Dim objConnPriority As New SQLiteConnection(strConnectionPriority)
        Dim objCommandPriority As New SQLiteCommand(sqlUpdatePriority, objConnPriority)
        objConnPriority.Open()
        objCommandPriority.ExecuteNonQuery()
        objConnPriority.Close()
        '--- End ---

        Dim inFile As String
        Dim OutFile As String
        inFile = Session("PutFdrvRun") & "IFsVar" & extension
        OutFile = DataFileDir & "IFsVar" & extension
        'jrs 2025/11/14 FileCopyCheck(inFile, OutFile)
        'The file in RUNFILES is not getting its datetime updated at this point, need to force the write
        FileCopy(inFile, OutFile)
        Session("JSNeedsUpdate") = True
        'jrs 2023/05/19
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

    Public Sub confirmedSave_Click(sender As Object, e As EventArgs) Handles confirmedSave.Click
        Save_Changes()
        UndoUserMadeChanges()

    End Sub

    Protected Sub AddNewList_Click(sender As Object, e As EventArgs) Handles AddNewList.Click
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Varsel.aspx")
    End Sub

    Private Sub LoadCategories()
        'jrs 2005/04/28
        Dim rsL As DataTable
        Dim str As String
        str = "SELECT DISTINCT Category FROM VarLists"
        'JRS 2005/11/16
        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            str = str + " WHERE HisCompEmpFor = False AND Type = 'V'" 'jrs 2006/01/18 'ListName NOT IN (SELECT V1.ListName FROM VarLists as V1, VarLists as V2 WHERE V1.ListName = V2.ListName AND V1.History = True AND V2.History = False)"
        Else
            str = str + " WHERE HistoryplusForecast = False AND Type = 'V'" 'jrs 2013/09/06 added and HistoryPlusForecast = False
        End If

        'jrs 2020/12/03 SQL is not working with LIKE ""*" & sFilter & "*"""
        Dim sFilter As String = ""
        'jrs 2019/03/27

        SetDataTablefromAccess(Session("PutFdrvRun") + "IFsVar" & extension, str, "", rsL)

        'jrs 2012/10/26
        Dim CatCol As Collection
        CatCol = New Collection
        Dim x As Object, i As Integer, j As Integer
        Do While (Not rsL.Rows.Count = j)
            'jrs 2020/12/03
            If sFilter = "" OrElse rsL.Rows(j).Item("PriorityList").ToString.Contains(sFilter) Then
                str = IIf(IsDBNull(rsL.Rows(j).Item("Category")), "", rsL.Rows(j).Item("Category"))
                x = Split(rsL.Rows(j).Item("Category"), ",")
                'On Error Resume Next
                For i = 0 To UBound(x)
                    If Not CatCol.Contains(Trim(x(i))) Then
                        CatCol.Add(Trim(x(i)), Trim(x(i)))
                    End If
                Next i
                'On Error GoTo 0
                If UBound(x) = -1 Then
                    CatCol.Add(Trim(str), Trim(str))
                End If
            End If

            j = j + 1
        Loop

        'jrs 2012/10/26 need to sort categories
        Dim Cat As Object
        Dim CatArr() As String
        ReDim CatArr(CatCol.Count)
        i = 0
        For Each Cat In CatCol
            i = i + 1
            CatArr(i) = Cat
        Next Cat
        'lstCategories.Sorted = True
        QuickSort(CatArr, 1, CatCol.Count)

        i = 0
        'jrs 2012/03/29
        Dim bReloadCats As Boolean
        bReloadCats = (Not Session("PDFlexCategoriesIdxs") Is Nothing)
        'jrs 2013/03/20 ReDim Session("PDFlexCategoriesIdxs")(rsL.Rows.Count)
        ReDim Session("PDFlexCategoriesIdxs")(CatCol.Count)
        Session("CategoryIds") = New Collection
        For i = 1 To CatCol.Count
            CType(Session("CategoryIds"), Collection).Add(i, CStr(CatArr(i)))
        Next i
    End Sub

    Protected Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        ExportListsToTransmission()
    End Sub

    Public Sub ExportListsToTransmission()
        If LstListName.SelectedValue = Nothing Or LstListName.Items.Count = 0 Then
            Response.Write("<script>alert('Please Select Lists to Export');</script>")
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To LstListName.Items.Count - 1
            If LstListName.Items.Item(i).Selected Then
                AddListItemToTransmission(LstListName.Items.Item(i).Text)
            End If
        Next i
        Response.Write("<script>alert('Successfully Exported Selected Values to VarListTransmission.db in your RUNFILES folder');</script>")
    End Sub
    Sub AddListItemToTransmission(ItemToAdd As String)
        Dim VariableList As DataTable
        'jrs 2025/12/16 added replace ' with '' to avoid SQL errors
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT * FROM VarLists WHERE ListName = '" & Replace(ItemToAdd, "'", "''") & "'", "", VariableList)
        If VariableList.Rows.Count = 0 Then
            ThrowError("There were no values found from your selection of vars. There maybe an issue with your IFsVar.mdb")
            Exit Sub
        End If

        Dim SourceConnectionStr As String = "Data Source=" & Session("PutFdrvRun") & "IFsVar" & extension
        Dim SourceConnection As New SQLiteConnection(SourceConnectionStr)

        Dim TargetConnectionStr As String = "Data Source=" & Session("PutFdrvRun") & "VarListTransmission" & extension
        Dim TargetConnection As New SQLiteConnection(TargetConnectionStr)


        SourceConnection.Open()
        TargetConnection.Open()

        Dim sqlCommand As SQLiteCommand
        Dim SQLSelectSource As String
        Dim SQLInsertTarget As String
        SQLSelectSource = "SELECT * FROM VarLists WHERE ListName = '" & Replace(ItemToAdd, "'", "''") & "'"
        sqlCommand = New SQLiteCommand(SQLSelectSource, SourceConnection)
        Dim DataReader As SQLiteDataReader
        DataReader = sqlCommand.ExecuteReader()

        Dim column As Integer
        While DataReader.Read()
            SQLInsertTarget = "INSERT INTO VarLists SELECT "
            For column = 0 To DataReader.FieldCount - 1
                If DataReader.GetValue(column).GetType.Name <> "String" Then
                    If IsDBNull(DataReader.GetValue(column)) Then
                        If column = DataReader.GetOrdinal("CreationDate") Or column = DataReader.GetOrdinal("ChangeDate") Then
                            SQLInsertTarget = SQLInsertTarget & "'" & CType(Date.Now, DateTime) & "', "
                        ElseIf column = DataReader.GetOrdinal("NumDecimals") Then
                            SQLInsertTarget = SQLInsertTarget & "2, "
                        Else
                            SQLInsertTarget = SQLInsertTarget & "'', "
                        End If
                    ElseIf DataReader.GetValue(column).GetType.Name = "DateTime" Then
                        'jrs 2024/07/22 SQLInsertTarget = SQLInsertTarget & "'" & DataReader.GetDateTime(column) & "', "
                        SQLInsertTarget = SQLInsertTarget & "'" & DataReader.GetDateTime(column).ToString("yyyy-MM-dd HH:mm:ss") & "', "
                    Else
                        SQLInsertTarget = SQLInsertTarget & "" & DataReader.GetValue(column) & ", "
                    End If
                Else
                    'jrs 2025/12/16 added Replace to avoid SQL errors
                    SQLInsertTarget = SQLInsertTarget & "'" & Replace(DataReader.GetValue(column), "'", "''") & "', "
                End If

            Next
            SQLInsertTarget = SQLInsertTarget.TrimEnd(" ")
            'jrs 2025/12/16 added replace to avoid SQL errors
            SQLInsertTarget = SQLInsertTarget.TrimEnd(",") & " WHERE NOT EXISTS (SELECT * FROM VarLists WHERE ListName = '" & Replace(ItemToAdd, "'", "''") & "' AND VarName = '" & DataReader.GetValue(1) & "');"
            sqlCommand = New SQLiteCommand(SQLInsertTarget, TargetConnection)
            sqlCommand.ExecuteNonQuery()
            If DataReader.GetValue(DataReader.GetOrdinal("Formula")) Then 'if Formula
                Dim formulaID As Integer
                formulaID = DataReader.GetValue(DataReader.GetOrdinal("FormulaId"))
                SQLSelectSource = "SELECT * FROM VarListsFormulas WHERE FormulaId = " & formulaID & ""
                sqlCommand = New SQLiteCommand(SQLSelectSource, SourceConnection)
                Dim FormulaDataReader As SQLiteDataReader
                FormulaDataReader = sqlCommand.ExecuteReader()
                While FormulaDataReader.Read()
                    SQLInsertTarget = "INSERT INTO VarListsFormulas SELECT "
                    For column = 0 To FormulaDataReader.FieldCount - 1
                        If FormulaDataReader.GetValue(column).GetType.Name <> "String" Then
                            If IsDBNull(FormulaDataReader.GetValue(column)) Then
                                SQLInsertTarget = SQLInsertTarget & "' ', "
                            ElseIf FormulaDataReader.GetValue(column).GetType.Name = "DateTime" Then
                                SQLInsertTarget = SQLInsertTarget & "'" & FormulaDataReader.GetDateTime(column) & "', "
                            Else
                                SQLInsertTarget = SQLInsertTarget & "" & FormulaDataReader.GetValue(column) & ", "
                            End If
                        Else
                            SQLInsertTarget = SQLInsertTarget & "'" & FormulaDataReader.GetValue(column) & "', "
                        End If
                    Next
                    SQLInsertTarget = SQLInsertTarget.TrimEnd(" ")
                    SQLInsertTarget = SQLInsertTarget.TrimEnd(",") & " WHERE NOT EXISTS (SELECT * FROM VarListsFormulas WHERE FormulaId = '" & formulaID & "' AND CodeVar = '" & FormulaDataReader.GetValue(4) & "');"
                    sqlCommand = New SQLiteCommand(SQLInsertTarget, TargetConnection)
                    sqlCommand.ExecuteNonQuery()
                End While

            End If
        End While

        SourceConnection.Close()
        TargetConnection.Close()
    End Sub

    Sub ThrowError(msg)
        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub


    Protected Sub Delete_Click(sender As Object, e As EventArgs) Handles DeleteList.Click
        For i = 0 To LstListName.Items.Count - 1
            If LstListName.Items(i).Selected Then
                Dim ConnectionStr As String = "Data Source=" & Session("PutFdrvRun") & "IFsVar" & extension
                Dim Connection As New SQLiteConnection(ConnectionStr)
                Connection.Open()
                Dim VarListRecords As DataTable
                Dim sqlCommand As SQLiteCommand
                Dim SQLDelete As String
                Dim FormulasToDelete(0) As String
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT Formula, FormulaId FROM VarLists WHERE ListName = '" & LstListName.Items(i).Text & "'", "", VarListRecords)
                For j = 0 To VarListRecords.Rows.Count - 1
                    If VarListRecords.Rows(j).Item("Formula") Then
                        FormulasToDelete(FormulasToDelete.Length - 1) = VarListRecords.Rows(j).Item("FormulaId")
                        ReDim Preserve FormulasToDelete(FormulasToDelete.Length)
                    End If
                Next


                SQLDelete = "DELETE FROM VarLists WHERE ListName = '" & LstListName.Items(i).Text & "'"
                sqlCommand = New SQLiteCommand(SQLDelete, Connection)
                sqlCommand.ExecuteNonQuery()
                If FormulasToDelete(0) <> "" Then
                    For j = 0 To FormulasToDelete.Length - 2
                        SQLDelete = "DELETE FROM VarListsFormulas WHERE FormulaId = " & CType(FormulasToDelete(j), String)
                        sqlCommand = New SQLiteCommand(SQLDelete, Connection)
                        sqlCommand.ExecuteNonQuery()
                    Next
                End If
                UserMadeChanges()
                Session("DeletedValues") = "True"
            End If
        Next
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub
    Protected Sub DeleteVariable_Click(sender As Object, e As EventArgs) Handles DeleteVariable.Click

        For i = 0 To LstListName.Items.Count - 1
            If LstListName.Items(i).Selected Then
                Dim VarListRecords As DataTable
                Dim FormulasToDelete(0) As String
                For j = 0 To LstVariableList.Items.Count - 1
                    If LstVariableList.Items(j).Selected Then
                        Dim ConnectionStr As String = "Data Source=" & Session("PutFdrvRun") & "IFsVar" & extension
                        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar" & extension, "SELECT Formula, FormulaId FROM VarLists WHERE ListName = '" & LstListName.Items(i).Text & "' " & " AND VarName = '" & LstVariableList.Items(j).Text & "'", "", VarListRecords)
                        For k = 0 To VarListRecords.Rows.Count - 1
                            If VarListRecords.Rows(k).Item("Formula") Then
                                FormulasToDelete(FormulasToDelete.Length - 1) = VarListRecords.Rows(k).Item("FormulaId")
                                ReDim Preserve FormulasToDelete(FormulasToDelete.Length)
                            End If
                        Next
                        Dim Connection As New SQLiteConnection(ConnectionStr)
                        Connection.Open()
                        Dim sqlCommand As SQLiteCommand
                        Dim SQLDelete As String
                        SQLDelete = "DELETE FROM VarLists WHERE ListName = '" & LstListName.Items(i).Text & "' " & " AND VarName = '" & LstVariableList.Items(j).Text & "'"
                        sqlCommand = New SQLiteCommand(SQLDelete, Connection)
                        sqlCommand.ExecuteNonQuery()
                        If FormulasToDelete(0) <> "" Then
                            For k = 0 To FormulasToDelete.Length - 2
                                SQLDelete = "DELETE FROM VarListsFormulas WHERE FormulaId = " & CType(FormulasToDelete(k), String)
                                sqlCommand = New SQLiteCommand(SQLDelete, Connection)
                                sqlCommand.ExecuteNonQuery()
                            Next
                        End If
                        UserMadeChanges()
                        Session("DeletedValues") = "True"
                    End If
                Next
            End If
        Next
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub ImportListButton_Click(sender As Object, e As EventArgs) Handles ImportListButton.Click
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
            'jrs 2024/07/22 
            Dim bReload As Boolean = False
            If FileName.Contains("VarListTransmission") = True Then
                Try
                    MyFile.SaveAs(Session("PutFdrvRun") & FileName)

                    Dim ConnectionStr2 As String = "Data Source=" & Session("PutFdrvRun") & "\" & FileName
                    Dim Connection2 As New SQLiteConnection(ConnectionStr2)

                    Connection2.Open()

                    Dim sqlCommand As SQLiteCommand
                    Dim SQLInsert As String
                    Dim SQLAttach As String

                    SQLAttach = "ATTACH DATABASE '" & Session("PutFdrvRun") & "IFsVar" & extension & "' AS targetDB"
                    sqlCommand = New SQLiteCommand(SQLAttach, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    'jrs 2023/06/27 *** start ***
                    'delete what already exists
                    'jrs 2025/09/25
                    'Case Insensitive, adding UPPER
                    Dim sqlDELETE As String
                    sqlDELETE = "DELETE FROM targetDB.VarListsFormulas WHERE FormulaID IN (SELECT LT.FormulaId FROM targetDB.VarLists LT, VarLists L WHERE UPPER(LT.ListName) = UPPER(L.ListName))"
                    sqlCommand = New SQLiteCommand(sqlDELETE, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    sqlDELETE = "DELETE FROM targetDB.VarListsFormulasHdr WHERE FormulaID IN (SELECT LT.FormulaId FROM targetDB.VarLists LT, VarLists L WHERE UPPER(LT.ListName) = UPPER(L.ListName))"
                    sqlCommand = New SQLiteCommand(sqlDELETE, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    sqlDELETE = "DELETE FROM targetDB.VarListsIndices WHERE UPPER(ListName) IN (SELECT UPPER(L.ListName) FROM VarLists L)"
                    sqlCommand = New SQLiteCommand(sqlDELETE, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    sqlDELETE = "DELETE FROM targetDB.VarLists WHERE UPPER(ListName) IN (SELECT UPPER(L.ListName) FROM VarLists L)"
                    sqlCommand = New SQLiteCommand(sqlDELETE, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    'check if formula id already exist, if it does change it
                    Dim SQLSelect As String
                    SQLSelect = "SELECT L.FormulaId FROM VarLists L, targetDB.VarLists FT WHERE L.FormulaId = FT.FormulaId AND L.FormulaId > 0"
                    sqlCommand = New SQLiteCommand(SQLSelect, Connection2)
                    Dim SQLreader = sqlCommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        'Need to update FormulaIds
                        SQLreader.Close()
                        'Find max id in target
                        SQLSelect = "SELECT MAX(FormulaId) FROM targetDB.VarLists"
                        sqlCommand = New SQLiteCommand(SQLSelect, Connection2)
                        SQLreader = sqlCommand.ExecuteReader()
                        'jrs 2024/07/22
                        SQLreader.Read()
                        Dim MaxFormulaId As Integer = SQLreader(0)
                        SQLreader.Close()
                        'Find min id in transmission file
                        SQLSelect = "SELECT MIN(FormulaId) FROM VarLists WHERE FormulaId > 0"
                        sqlCommand = New SQLiteCommand(SQLSelect, Connection2)
                        SQLreader = sqlCommand.ExecuteReader()
                        'jrs 2024/07/22
                        SQLreader.Read()
                        Dim MinFormulaId As Integer = SQLreader(0)
                        SQLreader.Close()
                        Dim Diff As Integer = MaxFormulaId - MinFormulaId + 1
                        'Update all codes in transmission file by diff
                        Dim sqlUPD As String
                        sqlUPD = "UPDATE VarListsFormulas SET FormulaId = FormulaId + " & Diff.ToString
                        sqlCommand = New SQLiteCommand(sqlUPD, Connection2)
                        sqlCommand.ExecuteNonQuery()

                        sqlUPD = "UPDATE VarListsFormulasHdr SET FormulaId = FormulaId + " & Diff.ToString
                        sqlCommand = New SQLiteCommand(sqlUPD, Connection2)
                        sqlCommand.ExecuteNonQuery()

                        sqlUPD = "UPDATE VarLists SET FormulaId = FormulaId + " & Diff.ToString & " WHERE FormulaId>0"
                        sqlCommand = New SQLiteCommand(sqlUPD, Connection2)
                        sqlCommand.ExecuteNonQuery()
                    Else
                        SQLreader.Close()
                    End If
                    'jrs 2023/06/27 ** end ***

                    SQLInsert = "INSERT INTO targetDB.VarLists SELECT * FROM VarLists"
                    sqlCommand = New SQLiteCommand(SQLInsert, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    SQLInsert = "INSERT INTO targetDB.VarListsFormulas SELECT * FROM VarListsFormulas"
                    sqlCommand = New SQLiteCommand(SQLInsert, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    SQLInsert = "INSERT INTO targetDB.VarListsFormulasHdr SELECT * FROM VarListsFormulasHdr"
                    sqlCommand = New SQLiteCommand(SQLInsert, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    SQLInsert = "INSERT INTO targetDB.VarListsIndices SELECT * FROM VarListsIndices"
                    sqlCommand = New SQLiteCommand(SQLInsert, Connection2)
                    sqlCommand.ExecuteNonQuery()

                    Session("ImportedValues") = "True"
                    Response.Write("<script> alert('values Successfully Imported') </script>")
                    UserMadeChanges()
                    SQLSelect = "SELECT ListName FROM VarLists ORDER BY ListName LIMIT 1"
                    sqlCommand = New SQLiteCommand(SQLSelect, Connection2)
                    SQLreader = sqlCommand.ExecuteReader()
                    While SQLreader.Read()
                        Session("SelectedLists") = New String() {}
                        Try
                            ReDim Session("SelectedLists")(1)
                            Session("SelectedLists")(0) = SQLreader("ListName").ToString()
                        Catch exeception As Exception
                            Exit Try
                        End Try
                    End While
                    Connection2.Close()
                    'jrs 2024/07/22 Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
                    bReload = True
                Catch exception As Exception
                    'jrs 2024/07/22
                    Dim s As String
                    s = exception.Message.ToString()
                    Response.Write("<script>alert('There was the following issue: " & s & "')</script>")
                End Try
            Else
                Response.Write("<script>alert('Your file must be a VarListsTransmission')</script>")
            End If
            'jrs 2024/07/21
            If bReload Then
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            End If
        End If

    End Sub

    'jrs 2025/02/03
    Protected Sub cmdEditUnits_Click(sender As Object, e As EventArgs) Handles cmdEditUnits.Click
        If LstListName.SelectedIndex < 0 Then
            Response.Write("<script>alert('You have to select a list name.')</script>")
            Exit Sub
        End If

        Dim rs As DataTable
        Dim sDefUnits As String
        Dim SQLSelect As String
        Dim sqlCommand As SQLiteCommand
        Dim ConnectionStr2 As String = "Data Source=" & Session("PutFdrvRun") & "\ifsVar.db"
        Dim Connection2 As New SQLiteConnection(ConnectionStr2)
        Connection2.Open()

        SQLSelect = "SELECT DISTINCT DisplayUnits FROM VarLists WHERE ListName = """ & LstListName.SelectedValue & """"
        sqlCommand = New SQLiteCommand(SQLSelect, Connection2)
        Dim SQLreader = sqlCommand.ExecuteReader()
        While SQLreader.Read()
            Try
                If Not IsDBNull(SQLreader(0)) And sDefUnits = "" Then
                    sDefUnits = SQLreader(0)
                Else
                    sDefUnits = ""
                End If
            Catch exeception As Exception
                Exit Try
            End Try
        End While
        SQLreader.Close()

        If sDefUnits = "" Then
            SQLSelect = "SELECT DISTINCT v.[Display Units] FROM VarLists l, ifsVar v WHERE l.VarName = v.Name AND " &
            "l.ListName = """ & LstListName.SelectedValue & """"
            sqlCommand.CommandText = SQLSelect
            SQLreader = sqlCommand.ExecuteReader()

            While SQLreader.Read()
                Try
                    If Not IsDBNull(SQLreader(0)) And sDefUnits = "" Then
                        sDefUnits = SQLreader(0)
                    Else
                        sDefUnits = ""
                    End If
                Catch exeception As Exception
                    Exit Try
                End Try
            End While
            SQLreader.Close()
        End If

        If sDefUnits = "" Then
            SQLSelect = "SELECT DISTINCT v.Units FROM VarLists l, VarListsFormulasHdr v WHERE l.FormulaId = v.FormulaId AND " &
            "l.ListName = """ & LstListName.SelectedValue & """"
            sqlCommand.CommandText = SQLSelect
            SQLreader = sqlCommand.ExecuteReader()

            While SQLreader.Read()
                Try
                    If Not IsDBNull(SQLreader(0)) And sDefUnits = "" Then
                        sDefUnits = SQLreader(0)
                    Else
                        sDefUnits = ""
                    End If
                Catch exeception As Exception
                    Exit Try
                End Try
            End While
            SQLreader.Close()
        End If

        lblInstrucions.Text = "Please enter the Units to use for this List (Y axis display)"
        lblInstrucions.Visible = True
        txtUnits.Text = sDefUnits
        txtUnits.Visible = True
        cmdOkUnits.Visible = True

        AddNewList.Visible = False
        Export.Visible = False
        DeleteList.Visible = False
        DeleteVariable.Visible = False
        ImportListButton.Visible = False
        ImportList.Visible = False
        txtCategory.Visible = False
        btnUpdateCategory.Visible = False
        lstCategoryList.Visible = False
        lblCat.Visible = False
        LstVariableList.Visible = False
        cmdUpdLstNm.Visible = False
        cmdEditUnits.Visible = False

        'Hide Save button, which is javascript
        Dim sJs As String = "<script>document.getElementById('Save').style.visibility = 'hidden';</script>"
        'ClientScript.RegisterClientScriptBlock(Me.GetType(), "HideSave", sJs, True)

        Connection2.Close()
    End Sub

    Private Sub cmdOkUnits_Click(sender As Object, e As EventArgs) Handles cmdOkUnits.Click
        Dim sUnits As String
        sUnits = txtUnits.Text
        If sUnits = "" Then Exit Sub

        Dim sql As String

        sql = "UPDATE VarLists SET DisplayUnits = """ & sUnits
        sql = sql & """, ChangeDate = '" & System.DateTime.Now().ToString("yyyy-MM-dd HH:mm:ss") & "' WHERE Type = 'V' AND ListName = """
        sql = sql & LstListName.SelectedValue & """"

        Dim ConnectionStr2 As String = "Data Source=" & Session("PutFdrvRun") & "\ifsVar.db"
        Dim Connection2 As New SQLiteConnection(ConnectionStr2)
        Connection2.Open()

        Dim sqlCommand As SQLiteCommand
        sqlCommand = Connection2.CreateCommand
        sqlCommand.CommandText = sql

        Try
            Dim rowsAff = sqlCommand.ExecuteNonQuery()

            If rowsAff > 0 Then
                Response.Write("<script>alert('Update successful!')</script>")
                UserMadeChanges()
            End If
        Catch exeception As Exception
            Response.Write("<script>alert('List NOT Updated!')</script>")
        Finally
            Connection2.Close()
        End Try

        txtUnits.Visible = False
        cmdOkUnits.Visible = False

        AddNewList.Visible = True
        Export.Visible = True
        DeleteList.Visible = True
        DeleteVariable.Visible = True
        ImportListButton.Visible = True
        ImportList.Visible = True
        txtCategory.Visible = True
        btnUpdateCategory.Visible = True
        lstCategoryList.Visible = True
        lblCat.Visible = True
        LstVariableList.Visible = True
        cmdUpdLstNm.Visible = True
        cmdEditUnits.Visible = True


    End Sub

    Private Sub cmdUpdLstNm_Click(sender As Object, e As EventArgs) Handles cmdUpdLstNm.Click
        If LstListName.SelectedIndex < 0 Then
            Response.Write("<script>alert('You have to select a list name.')</script>")
            Exit Sub
        End If

        lblInstrucions.Text = "Please enter the new name for the variable list"
        lblInstrucions.Visible = True
        txtNewLstNm.Text = ""
        txtNewLstNm.Visible = True
        cmdOkNewLstNm.Visible = True

        AddNewList.Visible = False
        Export.Visible = False
        DeleteList.Visible = False
        DeleteVariable.Visible = False
        ImportListButton.Visible = False
        ImportList.Visible = False
        txtCategory.Visible = False
        btnUpdateCategory.Visible = False
        lstCategoryList.Visible = False
        lblCat.Visible = False
        LstVariableList.Visible = False
        cmdUpdLstNm.Visible = False
        cmdEditUnits.Visible = False
    End Sub

    Private Sub cmdOkNewLstNm_Click(sender As Object, e As EventArgs) Handles cmdOkNewLstNm.Click
        Dim rs As DataTable
        Dim sqlCommand As SQLiteCommand
        Dim ConnectionStr2 As String = "Data Source=" & Session("PutFdrvRun") & "\ifsVar.db"
        Dim Connection2 As New SQLiteConnection(ConnectionStr2)
        Connection2.Open()

        Dim sql As String
        sql = "SELECT COUNT(*) FROM VarLists "
        sql = sql & "WHERE Type = 'V' AND ListName = """
        sql = sql & txtNewLstNm.Text & """"

        sqlCommand = New SQLiteCommand(sql, Connection2)
        Dim SQLreader = sqlCommand.ExecuteReader()
        While SQLreader.Read()
            Try
                If SQLreader(0) > 0 Then
                    Response.Write("<script>alert('The List Name already exists. Please select a different one.')</script>")
                    SQLreader.Close()
                    Connection2.Close()
                    Exit Sub
                End If
            Catch exeception As Exception
                Exit Try
            End Try
        End While
        SQLreader.Close()


        sql = "UPDATE VarLists SET ListName = """ & txtNewLstNm.Text
        sql = sql & """, ChangeDate = '" & System.DateTime.Now().ToString("yyyy-MM-dd HH:mm:ss") & "' WHERE Type = 'V' AND ListName = """
        sql = sql & LstListName.SelectedValue & """"

        sqlCommand = Connection2.CreateCommand
        sqlCommand.CommandText = sql

        Try
            Dim rowsAff = sqlCommand.ExecuteNonQuery()

            If rowsAff > 0 Then
                Response.Write("<script>alert('Update successful!')</script>")
                'Reload lists and focus on selected one

                UserMadeChanges()
            End If
        Catch exeception As Exception
            Response.Write("<script>alert('List NOT Updated!')</script>")
        Finally
            Connection2.Close()
        End Try

        txtNewLstNm.Visible = False
        cmdOkNewLstNm.Visible = False

        AddNewList.Visible = True
        Export.Visible = True
        DeleteList.Visible = True
        DeleteVariable.Visible = True
        ImportListButton.Visible = True
        ImportList.Visible = True
        txtCategory.Visible = True
        btnUpdateCategory.Visible = True
        lstCategoryList.Visible = True
        lblCat.Visible = True
        LstVariableList.Visible = True
        cmdUpdLstNm.Visible = True
        cmdEditUnits.Visible = True

        ReDim Session("SelectedLists")(1)
        Session("SelectedLists")(0) = txtNewLstNm.Text
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub

    Protected Sub Exit_Click1(sender As Object, e As EventArgs) Handles [Exit].Click

    End Sub
End Class


'cdv 2022/05/19 ****END****