Option Strict Off
Option Explicit On

Imports System.Math
Imports IFs.Data
'jam 2021/10/10
Imports Microsoft.AspNet.FriendlyUrls
Imports System.Drawing
Imports Snappy

Partial Class frm_SDGset
    Inherits Page

    'jrs 2017/05/09
    'Sustainable Development Goal Set Display
    'Implemented by José Solórzano
    Dim dbIFsVar As String

    'jrs 2021/04/12
    Dim FilesCol As New Collection
    Dim dfCol As New Collection


    Private Sub LoadRegions()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        cmb_Country.Items.Clear()
        Dim EleLoop As Integer
        For EleLoop = 1 To MaxVal(1) - 1 'Exclude 'ALL'
            cmb_Country.Items.Add(GRPS$(1, EleLoop))
        Next EleLoop
    End Sub

    Private Sub LoadFiles(ByVal FileList() As String, ByVal cbo As DropDownList)
        Dim i As Integer
        For i = 0 To UBound(FileList)
            cbo.Items.Add(FileList(i))
            cboSce2.Items.Add(FileList(i))
        Next i
    End Sub

    Private Sub LoadGroups()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        cmb_Country.Items.Clear()
        Dim db As String
        Dim valtbl As DataTable
        db = Session("IFs_MDB2Active")
        'jrs 2024/09/13 added UPPER to keep the same order across entire GUI
        SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew & " ORDER BY UPPER(GroupName)", "GroupName", valtbl)
        Dim idx As Integer = 0
        While Not valtbl.Rows.Count = idx
            cmb_Country.Items.Add(valtbl.Rows(idx).Item("GroupName"))
            idx = idx + 1
        End While

    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''     jam 2024/11/17    Loading Goals Combo
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>

    Private Sub LoadGoals()

        Dim dbSDG As String
        Dim i As Integer
        i = 0

        dbSDG = DataFileDir & "IFsMDG.mdb"
        Dim rsGoals As DataTable
        SetDataTablefromAccess(dbSDG, "SELECT * FROM Goals WHERE GoalSetId=2 ORDER BY ID", "", rsGoals)

        Session("MaxGoal") = rsGoals.Rows.Count
        ReDim Session("vGoals")(Session("MaxGoal"))

        cmb_Goal.Items.Clear()

        Do While i < Session("MaxGoal")
            cmb_Goal.Items.Add(rsGoals.Rows(i).Item("Name"))
            Session("vGoals")(i) = rsGoals.Rows(i).Item("Name")
            i = i + 1
        Loop

        If Session("SDGsetcmbGoalIdx") Is Nothing Then
            Session("SDGsetcmbGoalIdx") = 1
        End If
        cmb_Goal.SelectedIndex = Session("SDGsetcmbGoalIdx") - 1

    End Sub
    Private Sub LoadTargets()
        'This procedure loads a regression for each member of the variable list vs GDPPCP (linear and log are tested for best fit)
        Dim dbSDG As String

        dbSDG = DataFileDir & "IFsMDG.mdb"
        Dim rsTargets As DataTable

        'jam 2024/11/18
        'SetDataTablefromAccess(dbSDG, "SELECT * FROM Indicators WHERE GoalSetId=2 AND IFsVar > ''  ORDER BY ID", "", rsTargets)


        SetDataTablefromAccess(dbSDG, "SELECT * FROM Indicators WHERE GoalSetId=2 AND IFsVar > '' AND GoalID = " & Session("SDGsetcmbGoalIdx") & " ORDER BY ID", "", rsTargets)




        'Load data for each other variable in given year
        ReDim Session("GoalId")(rsTargets.Rows.Count)
        ReDim Session("TargetId")(rsTargets.Rows.Count)
        ReDim Session("sName")(rsTargets.Rows.Count)
        ReDim Session("Description")(rsTargets.Rows.Count)
        ReDim Session("IFsVarName")(rsTargets.Rows.Count)
        ReDim Session("Dim2")(rsTargets.Rows.Count)
        ReDim Session("Transform")(rsTargets.Rows.Count)
        ReDim Session("IFsVarName2")(rsTargets.Rows.Count)
        ReDim Session("Var2Dim2")(rsTargets.Rows.Count)
        ReDim Session("Target")(rsTargets.Rows.Count)
        ReDim Session("VarMaxs")(rsTargets.Rows.Count)
        ReDim Session("VarMins")(rsTargets.Rows.Count)
        ReDim Session("TargetYear")(rsTargets.Rows.Count)
        'jrs 2017/06/15
        ReDim Session("TargetIsRelative")(rsTargets.Rows.Count)
        ReDim Session("Valence")(rsTargets.Rows.Count)

        Dim j As Integer
        j = 0

        'list of all variables
        Do While j < rsTargets.Rows.Count

            j = j + 1
            Session("GoalId")(j) = rsTargets.Rows(j - 1).Item("GoalId")
            Session("TargetId")(j) = rsTargets.Rows(j - 1).Item("TargetId")
            Session("sName")(j) = rsTargets.Rows(j - 1).Item("Name")
            Session("Description")(j) = rsTargets.Rows(j - 1).Item("Description")
            Session("IFsVarName")(j) = rsTargets.Rows(j - 1).Item("IFsVar")
            Session("Dim2")(j) = rsTargets.Rows(j - 1).Item("IFsVar Dim2")
            Session("Transform")(j) = rsTargets.Rows(j - 1).Item("Transform")
            Session("IFsVarName2")(j) = rsTargets.Rows(j - 1).Item("Var2")
            Session("Var2Dim2")(j) = rsTargets.Rows(j - 1).Item("Var2Dim2")
            Session("Target")(j) = rsTargets.Rows(j - 1).Item("TargetValue")
            Session("VarMaxs")(j) = rsTargets.Rows(j - 1).Item("Maximum")
            Session("VarMins")(j) = rsTargets.Rows(j - 1).Item("Minimum")
            Session("TargetYear")(j) = rsTargets.Rows(j - 1).Item("Target Year")
            'jrs 2017/06/15
            If IsDBNull(rsTargets.Rows(j - 1).Item("TargetValueIsRelative")) Then
                Session("TargetIsRelative")(j) = False
            Else
                Session("TargetIsRelative")(j) = rsTargets.Rows(j - 1).Item("TargetValueIsRelative")
            End If
            If IsDBNull(rsTargets.Rows(j - 1).Item("Valence")) Then
                Session("Valence")(j) = False
            Else
                Session("Valence")(j) = rsTargets.Rows(j - 1).Item("Valence")
            End If
        Loop


    End Sub

    'jrs 2017/07/12
    Private Sub LoadTargetsH()
        'This procedure loads a regression for each member of the variable list vs GDPPCP (linear and log are tested for best fit)
        Dim dbSDG As String
        dbSDG = DataFileDir & "IFsMDG.mdb"
        Dim rsTargets As DataTable
        'jam 2024/11/18
        'SetDataTablefromAccess(dbSDG, "SELECT * FROM Indicators WHERE GoalSetId=2 AND IFsVar is Null ORDER BY ID", "", rsTargets)


        SetDataTablefromAccess(dbSDG, "SELECT * FROM Indicators WHERE GoalSetId=2 AND IFsVar is Null AND GoalID = " & Session("SDGsetcmbGoalIdx") & " ORDER BY ID", "", rsTargets)


        'Load data for each other variable in given year
        ReDim Session("GoalIdH")(rsTargets.Rows.Count)
        ReDim Session("TargetIdH")(rsTargets.Rows.Count)
        ReDim Session("sNameH")(rsTargets.Rows.Count)
        ReDim Session("DescriptionH")(rsTargets.Rows.Count)

        ReDim Session("VarMaxsH")(rsTargets.Rows.Count)
        ReDim Session("VarMinsH")(rsTargets.Rows.Count)
        'jrs 2017/07/21
        ReDim Session("TargetH")(rsTargets.Rows.Count)
        ReDim Session("TargetYearH")(rsTargets.Rows.Count)
        ReDim Session("TargetIsRelativeH")(rsTargets.Rows.Count)
        ReDim Session("ValenceH")(rsTargets.Rows.Count)
        'jrs 2017/08/25
        ReDim Session("VarMaxsH")(rsTargets.Rows.Count)
        ReDim Session("VarMinsH")(rsTargets.Rows.Count)

        Dim j As Integer
        j = 0

        'list of all variables
        Do While j < rsTargets.Rows.Count

            j = j + 1
            Session("GoalIdH")(j) = rsTargets.Rows(j - 1).Item("GoalId")
            Session("TargetIdH")(j) = rsTargets.Rows(j - 1).Item("TargetId")
            Session("sNameH")(j) = rsTargets.Rows(j - 1).Item("Name")
            Session("DescriptionH")(j) = rsTargets.Rows(j - 1).Item("Description")

            Session("VarMaxsH")(j) = rsTargets.Rows(j - 1).Item("Maximum")
            Session("VarMinsH")(j) = rsTargets.Rows(j - 1).Item("Minimum")

            Session("TargetH")(j) = rsTargets.Rows(j - 1).Item("TargetValue")
            Session("TargetYearH")(j) = rsTargets.Rows(j - 1).Item("Target Year")
            If IsDBNull(rsTargets.Rows(j - 1).Item("TargetValueIsRelative")) Then
                Session("TargetIsRelativeH")(j) = False
            Else
                Session("TargetIsRelativeH")(j) = rsTargets.Rows(j - 1).Item("TargetValueIsRelative")
            End If
            If IsDBNull(rsTargets.Rows(j - 1).Item("Valence")) Then
                Session("ValenceH")(j) = False
            Else
                Session("ValenceH")(j) = rsTargets.Rows(j - 1).Item("Valence")
            End If
        Loop
    End Sub

    Private Sub LoadTargetValues(VarIdx As Integer, curRow As Integer)
        Dim TarVal As Object
        Dim TarYr As Integer
        If Not IsDBNull(Session("TargetYear")(VarIdx)) Then
            TarYr = Session("TargetYear")(VarIdx)
            If TarYr = 0 Then
                TarYr = 2030
            End If
        Else
            TarYr = 2030
        End If

        'jrs 2017/06/15
        TarVal = DBNull.Value
        If Session("TargetIsRelative")(VarIdx) Then
            If Not IsDBNull(Session("Target")(VarIdx)) Then
                If Val(Cmb_Year.Text) >= TarYr And TarYr > FirstYearForecast Then
                    TarVal = Session("Baseval")(VarIdx) * Val(Session("Target")(VarIdx))
                Else
                    TarVal = Session("Baseval")(VarIdx) * (Val(Session("Target")(VarIdx)) - 1) * (1 - (TarYr - Val(Cmb_Year.Text)) / (TarYr - FirstYearForecast)) + Session("Baseval")(VarIdx)
                End If
            End If
        Else
            If Not IsDBNull(Session("Target")(VarIdx)) Then
                'jrs 2017/08/28 added the other side: Or (Valence(VarIdx) = False And Val(Baseval(VarIdx)) > Val(Target(VarIdx)))
                'If Target(VarIdx) = 3 And Baseval(VarIdx) < 3 Then
                If (Session("Valence")(VarIdx) = False And Val(Session("Baseval")(VarIdx)) < Val(Session("Target")(VarIdx))) Or
                    Session("Valence")(VarIdx) = True And Val(Session("Baseval")(VarIdx)) > Val(Session("Target")(VarIdx)) Then
                    TarVal = Val(Session("Baseval")(VarIdx))
                Else
                    TarVal = Val(Session("Target")(VarIdx))
                End If
            End If
        End If

        'If Not IsDBNull(Session("Target")(VarIdx)) Then

        '    Select Case Session("Target")(VarIdx)
        '        Case 0.5
        '            If Val(Cmb_Year.Text) >= TarYr And TarYr > FirstYearForecast Then
        '                TarVal = Session("Baseval")(VarIdx) * 2
        '            Else
        '                TarVal = Session("Baseval")(VarIdx) * (1 - (TarYr - Val(Cmb_Year.Text)) / (TarYr - FirstYearForecast)) + Session("Baseval")(VarIdx)
        '            End If
        '        Case -0.5
        '            If Val(Cmb_Year.Text) >= TarYr And TarYr > FirstYearForecast Then
        '                TarVal = Session("Baseval")(VarIdx) / 2
        '            Else
        '                TarVal = Session("Baseval")(VarIdx) / 2 * (TarYr - Val(Cmb_Year.Text)) / (TarYr - FirstYearForecast) + Session("Baseval")(VarIdx) / 2
        '            End If
        '        Case -0.33
        '            If Val(Cmb_Year.Text) >= TarYr And TarYr > FirstYearForecast Then
        '                TarVal = Session("Baseval")(VarIdx) * 2 / 3
        '            Else
        '                TarVal = Session("Baseval")(VarIdx) / 3 * (TarYr - Val(Cmb_Year.Text)) / (TarYr - FirstYearForecast) + Session("Baseval")(VarIdx) * 2 / 3
        '            End If
        '        Case -0.66
        '            If Val(Cmb_Year.Text) >= TarYr And TarYr > FirstYearForecast Then
        '                TarVal = Session("Baseval")(VarIdx) / 3
        '            Else
        '                TarVal = Session("Baseval")(VarIdx) * 2 / 3 * (TarYr - Val(Cmb_Year.Text)) / (TarYr - FirstYearForecast) + Session("Baseval")(VarIdx) / 3
        '            End If
        '        Case -0.75
        '            If Val(Cmb_Year.Text) >= TarYr And TarYr > FirstYearForecast Then
        '                TarVal = Session("Baseval")(VarIdx) / 4
        '            Else
        '                TarVal = Session("Baseval")(VarIdx) * 3 / 4 * (TarYr - Val(Cmb_Year.Text)) / (TarYr - FirstYearForecast) + Session("Baseval")(VarIdx) / 4
        '            End If
        '        Case 2
        '            If Val(Cmb_Year.Text) >= TarYr And TarYr > FirstYearForecast Then
        '                TarVal = Session("Baseval")(VarIdx) * 2
        '            Else
        '                TarVal = Session("Baseval")(VarIdx) * (1 - (TarYr - Val(Cmb_Year.Text)) / (TarYr - FirstYearForecast)) + Session("Baseval")(VarIdx)
        '            End If
        '        Case -1
        '            TarVal = 0  '1
        '        Case Else
        '            If Not IsDBNull(Session("Target")(VarIdx)) Then
        '                If Session("Target")(VarIdx) = 3 And Session("Baseval")(VarIdx) < 3 Then
        '                    TarVal = Val(Session("Baseval")(VarIdx))
        '                Else
        '                    TarVal = Val(Session("Target")(VarIdx))
        '                End If
        '            End If
        '    End Select
        'Else
        '    TarVal = DBNull.Value
        'End If

        If Not IsDBNull(TarVal) Then
            If Not IsDBNull(Session("VarMins")(VarIdx)) Then
                TarVal = AMAX(TarVal, Session("VarMins")(VarIdx))
            End If
            If Not IsDBNull(Session("VarMaxs")(VarIdx)) Then
                TarVal = Amin(TarVal, Session("VarMaxs")(VarIdx))
            End If
            grd_TableDisplay.Rows(curRow).Cells(4).Text = TarVal
        Else
            grd_TableDisplay.Rows(curRow).Cells(4).Text = ""
        End If
        Session("VarsInGrid")(4, curRow) = grd_TableDisplay.Rows(curRow).Cells(4).Text
        If grd_TableDisplay.Rows(curRow).Cells(4).Text > "" Then
            grd_TableDisplay.Rows(curRow).Cells(4).Text = "<a href=""javascript:onPointClick(2, " & curRow & ")"">" & grd_TableDisplay.Rows(curRow).Cells(4).Text & "</a>"
        End If
        grd_TableDisplay.Rows(curRow).Cells(4).Font.Bold = True
    End Sub

    'jrs 2017/07/21
    Private Sub LoadTargetValuesH(VarIdx As Integer, curRow As Integer)
        Dim TarVal As Object
        Dim TarYr As Integer
        If Not IsDBNull(Session("TargetYearH")(VarIdx)) Then
            TarYr = Session("TargetYearH")(VarIdx)
            If TarYr = 0 Then
                TarYr = 2030
            End If
        Else
            TarYr = 2030
        End If

        'jrs 2017/06/15
        TarVal = DBNull.Value
        If Session("TargetIsRelativeH")(VarIdx) Then
            If Not IsDBNull(Session("TargetH")(VarIdx)) And Not IsDBNull(Session("Baseval")(VarIdx)) Then
                If Val(Cmb_Year.Text) >= TarYr And TarYr > FirstYearForecast Then
                    TarVal = Session("Baseval")(VarIdx) * Val(Session("TargetH")(VarIdx))
                Else
                    TarVal = Session("Baseval")(VarIdx) * (Val(Session("TargetH")(VarIdx)) - 1) * (1 - (TarYr - Val(Cmb_Year.Text)) / (TarYr - FirstYearForecast)) + Session("Baseval")(VarIdx)
                End If
            End If
        Else
            If Not IsDBNull(Session("TargetH")(VarIdx)) And Not IsDBNull(Session("Baseval")(VarIdx)) Then
                'jrs 2017/08/28 added the other side: Or (Valence(VarIdx) = false And Val(Baseval(VarIdx)) > Val(Target(VarIdx)))
                'If Target(VarIdx) = 3 And Baseval(VarIdx) < 3 Then
                If (Session("ValenceH")(VarIdx) = False And Val(Session("Baseval")(VarIdx)) < Val(Session("TargetH")(VarIdx))) Or
                    Session("ValenceH")(VarIdx) = True And Val(Session("Baseval")(VarIdx)) > Val(Session("TargetH")(VarIdx)) Then
                    TarVal = Val(Session("Baseval")(VarIdx))
                Else
                    TarVal = Val(Session("TargetH")(VarIdx))
                End If
            End If
        End If

        If Not IsDBNull(TarVal) Then
            If Not IsDBNull(Session("VarMinsH")(VarIdx)) Then
                TarVal = AMAX(TarVal, Session("VarMinsH")(VarIdx))
            End If
            If Not IsDBNull(Session("VarMaxsH")(VarIdx)) Then
                TarVal = Amin(TarVal, Session("VarMaxsH")(VarIdx))
            End If
            grd_TableDisplay.Rows(curRow).Cells(4).Text = TarVal
        Else
            grd_TableDisplay.Rows(curRow).Cells(4).Text = ""
        End If
        Session("VarsInGrid")(4, curRow) = grd_TableDisplay.Rows(curRow).Cells(4).Text
        grd_TableDisplay.Rows(curRow).Cells(4).Font.Bold = True
    End Sub

    'Subroutine to Load the Values
    'into SDG table
    Private Sub LoadValues(reg As Integer, yr As Integer, Optional RegCompare As Integer = 9999, Optional YrCompare As Integer = 9999)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ---------------------------

        grd_TableDisplay.Rows(0).Cells(0).Font.Bold = True

        Dim curRow As Integer
        curRow = 1
        Dim sLine As String, sUsrDefCat As String
        Dim j As Integer
        j = 0
        Dim SE As Single, SE2 As Single
        Dim rsIdx As Integer = 0
        Dim TrVal As Single

        'OPen files
        'Dim fNum As Integer, fNum2 As Integer
        'Dim sFileName As String
        'fNum = FreeFile()
        'If cboFiles.SelectedIndex > 0 Then
        '    sFileName = Session("RunFiles")(cboFiles.SelectedIndex)
        'Else
        '    sFileName = Session("PutFdrvRun") + "Working.run"
        'End If
        'FileOpen(fNum, sFileName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
        'fNum2 = FreeFile()
        'If cboSce2.SelectedIndex > 0 Then
        '    sFileName = Session("RunFiles")(cboSce2.SelectedIndex)
        'Else
        '    sFileName = Session("PutFdrvRun") + "Working.run"
        'End If
        'FileOpen(fNum2, sFileName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        'Load Rows
        With grd_TableDisplay
            Do While rsIdx < Session("rsVars").rows.count
                If Not IsDBNull(Session("rsVars").Rows(rsIdx).Item("DisplayVarName")) Then
                    sLine = Session("rsVars").Rows(rsIdx).Item("DisplayVarName")
                Else
                    sLine = Session("sName")(j + 1) & " - " & Session("Description")(j + 1)
                End If
                If curRow >= .Rows.Count Then
                    Dim rr As New TableRow
                    Dim jj As Integer
                    For jj = 1 To 11
                        Dim cc As New TableCell
                        If jj > 5 Then
                            cc.Visible = False
                        ElseIf jj > 1 Then
                            cc.HorizontalAlign = HorizontalAlign.Right
                        End If
                        rr.Cells.Add(cc)
                    Next
                    .Rows.Add(rr)
                    ReDim Preserve Session("VarsInGrid")(10, curRow)
                End If
                If Not IsDBNull(Session("rsVars").Rows(rsIdx).Item("UsrDefCat")) Then
                    If Session("rsVars").Rows(rsIdx).Item("UsrDefCat") <> sUsrDefCat Then
                        If sUsrDefCat > "" Then
                            .Rows(curRow).Cells(0).Text = "*"
                            'jrs 2017/09/08
                            .Rows(curRow).Cells(1).Text = ""
                            .Rows(curRow).Cells(2).Text = ""
                            .Rows(curRow).Cells(3).Text = ""
                            .Rows(curRow).Cells(4).Text = ""
                            curRow = curRow + 1
                            If curRow >= .Rows.Count Then
                                Dim rr As New TableRow
                                Dim jj As Integer
                                For jj = 1 To 11
                                    Dim cc As New TableCell
                                    If jj > 5 Then
                                        cc.Visible = False
                                    ElseIf jj > 1 Then
                                        cc.HorizontalAlign = HorizontalAlign.Right
                                    End If
                                    rr.Cells.Add(cc)
                                Next
                                .Rows.Add(rr)
                                ReDim Preserve Session("VarsInGrid")(10, curRow)
                            End If
                        End If
                        sUsrDefCat = Session("rsVars").Rows(rsIdx).Item("UsrDefCat")
                        .Rows(curRow).Cells(0).Text = "********************** Goal " & Session("GoalId")(j + 1) & ": " & UCase(sUsrDefCat) & " **********************"
                        .Rows(curRow).Cells(0).Font.Bold = True
                        'jrs 2017/09/08
                        .Rows(curRow).Cells(1).Text = ""
                        .Rows(curRow).Cells(2).Text = ""
                        .Rows(curRow).Cells(3).Text = ""
                        .Rows(curRow).Cells(4).Text = ""
                        curRow = curRow + 1
                        If curRow >= .Rows.Count Then
                            Dim rr As New TableRow
                            Dim jj As Integer
                            For jj = 1 To 11
                                Dim cc As New TableCell
                                If jj > 5 Then
                                    cc.Visible = False
                                ElseIf jj > 1 Then
                                    cc.HorizontalAlign = HorizontalAlign.Right
                                End If
                                rr.Cells.Add(cc)
                            Next
                            .Rows.Add(rr)
                            ReDim Preserve Session("VarsInGrid")(10, curRow)
                        End If
                    End If
                End If
                LoadValuesVar(reg, yr, Session("rsVars").Rows(rsIdx).Item("VarName"), sLine, IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("Param2")), "", Session("rsVars").Rows(rsIdx).Item("Param2")),
                IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("Param3")), "", Session("rsVars").Rows(rsIdx).Item("Param3")),
                IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("FormulaDet")), "", Session("rsVars").Rows(rsIdx).Item("FormulaDet")),
                IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("FormulaId")), "", Session("rsVars").Rows(rsIdx).Item("FormulaId")), curRow)
                j = j + 1


                Dim pos As Integer, pos2 As Integer
                pos = InStr(.Rows(curRow).Cells(1).Text, ">")
                pos2 = InStr(pos + 1, .Rows(curRow).Cells(1).Text, "<")
                Session("Baseval")(j) = Val(Mid(.Rows(curRow).Cells(1).Text, pos + 1, pos2 - pos - 1))

                '.col = 2

                '.CellFontBold = True
                LoadTargetValues(j, curRow)

                'Compute Alert level
                '.col = 2 'Forecast Value
                pos = InStr(.Rows(curRow).Cells(2).Text, ">")
                pos2 = InStr(pos + 1, .Rows(curRow).Cells(2).Text, "<")
                SE = Round(Val(Mid(.Rows(curRow).Cells(2).Text, pos + 1, pos2 - pos - 1)), 4)
                '.col = 4 'Target Value
                If .Rows(curRow).Cells(4).Text > "" Then
                    pos = InStr(.Rows(curRow).Cells(4).Text, ">")
                    pos2 = InStr(pos + 1, .Rows(curRow).Cells(4).Text, "<")
                    TrVal = Round(Val(Mid(.Rows(curRow).Cells(4).Text, pos + 1, pos2 - pos - 1)), 4)
                Else
                    TrVal = 0
                End If
                SE = Round(SE - TrVal, 4)
                '.col = 3 'Intervention Value
                pos = InStr(.Rows(curRow).Cells(3).Text, ">")
                pos2 = InStr(pos + 1, .Rows(curRow).Cells(3).Text, "<")
                SE2 = Round(Val(Mid(.Rows(curRow).Cells(3).Text, pos + 1, pos2 - pos - 1)), 4)
                SE2 = Round(SE2 - TrVal, 4)
                '.col = 2
                .Rows(curRow).Cells(2).BackColor = Color.White
                'if negative valence
                If Not IsDBNull(Session("Target")(j)) Then
                    'Positive valence
                    If Session("Valence")(j) Then
                        'jrs 2017/08/28 added other side: And Not (TargetIsRelativeByReg(j, reg) = False And SE + Trval > Val(TargetByReg(j, reg)))
                        If SE < 0 And Not (Session("TargetIsRelative")(j) = False And SE + TrVal > CSng(Session("Target")(j))) Then
                            .Rows(curRow).Cells(2).BackColor = Color.Red
                        Else
                            .Rows(curRow).Cells(2).BackColor = Color.Green
                        End If
                    Else 'if negative valence
                        'If SE > 0 And Not (Target(j) = -0.7 And SE >= 0.7) And Not (Target(j) = 3 And SE + TrVal < 3) Then
                        If SE > 0 And Not (CSng(Session("Target")(j)) < 0 And SE >= Abs(CSng(Session("Target")(j)))) And Not (Session("TargetIsRelative")(j) = False And SE + TrVal < CSng(Session("Target")(j))) Then
                            .Rows(curRow).Cells(2).BackColor = Color.Red
                            'jrs 2017/06/28 ElseIf Not (CSng(Session("Target")(j)) < 0 And SE >= Abs(CSng(Session("Target")(j)))) And Not (Session("TargetIsRelative")(j) = False And SE + TrVal < CSng(Session("Target")(j))) Then
                        ElseIf Not (CSng(Session("Target")(j)) < 0 And SE >= Abs(CSng(Session("Target")(j)))) Then
                            .Rows(curRow).Cells(2).BackColor = Color.Green
                        End If
                    End If

                    'If (Session("Target")(j) <= 0 Or Session("Target")(j) = 3 Or Session("Target")(j) = 12 Or TrVal < 0) Then
                    '    If SE > 0 And Not (Session("Target")(j) = -0.7 And SE >= 0.7) And Not (Session("Target")(j) = 3 And SE + TrVal < 3) Then
                    '        .Rows(curRow).Cells(1).BackColor = Color.Red
                    '    End If
                    'Else
                    '    If SE < 0 Then
                    '        .Rows(curRow).Cells(1).BackColor = Color.Red
                    '    End If
                    'End If
                End If
                '.col = 3
                .Rows(curRow).Cells(3).BackColor = Color.White
                'if negative valence
                If Not IsDBNull(Session("Target")(j)) Then
                    'Positive valence
                    If Session("Valence")(j) Then
                        'jrs 2017/08/28 added other side: And Not (TargetIsRelative(j) = false And SE2 + Trval > Val(Target(j)))
                        If SE2 < 0 And Not (Session("TargetIsRelative")(j) = False And SE2 + TrVal > CSng(Session("Target")(j))) Then
                            If SE2 <= SE Then
                                .Rows(curRow).Cells(3).BackColor = Color.Red
                            Else
                                .Rows(curRow).Cells(3).BackColor = Color.Yellow
                            End If
                        Else
                            .Rows(curRow).Cells(3).BackColor = Color.Green
                        End If
                    Else 'if negative valence
                        'If SE > 0 And Not (Target(j) = -0.7 And SE >= 0.7) And Not (Target(j) = 3 And SE + TrVal < 3) Then
                        If SE2 > 0 And Not (CSng(Session("Target")(j)) < 0 And SE2 >= Abs(CSng(Session("Target")(j)))) And Not (Session("TargetIsRelative")(j) = False And SE2 + TrVal < CSng(Session("Target")(j))) Then
                            If SE2 >= SE Then
                                .Rows(curRow).Cells(3).BackColor = Color.Red
                            Else
                                .Rows(curRow).Cells(3).BackColor = Color.Yellow
                            End If
                            'jrs 2017/06/28 ElseIf Not (CSng(Session("Target")(j)) < 0 And SE2 >= Abs(CSng(Session("Target")(j)))) And Not (Session("TargetIsRelative")(j) = False And SE2 + TrVal < CSng(Session("Target")(j))) Then
                        ElseIf Not (CSng(Session("Target")(j)) < 0 And SE2 >= Abs(CSng(Session("Target")(j)))) Then
                            .Rows(curRow).Cells(3).BackColor = Color.Green
                        End If
                    End If


                    'If (Session("Target")(j) <= 0 Or Session("Target")(j) = 3 Or Session("Target")(j) = 12 Or TrVal < 0) Then
                    '    If SE > 0 And Not (Session("Target")(j) = -0.7 And SE >= 0.7) And Not (Session("Target")(j) = 3 And SE + TrVal < 3) Then
                    '        .Rows(curRow).Cells(1).BackColor = Color.Red
                    '    End If
                    'Else
                    '    If SE < 0 Then
                    '        .Rows(curRow).Cells(1).BackColor = Color.Red
                    '    End If
                    'End If
                End If

                '.col = 5
                .Rows(curRow).Cells(5).Text = j
                Session("VarsInGrid")(5, curRow) = j
                '.col = 6
                .Rows(curRow).Cells(6).Text = Session("rsVars").Rows(rsIdx).Item("VarName")
                Session("VarsInGrid")(6, curRow) = .Rows(curRow).Cells(6).Text
                '.col = 7 'jrs 2019/12/27 added iif(IsDBNull, "")
                .Rows(curRow).Cells(7).Text = IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("Param2")), "", Session("rsVars").Rows(rsIdx).Item("Param2"))
                Session("VarsInGrid")(7, curRow) = .Rows(curRow).Cells(7).Text
                '.col = 8 'jrs 2019/12/27 added iif(IsDBNull, "")
                .Rows(curRow).Cells(8).Text = IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("Param3")), "", Session("rsVars").Rows(rsIdx).Item("Param3"))
                Session("VarsInGrid")(8, curRow) = .Rows(curRow).Cells(8).Text
                '.col = 9
                .Rows(curRow).Cells(9).Text = IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("FormulaDet")), "", Session("rsVars").Rows(rsIdx).Item("FormulaDet"))
                Session("VarsInGrid")(9, curRow) = .Rows(curRow).Cells(9).Text
                '.col = 10
                .Rows(curRow).Cells(10).Text = IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("FormulaId")), "", Session("rsVars").Rows(rsIdx).Item("FormulaId"))
                Session("VarsInGrid")(10, curRow) = .Rows(curRow).Cells(10).Text

                '.col = 0
                .Rows(curRow).Cells(0).Font.Bold = True

                curRow = curRow + 1
                rsIdx = rsIdx + 1
            Loop

        End With

        'FileClose(fNum)
        'FileClose(fNum2)
    End Sub

    'jrs 2017/07/12
    Private Sub LoadValuesH()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ---------------------------
        Dim i As Integer

        grd_TableDisplay.Rows(0).Cells(0).Font.Bold = True
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim curRow As Integer
        curRow = 1
        Dim sLine As String, sUsrDefCat As String
        Dim j As Integer
        j = 0
        Dim SE As Single, SE2 As Single
        Dim rsIdx As Integer = 0
        Dim TrVal As Single

        'jrs 2017/08/24
        If Session("rsVarsH").rows.count > Session("rsVars").rows.count Then
            ReDim Session("Baseval")(Session("rsVarsH").rows.count)
        End If

        'Load Rows
        With grd_TableDisplay
            Do While rsIdx < Session("rsVarsH").rows.count
                If Not IsDBNull(Session("rsVarsH").Rows(rsIdx).Item("DisplayVarName")) Then
                    sLine = Session("rsVarsH").Rows(rsIdx).Item("DisplayVarName")
                Else
                    sLine = Session("sNameH")(j + 1) & " - " & Session("DescriptionH")(j + 1)
                End If
                If curRow >= .Rows.Count Then
                    Dim rr As New TableRow
                    Dim jj As Integer
                    For jj = 1 To 11
                        Dim cc As New TableCell
                        If jj > 5 Then
                            cc.Visible = False
                        ElseIf jj = 3 Or jj = 4 Then 'jrs 2017/09/07
                            cc.Visible = False
                        ElseIf jj > 1 Then
                            cc.HorizontalAlign = HorizontalAlign.Right
                        End If
                        rr.Cells.Add(cc)
                    Next
                    .Rows.Add(rr)
                    ReDim Preserve Session("VarsInGrid")(10, curRow)
                End If
                If Not IsDBNull(Session("rsVarsH").Rows(rsIdx).Item("UsrDefCat")) Then
                    If Session("rsVarsH").Rows(rsIdx).Item("UsrDefCat") <> sUsrDefCat Then
                        If sUsrDefCat > "" Then
                            .Rows(curRow).Cells(0).Text = "*"
                            For i = 1 To 4 Step 3
                                .Rows(curRow).Cells(i).Text = ""
                                Session("VarsInGrid")(i, curRow) = ""
                                .Rows(curRow).Cells(i).BackColor = Color.White
                            Next

                            curRow = curRow + 1
                            If curRow >= .Rows.Count Then
                                Dim rr As New TableRow
                                Dim jj As Integer
                                For jj = 1 To 11
                                    Dim cc As New TableCell
                                    If jj > 5 Then
                                        cc.Visible = False
                                    ElseIf jj = 3 Or jj = 4 Then 'jrs 2017/09/07
                                        cc.Visible = False
                                    ElseIf jj > 1 Then
                                        cc.HorizontalAlign = HorizontalAlign.Right
                                    End If
                                    rr.Cells.Add(cc)
                                Next
                                .Rows.Add(rr)
                                ReDim Preserve Session("VarsInGrid")(10, curRow)
                            End If
                        End If
                        sUsrDefCat = Session("rsVarsH").Rows(rsIdx).Item("UsrDefCat")
                        .Rows(curRow).Cells(0).Text = "********************** Goal " & Session("GoalIdH")(j + 1) & ": " & UCase(sUsrDefCat) & " **********************"
                        .Rows(curRow).Cells(0).Font.Bold = True
                        For i = 1 To 4 Step 3
                            .Rows(curRow).Cells(i).Text = ""
                            Session("VarsInGrid")(i, curRow) = ""
                            .Rows(curRow).Cells(i).BackColor = Color.White
                        Next
                        curRow = curRow + 1
                        If curRow >= .Rows.Count Then
                            Dim rr As New TableRow
                            Dim jj As Integer
                            For jj = 1 To 11
                                Dim cc As New TableCell
                                If jj > 5 Then
                                    cc.Visible = False
                                ElseIf jj = 3 Or jj = 4 Then 'jrs 2017/09/07
                                    cc.Visible = False
                                ElseIf jj > 1 Then
                                    cc.HorizontalAlign = HorizontalAlign.Right
                                End If
                                rr.Cells.Add(cc)
                            Next
                            .Rows.Add(rr)
                            ReDim Preserve Session("VarsInGrid")(10, curRow)
                        End If
                    End If
                End If

                If sLine > "" Then
                    .Rows(curRow).Cells(0).Text = sLine
                Else
                    .Rows(curRow).Cells(0).Text = Session("rsVarsH").Rows(rsIdx).Item("VarName")
                End If
                Session("VarsInGrid")(0, curRow) = .Rows(curRow).Cells(0).Text
                .Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(0, " & curRow & ")"">" & .Rows(curRow).Cells(0).Text & "</a>"
                'here we can go read ifsHistSeries and show a value for the base year if there is one
                Dim VarVals() As Object
                ReDim VarVals(1)
                'gLoadForDisplayHis(Session("rsVarsH").Rows(rsIdx).Item("VarName"), cmb_Country.Text, cmb_Country.SelectedIndex + 1, "", -1, "", -1, _
                'Session("SDGsetGroup"), 1, VarVals, 1, FirstYearForecast, FirstYearForecast, _
                'True, False, False, False, Session("FinYearHis")(1), _
                'Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"), _
                'Session("bComputation"), "", "", IfsPars, False, _
                'Session("bCompAll"), Session("dataAll"), Session("StaYearHis"))
                'If j = 104 Then
                '    j = j
                'End If
                gLoadForDisplayHisYrFld(Session("rsVarsH").Rows(rsIdx).Item("VarName"), cmb_Country.Text, cmb_Country.SelectedIndex + 1, "", -1, "", -1,
                Session("SDGsetGroup"), 1, VarVals, 1, "MostRecent", "MostRecent",
                True, False, False, "", False, False, -1, False, Nothing, Session("bLoadForDisplaySuccess"), Me, IfsPars, Session("FinYear"), False,
                IFsCtrys, Session("LastYear"), Session("LastYearForecast"), False, Session("mnuSQLite"))

                If Not IsDBNull(VarVals(1)) Then
                    .Rows(curRow).Cells(1).Text = Round(VarVals(1), 2)
                    'jrs 2017/08/25
                    .Rows(curRow).Font.Bold = True
                Else
                    .Rows(curRow).Cells(1).Text = ""
                End If

                j = j + 1

                'jrs 2017/07/21
                Session("Baseval")(j) = VarVals(1)

                Dim reg As Integer
                If Session("SDGsetGroup") Then
                    reg = 0
                Else
                    reg = cmb_Country.SelectedIndex + 1
                End If
                LoadTargetValuesH(j, curRow)

                For i = 2 To 3
                    .Rows(curRow).Cells(i).Text = ""
                    Session("VarsInGrid")(i, curRow) = ""
                    .Rows(curRow).Cells(i).BackColor = Color.White
                Next

                '.col = 5
                .Rows(curRow).Cells(5).Text = j
                Session("VarsInGrid")(5, curRow) = j
                '.col = 6
                .Rows(curRow).Cells(6).Text = Session("rsVarsH").Rows(rsIdx).Item("VarName")
                Session("VarsInGrid")(6, curRow) = .Rows(curRow).Cells(6).Text

                '.col = 7 'jrs 2024/11/22 handling nulls
                .Rows(curRow).Cells(7).Text = IIf(IsDBNull(Session("rsVarsH").Rows(rsIdx).Item("Param2")), "", Session("rsVarsH").Rows(rsIdx).Item("Param2"))
                Session("VarsInGrid")(7, curRow) = .Rows(curRow).Cells(7).Text
                '.col = 8 'jrs 2024/11/22 handling nulls
                .Rows(curRow).Cells(8).Text = IIf(IsDBNull(Session("rsVarsH").Rows(rsIdx).Item("Param3")), "", Session("rsVarsH").Rows(rsIdx).Item("Param3"))
                Session("VarsInGrid")(8, curRow) = .Rows(curRow).Cells(8).Text


                '.col = 9
                .Rows(curRow).Cells(9).Text = IIf(IsDBNull(Session("rsVarsH").Rows(rsIdx).Item("FormulaDet")), "", Session("rsVarsH").Rows(rsIdx).Item("FormulaDet"))
                Session("VarsInGrid")(9, curRow) = .Rows(curRow).Cells(9).Text
                '.col = 10
                .Rows(curRow).Cells(10).Text = IIf(IsDBNull(Session("rsVarsH").Rows(rsIdx).Item("FormulaId")), "", Session("rsVarsH").Rows(rsIdx).Item("FormulaId"))
                Session("VarsInGrid")(10, curRow) = .Rows(curRow).Cells(10).Text

                '.col = 0
                .Rows(curRow).Cells(0).Font.Bold = True

                curRow = curRow + 1
                rsIdx = rsIdx + 1
            Loop

        End With

        'FileClose(fNum)
        'FileClose(fNum2)
    End Sub

    'Subroutine to Load the Value into Cell
    Private Sub LoadValuesVar(ByVal reg As Integer, yr As Integer, sVar As String,
        sLineLabel As String, sSecondDim As String, sThirdDim As String, sFormulaDet As String,
        iFormulaId As Integer, curRow As Integer)

        Dim VarIdx As Integer
        Dim VarVal As Single
        Dim VarVals() As Single
        ReDim VarVals(yr - Session("FirstYear") + 1)
        VarIdx = IfsVars.Item(sVar).Index

        Dim IfsPars As New myParameters

        Dim curr As Single, s1 As String, s2 As String, s3 As String, s4 As String, s5 As String
        curr = CSng(Session("currencyVal"))
        s1 = CStr(Session("Ifs_MDB2Active"))
        s2 = CStr(Session("IFsCoVaTrASeries_MDB"))
        s3 = CStr(Session("IFsHistSeries_MDB"))
        s4 = CStr(Session("PutFdrvRun"))
        s5 = CStr(Session("CurrencySym"))

        IfsPars.LoadPars(curr, Session("ShortSce"), Session("RunFiles"),
        s1, s2, s3, s4, s5)

        If VarIdx > 0 Then
            If Session("mnuSQLite") = "True" Then
                If FilesCol.Contains("FileForDisplay" + CStr(cboFiles.SelectedIndex)) Then
                    Session("FileForDisplay" + CStr(cboFiles.SelectedIndex)) = FilesCol("FileForDisplay" + CStr(cboFiles.SelectedIndex))
                End If
                If dfCol.Contains("df" + sVar + CStr(cboFiles.SelectedIndex)) Then
                    Session("df" + sVar + CStr(cboFiles.SelectedIndex)) = dfCol("df" + sVar + CStr(cboFiles.SelectedIndex))
                End If

                gLoadForDisplayNET(sVar, VarIdx, cmb_Country.Text, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, False, False,
                Session("SDGsetGroup"), 1, VarVals, 1, 1, Session("FirstYear"), yr, 0, True, Session("bLoadForDisplaySuccess"),
                Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay" + CStr(cboFiles.SelectedIndex)), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))

                'jrs 2021/04/12
                If Not FilesCol.Contains("FileForDisplay" + CStr(cboFiles.SelectedIndex)) Then
                    Dim MyFileforDisplay1 As IStorage
                    FilesCol.Add(MyFileforDisplay1, "FileForDisplay" + CStr(cboFiles.SelectedIndex))
                End If
                If Not dfCol.Contains("df" + sVar + CStr(cboFiles.SelectedIndex)) Then
                    Dim MyDF1 As IDataframe
                    dfCol.Add(MyDF1, "df" + sVar + CStr(cboFiles.SelectedIndex))
                End If

            Else
                gLoadForDisplay(sVar, VarIdx, cmb_Country.Text, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, True, True,
                Session("SDGsetGroup"), 1, VarVals, 1, 1, Session("FirstYear"), yr, 0, True, Session("bLoadForDisplaySuccess"),
                Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        Else
            '    ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"), _
            '    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), _
            '    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"))
            gLoadComp(1, sFormulaDet, sVar, cmb_Country.Text, sSecondDim, sThirdDim,
            "", -1, Nothing, cboFiles.SelectedIndex, False, False, Session("SDGFormulas")(iFormulaId.ToString), Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(1),
            False, Session("bComputation"), Session("FirstYear"), yr, Session("LastYearForecast"),
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
            Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
            Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol)
            VarVals(yr - Session("FirstYear") + 1) = Session("PLOT")(yr - Session("FirstYear") + 1, 1)
            VarVals(1) = Session("PLOT")(1, 1)
        End If

        With grd_TableDisplay
            If sLineLabel > "" Then
                .Rows(curRow).Cells(0).Text = sLineLabel
            ElseIf VarIdx > 0 Then
                .Rows(curRow).Cells(0).Text = VDef(VarIdx)
            Else
                .Rows(curRow).Cells(0).Text = sVar
            End If
            If sSecondDim > "" Then
                .Rows(curRow).Cells(0).Text = .Rows(curRow).Cells(0).Text & " - " & sSecondDim
                If sThirdDim > "" Then
                    .Rows(curRow).Cells(0).Text = .Rows(curRow).Cells(0).Text & ", " & sThirdDim
                End If
            End If
            Session("VarsInGrid")(0, curRow) = .Rows(curRow).Cells(0).Text
            .Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(0, " & curRow & ")"">" & .Rows(curRow).Cells(0).Text & "</a>"
            '.col = 1
            .Rows(curRow).Cells(1).Text = VarVals(1)
            Session("VarsInGrid")(1, curRow) = .Rows(curRow).Cells(1).Text
            .Rows(curRow).Cells(1).Font.Bold = True
            .Rows(curRow).Cells(1).Text = "<a href=""javascript:onPointClick(1, " & curRow & ")"">" & .Rows(curRow).Cells(1).Text & "</a>"
            '.col = 2
            .Rows(curRow).Cells(2).Text = VarVals(yr - Session("FirstYear") + 1)
            Session("VarsInGrid")(2, curRow) = .Rows(curRow).Cells(2).Text
            .Rows(curRow).Cells(2).Font.Bold = True
            .Rows(curRow).Cells(2).Text = "<a href=""javascript:onPointClick(1, " & curRow & ")"">" & .Rows(curRow).Cells(2).Text & "</a>"

            If VarIdx > 0 Then
                If Session("mnuSQLite") = "True" Then
                    If FilesCol.Contains("FileForDisplay" + CStr(cboSce2.SelectedIndex)) Then
                        Session("FileForDisplay" + CStr(cboSce2.SelectedIndex)) = FilesCol("FileForDisplay" + CStr(cboSce2.SelectedIndex))
                    End If
                    If dfCol.Contains("df" + sVar + CStr(cboSce2.SelectedIndex)) Then
                        Session("df" + sVar + CStr(cboSce2.SelectedIndex)) = dfCol("df" + sVar + CStr(cboSce2.SelectedIndex))
                    End If

                    gLoadForDisplayNET(sVar, VarIdx, cmb_Country.Text, reg, sSecondDim, -1, sThirdDim, -1, "", cboSce2.SelectedIndex, False, False,
                    Session("SDGsetGroup"), 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"),
                    Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay" + CStr(cboSce2.SelectedIndex)), Session("df" + sVar + CStr(cboSce2.SelectedIndex)))

                    'jrs 2021/04/12
                    If Not FilesCol.Contains("FileForDisplay" + CStr(cboSce2.SelectedIndex)) Then
                        Dim MyFileforDisplay1 As IStorage
                        FilesCol.Add(MyFileforDisplay1, "FileForDisplay" + CStr(cboSce2.SelectedIndex))
                    End If
                    If Not dfCol.Contains("df" + sVar + CStr(cboSce2.SelectedIndex)) Then
                        Dim MyDF1 As IDataframe
                        dfCol.Add(MyDF1, "df" + sVar + CStr(cboSce2.SelectedIndex))
                    End If

                Else
                    gLoadForDisplay(sVar, VarIdx, cmb_Country.Text, reg, sSecondDim, -1, sThirdDim, -1, "", cboSce2.SelectedIndex, True, True,
                    Session("SDGsetGroup"), 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"),
                    Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If
            Else
                '    ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"), _
                '    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), _
                '    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"))
                gLoadComp(1, sFormulaDet, sVar, cmb_Country.Text, sSecondDim, sThirdDim,
                "", -1, Nothing, cboSce2.SelectedIndex, False, False, Session("SDGFormulas")(iFormulaId.ToString), Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(1),
                False, Session("bComputation"), Session("FirstYear"), yr, Session("LastYearForecast"),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol)
                VarVals(1) = Session("PLOT")(yr - Session("FirstYear") + 1, 1)
            End If
            '.col = 3
            .Rows(curRow).Cells(3).Text = VarVals(1)
            Session("VarsInGrid")(3, curRow) = .Rows(curRow).Cells(3).Text
            .Rows(curRow).Cells(3).Font.Bold = True
            .Rows(curRow).Cells(3).Text = "<a href=""javascript:onPointClick(1, " & curRow & ")"">" & .Rows(curRow).Cells(3).Text & "</a>"


            '        If iv > 0 Then
            '            .col = 2
            '            XYTABL sTblFunName, iv, VarVal
            '            .text = VarVal
            '        End If
        End With

        'jrs 2021/04/12
        Session("dfCol") = dfCol
        Session("FilesCol") = FilesCol

    End Sub

    Private Sub ClearValues()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim i As Integer, j As Integer
        With grd_TableDisplay
            For i = 1 To .Rows.Count - 1
                For j = 0 To 10
                    .Rows(i).Cells(j).Text = ""
                Next j
            Next i
            ReDim Session("VarsInGrid")(10, .Rows.Count)
        End With
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Switch"
                mnuGroups_Click()
            Case "Switch2"
                mnuHistOnly_Click()
        End Select
    End Sub

    Sub ItemClickHandlerContext(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        Dim currRow As Integer
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        currRow = Request.Form("txtRow")
        Session("bLastRow") = (currRow = grd_TableDisplay.Rows.Count - 1)
        Dim sPar As String
        If Session("SDGsetGroup") = True Then
            'jrs 2017/08/31 change & for %26 for the url, problems with groups like Latin America & Caribean
            Session("MMGroup") = Replace(Session("MMGroup"), "&", "%26")
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        Session("TableDisplayParent") = "frm_SDGset.aspx?" & sPar
        Select Case objEA.Item.Caption
            Case "Display Graph"
                DisplayGraph(currRow)
            Case "Show Over Time"
                DisplayOverTime(currRow)
            Case "Edit Target"
                LoadGraphVars()
                Session("rowidx") = Session("VarsInGrid")(5, currRow)
                Session("SDGiIndex") = FindIdx(Session("rowidx"))
                Session("SDGEditTargetParent") = "frm_SDGset.aspx"
                'jrs 2026/01/16 Support.FormManager.Show(Me, "frm_SDGEditTarget.aspx")
                'Changing to friendly URL
                Dim Vpath As String
                Dim Vext As String
                Dim vfriendlyurl As String
                Dim vurl As String

                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDGEditTarget")

                vurl = FriendlyUrl.Href("~/frm_SDGEditTarget")

                Response.Redirect(vurl)

        End Select
    End Sub

    Private Function FindIdx(rowidx) As Integer
        Dim i As Integer
        For i = 0 To 101
            If Session("SDGIndicatorName")(i) = Session("sName")(rowidx) Then
                FindIdx = i
                Exit For
            End If
        Next
    End Function

    Private Sub LoadGraphVars()
        If Session("SDGIndicator2Goal") Is Nothing Then
            Dim TotIndicator As Integer
            TotIndicator = 0
            Dim dbSDG As String
            dbSDG = DataFileDir$ + "IFsMDG.mdb"
            SetDataTablefromAccess(dbSDG, "Select *  From Indicators where GoalsetID = 2 order by GoalSetID,GoalID,TargetID,ID", "", Session("valtbl"))
            Dim idx As Integer, chk As Integer
            idx = 0

            Dim IndicatorMax As Integer
            IndicatorMax = 306

            Dim Indicator2Goal() As Integer
            Dim IndicatorDesc() As String
            Dim IndicatorName() As String
            Dim Indicator2Target() As String
            Dim Indicator_IfsVar() As String
            Dim Indicator_IfsVarDim2() As String
            Dim Indicator_IfsVarNo() As Integer
            Dim Indicator_Transform() As String
            Dim Indicator_Var2() As String
            Dim Indicator_Var2Dim2() As String
            Dim Indicator_Unit() As String
            Dim Indicator_BaseYear() As Integer
            Dim Indicator_TargetYear() As Integer
            Dim Indicator_InterimYear() As Integer
            Dim Indicator_TargetValue() As Object
            Dim Indicator_Function() As String
            Dim Indicator_SDGHistAnalog() As String
            Dim Indicator_BubbleGraph() As String
            Dim Indicator_Min() As Double
            Dim Indicator_Max() As Double
            'jrs 2017/06/14
            Dim Indicator_TargetValueIsRelative() As Boolean
            Dim Indicator_Valence() As Boolean

            'first driver table
            Dim Indicator_DriverTbl() As String
            Dim Indicator_Conversion() As String

            'mti July 2006 -start -2nd driver table to fill up GDP2003PCPPP holes from GDP2000PCPPP
            Dim Indicator_DriverTbl1() As String
            'mti July 2006 -end    

            Dim Indicator_ConversionNumber() As Double

            'jrs 2017/02/28
            Dim Indicator_TransFunction() As String
            Dim Indicator_TransFunctionVar() As String

            ReDim Indicator2Goal(IndicatorMax) 'As Integer
            ReDim IndicatorDesc(IndicatorMax) 'As String
            ReDim IndicatorName(IndicatorMax) 'As String
            ReDim Indicator2Target(IndicatorMax) 'As Integer
            ReDim Indicator_IfsVar(IndicatorMax) 'As String
            ReDim Indicator_IfsVarDim2(IndicatorMax) 'As String

            ReDim Indicator_IfsVarNo(IndicatorMax) 'As Integer
            ReDim Indicator_Transform(IndicatorMax) 'As String
            ReDim Indicator_Var2(IndicatorMax) 'As String
            ReDim Indicator_Var2Dim2(IndicatorMax) 'As String
            ReDim Indicator_Unit(IndicatorMax) 'As String
            ReDim Indicator_Function(IndicatorMax) 'As String
            ReDim Indicator_TargetValue(IndicatorMax) 'As Double

            ReDim Indicator_BaseYear(IndicatorMax) 'As Integer
            ReDim Indicator_TargetYear(IndicatorMax) 'As Integer
            ReDim Indicator_InterimYear(IndicatorMax) 'As Integer
            ReDim Indicator_SDGHistAnalog(IndicatorMax) 'As String
            ReDim Indicator_BubbleGraph(IndicatorMax) 'As String
            ReDim Indicator_Min(IndicatorMax) 'As Double
            ReDim Indicator_Max(IndicatorMax) 'As Double
            'jrs 2017/06/14
            ReDim Indicator_TargetValueIsRelative(IndicatorMax)
            ReDim Indicator_Valence(IndicatorMax)


            ReDim Indicator_DriverTbl(IndicatorMax) 'As String
            'mti July 2006 -start
            ReDim Indicator_DriverTbl1(IndicatorMax) 'As String
            'mti July 2006 -end
            ReDim Indicator_Conversion(IndicatorMax) 'As String
            ReDim Indicator_ConversionNumber(IndicatorMax) 'As Double
            'jrs 2017/05/31
            'jrs 2017/02/28
            ReDim Indicator_TransFunction(IndicatorMax)
            ReDim Indicator_TransFunctionVar(IndicatorMax)



            While Not Session("valtbl").rows.count = idx
                TotIndicator = TotIndicator + 1
                Indicator2Goal(TotIndicator) = Session("valtbl").rows(idx).item("GoalID")
                Indicator2Target(TotIndicator) = Session("valtbl").rows(idx).item("TargetID")
                IndicatorName(TotIndicator) = "" & Session("valtbl").rows(idx).item("Name")
                IndicatorDesc(TotIndicator) = "" & Session("valtbl").rows(idx).item("Name") & " - " & Session("valtbl").rows(idx).item("Description")
                Indicator_IfsVar(TotIndicator) = "" & Session("valtbl").rows(idx).item("IFsVar")
                Indicator_IfsVarDim2(TotIndicator) = "" & Session("valtbl").rows(idx).item("IFsVar Dim2")
                Indicator_Transform(TotIndicator) = "" & Session("valtbl").rows(idx).item("Transform")


                Indicator_Var2(TotIndicator) = "" & Session("valtbl").rows(idx).item("Var2")
                Indicator_Var2Dim2(TotIndicator) = "" & Session("valtbl").rows(idx).item("Var2Dim2")

                Indicator_Unit(TotIndicator) = "" & Session("valtbl").rows(idx).item("Unit")
                Indicator_Function(TotIndicator) = "" & Session("valtbl").rows(idx).item("Function Name")
                Indicator_TargetValue(TotIndicator) = Session("valtbl").rows(idx).item("TargetValue")

                Indicator_BaseYear(TotIndicator) = Val("" & Session("valtbl").rows(idx).item("Base Year"))
                If Indicator_BaseYear(TotIndicator) = 0 Then
                    Indicator_BaseYear(TotIndicator) = Session("FirstYear")
                End If

                Indicator_TargetYear(TotIndicator) = Val("" & Session("valtbl").rows(idx).item("Target Year"))
                If Indicator_TargetYear(TotIndicator) = 0 Then
                    Indicator_TargetYear(TotIndicator) = 2030
                End If

                Indicator_InterimYear(TotIndicator) = Val("" & Session("valtbl").rows(idx).item("Interim Year"))

                Indicator_SDGHistAnalog(TotIndicator) = "" & Session("valtbl").rows(idx).item("Historic Analog")
                Indicator_BubbleGraph(TotIndicator) = "" & Session("valtbl").rows(idx).item("Bubblegraph")

                Indicator_Min(TotIndicator) = "" & Session("valtbl").rows(idx).item("Minimum")
                Indicator_Max(TotIndicator) = "" & Session("valtbl").rows(idx).item("Maximum")

                Indicator_DriverTbl(TotIndicator) = "" & Session("valtbl").rows(idx).item("DriverTbl")
                'mti July 2006 -start
                Indicator_DriverTbl1(TotIndicator) = "" & Session("valtbl").rows(idx).item("DriverTbl1")
                'mti July 2006 -end
                Indicator_Conversion(TotIndicator) = "" & Session("valtbl").rows(idx).item("Conversion")
                Indicator_ConversionNumber(TotIndicator) = "" & Session("valtbl").rows(idx).item("ConversionNumber")

                'jrs 2017/02/28
                Indicator_TransFunction(TotIndicator) = "" & Session("valtbl").rows(idx).item("TransFunction")
                Indicator_TransFunctionVar(TotIndicator) = "" & Session("valtbl").rows(idx).item("TransFunctionVar")

                'jrs 2017/06/14
                If Not IsDBNull(Session("valtbl").rows(idx).item("TargetValueIsRelative")) Then
                    Indicator_TargetValueIsRelative(TotIndicator) = CBool(Session("valtbl").rows(idx).item("TargetValueIsRelative"))
                Else
                    Indicator_TargetValueIsRelative(TotIndicator) = False
                End If
                If Not IsDBNull(Session("valtbl").rows(idx).item("Valence")) Then
                    Indicator_Valence(TotIndicator) = CBool(Session("valtbl").rows(idx).item("Valence"))
                Else
                    Indicator_Valence(TotIndicator) = False
                End If

                '        If Indicator_InterimYear(TotIndicator) = 0 Then
                '            Indicator_InterimYear(TotIndicator) = MDInterimYear
                '        End If


                chk = 0
                If Indicator_IfsVar(TotIndicator) <> "" Then
                    chk = IfsVars.Item(Indicator_IfsVar(TotIndicator)).Index
                End If
                If chk > 0 Then
                    Indicator_IfsVarNo(TotIndicator) = chk
                End If
                idx = idx + 1
            End While

            Session("SDGIndicator2Goal") = Indicator2Goal
            Session("SDGIndicator2Target") = Indicator2Target
            Session("SDGIndicatorName") = IndicatorName
            Session("SDGIndicatorDesc") = IndicatorDesc
            Session("SDGIndicator_IfsVar") = Indicator_IfsVar
            Session("SDGIndicator_IfsVarDim2") = Indicator_IfsVarDim2
            Session("SDGIndicator_Transform") = Indicator_Transform
            Session("SDGIndicator_Var2") = Indicator_Var2
            Session("SDGIndicator_Var2Dim2") = Indicator_Var2Dim2
            Session("SDGIndicator_Unit") = Indicator_Unit
            Session("SDGIndicator_Function") = Indicator_Function
            Session("SDGIndicator_TargetValue") = Indicator_TargetValue
            Session("SDGIndicator_BaseYear") = Indicator_BaseYear
            Session("SDGIndicator_TargetYear") = Indicator_TargetYear
            Session("SDGIndicator_InterimYear") = Indicator_InterimYear
            Session("SDGIndicator_SDGHistAnalog") = Indicator_SDGHistAnalog
            Session("SDGIndicator_BubbleGraph") = Indicator_BubbleGraph
            Session("SDGIndicator_Max") = Indicator_Max
            Session("SDGIndicator_Min") = Indicator_Min
            Session("SDGIndicator_DriverTbl") = Indicator_DriverTbl
            Session("SDGIndicator_DriverTbl1") = Indicator_DriverTbl1
            Session("SDGIndicator_Conversion") = Indicator_Conversion
            Session("SDGIndicator_ConversionNumber") = Indicator_ConversionNumber
            Session("SDGIndicator_IfsVarNo") = Indicator_IfsVarNo
            'jrs 2017/05/31
            Session("SDGIndicator_TransFunction") = Indicator_TransFunction
            Session("SDGIndicator_TransFunctionVar") = Indicator_TransFunctionVar
            'jrs 2017/06/16
            Session("SDGIndicator_TargetValueIsRelative") = Indicator_TargetValueIsRelative
            Session("SDGIndicator_Valence") = Indicator_Valence

        End If
    End Sub

    Private Sub DisplayGraph(ByVal currRow As Integer)
        Session("SDGSetFileIdx") = cboFiles.SelectedIndex
        Session("SDGSetFileIdx2") = cboSce2.SelectedIndex

        Dim rowId As Integer
        rowId = Val(Session("VarsInGrid")(5, currRow))

        If Session("SDGHistoryOnly") Then
            Session("SDGGoalidx") = Session("GoalIdH")(rowId) - 1
            Session("SDGsetTargetName") = "Target " & Session("TargetIdH")(rowId)
            Session("SDGsetIndicatorName") = Session("sNameH")(rowId)
        Else
            Session("SDGGoalidx") = Session("GoalId")(rowId) - 1
            Session("SDGsetTargetName") = "Target " & Session("TargetId")(rowId)
            Session("SDGsetIndicatorName") = Session("sName")(rowId)
        End If

        Session("DisplayParent") = "frm_SDGset.aspx"

        If Session("SDGsetGroup") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                Support.FormManager.Show(Me, "frm_SDG.aspx?Group=" & cmb_Country.SelectedValue)
            Else
                Support.FormManager.Show(Me, "frm_SDG.aspx?Group=" & Session("MMGroup"))
            End If
        Else
            If Session("MMCountry") = Nothing Then
                Session("MMCountry") = IFsRegs.Item(cmb_Country.SelectedValue).ISO3166_2Code
            End If
            'jrs 2026/01/16 Support.FormManager.Show(Me, "frm_SDG.aspx?Country=" & Session("MMCountry"))
            'Changing to friendly URL
            Dim Vpath As String
            Dim Vext As String
            Dim vfriendlyurl As String
            Dim vurl As String

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDG")

            vurl = FriendlyUrl.Href("~/frm_SDG", Session("MMCountry"))

            Response.Redirect(vurl)

        End If
    End Sub

    Private Sub DisplayOverTime(ByVal currRow As Integer)
        'JRS 09/12/02
        'Subroutine to display variable over time
        Dim bShow As Boolean

        Dim sVar As String
        Dim sDim2 As String, sDim3 As String
        Dim bGDPpct As Boolean
        bGDPpct = False

        sVar = Session("VarsInGrid")(6, currRow)
        sDim2 = Session("VarsInGrid")(7, currRow)
        sDim3 = Session("VarsInGrid")(8, currRow)

        LoadVarAllYrs(sVar, sDim2, sDim3, True)

    End Sub

    Private Sub LoadVarAllYrs(ByVal sName As String, ByVal sDim2 As String, ByVal sDim3 As String, newCols As Boolean)
        Dim idx As Integer
        Dim idim2 As Integer
        Dim idx2 As Integer
        idx2 = 1
        idx = IfsVars.Item(sName).Index
        Dim RegInd As Integer
        RegInd = cmb_Country.SelectedIndex + 1

        If newCols Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            'JRS 2/17/03
            InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))
        End If

        Session("COUNTE") = Session("COUNTE") + 1
        Dim rsVarsIdx As Integer = 0
        Dim IfsPars As New myParameters

        If idx > 0 Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sName, idx, cmb_Country.Text, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, False,
            1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"),
            Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay(sName, idx, cmb_Country.Text, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, False,
                1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"),
                Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, cmb_Country.Text, sDim2, sDim3, Session("SDGsetGroup"), cboFiles.SelectedIndex, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        Else
            Do While rsVarsIdx < Session("rsVars").rows.count
                If sName = Session("rsVars").rows(rsVarsIdx).item("VarName") And sDim2 = IIf(IsDBNull(Session("rsVars").rows(rsVarsIdx).item("Param2")), "", Session("rsVars").rows(rsVarsIdx).item("Param2")) _
                 And sDim3 = IIf(IsDBNull(Session("rsVars").rows(rsVarsIdx).item("Param3")), "", Session("rsVars").rows(rsVarsIdx).item("Param3")) Then Exit Do
                rsVarsIdx = rsVarsIdx + 1
            Loop
            If rsVarsIdx = Session("rsVars").rows.count Then Exit Sub

            IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

            gLoadComp(Session("COUNTE"), Session("rsVars").rows(rsVarsIdx).item("FormulaDet"), Session("rsVars").rows(rsVarsIdx).item("VarName"), cmb_Country.Text, sDim2, sDim3,
            "", Session("rsVars").rows(rsVarsIdx).item("FormulaId"), Nothing, cboFiles.SelectedIndex, False, False, Nothing, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(1),
            False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
            Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
            Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
            FormatFormAfterLoad(cboFiles.SelectedIndex, Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
            IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
            Session("MaxFinYearHis"))
        End If

        Session("COUNTE") = Session("COUNTE") + 1

        If idx > 0 Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sName, idx, cmb_Country.Text, RegInd, sDim2, -1, sDim3, -1, "", cboSce2.SelectedIndex, True, True, False,
            1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"),
            Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay(sName, idx, cmb_Country.Text, RegInd, sDim2, -1, sDim3, -1, "", cboSce2.SelectedIndex, True, True, False,
                1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"),
                Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, cmb_Country.Text, sDim2, sDim3, Session("SDGsetGroup"), cboFiles.SelectedIndex, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        Else
            gLoadComp(Session("COUNTE"), Session("rsVars").rows(rsVarsIdx).item("FormulaDet"), Session("rsVars").rows(rsVarsIdx).item("VarName"), cmb_Country.Text, sDim2, sDim3,
            "", Session("rsVars").rows(rsVarsIdx).item("FormulaId"), Nothing, cboSce2.SelectedIndex, False, False, Nothing, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(1),
            False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
            Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
            Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
            FormatFormAfterLoad(cboSce2.SelectedIndex, Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
            IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
            Session("MaxFinYearHis"))
        End If

        'jrs 2026/01/16 Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
        'Changing to friendly URL
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay")

        vurl = FriendlyUrl.Href("~/frm_TableDisplay")

        Response.Redirect(vurl)
    End Sub


    Private Sub mnuGroups_Click()

        'jam 2021/10/10
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        Session("lstCountry.SelCount") = 0
        If Not Session("SDGsetGroup") Then
            cswmWebMenu.Groups("mnuGroups").Caption = "Using Groups"
            lbl_countries.Text = "Groups"
            LoadGroups()
            cmb_Country.SelectedValue = "World"
        Else
            cswmWebMenu.Groups("mnuGroups").Caption = "Using Countries/&Regions"
            lbl_countries.Text = "Countries or Regions"
            LoadRegions()
            cmb_Country.SelectedValue = "Kenya"
        End If
        'cmb_Country.SelectedIndex = 0   ' Display First Item

        Session("SDGsetGroup") = Not Session("SDGsetGroup")
        'cboFiles_SelectedIndexChanged(Me, New EventArgs)

        'jrs 2011/05/06
        If Session("SDGsetGroup") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                'jam 2021/10/10
                'Support.FormManager.Show(Me, "frm_SDGset.aspx?Group=" & cmb_Country.SelectedValue)


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDGset.aspx")


                vurl = FriendlyUrl.Href("~/frm_SDGset", SDGsetPrepareForFriendlyURL(cmb_Country.SelectedValue))

                Response.Redirect(vurl)



            Else
                'jam 2021/10/10
                'Support.FormManager.Show(Me, "frm_SDGset.aspx?Group=" & Session("MMGroup"))


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDGset.aspx")


                vurl = FriendlyUrl.Href("~/frm_SDGset", SDGsetPrepareForFriendlyURL(Session("MMGroup")))

                Response.Redirect(vurl)

            End If
        Else
            'jam 2021/10/10
            'Support.FormManager.Show(Me, "frm_SDGset.aspx?Country=KE")

            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDGset.aspx")


            vurl = FriendlyUrl.Href("~/frm_SDGset", "KE")

            Response.Redirect(vurl)


        End If
    End Sub

    'jrs 2017/07/12
    Private Sub mnuHistOnly_Click()
        'jrs 2017/07/12
        Session("SDGHistoryOnly") = Not Session("SDGHistoryOnly")
        Dim i As Integer

        If Session("SDGHistoryOnly") Then
            cswmWebMenu.Groups("mnuHistoryOnly").Caption = "Back to Forecasted Indicators"
            'jrs 2017/08/25
            Session("SDGFilesSelected") = 1
            ClearValues()
            LoadValuesH()
            'jrs 2017/07/21
            grd_TableDisplay.Rows(0).Cells(1).Text = "Most Recent"
            For i = 0 To grd_TableDisplay.Rows.Count - 1
                grd_TableDisplay.Rows(i).Cells(2).Visible = False
                grd_TableDisplay.Rows(i).Cells(3).Visible = False
            Next
        Else
            cswmWebMenu.Groups("mnuHistoryOnly").Caption = "Use History Only Indicators"
            ClearValues()
            ReDim Session("Baseval")(Session("rsVars").rows.count)
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text))
            RoundValues()
            'jrs 2017/07/21
            grd_TableDisplay.Rows(0).Cells(1).Text = CStr(FirstYearForecast)
            For i = 0 To grd_TableDisplay.Rows.Count - 1
                grd_TableDisplay.Rows(i).Cells(2).Visible = True
                grd_TableDisplay.Rows(i).Cells(3).Visible = True
            Next
        End If
    End Sub

    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        If Session("SDGsetGroup") = False Then
            Dim sJscript As String, i As Integer
            sJscript = "    var RegCode = [];" & vbCr
            For i = 1 To NR
                sJscript = sJscript & "    RegCode[" & i.ToString & "] = """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """;" & vbCr
            Next

            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadRegCode", sJscript, True)

            cmb_Country.Attributes.Add("onchange", "cntrylist_click();")
        Else
            cmb_Country.Attributes.Add("onchange", "lstGroups_click();")
        End If
    End Sub

    'Function to round values
    Private Sub RoundValues()

        Dim i As Integer
        Dim j As Integer
        Dim pos As Integer, pos2 As Integer
        With grd_TableDisplay
            For i = 1 To .Rows.Count - 1
                For j = 1 To 4
                    pos = InStr(.Rows(i).Cells(j).Text, ">")
                    pos2 = InStr(pos + 1, .Rows(i).Cells(j).Text, "<")

                    If pos > 0 Then
                        If IsNumeric(Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1)) Then
                            Select Case Abs(Val(Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1)))
                                Case Is > 1000
                                    .Rows(i).Cells(j).Text = Replace(.Rows(i).Cells(j).Text, Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1), Round(Val(Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1)), 0).ToString)
                                Case Is > 100
                                    .Rows(i).Cells(j).Text = Replace(.Rows(i).Cells(j).Text, Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1), Round(Val(Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1)), 1).ToString)
                                Case Is > 10
                                    .Rows(i).Cells(j).Text = Replace(.Rows(i).Cells(j).Text, Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1), Round(Val(Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1)), 2).ToString)
                                Case Is > 1
                                    .Rows(i).Cells(j).Text = Replace(.Rows(i).Cells(j).Text, Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1), Round(Val(Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1)), 3).ToString)
                                Case Is >= 0
                                    .Rows(i).Cells(j).Text = Replace(.Rows(i).Cells(j).Text, Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1), Round(Val(Mid(.Rows(i).Cells(j).Text, pos + 1, pos2 - pos - 1)), 4).ToString)
                            End Select
                        End If
                    End If
                Next

                '    j = 4
                '    If IsNumeric(.Rows(i).Cells(j).Text) Then
                '        Select Case Abs(Val(.Rows(i).Cells(j).Text))
                '            Case Is > 1000
                '                .Rows(i).Cells(j).Text = Round(Val(.Rows(i).Cells(j).Text), 0)
                '            Case Is > 100
                '                .Rows(i).Cells(j).Text = Round(Val(.Rows(i).Cells(j).Text), 1)
                '            Case Is > 10
                '                .Rows(i).Cells(j).Text = Round(Val(.Rows(i).Cells(j).Text), 2)
                '            Case Is > 1
                '                .Rows(i).Cells(j).Text = Round(Val(.Rows(i).Cells(j).Text), 3)
                '            Case Is >= 0
                '                .Rows(i).Cells(j).Text = Round(Val(.Rows(i).Cells(j).Text), 4)
                '        End Select
                '    End If

            Next i
        End With

    End Sub

    Private Sub CreateMenu()
        wmPopUp.UserData = "jsolorza::976739975"
        'jam 2021/10/10
        'wmPopUp.ClearPixelImage = "blank.gif"
        'wmPopUp.PopupIcon = "./images/Popup.gif"
        'wmPopUp.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmPopUp.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmPopUp.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmPopUp.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmPopUp.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/10/10
        wmPopUp.ClearPixelImage = Page.ResolveUrl("~/blank.gif ")
        wmPopUp.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmPopUp.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmPopUp.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmPopUp.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmPopUp.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmPopUp.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")


        wmPopUp.ScrollEnabled = True
        AddHandler wmPopUp.ServerItemClick, AddressOf ItemClickHandlerContext
        wmPopUp.MenuBar.ShowOnMouseOver = True

        'jrs 2012/01/26
        wmPopUp.ButtonFont.Size = "12px"
        wmPopUp.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp.Groups.Add()

        cswmGroup1.ID = "mnuSDGSetContext"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display Graph"


        wmPopUp.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp.ItemFont.PaddingLeft = 4

        CreateMenu1()

        CreateMenu2()

    End Sub

    Private Sub CreateMenu1()
        wmPopUp1.UserData = "jsolorza::976739975"
        'jam 2021/10/10
        'wmPopUp1.ClearPixelImage = "blank.gif"
        'wmPopUp1.PopupIcon = "./images/Popup.gif"
        'wmPopUp1.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmPopUp1.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmPopUp1.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmPopUp1.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmPopUp1.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/10/10
        wmPopUp1.ClearPixelImage = Page.ResolveUrl("~/blank.gif ")
        wmPopUp1.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmPopUp1.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmPopUp1.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmPopUp1.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmPopUp1.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmPopUp1.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")




        wmPopUp1.ScrollEnabled = True
        AddHandler wmPopUp1.ServerItemClick, AddressOf ItemClickHandlerContext
        wmPopUp1.MenuBar.ShowOnMouseOver = True

        'jrs 2012/01/26
        wmPopUp1.ButtonFont.Size = "12px"
        wmPopUp1.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp1.Groups.Add()

        cswmGroup1.ID = "mnuSDGSetContext1"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Over Time"

        wmPopUp1.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp1.ItemFont.PaddingLeft = 4

    End Sub

    Private Sub CreateMenu2()
        wmPopUp2.UserData = "jsolorza::976739975"
        'wmPopUp2.ClearPixelImage = "blank.gif"
        'wmPopUp2.PopupIcon = "./images/Popup.gif"
        'wmPopUp2.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmPopUp2.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmPopUp2.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmPopUp2.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmPopUp2.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/10/10
        wmPopUp2.ClearPixelImage = Page.ResolveUrl("~/blank.gif ")
        wmPopUp2.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmPopUp2.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmPopUp2.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmPopUp2.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmPopUp2.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmPopUp2.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")



        wmPopUp2.ScrollEnabled = True
        AddHandler wmPopUp2.ServerItemClick, AddressOf ItemClickHandlerContext
        wmPopUp2.MenuBar.ShowOnMouseOver = True

        'jrs 2012/01/26
        wmPopUp2.ButtonFont.Size = "12px"
        wmPopUp2.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp2.Groups.Add()

        cswmGroup1.ID = "mnuSDGSetContext2"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Edit Target"

        wmPopUp2.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp2.ItemFont.PaddingLeft = 4

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'jam 2021/10/10
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String


        CreateMenu()


        'jam 2021/10/10
        If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Request.QueryString("Group")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                othCountry = ""

            End If




            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDGSet.aspx")


            If othCountry <> "" Then
                vurl = FriendlyUrl.Href("~/frm_SDGSet", SDGsetPrepareForFriendlyURL(othCountry))

                Response.Redirect(vurl)


            End If


        End If

        'jam 2021/10/10
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String

        If segments.Count = 1 Then

            vCountry = segments(0)

        Else
            vCountry = Session("MMCountry")
            Session("vCountry") = vCountry

        End If

        Session("vCountry") = vCountry




        'jam 2024/11/19
        'If Not Page.IsPostBack Then
        If Not Page.IsPostBack Then







            'jam 2024/11/18
            If Session("SDGsetcmbGoalIdx") Is Nothing Then
                Session("SDGsetcmbGoalIdx") = 1
                Session("vUsrDefCat") = "Poverty"

                'Else
                '    cmb_Goal.SelectedIndex = Session("SDGsetcmbGoalIdx") - 1

            End If

            'jam 2024/11/18
            LoadGoals()

            'Need to open list to know number of rows
            dbIFsVar = Session("PutFdrvRun") & "IFsVar.mdb"
            'jam 2024/11/19 adding UsrDefCat filter
            'SetDataTablefromAccess(dbIFsVar, "SELECT VarName, UsrDefCat, Param2, Param3, Formula, FormulaId, FormulaDet, DisplayVarName FROM VarLists WHERE ListName = ""SDGs"" ORDER BY ListOrder", "", Session("rsVars"))
            'SetDataTablefromAccess(dbIFsVar, "SELECT VarName, UsrDefCat, Param2, Param3, Formula, FormulaId, FormulaDet, DisplayVarName FROM VarLists WHERE ListName = ""SDGs History Only"" ORDER BY ListOrder", "", Session("rsVarsH"))

            SetDataTablefromAccess(dbIFsVar, "SELECT VarName, UsrDefCat, Param2, Param3, Formula, FormulaId, FormulaDet, DisplayVarName FROM VarLists WHERE ListName = ""SDGs"" " & " AND UsrDefCat = " & """" & Session("vUsrDefCat") & """" & " ORDER BY ListOrder", "", Session("rsVars"))
            SetDataTablefromAccess(dbIFsVar, "Select VarName, UsrDefCat, Param2, Param3, Formula, FormulaId, FormulaDet, DisplayVarName FROM VarLists WHERE ListName = ""SDGs History Only"" " & " AND UsrDefCat = " & """" & Session("vUsrDefCat") & """" & " ORDER BY ListOrder", "", Session("rsVarsH"))


            'jrs 2021/06/01 PreLoad SDG Formulas
            If Session("SDGFormulas") Is Nothing Then
                LoadSDGFormulas()
            End If
            Dim i As Integer
            For i = 0 To Session("rsVars").Rows.Count - 1 'jam 2024/11/22 + 27
                Dim rr As New TableRow
                Dim j As Integer
                For j = 1 To 11
                    Dim cc As New TableCell
                    cc.BackColor = Color.White
                    If j > 5 Then
                        cc.Visible = False
                    ElseIf j > 1 Then
                        cc.HorizontalAlign = HorizontalAlign.Right
                    End If
                    rr.Cells.Add(cc)
                Next
                grd_TableDisplay.Rows.Add(rr)
            Next
            'Reset first year if History plus forecast
            If Session("FirstYear") = FirstYearHistory And UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
            End If

            grd_TableDisplay.Rows(0).Cells(1).Text = CStr(Session("FirstYear"))
            ReDim Session("VarsInGrid")(10, grd_TableDisplay.Rows.Count)




            If Session("TargetisRelative") Is Nothing Then
                LoadTargets()
                'jrs 2017/07/12
                LoadTargetsH()



            End If







            'Load Regions
            If Session("SDGsetGroup") Is Nothing Then
                Session("SDGsetGroup") = False
            End If
            If Session("SDGHistoryOnly") Is Nothing Then
                Session("SDGHistoryOnly") = False
            End If
            If Session("SDGsetGroup") = False Then
                LoadRegions()
            Else
                LoadGroups()
            End If
            'Load Files
            LoadFiles(gRetrieveFiles(Session("PutFdrvRun2"), Session("WorkingFile"), Session("RunFiles"), Session("FirstYear"), Session("FilesSelected"), Session("ShortSce"), Session("IFsRunFiles")), cboFiles)
            If cboFiles.Items.Count > 0 Then
                If Session("SDGsetcboFilesIdx") Is Nothing Then
                    cboFiles.SelectedIndex = 1
                Else
                    cboFiles.SelectedIndex = Session("SDGsetcboFilesIdx")
                End If
            End If
            If cboSce2.Items.Count > 0 Then
                If Session("SDGsetcboSce2Idx") Is Nothing Then
                    cboSce2.SelectedIndex = 0
                Else
                    cboSce2.SelectedIndex = Session("SDGsetcboSce2Idx")
                End If
            End If


            '   Establish Year List Box
            Dim finam As String
            If Session("mnuSQLite") = "True" Then
                finam = Session("PutFdrvRun") + "ifsBase.run.db"
            Else
                finam = Session("PutFdrvRun") + "ifsBase.run"
            End If
            Dim filNum As Integer, VLN As Long, Number As Single, VarVal As Double
            If Session("mnuSQLite") = "True" Then
                Dim working As IStorage
                Dim storageSvc As IStorageService = New StorageService(finam)
                working = storageSvc.Open()
                VarVal = working.LastYear
            Else
                filNum = FreeFile()
                FileOpen(filNum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
                VLN = VarPoint(PosModYear)
                Session("RECORD") = (VLN) * 4 - 3
                FileGet(filNum, Number, Session("RECORD"))
                VarVal = Number
                FileClose(filNum)

            End If
            Dim i2 As Integer
            For i2 = Session("FirstYear") To VarVal
                Cmb_Year.Items.Add(LTrim(Str$(i2)))
            Next i2

            'jrs 2021/04/12 show 15 years into the future or multiple of 5
            Dim iDefYr As Integer
            iDefYr = Session("FirstYear") + 15
            iDefYr = iDefYr - (iDefYr Mod 5) + 5

            'Display first elements
            If Cmb_Year.Items.Count > iDefYr - Session("FirstYear") Then
                If Session("SDGsetcmbYearIdx") Is Nothing Then
                    Cmb_Year.SelectedIndex = iDefYr - Session("FirstYear")   ' Display First Item
                Else
                    Cmb_Year.SelectedIndex = Session("SDGsetcmbYearIdx")
                End If
            ElseIf Cmb_Year.Items.Count > 0 Then
                If Session("SDGsetcmbYearIdx") Is Nothing Then
                    Cmb_Year.SelectedIndex = Cmb_Year.Items.Count - 1
                Else
                    Cmb_Year.SelectedIndex = Session("SDGsetcmbYearIdx")
                End If
            End If

            'jrs 2021/04/12
            grd_TableDisplay.Rows(0).Cells(2).Text = "Reference Scenario " & Cmb_Year.Text
            grd_TableDisplay.Rows(0).Cells(3).Text = "Intervention Scenario " & Cmb_Year.Text

            If Session("FilesCol") Is Nothing Then
                FilesCol = New Collection
            Else
                FilesCol = Session("FilesCol")
            End If
            If Session("dfCol") Is Nothing Then
                dfCol = New Collection
            Else
                dfCol = Session("dfCol")
            End If


            If Session("SDGsetGroup") Is Nothing Then
                Session("SDGsetGroup") = False
            End If



        End If


        cswmWebMenu.UserData = "jsolorza::    976739975"
        'jam 2021/10/10
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/06/02
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")

        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.GeneratedPrefix = "cswmWebMenu"

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
        cswmGroup1.ID = "Group_ce9f"
        'jrs 09/29/04
        'jrs 2011/09/26
        Session("DisplayParent") = "frm_MainMenu.aspx"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        'jam 2021/10/10
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"
        cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl(Session("DisplayParent")) & "';"



        cswmGroup1 = cswmWebMenu.Groups.Add()
        If Not Session("SDGsetGroup") Then
            cswmGroup1.Caption = "Using Country"
        Else
            cswmGroup1.Caption = "Using Groups"
        End If
        cswmGroup1.ID = "mnuGroups"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Switch"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        If Session("SDGHistoryOnly") Then
            cswmGroup1.Caption = "Back to Forecasted Indicators"
            'jrs 2017/07/21
            grd_TableDisplay.Rows(0).Cells(1).Text = "Most Recent"
            For i = 0 To grd_TableDisplay.Rows.Count - 1
                grd_TableDisplay.Rows(i).Cells(2).Visible = False
                grd_TableDisplay.Rows(i).Cells(3).Visible = False
            Next
        Else
            cswmGroup1.Caption = "Use History Only Indicators"
        End If
        cswmGroup1.ID = "mnuHistoryOnly"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Switch2"

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16007);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jam 2021/10/10
        'If Not IsNothing(Request.QueryString("Country")) Then
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(vCountry)) And Not Session("SDGsetGroup") Then
            'jam 2021/10/10
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> vCountry Or Not Page.IsPostBack Then
                'jam 2021/10/10
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = vCountry
                'jrs 2012/08/15 Session("SDGsetGroup") = False
                If Session("SDGsetGroup") = True Then
                    cswmWebMenu.Groups("mnuGroups").Caption = "Using Countries/&Regions"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions()
                    Session("SDGsetGroup") = False
                End If

                'Validate
                Dim rs As DataTable
                SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT NewRegion FROM [Country Translation] as c, " &
            Regionalization & " as r where c.Country = r.Country AND [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs)
                If rs.Rows.Count = 0 Then
                    Session("MMCountry") = ""
                    rs = Nothing
                Else
                    Session("cNumber") = rs.Rows(0).Item("NewRegion")
                End If
            End If
            'jam 2021/10/10
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(vCountry)) And Session("SDGsetGroup") Then
            'jam 2021/10/10
            'If cmb_Country.SelectedValue <> Request.QueryString("Group") Or Not Page.IsPostBack Then
            If cmb_Country.SelectedValue <> vCountry Or Not Page.IsPostBack Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/10/10
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(vCountry, False, Me)
                If gIdx > 0 Then
                    cmb_Country.SelectedIndex = gIdx - 1
                    Session("SDGsetcmbCountryIdx") = cmb_Country.SelectedIndex
                    Session("MMGroup") = Request.QueryString("Group")
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("SDGsetGroup") = True Then
                    'jrs 2017/08/31 if not postback it will run in the block below
                    If Page.IsPostBack Then
                        cmb_Country_SelectedIndexChanged(cmb_Country, New System.EventArgs)
                    End If
                Else
                    mnuGroups_Click()
                End If
            End If



        End If
        PackJScritpBuilder()

        'KIA 63008 ADD THE FOLLOWING IF BLOCK
        If Not Session("cNumber") Is Nothing Then
            cmb_Country.SelectedIndex = CInt(Session("cNumber")) - 1
            Session.Remove("cNumber")
            'RefreshDisplay()
            Session("SDGsetcmbCountryIdx") = cmb_Country.SelectedIndex
            cmb_Country_SelectedIndexChanged(Me, New System.EventArgs)
        ElseIf Not Page.IsPostBack Then
            ' goes into Procedure summary
            If cmb_Country.Items.Count > 0 Then
                If Session("SDGsetcmbCountryIdx") Is Nothing Then
                    cmb_Country.SelectedIndex = 84   'Kenya Display First Item
                Else
                    cmb_Country.SelectedIndex = Session("SDGsetcmbCountryIdx")
                End If
                cmb_Country_SelectedIndexChanged(Me, New EventArgs)
            End If
        End If
    End Sub

    Private Sub cmb_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
        If Not cmb_Country.Visible Then Exit Sub

        If Cmb_Year.SelectedIndex = -1 Then Exit Sub

        Session("SDGsetcmbCountryIdx") = cmb_Country.SelectedIndex

        If Not Session("SDGHistoryOnly") Then

            ClearValues()
            ReDim Session("Baseval")(Session("rsVars").rows.count)

            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text))

            RoundValues()
        Else
            LoadValuesH()
        End If
    End Sub

    Private Sub cboFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiles.SelectedIndexChanged
        'jrs 2005/05/16
        If Not cboFiles.Visible Then Exit Sub

        If Cmb_Year.SelectedIndex = -1 Then Exit Sub
        'jrs 2005/11/30
        Session("SDGsetcboFilesIdx") = cboFiles.SelectedIndex

        If Not Session("SDGHistoryOnly") Then
            ClearValues()
            ReDim Session("Baseval")(Session("rsVars").rows.count)

            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text))

            RoundValues()
        Else
            LoadValuesH()
        End If
    End Sub

    Private Sub Cmb_Year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Year.SelectedIndexChanged
        If Not Cmb_Year.Visible Then Exit Sub

        If cmb_Country.SelectedIndex = -1 Then Exit Sub

        'jrs 2005/11/30
        Session("SDGsetcmbYearIdx") = Cmb_Year.SelectedIndex

        If Not Session("SDGHistoryOnly") Then

            ClearValues()
            ReDim Session("Baseval")(Session("rsVars").rows.count)

            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text))

            RoundValues()
        Else
            LoadValuesH()
        End If

        grd_TableDisplay.Rows(0).Cells(2).Text = "Reference Scenario " & Cmb_Year.Text
        grd_TableDisplay.Rows(0).Cells(3).Text = "Intervention Scenario " & Cmb_Year.Text
    End Sub

    Protected Sub cboSce2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSce2.SelectedIndexChanged
        If Not cboSce2.Visible Then Exit Sub

        If cboSce2.SelectedIndex = -1 Then Exit Sub
        'jrs 2005/11/30
        Session("SDGsetcboSce2Idx") = cboSce2.SelectedIndex

        If Not Session("SDGHistoryOnly") Then

            ClearValues()
            ReDim Session("Baseval")(Session("rsVars").rows.count)

            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text))

            RoundValues()
        Else
            LoadValuesH()
        End If
    End Sub

    'jrs 2021/06/01
    'Creates a collection of Formulas to be used in SDT Table display
    'To avoid having to go to access every time
    Private Sub LoadSDGFormulas()
        'Create all needed formulas
        Dim rsIdx As Integer = 0
        Dim myCol As Collection = New Collection

        Do While rsIdx < Session("rsVars").Rows.Count
            If CBool(Session("rsVars").Rows(rsIdx).Item("Formula")) Then
                Dim f As Formula
                f = New Formula
                f.init(Session("rsVars").Rows(rsIdx).Item("FormulaDet"))
                f.Name = Session("rsVars").Rows(rsIdx).Item("VarName")
                f.Dimension = cmb_Country.Text
                f.Dimension2 = IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("Param2")), "", Session("rsVars").Rows(rsIdx).Item("Param2"))
                f.Dimension3 = IIf(IsDBNull(Session("rsVars").Rows(rsIdx).Item("Param3")), "", Session("rsVars").Rows(rsIdx).Item("Param3"))
                f.Unit = ""

                myCol.Add(f, Session("rsVars").Rows(rsIdx).Item("FormulaId"))
            End If

            rsIdx = rsIdx + 1
        Loop


        Dim rsComp As DataTable
        'jam 2024/11/19
        'SetDataTablefromAccess(dbIFsVar, "SELECT f.FormulaId, Code, FormulaName, CodeVar, f.VarName, f.Dim1, f.Dim2, f.Dim3, IndexListName " &
        '"FROM VarListsFormulas as f, VarLists as v " &
        '"WHERE f.FormulaId = v.FormulaId " &
        '"AND ListName = ""SDGs"" ORDER BY f.FormulaId, [CodeVar]", "", rsComp)

        SetDataTablefromAccess(dbIFsVar, "SELECT f.FormulaId, Code, FormulaName, CodeVar, f.VarName, f.Dim1, f.Dim2, f.Dim3, IndexListName " &
        "FROM VarListsFormulas as f, VarLists as v " &
        "WHERE f.FormulaId = v.FormulaId " &
        "AND UsrDefCat = " & """" & Session("vUsrDefCat") & """" &
        "AND ListName = ""SDGs"" ORDER BY f.FormulaId, [CodeVar]", "", rsComp)


        '"AND UsrDefCat = " & """" & Session("vUsrDefCat") & """"  &



        Dim FormulaId As Integer = 0
        Dim sVarName As String, sCodeVar As String
        Dim sD1 As String, sD2 As String, sD3 As String

        rsIdx = 0

        Do While rsIdx < rsComp.Rows.Count
            FormulaId = rsComp.Rows(rsIdx).Item("FormulaId")
            sVarName = rsComp.Rows(rsIdx).Item("VarName")
            sCodeVar = rsComp.Rows(rsIdx).Item("CodeVar")
            If IsDBNull(rsComp.Rows(rsIdx).Item("Dim1")) Then
                sD1 = ""
            Else
                sD1 = Trim(rsComp.Rows(rsIdx).Item("Dim1"))
            End If
            If IsDBNull(rsComp.Rows(rsIdx).Item("Dim2")) Then
                sD2 = ""
            Else
                sD2 = Trim(rsComp.Rows(rsIdx).Item("Dim2"))
            End If
            If IsDBNull(rsComp.Rows(rsIdx).Item("Dim3")) Then
                sD3 = ""
            Else
                sD3 = Trim(rsComp.Rows(rsIdx).Item("Dim3"))
            End If

            'check if it's an index
            If myCol(FormulaId.ToString).IndexVarListName = "" AndAlso Not IsDBNull(rsComp.Rows(rsIdx).Item("IndexListName")) Then
                myCol(FormulaId.ToString).IndexVarListName = rsComp.Rows(rsIdx).Item("IndexListName")
            End If

            myCol(FormulaId.ToString).disVar.setDataDetS(sCodeVar, sVarName)
            myCol(FormulaId.ToString).disVar.setDataDet1S(sCodeVar, sD1)
            myCol(FormulaId.ToString).disVar.setDataDet2S(sCodeVar, sD2)
            myCol(FormulaId.ToString).disVar.setDataDet3S(sCodeVar, sD3)

            rsIdx = rsIdx + 1
        Loop

        Session("SDGFormulas") = myCol
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/10/10
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function SDGsetPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        SDGsetPrepareForFriendlyURL = tmp
    End Function
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''     jam 2024/11/18 Index selected for Goal Dropdown
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmb_Goal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Goal.SelectedIndexChanged
        Dim vpivot As String
        Dim vindex As Integer

        vindex = 0

        If Not cmb_Goal.Visible Then Exit Sub

        If Cmb_Year.SelectedIndex = -1 Then Exit Sub

        Session("SDGsetcmbGoalIdx") = cmb_Goal.SelectedIndex + 1

        vpivot = Session("vGoals")(cmb_Goal.SelectedIndex)

        vindex = vpivot.IndexOf(": ") '+ vpivot.Length

        vpivot = vpivot.Substring(vindex + 2)  ': plus space before Goal Name

        Session("vUsrDefCat") = vpivot

        'jam 2024/11/20
        If cmb_Country.Items.Count > 0 Then
            If Session("SDGsetcmbCountryIdx") Is Nothing Then
                cmb_Country.SelectedIndex = 0
            Else
                cmb_Country.SelectedIndex = Session("SDGsetcmbCountryIdx")
            End If
        End If




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'Need to open list to know number of rows
        dbIFsVar = Session("PutFdrvRun") & "IFsVar.mdb"
        SetDataTablefromAccess(dbIFsVar, "SELECT VarName, UsrDefCat, Param2, Param3, Formula, FormulaId, FormulaDet, DisplayVarName FROM VarLists WHERE ListName = ""SDGs"" " & " AND UsrDefCat = " & """" & Session("vUsrDefCat") & """" & " ORDER BY ListOrder", "", Session("rsVars"))
        SetDataTablefromAccess(dbIFsVar, "Select VarName, UsrDefCat, Param2, Param3, Formula, FormulaId, FormulaDet, DisplayVarName FROM VarLists WHERE ListName = ""SDGs History Only"" " & " AND UsrDefCat = " & """" & Session("vUsrDefCat") & """" & " ORDER BY ListOrder", "", Session("rsVarsH"))


        'jrs 2021/06/01 PreLoad SDG Formulas
        'If Session("SDGFormulas") Is Nothing Then
        LoadSDGFormulas()
        'End If

        Dim i As Integer
        For i = 0 To Session("rsVars").Rows.Count - 1  'jam 2024/11/22 + 27
            Dim rr As New TableRow
            Dim j As Integer
            For j = 1 To 11
                Dim cc As New TableCell
                cc.BackColor = Color.White
                If j > 5 Then
                    cc.Visible = False
                ElseIf j > 1 Then
                    cc.HorizontalAlign = HorizontalAlign.Right
                End If
                rr.Cells.Add(cc)
            Next
            grd_TableDisplay.Rows.Add(rr)
        Next

        grd_TableDisplay.Rows(0).Cells(1).Text = CStr(Session("FirstYear"))
        ReDim Session("VarsInGrid")(10, grd_TableDisplay.Rows.Count)

        'If Session("TargetisRelative") Is Nothing Then
        LoadTargets()
        'jrs 2017/07/12
        LoadTargetsH()
        'End If

        'jam 2024/11/22
        'If Session("SDGsetGroup") = False Then
        '    LoadRegions()
        'Else
        '    LoadGroups()
        'End If

        'jam 2024/11/26 commented because combo contained duplicated values
        'Load Files
        'LoadFiles(gRetrieveFiles(Session("PutFdrvRun2"), Session("WorkingFile"), Session("RunFiles"), Session("FirstYear"), Session("FilesSelected"), Session("ShortSce"), Session("IFsRunFiles")), cboFiles)
        If cboFiles.Items.Count > 0 Then
            If Session("SDGsetcboFilesIdx") Is Nothing Then
                cboFiles.SelectedIndex = 1
            Else
                cboFiles.SelectedIndex = Session("SDGsetcboFilesIdx")
            End If
        End If
        If cboSce2.Items.Count > 0 Then
            If Session("SDGsetcboSce2Idx") Is Nothing Then
                cboSce2.SelectedIndex = 0
            Else
                cboSce2.SelectedIndex = Session("SDGsetcboSce2Idx")
            End If
        End If


        ''   Establish Year List Box
        'Dim finam As String
        'If Session("mnuSQLite") = "True" Then
        '    finam = Session("PutFdrvRun") + "ifsBase.run.db"
        'Else
        '    finam = Session("PutFdrvRun") + "ifsBase.run"
        'End If
        'Dim filNum As Integer, VLN As Long, Number As Single, VarVal As Double
        'If Session("mnuSQLite") = "True" Then
        '    Dim working As IStorage
        '    Dim storageSvc As IStorageService = New StorageService(finam)
        '    working = storageSvc.Open()
        '    VarVal = working.LastYear
        'Else
        '    filNum = FreeFile()
        '    FileOpen(filNum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
        '    VLN = VarPoint(PosModYear)
        '    Session("RECORD") = (VLN) * 4 - 3
        '    FileGet(filNum, Number, Session("RECORD"))
        '    VarVal = Number
        '    FileClose(filNum)

        'End If
        'Dim i2 As Integer
        'For i2 = Session("FirstYear") To VarVal
        '    Cmb_Year.Items.Add(LTrim(Str$(i2)))
        'Next i2

        'jrs 2021/04/12 show 15 years into the future or multiple of 5
        Dim iDefYr As Integer
        iDefYr = Session("FirstYear") + 15
        iDefYr = iDefYr - (iDefYr Mod 5) + 5

        ''Display first elements
        If Cmb_Year.Items.Count > iDefYr - Session("FirstYear") Then
            If Session("SDGsetcmbYearIdx") Is Nothing Then
                Cmb_Year.SelectedIndex = iDefYr - Session("FirstYear")   ' Display First Item
            Else
                Cmb_Year.SelectedIndex = Session("SDGsetcmbYearIdx")
            End If
        ElseIf Cmb_Year.Items.Count > 0 Then
            If Session("SDGsetcmbYearIdx") Is Nothing Then
                Cmb_Year.SelectedIndex = Cmb_Year.Items.Count - 1
            Else
                Cmb_Year.SelectedIndex = Session("SDGsetcmbYearIdx")
            End If
        End If

        ''jrs 2021/04/12
        grd_TableDisplay.Rows(0).Cells(2).Text = "Reference Scenario " & Cmb_Year.Text
        grd_TableDisplay.Rows(0).Cells(3).Text = "Intervention Scenario " & Cmb_Year.Text

        'If Session("FilesCol") Is Nothing Then
        '    FilesCol = New Collection
        'Else
        '    FilesCol = Session("FilesCol")
        'End If
        'If Session("dfCol") Is Nothing Then
        '    dfCol = New Collection
        'Else
        '    dfCol = Session("dfCol")
        'End If


        'If Session("SDGsetGroup") Is Nothing Then
        '    Session("SDGsetGroup") = False
        'End If





        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'jam 2024/11/20

        If Not Session("SDGHistoryOnly") Then

            ClearValues()
            ReDim Session("Baseval")(Session("rsVars").rows.count)

            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text))

            RoundValues()
        Else
            LoadValuesH()
        End If
    End Sub
End Class
