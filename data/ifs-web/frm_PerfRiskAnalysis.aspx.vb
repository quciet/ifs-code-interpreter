Imports System.Math
Imports IFs.Data
Imports System.Drawing

'jrs 2013/05/29 implemented by José Roberto Solórzano
Partial Class frm_PerfRiskAnalysis
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
    End Sub

#End Region

    'jrs 2021/03/22
    Dim FilesCol As New Collection
    Dim dfCol As New Collection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'jrs 2024/04/09
        ''jrs 2020/01/10
        'If Not DevelopmentMode = "True" Then
        '    LoadMessage("This feature is not enabled.", Session("Message"))
        '    Support.FormManager.StateStore(Me)
        '    Support.FormManager.Show(Me, "frm_Message.aspx")
        'End If

        CreateMenu()

        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            'jrs 2005/11/30
            Session("TableDisplayParent") = "frm_PerfRiskAnalysis.aspx"

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

            'Load General Values
            LoadGralValues(Not bVarInGridEmpty)
            'Load Regions
            If Session("PerfRiskGroup") Is Nothing Or Session("PerfRiskGroup") = False Then
                LoadRegions()
            Else
                LoadGroups()
            End If

            'Load Files
            LoadFiles(gRetrieveFiles(Session("PutFdrvRun2"), Session("WorkingFile"), Session("RunFiles"), Session("FirstYear"), Session("FilesSelected"), Session("ShortSce"), Session("IFsRunFiles")), cboFiles)
            If cboFiles.Items.Count > 0 Then
                If Session("RiskcboFilesIdx") Is Nothing Then
                    'jrs 2026/01/26 changed default to base
                    cboFiles.SelectedIndex = 1
                Else
                    cboFiles.SelectedIndex = Session("RiskcboFilesIdx")
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
                If Session("RiskcmbYearIdx") Is Nothing Then
                    Cmb_Year.SelectedIndex = 0   ' Display First Item
                Else
                    Cmb_Year.SelectedIndex = Session("RiskcmbYearIdx")
                End If
            End If
            ' goes into Procedure summary
            If cmb_Country.Items.Count > 0 Then
                If Session("RiskcmbCountryIdx") Is Nothing Then
                    cmb_Country.SelectedIndex = 0   ' Display First Item
                Else
                    cmb_Country.SelectedIndex = Session("RiskcmbCountryIdx")
                End If
            End If

            If Session("PerfRiskGroup") Is Nothing Then
                Session("PerfRiskGroup") = False
            End If

            'jrs 2013/07/01
            If Not Session("bAbsolute") Then
                grd_TableDisplay.Rows(0).Cells(2).Text = "Evaluation: Relative"
                grd_TableDisplay.Rows(1).Cells(9).Text = "Standard Error"
            End If

            If bReLoad Then
                'jrs 2010/01/15
                LoadAllCtryValues(False) 'jrs 2011/01/10 true
                'jrs 2010/07/21
                LoadAvgStd()
            End If

            LoadValues()

        End If


        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
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
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        If Not Session("PerfRiskGroup") Then
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

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Options"
        cswmGroup1.ID = "mnuDispOpt"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Recalculate and update with new weights"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using absolute evaluation"
        cswmItem.ID = "mnuDispOptAbs"
        If Session("bAbsolute") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using relative evaluation with income level"
        cswmItem.ID = "mnuDispOptRel"
        If Session("bAbsolute") = False Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select multiple scenarios"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select multiple Country or Groups"
        cswmItem.IconImage = "blank.gif"

        'jrs 2011/05/06
        If Not IsNothing(Request.QueryString("Country")) Then
            If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
                Session("MMCountry") = Request.QueryString("Country")
                'jrs 2012/08/15 Session("PerfRiskGroup") = False
                If Session("PerfRiskGroup") = True Then
                    cswmWebMenu.Groups("mnuGroups").Caption = "Using Countries/&Regions"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions()
                    Session("PerfRiskGroup") = False
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
        ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
            If cmb_Country.SelectedValue <> Request.QueryString("Group") Or Not Page.IsPostBack Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                If gIdx > 0 Then
                    cmb_Country.SelectedIndex = gIdx - 1
                    Session("RiskcmbCountryIdx") = cmb_Country.SelectedIndex
                    Session("MMGroup") = Request.QueryString("Group")
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("PerfRiskGroup") = True Then
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
            Session("RiskcmbCountryIdx") = cmb_Country.SelectedIndex
            cmb_Country_SelectedIndexChanged(Me, New System.EventArgs)
        End If

        'jrs 2021/03/22
        If Session("FilesCol") Is Nothing Then
            FilesCol = New Collection
        Else
            FilesCol = Session("FilesCol")
        End If
        If Session("dfCol") Is Nothing Then
            dfCol = New Collection
        Else
            dfCol = Session("dfCol")
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
                idx = Session("RiskRegs")(i)
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
            Case "Recalculate and update with new weights"
                UpdateWeights()
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
        Session("RiskcboFilesIdx") = cboFiles.SelectedIndex

        LoadAllCtryValues(False)
        'End If
        LoadValues()

        'if selected file is not preselected reset
        If Session("lstCountry.SelCount") > 0 Then
            Dim idx As Integer
            If Not lstFiles.Items(Session("RiskcboFilesIdx")).Selected Then
                For i = 0 To Session("lstFiles.SelCount") - 1
                    idx = Session("RiskFiles")(i)
                    lstFiles.Items(idx).Selected = False
                Next
                Session("lstFiles.SelCount") = 0
            End If
        End If

        grd_TableDisplay.Visible = True
    End Sub

    'Subroutine to Load the Values
    'into Development Profile Values table
    Private Sub LoadValues()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'Loads values for given country, year, and scenario
        Dim VarVals(1) As Single
        Dim yr As Integer
        yr = Cmb_Year.Text

        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim bGroups As Boolean = False
        If Session("PerfRiskGroup") = True Then
            bGroups = True
        End If
        'jrs 2010/07/14
        If bGroups Then
            'Read Member Countries
            gLoadGroupMembers(cmb_Country.Text, POPidx, 1, 1, Session("gCountGroupMembers"), Session("IFs_MDB2Active"),
                Session("gGroupMembrs"), Session("gGroupMembersPtrs"), Session("gGroupMemberWeights"))
        End If

        'jrs 2010/02/10
        Dim rr As Integer
        Dim ValPop As Single

        'jrs 2021/03/22
        Dim MyFileforDisplay As IStorage
        If Not Session("FileForDisplay" + CStr(cboFiles.SelectedIndex)) Is Nothing Then
            MyFileforDisplay = Session("FileForDisplay" + CStr(cboFiles.SelectedIndex))
        End If

        grd_TableDisplay.Visible = False
        For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
            If Session("VarsInGrid")(rr).VarName > "" Then

                If bGroups Then
                    If Not Session("VarsInGrid")(rr).bFormula Then
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(Session("VarsInGrid")(rr).VarName, Session("VarsInGrid")(rr).VarIdx, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, False, False,
                            True, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                            Session("CurrencyVal"), MyFileforDisplay, Session("df" + Session("VarsInGrid")(rr).VarName + CStr(cboFiles.SelectedIndex)))
                        Else
                            gLoadForDisplay(Session("VarsInGrid")(rr).VarName, Session("VarsInGrid")(rr).VarIdx, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "Total", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
                            True, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                            Session("CurrencyVal"))
                        End If
                    Else
                        gLoadComp(1, Session("VarsInGrid")(rr).f.Formula_Renamed, Session("VarsInGrid")(rr).VarName, cmb_Country.Text, "", "", "Percent", -1, True,
                        cboFiles.SelectedIndex, False, False, Session("VarsInGrid")(rr).f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                        False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                        Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                        Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
                        VarVals(1) = Session("PLOT")(yr - Session("FirstYear") + 1, 1)
                    End If
                    grd_TableDisplay.Rows(rr).Cells(5).Text = AggRank(rr) 'jrs 2010/07/14 "N/A"
                Else
                    grd_TableDisplay.Rows(rr).Cells(5).Text = "<a href=""javascript:onPointClick3(" & CStr(rr) & ", 5)"">" & CStr(Session("VarsInGrid")(rr).VarValsGet(cmb_Country.SelectedIndex + 1).Index) & "</a>"
                    VarVals(1) = Session("VarsInGrid")(rr).VarValsGet(cmb_Country.SelectedIndex + 1).value
                End If

                grd_TableDisplay.Rows(rr).Cells(1).Text = VarVals(1)

                If (Session("VarsInGrid")(rr).bPopPct Or Session("VarsInGrid")(rr).bPopPort) And bGroups Then
                    If Not bGroups Then
                        VarVals(1) = Session("PopAll")(cmb_Country.SelectedIndex + 1)
                    Else
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET("POP", POPidx, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False,
                            True, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                            Session("CurrencyVal"), MyFileforDisplay, Session("dfPOP" + CStr(cboFiles.SelectedIndex)))
                        Else
                            gLoadForDisplay("POP", POPidx, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
                            True, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                            Session("CurrencyVal"))
                        End If
                    End If

                    If Session("VarsInGrid")(rr).bPopPct Then
                        grd_TableDisplay.Rows(rr).Cells(1).Text = Val(grd_TableDisplay.Rows(rr).Cells(1).Text) / VarVals(1) * 100
                    Else
                        grd_TableDisplay.Rows(rr).Cells(1).Text = Val(grd_TableDisplay.Rows(rr).Cells(1).Text) / VarVals(1)
                    End If
                End If

                'jrs 2010/07/23
                'Risk Contribution
                VarVals(1) = grd_TableDisplay.Rows(rr).Cells(1).Text
                'jrs 2011/01/10 changed MinYr1 to Min and MaxYr1 to Max to compute in Run of the model (where we don't have future years)
                If Round(Session("VarsInGrid")(rr).Min, 6) <> Round(Session("VarsInGrid")(rr).Max, 6) Then
                    'if higher is riskier
                    If Session("VarsInGrid")(rr).Alert > Session("VarsInGrid")(rr).Goal Then
                        VarVals(1) = (VarVals(1) - Round(Session("VarsInGrid")(rr).Min, 6)) / Round(Session("VarsInGrid")(rr).Max - Session("VarsInGrid")(rr).Min, 6)
                    Else 'if lower is riskier
                        VarVals(1) = (VarVals(1) - Round(Session("VarsInGrid")(rr).Max, 6)) / Round(Session("VarsInGrid")(rr).Min - Session("VarsInGrid")(rr).Max, 6)
                    End If
                Else
                    VarVals(1) = 0
                End If
                grd_TableDisplay.Rows(rr).Cells(4).Text = "<a href=""javascript:onPointClick2(" & CStr(rr) & ", 4)"">" & CStr(Round(VarVals(1), 2)) & "</a>"
                'jrs 2010/07/30
                Session("RiskIdx")(rr) = VarVals(1)

                'jrs 2010/07/21
                'Standard Deviation
                VarVals(1) = grd_TableDisplay.Rows(rr).Cells(1).Text
                If Session("VarsInGrid")(rr).StdDevYr1 <> 0 Then
                    VarVals(1) = (VarVals(1) - Session("VarsInGrid")(rr).MeanYr1) / Session("VarsInGrid")(rr).StdDevYr1
                Else
                    VarVals(1) = 0
                End If
                If Session("VarsInGrid")(rr).Alert > Session("VarsInGrid")(rr).Goal Then
                    VarVals(1) = -1 * VarVals(1)
                End If
                If Session("bAbsolute") Then
                    grd_TableDisplay.Rows(rr).Cells(10).Text = "<a href=""javascript:onPointClick4(" & CStr(rr) & ", 10)"">" & CStr(Round(VarVals(1), 2)) & "</a>"
                Else
                    grd_TableDisplay.Rows(rr).Cells(10).Text = "" 'jrs 2013/07/01 Round(Session("VarsInGrid")(rr).StdDev, 2)
                End If
                grd_TableDisplay.Rows(rr).Cells(11).Text = Round(VarVals(1), 2)

                'Round at the end
                grd_TableDisplay.Rows(rr).Cells(1).Text = "<a href=""javascript:onPointClick2(" & CStr(rr) & ", 1)"">" & CStr(Round(Val(grd_TableDisplay.Rows(rr).Cells(1).Text), 2)) & "</a>"
            End If
        Next rr

        'jrs 2013/06/14
        UpdateSummaryRow()

        'Weigthed Average on Risk Contributions
        'LoadWeightAvgCol 1
        LoadWeightAvgRisk()
        'jrs 2013/07/02
        If Session("bAbsolute") Then
            'jrs 2010/07/21
            'Weigthed Average on Standar Deviation
            LoadWeightAvgCol(10) 'jrs 2013/05/13 3
        End If

        grd_TableDisplay.Visible = True

        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("GDPPCP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False,
            bGroups, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"), MyFileforDisplay, Session("dfGDPPCP" + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay("GDPPCP", -1, cmb_Country.Text, cmb_Country.SelectedIndex + 1, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True,
            bGroups, 1, VarVals, 1, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
        End If
        txtIncome.Text = Round(VarVals(1), 2)

        If Session("bAbsolute") Then
            ColorGridAbs()
        Else
            ColorGridRel(CSng(txtIncome.Text))
            'Weigthed Average on Standar Deviation
            LoadWeightAvgCol(10) 'jrs 2013/05/13 3
        End If

        'jrs 2021/03/22
        If Session("FileForDisplay" + CStr(cboFiles.SelectedIndex)) Is Nothing And Not (MyFileforDisplay Is Nothing) Then
            Session("FileForDisplay" + CStr(cboFiles.SelectedIndex)) = MyFileforDisplay
        End If

    End Sub

    Private Sub UpdateSummaryRow()
        Dim rr As Integer
        'jrs 2010/02/11
        Dim iRank As Integer

        'jrs 2010/02/11
        rr = grd_TableDisplay.Rows.Count - 1
        'jrs 2013/06/12 *** begin ***
        If Session("PerfRiskGroup") Then
            grd_TableDisplay.Rows(rr).Cells(5).Text = AggRank(rr) 'jrs 2010/07/14 "N/A"
            iRank = grd_TableDisplay.Rows(rr).Cells(5).Text
            'grd_TableDisplay.Rows(rr).Cells(1).Text = AggVal(rr) 'jrs 2010/07/14 "N/A"
        Else
            grd_TableDisplay.Rows(rr).Cells(5).Text = "<a href=""javascript:onPointClick3(" & CStr(rr) & ", 5)"">" & CStr(Session("VarsInGrid")(rr).VarValsGet(cmb_Country.SelectedIndex + 1).Index) & "</a>"
            iRank = Session("VarsInGrid")(rr).VarValsGet(cmb_Country.SelectedIndex + 1).Index
            'grd_TableDisplay.Rows(rr).Cells(1).Text = Session("VarsInGrid")(rr).VarValsGet(cmb_Country.SelectedIndex + 1).value
        End If
        '.CellFontBold = True
        If iRank <= 40 Then
            grd_TableDisplay.Rows(rr).Cells(4).BackColor = Color.Red
        ElseIf iRank > NR - 40 Then
            grd_TableDisplay.Rows(rr).Cells(4).BackColor = Color.Lime
        Else
            grd_TableDisplay.Rows(rr).Cells(4).BackColor = Color.Yellow
        End If
        'jrs 2013/06/12 *** end ***

        'jrs 2010/07/13
        grd_TableDisplay.Rows(rr).Cells(6).Text = Session("TotWeight")
        'jrs 2010/07/23

    End Sub

    'Private Sub grd_TableDisplay_GetCellProp(ByVal sender As Object, ByVal e As TMSWebControls.CellPropEventArgs) Handles grd_TableDisplay.GetCellProp
    '    If e.Column = 0 Then
    '        Select Case e.Row
    '            Case 0, 6, 13, 19
    '                e.Font.Bold = True
    '        End Select
    '    End If
    'End Sub

    Private Sub mnuGroups_Click()
        Session("lstCountry.SelCount") = 0
        If Not Session("PerfRiskGroup") Then
            cswmWebMenu.Groups("mnuGroups").Caption = "Using Groups"
            lbl_countries.Text = "Groups"
            LoadGroups()
        Else
            cswmWebMenu.Groups("mnuGroups").Caption = "Using Countries/&Regions"
            lbl_countries.Text = "Countries or Regions"
            LoadRegions()
        End If
        cmb_Country.SelectedIndex = 0   ' Display First Item
        Session("PerfRiskGroup") = Not Session("PerfRiskGroup")
        cboFiles_SelectedIndexChanged(Me, New EventArgs)

        'jrs 2011/05/06
        If Session("PerfRiskGroup") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                Support.FormManager.Show(Me, "frm_PerfRiskAnalysis.aspx?Group=" & cmb_Country.SelectedValue)
            Else
                Support.FormManager.Show(Me, "frm_PerfRiskAnalysis.aspx?Group=" & Session("MMGroup"))
            End If
        Else
            Support.FormManager.Show(Me, "frm_PerfRiskAnalysis.aspx?Country=AF")
        End If
    End Sub

    Private Sub LoadGroups()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        cmb_Country.Items.Clear()
        Dim db As String
        Dim valtbl As DataTable
        db = Session("IFs_MDB2Active")
        SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "GroupName", valtbl)
        Dim idx As Integer = 0
        While Not valtbl.Rows.Count = idx
            cmb_Country.Items.Add(valtbl.Rows(idx).Item("GroupName"))
            lstCountry.Items.Add(valtbl.Rows(idx).Item("GroupName"))
            idx = idx + 1
        End While

        'Preselect lstFiles
        If Session("lstCountry.SelCount") > 0 Then
            For i = 0 To Session("lstCountry.SelCount") - 1
                idx = Session("RiskRegs")(i)
                lstCountry.Items(idx).Selected = True
            Next
        End If

    End Sub

    Private Sub cmb_Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
        If Not cmb_Country.Visible Then Exit Sub

        If Cmb_Year.SelectedIndex = -1 Then Exit Sub

        'jrs 2005/11/30
        Session("RiskcmbCountryIdx") = cmb_Country.SelectedIndex

        LoadValues()
        grd_TableDisplay.Visible = True

        'if selected file is not preselected reset
        If Session("lstCountry.SelCount") > 0 Then
            Dim idx As Integer
            If Not lstCountry.Items(Session("RiskcmbCountryIdx")).Selected Then
                For i = 0 To Session("lstCountry.SelCount") - 1
                    idx = Session("RiskRegs")(i)
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
        Session("RiskcmbYearIdx") = Cmb_Year.SelectedIndex

        LoadAllCtryValues(False)
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
                gLoadForDisplayNET(sName, idx, sDim1, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, False, False, Session("PerfRiskGroup"),
            1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
            Else
                gLoadForDisplay(sName, idx, sDim1, RegInd, sDim2, -1, sDim3, -1, "", cboFiles.SelectedIndex, True, True, Session("PerfRiskGroup"),
                1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, sDim1, sDim2, sDim3, Session("PerfRiskGroup"), cboFiles.SelectedIndex, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        Else
            gLoadComp(Session("COUNTE"), sFormulaDet, sName, sDim1, "", "", "Percent",
            -1, Session("PerfRiskGroup"), cboFiles.SelectedIndex, False, False, f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
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
                gLoadForDisplayNET("POP", POPidx, sDim1, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("PerfRiskGroup"),
            1, POPs, 1, 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"))
            Else
                gLoadForDisplay("POP", POPidx, sDim1, RegInd, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("PerfRiskGroup"),
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
    Private Sub DisplayGraph(ByVal currRow As Integer)
        Dim sDevProFun As String = ""
        Dim iDevProFunIdx As Integer = 0
        sDevProFun = Session("VarsInGrid")(currRow).FunctionName
        If sDevProFun > "" Then
            Session("sDevProFun") = sDevProFun
            iDevProFunIdx = SearchFunctionNameIdx(sDevProFun, 1, TFCount)
            Session("iDevProFunIdx") = iDevProFunIdx
            Session("DPGDPPCP") = txtIncome.Text
            Dim posStart As Integer, posEnd As Integer
            posStart = InStr(grd_TableDisplay.Rows(currRow).Cells(1).Text, ">") + 1
            posEnd = InStr(posStart, grd_TableDisplay.Rows(currRow).Cells(1).Text, "<")
            Session("DPFunVal") = CSng(Mid(grd_TableDisplay.Rows(currRow).Cells(1).Text, posStart, posEnd - posStart))
            Session("RefreshGraph") = True
            Session("DevProReg") = cmb_Country.SelectedValue
            Session("DevProYr") = Cmb_Year.SelectedValue
            Support.FormManager.Show(Me, "frm_FuncGraph.aspx")
        End If
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        If Session("PerfRiskGroup") = False Then
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

    'Subroutine to Load the Values
    'into Risk Profile Values table
    Private Sub LoadGralValues(ByVal bOnlyLoadGrid As Boolean)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'jrs 2009/11/19
        Dim dbTbl As String
        Dim Tbl As DataTable
        Dim dbW As String, TblW As DataTable
        If Not bOnlyLoadGrid Then
            'Open Table Func to look for Std Dev
            dbTbl = Session("PutFdrvRun") & "TablFunc.mdb"
            SetDataTablefromAccess(dbTbl, "SELECT * FROM TablFunc", "Function Name", Tbl)
            'Open GlobalParameters table to load default weights
            dbW = Session("IFs_MDB2Active")
            'jrs 2013/05/30 Remember to ask Barry to index these 2 tables: GlobalParametersMult and *Hist, for field Comments/Source
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                SetDataTablefromAccess(dbW, "SELECT * FROM GlobalParametersMult WHERE ParameterName = ""govriskweight"" ORDER BY Dimension", "Comments/Source", TblW)
            Else
                SetDataTablefromAccess(dbW, "SELECT * FROM GlobalParametersMultHist WHERE ParameterName = ""govriskweight"" ORDER BY Dimension", "Comments/Source", TblW)
            End If
        End If
        Dim tblWidx As Integer = 0

        'jrs 2005/05/13
        Dim curRow As Integer
        curRow = 2 'jrs 2013/05/13 1
        'Load Rows
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Governance: Security"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If

        'Instability
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Instability</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 0.1
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 0.05
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Instability (2005) Linear"
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 1
            Session("VarsInGrid")(curRow).VarName = "SFINSTABALL"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("SFINSTABALL").Index
            Session("VarsInGrid")(curRow).bAsc = False
            Session("VarsInGrid")(curRow).bUseRelEval = False
            'jrs 2010/07/13
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "Instability", "Value")
        End If
        'jrs 2013/06/14 grd_TableDisplay.Rows(curRow).Cells(6).Text = Session("VarsInGrid")(curRow).Weight
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        'Internal war
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Internal war</a>"
        If Not bOnlyLoadGrid Then
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 0.1
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 0.05
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Internal War (2005) Linear"
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 1
            Session("VarsInGrid")(curRow).VarName = "SFINTLWARALL"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("SFINTLWARALL").Index
            Session("VarsInGrid")(curRow).bAsc = False
            Session("VarsInGrid")(curRow).bUseRelEval = False
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "InternalWar", "Value")
        End If

        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        '*************************************
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Governance: Capacity"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If

        curRow = curRow + 1
        'Corr = 10/(1+exp(1.1209-0.0729*GDPPCP)), R^2 = 0.6296
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Corruption</a>"

        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 3
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 7
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Governance Corruption (2010) Logit"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 10
            Session("VarsInGrid")(curRow).VarName = "GOVCORRUPT"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("GOVCORRUPT").Index
            Session("VarsInGrid")(curRow).bAsc = True
            Session("VarsInGrid")(curRow).Description = "Corruption in governance is represented by bureaucrats using positions of power " &
            "for personal gain.  This can happen at all levels of government, and has strong negative impacts on economic growth, " &
            "as well as stability.  In this IFs form, lower numbers signal more intense corruption."
            Session("VarsInGrid")(curRow).Literature = "-Mauro, Paolo. 1995. 'Corruption and Growth.' The Quarterly Journal of Economics 110(3) (August): 681-712." &
            "<br>" & "-Mo, Pak Hung. 2001. 'Corruption and Economic Growth.' Journal of Comparative Economics 29(1) (March): 66-79. doi:10.1006/jcec.2000.1703." &
            "<br>" & "-Shihata, Ibrahim F. I. 1996. 'Corruption - A General Review with an Emphasis on the Role of the World Bank.' Dickinson Journal of International Law 15: 451." &
            "<br>" & "-Tanzi, Vito. 1998. 'Corruption Around the World: Causes, Consequences, Scope, and Cures.' Staff Papers - International Monetary Fund 45(4) (December): 559-594." &
            "<br>" & "-World Bank. WBI Governance & Anti-Corruption. www.worldbank.org/wbi/governance."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "Corruption", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        'Effectiveness
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Effectiveness</a>"

        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 1.5
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 2.5
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Governance Effectiveness (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 5
            Session("VarsInGrid")(curRow).VarName = "GOVEFFECT"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("GOVEFFECT").Index
            Session("VarsInGrid")(curRow).bAsc = True
            Session("VarsInGrid")(curRow).Description = "Effectiveness in governance can be seen in the degree to which laws, " &
            "policies and social stability can be maintained through state institutions and actors. Effectiveness requires " &
            "strong state legitimacy, bureaucratic institutions and infrastructure.  Lower numbers indicate less effective governance."
            'jrs 2010/07/13
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "Effectiveness", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        '******************************************
        'jrs 2012/01/05
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Governance: Inclusion"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If

        'Democracy
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Democracy</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 7
            Session("VarsInGrid")(curRow).AlertRel = 0.5
            Session("VarsInGrid")(curRow).Goal = 14
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Democracy (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 20
            Session("VarsInGrid")(curRow).VarName = "DEMOCPOLITY"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("DEMOCPOLITY").Index
            Session("VarsInGrid")(curRow).bAsc = True
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "Democracy", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        'Freedom
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Freedom</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 5
            Session("VarsInGrid")(curRow).AlertRel = 0.5
            Session("VarsInGrid")(curRow).Goal = 11
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Freedom (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 2
            Session("VarsInGrid")(curRow).Max = 14
            Session("VarsInGrid")(curRow).VarName = "FREEDOM"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("FREEDOM").Index
            Session("VarsInGrid")(curRow).bAsc = True
            'jrs 2010/07/13
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "Freedom", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        'Gender Empowerment Meassure
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Gender Empowerment Measure</a>"
        If Not bOnlyLoadGrid Then
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 0.4
            Session("VarsInGrid")(curRow).AlertRel = 0.2
            Session("VarsInGrid")(curRow).Goal = 1
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus GEM(2010) Logit"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 1
            Session("VarsInGrid")(curRow).VarName = "GEM"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("GEM").Index
            Session("VarsInGrid")(curRow).bAsc = True
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "GEM", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        '*** Risk Driver: Population ***
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Risk Driver: Population"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If

        'Youth Bulge
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Youth Bulge</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 47.5
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 35
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Youth Bulge (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 100
            Session("VarsInGrid")(curRow).VarName = "YTHBULGE"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("YTHBULGE").Index
            Session("VarsInGrid")(curRow).bAsc = False
            Session("VarsInGrid")(curRow).bFormula = True
            Session("VarsInGrid")(curRow).FormulaSet("a*100")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("a", "YTHBULGE")
            Session("VarsInGrid")(curRow).Description = "The Youth Bulge is a measure of size of the population " &
            "aged 15-29 relative to the size of the adult population (15+).  If the size of this population is " &
            "quite large, it has been theorized and demonstrated empirically that domestic stability is more " &
            "difficult to maintain.  One example of this is the Iranian Revolution of the early 1980s, where the " &
            "Youth Bulge was over 50%."
            Session("VarsInGrid")(curRow).Literature = "-Goldstone, JA. 2001. 'Demography, environment, and security.' Environmental conflict: 84-108." &
            "<br>" & "---. 2002. 'Population and Security: How Demographic Change Can Lead to Violent Conflict..' Journal of International Affairs 56(1): 3-23." &
            "<br>" & "-Goldstone, Jack. 'The New Population Bomb.'" &
            "<br>" & "-Urdal, H. 2004. 'The devil in the demographics: the effect of youth bulges on domestic armed conflict, 1950-2000.' Social Development Papers: Conflict and Reconstruction Paper 14." &
            "<br>" & "---. 2006. 'A clash of generations? Youth bulges and political violence.' International Studies Quarterly 50(3): 607-629." &
            "<br>" & "-Ware, H. 2004. 'Pacific instability and youth bulges: the devil in the demography and the economy.' Paper delivered at the 12 th Biennial Conference of the Australian Population Association,, 15-17." &
            "<br>" & "-Yousef, CTM. 'Youth in the Middle East and North Africa: Demography, Employment, and Conflict.' Youth Explosion in Developing World Cities."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "YouthBulge", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        'Elderly Bulge
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Elderly Bulge</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 16
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 10
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Elderly Bulge (2010) Linear"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 100
            Session("VarsInGrid")(curRow).VarName = "POPGT65"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("POPGT65").Index
            Session("VarsInGrid")(curRow).bAsc = False
            Session("VarsInGrid")(curRow).bPopPct = True
            Session("VarsInGrid")(curRow).Description = "The Elderly Bulge measures the relative size of the old population " &
            "(aged 65 or older) to the total population.  This can be a strain on country-level performance, as the size " &
            "of the working population may not be large enough to support the aging population.  In order to correct this problem, " &
            "countries are forced to do one of two thing:  either cut benefits to the old, or allow for increased migration " &
            "to support the previously established benefit programs."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "ElderlyBulge", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        'Urbanization Rate
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Urbanization Rate</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 5
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 3
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Urban Growth (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")

            Session("VarsInGrid")(curRow).Min = -10
            Session("VarsInGrid")(curRow).Max = 10
            Session("VarsInGrid")(curRow).VarName = "POPURBGR"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("POPURBGR").Index
            Session("VarsInGrid")(curRow).bAsc = False
            Session("VarsInGrid")(curRow).Description = "The Urbanization Rate is the percent of the total " &
            "population that moves to urban areas in any given year.  This is theorized to lead to " &
            "changes in country stability, as large numbers of previously rural populations begin to " &
            "populate urban areas, many moving into unstable slums.  In addition, this measure may " &
            "indicate broader societal changes, as previously rural systems of production break down. "
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "UrbRate", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        '*** Risk Driver: Environment ***
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Risk Driver: Environment"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If
        'Water use
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Water use/Renewable</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 0.4
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 0.2
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 90
            Session("VarsInGrid")(curRow).VarName = "WATUSE/WATRES"
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Water use/renewable (2010) Linear"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).bFormula = True
            Session("VarsInGrid")(curRow).FormulaSet("a/b")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("a", "WATUSE")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("b", "watres")
            Session("VarsInGrid")(curRow).bUseRelEval = False
            Session("VarsInGrid")(curRow).Description = "Water Stress is a measure of the total fresh water consumption " &
            "relative to the amount of renewable fresh water reserves.  If this number growth too high, a country " &
            "may find fresh water resources to become dear, thus leading to a host of potential other problems."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "WatUseRenew", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max
        'Climate Change
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Climate Change"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = -5
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 5
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).Min = -30
            Session("VarsInGrid")(curRow).Max = 30
            Session("VarsInGrid")(curRow).VarName = "ENVYLCHG"
            Session("VarsInGrid")(curRow).FunctionName = ""
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).bUseRelEval = False
            Session("VarsInGrid")(curRow).Description = "A potentially important transition brought " &
            "about by climate change is either an increase or a decrease in overall agricultural " &
            "productivity levels.  If countries experience decreases in overall agricultural productivity, " &
            "this potentially leads to increased malnutrition and instability along with a growing reliance " &
            "on trade to procure food sources."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "ClimateChange", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max


        '*** Risk Driver: International ***
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Risk Driver: International"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If
        'Power Transition
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Power Transition"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
        End If

        '*** Peformance: Economy ***
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Peformance: Economy"
        If Not bOnlyLoadGrid Then
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If

        'Poverty Level
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Poverty Level</a>"
        If Not bOnlyLoadGrid Then
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 0.2
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 0.03
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Poverty Level (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 1
            Session("VarsInGrid")(curRow).VarName = "INCOMELT190LN" 'jrs 2016/10/12 changed to 190
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = False
            Session("VarsInGrid")(curRow).bPopPort = True
            Session("VarsInGrid")(curRow).Description = "The population who live on less than 1.25$ per day as a " &
            "portion of the total population of a country is one measure of absolute poverty.  " &
            "Countries with high levels of absolute poverty are not able to provide basic social safety " &
            "nets for their population, thus indicating a low level of overall wealth and poor governance."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "PovertyLevel", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        'Inequality
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Inequality</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 0.45
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 0.35
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Inequality (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 1
            Session("VarsInGrid")(curRow).VarName = "GINIDOM"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).Description = "One way to measure inequality is to explore " &
            "the distribution of income through the Gini coefficient.  This measure shows the degree " &
            "to which total country-level income is distributed evenly or unevenly.  If countries have " &
            "high levels of income inequality, it may indicate a very stratified society where stability " &
            "either is enforced through a strong police-state, or where social cleaves lead to political upheaval."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "Inequality", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max
        'Resource Export Dependence
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Resource Export Dependence</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 20
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 5
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 150
            Session("VarsInGrid")(curRow).VarName = "XSres/GDP*100"
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Resource Export Dependance (2010) Linear"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).bFormula = True
            Session("VarsInGrid")(curRow).FormulaSet("(V1+V2+V3)/G*100")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("V1", "XS")
            Session("VarsInGrid")(curRow).f.disVar.setDataDet2S("V1", "Agriculture")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("V2", "XS")
            Session("VarsInGrid")(curRow).f.disVar.setDataDet2S("V2", "Energy")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("V3", "XS")
            Session("VarsInGrid")(curRow).f.disVar.setDataDet2S("V3", "Materials")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("G", "GDP")
            Session("VarsInGrid")(curRow).bUseRelEval = False
            Session("VarsInGrid")(curRow).Description = "Resource Dependence in Exports is a measure of a country's reliance on " &
            "natural resources for income relative to their total income.  As natural resources are finite, and are exposed " &
            "to market volatility, excess reliance on these for a country's overall GDP can pose the potential for future " &
            "instability.  Additionally, this 'resource curse' can further squeeze out other exports, as it artificially " &
            "inflates the value of the export currency."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "ResExpDependence", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max
        'Rate of per capita Growth
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Rate of per capita Growth</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 1.5
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 3
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Rate of per capita Growth (2010) Linear"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = -100
            Session("VarsInGrid")(curRow).Max = 100
            Session("VarsInGrid")(curRow).VarName = "GDPR-POPR"
            Session("VarsInGrid")(curRow).VarIdx = -1
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).bFormula = True
            Session("VarsInGrid")(curRow).FormulaSet("a-b")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("a", "GDPR")
            Session("VarsInGrid")(curRow).f.disVar.setDataDetS("b", "POPR")
            Session("VarsInGrid")(curRow).Description = "The growth in GDP is a measure of the change in overall consumption, " &
            "investment and trade by different sectors in a country.  With low growth rates comes low government income, " &
            "low investment and the potential for a vicious cycle of depletion."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "RatePCGrowth", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        '*** Performance: Health ***
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Performance: Health"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If
        'Infant Mortality
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Infant Mortality</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 50
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 15
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Infant Mortality (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 500
            Session("VarsInGrid")(curRow).VarName = "INFMOR"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).GoalAbs = 10
            Session("VarsInGrid")(curRow).Description = "Infant Mortality is the measure of children who die within " &
            "the first year of life out of 1,000 people in the total population.  This is a perennial measure " &
            "of state instability, as high levels indicate a fundamental inability to provide basic health, " &
            "infrastructure and governance services."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "InfantMortality", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max
        'Life Expectancy
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Life Expectancy</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 60
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 70
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Life Expectancy (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 125
            Session("VarsInGrid")(curRow).VarName = "LIFEXP"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).Description = "The average number of years that a person lives in a given " &
            "country is the Life Expectancy.  A high level of longevity indicates a country that has experienced " &
            "long-term stability of government services and strong economic growth.  A low level of life-span " &
            "highlights a country that may suffer from persistent disease, lack of government service and conflict."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "LifeExpectancy", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max
        'Malnutrition
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Malnutrition</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 10
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 3
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Children Malnutrition (2010) Logit"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 100
            Session("VarsInGrid")(curRow).VarName = "MALNCHP"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).Description = "Malnutrition is a measure of the quality and quantity of caloric " &
            "intake for a country.  In this case, our measure explores total percentage of the population that " &
            "suffer from malnutrition.  Countries with high levels of malnutrition have poor economic growth, may suffer " &
            "from disease and do not typically have access to quality education.  Bad governance is one driver of malnutrition."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "Malnutrition", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max
        'HIV Prevalence Rate
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">HIV Prevalence Rate</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 1.5
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 0.2
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 100
            Session("VarsInGrid")(curRow).VarName = "HIVRATE"
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus HIV Rate (2005) Linear"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item(Session("VarsInGrid")(curRow).VarName).Index
            Session("VarsInGrid")(curRow).bAsc = Session("VarsInGrid")(curRow).Alert < Session("VarsInGrid")(curRow).Goal
            Session("VarsInGrid")(curRow).bUseRelEval = False
            Session("VarsInGrid")(curRow).Description = "The total percent of a population in a country with " &
            "HIV/AIDS is an indication of a county's potential for overall economic growth and stability." &
            "High overall levels of this disease indicate potential lower levels of productivity, increased " &
            "levels of medical spending and the potential for these trends to carry on into the future, " &
            "if the disease is not contained."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "HIVPrevRate", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        '*** Performance: Education ***
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "Performance: Education"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
        End If
        'Primary Net Enrollment
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Primary Net Enrollment</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 60
            Session("VarsInGrid")(curRow).AlertRel = 2
            Session("VarsInGrid")(curRow).Goal = 80
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Primary Net Enrollment (2010) Linear"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 100
            Session("VarsInGrid")(curRow).VarName = "EDPRIENRN"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("EDPRIENRN").Index
            Session("VarsInGrid")(curRow).bAsc = True
            Session("VarsInGrid")(curRow).Description = "Primary Net Enrollment rates measure the percent of eligible primary " &
            "school students who attend classes.  High levels of enrollment indicate stable societies, with government " &
            "programs positively promoting human capital formation.  Low levels of enrollment indicate a disintegrating " &
            "central authority that lacks funding to bring about full education."
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "PriNetEnroll", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        'Adult Education Years
        curRow = curRow + 1
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Adult Education Years</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = False
            Session("VarsInGrid")(curRow).Alert = 6 'jrs 2015/03/02  2
            Session("VarsInGrid")(curRow).AlertRel = 1
            Session("VarsInGrid")(curRow).Goal = 8 'jrs 2015/03/02 5
            Session("VarsInGrid")(curRow).GoalRel = 0
            Session("VarsInGrid")(curRow).FunctionName = "GDP/Capita (PPP 2005) Versus Adult Education (2010) Log"
            Session("VarsInGrid")(curRow).StdDev = SeekValue(Tbl, Session("VarsInGrid")(curRow).FunctionName, "StdDev")
            Session("VarsInGrid")(curRow).Min = 0
            Session("VarsInGrid")(curRow).Max = 20
            Session("VarsInGrid")(curRow).VarName = "EDYRSAG15"
            Session("VarsInGrid")(curRow).VarIdx = IfsVars.Item("EDYRSAG15").Index
            Session("VarsInGrid")(curRow).bAsc = True
            Session("VarsInGrid")(curRow).Description = "Adult Education Years is a measure of the total amount of " &
            "time spent in formal education for the population aged 15 and above.  High levels of this " &
            "variable indicate long term stability within a country with relatively good government programs " &
            "to participate people in formal education.  Low levels indicate persistent instability and a lack of a central authority. "
            Session("VarsInGrid")(curRow).Weight = SeekValue(TblW, "AdultEdYears", "Value")
        End If
        grd_TableDisplay.Rows(curRow).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(curRow).VarName &
            """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(curRow).Weight & """/>"

        grd_TableDisplay.Rows(curRow).Cells(8).Text = Session("VarsInGrid")(curRow).StdDev
        grd_TableDisplay.Rows(curRow).Cells(2).Text = Session("VarsInGrid")(curRow).Min
        grd_TableDisplay.Rows(curRow).Cells(3).Text = Session("VarsInGrid")(curRow).Max

        '*** Summary Measure
        curRow = curRow + 2
        grd_TableDisplay.Rows(curRow).Cells(0).Text = "<a href=""javascript:onPointClick(" & CStr(curRow) & ")"">Summary Measure</a>"
        If Not bOnlyLoadGrid Then
            'jrs 2010/02/10
            Session("VarsInGrid")(curRow) = New DisplayVariable
            Session("VarsInGrid")(curRow).text = grd_TableDisplay.Rows(curRow).Cells(0).Text
            Session("VarsInGrid")(curRow).bTitle = True
            Session("VarsInGrid")(curRow).bAsc = False 'jrs 2013/06/12 changed from true to use GOVRISK
        End If
        'jrs 2013/06/12
        ''jrs 2010/07/21
        'Session("VarsInGrid")(curRow).Description = "The Summary Measure of the Value column is a weighted average of the Ranks for each variable, " & _
        '"<br>" & "and the Summary Measure of the Global Rank column is the ordinal position of the country when sorting all countries " & _
        '"<br>" & "based on the Value column."

        UpdateAlertsAndGoals()

    End Sub

    'jrs 2010/02/10
    Private Sub UpdateAlertsAndGoals()

        grd_TableDisplay.Visible = False
        Dim rr As Integer, SD As String, col As Integer
        For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
            'Check for titles
            If Not Session("VarsInGrid")(rr).bTitle Then
                'Alert 
                If Session("bAbsolute") Then
                    grd_TableDisplay.Rows(rr).Cells(9).Text = Session("VarsInGrid")(rr).Alert
                End If
                'Goal 
                If Session("bAbsolute") Then
                    grd_TableDisplay.Rows(rr).Cells(7).Text = Session("VarsInGrid")(rr).Goal
                End If

                'jrs 2013/05/14
                'Standard Error
                If Session("bAbsolute") Then
                    col = 11
                Else
                    col = 12
                End If
                If grd_TableDisplay.Rows(rr).Cells(col).Text > "" Then
                    SD = grd_TableDisplay.Rows(rr).Cells(col).Text
                Else
                    SD = ""
                End If
                'jrs 2013/07/01
                If Session("bAbsolute") Then
                    SD = "<a href=""javascript:onPointClick4(" & CStr(rr) & ", 10)"">" & SD & "</a>"
                End If
                col = 10
                grd_TableDisplay.Rows(rr).Cells(col).Text = SD
            End If
        Next rr

        'jrs 2013/05/14
        rr = grd_TableDisplay.Rows.Count - 1
        If Session("bAbsolute") Then
            col = 11
            'jrs 2013/07/01
            SD = "<a href=""javascript:onPointClick4(" & CStr(rr) & ", 10)"">" & grd_TableDisplay.Rows(rr).Cells(col).Text & "</a>"
        Else
            col = 12
            SD = grd_TableDisplay.Rows(rr).Cells(col).Text
        End If
        col = 10
        grd_TableDisplay.Rows(rr).Cells(col).Text = SD
        grd_TableDisplay.Visible = True
    End Sub

    Private Function AggRank(ByVal rowIdx As Integer) As Single
        'Initialize Function
        AggRank = 0
        'Read Rank for each country
        Dim c As Integer, cIdx As Integer, TotPop As Single
        TotPop = 0
        For c = 1 To Session("gCountGroupMembers")
            'Find pointer to country
            cIdx = IFsRegs.Item(Session("gGroupMembrs")(c)).Index
            'Average with Population Weigths.
            AggRank = AggRank + Session("VarsInGrid")(rowIdx).VarValsGet(cIdx).Index * Session("PopAll")(cIdx)
            'Total Population
            TotPop = TotPop + Session("PopAll")(cIdx)
        Next c
        If TotPop > 0 Then
            AggRank = Round(AggRank / TotPop, 0)
        Else
            AggRank = 0
        End If
    End Function

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

    Private Sub LoadWeightAvgRisk()
        Dim rr As Integer, WeightAvg As Single
        WeightAvg = 0
        For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
            If Session("VarsInGrid")(rr).VarName > "" Then
                WeightAvg = WeightAvg + Session("RiskIdx")(rr) * Session("VarsInGrid")(rr).Weight
            End If
        Next rr
        WeightAvg = WeightAvg / Session("TotWeight")
        rr = grd_TableDisplay.Rows.Count - 1
        grd_TableDisplay.Rows(rr).Cells(4).Text = "<a href=""javascript:onPointClick2(" & CStr(rr) & ", 4)"">" & CStr(Round(WeightAvg, 2)) & "</a>"
    End Sub

    Private Sub LoadWeightAvgCol(ByVal icol As Integer)

        Dim rr As Integer, WeightAvg As Single, locTotWeight As Single
        WeightAvg = 0
        locTotWeight = 0
        For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
            If Session("VarsInGrid")(rr).VarName > "" Then
                If grd_TableDisplay.Rows(rr).Cells(icol).Text > "" Then
                    WeightAvg = WeightAvg + Val(Replace(Replace(grd_TableDisplay.Rows(rr).Cells(icol).Text, "<a href=""javascript:onPointClick4(" & rr.ToString & ", 10)"">", ""), "</a>", "")) * Session("VarsInGrid")(rr).Weight
                    'jrs 2013/05/15
                    locTotWeight = locTotWeight + Session("VarsInGrid")(rr).Weight
                End If
            End If
        Next rr
        'jrs 2013/05/15 WeightAvg = WeightAvg / TotWeight
        If locTotWeight > 0 Then
            WeightAvg = WeightAvg / locTotWeight
        End If

        rr = grd_TableDisplay.Rows.Count - 1
        Dim col As Integer
        If Session("bAbsolute") Then
            col = 11
            grd_TableDisplay.Rows(rr).Cells(icol).Text = "<a href=""javascript:onPointClick4(" & rr.ToString & ", 10)"">" & Round(WeightAvg, 2) & "</a>"
        Else
            col = 12
            grd_TableDisplay.Rows(rr).Cells(icol).Text = Round(WeightAvg, 2)
        End If
        grd_TableDisplay.Rows(rr).Cells(col).Text = Round(WeightAvg, 2)
    End Sub

    Private Sub LoadAllCtryValues(ByVal bSaveMinMax As Boolean)
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim yr As Integer
        yr = Cmb_Year.Text
        Dim VarVals() As Single
        'jrs 2010/07/29
        Dim FirstYearToLoad As Integer, LastYearToLoad As Integer
        Dim j As Integer, fileIdx As Integer
        If bSaveMinMax Then
            ReDim VarVals(Session("LastYear") - Session("FirstYear") + 1)
            FirstYearToLoad = Session("FirstYear")
            LastYearToLoad = Session("LastYear")
            fileIdx = 1
        Else
            ReDim VarVals(1)
            FirstYearToLoad = yr
            LastYearToLoad = yr
            fileIdx = cboFiles.SelectedIndex
        End If

        Dim DC As DisplayColumn
        'jrs 2010/02/10
        Dim rr As Integer

        Dim i As Integer

        'jrs 2021/03/22
        Dim MyFileforDisplay As IStorage
        If Not Session("FileForDisplay" + CStr(fileIdx)) Is Nothing Then
            MyFileforDisplay = Session("FileForDisplay" + CStr(fileIdx))
        End If

        For i = 1 To NR
            'jrs 2010/07/30
            If bSaveMinMax Then
                'jrs 2010/07/14
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("POP", POPidx, GRPS(1, i), i, "", -1, "", -1, "", fileIdx, False, False,
                    False, 1, Session("PopAllYrs"), i, 1, FirstYearToLoad, LastYearToLoad, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), MyFileforDisplay, Session("dfPOP" + CStr(fileIdx)))
                Else
                    gLoadForDisplay("POP", POPidx, GRPS(1, i), i, "", -1, "", -1, "", fileIdx, True, True,
                    False, 1, Session("PopAllYrs"), i, 1, FirstYearToLoad, LastYearToLoad, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
                End If
            Else
                'jrs 2010/07/14
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET("POP", POPidx, GRPS(1, i), i, "", -1, "", -1, "", fileIdx, False, False,
                    False, 1, Session("PopAll"), 1, i, FirstYearToLoad, LastYearToLoad, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), MyFileforDisplay, Session("dfPOP" + CStr(fileIdx)))
                Else
                    gLoadForDisplay("POP", POPidx, GRPS(1, i), i, "", -1, "", -1, "", fileIdx, True, True,
                    False, 1, Session("PopAll"), 1, i, FirstYearToLoad, LastYearToLoad, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"))
                End If
            End If

            'jrs 2021/03/22
            If Not FilesCol.Contains("FileForDisplay" + CStr(fileIdx)) Then
                If Not MyFileforDisplay Is Nothing Then
                    FilesCol.Add(MyFileforDisplay, "FileForDisplay" + CStr(fileIdx))
                End If
            End If

            For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
                If Session("VarsInGrid")(rr).VarName > "" Then

                    'jrs 2010/07/30
                    If bSaveMinMax And i = 1 Then
                        Session("VarsInGrid")(rr).MinYr1 = 1000000.0#
                        Session("VarsInGrid")(rr).MaxYr1 = -1000000.0#
                    End If

                    If Session("VarsInGrid")(rr).bFormula Then
                        gLoadComp(1, Session("VarsInGrid")(rr).f.Formula_Renamed, Session("VarsInGrid")(rr).VarName, GRPS(1, i), "", "", "Percent",
                        -1, False, fileIdx, False, False, Session("VarsInGrid")(rr).f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                        False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                        Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                        Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol)
                        VarVals(1) = Session("PLOT")(yr - Session("FirstYear") + 1, 1)
                    Else
                        If Session("mnuSQLite") = "True" Then
                            'jrs 2021/03/22
                            If dfCol.Contains("df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx)) Then
                                Session("df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx)) = dfCol("df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx))
                            End If

                            gLoadForDisplayNET(Session("VarsInGrid")(rr).VarName, Session("VarsInGrid")(rr).VarIdx, GRPS(1, i), i, "Total", -1, "", -1, "", fileIdx, False, False,
                            False, 1, VarVals, 1, 1, FirstYearToLoad, LastYearToLoad, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"),
                            Session("CurrencyVal"), MyFileforDisplay, Session("df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx)))

                            'jrs 2021/03/22
                            If Not dfCol.Contains("df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx)) Then
                                If Session("df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx)) Is Nothing Then
                                    Dim MyDF1 As IDataframe
                                    dfCol.Add(MyDF1, "df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx))
                                Else
                                    dfCol.Add(Session("df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx)), "df" + Session("VarsInGrid")(rr).VarName + CStr(fileIdx))
                                End If
                            End If

                        Else
                            gLoadForDisplay(Session("VarsInGrid")(rr).VarName, Session("VarsInGrid")(rr).VarIdx, GRPS(1, i), i, "Total", -1, "", -1, "", fileIdx, True, True,
                            False, 1, VarVals, 1, 1, FirstYearToLoad, LastYearToLoad, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                            Session("CurrencyVal"))
                        End If
                    End If

                    DC = New DisplayColumn
                    DC.Index = i
                    DC.Value = VarVals(1)

                    If Session("VarsInGrid")(rr).bPopPct Or Session("VarsInGrid")(rr).bPopPort Then
                        'jrs 2010/07/30
                        If bSaveMinMax And Session("PopAll")(i) = 0 Then
                            Session("PopAll")(i) = Session("PopAllYrs")(1, i)
                            'Convert All Vals to %Pop
                            For j = 1 To Session("FinYear")(1)
                                VarVals(j) = VarVals(j) / Session("PopAllYrs")(j, i)
                                If Session("VarsInGrid")(rr).bPopPct Then
                                    VarVals(j) = VarVals(j) * 100
                                End If
                            Next j
                        End If
                        If Session("VarsInGrid")(rr).bPopPct Then
                            DC.Value = DC.Value / Session("PopAll")(i) * 100
                        Else
                            DC.Value = DC.Value / Session("PopAll")(i)
                        End If
                    End If

                    Session("VarsInGrid")(rr).VarValsSet(DC, i)

                    'Find Min Max for country R
                    '--------------------------
                    If bSaveMinMax Then
                        For j = 1 To Session("FinYear")(1)
                            'Min
                            If Session("VarsInGrid")(rr).MinYr1 > VarVals(j) Then
                                Session("VarsInGrid")(rr).MinYr1 = VarVals(j)
                            End If
                            'Max
                            If Session("VarsInGrid")(rr).MaxYr1 < VarVals(j) Then
                                Session("VarsInGrid")(rr).MaxYr1 = VarVals(j)
                            End If
                        Next j
                    End If
                End If
            Next rr
        Next i

        'Rank
        '----
        Dim VarCount As Integer, MinVal As Single, MaxVal As Single
        For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
            If Session("VarsInGrid")(rr).VarName > "" Then
                Rank(Session("VarsInGrid")(rr).VarValsGetAll, Session("VarsInGrid")(rr).bAsc, bSaveMinMax, MinVal, MaxVal)
                'jrs 2010/07/30
                '            If bSaveMinMax Then
                '                session("VarsInGrid")(rr).MinYr1 = MinVal
                '                session("VarsInGrid")(rr).MaxYr1 = MaxVal
                '            End If
                VarCount = VarCount + 1
            End If
        Next rr

        'Compute Avg Rank
        Dim AvgRank As Single, LastRR As Integer
        Dim AvgRankDC As DisplayColumn
        'jrs 2010/07/13
        Session("TotWeight") = 0
        For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
            If Session("VarsInGrid")(rr).VarName > "" Then
                Session("TotWeight") = Session("TotWeight") + Session("VarsInGrid")(rr).Weight
            End If
        Next rr

        UpdateAvgRank(bSaveMinMax)

        'jrs 2021/03/22
        If Session("FileForDisplay" + CStr(fileIdx)) Is Nothing And Not (MyFileforDisplay Is Nothing) Then
            Session("FileForDisplay" + CStr(fileIdx)) = MyFileforDisplay
        End If
        Session("dfCol") = dfCol
        Session("FilesCol") = FilesCol


    End Sub

    Private Sub UpdateAvgRank(ByVal bSaveMinMax As Boolean)
        Dim LastRR As Integer
        Dim AvgRank As Single
        Dim AvgRankDC As DisplayColumn
        LastRR = grd_TableDisplay.Rows.Count - 1
        For i = 1 To NR
            AvgRank = 0
            For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
                If Session("VarsInGrid")(rr).VarName > "" And Session("VarsInGrid")(rr).Max <> Session("VarsInGrid")(rr).Min Then 'jrs 2013/06/12 added Max <> Min
                    'jrs 2010/07/13 adding weight AvgRank = AvgRank + session("VarsInGrid")(rr).VarValsGet(i).Index
                    'jrs 2013/06/12 AvgRank = AvgRank + Session("VarsInGrid")(rr).VarValsGet(i).Index * Session("VarsInGrid")(rr).Weight
                    If Session("VarsInGrid")(rr).Alert > Session("VarsInGrid")(rr).Goal Then
                        AvgRank = AvgRank + (Session("VarsInGrid")(rr).VarValsGet(i).value - Session("VarsInGrid")(rr).Min) / (Session("VarsInGrid")(rr).Max - Session("VarsInGrid")(rr).Min) * Session("VarsInGrid")(rr).Weight
                    Else
                        AvgRank = AvgRank + (Session("VarsInGrid")(rr).VarValsGet(i).value - Session("VarsInGrid")(rr).Max) / (Session("VarsInGrid")(rr).Min - Session("VarsInGrid")(rr).Max) * Session("VarsInGrid")(rr).Weight
                    End If
                End If
            Next rr
            AvgRankDC = New DisplayColumn
            AvgRankDC.Index = i
            'jrs 2010/07/13 AvgRankDC.value = Round(AvgRank / VarCount, 2)
            AvgRankDC.Value = Round(AvgRank / Session("TotWeight"), 3)
            AvgRankDC.Value2 = GRPS(1, i)
            Session("VarsInGrid")(LastRR).VarValsSet(AvgRankDC, i)
        Next i
        Rank(Session("VarsInGrid")(LastRR).VarValsGetAll, Session("VarsInGrid")(LastRR).bAsc, bSaveMinMax, Session("VarsInGrid")(LastRR).MinYr1, Session("VarsInGrid")(LastRR).MaxYr1)
        'jrs 2011/01/10
        Session("VarsInGrid")(LastRR).Min = 0
        Session("VarsInGrid")(LastRR).Max = 1
    End Sub

    Private Sub Rank(ByRef x() As DisplayColumn, ByVal bAscending As Boolean, ByVal bSaveMinMax As Boolean, ByRef MinVal As Single, ByRef MaxVal As Single)
        Dim VarVals() As DisplayColumn
        ReDim VarVals(NR)
        Dim i As Integer
        'Make a copy of array
        '--------------------
        For i = 1 To NR
            VarVals(i) = New DisplayColumn
            VarVals(i).Index = x(i).Index
            VarVals(i).Value = x(i).Value
            VarVals(i).Value2 = GRPS(1, i) 'to sort by country name
        Next i
        'Sort by Value
        '-------------
        QuickSortObj(VarVals, 1, CInt(NR), bAscending, True)

        'Save Ranks
        '----------
        'jrs 2010/07/13
        'for ties use the first rank value
        Dim LastRank As Integer
        LastRank = 1
        For i = 1 To NR
            x(VarVals(i).Index).Index = LastRank 'i
            x(VarVals(i).Index).Index2 = i
            If i < NR Then
                If VarVals(i + 1).Value <> VarVals(i).Value Then
                    LastRank = i + 1
                End If
            End If
        Next i

        'jrs 2010/07/23
        If bSaveMinMax Then
            If bAscending Then
                'Min Value
                MinVal = VarVals(1).Value
                'Max Value
                MaxVal = VarVals(NR).Value
            Else
                'Min Value
                MinVal = VarVals(NR).Value
                'Max Value
                MaxVal = VarVals(1).Value
            End If
        End If
    End Sub

    Private Sub ColorGridAbs()
        With grd_TableDisplay
            Dim col As Integer
            .Visible = False
            col = 1 'jrs 2013/05/13 2

            Dim rr As Integer
            Dim myVal As Single, Alert As Single, Goal As Single
            Dim iStart As Integer, iEnd As Integer

            For rr = 3 To .Rows.Count - 3  'jrs 2013/05/13 used to start at 2
                If .Rows(rr).Cells.Count > 1 Then
                    If .Rows(rr).Cells(col).Text > "" Then
                        .Rows(rr).Cells(col).Font.Bold = True
                        'Remove the <a> tags
                        iStart = InStr(.Rows(rr).Cells(col).Text, ">") + 1
                        iEnd = InStr(iStart, .Rows(rr).Cells(col).Text, "<")
                        myVal = Val(Mid(.Rows(rr).Cells(col).Text, iStart, iEnd - iStart))
                        col = 9 'jrs 2013/05/13 4
                        Alert = Val(.Rows(rr).Cells(col).Text)
                        col = 7 'jrs 2013/05/13 5
                        Goal = Val(.Rows(rr).Cells(col).Text)
                        col = 1 'jrs 2013/05/13 2
                        If Alert > Goal Then
                            If myVal >= Alert Then
                                .Rows(rr).Cells(col).BackColor = Color.Red
                            ElseIf myVal > Goal Then
                                .Rows(rr).Cells(col).BackColor = Color.Yellow
                            Else
                                .Rows(rr).Cells(col).BackColor = Color.Lime
                            End If
                        Else
                            If myVal <= Alert Then
                                .Rows(rr).Cells(col).BackColor = Color.Red
                            ElseIf myVal < Goal Then
                                .Rows(rr).Cells(col).BackColor = Color.Yellow
                            Else
                                .Rows(rr).Cells(col).BackColor = Color.Lime
                            End If
                        End If
                    End If
                End If
            Next rr
            .Visible = True
        End With
    End Sub

    Private Sub ColorGridRel(ByVal GDPPCP As Single)
        Dim myVal As Single, EstVal As Single, SE As Single
        Dim Alert As Single, Goal As Single
        Dim Min As Single, Max As Single
        'Read GDPPCP to compute estimates
        Dim VarVals(1) As Single
        Dim yr As Integer
        yr = Cmb_Year.Text
        'jrs 2010/02/10
        Dim rr As Integer
        Dim AlertVal As Single, GoalVal As Single
        Dim GoalAbs As Single
        Dim col As Integer

        With grd_TableDisplay
            .Visible = False
            For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 changed from 1 to 2
                If Session("VarsInGrid")(rr).VarName > "" Then
                    col = 1 'jrs 2013/05/13 2
                    myVal = Val(Replace(Replace(grd_TableDisplay.Rows(rr).Cells(col).Text, "<a href=""javascript:onPointClick2(" & rr.ToString & ", 1)"">", ""), "</a>", ""))
                    col = 8
                    SE = Val(grd_TableDisplay.Rows(rr).Cells(col).Text)
                    col = 2 'jrs 2013/05/13 6
                    Min = Val(grd_TableDisplay.Rows(rr).Cells(col).Text)
                    col = 3 'jrs 2013/05/13 7
                    Max = Val(grd_TableDisplay.Rows(rr).Cells(col).Text)

                    If Session("VarsInGrid")(rr).bUseRelEval Then
                        XYTABL(Session("VarsInGrid")(rr).FunctionName, GDPPCP, EstVal, Session("TFuncPoints"), Session("XPoints"), Session("YPoints"), "", Me, Session("APoint"), Session("BPoints"))
                    End If

                    If Session("VarsInGrid")(rr).Alert < Session("VarsInGrid")(rr).Goal Then
                        col = 9 'jrs 2013/05/13 4
                        If Session("VarsInGrid")(rr).bUseRelEval Then
                            AlertVal = AMAX(Round(EstVal - Session("VarsInGrid")(rr).AlertRel * SE, 2), Min)
                        Else
                            AlertVal = Session("VarsInGrid")(rr).Alert
                        End If
                        grd_TableDisplay.Rows(rr).Cells(col).Text = AlertVal
                        col = 7 'jrs 2013/05/13 5
                        If Session("VarsInGrid")(rr).bUseRelEval Then
                            GoalVal = Amin(AMAX(Round(EstVal - Session("VarsInGrid")(rr).GoalRel * SE, 2), Min), Max)
                        Else
                            GoalVal = Session("VarsInGrid")(rr).Goal
                        End If
                        grd_TableDisplay.Rows(rr).Cells(col).Text = GoalVal
                        col = 1 'jrs 2013/05/13 2

                        If myVal <= AlertVal Then
                            grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Red
                        ElseIf myVal < GoalVal Then
                            grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Yellow
                        Else
                            grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Lime
                        End If
                    Else
                        col = 9 'jrs 2013/05/13 4
                        If Session("VarsInGrid")(rr).bUseRelEval Then
                            AlertVal = Amin(Round(EstVal + Session("VarsInGrid")(rr).AlertRel * SE, 2), Max)
                        Else
                            AlertVal = Session("VarsInGrid")(rr).Alert
                        End If
                        grd_TableDisplay.Rows(rr).Cells(col).Text = AlertVal
                        col = 7 'jrs 2013/05/13 5
                        If Session("VarsInGrid")(rr).bUseRelEval Then
                            GoalVal = AMAX(Amin(Round(EstVal + Session("VarsInGrid")(rr).GoalRel * SE, 2), Max), Min)
                        Else
                            GoalVal = Session("VarsInGrid")(rr).Goal
                        End If
                        grd_TableDisplay.Rows(rr).Cells(col).Text = GoalVal
                        col = 1 'jrs 2013/05/13 2

                        GoalAbs = Session("VarsInGrid")(rr).GoalAbs
                        If GoalAbs = 0 Then
                            If myVal >= AlertVal Then
                                grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Red
                            ElseIf myVal > GoalVal Then
                                grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Yellow
                            Else
                                grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Lime
                            End If
                        Else
                            If myVal >= AlertVal And myVal >= GoalAbs Then
                                grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Red
                            ElseIf myVal > GoalVal And myVal >= GoalAbs Then
                                grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Yellow
                            Else
                                grd_TableDisplay.Rows(rr).Cells(col).BackColor = Color.Lime
                            End If
                        End If
                    End If
                    'jrs 2013/05/14
                    If Session("VarsInGrid")(rr).bUseRelEval And SE <> 0 Then
                        col = 12 'Standar Errors from Estimation
                        grd_TableDisplay.Rows(rr).Cells(col).Text = Round((myVal - EstVal) / SE, 2)
                        'jrs 2013/07/01 now using coloring after loading alerts and goals so this needs to be reloaded
                        col = 10
                        If grd_TableDisplay.Rows(rr).Cells(col).Text = "" Then
                            grd_TableDisplay.Rows(rr).Cells(col).Text = Round((myVal - EstVal) / SE, 2)
                        End If

                    End If
                End If
            Next rr
            .Visible = True
        End With
    End Sub

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
        wmPopUp.ClearPixelImage = "blank.gif"
        wmPopUp.PopupIcon = "./images/Popup.gif"
        wmPopUp.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmPopUp.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmPopUp.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmPopUp.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmPopUp.SelectedPopupIcon = "./images/SelectedPopup.gif"
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
        cswmItem.Caption = "Cross-Sectional Relationship"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Provide Information"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Count"
        cswmItem.ID = "mnuShowCount"
        If Not Session("bAbsolute") Then
            cswmItem.Enabled = False
        End If

        wmPopUp.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp.ItemFont.PaddingLeft = 4

        CreateMenu1()

        CreateMenu2()

        CreateMenu3()

        CreateMenu4()
    End Sub


    Private Sub CreateMenu1()
        wmPopUp1.UserData = "jsolorza::976739975"
        wmPopUp1.ClearPixelImage = "blank.gif"
        wmPopUp1.PopupIcon = "./images/Popup.gif"
        wmPopUp1.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmPopUp1.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmPopUp1.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmPopUp1.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmPopUp1.SelectedPopupIcon = "./images/SelectedPopup.gif"
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
        wmPopUp2.ClearPixelImage = "blank.gif"
        wmPopUp2.PopupIcon = "./images/Popup.gif"
        wmPopUp2.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmPopUp2.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmPopUp2.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmPopUp2.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmPopUp2.SelectedPopupIcon = "./images/SelectedPopup.gif"
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


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display All Years and All Countries Ranked"


        wmPopUp2.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp2.ItemFont.PaddingLeft = 4

    End Sub

    Private Sub CreateMenu4()
        wmPopUp4.UserData = "jsolorza::976739975"
        wmPopUp4.ClearPixelImage = "blank.gif"
        wmPopUp4.PopupIcon = "./images/Popup.gif"
        wmPopUp4.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmPopUp4.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmPopUp4.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmPopUp4.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmPopUp4.SelectedPopupIcon = "./images/SelectedPopup.gif"
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
        wmPopUp3.ClearPixelImage = "blank.gif"
        wmPopUp3.PopupIcon = "./images/Popup.gif"
        wmPopUp3.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmPopUp3.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmPopUp3.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmPopUp3.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmPopUp3.SelectedPopupIcon = "./images/SelectedPopup.gif"
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
        If Session("PerfRiskGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        Session("TableDisplayParent") = "frm_PerfRiskAnalysis.aspx?" & sPar
        Select Case objEA.Item.Caption
            Case "Cross-Sectional Relationship"
                DisplayGraph(currRow)
            Case "Provide Information"
                mnuPopupCellInformation_Click(currRow)
            Case "Show Count"
                DisplayCount(currRow)
        End Select
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
        If Session("PerfRiskGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        Session("TableDisplayParent") = "frm_PerfRiskAnalysis.aspx?" & sPar
        Select Case objEA.Item.Caption
            Case "Show Over Time"
                If Request.Form("txtCol") = 1 Then
                    DisplayOverTime(currRow)
                Else
                    DisplayFormulaOverTime(currRow, Request.Form("txtCol"))
                End If
            Case "Display All Years and All Countries Ranked"
                If Request.Form("txtCol") = 1 Then
                    DisplayRankedCountries(currRow)
                Else
                    DisplayRankedCountryRisk(currRow, Request.Form("txtCol"))
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
        If Session("PerfRiskGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        Session("TableDisplayParent") = "frm_PerfRiskAnalysis.aspx?" & sPar
        Select Case objEA.Item.Caption
            Case "Display all country names by ranking"
                DisplayRankedCountryNames(currRow)
        End Select
    End Sub

    Private Sub mnuPopupCellInformation_Click(ByVal currRow As Integer)
        Dim msg As String
        If Session("bLastRow") Then
            'Display Information
            'frm_PerfRiskAnalysisInfo.Caption = "Performance Risk Analysis - Summary Measure"
            msg = "Higher Risk Index number is riskier; smaller Value number is riskier. <br><br>" & Session("VarsInGrid")(grd_TableDisplay.Rows.Count - 1).Description
        Else
            msg = DisplayInformation(currRow)
            msg = msg & DisplayAnnotation(currRow)
            msg = msg & DisplayLiterature(currRow)
        End If

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    Private Function DisplayInformation(ByVal currRow As Integer) As String
        Dim sMsg As String = ""
        Dim rr As Integer
        rr = currRow
        If rr < grd_TableDisplay.Rows.Count - 3 Then
            If Session("VarsInGrid")(rr).VarName > "" Then
                'Valence
                If Session("VarsInGrid")(rr).Alert < Session("VarsInGrid")(rr).Goal Then
                    'Less is Riskier
                    sMsg = "Smaller Value number is riskier; higher Risk Index number is riskier. Risk Index column is computed using the Max Level minus the Value number divided by the full range (Max - Min levels)."
                Else
                    'More is Riskier
                    sMsg = "Higher Value number is riskier; higher Risk Index number is riskier. Risk Index column is computed using the Value number minus the Min Level divided by the full range (Max - Min levels)."
                End If
                'Description
                sMsg = sMsg & "<br><br>" & Session("VarsInGrid")(rr).Description
            End If
        End If

        DisplayInformation = "Performance Risk Analysis - " & Session("VarsInGrid")(rr).text & "<br><br><br>" & sMsg
    End Function

    Private Function DisplayAnnotation(ByVal currRow As Integer) As String
        Dim sMsg As String = ""
        'jrs 2010/02/11
        Dim rr As Integer
        rr = currRow
        If rr < grd_TableDisplay.Rows.Count - 2 Then
            If Session("VarsInGrid")(rr).VarName > "" Then
                If Session("VarsInGrid")(rr).bUseRelEval Then
                    'Alert level is below
                    If Session("VarsInGrid")(rr).Alert < Session("VarsInGrid")(rr).Goal Then
                        sMsg = "All values below or equal to the Alert level of " & "<br>" &
                        Session("VarsInGrid")(rr).Alert & " if using the Absolute Evaluation option or " & "<br>" &
                        Session("VarsInGrid")(rr).AlertRel & " Standard Errors below the Value expected given the level of income of the country if using the Relative Evaluation option " &
                        "are shown in red." & "<br>" & "<br>"
                        sMsg = sMsg & "All values above the Alert level and below the Goal level of " & "<br>" &
                        Session("VarsInGrid")(rr).Goal & " if using the Absolute Evaluation option or " & "<br>"
                        If Session("VarsInGrid")(rr).GoalRel <> 0 Then
                            sMsg = sMsg & Abs(Session("VarsInGrid")(rr).GoalRel) & " Standard Error "
                        End If
                        If Session("VarsInGrid")(rr).GoalRel >= 0 Then
                            sMsg = sMsg & "below "
                        Else
                            sMsg = sMsg & "above "
                        End If
                        sMsg = sMsg & "the Value expected given the level of income of the country if using the Relative Evaluation option " &
                        "are shown in yellow." & "<br>" & "<br>"
                        sMsg = sMsg & "All values above or equal to the Goal level are show in green."
                    Else 'Alert level is above
                        sMsg = "All values above or equal to the Alert level of " & "<br>" &
                        Session("VarsInGrid")(rr).Alert & " if using the Absolute Evaluation option or " & "<br>" &
                        Session("VarsInGrid")(rr).AlertRel & " Standard Errors above the Value expected given the level of income of the country if using the Relative Evaluation option " &
                        "are shown in red." & "<br>" & "<br>"
                        sMsg = sMsg & "All values below the Alert level and above the Goal level of " & "<br>" &
                        Session("VarsInGrid")(rr).Goal & " if using the Absolute Evaluation option or " & "<br>"
                        If Session("VarsInGrid")(rr).GoalRel <> 0 Then
                            sMsg = sMsg & Abs(Session("VarsInGrid")(rr).GoalRel) & " Standard Error "
                        End If
                        If Session("VarsInGrid")(rr).GoalRel >= 0 Then
                            sMsg = sMsg & "above "
                        Else
                            sMsg = sMsg & "below "
                        End If
                        sMsg = sMsg & "the Value expected given the level of income of the country if using the Relative Evaluation option " &
                        "are shown in yellow." & "<br>" & "<br>"
                        sMsg = sMsg & "All values below or equal to the Goal level are show in green."
                    End If
                Else
                    'for now only instability and war not using Std Dev.
                    sMsg = "All values above or equal to the Alert level of " & "<br>" &
                    Session("VarsInGrid")(rr).Alert & " are shown in red." & "<br>"
                    sMsg = sMsg & "All values below the Alert level and above the Goal level of " & "<br>" &
                    Session("VarsInGrid")(rr).Goal & " are shown in yellow." & "<br>"
                    sMsg = sMsg & "All values below or equal to the Goal level are show in green."
                End If
            End If
        End If

        'jrs 2010/07/13
        DisplayAnnotation = "<br><br>" & sMsg
    End Function

    Private Function DisplayLiterature(ByVal currRow As Integer) As String
        Dim sMsg As String = ""
        Dim rr As Integer
        rr = currRow
        If rr < grd_TableDisplay.Rows.Count - 3 Then
            If Session("VarsInGrid")(rr).Literature > "" Then
                sMsg = Session("VarsInGrid")(rr).Literature
            End If
        End If

        If sMsg > "" Then
            DisplayLiterature = "<br><br>" & sMsg
        Else
            DisplayLiterature = ""
        End If
    End Function

    Private Sub DisplayCount(ByVal currRow As Integer)
        If Not Session("PerfRiskGroup") Then
            Dim rr As Integer
            Dim sMsg As String
            Dim i As Integer
            Dim AlertVal As Single, GoalVal As Single
            rr = currRow
            If rr < grd_TableDisplay.Rows.Count - 2 Then
                If Session("VarsInGrid")(rr).VarName > "" Then
                    'Counting red and greens
                    '----------------------
                    With Session("VarsInGrid")(rr)
                        .RedCount = 0
                        .GreenCount = 0
                        '                    grd_TableDisplay.col = 2
                        '                    AlertVal = grd_TableDisplay.text
                        '                    grd_TableDisplay.col = 3
                        '                    GoalVal = grd_TableDisplay.text
                        '                    grd_TableDisplay.col = 0

                        For i = 1 To NR
                            If .bAsc Then
                                'Count Reds
                                '----------
                                If .VarValsGet(i).value <= .Alert Then
                                    .RedCount = .RedCount + 1
                                End If
                                'Count Greens
                                '------------
                                If .VarValsGet(i).value >= .Goal Then
                                    .GreenCount = .GreenCount + 1
                                End If
                            Else
                                'Count Reds
                                '----------
                                If .VarValsGet(i).value >= .Alert Then
                                    .RedCount = .RedCount + 1
                                End If
                                'Count Greens
                                '------------
                                If .VarValsGet(i).value <= .Goal Then
                                    .GreenCount = .GreenCount + 1
                                End If
                            End If
                        Next i
                    End With


                    sMsg = "There are " & Session("VarsInGrid")(rr).RedCount & " countries in alert level (red)."
                    sMsg = sMsg & "<br>" & "There are " & NR - Session("VarsInGrid")(rr).RedCount - Session("VarsInGrid")(rr).GreenCount & " countries in moderate risk level (yellow)."
                    sMsg = sMsg & "<br>" & "There are " & Session("VarsInGrid")(rr).GreenCount & " countries not at risk (green)."
                End If
            End If

            sMsg = "Performance Risk Analysis - " & Session("VarsInGrid")(rr).text & "<br><br><br>" & sMsg
            LoadMessage(sMsg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
    End Sub

    Private Sub DisplayRankedCountries(ByVal currRow As Integer)
        'JRS 09/12/02
        'Subroutine to display variable for All Countries Ranked
        'Dim bShow As Boolean

        Dim sVar As String
        Dim sDim2 As String, sDim3 As String
        Dim bPopPct As Boolean
        bPopPct = False
        'jrs 2010/02/11
        Dim bPopPort As Boolean
        'jrs 2010/02/04
        Dim bAsc As Boolean
        'jrs 2010/02/11
        Dim rr As Integer
        rr = currRow
        If rr < grd_TableDisplay.Rows.Count - 2 Then
            sVar = Session("VarsInGrid")(rr).VarName
            bAsc = Session("VarsInGrid")(rr).bAsc
            sDim2 = "Total"
            bPopPct = Session("VarsInGrid")(rr).bPopPct
            bPopPort = Session("VarsInGrid")(rr).bPopPort
        End If

        If sVar > "" Then
            'Load Variable over time and Across scenarios
            If Session("VarsInGrid")(rr).bFormula Then
                LoadAllOverTime(sVar, sDim2, sDim3, bPopPct, bAsc, bPopPort,
                True, Session("VarsInGrid")(rr).f.Formula, Session("VarsInGrid")(rr).f)
            Else
                LoadAllOverTime(sVar, sDim2, sDim3, bPopPct, bAsc, bPopPort,
                False, "", Nothing)
            End If
        End If

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

    Private Sub DisplayFormulaOverTime(ByVal currRow As Single, ByVal currCol As Single)
        'Subroutine to display a Formula over time

        Dim ff As New Formula
        If currRow < grd_TableDisplay.Rows.Count - 1 Then
            BuildRiskFormula(ff, currRow, currCol)
        Else
            BuildTotRiskFormula(ff, currCol)
        End If


        If ff.Formula_Renamed > "" Then
            Dim sName As String
            If currCol = 4 Then 'jrs 2013/05/13 used to be 1
                sName = "RISKIDX"
            Else
                sName = "STDDEV"
            End If
            'Load Variable over time and Across scenarios
            LoadOverTime(sName, "Total", "", False, False,
            True, ff.Formula_Renamed, ff)
        End If

    End Sub

    Private Sub BuildRiskFormula(ByRef fff As Object, ByVal currRow As Integer, ByVal currCol As Integer)
        Dim sFormula As String, currTerm As String, subFormula As String
        sFormula = ""
        Dim rr As Integer, i As Integer
        With grd_TableDisplay
            rr = currRow
            'jrs 2011/01/10 change MaxYr1 to Max and MinYr1 to Min
            If Session("VarsInGrid")(rr).VarName > "" And ((Session("VarsInGrid")(rr).Max <> Session("VarsInGrid")(rr).Min And currCol = 4) Or
            (Session("VarsInGrid")(rr).StdDevYr1 <> 0 And currCol = 10)) Then 'jrs 2013/05/13 changed 1 to 4, 3 to 10
                sFormula = sFormula & "(X"
                If rr < 10 Then
                    currTerm = "0"
                Else
                    currTerm = ""
                End If
                currTerm = currTerm & Trim(CStr(rr))
                'Risk Contribution
                If currCol = 4 Then 'jrs 2013/05/13 1
                    If Session("VarsInGrid")(rr).Alert > Session("VarsInGrid")(rr).Goal Then
                        sFormula = sFormula & currTerm & "-" & Format(Session("VarsInGrid")(rr).Min, "0.000000") & ")/" &
                        Format(Session("VarsInGrid")(rr).Max - Session("VarsInGrid")(rr).Min, "0.000000")
                    Else
                        sFormula = sFormula & currTerm & "-" & Format(Session("VarsInGrid")(rr).Max, "0.000000") & ")/" &
                        Format(Session("VarsInGrid")(rr).Min - Session("VarsInGrid")(rr).Max, "0.000000")
                    End If
                Else 'Standard Deviation
                    sFormula = sFormula & currTerm & "-" & Format(Session("VarsInGrid")(rr).MeanYr1, "0.000000") & ")/" &
                    Format(Session("VarsInGrid")(rr).StdDevYr1, "0.000000")
                    If Session("VarsInGrid")(rr).Alert > Session("VarsInGrid")(rr).Goal Then
                        sFormula = sFormula & "*(-1)"
                    End If
                End If
                'Check if it's a formula
                If Session("VarsInGrid")(rr).bFormula Then
                    subFormula = Session("VarsInGrid")(rr).f.Formula
                    For i = 1 To Session("VarsInGrid")(rr).f.disVar.disSize
                        subFormula = Replace(subFormula, Session("VarsInGrid")(rr).f.disVar.display(i), "X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i))
                    Next i
                    sFormula = Replace(sFormula, "X" & currTerm, "(" & subFormula & ")")
                ElseIf Session("VarsInGrid")(rr).bPopPct Or Session("VarsInGrid")(rr).bPopPort Then
                    subFormula = "X" & currTerm & "A/X" & currTerm & "B"
                    If Session("VarsInGrid")(rr).bPopPct Then
                        subFormula = subFormula & "*100"
                    End If
                    sFormula = Replace(sFormula, "X" & currTerm, "(" & subFormula & ")")
                End If

                'Load Formula
                Dim ff As New Formula
                ff.init(sFormula)

                If Session("VarsInGrid")(rr).bFormula Then
                    For i = 1 To Session("VarsInGrid")(rr).f.disVar.disSize
                        ff.disVar.setDataDetS("X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i), Session("VarsInGrid")(rr).f.disVar.displayDet(i))
                        ff.disVar.setDataDet1S("X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i), Session("VarsInGrid")(rr).f.disVar.displayDet1(i))
                        ff.disVar.setDataDet2S("X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i), Session("VarsInGrid")(rr).f.disVar.displayDet2(i))
                        ff.disVar.setDataDet3S("X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i), Session("VarsInGrid")(rr).f.disVar.displayDet3(i))
                    Next i
                ElseIf Session("VarsInGrid")(rr).bPopPct Or Session("VarsInGrid")(rr).bPopPort Then
                    ff.disVar.setDataDetS("X" & currTerm & "A", Session("VarsInGrid")(rr).VarName)
                    ff.disVar.setDataDet1S("X" & currTerm & "A", cmb_Country.Text)
                    ff.disVar.setDataDet2S("X" & currTerm & "A", "Total")
                    ff.disVar.setDataDet3S("X" & currTerm & "A", "")

                    ff.disVar.setDataDetS("X" & currTerm & "B", "POP")
                    ff.disVar.setDataDet1S("X" & currTerm & "B", cmb_Country.Text)
                    ff.disVar.setDataDet2S("X" & currTerm & "B", "")
                    ff.disVar.setDataDet3S("X" & currTerm & "B", "")
                Else
                    ff.disVar.setDataDetS("X" & currTerm, Session("VarsInGrid")(rr).VarName)
                    ff.disVar.setDataDet1S("X" & currTerm, cmb_Country.Text)
                    ff.disVar.setDataDet2S("X" & currTerm, "Total")
                    ff.disVar.setDataDet3S("X" & currTerm, "")
                End If
                'At this point formula is ready in ff
                fff = ff
            Else
                fff = Nothing
            End If
        End With
    End Sub

    Private Sub BuildTotRiskFormula(ByRef fff As Object, ByVal currCol As Integer)
        Dim sFormula As String, currTerm As String, subFormula As String
        sFormula = "("
        Dim rr As Integer, i As Integer
        With grd_TableDisplay
            For rr = 2 To .Rows.Count - 3 'jrs 2013/05/14 used to start from 1
                'jrs 2011/01/10 change MaxYr1 to Max and MinYr1 to Min
                If Session("VarsInGrid")(rr).VarName > "" And ((Session("VarsInGrid")(rr).Max <> Session("VarsInGrid")(rr).Min And currCol = 4) Or
                (Session("VarsInGrid")(rr).StdDevYr1 <> 0 And currCol = 10)) Then 'jrs 2013/05/13 1 changed to 4, 3 to 10
                    If rr > 3 Then 'jrs 2013/05/14 used to be > 2
                        sFormula = sFormula & "+"
                    End If
                    sFormula = sFormula & "(X"
                    If rr < 10 Then
                        currTerm = "0"
                    Else
                        currTerm = ""
                    End If
                    currTerm = currTerm & Trim(CStr(rr))
                    'Risk Contribution
                    If currCol = 4 Then 'jrs 2013/05/13 used to be 1
                        If Session("VarsInGrid")(rr).Alert > Session("VarsInGrid")(rr).Goal Then
                            sFormula = sFormula & currTerm & "-" & Format(Session("VarsInGrid")(rr).Min, "0.000000") & ")*" &
                            Session("VarsInGrid")(rr).Weight & "/" & Format(Session("VarsInGrid")(rr).Max - Session("VarsInGrid")(rr).Min, "0.000000")
                        Else
                            sFormula = sFormula & currTerm & "-" & Format(Session("VarsInGrid")(rr).Max, "0.000000") & ")*" &
                            Session("VarsInGrid")(rr).Weight & "/" & Format(Session("VarsInGrid")(rr).Min - Session("VarsInGrid")(rr).Max, "0.000000")
                        End If
                    Else 'Standard Deviation
                        sFormula = sFormula & currTerm & "-" & Format(Session("VarsInGrid")(rr).MeanYr1, "0.000000") & ")*" &
                        Session("VarsInGrid")(rr).Weight & "/" & Format(Session("VarsInGrid")(rr).StdDevYr1, "0.000000")
                        If Session("VarsInGrid")(rr).Alert > Session("VarsInGrid")(rr).Goal Then
                            sFormula = sFormula & "*(-1)"
                        End If
                    End If
                    'Check if it's a formula
                    If Session("VarsInGrid")(rr).bFormula Then
                        subFormula = Session("VarsInGrid")(rr).f.Formula_Renamed
                        For i = 1 To Session("VarsInGrid")(rr).f.disVar.disSize
                            subFormula = Replace(subFormula, Session("VarsInGrid")(rr).f.disVar.display(i), "X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i))
                        Next i
                        sFormula = Replace(sFormula, "X" & currTerm, "(" & subFormula & ")")
                    ElseIf Session("VarsInGrid")(rr).bPopPct Or Session("VarsInGrid")(rr).bPopPort Then
                        subFormula = "X" & currTerm & "A/X" & currTerm & "B"
                        If Session("VarsInGrid")(rr).bPopPct Then
                            subFormula = subFormula & "*100"
                        End If
                        sFormula = Replace(sFormula, "X" & currTerm, "(" & subFormula & ")")
                    End If
                End If
            Next rr
            sFormula = sFormula & ")/" & Session("TotWeight")
            'Load Formula
            Dim ff As New Formula
            ff.init(sFormula)
            For rr = 2 To .Rows.Count - 3 'jrs 2012/10/11 'jrs 2013/05/14 used to start at 1
                If Session("VarsInGrid")(rr).VarName > "" And ((Session("VarsInGrid")(rr).Max <> Session("VarsInGrid")(rr).Min And currCol = 4) Or
                (Session("VarsInGrid")(rr).StdDevYr1 <> 0 And currCol = 10)) Then 'jrs 2013/05/13 changed from 1 to 4, 3 to 10
                    If rr < 10 Then
                        currTerm = "0"
                    Else
                        currTerm = ""
                    End If
                    currTerm = currTerm & Trim(CStr(rr))

                    If Session("VarsInGrid")(rr).bFormula Then
                        For i = 1 To Session("VarsInGrid")(rr).f.disVar.disSize
                            ff.disVar.setDataDetS("X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i), Session("VarsInGrid")(rr).f.disVar.displayDet(i))
                            ff.disVar.setDataDet1S("X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i), Session("VarsInGrid")(rr).f.disVar.displayDet1(i))
                            ff.disVar.setDataDet2S("X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i), Session("VarsInGrid")(rr).f.disVar.displayDet2(i))
                            ff.disVar.setDataDet3S("X" & currTerm & Session("VarsInGrid")(rr).f.disVar.display(i), Session("VarsInGrid")(rr).f.disVar.displayDet3(i))
                        Next i
                    ElseIf Session("VarsInGrid")(rr).bPopPct Or Session("VarsInGrid")(rr).bPopPort Then
                        ff.disVar.setDataDetS("X" & currTerm & "A", Session("VarsInGrid")(rr).VarName)
                        ff.disVar.setDataDet1S("X" & currTerm & "A", cmb_Country.Text)
                        ff.disVar.setDataDet2S("X" & currTerm & "A", "Total")
                        ff.disVar.setDataDet3S("X" & currTerm & "A", "")

                        ff.disVar.setDataDetS("X" & currTerm & "B", "POP")
                        ff.disVar.setDataDet1S("X" & currTerm & "B", cmb_Country.Text)
                        ff.disVar.setDataDet2S("X" & currTerm & "B", "")
                        ff.disVar.setDataDet3S("X" & currTerm & "B", "")
                    Else
                        ff.disVar.setDataDetS("X" & currTerm, Session("VarsInGrid")(rr).VarName)
                        ff.disVar.setDataDet1S("X" & currTerm, cmb_Country.Text)
                        ff.disVar.setDataDet2S("X" & currTerm, "Total")
                        ff.disVar.setDataDet3S("X" & currTerm, "")
                    End If
                End If
            Next rr
            'At this point formula is ready in ff
            fff = ff
        End With
    End Sub

    Private Sub DisplayRankedCountryRisk(ByVal currRow As Integer, ByVal currCol As Integer)
        'Subroutine to display Risk index for All Countries Ranked
        'Dim bShow As Boolean


        Dim ff As New Formula
        If currRow < grd_TableDisplay.Rows.Count - 1 Then
            BuildRiskFormula(ff, currRow, currCol)
        Else
            BuildTotRiskFormula(ff, currCol)
        End If


        If Not (ff Is Nothing) Then
            Dim sName As String
            If currCol = 4 Then 'jrs 2013/05/13 changed from 1 to 4
                sName = "RISKIDX"
            Else
                sName = "STDDEV"
            End If
            'Load Variable over time for all regions ranked
            LoadAllOverTime(sName, "Total", "", False, False, False,
            True, ff.Formula_Renamed, ff)
        End If

    End Sub

    Private Sub DisplayRankedCountryNames(ByVal currRow As Integer)
        'JRS 09/12/02
        'Subroutine to display Ranked Country Names

        Dim sVar As String
        'jrs 2010/01/29
        Dim VarVals() As DisplayColumn
        'jrs 2010/02/11
        Dim rr As Integer
        rr = currRow
        If rr < grd_TableDisplay.Rows.Count - 2 Then
            sVar = Session("VarsInGrid")(rr).VarName
            If sVar > "" Then
                VarVals = Session("VarsInGrid")(rr).VarValsGetAll
            End If
        ElseIf rr = grd_TableDisplay.Rows.Count - 1 Then
            sVar = "Summary Measure"
            VarVals = Session("VarsInGrid")(rr).VarValsGetAll
        End If

        If sVar > "" Then
            Dim sTitle As String
            sTitle = grd_TableDisplay.Rows(rr).Cells(0).Text
            Dim pos1 As Integer, pos2 As Integer
            pos1 = InStr(sTitle, ">")
            pos2 = InStr(pos1, sTitle, "<")
            sTitle = Mid(sTitle, pos1 + 1, pos2 - pos1 - 1)
            Dim RankIdx As Integer
            'grd_TableDisplay.col = 5 'jrs 2013/05/13 9
            'jrs 2010/07/13 RankIdx = grd_TableDisplay.text
            RankIdx = VarVals(cmb_Country.SelectedIndex + 1).Index2
            'jrs 2013/06/12
            Dim myColor As Object
            If txtRow.Value < grd_TableDisplay.Rows.Count - 1 Then
                myColor = grd_TableDisplay.Rows(txtRow.Value).Cells(1).BackColor
            Else
                myColor = grd_TableDisplay.Rows(txtRow.Value).Cells(4).BackColor
            End If
            Session("PerfRiskColor") = myColor

            'Load Ranked Country Names in Table
            LoadCountryNames(VarVals, sTitle, RankIdx, myColor)
        End If

    End Sub

    Private Sub LoadCountryNames(ByVal VarVals() As DisplayColumn, ByVal sTitle As String, ByVal RankIdx As Integer, ByVal myColor As Object)
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

        Support.FormManager.Show(Me, "frm_Table.aspx")
    End Sub

    Private Sub UpdateWeights()
        Session("TotWeight") = 0
        Dim rr As Integer
        For rr = 2 To grd_TableDisplay.Rows.Count - 3 'jrs 2013/05/13 used to start at 1
            If Session("VarsInGrid")(rr).VarName > "" Then
                Session("VarsInGrid")(rr).Weight = Request.Form("txtWg" & Session("VarsInGrid")(rr).VarName)
                Session("TotWeight") = Session("TotWeight") + Session("VarsInGrid")(rr).Weight

                grd_TableDisplay.Rows(rr).Cells(6).Text = "<input name=""txtWg" & Session("VarsInGrid")(rr).VarName &
                """ style=""width: 50px; font-size: 6pt;"" type=""text"" value=""" & Session("VarsInGrid")(rr).Weight & """/>"
            End If
        Next rr

        UpdateAvgRank(False)

        UpdateSummaryRow()

        LoadWeightAvgRisk()

        LoadWeightAvgCol(10)
    End Sub

    Private Sub mnuAbsolute_Click()
        Session("bAbsolute") = Not Session("bAbsolute")
        With grd_TableDisplay
            '.row = 0
            .Visible = False
            If Session("bAbsolute") Then
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuDispOptAbs").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuDispOptRel").IconImage = "blank.gif"
                wmPopUp.Groups(0).Items("mnuShowCount").Enabled = True
                'jrs 2013/05/13
                .Rows(0).Cells(2).Text = "Evaluation: Absolute"
                .Rows(1).Cells(9).Text = "Standard Deviation"
            Else
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuDispOptAbs").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuDispOptRel").IconImage = "checkbox.gif"
                wmPopUp.Groups(0).Items("mnuShowCount").Enabled = False
                'jrs 2013/05/13
                .Rows(0).Cells(2).Text = "Evaluation: Relative"
                .Rows(1).Cells(9).Text = "Standard Error"
            End If
        End With

        UpdateAlertsAndGoals()
        If Session("bAbsolute") Then
            ColorGridAbs()
        Else
            ColorGridRel(CSng(txtIncome.Text))
        End If

        'jrs 2013/05/14
        LoadWeightAvgCol(10)

        grd_TableDisplay.Visible = True

    End Sub

    Protected Sub cmdSce_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSce.Click
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

                Session("RiskcboFilesIdx") = cboFiles.SelectedIndex
                LoadAllCtryValues(False)
                LoadValues()
            End If

            lstFiles.Visible = False
            cboFiles.Visible = True
        Else
            Session("lstFiles.SelCount") = 0
            Session("lstCountry.SelCount") = 0
            For i = 0 To lstCountry.Items.Count - 1
                If lstCountry.Items(i).Selected Then
                    ReDim Preserve Session("RiskRegs")(Session("lstCountry.SelCount"))
                    Session("RiskRegs")(Session("lstCountry.SelCount")) = i
                    Session("lstCountry.SelCount") = Session("lstCountry.SelCount") + 1
                    If Session("lstCountry.SelCount") = 1 Then
                        ChgIdx = i
                    End If

                End If
            Next
            If Not lstCountry.Items(cmb_Country.SelectedIndex).Selected Then
                cmb_Country.SelectedIndex = ChgIdx
                Session("RiskcmbCountryIdx") = cmb_Country.SelectedIndex
                'LoadValues()
                If Session("PerfRiskGroup") = True Then
                    Support.FormManager.Show(Me, "frm_PerfRiskAnalysis.aspx?Group=" & cmb_Country.SelectedValue)
                Else
                    Support.FormManager.Show(Me, "frm_PerfRiskAnalysis.aspx?Country=" & IFsRegs.Item(cmb_Country.SelectedValue).ISO3166_2Code)
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
End Class
