'jrs 2008/09/12

Option Strict Off
Option Explicit On
'JAM 2020-06-06
Imports System.Data.SQLite
'jam 2021/02/02
Imports Microsoft.AspNet.FriendlyUrls
Imports NLog.Time

Partial Class frm_MortCohorts
    Inherits Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
        End If
        'jrs 2011/05/02 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")

    End Sub

    Dim TableName(5) As String
    Dim FieldName(5, 6) As String
    Dim Case_of_Work As Short
    Dim CaseName As String

    Dim CommDis As New MortType
    Dim NonCommDis As New MortType
    Dim Injuries As New MortType
    'dsb 2026/06/11
    Dim AllMortSubTypes As New MortType

    'Weighted Average for PopulationNot Session("mnuSQLite")
    Dim POPMales(0, 0) As Single
    Dim POPFemales(0, 0) As Single
    'jrs 2008/09/08
    Dim sDefGrp As String
    Dim sDefReg As String
    'JAM 2020-06-06
    Dim vSQLiteCon As SQLiteConnection
    Dim vSQLiteCommand As SQLiteCommand
    Dim vSQLiteCommand2 As SQLiteCommand
    Dim vSQLiteReader As SQLiteDataReader
    Dim vSQLiteReader2 As SQLiteDataReader
    Dim vConnString As String
    'jam 2026/01/23 Declare Maximum cohort
    Dim MaxCohortGroup As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'jrs 2008/09/08
        sDefGrp = "World" 'jrs 2014/02/21 "UNESCO S&W Asia"
        sDefReg = "Afghanistan"

        Dim l As Short

        'jam 2021/02/07
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim othYear As String
        Dim othFile As String
        Dim qtyPAram As Integer


        'jam 2021/02/04
        If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Request.QueryString("Year")) Or Not IsNothing(Request.QueryString("File")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                'jrs 2024/04/23
                If Not IsNothing(Request.QueryString("Group")) And Not Page.IsPostBack Then
                    othCountry = Request.QueryString("Group")
                Else
                    othCountry = ""
                End If

            End If

            If Not IsNothing(Request.QueryString("Year")) And Not Page.IsPostBack Then

                othYear = Request.QueryString("Year")

            Else
                othYear = ""
            End If

            If Not IsNothing(Request.QueryString("File")) And Not Page.IsPostBack Then

                othFile = Request.QueryString("File")

            Else
                othFile = ""
            End If



            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")


            If othCountry <> "" And othYear <> "" And othFile <> "" Then
                vurl = FriendlyUrl.Href("~/frm_MortCohorts", MortPrepareForFriendlyURL(othCountry), othYear, othFile)

                Response.Redirect(vurl)

            Else
                If othCountry <> "" And othYear = "" And othFile = "" Then
                    vurl = FriendlyUrl.Href("~/frm_MortCohorts", MortPrepareForFriendlyURL(othCountry))

                    Response.Redirect(vurl)

                Else
                    If othCountry <> "" And othYear <> "" And othFile = "" Then
                        vurl = FriendlyUrl.Href("~/frm_MortCohorts", MortPrepareForFriendlyURL(othCountry), othYear)

                        Response.Redirect(vurl)
                    End If

                End If


            End If


        End If



        '--------------------------------------------------------------
        '   Initializing the table names and field names
        '   --------------------------------------------
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            TableName(1) = "MorDetBaseCase"
            TableName(2) = "PopBaseCase"
            TableName(3) = "MorBaseCase"

            FieldName(1, 1) = "MortPerMalesComDis"
            FieldName(1, 2) = "MortPerFeMalesComDis"
            FieldName(1, 3) = "MortPerMalesNonComDis"
            FieldName(1, 4) = "MortPerFeMalesNonComDis"
            FieldName(1, 5) = "MortPerMalesInjuries"
            FieldName(1, 6) = "MortPerFeMalesInjuries"

            FieldName(2, 1) = "PopulationMales"
            FieldName(2, 2) = "PopulationFemales"

            FieldName(3, 1) = "MortalityMales"
            FieldName(3, 2) = "MortalityFemales"

        Else
            TableName(1) = "Mor_Det_Base_Case"
            TableName(2) = "Pop_Base_Case"
            TableName(3) = "Mor_Base_Case"

            FieldName(1, 1) = "MortPerMalesComDis"
            FieldName(1, 2) = "MortPerFeMalesComDis"
            FieldName(1, 3) = "MortPerMalesNonComDis"
            FieldName(1, 4) = "MortPerFeMalesNonComDis"
            FieldName(1, 5) = "MortPerMalesInjuries"
            FieldName(1, 6) = "MortPerFeMalesInjuries"

            FieldName(2, 1) = "PopulationMales"
            FieldName(2, 2) = "PopulationFemales"

            FieldName(3, 1) = "MortalityMales"
            FieldName(3, 2) = "MortalityFemales"
        End If

        'JAM 2020-06-08
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                'JAM 2020-05-04
                If Not Session("SQLiteDB") Is Nothing Then
                    vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                Else
                    'jrs 2024/04/24
                    vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                End If

                'jam 2025/05/31 Changing connection mode
                'vSQLiteCon = New SQLiteConnection(vConnString) '& "New=true;"
                'vSQLiteCon.Open()


                'jam 2025/05/31 Changing connection mode


                vSQLiteCon = OpenJournalSQLite(vConnString)



            End If
        End If

        'jam 2021/02/06
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String
        Dim vYear As String
        Dim vScenario As String

        If segments.Count = 1 Then

            vCountry = segments(0)
            vYear = Nothing
            vScenario = Nothing

        ElseIf segments.Count = 2 Then
            'jam 2024/08/21
            'jrs 2024/07/08
            'If segments(0) = "Images" Then
            '    vCountry = Session("vCountry")
            'Else
            vCountry = segments(0)
            'End If
            'jrs 2024/04/23
            If IsNumeric(segments(1)) Then
                vYear = segments(1)
                vScenario = Nothing
            Else
                vYear = Nothing
                'jrs 2024/07/08
                If segments(1) = "pcl.jpg" Then
                    vScenario = Session("vScenario")
                Else
                    vScenario = segments(1)
                End If
            End If
        ElseIf segments.Count = 3 Then
            vCountry = segments(0)
            vYear = segments(1)
            vScenario = segments(2)

        Else
            vCountry = segments(0)
            Session("vCountry") = vCountry
            vYear = Nothing
            vScenario = Nothing
        End If

        Session("vCountry") = vCountry
        Session("vYear") = vYear
        Session("vScenario") = vScenario



        'JAM 2022/07/17
        If IsNothing(Session("cnvtoDeaths")) Then

            'jrs 2026/05/08 Session("cnvtoDeaths") = False            
            Session("cnvtoDeaths") = True

        End If




        If Not Page.IsPostBack Then

            If Session("POPCG_Case_of_Work") Is Nothing Then
                Case_of_Work = 0
                'jrs 2008/09/16
                Session("POPCG_Case_of_Work") = Case_of_Work
            Else
                Case_of_Work = Session("POPCG_Case_of_Work")
            End If


            If Session("EdCG_Casename") Is Nothing Then
                'jrs 2024/04/24
                CaseName = "Work Case"

                Session("EdCG_Casename") = CaseName
            Else
                CaseName = Session("EdCG_Casename")
            End If


            Dim TempDatabase(NCohorts, 8) 'As Single 'has to be session var
            Dim CountryGroup(250) As String
            Dim RegionIdGroup(250) As Short

            TempDatabase = Session("EC_TempDatabase")  '(NCohorts, 2)
            Session("CountryGroup") = CountryGroup
            Session("RegionIdGroup") = RegionIdGroup

            'jrs 2024/09/06 changed from = Nothing to is Nothing
            'mti WebForm Changes From June 21, 2004 -end
            'jrs 2011/05/05
            If Session("EdCohort_Grouping") Is Nothing Then
                Session("EdCohort_Grouping") = False
                Session("EdCohort_tempregion") = 1
                Session("EdCohort_regionname") = GRPS(1, 1)

                'jam 2021/03/06
                Session("IsGroup") = False

            End If

            Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
            Session("PopCohort_year_run") = Session("LastYear")

            'jrs 2014/02/19
            ReDim Session("dbSCEs")(3)
            ReDim Session("year_runs")(3)
            ReDim Session("tempyears")(3)
            ReDim Session("FirstYearSces")(3)
            ReDim Session("CaseNames")(3)
            For l = 0 To 3
                Session("dbSCEs")(l) = Session("db2Path")
                Session("CaseNames")(l) = CaseName
            Next l

            '   Initializing the graph with population graph for First Region, Session("FirstYear")
            '   ------------------------------------------------------------------------
            If Session("EdCohort_tempyear") Is Nothing Then
                Session("EdCohort_tempyear") = AMAX(FirstYearForecast, 1995)
            End If

            If Session("EdCohort_GraphType") Is Nothing Then
                Session("EdCohort_GraphType") = 0
            End If

            '   Code which takes the region names from GRPS$ vector
            '   ---------------------------------------------------
            cntrylist.Items.Clear()
            For l = 1 To NR
                cntrylist.Items.Add(New ListItem(GRPS(1, l)))
            Next l
            '   Code which takes the group names from Groupnames vector
            '   ---------------------------------------------------
            lstGroups.Items.Clear()

            'jrs 2024/09/06 *** start ***
            If Session("bCheckHideNonCoreGroups") Is Nothing Then
                Session("bCheckHideNonCoreGroups") = bCoreGroupCheck 'cdv 2021/09/01
            End If

            If Session("bCheckHideNonCoreGroups") Then
                For l = 1 To CGCount
                    lstGroups.Items.Add(New ListItem(CoreGroupNames(l)))
                Next l
            Else
                For l = 1 To GCount
                    lstGroups.Items.Add(New ListItem(GroupNames(l)))
                Next l
            End If
            'jrs 2024/09/06 *** end ***

            lstYear.Items.Clear()
            'jrs 2012/08/27
            cmbYr1.Items.Clear()
            cmbYr2.Items.Clear()
            cmbYr3.Items.Clear()
            cmbYr4.Items.Clear()
            'jrs 2024/07/08
            If Session("bCompare") = Nothing Then
                Session("bCompare") = False
            End If

            For l = Session("FirstYear") To Session("LastYear") 'GCount
                lstYear.Items.Add(New ListItem(l.ToString))
                'jrs 2012/08/27
                cmbYr1.Items.Add(New ListItem(l.ToString))
                cmbYr2.Items.Add(New ListItem(l.ToString))
                cmbYr3.Items.Add(New ListItem(l.ToString))
                cmbYr4.Items.Add(New ListItem(l.ToString))
            Next l
            cmbYr1.SelectedIndex = 0
            If cmbYr2.Items.Count > 5 Then
                cmbYr2.SelectedIndex = 5
            End If
            If cmbYr3.Items.Count > 10 Then
                cmbYr3.SelectedIndex = 10
            End If
            If cmbYr4.Items.Count > 15 Then
                cmbYr4.SelectedIndex = 15
            End If

            Session("AboutParent") = "frm_MortCohorts.aspx"

            'jrs 2013/08/19 added if Nothing
            If Session("bShow100") = Nothing Then
                Session("bShow100") = False
            End If

            'Load Mortality Types
            '---------------------

            'Communicable Diseases
            CommDis.setNumSubTypes(10)
            CommDis.sTitle = "Communicable Diseases"
            CommDis.setSubTypeFieldName("ComDisNoAIDSMales", 1)
            CommDis.setSubTypeName("Other Communicable Diseases", 1)
            '********* READ FOR COLORS ******************************************'
            'The colors translated using Color.FromArgb are inverted
            'so I'm using BGR
            'jrs 2011/09/21 
            'When moving to AnyChart I stopped using Color.FromArgb, so now the RGB colors are in proper order
            CommDis.setSubTypeColor(RGB(255, 215, 0), 1)
            CommDis.setSubTypeFieldName("ComDisNoAIDSFemales", 2)
            CommDis.setSubTypeColor(RGB(255, 215, 0), 2)
            CommDis.setSubTypeFieldName("AIDSMales", 3)
            CommDis.setSubTypeName("AIDS", 3)
            CommDis.setSubTypeColor(RGB(255, 0, 0), 3)
            CommDis.setSubTypeFieldName("AIDSFemales", 4)
            CommDis.setSubTypeColor(RGB(255, 0, 0), 4)
            'jrs 2009/08/12
            CommDis.setSubTypeFieldName("DiarrheaMales", 5)
            CommDis.setSubTypeName("Diarrhea", 5)
            CommDis.setSubTypeColor(RGB(133, 94, 66), 5) 'Dark Wood
            CommDis.setSubTypeFieldName("DiarrheaFemales", 6)
            CommDis.setSubTypeColor(RGB(133, 94, 66), 6)
            CommDis.setSubTypeFieldName("MalariaMales", 7)
            CommDis.setSubTypeName("Malaria", 7)
            CommDis.setSubTypeColor(RGB(0, 255, 0), 7)
            CommDis.setSubTypeFieldName("MalariaFemales", 8)
            CommDis.setSubTypeColor(RGB(0, 255, 0), 8)
            CommDis.setSubTypeFieldName("RespInfMales", 9)
            CommDis.setSubTypeName("Respiratory Infections", 9)
            CommDis.setSubTypeColor(RGB(65, 105, 225), 9)
            CommDis.setSubTypeFieldName("RespInfFemales", 10)
            CommDis.setSubTypeColor(RGB(65, 105, 225), 10)

            'Non Communicable Diseases
            NonCommDis.setNumSubTypes(14)
            NonCommDis.sTitle = "Non Communicable Diseases"
            NonCommDis.setSubTypeFieldName("MaligNeoPlasMales", 1)
            NonCommDis.setSubTypeName("Malignant Neoplasms", 1)
            NonCommDis.setSubTypeColor(RGB(255, 215, 0), 1)
            NonCommDis.setSubTypeFieldName("MaligNeoPlasFemales", 2)
            NonCommDis.setSubTypeColor(RGB(255, 215, 0), 2)
            NonCommDis.setSubTypeFieldName("CardioVascMales", 3)
            NonCommDis.setSubTypeName("Cardio Vascular", 3)
            NonCommDis.setSubTypeColor(RGB(65, 105, 255), 3)
            NonCommDis.setSubTypeFieldName("CardioVascFemales", 4)
            NonCommDis.setSubTypeColor(RGB(65, 105, 255), 4)
            NonCommDis.setSubTypeFieldName("DigestiveMales", 5)
            NonCommDis.setSubTypeName("Digestive", 5)
            NonCommDis.setSubTypeColor(RGB(205, 0, 0), 5)
            NonCommDis.setSubTypeFieldName("DigestiveFemales", 6)
            NonCommDis.setSubTypeColor(RGB(205, 0, 0), 6)
            NonCommDis.setSubTypeFieldName("RespiratoryMales", 7)
            NonCommDis.setSubTypeName("Respiratory", 7)
            NonCommDis.setSubTypeColor(RGB(0, 255, 0), 7)
            NonCommDis.setSubTypeFieldName("RespiratoryFemales", 8)
            NonCommDis.setSubTypeColor(RGB(0, 255, 0), 8)
            NonCommDis.setSubTypeFieldName("OtherG2Males", 9)
            NonCommDis.setSubTypeName("Other Non Comm", 9)
            NonCommDis.setSubTypeColor(RGB(155, 0, 155), 9)
            NonCommDis.setSubTypeFieldName("OtherG2Females", 10)
            NonCommDis.setSubTypeColor(RGB(155, 0, 155), 10)
            NonCommDis.setSubTypeFieldName("DiabetesMales", 11)
            NonCommDis.setSubTypeName("Diabetes", 11)
            NonCommDis.setSubTypeColor(RGB(255, 165, 0), 11)
            NonCommDis.setSubTypeFieldName("DiabetesFemales", 12)
            NonCommDis.setSubTypeColor(RGB(255, 165, 0), 12)
            NonCommDis.setSubTypeFieldName("MentalHealthMales", 13)
            NonCommDis.setSubTypeName("MentalHealth", 13)
            NonCommDis.setSubTypeColor(RGB(84, 84, 84), 13)
            NonCommDis.setSubTypeFieldName("MentalHealthFemales", 14)
            NonCommDis.setSubTypeColor(RGB(84, 84, 84), 14)

            'Injuries
            Injuries.setNumSubTypes(6)
            Injuries.sTitle = "Injuries"
            Injuries.setSubTypeFieldName("RoadTrafficAccMales", 1)
            Injuries.setSubTypeName("Road Traffic Accidents", 1)
            Injuries.setSubTypeColor(RGB(255, 215, 0), 1)
            Injuries.setSubTypeFieldName("RoadTrafficAccFemales", 2)
            Injuries.setSubTypeColor(RGB(255, 215, 0), 2)
            Injuries.setSubTypeFieldName("OtherunintInjMales", 3)
            Injuries.setSubTypeName("Other Unintentional", 3)
            Injuries.setSubTypeColor(RGB(65, 105, 255), 3)
            Injuries.setSubTypeFieldName("OtherunintInjFemales", 4)
            Injuries.setSubTypeColor(RGB(65, 105, 255), 4)
            Injuries.setSubTypeFieldName("IntenInjMales", 5)
            Injuries.setSubTypeName("Intentional", 5)
            Injuries.setSubTypeColor(RGB(205, 0, 0), 5)
            Injuries.setSubTypeFieldName("IntenInjFemales", 6)
            Injuries.setSubTypeColor(RGB(205, 0, 0), 6)

            'dsb 2026/06/11 All Mortality Subtypes
            AllMortSubTypes.setNumSubTypes(CommDis.NumSubTypes + NonCommDis.NumSubTypes + Injuries.NumSubTypes)
            AllMortSubTypes.sTitle = "All Mortality Subtypes"
            Dim idx As Integer = 1, j As Integer
            'Adding CommDis subtypes
            For j = 1 To CommDis.NumSubTypes
                AllMortSubTypes.setSubTypeFieldName(CommDis.getSubTypeFieldName(j), idx)
                Select Case j
                    Case 1, 2
                        AllMortSubTypes.setSubTypeColor(RGB(0, 100, 0), idx)
                    Case 3, 4
                        AllMortSubTypes.setSubTypeColor(RGB(128, 150, 128), idx)
                    Case 5, 6
                        AllMortSubTypes.setSubTypeColor(RGB(33, 176, 33), idx)
                    Case 7, 8
                        AllMortSubTypes.setSubTypeColor(RGB(47, 79, 47), idx)
                    Case 9, 10
                        AllMortSubTypes.setSubTypeColor(RGB(76, 240, 76), idx)
                    Case Else
                        AllMortSubTypes.setSubTypeColor(RGB(152, 251, 152), idx)
                End Select
                If j Mod 2 = 1 Then
                    AllMortSubTypes.setSubTypeName(CommDis.getSubTypeName(j), idx)
                End If
                idx += 1
            Next
            'Adding NonCommDis subtypes
            For j = 1 To NonCommDis.NumSubTypes
                AllMortSubTypes.setSubTypeFieldName(NonCommDis.getSubTypeFieldName(j), idx)
                Select Case j
                    Case 1, 2
                        AllMortSubTypes.setSubTypeColor(RGB(0, 0, 139), idx)
                    Case 3, 4
                        AllMortSubTypes.setSubTypeColor(RGB(30, 144, 255), idx)
                    Case 5, 6
                        AllMortSubTypes.setSubTypeColor(RGB(70, 130, 180), idx)
                    Case 7, 8
                        AllMortSubTypes.setSubTypeColor(RGB(100, 149, 237), idx)
                    Case 9, 10
                        AllMortSubTypes.setSubTypeColor(RGB(123, 104, 238), idx)
                    Case 11, 12
                        AllMortSubTypes.setSubTypeColor(RGB(72, 61, 139), idx)
                    Case 13, 14
                        AllMortSubTypes.setSubTypeColor(RGB(135, 206, 250), idx)
                    Case Else
                        AllMortSubTypes.setSubTypeColor(RGB(176, 224, 230), idx)
                End Select
                If j Mod 2 = 1 Then
                    AllMortSubTypes.setSubTypeName(NonCommDis.getSubTypeName(j), idx)
                End If
                idx += 1
            Next
            'Adding Injuries subtypes
            For j = 1 To Injuries.NumSubTypes
                AllMortSubTypes.setSubTypeFieldName(Injuries.getSubTypeFieldName(j), idx)
                Select Case j
                    Case 1, 2
                        AllMortSubTypes.setSubTypeColor(RGB(255, 140, 0), idx)
                    Case 3, 4
                        AllMortSubTypes.setSubTypeColor(RGB(255, 99, 71), idx)
                    Case 5, 6
                        AllMortSubTypes.setSubTypeColor(RGB(220, 20, 60), idx)
                    Case Else
                        AllMortSubTypes.setSubTypeColor(RGB(205, 92, 92), idx)
                End Select
                If j Mod 2 = 1 Then
                    AllMortSubTypes.setSubTypeName(Injuries.getSubTypeName(j), idx)
                End If
                idx += 1
            Next

            Session("CommDis") = CommDis
            Session("NonCommDis") = NonCommDis
            Session("Injuries") = Injuries
            Session("AllMortSubTypes") = AllMortSubTypes 'dsb 2026/06/11 

            'jrs 2020/06/02 Graph_Draw((Session("EdCohort_GraphType")))
            Session("FirstYearSce") = Session("FirstYear")
        Else
            'jrs 2008/09/16
            Case_of_Work = Session("POPCG_Case_of_Work")

            CommDis = Session("CommDis")
            NonCommDis = Session("NonCommDis")
            Injuries = Session("Injuries")
            AllMortSubTypes = Session("AllMortSubTypes") 'dsb 2026/06/11

            If Not IsNothing(Session("POPMales")) Then
                POPMales = Session("POPMales")
                POPFemales = Session("POPFemales")
            End If
        End If

        'webmenu
        wmMain.UserData = "jsolorza::976739975"

        wmMain.MenuBar.ShowOnMouseOver = True

        'wmMain.ClearPixelImage = "blank.gif"
        'wmMain.PopupIcon = "./images/Popup.gif"
        'wmMain.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmMain.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmMain.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmMain.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmMain.SelectedPopupIcon = "./images/SelectedPopup.gif"

        wmMain.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        wmMain.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmMain.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmMain.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmMain.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmMain.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmMain.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")


        wmMain.ScrollEnabled = True
        wmMain.HideTimer = 1000
        'jrs 2012/01/17
        wmMain.AccessorKeyCode = 27
        'jrs 2012/01/24
        wmMain.ButtonFont.Size = "12px"
        wmMain.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_e2bf"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        'jrs 2011/05/02
        If InStr(Session("DisplayParent"), "frm_CountryProfile.aspx") > 0 Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Main Menu"
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Distribution Type"
        cswmGroup1.ID = "Group_2e54"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "All types of Mortality"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Communicable Disease"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Non Communicable Disease"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Injuries"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Communicable Disease Subtypes"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Non Communicable Disease Subtypes"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Injuries Subtypes"

        'dsb 2026/06/11
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "All Mortality Subtypes"

        'jrs 2012/08/27
        cswmItem = cswmGroup1.Items.Add
        If Session("EdCohort_Grouping") = True Then
            cswmItem.Caption = "Compare Groups"
        Else
            cswmItem.Caption = "Compare Countries"
        End If
        cswmItem.ID = "mnuCompare"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Show Numbers"
        cswmGroup1.ID = "Group_79e9"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Numbers"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Factors"

        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Scaling Factors"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Normalization Factors"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Set Group or Country/Region"
        cswmGroup1.ID = "mnuGroups" 'jrs 2014/02/21

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Countries/Regions"
        cswmItem.IconHeight = 16
        If Session("EdCohort_Grouping") = True Then
            'jam 2021/03/06
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            'jam 2021/03/06
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Groups"
        cswmItem.IconHeight = 16
        If Session("EdCohort_Grouping") = True Then
            'jam 2021/03/06
            ' cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/03/06
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        'jrs 2024/09/06
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Hide Non Core Groups"
        cswmItem.IconHeight = 16
        If Session("bCheckHideNonCoreGroups") Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Change Display"
        cswmGroup1.ID = "Group_1113"

        cswmItem = cswmGroup1.Items.Add()
        If Session("EdCohort_Grouping") = True Then
            cswmItem.Caption = "Change Groups"
        Else
            cswmItem.Caption = "Change Countries/Regions"
        End If

        'jrs 2011/11/14
        cswmGroup2 = cswmItem.AddGroup()
        If Session("EdCohort_Grouping") = False Then
            'jrs 2026/02/03
            LoadRegionAlphaGroups(5, cswmGroup2, cswmItem)

            'For l = 1 To NR
            '    cswmItem = cswmGroup2.Items.Add()
            '    cswmItem.Caption = GRPS(1, l)
            '    cswmItem.UserData = "Reg"
            '    'cswmItem.OnClick = "cntrymnu_click('" & IFsRegs.Item(GRPS(1, l)).ISO3166_2Code & "');"
            '    If l < NR Then
            '        If GRPS(1, l).Chars(0) <> GRPS(1, l + 1).Chars(0) Then
            '            cswmItem = cswmGroup2.Items.Add()
            '            cswmItem.Caption = "--shadow--"
            '            cswmItem.DividerColor = "#808080"
            '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
            '        End If
            '    End If
            'Next
        Else
            'jrs 2024/09/06
            If Session("bCheckHideNonCoreGroups") Then
                'jrs 2026/02/03
                LoadCoreGroupsAlphaGroups(4, cswmGroup2, cswmItem)

                'For l = 1 To CGCount
                '    cswmItem = cswmGroup2.Items.Add()
                '    cswmItem.Caption = CoreGroupNames(l)
                '    cswmItem.UserData = "Grp"
                '    If l < CGCount Then
                '        If CoreGroupNames(l).Chars(0) <> CoreGroupNames(l + 1).Chars(0) Then
                '            cswmItem = cswmGroup2.Items.Add()
                '            cswmItem.Caption = "--shadow--"
                '            cswmItem.DividerColor = "#808080"
                '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                '        End If
                '    End If
                'Next
            Else
                'jrs 2026/02/03
                LoadAllGroupsAlphaGroups(20, cswmGroup2, cswmItem)

                'For l = 1 To GCount
                '    cswmItem = cswmGroup2.Items.Add()
                '    cswmItem.Caption = GroupNames(l)
                '    cswmItem.UserData = "Grp"
                '    If l < GCount Then
                '        If GroupNames(l).Chars(0) <> GroupNames(l + 1).Chars(0) Then
                '            cswmItem = cswmGroup2.Items.Add()
                '            cswmItem.Caption = "--shadow--"
                '            cswmItem.DividerColor = "#808080"
                '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                '        End If
                '    End If
                'Next
            End If
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Year"

        'jrs 2011/11/14
        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Please select a year"
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        For l = Session("FirstYear") To Session("LastYear")
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = l.ToString
            cswmItem.UserData = "Yr"
            'cswmItem.BackgroundImage = "./images/itemBackground.gif"
            If l < Session("LastYear") Then
                If l Mod 10 = 9 Then
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = "--shadow--"
                    cswmItem.DividerColor = "#808080"
                    cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                End If
            End If
        Next

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Scenario"
        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Base Case"
        cswmItem.IconHeight = 16
        If Session("EdCG_Casename") = "Base Case" Then
            'jam 2021/03/06
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/03/06
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Work Case"
        cswmItem.IconHeight = 16
        'jrs 2024/04/23
        If Session("EdCG_Casename") = "Work Case" Then
            'jam 2021/03/16
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"blank.gif"
        Else
            'jam 2021/03/16
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"checkbox.gif"
        End If

        If UCase(Session("ActiveBase")) <> "IFSBASEHIST.RUN" Then
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Historical Forecast"
            cswmItem.IconHeight = 16
            If Session("EdCG_Casename") = "Historical Forecast" Then
                'jam 2021/03/06
                'cswmItem.IconImage = "checkbox.gif"
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            Else
                'jam 2021/03/06
                'cswmItem.IconImage = "blank.gif"
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
            End If
        End If





        'Load other Scenario Menu options for display
        LoadSceMenus(cswmGroup2)

        'jam 2022/07/17
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Deaths"
        cswmGroup2 = cswmItem.AddGroup()



        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "ON"

        cswmItem.IconHeight = 16
        If Session("cnvtoDeaths") Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If




        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "OFF"

        cswmItem.IconHeight = 16
        If Session("cnvtoDeaths") Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If






        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Time"
        cswmGroup1.ID = "Group_5ca8"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Advance"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regress"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Include oldest age group"
        cswmGroup1.ID = "mnuAge100+"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuAge100+On"
        If Session("bShow100") Then
            'jam 2021/03/13
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/03/13
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuAge100+Off"
        If Session("bShow100") Then
            'jam 2021/03/13
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            'jam 2021/03/13
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If

        'jrs 2014/02/14
        If Session("bCompare") = True Then
            wmMain.Groups("Change Display").ShowMenuBarItem = False
            wmMain.Groups("Time").ShowMenuBarItem = False
            wmMain.Groups("mnuGroups").ShowMenuBarItem = False
        End If

        'jrs 2014/02/13
        If Session("mnuFixCtSc") Is Nothing Then
            Session("mnuFixCtSc") = False
            Session("mnuFixCteScTime") = False
            Session("mnuFixCtScMan") = False
            Session("mnuUsePctPop") = False
            Session("bReadGrpMaxs") = False
        End If
        ReDim Preserve Session("MaxScale")(7) 'dsb 2026/06/23 Changed index from 6 to 7

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Change Scale"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale across Time and Regions"
        If Session("mnuFixCtSc") = False Then
            'jam 2021/03/06
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            'jam 2021/03/06
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale across Time"
        If Session("mnuFixCteScTime") = False Then
            'jam 2021/03/06
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            'jam 2021/03/06
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale Manually"
        If Session("mnuFixCtScMan") = False Then
            'jam 2021/03/06
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            'jam 2021/03/06
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If

        'jrs 2013/09/04
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Help"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        AddHandler wmMain.ServerItemClick, AddressOf ItemClickHandlerMain

        'jam 2021/06/03
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(vCountry)) And Not Session("IsGroup") Then 'JAM 2020-09-28
            'jrs 2011/05/02
            ' If Not IsNothing(Request.QueryString("Country")) Then
            'jam 2021/02/06
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            'jam 2021/03/06
            If Session("MMCountry") <> vCountry Or Not Page.IsPostBack Then
                'jam 2021/02/06
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = vCountry
                'jrs 2011/09/15
                If Session("EdCohort_Grouping") Then
                    'jam 2021/03/06
                    'need to fix menus
                    wmMain.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    wmMain.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                    wmMain.Groups("Change Display").Items(0).Caption = "Change Countries/Regions"
                End If

                Session("EdCohort_Grouping") = False
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
            'jam 2021/06/03
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(vCountry)) And Session("IsGroup") Then

            'jam 2021/06/03
            If Session("EdCohort_regionname") <> vCountry Then 'JAM 2020-10-10
                'If Session("EdCohort_regionname") <> Request.QueryString("Group") Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2024/08/21
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)

                'jrs 2024/09/06
                If Session("bCheckHideNonCoreGroups") Then
                    If Not IsNothing(Request.QueryString("Group")) Then
                        gIdx = gGetCoreGroupIdx(Request.QueryString("Group"), False, Me)
                    Else
                        'jrs 2024/09/03 gIdx = gGetGroupIdx(Session("EdCohort_regionname"), False, Me)
                        gIdx = gGetCoreGroupIdx(vCountry, False, Me)
                    End If
                Else
                    If Not IsNothing(Request.QueryString("Group")) Then
                        gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                    Else
                        'jrs 2024/09/03 gIdx = gGetGroupIdx(Session("EdCohort_regionname"), False, Me)
                        gIdx = gGetGroupIdx(vCountry, False, Me)
                    End If
                End If


                If gIdx > 0 Then
                    lstGroups.SelectedIndex = gIdx - 1

                    'jam 2021/03/06
                    'Session("MMGroup") = Request.QueryString("Group")

                    Session("MMGroup") = vCountry

                Else
                    Session.Remove("MMGroup")
                End If
                If Session("EdCohort_Grouping") = True Then
                    lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
                Else
                    mnuGroups_Click(wmMain, New Coalesys.WebMenu.ServerItemClickEventArgs(wmMain.Groups("mnuGroups").Items(0)))
                End If
            End If
        End If
        'jrs 2012/05/03
        'If Not IsNothing(Request.QueryString("Year")) Then
        'jam 2021/02/07
        If Not IsNothing(Request.QueryString("Year")) Or Not IsNothing(vYear) Then
            'jrs 2024/07/08 added not Not IsNothing(vYear) andalso
            If Not IsNothing(vYear) AndAlso Session("EdCohort_tempyear") <> Request.QueryString("Year") Then
                'jam 2021/02/07
                'Session("EdCohort_tempyear") = Request.QueryString("Year")
                Session("EdCohort_tempyear") = vYear
                If Session("EdCohort_tempyear") < Session("FirstYearSce") Then Session("EdCohort_tempyear") = Session("FirstYearSce")
                If Session("EdCohort_tempyear") > Session("PopCohort_year_run") Then Session("EdCohort_tempyear") = Session("PopCohort_year_run")
            End If
        End If

        'jam 2021/02/07
        'jrs 2012/05/05
        'If Not IsNothing(Request.QueryString("File")) Then
        '    If Session("EdCG_Casename") <> Request.QueryString("File") Then
        If Not IsNothing(Request.QueryString("File")) Or Not IsNothing(vScenario) Then
            If Session("EdCG_Casename") <> vScenario Then

                'Session("POPCG_Case_of_Work") = Case_of_Work
                'jam 2021/02/07
                'CaseName = Request.QueryString("File")
                CaseName = vScenario
                'jrs 2014/02/21
                'If Request.QueryString("File") = "Base Case" Then
                'JAM 2021/02/07
                If vScenario = "Base Case" Then
                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                        'JAM 2020-06-08
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                        Else
                            Session("SQLiteDB") = "IFsPop.db"

                            Session("db2Path") = Session("PutFdrvRun") & "IFsPop.db"

                        End If
                    Else
                        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                        'JAM 2020-06-08
                        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                        Else
                            Session("SQLiteDB") = "IFsPopHist.db"

                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                        End If
                    End If
                    'JAM 2021/02/07
                    'ElseIf Request.QueryString("File") = "Work Case" Then
                ElseIf vScenario = "Work Case" Then
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

                    'JAM 2020-06-08
                    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        Session("SQLiteDB") = "IFsPopWork.db"

                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.db"

                    End If
                    'jam 2021/03/06
                    ' ElseIf Request.QueryString("File") = "Historical Forecast" Then
                ElseIf vScenario = "Historical Forecast" Then
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    'JAM 2020-06-08
                    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        Session("SQLiteDB") = "IFsPopHist.db"

                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                    End If

                Else
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"

                    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"
                    Else
                        Session("SQLiteDB") = "IFsPop" & CaseName & ".db"

                        Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".db"

                    End If

                    If Dir(Session("db2Path")) = "" Then
                        CaseName = "Base Case"
                        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                            Else
                                Session("SQLiteDB") = "IFsPop.db"

                                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.db"

                            End If


                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                            Else
                                Session("SQLiteDB") = "IFsPopHist.db"

                                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                            End If

                        End If
                    End If
                End If
                'jrs 2024/04/24
                If Session("mnuSQLite") = "True" Then
                    If Not Session("SQLiteDB") Is Nothing Then
                        vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                    Else
                        vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                    End If


                    'jam 2025/05/31 Changing connection mode
                    'vSQLiteCon = New SQLiteConnection(vConnString) '& "New=true;"
                    'vSQLiteCon.Open()


                    'jam 2025/05/31 Changing connection mode
                    vSQLiteCon = OpenJournalSQLite(vConnString)


                End If

                'jam 2021/03/06
                'jrs 2014/02/21 uncheck previous sce and check new one
                wmMain.Groups("Change Display").Items("Change Scenario").Group.Items(Session("EdCG_Casename")).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                Session("EdCG_Casename") = CaseName
                'jam 2021/03/06
                wmMain.Groups("Change Display").Items("Change Scenario").Group.Items(Session("EdCG_Casename")).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"

                Session("PopCohort_year_run") = FindLastYear()
                Session("EdCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("EdCohort_tempyear"))
                Session("FirstYearSce") = FindFirstYear()

                If Session("PopCohort_year_run") <= FirstYearForecast Then
                    Session("EdCohort_tempyear") = FirstYearHistory
                ElseIf Session("EdCohort_tempyear") < FirstYearForecast Then
                    Session("EdCohort_tempyear") = FirstYearForecast
                End If
            End If
        End If

        If Not Session("cNumber") Is Nothing Then
            lstGroups.Visible = False
            cntrylist.Visible = True
            cntrylist.SelectedIndex = CInt(Session("cNumber")) - 1
            Session.Remove("cNumber")
            cntrylist_SelectedIndexChanged(Me, New System.EventArgs)
        Else
            'jrs 2020/06/02
            If Not Page.IsPostBack Then
                Graph_Draw((Session("EdCohort_GraphType")))
            End If




            PackJScritpBuilder()
        End If

        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) And Not Page.IsPostBack Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

    End Sub

    Private Sub LoadSceMenus(ByRef cswmGroup2 As Coalesys.WebMenu.Group)
        'Read all files with format ifsPopXXX.mdb located in runfiles
        Dim sfile As String
        'sfile = Dir(Session("PutFdrvRun2") + "\IFsPop*.mdb")

        'JAM 2020-06-08
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            sfile = Dir(Session("PutFdrvRun2") + "\IFsPop*.mdb")

        Else
            sfile = Dir(Session("PutFdrvRun2") + "\IFsPop*.db")
        End If


        Dim cswmItem As Coalesys.WebMenu.Item

        'jrs 2014/01/31
        Dim bLoadSces As Boolean
        bLoadSces = (cmbSce1.Items.Count = 0)
        If bLoadSces Then
            cmbSce1.Items.Add("Base Case")
            cmbSce2.Items.Add("Base Case")
            cmbSce3.Items.Add("Base Case")
            cmbSce4.Items.Add("Base Case")
            cmbSce1.Items.Add("Work Case")
            cmbSce2.Items.Add("Work Case")
            cmbSce3.Items.Add("Work Case")
            cmbSce4.Items.Add("Work Case")
            cmbSce1.Items.Add("Historical Forecast")
            cmbSce2.Items.Add("Historical Forecast")
            cmbSce3.Items.Add("Historical Forecast")
            cmbSce4.Items.Add("Historical Forecast")
            cmbSce1.SelectedIndex = 0
            cmbSce2.SelectedIndex = 0
            cmbSce3.SelectedIndex = 0
            cmbSce4.SelectedIndex = 0
        End If

        Do While (sfile <> "")
            'JAM 2020-06-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then 'JAM 2020-05-29
                If UCase(sfile) <> "IFSPOPWORK.MDB" And UCase(sfile) <> "IFSPOP.MDB" And UCase(sfile) <> "IFSPOPHIST.MDB" Then 'jrs 2008/06/16
                    If Session("IFsRunFiles").item(Mid(Replace(Replace(sfile, ".mdb", ""), ".MDB", ""), 7) & ".run").Visible Then
                        cswmItem = cswmGroup2.Items.Add()
                        cswmItem.Caption = Mid(Replace(Replace(sfile, ".mdb", ""), ".MDB", ""), 7)
                        cswmItem.IconHeight = 16
                        If Session("EdCG_Casename") = cswmItem.Caption Then
                            'jam 2021/03/06
                            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                        Else
                            'jam 2021/03/06
                            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                        End If
                        'jrs 2014/02/12
                        If bLoadSces Then
                            cmbSce1.Items.Add(cswmItem.Caption)
                            cmbSce2.Items.Add(cswmItem.Caption)
                            cmbSce3.Items.Add(cswmItem.Caption)
                            cmbSce4.Items.Add(cswmItem.Caption)
                        End If
                    End If
                End If
            Else
                If UCase(sfile) <> "IFSPOPWORK.DB" And UCase(sfile) <> "IFSPOP.DB" And UCase(sfile) <> "IFSPOPHIST.DB" Then 'jrs 2008/06/16
                    If Session("IFsRunFiles").item(Mid(Replace(Replace(sfile, ".db", ""), ".DB", ""), 7) & ".run.db").Visible Or StandAloneMode Then 'jrs 2024/01/24 added or StandAloneMode
                        cswmItem = cswmGroup2.Items.Add()
                        cswmItem.Caption = Mid(Replace(Replace(sfile, ".db", ""), ".DB", ""), 7)
                        cswmItem.IconHeight = 16
                        If Session("EdCG_Casename") = cswmItem.Caption Then
                            'jrs 2024/04/24
                            'jam 2021/03/06
                            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                        Else
                            'jam 2021/03/06
                            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")  ' "blank.gif"
                        End If
                        'jrs 2014/02/12
                        If bLoadSces Then
                            cmbSce1.Items.Add(cswmItem.Caption)
                            cmbSce2.Items.Add(cswmItem.Caption)
                            cmbSce3.Items.Add(cswmItem.Caption)
                            cmbSce4.Items.Add(cswmItem.Caption)
                        End If
                    End If
                End If
            End If
            sfile = Dir()
        Loop
    End Sub

    Private Sub Graph_Draw(ByVal Index As Integer)
        '--------------------------------------------------------------

        Select Case Index
            Case 0
                CompositeGraph_Draw(Session("db2Path"), Index, 1, 1, 2, 3, 4)
            Case 1, 2, 3
                SeparateGraph_Draw(Session("db2Path"), Index, 4, 1, 2, 3, 4)
            Case 4
                CompositeGraph_Draw_All(Session("db2Path"), CommDis, 1)
            Case 5
                CompositeGraph_Draw_All(Session("db2Path"), NonCommDis, 2)
            Case 6
                CompositeGraph_Draw_All(Session("db2Path"), Injuries, 3)
            Case 7 'dsb 2026/06/11
                CompositeGraph_Draw_All(Session("db2Path"), AllMortSubTypes, 7)
        End Select

        Session("EdCG_Index") = Index
        Session("EdCG_Regionname") = Session("EdCohort_regionname")
        Session("EdCG_tempyear") = Session("EdCohort_tempyear")

        PackJScritpBuilder()

    End Sub

    Private Sub CompositeGraph_Draw(ByVal db2Path As String, ByVal gphIndex As Integer, ByVal tblindex As Integer, ByVal findex1 As Integer,
        Optional ByVal findex2 As Integer = 0, Optional ByVal findex3 As Integer = 0,
        Optional ByVal findex4 As Integer = 0)
        '--------------------------------------------------------------
        '--------------------------------------------------------------
        Dim tbl2 As DataTable
        Dim TCohort As Integer
        Dim GraphTitle As String
        Dim Tindex As Integer
        Dim rId As Integer


        ReDim Session("EC_TempPopDatabase")(NCohorts, 2)

        ReDim Session("EC_TempDatabase")(NCohorts, 6)

        '   Specify the graph control to be used
        '   ------------------------------------
        Dim i2 As Integer
        'jrs 2005/05/30 
        'For i2 = 0 To 4
        '    gphAgedst(i2).Visible = False
        'Next
        frameData.Visible = False
        grdcohort.Visible = False
        lbldata.Visible = False
        'jrs 2009/03/20
        grdcohortDet.Visible = False
        lblJS.Visible = True

        'jrs 2006/07/14 create connecion
        Dim Conn As OleDb.OleDbConnection
        'Conn = ADO_OleDB_CreateConnection(db2Path)
        'Conn.Open()
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'JAM 2020-06-08
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))
            Conn = ADO_OleDB_CreateConnection(Replace(Session("db2Path").ToString, ".db", ".mdb"))
            Conn.Open()
            myCMD.Connection = Conn
        End If

        'jrs 2023/08/25 Dim IfsConn As OleDb.OleDbConnection
        ''Dim IfsConn As SqliteConnection = New SqliteConnection
        'jrs 2023/08/23 IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        ''IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        'jam 2025/05/31
        Dim IfsConn As SQLiteConnection
        Dim ConnectionString As String

        ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        'jam 2025/05/31 Changing connection mode

        'If IfsConn.State <> ConnectionState.Open Then
        IfsConn = OpenJournalSQLite(ConnectionString)
        'End If

        Dim rdr As OleDb.OleDbDataReader


        Dim strSQL As String
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow

        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        dtnew = New DataTable
        regDS = New DataSet
        'jrs 2023/08/24
        regDS.EnforceConstraints = False

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2

        'jam 2026/01/23 Reopening the connection
        If Not (vSQLiteCon Is Nothing) Then

            If Not Session("mnuSQLite") Is Nothing Then
                If Session("mnuSQLite") Then
                    If Not Session("SQLiteDB") Is Nothing Then
                        vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                    Else
                        vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                    End If

                    vSQLiteCon = OpenJournalSQLite(vConnString)

                End If
            End If


        End If



        If Session("EdCohort_Grouping") = False Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(2) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
                i2 = 0
                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) = rdr.Item(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) = rdr.Item(FieldName(Tindex, 2))
                Loop
                rdr.Close()
            Else
                'jam 2029/01/22 Using the right db connection
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(2) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())


                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))

                End While

                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()

            End If
        Else
            ReDim POPMales(NR, NCohorts)
            ReDim POPFemales(NR, NCohorts)
            'jrs 2014/02/21
            Session("sRegs") = ""

            'jam 2025/05/31
            'IfsConn.Open()
            If IfsConn.State <> ConnectionState.Open Then
                IfsConn.Open()
            End If
            'jrs 2023/08/25 Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim rdGrpCmd As SQLiteCommand = IfsConn.CreateCommand
                rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
                Dim rdGrp As SQLiteDataReader
                rdGrp = rdGrpCmd.ExecuteReader


                Dim myTbl As DataTable = New DataTable
                myTbl.TableName = "Groups"
                regDS.Tables.Add(myTbl)
                regDS.Tables(0).Load(rdGrp)

                'jrs 2023/08/25 Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
                Dim rdRegCmd As SQLiteCommand = IfsConn.CreateCommand
                rdRegCmd.CommandText = "SELECT * FROM " & Grouping
                Dim rdReg As SQLiteDataReader
                rdReg = rdRegCmd.ExecuteReader

                Dim myTbl1 As DataTable = New DataTable
                myTbl1.TableName = "Regions"
                regDS.Tables.Add(myTbl1)
                regDS.Tables(1).Load(rdReg)

                'jrs 2023/08/25 Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
                Dim rdRegIdCmd As SQLiteCommand = IfsConn.CreateCommand
                rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
                Dim rdRegId As SQLiteDataReader
                rdRegId = rdRegIdCmd.ExecuteReader

                Dim myTbl2 As DataTable = New DataTable
                myTbl2.TableName = "RegionIDs"
                regDS.Tables.Add(myTbl2)
                regDS.Tables(2).Load(rdRegId)

                'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)


                'JAM 2020-06-08
                Dim popDA As OleDb.OleDbDataAdapter

                'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)


                'JAM 2020-06-08
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    popDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                Else

                    'JAM 2019-12-23
                    vSQLiteCommand2 = vSQLiteCon.CreateCommand()
                    vSQLiteCommand2.CommandText = "SELECT * FROM " & TableName(Tindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                    vSQLiteReader = vSQLiteCommand2.ExecuteReader()
                    DTsqlite.Load(vSQLiteReader)
                    'regDS.Tables.Add(DTsqlite)

                    'jrs 2023/08/25
                    'dtnew = DTsqlite.Clone
                    'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    'd2.DataType = System.Type.GetType("System.Int32")

                    'For Each row As DataRow In DTsqlite.Rows
                    '    dtnew.ImportRow(row)
                    'Next
                    'regDS.Tables.Add(dtnew)
                    regDS.Tables.Add(DTsqlite)

                End If







                'regDS = New DataSet
                'jrs 2023/08/25 grpDA.Fill(regDS, "Groups")
                'jrs 2023/08/25 regDA.Fill(regDS, "Regions")
                'jrs 2023/08/25 regIDDA.Fill(regDS, "RegionIDs")
                'popDA.Fill(regDS, "Population")


                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    popDA.Fill(regDS, "Population")
                End If


                grpRegRel = regDS.Relations.Add("GrpRegion",
                                                         regDS.Tables("Groups").Columns("GroupNum"),
                                                         regDS.Tables("Regions").Columns("GroupNo"))
                regRegIdRel = regDS.Relations.Add("RegRegId",
                                                         regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                         regDS.Tables("Regions").Columns("RegionNo"))
                'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Population").Columns("Region"))


                Dim regPopRel As DataRelation

                'JAM 2020-05-29
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    regPopRel = regDS.Relations.Add("RegPopulation",
                                                         regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                         regDS.Tables("Population").Columns("Region"))
                Else


                    regPopRel = regDS.Relations.Add("RegPopulation",
                                                         regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                         regDS.Tables(TableName(Tindex)).Columns("RegionId"))


                End If

                'JAM 2020-06-08
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    For Each pppRow In regDS.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                                If CStr(Session("sRegs")) = "" Then
                                    Session("sRegs") = ppRow.Item("RegionNo")
                                Else
                                    Session("sRegs") = Session("sRegs") & "," & ppRow.Item("RegionNo")
                                End If
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    'Console.WriteLine(pRow("CustomerID").ToString())
                                    For Each cRow In pRow.GetChildRows(regPopRel)
                                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) + cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) + cRow.Item(FieldName(Tindex, 2))

                                        POPMales(cRow.Item("Region"), cRow.Item("NCohorts")) = cRow.Item(FieldName(Tindex, 1))
                                        POPFemales(cRow.Item("Region"), cRow.Item("NCohorts")) = cRow.Item(FieldName(Tindex, 2))
                                    Next
                                Next
                            Next
                        End If
                    Next
                Else
                    For Each pppRow In regDS.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                                If CStr(Session("sRegs")) = "" Then
                                    Session("sRegs") = ppRow.Item("RegionNo")
                                Else
                                    Session("sRegs") = Session("sRegs") & "," & ppRow.Item("RegionNo")
                                End If
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    'Console.WriteLine(pRow("CustomerID").ToString())
                                    For Each cRow In pRow.GetChildRows(regPopRel)
                                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) + cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) + cRow.Item(FieldName(Tindex, 2))

                                        POPMales(cRow.Item("RegionId"), cRow.Item("CohortId")) = cRow.Item(FieldName(Tindex, 1))
                                        POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId")) = cRow.Item(FieldName(Tindex, 2))
                                    Next
                                Next
                            Next
                        End If
                    Next
                End If

                Session("POPMales") = POPMales
                Session("POPFemales") = POPFemales
            End If
            'End take population

            '   Change the data source
            '   ----------------------
            If Session("EdCohort_Grouping") = False Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
                i2 = 0
                Do While rdr.Read 'Not tbl2.Rows.Count = i2

                    'males - tertiary to No completed formal education
                    TCohort = rdr.Item("NCohorts") '5=Injury Male,3=Non Comm Dis Male, 1 = Comm Dis Male
                    Session("EC_TempDatabase")(TCohort, 3) = rdr.Item(FieldName(1, 5))
                    Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, rdr.Item(FieldName(1, 3)))
                    Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, rdr.Item(FieldName(1, 1)))

                    'females 6=Injury Female, 4=Non Comm Dis female, 2 = Comm Dis Female
                    Session("EC_TempDatabase")(TCohort, 6) = rdr.Item(FieldName(1, 6))
                    Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, rdr.Item(FieldName(1, 4)))
                    Session("EC_TempDatabase")(TCohort, 4) = AMAX(0, rdr.Item(FieldName(1, 2)))

                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())


                    'males - tertiary to No completed formal education
                    TCohort = vSQLiteReader("CohortId") '5=Injury Male,3=Non Comm Dis Male, 1 = Comm Dis Male
                    Session("EC_TempDatabase")(TCohort, 3) = vSQLiteReader(FieldName(1, 5))
                    Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, vSQLiteReader(FieldName(1, 3)))
                    Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, vSQLiteReader(FieldName(1, 1)))

                    'females 6=Injury Female, 4=Non Comm Dis female, 2 = Comm Dis Female
                    Session("EC_TempDatabase")(TCohort, 6) = vSQLiteReader(FieldName(1, 6))
                    Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, vSQLiteReader(FieldName(1, 4)))
                    Session("EC_TempDatabase")(TCohort, 4) = AMAX(0, vSQLiteReader(FieldName(1, 2)))

                End While

                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()

            End If
        Else
            '    Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(tblindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            'mortDA.Fill(regDS, "Mortality")

            Dim mortDA As OleDb.OleDbDataAdapter

            'JAM 2020-05-30
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then


                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(tblindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                mortDA.Fill(regDS, "Mortality")

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()

                'JAM 2020-06-21
                vSQLiteCommand.CommandText = "Select TimeId, CohortId, RegionId, MortPerMalesComDis, MortPerFeMalesComDis, MortPerMalesNonComDis, MortPerFeMalesNonComDis, MortPerMalesInjuries, MortPerFeMalesInjuries,ComDisNoAIDSMales,AIDSMales,ComDisNoAIDSFemales,AIDSFemales FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))

                'vSQLiteCommand.CommandText = "Select TimeId, CohortId, RegionId, MortPerMalesComDis, MortPerFeMalesComDis, MortPerMalesNonComDis, MortPerFeMalesNonComDis, MortPerMalesInjuries, MortPerFeMalesInjuries,ComDisNoAIDSMales,AIDSMales,ComDisNoAIDSFemales,AIDSFemales FROM " & TableName(tblindex) & " INDEXED BY mor_det_time_idx Where TimeId = " & Str(Session("EdCohort_tempyear"))


                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2023/08/25
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If






            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Mortality").Columns("Region"))


            Dim regMortRel As DataRelation

            'JAM 2020-05-30
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables(TableName(tblindex)).Columns("RegionId"))


            End If


            'JAM 2020-06-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")
                                    Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) +
                                    cRow.Item(FieldName(1, 5)) * POPMales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) +
                                    cRow.Item(FieldName(1, 3)) * POPMales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 1) = Session("EC_TempDatabase")(TCohort, 1) +
                                    cRow.Item(FieldName(1, 1)) * POPMales(rId, TCohort)

                                    Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) +
                                    cRow.Item(FieldName(1, 6)) * POPFemales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 5) = Session("EC_TempDatabase")(TCohort, 5) +
                                    cRow.Item(FieldName(1, 4)) * POPFemales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) +
                                    cRow.Item(FieldName(1, 2)) * POPFemales(rId, TCohort)
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")
                                    Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) +
                                    cRow.Item(FieldName(1, 5)) * POPMales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) +
                                    cRow.Item(FieldName(1, 3)) * POPMales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 1) = Session("EC_TempDatabase")(TCohort, 1) +
                                    cRow.Item(FieldName(1, 1)) * POPMales(rId, TCohort)

                                    Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) +
                                    cRow.Item(FieldName(1, 6)) * POPFemales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 5) = Session("EC_TempDatabase")(TCohort, 5) +
                                    cRow.Item(FieldName(1, 4)) * POPFemales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) +
                                    cRow.Item(FieldName(1, 2)) * POPFemales(rId, TCohort)
                                Next
                            Next
                        Next
                    End If
                Next


            End If
            IfsConn.Close()

            'Population Weigthed average
            For TCohort = 0 To NCohorts
                'males
                Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) / Session("EC_TempPopDatabase")(TCohort, 1)
                Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) / Session("EC_TempPopDatabase")(TCohort, 1)
                Session("EC_TempDatabase")(TCohort, 1) = Session("EC_TempDatabase")(TCohort, 1) / Session("EC_TempPopDatabase")(TCohort, 1)
                'females
                Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) / Session("EC_TempPopDatabase")(TCohort, 2)
                Session("EC_TempDatabase")(TCohort, 5) = Session("EC_TempDatabase")(TCohort, 5) / Session("EC_TempPopDatabase")(TCohort, 2)
                Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) / Session("EC_TempPopDatabase")(TCohort, 2)
            Next TCohort

        End If


        'jam 2022/07/17
        If Session("cnvtoDeaths") Then
            ConvertToDeaths(NCohorts, 3)
        End If


        'FillDataGrid Tindex
        'CompositeFillDataGrid() '1, 2, 3
        'end mti june, 2002


        '   Draw the graph
        '   --------------
        '    gphAgedst(Index).AutoInc = 1
        '    gphAgedst(Index).DataReset = 1

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn.Close()
        End If

        'jrs 2014/01/29
        If Session("bReadGrpMaxs") Then
            SetUpMaxsTime()
            Session("bReadGrpMaxs") = False
        End If



    End Sub

    Private Sub CompositeGraph_Draw_Compare(ByVal gphIndex As Integer)
        '--------------------------------------------------------------

        Select Case gphIndex
            Case 1
                Session("EdCohort_tempregion") = cmbReg1.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg1.SelectedValue
                Session("EdCohort_tempyear") = cmbYr1.SelectedValue
            Case 2
                Session("EdCohort_tempregion") = cmbReg2.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg2.SelectedValue
                Session("EdCohort_tempyear") = cmbYr2.SelectedValue
            Case 3
                Session("EdCohort_tempregion") = cmbReg3.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg3.SelectedValue
                Session("EdCohort_tempyear") = cmbYr3.SelectedValue
            Case 4
                Session("EdCohort_tempregion") = cmbReg4.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg4.SelectedValue
                Session("EdCohort_tempyear") = cmbYr4.SelectedValue
        End Select

        'jrs 2024/04/22
        vSQLiteCon.Close()
        vConnString = "DataSource=" + Session("dbsces")(gphIndex - 1) & ";"
        'jam 2025/05/31 Changing connection mode
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()


        'jam 2025/05/31 Changing connection mode
        vSQLiteCon = OpenJournalSQLite(vConnString)



        CompositeGraph_Draw(Session("dbsces")(gphIndex - 1), gphIndex, 1, 1, 2, 3, 4)

        'Load result to Compare array
        Dim i As Integer, j As Integer
        For i = 0 To NCohorts
            For j = 1 To 2
                Session("EC_TempPopDatabase_Comp")(i, j, gphIndex) = Session("EC_TempPopDatabase")(i, j)
            Next
            For j = 1 To 6
                Session("EC_TempDatabase_Comp")(i, j, gphIndex) = Session("EC_TempDatabase")(i, j)
            Next
        Next

        'jam 2022/07/17
        If Session("cnvtoDeaths") Then
            ConvertToDeaths(NCohorts, 3)
        End If

    End Sub

    Sub ItemClickHandlerMain(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'jam 2021/03/06
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue" 'jrs 2008/06/27
                mnuExit_Click()
            Case "Main Menu" 'jrs 2011/05/02
                Session("DisplayParent") = "frm_MainMenu.aspx"
                mnuExit_Click()
            Case "All types of Mortality"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                PackJScritpBuilder()
                mnuAllTypes_Click()
            Case "Communicable Disease"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                PackJScritpBuilder()
                mnuCommDis_Click()
            Case "Non Communicable Disease"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                PackJScritpBuilder()
                mnuNonCommDis_Click()
                    'jam 2022/07/24
                    'PackJScritpBuilder()
            Case "Injuries"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                PackJScritpBuilder()
                mnuInjuries_Click()
            Case "Communicable Disease Subtypes"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                PackJScritpBuilder()
                mnuCommSub_Click()
            Case "Non Communicable Disease Subtypes"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                PackJScritpBuilder()
                mnuNonCommSub_Click()
            Case "Injuries Subtypes"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                PackJScritpBuilder()
                mnuInjuriesSub_Click()
            Case "All Mortality Subtypes" 'dsb 2026/06/11
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                Session("bCompare") = False
                pnlCompare.Visible = False
                PackJScritpBuilder()
                mnuAllMortSub_Click()
            Case "Compare Countries", "Compare Groups" 'jrs 2012/08/17
                mnuCompare_Click()
            Case "Show Numbers"
                mnuShowData_Click(sender, e)
            Case "Scaling Factors"
                mnuShowScaling_Click()
            Case "Normalization Factors"
                mnuShowNormFac_Click()
            Case "Groups", "Countries/Regions" 'sets using Groups or Regions
                mnuGroups_Click(sender, e)
            Case "Hide Non Core Groups" 'jrs 2024/09/06
                If Session("bCheckHideNonCoreGroups") = 0 Then
                    Session("bCheckHideNonCoreGroups") = 1
                Else
                    Session("bCheckHideNonCoreGroups") = 0
                End If
                mnuHideNonCoreGroups_Click(sender, e)
            Case "Change Countries/Regions", "Change Groups" ', "Change Region"
                    'jrs 2011/11/16 mnuchngcntry_Click(sender, e)
            Case "Set Year"
                    'jrs 2011/11/16 mnuchngyr_Click(sender, e)
                    'jrs 2012/05/04
                    'Case "Base Case"
                    '    MnuBaseCase_Click(sender, e)
                    'Case "Work Case"
                    '    mnuWorkCase_Click(sender, e)
                    'Case "Advance"
                    '    mnuAdvance_Click(sender, e)
                    'Case "Regress"
                    '    mnuRegress_Click(sender, e)
            Case "On" 'Include 100+ age group
                mnuInclude100On_Click(sender, e)
            Case "Off" 'Include 100+ age group
                mnuInclude100Off_Click(sender, e)
                    'Case "Print"
                    'Case "Save"
                    'Case "About Population"
            Case "Fix Constant Scale across Time and Regions" 'jrs 2014/02/14
                mnuFixCtSc_Click(sender, e)
            Case "Fix Constant Scale across Time" 'jrs 2014/02/14
                mnuFixCteScTime_Click(sender, e)
            Case "Fix Constant Scale Manually" 'jrs 2014/02/14
                mnuFixCtScMan_Click(sender, e)
            Case "ON" 'jam 2022/0707 Convert to Deaths On
                mnuCnvtoDeathsOn_Click(sender, e)
                If Session("cnvtoDeaths") Then


                    objSender.Groups("Change Display").Items("Using Deaths").Group.Items("ON").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    objSender.Groups("Change Display").Items("Using Deaths").Group.Items("OFF").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                Else
                    objSender.Groups("Change Display").Items("Using Deaths").Group.Items("ON").IconImage = Page.ResolveUrl("~/Images//blank.gif") '"checkbox.gif"
                    objSender.Groups("Change Display").Items("Using Deaths").Group.Items("OFF").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"blank
                End If
            Case "OFF" 'jam 2022/0707 Convert to Deaths Off
                mnuCnvtoDeathsOff_Click(sender, e)

                If Not Session("cnvtoDeaths") Then
                    objSender.Groups("Change Display").Items("Using Deaths").Group.Items("ON").IconImage = Page.ResolveUrl("~/Images//blank.gif") '"checkbox.gif"
                    objSender.Groups("Change Display").Items("Using Deaths").Group.Items("OFF").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"blank.gif"
                Else

                    objSender.Groups("Change Display").Items("Using Deaths").Group.Items("ON").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    objSender.Groups("Change Display").Items("Using Deaths").Group.Items("OFF").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif
                End If

            Case Else
                'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
                If Not (vSQLiteCon Is Nothing) Then
                    vSQLiteCon.Close()
                    vSQLiteCon.Dispose()
                End If

                'jrs 2011/11/14
                Dim idx As Integer, sUrl As String, sParY As String, sParF As String, sParG As String, pos As Integer
                'jam 2021/03/06
                If Not IsNothing(Request.QueryString("Year")) Then 'jam 2021/03/15 Reactivating normal URL
                    sParY = "&Year=" & Request.QueryString("Year")
                Else
                    'jam 2025/01/25
                    'sParY = ""

                    If Not IsNothing(Session("vYear")) Then
                        sParY = "&Year=" & Session("vYear")
                    Else
                        sParY = ""
                    End If


                End If

                'If Not IsNothing(Session("vYear")) Then

                '    sParY = Session("vYear")
                'Else
                '    sParY = ""
                'End If

                'jam 2021/03/06
                If Not IsNothing(Request.QueryString("File")) Then 'jam 2021/03/15 Reactivating normal URL
                    sParF = "&File=" & Request.QueryString("File")
                Else
                    'jrs 2024/04/24
                    If Not IsNothing(Session("vScenario")) Then
                        sParF = "&File=" & Session("vScenario")
                    Else
                        sParF = ""
                    End If
                End If

                'If Not IsNothing(Session("vScenario")) Then

                '    sParF = Session("vScenario")
                'Else
                '    sParF = ""
                'End If

                'jam 2021/03/06
                'If Not IsNothing(Session("vCountry")) And Not Session("IsGroup") Then
                If Not IsNothing(Request.QueryString("Country")) Then 'jam 2021/03/15 Reactivating normal URL
                    'jam 2021/03/16
                    sParG = "Country=" & Request.QueryString("Country")
                    'sParG = Session("vCountry")
                    'jam 2021/03/06
                ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2012/06/27
                    'ElseIf Not IsNothing(Session("vCountry")) And Session("IsGroup") Then 'JAM 2020-10-10
                    sParG = "Group=" & Request.QueryString("Group")
                    'sParG = Session("vCountry")
                    'jrs 2024/04/24 it was using the wrong flag
                ElseIf Session("EdCohort_Grouping") = False Then
                    sParG = "Country=" & IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
                Else
                    sParG = "Group=" & Session("EdCohort_regionname")
                End If

                If objEA.Item.UserData = "Reg" Then

                    'jam 2021/03/16
                    Session("vCountry") = IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code

                    'Server Transfer doesn't change the URL, which we want to allow to be saved by user

                    'jam 2025/01/25
                    Response.Redirect("frm_MortCohorts.aspx?Country=" & IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code & sParY & sParF)

                    'Page.ResolveUrl("~/frm_MortCohorts.aspx?Country=" & IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code & sParY & sParF)

                    'JAM 2021/03/06
                    'Vpath = Request.GetFriendlyUrlFileVirtualPath
                    'Vext = Request.GetFriendlyUrlFileExtension
                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
                    'vurl = FriendlyUrl.Href("~/frm_MortCohorts.aspx", IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code, sParY, sParF)
                    'Response.Redirect(vurl)

                ElseIf objEA.Item.UserData = "Grp" Then

                    'jam 2021/03/16
                    Session("vCountry") = PrepareForURL(objEA.Item.Caption)

                    'jam 2021/03/06
                    Response.Redirect("frm_MortCohorts.aspx?Group=" & PrepareForURL(objEA.Item.Caption) & sParY & sParF)

                    'Vpath = Request.GetFriendlyUrlFileVirtualPath
                    'Vext = Request.GetFriendlyUrlFileExtension
                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
                    'vurl = FriendlyUrl.Href("~/frm_MortCohorts", MortPrepareForFriendlyURL(objEA.Item.Caption), sParY, sParF)
                    'Response.Redirect(vurl)



                ElseIf objEA.Item.UserData = "Yr" Then
                    'jrs 2012/05/04
                    If IsNumeric(objEA.Item.Caption) Then
                        lstYear.SelectedIndex = CInt(CInt(Trim(objEA.Item.Caption)) - Session("FirstYear"))
                        lstYear_SelectedIndexChanged(objSender, New EventArgs)
                    End If

                    'jam 2021/03/16
                    Session("vYear") = objEA.Item.Caption

                    'jam 2021/03/06
                    Response.Redirect("frm_MortCohorts.aspx?" & sParG & "&Year=" & objEA.Item.Caption & sParF)

                    'Vpath = Request.GetFriendlyUrlFileVirtualPath
                    'Vext = Request.GetFriendlyUrlFileExtension
                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
                    'vurl = FriendlyUrl.Href("~/frm_MortCohorts", sParG, objEA.Item.Caption, sParF)
                    'Response.Redirect(vurl)


                ElseIf objEA.Item.Caption = "Advance" Then
                    'cdv 2021/05/19 Readjust to be a year divisible by 5
                    If (Session("EdCohort_tempyear") + 1) Mod 5 = 0 Then
                        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") + 1
                    ElseIf Session("EdCohort_tempyear") Mod 5 = 0 Then
                        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") + 5
                    Else
                        While Session("EdCohort_tempyear") Mod 5 <> 0
                            Session("EdCohort_tempyear") = Session("EdCohort_tempyear") + 1
                        End While
                    End If
                    If Session("EdCohort_tempyear") > Session("PopCohort_year_run") Then Session("EdCohort_tempyear") = Session("PopCohort_year_run")

                    'jam 2021/03/16
                    Session("vYear") = Session("EdCohort_tempyear")

                    'jam 2021/03/15 Reactivating normal URL
                    Response.Redirect("frm_MortCohorts.aspx?" & sParG & "&Year=" & Session("EdCohort_tempyear") & sParF)

                    'jam 2021/03/06
                    'Vpath = Request.GetFriendlyUrlFileVirtualPath
                    'Vext = Request.GetFriendlyUrlFileExtension
                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
                    'vurl = FriendlyUrl.Href("~/frm_MortCohorts", sParG, Session("EdCohort_tempyear"), sParF)
                    'Response.Redirect(vurl)



                ElseIf objEA.Item.Caption = "Regress" Then
                    If Session("EdCohort_tempyear") Mod 5 = 0 Then
                        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") - 5
                    ElseIf (Session("EdCohort_tempyear") - 1) Mod 5 = 0 Then
                        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") - 1
                    Else
                        While Session("EdCohort_tempyear") Mod 5 <> 0
                            Session("EdCohort_tempyear") = Session("EdCohort_tempyear") - 1
                        End While
                    End If
                    If Session("EdCohort_tempyear") < Session("FirstYearSce") Then Session("EdCohort_tempyear") = Session("FirstYearSce")


                    'jam 2021/03/16
                    Session("vYear") = Session("EdCohort_tempyear")


                    'jam 2021/03/15 Reactivating normal URL
                    'jam 2021/03/06
                    Response.Redirect("frm_MortCohorts.aspx?" & sParG & "&Year=" & Session("EdCohort_tempyear") & sParF)

                    'jam 2021/03/06
                    'Vpath = Request.GetFriendlyUrlFileVirtualPath
                    'Vext = Request.GetFriendlyUrlFileExtension
                    'vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
                    'vurl = FriendlyUrl.Href("~/frm_MortCohorts", sParG, Session("EdCohort_tempyear"), sParF)
                    'Response.Redirect(vurl)
                Else
                    'jrs 2012/05/04 mnuSce_Click(objEA.Item.Caption, objSender)
                    'jam 2021/03/15 Reactivating normal URL
                    'jam 2021/03/06
                    'Response.Redirect("frm_MortCohorts.aspx?" & sParG & sParY & "&File=" & objEA.Item.Caption)

                    If Not IsNothing(Session("vYear")) Then

                        sParY = Session("vYear")
                    Else
                        sParY = ""
                    End If

                    If Not IsNothing(Session("vScenario")) Then

                        sParF = Session("vScenario")
                    Else
                        sParF = ""
                    End If


                    If Not IsNothing(Session("vCountry")) Then

                        sParG = Session("vCountry")
                    Else
                        sParG = ""
                    End If

                    'sParG = Session("vCountry")


                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
                    vurl = FriendlyUrl.Href("~/frm_MortCohorts", sParG, sParY, objEA.Item.Caption)
                    Response.Redirect(vurl)

                End If
        End Select
        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

    End Sub

    Private Sub mnuExit_Click()
        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

        Session.Remove("EC_TempDatabase")
        Session.Remove("EC_TempPopDatabase")

        Session.Remove("bShow100")
        Session.Remove("POPCG_Case_of_Work")

        Session.Remove("CommDis")
        Session.Remove("NonCommDis")
        Session.Remove("Injuries")

        Session.Remove("POPMale")
        Session.Remove("POPFemale")

        'jrs 2009/03/18
        Session.Remove("lblDataOrg")
        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If

        'jam 2021/03/06
        Session.Remove("IsGroup")

        'jam 2021/03/13
        Session.Remove("vCountry")
        Session.Remove("vYear")
        Session.Remove("vScenario")


        'jam 2021/03/06

        ' Support.FormManager.Show(Me, Session("DisplayParent"))

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        'jam 2021/03/06
        If Session("DisplayParent") = Nothing Or Session("DisplayParent") = "frm_MainMenu.aspx" Then
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_MainMenu.aspx")

            'Change REgion
            vurl = FriendlyUrl.Href("~/frm_MainMenu")
        Else
            vfriendlyurl = FriendlyUrl.Resolve("~/" & Session("DisplayParent"))
            'jam 2021/05/03
            'vurl = FriendlyUrl.Href("~/frm_MainMenu")
            vurl = FriendlyUrl.Href("~/" & Session("DisplayParent"))
        End If

        Response.Redirect(vurl)
    End Sub

    Protected Sub cntrylist_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cntrylist.SelectedIndexChanged
        '  Specify the country selected
        '  ----------------------------
        If cntrylist.SelectedIndex = -1 Then
            Session("EdCohort_tempregion") = 1
        Else
            Session("EdCohort_tempregion") = cntrylist.SelectedIndex + 1
        End If

        Session("EdCohort_regionname") = GRPS(1, Session("EdCohort_tempregion"))
        cntrylist.Visible = False

        'jrs 2014/01/29
        If Session("mnuFixCteScTime") Then
            SetUpMaxsTime()
        End If

        'jrs 2014/02/14
        If Session("bCompare") = False Then
            Graph_Draw((Session("EdCohort_GraphType")))
        End If

    End Sub

    Public Sub mnuchngcntry_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = "checkbox.gif" Then
            cntrylist.Visible = True
            lstGroups.Visible = False
        Else
            lstGroups.Visible = True
            cntrylist.Visible = False
        End If
    End Sub

    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/03/06
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If objEA.Item.Caption = "Groups" Then
            objSender.Groups("Set Group or Country/Region").Items("Groups").IconImage = "checkbox.gif"
            objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = "blank.gif"
            '"Change Display"
            objSender.Groups("Change Display").Items(0).Caption = "Change Groups" '"Change Countries/Regions"
            Session("EdCohort_Grouping") = True

            'jam 2021/03/06
            Session("IsGroup") = True

            'jrs 2012/08/27
            objSender.Groups("Distribution Type").Items("mnuCompare").Caption = "Compare Groups" '"Change Countries/Regions"

            'jrs 2008/09/05
            'Search for default group
            Dim i As Integer
            For i = 0 To lstGroups.Items.Count - 1
                If lstGroups.Items(i).Text = sDefGrp Then
                    lstGroups.Items(i).Selected = True
                    'jrs 2020/06/02 lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
                    Session("EdCohort_regionname") = lstGroups.SelectedValue
                    If Session("EdCohort_regionname") = "" Then Session("EdCohort_regionname") = lstGroups.Items(0).Text
                    lstGroups.Visible = False
                    Exit For
                End If
            Next i
            cntrylist.Visible = False
            lstYear.Visible = False
            'jrs 2011/05/05
            Session.Remove("MMCountry")
            'jrs 2011/09/22
            Session("MMGroup") = Session("EdCohort_regionname")


            'jam 2021/03/06
            'Support.FormManager.Show(Me, "frm_MortCohorts.aspx?Group=" & PrepareForURL(Session("EdCohort_regionname")))


            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")


            vurl = FriendlyUrl.Href("~/frm_MortCohorts", MortPrepareForFriendlyURL(Session("EdCohort_regionname")))

            Response.Redirect(vurl)

        Else
            objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = "checkbox.gif"
            objSender.Groups("Set Group or Country/Region").Items("Groups").IconImage = "blank.gif"
            objSender.Groups("Change Display").Items(0).Caption = "Change Countries/Regions"
            Session("EdCohort_Grouping") = False

            'jam 2021/03/06
            Session("IsGroup") = False

            'jrs 2012/08/27
            objSender.Groups("Distribution Type").Items("mnuCompare").Caption = "Compare Countries" '"Change Countries/Regions"

            'jrs 2014/02/06
            Dim sCtryCode As String = "AF"
            If Session("sDefReg") > "" Then
                sCtryCode = Session("sDefReg")
            ElseIf Session("EdCohort_tempregion") > 0 And cntrylist.SelectedIndex = -1 And IFsRegs.Item(Session("EdCohort_regionname")).Index = Session("EdCohort_tempregion") Then
                cntrylist.SelectedIndex = Session("EdCohort_tempregion") - 1
                sCtryCode = IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
            ElseIf Session("EdCohort_tempregion") > 0 And IFsRegs.Item(Session("EdCohort_regionname")).Index = Session("EdCohort_tempregion") Then 'jrs 2024/09/06
                sCtryCode = IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
            End If

            cntrylist_SelectedIndexChanged(cntrylist, New System.EventArgs)
            lstGroups.Visible = False
            lstYear.Visible = False

            'jam 2021/03/06
            'jrs 2011/05/05
            'Support.FormManager.Show(Me, "frm_MortCohorts.aspx?Country=AF")


            'jam 2021/03/06
            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
            vurl = FriendlyUrl.Href("~/frm_MortCohorts", MortPrepareForFriendlyURL(sCtryCode))
            Response.Redirect(vurl)
        End If

    End Sub

    'jrs 2024/09/06
    Public Sub mnuHideNonCoreGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/03/06
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If Session("EdCohort_Grouping") = True Then
            'lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
            cntrylist.Visible = False
            lstYear.Visible = False
            Session("sDefReg") = Session("MMCountry")
            Session.Remove("MMCountry")
            Session("MMGroup") = Session("EdCohort_regionname")

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
            vurl = FriendlyUrl.Href("~/frm_MortCohorts", MortPrepareForFriendlyURL(Session("EdCohort_regionname")))

            Response.Redirect(vurl)

        Else
            Dim sCtryCode As String = "AF"
            If Session("sDefReg") > "" Then
                sCtryCode = Session("sDefReg")
            ElseIf Session("EdCohort_tempregion") > 0 And cntrylist.SelectedIndex = -1 And IFsRegs.Item(Session("EdCohort_regionname")).Index = Session("EdCohort_tempregion") Then
                cntrylist.SelectedIndex = Session("EdCohort_tempregion") - 1
                sCtryCode = IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
            ElseIf Session("EdCohort_tempregion") > 0 And IFsRegs.Item(Session("EdCohort_regionname")).Index = Session("EdCohort_tempregion") Then
                sCtryCode = IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
            End If

            cntrylist_SelectedIndexChanged(cntrylist, New System.EventArgs)
            lstGroups.Visible = False
            lstYear.Visible = False

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")
            vurl = FriendlyUrl.Href("~/frm_MortCohorts", MortPrepareForFriendlyURL(sCtryCode))
            Response.Redirect(vurl)
        End If


    End Sub

    Protected Sub lstGroups_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGroups.SelectedIndexChanged
        Session("EdCohort_regionname") = lstGroups.SelectedValue
        If Session("EdCohort_regionname") = "" Then Session("EdCohort_regionname") = lstGroups.Items(0).Text
        lstGroups.Visible = False
        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    Public Sub mnuchngyr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        lstYear.Visible = True
    End Sub

    Protected Sub lstYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstYear.SelectedIndexChanged
        '--------------------------------------------------------------

        '  Specify the country selected
        '  ----------------------------
        If lstYear.SelectedIndex = -1 Then
            Session("EdCohort_tempyear") = Session("FirstYearSce")
        Else
            Session("EdCohort_tempyear") = Session("FirstYearSce") + lstYear.SelectedIndex
        End If

        If Session("EdCohort_tempyear") < Session("FirstYearSce") Then Session("EdCohort_tempyear") = Session("FirstYearSce")
        If Session("EdCohort_tempyear") > Session("PopCohort_year_run") Then Session("EdCohort_tempyear") = Session("PopCohort_year_run")
        lstYear.Visible = False

        '   Allow or disallow forward and backward movement
        '   -----------------------------------------------

        If Session("EdCohort_tempyear") > Session("FirstYearSce") Then
            wmMain.Groups("Time").Items("Regress").Enabled = True
        Else
            wmMain.Groups("Time").Items("Regress").Enabled = False
        End If
        If Session("EdCohort_tempyear") < Session("PopCohort_year_run") Then
            wmMain.Groups("Time").Items("Advance").Enabled = True
        Else
            wmMain.Groups("Time").Items("Advance").Enabled = False
        End If
        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    Private Sub mnuSce_Click(ByVal sSceName As String, ByRef objSender As Coalesys.WebMenu.WebMenu)
        'jam 2021/03/13
        'objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = "blank.gif"
        'objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = "blank.gif"

        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"

        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            'jam 2021/03/13
            'If objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).Caption = sSceName Then
            '    objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = "checkbox.gif"
            '    Case_of_Work = i + 2
            'Else
            '    objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = "blank.gif"
            'End If
            If objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).Caption = sSceName Then
                objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                Case_of_Work = i + 2
            Else
                objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            End If
        Next

        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        CaseName = sSceName
        Session("EdCG_Casename") = CaseName

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"


        'JAM 2020-06-08
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"

        Else
            Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".db"
        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Session("PopCohort_year_run") = FindLastYear()
        Session("EdCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("EdCohort_tempyear"))
        Session("FirstYearSce") = FindFirstYear()

        If Session("PopCohort_year_run") <= FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearHistory
        ElseIf Session("EdCohort_tempyear") < FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearForecast
        End If

        If Session("EdCohort_tempyear") = Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("EdCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If

        Graph_Draw((Session("EdCohort_GraphType")))

    End Sub

    Private Function FindLastYear()
        Dim rsLY As DataTable
        'SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)
        'If Not rsLY.Rows.Count = 0 Then
        '    FindLastYear = rsLY.Rows(0).Item("MaxYear")
        'Else
        '    FindLastYear = 0
        'End If

        'JAM 2020-06-08
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)
            If Not rsLY.Rows.Count = 0 Then
                FindLastYear = rsLY.Rows(0).Item("MaxYear")
            Else
                FindLastYear = 0
            End If

        Else
            'SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT Max(TimeId) As MaxYear FROM " + TableName(1)
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            FindLastYear = 0
            While (vSQLiteReader.Read())
                FindLastYear = vSQLiteReader("MaxYear")

            End While


        End If

    End Function

    Private Function FindFirstYear()
        Dim rsFY As DataTable
        'SetDataTablefromAccess(Session("db2Path"), "SELECT Min(Years) As MinYear FROM " + TableName(1), "", rsFY)
        'If Not rsFY.Rows.Count = 0 Then
        '    FindFirstYear = rsFY.Rows(0).Item("MinYear")
        'Else
        '    FindFirstYear = 0
        'End If


        'JAM 2020-05-30
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            SetDataTablefromAccess(Session("db2Path"), "SELECT Min(Years) As MinYear FROM " + TableName(1), "", rsFY)
            If Not rsFY.Rows.Count = 0 Then
                FindFirstYear = rsFY.Rows(0).Item("MinYear")
            Else
                FindFirstYear = 0
            End If

        Else

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT Min(TimeId) As MinYear FROM " + TableName(1)
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            FindFirstYear = 0
            While (vSQLiteReader.Read())
                FindFirstYear = vSQLiteReader("MinYear")

            End While



        End If

    End Function

    Public Sub MnuBaseCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'jam 2021/03/06
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = "blank.gif"
        Next
        Case_of_Work = 0
        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        'If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
        '    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
        'Else
        '    Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
        'End If


        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            Else

                Session("SQLiteDB") = "IFsPop.db"

                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.db"
            End If

        Else
            'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            Else

                Session("SQLiteDB") = "IFsPopHist.db"

                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"
            End If


        End If


        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        If Session("EdCohort_tempyear") < Session("FirstYear") Then
            Session("EdCohort_tempyear") = Session("FirstYear")
        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And Session("EdCohort_tempyear") > LastYearHistory Then
            Session("EdCohort_tempyear") = FirstYearHistory
        End If
        Session("FirstYearSce") = Session("FirstYear")

        CaseName = "Base Case"
        Session("EdCG_Casename") = CaseName
        'End If
        If Session("EdCohort_tempyear") = Session("FirstYear") Then objSender.Groups("Time").Items("Regress").Enabled = False
        If Session("EdCohort_tempyear") < Session("LastYear") Then objSender.Groups("Time").Items("Advance").Enabled = True
        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    Private Sub mnuHistSce_Click(ByRef objSender As Coalesys.WebMenu.WebMenu)

        Dim i As Integer
        For i = 0 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            'jam 2021/03/13
            'objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = "blank.gif"
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Next

        CaseName = "Historical Base"
        Session("EdCG_Casename") = CaseName

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

        Else

            Session("SQLiteDB") = "IFsPopHist.db"
            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"
        End If

        Session("PopCohort_year_run") = FindLastYear()
        Session("EdCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("EdCohort_tempyear"))
        Session("FirstYearSce") = FindFirstYear()

        If Session("PopCohort_year_run") <= FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearHistory
        ElseIf Session("EdCohort_tempyear") < FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearForecast
        End If

        If Session("EdCohort_tempyear") = Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("EdCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("EdCohort_GraphType")))

    End Sub

    Public Sub mnuWorkCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '--------------------------------------------------------------

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'jam 2021/03/06
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"

        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            'jam 2021/03/06
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Next

        Case_of_Work = 1
        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

        Else
            'JAM 2020-06-08
            Session("SQLiteDB") = "IFsPopWork.db"

            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

        End If


        Session("PopCohort_year_run") = FindLastYear()
        Session("EdCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("EdCohort_tempyear"))

        If Session("EdCohort_tempyear") < FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearForecast
        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And Session("EdCohort_tempyear") > LastYearHistory Then
            Session("EdCohort_tempyear") = FirstYearHistory
        End If
        Session("FirstYearSce") = Session("FirstYear")

        CaseName = "Working Scenario"
        Session("EdCG_Casename") = CaseName

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        If Session("EdCohort_tempyear") = Session("FirstYear") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("EdCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If
        If Session("PopCohort_year_run") = Session("FirstYear") Then
            Msg = "You must run the model to create a working case"
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
        Graph_Draw((Session("EdCohort_GraphType")))

    End Sub

    Public Sub mnuAdvance_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim end_year As Object
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        '--------------------------------------------------------------
        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") + 5

        If Case_of_Work = 0 Then
            end_year = FindLastYear()
            If end_year = 0 Then
                end_year = Session("LastYearForecast")
            End If
        Else
            end_year = Session("PopCohort_year_run")
        End If

        If Session("EdCohort_tempyear") <= end_year Then
            Graph_Draw((Session("EdCohort_GraphType")))
        End If
        If Session("EdCohort_tempyear") >= end_year Then
            objSender.Groups("Time").Items("Advance").Enabled = False
            Session("EdCohort_tempyear") = end_year
        End If
        objSender.Groups("Time").Items("Regress").Enabled = True
    End Sub

    Public Sub mnuRegress_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If Session("EdCohort_tempyear") <= Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            Session("EdCohort_tempyear") = Session("EdCohort_tempyear") - 5
            If Session("EdCohort_tempyear") <= Session("FirstYearSce") Then
                Session("EdCohort_tempyear") = Session("FirstYearSce")
                objSender.Groups("Time").Items("Regress").Enabled = False
            End If
            objSender.Groups("Time").Items("Advance").Enabled = True
            Graph_Draw((Session("EdCohort_GraphType")))
        End If

    End Sub

    Private Sub mnuInclude100On_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        'jam 2021/03/06
        objSender.Groups("mnuAge100+").Items("mnuAge100+On").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        objSender.Groups("mnuAge100+").Items("mnuAge100+Off").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Session("bShow100") = True

        'If mnuFixCtSc.Checked Then
        '    SetUpMaxs()
        'ElseIf mnuFixCteScTime.Checked Then 'jrs 2007/07/31
        '    SetUpMaxsTime()
        'End If
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub mnuInclude100Off_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        'jam 2021/03/06
        objSender.Groups("mnuAge100+").Items("mnuAge100+Off").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        objSender.Groups("mnuAge100+").Items("mnuAge100+On").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Session("bShow100") = False

        'If mnuFixCtSc.Checked Then
        '    SetUpMaxs()
        'ElseIf mnuFixCteScTime.Checked Then 'jrs 2007/07/31
        '    SetUpMaxsTime()
        'End If
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Public Sub mnuShowData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------

        Select Case Session("EdCG_Index")
            Case 0
                CompositeFillDataGrid()
            Case 1, 2, 3
                SeparateFillDataGrid(Session("EdCG_Index"))
            Case 4
                CompositeFillDataGridAll(CommDis)
            Case 5
                CompositeFillDataGridAll(NonCommDis)
            Case 6
                CompositeFillDataGridAll(Injuries)
            Case 7 'dsb 2026/06/11
                CompositeFillDataGridAll(AllMortSubTypes)
        End Select

        frameData.Visible = True
        grdcohort.Visible = True
        lbldata.Visible = True
        hfTitle.Value = lbldata.Text.Replace("<br/>", "")
        'jrs 2009/03/19
        lblJS.Visible = False
        grdcohort.HeaderVisible = True
        grdcohortDet.Visible = False
        lblJSLine.Visible = False
        cmdLine.Visible = False
    End Sub

    Private Sub CompositeFillDataGrid()
        '--------------------------------------------------------------
        '4,1,2,3

        Dim iNCohorts As Integer
        If Session("bShow100") Then
            iNCohorts = NCohorts
        Else
            iNCohorts = NCohorts - 1
        End If

        Dim i As Integer
        'jrs 2009/03/17 changed from 2 to 1, adding headers
        grdcohort.TotalRows = iNCohorts + 1
        grdcohort.RowCount = iNCohorts + 1

        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        For i = 1 To 11
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            If i > 1 And i < 9 Then
                myCol.Width = 150 '1300
            Else
                myCol.Width = 125 '1100
            End If

            'jrs 2009/03/17
            If i > 1 And Request.Browser.Type.StartsWith("IE") Then
                myCol.Type = TMSWebControls.Column.ColumnType.LinkField
            End If
            myCol.HeaderVAlignment = TMSWebControls.Column.VAligning.Top
            myCol.HeaderAlignment = TMSWebControls.Column.Aligning.Center
            grdcohort.Columns.Add(myCol)
        Next


        'cdv 2021/01/14 ****START**** Change data orders and labels to match gender

        Dim DataIndex1 As Integer
        Dim DataIndex2 As Integer
        Dim DataIndex3 As Integer

        Dim DataIndex4 As Integer
        Dim DataIndex5 As Integer
        Dim DataIndex6 As Integer

        If bMaleFirst Then
            DataIndex1 = 1
            DataIndex2 = 2
            DataIndex3 = 3

            DataIndex4 = 4
            DataIndex5 = 5
            DataIndex6 = 6
        Else
            DataIndex1 = 4
            DataIndex2 = 5
            DataIndex3 = 6

            DataIndex4 = 1
            DataIndex5 = 2
            DataIndex6 = 3
        End If

        Dim l As Integer

        For l = iNCohorts To 0 Step -1

            If l = NCohorts Then
                grdcohort(0, l) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
            ElseIf l = 0 Then
                grdcohort(0, l) = "Infants"
            ElseIf l = 1 Then
                grdcohort(0, l) = "1- 4"
            Else
                grdcohort(0, l) = Str$(l * 5 - 5) + "-" + Str$(l * 5 - 1)
            End If

            'male
            'Communicable Disease
            grdcohort(1, l) = "<u><font color=""#0000ff"">" & Format(Session("EC_TempDatabase")(l%, DataIndex1), "0.0000") & "</font></u>"

            'Non Communicable Disease
            grdcohort(2, l) = "<u><font color=""#0000ff"">" & Format(Session("EC_TempDatabase")(l%, DataIndex2), "0.0000") & "</font></u>"

            'Injury
            grdcohort(3, l) = "<u><font color=""#0000ff"">" & Format(Session("EC_TempDatabase")(l%, DataIndex3), "0.0000") & "</font></u>"

            'female
            'Communicable Disease
            grdcohort(4, l) = "<u><font color=""#0000ff"">" & Format(Session("EC_TempDatabase")(l%, DataIndex4), "0.0000") & "</font></u>"

            'Non Communicable Disease
            grdcohort(5, l) = "<u><font color=""#0000ff"">" & Format(Session("EC_TempDatabase")(l%, DataIndex5), "0.0000") & "</font></u>"

            'Injury
            grdcohort(6, l) = "<u><font color=""#0000ff"">" & Format(Session("EC_TempDatabase")(l%, DataIndex6), "0.0000") & "</font></u>"

            'total
            'dsb 2026/06/29 When showing number of deaths, total should be Male + Female
            If Session("cnvtoDeaths") Then
                'Communicable Disease
                grdcohort(7, l) = Format(Session("EC_TempDatabase")(l%, 1) + Session("EC_TempDatabase")(l%, 4), "0.0000")
                'Non Communicable Disease
                grdcohort(8, l) = Format(Session("EC_TempDatabase")(l%, 2) + Session("EC_TempDatabase")(l%, 5), "0.0000")
                'Injury
                grdcohort(9, l) = Format(Session("EC_TempDatabase")(l%, 3) + Session("EC_TempDatabase")(l%, 6), "0.0000")
            Else 'When showing deaths per 1000 people, total should be weighted average
                If Session("EC_TempPopDatabase")(l%, 1) + Session("EC_TempPopDatabase")(l%, 2) > 0 Then
                    'Communicable Disease
                    grdcohort(7, l) = Format((Session("EC_TempDatabase")(l%, 1) * Session("EC_TempPopDatabase")(l%, 1) +
                Session("EC_TempDatabase")(l%, 4) * Session("EC_TempPopDatabase")(l%, 2)) /
                    (Session("EC_TempPopDatabase")(l%, 1) + Session("EC_TempPopDatabase")(l%, 2)), "0.0000")
                    'Non Communicable Disease
                    grdcohort(8, l) = Format((Session("EC_TempDatabase")(l%, 2) * Session("EC_TempPopDatabase")(l%, 1) +
                Session("EC_TempDatabase")(l%, 5) * Session("EC_TempPopDatabase")(l%, 2)) /
                    (Session("EC_TempPopDatabase")(l%, 1) + Session("EC_TempPopDatabase")(l%, 2)), "0.0000")
                    'Injury
                    grdcohort(9, l) = Format((Session("EC_TempDatabase")(l%, 3) * Session("EC_TempPopDatabase")(l%, 1) +
                Session("EC_TempDatabase")(l%, 6) * Session("EC_TempPopDatabase")(l%, 2)) /
                    (Session("EC_TempPopDatabase")(l%, 1) + Session("EC_TempPopDatabase")(l%, 2)), "0.0000")
                Else
                    grdcohort(7, l) = "0.0000"
                    grdcohort(8, l) = "0.0000"
                    grdcohort(9, l) = "0.0000"
                End If
            End If

            'Total of Totals
            grdcohort(10, l) = "<u><font color=""#0000ff"">" & Format(CSng(grdcohort(7, l)) + CSng(grdcohort(8, l)) + CSng(grdcohort(9, l)), "0.0000") & "</font></u>"

            grdcohort(7, l) = "<u><font color=""#0000ff"">" & grdcohort(7, l) & "</font></u>"
            grdcohort(8, l) = "<u><font color=""#0000ff"">" & grdcohort(8, l) & "</font></u>"
            grdcohort(9, l) = "<u><font color=""#0000ff"">" & grdcohort(9, l) & "</font></u>"
        Next l%
        Dim GenderLabel1 As String
        Dim GenderLabel2 As String

        If bMaleFirst Then
            GenderLabel1 = "Male"
            GenderLabel2 = "Female"
        Else
            GenderLabel1 = "Female"
            GenderLabel2 = "Male"
        End If
        With grdcohort
            .Columns(0).title = "Age Cohort"
            .Columns(1).title = "Com Disease " & GenderLabel1
            .Columns(2).title = "Non Com Disease " & GenderLabel1
            .Columns(3).title = "Injury " & GenderLabel1
            .Columns(4).title = "Com Disease " & GenderLabel2
            .Columns(5).title = "Non Com Disease " & GenderLabel2
            .Columns(6).title = "Injury " & GenderLabel2
            .Columns(7).title = "Com Disease Total"
            .Columns(8).title = "Non Com Disease Total"
            .Columns(9).title = "Injury Total"
            .Columns(10).title = "Total Mortality"
        End With
        'cdv 2021/01/14 ****END****
        lbldata.Text = "Mortality Distribution for " +
            Session("EdCohort_regionname") + " in Year " + Session("EdCohort_tempyear").ToString + " [" + Session("EdCG_Casename") + "]" +
            " - Number of Deaths per 1000 People by Type (only) and Gender" 'gphAgedst(gphIndex).BottomTitle

    End Sub

    Protected Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        'jrs 2009/03/19
        If grdcohortDet.Visible Then
            grdcohortDet.Visible = False
            grdcohort.Visible = True
            lbldata.Text = Session("lblDataOrg")
            hfTitle.Value = lbldata.Text.Replace("<br/>", "")
            cmdLine.Visible = False
        ElseIf lblJSLine.Visible Then
            lblJSLine.Visible = False
            grdcohortDet.Visible = True
        Else
            frameData.Visible = False
            grdcohort.Visible = False
            lbldata.Visible = False
            lblJS.Visible = True
        End If
    End Sub

    Protected Sub cmdGridSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGridSave.Click
        Dim ii As Short
        Dim l As Short
        Dim CDERR_CANCEL As Object
        Dim GraphTitle As String
        'jrs 2012/01/27 
        '            Dim DataOutputFileName As String
        '            '--------------------------------------------------------------

        '            '   Get filename
        '            '   --------------

        '            On Error GoTo SelDataFileError_Line
        '            DataOutputFileName = Session("PutFdrvRun") & "Save.csv" 'jrs 2008/06/25 cmd_ScriptFile.Filename
        '            GoTo WriteDataFile_Line

        'SelDataFileError_Line:
        '            If Err.Number = CDERR_CANCEL Then
        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            Else
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            End If

        'WriteDataFile_Line:
        '            Dim fNum As Integer
        '            fNum = FreeFile()
        '            FileOpen(fNum, DataOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        'jrs 2012/01/27 
        'PrintLine(fNum, lbldata.Text)
        'PrintLine(fNum, New String("-", Len(lbldata.Text)))
        Context.Response.Write(lbldata.Text)
        Context.Response.Write(Environment.NewLine)
        Context.Response.Write(New String("-", Len(lbldata.Text)))
        Context.Response.Write(Environment.NewLine)

        'jrs 2009/03/19
        If grdcohort.Visible Then
            For ii = 0 To grdcohort.Columns.Count - 1
                'jrs 2012/01/27 Print(fNum, grdcohort.Columns(ii).title & ",")
                Context.Response.Write(grdcohort.Columns(ii).title & ",")
            Next
            'jrs 2012/01/27 PrintLine(fNum, TAB)
            Context.Response.Write(Environment.NewLine)
            For l = 0 To grdcohort.TotalRows - 1
                For ii = 0 To grdcohort.Columns.Count - 1
                    'jrs 2012/01/27 Print(fNum, grdcohort(ii, l) & ",")
                    Context.Response.Write(Replace(Replace(grdcohort(ii, l), "<u><font color=""#0000ff"">", ""), "</font></u>", "") & ",")
                Next ii
                'jrs 2012/01/27 PrintLine(fNum, TAB)
                Context.Response.Write(Environment.NewLine)
            Next l
        Else
            For ii = 0 To grdcohortDet.Columns.Count - 1
                'jrs 2012/01/27 Print(fNum, grdcohortDet.Columns(ii).title & ",")
                Context.Response.Write(grdcohortDet.Columns(ii).title & ",")
            Next
            'jrs 2012/01/27 PrintLine(fNum, TAB)
            Context.Response.Write(Environment.NewLine)
            For l = 0 To grdcohortDet.TotalRows - 1
                For ii = 0 To grdcohortDet.Columns.Count - 1
                    'jrs 2012/01/27 Print(fNum, grdcohortDet(ii, l) & ",")
                    Context.Response.Write(grdcohortDet(ii, l) & ",")
                Next ii
                'jrs 2012/01/27 PrintLine(fNum, TAB)
                Context.Response.Write(Environment.NewLine)
            Next l
        End If

        'jrs 2012/01/27 FileClose(fNum)

        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(DataOutputFileName)
        Response.End()
        '**************************************

    End Sub

    Public Sub mnuShowScaling_Click()
        '--------------------------------------------------------------
        lbldata.Text = "Scaling Factors for " & Session("EdCohort_regionname")

        If Session("EdCohort_GraphType") = 0 Then
            CompositeFillFactorGrid()
        Else
            Select Case Session("EdCohort_GraphType")
                Case 1, 4
                    SeparateFillFactorGrid(Session("EdCohort_GraphType"), CommDis)
                Case 2, 5
                    SeparateFillFactorGrid(Session("EdCohort_GraphType"), NonCommDis)
                Case 3, 6
                    SeparateFillFactorGrid(Session("EdCohort_GraphType"), Injuries)
            End Select
        End If

        frameData.Visible = True
        grdcohort.Visible = True
        lbldata.Visible = True
        hfTitle.Value = lbldata.Text
        'jrs 2009/03/20
        lblJS.Visible = False
        grdcohortDet.Visible = False
        cmdLine.Visible = False
        lblJSLine.Visible = False
    End Sub

    Private Sub CompositeFillFactorGrid()
        '--------------------------------------------------------------

        Dim TempFactDatabase(0, 0) As Single
        ReDim TempFactDatabase(NCohorts, 30) 'jrs 2011/08/30 from 20
        'jrs 2007/03/08 Optional display of 100+ age group
        Dim iNCohorts As Integer
        If Session("bShow100") Then
            iNCohorts = NCohorts
        Else
            iNCohorts = NCohorts - 1
        End If

        Dim i As Integer
        grdcohort.TotalRows = iNCohorts + 2
        grdcohort.RowCount = iNCohorts + 2

        grdcohort.Columns.Clear()
        'jrs 2009/03/19
        grdcohort.HeaderVisible = False
        Dim myCol As TMSWebControls.Column

        For i = 1 To 31 'jrs 2011/08/30 21
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            If i = 1 Then
                myCol.Width = 125
            Else
                myCol.Width = 150
            End If

            grdcohort.Columns.Add(myCol)
        Next

        'Dim Conn As OleDb.OleDbConnection
        'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))
        'Conn.Open()

        Dim Conn As OleDb.OleDbConnection
        'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))

        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'JAM 2020-06-08
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn = ADO_OleDB_CreateConnection(Replace(Session("db2Path").ToString, ".db", ".mdb"))
            Conn.Open()
            myCMD.Connection = Conn
        End If


        'jrs 2023/08/25 Dim IfsConn As OleDb.OleDbConnection
        ''Dim IfsConn As SqliteConnection = New SQLiteConnection
        'jrs 2023/08/25 IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        ''IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        'jam 2025/05/31
        Dim IfsConn As SQLiteConnection
        Dim ConnectionString As String

        ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        'jam 2025/05/31 Changing connection mode

        'If IfsConn.State <> ConnectionState.Open Then
        IfsConn = OpenJournalSQLite(ConnectionString)
        ' End If




        Dim rdr As OleDb.OleDbDataReader
        'Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = Conn
        Dim strSQL As String
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim TCohort As Integer

        Dim j As Integer


        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDS = New DataSet
        'jrs 2023/08/24
        regDS.EnforceConstraints = False

        If Session("EdCohort_Grouping") = False Then

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                strSQL = "SELECT * FROM MorDetFactors Where Region = " & Session("EdCohort_tempregion").ToString

                rdr = ADO_OleDB_GetReader(myCMD, strSQL)
                Do While rdr.Read
                    TCohort = rdr.Item("NCohorts")
                    i = 1
                    'males
                    TempFactDatabase(TCohort, i) = rdr.Item(FieldName(1, 1))
                    For j = 1 To NonCommDis.NumSubTypes / 2
                        i = i + 1
                        TempFactDatabase(TCohort, i) = rdr.Item(NonCommDis.getSubTypeFieldName((2 * j) - 1))
                    Next j
                    For j = 1 To Injuries.NumSubTypes / 2
                        i = i + 1
                        TempFactDatabase(TCohort, i) = rdr.Item(Injuries.getSubTypeFieldName((2 * j) - 1))
                    Next j
                    'females
                    i = i + 1
                    TempFactDatabase(TCohort, i) = rdr.Item(FieldName(1, 2))
                    For j = 1 To NonCommDis.NumSubTypes / 2
                        i = i + 1
                        TempFactDatabase(TCohort, i) = rdr.Item(NonCommDis.getSubTypeFieldName(2 * j))
                    Next j
                    For j = 1 To Injuries.NumSubTypes / 2
                        i = i + 1
                        TempFactDatabase(TCohort, i) = rdr.Item(Injuries.getSubTypeFieldName(2 * j))
                    Next j

                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Factors Where RegionId = " & Session("EdCohort_tempregion").ToString
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    TCohort = vSQLiteReader("CohortId")
                    i = 1
                    'males
                    TempFactDatabase(TCohort, i) = vSQLiteReader(FieldName(1, 1))
                    For j = 1 To NonCommDis.NumSubTypes / 2
                        i = i + 1
                        TempFactDatabase(TCohort, i) = vSQLiteReader(NonCommDis.getSubTypeFieldName((2 * j) - 1))
                    Next j
                    For j = 1 To Injuries.NumSubTypes / 2
                        i = i + 1
                        TempFactDatabase(TCohort, i) = vSQLiteReader(Injuries.getSubTypeFieldName((2 * j) - 1))
                    Next j
                    'females
                    i = i + 1
                    TempFactDatabase(TCohort, i) = vSQLiteReader(FieldName(1, 2))
                    For j = 1 To NonCommDis.NumSubTypes / 2
                        i = i + 1
                        TempFactDatabase(TCohort, i) = vSQLiteReader(NonCommDis.getSubTypeFieldName(2 * j))
                    Next j
                    For j = 1 To Injuries.NumSubTypes / 2
                        i = i + 1
                        TempFactDatabase(TCohort, i) = vSQLiteReader(Injuries.getSubTypeFieldName(2 * j))
                    Next j

                Loop

            End If
        Else

            'IfsConn.Open()

            'jam 2025/05/31
            'IfsConn.Open()
            If IfsConn.State <> ConnectionState.Open Then
                IfsConn.Open()
            End If

            'jrs 2023/08/25 Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim rdGrpCmd As SqliteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SqliteDataReader
            rdGrp = rdGrpCmd.ExecuteReader


            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            'jrs 2023/08/25 Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim rdRegCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SqliteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            'jrs 2023/08/25 Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim rdRegIdCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SqliteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            'Dim mortFctDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetFactors", Conn)
            'regDS = New DataSet


            Dim mortFctDA As OleDb.OleDbDataAdapter

            'JAM 2020-06-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortFctDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetFactors", Conn)
                mortFctDA.Fill(regDS, "Factors")

            Else
                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Factors"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2023/08/25
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)

            End If

            'jrs 2023/08/25 grpDA.Fill(regDS, "Groups")
            'jrs 2023/08/25 regDA.Fill(regDS, "Regions")
            'jrs 2023/08/25 regIDDA.Fill(regDS, "RegionIDs")
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                     regDS.Tables("Groups").Columns("GroupNum"),
                                                     regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regFacRel As DataRelation = regDS.Relations.Add("RegFactors", _
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"), _
            '                                     regDS.Tables("Factors").Columns("Region"))


            Dim regFacRel As DataRelation

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regFacRel = regDS.Relations.Add("RegFactors",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Factors").Columns("Region"))
            Else


                regFacRel = regDS.Relations.Add("RegFactors",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mor_Det_Factors").Columns("RegionId"))


            End If


            Dim rID As Integer
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regFacRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rID = cRow.Item("Region")

                                    i = 1
                                    'males
                                    TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) + cRow.Item(FieldName(1, 1)) * POPMales(rID, TCohort)
                                    For j = 1 To NonCommDis.NumSubTypes / 2
                                        i = i + 1
                                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                        cRow.Item(NonCommDis.getSubTypeFieldName((2 * j) - 1)) * POPMales(rID, TCohort)
                                    Next j
                                    For j = 1 To Injuries.NumSubTypes / 2
                                        i = i + 1
                                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                        cRow.Item(Injuries.getSubTypeFieldName((2 * j) - 1)) * POPMales(rID, TCohort)
                                    Next j

                                    'females
                                    TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) + cRow.Item(FieldName(1, 2)) * POPFemales(rID, TCohort)
                                    For j = 1 To NonCommDis.NumSubTypes / 2
                                        i = i + 1
                                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                        cRow.Item(NonCommDis.getSubTypeFieldName(2 * j)) * POPFemales(rID, TCohort)
                                    Next j
                                    For j = 1 To Injuries.NumSubTypes / 2
                                        i = i + 1
                                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                        cRow.Item(Injuries.getSubTypeFieldName(2 * j)) * POPFemales(rID, TCohort)
                                    Next j
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regFacRel)
                                    TCohort = cRow.Item("CohortId")
                                    rID = cRow.Item("RegionId")

                                    i = 1
                                    'males
                                    TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) + cRow.Item(FieldName(1, 1)) * POPMales(rID, TCohort)
                                    For j = 1 To NonCommDis.NumSubTypes / 2
                                        i = i + 1
                                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                        cRow.Item(NonCommDis.getSubTypeFieldName((2 * j) - 1)) * POPMales(rID, TCohort)
                                    Next j
                                    For j = 1 To Injuries.NumSubTypes / 2
                                        i = i + 1
                                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                        cRow.Item(Injuries.getSubTypeFieldName((2 * j) - 1)) * POPMales(rID, TCohort)
                                    Next j

                                    'females
                                    TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) + cRow.Item(FieldName(1, 2)) * POPFemales(rID, TCohort)
                                    For j = 1 To NonCommDis.NumSubTypes / 2
                                        i = i + 1
                                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                        cRow.Item(NonCommDis.getSubTypeFieldName(2 * j)) * POPFemales(rID, TCohort)
                                    Next j
                                    For j = 1 To Injuries.NumSubTypes / 2
                                        i = i + 1
                                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                        cRow.Item(Injuries.getSubTypeFieldName(2 * j)) * POPFemales(rID, TCohort)
                                    Next j
                                Next
                            Next
                        Next
                    End If
                Next

            End If
            'Population Weigthed average
            For TCohort = 0 To NCohorts
                For j = 1 To 15 'jrs 2011/08/30 10
                    TempFactDatabase(TCohort, j) = TempFactDatabase(TCohort, j) / Session("EC_TempPopDatabase")(TCohort, 1)
                Next j
                For j = 16 To 30 'jrs 2011/08/30 For j = 11 To 20
                    TempFactDatabase(TCohort, j) = TempFactDatabase(TCohort, j) / Session("EC_TempPopDatabase")(TCohort, 2)
                Next j
            Next TCohort

            IfsConn.Close()
        End If

        Dim l As Integer
        For l = iNCohorts To 0 Step -1

            If l = NCohorts Then
                grdcohort(0, l + 1) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
            ElseIf l = 0 Then
                grdcohort(0, l + 1) = "Infants"
            ElseIf l = 1 Then
                grdcohort(0, l + 1) = "1- 4"
            Else
                grdcohort(0, l + 1) = CStr(l * 5 - 5) + "-" + CStr(l * 5 - 1)
            End If
            'cdv 2021/01/14 ****START**** Flip data and labels
            If bMaleFirst Then
                'male
                For j = 1 To 15 'jrs 2011/08/30 10
                    grdcohort(j, l + 1) = Format(TempFactDatabase(l, j), "0.0000")
                Next j

                'female
                For j = 16 To 30 'jrs 2011/08/30 For j = 11 To 20
                    grdcohort(j, l + 1) = Format(TempFactDatabase(l%, j), "0.0000")
                Next j
            Else
                'female
                For j = 1 To 11 'jrs 2011/08/30 10
                    'If j < 8 Then
                    grdcohort(j + 11, l + 1) = Format(TempFactDatabase(l, j), "0.0000")
                    'End If
                Next j

                'male
                For j = 11 To 22 'jrs 2011/08/30 For j = 11 To 20
                    grdcohort(j - 10, l + 1) = Format(TempFactDatabase(l%, j), "0.0000")
                Next j
                For j = 23 To 30
                    grdcohort(j, l + 1) = ""
                Next
            End If

        Next l%
        Dim GenderLabel1 As String
        Dim GenderLabel2 As String

        If bMaleFirst Then
            GenderLabel1 = " Male"
            GenderLabel2 = " Female"
        Else
            GenderLabel1 = " Female"
            GenderLabel2 = " Male"
        End If


        grdcohort(0, 0) = "Age Cohort"
        grdcohort(1, 0) = "Com Disease" & GenderLabel1
        Dim col As Integer
        col = 1
        For j = 1 To NonCommDis.NumSubTypes / 2
            grdcohort(j + col, 0) = NonCommDis.getSubTypeName((2 * j) - 1) & GenderLabel1
        Next j
        col = col + NonCommDis.NumSubTypes / 2
        For j = 1 To Injuries.NumSubTypes / 2
            grdcohort(j + col, 0) = Injuries.getSubTypeName((2 * j) - 1) & GenderLabel1
        Next j
        col = col + Injuries.NumSubTypes / 2

        col = col + 1
        grdcohort(col, 0) = "Com Disease" & GenderLabel2
        For j = 1 To NonCommDis.NumSubTypes / 2
            grdcohort(j + col, 0) = NonCommDis.getSubTypeName((2 * j) - 1) & GenderLabel2
        Next j
        col = col + NonCommDis.NumSubTypes / 2

        For j = 1 To Injuries.NumSubTypes / 2
            grdcohort(j + col, 0) = Injuries.getSubTypeName((2 * j) - 1) & GenderLabel2
        Next j
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn.Close()
        End If
    End Sub

    Private Sub mnuShowNormFac_Click()

        '--------------------------------------------------------------
        lbldata.Text = "Normalization Factors for " & Session("EdCohort_regionname")

        FillNormFactorGrid()

        frameData.Visible = True
        grdcohort.Visible = True
        lbldata.Visible = True
        hfTitle.Value = lbldata.Text
        'jrs 2009/03/20
        lblJS.Visible = False
        grdcohortDet.Visible = False
        lblJSLine.Visible = False
        cmdLine.Visible = False
    End Sub

    Private Sub FillNormFactorGrid()
        '--------------------------------------------------------------

        Dim TempFactDatabase(0, 0) As Single
        ReDim TempFactDatabase(NCohorts, 2)
        'Optional display of 100+ age group
        Dim iNCohorts As Integer
        If Session("bShow100") Then
            iNCohorts = NCohorts
        Else
            iNCohorts = NCohorts - 1
        End If

        Dim i As Integer
        grdcohort.TotalRows = iNCohorts + 2
        grdcohort.RowCount = iNCohorts + 2

        'jrs 2009/03/20
        grdcohort.HeaderVisible = False

        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        For i = 1 To 3
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            If i = 1 Then
                myCol.Width = 125
            Else
                myCol.Width = 150
            End If

            grdcohort.Columns.Add(myCol)
        Next

        Dim Conn As OleDb.OleDbConnection
        'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))
        'Conn.Open()

        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'JAM 2020-06-01
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn = ADO_OleDB_CreateConnection(Replace(Session("db2Path").ToString, ".db", ".mdb"))
            Conn.Open()
            myCMD.Connection = Conn
        End If


        'jrs 2023/08/25 Dim IfsConn As OleDb.OleDbConnection
        ''Dim IfsConn As SqliteConnection = New SQLiteConnection
        'jrs 2023/08/25 IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        ''IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"


        'jam 2025/05/31
        Dim IfsConn As SQLiteConnection
        Dim ConnectionString As String

        ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        'jam 2025/05/31 Changing connection mode
        IfsConn = OpenJournalSQLite(ConnectionString)


        Dim rdr As OleDb.OleDbDataReader
        'Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = Conn
        Dim strSQL As String
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim TCohort As Integer

        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        dtnew = New DataTable
        regDS = New DataSet
        'jrs 2023/08/24
        regDS.EnforceConstraints = False

        Dim j As Integer
        If Session("EdCohort_Grouping") = False Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                strSQL = "SELECT * FROM MorDetNormFactors Where Region = " & Session("EdCohort_tempregion").ToString
                rdr = ADO_OleDB_GetReader(myCMD, strSQL)
                Do While rdr.Read
                    TCohort = rdr.Item("NCohorts")
                    'males
                    TempFactDatabase(TCohort, 1) = rdr.Item("Males")

                    'females
                    TempFactDatabase(TCohort, 2) = rdr.Item("Females")
                Loop
                rdr.Close()
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Norm_Factors Where RegionID = " & Session("EdCohort_tempregion").ToString
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    TCohort = vSQLiteReader("CohortId")
                    'males
                    TempFactDatabase(TCohort, 1) = vSQLiteReader("Males")

                    'females
                    TempFactDatabase(TCohort, 2) = vSQLiteReader("Females")


                Loop
            End If

        Else

            'jam 2025/05/31
            'IfsConn.Open()
            If IfsConn.State <> ConnectionState.Open Then
                IfsConn.Open()
            End If

            'jrs 2023/08/25 Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim rdGrpCmd As SqliteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SqliteDataReader
            rdGrp = rdGrpCmd.ExecuteReader


            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            'jrs 2023/08/25 Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim rdRegCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SqliteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            'jrs 2023/08/25 Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim rdRegIdCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SqliteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            'Dim mortFctDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetNormFactors", Conn)
            'regDS = New DataSet
            'jrs 2023/08/25 grpDA.Fill(regDS, "Groups")
            'jrs 2023/08/25 regDA.Fill(regDS, "Regions")
            'jrs 2023/08/25 regIDDA.Fill(regDS, "RegionIDs")
            'mortFctDA.Fill(regDS, "Factors")

            Dim mortFctDA As OleDb.OleDbDataAdapter

            'JAM 2020-06-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortFctDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetNormFactors", Conn)
                mortFctDA.Fill(regDS, "Factors")
            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Norm_Factors"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2023/08/25
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If



            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                     regDS.Tables("Groups").Columns("GroupNum"),
                                                     regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regFacRel As DataRelation = regDS.Relations.Add("RegFactors",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Factors").Columns("Region"))


            Dim regFacRel As DataRelation

            'JAM 2020-06-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regFacRel = regDS.Relations.Add("RegFactors",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mor_Det_Norm_Factors").Columns("Region"))
            Else


                regFacRel = regDS.Relations.Add("RegFactors",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mor_Det_Norm_Factors").Columns("RegionId")) 'jrs 2023/08/25 changed to Mor_Det_Norm_Factors from Factors


            End If



            Dim rID As Integer
            'JAM 2020-06-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regFacRel)
                                    TCohort = cRow.Item("CohortId")
                                    rID = cRow.Item("RegionId")

                                    'males
                                    TempFactDatabase(TCohort, 1) = TempFactDatabase(TCohort, 1) + cRow.Item("Males") * POPMales(rID, TCohort)

                                    'females
                                    TempFactDatabase(TCohort, 2) = TempFactDatabase(TCohort, 2) + cRow.Item("Females") * POPFemales(rID, TCohort)
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regFacRel)
                                    TCohort = cRow.Item("CohortId") 'jrs 2023/08/25
                                    rID = cRow.Item("RegionId") 'jrs 2023/08/25

                                    'males
                                    TempFactDatabase(TCohort, 1) = TempFactDatabase(TCohort, 1) + cRow.Item("Males") * POPMales(rID, TCohort)

                                    'females
                                    TempFactDatabase(TCohort, 2) = TempFactDatabase(TCohort, 2) + cRow.Item("Females") * POPFemales(rID, TCohort)
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            'Population Weigthed average
            For TCohort = 0 To NCohorts
                TempFactDatabase(TCohort, 1) = TempFactDatabase(TCohort, 1) / Session("EC_TempPopDatabase")(TCohort, 1)
                TempFactDatabase(TCohort, 2) = TempFactDatabase(TCohort, 2) / Session("EC_TempPopDatabase")(TCohort, 2)
            Next TCohort

            IfsConn.Close()
        End If

        Dim l As Integer
        For l = iNCohorts To 0 Step -1

            If l = NCohorts Then
                grdcohort(0, l + 1) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
            ElseIf l = 0 Then
                grdcohort(0, l + 1) = "Infants"
            ElseIf l = 1 Then
                grdcohort(0, l + 1) = "1- 4"
            Else
                grdcohort(0, l + 1) = CStr(l * 5 - 5) + "-" + CStr(l * 5 - 1)
            End If
            'cdv 2021/01/14 ****START**** Flip label and data
            If bMaleFirst Then
                'male
                grdcohort(1, l + 1) = Format(TempFactDatabase(l, 1), "0.0000")

                'female
                grdcohort(2, l + 1) = Format(TempFactDatabase(l, 2), "0.0000")
            Else
                'female
                grdcohort(1, l + 1) = Format(TempFactDatabase(l, 2), "0.0000")

                'male
                grdcohort(2, l + 1) = Format(TempFactDatabase(l, 1), "0.0000")
            End If
        Next l%


        grdcohort(0, 0) = "Age Cohort"
        If bMaleFirst Then
            grdcohort(1, 0) = "Males"

            grdcohort(2, 0) = "Females"
        Else
            grdcohort(2, 0) = "Males"

            grdcohort(1, 0) = "Females"
        End If
        'JAM 2020-06-08
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn.Close()
        End If
    End Sub

    Private Sub mnuCommDis_Click()
        'mnuNormOff.Enabled = True
        'mnuNormOn.Enabled = True
        Session("EdCohort_GraphType") = 1
        'GraphTypeSave = GraphType
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub SeparateGraph_Draw(ByVal db2Path As String, ByVal gphIndex As Integer, ByVal tblindex As Integer, ByVal findex1 As Integer,
        Optional ByVal findex2 As Integer = 0, Optional ByVal findex3 As Integer = 0,
        Optional ByVal findex4 As Integer = 0)
        '--------------------------------------------------------------
        '--------------------------------------------------------------
        Dim tbl2 As DataTable
        Dim TCohort As Integer
        Dim Tindex As Integer
        Dim rId As Integer
        ReDim Session("EC_TempPopDatabase")(NCohorts, 2)

        ReDim Session("EC_TempDatabase")(NCohorts, 6)

        Dim i2 As Integer

        '   Specify the graph control to be used
        '   ------------------------------------

        frameData.Visible = False
        grdcohort.Visible = False
        lbldata.Visible = False
        'jrs 2009/03/20
        grdcohortDet.Visible = False
        lblJS.Visible = True

        'jrs 2006/07/14 create connecion
        Dim Conn As OleDb.OleDbConnection
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'Conn = ADO_OleDB_CreateConnection(db2Path)
        'Conn.Open()
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn = ADO_OleDB_CreateConnection(Replace(db2Path, ".db", ".mdb"))
            Conn.Open()
            myCMD.Connection = Conn
        End If

        'jrs 2023/08/25 Dim IfsConn As OleDb.OleDbConnection
        ''Dim IfsConn As SqliteConnection = New SqliteConnection
        'jrs 2023/08/25 IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        ''IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        'jam 2025/05/31
        Dim IfsConn As SQLiteConnection
        Dim ConnectionString As String

        ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        'jam 2025/05/31 Changing connection mode
        IfsConn = OpenJournalSQLite(ConnectionString)


        Dim rdr As OleDb.OleDbDataReader

        'myCMD.Connection = Conn
        Dim strSQL As String
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow

        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable


        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        dtnew = New DataTable
        regDS = New DataSet
        'jrs 2023/08/24
        regDS.EnforceConstraints = False


        'take population
        '   Change the data source
        '   ----------------------
        ' GROUP

        Tindex = 2
        If Session("EdCohort_Grouping") = False Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(2) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
                i2 = 0
                Do While rdr.Read
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) = rdr.Item(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) = rdr.Item(FieldName(Tindex, 2))
                Loop
                rdr.Close()
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                'jam 2022/07/25 add TimeID condition, bug solution identifying by jrs
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(2) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))



                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))
                Loop

                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()

            End If
        Else
            ReDim POPMales(NR, NCohorts)
            ReDim POPFemales(NR, NCohorts)


            'jam 2025/05/31
            'IfsConn.Open()
            If IfsConn.State <> ConnectionState.Open Then
                IfsConn.Open()
            End If

            'jrs 2023/08/25 Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim rdGrpCmd As SqliteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SqliteDataReader
            rdGrp = rdGrpCmd.ExecuteReader


            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            'jrs 2023/08/25 Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim rdRegCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SqliteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            'jrs 2023/08/25 Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim rdRegIdCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SqliteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            'regDS = New DataSet
            'jrs 2023/08/25 grpDA.Fill(regDS, "Groups")
            'jrs 2023/08/25 regDA.Fill(regDS, "Regions")
            'jrs 2023/08/25 regIDDA.Fill(regDS, "RegionIDs")
            'popDA.Fill(regDS, "Population")


            Dim popDA As OleDb.OleDbDataAdapter
            'regDS = New DataSet
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'JAM 2020-06-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                popDA.Fill(regDS, "Population")
            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)

                'jrs 2023/08/25
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)

            End If





            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                     regDS.Tables("Groups").Columns("GroupNum"),
                                                     regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegionId",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

            Dim regPopRel As DataRelation

            'JAM 2020-06-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables(TableName(Tindex)).Columns("RegionId"))


            End If

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) + cRow.Item(FieldName(Tindex, 1))
                                    Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) + cRow.Item(FieldName(Tindex, 2))

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts")) = cRow.Item(FieldName(Tindex, 1))
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts")) = cRow.Item(FieldName(Tindex, 2))
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) + cRow.Item(FieldName(Tindex, 1))
                                    Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) + cRow.Item(FieldName(Tindex, 2))

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId")) = cRow.Item(FieldName(Tindex, 1))
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId")) = cRow.Item(FieldName(Tindex, 2))
                                Next
                            Next
                        Next
                    End If
                Next


            End If

            Session("POPMales") = POPMales
            Session("POPFemales") = POPFemales

        End If

        'End take population

        '   Change the data source
        '   ----------------------
        'GROUP
        If Session("EdCohort_Grouping") = False Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(1) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
                i2 = 0
                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    'males - tertiary to No completed formal education
                    TCohort = rdr.Item("NCohorts") '5=Injury Male,3=Non Comm Dis Male, 1 = Comm Dis Male
                    Session("EC_TempDatabase")(TCohort, 3) = rdr.Item(FieldName(1, 5))
                    Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, rdr.Item(FieldName(1, 3)))
                    Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, rdr.Item(FieldName(1, 1)))

                    'females 6=Injury Female, 4=Non Comm Dis female, 2 = Comm Dis Female
                    Session("EC_TempDatabase")(TCohort, 6) = rdr.Item(FieldName(1, 6))
                    Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, rdr.Item(FieldName(1, 4)))
                    Session("EC_TempDatabase")(TCohort, 4) = AMAX(0, rdr.Item(FieldName(1, 2)))
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(1) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    'males -Tertiary to No completed formal education
                    TCohort = vSQLiteReader("CohortId") '5=Injury Male,3=Non Comm Dis Male, 1 = Comm Dis Male
                    Session("EC_TempDatabase")(TCohort, 3) = vSQLiteReader(FieldName(1, 5))
                    Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, vSQLiteReader(FieldName(1, 3)))
                    Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, vSQLiteReader(FieldName(1, 1)))

                    'females 6=Injury Female, 4=Non Comm Dis female, 2 = Comm Dis Female
                    Session("EC_TempDatabase")(TCohort, 6) = vSQLiteReader(FieldName(1, 6))
                    Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, vSQLiteReader(FieldName(1, 4)))
                    Session("EC_TempDatabase")(TCohort, 4) = AMAX(0, vSQLiteReader(FieldName(1, 2)))
                Loop

                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()

            End If
        Else
            '    Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(1) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            'mortDA.Fill(regDS, "Mortality")



            Dim mortDA As OleDb.OleDbDataAdapter

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(1) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                mortDA.Fill(regDS, "Mortality")
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT TimeId, CohortId, RegionId, MortPerMalesComDis, MortPerFeMalesComDis, MortPerMalesNonComDis, MortPerFeMalesNonComDis, MortPerMalesInjuries, MortPerFeMalesInjuries FROM " & TableName(1) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2023/08/25
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)
            End If




            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Mortality").Columns("Region"))


            Dim regMortRel As DataRelation

            'JAM 2020-6-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables(TableName(1)).Columns("RegionId"))


            End If


            'JAM 2020-6-08
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")
                                    Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) +
                                    cRow.Item(FieldName(1, 5)) * POPMales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) +
                                    cRow.Item(FieldName(1, 3)) * POPMales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 1) = Session("EC_TempDatabase")(TCohort, 1) +
                                    cRow.Item(FieldName(1, 1)) * POPMales(rId, TCohort)

                                    Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) +
                                    cRow.Item(FieldName(1, 6)) * POPFemales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 5) = Session("EC_TempDatabase")(TCohort, 5) +
                                    cRow.Item(FieldName(1, 4)) * POPFemales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) +
                                    cRow.Item(FieldName(1, 2)) * POPFemales(rId, TCohort)
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")
                                    Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) +
                                    cRow.Item(FieldName(1, 5)) * POPMales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) +
                                    cRow.Item(FieldName(1, 3)) * POPMales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 1) = Session("EC_TempDatabase")(TCohort, 1) +
                                    cRow.Item(FieldName(1, 1)) * POPMales(rId, TCohort)

                                    Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) +
                                    cRow.Item(FieldName(1, 6)) * POPFemales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 5) = Session("EC_TempDatabase")(TCohort, 5) +
                                    cRow.Item(FieldName(1, 4)) * POPFemales(rId, TCohort)
                                    Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) +
                                    cRow.Item(FieldName(1, 2)) * POPFemales(rId, TCohort)
                                Next
                            Next
                        Next
                    End If
                Next

            End If
            IfsConn.Close()

            'Population Weigthed average
            For TCohort = 0 To NCohorts
                'males
                Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) / Session("EC_TempPopDatabase")(TCohort, 1)
                Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) / Session("EC_TempPopDatabase")(TCohort, 1)
                Session("EC_TempDatabase")(TCohort, 1) = Session("EC_TempDatabase")(TCohort, 1) / Session("EC_TempPopDatabase")(TCohort, 1)
                'females
                Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) / Session("EC_TempPopDatabase")(TCohort, 2)
                Session("EC_TempDatabase")(TCohort, 5) = Session("EC_TempDatabase")(TCohort, 5) / Session("EC_TempPopDatabase")(TCohort, 2)
                Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) / Session("EC_TempPopDatabase")(TCohort, 2)
            Next TCohort

        End If

        'jam 2022/07/17
        If Session("cnvtoDeaths") Then
            ConvertToDeaths(NCohorts, 3)
        End If


        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn.Close()
        End If

        Dim GraphTitle As String = ""
        If gphIndex = 3 Then
            GraphTitle$ = "People dying of Injury"
        ElseIf gphIndex = 2 Then
            GraphTitle$ = "People dying of Non Communicable Disease"
        ElseIf gphIndex = 1 Then
            GraphTitle$ = "People dying of Communicable Disease"

        End If

        lbldata.Text = GraphTitle & " by gender <br/>" & Session("EdCohort_regionname") &
            " in year " & Session("EdCohort_tempyear").ToString & ", (deaths per 1000 people) [" & Session("EdCG_Casename") & "]"

    End Sub

    'jrs 2016/05/20
    Private Sub SeparateGraph_Draw_compare(ByVal gphIndex As Integer, ByVal tblindex As Integer, ByVal findex1 As Integer,
        Optional ByVal findex2 As Integer = 0, Optional ByVal findex3 As Integer = 0,
        Optional ByVal findex4 As Integer = 0)
        Select Case gphIndex
            Case 1
                Session("EdCohort_tempregion") = cmbReg1.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg1.SelectedValue
                Session("EdCohort_tempyear") = cmbYr1.SelectedValue
            Case 2
                Session("EdCohort_tempregion") = cmbReg2.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg2.SelectedValue
                Session("EdCohort_tempyear") = cmbYr2.SelectedValue
            Case 3
                Session("EdCohort_tempregion") = cmbReg3.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg3.SelectedValue
                Session("EdCohort_tempyear") = cmbYr3.SelectedValue
            Case 4
                Session("EdCohort_tempregion") = cmbReg4.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg4.SelectedValue
                Session("EdCohort_tempyear") = cmbYr4.SelectedValue
        End Select

        'jrs 2024/04/22
        vSQLiteCon.Close()
        vConnString = "DataSource=" + Session("dbsces")(gphIndex - 1) & ";"

        'jam 2025/05/31 Changing connection mode
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()


        'jam 2025/05/31 Changing connection mode
        vSQLiteCon = OpenJournalSQLite(vConnString)



        SeparateGraph_Draw(Session("dbsces")(gphIndex - 1), gphIndex, tblindex, findex1, findex2, findex3, findex4)
        'Load result to Compare array
        Dim i As Integer, j As Integer
        For i = 0 To NCohorts
            For j = 1 To 2
                Session("EC_TempPopDatabase_Comp")(i, j, gphIndex) = Session("EC_TempPopDatabase")(i, j)
            Next
            For j = 1 To 6
                Session("EC_TempDatabase_Comp")(i, j, gphIndex) = Session("EC_TempDatabase")(i, j)
            Next
        Next


        'jam 2022/07/17
        If Session("cnvtoDeaths") Then
            ConvertToDeaths(NCohorts, 3)
        End If

    End Sub

    Private Sub SeparateFillDataGrid(ByVal gphIndex As Integer)
        '--------------------------------------------------------------
        Dim iNCohorts As Integer
        If Session("bShow100") Then
            iNCohorts = NCohorts
        Else
            iNCohorts = NCohorts - 1
        End If

        Dim i As Integer
        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        grdcohort.TotalRows = iNCohorts + 1
        grdcohort.RowCount = iNCohorts + 1

        myCol = New TMSWebControls.Column
        myCol.WidthType = TMSWebControls.WType.Absolute
        myCol.Width = 100 '1100
        grdcohort.Columns.Add(myCol)

        For i = 1 To 3
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            myCol.Width = 150
            'jrs 2009/03/17
            myCol.Type = TMSWebControls.Column.ColumnType.LinkField
            myCol.HeaderVAlignment = TMSWebControls.Column.VAligning.Top
            myCol.HeaderAlignment = TMSWebControls.Column.Aligning.Center

            grdcohort.Columns.Add(myCol)
        Next

        Dim l As Integer
        For l = iNCohorts To 0 Step -1

            If l = NCohorts Then
                grdcohort(0, l) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
            ElseIf l = 0 Then
                grdcohort(0, l) = "Infants"
            ElseIf l = 1 Then
                grdcohort(0, l) = "1- 4"
            Else
                grdcohort(0, l) = CStr(l * 5 - 5) & "-" & CStr(l * 5 - 1)
            End If
            'cdv 2021/01/14 ****START**** flip gender order of data and labels
            If bMaleFirst Then
                'male
                grdcohort(1, l) = Format(Session("EC_TempDatabase")(l, gphIndex), "0.0000")

                'female
                grdcohort(2, l) = Format(Session("EC_TempDatabase")(l, gphIndex + 3), "0.0000")
            Else
                'female
                grdcohort(1, l) = Format(Session("EC_TempDatabase")(l, gphIndex + 3), "0.0000")

                'male
                grdcohort(2, l) = Format(Session("EC_TempDatabase")(l, gphIndex), "0.0000")
            End If
            'total
            grdcohort(3, l) = Format((Session("EC_TempDatabase")(l, gphIndex) * Session("EC_TempPopDatabase")(l, 1) +
                Session("EC_TempDatabase")(l, gphIndex + 3) * Session("EC_TempPopDatabase")(l, 2)) /
                (Session("EC_TempPopDatabase")(l, 1) + Session("EC_TempPopDatabase")(l, 2)), "0.0000")
        Next l

        grdcohort.Columns(0).title = "Age Cohort"
        If bMaleFirst Then
            grdcohort.Columns(1).title = "Male"
            grdcohort.Columns(2).title = "Female"
        Else
            grdcohort.Columns(2).title = "Male"
            grdcohort.Columns(1).title = "Female"
        End If
        'cdv 2021/01/14 ****END****
        grdcohort.Columns(3).title = "Total"

        Dim GraphTitle As String = ""
        If gphIndex = 3 Then
            GraphTitle$ = "People dying of Injury"
        ElseIf gphIndex = 2 Then
            GraphTitle$ = "People dying of Non Communicable Disease"
        ElseIf gphIndex = 1 Then
            GraphTitle$ = "People dying of Communicable Disease"
        End If

        lbldata.Text = GraphTitle & " by gender <br/>" & Session("EdCohort_regionname") &
            " in year " & Session("EdCohort_tempyear").ToString & ", (deaths per 1000 people) [" & Session("EdCG_Casename") & "]"

    End Sub

    Private Sub mnuAllTypes_Click()
        'mnuNormOff.Enabled = True
        'mnuNormOn.Enabled = True
        Session("EdCohort_GraphType") = 0
        'GraphTypeSave = GraphType
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub SeparateFillFactorGrid(ByVal gphIndex As Integer, ByRef gphType As MortType)
        '--------------------------------------------------------------

        Dim TempFactDatabase(0, 0) As Single
        ReDim TempFactDatabase(NCohorts, 20)
        'Optional display of 100+ age group
        Dim iNCohorts As Integer
        If Session("bShow100") Then
            iNCohorts = NCohorts
        Else
            iNCohorts = NCohorts - 1
        End If

        Dim i As Integer
        grdcohort.TotalRows = iNCohorts + 2
        grdcohort.RowCount = iNCohorts + 2
        'jrs 2009/03/20
        grdcohort.HeaderVisible = False

        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column
        Dim TotCols As Integer
        If gphIndex = 1 Or gphIndex = 4 Then
            TotCols = 3
        Else
            TotCols = 1 + gphType.NumSubTypes
        End If

        For i = 1 To TotCols
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            If i = 1 Then
                myCol.Width = 125
            Else
                myCol.Width = 150
            End If

            grdcohort.Columns.Add(myCol)
        Next

        'Dim Conn As OleDb.OleDbConnection
        'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))
        'Conn.Open()

        Dim Conn As OleDb.OleDbConnection

        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))
        'JAM 2020-06-09
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn = ADO_OleDB_CreateConnection(Replace(Session("db2Path").ToString, ".db", ".mdb"))
            Conn.Open()
            myCMD.Connection = Conn
        End If


        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim rdr As OleDb.OleDbDataReader

        'myCMD.Connection = Conn
        Dim strSQL As String
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim TCohort As Integer

        'JAM 2020-06-09
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable


        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        dtnew = New DataTable
        regDS = New DataSet



        Dim j As Integer
        If Session("EdCohort_Grouping") = False Then

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                strSQL = "SELECT * FROM MorDetFactors Where Region = " & Session("EdCohort_tempregion").ToString

                rdr = ADO_OleDB_GetReader(myCMD, strSQL)
                Do While rdr.Read
                    TCohort = rdr.Item("NCohorts")
                    If gphIndex = 1 Or gphIndex = 4 Then
                        TempFactDatabase(TCohort, 1) = rdr.Item(FieldName(1, 1))
                        TempFactDatabase(TCohort, 2) = rdr.Item(FieldName(1, 2))
                    Else
                        'males
                        For i = 1 To gphType.NumSubTypes / 2
                            TempFactDatabase(TCohort, i) = rdr.Item(gphType.getSubTypeFieldName((2 * i) - 1))
                        Next
                        'females
                        For i = 1 To gphType.NumSubTypes / 2
                            TempFactDatabase(TCohort, i + (gphType.NumSubTypes / 2)) = rdr.Item(gphType.getSubTypeFieldName(2 * i))
                        Next
                    End If
                Loop
                rdr.Close()
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Factors Where RegionId = " & Session("EdCohort_tempregion").ToString
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    TCohort = vSQLiteReader("CohortId")
                    If gphIndex = 1 Or gphIndex = 4 Then
                        TempFactDatabase(TCohort, 1) = vSQLiteReader(FieldName(1, 1))
                        TempFactDatabase(TCohort, 2) = vSQLiteReader(FieldName(1, 2))
                    Else
                        'males
                        For i = 1 To gphType.NumSubTypes / 2
                            TempFactDatabase(TCohort, i) = vSQLiteReader(gphType.getSubTypeFieldName((2 * i) - 1))
                        Next
                        'females
                        For i = 1 To gphType.NumSubTypes / 2
                            TempFactDatabase(TCohort, i + (gphType.NumSubTypes / 2)) = vSQLiteReader(gphType.getSubTypeFieldName(2 * i))
                        Next
                    End If

                Loop
            End If
        Else

            'IfsConn.Open()

            'jam 2025/05/31
            'IfsConn.Open()
            If IfsConn.State <> ConnectionState.Open Then
                IfsConn.Open()
            End If

            Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            'Dim mortFctDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetFactors", Conn)
            regDS = New DataSet
            grpDA.Fill(regDS, "Groups")
            regDA.Fill(regDS, "Regions")
            regIDDA.Fill(regDS, "RegionIDs")
            'mortFctDA.Fill(regDS, "Factors")

            Dim mortFctDA As OleDb.OleDbDataAdapter

            'JAM 2020-06-09
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortFctDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetFactors", Conn)
                mortFctDA.Fill(regDS, "Factors")
            Else
                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Factors"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                dtnew = DTsqlite2.Clone
                Dim d2 As DataColumn = dtnew.Columns("RegionId")
                d2.DataType = System.Type.GetType("System.Int32")

                For Each row As DataRow In DTsqlite2.Rows
                    dtnew.ImportRow(row)
                Next
                regDS.Tables.Add(dtnew)

            End If



            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                     regDS.Tables("Groups").Columns("GroupNum"),
                                                     regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regFacRel As DataRelation = regDS.Relations.Add("RegFactors",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Factors").Columns("Region"))


            Dim regFacRel As DataRelation

            'JAM 2020-06-09
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regFacRel = regDS.Relations.Add("RegFactors",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Factors").Columns("Region"))

            Else


                regFacRel = regDS.Relations.Add("RegFactors",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mor_Det_Factors").Columns("RegionId"))


            End If



            Dim rID As Integer

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regFacRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rID = cRow.Item("Region")

                                    If gphIndex = 1 Or gphIndex = 4 Then
                                        TempFactDatabase(TCohort, 1) = TempFactDatabase(TCohort, 1) + cRow.Item(FieldName(1, 1)) * POPMales(rID, TCohort)
                                        TempFactDatabase(TCohort, 2) = TempFactDatabase(TCohort, 2) + cRow.Item(FieldName(1, 2)) * POPFemales(rID, TCohort)
                                    Else
                                        'males
                                        For i = 1 To gphType.NumSubTypes / 2
                                            TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                            cRow.Item(gphType.getSubTypeFieldName((2 * i) - 1)) * POPMales(rID, TCohort)
                                        Next i

                                        'females
                                        For i = 1 To gphType.NumSubTypes / 2
                                            TempFactDatabase(TCohort, i + (gphType.NumSubTypes / 2)) = TempFactDatabase(TCohort, i) +
                                            cRow.Item(gphType.getSubTypeFieldName(2 * i)) * POPFemales(rID, TCohort)
                                        Next i
                                    End If
                                Next 'Factor
                            Next 'RegionId
                        Next 'Group Member
                    End If
                Next 'Group

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regFacRel)
                                    TCohort = cRow.Item("CohortId")
                                    rID = cRow.Item("RegionId")

                                    If gphIndex = 1 Or gphIndex = 4 Then
                                        TempFactDatabase(TCohort, 1) = TempFactDatabase(TCohort, 1) + cRow.Item(FieldName(1, 1)) * POPMales(rID, TCohort)
                                        TempFactDatabase(TCohort, 2) = TempFactDatabase(TCohort, 2) + cRow.Item(FieldName(1, 2)) * POPFemales(rID, TCohort)
                                    Else
                                        'males
                                        For i = 1 To gphType.NumSubTypes / 2
                                            TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) +
                                            cRow.Item(gphType.getSubTypeFieldName((2 * i) - 1)) * POPMales(rID, TCohort)
                                        Next i

                                        'females
                                        For i = 1 To gphType.NumSubTypes / 2
                                            TempFactDatabase(TCohort, i + (gphType.NumSubTypes / 2)) = TempFactDatabase(TCohort, i) +
                                            cRow.Item(gphType.getSubTypeFieldName(2 * i)) * POPFemales(rID, TCohort)
                                        Next i
                                    End If
                                Next 'Factor
                            Next 'RegionId
                        Next 'Group Member
                    End If
                Next 'Group

            End If


            'Population Weigthed average
            For TCohort = 0 To NCohorts
                If gphIndex = 1 Or gphIndex = 4 Then
                    TempFactDatabase(TCohort, 1) = TempFactDatabase(TCohort, 1) / Session("EC_TempPopDatabase")(TCohort, 1)
                    TempFactDatabase(TCohort, 2) = TempFactDatabase(TCohort, 2) / Session("EC_TempPopDatabase")(TCohort, 2)
                Else
                    'males
                    For i = 1 To gphType.NumSubTypes / 2
                        TempFactDatabase(TCohort, i) = TempFactDatabase(TCohort, i) / Session("EC_TempPopDatabase")(TCohort, 1)
                    Next i
                    'females
                    For i = 1 To gphType.NumSubTypes / 2
                        TempFactDatabase(TCohort, i + (gphType.NumSubTypes / 2)) = TempFactDatabase(TCohort, i) / Session("EC_TempPopDatabase")(TCohort, 2)
                    Next i
                End If
            Next TCohort

            IfsConn.Close()
        End If

        Dim l As Integer
        For l = iNCohorts To 0 Step -1

            If l = NCohorts Then
                grdcohort(0, l + 1) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
            ElseIf l = 0 Then
                grdcohort(0, l + 1) = "Infants"
            ElseIf l = 1 Then
                grdcohort(0, l + 1) = "1- 4"
            Else
                grdcohort(0, l + 1) = CStr(l * 5 - 5) + "-" + CStr(l * 5 - 1)
            End If
            'cdv 2021/01/14 ****START**** Changes to flip gender
            If gphIndex = 1 Or gphIndex = 4 Then
                If bMaleFirst Then
                    grdcohort(1, l + 1) = Format(TempFactDatabase(l, 1), "0.0000")
                    grdcohort(2, l + 1) = Format(TempFactDatabase(l, 2), "0.0000")
                Else
                    grdcohort(1, l + 1) = Format(TempFactDatabase(l, 2), "0.0000")
                    grdcohort(2, l + 1) = Format(TempFactDatabase(l, 1), "0.0000")
                End If
            Else
                Dim IndexModifier1 As Integer
                Dim IndexModifier2 As Integer

                If bMaleFirst Then
                    IndexModifier1 = 0
                    IndexModifier2 = gphType.NumSubTypes / 2
                Else
                    IndexModifier1 = gphType.NumSubTypes / 2
                    IndexModifier2 = 0
                End If

                'male
                For j = 1 To gphType.NumSubTypes / 2
                    grdcohort(j, l + 1) = Format(TempFactDatabase(l, j + IndexModifier1), "0.0000")
                Next j
                'female
                For j = 1 To gphType.NumSubTypes / 2
                    grdcohort(j + gphType.NumSubTypes / 2, l + 1) = Format(TempFactDatabase(l%, j + IndexModifier2), "0.0000")
                Next j

            End If
        Next l%

        grdcohort(0, 0) = "Age Cohort"

        If gphIndex = 1 Or gphIndex = 4 Then
            If bMaleFirst Then
                grdcohort(1, 0) = "Males"
                grdcohort(2, 0) = "Females"
            Else
                grdcohort(2, 0) = "Males"
                grdcohort(1, 0) = "Females"
            End If
        Else
            Dim col As Integer
            col = 0

            Dim GenderLabel1 As String
            Dim GenderLabel2 As String

            If bMaleFirst Then
                GenderLabel1 = " Male"
                GenderLabel2 = " Female"
            Else
                GenderLabel2 = " Male"
                GenderLabel1 = " Female"
            End If


            'male
            For i = 1 To gphType.NumSubTypes / 2
                grdcohort(i + col, 0) = gphType.getSubTypeName((2 * i) - 1) & GenderLabel1
            Next i
            col = col + gphType.NumSubTypes / 2
            'female
            For i = 1 To gphType.NumSubTypes / 2
                grdcohort(i + col, 0) = gphType.getSubTypeName((2 * i) - 1) & GenderLabel2
            Next i
        End If
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn.Close()
        End If

    End Sub

    Private Sub mnuNonCommDis_Click()
        'mnuNormOff.Enabled = True
        'mnuNormOn.Enabled = True
        Session("EdCohort_GraphType") = 2
        'GraphTypeSave = GraphType
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub mnuInjuries_Click()
        'mnuNormOff.Enabled = True
        'mnuNormOn.Enabled = True
        Session("EdCohort_GraphType") = 3
        'GraphTypeSave = GraphType
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub mnuCommSub_Click()
        'mnuNormOff.Enabled = False
        'mnuNormOn.Enabled = False
        'mnuNormOff.Checked = True
        'mnuNormOn.Checked = False
        Session("EdCohort_GraphType") = 4
        'GraphTypeSave = 0
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub CompositeGraph_Draw_All(ByVal db2Path As String, ByRef gphType As MortType, ByVal gphIndex As Integer)
        '--------------------------------------------------------------
        'procedure for pyramid with all sub types of mortality for a given type; graphtype=5, 6, 8
        'Comm Dis, Non Comm Dis, Injuries
        Dim tbl2 As DataTable
        Dim TCohort As Integer
        Dim GraphTitle As String
        Dim Tindex As Integer
        Dim rId As Integer
        Dim tblindex As Integer = 1

        ReDim Session("EC_TempPopDatabase")(NCohorts, 2)

        ReDim Session("EC_TempDatabase")(NCohorts, gphType.NumSubTypes)

        '   Specify the graph control to be used
        '   ------------------------------------
        Dim i2 As Integer
        frameData.Visible = False
        grdcohort.Visible = False
        lbldata.Visible = False
        'jrs 2009/03/20
        grdcohortDet.Visible = False
        lblJS.Visible = True

        'create connecion
        'Dim Conn As OleDb.OleDbConnection
        'Conn = ADO_OleDB_CreateConnection(db2Path)
        ' Conn.Open()
        'JAM 2020-06-09
        Dim Conn As OleDb.OleDbConnection
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn = ADO_OleDB_CreateConnection(Replace(db2Path, ".db", ".mdb"))
            Conn.Open()
            myCMD.Connection = Conn
        End If


        'jrs 2023/08/25 Dim IfsConn As OleDb.OleDbConnection
        ''Dim IfsConn As SqliteConnection = New SQLiteConnection
        'jrs 2023/08/25 IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        ''IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"


        'jam 2025/05/31
        Dim IfsConn As SQLiteConnection
        Dim ConnectionString As String

        ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"

        'jam 2025/05/31 Changing connection mode
        IfsConn = OpenJournalSQLite(ConnectionString)


        Dim rdr As OleDb.OleDbDataReader
        'Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = Conn
        Dim strSQL As String
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow


        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable


        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        dtnew = New DataTable
        regDS = New DataSet
        'jrs 2023/08/24
        regDS.EnforceConstraints = False

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2

        If Session("EdCohort_Grouping") = False Then
            'JAM 2020-06-09
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(2) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
                i2 = 0
                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) = rdr.Item(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) = rdr.Item(FieldName(Tindex, 2))
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(2) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))
                Loop
            End If
        Else
            ReDim POPMales(NR, NCohorts)
            ReDim POPFemales(NR, NCohorts)

            'IfsConn.Open()

            'jam 2025/05/31
            'IfsConn.Open()
            If IfsConn.State <> ConnectionState.Open Then
                IfsConn.Open()
            End If

            'jrs 2023/08/25 Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim rdGrpCmd As SqliteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SqliteDataReader
            rdGrp = rdGrpCmd.ExecuteReader


            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            'jrs 2023/08/25 Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim rdRegCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SqliteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            'jrs 2023/08/25 Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim rdRegIdCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SqliteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            'jrs 2023/08/25 regDS = New DataSet
            'jrs 2023/08/25 grpDA.Fill(regDS, "Groups")
            'jrs 2023/08/25 regDA.Fill(regDS, "Regions")
            'jrs 2023/08/25 regIDDA.Fill(regDS, "RegionIDs")
            'popDA.Fill(regDS, "Population")

            Dim popDA As OleDb.OleDbDataAdapter


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                popDA.Fill(regDS, "Population")

            Else


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)

                'jrs 2023/08/25
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)

            End If


            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                     regDS.Tables("Groups").Columns("GroupNum"),
                                                     regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

            Dim regPopRel As DataRelation

            'JAM 2020-06-09
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables(TableName(Tindex)).Columns("RegionId"))


            End If


            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) + cRow.Item(FieldName(Tindex, 1))
                                    Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) + cRow.Item(FieldName(Tindex, 2))

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts")) = cRow.Item(FieldName(Tindex, 1))
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts")) = cRow.Item(FieldName(Tindex, 2))
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) + cRow.Item(FieldName(Tindex, 1))
                                    Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) + cRow.Item(FieldName(Tindex, 2))

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId")) = cRow.Item(FieldName(Tindex, 1))
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId")) = cRow.Item(FieldName(Tindex, 2))
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            Session("POPMales") = POPMales
            Session("POPFemales") = POPFemales
        End If
        'End take population

        Dim j As Integer

        '   Change the data source
        '   ----------------------
        If Session("EdCohort_Grouping") = False Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
                i2 = 0
                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    TCohort = rdr.Item("NCohorts") '5=Injury Male,3=Non Comm Dis Male, 1 = Comm Dis Male

                    'males, Odds
                    For j = 1 To gphType.NumSubTypes / 2
                        Session("EC_TempDatabase")(TCohort, j) = rdr.Item(gphType.getSubTypeFieldName((2 * j) - 1))
                    Next j

                    'females, Evens
                    For j = 1 To gphType.NumSubTypes / 2
                        Session("EC_TempDatabase")(TCohort, j + gphType.NumSubTypes / 2) = rdr.Item(gphType.getSubTypeFieldName(2 * j))
                    Next j
                Loop
                rdr.Close()

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    TCohort = vSQLiteReader("CohortId") '5=Injury Male,3=Non Comm Dis Male, 1 = Comm Dis Male

                    'males, Odds
                    For j = 1 To gphType.NumSubTypes / 2
                        Session("EC_TempDatabase")(TCohort, j) = vSQLiteReader(gphType.getSubTypeFieldName((2 * j) - 1))
                    Next j

                    'females, Evens
                    For j = 1 To gphType.NumSubTypes / 2
                        Session("EC_TempDatabase")(TCohort, j + gphType.NumSubTypes / 2) = vSQLiteReader(gphType.getSubTypeFieldName(2 * j))
                    Next j
                Loop
            End If
        Else
            '    Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(tblindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            'mortDA.Fill(regDS, "Mortality")

            Dim mortDA As OleDb.OleDbDataAdapter

            'JAM 2020-06-09
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(tblindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                mortDA.Fill(regDS, "Mortality")
            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()

                'vSQLiteCommand.CommandText = "SELECT TimeId, CohortId, RegionId, MortPerMalesComDis, MortPerFeMalesComDis, MortPerMalesNonComDis, MortPerFeMalesNonComDis, MortPerMalesInjuries, MortPerFeMalesInjuries,ComDisNoAIDSMales,AIDSMales,ComDisNoAIDSFemales,AIDSFemales FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                'vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))



                'vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))

                'JAM 2020-06-21
                If gphIndex = 3 Then
                    '"People dying of Injury"

                    vSQLiteCommand.CommandText = "SELECT TimeId,RegionId,CohortId,MortPerMalesInjuries,MortPerFemalesInjuries,RoadTrafficAccMales,RoadTrafficAccFemales,OtherunintInjMales,OtherunintInjFemales,IntenInjMales,IntenInjFemales FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))

                ElseIf gphIndex = 2 Then
                    '"People dying of Non Communicable Disease"

                    vSQLiteCommand.CommandText = "SELECT TimeId,RegionId,CohortId,MortPerMalesNonComDis,MortPerFemalesNonComDis,MaligNeoPlasMales,MaligNeoPlasFemales,CardioVascMales,CardioVascFemales,DigestiveMales,DigestiveFemales,RespiratoryMales,RespiratoryFemales,OtherG2Males,OtherG2Females,DiabetesMales,DiabetesFemales,MentalHealthMales,MentalHealthFemales FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))


                ElseIf gphIndex = 1 Then
                    '"People dying of Communicable Disease"

                    vSQLiteCommand.CommandText = "SELECT TimeId,RegionId,CohortId,MortPerMalesComDis,MortPerFemalesComDis,ComDisNoAIDSMales,ComDisNoAIDSFemales,AIDSMales,AIDSFemales,DiarrheaMales,DiarrheaFemales,MalariaMales,MalariaFemales,RespInfMales,RespInfFemales FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))

                Else
                    vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))


                End If

                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2023/08/25
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)

            End If




            'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality", _
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"), _
            '                                     regDS.Tables("Mortality").Columns("Region"))


            Dim regMortRel As DataRelation

            'JAM 2020-06-09
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mortality").Columns("Region"))
            Else


                regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables(TableName(tblindex)).Columns("RegionId"))

            End If



            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("NCohorts")
                                    rId = cRow.Item("Region")

                                    'males, Odds
                                    For j = 1 To gphType.NumSubTypes / 2
                                        Session("EC_TempDatabase")(TCohort, j) = Session("EC_TempDatabase")(TCohort, j) +
                                        cRow.Item(gphType.getSubTypeFieldName((2 * j) - 1)) * POPMales(rId, TCohort)
                                    Next j

                                    'females, Evens
                                    For j = 1 To gphType.NumSubTypes / 2
                                        Session("EC_TempDatabase")(TCohort, j + (gphType.NumSubTypes / 2)) =
                                        Session("EC_TempDatabase")(TCohort, j + (gphType.NumSubTypes / 2)) +
                                        cRow.Item(gphType.getSubTypeFieldName(2 * j)) * POPFemales(rId, TCohort)
                                    Next j

                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel)
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regMortRel)
                                    TCohort = cRow.Item("CohortId")
                                    rId = cRow.Item("RegionId")

                                    'males, Odds
                                    For j = 1 To gphType.NumSubTypes / 2
                                        Session("EC_TempDatabase")(TCohort, j) = Session("EC_TempDatabase")(TCohort, j) +
                                        cRow.Item(gphType.getSubTypeFieldName((2 * j) - 1)) * POPMales(rId, TCohort)
                                    Next j

                                    'females, Evens
                                    For j = 1 To gphType.NumSubTypes / 2
                                        Session("EC_TempDatabase")(TCohort, j + (gphType.NumSubTypes / 2)) =
                                        Session("EC_TempDatabase")(TCohort, j + (gphType.NumSubTypes / 2)) +
                                        cRow.Item(gphType.getSubTypeFieldName(2 * j)) * POPFemales(rId, TCohort)
                                    Next j

                                Next
                            Next
                        Next
                    End If
                Next

            End If
            IfsConn.Close()

            'Population Weigthed average
            For TCohort = 0 To NCohorts
                'males, Odds
                For j = 1 To gphType.NumSubTypes / 2
                    Session("EC_TempDatabase")(TCohort, j) = Session("EC_TempDatabase")(TCohort, j) / Session("EC_TempPopDatabase")(TCohort, 1)
                Next j

                'females, Evens
                For j = 1 To gphType.NumSubTypes / 2
                    Session("EC_TempDatabase")(TCohort, j + (gphType.NumSubTypes / 2)) =
                        Session("EC_TempDatabase")(TCohort, j + (gphType.NumSubTypes / 2)) / Session("EC_TempPopDatabase")(TCohort, 2)
                Next j

            Next TCohort

        End If



        'jam 2022/07/17
        If Session("cnvtoDeaths") Then
            ConvertToDeaths(NCohorts, gphType.NumSubTypes / 2)
        End If





        'FillDataGrid Tindex
        'CompositeFillDataGrid() '1, 2, 3
        'end mti june, 2002


        '   Draw the graph
        '   --------------
        '    gphAgedst(Index).AutoInc = 1
        '    gphAgedst(Index).DataReset = 1
        'JAM 2020-06-09
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Conn.Close()
        End If

        'jrs 2014/01/29
        If Session("bReadGrpMaxs") Then
            SetUpMaxsTime()
            Session("bReadGrpMaxs") = False
        End If

    End Sub

    'jrs 2023/08/25 added gphTypeIndex
    'jrs 2016/05/20
    Private Sub CompositeGraph_Draw_All_compare(ByRef gphType As MortType, ByVal gphIndex As Integer, ByVal gphTypeIndex As Integer)
        '--------------------------------------------------------------
        Select Case gphIndex
            Case 1
                Session("EdCohort_tempregion") = cmbReg1.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg1.SelectedValue
                Session("EdCohort_tempyear") = cmbYr1.SelectedValue
            Case 2
                Session("EdCohort_tempregion") = cmbReg2.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg2.SelectedValue
                Session("EdCohort_tempyear") = cmbYr2.SelectedValue
            Case 3
                Session("EdCohort_tempregion") = cmbReg3.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg3.SelectedValue
                Session("EdCohort_tempyear") = cmbYr3.SelectedValue
            Case 4
                Session("EdCohort_tempregion") = cmbReg4.SelectedIndex + 1
                Session("EdCohort_regionname") = cmbReg4.SelectedValue
                Session("EdCohort_tempyear") = cmbYr4.SelectedValue
        End Select

        'jrs 2024/04/22
        vSQLiteCon.Close()
        vConnString = "DataSource=" + Session("dbsces")(gphIndex - 1) & ";"

        'jam 2025/05/31 Changing connection mode
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()


        'jam 2025/05/31 Changing connection mode
        vSQLiteCon = OpenJournalSQLite(vConnString)




        CompositeGraph_Draw_All(Session("dbsces")(gphIndex - 1), gphType, gphTypeIndex)

        'Load result to Compare array
        Dim i As Integer, j As Integer
        For i = 0 To NCohorts
            For j = 1 To 2
                Session("EC_TempPopDatabase_Comp")(i, j, gphIndex) = Session("EC_TempPopDatabase")(i, j)
            Next
            For j = 1 To gphType.NumSubTypes
                Session("EC_TempDatabase_Comp")(i, j, gphIndex) = Session("EC_TempDatabase")(i, j)
            Next
        Next

        'jam 2022/07/17
        If Session("cnvtoDeaths") Then
            ConvertToDeaths(NCohorts, gphType.NumSubTypes / 2)
        End If

    End Sub

    Private Sub mnuNonCommSub_Click()
        'mnuNormOff.Enabled = False
        'mnuNormOn.Enabled = False
        'mnuNormOff.Checked = True
        'mnuNormOn.Checked = False
        Session("EdCohort_GraphType") = 5
        'GraphTypeSave = 0
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub mnuInjuriesSub_Click()
        'mnuNormOff.Enabled = False
        'mnuNormOn.Enabled = False
        'mnuNormOff.Checked = True
        'mnuNormOn.Checked = False
        Session("EdCohort_GraphType") = 6
        'GraphTypeSave = 0
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    'dsb 2026/06/11
    Private Sub mnuAllMortSub_Click()
        'mnuNormOff.Enabled = False
        'mnuNormOn.Enabled = False
        'mnuNormOff.Checked = True
        'mnuNormOn.Checked = False
        Session("EdCohort_GraphType") = 7
        'GraphTypeSave = 0
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub CompositeFillDataGridAll(ByRef gphType As MortType)
        '--------------------------------------------------------------
        '4,1,2,3

        Dim iNCohorts As Integer
        'dsb 2026/06/23 Male total and Female total column numbers
        Dim iHalfSubTypes As Integer = gphType.NumSubTypes / 2
        Dim iMaleTotalCol As Integer = gphType.NumSubTypes + iHalfSubTypes + 1
        Dim iFemaleTotalCol As Integer = gphType.NumSubTypes + iHalfSubTypes + 2
        If Session("bShow100") Then
            iNCohorts = NCohorts
        Else
            iNCohorts = NCohorts - 1
        End If

        Dim i As Integer
        grdcohort.TotalRows = iNCohorts + 1
        grdcohort.RowCount = iNCohorts + 1

        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        'jrs 2013/08/16
        Dim MaxWidth As Integer = 150
        If MaxWidth * (gphType.NumSubTypes * 3 / 2 + 2) + 125 > grdcohort.Width.Value Then 'dsb 2026/06/23 changed from 3/2 to 3/2 + 2 to account for male total and female total columns 
            grdcohort.Width = CType(MaxWidth * (gphType.NumSubTypes * 3 / 2 + 2) + 125, Unit)
        End If

        For i = 1 To 1 + gphType.NumSubTypes * 3 / 2 + 2 'dsb 2026/06/23 changed from 3/2 to 3/2 + 2 to account for male total and female total columns
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            If i > 1 Then
                myCol.Width = MaxWidth
            Else
                myCol.Width = 125
            End If
            'jrs 2009/03/17
            If i > 1 And Request.Browser.Type.StartsWith("IE") Then
                myCol.Type = TMSWebControls.Column.ColumnType.LinkField
            End If
            myCol.HeaderVAlignment = TMSWebControls.Column.VAligning.Top
            myCol.HeaderAlignment = TMSWebControls.Column.Aligning.Center

            grdcohort.Columns.Add(myCol)
        Next



        Dim l As Integer, j As Integer

        For l = iNCohorts To 0 Step -1

            If l = NCohorts Then
                grdcohort(0, l) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
            ElseIf l = 0 Then
                grdcohort(0, l) = "Infants"
            ElseIf l = 1 Then
                grdcohort(0, l) = "1- 4"
            Else
                grdcohort(0, l) = Str$(l * 5 - 5) + "-" + Str$(l * 5 - 1)
            End If
            'cdv 2021/01/14 ****START**** Flip Data Entry for Gender
            Dim IndexModifier1 As Integer
            Dim IndexModifier2 As Integer

            If bMaleFirst Then
                IndexModifier1 = 0
                IndexModifier2 = 0
            Else
                IndexModifier1 = gphType.NumSubTypes / 2
                IndexModifier2 = -(gphType.NumSubTypes / 2)
            End If
            Dim maleTotal As Single = 0 'dsb 2026/06/23
            Dim femaleTotal As Single = 0
            'male
            For j = 1 To gphType.NumSubTypes / 2
                grdcohort(j + IndexModifier1, l) = Format(Session("EC_TempDatabase")(l%, j), "0.0000")
                maleTotal = maleTotal + CSng(Session("EC_TempDatabase")(l%, j)) 'dsb 2026/06/23 
            Next j

            'female
            For j = 1 + gphType.NumSubTypes / 2 To gphType.NumSubTypes
                grdcohort(j + IndexModifier2, l) = Format(Session("EC_TempDatabase")(l%, j), "0.0000")
                femaleTotal = femaleTotal + CSng(Session("EC_TempDatabase")(l%, j)) 'dsb 2026/06/23
            Next j

            'dsb 2026/06/23 male/female total columns
            If bMaleFirst Then
                grdcohort(iMaleTotalCol, l) = Format(maleTotal, "0.0000")
                grdcohort(iFemaleTotalCol, l) = Format(femaleTotal, "0.0000")
            Else
                grdcohort(iMaleTotalCol, l) = Format(femaleTotal, "0.0000")
                grdcohort(iFemaleTotalCol, l) = Format(maleTotal, "0.0000")
            End If

            'total
            'dsb 2026/06/29 When showing number of deaths, total should be the sum, = Male + Female
            If Session("cnvtoDeaths") Then
                For j = 1 To gphType.NumSubTypes / 2
                    grdcohort(j + gphType.NumSubTypes, l) = Format(Session("EC_TempDatabase")(l%, j) + Session("EC_TempDatabase")(l%, j + (gphType.NumSubTypes / 2)), "0.0000")
                Next j
            Else 'When showing deaths per 1000 people, total should be the weighted average of male and female
                If Session("EC_TempPopDatabase")(l%, 1) + Session("EC_TempPopDatabase")(l%, 2) > 0 Then
                    For j = 1 To gphType.NumSubTypes / 2
                        grdcohort(j + gphType.NumSubTypes, l) = Format((Session("EC_TempDatabase")(l%, j + (gphType.NumSubTypes / 2)) * Session("EC_TempPopDatabase")(l%, 2) +
                Session("EC_TempDatabase")(l%, j) * Session("EC_TempPopDatabase")(l%, 1)) / (Session("EC_TempPopDatabase")(l%, 1) + Session("EC_TempPopDatabase")(l%, 2)), "0.0000")
                    Next j
                Else
                    For j = 1 To gphType.NumSubTypes / 2
                        grdcohort(j + gphType.NumSubTypes, l) = "0.0000"
                    Next j
                End If
            End If

        Next l%

        grdcohort.Columns(0).title = "Age Cohort"
        Dim GenderLabel1 As String
        Dim GenderLabel2 As String
        If bMaleFirst Then
            GenderLabel1 = " Male"
            GenderLabel2 = " Female"
        Else
            GenderLabel1 = " Female"
            GenderLabel2 = " Male"
        End If

        'Male
        For j = 1 To gphType.NumSubTypes / 2
            grdcohort.Columns(j).title = gphType.getSubTypeName((2 * j) - 1) & GenderLabel1
        Next j
        'Female
        For j = 1 To gphType.NumSubTypes / 2
            grdcohort.Columns(j + gphType.NumSubTypes / 2).title = gphType.getSubTypeName((2 * j) - 1) & GenderLabel2
        Next j
        'cdv 2021/01/14 ****END****
        'Total
        For j = 1 To gphType.NumSubTypes / 2
            grdcohort.Columns(j + gphType.NumSubTypes).title = gphType.getSubTypeName((2 * j) - 1) & " Total"
        Next j
        'dsb 2026/06/23 Male/Female total columns
        If bMaleFirst Then
            grdcohort.Columns(iMaleTotalCol).title = "Male Total"
            grdcohort.Columns(iFemaleTotalCol).title = "Female Total"
        Else
            grdcohort.Columns(iMaleTotalCol).title = "Female Total"
            grdcohort.Columns(iFemaleTotalCol).title = "Male Total"
        End If

        lbldata.Text = gphType.sTitle & " Distribution for " +
            Session("EdCohort_regionname") + " in Year " + Session("EdCohort_tempyear").ToString + " [" + Session("EdCG_Casename") + "]" +
            " - Number of Deaths per 1000 People by Type (only) and Gender" 'gphAgedst(gphIndex).BottomTitle

    End Sub

    Protected Sub grdcohort_CellClick(ByVal sender As Object, ByVal e As TMSWebControls.ModEventArgs) Handles grdcohort.CellClick

        Dim cc As Integer 'Cohort
        Dim kk As Integer 'Type of Mortality
        Dim rr As Integer
        cc = e.Row
        kk = e.Column
        rr = Session("EdCohort_tempregion")
        Session("lblDataOrg") = lbldata.Text

        'Open Database
        Dim dbBase As String, dbWork As String
        'If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
        '    dbBase = Session("PutFdrvRun") & "IFsPop.mdb"
        'Else
        '    dbBase = Session("PutFdrvRun") & "IFsPopHist.mdb"
        'End If
        'dbWork = Session("PutFdrvRun") & "IFsPopWork.mdb"

        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                dbBase = Session("PutFdrvRun") & "IFsPop.mdb"
            Else
                Session("SQLiteDB") = "IFsPop.db"

                dbBase = Session("PutFdrvRun") & "IFsPop.db"
            End If
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then


                dbBase = Session("PutFdrvRun") & "IFsPopHist.mdb"


            Else
                Session("SQLiteDB") = "IFsPopHist.db"

                dbBase = Session("PutFdrvRun") & "IFsPopHist.db"

            End If
        End If
        'dbWork = Session("PutFdrvRun") & "IFsPopWork.mdb"

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then


            dbWork = Session("PutFdrvRun") & "IFsPopWork.mdb"


        Else
            Session("SQLiteDB") = "IFsPopWork.db"

            dbWork = Session("PutFdrvRun") & "IFsPopWork.db"

        End If





        Dim rs As DataTable
        Dim sql As String
        Dim sFldName As String
        Dim k As Integer
        Dim sFldNamePop As String

        Select Case Session("EdCohort_GraphType")
            Case 0 'All types
                Select Case kk
                    Case 1
                        sFldName = "MortPerMalesComDis"
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = "Communicable Diseases for Males " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 2
                        sFldName = "MortPerMalesNonComDis"
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = "Non Communicable Diseases for Males " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 3
                        sFldName = "MortPerMalesInjuries"
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = "Injuries for Males " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 4
                        sFldName = "MortPerFeMalesComDis"
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = "Communicable Diseases for Females " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 5
                        sFldName = "MortPerFeMalesNonComDis"
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = "Non Communicable Diseases for Females " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 6
                        sFldName = "MortPerFeMalesInjuries"
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = "Injuries for Females " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 7
                        sFldName = "MortPerMalesComDis, MortPerFeMalesComDis"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "Communicable Diseases Total " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 8
                        sFldName = "MortPerMalesNonComDis, MortPerFeMalesNonComDis"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "Non Communicable Diseases Total " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 9
                        sFldName = "MortPerMalesInjuries, MortPerFeMalesInjuries"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "Injuries Total " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 10 'jrs 2008/04/18 adding Totoal of Totals
                        sFldName = "MortPerMalesComDis, MortPerFeMalesComDis, MortPerMalesNonComDis, MortPerFeMalesNonComDis, MortPerMalesInjuries, MortPerFeMalesInjuries"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "Total Mortality " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                End Select
            Case 1 'Communicable
                Select Case kk
                    Case 1
                        sFldName = "MortPerMalesComDis"
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = "Communicable Diseases for Males " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 2
                        sFldName = "MortPerFeMalesComDis"
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = "Communicable Diseases for Females " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 3
                        sFldName = "MortPerMalesComDis, MortPerFeMalesComDis"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "Communicable Diseases Total " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                End Select
            Case 2 'Non Communicable
                Select Case kk
                    Case 1
                        sFldName = "MortPerMalesNonComDis"
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = "Non Communicable Diseases for Males " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 2
                        sFldName = "MortPerFeMalesNonComDis"
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = "Non Communicable Diseases for Females " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 3
                        sFldName = "MortPerMalesNonComDis, MortPerFeMalesNonComDis"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "Non Communicable Diseases Total " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                End Select
            Case 3 'Injuries
                Select Case kk
                    Case 1
                        sFldName = "MortPerMalesInjuries"
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = "Injuries for Males " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 2
                        sFldName = "MortPerFeMalesInjuries"
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = "Injuries for Females " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 3
                        sFldName = "MortPerMalesInjuries, MortPerFeMalesInjuries"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "Injuries Total " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                End Select
            Case 4 'Communicable Det
                Select Case kk
                    Case 1
                        sFldName = "ComDisNoAIDSMales"
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = "Communicable Diseases (Excluding AIDS) for Males " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 2
                        sFldName = "AIDSMales"
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = "AIDS for Males " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 3
                        sFldName = "ComDisNoAIDSFemales"
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = "Communicable Diseases (Excluding AIDS) for Females " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 4
                        sFldName = "AIDSFemales"
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = "AIDS for Females " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 5
                        sFldName = "ComDisNoAIDSMales, ComDisNoAIDSFemales"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "Communicable Diseases (Excluding AIDS) Total " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Case 6
                        sFldName = "AIDSMales, AIDSFemales"
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = "AIDS Total " & grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                End Select
            Case 5 'Non Communicable Det
                If kk > 0 Then
                    If kk <= NonCommDis.NumSubTypes / 2 Then
                        k = 2 * kk - 1
                        sFldName = NonCommDis.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = NonCommDis.getSubTypeName(k) & " for Males " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    ElseIf kk <= NonCommDis.NumSubTypes Then
                        k = 2 * (kk - NonCommDis.NumSubTypes / 2)
                        sFldName = NonCommDis.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = NonCommDis.getSubTypeName(k - 1) & " for Females " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Else
                        k = 2 * (kk - NonCommDis.NumSubTypes)
                        sFldName = NonCommDis.getSubTypeFieldName(k - 1)
                        sFldName = sFldName & ", " & NonCommDis.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = NonCommDis.getSubTypeName(k - 1) & " Total " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    End If
                End If
            Case 6 'Injuries Det
                If kk > 0 Then
                    If kk <= Injuries.NumSubTypes / 2 Then
                        k = 2 * kk - 1
                        sFldName = Injuries.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = Injuries.getSubTypeName(k) & " for Males " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    ElseIf kk <= Injuries.NumSubTypes Then
                        k = 2 * (kk - Injuries.NumSubTypes / 2)
                        sFldName = Injuries.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = Injuries.getSubTypeName(k - 1) & " for Females " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Else
                        k = 2 * (kk - Injuries.NumSubTypes)
                        sFldName = Injuries.getSubTypeFieldName(k - 1)
                        sFldName = sFldName & ", " & Injuries.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = Injuries.getSubTypeName(k - 1) & " Total " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    End If
                End If

            Case 7 'dsb 2026/06/23 All Mortality Subtypes
                If kk > 0 Then
                    If kk <= AllMortSubTypes.NumSubTypes / 2 Then
                        k = 2 * kk - 1
                        sFldName = AllMortSubTypes.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationMales"
                        lbldata.Text = AllMortSubTypes.getSubTypeName(k) & " for Males " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    ElseIf kk <= AllMortSubTypes.NumSubTypes Then
                        k = 2 * (kk - AllMortSubTypes.NumSubTypes / 2)
                        sFldName = AllMortSubTypes.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationFemales"
                        lbldata.Text = AllMortSubTypes.getSubTypeName(k - 1) & " for Females " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    Else
                        k = 2 * (kk - AllMortSubTypes.NumSubTypes)
                        sFldName = AllMortSubTypes.getSubTypeFieldName(k - 1)
                        sFldName = sFldName & ", " & AllMortSubTypes.getSubTypeFieldName(k)
                        sFldNamePop = "PopulationMales, PopulationFemales"
                        lbldata.Text = AllMortSubTypes.getSubTypeName(k - 1) & " Total " &
                            grdcohort(0, e.Row) & ", " & Session("EdCohort_regionname")
                    End If
                End If

        End Select

        grdcohortDet.Clear()
        grdcohortDet.Columns.Clear()
        Dim myCol As TMSWebControls.Column
        Dim TotDet As Integer, i As Integer
        With grdcohortDet
            'jrs 2009/03/11 adding other scenarios
            If Session("db2Path") = dbBase Or Session("db2Path") = dbWork Then
                TotDet = 3
            Else
                TotDet = 4
            End If

            For i = 1 To TotDet
                myCol = New TMSWebControls.Column
                myCol.WidthType = TMSWebControls.WType.Absolute
                If i > 1 Then
                    myCol.Width = 150 '1300
                Else
                    myCol.Width = 125 '1100
                End If

                myCol.HeaderVAlignment = TMSWebControls.Column.VAligning.Top
                myCol.HeaderAlignment = TMSWebControls.Column.Aligning.Center
                .Columns.Add(myCol)
            Next

            .Columns(0).title = "Year"
            .Columns(1).title = "Base"
            .Columns(2).title = "Work"
            'jrs 2009/03/11 adding other scenarios
            If Session("db2Path") <> dbBase And Session("db2Path") <> dbWork Then
                .Columns(3).title = CaseName$
            End If
        End With

        'jrs 2009/03/11
        'Loading All Years for the base case
        LoadAllYrsMortIntoCol(sFldName, sFldNamePop, cc, rr, dbBase, 1)

        'Loading All Years for the working case
        LoadAllYrsMortIntoCol(sFldName, sFldNamePop, cc, rr, dbWork, 2)

        'jrs 2009/03/11 adding other scenarios
        If Session("db2Path") <> dbBase And Session("db2Path") <> dbWork Then
            'Loading All Years for other scenarios
            LoadAllYrsMortIntoCol(sFldName, sFldNamePop, cc, rr, Session("db2Path"), 3)
        End If


        grdcohortDet.Visible = True
        grdcohort.Visible = False
        lblJS.Visible = False
        hfTitle.Value = lbldata.Text.Replace("<br/>", "")
        cmdLine.Visible = True

    End Sub

    'jrs 2009/03/11 same logic for Base, Working and other scenarios
    Private Sub LoadAllYrsMortIntoCol(ByVal sFldName As String, ByVal sFldNamePop As String,
        ByVal cc As Integer, ByVal rr As Integer, ByRef dbX As String, ByVal ColId As Integer)
        '1. sFldName the name of the fields to read from the database
        '2. sFldNamePop the name of the fields to read from the population table
        '3. cc is the cohort id
        '4. rr is the region id
        '5. dbX Database in use
        '6. ColId is the column numer (0 based) to load mortality values

        If Session("EdCohort_Grouping") = True Then
            LoadAllYrsMortIntoColforGroups(sFldName, sFldNamePop, cc, rr, dbX, ColId)
            Exit Sub
        End If

        'Dim Conn As OleDb.OleDbConnection
        'Conn = ADO_OleDB_CreateConnection(dbX)
        'Conn.Open()

        Dim Conn As OleDb.OleDbConnection
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'JAM 2020-06-01
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))
            Conn = ADO_OleDB_CreateConnection(Replace(dbX, ".db", ".mdb"))
            Conn.Open()
            myCMD.Connection = Conn
        End If


        Dim sql As String
        Dim rsPop As OleDb.OleDbDataReader, rs As OleDb.OleDbDataReader
        'Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = Conn
        '2 readers on the same command are not allowed
        Dim myCMD2 As OleDb.OleDbCommand = New OleDb.OleDbCommand
        myCMD2.Connection = Conn
        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))

        Dim k As Integer
        Dim vCountReader As Integer
        Dim vCountReader2 As Integer


        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            'jrs 2008/04/18
            If InStr(sFldName, ",") > 0 Then
                'Load Population Data
                If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                    sql = "SELECT Years, " & sFldNamePop & " FROM PopBaseCase WHERE NCohorts = " & CStr(cc) &
                    " AND Region = " & CStr(rr) & " AND Years >= " & CStr(FirstYearForecast) & " AND Years <= " &
                    CStr(Session("LastYearForecast")) & " ORDER BY Years"
                Else
                    sql = "SELECT Years, " & sFldNamePop & " FROM PopBaseCase WHERE NCohorts = " & CStr(cc) &
                    " AND Region = " & CStr(rr) & " AND Years >= " & CStr(FirstYearHistory) & " AND Years <= " &
                    CStr(LastYearHistory) & " ORDER BY Years"
                End If
                rsPop = ADO_OleDB_GetReader(myCMD2, sql)
            End If

            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                sql = "SELECT Years, " & sFldName & " FROM MorDetBaseCase WHERE NCohorts = " & CStr(cc) &
                " AND Region = " & CStr(rr) & " AND Years >= " & CStr(FirstYearForecast) & " AND Years <= " &
                CStr(Session("LastYearForecast")) & " ORDER BY Years"
            Else
                sql = "SELECT Years, " & sFldName & " FROM MorDetBaseCase WHERE NCohorts = " & CStr(cc) &
                " AND Region = " & CStr(rr) & " AND Years >= " & CStr(FirstYearHistory) & " AND Years <= " &
                CStr(LastYearHistory) & " ORDER BY Years"
            End If

            rs = ADO_OleDB_GetReader(myCMD, sql)

            If ColId = 1 Then
                'Count how many rows (not implemented in reader)
                Dim Count As Integer = 0

                Do While rs.Read
                    Count = Count + 1
                Loop
                rs.Close()
                'Open the reader again
                rs = ADO_OleDB_GetReader(myCMD, sql)

                grdcohortDet.TotalRows = Count
                grdcohortDet.RowCount = Count
            End If

            Dim bEOF As Boolean
            With grdcohortDet
                rs.Read()
                For k = 0 To .TotalRows - 1
                    If ColId = 1 Then
                        grdcohortDet(0, k) = rs.Item("Years")
                    End If
                    If grdcohortDet(0, k) = rs.Item("Years") Then
                        If rs.FieldCount = 2 Then
                            grdcohortDet(ColId, k) = Format(rs.Item(1), "0.0000")
                        ElseIf rs.FieldCount = 3 Then
                            'jrs 2008/04/18 .text = rs.item(1) + rs.item(2)
                            rsPop.Read()
                            grdcohortDet(ColId, k) = Format((rs.Item(1) * rsPop.Item(1) + rs.Item(2) * rsPop.Item(2)) / (rsPop.Item(1) + rsPop.Item(2)), "0.0000")
                        Else 'jrs 2008/04/18 total of totals
                            rsPop.Read()
                            grdcohortDet(ColId, k) = Format((rs.Item(1) * rsPop.Item(1) + rs.Item(2) * rsPop.Item(2) +
                            rs.Item(3) * rsPop.Item(1) + rs.Item(4) * rsPop.Item(2) +
                            rs.Item(5) * rsPop.Item(1) + rs.Item(6) * rsPop.Item(2)) / (rsPop.Item(1) + rsPop.Item(2)), "0.0000")
                        End If
                    End If
                    bEOF = Not (rs.Read())
                    'jrs 2007/10/15
                    If bEOF Then Exit For
                Next k
            End With

            'jrs 2008/04/18
            rs.Close()
            If InStr(sFldName, ",") > 0 Then
                rsPop.Close()
            End If

        Else

            If InStr(sFldName, ",") > 0 Then
                'Load Population Data
                If UCase(Session("ActiveBase")) = "IFSBASE.RUN.DB" Then
                    sql = "SELECT TimeId, " & sFldNamePop & " FROM Pop_Base_Case WHERE CohortId = " & CStr(cc) &
                    " AND RegionId = " & CStr(rr) & " AND TimeId >= " & CStr(FirstYearForecast) & " AND TimeId <= " &
                    CStr(Session("LastYearForecast")) & " ORDER BY TimeId"
                Else
                    sql = "SELECT TimeId, " & sFldNamePop & " FROM Pop_Base_Case WHERE CohortId = " & CStr(cc) &
                    " AND RegionId = " & CStr(rr) & " AND TimeId >= " & CStr(FirstYearHistory) & " AND TimeId <= " &
                    CStr(LastYearHistory) & " ORDER BY TimeId"
                End If
                'rsPop = ADO_OleDB_GetReader(myCMD2, sql)

                vSQLiteCommand2 = vSQLiteCon.CreateCommand()
                vSQLiteCommand2.CommandText = sql
                vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()
                vCountReader2 = vSQLiteReader2.FieldCount

            End If

            If UCase(Session("ActiveBase")) = "IFSBASE.RUN.DB" Then
                sql = "SELECT TimeId, " & sFldName & " FROM Mor_Det_Base_Case WHERE CohortId = " & CStr(cc) &
                " AND RegionId = " & CStr(rr) & " AND TimeId >= " & CStr(FirstYearForecast) & " AND TimeId <= " &
                CStr(Session("LastYearForecast")) & " ORDER BY TimeId"
            Else
                sql = "SELECT TimeId, " & sFldName & " FROM Mor_Det_Base_Case WHERE CohortId = " & CStr(cc) &
                " AND RegionId = " & CStr(rr) & " AND TimeId >= " & CStr(FirstYearHistory) & " AND TimeId <= " &
                CStr(LastYearHistory) & " ORDER BY TimeId"
            End If

            'rs = ADO_OleDB_GetReader(myCMD, sql)
            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = sql
            vSQLiteReader = vSQLiteCommand.ExecuteReader()
            vCountReader = vSQLiteReader.FieldCount

            If ColId = 1 Then
                'Count how many rows (not implemented in reader)
                'Dim Count As Integer = 0

                'Do While rs.Read
                '    Count = Count + 1
                'Loop
                'rs.Close()

                Dim Count As Integer = 0


                While (vSQLiteReader.Read())
                    Count = Count + 1

                End While


                'Open the reader again
                'rs = ADO_OleDB_GetReader(myCMD, sql)

                grdcohortDet.TotalRows = Count
                grdcohortDet.RowCount = Count
            End If

            Dim bEOF As Boolean
            With grdcohortDet
                'rs.Read()
                For k = 0 To .TotalRows - 1
                    While (vSQLiteReader.Read())
                        While (vSQLiteReader2.Read())
                            If ColId = 1 Then
                                grdcohortDet(0, k) = vSQLiteReader("TimeId")
                            End If
                            If grdcohortDet(0, k) = vSQLiteReader("TimeId") Then
                                If vCountReader = 2 Then
                                    grdcohortDet(ColId, k) = Format(vSQLiteReader(1), "0.0000")
                                ElseIf vCountReader = 3 Then
                                    'jrs 2008/04/18 .text = rs.item(1) + rs.item(2)
                                    'rsPop.Read()
                                    grdcohortDet(ColId, k) = Format((vSQLiteReader(1) * vSQLiteReader2(1) + vSQLiteReader(2) * vSQLiteReader2(2)) / (vSQLiteReader2(1) + vSQLiteReader(2)), "0.0000")
                                Else 'jrs 2008/04/18 total of totals
                                    'rsPop.Read()
                                    grdcohortDet(ColId, k) = Format((vSQLiteReader(1) * vSQLiteReader2(1) + vSQLiteReader(2) * vSQLiteReader2(2) +
                            vSQLiteReader(3) * vSQLiteReader2(1) + vSQLiteReader(4) * vSQLiteReader2(2) +
                            vSQLiteReader(5) * vSQLiteReader2(1) + vSQLiteReader(6) * vSQLiteReader2(2)) / (vSQLiteReader2(1) + vSQLiteReader2(2)), "0.0000")
                                End If
                            End If


                        End While
                    End While
                Next k
            End With





        End If

    End Sub

    Private Sub LoadAllYrsMortIntoColforGroups(ByVal sFldName As String, ByVal sFldNamePop As String,
        ByVal cc As Integer, ByVal rr As Integer, ByRef dbX As String, ByVal ColId As Integer)
        '1. sFldName the name of the fields to read from the database
        '2. sFldNamePop the name of the fields to read from the population table
        '3. cc is the cohort id
        '4. rr is the region id
        '5. dbX Database in use
        '6. ColId is the column numer (0 based) to load mortality values

        On Error GoTo ErrHandler00
        Dim Conn As OleDb.OleDbConnection
        Conn = ADO_OleDB_CreateConnection(dbX)
        Conn.Open()

        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))

        Dim sql As String

        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow


        Dim TotYrs As Integer
        Dim k As Integer


        'IfsConn.Open()

        'jam 2025/05/31
        'IfsConn.Open()
        If IfsConn.State <> ConnectionState.Open Then
            IfsConn.Open()
        End If

        Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
        Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
        Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)

        regDS = New DataSet
        grpDA.Fill(regDS, "Groups")
        regDA.Fill(regDS, "Regions")
        regIDDA.Fill(regDS, "RegionIDs")
        grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
        regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))

        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            sql = "SELECT m.Years, " & sFldName & ", " & sFldNamePop & ", m.Region FROM " &
                "MorDetBaseCase as m, PopBaseCase as p WHERE m.Region = p.Region AND m.Years " &
                "= p.Years AND m.NCohorts = p.NCohorts AND m.NCohorts = " & CStr(cc) &
                " AND m.Years >= " & CStr(FirstYearForecast) & " AND m.Years <= " &
                CStr(Session("LastYearForecast"))

            TotYrs = Session("LastYearForecast") - FirstYearForecast + 1
        Else
            sql = "SELECT Years, " & sFldName & ", " & sFldNamePop & ", m.Region FROM " &
                "MorDetBaseCase as m, PopBaseCase as p WHERE m.Region = p.Region AND m.Years " &
                "= p.Years AND m.NCohorts = p.NCohorts AND m.NCohorts = " & CStr(cc) &
                " AND m.Years >= " & CStr(FirstYearHistory) & " AND m.Years <= " &
                CStr(LastYearHistory)

            TotYrs = LastYearHistory - FirstYearHistory + 1
        End If

        Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, Conn)
        mortDA.Fill(regDS, "Mortality")
        Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Mortality").Columns("Region"))


        If ColId = 1 Then
            grdcohortDet.TotalRows = TotYrs
            grdcohortDet.RowCount = TotYrs
        End If

        Dim PopYr(TotYrs) As Single

        For Each pppRow In regDS.Tables("Groups").Rows
            If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                For Each ppRow In pppRow.GetChildRows(grpRegRel)
                    For Each pRow In ppRow.GetParentRows(regRegIdRel)
                        For Each cRow In pRow.GetChildRows(regMortRel)

                            k = cRow.Item("Years") - Session("FirstYear")

                            If cRow.Table.Columns.Count = 4 Then
                                grdcohortDet(ColId, k) = Val(grdcohortDet(ColId, k)) + cRow.Item(1) * cRow.Item(2)
                                PopYr(k) = PopYr(k) + cRow.Item(2)
                            ElseIf cRow.Table.Columns.Count = 6 Then
                                grdcohortDet(ColId, k) = Val(grdcohortDet(ColId, k)) + cRow.Item(1) * cRow.Item(3) +
                                    cRow.Item(2) * cRow.Item(4)
                                PopYr(k) = PopYr(k) + cRow.Item(3) + cRow.Item(4)
                            Else 'jrs 2008/04/18 total of totals
                                grdcohortDet(ColId, k) = Val(grdcohortDet(ColId, k)) + cRow.Item(1) * cRow.Item(7) +
                                    cRow.Item(2) * cRow.Item(8) +
                                    cRow.Item(3) * cRow.Item(7) +
                                    cRow.Item(4) * cRow.Item(8) +
                                    cRow.Item(5) * cRow.Item(7) +
                                    cRow.Item(6) * cRow.Item(8)
                                PopYr(k) = PopYr(k) + cRow.Item(7) + cRow.Item(8)
                            End If
                        Next
                    Next
                Next
            End If
        Next

        For k = 0 To grdcohortDet.TotalRows - 1
            If ColId = 1 Then
                grdcohortDet(0, k) = Session("FirstYear") + k
            End If
            grdcohortDet(ColId, k) = Format(CSng(grdcohortDet(ColId, k)) / PopYr(k), "0.0000")
        Next k
ErrHandler00:
        Exit Sub
    End Sub

    Protected Sub cmdLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLine.Click
        'jrs 2009/03/20
        grdcohortDet.Visible = False
        lblJSLine.Visible = True
        Session("grdCohortDet") = grdcohortDet
        PackJScritpBuilderLine()
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        Dim iNCohortsWD As Integer = NCohorts
        'iNCohortsWD = FindLastCohortWithData(Session("EC_TempDataBase_Comp"), 6, gphIdx)

        Dim iNCohorts As Integer
        iNCohorts = iNCohortsWD 'jrs2013/08/16 NCohorts

        Dim Ages() As String
        ReDim Ages(iNCohorts)
        If iNCohorts = NCohorts Then
            Ages(iNCohorts) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
        Else
            Ages(iNCohorts) = CStr(iNCohorts * 5 - 5) & "-" & CStr(iNCohorts * 5 - 1)
        End If

        Dim l As Integer
        For l = 2 To iNCohorts - 1
            Ages(l) = Str(l * 5 - 5) & "-" & Str(l * 5 - 1)
        Next l
        Ages(0) = "Infants"
        Ages(1) = "1-4"





        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "            var mycategories = [" & vbCr
        For l = 0 To iNCohorts
            If l > 0 Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & "'" & Ages(l) & "'"
        Next
        sJscript2 = sJscript2 & "];" & vbCr

        sJscript2 = sJscript2 & "            var myPositions = [" & vbCr
        For l = 0 To iNCohorts
            If l > 0 Then
                sJscript2 = sJscript2 & ", "
            End If
            sJscript2 = sJscript2 & l.ToString
        Next
        sJscript2 = sJscript2 & "];" & vbCr

        If Session("bCompare") Then
            For l = 1 To 4
                sJscript2 = sJscript2 & PackJScritpBuilderContainter(l.ToString, iNCohortsWD)
            Next
        Else
            sJscript2 = sJscript2 & PackJScritpBuilderContainter("", iNCohortsWD)
        End If

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

        'lblJSLine.Text = Replace(lblJS.Text, "AnyChartDataforCohorts.aspx", "AnyChartDataforMort.aspx")

    End Sub

    Private Sub PackJScritpBuilderLine()
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/12/06 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr
        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxisXLabel As String
        sAxisXLabel = "Year"
        Dim sAxisYLabel As String
        sAxisYLabel = "Deaths per 1,000"

        sJscript2 = sJscript2 & "xAxis: {" & vbCr

        'jam 2025/12/06 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr



        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'," & vbCr





        sJscript2 = sJscript2 & "}" & vbCr

        'Load Years
        '----------
        'jrs 2015/09/11 removing categories from Bar chart, problematic for multiple series
        'it wasn't showing all bars in the first and last categories
        sJscript2 = sJscript2 & ", categories: ["
        Dim i As Integer
        For i = 0 To Session("grdCohortDet").TotalRows - 1
            If i > 0 Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & "'" & Session("grdCohortDet")(0, i) & "'"
        Next
        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr

        'sJscript2 = sJscript2 & ", min: " & Session("grdCohortDet")(0, 0) & vbCr
        'sJscript2 = sJscript2 & ", max: " & Session("grdCohortDet")(0, Session("grdCohortDet").TotalRows - 1) & vbCr

        'jrs 2025/07/21
        sJscript2 = sJscript2 & ", labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06 Increasing font 
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr


        'jam 2025/12/06 Increasing font for y axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr


        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr


        'jrs 2015/09/08
        sJscript2 = sJscript2 & "minPadding: 0," & vbCr
        'jrs 2025/07/21
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06 Increasing font for y axis
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "legend: {" & vbCr

        'jam 2025/12/06 Increasing fonts
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr


        sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
        sJscript2 = sJscript2 & "align:      'center'," & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
        sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        For i = 1 To Session("grdCohortDet").Columns.count - 1
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            sJscript2 = sJscript2 & PlotColToJSSeriesLine(i, Session("grdCohortDet"), Session("bShow100"))
        Next i
        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        'jam 2023/01/11
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

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95 - 50;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 200;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJSLine.Text = sJscript2

    End Sub

    Private Function PackJScritpBuilderContainter(ByVal sContainterIdx As String, ByVal iNCohortsWD As Integer) As String
        Dim sJscript2 As String = ""
        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container" & sContainterIdx & "').highcharts({" & vbCr

        'jam 2025/03/01 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        'sJscript2 = sJscript2 & "events: {" & vbCr
        'sJscript2 = sJscript2 & "load: function () {" & vbCr
        'sJscript2 = sJscript2 & "this.xAxis[0].tickInterval = 1;" & vbCr
        'sJscript2 = sJscript2 & "this.redraw();" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "type: 'bar'," & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        sJscript2 = sJscript2 & "events: {"
        sJscript2 = sJscript2 & "load: function(event) {"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.visibility = 'hidden';"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.height = 0;"
        sJscript2 = sJscript2 & "}}"
        sJscript2 = sJscript2 & "}," & vbCr

        Dim gphIdx As Integer = 0
        If sContainterIdx > "" Then
            gphIdx = CInt(sContainterIdx)
        End If

        Dim Index As Short
        Dim graphtitle As String = ""
        Dim regionname As String
        Dim CaseName As String
        Dim TempYear As Short
        Dim subtitle As String = ""
        Dim gphType As MortType
        Index = Session("EdCG_Index")
        regionname = Session("EdCG_Regionname")
        CaseName = Session("EdCG_Casename")
        TempYear = Session("EdCG_tempyear")
        'jrs 2014/02/21
        Dim iNCohorts As Integer = iNCohortsWD
        If Session("bShow100") = False Then
            iNCohorts = NCohorts - 1
        End If

        If Index = 0 Then
            graphtitle = "Age, Gender and Type Distribution of Mortality"
        ElseIf Index = 1 Then
            graphtitle$ = "People dying of Communicable Disease"
        ElseIf Index = 2 Then
            graphtitle$ = "People dying of Non Communicable Disease"
        ElseIf Index = 3 Then
            graphtitle$ = "People dying of Injury"
        Else
            Select Case Index
                Case 4
                    gphType = Session("CommDis")
                Case 5
                    gphType = Session("NonCommDis")
                Case 6
                    gphType = Session("Injuries")
                Case 7 'dsb 2026/06/11
                    gphType = Session("AllMortSubTypes")
            End Select
            If Not (gphType Is Nothing) Then
                graphtitle$ = "Age, Gender and Sub Type Distribution of " & gphType.sTitle
            End If
        End If
        If Index > 0 And Index < 4 Then
            graphtitle = graphtitle & " by gender"
        End If

        'jrs 2014/02/11
        If Session("bCompare") Then
            If gphIdx < 5 Then
                graphtitle = ""
                subtitle = Session("EC_Reg")(gphIdx) & " in year " & Str(Session("EC_Yr")(gphIdx)) & " [" + Session("CaseNames")(gphIdx - 1) + "]"
            Else
                'graphtitle = graphtitle & " [" + CaseName$ + "]"
                subtitle = ""
            End If
        Else
            subtitle = "for " & regionname & " in year " & Str(TempYear) & " [" + CaseName$ + "]"
        End If

        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        'jrs 2026/04/23
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        Else
            sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
        End If
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr
        If (Index = 0 Or Index > 3) Then
            sJscript2 = sJscript2 & "x: 25 //center" & vbCr
        Else
            sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "subtitle: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        'jrs 2026/04/23
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        Else
            sJscript2 = sJscript2 & "fontSize: '14px'," & vbCr
        End If

        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       """ & subtitle & """," & vbCr
        If (Index = 0 Or Index > 3) Then
            sJscript2 = sJscript2 & "x: 25 //center" & vbCr
        Else
            sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        'cdv 2021/01/14 ****START**** Flip legend to match gender order
        If (Index = 0 Or Index > 3) Then
            sJscript2 = sJscript2 & "legend: {" & vbCr
            'jrs 2026/05/01
            If Session("bCompare") Then
                sJscript2 = sJscript2 & "enabled:        false," & vbCr
                'dsb 2026/06/23 Replaced the image with a dynamic legend
                imgMortCats.Visible = False
                litMortCatsLegend.Visible = True
                litMortCatsLegend.Text = BuildDynamicMortalityLegend(Index, gphType)
                'If Index = 0 Then
                '    imgMortCats.ImageUrl = "~/Images/Big3DiseaseTypes.png"
                '    imgMortCats.Width = 750
                'ElseIf Index = 4 Then
                '    imgMortCats.ImageUrl = "~/Images/CommDisTypes.png"
                '    imgMortCats.Width = 750
                'ElseIf Index = 5 Then
                '    imgMortCats.ImageUrl = "~/Images/NonCommDisTypes.png"
                '    imgMortCats.Width = 750
                'ElseIf Index = 6 Then
                '    imgMortCats.ImageUrl = "~/Images/InjuryTypes.png"
                '    imgMortCats.Width = 750
                'ElseIf Index = 7 Then
                '    imgMortCats.ImageUrl = "~/Images/AllMortalitySubTypes.png"
                '    imgMortCats.Width = 850
                '    imgMortCats.Height = 120
                'End If
            End If

            sJscript2 = sJscript2 & "x:       60," & vbCr

            'jam 2025/12/06 Increasing fonts
            sJscript2 = sJscript2 & "itemStyle: {" & vbCr
            'jrs 2026/04/23
            If Not Session("bCompare") Then
                sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
            Else
                sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
            End If

            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "} " & vbCr

            sJscript2 = sJscript2 & "}," & vbCr
        Else
            sJscript2 = sJscript2 & "legend: {" & vbCr

            'jrs 2026/05/01
            If Session("bCompare") Then
                sJscript2 = sJscript2 & "enabled:        false," & vbCr
                imgMortCats.ImageUrl = "~/Images/FemalesAndMales.png"
                imgMortCats.Width = 300
            End If

            'jam 2025/12/06 Increasing fonts
            sJscript2 = sJscript2 & "itemStyle: {" & vbCr
            'jrs 2026/04/23
            If Not Session("bCompare") Then
                sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
            Else
                sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
            End If
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}, " & vbCr



            sJscript2 = sJscript2 & "reversed:     true" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If
        'cdv 2021/01/14 ****END****
        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Age Categories'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06 Increasing font
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        'jrs 2026/04/23
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        Else
            sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
        End If
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "categories: mycategories, " & vbCr
        sJscript2 = sJscript2 & "reversed: false, " & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        'jrs 2025/07/21
        If gphIdx = 0 Then
            sJscript2 = sJscript2 & "style: {" & vbCr
            'jrs 2026/04/23
            If Not Session("bCompare") Then
                sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
            Else
                sJscript2 = sJscript2 & "fontSize: '14px'," & vbCr
            End If
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        'jrs 2026/04/23
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "step: 1" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr
        'jrs 2026/04/23
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "tickPositions: myPositions" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxis As String = "Deaths per 1000"

        'jam 2022/07/17
        If Not Session("cnvtoDeaths") Then
            sAxis = "Deaths per 1000"
        Else
            sAxis = "Numer of deaths in Thousands"
        End If


        Dim sMax As String = "", sMin As String = ""
        If Session("mnuFixCtSc") = True Or Session("mnuFixCteScTime") = True Or Session("mnuFixCtScMan") = True Then
            sMax = "max: " & Session("MaxScale")(Index)
            If (Index = 0 Or Index > 3) Then
                sMin = "min: " & CStr(-1 * Session("MaxScale")(Index))
            Else
                sMin = "min: 0"
            End If
        End If

        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "reversedStacks: false," & vbCr
        If sMax > "" Then
            sJscript2 = sJscript2 & sMax & ", " & vbCr
            sJscript2 = sJscript2 & sMin & ", " & vbCr
        End If
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jrs 2026/04/23
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        Else
            sJscript2 = sJscript2 & "fontSize: '14px'," & vbCr
        End If

        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        If Index = 0 Or Index > 3 Then
            sJscript2 = sJscript2 & "x: 0," & vbCr
        Else
            sJscript2 = sJscript2 & "x: -50," & vbCr
        End If
        sJscript2 = sJscript2 & "text:       '" & sAxis & "'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        'jrs 2025/07/21
        If gphIdx = 0 Then
            sJscript2 = sJscript2 & "style: {" & vbCr
            'jrs 2026/04/23
            If Not Session("bCompare") Then
                sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
            Else
                sJscript2 = sJscript2 & "fontSize: '14px'," & vbCr
            End If
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return Math.abs(this.value);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Index > 3 Or Index = 0 Then
            sJscript2 = sJscript2 & "plotOptions: {" & vbCr
            sJscript2 = sJscript2 & "series: {" & vbCr
            sJscript2 = sJscript2 & "stacking:   'normal'," & vbCr

            sJscript2 = sJscript2 & "events: {" & vbCr
            sJscript2 = sJscript2 & "legendItemClick: function () {" & vbCr
            sJscript2 = sJscript2 & "return false;" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr

            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<b>' + this.series.name + ', age ' + this.point.category + '</b><br/>' +"
        sJscript2 = sJscript2 & "'Population: ' + Highcharts.numberFormat(Math.abs(this.point.y), 2);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim bAllZeros(2) As Boolean
        'jrs 2014/02/24
        Dim iStart As Integer, iEnd As Integer, iMed As Integer
        If Session("bCompare") Then
            iStart = 1
            iMed = 1
            iEnd = 2
            If Index = 0 Then
                iMed = 3
                iEnd = 6
            ElseIf Index > 3 Then
                iMed = gphType.NumSubTypes / 2
                iEnd = gphType.NumSubTypes
            End If

            'jrs 2013/08/16
            iNCohortsWD = FindLastCohortWithData(Session("EC_TempDataBase_Comp"), iEnd, gphIdx)
            'jrs 2014/02/24
            If gphIdx < 5 Then
                bAllZeros(1) = True
                For i = 1 To iMed
                    If Session("EC_TempDataBase_Comp")(iNCohortsWD, i, gphIdx) > 0 Then
                        bAllZeros(1) = False
                        Exit For
                    End If
                Next
                bAllZeros(2) = True
                For i = iMed + 1 To iEnd
                    If Session("EC_TempDataBase_Comp")(iNCohortsWD, i, gphIdx) > 0 Then
                        bAllZeros(2) = False
                        Exit For
                    End If
                Next
            End If

            If gphIdx > 0 And gphIdx < 5 Then
                Dim j As Integer = 1
                'Males 1 to 3, Females 4 to 6
                If Index = 0 Or Index > 3 Then
                    For i = 1 To iEnd
                        If i > iMed Then
                            j = 2
                        End If
                        If i > 1 Then
                            sJscript2 = sJscript2 & ","
                        End If
                        sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("EC_TempDataBase_Comp"), Session("bShow100"), Index, gphType, gphIdx, iNCohortsWD, bAllZeros(j)) 'jrs 2016/05/20 replaced ((i - 1) \ 3) + 1 with j
                    Next
                Else
                    'cdv 2021/01/14 ****START**** Flip Data Entry to match Gender
                    Dim GenderModifier1 As Integer
                    Dim GenderModifier2 As Integer
                    If bMaleFirst Then
                        GenderModifier1 = 3
                        GenderModifier2 = 0
                    Else
                        GenderModifier1 = 0
                        GenderModifier2 = 3
                    End If

                    'Males
                    sJscript2 = sJscript2 & PlotColToJSSeries(Index + GenderModifier1, Session("EC_TempDataBase_Comp"), Session("bShow100"), Index, Nothing, gphIdx, iNCohortsWD, True)
                    'Females
                    sJscript2 = sJscript2 & "," & PlotColToJSSeries(Index + GenderModifier2, Session("EC_TempDataBase_Comp"), Session("bShow100"), Index, Nothing, gphIdx, iNCohortsWD, True)
                End If
            End If
        Else
            iStart = 1
            'jrs 2016/05/20
            iMed = 1
            iEnd = 2
            If Index = 0 Then
                iMed = 3
                iEnd = 6
            ElseIf Index > 3 Then
                iMed = gphType.NumSubTypes / 2
                iEnd = gphType.NumSubTypes
            End If
            'jrs 2013/08/16
            iNCohortsWD = FindLastCohortWithData(Session("EC_TempDataBase"), iEnd, 0)
            bAllZeros(1) = True
            For i = iStart To iMed
                If Session("EC_TempDataBase")(iNCohortsWD, i) > 0 Then
                    bAllZeros(1) = False
                    Exit For
                End If
            Next
            bAllZeros(2) = True
            For i = iMed + 1 To iEnd
                If Session("EC_TempDataBase")(iNCohortsWD, i) > 0 Then
                    bAllZeros(2) = False
                    Exit For
                End If
            Next

            If Index = 0 Then
                'Males 1 to 3, Females 4 to 6
                For i = 1 To 6
                    If i > 1 Then
                        sJscript2 = sJscript2 & ","
                    End If

                    sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("EC_TempDataBase"), Session("bShow100"), Index, Nothing, 0, iNCohortsWD, bAllZeros(((i - 1) \ 3) + 1))
                Next
            ElseIf Index < 4 Then
                'cdv 2021/01/14 ****START**** Flip Data Entry to match Gender
                Dim GenderModifier1 As Integer
                Dim GenderModifier2 As Integer
                If bMaleFirst Then
                    GenderModifier1 = 3
                    GenderModifier2 = 0
                Else
                    GenderModifier1 = 0
                    GenderModifier2 = 3
                End If

                'Males
                sJscript2 = sJscript2 & PlotColToJSSeries(Index + GenderModifier1, Session("EC_TempDataBase"), Session("bShow100"), Index, Nothing, 0, iNCohortsWD, True)
                'Females
                sJscript2 = sJscript2 & "," & PlotColToJSSeries(Index + GenderModifier2, Session("EC_TempDataBase"), Session("bShow100"), Index, Nothing, 0, iNCohortsWD, True)

                'cdv 2021/01/14 ****END****
            Else
                For i = 1 To gphType.NumSubTypes
                    If i > 1 Then
                        sJscript2 = sJscript2 & ","
                    End If
                    sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("EC_TempDataBase"), Session("bShow100"), Index, gphType, 0, iNCohortsWD, bAllZeros(((i - 1) \ (gphType.NumSubTypes / 2)) + 1))
                Next i
            End If
        End If

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        'jam 2024/08/17 Bug fix related to GET parameters
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

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95 - 50;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 50;" & vbCr

        If Session("bCompare") Then
            sJscript2 = sJscript2 & "myWidth = myWidth * 0.49;" & vbCr
            sJscript2 = sJscript2 & "myHeight = myHeight * 0.49;" & vbCr
        End If

        If Index > 3 Or Index = 0 Then
            Dim tmalemax As Single, tfemalemax As Single, TVal As Single
            tmalemax = 0
            Dim k As Integer = 0
            iMed = 0
            iEnd = 0

            If Session("bCompare") = False Then
                If Index = 0 Then
                    iMed = 3
                    iEnd = 6
                ElseIf Index < 4 Then
                    iMed = 1
                    iEnd = 2
                Else
                    iMed = gphType.NumSubTypes / 2
                    iEnd = gphType.NumSubTypes
                End If

                For l% = iNCohorts To 0 Step -1
                    TVal = 0
                    For k = 1 To iMed
                        TVal = TVal + Session("EC_TempDataBase")(l%, k)
                    Next
                    If TVal > tmalemax Then
                        tmalemax = TVal
                    End If
                Next l%
                tmalemax = Math.Round(tmalemax, 2)

                tfemalemax = 0
                For l% = iNCohorts To 0 Step -1
                    TVal = 0
                    For k = iMed + 1 To iEnd
                        TVal = TVal + Session("EC_TempDataBase")(l%, k)
                    Next
                    If TVal > tfemalemax Then
                        tfemalemax = TVal
                    End If
                Next l%
            Else
                If Index = 0 Then
                    iMed = 3
                    iEnd = 6
                ElseIf Index < 4 Then
                    iMed = 1
                    iEnd = 2
                Else
                    iMed = gphType.NumSubTypes / 2
                    iEnd = gphType.NumSubTypes
                End If

                For l% = iNCohorts To 0 Step -1
                    TVal = 0
                    For k = 1 To iMed
                        TVal = TVal + Session("EC_TempDataBase_Comp")(l%, k, gphIdx)
                    Next
                    If TVal > tmalemax Then
                        tmalemax = TVal
                    End If
                Next l%
                tmalemax = Math.Round(tmalemax, 2)

                tfemalemax = 0
                For l% = iNCohorts To 0 Step -1
                    TVal = 0
                    For k = iMed + 1 To iEnd
                        TVal = TVal + Session("EC_TempDataBase_Comp")(l%, k, gphIdx)
                    Next
                    If TVal > tfemalemax Then
                        tfemalemax = TVal
                    End If
                Next l%
            End If

            sJscript2 = sJscript2 & "var point = chart.series[0].data[1], text = chart.renderer.text(" & vbCr
            'cdv 2021/01/14 Flip Labels for Gender
            If bMaleFirst Then
                'sJscript2 = sJscript2 & "'Males Max (" & CStr(Math.Round(tmalemax, 0)) & ")', " & vbCr
                sJscript2 = sJscript2 & "'Males', " & vbCr 'dsb 2026/06/23 Changed label to Males
            Else
                'sJscript2 = sJscript2 & "'Females Max (" & CStr(Math.Round(tfemalemax, 0)) & ")', " & vbCr
                sJscript2 = sJscript2 & "'Females', " & vbCr 'dsb 2026/06/23 Changed label to Females
            End If
            If Session("bCompare") = False Then
                sJscript2 = sJscript2 & "2.25 * chart.plotLeft + 10," & vbCr
                sJscript2 = sJscript2 & "chart.plotTop - 10"
            Else
                sJscript2 = sJscript2 & "chart.plotLeft + 10," & vbCr 'dsb 2026/06/23 Changed label position to be above chart
                sJscript2 = sJscript2 & "chart.plotTop + chart.plotHeight + 15"
            End If

            'jam 2025/12/06 Increasing fonts
            'sJscript2 = sJscript2 & ").attr({" & vbCr
            'sJscript2 = sJscript2 & "zIndex: 5" & vbCr
            'sJscript2 = sJscript2 & "}).add();" & vbCr


            sJscript2 = sJscript2 & ").attr({" & vbCr
            sJscript2 = sJscript2 & "zIndex: 5" & vbCr
            sJscript2 = sJscript2 & "})" & vbCr

            sJscript2 = sJscript2 & ".css({" & vbCr
            'jrs 2026/04/23
            If Not Session("bCompare") Then
                sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
            Else
                sJscript2 = sJscript2 & "fontSize: '12px'," & vbCr
            End If
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "})" & vbCr
            sJscript2 = sJscript2 & ".add();" & vbCr




            sJscript2 = sJscript2 & "var point2 = chart.series[" & iMed.ToString & "].data[1], text2 = chart.renderer.text(" & vbCr
            If bMaleFirst Then
                'sJscript2 = sJscript2 & "'Females Max (" & CStr(Math.Round(tfemalemax, 0)) & ")'," & vbCr
                sJscript2 = sJscript2 & "'Females'," & vbCr 'dsb 2026/06/23 Changed label to Females
            Else
                'sJscript2 = sJscript2 & "'Males Max (" & CStr(Math.Round(tmalemax, 0)) & ")'," & vbCr
                sJscript2 = sJscript2 & "'Males'," & vbCr 'dsb 2026/06/23 Changed label to Males
            End If
            If Session("bCompare") = False Then
                sJscript2 = sJscript2 & "myWidth - 200," & vbCr
                sJscript2 = sJscript2 & "chart.plotTop - 10"
            Else
                sJscript2 = sJscript2 & "myWidth - 80," & vbCr 'dsb 2026/06/23 Changed label position to be above chart
                sJscript2 = sJscript2 & "chart.plotTop + chart.plotHeight + 15"
            End If
            'jam 2025/12/06
            'sJscript2 = sJscript2 & ").attr({" & vbCr
            'sJscript2 = sJscript2 & "zIndex: 5" & vbCr
            'sJscript2 = sJscript2 & "}).add();" & vbCr

            sJscript2 = sJscript2 & ").attr({" & vbCr
            sJscript2 = sJscript2 & "zIndex: 5" & vbCr
            sJscript2 = sJscript2 & "})" & vbCr

            sJscript2 = sJscript2 & ".css({" & vbCr
            'jrs 2026/04/23
            If Not Session("bCompare") Then
                sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
            Else
                sJscript2 = sJscript2 & "fontSize: '12px'," & vbCr
            End If

            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "})" & vbCr
            sJscript2 = sJscript2 & ".add();" & vbCr


        End If

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr

        PackJScritpBuilderContainter = sJscript2
    End Function

    'jrs 2012/08/17
    Private Sub mnuCompare_Click()

        'jam 2024/08/31
        If Session("bCompare") = False Then
            wmMain.Groups("Change Display").ShowMenuBarItem = False
            wmMain.Groups("Time").ShowMenuBarItem = False
            wmMain.Groups("mnuGroups").ShowMenuBarItem = False
        End If



        'jrs 2016/05/20 mnuAllTypes_Click()
        'Clear Region combos
        cmbReg1.Items.Clear()
        cmbReg2.Items.Clear()
        cmbReg3.Items.Clear()
        cmbReg4.Items.Clear()

        If Session("EdCohort_Grouping") = True Then
            For l = 1 To GCount
                cmbReg1.Items.Add(New ListItem(GroupNames(l)))
                cmbReg2.Items.Add(New ListItem(GroupNames(l)))
                cmbReg3.Items.Add(New ListItem(GroupNames(l)))
                cmbReg4.Items.Add(New ListItem(GroupNames(l)))
            Next l

            cmbReg1.SelectedValue = Session("EdCohort_regionname")
            cmbReg2.SelectedValue = Session("EdCohort_regionname")
            cmbReg3.SelectedValue = Session("EdCohort_regionname")
            cmbReg4.SelectedValue = Session("EdCohort_regionname")

        Else
            For l = 1 To NR
                cmbReg1.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg2.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg3.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg4.Items.Add(New ListItem(GRPS(1, l)))
            Next l

            cmbReg1.SelectedIndex = cntrylist.SelectedIndex
            cmbReg2.SelectedIndex = cntrylist.SelectedIndex
            cmbReg3.SelectedIndex = cntrylist.SelectedIndex
            cmbReg4.SelectedIndex = cntrylist.SelectedIndex

        End If

        'cdv 2021/05/19 Adjust all additional years to be mod 5
        While Int(cmbYr2.Text) Mod 5 <> 0
            cmbYr2.SelectedIndex = cmbYr2.SelectedIndex - 1
            cmbYr3.SelectedIndex = cmbYr3.SelectedIndex - 1
            cmbYr4.SelectedIndex = cmbYr4.SelectedIndex - 1
        End While

        'Turn on bCompare flag
        Session("bCompare") = True
        'to Show comparison graphs (4) and hide single graph
        'Show Comparisson panel with 4 sets of region, year textbox and 1 refresh button
        pnlCompare.Visible = True
        cmdRefreshComp_Click(Me, New EventArgs)
    End Sub

    'jrs 2012/08/17
    Protected Sub cmdRefreshComp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRefreshComp.Click
        ReDim Session("EC_TempPopDatabase_Comp")(NCohorts, 2, 4)
        'dsb 2026/06/23 Changed to fit all mortality subtypes and both genders
        'ReDim Session("EC_TempDatabase_Comp")(NCohorts, 14, 4) 'jrs 2016/05/20 changed from 8 to 14 to fit 7 subtipes and 2 genders
        Dim iCompSubTypeCount As Integer = 14
        If Session("EdCohort_GraphType") = 7 Then
            iCompSubTypeCount = AllMortSubTypes.NumSubTypes
        End If
        ReDim Session("EC_TempDatabase_Comp")(NCohorts, iCompSubTypeCount, 4)
        ReDim Session("EC_Reg")(4)
        ReDim Session("EC_Yr")(4)

        Session("CaseNames")(0) = cmbSce1.SelectedValue
        Session("CaseNames")(1) = cmbSce2.SelectedValue
        Session("CaseNames")(2) = cmbSce3.SelectedValue
        Session("CaseNames")(3) = cmbSce4.SelectedValue
        'JAM 2020-06-10
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Select Case cmbSce1.SelectedIndex
                Case 0
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.mdb"
                Case 1
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                Case 2
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                Case Else
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".mdb"
            End Select
            Select Case cmbSce2.SelectedIndex
                Case 0
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.mdb"
                Case 1
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                Case 2
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                Case Else
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".mdb"
            End Select
            Select Case cmbSce3.SelectedIndex
                Case 0
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.mdb"
                Case 1
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                Case 2
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                Case Else
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".mdb"
            End Select
            Select Case cmbSce4.SelectedIndex
                Case 0
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.mdb"
                Case 1
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                Case 2
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                Case Else
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".mdb"
            End Select

        Else
            Select Case cmbSce1.SelectedIndex
                Case 0
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.db"
                Case 1
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.db"
                Case 2
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.db"
                Case Else
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".db"
            End Select
            Select Case cmbSce2.SelectedIndex
                Case 0
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.db"
                Case 1
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.db"
                Case 2
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.db"
                Case Else
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".db"
            End Select
            Select Case cmbSce3.SelectedIndex
                Case 0
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.db"
                Case 1
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.db"
                Case 2
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.db"
                Case Else
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".db"
            End Select
            Select Case cmbSce4.SelectedIndex
                Case 0
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.db"
                Case 1
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.db"
                Case 2
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.db"
                Case Else
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".db"
            End Select

        End If
        'jrs 2024/04/24 it was using the wrong flag
        If Session("EdCohort_Grouping") = True Then
            If Session("PopCohort_regionname") <> cmbReg1.SelectedValue Then
                Session("GroupTot") = Nothing
            End If
        End If

        If Session("mnuFixCteScTime") Then
            If Session("EdCohort_Grouping") = False Then
                Session("EdCohort_tempregion") = cmbReg1.SelectedIndex + 1
            Else
                Session("EdCohort_regionname") = cmbReg1.SelectedValue
            End If
            SetUpMaxsTime()
        End If

        Dim i As Integer

        'jrs 2016/05/19
        Select Case Session("EdCohort_GraphType")
            Case 0
                For i = 1 To 4
                    CompositeGraph_Draw_Compare(i)
                Next
            Case 1, 2, 3
                For i = 1 To 4
                    SeparateGraph_Draw_compare(i, 4, 1, 2, 3, 4)
                Next
            Case 4
                For i = 1 To 4
                    CompositeGraph_Draw_All_compare(CommDis, i, 1) 'jrs 2023/08/25 added , 1
                Next
            Case 5
                For i = 1 To 4
                    CompositeGraph_Draw_All_compare(NonCommDis, i, 2) 'jrs 2023/08/25 added , 2
                Next
            Case 6
                For i = 1 To 4
                    CompositeGraph_Draw_All_compare(Injuries, i, 3) 'jrs 2023/08/25 added , 3
                Next
            Case 7 'dsb 2026/06/23 Added All Mortality subtypes
                For i = 1 To 4
                    CompositeGraph_Draw_All_compare(AllMortSubTypes, i, 7)
                Next
        End Select

        'jrs 2024/04/22
        vSQLiteCon.Close()
        If Not Session("SQLiteDB") Is Nothing Then
            vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";"
        Else
            vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPop.db;"
        End If

        'jam 2025/05/31 Changing connection mode
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()


        'jam 2025/05/31 Changing connection mode
        vSQLiteCon = OpenJournalSQLite(vConnString)



        Session("EC_Reg")(1) = cmbReg1.SelectedValue
        Session("EC_Reg")(2) = cmbReg2.SelectedValue
        Session("EC_Reg")(3) = cmbReg3.SelectedValue
        Session("EC_Reg")(4) = cmbReg4.SelectedValue

        Session("EC_Yr")(1) = cmbYr1.SelectedValue
        Session("EC_Yr")(2) = cmbYr2.SelectedValue
        Session("EC_Yr")(3) = cmbYr3.SelectedValue
        Session("EC_Yr")(4) = cmbYr4.SelectedValue

        PackJScritpBuilder()
    End Sub

    'jrs 2014/01/28
    Private Sub mnuFixCtSc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/03/06
        If objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") Then '"checkbox.gif" Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtSc") = False
        Else
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Session("mnuFixCtSc") = True
        End If

        If Session("mnuFixCtSc") = True Then
            'jam 2021/03/13
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCteScTime") = False
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtScMan") = False

            SetUpMaxs()
        End If
        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
        Else
            Graph_Draw(Session("EdCohort_GraphType"))
        End If
    End Sub

    'jrs 2014/01/29
    Private Sub SetUpMaxs()

        Dim Margin As Single
        Margin = 1.2   '-> 20%

        'All Types of Mortality
        '======================
        Dim sql As String
        Dim rs As DataTable
        Dim MaxScale As Single = 0

        'JAM 2020-06-11
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            sql = "SELECT MAX(MortPerMalesComDis + MortPerFeMalesComDis + " &
                "MortPerMalesNonComDis + MortPerFeMalesNonComDis +  " &
                "MortPerMalesInjuries + MortPerFeMalesInjuries) " &
                "FROM MorDetBaseCase WHERE NCohorts > 0"
            If Session("bShow100") = False Then
                sql = sql & " AND NCohorts < " & CStr(NCohorts)
            End If

            SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
            MaxScale = rs.Rows(0).Item(0)

            Session("MaxScale")(0) = Amin(MaxScale, 1100)

            'Communicable Diseases
            '=====================
            sql = "SELECT MAX(MortPerMalesComDis + MortPerFeMalesComDis) " &
                "FROM MorDetBaseCase WHERE NCohorts > 0"
            If Session("bShow100") = False Then
                sql = sql & " AND NCohorts < " & CStr(NCohorts)
            End If

            Session("MaxScale")(1) = Amin(MaxScale, 1100)
            Session("MaxScale")(4) = Amin(MaxScale / 2, 1100)

            'Non Communicable Diseases
            '=========================
            sql = "SELECT MAX(MortPerMalesNonComDis + MortPerFeMalesNonComDis) " &
                "FROM MorDetBaseCase WHERE NCohorts > 0"
            If Session("bShow100") = False Then
                sql = sql & " AND NCohorts < " & CStr(NCohorts)
            End If

            SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
            MaxScale = rs.Rows(0).Item(0)

            Session("MaxScale")(2) = Amin(MaxScale, 1100)
            Session("MaxScale")(5) = Amin(MaxScale / 2, 1100)

            'Injuries 
            '=========================
            sql = "SELECT MAX(MortPerMalesInjuries + MortPerFeMalesInjuries) " &
                "FROM MorDetBaseCase WHERE NCohorts > 0"
            If Session("bShow100") = False Then
                sql = sql & " AND NCohorts < " & CStr(NCohorts)
            End If

            SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
            MaxScale = rs.Rows(0).Item(0)
        Else
            'jrs 2024/12/05 added space before FROM producing error, and fixed the name of table missing _ after Det
            sql = "SELECT MAX(MortPerMalesComDis + MortPerFeMalesComDis + " &
                "MortPerMalesNonComDis + MortPerFeMalesNonComDis +  " &
                "MortPerMalesInjuries + MortPerFeMalesInjuries) as morMax " &
                "FROM Mor_Det_Base_Case WHERE CohortId > 0"
            If Session("bShow100") = False Then
                sql = sql & " AND CohortId < " & CStr(NCohorts)
            End If

            'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
            'MaxScale = rs.Rows(0).Item(0)

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = sql
            vSQLiteReader = vSQLiteCommand.ExecuteReader()



            While (vSQLiteReader.Read())
                MaxScale = vSQLiteReader("morMax")

            End While



            Session("MaxScale")(0) = Amin(MaxScale, 1100)

            'Communicable Diseases
            '=====================
            sql = "SELECT MAX(MortPerMalesComDis + MortPerFeMalesComDis) " &
                "FROM Mor_Det_Base_Case WHERE CohortId > 0"
            If Session("bShow100") = False Then
                sql = sql & " AND CohortId < " & CStr(NCohorts)
            End If

            Session("MaxScale")(1) = Amin(MaxScale, 1100)
            Session("MaxScale")(4) = Amin(MaxScale / 2, 1100)

            'Non Communicable Diseases
            '=========================
            'jrs 2024/12/05 added needed space before FROM
            sql = "SELECT MAX(MortPerMalesNonComDis + MortPerFeMalesNonComDis) as MortMax " &
                "FROM Mor_Det_Base_Case WHERE CohortId > 0"
            If Session("bShow100") = False Then
                sql = sql & " AND CohortId < " & CStr(NCohorts)
            End If

            'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
            'MaxScale = rs.Rows(0).Item(0)

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = sql
            vSQLiteReader = vSQLiteCommand.ExecuteReader()



            While (vSQLiteReader.Read())
                MaxScale = vSQLiteReader("MortMax")

            End While



            Session("MaxScale")(2) = Amin(MaxScale, 1100)
            Session("MaxScale")(5) = Amin(MaxScale / 2, 1100)

            'Injuries 
            '=========================
            sql = "SELECT MAX(MortPerMalesInjuries + MortPerFeMalesInjuries) as MortMax2 " &
                "FROM Mor_Det_Base_Case WHERE CohortId > 0"
            If Session("bShow100") = False Then
                sql = sql & " AND CohortId < " & CStr(NCohorts)
            End If

            'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
            'MaxScale = rs.Rows(0).Item(0)

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = sql
            vSQLiteReader = vSQLiteCommand.ExecuteReader()



            While (vSQLiteReader.Read())
                MaxScale = vSQLiteReader("MortMax2")

            End While



        End If
        Session("MaxScale")(3) = Amin(MaxScale, 1100)
        Session("MaxScale")(6) = Amin(MaxScale / 2, 1100)
        Session("MaxScale")(7) = Session("MaxScale")(0) 'dsb 2026/06/23 Added All Mortality subtypes

    End Sub

    'jrs 2014/02/14
    Private Sub mnuFixCteScTime_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/03/06
        If objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") Then '"checkbox.gif" Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCteScTime") = False
        Else
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Session("mnuFixCteScTime") = True
        End If

        If Session("mnuFixCteScTime") = True Then
            'jam 2021/03/06
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtSc") = False
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtScMan") = False

            SetUpMaxsTime()
        End If
        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
        Else
            Graph_Draw(Session("EdCohort_GraphType"))
        End If
    End Sub

    'jrs 2014/01/28
    'Maxs across Time, specific per Region
    Private Sub SetUpMaxsTime()

        Dim Margin As Single
        Margin = 1.2   '-> 20%

        'All Types of Mortality
        '======================
        Dim sql As String
        Dim rs As DataTable
        Dim MaxScale As Single = 0
        Dim MaxScale1 As Single = 0
        Dim MaxScale2 As Single = 0

        'JAM 2020-06-11
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            If Session("EdCohort_Grouping") = False Then
                sql = "SELECT MAX(MortPerMalesComDis + MortPerFeMalesComDis + " &
                "MortPerMalesNonComDis + MortPerFeMalesNonComDis +  " &
                "MortPerMalesInjuries + MortPerFeMalesInjuries) " &
                "FROM MorDetBaseCase WHERE Region "
                sql = sql & "= " & Session("EdCohort_tempregion")
                sql = sql & " AND NCohorts > 0"
                If Session("bShow100") = False Then
                    sql = sql & " AND NCohorts < " & CStr(NCohorts)  'jrs 2014/01/29 21
                End If
                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0)
            Else
                sql = "SELECT SUM((MortPerMalesComDis + MortPerMalesNonComDis +  MortPerMalesInjuries) * PopulationMales) / " &
                "SUM(PopulationMales), " &
                "SUM((MortPerFeMalesComDis + MortPerFeMalesNonComDis + MortPerFeMalesInjuries) * PopulationFemales) / " &
                "SUM(PopulationFemales) " &
                "FROM MorDetBaseCase as m, PopBaseCase as p WHERE m.Region = p.Region AND m.NCohorts = p.NCohorts " &
                "AND m.Years = p.Years AND m.Years = " & CStr(Session("FirstYearSce")) 'jrs 2008/06/05 changed to FirstYearSce
                sql = sql & " AND m.Region IN (" & Session("sRegs") & ")"
                If Session("bShow100") = False Then
                    sql = sql & " AND m.NCohorts = " & CStr(NCohorts - 1) 'jrs 2014/01/29 20
                Else
                    sql = sql & " AND m.NCohorts = " & CStr(NCohorts) 'jrs 2014/01/29 21
                End If

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)
            End If

            Session("MaxScale")(0) = Amin(MaxScale * Margin, 1100)

            'Communicable Diseases
            '=====================
            If Session("EdCohort_Grouping") = False Then
                sql = "SELECT MAX(MortPerMalesComDis + MortPerFeMalesComDis) " &
                "FROM MorDetBaseCase WHERE Region "
                sql = sql & "= " & Session("EdCohort_tempregion")
                sql = sql & " AND NCohorts > 0"
                If Session("bShow100") = False Then
                    sql = sql & " AND NCohorts < " & CStr(NCohorts)  'jrs 2014/01/29 21
                End If
                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0)
            Else
                sql = "SELECT SUM((MortPerMalesComDis) * PopulationMales) / " &
                "SUM(PopulationMales), " &
                "SUM((MortPerFeMalesComDis) * PopulationFemales) / " &
                "SUM(PopulationFemales) " &
                "FROM MorDetBaseCase as m, PopBaseCase as p WHERE m.Region = p.Region AND m.NCohorts = p.NCohorts " &
                "AND m.Years = p.Years AND m.Years = " & CStr(Session("FirstYearSce")) 'jrs 2008/06/05 changed to FirstYearSce
                sql = sql & " AND m.Region IN (" & Session("sRegs") & ")"
                If Session("bShow100") = False Then
                    sql = sql & " AND m.NCohorts = " & CStr(NCohorts - 1) 'jrs 2014/01/29 20
                Else
                    sql = sql & " AND m.NCohorts = " & CStr(NCohorts) 'jrs 2014/01/29 21
                End If

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)
            End If

            Session("MaxScale")(1) = Amin(MaxScale / 2 * Margin, 1100)
            Session("MaxScale")(4) = Amin(MaxScale * Margin, 1100)

            'Non Communicable Diseases
            '=========================
            If Session("EdCohort_Grouping") = False Then
                sql = "SELECT MAX(MortPerMalesNonComDis + MortPerFeMalesNonComDis) " &
                "FROM MorDetBaseCase WHERE Region "
                sql = sql & "= " & Session("EdCohort_tempregion")
                sql = sql & " AND NCohorts > 0"
                If Session("bShow100") = False Then
                    sql = sql & " AND NCohorts < " & CStr(NCohorts)  'jrs 2014/01/29 21
                End If
                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0)
            Else
                sql = "SELECT SUM((MortPerMalesNonComDis) * PopulationMales) / " &
                "SUM(PopulationMales), " &
                "SUM((MortPerFeMalesNonComDis) * PopulationFemales) / " &
                "SUM(PopulationFemales) " &
                "FROM MorDetBaseCase as m, PopBaseCase as p WHERE m.Region = p.Region AND m.NCohorts = p.NCohorts " &
                "AND m.Years = p.Years AND m.Years = " & CStr(Session("FirstYearSce")) 'jrs 2008/06/05 changed to FirstYearSce
                sql = sql & " AND m.Region IN (" & Session("sRegs") & ")"
                If Session("bShow100") = False Then
                    sql = sql & " AND m.NCohorts = " & CStr(NCohorts - 1) 'jrs 2014/01/29 20
                Else
                    sql = sql & " AND m.NCohorts = " & CStr(NCohorts) 'jrs 2014/01/29 21
                End If

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)
            End If

            Session("MaxScale")(2) = Amin(MaxScale / 2 * Margin, 1100)
            Session("MaxScale")(5) = Amin(MaxScale * Margin, 1100)

            'Injuries 
            '=========================
            If Session("EdCohort_Grouping") = False Then
                sql = "SELECT MAX(MortPerMalesInjuries + MortPerFeMalesInjuries) " &
                "FROM MorDetBaseCase WHERE Region "
                sql = sql & "= " & Session("EdCohort_tempregion")
                sql = sql & " AND NCohorts > 0"
                If Session("bShow100") = False Then
                    sql = sql & " AND NCohorts < " & CStr(NCohorts)  'jrs 2014/01/29 21
                End If
                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0)
            Else
                sql = "SELECT SUM((MortPerMalesInjuries) * PopulationMales) / " &
                "SUM(PopulationMales), " &
                "SUM((MortPerFeMalesInjuries) * PopulationFemales) / " &
                "SUM(PopulationFemales) " &
                "FROM MorDetBaseCase as m, PopBaseCase as p WHERE m.Region = p.Region AND m.NCohorts = p.NCohorts " &
                "AND m.Years = p.Years AND m.Years = " & CStr(Session("FirstYearSce")) 'jrs 2008/06/05 changed to FirstYearSce
                sql = sql & " AND m.Region IN (" & Session("sRegs") & ")"
                If Session("bShow100") = False Then
                    sql = sql & " AND m.NCohorts = " & CStr(NCohorts - 1) 'jrs 2014/01/29 20
                Else
                    sql = sql & " AND m.NCohorts = " & CStr(NCohorts) 'jrs 2014/01/29 21
                End If

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)
            End If


        Else
            If Session("EdCohort_Grouping") = False Then
                sql = "SELECT MAX(MortPerMalesComDis + MortPerFeMalesComDis + " &
                "MortPerMalesNonComDis + MortPerFeMalesNonComDis +  " &
                "MortPerMalesInjuries + MortPerFeMalesInjuries)  As MaxYear1 " &
                "FROM Mor_Det_Base_Case WHERE RegionId "
                sql = sql & "= " & Session("EdCohort_tempregion")
                sql = sql & " AND CohortId > 0"
                If Session("bShow100") = False Then
                    sql = sql & " AND CohortId < " & CStr(NCohorts)  'jrs 2014/01/29 21
                End If
                'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                'MaxScale = rs.Rows(0).Item(0)

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'FindLastYear = 0
                While (vSQLiteReader.Read())
                    MaxScale = vSQLiteReader("MaxYear1")

                End While



            Else
                'jrs 2024/12/05 added missing space before FROM
                sql = "SELECT SUM((MortPerMalesComDis + MortPerMalesNonComDis +  MortPerMalesInjuries) * PopulationMales) / " &
                "SUM(PopulationMales) as MaxYear1, " &
                "SUM((MortPerFeMalesComDis + MortPerFeMalesNonComDis + MortPerFeMalesInjuries) * PopulationFemales) / " &
                "SUM(PopulationFemales) as MaxYear2 " &
                "FROM Mor_Det_Base_Case as m, Pop_Base_Case as p WHERE m.RegionId = p.RegionId AND m.CohortId = p.CohortId " &
                "AND m.TimeId = p.TimeId AND m.TimeId = " & CStr(Session("FirstYearSce")) 'jrs 2008/06/05 changed to FirstYearSce
                sql = sql & " AND m.RegionId IN (" & Session("sRegs") & ")"
                If Session("bShow100") = False Then
                    sql = sql & " AND m.CohortId = " & CStr(NCohorts - 1) 'jrs 2014/01/29 20
                Else
                    sql = sql & " AND m.CohortId = " & CStr(NCohorts) 'jrs 2014/01/29 21
                End If

                'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                'MaxScale = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'FindLastYear = 0
                While (vSQLiteReader.Read())
                    MaxScale2 = vSQLiteReader("MaxYear2")
                    MaxScale1 = vSQLiteReader("MaxYear1")
                End While

                MaxScale = MaxScale1 + MaxScale2

            End If

            Session("MaxScale")(0) = Amin(MaxScale * Margin, 1100)

            'Communicable Diseases
            '=====================
            If Session("EdCohort_Grouping") = False Then
                sql = "SELECT MAX(MortPerMalesComDis + MortPerFeMalesComDis) as MaxYear " &
                "FROM Mor_Det_Base_Case WHERE RegionId "
                sql = sql & "= " & Session("EdCohort_tempregion")
                sql = sql & " AND CohortId > 0"
                If Session("bShow100") = False Then
                    sql = sql & " AND CohortId < " & CStr(NCohorts)  'jrs 2014/01/29 21
                End If
                'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                'MaxScale = rs.Rows(0).Item(0)

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'FindLastYear = 0
                While (vSQLiteReader.Read())
                    MaxScale = vSQLiteReader("MaxYear")

                End While
            Else
                'jrs 2024/12/05 added missing space before FROM, and change p.Region to p.RegionId
                sql = "SELECT SUM((MortPerMalesComDis) * PopulationMales) / " &
                "SUM(PopulationMales) as MaxYear1, " &
                "SUM((MortPerFeMalesComDis) * PopulationFemales) / " &
                "SUM(PopulationFemales) as MaxYear2 " &
                "FROM Mor_Det_Base_Case as m, Pop_Base_Case as p WHERE m.RegionId = p.RegionId AND m.CohortId = p.CohortId " &
                "AND m.TimeId = p.TimeId AND m.TimeId = " & CStr(Session("FirstYearSce")) 'jrs 2008/06/05 changed to FirstYearSce
                sql = sql & " AND m.RegionId IN (" & Session("sRegs") & ")"
                If Session("bShow100") = False Then
                    sql = sql & " AND m.CohortId = " & CStr(NCohorts - 1) 'jrs 2014/01/29 20
                Else
                    sql = sql & " AND m.CohortId = " & CStr(NCohorts) 'jrs 2014/01/29 21
                End If

                'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                'MaxScale = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)



                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'FindLastYear = 0
                While (vSQLiteReader.Read())
                    MaxScale = vSQLiteReader("MaxYear2")
                    MaxScale = vSQLiteReader("MaxYear1")
                End While

                MaxScale = MaxScale1 + MaxScale2

            End If

            Session("MaxScale")(1) = Amin(MaxScale / 2 * Margin, 1100)
            Session("MaxScale")(4) = Amin(MaxScale * Margin, 1100)

            'Non Communicable Diseases
            '=========================
            If Session("EdCohort_Grouping") = False Then
                'jrs 2024/12/06 added needed space before FROM
                sql = "SELECT MAX(MortPerMalesNonComDis + MortPerFeMalesNonComDis) as MaxYear " &
                "FROM Mor_Det_Base_Case WHERE RegionId "
                sql = sql & "= " & Session("EdCohort_tempregion")
                sql = sql & " AND CohortId > 0"
                If Session("bShow100") = False Then
                    sql = sql & " AND CohortId < " & CStr(NCohorts)  'jrs 2014/01/29 21
                End If
                'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                'MaxScale = rs.Rows(0).Item(0)

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'FindLastYear = 0
                While (vSQLiteReader.Read())
                    MaxScale = vSQLiteReader("MaxYear")

                End While



            Else
                'jrs 2024/12/05 fixing syntax
                sql = "SELECT SUM((MortPerMalesNonComDis) * PopulationMales) / " &
                "SUM(PopulationMales) as MaxYear1, " &
                "SUM((MortPerFeMalesNonComDis) * PopulationFemales) / " &
                "SUM(PopulationFemales) as MaxYear2 " &
                "FROM Mor_Det_Base_Case as m, Pop_Base_Case as p WHERE m.RegionId = p.RegionId AND m.CohortId = p.CohortId " &
                "AND m.TimeId = p.TimeId AND m.TimeId = " & CStr(Session("FirstYearSce")) 'jrs 2008/06/05 changed to FirstYearSce
                sql = sql & " AND m.RegionId IN (" & Session("sRegs") & ")"
                If Session("bShow100") = False Then
                    sql = sql & " AND m.CohortId = " & CStr(NCohorts - 1) 'jrs 2014/01/29 20
                Else
                    sql = sql & " AND m.CohortId = " & CStr(NCohorts) 'jrs 2014/01/29 21
                End If

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                MaxScale = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)



                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'FindLastYear = 0
                While (vSQLiteReader.Read())
                    MaxScale = vSQLiteReader("MaxYear1")
                    MaxScale = vSQLiteReader("MaxYear2")
                End While

                MaxScale = MaxScale1 + MaxScale2

            End If

            Session("MaxScale")(2) = Amin(MaxScale / 2 * Margin, 1100)
            Session("MaxScale")(5) = Amin(MaxScale * Margin, 1100)

            'Injuries 
            '=========================
            If Session("EdCohort_Grouping") = False Then
                'jrs 2024/12/05 added needed space before FROM
                sql = "SELECT MAX(MortPerMalesInjuries + MortPerFeMalesInjuries) as MaxYear " &
                "FROM Mor_Det_Base_Case WHERE RegionId "
                sql = sql & "= " & Session("EdCohort_tempregion")
                sql = sql & " AND CohortId > 0"
                If Session("bShow100") = False Then
                    sql = sql & " AND CohortId < " & CStr(NCohorts)  'jrs 2014/01/29 21
                End If
                'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                'MaxScale = rs.Rows(0).Item(0)


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'FindLastYear = 0
                While (vSQLiteReader.Read())
                    MaxScale = vSQLiteReader("MaxYear")
                    'MaxScale = vSQLiteReader("MaxYear2")
                End While

                'MaxScale = MaxScale1 + MaxScale2


            Else
                'jrs 2024/12/05 added missing space before FROM
                sql = "SELECT SUM((MortPerMalesInjuries) * PopulationMales) / " &
                "SUM(PopulationMales) as MaxYear1, " &
                "SUM((MortPerFeMalesInjuries) * PopulationFemales) / " &
                "SUM(PopulationFemales) as MaxYear2 " &
                "FROM Mor_Det_Base_Case as m, Pop_Base_Case as p WHERE m.RegionId = p.RegionId AND m.CohortId = p.CohortId " &
                "AND m.TimeId = p.TimeId AND m.TimeId = " & CStr(Session("FirstYearSce")) 'jrs 2008/06/05 changed to FirstYearSce
                sql = sql & " AND m.RegionId IN (" & Session("sRegs") & ")"
                If Session("bShow100") = False Then
                    sql = sql & " AND m.CohortId = " & CStr(NCohorts - 1) 'jrs 2014/01/29 20
                Else
                    sql = sql & " AND m.CohortId = " & CStr(NCohorts) 'jrs 2014/01/29 21
                End If

                'SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                'MaxScale = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)



                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'FindLastYear = 0
                While (vSQLiteReader.Read())
                    'jrs 2024/12/05 changed to MaxYear1
                    MaxScale = vSQLiteReader("MaxYear1")
                    MaxScale = vSQLiteReader("MaxYear2")
                End While

                MaxScale = MaxScale1 + MaxScale2

            End If


        End If

        Session("MaxScale")(3) = Amin(MaxScale / 2 * Margin, 1100)
        Session("MaxScale")(6) = Amin(MaxScale * Margin, 1100)
        Session("MaxScale")(7) = Session("MaxScale")(0) 'dsb 2026/06/23 Added All Mortality subtypes


    End Sub

    'jrs 2014/02/14
    Private Sub mnuFixCtScMan_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/03/06
        If objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") Then '"checkbox.gif" Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtScMan") = False
        Else
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Session("mnuFixCtScMan") = True
        End If


        If Session("mnuFixCtScMan") = True Then
            'jam 2021/03/06
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtSc") = False
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCteScTime") = False

            txtAllM.Text = Session("MaxScale")(0)
            txtCD.Text = Session("MaxScale")(4)
            txtNCD.Text = Session("MaxScale")(5)
            txtInj.Text = Session("MaxScale")(6)
            lblJS.Visible = False
            pnlMaxScale.Visible = True
            pnlCompare.Visible = False
        Else
            PackJScritpBuilder()
        End If
    End Sub

    Protected Sub cmdOKMaxScale_Click(sender As Object, e As System.EventArgs) Handles cmdOKMaxScale.Click
        Dim i As Integer
        'jrs 2024/01/24 added if statements
        If txtAllM.Text > "" And IsNumeric(txtAllM.Text) Then
            Session("MaxScale")(0) = txtAllM.Text
        End If
        If txtAllM.Text > "" And IsNumeric(txtAllM.Text) Then 'dsb 2026/06/23 Added All Mortality subtypes
            Session("MaxScale")(7) = Session("MaxScale")(0)
        End If
        'jrs 2024/01/24 added if statements
        If txtCD.Text > "" And IsNumeric(txtCD.Text) Then
            Session("MaxScale")(1) = txtCD.Text / 2
            Session("MaxScale")(4) = txtCD.Text
        End If
        'jrs 2024/01/24 added if statements
        If txtNCD.Text > "" And IsNumeric(txtNCD.Text) Then
            Session("MaxScale")(2) = txtNCD.Text / 2
            Session("MaxScale")(5) = txtNCD.Text
        End If
        'jrs 2024/01/24 added if statements
        If txtInj.Text > "" And IsNumeric(txtInj.Text) Then
            Session("MaxScale")(3) = txtInj.Text / 2
            Session("MaxScale")(6) = txtInj.Text
        End If


        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
            pnlCompare.Visible = True
        Else
            Graph_Draw(Session("PopCohort_GraphType"))
        End If

        lblJS.Visible = True
        pnlMaxScale.Visible = False
    End Sub

    Private Function FindLastCohortWithData(ByRef Vals As Object, ByVal lastCol As Integer, ByVal gphIdx As Integer) As Integer
        'jrs 2013/03/29
        FindLastCohortWithData = 21

        'jam 2026/01/23 MAxCohort
        MaxCohortGroup = NCohorts


        If gphIdx > 4 Then
            Exit Function
        End If
        'jrs 2014/02/21 If NCohorts > 21 Then
        'Find Last cohort with data
        Dim k As Integer
        'jrs 2014/02/21
        If gphIdx > 0 Then
            For l% = NCohorts To 1 Step -1
                For k = 1 To lastCol
                    'jrs 2014/10/15
                    If Not IsDBNull(Vals(l%, k, gphIdx)) Then
                        If Vals(l%, k, gphIdx) > 0.00005 Then
                            FindLastCohortWithData = l
                            Exit For
                        End If
                    End If
                Next k
                If k < lastCol + 1 Then
                    Exit For
                End If
            Next l%


            'jam 2026/01/23 The graph with the max Cohort in compare Option
            If FindLastCohortWithData > MaxCohortGroup Then
                MaxCohortGroup = FindLastCohortWithData
            End If

        Else
            For l% = NCohorts To 1 Step -1
                For k = 1 To lastCol
                    'jrs 2014/10/15
                    If Not IsDBNull(Vals(l%, k)) Then
                        If Vals(l%, k) > 0.00005 Then
                            FindLastCohortWithData = l
                            Exit For
                        End If
                    End If
                Next k
                If k < lastCol + 1 Then
                    Exit For
                End If
            Next l%
        End If

        'jam 2026/01/23 With Compare option, return the maximum category with data
        'jrs 2014/02/21 End If
        'Return FindLastCohortWithData

        If Not Session("bCompare") Then
            Return FindLastCohortWithData
        Else
            If FindLastCohortWithData > MaxCohortGroup Then
                Return FindLastCohortWithData
            Else
                Return MaxCohortGroup
            End If

        End If



    End Function

    Private Function PlotColToJSSeries(ByVal i As Integer, ByRef Vals As Object, ByVal bShow100 As Boolean, ByVal Idx As Integer,
                            ByVal gphType As MortType, ByVal CompIdx As Integer, ByVal iNCohortsWD As Integer,
                            ByVal bAllZero As Boolean) As String
        Dim iNCohorts As Integer
        If bShow100 Then
            iNCohorts = iNCohortsWD 'jrs2013/08/16 NCohorts
        Else
            iNCohorts = iNCohortsWD - 1 'jrs2013/08/16NCohorts - 1
        End If

        Dim bLeftPyramid As Boolean = False, colIdx As Integer, iLowCohort As Integer
        colIdx = i
        iLowCohort = 0
        If Idx = 0 Then
            'cdv 2021/01/14 ****START**** Change data order to match male first
            If i < 4 And bMaleFirst Then
                bLeftPyramid = True
            ElseIf i >= 4 And bMaleFirst = False Then
                bLeftPyramid = True
            End If

        ElseIf Idx > 3 Then
            If i < gphType.NumSubTypes / 2 + 1 And bMaleFirst Then
                bLeftPyramid = True
            ElseIf i >= gphType.NumSubTypes / 2 + 1 And bMaleFirst = False Then
                bLeftPyramid = True
            End If

        End If

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i, Idx, gphType) & """, color:""" & getColor(i, Idx, gphType)
        If ((Idx = 0 Or Idx > 3) And Not bLeftPyramid And bMaleFirst) Or ((Idx = 0 Or Idx > 3) And bLeftPyramid And bMaleFirst = False) Then
            sJscript = sJscript & """, showInLegend: false, data:[" & vbCr
        Else
            sJscript = sJscript & """, data:[" & vbCr
        End If
        'cdv 2021/01/14 ****END****
        Dim j As Integer
        Dim bFirst As Boolean = True
        If Session("bCompare") And CompIdx > 0 And CompIdx < 5 And (Session("AboutParent") = "frm_EdnCohoDisp.aspx" Or Session("AboutParent") = "frm_MortCohorts.aspx" Or Session("AboutParent") = "frm_PopCohortDisp.aspx") Then 'jrs 2014/02/12
            For j = iLowCohort To iNCohorts
                'jrs 2014/10/15
                If Not IsDBNull(Vals(j, colIdx, CompIdx)) Then
                    'jrs 2013/03/29 health pyramid, for comm dis. is messed up when aids is 0 in higher ages, it shows up big
                    'it might only happen in stacked pyramids
                    If Vals(j, colIdx, CompIdx) > 0 Then
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & "," & vbCr
                        End If
                        If Not bLeftPyramid Then
                            sJscript = sJscript & CStr(Vals(j, colIdx, CompIdx))
                        Else
                            sJscript = sJscript & CStr(-1 * Vals(j, colIdx, CompIdx))
                        End If
                    End If
                End If
            Next j
        Else
            For j = iLowCohort To iNCohorts
                'jrs 2013/01/03
                If Not IsDBNull(Vals(j, colIdx)) Then
                    'jrs 2014/02/21 added condition to check if all are 0
                    'jrs 2013/03/29 
                    If Vals(j, colIdx) > 0 Or (j = iNCohorts And bAllZero) Then 'jrs 2013/08/19 added or j=iNCohorts, problems when Males are all 0s
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & "," & vbCr
                        End If
                        If Not bLeftPyramid Then
                            sJscript = sJscript & CStr(Vals(j, colIdx))
                        Else
                            sJscript = sJscript & CStr(-1 * Vals(j, colIdx))
                        End If
                    End If
                Else
                    'jrs 2013/03/29
                    'sJscript = sJscript & "<point name = """ & Ages(j) & _
                    '    """ y=""0""/>" & vbCr
                End If
            Next j
        End If

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Private Function getSeriesName(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Legend As String
        If Session("AboutParent") = "frm_PopCohortDisp.aspx" Then
            If ii = 1 And idx <> 2 Then
                Legend = "Males"
            Else
                Legend = "Females"
            End If
        ElseIf Session("AboutParent") = "frm_CohoInfDisp.aspx" Then 'jrs 2012/06/22
            If idx <> 2 Then
                If ii = 1 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            Else
                Legend = ""
            End If
        ElseIf Session("AboutParent") = "frm_EdnCohoDisp.aspx" Then
            If idx = 0 Or idx > 4 Then
                Select Case ii
                    Case 1, 6
                        Legend = "Children"
                    Case 2, 7
                        Legend = "No education or incomplete primary education"
                    Case 3, 8
                        Legend = "Completed primary education only"
                    Case 4, 9
                        Legend = "Completed through secondary education"
                    Case 5, 10
                        Legend = "Completed through tertiary education"
                End Select
                If ii > 5 Then
                    Legend = Legend & "(F)"
                End If
            ElseIf idx < 5 Then
                If ii < 6 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            End If
        ElseIf Session("AboutParent") = "frm_MortCohorts.aspx" Then 'Health
            If idx = 0 Then
                Select Case ii
                    Case 1
                        Legend = "Communicable Disease"
                    Case 2
                        Legend = "Non Communicable Disease"
                    Case 3
                        Legend = "Injury"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            Else
                If ii < gphType.NumSubTypes / 2 + 1 Then
                    Legend = gphType.getSubTypeName(ii * 2 - 1)
                Else
                    Legend = gphType.getSubTypeName((ii - gphType.NumSubTypes / 2) * 2 - 1) & "(F)"
                End If
            End If
        ElseIf Session("AboutParent") = "frm_MorbCohorts.aspx" Then 'jrs 2013/02/22
            If idx = 0 Then
                Select Case ii
                    Case 1
                        Legend = "Communicable Disease"
                    Case 2
                        Legend = "Non Communicable Disease"
                    Case 3
                        Legend = "Injury"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            End If
        Else 'WVS
            Dim VN As String, D1 As String
            '           Get legend components
            '           ---------------------

            VN = LTrim(RTrim(Session("VARN")(0, ii)))
            'jrs 10/27/04 Flexible Dimension Names for Graphs
            D1 = LTrim(RTrim(Session("FDNG1")(ii)))

            '          Compose legend and assign legend
            '           --------------------------------

            Legend = VN
            If Len(D1) > 0 Then
                Legend = Legend & "(" & D1 & ")"
            End If
        End If
        getSeriesName = Legend
    End Function

    Private Function getSeriesNameLine(ByVal ii As Integer) As String
        Dim Legend As String
        If Session("AboutParent") = "frm_MortCohorts.aspx" Then 'Health
            Legend = Session("grdCohortDet").Columns(ii).Title
        Else 'J-curve
            Legend = Session("mLegends")(1, ii) & ", " & Session("mLegends")(2, ii) & ", " &
                Session("mLegends")(3, ii) & ", " & Session("mLegends")(4, ii) & ", " & Session("mLegends")(5, ii)
        End If
        getSeriesNameLine = Legend
    End Function

    Private Function getColor(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Color As String
        If Session("AboutParent") = "frm_PopCohortDisp.aspx" Then
            If ii = 1 And idx <> 2 Then
                Color = "Blue"
            Else
                Color = "Lime"
            End If
        ElseIf Session("AboutParent") = "frm_CohoInfDisp.aspx" Then 'jrs 2012/06/25
            If ii = 1 Then
                Color = "Blue"
            Else
                Color = "Lime"
            End If
        ElseIf Session("AboutParent") = "frm_EdnCohoDisp.aspx" Then
            If idx = 0 Or idx > 4 Then
                Select Case ii
                    Case 1, 6
                        Color = "Gray"
                    Case 2, 7
                        Color = "Red"
                    Case 3, 8
                        Color = "Lime"
                    Case 4, 9
                        Color = "RGB(255, 215, 0)"
                    Case 5, 10
                        Color = "Blue"
                End Select
            ElseIf idx < 5 Then
                If ii < 6 Then
                    'Males
                    Color = "Blue"
                Else
                    Color = "Lime"
                End If
            End If
        ElseIf Session("AboutParent") = "frm_MortCohorts.aspx" Then  'Health
            If idx = 0 Then
                Select Case ii
                    Case 1, 4
                        Color = "RGB(255, 215, 0)"
                    Case 2, 5
                        Color = "Blue"
                    Case 3, 6
                        Color = "Red"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    'Males
                    Color = "Blue"
                Else
                    Color = "Lime"
                End If
            Else
                ' Return:
                '     iRed    contains the red component
                '     iGreen  the green component
                '     iBlue   the blue component
                '
                Dim lColor As Long, iRed As Integer, iGreen As Integer, iBlue As Integer
                If ii < gphType.NumSubTypes / 2 + 1 Then
                    lColor = gphType.getSubTypeColor(ii * 2 - 1)      'work long
                Else
                    lColor = gphType.getSubTypeColor((ii - gphType.NumSubTypes / 2) * 2 - 1)      'work long
                End If
                iRed = lColor Mod &H100  'get red component
                lColor = lColor \ &H100  'divide
                iGreen = lColor Mod &H100 'get green component
                lColor = lColor \ &H100  'divide
                iBlue = lColor Mod &H100 'get blue component

                Color = "RGB(" & iRed.ToString & "," & iGreen.ToString & "," & iBlue.ToString & ")"
            End If
        ElseIf Session("AboutParent") = "frm_MorbCohorts.aspx" Then  'jrs 2013/02/22 Morbidity
            If idx = 0 Then
                Select Case ii
                    Case 1, 4
                        Color = "RGB(255, 215, 0)"
                    Case 2, 5
                        Color = "Blue"
                    Case 3, 6
                        Color = "Red"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    'Males
                    Color = "Blue"
                Else
                    Color = "Lime"
                End If
            End If
        Else
            Color = "Lime"
        End If
        getColor = Color
    End Function

    Private Function getColorLine(ByVal i As Integer) As String
        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = " color:""Lime"""
                Case 2
                    sColor = " color:""Blue"""
                Case 3
                    sColor = " color:""Red"""
                Case 5 'blue violet
                    sColor = " color:""RGB(138,43,226)"""
                Case 6 'aqua 
                    sColor = " color:""RGB(0,255,255)"""
                Case 7 'deep pink
                    sColor = " color:""RGB(255,20,147)"""
                Case 8 'medium spring green
                    sColor = " color:""RGB(0,250,154)"""
            End Select
        End If
        getColorLine = sColor
    End Function


    Private Function PlotColToJSSeriesLine(ByVal i As Integer, ByRef Vals As Object, ByVal bShow100 As Boolean) As String
        Dim iNCohorts As Integer = 0

        Dim Ages() As String

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesNameLine(i) & """, " & getColorLine(i) & ", data: [" & vbCr

        Dim j As Integer
        Dim bfirst As Boolean = True
        For j = 0 To Session("grdCohortDet").TotalRows - 1
            If Session("grdCohortDet")(i, j) > "" Then
                If bfirst Then
                    bfirst = False
                Else
                    sJscript = sJscript & ", "
                End If
                sJscript = sJscript & CStr(Vals(i, j))
            End If
        Next j


        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeriesLine = sJscript
    End Function

    'dsb 2026/06/23 Changed image legend to dynamic legend for all subtypes
    Private Function BuildDynamicMortalityLegend(ByVal Index As Integer, ByVal gphType As MortType) As String

        Dim html As String = ""
        Dim i As Integer

        html &= "<div class='mort-dynamic-legend'>"

        If Index = 0 Then
            html &= BuildDynamicMortalityLegendItem("Communicable Disease", getColor(1, Index, gphType))
            html &= BuildDynamicMortalityLegendItem("Non Communicable Disease", getColor(2, Index, gphType))
            html &= BuildDynamicMortalityLegendItem("Injury", getColor(3, Index, gphType))
        ElseIf Index >= 4 AndAlso Not gphType Is Nothing Then
            For i = 1 To gphType.NumSubTypes / 2
                html &= BuildDynamicMortalityLegendItem(gphType.getSubTypeName((i * 2) - 1), getColor(i, Index, gphType))
            Next
        End If
        html &= "</div>"
        Return html

    End Function

    Private Function BuildDynamicMortalityLegendItem(ByVal legendText As String, ByVal legendColor As String) As String

        Return "<span class='mort-dynamic-legend-item'>" &
     "<span class='mort-dynamic-legend-color' style='background-color:" & legendColor & ";'></span>" &
     "<span>" & Server.HtmlEncode(legendText) & "</span>" &
     "</span>"

    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/02/04
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function MortPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        MortPrepareForFriendlyURL = tmp
    End Function
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2022/07/17 Convert to Death flag              
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Private Sub mnuCnvtoDeaths(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    If Session("cnvtoDeaths") Then
    '        Session("cnvtoDeaths") = False
    '    Else
    '        Session("cnvtoDeaths") = True
    '    End If

    'End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2022/07/17 Turn On Death flag              
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub mnuCnvtoDeathsOn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'Dim objSender As Coalesys.WebMenu.WebMenu
        'objSender = eventSender

        'objSender.Groups("Change Display").Items("Using Deaths").Group.Items(Session("ON1")).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        'objSender.Groups("Change Display").Items("Using Deaths").Group.Items(Session("OFF1")).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"



        'objSender.Groups("Using Deaths").Items("ON").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        'objSender.Groups("Using Deaths").Items("OFF").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"


        Session("cnvtoDeaths") = True
        Graph_Draw((Session("EdCohort_GraphType")))
        PackJScritpBuilder()
        'Graph_Draw(Session("EdCohort_GraphType"))
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2022/07/17 Turn off Death flag              
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuCnvtoDeathsOff_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'Dim objSender As Coalesys.WebMenu.WebMenu
        'objSender = eventSender

        'objSender.Groups("Change Display").Items("Using Deaths").Group.Items(Session("OFF1")).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        'objSender.Groups("Change Display").Items("Using Deaths").Group.Items(Session("ON1")).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Session("cnvtoDeaths") = False
        Graph_Draw((Session("EdCohort_GraphType")))
        PackJScritpBuilder()

        'Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2022/07/17 Convert to Death Procedure            
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub ConvertToDeaths(ByVal iNCohorts As Integer, ByVal iTypes As Integer)
        Dim g As Integer
        'Reload to target
        For j = 0 To iNCohorts
            'Mortality
            For T = 1 To 2 * iTypes
                If T < iTypes + 1 Then
                    g = 1
                Else
                    g = 2
                End If
                Session("EC_TempDatabase")(j, T) = Session("EC_TempDatabase")(j, T) * Session("EC_TempPopDatabase")(j, g) 'ThoUSAnd deaths
            Next T
        Next j
    End Sub


End Class
