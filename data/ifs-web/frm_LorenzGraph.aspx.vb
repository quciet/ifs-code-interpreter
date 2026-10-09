Imports IFs.Data

Partial Class frm_LorenzGraph
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            'jrs 2011/05/27

            If Session("LorenzX_Year") Is Nothing Then
                'Read querystring
                Dim sVarName As String, DefaultFile As Integer, idx As Integer
                sVarName = Request.QueryString("VarName")
                DefaultFile = CInt(Request.QueryString("File"))
                Session("DefaultFile") = DefaultFile

                Dim sDim2 As String = "", sDim3 As String = "", idxDim2 As Integer = -1, idxDim3 As Integer = -1
                If Not (Request.QueryString("Dim2") Is Nothing) Then
                    sDim2 = Request.QueryString("Dim2")
                End If
                If Not (Request.QueryString("Dim3") Is Nothing) Then
                    sDim3 = Request.QueryString("Dim3")
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
                    gLoadForDisplayNET(sVarName, idx, GRPS(1, ii), ii, sDim2, idxDim2, sDim3, idxDim3, "", DefaultFile, ii = 1, ii = RunCount, False, 1,
                        Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    'gLoadForDisplay(sVarName, idx, GRPS(1, ii), ii, sDim2, idxDim2, sDim3, idxDim3, "", DefaultFile, ii = 1, ii = RunCount, False, 1,
                    'Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                    'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    gLoadForDisplaySaveLbls(Session("COUNTE"), idx, GRPS(1, ii), sDim2, sDim3, False, DefaultFile, "",
                        Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                Next ii

                Dim IfsPars As New myParameters
                IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
                    Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
                    Session("PutFdrvRun"), Session("CurrencySym"))

                GiniSetup(IfsPars, Session("COUNTE"), Me, Session("VARN"), Session("LastYear"), Session("FirstYear"),
                    Session("PutFdrvRun"), Session("ActiveBase"), Session("RunFiles"), Session("bLoadForDisplaySuccess"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
                    Session("CurrencyVal"), Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis"),
                    Session("LastYearForecast"), Session("bComputation"), Session("bFillHisOnlyHoles"),
                    Session("LorenzX_Year"), Session("LorenzY_Year"), Session("PLOT"), Session("LorenzX"),
                    Session("LorenzY"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("YAxisLabel"),
                    Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))

                Session("LorenzParent") = "frm_WorldVariables.aspx"
            End If

            txtYear.Text = Session("FirstYear")
            WinFormsToWeb_FocusControl.Focus("txtYear")
            Session("Lorenzyear") = 1
            Session("LorenzAdd") = False
            Session("LorenzReset") = False
            Session("LorenzCurveCount") = 2

            If Session("DisplayLastYear") = 0 Then
                Session("DisplayLastYear") = Session("LastYear")
            End If
            Session("GType") = "LORENZ"
            Dim VN As String, D1 As String, D2 As String, D3 As String, Legend As String
            '       Legend composed from names & dimensions
            '       ---------------------------------------
            '           Get legend components
            '           ---------------------  
            VN = LTrim(RTrim(Session("VARN")(0, 1)))
            'jrs 10/27/04 Flexible Dimension Names for Graphs
            D1 = LTrim(RTrim(Session("FDNG1")(1)))
            D2 = LTrim(RTrim(Session("FDNG2")(1)))
            D3 = LTrim(RTrim(Session("FDNG3")(1)))

            '          Compose legend and assign legend
            '           --------------------------------

            Legend = "Lorenz"
            If Len(D1) > 0 Then Legend = Legend & "(" + D1
            If Len(D2) > 0 Then
                Legend = Legend & "," & D2
                'jrs 2011/06/03
                If Len(D3) > 0 Then
                    Legend = Legend & "," & D3 & ")"
                Else
                    Legend = Legend & ")"
                End If
            Else
                If Len(D1) > 0 Then
                    Legend = Legend + ")"
                End If
            End If
            Session("TableTitle") = Legend
        End If
        'jrs 09/17/04
        'jrs 2011/05/30 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

    Public newYear As Integer

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
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
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("LorenzParent")
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("LorenzParent") & "';"

        'jrs 2011/05/30
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Main Menu"
        cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Options"
        cswmGroup1.ID = "mnuOptions"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save"
        cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuRefresh"
        cswmItem.Caption = "Refresh"
        If Session("LorenzAdd") Then
            cswmItem.Enabled = False
            cmdRefresh.Visible = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuReset"
        cswmItem.Caption = "Reset"
        If Not Session("LorenzReset") Then
            cswmItem.Enabled = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Add Curve"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Variables"
        cswmGroup1.ID = "Group_b300"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_WorldVariables.aspx"
        cswmGroup1.OnMenuBarClick = "location.href='frm_WorldVariables.aspx';"
        'jrs 09/27/04
        lblMsg.Visible = False

        'jrs 2009/02/12
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,6510);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jrs 2011/10/20
        Dim s As String
        If Not (Request.QueryString("VarName") Is Nothing) Then
            s = "?VarName=" & Replace(Request.QueryString("VarName"), "'", "\'")
            s = s & "&File=" & Request.QueryString("File")
            s = s & "&GrpOp=" & Request.QueryString("GrpOp")
            If Not (Request.QueryString("Dim2") Is Nothing) Then
                s = s & "&Dim2=" & Request.QueryString("Dim2")
            End If
            If Not (Request.QueryString("Dim3") Is Nothing) Then
                s = s & "&Dim3=" & Request.QueryString("Dim3")
            End If
        End If

        PackJS()

        txtYear.Attributes.Add("onkeypress", "return clickButton(event, '" + cmdRefresh.ClientID + "')")
        'cswmWebMenu.Visible = False need to make invisible the menu to make the keypress event work
    End Sub

    Private Sub mnuRefresh_Click()
        If ((Val(txtYear.Text) > Session("FinYear")(1) + Session("FirstYear") - 1) Or (Val(txtYear.Text) < Session("FirstYear"))) Then
            'jrs 09/27/04
            lblMsg.Text = "Please enter a valid year between " + Str(Session("FirstYear")) + " and " + Str(Session("FinYear")(1) + Session("FirstYear") - 1) + "!"
            lblMsg.Visible = True
            txtYear.Text = Session("FirstYear")
        End If
        Session("Lorenzyear") = Val(txtYear.Text) - Session("FirstYear") + 1
        Session("Lorenzgraphidx") = 1
        If Session("LorenzCurveCount") > 2 Then
            Session("LorenzYearArr")(0) = Session("Lorenzyear")
        End If
        cswmWebMenu.Visible = True

        PackJS()
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Refresh"
                mnuRefresh_Click()
            Case "Add Curve"
                mnuAddCurve_Click()
            Case "Reset"
                mnuReset_Click()
        End Select
    End Sub

    'Private Sub txtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYear.TextChanged
    '    mnuRefresh_Click()
    'End Sub

    Private Sub mnuAddCurve_Click()

        'jrs 2005/10/26
        If Session("LorenzCurveCount") = 10 Then
            lblMsg.Text = "Maximum number of Graphs is 10."
            lblMsg.Visible = True
            txtAddYear.Text = Session("FirstYear")
            Exit Sub
        End If

        lblYear.Visible = False
        txtYear.Visible = False
        cmdRefresh.Visible = False
        lblAddYear.Visible = True
        txtAddYear.Visible = True
        cmdAdd.Visible = True
        txtAddYear.Text = Trim(Str(Int(txtYear.Text) + 10))
        WinFormsToWeb_FocusControl.Focus("txtAddYear")

        Session("LorenzAdd") = True
        Session("LorenzReset") = True
        cswmWebMenu.Groups("mnuOptions").Items("mnuRefresh").Enabled = False
        cswmWebMenu.Groups("mnuOptions").Items("mnuReset").Enabled = True
        'jrs 2015/09/21
        cswmWebMenu.Visible = False
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        'jrs 09/27/04
        If ((Val(txtAddYear.Text) > Session("FinYear")(1) + Session("FirstYear") - 1) Or (Val(txtAddYear.Text) < Session("FirstYear"))) Then
            lblMsg.Text = "Please enter a valid year between " + Str(Session("FirstYear")) + " and " + Str(Session("FinYear")(1) + Session("FirstYear") - 1) + "!"
            lblMsg.Visible = True
            txtAddYear.Text = Session("FirstYear")
            Exit Sub
        End If

        If Session("LorenzCurveCount") = 2 Then
            ReDim Session("LorenzYearArr")(0)
            Session("LorenzYearArr")(0) = Session("Lorenzyear")
        Else
            If Session("LorenzYearArr")(Session("LorenzCurveCount") - 2) = Int(txtAddYear.Text) - Session("FirstYear") + 1 Then
                'Already loaded
                Exit Sub
            End If
        End If

        Session("LorenzCurveCount") = Session("LorenzCurveCount") + 1
        Session("Lorenzyear") = Int(txtAddYear.Text) - Session("FirstYear") + 1
        Session("LorenzAdd") = False
        cswmWebMenu.Groups("mnuOptions").Items("mnuRefresh").Enabled = True

        lblYear.Visible = True
        txtYear.Visible = True
        lblAddYear.Visible = False
        txtAddYear.Visible = False
        cmdAdd.Visible = False
        cmdRefresh.Visible = True
        cswmWebMenu.Visible = True

        ReDim Preserve Session("LorenzYearArr")(Session("LorenzCurveCount") - 2)
        Session("LorenzYearArr")(Session("LorenzCurveCount") - 2) = Session("Lorenzyear")

        txtYear.Text = txtAddYear.Text
        PackJS()
    End Sub

    Private Sub mnuReset_Click()
        Session("LorenzCurveCount") = 2
        lblYear.Visible = True
        txtYear.Visible = True
        Session("LorenzAdd") = False
        cswmWebMenu.Groups("mnuOptions").Items("mnuRefresh").Enabled = True
        Session("LorenzReset") = False
        cswmWebMenu.Groups("mnuOptions").Items("mnuReset").Enabled = False
        cmdRefresh.Visible = True

        Session("Lorenzyear") = Val(txtYear.Text) - Session("FirstYear") + 1
        cswmWebMenu.Visible = True

        PackJS()
    End Sub

    Private Sub txtAddYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAddYear.TextChanged
        cmdAdd_Click(sender, e)
    End Sub

    'jrs 2015/09/21
    Private Sub PackJS()
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/10/07 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''






        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/16 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        If Session("TableTitle") > "" And Session("TableTitle") <> "Title" Then
            sJscript2 = sJscript2 & "text:       '" & Session("TableTitle") & "'," & vbCr
        Else
            sJscript2 = sJscript2 & "text:       ''," & vbCr
        End If

        'jam 2025/10/07  Increasing font size for legends
        sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '30px'}," & vbCr
        ''''''''''''''''''''''''''''



        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxisXLabel As String
        sAxisXLabel = getXAxisLabel()
        Dim sAxisYLabel As String
        sAxisYLabel = getYAxisLabel()

        sJscript2 = sJscript2 & "xAxis: {" & vbCr

        'jam 2025/10/13 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr


        sJscript2 = sJscript2 & "title: {" & vbCr

        'jam 2025/10/13 Increasing font for x axis
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'Load Years
        '----------
        'sJscript2 = sJscript2 & ", categories: ["
        'Dim i As Integer
        'For i = 1 To Session("DisYear") Step Session("Interval")
        '    If i > 1 Then
        '        sJscript2 = sJscript2 & ","
        '    End If
        '    sJscript2 = sJscript2 & "'" & CStr(Session("FirstYear") + i - 1) & "'"
        'Next
        'sJscript2 = sJscript2 & "]" & vbCr
        'If Session("DisYear") > 21 Or Session("Interval") > 1 Then
        '    If Session("Interval") > 1 Then
        '        sJscript2 = sJscript2 & ", tickInterval: " & Session("Interval") & vbCr
        '    Else
        '        sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr
        '    End If
        'End If

        'sJscript2 = sJscript2 & ", min: " & Session("FirstYear") & vbCr
        'sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("DisYear") - 1) & vbCr

        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr

        'jam 2025/10/13 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr

        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr


        'jam 2025/10/13 Increasing font for y axis
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr



        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr
        'sJscript2 = sJscript2 & "text:       '" & getYAxisLabel() & "'," & vbCr
        'sJscript2 = sJscript2 & "style: {" & vbCr
        'sJscript2 = sJscript2 & "fontSize:   '20px'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
        sJscript2 = sJscript2 & "align:      'center'," & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
        sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim sZeros As String = ""
        sJscript2 = sJscript2 & PlotLorenzToJSSeries(Session("Lorenzyear"), 0)
        If Session("LorenzCurveCount") = 2 Then
            sJscript2 = sJscript2 & PlotLorenzToJSSeries(Session("Lorenzyear"), 1)
        Else
            For i = 0 To Session("LorenzCurveCount") - 2
                sJscript2 = sJscript2 & PlotLorenzToJSSeries(Session("LorenzYearArr")(i), i + 1)
            Next i
        End If
        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr
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

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 50;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub

    Private Function getXAxisLabel() As String
        getXAxisLabel = ""
        If Session("XAxisLabel") > "" Then
            getXAxisLabel = Session("XAxisLabel")
        ElseIf Session("GType") = "LORENZ" Then
            getXAxisLabel = "Share of Global Population"
        End If
    End Function

    Private Function getYAxisLabel() As String
        getYAxisLabel = ""
        If Session("YAxisLabel") <> "" Then
            getYAxisLabel = Session("YAxisLabel")
            Exit Function
        ElseIf Session("GType") = "LORENZ" Then
            getYAxisLabel = "Share of Resource"
            Exit Function
        End If

    End Function

    Private Function PlotLorenzToJSSeries(ByVal yr As Integer, ByVal graphidx As Integer) As String
        'jrs 09/06/04 check for last value of x
        If Session("LorenzX_Year")(NR, yr) < 1 Then
            Session("LorenzX_Year")(NR, yr) = 1.0
        End If

        Dim sJscript As String = ""
        If graphidx > 0 Then
            Dim sColor As String = ""
            'jrs 2024/01/23
            'If graphidx = 1 Then
            '    sColor = " color: ""#0000FF"""
            'ElseIf graphidx = 2 Then
            '    sColor = " color: ""#FF0000"""
            'End If
            If graphidx < 9 Then
                Select Case graphidx + 1
                    Case 2
                        sColor = "Blue"
                    Case 3
                        sColor = "Red"
                    Case 4 'jrs 2023/11/20 Sandy Brown
                        sColor = "RGB(244,114,0)"
                    Case 5 'blue violet
                        sColor = "RGB(138,43,226)"
                    Case 6 'aqua 
                        sColor = "RGB(0,255,255)"
                    Case 7 'deep pink
                        sColor = "RGB(255,20,147)"
                    Case 8 'medium spring green
                        sColor = "RGB(0,250,154)"
                End Select
            End If
            'jrs 2024/01/23 added color: ''
            sJscript = sJscript & ",{name: """ & Str(yr - 1 + Session("FirstYear")) & ", GINI: " &
                IIf(IsDBNull(Session("PLOT")(yr, 1)), "", Str(System.Math.Round(Session("PLOT")(yr, 1), 3))) & """, color: '" & sColor & "', data: [" & vbCr
        Else
            sJscript = sJscript & "{name: ""Equality"", color: ""#32cd32"", data: [" & vbCr
        End If

        Dim rr As Short
        Dim idx As Short
        idx = 1
        For rr = 1 To NR
            If rr > 1 Then
                sJscript = sJscript & "," & vbCr
            End If
            If graphidx = 0 Then
                'Line y = x
                'JRS 08/13/02 end points in equality line
                'jrs 2026/01/09 added rounding to 4 decimal places
                sJscript = sJscript & "{y: " & CStr(System.Math.Round(rr / NR, 4)) &
                        ", x: " & CStr(System.Math.Round(rr / NR, 4)) & "}"
            Else
                Do While ((rr / NR) > Session("LorenzX_Year")(idx, yr))
                    idx = idx + 1
                Loop
                If Not (IsDBNull(Session("LorenzY_Year")(idx, yr)) Or IsDBNull(Session("LorenzY_Year")(idx - 1, yr))) Then
                    'y = (y2-y1)*(x-x1)/(x2-x1)+y1
                    'jrs 2026/01/09 added rounding to 4 decimal places
                    sJscript = sJscript & "{y: " & CStr(System.Math.Round((Session("LorenzY_Year")(idx, yr) - Session("LorenzY_Year")(idx - 1, yr)) * ((rr / NR) - Session("LorenzX_Year")(idx - 1, yr)) / (Session("LorenzX_Year")(idx, yr) - Session("LorenzX_Year")(idx - 1, yr)) + Session("LorenzY_Year")(idx - 1, yr), 4)) &
                        ", x: " & CStr(System.Math.Round((rr / NR), 4)) & "}" & vbCr
                End If
            End If
        Next rr

        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "}" & vbCr

        PlotLorenzToJSSeries = sJscript
    End Function

    Protected Sub cmdRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        mnuRefresh_Click()
    End Sub
End Class
