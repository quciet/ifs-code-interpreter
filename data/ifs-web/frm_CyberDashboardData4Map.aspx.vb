Partial Class frm_CyberDashboardData4Map
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
            Dim sSceName As String
            If Not (Request.QueryString("SceName") Is Nothing) Then
                sSceName = Replace(Request.QueryString("SceName"), """", "")
                sSceName = Trim(sSceName)
                Dim ss As Integer
                If sSceName = "Base Case" Then
                    Session("CDSceIdx") = 1
                    sSceName = "ifsBase"
                Else
                    For ss = 1 To 4
                        If Session("CDSceFileNames")(ss) = Session("PutFdrvRun2") + "\" & sSceName & ".run" Then
                            Session("CDSceIdx") = 5 + ss
                            Exit For
                        End If
                    Next
                End If

                LoadCD4Map(VarIdx, sSceName)
                PackJS()
            End If
        End If
    End Sub

    Private Sub LoadCD4Map(ByVal VarIdx As Integer, ByVal sCDSceName As String)

        Session("MaxCDValues") = -5000
        Session("MinCDValues") = 5000
        Dim Dim2Idx As Integer = -1, Dim3Idx As Integer = -1
        Dim i As Integer
        For i = 1 To NR
            'jam 2023/10/28
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(Session("CDVarName"), VarIdx, GRPS(1, i), i, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
                    Session("PutFdrvRun") & sCDSceName & ".run.db", Session("CDSceIdx"), i = 1, i = NR, False, 1, Session("CDValues"), i, 1, Session("FirstYear"), Session("DisplayLastYear"), 0, False,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                    Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplayNET(Session("CDVarName"), VarIdx, GRPS(1, i), i, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
                   Session("PutFdrvRun") & sCDSceName & ".run", Session("CDSceIdx"), i = 1, i = NR, False, 1, Session("CDValues"), i, 1, Session("FirstYear"), Session("DisplayLastYear"), 0, False,
                   Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                   Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If




            'gLoadForDisplay(Session("CDVarName"), VarIdx, GRPS(1, i), i, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
            'Session("PutFdrvRun") & sCDSceName & ".run", Session("CDSceIdx"), i = 1, i = NR, False, 1, Session("CDValues"), i, 1, Session("FirstYear"), Session("DisplayLastYear"), 0, False,
            'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
            'Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))

            'jrs 2015/05/10 check only first and last values
            'Max
            If Session("CDValues")(1, i) > Session("MaxCDValues") Then
                Session("MaxCDValues") = Session("CDValues")(1, i)
            End If
            If Session("CDValues")(Session("FinYear")(i), i) > Session("MaxCDValues") Then
                Session("MaxCDValues") = Session("CDValues")(Session("FinYear")(i), i)
            End If
            'Min
            If Session("CDValues")(1, i) < Session("MinCDValues") Then
                Session("MinCDValues") = Session("CDValues")(1, i)
            End If
            If Session("CDValues")(Session("FinYear")(i), i) < Session("MinCDValues") Then
                Session("MinCDValues") = Session("CDValues")(Session("FinYear")(i), i)
            End If
        Next
        Session("VarIdx") = VarIdx
    End Sub

    Private Sub PackJS()
        Dim sJscript2 As String
        sJscript2 = "    var dataAllYrs3 = [];" & vbCr
        For i = 1 To Session("DisplayLastYear") - Session("FirstYear") + 1
            sJscript2 = sJscript2 & "    var data" & i.ToString & " = [" & vbCr
            sJscript2 = sJscript2 & PrepareDataForMap(i)
            sJscript2 = sJscript2 & "];" & vbCr
            sJscript2 = sJscript2 & "    dataAllYrs3[" & (i - 1).ToString & "] = data" & i.ToString & ";" & vbCr
        Next

        Dim CDValRange As Single = (Session("MaxCDValues") - Session("MinCDValues")) / 6
        sJscript2 = sJscript2 & "    var CDValLimits = [];" & vbCr
        sJscript2 = sJscript2 & "    CDValLimits[3] = " & Math.Round((Session("MinCDValues") + Session("MaxCDValues")) / 2, Session("iRound")).ToString & ";" & vbCr
        sJscript2 = sJscript2 & "    CDValLimits[2] = " & Math.Round((Session("MinCDValues") + Session("MaxCDValues")) / 2 - CDValRange, Session("iRound")).ToString & ";" & vbCr
        sJscript2 = sJscript2 & "    CDValLimits[1] = " & Math.Round((Session("MinCDValues") + Session("MaxCDValues")) / 2 - 2 * CDValRange, Session("iRound")).ToString & ";" & vbCr
        sJscript2 = sJscript2 & "    CDValLimits[4] = " & Math.Round((Session("MinCDValues") + Session("MaxCDValues")) / 2 + CDValRange, Session("iRound")).ToString & ";" & vbCr
        sJscript2 = sJscript2 & "    CDValLimits[5] = " & Math.Round((Session("MinCDValues") + Session("MaxCDValues")) / 2 + 2 * CDValRange, Session("iRound")).ToString & ";" & vbCr


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

    Private Function PrepareDataForMap(ByVal yrIdx As Integer) As String
        Dim jScript As String = "", i As Integer
        Dim iRound As Integer = 4
        Select Case ABS_(Session("MaxCDValues"))
            Case Is > 1000
                iRound = 0
            Case Is > 100
                iRound = 1
            Case Is > 10
                iRound = 2
            Case Is > 1
                iRound = 3
            Case Else
                iRound = 4
        End Select
        Session("iRound") = iRound
        For i = 1 To NR
            If i > 1 Then
                jScript = jScript & "," & vbCr
            End If
            jScript = jScript & "{" & vbCr
            'jrs 2015/05/13 jScript = jScript & """code"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
            jScript = jScript & """key"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
            jScript = jScript & """value"": " & Math.Round(Session("CDValues")(yrIdx, i), iRound) & vbCr
            jScript = jScript & "}"
        Next
        jScript = jScript & vbCr
        PrepareDataForMap = jScript
    End Function

End Class
