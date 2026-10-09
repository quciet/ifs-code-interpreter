Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports IFs.Data
'jam 2020/11/09
Imports Microsoft.AspNet.FriendlyUrls

'jrs 2012/08/09 implemented by José Roberto Solórzano
'translated from vb6 version
Partial Class frm_WorldBankFinancialFlows
    Inherits Page



    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If

            If Session("WBFFGroup") Is Nothing Then
                Session("WBFFGroup") = False
            End If


            'LOADING DATA BASE NAME
            Dim sql As String
            sql = "SELECT (DEFINITION) AS DEF, REPORTVALUE1, REPORTVALUE2, REPORTVALUE3, REPORTVALUE4, REPORTVALUE5, REPORTVALUE6, " &
                "NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear1, Pct1stYear2, Pct1stYear3, Pct1stYear4, Pct1stYear5, Pct1stYear6, " &
                "PctBaseCase1, PctBaseCase2, PctBaseCase3, PctBaseCase4, PctBaseCase5, PctBaseCase6, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 " &
                "FROM FinancialFlows WHERE REPORT = TRUE ORDER BY [SUBMODULE NAME], NAME, DEFINITION"

            Dim tbl As DataTable
            SetDataTablefromAccess(Session("PutFdrvRun2") & "\IFsVar.mdb", sql, "", tbl)
            Session("tblWBFF") = tbl

            'FORMATING GRIDS
            With grdReport
                For i = 7 To .Columns.Count - 1
                    .Columns(i).Visible = False
                Next i
                .Columns(0).ItemStyle.Width = New System.Web.UI.WebControls.Unit(250)
                For i = 1 To .Columns.Count - 1
                    .Columns(i).ItemStyle.Width = New System.Web.UI.WebControls.Unit(125)
                Next
            End With

            '   Load Region Names Combo (Dropdown List) Box
            '   -----------------------------------------
            Session("FileNum") = 0
            If Session("WBFFGroup") = True Then
                LoadGroups()
            Else
                LoadRegions()
            End If

            LoadFiles()

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
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "GroupName", valtbl)
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
        sRunFile = Dir(Session("PutFdrvRun2") & "\*.run.db")

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

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'jam 2021/09/11
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim qtyPAram As Integer


        cswmWebMenu.UserData = "jsolorza::976739975"

        'jam 2021/09/11
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
        'jam 2021/09/11
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"

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
            'jam 2021/09/11
            'cswmItem.IconImage = "checkbox.gif"

            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/09/11
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")

        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Change from First Year"
        cswmItem.ID = "mnuFirstYear"
        If Session("mnuView") = "mnuFirstYear" Then
            'jam 2021/09/11
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/09/11
            cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Change From Base Case"
        cswmItem.ID = "mnuBaseCase"
        If Session("mnuView") = "mnuBaseCase" Then
            'jam 2021/09/11
            'cswmItem.IconImage = "checkbox.gif"

            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")

        Else
            'jam 2021/09/11
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
            'cswmItem.IconImage = "blank.gif"
        End If
        If Session("FileNum") = 1 Then
            cswmItem.Enabled = False
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Switch"
        cswmGroup1.ID = "mnuSwitch"

        cswmItem = cswmGroup1.Items.Add()
        If Session("WBFFGroup") Then
            cswmItem.Caption = "Use Country"
        Else
            cswmItem.Caption = "Use Groups"
        End If
        cswmItem.ID = "mnuGroups"

        'jrs 2013/09/04
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jam 2021/09/11
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

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_WorldBankFinancialFlows.aspx")


        If othCountry <> "" Then
            vurl = FriendlyUrl.Href("~/frm_WorldBankFinancialFlows", WorldPrepareForFriendlyURL(othCountry))

            Response.Redirect(vurl)

        Else

            vurl = FriendlyUrl.Href("~/frm_WorldBankFinancialFlows")

        End If


        'jam 2021/09/11
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String

        'jam 2021/05/01
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



        If Not Page.IsPostBack Then
            'jrs 2005/03/15
            If Not Session("WBFFFileIndex") = Nothing Then
                cboFiles.SelectedIndex = Session("WBFFFileIndex")
            Else 'jrs 2026/01/26
                cboFiles.SelectedIndex = 1
            End If
            cboFiles_SelectedIndexChanged(Me, New System.EventArgs)
        Else
            RefreshDisplay()
        End If

        'jrs 2011/05/06
        'If Not IsNothing(Request.QueryString("Country")) Then
        'jam 2021/09/11
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Session("vCountry"))) And Not Session("WBFFGroup") Then
            'jam 2021/09/11
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> Session("vCountry") Or Not Page.IsPostBack Then
                'jam 2021/09/11
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = Session("vCountry")
                'jrs 2012/08/15 Session("WBFFGroup") = False
                If Session("WBFFGroup") = True Then
                    cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions()
                    Session("WBFFGroup") = False
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
            'jam 2021/09/11
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Session("vCountry"))) And Session("WBFFGroup") Then 'jam 2021/09/11
            'jam 2021/09/11
            'If cmb_Country.SelectedValue <> Request.QueryString("Group") Then
            If cmb_Country.SelectedValue <> Session("vCountry") Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/09/11
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(Session("vCountry"), False, Me)
                If gIdx > 0 Then
                    cmb_Country.SelectedIndex = gIdx - 1
                    Session("WBFFCountryIdx") = cmb_Country.SelectedIndex
                    'jam 2021/09/11
                    'Session("MMGroup") = Request.QueryString("Group")
                    Session("MMGroup") = Session("vCountry")
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("WBFFGroup") = True Then
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
            Session("WBFFCountryIdx") = cmb_Country.SelectedIndex
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
            Case "Percent Change from First Year"
                mnuFirstYear_Click()
            Case "Percent Change From Base Case"
                MnuBaseCase_Click()

            Case "Use Groups"
                mnuGroups_Click(sender, e)
            Case "Use Country"
                mnuGroups_Click(sender, e)
        End Select

    End Sub

    Protected Sub cboFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiles.SelectedIndexChanged
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2006/02/03
        Dim sDispOpt As String
        Dim Number As String
        Dim VLN As Object
        sDispOpt = "Normal"
        If Session("mnuView") <> "mnuViewNormal" Then
            If Session("mnuView") = "mnuFirstYear" Then
                sDispOpt = "FrstYr"
            Else
                sDispOpt = "Pct"
            End If
            mnuViewNormal_Click()
        End If

        Session("FileNum") = cboFiles.SelectedIndex

        Dim finam As String
        If Session("FileNum") > 0 Then
            finam$ = Session("PutFdrvRun") + Mid(cboFiles.Text, InStr(cboFiles.Text, "-") + 2, Len(cboFiles.Text) - 1 - InStr(cboFiles.Text, "-"))
        Else
            finam$ = Session("PutFdrvRun") + "Working.run.db"
        End If


        Dim fNum As Integer
        Dim FinalYear As Short

        If Session("mnuSQLite") = "True" Then
            Dim working As IStorage
            Dim storageSvc As IStorageService = New StorageService(finam)
            working = storageSvc.Open()
            FinalYear = working.LastYear
        Else
            fNum = FreeFile()
            FileOpen(fNum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            VLN = VarPoint(PosModYear)
            Session("RECORD") = (VLN) * 4 - 3
            FileGet(fNum, Number, Session("RECORD"))
            FinalYear = Number
            FileClose(fNum)
        End If

        If InStr(StrConv(finam$, vbUpperCase), "IFSBASEHIST.RUN.DB") > 0 _
        Or UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            'In case of History file change First Year
            Session("FirstYear") = FirstYearHistory
            Session("LastYear") = LastYearHistory
        Else
            Session("FirstYear") = FirstYearForecast
            Session("LastYear") = Session("LastYearForecast")
        End If

        'DEFINE COLUMNS AND YEARS TO USE
        'RULES:
        'IF LAST YEAR < 2050, EVERY 10 YEARS
        'IF LAST YEAR > 2050, EVERY 20 YEARS
        'IF USING HISTORY USE EVERY 10 YEARS
        With grdReport
            .Columns(1).HeaderText = Str(Session("FirstYear"))
            .Columns(6).HeaderText = Str(FinalYear)
            If Session("FirstYear") = FinalYear Then
                .Columns(6).Visible = False
            End If
            Dim colidx As Integer
            Dim iYr As Integer
            Dim stepInc As Integer
            Dim startYr As Integer

            'jrs 2005/03/22
            Dim iInterVal As Integer
            iInterVal = FinalYear - Session("FirstYear")
            If (iInterVal < 50 And iInterVal > 25) Then
                stepInc = 10
            ElseIf (iInterVal < 26 And iInterVal > 5) Then
                stepInc = 5
            ElseIf (iInterVal < 6) Then
                stepInc = 1
            Else
                stepInc = 20
            End If

            '        'use 10 years
            '        If (FirstYear% = FirstYearHistory% Or FinalYear% < 2050) Then
            '            stepInc = 10
            '        'use 20 years
            '        Else
            '            stepInc = 20
            '        End If

            If (Session("FirstYear") Mod 10 = 0) Then
                startYr = Session("FirstYear") + stepInc
            Else
                startYr = Int(Session("FirstYear") / 10) * 10 + stepInc
            End If
            colidx = 2
            'check all years jrs 2005/03/22
            For iYr = startYr To FinalYear - 1 Step stepInc
                .Columns(colidx).HeaderText = Str(iYr)
                .Columns(colidx).Visible = True
                colidx = colidx + 1
            Next iYr
            'hide rest of the columns
            Dim icol As Integer
            For icol = colidx To 5
                .Columns(icol).Visible = False
            Next icol
        End With
        'display item
        cmb_Country_SelectedIndexChanged(Me, New EventArgs)
        'jrs 2006/02/03
        If sDispOpt <> "Normal" Then
            If sDispOpt = "FrstYr" Then
                mnuFirstYear_Click()
            Else
                MnuBaseCase_Click()
            End If
        End If

        cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").Enabled = Not (Session("FileNum") = 1)

    End Sub

    Public Sub mnuViewNormal_Click()
        'jrs 2006/07/21
        If Session("mnuView") = "mnuViewNormal" Then Exit Sub

        'JRS 2/24/03 GRID
        With grdReport
            For i = 1 To 6
                .Columns(i).Visible = True
                .Columns(i + 11).Visible = False
                .Columns(i + 17).Visible = False
            Next i
            .DataBind()
        End With
        'jam 2021/09/11
        cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"

        Session("mnuView") = "mnuViewNormal"
    End Sub

    Public Sub mnuFirstYear_Click()
        'jrs 2006/07/21
        If Session("mnuView") = "mnuFirstYear" Then Exit Sub

        With grdReport
            For i = 1 To 6
                .Columns(i + 11).Visible = True
                .Columns(i).Visible = False
                .Columns(i + 17).Visible = False
            Next i
            .DataBind()
        End With
        'jam 2021/09/11
        cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"

        Session("mnuView") = "mnuFirstYear"

    End Sub

    Public Sub MnuBaseCase_Click()
        'jrs 2006/07/21
        If Session("mnuView") = "mnuBaseCase" Then Exit Sub

        With grdReport
            For i = 1 To 6
                .Columns(i + 17).Visible = True
                .Columns(i).Visible = False
                .Columns(i + 11).Visible = False
            Next i
            .DataBind()
        End With
        'jam 2021/09/11
        cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"

        Session("mnuView") = "mnuBaseCase"

    End Sub

    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        'jam 2021/09/11
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups" Then
            cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Country"
            lbl_countries.Text = "Groups"
            LoadGroups()
            Session("WBFFGroup") = True
        Else
            cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"
            lbl_countries.Text = "Countries or Regions"
            LoadRegions()
            Session("WBFFGroup") = False
        End If
        cmb_Country.SelectedIndex = 0 ' Display First Item
        Session("WBFFCountryIdx") = 0
        cboFiles_SelectedIndexChanged(Me, New System.EventArgs)
        'jrs 2011/05/06
        If Session("WBFFGroup") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                'jam 2021/09/11
                'Support.FormManager.Show(Me, "frm_WorldBankFinancialFlows.aspx?Group=" & PrepareForURL(cmb_Country.SelectedValue))

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_WorldBankFinancialFlows.aspx")


                vurl = FriendlyUrl.Href("~/frm_WorldBankFinancialFlows", PrepareForURL(cmb_Country.SelectedValue))

                Response.Redirect(vurl)



            Else
                'jam 2021/09/11
                'Support.FormManager.Show(Me, "frm_WorldBankFinancialFlows.aspx?Group=" & PrepareForURL(Session("MMGroup")))

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_WorldBankFinancialFlows.aspx")


                vurl = FriendlyUrl.Href("~/frm_WorldBankFinancialFlows", Session("MMGroup"))

                Response.Redirect(vurl)

            End If
        Else
            'jam 2021/09/11
            'Support.FormManager.Show(Me, "frm_WorldBankFinancialFlows.aspx?Country=AF")

            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_WorldBankFinancialFlows.aspx")


            vurl = FriendlyUrl.Href("~/frm_WorldBankFinancialFlows", "AF")

            Response.Redirect(vurl)


        End If
    End Sub

    Protected Sub cmb_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
        'JRS 2/24/03 GRID
        Dim iYr As Short
        Dim iFrstCol As Short
        Select Case (True)
            Case Session("mnuView") = "mnuViewNormal"
                iFrstCol = 1
            Case Session("mnuView") = "mnuFirstYear"
                iFrstCol = 11
            Case Session("mnuView") = "mnuBaseCase"
                iFrstCol = 17
        End Select
        'JRS 03/25/04 SYNCHRONIZING WITH LAST REGION/GROUP
        If Not Page.IsPostBack And Not IsNothing(Session("WBFFCountryIdx")) Then
            cmb_Country.SelectedIndex = Session("WBFFCountryIdx")
        End If

        'JRS 3/20/02 GROUPING 'Exclude 'ALL'
        Dim j As Object
        Dim k As Short
        Dim RegInd As Integer
        RegInd = cmb_Country.SelectedIndex + 1
        For iYr = 0 To 5
            With grdReport
                If (.Columns(iFrstCol + iYr).Visible) Then
                    Summary(RegInd, (Val(.Columns(iYr + 1).HeaderText) - Session("FirstYear")))
                End If
            End With
        Next iYr
        'JRS 04/02/03 ROUND
        RoundValues()
        RefreshDisplay()
        'JRS 03/25/04
        Session("WBFFCountryIdx") = cmb_Country.SelectedIndex

    End Sub

    Private Sub Summary(ByRef RegionPTR As Short, ByRef YearOffset As Short)
        If YearOffset < 0 Then Exit Sub
        If RegionPTR < 1 Then Exit Sub
        'jrs 2006/04/24
        Dim Vals(1) As Single, BASE(1) As Single, FYear(1) As Single

        Dim NUMBER2 As Single
        Dim Number As Single
        Dim finam As String
        Dim tbl As DataTable = Session("tblWBFF")
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Region number and Offset from First Year as parameters

        If Session("FileNum") = 0 Then
            finam = Session("PutFdrvRun") & "working.run.db"
        Else
            finam = Session("RunFiles")(Session("FileNum"))
        End If

        'jrs 2004/11/26 Dim rs As DataTable
        'JRS 2/22/03
        Dim repcol As Integer
        Dim icol As Short
        'calculate report column to use
        For icol = 1 To 6
            If grdReport.Columns(icol).HeaderText = Str(Session("FirstYear") + YearOffset) Then
                repcol = icol
                Exit For
            End If
        Next icol

        'JRS 12/01/03
        Dim idx As Short
        idx = 1

        Dim FirstYearVal As Object
        Dim BaseCase As Single
        Dim dim2, k2 As Object
        Dim s As String
        Dim i As Integer
        'jrs 2006/07/25
        Dim bFirstTime As Boolean = True
        Dim bLastTime As Boolean = False
        'jrs 2009/01/29
        Dim dim3 As Integer

        'DEF is column 0
        'ReportValue1 is column 1
        'Name is column 7
        'ReportDimension2 is 9
        'Pct1stYear1 is column 12
        'PctBaseCase1 is column 18
        'ReportDimension3 is column 25

        Dim MyFileforDisplay, MyFileforDisplay1 As IStorage
        Dim MyDF, MyDF1 As IDataframe

        With tbl
            For i = 0 To .Rows.Count - 1 'jrs 2004/11/26 .Rows.Count - 1
                'JRS 2/26/03
                'jrs 2009/02/25 If bSingleGrid And Not IsDBNull(.Rows(i).Item("Name")) Then
                If Not IsDBNull(.Rows(i).Item(7)) Then
                    s = .Rows(i).Item(7).ToString
                    idx = IfsVars.Item(s).Index
                End If
                'JRS 1/18/03
                'jrs 2004/11/26 If (.Rows(i).Item("Report") Or .Rows(i).Item("Report2")) And Not IsDBNull(.Rows(i).Item("Name")) Then
                If Not IsDBNull(.Rows(i).Item(7)) Then
                    Dim sDim2 As String, idxDim2 As Integer
                    'jrs 2009/01/29 dim2 = .Rows(i).Item("Dimension2")
                    sDim2 = ""
                    idxDim2 = -1
                    dim2 = VDim2(idx)

                    If dim2 > 0 Then
                        sDim2 = .Rows(i).Item(9).ToString
                        'jrs 2009/01/29 idxDim2 = gGetDimIdx(.Rows(i).Item("Dimension2"), sDim2, True, Me)
                        idxDim2 = gGetDimIdx(dim2, sDim2, True, Me)
                    End If
                    'jrs 2006/05/17 Dim3
                    Dim sDim3 As String, idxDim3 As Integer
                    sDim3 = ""
                    idxDim3 = -1
                    dim3 = VDim3(idx)

                    'jrs 2009/01/29 If .Rows(i).Item("Dimension3") > 0 Then
                    If dim3 > 0 Then
                        sDim3 = .Rows(i).Item(25).ToString
                        'jrs 2009/01/29 idxDim3 = gGetDimIdx(.Rows(i).Item("Dimension3"), sDim3, True, Me)
                        idxDim3 = gGetDimIdx(dim3, sDim3, True, Me)
                    End If

                    'jrs 2006/07/25
                    If i = .Rows.Count - 1 And Session("FileNum") = 1 And YearOffset = 0 Then
                        bLastTime = True
                    End If
                    'jrs 2006/04/24
                    'Load Value
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(.Rows(i).Item(7).ToString, idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, finam$, Session("FileNum"), bFirstTime, bLastTime,
                    Session("WBFFGroup"), 1, Vals, 1, 1, Session("FirstYear") + YearOffset, Session("FirstYear") + YearOffset, YearOffset, True,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, Session("df" + .Rows(i).Item(7).ToString + CStr(Session("FileNum"))))
                    Else
                        gLoadForDisplay(.Rows(i).Item(7).ToString, idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, finam$, Session("FileNum"), bFirstTime, bLastTime,
                    Session("WBFFGroup"), 1, Vals, 1, 1, Session("FirstYear") + YearOffset, Session("FirstYear") + YearOffset, YearOffset, True,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    End If
                    'Load Base
                    If Session("FileNum") <> 1 Then
                        'jrs 2006/07/25
                        If i = .Rows.Count - 1 And YearOffset = 0 Then
                            bLastTime = True
                        End If
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(.Rows(i).Item(7).ToString, idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, "", 1, bFirstTime, bLastTime,
                        Session("WBFFGroup"), 1, BASE, 1, 1, Session("FirstYear") + YearOffset, Session("FirstYear") + YearOffset, YearOffset, True,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay1, Session("df" + .Rows(i).Item(7).ToString + "B"))
                        Else
                            gLoadForDisplay(.Rows(i).Item(7).ToString, idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, "", 1, False, bLastTime,
                            Session("WBFFGroup"), 1, BASE, 1, 1, Session("FirstYear") + YearOffset, Session("FirstYear") + YearOffset, YearOffset, True,
                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                            Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        End If
                    Else
                        BASE(1) = Vals(1)
                    End If

                    bFirstTime = False
                    'Load First Year
                    If YearOffset > 0 Then
                        'jrs 2006/07/25
                        If i = .Rows.Count - 1 Then
                            bLastTime = True
                        End If
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(.Rows(i).Item(7).ToString, idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, finam$, Session("FileNum"), False, bLastTime,
                        Session("WBFFGroup"), 1, FYear, 1, 1, Session("FirstYear"), Session("FirstYear"), 0, True,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, Session("df" + .Rows(i).Item(7).ToString + CStr(Session("FileNum"))))
                        Else
                            gLoadForDisplay(.Rows(i).Item(7).ToString, idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, finam$, Session("FileNum"), False, bLastTime,
                            Session("WBFFGroup"), 1, FYear, 1, 1, Session("FirstYear"), Session("FirstYear"), 0, True,
                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                            Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        End If
                    Else
                        FYear(1) = Vals(1)
                    End If

                    .Rows(i).Item(repcol) = Vals(1)
                    If FYear(1) <> 0 Then
                        .Rows(i).Item(11 + repcol) = 100 * (Vals(1) - FYear(1)) / FYear(1)
                    Else
                        .Rows(i).Item(11 + repcol) = 0
                    End If
                    If BASE(1) <> 0 Then
                        .Rows(i).Item(17 + repcol) = 100 * (Vals(1) - BASE(1)) / BASE(1)
                    Else
                        .Rows(i).Item(17 + repcol) = 0
                    End If

                End If
                idx = idx + 1
            Next i
        End With

        Session("tblWBFF") = tbl

    End Sub

    Private Sub RoundValues()
        Dim sUpd1 As String
        Dim sUpd2 As String
        Dim sUpd3 As String
        Dim sUpd4 As String
        Dim sUpd5 As String
        'JRS 09/09/04
        Dim sUpd6 As String
        Dim tbl As DataTable = Session("tblWBFF")

        'jrs 2004/11/26
        With tbl
            For i = 0 To .Rows.Count - 1
                If Not IsDBNull(.Rows(i).Item(7)) Then
                    If Math.Abs(CSng(.Rows(i).Item(1).ToString)) >= 1000 Then
                        .Rows(i).Item(1) = Math.Round(CSng(.Rows(i).Item(1).ToString), 0)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(1).ToString)) >= 100 Then
                        .Rows(i).Item(1) = Math.Round(CSng(.Rows(i).Item(1).ToString), 1)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(1).ToString)) >= 10 Then
                        .Rows(i).Item(1) = Math.Round(CSng(.Rows(i).Item(1).ToString), 2)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(1).ToString)) >= 1 Then
                        .Rows(i).Item(1) = Math.Round(CSng(.Rows(i).Item(1).ToString), 3)
                    ElseIf .Rows(i).Item(1).ToString < 1 Then
                        .Rows(i).Item(1) = Math.Round(CSng(.Rows(i).Item(1).ToString), 4)
                    End If
                    If Math.Abs(CSng(.Rows(i).Item(2).ToString)) >= 1000 Then
                        .Rows(i).Item(2) = Math.Round(CSng(.Rows(i).Item(2).ToString), 0)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(2).ToString)) >= 100 Then
                        .Rows(i).Item(2) = Math.Round(CSng(.Rows(i).Item(2).ToString), 1)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(2).ToString)) >= 10 Then
                        .Rows(i).Item(2) = Math.Round(CSng(.Rows(i).Item(2).ToString), 2)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(2).ToString)) >= 1 Then
                        .Rows(i).Item(2) = Math.Round(CSng(.Rows(i).Item(2).ToString), 3)
                    ElseIf .Rows(i).Item(2).ToString < 1 Then
                        .Rows(i).Item(2) = Math.Round(CSng(.Rows(i).Item(2).ToString), 4)
                    End If
                    If Math.Abs(CSng(.Rows(i).Item(3).ToString)) >= 1000 Then
                        .Rows(i).Item(3) = Math.Round(CSng(.Rows(i).Item(3).ToString), 0)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(3).ToString)) >= 100 Then
                        .Rows(i).Item(3) = Math.Round(CSng(.Rows(i).Item(3).ToString), 1)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(3).ToString)) >= 10 Then
                        .Rows(i).Item(3) = Math.Round(CSng(.Rows(i).Item(3).ToString), 2)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(3).ToString)) >= 1 Then
                        .Rows(i).Item(3) = Math.Round(CSng(.Rows(i).Item(3).ToString), 3)
                    ElseIf .Rows(i).Item(3).ToString < 1 Then
                        .Rows(i).Item(3) = Math.Round(CSng(.Rows(i).Item(3).ToString), 4)
                    End If
                    If Math.Abs(CSng(.Rows(i).Item(4).ToString)) >= 1000 Then
                        .Rows(i).Item(4) = Math.Round(CSng(.Rows(i).Item(4).ToString), 0)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(4).ToString)) >= 100 Then
                        .Rows(i).Item(4) = Math.Round(CSng(.Rows(i).Item(4).ToString), 1)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(4).ToString)) >= 10 Then
                        .Rows(i).Item(4) = Math.Round(CSng(.Rows(i).Item(4).ToString), 2)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(4).ToString)) >= 1 Then
                        .Rows(i).Item(4) = Math.Round(CSng(.Rows(i).Item(4).ToString), 3)
                    ElseIf .Rows(i).Item(4).ToString < 1 Then
                        .Rows(i).Item(4) = Math.Round(CSng(.Rows(i).Item(4).ToString), 4)
                    End If
                    If Math.Abs(CSng(.Rows(i).Item(5).ToString)) >= 1000 Then
                        .Rows(i).Item(5) = Math.Round(CSng(.Rows(i).Item(5).ToString), 0)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(5).ToString)) >= 100 Then
                        .Rows(i).Item(5) = Math.Round(CSng(.Rows(i).Item(5).ToString), 1)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(5).ToString)) >= 10 Then
                        .Rows(i).Item(5) = Math.Round(CSng(.Rows(i).Item(5).ToString), 2)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(5).ToString)) >= 1 Then
                        .Rows(i).Item(5) = Math.Round(CSng(.Rows(i).Item(5).ToString), 3)
                    ElseIf .Rows(i).Item(5).ToString < 1 Then
                        .Rows(i).Item(5) = Math.Round(CSng(.Rows(i).Item(5).ToString), 4)
                    End If
                    If Math.Abs(CSng(.Rows(i).Item(6).ToString)) >= 1000 Then
                        .Rows(i).Item(6) = Math.Round(CSng(.Rows(i).Item(6).ToString), 0)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(6).ToString)) >= 100 Then
                        .Rows(i).Item(6) = Math.Round(CSng(.Rows(i).Item(6).ToString), 1)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(6).ToString)) >= 10 Then
                        .Rows(i).Item(6) = Math.Round(CSng(.Rows(i).Item(6).ToString), 2)
                    ElseIf Math.Abs(CSng(.Rows(i).Item(6).ToString)) >= 1 Then
                        .Rows(i).Item(6) = Math.Round(CSng(.Rows(i).Item(6).ToString), 3)
                    ElseIf .Rows(i).Item(6).ToString < 1 Then
                        .Rows(i).Item(6) = Math.Round(CSng(.Rows(i).Item(6).ToString), 4)
                    End If
                    .Rows(i).Item(13) = Math.Round(CSng(.Rows(i).Item(13).ToString), 2)
                    .Rows(i).Item(14) = Math.Round(CSng(.Rows(i).Item(14).ToString), 2)
                    .Rows(i).Item(15) = Math.Round(CSng(.Rows(i).Item(15).ToString), 2)
                    .Rows(i).Item(16) = Math.Round(CSng(.Rows(i).Item(16).ToString), 2)
                    .Rows(i).Item(17) = Math.Round(CSng(.Rows(i).Item(17).ToString), 2)
                    .Rows(i).Item(18) = Math.Round(CSng(.Rows(i).Item(18).ToString), 2)
                    .Rows(i).Item(19) = Math.Round(CSng(.Rows(i).Item(19).ToString), 2)
                    .Rows(i).Item(20) = Math.Round(CSng(.Rows(i).Item(20).ToString), 2)
                    .Rows(i).Item(21) = Math.Round(CSng(.Rows(i).Item(21).ToString), 2)
                    .Rows(i).Item(22) = Math.Round(CSng(.Rows(i).Item(22).ToString), 2)
                    .Rows(i).Item(23) = Math.Round(CSng(.Rows(i).Item(23).ToString), 2)
                End If
            Next
        End With

        Session("tblWBFF") = tbl
    End Sub

    Private Sub RefreshDisplay()
        Dim tbl As DataTable = Session("tblWBFF")

        'jrs 2009/02/26
        Dim i As Integer
        With tbl
            For i = 0 To .Rows.Count - 1
                If Not IsDBNull(.Rows(i).Item(9)) Then
                    If InStr(.Rows(i).Item(0).ToString, .Rows(i).Item(9).ToString) <> 1 And .Rows(i).Item(9).ToString <> "null" Then
                        .Rows(i).Item(0) = .Rows(i).Item(9).ToString & " - " & .Rows(i).Item(0).ToString
                    End If
                End If
            Next i
        End With


        grdReport.DataSource = tbl
        grdReport.DataBind()

        'DataReport.Refresh()
        'grdReport.CtlRefresh()
    End Sub

    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        If Session("WBFFGroup") = False Then
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

    Private Sub grdReport_DblClick(ByVal sVarName As String, ByVal sDim2Rep1 As String,
    ByVal sDim3Rep1 As String, ByVal iDim2 As Integer, ByVal sAgg As String)
        'jam 2021/09/11
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        With grdReport
            'JRS 11/14/02
            Dim fileIdx As Integer
            'JRS 05/19/03
            Dim Dim2 As String, Dim3 As String
            For fileIdx = 0 To cboFiles.Items.Count - 1
                Session("FileNum") = fileIdx
                LoadVarAllYrs(sVarName, iDim2, sDim2Rep1, sDim3Rep1, sAgg, (fileIdx = 0))
            Next fileIdx
            Session("FileNum") = cboFiles.SelectedIndex
            Dim sPar As String
            If Session("WBFFGroup") = True Then
                If Session("MMGroup") > "" Then
                    sPar = "Group=" & Session("MMGroup")
                End If
            Else
                If Session("MMCountry") > "" Then
                    sPar = "Country=" & Session("MMCountry")
                End If
            End If

            'JRS 03/24/04
            Session("TableDisplayParent") = "frm_WorldBankFinancialFlows.aspx?" & PrepareForURL(sPar)
            'jam 2021/09/11
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


            vurl = FriendlyUrl.Href("~/frm_TableDisplay")

            Response.Redirect(vurl)


        End With
    End Sub

    Public Sub LoadVarAllYrs(ByRef sName As String, ByRef idim2 As Short, ByRef sReportDimension2 As String, ByVal sReportDimension3 As String, ByRef sAggregation As String, ByRef bAddCol As Boolean)
        'jrs 2005/03/18
        If UCase(sReportDimension2) = "NULL" Then
            sReportDimension2 = ""
        End If
        'jrs 2006/05/17 Dim3
        If UCase(sReportDimension3) = "NULL" Then
            sReportDimension3 = ""
        End If
        'JRS 4/1/02
        Dim idx As Short, RegInd As Integer
        idx = IfsVars.Item(sName).Index 'Exclude 'ALL'
        If bAddCol Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        End If
        Session("COUNTE") = Session("COUNTE") + 1
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sName, idx, cmb_Country.SelectedValue, RegInd, sReportDimension2, -1, sReportDimension3, -1, "", Session("FileNum"), True, True, Session("ReportGroup"),
        1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))       'JRS 1/13/03
        Else

            gLoadForDisplay(sName, idx, cmb_Country.SelectedValue, RegInd, sReportDimension2, -1, sReportDimension3, -1, "", Session("FileNum"), True, True, Session("ReportGroup"),
            1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))       'JRS 1/13/03
        End If
        gLoadForDisplaySaveLbls(Session("COUNTE"), idx, cmb_Country.SelectedValue, sReportDimension2, sReportDimension3, Session("ReportGroup"), Session("FileNum"), "",
        Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))


        'JRSVBNET Load(frm_TableDisplay)
    End Sub


    Protected Sub grdReport_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdReport.RowCommand
        Dim sVarName As String
        Dim sDim2Rep1 As String = ""
        Dim sDim3Rep1 As String = ""
        Dim sAgg As String

        Dim iDim2 As Integer
        Dim tbl As DataTable = Session("tblWBFF")
        Dim currentRowIndex As Integer = Int32.Parse(e.CommandArgument.ToString())
        sVarName = tbl.Rows(currentRowIndex).Item("Name").ToString
        If Not IsDBNull(tbl.Rows(currentRowIndex).Item("REPORTDIMENSION2")) Then
            sDim2Rep1 = tbl.Rows(currentRowIndex).Item("REPORTDIMENSION2").ToString
        End If
        'need to check where to find these values
        If Not IsDBNull(tbl.Rows(currentRowIndex).Item("REPORTDIMENSION3")) Then
            sDim3Rep1 = tbl.Rows(currentRowIndex).Item("REPORTDIMENSION3").ToString
        End If

        iDim2 = tbl.Rows(currentRowIndex).Item("DIMENSION2").ToString
        sAgg = tbl.Rows(currentRowIndex).Item("AGGREGATION").ToString

        If sVarName > "" Then
            grdReport_DblClick(sVarName, sDim2Rep1, sDim3Rep1, iDim2, sAgg)
        End If

    End Sub
    Public Function WorldPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")
        'jam 2021/09/11
        tmp = Replace(tmp, """", "%22")



        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        WorldPrepareForFriendlyURL = tmp
    End Function


End Class
