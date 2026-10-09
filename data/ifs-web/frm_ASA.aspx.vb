Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Imports IFs.Data
'jam 2021/12/29
Imports Microsoft.AspNet.FriendlyUrls

'Namespace IFsWeb
Partial Class frm_ASA
    Inherits Page

    Dim iFileOld As Integer
        Dim bShow As Boolean

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        If Support.FormManager.LoadState Then
            Support.FormManager.StateRestore(Me)
            Support.FormManager.LoadState = False
        End If

        If Session("ASAGroups") Is Nothing Then
                Session("ASAGroups") = False
            End If

            'LOADING DATA BASE NAME
            Dim sql As String
            sql = "SELECT * FROM ASA"

        Dim tbl As DataTable
        'jam 2025/10/30 Unifying Database opening mode
        SetDataTablefromAccess(Session("PutFdrvRun2") & "\IFs.mdb", sql, "", tbl, "", True)
        Session("tblASA") = tbl
        sql = "SELECT * FROM ASA2"
        SetDataTablefromAccess(Session("PutFdrvRun2") & "\IFs.mdb", sql, "", tbl, "", True)
        Session("tblASA2") = tbl
        sql = "SELECT * FROM ASABASE"
        SetDataTablefromAccess(Session("PutFdrvRun2") & "\IFs.mdb", sql, "", tbl, "", True)
        Session("tblASABASE") = tbl
        sql = "SELECT * FROM ASABASE2"
        SetDataTablefromAccess(Session("PutFdrvRun2") & "\IFs.mdb", sql, "", tbl, "", True)
        Session("tblASABASE2") = tbl
        sql = "SELECT * FROM ASABASEPct"
        SetDataTablefromAccess(Session("PutFdrvRun2") & "\IFs.mdb", sql, "", tbl, "", True)
        Session("tblASABASEPct") = tbl
        sql = "SELECT * FROM ASABASEPct2"
        SetDataTablefromAccess(Session("PutFdrvRun2") & "\IFs.mdb", sql, "", tbl, "", True)
        Session("tblASABASEPct2") = tbl

        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        'jrs 2026/01/26 changed default to base
        Session("FileNum") = 1
        If Session("ASAGroups") = True Then
            LoadGroups()
        Else
            LoadRegions()
        End If

        LoadFiles()

        ' Check for Currency Convertion
        CurrencyCheck(GDPidx)

        '   Establish Year List Box
        'JRS 09/12/02
        Dim finam As String
        Dim VLN As Object
        Dim Number As Single
        If Session("mnuSQLite") = "True" Then
            finam = Session("PutFdrvRun") & "Working.run.db"
        Else
            finam = Session("PutFdrvRun") & "Working.run"
        End If
        'jrs 2005/02/14
        If Session("mnuSQLite") = "True" Then
            Dim working As IStorage
            Dim storageSvc As IStorageService = New StorageService(finam)
            working = storageSvc.Open()
            Session("VarVal") = working.LastYear
        Else
            Dim fnum As Integer
            fnum = FreeFile()
            FileOpen(fnum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared) 'jrs 2008/05/06 added openaccess.read, openshare.shared
            VLN = VarPoint(PosModYear)
            Session("record") = (VLN) * 4 - 3
            FileGet(fnum, Number, Session("record"))
            Session("varval") = Number
            FileClose(fnum)
        End If



        'JRS 2005/02/23
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            Session("VarSelCheck") = Nothing
        End If
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = Session("LastYearForecast") Then
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
            Else
                Session("LastYear") = LastYearHistory
            End If
        End If
        For i2 = Session("FirstYear") To Session("VarVal")
            Cmb_Year.Items.Add(New ListItem(LTrim(Str(i2))))
        Next i2

        'Display first elements
        If Cmb_Year.Items.Count > 0 Then
            Cmb_Year.SelectedIndex = 0 ' Display First Item
        End If
        ' goes into Procedure summary
        If cmb_Country.Items.Count > 0 Then
            cmb_Country.SelectedIndex = 0 ' Display First Item
        End If
    End Sub

    Private Sub LoadRegions()
        Dim EleLoop As Object
        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        cmb_Country.Items.Clear()

        For EleLoop = 1 To NR
            cmb_Country.Items.Add(New ListItem(GRPS(1, EleLoop)))
        Next EleLoop
    End Sub

    Private Sub LoadGroups()
        cmb_Country.Items.Clear()
        Dim valtbl As DataTable

        'jam 2025/10/30 Unifying Database opening mode
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "GroupName", valtbl, "", True)
        Dim i As Integer
        For i = 0 To valtbl.Rows.Count - 1
            cmb_Country.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
        Next i
    End Sub

    Private Sub LoadFiles()
        'JRS 11/24/01 DISPLAY
        Dim fs 'As New Scripting.FileSystemObject
        Dim BASE 'As Scripting.File
        Dim currRun 'As Scripting.File
        'Dim fRunFile As Folder
        fs = Server.CreateObject("Scripting.FileSystemObject")
        'Set fRunFile = fs.GetFolder(PutFdrvRun2$)
        'fRunFile.Files returns collection of files
        'jrs 2005/03/17
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = LastYearHistory Then
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run")
            End If
        Else
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run")
            End If
        End If
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboFiles.Items.Clear()
        Dim FilesSelected As Integer
        FilesSelected = 1
        Dim sWorkingFile As String
        sWorkingFile = VB.Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboFiles.Items.Add(New ListItem("0 - " & "Working File, based on " & sWorkingFile))
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboFiles.Items(cboFiles.SelectedIndex).Text = "0 - " & "Working File, based on " & sWorkingFile
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboFiles.Items.Add(New ListItem("1 - " & BASE.Name))
        FilesSelected = 1

        Dim sRunFile As String
        ' Returns filename with specified extension. If more than one *.ini
        ' file exists, the first file found is returned.
        If Session("mnuSQLite") = "True" Then
            sRunFile = Dir(Session("PutFdrvRun2") & "\*.run.db")
        Else
            sRunFile = Dir(Session("PutFdrvRun2") & "\*.run")
        End If

        Do While (sRunFile <> "")
            If UCase(sRunFile) <> UCase(BASE.Name) And UCase(sRunFile) <> UCase(sWorkingFile) And UCase(sRunFile) <> "WORKING.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") & "\" & sRunFile)
                'jrs 2022/04/08 added Or StandAloneMode
                If Session("IFsRunFiles").item(sRunFile).Visible Or StandAloneMode Then 'jrs 2005/06/27
                    cboFiles.Items.Add(New ListItem(Trim(Str(cboFiles.Items.Count)) & " - " & sRunFile))
                    FilesSelected = FilesSelected + 1
                    'JRS RUNFILES 5/11/02
                    ReDim Preserve Session("RunFiles")(FilesSelected)
                    Session("RunFiles")(FilesSelected) = Session("PutFdrvRun2") & "\" & sRunFile
                    'jrs 2005/02/25
                    If FilesSelected > UBound(Session("ShortSce")) Then
                        ReDim Preserve Session("ShortSce")(FilesSelected)
                    End If
                    Dim k As Integer
                    k = getLongSceIdx(sRunFile)
                    If k > -1 Then
                        Session("ShortSce")(FilesSelected) = ShortSce(k)
                    Else
                        'jrs 2008/05/13 Session("ShortSce")(FilesSelected) = Left(sRunFile, 8)
                        Session("ShortSce")(FilesSelected) = TruncateSceFileName(sRunFile)
                    End If
                End If
            End If
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            sRunFile = Dir()
        Loop
        'jrs 2026/01/26
        cboFiles.SelectedIndex = 1
    End Sub

    Private Sub CurrencyCheck(ByVal SEL As Integer)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   JRS 5-1-01
        '- - - - - - - - - - - - - - By Jose Solorzano - - - - - - - - - - - - - - -
        '- - - - - - - - - - - - - - RA Working for Barry Hughes - - - - - - - - - - - - - - -
        '- - - - - - - - - - - - - - 5-01-01 - - - - - - - - - - - - - - -
        'JRS 10/15/02 CURRENCY LABEL
        Dim MyPos As Long
        Dim s As String
        Dim s2 As String

        'Checks if the flag for this variable is ON
        Session("bCnvrtCurr") = VCurrency(SEL)
        'If the option is ON procede
        If Session("bCnvrtCurr") Then
            'If it returns information, load it to the variables
            If Session("CurrencyVal") <> 0 Then 'JRS 2005/12/14 If rsCurr.RecordCount > 0 Then
                'Updates the Currency Factor
                Session("CurrencyFct") = Session("CurrencyVal") 'JRS 2005/12/14 rsCurr.Fields("Currency Value")
                s2 = VUnits(SEL)
                MyPos = InStr(1, s2, "$", vbTextCompare)
                s = RTrim(Session("CurrencySym"))
            Else
                Session("CurrencyFct") = 1
                'JRS 10/15/02 CURRENCY LABEL
                'jrs 2012/08/15 lblUnits.Caption = lblUnits.Caption + " " + VUnits(SEL)
            End If
        Else
            Session("CurrencyFct") = 1
            'JRS 10/15/02 CURRENCY LABEL
            'jrs 2012/08/15 lblUnits.Caption = lblUnits.Caption + " " + VUnits(SEL)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'jam 2021/12/29
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim qtyPAram As Integer

        cswmWebMenu.UserData = "jsolorza::976739975"
        'jam 2021/12/29
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/12/29
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")



        'jam 2021/12/29
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

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_ASA.aspx")


        If othCountry <> "" Then
            vurl = FriendlyUrl.Href("~/frm_ASA", ASAPrepareForFriendlyURL(othCountry))

            Response.Redirect(vurl)

        Else

            vurl = FriendlyUrl.Href("~/frm_ASA")

        End If


        'jam 2021/12/29
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String

        'jam 2021/12/29
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




        cswmWebMenu.ScrollEnabled = True
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
        cswmGroup1.ID = "Group_fa05"
        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        'jam 2021/12/29
        ' cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"
        'jam 2022/01/03
        'cswmGroup1.OnMenuBarClick = "location.href='" & FriendlyUrl.Resolve(Session("DisplayParent")) & "';"
        cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl("~/" + Session("DisplayParent")) & "';"



        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "View"
        cswmGroup1.ID = "mnuView"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Normal"
        cswmItem.ID = "mnuViewNormal"
        If Session("mnuView") Is Nothing Then
            Session("mnuView") = "mnuViewNormal"
        End If
        If Session("mnuView") = "mnuViewNormal" Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") ' jam 2021/12/29"checkbox.gif"
            grdASA.DataSource = Session("tblASA")
            grdASA2.DataSource = Session("tblASA2")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") ' jam 2021/12/29 "blank.gif"
            grdASA.DataSource = Session("tblASABasePct")
            grdASA2.DataSource = Session("tblASABasePct2")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Change From Base Case"
        cswmItem.ID = "mnuBaseCase"
        If Session("mnuView") = "mnuBaseCase" Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/29 "checkbox.gif"
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/29 "blank.gif"
        End If
        If Session("FileNum") = 1 Then
            cswmItem.Enabled = False
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Switch"
        cswmGroup1.ID = "mnuSwitch"

        cswmItem = cswmGroup1.Items.Add()
        If Session("ASAGroups") Then
            cswmItem.Caption = "Use Country"
        Else
            cswmItem.Caption = "Use Groups"
        End If
        cswmItem.ID = "mnuGroups"

        If Not Page.IsPostBack Then
            cboFiles_SelectedIndexChanged(Me, New System.EventArgs)
        Else
            RefreshDisplay()
        End If

        'jrs 2013/09/04
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jam 2021/12/29
        'jrs 2011/05/06
        'If Not IsNothing(Request.QueryString("Country")) Then
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Session("vCountry"))) And Not Session("ASAGroups") Then
            'jam 2021/12/29
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> Session("vCountry") Or Not Page.IsPostBack Then
                'jam 2021/12/29
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = Session("vCountry")
                'jrs 2012/08/15 Session("ASAGroups") = False
                If Session("ASAGroups") = True Then
                    cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions()
                    Session("ASAGroups") = False
                End If
                'Validate
                Dim rs As DataTable

                'jam 2025/10/30 Unifying Database opening mode
                SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT NewRegion FROM [Country Translation] as c, " &
                Regionalization & " as r where c.Country = r.Country AND [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs, "", True)
                If rs.Rows.Count = 0 Then
                        Session("MMCountry") = ""
                        rs = Nothing
                    Else
                        Session("cNumber") = rs.Rows(0).Item("NewRegion")
                    End If
                End If
                'jam 2021/12/29
                'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
            ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Session("vCountry"))) And Session("ASAGroups") Then 'jam 2021/12/29
                'jam 2021/12/29
                'If cmb_Country.SelectedValue <> Request.QueryString("Group") Then
                If cmb_Country.SelectedValue <> Session("vCountry") Then
                    Session.Remove("cNumber")
                    Dim gIdx As Integer
                    'jam 2021/12/29
                    'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                    gIdx = gGetGroupIdx(Session("vCountry"), False, Me)
                    If gIdx > 0 Then
                        cmb_Country.SelectedIndex = gIdx - 1
                        Session("ASACountryIdx") = cmb_Country.SelectedIndex
                        'jam 2021/12/29
                        'Session("MMGroup") = Request.QueryString("Group")
                        Session("MMGroup") = Session("vCountry")
                    Else
                        Session.Remove("MMGroup")
                    End If
                    If Session("ASAGroups") = True Then
                        cmb_Country_SelectedIndexChanged(cmb_Country, New System.EventArgs)
                    Else
                        mnuGroups_Click(cswmWebMenu, New Coalesys.WebMenu.ServerItemClickEventArgs(cswmWebMenu.Groups("mnuSwitch").Items(0)))
                    End If
                End If
            End If
                PackJScritpBuilder()

            'KIA 63008 ADD THE FOLLOWING IF BLOCK
            If Not Session("cNumber") Is Nothing Then
                cmb_Country.SelectedIndex = CInt(Session("cNumber")) - 1
                Session.Remove("cNumber")
                'RefreshDisplay()
                Session("ASACountryIdx") = cmb_Country.SelectedIndex
                cmb_Country_SelectedIndexChanged(Me, New System.EventArgs)
            End If
        End Sub

        Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim objSender As Coalesys.WebMenu.WebMenu
            objSender = sender

            Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
            objEA = e

            'Choose the correct function
            Select Case objEA.Item.Caption
                Case "Normal"
                    mnuViewNormal_Click()
                Case "Percent Change From Base Case"
                    MnuBaseCase_Click()

                Case "Use Groups"
                    mnuGroups_Click(sender, e)
                Case "Use Country"
                    mnuGroups_Click(sender, e)
            End Select

        End Sub

        Public Sub mnuViewNormal_Click()
            'jrs 2006/07/21
            If Session("mnuView") = "mnuViewNormal" Then Exit Sub

            cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/29 "blank.gif"
            cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/29 "checkbox.gif"

            Session("mnuView") = "mnuViewNormal"

            grdASA.DataSource = Session("tblASA")
            grdASA2.DataSource = Session("tblASA2")
            grdASA.DataBind()
            grdASA2.DataBind()

        End Sub

        Public Sub MnuBaseCase_Click()
            'jrs 2006/07/21
            If Session("mnuView") = "mnuBaseCase" Then Exit Sub

            cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/29 "blank.gif"
            cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/29 "checkbox.gif"

            Session("mnuView") = "mnuBaseCase"

            grdASA.DataSource = Session("tblASABasePct")
            grdASA2.DataSource = Session("tblASABasePct2")
            grdASA.DataBind()
            grdASA2.DataBind()
        End Sub

        Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
            'jam 2021/12/29
            Dim Vpath As String
            Dim Vext As String
            Dim vfriendlyurl As String
            Dim vurl As String


            If cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups" Then
                cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Country"
                lbl_countries.Text = "Groups"
                LoadGroups()
                Session("ASAGroups") = True
            Else
                cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"
                lbl_countries.Text = "Countries or Regions"
                LoadRegions()
                Session("ASAGroups") = False
            End If
            cmb_Country.SelectedIndex = 0 ' Display First Item
            Session("ASACountryIdx") = 0
            cboFiles_SelectedIndexChanged(Me, New System.EventArgs)
            'jrs 2011/05/06
            If Session("ASAGroups") = True Then
                Session.Remove("MMCountry")
                If Session("MMGroup") = Nothing Then



                    'jam 2021/12/29
                    ' Support.FormManager.Show(Me, "frm_ASA.aspx?Group=" & PrepareForURL(cmb_Country.SelectedValue))

                    Vpath = Request.GetFriendlyUrlFileVirtualPath

                    Vext = Request.GetFriendlyUrlFileExtension

                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_ASA.aspx")


                    vurl = FriendlyUrl.Href("~/frm_ASA", ASAPrepareForFriendlyURL(cmb_Country.SelectedValue))

                    Response.Redirect(vurl)



                Else
                    'jam 2021/12/29
                    'Support.FormManager.Show(Me, "frm_ASA.aspx?Group=" & PrepareForURL(Session("MMGroup")))


                    Vpath = Request.GetFriendlyUrlFileVirtualPath

                    Vext = Request.GetFriendlyUrlFileExtension

                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_ASA.aspx")


                    vurl = FriendlyUrl.Href("~/frm_ASA", ASAPrepareForFriendlyURL(Session("MMGroup")))

                    Response.Redirect(vurl)
                End If
            Else
                'jam 2021/12/29
                'Support.FormManager.Show(Me, "frm_ASA.aspx?Country=AF")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_ASA.aspx")


                vurl = FriendlyUrl.Href("~/frm_ASA", "AF")

                Response.Redirect(vurl)

            End If
        End Sub

        Protected Sub cboFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiles.SelectedIndexChanged
            If Cmb_Year.SelectedIndex = -1 Then Exit Sub
            'jrs 2021/04/05
            Session("MyFileforDisplay") = Nothing
            ClearValues()
            If Not IsNothing(Session("ASACountryIdx")) Then
                cmb_Country.SelectedIndex = Session("ASACountryIdx")
            End If
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
            PercentCalculation()
            RoundValues()

            grdASA.DataBind()
            grdASA2.DataBind()
            cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").Enabled = Not (Session("FileNum") = 1)
        End Sub

        Private Sub ClearValues()
            Dim tbl As DataTable
            Dim i As Integer, j As Integer

            tbl = Session("tblASA")
            For i = 0 To tbl.Rows.Count - 1
                If Not IsDBNull(tbl.Rows(i).Item(1)) Then
                    For j = 1 To tbl.Columns.Count - 1
                        tbl.Rows(i).Item(j) = 0
                    Next
                End If
            Next
            Session("tblASA") = tbl

            tbl = Session("tblASABASE")
            For i = 0 To tbl.Rows.Count - 1
                If Not IsDBNull(tbl.Rows(i).Item(1)) Then
                    For j = 1 To tbl.Columns.Count - 1
                        tbl.Rows(i).Item(j) = 0
                    Next
                End If
            Next
            Session("tblASABASE") = tbl

            tbl = Session("tblASA2")
            For i = 0 To tbl.Rows.Count - 1
                If Not IsDBNull(tbl.Rows(i).Item(1)) Then
                    For j = 1 To tbl.Columns.Count - 1
                        tbl.Rows(i).Item(j) = 0
                    Next
                End If
            Next
            Session("tblASA2") = tbl

            tbl = Session("tblASABASE2")
            For i = 0 To tbl.Rows.Count - 1
                If Not IsDBNull(tbl.Rows(i).Item(1)) Then
                    For j = 1 To tbl.Columns.Count - 1
                        tbl.Rows(i).Item(j) = 0
                    Next
                End If
            Next
            Session("tblASABASE2") = tbl
        End Sub

        'Subroutine to Load the Values
        'into ASA Values table
        Private Sub LoadValues(ByVal reg As Integer, ByVal yr As Integer)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            'Load Row Raw
            LoadValuesVar(reg, yr, "ASARAW", "Raw annual values")
            LoadValuesVar(reg, yr, "ASARAWDMAT", "Cumulative change in raw values, percent")

            'Load Row PerBillionGDP
            LoadValuesVar(reg, yr, "ASAGDP", "Impact/intensity per million GDP")
            LoadValuesVar(reg, yr, "ASAGDPDMAT", "Cumulative dematerialization of impact per unit of GDP, percent")
            LoadValuesVar(reg, yr, "ASAGDPGRRB", "Raw values associated with GDP growth (defined as gross rebound effect)")

            'Load Row PerMilPop
            LoadValuesVar(reg, yr, "ASAPOP", "Impact/intensity per thousand population")
            LoadValuesVar(reg, yr, "ASAPOPDMAT", "Cumulative dematerialization of impact per unit of population, percent")
            LoadValuesVar(reg, yr, "ASAPOPGRRB", "Raw values associated with population growth (defined as gross rebound effect)")

            'Load Row PerMilLab
            LoadValuesVar(reg, yr, "ASALAB", "Impact/intensity per thousand labor")
            LoadValuesVar(reg, yr, "ASALABDMAT", "Cumulative dematerialization of impact per unit of labor, percent")
            LoadValuesVar(reg, yr, "ASALABGRRB", "Raw values associated with labor growth (defined as gross rebound effect)")

            'JRS 1/25/03
            '******************* Grid 2 *******************'
            'Load Cell GDP, Values
            LoadValuesVarCell(reg, yr, "GDP", "GDP, billion", "ASAValues", 1, 1)
            'Load Cell GDP, CumPerChg
            LoadValuesVarCell(reg, yr, "ASAGDPCUMCHG", "GDP, billion", "CumPerChg", 1, 1)
            'Load Cell POP, Values
            LoadValuesVarCell(reg, yr, "POP", "Population, million", "ASAValues", 1, 1)
            'Load Cell POP, CumPerChg
            LoadValuesVarCell(reg, yr, "ASAPOPCUMCHG", "Population, million", "CumPerChg", 1, 1)
            'Load Cell LAB, Values
            LoadValuesVarCell(reg, yr, "LAB", "Labor Force, million", "ASAValues", 1, 1)
            'Load Cell LAB, CumPerChg
            LoadValuesVarCell(reg, yr, "ASALABCUMCHG", "Labor Force, million", "CumPerChg", 1, 1)
            '******************* Grid 2 *******************'

        End Sub

        'Subroutine to Load the Value into Cell
        Private Sub LoadValuesVar(ByRef reg As Short, ByRef yr As Short, ByRef sVar As String, ByRef srowName As String)
            Dim FilenameBase As String
            Dim VarValBase As Single
            Dim VarValBases(1) As Single
            Dim rsASABase As DataTable
            rsASABase = Session("tblASABase")

            Dim VarIdx As Integer
            Dim VarVal As Single
            Dim VarVals(1) As Single
            Dim iSizeDim2 As Integer, iSizeDim3 As Integer 'jrs 2006/05/17 Dim3
            Dim rsASA As DataTable
            VarIdx = IfsVars.Item(sVar).Index
            iSizeDim2 = MaxVal(VDim2(VarIdx)) - 1
            If VDim3(VarIdx) > 0 Then
                iSizeDim3 = MaxVal(VDim3(VarIdx)) - 1
            Else
                iSizeDim3 = 0
            End If
            rsASA = Session("tblASA")
            'JRS 1/15/02 TRANSPOSE rsASA.MoveFirst
            Dim rrASA As Integer = 0
            'JRS 1/29/03
            Do While UCase(IIf(IsDBNull(rsASA.Rows(rrASA).Item("Category")), "", rsASA.Rows(rrASA).Item("Category"))) <> UCase(srowName)
                rrASA = rrASA + 1
            Loop

            VarVal = 0

            'JRS 12/10/02 BASE PCT
            If Session("mnuSQLite") = "True" Then
                FilenameBase = Session("PutFdrvRun") + Session("ActiveBase") + ".db"
            End If

            'For every Sector
            Dim dim2idx As Integer, dim3idx As Integer
            Dim idx As Integer, idx3 As Integer

            For idx = 0 To rsASA.Columns.Count - 2 'iSizeDim2 - 1
                'JRS 1/15/03
                If idx < iSizeDim2 Then
                    dim2idx = idx
                Else
                    dim2idx = 0
                End If

                For idx3 = 0 To iSizeDim3
                    'Read value
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", idx + 1, "", idx3 + 1, "",
                    cboFiles.SelectedIndex, False, False, Session("ASAGroups"), 1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"),
                    True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
                    Else
                        gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", idx + 1, "", idx3 + 1, "",
                        cboFiles.SelectedIndex, True, True, Session("ASAGroups"), 1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"),
                        True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    End If
                    VarVal = VarVals(1)
                    'JRS 12/10/02 BASE PCT
                    If cboFiles.SelectedIndex <> 1 Then
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", idx + 1, "", idx3 + 1, FilenameBase,
                        1, False, False, Session("ASAGroups"), 1, VarValBases, 0, 1, yr, yr, yr - Session("FirstYear"),
                        True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                        Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay1"), Session("df" + sVar + "1"))
                        Else
                            gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", idx + 1, "", idx3 + 1, FilenameBase,
                            1, True, True, Session("ASAGroups"), 1, VarValBases, 0, 1, yr, yr, yr - Session("FirstYear"),
                            True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                            Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        End If
                    Else
                            VarValBases(1) = VarVals(1)
                    End If
                    'JRS 12/10/02 BASE PCT
                    VarValBase = VarValBases(1)

                    'JRS 12/10/02 BASE PCT
                    'JRS 1/15/02 TRANSPOSE rsASABase.Fields(scolName) = rsASABase.Fields(scolName) + VarValBase
                    If IsDBNull(rsASABase.Rows(rrASA).Item(idx + 1)) Then
                        rsASABase.Rows(rrASA).Item(idx + 1) = 0
                    End If
                    rsASABase.Rows(rrASA).Item(idx + 1) = rsASABase.Rows(rrASA).Item(idx + 1) + VarValBase
                    'JRS 1/15/02 TRANSPOSE rsASABase.MoveNext

                    'JRS 1/15/02 TRANSPOSE rsASA.Fields(scolName) = rsASA.Fields(scolName) + VarVal
                    If IsDBNull(rsASA.Rows(rrASA).Item(idx + 1)) Then
                        rsASA.Rows(rrASA).Item(idx + 1) = 0
                    End If
                    rsASA.Rows(rrASA).Item(idx + 1) = rsASA.Rows(rrASA).Item(idx + 1) + VarVal
                    'JRS 1/15/02 TRANSPOSE rsASA.MoveNext
                    'JRS 1/15/02 TRANSPOSE If rsASA.EOF Then Exit For
                Next idx3
            Next idx
            Session("tblASA") = rsASA
            Session("tblASABase") = rsASABase
        End Sub

        'JRS 1/25/03
        'Subroutine to Load the Value into Cell
        Private Sub LoadValuesVarCell(ByVal reg As Integer, ByVal yr As Integer, ByVal sVar As String, _
            ByVal srowName As String, ByVal scolName As String, ByVal Factor As Single, ByVal iSizeDim2 As Integer)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            Dim FilenameBase As String
            Dim VarIdx As Integer
            Dim VarVal As Single
            Dim VarVals(1) As Single
            Dim VarValBase As Single
            Dim VarValBases(1) As Single
            VarVal = 0

            VarIdx = IfsVars.Item(sVar).Index
            If Session("mnuSQLite") = "True" Then
                FilenameBase = Session("PutFdrvRun") + Session("ActiveBase") + ".db"
            End If
            'jrs 2006/05/18 Dim3
            Dim iSizeDim3 As Integer
            If VDim3(VarIdx) > 0 Then
                iSizeDim3 = MAXCOM(VDim3(VarIdx)) - 1
            Else
                iSizeDim3 = 1
            End If

            'For every Sector
            Dim dim2idx As Integer, dim3idx As Integer
            For dim2idx = 0 To iSizeDim2 - 1
                For dim3idx = 1 To iSizeDim3
                    'Read value
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", dim2idx + 1, "", dim3idx, "",
                        cboFiles.SelectedIndex, False, False, Session("ASAGroups"), 1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"),
                        True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                        Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
                    Else
                        gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", dim2idx + 1, "", dim3idx, "",
                        cboFiles.SelectedIndex, True, True, Session("ASAGroups"), 1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"),
                        True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                        Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    End If
                    VarVal = VarVal + VarVals(1)
                    If cboFiles.SelectedIndex <> 1 Then

                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", dim2idx + 1, "", dim3idx, FilenameBase,
                        1, False, False, Session("ASAGroups"), 1, VarValBases, 0, 1, yr, yr, yr - Session("FirstYear"),
                        True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                        Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay1"), Session("df" + sVar + "1"))
                        Else
                            gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", dim2idx + 1, "", dim3idx, FilenameBase,
                            1, True, True, Session("ASAGroups"), 1, VarValBases, 0, 1, yr, yr, yr - Session("FirstYear"),
                            True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                            Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        End If
                    Else
                            VarValBases(1) = VarVals(1)
                    End If
                    VarValBase = VarValBase + VarValBases(1)
                Next dim3idx
            Next dim2idx

            'Update Values Table
            Dim sUpdate As String
            'If factor < 0 And RegName$(reg) = "United States of America" Then Stop
            Dim i As Integer
            Dim tbl As DataTable = Session("tblASA2")
            For i = 0 To tbl.Rows.Count - 1
                If tbl.Rows(i).Item("Category").ToString = srowName Then
                    tbl.Rows(i).Item(scolName) = tbl.Rows(i).Item(scolName) + Math.Round(VarVal * Factor, 4)
                    Exit For
                End If
            Next
            Session("tblASA2") = tbl
            tbl = Session("tblASABase2")

            For i = 0 To tbl.Rows.Count - 1
                If tbl.Rows(i).Item("Category").ToString = srowName Then
                    tbl.Rows(i).Item(scolName) = tbl.Rows(i).Item(scolName) + Math.Round(VarVal * Factor, 4)
                    Exit For
                End If
            Next
            Session("tblASABase2") = tbl
        End Sub

        'JRS 12/10/02 BASE PCT
        Private Sub PercentCalculation()

            'dbIfsASA.Execute (sqlPct)

            'TO INCLUDE BLANC LINES
            Dim rsASA As DataTable
            Dim rsASABase As DataTable
            Dim rsASAPct As DataTable
            Dim iField As Integer
            rsASA = Session("tblASA")
            rsASABase = Session("tblASABase")
            rsASAPct = Session("tblASABasePct")
            Dim rrASA As Integer = 0

            Do While Not rsASA.Rows.Count = rrASA
                rsASAPct.Rows(rrASA).Item(0) = rsASA.Rows(rrASA).Item(0)
                For iField = 1 To rsASA.Columns.Count - 1
                    If IsDBNull(rsASABase.Rows(rrASA).Item(iField)) Then
                        rsASAPct.Rows(rrASA).Item(iField) = DBNull.Value
                    Else
                        If rsASABase.Rows(rrASA).Item(iField) = 0 Then
                            rsASAPct.Rows(rrASA).Item(iField) = DBNull.Value
                        Else
                            rsASAPct.Rows(rrASA).Item(iField) = (rsASA.Rows(rrASA).Item(iField) - rsASABase.Rows(rrASA).Item(iField)) / rsASABase.Rows(rrASA).Item(iField)
                        End If
                    End If
                Next iField
                rrASA = rrASA + 1
            Loop

            Session("tblASABasePct") = rsASAPct

            rsASA = Session("tblASA2")
            rsASABase = Session("tblASABase2")
            rsASAPct = Session("tblASABasePct2")

            rrASA = 0

            Do While Not rsASA.Rows.Count = rrASA
                rsASAPct.Rows(rrASA).Item(0) = rsASA.Rows(rrASA).Item(0)
                For iField = 1 To rsASA.Columns.Count - 1
                    If rsASABase.Rows(rrASA).Item(iField) = 0 Then
                        rsASAPct.Rows(rrASA).Item(iField) = DBNull.Value
                    Else
                        rsASAPct.Rows(rrASA).Item(iField) = (rsASA.Rows(rrASA).Item(iField) - rsASABase.Rows(rrASA).Item(iField)) / rsASABase.Rows(rrASA).Item(iField)
                    End If
                Next iField
                rrASA = rrASA + 1
            Loop

            Session("tblASABasePct2") = rsASAPct

        End Sub

        'Function to round values
        'JRS 04/21/03
        Private Sub RoundValues()
            Dim rs As DataTable
            rs = Session("tblASA")
            Dim rrASA As Integer = 0
            Dim tblIdx As Integer = 0

            Do While True
                Do While Not rs.Rows.Count = rrASA
                    If Not IsDBNull(rs.Rows(rrASA).Item(1)) Then
                        For i = 1 To rs.Columns.Count - 1
                            If Not IsDBNull(rs.Rows(rrASA).Item(i)) Then
                                Select Case Math.Abs(rs.Rows(rrASA).Item(i))
                                    Case Is > 1000
                                        rs.Rows(rrASA).Item(i) = Math.Round(rs.Rows(rrASA).Item(i), 0)
                                    Case Is > 100
                                        rs.Rows(rrASA).Item(i) = Math.Round(rs.Rows(rrASA).Item(i), 1)
                                    Case Is > 10
                                        rs.Rows(rrASA).Item(i) = Math.Round(rs.Rows(rrASA).Item(i), 2)
                                    Case Is > 1
                                        rs.Rows(rrASA).Item(i) = Math.Round(rs.Rows(rrASA).Item(i), 3)
                                    Case Is >= 0
                                        rs.Rows(rrASA).Item(i) = Math.Round(rs.Rows(rrASA).Item(i), 4)
                                End Select
                                If tblIdx = 0 Then
                                    If rs.Rows(rrASA).Item(i) <= -100 Then
                                        rs.Rows(rrASA).Item(i) = DBNull.Value
                                    End If
                                End If
                            End If
                        Next i
                    End If
                    rrASA = rrASA + 1
                Loop
                tblIdx = tblIdx + 1
                If tblIdx = 1 Then
                    Session("tblASA") = rs
                    rs = Session("tblASA2")
                    rrASA = 0
                ElseIf tblIdx = 2 Then
                    Session("tblASA2") = rs
                    rs = Session("tblASABasePct")
                    rrASA = 0
                ElseIf tblIdx = 3 Then
                    Session("tblASABasePct") = rs
                    rs = Session("tblASABasePct2")
                    rrASA = 0
                Else
                    Session("tblASABasePct2") = rs
                    Exit Do
                End If
            Loop
        End Sub


        Protected Sub cmb_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
            If Cmb_Year.SelectedIndex = -1 Then Exit Sub
            ClearValues()

            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
            PercentCalculation()
            RoundValues()

            grdASA.DataBind()
            grdASA2.DataBind()

            Session("ASACountryIdx") = cmb_Country.SelectedIndex
        End Sub

        Protected Sub Cmb_Year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Year.SelectedIndexChanged
            If cmb_Country.SelectedIndex = -1 Then Exit Sub
            ClearValues()
            If Not IsNothing(Session("ASACountryIdx")) Then
                cmb_Country.SelectedIndex = Session("ASACountryIdx")
            End If
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
            PercentCalculation()
            RoundValues()

            grdASA.DataBind()
            grdASA2.DataBind()

        End Sub

        Private Sub RefreshDisplay()
            grdASA.DataBind()
            grdASA2.DataBind()
        End Sub

        'Procedure to build array with codes to be saved in url
        Private Sub PackJScritpBuilder()
            If Session("ASAGroups") = False Then
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

        'jrs 2014/03/19
        Private Sub DisplayOverTime(sRowText As String, sColTitle As String)
            'JRS 09/12/02
            'Subroutine to display variable over time
            'JRS 12/10/02 MULTIPLE FILES
            iFileOld = cboFiles.SelectedIndex
            Dim iFileIdx As Integer

            'jam 2021/12/29
            Dim Vpath As String
            Dim Vext As String
            Dim vfriendlyurl As String
            Dim vurl As String

            bShow = False

            'ASA RAW
            LoadOverTime("Raw annual values", "ASARAW", sRowText, sColTitle)
            LoadOverTime("Cumulative change in raw values, percent", "ASARAWDMAT", sRowText, sColTitle)
            'ASA GDP
            LoadOverTime("Impact/intensity per million GDP", "ASAGDP", sRowText, sColTitle)
            LoadOverTime("Cumulative dematerialization of impact per unit of GDP, percent", "ASAGDPDMAT", sRowText, sColTitle)
            LoadOverTime("Raw values associated with GDP growth (defined as gross rebound effect)", "ASAGDPGRRB", sRowText, sColTitle)
            'ASA POP
            LoadOverTime("Impact/intensity per thousand population", "ASAPOP", sRowText, sColTitle)
            LoadOverTime("Cumulative dematerialization of impact per unit of population, percent", "ASAPOPDMAT", sRowText, sColTitle)
            LoadOverTime("Raw values associated with population growth (defined as gross rebound effect)", "ASAPOPGRRB", sRowText, sColTitle)
            'ASA LABOR
            LoadOverTime("Impact/intensity per thousand labor", "ASALAB", sRowText, sColTitle)
            LoadOverTime("Cumulative dematerialization of impact per unit of labor, percent", "ASALABDMAT", sRowText, sColTitle)
            LoadOverTime("Raw values associated with labor growth (defined as gross rebound effect)", "ASALABGRRB", sRowText, sColTitle)

            If bShow Then
                Dim sPar As String
                If Session("ASAGroups") = True Then
                    sPar = "Group=" & Session("MMGroup")
                Else
                    sPar = "Country=" & Session("MMCountry")
                End If
                'JRS 03/24/04
                Session("TableDisplayParent") = "frm_ASA.aspx?" & PrepareForURL(sPar)
                'jam 2021/12/29
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


                vurl = FriendlyUrl.Href("~/frm_TableDisplay")

                Response.Redirect(vurl)

            End If
        End Sub

        Private Sub LoadOverTime(sRow As String, sVar As String, sRowText As String, sColTitle As String)
            'Display over time
            'Cell sRow, Fossil Fuel Use (BBOE)
            If (sRowText = sRow) And (sColTitle = "Fossil Fuel Use (BBOE)") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs(sVar, 1, 0, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            'Cell sRow, Carbon Emissions (Billion Tons)
            If (sRowText = sRow) And (sColTitle = "Carbon Emissions (Billion Tons)") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs(sVar, 2, 0, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            'Cell sRow, Deforestation (Million Hectares)
            If (sRowText = sRow) And (sColTitle = "Deforestation (Million Hectares)") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs(sVar, 3, 0, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            'Cell sRow, Water Use (Cubic Kilometers)
            If (sRowText = sRow) And (sColTitle = "Water Use (Cubic Kilometers)") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs(sVar, 4, 0, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
        End Sub

        Private Sub DisplayOverTime2(sRowText As String, sColTitle As String)

            'jam 2021/12/29
            Dim Vpath As String
            Dim Vext As String
            Dim vfriendlyurl As String
            Dim vurl As String

            'JRS 1/25/03
            '******************* GRID 2 ***************************'
            bShow = False
            'Cell GDP, Values
            If (sRowText = "GDP, billion") And (sColTitle = "Values") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs("GDP", 1, 1, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            'Cell GDP, Cumulative Percentage Change
            If (sRowText = "GDP, billion") And (sColTitle = "Cumulative Percentage Change") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs("ASAGDPCUMCHG", 1, 1, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            'Cell Population, Values
            If (sRowText = "Population, million") And (sColTitle = "Values") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs("POP", 1, 1, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            'Cell Population, Cumulative Percentage Change
            If (sRowText = "Population, million") And (sColTitle = "Cumulative Percentage Change") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs("ASAPOPCUMCHG", 1, 1, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            'Cell Labor Force, Values
            If (sRowText = "Labor Force, million") And (sColTitle = "Values") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs("LAB", 1, 1, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            'Cell Labor Force, Cumulative Percentage Change
            If (sRowText = "Labor Force, million") And (sColTitle = "Cumulative Percentage Change") Then
                'JRS 12/10/02 MULTIPLE FILES
                For iFileIdx = 0 To cboFiles.Items.Count - 1
                    cboFiles.SelectedIndex = iFileIdx
                    LoadVarAllYrs("ASALABCUMCHG", 1, 1, (iFileIdx = 0))
                Next iFileIdx
                cboFiles.SelectedIndex = iFileOld
                bShow = True
            End If
            '******************************************************'

            If bShow Then
                Dim sPar As String
                If Session("ASAGroups") = True Then
                    sPar = "Group=" & Session("MMGroup")
                Else
                    sPar = "Country=" & Session("MMCountry")
                End If
                'JRS 03/24/04
                Session("TableDisplayParent") = "frm_ASA.aspx?" & PrepareForURL(sPar)
                'jam 2021/12/29
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


                vurl = FriendlyUrl.Href("~/frm_TableDisplay")

                Response.Redirect(vurl)


            End If
        End Sub

        ' Function to Load Values into PLOT vector, and then display them using frm_TableDisplay
        ' parameters:
        ' 1. name of the variable.
        ' 2. index for 2nd dimension.
        ' 3. index for 3rd dimension
        ' 4. Boolean to indicate we're starting from column 0 (true), or we're appending more
        '    columns (false).
        Public Sub LoadVarAllYrs(sName As String, idx2 As Integer, idx3 As Integer, newCols As Boolean)
            'JRS 9/11/02
            Dim initialCol As Integer

            If newCols Then
                ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            End If
            'JRS 12/03/02 COUNTE = COUNTE + dim2size
            Session("COUNTE") = Session("COUNTE") + 1
            'PercentOn = False
            Dim idx As Integer = -1
            Dim sDim2 As String = ""
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sName, idx, cmb_Country.Text, cmb_Country.SelectedIndex + 1, sDim2, idx2, "", idx3, "", cboFiles.SelectedIndex, False, False,
            Session("ASAGroups"), 1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sName + CStr(cboFiles.SelectedIndex)))
            Else
                gLoadForDisplay(sName, idx, cmb_Country.Text, cmb_Country.SelectedIndex + 1, sDim2, idx2, "", idx3, "", cboFiles.SelectedIndex, True, True,
                Session("ASAGroups"), 1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, cmb_Country.SelectedValue, sDim2, "", Session("ReportGroup"), cboFiles.SelectedIndex, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

        End Sub

        'jrs 2014/03/19
        Protected Sub grdASA_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdASA.RowCommand
            DisplayOverTime(grdASA.Rows(e.CommandArgument).Cells(0).Text, e.CommandName)
        End Sub

        'jrs 2014/03/19
        Protected Sub grdASA2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdASA2.RowCommand
            DisplayOverTime2(grdASA2.Rows(e.CommandArgument).Cells(0).Text, e.CommandName)
        End Sub
        ''' <summary>
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' 
        ''' 
        '''      jam 2021/12/29
        ''' 
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' </summary>
        ''' <param name="sParameter">URL Parameters</param>
        ''' <returns>Parameters transformed in a valid URL format</returns>
        Public Function ASAPrepareForFriendlyURL(ByVal sParameter As String) As String

            Dim tmp As String
            tmp = sParameter
            tmp = Replace(tmp, "%", "%25")
            tmp = Replace(tmp, "&", "%26")
            tmp = Replace(tmp, "+", "%2B")
            tmp = Replace(tmp, "<", "%3C")

            If tmp.IndexOf("=") >= 0 Then

                tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
            End If



            ASAPrepareForFriendlyURL = tmp
        End Function

    End Class
'End Namespace
