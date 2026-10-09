Option Strict Off
Option Explicit On
Imports Coalesys.WebMenu
Imports System.Web.Script.Services
Imports NLog

Partial Class frm_Run
    Inherits Page

    Private Shared _logger As NLog.Logger = LogManager.GetCurrentClassLogger()
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'If Session("RunningYear") >= 2 Then
        '    Debug.Print("Run Init: " & Session("RunningYear"))
        'End If

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
            'jrs 2005/05/25 changed First Year to First Year Forecast, if History plus Forecast
            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                    Session("FirstYear") = FirstYearForecast
                    ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                End If
            End If

            Dim msg As String
            Dim Number As Single
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '-----------raman 17th March
            '    General_Run   ' setting of the base file and run file
            '-----------------------------------

            '----------raman 23rd April-----------
            ran_guided_use = False

            '========raman march '02------------

            If reset_bool = False Then
                'CONVERSION_ISSUE: Class System.Windows.Forms.FormWindowState from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.WindowState.html'.
                'JRSVBNET WindowState = System.Windows.Forms.FormWindowState.Normal
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
                'JRSVBNET Width = VB6.TwipsToPixelsX(Inch * 7)
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
                'JRSVBNET Height = VB6.TwipsToPixelsY(Inch * 5.5)
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Top.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
                'JRSVBNET Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2)
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Left.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
                'JRSVBNET Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)
                'reset_bool = False
            End If

            '========raman march '02------------
            If Guided_use = True Then
                'CONVERSION_ISSUE: Class System.Windows.Forms.FormWindowState from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.WindowState.html'.
                'JRSVBNET WindowState = System.Windows.Forms.FormWindowState.Maximized
            End If

            reset_bool = False

            If Session("Scenarioflag") = True Then
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Running " & Session("ScenarioName") & " Scenario"
            Else
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Running " & ModelName
            End If

            'gvdb 2024/04/17 use client side scripting
            'cmd_continue.Visible = False
            cmd_continue.Visible = True

            'JRSVBNET grf_WPOP.Visible = False
            'JRSVBNET grf_WGDP.Visible = False
            'JRSVBNET grf_wforst.Visible = False
            'JRSVBNET grf_co2.Visible = False
            'JRSVBNET grf_WPOP.NumPoints = 2
            'JRSVBNET grf_WGDP.NumPoints = 2
            'JRSVBNET grf_wforst.NumPoints = 2
            'JRSVBNET grf_co2.NumPoints = 2
            'JRSVBNET grf_WPOP.DataReset = 1
            'JRSVBNET grf_WGDP.DataReset = 1
            'JRSVBNET grf_wforst.DataReset = 1
            'JRSVBNET grf_co2.DataReset = 1

            txt_RunYears.Visible = True
            txt_RunYears.Enabled = True

            If CBool(RebuiltBase) = True Then
                'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                txt_RunYears.Text = Str(Session("LastYear"))
            Else
                'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                If Session("HistoricBase") = True Then
                    'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    txt_RunYears.Text = Str(Session("LastYear"))
                Else
                    'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    Dim RunYear As Integer = FirstYearForecast + 20
                    While RunYear Mod 5 <> 0
                        RunYear += 1
                    End While
                    txt_RunYears.Text = Str(RunYear)
                End If
            End If
            lbl_RunYears.Text = "Year to which model will run (you may edit):"

            'jrs 2005/02/12
            'Dim fNum As Integer
            'fNum = FreeFile()
            'FileOpen(fNum, Session("PutFdrvRun") & "working.run", OpenMode.Binary)
            'Session("RECORD") = VarPoint(PosModYear)
            'Session("RECORD") = Session("RECORD") * 4 - 3
            'FileGet(fNum, Number, Session("RECORD"))
            Dim MODYEAR As Integer
            'MODYEAR = Number
            'FileClose(fNum)
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("hlmodelsw", -1, "", -1, "", -1, "", -1, Session("PutFdrvRun") & "working.run.db", 0, True, True, Nothing, 1, Session("PLOT"), 0,
                1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay("hlmodelsw", -1, "", -1, "", -1, "", -1, Session("PutFdrvRun") & "working.run", 0, True, True, Nothing, 1, Session("PLOT"), 0,
                1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If

            MODYEAR = Session("FirstYear") + Session("FinYear")(0) - 1

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("LastYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MODYEAR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            If MODYEAR = Session("LastYear") Or Session("Reload") = "True" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("FirstYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object BeginRun. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                Session("BeginRun") = Session("FirstYear")
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object MODYEAR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object BeginRun. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("BeginRun") = MODYEAR + 1
            End If

            msg = "ALERT: The model has been run before and "
            msg = msg & "will start running in the year "
            'UPGRADE_WARNING: Couldn't resolve default property of object BeginRun. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & Str(Session("BeginRun")) & " unless you reset it."
            lbl_BeginRun.Text = msg
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("LastYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MODYEAR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FirstYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            If MODYEAR = Session("FirstYear") Or MODYEAR = Session("LastYear") Or Session("Reload") = "True" Then
                lbl_BeginRun.Visible = False
                cmd_Reset.Visible = False
            Else
                lbl_BeginRun.Visible = True
                'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                cmd_Reset.Text = "Reset Start Year to" & Str(Session("FirstYear"))
                cmd_Reset.Visible = True
            End If
            'JRS 06/10/03
            'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            If Session("mnuSQLite") = "False" Then
                Session("WorkingFile") = Session("WorkingFile").ToString.Replace(".DB", "")
            End If
            'jrs 2021/05/04
            lbl_WorkingFile.Text = "The working file is based on " & Session("WorkingFile") & " (Base Year is " & CStr(FirstYearForecast) & ")"
            'jrs 2019/12/31
            If RebuiltBase Then
                lblRebuildBase.Visible = True
                lblRebuildBase.Text = "     Because you have rebuilt the base, you should run <br>"
                lblRebuildBase.Text = lblRebuildBase.Text & "&nbsp&nbsp&nbsp&nbsp&nbsp the model through the year " & Session("LastYear")
            Else
                lblRebuildBase.Visible = False
            End If
            'JRS 10/08/02 GUIDED USE
            'this is used to validate that we're really in step 4
            local_Guided_use = False
            Dim k As Short
            If Guided_use Then
                'UPGRADE_ISSUE: Forms collection was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Forms.Count. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'JRSVBNET *** BEGIN ***
                'For k = 0 To Forms.Count - 1
                '	'UPGRADE_ISSUE: Forms collection was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
                '	'UPGRADE_WARNING: Couldn't resolve default property of object Forms(k).Name. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                '	If Forms(k).Name = "frm_GuidedUse3" Or Forms(k).Name = "frm_GuidedUse2" Then
                '		local_Guided_use = True
                '		Exit For
                '	End If
                'Next k
                'JRSVBNET *** END *** 
            End If
            '***************************************************
            'JRS 08/01/02 GUIDEDUSE
            'jrsvbnet mnuExit.Visible = local_Guided_use
            'jrsvbnet mnuReset.Visible = local_Guided_use
            'jrsvbnet mnuBack.Visible = local_Guided_use
            'jrsvbnet mnuNext.Visible = local_Guided_use
            If local_Guided_use = True Then
                '-------------raman 17th March---------
                final_year_run = Val(txt_RunYears.Text)
                '-------------------------------------
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Step 5 - " & WinFormsToWeb_PageBar.Rows(0).Cells(1).Text
                'JRS 04/24/03
                'jrsvbnet mnuExit.Text = "Exit Guided Use"
            End If
            'mti WebForm Changes From June 21, 2004 -start
            'initialize arrays for four variables
            ReDim Session("rgYear")(Session("LastYear") - Session("FirstYear") + 1)
            ReDim Session("rgWPOP")(Session("LastYear") - Session("FirstYear") + 1)
            ReDim Session("rgWGDP")(Session("LastYear") - Session("FirstYear") + 1)
            ReDim Session("rgWFORST")(Session("LastYear") - Session("FirstYear") + 1)
            ReDim Session("rgCO2PPM")(Session("LastYear") - Session("FirstYear") + 1)
            'labels for graphs
            'lbl_WPOP.Text = "World Population (Millions)"
            'lbl_WGDP.Text = "World GDP (Billion Currency)"
            'lbl_wforst.Text = "World Forest (Million Hectares)"
            'lbl_co2.Text = "Atmospheric CO2 (PPM)"
            'mti WebForm Changes From June 21, 2004 - end
            'JRS 05/29/04
            'gvdb 2024/04/05
            ' remove timer control
            'Timer1.Visible = False
            'lblJS.Visible = False

            'jrs 2023/01/16
            If Not (Session("GuidedBaseYear") = Nothing) Then
                txtAutomaticRun.Value = "1"
                Session("GuidedBaseYear") = Nothing
            End If
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
    'CONVERSION_NOTE: Class 'AxGraphLib.AxGraph' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents 'JRSVBNET grf_wforst As AxGraphLib.AxGraph 
    'CONVERSION_NOTE: Class 'AxGraphLib.AxGraph' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents 'JRSVBNET grf_co2 As AxGraphLib.AxGraph 
    'CONVERSION_NOTE: Class 'AxGraphLib.AxGraph' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents 'JRSVBNET grf_WGDP As AxGraphLib.AxGraph 
    'CONVERSION_NOTE: Class 'AxGraphLib.AxGraph' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents 'JRSVBNET grf_WPOP As AxGraphLib.AxGraph 
    'CONVERSION_NOTE: Following code was added for support purposes.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1016.html'.
    Protected WithEvents IfsVB60 As System.Diagnostics.Process


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.IfsVB60 = New System.Diagnostics.Process

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_RunPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_Run
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_Run()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  

    '------------------------------------------Run.Frm-----------------------------
    '------------------------------------------------------------------------------
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last revision by BBH on April 28, 2001


    '   Whatever you can do, or dream you can, begin it.  Boldness has genius,
    '   power and magic in it.
    '   Johann Wolfgang von Goethe

    Dim reset_bool As Boolean
    Dim rtim As Short
    '------raman 23rd April-----------
    Public ran_guided_use As Boolean
    'JRS 10/08/02 GUIDED USE
    'this is used to validate that we're really in step 4
    Dim local_Guided_use As Boolean

    '=============================Run.Frm=======================
    Private Sub cmd_Cancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Cancel.Click
        'test jrs 05/20/04
        If cmd_Cancel.Text = "(C)ancel" Then
            Support.FormManager.StateStore(Me)
            'jrs 11/13/04
            Support.FormManager.Show(Me, Session("RunParent"))
        End If

        'gvdb 2024/04/15
        ' add new run module
        If RunManager.Instance.IsRunning(Session.SessionID) Then
            RunManager.Instance.RemoveSession(Session.SessionID)
        End If

        cmd_Cancel.Enabled = False
        cmd_Start.Enabled = True

        'jrs 2015/10/30
        cmd_Cancel.Text = "(C)ancel"
        cmd_Cancel.Enabled = True

        Exit Sub
        'end test jrs 05/20/04

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '----------raman 23rd April-----------
        ran_guided_use = False
        '----------------raman 17th March---------------
        final_year_run = Val(txt_RunYears.Text)
        '-----------------------------------
        'UPGRADE_WARNING: Couldn't resolve default property of object StopRun. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("StopRun") = True
        If cmd_Cancel.Text = "&Cancel" Then

            '--- 'raman feb 02--------
            'go back to step 2 in Guided_use
            If local_Guided_use = True Then
                Me.Visible = False
                'CONVERSION_ISSUE: Property 'IFs.frm_ScenarioCreate' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
            End If
            '-------------------------
            Support.FormManager.Close(Me)
        End If
    End Sub

    '=============================Run.Frm=======================
    Private Sub cmd_Continue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Eliminate variables for display because values have changed
        '   -----------------------------------------------------------

        '    For i2 = 1 To MaxVariables
        '        Session("VARN")$(1, i2) = ""
        '        Session("VARN")$(2, i2) = ""
        '        Session("VARN")$(3, i2) = ""
        '        Session("GR1N")$(i2) = ""
        '        Session("GR2N")$(i2) = ""
        '        Session("GR3N")$(i2) = ""
        '    Next i2
        '    Session("COUNTE") = 0
        '    For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
        '        For ICount% = 1 To MaxVariables
        '            session("PLOT")(i2, ICount%) = 0
        '        Next ICount%
        '    Next i2
        'Session("PercentOn")$ = "False"

        '----------raman 23rd April-------------
        ran_guided_use = True
        '----------------raman 17th March---------------
        final_year_run = Val(txt_RunYears.Text)
        '-----------------------------------

        '--- 'raman feb 02--------
        If local_Guided_use = True Then
            'JRSVBNET frm_GuidedUse6.DefInstance.Visible = True
        End If
        '-------------------------
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        Support.FormManager.Close(Me)

    End Sub

    '=============================Run.Frm=======================
    Private Sub cmd_Reset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Reset.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FirstYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object BeginRun. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        Session("BeginRun") = Session("FirstYear")
        'UPGRADE_WARNING: Couldn't resolve default property of object BeginRun. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        lbl_BeginRun.Text = "The model will begin running in year " & Str(Session("BeginRun"))
        'gvdb 2024/04/17 use client side script
        'cmd_Reset.Visible = False
        cmd_Reset.Visible = True
    End Sub


    '---------------------raman feb '02 ----------------
    Private Sub CmdBack_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBack.Click

        '-----------raman march'02-------------


        'CONVERSION_ISSUE: Property 'IFs.frm_ScenarioCreate' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'UPGRADE_WARNING: Form method frm_ScenarioCreate.ZOrder has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.BringToFront' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.BringToFront().html'.
        'frm_ScenarioCreate.BringToFront() 
        'frm_ScenarioCreate.Visible = False

        '----------------raman 17th March---------------
        final_year_run = Val(txt_RunYears.Text)
        '-----------------------------------

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
    End Sub
    '---------------------raman feb '02 ----------------
    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Dim response As Object
        Dim msg As String
        msg = "Do you wish to exit from Guided Use?"
        'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET response = WinFormsToWeb_MessageBox.Show(msg, 1)
        'JRSVBNET WinFormsToWeb_MessageBox.Show(msg, 1)
        'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If response = 1 Then
            If local_Guided_use = True Then Guided_use = False
            'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            '--------------------raman 17th March------------
            final_year_run = Val(txt_RunYears.Text)
            '-------------------------------------------

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_MainMenu.aspx")
        End If

    End Sub

    '---------------------raman feb '02 ----------------
    Private Sub cmdNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdNext.Click

        'JRSVBNET frm_GuidedUse6.DefInstance.Visible = True
        'UPGRADE_WARNING: Form method frm_GuidedUse6.ZOrder has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'JRSVBNET frm_GuidedUse6.DefInstance.BringToFront()
        'frm_ScenarioCreate.Visible = False
        '----------------raman 17th March---------------
        final_year_run = Val(txt_RunYears.Text)
        '-----------------------------------

        Support.FormManager.Close(Me)

    End Sub
    '---------------------raman feb '02 ----------------
    Private Sub cmdReset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReset.Click
        frm_Run_Load(Me, New System.EventArgs)
        reset_bool = True
    End Sub

    '=============================Run.Frm=======================
    'CONVERSION_NOTE: The code of Event 'frm_Run.frm_Run_Load' was moved to Method 'frm_Run.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_Run_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'If lblRebuildBase.Visible = False Then
        '    PackJS()
        'End If

        'jrs 2013/09/04
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
            ShowHelpJS("Running IFs", ""), True)

    End Sub

    '=============================Run.Frm=======================
    'UPGRADE_WARNING: Form event frm_Run.Unload has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Closed' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Closed.html'.
    Private Sub frm_Run_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '        StopRun = True

        '--- 'raman feb 02--------
        ' If Guided_use = True Then
        '    GuidedUse3.Visible = True
        ' End If
        '-------------------------
        '-----------------raman 17th march----------
        final_year_run = Val(txt_RunYears.Text)
        '----------------------------

        'JRS 01/24/04 Me.Close()
    End Sub



    Public Sub mnuBack_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '-----------raman march'02-------------
        'CONVERSION_ISSUE: Property 'IFs.frm_ScenarioCreate' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
        'UPGRADE_WARNING: Form method frm_ScenarioCreate.ZOrder has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.BringToFront' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.BringToFront().html'.
        'frm_ScenarioCreate.BringToFront() 
        '----------------raman 15th may---------------
        final_year_run = 1995 ' Val(txt_RunYears.Text)
        '-----------------------------------
        Support.FormManager.Close(Me)
    End Sub


    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim response As Object
        Dim msg As String
        '----------------raman 15th may---------------
        msg = "Do you wish to exit from Guided Use?"
        'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET response = WinFormsToWeb_MessageBox.Show(msg, 1)
        'JRSVBNET WinFormsToWeb_MessageBox.Show(msg, 1)
        'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If response = 1 Then
            Guided_use = False
            'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            Session("FlagGroup") = False
            final_year_run = Val(txt_RunYears.Text)
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_MainMenu.aspx")
        End If
    End Sub


    Public Sub mnuNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim dummy_var As Short
        '-----------raman 23rd april--------------
        If ran_guided_use = False Then
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRSVBNET WinFormsToWeb_MessageBox.Show("You have not run the model to compute the results of your scenario. You will now advance to evaluation step (6), but will want to return to this run step (5) before evaluating the results of your scenario choices", MsgBoxStyle.OKOnly)
        End If
        final_year_run = Val(txt_RunYears.Text)

        'JRSVBNET frm_GuidedUse6.DefInstance.Visible = True
        'UPGRADE_WARNING: Form method frm_GuidedUse6.ZOrder has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'JRSVBNET frm_GuidedUse6.DefInstance.BringToFront()
        'frm_ScenarioCreate.Visible = False
        Support.FormManager.Close(Me)

    End Sub


    Public Sub mnuReset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'raman
        cmd_Start.Visible = True
        cmd_Start.Enabled = True
        cmd_Cancel.Visible = True
        frm_Run_Load(Me, New System.EventArgs)
        reset_bool = True
        ran_guided_use = False
    End Sub

    'UPGRADE_WARNING: Event txt_RunYears.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub txt_RunYears_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '    Session("LastYear") = Val(txt_RunYears)
    End Sub

    Private Sub cmd_Start_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_Start.Click
        'jrs 2006/03/23 Validation of Year to Run
        Dim Message As String
        If Not (IsNumeric(txt_RunYears.Text)) Then
            Message = "Use a valid number for Year to Run."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Dim bHist As Boolean = False
        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            If Not (Val(txt_RunYears.Text) >= FirstYearForecast And Val(txt_RunYears.Text) <= Session("LastYearForecast")) Then
                Message = "Year to Run must be between " & FirstYearForecast & " and " & Session("LastYearForecast") & "."
                LoadMessage(Message, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            'jrs 2006/07/21
            Session("FarmFlag") = True
        End If

        If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            bHist = True
            If Not (Val(txt_RunYears.Text) >= FirstYearHistory And Val(txt_RunYears.Text) <= LastYearHistory) Then
                Message = "Year to Run must be between " & FirstYearHistory & " and " & LastYearHistory & "."
                LoadMessage(Message, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            'jrs 2006/07/21
            Session("FarmFlagHist") = True
        End If

        Dim runManager As RunManager = RunManager.Instance

        If runManager.CanStartRun(Session.SessionID) And runManager.PrepareRun(Session.SessionID, DemandFile, txt_RunYears.Text, StandAloneMode, bHist, Session("PutFdrvRun"), Session("SessionIdx"), True, CBool(DevelopmentMode)) Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

            cmd_continue.Text = "Run Successful - Click to Continue"
            cmd_Start.Enabled = False
            cmd_Cancel.Enabled = True
            cmd_Cancel.Text = "Interrupt"
            cmd_Cancel.Visible = True

            If File_Exists(Session("PutFdrvRun") & "progress.txt") Then
                _logger.Debug($"Run - delete file {Session("PutFdrvRun") & "progress.txt"}")
                My.Computer.FileSystem.DeleteFile(Session("PutFdrvRun") & "progress.txt")
            End If

            If File_Exists(Session("PutFdrvRun") & "ErrorSce.txt") Then
                _logger.Debug($"Run - delete file {Session("PutFdrvRun") & "ErrorSce.txt"}")
                My.Computer.FileSystem.DeleteFile(Session("PutFdrvRun") & "ErrorSce.txt")
            End If

            'jrs 2020/08/24 need to reset the base case in case a scenario was previously run
            Dim InFile, OutFile As String
            If Session("mnuSQLite") = "True" Then
                OutFile$ = Session("PutFdrvRun") + "working.run.db"
            Else
                OutFile$ = Session("PutFdrvRun") + "working.run"
            End If

            If Session("HistoricBase") = "False" Then
                If Session("mnuSQLite") = "True" Then
                    InFile$ = Session("PutFdrvRun") + "IFsBase.run.db"
                Else
                    InFile$ = Session("PutFdrvRun") + "IFsBase.run"
                End If
            Else
                If Session("mnuSQLite") = "True" Then
                    InFile = Session("PutFdrvRun") + "IFsBaseHist.run.db"
                Else
                    InFile = Session("PutFdrvRun") + "IFsBaseHist.run"
                End If
            End If
            'jrs 2023/08/01 don't reset when changing TablFunc or AnalFunc
            If Session("ChangingFunctions") = Nothing Then
                FileCopyCheck(InFile, OutFile)
            End If

            'jrs 2005/03/14
            Session("DisplayLastYear") = Val(txt_RunYears.Text)

            'jrs 2023/04/20
            If Not (Session("FileForDisplay0") Is Nothing) Then
                Session("FileForDisplay0").dispose()
            End If

            'jrs 2020/08/28
            Session("FileForDisplay0") = Nothing
            DeleteDFInCache(0, Session)
            'jrs 2023/10/03
            Session("MMVarName") = Nothing

            If runManager.StartRun(Session.SessionID) Then
                lblRebuildBase.Visible = True
                lblRebuildBase.Text = "     Running Model, Please wait for Initialization to finish ..."
                Session("RuningProgress") = True
            Else
                _logger.Error("StartRun failed")
            End If

        Else
            Message = "Other users are running the model, please try later."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Exit Sub
    End Sub

    Private Sub cmd_continue_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_continue.Click
        'gvdb 2024/04/17
        ' client side script
        Session("RuningProgress") = False

        If RebuiltBase Then
            Dim InFile, OutFile As String
            InFile$ = Session("PutFdrvRun") + "working.run.db"
            If Session("HistoricBase") = "False" Then
                If Session("mnuSQLite") = "True" Then
                    OutFile$ = Session("PutFdrvRun") + "IFsBase.run.db"
                Else
                    OutFile$ = Session("PutFdrvRun") + "IFsBase.run"
                End If
            Else
                If Session("mnuSQLite") = "True" Then
                    OutFile$ = Session("PutFdrvRun") + "IFsBaseHist.run.db"
                Else
                    OutFile$ = Session("PutFdrvRun") + "IFsBaseHist.run"
                End If
            End If
            FileCopy(InFile$, OutFile$)
            Session("WorkingFile") = OutFile$
            If Session("mnuSQLite") = "True" Then
                InFile$ = Session("PutFdrvRun") + "SAMWorking.db"
                OutFile$ = PutDataFileDir + "SAMBase.db"
            Else
                InFile$ = Session("PutFdrvRun") + "SAMWorking.mdb"
                OutFile$ = PutDataFileDir + "SAMBase.mdb"
            End If
            FileCopy(InFile$, OutFile$)
            If Session("mnuSQLite") = "True" Then
                InFile$ = Session("PutFdrvRun") + "IFsPopWork.db" 'Also copy cohort population
            Else
                InFile$ = Session("PutFdrvRun") + "IFsPopWork.mdb"
            End If
            If Session("HistoricBase") = "False" Then
                If Session("mnuSQLite") = "True" Then
                    OutFile$ = PutDataFileDir$ + "IFsPop.db"
                Else
                    OutFile$ = PutDataFileDir$ + "IFsPop.mdb"
                End If
            Else
                If Session("mnuSQLite") = "True" Then
                    OutFile$ = PutDataFileDir$ + "IFsPopHist.db"
                Else
                    OutFile$ = PutDataFileDir$ + "IFsPopHist.mdb"
                End If
            End If
            FileCopy(InFile$, OutFile$)  'Will change base when restart model
            'jrs 2007/06/05
            If Session("HistoricBase") = "False" Then
                If Session("mnuSQLite") = "True" Then
                    OutFile$ = Session("PutFdrvRun") + "IFsPop.db"  'copy also to runfiles to change
                Else
                    OutFile$ = Session("PutFdrvRun") + "IFsPop.mdb"
                End If
            Else
                If Session("mnuSQLite") = "True" Then
                    OutFile$ = Session("PutFdrvRun") + "IFsPopHist.db"
                Else
                    OutFile$ = Session("PutFdrvRun") + "IFsPopHist.mdb"
                End If 'copy also to runfiles to change
            End If
            'base immediately
            FileCopy(InFile$, OutFile$)
            '10/16/02 SAM COPY
            'jrs 2007/01/03 move SAMBase.mdb to DATAFileCopy PutFdrvRun$ + "SAMWorking.mdb", PutDataFileDir$ + "SAMBase.mdb"

            'jrs 2007/06/05 End If

            'jrs 2008/11/20
            'Save Smoking Forecast
            If Session("HistoricBase") = "False" Then
                If Session("mnuSQLite") = "True" Then
                    InFile$ = Session("PutFdrvRun") + "IFsSmoking.db"
                    OutFile$ = PutDataFileDir$ + "IFsSmoking.db"
                Else
                    InFile$ = Session("PutFdrvRun") + "IFsSmoking.mdb"
                    OutFile$ = PutDataFileDir$ + "IFsSmoking.mdb"
                End If
                FileCopy(InFile$, OutFile$)
            End If

            'jrs 2019/05/03 it was only for forecast, now also for historic
            'jrs 2016/09/20
            InFile$ = Session("PutFdrvRun") + "IFsForDyadicWork.db"
            OutFile$ = PutDataFileDir$ + "IFsForDyadic.db"
            If Dir(InFile$) <> "" Then
                FileCopy(InFile$, OutFile$)
            End If

            lblRebuildBase.Visible = True
            lblRebuildBase.Text = "     You have now saved a changed or new basecase."
            RebuiltBase = False
            'jrs 2024/05/27
            Session("RebuiltBase") = RebuiltBase

            'Reset all dataframes from Working file
            For i = 1 To NumbVars
                Session("df" & V(i) & "1") = Nothing
                'jrs 2023/08/08
                Session("df" & V(i) & "0") = Nothing
            Next

            'jrs 2023/06/08
            'Reset file
            Session("FileForDisplay0") = Nothing
            Session("FileForDisplay1") = Nothing
            Session("FilesCol") = Nothing
            Session("dfCol") = Nothing
        Else
            lblRebuildBase.Visible = False
        End If

        'jrs 2020/05/14
        'Reset all dataframes from Working file
        For i = 1 To NumbVars
            Session("df" & V(i) & "0") = Nothing
        Next
        'jrs 2023/06/08 
        Session("dfCol") = Nothing

        'jrs 2023/06/08
        'Reset file Session("FilesCol")
        Session("FileForDisplay0") = Nothing
        Session("FilesCol") = Nothing

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub

End Class
