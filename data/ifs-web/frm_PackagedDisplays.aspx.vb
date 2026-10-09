Partial Class frm_PackagedDisplays
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

        'jrs 2005/04/22
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
        End If

        'jrs 11/08/04
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

    'jrs 10/31/04
    'Prepackaged Displays, by Jose Solorzano
    Dim rs As DataTable
    Dim iGDPSel As Integer, iPopSel As Integer

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'jrs 2005/10/13
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Format"
        'jrs 2008/03/28
        cswmGroup1.ID = "mnuGrpDisplay"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"
        'jrs 2006/11/28
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Currency"
        'jrs 2006/11/28
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Horizon"
        'jrs 2006/11/28
        cswmItem.IconImage = "blank.gif"
        'jrs 2008/03/28
        cswmItem.ID = "mnuHorizon"

        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()
        'jrs 2008/03/28
        cswmGroup2.ID = "mnuHorizonGrp"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Display/Run Horizon"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Run File Horizon Choice (With Multiple File Use)"
        'jrs 2008/03/28
        cswmItem.ID = "mnuRunFile"

        Dim cswmGroup3 As Coalesys.WebMenu.Group
        cswmGroup3 = cswmItem.AddGroup()
        'jrs 2008/03/28
        cswmGroup3.ID = "mnuRunFileGrp"

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "Shortest Run File Horizon"
        'jrs 2008/03/28
        cswmItem.ID = "mnuShort"
        If Session("DisplayTime") = 0 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "Longest Run File Horizon"
        'jrs 2008/03/28
        cswmItem.ID = "mnuLong"
        If Session("DisplayTime") = 1 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use estimation to complete data"
        cswmItem.ID = "mnuEstimation"
        If CBool(Session("bFillHis")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2006/09/26
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use all available Historical Data"
        cswmItem.ID = "mnuuseAllHis"
        If CBool(Session("buseAllHis")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2006/12/12
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16001);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        'cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,8010);"
        'cswmGroup1.OnMenuBarClick = "RH_ShowHelp(0,'WebHelp/ifshelp.htm',HH_HELP_CONTEXT,8010);"
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            Session("TableDisplayParent") = "frm_PackagedDisplays.aspx"
            Session("GraphDisplayParent") = "frm_PackagedDisplays.aspx"
            'db = DBEngine.OpenDatabase(PutFdrvRun$ + "IFSVAR.mdb")
            'jrs 10/31/04
            iGDPSel = 0
            iPopSel = 0
            Dim X As Integer
            For X = 1 To NumbVars
                Select Case V$(X)
                    Case "GDP"
                        iGDPSel = X
                    Case "POP"
                        iPopSel = X
                End Select
                If iGDPSel > 0 And iPopSel > 0 Then Exit For
            Next X
            'jrs 2006/11/28
            If Session("bDisplayControl") Is Nothing Then
                'jrs 2005/11/29
                Session("TableTitle") = ""
            Else
                Session("bDisplayControl") = Nothing
            End If

            LoadCategories()
            'Load Files
            LoadFiles()
            Session("iGDPSel") = iGDPSel
            Session("iPopSel") = iPopSel
            'jrs 2005/03/15
            Session("AnnotationFile") = Nothing
            'jrs 2005/04/19
            ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)
            If Not (Session("PDFilesIdxs")(0) Is Nothing) Then
                Dim fileIdx As Integer
                For fileIdx = 0 To lstFiles.Items.Count - 1
                    lstFiles.Items(fileIdx).Selected = Session("PDFilesIdxs")(fileIdx)
                Next fileIdx
            End If

            'jrs 2011/11/04
            If Not Session("txtFiles") Is Nothing Then
                txtFiles.Value = Session("txtFiles")
            End If

            'jrs 2005/11/11
            If Not (Session("oldLastYear") Is Nothing) Then
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
        Else
            iGDPSel = Session("iGDPSel")
            iPopSel = Session("iPopSel")
            rs = Session("PackagedDispRs")
        End If
        'jrs 11/8/04
        Session("RefreshGraph") = "true"
        'jrs 2005/12/10
        Session("TableHis") = Nothing
        'jrs 2011/11/03
        lstFiles.Attributes.Add("onclick", "lstFiles_Click();")
        lstFiles.Attributes.Add("onmousedown", "lstFiles_ClickDown();")
        lstFiles.Attributes.Add("onmouseup", "lstFiles_ClickUp();")
    End Sub

    Private Sub cmdBar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBar.Click
        'jrs 2005/11/11
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        LoadPackage()
        If Session("COUNTE") = 0 Then
            Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstPacks.SelectedValue

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If


        On Error GoTo DefaultInterval
        'JRS 2/13/02 LABELS If CInt(Txt_Interval.Text) < 1 Then Txt_Interval.Text = Str$(1)
        'JRS 2/13/02 LABELS Interval% = CInt(Txt_Interval.Text)
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("GType") = "BAR"
        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")

        Exit Sub

DefaultInterval:
        Session("Interval") = 1
        Resume Next

    End Sub

    Private Sub LoadVariables()
        lstPacks.Items.Clear()
        Dim rsL As DataTable
        Dim str As String
        Dim sql As String
        sql = "SELECT DISTINCT L.ListName FROM VarLists as L"
        'sql = "SELECT I.Definition, L.VarName, L.Param1, L.Param2, L.DisplayTitle FROM VarLists as L, IFsVar as I WHERE L.VarName = I.Name AND L.ListName = 'PackagedDisplays'"
        If lstCategories.SelectedIndex > 0 Then
            'sql = sql + " AND L.Category = '" & lstCategories.List(lstCategories.ListIndex) & "'"
            sql = sql + " WHERE L.Category = """ & lstCategories.SelectedValue & """"
            'jrs 2005/11/09
            If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                sql = sql + " AND L.HistoryplusForecast = False"
            Else 'jrs 2005/11/16
                sql = sql + " AND L.HisCompEmpFor = False" 'L.ListName NOT IN (SELECT V1.ListName FROM VarLists as V1, VarLists as V2 WHERE V1.ListName = V2.ListName AND V1.History = True AND V2.History = False)"
            End If
        Else
            'jrs 2005/11/09
            If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                sql = sql + " WHERE L.HistoryplusForecast = False"
            Else 'jrs 2005/11/16
                sql = sql + " WHERE L.HisCompEmpFor = False" 'L.ListName NOT IN (SELECT V1.ListName FROM VarLists as V1, VarLists as V2 WHERE V1.ListName = V2.ListName AND V1.History = True AND V2.History = False)"
            End If
        End If
        'jrs 2006/01/12
        sql = sql + " AND L.Type = 'D'"

        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rsL)
        Dim i As String
        i = 0

        Do While (Not rsL.Rows.Count = i)
            lstPacks.Items.Add(rsL.Rows(i).Item("ListName"))
            i = i + 1
        Loop
        If lstPacks.Items.Count > 0 Then
            'jrs 2005/04/20
            If Not (Session("PDPacksIdx") Is Nothing) And Not Page.IsPostBack Then
                lstPacks.SelectedIndex = Session("PDPacksIdx")
            Else
                lstPacks.SelectedIndex = 0
            End If
            Call lstPacks_SelectedIndexChanged(Me, New System.EventArgs)
        End If
    End Sub

    Private Sub LoadFiles()
        'jrs 2005/02/21 changed First Year to First Year Forecast, if History plus Forecast
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            Session("VarSelCheck") = Nothing
        End If
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = Session("LastYearForecast") Then
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
                Session("VarSelCheckHis") = Nothing 'jrs 2005/10/12
            Else
                Session("LastYear") = LastYearHistory
            End If
        End If

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim fs 'As New FileSystemObject
        Dim BASE 'As File
        Dim currRun 'As File
        fs = CreateObject("Scripting.FileSystemObject")
        Dim sWorkingFile As String
        sWorkingFile = Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))

        'JRS 05/20/03
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = LastYearHistory Then
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
        'jrs 2005/05/09
        lstFiles.Items.Clear()

        lstFiles.Items.Add("0 - " + "Working File, based on " + sWorkingFile)
        lstFiles.Items.Add("1 - " + BASE.Name)
        'jrs 2005/02/28
        Session("FilesSelected") = 1

        Dim sRunFile As String
        ' Returns filename with specified extension. If more than one *.ini
        ' file exists, the first file found is returned.
        sRunFile = Dir(Session("PutFdrvRun2") + "\*.run.db")

        Do While (sRunFile <> "")
            If UCase(sRunFile) <> UCase(BASE.Name) And
                UCase(sRunFile) <> UCase(sWorkingFile) And
                UCase(sRunFile) <> "WORKING.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") + "\" + sRunFile)
                'jrs 2022/04/08 added Or StandAloneMode
                If Session("IFsRunFiles").item(sRunFile).Visible Or StandAloneMode Then 'jrs 2005/06/27
                    lstFiles.Items.Add(Trim(Str(lstFiles.Items.Count)) + " - " + sRunFile)
                    Session("FilesSelected") = Session("FilesSelected") + 1
                    ReDim Preserve Session("RunFiles")(Session("FilesSelected"))
                    Session("RunFiles")(Session("FilesSelected")) = Session("PutFdrvRun2") + "\" + sRunFile
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
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            sRunFile = Dir()
        Loop
        'jrs 2026/01/26 changed default to base case, it was working
        lstFiles.SelectedIndex = 1
        lstFiles.Items(1).Selected = True
    End Sub

    Private Sub LoadCategories()
        'jrs 2005/04/28
        lstCategories.Items.Clear()
        lstCategories.Items.Add("ALL")
        Dim rsL As DataTable
        Dim str As String
        str = "SELECT DISTINCT Category FROM VarLists"
        'JRS 2005/11/16
        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            str = str + " WHERE HisCompEmpFor = False AND Type = 'D'" 'jrs 2006/01/18 'ListName NOT IN (SELECT V1.ListName FROM VarLists as V1, VarLists as V2 WHERE V1.ListName = V2.ListName AND V1.History = True AND V2.History = False)"
        Else
            str = str + " WHERE Type = 'D'"
        End If
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", str, "", rsL)
        Dim i As Integer
        i = 0
        Do While (Not rsL.Rows.Count = i)
            str = rsL.Rows(i).Item("Category")
            lstCategories.Items.Add(str)
            i = i + 1
        Loop
        If lstCategories.Items.Count > 0 Then
            'jrs 2005/04/20
            If Not (Session("PDCategoriesIdx") Is Nothing) Then
                lstCategories.SelectedIndex = Session("PDCategoriesIdx")
            Else
                lstCategories.SelectedIndex = 0
            End If
            'jrs 2005/10/13
            Dim sT As String
            sT = CStr(Session("TableTitle"))
            Call lstCategories_SelectedIndexChanged(Me, New System.EventArgs)
            If sT > "" Then
                Session("TableTitle") = sT
            End If
        End If
    End Sub

    Private Sub LoadPackage()
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

        Dim bGrp As Object, FinAll As Integer
        Dim sDef As String, sUn As String
        'jrs 10/31/04
        If rs.Rows.Count = 0 Then
            Session("COUNTE") = 0
            lblMsg.Text = "Variable doesn't exist!"
            lblMsg.Visible = True
            Exit Sub
        End If

        Dim fileIdx As Integer
        Dim dim1 As String
        Dim dim2 As String
        Dim dim3 As String
        Dim sVar As String
        Dim sTitle As String
        Dim bFirst As Boolean
        bFirst = True
        Dim idx = 0
        Dim idxVar As Integer
        'jrs 2008/01/28 long Formulas (Indices)
        Dim sFormulaDet As String
        Dim rsFormulaDet As DataTable
        Dim sqlFDet As String

        'jrs 2005/11/09
        If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And rs.Rows(0).Item("HistoryplusForecast") Then
            LoadMessage("Can't Load Package for History Plus Forecast when using History.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        'jrs 2006/06/06 set up years
        If rs.Rows(0).Item("HistoryplusForecast") Then
            Session("FirstYear") = FirstYearHistory
        ElseIf rs.Rows(0).Item("History") Then
            Session("FirstYear") = FirstYearHistory
            Session("LastYear") = LastYearHistory
        End If

        'jrs 2007/10/19
        Dim bPov As Boolean, bPovPct As Boolean, bPovFind As Boolean, PovLevel As Single
        'jrs 2011/06/17
        Dim bIncR As Boolean, PovLevel2 As Single
        'jrs 2014/10/21
        Dim b2005PPP As Boolean

        'jrs 2011/11/04
        'Create Collection based on Order passed or listBox
        Dim SceOrdFiles As New Collection
        Dim sceFile As Object
        For fileIdx = 0 To lstFiles.Items.Count - 1
            Session("PDFilesIdxs")(fileIdx) = False
        Next
        If Trim(txtFiles.Value) > "" Then
            Dim Fs As Object
            Fs = Split(txtFiles.Value, ",")
            For fileIdx = 0 To UBound(Fs)
                SceOrdFiles.Add(CInt(Fs(fileIdx)), Fs(fileIdx).ToString)
            Next
        Else
            For fileIdx = 0 To lstFiles.Items.Count - 1
                If lstFiles.Items(fileIdx).Selected Then
                    SceOrdFiles.Add(fileIdx, fileIdx.ToString)
                End If
            Next
        End If

        Do While (Not idx = rs.Rows.Count)

            sVar = rs.Rows(idx).Item("VarName")
            'jrs 11/19/04
            If Not IsDBNull(rs.Rows(idx).Item("Param1")) Then
                If rs.Rows(idx).Item("Param1") > "" Then
                    dim1 = rs.Rows(idx).Item("Param1")
                    If Not IsDBNull(rs.Rows(idx).Item("Param2")) Then
                        If rs.Rows(idx).Item("Param2") > "" Then
                            dim2 = rs.Rows(idx).Item("Param2")
                            'jrs 2006/05/17 Dim3
                            If Not IsDBNull(rs.Rows(idx).Item("Param3")) Then
                                If rs.Rows(idx).Item("Param3") > "" Then
                                    dim3 = rs.Rows(idx).Item("Param3")
                                Else
                                    dim3 = ""
                                End If
                            Else
                                dim3 = ""
                            End If
                        Else
                            dim2 = ""
                            dim3 = ""
                        End If
                    Else
                        dim2 = ""
                        dim3 = ""
                    End If
                Else
                    dim1 = ""
                    dim2 = ""
                    dim3 = ""
                End If
            Else
                dim1 = ""
                dim2 = ""
            End If
            If Not IsDBNull(rs.Rows(idx).Item("DisplayTitle")) Then
                sTitle = rs.Rows(idx).Item("DisplayTitle")
            Else
                sTitle = ""
            End If

            'jrs 2007/10/19
            If IfsVars.Item(sVar).Index = -1 And Not rs.Rows(idx).Item("Formula") Then
                checkPovertyVarName(sVar, bPov, bPovPct, bPovFind, PovLevel, bIncR, PovLevel2, b2005PPP)
            Else
                bPov = False
                bPovPct = False
                bPovFind = False
                'jrs 2011/06/17
                bIncR = False
            End If

            'jrs 2011/11/11 For fileIdx = 0 To lstFiles.Items.Count - 1
            For Each sceFile In SceOrdFiles
                fileIdx = CInt(sceFile)
                Session("PDFilesIdxs")(fileIdx) = lstFiles.Items(fileIdx).Selected
                If lstFiles.Items(fileIdx).Selected Then
                    If rs.Rows(idx).Item("Formula") Then
                        If bFirst Then
                            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                        End If
                        Session("ShowVarDispList") = False
                        If rs.Rows(idx).Item("HistoryplusForecast") Then
                            Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast"
                        Else
                            Session("VarSelCheck") = "mnuVarSelOptShowVarPar"
                        End If
                        'jrs 2005/10/12
                        If IIf(IsDBNull(rs.Rows(idx).Item("FormulaDetHis")), "", rs.Rows(idx).Item("FormulaDetHis")) > "" Then 'jrs 2005/11/03
                            gLoadCompHisFor(rs, dim1, dim2, dim3, fileIdx, 1, Session("bFillHis"), Session("buseAllHis"),
                                Session("FinYearHis")(Session("COUNTE")), Session("COUNTE"), Session("aFormulas"),
                                Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("VARN"), Session("FileforDisplay"),
                                IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"), Nothing,
                                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                        Else
                            'jrs 2008/01/28 check for big formulas (Indices like Sofi)
                            'Memo field gets truncated when using UNION
                            If Len(rs.Rows(idx).Item("FormulaDet")) > 254 Then
                                sqlFDet = "SELECT L.FormulaDet FROM VarLists as L WHERE L.Formula = True AND L.FormulaId = " &
                                    CStr(rs.Rows(idx).Item("FormulaId")) & " AND L.ListName = '" &
                                    Replace(lstPacks.SelectedValue, "'", "''") & "'"
                                SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sqlFDet, "", rsFormulaDet)
                                sFormulaDet = rsFormulaDet.Rows(0).Item("FormulaDet")
                            Else
                                sFormulaDet = rs.Rows(idx).Item("FormulaDet")
                            End If

                            Session("COUNTE") = Session("COUNTE") + 1
                            gLoadComp(Session("COUNTE"), rs.Rows(idx).Item("FormulaDet"), sVar, "", "", "", "",
                                rs.Rows(idx).Item("FormulaId"), Nothing, fileIdx, rs.Rows(idx).Item("HistoryplusForecast"),
                                False, Nothing, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                False, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
                        End If
                        FormatFormAfterLoad(fileIdx, Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
                            IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
                            Session("MaxFinYearHis"))
                    Else
                        bGrp = Nothing
                        If bFirst Then
                            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                        End If
                        Session("COUNTE") = Session("COUNTE") + 1

                        'jrs 2005/11/11
                        If rs.Rows(idx).Item("History") Then
                            gLoadForDisplayHis(sVar, dim1, -1, dim2, -1, dim3, -1,
                                bGrp, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory,
                                False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                            gLoadForDisplayHisSaveLbls(Session("COUNTE"), sVar, dim1, bGrp, sDef, sUn, sTitle, Session("FileNumbr"), Session("VARN"),
                                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                        Else
                            If bIncR Then
                                gLoadIncomeRange(PovLevel, PovLevel2, dim1, fileIdx, bGrp, bPovPct, b2005PPP,
                                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                    Session("SensitivityFlag"), Session("mnuSQLite"))
                                'jrs 2007/10/19
                            ElseIf bPov Or bPovPct Then
                                gLoadPovertyDisp(PovLevel, dim1, fileIdx, bGrp, bPovPct, b2005PPP,
                                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                    Session("SensitivityFlag"), Session("mnuSQLite"))
                            ElseIf bPovFind Then
                                gFindPovertyLevel(dim1, fileIdx, bGrp, PovLevel, b2005PPP,
                                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                            Else

                                Dim iRowPosition As Integer, iFirstYear As Integer
                                If rs.Rows(idx).Item("HistoryplusForecast") Then
                                    iRowPosition = LastYearHistory - FirstYearHistory + 1
                                    iFirstYear = FirstYearForecast
                                Else
                                    iRowPosition = 1
                                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then 'JRS 2006/05/22
                                        iFirstYear = FirstYearForecast
                                        'jrs 2006/06/06 FirstYear = FirstYearForecast
                                    Else
                                        iFirstYear = FirstYearHistory
                                    End If
                                End If
                                idxVar = -1
                                If Session("mnuSQLite") = "True" Then
                                    gLoadForDisplayNET(sVar, idxVar, dim1, -1, dim2, -1, dim3, -1, "", fileIdx, True, True, bGrp, 1, Session("PLOT"), Session("COUNTE"), iRowPosition,
                                        iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                        Session("IFs_MDB2Active"), Session("CurrencyVal"))
                                Else
                                    gLoadForDisplay(sVar, idxVar, dim1, -1, dim2, -1, dim3, -1, "", fileIdx, True, True, bGrp, 1, Session("PLOT"), Session("COUNTE"), iRowPosition,
                                        iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                                        Session("IFs_MDB2Active"), Session("CurrencyVal"))
                                End If
                                gLoadForDisplaySaveLbls(Session("COUNTE"), idxVar, dim1, dim2, dim3, bGrp, fileIdx, sTitle,
                                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                            End If
                        End If
                    End If
                    bFirst = False
                    'jrs 2005/08/18
                    If rs.Rows(idx).Item("HistoryplusForecast") And Not rs.Rows(idx).Item("Formula") Then
                        FinAll = Session("FinYear")(Session("COUNTE"))
                        'jrs 2006/06/06 FirstYear = FirstYearHistory
                        'jrs 2006/06/08 lastyearhistory - 1 inorder to keep forecast value in year 2000 instead of historic one
                        gLoadForDisplayHis(sVar, dim1, -1, dim2, -1, dim3, -1,
                            bGrp, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                            False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                            Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                        Session("FinYear")(Session("COUNTE")) = LastYearHistory - FirstYearHistory + FinAll 'jrs 2006/09/22
                    End If
                End If
            Next 'jrs 2011/11/11 fileIdx

            idx = idx + 1
        Loop

        'jrs 2012/07/03
        Session.Remove("bVARN0Loaded")

    End Sub


    Private Sub cmdLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLine.Click
        'jrs 2005/11/11
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        LoadPackage()
        If Session("COUNTE") = 0 Then
            Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstPacks.SelectedValue

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GType") = "LINE"

        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
    End Sub

    Private Sub cmdTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        'jrs 2005/11/11
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        LoadPackage()
        If Session("COUNTE") > 0 Then
            'jrs 2005/06/07
            Session("TableTitle") = lstPacks.SelectedValue
            Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
            'frm_TableDisplay.Caption = lstPacks.List(lstPacks.ListIndex)
        End If
    End Sub

    Private Sub lstCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCategories.SelectedIndexChanged
        'jrs 2005/04/19
        Session("PDCategoriesIdx") = lstCategories.SelectedIndex
        LoadVariables()
    End Sub

    Private Sub lstPacks_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPacks.SelectedIndexChanged
        'jrs 2005/04/19
        Session("PDPacksIdx") = lstPacks.SelectedIndex

        Dim str As String
        Dim sql As String
        'jrs 2004/11/30
        'jrs 2005/08/18 sql = "SELECT I.Definition, L.VarName, L.Param1, L.Param2, L.DisplayTitle FROM VarLists as L, IFsVar as I WHERE L.VarName = I.Name AND L.ListName = '" & Replace(lstPacks.SelectedValue, "'", "''") & "'"
        sql = "SELECT L.ListOrder, I.Definition, L.VarName, L.Param1, L.Param2, L.Param3, L.DisplayTitle, L.Formula, L.FormulaId, L.FormulaDet, L.HistoryplusForecast, L.FormulaDetHis, L.History FROM VarLists as L, IFsVar as I WHERE L.VarName = I.Name AND L.ListName = '" & Replace(lstPacks.SelectedValue, "'", "''") & "' AND L.Formula = False AND L.History = False" 'jrs 2005/11/11 added , L.History 
        'jrs 2008/01/30 added not INCOMELT1LN and not INCOMELT2LN
        'jrs 2013/05/23 changed wildcard character in LIKE statement from * to %, vb6 and access like *, but not the web, % doesn't work in vb6 and access.
        sql = sql & " UNION SELECT L.ListOrder, L.VarName as Definition, L.VarName, L.Param1, L.Param2, L.Param3, L.DisplayTitle, L.Formula, L.FormulaId, L.FormulaDet, L.HistoryplusForecast, L.FormulaDetHis, L.History FROM VarLists as L WHERE (L.Formula = True OR L.History = True OR (L.VarName LIKE 'INCOMELT%LN%' AND L.VarName <> 'INCOMELT1LN' AND L.VarName <> 'INCOMELT2LN')) AND L.ListName = '" & Replace(lstPacks.SelectedValue, "'", "''") & "'" 'jrs 2005/11/11 added OR L.History = True
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rs)
        Session("PackagedDispRs") = rs
        'jrs 2005/10/13
        Session("TableTitle") = lstPacks.SelectedValue
    End Sub

    Private Sub cmdContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        'jrs 2005/08/24
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        'jrs 2005/11/09
        Session("TableTitle") = ""
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
                'jrs 2006/06/20
                Session("LastYear") = Session("LastYearForecast")
            End If
        End If
        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If

        Support.FormManager.Show(Me, Session("DisplayParent"))
    End Sub

    'jrs 2005/03/15
    Private Sub cmdAnnotation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnotation.Click
        'jrs 2005/04/22
        Dim i As Integer
        For i = 0 To lstFiles.Items.Count - 1
            Session("PDFilesIdxs")(i) = lstFiles.Items(i).Selected
        Next

        Dim sDirSess As String = "\Session"
        If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
            sDirSess = sDirSess & "0"
        End If
        sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sFileName As String
        Select Case lstFiles.SelectedIndex
            Case 0
                'jrs 2005/03/24
                sFileName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text) & "WorkingAnnotation.txt"
            Case 1
                sFileName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text) & "IFsBase.sce"
            Case Else
                'jrs 2005/04/12
                sFileName = FindFile(Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text), Replace(Mid(lstFiles.SelectedValue, InStr(lstFiles.SelectedValue, "-") + 2, Len(lstFiles.SelectedValue) - 1 - InStr(lstFiles.SelectedValue, "-")), ".run.db", ".sce", 1, -1, vbTextCompare), "sce")
        End Select

        Session("AnnotationFile") = sFileName

        If sFileName = "" Then
            Session("AnnotationFile") = Nothing
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "Notepad.aspx")
    End Sub

    'jrs 2005/10/13
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                cmdContinue_Click(Me, New EventArgs)
            Case "Currency"
                'jrs 2008/05/09
                Session("CurrencyParent") = "frm_PackagedDisplays.aspx"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_CurrencyOpt.aspx")
            Case "Set Title, Display Interval, or Year"
                mnuDispCtrl_Click(sender, e)
            Case "Display/Run Horizon"
                mnuDisplayRunHorizon_Click(sender, e)
            Case "Use estimation to complete data"
                mnuEstimation_Click()
            Case "Use all available Historical Data" 'jrs 2006/09/26
                mnuUseallHis_Click()
            Case "Shortest Run File Horizon" 'jrs 2008/03/28
                Session("DisplayTime") = 0
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuShort").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuLong").IconImage = "blank.gif"
            Case "Longest Run File Horizon"
                Session("DisplayTime") = 1
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuLong").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuShort").IconImage = "blank.gif"
        End Select
    End Sub

    'jrs 2005/10/13
    Public Sub mnuDispCtrl_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("DisplayControlParent") = "frm_PackagedDisplays.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
        'frm_DisplayControl.DefInstance.Show() 
    End Sub

    'jrs 2005/10/13
    Public Sub mnuDisplayRunHorizon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If CBool(Session("HistoricBase")) = True Then
            Msg = "You cannot change the horizon for historical analysis."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If
        cmb_Horizon.Visible = True
        frm_Horizon.Visible = True
        'jrs 2005/12/11
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If
        cmb_Horizon.Items.Clear()
        cmb_Horizon.Items.Add("")
        'jrs 2008/12/18
        cmb_Horizon.Items.Add(New ListItem(Session("FirstYear").ToString))

        For ICount = Session("FirstYear") + Session("Interval") To Session("LastYearForecast") Step Session("Interval") 'jrs 2005/12/11 added step interval
            cmb_Horizon.Items.Add(New ListItem(CStr(ICount)))
        Next ICount

    End Sub

    'JRS 2005/10/13
    Private Sub cmb_Horizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged
        Dim Ind As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2008/12/22 changed from 0 to -1, added FirstYear
        Ind = cmb_Horizon.SelectedIndex - 1
        'jrs 2005/12/11
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If

        'JRS 11/03/03 CHANGE LASTYEAR WITH DISPLAYLASTYEAR
        If Ind >= 1 Then
            Session("DisplayLastYear") = Session("FirstYear") + Ind * Session("Interval") 'jrs 2005/12/11
        Else
            Session("DisplayLastYear") = Val(cmb_Horizon.SelectedValue)
            'jrs 2008/12/18 If Session("DisplayLastYear") > LastYearForecast Or Session("DisplayLastYear") < FirstYearForecast + 1 Then Exit Sub
            If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                Session("DisplayLastYear") = Session("LastYear")
                LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If
        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False
    End Sub

    Private Sub mnuEstimation_Click()
        'jrs 2006/06/09
        Session("bFillHis") = Not CBool(Session("bFillHis"))
        If CBool(Session("bFillHis")) Then
            cswmWebMenu.Groups("Display Format").Items("mnuEstimation").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("Display Format").Items("mnuEstimation").IconImage = "blank.gif"
        End If

    End Sub

    'jrs 2006/09/26
    Private Sub mnuUseallHis_Click()
        Session("buseAllHis") = Not CBool(Session("buseAllHis"))
        If CBool(Session("buseAllHis")) Then
            'jrs 2025/06/20 cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuuseAllHis").IconImage = "checkbox.gif"
        Else
            'jrs 2025/06/20 cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuuseAllHis").IconImage = "blank.gif"
        End If

    End Sub

    Protected Sub lstFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        'jrs 2011/11/04
        Session("txtFiles") = txtFiles.Value
    End Sub
End Class

