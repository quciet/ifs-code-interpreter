'jrs 2011/07/15 Implemented by José Roberto Solórzano
Option Strict Off
Option Explicit On
Imports IFs.Data
'jam 2021/08/15
Imports Microsoft.AspNet.FriendlyUrls

'Namespace IFsWeb
Partial Class frm_Histogram
    Inherits Page

    'mti Validation changes 19 August, 2002
    Dim sInterval As Short

    'Dim CommonPlot As String
    'mti Validation changes
    Dim pCOUNTE As Short
    Dim VH(100) As Object
    Dim VF(100) As Object
    ''''''''''''''''''''JAM 2012-08-01
    'Dim gpercentiles As Integer 'JAM 2012-08-01
    Dim gorigpercentiles As Integer
    'Dim DefPerc()
    'Dim PopAll()
    Dim Yvalue()
    Dim Yctvalue()
    ' Dim Ypopulation As Boolean
    'Dim Session("DivValue") As Boolean
    Dim VarinTable As Boolean
    Dim Legend$
    'Dim Session("gcategories")()
    'Dim Session("counters")()
    Dim grangeyear As Integer
    Dim TotalMin
    Dim TotalMax
    Public gMinValue
    Public gMaxValue
    Public DefMinMax As Boolean
    Public gOrigMin
    Public gOrigMax
    Dim extra1 As Long
    Dim extra2 As Long
    'Dim ShowSelection As Boolean
    Dim DataTable()
    Dim VarTable()
    Dim percentilechosen
    Dim Valuechosen
    Dim Countrychosen
    Dim Variablechosen
    Dim SumPopulation
    Dim SumCt
    Dim Maxcoltable
    'Dim NoDataNull()  'JAM 2010-09-25
    Dim ginitrue As Boolean
    Dim validpoints As Integer
    Dim BckCategories() 'JAM 2012-05-25
    Public GLbgpercentiles 'JAM 2012-05-25
    Public ChangeCateg As Boolean 'JAM 2012-05-27
    Public ModeDefCateg As Boolean
    Dim ViewDetail As Char 'JAM 2013-01-26
    'Dim s As String 'JAM 2013-01-12
    'Dim gVarName As Char 'JAM 2013-01-30
    'Dim gFile As Char 'JAM 2013-01-30

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim sValidationVar As String
        Dim ii As Short
        Dim j As Integer

        'jam 2021/08/21
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othVarName As String
        Dim othFile As String
        Dim othDim2 As String
        Dim othDim3 As String
        Dim othshowDetail As String
        Dim qtyPAram As Integer




        'jam 2021/08/21
        If Not IsNothing(Request.QueryString.Get("VarName")) Or Not IsNothing(Request.QueryString.Get("File")) Or Not IsNothing(Request.QueryString.Get("Dim2")) Or Not IsNothing(Request.QueryString.Get("Dim3")) Or Not IsNothing(Request.QueryString("showdetail")) Then


            If Not IsNothing(Request.QueryString("VarName")) And Not Page.IsPostBack Then

                othVarName = Request.QueryString("VarName")

            Else
                othVarName = ""

            End If

            If Not IsNothing(Request.QueryString("File")) And Not Page.IsPostBack Then

                othFile = Request.QueryString("File")

            Else
                othFile = ""
            End If

            If Not IsNothing(Request.QueryString("Dim2")) And Not Page.IsPostBack Then

                othDim2 = Request.QueryString("Dim2")

            Else
                othDim2 = ""
            End If

            If Not IsNothing(Request.QueryString("Dim3")) And Not Page.IsPostBack Then

                othDim3 = Request.QueryString("Dim3")

            Else
                othDim3 = ""
            End If

            If Not IsNothing(Request.QueryString("showdetail")) And Not Page.IsPostBack Then

                othshowDetail = Request.QueryString("showdetail")

            Else
                othshowDetail = ""
            End If


            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Histogram.aspx")


            If othVarName <> "" And othFile <> "" And othDim2 <> "" And othDim3 <> "" And othshowDetail <> "" Then
                vurl = FriendlyUrl.Href("~/frm_Histogram", HistogramPrepareForFriendlyURL(othVarName), othFile, othDim2, othDim3, othshowDetail)

                Response.Redirect(vurl)

            Else
                If othVarName <> "" And othFile = "" And othDim2 = "" And othDim3 = "" And othshowDetail = "" Then
                    vurl = FriendlyUrl.Href("~/frm_Histogram", HistogramPrepareForFriendlyURL(othVarName))

                    Response.Redirect(vurl)

                Else
                    If othVarName <> "" And othFile <> "" And othDim2 = "" And othDim3 = "" And othshowDetail = "" Then
                        vurl = FriendlyUrl.Href("~/frm_Histogram", HistogramPrepareForFriendlyURL(othVarName), othFile)

                        Response.Redirect(vurl)

                    Else 'jam 2021/08/21
                        If othVarName <> "" And othFile <> "" And othDim2 = "" And othDim3 = "" And othshowDetail <> "" Then
                            vurl = FriendlyUrl.Href("~/frm_Histogram", HistogramPrepareForFriendlyURL(othVarName), othFile, othshowDetail)


                            Response.Redirect(vurl)

                        Else 'jam 2021/08/21

                            'jam 2021/08/21
                            If othVarName <> "" And othFile <> "" And othDim2 <> "" And othDim3 = "" And othshowDetail <> "" Then
                                vurl = FriendlyUrl.Href("~/frm_Histogram", HistogramPrepareForFriendlyURL(othVarName), othFile, othshowDetail, othDim2)


                                Response.Redirect(vurl)

                            Else

                                If othVarName <> "" And othFile <> "" And othDim2 <> "" And othDim3 <> "" And othshowDetail <> "" Then
                                    vurl = FriendlyUrl.Href("~/frm_Histogram", HistogramPrepareForFriendlyURL(othVarName), othFile, othshowDetail, othDim2, othDim3)


                                    Response.Redirect(vurl)

                                Else 'jam 2021/08/21
                                    vurl = FriendlyUrl.Href("~/frm_Histogram", HistogramPrepareForFriendlyURL(othVarName))


                                    Response.Redirect(vurl)


                                End If





                            End If




                        End If

                    End If

                End If


            End If


        End If


        'jam 2021/08/21
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vVarName As String
        Dim vFile As String
        Dim vDim2 As String
        Dim vDim3 As String
        Dim vshowdetail As String

        'jam 2021/08/21
        If segments.Count = 0 Then

            vVarName = Session("VarName")
            vFile = Nothing
            vDim2 = Nothing
            vDim3 = Nothing
            vshowdetail = Nothing

        Else



            If segments.Count = 1 Then

                vVarName = segments(0)
                vFile = Nothing
                vDim2 = Nothing
                vDim3 = Nothing
                vshowdetail = Nothing

            ElseIf segments.Count = 2 Then
                vVarName = segments(0)
                vFile = segments(1)
                vDim2 = Nothing
                vDim3 = Nothing
                vshowdetail = Nothing

            ElseIf segments.Count = 3 Then
                vVarName = segments(0)
                vFile = segments(1)
                vDim2 = segments(2)
                vDim3 = Nothing
                vshowdetail = Nothing

            ElseIf segments.Count = 4 Then
                vVarName = segments(0)
                vFile = segments(1)
                vDim2 = segments(2)
                vDim3 = segments(3)
                vshowdetail = Nothing

            ElseIf segments.Count = 5 Then
                vVarName = segments(0)
                vFile = segments(1)
                vDim2 = segments(2)
                vDim3 = segments(3)
                vshowdetail = segments(4)


            Else
                vVarName = segments(0)
                Session("vVarName") = vVarName
                vFile = Nothing
                vDim2 = Nothing
                vDim3 = Nothing
                vshowdetail = Nothing

            End If

        End If

        Session("vVarName") = vVarName
        Session("vFile") = vFile
        Session("vDim2") = vDim2
        Session("vDim3") = vDim3
        Session("vshowdetail") = vshowdetail




        'JAM 2013-03-20 Getting URL path
        Session("ServerURL") = "http://" & Request.Url.Host & HttpContext.Current.Request.ApplicationPath



        If Not Page.IsPostBack Then
            'jrs 2013/02/21
            If Session("PYear") = Nothing Then
                Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
                Dim FirstYearNotNull As Integer
                FirstYearNotNull = FindFirstYearNotNull()
                'jrs 2010/11/01
                If Session("PYear") > FirstYearNotNull Then
                    Session("PYear") = FirstYearNotNull
                End If
            End If

            '  If Session("PYear") Is Nothing Then 'JAM 2013-02-20
            'JAM 2012-08-26 Add Temporarily to get data in PYEar
            'Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
            'End If
            'JAM 2012-12-29 Adding default selected bar
            Session("vselected") = 0
            vselectedseries.Text = Session("vselected")

            'JAM 2013-01-31 Resetting Titles
            Session("TableTitle") = ""
            'Session("TableTitle") = "Title"



            'jrs  2013-01-25
            Dim bReloadj As Boolean

            'jam 2021/08/22
            'bReloadj = Not (Request.QueryString("VarName") Is Nothing)


            bReloadj = Not (Session("vVarName") Is Nothing)

            If bReloadj Then
                'JAM 2013-01-29
                Dim urlo As String
                urlo = Request.Url.ToString



                'some logic to remove the scenario number from the variable name
                Dim sVarName As String = ""
                If Session("COUNTE") = NR Then
                    Dim posx As Integer
                    posx = InStr(Session("VARN")(1, 1), "[") - 1
                    sVarName = Mid(Session("VARN")(1, 1), 1, posx)
                End If

                'jam 2021/08/22
                'If (Request.QueryString("VarName") = sVarName And Session("COUNTE") = NR) Then
                '    bReloadj = False
                'End If


                If (Session("vVarName") = sVarName And Session("COUNTE") = NR) Then
                    bReloadj = False
                End If




            End If


            'JAM 2013-01-24 Adding loading data when form is loaded from an url
            'If Session("GraphDisplayParent") Is Nothing Then
            'If Not (Request.QueryString("VarName") Is Nothing) And Session("GraphDisplayParent") Is Nothing Then
            If bReloadj Then
                'Read querystring
                Dim sVarName As String, DefaultFile As Integer, idx As Integer
                'jam 2021/08/22
                'sVarName = Request.QueryString("VarName")
                'DefaultFile = CInt(Request.QueryString("File"))

                sVarName = (Session("vVarName"))
                DefaultFile = CInt(Session("vFile"))


                Session("DefaultFile") = DefaultFile
                'gVarName = sVarName
                'gFile = CStr(DefaultFile)


                Dim sDim2 As String = "", sDim3 As String = "", idxDim2 As Integer = -1, idxDim3 As Integer = -1
                'jam 2021/08/22
                'If Not (Request.QueryString("Dim2") Is Nothing) Then
                '    sDim2 = Request.QueryString("Dim2")
                'End If
                'If Not (Request.QueryString("Dim3") Is Nothing) Then
                '    sDim3 = Request.QueryString("Dim3")
                'End If


                If Not (Session("vDim2") Is Nothing) Then
                    sDim2 = Session("vDim2")
                End If
                If Not (Session("vDim3") Is Nothing) Then
                    sDim3 = Session("vDim3")
                End If


                'JAM 2013-01-29
                'Dim url2 As String
                'url2 = Request.Url.ToString

                'Load Data
                Dim DimNum As Integer
                Dim RunCount As Integer
                Dim iix As Integer
                DimNum = 1
                RunCount = MaxVal(DimNum) - 1

                Dim MyFileforDisplay As IStorage
                Dim MyDF As IDataframe

                For iix = 1 To RunCount
                    Session("COUNTE") = Session("COUNTE") + 1
                    gLoadForDisplayNET(sVarName, idx, GRPS(1, iix), iix, sDim2, idxDim2, sDim3, idxDim3, "", DefaultFile, iix = 1, iix = RunCount, False, 1,
                    Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    'gLoadForDisplay(sVarName, idx, GRPS(1, iix), iix, sDim2, idxDim2, sDim3, idxDim3, "", DefaultFile, iix = 1, iix = RunCount, False, 1,
                    'Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                    'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    gLoadForDisplaySaveLbls(Session("COUNTE"), idx, GRPS(1, iix), sDim2, sDim3, False, DefaultFile, "",
                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                Next iix

            End If




            'JAM 2013-01-14
            'If Not Session("isfromDispMenu") Then 'JAM 2013-01-13 
            '    'Read querystring
            '    Dim sVarName As String, DefaultFile As Integer, idx As Integer
            '    sVarName = Request.QueryString("VarName")
            '    DefaultFile = CInt(Request.QueryString("File"))
            '    Session("DefaultFile") = DefaultFile

            '    Dim sDim2 As String = "", sDim3 As String = "", idxDim2 As Integer = -1, idxDim3 As Integer = -1
            '    If Not (Request.QueryString("Dim2") Is Nothing) Then
            '        sDim2 = Request.QueryString("Dim2")
            '    End If
            '    If Not (Request.QueryString("Dim3") Is Nothing) Then
            '        sDim3 = Request.QueryString("Dim3")
            '    End If

            'Load Data
            'Dim DimNum As Integer
            'Dim RunCount As Integer
            'Dim iix As Integer
            'DimNum = 1
            'RunCount = MaxVal(DimNum) - 1
            'For iix = 1 To RunCount
            '    Session("COUNTE") = Session("COUNTE") + 1
            '    gLoadForDisplay(sVarName, idx, GRPS(1, iix), iix, sDim2, idxDim2, sDim3, idxDim3, "", DefaultFile, iix = 1, iix = RunCount, False, 1, _
            '    Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, _
            '    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), _
            '    Session("IFs_MDB2Active"), Session("CurrencyVal"))
            '    gLoadForDisplaySaveLbls(Session("COUNTE"), idx, GRPS(1, iix), "", "", False, DefaultFile, "", _
            '    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"), _
            '    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
            'Next iix

            'Dim IfsPars As New myParameters
            'IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"), _
            'Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"), _
            'Session("PutFdrvRun"), Session("CurrencySym"))

            'GiniSetup(IfsPars, Session("COUNTE"), Me, Session("VARN"), Session("LastYear"), Session("FirstYear"), _
            'Session("PutFdrvRun"), Session("ActiveBase"), Session("RunFiles"), Session("bLoadForDisplaySuccess"), _
            'Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), _
            'Session("CurrencyVal"), Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis"), _
            'Session("LastYearForecast"), Session("bComputation"), Session("bFillHisOnlyHoles"), _
            'Session("LorenzX_Year"), Session("LorenzY_Year"), Session("PLOT"), Session("LorenzX"), _
            'Session("LorenzY"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"), _
            'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("YAxisLabel"), _
            'Session("XAxisLabel"))

            '    Session("HistogramParent") = "frm_WorldVariables.aspx"
            'End If


            'jrs 2011/06/07
            Dim bReload As Boolean = False
            'Dim sListNames As String = ""
            'If Not (Request.QueryString("ListNames") Is Nothing) Then
            '    sListNames = Request.QueryString("ListNames")
            '    If Not (Session("ListLoaded") Is Nothing) Then
            '        If Session("ListLoaded") <> sListNames Then
            '            bReload = True
            '        End If
            '    Else
            '        bReload = True
            '    End If
            '    Session("ListLoaded") = sListNames
            'End If

            'JAM 2013-02-05 Commented
            'jrs 2011/04/05
            ''If Session("COUNTE") = 0 Or bReload Then
            ''    'Read querystring
            ''    Dim sHistFor As String, GrpOp As Integer
            ''    sHistFor = Request.QueryString("HistFor")
            ''    GrpOp = CInt(Request.QueryString("GrpOp"))
            ''    Dim sDim1 As String, sDim2 As String, sDim3 As String, sFiles As String
            ''    If Not Request.QueryString("Dim1") Is Nothing Then
            ''        sDim1 = Request.QueryString("Dim1")
            ''    End If
            ''    If Not Request.QueryString("Dim2") Is Nothing Then
            ''        sDim2 = Request.QueryString("Dim2")
            ''    End If
            ''    If Not Request.QueryString("Dim3") Is Nothing Then
            ''        sDim3 = Request.QueryString("Dim3")
            ''    End If
            ''    sFiles = Request.QueryString("File")
            ''    If sFiles Is Nothing Then
            ''        sFiles = "1"
            ''    End If



            ''    'Dim rs As DataTable
            ''    'rs = getVarLists(sListNames, sHistFor, Session("PutFdrvRun"))
            ''    'Session("PackagedDispRs") = rs
            ''    'Dim p As Integer


            ''    'jrs 2012/04/12 
            ''    'p = InStr(2, sListNames, "'")
            ''    'If p > 1 Then
            ''    '    Session("TableTitle") = Mid(sListNames, 2, p - 2)
            ''    'Else
            ''    '    Session("TableTitle") = sListNames
            ''    'End If
            ''    'p = InStr(sListNames, """,""")
            ''    'If p > 0 Then
            ''    '    'Multiple lists
            ''    '    Session("TableTitle") = sListNames
            ''    'Else
            ''    '    'Single list
            ''    '    Session("TableTitle") = Mid(sListNames, 2, Len(sListNames) - 2)
            ''    'End If

            ''    Session("GType") = "BAR"
            ''    Dim IfsPars As New myParameters
            ''    IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"), _
            ''    Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"), _
            ''    Session("PutFdrvRun"), Session("CurrencySym"))
            ''    Session("GraphDisplayParent") = "frm_DisplayMenu.aspx"
            ''    'Load list boxes
            ''    Dim lstDim As New ListBox, lstDim2 As New ListBox, lstDim3 As New ListBox
            ''    Dim lstFiles As New ListBox
            ''    lstDim.ID = "lstDim"
            ''    lstDim2.ID = "lstDim2"
            ''    lstDim3.ID = "lstDim3"
            ''    LoadAllDims(lstDim, Session("AllDimsIdx"))
            ''    LoadAllDims(lstDim2, Session("AllDimsIdx"))
            ''    LoadAllDims(lstDim3, Session("AllDimsIdx"))

            ''    If sDim1 = "" Then
            ''        sDim1 = Trim(CStr(lstDim.Items.Count - 1))
            ''    End If
            ''    If sDim2 = "" Then
            ''        sDim2 = Trim(CStr(lstDim2.Items.Count - 1))
            ''    End If
            ''    If sDim3 = "" Then
            ''        sDim3 = Trim(CStr(lstDim3.Items.Count - 1))
            ''    End If

            ''    ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
            ''    ReDim Preserve Session("DimIdxs2")(lstDim2.Items.Count - 1)
            ''    ReDim Preserve Session("DimIdxs3")(lstDim3.Items.Count - 1)

            ''    LoadFiles(Session("VarSelCheck"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), _
            ''    Session("ActiveBase"), Session("VarSelCheckHis"), Session("PutFdrvRun2"), Session("WorkingFile"), _
            ''    Session("FilesSelected"), Session("IFsRunFiles"), Session("RunFiles"), Session("ShortSce"), lstFiles)
            ''    If sFiles > "" Then
            ''        lstFiles.Items(0).Selected = False
            ''    End If

            ''    ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)

            ''    'Set Region options
            ''    Select Case GrpOp
            ''        Case 0 'Regions
            ''            Session("bUseReg") = True
            ''            Session("bUseGroup") = False
            ''            Session("bUseGroupDec") = False
            ''            Session("bUseGList") = False
            ''        Case 1 'Groups
            ''            Session("bUseReg") = False
            ''            Session("bUseGroup") = True
            ''            Session("bUseGroupDec") = False
            ''            Session("bUseGList") = False
            ''        Case 2 'Dec Groups
            ''            Session("bUseReg") = False
            ''            Session("bUseGroup") = False
            ''            Session("bUseGroupDec") = True
            ''            Session("bUseGList") = False
            ''        Case 3 'G-Lists
            ''            Session("bUseReg") = False
            ''            Session("bUseGroup") = False
            ''            Session("bUseGroupDec") = False
            ''            Session("bUseGList") = True
            ''    End Select

            ''    'Set List Boxes
            ''    URLToDimLst(sDim1, lstDim)
            ''    URLToDimLst(sDim2, lstDim2)
            ''    URLToDimLst(sDim3, lstDim3)
            ''    URLToFileLst(sFiles, lstFiles, Session("txtFiles"), Session("IFsRunFiles"), Session("PDFilesIdxs"), Session("FilesSelected"), _
            ''    Session("RunFiles"), Session("PutFdrvRun2"), Session("ShortSce"))

            ''    'Load Package
            ''    'gLoadPackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), _
            ''    'Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, Session("TableTitle"), _
            ''    'Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"), _
            ''    'Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"), _
            ''    'Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"), _
            ''    'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"), _
            ''    'Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"), _
            ''    'Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"), _
            ''    'Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"), _
            ''    'Session("PopMales"), Session("PopFemales"), sListNames, Session("txtFiles"))
            ''End If

            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            '------------------------------------------------------------
            'JRS 11/03/03
            If Session("DisplayLastYear") = 0 Then
                Session("DisplayLastYear") = Session("LastYear")
            End If

            'jrs 2011/08/26
            'If Session("GType") <> "BAR" Then Session("Interval") = 1
            'If Session("GType") = "BAR" And Session("DisYear") > 20 And Session("Interval") < 5 Then
            Session("Interval") = 5
            'End If
            Session("CommonPlot") = "False"

            Dim i As Short

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
                    'JAM 2012-09-02
                    Session("pCOUNTE") = pCOUNTE
                Else
                    pCOUNTE = Session("COUNTE") / 3 * 2
                    'JAM 2012-09-02
                    Session("pCOUNTE") = pCOUNTE
                End If
            End If
            'mti Validation changes


            'jrs 2008/01/18
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
            Session("PE_CommonMenu") = "Display"
            Session("PE_CommonPlot") = "False"

            'mti Validation changes
            'If InStr(1, Session("GType"), "Validation") > 0 Then
            '    pCOUNTE = 0
            '    For j = 1 To Session("COUNTE") - 1 'the last one need to be forecast
            '        sValidationVar = Trim(Session("VARN")(4, j))
            '        If Mid(sValidationVar, 1, 2) = "VH" Then 'asc("V") = 86
            '            sValidationVar = "VF" & Mid(sValidationVar, 3)
            '            For ii = j + 1 To Session("COUNTE")
            '                If Trim(Session("VARN")(4, ii)) = sValidationVar Then
            '                    pCOUNTE = pCOUNTE + 1
            '                    VH(pCOUNTE) = j
            '                    VF(pCOUNTE) = ii
            '                    Exit For
            '                End If
            '            Next ii
            '        End If
            '    Next j
            '    'pCOUNTE = pCOUNTE * 2
            'Else
            '    If Not Session("bBubble") Or Session("GType") <> "SCAT" Then
            '        pCOUNTE = Session("COUNTE")
            '    Else
            '        pCOUNTE = Session("COUNTE") / 3 * 2
            '    End If

            'End If
            'JAM 2012-08-26
            'Dim FirstYearNotNull As Integer
            'FirstYearNotNull = FindFirstYearNotNull()
            ''jrs 2010/11/01
            'If Session("PYear") > FirstYearNotNull Then
            '    Session("PYear") = FirstYearNotNull
            'End If

            'JAM 2012-08-26
            FindGlbMinMax()

            'JAM 2012-08-26 Ask to Jose
            'If DisplayLastYear = 0 Then
            '    DisplayLastYear = LastYear
            'End If

            'JAM 2012-09-30 DisplayLastYear is managed in Global.asax
            'Session("DisplayLastYear") = Session("LastYear")

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim i As Integer



        'If Not (Session("BDAction") = Nothing) And Not Page.IsPostBack And Session("GType") = "SCAT" Then
        '    If Session("sVarZ") > "" Then
        '        pCOUNTE = Session("COUNTE") / 3 * 2
        '    Else
        '        pCOUNTE = Session("COUNTE")
        '    End If

        '    Select Case Session("BDAction")
        '        Case "POP"
        '            mnuBDPOP_Click()
        '        Case "GDP"
        '            mnuBDGDP_Click()
        '        Case "Other"
        '            mnuBDOther_Click()
        '        Case "None"
        '            mnuBDNone_Click()
        '    End Select
        '    Session.Remove("BDAction")
        'End If





        'jam 2021/08/22
        'JAM 2013-02-05
        'If Not (Request.QueryString("showdetail") Is Nothing) Then
        '    ViewDetail = Request.QueryString("showdetail")
        'End If


        If Not (Session("vshowdetail") Is Nothing) Then
            ViewDetail = Session("vshowdetail")
        End If


        'jrs 2012/05/30
        If Not Page.IsPostBack Then


            'JAM 2012-08-01
            Session("gpercentiles") = 10
            gorigpercentiles = Session("gpercentiles")
            Session("ShowSelection") = False
            Session("Ypopulation") = True
            If Session("OrdAlp") Is Nothing Then 'JAM 2013-02-25
                Session("OrdAlp") = False 'JAM 2012-12-29
            End If
            Session("DivValue") = False 'JAM 2012-10-06
            Session("mnuinvaluesChecked") = True 'JAM 2012-10-06
            Session("mnuinpercentageChecked") = False 'JAM 2012-10-06
            Call LoadPopAll()
            Call FillData()

            'JAM 2013-01-05 Chosen variable sorting
            'If Session("OrdAlp") Then
            '    For i = 0 To Session("gpercentiles") - 1
            '        Call BubbleOrder(i, 3)
            '    Next i
            'End If

            If Session("Ypopulation") Then

                If Session("COUNTE") = NR Then 'JAM 2010-11-10
                    Session("YAxisLabel") = "% of World Population"
                Else
                    Session("YAxisLabel") = "% of Group Population"
                End If
                Session("mnuPerCountriesChecked") = False
                Session("mnuPerPopulationChecked") = True
            Else
                Session("YAxisLabel") = "% of Countries"
                Session("mnuPerCountriesChecked") = True
                Session("mnuPerPopulationChecked") = False
            End If

        End If

        'jrs 2015/10/28 moved down to have the flag to Div already set
        LoadMenu()



        DisplayGraph()
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
        For ii = 1 To Session("pCOUNTE") 'pCOUNTE
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
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

        'If Session("GType") <> "SCAT" And Session("GType") <> "SCAT1" Then
        If Session("DisYear") * Session("pCOUNTE") > 25000 Then
            Session("DisYear") = 25000 / Session("pCOUNTE") 'pCOUNTE
        End If
        'Else
        'If Session("DisYear") * (pCOUNTE / 2) > 25000 Then
        '    Session("DisYear") = 25000 / (pCOUNTE / 2)
        'End If
        'End If

        PackJS()
    End Sub

    Private Sub LoadMenu()
        cswmWebMenu.UserData = "jsolorza::976739975"

        'jam 2021/08/22
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"



        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")




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

        If IsNothing(Session("GraphDisplayParent")) Then
            Session("GraphDisplayParent") = "frm_WorldVariables.aspx"




        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_4a6c"
        'jam 2021/08/22
        'JAM 2013-02-05
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("GraphDisplayParent") & "';"

        cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl(Session("GraphDisplayParent")) & "';"


        'JAM 2013-02-05
        'If Request.QueryString("ListNames") Is Nothing Then
        '    cswmGroup1.OnMenuBarClick = "location.href='" & Session("GraphDisplayParent") & "';"
        'Else
        '    Dim sLst As String
        '    sLst = Request.QueryString("ListNames")
        '    sLst = Replace(sLst, """,""", ",,")
        '    sLst = Mid(sLst, 2, Len(sLst) - 2)
        '    'jrs 2011/11/03 
        '    'Mark found that the list Foreign Reserves as Percent of GDP didn't work
        '    'there were 2 weird characters after the word Reserves, remove them
        '    sLst = Replace(sLst, Chr(127), " ")

        '    cswmGroup1.OnMenuBarClick = "location.href='" & Session("GraphDisplayParent") & "?ListNames=" & sLst & "';"
        'End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Main Menu"
        cswmGroup1.ID = "mnuMainMenu"
        'jam 2021/08/22
        'cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl("frm_MainMenu.aspx") & "';"



        'If Session("GType") = "PIE" Or Session("GType") = "COHORT" Then
        '    cswmGroup1 = cswmWebMenu.Groups.Add()
        '    cswmGroup1.Caption = "Time"
        '    cswmGroup1.ID = "Group_Time"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Advance"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Regress"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Go To Year"
        'ElseIf Session("GType") = "SCAT" Then
        '    cswmGroup1 = cswmWebMenu.Groups.Add()
        '    cswmGroup1.Caption = "Time"
        '    cswmGroup1.ID = "Group_Time"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Advance"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Regress"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Go To Year"

        '    'jrs 2012/03/12
        '    cswmGroup1 = cswmWebMenu.Groups.Add()
        '    cswmGroup1.Caption = "Grouping Options"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Use a G-List"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Use a Group"

        '    'jrs 2012/05/28
        '    cswmGroup1 = cswmWebMenu.Groups.Add()
        '    cswmGroup1.Caption = "Analysis"
        '    cswmGroup1.ID = "mnuAnalysis"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Statistics"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Relationships"
        '    cswmItem.ID = "mnuAnalysisRelationships"

        '    Dim cswmGroup2 As Coalesys.WebMenu.Group
        '    cswmGroup2 = cswmItem.AddGroup()




        '    'jrs 2012/03/15
        '    cswmGroup1 = cswmWebMenu.Groups.Add()
        '    cswmGroup1.Caption = "Bubble Diameter"
        '    cswmGroup1.ID = "mnuBD"

        '    If Session("sVarZ") > "" Then
        '        cswmItem = cswmGroup1.Items.Add()
        '        cswmItem.Caption = Session("sVarZ")
        '        cswmItem.ID = "mnuBDOther"
        '        If Session("BDCheck") = Nothing Then
        '            Session("BDCheck") = "Other"
        '        End If
        '        If Session("BDCheck") = "Other" Then
        '            cswmItem.IconImage = "checkbox.gif"
        '        Else
        '            cswmItem.IconImage = "blank.gif"
        '        End If
        '    ElseIf Session("BDCheck") Is Nothing Then
        '        Session("BDCheck") = "None"
        '    End If

        '    If Session("sVarZ") <> "POP" Then
        '        cswmItem = cswmGroup1.Items.Add()
        '        cswmItem.Caption = "POP"
        '        cswmItem.ID = "mnuBDPOP"
        '        If Session("BDCheck") = "POP" Then
        '            cswmItem.IconImage = "checkbox.gif"
        '        Else
        '            cswmItem.IconImage = "blank.gif"
        '        End If
        '    End If

        '    If Session("sVarZ") <> "GDP" Then
        '        cswmItem = cswmGroup1.Items.Add()
        '        cswmItem.Caption = "GDP"
        '        cswmItem.ID = "mnuBDGDP"
        '        If Session("BDCheck") = "GDP" Then
        '            cswmItem.IconImage = "checkbox.gif"
        '        Else
        '            cswmItem.IconImage = "blank.gif"
        '        End If
        '    End If

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "None"
        '    cswmItem.ID = "mnuBDNone"
        '    If Session("BDCheck") = "None" Then
        '        cswmItem.IconImage = "checkbox.gif"
        '    Else
        '        cswmItem.IconImage = "blank.gif"
        '    End If

        'Else
        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Scaling"
        'cswmGroup1.ID = "Group_Scaling"


        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.IconHeight = 16
        'cswmItem.Caption = "Common"
        'cswmItem.IconHeight = 16

        'If Session("PE_CommonMenu") <> "Common" Then
        '    cswmItem.IconImage = "blank.gif"
        'Else
        '    cswmItem.IconImage = "checkbox.gif"
        'End If

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Scale"
        'cswmItem.IconHeight = 16
        'If Session("PE_CommonMenu") <> "Common" Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If


        'End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Category Numbers"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Define Category Numbers"



        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Options"

        'jrs 2015/10/28 not necessary once you call this procedure after setting the flag DivValue in the load of the form 
        'it used to be called before it was set
        ''JAM 2012-12-24 Data initialization
        'If Not Page.IsPostBack Then
        '    Session("DivValue") = True
        'End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "X Values"

        Dim cswmGroup3 As Coalesys.WebMenu.Group
        cswmGroup3 = cswmItem.AddGroup()

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "In Values"
        'jrs 2015/10/28 this flag was backwards, so added NOT
        'JAM 2012-10-06
        If Not Session("DivValue") Then
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If


        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "In Percentage"
        'jrs 2015/10/28 this flag was backwards, so removed NOT
        'JAM 2012-10-06
        If Session("DivValue") Then
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
        Else
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
        End If

        'JAM 2012-12-24 Data initialization
        If Not Page.IsPostBack Then
            Session("Ypopulation") = True
        End If

        'JAM 2012-12-13
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Y Values"

        Dim cswmGroup4 As Coalesys.WebMenu.Group
        cswmGroup4 = cswmItem.AddGroup()

        cswmItem = cswmGroup4.Items.Add()
        cswmItem.Caption = "% of Population"

        If Session("Ypopulation") Then
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
        Else
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
        End If


        cswmItem = cswmGroup4.Items.Add()
        cswmItem.Caption = "% of Countries"

        If Not Session("Ypopulation") Then
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
        Else
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
        End If





        '''''''''JAM 2012-12-29 Adding Order Detail Options''''''''''''
        If Not Page.IsPostBack Then
            If Session("OrdAlp") Is Nothing Then 'JAM 2013-02-25
                Session("OrdAlp") = False
            End If

        End If

        'JAM 2012-12-13
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Order Detail"

        Dim cswmGroup5 As Coalesys.WebMenu.Group
        cswmGroup5 = cswmItem.AddGroup()



        cswmItem = cswmGroup5.Items.Add()
        cswmItem.Caption = "Alphabetical for Regions Name"

        If Session("OrdAlp") Then
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
        Else
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
        End If


        cswmItem = cswmGroup5.Items.Add()
        cswmItem.Caption = "Value for Chosen Variable"

        If Not Session("OrdAlp") Then
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/08/22
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
        End If




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'JAM 2013-02-02 Adding hide detail option
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Hide Detail Graph"
        'cswmItem.ID = "mnuHideDetail"
        'cswmGroup1.OnMenuBarClick = "location.href='" & Replace(Replace(Request.Url.ToString, "&showdetail=S", ""), "?showdetail=S", "") & "';"

        'Dim urlo As String
        'urlo = Replace(Replace(Request.Url.ToString, "&showdetail=S", ""), "?showdetail=S", "")




        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"

        'JAM 2012-09-15 
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display/Run Horizon"

        ''JAM 2013-01-19 View graph detail
        'If Not Page.IsPostBack Then
        '    Session("ViewDetail") = False
        'End If

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "View Graph Detail"

        'If Session("ViewDetail") Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If

        'JAM 2012-09-02 Adding Year Options
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Year Options"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Advance"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regress"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Go To Year"




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Use Alias for Group Names"
        'If Session("bAlias") Then
        '    cswmItem.IconImage = "checkbox.gif"
        'End If
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Format Legend Labels"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Format Legend Labels First Dimension Only"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Format Legend Labels Second Dimension Only"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Format Legend Labels Third Dimension Only"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Format Legend Labels Dimensions Only"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Format Legend Labels No Dimensions"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Format Legend Labels Scenario Names"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Format Legend Labels Reset"

        'jrs 2012/03/20
        'If Session("GType") = "LINE" Then
        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Display Vertical Line"

        '    Dim cswmGroup2 As Coalesys.WebMenu.Group
        '    cswmGroup2 = cswmItem.AddGroup()

        '    cswmItem = cswmGroup2.Items.Add()
        '    cswmItem.Caption = "Base Year"

        '    cswmItem = cswmGroup2.Items.Add()
        '    cswmItem.Caption = "Custom Year"

        '    cswmItem = cswmGroup2.Items.Add()
        '    cswmItem.Caption = "Hide Vertical Line"
        'End If

        'If Session("GType") = "SCAT" Then
        '    cswmGroup1 = cswmWebMenu.Groups.Add()
        '    cswmGroup1.Caption = "Swap Variables in Y-X"

        '    cswmItem = cswmGroup1.Items.Add()
        '    cswmItem.Caption = "Swap"
        'End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16031);"
        'jrs 2014/09/03 cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Help Web site"
        cswmItem.OnClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain Histogram"
        Dim msg As String
        msg = "The histogram analyzes all countries in IFs for the lowest and highest values (across all forecast years) of the variable displayed and divides the range between lowest and highest into equal categories (10 categories is the default).  It then identifies the countries that fall into each category (clicking on a bar will provide the list of countries in the category).  In the default case, the sum of the population of the countries in each category is shown in the bars as a percentage of world population.   Under Display Options, the Y-axis value can be changed to % of countries to see the percentage of countries in each category.  Also under Display Options the X-axis value can be changed from Values across the range to Percentiles across the range."
        cswmItem.OnClick = "alert('" & msg & "');"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'jam 2021/08/22
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'jrs 2012/03/19
        'Dim sOther As String = ""
        'If Session("sVarz") > "" Then
        '    sOther = Session("sVarz")
        'End If

        'Choose the correct function
        Select Case objEA.Item.Caption
                'Case "Continue"
                '    mnuContinue_Click(sender, e)
            Case "Save", "Print"
                    'Case "Common", "Scale" '"Delete Selected Driver/Dimension"
                    '    mnuCommon_Click(sender, e)
            Case "Define Category Numbers"
                mnuPercentiles_Click()
            Case "Advance"
                mnuAdvance_Click(sender, e)
            Case "Regress"
                mnuRegress_Click(sender, e)
            Case "Go To Year"
                mnuGoToYear_Click()
            Case "In Values"
                mnuinvalues_Click()
                'JAM 2012-10-09
                If Session("mnuinvaluesChecked") Then
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("X Values").Group.Items("In Percentage").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                    cswmWebMenu.Groups("Display Options").Items("X Values").Group.Items("In Values").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
                Else
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("X Values").Group.Items("In Values").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                End If
            Case "In Percentage"
                mnuinpercentage_Click()
                'JAM 2012-10-09
                If Session("mnuinpercentageChecked") Then
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("X Values").Group.Items("In Percentage").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
                    cswmWebMenu.Groups("Display Options").Items("X Values").Group.Items("In Values").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                Else
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("X Values").Group.Items("In Percentage").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                End If
            Case "% of Population"
                mnuPerPopulation_Click()
                If Session("Ypopulation") Then
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("Y Values").Group.Items("% of Population").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
                    cswmWebMenu.Groups("Display Options").Items("Y Values").Group.Items("% of Countries").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                Else
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("Y Values").Group.Items("% of Population").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                End If
            Case "% of Countries"
                mnuPerCountries_Click()
                If Not Session("Ypopulation") Then
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("Y Values").Group.Items("% of Population").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                    cswmWebMenu.Groups("Display Options").Items("Y Values").Group.Items("% of Countries").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
                Else
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("Y Values").Group.Items("% of Countries").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                End If
                    'Case "View Graph Detail"    'JAM 2013-01-19 Adding graph detail
                    '    mnuGraphDetail_Click()
                    '    If Session("ViewDetail") Then
                    '        cswmWebMenu.Groups("Display Options").Items("View Graph Detail").IconImage = "checkbox.gif"
                    '    Else
                    '        cswmWebMenu.Groups("Display Options").Items("View Graph Detail").IconImage = "blank.gif"
                    '    End If
            Case "Alphabetical for Regions Name"      'JAM 2012-12-29 Order Detail
                mnuAlphabetical_Click()
                If Session("OrdAlp") Then
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("Order Detail").Group.Items("Alphabetical for Regions Name").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
                    cswmWebMenu.Groups("Display Options").Items("Order Detail").Group.Items("Value for Chosen Variable").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                Else
                    cswmWebMenu.Groups("Display Options").Items("Order Detail").Group.Items("Alphabetical for Regions Name").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                End If
            Case "Value for Chosen Variable"
                mnuChosenVariable_Click()
                If Not Session("OrdAlp") Then
                    'jam 2021/08/22
                    cswmWebMenu.Groups("Display Options").Items("Order Detail").Group.Items("Alphabetical for Regions Name").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                    cswmWebMenu.Groups("Display Options").Items("Order Detail").Group.Items("Value for Chosen Variable").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
                Else
                    cswmWebMenu.Groups("Display Options").Items("Order Detail").Group.Items("Value for Chosen Variable").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
                End If
            Case "Hide Detail Graph" 'JAM 2013-02-02
                mnuHideDetailGraph_Click()
            Case "Set Title, Display Interval, or Year" 'jrs 2005/10/19
                Session("DisplayControlParent") = "frm_Histogram.aspx"

                'jam 2021/08/22
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_DisplayControl.aspx")


                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_DisplayControl.aspx")
                vurl = FriendlyUrl.Href("~/frm_DisplayControl")
                Response.Redirect(vurl)


            Case "Display/Run Horizon" 'jrs 2011/07/04
                mnuDisplayRunHorizon_Click(sender, e)
            Case "Use Alias for Group Names" 'jrs 2011/07/04
                mnuAlias_Click()
            Case "Format Legend Labels"
                Session("GraphLabelsParent") = "frm_Histogram.aspx"
                'jam 2021/08/22
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_GraphLabels.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_GraphLabels.aspx")
                vurl = FriendlyUrl.Href("~/frm_GraphLabels")
                Response.Redirect(vurl)


            Case "Format Legend Labels First Dimension Only" 'jrs 2008/01/21
                mnuFormLeg1stDim_Click()
            Case "Format Legend Labels Second Dimension Only" 'jrs 2007/08/10
                mnuFormLeg2ndDim_Click()
            Case "Format Legend Labels Third Dimension Only" 'jrs 2008/01/24
                mnuFormLeg3rdDim_Click()
            Case "Format Legend Labels Dimensions Only" 'jrs 2008/01/24
                mnuFormLegDims_Click()
            Case "Format Legend Labels No Dimensions" 'jrs 2008/01/24
                mnuFormLegNoDim_Click()
            Case "Format Legend Labels Scenario Names"
                mnuFormLegSce_Click()
            Case "Format Legend Labels Reset"
                mnuFormLegReset_Click()
                    'Case "Swap"
                    '    mnuSwapVars_Click()
            Case "Use a G-List" 'jrs 2012/03/12
                mnuUseGList_Click()
            Case "Use a Group"
                mnuUseGroup_Click()
                    'Case "POP" 'jrs 2012/03/16
                    '    mnuBDPOP_Click()
                    'Case "GDP" 'jrs 2012/03/19
                    '    mnuBDGDP_Click()
                    'Case sOther
                    '    mnuBDOther_Click()
                    'Case "None"
                    '    mnuBDNone_Click()
            Case "Base Year"
                Session("VLYear") = FirstYearForecast
            Case "Custom Year"
                mnuVLine_Click()
            Case "Hide Vertical Line"
                Session.Remove("VLYear")

            Case "Turn formulation display off" 'jrs 2012/05/31
                Session("TableTitle") = ""
                'Case "Statistics" 'jrs 2012/06/04
                '    mnuStatistics_Click()
        End Select
    End Sub

    Private Sub mnuAlias_Click()
        Dim i As Integer
        If Not Session("bAlias") Then
            Session("bAlias") = True
            If cswmWebMenu.Groups.Count > 0 Then
                'jam 2021/08/22
                cswmWebMenu.Groups("Display Format").Items("Use Alias for Group Names").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
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

    'Procedure to convert to XML format and save to JavaScript for AnyChart
    Private Sub PackJS()
        'Dim sJscript As String
        'sJscript = "function onPointClick(e) {" & vbCr
        ''JAM 2012-12-27 Adding selected value
        'sJscript = sJscript & "document.forms[0].vselectedseries.value=e.data.Index" & vbCr

        'sJscript = sJscript & "}" & vbCr


        'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadJS", _
        'sJscript, True)


        'jrs 2011/09/20
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        If Session("TableTitle") = "" Or Session("TableTitle") = "Title" Then
            Call BuildTitle()   'JAM 2012-09-09
            If IsNumeric((Session("FirstYear") + Session("PYear") - 1)) Then
                Session("TableTitle") = CStr((Session("FirstYear") + Session("PYear") - 1))
            Else
                Session("TableTitle") = (Session("FirstYear") + Session("PYear") - 1)
            End If
        End If
        Dim VarTitle As String
        VarTitle = CStr(Session("Title"))
        If InStr(VarTitle, "[") <> 0 Then
            VarTitle = VarTitle.Substring(0, InStr(VarTitle, "[") - 1)
        End If

        sJscript2 = sJscript2 & "var defaultTitle = '" & VarTitle & "';" & vbCr
        sJscript2 = sJscript2 & "var bDrillDown = false;" & vbCr

        Dim DimCount As Integer = 0
        If Session("VarsCount") <> 0 Then
            DimCount = Session("COUNTE") / Session("VarsCount")
        End If
        Dim sAxisXLabel As String
        sAxisXLabel = getXAxisLabel(DimCount + 1)
        Dim sAxisYLabel As String
        sAxisYLabel = getYAxisLabel()

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/10/07 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''


        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/11 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "type: 'column'," & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        sJscript2 = sJscript2 & "events: {" & vbCr
        sJscript2 = sJscript2 & "drilldown: function(e) {" & vbCr
        sJscript2 = sJscript2 & "$('#container').highcharts().setTitle({ text: defaultTitle + ' detail for ' + e.point.name });" & vbCr
        sJscript2 = sJscript2 & "   $('#container').highcharts().yAxis[0].axisTitle.attr({text:       'Population'});" & vbCr
        sJscript2 = sJscript2 & "   bDrillDown = true;" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "drillup: function(e) {" & vbCr
        sJscript2 = sJscript2 & "$('#container').highcharts().setTitle({ text: defaultTitle });" & vbCr
        sJscript2 = sJscript2 & "   $('#container').highcharts().yAxis[0].axisTitle.attr({text:       ' " & sAxisYLabel & " '});" & vbCr
        sJscript2 = sJscript2 & "   bDrillDown = false;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        If Session("TableTitle") > "" And Session("TableTitle") <> "Title" Then
            sJscript2 = sJscript2 & "text:       '" & Session("Title") & "'," & vbCr
        Else
            sJscript2 = sJscript2 & "text:       ''," & vbCr
        End If

        'jam 2025/10/07  Increasing font size for legends
        sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '30px'}," & vbCr
        ''''''''''''''''''''''''''''


        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Session("TableTitle") > "" And Session("TableTitle") <> "Title" Then
            sJscript2 = sJscript2 & "subtitle: {" & vbCr
            sJscript2 = sJscript2 & "text:       """ & Session("TableTitle") & """," & vbCr

            'jam 2025/10/07  Increasing font size for legends
            sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '25px', color: '#333333'}," & vbCr

            sJscript2 = sJscript2 & "x: -20 //center" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "var sTooltip = '';" & vbCr
        sJscript2 = sJscript2 & "if (bDrillDown){" & vbCr
        If VarTitle <> "POP" Then
            sJscript2 = sJscript2 & "sTooltip = '<b>" & VarTitle & ", ' + this.point.name + ':   ' + this.point.myData + '<br/><b>' + $('#container').highcharts().yAxis[0].axisTitle.textStr + ':   ' + this.point.y + '</b>';" & vbCr
        Else
            sJscript2 = sJscript2 & "sTooltip = '<b>" & VarTitle & ", ' + this.point.name + ':   ' + this.point.myData + '<br/></b>';" & vbCr
        End If
        sJscript2 = sJscript2 & "} else {" & vbCr
        sJscript2 = sJscript2 & "sTooltip = '<b>" & VarTitle & ":   ' + this.point.name + '<br/><b>' + $('#container').highcharts().yAxis[0].axisTitle.textStr + ':   ' + this.point.y + '</b>';}" & vbCr
        sJscript2 = sJscript2 & "return sTooltip;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "xAxis: {" & vbCr

        'jam 2025/10/13 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr


        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/10/13 Increasing font for x axis
        'sJscript2 = sJscript2 & "fontSize: '14px'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr



        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & ", type: 'category'"
        sJscript2 = sJscript2 & "}," & vbCr

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
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/10/13 Increasing font for y axis
        'sJscript2 = sJscript2 & "fontSize: '14px'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr
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
        sJscript2 = sJscript2 & "enabled:     false"
        sJscript2 = sJscript2 & "}," & vbCr

        '    sJscript2 = sJscript2 & "plotOptions: {" & vbCr
        '    sJscript2 = sJscript2 & "   series: {" & vbCr
        '    sJscript2 = sJscript2 & "       point: {" & vbCr
        '    sJscript2 = sJscript2 & "           events: {" & vbCr
        '    sJscript2 = sJscript2 & "               click: function () {" & vbCr
        '    sJscript2 = sJscript2 & "                   this.options.dataLabels.enabled = !this.options.dataLabels.enabled;" & vbCr
        '    sJscript2 = sJscript2 & "                   this.update();" & vbCr
        '    sJscript2 = sJscript2 & "               }" & vbCr
        '    sJscript2 = sJscript2 & "           }" & vbCr
        '    sJscript2 = sJscript2 & "       }" & vbCr
        '    sJscript2 = sJscript2 & "   }" & vbCr
        '    sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr
        sJscript2 = sJscript2 & PlotColToJSSeries()
        sJscript2 = sJscript2 & "]," & vbCr
        sJscript2 = sJscript2 & "drilldown: {" & vbCr
        sJscript2 = sJscript2 & "series: [" & vbCr
        For i = 1 To Session("gpercentiles")
            If i > 1 Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & PlotColToJSSeriesDrillDown(i - 1)
        Next
        sJscript2 = sJscript2 & "]" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        ' jam 2024/08/17 Bug fix related to GET parameters
        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr


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

        'JAM 2012-12-29
        If IsNumeric(Session("vselected")) Then
            If Session("vselected") < Session("gpercentiles") Then
                Session("vselected") = vselectedseries.Text
            Else
                Session("vselected") = 0
            End If

        Else
            Session("vselected") = 0
        End If

    End Sub

    Private Function PlotColToJSSeries() As String

        Dim sJscript As String = ""
        sJscript = sJscript & "{name: 'Histogram', " & vbCr

        Dim sColor As String = "Blue"
        sJscript = sJscript & "color: '" & sColor & "', " & vbCr

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim Diff As Single, Min As Single, Max As Single
        Min = 10 ^ 10
        Max = -10 ^ 10
        Dim iNumDec As Integer = 0

        If IsNumeric(Session("YData")(0, 0)) Then
            If Math.Abs(Session("YData")(0, 0)) < 1 Then
                iNumDec = 3
            ElseIf Math.Abs(Session("YData")(0, 0)) < 10 Then
                iNumDec = 2
            ElseIf Math.Abs(Session("YData")(0, 0)) < 100 Then
                iNumDec = 1
            End If
        Else
            iNumDec = 2
        End If

        Dim i As Integer
        For i = 1 To Session("gpercentiles")
            If i > 1 Then
                sJscript = sJscript & "," & vbCr
            End If
            If IsNumeric(Session("YData")(0, i - 1)) Then
                If Session("YData")(0, i - 1) > -99999 Then
                    sJscript = sJscript & "{name: '" & CStr(Session("PointLabels")(i - 1)) & "', y: "
                    sJscript = sJscript & CStr(Math.Round(Session("YData")(0, i - 1), iNumDec)) & ", drilldown: '"
                    sJscript = sJscript & CStr(Session("PointLabels")(i - 1)) & "'}"
                End If
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Private Function PlotColToJSSeriesDrillDown(ByVal idx As Integer) As String

        Dim sJscript As String = ""
        sJscript = sJscript & "{name: '" & CStr(Session("PointLabels")(idx)) & "', id: '" & CStr(Session("PointLabels")(idx)) & "', " & vbCr

        Dim sColor As String = "Green"
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

        If IsNumeric(Session("gcategories")(idx, 0, 1)) Then
            If Math.Abs(Session("gcategories")(idx, 0, 1)) < 1 Then
                iNumDec = 3
            ElseIf Math.Abs(Session("gcategories")(idx, 0, 1)) < 10 Then
                iNumDec = 2
            ElseIf Math.Abs(Session("gcategories")(idx, 0, 1)) < 100 Then
                iNumDec = 1
            End If
        Else
            iNumDec = 2
        End If

        Dim i As Integer
        For i = 1 To Session("counters")(idx)
            If i > 1 Then
                sJscript = sJscript & "," & vbCr
            End If
            If IsNumeric(Session("gcategories")(idx, i - 1, 1)) Then
                sJscript = sJscript & "{name: """ & CStr(Session("gcategories")(idx, i - 1, 0)) & """, y: "
                sJscript = sJscript & CStr(Math.Round(Session("gcategories")(idx, i - 1, 1), iNumDec)) & ", myData: "
                sJscript = sJscript & CStr(Math.Round(Session("gcategories")(idx, i - 1, 3), iNumDec)) & "}"
            Else
                sJscript = sJscript & "{name: """ & CStr(Session("gcategories")(idx, i - 1, 0)) & """, y: 0, myData: "
                sJscript = sJscript & CStr(Math.Round(Session("gcategories")(idx, i - 1, 3), iNumDec)) & "}"
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToJSSeriesDrillDown = sJscript
    End Function

    'Public Sub mnuCommon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    Dim objSender As Coalesys.WebMenu.WebMenu
    '    objSender = eventSender

    '    Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
    '    objEA = eventArgs

    '    If objEA.Item.Caption = "Common" Then
    '        objSender.Groups("Scaling").Items("Common").IconImage = "checkbox.gif"
    '        objSender.Groups("Scaling").Items("Scale").IconImage = "blank.gif"
    '        Session("CommonPlot") = "True"
    '    Else  'Scale
    '        objSender.Groups("Scaling").Items("Scale").IconImage = "checkbox.gif"
    '        objSender.Groups("Scaling").Items("Common").IconImage = "blank.gif"
    '        Session("CommonPlot") = "False"
    '    End If

    'End Sub

    ''jrs 2011/07/04
    Public Sub mnuDisplayRunHorizon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'jam 2021/08/22
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        If CBool(Session("HistoricBase")) = True Then
            Msg = "You cannot change the horizon for historic analysis."

            LoadMessage(Msg, Session("Message"))

            'jam 2021/08/22
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")


            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
            vurl = FriendlyUrl.Href("~/frm_Message")
            Response.Redirect(vurl)



        End If


        cmb_Horizon.Visible = True
        frm_Horizon.Visible = True
        cmb_Horizon.Items.Clear()
        cmb_Horizon.Items.Add("")
        cmb_Horizon.Items.Add(New ListItem(Session("FirstYear").ToString))

        For ICount = Session("FirstYear") + Session("Interval") To Session("LastYearForecast") Step Session("Interval") 'jrs 2005/12/11 added Session("Interval")
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

        'JAM 2012-09-02 Review Variables
        'ShowSelection = False
        'Ypopulation = True
        'JAM 2013-02-19 commented
        'Call FillData()

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

        'jam 2021/06/19
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Ind = cmb_Horizon.SelectedIndex - 1
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If
        If Ind >= 1 Then
            Session("DisplayLastYear") = Session("FirstYear") + Ind * Session("Interval")
        Else
            Session("DisplayLastYear") = Val(cmb_Horizon.SelectedValue)
            If Session("FirstYear") = FirstYearForecast Then
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = Session("LastYear")
                    LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))

                    'jam 2021/08/22
                    'Support.FormManager.StateStore(Me)
                    'Support.FormManager.Show(Me, "frm_Message.aspx")


                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_Report.aspx")
                    vurl = FriendlyUrl.Href("~/frm_Message")
                    Response.Redirect(vurl)


                End If

            ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = Session("LastYear")
                    LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                    'jam 2021/08/22
                    'Support.FormManager.StateStore(Me)
                    'Support.FormManager.Show(Me, "frm_Message.aspx")

                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_Report.aspx")
                    vurl = FriendlyUrl.Href("~/frm_Message")
                    Response.Redirect(vurl)



                End If

            End If
        End If
        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False
        'jrs 2011/08/26
        Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1

        'JAM 2013-02-19
        FillData()
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
        'Session("RegressType") = ""
        Session("TableTitle") = ""
        'JAM 2012-09-02 Review Variables
        'ShowSelection = False
        'Ypopulation = True
        Call FillData()

        'JAM 2013-02-05
        PackJS()

        'DisplayGraph()
        lblJS.Visible = True



    End Sub

    Public Sub mnuRegress_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("PYear") = Session("PYear") - 5
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
        'jrs 2012/05/30
        'Session("RegressType") = ""
        Session("TableTitle") = ""
        'JAM 2012-09-02 Review Variables
        'ShowSelection = False
        'Ypopulation = True
        Call FillData()

        'JAM 2013-02-05
        PackJS()
        lblJS.Visible = True
        'DisplayGraph()

    End Sub

    'Private Sub ColorDistribution()
    '    Dim DimCount As Integer = 0
    '    If Session("VarsCount") <> 0 Then
    '        DimCount = Session("COUNTE") / Session("VarsCount")
    '    End If
    '    'For All Regions do the same color for region
    '    If DimCount = NR Then
    '        ColorDistributionNR()
    '        Exit Sub
    '    End If

    '    'jrs 2010/09/28
    '    'Distribute Colors
    '    Dim GreenIdx As Integer, OrangeIdx As Integer, BlueIdx As Integer
    '    Dim YellowIdx As Integer, NavyBlueIdx As Integer, RedIdx As Integer
    '    Dim i As Integer
    '    GreenIdx = 0
    '    OrangeIdx = 1
    '    BlueIdx = 2
    '    YellowIdx = 3
    '    NavyBlueIdx = 4
    '    RedIdx = 5

    '    'jrs 2010/09/14 Load Colors
    '    Dim RColor() As Integer, GColor() As Integer, BColor() As Integer
    '    ReDim RColor(AMAX(DimCount, 10))
    '    ReDim GColor(AMAX(DimCount, 10))
    '    ReDim BColor(AMAX(DimCount, 10))
    '    'Green for Africa 0, 150, 0
    '    RColor(GreenIdx) = 0
    '    GColor(GreenIdx) = 150
    '    BColor(GreenIdx) = 0
    '    'Orange for Asia 255, 102, 0
    '    RColor(OrangeIdx) = 255
    '    GColor(OrangeIdx) = 102
    '    BColor(OrangeIdx) = 0
    '    'Blue for Europe 0, 0, 150
    '    RColor(BlueIdx) = 0
    '    GColor(BlueIdx) = 0
    '    BColor(BlueIdx) = 150
    '    'Yellow for Latam&Car 255, 255, 0
    '    RColor(YellowIdx) = 255
    '    GColor(YellowIdx) = 255
    '    BColor(YellowIdx) = 0
    '    'Navy Blue for North America 0, 191, 255
    '    RColor(NavyBlueIdx) = 0
    '    GColor(NavyBlueIdx) = 191
    '    BColor(NavyBlueIdx) = 255
    '    'Red for Oceania 255, 0, 0
    '    RColor(RedIdx) = 255
    '    GColor(RedIdx) = 0
    '    BColor(RedIdx) = 0
    '    'brown 130, 43, 0
    '    RColor(6) = 130
    '    GColor(6) = 43
    '    BColor(6) = 0
    '    'magenta 128, 0, 128
    '    RColor(7) = 128
    '    GColor(7) = 0
    '    BColor(7) = 128
    '    'pink 255, 153, 204
    '    RColor(8) = 255
    '    GColor(8) = 153
    '    BColor(8) = 204
    '    'gray 128, 128, 128
    '    RColor(9) = 128
    '    GColor(9) = 128
    '    BColor(9) = 128
    '    'Load randomly the remaining NR - 10 colors
    '    For i = 10 To UBound(RColor)
    '        RColor(i) = (Rnd() * 1000) Mod 256
    '        GColor(i) = (Rnd() * 1000) Mod 256
    '        BColor(i) = (Rnd() * 1000) Mod 256
    '    Next i

    '    Session("RColor") = RColor
    '    Session("GColor") = GColor
    '    Session("BColor") = BColor
    'End Sub

    'Private Sub ColorDistributionNR()
    '    'jrs 2010/09/28
    '    'Distribute Colors
    '    Dim sql As String
    '    Dim rs As DataTable
    '    sql = "SELECT GroupName, RegionNo " & _
    '    "From GListId, GList, " & GroupIDNew & ", " & Grouping & _
    '    " WHERE GListId.Code = GList.GListCode And " & _
    '    "GList.Region = " & GroupIDNew & ".GroupName AND " & _
    '    GroupIDNew & ".GroupNum = " & Grouping & ".GroupNo AND " & _
    '    "GListId.Description = 'UN Regions'"

    '    SetDataTablefromAccess(Session("IFs_MDB2"), sql, "", rs)
    '    Dim sGroupNames() As String
    '    ReDim sGroupNames(NR)
    '    Dim i As Integer = 0

    '    Do While i < rs.Rows.Count
    '        sGroupNames(rs.Rows(i).Item("RegionNo")) = rs.Rows(i).Item("GroupName")
    '        i = i + 1
    '    Loop

    '    Dim RColor() As Integer, GColor() As Integer, BColor() As Integer
    '    'jrs 2010/09/14 Load Colors
    '    ReDim RColor(NR)
    '    ReDim GColor(NR)
    '    ReDim BColor(NR)

    '    For i = 1 To NR
    '        If InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "AFRICA") > 0 Then
    '            'Green for Africa 0, 150, 0
    '            RColor(i - 1) = 0
    '            GColor(i - 1) = 150
    '            BColor(i - 1) = 0
    '        ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "ASIA") > 0 Then
    '            'Orange for Asia 255, 102, 0
    '            RColor(i - 1) = 255
    '            GColor(i - 1) = 102
    '            BColor(i - 1) = 0
    '        ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "EUROPE") > 0 Then
    '            'Blue for Europe 0, 0, 150
    '            RColor(i - 1) = 0
    '            GColor(i - 1) = 0
    '            BColor(i - 1) = 150
    '        ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "LATAM") > 0 Then
    '            'Yellow for Latam&Car 255, 255, 0
    '            RColor(i - 1) = 255
    '            GColor(i - 1) = 255
    '            BColor(i - 1) = 0
    '        ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "NORTH") > 0 Then
    '            'Navy Blue for North America 0, 191, 255
    '            RColor(i - 1) = 0
    '            GColor(i - 1) = 191
    '            BColor(i - 1) = 255
    '        ElseIf InStr(UCase(sGroupNames(IFsRegs.Item(Session("GR1N")(i)).Index)), "OCEANIA") > 0 Then
    '            'Red for Oceania 255, 0, 0
    '            RColor(i - 1) = 255
    '            GColor(i - 1) = 0
    '            BColor(i - 1) = 0
    '        Else
    '            'Random if not found
    '            RColor(i - 1) = (Rnd() * 1000) Mod 256
    '            GColor(i - 1) = (Rnd() * 1000) Mod 256
    '            BColor(i - 1) = (Rnd() * 1000) Mod 256
    '        End If
    '    Next i

    '    Session("RColor") = RColor
    '    Session("GColor") = GColor
    '    Session("BColor") = BColor
    'End Sub

    'jrs 2010/10/22
    '        Private Sub mnuSwapVars_Click()
    '            On Error GoTo Hdlr
    '            'Copy source
    '            Dim pCopy As Object, VARNCopy As Object, gr2Copy As Object, _
    '            gr3Copy As Object, gr4Copy As Object, fCopy As Object, _
    '            FileNumbrCopy As Object, FinYearCopy As Object, FinYearHisCopy As Object, _
    '            bNotDecomposablesCopy As Object, bLineFeedsCopy As Object, _
    '            sGroupTitlesCopy As Object, sUsrDefCatsCopy As Object, DispDecimalsCopy As Object
    '            Dim FDNG2Copy As Object, FDNG3Copy As Object, FDNG4Copy As Object

    '            pCopy = VB6.CopyArray(Session("PLOT"))
    '            VARNCopy = VB6.CopyArray(Session("VARN"))
    '            gr2Copy = VB6.CopyArray(Session("GR2N"))
    '            gr3Copy = VB6.CopyArray(Session("GR3N"))
    '            gr4Copy = VB6.CopyArray(Session("GR4N"))
    '            fCopy = VB6.CopyArray(Session("aFormulas"))

    '            FDNG2Copy = VB6.CopyArray(Session("FDNG2"))
    '            FDNG3Copy = VB6.CopyArray(Session("FDNG3"))
    '            FDNG4Copy = VB6.CopyArray(Session("FDNG4"))

    '            FileNumbrCopy = VB6.CopyArray(Session("FileNumbr"))
    '            FinYearCopy = VB6.CopyArray(Session("FinYear"))
    '            FinYearHisCopy = VB6.CopyArray(Session("FinYearHis"))
    '            'Reset Target
    '            Dim j As Integer, k As Integer, i As Integer
    '            Dim focus As Integer, DimCount As Integer = 0
    '            If Session("VarsCount") <> 0 Then
    '                DimCount = Session("COUNTE") / Session("VarsCount")
    '            End If
    '            For i = 1 To DimCount * 2
    '                'source index
    '                If i <= DimCount Then
    '                    k = i + DimCount
    '                Else
    '                    k = i - DimCount
    '                End If
    '                'copy to target
    '                For j = 0 To UBound(Session("PLOT"), 1)
    '                    Session("PLOT")(j, i) = pCopy(j, k)
    '                Next j

    '                For j = 0 To UBound(Session("VARN"), 1)
    '                    Session("VARN")(j, i) = VARNCopy(j, k)
    '                Next j
    '                Session("GR2N")(i) = gr2Copy(k)
    '                Session("GR3N")(i) = gr3Copy(k)
    '                Session("GR4N")(i) = gr4Copy(k)
    '                If k <= UBound(fCopy) Then
    '                    If i > UBound(Session("aFormulas")) Then
    '                        ReDim Preserve Session("aFormulas")(i)
    '                    End If
    '                    Session("aFormulas")(i) = fCopy(k)
    '                End If
    '                Session("FDNG2")(i) = FDNG2Copy(k)
    '                Session("FDNG3")(i) = FDNG3Copy(k)
    '                Session("FDNG4")(i) = FDNG4Copy(k)
    '                Session("FileNumbr")(i) = FileNumbrCopy(k)
    '                Session("FinYear")(i) = FinYearCopy(k)
    '                Session("FinYearHis")(i) = FinYearHisCopy(k)
    '            Next i

    '            'save original name to display
    '            ReDim Session("oVARN0")(Session("COUNTE"))
    '            For i = 1 To Session("COUNTE")
    '                Session("oVARN0")(i) = Session("VARN")(0, i)
    '            Next i
    '            'jrs 2010/02/04
    '            ReDim Session("oGR2N")(Session("COUNTE"))
    '            ReDim Session("oGR3N")(Session("COUNTE"))
    '            ReDim Session("oGR4N")(Session("COUNTE"))
    '            For i = 1 To Session("COUNTE")
    '                Session("oGR2N")(i) = Session("GR2N")(i)
    '                Session("oGR3N")(i) = Session("GR3N")(i)
    '                Session("oGR4N")(i) = Session("GR4N")(i)
    '            Next i

    '            'jrs 2012/05/30
    '            Session("RegressType") = ""
    '            Session("TableTitle") = ""

    '            Exit Sub

    'Hdlr:
    '            LoadMessage(Err.Description, Session("Message"))
    '            Support.FormManager.StateStore(Me)
    '            Support.FormManager.Show(Me, "frm_Message.aspx")
    '        End Sub

    'jrs 2012/03/09
    Protected Sub cmdGoToYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGoToYear.Click
        'jam 2021/08/22
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If Not IsNumeric(txtGoToYear.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
            'jam 2021/08/22
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")


            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
            vurl = FriendlyUrl.Href("~/frm_Message")
            Response.Redirect(vurl)


        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                'jam 2021/08/22
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")


                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
                vurl = FriendlyUrl.Href("~/frm_Message")
                Response.Redirect(vurl)


            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))

                'jam 2021/08/22
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
                vurl = FriendlyUrl.Href("~/frm_Message")
                Response.Redirect(vurl)


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

        'JAM 2013-02-19
        FillData()

        lblJS.Visible = True
        cswmWebMenu.Visible = True
        'jrs 2012/05/30
        'Session("RegressType") = ""
        Session("TableTitle") = ""

        PackJS()

    End Sub

    'jrs 2012/03/21
    'Protected Sub cmdVLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVLine.Click

    '    If Not IsNumeric(txtVLine.Text) Then
    '        LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
    '        Support.FormManager.StateStore(Me)
    '        Support.FormManager.Show(Me, "frm_Message.aspx")
    '    End If

    '    If Session("FirstYear") = FirstYearForecast Then
    '        If CInt(txtVLine.Text) > Session("LastYearForecast") Or CInt(txtVLine.Text) < FirstYearHistory Then
    '            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
    '            Support.FormManager.StateStore(Me)
    '            Support.FormManager.Show(Me, "frm_Message.aspx")
    '        End If

    '    ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
    '        If CInt(txtVLine.Text) > Session("LastYearForecast") Or CInt(txtVLine.Text) < FirstYearHistory Then
    '            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
    '            Support.FormManager.StateStore(Me)
    '            Support.FormManager.Show(Me, "frm_Message.aspx")
    '        End If

    '    End If

    '    Session("VLYear") = CInt(txtVLine.Text)

    '    frm_VLine.Visible = False
    '    lblJS.Visible = True
    '    cswmWebMenu.Visible = True
    'End Sub

    'jrs 2012/03/09
    'Protected Sub cmdHorizon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorizon.Click
    '    If Not IsNumeric(txtHorizon.Text) Then
    '        LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
    '        Support.FormManager.StateStore(Me)
    '        Support.FormManager.Show(Me, "frm_Message.aspx")
    '    End If

    '    If Session("FirstYear") = FirstYearForecast Then
    '        If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
    '            LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))
    '            Support.FormManager.StateStore(Me)
    '            Support.FormManager.Show(Me, "frm_Message.aspx")
    '        End If

    '    ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
    '        If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
    '            LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
    '            Support.FormManager.StateStore(Me)
    '            Support.FormManager.Show(Me, "frm_Message.aspx")
    '        End If

    '    End If

    '    If Session("Interval") > 1 Then
    '        If (CInt(txtHorizon.Text) - Session("FirstYear")) Mod Session("Interval") <> 0 Then
    '            LoadMessage("Invalid Year, year must follow the existing Interval, please use the combo box.", Session("Message"))
    '            Support.FormManager.StateStore(Me)
    '            Support.FormManager.Show(Me, "frm_Message.aspx")
    '        End If
    '    End If

    '    cmb_Horizon.Text = txtHorizon.Text
    '    cmb_Horizon_SelectedIndexChanged(Me, New EventArgs)
    'End Sub

    Private Sub mnuUseGList_Click()

        'jam 2021/08/22
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Session("IsaGroup") = False

        Session("SelectGeographyParent") = "frm_Histogram.aspx"
        'jam 2021/08/22
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_SelctGeography.aspx")

        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_SelctGeography.aspx")
        vurl = FriendlyUrl.Href("~/frm_SelctGeography")
        Response.Redirect(vurl)




    End Sub

    Private Sub mnuUseGroup_Click()
        'jam 2021/08/22
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Session("IsaGroup") = True

        Session("SelectGeographyParent") = "frm_Histogram.aspx"
        'jam 2021/08/22
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_SelctGeography.aspx")

        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_SelctGeography.aspx")
        vurl = FriendlyUrl.Href("~/frm_SelctGeography")
        Response.Redirect(vurl)


    End Sub

    'Private Sub getVariablesforDec(ByVal PlotPosition As Integer, ByRef sVarName As String, ByRef fileIdx As Integer, ByRef bUsingComp As Boolean)
    '    '1. PlotPos, is the column in the original plot vector where the array is stored.
    '    '2. Variable Name to be retrieved
    '    '3. fileIdx, index of the file to be used
    '    '4. bUsingComp is true if using formulas

    '    'Find variables
    '    Dim pos As Integer
    '    pos = InStr(1, Session("VARN")(1, PlotPosition), "[")
    '    If pos > 0 Then
    '        sVarName = Left(Session("VARN")(1, PlotPosition), pos - 1)
    '        fileIdx = Val(Mid(Session("VARN")(1, PlotPosition), pos + 1, Len(Session("VARN")(1, PlotPosition)) - pos - 1))
    '    Else
    '        sVarName = Session("VARN")(1, PlotPosition)
    '        bUsingComp = (UBound(Session("aFormulas")) >= PlotPosition)
    '        If bUsingComp Then
    '            bUsingComp = Not (Session("aFormulas")(PlotPosition) Is Nothing)
    '            If bUsingComp Then
    '                If Session("aFormulas")(PlotPosition).Name <> Session("VARN")(1, PlotPosition) Then
    '                    bUsingComp = False
    '                End If
    '            End If
    '        End If
    '        If Session("FirstYear") = FirstYearHistory Then
    '            fileIdx = -1
    '        Else
    '            fileIdx = 0
    '            'Handling Files with formulas
    '            If bUsingComp Then
    '                pos = InStr(1, Session("aFormulas")(PlotPosition).disVar.displayDet(1), "[")
    '                fileIdx = Val(Mid(Session("aFormulas")(PlotPosition).disVar.displayDet(1), pos + 1, Len(Session("aFormulas")(PlotPosition).disVar.displayDet(1)) - pos - 1))
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub mnuBDPOP_Click()
    '    If Session("sVarZ") > "" Then
    '        cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = "blank.gif"
    '        If Session("sVarZ") <> "POP" Then
    '            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = "checkbox.gif"
    '        End If
    '        If Session("sVarZ") <> "GDP" Then
    '            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = "blank.gif"
    '        End If
    '        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = "blank.gif"
    '    End If

    '    Session("BDCheck") = "POP"

    '    Session("COUNTE") = pCOUNTE * 3 / 2
    '    Session("VarsCount") = 3
    '    'Load POP
    '    Dim i As Integer, j As Integer
    '    For j = 1 To pCOUNTE / 2
    '        For i = 1 To Session("FinYear")(1)
    '            If IsNumeric(Session("BDPOP")(i, j)) Then
    '                If Session("BDPOP")(i, j) <> -9999 Then
    '                    Session("PLOT")(i, j + pCOUNTE) = Session("BDPOP")(i, j)
    '                Else
    '                    Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
    '                End If
    '            Else
    '                Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
    '            End If
    '        Next i
    '        Session("VARN")(0, j + pCOUNTE) = "POP[" & Session("ShortSce")(Session("fileIdxZ")) & "]"
    '        Session("VARN")(1, j + pCOUNTE) = "POP[" & Session("fileIdxZ") & "]"
    '        Session("GR1N")(j + pCOUNTE) = Session("GR1N")(j)
    '        Session("GR2N")(j + pCOUNTE) = ""
    '        Session("GR3N")(j + pCOUNTE) = ""
    '        Session("GR4N")(j + pCOUNTE) = "Mil People"
    '        Session("FinYear")(j + pCOUNTE) = Session("FinYear")(j)
    '    Next j

    '    'Save Old order
    '    Dim OldGR1N As Object
    '    OldGR1N = VB6.CopyArray(Session("GR1N"))
    '    'Sort by bubble size (descending)
    '    SortByBubbleSize(CInt(Session("FinYear")(1)), Session("COUNTE"), Session("PLOT"), Session("GR1N"), _
    '    Session("VARN"), Session("aFormulas"), Session("FDNG1"), Session("FileNumbr"), Session("FinYear"), _
    '    Session("FinYearHis")) 'jrs 2010/11/10 FirstYearNotNull
    '    'Create Old to New Map
    '    Dim Map() As Integer
    '    ReDim Map(pCOUNTE / 2)
    '    CreateMapBDOldtoNew(OldGR1N, Session("GR1N"), Map)
    '    'Reorder POP
    '    ReOrderBD(Session("BDPOP"), Map)
    '    'Reorder GDP
    '    If Session("sVarZ") <> "GDP" Then
    '        ReOrderBD(Session("BDGDP"), Map)
    '    End If
    '    'Reorder Other
    '    If Session("sVarZ") > "" Then
    '        ReOrderBD(Session("BDOTHR"), Map)
    '    End If
    '    'Reorder Color
    '    ReOrderColor(Session("RColor"), Map)
    '    ReOrderColor(Session("GColor"), Map)
    '    ReOrderColor(Session("BColor"), Map)

    '    Session("bBubble") = True

    '    'save original name to display
    '    ReDim Session("oVARN0")(Session("COUNTE"))
    '    For i = 1 To Session("COUNTE")
    '        Session("oVARN0")(i) = Session("VARN")(0, i)
    '    Next i
    '    'jrs 2010/02/04
    '    ReDim Session("oGR1N")(Session("COUNTE"))
    '    For i = 1 To Session("COUNTE")
    '        Session("oGR1N")(i) = Session("GR1N")(i)
    '    Next i

    '    'jrs 2010/10/13
    '    'SetUpManualScaleToMinMaxZ()

    'End Sub

    'Private Sub mnuBDGDP_Click()
    '    If Session("sVarZ") > "" Then
    '        cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = "blank.gif"
    '        If Session("sVarZ") <> "POP" Then
    '            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = "blank.gif"
    '        End If
    '        If Session("sVarZ") <> "GDP" Then
    '            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = "checkbox.gif"
    '        End If
    '        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = "blank.gif"
    '    End If

    '    Session("BDCheck") = "GDP"

    '    Session("COUNTE") = pCOUNTE * 3 / 2
    '    Session("VarsCount") = 3
    '    'Load POP
    '    Dim i As Integer, j As Integer
    '    For j = 1 To pCOUNTE / 2
    '        For i = 1 To Session("FinYear")(1)
    '            If IsNumeric(Session("BDGDP")(i, j)) Then
    '                If Session("BDGDP")(i, j) <> -9999 Then
    '                    Session("PLOT")(i, j + pCOUNTE) = Session("BDGDP")(i, j)
    '                Else
    '                    Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
    '                End If
    '            Else
    '                Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
    '            End If
    '        Next i
    '        Session("VARN")(0, j + pCOUNTE) = "GDP[" & Session("ShortSce")(Session("fileIdxZ")) & "]"
    '        Session("VARN")(1, j + pCOUNTE) = "GDP[" & Session("fileIdxZ") & "]"
    '        Session("GR1N")(j + pCOUNTE) = Session("GR1N")(j)
    '        Session("GR2N")(j + pCOUNTE) = ""
    '        Session("GR3N")(j + pCOUNTE) = ""
    '        Session("GR4N")(j + pCOUNTE) = "Billion $"
    '        Session("FinYear")(j + pCOUNTE) = Session("FinYear")(j)
    '    Next j

    '    'Save Old order
    '    Dim OldGR1N As Object
    '    OldGR1N = VB6.CopyArray(Session("GR1N"))
    '    'Sort by bubble size (descending)
    '    SortByBubbleSize(CInt(Session("FinYear")(1)), Session("COUNTE"), Session("PLOT"), Session("GR1N"), _
    '    Session("VARN"), Session("aFormulas"), Session("FDNG1"), Session("FileNumbr"), Session("FinYear"), _
    '    Session("FinYearHis")) 'jrs 2010/11/10 FirstYearNotNull
    '    'Create Old to New Map
    '    Dim Map() As Integer
    '    ReDim Map(pCOUNTE / 2)
    '    CreateMapBDOldtoNew(OldGR1N, Session("GR1N"), Map)
    '    'Reorder GDP
    '    ReOrderBD(Session("BDGDP"), Map)
    '    'Reorder POP
    '    If Session("sVarZ") <> "POP" Then
    '        ReOrderBD(Session("BDPOP"), Map)
    '    End If
    '    'Reorder Other
    '    If Session("sVarZ") > "" Then
    '        ReOrderBD(Session("BDOTHR"), Map)
    '    End If
    '    'Reorder Color
    '    ReOrderColor(Session("RColor"), Map)
    '    ReOrderColor(Session("GColor"), Map)
    '    ReOrderColor(Session("BColor"), Map)

    '    Session("bBubble") = True

    '    'save original name to display
    '    ReDim Session("oVARN0")(Session("COUNTE"))
    '    For i = 1 To Session("COUNTE")
    '        Session("oVARN0")(i) = Session("VARN")(0, i)
    '    Next i
    '    'jrs 2010/02/04
    '    ReDim Session("oGR1N")(Session("COUNTE"))
    '    For i = 1 To Session("COUNTE")
    '        Session("oGR1N")(i) = Session("GR1N")(i)
    '    Next i

    '    'jrs 2010/10/13
    '    'SetUpManualScaleToMinMaxZ()

    'End Sub

    'Private Sub mnuBDOther_Click()
    '    If Session("sVarZ") > "" Then
    '        cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = "checkbox.gif"
    '        If Session("sVarZ") <> "POP" Then
    '            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = "blank.gif"
    '        End If
    '        If Session("sVarZ") <> "GDP" Then
    '            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = "blank.gif"
    '        End If
    '        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = "blank.gif"
    '    End If

    '    Session("BDCheck") = "Other"

    '    Session("COUNTE") = pCOUNTE * 3 / 2
    '    Session("VarsCount") = 3
    '    'Load POP
    '    Dim i As Integer, j As Integer
    '    For j = 1 To pCOUNTE / 2
    '        For i = 1 To Session("FinYear")(1)
    '            If IsNumeric(Session("BDOTHR")(i, j)) Then
    '                If Session("BDOTHR")(i, j) <> -9999 Then
    '                    Session("PLOT")(i, j + pCOUNTE) = Session("BDOTHR")(i, j)
    '                Else
    '                    Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
    '                End If
    '            Else
    '                Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
    '            End If
    '        Next i
    '        Session("VARN")(0, j + pCOUNTE) = Session("sVarz") & "[" & Session("ShortSce")(Session("fileIdxZ")) & "]"
    '        Session("VARN")(1, j + pCOUNTE) = Session("sVarz") & "[" & Session("fileIdxZ") & "]"
    '        Session("GR1N")(j + pCOUNTE) = Session("GR1N")(j)
    '        Session("GR2N")(j + pCOUNTE) = ""
    '        Session("GR3N")(j + pCOUNTE) = ""
    '        If IfsVars.Item(Session("sVarZ")).Index > 0 Then
    '            Session("GR4N")(j + pCOUNTE) = VUnits(IfsVars.Item(Session("sVarZ")).Index)
    '        Else
    '            Session("GR4N")(j + pCOUNTE) = ""
    '        End If
    '        Session("FinYear")(j + pCOUNTE) = Session("FinYear")(j)
    '    Next j

    '    'Save Old order
    '    Dim OldGR1N As Object
    '    OldGR1N = VB6.CopyArray(Session("GR1N"))
    '    'Sort by bubble size (descending)
    '    SortByBubbleSize(CInt(Session("FinYear")(1)), Session("COUNTE"), Session("PLOT"), Session("GR1N"), _
    '    Session("VARN"), Session("aFormulas"), Session("FDNG1"), Session("FileNumbr"), Session("FinYear"), _
    '    Session("FinYearHis")) 'jrs 2010/11/10 FirstYearNotNull
    '    'Create Old to New Map
    '    Dim Map() As Integer
    '    ReDim Map(pCOUNTE / 2)
    '    CreateMapBDOldtoNew(OldGR1N, Session("GR1N"), Map)
    '    'Reorder GDP
    '    ReOrderBD(Session("BDOTHR"), Map)
    '    'Reorder POP
    '    If Session("sVarZ") <> "POP" Then
    '        ReOrderBD(Session("BDPOP"), Map)
    '    End If
    '    'Reorder Other
    '    If Session("sVarZ") <> "GDP" Then
    '        ReOrderBD(Session("BDGDP"), Map)
    '    End If
    '    'Reorder Color
    '    ReOrderColor(Session("RColor"), Map)
    '    ReOrderColor(Session("GColor"), Map)
    '    ReOrderColor(Session("BColor"), Map)

    '    Session("bBubble") = True

    '    'save original name to display
    '    ReDim Session("oVARN0")(Session("COUNTE"))
    '    For i = 1 To Session("COUNTE")
    '        Session("oVARN0")(i) = Session("VARN")(0, i)
    '    Next i
    '    'jrs 2010/02/04
    '    ReDim Session("oGR1N")(Session("COUNTE"))
    '    For i = 1 To Session("COUNTE")
    '        Session("oGR1N")(i) = Session("GR1N")(i)
    '    Next i

    '    'jrs 2010/10/13
    '    'SetUpManualScaleToMinMaxZ()

    'End Sub

    'Private Sub mnuBDNone_Click()
    '    If Session("sVarZ") > "" Then
    '        cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = "blank.gif"
    '        If Session("sVarZ") <> "POP" Then
    '            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = "blank.gif"
    '        End If
    '        If Session("sVarZ") <> "GDP" Then
    '            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = "blank.gif"
    '        End If
    '        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = "checkbox.gif"
    '    End If

    '    Session("BDCheck") = "None"

    '    Session("COUNTE") = pCOUNTE
    '    Session("VarsCount") = 2
    '    'Load POP
    '    Dim i As Integer, j As Integer
    '    For j = 1 To pCOUNTE / 2
    '        For i = 1 To Session("FinYear")(1)
    '            Session("PLOT")(i, j + pCOUNTE) = System.DBNull.Value
    '        Next i
    '        Session("VARN")(0, j + pCOUNTE) = ""
    '        Session("VARN")(1, j + pCOUNTE) = ""
    '        Session("GR1N")(j + pCOUNTE) = ""
    '        Session("GR2N")(j + pCOUNTE) = ""
    '        Session("GR3N")(j + pCOUNTE) = ""
    '        Session("GR4N")(j + pCOUNTE) = ""
    '        Session("FinYear")(j + pCOUNTE) = Nothing
    '    Next j

    '    'Create Old to New Map
    '    Dim Map() As Integer
    '    ReDim Map(pCOUNTE / 2)
    '    CreateMapBDOldtoNew(Session("GR1N"), Session("NoneOrder"), Map)
    '    'Reorder POP
    '    ReOrderBD(Session("BDPOP"), Map)
    '    'Reorder GDP
    '    If Session("sVarZ") <> "GDP" Then
    '        ReOrderBD(Session("BDGDP"), Map)
    '    End If
    '    'Reorder Other
    '    If Session("sVarZ") > "" Then
    '        ReOrderBD(Session("BDOTHR"), Map)
    '    End If
    '    'Reorder Color
    '    ReOrderColor(Session("RColor"), Map)
    '    ReOrderColor(Session("GColor"), Map)
    '    ReOrderColor(Session("BColor"), Map)

    '    Session("bBubble") = False

    '    'save original name to display
    '    ReDim Session("oVARN0")(Session("COUNTE"))
    '    For i = 1 To Session("COUNTE")
    '        Session("oVARN0")(i) = Session("VARN")(0, i)
    '    Next i
    '    'jrs 2010/02/04
    '    ReDim Session("oGR1N")(Session("COUNTE"))
    '    For i = 1 To Session("COUNTE")
    '        Session("oGR1N")(i) = Session("GR1N")(i)
    '    Next i

    '    'jrs 2010/10/13
    '    'SetUpManualScaleToMinMaxZ()

    'End Sub

    'Private Sub LoadBD(ByVal sVar As String, ByVal Target As Object)
    '    'Load sVar for all Members of GR1N
    '    Dim i As Integer
    '    For i = 1 To pCOUNTE / 2
    '        LoadBDMemberVals(Target, i, Session("GR1N")(i), sVar, Session("fileIdxZ"))
    '    Next i

    'End Sub

    'Private Sub LoadBDMemberVals(ByRef TargetVector As Object, ByVal TargetCol As Integer, ByVal sDim1 As String, ByVal sVar As String, _
    'ByVal fileIdx As Integer)
    '    '1. TargetVector, is vector to store results
    '    '2. TargetCol, indicates where in the 2-dimensioned array to store the country
    '    '3. sDim1, is the name of the country
    '    '4. sVar, name of the variable
    '    '5. fileIdx index of the file to be used

    '    Dim bHistFor As Boolean
    '    bHistFor = (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory)

    '    'jrs 2011/04/07 when using historic mode,
    '    'better to load forecast variables for Bubble Diameter
    '    '    If UCase(ActiveBase) = "IFSBASEHIST.RUN" Then
    '    '        gLoadForDisplayHis sVar, sDim1, -1, "", -1, "", -1, _
    '    '        True, True, Null, 1, TargetVector, TargetCol, FirstYearHistory, LastYearHistory, _
    '    '        False, False, False, "", bFillHis, buseAllHis, -1, bFillHisOnlyHoles
    '    '    Else
    '    Dim iRowPosition As Integer, iFirstYear As Integer
    '    If bHistFor Then
    '        iRowPosition = LastYearHistory - FirstYearHistory + 1
    '        iFirstYear = FirstYearForecast
    '    Else
    '        iRowPosition = 1
    '        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then 'JRS 2006/05/22
    '            iFirstYear = FirstYearForecast
    '        Else
    '            iFirstYear = FirstYearHistory
    '        End If
    '    End If
    '    'jrs 2011/12/16
    '    If fileIdx < 0 Then
    '        fileIdx = 1
    '    End If

    '    gLoadForDisplay(sVar, -1, sDim1, -1, "", -1, "", -1, "", fileIdx, True, True, Nothing, 1, TargetVector, TargetCol, _
    '    iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), _
    '    Session("IFs_MDB2Active"), Session("CurrencyVal"))
    '    '    End If

    '    If bHistFor Then
    '        Dim Fin As Integer
    '        Dim IfsPars As New myParameters
    '        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"), _
    '        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"), _
    '        Session("PutFdrvRun"), Session("CurrencySym"))

    '        Dim sDef As String, sUn As String
    '        Fin = Session("FinYear")(TargetCol)
    '        gLoadForDisplayHis(sVar, sDim1, -1, "", -1, "", -1, _
    '        Nothing, 1, TargetVector, TargetCol, FirstYearHistory, LastYearHistory - 1, _
    '        False, True, Session("bFillHis"), Session("buseAllHis"), -1, _
    '        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"), _
    '        Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"))
    '        Session("FinYear")(TargetCol) = LastYearHistory - FirstYearHistory + Fin
    '    End If

    'End Sub

    'Private Sub CreateMapBDOldtoNew(ByVal OldGR1 As Object, ByVal NewGR1 As Object, ByVal Target As Object)
    '    Dim j As Integer, k As Integer
    '    For j = 1 To pCOUNTE / 2
    '        For k = 1 To pCOUNTE / 2
    '            If NewGR1(j) = OldGR1(k) Then
    '                Exit For
    '            End If
    '        Next k
    '        Target(j) = k
    '    Next j
    'End Sub

    'Private Sub ReOrderBD(ByVal Target As Object, ByVal Map As Object)
    '    Dim tmp As Object
    '    tmp = VB6.CopyArray(Target)
    '    Dim i As Integer, j As Integer
    '    For j = 1 To pCOUNTE / 2
    '        For i = 1 To Session("FinYear")(1)
    '            Target(i, j) = tmp(i, Map(j))
    '        Next i
    '    Next j
    'End Sub

    'Only the first 10 will be constant
    'Private Sub ReOrderColor(ByVal Target As Object, ByVal Map As Object)
    '    Dim tmp As Object
    '    tmp = VB6.CopyArray(Target)
    '    Dim i As Integer
    '    For i = 1 To pCOUNTE / 2
    '        Target(i - 1) = tmp(Map(i) - 1)
    '    Next i
    'End Sub





    'Private Sub mnuStatistics_Click()
    '    'jrs 2011/12/15
    '    'Adding Analysis Across Time
    '    Dim iDimCount As Integer, iVarsCount As Integer
    '    If Session("mnuUseAllYrsTime") Then
    '        iDimCount = Session("Dim1Count") * (Session("LastYear") - Session("FirstYear") + 1)
    '        iVarsCount = Session("VarsCount") + 1
    '    ElseIf Session("mnuUseAllYrs") Then
    '        iDimCount = Session("Dim1Count") * (Session("LastYear") - Session("FirstYear") + 1)
    '        iVarsCount = Session("VarsCount")
    '    Else
    '        iDimCount = Session("Dim1Count")
    '        iVarsCount = Session("VarsCount")
    '    End If

    '    'jrs 2011/04/06
    '    'Statistics_Type = 0
    '    'frm_statistics.Show
    '    Dim RegVarD() As Object, RegVarI(0, 0) As Object, sVarI() As String
    '    ReDim RegVarD(iDimCount)
    '    Dim i As Integer, j As Integer, k As Integer
    '    'jrs 2011/12/15
    '    If Session("mnuUseAllYrs") Or Session("mnuUseAllYrsTime") Then
    '        For i = 1 To Session("Dim1Count")
    '            For j = 1 To Session("LastYear") - Session("FirstYear") + 1
    '                RegVarD(i + (j - 1) * Session("Dim1Count")) = Session("PLOT")(j, i)
    '            Next j
    '        Next i
    '    Else
    '        For i = 1 To Session("Dim1Count")
    '            RegVarD(i) = Session("PLOT")(Session("PYear"), i)
    '        Next i
    '    End If

    '    ReDim RegVarI(iDimCount, iVarsCount - 1)
    '    ReDim sVarI(iVarsCount - 1)

    '    For j = 1 To iVarsCount - 1
    '        'jrs 2011/12/15
    '        If j = iVarsCount - 1 And Session("mnuUseAllYrsTime") Then
    '            sVarI(j - 1) = "Year(YYYY)"
    '        Else
    '            sVarI(j - 1) = Session("VARN")(1, Session("Dim1Count") * j + 1)
    '            If Session("GR2N")(Session("Dim1Count") * j + 1) > "" Then
    '                sVarI(j - 1) = sVarI(j - 1) & "," & Session("GR2N")(Session("Dim1Count") * j + 1)
    '            End If
    '        End If

    '        'jrs 2011/12/15
    '        If Session("mnuUseAllYrs") Or Session("mnuUseAllYrsTime") Then
    '            If j = iVarsCount - 1 And Session("mnuUseAllYrsTime") Then
    '                For i = 1 To Session("Dim1Count")
    '                    For k = 1 To Session("LastYear") - Session("FirstYear") + 1
    '                        RegVarI(i + (k - 1) * Session("Dim1Count"), j - 1) = Session("FirstYear") + k - 1
    '                    Next k
    '                Next i
    '            Else
    '                For i = 1 To Session("Dim1Count")
    '                    For k = 1 To Session("LastYear") - Session("FirstYear") + 1
    '                        RegVarI(i + (k - 1) * Session("Dim1Count"), j - 1) = Session("PLOT")(k, Session("Dim1Count") * j + i)
    '                    Next k
    '                Next i
    '            End If
    '        Else
    '            sVarI(j - 1) = sVarI(j - 1) & "(" & CStr(Session("FirstYear") + Session("PYear") - 1) & ")"

    '            For i = 1 To Session("Dim1Count")
    '                RegVarI(i, j - 1) = Session("PLOT")(Session("PYear"), iDimCount * j + i)
    '            Next i
    '        End If
    '    Next j

    '    Dim sVarD As String
    '    sVarD = Session("VARN")(1, 1)
    '    If Session("GR2N")(1) > "" Then
    '        sVarD = sVarD & "," & Session("GR2N")(1)
    '    End If
    '    'jrs 2011/12/15
    '    If Not Session("mnuUseAllYrs") And Not Session("mnuUseAllYrsTime") Then
    '        sVarD = sVarD & "(" & CStr(Session("FirstYear") + Session("PYear") - 1) & ")"
    '    End If

    '    Session("Transformation") = ""
    '    Session("Statistics_Type") = 0

    '    SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"), _
    '    Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"), _
    '    Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"), _
    '    Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory), _
    '    Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"), _
    '    Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"), _
    '    Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"))

    '    PrepareTransformForStatistics(iDimCount, iVarsCount - 1, RegVarD, RegVarI, sVarD, sVarI, _
    '    Session("IFsStat_MDB"), Session("Total_Records"), Session("Total_Var"))

    '    '   Load the transformation form
    '    '   ----------------------------

    '    Support.FormManager.StateStore(Me)
    '    Support.FormManager.Show(Me, "Transform.aspx")

    'End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '           JAM 2012-07-07     Filling Data for histogram graphical display
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub FillData()


        Dim TotalIndex As Integer
        Dim LastData As Integer
        Dim TotalGroups As Integer
        Dim Total As Integer
        Dim i, h As Integer
        Dim maskinf As String
        Dim masksup As String
        Dim openrange As String
        Dim MaxSubset

        On Error GoTo grapherror7 'JAM 2012-06-17

        'JAM 2012-10-21 Defining Y-axis units
        If Session("Ypopulation") Then

            If Session("COUNTE") = NR Then
                Session("YAxisLabel") = "% of World Population"
            Else
                Session("YAxisLabel") = "% of Group Population"
            End If
            Session("mnuPerCountriesChecked") = False
            Session("mnuPerPopulationChecked") = True
        Else
            Session("YAxisLabel") = "% of Countries"
            Session("mnuPerCountriesChecked") = True
            Session("mnuPerPopulationChecked") = False
        End If



        Call DefinePercentiles()
        'JAM 2010-08-09 showing chosing data in a different subsets
        If Session("ShowSelection") Then
            MaxSubset = 1
        Else
            MaxSubset = 0
        End If

        ReDim Session("XData")(MaxSubset, Session("gpercentiles"))
        ReDim Session("YData")(MaxSubset, Session("gpercentiles"))
        ReDim Session("PointLabels")(Session("gpercentiles"))
        validpoints = 0
        For h = 0 To MaxSubset
            For i = 1 To Session("gpercentiles") 'pCOUNTE

                If Not IsNumeric(Session("PLOT")(Session("PYear"), i)) Then
                    Session("XData")(h, i - 1) = -99999
                Else
                    Session("XData")(h, i - 1) = 1 'str$(PLOT(PYear%, i))
                End If

                If Not IsNumeric(Session("PLOT")(Session("PYear"), i)) Then
                    Session("YData")(h, i - 1) = -99999
                Else
                    validpoints = validpoints + 1 'JAM 2010-09-28
                    If MaxSubset = 0 Then
                        If Session("Ypopulation") Then
                            Session("YData")(h, i - 1) = Yvalue(i - 1) 'str$(PLOT(PYear%, i))
                        Else
                            Session("YData")(h, i - 1) = Yctvalue(i - 1)
                        End If
                    Else 'JAM 2010-09-08  There more than one subsets
                        If (i - 1) = percentilechosen Then
                            If h = 1 Then
                                If Session("Ypopulation") Then
                                    Session("YData")(h, i - 1) = Math.Abs(Yvalue(i - 1) - (Valuechosen / SumPopulation) * 100)
                                Else
                                    Session("YData")(h, i - 1) = Math.Abs(Yctvalue(i - 1) - (1 / SumCt) * 100)
                                End If

                            Else
                                If Session("Ypopulation") Then
                                    Session("YData")(h, i - 1) = ((Valuechosen / SumPopulation) * 100)
                                Else
                                    Session("YData")(h, i - 1) = ((1 / SumCt) * 100)
                                End If

                            End If


                        Else
                            If h = 1 Then
                                If Session("Ypopulation") Then
                                    Session("YData")(h, i - 1) = Yvalue(i - 1)
                                Else
                                    Session("YData")(h, i - 1) = Yctvalue(i - 1)
                                End If

                            Else
                                Session("YData")(h, i - 1) = -99999

                            End If



                        End If


                    End If
                End If





                'JAM 2010-08-10 Formatting Mask
                If Not Session("DivValue") Then
                    If Math.Abs(Session("DefPerc")(i - 1, 0)) >= 0 And Math.Abs(Session("DefPerc")(i - 1, 0)) <= 9.9999 Then
                        maskinf = Format(Math.Round(Session("DefPerc")(i - 1, 0), 3), "0.000")
                    Else
                        If Math.Abs(Session("DefPerc")(i - 1, 0)) >= 10 And Math.Abs(Session("DefPerc")(i - 1, 0)) <= 99 Then
                            maskinf = Format(Math.Round(Session("DefPerc")(i - 1, 0), 2), "0.00")
                        Else
                            If Math.Abs(Session("DefPerc")(i - 1, 0)) >= 100 And Math.Abs(Session("DefPerc")(i - 1, 0)) <= 999 Then
                                maskinf = Format(Math.Round(Session("DefPerc")(i - 1, 0), 1), "0.0")
                            Else
                                maskinf = Str(Math.Round(Session("DefPerc")(i - 1, 0)))
                            End If
                        End If
                        'Format(DefPerc(i - 1, 0), "0.0000")



                    End If
                Else
                    If Session("gpercentiles") <> 0 Then
                        maskinf = Str(Math.Round(((100 / Session("gpercentiles")) * (i - 1)))) + "%"
                    End If

                End If

                If Not Session("DivValue") Then
                    If Math.Abs(Session("DefPerc")(i - 1, 1)) >= 0 And Math.Abs(Session("DefPerc")(i - 1, 1)) <= 9.9999 Then
                        masksup = Format(Math.Round(Session("DefPerc")(i - 1, 1), 3), "0.000")
                    Else
                        If Math.Abs(Session("DefPerc")(i - 1, 1)) >= 10 And Math.Abs(Session("DefPerc")(i - 1, 1)) <= 99 Then
                            masksup = Format(Math.Round(Session("DefPerc")(i - 1, 1), 2), "0.00")
                        Else
                            If Math.Abs(Session("DefPerc")(i - 1, 1)) >= 100 And Math.Abs(Session("DefPerc")(i - 1, 1)) <= 999 Then
                                masksup = Format(Math.Round(Session("DefPerc")(i - 1, 1), 1), "0.0")
                            Else
                                masksup = Str(Math.Round(Session("DefPerc")(i - 1, 1)))
                            End If
                        End If


                    End If
                Else
                    If Session("gpercentiles") <> 0 Then
                        masksup = Str(Math.Round((100 / Session("gpercentiles")) * i)) + "%"
                    End If
                End If
                If i = 1 Then
                    openrange = "["
                Else
                    openrange = "]"
                End If

                Session("PointLabels")(i - 1) = openrange + maskinf + "-" + masksup + "]"

            Next i
        Next h

        'JAM 2010-09-28 when there aren't valid points
        If validpoints = 0 Then
            'grp_histogram.MainTitle = "No Valid Points found for this year"
            'grp_histogram.Points = 0
        End If


        'JAM 2010-08-21 Showing Table Data

        'Call tablelegend


        'JAM 2013-01-05
        If Not Session("OrdAlp") Then
            For i = 0 To Session("gpercentiles") - 1
                Call BubbleOrder(i, 3)
            Next i
        Else
            For i = 0 To Session("gpercentiles") - 1
                Call BubbleOrder(i, 0) 'Order by Name
            Next i
        End If
        Exit Sub
grapherror7:
        Exit Sub

        'Msg$ = "Sorry. An error has arisen during filling data process "
        'MsgBox(Msg$, vbCritical, "Histogram Graphical Display")


    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' JAM 2012-08-01 Define Percentiles
    ''' </summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' <remarks></remarks>


    Private Sub DefinePercentiles()

        On Error GoTo grapherror1 'JAM 2012-06-17

        Dim percentilewidth
        Dim MinValue, MaxValue
        'Dim Session("varray")()
        Dim i, j As Integer


        ' ReDim Session("NoDataNull")(pCOUNTE)   'JAM 2010-09-25

        ReDim Session("NoDataNull")(Session("pCOUNTE"))   'JAM 2012-09-02
        'ReDim Session("varray")(pCOUNTE)
        ReDim Session("varray")(Session("pCOUNTE"))


        ReDim Session("DefPerc")(Session("gpercentiles"), 2)
        ReDim Yvalue(Session("gpercentiles"))
        ReDim Yctvalue(Session("gpercentiles"))
        'JAM 2010-08-22 Countries detail in each percentile
        'jrs 2014/08/22 ReDim Session("gcategories")(Session("gpercentiles"), 183, 4) 'JAM 2010-08-09 Adding some extra categorie to save population in all cases
        ReDim Session("gcategories")(Session("gpercentiles"), NR, 4)
        ReDim Session("counters")(Session("gpercentiles"))
        'ReDim Session("varray")(pCOUNTE)
        ReDim Session("varray")(Session("pCOUNTE"))


        For i = 0 To (Session("gpercentiles") - 1)
            Session("counters")(i) = 0
        Next i


        For i = 1 To Session("pCOUNTE") ' pCOUNTE
            Session("varray")(i) = Session("PLOT")(Session("PYear"), i)
            'a = GR1N(i)
            If Not IsNumeric(Session("PLOT")(Session("PYear"), i)) Then 'JAM 2010-09-25
                Session("NoDataNull")(i) = True
            Else
                Session("NoDataNull")(i) = False
            End If
        Next i

        MaxValue = Session("TotalMax")
        MinValue = Session("TotalMin")


        '        MaxValue = FindMax(Session("varray")(), pCOUNTE)
        '        MinValue = FindMin(Session("varray")(), pCOUNTE)
        gOrigMin = MinValue
        gOrigMax = MaxValue
        If Not DefMinMax Then
            gMinValue = MinValue
            gMaxValue = MaxValue

        End If

        If DefMinMax Then
            MinValue = gMinValue
            MaxValue = gMaxValue
        End If

        If Session("gpercentiles") <> 0 And IsNumeric(MaxValue) And IsNumeric(MinValue) Then

            'If Not ChangeCateg Then 'JAM 2012-06-09
            percentilewidth = (MaxValue - MinValue) / Session("gpercentiles")

            For i = 0 To (Session("gpercentiles") - 1)

                Session("DefPerc")(i, 0) = MinValue + (percentilewidth * i)
                Session("DefPerc")(i, 1) = (MinValue + percentilewidth) + (percentilewidth * i)

            Next i
            'End If

            'JAM 2012-08-04 Pending to change
            'JAM 2012-06-17    Redefine global arrays only is not changecateg
            'If ModeDefCateg Then 'JAM 2012-06-17
            '    If Not ChangeCateg Then
            '        'JAM 2012-05-26 Filling global Percentils
            '        BuildGlbCateg()
            '    Else   'JAM 2012-06-17
            '        FillGlbCateg()
            '    End If
            'Else
            '    BuildGlbCateg()
            'End If

        End If


        SumPopulation = TotalPop()
        SumCt = TotalCt()

        For j = 0 To (Session("gpercentiles") - 1)
            Yvalue(j) = 0
            Yctvalue(j) = 0
        Next j

        For i = 1 To Session("pCOUNTE") 'pCOUNTE
            For j = 0 To (Session("gpercentiles") - 1)
                If IsNumeric(Session("PLOT")(Session("PYear"), i)) Then
                    If Session("PLOT")(Session("PYear"), i) >= Session("DefPerc")(j, 0) And Session("PLOT")(Session("PYear"), i) <= Session("DefPerc")(j, 1) Then
                        Yvalue(j) = Yvalue(j) + Session("PopAll")(Session("PYear"), i)
                        Yctvalue(j) = Yctvalue(j) + 1
                        'JAM 2010-08-21 Loading Countries Data
                        Session("gcategories")(j, Session("counters")(j), 0) = Session("GR1N")(i)
                        If Not VarinTable Then
                            Session("gcategories")(j, Session("counters")(j), 1) = Session("PopAll")(Session("PYear"), i)
                        Else
                            Session("gcategories")(j, Session("counters")(j), 1) = Session("PLOT")(Session("PYear"), i)
                        End If
                        Session("gcategories")(j, Session("counters")(j), 2) = Session("PopAll")(Session("PYear"), i)
                        Session("gcategories")(j, Session("counters")(j), 3) = Session("PLOT")(Session("PYear"), i)
                        Session("counters")(j) = Session("counters")(j) + 1
                        Exit For
                    End If

                End If
            Next j
        Next i

        For j = 0 To (Session("gpercentiles") - 1)
            If SumPopulation <> 0 Then
                Yvalue(j) = (Yvalue(j) / SumPopulation) * 100 'JAM 2010-08-10 In %
            Else
                Yvalue(j) = 0
            End If

        Next j

        For j = 0 To (Session("gpercentiles") - 1)
            If SumCt <> 0 Then
                Yctvalue(j) = (Yctvalue(j) / SumCt) * 100 'JAM 2010-08-10 In %
            Else
                Yctvalue(j) = 0
            End If

        Next j


        'JAM 2013-01-05 Sorting detail for each percentile
        'If Session("OrdAlp") And Not Page.IsPostBack Then
        '    For i = 0 To Session("gpercentiles") - 1
        '        Call BubbleOrder(i, 3)
        '    Next i
        'Else
        '    For i = 0 To Session("gpercentiles") - 1
        '        Call BubbleOrder(i, 0) 'Order by Name
        '    Next i
        'End If
        ''''''''''''''''''

        Exit Sub
grapherror1:
        Exit Sub
        'JAM 2012-08-04 Pending
        'Screen.MousePointer = 0
        'Msg$ = "Sorry. An error has arisen during filling data process "
        'MsgBox(Msg$, vbCritical, "Histogram Graphical Display")

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2010-08-07  Loading Population All Variables
    '       jrs 2012-06-12  Adding formulas consideration
    '
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub LoadPopAll()

        Dim Fin As Integer
        Dim IfsPars As New myParameters
        'jrs 2016/01/06
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim sDef As String, sUn As String



        If Session("COUNTE") <> NR And ginitrue Then

            'JAM 2012-08-04 Pending
            'Msg$ = "You must select a single variable across all regions "

            'Msg$ = Msg$ + "in order to generate a histogram graph."

            'MsgBox(Msg$, vbOKOnly, "Histogram")

            'JRS 09/24/02 ERROR NUMBER FOR USER ERROR

            Err.Raise(Number:=3000)

            Exit Sub

        End If

        '

        '    'Checking that all the variables are the same

        '    Dim i, j As Integer

        '    For i = 2 To NR%

        '        If VARN$(1, 1) <> VARN$(1, i) Then

        '            msg$ = "You must select a single variable across all regions "

        '            msg$ = msg$ + "in order to generate histogram graph."

        '            MsgBox msg$, vbOKOnly, "Histogram"

        '            Exit Sub

        '        End If

        '    Next i

        'JRS 10/14/03

        'FIND AGGREGATION RULE

        Dim sAgg As String



        ReDim Session("PopMatrix")(Session("COUNTE") + 1)

        Dim year As Integer

        Dim POPptr, GDPptr, FileNum As Integer

        ReDim Session("PopAll")(Session("LastYear") - Session("FirstYear") + 1, Session("COUNTE") + 1) 'As Single

        'JRS 10/19/03

        Dim sFile As String

        'jrs 2012/06/12

        Dim pos As Integer, pos2 As Integer, sVar As String

        Dim finam As String

        Dim iRowPosition, iFirstYear, j, i As Integer

        sVar = Session("VARN")(1, 1)

        pos = InStr(sVar, "[")

        pos2 = InStr(sVar, "]")

        sFile = 0

        If pos > 0 And pos2 > 0 And pos2 > pos Then

            sFile = Mid(sVar, pos + 1, pos2 - pos - 1)

        Else

            If Not (Session("aFormulas")(1) Is Nothing) Then

                pos = InStr(Session("aFormulas")(1).disVar.displayDet(1), "[")

                pos2 = InStr(Session("aFormulas")(1).disVar.displayDet(1), "]")

                sVar = Session("aFormulas")(1).disVar.displayDet(1)

                sFile = Mid(sVar, pos + 1, pos2 - pos - 1)

            End If

        End If

        'Fill population Matrix

        'JRS 10/19/03 If (FirstYear% <> FirstYearHistory%) Then 'JRS 08/12/02

        If Val(sFile) > -1 Then

            'JRS 10/14/03

            'jrs 2012/06/12 chk = IfsVars.Item(mid(VARN$(1, 1), 1, InStr(VARN$(1, 1), "[") - 1)).Index

            'jrs 2012/06/12 sAgg = VAggregation(chk)

            POPptr = POPidx

            GDPptr = GDPidx

            'Open the file

            FileNum = Val(sFile)

            If FileNum = 0 Then

                finam = Session("PutFdrvRun") + "working.run.db"

            ElseIf FileNum = 1 Then
                'jrs 2026/02/17 added + .db
                finam = Session("PutFdrvRun") + Session("ActiveBase") + ".db" '"IFSBASE.RUN"

            Else

                finam = Session("RunFiles")(FileNum)

            End If

            'JAM 2010-10-05 for history plus forecast

            Dim a, b
            a = Session("LastYear")
            b = Session("FirstYear")

            If Session("LastYear") > LastYearHistory And Session("FirstYear") = FirstYearHistory Then

                iRowPosition = LastYearHistory - FirstYearHistory + 1

                iFirstYear = FirstYearForecast

                Dim MyFileforDisplay As IStorage
                Dim MyDF As IDataframe

                For j = 1 To Session("COUNTE") 'JAM 2010-10-05 Forecast

                    'JAM 2012-08-04 Checking with Jose
                    'gLoadForDisplay("POP", POPptr, Session("GR1N")(j), -1, "", -1, "", -1, finam, FileNum, (j = 1), False, False, 1, PopAll, _
                    '   j, iRowPosition, iFirstYear, Session("LastYear"), 0, False, "", False)


                    'JAM 2012-08-12 Jose, please check why I got a message: "Group not Found"

                    'JAM 2012-08-11
                    'Before changes
                    ' gLoadForDisplay("POP", POPptr, Session("GR1N")(j), -1, "", -1, "", -1, "", FileNum, (j = 1), False, Nothing, 1, Session("PopAll"), j, _
                    'iRowPosition, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), _
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))

                    gLoadForDisplayNET("POP", POPptr, Session("GR1N")(j), -1, "", -1, "", -1, finam, FileNum, (j = 1), False, Nothing, 1, Session("PopAll"), j,
                    iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    'gLoadForDisplay("POP", POPptr, Session("GR1N")(j), -1, "", -1, "", -1, finam, FileNum, (j = 1), False, Nothing, 1, Session("PopAll"), j,
                    'iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))



                Next j



                'jrs 2010/10/06

                Dim FinAll As Integer

                FinAll = Session("FinYear")(1)

                For i = 1 To Session("COUNTE") 'JAM 2010-10-05 history
                    'JAM 2012-08-04 Checking with Jose
                    'gLoadForDisplayHis("POPULATION", GR1N(i), -1, "", -1, "", -1, (i = 1), False, False, 1, PopAll, i, FirstYearHistory, _
                    'LastYearHistory - 1, False, False, False, "", bFillHis, buseAllHis, -1, bFillHisOnlyHoles)

                    'JAM 2012-08-11
                    'Before Changes
                    ' gLoadForDisplayHis("POPULATION", Session("GR1N")(i), -1, "", -1, "", -1, _
                    'Nothing, 1, Session("PopAll"), i, FirstYearHistory, LastYearHistory - 1, _
                    'False, False, Session("bFillHis"), Session("buseAllHis"), -1, _
                    'Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"), _
                    'Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"))

                    gLoadForDisplayHis("POPULATION", Session("GR1N")(i), -1, "", -1, "", -1,
                    False, 1, Session("PopAll"), i, FirstYearHistory, LastYearHistory - 1,
                    False, False, Session("bFillHis"), Session("buseAllHis"), -1,
                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                    Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))

                    Session("FinYear")(i) = LastYearHistory - FirstYearHistory + Fin


                    'jrs 2010/10/06

                    Session("FinYear")(i) = Session("FinYear")(i) + FinAll

                Next i

            Else

                Dim MyFileforDisplay As IStorage
                Dim MyDF As IDataframe

                For j = 1 To Session("COUNTE")
                    'JAM 2012-08-04 Checking with Jose
                    'gLoadForDisplay("POP", POPptr, GR1N(j), -1, "", -1, "", -1, finam, FileNum, (j = 1), False, False, 1, PopAll, _
                    'j, 1, FirstYear, LastYear, 0, False, "", False)

                    'JAM 2012-08-11
                    'Before Changes
                    'gLoadForDisplay("POP", POPptr, Session("GR1N")(j), -1, "", -1, "", -1, "", FileNum, (j = 1), False, Nothing, 1, Session("PopAll"), j, _
                    '1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), _
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))

                    gLoadForDisplayNET("POP", POPptr, Session("GR1N")(j), -1, "", -1, "", -1, finam, FileNum, (j = 1), False, Nothing, 1, Session("PopAll"), j,
                     1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                     Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    'gLoadForDisplay("POP", POPptr, Session("GR1N")(j), -1, "", -1, "", -1, finam, FileNum, (j = 1), False, Nothing, 1, Session("PopAll"), j,
                    ' 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    ' Session("IFs_MDB2Active"), Session("CurrencyVal"))

                Next j

            End If

        Else

            For i = 1 To Session("COUNTE")
                'JAM 2012-08-04 Checking with Jose
                'gLoadForDisplayHis("POPULATION", GR1N(i), -1, "", -1, "", -1, (i = 1), False, False, 1, PopAll, i, FirstYear, _
                'LastYear, False, False, False, "", bFillHis, buseAllHis, -1, bFillHisOnlyHoles)


                'JAM 2012-08-11
                'Before Changes
                'gLoadForDisplayHis("POPULATION", Session("GR1N")(i), -1, "", -1, "", -1, _
                'Nothing, 1, Session("PopAll"), i, Session("FirstYear"), Session("LastYear") - 1, _
                'False, False, Session("bFillHis"), Session("buseAllHis"), -1, _
                'Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"), _
                'Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"))
                'Session("FinYear")(i) = LastYearHistory - FirstYearHistory + Fin

                gLoadForDisplayHis("POPULATION", Session("GR1N")(i), -1, "", -1, "", -1,
                (i = 1), 1, Session("PopAll"), i, Session("FirstYear"), Session("LastYear") - 1,
                False, False, Session("bFillHis"), Session("buseAllHis"), -1,
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                Session("FinYear")(i) = LastYearHistory - FirstYearHistory + Fin


            Next i

        End If



    End Sub

    Private Function TotalPop()
        Dim acumpop, i

        acumpop = 0
        For i = 1 To Session("pCOUNTE") ' pCOUNTE
            acumpop = acumpop + Session("PopAll")(Session("PYear"), i)
        Next i
        TotalPop = acumpop
    End Function
    Private Function TotalCt()
        Dim acumct, i

        acumct = 0
        For i = 1 To Session("pCOUNTE") 'pCOUNTE
            acumct = acumct + 1
        Next i
        TotalCt = acumct
    End Function

    'jrs 2010/09/29
    Private Function FindFirstYearNotNull() As Integer
        Dim i As Integer, j As Integer
        FindFirstYearNotNull = 1 '-1 if not found better to set it to something valid
        For i = 1 To Session("FinYear")(Session("COUNTE"))
            For j = 1 To Session("pCOUNTE") / 2
                If IsNumeric(Session("PLOT")(i, j)) And IsNumeric(Session("PLOT")(i, Session("pCOUNTE") / 2 + j)) Then
                    FindFirstYearNotNull = i
                    Exit Function
                End If
            Next j
        Next i
    End Function
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2012-08-26 Finding Min and Max global
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub FindGlbMinMax()
        Dim pivotmin, pivotmax As Double
        Dim i, j, LocalFirstYear, LocalLastYear, LocalTimepoints As Integer

        On Error GoTo grapherror6

        LocalFirstYear = Session("FirstYear")
        LocalLastYear = Session("LastYear")

        'JAM 2012-08-26 review data
        Session("DisplayLastYear") = 2050
        LocalTimepoints = Session("DisplayLastYear") - LocalFirstYear + 1

        pivotmax = -1000000000
        pivotmin = 1000000000



        For i = 1 To LocalTimepoints
            For j = 1 To Session("pCOUNTE")
                If IsNumeric(Session("PLOT")(i, j)) Then
                    If Session("PLOT")(i, j) > pivotmax Then
                        pivotmax = Session("PLOT")(i, j)
                    End If

                    If Session("PLOT")(i, j) < pivotmin Then
                        pivotmin = Session("PLOT")(i, j)
                    End If

                End If

            Next j
        Next i


        Session("TotalMin") = pivotmin
        Session("TotalMax") = pivotmax

        Exit Sub
grapherror6:

        'Screen.MousePointer = 0
        'Msg$ = "Sorry. An error has arisen during filling data process "
        'MsgBox(Msg$, vbCritical, "Histogram Graphical Display")
        Exit Sub
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' JAM 2012-08-30 Removing Session Variables
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveVars()
        Session.Remove("PYear")
        Session.Remove("DisplayLastYear")
        Session.Remove("gpercentiles")
        Session.Remove("YAxisLabel")
        Session.Remove("mnuPerCountriesChecked")
        Session.Remove("mnuPerPopulationChecked")
        Session.Remove("XData")
        Session.Remove("YData")
        Session.Remove("PointLabels")
        Session.Remove("DefPerc")
        Session.Remove("NoDataNull")
        Session.Remove("varray")
        Session.Remove("PopAll")
        Session.Remove("PopMatrix")
        Session.Remove("PCounte")
        Session.Remove("TotalMax")
        Session.Remove("TotalMin")
        'Session("aFormulas")

    End Sub

    'JAM 2012-09-15 Define Percentiles
    Protected Sub cmdVLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVLine.Click

        'jam 2021/08/22
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If Not IsNumeric(txtVLine.Text) Then
            LoadMessage("You must enter a numeric value between 2 and 100", Session("Message"))

            'jam 2021/08/22
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
            vurl = FriendlyUrl.Href("~/frm_Message")
            Response.Redirect(vurl)


        Else
            If Val(txtVLine.Text) < 2 Or Val(txtVLine.Text) > 100 Then

                LoadMessage("You must enter a numeric value between 2 and 100", Session("Message"))

                'jam 2021/08/22
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
                vurl = FriendlyUrl.Href("~/frm_Message")
                Response.Redirect(vurl)

            End If
        End If

        'If Session("FirstYear") = FirstYearForecast Then
        '    If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
        '        LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
        '        Support.FormManager.StateStore(Me)
        '        Support.FormManager.Show(Me, "frm_Message.aspx")
        '    End If

        'ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
        '    If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
        '        LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
        '        Support.FormManager.StateStore(Me)
        '        Support.FormManager.Show(Me, "frm_Message.aspx")
        '    End If

        'End If

        'Session("PYear") = CInt(txtGoToYear.Text) - Session("FirstYear") + 1
        'If Session("PYear") < 1 Then Session("PYear") = 1
        'If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then
        '    'jrs 2012/05/02
        '    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" And Session("PYear") + Session("FirstYear") - 1 <= Session("LastYearForecast") Then
        '        Session("DisplayLastYear") = Session("PYear") + Session("FirstYear") - 1
        '    Else
        '        Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
        '    End If
        'End If
        'If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        Session("gpercentiles") = Val(txtVLine.Text)
        'Call FillData()
        frm_VLine.Visible = False

        'JAM 2013-02-19
        FillData()
        PackJS()

        lblJS.Visible = True
        cswmWebMenu.Visible = True



        'jrs 2012/05/30
        'Session("RegressType") = ""
        'Session("TableTitle") = ""

    End Sub
    Protected Sub mnuPercentiles_Click()
        frm_VLine.Visible = True
        txtVLine.Text = "10"

        txtVLine.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdVLine.ClientID + "')")

        'JAM 2012-09-02 Review Variables
        'ShowSelection = False
        'Ypopulation = True
        ' Call FillData()

        lblJS.Visible = False
        cswmWebMenu.Visible = False
    End Sub
    'JAM 2012-10-06 Show Percentiles in Percentage
    Private Sub mnuinpercentage_Click()
        Session("DivValue") = True
        Session("mnuinvaluesChecked") = False
        Session("mnuinpercentageChecked") = True
        'JAM 2013-02-19
        FillData()
        PackJS()

        lblJS.Visible = True
        'Call showGraph()
    End Sub
    'JAM 2012-10-06 Show Percentiles in Values
    Private Sub mnuinvalues_Click()
        Session("DivValue") = False
        Session("mnuinvaluesChecked") = True
        Session("mnuinpercentageChecked") = False
        'JAM 2013-02-19
        FillData()
        PackJS()

        lblJS.Visible = True
        'Call showGraph()
    End Sub
    'JAM 2012-10-16 % of Population
    Private Sub mnuPerPopulation_Click()
        Session("Ypopulation") = True
        'JAM 2013-02-19
        FillData()
        PackJS()

        lblJS.Visible = True
        'Call showGraph()
    End Sub
    'JAM 2012-10-16 % of Population
    Private Sub mnuPerCountries_Click()
        Session("Ypopulation") = False
        'JAM 2013-02-19
        FillData()
        PackJS()

        lblJS.Visible = True
        'Call showGraph()
    End Sub

    'JAM 2012-12-29 Alphabetical for Detail Order
    Private Sub mnuAlphabetical_Click()
        Dim i As Integer
        'If Not Session("OrdAlp") Then
        For i = 0 To Session("gpercentiles") - 1
            Call BubbleOrder(i, 0) 'Order by Name
        Next i

        'End If
        Session("OrdAlp") = True
        'Call FillData()

        'JAM 2013-02-19
        PackJS()

        lblJS.Visible = True

    End Sub
    'JAM 2012-12-29 Numeric for Chosen Variable
    Private Sub mnuChosenVariable_Click()
        Dim i As Integer
        'If Session("OrdAlp") Then
        For i = 0 To Session("gpercentiles") - 1
            Call BubbleOrder(i, 3)
        Next i
        'End If
        Session("OrdAlp") = False
        'Call FillData()

        'JAM 2013-02-19
        PackJS()

        lblJS.Visible = True
        'Call showGraph()
    End Sub

    'JAM 2012-12-29 Order Detail by Value or Region Name
    Private Sub BubbleOrder(ByVal vperdetail As Integer, ByVal indperord As Integer)

        Dim iMin As Long
        Dim iMax As Long
        Dim vtemp, vtemp2, vtemp3, vtemp4
        Dim pos As Long
        Dim i As Long


        iMin = 0
        iMax = Session("counters")(vperdetail) - 1

        While iMax > iMin
            pos = iMin
            For i = iMin To iMax - 1
                If indperord = 0 Then
                    If Session("gcategories")(vperdetail, i, indperord) > Session("gcategories")(vperdetail, i + 1, indperord) Then
                        'Sorting Values
                        vtemp = Session("gcategories")(vperdetail, i + 1, 1)
                        Session("gcategories")(vperdetail, i + 1, 1) = Session("gcategories")(vperdetail, i, 1)
                        Session("gcategories")(vperdetail, i, 1) = vtemp
                        'Sorting countries names
                        vtemp2 = Session("gcategories")(vperdetail, i + 1, 0)
                        Session("gcategories")(vperdetail, i + 1, 0) = Session("gcategories")(vperdetail, i, 0)
                        Session("gcategories")(vperdetail, i, 0) = vtemp2
                        ' Sorting population variables to show in all cases
                        vtemp3 = Session("gcategories")(vperdetail, i + 1, 2)
                        Session("gcategories")(vperdetail, i + 1, 2) = Session("gcategories")(vperdetail, i, 2)
                        Session("gcategories")(vperdetail, i, 2) = vtemp3
                        vtemp4 = Session("gcategories")(vperdetail, i + 1, 3)
                        Session("gcategories")(vperdetail, i + 1, 3) = Session("gcategories")(vperdetail, i, 3)
                        Session("gcategories")(vperdetail, i, 3) = vtemp4
                        pos = i
                    End If
                Else
                    If Session("gcategories")(vperdetail, i, indperord) > Session("gcategories")(vperdetail, i + 1, indperord) Then
                        'Sorting Values
                        vtemp = Session("gcategories")(vperdetail, i + 1, 1)
                        Session("gcategories")(vperdetail, i + 1, 1) = Session("gcategories")(vperdetail, i, 1)
                        Session("gcategories")(vperdetail, i, 1) = vtemp
                        'Sorting countries names
                        vtemp2 = Session("gcategories")(vperdetail, i + 1, 0)
                        Session("gcategories")(vperdetail, i + 1, 0) = Session("gcategories")(vperdetail, i, 0)
                        Session("gcategories")(vperdetail, i, 0) = vtemp2
                        ' Sorting population variables to show in all cases
                        vtemp3 = Session("gcategories")(vperdetail, i + 1, 2)
                        Session("gcategories")(vperdetail, i + 1, 2) = Session("gcategories")(vperdetail, i, 2)
                        Session("gcategories")(vperdetail, i, 2) = vtemp3
                        vtemp4 = Session("gcategories")(vperdetail, i + 1, 3)
                        Session("gcategories")(vperdetail, i + 1, 3) = Session("gcategories")(vperdetail, i, 3)
                        Session("gcategories")(vperdetail, i, 3) = vtemp4
                        pos = i
                    End If
                End If
            Next i
            iMax = pos
        End While

    End Sub
    'JAM 2013-01-19 Adding graph detail for histogram
    Private Sub mnuGraphDetail_Click()

        If Session("ViewDetail") Then
            Session("ViewDetail") = False
        Else
            Session("ViewDetail") = True
        End If

        lblJS.Visible = True

    End Sub
    Private Sub mnuHideDetailGraph_Click()  'JAM 2013-02-02


        '' Process.Start("http://localhost/ifsweb/ifs/frm_Histogram.aspx")

        Session("hidetail") = "S"

        'Call FillData()
        PackJS()
        DisplayGraph()

        lblJS.Visible = True

    End Sub
    'JAM 2013-02-25
    Protected Sub cmdHorizon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorizon.Click

        'jam 2021/08/22
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        If Not IsNumeric(txtHorizon.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))

            'jam 2021/08/22
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")


            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
            vurl = FriendlyUrl.Href("~/frm_Message")
            Response.Redirect(vurl)
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))

                'jam 2021/08/22
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
                vurl = FriendlyUrl.Href("~/frm_Message")
                Response.Redirect(vurl)

            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
                'jam 2021/08/22
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
                vurl = FriendlyUrl.Href("~/frm_Message")
                Response.Redirect(vurl)


            End If

        End If

        If Session("Interval") > 1 Then
            If (CInt(txtHorizon.Text) - Session("FirstYear")) Mod Session("Interval") <> 0 Then
                LoadMessage("Invalid Year, year must follow the existing Interval, please use the combo box.", Session("Message"))
                'jam 2021/08/22
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
                vurl = FriendlyUrl.Href("~/frm_Message")
                Response.Redirect(vurl)



            End If
        End If

        cmb_Horizon.Text = txtHorizon.Text
        cmb_Horizon_SelectedIndexChanged(Me, New EventArgs)
    End Sub

    Private Sub BuildTitle()
        Dim Legend, Vn, d1, d2, d3 As String


        Vn = LTrim(RTrim(Session("VARN")(0, 1)))

        d1 = LTrim(RTrim(Session("FDNG1")(1)))
        d2 = LTrim(RTrim(Session("FDNG2")(1)))
        d3 = LTrim(RTrim(Session("FDNG3(1)")))

        '          Compose legend and assign legend
        '           --------------------------------

        'JAM 2010-08-10 Adding Second and Third dimension
        Legend = Vn
        ' If Len(d1$) > 0 Then Legend$ = Legend$ + d1$
        If Len(d2) > 0 Then
            Legend = Legend + "," + d2$
            '    Else
            '        If Len(d1$) > 0 Then
            '            Legend$ = Legend$
            '        End If
        End If

        If Len(d3) > 0 Then
            Legend = Legend + "," + d3
            '    Else
            '        If Len(d1$) > 0 Then
            '            Legend$ = Legend$
            '        End If
        End If
        Session("Title") = Legend




    End Sub

    Private Function getYAxisLabel() As String
        If Session("YAxisLabel") <> "" Then
            getYAxisLabel = Session("YAxisLabel")
            Exit Function
        End If
        getYAxisLabel = ""
    End Function

    Private Function getXAxisLabel(ByVal i As Integer) As String
        If Not IsNumeric(Session("GR4N")(1)) Then
            getXAxisLabel = Session("GR4N")(1)
            Exit Function
        End If
        getXAxisLabel = ""
    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/08/21
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter">URL Parameters</param>
    ''' <returns>Parameters transformed in a valid URL format</returns>
    Public Function HistogramPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        HistogramPrepareForFriendlyURL = tmp
    End Function



End Class
