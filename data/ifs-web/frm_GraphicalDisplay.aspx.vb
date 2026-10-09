'jrs 2011/07/15 Implemented by José Roberto Solórzano
Option Strict Off
Option Explicit On
Imports System.Xml
Imports Microsoft.AspNet.FriendlyUrls
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_GraphicalDisplay
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
    'Dim prevHorizonYear As Integer 'jam 2016-03-06

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim sValidationVar As String
        Dim ii As Short
        Dim j As Integer
        Dim i As Short

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othListNames As String
        Dim othHistFor As String
        Dim othGrpOp As String
        Dim othDim1 As String
        Dim othDim2 As String
        Dim othDim3 As String
        Dim othFile As String
        Dim othDisplayLastYear As String
        'Dim qtyParam As Integer
        Dim strParam(10) As String
        'Dim seqParam(8) As Integer

        'jam 2021/01/02 Commented condition "Or IsNothing(Session("GraphDisplayParent"))"
        'jam 2020/11/10
        If IsNothing(Session("qtyParam")) Then 'jam 2020/12/23 Arrays are redimmensioned always that are called for an url directly
            ' If IsNothing(Session("qtyParam")) Or IsNothing(Session("GraphDisplayParent")) Then
            ReDim Session("seqParam")(10)
            Session("qtyParam") = 0
            ReDim Session("RetParam")(10)

            'jam 2021/02/14
            Session("isLeg") = False
            Session("numLeg") = -99

            Session("isAlias") = False
            Session("numAlias") = -99

        End If

        'jam 2020/11/07
        If Not IsNothing(Request.QueryString("ListNames")) Or Not IsNothing(Request.QueryString("HistFor")) Or Not IsNothing(Request.QueryString("GrpOp")) _
            Or Not IsNothing(Request.QueryString("Dim1")) Or Not IsNothing(Request.QueryString("Dim2")) Or Not IsNothing(Request.QueryString("Dim3")) _
            Or Not IsNothing(Request.QueryString("File")) Or Not IsNothing(Request.QueryString("DisplayLastYear")) _
            Or Not IsNothing(Request.QueryString("Alias")) Or Not IsNothing(Request.QueryString("Leg")) Then

            '0-> ListNames
            '1-> HistFor
            '2-> GrpOp
            '3-> Dim1
            '4-> Dim2
            '5-> Dim3
            '6-> File
            '7-> DisplayLastYear
            '8-> Alias
            '9-> Leg

            'jam 2021/01/02
            ReDim Session("seqParam")(10)
            Session("qtyParam") = 0
            ReDim Session("RetParam")(10)


            If Not IsNothing(Request.QueryString("ListNames")) And Not Page.IsPostBack Then

                othListNames = Request.QueryString("ListNames")

                'strParam(qtyParam) = GrpPrepareForFriendlyURL(othListNames)

                'jrs 2024/04/15 added PrepareForURL
                'strParam(Session("qtyParam")) = PrepareForURL(othListNames)
                strParam(Session("qtyParam")) = othListNames

                Session("seqParam")(Session("qtyParam")) = 0


                Session("qtyParam") = Session("qtyPAram") + 1
            Else
                othListNames = Nothing

            End If

            If Not IsNothing(Request.QueryString("HistFor")) And Not Page.IsPostBack Then

                othHistFor = Request.QueryString("HistFor")


                strParam(Session("qtyParam")) = othHistFor
                Session("seqParam")(Session("qtyPAram")) = 1
                Session("qtyParam") = Session("qtyParam") + 1


            Else
                othHistFor = Nothing
            End If

            If Not IsNothing(Request.QueryString("GrpOp")) And Not Page.IsPostBack Then

                othGrpOp = Request.QueryString("GrpOp")


                strParam(Session("qtyParam")) = othGrpOp
                Session("seqParam")(Session("qtyParam")) = 2


                Session("qtyParam") = Session("qtyPAram") + 1


            Else
                othGrpOp = Nothing
            End If


            If Not IsNothing(Request.QueryString("Dim1")) And Not Page.IsPostBack Then

                othDim1 = Request.QueryString("Dim1")


                strParam(Session("qtyParam")) = othDim1
                Session("seqParam")(Session("qtyParam")) = 3

                Session("qtyParam") = Session("qtyParam") + 1



            Else
                othDim1 = Nothing
            End If


            If Not IsNothing(Request.QueryString("Dim2")) And Not Page.IsPostBack Then

                othDim2 = Request.QueryString("Dim2")


                strParam(Session("qtyParam")) = othDim2
                Session("seqParam")(Session("qtyParam")) = 4
                Session("qtyParam") = Session("qtyParam") + 1



            Else
                othDim2 = Nothing
            End If

            If Not IsNothing(Request.QueryString("Dim3")) And Not Page.IsPostBack Then

                othDim3 = Request.QueryString("Dim3")


                strParam(Session("qtyParam")) = othDim3
                Session("seqParam")(Session("qtyParam")) = 5
                Session("qtyParam") = Session("qtyParam") + 1


            Else
                othDim3 = Nothing
            End If

            If Not IsNothing(Request.QueryString("File")) And Not Page.IsPostBack Then

                othFile = Request.QueryString("File")

                strParam(Session("qtyParam")) = othFile
                Session("seqParam")(Session("qtyParam")) = 6
                Session("qtyParam") = Session("qtyParam") + 1


            Else
                othFile = Nothing
            End If

            If Not IsNothing(Request.QueryString("DisplayLastYear")) And Not Page.IsPostBack Then

                othDisplayLastYear = Request.QueryString("DisplayLastYear")


                strParam(Session("qtyParam")) = othDisplayLastYear

                'jam 2020/12/26
                'If Not IsNumeric(strParam(Session("qtyParam"))) Then
                '    strParam(Session("qtyParam")) = Session("LastYear")
                'End If
                Session("seqParam")(Session("qtyParam")) = 7
                Session("qtyParam") = Session("qtyParam") + 1


            Else
                othDisplayLastYear = Nothing
            End If

            'jam 2021/01/01
            'jam 2020/12/23
            'For i = 0 To Session("qtyParam") - 1
            '    strParam(i) = GrpPrepareForFriendlyURL(strParam(i))

            'Next



            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_GraphicalDisplay.aspx")

            'JAM 2021/01/28 Managing "/" exception in URL parameters
            For i = 0 To Session("qtyParam") - 1
                strParam(i) = strParam(i).Replace("/", "™") 'jam 2021-01-30 using TradeMark character like pivot
            Next i

            'jam 2021/05/19
            'vurl = FriendlyUrl.Href("~/frm_GraphicalDisplay", strParam)

            'jrs 2022/07/25 added Not IsNothing(othDim1) AndAlso 
            'jam 2022/04/25
            If othListNames.Length > 200 Or (Not IsNothing(othDim1) AndAlso othDim1.Length > 200) Then

                vurl = FriendlyUrl.Href("~/frm_GraphicalDisplay")

                'vurl = FriendlyUrl.Href("~/frm_GraphicalDisplay", strParam)

                ''jam 2022/04/24
                ''jam 2021/06/29
                'If othDim1.Length <= 150 Then
                '    vurl = FriendlyUrl.Href("~/frm_GraphicalDisplay", strParam)
                'Else
                '    vurl = FriendlyUrl.Href("~/frm_GraphicalDisplay")
                'End If

            Else
                vurl = FriendlyUrl.Href("~/frm_GraphicalDisplay", strParam)
            End If







            Response.Redirect(vurl)





            'If othCountry <> "" And othYear <> "" And othFile <> "" Then
            '    vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(othCountry), othYear, othFile)


            '    Response.Redirect(vurl)

            'Else
            '    If othCountry <> "" And othYear = "" And othFile = "" Then
            '        vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(othCountry))

            '        Response.Redirect(vurl)

            '    Else
            '        If othCountry <> "" And othYear <> "" And othFile = "" Then
            '            vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(othCountry), othYear)

            '            Response.Redirect(vurl)
            '        End If

            '    End If


            'End If


        End If



        'jam 2020/09/15
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String
        Dim vYear As String
        Dim vScenario As String
        Dim vqtyParm As Integer

        vqtyParm = segments.Count
        If Session("qtyParam") = 0 And vqtyParm > 0 Then 'jam 2021-01-31
            Session("qtyParam") = vqtyParm

        End If
        For i = 0 To vqtyParm - 1
            segments(i) = segments(i).Replace("™", "/") 'jam 2021-01-30 using TradeMark character like pivot
            Session("RetParam")(i) = segments(i)
            If Session("seqParam")(i) Is Nothing Then 'jam 2021-01-30 When URL is loaded directly

                If i <= 3 Then 'jam 2021-01-30 Setting the right index for URL parameters
                    Session("seqParam")(i) = i

                Else
                    If i = 4 Then  'jam 2021/01/31 Dim1 or File
                        If IsNumeric(segments(i)) Or vqtyParm = 6 Then 'jam 2022/02/26 when Scenario id isn't numeric
                            Session("seqParam")(i) = 6
                        Else
                            Session("seqParam")(i) = i
                        End If
                    Else
                        If i = 5 Then
                            If IsNumeric(segments(i)) Then 'jam 2021/01/31 DisplayYear or File
                                If segments(i).Length = 4 Then
                                    Session("seqParam")(i) = 7
                                Else
                                    Session("seqParam")(i) = 6
                                End If
                            Else 'jam 2021/01/31 Dim3
                                Session("seqParam")(i) = i
                            End If
                        Else
                            If i = 6 Then
                                If IsNumeric(segments(i)) Then 'jam 2021/01/31 DisplayYear or File
                                    If segments(i).Length = 4 Then
                                        Session("seqParam")(i) = 7
                                    Else
                                        Session("seqParam")(i) = 6
                                    End If
                                Else 'jam 2021/01/31 
                                    '0-> ListNames
                                    '1-> HistFor
                                    '2-> GrpOp
                                    '3-> Dim1
                                    '4-> Dim2
                                    '5-> Dim3
                                    '6-> File
                                    '7-> DisplayLastYear
                                    '8-> Alias
                                    '9-> Leg
                                    Session("seqParam")(i) = i
                                End If
                            Else

                                Session("seqParam")(i) = i


                            End If

                        End If

                    End If
                End If
            End If


        Next i








        If Not Page.IsPostBack Then
            'jrs 2015/08/28 removed buseAllHis because now it could be forecast by others comparison
            'If buseAllHis And FirstYearPE = FirstYearHistory Then
            'removed the historic condition, becasue forecast comparison could be comparing vs forecast only
            'jrs 2014/03/27
            'If Session("FirstYear") = FirstYearHistory Then

            'jam 2016-02-12 Defining AllYears Variables
            If Session("mnuUseAllYrs") = Nothing Then
                Session("mnuUseAllYrs") = False
            End If

            'jam 2016-02-12 Defining AllYears Variables
            If Session("mnuUseAllYrsTime") = Nothing Then
                Session("mnuUseAllYrsTime") = False
            End If

            For i = 1 To Session("COUNTE")
                'jrs 2024/11/24 added and Session("UserFirstYear") = Nothing
                If Session("StaYearHis")(i) > 0 And Session("StaYearHis")(i) < Session("FirstYear") And Session("UserFirstYear") = Nothing Then
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
            'jam 2020/12/22    
            If vqtyParm <> 0 Then 'jam 2021/02/13
                'jam 2020/12/22
                'If Not (Request.QueryString("ListNames") Is Nothing) Then
                If Not (Request.QueryString("ListNames") Is Nothing) Or ExistsParameter(0) <> -1 Then
                    'sListNames = Request.QueryString("ListNames")

                    sListNames = Session("RetParam")(ExistsParameter(0))

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
            Else 'jam 2020/12/22
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

            End If

            'jrs 2013/10/08
            If Not bReload Then
                'jam 2020/12/22    
                If vqtyParm <> 0 Then 'jam 2021/02/13
                    'jam 2020/12/22
                    'If Not (Request.QueryString("Dim1") Is Nothing) Then
                    If Not (Request.QueryString("Dim1") Is Nothing) Or ExistsParameter(3) <> -1 Then
                        If Not (Session("Dim1sLoaded") Is Nothing) Then
                            'If Session("Dim1sLoaded") <> PrepareForURL(Request.QueryString("Dim1")) Then 'jrs 2016/10/03 added PrepareForURL
                            If Session("Dim1sLoaded") <> PrepareForURL(Session("RetParam")(ExistsParameter(3))) Then 'jrs 2016/10/03 added PrepareForURL

                                bReload = True
                            End If
                        Else
                            bReload = True
                        End If
                    End If


                Else  'jam 2020/12/22
                    If Not (Request.QueryString("Dim1") Is Nothing) Then
                        If Not (Session("Dim1sLoaded") Is Nothing) Then
                            If Session("Dim1sLoaded") <> PrepareForURL(Request.QueryString("Dim1")) Then 'jrs 2016/10/03 added PrepareForURL
                                bReload = True
                            End If
                        Else
                            bReload = True
                        End If
                    End If
                End If
            End If


            'jam 2020/12/22    
            If vqtyParm <> 0 Then 'jam 2021/02/13

                'jrs 2013/01/18
                'If Not IsNothing(Request.QueryString("DisplayLastYear")) Then
                'jam 2020/12/22
                If Not IsNothing(Request.QueryString("DisplayLastYear")) Or ExistsParameter(7) <> -1 Then
                    'jrs 2014/01/02
                    'If Session("DisplayLastYear") <> Request.QueryString("DisplayLastYear") Then
                    If Session("DisplayLastYear") <> Session("RetParam")(ExistsParameter(7)) Then
                        bReload = True
                    End If
                    'Session("DisplayLastYear") = Request.QueryString("DisplayLastYear")


                    Session("DisplayLastYear") = Session("RetParam")(ExistsParameter(7))

                    'jam 2020/12/26
                    'If Not IsNumeric(Session("DisplayLastYear")) Then
                    '    Session("DisplayLastYear") = Session("LastYear")


                    'End If

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

            Else 'jam 2020/12/22
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

            End If



            'jrs 2011/04/05
            If Session("COUNTE") = 0 Or bReload Then
                'Read querystring
                Dim sHistFor As String, GrpOp As Integer
                'sHistFor = Request.QueryString("HistFor")
                'GrpOp = CInt(Request.QueryString("GrpOp"))


                'jam 2020/12/22    
                If vqtyParm <> 0 Then 'jam 2021/02/13 

                    If ExistsParameter(1) <> -1 Then
                        sHistFor = Session("RetParam")(ExistsParameter(1))
                        'jrs 2023/06/08
                        If UseOnlySQLite Then
                            If sHistFor = "True" Then
                                sHistFor = "1"
                            Else
                                sHistFor = "0"
                            End If
                        End If
                    Else 'jrs 2023/08/09
                        sHistFor = "0"
                    End If

                    If ExistsParameter(2) <> -1 Then
                        GrpOp = CInt(Session("RetParam")(ExistsParameter(2)))
                    End If

                Else
                    sHistFor = Request.QueryString("HistFor")

                    'jrs 2023/06/08
                    If UseOnlySQLite Then
                        If sHistFor = "True" Then
                            sHistFor = "1"
                        Else
                            sHistFor = "0"
                        End If
                    End If

                    GrpOp = CInt(Request.QueryString("GrpOp"))
                End If

                Dim sDim1 As String, sDim2 As String, sDim3 As String, sFiles As String

                'jam 2020/12/22    
                If vqtyParm <> 0 Then 'jam 2021/02/13
                    'jam 2020/12/22
                    If Not Request.QueryString("Dim1") Is Nothing Or ExistsParameter(3) <> -1 Then
                        'If Not Request.QueryString("Dim1") Is Nothing Then
                        'sDim1 = Request.QueryString("Dim1")
                        sDim1 = Session("RetParam")(ExistsParameter(3))
                        'jrs 2013/10/08
                        Session("Dim1sLoaded") = sDim1
                    Else
                        Session("Dim1sLoaded") = Nothing
                    End If
                    'If Not Request.QueryString("Dim2") Is Nothing Then
                    If Not Request.QueryString("Dim2") Is Nothing Or ExistsParameter(4) <> -1 Then
                        'sDim2 = Request.QueryString("Dim2")
                        sDim2 = Session("RetParam")(ExistsParameter(4))

                    End If
                    'If Not Request.QueryString("Dim3") Is Nothing Then
                    If Not Request.QueryString("Dim3") Is Nothing Or ExistsParameter(5) <> -1 Then
                        'sDim3 = Request.QueryString("Dim3")
                        sDim3 = Session("RetParam")(ExistsParameter(5))
                    End If
                    'sFiles = Request.QueryString("File")

                    If ExistsParameter(6) <> -1 Then
                        'sFiles = Request.QueryString("File")
                        sFiles = Session("RetParam")(ExistsParameter(6))
                    End If

                Else 'jam 2020/12/22
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
                End If

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

                'jrs 2024/11/15
                If IsNumeric(sFiles) AndAlso CInt(sFiles) > lstFiles.Items.Count Then
                    sFiles = "1" 'Reset
                End If

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
                        Session("bUseCtryAndGroup") = False
                        Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                    Case 1 'Groups
                        Session("bUseReg") = False
                        Session("bUseGroup") = True
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = False
                        Session("bUseCtryAndGroup") = False
                        Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                    Case 2 'Dec Groups
                        Session("bUseReg") = False
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = True
                        Session("bUseGList") = False
                        Session("bUseCtryAndGroup") = False
                        Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                    Case 3 'G-Lists
                        Session("bUseReg") = False
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = True
                        Session("bUseCtryAndGroup") = False
                        Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                    Case 4 'jrs 2016/11/10 Ctrys and Groups 
                        Session("bUseReg") = False
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = False
                        Session("bUseCtryAndGroup") = True
                        Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                    Case 5 'jrs 2025/05/30 Ctrys and Dec Groups 
                        Session("bUseReg") = False
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = False
                        Session("bUseCtryAndGroup") = False
                        Session("bUseCtryAndGroupDec") = True
                End Select

                'Set List Boxes
                URLToDimLst(sDim1, lstDim, Session("bUseCtryAndGroup")) 'jrs 2021/04/12
                URLToDimLst(sDim2, lstDim2)
                URLToDimLst(sDim3, lstDim3)
                URLToFileLst(sFiles, lstFiles, Session("txtFiles"), Session("IFsRunFiles"), Session("PDFilesIdxs"), Session("FilesSelected"),
                Session("RunFiles"), Session("PutFdrvRun2"), Session("ShortSce"), Session("ScenarioFileDir"))

                'jrs 2013/08/09 new location for declaration of IfsPars, to pick up any changes in RunFiles (loading Preloaded scenarios)
                Dim IfsPars As New myParameters
                IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
                Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
                Session("PutFdrvRun"), Session("CurrencySym"))

                'jrs 2024/11/22
                Session("MsgParent") = "~/frm_PackagedDisplaysFlex.aspx"

                'jrs 2025/07/17 updating function to the one used in Flex Displays, loading directly with URL was not working
                'jrs 2023/07/25
                'Load Package
                'gLoadPackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                'Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, Session("TableTitle"),
                'Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
                'Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
                'Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
                'Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
                'Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
                'Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
                'Session("PopMales"), Session("PopFemales"), sListNames, Session("sUsrDefCats"), Session("txtFiles"), vmnuSQLITE:=Session("mnuSQLite"))
                gLoadPackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, Session("TableTitle"), Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
                Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("bUseCtryAndGroupDec"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
                Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
                Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
                Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
                Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
                Session("PopMales"), Session("PopFemales"), sListNames, Session("sUsrDefCats"), "", "", "", "", Nothing,
                Nothing, Nothing, vmnuSQLITE:=Session("mnuSQLite"), OnlyCoreGroups:=Session("bCheckHideNonCoreGroups"),
                IFsRunFiles:=Session("IFsRunFiles"))

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

                'jrs 2025/06/13 added or  Or Session("oGR1N") Is Nothing
                'jrs 2021/02/08
                If Session("bAlias") = False Or Session("oGR1N") Is Nothing Then
                    'jrs 2023/08/08
                    'If Session("bComparisonOn") <> True Then
                    'jrs 2010/02/04
                    ReDim Session("oGR1N")(Session("COUNTE"))
                    'End If
                End If

                For i = 1 To Session("COUNTE")
                    'jrs 2026/03/05 added .lenght > i andAlso
                    'jrs 2021/02/08
                    If Session("bAlias") = False Or (Session("oGR1N").length > i AndAlso Session("oGR1N")(i) = "") Then
                        Session("oGR1N")(i) = Session("GR1N")(i)
                    End If
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
                    'jrs 2025/03/10 added 2nd condition, to avoid putting bilateral variables in the map
                    'jrs 2023/11/14 added condition
                    If VDim1(IfsVars.Item(sVar).Index) = 1 AndAlso VDim2(IfsVars.Item(sVar).Index) <> 1 Then
                        Session("MMVarName") = sVar
                        Session("MMDim2") = Session("GR2N")(1)
                        Session("MMDim3") = Session("GR3N")(1)
                    End If
                End If
            End If
            'jrs 2011/07/04
            If Session("bAlias") = Nothing Then
                Session("bAlias") = False
            ElseIf Session("bAlias") = True Then
                Session("bAlias") = False
                mnuAlias_Click()
            End If
            'jrs 2024/09/04
            If Session("ShowVersion") = Nothing Then
                Session("ShowVersion") = True
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
                        ReDim Session("BDPOP")(AMAX(Session("FinYear")(1), Session("LastYear") - Session("FirstYear") + 1), pCOUNTE / 2)
                        LoadBD("POP", Session("BDPOP"))
                    End If
                    If Session("sVarZ") <> "GDP" Then
                        ReDim Session("BDGDP")(AMAX(Session("FinYear")(1), Session("LastYear") - Session("FirstYear") + 1), pCOUNTE / 2)
                        LoadBD("GDP", Session("BDGDP"))
                    End If
                    Session("NoneOrder") = VB6.Support.CopyArray(Session("GR1N"))
                    '        'jrs 2010/10/13
                    '        If bBubble Then
                    '            SetUpManualScaleToMinMaxZ()
                    '        End If

                End If
                'jrs 2010/09/29
                Dim FirstYearNotNull As Integer
                FirstYearNotNull = FindFirstYearNotNull()
                'jrs 2010/11/01
                If Session("PYear") > FirstYearNotNull Then
                    Session("PYear") = FirstYearNotNull
                End If
                'jrs 2012/06/01
                If Session("mnuUseAllYrsTime") Is Nothing Then
                    Session("mnuUseAllYrsTime") = False
                    Session("mnuUseAllYrs") = False
                End If
            End If

            'jam 2020/12/22
            If vqtyParm <> 0 Then
                'jrs 2012/07/03
                ' If Not IsNothing(Request.QueryString("Alias")) Then
                If Not IsNothing(Request.QueryString("Alias")) Or ExistsParameter(8) <> -1 Then
                    Dim sAlias As String
                    'sAlias = Request.QueryString("Alias")
                    sAlias = Session("RetParam")(ExistsParameter(8))
                    If (Not Session("bAlias") And sAlias = "1") Or
                    (Session("bAlias") And sAlias = "0") Then
                        mnuAlias_Click()
                    End If
                End If
            Else
                'jrs 2012/07/03
                If Not IsNothing(Request.QueryString("Alias")) Or (Session("GraphDisplayParent") = "frm_DisplayMenu.aspx" And Session("IsAlias")) Then 'jam 2021/02/14
                    Dim sAlias As String
                    sAlias = Request.QueryString("Alias")

                    'jam 2021/02/14
                    If sAlias Is Nothing Then
                        sAlias = Session("numAlias")

                    End If

                    If (Not Session("bAlias") And sAlias = "1") Or
                        (Session("bAlias") And sAlias = "0") Then
                        mnuAlias_Click()
                    End If
                End If
            End If

            'jam 2020/12/22
            If vqtyParm <> 0 Then
                'jam 2020/12/22
                'If Not IsNothing(Request.QueryString("Leg")) Then
                If Not IsNothing(Request.QueryString("Leg")) Or ExistsParameter(9) <> -1 Then
                    Dim sLeg As String
                    'sLeg = Request.QueryString("Leg")

                    sLeg = Session("RetParam")(ExistsParameter(9))
                    'jrs 2026/04/27
                    Session("bHideLegends") = False

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
                        Case "6" 'jrs 2026/04/27
                            Session("bHideLegends") = True
                        Case "9"
                            mnuFormLegReset_Click()
                    End Select
                End If

            Else
                If Not IsNothing(Request.QueryString("Leg")) Or (Session("GraphDisplayParent") = "frm_DisplayMenu.aspx" And Session("IsLeg")) Then 'jam 2021/02/14
                    Dim sLeg As String
                    sLeg = Request.QueryString("Leg")

                    'jam 2021/02/14
                    If sLeg Is Nothing Then
                        sLeg = Session("numLeg")

                    End If
                    'jrs 2026/04/27
                    Session("bHideLegends") = False

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
                        Case "6" 'jrs 2026/04/27
                            Session("bHideLegends") = True
                        Case "9"
                            mnuFormLegReset_Click()
                    End Select
                End If

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
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadMenu()
        'cdv 2021/11/04

        'jrs 2023/10/18 added And Session("RowAdjustment") <> FirstYearForecast - FirstYearHistory + 1
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
        'jam 2016-01-11
        If Not Session("fromTableDisp") Then
            If Session("bTrillionsOk") Is Nothing Then
                Session("bTrillionsOk") = False
                Session("prevHorizonYear") = 1
                ReDim Session("PLOTTrillions")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2)) 'jam 2016-05-16
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

        'dsb 2025/12/07 Base-year red line default for line graph
        If Not Page.IsPostBack Then
            If Session("GType") = "LINE" Then
                Dim startYear As Integer = Session("FirstYear")
                Dim baseYear As Integer = FirstYearForecast

                If startYear < baseYear Then
                    If Session("VLYear") Is Nothing Then
                        Session("VLYear") = baseYear
                    End If
                Else
                    Session.Remove("VLYear")
                End If
            End If
        End If

        DisplayGraph()

        'jrs 2023/07/18
        If Not Page.IsPostBack Then
            If IsNothing(Request.QueryString("Alias")) AndAlso ExistsParameter(8) = -1 Then
                'jrs 2023/10/03
                If Session("LabelChanged") = Nothing OrElse Session("LabelChanged") = False Then
                    'jrs 2023/08/09
                    If Session("GraphDisplayParent") <> "frm_TableDisplay.aspx" Then
                        'jrs 2023/10/12 Only use it to turn on Alias the first time
                        If Session("bAlias") = False Then
                            mnuAlias_Click()
                        End If
                    End If
                End If
            End If
            If IsNothing(Request.QueryString("Leg")) AndAlso ExistsParameter(9) = -1 Then
                If Session("COUNTE") > 1 AndAlso OnlyUniqueFirstDimensions() Then
                    mnuFormLeg1stDim_Click()
                End If
            End If
        End If

        PackJS()

        'jrs 2023/10/23
        If Session("OriginalFirstYear") <> Nothing AndAlso Session("RowAdjustment") > 0 Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " (Start year Option Is On, there could be data In previous years)"
        End If

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




        'jam 2016-03-06
        If Not Session("fromTableDisp") Then

            Call Convert_Billions_To_Trillions()
        Else

            'jrs 2025/09/18 Why do we need to reset PLOTTrillions?
            'It was already set in TableDisplay
            'And if there options like Percent that have changed the PLOT, this messes up the array
            'For cc = 1 To Session("COUNTE")
            '    For rr = 1 To Session("DisYear")
            '        Session("PLOTTrillions")(rr, cc) = Session("PLOT")(rr, cc) 'jam 2016-05-16
            '    Next rr
            'Next cc
        End If



        'jrs 2022/07/26 testing more points, 25k is a problem with history plus forecast with 2 vars (50k), 11 vars needs (Water list) 300k
        If Session("GType") <> "SCAT" And Session("GType") <> "SCAT1" Then
            'jrs 2022/07/26 If Session("DisYear") * pCOUNTE > 25000 Then
            If Session("DisYear") * pCOUNTE > 300000 Then
                Session("DisYear") = 300000 / pCOUNTE
            End If
        Else
            'jrs 2022/07/26 If Session("DisYear") * (pCOUNTE / 2) > 25000 Then
            If Session("DisYear") * (pCOUNTE / 2) > 300000 Then
                Session("DisYear") = 300000 / (pCOUNTE / 2)
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
        'jam 2020/12/19
        'cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif")
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

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_4a6c"

        'jam 2020/12/22
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"



        ''If Request.QueryString("ListNames") Is Nothing Then
        ''    'jam 2013-04-09 Adding showhg(); for hourglass cursor
        ''    cswmGroup1.OnMenuBarClick = "showhg();location.href='" & Session("GraphDisplayParent") & "';"

        ''Else
        ''    Dim sLst As String
        ''    sLst = Request.QueryString("ListNames")
        ''    sLst = Replace(sLst, """,""", ",,")
        ''    sLst = Mid(sLst, 2, Len(sLst) - 2)
        ''    'jrs 2011/11/03 
        ''    'Mark found that the list Foreign Reserves as Percent of GDP didn't work
        ''    'there were 2 weird characters after the word Reserves, remove them
        ''    sLst = Replace(sLst, Chr(127), " ")
        ''    'jam 2013-04-09 Adding showhg(); for hourglass cursor
        ''    cswmGroup1.OnMenuBarClick = "showhg();location.href='" & Session("GraphDisplayParent") & "?ListNames=" & sLst & "';"
        ''End If

        'jrs 2026/08/07
        If StandAloneMode Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Main Menu"
            cswmGroup1.ID = "mnuMainMenu"



            'jam 2020/12/21
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Main Menu"
        End If

        'jam 2020/12/21
        'cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"


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
            cswmItem.Caption = "Use a Decomposed Group"

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
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Logarithmic"
                If Session("RegressType") = "LOG" Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Exponential"
                If Session("RegressType") = "EXP" Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Power"
                If Session("RegressType") = "POW" Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Polynomial Second Degree"
                cswmItem.ID = "mnuPolSec"

                If Session("RegressType") = "POLYNOMIAL(Second Degree)" Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Polynomial Third Degree"
                cswmItem.ID = "mnuPolThird"

                If Session("RegressType") = "POLYNOMIAL(Third Degree)" Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If

                'jam 2011-01-22 Logistic option
                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Logistic"

                If Session("mnuLogistic") Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "Turn formulation display off"
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22

                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = "No Regression"
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22

                'jam 2020/12/20 Moved
                'jam 2016-02-12 Adding AllYears Options
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Use all Years (in Statistics)"


                'jam 2016-02-12
                If Session("mnuUseAllYrs") Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If


                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Use all Years and Time (in Statistics)"

                'jam 2016-02-12 
                If Session("mnuUseAllYrsTime") Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If



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
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If
            ElseIf Session("BDCheck") Is Nothing Then
                Session("BDCheck") = "None"
            End If

            If Session("sVarZ") <> "POP" Then
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "POP"
                cswmItem.ID = "mnuBDPOP"
                If Session("BDCheck") = "POP" Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If
            End If

            If Session("sVarZ") <> "GDP" Then
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "GDP"
                cswmItem.ID = "mnuBDGDP"
                If Session("BDCheck") = "GDP" Then
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                Else
                    cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "None"
            cswmItem.ID = "mnuBDNone"
            If Session("BDCheck") = "None" Then
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            Else
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
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
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            Else
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.IconHeight = 16
            cswmItem.Caption = "Common"
            cswmItem.IconHeight = 16

            If Session("PE_CommonMenu") = "Common" Then
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            Else
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            End If


            'jrs 2014/03/21
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.IconHeight = 16
            cswmItem.Caption = "Index Base 1"
            cswmItem.IconHeight = 16
            If Session("PE_CommonMenu") = "Base1" Then
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            Else
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            End If

        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Format"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"
        'CDV 2021/10/22
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Start Year"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display/Run Horizon"

        'jrs 2026/05/04 *** start ***
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Custom Font Size"

        Dim cswmGroup3 As Coalesys.WebMenu.Group
        cswmGroup3 = cswmItem.AddGroup()

        For i = 2 To 25
            cswmItem = cswmGroup3.Items.Add()
            cswmItem.Caption = i.ToString
            If Session("CustomFontSize") = i Then
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            Else
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
            End If
        Next
        'jrs 2026/05/04 *** end ***

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Alias for Group/Country Names" 'jrs 2021/02/08
        If Session("bAlias") Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
        End If

        'jrs 2024/09/04
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Version in Graph"
        If Session("ShowVersion") Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
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

        'jrs 2026/04/27
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend No Labels"

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

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'jrs 2012/03/19
        Dim sOther As String = ""
        If Session("sVarz") > "" Then
            sOther = Session("sVarz")
        End If

        'Choose the correct function
        Select Case objEA.Item.Caption
                'Case "Continue"
                '    mnuContinue_Click(sender, e)
            Case "Continue" 'jam 2020/12/05
                mnuExit_Click(False)
            Case "Main Menu" 'jam 2020/12/21
                mnuExit_Click(True)
            Case "Save", "Print"
            Case "Common" '"Delete Selected Driver/Dimension"
                'jrs 2014/03/21 mnuCommon_Click(sender, e)
                objSender.Groups("Scaling").Items("Common").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                objSender.Groups("Scaling").Items("No Scaling").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                objSender.Groups("Scaling").Items("Index Base 1").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                Session("CommonPlot") = "True"
                Session("PE_CommonMenu") = "Common"
                PackJS()
            Case "No Scaling" 'jrs 2014/03/21
                objSender.Groups("Scaling").Items("Common").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                objSender.Groups("Scaling").Items("No Scaling").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                objSender.Groups("Scaling").Items("Index Base 1").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                Session("CommonPlot") = "False"
                Session("PE_CommonMenu") = "Scale"
                PackJS()
            Case "Index Base 1" 'jrs 2014/03/21
                objSender.Groups("Scaling").Items("Common").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                objSender.Groups("Scaling").Items("No Scaling").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                objSender.Groups("Scaling").Items("Index Base 1").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
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
            Case "Set Title, Display Interval, or Year" 'jrs 2005/10/19
                'jrs 2012/07/23 Session("DisplayControlParent") = "frm_GraphicalDisplay.aspx"
                Session("DisplayControlParent") = Request.Url.AbsoluteUri
                Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_DisplayControl.aspx"))

                    'jam 2020/11/23
                    'Vpath = Request.GetFriendlyUrlFileVirtualPath

                    'Vext = Request.GetFriendlyUrlFileExtension

                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_DisplayControl.aspx")


                    'vurl = FriendlyUrl.Href("~/frm_DisplayControl")

                    'Response.Redirect(vurl)
                    'CDV 2021/10/22 ****START****
            Case "Change Start Year"
                Session("DisplayControlParent") = Request.Url.AbsoluteUri
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_StartYearControl.aspx"))
                    'CDV 2021/10/22 ****END****
            Case "Display/Run Horizon" 'jrs 2011/07/04
                mnuDisplayRunHorizon_Click(sender, e)
            Case "Use Alias for Group/Country Names" 'jrs 2011/07/04 'jrs 2021/02/08 added Country Names
                'jrs 2023/08/09
                If Session("qtyParam") = 0 Then
                    mnuAlias_Click()
                    'jrs 2023/08/09
                    PackJS()
                Else
                    'jrs 2012/07/02
                    If Not Session("bAlias") Then
                        Response.Redirect(setAliasURL("1"))
                    Else
                        Response.Redirect(setAliasURL("0"))
                    End If
                End If
                    'mnuAlias_Click()
            Case "Show Version in Graph" 'jrs 2024/09/04
                If Session("ShowVersion") Then
                    Session("ShowVersion") = False
                    cswmWebMenu.Groups("Display Format").Items("Show Version in Graph").IconImage = ""
                Else
                    Session("ShowVersion") = True
                    cswmWebMenu.Groups("Display Format").Items("Show Version in Graph").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
                End If
                PackJS()
            Case "Format Legend Labels"
                'jrs 2012/07/23 Session("GraphLabelsParent") = "frm_GraphicalDisplay.aspx"
                Session("GraphLabelsParent") = Request.Url.AbsoluteUri
                Support.FormManager.StateStore(Me)

                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_GraphLabels.aspx"))


                    'jam 2020/11/23
                    'Vpath = Request.GetFriendlyUrlFileVirtualPath

                    'Vext = Request.GetFriendlyUrlFileExtension

                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_GraphLabels.aspx")


                    'vurl = FriendlyUrl.Href("~/frm_GraphLabels")

                    'Response.Redirect(vurl)



            Case "Format Legend Labels First Dimension Only" 'jrs 2008/01/21
                'jrs 2023/08/09
                If Session("qtyParam") = 0 Then
                    mnuFormLeg1stDim_Click()
                    PackJS()
                Else
                    'jrs 2012/07/02
                    Response.Redirect(setLegURL("1"))
                End If
                    'mnuFormLeg1stDim_Click()
            Case "Format Legend Labels Second Dimension Only" 'jrs 2007/08/10
                'jrs 2023/08/09
                If Session("qtyParam") = 0 Then
                    mnuFormLeg2ndDim_Click()
                    PackJS()
                Else
                    'jrs 2012/07/02
                    Response.Redirect(setLegURL("2"))
                End If
                    'jrs 2012/07/02
                    'mnuFormLeg2ndDim_Click()
            Case "Format Legend Labels Third Dimension Only" 'jrs 2008/01/24
                'jrs 2023/08/09
                If Session("qtyParam") = 0 Then
                    mnuFormLeg3rdDim_Click()
                    PackJS()
                Else
                    'jrs 2012/07/02
                    Response.Redirect(setLegURL("3"))
                End If

                    'mnuFormLeg3rdDim_Click()
            Case "Format Legend Labels Dimensions Only" 'jrs 2008/01/24
                'jrs 2023/08/09
                If Session("qtyParam") = 0 Then
                    mnuFormLegDims_Click()
                    PackJS()
                Else
                    'jrs 2012/07/02
                    Response.Redirect(setLegURL("4"))
                End If
                    'mnuFormLegDims_Click()
            Case "Format Legend Labels No Dimensions" 'jrs 2008/01/24
                'jrs 2023/08/09
                If Session("qtyParam") = 0 Then
                    mnuFormLegNoDim_Click()
                    PackJS()
                Else
                    'jrs 2012/07/02
                    Response.Redirect(setLegURL("0"))
                End If

                    'mnuFormLegNoDim_Click()
            Case "Format Legend No Labels" 'jrs 2026/04/27
                If Session("qtyParam") = 0 Then
                    mnuFormLegNoLabels_Click()
                    PackJS()
                Else
                    Response.Redirect(setLegURL("6"))
                End If

            Case "Format Legend Labels Scenario Names"
                'jrs 2023/08/09
                If Session("qtyParam") = 0 Then
                    mnuFormLegSce_Click()
                    PackJS()
                Else
                    'jrs 2012/07/02
                    Response.Redirect(setLegURL("5"))
                End If

                    'mnuFormLegSce_Click()
            Case "Format Legend Labels Reset"
                'jrs 2023/08/09
                If Session("qtyParam") = 0 Then
                    mnuFormLegReset_Click()
                    PackJS()
                Else
                    'jrs 2012/07/02
                    Response.Redirect(setLegURL("9"))
                End If
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
            Case "Use a Decomposed Group"
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
                'jrs 2025/07/11
                Session("RSQR") = ""

                PackJS()
            Case "Statistics" 'jrs 2012/06/04
                mnuStatistics_Click()
            Case "Use all Years (in Statistics)" 'jam 2016-02-12
                AllYears()
                If Session("mnuUseAllYrs") Then
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                Else
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If


            Case "Use all Years and Time (in Statistics)" 'jam 2016-02-12
                AllYearsAndTime()
                If Session("mnuUseAllYrsTime") Then
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                Else
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
                End If


        End Select

        'jrs 2026/05/04
        If IsNumeric(objEA.Item.Caption) Then
            Session("CustomFontSize") = objEA.Item.Caption
            PackJS()
            For i = 2 To 25
                If Session("CustomFontSize") = i Then
                    objSender.Groups("Display Format").Items("Custom Font Size").Group.Items(i.ToString).IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
                Else
                    objSender.Groups("Display Format").Items("Custom Font Size").Group.Items(i.ToString).IconImage = Page.ResolveUrl("~/Images/blank.gif")
                End If
            Next
        End If
    End Sub

    'jrs 2012/07/02
    Private Function setLegURL(ByVal sOpt As String) As String
        'Dim sUrl As String, sLeg As String
        'sUrl = Request.Url.AbsoluteUri
        'If Not IsNothing(Request.QueryString("Leg")) Then
        '    sLeg = Request.QueryString("Leg")
        '    sUrl = Replace(sUrl, "Leg=" & sLeg, "Leg=" & sOpt)
        'Else
        '    If InStr(sUrl, "&") > 0 Or InStr(sUrl, "?") > 0 Then
        '        sUrl = sUrl & "&"
        '    Else
        '        sUrl = sUrl & "?"
        '    End If
        '    sUrl = sUrl & "Leg=" & sOpt
        'End If

        'setLegURL = sUrl

        Dim sUrl As String, sLeg As String
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim newParameters(10) As String
        Dim i As Integer


        For i = 0 To Session("qtyPAram") - 1
            newParameters(i) = Session("RetParam")(i)

        Next i


        'jam 2020/11/23
        If IsNothing(Session("qtyParam")) Then

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





            'setLegURL = sUrl

        Else 'jam 2020/12/22



            If ExistsParameter(9) <> -1 Then

                Session("RetParam")(ExistsParameter(9)) = sOpt
                'jam 2020/12/21
                newParameters(ExistsParameter(9)) = sOpt


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_GraphicalDisplay.aspx")

                'sUrl = FriendlyUrl.Href("~/frm_GraphicalDisplay", Session("RetParam"))


                sUrl = FriendlyUrl.Href("~/frm_GraphicalDisplay", newParameters)

            Else
                'jam 2021/02/14
                If Session("GraphDisplayParent") <> "frm_DisplayMenu.aspx" Then
                    Session("qtyParam") = Session("qtyPAram") + 1

                    Session("seqParam")(Session("qtyParam") - 1) = 9

                    Session("RetParam")(Session("qtyPAram") - 1) = sOpt

                    newParameters(Session("qtyPAram") - 1) = sOpt

                    'jam 2021/02/14
                    Session("isLeg") = False

                Else
                    'jam 2021/02/14
                    Session("isLeg") = True
                    Session("numLeg") = sOpt

                End If
                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_GraphicalDisplay.aspx")

                'sUrl = FriendlyUrl.Href("~/frm_GraphicalDisplay", Session("RetParam"))

                sUrl = FriendlyUrl.Href("~/frm_GraphicalDisplay", newParameters)


            End If




        End If

        setLegURL = sUrl



    End Function

    'jrs 2012/07/03
    Private Function setAliasURL(ByVal sOpt As String) As String
        'Dim sUrl As String, sAlias As String
        'sUrl = Request.Url.AbsoluteUri
        'If Not IsNothing(Request.QueryString("Alias")) Then
        '    sAlias = Request.QueryString("Alias")
        '    sUrl = Replace(sUrl, "Alias=" & sAlias, "Alias=" & sOpt)
        'Else
        '    If InStr(sUrl, "&") > 0 Or InStr(sUrl, "?") > 0 Then
        '        sUrl = sUrl & "&"
        '    Else
        '        sUrl = sUrl & "?"
        '    End If
        '    sUrl = sUrl & "Alias=" & sOpt
        'End If

        'setAliasURL = sUrl

        Dim sUrl As String, sAlias As String

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim newParameters(10) As String
        Dim i As Integer


        'jam 2020/12/21
        For i = 0 To Session("qtyPAram") - 1
            newParameters(i) = Session("RetParam")(i)

        Next i


        If IsNothing(Session("qtyParam")) Then
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


        Else 'jam 2020/12/22

            If ExistsParameter(8) <> -1 Then

                Session("RetParam")(ExistsParameter(8)) = sOpt
                'jam 2020/12/21
                newParameters(ExistsParameter(8)) = sOpt

                'jam 2021/01/01
                'jam 2020/12/23
                'For i = 0 To Session("qtyParam") - 1
                '    newParameters(i) = GrpPrepareForFriendlyURL(newParameters(i))

                'Next

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_GraphicalDisplay.aspx")

                'sUrl = FriendlyUrl.Href("~/frm_GraphicalDisplay", Session("RetParam"))

                sUrl = FriendlyUrl.Href("~/frm_GraphicalDisplay", newParameters)

            Else

                'jam 2021/02/14
                If Session("GraphDisplayParent") <> "frm_DisplayMenu.aspx" Then


                    Session("qtyParam") = Session("qtyPAram") + 1

                    Session("seqParam")(Session("qtyParam") - 1) = 8

                    Session("RetParam")(Session("qtyPAram") - 1) = sOpt



                    newParameters(Session("qtyPAram") - 1) = sOpt

                    'jam 2021/02/14
                    Session("isAlias") = False

                Else

                    'jam 2021/02/14
                    Session("isAlias") = True
                    Session("numAlias") = sOpt

                End If

                'jam 2021/01/01
                'jam 2020/12/23
                'For i = 0 To Session("qtyParam") - 1
                '    newParameters(i) = GrpPrepareForFriendlyURL(newParameters(i))

                'Next


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_GraphicalDisplay.aspx")

                'sUrl = FriendlyUrl.Href("~/frm_GraphicalDisplay", Session("RetParam"))

                sUrl = FriendlyUrl.Href("~/frm_GraphicalDisplay", newParameters)

            End If

        End If
        setAliasURL = sUrl


    End Function

    Private Sub mnuAlias_Click()
        Dim i As Integer
        If Not Session("bAlias") Then
            Session("bAlias") = True
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("Display Format").Items("Use Alias for Group/Country Names").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            End If

            'Read and load all aliases
            Dim Tbl As DataTable, Tbl2 As DataTable 'jrs 2021/02/08
            Dim s As String
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT GroupName, Alias FROM " & GroupIDNew, "GroupName", Tbl)
            'jrs 2021/02/08
            Dim RegIdx As Integer
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT RegionNum, RegionAlias FROM " & RegionIDNew, "RegionNum", Tbl2)

            For i = 1 To Session("COUNTE")
                s = SeekValue(Tbl, Session("GR1N")(i), "Alias")
                If s > "" Then
                    Session("GR1N")(i) = s
                Else 'jrs 2021/02/08
                    RegIdx = IFsRegs.Item(Session("GR1N")(i)).Index
                    If RegIdx > 0 Then
                        s = SeekValue(Tbl2, RegIdx, "RegionAlias")
                        If s > "" Then
                            Session("GR1N")(i) = s
                        End If
                    End If
                End If
            Next i
        Else
            Session("bAlias") = False
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("Display Format").Items("Use Alias for Group/Country Names").IconImage = ""
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

    'jrs 2026/06/04
    Private Sub mnuFormLegNoLabels_Click()
        Dim I As Integer
        For I = 1 To Session("COUNTE")
            Session("VARN")(0, I) = ""
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
        'jrs 2011/09/20
        Dim sJscript2 As String

        'jam 2025/09/01 Changing concatenation process
        sJscript2 = ""

        Dim sb As New StringBuilder(sJscript2)

        'jam 2025/09/01 Changing concatenation process
        'sJscript2 = "<script>" & vbCr
        sb.Append("<script>" & vbCr)

        If Session("GType") = "SCAT" And Session("bBubble") = True Then
            'jrs 2020/10/05 sJscript2 = "<script src=""http://code.highcharts.com/highcharts-more.js""></script>" & vbCr
            'moved to local file
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = "<script src=""Scripts/highcharts-more.js""></script>" & vbCr
            'sJscript2 = sJscript2 & "<script>" & vbCr

            sb.Clear()
            sb.Append("<script src=""Scripts/highcharts-more.js""></script>" & vbCr)
            sb.Append("<script>" & vbCr)


        End If
        'jam 2025/09/01 Changing concatenation process
        'sJscript2 = sJscript2 & "            $(function () {" & vbCr
        sb.Append("            $(function () {" & vbCr)

        'jrs 2024/10/30 This is not working anymore, it asks for access to the server and we want to keep everything local
        'sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        'sJscript2 = sJscript2 & "lang: {" & vbCr
        'sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "});" & vbCr

        'jam 2025/09/01 Changing concatenation process
        'sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        sb.Append("$('#container').highcharts({" & vbCr)

        'jam 2025/09/01 Changing concatenation process
        'jam 2025/02/15 New highcharts version libraries
        'sJscript2 = sJscript2 & "exporting: {" & vbCr
        'sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        'sJscript2 = sJscript2 & "chart: {" & vbCr


        sb.Append("exporting: {" & vbCr)
        sb.Append("fallbackToExportServer: false" & vbCr)
        sb.Append("}," & vbCr)

        sb.Append("chart: {" & vbCr)

        If Session("GType") = "BAR" Then
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & "type: 'column'," & vbCr

            sb.Append("type: 'column'," & vbCr)

        ElseIf Session("GType") = "PIE" Then
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & "type: 'pie'," & vbCr

            sb.Append("type: 'pie'," & vbCr)

        ElseIf Session("GType") = "SCAT" Then
            If Session("bBubble") = True Then
                'jam 2025/09/01 Changing concatenation process
                'sJscript2 = sJscript2 & "type: 'bubble'," & vbCr

                sb.Append("type: 'bubble'," & vbCr)

            Else
                'jam 2025/09/01 Changing concatenation process
                'sJscript2 = sJscript2 & "type: 'scatter'," & vbCr

                sb.Append("type: 'scatter'," & vbCr)

            End If
        ElseIf Session("GType") = "STACKEDAREA" Then 'jrs 2018/05/15
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & "type: 'area'," & vbCr

            sb.Append("type: 'area'," & vbCr)
        End If

        'jam 2025/09/01 Changing concatenation process
        'sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        'sJscript2 = sJscript2 & "panning: true," & vbCr
        'sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        'sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        'sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        'sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        'sJscript2 = sJscript2 & "title: {" & vbCr

        sb.Append("zoomType: 'xy'," & vbCr)
        sb.Append("panning: true," & vbCr)
        sb.Append("panKey: 'shift'," & vbCr)
        sb.Append("borderWidth: 3," & vbCr)
        sb.Append("borderRadius: 15," & vbCr)
        sb.Append("spacingLeft: 60" & vbCr)
        sb.Append("}," & vbCr)
        sb.Append("title: {" & vbCr)


        If Session("TableTitle") > "" And Session("TableTitle") <> "Title" Then
            'jrs 2023/05/02 added PrepareForHTML
            'jrs 2024/03/13 added If Session("bTrillionsOk") Then
            If Session("bTrillionsOk") Then
                'jam 2025/09/01 Changing concatenation process
                'sJscript2 = sJscript2 & "text:       '" & PrepareForHTML(Replace(Session("TableTitle"), "Billion", "Trillion")) & "'," & vbCr

                sb.Append("text:       '" & PrepareForHTML(Replace(Session("TableTitle"), "Billion", "Trillion")) & "'," & vbCr)
            Else
                'jam 2025/09/01 Changing concatenation process
                'sJscript2 = sJscript2 & "text:       '" & PrepareForHTML(Session("TableTitle")) & "'," & vbCr

                sb.Append("text:       '" & PrepareForHTML(Session("TableTitle")) & "'," & vbCr)
            End If
        Else
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & "text:       ''," & vbCr

            sb.Append("text:       ''," & vbCr)

        End If
        'jam 2025/09/01 Changing concatenation process
        'jrs 2023/07/18
        'sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '30px'}," & vbCr

        'sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        sb.Append("style: {fontWeight: 'bold', fontSize: '30px'}," & vbCr)

        sb.Append("x: -20 //center" & vbCr)
        sb.Append("}," & vbCr)



        If Session("GType") = "PIE" Then
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & "subtitle: {" & vbCr
            'sJscript2 = sJscript2 & "text:       ""Year " & CStr(Session("FirstYear") + Session("PYear") - 1) & """," & vbCr
            'sJscript2 = sJscript2 & "x: -20, //center" & vbCr
            ''jrs 2024/12/09
            'sJscript2 = sJscript2 & "style: {
            'color: 'black',
            'fontSize: '25px'
            '}" & vbCr
            'sJscript2 = sJscript2 & "}," & vbCr

            sb.Append("subtitle: {" & vbCr)
            sb.Append("text:       ""Year " & CStr(Session("FirstYear") + Session("PYear") - 1) & """," & vbCr)
            sb.Append("x: -20, //center" & vbCr)
            'jrs 2024/12/09
            sb.Append("style: {
            color: 'black',
            fontSize: '25px'
            }" & vbCr)
            sb.Append("}," & vbCr)

        ElseIf Session("RSQR") > "" Then 'jrs 2025/07/11
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & "subtitle: {" & vbCr
            'sJscript2 = sJscript2 & "text:       """ & Session("RSQR") & """," & vbCr
            'sJscript2 = sJscript2 & "x: -20, //center" & vbCr
            'sJscript2 = sJscript2 & "style: {
            'color: 'black',
            'fontSize: '25px'
            '}" & vbCr
            'sJscript2 = sJscript2 & "}," & vbCr

            sb.Append("subtitle: {" & vbCr)
            sb.Append("text:       """ & Session("RSQR") & """," & vbCr)
            sb.Append("x: -20, //center" & vbCr)
            sb.Append("style: {
            color: 'black',
            fontSize: '25px'
            }" & vbCr)
            sb.Append("}," & vbCr)

        End If
        'jam 2025/09/01 Changing concatenation process
        ''sJscript2 = sJscript2 & "credits: {" & vbCr
        ''sJscript2 = sJscript2 & "enabled:       false" & vbCr
        '''jam 2025/02/15 New highcharts version libraries
        ''sJscript2 = sJscript2 & "}," & vbCr
        '''sJscript2 = sJscript2 & "exporting: {" & vbCr
        '''sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        '''sJscript2 = sJscript2 & "}," & vbCr

        sb.Append("credits: {" & vbCr)
        sb.Append("enabled:       false" & vbCr)
        'jam 2025/02/15 New highcharts version libraries
        sb.Append("}," & vbCr)
        'sJscript2 = sJscript2 & "exporting: {" & vbCr
        'sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr


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

        If Session("GType") = "SCAT" Then
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & "tooltip: {" & vbCr
            'sJscript2 = sJscript2 & "formatter: function () {" & vbCr
            'sJscript2 = sJscript2 & "return '<span style=""color:' + this.point.fillColor + '"">●</span>' + ' ' + this.point.name + '<br>" & sAxisXLabel & ": ' + this.point.x + '<br>" & sAxisYLabel & ": ' + this.point.y"
            
            sb.Append("tooltip: {" & vbCr)
            sb.Append("formatter: function () {" & vbCr)
            sb.Append("return '<span style=""color:' + this.point.fillColor + '"">●</span>' + ' ' + this.point.name + '<br>" & sAxisXLabel & ": ' + this.point.x + '<br>" & sAxisYLabel & ": ' + this.point.y")
            
            
            If Session("bBubble") = True Then
                'jam 2025/09/01 Changing concatenation process
                'sJscript2 = sJscript2 & " + '<br>" & Session("VARN")(1, 1 + pCOUNTE) & ": ' + this.point.z;" & vbCr
                sb.Append(" + '<br>" & Session("VARN")(1, 1 + pCOUNTE) & ": ' + this.point.z;" & vbCr)
                        
            
            Else
                'jam 2025/09/01 Changing concatenation process
                'sJscript2 = sJscript2 & ";" & vbCr

                sb.Append(";" & vbCr)
            End If
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & "}" & vbCr
            'sJscript2 = sJscript2 & "}," & vbCr

            sb.Append("}" & vbCr)
            sb.Append("}," & vbCr)

        End If
        'jam 2025/09/01 Changing concatenation process
        ''sJscript2 = sJscript2 & "subtitle: {" & vbCr
        ''sJscript2 = sJscript2 & "text:       'Source: WorldClimate.com'," & vbCr
        ''sJscript2 = sJscript2 & "x: -20" & vbCr
        ''sJscript2 = sJscript2 & "}," & vbCr
        'sJscript2 = sJscript2 & "xAxis: {" & vbCr
        ''jrs 2023/04/17
        'sJscript2 = sJscript2 & "labels: {" & vbCr
        'sJscript2 = sJscript2 & "style: {" & vbCr
        'sJscript2 = sJscript2 & "color: 'black'," & vbCr
        'sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr 'jrs 2023/07/18 changed from 14px
        'sJscript2 = sJscript2 & "}}," & vbCr
        'sJscript2 = sJscript2 & "title: {" & vbCr
        ''jrs 2023/04/17
        'sJscript2 = sJscript2 & "style: {" & vbCr
        'sJscript2 = sJscript2 & "color: 'black'," & vbCr
        'sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr 'jrs 2023/07/18 changed from 14px
        'sJscript2 = sJscript2 & "}," & vbCr


        'sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr


        'sJscript2 = sJscript2 & "subtitle: {" & vbCr
        'sJscript2 = sJscript2 & "text:       'Source: WorldClimate.com'," & vbCr
        'sJscript2 = sJscript2 & "x: -20" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        sb.Append("xAxis: {" & vbCr)
        'jrs 2023/04/17
        sb.Append("labels: {" & vbCr)
        sb.Append("style: {" & vbCr)
        sb.Append("color: 'black'," & vbCr)
        sb.Append("fontSize: '25px'" & vbCr) 'jrs 2023/07/18 changed from 14px
        sb.Append("}}," & vbCr)
        sb.Append("title: {" & vbCr)
        'jrs 2023/04/17
        sb.Append("style: {" & vbCr)
        sb.Append("color: 'black'," & vbCr)
        sb.Append("fontSize: '25px'" & vbCr) 'jrs 2023/07/18 changed from 14px
        sb.Append("}," & vbCr)


        sb.Append("text:       '" & sAxisXLabel & "'" & vbCr)
        sb.Append("}" & vbCr)


        'Load Years
        '----------
        'sJscript2 = sJscript2 & "categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'," & vbCr
        'sJscript2 = sJscript2 & "'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']" & vbCr
        If Session("GType") <> "BAR" And Session("GType") <> "PIE" And Session("GType") <> "SCAT" Then
            'jrs 2015/09/11 removing categories from Bar chart, problematic for multiple series
            'it wasn't showing all bars in the first and last categories
            'jam 2025/09/01 Changing concatenation process
            'sJscript2 = sJscript2 & ", categories: ["
            sb.Append(", categories: [")

            Dim i As Integer
            For i = 1 To Session("DisYear") Step Session("Interval")
                If i > 1 Then
                    'jam 2025/09/01 Changing concatenation process
                    'sJscript2 = sJscript2 & ","

                    sb.Append(",")
                End If
                'jam 2025/09/01 Changing concatenation process
                'sJscript2 = sJscript2 & "'" & CStr(Session("FirstYear") + i - 1) & "'"

                sb.Append("'" & CStr(Session("FirstYear") + i - 1) & "'")
                
            Next
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "]" & vbCr

            'sJscript2 = sJscript2 & "]" & vbCr

            
            sb.Append( "]" & vbCr)



        End If
        'jrs 2015/09/08
        If Session("GType") <> "PIE" And Session("GType") <> "SCAT" Then
            If Session("DisYear") > 21 Or Session("Interval") > 1 Then
                If Session("Interval") > 1 Then
                    'jam 2025/09/02 Changing concatenation process
                    'sJscript2 = sJscript2 & ", tickInterval: " & Session("Interval") & vbCr

                    sb.Append( ", tickInterval: " & Session("Interval") & vbCr)

                Else
                    'jam 2025/09/02 Changing concatenation process
                    'sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr

                    sb.Append( ", tickInterval: 5" & vbCr)

                End If
            End If
        End If

        If Not (Session("VLYear") Is Nothing) Then
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & ", plotLines: [{" & vbCr
            'sJscript2 = sJscript2 & "color:          'red', " & vbCr
            'sJscript2 = sJscript2 & "value: " & CStr(Session("VLYear")) & ", " & vbCr
            'sJscript2 = sJscript2 & "width: 2 " & vbCr
            'sJscript2 = sJscript2 & "}]" & vbCr

            sb.Append( ", plotLines: [{" & vbCr)
            sb.Append( "color:          '#3E414A', " & vbCr) 'jrs 2026/01/29 changed color to soft charcoal from red
            sb.Append( "value: " & CStr(Session("VLYear")) & ", " & vbCr)
            sb.Append( "width: 2 " & vbCr)
            sb.Append( "}]" & vbCr)

        End If
        'jrs 2015/09/18
        If Session("GType") <> "PIE" And Session("GType") <> "SCAT" Then
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & ", min: " & Session("FirstYear") & vbCr
            sb.Append(", min: " & Session("FirstYear") & vbCr)
            
            'jrs 2023/07/18
            If Session("DisYear") > 50 Then
                'jam 2025/09/02 Changing concatenation process
                'sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("DisYear") + 1) & vbCr 'jrs 2023/07/18 added extra years to avoid weird character
            
                sb.Append(", max: " & CStr(Session("FirstYear") + Session("DisYear") + 1) & vbCr) 'jrs 2023/07/18 added extra years to avoid weird character
            
            Else
                'jam 2025/09/02 Changing concatenation process
                'sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("DisYear") - 1) & vbCr
            
                sb.Append(", max: " & CStr(Session("FirstYear") + Session("DisYear") - 1) & vbCr)
            End If
        End If
        'jam 2025/09/02 Changing concatenation process
        'sJscript2 = sJscript2 & "}," & vbCr
        'sJscript2 = sJscript2 & "yAxis: {" & vbCr

        sb.Append("}," & vbCr)
        sb.Append("yAxis: {" & vbCr)

        'jrs 2024/01/17
        If Session("YAxisMax") > "" Then
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "max: " & Session("YAxisMax") & "," & vbCr
            sb.Append( "max: " & Session("YAxisMax") & "," & vbCr)
        End If
        If Session("YAxisMin") > "" Then
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "min: " & Session("YAxisMin") & "," & vbCr
            sb.Append( "min: " & Session("YAxisMin") & "," & vbCr)
        End If
        'jam 2025/09/02 Changing concatenation process
        'jrs 2023/04/17
        'sJscript2 = sJscript2 & "labels: {" & vbCr
        'sJscript2 = sJscript2 & "style: {" & vbCr
        'sJscript2 = sJscript2 & "color: 'black'," & vbCr
        'sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr 'jrs 2023/07/18 changed from 14px
        'sJscript2 = sJscript2 & "}}," & vbCr

        sb.Append( "labels: {" & vbCr)
        sb.Append( "style: {" & vbCr)
        sb.Append( "color: 'black'," & vbCr)
        sb.Append( "fontSize: '25px'" & vbCr) 'jrs 2023/07/18 changed from 14px
        sb.Append( "}}," & vbCr)



        'jrs 2018/05/16
        'needed to have first series at the bottom, for example in Education Atteinment Display, Non-Primary at the bottom
        If Session("GType") = "STACKEDAREA" Then
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "reversedStacks: false," & vbCr

            sb.Append(  "reversedStacks: false," & vbCr)
        End If
        'jrs 2024/01/31
        If Session("gridLineWidth") > "0" Then
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "gridLineWidth: " & Session("gridLineWidth") & "," & vbCr
            sb.Append(  "gridLineWidth: " & Session("gridLineWidth") & "," & vbCr)
        Else
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
            sb.Append(  "gridLineWidth: 0," & vbCr)
        End If
        'jam 2025/09/02 Changing concatenation process
        ''sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        ''sJscript2 = sJscript2 & "title: {" & vbCr
        '''jrs 2023/04/17
        ''sJscript2 = sJscript2 & "style: {" & vbCr
        ''sJscript2 = sJscript2 & "color: 'black'," & vbCr
        ''sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr 'jrs 2023/07/18 changed from 14px
        ''sJscript2 = sJscript2 & "}," & vbCr

        ''sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr
        '''sJscript2 = sJscript2 & "text:       '" & getYAxisLabel() & "'," & vbCr
        '''sJscript2 = sJscript2 & "style: {" & vbCr
        '''sJscript2 = sJscript2 & "fontSize:   '20px'" & vbCr
        '''sJscript2 = sJscript2 & "}" & vbCr

        ''sJscript2 = sJscript2 & "}," & vbCr


        '''jrs 2015/09/08
        ''sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        ''sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        ''sJscript2 = sJscript2 & "value: 0," & vbCr
        ''sJscript2 = sJscript2 & "width: 1," & vbCr
        ''sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        '''''''''''''''

        sb.Append(   "lineWidth: 1," & vbCr)
        sb.Append(   "title: {" & vbCr)
        'jrs 2023/04/17
        sb.Append(   "style: {" & vbCr)
        sb.Append(   "color: 'black'," & vbCr)
        sb.Append(   "fontSize: '25px'" & vbCr) 'jrs 2023/07/18 changed from 14px
        sb.Append(   "}," & vbCr)

        sb.Append(   "text:       '" & sAxisYLabel & "'" & vbCr)
        'sJscript2 = sJscript2 & "text:       '" & getYAxisLabel() & "'," & vbCr
        'sJscript2 = sJscript2 & "style: {" & vbCr
        'sJscript2 = sJscript2 & "fontSize:   '20px'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        sb.Append(   "}," & vbCr)


        'jrs 2015/09/08
        sb.Append(   "minPadding: 0," & vbCr)

        sb.Append(   "plotLines: [{" & vbCr)
        sb.Append(   "value: 0," & vbCr)
        sb.Append(   "width: 1," & vbCr)
        sb.Append(   "color:      '#808080'" & vbCr)


        'jrs 2024/01/31 *** start ***
        If Session("HorizontalLineValue") > "" Then
        'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "}, {" & vbCr
            'sJscript2 = sJscript2 & "value: " & Session("HorizontalLineValue") & "," & vbCr
            'sJscript2 = sJscript2 & "width: 2," & vbCr
            'sJscript2 = sJscript2 & "color:      'red'," & vbCr
            'sJscript2 = sJscript2 & "dashStyle: 'shortdash'," & vbCr
            'sJscript2 = sJscript2 & "label: {" & vbCr
            'sJscript2 = sJscript2 & "text: '" & Session("HorizontalLineText") & "'" & vbCr
            'sJscript2 = sJscript2 & "} " & vbCr
        
            sb.Append( "}, {" & vbCr)
            sb.Append( "value: " & Session("HorizontalLineValue") & "," & vbCr)
            sb.Append( "width: 2," & vbCr)
            sb.Append( "color:      'red'," & vbCr)
            sb.Append( "dashStyle: 'shortdash'," & vbCr)
            sb.Append( "label: {" & vbCr)
            sb.Append( "text: '" & Session("HorizontalLineText") & "'" & vbCr)
            sb.Append( "} " & vbCr)
        
        End If
        'jam 2025/09/02 Changing concatenation process
        'jrs 2024/01/31 *** end ***
        'sJscript2 = sJscript2 & "}]" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        'sJscript2 = sJscript2 & "legend: {" & vbCr
        'sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
        'sJscript2 = sJscript2 & "align:      'center'," & vbCr

        sb.Append(  "}]" & vbCr)
        sb.Append(  "}," & vbCr)
        sb.Append(  "legend: {" & vbCr)
        sb.Append(  "layout:     'horizontal'," & vbCr)
        sb.Append(  "align:      'center'," & vbCr)
        'cdv 2021/02/02 flip legend to match intended order
        'jrs 2023/07/18 sJscript2 = sJscript2 & "reversed:      true," & vbCr
        'jrs 2026/04/27 added or Session("bHideLegends") = True       
        If pCOUNTE >= 120 or Session("bHideLegends") = True Then
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "enabled:        false," & vbCr 'cdv 2022/03/23 Hide legend when we have more than 120 subsets. This makes our functionality match VB6
        
            sb.Append(  "enabled:        false," & vbCr) 'cdv 2022/03/23 Hide legend when we have more than 120 subsets. This makes our functionality match VB6
        
        End If
        'jrs 2016/01/04
        If Session("GType") = "SCAT" Then
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "verticalAlign:  'bottom'," & vbCr
            'sJscript2 = sJscript2 & "x: 20, " & vbCr 'avoid overlap
            'sJscript2 = sJscript2 & "y: 0, " & vbCr 'avoid overlap

            sb.Append( "verticalAlign:  'bottom'," & vbCr)
            sb.Append( "x: 20, " & vbCr) 'avoid overlap
            sb.Append( "y: 0, " & vbCr) 'avoid overlap

        Else
            'jam 2025/09/02 Changing concatenation process
            'sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
            'sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
            'sJscript2 = sJscript2 & "y: 40, " & vbCr 'jrs 2016/04/26 changed from 20 to 40 'avoid overlap 

            sb.Append( "verticalAlign:  'top'," & vbCr)
            sb.Append( "x: -20, " & vbCr) 'avoid overlap
            sb.Append( "y: 40, " & vbCr) 'jrs 2016/04/26 changed from 20 to 40 'avoid overlap 

        End If
        'jam 2025/09/02 Changing concatenation process
        'sJscript2 = sJscript2 & "borderWidth: 0, " & vbCr
        ''jrs 2023/04/17
        'sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        'sJscript2 = sJscript2 & "color: '#000000'," & vbCr
        'sJscript2 = sJscript2 & "fontWeight: 'bold'," & vbCr
        
        sb.Append( "borderWidth: 0, " & vbCr)
        'jrs 2023/04/17
        sb.Append( "itemStyle: {" & vbCr)
        sb.Append( "color: '#000000'," & vbCr)
        sb.Append( "fontWeight: 'bold'," & vbCr)
        
        
        'jrs 2023/07/18
        Dim sFontSize As String = "14"
        If Session("COUNTE") < 10 Then
            sFontSize = "25"
        ElseIf Session("COUNTE") < 20 Then
            sFontSize = "21"
        ElseIf Session("COUNTE") < 30 Then
            sFontSize = "18"
        End If

        'jrs 2026/05/04
        If Session("CustomFontSize") > "" Then
            sFontSize = Session("CustomFontSize")
        End If

        'jam 2025/09/02 Changing concatenation process

        'sJscript2 = sJscript2 & "fontSize: '" & sFontSize & "px'" & vbCr 'jrs 2023/07/18 changed from 14px
        'sJscript2 = sJscript2 & "}" & vbCr

        'sJscript2 = sJscript2 & "}," & vbCr

        sb.Append( "fontSize: '" & sFontSize & "px'" & vbCr) 'jrs 2023/07/18 changed from 14px
        sb.Append( "}" & vbCr)

        sb.Append( "}," & vbCr)


        'jrs 2015/09/18
        If Session("GType") = "SCAT" Then
            
            'jam 2025/09/06 Changing concatenation process
            'sJscript2 = sJscript2 & "plotOptions: {" & vbCr
            'sJscript2 = sJscript2 & "   series: {" & vbCr
            'sJscript2 = sJscript2 & "       point: {" & vbCr
            'sJscript2 = sJscript2 & "           events: {" & vbCr
            'sJscript2 = sJscript2 & "               click: function () {" & vbCr
            'sJscript2 = sJscript2 & "                   this.options.dataLabels.enabled = !this.options.dataLabels.enabled;" & vbCr
            'sJscript2 = sJscript2 & "                   this.update();" & vbCr
            'sJscript2 = sJscript2 & "               }" & vbCr
            'sJscript2 = sJscript2 & "           }" & vbCr
            'sJscript2 = sJscript2 & "       }" & vbCr
            'sJscript2 = sJscript2 & "   }" & vbCr
            'sJscript2 = sJscript2 & "}," & vbCr
            
            sb.Append( "plotOptions: {" & vbCr)
            sb.Append( "   series: {" & vbCr)
            sb.Append( "       point: {" & vbCr)
            sb.Append( "           events: {" & vbCr)
            sb.Append( "               click: function () {" & vbCr)
            sb.Append( "                   this.options.dataLabels.enabled = !this.options.dataLabels.enabled;" & vbCr)
            sb.Append( "                   this.update();" & vbCr)
            sb.Append( "               }" & vbCr)
            sb.Append( "           }" & vbCr)
            sb.Append( "       }" & vbCr)
            sb.Append( "   }" & vbCr)
            sb.Append( "}," & vbCr)
            
            
            'jrs 2018/05/15
        ElseIf Session("GType") = "STACKEDAREA" Then
            'jam 2025/09/06 Changing concatenation process
            'sJscript2 = sJscript2 & "plotOptions: {" & vbCr
            'sJscript2 = sJscript2 & "   series: {" & vbCr
            'sJscript2 = sJscript2 & "       stacking: 'normal' " & vbCr
            'sJscript2 = sJscript2 & "   }" & vbCr
            'sJscript2 = sJscript2 & "}," & vbCr

            sb.Append(  "plotOptions: {" & vbCr)
            sb.Append(  "   series: {" & vbCr)
            sb.Append(  "       stacking: 'normal' " & vbCr)
            sb.Append(  "   }" & vbCr)
            sb.Append(  "}," & vbCr)

        End If
        'jam 2025/09/06 Changing concatenation process
        'sJscript2 = sJscript2 & "series: [" & vbCr

        sb.Append( "series: [" & vbCr)

        Dim sZeros As String = ""
        If Session("GType") = "PIE" Then
            'jam 2025/09/06 Changing concatenation process
            'sJscript2 = sJscript2 & PlotYearToJSSeries(Session("PYear"), sZeros)

            sb.Append(  PlotYearToJSSeries(Session("PYear"), sZeros))


        ElseIf Session("GType") = "SCAT" Then
            'jam 2025/09/06 Changing concatenation process
            'sJscript2 = sJscript2 & PlotPairToJSSeries(Session("PYear"), Session("VarsCount"), Session("bBubble"))
            
            sb.Append(   PlotPairToJSSeries(Session("PYear"), Session("VarsCount"), Session("bBubble")))
            

            'jrs 2012/05/28
            If Session("RegressType") <> "" Then
                'jam 2025/09/06 Changing concatenation process
                'Plot Regression
                'sJscript2 = sJscript2 & "," & PlotRegPairToJSSeries()


               sb.Append(  "," & PlotRegPairToJSSeries())
            End If
        Else
            For i = 1 To Session("COUNTE")
                If i > 1 Then
                    'jam 2025/09/06 Changing concatenation process
                    'sJscript2 = sJscript2 & "," & vbCr

                    sb.Append(   "," & vbCr)

                End If
                'jam 2025/09/06 Changing concatenation process
                'sJscript2 = sJscript2 & PlotColToJSSeries(i)

                sb.Append(PlotColToJSSeries(i))

            Next
        End If

        'jam 2025/09/06 Changing concatenation process

        'sJscript2 = sJscript2 & "]" & vbCr

        'sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        ''jam 2023/11/01
        '''sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        'sJscript2 = sJscript2 & "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr



        'sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        'sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        'sJscript2 = sJscript2 & "})" & vbCr
        'sJscript2 = sJscript2 & ".attr({" & vbCr
        'sJscript2 = sJscript2 & "zIndex: 0" & vbCr
        'sJscript2 = sJscript2 & "})" & vbCr
        'sJscript2 = sJscript2 & ".css({"
        'sJscript2 = sJscript2 & "cursor: 'pointer'"
        'sJscript2 = sJscript2 & "})"
        'sJscript2 = sJscript2 & ".add();" & vbCr

        ''''''''''''''''''''''''''''

        sb.Append("]" & vbCr)

        sb.Append( "}, function(chart){" & vbCr)

        'jam 2023/11/01
        ''sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        sb.Append(  "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr)



        sb.Append(  ".on('click', function () {" & vbCr)
        sb.Append(  "location.href = 'https://korbel.du.edu/pardee/';" & vbCr)
        sb.Append(  "})" & vbCr)
        sb.Append(  ".attr({" & vbCr)
        sb.Append(  "zIndex: 0" & vbCr)
        sb.Append(  "})" & vbCr)
        sb.Append(  ".css({")
        sb.Append(  "cursor: 'pointer'")
        sb.Append(  "})")
        sb.Append(  ".add();" & vbCr)



        'jrs 2024/09/04 Adding Text with IFs Version
        If Session("ShowVersion") Then
            'jam 2025/09/06 Changing concatenation process
            'sJscript2 = sJscript2 & "chart.renderer.text('IFs " & ModelVersion.Replace("Version ", "") & "')" & vbCr
            'sJscript2 = sJscript2 & ".attr({" & vbCr
            'sJscript2 = sJscript2 & "x: 15," & vbCr
            'sJscript2 = sJscript2 & "y: 80" & vbCr
            'sJscript2 = sJscript2 & "})" & vbCr
            'sJscript2 = sJscript2 & ".css({"
            'sJscript2 = sJscript2 & "fontStyle: 'italic',"
            ''jam 2025/02/18 Fixing version label
            'sJscript2 = sJscript2 & "fontSize: '12',"
            'sJscript2 = sJscript2 & "fontWeight: 'Bold'"
            '''''''''''''''''''''''''''''''''''''''''''''
            'sJscript2 = sJscript2 & "})"
            'sJscript2 = sJscript2 & ".add();" & vbCr


            ''''''''''''''''''''''''''''''''''''''''''''''''''
            sb.Append( "chart.renderer.text('IFs " & ModelVersion.Replace("Version ", "") & "')" & vbCr)
            sb.Append( ".attr({" & vbCr)
            sb.Append( "x: 15," & vbCr)
            sb.Append( "y: 80" & vbCr)
            sb.Append( "})" & vbCr)
            sb.Append( ".css({")
            sb.Append( "fontStyle: 'italic',")
            'jam 2025/02/18 Fixing version label
            sb.Append( "fontSize: '12',")
            sb.Append( "fontWeight: 'Bold'")
            ''''''''''''''''''''''''''''''''''''''''''''
            sb.Append( "})")
            sb.Append( ".add();" & vbCr)

        End If

        'If Not (Session("VLYear") Is Nothing) Then
        '    sJscript2 = sJscript2 & "chart.renderer.path(['m', " & CStr((Session("VLYear") - Session("FirstYear")) * 70 + 100) & ", 0, 'V', 800])" & vbCr
        '    sJscript2 = sJscript2 & ".attr({" & vbCr
        '    sJscript2 = sJscript2 & "'stroke-width': 1," & vbCr
        '    sJscript2 = sJscript2 & "stroke:         'red'," & vbCr
        '    sJscript2 = sJscript2 & "zIndex:1000" & vbCr
        '    sJscript2 = sJscript2 & "})" & vbCr
        '    sJscript2 = sJscript2 & ".add();" & vbCr
        'End If

        'jam 2025/09/06 Changing concatenation process
        'sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        'sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        ''Non-IE
        'sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        'sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        ''//IE 6+ in 'standards compliant mode'
        'sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        'sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        ''//IE 4 compatible
        'sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        'sJscript2 = sJscript2 & "{" & vbCr
        'sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        'sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = myHeight * 0.90;" & vbCr

        'sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        'sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        'sJscript2 = sJscript2 & "});" & vbCr
        'sJscript2 = sJscript2 & "});" & vbCr

        'sJscript2 = sJscript2 & "</script>"

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        sb.Append( "var myWidth = 0, myHeight = 0;" & vbCr)
        sb.Append( "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr)
        'Non-IE
        sb.Append( "myWidth = window.innerWidth;" & vbCr)
        sb.Append( "myHeight = window.innerHeight;" & vbCr)
        sb.Append( "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr)
        '//IE 6+ in 'standards compliant mode'
        sb.Append( "myWidth = document.documentElement.clientWidth;" & vbCr)
        sb.Append( "myHeight = document.documentElement.clientHeight;" & vbCr)
        sb.Append( "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr)
        '//IE 4 compatible
        sb.Append( "myWidth = document.body.clientWidth;" & vbCr)
        sb.Append( "myHeight = document.body.clientHeight;" & vbCr)
        sb.Append( "}" & vbCr)
        sb.Append( "if (myWidth == 0)" & vbCr)
        sb.Append( "{" & vbCr)
        sb.Append( "myWidth = screen.width;" & vbCr)
        sb.Append( "myHeight = screen.height;" & vbCr)
        sb.Append( "}" & vbCr)

        sb.Append( "myWidth = myWidth * 0.95;" & vbCr)
        sb.Append( "myHeight = myHeight * 0.90;" & vbCr)

        sb.Append( "chart.setSize(myWidth, myHeight);" & vbCr)
        sb.Append( "chart.hasUserSize = null;" & vbCr)


        sb.Append( "});" & vbCr)
        sb.Append( "});" & vbCr)

        sb.Append( "</script>")


        ' jam 2025/06/09
        'lblJS.Text = sJscript2


        lblJS.Text = sb.ToString()

    End Sub


    Public Sub mnuCommon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objEA.Item.Caption = "Common" Then
            objSender.Groups("Scaling").Items("Common").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            objSender.Groups("Scaling").Items("No Scaling").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            Session("CommonPlot") = "True"
        Else  'Scale
            objSender.Groups("Scaling").Items("No Scaling").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            objSender.Groups("Scaling").Items("Common").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
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

            'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
            Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx")) 'jam 2020/12/22

            'Vpath = Request.GetFriendlyUrlFileVirtualPath

            'Vext = Request.GetFriendlyUrlFileExtension

            'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")

            'Change REgion
            'vurl = FriendlyUrl.Href("~/frm_Message")

            'Response.Redirect(vurl)


        End If

        Dim rr, cc As Integer
        ' Dim aa


        'jam 2016-03-06  Reverting Trillions units applied
        If Session("bTrillionsOk") Then
            For cc = 1 To Session("COUNTE")
                For rr = 1 To Session("DisYear")
                    'aa = Session("Unit_PLOT")(rr, cc)
                    Session("PLOT")(rr, cc) = Session("PLOTTrillions")(rr, cc) 'jam 2016-05-16
                Next rr
            Next cc

            'jam 2025/07/24 Changing previous horizon
            'Session("prevHorizonYear") = Session("DisYear")

            Session("prevHorizonYear") = 1

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

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


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

                    'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                    Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                    'Vpath = Request.GetFriendlyUrlFileVirtualPath

                    'Vext = Request.GetFriendlyUrlFileExtension

                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")

                    'Change REgion
                    'vurl = FriendlyUrl.Href("~/frm_Message")

                    'Response.Redirect(vurl)



                End If

            ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = Session("LastYear")
                    LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                    Support.FormManager.StateStore(Me)
                    'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                    Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                    'Vpath = Request.GetFriendlyUrlFileVirtualPath

                    'Vext = Request.GetFriendlyUrlFileExtension

                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                    'vurl = FriendlyUrl.Href("~/frm_Message")

                    'Response.Redirect(vurl)



                End If

            End If
        End If
        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False




        'jrs 2011/08/26
        Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1

        'jam 2016-06-03 Convert from Billions to Trillions
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
            'jrs 2025/07/11
            Session("RSQR") = ""
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
            'jrs 2025/07/11
            Session("RSQR") = ""
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
        'jrs 2024/05/13
        'If alias are on, need to turn off
        Dim bAliasLocal As Boolean = False
        bAliasLocal = (Session("bAlias") = True)
        If bAliasLocal Then
            mnuAlias_Click()
        End If
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

        'jrs 2024/05/13
        If bAliasLocal Then
            mnuAlias_Click()
        End If
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

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


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
        'jrs 2025/07/11
        Session("RSQR") = ""

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
        'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
        Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

        'Vpath = Request.GetFriendlyUrlFileVirtualPath

        'Vext = Request.GetFriendlyUrlFileExtension

        'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


        'vurl = FriendlyUrl.Href("~/frm_Message")

        'Response.Redirect(vurl)


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

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


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
        'jrs 2025/07/11
        Session("RSQR") = ""


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
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            Else
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            End If
        ElseIf sVartmp = "GDP" Then
            cswmItem = cswmWebMenu.Groups("mnuBD").Items.Add()
            cswmItem.Caption = "GDP"
            cswmItem.ID = "mnuBDGDP"
            If Session("BDCheck") = "POP" Then
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            Else
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            End If
        End If

        'Repaint the graph
        mnuBDOther_Click()

        Exit Sub

Hdlr:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
        Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

        'Vpath = Request.GetFriendlyUrlFileVirtualPath

        'Vext = Request.GetFriendlyUrlFileExtension

        'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


        'vurl = FriendlyUrl.Href("~/frm_Message")

        'Response.Redirect(vurl)



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

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


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
        'jrs 2025/07/11
        Session("RSQR") = ""


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
        'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
        Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

        'Vpath = Request.GetFriendlyUrlFileVirtualPath

        'Vext = Request.GetFriendlyUrlFileExtension

        'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


        'vurl = FriendlyUrl.Href("~/frm_Message")

        'Response.Redirect(vurl)

    End Sub

    'jrs 2012/03/09
    Protected Sub cmdGoToYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGoToYear.Click

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String



        If Not IsNumeric(txtGoToYear.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
            Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                'Vpath = Request.GetFriendlyUrlFileVirtualPath

                'Vext = Request.GetFriendlyUrlFileExtension

                'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                'vurl = FriendlyUrl.Href("~/frm_Message")

                'Response.Redirect(vurl)

            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                'Vpath = Request.GetFriendlyUrlFileVirtualPath

                'Vext = Request.GetFriendlyUrlFileExtension

                'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                'vurl = FriendlyUrl.Href("~/frm_Message")

                'Response.Redirect(vurl)

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
            'jrs 2025/07/11
            Session("RSQR") = ""
        End If


    End Sub

    'jrs 2012/03/21
    Protected Sub cmdVLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVLine.Click
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        If Not IsNumeric(txtVLine.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
            Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

            'Vpath = Request.GetFriendlyUrlFileVirtualPath

            'Vext = Request.GetFriendlyUrlFileExtension

            'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


            'vurl = FriendlyUrl.Href("~/frm_Message")

            'Response.Redirect(vurl)

        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtVLine.Text) > Session("LastYearForecast") Or CInt(txtVLine.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                'Vpath = Request.GetFriendlyUrlFileVirtualPath

                'Vext = Request.GetFriendlyUrlFileExtension

                'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                'vurl = FriendlyUrl.Href("~/frm_Message")

                'Response.Redirect(vurl)


            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtVLine.Text) > Session("LastYearForecast") Or CInt(txtVLine.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))
                'Vpath = Request.GetFriendlyUrlFileVirtualPath

                'Vext = Request.GetFriendlyUrlFileExtension

                'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                'vurl = FriendlyUrl.Href("~/frm_Message")

                'Response.Redirect(vurl)



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
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If Not IsNumeric(txtHorizon.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
            Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

            'Vpath = Request.GetFriendlyUrlFileVirtualPath

            'Vext = Request.GetFriendlyUrlFileExtension

            'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


            'vurl = FriendlyUrl.Href("~/frm_Message")

            'Response.Redirect(vurl)

        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))
                Support.FormManager.StateStore(Me)
                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                'Vpath = Request.GetFriendlyUrlFileVirtualPath

                'Vext = Request.GetFriendlyUrlFileExtension

                'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                'vurl = FriendlyUrl.Href("~/frm_Message")

                'Response.Redirect(vurl)



            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
                Support.FormManager.StateStore(Me)

                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                'Vpath = Request.GetFriendlyUrlFileVirtualPath

                'Vext = Request.GetFriendlyUrlFileExtension

                'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                'vurl = FriendlyUrl.Href("~/frm_Message")

                'Response.Redirect(vurl)

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


                'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                'Vpath = Request.GetFriendlyUrlFileVirtualPath

                'Vext = Request.GetFriendlyUrlFileExtension

                'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                'vurl = FriendlyUrl.Href("~/frm_Message")

                'Response.Redirect(vurl)

            End If
        End If

        'jrs 2026/04/04
        If cmb_Horizon.Items.FindByText(txtHorizon.Text) IsNot Nothing Then
            cmb_Horizon.Text = txtHorizon.Text
            cmb_Horizon_SelectedIndexChanged(Me, New EventArgs)
        End If
    End Sub

    Private Sub mnuUseGList_Click()
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        Session("pCOUNTE") = pCOUNTE

        Session("IsaGroup") = False

        Session("SelectGeographyParent") = "frm_GraphicalDisplay.aspx"
        Support.FormManager.StateStore(Me)
        'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
        Support.FormManager.Show(Me, Page.ResolveUrl("frm_SelctGeography.aspx"))


        'Vpath = Request.GetFriendlyUrlFileVirtualPath

        'Vext = Request.GetFriendlyUrlFileExtension

        'vfriendlyurl = FriendlyUrl.Resolve("~/frm_SelctGeography.aspx")


        'vurl = FriendlyUrl.Href("~/frm_SelctGeography")

        'Response.Redirect(vurl)

    End Sub

    Private Sub mnuUseGroup_Click()
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Session("pCOUNTE") = pCOUNTE

        Session("IsaGroup") = True

        Session("SelectGeographyParent") = "frm_GraphicalDisplay.aspx"
        Support.FormManager.StateStore(Me)
        'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
        Support.FormManager.Show(Me, Page.ResolveUrl("frm_SelctGeography.aspx"))

        'Vpath = Request.GetFriendlyUrlFileVirtualPath

        'Vext = Request.GetFriendlyUrlFileExtension

        'vfriendlyurl = FriendlyUrl.Resolve("~/frm_SelctGeography.aspx")


        'vurl = FriendlyUrl.Href("~/frm_SelctGeography")

        'Response.Redirect(vurl)


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
            cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            'jrs 2013/05/01
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZX").Enabled = False
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZY").Enabled = False
        End If
        If Session("sVarZ") <> "POP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
        End If
        If Session("sVarZ") <> "GDP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        End If
        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22

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
            cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            'jrs 2013/05/01
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZX").Enabled = False
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZY").Enabled = False
        End If
        If Session("sVarZ") <> "POP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        End If
        If Session("sVarZ") <> "GDP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
        End If
        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22

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
        cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
        If Session("sVarZ") <> "POP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        End If
        If Session("sVarZ") <> "GDP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        End If
        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
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
            cswmWebMenu.Groups("mnuBD").Items("mnuBDOther").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            'jrs 2013/05/01
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZX").Enabled = False
            cswmWebMenu.Groups("mnuSwap").Items("mnuSwapZY").Enabled = False
        End If
        If Session("sVarZ") <> "POP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDPOP").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        End If
        If Session("sVarZ") <> "GDP" Then
            cswmWebMenu.Groups("mnuBD").Items("mnuBDGDP").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        End If
        cswmWebMenu.Groups("mnuBD").Items("mnuBDNone").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22

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
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sVar, -1, sDim1, -1, "", -1, "", -1, "", fileIdx, True, True, Nothing, 1, TargetVector, TargetCol,
        iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Else
            gLoadForDisplay(sVar, -1, sDim1, -1, "", -1, "", -1, "", fileIdx, True, True, Nothing, 1, TargetVector, TargetCol,
            iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
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
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif")
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
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If
    End Sub

    Private Sub mnuExpon_Click()
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String



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
                        'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
                        Support.FormManager.Show(Me, Page.ResolveUrl("frm_Message.aspx"))

                        'Vpath = Request.GetFriendlyUrlFileVirtualPath

                        'Vext = Request.GetFriendlyUrlFileExtension

                        'vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                        'vurl = FriendlyUrl.Href("~/frm_Message")

                        'Response.Redirect(vurl)




                    End If
                End If
            Next ii

            Session("RegressType") = "EXP"
            RegressionPLOT(1, "EXP")

            Session("Transformation") = "EXP"
            Session("mnuLogistic") = False
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        End If
    End Sub

    Private Sub mnuPower_Click()
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


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
            Session("ScatterPower") = "frm_GraphicalDisplay.aspx"
            Support.FormManager.StateStore(Me)
            'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
            Support.FormManager.Show(Me, Page.ResolveUrl("frm_ScatterPower.aspx"))

            'Vpath = Request.GetFriendlyUrlFileVirtualPath

            'Vext = Request.GetFriendlyUrlFileExtension

            'vfriendlyurl = FriendlyUrl.Resolve("~/frm_ScatterPower.aspx")


            'vurl = FriendlyUrl.Href("~/frm_ScatterPower")

            'Response.Redirect(vurl)

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
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
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
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2020/12/22
            cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        End If
    End Sub

    Private Sub mnuLogistic_Click()
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'jam 2009-11-08    Parameter for Saturation Point
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

            Session("ScatterLogistic") = "frm_GraphicalDisplay.aspx"
            Support.FormManager.StateStore(Me)
            'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
            Support.FormManager.Show(Me, Page.ResolveUrl("frm_SaturParam.aspx"))

            'Vpath = Request.GetFriendlyUrlFileVirtualPath

            'Vext = Request.GetFriendlyUrlFileExtension

            'vfriendlyurl = FriendlyUrl.Resolve("~/frm_SaturParam.aspx")


            'vurl = FriendlyUrl.Href("~/frm_SaturParam")

            'Response.Redirect(vurl)

        End If
    End Sub

    Private Sub mnuNoRegress_Click()
        Session("RegressType") = ""
        Session("TableTitle") = ""
        'jrs 2025/07/11
        Session("RSQR") = ""

        Session("mnuLogistic") = False
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logarithmic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Power").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolSec").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("mnuPolThird").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
        cswmWebMenu.Groups("mnuAnalysis").Items("mnuAnalysisRelationships").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2020/12/22
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
                    'jam 2009-11-08     Saturation Point must be changed
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
            'jam 2009-08-22 If We've choose Saturation Option
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


        'jrs 2025/07/11 Session("TableTitle") = Equation & "<br/>R(SQR) = " & Format(RSQR, RFormat)
        Session("TableTitle") = Equation
        Session("RSQR") = "R(SQR) = " & Format(RSQR, RFormat)
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

        If Session("mnuUseAllYrs") Or Session("mnuUseAllYrsTime") Then 'jam 2016-02-07 Adding AllYears Exception
            iDimCount = Session("Dim1Count") * (Session("LastYear") - Session("FirstYear") + 1)
            If Session("mnuUseAllYrs") Then
                iVarsCount = Session("VarsCount")
            Else 'jam 2016-02-07 It's Session("mnuUseAllYrsTime")
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

        'jrs 2026/08/07 If StandAloneMode Then
        If Session("mnuSQLite") = "True" Then
            PrepareTransformPyForStatistics(iDimCount, iVarsCount - 1, RegVarD, RegVarI, sVarD, sVarI,
        Session("IFsStat_MDB"), Session("Total_Records"), Session("Total_Var"),
        Session("ValidData"), Session("AllValidPoints"), Session("VarNameD"), Session("VarNameI"))

            '   Load the transformation form
            '   ----------------------------

            Support.FormManager.StateStore(Me)
            'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
            Dim sURL As String = Page.ResolveUrl("TransformPy.aspx")

            Support.FormManager.Show(Me, sURL)
        Else
            PrepareTransformForStatistics(iDimCount, iVarsCount - 1, RegVarD, RegVarI, sVarD, sVarI,
        Session("IFsStat_MDB"), Session("Total_Records"), Session("Total_Var"))

            '   Load the transformation form
            '   ----------------------------

            Support.FormManager.StateStore(Me)
            'jam 2020/12/21 For friendlyURL, It was neccesary to add Page.ResolveURL method
            Support.FormManager.Show(Me, Page.ResolveUrl("Transform.aspx"))
        End If

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
            'jrs 2016/11/09 moved up
            If idx > 0 Then
                If Session("VDisplayUnitsName")(idx) > "" Then
                    'jrs 2012/10/24 sUnits = VDisplayUnitsName(idx)
                    sUnits = getDisplayUnitsWCurrency(idx, Session("CurrencyName"), Session("VDisplayUnitsName"))
                End If
            End If

            'jam 2016-03-06
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
            'If getYAxisLabel = "" Then
            'End If

        End If

        'jrs 2023/05/02 added PrepareForHTML
        Dim sLbl As String
        sLbl = getYAxisLabel
        getYAxisLabel = PrepareForHTML(sLbl)
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
                Case 4 'jrs 2023/11/20 Sandy Brown
                    sColor = "RGB(244,114,0)"
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
                If IsNumeric(Session("PLOT")(j, i)) Then
                    If Session("PLOT")(j, i) < Min Then
                        Min = Session("PLOT")(j, i)
                    End If
                    If Session("PLOT")(j, i) > Max Then
                        Max = Session("PLOT")(j, i)
                    End If
                End If
            Next
            Diff = Max - Min
            If Diff = 0 Then
                Diff = 0.00001
            End If
        ElseIf Session("PE_CommonMenu") = "Base1" Then 'jrs 2014/03/21
            'jrs 2025/10/23 new start of the loop
            Dim StartIdx As Integer
            If Session("RowAdjustment") = 0 Then
                StartIdx = 1
            Else
                StartIdx = Session("RowAdjustment") + 1
            End If
            'Find First Year value
            For j = StartIdx To Session("DisYear")
                If IsNumeric(Session("PLOT")(j, i)) Then
                    If Session("PLOT")(j, i) > 0 Then
                        FstVal = Session("PLOT")(j, i)
                        Exit For
                    End If
                End If
            Next
        Else
            Dim idx As Integer = Session("DisYear")
            If Session("GType") <> "LORENZ" And IsNumeric(Session("PLOT")(idx, 1)) Then
                'jrs 2025/10/21 sometimes aggregations cause 1s to become a little higher than one
                'changing 1 limit to 10
                If Math.Abs(CType(Session("PLOT")(idx, 1), Double)) < 10 Then 'cdv 2022/06/30
                    iNumDec = 5 'jrs 2024/07/11 changed from 3 to 5
                ElseIf Math.Abs(Session("PLOT")(idx, 1)) < 90 Then 'jrs 2023/10/25 changed from < 10 to < 90, still some percentages need more decimals
                    iNumDec = 2
                    'jrs 2023/06/30 graphs using percentages that top at 100 are producing ugly steps
                    'changed from 100 to 150
                ElseIf Math.Abs(Session("PLOT")(idx, 1)) < 150 Then
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
        'CDV 2021/10/22 ****START****
        For j = iStart To Session("DisYear") Step Session("Interval") 'jrs 2016/03/23 j = 1
            If IsNumeric(Session("PLOT")(j + Session("RowAdjustment"), i)) Then
                sJscript = sJscript & "[" & CStr(Session("FirstYear") + j - 1) & ", "
                If Session("CommonPlot") = "True" Then
                    sJscript = sJscript & CStr(Math.Round((Session("PLOT")(j + Session("RowAdjustment"), i) - Min) / Diff, 2)) & "]"
                ElseIf Session("PE_CommonMenu") = "Base1" And FstVal > 0 Then 'jrs 2014/03/21
                    sJscript = sJscript & CStr(Math.Round((Session("PLOT")(j + Session("RowAdjustment"), i)) / FstVal, 2)) & "]"
                Else
                    sJscript = sJscript & CStr(Math.Round(CType(Session("PLOT")(j + Session("RowAdjustment"), i), Double), iNumDec)) & "]" 'cdv 2022/06/30
                End If

                If j < Session("DisYear") Then
                    sJscript = sJscript & ", "
                End If
                'ElseIf Session("GType") = "BAR" Then
                '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
            End If
            'CDV 2021/10/22 ****END****
        Next

        If j - Session("Interval") <> Session("DisYear") Then
            j = Session("DisYear")
            If IsNumeric(Session("PLOT")(j, i)) Then
                sJscript = sJscript & "[" & CStr(Session("FirstYear") + j - 1) & ", "
                If Session("CommonPlot") = "True" Then
                    sJscript = sJscript & CStr((Session("PLOT")(j, i) - Min) / Diff)
                Else
                    sJscript = sJscript & CStr(Session("PLOT")(j, i)) & "]"
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
        'jrs 2024/12/09
        sJscript = sJscript & "dataLabels: {
                style: {
                    fontSize: 15
                }
            }," & vbCr

        sJscript = sJscript & "data: ["

        Dim almostzero As Double 'jam 2026/03/12 Value close to zero
        Dim j As Integer
        For j = 1 To Session("COUNTE")
            sJscript = sJscript & "{name: """ & getSeriesName(j) & """, y: "
            If IsNumeric(Session("PLOT")(i, j)) Then
                'jam 2026/03/12 Value close to zero
                If Session("GType") = "PIE" Then
                    almostzero = 0.00000001
                Else
                    almostzero = 0.0005
                End If

                'jam 2026/03/12 Value close to zero
                If Session("PLOT")(i, j) >= almostzero Then
                    'If Session("PLOT")(i, j) >= 0.0005 Then
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
                    'jrs 2020/10/05 Markers are not working anymore
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

    'jam 2016-02-12 Adding All Years for Statistics
    Protected Sub AllYears()
        If Session("mnuUseAllYrs") Then
            Session("mnuUseAllYrs") = False
        Else
            Session("mnuUseAllYrs") = True
        End If
        Session("mnuUseAllYrsTime") = False


    End Sub
    'jam 2016-02-12 Adding All Years and Time for Statistics
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
        convApplied = False 'jam 2013-03-21

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

        If (Session("prevHorizonYear") = 1 And Not Session("fromTableDisp")) Then 'jam 2016-03-05 Change the condition from "or" to "and" because Session("fromTableDisp")
            For i = 1 To Session("COUNTE")
                Session("ColumnsToConvert")(i) = False
                If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 Or
                    InStr(UCase(Session("GR4N")(i)), "BIL ") > 0 Then 'jrs 2019/05/14 added "BIL " for historical variables like GDP2011
                    II = II + 1

                    For j = 1 To vDisYear
                        'jrs 2016/09/30
                        If Not IsDBNull(Session("PLOT")(j, i)) Then
                            aa = Session("PLOT")(j, i)
                            If Session("PLOT")(j, i) >= 100000 Then 'jrs 2016/04/11 changed to 100,000
                                jj = jj + 1
                                Session("ColumnsToConvert")(i) = True
                                convApplied = True 'jam 2016-03-21

                                Exit For
                            End If
                        End If
                    Next j

                End If
            Next i

            'jam 2016-03-21 If there are some trillions conversion
            For i = 1 To Session("COUNTE")
                'jrs 2019/05/14 added "BIL " for historical variables like GDP2011
                If (InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 Or InStr(UCase(Session("GR4N")(i)), "BIL ") > 0) And convApplied And Not Session("ColumnsToConvert")(i) Then
                    Session("ColumnsToConvert")(i) = True
                End If
            Next i



        Else
            For i = 1 To Session("COUNTE")
                Session("ColumnsToConvert")(i) = False
                If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 Then 'jam 2016-03-21 In vb6 version is trillion the condition
                    II = II + 1

                    For j = (Session("prevHorizonYear") + 1) To vDisYear
                        If Session("PLOT")(j, i) >= 10000 Then
                            jj = jj + 1
                            Session("ColumnsToConvert")(i) = True
                            convApplied = True 'jam 2016-03-21
                            Exit For
                        End If
                    Next j

                End If
            Next i

            'jam 2016-03-21 If there are some trillions conversion
            For i = 1 To Session("COUNTE")
                If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 And convApplied And Not Session("ColumnsToConvert")(i) Then
                    Session("ColumnsToConvert")(i) = True
                End If
            Next i


        End If


        'jam 2016-03-06 If The same columns number are in Billions and have a value greater than 10K
        'If II <> 0 And jj <> 0 And II = jj Then
        'jam 2016-03-21 Only it's necessary one trillion column to apply the logic
        If II <> 0 And jj <> 0 Then
            If Session("prevHorizonYear") = 1 Or Not Session("fromTableDisp") Then
                For cc = 1 To Session("COUNTE")
                    For rr = 1 To vDisYear
                        'jam 2016-05-16
                        Session("PLOTTrillions")(rr, cc) = Session("PLOT")(rr, cc)

                        'jrs 2022/07/26
                        If Session("ColumnsToConvert")(cc) And Not IsDBNull(Session("PLOT")(rr, cc)) Then
                            Session("PLOT")(rr, cc) = CDbl(Session("PLOT")(rr, cc)) / 1000 'jam 2016-05-16
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
                                Session("PLOT")(rr, cc) = Session("PLOTTrillions")(rr, cc) 'jam 2016-05-16
                            Next rr
                            'jrs 2025/07/18
                            Session("GR4N")(cc) = Session("GR4N")(cc).replace("Trillion", "Billion")
                        Next cc
                    End If

                    Session("bTrillionsOk") = False
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' JAM 2020/11/09 Used for special characters in friendly URL
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function GrpPrepareForFriendlyURL(ByVal sParameter As String) As String
        'Remember to Update JavaScript Function too
        Dim tmp As String
        tmp = sParameter

        'jrs 2023/11/08
        GrpPrepareForFriendlyURL = ""
        If IsNothing(sParameter) Then Exit Function

        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")
        'jam 2020/12/24
        tmp = Replace(tmp, """", "%22")



        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        GrpPrepareForFriendlyURL = tmp
    End Function

    '' ' <summary>
    '' ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' ' 
    '' ' JAM 2020/11/10 Function used to identify the parameter in URL
    '' ' 
    '' ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' ' </summary>
    '' ' <param name="sParameter"></param>
    '' ' <returns></returns>
    Public Function ExistsParameter(ByVal idx As Integer) As Integer
        'Remember to Update JavaScript Function too
        Dim i As Integer
        Dim isthevalue As Integer


        isthevalue = -1
        'jrs 2023/07/27
        If Session("qtyParam") > 0 Then
            For i = 0 To Session("qtyParam")
                If idx = Session("seqParam")(i) Then
                    isthevalue = i
                    Exit For
                End If
            Next i
        End If

        ExistsParameter = isthevalue
    End Function

    'jam 2020/12/05
    Private Sub mnuExit_Click(isMainMenu As Boolean)


        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim vform As String
        Dim sLst As String




        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        'vfriendlyurl = FriendlyUrl.Resolve("~/frm_MainMenu.aspx")



        'jrs 2023/07/27 changed from Or to And
        If Request.QueryString("ListNames") Is Nothing And ExistsParameter(0) = -1 Then




            'jam 2020/12/21
            If Not isMainMenu Then
                vform = (Session("GraphDisplayParent"))
                vfriendlyurl = FriendlyUrl.Resolve("~/" & Session("GraphDisplayParent"))
            Else
                vform = "frm_MainMenu.aspx"
                vfriendlyurl = FriendlyUrl.Resolve("~/" & vform)
            End If


            'vurl = FriendlyUrl.Href("~/" & Session("GraphDisplayParent"))

            vurl = FriendlyUrl.Href("~/" & vform)

        Else

            'jam 2020/12/21
            If Not isMainMenu Then

                sLst = Session("RetParam")(ExistsParameter(0))
                sLst = Replace(sLst, """,""", ",,")
                'jrs 2023/08/03
                If Len(sLst) > 2 Then
                    sLst = Mid(sLst, 2, Len(sLst) - 2)
                End If

                sLst = Replace(sLst, Chr(127), " ")

                'jam 2020/12/23
                sLst = GrpPrepareForFriendlyURL(sLst)

                'cswmGroup1.OnMenuBarClick = "showhg();location.href='" & Page.ResolveUrl(Session("GraphDisplayParent")) & "?ListNames=" & sLst & "';"

                vform = (Session("GraphDisplayParent"))

                vurl = FriendlyUrl.Href("~/" & Session("GraphDisplayParent") & "?ListNames=" & sLst)
            Else
                vform = ("frm_MainMenu.aspx")

                vurl = FriendlyUrl.Href("~/" & vform)

            End If


        End If

        Session.Remove("qtyParam")
        Session.Remove("seqParam")
        Session.Remove("RetParam")

        'jam 2021/02/14
        Session.Remove("isLeg")
        Session.Remove("numLeg")
        Session.Remove("isAlias")
        Session.Remove("numAlias")

        'jam 2025/01/18
        Session.Remove("bVARN0Loaded")


        Response.Redirect(vurl)


    End Sub

    'jrs 2023/07/18
    Private Function OnlyGroupsSelected() As Boolean
        OnlyGroupsSelected = True
        Dim idx = gGetDimIdx(1, Session("GR1N")(1), False, Me)
        If idx > 0 Then
            OnlyGroupsSelected = False
        End If
    End Function


    'jrs 2023/07/18
    Private Function OnlyUniqueFirstDimensions() As Boolean
        OnlyUniqueFirstDimensions = True
        Dim sListDim1 As List(Of String) = New List(Of String)
        For i = 1 To Session("COUNTE")
            If Trim(Session("GR2N")(i)) > "" Then
                OnlyUniqueFirstDimensions = False
                Exit Function
            End If
            If Trim(Session("GR1N")(i)) > "" Then
                If sListDim1.Contains(Session("GR1N")(i)) Then
                    OnlyUniqueFirstDimensions = False
                    Exit Function
                Else
                    sListDim1.Add(Session("GR1N")(i))
                End If
            End If
        Next
    End Function

End Class

