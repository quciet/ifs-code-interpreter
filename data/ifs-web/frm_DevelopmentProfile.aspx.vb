Imports IFs.Data
'jam 2021/07/30
Imports Microsoft.AspNet.FriendlyUrls

Partial Class frm_DevelopmentProfile
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        'jrs 2005/11/28
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
        End If
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'jam 2021/07/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String



        'jam 2021/07/30
        If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Request.QueryString("Group")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                othCountry = ""

            End If




            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            'jam 2021/07/30
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_DevelopmentProfile.aspx")


            If othCountry <> "" Then
                'jam 2021/07/30
                vurl = FriendlyUrl.Href("~/frm_DevelopmentProfile", DevProfileForFriendlyURL(othCountry))

                Response.Redirect(vurl)


            End If


        End If


        'jam 2021/07/30
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String

        If segments.Count = 1 Then

            vCountry = segments(0)

        Else
            vCountry = segments(0)
            Session("vCountry") = vCountry

        End If

        Session("vCountry") = vCountry

        'jam 2021/07/30
        If Session("IsGroup") = Nothing Then

            Session("IsGroup") = False

        End If





        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            'jrs 2013/12/10
            ClearAllValues()
            If Session("DPCompare") = True Then
                pnlCompare.Visible = True
            End If
            If pnlCompare.Visible Then
                grd_TableDisplay.TotalRows = 63
                grd_TableDisplay.RowCount = 63
            Else
                grd_TableDisplay.TotalRows = 27
                grd_TableDisplay.RowCount = 27
            End If
            'jrs 2005/11/30
            Session("TableDisplayParent") = "frm_DevelopmentProfile.aspx"
            'prepare grid
            'load Std Devs
            LoadStdDevs()
            'Load Regions
            If Session("DevProfGroup") Is Nothing Or Session("DevProfGroup") = False Then
                LoadRegions(cmb_Country)
            Else
                LoadGroups(cmb_Country)
            End If

            'jrs 2013/12/10
            If pnlCompare.Visible Then
                'Load Regions
                If Session("DevProfGroup") Is Nothing Or Session("DevProfGroup") = False Then
                    LoadRegions(cmb_Country_Compare)
                Else
                    LoadGroups(cmb_Country_Compare)
                End If
            End If

            'Load Files
            cboFiles.Items.Clear()
            LoadFiles(gRetrieveFiles(Session("PutFdrvRun2"), Session("WorkingFile"), Session("RunFiles"), Session("FirstYear"), Session("FilesSelected"), Session("ShortSce"), Session("IFsRunFiles")), cboFiles)
            If cboFiles.Items.Count > 0 Then
                If Session("DevcboFilesIdx") Is Nothing Then
                    'jrs 2026/01/26 change default to base
                    cboFiles.SelectedIndex = 1
                Else
                    cboFiles.SelectedIndex = Session("DevcboFilesIdx")
                End If
            End If

            'Reset first year if History plus forecast
            If Session("FirstYear") = FirstYearHistory And UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
            End If

            '   Establish Year List Box
            Dim finam As String
            If Session("mnuSQLite") = "True" Then
                finam = Session("PutFdrvRun") + "Working.run.db"
            Else
                finam = Session("PutFdrvRun") + "Working.run"
            End If

            Dim filNum As Integer, VLN As Long, Number As Single, VarVal As Double
            'cdv 2021/03/15 Fix FNF error
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
                'mti Sep 12, 2011 - start
                Cmb_Year_Compare.Items.Add(LTrim(Str$(i2)))
                'mti Sep 12, 2011 - end

            Next i2

            'Display first elements
            If Cmb_Year.Items.Count > 0 Then
                If Session("DevcmbYearIdx") Is Nothing Then
                    Cmb_Year.SelectedIndex = 0   ' Display First Item
                Else
                    Cmb_Year.SelectedIndex = Session("DevcmbYearIdx")
                End If
                'jrs 2013/12/10
                If Session("DevcmbYearIdx2") Is Nothing Then
                    Cmb_Year_Compare.SelectedIndex = 0   ' Display First Item
                Else
                    Cmb_Year_Compare.SelectedIndex = Session("DevcmbYearIdx2")
                End If
            End If
            ' goes into Procedure summary
            If cmb_Country.Items.Count > 0 Then
                If Session("DevcmbCountryIdx") Is Nothing Then
                    cmb_Country.SelectedIndex = 0   ' Display First Item
                Else
                    cmb_Country.SelectedIndex = Session("DevcmbCountryIdx")
                End If
                'jrs 2013/10/12
                If Session("DevcmbCountryIdx2") Is Nothing Then
                    cmb_Country_Compare.SelectedIndex = 0   ' Display First Item
                Else
                    cmb_Country_Compare.SelectedIndex = Session("DevcmbCountryIdx2")
                End If
            End If

            If Session("DevProfGroup") Is Nothing Then
                Session("DevProfGroup") = False

                'jam 2021/07/30
                Session("IsGroup") = False

            End If


            cboFiles_SelectedIndexChanged(Me, New EventArgs)

            'jrs 2013/11/25
            LoadValuesToCell("World", Val(Cmb_Year.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow"), 2, "POPWORK")
            'jrs 2013/12/10
            If pnlCompare.Visible Then
                LoadValuesToCell("World", Val(Cmb_Year_Compare.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow1"), 2, "POPWORK")
            End If

            Dim myVal As Single
            myVal = grd_TableDisplay(1, Session("DPDisabRow"))
            myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow"))), 4)
            grd_TableDisplay(4, Session("DPDisabRow")) = myVal

            'jrs 2013/12/10
            If pnlCompare.Visible Then
                myVal = grd_TableDisplay(1, Session("DPDisabRow1"))
                myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow1"))), 4)
                grd_TableDisplay(4, Session("DPDisabRow1")) = myVal
            End If
        Else
            StdDevs = Session("StdDevs")
        End If

        'jam 2021/07/30
        'cswmWebMenu.UserData = "jsolorza::976739975"
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        'cswmWebMenu.ScrollEnabled = True

        'jam 2021/07/30
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")
        cswmWebMenu.ScrollEnabled = True

        'jam 2021/07/30
        Dim cswmItem As Coalesys.WebMenu.Item

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
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        'jam 2021/07/30
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"



        cswmGroup1 = cswmWebMenu.Groups.Add()
        If Not Session("DevProfGroup") Then
            cswmGroup1.Caption = "Using Country"
        Else
            cswmGroup1.Caption = "Using Groups"
        End If
        cswmGroup1.ID = "mnuGroups"

        'jam 2021/07/30
        'Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Switch"

        'jrs 2013/12/02
        cswmGroup1 = cswmWebMenu.Groups.Add()
        If Not Session("DevProfCompare") Then
            cswmGroup1.Caption = "Compare Country"
        Else
            cswmGroup1.Caption = "Compare Groups"
        End If
        cswmGroup1.ID = "mnuCompare"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Hide"

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/03 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16007);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jrs 2011/05/06
        'jam 2021/07/30
        'If Not IsNothing(Request.QueryString("Country")) Then
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(vCountry)) And Not Session("DevProfGroup") Then
            'jam 2021/07/30
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> vCountry Or Not Page.IsPostBack Then
                'jam 2021/07/30
                'Session("MMCountry") = Request.QueryString("Country")

                Session("MMCountry") = vCountry
                'jrs 2012/08/15 Session("DevProfGroup") = False
                If Session("DevProfGroup") = True Then
                    cswmWebMenu.Groups("mnuGroups").Caption = "Using Countries/&Regions"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions(cmb_Country)
                    Session("DevProfGroup") = False

                    'jam 2021/07/30
                    Session("IsGroup") = False

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
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(vCountry)) And Session("DevProfGroup") Then
            'jam 2021/07/30
            'If cmb_Country.SelectedValue <> Request.QueryString("Group") Then
            If cmb_Country.SelectedValue <> vCountry Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/07/30
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(vCountry, False, Me)
                If gIdx > 0 Then
                    cmb_Country.SelectedIndex = gIdx - 1
                    Session("DevcmbCountryIdx") = cmb_Country.SelectedIndex
                    'jam 2021/07/30
                    'Session("MMGroup") = Request.QueryString("Group")
                    Session("MMGroup") = vCountry
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("DevProfGroup") = True Then
                    cmb_Country_SelectedIndexChanged(cmb_Country, New System.EventArgs)
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
            Session("DevcmbCountryIdx") = cmb_Country.SelectedIndex
            cmb_Country_SelectedIndexChanged(Me, New System.EventArgs)
        End If

    End Sub

    'jrs 2005/03/28
    'In order to add a new row you have to add a new entry to the array
    'of Standard deviations, the index of this last entry should be used
    'in LoadVar subroutine to add the new row
    '
    'To change current rows verify:
    '1) LoadValues
    '2) PctofGDP
    '3) DisplayOverTime
    '4) DisplayGraph
    '5) AggregateValues
    '************************************************************
    Dim GDPPCP As Single

    'Indices for aggregation calc.
    Public POPval As Single
    Public GDPval As Single
    'grouping
    Dim CountryGroup(200) As String
    Dim GroupTot As Integer
    'currency
    Dim bCnvrtCurr As Boolean 'True if Currency Conversion is ON
    Dim CurrencyFct As Single 'Stores the Currency Factor for Conversion
    'Declares and opens a RecordSet with configuration values
    Dim rsCurr As DataTable
    'Array of Std Devs
    Dim StdDevs(22) As Single 'jrs 2018/09/14 from 20 to 22 'jrs 2013/11/28 from 17 to 20
    Dim oConn As ADODB.Connection
    'jrs 2005/04/04
    Public sDevProFun As String
    Public iDevProFunIdx As Integer
    Public FunVal As Single
    'jrs 2005/11/18
    Dim gridRow As Integer

    Private Sub LoadRegions(CmbBx As DropDownList)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        CmbBx.Items.Clear()
        Dim EleLoop As Integer
        For EleLoop = 1 To MaxVal(1) - 1 'Exclude 'ALL'
            CmbBx.Items.Add(GRPS$(1, EleLoop))
        Next EleLoop
    End Sub

    Private Sub LoadFiles(ByVal FileList() As String, ByVal cbo As DropDownList)
        Dim i As Integer
        For i = 0 To UBound(FileList)
            cbo.Items.Add(FileList(i))
        Next i
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
            Case "Show"
                mnuShowComparison_Click()
            Case "Hide"
                mnuHideComparison_Click()
            Case "Continue" 'jam 2021/07/30
                mnuContinue_Click(sender, e)
        End Select
    End Sub

    Private Sub cboFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiles.SelectedIndexChanged
        'jrs 2005/05/16
        If Not cboFiles.Visible Then Exit Sub

        grd_TableDisplay.Visible = False
        If Cmb_Year.SelectedIndex = -1 Then Exit Sub
        'jrs 2005/11/30
        Session("DevcboFilesIdx") = cboFiles.SelectedIndex

        ClearValues()
        'mti Aug 15, 2011
        If pnlCompare.Visible Then
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text), cmb_Country_Compare.SelectedIndex + 1, Val(Cmb_Year_Compare.Text))
        Else
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        End If
        'mti Aug 15, 2011 -end


        'jrs 2013/11/25
        LoadValuesToCell("World", Val(Cmb_Year.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow"), 2, "POPWORK")
        'jrs 2013/12/10
        If pnlCompare.Visible Then
            LoadValuesToCell("World", Val(Cmb_Year_Compare.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow1"), 2, "POPWORK")
        End If

        Dim myVal As Single
        myVal = grd_TableDisplay(1, Session("DPDisabRow"))
        myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow"))), 4)
        grd_TableDisplay(4, Session("DPDisabRow")) = myVal

        'jrs 2013/12/10
        If pnlCompare.Visible Then
            myVal = grd_TableDisplay(1, Session("DPDisabRow1"))
            myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow1"))), 4)
            grd_TableDisplay(4, Session("DPDisabRow1")) = myVal
        End If

        'Pct of GDP
        PctofGDP()
        'jrs 2013/11/25
        'Pct of POPWORK
        PctofPOPWORK()
        'Std Dev calc
        StdDevCalc()
        'ROUND
        RoundValues()
        grd_TableDisplay.Visible = True
    End Sub

    Private Sub ClearValues()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim i As Integer, j As Integer
        For i = 0 To grd_TableDisplay.TotalRows - 1
            For j = 1 To 6
                'jrs 2013/12/03
                'If grd_TableDisplay(j, i) > "" Then
                '    grd_TableDisplay(j, i) = "0"
                'End If
                'jrs 2013/12/10
                If pnlCompare.Visible Then
                    If (i <> Session("DPDisabRow") And i <> Session("DPDisabRow1")) Or j <> 2 Then
                        grd_TableDisplay(j, i) = ""
                    End If
                Else
                    If i <> Session("DPDisabRow") Or j <> 2 Then
                        grd_TableDisplay(j, i) = ""
                    End If
                End If
            Next j
        Next i
        'JRS 09/18/02 AGGREGATION
        POPval = 0
        GDPval = 0
    End Sub

    'jrs 2013/12/03
    Private Sub ClearAllValues()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim i As Integer, j As Integer
        For i = 0 To grd_TableDisplay.TotalRows - 1
            For j = 0 To 6
                grd_TableDisplay(j, i) = ""
            Next j
        Next i
    End Sub

    'jrs 2013/11/28 addedd 2 optional parameters RegCompare and YrCompare
    'Subroutine to Load the Values
    'into Development Profile Values table
    Private Sub LoadValues(ByVal reg As Integer, ByVal yr As Integer, Optional RegCompare As Integer = 9999, Optional YrCompare As Integer = 9999)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'Load IV
        Dim GDPPCPs(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("GDPPCP", -1, cmb_Country.SelectedValue, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, GDPPCPs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGDPPCP" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("GDPPCP", -1, cmb_Country.SelectedValue, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, GDPPCPs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        GDPPCP = GDPPCPs(1)
        Session("DPGDPPCP") = GDPPCP
        'jrs 2013/11/26
        Dim EDYRS15s(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("EDYRSAG15", -1, cmb_Country.Text, reg, "Total", 3, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, EDYRS15s, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfEDYRSAG15" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("EDYRSAG15", -1, cmb_Country.Text, reg, "Total", 3, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, EDYRS15s, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
        End If
        Session("DPEDYRS15") = EDYRS15s(1)
        Dim DEMOCPOLITYs(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("DEMOCPOLITY", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, DEMOCPOLITYs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfDEMOCPOLITY" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("DEMOCPOLITY", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, DEMOCPOLITYs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        Session("DPDEMOCPOLITY") = DEMOCPOLITYs(1)
        Dim ENXs(1) As Single, ENPRIs(1) As Single, GDPs(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("ENX", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, ENXs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfENX" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("ENX", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, ENXs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("ENPRI", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, ENPRIs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfENPRI" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("ENPRI", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, ENPRIs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("GDP", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, GDPs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGDP" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("GDP", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, GDPs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        Session("DPEnerDepend") = ENXs(1) * ENPRIs(1) / GDPs(1)
        Dim GEMs(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("GEM", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, GEMs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGEM" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("GEM", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, GEMs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        Session("DPGEM") = GEMs(1)
        Dim YTHBULGEs(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("YTHBULGE", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, YTHBULGEs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfYTHBULGE" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("YTHBULGE", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, YTHBULGEs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        Session("DPYTHBULGE") = YTHBULGEs(1)

        If RegCompare <> 9999 Then
            Dim GDPPCPs1(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("GDPPCP", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, GDPPCPs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGDPPCP" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("GDPPCP", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, GDPPCPs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            Session("DPGDPPCP1") = GDPPCPs1(1)
            'jrs 2013/11/26
            Dim EDYRS15s1(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("EDYRSAG15", -1, cmb_Country_Compare.Text, RegCompare, "Total", 3, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, EDYRS15s1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfEDYRSAG15" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("EDYRSAG15", -1, cmb_Country_Compare.Text, RegCompare, "Total", 3, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, EDYRS15s1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            Session("DPEDYRS151") = EDYRS15s1(1)
            Dim DEMOCPOLITYs1(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("DEMOCPOLITY", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, DEMOCPOLITYs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfDEMOCPOLITY" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("DEMOCPOLITY", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, DEMOCPOLITYs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            Session("DPDEMOCPOLITY1") = DEMOCPOLITYs1(1)
            Dim ENXs1(1) As Single, ENPRIs1(1) As Single, GDPs1(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("ENX", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, ENXs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfENX" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("ENX", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, ENXs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("ENPRI", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, ENPRIs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfENPRI" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("ENPRI", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, ENPRIs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("GDP", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, GDPs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGDP" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("GDP", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, GDPs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            Session("DPEnerDepend1") = ENXs1(1) * ENPRIs1(1) / GDPs1(1)
            Dim GEMs1(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("GEM", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, GEMs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGEM" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("GEM", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, GEMs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            Session("DPGEM1") = GEMs1(1)
            Dim YTHBULGEs1(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("YTHBULGE", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, YTHBULGEs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfYTHBULGE" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("YTHBULGE", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, YTHBULGEs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            Session("DPYTHBULGE1") = YTHBULGEs1(1)
        End If

        'mti July 18, 2011 - start
        Dim VarVals(1) As Single, VarIdx As Integer, InfraSwitch As Integer
        'jrs 2017/06/06 
        'VarIdx = IfsVars.Item("inframfpsw").Index

        'If VarIdx = -1 Then Exit Sub

        'gLoadForDisplay("inframfpsw", VarIdx, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, _
        'Session("DevProfGroup"), 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), _
        'Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), _
        'Session("CurrencyVal"))
        'InfraSwitch = CInt(VarVals(1))
        'mti July 18, 2011 - end
        InfraSwitch = 3

        'mti 2011/09/12 *** begin ***
        Dim msg As String
        msg = "GDP Per Capita (Thou. Real PPP$)<br>"
        msg$ = msg$ + cmb_Country.Text + " (" + Cmb_Year.Text + "): " + Format(GDPPCP, "0.000")
        If RegCompare <> 9999 Then
            msg$ = msg$ + "<br>" + cmb_Country_Compare.Text + " (" + Cmb_Year_Compare.Text + "): " + Format(Session("DPGDPPCP1"), "0.000")
        End If
        grd_TableDisplay.Columns(0).title = msg
        'mti 2011/09/12 *** end ***

        'jrs 2005/05/13
        Dim curRow As Integer
        curRow = 0
        'Load Rows
        With grd_TableDisplay
            gridRow = curRow '1
            Session("DPHCRow") = curRow
            LoadValuesVar(reg, yr, "MFPHC", -1, "", "Human Capital", -1, "", "")

            'mti Aug 15, 2011 - start
            If RegCompare <> 9999 Then
                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPHCRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "MFPHC", -1, "", "Human Capital", -1, "", "", True)
                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPHCRow1") = Nothing
            End If
            'mti Aug 15, 2011 -  end

            curRow = curRow + 1
            gridRow = curRow


            'jrs 2013/11/28 LoadValuesVar(reg, yr, "EDYRSAG25", 0, "GDP/Capita (PPP) Versus Years of Educ at 25+ (2000)", "Years of Education", GDPPCP, "Total", "")
            LoadValuesVar(reg, yr, "EDYRSAG15", 0, "GDP/Capita (PPP 2005) Versus Years of Educ at 15+ (2010) Log", "Years of Education", GDPPCP, "Total", "")
            LoadValuesVar(reg, yr, "mfpedyrs", -6, "", "Years of Education", -1, "Total", "")

            'mti Aug 15, 2011 - start
            Session("DPEdYrsRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPEdYrsRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "EDYRSAG15", 0, "GDP/Capita (PPP 2005) Versus Years of Educ at 15+ (2010) Log", "Years of Education", Session("DPGDPPCP1"), "Total", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpedyrs", -6, "", "Years of Education", -1, "Total", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPEdYrsRow1") = Nothing
            End If
            'mti Aug 15, 2011 - end

            '        curRow = curRow + 1
            '        gridRow = curRow
            '        LoadValuesVar reg, yr, "GDS", 1, "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - linear", "Education Expenditure (Linear)", GDPPCP, "Education"
            curRow = curRow + 1
            gridRow = curRow
            'jrs 2024/11/08 LoadValuesVar(reg, yr, "GDS", 2, "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - log", "Education Expenditure (Log)", GDPPCP, "Education", "")
            LoadValuesVar(reg, yr, "GDS", 2, "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) Log", "Education Expenditure (Log)", GDPPCP, "Education", "")
            LoadValuesVar(reg, yr, "mfpedspn", -6, "", "Education Expenditure (Log)", -1, "Education", "")

            'mti Aug 15, 2011 - start
            Session("DPEdnSpndRow") = curRow
            If RegCompare <> 9999 Then
                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPEdnSpndRow1") = curRow
                'jrs 2024/11/08 LoadValuesVar(RegCompare, YrCompare, "GDS", 2, "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - log", "Education Expenditure (Log)", Session("DPGDPPCP1"), "Education", "", True)
                LoadValuesVar(RegCompare, YrCompare, "GDS", 2, "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) Log", "Education Expenditure (Log)", Session("DPGDPPCP1"), "Education", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpedspn", -6, "", "Education Expenditure (Log)", -1, "Education", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPEdnSpndRow1") = Nothing
            End If
            'mti Aug 15, 2011 - end

            curRow = curRow + 1
            gridRow = curRow
            LoadValuesVar(reg, yr, "LIFEXP", 3, "GDP/Capita (PPP) Versus Life Expectancy (2000)", "Life Expectancy", GDPPCP, "Total", "")
            'jrs 2019/08/22 LoadValuesVar(reg, yr, "mfpedlifexp", -6, "", "Life Expectancy", -1, "", "")
            LoadValuesVar(reg, yr, "mfplife", -6, "", "Life Expectancy", -1, "", "")

            'jrs 2013/11/25 *** begin ***
            VarVals(1) = grd_TableDisplay(6, curRow)
            VarVals(1) = Math.Round(VarVals(1) * Val(grd_TableDisplay(6, Session("DPEdYrsRow"))), 4)
            grd_TableDisplay(6, curRow) = VarVals(1)
            'jrs 2013/11/25 *** end ***

            'mti Aug 15, 2011 - start
            Session("DPLERow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPLERow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "LIFEXP", 3, "GDP/Capita (PPP) Versus Life Expectancy (2000)", "Life Expentancy", Session("DPGDPPCP1"), "Total", "", True)
                'jrs 2019/08/22 LoadValuesVar(RegCompare, YrCompare, "mfpedlifexp", -6, "", "Life Expentancy", -1, "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfplife", -6, "", "Life Expentancy", -1, "", "", True)
                'jrs 2013/11/25 *** begin ***
                VarVals(1) = grd_TableDisplay(6, curRow)
                VarVals(1) = Math.Round(VarVals(1) * Val(grd_TableDisplay(6, Session("DPEdYrsRow1"))), 4)
                grd_TableDisplay(6, curRow) = VarVals(1)
                'jrs 2013/11/25 *** end ***

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPLERow1") = Nothing
            End If

            '        curRow = curRow + 1
            '        gridRow = curRow
            '        LoadValuesVar reg, yr, "GDS", 4, "GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (2000) - linear", "Health Expenditure (Linear)", GDPPCP, "Health"
            curRow = curRow + 1
            gridRow = curRow
            'jrs 2013/11/29
            'LoadValuesVar(reg, yr, "GDS", 5, "GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (2000) - log", "Health Expenditure (Log)", GDPPCP, "Health", "")
            'LoadValuesVar(reg, yr, "mfphlspn", -6, "", "Health Expenditure (Log)", -1, "Health", "")
            LoadValuesVar(reg, yr, "HLSTUNT", 19, "GDP/Capita (PPP 2005) Versus Stunting (2006) Log", "Stunting (Log)", GDPPCP, "", "")
            LoadValuesVar(reg, yr, "mfpstunt", -6, "", "Stunting (Log)", -1, "Health", "")

            Session("DPStuntRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPStuntRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "HLSTUNT", 19, "GDP/Capita (PPP 2005) Versus Stunting (2006) Log", "Stunting (Log)", Session("DPGDPPCP1"), "Health", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpstunt", -6, "", "Stunting (Log)", -1, "Health", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPStuntRow1") = Nothing
            End If

            curRow = curRow + 1
            gridRow = curRow
            LoadValuesVar(reg, yr, "HLYLDWORK", 0, "", "Disability", -1, "Total", "Total")
            LoadValuesVar(reg, yr, "mfphlyld", -6, "", "Disability", -1, "Health", "")

            Session("DPDisabRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text


                curRow = curRow + 1
                gridRow = curRow
                Session("DPDisabRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "HLYLDWORK", 0, "", "Disability", -1, "Total", "Total", True)
                LoadValuesVar(RegCompare, YrCompare, "mfphlyld", -6, "", "Disability", -1, "Health", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPDisabRow1") = Nothing
            End If
            'jrs 2013/11/25 *** end ***

            'jrs 2018/09/14 *** begin ***
            curRow = curRow + 1
            gridRow = curRow

            LoadValuesVar(reg, yr, "EDSECLOWRVOC", 0, "", "Lower Vocational", -1, "Total", "")
            grd_TableDisplay(3, curRow) = 0
            grd_TableDisplay(4, curRow) = 0
            LoadValuesVar(reg, yr, "mfpedvoc", -6, "", "Lower Vocational", -1, "Total", "")

            Session("DPEdLowVocRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPEdLowVocRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "EDSECLOWRVOC", 0, "", "Lower Vocational", -1, "Total", "", True)
                grd_TableDisplay(3, curRow) = 0
                grd_TableDisplay(4, curRow) = 0
                LoadValuesVar(RegCompare, YrCompare, "mfpedvoc", -6, "", "Lower Vocational", -1, "Total", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPEdLowVocRow1") = Nothing
            End If

            curRow = curRow + 1
            gridRow = curRow

            LoadValuesVar(reg, yr, "EDSECUPPRVOC", 0, "", "Upper Vocational", -1, "Total", "")
            grd_TableDisplay(3, curRow) = 0
            grd_TableDisplay(4, curRow) = 0
            LoadValuesVar(reg, yr, "mfpedvoc", -6, "", "Upper Vocational", -1, "Total", "")

            Session("DPEdUpVocRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPEdUpVocRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "EDSECUPPRVOC", 0, "", "Upper Vocational", -1, "Total", "", True)
                grd_TableDisplay(3, curRow) = 0
                grd_TableDisplay(4, curRow) = 0
                LoadValuesVar(RegCompare, YrCompare, "mfpedvoc", -6, "", "Upper Vocational", -1, "Total", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPEdUpVocRow1") = Nothing
            End If

            curRow = curRow + 1
            gridRow = curRow

            LoadValuesVar(reg, yr, "EDQUALAG15", 21, "GDP/Capita (PPP 2011) Versus Quality of Educ at 15+ (2015) Log", "Education Quality", GDPPCP, "Total", "")
            LoadValuesVar(reg, yr, "mfpedqual", -6, "", "Education Quality", -1, "Total", "")

            Session("DPEdQualRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPEdQualRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "EDQUALAG15", 21, "GDP/Capita (PPP 2011) Versus Quality of Educ at 15+ (2015) Log", "Education Quality", Session("DPGDPPCP1"), "Total", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpedqual", -6, "", "Education Quality", -1, "Total", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPEdQualRow1") = Nothing
            End If
            'jrs 2018/09/14 *** end ***

            curRow = curRow + 2 '7
            gridRow = curRow
            Session("DPSCRow") = curRow
            LoadValuesVar(reg, yr, "MFPSC", -1, "", "Social Capital", -1, "", "")

            'mti Aug 15, 2011 - start
            If RegCompare <> 9999 Then
                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPSCRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "MFPSC", -1, "", "Social Capital", -1, "", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPSCRow1") = Nothing
            End If
            'mti Aug 15, 2011 -  end

            curRow = curRow + 1
            gridRow = curRow

            'jrs 2006/02/01 LoadValuesVar(reg, yr, "FREEDOM", 6, "GDP/Capita (PPP) Versus Freedom (2000)", "Freedom", GDPPCP, "")
            'jrs 2013/11/29 LoadValuesVar(reg, yr, "FREEDOM", 6, "GDP/Capita (PPP) Versus Freedom -Logged and Inverted (2005)", "Freedom", GDPPCP, "", "")
            LoadValuesVar(reg, yr, "FREEDOM", 0, "", "Freedom", 0, "", "")
            LoadValuesVar(reg, yr, "mfpfree", -6, "", "Freedom", -1, "", "")

            Call AnalFunc("Freedom (Inverted) with GDPPCP and EdYears 15 (2012)",
                New Single() {GDPPCP, Session("DPEDYRS15")}, VarVals(1), Session("AnalTbl"))
            grd_TableDisplay(2, curRow) = Math.Round(AMAX(1, Amin(VarVals(1), 14)), 4)
            grd_TableDisplay(3, curRow) = StdDevs(6)
            grd_TableDisplay(4, curRow) = StdDevs(6)

            'mti Aug 15, 2011 - start
            Session("DPFRDMRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text


                curRow = curRow + 1
                gridRow = curRow
                Session("DPFRDMRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "FREEDOM", 0, "", "Freedom", 0, "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpfree", -6, "", "Freedom", -1, "", "", True)

                Call AnalFunc("Freedom (Inverted) with GDPPCP and EdYears 15 (2012)",
                    New Single() {Session("DPGDPPCP1"), Session("DPEDYRS151")}, VarVals(1), Session("AnalTbl"))
                grd_TableDisplay(2, curRow) = Math.Round(VarVals(1), 4)
                grd_TableDisplay(3, curRow) = StdDevs(6)
                grd_TableDisplay(4, curRow) = StdDevs(6)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPFRDMRow1") = Nothing
            End If
            'mti Aug 15, 2011 - end

            curRow = curRow + 1
            gridRow = curRow
            'JRS 2013/11/29 LoadValuesVar(reg, yr, "GOVEFFECT", 7, "GDP/Capita (PPP 2000) Versus Governance Effectiveness (2002) Linear", "Governance Effectiveness (Linear)", GDPPCP, "", "")
            LoadValuesVar(reg, yr, "GOVEFFECT", 0, "", "Governance Effectiveness", 0, "", "")
            LoadValuesVar(reg, yr, "mfpgoveff", -6, "", "Governance Effectiveness", -1, "", "")
            Call AnalFunc("Governance Effectiveness (2005)",
                New Single() {GDPPCP, Session("DPDEMOCPOLITY"), Session("DPEnerDepend")}, VarVals(1), Session("AnalTbl"))
            grd_TableDisplay(2, curRow) = Math.Round(AMAX(0, Amin(5, VarVals(1))), 4)
            grd_TableDisplay(3, curRow) = StdDevs(7)
            grd_TableDisplay(4, curRow) = StdDevs(7)

            'mti Aug 15, 2011 - start
            Session("DPGOVEFFLinRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text


                curRow = curRow + 1
                gridRow = curRow
                Session("DPGOVEFFLinRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "GOVEFFECT", 0, "", "Governance Effectiveness", 0, "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpgoveff", -6, "", "Governance Effectiveness", -1, "", "", True)

                'jrs 2013/11/26
                Call AnalFunc("Governance Effectiveness (2005)",
                        New Single() {Session("DPGDPPCP1"), Session("DPDEMOCPOLITY1"), Session("DPEnerDepend1")}, VarVals(1), Session("AnalTbl"))
                grd_TableDisplay(2, curRow) = Math.Round(AMAX(0, Amin(5, VarVals(1))), 4)
                grd_TableDisplay(3, curRow) = StdDevs(7)
                grd_TableDisplay(4, curRow) = StdDevs(7)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPGOVEFFLinRow1") = Nothing
            End If
            'mti Aug 15, 2011 - end

            curRow = curRow + 1
            gridRow = curRow
            'JRS 2013/11/29
            'LoadValuesVar(reg, yr, "GOVEFFECT", 8, "GDP/Capita (PPP 2000) Versus Governance Effectiveness (2002) Log", "Governance Effectiveness (Log)", GDPPCP, "", "")
            'LoadValuesVar(reg, yr, "mfpgoveff", -6, "", "Governance Effectiveness (Log)", -1, "", "")
            'curRow = curRow + 1
            'gridRow = curRow
            'JRS 2013/11/29 LoadValuesVar(reg, yr, "GOVCORRUPT", 9, "GDP/Capita (PPP 2000) Versus TI Corruption Perception (2000) Linear", "Corruption Perception", GDPPCP, "", "")
            LoadValuesVar(reg, yr, "GOVCORRUPT", 0, "", "Corruption Perception", 0, "", "")
            LoadValuesVar(reg, yr, "mfpgovcor", -6, "", "Corruption Perception", -1, "", "")
            Call AnalFunc("GOVCORRUPTversus GDPPCP, DEMOCPOLITY, GEM, Ex Export Depend (2010)",
                New Single() {GDPPCP, Session("DPDEMOCPOLITY"), Session("DPGEM"), Session("DPEnerDepend")}, VarVals(1), Session("AnalTbl"))
            grd_TableDisplay(2, curRow) = Math.Round(AMAX(1, Amin(10, VarVals(1))), 4)
            grd_TableDisplay(3, curRow) = StdDevs(9)
            grd_TableDisplay(4, curRow) = StdDevs(9)

            'mti Aug 15, 2011 - start
            Session("DPCORRow") = curRow
            If RegCompare <> 9999 Then
                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text


                curRow = curRow + 1
                gridRow = curRow
                Session("DPCORRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "GOVCORRUPT", 0, "", "Corruption Perception", 0, "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpgovcor", -6, "", "Corruption Perception", -1, "", "", True)

                Call AnalFunc("GOVCORRUPTversus GDPPCP, DEMOCPOLITY, GEM, Ex Export Depend (2010)",
                        New Single() {GDPPCP, Session("DPDEMOCPOLITY"), Session("DPGEM"), Session("DPEnerDepend")}, VarVals(1), Session("AnalTbl"))
                grd_TableDisplay(2, curRow) = Math.Round(AMAX(1, Amin(10, VarVals(1))), 4)
                grd_TableDisplay(3, curRow) = StdDevs(9)
                grd_TableDisplay(4, curRow) = StdDevs(9)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPCORRow1") = Nothing
            End If
            'mti Aug 15, 2011 - end

            curRow = curRow + 1
            gridRow = curRow
            'JRS 2013/11/29 LoadValuesVar(reg, yr, "ECONFREE", 10, "GDP/Capita (PPP) Versus Economic Freedom (2000) Log", "Economic Freedom (Log)", GDPPCP, "", "")
            LoadValuesVar(reg, yr, "ECONFREE", 0, "", "Economic Freedom", 0, "", "")
            LoadValuesVar(reg, yr, "mfpefree", -6, "", "Economic Freedom", -1, "", "")

            Call AnalFunc("Economic Freedom (2010)",
                    New Single() {GDPPCP, Session("DPDEMOCPOLITY"), Session("DPEnerDepend")}, VarVals(1), Session("AnalTbl"))
            grd_TableDisplay(2, curRow) = Math.Round(AMAX(1, Amin(10, VarVals(1))), 4)
            grd_TableDisplay(3, curRow) = StdDevs(10)
            grd_TableDisplay(4, curRow) = StdDevs(10)

            'mti Aug 15, 2011 - start
            Session("DPEconFRDMRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPEconFRDMRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "ECONFREE", 0, "", "Economic Freedom", 0, "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpefree", -6, "", "Economic Freedom", -1, "", "", True)

                Call AnalFunc("Economic Freedom (2010)",
                        New Single() {Session("DPGDPPCP1"), Session("DPDEMOCPOLITY1"), Session("DPEnerDepend1")}, VarVals(1), Session("AnalTbl"))
                grd_TableDisplay(2, curRow) = Math.Round(AMAX(1, Amin(10, VarVals(1))), 4)
                grd_TableDisplay(3, curRow) = StdDevs(10)
                grd_TableDisplay(4, curRow) = StdDevs(10)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPEconFRDMRow1") = Nothing
            End If
            'mti Aug 15, 2011 - end


            'jrs 2013/11/26 *** begin ***
            curRow = curRow + 1
            gridRow = curRow
            LoadValuesVar(reg, yr, "DEMOCPOLITY", 0, "", "Democracy", 0, "", "")
            LoadValuesVar(reg, yr, "mfpdemoc", -6, "", "Democracy", -1, "", "")

            Call AnalFunc("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                    New Single() {Session("DPGEM"), Session("DPYTHBULGE"), Session("DPEnerDepend")}, VarVals(1), Session("AnalTbl"))
            grd_TableDisplay(2, curRow) = Math.Round(AMAX(1, Amin(20, VarVals(1))), 4)
            grd_TableDisplay(3, curRow) = StdDevs(11)
            grd_TableDisplay(4, curRow) = StdDevs(11)

            Session("DPDemocRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPDemocRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "DEMOCPOLITY", 0, "", "Democracy", 0, "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpdemoc", -6, "", "Democracy", -1, "", "", True)

                Call AnalFunc("Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)",
                        New Single() {Session("DPGEM1"), Session("DPYTHBULGE1"), Session("DPEnerDepend1")}, VarVals(1), Session("AnalTbl"))
                grd_TableDisplay(2, curRow) = Math.Round(AMAX(1, Amin(20, VarVals(1))), 4)
                grd_TableDisplay(3, curRow) = StdDevs(11)
                grd_TableDisplay(4, curRow) = StdDevs(11)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPDemocRow1") = Nothing
            End If
            'jrs 2013/11/26 *** end ***
            '        curRow = curRow + 1
            '        gridRow = curRow
            '        LoadValuesVar reg, yr, "ECONFREE", 11, "GDP/Capita (PPP) Versus Economic Freedom (2000) Linear", "Economic Freedom (Linear)", GDPPCP, ""
            '        LoadValuesVar reg, yr, "mfpimprat", -6, "", "Import Rate", -1, ""

            curRow = curRow + 2 '14
            gridRow = curRow
            Session("DPPCRow") = curRow
            LoadValuesVar(reg, yr, "MFPPC", -1, "", "Physical Capital", -1, "", "")
            'mti Aug 15, 2011 - start
            If RegCompare <> 9999 Then
                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPPCRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "MFPPC", -1, "", "Physical Capital", -1, "", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPPCRow1") = Nothing
            End If
            'mti Aug 15, 2011 -  end

            'mti July 18, 2011 - start
            If InfraSwitch = 0 Or InfraSwitch = 2 Then

                curRow = curRow + 1
                gridRow = curRow
                LoadValuesVar(reg, yr, "INFRAROAD", 12, "GDP/Capita (PPP) Versus Road Network/Land Area (2000)", "Road Ntwk/Land Area", GDPPCP, "", "")
                LoadValuesVar(reg, yr, "mfpinfrrd", -6, "", "Road Ntwk/Land Area", -1, "", "")

                'mti Aug 15, 2011 - start
                Session("DPRdRow") = curRow
                If RegCompare <> 9999 Then

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                    curRow = curRow + 1


                    gridRow = curRow
                    Session("DPRdRow1") = curRow
                    LoadValuesVar(RegCompare, YrCompare, "INFRAROAD", 12, "GDP/Capita (PPP) Versus Road Network/Land Area (2000)", "Road Ntwk/Land Area", Session("DPGDPPCP1"), "", "", True)
                    LoadValuesVar(RegCompare, YrCompare, "mfpinfrrd", -6, "", "Road Ntwk/Land Area", -1, "", "", True)

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                    curRow = curRow + 1
                Else
                    Session("DPRdRow1") = Nothing
                End If
                'mti Aug 15, 2011 - end


                curRow = curRow + 1
                gridRow = curRow
                Session("DPElecConsRow") = curRow
                'jrs 2009/11/10 changed from 2000 to 2001 version
                'jrs 2011/01/21 LoadValuesVar reg, yr, "INFRAELEC", 13, "GDP/Capita (PPP 2000) Versus Kilowatt-Hours per Capita (2001) Linear", "Kilowatt-hours per capita", GDPPCP, "", ""
                LoadValuesVar(reg, yr, "INFRAELEC", 13, "GDP/Capita (PPP 2000) Versus Kilowatt-Hours per Capita (2000) Linear", "Kilowatt-hours per capita", GDPPCP, "", "")
                LoadValuesVar(reg, yr, "mfpinfrel", -6, "", "Kilowatt-hours per capita", -1, "", "")

                'mti Aug 15, 2011 - start
                If RegCompare <> 9999 Then

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                    curRow = curRow + 1
                    gridRow = curRow
                    Session("DPElecConsRow1") = curRow
                    LoadValuesVar(RegCompare, YrCompare, "INFRAELEC", 13, "GDP/Capita (PPP 2000) Versus Kilowatt-Hours per Capita (2000) Linear", "Kilowatt-hours per capita", Session("DPGDPPCP1"), "", "", True)
                    LoadValuesVar(RegCompare, YrCompare, "mfpinfrel", -6, "", "Kilowatt-hours per capita", -1, "", "", True)
                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                    curRow = curRow + 1
                Else
                    Session("DPElecConsRow1") = Nothing
                End If
                'mti Aug 15, 2011 - end
            End If
            'mti July 18, 2011 - end

            'mti July 18, 2011 - start
            If InfraSwitch = 0 Or InfraSwitch = 1 Then

                curRow = curRow + 1
                gridRow = curRow
                'mti June 2011 -start
                'LoadValuesVar reg, yr, "INFRATELE", 14, "GDP/Capita (PPP) Versus Telephones per 1000 (2000)", "Telephones per 1000", GDPPCP, "", ""

                'mti Sep 12, 2011 - start
                'LoadValuesVar reg, yr, "INFRATELEOLD", 14, "GDP/Capita (PPP) Versus Telephones per 1000 (2000)", "Telephones per 1000", GDPPCP, "", ""
                LoadValuesVar(reg, yr, "INFRATELEOLD", 14, "GDP/Capita (PPP) Versus Telephones per 1000 (2000)", "Telephones per 1000", Amin(GDPPCP, 40), "", "")
                'mti June 2011 - end
                'LoadValuesVar reg, yr, "mfpinfrate", -6, "", "Telephones per 1000", -1, "", ""
                LoadValuesVar(reg, yr, "mfpinfrte", -6, "", "Telephones per 1000", -1, "", "")
                'mti Sep 12, 2011 - end



                'mti Aug 15, 2011 - start
                Session("DPTeleOldRow") = curRow
                If RegCompare <> 9999 Then

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                    curRow = curRow + 1
                    gridRow = curRow
                    Session("DPTeleOldRow1") = curRow
                    'mti Sep 12, 2011 - start
                    '                LoadValuesVar RegCompare, yr, "INFRATELEOLD", 14, "GDP/Capita (PPP) Versus Telephones per 1000 (2000)", "Telephones per 1000", GDPPCP1, "", "", True
                    '                LoadValuesVar RegCompare, yr, "mfpinfrate", -6, "", "Telephones per 1000", -1, "", "", True

                    LoadValuesVar(RegCompare, YrCompare, "INFRATELEOLD", 14, "GDP/Capita (PPP) Versus Telephones per 1000 (2000)", "Telephones per 1000", Amin(Session("DPGDPPCP1"), 40), "", "", True)
                    LoadValuesVar(RegCompare, YrCompare, "mfpinfrte", -6, "", "Telephones per 1000", -1, "", "", True)

                    'mti Sep 12, 2011 - end
                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                    curRow = curRow + 1

                Else
                    Session("DPTeleOldRow1") = Nothing
                End If
                'mti Aug 15, 2011 - end


                curRow = curRow + 1
                gridRow = curRow
                'jrs 2009/11/10 changed from 2000 to 2007
                LoadValuesVar(reg, yr, "INFRANET", 15, "GDP/Capita (PPP) Versus Internet Percent Use (2007)", "Internet Percent Use", GDPPCP, "", "")
                LoadValuesVar(reg, yr, "mfpinfrnt", -6, "", "Internet Percent Use", -1, "", "")

                'mti Aug 15, 2011 - start
                Session("DPNetPcntRow") = curRow
                If RegCompare <> 9999 Then

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                    curRow = curRow + 1

                    gridRow = curRow
                    Session("DPNetPcntRow1") = curRow
                    LoadValuesVar(RegCompare, YrCompare, "INFRANET", 15, "GDP/Capita (PPP) Versus Internet Percent Use (2007)", "Internet Percent Use", Session("DPGDPPCP1"), "", "", True)
                    LoadValuesVar(RegCompare, YrCompare, "mfpinfrnt", -6, "", "Internet Percent Use", -1, "", "", True)

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                    curRow = curRow + 1
                Else
                    Session("DPNetPcntRow1") = Nothing
                End If
                'mti Aug 15, 2011 - end

            End If
            'mti July 18, 2011 - end

            'mti Sep 12, 2011 - start -show trad index for current base
            'mti July 18, 2011 - start
            If InfraSwitch = 1 Or InfraSwitch = 3 Then
                '        If InfraSwitch = 0 Or InfraSwitch = 1 Or InfraSwitch = 3 Then
                'mti Sep 12, 2011 - end -show trad index for current base

                curRow = curRow + 1
                gridRow = curRow


                'mti Sep 12, 2011 - start
                'LoadValuesVar reg, yr, "INFRAINDTRAD", 18, "GDP/Capita (PPP) Versus Infrastructure Index Traditional w/o Phone (2005) Log", "Infrastructure Index, Traditonal", Amin(40, GDPPCP), "", ""
                If GDPPCP <= 5 Then
                    LoadValuesVar(reg, yr, "INFRAINDTRAD", 18, "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2010) Log", "Infrastructure Index, Traditonal", GDPPCP, "", "")
                ElseIf GDPPCP >= 40 Then
                    LoadValuesVar(reg, yr, "INFRAINDTRAD", 20, "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2100) Log", "Infrastructure Index, Traditonal", GDPPCP, "", "")
                Else
                    LoadValuesVar(reg, yr, "INFRAINDTRAD", 0, "", "Infrastructure Index, Traditonal", 0, "", "")
                    XYTABL("GDP/Capita (PPP) Versus Infrastructure Index Traditional (2010) Log", GDPPCP, VarVals(1), Session("TFuncPoints"), Session("XPoints"), Session("YPoints"), "", Me, Session("APoint"), Session("BPoints"))
                    grd_TableDisplay(2, curRow) = VarVals(1)
                    XYTABL("GDP/Capita (PPP) Versus Infrastructure Index Traditional (2100) Log", GDPPCP, VarVals(1), Session("TFuncPoints"), Session("XPoints"), Session("YPoints"), "", Me, Session("APoint"), Session("BPoints"))
                    grd_TableDisplay(2, curRow) = Math.Round((40 - GDPPCP) / 35 * Val(grd_TableDisplay(2, curRow)) + (GDPPCP - 5) / 35 * VarVals(1), 4)
                    'jrs 2014/03/11
                    'Adjust Standard Error
                    grd_TableDisplay(3, curRow) = StdDevs(18)
                    grd_TableDisplay(3, curRow) = Math.Round((40 - GDPPCP) / 35 * Val(grd_TableDisplay(3, curRow)) + (GDPPCP - 5) / 35 * StdDevs(20), 4)
                    grd_TableDisplay(4, curRow) = grd_TableDisplay(3, curRow)
                End If
                LoadValuesVar(reg, yr, "mfpinfrindtrad", -6, "", "Infrastructure Index, Traditonal", -1, "", "")
                'mti Sep 12, 2011 - end

                'mti Aug 15, 2011 - start
                Session("DPInfTradRow") = curRow
                If RegCompare <> 9999 Then

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                    curRow = curRow + 1
                    gridRow = curRow
                    Session("DPInfTradRow1") = curRow
                    'mti Sep 12, 2011 - start
                    'LoadValuesVar RegCompare, yr, "INFRAINDTRAD", 18, "GDP/Capita (PPP) Versus Infrastructure Index Traditional w/o Phone (2005) Log", "Infrastructure Index, Traditonal", Amin(40, GDPPCP1), "", "", True
                    If Session("DPGDPPCP1") <= 5 Then
                        LoadValuesVar(RegCompare, YrCompare, "INFRAINDTRAD", 18, "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2010) Log", "Infrastructure Index, Traditonal", Session("DPGDPPCP1"), "", "", True)
                    ElseIf Session("DPGDPPCP1") >= 40 Then
                        LoadValuesVar(RegCompare, YrCompare, "INFRAINDTRAD", 20, "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2100) Log", "Infrastructure Index, Traditonal", Session("DPGDPPCP1"), "", "", True)
                    Else
                        LoadValuesVar(RegCompare, YrCompare, "INFRAINDTRAD", 0, "", "Infrastructure Index, Traditonal", 0, "", "", True)
                        XYTABL("GDP/Capita (PPP) Versus Infrastructure Index Traditional (2010) Log", Session("DPGDPPCP1"), VarVals(1), Session("TFuncPoints"), Session("XPoints"), Session("YPoints"), "", Me, Session("APoint"), Session("BPoints"))
                        grd_TableDisplay(2, curRow) = VarVals(1)
                        XYTABL("GDP/Capita (PPP) Versus Infrastructure Index Traditional (2100) Log", Session("DPGDPPCP1"), VarVals(1), Session("TFuncPoints"), Session("XPoints"), Session("YPoints"), "", Me, Session("APoint"), Session("BPoints"))
                        grd_TableDisplay(2, curRow) = Math.Round((40 - Session("DPGDPPCP1")) / 35 * Val(grd_TableDisplay(2, curRow)) + (Session("DPGDPPCP1") - 5) / 35 * VarVals(1), 4)
                    End If
                    LoadValuesVar(RegCompare, YrCompare, "mfpinfrindtrad", -6, "", "Infrastructure Index, Traditonal", -1, "", "", True)
                    'mti Sep 12, 2011 - end
                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                    curRow = curRow + 1
                Else
                    Session("DPInfTradRow1") = Nothing
                End If
                'mti Aug 15, 2011 - end


            End If

            If InfraSwitch = 2 Or InfraSwitch = 3 Then
                curRow = curRow + 1
                gridRow = curRow
                'jrs 2005/10/18
                'mti Sep 12, 2011 - start
                'GDP Per Capita at PPP Versus Infratructure Index ICT (2005) Log
                'dummy function
                'LoadValuesVar reg, yr, "INFRAINDICT", 19, "GDP/Capita (PPP) Versus Infrastructure Index Traditional w/o Phone (2005) Log", "Infrastructure Index, Traditonal", Amin(40, GDPPCP), "", ""
                'jrs 2013/11/27 LoadValuesVar reg, yr, "INFRAINDICT", 19, "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2010) Log", "Infrastructure Index, Traditonal", Amin(40, GDPPCP), "", ""
                LoadValuesVar(reg, yr, "INFRAINDICT", 0, "", "Infrastructure Index, ICT- Algorithm", 0, "", "")
                LoadValuesVar(reg, yr, "mfpinfrindict", -6, "", "Infrastructure Index, ICT- Algorithm", -1, "", "")
                'mti Sep 12, 2011 - end


                'remove column values as it is algorithmic
                Dim II1 As Integer
                'mti Sep 12, 2011 - start
                For II1 = 1 To .Columns.Count - 1
                    'For II1 = 2 To .cols - 2
                    'mti Sep 12, 2011 - end
                    grd_TableDisplay(II1, curRow) = ""
                Next


                'mti Aug 15, 2011 - start
                Session("DPInfICTRow") = curRow
                If RegCompare <> 9999 Then


                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                    curRow = curRow + 1
                    gridRow = curRow
                    Session("DPInfICTRow1") = curRow

                    'mti Sep 12, 2011 - start
                    'LoadValuesVar RegCompare, yr, "INFRAINDICT", 19, "GDP/Capita (PPP) Versus Infrastructure Index Traditional w/o Phone (2005) Log", "Infrastructure Index, Traditonal", Amin(40, GDPPCP1), "", "", True
                    'jrs 2013/11/27 LoadValuesVar RegCompare, YrCompare, "INFRAINDICT", 19, "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2010) Log", "Infrastructure Index, Traditonal", Amin(40, GDPPCP1), "", "", True
                    LoadValuesVar(RegCompare, YrCompare, "INFRAINDICT", 0, "", "Infrastructure Index, ICT- Algorithm", 0, "", "", True)
                    LoadValuesVar(RegCompare, YrCompare, "mfpinfrindict", -6, "", "Infrastructure Index, ICT- Algorithm", -1, "", "", True)
                    'mti Sep 12, 2011 - end

                    'mti Sep 12, 2011 - start
                    For II1 = 1 To .Columns.Count - 1
                        'For II1 = 2 To .cols - 2
                        'mti Sep 12, 2011 - end
                        grd_TableDisplay(II1, curRow) = ""
                    Next
                    'mti Aug 15, 2011 - end

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                    curRow = curRow + 1

                Else
                    Session("DPInfICTRow1") = Nothing
                End If



            End If

            'mti July 18, 2011 - end

            'jrs 2013/11/27 *** begin ***
            If InfraSwitch = 3 Then
                curRow = curRow + 1
                gridRow = curRow
                LoadValuesVar(reg, yr, "GDS", 0, "", "Other Infrastructure Spending", 0, "InfraOther", "")
                grd_TableDisplay(2, curRow) = Math.Round(Amin(4, 1.8162 + 0.061 * Math.Log(GDPPCP)), 4)
                grd_TableDisplay(3, curRow) = 0
                grd_TableDisplay(4, curRow) = 0
                grd_TableDisplay(6, curRow) = 0.2


                Session("DPOthrInfrSpenRow") = curRow
                If RegCompare <> 9999 Then

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                    curRow = curRow + 1
                    gridRow = curRow
                    Session("DPOthrInfrSpenRow1") = curRow

                    LoadValuesVar(RegCompare, YrCompare, "GDS", 0, "", "Other Infrastructure Spending", 0, "InfraOther", "", True)
                    grd_TableDisplay(2, curRow) = Math.Round(Amin(4, 1.8162 + 0.061 * Math.Log(Session("DPGDPPCP1"))), 4)
                    grd_TableDisplay(3, curRow) = 0
                    grd_TableDisplay(4, curRow) = 0
                    grd_TableDisplay(6, curRow) = 0.2

                    grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                    curRow = curRow + 1
                Else
                    Session("DPOthrInfrSpenRow1") = Nothing
                End If
            End If
            'jrs 2013/11/27 *** end ***

            'curRow = curRow + 1
            'gridRow = curRow
            'LoadValuesVar(reg, yr, "INFRAROAD", 12, "GDP/Capita (PPP) Versus Road Network/Land Area (2000)", "Road Ntwk/Land Area", GDPPCP, "", "")
            'LoadValuesVar(reg, yr, "mfpinfrrd", -6, "", "Road Ntwk/Land Area", -1, "", "")
            'curRow = curRow + 1
            'gridRow = curRow
            'LoadValuesVar(reg, yr, "INFRAELEC", 13, "GDP/Capita (PPP 2000) Versus Kilowatt-Hours per Capita (2000) Linear", "Kilowatt-hours per capita", GDPPCP, "", "")
            'LoadValuesVar(reg, yr, "mfpinfrel", -6, "", "Kilowatt-hours per capita", -1, "", "")
            'curRow = curRow + 1
            'gridRow = curRow
            'LoadValuesVar(reg, yr, "INFRATELE", 14, "GDP/Capita (PPP) Versus Telephones per 1000 (2000)", "Telephones per 1000", GDPPCP, "", "")
            'LoadValuesVar(reg, yr, "mfpinfrate", -6, "", "Telephones per 1000", -1, "", "")
            'curRow = curRow + 1
            'gridRow = curRow
            'LoadValuesVar(reg, yr, "INFRANET", 15, "GDP/Capita (PPP) Versus Internet Percent Use (2000)", "Internet Percent Use", GDPPCP, "", "")
            'LoadValuesVar(reg, yr, "mfpinfrnt", -6, "", "Internet Percent Use", -1, "", "")

            curRow = curRow + 2 '20
            gridRow = curRow
            Session("DPKNRow") = curRow
            LoadValuesVar(reg, yr, "MFPKN", -1, "", "Knowledge", -1, "", "")
            'mti Aug 15, 2011 - start
            If RegCompare <> 9999 Then
                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPKNRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "MFPKN", -1, "", "Knowledge", -1, "", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPKNRow1") = Nothing
            End If
            'mti Aug 15, 2011 -  end

            curRow = curRow + 1
            gridRow = curRow
            'mti July 18, 2011 - start
            'save R&DRow for later use (may be different depending on switch values)
            Session("DPRNDRow") = curRow
            'mti July 18, 2011 - end

            'JRS 2013/11/29 LoadValuesVar(reg, yr, "GDS", 16, "GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear", "R&D Expenditures", GDPPCP, "R&D", "")
            LoadValuesVar(reg, yr, "RANDDEXP", 16, "GDP/Capita (PPP 2005) Versus R&D Total  as % of GDP (2009) Linear", "R&D Expenditures", GDPPCP, "", "")
            LoadValuesVar(reg, yr, "mfprandd", -6, "", "R&D Expenditures", -1, "R&D", "")

            'mti Aug 15, 2011 - start
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow

                'save R&DRow for later use (may be different depending on switch values)
                Session("DPRNDRow1") = curRow

                'jrs 2013/11/27 LoadValuesVar RegCompare, YrCompare, "GDS", 16, "GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear", "R&D Expenditures", GDPPCP1, "R&D", "", True
                LoadValuesVar(RegCompare, YrCompare, "RANDDEXP", 16, "GDP/Capita (PPP 2005) Versus R&D Total  as % of GDP (2009) Linear", "R&D Expenditures", Session("DPGDPPCP1"), "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfprandd", -6, "", "R&D Expenditures", -1, "R&D", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPRNDRow1") = Nothing
            End If
            'mti Aug 15, 2011 - end

            curRow = curRow + 1
            gridRow = curRow
            'jrs 2005/10/18
            'jrs 2024/11/08 LoadValuesVar(reg, yr, "ECONINTEG", 17, "GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear", "R&D Expenditures", GDPPCP, "", "")
            LoadValuesVar(reg, yr, "ECONINTEG", 17, "GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) Linear", "R&D Expenditures", GDPPCP, "", "")
            LoadValuesVar(reg, yr, "mfpeconint", -6, "", "Economic Integration - Algorithm", -1, "", "")
            'jrs 2025/07/25
            grd_TableDisplay(0, gridRow) = "Economic Integration"
            'jrs 2005/11/17 remove when ready
            Dim ii As Integer
            For ii = 1 To 6
                grd_TableDisplay(ii, gridRow) = ""
            Next

            'mti Aug 15, 2011 - start
            Session("DPECONINTEGRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPECONINTEGRow1") = curRow
                'jrs 2024/11/08 LoadValuesVar(RegCompare, YrCompare, "ECONINTEG", 17, "GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear", "R&D Expenditures", Session("DPGDPPCP1"), "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "ECONINTEG", 17, "GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) Linear", "R&D Expenditures", Session("DPGDPPCP1"), "", "", True)
                LoadValuesVar(RegCompare, YrCompare, "mfpeconint", -6, "", "Economic Integration - Algorithm", -1, "", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text

                For ii = 1 To .Columns.Count - 1
                    grd_TableDisplay(ii, curRow) = ""
                Next
            Else
                Session("DPECONINTEGRow1") = Nothing
            End If
            'mti Aug 15, 2011 - end

            'jrs 2018/09/14 *** begin ***
            curRow = curRow + 1
            gridRow = curRow

            LoadValuesVar(reg, yr, "EDTERGRATE", 22, "GDP/Capita (PPP) Versus Tertiary SciEngg Graduation Rate Log (MostRecent)", "Tertiary SciEngg Graduation Rate", GDPPCP, "Total", "")
            Dim EDTERGRSCIENs(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("EDTERGRSCIEN", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, EDTERGRSCIENs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfEDTERGRSCIEN" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("EDTERGRSCIEN", -1, cmb_Country.Text, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, EDTERGRSCIENs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            Session("EDTERGRSCIENs") = EDTERGRSCIENs(1)
            grd_TableDisplay(1, curRow) = Val(grd_TableDisplay(1, curRow)) * EDTERGRSCIENs(1) / 100
            LoadValuesVar(reg, yr, "mfpedscien", -6, "", "Tertiary SciEngg Graduation Rate", -1, "Total", "")

            Session("DPEdSciGradRow") = curRow
            If RegCompare <> 9999 Then

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country.Text + " - " + Cmb_Year.Text

                curRow = curRow + 1
                gridRow = curRow
                Session("DPEdSciGradRow1") = curRow
                LoadValuesVar(RegCompare, YrCompare, "EDTERGRATE", 22, "GDP/Capita (PPP) Versus Tertiary SciEngg Graduation Rate Log (MostRecent)", "Tertiary SciEngg Graduation Rate", Session("DPGDPPCP1"), "Total", "", True)
                Dim EDTERGRSCIENs1(1) As Single
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("EDTERGRSCIEN", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                        1, EDTERGRSCIENs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfEDTERGRSCIEN" + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay("EDTERGRSCIEN", -1, cmb_Country_Compare.Text, RegCompare, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                        1, EDTERGRSCIENs1, 1, 1, YrCompare, YrCompare, YrCompare - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                End If
                Session("EDTERGRSCIENs1") = EDTERGRSCIENs1(1)
                grd_TableDisplay(1, curRow) = Val(grd_TableDisplay(1, curRow)) * EDTERGRSCIENs1(1) / 100

                LoadValuesVar(RegCompare, YrCompare, "mfpedscien", -6, "", "Tertiary SciEngg Graduation Rate", -1, "Total", "", True)

                grd_TableDisplay(0, curRow) = grd_TableDisplay(0, curRow) + " - " + cmb_Country_Compare.Text + " - " + Cmb_Year_Compare.Text
                curRow = curRow + 1
            Else
                Session("DPEdSciGradRow1") = Nothing
            End If
            'jrs 2018/09/14 *** end ***
        End With
    End Sub

    'jrs 2013/11/28 added bCompareCountry
    'Subroutine to Load the Value into Cell
    Private Sub LoadValuesVar(ByVal reg As Integer, ByVal yr As Integer, ByVal sVar As String,
        ByVal iStdDevIdx As Integer, ByVal sTblFunName As String, ByVal sLineLabel As String,
        ByVal iv As Single, ByVal sSecondDim As String, ByVal sThirdDim As String,
        Optional bCompareCountry As Boolean = False)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim VarIdx As Integer
        Dim VarVal As Single
        Dim VarVals(1) As Single
        VarIdx = IfsVars.Item(sVar).Index
        'jrs 2005/05/19
        If VarIdx = -1 Then Exit Sub
        'jrs 2006/07/04
        Dim bGrp As Boolean = Session("DevProfGroup")

        'jrs 2013/12/10
        If Not bCompareCountry Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, False, False,
                    bGrp, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, True, True,
                    bGrp, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
        Else
            Dim yrc As Integer
            yrc = Val(Cmb_Year_Compare.Text)
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sVar, VarIdx, cmb_Country_Compare.SelectedValue, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, False, False,
                    bGrp, 1, VarVals, 1, 1, yrc, yrc, yrc - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay(sVar, VarIdx, cmb_Country_Compare.SelectedValue, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, True, True,
                    bGrp, 1, VarVals, 1, 1, yrc, yrc, yrc - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
        End If

        grd_TableDisplay(0, gridRow) = sLineLabel
        'jrs 2005/05/13
        If iStdDevIdx > -1 Then
            grd_TableDisplay(1, gridRow) = VarVals(1)
            If iv > 0 Then
                XYTABL(sTblFunName, iv, VarVal, Session("TFuncPoints"), Session("XPoints"), Session("YPoints"), "", Me, Session("APoint"), Session("BPoints"))
                grd_TableDisplay(2, gridRow) = VarVal
            End If
            'jrs 2013/11/25
            If sVar = "HLYLDWORK" Then
                grd_TableDisplay(3, gridRow) = 0
                If grd_TableDisplay(2, gridRow) > "0" Then
                    VarVals(1) = Math.Round(VarVals(1) / Val(grd_TableDisplay(2, gridRow)), 4)
                    grd_TableDisplay(4, gridRow) = VarVals(1)
                End If
            Else
                'jrs 2005/04/26
                grd_TableDisplay(3, gridRow) = StdDevs(iStdDevIdx)
                grd_TableDisplay(4, gridRow) = StdDevs(iStdDevIdx)
            End If
        Else
            If iStdDevIdx = -6 Then
                grd_TableDisplay(6, gridRow) = VarVals(1)
            Else
                grd_TableDisplay(5, gridRow) = VarVals(1)
            End If
        End If

    End Sub

    'jrs 2013/11/25
    Private Sub LoadValuesToCell(ByVal sReg As String, yr As Integer, sVar As String,
        sSecondDim As String, sThirdDim As String, bGroup As Boolean, rowIdx As Integer,
        colidx As Integer, sPct As String)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Dim yrc As Integer
        Dim VarIdx As Integer
        Dim VarVal As Single
        Dim VarVals(1) As Single
        VarIdx = IfsVars.Item(sVar).Index

        If VarIdx = -1 Then Exit Sub
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sVar, VarIdx, sReg, -1, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, False, False,
                bGroup, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay(sVar, VarIdx, sReg, -1, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, True, True,
                bGroup, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        If sPct = "POPWORK" Then
            Dim POPWs(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("POPWORKING", -1, sReg, -1, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, False, False,
                    bGroup, 1, POPWs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfPOPWORKING" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("POPWORKING", -1, sReg, -1, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, True, True,
                    bGroup, 1, POPWs, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            If POPWs(1) > 0 Then
                VarVals(1) = Math.Round(VarVals(1) / POPWs(1), 4)
            Else
                VarVals(1) = 0
            End If
        End If


        grd_TableDisplay(colidx, rowIdx) = VarVals(1)

    End Sub

    Private Sub grd_TableDisplay_GetCellProp(ByVal sender As Object, ByVal e As TMSWebControls.CellPropEventArgs) Handles grd_TableDisplay.GetCellProp
        If e.Column = 0 Then
            If pnlCompare.Visible Then
                Select Case e.Row
                    Case Session("DPHCRow"), Session("DPSCRow"), Session("DPPCRow"), Session("DPKNRow"), Session("DPHCRow1"), Session("DPSCRow1"), Session("DPPCRow1"), Session("DPKNRow1")
                        e.Font.Bold = True
                End Select
            Else
                Select Case e.Row
                    Case Session("DPHCRow"), Session("DPSCRow"), Session("DPPCRow"), Session("DPKNRow") 'jrs 2013/12/03 0, 6, 13, 19
                        e.Font.Bold = True
                End Select
            End If
        End If
    End Sub

    'Function to round values
    Private Sub RoundValues()

        Dim i As Integer
        Dim j As Integer
        For i = 0 To grd_TableDisplay.TotalRows - 1
            For j = 1 To 6
                If IsNumeric(grd_TableDisplay(j, i)) Then
                    Select Case Math.Abs(Val(grd_TableDisplay(j, i)))
                        Case Is > 1000
                            grd_TableDisplay(j, i) = Math.Round(Val(grd_TableDisplay(j, i)), 0)
                        Case Is > 100
                            grd_TableDisplay(j, i) = Math.Round(Val(grd_TableDisplay(j, i)), 1)
                        Case Is > 10
                            grd_TableDisplay(j, i) = Math.Round(Val(grd_TableDisplay(j, i)), 2)
                        Case Is > 1
                            grd_TableDisplay(j, i) = Math.Round(Val(grd_TableDisplay(j, i)), 3)
                        Case Is >= 0
                            grd_TableDisplay(j, i) = Math.Round(Val(grd_TableDisplay(j, i)), 4)
                    End Select
                End If
            Next j
        Next i

    End Sub

    Private Sub StdDevCalc()
        'Recalculate Std Dev
        With grd_TableDisplay
            Dim i As Integer
            Dim V As Single, f As Single
            For i = 1 To grd_TableDisplay.TotalRows - 1
                If IsNumeric(grd_TableDisplay(1, i)) Then
                    V = CSng(grd_TableDisplay(1, i))
                    'jrs 2013/12/02
                    If IsNumeric(grd_TableDisplay(2, i)) Then
                        f = CSng(grd_TableDisplay(2, i))
                        If grd_TableDisplay(4, i) <> 0 Then
                            grd_TableDisplay(4, i) = (V - f) / CSng(grd_TableDisplay(4, i))
                        End If
                    End If
                End If
            Next i
        End With
    End Sub

    Private Sub PctofGDP()
        Dim GDPvals(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("GDP", GDPidx, cmb_Country.SelectedValue, cmb_Country.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, GDPvals, 1, 1, Cmb_Year.SelectedValue, Cmb_Year.SelectedValue, Cmb_Year.SelectedValue - Session("FirstYear"), True,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGDP" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("GDP", GDPidx, cmb_Country.SelectedValue, cmb_Country.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, GDPvals, 1, 1, Cmb_Year.SelectedValue, Cmb_Year.SelectedValue, Cmb_Year.SelectedValue - Session("FirstYear"), True,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        GDPval = GDPvals(1)

        'jrs 2013/12/02 grd_TableDisplay(1, 2) = CSng(grd_TableDisplay(1, 2)) / GDPval * 100
        grd_TableDisplay(1, Session("DPEdnSpndRow")) = Math.Round(CSng(grd_TableDisplay(1, Session("DPEdnSpndRow"))) / GDPval * 100, 4)
        'jrs 2014/08/22 grd_TableDisplay(1, Session("DPRNDRow")) = Math.Round(CSng(grd_TableDisplay(1, Session("DPRNDRow"))) / GDPval * 100, 4)
        grd_TableDisplay(1, Session("DPOthrInfrSpenRow")) = Math.Round(CSng(grd_TableDisplay(1, Session("DPOthrInfrSpenRow"))) / GDPval * 100, 4)
        '        .row = 4
        '        .Text = CSng(.Text) / GDPval * 100
        'jrs 2013/12/02 grd_TableDisplay(1, 4) = CSng(grd_TableDisplay(1, 4)) / GDPval * 100
        '        .row = 7
        '        .Text = CSng(.Text) / GDPval * 100

        'jrs 2013/12/02 grd_TableDisplay(1, 20) = CSng(grd_TableDisplay(1, 20)) / GDPval * 100
        'jrs 2005/10/18
        '        .row = 22
        '        .Text = CSng(.Text) / GDPval * 100

        'jrs 2013/12/02
        ''jrs 2005/04/26 Adjust the Government Effectiveness
        'grd_TableDisplay(2, 8) = CSng(grd_TableDisplay(2, 8)) + 2.5
        'grd_TableDisplay(2, 9) = CSng(grd_TableDisplay(2, 9)) + 2.5

        If pnlCompare.Visible Then
            grd_TableDisplay(1, Session("DPEdnSpndRow1")) = Math.Round(CSng(grd_TableDisplay(1, Session("DPEdnSpndRow1"))) / GDPval * 100, 4)
            'jrs 2014/08/22 grd_TableDisplay(1, Session("DPRNDRow1")) = Math.Round(CSng(grd_TableDisplay(1, Session("DPRNDRow1"))) / GDPval * 100, 4)
            grd_TableDisplay(1, Session("DPOthrInfrSpenRow1")) = Math.Round(CSng(grd_TableDisplay(1, Session("DPOthrInfrSpenRow1"))) / GDPval * 100, 4)
        End If

    End Sub


    Private Sub LoadStdDevs()

        'jrs 2013/11/28 
        'StdDevs(0) = getStdDev(DataFileDir & "Functions for Development Profile\EdYears Func of GDPPCP.xls")
        'StdDevs(1) = getStdDev(DataFileDir & "Functions for Development Profile\Ed per GDP Func of GDPPCP linear.xls")
        'StdDevs(2) = getStdDev(DataFileDir & "Functions for Development Profile\Ed per GDP Func of GDPPCP log.xls")
        'StdDevs(3) = getStdDev(DataFileDir & "Functions for Development Profile\LifeExpect Func of GDPPCP.xls")
        'StdDevs(4) = getStdDev(DataFileDir & "Functions for Development Profile\Hlth per GDP Func of GDPPCP linear.xls")
        'StdDevs(5) = getStdDev(DataFileDir & "Functions for Development Profile\Hlth per GDP Func of GDPPCP log.xls")

        'StdDevs(6) = getStdDev(DataFileDir & "Functions for Development Profile\Freedom Func of GDPPCP.xls")
        'StdDevs(7) = getStdDev(DataFileDir & "Functions for Development Profile\Govt Effect Func of GDPPCP linear.xls")
        'StdDevs(8) = getStdDev(DataFileDir & "Functions for Development Profile\Govt Effect Func of GDPPCP log.xls")
        'StdDevs(9) = getStdDev(DataFileDir & "Functions for Development Profile\Corruption Percept func of GDPPCP linear.xls")
        'StdDevs(10) = getStdDev(DataFileDir & "Functions for Development Profile\Econ Freedom Func of GDPPCP log.xls")
        'StdDevs(11) = getStdDev(DataFileDir & "Functions for Development Profile\Econ Freedom Func of GDPPCP lin.xls")

        'StdDevs(12) = getStdDev(DataFileDir & "Functions for Development Profile\Road net func of GDPPCP.xls")
        'StdDevs(13) = getStdDev(DataFileDir & "Functions for Development Profile\KHR per cap func of GDPPCP.xls")
        'StdDevs(14) = getStdDev(DataFileDir & "Functions for Development Profile\Tele Lines func of GDPPCP.xls")
        'StdDevs(15) = getStdDev(DataFileDir & "Functions for Development Profile\Internet Use Rate func of GDPPCP.xls")

        'StdDevs(16) = getStdDev(DataFileDir & "Functions for Development Profile\R&D govt func of GDPPCP Linear.xls")
        'jrs 2005/10/18
        'StdDevs(17) = getStdDev(DataFileDir & "Functions for Development Profile\R&D govt func of GDPPCP Linear.xls")

        'jrs 2009/10/16 Retrieve Std Devs from TablFunc
        Dim sTbl As String
        sTbl = Session("PutFdrvRun") & "TablFunc.mdb"
        Dim Tbl As DataTable
        SetDataTablefromAccess(sTbl, "SELECT * FROM TablFunc", "Function Name", Tbl)

        'jrs 2013/11/26 adding Analitical Functions
        sTbl = Session("PutFdrvRun") & "AnalFunc.mdb"
        SetDataTablefromAccess(sTbl, "SELECT * FROM AnalFunc", "", Session("AnalTbl"))

        StdDevs(0) = SeekValue(Tbl, "GDP/Capita (PPP 2005) Versus Years of Educ at 15+ (2010) Log", "StdDev")
        StdDevs(1) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - linear", "StdDev")
        'jrs 2024/11/08 StdDevs(2) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - log", "StdDev")
        StdDevs(2) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) Log", "StdDev")
        StdDevs(3) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Life Expectancy (2000)", "StdDev")

        StdDevs(6) = SeekValueNoIdx(Session("AnalTbl"), "[Function Name]", "Freedom (Inverted) with GDPPCP and EdYears 15 (2012)", "StandardError")
        StdDevs(7) = SeekValueNoIdx(Session("AnalTbl"), "[Function Name]", "Governance Effectiveness (2005)", "StandardError")

        StdDevs(9) = SeekValueNoIdx(Session("AnalTbl"), "[Function Name]", "GOVCORRUPTversus GDPPCP, DEMOCPOLITY, GEM, Ex Export Depend (2010)", "StandardError")
        StdDevs(10) = SeekValueNoIdx(Session("AnalTbl"), "[Function Name]", "Economic Freedom (2010)", "StandardError")
        StdDevs(11) = SeekValueNoIdx(Session("AnalTbl"), "[Function Name]", "Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)", "StandardError")
        StdDevs(12) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Road Network/Land Area (2000)", "StdDev")
        StdDevs(13) = SeekValue(Tbl, "GDP/Capita (PPP 2000) Versus Kilowatt-Hours per Capita (2000) Linear", "StdDev")
        StdDevs(14) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Telephones per 1000 (2000)", "StdDev")
        StdDevs(15) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Internet Percent Use (2007)", "StdDev")
        StdDevs(16) = SeekValue(Tbl, "GDP/Capita (PPP 2005) Versus R&D Total  as % of GDP (2009) Linear", "StdDev")

        StdDevs(18) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2010) Log", "StdDev")
        StdDevs(19) = SeekValue(Tbl, "GDP/Capita (PPP 2005) Versus Stunting (2006) Log", "StdDev")
        StdDevs(20) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2100) Log", "StdDev")

        'jrs 2018/09/14
        StdDevs(21) = SeekValue(Tbl, "GDP/Capita (PPP 2011) Versus Quality of Educ at 15+ (2015) Log", "StdDev")
        StdDevs(22) = SeekValue(Tbl, "GDP/Capita (PPP) Versus Tertiary SciEngg Graduation Rate Log (MostRecent)", "StdDev")

        Session("StdDevs") = StdDevs

    End Sub

    Private Function getStdDev(ByVal sFileName) As Single
        oConn = New ADODB.Connection
        oConn.Open("Provider=Microsoft.Jet.OLEDB.4.0;" &
                       "Data Source=" & sFileName & ";" &
                       "Extended Properties=""Excel 8.0;HDR=NO;""")
        Dim oRS As ADODB.Recordset
        oRS = New ADODB.Recordset
        oRS.Open("SELECT * FROM [Sheet1$F2:F2]", oConn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        getStdDev = oRS.Fields(0).Value
        oRS.Close()
        oConn.Close()
    End Function

    'Subroutine to load countries in a group
    Private Sub LoadCountries(ByVal GroupName As String)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim dbG As String
        Dim valtblG As DataTable
        Dim resG As DataTable

        dbG = Session("IFs_MDB2Active")
        SetDataTablefromAccess(dbG, "SELECT * FROM " + GroupIDNew$ + " WHERE GroupName = """ &
            GroupName & """", "", resG)
        Dim GroupNum As Integer
        GroupNum = resG.Rows(0).Item("GroupNum")

        SetDataTablefromAccess(dbG, "SELECT * FROM " & Grouping, "", valtblG)
        Dim iTotal As Integer
        iTotal = 0

        '   Load Group Member Names List Box
        If Not (valtblG Is Nothing) Then
            Dim idx As Integer = 0
            Do While Not valtblG.Rows.Count = idx
                If valtblG.Rows(idx).Item("GroupNo") = GroupNum Then
                    iTotal = iTotal + 1
                    CountryGroup(iTotal) = valtblG.Rows(idx).Item("Region") 'resR.Fields("Country")
                End If
                idx = idx + 1
            Loop
        End If
        GroupTot = iTotal
    End Sub

    Private Sub mnuGroups_Click()

        'jam 2021/07/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        If Not Session("DevProfGroup") Then
            cswmWebMenu.Groups("mnuGroups").Caption = "Using Groups"
            cswmWebMenu.Groups("mnuCompare").Caption = "Compare Groups"
            lbl_countries.Text = "Groups"

            LoadGroups(cmb_Country)
        Else
            cswmWebMenu.Groups("mnuGroups").Caption = "Using Countries/&Regions"
            cswmWebMenu.Groups("mnuCompare").Caption = "Compare Countries/&Regions"
            lbl_countries.Text = "Countries or Regions"
            LoadRegions(cmb_Country)
        End If
        pnlCompare.Visible = False
        cmb_Country.SelectedIndex = 0   ' Display First Item
        Session("DevProfGroup") = Not Session("DevProfGroup")


        'jam 2021/07/30
        Session("IsGroup") = Not Session("DevProfGroup")



        cboFiles_SelectedIndexChanged(Me, New EventArgs)

        'jrs 2011/05/06
        If Session("DevProfGroup") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                'jam 2021/07/30
                'Support.FormManager.Show(Me, "frm_DevelopmentProfile.aspx?Group=" & cmb_Country.SelectedValue)

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_DevelopmentProfile.aspx")


                vurl = FriendlyUrl.Href("~/frm_DevelopmentProfile", DevProfileForFriendlyURL(cmb_Country.SelectedValue))

                Response.Redirect(vurl)

            Else
                'jam 2021/07/30
                'Support.FormManager.Show(Me, "frm_DevelopmentProfile.aspx?Group=" & Session("MMGroup"))

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_DevelopmentProfile.aspx")


                vurl = FriendlyUrl.Href("~/frm_DevelopmentProfile", DevProfileForFriendlyURL(Session("MMGroup")))

                Response.Redirect(vurl)


            End If
        Else
            'jam 2021/07/30
            'Support.FormManager.Show(Me, "frm_DevelopmentProfile.aspx?Country=AF")


            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_DevelopmentProfile.aspx")


            vurl = FriendlyUrl.Href("~/frm_DevelopmentProfile", "AF")

            Response.Redirect(vurl)


        End If
    End Sub

    Private Sub LoadGroups(CmbBx As DropDownList)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        CmbBx.Items.Clear()
        Dim db As String
        Dim valtbl As DataTable
        db = Session("IFs_MDB2Active")
        SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "GroupName", valtbl)
        Dim idx As Integer = 0
        While Not valtbl.Rows.Count = idx
            CmbBx.Items.Add(valtbl.Rows(idx).Item("GroupName"))
            idx = idx + 1
        End While
    End Sub

    Private Sub cmb_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
        If Not cmb_Country.Visible Then Exit Sub

        grd_TableDisplay.Visible = False
        If Cmb_Year.SelectedIndex = -1 Then Exit Sub

        'jrs 2005/11/30
        Session("DevcmbCountryIdx") = cmb_Country.SelectedIndex

        ClearValues()
        'mti Aug 15, 2011
        If pnlCompare.Visible Then
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text), cmb_Country_Compare.SelectedIndex + 1, Val(Cmb_Year_Compare.Text))
        Else
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        End If
        'mti Aug 15, 2011 -end

        'Pct of GDP
        PctofGDP()
        'jrs 2013/11/25
        'Pct of POPWORK
        PctofPOPWORK()
        'Std Dev calc
        StdDevCalc()
        'ROUND
        RoundValues()
        grd_TableDisplay.Visible = True
    End Sub

    Private Sub Cmb_Year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Year.SelectedIndexChanged
        If Not Cmb_Year.Visible Then Exit Sub

        grd_TableDisplay.Visible = False
        If cmb_Country.SelectedIndex = -1 Then Exit Sub

        'jrs 2005/11/30
        Session("DevcmbYearIdx") = Cmb_Year.SelectedIndex

        ClearValues()
        'mti Aug 15, 2011
        If pnlCompare.Visible Then
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.Text), cmb_Country_Compare.SelectedIndex + 1, Val(Cmb_Year_Compare.Text))
        Else
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        End If
        'mti Aug 15, 2011 -end

        'jrs 2013/11/25
        LoadValuesToCell("World", Val(Cmb_Year.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow"), 2, "POPWORK")

        Dim myVal As Single
        myVal = grd_TableDisplay(1, Session("DPDisabRow"))
        myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow"))), 4)
        grd_TableDisplay(4, Session("DPDisabRow")) = myVal

        'Pct of GDP
        PctofGDP()
        'jrs 2013/11/25
        'Pct of POPWORK
        PctofPOPWORK()
        'Std Dev calc
        StdDevCalc()
        'ROUND
        RoundValues()
        grd_TableDisplay.Visible = True
    End Sub

    'jrs 2005/11/30
    Private Sub DisplayOverTime(ByVal currRow As Integer)
        'JRS 09/12/02
        'Subroutine to display variable over time
        Dim bShow As Boolean

        Dim sVar As String
        Dim sDim2 As String, sDim3 As String
        'jrs 2013/11/27
        'Dim bGDPpct As Boolean
        'bGDPpct = False
        Dim sPct As String = ""
        Select Case currRow
            Case Session("DPEdYrsRow"), Session("DPEdYrsRow1") '2
                sVar = "EDYRSAG15"
                sDim2 = "Total"
                sDim3 = ""
            Case Session("DPEdnSpndRow"), Session("DPEdnSpndRow1") '3
                sVar = "GDS"
                sDim2 = "Education"
                'jrs 2013/12/02 bGDPpct = True
                sPct = "GDP"
                sDim3 = ""
            Case Session("DPLERow"), Session("DPLERow1") '4
                sVar = "LIFEXP"
                sDim2 = "Total"
                sDim3 = ""
            Case Session("DPHlthSpndRow"), Session("DPHlthSpndRow1") '5
                sVar = "GDS"
                sDim2 = "Health"
                'jrs 2013/12/01 bGDPpct = True
                sDim3 = ""
                sPct = "GDP"
                    'jrs 2013/11/25
            Case Session("DPStuntRow"), Session("DPStuntRow1")
                sVar = "HLSTUNT"
                sDim2 = ""
                sDim3 = ""
                    'jrs 2013/11/25
            Case Session("DPDisabRow"), Session("DPDisabRow1")
                sVar = "HLYLDWORK"
                sDim2 = "Total"
                sDim3 = "Total"
                sPct = "POPWORK"

            Case Session("DPFRDMRow"), Session("DPFRDMRow1")
                sVar = "FREEDOM"
                sDim2 = ""
                sDim3 = ""
            Case Session("DPGOVEFFLinRow"), Session("DPGOVEFFLinRow1") '9, 10
                sVar = "GOVEFFECT"
                sDim2 = ""
                sDim3 = ""
            Case Session("DPCORRow"), Session("DPCORRow1") '11
                sVar = "GOVCORRUPT"
                sDim2 = ""
                sDim3 = ""
            Case Session("DPEconFRDMRow"), Session("DPEconFRDMRow1") '12
                sVar = "ECONFREE"
                sDim2 = ""
                sDim3 = ""
                    'jrs 2013/11/26
            Case Session("DPDemocRow"), Session("DPDemocRow1")
                sVar = "DEMOCPOLITY"
                sDim2 = ""
                sDim3 = ""

            Case Session("DPRdRow"), Session("DPRdRow1") '15
                sVar = "INFRAROAD"
                sDim2 = ""
                sDim3 = ""
            Case Session("DPElecConsRow"), Session("DPElecConsRow1") '16
                sVar = "INFRAELEC"
                sDim2 = ""
                sDim3 = ""
            Case Session("DPTeleOldRow"), Session("DPTeleOldRow1") '17
                'sVar = "INFRATELE"
                sVar = "INFRATELEOLD"
                sDim2 = ""
                sDim3 = ""
            Case Session("DPNetPcntRow"), Session("DPNetPcntRow1") '18
                sVar = "INFRANET"
                sDim2 = ""
                sDim3 = ""
            Case Session("DPInfTradRow"), Session("DPInfTradRow1")
                sVar = "INFRAINDTRAD"
                sDim2 = ""
                sDim3 = ""

            Case Session("DPOthrInfrSpenRow"), Session("DPOthrInfrSpenRow1")
                sVar = "GDS"
                sDim2 = "InfraOther"
                sDim3 = ""
                sPct = "GDP"

            Case Session("DPRNDRow"), Session("DPRNDRow1") '21

                sVar = "RANDDEXP" 'jrs 2014/08/22 "GDS"
                sDim2 = "" 'jrs 2014/08/22 "R&D"

                'JRS 2013/12/02 bGDPpct = True
                'jrs 2014/08/22 sPct = "GDP"

                sDim3 = ""
                    'jrs 2005/10/18
                    '        Case 22
                    '            sVar = "ECONINTEG"
                    '            sDim2 = ""
                    '            bGDPpct = True
            Case Session("DPHCRow"), Session("DPHCRow1")
                sVar = "MFPHC"
                sDim2 = ""
                sDim3 = ""

            Case Session("DPSCRow"), Session("DPSCRow1")
                sVar = "MFPSC"
                sDim2 = ""
                sDim3 = ""

            Case Session("DPPCRow"), Session("DPPCRow1")
                sVar = "MFPPC"
                sDim2 = ""
                sDim3 = ""

            Case Session("DPKNRow"), Session("DPKNRow1")
                sVar = "MFPKN"
                sDim2 = ""
                sDim3 = ""
                            'jrs 2018/09/14
            Case Session("DPEdLowVocRow"), Session("DPEdLowVocRow1")
                sVar = "EDSECLOWRVOC"
                sDim2 = "Total"
                sDim3 = ""
            Case Session("DPEdUpVocRow"), Session("DPEdUpVocRow1")
                sVar = "EDSECUPPRVOC"
                sDim2 = "Total"
                sDim3 = ""
            Case Session("DPEdQualRow"), Session("DPEdQualRow1")
                sVar = "EDQUALAG15"
                sDim2 = "Total"
                sDim3 = ""
            Case Session("DPEdSciGradRow"), Session("DPEdSciGradRow1")
                sVar = "EDTERGRATE"
                sDim2 = "Total"
                sDim3 = ""
                sPct = "EDTERGRSCIEN"
        End Select

        Dim bCompareCountry As Boolean
        bCompareCountry = DetIfCompare(currRow)

        If sVar > "" Then
            'Load Variable over time and Across scenarios
            LoadOverTime(sVar, sDim2, sDim3, sPct, currRow, bCompareCountry)
        End If

    End Sub

    'jrs 2005/11/30
    'jrs 2013/11/25 changed parameter from bGDPPct to sPct
    Private Sub LoadOverTime(ByVal sVar As String, ByVal sDim2 As String, ByVal sDim3 As String,
                                 ByVal sPct As String, ByVal currRow As Integer, Optional bCompareCountry As Boolean = False)

        'jam 2021/07/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'Display over time
        With grd_TableDisplay
            'JRS 12/10/02 MULTIPLE FILES
            Dim iFileOld As Integer
            iFileOld = cboFiles.SelectedIndex
            'jrs 2005/05/16
            cboFiles.Visible = False
            Dim iFileIdx As Integer
            For iFileIdx = 0 To cboFiles.Items.Count - 1
                cboFiles.SelectedIndex = iFileIdx
                LoadVarAllYrs(sVar, sDim2, sDim3, (iFileIdx = 0), sPct, currRow, bCompareCountry)
            Next iFileIdx
            cboFiles.SelectedIndex = iFileOld
            'jrs 2005/05/16
            cboFiles.Visible = True

            'jam 2021/07/30
            'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")
            vurl = FriendlyUrl.Href("~/frm_TableDisplay")
            Response.Redirect(vurl)


            'jrs 2005/11/30 .col = 0
            'jrs 2005/11/30 frm_TableDisplay.Caption = .Text
            'jrs 2005/11/30 bShow = True
        End With
    End Sub

    'jrs 2005/11/30
    ' Function to Load Values into PLOT vector, and then display them using frm_TableDisplay
    ' parameters:
    ' 1. name of the variable.
    ' 2. index for 2nd dimension.
    ' 3. index for 3rd dimension
    ' 4. Boolean to indicate we're starting from column 0 (true), or we're appending more
    '    columns (false).
    ' 5. Boolean percentage of GDP, changed to string to use other aux. variables like POPWORK
    ' 6. Integer Row number to display
    Public Sub LoadVarAllYrs(ByVal sName As String, ByVal sDim2 As String, ByVal sDim3 As String, ByVal newCols As Boolean, ByVal sPct As String, ByVal currRow As Integer, Optional bCompareCountry As Boolean = False)
        'JRS 9/11/02
        Dim idx As Integer
        Dim idim2 As Integer
        Dim idx2 As Integer
        idx2 = 1
        idx = IfsVars.Item(sName).Index
        Dim RegInd As Integer
        If Not bCompareCountry Then
            RegInd = cmb_Country.SelectedIndex + 1
        Else
            RegInd = cmb_Country_Compare.SelectedIndex + 1
        End If


        If newCols Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            'JRS 2/17/03
            InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))
        End If
        Session("COUNTE") = Session("COUNTE") + 1
        If Not bCompareCountry Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sName, idx, cmb_Country.SelectedValue, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sName + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay(sName, idx, cmb_Country.SelectedValue, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, cmb_Country.SelectedValue, sDim2, sDim3, Session("DevProfGroup"), cboFiles.SelectedIndex, "",
                Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        Else
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sName, idx, cmb_Country_Compare.SelectedValue, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sName + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay(sName, idx, cmb_Country_Compare.SelectedValue, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, cmb_Country_Compare.SelectedValue, sDim2, sDim3, Session("DevProfGroup"), cboFiles.SelectedIndex, "",
                Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        End If

        'mti June 2011 -start
        If sName = "INFRATELEOLD" Then
            'convert per 100 to per 1000
            Dim i1 As Integer
            For i1 = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("PLOT")(i1, Session("COUNTE")) = Session("PLOT")(i1, Session("COUNTE")) * 10
            Next i1
            Session("GR4N")(Session("COUNTE")) = "Lines per 1000"
        End If
        'mti June 2011 - end

        Dim i As Integer
        If sPct = "GDP" Then
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "/GDP"
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "/GDP"
            Dim GDPs() As Single
            ReDim GDPs(Session("LastYear") - Session("FirstYear") + 1)
            If Not bCompareCountry Then
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("GDP", GDPidx, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                        1, GDPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGDP" + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay("GDP", GDPidx, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                        1, GDPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                End If
            Else
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("GDP", GDPidx, cmb_Country_Compare.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                        1, GDPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfGDP" + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay("GDP", GDPidx, cmb_Country_Compare.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                        1, GDPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                End If
            End If
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) / GDPs(i) * 100 'jrs 2014/08/22 I was using 1 instaed of i on the RHS of the = for PLOT
            Next i
        ElseIf sPct = "POPWORK" Then 'jrs 2013/11/25
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "/POPWORK"
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "/POPWORK"
            Session("GR4N")(Session("COUNTE")) = "Disab Yrs p/cap"
            Dim POPWorks() As Single
            ReDim POPWorks(Session("LastYear") - Session("FirstYear") + 1)
            If Not bCompareCountry Then
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("POPWORKING", -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                        1, POPWorks, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfPOPWORKING" + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay("POPWORKING", -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                        1, POPWorks, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                End If
            Else
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("POPWORKING", -1, cmb_Country_Compare.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                        1, POPWorks, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfPOPWORKING" + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay("POPWORKING", -1, cmb_Country_Compare.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                        1, POPWorks, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                End If
            End If
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) / POPWorks(i) 'jrs 2014/08/22 I was using 1 instaed of i on the RHS of the = for PLOT
            Next i
        ElseIf sPct = "EDTERGRSCIEN" Then 'jrs 2018/09/14
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "*EDTERGRSCIEN/100"
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "*EDTERGRSCIEN/100"
            Session("GR4N")(Session("COUNTE")) = "Percentage"
            Dim EDTerScis() As Single
            ReDim EDTerScis(Session("LastYear") - Session("FirstYear") + 1)

            If Not bCompareCountry Then
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("EDTERGRSCIEN", -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                        1, EDTerScis, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfEDTERGRSCIEN" + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay("EDTERGRSCIEN", -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                        1, EDTerScis, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                End If
            Else
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("EDTERGRSCIEN", -1, cmb_Country_Compare.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                        1, EDTerScis, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfEDTERGRSCIEN" + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay("EDTERGRSCIEN", -1, cmb_Country_Compare.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                        1, EDTerScis, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                End If
            End If

            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) * EDTerScis(i) / 100
            Next i

        End If

        'jrs 2005/11/30
        'frm_TableDisplay.RefreshTable()
        'frm_TableDisplay.Show()
    End Sub


    Private Sub grd_TableDisplay_CellClick(ByVal sender As Object, ByVal e As TMSWebControls.ModEventArgs) Handles grd_TableDisplay.CellClick
        'jrs 2011/05/06
        Dim sPar As String
        If Session("DevProfGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        Session("TableDisplayParent") = "frm_DevelopmentProfile.aspx?" & sPar
        If e.Column = 1 And IsNumeric(grd_TableDisplay(e.Column, e.Row)) Then
            DisplayOverTime(e.Row)
        End If
        If e.Column = 0 And e.Row > 0 Then
            DisplayGraph(e.Row)
        End If
    End Sub

    'jrs 2005/11/30
    Private Sub DisplayGraph(ByVal currRow As Integer)

        'jam 2021/07/31
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        sDevProFun = ""
        Dim msg As String = ""
        Dim frm_msg As New frm_Message
        Select Case currRow
            Case Session("DPEdYrsRow"), Session("DPEdYrsRow1") '2
                'jrs 2013/11/28 sDevProFun = "GDP/Capita (PPP) Versus Years of Educ at 25+ (2000)"
                sDevProFun = "GDP/Capita (PPP 2005) Versus Years of Educ at 15+ (2010) Log"
                    '        Case 3
                    '            sDevProFun = "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - linear"
            Case Session("DPEdnSpndRow"), Session("DPEdnSpndRow1") '3
                'jrs 2024/11/08 sDevProFun = "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) - log"
                sDevProFun = "GDP/Capita (PPP) Versus Govt Exp Educ as % of GDP (2002) Log"
            Case Session("DPLERow"), Session("DPLERow1") '4
                sDevProFun = "GDP/Capita (PPP) Versus Life Expectancy (2000)"
                    '        Case 6
                    '            sDevProFun = "GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (2000) - linear"
            Case Session("DPHlthSpndRow"), Session("DPHlthSpndRow1") '5
                sDevProFun = "GDP/Capita (PPP) Versus Govt Exp Hlth as % of GDP (2000) - log"
                    'jrs 2013/11/25
            Case Session("DPStuntRow"), Session("DPStuntRow1")
                sDevProFun = "GDP/Capita (PPP 2005) Versus Stunting (2006) Log"

            Case Session("DPFRDMRow"), Session("DPFRDMRow1") '8
                'jrs 2006/02/01 sDevProFun = "GDP/Capita (PPP) Versus Freedom (2000)"
                'jrs 2013/12/02 sDevProFun = "GDP/Capita (PPP) Versus Freedom -Logged and Inverted (2005)"
                msg = "Sorry, we can't graph multi-dimensioned functions like: <br>Freedom (Inverted) with GDPPCP and EdYears 15 (2012)"
                frm_msg.LoadMessage(msg)
                'jam 2021/07/31
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)


            Case Session("DPGOVEFFLinRow"), Session("DPGOVEFFLinRow1") '9
                'jrs 2013/12/02 sDevProFun = "GDP/Capita (PPP 2000) Versus Governance Effectiveness (2002) Linear"
                msg = "Sorry, we can't graph multi-dimensioned functions like: <br>Governance Effectiveness (2005)"
                frm_msg.LoadMessage(msg)
                'jam 2021/07/31
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)



                    'jrs 2013/12/02
                    'Case 10
                    '    sDevProFun = "GDP/Capita (PPP 2000) Versus Governance Effectiveness (2002) Log"
            Case Session("DPCORRow"), Session("DPCORRow1") '11
                'jrs 2013/12/02 sDevProFun = "GDP/Capita (PPP 2000) Versus TI Corruption Perception (2000) Linear"
                msg = "Sorry, we can't graph multi-dimensioned functions like: <br>GOVCORRUPTversus GDPPCP, DEMOCPOLITY, GEM, Ex Export Depend (2010)"
                frm_msg.LoadMessage(msg)
                'jam 2021/07/30
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)



                    'jrs 2013/12/02 
                    'Case 12
                    '   sDevProFun = "GDP/Capita (PPP) Versus Economic Freedom (2000) Log"
                    '        Case 15
                    '            sDevProFun = "GDP/Capita (PPP) Versus Economic Freedom (2000) Linear"
            Case Session("DPEconFRDMRow"), Session("DPEconFRDMRow1") '12
                msg = "Sorry, we can't graph multi-dimensioned functions like: <br>Economic Freedom (2010)"
                frm_msg.LoadMessage(msg)
                'jam 2021/07/31
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)


            Case Session("DPDemocRow"), Session("DPDemocRow1") 'jrs 2013/11/26
                msg = "Sorry, we can't graph multi-dimensioned functions like: <br>Polity Democracy versus GEM, YTHBULGE, En Export Dependence (2010)"
                frm_msg.LoadMessage(msg)
                'jam 2021/07/31
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)

            Case Session("DPRdRow"), Session("RdRow1") '15
                sDevProFun = "GDP/Capita (PPP) Versus Road Network/Land Area (2000)"
            Case Session("DPElecConsRow"), Session("DPElecConsRow1") '16
                sDevProFun = "GDP/Capita (PPP 2000) Versus Kilowatt-Hours per Capita (2000) Linear"
            Case Session("DPTeleOldRow"), Session("DPTeleOldRow1") '17
                sDevProFun = "GDP/Capita (PPP) Versus Telephones per 1000 (2000)"
            Case Session("DPNetPcntRow"), Session("DPNetPcntRow1") '18
                'jrs 2013/12/02 sDevProFun = "GDP/Capita (PPP) Versus Internet Percent Use (2000)"
                sDevProFun = "GDP/Capita (PPP) Versus Internet Percent Use (2007)"
            Case Session("DPInfTradRow"), Session("DPInfTradRow1")

                'jrs 2009/11/10 sDevProFun = "GDP/Capita (PPP) Versus Internet Percent Use (2000)"
                'mti Sep 12, 2011 - start
                'sDevProFun = "GDP/Capita (PPP) Versus Infrastructure Index Traditional w/o Phone (2005) Log"
                'jrs 2013/11/27
                If GDPPCP <= 5 Then
                    sDevProFun = "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2010) Log"
                ElseIf GDPPCP >= 40 Then
                    sDevProFun = "GDP/Capita (PPP) Versus Infrastructure Index Traditional (2100) Log"
                Else
                    msg = "Sorry, we currently can't graph the function for GDPPCP between 5 and 40 because the system uses the combination of 2 functions. "
                    frm_msg.LoadMessage(msg)
                    'jam 2021/07/31
                    'Support.FormManager.StateStore(Me)
                    'Support.FormManager.Show(Me, "frm_Message.aspx")


                    Vpath = Request.GetFriendlyUrlFileVirtualPath

                    Vext = Request.GetFriendlyUrlFileExtension

                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message")


                    vurl = FriendlyUrl.Href("~/frm_Message")

                    Response.Redirect(vurl)



                End If
                    'mti Sep 12, 2011 - end
                    'bound at 40 thousand dollars
                    'jrs 2013/11/27 BoundInp = 40

                    'jrs 2013/11/27
            Case Session("DPOthrInfrSpenRow"), Session("DPOthrInfrSpenRow1")
                msg = "Sorry, we currently can't graph the function: OthInfSp = 1.8162 + 0.061 * Log(GDPPCP) "
                frm_msg.LoadMessage(msg)
                'jam 2021/07/31
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)



            Case Session("DPRNDRow"), Session("DPRNDRow1") '21
                'jrs 2013/12/02 sDevProFun = "GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear"
                sDevProFun = "GDP/Capita (PPP 2005) Versus R&D Total  as % of GDP (2009) Linear"
                    'jrs 2005/10/18
                    '        Case 22
                    '            sDevProFun = "GDP/Capita (PPP) Versus Govt Exp R&D as % of GNP (2000) - linear"
        'jrs 2018/09/14
            Case Session("DPEdQualRow"), Session("DPEdQualRow1")
                sDevProFun = "GDP/Capita (PPP 2011) Versus Quality of Educ at 15+ (2015) Log"
            Case Session("DPEdSciGradRow"), Session("DPEdSciGradRow1")
                sDevProFun = "GDP/Capita (PPP) Versus Tertiary SciEngg Graduation Rate Log (MostRecent)"
        End Select

        If sDevProFun > "" Then
            Session("sDevProFun") = sDevProFun
            iDevProFunIdx = 0
            Dim i As Integer
            For i = 1 To TFCount
                If sDevProFun = FuncName$(i) Then
                    iDevProFunIdx = i
                    Exit For
                End If
            Next i
            Session("iDevProFunIdx") = iDevProFunIdx

            'jrs 2005/04/05
            FunVal = CSng(grd_TableDisplay(1, currRow))

            'jrs 2013/12/02
            If pnlCompare.Visible Then
                If DetIfCompare(currRow) Then
                    'comparison (latter) row
                    Session("DPFunVal1") = FunVal
                    FunVal = CSng(grd_TableDisplay(1, currRow - 1))
                Else
                    'original (former) row
                    Session("DPFunVal1") = CSng(grd_TableDisplay(1, currRow + 1))
                End If
                Session("DevProReg1") = cmb_Country_Compare.SelectedValue
                Session("DevProYr1") = Cmb_Year_Compare.SelectedValue
            Else
                Session("DevProReg1") = Nothing
                Session("DevProYr1") = Nothing
            End If
            Session("DPFunVal") = FunVal
            Session("RefreshGraph") = True
            Session("DevProReg") = cmb_Country.SelectedValue
            Session("DevProYr") = Cmb_Year.SelectedValue

            'jam 2021/07/31
            'Support.FormManager.Show(Me, "frm_FuncGraph.aspx")


            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_FuncGraph")


            vurl = FriendlyUrl.Href("~/frm_FuncGraph")

            Response.Redirect(vurl)



        End If
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        If Session("DevProfGroup") = False Then
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

    'jrs 2013/11/25
    Private Sub PctofPOPWORK()
        Dim POPWorkvals(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("POPWORKING", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                1, POPWorkvals, 1, 1, Cmb_Year.Text, Cmb_Year.Text, Cmb_Year.Text - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfPOPWORKING" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("POPWORKING", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                1, POPWorkvals, 1, 1, Cmb_Year.Text, Cmb_Year.Text, Cmb_Year.Text - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        Session("DPPOPWorkVal") = POPWorkvals(1)

        If pnlCompare.Visible Then
            Dim POPWorkvals1(1) As Single
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("POPWORKING", -1, cmb_Country_Compare.Text, cmb_Country_Compare.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("DevProfGroup"),
                    1, POPWorkvals1, 1, 1, Cmb_Year_Compare.Text, Cmb_Year_Compare.Text, Cmb_Year_Compare.Text - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("MyFileforDisplay"), Session("dfPOPWORKING" + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay("POPWORKING", -1, cmb_Country_Compare.Text, cmb_Country_Compare.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("DevProfGroup"),
                    1, POPWorkvals1, 1, 1, Cmb_Year_Compare.Text, Cmb_Year_Compare.Text, Cmb_Year_Compare.Text - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            Session("DPPOPWorkVal1") = POPWorkvals1(1)
        End If

        'RNDRow1% , EdnSpndRow%, EdnSpndRow1%, HlthSpndRow%, HlthSpndRow1%, GOVEFFLinRow%, GOVEFFLinRow1%, GOVEFFLogRow%, GOVEFFLogRow1%

        grd_TableDisplay(1, Session("DPDisabRow")) = CSng(grd_TableDisplay(1, Session("DPDisabRow"))) / Session("DPPOPWorkVal")

        If pnlCompare.Visible Then
            grd_TableDisplay(1, Session("DPDisabRow1")) = CSng(grd_TableDisplay(1, Session("DPDisabRow1"))) / Session("DPPOPWorkVal1")
        End If
    End Sub

    Protected Sub cmb_Country_Compare_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmb_Country_Compare.SelectedIndexChanged

        grd_TableDisplay.Visible = False
        If Cmb_Year.SelectedIndex = -1 Then Exit Sub
        If Cmb_Year_Compare.SelectedIndex = -1 Then Exit Sub
        If cmb_Country_Compare.SelectedIndex = -1 Then Exit Sub

        Session("DevcmbCountryIdx2") = cmb_Country_Compare.SelectedIndex

        ClearValues()
        LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), cmb_Country_Compare.SelectedIndex + 1, Val(Cmb_Year_Compare.Text))
        'Pct of GDP
        PctofGDP()
        'jrs 2013/11/25
        'Pct of POPWORK
        PctofPOPWORK()
        'Std Dev calc
        StdDevCalc()
        'ROUND
        RoundValues()
        grd_TableDisplay.Visible = True

    End Sub

    Protected Sub Cmb_Year_Compare_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles Cmb_Year_Compare.SelectedIndexChanged

        grd_TableDisplay.Visible = False
        If Cmb_Year_Compare.SelectedIndex = -1 Then Exit Sub
        If cmb_Country_Compare.SelectedIndex = -1 Then Exit Sub

        Session("DevcmbYearIdx2") = Cmb_Year_Compare.SelectedIndex

        ClearValues()
        LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), cmb_Country_Compare.SelectedIndex + 1, Val(Cmb_Year_Compare.Text))
        LoadValuesToCell("World", Val(Cmb_Year.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow1"), 2, "POPWORK")

        Dim myVal As Single
        myVal = grd_TableDisplay(1, Session("DPDisabRow1"))
        myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow1"))), 4)
        grd_TableDisplay(4, Session("DPDisabRow1")) = myVal

        'Pct of GDP
        PctofGDP()
        'jrs 2013/11/25
        'Pct of POPWORK
        PctofPOPWORK()
        'Std Dev calc
        StdDevCalc()
        'ROUND
        RoundValues()
        grd_TableDisplay.Visible = True

    End Sub

    Private Sub mnuHideComparison_Click()
        'mti Aug 15, 2011 -new proc
        pnlCompare.Visible = False
        Session("DPCompare") = Nothing
        grd_TableDisplay.TotalRows = 27 'jrs 2019/08/22 23 'grd_TableDisplay.rows * 2
        grd_TableDisplay.RowCount = grd_TableDisplay.TotalRows
        ClearAllValues()
        cmb_Country_SelectedIndexChanged(Me, New EventArgs)
        'jrs 2013/11/25
        LoadValuesToCell("World", Val(Cmb_Year.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow"), 2, "POPWORK")
        Dim myVal As Single
        myVal = grd_TableDisplay(1, Session("DPDisabRow"))
        myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow"))), 4)
        grd_TableDisplay(4, Session("DPDisabRow")) = myVal

    End Sub

    Private Sub mnuShowComparison_Click()
        If pnlCompare.Visible Then
            Exit Sub
        End If
        'mti Aug 15, 2011 -new proc

        'mti Sep 12, 2011 - whole proc
        '    frcompare.ZOrder 0
        pnlCompare.Visible = True
        Session("DPCompare") = True

        If Session("DevProfGroup") Then
            lbl_Countries_Compare.Text = "Compare Groups"
            LoadGroups(cmb_Country_Compare)
        Else
            lbl_Countries_Compare.Text = "Compare Countries or Regions"
            LoadRegions(cmb_Country_Compare)
        End If



        grd_TableDisplay.TotalRows = 59
        grd_TableDisplay.RowCount = grd_TableDisplay.TotalRows
        ClearAllValues()

        ''   Load Region Names Combo (Dropdown List) Box
        ''   -----------------------------------------
        '    cmb_Country_Compare.Clear
        '    Dim EleLoop As Integer
        '
        '    For EleLoop = 1 To MaxVal(1) - 1 'Exclude 'ALL'
        '       cmb_Country_Compare.AddItem GRPS$(1, EleLoop)
        '    Next EleLoop

        'mti Sep 12, 2011 - start
        Cmb_Year_Compare.SelectedIndex = Cmb_Year.SelectedIndex
        'mti Sep 12, 2011 - end

        cmb_Country_Compare.SelectedIndex = cmb_Country.SelectedIndex
        cmb_Country_Compare_SelectedIndexChanged(Me, New EventArgs)

        'jrs 2013/11/25
        LoadValuesToCell("World", Val(Cmb_Year.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow"), 2, "POPWORK")
        LoadValuesToCell("World", Val(Cmb_Year_Compare.Text), "HLYLDWORK", "Total", "Total", True, Session("DPDisabRow1"), 2, "POPWORK")
        Dim myVal As Single
        myVal = grd_TableDisplay(1, Session("DPDisabRow"))
        myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow"))), 4)
        grd_TableDisplay(4, Session("DPDisabRow")) = myVal

        myVal = grd_TableDisplay(1, Session("DPDisabRow1"))
        myVal = Math.Round(myVal - Val(grd_TableDisplay(2, Session("DPDisabRow1"))), 4)
        grd_TableDisplay(4, Session("DPDisabRow1")) = myVal


    End Sub

    Private Function DetIfCompare(currRow As Integer) As Boolean
        'mti Aug 15, 2011 - start
        Select Case currRow
            Case Session("DPEdYrsRow"), Session("DPEdnSpndRow"), Session("DPLERow"), Session("DPHlthSpndRow"), Session("DPFRDMRow"), Session("DPGOVEFFLinRow"), Session("DPCORRow"), Session("DPEconFRDMRow"), Session("DPRdRow"), Session("DPElecConsRow"), Session("DPTeleOldRow"), Session("DPNetPcntRow"), Session("DPInfTradRow"), Session("DPInfICTRow"), Session("DPRNDRow"), Session("DPHCRow"), Session("DPSCRow"), Session("DPPCRow"), Session("DPKNRow")
                DetIfCompare = False
            Case Session("DPEdYrsRow1"), Session("DPEdnSpndRow1"), Session("DPLERow1"), Session("DPHlthSpndRow1"), Session("DPFRDMRow1"), Session("DPGOVEFFLinRow1"), Session("DPCORRow1"), Session("DPEconFRDMRow1"), Session("DPRdRow1"), Session("DPElecConsRow1"), Session("DPTeleOldRow1"), Session("DPNetPcntRow1"), Session("DPInfTradRow1"), Session("DPInfICTRow1"), Session("DPRNDRow1"), Session("DPHCRow1"), Session("DPSCRow1"), Session("DPPCRow1"), Session("DPKNRow1"), Session("DPStuntRow1"), Session("DPDisabRow1"), Session("DPDemocRow1") 'jrs 2013/12/02
                DetIfCompare = True
            Case Else
                DetIfCompare = False
        End Select
        'mti Aug 15, 2011 -  end

    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/07/30
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function DevProfileForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        DevProfileForFriendlyURL = tmp
    End Function
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''     jam 2021/07/30
    ''' 
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>

    Public Sub mnuContinue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'jam 2021/07/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'Session.Remove("WVS_ bGroups")
        'Session.Remove("WVS_CommonPlot")
        Session.Remove("DevProfGroup")
        Session.Remove("IsGroup")


        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/" + Session("DisplayParent"))
        vurl = FriendlyUrl.Href("~/" + Session("DisplayParent"))

        Response.Redirect(vurl)




        'Support.FormManager.Close(Me)

    End Sub
End Class
