Option Strict Off
Option Explicit On


'Namespace IFsWeb


Public Module IFsVarInit

        Public Sub IFsVarIni(ByRef PutFdrvRun As String, ByRef VDisplayUnitsName As Object) 'jrs 2016/11/09 added parameter VDisplayUnitsName, this one needs to change
            Dim TEMP1 As Object
            Dim Temp2 As Object
            Dim temp As Object
            Dim pcnt As Object
            Dim iloop As Object
            Dim NVShort As Object
            Dim InFile As String
            Dim Msg As String
            Static SaveHelpIDEqn As Object
            '   Open IFsVar.MDB and Fill Variable Description Fields
            '   ----------------------------------------------------

            Msg = "There is an error in init, probably associated with opening IFsVar.mdb"
            InFile = PutFdrvRun & "IFSVAR.mdb"
            Dim valtbl7 As DataTable
            '    msg$ = "Have opened ifsvar.mdb"
            '    MsgBox msg$

            '   Compute Number of Variables in Ifsvar.mdb
            '   -----------------------------------------

            Msg = "There is an error in init, probably associated with using IFsVar.mdb"
        'jam 2025/10/30 Unifying Database opening mode
        'jam 2025/08/10 Improving reading sqlite files
        SetDataTablefromAccess(InFile, "SELECT count(*) As Expr1 from IFSVAR", "", valtbl7, "", True)
        'SetDataTablefromAccessJournal(InFile, "SELECT count(*) As Expr1 from IFSVAR", "", valtbl7)

        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        NumbVars = CInt(valtbl7.Rows(0).Item("Expr1"))

            'jrs 2012/01/31 added preserve to avoid loosing pointers even for a sec
            ReDim Preserve V(NumbVars)
            ReDim Preserve VScenario(NumbVars)
            ReDim Preserve VDef(NumbVars)
            ReDim Preserve VCat(NumbVars)
            ReDim Preserve VUnits(NumbVars)
            ReDim Preserve VCurrency(NumbVars)
            ReDim Preserve VShort(NumbVars)
            ReDim Preserve VMin(NumbVars)
            ReDim Preserve VMax(NumbVars)
            ReDim Preserve VType(NumbVars)
            ReDim Preserve VAggregation(NumbVars)
            'jrs 2014/09/25
            ReDim Preserve VAggregation2(NumbVars)
            'jrs 2014/10/08
            ReDim Preserve VNoTot2(NumbVars)

            ReDim Preserve VDim1(NumbVars)
            ReDim Preserve VDim2(NumbVars)
            ReDim Preserve VDim3(NumbVars)
            ReDim Preserve VarPoint(NumbVars)
            ReDim Preserve VHelpID(NumbVars)
            ReDim Preserve VCatName(NumbVars)
            ReDim Preserve VHelpIDEqn(NumbVars)
            'JRS 05/08/03 HISTORIC ANALOG
            ReDim Preserve VHistoricAnalog(NumbVars)
            'jrs 2008/06/12
            ReDim Preserve VUsrDefCat(NumbVars)
            'jrs 2005/08/19
            ReDim Preserve VExpl(NumbVars)
            'jrs 2011/04/01
            ReDim Preserve VDisplayName(NumbVars), VDisplayUnitsName(NumbVars), VNumDecimals(NumbVars)

            '----Scenario Change August 2001----
            ReDim Preserve VScenCat(NumbVars)
            ReDim Preserve VShiftUp(NumbVars)
            ReDim Preserve VShiftDown(NumbVars)
            ReDim Preserve VShiftYears(NumbVars)

            'jrs 2019/07/11
            ReDim Preserve VHidePrivateVars(NumbVars)

        '   Read and Process Ifsvar.mdb
        '   ---------------------------
        'jam 2025/10/30 Unifying Database opening mode
        'jam 2025/08/10 Improving to read sqlite files
        'jrs 2023/11/17 added UPPER()
        SetDataTablefromAccess(InFile, "SELECT * from IFSVAR ORDER BY UPPER(NAME)", "", valtbl7, "", True)
        'SetDataTablefromAccessJournal(InFile, "SELECT * from IFSVAR ORDER BY UPPER(NAME)", "", valtbl7)
        'UPGRADE_WARNING: Couldn't resolve default property of object NVShort. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        NVShort = 0
        'jrs 2025/10/31
        Dim bContainsDisplayName As Boolean
        bContainsDisplayName = valtbl7.Columns.Contains("Display Name")
        Dim bContainsNumDecimals As Boolean
        bContainsNumDecimals = valtbl7.Columns.Contains("NumDecimals")
        Dim bContainsDisplayUnits As Boolean
        bContainsDisplayUnits = valtbl7.Columns.Contains("Display Units")
        Dim bContainsDimension3 As Boolean
        bContainsDimension3 = valtbl7.Columns.Contains("Dimension3")

        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For iloop = 1 To NumbVars
            'jrs 2024/03/07
            If IfsVars.Count < NumbVars Then
                'jrs 2005/11/18
                IfsVars.Add(valtbl7.Rows(iloop - 1).Item("Name"), iloop)
            End If

            V(iloop) = valtbl7.Rows(iloop - 1).Item("Name")
            VScenario(iloop) = valtbl7.Rows(iloop - 1).Item("SCENARIO NAME") & ""
            'jam 2011/09/25 VDef(iloop) = valtbl7.Rows(iloop - 1).Item("Definition")
            'JAM 2011-09-25 Replacing three and two blank spaces
            VDef(iloop) = Replace(Replace(valtbl7.Rows(iloop - 1).Item("Definition"), "   ", " "), "  ", " ")
            VType(iloop) = valtbl7.Rows(iloop - 1).Item("Type")
            VDim1(iloop) = valtbl7.Rows(iloop - 1).Item("Dimension1")
            VDim2(iloop) = valtbl7.Rows(iloop - 1).Item("Dimension2")
            'jrs 2025/10/31
            If bContainsDimension3 Then
                VDim3(iloop) = valtbl7.Rows(iloop - 1).Item("Dimension3")
            End If
            VCat(iloop) = valtbl7.Rows(iloop - 1).Item("SUBMODULE")
            VCatName(iloop) = valtbl7.Rows(iloop - 1).Item("SUBMODULE NAME")
            'jrs 2005/08/19
            If Not IsDBNull(valtbl7.Rows(iloop - 1).Item("Parameter Explanation")) Then
                VExpl(iloop) = valtbl7.Rows(iloop - 1).Item("Parameter Explanation")
            End If
            'jrs 2022/06/15 compatibility with old version
            If valtbl7.Columns.Contains("NoTotal2") Then
                'jrs 2014/10/08
                If Not IsDBNull(valtbl7.Rows(iloop - 1).Item("NoTotal2")) Then
                    If CBool(valtbl7.Rows(iloop - 1).Item("NoTotal2")) Then
                        VNoTot2(iloop) = True
                    End If
                End If
            End If

            '----Scenario Change August 2001----
            VScenCat(iloop) = valtbl7.Rows(iloop - 1).Item("Category")
            VShiftUp(iloop) = valtbl7.Rows(iloop - 1).Item("Shift Up")
            VShiftDown(iloop) = valtbl7.Rows(iloop - 1).Item("Shift Down")
            If Not IsDBNull(valtbl7.Rows(iloop - 1).Item("Shift Years")) Then
                VShiftYears(iloop) = valtbl7.Rows(iloop - 1).Item("Shift Years")
            Else
                VShiftYears(iloop) = 0
            End If

            If ModelLoad = "LoadTERRA" Then
                If valtbl7.Rows(iloop - 1).Item("SUBMODULE").Value = 1 Then VCat(iloop) = 3
                If valtbl7.Rows(iloop - 1).Item("SUBMODULE") = 2 Then VCat(iloop) = 3
                If valtbl7.Rows(iloop - 1).Item("SUBMODULE") = 3 Then VCat(iloop) = 4
                If valtbl7.Rows(iloop - 1).Item("SUBMODULE") = 6 Then VCat(iloop) = 1
                If valtbl7.Rows(iloop - 1).Item("SUBMODULE") = 7 Then VCat(iloop) = 2
                If valtbl7.Rows(iloop - 1).Item("SUBMODULE") = 8 Then VCat(iloop) = 2
            End If
            VUnits(iloop) = valtbl7.Rows(iloop - 1).Item("Units")
            VShort(iloop) = valtbl7.Rows(iloop - 1).Item("Change_priority")
            If VShort(iloop) = 1 Then NVShort = NVShort + 1
            VMin(iloop) = valtbl7.Rows(iloop - 1).Item("Minimum")
            VMax(iloop) = valtbl7.Rows(iloop - 1).Item("Maximum")
            'JRS 8/16/01 GUI VAggregation(ILOOP) = valtbl7.Rows(iloop - 1).Item("Aggregation")
            VAggregation(iloop) = IIf(IsDBNull(valtbl7.Rows(iloop - 1).Item("Aggregation")), "", valtbl7.Rows(iloop - 1).Item("Aggregation"))
            'jrs 2022/06/15 compatibility with old version
            If valtbl7.Columns.Contains("Aggregation2") Then
                'jrs 2014/09/25
                VAggregation2(iloop) = IIf(IsDBNull(valtbl7.Rows(iloop - 1).Item("Aggregation2")), "", valtbl7.Rows(iloop - 1).Item("Aggregation2"))
            End If
            VHelpID(iloop) = valtbl7.Rows(iloop - 1).Item("HELPID")
            'JRS 8/16/01 GUI VCurrency(ILOOP) = valtbl7.Fields("Currency")
            'JRS 8/16/01 GUI If IsNull(VCurrency(ILOOP)) Then VCurrency(ILOOP) = False
            SaveHelpIDEqn = valtbl7.Rows(iloop - 1).Item("HelpIDEqn")
            If IsDBNull(SaveHelpIDEqn) Then SaveHelpIDEqn = 0
            VHelpIDEqn(iloop) = SaveHelpIDEqn
            VCurrency(iloop) = valtbl7.Rows(iloop - 1).Item("Currency")
            If IsDBNull(VCurrency(iloop)) Then VCurrency(iloop) = False
            VHistoricAnalog(iloop) = valtbl7.Rows(iloop - 1).Item("Historic Analog")
            'jrs 2008/06/12
            If Not IsDBNull(valtbl7.Rows(iloop - 1).Item("UsrDefCat")) Then
                VUsrDefCat(iloop) = IIf(valtbl7.Rows(iloop - 1).Item("UsrDefCat") > "", valtbl7.Rows(iloop - 1).Item("UsrDefCat"), VCatName$(iloop))
            Else
                VUsrDefCat(iloop) = VCatName$(iloop)
            End If
            'jrs 2022/06/23 compatibility with old version
            'jrs 2025/10/31
            'If valtbl7.Columns.Contains("Display Name") Then
            If bContainsDisplayName Then
                'jrs 2011/04/01
                VDisplayName(iloop) = IIf(IsDBNull(valtbl7.Rows(iloop - 1).Item("Display Name")), V$(iloop), valtbl7.Rows(iloop - 1).Item("Display Name"))
            End If
            If Trim(VDisplayName(iloop)) = "" Then VDisplayName(iloop) = V(iloop)
            'jrs 2022/06/23 compatibility with old version
            'jrs 2025/10/31
            'If valtbl7.Columns.Contains("Display Units") Then
            If bContainsDisplayUnits Then
                VDisplayUnitsName(iloop) = IIf(IsDBNull(valtbl7.Rows(iloop - 1).Item("Display Units")), VUnits(iloop), valtbl7.Rows(iloop - 1).Item("Display Units"))
            End If
            If Trim(VDisplayUnitsName(iloop)) = "" Then VDisplayUnitsName(iloop) = VUnits(iloop)
            'jrs 2025/10/31
            If bContainsNumDecimals Then
                VNumDecimals(iloop) = valtbl7.Rows(iloop - 1).Item("NumDecimals")
            End If

        Next iloop

        'jrs 2019/07/11
        If bHidePrivateVars Then
            'jrs 2023/11/17 added UPPER()
            'Load Variables to Hide
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/08/10 Improving to read sqlite files
            SetDataTablefromAccess(InFile, "Select VarName from PrivateVars ORDER BY UPPER(VarName)", "", valtbl7, "", True)
            'SetDataTablefromAccessJournal(InFile, "Select VarName from PrivateVars ORDER BY UPPER(VarName)", "", valtbl7)
            Dim iloop2 As Integer
                For iloop = 1 To valtbl7.Rows.Count
                    iloop2 = IfsVars.Item(valtbl7.Rows(iloop - 1).Item("VarName")).Index
                    VHidePrivateVars(iloop2) = True
                Next
            End If

            'jrs 2024/03/07
            Dim sVar As String, sDim2 As String, sDim3 As String, sHistAna As String
        If IFsHistAnalogs.Count = 0 Then
            'jrs 2006/06/05 added to handle historic analog options for 3 dims.
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/08/10 Improving to read sqlite files
            SetDataTablefromAccess(InFile, "SELECT * from IFSVARHISTANA", "", valtbl7, "", True)
            'SetDataTablefromAccessJournal(InFile, "SELECT * from IFSVARHISTANA", "", valtbl7)
            'Dim sVar As String, sDim2 As String, sDim3 As String, sHistAna As String
            iloop = 0
            'jrs 2025/10/31
            If Not IsNothing(valtbl7) Then
                Do While Not valtbl7.Rows.Count = iloop
                    sVar = IIf(IsDBNull(valtbl7.Rows(iloop).Item("VarName")), "", valtbl7.Rows(iloop).Item("VarName"))
                    sDim2 = IIf(IsDBNull(valtbl7.Rows(iloop).Item("Dim2")), "", valtbl7.Rows(iloop).Item("Dim2"))
                    sDim3 = IIf(IsDBNull(valtbl7.Rows(iloop).Item("Dim3")), "", valtbl7.Rows(iloop).Item("Dim3"))
                    sHistAna = IIf(IsDBNull(valtbl7.Rows(iloop).Item("Historic Analog")), "", valtbl7.Rows(iloop).Item("Historic Analog"))
                    IFsHistAnalogs.Add(sVar, sDim2, sDim3, sHistAna)
                    iloop = iloop + 1
                Loop
            End If
        End If

        'jrs 2024/03/07
        If IFsAggRules.Count = 0 Then
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/08/10 Improving to read sqlite files
            SetDataTablefromAccess(InFile, "SELECT * from AggRules", "", valtbl7, "", True)
            'SetDataTablefromAccessJournal(InFile, "SELECT * from AggRules", "", valtbl7)
            Dim sAggName As String, sForecastVar As String, sHisEmpVar As String
            iloop = 0
            'jrs 2025/10/31
            If Not IsNothing(valtbl7) Then
                Do While Not valtbl7.Rows.Count = iloop
                    sAggName = valtbl7.Rows(iloop).Item("AggName")
                    sForecastVar = IIf(IsDBNull(valtbl7.Rows(iloop).Item("ForecastVar")), "", valtbl7.Rows(iloop).Item("ForecastVar"))
                    sDim2 = IIf(IsDBNull(valtbl7.Rows(iloop).Item("Dim2")), "", valtbl7.Rows(iloop).Item("Dim2"))
                    sDim3 = IIf(IsDBNull(valtbl7.Rows(iloop).Item("Dim3")), "", valtbl7.Rows(iloop).Item("Dim3"))
                    sHisEmpVar = IIf(IsDBNull(valtbl7.Rows(iloop).Item("HisEmpVar")), "", valtbl7.Rows(iloop).Item("HisEmpVar"))
                    IFsAggRules.Add(sAggName, sForecastVar, sDim2, sDim3, sHisEmpVar)
                    iloop = iloop + 1
                Loop
            End If
        End If

        'jrs 2006/09/27 added to handle generic aggregation rules

        '    msg$ = "Have closed IfsVar"
        '    MsgBox msg$

        '   Develop Pointers to the First Position of Each Variable Name
        '   ------------------------------------------------------------
        pcnt = 1 'position counter
        VarPoint(1) = 1 'pointer to first variable is position one
        For iloop = 2 To NumbVars 'Look back 1 variable to see how much adds
            'to pointer
            If VDim1(iloop - 1) = 0 Then
                pcnt = pcnt + 1
                VarPoint(iloop) = pcnt
            ElseIf VDim1(iloop - 1) > 0 And VDim2(iloop - 1) = 0 Then
                temp = VDim1(iloop - 1)
                pcnt = pcnt + MAXCOM(temp) - 1
                VarPoint(iloop) = pcnt
            ElseIf VDim2(iloop - 1) > 0 And VDim3(iloop - 1) = 0 Then ' double dimension
                temp = VDim1(iloop - 1)
                TEMP1 = VDim2(iloop - 1)
                pcnt = pcnt + ((MAXCOM(temp) - 1) * (MAXCOM(TEMP1) - 1))
                VarPoint(iloop) = pcnt 'Must be less than approx 32500 if integer
            ElseIf VDim3(iloop - 1) > 0 Then 'triple dimension 'jrs 2006/05/18 Dim3
                temp = VDim1(iloop - 1)
                TEMP1 = VDim2(iloop - 1)
                Temp2 = VDim3(iloop - 1)
                pcnt = pcnt + ((MAXCOM(temp) - 1) * (MAXCOM(TEMP1) - 1) * (MAXCOM(Temp2) - 1))
                VarPoint(iloop) = pcnt  'Must be less than approx 32500 if integer
                'jrs 2006/05/18 182 * 182 * 182 = 6M, thus it could be a problem for integers
                'but I don't see a ceclaration for integer in the array or pcnt

            End If
            If V(iloop) = "MODYEAR" Then PosModYear = iloop
            If V(iloop) = "MODYEARI" Then PosModYearI = iloop
        Next iloop

        '   Calculate the file length
        '   same procedure as above for the last variable
        '   ---------------------------------------------
        If VDim1(NumbVars) = 0 Then 'global variable, no dimensions
            filen = pcnt + 1
        ElseIf VDim1(NumbVars) > 0 And VDim2(NumbVars) = 0 Then
            temp = VDim1(NumbVars)
            filen = pcnt + MAXCOM(temp) - 1
        ElseIf VDim2(NumbVars) > 0 And VDim3(NumbVars) = 0 Then 'jrs 2006/05/18 Dim3
            temp = VDim1(NumbVars)
            TEMP1 = VDim2(NumbVars)
            filen = pcnt + ((MAXCOM(temp) - 1) * (MAXCOM(TEMP1) - 1))
        Else
            temp = VDim1(NumbVars)
            TEMP1 = VDim2(NumbVars)
            Temp2 = VDim3(NumbVars)
            filen = pcnt + ((MAXCOM(temp) - 1) * (MAXCOM(TEMP1) - 1) * (MAXCOM(Temp2) - 1))
        End If
        FileClose()
        'JRS 01/25/04
        FILEBUF2.Initialize()
    End Sub
End Module

'End Namespace
