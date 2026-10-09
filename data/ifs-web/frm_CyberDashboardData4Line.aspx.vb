Partial Class frm_CyberDashboardData4Line
    Inherits Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("CDSceIdx") Is Nothing Then
            'Reload initialization values
            DashboardInitialization(Session("DisYear"), Session("DisplayLastYear"), Session("FirstYear"), Session("LastYear"),
                    Session("CostBenefitIdx"), Session("CDSceFileNames"), Session("PutFdrvRun2"), Session("vShortSce"),
                    Session("RunFiles"), Session("IFsGroupAlias"), Session("CDValues"), Session("CDValuesAG"),
                    Session("CDValuesLine"), Session("CDValuesLineDim1"), Session("CDValuesLineSce"), Session("CDSceIdx"))
        End If
        If Not (Request.QueryString("VarDesc") Is Nothing) Then
            Dim VarIdx As Integer, sVarDesc As String, sVar As String
            sVarDesc = Request.QueryString("VarDesc")
            sVar = LoadVarNameDsh(Session("PutFdrvRun"), sVarDesc)
            VarIdx = IfsVars.Item(sVar).Index
            Session("CDVarName") = sVar
            Session("CDVarDesc") = sVarDesc
            Dim SceIDs() As String
            If Not (Request.QueryString("SceIds") Is Nothing) Then
                SceIDs = Split(Request.QueryString("SceIds"), ",")
                Dim RegIDs() As String
                If Not (Request.QueryString("RegIds") Is Nothing) Then
                    RegIDs = Split(Request.QueryString("RegIds"), ",")
                    LoadCD4Lines(VarIdx, SceIDs, RegIDs)
                    PackJS(VarIdx)
                End If
            End If
        End If
    End Sub

    Private Sub LoadCD4Lines(ByRef VarIdx As Integer, ByRef SceIDs As Object, ByRef RegIDs As Object)
        Dim Dim2Idx As Integer = -1, Dim3Idx As Integer = -1
        Dim sDim1 As String
        'Need to handle
        'Multiple Regions and Multiple Scenarios
        Dim j As Integer, k As Integer, m As Integer, n As Integer, p As Integer, i As Integer
        Dim bGrp As Boolean = False
        Session("CDValuesLineCOUNTE") = 0
        For n = 0 To 4
            Session("CDSces")(n) = False
        Next
        For n = 0 To NR + 11
            Session("CDRegs")(n) = False
        Next
        For n = 0 To UBound(SceIDs)  'Number of Scenarios
            If IsNumeric(SceIDs(n)) Then
                j = CInt(SceIDs(n)) + 1
            Else
                If SceIDs(n) = "Base Case" Then
                    j = 1
                Else
                    For i = 1 To 4
                        If InStr(Session("CDSceFileNames")(i), Trim(SceIDs(n))) > 0 Then
                            j = i + 1
                            Exit For
                        End If
                    Next
                End If

            End If

            Session("CDSces")(j - 1) = True
            For p = 0 To UBound(RegIDs) 'for each Region/Group selected
                If IsNumeric(RegIDs(p)) Then
                    i = CInt(RegIDs(p)) + 1
                Else
                    i = IFsRegs.Item(RegIDs(p)).Index
                    If i > 0 Then
                        i = i + 12
                    Else
                        If RegIDs(p) = "World" Then
                            i = 2
                        Else
                            For m = 0 To 9
                                If RegIDs(p) = CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).Keys(m) Then
                                    i = m + 3
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If
                Session("CDRegs")(i - 1) = True
                If i > 12 Then
                    k = i - 12
                    If k < 0 Then
                        bGrp = True
                    Else
                        bGrp = False
                    End If
                    sDim1 = GRPS(1, k)
                Else
                    k = -1
                    bGrp = True
                    If i = 2 Then
                        sDim1 = "World"
                    Else
                        sDim1 = CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).Values(i - 3)
                    End If
                End If

                Session("CDValuesLineCOUNTE") = Session("CDValuesLineCOUNTE") + 1
                If i > 12 Or i = 2 Then
                    Session("CDValuesLineDim1")(Session("CDValuesLineCOUNTE")) = sDim1
                Else
                    Session("CDValuesLineDim1")(Session("CDValuesLineCOUNTE")) = CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).Keys(i - 3)
                End If
                If j = 1 Then
                    Session("CDValuesLineSce")(Session("CDValuesLineCOUNTE")) = "Base Case"
                Else
                    Session("CDValuesLineSce")(Session("CDValuesLineCOUNTE")) = Session("vShortSce")(j + 4)
                End If
                Dim iFile As Integer
                If j = 1 Then
                    iFile = 1
                Else
                    iFile = j + 4
                End If
                gLoadForDisplayNET(Session("CDVarName"), VarIdx, sDim1, k, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
                    Session("CDSceFileNames")(j - 1), iFile, True, True, bGrp, 1, Session("CDValuesLine"), Session("CDValuesLineCOUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                    Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                'gLoadForDisplay(Session("CDVarName"), VarIdx, sDim1, k, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
                'Session("CDSceFileNames")(j - 1), iFile, True, True, bGrp, 1, Session("CDValuesLine"), Session("CDValuesLineCOUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                'Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Next
        Next
    End Sub

    Private Sub PackJS(ByVal VarIdx As Integer)
        Dim sJscript2 As String
        sJscript2 = "    var dataAllSeriesLine = [];" & vbCr
        sJscript2 = sJscript2 & "    var LineCounte = " & Session("CDValuesLineCOUNTE") & ";" & vbCr
        Dim sReg As String, sSce As String
        For i = 1 To Session("CDValuesLineCOUNTE")
            sReg = Session("CDValuesLineDim1")(i)
            sSce = Session("CDValuesLineSce")(i)
            sJscript2 = sJscript2 & "dataAllSeriesLine[" & (i - 1).ToString & "] = [" & PlotColToHCSeries(sReg, sSce, i) & vbCr
            sJscript2 = sJscript2 & "];" & vbCr
        Next
        sJscript2 = sJscript2 & "var VarUnits = '" & getYAxisLabel(VarIdx) & "';"

        'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadXML", _
        'sJscript, False)
        Response.Write(sJscript2)
    End Sub

    Private Function LoadVarNameDsh(ByVal gPutFdrvRun As String, ByVal VarDefinition As String) As String
        Dim Rcnt As Short
        Dim Varname As String
        Dim valtbl As DataTable
        Dim db As String
        Dim idx As Integer
        Dim sql As String




        db = gPutFdrvRun & "IFSVAR.mdb"
        sql = "SELECT NAME FROM IFSVAR WHERE TYPE=1 AND DEFINITION LIKE  """ & VarDefinition & """"
        SetDataTablefromAccess(db, sql, "", valtbl)



        idx = 0
        Varname = Session("CDVarName")
        For Rcnt = 0 To valtbl.Rows.Count - 1
            Varname = valtbl.Rows(Rcnt).Item("NAME")

            'VarDefinition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")
            'VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")

            idx = idx + 1

        Next Rcnt

        LoadVarNameDsh = Varname
    End Function

    Private Function PlotColToHCSeries(ByVal sReg As String, ByVal sSce As String, ByVal i As Integer) As String

        Dim sJscript As String = ""
        Dim sName As String = ""
        sName = "{name: '" & sReg & ", " & sSce & "',"
        sJscript = sJscript & sName & vbCr

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "Lime"
                Case 2
                    sColor = "Blue"
                Case 3
                    sColor = "Red"
                Case 4 'blue violet
                    sColor = "RGB(138,43,226)"
                Case 5 'aqua 
                    sColor = "RGB(0,255,255)"
                Case 6 'deep pink
                    sColor = "RGB(255,20,147)"
                Case 7 'medium spring green
                    sColor = "RGB(0,250,154)"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2


        For j = 1 To Session("DisYear")
            If Not IsDBNull(Session("CDValuesLine")(j, i)) Then
                If IsNumeric(Session("CDValuesLine")(j, i)) Then
                    sJscript = sJscript & CStr(Math.Round(Session("CDValuesLine")(j, i), iNumDec))

                    If j < Session("DisYear") Then
                        sJscript = sJscript & ", "
                    End If
                End If
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeries = sJscript
    End Function

    Private Function getYAxisLabel(ByRef idx As Integer) As String
        getYAxisLabel = ""
        If idx > 0 Then
            If Session("VDisplayUnitsName")(idx) > "" Then
                getYAxisLabel = getDisplayUnitsWCurrency(idx, Session("CurrencyName"), Session("VDisplayUnitsName"))
            End If
        End If
    End Function

End Class
