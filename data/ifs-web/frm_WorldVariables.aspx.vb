Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Imports IFs.Data

Partial Class frm_WorldVariables
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
        cmb_VarName.EnableViewState = False
        lstVarName.EnableViewState = False
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            '
            '   Load Variable Name Combo Box
            '   ----------------------------
            Session("RecordSourceDef") = "Map"
            Session("PYear") = 1

            LoadFiles()
            'jrs 2026/01/26 changed default to base
            If Session("DefaultFile") = Nothing Then
                Session("DefaultFile") = 1
                cboFiles.SelectedIndex = 1
            Else
                cboFiles.SelectedIndex = Session("DefaultFile")
            End If
            'jrs 09/15/04
            Session("RefreshGraph") = "true"
            'jrs 09/30/04
            Session("mapParent") = "frm_WorldVariables.aspx"

            'jrs 09/30/04
            Select Case Session("WorldVaropt")
                Case "optMap"
                    optMap.Checked = True
                    optGini.Checked = False
                    optLorenz.Checked = False
                    optHistogram.Checked = False
                Case "optGini"
                    optMap.Checked = False
                    optGini.Checked = True
                    optLorenz.Checked = False
                    optHistogram.Checked = False
                Case "optLorenz"
                    optMap.Checked = False
                    optGini.Checked = False
                    optLorenz.Checked = True
                    optHistogram.Checked = False
                Case "optHistogram" 'JAM 2013-01-12
                    optMap.Checked = False
                    optGini.Checked = False
                    optLorenz.Checked = False
                    optHistogram.Checked = True
            End Select
            'JRS 2005/02/23
            If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
                Session("VarSelCheck") = Nothing
            End If

            'jrs 2016/01/06
            If IsNothing(Session("WVHistoryPlusForecast")) Then
                If Session("FirstYear") = FirstYearHistory And Session("LastYear") = Session("LastYearForecast") Then
                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                        Session("FirstYear") = FirstYearForecast
                    Else
                        Session("LastYear") = LastYearHistory
                    End If
                End If
            End If

            'jrs 2011/06/07
            Session.Remove("LorenzX_Year")
            'jrs 2015/04/06
            If Session("WVHistoryPlusForecast") Is Nothing Then
                Session("WVHistoryPlusForecast") = False
                Session("bFillHisWV") = False
                Session("bFillHisOnlyHolesWV") = True
            End If
        Else 'jrs 2006/02/24
            DefaultFile = Session("DefaultFile")
            sVarDim2 = Session("frm_WorldVariables.sVarDim2")
        End If

        LoadVars()


        'jrs 09/17/04
        'jrs 2011/05/30 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

    Dim DefaultFile As Integer

#End Region

    Dim sVarDim2 As String
    'jrs 2006/05/17
    Dim sVarDim3 As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        'jrs 2012/01/17
        'cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        'AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        'jrs 2011/05/04
        Session("DisplayParent") = "frm_MainMenu.aspx"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"
        Session("LorenzParent") = "frm_WorldVariables.aspx"
        'jrs 2014/03/19
        Session("DisplayParent") = "frm_WorldVariables.aspx"

        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            'jrs 2015/04/06
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "History plus Forecast"
            cswmGroup1.ID = "mnuHistFor"

            Dim cswmItem As Coalesys.WebMenu.Item
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "On"
            If Session("WVHistoryPlusForecast") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Off"
            If Not Session("WVHistoryPlusForecast") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Use Estimation to Complete Data"
            cswmGroup1.ID = "mnuFillHoles"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Full Estimation (Includes Extrapolation)"
            cswmItem.ID = "mnuEstimation"
            If Session("bFillHisWV") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Fill Holes Only (No extrapolation)"
            cswmItem.ID = "mnuEstimationOnlyHoles"
            If Session("bFillHisOnlyHolesWV") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmGroup1.ShowMenuBarItem = Session("WVHistoryPlusForecast")

        End If

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,6510);"
        'jrs 2006/12/21 cswmGroup1.OnMenuBarClick = "window.open('./WebHelp/ifshelp.htm#ifs_help_for_web_version/display/basic_report_web_version.htm', '__webCshStub');"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

    End Sub

    Private Sub LoadFiles()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim fs 'As New Scripting.FileSystemObject
        Dim BASE 'As Scripting.File
        Dim currRun 'As Scripting.File

        fs = Server.CreateObject("Scripting.FileSystemObject")

        Dim sWorkingFile As String
        cboFiles.Items.Clear()
        Session("FilesSelected") = 1
        sWorkingFile = VB.Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))

        If Session("FirstYear") = FirstYearHistory And Session("LastYear") - Session("FirstYear") < 100 Then
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

        cboFiles.Items.Add(New ListItem("0 - " & "Working File, based on " & sWorkingFile))
        cboFiles.Items(cboFiles.SelectedIndex).Text = "0 - " & "Working File, based on " & sWorkingFile
        cboFiles.Items.Add(New ListItem("1 - " & BASE.Name))

        Dim sRunFile As String
        '' Returns filename with specified extension. If more than one *.ini
        '' file exists, the first file found is returned.
        sRunFile = Dir(Session("PutFdrvRun2") & "\*.run.db")

        Do While (sRunFile <> "")
            If UCase(sRunFile) <> UCase(BASE.Name) And UCase(sRunFile) <> UCase(sWorkingFile) And UCase(sRunFile) <> "WORKING.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") & "\" & sRunFile)
                'jrs 2022/04/08 added Or StandAloneMode
                If Session("IFsRunFiles").item(sRunFile).Visible Or StandAloneMode Then 'jrs 2005/06/27
                    cboFiles.Items.Add(New ListItem(Trim(Str(cboFiles.Items.Count)) & " - " & sRunFile))
                    Session("FilesSelected") = Session("FilesSelected") + 1
                    ReDim Preserve Session("RunFiles")(Session("FilesSelected"))
                    Session("RunFiles")(Session("FilesSelected")) = Session("PutFdrvRun2") & "\" & sRunFile
                    'jrs 2005/02/25
                    If Session("FilesSelected") > UBound(Session("ShortSce")) Then
                        ReDim Preserve Session("ShortSce")(Session("FilesSelected"))
                    End If
                    Dim k As Integer
                    k = getLongSceIdx(sRunFile)
                    If k > -1 Then
                        Session("ShortSce")(Session("FilesSelected")) = ShortSce(k)
                    Else
                        'jrs 2008/05/13 Session("ShortSce")(Session("FilesSelected")) = Left(sRunFile, 8)
                        Session("ShortSce")(Session("FilesSelected")) = TruncateSceFileName(sRunFile)
                    End If
                End If
            End If
            '    ' Call Dir again without arguments to return the next *.INI file in the
            '    ' same directory.
            sRunFile = Dir()
        Loop
    End Sub

    Private Sub cboFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiles.SelectedIndexChanged
        DefaultFile = cboFiles.SelectedIndex
        Session("DefaultFile") = DefaultFile

    End Sub

    Private Sub cmb_DIM1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_DIM1.SelectedIndexChanged
        lstDIM1.SelectedIndex = cmb_DIM1.SelectedIndex - 1
        Dim sVarName As String
        If InStr(lbl_VarName.Text, "-") > 0 Then
            sVarName = Mid(lbl_VarName.Text, 1, InStr(lbl_VarName.Text, "-") - 2)
        Else
            sVarName = cmb_VarName.SelectedValue
        End If

        If sVarDim2 = "" Then
            If InStr(cmb_DIM1.SelectedValue, "-") > 0 Then
                sVarDim2 = Mid(cmb_DIM1.SelectedValue, 1, InStr(cmb_DIM1.SelectedValue, "-") - 2)
            Else
                sVarDim2 = cmb_DIM1.SelectedValue
            End If
            'jrs 2006/05/17 Dim3
            Dim idx As Integer
            idx = IfsVars.Item(sVarName).Index
            If VDim3(idx) > 0 Then
                cmb_VarName.Enabled = False
                lstVarName.Visible = False
                cmb_DIM1.Visible = True
                lstDIM1.Visible = True
                cmb_DIM1.Items.Clear()
                lstDIM1.Items.Clear()
                cmb_DIM1.Items.Add("")
                Dim EleLoop As Integer
                'cdv 2021/02/02 ****START**** Same Changes as VDim2
                For EleLoop = 1 To MaxVal(VDim3(idx)) - 1
                    If GRPS$(VDim3(idx), EleLoop) <> "Male" And GRPS$(VDim3(idx), EleLoop) <> "Female" Then
                        cmb_DIM1.Items.Add(GRPS$(VDim3(idx), EleLoop))
                        lstDIM1.Items.Add(GRPS$(VDim3(idx), EleLoop))
                    ElseIf bMaleFirst Then
                        cmb_DIM1.Items.Add(GRPS$(VDim3(idx), EleLoop))
                        lstDIM1.Items.Add(GRPS$(VDim3(idx), EleLoop))
                    ElseIf GRPS$(VDim3(idx), EleLoop) = "Male" Then
                        cmb_DIM1.Items.Add("Female")
                        lstDIM1.Items.Add("Female")
                    Else
                        cmb_DIM1.Items.Add("Male")
                        lstDIM1.Items.Add("Male")
                    End If
                    'cdv 2021/02/02 ****END****
                Next EleLoop
                Session("frm_WorldVariables.sVarDim2") = sVarDim2
                Exit Sub
            End If
        Else
            sVarDim3 = cmb_DIM1.SelectedValue
        End If


        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        cmb_DIM1.Items.Clear()
        lstDIM1.Items.Clear()
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        'jrs 2015/04/06
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))
        Dim iRowPosition As Integer, iFirstYear As Integer, FinAll As Integer
        If Session("WVHistoryPlusForecast") Then
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

        'Load Data
        Dim DimNum As Integer
        Dim RunCount As Integer
        Dim ii As Integer
        Dim VARNUM As Integer
        VARNUM = IfsVars.Item(sVarName).Index
        DimNum = 1
        RunCount = MaxVal(DimNum) - 1
        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe
        For ii = 1 To RunCount
            Session("COUNTE") = Session("COUNTE") + 1
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sVarName, VARNUM, GRPS(1, ii), ii, sVarDim2, -1, sVarDim3, -1, "", DefaultFile, ii = 1, ii = RunCount, False, 1,
            Session("PLOT"), Session("COUNTE"), iRowPosition, iFirstYear, Session("LastYear"), 0, False,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            Else

                gLoadForDisplay(sVarName, VARNUM, GRPS(1, ii), ii, sVarDim2, -1, sVarDim3, -1, "", DefaultFile, ii = 1, ii = RunCount, False, 1,
                Session("PLOT"), Session("COUNTE"), iRowPosition, iFirstYear, Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), VARNUM, GRPS(1, ii), sVarDim2, sVarDim3, False, DefaultFile, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
            'jrs 2015/04/06
            If Session("WVHistoryPlusForecast") Then
                FinAll = Session("FinYear")(Session("COUNTE"))
                gLoadForDisplayHis(sVarName, GRPS(1, ii), -1, sVarDim2, -1, "", -1,
                False, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                False, True, Session("bFillHisWV"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                Session("LastYearForecast"), Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHolesWV"),
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                Session("FinYear")(Session("COUNTE")) = LastYearHistory - FirstYearHistory + FinAll 'jrs 2006/09/22
            End If
        Next ii
        'jrs 09/30/04
        Select Case True
            Case optMap.Checked
                Session("WorldVaropt") = "optMap"
            Case optGini.Checked
                Session("WorldVaropt") = "optGini"
            Case optLorenz.Checked
                Session("WorldVaropt") = "optLorenz"
            Case optHistogram.Checked 'JAM 2013-01-12
                Session("WorldVaropt") = "optHistogram"
        End Select
        Dim sURLDim2 As String
        If optMap.Checked Then
            'jrs 09/29/04
            '   f. Render "Map" into categories of data
            '   ----------------------------------------
            Session("MapChosenTable") = "Map"
            Session("MapFieldName") = "StoreData"
            'jrs 2008/03/28
            Session("MapVar") = sVarName & " - " & sVarDim2
            If sVarDim3 > "" Then
                Session("MapVar") = Session("MapVar") & " - " & sVarDim3
            End If
            'jrs 09/29/04
            Support.FormManager.Show(Me, "frm_mapViewer.aspx")
        ElseIf optHistogram.Checked Then 'JAM 2013-01-12
            'Dim sURLDim2 As String
            sURLDim2 = "&Dim2=" & sVarDim2
            If sVarDim3 > "" Then
                sURLDim2 = sURLDim2 & "&Dim3=" & sVarDim3
            End If
            'jrs 2013/02/21
            Session("PYear") = Nothing
            Session("GraphDisplayParent") = "frm_WorldVariables.aspx" 'JAM 2013-01-19
            Support.FormManager.Show(Me, "frm_Histogram.aspx?VarName=" & sVarName & "&File=" & Session("DefaultFile") & sURLDim2)
        Else

            'jrs 2011/05/31
            'Dim frm_DisplayMenu As New frm_DisplayMenu
            'frm_DisplayMenu.GiniSetup()
            'Dim IfsPars As New myParameters
            'IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"), _
            'Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"), _
            'Session("PutFdrvRun"), Session("CurrencySym"))

            GiniSetup(IfsPars, Session("COUNTE"), Me, Session("VARN"), Session("LastYear"), Session("FirstYear"),
            Session("PutFdrvRun"), Session("ActiveBase"), Session("RunFiles"), Session("bLoadForDisplaySuccess"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"), Session("bFillHisWV"), Session("buseAllHis"), Session("FinYearHis"),
            Session("LastYearForecast"), Session("bComputation"), Session("bFillHisOnlyHolesWV"),
            Session("LorenzX_Year"), Session("LorenzY_Year"), Session("PLOT"), Session("LorenzX"),
            Session("LorenzY"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("YAxisLabel"),
            Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))

            If optGini.Checked Then
                Support.FormManager.Show(Me, "frm_DisplayMenu.aspx")
            Else
                Session("isfromDispMenu") = False  'JAM 2013-01-13


                sURLDim2 = "&Dim2=" & sVarDim2
                If sVarDim3 > "" Then
                    sURLDim2 = sURLDim2 & "&Dim3=" & sVarDim3
                End If
                Support.FormManager.Show(Me, "frm_LorenzGraph.aspx?VarName=" & sVarName & "&File=" & Session("DefaultFile") & sURLDim2)
            End If
        End If
    End Sub

    Private Sub cmb_VarName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_VarName.SelectedIndexChanged
        'jrs 2006/05/17 Dim3
        sVarDim2 = ""
        Session("frm_WorldVariables.sVarDim2") = ""

        lstVarName.SelectedIndex = cmb_VarName.SelectedIndex - 1

        lbl_VarName.Text = cmb_VarName.SelectedValue
        Dim sVarName As String
        If InStr(lbl_VarName.Text, "-") > 0 Then
            sVarName = Mid(lbl_VarName.Text, 1, InStr(lbl_VarName.Text, "-") - 2)
        Else
            sVarName = cmb_VarName.SelectedValue
        End If
        Dim idx As Integer
        Dim EleLoop As Integer
        idx = IfsVars.Item(sVarName).Index
        If VDim2(idx) > 0 Then
            cmb_VarName.Enabled = False
            lstVarName.Visible = False
            cmb_DIM1.Visible = True
            lstDIM1.Visible = True
            cmb_DIM1.Items.Clear()
            lstDIM1.Items.Clear()
            cmb_DIM1.Items.Add("")
            'cdv 2021/02/02 ****START****
            '1. if the word isn't gendered, don't change anything
            '2. if the order is bMaleFirst, don't change anything
            '3. if the word is gendered, and the order is !bMaleFirst, flip the order

            For EleLoop = 1 To MaxVal(VDim2(idx)) - 1
                If GRPS(VDim2(idx), EleLoop) <> "Male" And GRPS(VDim2(idx), EleLoop) <> "Female" Then
                    cmb_DIM1.Items.Add(GRPS(VDim2(idx), EleLoop))
                    lstDIM1.Items.Add(GRPS(VDim2(idx), EleLoop))
                ElseIf bMaleFirst Then
                    cmb_DIM1.Items.Add(GRPS(VDim2(idx), EleLoop))
                    lstDIM1.Items.Add(GRPS(VDim2(idx), EleLoop))
                ElseIf GRPS(VDim2(idx), EleLoop) = "Male" Then
                    cmb_DIM1.Items.Add("Female")
                    lstDIM1.Items.Add("Female")
                Else
                    cmb_DIM1.Items.Add("Male")
                    lstDIM1.Items.Add("Male")
                End If
            Next EleLoop
            'cdv 2021/02/02 ****END****
            Exit Sub
        End If
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        'jrs 2015/04/06
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))
        Dim iRowPosition As Integer, iFirstYear As Integer, FinAll As Integer
        If Session("WVHistoryPlusForecast") Then
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

        'Load Data
        Dim DimNum As Integer
        Dim RunCount As Integer
        Dim ii As Integer
        DimNum = 1
        RunCount = MaxVal(DimNum) - 1
        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe
        For ii = 1 To RunCount
            Session("COUNTE") = Session("COUNTE") + 1
            If Session("mnuSQLite") = "True" Then

                gLoadForDisplayNET(sVarName, idx, GRPS(1, ii), ii, "", -1, "", -1, "", DefaultFile, ii = 1, ii = RunCount, False, 1,
            Session("PLOT"), Session("COUNTE"), iRowPosition, iFirstYear, Session("LastYear"), 0, False,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            Else
                gLoadForDisplay(sVarName, idx, GRPS(1, ii), ii, "", -1, "", -1, "", DefaultFile, ii = 1, ii = RunCount, False, 1,
                Session("PLOT"), Session("COUNTE"), iRowPosition, iFirstYear, Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, GRPS(1, ii), "", "", False, DefaultFile, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
            'jrs 2015/04/06
            If Session("WVHistoryPlusForecast") Then
                FinAll = Session("FinYear")(Session("COUNTE"))
                gLoadForDisplayHis(sVarName, GRPS(1, ii), -1, "", -1, "", -1,
                False, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                False, True, Session("bFillHisWV"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                Session("LastYearForecast"), Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHolesWV"),
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                Session("FinYear")(Session("COUNTE")) = LastYearHistory - FirstYearHistory + FinAll 'jrs 2006/09/22
            End If
        Next ii
        'jrs 09/30/04
        Select Case True
            Case optMap.Checked
                Session("WorldVaropt") = "optMap"
            Case optGini.Checked
                Session("WorldVaropt") = "optGini"
            Case optLorenz.Checked
                Session("WorldVaropt") = "optLorenz"
            Case optHistogram.Checked 'JAM 2013-01-12
                Session("WorldVaropt") = "optHistogram"
        End Select
        If optMap.Checked Then
            'jrs 09/29/04
            '   f. Render "Map" into categories of data
            '   ----------------------------------------
            Session("MapChosenTable") = "Map"
            Session("MapFieldName") = "StoreData"
            Session("MapVar") = cmb_VarName.SelectedValue
            'NumBreaks = 6
            Support.FormManager.Show(Me, "frm_mapViewer.aspx")
        ElseIf optHistogram.Checked Then 'JAM 2013-01-12
            'jrs 2013/02/21
            Session("PYear") = Nothing
            Session("isfromDispMenu") = False  'JAM 2013-01-13
            Session("GraphDisplayParent") = "frm_WorldVariables.aspx" 'JAM 2013-01-19
            Support.FormManager.Show(Me, "frm_Histogram.aspx?VarName=" & sVarName & "&File=" & Session("DefaultFile"))
        Else
            ''jrs 2011/05/30
            ''Dim frm_DisplayMenu As New frm_DisplayMenu
            ''frm_DisplayMenu.GiniSetup()
            'Dim IfsPars As New myParameters
            'IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"), _
            'Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"), _
            'Session("PutFdrvRun"), Session("CurrencySym"))

            GiniSetup(IfsPars, Session("COUNTE"), Me, Session("VARN"), Session("LastYear"), Session("FirstYear"),
            Session("PutFdrvRun"), Session("ActiveBase"), Session("RunFiles"), Session("bLoadForDisplaySuccess"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"), Session("bFillHisWV"), Session("buseAllHis"), Session("FinYearHis"),
            Session("LastYearForecast"), Session("bComputation"), Session("bFillHisOnlyHolesWV"),
            Session("LorenzX_Year"), Session("LorenzY_Year"), Session("PLOT"), Session("LorenzX"),
            Session("LorenzY"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("YAxisLabel"),
            Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))

            If optGini.Checked Then
                'JRSVBNET Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DisplayMenu.aspx")
            Else
                Support.FormManager.Show(Me, "frm_LorenzGraph.aspx?VarName=" & sVarName & "&File=" & Session("DefaultFile"))
            End If
        End If
    End Sub


    Private Sub lstVarName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstVarName.SelectedIndexChanged
        Dim old As Integer
        old = cmb_VarName.SelectedIndex
        cmb_VarName.SelectedIndex = lstVarName.SelectedIndex + 1
        If old <> cmb_VarName.SelectedIndex Then
            Call cmb_VarName_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub lstDIM1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDIM1.SelectedIndexChanged
        Dim old As Integer
        old = cmb_DIM1.SelectedIndex
        cmb_DIM1.SelectedIndex = lstDIM1.SelectedIndex + 1
        If old <> cmb_DIM1.SelectedIndex Then
            Call cmb_DIM1_SelectedIndexChanged(sender, e)
        End If
    End Sub

    'jrs 2015/04/06
    Private Sub LoadVars()
        cmb_VarName.Items.Clear()
        lstVarName.Items.Clear()
        cmb_VarName.Items.Add(New ListItem(""))
        Dim i2 As Integer
        Dim AddName As String
        For i2 = 1 To NumbVars
            If IIf(IsDBNull(VHistoricAnalog(i2)), "", VHistoricAnalog(i2)) > "" Or Not Session("WVHistoryPlusForecast") Then
                AddName = V(i2)
                AddName = AddName + " - " + VDef(i2) + " - " + VUnits(i2)
                If VDim1(i2) = 1 Then
                    cmb_VarName.Items.Add(New ListItem(AddName))
                    lstVarName.Items.Add(New ListItem(AddName))
                End If
            End If
        Next i2
    End Sub

    '2015/04/06
    Private Sub mnuFillHolesAll_Click()
        Session("bFillHisWV") = Not Session("bFillHisWV")
        If CBool(Session("bFillHisWV")) Then
            cswmWebMenu.Groups("mnuFillHoles").Items("mnuEstimation").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("mnuFillHoles").Items("mnuEstimation").IconImage = "blank.gif"
        End If
        If CBool(Session("bFillHisWV")) Then
            cswmWebMenu.Groups("mnuFillHoles").Items("mnuEstimationOnlyHoles").IconImage = "blank.gif"
            Session("bFillHisOnlyHolesWV") = False
        End If
    End Sub

    'jrs 2015/04/06
    Private Sub mnuFillHolesNoExtra_Click()
        Session("bFillHisOnlyHolesWV") = Not Session("bFillHisOnlyHolesWV")
        If CBool(Session("bFillHisOnlyHolesWV")) Then
            cswmWebMenu.Groups("mnuFillHoles").Items("mnuEstimationOnlyHoles").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("mnuFillHoles").Items("mnuEstimationOnlyHoles").IconImage = "blank.gif"
        End If
        If CBool(Session("bFillHisOnlyHolesWV")) Then
            cswmWebMenu.Groups("mnuFillHoles").Items("mnuEstimation").IconImage = "blank.gif"
            Session("bFillHisWV") = False
        End If
    End Sub

    Private Sub mnuHistForOff_Click()
        'jrs 2015/04/06
        Session("WVHistoryPlusForecast") = False
        cswmWebMenu.Groups("mnuHistFor").Items("On").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuHistFor").Items("Off").IconImage = "checkbox.gif"
        LoadVars()
        Session("FirstYear") = FirstYearForecast
        cswmWebMenu.Groups("mnuFillHoles").ShowMenuBarItem = False

    End Sub

    Private Sub mnuHistForOn_Click()
        'jrs 2015/04/06
        Session("WVHistoryPlusForecast") = True
        cswmWebMenu.Groups("mnuHistFor").Items("On").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuHistFor").Items("Off").IconImage = "blank.gif"
        LoadVars()
        Session("FirstYear") = FirstYearHistory
        cswmWebMenu.Groups("mnuFillHoles").ShowMenuBarItem = True
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "On"
                mnuHistForOn_Click()
            Case "Off"
                mnuHistForOff_Click()
            Case "Full Estimation (Includes Extrapolation)"
                mnuFillHolesAll_Click()
            Case "Fill Holes Only (No extrapolation)"
                mnuFillHolesNoExtra_Click()
        End Select
    End Sub

End Class

