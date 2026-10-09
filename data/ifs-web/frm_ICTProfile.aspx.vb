Imports System.Math
Imports IFs.Data
'jam 2021/12/30
Imports Microsoft.AspNet.FriendlyUrls

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'     JAM 2017-11-11 Infrastructure Profile Form
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Partial Class frm_ICTProfile
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim StdDevs(50) As Single 'JAM 2017-12-09


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        'jrs 2005/11/28
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'jam 2021/12/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim qtyPAram As Integer




        'jam 2021/12/30
        othCountry = ""
        If (Not IsNothing(Request.QueryString.Get("Country")) Or Not IsNothing(Request.QueryString.Get("Group"))) And Not Page.IsPostBack Then

            If Not IsNothing(Request.QueryString("Country")) Then

                othCountry = Request.QueryString("Country")
            Else
                If Not IsNothing(Request.QueryString("Group")) Then
                    othCountry = Request.QueryString("Group")

                Else
                    othCountry = ""


                End If
            End If
        End If



        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_ICTProfile.aspx")


        If othCountry <> "" Then
            vurl = FriendlyUrl.Href("~/frm_ICTProfile", ICTPrepareForFriendlyURL(othCountry))

            Response.Redirect(vurl)

        Else

            vurl = FriendlyUrl.Href("~/frm_ICTProfile")

        End If


        'jam 2021/12/30
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String

        'jam 2021/12/30
        If segments.Count = 0 Then

            vCountry = Session("MMCountry")

        Else

            If segments.Count = 1 Then

                vCountry = segments(0)


            Else
                vCountry = segments(0)
                Session("vCountry") = vCountry

            End If

        End If

        Session("vCountry") = vCountry





        CreateMenu()

        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            'jrs 2005/11/30
            Session("TableDisplayParent") = "frm_ICTProfile.aspx"



            Dim bVarInGridEmpty As Boolean = False
            Dim bReLoad As Boolean
            bReLoad = (Session("VarsInGrid") Is Nothing)
            If Not bReLoad Then
                If UBound(Session("PopAllYrs"), 1) <> Session("LastYear") - Session("FirstYear") + 1 Then
                    bReLoad = True
                End If
            End If

            If bReLoad Then
                bVarInGridEmpty = True
                ReDim Session("VarsInGrid")(37)
                ReDim Session("RiskIdx")(37)
                ReDim Session("PopAll")(NR)
                ReDim Session("PopAllYrs")(Session("LastYear") - Session("FirstYear") + 1, NR)
                'prepare grid
                Session("bAbsolute") = True
            End If

            'JAM 2017-12-09
            LoadStdDevs()


            'Load General Values
            LoadGralValues(Not bVarInGridEmpty)
            'Load Regions
            If Session("IsICTGroup") Is Nothing Or Session("IsICTGroup") = False Then
                LoadRegions()
                'JAM 2018-03-20 Changing country/group label
                lbl_countries.Text = "Countries or Regions"
            Else
                LoadGroups()
                'JAM 2018-03-20 Changing country/group label
                lbl_countries.Text = "Groups"
            End If

            'Load Files
            LoadFiles(gRetrieveFiles(Session("PutFdrvRun2"), Session("WorkingFile"), Session("RunFiles"), Session("FirstYear"), Session("FilesSelected"), Session("ShortSce"), Session("IFsRunFiles")), cboFiles)
            If cboFiles.Items.Count > 0 Then
                If Session("ICTFilesIdx") Is Nothing Then
                    'jrs 2026/01/26 changed default
                    cboFiles.SelectedIndex = 1
                Else
                    cboFiles.SelectedIndex = Session("ICTFilesIdx")
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

            'Display first elements
            If Cmb_Year.Items.Count > 0 Then
                If Session("ICTcmbYearIdx") Is Nothing Then
                    Cmb_Year.SelectedIndex = 0   ' Display First Item
                Else
                    Cmb_Year.SelectedIndex = Session("ICTcmbYearIdx")
                End If
            End If
            ' goes into Procedure summary
            If cmb_Country.Items.Count > 0 Then
                If Session("ICTcmbCountryIdx") Is Nothing Then
                    cmb_Country.SelectedIndex = 0   ' Display First Item
                Else
                    cmb_Country.SelectedIndex = Session("ICTcmbCountryIdx")
                End If
            End If

            If Session("IsICTGroup") Is Nothing Then
                Session("IsICTGroup") = False
            End If


            LoadValues()

        End If


        cswmWebMenu.UserData = "jsolorza::976739975"
        'jam 2021/12/30
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/12/30
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
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
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If
        'jam 2021/12/30
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"
        'jam 2022/01/03
        'cswmGroup1.OnMenuBarClick = "location.href='" & FriendlyUrl.Resolve(Session("DisplayParent")) & "';"
        cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl("~/" + Session("DisplayParent")) & "';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        If Not Session("IsICTGroup") Then
            cswmGroup1.Caption = "Using Country"
        Else
            cswmGroup1.Caption = "Using Groups"
        End If
        cswmGroup1.ID = "mnuGroups"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Switch"

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16007);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        ''cswmGroup1 = cswmWebMenu.Groups.Add()
        ''cswmGroup1.Caption = "Display Options"
        ''cswmGroup1.ID = "mnuDispOpt"

        ''cswmItem = cswmGroup1.Items.Add()
        ''cswmItem.Caption = "Recalculate and update with new weights"
        ''cswmItem.IconImage = "blank.gif"

        ''cswmItem = cswmGroup1.Items.Add()
        ''cswmItem.Caption = "Using absolute evaluation"
        ''cswmItem.ID = "mnuDispOptAbs"
        ''If Session("bAbsolute") = True Then
        ''    cswmItem.IconImage = "checkbox.gif"
        ''Else
        ''    cswmItem.IconImage = "blank.gif"
        ''End If

        ''cswmItem = cswmGroup1.Items.Add()
        ''cswmItem.Caption = "Using relative evaluation with income level"
        ''cswmItem.ID = "mnuDispOptRel"
        ''If Session("bAbsolute") = False Then
        ''    cswmItem.IconImage = "checkbox.gif"
        ''Else
        ''    cswmItem.IconImage = "blank.gif"
        ''End If

        ''cswmItem = cswmGroup1.Items.Add()
        ''cswmItem.Caption = "Select multiple scenarios"
        ''cswmItem.IconImage = "blank.gif"

        ''cswmItem = cswmGroup1.Items.Add()
        ''cswmItem.Caption = "Select multiple Country or Groups"
        ''cswmItem.IconImage = "blank.gif"
        'jam 2021/12/30
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Session("vCountry"))) And Not Session("IsICTGroup") Then
            'jrs 2011/05/06
            'If Not IsNothing(Request.QueryString("Country")) Then
            'jam 2021/12/30
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> Session("vCountry") Or Not Page.IsPostBack Then
                'jam 2021/12/30
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = Session("vCountry")
                'jrs 2012/08/15 Session("IsICTGroup") = False
                If Session("IsICTGroup") = True Then
                    cswmWebMenu.Groups("mnuGroups").Caption = "Using Countries/&Regions"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions()
                    Session("IsICTGroup") = False
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
            'jam 2021/12/30
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Session("vCountry"))) And Session("IsICTGroup") Then 'jam 2021/12/30
            'jam 2021/12/30
            'If cmb_Country.SelectedValue <> Request.QueryString("Group") Or Not Page.IsPostBack Then
            If cmb_Country.SelectedValue <> Session("vCountry") Or Not Page.IsPostBack Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/12/30
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(Session("vCountry"), False, Me)
                If gIdx > 0 Then
                    cmb_Country.SelectedIndex = gIdx - 1
                    Session("ICTcmbCountryIdx") = cmb_Country.SelectedIndex
                    'jam 2021/12/30
                    'Session("MMGroup") = Request.QueryString("Group")
                    Session("MMGroup") = Session("vCountry")
                    'Else
                    '    'JAM 2018-03-30 Removing isn't necessary
                    '    Session.Remove("MMGroup")
                End If
                If Session("IsICTGroup") = True Then
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
            Session("ICTcmbCountryIdx") = cmb_Country.SelectedIndex
            cmb_Country_SelectedIndexChanged(Me, New System.EventArgs)
        End If

    End Sub



    Private Sub LoadRegions()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        cmb_Country.Items.Clear()
        lstCountry.Items.Clear()
        Dim EleLoop As Integer
        For EleLoop = 1 To MaxVal(1) - 1 'Exclude 'ALL'
            cmb_Country.Items.Add(GRPS$(1, EleLoop))
            lstCountry.Items.Add(GRPS$(1, EleLoop))
        Next EleLoop
        'Preselect lstFiles
        If Session("lstCountry.SelCount") > 0 Then
            Dim idx As Integer
            For i = 0 To Session("lstCountry.SelCount") - 1
                idx = Session("ICTRegs")(i)
                If idx < lstCountry.Items.Count Then
                    lstCountry.Items(idx).Selected = True
                End If
            Next
        End If

    End Sub

    Private Sub LoadFiles(ByVal FileList() As String, ByVal cbo As DropDownList)
        Dim i As Integer
        For i = 0 To UBound(FileList)
            cbo.Items.Add(FileList(i))
            lstFiles.Items.Add(FileList(i))
        Next i
        'Preselect lstFiles
        If Session("lstFiles.SelCount") > 0 Then
            Dim idx As Integer
            For i = 0 To Session("lstFiles.SelCount") - 1
                idx = Session("RiskFiles")(i)
                lstFiles.Items(idx).Selected = True
            Next
        End If
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
            Case "Using absolute evaluation", "Using relative evaluation with income level"
                mnuAbsolute_Click()
            Case "Select multiple scenarios"
                mnuSelMulSce_Click()
            Case "Select multiple Country or Groups"
                mnuSelMulGrp_Click()
        End Select
    End Sub

    Private Sub mnuSelMulGrp_Click()
        lstCountry.Visible = True
        cmdSce.Visible = True
        cmdSceClear.Visible = True
        grd_TableDisplay.Visible = False
        cmb_Country.Visible = False
        If Session("lstCountry.SelCount") = 0 Then
            Dim i As Integer
            For i = 0 To lstCountry.Items.Count - 1
                lstCountry.Items(i).Selected = False
            Next
            lstCountry.Items(cmb_Country.SelectedIndex).Selected = True
        End If
    End Sub

    Private Sub mnuSelMulSce_Click()
        lstFiles.Visible = True
        cmdSce.Visible = True
        cmdSceClear.Visible = True
        grd_TableDisplay.Visible = False
        cboFiles.Visible = False
        If Session("lstFiles.SelCount") = 0 Then
            Dim i As Integer
            For i = 0 To lstFiles.Items.Count - 1
                lstFiles.Items(i).Selected = False
            Next
            lstFiles.Items(cboFiles.SelectedIndex).Selected = True
        End If
    End Sub

    Private Sub cboFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiles.SelectedIndexChanged
        'jrs 2005/05/16
        If Not cboFiles.Visible Then Exit Sub

        grd_TableDisplay.Visible = False
        If Cmb_Year.SelectedIndex = -1 Then Exit Sub
        'jrs 2005/11/30
        Session("ICTFilesIdx") = cboFiles.SelectedIndex

        ''LoadAllCtryValues(False)
        'End If
        LoadValues()

        'if selected file is not preselected reset
        If Session("lstCountry.SelCount") > 0 Then
            Dim idx As Integer
            If Not lstFiles.Items(Session("ICTFilesIdx")).Selected Then
                For i = 0 To Session("lstFiles.SelCount") - 1
                    idx = Session("RiskFiles")(i)
                    lstFiles.Items(idx).Selected = False
                Next
                Session("lstFiles.SelCount") = 0
            End If
        End If

        grd_TableDisplay.Visible = True
    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2017-12-13 Adding Loading Values
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Subroutine to Load the Values
    'into Development Profile Values table
    Private Sub LoadValues()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'Loads values for given country, year, and scenario
        Dim VarVals(1) As Single
        Dim yr As Integer

        Dim curRow As Integer



        'JAM 2017-12-14
        Dim vGDP(1) As Single
        Dim vPOP(1) As Single
        Dim vInfraRoad(1) As Single
        Dim vLandArea(1) As Single
        Dim vGDPPCP(1) As Single
        Dim vPavedRoad(1) As Single
        Dim vRuralAccess(1) As Single
        Dim vGenCapac(1) As Single
        Dim vTransLoss(1) As Single
        Dim vElecAccNat(1) As Single
        Dim vElecAccRural(1) As Single
        Dim vElecAccUrb(1) As Single
        Dim vAccWatPiped(1) As Single
        Dim vAccWatOther(1) As Single
        Dim vAccWatUnimp(1) As Single
        Dim vAccSanImp(1) As Single
        Dim vAccSanSha(1) As Single
        Dim vAccSanUni(1) As Single
        Dim vWasWatColl(1) As Single
        Dim vWasWatTreat(1) As Single
        Dim vLandAreaEquip(1) As Single
        Dim vLDGrazing(1) As Single
        Dim vLDCrop(1) As Single
        Dim vInfraInd(1) As Single
        Dim vInfraIndWatSan(1) As Single
        Dim vInfraIndictz(1) As Single





        Dim OneGDP As Single
        Dim OnePOP As Single
        Dim OneInfraRoad As Single
        Dim OneLandArea As Single
        Dim OneGDPPCP As Single
        Dim OnePavedRoad As Single
        Dim OneRuralAccess As Single
        Dim OneGenCapac As Single
        Dim RoadCol1 As Single
        Dim OneTransLoss As Single
        Dim OneElecAccNat As Single
        Dim OneElecAccRural As Single
        Dim OneElecAccUrb As Single
        Dim OneAccWatPiped As Single
        Dim OneAccWatOther As Single
        Dim OneAccWatUnimp As Single
        Dim OneAccSanImp As Single
        Dim OneAccSanSha As Single
        Dim OneAccSanUni As Single
        Dim OneWasWatColl As Single
        Dim OneWasWatTreat As Single
        Dim OneLandAreaEquip As Single
        Dim OneLD As Single
        Dim OneLDGrazing As Single
        Dim OneInfraInd As Single
        Dim OneInfraIndWatSan As Single
        Dim OneInfraIndictz As Single
        Dim HC As Single  'JAM 2018-02-28



        Dim bGroups As Boolean = False
        If Session("IsICTGroup") = True Then
            bGroups = True
        End If

        curRow = 1


        yr = Cmb_Year.Text

        'JAM 2017-12-14
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("GDP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
            bGroups, 1, vGDP, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
        Else
            gLoadForDisplay("GDP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
                bGroups, 1, vGDP, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        OneGDP = vGDP(1)

        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("GDPPCP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
            bGroups, 1, vGDPPCP, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
        Else
            gLoadForDisplay("GDPPCP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
                bGroups, 1, vGDPPCP, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        OneGDPPCP = vGDPPCP(1)
        ''Session("DPGDPPCP") = CSng(OneGDPPCP.ToString)



        txtIncome.Text = Math.Round(OneGDPPCP, 2).ToString("0.00")

        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("POP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
            bGroups, 1, vPOP, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
        Else
            gLoadForDisplay("POP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
                bGroups, 1, vPOP, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        OnePOP = vPOP(1)





        ''gLoadForDisplay("INFRAROAD", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups, _
        ''1, vInfraRoad, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), _
        ''Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), _
        ''Session("CurrencyVal"))
        ''OneInfraRoad = vInfraRoad(1)

        curRow = curRow + 1
        'JAM 2018-02-12
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAROAD", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Roads per Capita (KM) (2010 Forecast) Log", "Roads per capita (km.)", OneGDPPCP, "", "")


        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("LANDAREA", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
        1, vLandArea, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
        Session("CurrencyVal"))
        Else
            gLoadForDisplay("LANDAREA", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
            1, vLandArea, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
        End If
        OneLandArea = vLandArea(1)

        RoadCol1 = Round(1000 * Val(grd_TableDisplay.Rows(curRow).Cells(1).Text) * OneLandArea / OnePOP, 2)


        grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(RoadCol1, 2).ToString("0.00")

        HC = Val(grd_TableDisplay.Rows(curRow).Cells(1).Text)

        'JAM 2018-02-12
        grd_TableDisplay.Rows(curRow).Cells(2).Text = grd_TableDisplay.Rows(curRow).Cells(2).Text * 1000

        HC = HC - Val(grd_TableDisplay.Rows(curRow).Cells(2).Text)



        curRow = curRow + 1

        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAROADPAVEDPCNT", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Paved Roads (% of total) (2010 Forecast) Log", "Paved Roads (% of total)", OneGDPPCP, "", "")





        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAROADRAI", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Rural Access Index (% of rural pop) (2010 FC) Log", "Rural Access Index (% of rural population)", OneGDPPCP, "", "")


        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneRuralAccess, 2).ToString("0.00")




        ''''''''Energy

        'Generating Capacity per Capita


        curRow = curRow + 2
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAELECGENCAP", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Generating Capacity per Capita (KW FC) 2010 Linear", "Generating Capacity per Capita (Kw)", OneGDPPCP, "", "")


        'grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneGenCapac, 2).ToString("0.00")


        'Transmission Loss


        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAELECTRANLOSS", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Transmission Loss (%) (2010 Forecast) Log", "Transmission Loss (%)", OneGDPPCP, "", "")

        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneTransLoss, 2).ToString("0.00")


        'Electricity Access National


        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAELECACC", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access National (% Pop) (2010 FC) Log", "Electricity Access - National (% of population)", OneGDPPCP, "Total", "")

        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneElecAccNat, 2).ToString("0.00")


        'Electricity Access Urban
        '"GDP/Capita (PPP 2010 Forecast) Versus Electricity Access Urban (% Pop) (2010 Forecast) Log"
        VAggregation(IfsVars.Item("INFRAELECACC").Index) = "POPURBAN"
        VAggregation(IfsVars.Item("INFRAELECACCDEM").Index) = "POPURBAN"



        curRow = curRow + 1
        'grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneElecAccUrb, 2).ToString("0.00")
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = OneElecAccUrb

        'JAM 2018-02-11
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAELECACC", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access Urban (% Pop) (2010 Forecast) Log", "Electricity Access - Urban (% of population)", OneGDPPCP, "Urban", "")


        'Electricity Access Rural
        '"GDP/Capita (PPP 2010 Forecast) Versus Electricity Access Rural (% of Pop) (2010 FC) Log"
        VAggregation(IfsVars.Item("INFRAELECACC").Index) = "POPRURAL"
        VAggregation(IfsVars.Item("INFRAELECACCDEM").Index) = "POPRURAL"





        curRow = curRow + 1
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneElecAccRural, 2).ToString("0.00")
        'grd_TableDisplay.Rows(curRow).Cells(1).Text = OneElecAccRural
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAELECACC", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access Rural (% of Pop) (2010 FC) Log", "Electricity Access - Rural (% of population)", OneGDPPCP, "Rural", "")


        VAggregation(IfsVars.Item("INFRAELECACC").Index) = "POP"
        VAggregation(IfsVars.Item("INFRAELECACCDEM").Index) = "POP"

        '''''''''''''''''Water Sanitation

        'Access to Water - Piped
        curRow = curRow + 2
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneAccWatPiped, 2).ToString("0.00")
        'JAM 2018-02-17
        'jrs 2026/01/23 LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "WATSAFE", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Access to Water-Piped (% of Pop) (2010 Forecast) Log", "Access to Water - Piped (% of population)", OneGDPPCP, "Piped", "")
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "WATERHH", curRow, "GDP/Capita (PPP 2021) Versus Access to Water-SafelyManaged (% of Pop) Forecast (2022) Log", "Access to Water - Safely Managed (% of population)", OneGDPPCP, "SafelyManaged", "Total")


        'Access to Water - Other Improved

        curRow = curRow + 1
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneAccWatOther, 2).ToString("0.00")
        'JAM 2018-02-17
        'jrs 2026/01/23 LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "WATSAFE", curRow, "GDP/Capita (PPP 2010 FC) Versus Access to Water-Other Improved (% of Pop) (2010 FC) Log", "Access to Water - Other Improved (% of population)", OneGDPPCP, "OthImproved", "")
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "SANITHH", curRow, "GDP/Capita (PPP 2021) Versus Access to Sanitation-SafelyManaged(% of Pop) Forecast (2022) Log", "Access to Water - Other Improved (% of population)", OneGDPPCP, "SafelyManaged", "Total")


        'Access to Water - Unimproved
        'jrs 2026/01/23 curRow = curRow + 1
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneAccWatUnimp, 2).ToString("0.00")
        'JAM 2018-02-17
        'jrs 2026/01/23 LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "WATSAFE", curRow, "GDP/Capita (PPP 2010 FC) Versus Access to Water-UnImproved (% of Pop) (2010 FC) Log", "Access to Water - Unimproved (% of population)", OneGDPPCP, "UnImproved", "")

        'Access to Sanitation - Improved

        'jrs 2026/01/23 curRow = curRow + 1
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneAccSanImp, 2).ToString("0.00")
        'JAM 2018-02-17
        'jrs 2026/01/23 LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "SANITATION", curRow, "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Improved (% of Pop) (2010 FC) Log", "Access to Sanitation - Improved (% of population)", OneGDPPCP, "Improved", "")


        'Access to Sanitation - Shared
        'jrs 2026/01/23 curRow = curRow + 1
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneAccSanSha, 2).ToString("0.00")
        'JAM 2018-02-17
        'jrs 2026/01/23 LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "SANITATION", curRow, "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Shared (% of Pop) (2010 FC)  Log", "Access to Sanitation - Shared (% of population)", OneGDPPCP, "Shared", "")



        'Access to Sanitation - Unimproved
        'jrs 2026/01/23 curRow = curRow + 1
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneAccSanUni, 2).ToString("0.00")
        'JAM 2018-02-17
        'jrs 2026/01/23 LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "SANITATION", curRow, "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Unimproved (% of Pop) (2010 FC) Log", "Access to Sanitation - Unimproved (% of population)", OneGDPPCP, "OthUnimproved", "")



        'Wastewater Collected
        curRow = curRow + 1
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneWasWatColl, 2).ToString("0.00")
        'JAM 2018-02-17
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "WATWASTE", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Wastewater Collect (% of Pop) (2010 Forecast) Log", "Wastewater Collected (% of population)", OneGDPPCP, "", "")



        'Wastewater Treated
        curRow = curRow + 1
        ''grd_TableDisplay.Rows(curRow).Cells(1).Text = Math.Round(OneWasWatTreat, 2).ToString("0.00")
        'JAM 2018-02-17
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "WATWASTETREAT", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Wastewater Treated (% of Pop) (2010 Forecast) Log", "Wastewater Treated (% of population)", OneGDPPCP, "", "")


        curRow = curRow + 1
        'JAM 2018-02-25
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "LANDIRAREAEQUIP", curRow, "GDP/Capita (PPP) (2010) FC Versus ((Land Area for Irrigation/(CL + GL))/10) (2010) FC Log", "Area Equipped for Irrigation (% of Crop and Grazing land )", OneGDPPCP, "", "")
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("LD", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Crop", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
          1, vLDCrop, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
          Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
          Session("CurrencyVal"))
        Else
            gLoadForDisplay("LD", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Crop", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
              1, vLDCrop, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
              Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
              Session("CurrencyVal"))
        End If
        OneLD = vLDCrop(1)

        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("LD", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Grazing", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
          1, vLDGrazing, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
          Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
          Session("CurrencyVal"))
        Else
            gLoadForDisplay("LD", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Grazing", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
              1, vLDGrazing, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
              Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
              Session("CurrencyVal"))
        End If
        OneLDGrazing = vLDGrazing(1)

        OneLD = OneLD + OneLDGrazing
        grd_TableDisplay.Rows(curRow).Cells(1).Text = Round(Val(grd_TableDisplay.Rows(curRow).Cells(1).Text) / OneLD / 10, 2)

        'JAM 2018-02-26
        curRow = curRow + 2
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "NUMNWP", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Fixed Telephone Lines (per 100) (2010 Forecast) Log", "Percentage of networked persons", OneGDPPCP, "", "")

        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "ICTMOBIL", curRow, "GDP/Capita (PPP 2010 FC) Versus Mobile Telephone Subscriptions (per 100) (2010 FC) Log", "Mobile Telephone Subscriptions (per 100 persons)", OneGDPPCP, "", "")

        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "ICTBROAD", curRow, "GDP/Capita (PPP 2010 FC) Versus Fixed Broadband Subscriptions (per 100) (2010 FC) Linear", "Fixed Broadband Subscriptions (per 100 persons)", OneGDPPCP, "", "")

        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "ICTBROADMOBIL", curRow, "GDP/Capita (PPP 2010 FC) Versus Mobile Broadband Subscriptions (per 100) (2010 FC) Linear", "Mobile Broadband Subscriptions (per 100 persons)", Amin(OneGDPPCP, 80), "", "")

        'JAM 2018-02-26
        curRow = curRow + 2
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAINDELEC", curRow, "GDP/Capita (PPP 2010 FC) Versus Traditional Infrastructure Index (2010 FC) Log", "Traditional (standard errors, World = 0 in 2010)", OneGDPPCP, "", "")
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("INFRAINDTRAN", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
          1, vInfraInd, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
          Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
          Session("CurrencyVal"))
        Else
            gLoadForDisplay("INFRAINDTRAN", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
              1, vInfraInd, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
              Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
              Session("CurrencyVal"))
        End If
        OneInfraInd = vInfraInd(1)

        grd_TableDisplay.Rows(curRow).Cells(1).Text = Val(grd_TableDisplay.Rows(curRow).Cells(1).Text) + OneInfraInd
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("INFRAINDWATSAN", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
         1, vInfraIndWatSan, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
         Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
         Session("CurrencyVal"))
        Else
            gLoadForDisplay("INFRAINDWATSAN", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
             1, vInfraIndWatSan, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
             Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
             Session("CurrencyVal"))
        End If
        OneInfraIndWatSan = vInfraIndWatSan(1)

        grd_TableDisplay.Rows(curRow).Cells(1).Text = Round((Val(grd_TableDisplay.Rows(curRow).Cells(1).Text) + OneInfraIndWatSan) / 3, 2)
        OneInfraIndWatSan = Val(grd_TableDisplay.Rows(curRow).Cells(1).Text)

        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAINDICTZ", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Modern Infrastructure Index (SE) (2010 Forecast) Log", "Modern (standard errors, World = 0 in 2010)", OneGDPPCP, "Total", "")


        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAINDICT", curRow, "GDP/Capita (PPP 2010 FC) Versus Modern Infrastructure Index (0-100) (2010 FC) Log", "Modern (0-100)", OneGDPPCP, "", "")

        curRow = curRow + 1
        LoadValuesVar(curRow, cmb_Country.SelectedIndex + 1, yr, "INFRAINDELEC", curRow, "GDP/Capita (PPP 2010 Forecast) Versus Overall Infrastructure Index (2010) Log", "Overall (standard errors, World = 0 in 2010)", OneGDPPCP, "", "")
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("INFRAINDTRAN", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
          1, vInfraInd, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
          Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
          Session("CurrencyVal"))
        Else
            gLoadForDisplay("INFRAINDTRAN", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
              1, vInfraInd, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
              Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
              Session("CurrencyVal"))
        End If
        OneInfraInd = vInfraInd(1)

        grd_TableDisplay.Rows(curRow).Cells(1).Text = Val(grd_TableDisplay.Rows(curRow).Cells(1).Text) + OneInfraInd
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("INFRAINDWATSAN", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
        1, vInfraIndWatSan, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
        Session("CurrencyVal"))
        Else
            gLoadForDisplay("INFRAINDWATSAN", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
            1, vInfraIndWatSan, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
        End If
        OneInfraIndWatSan = vInfraIndWatSan(1)

        grd_TableDisplay.Rows(curRow).Cells(1).Text = Val(grd_TableDisplay.Rows(curRow).Cells(1).Text) + OneInfraIndWatSan
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("INFRAINDICTZ", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
   1, vInfraIndictz, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
   Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
   Session("CurrencyVal"))
        Else
            gLoadForDisplay("INFRAINDICTZ", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True, bGroups,
           1, vInfraIndictz, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
           Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
           Session("CurrencyVal"))
        End If
        OneInfraIndictz = vInfraIndictz(1)


        grd_TableDisplay.Rows(curRow).Cells(1).Text = Round((Val(grd_TableDisplay.Rows(curRow).Cells(1).Text) + OneInfraIndictz) / 4, 2)

        'grd_TableDisplay.Visible = True


        StdDevCalc()

        'JAM 2018-02-27
        RoundMatrix()

    End Sub

    Private Sub mnuGroups_Click()

        'jam 2021/12/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Session("lstCountry.SelCount") = 0
        If Not Session("IsICTGroup") Then
            cswmWebMenu.Groups("mnuGroups").Caption = "Using Groups"
            lbl_countries.Text = "Groups"
            LoadGroups()
        Else
            cswmWebMenu.Groups("mnuGroups").Caption = "Using Countries/&Regions"
            lbl_countries.Text = "Countries or Regions"
            LoadRegions()
        End If
        cmb_Country.SelectedIndex = 0   ' Display First Item
        Session("IsICTGroup") = Not Session("IsICTGroup")
        cboFiles_SelectedIndexChanged(Me, New EventArgs)

        'jrs 2011/05/06
        If Session("IsICTGroup") = True Then
            'JAM 2018-03-30
            'Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                'JAM 2018-03-30
                'Support.FormManager.Show(Me, "frm_ICTProfile.aspx?Group=" & cmb_Country.SelectedValue)
                Session("MMGroup") = "World"
                'jam 2021/12/30
                'Support.FormManager.Show(Me, "frm_ICTProfile.aspx?Group=" & Session("MMGroup"))



                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_ICTProfile.aspx")


                vurl = FriendlyUrl.Href("~/frm_ICTProfile", Session("MMGroup"))

                Response.Redirect(vurl)

            Else
                'jam 2021/12/30
                'Support.FormManager.Show(Me, "frm_ICTProfile.aspx?Group=" & Session("MMGroup"))



                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_ICTProfile.aspx")


                vurl = FriendlyUrl.Href("~/frm_ICTProfile", Session("MMGroup"))

                Response.Redirect(vurl)
            End If
        Else
            'JAM 2018-03-30
            'Support.FormManager.Show(Me, "frm_ICTProfile.aspx?Country=AF")
            'jam 2021/12/30
            'Support.FormManager.Show(Me, "frm_ICTProfile.aspx?Country=" & Session("MMCountry"))


            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_ICTProfile.aspx")


            vurl = FriendlyUrl.Href("~/frm_ICTProfile", Session("MMCountry"))

            Response.Redirect(vurl)

        End If
    End Sub

    Private Sub LoadGroups()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        cmb_Country.Items.Clear()
        'jrs 2026/01/14
        lstCountry.Items.Clear()
        Dim db As String
        Dim valtbl As DataTable
        Dim vSelected As Integer

        db = Session("IFs_MDB2Active")
        'jrs 2026/01/14
        SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew & " ORDER BY UPPER(GroupName)", "GroupName", valtbl)
        Dim idx As Integer = 0
        While Not valtbl.Rows.Count = idx
            cmb_Country.Items.Add(valtbl.Rows(idx).Item("GroupName"))
            lstCountry.Items.Add(valtbl.Rows(idx).Item("GroupName"))

            'jrs 2026/01/14
            'If valtbl.Rows(idx).Item("GroupName") = Session("MMGroup") Then
            '    Session("ICTcmbGroupIdx") = idx
            'End If

            idx = idx + 1




        End While

        'JAM 2018-03-30 Default Group is "World"
        'If cmb_Country.SelectedIndex <= 0 Then
        '    If IsNothing(Session("MMGroup")) And Session("IsICTGroup") Then
        '        Session("MMGroup") = "World"
        '    End If
        '    '    ''cmb_Country.SelectedIndex = vSelected
        '    '    ''lstCountry.SelectedIndex = vSelected
        'End If


        'Preselect lstFiles
        If Session("lstCountry.SelCount") > 0 Then
            For i = 0 To Session("lstCountry.SelCount") - 1
                idx = Session("ICTRegs")(i)
                lstCountry.Items(idx).Selected = True
            Next
        End If

    End Sub

    Private Sub cmb_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
        If Not cmb_Country.Visible Then Exit Sub

        If Cmb_Year.SelectedIndex = -1 Then Exit Sub



        'jrs 2005/11/30
        Session("ICTcmbCountryIdx") = cmb_Country.SelectedIndex

        'JAM 2018-03-30
        If Session("IsICTGroup") Then
            Session("MMGroup") = cmb_Country.SelectedValue
            '    'Else
            '    '    Session("MMCountry") = cmb_Country.SelectedValue
        End If



        LoadValues()
        grd_TableDisplay.Visible = True

        'if selected file is not preselected reset
        If Session("lstCountry.SelCount") > 0 Then
            Dim idx As Integer
            If Not lstCountry.Items(Session("ICTcmbCountryIdx")).Selected Then
                For i = 0 To Session("lstCountry.SelCount") - 1
                    idx = Session("ICTRegs")(i)
                    If idx < lstCountry.Items.Count Then
                        lstCountry.Items(idx).Selected = False
                    End If
                Next
                Session("lstCountry.SelCount") = 0
            End If
        End If

    End Sub

    Private Sub Cmb_Year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Year.SelectedIndexChanged
        If Not Cmb_Year.Visible Then Exit Sub

        grd_TableDisplay.Visible = False
        If cmb_Country.SelectedIndex = -1 Then Exit Sub

        'jrs 2005/11/30
        Session("ICTcmbYearIdx") = Cmb_Year.SelectedIndex

        ''LoadAllCtryValues(False)
        'End If
        LoadValues()
        grd_TableDisplay.Visible = True
    End Sub

    'jrs 2005/11/30
    Private Sub DisplayOverTime(ByVal currRow As Integer)
        'JRS 09/12/02
        'Subroutine to display variable over time
        Dim bShow As Boolean

        Dim sVar As String
        Dim sDim2 As String, sDim3 As String
        Dim bGDPpct As Boolean
        bGDPpct = False

        If currRow < grd_TableDisplay.Rows.Count - 2 Then
            If Session("VarsInGrid")(currRow).VarName > "" Then
                'Load Variable over time and Across scenarios
                If Session("VarsInGrid")(currRow).bFormula Then
                    LoadOverTime(Session("VarsInGrid")(currRow).VarName, "Total", "", Session("VarsInGrid")(currRow).bPopPct, Session("VarsInGrid")(currRow).bPopPort,
                    True, Session("VarsInGrid")(currRow).f.Formula, Session("VarsInGrid")(currRow).f)
                Else
                    LoadOverTime(Session("VarsInGrid")(currRow).VarName, "Total", "", Session("VarsInGrid")(currRow).bPopPct, Session("VarsInGrid")(currRow).bPopPort,
                    False, "", Nothing)
                End If
            End If
        End If

    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''              JAM 2018-03-29 Display Over Time for ICT
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="currRow"></param>
    ''' <remarks></remarks>
    Private Sub TableDisplayOverTime(ByVal currRow As Integer)
        'JRS 09/12/02
        'Subroutine to display variable over time
        Dim bShow As Boolean

        Dim sVar As String
        Dim sDim2 As String, sDim3 As String
        Dim bGDPpct As Boolean
        Dim sDenominator As String, sMult As String, sAdd As String
        bGDPpct = False

        ''''''''''''''''''''

        sDenominator = ""
        sMult = ""
        sAdd = ""
        Select Case currRow

            Case 2
                sVar = "INFRAROAD"
                sDim2 = ""
                sDim3 = ""
                sMult = "LANDAREA*1000"
                sDenominator = "POP"
            Case 3
                sVar = "INFRAROADPAVEDPCNT"
                sDim2 = ""
                sDim3 = ""
            Case 4
                sVar = "INFRAROADRAI"
                sDim2 = ""
                sDim3 = ""

            Case 6
                sVar = "INFRAELECGENCAP"
                sDim2 = ""
                sDim3 = ""
            Case 7
                sVar = "INFRAELECTRANLOSS"
                sDim2 = ""
                sDim3 = ""
            Case 8
                sVar = "INFRAELECACC"
                sDim2 = "Total"
                sDim3 = ""
            Case 9
                sVar = "INFRAELECACC"
                sDim2 = "Urban"
                sDim3 = ""
            Case 10
                sVar = "INFRAELECACC"
                sDim2 = "Rural"
                sDim3 = ""

            Case 12
                'jrs 2026/01/23
                'sVar = "WATSAFE"
                'sDim2 = "Piped"
                'sDim3 = ""
                sVar = "WATERHH"
                sDim2 = "SafelyManaged"
                sDim3 = "Total"
            Case 13
                'jrs 2026/01/23
                'sVar = "WATSAFE"
                'sDim2 = "OthImproved"
                'sDim3 = ""
                sVar = "SANITHH"
                sDim2 = "SafelyManaged"
                sDim3 = "Total"
            'Case 14
            '    sVar = "WATSAFE"
            '    sDim2 = "UnImproved"
            '    sDim3 = ""
            'Case 15
            '    sVar = "SANITATION"
            '    sDim2 = "Improved"
            '    sDim3 = ""
            'Case 16
            '    sVar = "SANITATION"
            '    sDim2 = "Shared"
            '    sDim3 = ""
            'Case 17
            '    sVar = "SANITATION"
            '    sDim2 = "OthUnImproved"
            '    sDim3 = ""
            Case 14
                sVar = "WATWASTE"
                sDim2 = ""
                sDim3 = ""
            Case 15
                sVar = "WATWASTETREAT"
                sDim2 = ""
                sDim3 = ""
            Case 16
                sVar = "LANDIRAREAEQUIP"
                sDim2 = ""
                sDim3 = ""
                'jrs 2014/05/14
                sDenominator = "LD(Crop)+LD(Grazing)"
                sMult = "0.1"

            Case 18
                sVar = "INFRATELE"
                sDim2 = ""
                sDim3 = ""
            Case 19
                sVar = "ICTMOBIL"
                sDim2 = ""
                sDim3 = ""
            Case 20
                sVar = "ICTBROAD"
                sDim2 = ""
                sDim3 = ""
            Case 21
                sVar = "ICTBROADMOBIL"
                sDim2 = ""
                sDim3 = ""

            Case 23
                sVar = "INFRAINDELEC"
                sDim2 = ""
                sDim3 = ""
                sAdd = "INFRAINDTRAN+INFRAINDWATSAN"
                sDenominator = "3"
            Case 24
                sVar = "INFRAINDICTZ"
                sDim2 = ""
                sDim3 = ""
            Case 25
                sVar = "INFRAINDICT"
                sDim2 = ""
                sDim3 = ""
            Case 26
                sVar = "INFRAINDELEC"
                sDim2 = ""
                sDim3 = ""
                sAdd = "INFRAINDTRAN+INFRAINDWATSAN+INFRAINDICTZ"
                sDenominator = "4"
        End Select

        'Desired values
        If Request.Form("txtCol") = 2 Then
            sVar = sVar & "DEM"
            If currRow = 23 Then
                sVar = "INFRAINDTRADEXP"
                sAdd = ""
                sDenominator = ""
            ElseIf currRow > 23 Then
                sVar = ""
            End If
        End If

        If sVar > "" Then
            'Load Variable over time and Across scenarios
            TableLoadOverTime(sVar, sDim2, sDim3, sDenominator, sMult, sAdd, currRow)
        End If


    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''   JAM 2018-03-29 Loading values for TableDisplay
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sVar"></param>
    ''' <param name="sDim2"></param>
    ''' <param name="sDim3"></param>
    ''' <param name="sDenominator"></param>
    ''' <param name="sMult"></param>
    ''' <param name="sAdd"></param>
    ''' <param name="currRow"></param>
    ''' <remarks></remarks>
    Private Sub TableLoadOverTime(ByVal sVar As String, ByVal sDim2 As String, ByVal sDim3 As String,
                             ByVal sDenominator As String, ByVal sMult As String, ByVal sAdd As String, ByVal currRow As Integer)


        'jam 2023/12/06
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim vform As String
        Dim sLst As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension



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
                TableLoadVarAllYrs(sVar, sDim2, sDim3, (iFileIdx = 0), sDenominator, sMult, sAdd, currRow)
            Next iFileIdx
            cboFiles.SelectedIndex = iFileOld
            'jrs 2005/05/16
            cboFiles.Visible = True

            'jam 2023/12/06
            'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

            'jam 2023/12/06
            vform = ("frm_TableDisplay.aspx")

            vurl = FriendlyUrl.Href("~/" & vform)

            Response.Redirect(vurl)



            'jrs 2005/11/30 .col = 0
            'jrs 2005/11/30 frm_TableDisplay.Caption = .Text
            'jrs 2005/11/30 bShow = True
        End With
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''           JAM 2018-03-29 Loading All Years for each scenario 
    ''' 
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sName"></param>
    ''' <param name="sDim2"></param>
    ''' <param name="sDim3"></param>
    ''' <param name="newCols"></param>
    ''' <param name="sDenominator"></param>
    ''' <param name="sMult"></param>
    ''' <param name="sAdd"></param>
    ''' <param name="currRow"></param>
    ''' <remarks></remarks>

    Public Sub TableLoadVarAllYrs(ByVal sName As String, ByVal sDim2 As String, ByVal sDim3 As String, ByVal newCols As Boolean, ByVal sDenominator As String, ByVal sMult As String, ByVal sAdd As String, ByVal currRow As Integer)

        Dim idx As Integer

        Dim idx2 As Integer
        Dim RegInd As Integer

        Dim j As Integer
        Dim i As Integer
        Dim Vals() As Single, Vals2() As Single
        Dim x As Object


        idx2 = 1
        idx = IfsVars.Item(sName).Index
        RegInd = cmb_Country.SelectedIndex + 1

        If newCols Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))
        End If
        Session("COUNTE") = Session("COUNTE") + 1
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sName, idx, cmb_Country.SelectedValue, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
            1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
        Else
            gLoadForDisplay(sName, idx, cmb_Country.SelectedValue, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
        End If
        gLoadForDisplaySaveLbls(Session("COUNTE"), idx, cmb_Country.SelectedValue, sDim2, sDim3, Session("DevProfGroup"), cboFiles.SelectedIndex, "",
        Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        If sAdd > "" Then
            Session("VARN")(0, Session("COUNTE")) = "(" & Session("VARN")(0, Session("COUNTE")) & "+" & sAdd & ")"
            Session("VARN")(1, Session("COUNTE")) = "(" & Session("VARN")(1, Session("COUNTE")) & "+" & sAdd & ")"

            ReDim Vals(Session("LastYear") - Session("FirstYear") + 1)
            x = Split(sAdd, "+")
            For j = 0 To UBound(x)
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(x(j), -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                1, Vals, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
                Else
                    gLoadForDisplay(x(j), -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                    1, Vals, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
                End If
                For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                    Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) + Vals(i)
                Next i
            Next j
        End If
        If sMult > "" Then
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "*" & sMult
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "*" & sMult

            ReDim Vals(Session("LastYear") - Session("FirstYear") + 1)
            x = Split(sMult, "*")
            For j = 0 To UBound(x)
                If Not IsNumeric(x(j)) Then
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(x(j), -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                    1, Vals, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
                    Else
                        gLoadForDisplay(x(j), -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                        1, Vals, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                    End If
                    For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                        Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) * Vals(i)
                    Next i

                Else
                    For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                        Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) * Val(x(j))
                    Next i


                End If
            Next j
        End If
        If sDenominator > "" Then
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "/" & sDenominator
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "/" & sDenominator

            ReDim Vals(Session("LastYear") - Session("FirstYear") + 1)
            ReDim Vals2(Session("LastYear") - Session("FirstYear") + 1)
            If Not IsNumeric(sDenominator) Then
                Dim pos As Integer
                pos = InStr(sDenominator, "+")
                If pos > 0 Then
                    Dim sDenominatorOrg As String
                    sDenominatorOrg = sDenominator
                    sDenominator = Mid(sDenominatorOrg, 1, pos - 1)
                    'Detect if there's second dimension
                    Dim pos2 As Integer
                    pos2 = InStr(sDenominator, "(")
                    If pos2 > 0 Then
                        sDim2 = Mid(sDenominator, pos + 1)
                        sDim2 = Replace(sDim2, ")", "")
                        sDenominator = Mid(sDenominator, 1, pos - 1)
                    End If
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(sDenominator, -1, cmb_Country.SelectedValue, RegInd, sDim2, -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                    1, Vals, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
                    Else
                        gLoadForDisplay(sDenominator, -1, cmb_Country.SelectedValue, RegInd, sDim2, -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                        1, Vals, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                    End If
                    'Second part of the SUM
                    sDenominator = Mid(sDenominatorOrg, pos + 1)
                    pos2 = InStr(sDenominator, "(")
                    If pos2 > 0 Then
                        sDim2 = Mid(sDenominator, pos + 1)
                        sDim2 = Replace(sDim2, ")", "")
                        sDenominator = Mid(sDenominator, 1, pos - 1)
                    End If
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(sDenominator, -1, cmb_Country.SelectedValue, RegInd, sDim2, -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                    1, Vals2, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
                    Else
                        gLoadForDisplay(sDenominator, -1, cmb_Country.SelectedValue, RegInd, sDim2, -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                        1, Vals2, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                    End If

                    For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                        If Vals(i) = 0 Then Exit For
                        Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) / (Vals(i) + Vals2(i))
                    Next i
                Else
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(sDenominator, -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                    1, Vals, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
                    Else
                        gLoadForDisplay(sDenominator, -1, cmb_Country.SelectedValue, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                        1, Vals, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                        Session("CurrencyVal"))
                    End If
                    For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                        If Vals(i) = 0 Then Exit For
                        Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) / Vals(i)
                    Next i
                End If

            Else
                For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                    Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) / Val(sDenominator)
                Next i
            End If
        End If



    End Sub



    'jrs 2005/11/30
    ' Function to Load Values into PLOT vector, and then display them using frm_TableDisplay
    ' parameters:
    ' 1. name of the variable.
    ' 2. index for 2nd dimension.
    ' 3. index for 3rd dimension
    ' 4. Boolean to indicate we're starting from column 0 (true), or we're appending more
    '    columns (false).
    ' 5. Boolean percentage of GDP.
    ' 6. Integer Row number to display
    Public Sub TableLoadVarAllYrs(ByVal sName As String, ByVal sDim1 As String, ByVal RegInd As Integer, ByVal sDim2 As String, ByVal sDim3 As String, ByVal newCols As Boolean, ByVal bPopPct As Boolean,
    ByVal bOpen As Boolean, ByVal bClose As Boolean, ByVal bPopPort As Boolean, ByVal bFormula As Boolean, ByVal sFormulaDet As String,
    ByVal f As Formula)
        'JRS 9/11/02
        Dim idx As Integer
        Dim idim2 As Integer
        Dim idx2 As Integer
        idx2 = 1
        idx = IfsVars.Item(sName).Index
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        If newCols Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            'JRS 2/17/03
            InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))
        End If
        Session("COUNTE") = Session("COUNTE") + 1


        If Not bFormula Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sName, idx, sDim1, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
            1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
            Else
                gLoadForDisplay(sName, idx, sDim1, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("isictgroup"),
                1, Session("plot"), Session("counte"), 1, Session("firstyear"), Session("lastyear"), 0, False, Session("bloadfordisplaysuccess"), Me, Session("runfiles"), Session("putfdrvrun"),
                Session("shortsce"), Session("filefordisplay"), Session("finyear"), Session("ifs_mdb2active"),
                Session("currencyval"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, sDim1, sDim2, sDim3, Session("IsICTGroup"), cboFiles.SelectedIndex, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        Else
            gLoadComp(Session("COUNTE"), sFormulaDet, sName, sDim1, "", "", "Percent",
            -1, Session("IsICTGroup"), cboFiles.SelectedIndex, False, False, f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
            False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
            Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
            Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
            FormatFormAfterLoad(cboFiles.SelectedIndex, Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
            IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
            Session("MaxFinYearHis"))
        End If

        Dim i As Integer
        If bPopPct Or bPopPort Then
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "/POP"
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "/POP"
            Session("GR4N")(Session("COUNTE")) = "Portion"
            If bPopPct Then
                Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "*100"
                Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "*100"
                Session("GR4N")(Session("COUNTE")) = "Percent"
            End If

            Dim POPs() As Single
            ReDim POPs(Session("LastYear") - Session("FirstYear") + 1)
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("POP", POPidx, sDim1, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
            1, POPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
            Else
                gLoadForDisplay("POP", POPidx, sDim1, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                1, POPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
            End If
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                'jrs 2010/07/13
                If POPs(i) = 0 Then Exit For
                Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) / POPs(i)
                If bPopPct Then
                    Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) * 100
                End If
            Next i
        End If

    End Sub




    'jrs 2005/11/30
    Private Sub LoadOverTime(ByVal sVar As String, ByVal sDim2 As String, ByVal sDim3 As String, ByVal bPopPct As Boolean, ByVal bPopPort As Boolean,
    ByVal bFormula As Boolean, ByVal sFormulaDet As String, ByVal f As Formula)
        'jrs 2010/07/26
        Dim iFirstSceSel As Integer
        'jrs 2010/07/28
        Dim iFirstCountrySel As Integer

        'Display over time
        With grd_TableDisplay
            'JRS 12/10/02 MULTIPLE FILES
            Dim iFileOld As Integer
            iFileOld = cboFiles.SelectedIndex
            'jrs 2010/07/28 Multiple Groups
            Dim iCountryOld As Integer
            iCountryOld = cmb_Country.SelectedIndex
            'jrs 2005/05/16
            cboFiles.Visible = False
            'jrs 2010/07/26
            iFirstSceSel = -1
            iFirstCountrySel = -1
            Dim iFileIdx As Integer, iCountryIdx As Integer
            For iFileIdx = 0 To cboFiles.Items.Count - 1
                'jrs 2010/07/26
                If lstFiles.Items(iFileIdx).Selected Or (lstFiles.SelectedIndex = -1 And iFileOld = iFileIdx) Then
                    If iFirstSceSel = -1 Then iFirstSceSel = iFileIdx
                    cboFiles.SelectedIndex = iFileIdx
                    'jrs 2010/07/28
                    If lstCountry.SelectedIndex = -1 Then
                        LoadVarAllYrs(sVar, cmb_Country.Text, cmb_Country.SelectedIndex + 1, sDim2, sDim3, (iFileIdx = iFirstSceSel), bPopPct,
                        True, True, bPopPort, bFormula, sFormulaDet, f)
                    Else
                        For iCountryIdx = 0 To cmb_Country.Items.Count - 1
                            If lstCountry.Items(iCountryIdx).Selected Then
                                If iFirstCountrySel = -1 Then iFirstCountrySel = iCountryIdx
                                LoadVarAllYrs(sVar, lstCountry.Items(iCountryIdx).Text, iCountryIdx + 1, sDim2, sDim3, (iFileIdx = iFirstSceSel And iCountryIdx = iFirstCountrySel), bPopPct,
                                True, True, bPopPort, bFormula, sFormulaDet, f)
                            End If
                        Next iCountryIdx
                    End If
                End If
            Next iFileIdx

            cboFiles.SelectedIndex = iFileOld
            'jrs 2005/05/16
            cboFiles.Visible = True
            Session("TableTitle") = .Rows(txtRow.Value).Cells(0).Text
            Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

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
    ' 5. Boolean percentage of GDP.
    ' 6. Integer Row number to display
    Public Sub LoadVarAllYrs(ByVal sName As String, ByVal sDim1 As String, ByVal RegInd As Integer, ByVal sDim2 As String, ByVal sDim3 As String, ByVal newCols As Boolean, ByVal bPopPct As Boolean,
    ByVal bOpen As Boolean, ByVal bClose As Boolean, ByVal bPopPort As Boolean, ByVal bFormula As Boolean, ByVal sFormulaDet As String,
    ByVal f As Formula)
        'JRS 9/11/02
        Dim idx As Integer
        Dim idim2 As Integer
        Dim idx2 As Integer
        idx2 = 1
        idx = IfsVars.Item(sName).Index
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        If newCols Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            'JRS 2/17/03
            InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))
        End If
        Session("COUNTE") = Session("COUNTE") + 1


        If Not bFormula Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sName, idx, sDim1, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
            1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
            Else
                gLoadForDisplay(sName, idx, sDim1, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, sDim1, sDim2, sDim3, Session("IsICTGroup"), cboFiles.SelectedIndex, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        Else
            gLoadComp(Session("COUNTE"), sFormulaDet, sName, sDim1, "", "", "Percent",
            -1, Session("IsICTGroup"), cboFiles.SelectedIndex, False, False, f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
            False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
            Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
            Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
            FormatFormAfterLoad(cboFiles.SelectedIndex, Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
            IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
            Session("MaxFinYearHis"))
        End If

        Dim i As Integer
        If bPopPct Or bPopPort Then
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "/POP"
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "/POP"
            Session("GR4N")(Session("COUNTE")) = "Portion"
            If bPopPct Then
                Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "*100"
                Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "*100"
                Session("GR4N")(Session("COUNTE")) = "Percent"
            End If

            Dim POPs() As Single
            ReDim POPs(Session("LastYear") - Session("FirstYear") + 1)
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("POP", POPidx, sDim1, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
            1, POPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
            Else
                gLoadForDisplay("POP", POPidx, sDim1, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("IsICTGroup"),
                1, POPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
            End If
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                'jrs 2010/07/13
                If POPs(i) = 0 Then Exit For
                Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) / POPs(i)
                If bPopPct Then
                    Session("PLOT")(i, Session("COUNTE")) = Session("PLOT")(i, Session("COUNTE")) * 100
                End If
            Next i
        End If

    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '                JAM 2017-02-27 Showing Graph
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub DisplayGraph(ByVal currRow As Integer)
        Dim sDevProFun As String = ""
        Dim iDevProFunIdx As Integer = 0

        Dim Vals(1) As Single
        Dim yr As Single
        Dim bGroup As Boolean
        Dim VFunVal As Single

        'jam 2023/12/05
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim vform As String
        Dim sLst As String




        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension




        sDevProFun = ""
        Select Case currRow
            Case 2
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Roads per Capita (KM) (2010 Forecast) Log"
            Case 3
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Paved Roads (% of total) (2010 Forecast) Log"
            Case 4
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Rural Access Index (% of rural pop) (2010 FC) Log"

            Case 6
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Generating Capacity per Capita (KW FC) 2010 Linear"
            Case 7
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Transmission Loss (%) (2010 Forecast) Log"
            Case 8
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access National (% Pop) (2010 FC) Log"
            Case 9
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access Urban (% Pop) (2010 Forecast) Log"
            Case 10
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access Rural (% of Pop) (2010 FC) Log"

            Case 12
                'jrs 2026/01/23 sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Access to Water-Piped (% of Pop) (2010 Forecast) Log"
                sDevProFun = "GDP/Capita (PPP 2021) Versus Access to Water-SafelyManaged (% of Pop) Forecast (2022) Log"
            Case 13
                'jrs 2026/01/23 sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Access to Water-Other Improved (% of Pop) (2010 FC) Log"
                sDevProFun = "GDP/Capita (PPP 2021) Versus Access to Sanitation-SafelyManaged(% of Pop) Forecast (2022) Log"
            'Case 14
            '    sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Access to Water-UnImproved (% of Pop) (2010 FC) Log"
            'Case 15
            '    sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Improved (% of Pop) (2010 FC) Log"
            'Case 16
            '    sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Shared (% of Pop) (2010 FC)  Log"
            'Case 17
            '    sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Unimproved (% of Pop) (2010 FC) Log"
            Case 14
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Wastewater Collect (% of Pop) (2010 Forecast) Log"
            Case 15
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Wastewater Treated (% of Pop) (2010 Forecast) Log"
            Case 22
                sDevProFun = "GDP/Capita (PPP) (2010) FC Versus ((Land Area for Irrigation/(CL + GL))/10) (2010) FC Log"

            Case 18
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Fixed Telephone Lines (per 100) (2010 Forecast) Log"
            Case 19
                sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Mobile Telephone Subscriptions (per 100) (2010 FC) Log"
            Case 20
                sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Fixed Broadband Subscriptions (per 100) (2010 FC) Linear"
            Case 21
                sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Mobile Broadband Subscriptions (per 100) (2010 FC) Linear"

            Case 23
                sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Traditional Infrastructure Index (2010 FC) Log"
            Case 24
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Modern Infrastructure Index (SE) (2010 Forecast) Log"
            Case 25
                sDevProFun = "GDP/Capita (PPP 2010 FC) Versus Modern Infrastructure Index (0-100) (2010 FC) Log"
            Case 26
                sDevProFun = "GDP/Capita (PPP 2010 Forecast) Versus Overall Infrastructure Index (2010) Log"

        End Select

        'JAM 2018-02-28
        yr = Cmb_Year.Text
        bGroup = Session("IsICTGroup")
        If currRow = 2 Then

            'VFunVal = CSng(grd_TableDisplay.Rows(currRow).Cells(1).Text)
            'JAM 2018-03-27
            VFunVal = CSng(Session("ComputedValue")(currRow))
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("GDP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
               bGroup, 1, Vals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
               Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
               Session("CurrencyVal"))
            Else

                gLoadForDisplay("GDP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
                   bGroup, 1, Vals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                   Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                   Session("CurrencyVal"))
            End If

            VFunVal = VFunVal / Vals(1)
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("POP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
                bGroup, 1, Vals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
            Else

                gLoadForDisplay("POP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
                    bGroup, 1, Vals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
            End If
            VFunVal = VFunVal * Vals(1) / 1000


        End If



        If sDevProFun > "" Then
            Session("sDevProFun") = sDevProFun
            iDevProFunIdx = SearchFunctionNameIdx(sDevProFun, 1, TFCount)
            Session("iDevProFunIdx") = iDevProFunIdx
            Session("DPGDPPCP") = CSng(txtIncome.Text)
            ''Dim posStart As Integer, posEnd As Integer
            ''posStart = InStr(grd_TableDisplay.Rows(currRow).Cells(1).Text, ">") + 1
            ''posEnd = InStr(posStart, grd_TableDisplay.Rows(currRow).Cells(1).Text, "<")
            ''Session("DPFunVal") = CSng(Mid(grd_TableDisplay.Rows(currRow).Cells(1).Text, posStart, posEnd - posStart))
            If currRow = 2 Then
                Session("DPFunVal") = VFunVal
            Else
                'Session("DPFunVal") = CSng(grd_TableDisplay.Rows(currRow).Cells(1).Text)
                'JAM 2018-03-27
                Session("DPFunVal") = CSng(Session("ComputedValue")(currRow))

            End If
            Session("RefreshGraph") = True
            Session("DevProReg") = cmb_Country.SelectedValue
            Session("DevProYr") = Cmb_Year.SelectedValue
            'jam 2023/12/05
            'Support.FormManager.Show(Me, "frm_FuncGraph.aspx")

            'jam 2023/12/05
            vform = ("frm_FuncGraph.aspx")

            vurl = FriendlyUrl.Href("~/" & vform)

            Response.Redirect(vurl)

        End If
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        If Session("IsICTGroup") = False Then
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
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '             JAM 2017-12-12    Loading General Values
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Subroutine to Load the Values
    'into Risk Profile Values table
    Private Sub LoadGralValues(ByVal bOnlyLoadGrid As Boolean)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -



        Dim dbW As String, TblW As DataTable

        Dim tblWidx As Integer = 0

        'jrs 2005/05/13
        Dim curRow As Integer
        curRow = 1 'jrs 2013/05/13 1

        ' Roads per Carpita
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Roads per capita (km.)</a>"
        Session("ChosenVariable") = "GDP/Capita (PPP 2010 Forecast) Versus Roads per Capita (KM) (2010 Forecast) Log"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(0), 2).ToString("0.00")



        'Paved Roads
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Paved Roads (% of total)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(1), 2).ToString("0.00")


        'Rural Access Index
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Rural Access Index (% of rural population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(2), 2).ToString("0.00")

        '''''''''Energy Subdivision 
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Energy"

        'Generating Capacity
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Generating Capacity per Capita (kw)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(3), 2).ToString("0.00")

        'Transmission Loss
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Transmission Loss (%)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(4), 2).ToString("0.00")


        'Electricity Access - National
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Electricity Access - National (% of population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(5), 2).ToString("0.00")

        'Electricity Access - Urban
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Electricity Access - Urban (% of population)</a>"
        'grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(6), 2).ToString("0.00")
        grd_TableDisplay.Rows(curRow).Cells(3).Text = StdDevs(6)


        'Electricity Access - Rural
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Electricity Access - Rural (% of population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(7), 2).ToString("0.00")

        '''''''''Water and Sanitation Subdivision
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Water and Sanitation"


        'Access to Water - Piped
        curRow = curRow + 1
        'jrs 2026/01/23 grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Access to Water - Piped (% of population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Access to Water - Safely Managed (% of population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(8), 2).ToString("0.00")

        'Access to Water - Other Improved
        curRow = curRow + 1
        'jrs 2026/01/23 grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Access to Water - Other Improved (% of population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Access to Sanitation - Safely Managed (% of population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(9), 2).ToString("0.00")

        ''Access to Water - Unimproved
        'curRow = curRow + 1
        'grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Access to Water - Unimproved (% of population)</a>"
        'grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(10), 2).ToString("0.00")


        ''Access to Sanitation - Improved
        'curRow = curRow + 1
        'grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Access to Sanitation - Improved (% of population)</a>"
        'grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(11), 2).ToString("0.00")

        ''Access to Sanitation - Shared
        'curRow = curRow + 1
        'grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Access to Sanitation - Shared (% of population)</a>"
        'grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(12), 2).ToString("0.00")


        ''Access to Sanitation - Unimproved
        'curRow = curRow + 1
        'grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Access to Sanitation - Unimproved (% of population)</a>"
        'grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(13), 2).ToString("0.00")

        'Wastewater Collected 
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Wastewater Collected (% of population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(10), 2).ToString("0.00")

        'Wastewater Treated 
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Wastewater Treated (% of population)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(11), 2).ToString("0.00")

        'Area Equipped for Irrigation (% of Crop and Grazing Land)
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Area Equipped for Irrigation (% of Crop and Grazing Land)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(12), 2).ToString("0.00")


        '''''''''ICT SubDivision
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "ICT"


        'Percentage of networked persons
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Percentage of networked persons</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(13), 2).ToString("0.00")

        'Mobile Telephone Subscriptions (per 100 persons)
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Mobile Telephone Subscriptions (per 100 persons)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(14), 2).ToString("0.00")


        'Fixed Broadbands Subscriptions (per 100 persons)
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Fixed Broadbands Subscriptions (per 100 persons)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(15), 2).ToString("0.00")


        'Mobile Broadbands Subscriptions (per 100 persons)
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Mobile Broadbands Subscriptions (per 100 persons)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(16), 2).ToString("0.00")

        '''''''''Infrastructure Indices SubDivision
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Infrastructure Indices"


        'Traditional (standard errors, World = 0 in 2010)
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Traditional (standard errors, World = 0 in 2010)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(17), 2).ToString("0.00")


        'Modern (standard errors, World = 0 in 2010)
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Modern (standard errors, World = 0 in 2010)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(18), 2).ToString("0.00")


        'Modern (0-100)
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Modern (0-100)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(19), 2).ToString("0.00")

        'Overall (standard errors, World = 0 in 2010)
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Overall (standard errors, World = 0 in 2010)</a>"
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Math.Round(StdDevs(20), 2).ToString("0.00")


    End Sub


    Private Function AggVal(ByVal rowIdx As Integer) As Single
        'Initialize Function
        AggVal = 0
        'Read Rank for each country
        Dim c As Integer, cIdx As Integer, TotPop As Single
        TotPop = 0
        For c = 1 To Session("gCountGroupMembers")
            'Find pointer to country
            cIdx = IFsRegs.Item(Session("gGroupMembrs")(c)).Index
            'Average with Population Weigths.
            AggVal = AggVal + Session("VarsInGrid")(rowIdx).VarValsGet(cIdx).value * Session("PopAll")(cIdx)
            'Total Population
            TotPop = TotPop + Session("PopAll")(cIdx)
        Next c
        If TotPop > 0 Then
            AggVal = Round(AggVal / TotPop, 3)
        Else
            AggVal = 0
        End If
    End Function

    Private Sub LoadAvgStd()
        With grd_TableDisplay
            Dim rr As Integer, i As Integer
            For rr = 2 To .Rows.Count - 3 'jrs 2013/05/13 used to start at 1
                If Session("VarsInGrid")(rr).VarName > "" Then
                    'Mean
                    Session("VarsInGrid")(rr).MeanYr1 = 0
                    For i = 1 To NR
                        Session("VarsInGrid")(rr).MeanYr1 = Session("VarsInGrid")(rr).MeanYr1 + Session("VarsInGrid")(rr).VarValsGet(i).value
                    Next i
                    Session("VarsInGrid")(rr).MeanYr1 = Session("VarsInGrid")(rr).MeanYr1 / NR 'jrs 2010/11/05 (NR * FinYear(1))
                    'Standard Deviation
                    Session("VarsInGrid")(rr).StdDevYr1 = 0
                    For i = 1 To NR
                        Session("VarsInGrid")(rr).StdDevYr1 = Session("VarsInGrid")(rr).StdDevYr1 + (Session("VarsInGrid")(rr).VarValsGet(i).value - Session("VarsInGrid")(rr).MeanYr1) ^ 2
                    Next i
                    Session("VarsInGrid")(rr).StdDevYr1 = (Session("VarsInGrid")(rr).StdDevYr1 / NR) ^ (1 / 2)
                End If
            Next rr
        End With
    End Sub

    Private Sub CreateMenu()
        wmPopUp.UserData = "jsolorza::976739975"
        'jam 2021/12/30
        'wmPopUp.ClearPixelImage = "blank.gif"
        'wmPopUp.PopupIcon = "./images/Popup.gif"
        'wmPopUp.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmPopUp.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmPopUp.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmPopUp.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmPopUp.SelectedPopupIcon = "./images/SelectedPopup.gif"


        'jam 2021/12/30
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")

        wmPopUp.ScrollEnabled = True
        AddHandler wmPopUp.ServerItemClick, AddressOf ItemClickHandlerContext
        wmPopUp.MenuBar.ShowOnMouseOver = True

        'jrs 2012/01/26
        wmPopUp.ButtonFont.Size = "12px"
        wmPopUp.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp.Groups.Add()

        cswmGroup1.ID = "mnuPerfRiskContext"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Graph"




        wmPopUp.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp.ItemFont.PaddingLeft = 4

        CreateMenu1()

        CreateMenu2()

        CreateMenu3()

        CreateMenu4()
    End Sub


    Private Sub CreateMenu1()
        wmPopUp1.UserData = "jsolorza::976739975"
        'jam 2021/12/30
        'wmPopUp1.ClearPixelImage = "blank.gif"
        'wmPopUp1.PopupIcon = "./images/Popup.gif"
        'wmPopUp1.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmPopUp1.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmPopUp1.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmPopUp1.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmPopUp1.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/12/30
        wmPopUp1.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
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

        cswmGroup1.ID = "mnuPerfRiskContext1"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Provide Information"

        wmPopUp1.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp1.ItemFont.PaddingLeft = 4

    End Sub

    Private Sub CreateMenu2()
        wmPopUp2.UserData = "jsolorza::976739975"
        'jam 2021/12/30
        'wmPopUp2.ClearPixelImage = "blank.gif"
        'wmPopUp2.PopupIcon = "./images/Popup.gif"
        'wmPopUp2.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmPopUp2.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmPopUp2.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmPopUp2.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmPopUp2.SelectedPopupIcon = "./images/SelectedPopup.gif"


        'jam 2021/12/30
        wmPopUp2.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        wmPopUp2.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmPopUp2.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmPopUp2.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmPopUp2.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmPopUp2.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmPopUp2.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")

        wmPopUp2.ScrollEnabled = True
        AddHandler wmPopUp2.ServerItemClick, AddressOf ItemClickHandlerContext2
        wmPopUp2.MenuBar.ShowOnMouseOver = True

        'jrs 2012/01/26
        wmPopUp2.ButtonFont.Size = "12px"
        wmPopUp2.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp2.Groups.Add()

        cswmGroup1.ID = "mnuPerfRiskContext2"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Over Time"


        wmPopUp2.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp2.ItemFont.PaddingLeft = 4

    End Sub

    Private Sub CreateMenu4()
        wmPopUp4.UserData = "jsolorza::976739975"
        'jam 2021/12/30
        'wmPopUp4.ClearPixelImage = "blank.gif"
        'wmPopUp4.PopupIcon = "./images/Popup.gif"
        'wmPopUp4.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmPopUp4.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmPopUp4.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmPopUp4.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmPopUp4.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/12/30
        wmPopUp4.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        wmPopUp4.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmPopUp4.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmPopUp4.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmPopUp4.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmPopUp4.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmPopUp4.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")

        wmPopUp4.ScrollEnabled = True
        AddHandler wmPopUp4.ServerItemClick, AddressOf ItemClickHandlerContext2
        wmPopUp4.MenuBar.ShowOnMouseOver = True

        'jrs 2012/01/26
        wmPopUp4.ButtonFont.Size = "12px"
        wmPopUp4.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp4.Groups.Add()

        cswmGroup1.ID = "mnuPerfRiskContext4"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Over Time"

        wmPopUp4.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp4.ItemFont.PaddingLeft = 4

    End Sub

    Private Sub CreateMenu3()
        wmPopUp3.UserData = "jsolorza::976739975"
        'jam 2021/12/30
        'wmPopUp3.ClearPixelImage = "blank.gif"
        'wmPopUp3.PopupIcon = "./images/Popup.gif"
        'wmPopUp3.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmPopUp3.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmPopUp3.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmPopUp3.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmPopUp3.SelectedPopupIcon = "./images/SelectedPopup.gif"


        'jam 2021/12/30
        wmPopUp3.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        wmPopUp3.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmPopUp3.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmPopUp3.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmPopUp3.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmPopUp3.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmPopUp3.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")

        wmPopUp3.ScrollEnabled = True
        AddHandler wmPopUp3.ServerItemClick, AddressOf ItemClickHandlerContext3
        wmPopUp3.MenuBar.ShowOnMouseOver = True

        'jrs 2012/01/26
        wmPopUp3.ButtonFont.Size = "12px"
        wmPopUp3.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp3.Groups.Add()

        cswmGroup1.ID = "mnuPerfRiskContext3"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display all country names by ranking"

        wmPopUp3.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp3.ItemFont.PaddingLeft = 4

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
        If Session("IsICTGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        Session("TableDisplayParent") = "frm_ICTProfile.aspx?" & sPar
        Select Case objEA.Item.Caption
            Case "Graph"
                DisplayGraph(currRow)



        End Select
    End Sub

    Private Sub mnuPopupCellInformation_Click(ByVal currRow As Integer)
        Dim msg As String

        'jam 2021/12/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If Session("bLastRow") Then
            'Display Information
            'frm_ICTProfileInfo.Caption = "Performance Risk Analysis - Summary Measure"
            msg = "Higher Risk Index number is riskier; smaller Value number is riskier. <br><br>" & Session("VarsInGrid")(grd_TableDisplay.Rows.Count - 1).Description
        Else
            ''msg = DisplayInformation(currRow)
            ''msg = msg & DisplayAnnotation(currRow)
            ''msg = msg & DisplayLiterature(currRow)
        End If

        LoadMessage(msg, Session("Message"))
        'jam 2021/12/30
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


        vurl = FriendlyUrl.Href("~/frm_Message")

        Response.Redirect(vurl)


    End Sub



    Sub ItemClickHandlerContext2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        Dim currRow As Integer
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        currRow = Request.Form("txtRow")
        Session("bLastRow") = (currRow = grd_TableDisplay.Rows.Count - 1)
        Dim sPar As String
        If Session("IsICTGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        Session("TableDisplayParent") = "frm_ICTProfile.aspx?" & sPar
        Select Case objEA.Item.Caption
            Case "Show Over Time"
                If Request.Form("txtCol") = 1 Then
                    'DisplayOverTime(currRow)
                    TableDisplayOverTime(currRow)
                End If


        End Select
    End Sub

    Sub ItemClickHandlerContext3(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        Dim currRow As Integer
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        currRow = Request.Form("txtRow")
        Session("bLastRow") = (currRow = grd_TableDisplay.Rows.Count - 1)
        Dim sPar As String
        If Session("IsICTGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        Session("TableDisplayParent") = "frm_ICTProfile.aspx?" & sPar
        Select Case objEA.Item.Caption
            Case "Display all country names by ranking"

        End Select
    End Sub


    Private Sub LoadAllOverTime(ByVal sVar As String, ByVal sDim2 As String, ByVal sDim3 As String, ByVal bPopPct As Boolean, ByVal bAsc As Boolean,
    ByVal bPopPort As Boolean, ByVal bFormula As Boolean, ByVal sFormulaDet As String, ByVal f As Formula)
        'Display all countries over time by Ranking
        With grd_TableDisplay
            Dim rr As Integer
            For rr = 1 To NR
                LoadVarAllYrs(sVar, GRPS(1, rr), rr, sDim2, sDim3, (rr = 1), bPopPct, rr = 1, rr = NR, bPopPort,
                bFormula, sFormulaDet, f)
            Next rr

            'Set Ranking Year
            Session("RankDescOrd") = Not bAsc
            Session("Rank_Year") = Val(Cmb_Year.Text)
            'jrs 2013/05/14
            If Val(Cmb_Year.Text) > Session("DisplayLastYear") Then
                Session("DisplayLastYear") = Val(Cmb_Year.Text)
            End If
            Session("PerfRiskAction") = "Rank"
            Session("PerfRiskCountry") = cmb_Country.SelectedValue
            Session("PerfRiskYear") = Cmb_Year.SelectedValue
            Session("TableTitle") = .Rows(txtRow.Value).Cells(0).Text

            'jrs 2010/02/04
            Dim myColor As Object
            If txtRow.Value < grd_TableDisplay.Rows.Count - 1 Then
                myColor = .Rows(txtRow.Value).Cells(1).BackColor
            Else
                myColor = .Rows(txtRow.Value).Cells(4).BackColor
            End If
            Session("PerfRiskColor") = myColor

            Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
        End With
    End Sub

    Private Sub LoadCountryNames(ByVal VarVals() As DisplayColumn, ByVal sTitle As String, ByVal RankIdx As Integer, ByVal myColor As Object)
        'jam 2021/12/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        Session("Table.Caption") = sTitle & " Ranked"
        Session("Table.cols") = 2
        Session("Table.rows") = NR
        Session("Table.FixedCols") = 1
        Session("Table.FixedRows") = 0

        ReDim Session("Table.Array")(NR, 2)

        Dim rr As Integer
        For rr = 1 To NR
            Session("Table.Array")(VarVals(rr).Index2 - 1, 0) = VarVals(rr).Index
            Session("Table.Array")(VarVals(rr).Index2 - 1, 1) = GRPS(1, rr)

            If VarVals(rr).Index2 = RankIdx Then
                Session("Table.RowSel") = VarVals(rr).Index2 - 1
                Session("Table.CellBackColor") = myColor
            End If
        Next rr

        'jam 2021/12/30
        ' Support.FormManager.Show(Me, "frm_Table.aspx")


        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Table.aspx")


        vurl = FriendlyUrl.Href("~/frm_Table")

        Response.Redirect(vurl)

    End Sub



    Private Sub mnuAbsolute_Click()
        Session("bAbsolute") = Not Session("bAbsolute")
        With grd_TableDisplay
            '.row = 0
            .Visible = False
            If Session("bAbsolute") Then
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuDispOptAbs").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/30"checkbox.gif"
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuDispOptRel").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/30 "blank.gif"
                wmPopUp.Groups(0).Items("mnuShowCount").Enabled = True
                'jrs 2013/05/13
                .Rows(0).Cells(2).Text = "Evaluation: Absolute"
                .Rows(1).Cells(9).Text = "Standard Deviation"
            Else
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuDispOptAbs").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/30 "blank.gif"
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuDispOptRel").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/30 "checkbox.gif"
                wmPopUp.Groups(0).Items("mnuShowCount").Enabled = False
                'jrs 2013/05/13
                .Rows(0).Cells(2).Text = "Evaluation: Relative"
                .Rows(1).Cells(9).Text = "Standard Error"
            End If
        End With



        grd_TableDisplay.Visible = True

    End Sub

    Protected Sub cmdSce_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSce.Click
        'jam 2021/12/30
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Dim i As Integer, ChgIdx As Integer = 0
        If lstFiles.Visible Then
            Session("lstFiles.SelCount") = 0
            Session("lstCountry.SelCount") = 0
            For i = 0 To lstFiles.Items.Count - 1
                If lstFiles.Items(i).Selected Then
                    ReDim Preserve Session("RiskFiles")(Session("lstFiles.SelCount"))
                    Session("RiskFiles")(Session("lstFiles.SelCount")) = i

                    Session("lstFiles.SelCount") = Session("lstFiles.SelCount") + 1
                    If Session("lstFiles.SelCount") = 1 Then
                        ChgIdx = i
                    End If
                End If
            Next
            If Not lstFiles.Items(cboFiles.SelectedIndex).Selected Then
                cboFiles.SelectedIndex = ChgIdx

                Session("ICTFilesIdx") = cboFiles.SelectedIndex
                ''LoadAllCtryValues(False)
                LoadValues()
            End If

            lstFiles.Visible = False
            cboFiles.Visible = True
        Else
            Session("lstFiles.SelCount") = 0
            Session("lstCountry.SelCount") = 0
            For i = 0 To lstCountry.Items.Count - 1
                If lstCountry.Items(i).Selected Then
                    ReDim Preserve Session("ICTRegs")(Session("lstCountry.SelCount"))
                    Session("ICTRegs")(Session("lstCountry.SelCount")) = i
                    Session("lstCountry.SelCount") = Session("lstCountry.SelCount") + 1
                    If Session("lstCountry.SelCount") = 1 Then
                        ChgIdx = i
                    End If

                End If
            Next
            If Not lstCountry.Items(cmb_Country.SelectedIndex).Selected Then
                cmb_Country.SelectedIndex = ChgIdx
                Session("ICTcmbCountryIdx") = cmb_Country.SelectedIndex
                'LoadValues()
                If Session("IsICTGroup") = True Then
                    'jam 2021/12/30
                    'Support.FormManager.Show(Me, "frm_ICTProfile.aspx?Group=" & cmb_Country.SelectedValue)

                    Vpath = Request.GetFriendlyUrlFileVirtualPath

                    Vext = Request.GetFriendlyUrlFileExtension

                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_ICTProfile.aspx")


                    vurl = FriendlyUrl.Href("~/frm_ICTProfile", cmb_Country.SelectedValue)

                    Response.Redirect(vurl)

                Else
                    'jam 2021/12/30
                    ' Support.FormManager.Show(Me, "frm_ICTProfile.aspx?Country=" & IFsRegs.Item(cmb_Country.SelectedValue).ISO3166_2Code)


                    Vpath = Request.GetFriendlyUrlFileVirtualPath

                    Vext = Request.GetFriendlyUrlFileExtension

                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_ICTProfile.aspx")


                    vurl = FriendlyUrl.Href("~/frm_ICTProfile", IFsRegs.Item(cmb_Country.SelectedValue).ISO3166_2Code)

                    Response.Redirect(vurl)

                End If

            End If

            lstCountry.Visible = False
            cmb_Country.Visible = True
        End If

        cmdSce.Visible = False
        cmdSceClear.Visible = False
        grd_TableDisplay.Visible = True
    End Sub

    Protected Sub cmdSceClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSceClear.Click
        Dim i As Integer
        If lstFiles.Visible Then
            Session("lstFiles.SelCount") = 0
            For i = 0 To lstFiles.Items.Count - 1
                lstFiles.Items(i).Selected = False
            Next i
            lstFiles.Visible = False
            cboFiles.Visible = True
        Else
            Session("lstCountry.SelCount") = 0
            For i = 0 To lstCountry.Items.Count - 1
                lstCountry.Items(i).Selected = False
            Next i
            lstCountry.Visible = False
            cmb_Country.Visible = True
        End If

        cmdSce.Visible = False
        cmdSceClear.Visible = False
        grd_TableDisplay.Visible = True
    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '  JAM 2017-09-12 Retrieving Standard Deviation for ICT variables
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub LoadStdDevs()

        Dim sTbl As String
        sTbl = Session("PutFdrvRun") & "TablFunc.mdb"
        Dim Tbl As DataTable
        SetDataTablefromAccess(sTbl, "SELECT * FROM TablFunc", "Function Name", Tbl)

        StdDevs(0) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Roads per Capita (KM) (2010 Forecast) Log", "StdDev")
        StdDevs(0) = StdDevs(0) * 1000

        StdDevs(1) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Paved Roads (% of total) (2010 Forecast) Log", "StdDev")

        StdDevs(2) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Rural Access Index (% of rural pop) (2010 FC) Log", "StdDev")

        StdDevs(3) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Generating Capacity per Capita (KW FC) 2010 Linear", "StdDev")

        StdDevs(4) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Transmission Loss (%) (2010 Forecast) Log", "StdDev")

        StdDevs(5) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access National (% Pop) (2010 FC) Log", "StdDev")

        StdDevs(6) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access Urban (% Pop) (2010 Forecast) Log", "StdDev")

        StdDevs(7) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Electricity Access Rural (% of Pop) (2010 FC) Log", "StdDev")

        'jrs 2026/01/23 *** start ***
        Dim idx As Integer = 8
        'StdDevs(8) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Access to Water-Piped (% of Pop) (2010 Forecast) Log", "StdDev")
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2021) Versus Access to Water-SafelyManaged (% of Pop) Forecast (2022) Log", "StdDev")

        'StdDevs(9) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Access to Water-Other Improved (% of Pop) (2010 FC) Log", "StdDev")
        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2021) Versus Access to Sanitation-SafelyManaged(% of Pop) Forecast (2022) Log", "StdDev")

        'StdDevs(10) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Access to Water-UnImproved (% of Pop) (2010 FC) Log", "StdDev")

        'StdDevs(11) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Improved (% of Pop) (2010 FC) Log", "StdDev")

        'StdDevs(12) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Shared (% of Pop) (2010 FC)  Log", "StdDev")

        'StdDevs(13) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Access to Sanitation-Unimproved (% of Pop) (2010 FC) Log", "StdDev")
        'jrs 2026/01/23 *** end ***

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Wastewater Collect (% of Pop) (2010 Forecast) Log", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Wastewater Treated (% of Pop) (2010 Forecast) Log", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP) (2010) FC Versus ((Land Area for Irrigation/(CL + GL))/10) (2010) FC Log", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Fixed Telephone Lines (per 100) (2010 Forecast) Log", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Mobile Telephone Subscriptions (per 100) (2010 FC) Log", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Fixed Broadband Subscriptions (per 100) (2010 FC) Linear", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Mobile Broadband Subscriptions (per 100) (2010 FC) Linear", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Traditional Infrastructure Index (2010 FC) Log", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Modern Infrastructure Index (SE) (2010 Forecast) Log", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 FC) Versus Modern Infrastructure Index (0-100) (2010 FC) Log", "StdDev")

        idx = idx + 1
        StdDevs(idx) = SeekValue(Tbl, "GDP/Capita (PPP 2010 Forecast) Versus Overall Infrastructure Index (2010) Log", "StdDev")




        Session("StdDevs") = StdDevs

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '                 JAM 2018-02-11 Loading Values for 1st, 2nd and 4th columns
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



    Private Sub LoadValuesVar(ByVal vrow As Integer, ByVal reg As Integer, ByVal yr As Integer, ByVal sVar As String,
    ByVal iStdDevIdx As Integer, ByVal sTblFunName As String, ByVal sLineLabel As String,
    ByVal iv As Single, ByVal sSecondDim As String, ByVal sThirdDim As String,
    Optional ByVal bCompareCountry As Boolean = False)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim VarIdx As Integer
        Dim VarVal As Single
        Dim VarVals(1) As Single
        VarIdx = IfsVars.Item(sVar).Index
        'jrs 2005/05/19
        If VarIdx = -1 Then Exit Sub
        'jrs 2006/07/04
        Dim bGrp As Boolean = Session("IsICTGroup")

        'jrs 2013/12/10
        '' If Not bCompareCountry Then
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, True, True,
        bGrp, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
        Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
        Session("CurrencyVal"))
        Else

            gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, True, True,
            bGrp, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
        End If
        ''Else
        ''Dim yrc As Integer
        ''yrc = Val(Cmb_Year_Compare.Text)
        ''gLoadForDisplay(sVar, VarIdx, cmb_Country_Compare.SelectedValue, reg, sSecondDim, -1, sThirdDim, -1, "", cboFiles.SelectedIndex, True, True, _
        ''bGrp, 1, VarVals, 1, 1, yrc, yrc, yrc - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), _
        ''Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), _
        ''Session("CurrencyVal"))
        ''End If



        'grd_TableDisplay.Rows(vrow).Cells(1).Text = Math.Round(VarVals(1), 2).ToString("0.00")
        grd_TableDisplay.Rows(vrow).Cells(1).Text = VarVals(1)
        If iv > 0 Then
            XYTABL(sTblFunName, iv, VarVal, Session("TFuncPoints"), Session("XPoints"), Session("YPoints"), "", Me, Session("APoint"), Session("BPoints"))
            'grd_TableDisplay.Rows(vrow).Cells(2).Text = Math.Round(VarVal, 2).ToString("0.00")
            grd_TableDisplay.Rows(vrow).Cells(2).Text = VarVal
        End If

        If sVar = "HLYLDWORK" Then
            grd_TableDisplay.Rows(vrow).Cells(3).Text = 0
            If grd_TableDisplay.Rows(vrow).Cells(2).Text > "0" Then
                VarVals(1) = Math.Round(VarVals(1) / Val(grd_TableDisplay.Rows(vrow).Cells(2).Text), 4)
                grd_TableDisplay.Rows(vrow).Cells(4).Text = VarVals(1)
            End If
        Else
            'grd_TableDisplay.Rows(vrow).Cells(4).Text = StdDevs(iStdDevIdx)
            grd_TableDisplay.Rows(vrow).Cells(4).Text = grd_TableDisplay.Rows(vrow).Cells(3).Text
        End If

    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '                 JAM 2018-02-11   Calculating 4th colum
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub StdDevCalc()
        'Recalculate Std Dev


        Dim i As Integer
        Dim V As Single, f As Single
        For i = 1 To grd_TableDisplay.Rows.Count - 1
            If i <> 1 And i <> 5 And i <> 11 And i <> 17 And i <> 22 And i <= 26 Then
                If IsNumeric(grd_TableDisplay.Rows(i).Cells(1).Text) Then
                    V = CSng(grd_TableDisplay.Rows(i).Cells(1).Text)
                    'jrs 2013/12/02
                    If IsNumeric(grd_TableDisplay.Rows(i).Cells(2).Text) Then
                        f = CSng(grd_TableDisplay.Rows(i).Cells(2).Text)
                        If grd_TableDisplay.Rows(i).Cells(4).Text <> 0 Then
                            'grd_TableDisplay.Rows(i).Cells(4).Text = (V - f) / CSng(grd_TableDisplay.Rows(i).Cells(4).Text)
                            grd_TableDisplay.Rows(i).Cells(4).Text = (V - f) / CSng(grd_TableDisplay.Rows(i).Cells(4).Text)
                        End If
                    End If
                End If
            End If
        Next i

    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '                 JAM 2018-02-27   Rounding Matrix
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub RoundMatrix()


        Dim i As Integer
        Dim V As Single, f As Single
        ReDim Session("ComputedValue")(50)
        For i = 1 To grd_TableDisplay.Rows.Count - 1
            If i <> 1 And i <> 5 And i <> 11 And i <> 17 And i <> 22 And i <= 26 Then
                If IsNumeric(grd_TableDisplay.Rows(i).Cells(1).Text) Then
                    'grd_TableDisplay.Rows(i).Cells(1).Text = Math.Round(Val(grd_TableDisplay.Rows(i).Cells(1).Text), 2).ToString("0.00")
                    'grd_TableDisplay.Rows(i).Cells(1).Text = TotalRound(Val(grd_TableDisplay.Rows(i).Cells(1).Text)).ToString

                    'JAM 2018-03-27 Adding Table Form hyperlink 
                    Session("ComputedValue")(i) = TotalRound(Val(grd_TableDisplay.Rows(i).Cells(1).Text)).ToString
                    grd_TableDisplay.Rows(i).Cells(1).Text = "<a href=""javascript:onPointClick2(" & CStr(i) & ", 1)"">" & TotalRound(Val(grd_TableDisplay.Rows(i).Cells(1).Text)).ToString & "</a>"



                End If

                If IsNumeric(grd_TableDisplay.Rows(i).Cells(2).Text) Then
                    'grd_TableDisplay.Rows(i).Cells(2).Text = Math.Round(Val(grd_TableDisplay.Rows(i).Cells(2).Text), 2).ToString("0.00")
                    grd_TableDisplay.Rows(i).Cells(2).Text = TotalRound(Val(grd_TableDisplay.Rows(i).Cells(2).Text)).ToString
                End If

                If IsNumeric(grd_TableDisplay.Rows(i).Cells(3).Text) Then
                    'grd_TableDisplay.Rows(i).Cells(2).Text = Math.Round(Val(grd_TableDisplay.Rows(i).Cells(2).Text), 2).ToString("0.00")
                    grd_TableDisplay.Rows(i).Cells(3).Text = TotalRound(Val(grd_TableDisplay.Rows(i).Cells(3).Text)).ToString
                End If


                If IsNumeric(grd_TableDisplay.Rows(i).Cells(4).Text) Then
                    'grd_TableDisplay.Rows(i).Cells(4).Text = Math.Round(Val(grd_TableDisplay.Rows(i).Cells(4).Text), 4).ToString("0.0000")
                    grd_TableDisplay.Rows(i).Cells(4).Text = TotalRound(Val(grd_TableDisplay.Rows(i).Cells(4).Text)).ToString
                End If

            End If
        Next i

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '                 JAM 2018-03-14   Rounding each value
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function TotalRound(ByVal vdata As Single) As Single
        Dim RoundedValue As Single

        Select Case Abs(Val(vdata))
            Case Is > 1000
                RoundedValue = Round(Val(vdata), 0)
            Case Is > 100
                RoundedValue = Round(Val(vdata), 1)
            Case Is > 10
                RoundedValue = Round(Val(vdata), 2)
            Case Is > 1
                RoundedValue = Round(Val(vdata), 3)
            Case Is >= 0
                RoundedValue = Round(Val(vdata), 4)
        End Select

        TotalRound = RoundedValue

    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/12/30
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter">URL Parameters must be transform</param>
    ''' <returns>Parameters transformed in a valid URL format</returns>
    Public Function ICTPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If


        ICTPrepareForFriendlyURL = tmp
    End Function

End Class


