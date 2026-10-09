Option Strict Off
Option Explicit On

'jam 2021/04/18
Imports System.Drawing
Imports EO.Web.Internal
Imports Microsoft.Ajax.Utilities
Imports Microsoft.AspNet.FriendlyUrls

Partial Class frm_CountryProfile
    Inherits Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'jam 2021/04/18
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim othVar As String
        Dim othDim2 As String
        Dim othDim3 As String
        Dim othLine As String
        Dim qtyPAram As Integer


        'jam 2021/04/19
        If Not IsNothing(Request.QueryString.Get("Var")) Or Not IsNothing(Request.QueryString.Get("Dim2")) Or Not IsNothing(Request.QueryString.Get("Dim3")) Or Not IsNothing(Request.QueryString("Line")) Or Not IsNothing(Request.QueryString("Country")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                othCountry = ""

            End If

            If Not IsNothing(Request.QueryString("Var")) And Not Page.IsPostBack Then

                othVar = Request.QueryString("Var")

            Else
                othVar = ""
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

            If Not IsNothing(Request.QueryString("Line")) And Not Page.IsPostBack Then

                othLine = Request.QueryString("Line")

            Else
                othLine = ""

            End If


            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_CountryProfile.aspx")


            If othCountry <> "" And othVar <> "" And othDim2 <> "" And othDim3 <> "" And othLine <> "" Then
                vurl = FriendlyUrl.Href("~/frm_CountryProfile", CountryProfPrepareForFriendlyURL(othCountry), othVar, othLine, othDim2, othDim3)

                Response.Redirect(vurl)

            Else
                If othCountry <> "" And othVar = "" And othDim2 = "" And othDim3 = "" And othLine = "" Then
                    vurl = FriendlyUrl.Href("~/frm_CountryProfile", CountryProfPrepareForFriendlyURL(othCountry))

                    Response.Redirect(vurl)

                Else
                    If othCountry <> "" And othVar <> "" And othDim2 = "" And othDim3 = "" And othLine = "" Then
                        vurl = FriendlyUrl.Href("~/frm_CountryProfile", CountryProfPrepareForFriendlyURL(othCountry), othVar)

                        Response.Redirect(vurl)

                    Else 'jam 2021/04/30
                        If othCountry <> "" And othVar <> "" And othDim2 = "" And othDim3 = "" And othLine <> "" Then
                            vurl = FriendlyUrl.Href("~/frm_CountryProfile", CountryProfPrepareForFriendlyURL(othCountry), othVar, othLine)


                            Response.Redirect(vurl)

                        Else 'jam 2021/04/30

                            'jam 2021/05/03
                            If othCountry <> "" And othVar <> "" And othDim2 <> "" And othDim3 = "" And othLine <> "" Then
                                vurl = FriendlyUrl.Href("~/frm_CountryProfile", CountryProfPrepareForFriendlyURL(othCountry), othVar, othLine, othDim2)


                                Response.Redirect(vurl)

                            Else

                                If othCountry <> "" And othVar <> "" And othDim2 <> "" And othDim3 <> "" And othLine <> "" Then
                                    vurl = FriendlyUrl.Href("~/frm_CountryProfile", CountryProfPrepareForFriendlyURL(othCountry), othVar, othLine, othDim2, othDim3)


                                    Response.Redirect(vurl)

                                Else 'jam 2021/05/03

                                    'jam 2025/01/18
                                    If othCountry <> "" And othVar <> "" And othDim2 <> "" And othDim3 = "" And othLine = "" Then
                                        vurl = FriendlyUrl.Href("~/frm_CountryProfile", CountryProfPrepareForFriendlyURL(othCountry), othVar, othLine, othDim2)


                                        Response.Redirect(vurl)


                                    Else
                                        vurl = FriendlyUrl.Href("~/frm_CountryProfile", CountryProfPrepareForFriendlyURL(othCountry))


                                        Response.Redirect(vurl)

                                    End If
                                End If





                            End If




                        End If

                    End If

                End If


            End If


        End If


        'jam 2021/04/19
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String
        Dim vVar As String
        Dim vDim2 As String
        Dim vDim3 As String
        Dim vLine As String

        'jam 2021/05/01
        If segments.Count = 0 Then

            vCountry = Session("MMCountry")
            vVar = Nothing
            vLine = Nothing
            vDim2 = Nothing
            vDim3 = Nothing

        Else



            If segments.Count = 1 Then

                vCountry = segments(0)
                vVar = Nothing
                vLine = Nothing
                vDim2 = Nothing
                vDim3 = Nothing

            ElseIf segments.Count = 2 Then
                vCountry = segments(0)
                vVar = segments(1)
                vLine = Nothing
                vDim2 = Nothing
                vDim3 = Nothing


            ElseIf segments.Count = 3 Then
                'jam 2025/01/18 Managing Table option
                If segments(2) = "True" Then

                    vCountry = segments(0)
                    vVar = segments(1)
                    vLine = segments(2)
                    vDim2 = Nothing
                    vDim3 = Nothing
                Else
                    If segments(2) = "False " Then
                        vCountry = segments(0)
                        vVar = segments(1)
                        vLine = segments(2)
                        vDim2 = Nothing
                        vDim3 = Nothing
                    Else
                        vCountry = segments(0)
                        vVar = segments(1)
                        vLine = Nothing
                        vDim2 = segments(2)
                        vDim3 = Nothing

                    End If
                End If
            ElseIf segments.Count = 4 Then
                vCountry = segments(0)
                vVar = segments(1)
                    vLine = segments(2)
                    vDim2 = segments(3)
                    vDim3 = Nothing

            ElseIf segments.Count = 5 Then 'jam 2025/01/25 Change 4 to 5
                vCountry = segments(0)
                vVar = segments(1)
                vLine = segments(2)
                vDim2 = segments(3)
                vDim3 = segments(4)


            Else
                vCountry = segments(0)
                Session("vCountry") = vCountry
                vVar = Nothing
                vDim2 = Nothing
                vDim3 = Nothing
                vLine = Nothing

            End If

        End If

        Session("vCountry") = vCountry
        Session("vVar") = vVar
        Session("vDim2") = vDim2
        Session("vDim3") = vDim3
        Session("vLine") = vLine






        If Not Page.IsPostBack Or Session("bTimeOut") = True Then
            Session("bTimeOut") = False

            Session("TableDisplayParent") = "frm_CountryProfile.aspx"
            'jrs 2008/10/07
            Session("MsgParent") = "frm_CountryProfile.aspx"
            'Check if Click on Link
            Dim sVar As String
            'jam 2021/04/19
            'sVar = Request.QueryString.Get("Var")
            sVar = Session("vVar")
            If Not sVar Is Nothing And Session("sRegCtrPr") > "" Then 'jrs 2011/07/22 added check to session variable
                Dim sDim2 As String, sDim3 As String
                'jam 2021/04/19
                'sDim2 = Request.QueryString.Get("Dim2")
                'sDim3 = Request.QueryString.Get("Dim3")

                sDim2 = Session("vDim2")
                sDim3 = Session("vDim3")

                'jrs 2011/04/05
                Dim sLine As String
                'jam 2021/04/19
                'sLine = Request.QueryString.Get("Line")
                sLine = Session("vLine")
                'jam 2021/04/19
                'If sLine = "" Then
                If sLine = "" Or sLine Is Nothing Then
                    sLine = "False"
                End If
                lblLinkText_Click(sVar, sDim2, sDim3, CBool(sLine))
            End If

            Dim rs As DataTable

            'jrs 2011/03/24
            ' If Not IsNothing(Request.QueryString("Country"))  Then
            'jam 2021/05/01
            If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Session("vCountry")) Then
                Session("MMCountry") = Request.QueryString("Country")
                'jam 2021/05/01
                If IsNothing(Session("MMCountry")) Then
                    'jam 2021/05/02
                    If Session("vCountry") <> "" Then
                        Session("MMCountry") = Session("vCountry")
                    End If

                End If


                'jam 2025/10/30 Unifying Database opening mode
                'jam 2025/06/13
                SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT FIPS_CODE, Country FROM [Country Translation] where [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs, "", True)
                'SetDataTablefromAccessJournal(Session("FdrvRun") & "IFs.mdb", "SELECT FIPS_CODE, Country FROM [Country Translation] where [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs)
                If rs.Rows.Count = 0 Then
                    Session("MMCountry") = ""
                    rs = Nothing
                End If
            End If

            If Session("MMCountry") = "" Then
                Session("MMCountry") = "US"
                Session("MMCtryName") = "United States"
            End If

            'jrs 2011/05/02
            'jam 2021/04/19
            Session("DisplayParent") = "frm_CountryProfile.aspx?Country=" & Session("MMCountry")
            'jam 2021/04/30 It'll invoked from another form, itsn't necessary friendly URL
            'Session("DisplayParent") = Page.ResolveUrl("~/frm_CountryProfile.aspx") & "/" & Session("MMCountry")
            Session("TableDisplayParent") = Session("DisplayParent")
            Session("MsgParent") = Session("DisplayParent")

            'Load Country Image
            '********************************************
            Dim fname As String
            Dim path As String

            If rs Is Nothing Then

                'jam 2025/10/30 Unifying Database opening mode
                'jam 2025/06/13
                SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT FIPS_CODE, Country FROM [Country Translation] where [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs, "", True)
                'SetDataTablefromAccessJournal(Session("FdrvRun") & "IFs.mdb", "SELECT FIPS_CODE, Country FROM [Country Translation] where [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs)

            End If

            Dim fips As String
            fips = rs.Rows(0).Item("FIPS_CODE")
            Session("MMCtryName") = rs.Rows(0).Item("Country")
            fname = fips + ".gif"
            path = "~/Maps/" & fname
            Image1.ImageUrl = path

            'jam 2026/03/29 For memory usage is required image automatic dispose
            'jrs 2008/08/19
            'If Dir(DataFileDir & "Country Maps\" & fname) > "" Then
            '    'jrs 2008/07/25 
            '    'size it
            '    Dim source As New Bitmap(DataFileDir & "Country Maps\" & fname)
            '    If source.Height > 376 Then
            '        Image1.Height = 376
            '        Image1.Width = source.Width * 376 / source.Height
            '        Image1.Enabled = True
            '    End If
            'End If

            'jam 2026/03/29 For memory usage is required image automatic dispose
            If Dir(DataFileDir & "Country Maps\" & fname) > "" Then
                Try

                    If Dir(DataFileDir & "Country Maps\" & fname) > "" Then
                        Using source As New Bitmap(DataFileDir & "Country Maps\" & fname)
                            If source.Height > 376 Then
                                Image1.Height = 376
                                Image1.Width = source.Width * 376 / source.Height
                                Image1.Enabled = True
                            End If
                        End Using
                    End If
                Catch ex As Exception
                    Session("Message") = $"Error loading country map: {fname}"
                    Support.FormManager.Show(New Page, "frm_Message.aspx")

                End Try
            End If



            'jam 2025/02/01
            'If fUsrDefCat.Value <> "" Then
            '    Session("vcUsrDefCat") = fUsrDefCat.Value
            'Else
            '    Session("vcUsrDefCat") = "Population"

            'End If


            'jam 2025/02/02
            If Not Request.QueryString("pUsrDefCat") Is Nothing Then
                Session("vcUsrDefCat") = Request.QueryString("pUsrDefCat")
            End If

            'jam 2025/01/25
            If Session("vcUsrDefCat") Is Nothing Then
                Session("vcUsrDefCat") = "Population"
            End If



            'Load Profile Var List
            '*******************************************
            Dim sql As String
                'jrs 2018/06/13 changed from I.[SubModule Name] as  to L.UsrDefCat
                'jrs 2013/01/18 changed from using ifsVar.Units to ifsVar.Display Units
                sql = "SELECT L.ListOrder, I.Definition, L.VarName, L.Param1, L.Param2, L.Param3, L.DisplayTitle, L.Formula, L.FormulaId, L.FormulaDet, " &
            "L.FormulaDetHis, L.UsrDefCat, L.NumDecimals, I.[Display Units] as Units FROM VarLists as L, IFsVar as I WHERE L.VarName = I.Name AND L.ListName = '" &
            "Country Profile' AND L.Formula = False AND L.History = False" 'jrs 2005/11/10 added L.History
                'jam 2025/01/25
                sql = sql & " AND L.UsrDefCat = " & """" & Session("vcUsrDefCat") & """"
                'jrs 2008/01/30 added not INCOMELT1LN and not INCOMELT2LN
                sql = sql & " UNION SELECT L.ListOrder, L.VarName as Definition, L.VarName, L.Param1, L.Param2, L.Param3, L.DisplayTitle, L.Formula, " &
            "L.FormulaId, L.FormulaDet, L.FormulaDetHis, L.UsrDefCat, L.NumDecimals, '' as Units FROM VarLists as L WHERE (L.Formula = True " &
            "OR L.History = True OR (L.VarName LIKE 'INCOMELT*LN*' AND L.VarName <> 'INCOMELT190LN' AND L.VarName <> 'INCOMELT310LN' AND L.VarName <> 'INCOMELT125LN2005' AND L.VarName <> 'INCOMELT200LN2005')) AND L.ListName = '" &
            "Country Profile'" 'jrs 2005/11/10 added OR History = true
                'jam 2025/01/25
                sql = sql & " AND L.UsrDefCat = " & """" & Session("vcUsrDefCat") & """"

                'jam 2025/01/25 Adding Categories without data
                sql = sql & " UNION "
                sql = sql & " SELECT DISTINCT 99, '', '', '', '', '', '', 0, 0, 0,"
                sql = sql & " 0, l.UsrDefCat, 1, '' "
                sql = sql & " FROM VarLists As L, IFsVar As I "
                sql = sql & " WHERE L.VarName = I.Name And L.ListName = "
                sql = sql & " 'Country Profile' AND L.Formula = False AND L.History = False "
                sql = sql & " AND L.UsrDefCat <> " & """" & Session("vcUsrDefCat") & """"

            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/06/13 Changing opening mode for sqlite database
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFSVAR.mdb", sql, "", rs, "", True)
            'SetDataTablefromAccessJournal(Session("PutFdrvRun") & "IFSVAR.mdb", sql, "", rs)

            If Session("iFileCtrPr") Is Nothing Then
                    Session("iFileCtrPr") = 1
                    Session("iHorizonCtrPr") = 1 '0. Forecast, 1. Hist+Fore, 2. Hist Emp
                    Session("bAllRegsCtrPr") = False
                End If

                If Not (Session("oldLastYear") Is Nothing) Then
                    'jrs 2008/07/15
                    If Session("COUNTE") > 0 Then
                        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                    End If

                    Session("LastYear") = Session("oldLastYear")
                    Session("oldLastYear") = Nothing
                End If
                If Not (Session("oldFirstYear") Is Nothing) Then
                    Session("FirstYear") = Session("oldFirstYear")
                    Session("oldFirstYear") = Nothing
                End If
                If Session("DisplayLastYear") = 0 Then
                    Session("DisplayLastYear") = Session("LastYear")
                End If

                'ReDim sVars(1) As String
                'ReDim sDim2s(1) As String
                'ReDim sDim3s(1) As String

                If rs.Rows.Count > 0 Then
                    BuildText(rs)
                End If

                'jrs 2011/03/25
                If Session("RW") Is Nothing Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "BrowserSize",
                JscriptSaveBrowserSize, True)
                End If

            End If

            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text &
        " - " & Session("MMCtryName") & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & ModelName & " " & ModelVersion 'jrs 2011/04/29 Session: " & Session("SessionIdx")

        'jrs 2012/02/23 
        Page.Header.Title = "Key Development Forecasts for " & Session("MMCtryName")

    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cswmWebMenu.UserData = "jsolorza::976739975"
        'jam 2021/04/19
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"


        'jam 2021/04/19
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")


        cswmWebMenu.ScrollEnabled = True
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"

        'jrs 2011/03/25 cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        'jrs 2013/01/22
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Change Country"
        cswmGroup1.ID = "mnuChgCtry"

        Dim R As Integer
        For R = 1 To NR
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = GRPS(1, R)
        Next

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Options"
        cswmGroup1.ID = "mnuDisplayOpt"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Horizon"
        cswmItem.ID = "mnuHorizon"

        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Forecast"
        cswmItem.ID = "mnuForecast"
        If Session("iHorizonCtrPr") = 0 Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "History plus Forecast"
        cswmItem.ID = "mnuHist+For"
        If Session("iHorizonCtrPr") = 1 Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "History (empirical)"
        cswmItem.ID = "mnuHistory"
        If Session("iHorizonCtrPr") = 2 Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        '***
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Scenarios"
        cswmItem.ID = "mnuSce"

        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Working"
        cswmItem.ID = "mnuSceWorking"
        If Session("iFileCtrPr") = 0 Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Base"
        cswmItem.ID = "mnuSceBase"
        If Session("iFileCtrPr") = 1 Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        'Load Other scenario menus
        LoadSceMenus(cswmGroup2)

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "All"
        cswmItem.ID = "mnuSceAll"
        If Session("iFileCtrPr") = -1 Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        '***
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regions/Countries"
        cswmItem.ID = "mnuRegs"

        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Single Region/Country"
        cswmItem.ID = "mnuSingleReg"
        If Session("bAllRegsCtrPr") = False Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "All Regions/Countries"
        cswmItem.ID = "mnuAllRegs"
        If Session("bAllRegsCtrPr") = True Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        '***
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use all available Historical Data"
        cswmItem.ID = "mnuuseAllHis"
        If CBool(Session("buseAllHis")) Then
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/04/19
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"blank.gif"
        End If

        'jrs 2008/07/14
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Map Source"
        cswmGroup1.OnMenuBarClick = "alert('The maps shown in the Country Profile are those available in the U.S. Central Intelligence Agency’s World Factbook.');"

        'jrs 2014/08/27
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        cswmGroup1.OnMenuBarClick = LoadHelpJS("Country Profile", "")

    End Sub


    Private Sub BuildText(ByVal rs As DataTable)
        'using 1 label control build text
        '1. Titles
        '2. Regular Text
        '3. Links
        Dim sUsrDefCat As String, sprevUsrDefCat As String = "", sVar As String
        Dim bPov As Boolean, bPovPct As Boolean, bPovFind As Boolean, PovLevel As Single
        'jrs 2011/06/17
        Dim bIncR As Boolean, PovLevel2 As Single
        'jrs 2014/10/21
        Dim b2005PPP As Boolean

        Dim sVal As String
        Dim Dim2 As String, Dim3 As String

        Session("sRegCtrPr") = getRegionFromCtry(Session("MMCtryName"))

        Dim iRs As Integer = 0
        'jrs 2008/03/19
        Dim bGroups As Object
        bGroups = False 'provinces not implemented on the Web
        lblText.Text = ""
        'jrs 2012/01/05
        lblTitLink.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span>Forecast Categories for " & Session("MMCtryName") & "</span><br>"

        'jrs 2018/06/13
        Dim CatCount As Integer = 0

        'jam 2025/09/01 Changing concatenation process
        Dim sb As New StringBuilder(lblText.Text)


        Do While rs.Rows.Count > iRs
            sVar = rs.Rows(iRs).Item("VarName")

            If sVar <> "" Then


                Dim2 = ""
                Dim3 = ""

                If IfsVars.Item(sVar).Index = -1 And Not rs.Rows(iRs).Item("Formula") Then
                    checkPovertyVarName(sVar, bPov, bPovPct, bPovFind, PovLevel, bIncR, PovLevel2, b2005PPP) 'jrs 2011/06/17
                Else
                    bPov = False
                    bPovPct = False
                    bPovFind = False
                    'jrs 2011/06/17
                    bIncR = False
                End If

                sUsrDefCat = IIf(IsDBNull(rs.Rows(iRs).Item("UsrDefCat")), "", rs.Rows(iRs).Item("UsrDefCat"))
                If sUsrDefCat = "" Then
                    If IfsVars.Item(sVar).Index > -1 Then
                        sUsrDefCat = VUsrDefCat(IfsVars.Item(sVar).Index)
                    ElseIf bPov Or bPovPct Or bPovFind Or bIncR Then 'jrs 2011/06/17
                        sUsrDefCat = VUsrDefCat(IfsVars.Item("INCOMELT190LN").Index) 'jrs 2016/10/12 changed to 190
                    End If
                End If

                'Titles
                If sUsrDefCat <> sprevUsrDefCat Then
                    'jrs 2018/06/13
                    CatCount = CatCount + 1

                    If sprevUsrDefCat > "" Then

                        'jam 2025/09/01 Changing concatenation process
                        'lblText.Text = lblText.Text & "</font>"
                        'lblText.Text = lblText.Text & "</div>"

                        sb.Append("</font>")
                        sb.Append("</div>")
                    End If
                    sprevUsrDefCat = sUsrDefCat
                    'jam 2025/09/01 Changing concatenation process
                    '    lblText.Text = lblText.Text & "<br><div id=""HdrCat" & sUsrDefCat & CStr(CatCount) & """ style=""background-color: #EBDAB6"" >" &
                    '"<input type=""button"" id=""a" & sUsrDefCat & CStr(CatCount) & """ onclick =""javascript:expandCollapse('d" & sUsrDefCat & CStr(CatCount) &
                    '"', 'a" & sUsrDefCat & CStr(CatCount) & "')"" href=""javascript:;"" onsubmit=""return false;"" value=""-"" /> " &
                    '"<font face=""Arial"" size=""4"">" &
                    '"<a name=""" & sUsrDefCat & CStr(CatCount) & """>" & sUsrDefCat & "</a> Forecast for " & Session("MMCtryName")

                    sb.Append("<br><div id=""HdrCat" & sUsrDefCat & CStr(CatCount) & """ style=""background-color: #EBDAB6"" >" &
                "<input type=""button"" id=""a" & sUsrDefCat & CStr(CatCount) & """ onclick =""javascript:expandCollapse('d" & sUsrDefCat & CStr(CatCount) &
                "', 'a" & sUsrDefCat & CStr(CatCount) & "')"" href=""javascript:;"" onsubmit=""return false;"" value=""-"" /> " &
                "<font face=""Arial"" size=""4"">" &
                "<a name=""" & sUsrDefCat & CStr(CatCount) & """>" & sUsrDefCat & "</a> Forecast for " & Session("MMCtryName"))


                    Select Case sUsrDefCat
                        Case "Population"
                            'jam 2021/04/30
                            'lblText.Text = lblText.Text & " - <a href='frm_PopCohortDisp.aspx?Country=" &
                            'Session("MMCountry") & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                            '"src=""Images\Pyramid_Icon_26x26.jpg"" title=""Show a Population Distribution""/></a>"

                            'jam 2025/09/01 Changing concatenation process
                            '    lblText.Text = lblText.Text & " - <a href='" & Page.ResolveUrl("~/frm_PopCohortDisp.aspx?Country=" & Session("MMCountry")) & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                            '"src=""" & Page.ResolveUrl("~/Images/Pyramid_Icon_26x26.jpg") & """ title=""Show a Population Distribution""/></a>"

                            sb.Append(" - <a href='" & Page.ResolveUrl("~/frm_PopCohortDisp.aspx?Country=" & Session("MMCountry")) & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                        "src=""" & Page.ResolveUrl("~/Images/Pyramid_Icon_26x26.jpg") & """ title=""Show a Population Distribution""/></a>")


                        Case "Health"
                            'jam 2021/04/30
                            'lblText.Text = lblText.Text & " - <a href='frm_MortCohorts.aspx?Country=" &
                            'Session("MMCountry") & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                            '"src=""Images\Pyramid_Icon_26x26.jpg"" title=""Show a Mortality Distribution""/></a>"

                            'jam 2025/09/01 Changing concatenation process
                            '     lblText.Text = lblText.Text & " - <a href='" & Page.ResolveUrl("~/frm_MortCohorts.aspx?Country=" & Session("MMCountry")) & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                            '"src=""" & Page.ResolveUrl("~/Images/Pyramid_Icon_26x26.jpg") & """ title=""Show a Mortality Distribution""/></a>"


                            sb.Append(" - <a href='" & Page.ResolveUrl("~/frm_MortCohorts.aspx?Country=" & Session("MMCountry")) & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                                "src=""" & Page.ResolveUrl("~/Images/Pyramid_Icon_26x26.jpg") & """ title=""Show a Mortality Distribution""/></a>")


                        Case "Education"
                            'jam 2021/04/30
                            'lblText.Text = lblText.Text & " - <a href='frm_EdnCohoDisp.aspx?Country=" &
                            'Session("MMCountry") & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                            '"src=""Images\Pyramid_Icon_26x26.jpg"" title=""Show an Education Distribution""/></a>"

                            'jam 2025/09/01 Changing concatenation process
                            '    lblText.Text = lblText.Text & " - <a href='" & Page.ResolveUrl("~/frm_EdnCohoDisp.aspx?Country=" & Session("MMCountry")) & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                            '"src=""" & Page.ResolveUrl("~/Images/Pyramid_Icon_26x26.jpg") & """ title=""Show an Education Distribution""/></a>"


                            sb.Append(" - <a href='" & Page.ResolveUrl("~/frm_EdnCohoDisp.aspx?Country=" & Session("MMCountry")) & "'><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                        "src=""" & Page.ResolveUrl("~/Images/Pyramid_Icon_26x26.jpg") & """ title=""Show an Education Distribution""/></a>")


                    End Select
                    'jrs 2013/01/18
                    If sUsrDefCat <> "Population" Then

                        'jam 2021/04/30
                        'lblText.Text = lblText.Text & "<a href=""#top""><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                        '    "src=""Images\top_icon.GIF"" title=""Top"" align=""right""/></a>"

                        'jam 2025/09/01 Changing concatenation process
                        'lblText.Text = lblText.Text & "<a href=""#top""><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                        '"src=""" & Page.ResolveUrl("~/Images/top_icon.GIF") & """ title=""Top"" align=""right""/></a>"


                        sb.Append("<a href=""#top""><img style=""border-top: #EBDAB6 10px solid; border-right-style: none; border-left-style: none; border-bottom-style: none;""" &
                        "src=""" & Page.ResolveUrl("~/Images/top_icon.GIF") & """ title=""Top"" align=""right""/></a>")


                    End If

                    'jam 2025/09/01 Changing concatenation process
                    'lblText.Text = lblText.Text & "</font>"
                    'lblText.Text = lblText.Text & "</div>"
                    'lblText.Text = lblText.Text & "<div class=""mid"" id=""d" & sUsrDefCat & CStr(CatCount) & """ style=""DISPLAY: inline"" >"
                    'lblText.Text = lblText.Text & "<font face=""Arial"" size=""2"">"


                    sb.Append("</font>")
                    sb.Append("</div>")
                    sb.Append("<div class=""mid"" id=""d" & sUsrDefCat & CStr(CatCount) & """ style=""DISPLAY: inline"" >")
                    sb.Append("<font face=""Arial"" size=""2"">")

                    'jam 2025/02/03
                    'lblTitLink.Text = lblTitLink.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=""#" & sUsrDefCat & CStr(CatCount) & """>" & sUsrDefCat & "</a><br>"
                    lblTitLink.Text = lblTitLink.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=""#"" onclick=" & """this.href = window.location.pathname + '?pUsrDefCat=" & sUsrDefCat & "';" & """>" & sUsrDefCat & "</a><br>"


                Else
                    'jam 2025/09/01 Changing concatenation process
                    'lblText.Text = lblText.Text & "<br>"

                    sb.Append("<br>")
                End If

                'Text
                'jam 2025/09/01 Changing concatenation process
                'lblText.Text = lblText.Text & rs.Rows(iRs).Item("Definition")

                sb.Append(rs.Rows(iRs).Item("Definition"))

                If Not IsDBNull(rs.Rows(iRs).Item("Param2")) Then
                    If rs.Rows(iRs).Item("Param2") > "" Then

                        'jam 2025/09/01 Changing concatenation process
                        'lblText.Text = lblText.Text & " - " & rs.Rows(iRs).Item("Param2")

                        sb.Append(" - " & rs.Rows(iRs).Item("Param2"))

                        Dim2 = rs.Rows(iRs).Item("Param2")
                    End If
                End If
                If Not IsDBNull(rs.Rows(iRs).Item("Param3")) Then
                    If rs.Rows(iRs).Item("Param3") > "" Then
                        'jam 2025/09/01 Changing concatenation process
                        'lblText.Text = lblText.Text & " - " & rs.Rows(iRs).Item("Param3")

                        sb.Append(" - " & rs.Rows(iRs).Item("Param3"))

                        Dim3 = rs.Rows(iRs).Item("Param3")
                    End If
                End If
                If Not IsDBNull(rs.Rows(iRs).Item("Units")) Then
                    If rs.Rows(iRs).Item("Units") > "" Then
                        'jam 2025/09/01 Changing concatenation process
                        'lblText.Text = lblText.Text & " - " & rs.Rows(iRs).Item("Units")

                        sb.Append(" - " & rs.Rows(iRs).Item("Units"))
                    End If
                End If
                'jam 2021/04/30
                'jrs 2011/04/05
                'lblText.Text = lblText.Text & " - <a href='frm_CountryProfile.aspx?Country=" & Session("MMCountry") & "&Var=" &
                'sVar & "&Dim2=" & Dim2 & "&Dim3=" & Dim3 & "&Line=True'><img border=""0"" " &
                '"src=""Images\Line_Graph_Icon_26x26.jpg"" title=""Draw a Line Graph""/></a>" &
                '" - <a href='frm_CountryProfile.aspx?Country=" & Session("MMCountry") & "&Var=" &
                'sVar & "&Dim2=" & Dim2 & "&Dim3=" & Dim3 & "'><img border=""0"" " &
                '"src=""Images\Table_Icon_26x26.jpg"" title=""Show a Table""/></a>"
                'jam 2021/04/30

                'jam 2025/09/01 Changing concatenation process
                '    lblText.Text = lblText.Text & " - <a href='" & Page.ResolveUrl("~/frm_CountryProfile.aspx?Country=" & Session("MMCountry") & "&Var=" &
                'sVar & "&Dim2=" & Dim2 & "&Dim3=" & Dim3 & "&Line=True") & "'><img border=""0"" " &
                '"src=""" & Page.ResolveUrl("~/Images/Line_Graph_Icon_26x26.jpg") & """ title=""Draw a Line Graph""/></a>" &
                '" - <a href='" & Page.ResolveUrl("~/frm_CountryProfile.aspx?Country=" & Session("MMCountry") & "&Var=" &
                'sVar & "&Dim2=" & Dim2 & "&Dim3=" & Dim3) & "'><img border=""0"" " &
                '"src=""" & Page.ResolveUrl("~/Images/Table_Icon_26x26.jpg") & """ title=""Show a Table""/></a>"


                sb.Append(" - <a href='" & Page.ResolveUrl("~/frm_CountryProfile.aspx?Country=" & Session("MMCountry") & "&Var=" &
            sVar & "&Dim2=" & Dim2 & "&Dim3=" & Dim3 & "&Line=True") & "'><img border=""0"" " &
            "src=""" & Page.ResolveUrl("~/Images/Line_Graph_Icon_26x26.jpg") & """ title=""Draw a Line Graph""/></a>" &
            " - <a href='" & Page.ResolveUrl("~/frm_CountryProfile.aspx?Country=" & Session("MMCountry") & "&Var=" &
            sVar & "&Dim2=" & Dim2 & "&Dim3=" & Dim3) & "'><img border=""0"" " &
            "src=""" & Page.ResolveUrl("~/Images/Table_Icon_26x26.jpg") & """ title=""Show a Table""/></a>")



                'instead of Line graph could use an icon, but need to create a nice one
                '<img border=""0"" src=""Images\linegraph.gif"" />

                'jrs 2011/05/02 sVal = LoadVal(sVar, Session("sRegCtrPr"), IIf(IsDBNull(rs.Rows(iRs).Item("Param2")), "", rs.Rows(iRs).Item("Param2")), _
                'IIf(IsDBNull(rs.Rows(iRs).Item("Param3")), "", rs.Rows(iRs).Item("Param3")), bGroups, FirstYearForecast)

                'jrs 2011/05/02 lblText.Text = lblText.Text & "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & CStr(FirstYearForecast) & ": " & sVal

                'Link
                sVal = LoadVal(sVar, Session("sRegCtrPr"), IIf(IsDBNull(rs.Rows(iRs).Item("Param2")), "", rs.Rows(iRs).Item("Param2")),
            IIf(IsDBNull(rs.Rows(iRs).Item("Param3")), "", rs.Rows(iRs).Item("Param3")), bGroups, Year(Now))

                'lblText.Text = lblText.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='frm_CountryProfile.aspx?Var=" & _
                'sVar & "&Dim2=" & Dim2 & "&Dim3=" & Dim3 & "'>" & CStr(Year(Now)) & ": " & sVal & "</a>"


                'jam 2025/09/01 Changing concatenation process
                'lblText.Text = lblText.Text & "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & CStr(Year(Now)) & ": " & sVal


                sb.Append("<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & CStr(Year(Now)) & ": " & sVal)

                sVal = LoadVal(sVar, Session("sRegCtrPr"), IIf(IsDBNull(rs.Rows(iRs).Item("Param2")), "", rs.Rows(iRs).Item("Param2")),
            IIf(IsDBNull(rs.Rows(iRs).Item("Param3")), "", rs.Rows(iRs).Item("Param3")), bGroups, 2030)

                'jam 2025/09/01 Changing concatenation process
                'lblText.Text = lblText.Text & "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2030: " & sVal

                sb.Append("<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2030: " & sVal)

                sVal = LoadVal(sVar, Session("sRegCtrPr"), IIf(IsDBNull(rs.Rows(iRs).Item("Param2")), "", rs.Rows(iRs).Item("Param2")),
            IIf(IsDBNull(rs.Rows(iRs).Item("Param3")), "", rs.Rows(iRs).Item("Param3")), bGroups, 2060)

                'jam 2025/09/01 Changing concatenation process
                'lblText.Text = lblText.Text & "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2060: " & sVal


                sb.Append("<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2060: " & sVal)

                'jam 2025/02/01 Moved outside conditional
                'iRs = iRs + 1



            Else 'jam 2025/02/01 Only showing categories
                sUsrDefCat = IIf(IsDBNull(rs.Rows(iRs).Item("UsrDefCat")), "", rs.Rows(iRs).Item("UsrDefCat"))

                'Titles
                If sUsrDefCat <> sprevUsrDefCat Then
                    'jrs 2018/06/13
                    CatCount = CatCount + 1

                    If sprevUsrDefCat > "" Then
                        'jam 2025/09/01 Changing concatenation process
                        'lblText.Text = lblText.Text & "</font>"
                        'lblText.Text = lblText.Text & "</div>"


                        sb.Append("</font>")
                        sb.Append("</div>")

                    End If
                    sprevUsrDefCat = sUsrDefCat
                    '    lblText.Text = lblText.Text & "<br><div id=""HdrCat" & sUsrDefCat & CStr(CatCount) & """ style=""background-color: #EBDAB6"" >" &
                    '"<button type=""submit"" id=""a" & sUsrDefCat & CStr(CatCount) & """ onclick =""javascript:changeCat('d" & sUsrDefCat & CStr(CatCount) &
                    '"', 'a" & sUsrDefCat & CStr(CatCount) & "')"" href=""javascript:;"" onsubmit=""return true;"" value=""-"" /> " &
                    '"<font face=""Arial"" size=""4"">" &
                    '"<a name=""" & sUsrDefCat & CStr(CatCount) & """>" & sUsrDefCat & "</a> Forecast for " & Session("MMCtryName")

                    'jam 2025/09/01 Changing concatenation process
                    '    lblText.Text = lblText.Text & "<br><div id=""HdrCat" & sUsrDefCat & CStr(CatCount) & """ style=""background-color: #EBDAB6"" >" &
                    '"<input type=""button"" id=""a" & sUsrDefCat & CStr(CatCount) & """ onclick =""javascript:changeCat('d" & sUsrDefCat & CStr(CatCount) &
                    '"', 'a" & sUsrDefCat & CStr(CatCount) & "')"" href=""javascript:;"" onsubmit=""return true;"" value=""Choose"" /> " &
                    '"<font face=""Arial"" size=""4"">" &
                    '"<a name=""" & sUsrDefCat & CStr(CatCount) & """>" & sUsrDefCat & "</a> Forecast for " & Session("MMCtryName")

                    sb.Append("<br><div id=""HdrCat" & sUsrDefCat & CStr(CatCount) & """ style=""background-color: #EBDAB6"" >" &
                "<input type=""button"" id=""a" & sUsrDefCat & CStr(CatCount) & """ onclick =""javascript:changeCat('d" & sUsrDefCat & CStr(CatCount) &
                "', 'a" & sUsrDefCat & CStr(CatCount) & "')"" href=""javascript:;"" onsubmit=""return true;"" value=""Choose"" /> " &
                "<font face=""Arial"" size=""4"">" &
                "<a name=""" & sUsrDefCat & CStr(CatCount) & """>" & sUsrDefCat & "</a> Forecast for " & Session("MMCtryName"))

                    'jam 2025/09/01 Changing concatenation process
                    'lblText.Text = lblText.Text & "</font>"
                    'lblText.Text = lblText.Text & "</div>"
                    'lblText.Text = lblText.Text & "<div class=""mid"" id=""d" & sUsrDefCat & CStr(CatCount) & """ style=""DISPLAY: inline"" >"
                    'lblText.Text = lblText.Text & "<font face=""Arial"" size=""2"">"

                    sb.Append("</font>")
                    sb.Append("</div>")
                    sb.Append("<div class=""mid"" id=""d" & sUsrDefCat & CStr(CatCount) & """ style=""DISPLAY: inline"" >")
                    sb.Append("<font face=""Arial"" size=""2"">")

                    'jam 2025/02/03
                    'lblTitLink.Text = lblTitLink.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=""#" & sUsrDefCat & CStr(CatCount) & """>" & sUsrDefCat & "</a><br>"
                    lblTitLink.Text = lblTitLink.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=""#"" onclick=" & """this.href = window.location.pathname + '?pUsrDefCat=" & sUsrDefCat & "';" & """>" & sUsrDefCat & "</a><br>"


                Else
                    'jam 2025/09/01 Changing concatenation process
                    'lblText.Text = lblText.Text & "<br>"

                    sb.Append("<br>")

                End If
            End If


            'lblText.Text = lblText.Text & "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2060: " & sVal

            iRs = iRs + 1

        Loop
        'jam 2025/09/01 Changing concatenation process
        'lblText.Text = lblText.Text & "</font>"
        'lblText.Text = lblText.Text & "</div>"

        sb.Append("</font>")
        sb.Append("</div>")

        'jam 2025/09/01 Assign final value
        lblText.Text = sb.ToString()

    End Sub

    Private Function LoadVal(ByVal sVar As String, ByVal sReg1 As String, ByVal sDim2 As String, ByVal sDim3 As String, ByVal bGroups As Object, ByVal iYear As Integer) As String
        Dim Vals(1) As Single
        Dim idxFile As Integer
        If Session("iFileCtrPr") > -1 Then
            idxFile = Session("iFileCtrPr")
        Else
            idxFile = 1
        End If
        If Session("mnuSQLite") = "True" Then

            'jrs 2025/01/10 added FileForDisplay#, dfsVar# to store dataframe in session and avoid reloading 3 times for current year, 2030 and 2060
            'changed open file and close file to false, false
            gLoadForDisplayNET(sVar, -1, sReg1, -1, sDim2, -1, sDim3, -1, "", idxFile, False, False,
            bGroups, 1, Vals, 1, 1, iYear, iYear, iYear - FirstYearForecast, True,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"),
            Session("FileForDisplay" + CStr(idxFile)), Session("df" + sVar + CStr(idxFile)))

        Else
            gLoadForDisplay(sVar, -1, sReg1, -1, sDim2, -1, sDim3, -1, "", idxFile, True, True,
            bGroups, 1, Vals, 1, 1, iYear, iYear, iYear - FirstYearForecast, True,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If

        LoadVal = Trim(ValueScale(Vals(1), False, True))
    End Function


    Private Sub lblLinkText_Click(ByVal sVar As String, ByVal sDim2 As String, ByVal sDim3 As String, ByVal bLine As Boolean)

        'jam 2021/04/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'set up years
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        If Session("iHorizonCtrPr") = 1 Then 'History plus forecast
            Session("FirstYear") = FirstYearHistory
        ElseIf Session("iHorizonCtrPr") = 2 Then
            Session("FirstYear") = FirstYearHistory
            Session("LastYear") = LastYearHistory
        End If

        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim sDef As String, sUn As String

        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        'Loop for files
        Dim iFst As Integer, iLst As Integer, idxFile As Integer
        If Session("iFileCtrPr") = -1 Then
            iFst = 0
            iLst = Session("FilesSelected")
        Else
            iFst = Session("iFileCtrPr")
            iLst = Session("iFileCtrPr")
        End If

        'Loop for Regions
        Dim iRegFst As Integer, iRegLst As Integer, RegIdx As Integer
        If Session("bAllRegsCtrPr") = True Then 'All Regions
            iRegFst = 1
            iRegLst = NR
        Else
            iRegFst = IFsRegs.Item(Session("sRegCtrPr")).Index
            iRegLst = iRegFst
        End If

        Dim idxVar As Integer

        For RegIdx = iRegFst To iRegLst
            For idxFile = iFst To iLst
                Session("COUNTE") = Session("COUNTE") + 1

                Dim iFirstYear As Integer, iLastYear As Integer
                Dim iRowPosition As Integer, FinAll As Integer
                If Session("iHorizonCtrPr") = 1 Then 'History plus forecast
                    iFirstYear = FirstYearForecast
                    iRowPosition = LastYearHistory - FirstYearHistory + 1
                Else
                    iFirstYear = Session("FirstYear")
                    iRowPosition = 1
                End If
                iLastYear = Session("LastYear")

                If Session("iHorizonCtrPr") = 2 Then 'Historic empirical
                    gLoadForDisplayHis(sVar, GRPS(1, RegIdx), RegIdx, sDim2, -1, sDim3, -1,
                    False, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory,
                    False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                    Session("LastYearForecast"), Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                    gLoadForDisplayHisSaveLbls(Session("COUNTE"), sVar, GRPS(1, RegIdx), False, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                    Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                Else
                    If Session("mnuSQLite") = "True" Then
                        'jrs 2025/01/10 added FileForDisplay#, dfsVar# to store dataframe in session and avoid reloading 3 times for current year, 2030 and 2060
                        'changed open file and close file to false, false
                        gLoadForDisplayNET(sVar, idxVar, GRPS(1, RegIdx), RegIdx, sDim2, -1, sDim3, -1, "", idxFile, False, False,
                        False, 1, Session("PLOT"), Session("COUNTE"), iRowPosition, iFirstYear, iLastYear, 0, False,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"),
                        Session("FileForDisplay" + CStr(idxFile)), Session("df" + sVar + CStr(idxFile)))
                    Else
                        gLoadForDisplay(sVar, idxVar, GRPS(1, RegIdx), RegIdx, sDim2, -1, sDim3, -1, "", idxFile, True, True,
                        False, 1, Session("PLOT"), Session("COUNTE"), iRowPosition, iFirstYear, iLastYear, 0, False,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    End If
                    gLoadForDisplaySaveLbls(Session("COUNTE"), idxVar, GRPS(1, RegIdx), sDim2, sDim3, False, idxFile, "",
                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                    If Session("iHorizonCtrPr") = 1 Then 'History plus forecast
                        FinAll = Session("FinYear")(Session("COUNTE"))
                        gLoadForDisplayHis(sVar, GRPS(1, RegIdx), RegIdx, sDim2, -1, sDim3, -1,
                        False, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                        False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                        Session("LastYearForecast"), Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                        Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))

                        Session("FinYear")(Session("COUNTE")) = LastYearHistory - FirstYearHistory + FinAll 'jrs 2006/09/22
                    End If
                End If
            Next idxFile
        Next RegIdx

        'jrs 2012/09/28
        If Session("iHorizonCtrPr") = 1 Then 'History plus forecast
            Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast"
        Else
            Session("VarSelCheck") = ""
        End If

        'jrs 2011/04/05
        If bLine Then
            Session("GType") = "LINE"
            Session("GraphDisplayParent") = "frm_CountryProfile.aspx"

            'jam 2021/04/30
            'Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_GraphicalDisplay.aspx")
            vurl = FriendlyUrl.Href("~/frm_GraphicalDisplay")
            Response.Redirect(vurl)


        Else
            'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")


            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")
            vurl = FriendlyUrl.Href("~/frm_TableDisplay")
            Response.Redirect(vurl)

        End If

    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        Dim i As Integer

        'jam 2021/04/19
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                Dim sTarget As String
                sTarget = "frm_MainMenu.aspx"
                If Session("RW") Is Nothing Then
                    Dim resval As String
                    resval = Request.Form("txtResValueHolder").ToString
                    If resval > "" Then
                        Dim findex As Integer
                        Dim width = resval.Substring(0, resval.IndexOf(","))
                        findex = resval.IndexOf(",") + 1
                        Dim height = resval.Substring(findex, resval.Length - resval.IndexOf(",") - 1)
                        sTarget = sTarget & "?W=" & width & "&H=" & height
                    End If
                End If
                'jam 2021/07/01
                'Support.FormManager.Show(Me, sTarget)

                'jam 2025/01/25
                Session.Remove("vcUsrDefCat")

                'jam 2021/07/01
                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/" + sTarget)


                vurl = FriendlyUrl.Href("~/" + sTarget)

                Response.Redirect(vurl)



            Case "Forecast"
                Session("iHorizonCtrPr") = 0
                'jam 2021/04/19
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuForecast").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuHist+For").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuHistory").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Case "History plus Forecast"
                Session("iHorizonCtrPr") = 1
                'jam 2021/04/19
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuForecast").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuHist+For").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuHistory").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Case "History (empirical)"
                Session("iHorizonCtrPr") = 2
                'jam 2021/04/19
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuForecast").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuHist+For").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuHorizon").Group.Items("mnuHistory").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Case "Working"
                Session("iFileCtrPr") = 0
                For i = 0 To cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items.Count - 1
                    'jam 2021/04/19
                    cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                Next
                'jam 2021/04/19
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items("mnuSceWorking").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                RefreshLinks()
            Case "Base"
                Session("iFileCtrPr") = 1
                For i = 0 To cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items.Count - 1
                    'jam 2021/04/19
                    cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"blank.gif"
                Next
                'jam 2021/04/19
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items("mnuSceBase").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                RefreshLinks()
            Case "All"
                Session("iFileCtrPr") = -1
                For i = 0 To cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items.Count - 1
                    'jam 2021/04/19
                    cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                Next
                'jam 2021/04/19
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group.Items("mnuSceAll").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Case "Single Region/Country"
                Session("bAllRegsCtrPr") = False
                'jam 2021/04/19
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuRegs").Group.Items("mnuSingleReg").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuRegs").Group.Items("mnuAllRegs").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Case "All Regions/Countries"
                Session("bAllRegsCtrPr") = True
                'jam 2021/04/19
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuRegs").Group.Items("mnuSingleReg").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuRegs").Group.Items("mnuAllRegs").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Case "Use all available Historical Data" 'jrs 2006/09/26
                Session("buseAllHis") = Not CBool(Session("buseAllHis"))
                If CBool(Session("buseAllHis")) Then
                    'jam 2021/04/19
                    cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuuseAllHis").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                Else
                    'jam 2021/04/19
                    cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuuseAllHis").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                End If
            Case Else 'Other Scenarios
                'jrs 2013/01/22
                If IFsRegs.Item(objEA.Item.Caption).Index > 0 Then
                    'jam 2021/04/19
                    'Support.FormManager.Show(Me, "frm_CountryProfile.aspx?Country=" & PrepareForURL(IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code))

                    Vpath = Request.GetFriendlyUrlFileVirtualPath

                    Vext = Request.GetFriendlyUrlFileExtension

                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_CountryProfile.aspx.aspx")


                    vurl = FriendlyUrl.Href("~/frm_CountryProfile.aspx", CountryProfPrepareForFriendlyURL(IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code))

                    'jrs 2024/05/29
                    Session("MMCountry") = IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code

                    Response.Redirect(vurl)


                Else
                    mnuSce_Click(objEA.Item.Caption, objSender)
                End If
        End Select
    End Sub

    Private Sub RefreshLinks()
        Dim i As Integer, sVal As String
        Dim sVar As String, sDim2 As String, sDim3 As String, sCurrVal As String
        Dim VarPos As Integer, Dim2Pos As Integer, Dim3Pos As Integer, EndPos As Integer
        Dim CurrValPos As Integer, CurrValEndPos As Integer
        'jrs 2012/02/17
        Dim CurrValPos30 As Integer, CurrValPos60 As Integer, sVal30 As String, sVal60 As String

        Dim bGroups As Object
        bGroups = False 'no provinces

        Dim X As Object
        X = Split(lblText.Text, "href=")
        lblText.Text = X(0)

        For i = 1 To UBound(X)
            VarPos = InStr(X(i), "Var=")
            Dim2Pos = InStr(X(i), "&Dim2=")
            Dim3Pos = InStr(X(i), "&Dim3=")
            EndPos = InStr(X(i), "'>")
            CurrValPos = InStr(X(i), CStr(Year(Now)) & ": ")
            'jrs 2012/02/17
            CurrValPos30 = InStr(X(i), "2030: ")
            CurrValPos60 = InStr(X(i), "2060: ")
            'CurrValEndPos = InStr(X(i), "</a>")
            If CurrValPos60 > 0 Then
                CurrValEndPos = InStr(CurrValPos60, X(i), "<")
            Else
                CurrValEndPos = 0
            End If

            'jrs 2012/02/17
            If VarPos > 0 And Dim2Pos > VarPos Then
                sVar = Mid(X(i), VarPos + 4, Dim2Pos - VarPos - 4)
            Else
                sVar = ""
            End If
            If Dim2Pos > 0 And Dim3Pos > Dim2Pos Then
                sDim2 = Mid(X(i), Dim2Pos + 6, Dim3Pos - Dim2Pos - 6)
            Else
                sDim2 = ""
            End If
            If Dim3Pos > 0 And EndPos > Dim3Pos Then
                sDim3 = Mid(X(i), Dim3Pos + 6, EndPos - Dim3Pos - 6)
            Else
                sDim3 = ""
            End If
            'jrs 2012/02/17
            If CurrValPos > 0 And CurrValPos30 > CurrValPos Then
                sCurrVal = Mid(X(i), CurrValPos + 6, CurrValPos30 - CurrValPos - 40)
            Else
                sCurrVal = ""
            End If
            If CurrValPos30 > 0 And CurrValPos60 > CurrValPos30 Then
                sVal30 = Mid(X(i), CurrValPos30 + 6, CurrValPos60 - CurrValPos30 - 40)
            Else
                sVal30 = ""
            End If
            If CurrValPos60 > 0 And CurrValEndPos > CurrValPos60 Then
                sVal60 = Mid(X(i), CurrValPos60 + 6, CurrValEndPos - CurrValPos60 - 6)
            Else
                sVal60 = ""
            End If

            If sVar > "" Then
                sVal = LoadVal(sVar, Session("sRegCtrPr"), sDim2, sDim3, bGroups, Year(Now))
                X(i) = Replace(X(i), CStr(Year(Now)) & ": " & sCurrVal, CStr(Year(Now)) & ": " & sVal)
                sVal = LoadVal(sVar, Session("sRegCtrPr"), sDim2, sDim3, bGroups, 2030)
                X(i) = Replace(X(i), "2030: " & sVal30, "2030: " & sVal)
                sVal = LoadVal(sVar, Session("sRegCtrPr"), sDim2, sDim3, bGroups, 2060)
                X(i) = Replace(X(i), "2060: " & sVal60, "2060: " & sVal)
            End If

            'jam 2021/04/30
            'lblText.Text = lblText.Text & "href=" & X(i)

            lblText.Text = lblText.Text & "href=" & Page.ResolveUrl("~/" & X(i))


        Next i
    End Sub

    Private Sub LoadSceMenus(ByRef cswmGroup2 As Coalesys.WebMenu.Group)
        'Read all files with format XXX.run located in runfiles
        Dim sfile As String
        sfile = Dir(Session("PutFdrvRun2") + "\*.run.db")
        Dim cswmItem As Coalesys.WebMenu.Item

        Dim fs 'As New FileSystemObject
        Dim BASE 'As File
        Dim currRun 'As File
        fs = CreateObject("Scripting.FileSystemObject")
        Dim sWorkingFile As String
        sWorkingFile = Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))
        If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBaseHist.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBaseHist.run")
            End If
        Else
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run")
            End If
        End If
        Session("FilesSelected") = 1

        Do While (sfile <> "")
            If UCase(sfile) <> UCase(BASE.Name) And
            UCase(sfile) <> UCase(sWorkingFile) And
            UCase(sfile) <> "WORKING.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") & "\" & sfile)

                If Session("IFsRunFiles").item(sfile).Visible Then
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = Mid(Replace(Replace(sfile, ".run", ""), ".RUN", ""), 1)
                    cswmItem.IconHeight = 16

                    Session("FilesSelected") = Session("FilesSelected") + 1
                    ReDim Preserve Session("RunFiles")(Session("FilesSelected"))
                    Session("RunFiles")(Session("FilesSelected")) = Session("PutFdrvRun2") + "\" + sfile
                    If Session("FilesSelected") > UBound(Session("ShortSce")) Then
                        ReDim Preserve Session("ShortSce")(Session("FilesSelected"))
                    End If
                    Dim k As Integer
                    k = getLongSceIdx(sfile)
                    If k > -1 Then
                        Session("ShortSce")(Session("FilesSelected")) = ShortSce(k)
                    Else
                        Session("ShortSce")(Session("FilesSelected")) = TruncateSceFileName(sfile)
                    End If

                    If Session("iFileCtrPr") = Session("FilesSelected") Then
                        'jam 2021/04/19
                        cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    Else
                        'jam 2021/04/19
                        cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                    End If

                End If
            End If
            sfile = Dir()
        Loop
    End Sub

    Private Sub mnuSce_Click(ByVal sSceName As String, ByRef objSender As Coalesys.WebMenu.WebMenu)
        Dim i As Integer
        With cswmWebMenu.Groups("mnuDisplayOpt").Items("mnuSce").Group
            For i = 0 To .Items.Count - 1
                If .Items(i).Caption = sSceName Then
                    'jam 2021/04/19
                    .Items(i).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    Session("iFileCtrPr") = i
                Else
                    'jam 2021/04/19
                    .Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                End If
            Next
        End With

        RefreshLinks()
    End Sub

    Protected Sub Image1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Image1.Click
        'jrs 2008/07/25
        Dim fname As String
        fname = Image1.ImageUrl.Replace("~/Maps/", "")

        'jam 2026/03/29 Adding automatic dispose for image variable
        'Dim source As New Bitmap(DataFileDir & "Country Maps\" & fname)
        'If Image1.Height.Value > 376 Then
        '    Image1.Height = 376
        '    Image1.Width = source.Width * 376 / source.Height
        'Else
        '    Image1.Height = source.Height
        '    Image1.Width = source.Width
        'End If

        'jam 2026/03/29 Adding automatic dispose for image variable
        If Dir(DataFileDir & "Country Maps\" & fname) > "" Then
            Try


                Using source As New Bitmap(DataFileDir & "Country Maps\" & fname)
                    If Image1.Height.Value > 376 Then
                        Image1.Height = 376
                        Image1.Width = source.Width * 376 / source.Height
                    Else
                        Image1.Height = source.Height
                        Image1.Width = source.Width
                    End If
                End Using
            Catch ex As Exception
                Session("Message") = $"Error loading country map: {fname}"
                Support.FormManager.Show(New Page, "frm_Message.aspx")

            End Try
        End If


    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/04/18
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter">URL Parameters</param>
    ''' <returns>Parameters transformed in a valid URL format</returns>
    Public Function CountryProfPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        CountryProfPrepareForFriendlyURL = tmp
    End Function


End Class

