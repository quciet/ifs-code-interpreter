Option Strict Off
Option Explicit On
Imports Microsoft.VisualStudio.OLE.Interop

Partial Class frm_ChangeValues
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
                'mti August 2004 2 Talk to Jose- I do not need it
                If Not Session("Changing_Custom_Scenario") Then
                    Support.FormManager.StateRestore(Me)
                End If
                Support.FormManager.LoadState = False
            End If
            Dim NewLargeChange As Short
            Dim finam As String
            Dim VARNUM As Object
            Dim ParType As Short
            'mti August 2004 - 2 start
            Dim j As Short 'Object
            'mti August 2004 - 2 start
            Dim rr As Short
            Dim cc As Short
            Dim TotalColWidth As Object
            Dim ColWidth As Short
            Dim RowHeight As Object
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            'WindowState = System.Windows.Forms.FormWindowState.Normal
            'Width = VB6.TwipsToPixelsX(Inch * 6.5)
            'Height = VB6.TwipsToPixelsY(Inch * 5)
            'Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2)
            'Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)

            ChangesMade = CStr(False)
            Session("ChangesMade") = ChangesMade
            but_CancelAll.Visible = False
            but_another.Text = "Select A Different Parameter"
            cmd_Exit2.Text = "Exit"
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '   Grid Setup
            '   ----------
            'Scenario Change MTI 18 May 2001
            If Not Session("Changing_Custom_Scenario") Then
                RowHeight = 230
                ColWidth = HalfInch
                TotalColWidth = ColWidth * 5
                'grd_VariableDisplay.Rows = 3
                'grd_VariableDisplay.cols = MaxVariables
                'grd_VariableDisplay.FixedCols = 0
                'grd_VariableDisplay.FixedRows = 0
                'grd_VariableDisplay.Visible = True
                'For cc = 0 To grd_VariableDisplay.cols - 1
                '            For rr = 0 To grd_VariableDisplay.Rows - 1
                '           grd_VariableDisplay.col = cc
                '          grd_VariableDisplay.row = rr
                '         grd_VariableDisplay.CtlText = ""
                '        Next rr
                '		Next cc
                Call Gridput()
            Else 'Change Caption if came from scenario-custom                    
                'mti August 2004 - 2 start
                grd_VariableDisplay.Visible = False

                j = Session("Scenario_PlotCol")
                'mti August 2004 - 2 end
                'j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " " & VScenario(Session("SEL")) &
                    "   " & Session("tGR1N")(j) & "   " & Session("tGR2N")(j) & "   " & Session("tGR3N")(j)

                'backup Session("tPLOT") vectors
                ReDim Session("PlotBack")(Session("LastYear") - Session("FirstYear") + 1)
                ReDim Session("PlotRIBack")(Session("LastYear") - Session("FirstYear") + 1)

                'For i2 = 1 To frm_MainMenu.DefInstance.IFsGUIctrl1.cLastYear - frm_MainMenu.DefInstance.IFsGUIctrl1.cFirstYear + 1
                For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
                    'PlotBack(i2) = Session("tPLOT")(i2, j)
                    Session("PlotBack")(i2) = Session("tPLOT")(i2, j)
                    'PlotRIBack(i2) = PlotRI(i2, j)
                    Session("PlotRIBack")(i2) = Session("PlotRI")(i2, j)
                Next i2

            End If
            '-------------------

            '   Set up Screen
            '   -------------

            'frm_Info.Visible = True
            lbl_year.Visible = True
            txt_year.Visible = True
            lbl_value.Visible = True
            txt_value.Visible = True
            lbl_min.Visible = True
            txt_min.Visible = True
            lbl_max.Visible = True
            txt_max.Visible = True

            'jrs 2024/08/19 txt_year.Enabled = False
            txt_value.Enabled = False
            txt_min.Enabled = False
            txt_max.Enabled = False

            'mti August 2004 2
            'txt_sliderval.Enabled = False

            'frm_prepare.Visible = True
            lbl_desired.Visible = True
            txt_desired.Visible = True
            lbl_repeat.Visible = False
            txt_repeat.Visible = False

            'Scenario Change MTI 18 May 2001
            If Session("Changing_Custom_Scenario") Then
                but_another.Visible = False
                cmd_Exit2.Text = "Exit to Scenario Tree"
            End If

            cmd_Exit2.Visible = True

            ParType = VType(Session("SEL"))
            but_ChangeRepeat.Visible = True
            'but_CancelAll.Visible = True
            If ParType = 3 Then '3 = Time Series; 1 = Internal, Computed & 2 = Initial Cond
                txt_repeat.Visible = True
                lbl_repeat.Visible = True
                but_Interpolate.Visible = True
                but_NextYear.Visible = True
                but_PreviousYear.Visible = True
                lbl_FirstOnly.Visible = False
                'frm_prepare.Text = "Change Numerically (Year-by-Year Possible)" 
                'frm_Slider.Text = "Change with Slider (All Years)" 
                but_ChangeRepeat.Text = "Change/Repeat"
            Else '2= Init Cond; Internal, Computed variables screened out earlier
                lbl_repeat.Visible = False
                txt_repeat.Visible = False
                but_Interpolate.Visible = False
                but_NextYear.Visible = False
                but_PreviousYear.Visible = False
                lblJS.Visible = False
                lbl_FirstOnly.Visible = True
                'frm_prepare.Text = "Change Numerically" 
                'frm_Slider.Text = "Change with Slider" 
                but_ChangeRepeat.Text = "Change"
            End If

            '   Set First Year and Value
            '   ------------------------

            'CKD 3/18/04 remove DefInstance
            txt_year.Text = Str(Session("FirstYear"))
            CurYear = Session("FirstYear")
            Session("CurYear") = CurYear
            'Scenario Change MTI 18 May 2001
            If Not Session("Changing_Custom_Scenario") Then
                'jrs 2006/06/02 changing logic for groups
                finam$ = Session("PutFdrvRun") + "working.run.db"
                ClearPlot(Session("tCOUNTE"), Session("tPLOT"), Session("tVARN"), Session("tGR1N"), Session("tGR2N"),
                    Session("tGR3N"), Session("tGR4N"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"),
                    Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
                    Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                gLoadForDisplayNET(V(Session("SEL")), Session("SEL"), Session("frm_change.sDim_1"), -1, Session("frm_change.sDim_2"), -1, Session("frm_change.sDim_3"), -1,
                    finam$, 0, True, True, Session("frm_change.FlagGroup"), 1, Session("tPLOT"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                'gLoadForDisplay(V(Session("SEL")), Session("SEL"), Session("frm_change.sDim_1"), -1, Session("frm_change.sDim_2"), -1, Session("frm_change.sDim_3"), -1,
                'finam$, 0, True, True, Session("frm_change.FlagGroup"), 1, Session("tPLOT"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                Session("VarVal") = Session("tPLOT")(1, 1)
                CurValue = Session("VarVal")
                Session("CurValue") = CurValue
            Else

                'CKD 3/24/04 CurNodeNo is not resolved, commenting out
                'j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
                'take from Session("tPLOT") vector instead of file                    
                'mti August 2004 - 2 start
                j = Session("Scenario_PlotCol")
                'mti August 2004 - 2 end
                Session("VarVal") = Session("tPLOT")(1, j)

                CurValue = Session("VarVal")
                Session("CurValue") = CurValue
            End If
            '----------------
            txt_value.Text = Str(Session("VarVal"))
            txt_desired.Text = Str(Session("VarVal"))
            txt_repeat.Text = "1"

            'jrs 2008/06/03 Currency
            Dim currFct As Single
            currFct = 1
            If VCurrency(CInt(Session("SEL"))) Then
                currFct = Session("CurrencyVal")
            End If

            txt_max.Text = Math.Round(VMax(Session("SEL")) / currFct, 4)
            txt_min.Text = Math.Round(VMin(Session("SEL")) / currFct, 4)

            '   Set Slider Values
            '   -----------------

            If VMax(Session("SEL")) / currFct < 100 Then
                ValMult = 100
            Else
                ValMult = 1
            End If
            'Valid Min and Max properties range from -32,768 to 32,767
            'Difference between Min and Max cannot exceed -32,768
            If VMax(Session("SEL")) / currFct > 16000 Then
                ValMult = 16000 / (VMax(Session("SEL")) / currFct)
            End If
            If VMin(Session("SEL")) / currFct < -16000 Then
                ValMult = Amin(ValMult, -16000 / (VMin(Session("SEL")) / currFct))
            End If

            '   Fill Session("tPLOT") so that can reset if decide to cancel
            '   -----------------------------------------------

            '   Place Values into Session("tPLOT") so that can display
            '   ------------------------------------------

            'Scenario Change MTI 18 May 2001
            cmd_Exit2.Enabled = True
            '---------------------
            'JRS 07/07/03 Grouping using AVG
            If Not Session("Changing_Custom_Scenario") Then

                'CKD 3/24/04 frm_change is not upgraded comment out
                If Session("ChangecmdUseGroups") = "Use Countries" Then
                    'CKD 3/18/04 remove DefInstance
                    CurValue = Session("tPLOT")(1, 1)
                    Session("CurValue") = CurValue
                    Session("VarVal") = CurValue
                    txt_desired.Text = Str(CurValue)
                End If

            End If

            If Session("VarVal") * ValMult < -0.5 Then ValMult = -0.5 / Session("VarVal")
            'It appears that cannot set .Value < -.5 without illegal property value

            'CKD 4/4/04 comment out slider
            'hsc_Slider1.Value = Session("VarVal") * ValMult

            txt_sliderval.Text = Str(Session("VarVal"))

            'CKD 4/4/04 Comment out Slider
            'hsc_Slider1.Maximum = (VMax(Session("SEL")) * ValMult + hsc_Slider1.LargeChange - 1)


            'CKD 4/4/04 comment out Slider section start
            'hsc_Slider1.Minimum = VMin(Session("SEL")) * ValMult
            'If (hsc_Slider1.Maximum - hsc_Slider1.LargeChange + 1) - hsc_Slider1.Minimum > 1000 Then
            '    NewLargeChange = 100
            '    hsc_Slider1.Maximum = hsc_Slider1.Maximum + NewLargeChange - hsc_Slider1.LargeChange
            '    hsc_Slider1.LargeChange = NewLargeChange
            '    hsc_Slider1.SmallChange = 1
            'ElseIf (hsc_Slider1.Maximum - hsc_Slider1.LargeChange + 1) - hsc_Slider1.Minimum > 100 Then
            '    NewLargeChange = 10
            '    hsc_Slider1.Maximum = hsc_Slider1.Maximum + NewLargeChange - hsc_Slider1.LargeChange
            '    hsc_Slider1.LargeChange = NewLargeChange
            '    hsc_Slider1.SmallChange = 1
            'Else
            '    NewLargeChange = 1
            '    hsc_Slider1.Maximum = hsc_Slider1.Maximum + NewLargeChange - hsc_Slider1.LargeChange
            '    hsc_Slider1.LargeChange = NewLargeChange
            'End If
            'CKD 4/4/04 section end

            'CKD 3/18/04 remove DefInstance
            txt_year.Text = Str(Session("FirstYear"))
            'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            txt_value.Text = Str(CurValue)

            'mti webform June 2004 ChangeVal
            'Call GraphUpdate()
            'lblJS.PeFunction.ReinitializeResetImage()
            'lblJS.Visible = True



            'is this needed?
            'Scenario Change MTI 18 May 2001
            'If Not Session("Changing_Custom_Scenario") Then
            '    frm_ChangeValues.Show
            'End If

            'took txt_desired.setfocus to form.activate

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.

            'CKD 3/24/04 comment out any windows forms reference
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'jrs 09/22/04
            Session("NameDispParent") = "frm_ChangeValues.aspx"


        Else
            ChangesMade = Session("ChangesMade")
            CurYear = Session("CurYear")
            CurValue = Session("CurValue")

        End If
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
    'CONVERSION_ISSUE: Class 'System.Windows.Forms.HScrollBar' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.

    'CKD 3/24/04 comment out any windows forms reference
    'Protected WithEvents hsc_Slider1 As System.Windows.Forms.HScrollBar
    'CONVERSION_NOTE: Class 'AxGraphLib.AxGraph' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents lblJS As AxGraphLib.AxGraph 
    'CONVERSION_NOTE: Class 'AxMSGrid.AxGrid' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents grd_VariableDisplay As AxMSGrid.AxGrid 
    Public mnuDefine As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDash As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuExplain As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPop As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'CONVERSION_NOTE: Following code was added for support purposes.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1016.html'.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_ChangeValuesPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_ChangeValues
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_ChangeValues()
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
    '------------------------------------------ChanVals.Frm-------------------------------------
    '------------------------------------------------------------------------------------------
    '

    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last revision by BBH 10/21/01

    '   This form is used in slightly different ways by three processes:

    '   1.  Normal changing of values after selection of parameters to change.
    '       A filled Session("tPLOT") matrix comes in from Change; Session("VL") carries pointers.
    '       All operations of change (single values, repeats, interpolations)
    '       affect working.run, but can be reversed of values in Session("tPLOT").
    '       Upon exit, Session("tPLOT") is filled with updated values from working.run.
    '   2.  From Sensitivity Analysis.   Again a filled Session("tPLOT") vector comes in
    '       from Change and all operations affect working.run.
    '       Upon exit, changed values are written out from working.run to
    '       SensitivityInput.dat (but are not appended, so could be lost).
    '       The Session("tPLOT") matrix is used to refill working.run so all changes are reversed.
    '   3.  From Scenario Analysis.  Again a filled Session("tPLOT") vector comes in,
    '       but from CreateScenario, not Change.  There are no Session("VL") pointers, and
    '       the node values are used to identify the elements in Session("tPLOT") that should
    '       be changed.  No values are changed within working.run and there is no
    '       need to reverse such changes upon Unload.

    'Scenario Change MTI 18 May 2001

    'mti Auust 2004 2
    'Dim PlotBack() As Object
    'Dim PlotRIBack() As Object
    'mti Auust 2004 2
    '---------------

    Dim GMAX1, i2, DimNum As Object
    Dim eleloop As Short
    Dim SubDimDone, Code, SEL1 As String
    Dim SDimPoint, DimPoint, SubDimNum, SDimNum, CurYear As Short
    Dim ValMax, CurValue, ValMin As Object
    Dim Number As Single
    Dim SaveVCat, Carret, ValMult As Object
    Dim Cat_HelpId() As Object
    Dim Itemhelp(200) As Object
    Dim ChangesMade As String
    'JRS 07/11/03
    Dim RatioChange As Single


    '   VARVAL, Session("RECORD"), session("tCOUNTE"), and VARNUM are in Global Declarations

    '=============================ChanVals.Frm ===========================
    Private Sub but_another_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles but_another.Click
        Dim rr As Short
        Dim cc As Short
        Dim TotalColWidth As Object
        Dim ColWidth As Short
        Dim RowHeight As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '    If ChangesMade$ = False Then
        'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        RowHeight = 230
        ColWidth = Inch
        'UPGRADE_WARNING: Couldn't resolve default property of object TotalColWidth. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TotalColWidth = ColWidth * 5

        'CK 4/4/04 comment out GRID section start
        'grd_VariableDisplay.Rows = 3
        'grd_VariableDisplay.cols = MaxVariables
        'grd_VariableDisplay.FixedCols = 0
        'grd_VariableDisplay.FixedRows = 0
        'grd_VariableDisplay.Visible = True
        'For cc = 0 To grd_VariableDisplay.cols - 1
        '    For rr = 0 To grd_VariableDisplay.Rows - 1
        '        grd_VariableDisplay.col = cc
        '        grd_VariableDisplay.row = rr
        '        grd_VariableDisplay.CtlText = ""
        '    Next rr
        'Next cc
        'CKD 4/4/04 GRID section end

        '    End If

        'jrs 2005/04/29
        frm_ChangeValues_Closed(eventSender, eventArgs)

        '   Initialize counting of variables selected
        '   -----------------------------------------

        Session("tCOUNTE") = 0
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'CKD 3/18/04 remove DefInstance
        Support.FormManager.Close(Me)



        '   Grid Setup
        '   ----------


        '   Return to Parameter Selection for Change
        '   -----------------------------------------

        'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        WinFormsToWeb_FocusControl.Focus("cmb_VarName")
        'UPGRADE_WARNING: Couldn't resolve default property of object LongFileOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("LongFileOn") = True Then
            'CKD 3/24/04 change has not been updated yet
            'frm_change.DefInstance.cmb_VCAT.SelectedIndex = 0
        Else
            'CKD 3/24/04 change not updated yet
            'frm_change.DefInstance.cmb_VCAT.SelectedIndex = 1
        End If


    End Sub



    '=============================ChanVals.Frm ===========================
    Private Sub but_CancelAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles but_CancelAll.Click
        Dim VARNUM As Object

        'mti August 2004 - 2 
        Dim n2p As Short 'Object
        Dim j As Short 'Object

        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CKD 3/24/04 comment out any windows forms reference
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        txt_year.Visible = True
        cmd_Exit2.Visible = True
        cmd_Exit2.Enabled = True
        txt_value.Text = "Please Wait"

        '   Use Session("tPLOT") to reset
        '   -----------------
        'Scenario Change MTI 18 May 2001
        If Not Session("Changing_Custom_Scenario") Then
            finam = Session("PutFdrvRun") & "working.run.db"
            'jrs 2006/06/02 centralization
            ClearPlot(Session("COUNTE"), Session("tPLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"),
                Session("GR4N"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"),
                Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            gLoadForDisplayNET(V(Session("SEL")), Session("SEL"), Session("frm_change.sDim_1"), -1, Session("frm_change.sDim_2"), -1, Session("frm_change.sDim_3"), -1,
                finam$, 0, True, True, Session("frm_change.FlagGroup"), 1, Session("tPLOT"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            'gLoadForDisplay(V(Session("SEL")), Session("SEL"), Session("frm_change.sDim_1"), -1, Session("frm_change.sDim_2"), -1, Session("frm_change.sDim_3"), -1,
            'finam$, 0, True, True, Session("frm_change.FlagGroup"), 1, Session("tPLOT"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, False,
            'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            'Session("IFs_MDB2Active"), Session("CurrencyVal"))

        Else
            'CKD 3/24/04 CurNodeNo not updated 

            'n2p = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            n2p = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
            'CKD 3/18/04 remove DefInstance
            For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CKD 3/18/04 remove DefInstance
                txt_year.Text = Str(Session("FirstYear") + i2)
                'Session("RECORD") = (Session("VL")(j) + ((i2 - 1) * filen)) * 4 - 3
                'VARNUM = j
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object PlotBack(i2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'mti August 2004 2
                Number = Session("PlotBack")(i2)
                Session("tPLOT")(i2, n2p) = Session("PlotBack")(i2)
                Session("PlotRI")(i2, n2p) = Session("PlotRIBack")(i2)
            Next i2
        End If


        'CKD 3/18/04 remove DefInstance
        CurYear = Session("FirstYear")
        Session("CurYear") = CurYear
        txt_year.Text = Str(CurYear)

        If Not Session("Changing_Custom_Scenario") Then
            Session("VarVal") = Session("tPLOT")(1, 1)
            CurValue = Session("VarVal")
            Session("CurValue") = CurValue
        Else

            'CKD 3/24/04 CurNodeNo not updated yet
            'j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            j = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
            'take from Session("tPLOT") vector instead of file
            'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("tPLOT")(1, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VarVal") = Session("tPLOT")(1, j)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CurValue = Session("VarVal")
            Session("CurValue") = CurValue
        End If


        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        txt_value.Text = Str(Session("VarVal"))
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        txt_desired.Text = Str(Session("VarVal"))
        txt_repeat.Text = "1"
        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

        'CKD 4/4/04 comment out slider
        'hsc_Slider1.Value = Session("VarVal") * ValMult

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        txt_sliderval.Text = Str(Session("VarVal"))

        ChangesMade = CStr(False)
        Session("ChangesMade") = ChangesMade
        but_CancelAll.Visible = False
        but_another.Text = "Select Another Parameter"
        cmd_Exit2.Text = "Exit"
        If Session("Changing_Custom_Scenario") Then
            cmd_Exit2.Text = "Exit to Scenario Tree"
        End If

        'mti webform June 2004 ChangeVal
        'Call GraphUpdate()
        'lblJS.PeFunction.ReinitializeResetImage()
        lblJS.Visible = True
        'CKD 3/24/04 remove any windows forms reference
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub but_Change_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles but_Change.Click

        'mti August 2004 - 2 
        Dim j As Short 'Object
        Dim n2p As Short 'Object

        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CKD 3/24/04 remove windows forms reference
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Record slider value for all years
        '   ---------------------------------

        cmd_Exit2.Enabled = True

        'jrs 2005/02/16
        Dim fNum As Integer
        fNum = FreeFile()
        'Scenario Change MTI 18 May 2001
        If Session("Changing_Custom_Scenario") Then
            'CKD 3/24/04 frm_scenariocreate not updated
            'n2p = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            n2p = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
        End If

        'Scenario Change MTI 18 May 2001
        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

        'mti August 2004 - 2 - start
        'CKD 4/4/04 comment out slider
        Number = Val(txt_sliderval.Text) 'Val(CStr(hsc_Slider1.Value / ValMult))

        'CKD comment out slider
        txt_value.Text = txt_sliderval.Text 'Str(hsc_Slider1.Value / ValMult)
        'mti August 2004 - 2 end

        'jrs 2006/06/01 this is not considering groups
        'for groups, we need to check if the agg = SUM, then use factor = newvalue/basevalue, or newvalue/groupmemberscount if basevalue=0
        'if agg <> SUM, then use factor = newvalue - basevalue

        'JRS 07/11/03 BEGIN
        'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If CurValue <> 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RatioChange = Val(txt_value.Text) / CurValue
        Else
            RatioChange = 1
        End If
        'JRS 07/11/03 END

        'CKD 3/24/04 remove DefInstance
        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
            If Not Session("Changing_Custom_Scenario") Then
                For j = 1 To grd_VariableDisplay.Columns.Count
                    Session("tPLOT")(i2, j) = Number
                    If j = 1 Then CurValue = Number
                    Session("CurValue") = CurValue
                Next j
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object n2p. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("tPLOT")(i2, n2p). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("tPLOT")(i2, n2p) = Number
                'UPGRADE_WARNING: Couldn't resolve default property of object n2p. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PlotRI")(i2, n2p). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("PlotRI")(i2, n2p) = "REPEAT"
                'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                CurValue = Number
                Session("CurValue") = CurValue
            End If
        Next i2
        'JRS 07/10/03
        'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        CurValue = Val(txt_value.Text)
        Session("CurValue") = CurValue


        ChangesMade = CStr(True)
        Session("ChangesMade") = ChangesMade
        but_CancelAll.Visible = True
        but_another.Text = "Save/Select Another Parameter"
        cmd_Exit2.Text = "Save and Exit"
        If Session("Changing_Custom_Scenario") Then
            cmd_Exit2.Text = "Exit to Scenario Tree"
        End If

        'mti webform June 2004 ChangeVal
        'Call GraphUpdate()
        'lblJS.PeFunction.ReinitializeResetImage()
        lblJS.Visible = True
        'CKD 3/24/04 remove windows forms reference
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub but_ChangeRepeat_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles but_ChangeRepeat.Click

        'mti August 2004 - 2 
        Dim n2p As Short 'Object
        Dim j As Short 'Object

        Dim finam As String
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CKD 3/24/04 remove windows forms reference
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Check against maximum and minimum
        '   ---------------------------------

        If Val(txt_desired.Text) > Val(txt_max.Text) Then
            Msg = "Warning:  Desired value is greater than maximum allowed.  "
            Msg = Msg & "The maximum is being imposed."
            txt_desired.Text = txt_max.Text

            'jrs 2023/09/21
            'LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('" & Msg & "');</script>")

        ElseIf Val(txt_desired.Text) < Val(txt_min.Text) Then
            Msg = "Warning:  Desired value is lower than minimum allowed.  "
            Msg = Msg & "The minimum is being imposed."
            txt_desired.Text = txt_min.Text

            'jrs 2023/09/21
            'LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('" & Msg & "');</script>")

        End If

        '   Reset Slider Values
        '   ----------------------------

        'CKD 4/4/04 comment out slider
        'hsc_Slider1.Value = Val(txt_desired.Text) * ValMult
        txt_sliderval.Text = txt_desired.Text

        'jrs 2006/06/01 this is not considering groups
        'for groups, we need to check if the agg = SUM, then use factor = newvalue/basevalue, or newvalue/groupmemberscount if basevalue=0
        'if agg <> SUM, then use factor = newvalue - basevalue

        'JRS 10/05/03 BEGIN
        'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        RatioChange = Val(txt_desired.Text) - CurValue
        'JRS 10/05/03 END

        'jrs 2005/02/16
        Dim fNum As Integer

        '   Record existing/edited value
        '   ----------------------------
        'Scenario Change MTI 18 May 2001
        Number = Val(txt_desired.Text)
        txt_value.Text = txt_desired.Text
        If Not Session("Changing_Custom_Scenario") Then
            Session("tPLOT")(CurYear - Session("FirstYear") + 1, 1) = Val(txt_desired.Text)
        Else
            'CKD 3/24/04 frm_scenariocreate not updated
            'j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            j = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
            'CKD 3/24/04 remove DefInstance
            Session("tPLOT")(CurYear - Session("FirstYear") + 1, j) = Number
            'CKD 3/24/04 remove DefInstance
            Session("PlotRI")(CurYear - Session("FirstYear") + 1, j) = "REPEAT"
        End If
        '------------------


        cmd_Exit2.Enabled = True

        If Val(txt_repeat.Text) > 1 Then
            'Scenario Change MTI 18 May 2001
            If Session("Changing_Custom_Scenario") Then
                'CKD 3/24/04 frm_scenarioCreate not updated
                'n2p = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
                'mti August 2004 - 2 start
                n2p = Session("Scenario_PlotCol")
                'mti August 2004 - 2 end
            End If
            '----------------

            'Scenario Change MTI 18 May 2001
            Number = Val(txt_desired.Text)
            'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CurValue = Number
            Session("CurValue") = CurValue
            txt_value.Text = txt_desired.Text
            '----------------

            For i2 = 1 To Val(txt_repeat.Text) - 1
                'CKD 3/24/04 remove DefInstance
                If CurYear >= Session("LastYear") Then
                    Exit For
                Else
                    CurYear = CurYear + 1
                    Session("CurYear") = CurYear
                    txt_year.Text = Str(CurYear)
                    'JRS 10/05/03 BEGIN
                    'CKD 4/4/04 comment out PARAMETER
                    'lblJS.ThisPoint = CurYear - Session("FirstYear") + 1
                    'RatioChange = Val(txt_desired.Text) - lblJS.GraphData

                    'JRS 10/05/03 END
                End If

                'Scenario Change MTI 18 May 2001
                If Not Session("Changing_Custom_Scenario") Then
                    For j = 1 To grd_VariableDisplay.Columns.Count
                        Session("tPLOT")(CurYear - Session("FirstYear") + 1, j) = Number!
                    Next j
                Else
                    'CKD 3/24/04 remove DefInstance
                    Session("tPLOT")(CurYear - Session("FirstYear") + 1, n2p) = Number
                    'CKD 3/24/04 remove Definstance
                    Session("PlotRI")(CurYear - Session("FirstYear") + 1, n2p) = "REPEAT"
                End If
            Next i2
            '------'Scenario Change MTI 18 May 2001
        End If


        ChangesMade = CStr(True)
        Session("ChangesMade") = ChangesMade
        but_CancelAll.Visible = True
        but_another.Text = "Save/Select Another Parameter"
        cmd_Exit2.Text = "Save and Exit"
        If Session("Changing_Custom_Scenario") Then
            cmd_Exit2.Text = "Exit to Scenario Tree"
        End If

        'mti webform June 2004 ChangeVal
        'Call GraphUpdate()
        'lblJS.PeFunction.ReinitializeResetImage()
        'lblJS.Visible = True
        'CKD 3/24/04 remove windows forms reference
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'jrs 2014/07/18
        GraphUpdate()
    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub but_Interpolate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles but_Interpolate.Click
        Dim VARNUM As Object

        'mti August 2004 - 2 
        Dim n2p As Short 'Object

        Dim IntTime As Single
        Dim VarTar As Single
        Dim Msg As String
        Dim VarBase As Single

        'mti August 2004 - 2 
        Dim j As Short 'Object

        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'jrs 2006/06/01 this is not considering groups
        'for groups, we need to check if the agg = SUM, then use factor = newvalue/basevalue, or newvalue/groupmemberscount if basevalue=0
        'if agg <> SUM, then use factor = newvalue - basevalue

        'CKD 3/24/04 remove windows forms reference
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'JRS 10/05/03 BEGIN
        'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        RatioChange = Val(txt_desired.Text) - CurValue
        Dim DeltaRatioChange As Single
        DeltaRatioChange = 0
        'JRS 10/05/03 END

        '   Record existing/edited value
        '   ----------------------------
        'jrs 2005/02/16
        Dim fNum As Integer
        'Scenario Change MTI 18 May 2001
        Number = Val(txt_value.Text)
        If Session("Changing_Custom_Scenario") Then
            'CKD 3/24/04 frm_scenarioCreate not updated
            'j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            j = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
            'CKD 3/24/04 remove DefInstance
            Session("tPLOT")(CurYear - Session("FirstYear") + 1, j) = Number
            'CKD 3/24/04 remove DefInstance
            Session("PlotRI")(CurYear - Session("FirstYear") + 1, j) = "INTERPOLATE"
        End If


        '   Set interpolation parameters and impose limits
        '   ----------------------------------------------

        VarBase = Val(txt_value.Text)
        If Val(txt_desired.Text) > Val(txt_max.Text) Then
            Msg = "Warning:  Desired value is greater than maximum allowed.  "
            Msg = Msg & "The maximum is being imposed."
            txt_desired.Text = txt_max.Text

            'jrs 2023/09/21
            'LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('" & Msg & "');</script>")

        ElseIf Val(txt_desired.Text) < Val(txt_min.Text) Then
            Msg = "Warning:  Desired value is lower than minimum allowed.  "
            Msg = Msg & "The minimum is being imposed."
            txt_desired.Text = txt_min.Text

            'jrs 2023/09/21
            'LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('" & Msg & "');</script>")

        End If
        VarTar = Val(txt_desired.Text)
        IntTime = Val(txt_repeat.Text)

        '   Interpolate
        '   -----------

        cmd_Exit2.Enabled = True

        If Session("Changing_Custom_Scenario") Then
            'CKD 3/24/04 frm_scenarioCreate not updated
            'n2p = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            n2p = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
        End If
        For i2 = 1 To Val(txt_repeat.Text)
            'CKD 3/24/04 remove DefInstance
            If CurYear >= Session("LastYear") Then
                Exit For
            Else
                CurYear = CurYear + 1
                Session("CurYear") = CurYear
                txt_year.Text = Str(CurYear)
            End If
            If Not Session("Changing_Custom_Scenario") Then
                For j = 1 To grd_VariableDisplay.Columns.Count
                    VARNUM = j
                    Number = VarBase + (VarTar - VarBase) * (i2 / IntTime)
                    'jrs 2006/06/02 change group logic Put #1, RECORD, Number!
                    Session("tPLOT")(CurYear - Session("FirstYear") + 1, j) = Number
                    If j = 1 Then CurValue = Number
                    Session("CurValue") = CurValue
                Next j
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Number = VarBase + (VarTar - VarBase) * (i2 / IntTime)
                'CKD 3/24/04 remove DefInstance
                Session("tPLOT")(CurYear - Session("FirstYear") + 1, n2p) = Number
                'CKD 3/24/04 remove DefInstance
                Session("PlotRI")(CurYear - Session("FirstYear") + 1, n2p) = "INTERPOLATE"
                'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                CurValue = Number
                Session("CurValue") = CurValue
            End If
        Next i2
        'JRS 07/16/03 txt_value = Str$(Number!)
        txt_value.Text = txt_desired.Text
        '   Reset Slider Values
        '   ----------------------------

        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

        'CKD 4/4/04 comment out slider
        'hsc_Slider1.Value = Val(txt_value.Text) * ValMult
        txt_sliderval.Text = txt_value.Text

        ChangesMade = CStr(True)
        Session("ChangesMade") = ChangesMade
        but_CancelAll.Visible = True
        but_another.Text = "Save/Select Another Parameter"
        cmd_Exit2.Text = "Save and Exit"
        If Session("Changing_Custom_Scenario") Then
            cmd_Exit2.Text = "Exit to Scenario Tree"
        End If

        'JRS 07/10/03
        'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        CurValue = Val(txt_desired.Text)
        Session("CurValue") = CurValue

        'mti webform June 2004 ChangeVal
        'Call GraphUpdate()
        'lblJS.PeFunction.ReinitializeResetImage()
        lblJS.Visible = True
        'CKD 3/24/04 remove windows forms reference
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'jrs 2014/07/18
        GraphUpdate()

    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub but_NextYear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles but_NextYear.Click
        Dim VARNUM As Object

        'mti August 2004 - 2 
        Dim j As Short 'Object

        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Record existing/edited value
        '   ----------------------------

        'Scenario Change MTI 18 May 2001
        'jrs 2005/02/16
        Dim fNum As Integer
        'this part might be redundant for scenario
        Number = Val(txt_value.Text)
        If Session("Changing_Custom_Scenario") Then
            'CKD 3/24/04 frm_scenarioCreate not updated
            'j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            j = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
            'CKD 3/24/04 remove DefInstance
            Session("tPLOT")(CurYear - Session("FirstYear") + 1, j) = Number
            'no need to change plotri() at this point
        End If

        '   Advance Year and Value
        '   ----------------------

        CurYear = CurYear + 1
        Session("CurYear") = CurYear
        'CKD 3/24/04 remove DefInstance
        If CurYear > Session("LastYear") Then CurYear = Session("LastYear")
        txt_year.Text = Str(CurYear)

        'Scenario Change MTI 18 May 2001
        If Not Session("Changing_Custom_Scenario") Then
            Session("VarVal") = Session("tPLOT")(CurYear - Session("FirstYear") + 1, 1)
            CurValue = Session("VarVal")
            txt_value.Text = Str(Session("VarVal"))
            Session("CurValue") = CurValue
        Else
            'CKD 3/24/04 frm_scenarioCreate nit updated
            'j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            j = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
            'CKD 3/24/04 remove DefInstance
            Session("VarVal") = Session("tPLOT")(CurYear - Session("FirstYear") + 1, j)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CurValue = Session("VarVal")
            Session("CurValue") = CurValue
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            txt_value.Text = Str(Session("VarVal"))
        End If
        '-----------------

        'jrs 2008/06/23
        txt_desired.Text = txt_value.Text


        'mti webform June 2004 ChangeVal
        'Call GraphUpdate()
        'lblJS.PeFunction.ReinitializeResetImage()
        lblJS.Visible = True
    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub but_PreviousYear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles but_PreviousYear.Click
        Dim VARNUM As Object

        'mti August 2004 - 2
        Dim j As Short 'Object

        Dim finam As String
        'jrs 2005/02/16
        Dim fNum As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Record existing/edited value
        '   ----------------------------
        'Scenario Change MTI 18 May 2001
        Number = Val(txt_value.Text)
        If Session("Changing_Custom_Scenario") Then
            'CKD 3/24/04 frm_ScenarioCreat not updated
            'j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            j = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
            'CKD 3/24/04 remove DefInstance
            Session("tPLOT")(CurYear - Session("FirstYear") + 1, j) = Number
            'no need to change plotri() at this point
        End If
        '----------------

        '   Back up one Year and retrieve Value
        '   -----------------------------------

        CurYear = CurYear - 1

        'CKD 3/24/04 remove DefInstance
        If CurYear < Session("FirstYear") Then CurYear = Session("FirstYear")
        Session("CurYear") = CurYear
        txt_year.Text = Str(CurYear)

        'Scenario Change MTI 18 May 2001
        If Not Session("Changing_Custom_Scenario") Then
            Session("VarVal") = Session("tPLOT")(CurYear - Session("FirstYear") + 1, 1)
            CurValue = Session("VarVal")
            Session("CurValue") = CurValue
            txt_value.Text = Str(CurValue)
            txt_desired.Text = Str(CurValue)
        Else
            'CKD 3/24/04 frm_scenarioCreate not updated
            ' j = Session("Node2Plot")(frm_ScenarioCreate.DefInstance.CurNodeNo)
            'mti August 2004 - 2 start
            j = Session("Scenario_PlotCol")
            'mti August 2004 - 2 end
            'CKD 3/24/04 remove Definstance
            Session("VarVal") = Session("tPLOT")(CurYear - Session("FirstYear") + 1, j)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CurValue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CurValue = Session("VarVal")
            Session("CurValue") = CurValue
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            txt_value.Text = Str(Session("VarVal"))
        End If

        '----------------------

        'jrs 2008/06/23
        txt_desired.Text = txt_value.Text


        'mti webform June 2004 ChangeVal
        'Call GraphUpdate()
        'lblJS.PeFunction.ReinitializeResetImage()
        lblJS.Visible = True
    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub cmd_Exit2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit2.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CKD 3/24/04 remove windows forms references
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'CKD 3/24/04 remove DefInstance
        frm_ChangeValues_Closed(eventSender, eventArgs)

        'mti August 2004 - 2 start
        'note from mti: followed the path back according to vb6 code

        'Scenario Change MTI 18 May 2001
        If Not Session("Changing_Custom_Scenario") Then
            'jrs 11/17/04
            Support.FormManager.Show(Me, "frm_ScenarioManage.aspx")
            'Support.FormManager.Close(Me)
            'Support.FormManager.Show(Me, "frm_MainMenu.aspx")
        Else
            'jrs 10/19/04 Support.FormManager.Close(Me)
            Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
        End If

        'mti August 2004 - 2 end

        'CKD 3/24/04 remove windows forms references
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


    End Sub

    'UPGRADE_WARNING: Form event frm_ChangeValues.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Activated' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Activated.html'.
    Private Sub frm_ChangeValues_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'Scenario Change MTI 18 May 2001
        'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        WinFormsToWeb_FocusControl.Focus("txt_desired")
    End Sub

    '=============================ChanVals.Frm ===========================
    'CONVERSION_NOTE: The code of Event 'frm_ChangeValues.frm_ChangeValues_Load' was moved to Method 'frm_ChangeValues.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_ChangeValues_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        PackJS()

        'jrs 2026/03/18 Title has variable name, so easier just to pass a constant value
        ''jrs 2013/09/03
        'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
        '    ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
            ShowHelpJS("Scenario Development and Change", ""), True)
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
        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "enabled: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "type: 'category'," & vbCr
        sJscript2 = sJscript2 & "tickInterval: 20," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & ", min: " & Session("FirstYear") & vbCr
        'If Session("rgYear") Is Nothing Then
        '    sJscript2 = sJscript2 & ", max: " & CStr(Session("LastYear")) & vbCr
        'Else
        '    sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("RunningYear") - 1) & vbCr
        'End If
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & ", min: " & Session("grfMin") & vbCr
        sJscript2 = sJscript2 & ", max: " & Session("grfMax") & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim j As Integer
        j = CShort(Session("Scenario_CurNodeFlatIndex"))
        j = Session("Node2Plot")(j)
        sJscript2 = sJscript2 & PlotColToJSSeries(j)

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

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

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95 - 50;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 50;" & vbCr

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.25;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.3;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2
    End Sub

    Private Function PlotColToJSSeries(idx As Integer) As String
        Dim sJscript As String = ""
        sJscript = sJscript & "{name: '" & V(Session("SEL")) & "'," & vbCr

        Dim sColor As String = ""
        sColor = "Blue"
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr

        sJscript = sJscript & "data: ["

        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim Diff As Single, Min As Single, Max As Single
        Min = 10 ^ 10
        Max = -10 ^ 10
        Dim iNumDec As Integer = 0

        If IsNumeric(Session("tPLOT")(1, idx)) Then
            If Math.Abs(Session("tPLOT")(1, idx)) < 1 Then
                'jrs 2023/09/28
                If Session("grfMax") - Session("grfMin") < 0.0015 Then
                    iNumDec = 4
                Else
                    iNumDec = 3
                End If
            ElseIf Math.Abs(Session("tPLOT")(1, idx)) < 10 Then
                iNumDec = 2
            ElseIf Math.Abs(Session("tPLOT")(1, idx)) < 100 Then
                iNumDec = 1
            End If
        Else
            iNumDec = 2
        End If

        For j = 1 To Session("LastYear") - Session("FirstYear") + 1
            If j > 1 Then
                sJscript = sJscript & ", "
            End If
            sJscript = sJscript & "[" & CStr(Session("FirstYear") + j - 1) & ", "
            sJscript = sJscript & CStr(Math.Round(Session("tPLOT")(j, idx), iNumDec)) & "]"
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToJSSeries = sJscript
    End Function

    '=============================ChanVals.Frm ===========================
    'jrs 2010/09/01 This procedure is not working
    'Private Sub frm_ChangeValues_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    Dim LineOut As String
    '    Dim LenNum As Object
    '    Dim VLine As String
    '    Dim finam2 As String
    '    Dim VARNUM As Object
    '    Dim finam As String
    '    Dim chk As Object
    '    'mti August 2004 2
    '    Dim j As Short 'Object
    '    'jrs 2008/05/30 Currency
    '    Dim CurrFactor As Single
    '    Dim sVar As String
    '    Dim idx As Integer
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '    'mti August 2004 2 - start
    '    'If session("tCOUNTE") = 0 Or CBool(ChangesMade) = False Then Exit Sub 'Changes cancelled or not introduced
    '    If Session("tCOUNTE") = 0 Or CBool(Session("ChangesMade")) = False Then Exit Sub 'Changes cancelled or not introduced
    '    'mti August 2004 2 - end

    '    'Mohammod Oct 03, 2001
    '    'Write String to Working.Sce
    '    Dim WriteStr As String

    '    'JRS 10/08/03
    '    Dim bAll As Boolean
    '    Dim DimNum As Short
    '    Dim sWrite() As String
    '    Dim ii As Short
    '    Dim WriteStrOrg As String

    '    'mtiwebformsMarch2004 (2)ScenarioCreate
    '    'mti August 2004 2
    '    Dim TempStr As String
    '    If Session("Changing_Custom_Scenario") Then

    '        j = Session("Scenario_PlotCol")

    '        'jrs 2008/05/30 Currency
    '        sVar = Session("tVARN")(1, j)
    '        idx = InStr(sVar, "[", CompareMethod.Text)
    '        If idx > 0 Then
    '            sVar = Mid(sVar, 1, idx - 1)
    '        End If
    '        chk = IfsVars.Item(sVar).Index

    '        CurrFactor = 1
    '        'if it's a currency variable
    '        If VCurrency(CInt(chk)) = "True" Then
    '            'load the currency convertion value
    '            CurrFactor = Session("CurrencyVal")
    '        End If

    '        TempStr = Trim(Str(Math.Round(Session("tPLOT")(1, j) * CurrFactor, 4)))

    '        For i2 = 2 To Session("LastYear") - Session("FirstYear") + 1
    '            TempStr = TempStr & "," & Trim(Str(Math.Round(Session("tPLOT")(i2, j) * CurrFactor, 4)))
    '        Next i2

    '        'cut last comma and return
    '        Session("ChVal_CustStr") = TempStr
    '        Exit Sub
    '    End If
    '    'mti August 2004 2

    '    'CKD 3/24/04 frm_chane not updated
    '    bAll = InStr(Session("ChangeScenPref"), "ALL") > 0 'jrs 2006/06/02 ALL has been disabled
    '    If bAll Then
    '        WriteStr = ""
    '        'CKD 3/24/04 frm_change not updated
    '        sWrite = Split(Session("ChangeScenPref"), ",")
    '        For ii = 0 To UBound(sWrite)
    '            If sWrite(ii) = "ALL" Then
    '                DimNum = ii - 1
    '                Exit For
    '            End If
    '            WriteStrOrg = WriteStrOrg & sWrite(ii) & ","
    '        Next ii
    '    Else
    '        'CKD 3/24/04 frm_change not updated
    '        WriteStr = Session("ChangeScenPref") ' Trim(Str(Session("tPLOT")(1, j)))
    '    End If

    '    j = Session("tCOUNTE")
    '    sVar = Session("tVARN")(1, j)
    '    idx = InStr(sVar, "[", CompareMethod.Text)
    '    If idx > 0 Then
    '        sVar = Mid(sVar, 1, idx - 1)
    '    End If
    '    chk = IfsVars.Item(sVar).Index
    '    Session("SEL") = chk

    '    'jrs 2008/05/30 Currency
    '    CurrFactor = 1
    '    'if it's a currency variable
    '    If VCurrency(CInt(Session("SEL"))) = "True" Then
    '        'load the currency convertion value
    '        CurrFactor = Session("CurrencyVal")
    '    End If

    '    'If VScenario(Session("SEL")) <> "" Then
    '    'jrs 2005/02/16
    '    Dim fNum As Integer
    '    'mti August 2004 2 start: Talk to Jose (VB6 has Counte, not grd...columncount
    '    'also changevalue should be 1 column always?
    '    'JRS 07/09/03
    '    'For j = 1 To grd_VariableDisplay.Columns.Count 'IFSWEB had this before- mti changed to the following (as per vb6)
    '    For j = 1 To Session("tCOUNTE") 'grd_VariableDisplay.Columns.Count
    '        If Not Session("Changing_Custom_Scenario") Then

    '            'CKD 3/24/04 frm_change not updated
    '            If Session("ChangecmdUseGroups") = "Use Countries" Then
    '                WriteStr = "CUSTOM," & V(Session("SEL")) & ","
    '                'JRS 10/01/03
    '                WriteStr = WriteStr & grd_VariableDisplay(j - 1, 1) 'END 07/09/03
    '            End If
    '        End If

    '        'JRS 10/08/03
    '        If bAll Then
    '            If DimNum = 1 Then
    '                WriteStr = WriteStrOrg & Session("tGR1N")(j)
    '                If VDim2(Session("SEL")) > 0 Then
    '                    WriteStr = WriteStr & "," & sWrite(UBound(sWrite))
    '                End If
    '            Else
    '                WriteStr = WriteStrOrg & Session("tGR2N")(j)
    '            End If
    '        End If

    '        'CKD 3/24/04 remove DefInstance

    '        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
    '            Number = Math.Round(Session("tPLOT")(i2, j) * CurrFactor, 4)
    '            Session("VarVal") = Number
    '            WriteStr = WriteStr & "," & Session("VarVal") 'Trim(Str(Session("tPLOT")(i2, j)))
    '        Next i2
    '        'jrs 2005/02/16
    '        fNum = FreeFile()
    '        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Append)
    '        PrintLine(fNum, WriteStr)
    '        FileClose(fNum)

    '    Next j
    '    'End If

    '    'Mohammod Oct 03, 2001


    '    Dim iRecordNum As Short
    '    Dim sSQL As String
    '    If CBool(Session("SensitivityFlag")) = True Then

    '        '   Write to SensitivityInput File
    '        '   ------------------------------

    '        Dim fNum2 As Integer, fNum4 As Integer
    '        fNum2 = FreeFile()
    '        finam2 = Session("SensitivityFileDir") & "SensitivityInput.dat"
    '        '        Open finam2$ For Output As #2

    '        'JRS Change Active Run BEGIN 5/14/01
    '        If CBool(Session("ActiveRun")) = False Then
    '            'JRS Append vs OverWrite Option BEGIN 05/11/01
    '            If CBool(Session("SensitivityOW")) = True Then
    '                FileOpen(fNum2, finam2, OpenMode.Output)
    '            Else
    '                FileOpen(fNum2, finam2, OpenMode.Append)
    '            End If
    '            'JRS Append vs OverWrite Option END 05/11/01
    '        Else
    '            FileOpen(fNum2, finam2, OpenMode.Input)
    '            fNum4 = FreeFile()
    '            FileOpen(fNum4, finam2 & "2", OpenMode.Output)
    '            While Not EOF(fNum2)
    '                Input(fNum2, iRecordNum)
    '                VLine = LineInput(fNum2)
    '                If iRecordNum <> Session("SensitivityNumber") Then
    '                    VLine = RTrim(Str(iRecordNum)) & ", " & VLine
    '                    PrintLine(fNum4, VLine)
    '                End If
    '            End While
    '            FileClose(fNum2)
    '            FileClose(fNum4)
    '            Kill(finam2)
    '            Rename(finam2 & "2", finam2)
    '            FileOpen(fNum2, finam2, OpenMode.Append)
    '            'JRS 06/08/03 RAND PARDEE
    '            updateAccessWith1Command(Session("SensitivityFileDir") & "SensitivityInput.mdb", ("DELETE * FROM SensitivityInput WHERE Run = " & Trim(Str(Session("SensitivityNumber")))))

    '        End If
    '        'JRS Change Active Run END 5/14/01


    '        For j = 1 To grd_VariableDisplay.Columns.Count
    '            LenNum = Len(Session("SensitivityNumber"))
    '            '            LineOut$ = right$(Str$(SensitivityNumber), LenNum) + ", " + Session("tVARN")$(1, j) + ", "
    '            LineOut = VB.Right(LTrim(Str(Session("SensitivityNumber"))), LenNum) & ", " & Session("tVARN")(1, j) & ", "
    '            If Session("tGR1N")(j) <> "" Then LineOut = LineOut & Session("tGR1N")(j) & ", "
    '            If Session("tGR2N")(j) <> "" Then LineOut = LineOut & Session("tGR2N")(j) & ", "
    '            If Session("tGR3N")(j) <> "" Then LineOut = LineOut & Session("tGR3N")(j) & ", " 'jrs 2006/06/01 dim3
    '            'JRS 05/28/03 RAND PARDEE
    '            sSQL = "INSERT INTO SensitivityInput VALUES(" & Trim(Str(Session("SensitivityNumber"))) & ",'" & Session("tVARN")(1, j) & "','"
    '            If Session("tGR1N")(j) <> "" Then sSQL = sSQL & Session("tGR1N")(j)
    '            sSQL = sSQL & "','"
    '            If Session("tGR2N")(j) <> "" Then sSQL = sSQL & Session("tGR2N")(j)
    '            sSQL = sSQL & "',"
    '            'jrs 2006/06/01 dim3
    '            If Session("tGR3N")(j) <> "" Then sSQL = sSQL & Session("tGR3N")(j)
    '            sSQL = sSQL & "',"

    '            'CKD 3/24/04 remove Definstance
    '            For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
    '                If i2 > Session("tFinYear")(j) Then
    '                    Session("VarVal") = 0
    '                Else
    '                    'jrs 2008/05/30 Currency, save numbers in US$, added * CurrFactor
    '                    Number = Math.Round(Session("tPLOT")(i2, j) * CurrFactor, 4)
    '                    Session("VarVal") = Number
    '                End If

    '                'CKD 3/24/04 remove DefInstance
    '                If i2 = Session("LastYear") - Session("FirstYear") + 1 Then
    '                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                    LineOut = LineOut & Str(Session("VarVal"))
    '                Else
    '                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                    LineOut = LineOut & Str(Session("VarVal")) & ", "
    '                End If
    '                'JRS 05/28/03 RAND PARDEE
    '                'CKD 3/24/04 remove DefInstance
    '                updateAccessWith1Command(Session("SensitivityFileDir") & "SensitivityInput.mdb", (sSQL & Trim(Str(Session("FirstYear") + i2 - 1)) & "," & Trim(Str(Session("VarVal"))) & ")"))
    '            Next i2
    '            PrintLine(fNum2, LineOut)
    '        Next j
    '        FileClose(fNum2)
    '        Session("SensitivityNumberTot") = AMAX(Session("SensitivityNumberTot"), Session("SensitivityNumber"))
    '        Session("PercentOn") = "False"
    '        Session("ActiveRun") = CStr(False)

    '        '   Reset working.run
    '        '   ---------------
    '        Session("SensitivityFlag") = CStr(False)
    '    ElseIf Not Session("Changing_Custom_Scenario") Then
    '        'jrs 2006/06/02 execution never reaches this code,
    '        'since we don't allow to enter change from other place but from Sensitivity
    '        'anyway VL(j) doesn't contain the right pointer, since we chaged the logic for groups

    '        '   Place Values into Session("tPLOT") so that can display
    '        '   ------------------------------------------

    '        finam = Session("PutFdrvRun") & "working.run"
    '        fNum = FreeFile()
    '        FileOpen(fNum, finam, OpenMode.Binary)

    '        'CKD 3/24/04 remove DefInstance
    '        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
    '            For j = 1 To grd_VariableDisplay.Columns.Count
    '                If i2 > Session("tFinYear")(j) Then
    '                    Session("VarVal") = 0
    '                Else
    '                    Session("RECORD") = (Session("VL")(j) + ((i2 - 1) * filen)) * 4 - 3
    '                    VARNUM = j
    '                    FileGet(fNum, Number, Session("RECORD"))
    '                    Session("VarVal") = Number
    '                End If
    '                Session("tPLOT")(i2, j) = Session("VarVal")
    '            Next j
    '        Next i2
    '        FileClose(fNum)
    '        Session("PercentOn") = "False"
    '    End If

    'End Sub

    'jrs 2010/09/01 new version
    Private Sub frm_ChangeValues_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If Session("tCOUNTE") = 0 Or CBool(Session("ChangesMade")) = False Then Exit Sub 'Changes cancelled or not introduced

        'Mohammod Oct 03, 2001
        'Write String to Working.Sce
        Dim WriteStr$

        'JRS 10/08/03
        Dim bAll As Boolean
        Dim DimNum As Integer
        Dim sWrite() As String
        Dim II As Integer
        Dim WriteStrOrg As String
        Dim j As Integer, chk As Integer, SEL As Integer, idx As Integer, fNum As Integer
        Dim sVar As String, VarVal As Single
        bAll = InStr(Session("ChangeScenPref"), "ALL") > 0 'jrs 2006/06/02 ALL has been disabled
        If bAll Then
            WriteStr$ = ""
            sWrite = Split(Session("ChangeScenPref"), ",")
            For II = 0 To UBound(sWrite)
                If sWrite(II) = "ALL" Then
                    DimNum = II - 1
                    Exit For
                End If
                WriteStrOrg = WriteStrOrg + sWrite(II) + ","
            Next II
        Else
            'jrs 2010/09/01 WriteStr$ = Session("ChangeScenPref")  ' Trim(Str(Session("tPLOT")(1, j)))
            WriteStr = Session("LinePrefix")(Session("Scenario_CurNodeFlatIndex"))
        End If

        'jrs 2010/09/01 j = Session("tCOUNTE")
        j = Session("Scenario_PlotCol")
        'jrs 2008/05/30 Currency
        sVar = Session("tVARN")(1, j)
        'jrs 2023/06/09
        If sVar = "" Then
            Exit Sub
        End If
        idx = InStr(sVar, "[", CompareMethod.Text)
        If idx > 0 Then
            sVar = Mid(sVar, 1, idx - 1)
        End If
        chk = IfsVars.Item(sVar).Index
        SEL = chk

        'jrs 2008/05/30 Currency
        Dim CurrFactor As Single
        CurrFactor = 1
        'if it's a currency variable
        If VCurrency(CInt(chk)) = "True" Then
            'load the currency convertion value
            CurrFactor = Session("CurrencyVal")
        End If


        'JRS 10/08/03
        If bAll Then
            If DimNum = 1 Then
                WriteStr = WriteStrOrg + Session("tGR1N")(j)
                If VDim2(SEL) > 0 Then
                    WriteStr = WriteStr + "," + sWrite(UBound(sWrite))
                End If
            Else
                WriteStr = WriteStrOrg + Session("tGR2N")(j)
            End If
        End If

        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
            'jrs 2006/06/02 change group logic
            '                RECORD = (VL(j) + ((i2 - 1) * filen)) * 4 - 3
            '                VARNUM = j
            '                Get #1, RECORD, Number!

            'jrs 2008/05/30 Currency, save numbers in US$, added * CurrFactor
            Number = Math.Round(Session("tPLOT")(i2, j) * CurrFactor, 4)

            VarVal = Number!
            WriteStr$ = WriteStr$ & "," & VarVal 'Trim(Str(Session("tPLOT")(i2, j)))
        Next i2
        'jrs 2006/06/02 change group logic Close #1

        'jrs 2005/02/16
        fNum = FreeFile()
        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Append)
        PrintLine(fNum, WriteStr)
        FileClose(fNum)

        'jrs 2008/05/30    Next j
        '   BBH 7/18/04 V5_01 Begin
        'msg$ = "Your customized changes have been added to the scenario tree."
        'MsgBox(msg$)
        '   BBH 7/18/04 V5_01 End
        'End If

        'Mohammod Oct 03, 2001

        'jrs 2020/09/17
        Session("ChVal_CustStr") = WriteStr
        RemoveRepeatedParameters(WriteStr)
    End Sub

    'jrs 2020/09/17
    Private Sub RemoveRepeatedParameters(WriteStr As String)
        'Open File
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Input)
        Dim sline As String
        Dim sLines() As String
        Dim length As Integer = 0
        Dim key As String = Session("sSaveLine")
        'jrs 2023/06/23
        Dim bTheresComment As Boolean = False
        'jrs 2023/10/26
        Dim bFlagSkip As Boolean = False
        'Load all lines exept the saved one
        Do While Not EOF(fNum)
            sline = LineInput(fNum)
            'jrs 2023/06/23
            If sline = "COMMENT,START" Then
                bTheresComment = True
            End If
            'jrs 2023/06/23 added sline.Length<key.Length OrElse 
            If sline.Length < key.Length OrElse sline.Substring(0, key.Length) <> key Or sline = WriteStr Then
                ReDim Preserve sLines(length)
                sLines(length) = sline
                length = length + 1
            Else 'jrs 2023/10/26
                bFlagSkip = True
            End If
        Loop
        FileClose(fNum)
        'Rewrite the file with te loaded lines
        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Output)
        For i = 0 To length - 1
            PrintLine(fNum, sLines(i))
        Next
        FileClose(fNum)
        'jrs 2023/10/26 the modified line now goes last, and the original one was skipped when re-writting
        If bFlagSkip Then
            Session("Scenario_PlotCol_Last") = length
        Else
            Session("Scenario_PlotCol_Last") = Session("Scenario_PlotCol")
        End If

        'jrs 2023/06/23
        If bTheresComment Then
            MoveCommentsToEnd(WriteStr)
        End If
    End Sub

    'jrs 2023/06/23
    Private Sub MoveCommentsToEnd(WriteStr As String)
        'Open File
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Input)
        Dim sline As String
        Dim sLines() As String, sComments() As String
        Dim length As Integer = 0, lengthC As Integer = 0
        Dim key As String = Session("sSaveLine")
        Dim bCommentStart As Boolean = False
        'Load all lines exept the saved one
        Do While Not EOF(fNum)
            sline = LineInput(fNum)
            If sline = "COMMENT,START" Then
                bCommentStart = True
            End If
            If Not bCommentStart Then
                ReDim Preserve sLines(length)
                sLines(length) = sline
                length = length + 1
            Else
                If sline <> WriteStr Then
                    ReDim Preserve sComments(lengthC)
                    sComments(lengthC) = sline
                    lengthC = lengthC + 1
                End If
            End If
        Loop
        FileClose(fNum)
        'Rewrite the file with te loaded lines
        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Output)
        For i = 0 To length - 1
            PrintLine(fNum, sLines(i))
        Next
        'Rewrite new line
        PrintLine(fNum, WriteStr)
        'Rewrite comment at the bottom
        For i = 0 To lengthC - 1
            PrintLine(fNum, sComments(i))
        Next
        FileClose(fNum)
    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub Gridput()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Fill Grid
        '   ---------

        Dim iloop As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 4 / 25 / 2
        '   Fill Grid
        '   ---------
        grd_VariableDisplay.Columns.Clear()
        grd_VariableDisplay.Visible = (Session("tCOUNTE") > 0)

        '      If Counte < 1 Then Counte = 1
        'JRS 02/18/04
        Dim r1 As New TableRow, r2 As New TableRow, r3 As New TableRow, r4 As New TableRow
        For iloop = 1 To Session("tCOUNTE")
            Dim myCol As New TMSWebControls.Column
            'myCol.Type = TMSWebControls.Column.ColumnType.Button
            myCol.WidthType = TMSWebControls.WType.Absolute
            'myCol.ButtonWidth = 100
            myCol.Width = 100
            grd_VariableDisplay.Columns.Add(myCol)

            grd_VariableDisplay(iloop - 1, 0) = Session("tVARN")(1, iloop)
            grd_VariableDisplay(iloop - 1, 1) = Session("tGR1N")(iloop)
            grd_VariableDisplay(iloop - 1, 2) = Session("tGR2N")(iloop)
            grd_VariableDisplay(iloop - 1, 3) = Session("tGR3N")(iloop)
            grd_VariableDisplay(iloop - 1, 4) = Session("tGR4N")(iloop) 'jrs 2006/06/16

        Next iloop

    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub hsc_Slider1_Change(ByVal newScrollValue As Integer)
        Dim SliderVal As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        SliderVal = newScrollValue
        txt_sliderval.Text = Str(SliderVal / ValMult)

    End Sub






    '=============================ChanVals.Frm ===========================
    Private Sub PutValues()
        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '     Put values into buffer
        '     ----------------------

        finam = Session("PutFdrvRun") & "working.run.db"
        'jrs 2005/02/16
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, finam, OpenMode.Binary)
        'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FilePut(fNum, Number, CInt(Session("RECORD")))
        FileClose(fNum)


    End Sub

    '=============================ChanVals.Frm ===========================
    Private Sub RetrValues()
        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '     Retrieve values from buffer
        '     ---------------------------

        finam = Session("PutFdrvRun") & "working.run.db"
        'jrs 2005/02/16
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, finam, OpenMode.Binary)
        FileGet(fNum, Number, Session("RECORD"))
        Session("VarVal") = Number
        FileClose(fNum)

    End Sub

    '=============================ChanVals.Frm ===========================
    Public Sub mnuDefine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_NameDisp.aspx")
    End Sub
    '=============================ChanVals.Frm ===========================
    'CKD 4/7/04 Comment out windows menu function
    'Public Sub mnuExplain_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuExplain.Popup
    '    mnuExplain_Click(eventSender, eventArgs)
    'End Sub
    Public Sub mnuExplain_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim curr_col As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

        'CKD 4/4/04 comment out GRID
        'curr_col = grd_VariableDisplay.col + 1

        'CKD 4/4/04 comment out windows reference
        'System.Windows.Forms.SendKeys.Send("{F1}")

    End Sub

    'CKD 4/4/04 Comment out Slider events section start
    'Private Sub hsc_Slider1_Scroll(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ScrollEventArgs) Handles hsc_Slider1.Scroll
    '    'CONVERSION_ISSUE: Class System.Windows.Forms.ScrollEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    'CONVERSION_ISSUE: Class System.Windows.Forms.ScrollEventType from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    Select Case eventArgs.type
    '        Case System.Windows.Forms.ScrollEventType.EndScroll
    '            'CONVERSION_ISSUE: Class System.Windows.Forms.ScrollEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '            hsc_Slider1_Change(eventArgs.newValue)
    '    End Select
    'End Sub
    'CKD 4/4/04 slider section end

    'jrs 2014/07/18
    Private Sub GraphUpdate()
        Dim j As Integer, RTime As Integer, i As Integer
        j = Session("Node2Plot")(Session("Scenario_CurNodeFlatIndex"))
        RTime = Session("LastYear") - Session("FirstYear") + 1
        Session("grfMax") = Session("tPLOT")(1, j)
        Session("grfMin") = Session("tPLOT")(1, j)
        If Session("grfMin") > 0 Then
            Session("grfMin") = 0
        End If

        For i = 2 To RTime
            If Session("grfMax") < Session("tPLOT")(i, j) Then
                Session("grfMax") = Session("tPLOT")(i, j)
            End If
            If Session("grfMin") > Session("tPLOT")(i, j) Then
                Session("grfMin") = Session("tPLOT")(i, j)
            End If
        Next

        If Session("grfMax") = Session("grfMin") Then
            Session("grfMax") = Session("grfMax") + 1
        End If

        PackJS()
    End Sub

    Private Sub txt_year_TextChanged(sender As Object, e As EventArgs) Handles txt_year.TextChanged
        'jrs 2024/08/19
        If txt_year.Text > "" Then
            Dim tmpVal As String
            tmpVal = txt_year.Text
            If IsNumeric(tmpVal) Then
                Dim YearVal As Integer
                YearVal = CInt(tmpVal)
                If YearVal >= Session("FirstYear") And YearVal <= Session("LastYear") Then
                    CurYear = YearVal
                    Session("CurYear") = CurYear
                    Dim j = Session("Scenario_PlotCol")
                    Session("VarVal") = Session("tPLOT")(CurYear - Session("FirstYear") + 1, j)
                    CurValue = Session("VarVal")
                    Session("CurValue") = CurValue
                    txt_value.Text = Str(Session("VarVal"))
                    txt_desired.Text = txt_value.Text
                Else
                    Dim MSG = "Warning:  Year needs to be between " & Session("FirstYear") & " and " & Session("LastYear")
                    Response.Write("<script>alert('" & MSG & "');</script>")
                End If
            Else
                Dim MSG = "Warning:  Year needs to be between " & Session("FirstYear") & " and " & Session("LastYear")
                Response.Write("<script>alert('" & MSG & "');</script>")
            End If
        Else
            Dim MSG = "Warning:  Need to imput Year."
            Response.Write("<script>alert('" & MSG & "');</script>")
        End If

    End Sub
End Class
