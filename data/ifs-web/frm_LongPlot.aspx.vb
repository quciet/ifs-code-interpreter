'
'
'
'

Option Strict Off
Option Explicit On

Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_LongPlot2
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer.
        'Do not modify it using the code editor.
        'CONVERSION_NOTE:  'Code' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
        'If m_vb6FormDefInstance Is Nothing Then 
        'If m_InitializingDefInstance Then 
        'm_vb6FormDefInstance = Me
        'Else 
        'Try 
        ''For the start-up form, the first instance created is the default instance.
        'If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then 
        'm_vb6FormDefInstance = Me
        'End If 
        'Catch 
        'End Try 
        'End If 
        'End If  
        InitializeComponent()

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            '------------------------------------------------------------

            Dim i As Short

            'Dim cw As Integer
            'Dim ch As Integer


            'Mohammod .Net Win Forms Feb 2004
            ''timeseries extrapolation
            'txtYearsAhead.Text = CStr(25) 'default
            'txtP1.Text = ""
            'txtP2.Text = ""
            'txtP3.Text = ""
            ''timeseries extrapolation Jan 16, 2004

            'end timeseries extrapolation
            'mti Augut 2004 3
            If Session("oldLastYear") <> Nothing Then
                Session("LastYear") = Session("oldLastYear")
                Session("FirstYear") = Session("oldFirstYear")
                'jrs 09/28/04
                Session("oldLastYear") = Nothing
            End If

            'From ChartHist





            '   Choose Menu Options
            '   -------------------



            'mnuAdvance.Visible = False
            'mnuRegress.Visible = False
            'mnuCommon.Visible = True
            'mnuStatistics.Visible = False

            'JRS 04-17-01 BEGIN
            Session("LV_CommonPlot") = "False"
            'jrs 2015/09/24 moved up, to be ready for LongPlot_Activated
            'jrs 2011/09/09
            Session("Interval") = 1
            'JRS 04-17-01 END
            'mtiwebform June 2004 Longitudinal - Uncomment later
            'mnuTable.Visible = (frm_LongVarS.DefInstance.Chk_Indep.Checked = 1)
            'mti August 2004 4
            frm_LongPlot_Activated(Me, New System.EventArgs)
            Session("TableDisplayParent") = "frm_LongPlot.aspx"
        End If
    End Sub
    'Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    'If Disposing Then 
    'If Not components Is Nothing Then 
    'components.Dispose()
    'End If 
    'End If 
    'MyBase.Dispose(Disposing)
    'End Sub  
    'CONVERSION_NOTE: Field 'components' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Private components As System.ComponentModel.IContainer 
    'CONVERSION_NOTE: Class 'System.Windows.Forms.ToolTip' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public ToolTip1 As System.Windows.Forms.ToolTip 
    'CONVERSION_NOTE: Class 'AxMSComDlg.AxCommonDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents cmd_ScriptFile As AxMSComDlg.AxCommonDialog 
    'CONVERSION_NOTE: Class 'Gigasoft.ProEssentials.Pego' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents PegoNet1 As Gigasoft.ProEssentials.Pego 
    Protected WithEvents ToolTip1 As System.Web.UI.WebControls.Label
    Protected WithEvents cmd_ScriptFile As System.Web.UI.WebControls.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_LongPlotProEssentialPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_LongPlotProEssential
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_LongPlotProEssential()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    '
    '=============================frm_LongPlotProEssential ======================
    '------------------------------------------------------------------------------------------
    '
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2000
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last revision 6/28/99

    'Dim CommonPlot As String
    'JRS 11/17/02
    'JRS 01/29/04
    'Mohammod .Net Win Forms Feb 2004

    '   'mtiwebform June 2004 Longitudinal - take declarations to global
    '   'needed to change name to avoid conflict with LongVars declaration taken to global
    'Dim Session("PlotLong")(0, 0) As Object

    'Dim Session("PlotLabels")(0, 0) As String

    'Dim Session("PlotSize") As Short

    'timeseries extrapolation: New Declarations
    Dim reg As New RegressionObject
    Dim bExtrapolating As Boolean
    'Dim ExtpType As String
    'JRS 01/29/04
    'Mohammod .Net Win Forms Feb 2004
    'mtiwebform June 2004 Longitudinal - take declarations to global
    'Dim ForeLong(0, 0) As Object
    'Dim ForeLabels(0, 0) As String

    '   Dim ForeSize As Short

    'Dim RSQR() As Double
    'Dim YearsAhead As Short
    'Dim sUnits As String

    'nl regression
    Dim El As Short
    Dim NPts As Short
    Dim NVar As Short
    Dim Calc As Short

    Dim YData() As Double
    Dim XData() As Double

    Dim PI() As Double
    Dim PU() As Double
    Dim PL() As Double
    Dim POPT() As Double
    Dim SDev() As Double

    Dim CRLF As String
    Dim OKDbl As String

    'Mohammod .Net Win Forms Feb 2004
    'default parameters
    'Dim DefYearsAhead As Short
    'Dim DefPOLY1 As Double
    'Dim DefEXPO1 As Double
    'Dim DefEXPO2 As Double
    'Dim DefSLIN1 As Double
    'Dim DefSCUR1 As Double
    'Dim DefSCUR2 As Double
    'Dim DefSCUR3 As Double
    'Dim PlusorMinus As Short '1 for s-curve (nonlinear) growth, -1 for decay
    'Dim AvgGrowthRate As Double

    ''parameters input by users
    'Dim InpYearsAhead As Short
    'Dim InpPOLY1 As Short
    'Dim InpEXPO1 As Double
    'Dim InpEXPO2 As Double
    'Dim InpSLIN1 As Double
    'Dim InpSCUR1 As Double
    'Dim InpSCUR2 As Double
    '   Dim InpSCUR3 As Double
    'Mohammod .Net Win Forms Feb 2004
    'End timeseries extrapolation: New Declarations





    Private Sub frm_LongPlot_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) 'Handles frm_LongPlotProEssential.Activated
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation - take all


        If Session("LV_Chk_Indep") = 1 Then
            'mti August 2004 4
            'mnuTrend.Visible = True
            'defaults
            DefYearsAhead = 25
            If IsNothing(Session("InpYearsAhead")) Then
                Session("InpYearsAhead") = DefYearsAhead

            End If
            'polynomial
            DefPOLY1 = 2 '2nd degree polynomial
            If IsNothing(Session("InpPOLY1")) Then Session("InpPOLY1") = DefPOLY1
            'jrs 2012/02/23
            If Session("DisYear") > UBound(Session("PlotLongPE"), 1) Then
                Session("DisYear") = UBound(Session("PlotLongPE"), 1)
            End If
            'exponential
            'jrs 2008/12/08 added ToString
            'jrs 2005/05/19
            If CStr(Session("PlotLongPE")(1, 1).ToString) = "" Or CStr(Session("PlotLongPE")(Session("DisYear"), 1).ToString) = "" Then
                Exit Sub
            End If
            'Mohammod .Net Win Forms Feb 2004
            If IsNothing(Session("DefEXPO1")) Then Session("DefEXPO1") = Session("PlotLongPE")(1, 1)
            If IsNothing(Session("InpEXPO1")) Then Session("InpEXPO1") = Session("DefEXPO1")
            If IsNothing(Session("DefEXPO2")) Then Session("DefEXPO2") = 0.3
            If IsNothing(Session("InpEXPO2")) Then Session("InpEXPO2") = Session("DefEXPO2")
            's-curve
            'SaturatePoint = YData(Session("DisYear")%) + ((YData(Session("DisYear")%) - YData(1)) / Session("DisYear")%) * YearsAhead
            If Session("PlotLongPE")(1, 1) <= Session("PlotLongPE")(Session("DisYear"), 1) Then
                'growth
                Session("AvgGrowthRate") = (Session("PlotLongPE")(Session("DisYear"), 1) - Session("PlotLongPE")(1, 1)) / Session("DisYear")
                Session("DefSCUR1") = CDbl(Session("PlotLongPE")(1, 1) + Session("AvgGrowthRate") * DefYearsAhead) ' will be setup depending on the series
                If Session("DefSCUR1") < Session("PlotLongPE")(Session("DisYear"), 1) Then
                    Session("DefSCUR1") = Session("DefSCUR1") + Session("PlotLongPE")(Session("DisYear"), 1)
                End If
                Session("PlusorMinus") = 1
            Else
                'decay
                Session("AvgGrowthRate") = (Session("PlotLongPE")(1, 1) - Session("PlotLongPE")(Session("DisYear"), 1)) / Session("DisYear")
                Session("DefSCUR1") = CDbl(Session("PlotLongPE")(1, 1)) + Session("AvgGrowthRate")
                Session("PlusorMinus") = -1
            End If

            Session("DefSCUR2") = 60
            Session("DefSCUR3") = 20
            If IsNothing(Session("InpSCUR1")) Then Session("InpSCUR1") = Session("DefSCUR1")
            If IsNothing(Session("InpSCUR2")) Then Session("InpSCUR2") = Session("DefSCUR2")
            If IsNothing(Session("InpSCUR3")) Then Session("InpSCUR3") = Session("DefSCUR3")

            Session("DefSLIN1") = Session("DefSCUR1")
            If IsNothing(Session("InpSLIN1")) Then Session("InpSLIN1") = Session("DefSLIN1")

        Else
            'mti August 2004 4
            'mnuTrend.Visible = False
        End If

        PackJS()
    End Sub


    '=============================frm_LongPlotProEssential ======================
    'CONVERSION_NOTE: The code of Event 'frm_LongPlotProEssential.frm_LongPlotProEssential_Load' was moved to Method 'frm_LongPlotProEssential.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_LongPlot_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'mti IfsWeb Menu license

        cswmWebMenu.UserData = "jsolorza::976739975"

        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_a624"
        'jrs 2006/11/22 cswmGroup1.DownLevelURL = "LongVarS.aspx"
        cswmGroup1.OnMenuBarClick = "location.href='LongVarS.aspx';"

        'jrs 2011/12/22
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Main Menu"
        cswmGroup1.ID = "mnuMainMenu"
        cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        'mti commenting save and print 
        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Save"
        'cswmGroup1.ID = "Group_8830"

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Print"
        'cswmGroup1.ID = "Group_6a3c"
        'mti commenting save and print  - end

        'mti August 2004 4
        If Session("LV_Chk_Indep") = 1 Or Session("LV_Chk_Time") = 1 Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Scaling"
            cswmGroup1.ID = "Group_4c48"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Common"
            cswmItem.IconHeight = 16
            'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
            cswmItem.IconImage = "blank.gif"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Scale"
            cswmItem.IconHeight = 16
            'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
            cswmItem.IconImage = "checkbox.gif"




        End If
        If Session("LV_Chk_Indep") = 1 Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "View Table"
            cswmGroup1.ID = "Group_2e54"
            'mti Augut 2004 3
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Table"

            'cswmGroup1.DownLevelURL = "frm_TableDisplay.aspx"
            Session("TableDisplayParent") = "frm_LongPlot.aspx"

            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Trend"
            cswmGroup1.ID = "Group_1060"


            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Linear"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Polynomial"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Logarithmic"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Exponential"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "S-Curve"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "."
            cswmItem.DividerColor = "#808080"
            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "."
            cswmItem.DividerColor = "#ffffff"
            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Extrapolation Setup"
        End If

        'jrs 2006/05/08
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Format"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels"

        'jrs 2006/12/22
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16031);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

        If Not Page.IsPostBack Then
            ReDim Session("LV_MaxValC")(MaxVariables), Session("LV_MinValC")(MaxVariables), Session("LV_diffvalc")(MaxVariables) 'Used in Common Display
            Session("LV_CommonPlot") = "False"
        End If

        If Session("LV_Chk_Indep") = 1 Then
            If Session("LV_CommonPlot") Then
                cswmWebMenu.Groups("Scaling").Items("Common").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("Scaling").Items("Scale").IconImage = "blank.gif"
                'Session("LV_Subtitle") = Session("LV_sUnits")
                'Call CommonGraph()
            Else  'Scale
                cswmWebMenu.Groups("Scaling").Items("Scale").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("Scaling").Items("Common").IconImage = "blank.gif"
                'Session("LV_Subtitle") = ""
                'Call ScaledGraph01()



            End If
        End If
        'mti WebForm Changes From June 26, 2004
        Session("LV_CommonMenu") = "Display"
        'mti WebForm Changes From June 26, 2004

        PackJS()
    End Sub

    '=============================frm_LongPlotProEssential ======================
    Private Function GraphScaleLabel(ByRef V As String, ByRef Rescaler As Object) As String
        Dim GSLabel As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Couldn't resolve default property of object Rescaler. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Rescaler = 0 Then
            GSLabel = ""
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Rescaler. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            GSLabel = "10^" & LTrim(Str(Rescaler))
        End If

        GraphScaleLabel = GSLabel

    End Function


    '=============================frm_LongPlotProEssential ======================
    Public Sub mnuCommon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation
        Session("LongPlotFore") = False

        'DisplayGraph()

        ''Pego1.set_MultiSubTitles(0, "") ' = sUnits
        'PegoNet1.PeString.MultiSubTitles(0) = ""

        ''end timeseries extrapolation


        'If Session("LV_CommonPlot") = "True" Then
        '    Call CommonGraph()
        '    'timeseries extrapolation
        '    'Pego1.SubTitle = sUnits
        '    PegoNet1.PeString.SubTitle = sUnits
        '    'end timeseries extrapolation

        'Else
        '    'Call ScaledGraph
        '    'JRS 04-17-01 BEGIN
        '    Call ScaledGraph01()
        '    'JRS 04-17-01 END
        '    'timeseries extrapolation
        '    'Pego1.SubTitle = ""
        '    PegoNet1.PeString.SubTitle = ""
        '    'end timeseries extrapolation

        'End If

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objEA.Item.Caption = "Common" Then
            objSender.Groups("Scaling").Items("Common").IconImage = "checkbox.gif"
            objSender.Groups("Scaling").Items("Scale").IconImage = "blank.gif"
            Session("LV_Subtitle") = Session("LV_sUnits")
            Session("LV_CommonMenu") = "Common"
            'Call CommonGraph()
        Else  'Scale
            objSender.Groups("Scaling").Items("Scale").IconImage = "checkbox.gif"
            objSender.Groups("Scaling").Items("Common").IconImage = "blank.gif"
            Session("LV_Subtitle") = ""
            'Call ScaledGraph01()
            Session("LV_CommonMenu") = "Display"

        End If

    End Sub


    Public Sub mnuContinue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Support.FormManager.Close(Me)

    End Sub

    Public Sub mnuExponential_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation- new proc
        Dim TP1 As Double
        Dim TP2 As Double

        Session("ExtpType") = "EXPONENTIAL"
        Session("YearsAhead") = Session("InpYearsAhead")

        TP1 = Session("InpEXPO1")
        TP2 = Session("InpEXPO2")

        LoadForeVec(Session("PlotLongPE"), Session("PlotSizePE"), Session("PlotLabelsPE"), 1, "EXP", TP1, TP2) '0.3
    End Sub
    '=============================frm_LongPlotProEssential ======================

    Public Sub mnuLinear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation new proc
        Session("ExtpType") = "LINEAR"

        Session("YearsAhead") = Session("InpYearsAhead")


        Call LoadForeVec(Session("PlotLongPE"), Session("PlotSizePE"), Session("PlotLabelsPE"), 1, "NONE")

    End Sub
    '=============================frm_LongPlotProEssential ======================

    Public Sub mnulogarithmic_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation- new proc
        Session("ExtpType") = "LOGARITHMIC"
        Session("YearsAhead") = Session("InpYearsAhead")

        LoadForeVec(Session("PlotLongPE"), Session("PlotSizePE"), Session("PlotLabelsPE"), 1, "LOG")
    End Sub
    '=============================frm_LongPlotProEssential ======================
    Public Sub mnuPolynom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------

        Session("ExtpType") = "POLYNOMIAL (" & Trim(Str(Session("InpPOLY1"))) & ")"
        Session("YearsAhead") = Session("InpYearsAhead")

        LoadForeVec(Session("PlotLongPE"), Session("PlotSizePE"), Session("PlotLabelsPE"), CShort(Session("InpPOLY1")), "NONE")


    End Sub


    '=============================frm_LongPlotProEssential ======================
    Public Sub mnuS_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        'Linear Transformation of S-Curve
        'The Logistic Curve.  The logistic curve represented by the function
        '                        Y = K / [1+ a*exp(-bt)]
        'By defining
        '                        Y = Ln((K / Y) - 1)
        '                        A = ln(a)  so that a = exp(A)
        '                        B = -b And b = -B
        'the non-linear logistic function is transformed
        '   to the linear function, Y = A + B t
        'Given a data set of y's and t's,
        'the y-values are transformed to Y-values,
        'and linear regression is used to estimate A and B for a given K,
        'from which a and b are calculated

        Dim TP1 As Double

        Session("ExtpType") = "S-CURVE"
        Session("YearsAhead") = Session("InpYearsAhead")

        TP1 = Session("InpSLIN1")
        'linear transform
        LoadForeVec(Session("PlotLongPE"), Session("PlotSizePE"), Session("PlotLabelsPE"), 1, "SCU", TP1) ', 3, 0.25
        'Exit Sub
    End Sub

    '=============================frm_LongPlotProEssential ======================
    Public Sub mnuSNL_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim rr As Short
        Dim i2 As Object
        Dim cc As Short
        '------------------------------------------------------------------------------------------
        'Non Linear Fit S-Curve
        On Error GoTo Forecasterr
        Dim SaturatePoint As Double
        Session("ExtpType") = "S-CURVE (NL)"
        Session("YearsAhead") = Session("InpYearsAhead")

        Dim NITER As Short

        NITER = 0 'iterations for non-linear solution

        'UPGRADE_ISSUE: As Variant was removed from ReDim ForeLong(UBound(Session("PlotLong")) + YearsAhead, UBound(Session("PlotLong"), 2)) statement. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1056"'
        ReDim Session("ForeLong")(UBound(Session("PlotLongPE")) + Session("YearsAhead"), UBound(Session("PlotLongPE"), 2))
        ReDim Session("ForeLabels")(UBound(Session("PlotLabelsPE")), UBound(Session("PlotLabelsPE"), 2) + Session("YearsAhead"))
        ReDim Session("RSQR")(UBound(Session("PlotLongPE"), 2))
        '    ForeLong = V
        '    ForeLabels = v2
        Session("ForeSize") = Session("PlotSizePE")

        '---- number of fitting parameters

        NVar = 3

        '---- number of X and Y data points

        NPts = Session("DisYear")

        '---- Dimension solution vector and data arrays

        ReDim YData(NPts)
        ReDim PI(NVar)
        ReDim XData(NPts)
        ReDim PL(NVar)
        ReDim PU(NVar)
        ReDim POPT(NVar)
        ReDim SDev(NVar)


        '---- Initial guesses for fitting parameters

        PI(2) = Session("PlusorMinus") * Session("InpSCUR2")
        'If PI(2) = 0 Then PI(2) = 30#

        PI(3) = Session("InpSCUR3")
        'If PI(3) = 0 Then PI(3) = -0.5


        '  PI(2) = 30# 'growth rate * mid point
        '  PI(3) = -1.5  'growrh rate
        '30, .5 gives s

        '---- Upper and lower bounds for each fit parameter

        PU(1) = 10000.0#
        PL(1) = 0.1
        PU(2) = 100.0#
        PL(2) = 1.0#
        PU(3) = 100.0#
        PL(3) = 1.0#

        '---- Define array of X and Y data points

        For cc = 1 To Session("ForeSize")

            For i2 = 1 To Session("DisYear") 'Session("LastYear")% - Session("FirstYear")% + 1
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                XData(i2) = CDbl(i2)
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PlotLong")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                YData(i2) = CDbl(Session("PlotLongPE")(i2, cc)) '1
            Next i2

            PI(1) = Session("InpSCUR1") 'CDbl(Val(txtP1.Text))
            If YData(Session("DisYear")) >= YData(1) Then
                Session("AvgGrowthRate") = (YData(Session("DisYear")) - YData(1)) / Session("DisYear")
                SaturatePoint = YData(Session("DisYear")) + Session("AvgGrowthRate") * Session("YearsAhead")
                If SaturatePoint < YData(Session("DisYear")) Then
                    SaturatePoint = SaturatePoint + YData(Session("DisYear"))
                End If
            Else
                Session("AvgGrowthRate") = (YData(1) - YData(Session("DisYear"))) / Session("DisYear")
                SaturatePoint = YData(1) + Session("AvgGrowthRate")
            End If

            'keep parameter 1 textbox blank for different saturatepoint for each series
            'Mohammod .Net Win Forms Feb 2004
            'If txtP1.Text = "" Then PI(1) = SaturatePoint 'YData(Session("DisYear")%)

            'PI(1) = YData(Session("DisYear")%) '282#

            'UPGRADE_WARNING: Couldn't resolve default property of object NLFIT.NLFIT. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NLFIT.NLFIT(NVar, NPts, XData, YData, PI, PU, PL, POPT, SDev, NITER)
            For rr = 1 To Session("DisYear") + Session("YearsAhead") 'Step Session("Interval")%

                'UPGRADE_WARNING: Couldn't resolve default property of object ForeLong(rr, cc). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("ForeLong")(rr, cc) = POPT(1) / (1.0# + System.Math.Exp(-(System.Math.Log(81) / POPT(2)) * (rr - POPT(3))))
                'POPT (1) / (1# + exp(POPT(2) + POPT(3) * rr%))
                'Pego1.PointLabels(rr%) = Session("PlotLabels")(cc%, Session("DisYear")% - 1) + rr% - Session("DisYear")%
            Next rr


        Next cc

        '  CalcRSQR
        'DisplayGraphFore()


        '  El = 1           'Starting array element



        Exit Sub
Forecasterr:

        LoadMessage("Error occurred while calculating " & Session("ExtpType") & " trend.", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")



    End Sub

    '=============================frm_LongPlotProEssential ======================
    Public Sub mnuSetExtrapolation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        'Mohammod .Net Win Forms Feb 2004
        'Dim frm As New frm_setextrapolate
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "setextrapolate.aspx")
        'frm.Show() 


    End Sub

    '=============================frm_LongPlotProEssential ======================
    Public Sub mnuStatistics_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        Session("Statistics_Type") = 0
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_statistics.aspx")
    End Sub

    '=============================frm_LongPlotProEssential ======================
    'JRS 2/6/03
    Public Sub mnuTable_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        'JRS 11/19/02
        'mtiwebform June 2004 Longitudinal 
        'ClearPlot()
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        Session("COUNTE") = Session("PlotSizePE")
        'UPGRADE_NOTE: year was upgraded to year_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim year_Renamed As Short
        Dim country As Short
        Dim i As Short

        'jrs 2005/06/29
        Dim MinYr As Integer, MaxYr As Integer
        MinYr = FirstYearHistory
        MaxYr = LastYearHistory
        'jrs 2007/05/01 use first element, since we're not storing Earliest anymore
        'we were using second element
        If IsNumeric(Session("PlotLabelsPE")(1, 1)) Then
            If CInt(Session("PlotLabelsPE")(1, 1)) < MinYr Then
                MinYr = CInt(Session("PlotLabelsPE")(1, 1))
            End If
        End If

        'jrs 2007/02/20 If IsNumeric(PlotLabels(1, UBound(PlotLabels, 2) - 1)) Then
        'removed most recent at the end
        If IsNumeric(Session("PlotLabelsPE")(1, UBound(Session("PlotLabelsPE"), 2))) Then
            If CInt(Session("PlotLabelsPE")(1, UBound(Session("PlotLabelsPE"), 2))) > MaxYr Then
                MaxYr = CInt(Session("PlotLabelsPE")(1, UBound(Session("PlotLabelsPE"), 2)))
            End If
        End If

        'jrs 2012/02/21
        'GDPANGUSMADISON causes blow up, starts in year 1
        If MaxYr - MinYr + 1 > UBound(Session("PLOT"), 1) Then
            'find first year that fits in the PLOT vector (300 years allowed)
            For i = 1 To UBound(Session("PlotLabelsPE"), 2)
                If MaxYr - CInt(Session("PlotLabelsPE")(1, i)) + 1 <= UBound(Session("PLOT"), 1) Then
                    MinYr = CInt(Session("PlotLabelsPE")(1, i))
                    Exit For
                End If
            Next i

            'need to remove a few years
            Dim c As Integer, k As Integer
            c = i - 1
            For i = 1 To UBound(Session("PlotLabelsPE"), 2) - c
                Session("PlotLabelsPE")(1, i) = Session("PlotLabelsPE")(1, c + i)
                For k = 1 To Session("COUNTE")
                    Session("PlotLongPE")(i, k) = Session("PlotLongPE")(c + i, k)
                Next k
            Next i
            ReDim Preserve Session("PlotLabelsPE")(UBound(Session("PlotLabelsPE"), 1), i - 1)
        End If

        'jrs 2007/02/20 For i = 2 To UBound(PlotLabels, 2) - 1
        'jrs 2007/05/01 change I = 2 to I = 1, since we're not storing the Earliest field anymore, only numeric ones
        For i = 1 To UBound(Session("PlotLabelsPE"), 2)
            'JRS 12/08/03
            If CShort(Session("PlotLabelsPE")(1, 1)) < FirstYearHistory Then
                year_Renamed = CShort(Session("PlotLabelsPE")(1, i)) - CShort(Session("PlotLabelsPE")(1, 1)) + 1
            Else
                year_Renamed = CShort(Session("PlotLabelsPE")(1, i)) - FirstYearHistory + 1
            End If
            'Session("TableTitle")
            For country = 1 To Session("COUNTE")  'COUNTE

                'mti Augut 2004 3

                'Session("TableTitle")
                'VARN(1, country) = Mid(Pego1.MainTitle, 1, InStr(Pego1.MainTitle, "versus") - 2)
                'Session("VARN")(1, country) = Mid(PegoNet1.PeString.MainTitle, 1, InStr(PegoNet1.PeString.MainTitle, "versus") - 2)
                Session("VARN")(1, country) = Mid(Session("TableTitle"), 1, InStr(Session("TableTitle"), "versus") - 2)
                Session("GR1N")(country) = Session("CountryList")(country)

                'jrs 10/27/04 Flexible Dimension Names for Graphs
                Session("FDNG1")(country) = Session("GR1N")(country)

                'jrs 2012/02/22 Session("FinYear")(country) = LastYearHistory
                If CInt(Session("PlotLabelsPE")(1, 1)) < FirstYearHistory Then
                    Session("FinYear")(country) = LastYearHistory - CInt(Session("PlotLabelsPE")(1, 1)) + 1
                Else
                    Session("FinYear")(country) = LastYearHistory - FirstYearHistory + 1
                End If

                Session("PLOT")(year_Renamed, country) = Session("PlotLongPE")(i, country)
            Next country
        Next i
        'Update variables
        'mti August 2004 3
        'Dim oldLastYear As Short
        'Dim oldFirstYear As Short
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")
        Session("LastYear") = MaxYr 'jrs 2005/06/30 LastYearHistory
        Session("FirstYear") = MinYr 'jrs 2005/06/30 FirstYearHistory
        Session("PercentOn") = "False"
        'jrs 2005/10/25
        Session("DisplayLastYear") = Session("LastYear")
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.

        'mtiwebform June 2004 Longitudinal - commented for now
        'frm_TableDisplay.DefInstance.mnuGraph.Visible = False
        'frm_TableDisplay.DefInstance.Text = Pego1.SubTitle
        'JRS 09/01/04
        If Not (WinFormsToWeb_PageBar Is Nothing) Then
            WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = Session("LV_Subtitle") 'PegoNet1.PeString.SubTitle
        End If
        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Hide().html'.
        'frm_TableDisplay.DefInstance.Hide() 
        'JRS 12/08/03
        'mti August 2004 3
        'If CShort(Session("PlotLabelsPE")(1, 2)) < FirstYearHistory Then
        'jrs 2004/12/02
        If IsNumeric(Session("PlotLabelsPE")(1, 2)) Then
            'JRS 09/01/04
            If CShort(Session("PlotLabelsPE")(1, 2)) <= FirstYearHistory And Not (WinFormsToWeb_PageBar Is Nothing) Then
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'mtiwebform June 2004 Longitudinal - commented for now
                'frm_TableDisplay.DefInstance.bLoadLongitudinalBFYH = True
                ''CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'frm_TableDisplay.DefInstance.iFirstYearBFYH = CShort(Session("PlotLabelsPE")(1, 2))
                ''CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'frm_TableDisplay.DefInstance.RefreshTable()

            End If
            'JRS 2/19/03
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'mtiwebform June 2004 Longitudinal - commented for now
            'frm_TableDisplay.DefInstance.mnuFilter.Visible = False
            'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.ShowDialog().html'.
            'frm_TableDisplay.DefInstance.ShowDialog() 
            'mti Augut 2004 3
            'Session("LastYear") = oldLastYear
            'Session("FirstYear") = oldFirstYear
        End If
        'jrs 2005/10/19
        If Session("LongVarTable") Is Nothing Or Session("LongVarTable") <> True Then
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
        End If
    End Sub

    '=============================frm_LongPlotProEssential ======================
    'JRS 11/18/02
    'Mohammod .Net Win Forms Feb 2004
    'mtiwebform June 2004 Longitudinal - take longplotPE sub to Function
    'Public Sub LoadPlotVec(ByRef V As Object, ByRef i As Short, ByRef v2 As Object)
    '    '------------------------------------------------------------------------------------------
    '    Dim PlotLoad(UBound(V), UBound(V, 2)) As Object
    '    ReDim Session("PlotLabelsPE")(UBound(v2), UBound(v2, 2))
    '    'UPGRADE_WARNING: Couldn't resolve default property of object V. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    PlotLongPE = V
    '    'UPGRADE_WARNING: Couldn't resolve default property of object v2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    Session("PlotLabelsPE") = v2
    '    Session("PlotSizePE") = i
    'End Sub
    '=============================frm_LongPlotProEssential ======================
    'Mohammod .Net Win Forms Feb 2004
    Public Sub LoadForeVec(ByRef V As Object, ByRef i As Short, ByRef v2 As Object, ByRef Degree As Short, ByRef Transform As String, Optional ByRef TC1 As Double = 0, Optional ByRef TC2 As Double = 0, Optional ByRef TC3 As Double = 0)

        Dim rr As Short
        Dim i2 As Object
        Dim cc As Short
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation-new proc
        'calculate forecast using Regression Class
        On Error GoTo Forecasterr
        Dim SaturatePoint As Double
        Dim X, Y As Double
        Dim a, a1, b1, b As Double
        ReDim Session("ForeLong")(UBound(V) + Session("YearsAhead"), UBound(V, 2))
        ReDim Session("ForeLabels")(UBound(v2), UBound(v2, 2) + Session("YearsAhead"))
        ReDim Session("RSQR")(UBound(V, 2))
        '    ForeLong = V
        '    ForeLabels = v2
        Session("ForeSize") = i


        For cc = 1 To Session("ForeSize")
            If Transform = "SCU" Then
                If Session("PlotLongPE")(1, cc) <= Session("PlotLongPE")(Session("DisYear"), cc) Then
                    'growth
                    Session("AvgGrowthRate") = (Session("PlotLongPE")(Session("DisYear"), cc) - Session("PlotLongPE")(1, cc)) / Session("DisYear")
                    SaturatePoint = CDbl(Session("PlotLongPE")(1, cc) + Session("AvgGrowthRate") * DefYearsAhead) ' will be setup depending on the series
                    If SaturatePoint < Session("PlotLongPE")(Session("DisYear"), cc) Then
                        SaturatePoint = SaturatePoint + Session("PlotLongPE")(Session("DisYear"), cc)
                    End If
                Else
                    'decay
                    Session("AvgGrowthRate") = (Session("PlotLongPE")(1, cc) - Session("PlotLongPE")(Session("DisYear"), cc)) / Session("DisYear")
                    SaturatePoint = CDbl(Session("PlotLongPE")(1, cc)) + Session("AvgGrowthRate")
                End If
                'keep parameter 1 textbox blank for different saturatepoint for each series
                'Mohammod .Net Win Forms Feb 2004
                'If txtP1.Text = "" Then TC1 = SaturatePoint 'YData(Session("DisYear")%)

            End If

            With reg
                .Degree = Degree
                For i2 = 1 To CInt(Session("DisYear")) 'Session("LastYear")% - Session("FirstYear")% + 1
                    'Format(Session("PlotLong")(Session("PYear")%, cc%), "0.000")
                    'jrs 2012/01/30
                    If Not IsNumeric(v2(1, i2)) Then
                        Select Case Transform
                            Case "LOG"
                                X = CDbl(System.Math.Log(i2))
                            Case "EXP"
                                X = CDbl(TC1 * System.Math.Exp(TC2 * i2))
                            Case Else
                                X = CDbl(i2)
                        End Select
                    Else
                        Select Case Transform
                            Case "LOG"
                                X# = CDbl(System.Math.Log(v2(1, i2) - v2(1, 1) + 1))
                            Case "EXP"
                                X# = CDbl(TC1 * System.Math.Exp(TC2 * (v2(1, i2) - v2(1, 1) + 1)))
                            Case Else
                                X# = CDbl(v2(1, i2) - v2(1, 1) + 1)
                        End Select
                    End If

                    'mti August 2004 4 - debugged vb6 also
                    If Session("PlotLongPE")(i2, cc).ToString <> "" Then
                        Y = CDbl(Session("PlotLongPE")(i2, cc)) '1

                        If Transform = "SCU" Then
                            'jrs 2023/11/03
                            If Y = 0 Then Y = Y + 0.001
                            'jrs 2023/11/03 added < it used to be only =
                            If TC1 <= Y Then TC1 = Y + 0.001
                            Y = System.Math.Log((TC1 / Y) - 1)
                        End If
                        'Debug.Print("X = " & X.ToString & ", Y = " & Y.ToString)
                        .XYAdd(X, Y) 'Ln(i2), Format(Session("PlotLong")(i2, 1), "0.000")
                    End If
                Next i2
            End With
            If Transform = "SCU" Then 'S-Curve Linear Transformation

                a1 = reg.Coeff(0) 'Y=a + bt
                b1 = reg.Coeff(1)
                a = System.Math.Exp(a1)
                b = -b1
                For rr = 1 To Session("DisYear") + Session("YearsAhead")
                    '=limit/(1+param_a*EXP(-param_b*A14))
                    'jrs 2015/01/19
                    'jrs 2012/01/30
                    'If Not IsNumeric(v2(1, rr)) Then
                    '    Session("ForeLong")(rr, cc) = TC1 / (1 + a * System.Math.Exp(-b * rr))
                    'Else
                    '    If rr <= Session("DisYear") Then
                    '        Session("ForeLong")(rr, cc) = TC1 / (1 + a * System.Math.Exp(-b * (v2(1, rr) - v2(1, 1) + 1)))
                    '    Else
                    '        Session("ForeLong")(rr, cc) = TC1 / (1 + a * System.Math.Exp(-b * (v2(1, Session("DisYear")) - v2(1, 1) + 1 + rr - Session("DisYear"))))
                    '    End If
                    'End If

                    If rr <= Session("DisYear") Then
                        If Not IsNumeric(v2(1, rr)) Or Not IsNumeric(v2(1, 1)) Then
                            Session("ForeLong")(rr, cc) = TC1 / (1 + a * System.Math.Exp(-b * rr))
                        Else
                            Session("ForeLong")(rr, cc) = TC1 / (1 + a * System.Math.Exp(-b * (v2(1, rr) - v2(1, 1) + 1)))
                        End If
                    Else
                        If Not IsNumeric(v2(1, Session("DisYear"))) Then
                            Session("ForeLong")(rr, cc) = TC1 / (1 + a * System.Math.Exp(-b * rr))
                        Else
                            Session("ForeLong")(rr, cc) = TC1 / (1 + a * System.Math.Exp(-b * (v2(1, Session("DisYear")) - v2(1, 1) + 1 + rr - Session("DisYear"))))
                        End If
                    End If


                Next rr
            Else
                For rr = 1 To Session("DisYear") + Session("YearsAhead") 'Step Session("Interval")%
                    'jrs 2012/01/30
                    If Not IsNumeric(v2(1, 1)) Then
                        Select Case Transform
                            Case "LOG"
                                X = CDbl(System.Math.Log(rr))
                            Case "EXP"
                                X = CDbl(TC1 * System.Math.Exp(TC2 * rr))
                            Case Else
                                X = CDbl(rr)
                        End Select
                    Else
                        If rr <= Session("DisYear") Then
                            Select Case Transform
                                Case "LOG"
                                    X = CDbl(System.Math.Log((v2(1, rr) - v2(1, 1) + 1)))
                                Case "EXP"
                                    X = CDbl(TC1 * System.Math.Exp(TC2 * (v2(1, rr) - v2(1, 1) + 1)))
                                Case Else
                                    X = CDbl((v2(1, rr) - v2(1, 1) + 1))
                            End Select
                        Else
                            Select Case Transform
                                Case "LOG"
                                    X = CDbl(System.Math.Log((v2(1, Session("DisYear")) - v2(1, 1) + 1 + rr - Session("DisYear"))))
                                Case "EXP"
                                    X = CDbl(TC1 * System.Math.Exp(TC2 * (v2(1, Session("DisYear")) - v2(1, 1) + 1 + rr - Session("DisYear"))))
                                Case Else
                                    X = CDbl((v2(1, Session("DisYear")) - v2(1, 1) + 1 + rr - Session("DisYear")))
                            End Select
                        End If
                    End If

                    Session("ForeLong")(rr, cc) = reg.RegVal(CDbl(X)) '+ TC1
                    'Pego1.PointLabels(rr%) = Session("PlotLabels")(cc%, Session("DisYear")% - 1) + rr% - Session("DisYear")%
                Next rr
            End If
        Next cc

        Session("LongPlotFore") = True

        CalcRSQR()
        'DisplayGraphFore()


        Exit Sub
        '  Pego1.Points = Pego1.Points - 10
Forecasterr:

        LoadMessage("Error occurred while calculating " & Session("ExtpType") & " trend.", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub


    '=============================frm_LongPlotProEssential ======================
    Private Sub CalcRSQR()
        Dim rr As Short
        Dim cc As Short
        Dim ForeMean As Object
        Dim Foretot As Object
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation-new proc
        Dim SUMOFYSQR, YBAR, SQROFYSUM As Double
        Dim SSE, SST As Double
        Dim ObsNo As Short
        'UPGRADE_WARNING: Couldn't resolve default property of object Foretot. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Foretot = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object ForeMean. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ForeMean = 0
        For cc = 1 To Session("PlotSizePE")
            SSE = 0
            SST = 0
            YBAR = 0
            SUMOFYSQR = 0
            ObsNo = 0
            For rr = 1 To Session("DisYear")
                If IsNumeric(Session("PlotLongPE")(rr, cc)) Then
                    SSE = SSE + (Session("PlotLongPE")(rr, cc) - Session("ForeLong")(rr, cc)) ^ 2
                    YBAR = YBAR + Session("PlotLongPE")(rr, cc)
                    SUMOFYSQR = SUMOFYSQR + Session("PlotLongPE")(rr, cc) ^ 2
                    ObsNo = ObsNo + 1
                End If
            Next rr
            SQROFYSUM = YBAR ^ 2
            YBAR = YBAR / ObsNo

            For rr = 1 To Session("DisYear")
                'If Session("PlotLong")(rr, cc) <> Pego1.NullDataValue Then
                If IsNumeric(Session("PlotLongPE")(rr, cc)) Then
                    SST = SST + (Session("PlotLongPE")(rr, cc) - YBAR) ^ 2
                End If
            Next rr
            '    SST = SUMOFYSQR - (SQROFYSUM / ObsNo)
            Session("RSQR")(cc) = 1 - (SSE / SST)
        Next cc
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e



        'Choose the correct function
        Select Case objEA.Item.Caption
                'Case "Continue"
                '    mnuContinue_Click(sender, e)
            Case "Save", "Print"
            Case "Common", "Scale" '"Delete Selected Driver/Dimension"
                mnuCommon_Click(sender, e)
            Case "Table"
                mnuTable_Click(sender, e)
            Case "Linear"
                mnuLinear_Click(sender, e)
                    'SetGroupReg(sender, e,objEA.Item.Caption)
            Case "Polynomial" '"Annotate Scenario"
                mnuPolynom_Click(sender, e)
            Case "Logarithmic"
                mnulogarithmic_Click(sender, e)
            Case "Exponential"
                mnuExponential_Click(sender, e)
                    'SetGroupReg(sender, e,objEA.Item.Caption)
            Case "S-Curve" '"Annotate Scenario"
                mnuS_Click(sender, e)
            Case "Extrapolation Setup" '"Annotate Scenario"
                mnuSetExtrapolation_Click(sender, e)
            Case "Set Title, Display Interval, or Year" 'jrs 2006/05/08
                Session("DisplayControlParent") = "frm_LongPlot.aspx"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
            Case "Format Legend Labels"
                If Session("COUNTE") > 0 Then
                    ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                End If
                Session("COUNTE") = Session("PlotSizePE")
                Dim i As Integer
                For i = 1 To Session("PlotSizePE")
                    If Session("FDNG1")(i) = "" Then
                        If Session("GType") = "SCAT" Then
                            Session("FDNG1")(i) = Session("PlotLabelsPE")(1, i)
                        Else
                            If Session("LV_Chk_Time") = 1 Then
                                Session("FDNG1")(i) = IIf(i = 1, Session("VarNameInd"), Session("VarNameDep"))
                            Else
                                Session("FDNG1")(i) = Session("CountryList")(i)
                            End If
                        End If
                    End If
                Next i

                Session("GraphLabelsParent") = "frm_LongPlot.aspx"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_GraphLabels.aspx")
        End Select
        PackJS()
    End Sub

    Private Sub PackJS()
        'jrs 2011/09/20
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
        sJscript2 = sJscript2 & "fontSize: '30px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''



        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/02/25 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "chart: {" & vbCr
        'jrs 2024/04/29 added and Session("WVSTIME") = False
        If Session("GType") = "SCAT" And Session("WVSTIME") = False Then
            sJscript2 = sJscript2 & "type: 'scatter'," & vbCr
        End If
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
        'jrs 2023/11/03
        sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '30px'}," & vbCr

        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxisXLabel As String
        sAxisXLabel = getXAxisLabel()
        Dim sAxisYLabel As String
        sAxisYLabel = getYAxisLabel()

        If Session("GType") = "SCAT" Then
            sJscript2 = sJscript2 & "tooltip: {" & vbCr
            sJscript2 = sJscript2 & "formatter: function () {" & vbCr
            sJscript2 = sJscript2 & "var symbol;"
            sJscript2 = sJscript2 & "switch ( this.point.graphic.symbolName ) {"
            sJscript2 = sJscript2 & "case 'circle':"
            sJscript2 = sJscript2 & "symbol = '●';"
            sJscript2 = sJscript2 & "break;"
            sJscript2 = sJscript2 & "case 'diamond':"
            sJscript2 = sJscript2 & "symbol = '♦';"
            sJscript2 = sJscript2 & "break;"
            sJscript2 = sJscript2 & "case 'square':"
            sJscript2 = sJscript2 & "symbol = '■';"
            sJscript2 = sJscript2 & "break;"
            sJscript2 = sJscript2 & "case 'triangle':"
            sJscript2 = sJscript2 & "symbol = '▲';"
            sJscript2 = sJscript2 & "break;"
            sJscript2 = sJscript2 & "case 'triangle-down':"
            sJscript2 = sJscript2 & "symbol = '▼';"
            sJscript2 = sJscript2 & "break;"
            sJscript2 = sJscript2 & "}"
            sJscript2 = sJscript2 & "return '<span style=""color:' + this.series.color + '"">' + symbol + '</span>' + ' ' + this.point.name + '<br>" & sAxisXLabel & ": ' + this.point.x + '<br>" & sAxisYLabel & ": ' + this.point.y;"
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        'sJscript2 = sJscript2 & "subtitle: {" & vbCr
        'sJscript2 = sJscript2 & "text:       'Source: WorldClimate.com'," & vbCr
        'sJscript2 = sJscript2 & "x: -20" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        'jrs 2024/04/15 categories for WVS
        If Session("LV_Cohort") = "Yes" And Session("LV_lbl_Ind") = "TIME" Then
            sJscript2 = sJscript2 & "categories: [" & vbCr
            For j = 1 To CInt(Session("DisYear")) Step Session("Interval")
                If j > 1 Then
                    sJscript2 = sJscript2 & ", "
                End If
                sJscript2 = sJscript2 & """" & Session("PlotLabelsPE")(1, j) & """"
            Next
            sJscript2 = sJscript2 & "]," & vbCr
        End If
        'jrs 2023/11/03
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr 'jrs 2023/07/18 changed from 14px
        sJscript2 = sJscript2 & "}}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        'jrs 2023/11/03
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr 'jrs 2023/07/18 changed from 14px
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'Load Years
        '----------
        'If Session("GType") <> "SCAT" Then
        '    'jrs 2015/09/11 removing categories from Bar chart, problematic for multiple series
        '    'it wasn't showing all bars in the first and last categories
        '    sJscript2 = sJscript2 & ", categories: ["
        '    Dim i As Integer
        '    For i = 1 To Session("DisYear") Step Session("Interval")
        '        If i > 1 Then
        '            sJscript2 = sJscript2 & ","
        '        End If
        '        sJscript2 = sJscript2 & "'" & CStr(Session("FirstYear") + i - 1) & "'"
        '    Next
        '    sJscript2 = sJscript2 & "]" & vbCr
        'End If
        'jrs 2015/09/08
        'If Session("GType") <> "SCAT" Then
        '    If Session("DisYear") > 21 Or Session("Interval") > 1 Then
        '        If Session("Interval") > 1 Then
        '            sJscript2 = sJscript2 & ", tickInterval: " & Session("Interval") & vbCr
        '        Else
        '            sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr
        '        End If
        '    End If
        'End If

        'jrs 2015/09/18
        'If Session("GType") <> "SCAT" Then
        '    sJscript2 = sJscript2 & ", min: " & Session("FirstYear") & vbCr
        '    sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("DisYear") - 1) & vbCr
        'End If

        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        'jrs 2023/11/03
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr

        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        'jrs 2023/11/03
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr 'jrs 2023/07/18 changed from 14px
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr
        'sJscript2 = sJscript2 & "text:       '" & getYAxisLabel() & "'," & vbCr
        'sJscript2 = sJscript2 & "style: {" & vbCr
        'sJscript2 = sJscript2 & "fontSize:   '20px'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr


        'jrs 2015/09/08
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
        sJscript2 = sJscript2 & "borderWidth: 0, " & vbCr
        'jrs 2023/11/03
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        sJscript2 = sJscript2 & "color: '#000000'," & vbCr
        'sJscript2 = sJscript2 & "fontWeight: 'bold'," & vbCr
        Dim sFontSize As String = "14"
        If Session("PlotSizePE") < 10 Then
            sFontSize = "21"
        ElseIf Session("PlotSizePE") < 20 Then
            sFontSize = "18"
        ElseIf Session("PlotSizePE") < 30 Then
            sFontSize = "16"
        End If
        sJscript2 = sJscript2 & "fontSize: '" & sFontSize & "px'" & vbCr 'jrs 2023/07/18 changed from 14px
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr
        'jrs 2015/09/18
        If Session("GType") = "SCAT" Then
            sJscript2 = sJscript2 & "plotOptions: {" & vbCr
            sJscript2 = sJscript2 & "   series: {" & vbCr
            sJscript2 = sJscript2 & "       point: {" & vbCr
            sJscript2 = sJscript2 & "           events: {" & vbCr
            sJscript2 = sJscript2 & "               click: function () {" & vbCr
            sJscript2 = sJscript2 & "                   this.options.dataLabels.enabled = !this.options.dataLabels.enabled;" & vbCr
            sJscript2 = sJscript2 & "                   this.update();" & vbCr
            sJscript2 = sJscript2 & "               }" & vbCr
            sJscript2 = sJscript2 & "           }" & vbCr
            sJscript2 = sJscript2 & "       }" & vbCr
            sJscript2 = sJscript2 & "   }" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim sZeros As String = ""
        If Session("GType") = "SCAT" Then
            For i = 1 To Session("CountryTot")
                If i > 1 Then
                    sJscript2 = sJscript2 & "," & vbCr
                End If
                sJscript2 = sJscript2 & PlotPairToJSSeries(Session("PlotSizePE") / 2, i)
            Next
        Else
            For i = 1 To Session("PlotSizePE")
                If i > 1 Then
                    sJscript2 = sJscript2 & "," & vbCr
                End If
                sJscript2 = sJscript2 & PlotColToJSSeries(i)
            Next

            If Session("LongPlotFore") Then
                For i = 1 To Session("ForeSize")
                    sJscript2 = sJscript2 & "," & PlotForeToJSSeries(i)
                Next
            End If
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

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub

    Private Function getYAxisLabel() As String
        If Session("YAxisLabel") <> "" Then
            getYAxisLabel = Session("YAxisLabel")
        ElseIf Session("GType") = "SCAT" Then
            getYAxisLabel = Session("LV_lbl_Dep")
        Else
            getYAxisLabel = Session("LV_sUnits")
        End If
    End Function

    Private Function getXAxisLabel() As String
        If Session("XAxisLabel") > "" Then
            getXAxisLabel = Session("XAxisLabel")
        ElseIf Session("GType") = "SCAT" Then
            getXAxisLabel = Session("LV_lbl_Ind")
        Else
            getXAxisLabel = "Year"
        End If
    End Function

    Private Function PlotPairToJSSeries(ByVal VarCount As Integer, ByVal CtryIdx As Integer) As String
        Dim idx As Integer = Session("DisYear")
        Dim iNumDecX As Integer = 0, iNumDecY As Integer = 0
        Dim j As Integer, i As Integer, d1 As String
        i = CtryIdx
        j = 1
        If IsNumeric(Session("PlotLongPE")(i, j)) Then
            If Math.Abs(Val(Session("PlotLongPE")(i, j))) < 1 Then
                iNumDecX = 3
            ElseIf Math.Abs(Val(Session("PlotLongPE")(i, j))) < 10 Then
                iNumDecX = 2
            ElseIf Math.Abs(Val(Session("PlotLongPE")(i, j))) < 100 Then
                iNumDecX = 1
            End If
        Else
            iNumDecX = 2
        End If

        If IsNumeric(Session("PlotLongPE")(i, j + VarCount)) Then
            If Math.Abs(Val(Session("PlotLongPE")(i, j + VarCount))) < 1 Then
                iNumDecY = 3
            ElseIf Math.Abs(Val(Session("PlotLongPE")(i, j + VarCount))) < 10 Then
                iNumDecY = 2
            ElseIf Math.Abs(Val(Session("PlotLongPE")(i, j + VarCount))) < 100 Then
                iNumDecY = 1
            End If
        Else
            iNumDecY = 2
        End If

        Dim DimCount As Integer
        DimCount = 1

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "Lime"
                Case 2
                    sColor = "Blue"
                Case 3
                    sColor = "Red"
                Case 4 'jrs 2023/11/20 Sandy Brown
                    sColor = "RGB(244,114,0)"
                Case 5 'blue violet
                    'jrs 2023/11/20 sColor = " color: ""#8A2BE2"""
                    sColor = "RGB(138,43,226)"
                Case 6 'aqua 
                    'jrs 2023/11/20 sColor = " color: ""#00FFFF"""
                    sColor = "RGB(0,255,255)"
                Case 7 'deep pink
                    'jrs 2023/11/20 sColor = " color: ""#FF1493"""
                    sColor = "RGB(255,20,147)"
                Case 8 'medium spring green
                    'jrs 2023/11/20 sColor = " color: ""#00FA9A"""
                    sColor = "RGB(0,250,154)"
            End Select
        End If

        Dim sJscript As String = "", sdataLabelsEnabled As String = ""
        sJscript = sJscript & "{name: """ & Session("CountryList")(CtryIdx) & """," & vbCr
        If sColor > "" Then
            sJscript = sJscript & "color: '" & sColor & "'," & vbCr
        End If
        sJscript = sJscript & "dataLabels: {" & vbCr
        sJscript = sJscript & "enabled: true," & vbCr
        sdataLabelsEnabled = "enabled: true"
        sJscript = sJscript & "format: '{point.name}'" & vbCr
        sJscript = sJscript & "}," & vbCr
        sJscript = sJscript & "data: ["

        Dim bFirst As Boolean = True
        Dim sColorHex As String = ""
        For j = 1 To VarCount
            If IsNumeric(Session("PlotLongPE")(i, j)) And IsNumeric(Session("PlotLongPE")(i, j + VarCount)) Then
                If Not bFirst Then
                    sJscript = sJscript & ","
                Else
                    bFirst = False
                End If
                d1 = LTrim(RTrim(Session("FDNG1")(j)))
                If d1 = "" Then
                    d1 = Session("PlotLabels")(i, j)
                End If
                sJscript = sJscript & "{name: """ & d1 &
                """, y:" & CStr(Math.Round(Val(Session("PlotLongPE")(i, j + VarCount)), iNumDecY)) & ", x: " & CStr(Math.Round(Val(Session("PlotLongPE")(i, j)), iNumDecX)) &
                ", dataLabels: {" & sdataLabelsEnabled & "}}" & vbCr
            End If
        Next j
        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "} " & vbCr

        PlotPairToJSSeries = sJscript

    End Function

    Private Function PlotColToJSSeries(ByVal i As Integer) As String

        Dim sJscript As String = ""
        sJscript = sJscript & "{name: '" & getSeriesName(i) & "'," & vbCr

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "Lime"
                Case 2
                    sColor = "Blue"
                Case 3
                    sColor = "Red"
                Case 4 'jrs 2023/11/20 Sandy Brown
                    sColor = "RGB(244,114,0)"
                Case 5 'blue violet
                    'jrs 2023/11/20 sColor = " color: ""#8A2BE2"""
                    sColor = "RGB(138,43,226)"
                Case 6 'aqua 
                    'jrs 2023/11/20 sColor = " color: ""#00FFFF"""
                    sColor = "RGB(0,255,255)"
                Case 7 'deep pink
                    'jrs 2023/11/20 sColor = " color: ""#FF1493"""
                    sColor = "RGB(255,20,147)"
                Case 8 'medium spring green
                    'jrs 2023/11/20 sColor = " color: ""#00FA9A"""
                    sColor = "RGB(0,250,154)"
            End Select
        End If
        If sColor > "" Then
            sJscript = sJscript & "color: '" & sColor & "'," & vbCr
        End If

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim Diff As Single, Min As Single, Max As Single
        Min = 10 ^ 10
        Max = -10 ^ 10
        Dim iNumDec As Integer = 0
        Session("iNumDec") = 0
      
        'jrs 2024/04/16 move it out of if isnumeric() use it too for WVS
            If Session("LV_CommonMenu") = "Common" Then
                'Find Min and Max
                For j = 1 To Session("DisYear")
                    If IsNumeric(Session("PlotLongPE")(j, i)) Then
                        If Val(Session("PlotLongPE")(j, i)) < Min Then
                            Min = Val(Session("PlotLongPE")(j, i))
                        End If
                        If Val(Session("PlotLongPE")(j, i)) > Max Then
                            Max = Val(Session("PlotLongPE")(j, i))
                        End If
                    End If
                Next
                Diff = Max - Min
                If Diff = 0 Then
                    Diff = 0.00001
                End If
            ElseIf Session("LV_CommonMenu") = "Base1" Then 'jrs 2014/03/21
                'Find First Year value
                For j = 1 To Session("DisYear")
                    If IsNumeric(Session("PlotLongPE")(j, i)) Then
                        If Val(Session("PlotLongPE")(j, i)) > 0 Then
                            FstVal = Val(Session("PlotLongPE")(j, i))
                            Exit For
                        End If
                    End If
                Next
            Else
                Dim idx As Integer = Session("DisYear")
                If IsNumeric(Session("PlotLongPE")(idx, 1)) Then
                    If Math.Abs(Val(Session("PlotLongPE")(idx, 1))) < 1 Then
                        iNumDec = 3
                    ElseIf Math.Abs(Val(Session("PlotLongPE")(idx, 1))) < 10 Then
                        iNumDec = 2
                    ElseIf Math.Abs(Val(Session("PlotLongPE")(idx, 1))) < 100 Then
                        iNumDec = 1
                    End If
                Else
                    iNumDec = 2
                End If
                Session("iNumDec") = iNumDec
            End If

        If IsNumeric(Session("PlotLabelsPE")(1, 1)) Then



            For j = 1 To Session("DisYear") Step Session("Interval")
                If IsNumeric(Session("PlotLongPE")(j, i)) Then
                    sJscript = sJscript & "[" & CStr(Session("PlotLabelsPE")(1, j)) & ", "
                    If Session("LV_CommonMenu") = "Common" Then
                        sJscript = sJscript & CStr(Math.Round((Val(Session("PlotLongPE")(j, i)) - Min) / Diff, 2)) & "]"
                    ElseIf Session("LV_CommonMenu") = "Base1" And FstVal > 0 Then 'jrs 2014/03/21
                        sJscript = sJscript & CStr(Math.Round((Val(Session("PlotLongPE")(j, i))) / FstVal, 2)) & "]"
                    Else
                        sJscript = sJscript & CStr(Math.Round(Val(Session("PlotLongPE")(j, i)), iNumDec)) & "]"
                    End If

                    If j < Session("DisYear") Then
                        sJscript = sJscript & ", "
                    End If
                    'ElseIf Session("GType") = "BAR" Then
                    '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                    '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
                End If
            Next

            If j - Session("Interval") <> Session("DisYear") Then
                j = Session("DisYear")
                If IsNumeric(Session("PlotLongPE")(j, i)) Then
                    sJscript = sJscript & "[" & CStr(Session("PlotLabelsPE")(1, j)) & ", "
                    If Session("LV_CommonMenu") = "Common" Then
                        sJscript = sJscript & CStr((Val(Session("PlotLongPE")(j, i)) - Min) / Diff)
                    ElseIf Session("LV_CommonMenu") = "Base1" And FstVal > 0 Then 'jrs 2014/03/21
                        sJscript = sJscript & CStr(Math.Round((Val(Session("PlotLongPE")(j, i))) / FstVal, 2)) & "]"
                    Else
                        sJscript = sJscript & CStr(Val(Session("PlotLongPE")(j, i))) & "]"
                    End If
                    'ElseIf Session("GType") = "BAR" Then
                    '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                    '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
                End If
            End If
        Else 'Cohorts (WVS)
            'jrs 2024/04/15
            Dim bFirst As Boolean = True
            For j = 1 To Session("DisYear") Step Session("Interval")
                If IsNumeric(Session("PlotLongPE")(j, i)) Then
                    'jrs 2024/04/15
                    If bFirst Then
                        bFirst = False
                    Else
                        sJscript = sJscript & ", "
                    End If

                    If Session("LV_CommonMenu") = "Common" Then
                        'jrs 2024/04/15
                        'sJscript = sJscript & "{name : """ & Session("PlotLabelsPE")(1, j) &
                        '""" y=""" & CStr((Val(Session("PlotLongPE")(j, i)) - Min) / Diff) & """}" & vbCr
                        sJscript = sJscript & "[" & (j - 1).ToString &
                        ", " & CStr((Val(Session("PlotLongPE")(j, i)) - Min) / Diff) & "]" & vbCr
                    ElseIf Session("LV_CommonMenu") = "Base1" And FstVal > 0 Then 'jrs 2014/03/21
                        'jrs 2024/04/15
                        'sJscript = sJscript & CStr(Math.Round((Val(Session("PlotLongPE")(1, j))) / FstVal, 2)) & "]"
                        sJscript = sJscript & "[" & (j - 1).ToString &
                        ", " & CStr(Math.Round((Val(Session("PlotLongPE")(1, j))) / FstVal, 2)) & "]"
                    Else
                        'jrs 2024/04/15 
                        'sJscript = sJscript & "{name : """ & Session("PlotLabelsPE")(1, j) &
                        '""" y: """ & CStr(Val(Session("PlotLongPE")(j, i))) & """}" & vbCr
                        sJscript = sJscript & "[" & (j - 1).ToString & ", " &
                        CStr(Val(Session("PlotLongPE")(j, i))) & "]" & vbCr
                    End If
                End If
            Next

            If j - Session("Interval") <> Session("DisYear") Then
                j = Session("DisYear")
                If IsNumeric(Session("PlotLongPE")(j, i)) Then
                    If Session("LV_CommonMenu") = "Common" Then
                        'jrs 2024/04/15
                        'sJscript = sJscript & "{name : """ & Session("PlotLabelsPE")(1, j) &
                        '""" y : """ & CStr((Val(Session("PlotLongPE")(j, i)) - Min) / Diff) & """}" & vbCr
                        sJscript = sJscript & "[" & (j - 1).ToString &
                        ", " & CStr((Val(Session("PlotLongPE")(j, i)) - Min) / Diff) & "]" & vbCr
                    ElseIf Session("LV_CommonMenu") = "Base1" And FstVal > 0 Then 'jrs 2014/03/21
                        'jrs 2024/04/15
                        'sJscript = sJscript & CStr(Math.Round((Val(Session("PlotLongPE")(1, j))) / FstVal, 2)) & "]"
                        sJscript = sJscript & "[" & (j - 1).ToString &
                        ", " & CStr(Math.Round((Val(Session("PlotLongPE")(1, j))) / FstVal, 2)) & "]"
                    Else
                        'jrs 2024/04/15
                        'sJscript = sJscript & "{name : """ & Session("PlotLabelsPE")(1, j) &
                        '""" y : """ & CStr(Val(Session("PlotLongPE")(j, i))) & """}" & vbCr
                        sJscript = sJscript & "[" & (j - 1).ToString & ", " &
                        CStr(Val(Session("PlotLongPE")(j, i))) & "]" & vbCr
                    End If
                End If
            End If

        End If
        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToJSSeries = sJscript

    End Function

    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim d1, Legend As String
        d1 = LTrim(RTrim(Session("FDNG1")(ii)))

        If d1 = "" Then
            If Session("LV_Chk_Time") = 1 Then
                Legend = IIf(ii = 1, Session("LV_lbl_Ind"), Session("LV_lbl_Dep"))
            Else
                Legend = Session("CountryList")(ii)
            End If
        Else
            Legend = d1
        End If
        If Legend.Contains("'") Then
            Legend = Legend.Replace("'", "\'") 'cdv 2022/07/01
        End If
        getSeriesName = Legend
    End Function

    Private Function PlotForeToJSSeries(ByVal i As Integer) As String
        Dim sJscript As String = ""
        Dim sColor As String = ""
        If i + Session("PlotSizePE") < 9 Then
            Select Case i + Session("PlotSizePE")
                Case 1
                    sColor = " color: ""Lime"""
                Case 2
                    sColor = " color: ""Blue"""
                Case 3
                    sColor = " color: ""Red"""
                Case 4 'jrs 2023/11/20 Sandy Brown
                    sColor = "RGB(244,114,0)"
                Case 5 'blue violet
                    'jrs 2023/11/20 sColor = " color: ""#8A2BE2"""
                    sColor = "RGB(138,43,226)"
                Case 6 'aqua 
                    'jrs 2023/11/20 sColor = " color: ""#00FFFF"""
                    sColor = "RGB(0,255,255)"
                Case 7 'deep pink
                    'jrs 2023/11/20 sColor = " color: ""#FF1493"""
                    sColor = "RGB(255,20,147)"
                Case 8 'medium spring green
                    'jrs 2023/11/20 sColor = " color: ""#00FA9A"""
                    sColor = "RGB(0,250,154)"
            End Select
        End If
        If sColor > "" Then
            sJscript = sJscript & "{name : """ & getSeriesNameFore(i) & """, " & sColor & ", data: ["
        Else
            sJscript = sJscript & "{name : """ & getSeriesNameFore(i) & """, data: ["
        End If

        'jrs 2015/09/24
        Dim bFirst As Boolean = True

        Dim j As Integer
        'jrs 2012/02/06
        If IsNumeric(Session("PlotLabelsPE")(1, 1)) Then
            For j = 1 To Session("DisYear") + Session("YearsAhead") Step Session("Interval")
                If Not bFirst Then
                    sJscript = sJscript & ", "
                Else
                    bFirst = False
                End If
                If j > Session("DisYear") Then
                    sJscript = sJscript & "[" & CStr(Session("PlotLabelsPE")(1, Session("DisYear")) + j - Session("DisYear")) &
                                                ", " & CStr(Math.Round(Val(Session("ForeLong")(j, i)), Session("iNumDec"))) & "]"

                Else
                    sJscript = sJscript & "[" & CStr(Session("PlotLabelsPE")(1, j)) &
                                                ", " & CStr(Math.Round(Val(Session("ForeLong")(j, i)), Session("iNumDec"))) & "]"
                End If
            Next
        Else
            For j = 1 To Session("DisYear") + Session("YearsAhead") Step Session("Interval")
                If Not bFirst Then
                    sJscript = sJscript & ", "
                Else
                    bFirst = False
                End If
                If j > Session("DisYear") Then
                    sJscript = sJscript & "{name = ""WF" & j &
                                                ", y: " & CStr(Math.Round(Val(Session("ForeLong")(j, i)), Session("iNumDec"))) & "}" & vbCr

                Else
                    sJscript = sJscript & "{name: " & Session("PlotLabelsPE")(1, j) &
                                                ", y:" & CStr(Math.Round(Val(Session("ForeLong")(j, i)), Session("iNumDec"))) & "}" & vbCr
                End If
            Next
        End If

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "}" & vbCr
        PlotForeToJSSeries = sJscript
    End Function

    Private Function getSeriesNameFore(ByVal ii As Integer) As String
        getSeriesNameFore = getSeriesName(ii)
        If Session("RSQR")(ii) <> 0 Then
            getSeriesNameFore = getSeriesNameFore & "-Trend" & "/R(SQR) = " & VB6.Support.Format(Session("RSQR")(ii), "0.0000")
        Else
            getSeriesNameFore = getSeriesNameFore & "-Trend"
        End If
    End Function

End Class
