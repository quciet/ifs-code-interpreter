Option Strict Off
Option Explicit On

Partial Class frm_FuncGraph
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'Do not modify it using the code editor.
        InitializeComponent()

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            '------------------------------------------------------------

            lblJS.Visible = True
            'used in TablFunc2
            'set to nothing to identify in anychart xml file we're doing FuncGraph (and not TablFunc2)
            Session("TotalGraphs") = Nothing
            'This one is used in TablFunc, to change bi-variate functions
            Session("FuncInd") = Nothing

        End If
    End Sub

    'CONVERSION_NOTE: Class 'Gigasoft.ProEssentials.Pego' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents PegoNet1 As Gigasoft.ProEssentials.Pego 
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



    '=============================frm_LongPlotProEssential ======================
    'CONVERSION_NOTE: The code of Event 'frm_LongPlotProEssential.frm_LongPlotProEssential_Load' was moved to Method 'frm_LongPlotProEssential.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_FuncGraph_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_DevelopmentProfile.aspx"
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("TableDisplayParent") & "';"

        'jrs 2011/09/26
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Main Menu"
        cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        'jrs 2005/12/02 AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

        'jrs 2005/12/02 PesgoNet1.PeFunction.ReinitializeResetImage()

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
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/17 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        sJscript2 = sJscript2 & "marginBottom: 100" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sFormula As String = ""
        If Session("TotalGraphs") = Nothing Then
            sFormula = CreateFormulaText(Session("iDevProFunIdx"), Session("BPoints"), Session("APoint"))
        End If

        sJscript2 = sJscript2 & "title: {" & vbCr
        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then
            sJscript2 = sJscript2 & "text:       '" & sFormula & "'," & vbCr
        Else
            sJscript2 = sJscript2 & "text:       ''," & vbCr
        End If
        sJscript2 = sJscript2 & "x: -20, //center" & vbCr
        sJscript2 = sJscript2 & "y: -20, //center" & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'bottom'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'sJscript2 = sJscript2 & "xAxis: {" & vbCr
        'sJscript2 = sJscript2 & "title: {" & vbCr
        'sJscript2 = sJscript2 & "text:       ''" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & ", min: " & Session("FirstYear") & vbCr
        'sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("DisYear") - 1) & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        'sJscript2 = sJscript2 & "title: {" & vbCr
        'sJscript2 = sJscript2 & "text:       '" '" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr


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
        sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then
            'Function
            sJscript2 = sJscript2 & PlotPairToJSSeries(Session("iDevProFunIdx"), 0)

            'Country Point
            sJscript2 = sJscript2 & "," & PlotPairToJSSeries(0, 0)

            'jrs 2013/12/02
            'Second Country Point for compare
            If Not Session("DevProReg1") = Nothing Then
                sJscript2 = sJscript2 & "," & PlotPairToJSSeriesCompare()
            End If
        Else
            'TableFunc2
            If Session("FuncInd") = Nothing Then
                Dim i As Integer, count As Integer
                count = 0
                For i = 1 To TFCount
                    If Session("FuncSelected")(i) = True Then
                        'Function
                        If count > 0 Then
                            sJscript2 = sJscript2 & ","
                        End If
                        count = count + 1
                        sJscript2 = sJscript2 & PlotPairToJSSeries(i, count)
                    End If
                Next i
            Else
                sJscript2 = sJscript2 & PlotPairToJSSeries(Session("FuncInd"), 1)
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

    Private Function PlotPairToJSSeries(ByVal i As Integer, ByVal count As Integer) As String
        Dim idx As Integer = Session("DisYear")
        Dim iNumDecY As Integer = 2

        Dim iNumDecX As Integer = 2

        Dim sJscript As String = "", sdataLabelsEnabled As String = ""

        If i > 0 Then
            sJscript = sJscript & "{name: """ & getSeriesName(i) & """, color: " & getColor(count) & ", zIndex: " & i.ToString & "," & vbCr
        Else
            sJscript = sJscript & "{name: """ & Session("DevProReg") & " - " & Session("DevProYr") & """, type: ""column"", color: ""Blue"", zIndex: 0," & vbCr
        End If
        sJscript = sJscript & "data: ["

        Dim bFirst As Boolean = True
        Dim DimCount As Integer, bPoints As Boolean = False
        bPoints = Session("TFuncPoints")(i) > 0
        If bPoints Then
            DimCount = Session("TFuncPoints")(i)
        Else
            DimCount = 25
        End If

        Dim j As Integer, Xval As Single, Yval As Single

        If i > 0 Then
            For j = 1 To DimCount
                If bPoints Then
                    If IsNumeric(Session("XPoints")(i, j)) And IsNumeric(Session("YPoints")(i, j)) Then
                        If Not bFirst Then
                            sJscript = sJscript & ","
                        Else
                            bFirst = False
                        End If

                        sJscript = sJscript & "{y:" & CStr(Math.Round(Session("YPoints")(i, j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("XPoints")(i, j), iNumDecX)) & "}" & vbCr
                    End If
                Else
                    If Not IsNumeric(Session("MinPoint")(i)) Or Not IsNumeric(Session("MaxPoint")(i)) Then
                        LoadMessage("Minimum or maximum point on table function must be non-null", Session("Message"))
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If
                    'JRS 10/01/02 ERROR HANDLING
                    'we were getting a log(0) error
                    If Session("MinPoint")(i) = Session("MaxPoint")(i) Then
                        Session("MaxPoint")(i) = Session("MinPoint")(i) + 25
                    End If
                    'JRS 08/12/03 it used to be 100, changed to 25
                    Xval = Session("MinPoint")(i) + (Session("MaxPoint")(i) - Session("MinPoint")(i)) / 25 * j
                    Yval = Session("APoint")(i) + Session("BPoints")(i, 1) * Xval _
                            + Session("BPoints")(i, 2) * Xval ^ 2 + Session("BPoints")(i, 3) * Xval ^ 3 _
                            + Session("BPoints")(i, 4) * Math.Log(Xval) + Session("BPoints")(i, 5) * Xval ^ Session("BPoints")(i, 6) _
                            + Session("BPoints")(i, 7) * Math.Exp(Session("BPoints")(i, 8) * Xval)

                    If Not bFirst Then
                        sJscript = sJscript & ","
                    Else
                        bFirst = False
                    End If

                    sJscript = sJscript & "{y:" & CStr(Math.Round(Yval, iNumDecY)) & ", x: " & CStr(Math.Round(Xval, iNumDecX)) & "}" & vbCr
                End If
            Next j
        Else
            sJscript = sJscript & "{y:" & CStr(Math.Round(Session("DPFunVal"), iNumDecY)) & ", x: " & CStr(Math.Round(Session("DPGDPPCP"), iNumDecX)) & "}" & vbCr
        End If
        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "} " & vbCr

        PlotPairToJSSeries = sJscript
    End Function

    Private Function PlotPairToJSSeriesCompare() As String
        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & Session("DevProReg1") & " - " & Session("DevProYr1") &
                """, type:""column"", color:""Red"", data: [" & vbCr 'style=""MySelected""

        Dim j As Integer, Xval As Single, Yval As Single

        sJscript = sJscript & "{y:" & Session("DPFunVal1") & ", x: " & Session("DPGDPPCP1") & "}" & vbCr
        sJscript = sJscript & "]}" & vbCr

        PlotPairToJSSeriesCompare = sJscript
    End Function


    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String = ""
        If Session("TotalGraphs") = Nothing And Session("FuncInd") = Nothing Then
            Legend = Session("sDevProFun")
        Else
            Legend = FuncName(ii)
        End If

        getSeriesName = Legend
    End Function

    Private Function getColor(ByVal i As Integer) As String
        Dim sColor As String = ""
        If Session("TotalGraphs") = Nothing Then
            sColor = """Lime"""
        Else
            If i < 9 Then
                Select Case i
                    Case 1
                        sColor = """Lime"""
                    Case 2
                        sColor = """Blue"""
                    Case 3
                        sColor = """Red"""
                    Case 4 'blue violet
                        sColor = """RGB(138,43,226)"""
                    Case 5 'aqua 
                        sColor = """RGB(0,255,255)"""
                    Case 6 'deep pink
                        sColor = """RGB(255,20,147)"""
                    Case 7 'medium spring green
                        sColor = """RGB(0,250,154)"""
                    Case Else
                        sColor = """RGB(" & (RandD Mod 256).ToString & "," & (RandD Mod 256).ToString & "," & (RandD Mod 256).ToString & ")"""
                End Select
            End If
        End If
        getColor = sColor
    End Function
End Class

