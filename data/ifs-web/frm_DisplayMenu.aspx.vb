Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Partial Class frm_DisplayMenu
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
        Me.ControlArrayInit()
        'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim Msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            'JAM 2016-01-24 Defining AllYears Variables
            If Session("mnuUseAllYrs") = Nothing Then
                Session("mnuUseAllYrs") = False
            End If

            'JAM 2016-01-24 Defining AllYears Variables
            If Session("mnuUseAllYrsTime") = Nothing Then
                Session("mnuUseAllYrsTime") = False
            End If

            'JRS 2/13/02 LABELS lbl_interval.Caption = "Edit table display interval:"
            'JRS 2/13/02 LABELS Txt_Interval.Text = Str$(1)
            'JRS 2/13/02 LABELS lbl_interval.Visible = True
            'JRS 2/13/02 LABELS Txt_Interval.Visible = True
            'JRS 2/13/02 LABELS Session("Interval")% = CInt(Txt_Interval.Text)
            If Session("Interval") <= 0 Then Session("Interval") = 1

            'JRS 2/13/02 LABELS lbl_PieYear.Caption = "Edit year for pie, scatterplot, or map:"
            'JRS 2/13/02 LABELS lbl_PieYear.Visible = True
            'jrs 2008/07/03
            If Session("frm_DisplayMenuTxt_PieYear") Is Nothing Then
                Txt_PieYear.Text = Str(Session("FirstYear"))
            Else
                Txt_PieYear.Text = Session("frm_DisplayMenuTxt_PieYear")
            End If
            'JRS 2/13/02 LABELS Txt_PieYear.Visible = True

            lbl_TableTitle.Text = "Edit title for tables and graphs:"
            txt_TableTitle.Text = Session("TableTitle")
            'JRS 2/13/02 LABELS lbl_TableTitle.Visible = True
            'JRS 2/13/02 LABELS txt_TableTitle.Visible = True

            lbl_CurSelect.Visible = True

            cmb_Horizon.Visible = False
            frm_Horizon.Visible = False
            Carret = "<br>"
            carret2 = "<br><br>"
            '    IFsLabl.FontName = "Arial MT"
            Msg = "Typical Steps in Display:" & carret2
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg & "     1. Identify variable/parameter names to display (Select SELECTNAMES)" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg & "     2. Choose a display form" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg & "        (Select TABLE, LINEGRAPH, BARCHART, or PIECHART)" + Carret
            Msg = Msg & "     3. Improve display appearance (Edit title, year, or interval)" & carret2
            'Msg = Msg & "Touch the F1 key for extended help" & carret2
            Msg = Msg & "Click the mouse button on status box or tables/graphs for definitions"
            Lbl_display.Text = Msg





            '   If Variables already selected, update end years of all in case
            '       some have been run
            '   --------------------------------------------------------------

            If Not (Session("frm_DisplayMenuTxt_PieYear") Is Nothing) Then Txt_PieYear.Text = Session("frm_DisplayMenuTxt_PieYear")
            'jrs 2005/06/07 If Not (Session("frm_DisplayMenutxt_TableTitle") Is Nothing) Then txt_TableTitle.Text = Session("frm_DisplayMenutxt_TableTitle")
            'jrs 09/15/04
            Session("RefreshGraph") = "true"
            'jrs 09/22/04
            Session("NameDispParent") = "frm_DisplayMenu.aspx"
            'jrs 11/11/04
            Session("mapParent") = "frm_DisplayMenu.aspx"
            'jrs 2011/09/06
            Session("GraphDisplayParent") = "frm_DisplayMenu.aspx"
            'Else 'JAM 2016-02-07 Resetting All Year Variables

            '    Session("mnuUseAllYrs") = False
            '    Session("mnuUseAllYrsTime") = False

            'jrs 2021/02/08
            If Session("bAlias") = True Then
                If Not (Session("oGR1N") Is Nothing) Then
                    'jrs 2023/10/26
                    Dim orgIdx As Integer = UBound(Session("oGR1N"))
                    If Session("COUNTE") > orgIdx Then
                        ReDim Preserve Session("oGR1N")(Session("COUNTE"))
                        For i = orgIdx + 1 To Session("COUNTE")
                            Session("oGR1N")(i) = Session("GR1N")(i)
                        Next
                    End If
                    For i = 1 To Session("COUNTE")
                        Session("GR1N")(i) = Session("oGR1N")(i)
                    Next
                End If
                For i = 1 To Session("COUNTE")
                    Session("FDNG1")(i) = Session("GR1N")(i)
                Next
            End If
            Session("bAlias") = False

            'jrs 2021/05/19
            If Session("COUNTE") > 0 Then
                If Session("StaYearHis")(1) > 0 Then
                    If Session("StaYearHis")(1) < FirstYearHistory Then 'jrs 2017/12/07
                        If Not IsNumeric(Session("PLOT")(1, 1)) Then
                            CleanStartPLOTWallAvHis(Session("COUNTE"), Session("StaYearHis"), Session("PLOT"), Session("FinYear"))
                        End If
                    ElseIf Session("FirstYear") = FirstYearForecast Then
                        If Not IsNumeric(Session("PLOT")(1, 1)) Then
                            CleanStartPLOTWCompareForecast(Session("COUNTE"), Session("StaYearHis"), Session("PLOT"), Session("FinYear"))
                        End If
                    End If
                End If
            End If
        End If
        'JRS 02/18/04
        frm_DisplayMenu_Activated()
        'jrs 2011/05/30 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub
    'CONVERSION_NOTE: Method 'Dispose' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
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
    Public DisTime As System.Collections.ArrayList
    Public mnuBarChartStyle As System.Collections.ArrayList
    Public mnuBarChartType As System.Collections.ArrayList
    Public mnuDisTime As System.Collections.ArrayList
    Public mnuLineGraphStyle As System.Collections.ArrayList
    Public mnuLineGraphType As System.Collections.ArrayList
    Public mnuPieChartStyle As System.Collections.ArrayList
    Public mnuPieChartType As System.Collections.ArrayList
    Public mnu_Exit As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnu_Standard As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnu_Flexible As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnu_SelectVariables As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnu_Table As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPELine As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public GraphDisplay As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphType_6 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphType_7 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphType_8 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public LineGraphType As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphStyle_0 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphStyle_1 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphStyle_2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphStyle_3 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphStyle_5 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphStyle_6 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuLineGraphStyle_7 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public LineGraphStyle As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public LineGraph As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public MS2dLine As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public MS3dLine As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public MSLineGraph As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPEBar As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public BarChartDisplay As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuBarChartType_3 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuBarChartType_4 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public BarChartType As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuBarChartStyle_0 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuBarChartStyle_1 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuBarChartStyle_2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuBarChartStyle_3 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuBarChartStyle_4 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuBarChartStyle_5 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public BarChartStyle As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public BarChart As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPEPie As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public PieChartDisplay As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartType_1 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartType_2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public PieChartType As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartStyle_0 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartStyle_1 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartStyle_2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartStyle_3 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartStyle_4 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartStyle_5 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartStyle_6 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuPieChartStyle_7 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public PieChartStyle As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public PieChart As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPEScatter As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public ScatterPlot As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public Graph As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDispCtrl As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnu_Currency As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuGraphLabels As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDisplayRunHorizon As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuDisTime_0 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _mnuDisTime_1 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public _DisTime_0 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public Horizon As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public DisplayFeatures As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public Map As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public Map2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public Gini As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuLorenz As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuSpecialized As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public MnuComboBL As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuValidation As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuHistory As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDefine As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDash1 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDrivers As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDash3 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuExplain As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDash2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuViewEquations As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDash4 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDelete As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPop As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'CONVERSION_NOTE: Following code was added for support purposes.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1016.html'.
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Sub ControlArrayInit()
        Me.DisTime = New System.Collections.ArrayList
        Me.mnuBarChartStyle = New System.Collections.ArrayList
        Me.mnuBarChartType = New System.Collections.ArrayList
        Me.mnuDisTime = New System.Collections.ArrayList
        Me.mnuLineGraphStyle = New System.Collections.ArrayList
        Me.mnuLineGraphType = New System.Collections.ArrayList
        Me.mnuPieChartStyle = New System.Collections.ArrayList
        Me.mnuPieChartType = New System.Collections.ArrayList
        Me.DisTime.Add(_DisTime_0)
        Me.mnuBarChartStyle.Add(_mnuBarChartStyle_0)
        Me.mnuBarChartStyle.Add(_mnuBarChartStyle_1)
        Me.mnuBarChartStyle.Add(_mnuBarChartStyle_2)
        Me.mnuBarChartStyle.Add(_mnuBarChartStyle_3)
        Me.mnuBarChartStyle.Add(_mnuBarChartStyle_4)
        Me.mnuBarChartStyle.Add(_mnuBarChartStyle_5)
        Me.mnuBarChartType.Add(_mnuBarChartType_3)
        Me.mnuBarChartType.Add(_mnuBarChartType_4)
        Me.mnuDisTime.Add(_mnuDisTime_0)
        Me.mnuDisTime.Add(_mnuDisTime_1)
        Me.mnuLineGraphStyle.Add(_mnuLineGraphStyle_0)
        Me.mnuLineGraphStyle.Add(_mnuLineGraphStyle_1)
        Me.mnuLineGraphStyle.Add(_mnuLineGraphStyle_2)
        Me.mnuLineGraphStyle.Add(_mnuLineGraphStyle_3)
        Me.mnuLineGraphStyle.Add(_mnuLineGraphStyle_5)
        Me.mnuLineGraphStyle.Add(_mnuLineGraphStyle_6)
        Me.mnuLineGraphStyle.Add(_mnuLineGraphStyle_7)
        Me.mnuLineGraphType.Add(_mnuLineGraphType_6)
        Me.mnuLineGraphType.Add(_mnuLineGraphType_7)
        Me.mnuLineGraphType.Add(_mnuLineGraphType_8)
        Me.mnuPieChartStyle.Add(_mnuPieChartStyle_0)
        Me.mnuPieChartStyle.Add(_mnuPieChartStyle_1)
        Me.mnuPieChartStyle.Add(_mnuPieChartStyle_2)
        Me.mnuPieChartStyle.Add(_mnuPieChartStyle_3)
        Me.mnuPieChartStyle.Add(_mnuPieChartStyle_4)
        Me.mnuPieChartStyle.Add(_mnuPieChartStyle_5)
        Me.mnuPieChartStyle.Add(_mnuPieChartStyle_6)
        Me.mnuPieChartStyle.Add(_mnuPieChartStyle_7)
        Me.mnuPieChartType.Add(_mnuPieChartType_1)
        Me.mnuPieChartType.Add(_mnuPieChartType_2)
    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_DisplayMenuPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_DisplayMenu
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_DisplayMenu()
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
    '------------------------------------------DisplayMenu.Frm-------------------------------
    '----------------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '

    '   Last changed by BBH on July 17, 2002 - added MTI's validation code

    'UPGRADE_NOTE: DefSng A-Z statement was removed. Variables were explicitly declared as type Single. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1005"'
    Dim LineGraphStyl As Single
    Dim LineGraphTyp As Short
    Dim BarGraphStyl As Single
    Dim BarGraphTyp As Short
    Dim PieGraphStyl As Single
    Dim PieGraphTyp As Short
    Dim ScatGraphStyl As Single
    Dim ScatGraphTyp As Short
    Dim DisTim As Short



    Public Sub BarChart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim iloop As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        If BarGraphStyl < 0 Or BarGraphStyl > 5 Then BarGraphStyl = 0
        If BarGraphTyp < 3 Or BarGraphTyp > 4 Then BarGraphTyp = 3


        For iloop = 0 To 5
            If iloop = BarGraphStyl Then
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartStyle(iloop).Checked = True 
            Else
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartStyle(iloop).Checked = False 
            End If
        Next iloop

        For iloop = 3 To 4
            If iloop = BarGraphTyp Then
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartType(iloop).Checked = True 
            Else
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartType(iloop).Checked = False 
            End If
        Next iloop

    End Sub


    Public Sub BarChartDisplay_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GraphStyle") = BarGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = BarGraphTyp

        On Error GoTo DefaultInterval
        'JRS 2/13/02 LABELS If CInt(Txt_Interval.Text) < 1 Then frmGraphLabels.Txt_Interval.Text = Str$(1)
        'JRS 2/13/02 LABELS Session("Interval")% = CInt(Txt_Interval.Text)
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("GType") = "BAR"
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_LineDisplay.aspx")
        'frm_LineDisplay.DefInstance.Show() 
        Exit Sub

DefaultInterval:
        Session("Interval") = 1
        Resume Next

    End Sub

    '==========================frm_DisplayMenu===================
    Private Sub Display_Time_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If DisTim = 0 Then
            Session("DisplayTime") = 0
            'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuDisTime(0).Checked = True 
            'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuDisTime(1).Checked = False 
        Else
            Session("DisplayTime") = 1
            'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuDisTime(0).Checked = False 
            'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuDisTime(1).Checked = True 
        End If



    End Sub
    '==========================frm_DisplayMenu===================
    Private Sub ScatterError()
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Msg = "For a scattergram you must select all instances "
        Msg = Msg & "of the first variable before selecting the "
        Msg = Msg & "identical instances of the second variable.  "
        Msg = Msg & "Moreover, the sequence of instances "
        Msg = Msg & "for the second variable must be "
        Msg = Msg & "identical to the sequence for the first variable.  "
        Msg = Msg & "E.g. GDP(US), GDP(EU), GDP(JAPAN), POP(US), "
        Msg = Msg & "POP(EU), POP(JAPAN)"
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
        lblMsg.Text = Msg
        lblMsg.Visible = True

    End Sub

    '==========================frm_DisplayMenu===================
    'UPGRADE_WARNING: Event cmb_Horizon.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cmb_Horizon_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged
        Dim Ind As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2008/12/22 changed from 1 to -1, added "" and FirstYear
        Ind = cmb_Horizon.SelectedIndex - 1
        'JRS 11/03/03 CHANGE LASTYEAR WITH DISPLAYLASTYEAR
        If Ind >= 1 Then
            Session("DisplayLastYear") = Session("FirstYear") + Ind * Session("Interval") 'jrs 2008/12/22 
        Else
            Session("DisplayLastYear") = Val(cmb_Horizon.SelectedValue)
            'jrs 2008/12/18 If Session("DisplayLastYear") > LastYearForecast Or Session("DisplayLastYear") < FirstYearForecast + 1 Then Exit Sub
            If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                Session("DisplayLastYear") = Session("LastYear")
                lblMsg.Text = "Invalid Horizon, reseting to " & Session("LastYear").ToString
                lblMsg.Visible = True
            End If
        End If
        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False
        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Refresh' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Refresh().html'.
        'Refresh() 
        cswmWebMenu.Visible = True
    End Sub

    '=============================frm_DisplayMenu==============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ComboBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.KeyPress.html'.
    'JRSVBNET *** BEGIN ***
    'Private Sub cmb_Horizon_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cmb_Horizon.KeyPress
    '    'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '    If KeyAscii = 13 Then Call cmb_Horizon_SelectedIndexChanged(cmb_Horizon, New System.EventArgs) 'enter key
    '    If KeyAscii = 27 Then 'Escape
    '        cmb_Horizon.Visible = False
    '        frm_Horizon.Visible = False
    '        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Refresh' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Refresh().html'.
    '        'Refresh() 
    '    End If

    '    If KeyAscii = 0 Then
    '        'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '        eventArgs.Handled = True
    '    End If
    'End Sub
    'JRSVBNET *** END ***


    '==========================frm_DisplayMenu===================
    'UPGRADE_WARNING: Form event frm_DisplayMenu.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Activated' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Activated.html'.
    Private Sub frm_DisplayMenu_Activated()
        Dim rr As Short
        Dim cc As Short
        Dim TotalColWidth As Single
        Dim ColWidth As Short
        Dim RowHeight As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '
        '   Grid Setup
        '   ----------

        RowHeight = 230
        ColWidth = HalfInch
        TotalColWidth = ColWidth * 5

        grd_VariableDisplay.Visible = True


        '   Fill Grid
        '   ---------

        FillGrid()

        '   Activate Label Also
        '   -------------------

        lbl_CurSelect.Text = "Currently selected names are: (If status box is empty, choose SelectNames)"
        lbl_CurSelect.Visible = True


        '    mnuHistory.Visible = False
        '    For j = 1 To Session("COUNTE")
        '        FileNum = Val(Session("FileNumbr")$(j))
        '        If FileNum = -1 Then
        '            mnuHistory.Visible = True
        '            Exit For 'mti Validation changes
        '        End If
        '    Next j



    End Sub


    '==========================frm_DisplayMenu===================
    Private Sub frm_DisplayMenu_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'jrs 10/05/04
        lblMsg.Visible = False

        cswmWebMenu.UserData = "jsolorza::976739975"
        'JRS 02/27/04
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
        cswmGroup1.ID = "Group_ab50"

        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"

        'jrs 2011/05/30
        If Session("DisplayParent") <> "frm_MainMenu.aspx" Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Main Menu"
            cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        End If

        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Select Names"
        cswmGroup1.ID = "Group_39c0"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Standard Subset (of Variables)"
        'cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Full Set (of Variables/Parameters)"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Table"
        cswmGroup1.ID = "Group_c830"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_TableDisplay.aspx"
        cswmGroup1.OnMenuBarClick = "location.href='frm_TableDisplay.aspx';"
        Session("TableDisplayParent") = "frm_DisplayMenu.aspx"
        Session("LorenzParent") = "frm_DisplayMenu.aspx"
        Session("ProEssentialParent") = "frm_DisplayMenu.aspx"
        Session("GraphDisplayParent") = "frm_DisplayMenu.aspx"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Graph"
        cswmGroup1.ID = "Group_56a0"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Line"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Bar Chart"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Pie Chart"

        'JAM 2009-07-18     Adding Radial Chart Option
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Radial Chart"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Scatter Plot"
        'cswmItem.Enabled = False

        'JAM 2012-07-07     Adding Histogram Chart Option
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Histogram Chart"

        'JAM 2017-08-12 Adding Stacked Bar
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Stacked Bar"

        'jrs 2018/05/17
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Stacked Area"


        'jrs 2008/08/12
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Motion Chart (Using Gapminder by Google)"


        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Format"
        cswmGroup1.ID = "mnuGrpDisplay"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Currency"
        'jrs 2005/03/07
        'cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Horizon"
        cswmItem.ID = "mnuHorizon"

        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()
        cswmGroup2.ID = "mnuHorizonGrp"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Display/Run Horizon"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Run File Horizon Choice (With Multiple File Use)"
        cswmItem.ID = "mnuRunFile"

        Dim cswmGroup3 As Coalesys.WebMenu.Group
        cswmGroup3 = cswmItem.AddGroup()
        'jrs 2007/11/30
        cswmGroup3.ID = "mnuRunFileGrp"

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "Shortest Run File Horizon"
        'jrs 2007/11/30
        cswmItem.ID = "mnuShort"
        If Session("DisplayTime") = 0 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "Longest Run File Horizon"
        'jrs 2007/11/30
        cswmItem.ID = "mnuLong"
        If Session("DisplayTime") = 1 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Regional Displays"
        cswmGroup1.ID = "Group_7380"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "WMap"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Calculate Gini"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Lorenz"

        'JAM 2016-01-23 Adding AllYear for statistics
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Analysis"
        cswmGroup1.ID = "Group_7381"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use all Years (in Statistics)"

        'JAM 2010-01-24 
        If Session("mnuUseAllYrs") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use all Years and Time (in Statistics)"

        'JAM 2010-01-24 
        If Session("mnuUseAllYrsTime") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/03 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,6522);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "History"
        'cswmGroup1.ID = "Group_01f0"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Combination"


    End Sub


    Public Sub Gini_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 10/10/02

        On Error GoTo HandleWrongVarSelected

        'jrs 2006/10/25
        Dim i As Integer, j As Integer
        If Not (grd_VariableDisplay Is Nothing) Then
            For i = 0 To grd_VariableDisplay.Columns.Count - 1
                For j = 0 To grd_VariableDisplay.RowCount - 1
                    grd_VariableDisplay(i, j) = ""
                Next j
            Next i
        End If

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

        '   Fill Grid
        '   ---------
        FillGrid()
        'END PROCESS

        'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2049"'
        'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2036"'
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

HandleWrongVarSelected:
        'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2049"'
        'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2036"'
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Msg = "Please select one variable across all countries/regions "
        Msg = Msg & " and then return to Gini calculation."
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
        lblMsg.Text = Msg
        lblMsg.Visible = True

    End Sub


    Public Sub GraphDisplay_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GraphStyle") = LineGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = LineGraphTyp
        Session("GType") = "LINE"
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_LineDisplay.aspx")
        'frm_LineDisplay.DefInstance.Show() 

    End Sub

    '==========================frm_DisplayMenu===================
    'JRSVBNET *** BEGIN ***
    'Private Sub grd_VariableDisplay_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles grd_VariableDisplay.SelectedIndexChanged
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
    '    'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
    '    Support.FormManager.StateStore(Me)
    '    Support.FormManager.Show(Me, "frm_NameDisp.aspx")
    '    'frm_NameDisp.DefInstance.Show() 

    'End Sub
    'JRSVBNET *** END ***

    'CONVERSION_ISSUE: Event 'AxMSFlexGridLib.AxMSFlexGrid.MouseDownEvent' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.MouseDownEvent.html'.
    'JRSVBNET *** BEGIN ***
    'Private Sub grd_VariableDisplay_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxMSFlexGridLib.DMSFlexGridEvents_MouseDownEvent) Handles grd_VariableDisplay.MouseDownEvent
    '    If Session("COUNTE") > 0 Then
    '        'show popup menu
    '        'Me.PopupMenu mnuPop
    '        'mti 07 Feb 03
    '        If Trim(Session("VARN")(1, Support.DataGridManager.manager.GetCol(grd_VariableDisplay) + 1)) <> "" Then
    '            'UPGRADE_ISSUE: Form method frm_DisplayMenu.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
    '            'JRSVBNET Me.PopupMenu(mnuPOP)
    '        End If
    '        'end mti 07 Feb 03


    '    End If

    'End Sub
    'JRSVBNET *** END ***


    Public Sub LineGraph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim iloop As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        If LineGraphStyl < 0 Or LineGraphStyl > 7 Or LineGraphStyl = 4 Then LineGraphStyl = 0
        If LineGraphTyp < 6 Or LineGraphTyp > 8 Then LineGraphTyp = 6
        For iloop = 0 To 7
            If iloop <> 4 Then
                If iloop = LineGraphStyl Then
                    'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuLineGraphStyle(iloop).Checked = True 
                Else
                    'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuLineGraphStyle(iloop).Checked = False 
                End If
            End If
        Next iloop

        For iloop = 6 To 8
            If iloop = LineGraphTyp Then
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuLineGraphType(iloop).Checked = True 
            Else
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuLineGraphType(iloop).Checked = False 
            End If
        Next iloop


    End Sub


    Public Sub Map_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Path to ESRI MapObjects

        If Session("COUNTE") <> NR Then
            Msg = "You must select a single variable across all regions "
            Msg = Msg & "in order to display on a map."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'Checking that all the variables are the same
        Dim i As Single
        Dim j As Short
        For i = 2 To NR
            If Session("VARN")(1, 1) <> Session("VARN")(1, i) Then
                Msg = "You must select a single variable across all regions "
                Msg = Msg & "in order to display on a map."
                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
                lblMsg.Text = Msg
                lblMsg.Visible = True
                Exit Sub
            End If
        Next i


        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET 'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1

        Session("RecordSourceDef") = "Map"
        '    DataFieldDef$ = "StoreData"


        'Have commented out next two lines for call to ESRI frm_Map and
        '   its calls to frm_MapParameters and frm_CounChar; removed all 3 forms from IFs
        '   Also removed clsDrawSymbol(DrawSymbol.cls) from IFs project
        '    frm_Map.legMapDisp.LoadLegend True
        '    frm_Map.Show
        'Also took ESRI MO2 out of components for project
        'Also took out several functions:  ClassBreaksEven; FillRegionMap; cmd_ResetLegend
        '       RelateTables; ClassBreaksEqualNumber; ClassBreaksStanDev
        Msg = "ESRI mapping no longer included in IFs; if necessary "
        Msg = Msg & "return to versions before June 17, 2001"
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
        lblMsg.Text = Msg
        lblMsg.Visible = True

    End Sub


    Public Sub Map2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Path to Blue Marble GeoObjects

        If Session("COUNTE") <> NR Then
            Msg = "You must select a single variable across all regions "
            Msg = Msg & "in order to display on a map."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'Checking that all the variables are the same
        Dim i As Single
        Dim j As Short
        For i = 2 To NR
            If Session("VARN")(1, 1) <> Session("VARN")(1, i) Then
                Msg = "You must select a single variable across all regions "
                Msg = Msg & "in order to display on a map."
                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
                lblMsg.Text = Msg
                lblMsg.Visible = True
                Exit Sub
            End If
        Next i


        Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        'jrs 2008/07/01
        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        Session("RecordSourceDef") = "Map"

        'jrs 10/30/04
        Session("MapChosenTable") = "Map"
        Session("MapFieldName") = "StoreData"
        Session("MapVar") = Session("VARN")(1, 1)
        Support.FormManager.Show(Me, "frm_mapViewer.aspx")

    End Sub

    '==========================frm_DisplayMenu===================
    Private Sub mnu_ChangeCurrency_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRSVBNET frm_CurrencyOpt.DefInstance.Show()

    End Sub


    Public Sub mnu_Currency_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRSVBNET frm_CurrencyOpt.DefInstance.Show()
        'jrs 2008/05/08
        Session("CurrencyParent") = "frm_DisplayMenu.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_CurrencyOpt.aspx")

    End Sub


    Public Sub mnu_Exit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'JRS 8/5/01 GUI frm_MainMenu.Show
        'JRS 02/25/04
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")

    End Sub


    Public Sub mnu_Flexible_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '    If Session("Scenarioflag") = True Then
        '       msg$ = "Selections have been determined within the scenario creation "
        '        msg$ = msg$ + "process.  If you proceed to override those, you will "
        '        msg$ = msg$ + "invalidate your scenario.  Do you wish to proceed?"
        '        response = MsgBox(msg$, vbYesNo)
        '        If response <> 6 Then
        '            Exit Sub
        '        Else
        '            Session("Scenarioflag") = False
        '        End If
        '    End If

        'CONVERSION_ISSUE: Property 'IFsGUI.frm_VarSel.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET Support.FormManager.Close(frm_VarSel.DefInstance) 'This allows reset of all with new load
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_VarSel.aspx")
        'frm_VarSel.DefInstance.Show() 

    End Sub

    '==========================frm_DisplayMenu===================
    Private Sub mnu_PopulationCohorts_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Hide().html'.
        'Hide() 
        'JRSVBNET frm_CohoDisp.DefInstance.Show()

    End Sub

    Public Sub mnu_SelectVariables_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN ***
        ''JRS 11/25/02
        ''Check if Table Display is loaded and reset to whole values (vs percent)
        'Dim fr As System.Web.UI.Page
        ''UPGRADE_ISSUE: Forms collection was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'

        'For Each fr In Forms
        '    If fr.Name = "frm_TableDisplay" Then
        '        If frm_TableDisplay.DefInstance.mnuPercent.Text = "&Whole" Then
        '            'Update Plot Vector
        '            frm_TableDisplay.DefInstance.ResetPLOT()
        '        End If
        '    End If
        'Next fr
        'JRSVBNET *** END ***
    End Sub


    Public Sub mnu_Standard_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '    If Session("Scenarioflag") = True Then
        '        msg$ = "Selections have been determined within the scenario creation "
        '        msg$ = msg$ + "process.  If you proceed to override those, you will "
        '        msg$ = msg$ + "invalidate your scenario.  Do you wish to proceed?"
        '        response = MsgBox(msg$, vbYesNo)
        '        If response <> 6 Then
        '            Exit Sub
        '        Else
        '            Session("Scenarioflag") = False
        '        End If
        '    End If


        'JRSVBNET frm_StanDisp.DefInstance.Show()

    End Sub


    Public Sub mnu_Table_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a table"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        On Error GoTo DefaultInterval2
        'JRS 2/13/02 LABELS If CInt(Txt_Interval.Text) < 1 Then Txt_Interval.Text = Str$(1)
        'JRS 2/13/02 LABELS Session("Interval")% = CInt(Txt_Interval.Text)
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("TableTitle") = txt_TableTitle.Text
        'mti Validation changes
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuValidation.Checked = False 
        'mti Validation changes
        'mti Validation changes July 28, 2002
        'JRS 12/26/02
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET Support.FormManager.Close(frm_TableDisplay.DefInstance)
        WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = "Table Display"
        'mti Validation changes July 28, 2002

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
        'frm_TableDisplay.DefInstance.Show() 
        Exit Sub

DefaultInterval2:
        Session("Interval") = 1
        Resume Next

    End Sub

    '==========================frm_DisplayMenu===================
    Private Sub mnu_WorldValueSurveyCohorts_Click()
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        '    frm_DisplayMenu.Hide
        '    frm_WVSDisp.Show

        'UPGRADE_WARNING: Couldn't resolve default property of object WVSCohorts. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("COUNTE") <> WVSCohorts Then
            Msg = "This type of display is only suitable for variables representing "
            Msg = Msg & "values by age cohort (6 cohorts plus total) for a single country/region, "
            Msg = Msg & "such as MATPOSTR."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GraphStyle") = 1 ' = Horizontal BarGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = 3 'BarGraphTyp

        Session("GType") = "COHORT"
        Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        Session("TableTitle") = txt_TableTitle.Text

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_LineDisplay.aspx")
        'frm_LineDisplay.DefInstance.Show() 
        Exit Sub

    End Sub

    '==========================frm_DisplayMenu===================
    Public Sub mnuBarChartStyle_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuBarChartStyle_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuBarChartStyle_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim index As Short = mnuBarChartStyle.IndexOf(eventSender)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuBarChartStyle(0).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuBarChartStyle(1).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuBarChartStyle(2).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuBarChartStyle(3).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuBarChartStyle(4).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuBarChartStyle(5).Checked = False 

        Select Case index
            Case 0 'Vertical Bars
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartStyle(0).Checked = True 
                BarGraphStyl = 0
            Case 1 'Horizontal
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartStyle(1).Checked = True 
                BarGraphStyl = 1
            Case 2 'Stacked
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartStyle(2).Checked = True 
                BarGraphStyl = 2
            Case 3 'Horizontal Stacked
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartStyle(3).Checked = True 
                BarGraphStyl = 3
            Case 4 'Stacked Percent
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartStyle(4).Checked = True 
                BarGraphStyl = 4
            Case 5 'Horizontal Stacked Percent
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartStyle(5).Checked = True 
                BarGraphStyl = 5
        End Select


    End Sub

    '==========================frm_DisplayMenu===================
    Public Sub mnuBarChartType_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuBarChartType_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuBarChartType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim index As Short = mnuBarChartType.IndexOf(eventSender)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuBarChartType(3).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuBarChartType(4).Checked = False 

        Select Case index
            Case 3 'Two Dimensional
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartType(3).Checked = True 
                BarGraphTyp = 3
            Case 4 'Three Dimensional
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuBarChartType(4).Checked = True 
                BarGraphTyp = 4
        End Select

    End Sub


    Public Sub MnuComboBL_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("TableTitle") = txt_TableTitle.Text

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GraphStyle") = LineGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = LineGraphTyp
        Session("GType") = "LINE"
        'frm_LineDisplay.Show
        'JRSVBNET frm_ChartHist.DefInstance.Show()

    End Sub




    Public Sub mnuDispCtrl_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("DisplayControlParent") = "frm_DisplayMenu.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
        'frm_DisplayControl.DefInstance.Show() 
    End Sub


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

        'jrs 2006/07/14
        cmb_Horizon.Items.Clear()
        Dim ly As Integer
        'jrs 2008/12/22 if you pick 2010, and then try to come back you don't
        'get the option to plot higher years, unless you type them
        'If Session("DisplayLastYear") > 0 Then
        '    ly = Session("DisplayLastYear")
        'Else
        '    ly = LastYearForecast
        'End If
        ly = Session("LastYearForecast")

        'jrs 2008/12/18
        cmb_Horizon.Items.Add(New ListItem(""))
        cmb_Horizon.Items.Add(New ListItem(Session("FirstYear").ToString))

        For ICount = Session("FirstYear") + Session("Interval") To ly Step Session("Interval")
            cmb_Horizon.Items.Add(New ListItem(CStr(ICount)))
        Next ICount
        WinFormsToWeb_FocusControl.Focus("cmb_Horizon")

        'jrs 2012/03/09
        txtHorizon.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdHorizon.ClientID + "')")
        cswmWebMenu.Visible = False
    End Sub

    '==========================frm_DisplayMenu===================
    Public Sub mnuDisTime_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuDisTime_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuDisTime_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim index As Short = mnuDisTime.IndexOf(eventSender)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuDisTime(0).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuDisTime(1).Checked = False 

        Select Case index
            Case 0 'Short File
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuDisTime(0).Checked = True 
                DisTim = 0
                Session("DisplayTime") = 0
            Case 1 'Long File
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuDisTime(1).Checked = True 
                DisTim = 1
                Session("DisplayTime") = 1
        End Select

    End Sub

    Public Sub mnuDrivers_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN *** 
        Dim curr_col As Integer
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim pos1 As Short

        curr_col = Session("VarSelGridCol") + 1

        pos1 = InStr(Session("VARN")(1, curr_col), "[")
        Session("myWidth") = txtWidth.Value
        Session("myHeight") = txtHeight.Value
        Session("DrvDim1") = Session("GR1N")(curr_col)
        Session("DrvDim2") = Session("GR2N")(curr_col)
        Session("DrvDim3") = Session("GR3N")(curr_col)
        If pos1 > 0 Then
            Session("DrvFl") = getFileNum(Session("VARN")(1, curr_col), Session("vShortSce"))
        Else
            Session("DrvFl") = 0
        End If
        If pos1 > 0 Then
            Support.FormManager.Show(Me, "frm_Drivers.aspx?VName=" & Mid(Session("VARN")(1, curr_col), 1, pos1 - 1))
        Else
            Support.FormManager.Show(Me, "frm_Drivers.aspx?VName=" & Session("VARN")(1, curr_col))
        End If
    End Sub

    Public Sub mnuGraphLabels_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("GraphLabelsParent") = "frm_DisplayMenu.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_GraphLabels.aspx")
        'frm_GraphLabels.DefInstance.Show() 
    End Sub


    Public Sub MnuHistory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        Dim ii As Short
        Dim FileNum As Single
        Dim j As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '    MnuComboBL.Enabled = False
        '    If Session("COUNTE") > 0 Then
        '        For j = 1 To Session("COUNTE")
        '            FileNum = Val(Session("FileNumbr")$(j))
        '            If FileNum = -1 Then
        '                MnuComboBL.Enabled = True
        '                Exit Sub
        '            End If
        '        Next j
        '    End If
        Dim sValidationVar As String 'mti Validation changes

        MnuComboBL.Enabled = False
        'mti Validation changes
        'JRS 04/01/03 mnuValidation.Enabled = False
        'mnuValidation.Visible = False
        'mti Validation changes

        If Session("COUNTE") > 0 Then
            For j = 1 To Session("COUNTE")
                FileNum = Val(Session("FileNumbr")(j))
                If FileNum = -1 Then
                    MnuComboBL.Enabled = True
                    Exit For 'mti Validation changes
                End If
            Next j
            'mti Validation changes
            For j = 1 To Session("COUNTE") - 1
                sValidationVar = Trim(Session("VARN")(4, j))
                If Mid(sValidationVar, 1, 2) = "VH" Then 'asc("V") = 86 'Mid(sValidationVar, 1, 2) = "VH"
                    sValidationVar = "VF" & Mid(sValidationVar, 3)
                    For ii = j + 1 To Session("COUNTE")
                        If Trim(Session("VARN")(4, ii)) = sValidationVar Then
                            'JRS 04/01/03 mnuValidation.Enabled = True
                            'mnuValidation.Visible = True
                            Exit Sub
                        End If
                    Next ii
                End If
            Next j
            'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuValidation.Checked = False 
        Else
            Msg = "You must select one or more variables before you can proceed"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuValidation.Checked = False 
            Exit Sub

            'mti Validation changes
        End If


    End Sub

    '==========================frm_DisplayMenu===================
    Public Sub mnuLineGraphStyle_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuLineGraphStyle_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuLineGraphStyle_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim index As Short = mnuLineGraphStyle.IndexOf(eventSender)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphStyle(0).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphStyle(1).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphStyle(2).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphStyle(3).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphStyle(5).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphStyle(6).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphStyle(7).Checked = False 

        Select Case index
            Case 0 'Line
                LineGraphStyl = 0
                    'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuLineGraphStyle(0).Checked = True 
            Case 1 'Symbols
                LineGraphStyl = 1
                    'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuLineGraphStyle(1).Checked = True 
            Case 2 'Sticks
                LineGraphStyl = 2
                    'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuLineGraphStyle(2).Checked = True 
            Case 3 'Sticks and Symbols
                LineGraphStyl = 3
                    'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuLineGraphStyle(3).Checked = True 
            Case 5 'Lines and Symbols
                LineGraphStyl = 5
                    'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuLineGraphStyle(5).Checked = True 
            Case 6 'Lines and Sticks
                LineGraphStyl = 6
                    'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuLineGraphStyle(6).Checked = True 
            Case 7 'Lines and Sticks and Symbols
                LineGraphStyl = 7
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuLineGraphStyle(7).Checked = True 
        End Select
    End Sub

    '==========================frm_DisplayMenu===================
    Public Sub mnuLineGraphType_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuLineGraphType_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuLineGraphType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim index As Short = mnuLineGraphType.IndexOf(eventSender)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphType(6).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphType(7).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuLineGraphType(8).Checked = False 

        Select Case index
            Case 6 'Line
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuLineGraphType(6).Checked = True 
                LineGraphTyp = 6
            Case 7 'Log/Lin
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuLineGraphType(6).Checked = True 
                LineGraphTyp = 7
            Case 8 'Area
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuLineGraphType(6).Checked = True 
                LineGraphTyp = 8
        End Select

    End Sub


    Public Sub mnuLorenz_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 09/24/02
        On Error GoTo HandleWrongVarSelected
        '   Call Computation of Gini and setup of Lorenz curve
        '   --------------------------------------------------

        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        GiniSetup(IfsPars, Session("COUNTE"), Nothing, Session("VARN"), Session("LastYear"), Session("FirstYear"),
        Session("PutFdrvRun"), Session("ActiveBase"), Session("RunFiles"), Session("bLoadForDisplaySuccess"),
        Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
        Session("CurrencyVal"), Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis"),
        Session("LastYearForecast"), Session("bComputation"), Session("bFillHisOnlyHoles"),
        Session("LorenzX_Year"), Session("LorenzY_Year"), Session("PLOT"), Session("LorenzX"),
        Session("LorenzY"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("YAxisLabel"),
        Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))

        '   Fill Grid
        '   ---------
        FillGrid()

        'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2049"'
        'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2036"'
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_LorenzGraph.aspx")
        'frm_LorenzGraph.DefInstance.Show() 
        Exit Sub
HandleWrongVarSelected:
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 10/01/04 WinFormsToWeb_MessageBox.Show("Please select apropiate variables")
        lblMsg.Text = "Please select apropiate variables"
        lblMsg.Visible = True
    End Sub


    Public Sub mnuPEBar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("TableTitle") = txt_TableTitle.Text

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'Session("GraphStyle") = LineGraphStyl

        'Session("GraphType") = GPM_LINE
        On Error GoTo DefaultInterval
        'JRS 2/13/02 LABELS If CInt(Txt_Interval.Text) < 1 Then Txt_Interval.Text = Str$(1)
        'JRS 2/13/02 LABELS Session("Interval")% = CInt(Txt_Interval.Text)
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("GType") = "BAR"
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
        'jrs 09/14/04         Page.Response.Redirect("frm_ProEssential.aspx?" & CStr(Date.Now))
        'location.href = "yourpage" + "?" + new Date().getTime();
        'frm_ProEssential.DefInstance.Show() 

        Exit Sub

DefaultInterval:
        Session("Interval") = 1
        Resume Next

    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''                       JAM 2017-08-12 Adding Stacked Bar Graph
    ''' 
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    ''' 
    Public Sub mnuPEStackedBar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("TableTitle") = txt_TableTitle.Text

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'Session("GraphStyle") = LineGraphStyl

        'Session("GraphType") = GPM_LINE
        On Error GoTo DefaultInterval
        'JRS 2/13/02 LABELS If CInt(Txt_Interval.Text) < 1 Then Txt_Interval.Text = Str$(1)
        'JRS 2/13/02 LABELS Session("Interval")% = CInt(Txt_Interval.Text)
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("GType") = "BAR"
        Session("PatternSel") = Nothing 'JAM 2017-08-12
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_StackedBar.aspx")
        'jrs 09/14/04         Page.Response.Redirect("frm_ProEssential.aspx?" & CStr(Date.Now))
        'location.href = "yourpage" + "?" + new Date().getTime();
        'frm_ProEssential.DefInstance.Show() 

        Exit Sub

DefaultInterval:
        Session("Interval") = 1
        Resume Next

    End Sub

    'jrs 2018/05/17
    Private Sub mnuStackedArea_Click()
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If



        Session("GType") = "STACKEDAREA"

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")

    End Sub

    Public Sub mnuPELine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'Session("GraphStyle") = LineGraphStyl

        'Session("GraphType") = GPM_LINE


        Session("GType") = "LINE"

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
        'jrs 09/14/04 Page.Response.Redirect("frm_ProEssential.aspx?" & CStr(Date.Now))
        'frm_ProEssential.DefInstance.Show() 



    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' JAM 2012-07-07 Calling Histogram Chart
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    Public Sub mnuPEHistogram_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If
        'Session("GType") = "HIST"
        On Error GoTo DefaultInterval
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("GType") = "BAR"
        Session("isfromDispMenu") = True  'JAM 2013-01-13
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Histogram.aspx")


        Exit Sub
DefaultInterval:
        Session("Interval") = 1
        Resume Next






        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'JAM 2012-07-07 Validations 
        'TableTitle = txt_TableTitle
        'lbl_PieYear.Caption = "Edit year for scatterplot:"
        'If COUNTE = 0 Then
        '    Msg$ = "You must select one or more variables before you create a display"
        '    MsgBox(Msg$)
        '    Exit Sub
        'End If
        ''   Divert to statistics form if more than two variables selected
        ''   -------------------------------------------------------------

        ''jrs 2010/09/14 changed from 2 to 3 times NR

        'If COUNTE = NR% Or ValidateSetForHistogram Then    'Must alter to check for regionalized variables

        '    'Have candidate for scattergram; check it out

        '    '   Check that Scattergram variable selections are correct

        '    '   ------------------------------------------------------

        '    'jrs 2008/10/21

        '    Dim Vars As New Collection
        '    Dim Dim1s As New Collection
        '    Dim i As Integer
        '    Dim D As Object, v As Object

        '    'Generates error when Item not found, then add it

        '    On Error Resume Next

        '    For i = 1 To COUNTE
        '        v = Empty
        '        v = Vars.Item(VARN(1, i))
        '        If IsEmpty(v) Then
        '            Vars.Add(i, VARN(1, i))
        '        End If



        '        D = Empty
        '        D = Dim1s.Item(GR1N(i))
        '        If IsEmpty(D) Then
        '            Dim1s.Add(i, GR1N(i))
        '        End If
        '    Next

        '    'Back to original handler

        '    On Error GoTo 0



        '    a = Vars.Count
        '    b = Dim1s.Count



        '    If Vars.Count <> 1 And Dim1s.Count <> COUNTE Then
        '        'msg = "To display a motion chart you must select 2 or 3 Variables across the same set of Regions:  dependent, independent, and bubble sizing (optional)."
        '        'lblMsg.text = msg
        '        'lblMsg.Visible = True
        '        '            Call ScatterError
        '        '            Exit Sub
        '        Msg$ = "You must pick only one variable across all regions for a histogram."
        '        MsgBox(Msg$)
        '        Exit Sub
        '    End If

        '    '

        '    GType$ = "HIST"
        '    PYear% = CInt(Txt_PieYear.Text) - FirstYear% + 1
        '    If PYear% < 1 Then PYear% = 1
        '    If PYear% > LastYear% - FirstYear% + 1 Then PYear% = LastYear% - FirstYear% + 1
        '    'jrs 2008/07/01

        '    If PYear > FinYear(1) Then PYear = FinYear(1)
        '    frm_histogram.Show()

        'Else  'Divert to statistics form

        '    Msg$ = "You must pick only one variable across all regions for a histogram."
        '    MsgBox(Msg$)
        '    Exit Sub
        'End If


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub
    Public Sub mnuPEPie_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        If Session("COUNTE") <= 1 Then
            Msg = "You must select at least two variables before you create a pie chart"
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'Session("GraphStyle") = PieGraphStyl
        'Session("GraphType") = PieGraphTyp
        Session("GType") = "PIE"
        Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        'jrs 2008/07/01
        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        '   Check for Limits on Pie Chart
        '   -----------------------------

        '????
        If Session("GType") = "PIE" And Session("COUNTE") > 75 Then
            Msg = "You have selected too many points for a pie chart, the maximum is 75."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'jrs 2008/09/26
        'check for negatives
        If CheckNegsPLOT(Session("COUNTE"), Session("FinYear"), Session("PLOT")) Then
            Msg$ = "There are negative values loaded. This is not allowed for a Pie chart."
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If


        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
        'frm_ProEssential.DefInstance.Show() 
        '    txt_PieYear.Visible = False
        '    lbl_PieYear.Visible = False

    End Sub
    'JAM 2009-07-18 Radial Option
    Public Sub mnuPERadial_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)


        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")

        Session("TableTitle") = txt_TableTitle.Text


        If Session("COUNTE") = 0 Then
            Exit Sub
        End If


        If Session("COUNTE") <= 2 Then
            lblMsg.Text = "You must select at least three variables before you create a radial chart"
            lblMsg.Visible = True
            Exit Sub
        End If



        Session("PYear") = 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then
            Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        End If

        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        Session("GType") = "RADIAL"
        Session("SourceRadial") = "Package"
        Session("MultipleYears") = False
        Session("MultipleGraphs") = False

        Session("BaseRadialParent") = "frm_DisplayMenu.aspx"

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_BaseRadial.aspx")
    End Sub


    '==========================frm_DisplayMenu===================
    Private Sub mnuPEPopulationCohorts_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Hide().html'.
        'Hide() 
        'JRSVBNET frm_CohoDisp.DefInstance.Show()

    End Sub


    Public Sub mnuPEScatter_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim MinLen As Single
        Dim SD1 As String
        Dim VNP As String
        Dim SVN As String
        Dim D2 As String
        Dim d1 As String
        Dim VN As String
        Dim ii As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("TableTitle") = txt_TableTitle.Text

        lbl_PieYear.Text = "Edit year for scatterplot:"

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'jrs 2011/04/07
        'moved out from if statement
        'jrs 2008/10/21
        Dim Vars As New Collection
        Dim Dim1s As New Collection
        Dim i As Integer
        Dim D As Object, v As Object
        'jrs 2010/10/19
        'allow using different scenarios
        Dim sVar As String, pos As Integer
        'Generates error when Item not found, then add it
        On Error Resume Next
        For i = 1 To Session("COUNTE")
            pos = InStr(Session("VARN")(1, i), "[")
            If pos > 0 Then
                sVar = Mid(Session("VARN")(1, i), 1, pos - 1)
            Else
                sVar = Session("VARN")(1, i)
            End If
            v = Nothing
            'jrs 2010/10/19 v = Vars.Item(VARN(1, i))
            v = Vars.Item(sVar)
            If (v Is Nothing) Then
                'jrs 2010/10/19 Vars.Add i, VARN(1, i)
                Vars.Add(i, sVar)
            End If

            D = Nothing
            D = Dim1s.Item(Session("GR1N")(i))
            If (D Is Nothing) Then
                Dim1s.Add(i, Session("GR1N")(i))
            End If
        Next
        'Back to original handler
        On Error GoTo 0
        Session("VarsCount") = Vars.Count
        'jrs 2012/06/01
        Session("Dim1Count") = Dim1s.Count

        '   Divert to statistics form if more than two variables selected
        '   -------------------------------------------------------------

        If Vars.Count <= 3 Then 'Must alter to check for regionalized variables
            'Have candidate for scattergram; check it out

            If Vars.Count * Dim1s.Count > Session("COUNTE") Or
            (Vars.Count = 1 And Dim1s.Count = Session("COUNTE")) Or
            Vars.Count > 3 Or
            Session("COUNTE") Mod Dim1s.Count <> 0 Then 'jrs 2010/11/04 added COUNTE MOD DIM1S.COUNT <> 0
                'msg = "To display a motion chart you must select 2 or 3 Variables across the same set of Regions:  dependent, independent, and bubble sizing (optional)."
                'lblMsg.text = msg
                'lblMsg.Visible = True
                Call ScatterError()
                Exit Sub
            End If

            Dim jj As Integer
            For jj = 1 To Vars.Count
                SVN$ = LTrim$(RTrim$(Session("VARN")(1, (jj - 1) * Dim1s.Count + 1)))

                For ii = 1 To Dim1s.Count 'jrs 2010/09/14 COUNTE / 2

                    VN$ = LTrim$(RTrim$(Session("VARN")(1, (jj - 1) * Dim1s.Count + ii)))
                    d1$ = LTrim$(RTrim$(Session("GR1N")((jj - 1) * Dim1s.Count + ii)))
                    D2$ = LTrim$(RTrim$(Session("GR2N")((jj - 1) * Dim1s.Count + ii)))

                    If VN$ <> SVN$ Then 'Is the first set complete/consistent?
                        Call ScatterError()
                        Exit Sub
                    End If


                Next ii
            Next jj

            'jrs 2013/04/18
            'Check if the order is fine: Sce1 for all countries and then Sce2 for all countries
            'Flex Displays generates Country1 for all Sces, and then Country2 for all Sces
            If Vars.Count = 1 Then
                If Session("GR1N")(1) = Session("GR1N")(Session("COUNTE") / 2 + 1) Then
                    Session("VarsCount") = 2
                End If
            End If

            Session("bBubble") = (Vars.Count > 2)

            'ScatGraphStyl = 0
            'ScatGraphTyp = 9
            'Session("GraphStyle") = ScatGraphStyl
            'Session("GraphType") = ScatGraphTyp
            Session("GType") = "SCAT"
            Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
            If Session("PYear") < 1 Then Session("PYear") = 1
            If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
            'jrs 2008/07/01
            If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

            Support.FormManager.StateStore(Me)
            'jrs 2011/09/05 Support.FormManager.Show(Me, "frm_ProEssential.aspx")
            Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
            'frm_ProEssential.DefInstance.Show() 
        Else 'Divert to statistics form

            If Session("COUNTE") Mod NR = 0 Then 'only if Session("COUNTE") is even multiplier of NR%
                Session("Statistics_Type") = 0
                Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
                If Session("PYear") < 1 Then Session("PYear") = 1
                If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
                'jrs 2008/07/01
                If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)
                SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
                    Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
                    Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
                    Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
                    Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
                    Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
                    Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))

                'Hide() 
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "Statistics.aspx")
                'frm_statistics.DefInstance.Show() 
            Else
                Msg = "You must pick two or more variables across all regions for a scattergram."
                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
                lblMsg.Text = Msg
                lblMsg.Visible = True
                Exit Sub
            End If
        End If

    End Sub

    '==========================frm_DisplayMenu===================
    Private Sub mnuPEWorldValueSurveyCohorts_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        '    frm_DisplayMenu.Hide
        '    frm_WVSDisp.Show

        'JRS 10/04/02 WVS
        'If Session("COUNTE") <> WVSCohorts Then
        '    Msg$ = "This type of display is only suitable for variables representing "
        '    Msg$ = Msg$ + "values by age cohort (6 cohorts plus total) for a single country/region, "
        '    Msg$ = Msg$ + "such as MATPOSTR."
        '    MsgBox Msg$
        '    Exit Sub
        'End If

        'Session("GraphStyle") = 1  ' = Horizontal BarGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = 3 'BarGraphTyp

        Session("GType") = "COHORT"
        Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        Session("TableTitle") = txt_TableTitle.Text

        'JRSVBNET frm_ProEssentialWVS.DefInstance.Show()
        Exit Sub

    End Sub

    '==========================frm_DisplayMenu===================
    Public Sub mnuPieChartStyle_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuPieChartStyle_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuPieChartStyle_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim index As Short = mnuPieChartStyle.IndexOf(eventSender)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartStyle(0).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartStyle(1).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartStyle(2).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartStyle(3).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartStyle(4).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartStyle(5).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartStyle(6).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartStyle(7).Checked = False 

        Select Case index
            Case 0 'Lines join labels to pie
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(0).Checked = True 
                PieGraphStyl = 0
            Case 1 'No label lines
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(1).Checked = True 
                PieGraphStyl = 1
            Case 2 'Colored labels
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(2).Checked = True 
                PieGraphStyl = 2
            Case 3 'Colored labels without lines
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(3).Checked = True 
                PieGraphStyl = 3
            Case 4 'Percent labels
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(4).Checked = True 
                PieGraphStyl = 4
            Case 5 'Percent labels without lines
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(5).Checked = True 
                PieGraphStyl = 5
            Case 6 'Colored labaels
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(6).Checked = True 
                PieGraphStyl = 6
            Case 7 'Colored labels without lines
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(7).Checked = True 
                PieGraphStyl = 7
        End Select
    End Sub

    '==========================frm_DisplayMenu===================
    Public Sub mnuPieChartType_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuPieChartType_Click(eventSender, eventArgs)
    End Sub
    Public Sub mnuPieChartType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim index As Short = mnuPieChartType.IndexOf(eventSender)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartType(1).Checked = False 
        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuPieChartType(2).Checked = False 

        Select Case index
            Case 1 'Two Dimensional
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartType(1).Checked = True 
                PieGraphTyp = 1
            Case 2 'Three Dimensional
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartType(1).Checked = True 
                PieGraphTyp = 2
        End Select

    End Sub

    Private Sub mnuSocialAccountingMatrix_Click()
        'JRSVBNET frm_SAM.DefInstance.Show()
    End Sub

    Public Sub mnuValidation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes- Whole proc

        Dim sValidationVar As String

        'Dim VH(MaxVariables / 2)
        'Dim VF(MaxVariables / 2)
        'Dim VCOUNTE As Integer
        '
        '    On Error GoTo DefaultInterval2:
        '
        '        VCOUNTE = 0
        '        For j = 1 To Session("COUNTE") - 1  'the last one need to be forecast
        '            sValidationVar = Trim(Session("VARN")$(4, j))
        '            If Asc(sValidationVar) = 86 Then  'asc("V") = 86
        '                sValidationVar = "VF" + Mid(sValidationVar, 3)
        '                For ii% = j + 1 To Session("COUNTE")
        '                    If Trim(Session("VARN")$(4, ii%)) = sValidationVar Then
        '                        VCOUNTE = VCOUNTE + 1
        '                        VH(VCOUNTE) = j
        '                        VF(VCOUNTE) = ii%
        '                        Exit For
        '                    End If
        '                Next ii%
        '            End If
        '        Next j
        '
        '        If VCOUNTE < 1 Then
        '            msg$ = "You must select one or more variables before you can proceed with validation"
        '            MsgBox msg$
        '            mnuValidation.Enabled = True
        '            Exit Sub
        '        End If


        'JRS 2/13/02 LABELS If CInt(Txt_Interval.Text) < 1 Then Txt_Interval.Text = Str$(1)
        'JRS 2/13/02 LABELS Session("Interval")% = CInt(Txt_Interval.Text)
        If Session("Interval") < 1 Then Session("Interval") = 1
        Session("TableTitle") = txt_TableTitle.Text

        'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuValidation.Checked = True 

        'mti Validation changes July 28, 2002
        WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = "Historical Validation"
        'mti Validation changes July 28, 2002

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
        'frm_TableDisplay.DefInstance.Show() 
        Exit Sub

DefaultInterval2:
        Session("Interval") = 1
        Resume Next

    End Sub


    Public Sub mnuViewEquations_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim curr_col As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRSVBNET curr_col = Support.DataGridManager.manager.GetCol(grd_VariableDisplay) + 1
        'JRS 9/13/01 COMPUTATIONS
        'UPGRADE_ISSUE: VBControlExtender property grd_VariableDisplay.HelpContextID was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET grd_VariableDisplay.HelpContextID = Val(Session("VARN")(6, curr_col))
        'CONVERSION_ISSUE: Class System.Windows.Forms.SendKeys from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.SendKeys.Send("{F1}")

    End Sub

    'Private Sub MSGraph_Click()

    '    Session("TableTitle") = txt_TableTitle

    '    If Session("COUNTE") = 0 Then
    '        Msg$ = "You must select one or more variables before you create a display"
    '        MsgBox Msg$
    '        Exit Sub
    '    End If

    '    frm_msgraph.Show

    'End Sub


    Public Sub PieChart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim iloop As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        If PieGraphStyl < 0 Or PieGraphStyl > 7 Then PieGraphStyl = 4
        If PieGraphTyp < 1 Or PieGraphTyp > 2 Then PieGraphTyp = 1

        For iloop = 0 To 7
            If iloop = PieGraphStyl Then
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(iloop).Checked = True 
            Else
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartStyle(iloop).Checked = False 
            End If
        Next iloop

        For iloop = 1 To 2
            If iloop = PieGraphTyp Then
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartType(iloop).Checked = True 
            Else
                'CONVERSION_NOTE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuPieChartType(iloop).Checked = False 
            End If
        Next iloop

        '    lbl_PieYear.Caption = "Edit year for pie chart:"
        '    lbl_PieYear.Visible = True
        '    txt_PieYear.Visible = True



    End Sub


    Public Sub PieChartDisplay_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If Session("COUNTE") <= 1 Then
            Msg = "You must select at least two variables before you create a pie chart"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("GraphStyle") = PieGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = PieGraphTyp
        Session("GType") = "PIE"
        Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1

        '   Check for Limits on Pie Chart
        '   -----------------------------

        If Session("GType") = "PIE" And Session("COUNTE") > 75 Then
            Msg = "You have selected too many points for a pie chart."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_LineDisplay.aspx")
        'frm_LineDisplay.DefInstance.Show() 
        '    txt_PieYear.Visible = False
        '    lbl_PieYear.Visible = False
    End Sub

    '==========================frm_DisplayMenu===================
    Private Sub Report_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '    msg$ = "Preparing to Show Report3"
        '    MsgBox msg$
        'JRS May 15, 2002
        '    Dim sConn As String
        '    sConn = "Provider=MSDataShape.1;Persist Security Info=False;Data Source="
        '    sConn = sConn + PutFdrvRun2$ + "\IFsVar.mdb;Data Provider=MICROSOFT.JET.OLEDB.4.0"
        '    deIFs.deConIFs.ConnectionString = sConn
        '    msg$ = "Preparing to Show Report3 2"
        '    MsgBox msg$

        'JRSVBNET frm_Report3.DefInstance.Show()

    End Sub


    Public Sub ScatterPlot_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim MinLen As Single
        Dim SD1 As String
        Dim VNP As String
        Dim SVN As String
        Dim D2 As String
        Dim d1 As String
        Dim VN As String
        Dim ii As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Session("TableTitle") = txt_TableTitle.Text

        lbl_PieYear.Text = "Edit year for scatterplot:"

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        '   Divert to statistics form if more than two variables selected
        '   -------------------------------------------------------------

        If Session("COUNTE") <= NR * 2 Then 'Must alter to check for regionalized variables
            'Have candidate for scattergram; check it out

            '   Check that Scattergram variable selections are correct
            '   ------------------------------------------------------

            If Session("COUNTE") / 2 <> Session("COUNTE") \ 2 Then 'Are there two sets?
                Msg = "You must select an equal number of instances for each of two "
                Msg = Msg & "variables before creating a scattergram.  "
                Msg = Msg & "E.g. GDP(US), GDP(EU), GDP(JAPAN), POP(US), POP(EU), POP(JAPAN)"
                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
                lblMsg.Text = Msg
                lblMsg.Visible = True
                Exit Sub
            End If

            For ii = 1 To Session("COUNTE") / 2

                VN = LTrim(RTrim(Session("VARN")(1, ii)))
                d1 = LTrim(RTrim(Session("GR1N")(ii)))
                D2 = LTrim(RTrim(Session("GR2N")(ii)))

                SVN = LTrim(RTrim(Session("VARN")(1, 1)))
                If ii <= Session("COUNTE") / 2 Then
                    If VN <> SVN Then 'Is the first set complete/consistent?
                        Call ScatterError()
                        Exit Sub
                    End If
                End If

                SVN = LTrim(RTrim(Session("VARN")(1, ii + Session("COUNTE") / 2)))
                VNP = LTrim(RTrim(Session("VARN")(1, 1 + Session("COUNTE") / 2)))

                If VNP <> SVN Then 'Is the second set complete/consistent?
                    Call ScatterError()
                    Exit Sub
                End If

                SD1 = LTrim(RTrim(Session("GR1N")(ii + Session("COUNTE") / 2)))
                'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                MinLen = Amin(Len(SD1), Len(d1))
                '   Look at shortest in case of %, / Variable
                d1 = VB.Left(d1, MinLen)
                SD1 = VB.Left(SD1, MinLen)
                If d1 <> SD1 Then 'Are dimensions in correct sequence?
                    Call ScatterError()
                    Exit Sub
                End If

            Next ii

            ScatGraphStyl = 0
            ScatGraphTyp = 9
            Session("GraphStyle") = ScatGraphStyl
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("GraphType") = ScatGraphTyp
            Session("GType") = "SCAT"
            Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
            If Session("PYear") < 1 Then Session("PYear") = 1
            If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_LineDisplay.aspx")
            'frm_LineDisplay.DefInstance.Show() 

        Else 'Divert to statistics form

            If Session("COUNTE") Mod NR = 0 Then 'only if Session("COUNTE") is even multiplier of NR%
                Session("Statistics_Type") = 0
                Session("PYear") = CShort(Txt_PieYear.Text) - Session("FirstYear") + 1
                If Session("PYear") < 1 Then Session("PYear") = 1
                If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
                'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Hide().html'.
                'Hide() 
                'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_statistics.aspx")
                'frm_statistics.DefInstance.Show() 
            Else
                Msg = "You must pick two or more variables across all regions for a scattergram."
                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'jrs 10/01/04 WinFormsToWeb_MessageBox.Show(Msg)
                lblMsg.Text = Msg
                lblMsg.Visible = True
                Exit Sub
            End If
        End If

    End Sub

    '==========================frm_DisplayMenu===================
    Private Sub Title_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        lbl_TableTitle.Visible = True
        txt_TableTitle.Visible = True

    End Sub


    Public Sub mnuDefine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_NameDisp.aspx")
        'frm_NameDisp.DefInstance.Show() 
    End Sub


    Public Sub MnuDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN ***
        'Dim Label3 As String
        'Dim Label2 As String
        'Dim Label1 As String
        'Dim iloop As Short
        'Dim ICount As Short
        'Dim i2 As Single
        'Dim Msg As String
        'Dim curr_col As Single
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'curr_col = Support.DataGridManager.manager.GetCol(grd_VariableDisplay) + 1
        'Msg = Session("VARN")(1, curr_col) & "<br>" & Session("VARN")(2, curr_col) & "<br>"
        'Msg = Msg & Session("GR1N")(curr_col) & "-" & Session("GR2N")(curr_col) & "-" & Session("GR3N")(curr_col)
        'Msg = Msg & "<br>" & Chr(10) & Chr(10) & "Delete this varaiable?"
        'Dim dbV As DAOJR.Database
        ''CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'If WinFormsToWeb_MessageBox.Show(Msg, MsgBoxStyle.YesNo, "Variable Selected") = MsgBoxResult.Yes Then
        '    ' Delete Variable
        '    Session("COUNTE") = Session("COUNTE") - 1 ''Do later?
        '    For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
        '        For ICount = curr_col To Session("COUNTE")
        '            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, ICount + 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            Session("PLOT")(i2, ICount) = Session("PLOT")(i2, ICount + 1)
        '        Next ICount
        '        'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, Session("COUNTE") + 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '        Session("PLOT")(i2, Session("COUNTE") + 1) = 0
        '    Next i2
        '    ' Take care of Grid Display & Array Variables Shifting
        '    For iloop = curr_col To Session("COUNTE") Step 1
        '        Session("FileNumbr")(iloop) = Session("FileNumbr")(iloop + 1)
        '        Session("VARN")(0, iloop) = Session("VARN")(0, iloop + 1)
        '        Session("VARN")(1, iloop) = Session("VARN")(1, iloop + 1)
        '        Session("VARN")(2, iloop) = Session("VARN")(2, iloop + 1)
        '        Session("VARN")(3, iloop) = Session("VARN")(3, iloop + 1)
        '        Session("VARN")(6, iloop) = Session("VARN")(6, iloop + 1)
        '        'mti Validation changes July 28, 2002
        '        Session("VARN")(4, iloop) = Session("VARN")(4, iloop + 1)
        '        'mti Validation changes July 28, 2002
        '        'mti Validation changes 19 August, 2002
        '        Session("VARN")(5, iloop) = Session("VARN")(5, iloop + 1)
        '        'mti Validation changes 19 August, 2002

        '        Session("GR1N")(iloop) = Session("GR1N")(iloop + 1)
        '        Session("GR2N")(iloop) = Session("GR2N")(iloop + 1)
        '        Session("GR3N")(iloop) = Session("GR3N")(iloop + 1)
        '        Support.DataGridManager.manager.SetCol(grd_VariableDisplay, iloop - 1)
        '        Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
        '        Support.DataGridManager.SetColWidth(grd_VariableDisplay, iloop - 1, Inch)
        '        Support.DataGridManager.SetText(grd_VariableDisplay, Session("VARN")(1, iloop))
        '        'grd_VariableDisplay.Col = Session("COUNTE") - 1
        '        Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 1)
        '        Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR1N")(iloop))
        '        Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 2)
        '        Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR2N")(iloop))
        '        Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 3)
        '        Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR3N")(iloop))
        '    Next iloop
        '    Session("VARN")(0, Session("COUNTE") + 1) = ""
        '    Session("VARN")(1, Session("COUNTE") + 1) = ""
        '    Session("VARN")(2, Session("COUNTE") + 1) = ""
        '    Session("VARN")(3, Session("COUNTE") + 1) = ""
        '    Session("VARN")(6, Session("COUNTE") + 1) = ""
        '    'mti Validation changes July 28, 2002
        '    Session("VARN")(4, Session("COUNTE") + 1) = ""
        '    'mti Validation changes July 28, 2002
        '    'mti Validation changes 19 August, 2002
        '    Session("VARN")(5, Session("COUNTE") + 1) = ""
        '    'mti Validation changes 19 August, 2002

        '    Session("GR1N")(Session("COUNTE") + 1) = ""
        '    Session("GR2N")(Session("COUNTE") + 1) = ""
        '    Session("GR3N")(Session("COUNTE") + 1) = ""
        '    Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE")) 'columns starts from zero
        '    Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
        '    Support.DataGridManager.SetColWidth(grd_VariableDisplay, Session("COUNTE"), Inch)
        '    Support.DataGridManager.SetText(grd_VariableDisplay, Session("VARN")(1, Session("COUNTE") + 1))
        '    'grd_VariableDisplay.Col = Session("COUNTE") - 1
        '    Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 1)
        '    Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR1N")(Session("COUNTE") + 1))
        '    Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 2)
        '    Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR2N")(Session("COUNTE") + 1))
        '    Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 3)
        '    Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR3N")(Session("COUNTE") + 1))
        '    Support.DataGridManager.manager.SetCol(grd_VariableDisplay, curr_col - 1) 'reposition the cursor
        '    'grd_VariableDisplay.ColWidth(Curr_Col) = Inch
        '    Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
        '    Label1 = ""
        '    Label2 = ""
        '    Label3 = ""
        '    'JRS 9/14/01 COMPUTATIONS
        '    dbV = DAODBEngine_definst.OpenDatabase(PutFdrvRun & "IFSVAR.MDB")
        '    dbV.Execute("DELETE * FROM [Formulas] WHERE [Code] = " & Str(Session("COUNTE") + 1))
        '    dbV.Close()
        'End If
        'JRSVBNET *** END ***
        Dim Msg As Object
        Dim curr_col As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        curr_col = Session("VarSelGridCol") + 1
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Session("VARN")(1, curr_col) & "<br>" & Session("VARN")(2, curr_col) & "<br>"
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg + Session("GR1N")(curr_col) + "-" + Session("GR2N")(curr_col) + "-" + Session("GR3N")(curr_col)
        'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg + "<br>" + Chr(10) + Chr(10) + "Delete this varaiable?"
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        If True Then 'JRSVBNET WinFormsToWeb_MessageBox.Show(Msg, MsgBoxStyle.YesNo, "Variable Selected") = MsgBoxResult.Yes Then
            DeleteVar((curr_col))
            'jrs 2005/12/19 RAM for Formulas
            ''JRS 9/14/01 COMPUTATIONS
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", "DELETE * FROM [Formulas] WHERE [Code] = " & Str(curr_col - 1))
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", "UPDATE [Formulas] SET Code = Code - 1 WHERE [Code] >= " & Str(curr_col) & " AND Code < 1000")
            ''jrs 2005/11/17
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", "DELETE * FROM [Formulas] WHERE [Code] = " & Str(curr_col + 999))
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", "UPDATE [Formulas] SET Code = Code - 1 WHERE [Code] >= " & Str(curr_col + 1000) & " AND Code < 2000")
        End If

    End Sub

    Public Sub mnuExplain_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim curr_col As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRSVBNET curr_col = Support.DataGridManager.manager.GetCol(grd_VariableDisplay) + 1
        'JRS 9/13/01 COMPUTATIONS
        'UPGRADE_ISSUE: VBControlExtender property grd_VariableDisplay.HelpContextID was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET grd_VariableDisplay.HelpContextID = Val(Session("VARN")(3, curr_col))
        'CONVERSION_ISSUE: Class System.Windows.Forms.SendKeys from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.SendKeys.Send("{F1}")

    End Sub

    '==========================frm_DisplayMenu===================
    Public Sub FillGrid()
        'jrs 2024/11/25 Dim iloop As Short
        Dim iloop As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 4 / 25 / 2
        '   Fill Grid
        '   ---------
        grd_VariableDisplay.Columns.Clear()
        grd_VariableDisplay.Visible = (Session("COUNTE") > 0)

        '      If Counte < 1 Then Counte = 1
        'JRS 02/18/04
        Dim r1 As New TableRow, r2 As New TableRow, r3 As New TableRow, r4 As New TableRow

        For iloop = 1 To Session("COUNTE")

            Dim myCol As New TMSWebControls.Column
            'myCol.Type = TMSWebControls.Column.ColumnType.Button
            myCol.WidthType = TMSWebControls.WType.Absolute
            'myCol.ButtonWidth = 100
            myCol.Width = 100
            grd_VariableDisplay.Columns.Add(myCol)

            grd_VariableDisplay(iloop - 1, 0) = Session("VARN")(1, iloop)
            grd_VariableDisplay(iloop - 1, 1) = Session("GR1N")(iloop)

            grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)

            grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)


            grd_VariableDisplay(iloop - 1, 4) = Session("GR4N")(iloop) 'jrs 2006/06/19

        Next iloop


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_VarSel.aspx")
    End Sub

    'JRS 03/10/04
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "BarChart"
                BarChart_Click(sender, e)
            Case "Bar Chart Display"
                BarChartDisplay_Click(sender, e)
            Case "Calculate Gini"
                Gini_Click(sender, e)
            Case "Graph Display"
                GraphDisplay_Click(sender, e)
            Case "Line Graph"
                LineGraph_Click(sender, e)
            Case "Map"
                Map_Click(sender, e)
            Case "WMap"
                Map2_Click(sender, e)
            Case "Currency"
                mnu_Currency_Click(sender, e)
            Case "Full Set (of Variables/Parameters)"
                mnu_Flexible_Click(sender, e)
            Case "Select Names" '
                mnu_SelectVariables_Click(sender, e)
            Case "Standard Subset (of Variables)" '
                mnu_Standard_Click(sender, e)
            Case "Combination"
                MnuComboBL_Click(sender, e)
            Case "Set Title, Display Interval, or Year"
                mnuDispCtrl_Click(sender, e)
            Case "Display/Run Horizon"
                mnuDisplayRunHorizon_Click(sender, e)
            Case "Shortest Run File Horizon"
                Session("DisplayTime") = 0
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuShort").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuLong").IconImage = "blank.gif"
            Case "Longest Run File Horizon"
                Session("DisplayTime") = 1
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuLong").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuGrpDisplay").Items("mnuHorizon").Group.Items("mnuRunFile").Group.Items("mnuShort").IconImage = "blank.gif"
            Case "Drivers"
                mnuDrivers_Click(sender, e)
            Case "Format Legend Labels"
                mnuGraphLabels_Click(sender, e)
            Case "History"
                MnuHistory_Click(sender, e)
            Case "Lorenz"
                mnuLorenz_Click(sender, e)
            Case "Bar Chart"
                mnuPEBar_Click(sender, e)
            Case "Line"
                mnuPELine_Click(sender, e)
            Case "Pie Chart"
                mnuPEPie_Click(sender, e)
            Case "Histogram Chart"     'JAM 2009-07-18
                mnuPEHistogram_Click(sender, e)
            Case "Radial Chart"     'JAM 2012-07-07
                mnuPERadial_Click(sender, e)
            Case "Stacked Bar" 'JAM 2017-08-12
                mnuPEStackedBar_Click(sender, e)
            Case "Stacked Area" 'jrs 2018/05/17
                mnuStackedArea_Click()
            Case "Scatter Plot"
                mnuPEScatter_Click(sender, e)
            Case "Validation"
                mnuValidation_Click(sender, e)
            Case "View Equations"
                mnuViewEquations_Click(sender, e)
            Case "PieChart"
                PieChart_Click(sender, e)
            Case "Pie Chart Display"
                PieChartDisplay_Click(sender, e)
            Case "ScatterPlot"
                ScatterPlot_Click(sender, e)
            Case "Define"
                mnuDefine_Click(sender, e)
            Case "Delete"
                MnuDelete_Click(sender, e)
            Case "Explain"
                mnuExplain_Click(sender, e)
            Case "Motion Chart (Using Gapminder by Google)" 'jrs 2008/08/12
                LoadMovingChart()
            Case "Use all Years (in Statistics)" 'JAM 2016-01-23
                AllYears()
                If Session("mnuUseAllYrs") Then
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = "checkbox.gif"
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = "blank.gif"
                Else
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = "blank.gif"
                End If


            Case "Use all Years and Time (in Statistics)" 'JAM 2016-01-23
                AllYearsAndTime()
                If Session("mnuUseAllYrsTime") Then
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = "checkbox.gif"
                    objSender.Groups("Analysis").Items("Use all Years (in Statistics)").IconImage = "blank.gif"
                Else
                    objSender.Groups("Analysis").Items("Use all Years and Time (in Statistics)").IconImage = "blank.gif"
                End If
        End Select
    End Sub

    'jrs 2008/08/12
    Private Sub LoadMovingChart()
        Dim Msg As String

        If Session("COUNTE") = 0 Then
            Msg = "You must select one or more variables before you create a display"
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        'jrs 2008/08/26 If Session("COUNTE") <> 2 * NR And Session("COUNTE") <> 3 * NR Then
        'If Not (Session("COUNTE") Mod 2 = 0 Or Session("COUNTE") Mod 3 = 0) Then
        '    'jrs 2008/08/26 Msg = "You must select 2 or 3 Variables for All Regions"
        '    Msg = "You must select 2 or 3 Variables for a set of Regions"
        '    lblMsg.Text = Msg
        '    lblMsg.Visible = True
        '    Exit Sub
        'End If

        'jrs 2008/10/21
        Dim Vars As New Collection
        Dim Dim1s As New Collection
        Dim i As Integer
        For i = 1 To Session("COUNTE")
            If Not Vars.Contains(Session("VARN")(1, i)) Then
                Vars.Add(i, Session("VARN")(1, i))
            End If
            If Not Dim1s.Contains(Session("GR1N")(i)) Then
                Dim1s.Add(i, Session("GR1N")(i))
            End If
        Next

        If Vars.Count * Dim1s.Count > Session("COUNTE") Or
        (Vars.Count = 1 And Dim1s.Count = Session("COUNTE")) Or
        Vars.Count > 3 Then
            'Msg = "You must select 2 or 3 Variables across the same set of Regions"
            Msg = "To display a motion chart you must select 2 or 3 Variables across the same set of Regions:  dependent, independent, and bubble sizing (optional)."
            lblMsg.Text = Msg
            lblMsg.Visible = True
            Exit Sub
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_MotionChart.aspx")
    End Sub

    Private Sub grd_VariableDisplay_ButtonClick(ByVal sender As System.Object, ByVal e As TMSWebControls.ModEventArgs) Handles grd_VariableDisplay.ButtonClick
        pnlContext.Visible = True
        lblRowCol.Text = grd_VariableDisplay(e.Column, 0) & ", " & grd_VariableDisplay(e.Column, 1)
        If grd_VariableDisplay(e.Column, 2) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 2)
        End If
        Session("VarSelGridCol") = e.Column
    End Sub

    Private Sub lbtCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtCancel.Click
        pnlContext.Visible = False
    End Sub

    Private Sub lbtDefine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtDefine.Click
        mnuDefine_Click(sender, e)
        pnlContext.Visible = False
    End Sub

    Private Sub lbtDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtDelete.Click
        MnuDelete_Click(sender, e)
        pnlContext.Visible = False
    End Sub

    Private Sub lbtExplain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mnuExplain_Click(sender, e)
        pnlContext.Visible = False
    End Sub

    Public Sub DeleteVar(ByRef curr_col As Short)
        Dim iloop As Short
        Dim ICount As Short
        Dim i2 As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 9/10/01 CREATED FOR COMPUTATIONS
        ' Delete Variable
        Session("COUNTE") = Session("COUNTE") - 1 ''Do later?
        'jrs 2006/09/25 use all hist
        Dim iLstYr As Integer
        iLstYr = Session("LastYear")
        If Session("buseAllHis") And UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            For i2 = curr_col To Session("COUNTE") + 1
                If Session("FinYearHis")(i2) > iLstYr Then
                    iLstYr = Session("FinYearHis")(i2)
                End If
            Next i2
        End If

        For i2 = 1 To iLstYr - Session("FirstYear") + 1
            For ICount = curr_col To Session("COUNTE")
                Session("PLOT")(i2, ICount) = Session("PLOT")(i2, ICount + 1)
            Next ICount
            Session("PLOT")(i2, Session("COUNTE") + 1) = 0
        Next i2

        'JRS 9/24/01 COMPUTATIONS
        Dim j1 As Object
        Dim uaf As Short
        uaf = UBound(Session("aFormulas")) - 1
        If Session("COUNTE") + 1 < uaf Then uaf = Session("COUNTE") + 1
        For j1 = curr_col + 1 To uaf
            'JRS 04/15/03
            If Not (Session("aFormulas")(j1 + 1) Is Nothing) Then
                Session("aFormulas")(j1) = Session("aFormulas")(j1 + 1)
            End If
        Next j1

        ' Take care of Grid Display & Array Variables Shifting
        For iloop = curr_col To Session("COUNTE") Step 1
            Session("FileNumbr")(iloop) = Session("FileNumbr")(iloop + 1)
            Session("VARN")(0, iloop) = Session("VARN")(0, iloop + 1)
            Session("VARN")(1, iloop) = Session("VARN")(1, iloop + 1)
            Session("VARN")(2, iloop) = Session("VARN")(2, iloop + 1)
            Session("VARN")(3, iloop) = Session("VARN")(3, iloop + 1)
            Session("VARN")(6, iloop) = Session("VARN")(6, iloop + 1)
            'mti Validation changes
            Session("VARN")(4, iloop) = Session("VARN")(4, iloop + 1)
            'mti Validation changes
            'mti Validation changes 19 August, 2002
            Session("VARN")(5, iloop) = Session("VARN")(5, iloop + 1)
            'mti Validation changes 19 August, 2002

            Session("GR1N")(iloop) = Session("GR1N")(iloop + 1)
            Session("GR2N")(iloop) = Session("GR2N")(iloop + 1)
            Session("GR3N")(iloop) = Session("GR3N")(iloop + 1)
            Session("GR4N")(iloop) = Session("GR4N")(iloop + 1)

            'jrs 10/27/04 Flexible Dimension Names for Graphs
            Session("FDNG1")(iloop) = Session("FDNG1")(iloop + 1)
            Session("FDNG2")(iloop) = Session("FDNG2")(iloop + 1)
            Session("FDNG3")(iloop) = Session("FDNG3")(iloop + 1)
            Session("FDNG4")(iloop) = Session("FDNG4")(iloop + 1)
            'jrs 2006/09/25 Use all available history
            Session("FinYear")(iloop) = Session("FinYear")(iloop + 1)
            Session("FinYearHis")(iloop) = Session("FinYearHis")(iloop + 1)

        Next iloop
        Session("VARN")(0, Session("COUNTE") + 1) = ""
        Session("VARN")(1, Session("COUNTE") + 1) = ""
        Session("VARN")(2, Session("COUNTE") + 1) = ""
        Session("VARN")(3, Session("COUNTE") + 1) = ""
        Session("VARN")(6, Session("COUNTE") + 1) = ""
        'mti Validation changes July 28, 2002
        Session("VARN")(4, Session("COUNTE") + 1) = ""
        'mti Validation changes July 28, 2002
        'mti Validation changes 19 August, 2002
        Session("VARN")(5, Session("COUNTE") + 1) = ""
        'mti Validation changes 19 August, 2002

        Session("GR1N")(Session("COUNTE") + 1) = ""
        Session("GR2N")(Session("COUNTE") + 1) = ""
        Session("GR3N")(Session("COUNTE") + 1) = ""
        Session("GR4N")(Session("COUNTE") + 1) = ""

        'jrs 10/27/04 Flexible Dimension Names for Graphs
        Session("FDNG1")(Session("COUNTE") + 1) = ""
        Session("FDNG2")(Session("COUNTE") + 1) = ""
        Session("FDNG3")(Session("COUNTE") + 1) = ""
        Session("FDNG4")(Session("COUNTE") + 1) = ""

        FillGrid()
    End Sub

    Private Sub grd_VariableDisplay_CellClick(ByVal sender As System.Object, ByVal e As TMSWebControls.ModEventArgs)
        pnlContext.Visible = True
        lblRowCol.Text = grd_VariableDisplay(e.Column, 0) & ", " & grd_VariableDisplay(e.Column, 1)
        If grd_VariableDisplay(e.Column, 2) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 2)
        End If
        Session("VarSelGridCol") = e.Column
        lblExplain.Attributes.Clear()
        lblExplain.Attributes.Add("onclick", "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT," & Session("VARN")(3, e.Column + 1) & ");")
        'jrs 09/10/04
        lblViewEquations.Attributes.Clear()
        lblViewEquations.Attributes.Add("onclick", "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT," & Session("VARN")(6, e.Column + 1) & ");")
    End Sub

    'jrs 2005/10/19
    Private Sub grd_VariableDisplay_CellClick1(ByVal sender As System.Object, ByVal e As TMSWebControls.ModEventArgs) Handles grd_VariableDisplay.CellClick

        pnlContext.Visible = True
        'jrs 2006/04/04
        lblRowCol.Visible = True
        lblRowCol.Text = grd_VariableDisplay(e.Column, 0)
        If grd_VariableDisplay(e.Column, 1) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 1)
        End If
        If grd_VariableDisplay(e.Column, 2) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 2)
        End If
        'jrs 2006/06/27
        If grd_VariableDisplay(e.Column, 3) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 3)
        End If
        Session("VarSelGridCol") = e.Column
        'jrs 2006/04/04
        lblExplain.Visible = True
        lblExplain.Attributes.Clear()
        'lbtExplain.Attributes.Add("HREF", "javascript:RH_ShowHelp(0,""" & Me.MapPathSecure("") & "\WebHelp\ifshelp.htm""" & ", HH_HELP_CONTEXT, " & Session("VARN")(3, e.Column + 1) & ")")
        'jrs 2013/09/06 lblExplain.Attributes.Add("onclick", "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT," & Session("VARN")(3, e.Column + 1) & ");")
        lblExplain.Attributes.Add("onclick", "javascript:" & LoadIFsVarHelpJS(Session("VARN")(3, e.Column + 1)))
        'lblExplain.Attributes.Add("onclick", "showExplain(" & Session("VARN")(3, e.Column + 1) & ");")
        'lbtExplain.Attributes.Add("onclick", "alert('Hello');")

        'jrs 2006/04/04
        lblViewEquations.Visible = True
        'jrs 09/10/04
        lblViewEquations.Attributes.Clear()
        'jrs 2013/09/06 lblViewEquations.Attributes.Add("onclick", "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT," & Session("VARN")(6, e.Column + 1) & ");")
        lblViewEquations.Attributes.Add("onclick", "javascript:" & LoadIFsVarHelpEqnJS(Session("VARN")(6, e.Column + 1)))
        'jrs 2004/07/06 lblViewEquations.Attributes.Add("onclick", "showEquation(" & Session("VARN")(6, e.Column + 1) & ");")

    End Sub


    Private Sub lbtViewCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtViewCode.Click
        'jrs 2006/04/03
        Dim valtbl As DataTable
        Dim sql As String
        Dim CurVar As String = Session("VARN")(1, Session("VarSelGridCol") + 1)
        Dim pos As Integer = InStr(CurVar, "[")
        If pos > 0 Then
            CurVar = Mid(CurVar, 1, pos - 1)
        End If
        sql = "SELECT * FROM IFsVar WHERE Name = '" & CurVar & "'"
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", sql, "Name", valtbl)

        Dim sCode As String
        Dim sDef As String = VDef(IfsVars.Item(CurVar).Index)

        sCode = CurVar + "<br>" + VB.StrDup(Len(CurVar) + 10, "-") + "<br>" +
        sDef + "<br>" + VB.StrDup(Len(sDef) + 20, "-") + "<br>"

        Dim iRow As Integer
        Dim svbCode As String

        With valtbl

            iRow = 0

            svbCode = SeekValue(valtbl, CurVar, "vbcode")
            If svbCode > "" Then
                svbCode = Replace(svbCode, VB.Chr(13), "<br>")
                sCode = sCode + "<br>" + svbCode
            Else
                If UCase(Mid(CurVar, 1, 1)) = "S" Then
                    iRow = 0
                    svbCode = SeekValue(valtbl, Mid(CurVar, 2), "vbcode")
                    If svbCode > "" Then
                        svbCode = Replace(svbCode, VB.Chr(13), "<br>")
                        sCode = sCode + "<br>" + svbCode
                    End If
                End If
                'Driver Fix - November 2, 2003
            End If
        End With

        pnlContext.Visible = False

        'Display Msg Form
        LoadMessage(sCode, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    Protected Sub lbtDrivers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtDrivers.Click
        Session("DriverParent") = "frm_Varsel.aspx"
        pnlContext.Visible = False
        mnuDrivers_Click(sender, e)
    End Sub

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

    'JAM 2016-01-23 Adding All Years for Statistics
    Protected Sub AllYears()
        If Session("mnuUseAllYrs") Then
            Session("mnuUseAllYrs") = False
        Else
            Session("mnuUseAllYrs") = True
        End If
        Session("mnuUseAllYrsTime") = False


    End Sub
    'JAM 2016-01-23 Adding All Years and Time for Statistics
    Protected Sub AllYearsAndTime()
        If Session("mnuUseAllYrsTime") Then
            Session("mnuUseAllYrsTime") = False
        Else
            Session("mnuUseAllYrsTime") = True
        End If


        Session("mnuUseAllYrs") = False

    End Sub
End Class

