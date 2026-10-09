Imports IFs.Data
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_Cyber
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Listbox1 As System.Web.UI.WebControls.ListBox


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        'jrs 2011/05/03 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

    'jrs 10/31/04
    'Prepackaged Displays, by Jose Solorzano
    Dim rs As DataTable
    Dim iGDPSel As Integer, iPopSel As Integer

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load 'jrs 2011/02/11 this was causing a 2nd call to Load, Me.Load
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
        cswmGroup1.Caption = "Display Options"
        cswmGroup1.ID = "mnuGrpDisplay"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Parameters"
        cswmItem.ID = "mnuUsePar"
        If CBool(Session("bUsePar")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Historical Data"
        cswmItem.ID = "mnuUseHist"
        If CBool(Session("bUseHist")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2013/05/09
        If DevelopmentMode Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Create JavaScript File for Flex Displays Variables"
            cswmItem.IconImage = "blank.gif"
        End If

        If Session("bUseReg") Is Nothing Then
            Session("bUseReg") = True
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
        End If

        If Not Page.IsPostBack Then
            optReg.Checked = Session("bUseReg")
            optGroup.Checked = Session("bUseGroup")
            optGroupDec.Checked = Session("bUseGroupDec")
            optGList.Checked = Session("bUseGList")
            'chkIntercept.Checked = Session("bIntercept")

        End If
        optGroup.Attributes.Add("onClick", "javascript:optGroup_Click()")
        optGroupDec.Attributes.Add("onClick", "javascript:optGroupDec_Click()")
        optGList.Attributes.Add("onClick", "javascript:optGList_Click()")
        optReg.Attributes.Add("onClick", "javascript:optReg_Click()")


        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Run"
        cswmGroup1.ID = "mnuRun"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Cost-Benefit Analysis"
        cswmGroup1.ID = "mnuCostBenefitAnalysis"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Adverse Cyber Event Probability"
        cswmItem.ID = "mnuChgProbability"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Adverse Cyber Event Costy"
        cswmItem.ID = "mnuChgCost"

        'jrs 2011/09/06
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Cost-Benefit Display"
        cswmItem.ID = "mnuCostBenefit"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Cross-Section Cost vs Probability"
        cswmItem.ID = "mnuShowCrossCostProb"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Search Variables/Parameters"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Search"

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Help"
        'cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        cmdTable.Attributes.Add("onClick", "showhg(cmdTable);")
        cmdLine.Attributes.Add("onClick", "showhg(cmdLine);")

        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            Session("TableDisplayParent") = "frm_Cyber.aspx"
            Session("GraphDisplayParent") = "frm_Cyber.aspx"
            'db = DBEngine.OpenDatabase(PutFdrvRun$ + "IFSVAR.mdb")
            'jrs 10/31/04
            iGDPSel = IfsVars.Item("GDP").Index
            iPopSel = IfsVars.Item("POP").Index
            'jrs 2006/11/28
            If Session("bDisplayControl") Is Nothing Then
                'jrs 2005/11/29
                Session("TableTitle") = ""
            Else
                Session("bDisplayControl") = Nothing
            End If

            'Load Variables
            LoadVariables()
            'Load Files
            LoadFiles(Session("VarSelCheck"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("ActiveBase"),
            Session("VarSelCheckHis"), Session("PutFdrvRun2"), Session("WorkingFile"), Session("FilesSelected"), Session("IFsRunFiles"),
            Session("RunFiles"), Session("ShortSce"), lstFiles)
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

            RefreshIdxS()

            'jrs 2011/11/04
            If Not Session("txtFiles") Is Nothing Then
                txtFiles.Value = Session("txtFiles")
            End If
            'jrs 2012/12/05
            If Not Session("txtDims") Is Nothing Then
                txtDims.Value = Session("txtDims")
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

            Session("GraphDisplayParent") = "frm_Cyber.aspx"
        Else
            iGDPSel = Session("iGDPSel")
            iPopSel = Session("iPopSel")
            'jrs 2013/05/22
            If Not Session("txtDims") Is Nothing And txtDims.Value = "" And InStr(Session("txtDims"), CStr(lstDim.SelectedIndex)) > 0 Then
                txtDims.Value = Session("txtDims")
            End If
        End If

        'jrs 11/8/04
        Session("RefreshGraph") = "true"
        'jrs 2005/12/10
        Session("TableHis") = Nothing
    End Sub

    'jrs 2011/02/11
    Private Sub RefreshIdxS()
        'jrs 2006/01/18
        'jrs 2013/05/09
        If lstDim.Items.Count > 0 Then
            ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
        Else
            ReDim Session("DimIdxs")(1)
        End If

        If Not (Session("DimIdxs")(0) Is Nothing) Then
            Dim dimIdx As Integer
            For dimIdx = 0 To lstDim.Items.Count - 1
                lstDim.Items(dimIdx).Selected = Session("DimIdxs")(dimIdx)
            Next dimIdx
        End If
    End Sub



    'jrs 2011/04/12
    Private Sub URLTolstVars(ByVal sListNames)
        Dim x As Object
        x = Split(sListNames, ",,")
        Dim i As Integer, j As Integer
        j = 0
        For i = 0 To UBound(x)
            Do While lstVars.Items(j).Text <> x(i)
                j = j + 1
                If j = lstVars.Items.Count Then
                    Exit For
                End If
            Loop
            lstVars.Items(j).Selected = True
        Next
        'jrs 2011/11/03
        If lstVars.SelectedIndex = -1 And lstVars.Items.Count > 0 Then
            lstVars.Items(0).Selected = True
        End If
    End Sub




    Private Sub cmdLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLine.Click
        'jrs 2011/04/11
        ''jrs 2005/11/11
        'Session("oldLastYear") = Session("LastYear")
        'Session("oldFirstYear") = Session("FirstYear")

        LoadSet()

        If Session("COUNTE") = 0 Then
            Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstVars.SelectedValue

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GType") = "LINE"

        'jrs 2011/04/05
        Dim TargetUrl As String
        TargetUrl = "frm_GraphicalDisplayHC.aspx"
        'If txtVarNames.Value > "" Then
        '    TargetUrl = TargetUrl & "?VarNames=" & PrepareForURL(txtVarNames.Value)
        '    TargetUrl = TargetUrl & "&HistFor=" & txtHistFor.Value
        '    'jrs 2011/04/11
        '    TargetUrl = TargetUrl & "&GrpOp="
        '    Select Case True
        '        Case Session("bUseGroup") 'Groups 1
        '            TargetUrl = TargetUrl & "1"
        '        Case Session("bUseGroupDec") 'Dec Groups 2
        '            TargetUrl = TargetUrl & "2"
        '        Case Session("bUseGList") 'G-List 3
        '            TargetUrl = TargetUrl & "3"
        '        Case Else 'Regions 0
        '            TargetUrl = TargetUrl & "0"
        '    End Select
        '    'parameter for dimensions
        '    'jrs 2013/10/08
        '    Session("Dim1sLoaded") = DimLstToURL(lstDim, 1)
        '    TargetUrl = TargetUrl & Session("Dim1sLoaded")
        '    'jrs 2013/10/08
        '    Session("Dim1sLoaded") = Replace(Session("Dim1sLoaded"), "&Dim1=", "")
        '    TargetUrl = TargetUrl & FileLstToURL(lstFiles, txtFiles.Value)
        '    'jrs 2013/01/18
        '    TargetUrl = TargetUrl & "&DisplayLastYear=" & Session("DisplayLastYear")
        'End If

        Support.FormManager.Show(Me, TargetUrl)
    End Sub



    Private Sub cmdTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        'jrs 2005/11/11
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        LoadSet()
        If Session("COUNTE") > 0 Then
            'jrs 2005/06/07
            'jrs2012/09/26 Session("TableTitle") = lstVars.SelectedValue
            Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
            'frm_TableDisplay.Caption = lstVars.List(lstVars.ListIndex)
        End If
    End Sub



    'jrs 2008/12/17
    Private Sub DisableButtons()
        cmdLine.Enabled = False
        cmdTable.Enabled = False
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
        'jrs 2008/09/04
        Session.Remove("bUseReg")
        Session.Remove("bUseGroup")
        Session.Remove("bUseGroupDec")
        Session.Remove("bUseGList")
        'jrs 2010/11/19
        Session.Remove("bIntercept")
        'jrs 2008/12/18
        Session.Remove("CyberVarsIdxs")
        'jrs 2012/02/07
        Session.Remove("PDFlexCategoriesIdxs")
        Session.Remove("PDFilesIdxs")
        Session.Remove("DimIdxs")
        Session.Remove("DimIdxs2")
        'jrs 2011/01/20
        Session.Remove("AllDimsIdx")
        Session.Remove("SelIdx")
        'jrs 2011/07/04
        Session.Remove("bAlias")
        Session.Remove("PE_CommonMenu")
        Session.Remove("PE_CommonPlot")
        Session.Remove("oGR1N")
        'jrs 2020/12/22
        Session.Remove("bVARN0Loaded")

        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If

        Support.FormManager.Show(Me, Session("DisplayParent"))
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
            Case "Using Countries/Regions" 'jrs 2008/09/04
                mnuReg_Click()
            Case "Using Groups" 'jrs 2008/09/04
                mnuGroup_Click()
            Case "Using Decomposed Groups" 'jrs 2008/09/04
                mnuGroupDec_Click()
            Case "Using G-Lists" 'jrs 2008/09/04
                mnuGList_Click()
            Case "Create JavaScript File for Flex Displays Variables" 'jrs 2013/05/09
                CreateJSforCyberDisplays(Session("PutFdrvRun"))
            Case "Search" 'jrs 2012/01/20
                'jrs 2013/07/19 Support.FormManager.StateStore(Me) Continue with no selection was giving error
                Support.FormManager.Show(Me, "frm_SearchVList.aspx")
            Case Else 'jrs 2011/04/19
                SaveDimIdxs()
                PackJScritpBuilder()
        End Select
    End Sub

    'jrs 2009/03/17
    Public Sub AddVarList(ByVal sListName As String, ByRef sqlV As String, ByRef sTitle As String)
        If sqlV > "" Then
            sqlV = sqlV & " UNION "
            sTitle = sTitle & ", " & sListName
        Else
            sTitle = sTitle & " " & sListName
        End If
        'Forecast Variables with no formulas
        sqlV = sqlV & "SELECT ListOrder, VarName, DEFINITION, FormulaId, FormulaDetHis, Param2, Param3, [Historic Analog] as Historic_Analog " &
        "FROM VarLists, IFSVAR WHERE Varname = [NAME] AND Formula = False AND " &
        "History = False AND ListName = """ & sListName & """"
        'Historic Variables with no formulas
        sqlV = sqlV & " UNION SELECT ListOrder, VarName, '' as DEFINITION, FormulaId, FormulaDetHis, Param2, Param3, '' as Historic_Analog " &
        "FROM VarLists WHERE Formula = False AND History = True AND ListName = """ &
        sListName & """"
        'Rows with formulas
        sqlV = sqlV & " UNION SELECT ListOrder, VarName, FormulaDet as DEFINITION, FormulaId, FormulaDetHis, Param2, Param3, '' as Historic_Analog " &
        "FROM VarLists WHERE Formula = True AND ListName = """ & sListName & """"
        'jrs 2011/07/12
        'Poverty Level Variables
        sqlV = sqlV & " UNION SELECT ListOrder, VarName, '' as DEFINITION, FormulaId, FormulaDetHis, Param2, Param3, '' as Historic_Analog " &
        "FROM VarLists WHERE NOT EXISTS (SELECT * FROM IFsVar WHERE Varname = [NAME]) AND Formula = False AND " &
        "History = False AND ListName = """ & sListName & """"

    End Sub


    'JRS 2005/10/13
    Private Sub cmb_Horizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged

        Dim Ind As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2008/12/18 added -1 because added firstyear
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
        'jrs 2006/02/03
        lstDim.Visible = True
        'jrs 2011/02/11
        PackJScritpBuilder()

        cswmWebMenu.Visible = True
    End Sub

    'jrs 2006/01/12
    Private Sub LoadDim1s()

        Dim idx As Integer
        Dim i As Integer
        Dim j As Integer
        Dim db As String
        Dim valtbl As DataTable

        lstDim.Items.Clear()

        If (Session("bUseGroup") Or Session("bUseGroupDec")) Then 'jrs 2008/09/04 added GroupDec
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM [" & GroupIDNew$ & "] ORDER BY GroupName", "GroupName", valtbl)
            j = 0
            While valtbl.Rows.Count > j
                lstDim.Items.Add(valtbl.Rows(j).Item("GroupName"))
                j = j + 1
            End While
        ElseIf Session("bUseGList") Then 'jrs 2008/09/04 added GroupDec 'jrs 2006/01/17
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)
            j = 0
            While valtbl.Rows.Count > j
                lstDim.Items.Add(valtbl.Rows(j).Item("Description"))
                j = j + 1
            End While
        Else
            For i = 1 To MaxVal(1) - 1
                lstDim.Items.Add(GRPS$(1, i))
            Next i
        End If

        If lstDim.Items.Count > 0 Then
            lstDim.SelectedIndex = 0
            ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
        End If

    End Sub


    Private Sub LoadAllDimLists()
        LoadAllDims(lstDim, Session("AllDimsIdx"))

        ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
    End Sub

    'jrs 2008/09/04
    Private Sub mnuReg_Click()
        Session("bUseReg") = True
        Session("bUseGroup") = False
        Session("bUseGroupDec") = False
        Session("bUseGList") = False

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        ChangeGroupingOption()
        'jrs 2011/02/11
        lstDim.SelectedIndex = -1
        lstDim.Items(0).Selected = True
        ReDim Session("DimIdxs")(lstDim.Items.Count - 1)
        RefreshIdxS()
        PackJScritpBuilder()

    End Sub

    'jrs 2008/09/04
    Private Sub mnuGroup_Click()
        'jrs 2008/11/13
        Dim bSkip As Boolean
        bSkip = Session("bUseGroupDec")

        Session("bUseReg") = False
        Session("bUseGroup") = True
        Session("bUseGroupDec") = False
        Session("bUseGList") = False

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        If Not bSkip Then
            ChangeGroupingOption()
            'jrs 2011/02/11
            lstDim.SelectedIndex = -1
            lstDim.Items(0).Selected = True
            ReDim Session("DimIdxs")(lstDim.Items.Count - 1)
        Else
            Dim AuxIdx As Integer, DimIdx As Integer
            For DimIdx = 0 To lstDim.Items.Count - 1
                If lstDim.Items(DimIdx).Selected Then
                    AuxIdx = gGetGroupIdx(lstDim.Items(DimIdx).Text, False, Me)
                    Session("DimIdxs")(AuxIdx - 1) = True
                    lstDim.Items(DimIdx).Selected = False
                    lstDim.Items(AuxIdx - 1).Selected = True
                End If
            Next
        End If
        RefreshIdxS()
        'jrs 2010/12/10
        PackJScritpBuilder()

    End Sub

    'jrs 2008/09/04
    Private Sub mnuGroupDec_Click()
        'jrs 2008/11/13
        Dim bSkip As Boolean
        bSkip = Session("bUseGroup")

        Session("bUseReg") = False
        Session("bUseGroup") = False
        Session("bUseGroupDec") = True
        Session("bUseGList") = False

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        If Not bSkip Then
            ChangeGroupingOption()
            'jrs 2011/02/11
            lstDim.SelectedIndex = -1
            lstDim.Items(0).Selected = True
            ReDim Session("DimIdxs")(lstDim.Items.Count - 1)
        Else
            Dim AuxIdx As Integer, DimIdx As Integer
            For DimIdx = 0 To lstDim.Items.Count - 1
                If lstDim.Items(DimIdx).Selected Then
                    AuxIdx = gGetGroupIdx(lstDim.Items(DimIdx).Text, False, Me)
                    Session("DimIdxs")(AuxIdx - 1) = True
                    lstDim.Items(DimIdx).Selected = False
                    lstDim.Items(AuxIdx - 1).Selected = True
                End If
            Next
        End If
        RefreshIdxS()
        'jrs 2010/12/10
        PackJScritpBuilder()
    End Sub

    'jrs 2008/09/04
    Private Sub mnuGList_Click()
        Session("bUseReg") = False
        Session("bUseGroup") = False
        Session("bUseGroupDec") = False
        Session("bUseGList") = True

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "checkbox.gif"

        ChangeGroupingOption()
        'jrs 2011/02/11
        lstDim.SelectedIndex = -1
        lstDim.Items(0).Selected = True
        ReDim Session("DimIdxs")(lstDim.Items.Count - 1)
        RefreshIdxS()
        'jrs 2010/12/10
        PackJScritpBuilder()
    End Sub

    'jrs 2008/09/04
    Private Sub ChangeGroupingOption()

        Session("lstDimTag") = -1
        If rs.Rows(0).Item("Formula") Then
            Dim dbComp As String
            dbComp = Session("PutFdrvRun") + "IFSVAR.MDB"
            Dim rsComp As DataTable
            'jrs 2006/08/21 added Dim2, Dim3 to check if Dimensions should be flexible
            SetDataTablefromAccess(dbComp, "SELECT VarName, Dim2, Dim3 FROM VarListsFormulas WHERE FormulaId = " & CStr(rs.Rows(0).Item("FormulaId")), "", rsComp)
            Dim idxPos As Integer
            'jrs 2006/08/21 when formulas with doubly dimension variables are loaded check if second dimension should be flexible
            Dim sVar As String, bPar2 As Boolean, bPar3 As Boolean, sPar2 As String, sPar3 As String,
            idx As Integer, rr As Integer, sFlexVar As String, idxFlex As Integer
            bPar2 = True
            bPar3 = True
            rr = 0
            Do While Not rsComp.Rows.Count = rr
                idxPos = InStr(1, rsComp.Rows(rr).Item("VarName"), "[")
                sVar = Left(rsComp.Rows(rr).Item("VarName"), idxPos - 1)
                idx = IfsVars.Item(sVar).Index
                If idx < 0 Then
                    idx = 0
                End If
                If sFlexVar = "" Then
                    sFlexVar = sVar
                    idxFlex = idx
                Else
                    If VDim1(idx) > 0 And VDim1(idxFlex) = 0 Then
                        sFlexVar = sVar
                        idxFlex = idx
                    End If
                    If VDim2(idx) > 0 And VDim2(idxFlex) = 0 Then
                        sFlexVar = sVar
                        idxFlex = idx
                    End If
                    If VDim3(idx) > 0 And VDim3(idxFlex) = 0 Then
                        sFlexVar = sVar
                        idxFlex = idx
                    End If
                End If
                If bPar2 And VDim2(idx) > 0 And Not IsDBNull(rsComp.Rows(rr).Item("Dim2")) Then
                    If rsComp.Rows(rr).Item("Dim2") > "" Then
                        If sPar2 = "" Then
                            sPar2 = rsComp.Rows(rr).Item("Dim2")
                        Else
                            If sPar2 <> rsComp.Rows(rr).Item("Dim2") Then
                                bPar2 = False
                            End If
                        End If
                    End If
                End If
                If bPar3 And VDim3(idx) > 0 And Not IsDBNull(rsComp.Rows(rr).Item("Dim3")) Then
                    If rsComp.Rows(rr).Item("Dim3") > "" Then
                        If sPar3 = "" Then
                            sPar3 = rsComp.Rows(rr).Item("Dim3")
                        Else
                            If sPar3 <> rsComp.Rows(rr).Item("Dim3") Then
                                bPar3 = False
                            End If
                        End If
                    End If
                End If
                rr = rr + 1
            Loop

            'jrs 2011/02/09 LoadDims(sFlexVar, bPar2, bPar3)
        Else
            'jrs 2011/02/09 LoadDims(rs.Rows(0).Item("VarName"), CStr(IIf(IsDBNull(rs.Rows(0).Item("Param2")), "", rs.Rows(0).Item("Param2"))) = "", _
            'CStr(IIf(IsDBNull(rs.Rows(0).Item("Param3")), "", rs.Rows(0).Item("Param3"))) = "")
        End If


    End Sub

    'jrs 2008/01/30
    'loop throught the V-List searching for the dimensions to be loaded (originally from VarSelect)
    Private Sub SearchDimstoLoadforVList(ByVal sVar As String, ByRef svIdx As Object, ByRef bDim2 As Boolean, ByRef bDim3 As Boolean)
        Dim idx As Integer
        Dim sDim2 As String
        Dim sDim3 As String
        Dim rsList As DataTable
        Dim rsListIdx As Integer = 0
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT * FROM VarLists WHERE ListName = """ & sVar & """ ORDER BY ListOrder", "", rsList)
        Dim rsListComp As DataTable
        Dim rsListCompIdx As Integer
        bDim2 = True
        bDim3 = True
        svIdx = -1
        Dim MaxDimNum As Integer
        MaxDimNum = 0
        'jrs 2007/10/19
        Dim bPov As Boolean, bPovPct As Boolean, bPovFind As Boolean, PovLevel As Single
        'jrs 2011/06/17
        Dim bIncR As Boolean, PovLevel2 As Single
        'jrs 2014/10/21
        Dim b2005PPP As Boolean

        'jrs 2010/05/14
        Dim bMortChl As Boolean, bMortAdl As Boolean

        'loop each variable
        Do While Not (rsList.Rows.Count = rsListIdx)
            'read index
            idx = ReadIdx(rsList.Rows(rsListIdx).Item("VarName"))
            'jrs 2008/04/07 added in 2009/07/06
            If svIdx = -1 Then
                svIdx = idx
            End If

            'check dimensions
            If VDim1(idx) > 0 And MaxDimNum = 0 Then
                MaxDimNum = 1
                svIdx = idx
            End If
            CheckDims(bDim2, VDim2(idx), idx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param2")), "", rsList.Rows(rsListIdx).Item("Param2")), sDim2, MaxDimNum, svIdx, "", 2, False)
            CheckDims(bDim3, VDim3(idx), idx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param3")), "", rsList.Rows(rsListIdx).Item("Param3")), sDim3, MaxDimNum, svIdx, "", 3, False)

            'if formula loop each member
            If rsList.Rows(rsListIdx).Item("Formula") Then
                SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", "SELECT * FROM VarListsFormulas WHERE FormulaId = " & rsList.Rows(rsListIdx).Item("FormulaId") & " ORDER BY [CodeVar]", "", rsListComp)
                'jrs 2009/01/16
                rsListCompIdx = 0
                'check dimensions
                Do While Not (rsListComp.Rows.Count = rsListCompIdx)
                    idx = ReadIdx(rsListComp.Rows(rsListCompIdx).Item("VarName"))
                    If VDim1(idx) > 0 And MaxDimNum = 0 Then
                        MaxDimNum = 1
                        svIdx = idx
                    End If
                    CheckDims(bDim2, VDim2(idx), idx, IIf(IsDBNull(rsListComp.Rows(rsListCompIdx).Item("Dim2")), "", rsListComp.Rows(rsListCompIdx).Item("Dim2")),
                    sDim2, MaxDimNum, svIdx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param2")), "", rsList.Rows(rsListIdx).Item("Param2")), 2, True)
                    CheckDims(bDim3, VDim3(idx), idx, IIf(IsDBNull(rsListComp.Rows(rsListCompIdx).Item("Dim3")), "", rsListComp.Rows(rsListCompIdx).Item("Dim3")),
                    sDim3, MaxDimNum, svIdx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param3")), "", rsList.Rows(rsListIdx).Item("Param3")), 3, True)
                    rsListCompIdx = rsListCompIdx + 1
                Loop
            Else 'jrs 2007/10/19
                If idx < 1 Then
                    checkPovertyVarName(rsList.Rows(rsListIdx).Item("VarName"), bPov, bPovPct, bPovFind, PovLevel, bIncR, PovLevel2, b2005PPP) 'jrs 2011/06/17
                    If bPov Or bPovPct Or bPovFind Or bIncR Then 'jrs 2011/06/17
                        idx = IfsVars.Item("INCOMELT190LN").Index 'jrs 2016/10/12 changed to 190
                    Else 'jrs 2010/05/13 check Mortality Prob
                        checkMortProbName(rsList.Rows(rsListIdx).Item("VarName"), bMortChl, bMortAdl)
                        If bMortChl Or bMortAdl Then
                            idx = IfsVars.Item("DEATHCAT").Index
                        End If
                    End If

                    If idx > 0 Then
                        'check dimensions
                        If VDim1(idx) > 0 And MaxDimNum = 0 Then
                            MaxDimNum = 1
                            svIdx = idx
                        End If
                        CheckDims(bDim2, VDim2(idx), idx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param2")), "", rsList.Rows(rsListIdx).Item("Param2")), sDim2, MaxDimNum, svIdx, "", 2, False)
                        CheckDims(bDim3, VDim3(idx), idx, IIf(IsDBNull(rsList.Rows(rsListIdx).Item("Param3")), "", rsList.Rows(rsListIdx).Item("Param3")), sDim3, MaxDimNum, svIdx, "", 3, False)
                    End If
                End If
            End If
            rsListIdx = rsListIdx + 1
        Loop
        'if dimensions are different don't Load them, use the ones saved (except on Dim1).
    End Sub

    'jrs 2008/01/30 (originally from VarSelect)
    Private Sub CheckDims(ByRef bDim As Boolean, ByVal VDim As Integer, ByVal idx As Integer, ByVal rsPar As String, ByRef sDim As String,
    ByRef MaxDimNum As Integer, ByRef svIdx As Object, ByVal ss As String, ByVal Dim_Num As Integer, ByVal bFormula As Boolean)
        '1. bDim is the flag that will block loading this dimension if not needed.
        '2. VDim is the Dimension number, i.e. VDim2(idx) or VDim3(idx)
        '3. idx is the id of the Variable
        '4. rsPar is the Parameter stored with the list
        '5. sDim is the global variable that's used to check if the same value is used accross this dimension.
        '6. MaxDimNum Dimension number, that is number of dimensions a variable has
        '7. svIdx index of variable being analized
        '8. ss used for formulas, this is the parameter passed to the formula as a whole vs the parameters for each member of the formula
        '9. Dim_Num is the variable being analized (2nd or 3rd).
        '10. bFormula indicates if working with a Formula or not.
        If bDim And VDim > 0 Then
            If rsPar > "" And (ss = "" Or ss = rsPar) Then 'jrs 2010/12/16 added or ss=rsPar consider Google list with formulas for Deaths and DALYs across Comm. Dis, saving for Males, Females and Total
                If sDim = "" Then
                    sDim = rsPar
                    If Not bFormula Then
                        bDim = False
                    End If
                ElseIf sDim <> rsPar Then
                    bDim = False
                End If
            End If
            If MaxDimNum < Dim_Num Then
                MaxDimNum = Dim_Num
                svIdx = idx
            End If
        End If
    End Sub

    'jrs 2008/01/30 originally from VarSelect
    Private Function ReadIdx(ByVal rsVar As String) As Integer
        Dim pp As Integer
        Dim ss As String
        pp = InStr(rsVar, "[") - 1
        If pp > 0 Then
            ss = Mid(rsVar, 1, pp)
        Else
            ss = rsVar
        End If
        ReadIdx = IfsVars.Item(ss).Index
        If ReadIdx < 0 Then
            ReadIdx = 0
        End If
    End Function



    'jrs 2010/12/10
    'Procedure to build array with dimensions to be loaded for each packaged list
    Private Sub PackJScritpBuilder()
        'Dim rsL As DataTable
        'Dim sql As String
        'sql = "SELECT Dim1, Dim2, Dim3, bDim2, bDim3 FROM VarListsHDR"
        'If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
        '    sql = sql & " WHERE EXISTS (SELECT * FROM VarLists as L WHERE L.ListName = VarListsHDR.ListName " & _
        '    "AND L.HistoryplusForecast = False)"
        'Else
        '    sql = sql & " WHERE EXISTS (SELECT * FROM VarLists as L WHERE L.ListName = VarListsHDR.ListName " & _
        '    "AND L.HisCompEmpFor = False)"
        'End If

        'sql = sql & " ORDER BY ListName"
        'SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rsL)

        Dim sJscript As String, i As Integer
        'dynamically load and add this .js file
        sJscript = "   loadjsfile(""Scripts/CyberDisplaysVars.js"");" & vbCr

        'sJscript = "    var DimXList = [];" & vbCr
        'For i = 0 To rsL.Rows.Count - 1
        '    sJscript = sJscript & "    DimXList[" & i.ToString & "] = new Array(" & DimStrBuilder(i, rsL) & ");" & vbCr
        'Next
        ''jrs 2011/04/15 adding Categories
        'sJscript = sJscript & vbCr & "    var CatXList = [];" & vbCr
        'sql = "SELECT DISTINCT ListName, Category FROM VarLists"
        'If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
        '    sql = sql & " WHERE HistoryplusForecast = False"
        'Else
        '    sql = sql & " WHERE HisCompEmpFor = False"
        'End If
        'sql = sql & " AND Type = 'V'  ORDER BY ListName"
        'SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rsL)
        ''jrs 2012/12/06
        ''For i = 0 To rsL.Rows.Count - 1
        ''    sJscript = sJscript & "    CatXList[" & i.ToString & "] = new Array(""" & rsL.Rows(i).Item("ListName") & _
        ''    """,""" & rsL.Rows(i).Item("Category") & """," & _
        ''    CType(Session("CategoryIds"), Collection).Item(rsL.Rows(i).Item("Category")).ToString & ");" & vbCr
        ''Next
        'i = 0
        'Dim str As String, catrow As String, j As String, x As Object
        'j = 0
        'Do While True
        '    If j = rsL.Rows.Count Then Exit Do
        '    Str = IIf(IsDBNull(rsL.Rows(j).Item("Category")), "", rsL.Rows(j).Item("Category"))
        '    x = Split(rsL.Rows(j).Item("Category"), ",")
        '    'On Error Resume Next
        '    catrow = ""
        '    For i = 0 To UBound(x)
        '        If CType(Session("CategoryIds"), Collection).Contains(Trim(x(i))) Then
        '            If catrow > "" Then
        '                catrow = catrow & ","
        '            End If
        '            catrow = catrow & CType(Session("CategoryIds"), Collection).Item(Trim(x(i))).ToString
        '        End If
        '    Next i
        '    'On Error GoTo 0
        '    If UBound(x) = -1 Then
        '        catrow = CType(Session("CategoryIds"), Collection).Item(Trim(str)).ToString
        '    End If

        '    sJscript = sJscript & "    CatXList[" & j.ToString & "] = new Array(""" & rsL.Rows(j).Item("ListName") & _
        '    """,""" & rsL.Rows(j).Item("Category") & """,""" & _
        '    catrow & """);" & vbCr

        '    j = j + 1
        'Loop

        'sJscript = sJscript & vbCr & "    var listCount=" & rsL.Rows.Count.ToString & ";" & vbCr
        'sJscript = sJscript & "    var MapToList = [];" & vbCr
        'sJscript = sJscript & "    var FilterListCount=" & rsL.Rows.Count.ToString & ";" & vbCr

        ''3 other variables for the tag
        'sJscript = sJscript & vbCr & "    var currDim1=0;" & vbCr
        'sJscript = sJscript & "    var currDim2=0;" & vbCr
        'sJscript = sJscript & "    var currDim3=0;" & vbCr

        'Grouping variables
        sJscript = sJscript & vbCr & "   var bUseReg=" & LCase(Session("bUseReg")) & ";" & vbCr
        sJscript = sJscript & vbCr & "   var bUseGroup=" & LCase(Session("bUseGroup")) & ";" & vbCr
        sJscript = sJscript & "   var bUseGroupDec=" & LCase(Session("bUseGroupDec")) & ";" & vbCr
        sJscript = sJscript & "   var bUseGList=" & LCase(Session("bUseGList")) & ";" & vbCr

        ''Dimension arrays
        'sJscript = sJscript & vbCr & "   var MaxVal=[];" & vbCr
        'For i = 1 To UBound(MaxVal)
        '    sJscript = sJscript & "    MaxVal[" & i.ToString & "] = " & MaxVal(i).ToString & ";" & vbCr
        'Next
        'sJscript = sJscript & vbCr & "   var GRPS=[];" & vbCr
        'For i = 1 To UBound(GRPS, 1)
        '    sJscript = sJscript & "    GRPS[" & i.ToString & "] = new Array(" & DimStrBuilder(i) & ");" & vbCr
        'Next
        'sJscript = sJscript & vbCr & "   var Groups=[];" & vbCr
        'For i = 1 To GCount
        '    sJscript = sJscript & "    Groups[" & i.ToString & "] = """ & GroupNames$(i) & """;" & vbCr
        'Next
        'sJscript = sJscript & "    var iGroupCount=" & GCount.ToString & ";" & vbCr
        'sJscript = sJscript & vbCr & "   var GLists=[];" & vbCr
        'For i = 1 To GListCount
        '    sJscript = sJscript & "    GLists[" & i.ToString & "] = """ & GListNames(i) & """;" & vbCr
        'Next
        'sJscript = sJscript & "    var iGListCount=" & GListCount.ToString & ";" & vbCr

        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadDimXList",
        sJscript, True)

        'lstVars.Attributes.Add("onchange", "lstVars_click();")

        'jrs 2012/12/06 need argument event to make it work in FireFox, also the Drag doesn't work in Chrome and Firefox so they need onchange
        'jrs 2011/11/03
        lstFiles.Attributes.Add("onclick", "lstFiles_Click(event);")
        lstFiles.Attributes.Add("onmousedown", "lstFiles_ClickDown(event);")
        lstFiles.Attributes.Add("onmouseup", "lstFiles_ClickUp(event);")
        If Request.Browser.Browser = "Chrome" Then
            lstFiles.Attributes.Add("onchange", "lstFiles_Change(event);")
        ElseIf Request.Browser.Browser = "Firefox" Then
            lstFiles.Attributes.Add("onchange", "lstFiles_Click(event);")
        End If

        'jrs 2012/12/05
        lstDim.Attributes.Add("onclick", "lstDim_Click(event);")
        lstDim.Attributes.Add("onmousedown", "lstDim_ClickDown(event);")
        lstDim.Attributes.Add("onmouseup", "lstDim_ClickUp(event);")
        'If Request.Browser.Browser = "Chrome" Then
        lstDim.Attributes.Add("onchange", "lstDim_Change(event);")
        'ElseIf Request.Browser.Browser = "Firefox" Then
        'lstDim.Attributes.Add("onchange", "lstDim_Click(event);")
        'End If
    End Sub

    'jrs 2013/05/09 moved to Function2
    ''jrs 2010/12/10
    ''Procedure to build a string with the dimensions to be used when selecting a package
    'Private Function DimStrBuilder(ByVal i As Integer, ByRef rs As DataTable) As String
    '    'i is the index of the list to be used
    '    DimStrBuilder = rs.Rows(i).Item("Dim1").ToString & "," & rs.Rows(i).Item("Dim2").ToString & "," & _
    '    rs.Rows(i).Item("Dim3").ToString & "," & Convert.ToInt32(rs.Rows(i).Item("bDim2")).ToString & "," & _
    '    Convert.ToInt32(rs.Rows(i).Item("bDim3")).ToString
    'End Function

    'jrs 2013/05/09 moved to Function2
    'Private Function DimStrBuilder(ByVal i As Integer) As String
    '    'i is the index of the list to be used
    '    DimStrBuilder = """"""
    '    Dim j As Integer
    '    For j = 1 To MaxVal(i) - 1
    '        DimStrBuilder = DimStrBuilder & ",""" & GRPS(i, j).ToString & """"
    '    Next
    'End Function

    'jrs 2011/01/19
    'Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
    '    Page.ClientScript.RegisterForEventValidation(lstDim3.UniqueID, "")
    '    MyBase.Render(writer)
    'End Sub

    'jrs 2011/02/11
    Private Sub SaveDimIdxs()
        'bRefresh is true when the page will be refreshed immediately (without going to another page)
        Dim DimIdx As Integer, AuxIdx As Integer
        'First Dimension
        If VDim1(Session("SelIdx")) > 0 Then
            For DimIdx = 0 To lstDim.Items.Count - 1
                If lstDim.Items(DimIdx).Selected Then
                    If Session("bUseGroup") Or Session("bUseGroupDec") Then
                        AuxIdx = gGetGroupIdx(lstDim.Items(DimIdx).Text, False, Me)
                    ElseIf Session("bUseGList") Then
                        AuxIdx = gGetGListIdx(lstDim.Items(DimIdx).Text, False, Me)
                    ElseIf lstDim.Items(DimIdx).Text = GRPS(VDim1(Session("SelIdx")), Session("AllDimsIdx")(DimIdx) + 1) Then 'jrs 2012/12/14 added + 1
                        AuxIdx = Session("AllDimsIdx")(DimIdx) + 1
                    Else
                        For AuxIdx = MAXCOM(VDim1(Session("SelIdx"))) - 1 To 1 Step -1
                            If lstDim.Items(DimIdx).Text = GRPS(VDim1(Session("SelIdx")), AuxIdx) Then
                                Exit For
                            End If
                        Next AuxIdx
                    End If
                    If AuxIdx = 0 Then
                        AuxIdx = 1
                    End If
                    Session("DimIdxs")(AuxIdx - 1) = True
                    lstDim.Items(DimIdx).Selected = False
                    lstDim.Items(AuxIdx - 1).Selected = True
                End If
            Next DimIdx
        End If
    End Sub

    'jrs 2011/05/03
    Protected Sub optReg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optReg.CheckedChanged
        If optReg.Checked Then
            Session("bUseReg") = True
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
        End If
    End Sub

    Protected Sub optGList_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGList.CheckedChanged
        If optGList.Checked Then
            Session("bUseReg") = False
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = True
        End If
    End Sub


    Protected Sub optGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGroup.CheckedChanged
        If optGroup.Checked Then
            Session("bUseReg") = False
            Session("bUseGroup") = True
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
        End If
    End Sub

    Protected Sub optGroupDec_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGroupDec.CheckedChanged
        If optGroupDec.Checked Then
            Session("bUseReg") = False
            Session("bUseGroup") = False
            Session("bUseGroupDec") = True
            Session("bUseGList") = False
        End If
    End Sub





    Protected Sub lstFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        'jrs 2011/11/04
        Session("txtFiles") = txtFiles.Value
    End Sub

    'jrs 2012/03/09
    Protected Sub cmdHorizon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorizon.Click
        If Not IsNumeric(txtHorizon.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        If Session("Interval") > 1 Then
            If (CInt(txtHorizon.Text) - Session("FirstYear")) Mod Session("Interval") <> 0 Then
                LoadMessage("Invalid Year, year must follow the existing Interval, please use the combo box.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If

        cmb_Horizon.Text = txtHorizon.Text
        cmb_Horizon_SelectedIndexChanged(Me, New EventArgs)

    End Sub



    'jrs 2013/04/18
    Private Sub ReOrderForScatterPlot(ByVal DimCount As Integer)
        'Copy source
        Dim pCopy As Object, VARNCopy As Object, gr1Copy As Object, gr2Copy As Object,
        gr3Copy As Object, gr4Copy As Object, fCopy As Object,
        FileNumbrCopy As Object, FinYearCopy As Object, FinYearHisCopy As Object,
        bNotDecomposablesCopy As Object, bLineFeedsCopy As Object,
        sGroupTitlesCopy As Object, sUsrDefCatsCopy As Object, DispDecimalsCopy As Object
        'splot and focus
        Dim FDNG1Copy As Object, FDNG2Copy As Object, FDNG3Copy As Object, FDNG4Copy As Object

        pCopy = VB6.Support.CopyArray(Session("PLOT"))
        VARNCopy = VB6.Support.CopyArray(Session("VARN"))
        gr1Copy = VB6.Support.CopyArray(Session("GR1N"))
        gr2Copy = VB6.Support.CopyArray(Session("GR2N"))
        gr3Copy = VB6.Support.CopyArray(Session("GR3N"))
        gr4Copy = VB6.Support.CopyArray(Session("GR4N"))
        fCopy = VB6.Support.CopyArray(Session("aFormulas"))

        FDNG1Copy = VB6.Support.CopyArray(Session("FDNG1"))
        FDNG2Copy = VB6.Support.CopyArray(Session("FDNG2"))
        FDNG3Copy = VB6.Support.CopyArray(Session("FDNG3"))
        FDNG4Copy = VB6.Support.CopyArray(Session("FDNG4"))

        FileNumbrCopy = VB6.Support.CopyArray(Session("FileNumbr"))
        FinYearCopy = VB6.Support.CopyArray(Session("FinYear"))
        FinYearHisCopy = VB6.Support.CopyArray(Session("FinYearHis"))

        'Reset Target
        Dim j As Integer, k As Integer
        Dim focus As Integer
        For i = 1 To Session("COUNTE")
            'source index
            If i <= Session("COUNTE") / 2 Then
                k = 2 * (i - 1) + 1
            Else
                k = 2 * (i - DimCount - 1) + 2
            End If
            'copy to target
            For j = 0 To UBound(Session("PLOT"), 1)
                Session("PLOT")(j, i) = pCopy(j, k)
            Next j

            For j = 0 To UBound(Session("VARN"), 1)
                Session("VARN")(j, i) = VARNCopy(j, k)
            Next j
            Session("GR1N")(i) = gr1Copy(k)
            Session("GR2N")(i) = gr2Copy(k)
            Session("GR3N")(i) = gr3Copy(k)
            Session("GR4N")(i) = gr4Copy(k)
            If k <= UBound(fCopy) Then
                If i > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(i)
                End If
                Session("aFormulas")(i) = fCopy(k)
            End If
            Session("FDNG1")(i) = FDNG1Copy(k)
            Session("FDNG2")(i) = FDNG2Copy(k)
            Session("FDNG3")(i) = FDNG3Copy(k)
            Session("FDNG4")(i) = FDNG4Copy(k)
            Session("FileNumbr")(i) = FileNumbrCopy(k)
            Session("FinYear")(i) = FinYearCopy(k)
            Session("FinYearHis")(i) = FinYearHisCopy(k)
        Next i

    End Sub

    Private Sub LoadVariables()
        lstVars.Items.Clear()
        Dim rsL As DataTable
        Dim str As String
        Dim sql As String
        'jrs 2015/03/31
        'sql = "SELECT NAME, DEFINITION FROM IFSVAR WHERE TYPE=1 AND NAME LIKE 'ICT*' ORDER BY DEFINITION"
        'remember that in ADO.NET LIKE 'ICT*' doesn't work, it has to be LIKE 'ICT%'
        sql = "SELECT NAME, DEFINITION FROM IFSVAR WHERE TYPE=1 AND NAME LIKE 'ICT%' ORDER BY DEFINITION"
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rsL)
        ReDim Session("VarNames")(rsL.Rows.Count)
        Dim i As Integer
        i = 0
        ReDim Session("CostBenefitIdx")(5)

        Do While (Not rsL.Rows.Count = i)
            lstVars.Items.Add(rsL.Rows(i).Item("DEFINITION"))
            Session("VarNames")(i) = rsL.Rows(i).Item("NAME")
            Select Case Session("VarNames")(i)
                Case "ICTCYBSECSPEND"
                    Session("CostBenefitIdx")(1) = i
                Case "ICTCYBEVTCOST"
                    Session("CostBenefitIdx")(2) = i
                Case "ICTCYBOPCOST"
                    Session("CostBenefitIdx")(3) = i
                Case "ICTCYBBENEFIT"
                    Session("CostBenefitIdx")(4) = i
                Case "ICTCONSURPLUS"
                    Session("CostBenefitIdx")(5) = i
            End Select

            i = i + 1
        Loop
        If lstVars.Items.Count > 0 Then
            'jrs 2005/04/20
            If Not (Session("CyberVarsIdxs") Is Nothing) And Not Page.IsPostBack Then
                'jrs 2008/12/18 lstvars.SelectedIndex = Session("PDFlexPacksIdx")
                For i = 0 To lstVars.Items.Count - 1
                    lstVars.Items(i).Selected = Session("CyberVarsIdxs")(i)
                Next
            Else
                lstVars.SelectedIndex = 0
            End If
            LoadAllDimLists()
            'jrs 2010/12/10
            PackJScritpBuilder()
        End If

        'Load Parameters Names
        lstPars.Items.Clear()
        lstPars.Enabled = False
        Dim rsP As DataTable
        'idx = IfsVars.Item("IALK").Index
        sql = "SELECT NAME, DEFINITION FROM IFSVAR WHERE TYPE > 1 AND NAME LIKE 'ict%' ORDER BY DEFINITION"
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", sql, "", rsP)
        ReDim Session("ParNames")(rsL.Rows.Count)
        i = 0
        'For i = idx To NumbVars
        Do While (Not rsL.Rows.Count = i)
            'If mid(v(i), 1, 3) = "ict" Then
            lstPars.Items.Add(rsP.Rows(i).Item("DEFINITION")) ' VDef(i)
            Session("ParNames")(i) = rsP.Rows(i).Item("NAME") ' v(i)
            If Session("ParNames")(i) = "ictcybevcost" Then
                Session("CostIdx") = i
            ElseIf Session("ParNames")(i) = "ictcybevprob" Then
                Session("ProbIdx") = i
            End If
            i = i + 1
            '        ElseIf mid(v(i), 1, 3) = "IDS" Then
            '            Exit For
            '        End If
            '    Next i
        Loop

        If lstPars.Items.Count > 0 Then
            'jrs 2005/04/20
            If Not (Session("CyberParsIdxs") Is Nothing) And Not Page.IsPostBack Then
                'jrs 2008/12/18 lstvars.SelectedIndex = Session("PDFlexPacksIdx")
                For i = 0 To lstPars.Items.Count - 1
                    lstPars.Items(i).Selected = Session("CyberParsIdxs")(i)
                Next
            Else
                lstPars.SelectedIndex = 0
            End If
        End If

        'Load Historical Data
        Dim rsDD As DataTable
        sql = "SELECT Variable, Definition FROM DataDict WHERE (UCASE(Definition) LIKE '%ICT%' OR UCASE(Variable) LIKE '%ICT%*') " &
        "AND (Years LIKE '%-%' OR Years LIKE '%,%') ORDER BY Definition"
        SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.mdb", sql, "", rsDD)
        ReDim Session("VarNamesH")(rsDD.Rows.Count)

        Do While (Not rsL.Rows.Count = i)
            Session("VarNamesH")(i) = rsDD.Rows(i).Item("Variable")
            lstVarsH.Items.Add(rsDD.Rows(i).Item("Definition"))
            i = i + 1
        Loop

        If lstVarsH.Items.Count > 0 Then
            'jrs 2005/04/20
            If Not (Session("CyberVarsHIdxs") Is Nothing) And Not Page.IsPostBack Then
                'jrs 2008/12/18 lstvars.SelectedIndex = Session("PDFlexPacksIdx")
                For i = 0 To lstVarsH.Items.Count - 1
                    lstVarsH.Items(i).Selected = Session("CyberVarsIdxs")(i)
                Next
            Else
                lstVarsH.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub LoadSet()
        Dim f As Integer
        Dim r As Integer, T As Integer, g As Integer, y As Integer, v As Integer
        Dim dbM As String
        Dim bGrp As Boolean
        Dim dbG As String
        Dim rsG As DataTable
        Dim sDim1 As String
        Dim iDim1 As Integer
        Dim iGL As Integer

        Dim lstV As ListBox
        If lstPars.Enabled Then
            lstV = lstPars
        Else
            lstV = lstVars
        End If

        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        dbG = Session("IFs_MDB2Active")

        'jrs 2011/01/20
        For DimIdx = 0 To lstDim.Items.Count - 1
            Session("DimIdxs")(DimIdx) = False
        Next

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

        'jrs 2012/12/05
        'Create Collection based on Order passed or listBox
        Dim OrdDims As New Collection
        Dim Location As Object
        If Trim(txtDims.Value) > "" Then
            'jrs 2013/10/01
            Dim DimAll As Integer

            Dim FDs As Object
            FDs = Split(txtDims.Value, ",")
            For DimIdx = 0 To UBound(FDs)
                'jrs 2013/10/01 Convert Dimensions, they bring single index 
                'need cumulative index when all is loaded
                DimAll = ConvertFromSimpleIndexToComplexForDim(CInt(FDs(DimIdx)), Session("bUseGroup") Or Session("bUseGroupDec"), Session("bUseGList"), False, False)
                OrdDims.Add(DimAll, DimAll.ToString)
            Next
        Else
            For DimIdx = 0 To lstDim.Items.Count - 1
                If lstDim.Items(DimIdx).Selected Then
                    OrdDims.Add(DimIdx, DimIdx.ToString)
                End If
            Next
        End If


        'loop through regions
        'For r = 0 To lstDim.Items.Count - 1
        For Each Location In OrdDims
            r = CInt(Location)
            sDim1 = lstDim.Items(r).Text
            Session("DimIdxs")(r) = True
            iDim1 = r + 1
            'for later: consider g-lists and decomposed groups
            Select Case True
                Case Session("bUseReg")
                    bGrp = False
                Case Session("bUseGroup")
                    bGrp = True
                Case Session("bUseGroupDec")
                    bGrp = False
                    SetDataTablefromAccess(dbG, "SELECT * FROM " & GroupIDNew & " i, " &
                    Grouping & " d WHERE i.GroupNum = d.GroupNo AND i.GroupName = """ &
                    sDim1 & """ ORDER BY Region", "", rsG)
                    iGL = 0
                Case Session("bUseGList")
                    SetDataTablefromAccess(dbG, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ &
                    sDim1 & """ ORDER BY MemberOrder, Region", "", rsG)
                    iGL = 0
            End Select

            Do While True 'loop for G-Lists and Dec. Groups
                If Session("bUseGList") Or Session("bUseGroupDec") Then
                    sDim1 = rsG.Rows(iGL).Item("Region")
                    iDim1 = IFsRegs.Item(sDim1).Index
                    If Session("bUseGList") Then
                        bGrp = rsG.Rows(iGL).Item("Group")
                    End If
                End If
                'loop through variables
                For v = 0 To lstV.Items.Count - 1
                    If lstV.Items(v).Selected Then
                        'loop through files
                        'For f = 0 To lstFiles.Items.Count - 1
                        For Each sceFile In SceOrdFiles
                            f = CInt(sceFile)
                            If lstFiles.Items(f).Selected Then
                                Session("PDFilesIdxs")(f) = True
                                If Session("TotalGraphs") < MaxVariables Then
                                    LoadCyberVar(f, sDim1, iDim1, bGrp, v)
                                End If
                            End If 'if file selected
                        Next sceFile
                    End If 'if variable is selected
                Next v
                If Session("bUseGList") Or Session("bUseGroupDec") Then
                    iGL = iGL + 1
                    If iGL = rsG.Rows.Count Then Exit Do
                Else
                    Exit Do
                End If
            Loop
        Next Location
    End Sub

    Private Sub LoadCyberVar(ByVal iFile As Integer, ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal iVar As Integer)
        Dim i As Integer, j As Integer
        Session("COUNTE") = Session("COUNTE") + 1
        Dim sVar As String
        If lstPars.Enabled Then
            sVar = Session("ParNames")(iVar)
        Else
            sVar = Session("VarNames")(iVar)
        End If
        Dim VarIdx As Integer
        VarIdx = IfsVars.Item(sVar).Index

        If sVar = "ICTCYBEVTCOST" And iFile = 0 Then
            'Need to compute prob x cost across the 12 cats
            EstimateEVTCOST()
            Session("GR1N")(Session("COUNTE")) = sReg
            Session("FDNG1")(Session("COUNTE")) = Session("GR1N")(Session("COUNTE"))
        ElseIf sVar = "ictcybevprob" Or sVar = "ictcybevcost" Then  'Add logic for Prob or Cost
            'Load all dimensions
            Session("COUNTE") = Session("COUNTE") - 1
            If iFile = 0 Then
                LoadSetSCE(sVar)
            Else
                Dim MyFileforDisplay As IStorage
                Dim MyDF As IDataframe

                For i = 1 To MAXCOM(VDim1(VarIdx)) - 1
                    For j = 1 To MAXCOM(VDim2(VarIdx)) - 1
                        Session("COUNTE") = Session("COUNTE") + 1
                        gLoadForDisplayNET(sVar, VarIdx, "", i, "", j, "", -1, "", iFile, (i = 1 And j = 1), (i = MAXCOM(VDim1(VarIdx)) - 1 And j = MAXCOM(VDim2(VarIdx)) - 1), False, 1, Session("PLOT"), Session("COUNTE"),
                        1, Session("FirstYear"), Session("LastYear"), 0, False,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                        'gLoadForDisplay(sVar, VarIdx, "", i, "", j, "", -1, "", iFile, (i = 1 And j = 1), (i = MAXCOM(VDim1(VarIdx)) - 1 And j = MAXCOM(VDim2(VarIdx)) - 1), False, 1, Session("PLOT"), Session("COUNTE"),
                        '1, Session("FirstYear"), Session("LastYear"), 0, False,
                        'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                        'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        gLoadForDisplaySaveLbls(Session("COUNTE"), VarIdx, GRPS(VDim1(VarIdx), i), GRPS(VDim2(VarIdx), j), "", False, iFile, "",
                        Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                    Next j
                Next i
            End If
        Else
            Dim sDim2 As String
            sDim2 = ""
            j = -1
            gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, j, "", -1, "", iFile, True, True, bGrp, 1, Session("PLOT"), Session("COUNTE"),
            1, Session("FirstYear"), Session("LastYear"), 0, False,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
            'gLoadForDisplay(sVar, VarIdx, sReg, iReg, sDim2, j, "", -1, "", iFile, True, True, bGrp, 1, Session("PLOT"), Session("COUNTE"),
            '1, Session("FirstYear"), Session("LastYear"), 0, False,
            'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            'Session("IFs_MDB2Active"), Session("CurrencyVal"))
            If j = -1 Then
                j = 0
            End If
            gLoadForDisplaySaveLbls(Session("COUNTE"), VarIdx, GRPS(VDim1(VarIdx), iReg), GRPS(VDim2(VarIdx), j), "", False, iFile, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        End If
    End Sub

    'jrs 2015/02/16
    Private Sub EstimateEVTCOST()
        Dim ProbIdx As Integer, CostIdx As Integer, MaxVdim1 As Integer, MaxVdim2 As Integer
        ProbIdx = IfsVars.Item("ictcybevprob").Index
        CostIdx = IfsVars.Item("ictcybevcost").Index
        MaxVdim1 = MAXCOM(VDim1(ProbIdx)) - 1
        MaxVdim2 = MAXCOM(VDim2(ProbIdx)) - 1
        Dim bProb(0, 0) As Boolean, bCost(0, 0) As Boolean
        ReDim Session("Prob")(MaxVdim1, MaxVdim2, Session("LastYear") - Session("FirstYear") + 1)
        ReDim Session("Cost")(MaxVdim1, MaxVdim2, Session("LastYear") - Session("FirstYear") + 1)
        ReDim bProb(MaxVdim1, MaxVdim2)
        ReDim bCost(MaxVdim1, MaxVdim2)
        Dim i As Integer, j As Integer, y As Integer

        '1. Need to check if there's sce file
        If Dir(Session("ScenarioFileDir") + "Working.Sce") > "" Then
            '2. if there is, look for prob and cost, and load them
            Dim sLine As String
            FileOpen(1, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Input)
            Do While Not EOF(1)
                sLine = LineInput(1)
                If InStr(sLine, "ictcybevprob") > 0 Or InStr(sLine, "ictcybevcost") > 0 Then
                    Dim x() As String
                    x = Split(sLine, ",")
                    i = gGetDimIdx(VDim1(ProbIdx), x(2), True, Me)
                    j = gGetDimIdx(VDim2(ProbIdx), x(3), True, Me)
                    If InStr(sLine, "ictcybevprob") > 0 Then
                        bProb(i, j) = True
                        For y = 4 To UBound(x)
                            Session("Prob")(i, j, y - 3) = x(y)
                        Next y
                    Else
                        bCost(i, j) = True
                        For y = 4 To UBound(x)
                            Session("Cost")(i, j, y - 3) = x(y)
                        Next y
                    End If
                End If
            Loop
            FileClose(1)
        End If
        '3. if any of the 12 cats is not found in sce file, then need to open base case to load
        Dim VarVals() As Single
        ReDim VarVals(Session("LastYear") - Session("FirstYear") + 1)
        For i = 1 To MaxVdim1
            For j = 1 To MaxVdim2
                If Not bProb(i, j) Then
                    gLoadForDisplayNET("ictcybevprob", ProbIdx, "", i, "", j, "", -1, "", 1, True, True, False, 1, VarVals, 1,
                    1, Session("FirstYear"), Session("LastYear"), 0, True,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    'gLoadForDisplay("ictcybevprob", ProbIdx, "", i, "", j, "", -1, "", 1, True, True, False, 1, VarVals, 1,
                    '1, Session("FirstYear"), Session("LastYear"), 0, True,
                    'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    For y = 1 To Session("LastYear") - Session("FirstYear") + 1
                        Session("Prob")(i, j, y) = VarVals(y)
                    Next y
                End If
                If Not bCost(i, j) Then
                    gLoadForDisplayNET("ictcybevcost", CostIdx, "", i, "", j, "", -1, "", 1, True, True, False, 1, VarVals, 1,
                    1, Session("FirstYear"), Session("LastYear"), 0, True,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    'gLoadForDisplay("ictcybevcost", CostIdx, "", i, "", j, "", -1, "", 1, True, True, False, 1, VarVals, 1,
                    '1, Session("FirstYear"), Session("LastYear"), 0, True,
                    'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    For y = 1 To Session("LastYear") - Session("FirstYear") + 1
                        Session("Cost")(i, j, y) = VarVals(y)
                    Next y
                End If
            Next j
        Next i
        '4. Compute sum product of the 12 cats and Save to session("PLOT") vector
        For y = 1 To Session("LastYear") - Session("FirstYear") + 1
            Session("PLOT")(y, Session("COUNTE")) = 0
            For i = 1 To MaxVdim1
                For j = 1 To MaxVdim2
                    Session("PLOT")(y, Session("COUNTE")) = Session("PLOT")(y, Session("COUNTE")) + Session("Prob")(i, j, y) * Session("Cost")(i, j, y)
                Next j
            Next i
        Next y
        '5. Save other labels
        Session("FinYear")(Session("COUNTE")) = Session("LastYear") - Session("FirstYear") + 1
        Dim idxVar As Integer
        idxVar = IfsVars.Item("ICTCYBEVTCOST").Index

        Session("VARN")(0, Session("COUNTE")) = V$(idxVar)
        Session("VARN")(1, Session("COUNTE")) = V$(idxVar)
        Session("VARN")(2, Session("COUNTE")) = VDef$(idxVar)
        Session("VARN")(3, Session("COUNTE")) = VHelpID(idxVar)
        Session("VARN")(6, Session("COUNTE")) = VHelpIDEqn(idxVar)

        Session("VARN")(5, Session("COUNTE")) = "REGION"

        If Session("SensitivityFlag") <> True Then
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "[Working]"
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "[0]"
        End If

        Session("GR2N")(Session("COUNTE")) = ""
        Session("GR3N")(Session("COUNTE")) = ""

        Session("GR4N")(Session("COUNTE")) = VUnits(idxVar) 'Other Grid Values determined earlier

        'Flexible Dimension Names for Graphs
        Session("FDNG2")(Session("COUNTE")) = Session("GR2N")(Session("COUNTE"))
        Session("FDNG3")(Session("COUNTE")) = Session("GR3N")(Session("COUNTE"))
        Session("FDNG4")(Session("COUNTE")) = Session("GR4N")(Session("COUNTE"))

    End Sub

    Private Sub LoadSetSCE(sVar As String)
        Dim idx As Integer, MaxVdim1 As Integer, MaxVdim2 As Integer
        idx = IfsVars.Item(sVar).Index
        MaxVdim1 = MAXCOM(VDim1(idx)) - 1
        MaxVdim2 = MAXCOM(VDim2(idx)) - 1
        Dim bVar(0, 0) As Boolean, Vals(0, 0, 0) As Single
        ReDim Vals(MaxVdim1, MaxVdim2, Session("LastYear") - Session("FirstYear") + 1)
        ReDim bVar(MaxVdim1, MaxVdim2)
        Dim i As Integer, j As Integer, y As Integer

        '1. Need to check if there's sce file
        If Dir(Session("ScenarioFileDir") + "Working.Sce") > "" Then
            '2. if there is, look for Vals and cost, and load them
            Dim sLine As String
            FileOpen(1, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Input)
            Do While Not EOF(1)
                sLine = LineInput(1)
                If InStr(sLine, sVar) > 0 Then
                    Dim x() As String
                    x = Split(sLine, ",")
                    i = gGetDimIdx(VDim1(idx), x(2), True, Me)
                    j = gGetDimIdx(VDim2(idx), x(3), True, Me)
                    bVar(i, j) = True
                    For y = 4 To UBound(x)
                        Vals(i, j, y - 3) = x(y)
                    Next y
                End If
            Loop
            FileClose(1)
        End If
        '3. if any of the 12 cats is not found in sce file, then need to open base case to load
        Dim VarVals() As Single
        ReDim VarVals(Session("LastYear") - Session("FirstYear") + 1)
        For i = 1 To MaxVdim1
            For j = 1 To MaxVdim2
                If Not bVar(i, j) Then
                    gLoadForDisplayNET("ictcybevVals", idx, "", i, "", j, "", -1, "", 1, True, True, False, 1, VarVals, 1,
                    1, Session("FirstYear"), Session("LastYear"), 0, True,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    'gLoadForDisplay("ictcybevVals", idx, "", i, "", j, "", -1, "", 1, True, True, False, 1, VarVals, 1,
                    '1, Session("FirstYear"), Session("LastYear"), 0, True,
                    'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    For y = 1 To Session("LastYear") - Session("FirstYear") + 1
                        Vals(i, j, y) = VarVals(y)
                    Next y
                End If
            Next j
        Next i
        '4. Move to session("PLOT") vector
        For i = 1 To MaxVdim1
            For j = 1 To MaxVdim2
                Session("COUNTE") = Session("COUNTE") + 1
                For y = 1 To Session("LastYear") - Session("FirstYear") + 1
                    Session("PLOT")(y, Session("COUNTE")) = Vals(i, j, y)
                Next y
                SaveLabels(idx)
                Session("GR1N")(Session("COUNTE")) = GRPS(VDim1(idx), i)
                Session("FDNG1")(Session("COUNTE")) = Session("GR1N")(Session("COUNTE"))
                Session("GR2N")(Session("COUNTE")) = GRPS(VDim2(idx), j)
                Session("FDNG2")(Session("COUNTE")) = Session("GR2N")(Session("COUNTE"))
            Next j
        Next i
    End Sub

    Private Sub SaveLabels(idxVar As Integer)
        '5. Save other labels
        Session("FinYear")(Session("COUNTE")) = Session("LastYear") - Session("FirstYear") + 1

        Session("VARN")(0, Session("COUNTE")) = V$(idxVar)
        Session("VARN")(1, Session("COUNTE")) = V$(idxVar)
        Session("VARN")(2, Session("COUNTE")) = VDef$(idxVar)
        Session("VARN")(3, Session("COUNTE")) = VHelpID(idxVar)
        Session("VARN")(6, Session("COUNTE")) = VHelpIDEqn(idxVar)

        Session("VARN")(5, Session("COUNTE")) = "REGION"

        If Session("SensitivityFlag") <> True Then
            Session("VARN")(0, Session("COUNTE")) = Session("VARN")(0, Session("COUNTE")) & "[Working]"
            Session("VARN")(1, Session("COUNTE")) = Session("VARN")(1, Session("COUNTE")) & "[0]"
        End If

        Session("GR3N")(Session("COUNTE")) = ""

        Session("GR4N")(Session("COUNTE")) = VUnits(idxVar) 'Other Grid Values determined earlier

        'Flexible Dimension Names for Graphs
        Session("FDNG3")(Session("COUNTE")) = Session("GR3N")(Session("COUNTE"))
        Session("FDNG4")(Session("COUNTE")) = Session("GR4N")(Session("COUNTE"))
    End Sub

    Protected Sub lstDim_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDim.SelectedIndexChanged
        'jrs 2012/12/05
        Session("txtDims") = txtDims.Value
    End Sub

    Protected Sub lstVars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstVars.SelectedIndexChanged
        ReDim Session("CyberVarsIdxs")(lstVars.Items.Count - 1)

        Dim pp As Integer
        Dim sListNames As String

        For pp = 0 To lstVars.Items.Count - 1
            Session("CyberVarsIdxs")(pp) = lstVars.Items(pp).Selected
            If lstVars.Items(pp).Selected Then
                If sListNames > "" Then
                    'jrs 2010/10/08 sListNames = sListNames & ",'" & Replace(lstVars.Items(pp).Value, "'", "''") & "'"
                    sListNames = sListNames & ",""" & lstVars.Items(pp).Value & """"
                Else
                    'jrs 2010/10/08 sListNames = "'" & Replace(lstVars.Items(pp).Value, "'", "''") & "'"
                    sListNames = """" & lstVars.Items(pp).Value & """"
                End If
            End If 'if selected
        Next pp

        'jrs 2005/10/13
        'jrs 2012/04/12 Session("TableTitle") = lstVars.SelectedValue
        If InStr(sListNames, """,""") > 0 Then
            'Multiple lists
            'jrs 2012/09/26 Session("TableTitle") = sListNames
            Session("TableTitle") = Replace(sListNames, """", "")
        Else
            'Single list
            Session("TableTitle") = lstVars.SelectedValue
        End If


    End Sub

    Protected Sub cmdLineAC_Click(sender As Object, e As EventArgs) Handles cmdLineAC.Click
        'jrs 2011/04/11
        ''jrs 2005/11/11
        'Session("oldLastYear") = Session("LastYear")
        'Session("oldFirstYear") = Session("FirstYear")

        LoadSet()

        If Session("COUNTE") = 0 Then
            Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstVars.SelectedValue

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GType") = "LINE"

        'jrs 2011/04/05
        Dim TargetUrl As String
        TargetUrl = "frm_GraphicalDisplayAC.aspx"
        'If txtVarNames.Value > "" Then
        '    TargetUrl = TargetUrl & "?VarNames=" & PrepareForURL(txtVarNames.Value)
        '    TargetUrl = TargetUrl & "&HistFor=" & txtHistFor.Value
        '    'jrs 2011/04/11
        '    TargetUrl = TargetUrl & "&GrpOp="
        '    Select Case True
        '        Case Session("bUseGroup") 'Groups 1
        '            TargetUrl = TargetUrl & "1"
        '        Case Session("bUseGroupDec") 'Dec Groups 2
        '            TargetUrl = TargetUrl & "2"
        '        Case Session("bUseGList") 'G-List 3
        '            TargetUrl = TargetUrl & "3"
        '        Case Else 'Regions 0
        '            TargetUrl = TargetUrl & "0"
        '    End Select
        '    'parameter for dimensions
        '    'jrs 2013/10/08
        '    Session("Dim1sLoaded") = DimLstToURL(lstDim, 1)
        '    TargetUrl = TargetUrl & Session("Dim1sLoaded")
        '    'jrs 2013/10/08
        '    Session("Dim1sLoaded") = Replace(Session("Dim1sLoaded"), "&Dim1=", "")
        '    TargetUrl = TargetUrl & FileLstToURL(lstFiles, txtFiles.Value)
        '    'jrs 2013/01/18
        '    TargetUrl = TargetUrl & "&DisplayLastYear=" & Session("DisplayLastYear")
        'End If

        Support.FormManager.Show(Me, TargetUrl)

    End Sub

    Protected Sub cmdLineFC_Click(sender As Object, e As EventArgs) Handles cmdLineFC.Click
        'jrs 2011/04/11
        ''jrs 2005/11/11
        'Session("oldLastYear") = Session("LastYear")
        'Session("oldFirstYear") = Session("FirstYear")

        LoadSet()

        If Session("COUNTE") = 0 Then
            Exit Sub
        End If
        'jrs 2005/10/13 Session("TableTitle") = lstVars.SelectedValue

        If Session("COUNTE") = 0 Then
            lblMsg.Text = "You must select one or more variables before you create a display"
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GType") = "LINE"

        'jrs 2011/04/05
        Dim TargetUrl As String
        TargetUrl = "frm_GraphicalDisplayFC.aspx"
        'If txtVarNames.Value > "" Then
        '    TargetUrl = TargetUrl & "?VarNames=" & PrepareForURL(txtVarNames.Value)
        '    TargetUrl = TargetUrl & "&HistFor=" & txtHistFor.Value
        '    'jrs 2011/04/11
        '    TargetUrl = TargetUrl & "&GrpOp="
        '    Select Case True
        '        Case Session("bUseGroup") 'Groups 1
        '            TargetUrl = TargetUrl & "1"
        '        Case Session("bUseGroupDec") 'Dec Groups 2
        '            TargetUrl = TargetUrl & "2"
        '        Case Session("bUseGList") 'G-List 3
        '            TargetUrl = TargetUrl & "3"
        '        Case Else 'Regions 0
        '            TargetUrl = TargetUrl & "0"
        '    End Select
        '    'parameter for dimensions
        '    'jrs 2013/10/08
        '    Session("Dim1sLoaded") = DimLstToURL(lstDim, 1)
        '    TargetUrl = TargetUrl & Session("Dim1sLoaded")
        '    'jrs 2013/10/08
        '    Session("Dim1sLoaded") = Replace(Session("Dim1sLoaded"), "&Dim1=", "")
        '    TargetUrl = TargetUrl & FileLstToURL(lstFiles, txtFiles.Value)
        '    'jrs 2013/01/18
        '    TargetUrl = TargetUrl & "&DisplayLastYear=" & Session("DisplayLastYear")
        'End If

        Support.FormManager.Show(Me, TargetUrl)

    End Sub
End Class
