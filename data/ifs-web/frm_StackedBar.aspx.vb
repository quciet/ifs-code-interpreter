'jrs 2011/07/15 Implemented by José Roberto Solórzano
Option Strict Off
Option Explicit On
Imports System.Drawing
Imports System.Reflection.Emit
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_StackedBar
    Inherits Page

    'mti Validation changes 19 August, 2002
    Dim sInterval As Short

    'Dim CommonPlot As String
    'mti Validation changes
    Dim pCOUNTE As Short
    Dim VH(100) As Object
    Dim VF(100) As Object
    'mti Validation changes-replace all pCOUNTE with pCOUNTE for this module
    Dim reg As New RegressionObject
    Dim RSQR As Double
    'Dim prevHorizonYear As Integer 'JAM 2016-03-06

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim sValidationVar As String
        Dim ii As Short
        Dim j As Integer
        Dim i As Short


        'jrs 2023/10/19 *** start ***
        If (Session("OriginalFirstYear") = FirstYearForecast And Session("FirstYear") = FirstYearHistory) Or
            (Session("OriginalFirstYear") = FirstYearHistory And Session("FirstYear") = FirstYearForecast And
            Session("RowAdjustment") <> FirstYearForecast - FirstYearHistory And
            Session("finyear")(1) > Session("LastYearForecast") - FirstYearForecast + 1) Then
            Session("OriginalFirstYear") = Nothing
            Session("UserFirstYear") = Nothing
        End If


        If Session("FirstYear") = Session("OriginalFirstYear") Or Session("OriginalFirstYear") = Nothing Then
            Session("RowAdjustment") = 0
        End If
        'jrs 2023/10/19 *** end ***


        If Not Page.IsPostBack Then
            'jrs 2015/08/28 removed buseAllHis because now it could be forecast by others comparison
            'If buseAllHis And FirstYearPE = FirstYearHistory Then
            'removed the historic condition, becasue forecast comparison could be comparing vs forecast only
            'jrs 2014/03/27
            'If Session("FirstYear") = FirstYearHistory Then

            'JAM 2017-08-01
            If Session("PatternSel") Is Nothing Then
                Session("PatternSel") = " "
            End If



            'JAM 2016-02-12 Defining AllYears Variables
            If Session("mnuUseAllYrs") = Nothing Then
                Session("mnuUseAllYrs") = False
            End If

            'JAM 2016-02-12 Defining AllYears Variables
            If Session("mnuUseAllYrsTime") = Nothing Then
                Session("mnuUseAllYrsTime") = False
            End If

            For i = 1 To Session("COUNTE")
                If Session("StaYearHis")(i) > 0 And Session("StaYearHis")(i) < Session("FirstYear") Then
                    'Check for DATA
                    For j = 1 To Session("FirstYear") - Session("StaYearHis")(i)
                        If IsNumeric(Session("PLOT")(j, i)) Then
                            'bLoadLongitudinalBFYH = True
                            'iFirstYearBFYH = Session("StaYearHis")(i) + j - 1
                            Session("FirstYear") = Session("StaYearHis")(i) + j - 1
                            Exit For
                        End If
                    Next j
                End If
            Next i
            'End If

            'jrs 2015/08/28 removed buseAllHis because now it could be forecast by others comparison
            'jrs 2015/02/25
            'If buseAllHis And LastYearPE = LastYearHistory Then
            'removed the historic condition, becasue forecast comparison could be comparing vs forecast only
            'If LastYearPE = LastYearHistory Then
            For i = 1 To Session("COUNTE")
                If Session("FinYearHis")(i) > 0 And Session("FinYearHis")(i) > Session("LastYear") Then
                    'Check for DATA
                    For j = Session("FinYearHis")(i) - Session("LastYear") To 1 Step -1
                        If Not (IsNumeric(Session("PLOT")(j + Session("LastYear") - Session("FirstYear") + 1, i))) Then
                            Session("LastYear") = Session("LastYear") + j
                            Exit For
                        End If
                    Next j
                End If
            Next i
            'End If

            'jrs 2011/06/07
            Dim bReload As Boolean = False
            Dim sListNames As String = ""
            If Not (Request.QueryString("ListNames") Is Nothing) Then
                sListNames = Request.QueryString("ListNames")
                If Not (Session("ListLoaded") Is Nothing) Then
                    If Session("ListLoaded") <> sListNames Then
                        bReload = True
                    End If
                Else
                    bReload = True
                End If
                'jrs 2014/01/22
                'ListNames should have doble quotation marks ""
                If InStr(sListNames, """") = 0 Then
                    sListNames = """" & sListNames & """"
                End If
                Session("ListLoaded") = sListNames
            End If
            'jrs 2013/10/08
            If Not bReload Then
                If Not (Request.QueryString("Dim1") Is Nothing) Then
                    If Not (Session("Dim1sLoaded") Is Nothing) Then
                        If Session("Dim1sLoaded") <> Request.QueryString("Dim1") Then
                            bReload = True
                        End If
                    Else
                        bReload = True
                    End If
                End If
            End If

            'jrs 2013/01/18
            If Not IsNothing(Request.QueryString("DisplayLastYear")) Then
                'jrs 2014/01/02
                If Session("DisplayLastYear") <> Request.QueryString("DisplayLastYear") Then
                    bReload = True
                End If
                Session("DisplayLastYear") = Request.QueryString("DisplayLastYear")
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = 2030
                End If
            Else
                'jrs 2011/12/05
                If Session("DisplayLastYear") > Session("LastYearForecast") Then
                    Session.Remove("DisplayLastYear")
                    'jrs 2014/01/02
                    bReload = True
                End If
            End If

            'jrs 2011/04/05
            If Session("COUNTE") = 0 Or bReload Then
                'Read querystring
                Dim sHistFor As String, GrpOp As Integer
                sHistFor = Request.QueryString("HistFor")
                GrpOp = CInt(Request.QueryString("GrpOp"))
                Dim sDim1 As String, sDim2 As String, sDim3 As String, sFiles As String
                If Not Request.QueryString("Dim1") Is Nothing Then
                    sDim1 = Request.QueryString("Dim1")
                    'jrs 2013/10/08
                    Session("Dim1sLoaded") = sDim1
                Else
                    Session("Dim1sLoaded") = Nothing
                End If
                If Not Request.QueryString("Dim2") Is Nothing Then
                    sDim2 = Request.QueryString("Dim2")
                End If
                If Not Request.QueryString("Dim3") Is Nothing Then
                    sDim3 = Request.QueryString("Dim3")
                End If
                sFiles = Request.QueryString("File")
                If sFiles Is Nothing Then
                    sFiles = "1"
                End If

                Dim rs As DataTable
                rs = getVarLists(sListNames, sHistFor, Session("PutFdrvRun"))
                Session("PackagedDispRs") = rs
                Dim p As Integer
                'jrs 2012/04/12 
                'p = InStr(2, sListNames, "'")
                'If p > 1 Then
                '    Session("TableTitle") = Mid(sListNames, 2, p - 2)
                'Else
                '    Session("TableTitle") = sListNames
                'End If
                'jrs 2014/08/25
                If sListNames > "" Then
                    p = InStr(sListNames, """,""")
                    If p > 0 Then
                        'Multiple lists
                        Session("TableTitle") = sListNames
                    Else
                        'Single list
                        Session("TableTitle") = Mid(sListNames, 2, Len(sListNames) - 2)
                    End If
                End If

                Session("GType") = "LINE"
                'jrs 2013/08/09 declaration of ifsPars used to be here
                Session("GraphDisplayParent") = "frm_PackagedDisplaysFlex.aspx"
                'Load list boxes
                Dim lstDim As New ListBox, lstDim2 As New ListBox, lstDim3 As New ListBox
                Dim lstFiles As New ListBox
                lstDim.ID = "lstDim"
                lstDim2.ID = "lstDim2"
                lstDim3.ID = "lstDim3"
                LoadAllDims(lstDim, Session("AllDimsIdx"))
                LoadAllDims(lstDim2, Session("AllDimsIdx"))
                LoadAllDims(lstDim3, Session("AllDimsIdx"))

                If sDim1 = "" Then
                    sDim1 = Trim(CStr(lstDim.Items.Count - 1))
                End If
                If sDim2 = "" Then
                    sDim2 = Trim(CStr(lstDim2.Items.Count - 1))
                End If
                If sDim3 = "" Then
                    sDim3 = Trim(CStr(lstDim3.Items.Count - 1))
                End If

                ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
                ReDim Preserve Session("DimIdxs2")(lstDim2.Items.Count - 1)
                ReDim Preserve Session("DimIdxs3")(lstDim3.Items.Count - 1)

                LoadFiles(Session("VarSelCheck"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("ActiveBase"), Session("VarSelCheckHis"), Session("PutFdrvRun2"), Session("WorkingFile"),
                Session("FilesSelected"), Session("IFsRunFiles"), Session("RunFiles"), Session("ShortSce"), lstFiles)
                If sFiles > "" Then
                    lstFiles.Items(0).Selected = False
                End If

                ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)

                'Set Region options
                Select Case GrpOp
                    Case 0 'Regions
                        Session("bUseReg") = True
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = False
                    Case 1 'Groups
                        Session("bUseReg") = False
                        Session("bUseGroup") = True
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = False
                    Case 2 'Dec Groups
                        Session("bUseReg") = False
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = True
                        Session("bUseGList") = False
                    Case 3 'G-Lists
                        Session("bUseReg") = False
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = True
                End Select

                'Set List Boxes
                URLToDimLst(sDim1, lstDim)
                URLToDimLst(sDim2, lstDim2)
                URLToDimLst(sDim3, lstDim3)
                URLToFileLst(sFiles, lstFiles, Session("txtFiles"), Session("IFsRunFiles"), Session("PDFilesIdxs"), Session("FilesSelected"),
                Session("RunFiles"), Session("PutFdrvRun2"), Session("ShortSce"), Session("ScenarioFileDir"))

                'jrs 2013/08/09 new location for declaration of IfsPars, to pick up any changes in RunFiles (loading Preloaded scenarios)
                Dim IfsPars As New myParameters
                IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
                Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
                Session("PutFdrvRun"), Session("CurrencySym"))

                'Load Package
                gLoadPackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, Session("TableTitle"),
                Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
                Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("bUseCtryAndGroupDec"),
                Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
                Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
                Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
                Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
                Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
                Session("PopMales"), Session("PopFemales"), sListNames, Session("sUsrDefCats"), Session("txtFiles"))

                'jrs 2012/07/03
                Session.Remove("bVARN0Loaded")
            End If

            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            '------------------------------------------------------------
            'JRS 11/03/03
            If Session("DisplayLastYear") = 0 Then
                Session("DisplayLastYear") = Session("LastYear")
            End If

            'jrs 2015/09/10 moving back to DisplayGraph
            ''jrs 2011/08/26
            'If Session("GType") <> "BAR" Then Session("Interval") = 1
            'If Session("GType") = "BAR" And Session("DisYear") > 20 And Session("Interval") < 5 Then
            '    Session("Interval") = 5
            'End If
            Session("CommonPlot") = "False"


            'mti Validation changes 19 August, 2002
            sInterval = Session("Interval")
            'mti Validation changes 19 August, 2002

            'From ChartHist
            '--------------------------------

            '   Set form and graph dimensions
            '   -----------------------------

            'mti Validation changes
            If InStr(1, Session("GType"), "Validation") > 0 Then
                pCOUNTE = 0
                For j = 1 To Session("COUNTE") - 1 'the last one need to be forecast
                    sValidationVar = Trim(Session("VARN")(4, j))
                    If Mid(sValidationVar, 1, 2) = "VH" Then 'asc("V") = 86
                        sValidationVar = "VF" & Mid(sValidationVar, 3)
                        For ii = j + 1 To Session("COUNTE")
                            If Trim(Session("VARN")(4, ii)) = sValidationVar Then
                                pCOUNTE = pCOUNTE + 1
                                VH(pCOUNTE) = j
                                VF(pCOUNTE) = ii
                                Exit For
                            End If
                        Next ii
                    End If
                Next j
                'pCOUNTE = pCOUNTE * 2
            Else
                If Not Session("bBubble") Or Session("GType") <> "SCAT" Then
                    pCOUNTE = Session("COUNTE")
                Else
                    pCOUNTE = Session("COUNTE") / 3 * 2
                End If
            End If
            'mti Validation changes


            'jrs 2008/01/18
            'save original name to display
            If Session("bVARN0Loaded") Is Nothing Then 'jrs 2012/07/03
                ReDim Session("oVARN0")(Session("COUNTE"))
                For i = 1 To Session("COUNTE")
                    Session("oVARN0")(i) = Session("VARN")(0, i)
                Next i

                'jrs 2010/02/04
                ReDim Session("oGR1N")(Session("COUNTE"))
                For i = 1 To Session("COUNTE")
                    Session("oGR1N")(i) = Session("GR1N")(i)
                Next i

                Session("bVARN0Loaded") = True
            End If

            'jrs 2011/04/01
            Dim pos As Integer
            pos = InStr(Session("VARN")(1, 1), "[")
            If pos > 0 Then
                Dim sVar As String
                sVar = Left(Session("VARN")(1, 1), pos - 1)
                If IfsVars.Item(sVar).Index > 0 Then
                    Session("MMVarName") = sVar
                    Session("MMDim2") = Session("GR2N")(1)
                    Session("MMDim3") = Session("GR3N")(1)
                End If
            End If
            'jrs 2011/07/04
            If Session("bAlias") = Nothing Then
                Session("bAlias") = False
            ElseIf Session("bAlias") = True Then
                Session("bAlias") = False
                mnuAlias_Click()
            End If
            Session("PE_CommonMenu") = "Scale" 'jrs 2014/03/24 "Display"
            Session("PE_CommonPlot") = "False"
            'jrs 2011/09/05
            If Session("GType") = "SCAT" Then
                'jrs 2015/09/16 ColorDistribution()

                If Session("BDAction") = Nothing Then
                    'jrs 2015/09/16 moved from outside the if
                    ColorDistribution()
                    'jrs 2012/03/12
                    'Variables to use when decomposing
                    getVariablesforDec(1, Session("sVarY"), Session("fileIdxY"), Session("bCompY"))
                    getVariablesforDec(1 + pCOUNTE / 2, Session("sVarX"), Session("fileIdxX"), Session("bCompX"))
                    If Session("bBubble") Then
                        getVariablesforDec(1 + pCOUNTE, Session("sVarZ"), Session("fileIdxZ"), Session("bCompZ"))
                        'jrs 2012/03/19
                        Session("sDim2Z") = Session("GR2N")(pCOUNTE + 1)
                        Session("sDim3Z") = Session("GR3N")(pCOUNTE + 1)

                        'jrs 2010/10/01
                        ReDim Session("BDOTHR")(Session("FinYear")(1), pCOUNTE / 2)
                        For j = 1 To pCOUNTE / 2
                            For i = 1 To Session("FinYear")(1)
                                If IsNumeric(Session("PLOT")(i, j + pCOUNTE)) Then
                                    Session("BDOTHR")(i, j) = Session("PLOT")(i, j + pCOUNTE)
                                Else
                                    Session("BDOTHR")(i, j) = -9999
                                End If
                            Next i
                        Next j
                    Else
                        Session("fileIdxZ") = Session("fileIdxY")
                        'jrs 2013/09/10
                        Session("sVarZ") = ""
                    End If
                    'jrs 2010/09/29
                    'FirstYearNotNull = FindFirstYearNotNull
                    'jrs 2010/11/01
                    '        If PYear > FirstYearNotNull Then
                    '            PYear = FirstYearNotNull
                    '        End If
                    '        'jrs 2010/10/27 FirstYearToPrintLblWTrace = 1
                    '        ReDim FirstYearToPrintLblWTrace(pCOUNTE / 2)
                    '        For i = 1 To pCOUNTE / 2
                    '            FirstYearToPrintLblWTrace(i) = 1
                    '        Next i

                    'Storing Auxiliaries for Bubble Diameter
                    If Session("sVarZ") <> "POP" Then
                        ReDim Session("BDPOP")(Session("FinYear")(1), pCOUNTE / 2)
                        LoadBD("POP", Session("BDPOP"))
                    End If
                    If Session("sVarZ") <> "GDP" Then
                        ReDim Session("BDGDP")(Session("FinYear")(1), pCOUNTE / 2)
                        LoadBD("GDP", Session("BDGDP"))
                    End If
                    Session("NoneOrder") = VB6.Support.CopyArray(Session("GR1N"))
                    '        'jrs 2010/10/13
                    '        If bBubble Then
                    '            SetUpManualScaleToMinMaxZ()
                    '        End If

                End If
                ''JAM 2017-08-12 Commented and moved outside if conditional
                'jrs 2010/09/29
                ''Dim FirstYearNotNull As Integer
                ''FirstYearNotNull = FindFirstYearNotNull()
                ' ''jrs 2010/11/01
                ''If Session("PYear") > FirstYearNotNull Then
                ''    Session("PYear") = FirstYearNotNull
                ''End If
                'jrs 2012/06/01
                If Session("mnuUseAllYrsTime") Is Nothing Then
                    Session("mnuUseAllYrsTime") = False
                    Session("mnuUseAllYrs") = False
                End If
            End If

            '''''''''''''''JAM 2017-08-12
            'jrs 2010/09/29
            Dim FirstYearNotNull As Integer
            FirstYearNotNull = FindFirstYearNotNull()
            'jrs 2010/11/01
            If Session("PYear") > FirstYearNotNull Then
                Session("PYear") = FirstYearNotNull
            End If
            ' ''jrs 2012/06/01
            ''If Session("mnuUseAllYrsTime") Is Nothing Then
            ''    Session("mnuUseAllYrsTime") = False
            ''    Session("mnuUseAllYrs") = False
            ''End If
            '''''''''''''''''''''''''''''''''''''''''''''


            'jrs 2012/07/03
            If Not IsNothing(Request.QueryString("Alias")) Then
                Dim sAlias As String
                sAlias = Request.QueryString("Alias")
                If (Not Session("bAlias") And sAlias = "1") Or
                (Session("bAlias") And sAlias = "0") Then
                    mnuAlias_Click()
                End If
            End If

            If Not IsNothing(Request.QueryString("Leg")) Then
                Dim sLeg As String
                sLeg = Request.QueryString("Leg")
                Select Case sLeg
                    Case "1"
                        mnuFormLeg1stDim_Click()
                    Case "2"
                        mnuFormLeg2ndDim_Click()
                    Case "3"
                        mnuFormLeg3rdDim_Click()
                    Case "4"
                        mnuFormLegDims_Click()
                    Case "0"
                        mnuFormLegNoDim_Click()
                    Case "5"
                        mnuFormLegSce_Click()
                    Case "9"
                        mnuFormLegReset_Click()
                End Select
            End If



            'JAM 2017-07-31 
            ReDim Session("CountryPattern")(pCOUNTE, 3)
            ReDim Session("VarNumber")(pCOUNTE)
            ReDim Session("VarGroup")(pCOUNTE)

            'JAM 2017-08-12
            If Session("PatternSel") Is Nothing Then
                Session("PatternSel") = " "
            End If


        Else 'jrs 2011/08/25
            'mti Validation changes
            If InStr(1, Session("GType"), "Validation") > 0 Then
                pCOUNTE = 0
                For j = 1 To Session("COUNTE") - 1 'the last one need to be forecast
                    sValidationVar = Trim(Session("VARN")(4, j))
                    If Mid(sValidationVar, 1, 2) = "VH" Then 'asc("V") = 86
                        sValidationVar = "VF" & Mid(sValidationVar, 3)
                        For ii = j + 1 To Session("COUNTE")
                            If Trim(Session("VARN")(4, ii)) = sValidationVar Then
                                pCOUNTE = pCOUNTE + 1
                                VH(pCOUNTE) = j
                                VF(pCOUNTE) = ii
                                Exit For
                            End If
                        Next ii
                    End If
                Next j
                'pCOUNTE = pCOUNTE * 2
            Else
                If Not Session("bBubble") Or Session("GType") <> "SCAT" Then
                    pCOUNTE = Session("COUNTE")
                Else
                    pCOUNTE = Session("COUNTE") / 3 * 2
                End If

            End If
            'mti Validation changes

            'JAM 2017-07-31
            If Session("PYear") Is Nothing Then
                Dim FirstYearNotNull As Integer
                FirstYearNotNull = FindFirstYearNotNull()
                Session("PYear") = FirstYearNotNull

            End If



        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadMenu()

        'JAM 2016-01-11
        If Not Session("fromTableDisp") Then
            If Session("bTrillionsOk") Is Nothing Then
                Session("bTrillionsOk") = False
                Session("prevHorizonYear") = 1
                ReDim Session("PLOTTrillions")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2)) 'JAM 2016-05-16
            End If
        End If




        If Not (Session("BDAction") = Nothing) And Not Page.IsPostBack And Session("GType") = "SCAT" Then
            If Session("sVarZ") > "" Then
                pCOUNTE = Session("COUNTE") / 3 * 2
            Else
                pCOUNTE = Session("COUNTE")
            End If

            Select Case Session("BDAction")
                Case "POP"
                    mnuBDPOP_Click()
                Case "GDP"
                    mnuBDGDP_Click()
                Case "Other"
                    mnuBDOther_Click()
                Case "None"
                    mnuBDNone_Click()
            End Select
            Session.Remove("BDAction")
        End If

        'jrs 2012/05/30
        If Not Page.IsPostBack Then
            If Session("RegressType") <> "" Then
                If Session("RegressType") = "LIN" And Not Session("ChangeSatPoint") Then
                    RegressionPLOT(1, "NONE")
                Else
                    If Session("RegressType") = "LOG" Then
                        RegressionPLOT(1, "LOG")
                    Else
                        If Session("RegressType") = "EXP" Then
                            RegressionPLOT(1, "EXP")
                        Else
                            If Session("RegressType") = "POW" And Session("IsChangedNum") Then
                                RegressionPLOT(1, "POW")
                            Else
                                If Session("RegressType") = "POLYNOMIAL(Second Degree)" Then
                                    RegressionPLOT(2, "NONE")
                                Else
                                    If Session("RegressType") = "POLYNOMIAL(Third Degree)" Then
                                        RegressionPLOT(3, "NONE")
                                    Else
                                        If Session("RegressType") = "LIN" And Session("ChangeSatPoint") Then
                                            TransSatDep()
                                            RegressionPLOT(1, "NONE")
                                            Call RevokeSatDep()
                                            Session("ChangeSatPoint") = False
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        DisplayGraph()
        PackJS()
    End Sub

    Private Sub DisplayGraph()
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim ii As Short
        '   Set number of points, interval
        '   ------------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To pCOUNTE
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("Interval") > MaxYear Then
            Session("Interval") = MaxYear - 1
        End If

        If Session("DisplayTime") = 0 Then
            Session("DisYear") = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                Session("DisYear") = MaxYear
            End If
        End If




        'JAM 2016-03-06
        If Not Session("fromTableDisp") Then

            Call Convert_Billions_To_Trillions()
        Else



            For cc = 1 To Session("COUNTE")
                For rr = 1 To Session("DisYear")
                    Session("PLOTTrillions")(rr, cc) = Session("PLOT")(rr, cc) 'JAM 2016-05-16

                Next rr
            Next cc
        End If




        If Session("GType") <> "SCAT" And Session("GType") <> "SCAT1" Then
            If Session("DisYear") * pCOUNTE > 25000 Then
                Session("DisYear") = 25000 / pCOUNTE
            End If
        Else
            If Session("DisYear") * (pCOUNTE / 2) > 25000 Then
                Session("DisYear") = 25000 / (pCOUNTE / 2)
            End If
        End If

        'jrs 2010/09/15 added <> SCAT
        'jrs 2011/08/26
        If Session("GType") <> "BAR" And Session("GType") <> "SCAT" Then Session("Interval") = 1
        If Session("GType") = "BAR" And Session("DisYear") > 20 And Session("Interval") < 5 Then
            Session("Interval") = 5
        End If

    End Sub

    Private Sub LoadMenu()
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        ''jrs 2012/01/23
        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.IconImage = "./images/pcl.jpg"
        'cswmGroup1.IconHeight = "30"
        'cswmGroup1.IconWidth = "30"
        'cswmGroup1.OnMenuBarClick = "location.href='http://www.ifs.du.edu';"
        'cswmGroup1.Caption = "   "
        'cswmGroup1.IconToolTip = "IFs Home"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_4a6c"

        If Request.QueryString("ListNames") Is Nothing Then
            'JAM 2013-04-09 Adding showhg(); for hourglass cursor
            cswmGroup1.OnMenuBarClick = "showhg();location.href='" & Session("GraphDisplayParent") & "';"

        Else
            Dim sLst As String
            sLst = Request.QueryString("ListNames")
            sLst = Replace(sLst, """,""", ",,")
            sLst = Mid(sLst, 2, Len(sLst) - 2)
            'jrs 2011/11/03 
            'Mark found that the list Foreign Reserves as Percent of GDP didn't work
            'there were 2 weird characters after the word Reserves, remove them
            sLst = Replace(sLst, Chr(127), " ")
            'JAM 2013-04-09 Adding showhg(); for hourglass cursor
            cswmGroup1.OnMenuBarClick = "showhg();location.href='" & Session("GraphDisplayParent") & "?ListNames=" & sLst & "';"
        End If

        'jrs 2026/08/07
        If StandAloneMode Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Main Menu"
            cswmGroup1.ID = "mnuMainMenu"
            cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        End If



        If Session("GType") = "PIE" Or Session("GType") = "COHORT" Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Time"
            cswmGroup1.ID = "Group_Time"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Advance"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Regress"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Go To Year"
        ElseIf Session("GType") = "SCAT" Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Time"
            cswmGroup1.ID = "Group_Time"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Advance"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Regress"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Go To Year"

            'jrs 2012/03/12
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Grouping Options"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Use a G-List"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Use a Group"

            'jrs 2012/07/23
            If (Session("IsaGroup") = Nothing Or Session("IsaGroup") = False) And
            (Session("IsaGlist") = Nothing Or Session("IsaGlist") = False) Then
                'jrs 2012/05/28
                cswmGroup1 = cswmWebMenu.Groups.Add()
                cswmGroup1.Caption = "Analysis"
                cswmGroup1.ID = "mnuAnalysis"

                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Statistics"



                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Relationships"
                cswmItem.ID = "mnuAnalysisRelationships"

                Dim cswmGroup2 As Coalesys.WebMenu.Group
                cswmGroup2 = cswmItem.AddGroup()

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Linear"
                If Session("mnuLogistic") Is Nothing Then
                    Session("mnuLogistic") = False
                End If
                If Session("RegressType") = "LIN" And Session("mnuLogistic") = False Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Logarithmic"
                If Session("RegressType") = "LOG" Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Exponential"
                If Session("RegressType") = "EXP" Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Power"
                If Session("RegressType") = "POW" Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Polynomial Second Degree"
                cswmItem.ID = "mnuPolSec"

                If Session("RegressType") = "POLYNOMIAL(Second Degree)" Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Polynomial Third Degree"
                cswmItem.ID = "mnuPolThird"

                If Session("RegressType") = "POLYNOMIAL(Third Degree)" Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If

                'JAM 2011-01-22 Logistic option
                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Logistic"

                If Session("mnuLogistic") Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Turn formulation display off"
                cswmItem.IconImage = "blank.gif"

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "No Regression"
                cswmItem.IconImage = "blank.gif"

            End If

            'JAM 2016-02-12 Adding AllYears Options
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Use all Years (in Statistics)"


            'JAM 2016-02-12
            If Session("mnuUseAllYrs") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If


            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Use all Years and Time (in Statistics)"

            'JAM 2016-02-12 
            If Session("mnuUseAllYrsTime") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If



            'jrs 2012/03/15
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Bubble Diameter"
            cswmGroup1.ID = "mnuBD"

            If Session("sVarZ") > "" Then
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = Session("sVarZ")
                cswmItem.ID = "mnuBDOther"
                If Session("BDCheck") = Nothing Then
                    Session("BDCheck") = "Other"
                End If
                If Session("BDCheck") = "Other" Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If
            ElseIf Session("BDCheck") Is Nothing Then
                Session("BDCheck") = "None"
            End If

            If Session("sVarZ") <> "POP" Then
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "POP"
                cswmItem.ID = "mnuBDPOP"
                If Session("BDCheck") = "POP" Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If
            End If

            If Session("sVarZ") <> "GDP" Then
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "GDP"
                cswmItem.ID = "mnuBDGDP"
                If Session("BDCheck") = "GDP" Then
                    cswmItem.IconImage = "checkbox.gif"
                Else
                    cswmItem.IconImage = "blank.gif"
                End If
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "None"
            cswmItem.ID = "mnuBDNone"
            If Session("BDCheck") = "None" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

        Else
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Scaling"
            cswmGroup1.ID = "Group_Scaling"


            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "No Scaling"
            cswmItem.IconHeight = 16
            If Session("PE_CommonMenu") = Nothing Then
                Session("PE_CommonMenu") = "Scale"
            End If
            If Session("PE_CommonMenu") = "Scale" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.IconHeight = 16
            cswmItem.Caption = "Common"
            cswmItem.IconHeight = 16

            If Session("PE_CommonMenu") = "Common" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If


            'jrs 2014/03/21
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.IconHeight = 16
            cswmItem.Caption = "Index Base 1"
            cswmItem.IconHeight = 16
            If Session("PE_CommonMenu") = "Base1" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

        End If

        'JAM 2017-07-30 Adding Stacked Options
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Selection"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regions"

        'JAM 2017-08-01
        If Session("PatternSel") = "C" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If



        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Scenarios"

        'JAM 2017-08-01
        If Session("PatternSel") = "S" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Variables"

        'JAM 2017-08-01
        If Session("PatternSel") = "V" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If



        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Years"

        'JAM 2017-08-01
        If Session("PatternSel") = " " Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'JAM 2017-09-03
        If Session("GR2N")(1) <> "" And Not IsNothing(Session("GR2N")(1)) Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Second Dimmension"


            If Session("PatternSel") = "V2" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If
        End If
        'JAM 2017-09-03
        If Session("GR3N")(1) <> "" And Not IsNothing(Session("GR3N")(1)) Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Third Dimmension"

            If Session("PatternSel") = "V3" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If
        End If


        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Format"



        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display/Run Horizon"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Alias for Group Names"
        If Session("bAlias") Then
            cswmItem.IconImage = "checkbox.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels First Dimension Only"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Second Dimension Only"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Third Dimension Only"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Dimensions Only"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels No Dimensions"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Scenario Names"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Reset"

        'jrs 2012/03/20
        If Session("GType") = "LINE" Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Display Vertical Line"

            Dim cswmGroup2 As Coalesys.WebMenu.Group
            cswmGroup2 = cswmItem.AddGroup()

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Base Year"

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Custom Year"

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Hide Vertical Line"
        End If

        If Session("GType") = "SCAT" Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Swap"
            cswmGroup1.ID = "mnuSwap"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Swap Variables in Y-X"

            'jrs 2013/05/01
            If Session("sVarZ") > "" Then
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Swap Variables in Z (Bubble Diameter) and Y"
                cswmItem.ID = "mnuSwapZY"
                If Session("BDCheck") <> "Other" Then
                    cswmItem.Enabled = False
                End If

                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Swap Variables in Z (Bubble Diameter) and X"
                cswmItem.ID = "mnuSwapZX"
                If Session("BDCheck") <> "Other" Then
                    cswmItem.Enabled = False
                End If
            End If
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16031);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'jrs 2012/03/19
        Dim sOther As String = ""
        If Session("sVarz") > "" Then
            sOther = Session("sVarz")
        End If

        'Choose the correct function
        Select Case objEA.Item.Caption
                'Case "Continue"
                '    mnuContinue_Click(sender, e)
            Case "Save", "Print"
            Case "Common" '"Delete Selected Driver/Dimension"
                'jrs 2014/03/21 mnuCommon_Click(sender, e)
                objSender.Groups("Scaling").Items("Common").IconImage = "checkbox.gif"
                objSender.Groups("Scaling").Items("No Scaling").IconImage = "blank.gif"
                objSender.Groups("Scaling").Items("Index Base 1").IconImage = "blank.gif"
                Session("CommonPlot") = "True"
                Session("PE_CommonMenu") = "Common"
                PackJS()
            Case "No Scaling" 'jrs 2014/03/21
                objSender.Groups("Scaling").Items("Common").IconImage = "blank.gif"
                objSender.Groups("Scaling").Items("No Scaling").IconImage = "checkbox.gif"
                objSender.Groups("Scaling").Items("Index Base 1").IconImage = "blank.gif"
                Session("CommonPlot") = "False"
                Session("PE_CommonMenu") = "Scale"
                PackJS()
            Case "Index Base 1" 'jrs 2014/03/21
                objSender.Groups("Scaling").Items("Common").IconImage = "blank.gif"
                objSender.Groups("Scaling").Items("No Scaling").IconImage = "blank.gif"
                objSender.Groups("Scaling").Items("Index Base 1").IconImage = "checkbox.gif"
                Session("CommonPlot") = "False"
                Session("PE_CommonMenu") = "Base1"
                PackJS()
            Case "Advance"
                mnuAdvance_Click(sender, e)
                PackJS()
            Case "Regress"
                mnuRegress_Click(sender, e)
                PackJS()
            Case "Go To Year"
                mnuGoToYear_Click()
            Case "Regions"   'JAM 2017-07-30
                mnuSelRegions_Click()

                If Session("PatternSel") = "C" Then
                    objSender.Groups("Selection").Items("Regions").IconImage = "checkbox.gif"
                    objSender.Groups("Selection").Items("Scenarios").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Variables").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Years").IconImage = "blank.gif"
                    If Session("GR2N")(1) <> "" And Not IsNothing(Session("GR2N")(1)) Then
                        objSender.Groups("Selection").Items("Second Dimmension").IconImage = "blank.gif"
                    End If
                    If Session("GR3N")(1) <> "" And Not IsNothing(Session("GR3N")(1)) Then
                        objSender.Groups("Selection").Items("Third Dimmension").IconImage = "blank.gif"
                    End If
                Else
                    objSender.Groups("Selection").Items("Regions").IconImage = "blank.gif"
                End If


            Case "Scenarios"  'JAM 2017-07-30
                mnuSelScenarios_Click()
                If Session("PatternSel") = "S" Then
                    objSender.Groups("Selection").Items("Regions").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Scenarios").IconImage = "checkbox.gif"
                    objSender.Groups("Selection").Items("Variables").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Years").IconImage = "blank.gif"
                    If Session("GR2N")(1) <> "" And Not IsNothing(Session("GR2N")(1)) Then
                        objSender.Groups("Selection").Items("Second Dimmension").IconImage = "blank.gif"
                    End If
                    If Session("GR3N")(1) <> "" And Not IsNothing(Session("GR3N")(1)) Then
                        objSender.Groups("Selection").Items("Third Dimmension").IconImage = "blank.gif"
                    End If
                Else
                    objSender.Groups("Selection").Items("Scenarios").IconImage = "blank.gif"
                End If


            Case "Variables"   'JAM 2017-07-30
                mnuSelVariables_Click()

                If Session("PatternSel") = "V" Then
                    objSender.Groups("Selection").Items("Regions").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Scenarios").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Variables").IconImage = "checkbox.gif"
                    objSender.Groups("Selection").Items("Years").IconImage = "blank.gif"
                    If Session("GR2N")(1) <> "" And Not IsNothing(Session("GR2N")(1)) Then
                        objSender.Groups("Selection").Items("Second Dimmension").IconImage = "blank.gif"
                    End If
                    If Session("GR3N")(1) <> "" And Not IsNothing(Session("GR3N")(1)) Then
                        objSender.Groups("Selection").Items("Third Dimmension").IconImage = "blank.gif"
                    End If
                Else
                    objSender.Groups("Selection").Items("Variables").IconImage = "blank.gif"
                End If



            Case "Years"    'JAM 2017-07-30
                mnuSelYears_Click()

                If Session("PatternSel") = " " Then
                    objSender.Groups("Selection").Items("Regions").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Scenarios").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Variables").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Years").IconImage = "checkbox.gif"
                    If Session("GR2N")(1) <> "" And Not IsNothing(Session("GR2N")(1)) Then
                        objSender.Groups("Selection").Items("Second Dimmension").IconImage = "blank.gif"
                    End If
                    If Session("GR3N")(1) <> "" And Not IsNothing(Session("GR3N")(1)) Then
                        objSender.Groups("Selection").Items("Third Dimmension").IconImage = "blank.gif"
                    End If
                Else
                    objSender.Groups("Selection").Items("Years").IconImage = "blank.gif"
                End If

            Case "Second Dimmension"   'JAM 2017-09-03
                mnuSelVariables2nd_Click()

                If Session("PatternSel") = "V2" Then
                    objSender.Groups("Selection").Items("Regions").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Scenarios").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Variables").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Years").IconImage = "blank.gif"
                    If Session("GR2N")(1) <> "" And Not IsNothing(Session("GR2N")(1)) Then
                        objSender.Groups("Selection").Items("Second Dimmension").IconImage = "checkbox.gif"
                    End If
                    If Session("GR3N")(1) <> "" And Not IsNothing(Session("GR3N")(1)) Then
                        objSender.Groups("Selection").Items("Third Dimmension").IconImage = "blank.gif"
                    End If
                Else
                    If Session("GR2N")(1) <> "" And Not IsNothing(Session("GR2N")(1)) Then
                        objSender.Groups("Selection").Items("Second Dimmension").IconImage = "blank.gif"
                    End If
                End If

            Case "Third Dimmension"   'JAM 2017-09-03
                mnuSelVariables3rd_Click()

                If Session("PatternSel") = "V3" Then
                    objSender.Groups("Selection").Items("Regions").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Scenarios").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Variables").IconImage = "blank.gif"
                    objSender.Groups("Selection").Items("Years").IconImage = "blank.gif"
                    If Session("GR2N")(1) <> "" And Not IsNothing(Session("GR2N")(1)) Then
                        objSender.Groups("Selection").Items("Second Dimmension").IconImage = "blank.gif"
                    End If
                    If Session("GR3N")(1) <> "" And Not IsNothing(Session("GR3N")(1)) Then
                        objSender.Groups("Selection").Items("Third Dimmension").IconImage = "checkbox.gif"
                    End If
                Else
                    If Session("GR3N")(1) <> "" And Not IsNothing(Session("GR3N")(1)) Then
                        objSender.Groups("Selection").Items("Third Dimmension").IconImage = "blank.gif"
                    End If
                End If


            Case "Set Title, Display Interval, or Year" 'jrs 2005/10/19
                'jrs 2012/07/23 Session("DisplayControlParent") = "frm_StackedBar.aspx"
                Session("DisplayControlParent") = Request.Url.AbsoluteUri
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
            Case "Display/Run Horizon" 'jrs 2011/07/04
                mnuDisplayRunHorizon_Click(sender, e)
            Case "Use Alias for Group Names" 'jrs 2011/07/04
                'jrs 2012/07/02
                If Not Session("bAlias") Then
                    Response.Redirect(setAliasURL("1"))
                Else
                    Response.Redirect(setAliasURL("0"))
                End If
                        'mnuAlias_Click()
            Case "Format Legend Labels"
                'jrs 2012/07/23 Session("GraphLabelsParent") = "frm_StackedBar.aspx"
                Session("GraphLabelsParent") = Request.Url.AbsoluteUri
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_GraphLabels.aspx")

            Case "Format Legend Labels First Dimension Only" 'jrs 2008/01/21
                'jrs 2012/07/02
                Response.Redirect(setLegURL("1"))
                        'mnuFormLeg1stDim_Click()
            Case "Format Legend Labels Second Dimension Only" 'jrs 2007/08/10
                'jrs 2012/07/02
                Response.Redirect(setLegURL("2"))
                        'mnuFormLeg2ndDim_Click()
            Case "Format Legend Labels Third Dimension Only" 'jrs 2008/01/24
                'jrs 2012/07/02
                Response.Redirect(setLegURL("3"))
                        'mnuFormLeg3rdDim_Click()
            Case "Format Legend Labels Dimensions Only" 'jrs 2008/01/24
                'jrs 2012/07/02
                Response.Redirect(setLegURL("4"))
                        'mnuFormLegDims_Click()
            Case "Format Legend Labels No Dimensions" 'jrs 2008/01/24
                'jrs 2012/07/02
                Response.Redirect(setLegURL("0"))
                        'mnuFormLegNoDim_Click()
            Case "Format Legend Labels Scenario Names"
                'jrs 2012/07/02
                Response.Redirect(setLegURL("5"))
                        'mnuFormLegSce_Click()
            Case "Format Legend Labels Reset"
                'jrs 2012/07/02
                Response.Redirect(setLegURL("9"))
                        'mnuFormLegReset_Click()
            Case "Swap Variables in Y-X"
                mnuSwapVars_Click()
                PackJS()
            Case "Swap Variables in Z (Bubble Diameter) and Y" 'jrs 2013/05/01
                mnuSwapVarsZY_Click()
                PackJS()
            Case "Swap Variables in Z (Bubble Diameter) and X" 'jrs 2013/05/01
                mnuSwapVarsZX_Click()
                PackJS()
            Case "Use a G-List" 'jrs 2012/03/12
                mnuUseGList_Click()
                PackJS()
            Case "Use a Group"
                mnuUseGroup_Click()
                PackJS()
            Case "POP" 'jrs 2012/03/16
                mnuBDPOP_Click()
                PackJS()
            Case "GDP" 'jrs 2012/03/19
                mnuBDGDP_Click()
                PackJS()
            Case sOther
                mnuBDOther_Click()
                PackJS()
            Case "None"
                mnuBDNone_Click()
                PackJS()
            Case "Base Year"
                Session("VLYear") = FirstYearForecast
                PackJS()
            Case "Custom Year"
                mnuVLine_Click()
                PackJS()
            Case "Hide Vertical Line"
                Session.Remove("VLYear")
                PackJS()
            Case "Linear" 'jrs 2012/05/28
                mnuLinear_Click()
                PackJS()
            Case "Logarithmic" 'jrs 2012/05/29
                mnuLog_Click()
                PackJS()
            Case "Exponential" 'jrs 2012/05/29
                mnuExpon_Click()
                PackJS()
            Case "Power" 'jrs 2012/05/30
                mnuPower_Click()
                PackJS()
            Case "Polynomial Second Degree" 'jrs 2012/05/30
                mnuSecondDeg_Click()
                PackJS()
            Case "Polynomial Third Degree" 'jrs 2012/05/30
                mnuThirdDeg_Click()
                PackJS()
            Case "Logistic" 'jrs 2012/05/30
                mnuLogistic_Click()
                PackJS()
            Case "No Regression" 'jrs 2012/05/30
                mnuNoRegress_Click()
                PackJS()
            Case "Turn formulation display off" 'jrs 2012/05/31
                Session("TableTitle") = ""
                PackJS()
            Case "Statistics" 'jrs 2012/06/04
                mnuStatistics_Click()
            Case "Use all Years (in Statistics)" 'JAM 2016-02-12
                AllYears()
                If Session("mnuUseAllYrs") Then
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = "checkbox.gif"
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = "blank.gif"
                Else
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = "blank.gif"
                End If


            Case "Use all Years and Time (in Statistics)" 'JAM 2016-02-12
                AllYearsAndTime()
                If Session("mnuUseAllYrsTime") Then
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = "checkbox.gif"
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = "blank.gif"
                Else
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = "blank.gif"
                End If


        End Select
    End Sub

    'jrs 2012/07/02
    Private Function setLegURL(ByVal sOpt As String) As String
        Dim sUrl As String, sLeg As String
        sUrl = Request.Url.AbsoluteUri
        If Not IsNothing(Request.QueryString("Leg")) Then
            sLeg = Request.QueryString("Leg")
            sUrl = Replace(sUrl, "Leg=" & sLeg, "Leg=" & sOpt)
        Else
            If InStr(sUrl, "&") > 0 Or InStr(sUrl, "?") > 0 Then
                sUrl = sUrl & "&"
            Else
                sUrl = sUrl & "?"
            End If
            sUrl = sUrl & "Leg=" & sOpt
        End If

        setLegURL = sUrl
    End Function

    'jrs 2012/07/03
    Private Function setAliasURL(ByVal sOpt As String) As String
        Dim sUrl As String, sAlias As String
        sUrl = Request.Url.AbsoluteUri
        If Not IsNothing(Request.QueryString("Alias")) Then
            sAlias = Request.QueryString("Alias")
            sUrl = Replace(sUrl, "Alias=" & sAlias, "Alias=" & sOpt)
        Else
            If InStr(sUrl, "&") > 0 Or InStr(sUrl, "?") > 0 Then
                sUrl = sUrl & "&"
            Else
                sUrl = sUrl & "?"
            End If
            sUrl = sUrl & "Alias=" & sOpt
        End If

        setAliasURL = sUrl
    End Function

    Private Sub mnuAlias_Click()
        Dim i As Integer
        If Not Session("bAlias") Then
            Session("bAlias") = True
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("Display Format").Items("Use Alias for Group Names").IconImage = "checkbox.gif"
            End If

            'Read and load all aliases
            Dim Tbl As DataTable
            Dim s As String
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT GroupName, Alias FROM " & GroupIDNew, "GroupName", Tbl)
            For i = 1 To Session("COUNTE")
                s = SeekValue(Tbl, Session("GR1N")(i), "Alias")
                If s > "" Then
                    Session("GR1N")(i) = s
                End If
            Next i
        Else
            Session("bAlias") = False
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("Display Format").Items("Use Alias for Group Names").IconImage = ""
            End If

            For i = 1 To Session("COUNTE")
                Session("GR1N")(i) = Session("oGR1N")(i)
            Next i
        End If
        mnuFormLegReset_Click()
    End Sub

    Private Sub mnuFormLeg1stDim_Click()
        'jrs 2007/08/06
        Dim I As Integer
        For I = 1 To Session("COUNTE")
            If Trim(Session("GR1N")(I)) > "" Then
                Session("VARN")(0, I) = Session("GR1N")(I)
                Session("FDNG1")(I) = ""
                Session("FDNG2")(I) = ""
                Session("FDNG3")(I) = ""
            End If
        Next I
    End Sub

    Private Sub mnuFormLeg2ndDim_Click()
        'jrs 2007/08/06
        Dim I As Integer
        For I = 1 To Session("COUNTE")
            If Trim(Session("GR2N")(I)) > "" Then
                Session("VARN")(0, I) = Session("GR2N")(I)
                Session("FDNG1")(I) = ""
                Session("FDNG2")(I) = ""
                Session("FDNG3")(I) = ""
            End If
        Next I
    End Sub

    'jrs 2008/01/24
    Private Sub mnuFormLeg3rdDim_Click()
        'jrs 2007/08/06
        Dim I As Integer
        For I = 1 To Session("COUNTE")
            If Trim(Session("GR3N")(I)) > "" Then
                Session("VARN")(0, I) = Session("GR3N")(I)
                Session("FDNG1")(I) = ""
                Session("FDNG2")(I) = ""
                Session("FDNG3")(I) = ""
            End If
        Next I

    End Sub

    'jrs 2008/01/24
    Private Sub mnuFormLegDims_Click()
        'jrs 2007/08/06
        Dim I As Integer
        For I = 1 To Session("COUNTE")
            If Trim(Session("GR1N")(I)) > "" Then
                Session("VARN")(0, I) = Session("GR1N")(I)
                Session("FDNG1")(I) = Session("GR2N")(I)
                Session("FDNG2")(I) = Session("GR3N")(I)
                Session("FDNG3")(I) = ""
            End If
        Next I
    End Sub

    'jrs 2008/01/24
    Private Sub mnuFormLegNoDim_Click()
        'jrs 2007/08/06
        Dim I As Integer
        For I = 1 To Session("COUNTE")
            Session("FDNG1")(I) = ""
            Session("FDNG2")(I) = ""
            Session("FDNG3")(I) = ""
        Next I
    End Sub

    'jrs 2008/01/18
    Private Sub mnuFormLegSce_Click()
        'jrs 2007/08/06
        Dim i As Integer
        Dim pos1 As Integer, pos2 As Integer
        For i = 1 To Session("COUNTE")
            pos1 = InStr(Session("oVARN0")(i), "[")
            pos2 = InStr(Session("oVARN0")(i), "]")
            If pos1 > 0 And pos2 > 0 Then
                Session("VARN")(0, i) = Mid(Session("oVARN0")(i), pos1 + 1, pos2 - pos1 - 1)
            End If

            Session("FDNG1")(i) = ""
            Session("FDNG2")(i) = ""
            Session("FDNG3")(i) = ""
        Next i
    End Sub

    Private Sub mnuFormLegReset_Click()
        Dim i As Integer
        For i = 1 To Session("COUNTE")
            Session("VARN")(0, i) = Session("oVARN0")(i)
            Session("FDNG1")(i) = Session("GR1N")(i)
            Session("FDNG2")(i) = Session("GR2N")(i)
            Session("FDNG3")(i) = Session("GR3N")(i)
        Next i
    End Sub

    'jrs 2015/09/08
    'Procedure to convert to JavaScript format to load HighCharts
    Private Sub PackJS()

        Dim icat As Integer
        'jrs 2011/09/20
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        If Session("GType") = "SCAT" And Session("bBubble") = True Then
            sJscript2 = "<script src=""http://code.highcharts.com/highcharts-more.js""></script>" & vbCr
            sJscript2 = sJscript2 & "<script>" & vbCr
        End If
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/10/06 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        'sJscript2 = sJscript2 & "fontFamily: 'Arial, sans-serif'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        'sJscript2 = sJscript2 & "color: '#333333' " & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/02/23 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        If Session("GType") = "BAR" Then
            sJscript2 = sJscript2 & "type: 'column'," & vbCr
        ElseIf Session("GType") = "PIE" Then
            sJscript2 = sJscript2 & "type: 'pie'," & vbCr
        ElseIf Session("GType") = "SCAT" Then
            If Session("bBubble") = True Then
                sJscript2 = sJscript2 & "type: 'bubble'," & vbCr
            Else
                sJscript2 = sJscript2 & "type: 'scatter'," & vbCr
            End If
        End If
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        If Session("TableTitle") > "" And Session("TableTitle") <> "Title" Then
            sJscript2 = sJscript2 & "text:       '" & Session("TableTitle") & "'," & vbCr
        Else
            sJscript2 = sJscript2 & "text:       ''," & vbCr
        End If
        'jam 2025/10/06  Increasing font size for legends
        sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '30px'}," & vbCr
        ''''''''''''''''''''''''''''

        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Session("GType") = "PIE" Then
            sJscript2 = sJscript2 & "subtitle: {" & vbCr
            sJscript2 = sJscript2 & "text:       ""Year " & CStr(Session("FirstYear") + Session("PYear") - 1) & """," & vbCr
            sJscript2 = sJscript2 & "x: -20 //center" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        Dim DimCount As Integer = 0
        If Session("GType") = "SCAT" Then
            If Session("VarsCount") <> 0 Then
                DimCount = Session("COUNTE") / Session("VarsCount")
            End If
        End If


        Dim sAxisXLabel As String
        sAxisXLabel = getXAxisLabel(DimCount + 1)
        Dim sAxisYLabel As String
        sAxisYLabel = getYAxisLabel()


        If Session("PatternSel") = " " Then  'JAM 2017-08-01


            If Session("GType") = "SCAT" Then
                sJscript2 = sJscript2 & "tooltip: {" & vbCr
                sJscript2 = sJscript2 & "formatter: function () {" & vbCr
                sJscript2 = sJscript2 & "return '<span style=""color:' + this.point.fillColor + '"">●</span>' + ' ' + this.point.name + '<br>" & sAxisXLabel & ": ' + this.point.x + '<br>" & sAxisYLabel & ": ' + this.point.y"
                If Session("bBubble") = True Then
                    sJscript2 = sJscript2 & " + '<br>" & Session("VARN")(1, 1 + pCOUNTE) & ": ' + this.point.z;" & vbCr
                Else
                    sJscript2 = sJscript2 & ";" & vbCr
                End If
                sJscript2 = sJscript2 & "}" & vbCr
                sJscript2 = sJscript2 & "}," & vbCr
            End If

            'sJscript2 = sJscript2 & "subtitle: {" & vbCr
            'sJscript2 = sJscript2 & "text:       'Source: WorldClimate.com'," & vbCr
            'sJscript2 = sJscript2 & "x: -20" & vbCr
            'sJscript2 = sJscript2 & "}," & vbCr
            sJscript2 = sJscript2 & "xAxis: {" & vbCr

            'jam 2025/10/13 Increasing font for x axis
            sJscript2 = sJscript2 & "labels: {" & vbCr
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "color: 'black'," & vbCr
            sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
            sJscript2 = sJscript2 & "}}," & vbCr


            sJscript2 = sJscript2 & "title: {" & vbCr

            'jam 2025/10/13 Increasing font for x axis
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr


            sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            'Load Years
            '----------
            'sJscript2 = sJscript2 & "categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'," & vbCr
            'sJscript2 = sJscript2 & "'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']" & vbCr
            If Session("GType") <> "BAR" And Session("GType") <> "PIE" And Session("GType") <> "SCAT" Then
                'jrs 2015/09/11 removing categories from Bar chart, problematic for multiple series
                'it wasn't showing all bars in the first and last categories
                sJscript2 = sJscript2 & ", categories: ["
                Dim i As Integer
                For i = 1 To Session("DisYear") Step Session("Interval")
                    If i > 1 Then
                        sJscript2 = sJscript2 & ","
                    End If
                    sJscript2 = sJscript2 & "'" & CStr(Session("FirstYear") + i - 1) & "'"
                Next
                sJscript2 = sJscript2 & "]" & vbCr
            End If
            'jrs 2015/09/08
            If Session("GType") <> "PIE" And Session("GType") <> "SCAT" Then
                If Session("DisYear") > 21 Or Session("Interval") > 1 Then
                    If Session("Interval") > 1 Then
                        sJscript2 = sJscript2 & ", tickInterval: " & Session("Interval") & vbCr
                    Else
                        sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr
                    End If
                End If
            End If

            If Not (Session("VLYear") Is Nothing) Then
                sJscript2 = sJscript2 & ", plotLines: [{" & vbCr
                sJscript2 = sJscript2 & "color:          'red', " & vbCr
                sJscript2 = sJscript2 & "value: " & CStr(Session("VLYear")) & ", " & vbCr
                sJscript2 = sJscript2 & "width: 2 " & vbCr
                sJscript2 = sJscript2 & "}]" & vbCr
            End If
            'jrs 2015/09/18
            If Session("GType") <> "PIE" And Session("GType") <> "SCAT" Then
                sJscript2 = sJscript2 & ", min: " & Session("FirstYear") & vbCr
                sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("DisYear") - 1) & vbCr
            End If

            sJscript2 = sJscript2 & "}," & vbCr

        End If  ''Session("PatternSel") = " "

        sJscript2 = sJscript2 & "yAxis: {" & vbCr

        'jam 2025/10/13 Increasing font for y axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr


        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

        'jam 2025/10/13 Increasing font for y axis
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr
        'sJscript2 = sJscript2 & "text:       '" & getYAxisLabel() & "'," & vbCr
        'sJscript2 = sJscript2 & "style: {" & vbCr
        'sJscript2 = sJscript2 & "fontSize:   '25px'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr


        'jrs 2015/09/08
        sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "legend: {" & vbCr




        sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
        sJscript2 = sJscript2 & "align:      'center'," & vbCr
        'jrs 2016/01/04
        If Session("GType") = "SCAT" Then
            sJscript2 = sJscript2 & "verticalAlign:  'bottom'," & vbCr
            sJscript2 = sJscript2 & "x: 20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "y: 0, " & vbCr 'avoid overlap
        Else
            sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
            sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "y: 40, " & vbCr 'jrs 2016/04/26 changed from 20 to 40 'avoid overlap 
        End If
        sJscript2 = sJscript2 & "borderWidth: 0" & vbCr



        sJscript2 = sJscript2 & "}," & vbCr
        'jrs 2015/09/18
        If Session("GType") = "SCAT" Then
            sJscript2 = sJscript2 & "plotOptions: {" & vbCr
            sJscript2 = sJscript2 & "   series: {" & vbCr
            sJscript2 = sJscript2 & "       point: {" & vbCr
            sJscript2 = sJscript2 & "           events: {" & vbCr
            sJscript2 = sJscript2 & "               click: function () {" & vbCr
            sJscript2 = sJscript2 & "                   this.options.dataLabels.enabled = !this.options.dataLabels.enabled;" & vbCr
            sJscript2 = sJscript2 & "                   this.update();" & vbCr
            sJscript2 = sJscript2 & "               }" & vbCr
            sJscript2 = sJscript2 & "           }" & vbCr
            sJscript2 = sJscript2 & "       }" & vbCr
            sJscript2 = sJscript2 & "   }" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        'JAM 2017-06-11 Adding Stacked Bar Option
        sJscript2 = sJscript2 & "plotOptions: {" & vbCr
        sJscript2 = sJscript2 & "       column: {" & vbCr
        sJscript2 = sJscript2 & "           stacking: 'normal' " & vbCr
        sJscript2 = sJscript2 & "       } " & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        ''''''''''''''''''''''''''''''''''''''''''

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim sZeros As String = ""
        If Session("GType") = "PIE" Then
            sJscript2 = sJscript2 & PlotYearToJSSeries(Session("PYear"), sZeros)
        ElseIf Session("GType") = "SCAT" Then
            sJscript2 = sJscript2 & PlotPairToJSSeries(Session("PYear"), Session("VarsCount"), Session("bBubble"))
            'jrs 2012/05/28
            If Session("RegressType") <> "" Then
                'Plot Regression
                sJscript2 = sJscript2 & "," & PlotRegPairToJSSeries()
            End If
        Else
            'JAM 2017-07-30  A different pattern is applied
            If Session("PatternSel") <> " " Then
                sJscript2 = sJscript2 & PatternFillData()

            Else
                For i = 1 To Session("COUNTE")
                    If i > 1 Then
                        sJscript2 = sJscript2 & "," & vbCr
                    End If
                    sJscript2 = sJscript2 & PlotColToJSSeries(i)
                Next
            End If
        End If
        sJscript2 = sJscript2 & "]" & vbCr

        'JAM 2017-08-01  Defining XAxis
        If Session("PatternSel") <> " " Then
            sJscript2 = sJscript2 & "," & vbCr
            sJscript2 = sJscript2 & "xAxis: { " & vbCr
            sJscript2 = sJscript2 & "title: { " & vbCr

            If Session("mnuFormatGrpbyLocation.Checked") Then
                sJscript2 = sJscript2 & "text: 'Year' " & vbCr
            Else
                If Session("PatternSel") = "C" Then
                    sJscript2 = sJscript2 & "text: 'Region' " & vbCr
                Else
                    If Session("PatternSel") = "V" Then
                        sJscript2 = sJscript2 & "text: 'Variable' " & vbCr
                    Else
                        If Session("PatternSel") = "S" Then
                            sJscript2 = sJscript2 & "text: 'Scenario' " & vbCr
                        Else
                            If Session("PatternSel") = "V2" Then 'JAM 2017-09-03
                                sJscript2 = sJscript2 & "text: 'Second Dimmension' " & vbCr
                            Else
                                If Session("PatternSel") = "V3" Then 'JAM 2017-09-03 
                                    sJscript2 = sJscript2 & "text: 'Third Dimmension' " & vbCr
                                Else
                                    sJscript2 = sJscript2 & "text: 'Year' " & vbCr
                                End If
                            End If
                        End If
                    End If

                End If
            End If
            sJscript2 = sJscript2 & "}," & vbCr
            sJscript2 = sJscript2 & "categories: [" & vbCr

            For icat = 1 To Session("Pego1.Points")
                sJscript2 = sJscript2 & "'" & Session("VarNumber")(icat) & "'"
                If icat < Session("Pego1.Points") Then
                    sJscript2 = sJscript2 & ","
                End If
            Next

            sJscript2 = sJscript2 & "]" & vbCr



            sJscript2 = sJscript2 & "}" & vbCr
        End If


        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr
        sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".css({"
        sJscript2 = sJscript2 & "cursor: 'pointer'"
        sJscript2 = sJscript2 & "})"
        sJscript2 = sJscript2 & ".add();" & vbCr

        'If Not (Session("VLYear") Is Nothing) Then
        '    sJscript2 = sJscript2 & "chart.renderer.path(['m', " & CStr((Session("VLYear") - Session("FirstYear")) * 70 + 100) & ", 0, 'V', 800])" & vbCr
        '    sJscript2 = sJscript2 & ".attr({" & vbCr
        '    sJscript2 = sJscript2 & "'stroke-width': 1," & vbCr
        '    sJscript2 = sJscript2 & "stroke:         'red'," & vbCr
        '    sJscript2 = sJscript2 & "zIndex:1000" & vbCr
        '    sJscript2 = sJscript2 & "})" & vbCr
        '    sJscript2 = sJscript2 & ".add();" & vbCr
        'End If


        sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub


    Public Sub mnuCommon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objEA.Item.Caption = "Common" Then
            objSender.Groups("Scaling").Items("Common").IconImage = "checkbox.gif"
            objSender.Groups("Scaling").Items("No Scaling").IconImage = "blank.gif"
            Session("CommonPlot") = "True"
        Else  'Scale
            objSender.Groups("Scaling").Items("No Scaling").IconImage = "checkbox.gif"
            objSender.Groups("Scaling").Items("Common").IconImage = "blank.gif"
            Session("CommonPlot") = "False"
        End If

    End Sub

    ''jrs 2011/07/04
    Public Sub mnuDisplayRunHorizon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If CBool(Session("HistoricBase")) = True Then
            Msg = "You cannot change the horizon for historical analysis."

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        Dim rr, cc As Integer
        ' Dim aa


        'JAM 2016-03-06  Reverting Trillions units applied
        If Session("bTrillionsOk") Then
            For cc = 1 To Session("COUNTE")
                For rr = 1 To Session("DisYear")
                    'aa = Session("Unit_PLOT")(rr, cc)
                    Session("PLOT")(rr, cc) = Session("PLOTTrillions")(rr, cc) 'JAM 2016-05-16
                Next rr
            Next cc

            Session("prevHorizonYear") = Session("DisYear")
        End If
        Session("bTrillionsOk") = False


        cmb_Horizon.Visible = True
        frm_Horizon.Visible = True
        cmb_Horizon.Items.Clear()
        cmb_Horizon.Items.Add("")
        cmb_Horizon.Items.Add(New ListItem(Session("FirstYear").ToString))

        'jrs 2016/03/23
        Dim iFirstYear As Integer
        If Session("FirstYear") Mod 5 = 0 Or Session("Interval") Mod 5 <> 0 Then
            iFirstYear = Session("FirstYear")
        Else
            Dim Diff As Integer
            Diff = Session("FirstYear") Mod 5
            iFirstYear = Session("FirstYear") + 5 - Diff
        End If
        For ICount = iFirstYear + Session("Interval") To Session("LastYearForecast") Step Session("Interval") 'jrs 2005/12/11 added Session("Interval") 'jrs 2016/03/23 session("FirstYear")
            cmb_Horizon.Items.Add(New ListItem(CStr(ICount)))
        Next ICount

        cmb_Horizon.Text = Session("DisplayLastYear")

        'jrs 2012/03/09
        txtHorizon.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdHorizon.ClientID + "')")

        lblJS.Visible = False
        cswmWebMenu.Visible = False

    End Sub

    'jrs 2012/03/09
    Public Sub mnuGoToYear_Click()
        frm_GoToYear.Visible = True
        txtGoToYear.Text = ""

        txtGoToYear.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdGoToYear.ClientID + "')")

        lblJS.Visible = False
        cswmWebMenu.Visible = False
    End Sub

    'jrs 2012/03/21
    Public Sub mnuVLine_Click()
        frm_VLine.Visible = True
        txtVLine.Text = ""

        txtVLine.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdVLine.ClientID + "')")

        lblJS.Visible = False
        cswmWebMenu.Visible = False
    End Sub

    Protected Sub cmb_Horizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged
        Dim Ind As Integer




        Ind = cmb_Horizon.SelectedIndex - 1
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If
        'jrs 2016/03/23
        Session("DisplayLastYear") = Val(cmb_Horizon.SelectedValue)
        'If Ind >= 1 Then
        'jrs 2016/03/23 Session("DisplayLastYear") = Session("FirstYear") + Ind * Session("Interval")
        'Else
        If Ind < 1 Then
            'jrs 2016/03/23 Session("DisplayLastYear") = Val(cmb_Horizon.SelectedValue)
            If Session("FirstYear") = FirstYearForecast Then
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = Session("LastYear")
                    LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If

            ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = Session("LastYear")
                    LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If

            End If
        End If
        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False




        'jrs 2011/08/26
        Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1

        'JAM 2016-06-03 Convert from Billions to Trillions
        Call Convert_Billions_To_Trillions()

        PackJS()
        'jrs 2012/03/09
        lblJS.Visible = True
        cswmWebMenu.Visible = True

    End Sub

    Public Sub mnuAdvance_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("PYear") = Session("PYear") + 5
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)
        'jrs 2012/05/30
        Session("RegressType") = ""
        If Session("GType") <> "PIE" Then
            Session("TableTitle") = ""
        End If

    End Sub

    Public Sub mnuRegress_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("PYear") = Session("PYear") - 5
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
        'jrs 2012/05/30
        Session("RegressType") = ""
        If Session("GType") <> "PIE" Then
            Session("TableTitle") = ""
        End If


    End Sub

    Private Sub ColorDistribution()
        Dim DimCount As Integer = 0
        If Session("VarsCount") <> 0 Then
            DimCount = Session("COUNTE") / Session("VarsCount")
        End If
        'For All Regions do the same color for region
        If DimCount = NR Then
            ColorDistributionNR()
            Exit Sub
        End If

        'jrs 2010/09/28
        'Distribute Colors
        Dim GreenIdx As Integer, OrangeIdx As Integer, BlueIdx As Integer
        Dim YellowIdx As Integer, NavyBlueIdx As Integer, RedIdx As Integer
        Dim i As Integer
        GreenIdx = 0
        OrangeIdx = 1
        BlueIdx = 2
        YellowIdx = 3
        NavyBlueIdx = 4
        RedIdx = 5

        'jrs 2010/09/14 Load Colors
        Dim RColor() As Integer, GColor() As Integer, BColor() As Integer
        ReDim RColor(AMAX(DimCount, 10))
        ReDim GColor(AMAX(DimCount, 10))
        ReDim BColor(AMAX(DimCount, 10))
        'Green for Africa 0, 150, 0
        RColor(GreenIdx) = 0
        GColor(GreenIdx) = 150
        BColor(GreenIdx) = 0
        'Orange for Asia 255, 102, 0
        RColor(OrangeIdx) = 255
        GColor(OrangeIdx) = 102
        BColor(OrangeIdx) = 0
        'Blue for Europe 0, 0, 150
        RColor(BlueIdx) = 0
        GColor(BlueIdx) = 0
        BColor(BlueIdx) = 150
        'Yellow for Latam&Car 255, 255, 0
        RColor(YellowIdx) = 255
        GColor(YellowIdx) = 255
        BColor(YellowIdx) = 0
        'Navy Blue for North America 0, 191, 255
        RColor(NavyBlueIdx) = 0
        GColor(NavyBlueIdx) = 191
        BColor(NavyBlueIdx) = 255
        'Red for Oceania 255, 0, 0
        RColor(RedIdx) = 255
        GColor(RedIdx) = 0
        BColor(RedIdx) = 0
        'brown 130, 43, 0
        RColor(6) = 130
        GColor(6) = 43
        BColor(6) = 0
        'magenta 128, 0, 128
        RColor(7) = 128
        GColor(7) = 0
        BColor(7) = 128
        'pink 255, 153, 204
        RColor(8) = 255
        GColor(8) = 153
        BColor(8) = 204
        'gray 128, 128, 128
        RColor(9) = 128
        GColor(9) = 128
        BColor(9) = 128
        'Load randomly the remaining NR - 10 colors
        For i = 10 To UBound(RColor)
            RColor(i) = (Rnd() * 1000) Mod 256
            GColor(i) = (Rnd() * 1000) Mod 256
            BColor(i) = (Rnd() * 1000) Mod 256
        Next i

        Session("RColor") = RColor
        Session("GColor") = GColor
        Session("BColor") = BColor
    End Sub

    Private Sub ColorDistributionNR()
        'jrs 2010/09/28
        'Distribute Colors
        Dim sql As String
        Dim rs As DataTable
        sql = "SELECT GroupName, RegionNo " &
        "From GListId, GList, " & GroupIDNew & ", " & Grouping &
        " WHERE GListId.Code = GList.GListCode And " &
        "GList.Region = " & GroupIDNew & ".GroupName AND " &
        GroupIDNew & ".GroupNum = " & Grouping & ".GroupNo AND " &
        "GListId.Description = 'UN Regions'"

        SetDataTablefromAccess(Session("IFs_MDB2"), sql, "", rs)
        Dim sGroupNames() As String
        ReDim sGroupNames(NR)
        Dim i As Integer = 0

        Do While i < rs.Rows.Count
            sGroupNames(rs.Rows(i).Item("RegionNo")) = rs.Rows(i).Item("GroupName")
            i = i + 1
        Loop

        Dim RColor() As Integer, GColor() As Integer, BColor() As Integer
        'jrs 2010/09/14 Load Colors
        ReDim RColor(NR)
        ReDim GColor(NR)
        ReDim BColor(NR)

        For i = 1 To NR
            If InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "AFRICA") > 0 Then
                'Green for Africa 0, 150, 0
                RColor(i - 1) = 0
                GColor(i - 1) = 150
                BColor(i - 1) = 0
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "ASIA") > 0 Then
                'Orange for Asia 255, 102, 0
                RColor(i - 1) = 255
                GColor(i - 1) = 102
                BColor(i - 1) = 0
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "EUROPE") > 0 Then
                'Blue for Europe 0, 0, 150
                RColor(i - 1) = 0
                GColor(i - 1) = 0
                BColor(i - 1) = 150
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "LATAM") > 0 Then
                'Yellow for Latam&Car 255, 255, 0
                RColor(i - 1) = 255
                GColor(i - 1) = 255
                BColor(i - 1) = 0
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "NORTH") > 0 Then
                'Navy Blue for North America 0, 191, 255
                RColor(i - 1) = 0
                GColor(i - 1) = 191
                BColor(i - 1) = 255
            ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "OCEANIA") > 0 Then
                'Red for Oceania 255, 0, 0
                RColor(i - 1) = 255
                GColor(i - 1) = 0
                BColor(i - 1) = 0
            Else
                'Random if not found
                RColor(i - 1) = (Rnd() * 1000) Mod 256
                GColor(i - 1) = (Rnd() * 1000) Mod 256
                BColor(i - 1) = (Rnd() * 1000) Mod 256
            End If
        Next i

        Session("RColor") = RColor
        Session("GColor") = GColor
        Session("BColor") = BColor
    End Sub

    'jrs 2010/10/22
    Private Sub mnuSwapVars_Click()
        On Error GoTo Hdlr
        'Copy source
        Dim pCopy As Object, VARNCopy As Object, gr2Copy As Object,
        gr3Copy As Object, gr4Copy As Object, fCopy As Object,
        FileNumbrCopy As Object, FinYearCopy As Object, FinYearHisCopy As Object,
        bNotDecomposablesCopy As Object, bLineFeedsCopy As Object,
        sGroupTitlesCopy As Object, sUsrDefCatsCopy As Object, DispDecimalsCopy As Object
        Dim FDNG2Copy As Object, FDNG3Copy As Object, FDNG4Copy As Object

        pCopy = VB6.CopyArray(Session("PLOT"))
        VARNCopy = VB6.CopyArray(Session("VARN"))
        gr2Copy = VB6.CopyArray(Session("GR2N"))
        gr3Copy = VB6.CopyArray(Session("GR3N"))
        gr4Copy = VB6.CopyArray(Session("GR4N"))
        fCopy = VB6.CopyArray(Session("aFormulas"))

        FDNG2Copy = VB6.CopyArray(Session("FDNG2"))
        FDNG3Copy = VB6.CopyArray(Session("FDNG3"))
        FDNG4Copy = VB6.CopyArray(Session("FDNG4"))

        FileNumbrCopy = VB6.CopyArray(Session("FileNumbr"))
        FinYearCopy = VB6.CopyArray(Session("FinYear"))
        FinYearHisCopy = VB6.CopyArray(Session("FinYearHis"))
        'Reset Target
        Dim j As Integer, k As Integer, i As Integer
        Dim focus As Integer, DimCount As Integer = 0
        If Session("VarsCount") <> 0 Then
            DimCount = Session("COUNTE") / Session("VarsCount")
        End If
        For i = 1 To DimCount * 2
            'source index
            If i <= DimCount Then
                k = i + DimCount
            Else
                k = i - DimCount
            End If
            'copy to target
            For j = 0 To UBound(Session("PLOT"), 1)
                Session("PLOT")(j, i) = pCopy(j, k)
            Next j

            For j = 0 To UBound(Session("VARN"), 1)
                Session("VARN")(j, i) = VARNCopy(j, k)
            Next j
            Session("GR2N")(i) = gr2Copy(k)
            Session("GR3N")(i) = gr3Copy(k)
            Session("GR4N")(i) = gr4Copy(k)
            If k <= UBound(fCopy) Then
                If i > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(i)
                End If
                Session("aFormulas")(i) = fCopy(k)
            End If
            Session("FDNG2")(i) = FDNG2Copy(k)
            Session("FDNG3")(i) = FDNG3Copy(k)
            Session("FDNG4")(i) = FDNG4Copy(k)
            Session("FileNumbr")(i) = FileNumbrCopy(k)
            Session("FinYear")(i) = FinYearCopy(k)
            Session("FinYearHis")(i) = FinYearHisCopy(k)
        Next i

        'save original name to display
        ReDim Session("oVARN0")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oVARN0")(i) = Session("VARN")(0, i)
        Next i
        'jrs 2010/02/04
        ReDim Session("oGR2N")(Session("COUNTE"))
        ReDim Session("oGR3N")(Session("COUNTE"))
        ReDim Session("oGR4N")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oGR2N")(i) = Session("GR2N")(i)
            Session("oGR3N")(i) = Session("GR3N")(i)
            Session("oGR4N")(i) = Session("GR4N")(i)
        Next i

        'jrs 2012/05/30
        Session("RegressType") = ""
        Session("TableTitle") = ""

        'Swap Variables to use when decomposing
        Dim sVartmp As String, fileIdxtmp As Integer, bCompTmp As Boolean
        sVartmp = Session("sVarY")
        fileIdxtmp = Session("fileIdxY")
        bCompTmp = Session("bCompY")

        Session("sVarY") = Session("sVarX")
        Session("fileIdxY") = Session("fileIdxX")
        Session("bCompY") = Session("bCompX")

        Session("sVarX") = sVartmp
        Session("fileIdxX") = fileIdxtmp
        Session("bCompX") = bCompTmp


        Exit Sub

Hdlr:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    'jrs 2013/05/01
    Private Sub mnuSwapVarsZX_Click()
        On Error GoTo Hdlr
        'Copy source
        Dim pCopy As Object, VARNCopy As Object, gr2Copy As Object,
        gr3Copy As Object, gr4Copy As Object, fCopy As Object,
        FileNumbrCopy As Object, FinYearCopy As Object, FinYearHisCopy As Object,
        bNotDecomposablesCopy As Object, bLineFeedsCopy As Object,
        sGroupTitlesCopy As Object, sUsrDefCatsCopy As Object, DispDecimalsCopy As Object
        Dim FDNG2Copy As Object, FDNG3Copy As Object, FDNG4Copy As Object

        pCopy = VB6.CopyArray(Session("PLOT"))
        VARNCopy = VB6.CopyArray(Session("VARN"))
        gr2Copy = VB6.CopyArray(Session("GR2N"))
        gr3Copy = VB6.CopyArray(Session("GR3N"))
        gr4Copy = VB6.CopyArray(Session("GR4N"))
        fCopy = VB6.CopyArray(Session("aFormulas"))

        FDNG2Copy = VB6.CopyArray(Session("FDNG2"))
        FDNG3Copy = VB6.CopyArray(Session("FDNG3"))
        FDNG4Copy = VB6.CopyArray(Session("FDNG4"))

        FileNumbrCopy = VB6.CopyArray(Session("FileNumbr"))
        FinYearCopy = VB6.CopyArray(Session("FinYear"))
        FinYearHisCopy = VB6.CopyArray(Session("FinYearHis"))
        'Reset Target
        Dim j As Integer, k As Integer, i As Integer
        Dim focus As Integer, DimCount As Integer = 0
        If Session("VarsCount") <> 0 Then
            DimCount = Session("COUNTE") / Session("VarsCount")
        End If
        For i = DimCount + 1 To Session("COUNTE")
            'source index
            If i <= DimCount * 2 Then
                k = i + DimCount
            Else
                k = i - DimCount
            End If
            'copy to target
            For j = 0 To UBound(Session("PLOT"), 1)
                Session("PLOT")(j, i) = pCopy(j, k)
            Next j

            For j = 0 To UBound(Session("VARN"), 1)
                Session("VARN")(j, i) = VARNCopy(j, k)
            Next j
            Session("GR2N")(i) = gr2Copy(k)
            Session("GR3N")(i) = gr3Copy(k)
            Session("GR4N")(i) = gr4Copy(k)
            If k <= UBound(fCopy) Then
                If i > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(i)
                End If
                Session("aFormulas")(i) = fCopy(k)
            End If
            Session("FDNG2")(i) = FDNG2Copy(k)
            Session("FDNG3")(i) = FDNG3Copy(k)
            Session("FDNG4")(i) = FDNG4Copy(k)
            Session("FileNumbr")(i) = FileNumbrCopy(k)
            Session("FinYear")(i) = FinYearCopy(k)
            Session("FinYearHis")(i) = FinYearHisCopy(k)
        Next i

        'save original name to display
        ReDim Session("oVARN0")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oVARN0")(i) = Session("VARN")(0, i)
        Next i
        'jrs 2010/02/04
        ReDim Session("oGR2N")(Session("COUNTE"))
        ReDim Session("oGR3N")(Session("COUNTE"))
        ReDim Session("oGR4N")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oGR2N")(i) = Session("GR2N")(i)
            Session("oGR3N")(i) = Session("GR3N")(i)
            Session("oGR4N")(i) = Session("GR4N")(i)
        Next i

        'jrs 2012/05/30
        Session("RegressType") = ""
        Session("TableTitle") = ""

        'Swap Variables to use when decomposing
        Dim sVartmp As String, fileIdxtmp As Integer, bCompTmp As Boolean
        sVartmp = Session("sVarZ")
        fileIdxtmp = Session("fileIdxZ")
        bCompTmp = Session("bCompZ")

        Session("sVarZ") = Session("sVarX")
        Session("fileIdxZ") = Session("fileIdxX")
        Session("bCompZ") = Session("bCompX")

        Session("sVarX") = sVartmp
        Session("fileIdxX") = fileIdxtmp
        Session("bCompX") = bCompTmp

        cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").Caption = Session("sVarZ")
        Session("sDim2Z") = Session("GR2N")(pCOUNTE + 1)
        Session("sDim3Z") = Session("GR3N")(pCOUNTE + 1)
        If Session("bCompZ") Then
            Session("fZ") = Session("aFormulas")(pCOUNTE + 1)
        End If

        For j = 1 To pCOUNTE / 2
            For i = 1 To Session("FinYear")(1)
                If IsNumeric(Session("PLOT")(i, j + pCOUNTE)) Then
                    Session("BDOTHR")(i, j) = Session("PLOT")(i, j + pCOUNTE)
                Else
                    Session("BDOTHR")(i, j) = -9999
                End If
            Next i
        Next j

        'jrs 2013/09/10
        Dim cswmItem As Coalesys.WebMenu.Item
        If sVartmp = "POP" Then
            cswmItem = cswmWebMenu.Groups("mnuBD").Items.Add()
            cswmItem.Caption = "POP"
            cswmItem.ID = "mnuBDPOP"
            If Session("BDCheck") = "POP" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If
        ElseIf sVartmp = "GDP" Then
            cswmItem = cswmWebMenu.Groups("mnuBD").Items.Add()
            cswmItem.Caption = "GDP"
            cswmItem.ID = "mnuBDGDP"
            If Session("BDCheck") = "POP" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If
        End If

        'Repaint the graph
        mnuBDOther_Click()

        Exit Sub

Hdlr:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    'jrs 2013/05/01
    Private Sub mnuSwapVarsZY_Click()
        On Error GoTo Hdlr
        'Copy source
        Dim pCopy As Object, VARNCopy As Object, gr2Copy As Object,
        gr3Copy As Object, gr4Copy As Object, fCopy As Object,
        FileNumbrCopy As Object, FinYearCopy As Object, FinYearHisCopy As Object,
        bNotDecomposablesCopy As Object, bLineFeedsCopy As Object,
        sGroupTitlesCopy As Object, sUsrDefCatsCopy As Object, DispDecimalsCopy As Object
        Dim FDNG2Copy As Object, FDNG3Copy As Object, FDNG4Copy As Object

        pCopy = VB6.CopyArray(Session("PLOT"))
        VARNCopy = VB6.CopyArray(Session("VARN"))
        gr2Copy = VB6.CopyArray(Session("GR2N"))
        gr3Copy = VB6.CopyArray(Session("GR3N"))
        gr4Copy = VB6.CopyArray(Session("GR4N"))
        fCopy = VB6.CopyArray(Session("aFormulas"))

        FDNG2Copy = VB6.CopyArray(Session("FDNG2"))
        FDNG3Copy = VB6.CopyArray(Session("FDNG3"))
        FDNG4Copy = VB6.CopyArray(Session("FDNG4"))

        FileNumbrCopy = VB6.CopyArray(Session("FileNumbr"))
        FinYearCopy = VB6.CopyArray(Session("FinYear"))
        FinYearHisCopy = VB6.CopyArray(Session("FinYearHis"))
        'Reset Target
        Dim j As Integer, k As Integer, i As Integer
        Dim focus As Integer, DimCount As Integer = 0
        If Session("VarsCount") <> 0 Then
            DimCount = Session("COUNTE") / Session("VarsCount")
        End If
        For i = 1 To Session("COUNTE")
            'source index
            If i <= DimCount Then
                k = i + DimCount * 2
            ElseIf i > DimCount * 2 Then
                k = i - DimCount * 2
            Else
                k = i
            End If
            'copy to target
            For j = 0 To UBound(Session("PLOT"), 1)
                Session("PLOT")(j, i) = pCopy(j, k)
            Next j

            For j = 0 To UBound(Session("VARN"), 1)
                Session("VARN")(j, i) = VARNCopy(j, k)
            Next j
            Session("GR2N")(i) = gr2Copy(k)
            Session("GR3N")(i) = gr3Copy(k)
            Session("GR4N")(i) = gr4Copy(k)
            If k <= UBound(fCopy) Then
                If i > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(i)
                End If
                Session("aFormulas")(i) = fCopy(k)
            End If
            Session("FDNG2")(i) = FDNG2Copy(k)
            Session("FDNG3")(i) = FDNG3Copy(k)
            Session("FDNG4")(i) = FDNG4Copy(k)
            Session("FileNumbr")(i) = FileNumbrCopy(k)
            Session("FinYear")(i) = FinYearCopy(k)
            Session("FinYearHis")(i) = FinYearHisCopy(k)
        Next i

        'save original name to display
        ReDim Session("oVARN0")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oVARN0")(i) = Session("VARN")(0, i)
        Next i
        'jrs 2010/02/04
        ReDim Session("oGR2N")(Session("COUNTE"))
        ReDim Session("oGR3N")(Session("COUNTE"))
        ReDim Session("oGR4N")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oGR2N")(i) = Session("GR2N")(i)
            Session("oGR3N")(i) = Session("GR3N")(i)
            Session("oGR4N")(i) = Session("GR4N")(i)
        Next i

        'jrs 2012/05/30
        Session("RegressType") = ""
        Session("TableTitle") = ""

        'Swap Variables to use when decomposing
        Dim sVartmp As String, fileIdxtmp As Integer, bCompTmp As Boolean
        sVartmp = Session("sVarZ")
        fileIdxtmp = Session("fileIdxZ")
        bCompTmp = Session("bCompZ")

        Session("sVarZ") = Session("sVarY")
        Session("fileIdxZ") = Session("fileIdxY")
        Session("bCompZ") = Session("bCompY")

        Session("sVarY") = sVartmp
        Session("fileIdxY") = fileIdxtmp
        Session("bCompY") = bCompTmp

        cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").Caption = Session("sVarZ")
        Session("sDim2Z") = Session("GR2N")(pCOUNTE + 1)
        Session("sDim3Z") = Session("GR3N")(pCOUNTE + 1)
        If Session("bCompZ") Then
            Session("fZ") = Session("aFormulas")(pCOUNTE + 1)
        End If

        For j = 1 To pCOUNTE / 2
            For i = 1 To Session("FinYear")(1)
                If IsNumeric(Session("PLOT")(i, j + pCOUNTE)) Then
                    Session("BDOTHR")(i, j) = Session("PLOT")(i, j + pCOUNTE)
                Else
                    Session("BDOTHR")(i, j) = -9999
                End If
            Next i
        Next j

        'Repaint the graph
        mnuBDOther_Click()

        Exit Sub

Hdlr:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    'jrs 2012/03/09
    Protected Sub cmdGoToYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGoToYear.Click

        If Not IsNumeric(txtGoToYear.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        Session("PYear") = CInt(txtGoToYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then
            'jrs 2012/05/02
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" And Session("PYear") + Session("FirstYear") - 1 <= Session("LastYearForecast") Then
                Session("DisplayLastYear") = Session("PYear") + Session("FirstYear") - 1
            Else
                Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
            End If
        End If
        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        frm_GoToYear.Visible = False
        PackJS()
        lblJS.Visible = True
        cswmWebMenu.Visible = True
        'jrs 2012/05/30
        Session("RegressType") = ""
        If Session("GType") <> "PIE" Then
            Session("TableTitle") = ""
        End If


    End Sub

    'jrs 2012/03/21
    Protected Sub cmdVLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVLine.Click

        If Not IsNumeric(txtVLine.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtVLine.Text) > Session("LastYearForecast") Or CInt(txtVLine.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtVLine.Text) > Session("LastYearForecast") Or CInt(txtVLine.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        Session("VLYear") = CInt(txtVLine.Text)

        frm_VLine.Visible = False
        PackJS()
        lblJS.Visible = True
        cswmWebMenu.Visible = True
    End Sub

    'jrs 2012/03/09
    Protected Sub cmdHorizon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorizon.Click
        If Not IsNumeric(txtHorizon.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        If Session("Interval") > 1 Then
            'jrs 2016/03/23
            Dim iFirstYear As Integer
            If Session("FirstYear") Mod 5 = 0 Or Session("Interval") Mod 5 <> 0 Then
                iFirstYear = Session("FirstYear")
            Else
                Dim Diff As Integer
                Diff = Session("FirstYear") Mod 5
                iFirstYear = Session("FirstYear") + 5 - Diff
            End If
            If (CInt(txtHorizon.Text) - iFirstYear) Mod Session("Interval") <> 0 Then 'jrs 2016/03/23 session("FirstYear")
                LoadMessage("Invalid Year, year must follow the existing Interval, please use the combo box.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If

        cmb_Horizon.Text = txtHorizon.Text
        cmb_Horizon_SelectedIndexChanged(Me, New EventArgs)
    End Sub

    Private Sub mnuUseGList_Click()
        Session("pCOUNTE") = pCOUNTE

        Session("IsaGroup") = False

        Session("SelectGeographyParent") = "frm_StackedBar.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_SelctGeography.aspx")

    End Sub

    Private Sub mnuUseGroup_Click()
        Session("pCOUNTE") = pCOUNTE

        Session("IsaGroup") = True

        Session("SelectGeographyParent") = "frm_StackedBar.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_SelctGeography.aspx")

    End Sub

    Private Sub getVariablesforDec(ByVal PlotPosition As Integer, ByRef sVarName As String, ByRef fileIdx As Integer, ByRef bUsingComp As Boolean)
        '1. PlotPos, is the column in the original plot vector where the array is stored.
        '2. Variable Name to be retrieved
        '3. fileIdx, index of the file to be used
        '4. bUsingComp is true if using formulas

        'Find variables
        Dim pos As Integer
        pos = InStr(1, Session("VARN")(1, PlotPosition), "[")
        If pos > 0 Then
            sVarName = Left(Session("VARN")(1, PlotPosition), pos - 1)
            fileIdx = Val(Mid(Session("VARN")(1, PlotPosition), pos + 1, Len(Session("VARN")(1, PlotPosition)) - pos - 1))
        Else
            sVarName = Session("VARN")(1, PlotPosition)
            bUsingComp = (UBound(Session("aFormulas")) >= PlotPosition)
            If bUsingComp Then
                bUsingComp = Not (Session("aFormulas")(PlotPosition) Is Nothing)
                If bUsingComp Then
                    If Session("aFormulas")(PlotPosition).Name <> Session("VARN")(1, PlotPosition) Then
                        bUsingComp = False
                    End If
                End If
            End If
            If Session("FirstYear") = FirstYearHistory Then
                fileIdx = -1
            Else
                fileIdx = 0
                'Handling Files with formulas
                If bUsingComp Then
                    pos = InStr(1, Session("aFormulas")(PlotPosition).disVar.displayDet(1), "[")
                    fileIdx = Val(Mid(Session("aFormulas")(PlotPosition).disVar.displayDet(1), pos + 1, Len(Session("aFormulas")(PlotPosition).disVar.displayDet(1)) - pos - 1))
                End If
            End If
        End If
    End Sub

    Private Sub mnuBDPOP_Click()
        If Session("sVarZ") > "" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = "blank.gif"
            'jrs 2013/05/01
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZX").Enabled = False
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZY").Enabled = False
        End If
        If Session("sVarZ") <> "POP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = "checkbox.gif"
        End If
        If Session("sVarZ") <> "GDP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = "blank.gif"
        End If
        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = "blank.gif"

        Session("BDCheck") = "POP"

        Session("COUNTE") = pCOUNTE * 3 / 2
        Session("VarsCount") = 3
        'Load POP
        Dim i As Integer, j As Integer
        For j = 1 To pCOUNTE / 2
            For i = 1 To Session("FinYear")(1)
                If IsNumeric(Session("BDPOP")(i, j)) Then
                    If Session("BDPOP")(i, j) <> -9999 Then
                        Session("PLOT")(i, j + pCOUNTE) = Session("BDPOP")(i, j)
                    Else
                        Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
                    End If
                Else
                    Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
                End If
            Next i
            'jrs 2014/08/04
            If Session("fileIdxZ") < 0 Then
                Session("VARN")(0, j + pCOUNTE) = "POP[Hist]"
            Else
                Session("VARN")(0, j + pCOUNTE) = "POP[" & Session("ShortSce")(Session("fileIdxZ")) & "]"
            End If

            Session("VARN")(1, j + pCOUNTE) = "POP[" & Session("fileIdxZ") & "]"
            Session("GR1N")(j + pCOUNTE) = Session("GR1N")(j)
            Session("GR2N")(j + pCOUNTE) = ""
            Session("GR3N")(j + pCOUNTE) = ""
            Session("GR4N")(j + pCOUNTE) = "Mil People"
            Session("FinYear")(j + pCOUNTE) = Session("FinYear")(j)
        Next j

        'Save Old order
        Dim OldGR1N As Object
        OldGR1N = VB6.CopyArray(Session("GR1N"))
        'Sort by bubble size (descending)
        SortByBubbleSize(CInt(Session("FinYear")(1)), Session("COUNTE"), Session("PLOT"), Session("GR1N"),
        Session("VARN"), Session("aFormulas"), Session("FDNG1"), Session("FileNumbr"), Session("FinYear"),
        Session("FinYearHis")) 'jrs 2010/11/10 FirstYearNotNull
        'Create Old to New Map
        Dim Map() As Integer
        ReDim Map(pCOUNTE / 2)
        CreateMapBDOldtoNew(OldGR1N, Session("GR1N"), Map)
        'Reorder POP
        ReOrderBD(Session("BDPOP"), Map)
        'Reorder GDP
        If Session("sVarZ") <> "GDP" Then
            ReOrderBD(Session("BDGDP"), Map)
        End If
        'Reorder Other
        If Session("sVarZ") > "" Then
            ReOrderBD(Session("BDOTHR"), Map)
        End If
        'Reorder Color
        ReOrderColor(Session("RColor"), Map)
        ReOrderColor(Session("GColor"), Map)
        ReOrderColor(Session("BColor"), Map)

        Session("bBubble") = True

        'save original name to display
        ReDim Session("oVARN0")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oVARN0")(i) = Session("VARN")(0, i)
        Next i
        'jrs 2010/02/04
        ReDim Session("oGR1N")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oGR1N")(i) = Session("GR1N")(i)
        Next i

        'jrs 2010/10/13
        'SetUpManualScaleToMinMaxZ()

    End Sub

    Private Sub mnuBDGDP_Click()
        If Session("sVarZ") > "" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = "blank.gif"
            'jrs 2013/05/01
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZX").Enabled = False
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZY").Enabled = False
        End If
        If Session("sVarZ") <> "POP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = "blank.gif"
        End If
        If Session("sVarZ") <> "GDP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = "checkbox.gif"
        End If
        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = "blank.gif"

        Session("BDCheck") = "GDP"

        Session("COUNTE") = pCOUNTE * 3 / 2
        Session("VarsCount") = 3
        'Load POP
        Dim i As Integer, j As Integer
        For j = 1 To pCOUNTE / 2
            For i = 1 To Session("FinYear")(1)
                If IsNumeric(Session("BDGDP")(i, j)) Then
                    If Session("BDGDP")(i, j) <> -9999 Then
                        Session("PLOT")(i, j + pCOUNTE) = Session("BDGDP")(i, j)
                    Else
                        Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
                    End If
                Else
                    Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
                End If
            Next i
            Session("VARN")(0, j + pCOUNTE) = "GDP[" & Session("ShortSce")(Session("fileIdxZ")) & "]"
            Session("VARN")(1, j + pCOUNTE) = "GDP[" & Session("fileIdxZ") & "]"
            Session("GR1N")(j + pCOUNTE) = Session("GR1N")(j)
            Session("GR2N")(j + pCOUNTE) = ""
            Session("GR3N")(j + pCOUNTE) = ""
            Session("GR4N")(j + pCOUNTE) = "Billion $"
            Session("FinYear")(j + pCOUNTE) = Session("FinYear")(j)
        Next j

        'Save Old order
        Dim OldGR1N As Object
        OldGR1N = VB6.CopyArray(Session("GR1N"))
        'Sort by bubble size (descending)
        SortByBubbleSize(CInt(Session("FinYear")(1)), Session("COUNTE"), Session("PLOT"), Session("GR1N"),
        Session("VARN"), Session("aFormulas"), Session("FDNG1"), Session("FileNumbr"), Session("FinYear"),
        Session("FinYearHis")) 'jrs 2010/11/10 FirstYearNotNull
        'Create Old to New Map
        Dim Map() As Integer
        ReDim Map(pCOUNTE / 2)
        CreateMapBDOldtoNew(OldGR1N, Session("GR1N"), Map)
        'Reorder GDP
        ReOrderBD(Session("BDGDP"), Map)
        'Reorder POP
        If Session("sVarZ") <> "POP" Then
            ReOrderBD(Session("BDPOP"), Map)
        End If
        'Reorder Other
        If Session("sVarZ") > "" Then
            ReOrderBD(Session("BDOTHR"), Map)
        End If
        'Reorder Color
        ReOrderColor(Session("RColor"), Map)
        ReOrderColor(Session("GColor"), Map)
        ReOrderColor(Session("BColor"), Map)

        Session("bBubble") = True

        'save original name to display
        ReDim Session("oVARN0")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oVARN0")(i) = Session("VARN")(0, i)
        Next i
        'jrs 2010/02/04
        ReDim Session("oGR1N")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oGR1N")(i) = Session("GR1N")(i)
        Next i

        'jrs 2010/10/13
        'SetUpManualScaleToMinMaxZ()

    End Sub

    Private Sub mnuBDOther_Click()
        cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = "checkbox.gif"
        If Session("sVarZ") <> "POP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = "blank.gif"
        End If
        If Session("sVarZ") <> "GDP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = "blank.gif"
        End If
        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = "blank.gif"
        'jrs 2013/05/01
        cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZX").Enabled = True
        cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZY").Enabled = True

        Session("BDCheck") = "Other"

        Session("COUNTE") = pCOUNTE * 3 / 2
        Session("VarsCount") = 3
        'Load POP
        Dim i As Integer, j As Integer
        For j = 1 To pCOUNTE / 2
            For i = 1 To Session("FinYear")(1)
                If IsNumeric(Session("BDOTHR")(i, j)) Then
                    If Session("BDOTHR")(i, j) <> -9999 Then
                        Session("PLOT")(i, j + pCOUNTE) = Session("BDOTHR")(i, j)
                    Else
                        Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
                    End If
                Else
                    Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
                End If
            Next i
            Session("VARN")(0, j + pCOUNTE) = Session("sVarz") & "[" & Session("ShortSce")(Session("fileIdxZ")) & "]"
            Session("VARN")(1, j + pCOUNTE) = Session("sVarz") & "[" & Session("fileIdxZ") & "]"
            Session("GR1N")(j + pCOUNTE) = Session("GR1N")(j)
            Session("GR2N")(j + pCOUNTE) = ""
            Session("GR3N")(j + pCOUNTE) = ""
            If IfsVars.Item(Session("sVarZ")).Index > 0 Then
                Session("GR4N")(j + pCOUNTE) = VUnits(IfsVars.Item(Session("sVarZ")).Index)
            Else
                Session("GR4N")(j + pCOUNTE) = ""
            End If
            Session("FinYear")(j + pCOUNTE) = Session("FinYear")(j)
        Next j

        'Save Old order
        Dim OldGR1N As Object
        OldGR1N = VB6.CopyArray(Session("GR1N"))
        'Sort by bubble size (descending)
        SortByBubbleSize(CInt(Session("FinYear")(1)), Session("COUNTE"), Session("PLOT"), Session("GR1N"),
        Session("VARN"), Session("aFormulas"), Session("FDNG1"), Session("FileNumbr"), Session("FinYear"),
        Session("FinYearHis")) 'jrs 2010/11/10 FirstYearNotNull
        'Create Old to New Map
        Dim Map() As Integer
        ReDim Map(pCOUNTE / 2)
        CreateMapBDOldtoNew(OldGR1N, Session("GR1N"), Map)
        'Reorder GDP
        ReOrderBD(Session("BDOTHR"), Map)
        'Reorder POP
        If Session("sVarZ") <> "POP" Then
            ReOrderBD(Session("BDPOP"), Map)
        End If
        'Reorder Other
        If Session("sVarZ") <> "GDP" Then
            ReOrderBD(Session("BDGDP"), Map)
        End If
        'Reorder Color
        ReOrderColor(Session("RColor"), Map)
        ReOrderColor(Session("GColor"), Map)
        ReOrderColor(Session("BColor"), Map)

        Session("bBubble") = True

        'save original name to display
        ReDim Session("oVARN0")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oVARN0")(i) = Session("VARN")(0, i)
        Next i
        'jrs 2010/02/04
        ReDim Session("oGR1N")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oGR1N")(i) = Session("GR1N")(i)
        Next i

        'jrs 2010/10/13
        'SetUpManualScaleToMinMaxZ()

    End Sub

    Private Sub mnuBDNone_Click()
        If Session("sVarZ") > "" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = "blank.gif"
            'jrs 2013/05/01
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZX").Enabled = False
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZY").Enabled = False
        End If
        If Session("sVarZ") <> "POP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = "blank.gif"
        End If
        If Session("sVarZ") <> "GDP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = "blank.gif"
        End If
        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = "checkbox.gif"

        Session("BDCheck") = "None"

        Session("COUNTE") = pCOUNTE
        Session("VarsCount") = 2
        'Load POP
        Dim i As Integer, j As Integer
        For j = 1 To pCOUNTE / 2
            For i = 1 To Session("FinYear")(1)
                Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
            Next i
            Session("VARN")(0, j + pCOUNTE) = ""
            Session("VARN")(1, j + pCOUNTE) = ""
            Session("GR1N")(j + pCOUNTE) = ""
            Session("GR2N")(j + pCOUNTE) = ""
            Session("GR3N")(j + pCOUNTE) = ""
            Session("GR4N")(j + pCOUNTE) = ""
            Session("FinYear")(j + pCOUNTE) = Nothing
        Next j

        'Create Old to New Map
        Dim Map() As Integer
        ReDim Map(pCOUNTE / 2)
        CreateMapBDOldtoNew(Session("GR1N"), Session("NoneOrder"), Map)
        'Reorder POP
        ReOrderBD(Session("BDPOP"), Map)
        'Reorder GDP
        If Session("sVarZ") <> "GDP" Then
            ReOrderBD(Session("BDGDP"), Map)
        End If
        'Reorder Other
        If Session("sVarZ") > "" Then
            ReOrderBD(Session("BDOTHR"), Map)
        End If
        'Reorder Color
        ReOrderColor(Session("RColor"), Map)
        ReOrderColor(Session("GColor"), Map)
        ReOrderColor(Session("BColor"), Map)

        Session("bBubble") = False

        'save original name to display
        ReDim Session("oVARN0")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oVARN0")(i) = Session("VARN")(0, i)
        Next i
        'jrs 2010/02/04
        ReDim Session("oGR1N")(Session("COUNTE"))
        For i = 1 To Session("COUNTE")
            Session("oGR1N")(i) = Session("GR1N")(i)
        Next i

        'jrs 2010/10/13
        'SetUpManualScaleToMinMaxZ()

    End Sub

    Private Sub LoadBD(ByVal sVar As String, ByVal Target As Object)
        'Load sVar for all Members of GR1N
        Dim i As Integer
        'jrs 2012/07/23 changed from GR1N to oGR1N
        For i = 1 To pCOUNTE / 2
            LoadBDMemberVals(Target, i, Session("oGR1N")(i), sVar, Session("fileIdxZ"))
        Next i

    End Sub

    Private Sub LoadBDMemberVals(ByRef TargetVector As Object, ByVal TargetCol As Integer, ByVal sDim1 As String, ByVal sVar As String,
    ByVal fileIdx As Integer)
        '1. TargetVector, is vector to store results
        '2. TargetCol, indicates where in the 2-dimensioned array to store the country
        '3. sDim1, is the name of the country
        '4. sVar, name of the variable
        '5. fileIdx index of the file to be used

        Dim bHistFor As Boolean
        bHistFor = (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory)

        'jrs 2011/04/07 when using historic mode,
        'better to load forecast variables for Bubble Diameter
        '    If UCase(ActiveBase) = "IFSBASEHIST.RUN" Then
        '        gLoadForDisplayHis sVar, sDim1, -1, "", -1, "", -1, _
        '        True, True, Null, 1, TargetVector, TargetCol, FirstYearHistory, LastYearHistory, _
        '        False, False, False, "", bFillHis, buseAllHis, -1, bFillHisOnlyHoles
        '    Else
        Dim iRowPosition As Integer, iFirstYear As Integer
        If bHistFor Then
            iRowPosition = LastYearHistory - FirstYearHistory + 1
            iFirstYear = FirstYearForecast
        Else
            iRowPosition = 1
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then 'JRS 2006/05/22
                iFirstYear = FirstYearForecast
            Else
                iFirstYear = FirstYearHistory
            End If
        End If
        'jrs 2011/12/16
        If fileIdx < 0 Then
            fileIdx = 1
        End If

        gLoadForDisplayNET(sVar, -1, sDim1, -1, "", -1, "", -1, "", fileIdx, True, True, Nothing, 1, TargetVector, TargetCol,
        iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))
        'gLoadForDisplay(sVar, -1, sDim1, -1, "", -1, "", -1, "", fileIdx, True, True, Nothing, 1, TargetVector, TargetCol,
        'iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
        'Session("IFs_MDB2Active"), Session("CurrencyVal"))
        '    End If

        If bHistFor Then
            Dim Fin As Integer
            Dim IfsPars As New myParameters
            IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

            Dim sDef As String, sUn As String
            Fin = Session("FinYear")(TargetCol)
            gLoadForDisplayHis(sVar, sDim1, -1, "", -1, "", -1,
            Nothing, 1, TargetVector, TargetCol, FirstYearHistory, LastYearHistory - 1,
            False, True, Session("bFillHis"), Session("buseAllHis"), -1,
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
            Session("FinYear")(TargetCol) = LastYearHistory - FirstYearHistory + Fin
        End If

    End Sub

    Private Sub CreateMapBDOldtoNew(ByVal OldGR1 As Object, ByVal NewGR1 As Object, ByVal Target As Object)
        Dim j As Integer, k As Integer
        For j = 1 To pCOUNTE / 2
            For k = 1 To pCOUNTE / 2
                If NewGR1(j) = OldGR1(k) Then
                    Exit For
                End If
            Next k
            If k <= pCOUNTE / 2 Then 'jrs 2012/07/23 added if statement and else branch
                Target(j) = k
            Else
                Target(j) = 0
            End If
        Next j
    End Sub

    Private Sub ReOrderBD(ByVal Target As Object, ByVal Map As Object)
        Dim tmp As Object
        tmp = VB6.CopyArray(Target)
        Dim i As Integer, j As Integer
        For j = 1 To pCOUNTE / 2
            For i = 1 To Session("FinYear")(1)
                Target(i, j) = tmp(i, Map(j))
            Next i
        Next j
    End Sub

    'Only the first 10 will be constant
    Private Sub ReOrderColor(ByVal Target As Object, ByVal Map As Object)
        Dim tmp As Object
        tmp = VB6.CopyArray(Target)
        Dim i As Integer
        For i = 1 To pCOUNTE / 2
            Target(i - 1) = tmp(Map(i) - 1)
        Next i
    End Sub

    'jrs 2010/09/29
    Private Function FindFirstYearNotNull() As Integer
        Dim i As Integer, j As Integer
        FindFirstYearNotNull = 1 '-1 if not found better to set it to something valid
        For i = 1 To Session("FinYear")(Session("COUNTE"))
            For j = 1 To pCOUNTE / 2
                If IsNumeric(Session("PLOT")(i, j)) And IsNumeric(Session("PLOT")(i, pCOUNTE / 2 + j)) Then
                    FindFirstYearNotNull = i
                    Exit Function
                End If
            Next j
        Next i
    End Function

    Private Sub mnuLinear_Click()
        If Session("GType") = "SCAT" Then
            ''jrs 2011/02/04
            'If mnuTracingOn.Checked Then
            '    mnuTracingOff_Click()
            'End If

            Session("ChangeSatPoint") = False
            'jrs 2011/02/04 SaturationValue = 100

            Session("RegressType") = "LIN"
            RegressionPLOT(1, "NONE")

            Session("Transformation") = "LIN"
            Session("mnuLogistic") = False
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = "blank.gif"
        End If
    End Sub

    Private Sub mnuLog_Click()
        If Session("GType") = "SCAT" Then
            ''jrs 2011/02/04
            'If mnuTracingOn.Checked Then
            '    mnuTracingOff_Click()
            'End If

            Session("ChangeSatPoint") = False
            'jrs 2011/02/04 SaturationValue = 100

            Session("RegressType") = "LOG"
            RegressionPLOT(1, "LOG")

            Session("Transformation") = "LOG"
            Session("mnuLogistic") = False
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = "blank.gif"
        End If
    End Sub

    Private Sub mnuExpon_Click()

        If Session("GType") = "SCAT" Then
            ''jrs 2011/02/04
            'If mnuTracingOn.Checked Then
            '    mnuTracingOff_Click()
            'End If

            Session("ChangeSatPoint") = False
            'jrs 2011/02/04 SaturationValue = 100

            Dim ii As Integer
            For ii = 1 To pCOUNTE
                If IsNumeric(Session("PLOT")(Session("PYear"), ii%)) Then
                    If Session("PLOT")(Session("PYear"), ii%) > 709 Then
                        Dim Msg As String
                        Msg = "Please Re-scale First Independent Variable, It exceeds the max value(709) for an exponent formulation"
                        LoadMessage(Msg, Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If
                End If
            Next ii

            Session("RegressType") = "EXP"
            RegressionPLOT(1, "EXP")

            Session("Transformation") = "EXP"
            Session("mnuLogistic") = False
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = "blank.gif"
        End If
    End Sub

    Private Sub mnuPower_Click()

        If Session("GType") = "SCAT" Then
            'jrs 2011/02/04
            'If mnuTracingOn.Checked Then
            '    mnuTracingOff_Click()
            'End If

            Session("ChangeSatPoint") = False
            'jrs 2011/02/04 SaturationValue = 100


            'PowerDegree$ = InputBox("For Formulation (aX^b), Enter a value for b. You  can use Excel to determine b", , 1)
            'If PowerDegree$ = "" Then
            '    PowerDegree$ = 1
            'End If

            If Session("PowerDegree") Is Nothing Then
                Session("PowerDegree") = 1
            End If

            Session("RegressType") = "POW"
            Session("Transformation") = "POW"
            'PubPowerDegree = PowerDegree$

            'RegressionPLOT(1, "POW")

            Session("mnuLogistic") = False
            Session("ScatterPower") = "frm_StackedBar.aspx"
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_ScatterPower.aspx")
        End If
    End Sub

    Private Sub mnuSecondDeg_Click()

        If Session("GType") = "SCAT" Then
            'jrs 2011/02/04
            'If mnuTracingOn.Checked Then
            '    mnuTracingOff_Click()
            'End If


            Session("ChangeSatPoint") = False
            'jrs 2011/02/04 SaturationValue = 100


            Session("RegressType") = "POLYNOMIAL(Second Degree)"

            Session("Transformation") = "SEC"

            RegressionPLOT(2, "NONE")

            Session("mnuLogistic") = False
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = "blank.gif"
        End If
    End Sub

    Private Sub mnuThirdDeg_Click()
        If Session("GType") = "SCAT" Then
            'jrs 2011/02/04
            'If mnuTracingOn.Checked Then
            '    mnuTracingOff_Click()
            'End If

            Session("ChangeSatPoint") = False
            'jrs 2011/02/04 SaturationValue = 100


            Session("RegressType") = "POLYNOMIAL(Third Degree)"
            RegressionPLOT(3, "NONE")

            Session("Transformation") = "THI"

            Session("mnuLogistic") = False
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = "blank.gif"
        End If
    End Sub

    Private Sub mnuLogistic_Click()
        'JAM 2009-11-08    Parameter for Saturation Point
        If Session("GType") = "SCAT" Then
            'jrs 2011/02/04
            'If mnuTracingOn.Checked Then
            '    mnuTracingOff_Click()
            'End If

            Session("ChangeSatPoint") = False
            'Session("SaturationValue") = 100
            If Session("SaturationValue") Is Nothing Then
                Session("SaturationValue") = 100
            End If

            Session("RegressType") = "LIN"

            Session("ScatterLogistic") = "frm_StackedBar.aspx"
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_SaturParam.aspx")
        End If
    End Sub

    Private Sub mnuNoRegress_Click()
        Session("RegressType") = ""
        Session("TableTitle") = ""
        Session("mnuLogistic") = False
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = "blank.gif"
    End Sub

    Public Sub RegressionPLOT(ByVal Degree As Integer, ByVal Transform As String)

        On Error GoTo Forecasterr
        Dim SaturatePoint As Double
        Dim x#, y#
        Dim a1#, B1#, a#, b#
        Dim Equation As String
        Dim RegResult As Double
        Dim i2 As Integer
        Dim Formula As String
        Dim CoefB1, CoefB2, CoefB3, CoefB4
        Dim ThirdCoef, SecondCoef, FourthCoef, sign, sign2, sign3, sign4
        Dim B1Format, B2Format, B3Format, B4Format, RFormat
        Dim RegressFormul, RsqrFormul

        Dim MinVI As Single, MaxVI As Single
        MinVI = 1000000000
        MaxVI = -1000000000

        Dim II As Integer
        If Not Session("ChangeSatPoint") Then

            For II% = 1 To pCOUNTE / 2
                If IsNumeric(Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2))) Then
                    If MinVI > Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2)) And IsNumeric(Session("PLOT")(Session("PYear"), II%)) Then MinVI = Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2))
                    If MaxVI < Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2)) And IsNumeric(Session("PLOT")(Session("PYear"), II%)) Then MaxVI = Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2))
                End If
            Next II%

        Else
            For II% = 1 To pCOUNTE / 2
                If IsNumeric(Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2))) Then
                    'JAM 2009-11-08     Saturation Point must be changed
                    If MinVI > Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2)) Then MinVI = Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2))
                    If MaxVI < Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2)) Then MaxVI = Session("PLOT")(Session("PYear"), II% + (pCOUNTE / 2))
                End If
            Next II%
        End If

        '  CCount = TotCountries


        With reg
            .Degree = Degree

            For i2 = 1 To pCOUNTE / 2
                If IsNumeric(Session("PLOT")(Session("PYear"), i2 + (pCOUNTE / 2))) And IsNumeric(Session("PLOT")(Session("PYear"), i2)) Then

                    Select Case Transform
                        Case "LOG"
                            x# = CDbl(Math.Log(Session("PLOT")(Session("PYear"), i2 + (pCOUNTE / 2))))
                        Case "EXP"
                            x# = CDbl(Math.Exp(Session("PLOT")(Session("PYear"), i2 + (pCOUNTE / 2))))
                        Case "POW"
                            x# = CDbl((Session("PLOT")(Session("PYear"), i2 + (pCOUNTE / 2)) ^ (Session("PowerDegree"))))
                        Case Else
                            x# = CDbl(Session("PLOT")(Session("PYear"), i2 + (pCOUNTE / 2)))
                    End Select

                    y# = CDbl(Session("PLOT")(Session("PYear"), i2))  '1

                    .XYAdd(x#, y#)

                End If

            Next i2

        End With

        'CCount = TotCountries
        ReDim Session("RegData")(2, pCOUNTE / 2)

        For II% = 1 To pCOUNTE / 2

            Session("RegData")(1, II% - 1) = MinVI + ((MaxVI - MinVI) * ((II% - 1) / ((pCOUNTE / 2) - 1)))

            If Transform = "LOG" Then
                RegResult = reg.RegVal(CDbl(Math.Log(Session("RegData")(1, II% - 1))))
            Else
                If Transform = "EXP" Then
                    RegResult = reg.RegVal(CDbl(Math.Exp(Session("RegData")(1, II% - 1))))
                Else
                    If Transform = "POW" Then
                        RegResult = reg.RegVal(CDbl((Session("RegData")(1, II% - 1)) ^ (Session("PowerDegree"))))
                    Else
                        If Not Session("ChangeSatPoint") Then
                            RegResult = reg.RegVal(CDbl(Session("RegData")(1, II% - 1)))
                        Else
                            RegResult = Session("SaturationValue") / (1 + Math.Exp(CDbl(reg.Coeff(0) + reg.Coeff(1) * Session("RegData")(1, II% - 1))))
                        End If
                    End If
                End If
            End If


            If Math.Abs(RegResult) >= 1.401298E-45 And Math.Abs(RegResult) <= 3.402823E+38 Then
                Session("RegData")(2, II% - 1) = RegResult
                a = Session("RegData")(1, II% - 1)
            Else
                Session("RegData")(2, II% - 1) = Nothing
            End If


        Next II%



        Select Case Transform
            Case "LINEAR"
                Formula = "("
            Case "LOG"
                Formula = "ln("
            Case "EXP"
                Formula = "Exp("
            Case "POW"
                Formula = "^"
            Case Else
                Formula = "("
        End Select






        CoefB1 = reg.Coeff(0)  'Y=a + bt
        CoefB2 = reg.Coeff(1)
        CoefB3 = -9999
        CoefB4 = -9999

        If Degree > 1 Then

            CoefB3 = reg.Coeff(2)
            ThirdCoef = Math.Abs(CoefB3)
            If CoefB3 < 0 Then
                sign2 = " - "

            Else
                sign2 = " + "
            End If

            If Degree > 2 Then
                CoefB4 = reg.Coeff(3)
                FourthCoef = Math.Abs(CoefB4)
                If CoefB4 < 0 Then
                    sign3 = " - "
                Else
                    sign3 = " + "
                End If

            End If

        End If

        SecondCoef = Math.Abs(CoefB2)
        If CoefB2 < 0 Then
            sign = " - "

        Else
            sign = " + "
        End If


        CalcRSQR(Transform)

        If Math.Abs(CoefB1) < 0.01 Or Math.Abs(CoefB1) > 1000000 Then
            B1Format = "0.00E+00"
        Else
            B1Format = "0.0000"
        End If

        If SecondCoef < 0.01 Or SecondCoef > 1000000 Then
            B2Format = "0.00E+00"
        Else
            B2Format = "0.0000"
        End If

        If ThirdCoef < 0.01 Or ThirdCoef > 1000000 Then
            B3Format = "0.00E+00"
        Else
            B3Format = "0.0000"
        End If

        If FourthCoef < 0.01 Or FourthCoef > 1000000 Then
            B4Format = "0.00E+00"
        Else
            B4Format = "0.0000"
        End If

        If Math.Abs(RSQR) < 0.01 Or Math.Abs(RSQR) > 1000000 Then
            RFormat = "0.00E+00"
        Else
            RFormat = "0.0000"
        End If


        If Transform = "NONE" Or Transform = "LOG" Then
            Equation = "y= " + Format(CoefB1, B1Format) + sign + Format(SecondCoef, B2Format) + "*" + Formula + "x)"
            'JAM 2009-08-22 If We've choose Saturation Option
            If Session("ChangeSatPoint") Then
                If sign = " - " Then
                    Equation = "y= " & Format(Val(Session("SaturationValue")), "0.00") & "/(1 + e^(" & Format(CoefB1, B1Format) & sign & Format(SecondCoef, B2Format) & "*" & Formula & "x)))"
                Else
                    Equation = "y= " & Format(Val(Session("SaturationValue")), "0.00") & "/(1 + e^(" & Format(CoefB1, B1Format) & "+" & Format(SecondCoef, B2Format) & "*" & Formula & "x)))"
                End If
            End If
        Else
            If Transform = "EXP" Then
                Equation = "y= " & Format(CoefB1, B1Format) & sign & Format(SecondCoef, B2Format) & "*e^(" & "x)" 'sign + Format(SecondCoef, B2Format) + "*x)"
            Else
                If Transform = "POW" Then
                    Equation = "y= " & Format(CoefB1, B1Format) & sign & Format(SecondCoef, B2Format) & "*x^(" & Session("PowerDegree") & ")"
                Else
                    Equation = "y= " & Format(CoefB1, B1Format) & sign & Format(SecondCoef, B2Format) & "*" & Formula & "x)"
                End If
            End If
        End If

        If CoefB3 <> -9999 Then
            Equation = Equation & sign2 & Format(ThirdCoef, B3Format) & "*" & Formula & "x^2)"
        End If

        If CoefB4 <> -9999 Then
            Equation = Equation & sign3 & Format(FourthCoef, B4Format) & "*" & Formula & "x^3)"
        End If


        Session("TableTitle") = Equation & "<br/>R(SQR) = " & Format(RSQR, RFormat)
        RegressFormul = Equation
        RsqrFormul = Format(RSQR, RFormat)



        Exit Sub

Forecasterr:
        'MsgBox("Error occurred while calculating " + ExtpType$ + " trend.")


    End Sub

    Private Sub CalcRSQR(ByVal vTransform As String)

        Dim YBAR#, SUMOFYSQR#, SQROFYSUM#
        Dim SSE#, SST#
        Dim ObsNo%


        Dim ForeTot As Single, ForeMean As Single
        ForeTot = 0
        ForeMean = 0

        SSE = 0
        SST = 0
        YBAR = 0
        SUMOFYSQR = 0
        ObsNo = 0

        Dim rr As Integer, XReal As Single
        For rr% = 1 To pCOUNTE / 2
            If IsNumeric(Session("PLOT")(Session("PYear"), rr% + (pCOUNTE / 2))) And IsNumeric(Session("PLOT")(Session("PYear"), rr%)) Then
                Select Case vTransform
                    Case "LINEAR"
                        XReal = Session("PLOT")(Session("PYear"), rr% + (pCOUNTE / 2))
                    Case "LOG"
                        XReal = Math.Log(Session("PLOT")(Session("PYear"), rr% + (pCOUNTE / 2)))
                    Case "EXP"
                        XReal = Math.Exp(Session("PLOT")(Session("PYear"), rr% + (pCOUNTE / 2)))
                    Case "POW"
                        XReal = ((Session("PLOT")(Session("PYear"), rr% + (pCOUNTE / 2))) ^ (Session("PowerDegree")))
                    Case Else
                        XReal = Session("PLOT")(Session("PYear"), rr% + (pCOUNTE / 2))
                End Select

                SSE = SSE + (Session("PLOT")(Session("PYear"), rr%) - (reg.RegVal(CDbl(XReal)))) ^ 2
                YBAR = YBAR + Session("PLOT")(Session("PYear"), rr%)
                SUMOFYSQR = SUMOFYSQR + Session("PLOT")(Session("PYear"), rr%) ^ 2
                ObsNo = ObsNo + 1
            End If

        Next rr%
        SQROFYSUM = YBAR ^ 2
        YBAR = YBAR / ObsNo


        For rr% = 1 To pCOUNTE / 2
            If IsNumeric(Session("PLOT")(Session("PYear"), rr%)) And IsNumeric(Session("PLOT")(Session("PYear"), rr% + (pCOUNTE / 2))) Then
                SST = SST + (Session("PLOT")(Session("PYear"), rr%) - YBAR) ^ 2
            End If
        Next rr%

        RSQR = 1 - (SSE / SST)
    End Sub

    Private Sub TransSatDep()
        Dim i, limit As Integer

        If Session("SaturCVarD") Is Nothing Then
            ReDim Session("SaturCVarD")(pCOUNTE)
        End If

        limit = pCOUNTE / 2
        For i = 1 To limit
            Session("SaturCVarD")(i) = Session("PLOT")(Session("PYear"), i)
            If IsNumeric(Session("PLOT")(Session("PYear"), i)) Then
                If ((Session("SaturationValue") / Session("PLOT")(Session("PYear"), i)) - 1) > 0 And Session("PLOT")(Session("PYear"), i) <> 0 Then
                    Session("PLOT")(Session("PYear"), i) = Math.Log(((Session("SaturationValue") / Session("PLOT")(Session("PYear"), i))) - 1)
                Else
                    Session("PLOT")(Session("PYear"), i) = System.DBNull.Value
                End If
            Else
                Session("PLOT")(Session("PYear"), i) = System.DBNull.Value
            End If
        Next i
    End Sub

    Private Sub RevokeSatDep()
        Dim i, limit As Integer

        limit = pCOUNTE / 2
        For i = 1 To limit
            Session("PLOT")(Session("PYear"), i) = Session("SaturCVarD")(i)
        Next i

    End Sub

    Private Sub mnuStatistics_Click()
        'jrs 2011/12/15
        'Adding Analysis Across Time
        Dim iDimCount As Integer, iVarsCount As Integer

        If Session("mnuUseAllYrs") Or Session("mnuUseAllYrsTime") Then 'JAM 2016-02-07 Adding AllYears Exception
            iDimCount = Session("Dim1Count") * (Session("LastYear") - Session("FirstYear") + 1)
            If Session("mnuUseAllYrs") Then
                iVarsCount = Session("VarsCount")
            Else 'JAM 2016-02-07 It's Session("mnuUseAllYrsTime")
                iVarsCount = Session("VarsCount") + 1
            End If
        Else
            iDimCount = Session("Dim1Count")
            iVarsCount = Session("VarsCount")
        End If




        'jrs 2011/04/06
        'Statistics_Type = 0
        'frm_statistics.Show
        Dim RegVarD() As Object, RegVarI(0, 0) As Object, sVarI() As String
        ReDim RegVarD(iDimCount)
        Dim i As Integer, j As Integer, k As Integer
        'jrs 2011/12/15
        If Session("mnuUseAllYrs") Or Session("mnuUseAllYrsTime") Then
            For i = 1 To Session("Dim1Count")
                For j = 1 To Session("LastYear") - Session("FirstYear") + 1
                    RegVarD(i + (j - 1) * Session("Dim1Count")) = Session("PLOT")(j, i)
                Next j
            Next i
        Else
            For i = 1 To Session("Dim1Count")
                RegVarD(i) = Session("PLOT")(Session("PYear"), i)
            Next i
        End If

        ReDim RegVarI(iDimCount, iVarsCount - 1)
        ReDim sVarI(iVarsCount - 1)

        For j = 1 To iVarsCount - 1
            'jrs 2011/12/15
            If j = iVarsCount - 1 And Session("mnuUseAllYrsTime") Then
                sVarI(j - 1) = "Year(YYYY)"
            Else
                sVarI(j - 1) = Session("VARN")(1, Session("Dim1Count") * j + 1)
                If Session("GR2N")(Session("Dim1Count") * j + 1) > "" Then
                    sVarI(j - 1) = sVarI(j - 1) & "," & Session("GR2N")(Session("Dim1Count") * j + 1)
                End If
            End If

            'jrs 2011/12/15
            If Session("mnuUseAllYrs") Or Session("mnuUseAllYrsTime") Then
                If j = iVarsCount - 1 And Session("mnuUseAllYrsTime") Then
                    For i = 1 To Session("Dim1Count")
                        For k = 1 To Session("LastYear") - Session("FirstYear") + 1
                            RegVarI(i + (k - 1) * Session("Dim1Count"), j - 1) = Session("FirstYear") + k - 1
                        Next k
                    Next i
                Else
                    For i = 1 To Session("Dim1Count")
                        For k = 1 To Session("LastYear") - Session("FirstYear") + 1
                            RegVarI(i + (k - 1) * Session("Dim1Count"), j - 1) = Session("PLOT")(k, Session("Dim1Count") * j + i)
                        Next k
                    Next i
                End If
            Else
                sVarI(j - 1) = sVarI(j - 1) & "(" & CStr(Session("FirstYear") + Session("PYear") - 1) & ")"

                For i = 1 To Session("Dim1Count")
                    RegVarI(i, j - 1) = Session("PLOT")(Session("PYear"), iDimCount * j + i)
                Next i
            End If
        Next j

        Dim sVarD As String
        sVarD = Session("VARN")(1, 1)
        If Session("GR2N")(1) > "" Then
            sVarD = sVarD & "," & Session("GR2N")(1)
        End If
        'jrs 2011/12/15
        If Not Session("mnuUseAllYrs") And Not Session("mnuUseAllYrsTime") Then
            sVarD = sVarD & "(" & CStr(Session("FirstYear") + Session("PYear") - 1) & ")"
        End If

        Session("Transformation") = ""
        Session("Statistics_Type") = 0

        SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
        Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
        Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
        Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
        Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
        Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
        Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))

        PrepareTransformForStatistics(iDimCount, iVarsCount - 1, RegVarD, RegVarI, sVarD, sVarI,
        Session("IFsStat_MDB"), Session("Total_Records"), Session("Total_Var"))

        '   Load the transformation form
        '   ----------------------------

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "Transform.aspx")

    End Sub

    'jrs 2015/09/08
    Private Function getXAxisLabel(ByVal i As Integer) As String
        If Session("XAxisLabel") > "" Then
            getXAxisLabel = Session("XAxisLabel")
        ElseIf Session("GType") = "SCAT" Then
            getXAxisLabel = Session("VARN")(1, i)
        ElseIf Session("GType") = "LORENZ" Then
            getXAxisLabel = "Share of Global Population"
        Else
            getXAxisLabel = "Year"
        End If
    End Function

    'jrs 2015/09/08
    Private Function getYAxisLabel() As String
        If Session("YAxisLabel") <> "" Then
            getYAxisLabel = Session("YAxisLabel")
            Exit Function
        ElseIf Session("GType") = "SCAT" Then
            getYAxisLabel = Session("VARN")(1, 1)
            Exit Function
        ElseIf Session("GType") = "LORENZ" Then
            getYAxisLabel = "Share of Resource"
            Exit Function
        End If

        If Session("PercentOn") = "True" Then
            getYAxisLabel = "% Change"
        Else
            Dim sUnits As String, idx As Integer
            sUnits = Session("GR4N")(1)
            idx = InStr(Session("VARN")(1, 1), "[")
            If idx > 0 Then
                idx = IfsVars.Item(Mid(Session("VARN")(1, 1), 1, idx - 1)).Index
            Else
                idx = IfsVars.Item(Session("VARN")(1, 1)).Index
            End If
            'JAM 2016-03-06
            If Session("bTrillionsOk") Then
                getYAxisLabel = Replace(sUnits, "Billion", "Trillion")

            Else
                getYAxisLabel = sUnits
            End If
            'getYAxisLabel = sUnits

            Dim iv As Integer
            For iv = 2 To Session("COUNTE")
                If Session("GR4N")(iv) <> Session("GR4N")(1) Then
                    getYAxisLabel = ""
                    Exit For
                End If
            Next iv

            'jrs 2016/03/28
            If getYAxisLabel = "" Then
                If idx > 0 Then
                    'jrs 2017/06/13 Session
                    If Session("VDisplayUnitsName")(idx) > "" Then
                        'jrs 2012/10/24 sUnits = VDisplayUnitsName(idx)
                        'jrs 2017/06/13 added Session("VDisplayUnitsName")
                        sUnits = getDisplayUnitsWCurrency(idx, Session("CurrencyName"), Session("VDisplayUnitsName"))
                    End If
                End If
            End If

        End If
    End Function

    Private Function PlotColToJSSeries(ByVal i As Integer) As String

        Dim sJscript As String = ""
        sJscript = sJscript & "{name: '" & getSeriesName(i) & "'," & vbCr

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "Lime"
                Case 2
                    sColor = "Blue"
                Case 3
                    sColor = "Red"
                Case 5 'blue violet
                    sColor = "RGB(138,43,226)"
                Case 6 'aqua 
                    sColor = "RGB(0,255,255)"
                Case 7 'deep pink
                    sColor = "RGB(255,20,147)"
                Case 8 'medium spring green
                    sColor = "RGB(0,250,154)"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim Diff As Single, Min As Single, Max As Single
        Min = 10 ^ 10
        Max = -10 ^ 10
        Dim iNumDec As Integer = 0

        If Session("CommonPlot") = "True" Then
            'Find Min and Max
            For j = 1 To Session("DisYear")
                'jrs 2023/10/19
                If IsNumeric(Session("PLOT")(j + Session("RowAdjustment"), i)) Then
                    If Session("PLOT")(j + Session("RowAdjustment"), i) < Min Then
                        Min = Session("PLOT")(j + Session("RowAdjustment"), i)
                    End If
                    If Session("PLOT")(j + Session("RowAdjustment"), i) > Max Then
                        Max = Session("PLOT")(j + Session("RowAdjustment"), i)
                    End If
                End If
            Next
            Diff = Max - Min
            If Diff = 0 Then
                Diff = 0.00001
            End If
        ElseIf Session("PE_CommonMenu") = "Base1" Then 'jrs 2014/03/21
            'Find First Year value
            For j = 1 To Session("DisYear")
                If IsNumeric(Session("PLOT")(j + Session("RowAdjustment"), i)) Then
                    If Session("PLOT")(j + Session("RowAdjustment"), i) > 0 Then
                        FstVal = Session("PLOT")(j + Session("RowAdjustment"), i)
                        Exit For
                    End If
                End If
            Next
        Else
            Dim idx As Integer = Session("DisYear")
            If Session("GType") <> "LORENZ" And IsNumeric(Session("PLOT")(idx, 1)) Then
                If Math.Abs(Session("PLOT")(idx, 1)) < 1 Then
                    iNumDec = 3
                ElseIf Math.Abs(Session("PLOT")(idx, 1)) < 10 Then
                    iNumDec = 2
                ElseIf Math.Abs(Session("PLOT")(idx, 1)) < 100 Then
                    iNumDec = 1
                End If
            Else
                iNumDec = 2
            End If
        End If

        'jrs 2016/03/23
        Dim iStart As Integer, iFirstYear As Integer, Diff2 As Integer
        If Session("FirstYear") Mod 5 = 0 Or Session("Interval") Mod 5 <> 0 Then
            iFirstYear = Session("FirstYear")
            iStart = 1
        Else
            Diff2 = Session("FirstYear") Mod 5
            iFirstYear = Session("FirstYear") + 5 - Diff2
            iStart = 5 - Diff2 + 1
        End If

        For j = iStart To Session("DisYear") Step Session("Interval") 'jrs 2016/03/23 j = 1
            If IsNumeric(Session("PLOT")(j + Session("RowAdjustment"), i)) Then
                sJscript = sJscript & "[" & CStr(Session("FirstYear") + j - 1) & ", "
                If Session("CommonPlot") = "True" Then
                    sJscript = sJscript & CStr(Math.Round((Session("PLOT")(j + Session("RowAdjustment"), i) - Min) / Diff, 2)) & "]"
                ElseIf Session("PE_CommonMenu") = "Base1" And FstVal > 0 Then 'jrs 2014/03/21
                    sJscript = sJscript & CStr(Math.Round((Session("PLOT")(j + Session("RowAdjustment"), i)) / FstVal, 2)) & "]"
                Else
                    sJscript = sJscript & CStr(Math.Round(Session("PLOT")(j + Session("RowAdjustment"), i), iNumDec)) & "]"
                End If

                If j < Session("DisYear") Then
                    sJscript = sJscript & ", "
                End If
                'ElseIf Session("GType") = "BAR" Then
                '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
            End If
        Next

        If j - Session("Interval") <> Session("DisYear") Then
            j = Session("DisYear")
            If IsNumeric(Session("PLOT")(j + Session("RowAdjustment"), i)) Then
                sJscript = sJscript & "[" & CStr(Session("FirstYear") + j - 1) & ", "
                If Session("CommonPlot") = "True" Then
                    sJscript = sJscript & CStr((Session("PLOT")(j + Session("RowAdjustment"), i) - Min) / Diff)
                Else
                    sJscript = sJscript & CStr(Session("PLOT")(j + Session("RowAdjustment"), i)) & "]"
                End If
                'ElseIf Session("GType") = "BAR" Then
                '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
            End If
        End If

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Private Function PlotYearToJSSeries(ByVal i As Integer, ByRef sZeros As String) As String
        'i is the year
        'jrs 2023/10/19 implemented StartYear

        Dim idx As Integer = Session("DisYear")
        Dim iNumDec As Integer = 0
        If Session("GType") <> "LORENZ" And IsNumeric(Session("PLOT")(idx, i)) Then
            If Math.Abs(Session("PLOT")(idx, i)) < 1 Then
                iNumDec = 3
            ElseIf Math.Abs(Session("PLOT")(idx, i)) < 10 Then
                iNumDec = 2
            ElseIf Math.Abs(Session("PLOT")(idx, i)) < 100 Then
                iNumDec = 1
            End If
        Else
            iNumDec = 2
        End If

        Dim sJscript As String = ""
        sJscript = sJscript & "{name: ""Year " & CStr(Session("FirstYear") + i - 1) & """," & vbCr
        sJscript = sJscript & "data: ["

        Dim j As Integer
        For j = 1 To Session("COUNTE")
            sJscript = sJscript & "{name: """ & getSeriesName(j) & """, y: "
            If IsNumeric(Session("PLOT")(i, j)) Then
                If Session("PLOT")(i, j) >= 0.0005 Then
                    sJscript = sJscript & CStr(Math.Round(Session("PLOT")(i, j), iNumDec))
                Else
                    If sZeros > "" Then
                        sZeros = sZeros & ", "
                    Else
                        sZeros = "* Zero Data, "
                    End If
                    sZeros = sZeros & getSeriesName(j)
                End If
            End If
            sJscript = sJscript & "}"
            If j < Session("COUNTE") Then
                sJscript = sJscript & "," & vbCr
            End If
        Next

        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "} " & vbCr

        PlotYearToJSSeries = sJscript
    End Function

    Private Function PlotPairToJSSeries(ByVal i As Integer, ByVal VarCount As Integer, ByVal bBubble As Boolean) As String
        Dim idx As Integer = Session("DisYear")
        Dim iNumDecY As Integer = 0
        If Session("GType") <> "LORENZ" And IsNumeric(Session("PLOT")(idx, i)) Then
            If Math.Abs(Session("PLOT")(idx, i)) < 1 Then
                iNumDecY = 3
            ElseIf Math.Abs(Session("PLOT")(idx, i)) < 10 Then
                iNumDecY = 2
            ElseIf Math.Abs(Session("PLOT")(idx, i)) < 100 Then
                iNumDecY = 1
            End If
        Else
            iNumDecY = 2
        End If

        Dim DimCount As Integer
        DimCount = Session("COUNTE") / VarCount

        Dim iNumDecX As Integer = 0
        If Session("GType") <> "LORENZ" And IsNumeric(Session("PLOT")(idx, i + DimCount)) Then
            If Math.Abs(Session("PLOT")(idx, i + DimCount)) < 1 Then
                iNumDecX = 3
            ElseIf Math.Abs(Session("PLOT")(idx, i + DimCount)) < 10 Then
                iNumDecX = 2
            ElseIf Math.Abs(Session("PLOT")(idx, i + DimCount)) < 100 Then
                iNumDecX = 1
            End If
        Else
            iNumDecX = 2
        End If

        Dim iNumDecZ As Integer = 0
        If bBubble Then
            If Session("GType") <> "LORENZ" And IsNumeric(Session("PLOT")(idx, i + 2 * DimCount)) Then
                If Math.Abs(Session("PLOT")(idx, i + 2 * DimCount)) < 1 Then
                    iNumDecZ = 3
                ElseIf Math.Abs(Session("PLOT")(idx, i + 2 * DimCount)) < 10 Then
                    iNumDecZ = 2
                ElseIf Math.Abs(Session("PLOT")(idx, i + 2 * DimCount)) < 100 Then
                    iNumDecZ = 1
                End If
            Else
                iNumDecZ = 2
            End If
        End If
        Dim sJscript As String = "", sdataLabelsEnabled As String = ""
        sJscript = sJscript & "{name: ""Year " & CStr(Session("FirstYear") + i - 1) & """," & vbCr
        sJscript = sJscript & "dataLabels: {" & vbCr




        If DimCount < NR / 3 Then
            sJscript = sJscript & "enabled: true," & vbCr
            sdataLabelsEnabled = "enabled: true"
        Else
            sJscript = sJscript & "enabled: false," & vbCr
            sdataLabelsEnabled = "enabled: false"
        End If
        sJscript = sJscript & "format: '{point.name}'" & vbCr
        sJscript = sJscript & "}," & vbCr
        sJscript = sJscript & "data: ["
        'If bBubble Then
        '    sJscript = sJscript & "<series id=""CurrentYear" & CStr(i) & """ type=""Bubble"">" & vbCr ' style=""MySelected""
        'Else
        '    sJscript = sJscript & "<series id=""CurrentYear" & CStr(i) & """ type=""Marker"">" & vbCr 'style=""MySelected""
        'End If

        Dim j As Integer
        Dim bFirst As Boolean = True
        Dim sColorHex As String = ""
        For j = 1 To DimCount
            If IsNumeric(Session("PLOT")(i, j)) And IsNumeric(Session("PLOT")(i, j + DimCount)) Then
                If Not bFirst Then
                    sJscript = sJscript & ","
                Else
                    bFirst = False
                End If
                sColorHex = RGBtoHex(Session("RColor")(j - 1), Session("GColor")(j - 1), Session("BColor")(j - 1))
                If Not bBubble Then
                    sJscript = sJscript & "{name: """ & Session("GR1N")(j) &
                    """, y:" & CStr(Math.Round(Session("PLOT")(i, j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("PLOT")(i, j + DimCount), iNumDecX)) &
                    ", fillColor: '" & sColorHex & "', color: '" & sColorHex & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorHex & "'}}}}" & vbCr
                Else
                    If IsNumeric(Session("PLOT")(i, j + 2 * DimCount)) Then
                        sJscript = sJscript & "{name: """ & Session("GR1N")(j) &
                        """, y:" & CStr(Math.Round(Session("PLOT")(i, j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("PLOT")(i, j + DimCount), iNumDecX)) & ", z:" &
                        CStr(Math.Round(Session("PLOT")(i, j + 2 * DimCount), iNumDecZ)) & ", fillColor: '" &
                        sColorHex & "', color: '" & sColorHex & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorHex & "'}}}}" & vbCr
                    Else
                        sJscript = sJscript & "{name: """ & Session("GR1N")(j) &
                        """, y:" & CStr(Math.Round(Session("PLOT")(i, j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("PLOT")(i, j + DimCount), iNumDecX)) &
                        ", z: 0, fillColor= '" & sColorHex & "', color: '" & sColorHex & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorHex & "'}}}}" & vbCr
                    End If
                End If
            End If
        Next j
        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "} " & vbCr

        PlotPairToJSSeries = sJscript
    End Function

    Private Function PlotRegPairToJSSeries() As String
        Dim sJscript As String = ""

        Dim j As Integer
        Dim idx As Integer = Session("DisYear")
        Dim iNumDec As Integer = 0
        If IsNumeric(Session("RegData")(1, 1)) Then
            If Math.Abs(Session("RegData")(1, 1)) < 1 Then
                iNumDec = 3
            ElseIf Math.Abs(Session("RegData")(1, 1)) < 10 Then
                iNumDec = 2
            ElseIf Math.Abs(Session("RegData")(1, 1)) < 100 Then
                iNumDec = 1
            End If
        Else
            iNumDec = 2
        End If

        sJscript = sJscript & "{name: ""Regression"", color: ""#000000""," & vbCr
        sJscript = sJscript & "data: ["

        Dim bFirst As Boolean = True
        For j = 1 To UBound(Session("RegData"), 2)
            If IsNumeric(Session("RegData")(1, j)) And IsNumeric(Session("RegData")(2, j)) Then
                If Not bFirst Then
                    sJscript = sJscript & ","
                Else
                    bFirst = False
                End If
                sJscript = sJscript & "{y:" & CStr(Math.Round(Session("RegData")(2, j), iNumDec)) & ", x: " & CStr(Math.Round(Session("RegData")(1, j), iNumDec)) & "}"
            End If
        Next j
        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "} " & vbCr

        PlotRegPairToJSSeries = sJscript
    End Function


    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim VN, d1, d2, d3, Legend As String
        VN = LTrim(RTrim(Session("VARN")(0, ii)))
        d1 = LTrim(RTrim(Session("FDNG1")(ii)))
        d2 = LTrim(RTrim(Session("FDNG2")(ii)))
        d3 = LTrim(RTrim(Session("FDNG3")(ii)))

        Legend = VN
        If Len(d1) > 0 Then Legend = Legend & "(" & d1
        If Len(d2) > 0 Then
            Legend = Legend & "," & d2
            If d3 > "" Then
                Legend = Legend & "," & d3
            End If
            Legend = Legend & ")"
        Else
            If Len(d1) > 0 Then
                Legend = Legend
                If d3 > "" Then
                    Legend = Legend & "," & d3
                End If
                Legend = Legend & ")"
            End If
        End If

        getSeriesName = PrepareForHTML(Legend) 'jrs 2016/01/04 added PrepareForHTML
    End Function

    Private Function RGBtoHex(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As String
        'jrs 2015/09/14 I had to invert the parameters to make it work
        Dim value As Integer = RGB(B, G, R)
        Dim hex As String = value.ToString("X6")
        RGBtoHex = "#" & hex
    End Function

    'JAM 2016-02-12 Adding All Years for Statistics
    Protected Sub AllYears()
        If Session("mnuUseAllYrs") Then
            Session("mnuUseAllYrs") = False
        Else
            Session("mnuUseAllYrs") = True
        End If
        Session("mnuUseAllYrsTime") = False


    End Sub
    'JAM 2016-02-12 Adding All Years and Time for Statistics
    Protected Sub AllYearsAndTime()
        If Session("mnuUseAllYrsTime") Then
            Session("mnuUseAllYrsTime") = False
        Else
            Session("mnuUseAllYrsTime") = True
        End If


        Session("mnuUseAllYrs") = False

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '               JAM 2016-03-06      Checking for unit conversion from billions to trillions
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub Convert_Billions_To_Trillions()
        Dim i, j, II, jj, a As Integer
        Dim cc, rr As Integer
        Dim vDisYear, vMaxYear, vMinYear As Integer
        Dim convApplied As Boolean
        Dim aa


        ReDim Session("ColumnsToConvert")(Session("COUNTE"))


        II = 0
        jj = 0
        convApplied = False 'JAM 2013-03-21

        vMaxYear = 1
        vMinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For a = 1 To Session("COUNTE")
            If Session("FinYear")(a) > vMaxYear Then vMaxYear = Session("FinYear")(a)
            If Session("FinYear")(a) < vMinYear Then vMinYear = Session("FinYear")(a)
        Next a


        If Session("DisplayTime") = 0 Then
            vDisYear = vMinYear
        Else

            If Session("DisplayLastYear") - Session("FirstYear") + 1 < vMaxYear Then
                vDisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                vDisYear = vMaxYear
            End If
        End If

        If (Session("prevHorizonYear") = 1 And Not Session("fromTableDisp")) Then 'JAM 2016-03-05 Change the condition from "or" to "and" because Session("fromTableDisp")
            For i = 1 To Session("COUNTE")
                Session("ColumnsToConvert")(i) = False
                If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 Then
                    II = II + 1

                    For j = 1 To vDisYear
                        aa = Session("PLOT")(j, i)
                        If Session("PLOT")(j, i) >= 100000 Then 'jrs 2016/04/11 changed to 100,000
                            jj = jj + 1
                            Session("ColumnsToConvert")(i) = True
                            convApplied = True 'JAM 2016-03-21

                            Exit For
                        End If
                    Next j

                End If
            Next i

            'JAM 2016-03-21 If there are some trillions conversion
            For i = 1 To Session("COUNTE")
                If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 And convApplied And Not Session("ColumnsToConvert")(i) Then
                    Session("ColumnsToConvert")(i) = True
                End If
            Next i



        Else
            For i = 1 To Session("COUNTE")
                Session("ColumnsToConvert")(i) = False
                If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 Then 'JAM 2016-03-21 In vb6 version is trillion the condition
                    II = II + 1

                    For j = (Session("prevHorizonYear") + 1) To vDisYear
                        If Session("PLOT")(j, i) >= 10000 Then
                            jj = jj + 1
                            Session("ColumnsToConvert")(i) = True
                            convApplied = True 'JAM 2016-03-21
                            Exit For
                        End If
                    Next j

                End If
            Next i

            'JAM 2016-03-21 If there are some trillions conversion
            For i = 1 To Session("COUNTE")
                If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 And convApplied And Not Session("ColumnsToConvert")(i) Then
                    Session("ColumnsToConvert")(i) = True
                End If
            Next i


        End If


        'JAM 2016-03-06 If The same columns number are in Billions and have a value greater than 10K
        'If II <> 0 And jj <> 0 And II = jj Then
        'JAM 2016-03-21 Only it's necessary one trillion column to apply the logic
        If II <> 0 And jj <> 0 Then
            If Session("prevHorizonYear") = 1 Or Not Session("fromTableDisp") Then
                For cc = 1 To Session("COUNTE")
                    For rr = 1 To vDisYear
                        'JAM 2016-05-16
                        Session("PLOTTrillions")(rr, cc) = Session("PLOT")(rr, cc)

                        If Session("ColumnsToConvert")(cc) Then
                            Session("PLOT")(rr, cc) = CDbl(Session("PLOT")(rr, cc)) / 1000 'JAM 2016-05-16


                        End If


                    Next rr

                Next cc
            Else
                For cc = 1 To Session("COUNTE")
                    For rr = (Session("prevHorizonYear") + 1) To vDisYear
                        Session("PLOTTrillions")(rr, cc) = Session("PLOT")(rr, cc)

                        If Session("ColumnsToConvert")(cc) Then
                            Session("PLOT")(rr, cc) = Session("PLOT")(rr, cc) / 1000
                        End If

                    Next rr

                Next cc

            End If



            Session("bTrillionsOk") = True

        End If


    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''   JAM 2016-03-06 Resetting variables used for Trillions
    ''' 
    ''' </summary>
    ''' <remarks></remarks>


    Protected Sub ResetTrillionsVars()
        Dim rr, cc As Integer

        If Not (Session("bTrillionsOk") Is Nothing) Then
            If Session("bTrillionsOk") Then
                If Not Session("fromTableDisp") Then

                    If Session("bTrillionsOk") Then
                        For cc = 1 To Session("COUNTE")
                            For rr = 1 To Session("DisYear")
                                Session("PLOT")(rr, cc) = Session("PLOTTrillions")(rr, cc) 'JAM 2016-05-16
                            Next rr
                        Next cc
                    End If

                    Session("bTrillionsOk") = False
                End If
            End If
        End If
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '           JAM 2017-07-30      Stacked Bar Options
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub mnuSelRegions_Click()
        Session("PatternSel") = "C"

        'mnuSelCountries.Checked = True
        'mnuSelScenarios.Checked = False
        'mnuSelVariables.Checked = False
        'mnuSelYears.Checked = False

        Call DisplayGraph()
        PackJS()


        ' PatternFillData
    End Sub

    Private Sub mnuSelScenarios_Click()
        Session("PatternSel") = "S"
        'mnuSelCountries.Checked = False
        'mnuSelScenarios.Checked = True
        'mnuSelVariables.Checked = False
        'mnuSelYears.Checked = False

        Call DisplayGraph()
        PackJS()
    End Sub

    Private Sub mnuSelVariables_Click()
        Session("PatternSel") = "V"
        'mnuSelCountries.Checked = False
        'mnuSelScenarios.Checked = False
        'mnuSelVariables.Checked = True
        'mnuSelYears.Checked = False


        Call DisplayGraph()
        PackJS()
    End Sub

    Private Sub mnuSelYears_Click()

        Session("PatternSel") = " "

        'mnuSelCountries.Checked = False
        'mnuSelScenarios.Checked = False
        'mnuSelVariables.Checked = False
        'mnuSelYears.Checked = True


        Call DisplayGraph()
        PackJS()
    End Sub
    Private Sub mnuSelVariables2nd_Click()
        'JAM 2017-09-03
        Session("PatternSel") = "V2"
        Call DisplayGraph()
        PackJS()
    End Sub
    Private Sub mnuSelVariables3rd_Click()
        'JAM 2017-09-03
        Session("PatternSel") = "V3"
        Call DisplayGraph()
        PackJS()
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '           JAM 2017-07-09 Filling data based on selected pattern
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function PatternFillData() As String

        On Error GoTo PatternError

        Dim Timepoints As String
        Dim sJscript As String = ""  'JAM 2017-07-31

        Timepoints = Session("DisplayLastYear") - Session("FirstYear") + 1 'FirstYearPE% + 1

        Dim validpoints As Integer
        validpoints = 0

        Dim pp As Integer, PtIdx As Integer

        'Dim VarQty() As Integer
        Dim isagg As Integer
        Dim l As Integer
        'Dim DiffGroup() As Integer


        ReDim Session("VarQty")(pCOUNTE)
        ReDim Session("DiffGroup")(pCOUNTE)



        Dim p, q, g, j As Integer
        Dim PI, GI As Integer
        Dim vexists, vexistsG As Boolean
        Dim iStart As Integer
        Dim NormalGroup As Boolean
        Dim GType As String
        Dim sColor As String


        If Session("GType") = "SCAT" Then
            PtIdx = pCOUNTE / 2
        End If

        If Session("PatternSel") = "C" Then

            For p = 1 To pCOUNTE
                Session("VarQty")(p) = 0

            Next p

            For p = 1 To pCOUNTE
                Session("DiffGroup")(p) = 0
            Next p


            PI = 1
            GI = 1
            For p = 1 To pCOUNTE
                vexists = False
                If p = 1 Then
                    Session("VarNumber")(1) = Session("GR1N")(1)


                    Session("CountryPattern")(1, 1) = Session("VARN")(0, 1) + " " + Session("GR2N")(1) + " " + Session("GR3N")(1)
                    Session("CountryPattern")(1, 2) = p
                    Session("CountryPattern")(1, 3) = PI


                    Session("VarGroup")(1) = Session("VARN")(0, 1) + " " + Session("GR2N")(1) + " " + Session("GR3N")(1)
                Else
                    For q = 1 To PI 'pCOUNTE
                        If Session("GR1N")(p) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                            vexists = False
                        Else
                            vexists = True
                            Exit For
                        End If
                    Next q


                    'JAM 2017-07-11 Defining Subsets
                    For g = 1 To GI
                        If (Session("VARN")(0, p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)) <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                            vexistsG = False
                        Else
                            vexistsG = True
                            Exit For
                        End If


                    Next g


                    If Not vexists Then
                        PI = PI + 1
                        Session("VarNumber")(PI) = Session("GR1N")(p)

                    End If

                    If Not vexistsG Then
                        GI = GI + 1
                        Session("VarGroup")(GI) = Session("VARN")(0, p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)

                    End If



                    Session("CountryPattern")(p, 1) = Session("VARN")(0, p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)
                    Session("CountryPattern")(p, 2) = p
                    Session("CountryPattern")(p, 3) = PI



                End If
            Next p

            'JAM 2017-07-12 Define Subset Points
            Session("Pego1.Subsets") = GI

            Session("StackedPoints") = PI

            'JAM 2017-07-12
            Session("Pego1.Points") = Session("StackedPoints")

            'JAM 2017-07-31
            ReDim Session("Pego1.YData")(GI, PI)

            sJscript = ""

            '   Fill in data
            '   ------------
            For l = 1 To PI

                'JAM 2017-07-31 Filling data series

                ''sJscript = sJscript & "{name: '" & Session("VarNumber")(l) & "'," & vbCr


                ''sColor = ""
                ''If l < 9 Then
                ''    Select Case l
                ''        Case 1
                ''            sColor = "Lime"
                ''        Case 2
                ''            sColor = "Blue"
                ''        Case 3
                ''            sColor = "Red"
                ''        Case 5 'blue violet
                ''            sColor = "RGB(138,43,226)"
                ''        Case 6 'aqua 
                ''            sColor = "RGB(0,255,255)"
                ''        Case 7 'deep pink
                ''            sColor = "RGB(255,20,147)"
                ''        Case 8 'medium spring green
                ''            sColor = "RGB(0,250,154)"
                ''    End Select
                ''End If
                ''sJscript = sJscript & "color: '" & sColor & "'," & vbCr

                ''sJscript = sJscript & "data: ["






                For cc% = 1 To pCOUNTE
                    '-------from old filldata
                    '       Get yearly values
                    '       -----------------
                    'mti Validation changes
                    If GType = "Validation Distance" Then
                        j = cc% * 2 - 1
                    ElseIf GType = "Validation % Error" Then
                        j = cc% * 2
                    Else
                        j = cc%
                    End If

                    Dim abc As String

                    If Session("FirstYear") Mod 5 = 0 Or Session("Interval") Mod 5 <> 0 Then
                        'If FirstYearPE Mod 5 = 0 Or Interval Mod 5 <> 0 Then
                        iStart = 1
                    Else
                        iStart = 5 - (Session("FirstYear") Mod 5) + 1
                    End If
                    If Session("mnuFormatGrpbyLocation.Checked") Then 'JAM 2017-07-12 Variable doesn't exist

                        If Session("VarNumber")(l) = Session("GR1N")(cc%) Then
                            abc = Session("PLOT")(Session("PYear"), cc%)
                            If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                    Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                Else
                                    Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                End If
                            Else
                                Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                            End If

                            Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                            ''Next rr%

                            ''''''''''''''''''''JAM 2017-07-31
                            ''sJscript = sJscript & "['" & Session("VarGroup")(Session("DiffGroup")(l)) & "', "

                            ''sJscript = sJscript & Session("Pego1.YData")((Session("DiffGroup")(l) - 1) / Session("Interval"), l - 1) & "]"

                            ''If cc% < pCOUNTE Then
                            ''    sJscript = sJscript & ", "

                            ''End If
                            ''''''''''''''''''''''
                        End If


                    Else

                        If Session("VarNumber")(l) = Session("GR1N")(cc%) Then
                            'abc = Session("PLOT")(Session("PYear"), cc%)
                            If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                    Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                Else
                                    Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                End If
                            Else
                                Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                            End If

                            Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1


                            ''''''''''''''''''''JAM 2017-07-31
                            ''sJscript = sJscript & "['" & Session("VarGroup")(Session("DiffGroup")(l)) & "', "

                            ''sJscript = sJscript & Session("Pego1.YData")((Session("DiffGroup")(l) - 1) / Session("Interval"), l - 1) & "]"

                            ''If cc% < pCOUNTE Then
                            ''    sJscript = sJscript & ", "

                            ''End If
                            ''''''''''''''''''''''



                        End If



                    End If 'mnuFormatGrpbyLocation.Checked (else)



                Next cc%

                ''sJscript = sJscript & "] " & vbCr
                ''sJscript = sJscript & "} " & vbCr
                ''If l < PI Then
                ''    sJscript = sJscript & ", " & vbCr
                ''End If

            Next l




        Else 'JAM 2017-08-17 Fixing Subsets Pattern: Adding GR2N and GR3N
            If Session("PatternSel") = "V" Then 'Variables

                For p = 1 To pCOUNTE
                    Session("VarQty")(p) = 0

                Next p

                For p = 1 To pCOUNTE
                    Session("DiffGroup")(p) = 0
                Next p


                PI = 1
                'JAM 2017-07-15
                GI = 1
                For p = 1 To pCOUNTE
                    vexists = False
                    If p = 1 Then
                        'JAM 2017-08-12 Validating Scenarios labels
                        If InStr(Session("VARN")(0, 1), "[") > 0 Then
                            Session("VarNumber")(1) = Mid(Session("VARN")(0, 1), 1, InStr(1, Session("VARN")(0, 1), "[") - 1)


                            'JAM 2017-07-15 Avoiding Considering "Total" in GR2n like a different group
                            'JAM 2017-09-03 Adding gr2 and Gr3
                            Session("CountryPattern")(1, 1) = Session("GR1N")(1) + Session("GR2N")(1) + Session("GR3N")(1) + "[" + Mid(Session("VARN")(0, 1), InStr(1, Session("VARN")(0, 1), "[") + 1, InStr(1, Session("VARN")(0, 1), "]") - InStr(1, Session("VARN")(0, 1), "[") - 1) + "]"
                            Session("CountryPattern")(1, 2) = p
                            Session("CountryPattern")(1, 3) = p

                            'JAM 2017-07-15
                            Session("VarGroup")(1) = Session("GR1N")(1) + Session("GR2N")(1) + Session("GR3N")(1) + "[" + Mid(Session("VARN")(0, 1), InStr(1, Session("VARN")(0, 1), "[") + 1, InStr(1, Session("VARN")(0, 1), "]") - InStr(1, Session("VARN")(0, 1), "[") - 1) + "]"
                        Else
                            Session("VarNumber")(1) = Session("VARN")(0, 1)


                            'JAM 2017-07-15 Avoiding Considering "Total" in GR2n like a different group
                            Session("CountryPattern")(1, 1) = Session("GR1N")(1) + Session("GR2N")(1) + Session("GR3N")(1) + "[" + Session("VARN")(0, 1) + "]"
                            Session("CountryPattern")(1, 2) = p
                            Session("CountryPattern")(1, 3) = p

                            'JAM 2017-07-15
                            Session("VarGroup")(1) = Session("GR1N")(1) + Session("GR2N")(1) + Session("GR3N")(1) + "[" + Session("VARN")(0, 1) + "]"
                        End If
                    Else
                        For q = 1 To PI 'pCOUNTE
                            'JAM 2017-08-12 Validating Scenarios labels
                            If InStr(Session("VARN")(0, p), "[") > 0 Then
                                'JAM 2017-07-16
                                If Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                    vexists = False
                                Else
                                    vexists = True
                                    Exit For
                                End If
                            Else
                                'JAM 2017-07-16
                                If Session("VARN")(0, p) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                    vexists = False
                                Else
                                    vexists = True
                                    Exit For
                                End If
                            End If
                        Next q

                        'JAM 2017-07-16 Defining Subsets
                        For g = 1 To GI
                            'JAM 2017-08-12 Validating Scenarios labels
                            If InStr(Session("VARN")(0, p), "[") > 0 Then
                                If (Session("GR1N")(p) + Session("GR2N")(p) + Session("GR3N")(p) + "[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]") <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                                    vexistsG = False
                                Else
                                    vexistsG = True
                                    Exit For
                                End If
                            Else
                                'JAM 2017-08-12 Validating Scenarios labels
                                If (Session("GR1N")(p) + Session("GR2N")(p) + Session("GR3N")(p) + "[" + Session("VARN")(0, p) + "]") <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                                    vexistsG = False
                                Else
                                    vexistsG = True
                                    Exit For
                                End If
                            End If
                        Next g



                        If Not vexists Then
                            PI = PI + 1
                            'JAM 2017-08-12 Validating Scenarios labels
                            If InStr(Session("VARN")(0, p), "[") > 0 Then
                                Session("VarNumber")(PI) = Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1)
                            Else
                                Session("VarNumber")(PI) = Session("VARN")(0, p)
                            End If
                        End If

                        'JAM 2017-07-18
                        If Not vexistsG Then
                            GI = GI + 1
                            'JAM 2017-08-12 Validating Scenarios labels
                            If InStr(Session("VARN")(0, p), "[") > 0 Then
                                Session("VarGroup")(GI) = Session("GR1N")(p) + Session("GR2N")(p) + Session("GR3N")(p) + "[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]"
                            Else
                                Session("VarGroup")(GI) = Session("GR1N")(p) + Session("GR2N")(p) + Session("GR3N")(p) + "[" + Session("VARN")(0, p) + "]"
                            End If
                        End If
                        'JAM 2017-08-12 Validating Scenarios labels
                        If InStr(Session("VARN")(0, p), "[") > 0 Then
                            Session("CountryPattern")(p, 1) = Session("GR1N")(p) + Session("GR2N")(p) + Session("GR3N")(p) + "[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]"
                            Session("CountryPattern")(p, 2) = p
                            Session("CountryPattern")(p, 3) = PI
                        Else
                            'JAM 2917-09-03 Adding gr2 and Gr3
                            Session("CountryPattern")(p, 1) = Session("GR1N")(p) + Session("GR2N")(p) + Session("GR3N")(p) + "[" + Session("VARN")(0, p) + "]"
                            Session("CountryPattern")(p, 2) = p
                            Session("CountryPattern")(p, 3) = PI

                        End If




                    End If
                Next p

                'JAM 2017-07-20 Define Subset Points
                Session("Pego1.Subsets") = GI

                Session("StackedPoints") = PI

                'JAM 2017-07-20
                Session("Pego1.Points") = Session("StackedPoints")

                'JAM 2017-07-31
                ReDim Session("Pego1.YData")(GI, PI)

                sJscript = ""



                '   Fill in data
                '   ------------
                For l = 1 To PI
                    For cc% = 1 To pCOUNTE
                        If GType = "Validation Distance" Then
                            j = cc% * 2 - 1
                        ElseIf GType = "Validation % Error" Then
                            j = cc% * 2
                        Else
                            j = cc%
                        End If

                        If Session("FirstYear") Mod 5 = 0 Or Session("Interval") Mod 5 <> 0 Then
                            iStart = 1
                        Else
                            iStart = 5 - (Session("FirstYear") Mod 5) + 1
                        End If
                        'JAM 2017-08-12 Validating Scenarios labels
                        If InStr(1, Session("VARN")(0, cc%), "[") > 0 Then
                            If Session("mnuFormatGrpbyLocation.Checked") Then
                                'JAM 2017-07-20 Only a year is graphed
                                If Session("VarNumber")(l) = Mid(Session("VARN")(0, cc%), 1, InStr(1, Session("VARN")(0, cc%), "[") - 1) Then
                                    If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                        If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                            Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                        Else
                                            Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                        End If
                                    Else
                                        Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                    End If

                                    Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1

                                End If

                            Else
                                If Session("VarNumber")(l) = Mid(Session("VARN")(0, cc%), 1, InStr(1, Session("VARN")(0, cc%), "[") - 1) Then

                                    If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                        If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                            Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                        Else
                                            Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                        End If
                                    Else
                                        Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                    End If

                                    Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                                End If

                            End If 'mnuFormatGrpbyLocation.Checked (else)

                        Else
                            If Session("mnuFormatGrpbyLocation.Checked") Then
                                'JAM 2017-07-20 Only a year is graphed
                                If Session("VarNumber")(l) = Session("VARN")(0, cc%) Then
                                    If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                        If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                            Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                        Else
                                            Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                        End If
                                    Else
                                        Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                    End If

                                    Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1

                                End If

                            Else
                                If Session("VarNumber")(l) = Session("VARN")(0, cc%) Then

                                    If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                        If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                            Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                        Else
                                            Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                        End If
                                    Else
                                        Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                    End If

                                    Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                                End If

                            End If 'mnuFormatGrpbyLocation.Checked (else)
                        End If

                    Next cc%
                Next l

            Else
                If Session("PatternSel") = "S" Then 'Scenarios

                    For p = 1 To pCOUNTE
                        Session("VarQty")(p) = 0

                    Next p

                    For p = 1 To pCOUNTE
                        Session("DiffGroup")(p) = 0
                    Next p


                    PI = 1
                    'JAM 2017-07-22
                    GI = 1
                    For p = 1 To pCOUNTE
                        vexists = False
                        If p = 1 Then
                            'JAM 2017-08-12 Validating Scenarios labels
                            If InStr(1, Session("VARN")(0, 1), "]") > 0 Then
                                Session("VarNumber")(1) = Mid(Session("VARN")(0, 1), InStr(1, Session("VARN")(0, 1), "[") + 1, InStr(1, Session("VARN")(0, 1), "]") - InStr(1, Session("VARN")(0, 1), "[") - 1)

                                Session("CountryPattern")(1, 1) = Mid(Session("VARN")(0, 1), 1, InStr(1, Session("VARN")(0, 1), "[") - 1) + " " + Session("GR1N")(1) + " " + Session("GR2N")(1) + " " + Session("GR3N")(1)
                                Session("CountryPattern")(1, 2) = p
                                Session("CountryPattern")(1, 3) = p


                                'JAM 2017-07-22
                                Session("VarGroup")(1) = Mid(Session("VARN")(0, 1), 1, InStr(1, Session("VARN")(0, 1), "[") - 1) + " " + Session("GR1N")(1) + " " + Session("GR2N")(1) + " " + Session("GR3N")(1)
                            Else
                                Session("VarNumber")(1) = Session("VARN")(0, 1)

                                Session("CountryPattern")(1, 1) = Session("VARN")(0, 1) + " " + Session("GR1N")(1) + " " + Session("GR2N")(1) + " " + Session("GR3N")(1)
                                Session("CountryPattern")(1, 2) = p
                                Session("CountryPattern")(1, 3) = p


                                'JAM 2017-07-22
                                Session("VarGroup")(1) = Session("VARN")(0, 1) + " " + Session("GR1N")(1) + " " + Session("GR2N")(1) + " " + Session("GR3N")(1)
                            End If
                        Else
                            'JAM 2017-07-22
                            For q = 1 To PI 'pCOUNTE
                                If InStr(1, Session("VARN")(0, p), "[") > 0 Then
                                    If Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                        vexists = False
                                    Else
                                        vexists = True
                                        Exit For
                                    End If
                                Else
                                    If Session("VARN")(0, p) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                        vexists = False
                                    Else
                                        vexists = True
                                        Exit For
                                    End If
                                End If
                            Next q

                            'JAM 2017-07-23 Defining Subsets
                            For g = 1 To GI
                                'JAM 2017-08-12 Validating Scenarios labels
                                If InStr(1, Session("VARN")(0, p), "[") > 0 Then
                                    If (Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1) + " " + Session("GR1N")(p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)) <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                                        vexistsG = False
                                    Else
                                        vexistsG = True
                                        Exit For
                                    End If
                                Else
                                    If (Session("VARN")(0, p) + " " + Session("GR1N")(p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)) <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                                        vexistsG = False
                                    Else
                                        vexistsG = True
                                        Exit For
                                    End If
                                End If
                            Next g

                            'JAM 2017-08-12 Validating Scenarios labels
                            If InStr(1, Session("VARN")(0, p), "[") > 0 Then
                                If Not vexists Then
                                    PI = PI + 1
                                    Session("VarNumber")(PI) = Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1)
                                End If

                                'JAM 2017-07-23
                                If Not vexistsG Then
                                    GI = GI + 1
                                    Session("VarGroup")(GI) = Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1) + " " + Session("GR1N")(p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)
                                End If



                                Session("CountryPattern")(p, 1) = Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1) + " " + Session("GR1N")(p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)
                                Session("CountryPattern")(p, 2) = p
                                Session("CountryPattern")(p, 3) = PI

                            Else
                                If Not vexists Then
                                    PI = PI + 1
                                    Session("VarNumber")(PI) = Session("VARN")(0, p)
                                End If

                                'JAM 2017-07-23
                                If Not vexistsG Then
                                    GI = GI + 1
                                    Session("VarGroup")(GI) = Session("VARN")(0, p) + " " + Session("GR1N")(p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)
                                End If



                                Session("CountryPattern")(p, 1) = Session("VARN")(0, p) + " " + Session("GR1N")(p) + " " + Session("GR2N")(p) + " " + Session("GR3N")(p)
                                Session("CountryPattern")(p, 2) = p
                                Session("CountryPattern")(p, 3) = PI
                            End If

                        End If
                    Next p

                    'JAM 2017-07-26 Define Subset Points
                    Session("Pego1.Subsets") = GI

                    Session("StackedPoints") = PI

                    'JAM 2017-07-26
                    Session("Pego1.Points") = Session("StackedPoints")

                    'JAM 2017-07-31
                    ReDim Session("Pego1.YData")(GI, PI)

                    sJscript = ""



                    '   Fill in data
                    '   ------------
                    For l = 1 To PI
                        For cc% = 1 To pCOUNTE
                            '-------from old filldata
                            '       Get yearly values
                            '       -----------------
                            'mti Validation changes
                            If GType = "Validation Distance" Then
                                j = cc% * 2 - 1
                            ElseIf GType = "Validation % Error" Then
                                j = cc% * 2
                            Else
                                j = cc%
                            End If


                            If Session("FirstYear") Mod 5 = 0 Or Session("Interval") Mod 5 <> 0 Then
                                iStart = 1
                            Else
                                iStart = 5 - (Session("FirstYear") Mod 5) + 1
                            End If

                            'JAM 2017-08-12 Validating Scenarios labels
                            If InStr(1, Session("VARN")(0, cc%), "[") > 0 Then
                                If Session("mnuFormatGrpbyLocation.Checked") Then

                                    If Session("VarNumber")(l) = Mid(Session("VARN")(0, cc%), InStr(1, Session("VARN")(0, cc%), "[") + 1, InStr(1, Session("VARN")(0, cc%), "]") - InStr(1, Session("VARN")(0, cc%), "[") - 1) Then
                                        If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                            If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                            Else
                                                Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                            End If
                                        Else
                                            Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                        End If

                                        Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1

                                    End If

                                Else

                                    If Session("VarNumber")(l) = Mid(Session("VARN")(0, cc%), InStr(1, Session("VARN")(0, cc%), "[") + 1, InStr(1, Session("VARN")(0, cc%), "]") - InStr(1, Session("VARN")(0, cc%), "[") - 1) Then

                                        If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                            If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                            Else
                                                Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                            End If
                                        Else
                                            Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                        End If

                                        Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                                    End If

                                End If 'mnuFormatGrpbyLocation.Checked (else)

                            Else
                                If Session("mnuFormatGrpbyLocation.Checked") Then

                                    If Session("VarNumber")(l) = Session("VARN")(0, cc%) Then
                                        If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                            If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                            Else
                                                Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                            End If
                                        Else
                                            Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                        End If

                                        Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1

                                    End If

                                Else

                                    If Session("VarNumber")(l) = Session("VARN")(0, cc%) Then

                                        If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                            If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                            Else
                                                Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                            End If
                                        Else
                                            Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                        End If

                                        Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                                    End If

                                End If 'mnuFormatGrpbyLocation.Checked (else)

                            End If

                        Next cc%
                    Next l
                    'StackedPoints = PI
                Else 'JAM 2017-09-03  Adding GR2N  Pattern
                    If Session("PatternSel") = "V2" Then 'Variables

                        For p = 1 To pCOUNTE
                            Session("VarQty")(p) = 0

                        Next p

                        For p = 1 To pCOUNTE
                            Session("DiffGroup")(p) = 0
                        Next p


                        PI = 1
                        'JAM 2017-07-15
                        GI = 1
                        For p = 1 To pCOUNTE
                            vexists = False
                            If p = 1 Then
                                'JAM 2017-08-12 Validating Scenarios labels
                                If InStr(Session("VARN")(0, 1), "[") > 0 Then
                                    'JAM 2017-09-05
                                    'Session("VarNumber")(1) = Mid(Session("VARN")(0, 1), 1, InStr(1, Session("VARN")(0, 1), "[") - 1) + Session("GR2N")(1)
                                    Session("VarNumber")(1) = Session("GR2N")(1)

                                    'JAM 2017-07-15 Avoiding Considering "Total" in GR2n like a different group
                                    Session("CountryPattern")(1, 1) = "[" + Mid(Session("VARN")(0, 1), InStr(1, Session("VARN")(0, 1), "[") + 1, InStr(1, Session("VARN")(0, 1), "]") - InStr(1, Session("VARN")(0, 1), "[") - 1) + "]" + Session("GR2N")(1) + " " + Session("GR1N")(1)
                                    Session("CountryPattern")(1, 2) = p
                                    Session("CountryPattern")(1, 3) = p

                                    'JAM 2017-07-15
                                    Session("VarGroup")(1) = "[" + Mid(Session("VARN")(0, 1), InStr(1, Session("VARN")(0, 1), "[") + 1, InStr(1, Session("VARN")(0, 1), "]") - InStr(1, Session("VARN")(0, 1), "[") - 1) + "]" + " " + Session("GR1N")(1)
                                Else
                                    'JAM 2017-09-05
                                    'Session("VarNumber")(1) = Session("VARN")(0, 1) + Session("GR2N")(1)
                                    Session("VarNumber")(1) = Session("GR2N")(1)

                                    'JAM 2017-07-15 Avoiding Considering "Total" in GR2n like a different group
                                    Session("CountryPattern")(1, 1) = "[" + Session("VARN")(0, 1) + "]" + Session("GR2N")(1) + " " + Session("GR1N")(1)
                                    Session("CountryPattern")(1, 2) = p
                                    Session("CountryPattern")(1, 3) = p

                                    'JAM 2017-07-15
                                    Session("VarGroup")(1) = "[" + Session("VARN")(0, 1) + "]" + " " + Session("GR1N")(1)
                                End If
                            Else
                                For q = 1 To PI 'pCOUNTE
                                    'JAM 2017-08-12 Validating Scenarios labels
                                    If InStr(Session("VARN")(0, p), "[") > 0 Then
                                        'JAM 2017-07-16
                                        'If (Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1) + Session("GR2N")(p)) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                        If (Session("GR2N")(p)) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                            vexists = False
                                        Else
                                            vexists = True
                                            Exit For
                                        End If
                                    Else
                                        'JAM 2017-07-16
                                        'If (Session("VARN")(0, p) + Session("GR2N")(p)) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                        If (Session("GR2N")(p)) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                            vexists = False
                                        Else
                                            vexists = True
                                            Exit For
                                        End If
                                    End If
                                Next q

                                'JAM 2017-07-16 Defining Subsets
                                For g = 1 To GI
                                    'JAM 2017-08-12 Validating Scenarios labels
                                    If InStr(Session("VARN")(0, p), "[") > 0 Then
                                        If ("[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]" + " " + Session("GR1N")(p)) <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                                            vexistsG = False
                                        Else
                                            vexistsG = True
                                            Exit For
                                        End If
                                    Else
                                        'JAM 2017-08-12 Validating Scenarios labels
                                        If ("[" + Session("VARN")(0, p) + "]" + " " + Session("GR1N")(p)) <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                                            vexistsG = False
                                        Else
                                            vexistsG = True
                                            Exit For
                                        End If
                                    End If
                                Next g



                                If Not vexists Then
                                    PI = PI + 1
                                    'JAM 2017-08-12 Validating Scenarios labels
                                    If InStr(Session("VARN")(0, p), "[") > 0 Then
                                        'Session("VarNumber")(PI) = Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1) + Session("GR2N")(p)
                                        Session("VarNumber")(PI) = Session("GR2N")(p)
                                    Else
                                        'Session("VarNumber")(PI) = Session("VARN")(0, p) + Session("GR2N")(p)
                                        Session("VarNumber")(PI) = Session("GR2N")(p)
                                    End If
                                End If

                                'JAM 2017-07-18
                                If Not vexistsG Then
                                    GI = GI + 1
                                    'JAM 2017-08-12 Validating Scenarios labels
                                    If InStr(Session("VARN")(0, p), "[") > 0 Then
                                        Session("VarGroup")(GI) = "[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]" + " " + Session("GR1N")(p)
                                    Else
                                        Session("VarGroup")(GI) = "[" + Session("VARN")(0, p) + "]" + " " + Session("GR1N")(p)
                                    End If
                                End If
                                'JAM 2017-08-12 Validating Scenarios labels
                                If InStr(Session("VARN")(0, p), "[") > 0 Then
                                    Session("CountryPattern")(p, 1) = "[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]" + Session("GR2N")(p) + " " + Session("GR1N")(p)
                                    Session("CountryPattern")(p, 2) = p
                                    Session("CountryPattern")(p, 3) = PI
                                Else
                                    Session("CountryPattern")(p, 1) = "[" + Session("VARN")(0, p) + "]" + Session("GR2N")(p) + " " + Session("GR1N")(p)
                                    Session("CountryPattern")(p, 2) = p
                                    Session("CountryPattern")(p, 3) = PI

                                End If




                            End If
                        Next p

                        'JAM 2017-07-20 Define Subset Points
                        Session("Pego1.Subsets") = GI

                        Session("StackedPoints") = PI

                        'JAM 2017-07-20
                        Session("Pego1.Points") = Session("StackedPoints")

                        'JAM 2017-07-31
                        ReDim Session("Pego1.YData")(GI, PI)

                        sJscript = ""



                        '   Fill in data
                        '   ------------
                        For l = 1 To PI
                            For cc% = 1 To pCOUNTE
                                If GType = "Validation Distance" Then
                                    j = cc% * 2 - 1
                                ElseIf GType = "Validation % Error" Then
                                    j = cc% * 2
                                Else
                                    j = cc%
                                End If

                                If Session("FirstYear") Mod 5 = 0 Or Session("Interval") Mod 5 <> 0 Then
                                    iStart = 1
                                Else
                                    iStart = 5 - (Session("FirstYear") Mod 5) + 1
                                End If
                                'JAM 2017-08-12 Validating Scenarios labels
                                If InStr(1, Session("VARN")(0, cc%), "[") > 0 Then
                                    If Session("mnuFormatGrpbyLocation.Checked") Then
                                        'JAM 2017-07-20 Only a year is graphed
                                        'If Session("VarNumber")(l) = Mid(Session("VARN")(0, cc%), 1, InStr(1, Session("VARN")(0, cc%), "[") - 1) + Session("GR2N")(cc%) Then
                                        If Session("VarNumber")(l) = Session("GR2N")(cc%) Then
                                            If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                                If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                    Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                                Else
                                                    Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                                End If
                                            Else
                                                Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                            End If

                                            Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1

                                        End If

                                    Else
                                        'If Session("VarNumber")(l) = Mid(Session("VARN")(0, cc%), 1, InStr(1, Session("VARN")(0, cc%), "[") - 1) + Session("GR2N")(cc%) Then
                                        If Session("VarNumber")(l) = Session("GR2N")(cc%) Then

                                            If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                                If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                    Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                                Else
                                                    Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                                End If
                                            Else
                                                Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                            End If

                                            Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                                        End If

                                    End If 'mnuFormatGrpbyLocation.Checked (else)

                                Else
                                    If Session("mnuFormatGrpbyLocation.Checked") Then
                                        'JAM 2017-07-20 Only a year is graphed
                                        'If Session("VarNumber")(l) = Session("VARN")(0, cc%) + Session("GR2N")(cc%) Then
                                        If Session("VarNumber")(l) = Session("GR2N")(cc%) Then
                                            If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                                If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                    Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                                Else
                                                    Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                                End If
                                            Else
                                                Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                            End If

                                            Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1

                                        End If

                                    Else
                                        'If Session("VarNumber")(l) = Session("VARN")(0, cc%) + Session("GR2N")(cc%) Then
                                        If Session("VarNumber")(l) = Session("GR2N")(cc%) Then

                                            If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                                If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                    Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                                Else
                                                    Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                                End If
                                            Else
                                                Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                            End If

                                            Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                                        End If

                                    End If 'mnuFormatGrpbyLocation.Checked (else)
                                End If

                            Next cc%
                        Next l
                    Else
                        If Session("PatternSel") = "V3" Then 'Third Dimmension

                            For p = 1 To pCOUNTE
                                Session("VarQty")(p) = 0

                            Next p

                            For p = 1 To pCOUNTE
                                Session("DiffGroup")(p) = 0
                            Next p


                            PI = 1
                            'JAM 2017-07-15
                            GI = 1
                            For p = 1 To pCOUNTE
                                vexists = False
                                If p = 1 Then
                                    'JAM 2017-08-12 Validating Scenarios labels
                                    If InStr(Session("VARN")(0, 1), "[") > 0 Then
                                        'JAM 2017-09-05
                                        'Session("VarNumber")(1) = Mid(Session("VARN")(0, 1), 1, InStr(1, Session("VARN")(0, 1), "[") - 1) + Session("GR3N")(1)
                                        Session("VarNumber")(1) = Session("GR3N")(1)


                                        'JAM 2017-07-15 Avoiding Considering "Total" in GR2n like a different group
                                        Session("CountryPattern")(1, 1) = "[" + Mid(Session("VARN")(0, 1), InStr(1, Session("VARN")(0, 1), "[") + 1, InStr(1, Session("VARN")(0, 1), "]") - InStr(1, Session("VARN")(0, 1), "[") - 1) + "]" + Session("GR3N")(1)
                                        Session("CountryPattern")(1, 2) = p
                                        Session("CountryPattern")(1, 3) = p

                                        'JAM 2017-07-15
                                        Session("VarGroup")(1) = "[" + Mid(Session("VARN")(0, 1), InStr(1, Session("VARN")(0, 1), "[") + 1, InStr(1, Session("VARN")(0, 1), "]") - InStr(1, Session("VARN")(0, 1), "[") - 1) + "]" + " " + Session("GR1N")(1) + " " + Session("GR2N")(1)
                                    Else
                                        'Session("VarNumber")(1) = Session("VARN")(0, 1) + Session("GR3N")(1)
                                        Session("VarNumber")(1) = Session("GR3N")(1)


                                        'JAM 2017-07-15 Avoiding Considering "Total" in GR2n like a different group
                                        Session("CountryPattern")(1, 1) = "[" + Session("VARN")(0, 1) + "]" + Session("GR3N")(1)
                                        Session("CountryPattern")(1, 2) = p
                                        Session("CountryPattern")(1, 3) = p

                                        'JAM 2017-07-15
                                        Session("VarGroup")(1) = "[" + Session("VARN")(0, 1) + "]" + " " + Session("GR1N")(1) + " " + Session("GR2N")(1)
                                    End If
                                Else
                                    For q = 1 To PI 'pCOUNTE
                                        'JAM 2017-08-12 Validating Scenarios labels
                                        If InStr(Session("VARN")(0, p), "[") > 0 Then
                                            'JAM 2017-07-16
                                            'If (Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1) + Session("GR3N")(p)) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                            If (Session("GR3N")(p)) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                                vexists = False
                                            Else
                                                vexists = True
                                                Exit For
                                            End If
                                        Else
                                            'JAM 2017-07-16
                                            'If (Session("VARN")(0, p) + Session("GR3N")(p)) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                            If (Session("GR3N")(p)) <> Session("VarNumber")(q) And Session("VarNumber")(q) <> "" Then
                                                vexists = False
                                            Else
                                                vexists = True
                                                Exit For
                                            End If
                                        End If
                                    Next q

                                    'JAM 2017-07-16 Defining Subsets
                                    For g = 1 To GI
                                        'JAM 2017-08-12 Validating Scenarios labels
                                        If InStr(Session("VARN")(0, p), "[") > 0 Then
                                            If ("[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]" + " " + Session("GR1N")(p) + " " + Session("GR2N")(p)) <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                                                vexistsG = False
                                            Else
                                                vexistsG = True
                                                Exit For
                                            End If
                                        Else
                                            'JAM 2017-08-12 Validating Scenarios labels
                                            If ("[" + Session("VARN")(0, p) + "]" + " " + Session("GR1N")(p) + " " + Session("GR2N")(p)) <> Session("VarGroup")(g) And Session("VarGroup")(g) <> "" Then
                                                vexistsG = False
                                            Else
                                                vexistsG = True
                                                Exit For
                                            End If
                                        End If
                                    Next g



                                    If Not vexists Then
                                        PI = PI + 1
                                        'JAM 2017-08-12 Validating Scenarios labels
                                        If InStr(Session("VARN")(0, p), "[") > 0 Then
                                            'Session("VarNumber")(PI) = Mid(Session("VARN")(0, p), 1, InStr(1, Session("VARN")(0, p), "[") - 1) + Session("GR3N")(p)
                                            Session("VarNumber")(PI) = Session("GR3N")(p)
                                        Else
                                            ' Session("VarNumber")(PI) = Session("VARN")(0, p) + Session("GR3N")(p)
                                            Session("VarNumber")(PI) = Session("GR3N")(p)
                                        End If
                                    End If

                                    'JAM 2017-07-18
                                    If Not vexistsG Then
                                        GI = GI + 1
                                        'JAM 2017-08-12 Validating Scenarios labels
                                        If InStr(Session("VARN")(0, p), "[") > 0 Then
                                            Session("VarGroup")(GI) = "[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]" + " " + Session("GR1N")(p) + " " + Session("GR2N")(p)
                                        Else
                                            Session("VarGroup")(GI) = "[" + Session("VARN")(0, p) + "]" + " " + Session("GR1N")(p) + " " + Session("GR2N")(p)
                                        End If
                                    End If
                                    'JAM 2017-08-12 Validating Scenarios labels
                                    If InStr(Session("VARN")(0, p), "[") > 0 Then
                                        Session("CountryPattern")(p, 1) = "[" + Mid(Session("VARN")(0, p), InStr(1, Session("VARN")(0, p), "[") + 1, InStr(1, Session("VARN")(0, p), "]") - InStr(1, Session("VARN")(0, p), "[") - 1) + "]" + Session("GR3N")(p)
                                        Session("CountryPattern")(p, 2) = p
                                        Session("CountryPattern")(p, 3) = PI
                                    Else
                                        Session("CountryPattern")(p, 1) = "[" + Session("VARN")(0, p) + "]" + Session("GR3N")(p)
                                        Session("CountryPattern")(p, 2) = p
                                        Session("CountryPattern")(p, 3) = PI

                                    End If




                                End If
                            Next p

                            'JAM 2017-07-20 Define Subset Points
                            Session("Pego1.Subsets") = GI

                            Session("StackedPoints") = PI

                            'JAM 2017-07-20
                            Session("Pego1.Points") = Session("StackedPoints")

                            'JAM 2017-07-31
                            ReDim Session("Pego1.YData")(GI, PI)

                            sJscript = ""



                            '   Fill in data
                            '   ------------
                            For l = 1 To PI
                                For cc% = 1 To pCOUNTE
                                    If GType = "Validation Distance" Then
                                        j = cc% * 2 - 1
                                    ElseIf GType = "Validation % Error" Then
                                        j = cc% * 2
                                    Else
                                        j = cc%
                                    End If

                                    If Session("FirstYear") Mod 5 = 0 Or Session("Interval") Mod 5 <> 0 Then
                                        iStart = 1
                                    Else
                                        iStart = 5 - (Session("FirstYear") Mod 5) + 1
                                    End If
                                    'JAM 2017-08-12 Validating Scenarios labels
                                    If InStr(1, Session("VARN")(0, cc%), "[") > 0 Then
                                        If Session("mnuFormatGrpbyLocation.Checked") Then
                                            'JAM 2017-07-20 Only a year is graphed
                                            'If Session("VarNumber")(l) = Mid(Session("VARN")(0, cc%), 1, InStr(1, Session("VARN")(0, cc%), "[") - 1) + Session("GR3N")(cc%) Then
                                            If Session("VarNumber")(l) = Session("GR3N")(cc%) Then
                                                If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                                    If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                        Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                                    Else
                                                        Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                                    End If
                                                Else
                                                    Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                                End If

                                                Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1

                                            End If

                                        Else
                                            'If Session("VarNumber")(l) = Mid(Session("VARN")(0, cc%), 1, InStr(1, Session("VARN")(0, cc%), "[") - 1) + Session("GR3N")(cc%) Then
                                            If Session("VarNumber")(l) = Session("GR3N")(cc%) Then

                                                If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                                    If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                        Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                                    Else
                                                        Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                                    End If
                                                Else
                                                    Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                                End If

                                                Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                                            End If

                                        End If 'mnuFormatGrpbyLocation.Checked (else)

                                    Else
                                        If Session("mnuFormatGrpbyLocation.Checked") Then
                                            'JAM 2017-07-20 Only a year is graphed
                                            'If Session("VarNumber")(l) = Session("VARN")(0, cc%) + Session("GR3N")(cc%) Then
                                            If Session("VarNumber")(l) = Session("GR3N")(cc%) Then
                                                If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                                    If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                        Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                                    Else
                                                        Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                                    End If
                                                Else
                                                    Session("Pego1.YData")((Session("DiffGroup")(l)) / Session("Interval"), l - 1) = -9999
                                                End If

                                                Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1

                                            End If

                                        Else
                                            'If Session("VarNumber")(l) = Session("VARN")(0, cc%) + Session("GR3N")(cc%) Then
                                            If Session("VarNumber")(l) = Session("GR3N")(cc%) Then

                                                If IsNumeric(Session("PLOT")(Session("PYear"), cc%)) Then
                                                    If Not (Session("PLOT")(Session("PYear"), l) = 99999999 And Session("PercentOn")) Then
                                                        Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = Math.Round(Session("PLOT")(Session("PYear"), cc%), 4)
                                                    Else
                                                        Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                                    End If
                                                Else
                                                    Session("Pego1.YData")(Session("DiffGroup")(l), l - 1) = -9999
                                                End If

                                                Session("DiffGroup")(l) = Session("DiffGroup")(l) + 1
                                            End If

                                        End If 'mnuFormatGrpbyLocation.Checked (else)
                                    End If

                                Next cc%
                            Next l


                        End If 'End If PatternSel = 'V3'

                    End If 'End If PatternSel = 'V2'

                End If 'End If PatternSel = 'S'



            End If 'End If PatternSel = 'V'


        End If 'End If Pego1.XAxisLabel = "Year"= 'C'

        '' Call DisplayGraph
        'Exit Function
        ''PatternError:

        '          Exit Sub

        ' sJscript = sJscript & "]" & vbCr
        ' sJscript = sJscript & "} " & vbCr

        'JAM 2017-08-01 Filling Data
        For l = 1 To Session("Pego1.Subsets")

            sJscript = sJscript & "{name: '" & Session("VarGroup")(l) & "'," & vbCr

            sColor = ""
            If l < 9 Then
                Select Case l
                    Case 1
                        sColor = "Lime"
                    Case 2
                        sColor = "Blue"
                    Case 3
                        sColor = "Red"
                    Case 5 'blue violet
                        sColor = "RGB(138,43,226)"
                    Case 6 'aqua 
                        sColor = "RGB(0,255,255)"
                    Case 7 'deep pink
                        sColor = "RGB(255,20,147)"
                    Case 8 'medium spring green
                        sColor = "RGB(0,250,154)"
                End Select
            End If
            sJscript = sJscript & "color: '" & sColor & "'," & vbCr

            sJscript = sJscript & "data: ["


            For cc% = 1 To Session("Pego1.Points")
                sJscript = sJscript & "['" & Session("VarNumber")(cc%) & "', "

                sJscript = sJscript & Session("Pego1.YData")(l - 1, cc% - 1) & "]"

                If cc% < Session("Pego1.Points") Then
                    sJscript = sJscript & ", "

                End If

            Next cc%

            sJscript = sJscript & "] " & vbCr
            sJscript = sJscript & "} " & vbCr
            If l < Session("Pego1.Subsets") Then
                sJscript = sJscript & ", " & vbCr
            End If


        Next l







        PatternFillData = sJscript

PatternError:
        Exit Function

    End Function





End Class
