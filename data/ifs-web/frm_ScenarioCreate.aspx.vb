Option Strict Off
Option Explicit On
Imports Microsoft.Web.UI.WebControls
Imports VB = Microsoft.VisualBasic
Imports System.Data.SQLite
Imports Microsoft.VisualBasic.Compatibility
Imports Microsoft.Ajax.Utilities
Imports IFs.Data

Partial Class frm_ScenarioCreate
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'Panel1.Visible = True
        'Panel2.Visible = True
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
        If Not Page.IsPostBack Then
            'jrs 2011/10/13
            If Session("SessionIdx") = -1 Then
                SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
            Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
            Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
            Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
            Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
            Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
            Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))
            End If

            'jrs 2005/05/16 changed First Year to First Year Forecast, if History plus Forecast
            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                    Session("FirstYear") = FirstYearForecast
                    ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                    Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                End If
            End If

            Dim msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'JRS 11/04/03
            bNextDirty = False

            '   Set screeen
            '   -----------

            '========raman march '02 ============
            If reset_bool = False Then
                'CONVERSION_ISSUE: Class System.Windows.Forms.FormWindowState from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.WindowState.html'.
                'JRSVBNET WindowState = System.Windows.Forms.FormWindowState.Maximized
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
                'JRSVBNET Width = VB6.TwipsToPixelsX(Inch * 6)
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
                'JRSVBNET Height = VB6.TwipsToPixelsY(Inch * 3.4)
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Top.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
                'JRSVBNET Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2)
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Left.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
                'JRSVBNET Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)
            End If

            '   Display Scenario instructions
            '   --------------------
            reset_bool = False
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Carret = "<br>" & Chr(10)
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            carret2 = Carret + Carret
            msg = "Typical steps in scenario development:" & carret2
            msg = msg & "     1.  Select an issue area from the tree to the left." & carret2
            msg = msg & "     2.  Add or delete drivers and regions (use the menu "
            msg = msg & "option and follow instructions)" & carret2
            msg = msg & "     3.  Repeat issue and driver/region selection to "
            msg = msg & "build your scenario." & carret2
            msg = msg & "     4.  Save your scenario." & carret2
            msg = msg & "     5.  Run your scenario to recompute all model values."
            msg = msg & carret2
            msg = msg & carret2
            lbl_hint.Text = msg
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmbShiftYears.Items.Clear()
            'mti August 2004
            cmbShiftYears.Items.Add(New ListItem(""))
            For i2 = 1 To 50 '1 To 50
                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                cmbShiftYears.Items.Add(New ListItem(CStr(i2)))
            Next i2
            Session("cur_file") = "Untitled"
            Call InitTree()
            'initialize variables
            Call InitVars()
            'mtiwebforms April 2004 ADO.Net
            'dbGroup = DAODBEngine_definst.OpenDatabase(PutFdrv & "RUNFILES\IFSVar.MDB")
            dbGroup = Session("FdrvRun") & "IFSVar.MDB"
            'SetDataTablefromAccess(PutFdrv & "RUNFILES\IFSVar.MDB", ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable)
            'Set rsGroup = dbGroup.OpenRecordset("GroupScenario")

            'Changes MTI 03 July,2002
            BackFromAnnotation = False
            'Changes MTI 03 July,2002


            'JRS 10/08/02 GUIDED USE
            'this is used to validate that we're really in step 4
            local_Guided_use = False
            Dim k As Short
            'mti ScenarioCreate May 2004 uncomment later for guided use
            'If Guided_use Then 
            '	'UPGRADE_ISSUE: Forms collection was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
            '	'UPGRADE_WARNING: Couldn't resolve default property of object Forms.Count. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            '	'JRSVBNET **** BEGIN ****
            '	'For k = 0 To Forms.Count - 1
            '	'    'UPGRADE_ISSUE: Forms collection was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
            '	'    'UPGRADE_WARNING: Couldn't resolve default property of object Forms(k).Name. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            '	'    If Forms(k).Name = "frm_GuidedUse3" Or Forms(k).Name = "frm_GuidedUse2" Then
            '	'        local_Guided_use = True
            '	'        Exit For
            '	'    End If
            '	'Next k
            '	'JRSVBNET **** END ****
            'End If 
            ''***************************************************
            ''JRS 07/29/02 GUIDEDUSE
            'mnuReset.Visible = local_Guided_use
            'mnuBack.Visible = local_Guided_use
            'mnuNext.Visible = local_Guided_use
            'If local_Guided_use Then
            '	WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Step 4 - Scenario Development and Change"
            '	'------------------raman april 29th------
            '	first_time = True
            '	'JRS 5/25/02
            '	If FlagGroup = True Then mnuGroups.Text = "&Use Countries"
            '	'JRS 04/24/03
            '	mnuMainMenu.Text = "E&xit Guided Use"
            'End If

            'mti WebForm Changes From June 21, 2004  - start
            Session("fpPurpose") = ""
            'mti WebForm Changes From June 21, 2004 -end
            'ScenarioFileDir & "Working.Sce"

            'jrs 2014/12/15 
            If IsNothing(Session("FlagGroup")) Then
                'jrs 11/15/04
                Session("FlagGroup") = False
            End If
            'jrs 2005/05/09
            If Session("LoadSceDesc") = True Then
                FileCopy(Session("ScenarioFileDir") & "Working2.txt", Session("ScenarioFileDir") & "Working.txt")
                Session("LoadSceDesc") = False
            End If
            'jrs 2021/10/18
            If IsNothing(Session("GRPSLoc")) Then
                GRPSLoc = GRPS.Clone
                Session("GRPSLoc") = GRPSLoc
            Else
                GRPSLoc = Session("GRPSLoc")
            End If

            frm_ScenarioCreate_Activated(Me, New System.EventArgs)

            'end mti ScenarioCreate May 2004 

            'jrs 11/13/04
            Session("RunParent") = "frm_ScenarioCreate.aspx"
            'jrs 2011/10/07
            Session("rgYear") = Nothing
            'jrs 2011/11/22
            Session("MsgParent") = Nothing
            'jrs 2013/09/11
            If Session("ScenarioName") = Nothing Then
                Session("ScenarioName") = "Working"
            End If

        Else


            'JRS 03/03/04
            SaveVCat = Session("SaveVCat")
            DimNum = Session("DimNum")
            SubDimNum = Session("SubDimNum")
            SubDimDone = Session("SubDimDone")
            VPTR = Session("VPTR")
            SEL1 = Session("SEL1") 'JRS 2006/07/21
            'JRS 08/20/04
            Totcat = Session("ScenCrTotcat")
            'jrs 2005/05/13
            GMAX1 = Session("GMAX1")
            GMAX2 = Session("GMAX2")
            sDim2 = Session("sDim2")
            'jrs 2021/10/18
            If IsNothing(Session("GRPSLoc")) Then
                GRPSLoc = GRPS.Clone
                Session("GRPSLoc") = GRPSLoc
            Else
                GRPSLoc = Session("GRPSLoc")
            End If

        End If
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")

        'jrs 2017/01/04
        If Dir(Session("PutFdrvRun") & "IFsForDyadicWork.db") <> "" Then
            If Session("orgMaxVars") = Nothing Then
                Session("orgMaxVars") = NumbVars

                Dim dbBT As String
                dbBT = Session("PutFdrvRun") & "IFsForDyadicWork.db;"
                i2 = NumbVars

                Dim vSQLiteCommand As SQLiteCommand
                Dim vSQLiteCon As SQLiteConnection
                Dim vSQLiteReader As SQLiteDataReader
                Dim vConnString As String
                vConnString = "DataSource=" + dbBT
                vSQLiteCon = New SQLiteConnection(vConnString)
                vSQLiteCon.Open()

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM ParametersDefault ORDER BY Description"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                While (vSQLiteReader.Read())
                    NumbVars = NumbVars + 1

                    ReDim Preserve VScenCat(NumbVars)
                    ReDim Preserve VScenario(NumbVars)
                    ReDim Preserve V(NumbVars)
                    ReDim Preserve VDim1(NumbVars)
                    ReDim Preserve VDim2(NumbVars)
                    ReDim Preserve VDim3(NumbVars)
                    ReDim Preserve VNoTot2(NumbVars)
                    ReDim Preserve VDef(NumbVars)
                    ReDim Preserve VHelpID(NumbVars)
                    ReDim Preserve VHelpIDEqn(NumbVars) 'jrs 2021/02/05
                    ReDim Preserve VUnits(NumbVars)
                    ReDim Preserve VarPoint(NumbVars)
                    ReDim Preserve VShiftUp(NumbVars)
                    ReDim Preserve VShiftDown(NumbVars)
                    ReDim Preserve VShiftYears(NumbVars)
                    ReDim Preserve VType(NumbVars)
                    ReDim Preserve VMax(NumbVars)
                    ReDim Preserve VMin(NumbVars)
                    ReDim Preserve VExpl(NumbVars)
                    'jrs 2019/08/07
                    ReDim Preserve VHidePrivateVars(NumbVars)
                    'jrs 2020/12/08
                    ReDim Preserve VCurrency(NumbVars)
                    'jrs 2021/04/22
                    ReDim Preserve VHistoricAnalog(NumbVars)

                    i2 = i2 + 1
                    VScenCat(i2) = Session("BilateralCat")
                    VScenario(i2) = vSQLiteReader("Description")
                    VDef(i2) = vSQLiteReader("Description")
                    VExpl(i2) = vSQLiteReader("Description")
                    V(i2) = vSQLiteReader("ParameterName")
                    VDim1(i2) = 1
                    VDim2(i2) = 1
                    'jrs 2022/01/18 added andalso vSQLiteReader("Dim3")<>""
                    'jrs 2018/01/06
                    If Not IsDBNull(vSQLiteReader("Dim3")) AndAlso IsNumeric(vSQLiteReader("Dim3")) Then
                        VDim3(i2) = vSQLiteReader("Dim3")
                    Else
                        VDim3(i2) = 0
                    End If
                    VNoTot2(i2) = True 'jrs 2022/06/15 hide total for these parameters False
                    VShiftUp(i2) = 0
                    VShiftDown(i2) = 0
                    VShiftYears(i2) = 0
                    VType(i2) = 3 '2 is only initial conditions, doesn't allow setting a year to start the intervention
                    VarPoint(i2) = vSQLiteReader("DefaultValue")
                    VMax(i2) = 200 'jrs 2018/07/06 increased from 100 to 200
                    VMin(i2) = -200 'jrs 2018/07/06 increased from 100 to 200
                    VCurrency(i2) = False 'jrs 2020/12/08
                    IfsVars.Add(V(i2), i2)
                End While

                vSQLiteCon.Close()
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
    'CONVERSION_ISSUE: Class 'System.Windows.Forms.VScrollBar' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'CONVERSION_NOTE: Class 'AxMSComDlg.AxCommonDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents cmnDlg1 As AxMSComDlg.AxCommonDialog 
    'CONVERSION_NOTE: Class 'AxGraphLib.AxGraph' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents lblJS As AxGraphLib.AxGraph 
    Public Opt_highlow As System.Collections.ArrayList
    'Public mnuMainMenu As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuNew As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuOpen As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuAdd As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuSave As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuDash1 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuPrint As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuscenario As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuDelete As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuGroups As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuNote As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuReset As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuBack As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuNext As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuOther As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuPop As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    '   Protected WithEvents MainMenu1 As ArtinSoft.WinFormsToWeb.WebControls.MainMenu
    '   Protected WithEvents ContextMenu1 As System.Web.UI.WebControls.Label
    '   'CONVERSION_ISSUE: Class System.Windows.Forms.ContextMenu from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '   Friend MenuItem2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Friend MenuItem4 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Friend MenuItem6 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Friend MenuItem8 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Friend mnuSelect As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Friend mnuDrivers As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Friend mnuExplain As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Friend mnuViewEquations As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Friend mnuDefine As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'CONVERSION_NOTE: Following code was added for support purposes.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1016.html'.
    Protected WithEvents FilePanel As System.Web.UI.WebControls.Panel
    Protected WithEvents LBLSLIDERVAL As System.Web.UI.WebControls.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Sub ControlArrayInit()
        Me.Opt_highlow = New System.Collections.ArrayList
        Me.Opt_highlow.Add(Me._Opt_highlow_0)
        Me.Opt_highlow.Add(Me._Opt_highlow_1)
        Me.Opt_highlow.Add(Me._Opt_highlow_2)
        AddHandler CType(Opt_highlow.Item(2), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Opt_highlow_CheckedChanged
        AddHandler CType(Opt_highlow.Item(1), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Opt_highlow_CheckedChanged
        AddHandler CType(Opt_highlow.Item(0), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Opt_highlow_CheckedChanged
    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_ScenarioCreatePrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_ScenarioCreate
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_ScenarioCreate()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  

    '------------------ScenarioCreate.Frm-------------------------------------
    '------------------------------------------------------------------------------------------
    'Start- The following Comments are for merging code from Mohammod - 2 July, 2002
    '------------------------------------------------------------------------------------------
    'Please delete these comments after merging

    ' A. Code Update Search String: 'Scenario Changes MTI

    ' In all but one case:whole procedure is to be replaced(as commented)
    ' In MnuDelete_Click procedure, please replace the marked part only
    '
    '--End-- The following Comments are for merging code from Mohammod
    '------------------------------------------------------------------------------------------


    '------------------ScenarioCreate.Frm-------------------------------------
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last changed by BBH on November 18, 2001


    'Changes MTI 03 July,2002
    Dim BackFromAnnotation As Boolean
    'Changes MTI 03 July,2002
    'mti 19/09

    Dim blnSettingSlider As Boolean
    Dim blnSettingYears As Boolean

    Dim lkey As Short

    Dim SaveCancelled As Boolean

    Dim Totcat As Short
    Dim chk As Object

    Dim ValStr, Scenariotype As String 'don't need scenariotype
    'take this to declare2
    'Dim Session("LinePrefix")()  'CustomStr()
    'JRS 08/13/03
    Public Curnode As Microsoft.Web.UI.WebControls.TreeNode

    'take this in declarations
    'JRS 10/30/03
    Public blnScenarioDirty As Boolean

    Dim CurYear As Short

    Dim blnFromSlider As Boolean

    Public CurNodeNo As String

    Dim DriveNodeno As Short
    Dim ValMult As Object

    'Dim FlagGroup As Boolean ' declared global in module VAR for generalization in RDA's work
    Dim VPTR, GMAX1 As Object, GMAX2 As Integer 'JRS 2006/05/31 DIM3
    Dim eleloop As Short
    Dim i2 As Short
    Dim SubDimDone, Code, SEL1 As String
    Dim SubDimNum As Short 'number of subdimensions
    Dim DimNum As Short 'latest (and possibly 1st) subdimension number
    Dim SDimNum As Short 'first subdimension number where more than 1 dimension
    Dim DimPoint As Short 'latest (and possibly 1st) subdimension pointer (i.e. element #)
    Dim SDimPoint As Short 'first subdimension pointer (i.e, element #) when more than 1 dimension
    Dim GroupPoint As Short 'pointer to group number when regionalized variable is to be grouped
    Dim SaveDim1 As String
    Dim Label2, VarPointer, Label1, Label3 As Object
    Dim SeqOn, SeqRepeat As Object
    Dim groupon As Boolean 'If true, are running sums across all regions in groups
    Dim GroupDone As Boolean 'If true, have finished group and ready to finalize aggregation
    Private Enum ObjectType
        otNone = 0
        otBroadCategory = 1
        otIssueArea = 2
        OtDriver = 3
        Otregion = 4
        otTop = 5
    End Enum


    Const MEDIUMCOLOR As Short = 2 'QBcolor(2) = Green
    Const HIGHCOLOR As Short = 4 'Red
    Const LOWCOLOR As Short = 1 'Blue


    Private SourceNode As Object
    Private SourceType As ObjectType
    Private Targetnode As Object
    Dim AddName As String
    Dim ICount As Short
    Dim SaveVCat As Short

    Dim OFColor As Integer
    Dim NColor As Short

    Dim CountryGroup(250) As String
    Public GroupTot As Short
    Public GroupSel As String

    'mtiwebforms April 2004 ADO.Net
    'Dim dbGroup As DAO.Database
    'Dim rsGroup As DAO.Recordset
    Public dbGroup As String  'DAO.Database
    Public rsGroup As New DataTable 'DAO.Recordset


    Dim GroupNum As Short
    'Raman
    Dim reset_bool As Boolean
    'JRS 10/08/02 GUIDED USE
    'this is used to validate that we're really in step 4
    Dim local_Guided_use As Boolean
    'JRS 11/04/04
    Dim bNextDirty As Boolean
    'jrs 2006/05/30 dim3
    Dim sDim2 As String
    'jrs 2021/10/18
    Dim GRPSLoc As String(,)

    '=============================ScenarioCreate.Frm=============================
    Private Function NodeType(ByRef test_node As Microsoft.Web.UI.WebControls.TreeNode) As ObjectType
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If test_node Is Nothing Then
            NodeType = ObjectType.otNone
        Else
            Select Case VB.Left(test_node.NodeData, 1)
                Case "t"
                    NodeType = ObjectType.otTop
                Case "b"
                    NodeType = ObjectType.otBroadCategory
                Case "i"
                    NodeType = ObjectType.otIssueArea
                Case "d"
                    NodeType = ObjectType.OtDriver
                Case "r"
                    NodeType = ObjectType.Otregion
            End Select
        End If
    End Function
    '=============================ScenarioCreate.Frm=============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ComboBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.KeyPress.html'.
    Private Sub cmb_dim1_KeyPress()
        'JRSVBNET *** BEGIN ***
        ''CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'lkey = KeyAscii
        'If KeyAscii = 27 Then
        '    frm_dim1.Visible = False
        '    If NodeType(Curnode) = ObjectType.otIssueArea Then
        '        frm_options.Visible = False
        '        'JRSVBNET lblJS.Visible = False
        '        VSc_slider.Visible = False
        '        LBLSLIDERVAL.Visible = False
        '        Frm_Custom.Visible = False
        '        frm_time.Visible = False
        '        'Mohammod 08 October, 2001
        '        Frm_nonscenario.Visible = False


        '        lbl_hint.Visible = True
        '    End If
        'End If
        'If KeyAscii = 13 Then Call cmb_Dim1_SelectedIndexChanged(cmb_dim1, New System.EventArgs)
        'If KeyAscii >= 97 And KeyAscii <= 146 Then
        '    'UPGRADE_ISSUE: Assignment not supported: KeyAscii to a non-zero value Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1058"'
        '    KeyAscii = KeyAscii - 32
        'End If
        'If KeyAscii = 0 Then
        '    'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '    EventArgs.Handled = True
        'End If
        'JRSVBNET *** END ***
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub cmbShiftYears_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbShiftYears.SelectedIndexChanged
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mtiwebformsMarch2004 (2)ScenarioCreate
        If blnSettingYears Then
            Exit Sub
        End If

        Dim SliderVal As Object
        Dim k As Short


        'JRS 03/09/04        k = CurNodeNo 'node pointer
        'k = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)

        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        'Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)

        k = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Scenariotype = Session("LinePrefix")(k)

        Select Case Mid(Scenariotype, 1, 3)

            Case "HIG"
                Call Prepare_high()


            Case "LOW"
                Call Prepare_low()


            Case "SLI"
                'mti Scenario Slider May 2004
                Call btnSlider_Click(eventSender, eventArgs)
                'Call Prepare_Slider() 'VSc_slider_Change(0)


            Case Else

                LoadMessage("Shift Years not allowed with current option.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

        End Select
        'mti WebForm Changes From June 26, 2004
        Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")


    End Sub

    '=============================ScenarioCreate.Frm=============================
    Private Sub cmdCustom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCustom.Click
        Dim j As Short 'Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        On Error GoTo ErrorHandling

        'mti September 2004 2 
        Dim TScenPref As String

        TScenPref = Session("LinePrefix")(TreeIndexToFlatIndex(treeScenario.Nodes, treeScenario.SelectedNodeIndex.ToString))

        If TScenPref = "" Then Exit Sub
        'mti September 2004 2 -end 


        'mtiwebformsMarch2004 (2)ScenarioCreate
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        j = Session("Scenario_CurNodeFlatIndex")

        'jrs 2014/10/07
        Session("sSaveLine") = Session("LinePrefix")(j)

        Session("LinePrefix")(j) = "CUSTOM," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
        'frm_ChangeValues.DefInstance.ShowDialog() 'modal

        'mti August 2004 - 2

        Session("Scenario_PlotCol") = Session("Node2Plot")(j)
        Session("Scenario_CurNodeIndex") = Curnode.GetNodeIndex

        'jrs 2008/07/07 Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
        'it was changing the pointer of what parameter is changed

        Session("Changing_Custom_Scenario") = True
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ChangeValues.aspx")

        'Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
        'Session("cur_file") = "Working.Sce"

        'blnScenarioDirty = True 'Reset after InitVars
        'Call Prepare_Custom()
        Exit Sub
        'JRS 08/07/02 ERROR HANDLING
ErrorHandling:
        LoadMessage("Unexpected error found, please report it to your software provider", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

        'mti ScenarioCreate May 2004 Uncomment later
        'mnuReset_Click(mnuReset, New System.EventArgs)
    End Sub

    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_WARNING: Form event frm_ScenarioCreate.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Activated' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Activated.html'.
    Private Sub frm_ScenarioCreate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'Changes MTI 03 July,2002
        If BackFromAnnotation Then
            BackFromAnnotation = False
            Exit Sub
        End If
        'Changes MTI 03 July,2002

        'mti August 2004 2 -start
        If Not Session("Changing_Custom_Scenario") Then
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
                Session("cur_file") = "Working.Sce"
                Call InitTree()
                Call InitVars() 'initializes blnscenariodirty
                Session("cur_file") = "Working.Sce"
                blnScenarioDirty = True 'Reset after InitVars
                Call BuildDrivers(Session("ScenarioFileDir") & "Working.Sce")
                'mti WebForm Changes From July 04, 2004
                Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")

                'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
                'mti WebForm Changes From June 26, 2004
                treeScenario.SelectedNodeIndex = 0 '1
                'treeScenario.Nodes.Item(1).Selected = True 
                Curnode = treeScenario.Nodes.Item(0) '1
            Else
                Session("cur_file") = "Untitled"
                Call InitTree()
                Call InitVars()
            End If
            Session("Scenarioflag") = True
        Else
            If Session("Changing_Custom_Scenario") Then
                Session("Changing_Custom_Scenario") = False

                Call BuildDrivers(Session("ScenarioFileDir") & "Working.Sce")

                If Session("ChangesMade") Then
                    Call SaveScenario(Session("ScenarioFileDir") + "Working.Sce", "Working.Sce")
                    Session("Cur_File") = "Working.Sce"

                    blnScenarioDirty = True 'Reset after InitVars
                    Call Prepare_Custom()
                Else
                    Dim j As Short
                    j = Session("Scenario_CurNodeFlatIndex")
                    Session("LinePrefix")(j) = Session("sSaveLine")
                End If

                'jrs 2023/06/09 start need to remove variable, and dimensions from string
                Dim sSaveLine As String
                sSaveLine = Session("sSaveLine")
                sSaveLine = sSaveLine.Replace("MEDIUM,", "")
                sSaveLine = sSaveLine.Replace("HIGH,", "")
                sSaveLine = sSaveLine.Replace("LOW,", "")
                sSaveLine = sSaveLine.Replace("CUSTOM,", "")
                'jrs 2023/06/26
                If Session("ChVal_CustStr") > "" Then
                    Session("ChVal_CustStr") = Session("ChVal_CustStr").Replace("MEDIUM,", "")
                    Session("ChVal_CustStr") = Session("ChVal_CustStr").Replace("HIGH,", "")
                    Session("ChVal_CustStr") = Session("ChVal_CustStr").Replace("LOW,", "")
                    Session("ChVal_CustStr") = Session("ChVal_CustStr").Replace("CUSTOM,", "")
                    Session("ChVal_CustStr") = Session("ChVal_CustStr").Replace(sSaveLine & ",", "")
                    'jrs 2023/06/09 end
                    'jrs 2023/10/26 changed from Session("Scenario_PlotCol") to Session("Scenario_PlotCol_Last")
                    ParseCustomStr(Session("ChVal_CustStr"), Session("Scenario_PlotCol_Last"), Session("SceLag"))
                End If
                Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
                CurNodeNo = Session("Scenario_CurNodeIndex") 'SCurnodeNo
                Curnode = Support.TreeViewManager.GetNode(treeScenario.Nodes, CurNodeNo)
                Support.TreeViewManager.SetSelectedNode(treeScenario, Curnode)

                Repaint_Right()
                'Call Prepare_Custom()
                Session("ChVal_CustStr") = ""
            End If
            'mti August 2004 2- end

        End If
        'JRS 07/29/02 GUIDEDUSE
        'JRS 10/02/02 GUIDED USE
        'mti ScenarioCreate May 2004 uncomment for guided use
        'mnuReset.Visible = local_Guided_use
        'mnuBack.Visible = local_Guided_use
        'mnuNext.Visible = local_Guided_use
        'end mti ScenarioCreate May 2004 uncomment for guided use

    End Sub
    '=============================ScenarioCreate.Frm=============================
    'CONVERSION_NOTE: The code of Event 'frm_ScenarioCreate.frm_ScenarioCreate_Load' was moved to Method 'frm_ScenarioCreate.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_ScenarioCreate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'jrs 2023/07/04
        LoadMenu()

        'mti Augut 2004 3
        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item
        'If Not Page.IsPostBack Then
        'Panel1.Visible = True
        'Panel2.Visible = True
        'Panel3.Visible = True
        'main menu using WebMenu

        'context menu using WebMenu
        'End If 'mti Augut 2004 3 ading If page.isposback
        'mti WebForm Changes to be taken by Jose
        wmContext.UserData = "jsolorza::976739975"

        wmContext.ClearPixelImage = "./images/blank.gif"
        wmContext.PopupIcon = "./images/Popup.gif"
        wmContext.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmContext.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmContext.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmContext.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmContext.SelectedPopupIcon = "./images/SelectedPopup.gif"
        wmContext.ScrollEnabled = True
        'wmContext.Visible = False
        'jrs 2012/01/17
        wmContext.AccessorKeyCode = 27
        wmContext.ButtonFont.Size = "12px"
        wmContext.ItemFont.Size = "12px"

        'Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmContext.Groups.Add()
        cswmGroup1.ID = "Group_0bd4"

        'Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select"

        AddHandler wmContext.ServerItemClick, AddressOf ItemClickHandler
        'cswmItem.OnClick = "mnuSelect_Click"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "New Item"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Drivers"
        cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "New Item"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'mti August 2004
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain (Pop-Up)"
        cswmItem.OnClick() = "showExplain()"
        'cswmItem.OnMouseOver = "showExplain()"
        'cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "New Item"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'mti August 2004
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "View Equations (Pop-Up)"
        cswmItem.OnClick = "showEquation()"
        'cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "New Item"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Define"

        'mti September 2004 2
        _Opt_highlow_0.Enabled = True
        _Opt_highlow_1.Enabled = True
        _Opt_highlow_2.Enabled = True
        cmdCustom.Enabled = True
        btnSlider.Enabled = True
        'mti September 2004 2 end 

        'txtmax.Visible = False
        'txtmin.Visible = False
        'PegoNet1.Style.Item("TOP") = 0
        'mti WebForm Changes From June 26, 2004
        'lst_varname.Attributes.Add("onclick", "cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);")
        ' cmb_dim1.Attributes.Add("onchange", "showhg();")

        '        lst_varname.Attributes.Add("onclick", "cswmShow('Group_0bd4', 'lst_varname', 'below', 400, 0, false);")
        'lst_varname.Attributes.Add("onclick", "ShowContextMenu;")
        'lst_varname.Attributes.Add("onclick", "cswmShow('Group_0bd4', 'lst_varname', 'above', 10, 10, false);")

        'jrs 2005/07/01
        If Not IsNothing(Session("LoadSearchIdx")) Then
            LoadSearch(Session("LoadSearchIdx"))
            Session("LoadSearchIdx") = Nothing
        End If

        PackJS()

        'jrs 2015/10/30
        ''jrs 2013/06/25 Tree not loading in IE10, force compatibility mode
        'Try
        '    Response.AddHeader("X-UA-Compatible", "IE=5")
        'Catch ex As Exception

        'End Try

        'jrs 2021/09/14
        If Not Page.IsPostBack Then
            WriteFormControlCaption(Me, Session("PutFdrvRun"), sLanguage)
        End If
    End Sub

    Private Sub LoadMenu()
        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        'mti WebForm Changes to be taken by Jose
        wmMain.UserData = "jsolorza::976739975"
        wmMain.ClearPixelImage = "./images/blank.gif"
        wmMain.PopupIcon = "./images/Popup.gif"
        wmMain.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmMain.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmMain.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmMain.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmMain.SelectedPopupIcon = "./images/SelectedPopup.gif"
        wmMain.ScrollEnabled = True
        wmMain.MenuBar.ShowOnMouseOver = True
        wmMain.GeneratedPrefix = "wmMain"

        'jrs 2012/01/17
        wmMain.AccessorKeyCode = 27
        wmMain.ButtonFont.Size = "12px"
        wmMain.ItemFont.Size = "12px"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_bc61"

        'jrs 2011/10/07
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Scenario Files"
        cswmGroup1.ID = "Group_fb2c"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Clear Tree"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Open"

        'jrs 2005/04/06
        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()

        Dim i As Integer
        For i = 0 To UBound(LongSce)
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = LongSce(i)
            cswmItem.ID = "ADD"
        Next

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Other"

        'jrs 2006/11/02 removing file picker
        Dim cswmGroup3 As Coalesys.WebMenu.Group
        cswmGroup3 = cswmItem.AddGroup
        Dim sDirSess As String = "\Session"
        If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
            sDirSess = sDirSess & "0"
        End If
        sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sDirName As String
        sDirName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text)
        LoadMenuFiles(sDirName, cswmItem, cswmGroup3, "sce", 0, "") 'JRS 2005/11/14 ADDED 0 'jrs 2022/01/14 removed "ADD"

        'jrs 2006/11/08 removing FilePicker
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Add"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Name and Save"
        'jrs 2005/05/24 cswmItem.Enabled = False

        'jrs 2024/08/19
        If StandAloneMode Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Export to IFs Standard CSV Format"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Export Working File"

        'jrs 2024/08/21 *** start ***
        If StandAloneMode Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Imprt IFs Standard CSV Format files"

            Dim cswmGroup4 As Coalesys.WebMenu.Group
            cswmGroup4 = cswmItem.AddGroup
            LoadMenuFiles(sDirName, cswmItem, cswmGroup4, "csv", 0, "IMP")
        End If
        'jrs 2024/08/21 *** end ***

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "."
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2005/04/28
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "."
        'cswmItem.DividerColor = "#ffffff"
        'cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Print"

        'jrs 2005/04/07
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Add Scenario Component"
        LoadMenuFiles(sDirName, cswmItem, cswmGroup1, "sce", 0, "ADD") 'jrs 2022/01/14 added "ADD" 'JRS 2005/11/14 ADDED 0

        'jrs 2011/10/01
        If Session("QuickScenario") Is Nothing Then
            Session("QuickScenario") = True
        End If

        'JRS 08/20/04
        If Session("QuickScenario") Then
            cswmGroup1 = wmMain.Groups.Add()
            cswmGroup1.Caption = "Run Scenario"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Run"
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Delete Selection"
        cswmGroup1.ID = "Group_39f7"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Delete"




        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Set Group or Country"
        cswmGroup1.ID = "Group_78c2"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Groups"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'mti Augut 2004 3 -start
        'cswmItem.IconImage = "blank.gif"
        If Session("FlagGroup") = True Then
            cswmItem.IconImage = "./images/checkbox.gif"
        Else
            cswmItem.IconImage = "./images/blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Countries"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"
        If Session("FlagGroup") = True Then
            cswmItem.IconImage = "./images/blank.gif"
        Else
            cswmItem.IconImage = "./images/checkbox.gif"
        End If
        'mti Augut 2004 3 end

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Annotate Scenario"
        cswmGroup1.ID = "Group_b78d"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Annotate"
        'cswmItem.OnClick()

        'jrs 2005/07/01
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Parameter Search"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Search"

        'jrs 2005/06/20
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Help"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Contents"
        'jrs 09/17/04 cswmItem.OnMouseOver = "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT, 0);"
        'jrs 2013/09/04 cswmItem.OnClick = "showHelp()"
        cswmItem.OnClick = LoadHelpJS("Scenario Development and Change", "")

        'jrs 2006/12/21 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16040);"

        AddHandler wmMain.ServerItemClick, AddressOf ItemClickHandlerMain

        'mti Augut 2004 3 - fron Jose mainmenu
        wmMain.MenuBar.AbsoluteDockEnabled = False
        wmMain.MenuBar.AbsoluteDragEnabled = False
        wmMain.MenuBar.BorderSize = 1
        wmMain.MenuBar.OuterHighlightColor = "#ffffff"
        wmMain.MenuBar.OuterShadowColor = "#808080"
        wmMain.MenuBar.SelectedColor = "navy"
        wmMain.MenuBar.SelectedHighlightColor = "navy"
        wmMain.MenuBar.SelectedShadowColor = "navy"
        wmMain.MenuBar.SelectedTextColor = "white"
        wmMain.ButtonFont.PaddingLeft = 10
        wmMain.ButtonFont.PaddingRight = 10
        wmMain.ItemFont.PaddingLeft = 4
        'mti Augut 2004 3

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

        'jrs 2024/12/10
        If Session("Node2Plot") Is Nothing Then Exit Sub

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
                iNumDec = 3
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

    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_WARNING: Form event frm_ScenarioCreate.QueryUnload has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Closing' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Closing.html'.
    Private Sub frm_ScenarioCreate_Closing(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
        Dim Cancel As Short = eventArgs.Cancel
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_ISSUE: Constant vbFormControlMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2070"'
        'UPGRADE_ISSUE: Event parameter UnloadMode was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1057"'
        'JRSVBNET **** BEGIN ****
        'If UnloadMode = vbFormControlMenu Then '0

        '    If treeScenario.Nodes.Count > Totcat Then
        '        Call SaveScenario(ScenarioFileDir & "Working.Sce", "Working.Sce")
        '    End If
        '    frm_MainMenu.DefInstance.Show()

        'End If
        'eventArgs.Cancel = Cancel
        'JRSVBNET **** END ****
    End Sub

    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_WARNING: Event frm_ScenarioCreate.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Resize' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Resize.html'.
    Private Sub frm_ScenarioCreate_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Change mti May 28, 2002
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.ClientRectangle.html'.
        'JRSVBNET treeScenario.Height = System.Web.UI.WebControls.Unit.Pixel(Me.ClientRectangle.Height)'* 0.98
        'treeScenario.Width = Me.ScaleWidth * 0.4
    End Sub

    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_WARNING: Form event frm_ScenarioCreate.Unload has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Closed' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Closed.html'.
    Private Sub frm_ScenarioCreate_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'rsGroup.Close
        'mtiwebforms April 2004 ADO.Net
        'dbGroup.Close()
    End Sub

    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_WARNING: Event lst_nonscenario.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub lst_nonscenario_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lst_nonscenario.SelectedIndexChanged
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim TVar As String
        TVar = Trim(GetFirstToken(Support.ListBoxSelectedIndexCollectionManager.GetText(lst_nonscenario), ","))
        'search to match variable number       'LastVar$ = Var$
        chk = IfsVars.Item(TVar).Index
        Session("SEL") = chk
        'mti ScenarioCreate May 2004 Uncomment later
        'mnuSelect.Text = "De&lete"
        'UPGRADE_ISSUE: Form method frm_ScenarioCreate.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET Me.PopupMenu(mnuPop)

    End Sub
    '=============================ScenarioCreate.Frm=============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ListBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.KeyPress.html'.
    Private Sub lst_nonscenario_KeyPress()
        'JRSVBNET *** BEGIN ***
        ''CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'If KeyAscii = 27 Then
        '    Frm_nonscenario.Visible = False
        'End If

        'If KeyAscii = 0 Then
        '    'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '    EventArgs.Handled = True
        'End If
        'JRSVBNET *** END ***
    End Sub

    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_WARNING: Event lst_Varname.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub lst_Varname_SelectedIndexChanged1() '(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim SELN As String
        Dim SE As String
        Dim msg As String
        Dim Ind As Short
        Dim cnt As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        cnt = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For i2 = 1 To NumbVars
            'UPGRADE_WARNING: Couldn't resolve default property of object VScenCat(i2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRS 01/23/04
            If Not IsDBNull(VScenCat(i2)) Then
                If SaveVCat = VScenCat(i2) Then 'VCat(i2)
                    If Trim(VScenario(i2)) <> "" Then
                        If lst_varname.SelectedIndex = cnt Then
                            Ind = i2
                            Exit For
                        End If
                        cnt = cnt + 1 'Increment counter only within Session("Category") and scenario name not blank
                    End If
                End If
            End If
        Next i2

        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Ind = 0 Or Ind > NumbVars Then
            msg = "This selection is invalid."
            frm_varname.Visible = False
            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        SE = V(Ind) 'Name selected by clicking only
        SELN = UCase(SE)

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("SEL") = i2
        'mti ScenarioCreate May 2004 Uncomment later
        'mnuSelect.Text = "&Select"
        'UPGRADE_ISSUE: Form method frm_ScenarioCreate.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET Me.PopupMenu(mnuPop)
        '03/03/04 ContextMenu1_Popup(eventSender, eventArgs)
        'wmContext.Visible = True


    End Sub
    '=============================ScenarioCreate.Frm=============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ListBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.KeyPress.html'.
    Private Sub lst_varname_KeyPress()
        'JRSVBNET *** BEGIN ***
        ''CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'lkey = KeyAscii
        'If KeyAscii = 27 Then
        '    frm_varname.Visible = False

        '    frm_options.Visible = False
        '    'JRSVBNET lblJS.Visible = False
        '    VSc_slider.Visible = False
        '    LBLSLIDERVAL.Visible = False
        '    Frm_Custom.Visible = False
        '    frm_time.Visible = False
        '    'Mohammod 08 October, 2001
        '    Frm_nonscenario.Visible = False
        '    lbl_hint.Visible = True

        'End If
        'If KeyAscii = 0 Then
        '    'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '    EventArgs.Handled = True
        'End If
        'JRSVBNET *** END ***
    End Sub
    '=============================ScenarioCreate.Frm=============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ListBox.MouseMove' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.MouseMove.html'.
    Private Sub lst_varname_MouseMove()
        'JRSVBNET *** BEGIN ***
        ''CONVERSION_ISSUE: Class System.Windows.Forms.MouseEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'Dim Button As Short = EventArgs.Button \ &H100000
        ''CONVERSION_ISSUE: Property 'System.Windows.Forms.Control.ModifierKeys' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.ModifierKeys.html'.
        'Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        ''CONVERSION_ISSUE: Class System.Windows.Forms.MouseEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'Dim X As Single = VB6.PixelsToTwipsX(EventArgs.X)
        ''CONVERSION_ISSUE: Class System.Windows.Forms.MouseEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'Dim Y As Single = VB6.PixelsToTwipsY(EventArgs.Y)
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Dim YPos, iOldFontSize As Short
        ''CONVERSION_ISSUE: Property 'System.Drawing.Font.SizeInPoints' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Drawing.Font.SizeInPoints.html'.
        ''JRSVBNET iOldFontSize = Me.Font.SizeInPoints
        ''CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Font' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Font.html'.
        ''JRSVBNET Me.Font = VB6.FontChangeSize(Me.Font, lst_varname.Font.SizeInPoints)
        ''UPGRADE_ISSUE: Form method frm_ScenarioCreate.TextHeight was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        ''JRSVBNET YPos = Y \ Me.TextHeight("Xyz") + lst_varname.TopIndex
        ''CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Font' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Font.html'.
        ''JRSVBNET Me.Font = VB6.FontChangeSize(Me.Font, iOldFontSize)
        ''CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'If YPos < lst_varname.Items.Count Then
        'Else
        'End If
        'JRSVBNET *** END ***
    End Sub

    Public Sub mnuAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'Scenario Changes MTI- Change this whole proc

        'On Error GoTo er_Open

        Dim intfile As Short
        Dim intWfile As Short
        Dim intCfile As Short
        Dim intNfile As Short
        Dim RLine As String


        'frm_varname.Visible = False

        'Frm_nonscenario.Visible = False
        'frm_dim1.Visible = False

        ''mti WebForm Changes From June 21, 2004  -added this to bypass the branch
        'blnScenarioDirty = False

        'If blnScenarioDirty Then

        '    Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")

        '    If InStr(1, Session("cur_file"), ".sce", 1) > 0 Then 'vbtextcompare
        '        msg = "Save Changes to " & Mid(Session("cur_file"), 1, InStr(1, Session("cur_file"), ".sce", 1) - 1) & "?"
        '    Else
        '        msg = "Save Changes to " & Session("cur_file") & "?"
        '    End If

        '    'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '    Select Case MsgBoxResult.Yes 'JRSVBNET WinFormsToWeb_MessageBox.Show(msg, MsgBoxStyle.YesNoCancel, "IFS - Add Scenario")
        '        Case MsgBoxResult.Yes
        '            'If UCase(Session("cur_file")) = "UNTITLED" Then
        '            If UCase(Session("cur_file")) = "UNTITLED" Or Mid(UCase(Session("cur_file")), 1, 7) = "WORKING" Then

        '                'mti ScenarioCreate May 2004 Uncomment later
        '                'Call mnuSave_Click(mnuSave, New System.EventArgs)

        '                If SaveCancelled Then
        '                    Exit Sub
        '                End If
        '            Else
        '                Call SaveScenario(Session("ScenarioFileDir") & Session("cur_file"), Session("cur_file"))
        '            End If
        '        Case MsgBoxResult.No
        '            'Do Nothing : go ahead
        '        Case MsgBoxResult.Cancel
        '            Exit Sub
        '    End Select
        'End If



        'JRSVBNET cmnDlg1.DialogTitle = "Choose Scenario File to Add"

        'JRSVBNET cmnDlg1.filter = "Scenario Files (*.sce)|*.sce|All Files (*.*)|*.*"

        'JRSVBNET cmnDlg1.Filename = ""
        'JRSVBNET cmnDlg1.DefaultExt = ".sce"
        'JRSVBNET cmnDlg1.InitDir = ScenarioFileDir
        'JRSVBNET cmnDlg1.FLAGS = MSComDlg.FileOpenConstants.cdlOFNFileMustExist
        'JRSVBNET cmnDlg1.CancelError = True
        'JRSVBNET cmnDlg1.ShowOpen()

        'mti WebForm Changes From June 21, 2004 -start
        Session("fpPurpose") = "Scenario_Add"
        Session("fpInitRootPath") = "~/Scenario"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "filepicker.aspx")
        'mti WebForm Changes From June 21, 2004  -end 

        'msg = "Add Scenario "  'JRSVBNET & cmnDlg1.FileTitle & " to the working?"

        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET If WinFormsToWeb_MessageBox.Show(msg, MsgBoxStyle.YesNo, "IFS - Add Scenario") = MsgBoxResult.No Then
        'JRSVBNET Exit Sub
        'JRSVBNET End If

        'JRSVBNET Session("cur_file") = 'JRSVBNET cmnDlg1.Filename 'name and path


        ''UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'If Dir(Session("ScenarioFileDir") & "Working.Add") <> "" Then
        '    Kill(Session("ScenarioFileDir") & "Working.Add")
        'End If

        'intfile = FreeFile()
        ''temporary  file where working and the newly selected one are added
        'FileOpen(intfile, Session("ScenarioFileDir") & "Working.add", OpenMode.Append)

        'intWfile = FreeFile() 'working.sce
        ''UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
        '    FileOpen(intWfile, Session("ScenarioFileDir") & "Working.sce", OpenMode.Input)
        'Else
        '    FileOpen(intWfile, Session("ScenarioFileDir") & "Working.sce", OpenMode.Append)
        'End If

        'intCfile = FreeFile()
        'FileOpen(intCfile, Session("cur_file"), OpenMode.Input) 'file selected from dialog for adding


        'intNfile = FreeFile() 'working comment file
        'FileOpen(intNfile, Session("ScenarioFileDir") & "Working.txt", OpenMode.Append)

        ''Changes MTI 02 July,2002
        'PrintLine(intNfile)
        'PrintLine(intNfile, New String("=", 50))
        'PrintLine(intNfile, "Added Scenario: ")  'JRSVBNET & cmnDlg1.FileTitle & " on " & VB6.Format(Today, "dddd, mmm d yyyy") & " at " & VB6.Format(TimeOfDay, "hh:mm:ss AMPM"))
        'PrintLine(intNfile, New String("=", 50))
        ''end Changes MTI 02 July,2002

        'Do While Not EOF(intWfile)
        '    RLine = LineInput(intWfile)
        '    If UCase(Mid(RLine, 1, 7)) <> "COMMENT" Then
        '        PrintLine(intfile, RLine)
        '    Else
        '        'Comment, proceed on- working comment is already in working.txt
        '        Exit Do
        '    End If
        'Loop

        'Do While Not EOF(intCfile)
        '    RLine = LineInput(intCfile)
        '    Select Case UCase(Mid(RLine, 1, 3)) ' <> "COMMENT" Then
        '        Case "LOW", "HIG", "MED", "CUS", "SLI"
        '            PrintLine(intfile, RLine)
        '        Case "COM" 'Comment, write to working.txt
        '            PrintLine(intNfile)
        '            PrintLine(intNfile, New String("=", 40))
        '            PrintLine(intNfile)
        '            Do While True
        '                RLine = LineInput(intCfile)
        '                PrintLine(intNfile, RLine)
        '                If EOF(intCfile) Then
        '                    Exit Do
        '                End If
        '            Loop
        '            Exit Do
        '        Case Else 'garbage written in file
        '            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '            WinFormsToWeb_MessageBox.Show("File Type Mismatch!", MsgBoxStyle.OKOnly, "IFS - Add Scenario")
        '            FileClose(intfile)
        '            FileClose(intWfile)
        '            FileClose(intCfile)
        '            FileClose(intNfile)
        '            Exit Sub
        '    End Select
        'Loop

        ''must close here - or might not be able to copy
        'FileClose(intfile)
        'FileClose(intWfile)
        'FileClose(intCfile)
        'FileClose(intNfile)

        'FileCopy(Session("ScenarioFileDir") & "Working.add", Session("ScenarioFileDir") & "Working.sce")

        '        Session("cur_file") = "Working.Sce"

        '        'start here mti
        '        Call InitTree()
        '        Call InitVars() 'initializes blnscenariodirty

        '        'Session("cur_file") = 'JRSVBNET cmnDlg1.FileTitle

        '        Call BuildDrivers(Session("ScenarioFileDir") & "Working.sce")

        '        'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
        '        'treeScenario.Nodes.Item(1).Selected = True 
        '        'Curnode = treeScenario.Nodes.Item(1)
        '        'mti WebForm Changes From June 26, 2004
        '        treeScenario.SelectedNodeIndex = 0 '1
        '        Curnode = treeScenario.Nodes.Item(0) '1

        '        frm_varname.Visible = False



        '        blnScenarioDirty = True

        '        Exit Sub
        'er_Open:


    End Sub

    Public Sub mnuBack_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
        'JRSVBNET frm_GuidedUse3.DefInstance.Visible = True
        'UPGRADE_WARNING: Form method frm_GuidedUse3.ZOrder has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'JRSVBNET frm_GuidedUse3.DefInstance.BringToFront()
        Support.FormManager.Close(Me)
    End Sub

    Public Sub MnuDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        frm_varname.Visible = False

        Frm_nonscenario.Visible = False
        frm_dim1.Visible = False
        'jrs 2023/07/11
        frm_VarDesc.Visible = False

        Dim TotSiblings As Short
        Dim Parentnode As Microsoft.Web.UI.WebControls.TreeNode
        Dim lsibling, DelSibling As Short

        'mtiwebformsMarch2004 (2)ScenarioCreate
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)

        Select Case NodeType(Curnode)

            Case ObjectType.Otregion
                TotSiblings = Curnode.Parent.Nodes.Count
                Parentnode = Curnode.Parent

                'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                'JRSVBNET lsibling = Curnode.LastSibling.Index
                'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                'mtiwebformsMarch2004 (2)ScenarioCreate
                DelSibling = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)

                i2 = DelSibling

                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                If True Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("Delete Dimension " & Curnode.Text & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Call DelAdjust_NodePlot(i2, Support.TreeViewManager.CountNodes(treeScenario.Nodes), CShort(Session("Node2Plot")(i2)))
                    'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Remove' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                    'treeScenario.Nodes.Remove(treeScenario.GetNodeFromIndex(Curnode.GetNodeIndex))
                    'mtiwebformsMarch2004 (2)ScenarioCreate
                    Curnode.Remove()

                    Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)

                    'delete driver in case only children is removed
                    If TotSiblings = 1 Then
                        'no region left- so delete driver
                        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        'i2 = Parentnode.GetNodeIndex
                        i2 = TreeIndexToFlatIndex(treeScenario.Nodes, Parentnode.GetNodeIndex)

                        Call DelAdjust_NodeOnly(i2, Support.TreeViewManager.CountNodes(treeScenario.Nodes))
                        'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Remove' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        'treeScenario.Nodes.Remove(treeScenario.GetNodeFromIndex(Parentnode.GetNodeIndex))
                        Parentnode.Remove()
                        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)

                    Else
                        If DelSibling = lsibling Then
                            'lastnode deleted, select the previous node, if any, instead of parent
                            'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
                            'Parentnode.Child.LastSibling.Selected = True 
                            Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                        End If
                    End If

                End If ' vbyesno


            Case ObjectType.OtDriver
                If Curnode.Nodes.Count = 0 Then 'no subdims but contains data
                    'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                    'i2 = Curnode.GetNodeIndex
                    i2 = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
                    'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    If True Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("Delete driver " & Curnode.Text & "?", MsgBoxStyle.YesNo, "IFS - Scenario") = MsgBoxResult.Yes Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Call DelAdjust_NodePlot(i2, Support.TreeViewManager.CountNodes(treeScenario.Nodes), CShort(Session("Node2Plot")(i2)))
                        'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Remove' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                        'JRSVBNET treeScenario.Nodes.Remove(Curnode.GetNodeIndex)
                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        Curnode.Remove()
                        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                    End If

                Else
                    'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                    If True Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("Delete driver " & Curnode.Text & " and all its " & DelMsg(Curnode.Child.Index) & "?", MsgBoxStyle.YesNo, "IFS - Scenario") = MsgBoxResult.Yes Then
                        'remove children
                        Do While Curnode.Nodes.Count > 0
                            'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                            'JRSVBNET 
                            'mtiwebformsMarch2004 (2)ScenarioCreate
                            'i2 = Curnode.Child.Index
                            i2 = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.Nodes.Item(0).GetNodeIndex)
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Call DelAdjust_NodePlot(i2, Support.TreeViewManager.CountNodes(treeScenario.Nodes), CShort(Session("Node2Plot")(i2)))
                            'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Remove' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                            'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                            'JRSVBNET treeScenario.Nodes.Remove(Curnode.Child.Index)
                            'Set Curnode = treeScenario.SelectedItem
                            'mtiwebformsMarch2004 (2)ScenarioCreate
                            Curnode.Nodes.Item(0).Remove()
                            Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                        Loop
                        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        'i2 = Curnode.GetNodeIndex
                        i2 = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
                        Call DelAdjust_NodeOnly(i2, Support.TreeViewManager.CountNodes(treeScenario.Nodes))
                        'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Remove' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                        'JRSVBNET treeScenario.Nodes.Remove(Curnode.GetNodeIndex)
                        'jrs 2005/04/20
                        Curnode.Remove()
                        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                    End If

                End If
            Case ObjectType.otIssueArea
                Exit Sub
            Case ObjectType.otBroadCategory
                Exit Sub
        End Select

        frm_options.Visible = False
        'mtiwebformsMarch2004 (2)ScenarioCreate
        'JRSVBNET 
        lblJS.Visible = False
        'JRSVBNET VSc_slider.Visible = False

        'LBLSLIDERVAL.Visible = False

        pnlSlider.Visible = False



        Frm_Custom.Visible = False
        frm_time.Visible = False
        'Mohammod 08 October, 2001
        Frm_nonscenario.Visible = False
        frm_varname.Visible = False

        frm_dim1.Visible = False



        lbl_hint.Visible = True
        pnlMaxmin.Visible = False

        If Support.TreeViewManager.CountNodes(treeScenario.Nodes) > Totcat Then
            Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
            Session("cur_file") = "Working.Sce"
            Call InitTree()
            Call InitVars() 'initializes blnscenariodirty
            Session("cur_file") = "Working.Sce"
            Call BuildDrivers(Session("ScenarioFileDir") & "Working.Sce")
        Else

            'Start 'Scenario Changes MTI
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
                Kill(Session("ScenarioFileDir") & "Working.Sce")
            End If
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(Session("ScenarioFileDir") & "Working.txt") <> "" Then
                Kill(Session("ScenarioFileDir") & "Working.txt")
            End If
            Session("cur_file") = "Untitled" '"Working.Sce"
            Call InitTree()
            Call InitVars() 'initializes blnscenariodirty
            Session("cur_file") = "Untitled" '"Working.Sce"
            'End 'Scenario Changes MTI

        End If

        'JRS 11/22/03 blnScenarioDirty = False 'Reset after InitVars
        blnScenarioDirty = True

        'mti August 2004
        'Curnode = treeScenario.Nodes.Item(1)
        'mti WebForm Changes From June 26, 2004
        treeScenario.SelectedNodeIndex = 0 '1
        'treeScenario.Nodes.Item(1).Selected = True 
        Curnode = treeScenario.Nodes.Item(0) '1
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub DimLoad()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Scenario ADO.net May 2004
        Dim Rcnt As Short
        Dim Valtbl As DataTable

        '       Make Combo Box and Label Visible for Subdimension(s)
        '       ----------------------------------------------------
        'jrs 2012/01/27
        wmMain.AccessorKeyCode = 0

        frm_dim1.Visible = True


        'mti September 2004 2
        _Opt_highlow_0.Enabled = False
        _Opt_highlow_1.Enabled = False
        _Opt_highlow_2.Enabled = False
        cmdCustom.Enabled = False
        btnSlider.Enabled = False
        'mti September 2004 2 end 

        'CONVERSION_NOTE: Method 'System.Windows.Forms.GroupBox.BringToFront' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.BringToFront().html'.
        'frm_dim1.BringToFront() 
        frm_varname.Visible = False


        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_DIM1.Items.Clear()
        lstDIM1.Items.Clear()

        'JRSVBNET WinFormsToWeb_FocusControl.Focus("cmb_DIM1")

        '       Load element names into Combo Box
        '       ---------------------------------
        'mtiwebformsMarch2004 (2)ScenarioCreate
        cmb_DIM1.Items.Insert(0, "")

        If Session("FlagGroup") = True And DimNum = 1 Then 'Set up for groups
            'mti Scenario ADO.net May 2004
            'why not take from groupnames$() -mti
            'db = DAODBEngine_definst.OpenDatabase(IFs_MDB2Active)
            SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " + GroupIDNew + " order by upper(GroupName)", "", Valtbl)

            'valtbl = db.OpenRecordset(GroupIDNew)
            'valtbl.Index = "GroupName"
            'valtbl.MoveFirst()
            For Rcnt = 0 To Valtbl.Rows.Count - 1
                'While Not valtbl.EOF
                'JRS 07/29/02 GUIDEDUSE BEGIN
                'If Guided_use And UCase(Select_name) = UCase(valtbl.Fields("GroupName").Value) Then
                If Guided_use And UCase(Select_name) = UCase(Valtbl.Rows(Rcnt).Item("GroupName")) Then
                    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = Select_name
                    lstDIM1.Items(lstDIM1.SelectedIndex).Text = Select_name
                End If
                'JRS 07/29/02 GUIDEDUSE END
                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'cmb_dim1.Items.Add(New ListItem(valtbl.Fields("GroupName").Value))
                cmb_DIM1.Items.Add(New ListItem(Valtbl.Rows(Rcnt).Item("GroupName")))
                lstDIM1.Items.Add(New ListItem(Valtbl.Rows(Rcnt).Item("GroupName")))

                'valtbl.MoveNext()
                'End While
            Next
            'valtbl.Close()
            'db.Close()
            Valtbl = Nothing
            'jrs 2006/05/18 Remove All
            'cmb_DIM1.Items.Add(New ListItem("ALL"))
            'lstDIM1.Items.Add(New ListItem("ALL"))
        Else 'Proceed with regions or secondary dimensions

            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For eleloop = 1 To MaxVal(DimNum) - 1 'jrs 2006/05/18 Remove All
                'JRS 07/29/02 GUIDEDUSE BEGIN
                If Guided_use And UCase(Select_name) = UCase(GRPS(DimNum, eleloop)) Then
                    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = Select_name
                    lstDIM1.Items(lstDIM1.SelectedIndex).Text = Select_name
                End If
                'JRS 07/29/02 GUIDEDUSE END
                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'cdv 2021/02/10 ****START**** Flip gender based on bMaleFirst
                If GRPS(DimNum, eleloop) <> "Male" And GRPS(DimNum, eleloop) <> "Female" Then
                    cmb_DIM1.Items.Add(New ListItem(GRPS(DimNum, eleloop)))
                    lstDIM1.Items.Add(New ListItem(GRPS(DimNum, eleloop)))
                ElseIf bMaleFirst Then
                    cmb_DIM1.Items.Add(New ListItem(GRPS(DimNum, eleloop)))
                    lstDIM1.Items.Add(New ListItem(GRPS(DimNum, eleloop)))
                ElseIf GRPS(DimNum, eleloop) = "Male" And eleloop = 1 And bMaleFirst = False Then
                    'GRPS(DimNum, eleloop) = "Female"
                    cmb_DIM1.Items.Add(New ListItem("Female"))
                    lstDIM1.Items.Add(New ListItem("Female"))

                ElseIf GRPS(DimNum, eleloop) = "Female" And eleloop = 2 And bMaleFirst = False Then
                    'GRPS(DimNum, eleloop) = "Male"
                    cmb_DIM1.Items.Add(New ListItem("Male"))
                    lstDIM1.Items.Add(New ListItem("Male"))

                Else
                    cmb_DIM1.Items.Add(New ListItem(GRPS(DimNum, eleloop)))
                    lstDIM1.Items.Add(New ListItem(GRPS(DimNum, eleloop)))
                End If
                'cdv 2021/02/10 ****END****
            Next eleloop
            'jrs 2014/03/07
            If DimNum <> 1 And GRPS$(DimNum, eleloop - 1) <> "Total" And Not VNoTot2(Session("SEL")) Then 'jrs 2014/10/08 added not vnotot2
                cmb_DIM1.Items.Add("Total")
                lstDIM1.Items.Add("Total")
                'jrs 2014/10/08
                lblTot2.Visible = True
            End If
        End If


    End Sub
    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_WARNING: Event cmb_Dim1.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cmb_Dim1_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_DIM1.SelectedIndexChanged
        'jrs 2023/07/11
        frm_VarDesc.Visible = False

        lstDIM1.SelectedIndex = cmb_DIM1.SelectedIndex - 1
        'mtiwebformsMarch2004 (2)ScenarioCreate
        'mti Agust 2004
        Dim SCurnodeNo As String 'Microsoft.Web.UI.WebControls.TreeNode
        'mti August 2004 -end

        Dim msg As String
        Dim Found As String
        Dim Ind As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim ntext As String
        Dim curkey As String
        '   Select Dimension number
        '   -----------------------
        'mtiwebformsMarch2004 (2)ScenarioCreate
        'Ind = cmb_dim1.SelectedIndex + 1
        Ind = cmb_DIM1.SelectedIndex  'to adjust for blank item inserted in combo in dimload
        'end mtiwebformsMarch2004 (2)ScenarioCreate
        If Ind >= 1 And Session("FlagGroup") = False Then
            '       SEL2$ = UCase$(GRPS$(DimNum, ind%))
            'CONVERSION_NOTE: Untranslated statement in next line(s). Please check source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1008.html'.
            sel2 = GRPS(DimNum, Ind)
            'jrs 2014/03/07
            If sel2$ = "ALL" Then
                sel2$ = "Total"
            End If
        Else
            sel2 = cmb_DIM1.SelectedValue
        End If
        '
        '   Check Validity of Selection
        '   ---------------------------

        '    Found$ = "False"
        '    For EleLoop = 1 To MaxVal(DimNum)
        '        If UCase$(sel2$) = UCase$(GRPS$(DimNum, EleLoop)) Then
        '            Found$ = "True"
        '            DimPoint% = EleLoop
        '        End If
        '    Next EleLoop
        '    If Found$ = "False" Then
        '        GoSub DimError:
        '        Exit Sub
        '    End If

        Found = "False"
        If Session("FlagGroup") And DimNum = 1 Then
            Found = CheckGroups(sel2)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For eleloop = 1 To MaxVal(DimNum)
                'jrs 2014/03/07 No validation for Total
                If DimNum <> 1 And sel2 = "Total" And GRPS(DimNum, MaxVal(DimNum) - 1) <> "Total" Then
                    Found = True
                    DimPoint = 1
                    Ind = DimPoint
                    Exit For
                End If

                '       If SEL2$ = UCase$(GRPS$(DimNum, EleLoop)) Then
                If UCase(sel2) = UCase(GRPS(DimNum, eleloop)) Then
                    Found = "True"
                    DimPoint = eleloop
                    'JRS 07/08/03
                    Ind = DimPoint
                    Exit For
                End If
            Next eleloop
        End If
        If Found = "False" Then
            'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1014"'
            'JRS 01/23/04
            DimError()
            Exit Sub
        End If


        '   If ALL Selected
        '   ---------------

        If ((SubDimNum = 1 Or SubDimDone = "False") And sel2 = "ALL") Then SEL1 = "ALL"

        'jrs 10/27/04
        If VDim1(Session("SEL")) = 1 And SEL1 = "ALL" Then
            LoadMessage("Please use groups, and select World as Dimension 1, instead of using ALL regions.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        If SEL1 = "ALL" Or sel2 = "ALL" Then

            If SubDimNum = 1 Then '  for single-dimensioned variable, ALL
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                ntext = VScenario(Session("SEL"))

                'jrs 10/25/04
                Dim i As Integer
                For i = 1 To MaxVal(VDim1(Session("SEL"))) - 1
                    'mtiwebformsMarch2004 (2)ScenarioCreate
                    CurNodeNo = AddChildNode(treeScenario, ntext, "d ", True, Category_Node_Index(treeScenario, SaveVCat)) 'Savevcat + 1  : '1 for root node
                    Session("tGR1N")(Session("tCOUNTE") + 1) = GRPS(VDim1(Session("SEL")), i)
                    Session("tGR2N")(Session("tCOUNTE") + 1) = ""
                    sel2 = GRPS(VDim1(Session("SEL")), i)
                    SEL1 = sel2

                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    curkey = "r1" & V(Session("SEL")) & "," & SEL1

                    i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes)

                    'Mohammod 08 October, 2001
                    'mtiwebformsMarch2004 (2)ScenarioCreate
                    CurNodeNo = AddChildNode(treeScenario, SEL1, curkey, False, CurNodeNo)
                    'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
                    'treeScenario.Nodes.Item(CurNodeNo).Selected = True 
                    Curnode = Support.TreeViewManager.GetNode(treeScenario.Nodes, CurNodeNo)
                    Session("CurNodeNo") = CurNodeNo

                    Support.TreeViewManager.SetSelectedNode(treeScenario, Curnode)
                    If Support.TreeViewManager.CountNodes(treeScenario.Nodes) > i2 Then
                        'mti Agust 2004
                        SCurnodeNo = CurNodeNo

                        Call HoldNodeInfo()
                        Call Prepare_Right()
                        Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
                        Session("cur_file") = "Working.Sce"
                        Call InitTree()
                        Call InitVars() 'initializes blnscenariodirty
                        Session("cur_file") = "Working.Sce"
                        blnScenarioDirty = True 'Reset after InitVars
                        Call BuildDrivers(Session("ScenarioFileDir") & "Working.Sce")
                        CurNodeNo = SCurnodeNo
                        Curnode = Support.TreeViewManager.GetNode(treeScenario.Nodes, CurNodeNo)
                        Support.TreeViewManager.SetSelectedNode(treeScenario, Curnode)
                        'mti Agust 2004 -end
                        Call Prepare_Right()
                    End If
                Next i
                'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1014"'
                'JRS 01/23/04
                RemoveDim1()
                Exit Sub

            ElseIf SubDimNum = 2 And SubDimDone = "False" Then ' Get Second Dim

                SubDimDone = "True"
                SDimNum = DimNum
                SDimPoint = DimPoint
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                DimNum = VDim2(Session("SEL"))
                'JRS 03/04/04
                Session("DimNum") = DimNum
                Session("SubDimDone") = SubDimDone
                Call DimLoad()
                Exit Sub

            ElseIf SubDimNum = 2 And SubDimDone = "True" Then ' for 2-dimensioned variable
                'jrs 10/26/04
                Dim iAll As Integer
                '1 Dim 1 is ALL
                '2 Dim 2 is ALL
                '3 Dim 1 and 2 are ALL
                iAll = 0

                If SEL1$ = "ALL" And sel2$ = "ALL" Then
                    DimPoint = 1
                    iAll = 3
                End If
                If iAll = 0 And SEL1$ = "ALL" Then
                    iAll = 1
                ElseIf iAll = 0 And sel2$ = "ALL" Then
                    iAll = 2
                End If



                'mtiwebformsMarch2004 (2)ScenarioCreate
                SEL1 = Session("SEL1")

                'If SEL1 = "ALL" And sel2 = "ALL" Then DimPoint = 1
                'If SEL1 = "ALL" Then ' (All on 1st dimension)

                '    'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                '    ntext = VScenario(Session("SEL"))

                '    Session("tGR1N")(Session("tCOUNTE") + 1) = "ALL"
                '    Session("tGR2N")(Session("tCOUNTE") + 1) = sel2
                '    'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                '    curkey = "r " & V(Session("SEL")) & "," & SEL1 & "," & sel2


                'Else ' (All on 2nd dimension)

                '    Session("tGR1N")(Session("tCOUNTE") + 1) = GRPS(SDimNum, SDimPoint)
                '    Session("tGR2N")(Session("tCOUNTE") + 1) = sel2

                '    'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                '    ntext = VScenario(Session("SEL")) & " (" & Trim(sel2) & ")"
                '    '                curkey = "r " & V$(Session("SEL")) & "," & SEL1$ & "," & Str(Trim(Ind%)) 'all??
                '    'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                '    curkey = "r " & V(Session("SEL")) & "," & SEL1 & "," & sel2
                'End If

                Dim j As Integer
                Dim iEnd As Integer
                Dim jEnd As Integer

                Select Case iAll
                    Case 1
                        iEnd = MaxVal(VDim1(Session("SEL"))) - 1
                        jEnd = 1
                    Case 2
                        iEnd = 1
                        jEnd = MaxVal(VDim2(Session("SEL"))) - 1
                    Case 3
                        iEnd = MaxVal(VDim1(Session("SEL"))) - 1
                        jEnd = MaxVal(VDim2(Session("SEL"))) - 1
                End Select

                Dim i As Integer
                For i = 1 To iEnd
                    For j = 1 To jEnd

                        ntext = VScenario$(Session("SEL"))
                        'jrs 2005/05/06
                        If iAll = 1 Or iAll = 3 Then
                            Session("tGR1N")(Session("tCOUNTE") + 1) = GRPS(VDim1(Session("SEL")), i)
                        Else
                            Session("tGR1N")(Session("tCOUNTE") + 1) = SEL1$
                        End If
                        'jrs 2005/05/06
                        If iAll = 2 Or iAll = 3 Then
                            Session("tGR2N")(Session("tCOUNTE") + 1) = GRPS(VDim2(Session("SEL")), j)
                        Else
                            Session("tGR2N")(Session("tCOUNTE") + 1) = sel2$
                        End If


                        curkey = "r " & V$(Session("SEL")) & "," & Session("tGR1N")(Session("tCOUNTE") + 1) & "," & Session("tGR2N")(Session("tCOUNTE") + 1)


                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        CurNodeNo = AddChildNode(treeScenario, ntext, "d ", True, Category_Node_Index(treeScenario, SaveVCat)) 'Savevcat + 1  : '1 for root node
                        i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes)

                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        CurNodeNo = AddChildNode(treeScenario, SEL1, curkey, False, CurNodeNo)
                        'Mohammod 08 October, 2001
                        'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
                        'treeScenario.Nodes.Item(CurNodeNo).Selected = True 
                        'Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                        Curnode = Support.TreeViewManager.GetNode(treeScenario.Nodes, CurNodeNo)
                        Session("CurNodeNo") = CurNodeNo

                        Support.TreeViewManager.SetSelectedNode(treeScenario, Curnode)
                        'jrs 2005/05/06
                        If Session("FlagGroup") Then
                            Curnode.ID = "Group"
                        Else
                            Curnode.ID = ""
                        End If

                        If Support.TreeViewManager.CountNodes(treeScenario.Nodes) > i2 Then
                            'new node added
                            'mti Agust 2004
                            SCurnodeNo = CurNodeNo

                            Call HoldNodeInfo()
                            Call Prepare_Right()
                            Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
                            Session("cur_file") = "Working.Sce"
                            Call InitTree()
                            Call InitVars() 'initializes blnscenariodirty
                            Session("cur_file") = "Working.Sce"
                            blnScenarioDirty = True 'Reset after InitVars
                            Call BuildDrivers(Session("ScenarioFileDir") & "Working.Sce")

                            CurNodeNo = SCurnodeNo
                            Curnode = Support.TreeViewManager.GetNode(treeScenario.Nodes, CurNodeNo)
                            Support.TreeViewManager.SetSelectedNode(treeScenario, Curnode)
                            'mti Agust 2004 -end 
                            Call Prepare_Right()

                        End If

                    Next j
                Next i

                SubDimDone = "False"
                'JRS 03/04/04
                Session("SubDimDone") = SubDimDone
                SEL1 = ""
                'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1014"'
                'JRS 01/23/04
                RemoveDim1()
                Exit Sub
            End If
        End If

        'IF a Group Selected
        'JRS 11/4/01 GROUPING BEGIN

        If DimNum = 1 And Session("FlagGroup") = True Then
            GroupSel = sel2
            LoadCountries((GroupSel))
            DimPoint = RegionIdx(CountryGroup(1))
        End If

        'JRS 11/4/01 GROUPING END


        '
        '   Load Subdimension names and Pointers - ALL on neither dimension
        '   ---------------------------------------------------------------

        If SubDimNum = 1 Or SubDimDone = "False" Then
            'jrs 2006/05/31 dim3
            If Session("tGR1N")(Session("tCOUNTE") + 1) = "" Then
                Session("tGR1N")(Session("tCOUNTE") + 1) = sel2$
                VPTR = VPTR + DimPoint
                GMAX1 = MAXCOM(DimNum) - 1
                'cdv 2021/02/10 ****START**** Third Dim loads correctly now
            ElseIf Session("tGR2N")(Session("tCOUNTE") + 1) = "" Then
                Session("tGR2N")(Session("tCOUNTE") + 1) = sel2$
                VPTR = VPTR + ((DimPoint - 1) * GMAX1)
                GMAX2 = MAXCOM(DimNum) - 1
            Else
                Session("tGR3N")(Session("tCOUNTE") + 1) = sel2$
                VPTR = VPTR + ((DimPoint - 1) * GMAX1 * GMAX2)
                SubDimDone = "True"
                '2021/02/10 ****END****
            End If
            'jrs 2005/05/13
            Session("GMAX1") = GMAX1
            Session("GMAX2") = GMAX2
        Else
            'jrs 2006/05/31 dim3
            If Session("tGR2N")(Session("tCOUNTE") + 1) = "" Then
                Session("tGR2N")(Session("tCOUNTE") + 1) = sel2$
                VPTR = VPTR + ((DimPoint - 1) * GMAX1)
            Else
                Session("tGR3N")(Session("tCOUNTE") + 1) = sel2$
                VPTR = VPTR + ((DimPoint - 1) * GMAX1 * GMAX2)
            End If
        End If
        'JRS 03/04/04
        Session("VPTR") = VPTR
        '
        '   Determine whether to return to form for another variable
        '       or to get another subdimension element
        '   --------------------------------------------------------

        If SubDimNum = 1 Or SubDimDone = "True" Then

            'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1014"'
            'JRS 01/23/04 
            RemoveDim1()


            'add child nodes and prepare right
            'Function AddChildNode(TargetTree As TreeView, NodeText As String, KeyPrefix As String, TargetIndex As Integer) As Integer


            If SubDimNum <= 1 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                ntext = VScenario(Session("SEL"))
                'cdv 2021/02/10 Flip Text if Gendered and bMaleFirst = False
                If bMaleFirst = False Then
                    If sel2 = "Male" Then
                        sel2 = "Female"
                    ElseIf sel2 = "Female" Then
                        sel2 = "Male"
                    End If
                End If
                SEL1 = sel2
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                curkey = "r " & V(Session("SEL")) & "," & SEL1
            ElseIf SubDimNum <= 2 Then 'jrs 2006/05/30 dim3
                'cdv 2021/02/10 Flip Text if Gendered and bMaleFirst = False
                If Session("FlagGroup") = False Then 'cdv 2022/10/24
                    If bMaleFirst = False Then
                        If sel2$ = "Male" Then
                            sel2$ = "Female"
                        ElseIf sel2$ = "Female" Then
                            sel2$ = "Male"
                        End If
                    End If
                End If
                ntext = VScenario(Session("SEL")) + " (" + Trim(sel2$) + ")"
                'jrs 2006/12/28 curkey = "r " & V(Session("SEL")) & "," & SEL1$ & "," & Trim(Str(Ind))  'number of second dim goes to key
                'cdv 2021/02/10 ****START**** Logic to updated indexes for Scenarios based off Text Values
                If sel2$ = "Male" Then
                    curkey = "r " & V$(Session("SEL")) & "," & SEL1$ & "," & "1"
                ElseIf sel2$ = "Female" Then
                    curkey = "r " & V$(Session("SEL")) & "," & SEL1$ & "," & "2"
                Else
                    curkey = "r " & V$(Session("SEL")) & "," & SEL1$ & "," & sel2$
                End If

            Else 'jrs 2006/05/30 dim3
                'cdv 2021/02/10 Flip Text if Gendered and bMaleFirst = False
                If bMaleFirst = False Then
                    If sel2$ = "Male" Then
                        sel2$ = "Female"
                    ElseIf sel2$ = "Female" Then
                        sel2$ = "Male"
                    End If
                End If
                'mtiwebformsMarch2004 (2)ScenarioCreate
                SEL1 = Session("SEL1")

                'jrs 2023/06/23 added space after ,
                ntext = VScenario$(Session("SEL")) + " (" + sDim2 + ", " + Trim(sel2$) + ")"

                'cdv 2021/02/10 Override Index value if Dim 3 is gendered to match Male = 1 Female = 2
                If sel2$ = "Male" Then
                    Ind = 1
                ElseIf sel2$ = "Female" Then
                    Ind = 2
                End If
                'cdv 2021/02/10 ****END****
                'jrs 2023/04/26
                If gGetDimIdx(VDim2(Session("SEL")), sDim2, False, Me) > 0 Then
                    curkey = "r " & V$(Session("SEL")) & "," & SEL1$ & "," & gGetDimIdx(VDim2(Session("SEL")), sDim2, True, Me).ToString.Trim & "," & Ind.ToString.Trim 'number of third dim goes to key
                Else
                    curkey = "r " & V$(Session("SEL")) & "," & SEL1$ & "," & sDim2 & "," & Ind.ToString.Trim 'number of third dim goes to key
                End If
                sDim2 = ""
                Session("sDim2") = sDim2

            End If
            'Dim i2 As Integer
            'add driver and second dim
            'mtiwebformsMarch2004 (2)ScenarioCreate
            'cdv 2021/02/10 Change the ntext to match the new GRP Order


            CurNodeNo = AddChildNode(treeScenario, ntext, "d ", True, Category_Node_Index(treeScenario, SaveVCat))  'Savevcat + 1  : '1 for root node

            'add region
            'RDM,Netherlands,2,
            'CurKey = "r " & V$(Session("SEL")) & "," & SEL1$ & "," & Str(Trim(Ind%))
            i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes)
            'mtiwebformsMarch2004 (2)ScenarioCreate
            CurNodeNo = AddChildNode(treeScenario, SEL1, curkey, False, CurNodeNo)
            'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
            Curnode = Support.TreeViewManager.GetNode(treeScenario.Nodes, CurNodeNo)
            Session("CurNodeNo") = CurNodeNo

            Support.TreeViewManager.SetSelectedNode(treeScenario, Curnode)

            If Session("FlagGroup") Then
                'CONVERSION_ISSUE: Method 'MSComctlLib.NodeClass.let_Tag' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.html'.
                Curnode.ID = "Group"
                'JRS 09/06/03
            Else
                'CONVERSION_ISSUE: Method 'MSComctlLib.NodeClass.let_Tag' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.html'.
                Curnode.ID = ""
            End If
            If Support.TreeViewManager.CountNodes(treeScenario.Nodes) > i2 Then
                'new node added
                'mti Agust 2004
                SCurnodeNo = CurNodeNo

                Call HoldNodeInfo()

                Call Prepare_Right()

                Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
                Session("cur_file") = "Working.Sce"

                Call InitTree()
                Call InitVars() 'initializes blnscenariodirty
                Session("cur_file") = "Working.Sce"
                Call BuildDrivers(Session("ScenarioFileDir") & "Working.Sce")
                '--------

                blnScenarioDirty = True 'Reset after InitVars


                ''last added node will be curnode
                'Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                ''JRS 07/07/03
                'If Session("FlagGroup") Then
                '    'CONVERSION_ISSUE: Method 'MSComctlLib.NodeClass.let_Tag' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.html'.
                '    Curnode.ID = "Group"
                '    'JRS 09/06/03
                'Else
                '    'CONVERSION_ISSUE: Method 'MSComctlLib.NodeClass.let_Tag' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.html'.
                '    Curnode.ID = ""
                'End If
                CurNodeNo = SCurnodeNo
                Curnode = Support.TreeViewManager.GetNode(treeScenario.Nodes, CurNodeNo)
                Support.TreeViewManager.SetSelectedNode(treeScenario, Curnode)
                Call Prepare_Right() 'JRS 09/06/03
                'mti August 2004 - end

                'JRS 04/27/03
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If VType(Session("SEL")) = 2 Then
                    cmdCustom_Click(cmdCustom, New System.EventArgs)
                End If
            End If
        Else

            '       Load Combo Box with new elements
            '       --------------------------------
            If VDim3(Session("SEL")) = 0 Or sDim2 > "" Then 'jrs 2006/05/30 dim3
                SubDimDone$ = "True"
            End If
            'jrs 2006/07/21
            If SEL1 = "" Then
                SEL1$ = sel2$
                Session("SEL1") = SEL1
            Else
                sDim2 = sel2$
                Session("sDim2") = sDim2
            End If
            SDimNum = DimNum
            SDimPoint = DimPoint
            '        DimNum = Val(Mid$(Code$, 3, 1))
            If sDim2 = "" Then 'jrs 2006/05/30 dim3
                DimNum = VDim2(Session("SEL"))
            Else
                DimNum = VDim3(Session("SEL"))
            End If

            'JRS 03/04/04
            Session("DimNum") = DimNum
            Session("SubDimDone") = SubDimDone
            Call DimLoad()

        End If
        Exit Sub


    End Sub

    'JRS 01/23/04
    Private Sub DimError()
        Dim msg As String
        msg = sel2 & " is an incorrect sub-dimension choice"

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

        'UPGRADE_WARNING: Return has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
    End Sub

    'JRS 01/23/04
    Private Sub RemoveDim1() ' Make Combo Box and Label Invisible for Subdimension(s)
        frm_dim1.Visible = False
        'jrs 2014/10/08
        lblTot2.Visible = False
        'CONVERSION_NOTE: Method 'MSComctlLib.NodeClass.EnsureVisible' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.html'.
        'treeScenario.Nodes.Item(1).EnsureVisible() 
        'CONVERSION_NOTE: Method 'AxMSComctlLib.AxTreeView.CtlRefresh' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSComctlLib.AxTreeView.CtlRefresh().html'.
        'treeScenario.CtlRefresh() 
        'UPGRADE_WARNING: Return has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
    End Sub

    Private Sub mnuDriver_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_NOTE: Name was upgraded to Name_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim Name_Renamed As String
        Dim SelNode As Microsoft.Web.UI.WebControls.TreeNode
        Dim Driver As Microsoft.Web.UI.WebControls.TreeNode
        'UPGRADE_NOTE: Region was upgraded to Region_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim Region_Renamed As Microsoft.Web.UI.WebControls.TreeNode

        Session("cur_file") = "Working File"
        SelNode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        'Check Node Types from Bottom Up

        If NodeType(SelNode) = ObjectType.otBroadCategory Then
            LoadMessage("Please select an Issue Area for adding Driver.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        blnScenarioDirty = True

        If NodeType(SelNode) = ObjectType.Otregion Then SelNode = SelNode.Parent
        If NodeType(SelNode) = ObjectType.OtDriver Then SelNode = SelNode.Parent

        SaveVCat = Val(Mid(SelNode.NodeData, 3)) 'left$(test_node.Key, 1) '
        'JRS 03/03/04
        Session("SaveVCat") = SaveVCat
        'jrs 2014/07/18
        Dim sJscript As String = ""
        sJscript = sJscript & vbCr & "   var IFsVarHelpPath=[];" & vbCr
        sJscript = sJscript & vbCr & "   var IFsVarHelpEqnPath=[];" & vbCr

        Dim VTracker(NumbVars) As Object
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        lst_varname.Items.Clear()
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Carret = "<br>" & Chr(10)
        ICount = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For i2 = 1 To NumbVars
            'JRS 02/04/04
            If Not IsDBNull(VScenCat(i2)) And Not VHidePrivateVars(i2) Then 'jrs 2019/07/11 and not VHidePrivateVars
                'new
                'UPGRADE_WARNING: Couldn't resolve default property of object VScenCat(i2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If VScenCat(i2) = SaveVCat Then 'no need of 0 for "All"
                    AddName = VScenario(i2)
                    'later take only drivers with friendly name
                    'AddName$ = AddName$ + " - " + VDef$(i2)
                    'UPGRADE_WARNING: Couldn't resolve default property of object VCat(i2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'jrs 2021/03/01 If UCase(ModelLoad) = UCase("LoadCoVaTrA") = True Or VCatName(i2) <> "Transportation" Then  'jrs 2011/05/27 VCat(i2) <> 9 Then
                    If Trim(AddName) <> "" Then
                        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        'mti August 2004 2 start
                        lst_varname.Items.Add(New ListItem(V(i2) & " - " & AddName, CStr(ICount) + "-" + CStr(VHelpID(i2)) + "/" + CStr(VHelpIDEqn(i2)))) 'JRS 2005/07/06

                        'jrs 2014/07/18
                        sJscript = sJscript & "    IFsVarHelpPath[" & lst_varname.Items.Count.ToString & "] = '" & Replace(Replace(LoadIFsVarHelpJS(VHelpID(i2)), "window.open('", ""), "');", "") & "';" & vbCr
                        sJscript = sJscript & "    IFsVarHelpEqnPath[" & lst_varname.Items.Count.ToString & "] = '" & Replace(Replace(LoadIFsVarHelpEqnJS(VHelpIDEqn(i2)), "window.open('", ""), "');", "") & "';" & vbCr

                        'mti August 2004- help
                        'lst_varname.Items(ICount).Value = CStr(VHelpID(i2)) + "/" + CStr(VHelpIDEqn(i2))
                        'mti August 2004
                        'mti August 2004 2 end

                        ICount = ICount + 1
                        'UPGRADE_WARNING: Couldn't resolve default property of object VTracker(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        VTracker(ICount) = i2
                    End If
                    'jrs 2021/03/01 End If
                End If
            End If
        Next i2

        frm_varname.Visible = True

        'mti September 2004 2
        _Opt_highlow_0.Enabled = False
        _Opt_highlow_1.Enabled = False
        _Opt_highlow_2.Enabled = False
        cmdCustom.Enabled = False
        btnSlider.Enabled = False
        'mti September 2004 2 end 

        'mti August 2004 2 -start
        lst_varname.SelectedIndex = -1 'Then Exit Sub
        'mti August 2004 2 -end 


        'CONVERSION_NOTE: Method 'System.Windows.Forms.GroupBox.BringToFront' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.BringToFront().html'.
        'frm_varname.BringToFront() 
        'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        WinFormsToWeb_FocusControl.Focus("lst_varname")

        'jrs 2014/07/18
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadHelpLists", sJscript, True)

    End Sub

    '=============================ScenarioCreate.Frm=============================
    Private Sub mnuExit_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        On Error GoTo SError
        If blnScenarioDirty Then

            If Support.TreeViewManager.CountNodes(treeScenario.Nodes) > Totcat Then '  blnScenarioDirty Then

                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                Select Case MsgBoxResult.Yes 'JRSVBNET WinFormsToWeb_MessageBox.Show("Save Changes to " & Session("cur_file") & "?", MsgBoxStyle.YesNoCancel, "IFS - Open Scenario")
                    Case MsgBoxResult.Yes
                        If UCase(Session("cur_file")) = "UNTITLED" Then
                            'mti ScenarioCreate May 2004 Uncomment later
                            'Call mnuSave_Click(mnuSave, New System.EventArgs)
                            If SaveCancelled Then
                                Exit Sub
                            End If
                        Else
                            Call SaveScenario(Session("ScenarioFileDir") & Session("cur_file"), Session("cur_file"))
                        End If
                    Case MsgBoxResult.No
                            'Do Nothing
                    Case MsgBoxResult.Cancel
                        Exit Sub
                End Select
            End If
        End If


        Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
        Session("cur_file") = ""

        'reset scenario flag
        Session("Scenarioflag") = False

        Call InitVars()

        blnScenarioDirty = False

        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        Support.FormManager.Close(Me)
SError:
    End Sub


    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/15/01
        'mti ScenarioCreate May 2004 -uncomment later
        ''Session("mnuGroupText") = "Use"
        'If mnuGroups.Text = "Use &Groups" Then
        '    mnuGroups.Text = "&Use Countries"
        '    FlagGroup = True
        'Else
        '    mnuGroups.Text = "Use &Groups"
        '    FlagGroup = False
        'End If
        Dim objSender As Coalesys.WebMenu.WebMenu
        'mti Augut 2004 3
        Dim SaveGroupInfo As Boolean
        SaveGroupInfo = Session("FlagGroup")

        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objEA.Item.Caption = "Groups" Then
            objSender.Groups("Set Group or Country").Items("Groups").IconImage = "./images/checkbox.gif"
            objSender.Groups("Set Group or Country").Items("Countries").IconImage = "./images/blank.gif"
            Session("FlagGroup") = True

        Else
            objSender.Groups("Set Group or Country").Items("Countries").IconImage = "./images/checkbox.gif"
            objSender.Groups("Set Group or Country").Items("Groups").IconImage = "./images/blank.gif"
            Session("FlagGroup") = False
            'jrs 2006/12/28
            If cmb_DIM1.Visible And SubDimNum > 1 Then
                frm_dim1.Visible = False
            End If
        End If

        'mti Augut 2004 3
        If frm_dim1.Visible And SaveGroupInfo <> CBool(Session("FlagGroup")) Then
            DimLoad()
        End If

    End Sub

    Public Sub mnuMainmenu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim response As Object
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If local_Guided_use Then
            msg = "Do you wish to exit from Guided Use?"
            'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRSVBNET response = WinFormsToWeb_Messagebox.show(msg, 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If response = 1 Then
                Session("FlagGroup") = False
                Guided_use = False
                'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_MainMenu.aspx")
            End If
        Else
            frm_varname.Visible = False

            Frm_nonscenario.Visible = False
            frm_dim1.Visible = False
            Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
            'frm_MainMenu.Show
            Support.FormManager.Close(Me)
        End If
    End Sub

    Public Sub mnuNew_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'Scenario Changes MTI- Change this whole proc

        On Error GoTo er_Open

        'mti WebForm Changes From July 04, 2004
        Session("Open_File") = ""
        'mti WebForm Changes From July 04, 2004
        'jrs 2023/07/04
        Session("Saved_file") = ""
        'jrs 2024/08/19
        Session("fpSelFilePath") = ""

        frm_varname.Visible = False

        Frm_nonscenario.Visible = False
        frm_dim1.Visible = False
        If blnScenarioDirty Then

            If InStr(1, Session("cur_file"), ".sce", 1) > 0 Then 'vbtextcompare
                msg = "Save Changes to " & Mid(Session("cur_file"), 1, InStr(1, Session("cur_file"), ".sce", 1) - 1) & "?"
            Else
                msg = "Save Changes to " & Session("cur_file") & "?"
            End If

            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            Select Case MsgBoxResult.Yes 'JRSVBNET WinFormsToWeb_MessageBox.Show(msg, MsgBoxStyle.YesNoCancel, "IFS - New Scenario")

                    'Select Case MsgBox("Save Changes to " & Session("cur_file") & "?", vbYesNoCancel, "IFS - New Scenario")

                Case MsgBoxResult.Yes
                    If UCase(Session("cur_file")) = "UNTITLED" Or Mid(UCase(Session("cur_file")), 1, 7) = "WORKING" Then

                        'mti ScenarioCreate May 2004 Uncomment later
                        'Call mnuSave_Click(mnuSave, New System.EventArgs)
                        If SaveCancelled Then
                            Exit Sub
                        End If
                    Else
                        Call SaveScenario(Session("ScenarioFileDir") & Session("cur_file"), Session("cur_file"))
                    End If


                Case MsgBoxResult.No
                        'Do Nothing
                        'Kill ScenarioFileDir$ + "Working.Sce"


                Case MsgBoxResult.Cancel
                    Exit Sub
            End Select
        End If

        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
            Kill(Session("ScenarioFileDir") & "Working.Sce")
        End If

        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(Session("ScenarioFileDir") & "Working.txt") <> "" Then
            Kill(Session("ScenarioFileDir") & "Working.txt")
        End If


        Session("cur_file") = "Untitled"

        Call InitTree()
        Call InitVars() 'initializes blnscenariodirty
        frm_varname.Visible = False
        'jrs 2024/02/27
        Session("SceLag") = 0

        Exit Sub
er_Open:


    End Sub

    Public Sub mnuNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRS 11/4/03
        bNextDirty = True
        'JRS 11/4/02 Call SaveScenario(ScenarioFileDir$ + "Working.Sce", "Working.Sce")
        'mti ScenarioCreate May 2004 Uncomment later
        'mnuSave_Click(mnuSave, New System.EventArgs)
        'JRS 11/4/03
        bNextDirty = False
        'JRS 11/8/02
        If SaveCancelled Then Exit Sub
        'CONVERSION_ISSUE: Property 'IFs.frm_Run.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET Support.FormManager.Close(frm_Run.aspx)
        'JRS 2/7/03
        Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
        'JRS 4/01/03
        'CONVERSION_ISSUE: Class System.Windows.Forms.Application from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Application.DoEvents()
        '-----------raman 17th March
        'JRSVBNET General_Run(Session("PutFdrvRun"), Session("ScenarioFileDir")) ' setting of the base file and run file
        '-----------------------------------

        'CONVERSION_ISSUE: Property 'IFs.frm_Run.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Run.aspx")
        'UPGRADE_WARNING: Form method frm_Run.ZOrder has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.BringToFront' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.BringToFront().html'.
        'frm_Run.DefInstance.BringToFront() 
        Support.FormManager.Close(Me)
        '    Call SaveScenario(ScenarioFileDir$ + "Working.Sce", "Working.Sce")
        '    Unload frm_Run
        '    '-----------raman 17th March
        '    General_Run   ' setting of the base file and run file
        '    '-----------------------------------
        '
        '    frm_Run.Visible = True
        '    frm_Run.ZOrder 0
        '    Unload Me
    End Sub

    Public Sub mnuNote_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2005/06/02
        Dim Message As String
        If Dir(Session("ScenarioFileDir") & "Working.sce") = "" Then
            Message = "The tree is empty."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
        'Changes MTI 03 July,2002
        BackFromAnnotation = True
        'Changes MTI 03 July,2002
        'jrs 2006/08/05 after clearing the tree working.txt disapears
        'jrs 2020/05/07
        Dim bLoadSceDesc As Boolean = True
        If IO.File.Exists(Session("ScenarioFileDir") & "Working.txt") Then
            'jrs 2005/05/09
            FileCopy(Session("ScenarioFileDir") & "Working.txt", Session("ScenarioFileDir") & "Working2.txt")
            'jrs 2020/05/07
            Dim fN As Integer
            fN = FreeFile()
            FileOpen(fN, Session("ScenarioFileDir") & "Working.txt", OpenMode.Input)
            Dim sline As String
            Do While Not EOF(fN)
                sline = LineInput(fN)
                If sline.Contains("Details of Implementation") Then
                    'Already loaded
                    bLoadSceDesc = False
                    Exit Do
                End If
            Loop
            FileClose(fN)

        Else
            Dim fN As Integer
            fN = FreeFile()
            FileOpen(fN, Session("ScenarioFileDir") & "Working2.txt", OpenMode.Append)
            FileClose(fN)
        End If
        Session("LoadSceDesc") = True
        'jrs 2020/05/07
        If bLoadSceDesc Then
            LoadSceDesc(Session("ScenarioFileDir") & "Working.sce")
        End If

        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.ShowDialog().html'.
        'frm_NotePad.DefInstance.ShowDialog() 
        'jrs 2015/01/09
        Session("GraphDisplayParent") = "frm_ScenarioCreate.aspx"

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "Notepad.aspx")

        'Call UpdateWorkingFile("Working.Sce", "Working.Sce")
    End Sub


    Public Sub mnuOpen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mtiwebforms May 2004 FilePicker 
        'Panel1.Visible = True
        'fileButton.Text = "Open"
        'jrs 2006/11/02
        'Session("fpPurpose") = "Scenario_Open"
        'Session("fpInitRootPath") = "~/Scenario"
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "filepicker.aspx")

        '        'Scenario Changes MTI- Change this whole proc
        '        On Error GoTo er_Open
        '        Dim HidIt As Boolean
        '        frm_varname.Visible = False
        '        Frm_nonscenario.Visible = False
        '        frm_dim1.Visible = False
        '        If blnScenarioDirty Then

        '            If InStr(1, Session("cur_file"), ".sce", 1) > 0 Then 'vbtextcompare
        '                msg = "Save Changes to " & Mid(Session("cur_file"), 1, InStr(1, Session("cur_file"), ".sce", 1) - 1) & "?"
        '            Else
        '                msg = "Save Changes to " & Session("cur_file") & "?"
        '            End If

        '            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '            Select Case MsgBoxResult.Yes 'JRSVBNET WinFormsToWeb_MessageBox.Show(msg, MsgBoxStyle.YesNoCancel, "IFS - Open Scenario")
        '                Case MsgBoxResult.Yes
        '                    'If UCase(Session("cur_file")) = "UNTITLED" Then
        '                    If UCase(Session("cur_file")) = "UNTITLED" Or Mid(UCase(Session("cur_file")), 1, 7) = "WORKING" Then
        '                        'mti ScenarioCreate May 2004 Uncomment later
        '                        'Call mnuSave_Click(mnuSave, New System.EventArgs)
        '                        If SaveCancelled Then
        '                            Exit Sub
        '                        End If
        '                    Else
        '                        Call SaveScenario(ScenarioFileDir & Session("cur_file"), Session("cur_file"))
        '                    End If
        '                Case MsgBoxResult.No
        '                    'Do Nothing
        '                Case MsgBoxResult.Cancel
        '                    Exit Sub
        '            End Select
        '        End If

        '        Dim intfile As Short


        '        'JRSVBNET cmnDlg1.DialogTitle = "Open Scenario File"

        '        'JRSVBNET cmnDlg1.Filter = "Scenario Files (*.sce)|*.sce|All Files (*.*)|*.*"

        '        'JRSVBNET cmnDlg1.FileName = ""
        '        'JRSVBNET cmnDlg1.DefaultExt = ".sce"
        '        'JRSVBNET cmnDlg1.InitDir = ScenarioFileDir

        '        'Scenario Changes 16 June 2002
        '        'JRSVBNET cmnDlg1.Flags = MSComDlg.FileOpenConstants.cdlOFNFileMustExist
        '        HidIt = False
        '        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        '        If Dir(ScenarioFileDir & "Working.Sce") <> "" Then
        '            SetAttr(ScenarioFileDir & "Working.Sce", FileAttribute.Hidden)
        '            HidIt = True
        '        End If

        '        'JRSVBNET cmnDlg1.CancelError = True
        '        'JRSVBNET cmnDlg1.ShowOpen()

        '        If HidIt Then ' Dir(ScenarioFileDir$ + "Working.Sce") <> "" Then
        '            SetAttr(ScenarioFileDir & "Working.Sce", FileAttribute.Normal)
        '            HidIt = False
        '        End If

        '        'Change mti May 28, 2002

        '        'Session("cur_file") = 'JRSVBNET cmnDlg1.FileTitle
        '        'FileCopy ScenarioFileDir$ + Session("cur_file"), ScenarioFileDir$ + "Working.sce"

        '        'JRSVBNET Session("cur_file") = 'JRSVBNET cmnDlg1.FileName
        '        FileCopy(Session("cur_file"), ScenarioFileDir & "Working.sce")
        '        'JRSVBNET Session("cur_file") = 'JRSVBNET cmnDlg1.FileTitle
        '        'End Change mti May 28, 2002

        '        Call InitTree()
        '        Call InitVars() 'initializes blnscenariodirty
        '        'JRSVBNET Session("cur_file") = 'JRSVBNET cmnDlg1.FileTitle

        '        'JRSVBNET Call BuildDrivers(('JRSVBNET cmnDlg1.FileName))

        '        'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
        '        'treeScenario.Nodes.Item(1).Selected = True 
        '        Curnode = treeScenario.Nodes.Item(1)

        '        frm_varname.Visible = False
        '        Exit Sub

        'er_Open:
        '        If HidIt Then ' Dir(ScenarioFileDir$ + "Working.Sce") <> "" Then
        '            SetAttr(ScenarioFileDir & "Working.Sce", FileAttribute.Normal)
        '            HidIt = False
        '        End If
    End Sub

    Public Sub mnuOther_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        frm_varname.Visible = False

        frm_dim1.Visible = False

        If Frm_nonscenario.Visible = True Then
            Frm_nonscenario.Visible = False
            Exit Sub
        End If

        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        If lst_nonscenario.Items.Count > 0 Then
            Frm_nonscenario.Visible = True
            'CONVERSION_NOTE: Method 'System.Windows.Forms.GroupBox.BringToFront' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.BringToFront().html'.
            'Frm_nonscenario.BringToFront() 
            'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            WinFormsToWeb_FocusControl.Focus("lst_nonscenario")
        End If

    End Sub

    Public Sub mnuPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN ***
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'frm_varname.Visible = False
        'Frm_nonscenario.Visible = False
        'frm_dim1.Visible = False
        'Dim pageline As Short
        'Dim ofont As Short
        'Dim shead, TStr As String
        'Dim Dcnt, icnt, Rcnt As Short
        'Dim Rootnode As MSComctlLib.Node
        'Dim Drivernode As MSComctlLib.Node
        'Dim Regionnode As MSComctlLib.Node
        'Dim tval As Object

        ''UPGRADE_ISSUE: Printer property Printer.FontSize was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'ofont = Printer.FontSize

        ''Printer.Print Tab(20); String(130, "-")

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()

        ''UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.FontUnderline = True
        ''UPGRADE_ISSUE: Printer property Printer.FontBold was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.FontBold = True
        ''UPGRADE_ISSUE: Printer property Printer.FontSize was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.FontSize = 18

        'shead = "Scenario File : " & Session("cur_file")

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print(TAB(20), shead)

        ''Printer.Print Tab(20); String(Len(shead$), "-")

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()

        ''UPGRADE_ISSUE: Printer property Printer.FontBold was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.FontBold = False
        ''UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.FontUnderline = False

        'For icnt = 1 To Totcat + 1 'total number of Variable Categories = Main Nodes
        '    Rootnode = treeScenario.Nodes.Item(icnt)
        '    'UPGRADE_WARNING: Couldn't resolve default property of object tval. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '    tval = Val(Mid(Rootnode.Key, 3))
        '    'UPGRADE_WARNING: Couldn't resolve default property of object tval. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '    'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        '    If tval > 0 And tval Mod 100 = 0 Then 'Broad Categories

        '        'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '        'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '        Printer.Print(" ")
        '        TStr = Rootnode.Text
        '        'UPGRADE_ISSUE: Printer property Printer.FontSize was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '        Printer.FontSize = 16
        '        'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '        Printer.FontUnderline = True

        '        'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '        'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '        Printer.Print(TAB(30), TStr)
        '        'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '        Printer.FontUnderline = False
        '        'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '        'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '        Printer.Print(" ")

        '    Else
        '        'UPGRADE_WARNING: Couldn't resolve default property of object tval. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '        tval = Val(Mid(Rootnode.Parent.Key, 3))
        '        'UPGRADE_WARNING: Couldn't resolve default property of object tval. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '        'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        '        If (Rootnode.Children > 0) And (tval > 0 And tval Mod 100 = 0) Then
        '            'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '            'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '            Printer.Print()
        '            TStr = "IssueArea : " & Rootnode.Text
        '            'UPGRADE_ISSUE: Printer property Printer.FontSize was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '            Printer.FontSize = 13
        '            'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '            Printer.FontUnderline = True
        '            'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '            'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '            Printer.Print(TAB(10), TStr)
        '            'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '            Printer.FontUnderline = False


        '            'Printer.Print String(Len(tstr$), "_")
        '            Dcnt = Rootnode.Child.Index
        '            Do While True ' While Dcnt <> Rootnode.Child.LastSibling.Index
        '                'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '                'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                Printer.Print()
        '                Drivernode = treeScenario.Nodes.Item(Dcnt)
        '                If Drivernode.Children > 0 Then
        '                    TStr = Drivernode.Text
        '                    'UPGRADE_ISSUE: Printer property Printer.FontSize was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.FontSize = 12
        '                    'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.FontUnderline = True
        '                    'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '                    'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.Print()
        '                    'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '                    'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.Print(TAB(20), TStr)
        '                    'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.FontUnderline = False


        '                    Rcnt = Drivernode.Child.Index
        '                    Do While True 'While Rcnt <> Drivernode.Child.LastSibling.Index

        '                        Regionnode = treeScenario.Nodes.Item(Rcnt)
        '                        TStr = Regionnode.Text
        '                        'UPGRADE_ISSUE: Printer property Printer.FontSize was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                        Printer.FontSize = 11
        '                        'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '                        'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                        Printer.Print()
        '                        'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '                        'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                        Printer.Print(TAB(25), TStr, "  ::  " & PStr(Regionnode))
        '                        'Reset counter to next node index
        '                        If Rcnt = Drivernode.Child.LastSibling.Index Then
        '                            Exit Do
        '                        Else
        '                            Rcnt = treeScenario.Nodes(Rcnt).Next.Index
        '                        End If

        '                    Loop  'Wend

        '                Else 'no subdim

        '                    TStr = Drivernode.Text
        '                    'UPGRADE_ISSUE: Printer property Printer.FontSize was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.FontSize = 12
        '                    'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.FontUnderline = True
        '                    'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '                    'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.Print()
        '                    'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '                    'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.Print(TAB(20), TStr, "  ::  " & PStr(Drivernode))
        '                    'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '                    Printer.FontUnderline = False

        '                End If


        '                ' Reset counter to next node index.
        '                If Dcnt = Rootnode.Child.LastSibling.Index Then
        '                    Exit Do
        '                Else
        '                    Dcnt = treeScenario.Nodes(Dcnt).Next.Index
        '                End If
        '            Loop  'Wend


        '        End If
        '    End If
        'Next icnt

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()

        ''UPGRADE_ISSUE: Printer method Printer.NewPage was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.NewPage()
        ''UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.EndDoc()
        'JRSVBNET *** END ***
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub mnuRegion_Click()
        Dim msg As String
        Dim SELN As String
        Dim CalcType As Object
        Dim CalcRunning As Object
        Dim SumOn As Object
        Dim ii As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_NOTE: Name was upgraded to Name_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim Name_Renamed As String
        Dim SelNode As Microsoft.Web.UI.WebControls.TreeNode
        Dim Driver As Microsoft.Web.UI.WebControls.TreeNode
        'UPGRADE_NOTE: Region was upgraded to Region_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim Region_Renamed As Microsoft.Web.UI.WebControls.TreeNode

        Dim otNt As ObjectType
        SelNode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        otNt = NodeType(SelNode)
        If otNt = ObjectType.otIssueArea Or otNt = ObjectType.otBroadCategory Then 'otTop
            LoadMessage("Please select a Driver first; then add region or other dimension.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        If NodeType(SelNode) = ObjectType.OtDriver Then
            'the very first entry of driver has to
            'have a child(region/subdim) if the
            'driver has subdims: (brought through
            'lstvarname_click)
            If SelNode.Nodes.Count = 0 Then
                LoadMessage("Driver has no subdimensions.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
        End If

        blnScenarioDirty = True

        'if region/subdim clicked go to parent(=driver)
        If NodeType(SelNode) = ObjectType.Otregion Then SelNode = SelNode.Parent

        SaveVCat = Val(Mid(SelNode.Parent.NodeData, 3)) 'left$(test_node.Key, 1) '
        'JRS 03/03/04
        Session("SaveVCat") = SaveVCat

        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Child' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Child.html'.
        'mtiwebformsMarch2004 (2)ScenarioCreate
        'JRSVBNET Call FindVarNumb(chk, GetFirstToken(Mid(SelNode.Child.NodeData, 3), ","))
        chk = IfsVars.Item(GetFirstToken(Mid(SelNode.Nodes.Item(0).NodeData, 3), ",")).Index
        'UPGRADE_WARNING: Couldn't resolve default property of object chk. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("SEL") = chk


        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VDim1(Session("SEL")) = 1 And Session("FlagGroup") = True Then
            groupon = True
        Else
            groupon = False
        End If
        '
        '   Check for Attempt to Compute WW variable across groups
        '   ------------------------------------------------------
        ' Code Omitted

        '   Initialize Dimensions, Reset Variable Pointer
        '   ---------------------------------------------
        'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label1 = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object Label2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label2 = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label3 = ""
        SEL1 = ""
        sel2 = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object VPTR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        VPTR = 0
        'JRS 03/04/04
        Session("VPTR") = VPTR

        'jrs 2006/12/29
        Session("SEL1") = SEL1
        '   Check if variable has subdimensions
        '   -----------------------------------
        SubDimNum = 0
        If VDim1(Session("SEL")) > 0 Then
            SubDimNum = 1 'have at least 1 dimension
        End If
        If VDim2(Session("SEL")) > 0 Then
            SubDimNum = 2 'have both dimensions
        End If
        If VDim3(Session("SEL")) > 0 Then 'jrs 2006/05/30 dim3
            SubDimNum = 3 'have 3 dimensions
        End If
        'JRS 03/04/04
        Session("SubDimNum") = SubDimNum

        If SubDimNum = 0 Then 'no dimension
            'UPGRADE_WARNING: Couldn't resolve default property of object VPTR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            VPTR = 1
            'UPGRADE_WARNING: Couldn't resolve default property of object SumOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ElseIf SumOn = True And SubDimNum = 1 Then 'Sum Across Single Dim
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DimNum = VDim1(Session("SEL"))
            'JRS 03/04/04
            Session("DimNum") = DimNum
            'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label1 = "ALL"
            'UPGRADE_WARNING: Couldn't resolve default property of object Label2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label2 = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label3 = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For ii = 1 To MaxVal(DimNum) - 1
                'UPGRADE_WARNING: Couldn't resolve default property of object VPTR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                VPTR = ii 'Variable pointer shift reflecting dimensionality
                'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If ii = MaxVal(DimNum) - 1 Then 'Finished with summation across all
                    'UPGRADE_WARNING: Couldn't resolve default property of object SumOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    SumOn = False
                    'UPGRADE_WARNING: Couldn't resolve default property of object CalcType. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object CalcRunning. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If CalcType = "None" Then CalcRunning = False
                End If
            Next ii
            'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            WinFormsToWeb_FocusControl.Focus("lst_varname")
        Else 'There are two dimensions; start with 1st
            SubDimDone = "False" 'remember to come back to 2nd dimension
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DimNum = VDim1(Session("SEL")) 'identify 1st dimension
            'JRS 03/04/04
            Session("DimNum") = DimNum
            Session("SubDimDone") = SubDimDone
            Call DimLoad()
        End If
        'JRS 03/04/04
        Session("VPTR") = VPTR

        Exit Sub


BadVariable:
        msg = SELN & " is an incorrect variable name"

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")



    End Sub

    Public Sub mnuReset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim f As Object
        Dim fs As Object
        reset_bool = True

        '----------------raman 15th May -------------
        fs = CreateObject("Scripting.FileSystemObject")
        'UPGRADE_WARNING: Couldn't resolve default property of object fs.GetFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        f = fs.GetFile(Session("ScenarioFileDir") & "Working.Sce")
        'UPGRADE_WARNING: Couldn't resolve default property of object f.Delete. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        f.Delete()
        'frm_varname.Visible = False
        frm_ScenarioCreate_Load(Me, New System.EventArgs)
        first_time = True
        'UPGRADE_WARNING: Form event frm_ScenarioCreate.Activated has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        frm_ScenarioCreate_Activated(Me, New System.EventArgs)
    End Sub

    Public Sub mnuSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2005/06/14
        If Dir(Session("ScenarioFileDir") & "Working.sce") = "" Then
            Dim Message As String
            Message = "The Tree is empty."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
        'mtiwebforms May 2004 FilePicker 
        'Panel1.Visible = True
        'fileButton.Text = "Open"
        Call UpdateWorkingFile(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")

        'jrs 2005/05/24
        'Session("fpPurpose") = "Scenario_Save"
        'Session("fpInitRootPath") = "~/Scenario"
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "filepicker.aspx")
        frm_FileName.Visible = True
        'jrs 2023/05/02
        If StandAloneMode Then
            divSaveInstructions.InnerText = "Files can be retrieved under Add Scenario Component/a User Defined Sceanrios/, or under Open/a User Defined Scenarios/"
        Else
            divSaveInstructions.InnerText = "(e.g. your ID)Files are stored for 1 month after being saved, and can be retrieved under Add Scenario Component/a User Defined Sceanrios/Your ID, or under Open/Other/a User Defined Scenarios/Your ID."
        End If
        'jrs 2026/06/11
        frm_VarDesc.Visible = False

        '        'Scenario Changes MTI- Change this whole proc
        '        On Error GoTo SError



        '        frm_varname.Visible = False
        '        Frm_nonscenario.Visible = False
        '        frm_dim1.Visible = False
        '        Dim Extpos As Short
        '        Dim TmpFName As String
        '        Dim HidIt As Boolean
        '        'JRS 11/02/03
        '        Dim ans As Short
        '        If Not blnScenarioDirty And Not bNextDirty Then
        '            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '            'JRSVBNET ans = WinFormsToWeb_MessageBox.Show("The scenario has not been changed.  Are you sure you want to save it again?", MsgBoxStyle.YesNo, "Scenario")
        '            If ans = MsgBoxResult.Yes Then
        '                blnScenarioDirty = True
        '            End If
        '        End If
        '        'JRS 11/11/02
        '        If blnScenarioDirty Then

        '            'JRSVBNET cmnDlg1.DialogTitle = "Save Scenario File"

        '            'JRSVBNET cmnDlg1.Filter = "Scenario Files (*.sce)|*.sce|All Files (*.*)|*.*"

        '            ''JRSVBNET cmnDlg1.Action = 1
        '            HidIt = False
        '            If UCase(Session("cur_file")) = "WORKING.SCE" Then
        '                SetAttr(ScenarioFileDir & Session("cur_file"), FileAttribute.Hidden)
        '                HidIt = True
        '                'JRSVBNET cmnDlg1.FileName = ""
        '            Else
        '                'JRSVBNET cmnDlg1.FileName = Session("cur_file")
        '            End If

        '            'JRSVBNET cmnDlg1.DefaultExt = ".sce"
        '            'JRSVBNET cmnDlg1.InitDir = ScenarioFileDir
        '            'JRSVBNET cmnDlg1.Flags = MSComDlg.FileOpenConstants.cdlOFNOverwritePrompt
        '            'JRSVBNET cmnDlg1.CancelError = True
        '            'JRSVBNET cmnDlg1.ShowSave()
        '            Panel1.Visible = True
        '            fileButton.Text = "Save"
        '            'Scenario Changes 16 June 2002
        '            Do While True
        '                'JRSVBNET TmpFName = 'JRSVBNET cmnDlg1.FileTitle
        '                If UCase(TmpFName) = "WORKING.SCE" Then
        '                    'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '                    WinFormsToWeb_MessageBox.Show("Working.Sce is a System File. Please choose a different name.", MsgBoxStyle.Information)
        '                    'JRSVBNET cmnDlg1.ShowSave()
        '                Else
        '                    Exit Do
        '                End If
        '            Loop

        '            If HidIt Then 'UCase(Session("cur_file")) = "WORKING.SCE" Then
        '                SetAttr(ScenarioFileDir & Session("cur_file"), FileAttribute.Normal)
        '                HidIt = False
        '            End If

        '            If Support.TreeViewManager.CountNodes(treeScenario.Nodes) > Totcat Then '  blnScenarioDirty Then
        '                'Make sure the tree contains some node
        '                'other than variable categories and the scenario name
        '                'JRSVBNET Call SaveScenario(('JRSVBNET cmnDlg1.FileName), ('JRSVBNET cmnDlg1.FileTitle))
        '            Else
        '                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '                WinFormsToWeb_MessageBox.Show("Scenario empty - it will not be saved.", MsgBoxStyle.Information)
        '                Exit Sub
        '            End If


        '            Extpos = InStr(1, TmpFName, ".sce", 1)
        '            If Extpos > 0 Then
        '                Session("cur_file") = Mid(TmpFName, 1, Extpos - 1)
        '            Else
        '                Session("cur_file") = TmpFName
        '            End If

        '            Extpos = InStr(1, Session("cur_file"), ".sce", 1) 'vbtextcompare
        '            If Extpos > 0 Then
        '                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Scenario Development and Change" & ": " & Mid(Session("cur_file"), 1, Extpos - 1)
        '            Else
        '                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Scenario Development and Change" & ": " & Session("cur_file")
        '            End If

        '            blnScenarioDirty = False

        '        End If
        '        SaveCancelled = False
        '        Exit Sub

        'SError:
        '        If HidIt Then 'UCase(Session("cur_file")) = "WORKING.SCE" Then
        '            SetAttr(ScenarioFileDir & Session("cur_file"), FileAttribute.Normal)
        '            HidIt = False
        '        End If
        '        SaveCancelled = True
    End Sub




    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_WARNING: Event Opt_highlow.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Opt_highlow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Dim Index As Short = Opt_highlow.IndexOf(eventSender)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


            Dim TScenPref As String
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(treeScenario.SelectedItem.Index). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
            'mtiwebformsMarch2004 (2)ScenarioCreate
            'mti September 2004 2
            'TScenPref = Session("LinePrefix")(CType(Session("Scenario_CurNodeFlatIndex"), Integer)).ToString
            TScenPref = Session("LinePrefix")(TreeIndexToFlatIndex(treeScenario.Nodes, treeScenario.SelectedNodeIndex.ToString))
            'mti September 2004 2 -end 
            If TScenPref <> "" Then
                blnScenarioDirty = True
                If Mid(TScenPref, 1, 3) = "CUS" Then
                    'mti August 2004 -later
                    'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    'If True Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("You will lose the customization. OK to proceed?", MsgBoxStyle.YesNo, "IFS - Scenario") = MsgBoxResult.No Then
                    '    Opt_highlow(Index).Checked = False
                    '    Exit Sub
                    'End If
                    'mti August 2004
                End If
                Select Case Index
                    Case 0 'high
                        'mti September 2004 1 -start
                        _Opt_highlow_1.Checked = False '
                        _Opt_highlow_2.Checked = False '
                        'mti September 2004 1 -end
                        Call Prepare_high()
                    Case 1 'low
                        'mti September 2004 1 -start
                        _Opt_highlow_0.Checked = False '
                        _Opt_highlow_2.Checked = False '
                        'mti September 2004 1 -end
                        Call Prepare_low()
                    Case 2 'medium
                        'mti September 2004 1 -start
                        _Opt_highlow_0.Checked = False '
                        _Opt_highlow_1.Checked = False '
                        'mti September 2004 1 -end
                        Call Prepare_Medium()
                            'add record to driver
                    Case 3 'custom
                        Call Prepare_Custom()
                End Select
            End If
        End If
        'mti WebForm Changes From June 26, 2004
        Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")

    End Sub
    '=============================ScenarioCreate.Frm=============================
    Function AddChildNode(ByRef TargetTree As Microsoft.Web.UI.WebControls.TreeView, ByRef NodeText As String, ByRef KeyText As String, ByRef AddNodeText As Boolean, ByRef TargetIndex As String, Optional ByRef Nodecolor As Short = 0) As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mtiwebformsMarch2004 (2)ScenarioCreate - whole proc
        'Attempts to add a Child at (targetindex) node of TargetTree
        'return value node index (integer)


        'Checks if a child with same name already exists

        'if not
        'adds a child with the text
        'the key of the new node is keyprefix & Text
        'returns node index of newborn

        'if node exists
        'returns node index of newborn
        Dim i As Short
        Dim strC As String
        Dim Targetnode As Microsoft.Web.UI.WebControls.TreeNode
        Dim Newborn As Microsoft.Web.UI.WebControls.TreeNode

        'CONVERSION_TODO: The equivalent of Property 'AxMSComctlLib.AxTreeView.Scroll' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        Support.TreeViewManager.SetScroll(treeScenario, False)

        'JRS 03/06/04 Targetnode = TargetTree.Nodes.Item(TargetIndex)
        'Targetnode = Support.TreeViewManager.GetNode(TargetTree.Nodes, TargetIndex)
        Targetnode = TargetTree.GetNodeFromIndex(TargetIndex)
        If Targetnode.Nodes.Count > 0 Then ' There are Drivers (or Child Nodes)
            For i = 0 To Targetnode.Nodes.Count - 1
                strC = Trim(Targetnode.Nodes.Item(i).Text)
                If strC = NodeText Then
                    AddChildNode = Targetnode.Nodes.Item(i).GetNodeIndex
                    'JRS 10/06/03
                    'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
                    'Targetnode.Child.Selected = True 
                    TargetTree.SelectedNodeIndex = AddChildNode
                    Exit Function
                End If
            Next
        End If
        If AddNodeText Then
            'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Add' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'Newborn = Support.TreeViewManager.AddNode(treeScenario.Nodes, Targetnode, 4, KeyText & NodeText, NodeText)
            Newborn = Support.TreeViewManager.AddNode(treeScenario.Nodes, Targetnode, 4, KeyText & NodeText, NodeText)
        Else
            'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Add' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            Newborn = Support.TreeViewManager.AddNode(treeScenario.Nodes, Targetnode, 4, KeyText, NodeText)
        End If

        'CONVERSION_NOTE: Method 'MSComctlLib.NodeClass.EnsureVisible' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.EnsureVisible().html'.
        'JRS 03/09/04
        If Not Targetnode.Expanded Then
            Targetnode.Expanded = True
        End If
        Targetnode.Expanded = True

        'mtiwebformsMarch2004 (2)ScenarioCreate
        If Targetnode.Parent.GetType Is Targetnode.GetType Then
            CType(Targetnode.Parent, TreeNode).Expanded = True
        End If
        'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
        'mtiwebformsMarch2004 (2)ScenarioCreate
        TargetTree.SelectedNodeIndex = Newborn.GetNodeIndex()
        'Support.TreeViewManager.SetSelectedNode(TargetTree, Newborn)

        If Nodecolor > 0 Then
            'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.ForeColor' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.ForeColor.html'.
            'JRSVBNET treeScenario.SelectedItem.ForeColor = System.Convert.ToUInt32(QBColor(Nodecolor))
        End If

        'jrs 2005/04/25 I'm using property Target to store the index id, trying to simulate the index in VB60
        Newborn.Target = Trim(Str(Support.TreeViewManager.CountNodes(treeScenario.Nodes)))

        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
        AddChildNode = Newborn.GetNodeIndex

        'CONVERSION_TODO: The equivalent of Property 'AxMSComctlLib.AxTreeView.Scroll' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        treeScenario.Height = New System.Web.UI.WebControls.Unit(467)
        'jrs 2011/11/22
        treeScenario.Width = New System.Web.UI.WebControls.Unit(467)
        'Support.TreeViewManager.SetScroll(treeScenario, True)

    End Function
    '=============================ScenarioCreate.Frm=============================
    Private Sub Prepare_Right()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Set up Screen
        '   -------------

        lbl_hint.Visible = False
        pnlMaxmin.Visible = True


        chk = IfsVars.Item(GetFirstToken(Mid(Curnode.NodeData, 3), ",")).Index

        'mti webform scenario slider May 20, 2004
        'txtMax.Text = CType(VMax(Session("SEL")), String).ToString
        'txtMin.Text = CType(VMin(Session("SEL")), String).ToString
        'txtSliderVal.Text = txtMin.Text
        'mti WebForm Changes From July 04, 2004
        'SetSliderMaxMin()

        'UPGRADE_WARNING: Couldn't resolve default property of object chk. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("SEL") = chk
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VShiftDown(Session("SEL")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VShiftUp(Session("SEL")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

        'mti September 2004 3
        If IsDBNull(VShiftUp(Session("SEL"))) Then VShiftUp(Session("SEL")) = 0 'protect against holes in IFsVar.mdb
        If IsDBNull(VShiftDown(Session("SEL"))) Then VShiftDown(Session("SEL")) = 0
        'If IsDBNull(VMax(Session("SEL"))) Then VMax(Session("SEL")) = 0
        'If IsDBNull(VMin(Session("SEL"))) Then VMin(Session("SEL")) = 0

        'mtiwebformsMarch2004 (2)ScenarioCreate
        If VShiftUp(Session("SEL")) = 0 And VShiftDown(Session("SEL")) = 0 Then 'Only allow custom
            _Opt_highlow_0.Visible = False '_Opt_highlow_0
            _Opt_highlow_1.Visible = False
            _Opt_highlow_2.Text = "Base"
        Else
            _Opt_highlow_0.Visible = True
            _Opt_highlow_1.Visible = True
            _Opt_highlow_2.Text = "Base"
        End If
        'end mtiwebformsMarch2004 (2)ScenarioCreate

        blnSettingYears = True
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VShiftYears(Session("SEL")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If IsDBNull(VShiftYears(Session("SEL"))) Then VShiftYears(Session("SEL")) = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VShiftYears(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'mti August 2004
        cmbShiftYears.SelectedIndex = Val(VShiftYears(Session("SEL"))) '+ 1
        blnSettingYears = False

        'JRS 04/27/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VType(Session("SEL")) <> 2 Then
            frm_options.Visible = True
        End If
        'mtiwebformsMarch2004 (2)ScenarioCreate
        _Opt_highlow_2.Checked = True 'will goto click?

        'mti September 2004 1 -start
        _Opt_highlow_0.Checked = False '
        _Opt_highlow_1.Checked = False '
        'mti September 2004 1 -end

        Call Prepare_Medium()

        'jrs 2023/07/11
        lblDesc.Text = V(Session("SEL")) & " - " & Curnode.Parent.Text & " - " & VUnits(Session("SEL"))

        'jrs 2026/04/17
        If VExpl(Session("SEL")) > "" Then
            lblDesc.Text = lblDesc.Text & "<br><br>Explanation: " & VExpl(Session("SEL"))
        End If

        'JRS 04/27/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VType(Session("SEL")) <> 2 Then
            'JRSVBNET VSc_slider.Visible = True
            'LBLSLIDERVAL.Visible = True
            pnlSlider.Visible = True
            Frm_Custom.Visible = True
            frm_time.Visible = True
            'jrs 2023/07/11
            frm_VarDesc.Visible = True
        End If
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub Prepare_Medium() 'mtiwebformsMarch2004 (2)ScenarioCreate
        Dim j As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRS 03/09/04 j = CurNodeNo 'change to curnode.index
        'j = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
        'mtiwebformsMarch2004 (2)ScenarioCreate
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        'the following will be used as k in frm_scenariocreatePego
        Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        j = Session("Scenario_CurNodeFlatIndex")

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("LinePrefix")(j) = "MEDIUM," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        j = Session("Node2Plot")(j)
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("tVARN")(1, j) <> "" Then
            chk = IfsVars.Item(Session("tVARN")(1, j)).Index
        Else
            frm_options.Visible = False
            lblJS.Visible = False
            'JRSVBNET VSc_slider.Visible = False

            'LBLSLIDERVAL.Visible = False
            pnlSlider.Visible = False

            Frm_Custom.Visible = False
            frm_time.Visible = False
            Frm_nonscenario.Visible = False
            lbl_hint.Visible = True
            pnlMaxmin.Visible = False
            'jrs 2023/07/11
            frm_VarDesc.Visible = False

            Exit Sub
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object chk. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("SEL") = chk
        'JRS 07/16/03
        'FlagGroup = CheckGroups(Session("tGR1N")(j))

        'txtMax.Text = CType(VMax(Session("SEL")), String).ToString
        'txtMin.Text = CType(VMin(Session("SEL")), String).ToString
        'txtSliderVal.Text = txtMin.Text
        'mti WebForm Changes From July 04, 2004
        'SetSliderMaxMin()
        setslider(j)
        'mti WebForm Changes From July 04, 2004

        blnSettingYears = True
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VShiftYears(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'mti August 2004
        cmbShiftYears.SelectedIndex = Val(VShiftYears(Session("SEL"))) '- 1
        blnSettingYears = False
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Call ReadMedium(j)
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Call setslider(j)

        Call GraphUpdate()

        blnSettingSlider = True

        'JRS 08/07/02 ERROR TRAPING BEGIN
        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If False Then 'JRSVBNET (((VSc_slider.Maximum - VSc_slider.LargeChange + 1) > Session("VarVal") * ValMult) And (VSc_slider.Minimum > Session("VarVal") * ValMult)) Or (((VSc_slider.Maximum - VSc_slider.LargeChange + 1) < Session("VarVal") * ValMult) And (VSc_slider.Minimum < Session("VarVal") * ValMult)) Then
            LoadMessage("Value out Of range", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
        'JRS 08/07/02 ERROR TRAPING END
        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET VSc_slider.Value = Session("VarVal") * ValMult
        blnSettingSlider = False
        'JRS 07/07/03 LBLSLIDERVAL.Caption = Format(Session("VarVal"), "0.0##") '"general number") '"Standard")
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'LBLSLIDERVAL.Text = CStr(System.Math.Round(Session("VarVal"), 3))
        'txtSliderVal.Text = CStr(System.Math.Round(Session("VarVal"), 3))

        'JRS 04/27/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VType(Session("SEL")) <> 2 Then
            lblJS.Visible = True
        End If
        Scenariotype = "MEDIUM"
        ValStr = ""



        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.ForeColor' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.ForeColor.html'.
        'JRSVBNET treeScenario.SelectedItem.ForeColor = System.Convert.ToUInt32(QBColor(MEDIUMCOLOR)) '2

        cmbShiftYears.Enabled = False
        lblFullyCustomized.Visible = False
        lblCustomized.Visible = False
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub GraphUpdate()
        'mtiwebformsMarch2004 (2)ScenarioCreate
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        'mti WebForm Changes From July 04, 2004
        'jrs 2005/05/24 Session("grfMax") = txtMax.Text
        'jrs 2005/05/24 Session("grfMin") = txtMin.Text
        'jrs 2005/05/24
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

        'JRS 2005/06/10
        If VType(Session("SEL")) <> 2 Then
            'jrs 2011/11/29
            frm_graph.Visible = True

            lblJS.Visible = True
        End If

        'jrs 2011/10/07
        If Session("grfMax") = Session("grfMin") Then
            Session("grfMax") = Session("grfMax") + 1
        End If

        PackJS()
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub HoldNodeInfo()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Increment Count
        '   ---------------

        Session("tCOUNTE") = Session("tCOUNTE") + 1

        Session("Node2Plot")(TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)) = Session("tCOUNTE")

        '   Save variable name and units
        '   -----------------------------

        Session("tVARN")(1, Session("tCOUNTE")) = V(Session("SEL"))
        Session("tVARN")(2, Session("tCOUNTE")) = VDef(Session("SEL"))
        Session("tVARN")(3, Session("tCOUNTE")) = CStr(VHelpID(Session("SEL"))) 'VCat(Session("SEL"))
        Session("tGR4N")(Session("tCOUNTE")) = VUnits(Session("SEL"))

        Session("VL")(Session("tCOUNTE")) = VarPoint(Session("SEL")) + VPTR - 1

        Session("tFinYear")(Session("tCOUNTE")) = Session("LastYear") - Session("FirstYear") + 1



        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub Prepare_high() 'mtiwebformsMarch2004 (2)ScenarioCreate
        Dim LowEnd As Object
        Dim HighEnd As Object
        Dim valuetoplot As Object
        Dim VLine As String
        Dim j As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRS 03/09/04 j = CurNodeNo 'curnode.index??
        'j = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        j = Session("Scenario_CurNodeFlatIndex")        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("LinePrefix")(j) = "HIGH" & VB6.Format(Trim(cmbShiftYears.SelectedValue), "0000") & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        j = Session("Node2Plot")(j)

        chk = IfsVars.Item(Session("tVARN")(1, j)).Index
        Session("SEL") = chk
        'txtMax.Text = CType(VMax(Session("SEL")), String).ToString
        'txtMin.Text = CType(VMin(Session("SEL")), String).ToString
        'txtSliderVal.Text = txtMin.Text
        'mti WebForm Changes From July 04, 2004
        'SetSliderMaxMin()
        setslider(j)
        'mti WebForm Changes From July 04, 2004
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
        If Not IsDBNull(VShiftUp(Session("SEL"))) Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Tag' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Tag.html'.
            'VLine = BuildShiftStr(j, VShiftUp(Session("SEL")), "Up", Val(Trim(cmbShiftYears.SelectedValue)), VMax(Session("SEL")), Curnode.ID = "Group")
            Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
            VLine = BuildShiftStr(j, VShiftUp(Session("SEL")), "Up", Val(Trim(cmbShiftYears.SelectedValue)), VMax(Session("SEL")), Curnode.ID = "Group", Session("PutFdrvRun"), Session("VL"), Session("tGR1N"), Session("tGR2N"), Session("tGR3N"), Session("tGR4N"),
            Session("FirstYear"), Session("LastYear"), Session("FdrvRun"), Session("SEL"), Session("RunFiles"), Session("IFs_MDB2Active"), Session("tFinYear"), Session("PercentOn"),
            Session("ShortSce"), Session("ActiveBase"), Session("bLoadForDisplaySuccess"), Me, Session("ShortSce"), Session("FileForDisplay"), Session("CurrencyVal"), Session("mnuSQLite"))
        Else
            LoadMessage("Shift Up Value Not found!", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If


        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Call ParseCustomStr(VLine, j, Session("SceLag"))

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Call setslider(j)

        lblJS.Visible = True

        Call GraphUpdate()
        blnSettingSlider = True

        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object valuetoplot. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        valuetoplot = Session("VarVal") * ValMult
        'UPGRADE_WARNING: Couldn't resolve default property of object AMAX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object HighEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET HighEnd = AMAX((VSc_slider.Minimum), ((VSc_slider.Maximum - VSc_slider.LargeChange + 1)))
        'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object LowEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET LowEnd = Amin((VSc_slider.Minimum), ((VSc_slider.Maximum - VSc_slider.LargeChange + 1)))
        'UPGRADE_WARNING: Couldn't resolve default property of object HighEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object valuetoplot. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If valuetoplot > HighEnd Then
            'UPGRADE_WARNING: Couldn't resolve default property of object HighEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET If VSc_slider.Minimum > (VSc_slider.Maximum - VSc_Slider.LargeChange + 1) Then VSc_slider.Minimum = HighEnd
            'seems strange, but when have negative value, min exeeds max
            'UPGRADE_WARNING: Couldn't resolve default property of object HighEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET If (VSc_slider.Maximum - VSc_slider.LargeChange + 1) > VSc_slider.Minimum Then VSc_slider.Maximum = (HighEnd + VSc_slider.LargeChange - 1)
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object LowEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object valuetoplot. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If valuetoplot < LowEnd Then
            'UPGRADE_WARNING: Couldn't resolve default property of object LowEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET If (VSc_slider.Maximum - VSc_slider.LargeChange + 1) < VSc_slider.Minimum Then VSc_slider.Maximum = (LowEnd + VSc_slider.LargeChange - 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object LowEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET If VSc_slider.Minimum < (VSc_slider.Maximum - VSc_slider.LargeChange + 1) Then VSc_slider.Minimum = LowEnd
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET VSc_slider.Value = Session("VarVal") * ValMult
        blnSettingSlider = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'LBLSLIDERVAL.Text = VB6.Format(Session("VarVal"), "0.0##") '"general number") '"Standard")
        'txtSliderVal.Text = VB6.Format(Session("VarVal"), "0.0##") '"general number") '"Standard") 'CStr(System.Math.Round(Session("VarVal"), 3))

        Scenariotype = "HIGH"
        ValStr = ""
        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.ForeColor' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.ForeColor.html'.
        'JRSVBNET treeScenario.SelectedItem.ForeColor = System.Convert.ToUInt32(QBColor(HIGHCOLOR))

        cmbShiftYears.Enabled = True
        lblFullyCustomized.Visible = False
        lblCustomized.Visible = False
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub Prepare_low() 'mtiwebformsMarch2004 (2)ScenarioCreate
        Dim LowEnd As Object
        Dim HighEnd As Object
        Dim valuetoplot As Object
        Dim VLine As String
        Dim j As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRS 03/09/04 j = CurNodeNo 'curnode.Index
        'j = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        j = Session("Scenario_CurNodeFlatIndex")
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("LinePrefix")(j) = "LOW" & VB6.Format(Trim(cmbShiftYears.SelectedValue), "0000") & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        j = Session("Node2Plot")(j)
        chk = IfsVars.Item(Session("tVARN")(1, j)).Index
        Session("SEL") = chk

        'txtMax.Text = CType(VMax(Session("SEL")), String).ToString
        'txtMin.Text = CType(VMin(Session("SEL")), String).ToString
        'txtSliderVal.Text = txtMin.Text
        'mti WebForm Changes From July 04, 2004
        'SetSliderMaxMin()
        setslider(j)
        'mti WebForm Changes From July 04, 2004
        'JRS 07/16/03
        'FlagGroup = CheckGroups(Session("tGR1N")(j))
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
        If Not IsDBNull(VShiftDown(Session("SEL"))) Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Tag' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Tag.html'.
            Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
            VLine = BuildShiftStr(j, VShiftDown(Session("SEL")), "Down", Val(Trim(cmbShiftYears.SelectedValue)), VMin(Session("SEL")), Curnode.ID = "Group", Session("PutFdrvRun"), Session("VL"), Session("tGR1N"), Session("tGR2N"), Session("tGR3N"), Session("tGR4N"),
            Session("FirstYear"), Session("LastYear"), Session("FdrvRun"), Session("SEL"), Session("RunFiles"), Session("IFs_MDB2Active"), Session("tFinYear"), Session("PercentOn"),
            Session("ShortSce"), Session("ActiveBase"), Session("bLoadForDisplaySuccess"), Me, Session("ShortSce"), Session("FileForDisplay"), Session("CurrencyVal"), Session("mnuSQLite"))
        Else
            LoadMessage("Low Or Shift Down Value Not found!", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Call ParseCustomStr(VLine, CShort(j), Session("SceLag"))

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Call setslider(CShort(j))

        lblJS.Visible = True

        Call GraphUpdate()

        blnSettingSlider = True

        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object valuetoplot. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        valuetoplot = Session("VarVal") * ValMult
        'UPGRADE_WARNING: Couldn't resolve default property of object AMAX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object HighEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET HighEnd = AMAX((VSc_slider.Minimum), ((VSc_slider.Maximum - VSc_slider.LargeChange + 1)))
        'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object LowEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET LowEnd = Amin((VSc_slider.Minimum), ((VSc_slider.Maximum - VSc_slider.LargeChange + 1)))
        'UPGRADE_WARNING: Couldn't resolve default property of object HighEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object valuetoplot. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If valuetoplot > HighEnd Then
            'UPGRADE_WARNING: Couldn't resolve default property of object HighEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET If VSc_slider.Minimum > (VSc_slider.Maximum - VSc_slider.LargeChange + 1) Then VSc_slider.Minimum = HighEnd
            'seems strange, but when have negative value, min exeeds max
            'UPGRADE_WARNING: Couldn't resolve default property of object HighEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET If (VSc_slider.Maximum - VSc_slider.LargeChange + 1) > VSc_slider.Minimum Then VSc_slider.Maximum = (HighEnd + VSc_slider.LargeChange - 1)
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object LowEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object valuetoplot. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If valuetoplot < LowEnd Then
            'UPGRADE_WARNING: Couldn't resolve default property of object LowEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET If (VSc_slider.Maximum - VSc_slider.LargeChange + 1) < VSc_slider.Minimum Then VSc_slider.Maximum = (LowEnd + VSc_slider.LargeChange - 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object LowEnd. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET If VSc_slider.Minimum < (VSc_slider.Maximum - VSc_slider.LargeChange + 1) Then VSc_slider.Minimum = LowEnd
        End If

        'JRS 2/15/03 RANGE
        Dim iVal As Short
        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        iVal = Session("VarVal") * ValMult
        'JRSVBNET *** BEGIN ***
        'If (iVal < (VSc_slider.Maximum - VSc_slider.LargeChange + 1)) Then
        '    iVal = (VSc_slider.Maximum - VSc_slider.LargeChange + 1)
        'ElseIf (iVal > VSc_slider.Minimum) Then
        '    iVal = VSc_slider.Minimum
        'End If
        'VSc_slider.Value = iVal
        'JRSVBNET  *** END ***
        ' VSc_slider.Value = Session("VarVal") * ValMult

        blnSettingSlider = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'LBLSLIDERVAL.Text = VB6.Format(Session("VarVal"), "0.0##") '"general number") '"Standard")
        'txtSliderVal.Text = VB6.Format(Session("VarVal"), "0.0##") '"general number") '"Standard")

        Scenariotype = "LOW"
        ValStr = ""

        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.ForeColor' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.ForeColor.html'.
        'JRSVBNET treeScenario.SelectedItem.ForeColor = System.Convert.ToUInt32(QBColor(LOWCOLOR)) 'blue

        cmbShiftYears.Enabled = True

        lblFullyCustomized.Visible = False
        lblCustomized.Visible = False

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub Prepare_Custom()
        Dim j As Short 'Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Dim k As Short
        'mtiwebformsMarch2004 (2)ScenarioCreate
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        k = Session("Scenario_CurNodeFlatIndex")

        ''JRS 03/09/04 k = CurNodeNo 'node pointer
        'k = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
        j = Session("Node2Plot")(k) 'Session("tPLOT") pointer
        chk = IfsVars.Item(Session("tVARN")(1, j)).Index
        Session("SEL") = chk

        'mti August 2004
        setslider(j)

        blnSettingYears = True
        'mti August 2004
        cmbShiftYears.SelectedIndex = Val(VShiftYears(Session("SEL"))) '- 1
        blnSettingYears = False


        If UCase(Mid(Session("LinePrefix")(k), 1, 3)) <> "CUS" Then 'jrs 2006/08/01 one of the scenarios was created manually and read: Custom, instead of CUSTOM

            Call ReadMedium(CShort(j))

            Session("LinePrefix")(k) = "MEDIUM," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Call setslider(CShort(j))

        'JRS 04/27/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VType(Session("SEL")) <> 2 Then
            lblJS.Visible = True
        Else
            lblJS.Visible = False
        End If

        Call GraphUpdate()


        'LBLSLIDERVAL.Text = ""
        'txtSliderVal.Text = ""

        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.ForeColor' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.ForeColor.html'.
        'JRSVBNET treeScenario.SelectedItem.ForeColor = System.Convert.ToUInt32(OFColor)
        cmbShiftYears.Enabled = False
        'JRS 04/27/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VType(Session("SEL")) <> 2 Then
            lblFullyCustomized.Visible = True
            lblCustomized.Visible = True
        Else
            lblFullyCustomized.Visible = False
            lblCustomized.Visible = False
        End If

        'mtiwebformsMarch2004 (2)ScenarioCreate
        _Opt_highlow_0.Checked = False
        _Opt_highlow_1.Checked = False
        _Opt_highlow_2.Checked = False
        'end mtiwebformsMarch2004 (2)ScenarioCreate

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    '=============================ScenarioCreate.Frm=============================
    Private Sub treeScenario_NodeClick(ByVal eventSender As System.Object, ByVal eventArgs As Microsoft.Web.UI.WebControls.TreeViewSelectEventArgs) Handles treeScenario.SelectedIndexChange
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'mti WebForm Changes From July 04, 2004
        Dim j As Short

        frm_varname.Visible = False

        Frm_nonscenario.Visible = False
        frm_dim1.Visible = False
        frm_dim1.Visible = False

        Curnode = treeScenario.GetNodeFromIndex(eventArgs.NewNode)

        'jrs 2011/11/29
        frm_options.Visible = False
        frm_time.Visible = False
        frm_graph.Visible = False
        Frm_Custom.Visible = False
        'jrs 2023/07/11
        frm_VarDesc.Visible = False

        Select Case NodeType(Curnode)
            Case ObjectType.otIssueArea
                Call mnuDriver_Click()


            Case ObjectType.OtDriver
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(Curnode.Index). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
                'mtiwebformsMarch2004 (2)ScenarioCreate
                'mti WebForm Changes From July 04, 2004
                j = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
                'If Session("LinePrefix")(Curnode.GetNodeIndex) = "" Then ' otRegion Then
                'If Session("LinePrefix")(TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)) = "" Then ' otRegion Then
                If Session("LinePrefix")(j) = "" Then ' otRegion Then
                    Call mnuRegion_Click()

                Else
                    'JRS 08/11/03
                    'FlagGroup = (Curnode.Tag = "Group")
                    Call Repaint_Right()
                    'JRS 04/27/03
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If VType(Session("SEL")) = 2 Then
                        cmdCustom_Click(cmdCustom, New System.EventArgs)
                    End If
                End If


            Case ObjectType.Otregion
                Call Repaint_Right()
                'JRS 04/27/03
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If VType(Session("SEL")) = 2 Then
                    cmdCustom_Click(cmdCustom, New System.EventArgs)
                End If
            Case ObjectType.otBroadCategory
                frm_options.Visible = False
                lblJS.Visible = False
                'JRSVBNET VSc_slider.Visible = False
                'LBLSLIDERVAL.Visible = False
                pnlSlider.Visible = False
                Frm_Custom.Visible = False
                frm_time.Visible = False
                Frm_nonscenario.Visible = False


                lbl_hint.Visible = True
                pnlMaxmin.Visible = False

                'mti August 2004 2 -start
            Case Else 'root is broad Session("Category")

        End Select
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub SaveScenario(ByRef sFileName As String, ByRef sFileTitle As String)
        Dim grpfile As Object
        Dim k As Integer
        Dim j As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        On Error GoTo er_Save
        Dim intfile As Short
        Dim intNotefile As Short
        Dim NoteTxt As String
        Dim WriteStr As String
        intfile = FreeFile()
        FileOpen(intfile, sFileName, OpenMode.Output)

        'JRS 03/09/04
        Dim n As Microsoft.Web.UI.WebControls.TreeNode
        Dim NodeList As Microsoft.Web.UI.WebControls.TreeNodeCollection
        'THE INDEX INFORMATION IS STORED IN NODE DATA
        NodeList = TreeToList(treeScenario.Nodes)
        'JRS 2005/04/25 For Each n In NodeList
        'JRS 2005/04/25 j = TreeIndexToFlatIndex(treeScenario.Nodes, n.NodeData)
        For j = 1 To Support.TreeViewManager.CountNodes(treeScenario.Nodes)
            WriteStr = Session("LinePrefix")(j)
            If WriteStr <> "" Then 'write

                k = Session("Node2Plot")(j)

                chk = IfsVars.Item(Session("tVARN")(1, k)).Index
                Session("SEL") = chk

                Select Case Mid(WriteStr, 1, 3)
                    Case "MED"
                        WriteStr = WriteStr & "," & BuildCustomStr(k) '+ Trim(VMEDIUM(Session("SEL")))   ' + ":" + Trim(Str(frm_MainMenu.IFsGUIctrl1.Session("LastYear") - frm_MainMenu.IFsGUIctrl1.Session("FirstYear") + 1))
                    Case "HIG"
                        WriteStr = WriteStr & "," & BuildCustomStr(k) 'Trim(VHIGH(Session("SEL")))   '+ ":" + Trim(Str(frm_MainMenu.IFsGUIctrl1.Session("LastYear") - frm_MainMenu.IFsGUIctrl1.Session("FirstYear") + 1))
                    Case "LOW"
                        WriteStr = WriteStr & "," & BuildCustomStr(k) 'Trim(VLOW(Session("SEL"))) '+ ":" + Trim(Str(frm_MainMenu.IFsGUIctrl1.Session("LastYear") - frm_MainMenu.IFsGUIctrl1.Session("FirstYear") + 1))
                            'mti 19/09- Scenario Slider
                    Case "SLI"
                        WriteStr = WriteStr & "," & BuildCustomStr(k) 'Trim(VLOW(Session("SEL"))) '+ ":" + Trim(Str(frm_MainMenu.IFsGUIctrl1.Session("LastYear") - frm_MainMenu.IFsGUIctrl1.Session("FirstYear") + 1))
                    Case "CUS"
                        WriteStr = WriteStr & "," & BuildCustomStr(k)
                End Select
                PrintLine(intfile, WriteStr)
            End If
        Next j 'JRS 2005/04/25 n

        'write nonScenario
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        For i2 = 0 To lst_nonscenario.Items.Count - 1
            PrintLine(intfile, "CUSTOM," & lst_nonscenario.Items(i2).Text)
        Next i2
        '    Close #intfile
        If sFileTitle <> "Working.Sce" Then
            PrintLine(intfile, "COMMENT," & "START")
            intNotefile = FreeFile()
            FileOpen(intNotefile, Session("ScenarioFileDir") & "working.txt", OpenMode.Input)
            NoteTxt = InputString(intNotefile, LOF(intNotefile))
            'jrs 2005/08/05
            Dim d As Long
            d = InStr(NoteTxt, "****************** Details of Implementation") - 1
            If d > 1 Then
                NoteTxt = Mid(NoteTxt, 1, d)
            End If

            PrintLine(intfile, NoteTxt)
            FileClose(intNotefile)
        End If

        FileClose(intfile)
        'UPGRADE_WARNING: Couldn't resolve default property of object grpfile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        FileClose(grpfile)

        Exit Sub
er_Save:
        'JRS 2/11/03
        FileClose(intfile)
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub BuildDrivers(ByRef Filename As Object)
        Dim msg As String = ""
        msg = msg & "<br>" & UCase(GRPS(1, 1)) & "."

        'mti WebForm Changes From July 02, 2004- start
        Dim TmpVLine(MaxVariables + 2) As String
        Dim TmpLinePrefix(MaxVariables + 2) As String
        Dim TmpRealNode(MaxVariables + 2) As String
        Dim TmpNode2Plot(MaxVariables + 2) As Short
        Dim TNN As Short = 0
        Dim MyCnt1 As Short
        Dim MyCnt2 As Short
        'mti WebForm Changes From July 02, 2004 - end

        Dim Dim2No As Object, Dim3No As Object
        Dim Dim2Max As Object, Dim3Max As Object
        Dim tmp_p2 As Object, tmp_p3 As Object
        Dim p2 As String, p3 As String 'jrs 2021/04/14 changed from object to string to read full string in p2 for DRC
        Dim Maximum As Object
        Dim Dim1No As Object
        Dim Dim1MAX As Object
        Dim reg As String
        'mti August 2004
        Dim p1 As String 'Object
        'jrs 2021/04/13
        Dim p1_2, p2_2 As String

        Dim VLine As String
        'jrs 2005/04/26
        Dim VLine2 As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Dim ntext, Var, LastVar, curkey As String
        Dim j As Short
        'jrs 2005/04/26
        Dim y As Short
        'JRS 07/08/03
        Dim bGroupFlag As Boolean
        bGroupFlag = False

        Dim NodeLine As String
        Dim Drivernode As String
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        lst_nonscenario.Items.Clear()
        'jrs 2005/02/12
        Dim fNum As Integer, fNum2 As Integer
        fNum = FreeFile()
        'UPGRADE_WARNING: Couldn't resolve default property of object Filename. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        FileOpen(fNum, Filename, OpenMode.Input) 'jrs 2020/05/05 Binary is removing comment 'jrs 2005/06/13 Input)
        fNum2 = FreeFile()
        Dim myGrp As String
        Dim sReg As String
        'jrs 2024/02/23
        Dim SceBaseYear As Integer = Session("FirstYear")
        Dim Lag As Short = 0

        Do While Not EOF(fNum)
            Input(fNum, Scenariotype)
            Input(fNum, Var)

            'jrs 2024/02/23
            If Scenariotype = "BASEYEAR" Then
                SceBaseYear = CInt(Var)
                Input(fNum, Scenariotype)
                Input(fNum, Var)
                Lag = SceBaseYear - FirstYearForecast
            End If

            Select Case Mid(Scenariotype, 1, 3)

                Case "COM" 'Comment Starts with Comment, Start
                    'write comment to working.txt
                    'Take care of Blank: Doesn't Even Come to this Loop
                    FileOpen(fNum2, Session("ScenarioFileDir") & "working.txt", OpenMode.Output)
                    'jrs 2007/12/21 Var = LineInput(fNum) 'jrs 2005/06/22 ""
                    Var = ""

                    Do While Not EOF(fNum)
                        Var = Var & InputString(fNum, 1)
                    Loop
                    'jrs 2005/05/05
                    Var = Replace(Var, vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf, vbCrLf)
                    Var = Replace(Var, vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf, vbCrLf)
                    PrintLine(fNum2, Var)
                    FileClose(fNum2)
                    Exit Do


                Case "MED"
                    NColor = MEDIUMCOLOR
                Case "HIG"
                    NColor = HIGHCOLOR
                Case "LOW"
                    NColor = LOWCOLOR
                Case Else
                    NColor = CShort(Nothing)
            End Select

            If Var <> LastVar Then
                'search to match variable number       'LastVar$ = Var$
                chk = IfsVars.Item(Var).Index
                'jrs 2023/04/27
                'JRS 04/08/03
                If (chk = -1) Then
                    'read rest of line
                    'jrs 2005/05/11
                    If Not EOF(fNum) Then
                        VLine = LineInput(fNum)
                    End If
                    GoTo EndLooopEOF
                End If
                Session("SEL") = chk
            End If
            If Session("SEL") <> 0 Then
                If VDim1(Session("SEL")) = 0 And VDim2(Session("SEL")) = 0 Then 'routine for simple variables - no subdim

                    ntext = VScenario(Session("SEL"))

                    If Trim(ntext) <> "" Then
                        'mti WebForm Changes From July 04, 2004
                        'jrs 2005/04/26 i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes)
                        Drivernode = AddChildNode(treeScenario, ntext, "d " & V(Session("SEL")), False, Category_Node_Index(treeScenario, CShort(VScenCat(Session("SEL")))), 0) 'CInt(VCat(Session("SEL"))) + 1,
                        'jrs 2005/04/26 
                        'If i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes) Then
                        '    VLine = LineInput(fNum)
                        '    GoTo EndLooopEOF
                        'End If
                        'mti WebForm Changes From July 04, 2004
                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        Session("CurNodeNo") = Drivernode
                        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                        'JRS 03/09/04 j = CurNodeNo
                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        'j = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
                        j = TreeIndexToFlatIndex(treeScenario.Nodes, Drivernode)

                        'Session("LinePrefix")(j) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        'jrs 2005/04/26
                        Session("LinePrefix")(j) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)

                        VPTR = 1
                        'JRS 03/04/04
                        Session("VPTR") = VPTR

                        Call HoldNodeInfo()

                        'mti WebForm Changes From July 02, 2004- start
                        'uncomment these if change does not work
                        'Session("LinePrefix")(j) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        'jrs 2005/04/26
                        y = j
                        j = Session("Node2Plot")(j)
                        VLine = LineInput(fNum) 'read rest of line
                        VLine2 = VLine
                        Call ParseCustomStr(VLine, j, Lag)
                        'comment these if change does not work 
                        TNN = TNN + 1
                        TmpLinePrefix(TNN) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        TmpVLine(TNN) = VLine2 'jrs 2005/04/26 LineInput(fNum)
                        'mti WebForm Changes From July 04, 2004
                        TmpRealNode(TNN) = Drivernode 'CurNodeNo
                        TmpNode2Plot(TNN) = Session("Node2Plot")(y)
                        'mti WebForm Changes From July 02, 2004- end
                    Else
                        VLine = LineInput(fNum)
                        lst_nonscenario.Items.Add(New ListItem(Var & "," & VLine)) 'V$(Session("SEL")) & "," & VLine$
                    End If
                ElseIf VDim1(Session("SEL")) > 0 And VDim2(Session("SEL")) = 0 Then 'routine for single dim. vars.
                    ntext = VScenario(Session("SEL"))

                    If Trim(ntext) <> "" Then

                        Drivernode = AddChildNode(treeScenario, ntext, "d ", True, Category_Node_Index(treeScenario, CShort(VScenCat(Session("SEL")))), 0) 'CInt(VCat(Session("SEL"))) + 1,

                    End If

                    Input(fNum, p1)
                    'jrs 2021/04/13
                    If p1 = "Congo" Then
                        'check if not DRC
                        Input(fNum, p1_2)
                        If p1_2 = "Dem. Republic of the" Then
                            p1 = p1 & ", " & p1_2
                        End If
                    End If

                    If Trim(ntext) <> "" Then

                        'jrs 2024/02/01 added orElse p1.Contains("$")
                        If Val(p1) = 0 OrElse p1.Contains("$") Then 'string in dim
                            reg = p1
                            Dim1MAX = MaxVal(VDim1(Session("SEL")))
                            Dim1No = VDim1(Session("SEL"))
                            'jrs 2021/10/18 msg = msg & "<br>" & Dim1No.ToString & "." & eleloop.ToString & "." & UCase(GRPSLoc(1, 1)) & "."
                            For eleloop = 1 To Dim1MAX 'Up to number of regions
                                'jrs 2014/03/07
                                If reg$ = "Total" And GRPSLoc(Dim1No, Dim1MAX - 1) <> "Total" Then
                                    p1 = 1
                                    Exit For
                                End If

                                'jrs 2021/10/18 msg = msg & "<br>" & Dim1No.ToString & "." & eleloop.ToString & "." & UCase(GRPSLoc(Dim1No, eleloop)) & "."

                                If UCase(reg) = UCase(GRPSLoc(Dim1No, eleloop)) Then
                                    p1 = eleloop
                                    bGroupFlag = False 'jrs 2005/06/13
                                    Exit For
                                End If
                                'jrs 2005/06/13
                                If InStr(GRPSLoc(Dim1No, eleloop), ",") > 0 Then
                                    If UCase(reg) = UCase(Left(GRPSLoc(Dim1No, eleloop), InStr(GRPSLoc(Dim1No, eleloop), ",") - 1)) Then
                                        Input(fNum, p1)
                                        reg = reg + ", " + p1
                                        If UCase(reg) = UCase(GRPSLoc(Dim1No, eleloop)) Then
                                            p1 = eleloop
                                            bGroupFlag = False
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next eleloop
                            'JRS 11/30/01 GROUPING
                            If eleloop > Dim1MAX And Not IsNumeric(p1) Then
                                'JRS 08/04/03
                                myGrp = p1
                                If Not CBool(CheckGroups(myGrp)) = True Then
                                    'jrs 2025/05/20
                                    FileClose(fNum)
                                    DeleteRowFromWorkingSCE(myGrp)
                                    'jrs 2021/10/18
                                    msg = p1 + " is not a valid group, please check your original file, removing line from woking.sce to continue."
                                    'msg = msg & "<br>" & Dim1No.ToString & "." & eleloop.ToString & "." & UCase(GRPSLoc(1, 1)) & "."
                                    LoadMessage(msg, Session("Message"))
                                    Support.FormManager.StateStore(Me)
                                    Support.FormManager.Show(Me, "frm_Message.aspx")

                                End If
                                'JRS 5/2/02 IF THE GROUP IS EMPTY LOAD IT
                                If CountryGroup(1) = "" Then
                                    LoadCountries((p1))
                                End If
                                'JRS 04/08/03
                                sReg = p1
                                p1 = RegionIdx(CountryGroup(1))
                                'JRS 04/08/03
                                If (p1 = 0) Then
                                    'read rest of line
                                    VLine = LineInput(1)

                                    LoadMessage("Region " & sReg & " not found.", Session("Message"))
                                    Support.FormManager.StateStore(Me)
                                    Support.FormManager.Show(Me, "frm_Message.aspx")

                                    GoTo EndLooopEOF
                                End If
                            End If
                        Else
                            reg = GRPSLoc(Dim1No, p1)
                        End If


                        curkey = "r " & V(Session("SEL")) & "," & reg '& "," & Str(Trim(Ind%))
                        'mtiwebformsMarch2004 (2)ScenarioCreate
                        'mti WebForm Changes From July 04, 2004
                        'jrs 2005/04/26 i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes)
                        CurNodeNo = AddChildNode(treeScenario, reg, curkey, False, Drivernode, NColor)
                        'jrs 2005/04/26 
                        'If i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes) Then
                        '    VLine = LineInput(fNum)
                        '    GoTo EndLooopEOF
                        'End If
                        'mti WebForm Changes From July 04, 2004
                        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)

                        'JRS 07/14/03
                        If CBool(CheckGroups(reg)) Then
                            'treeScenario.SelectedItem.Tag = "Group" 
                            Curnode.ID = "Group"
                        End If



                        Session("tGR1N")(Session("tCOUNTE") + 1) = reg
                        VPTR = p1
                        'JRS 03/04/04
                        Session("VPTR") = VPTR
                        Call HoldNodeInfo()
                        'JRS 03/09/04 j = CurNodeNo
                        j = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)



                        'mti WebForm Changes From July 02, 2004- start
                        'uncomment these if change does not work
                        'jrs 2005/04/26
                        Session("LinePrefix")(j) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        y = j
                        j = Session("Node2Plot")(j)
                        VLine = LineInput(fNum) 'read rest of line
                        'jrs 2021/04/13
                        If reg = "Congo" Then
                            VLine = p1_2 & "," & VLine 'jrs 2021/04/22
                        End If
                        VLine2 = VLine
                        Call ParseCustomStr(VLine, j, Lag)
                        'comment these if change does not work 
                        TNN = TNN + 1
                        TmpLinePrefix(TNN) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        TmpVLine(TNN) = VLine2 'jrs 2005/04/26 LineInput(fNum)
                        TmpRealNode(TNN) = CurNodeNo
                        TmpNode2Plot(TNN) = Session("Node2Plot")(y)
                        'mti WebForm Changes From July 02, 2004- end
                    Else
                        VLine = LineInput(fNum)
                        'UPGRADE_WARNING: Couldn't resolve default property of object p1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        lst_nonscenario.Items.Add(New ListItem(Var & "," & p1 & "," & VLine)) 'V$(Session("SEL")) & "," & reg$ & "," & VLine$
                    End If
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                ElseIf VDim1(Session("SEL")) > 0 And VDim2(Session("SEL")) > 0 And VDim3(Session("SEL")) = 0 Then 'routine for double dim. vars.
                    Maximum = MAXCOM(VDim1(Session("SEL"))) - 1

                    Input(fNum, p1)
                    Input(fNum, p2)

                    If Trim(VScenario(Session("SEL"))) = "" Then
                        VLine = LineInput(fNum)
                        lst_nonscenario.Items.Add(New ListItem(Var & "," & p1 & "," & p2 & "," & VLine)) 'V$(Session("SEL")) & "," & reg$ & "," & Trim(GRPS$(VDim2%(Session("SEL")), p2)) & "," & VLine$
                    Else

                        If Val(p1) = 0 Then 'string in dim
                            'jrs 2021/04/13
                            If p1 = "Congo" And p2 = "Dem. Republic of the" Then
                                p1 = "Congo, Dem. Republic of the"
                                Input(fNum, p2)
                            End If
                            'jrs 2022/01/14
                            If p1 = "Korea" And p2 = "Republic of" Then
                                p1 = "Korea, Republic of"
                                Input(fNum, p2)
                            End If

                            reg = p1
                            Dim1MAX = MaxVal(VDim1(Session("SEL")))
                            Dim1No = VDim1(Session("SEL"))
                            For eleloop = 1 To Dim1MAX 'Up to number of regions
                                If UCase(reg) = UCase(GRPS(Dim1No, eleloop)) Then
                                    p1 = eleloop
                                    'JRS 09/04/03
                                    bGroupFlag = False
                                    Exit For
                                End If
                                'jrs 2005/06/13
                                If InStr(GRPS(Dim1No, eleloop), ",") > 0 Then
                                    If UCase(reg) = UCase(Left(GRPS(Dim1No, eleloop), InStr(GRPS(Dim1No, eleloop), ",") - 1)) Then
                                        Input(fNum, p1)
                                        reg = reg + ", " + p2
                                        p2 = p1
                                        If UCase(reg) = UCase(GRPS(Dim1No, eleloop)) Then
                                            p1 = eleloop
                                            bGroupFlag = False
                                            Exit For
                                        End If
                                        'jrs 2020/05/20 p2 only loaded Democratic, missing Republic of
                                        Input(fNum, p1)
                                        reg = reg + " " + p2
                                        p2 = p1
                                        If UCase(reg) = UCase(GRPS(Dim1No, eleloop)) Then
                                            p1 = eleloop
                                            bGroupFlag = False
                                            Exit For
                                        End If
                                    End If
                                End If

                            Next eleloop
                            'UPGRADE_WARNING: Couldn't resolve default property of object Dim1MAX. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            If eleloop > Dim1MAX And Not IsNumeric(p1) Then
                                'jrs 2025/10/23 *** start ***
                                myGrp = p1
                                If Not CBool(CheckGroups(myGrp)) = True Then
                                    'jrs 2025/05/20
                                    FileClose(fNum)
                                    DeleteRowFromWorkingSCE(myGrp)
                                    'jrs 2021/10/18
                                    msg = p1 + " is not a valid group, please check your original file, removing line from woking.sce to continue."
                                    'msg = msg & "<br>" & Dim1No.ToString & "." & eleloop.ToString & "." & UCase(GRPSLoc(1, 1)) & "."
                                    LoadMessage(msg, Session("Message"))
                                    Support.FormManager.StateStore(Me)
                                    Support.FormManager.Show(Me, "frm_Message.aspx")

                                End If
                                'JRS 5/2/02 IF THE GROUP IS EMPTY LOAD IT
                                If CountryGroup(1) = "" Then
                                    LoadCountries((p1))
                                End If
                                'JRS 04/08/03
                                sReg = p1
                                'jrs 2025/10/23 *** end ***
                                'UPGRADE_WARNING: Couldn't resolve default property of object p1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                p1 = RegionIdx(CountryGroup(1))
                                'jrs 2025/10/23 *** start ***
                                If (p1 = 0) Then
                                    'read rest of line
                                    VLine = LineInput(1)

                                    LoadMessage("Region " & sReg & " not found.", Session("Message"))
                                    Support.FormManager.StateStore(Me)
                                    Support.FormManager.Show(Me, "frm_Message.aspx")

                                    GoTo EndLooopEOF
                                End If
                                'jrs 2025/10/23 *** end ***
                                bGroupFlag = True
                            End If
                        Else
                            reg = GRPS(Dim1No, p1)
                        End If

                        If Val(p2) = 0 Then 'string in dim
                            'jrs 2021/04/13
                            If p2 = "Congo" Then
                                'check if not DRC
                                Input(fNum, p2_2)
                                If p2_2 = "Dem. Republic of the" Then
                                    p2 = p2 & ", " & p2_2
                                End If
                            End If

                            tmp_p2 = p2
                            Dim2Max = MaxVal(VDim2(Session("SEL")))
                            Dim2No = VDim2(Session("SEL"))
                            For eleloop = 1 To Dim2Max 'Up to number of regions
                                'jrs 2014/03/07
                                If tmp_p2 = "Total" And GRPS(Dim2No, Dim2Max - 1) <> "Total" Then
                                    Exit For
                                End If

                                If UCase(tmp_p2) = UCase(GRPS(Dim2No, eleloop)) Then
                                    p2 = eleloop
                                    Exit For
                                End If
                                'jrs 2005/06/13
                                If InStr(GRPS(Dim1No, eleloop), ",") > 0 Then
                                    If UCase(tmp_p2) = UCase(Left(GRPS(Dim1No, eleloop), InStr(GRPS(Dim1No, eleloop), ",") - 1)) Then
                                        Input(fNum, p2)
                                        tmp_p2 = tmp_p2 + ", " + p2
                                        If UCase(tmp_p2) = UCase(GRPS(Dim1No, eleloop)) Then
                                            p2 = eleloop
                                            bGroupFlag = False
                                            Exit For
                                        End If
                                    End If
                                End If

                            Next eleloop

                        End If

                        'jrs 2006/12/28
                        If IsNumeric(p2) Then
                            ntext = VScenario(Session("SEL")) & " (" & Trim(GRPS(VDim2(Session("SEL")), p2)) & ")"
                        Else
                            ntext = VScenario$(Session("SEL")) + " (" + p2 + ")"
                        End If

                        Drivernode = AddChildNode(treeScenario, ntext, "d ", True, Category_Node_Index(treeScenario, CShort(VScenCat(Session("SEL")))), 0) 'CInt(VCat(Session("SEL"))) + 1,

                        'jrs 2006/12/28 curkey = "r " & V(Session("SEL")) & "," & reg & "," & Str(CDbl(Trim(p2)))
                        curkey = "r " & V$(Session("SEL")) & "," & reg$ & "," & p2
                        'mti WebForm Changes From July 04, 2004 -start
                        'jrs 2005/04/26 i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes)
                        CurNodeNo = AddChildNode(treeScenario, reg, curkey, False, Drivernode, NColor)
                        'jrs 2005/04/26 
                        'If i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes) Then
                        '    VLine = LineInput(fNum)
                        '    GoTo EndLooopEOF
                        'End If
                        'mti WebForm Changes From July 04, 2004
                        Session("tGR1N")(Session("tCOUNTE") + 1) = reg
                        'jrs 2006/12/28
                        If IsNumeric(p2) Then
                            Session("tGR2N")(Session("tCOUNTE") + 1) = GRPS(VDim2(Session("SEL")), p2)
                        Else
                            Session("tGR2N")(Session("tCOUNTE") + 1) = p2
                            Dim i As Integer
                            For i = 1 To MaxVal(VDim2(Session("SEL"))) - 1
                                'jrs 2014/03/07
                                If p2 = "Total" And GRPS(VDim2(Session("SEL")), MaxVal(VDim2(Session("SEL"))) - 1) <> "Total" Then
                                    p2 = 1
                                    Exit For
                                End If
                                If UCase(p2) = UCase(GRPS(VDim2(Session("SEL")), i)) Then
                                    p2 = i
                                    Exit For
                                End If
                            Next i
                        End If
                        If VDim2(Session("SEL")) = 1 And Not IsNumeric(p2) Then
                            p2 = RegionIdx(CountryGroup(1))
                            bGroupFlag = True
                        End If

                        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                        'JRS 07/08/03
                        If bGroupFlag Then
                            Curnode.ID = "Group"
                        End If
                        VPTR = p1 + ((p2 - 1) * GMAX1) 'JRS 2005/07/06 Dim1MAX)
                        'JRS 03/04/04
                        Session("VPTR") = VPTR
                        Call HoldNodeInfo()

                        'JRS 03/09/04 j = CurNodeNo
                        j = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
                        'mti WebForm Changes From July 02, 2004- start
                        'uncomment these if change does not work
                        'Session("LinePrefix")(j) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        'jrs 2005/04/26
                        Session("LinePrefix")(j) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'jrs 2005/04/26
                        y = j
                        j = Session("Node2Plot")(j)
                        VLine = LineInput(fNum) 'read rest of line
                        'jrs 2021/04/13
                        If GRPS(1, p2) = "Congo" And p2_2 > "" Then
                            VLine = p2_2 & ", " & VLine
                        End If
                        VLine2 = VLine
                        Call ParseCustomStr(VLine, j, Lag)
                        'comment these if change does not work 
                        TNN = TNN + 1
                        TmpLinePrefix(TNN) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        TmpVLine(TNN) = VLine2 'jrs 2005/04/26 LineInput(fNum)
                        TmpRealNode(TNN) = CurNodeNo
                        TmpNode2Plot(TNN) = Session("Node2Plot")(y)
                        'mti WebForm Changes From July 02, 2004- end
                    End If 'Vscenario$(Session("SEL")) = ""
                ElseIf VDim3(Session("SEL")) > 0 Then 'routine for triple dim. vars. 'jrs 2006/05/30 dim3
                    Maximum = MAXCOM(VDim1(Session("SEL"))) - 1
                    Input(fNum, p1)
                    Input(fNum, p2)
                    Input(fNum, p3)


                    If Trim(VScenario$(Session("SEL"))) = "" Then
                        VLine = LineInput(fNum)
                        lst_nonscenario.Items.Add(New ListItem(Var & "," & p1 & "," & p2 & "," & p3 & "," & VLine)) 'V$(Session("SEL")) & "," & reg$ & "," & Trim(GRPS$(VDim2%(Session("SEL")), p2)) & "," & VLine$
                    Else

                        If Val(p1) = 0 Then  'string in dim
                            'jrs 2021/04/13
                            If p1 = "Congo" And p2 = "Dem. Republic of the" Then
                                p1 = "Congo, Dem. Republic of the"
                                p2 = p3
                                Input(fNum, p3)
                            End If

                            reg$ = p1
                            Dim1MAX = MaxVal(VDim1(Session("SEL")))
                            Dim1No = VDim1(Session("SEL"))
                            For eleloop = 1 To Dim1MAX 'Up to number of regions
                                If UCase(reg$) = UCase(GRPS$(Dim1No, eleloop)) Then
                                    p1 = eleloop
                                    'JRS 09/04/03
                                    bGroupFlag = False
                                    Exit For
                                End If
                                'jrs 2005/06/13
                                If InStr(GRPS(Dim1No, eleloop), ",") > 0 Then
                                    If UCase(reg) = UCase(Left(GRPS(Dim1No, eleloop), InStr(GRPS(Dim1No, eleloop), ",") - 1)) Then
                                        Input(fNum, p1)
                                        reg = reg + ", " + p2
                                        p2 = p3
                                        p3 = p1
                                        If UCase(reg) = UCase(GRPS$(Dim1No, eleloop)) Then
                                            p1 = eleloop
                                            bGroupFlag = False
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next eleloop
                            If eleloop > Dim1MAX And Not IsNumeric(p1) Then
                                'jrs 2020/12/08
                                Dim myGrp1 As String
                                myGrp1 = p1
                                'jrs 2023/06/09 added CBool
                                If Not CBool(CheckGroups(myGrp1)) = True Then
                                    'jrs 2025/05/20
                                    FileClose(fNum)
                                    DeleteRowFromWorkingSCE(myGrp)
                                    Session("Message") = p1 + " is not a valid group, please check your file, removing line from woking.sce to continue."
                                    LoadMessage("Build Drivers", Session("Message"))
                                    Support.FormManager.StateStore(Me)
                                    Support.FormManager.Show(Me, "frm_Message.aspx")
                                End If
                                If CountryGroup(1) = "" Then
                                    LoadCountries(p1)
                                End If
                                sReg = p1

                                p1 = RegionIdx(CountryGroup(1))
                                bGroupFlag = True
                            End If
                        Else
                            reg$ = GRPS$(Dim1No, p1)
                        End If

                        If Val(p2) = 0 Then  'string in dim
                            tmp_p2 = p2
                            Dim2Max = MaxVal(VDim2(Session("SEL")))
                            Dim2No = VDim2(Session("SEL"))
                            For eleloop = 1 To Dim2Max 'Up to number of regions
                                If UCase(tmp_p2) = UCase(GRPS$(Dim2No, eleloop)) Then
                                    p2 = eleloop
                                    Exit For
                                End If
                                'jrs 2005/06/13
                                If InStr(GRPS(Dim2No, eleloop), ",") > 0 Then
                                    If UCase(tmp_p2) = UCase(Left(GRPS(Dim2No, eleloop), InStr(GRPS(Dim2No, eleloop), ",") - 1)) Then
                                        Input(fNum, p2)
                                        tmp_p2 = tmp_p2 + ", " + p3
                                        p3 = p2
                                        If UCase(tmp_p2) = UCase(GRPS$(Dim2No, eleloop)) Then
                                            p2 = eleloop
                                            bGroupFlag = False
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next eleloop
                            'jrs 2020/12/09
                            If eleloop > Dim2Max And Not IsNumeric(p2) Then
                                Dim myGrp2 As String
                                myGrp2 = p2
                                'jrs 2023/06/09 added cbool
                                If Not CBool(CheckGroups(myGrp2)) = True Then
                                    'Try using p3
                                    Do While Not IsNumeric(p3)
                                        myGrp2 = myGrp2 & " " & p3
                                        Input(fNum, p3)
                                        'jrs 2023/06/09 added cbool
                                        If CBool(CheckGroups(myGrp2)) = True Then
                                            Exit Do
                                        End If
                                    Loop

                                    'jrs 2023/06/09 added CBool
                                    If Not CBool(CheckGroups(myGrp2)) = True Then
                                        'jrs 2025/05/20
                                        FileClose(fNum)
                                        DeleteRowFromWorkingSCE(myGrp2)
                                        Session("Message") = myGrp2 + " is not a valid group, please check your file, removing line from woking.sce to continue."
                                        LoadMessage("Build Drivers", Session("Message"))
                                        Support.FormManager.StateStore(Me)
                                        Support.FormManager.Show(Me, "frm_Message.aspx")
                                    End If
                                End If
                                If CountryGroup(1) = "" Then
                                    LoadCountries(myGrp2)
                                End If
                                'sReg2 = p2

                                'p2 = RegionIdx(CountryGroup(1))
                                p2 = myGrp2
                                bGroupFlag = True
                            End If
                        End If

                        If Val(p3) = 0 Then  'string in dim
                            tmp_p3 = p3
                            Dim3Max = MaxVal(VDim3(Session("SEL")))
                            Dim3No = VDim3(Session("SEL"))
                            For eleloop = 1 To Dim3Max 'Up to number of regions
                                If UCase(tmp_p3) = UCase(GRPS$(Dim3No, eleloop)) Then
                                    p3 = eleloop
                                    Exit For
                                End If
                                'jrs 2005/06/13
                                If InStr(GRPS(Dim3No, eleloop), ",") > 0 Then
                                    If UCase(tmp_p3) = UCase(Left(GRPS(Dim3No, eleloop), InStr(GRPS(Dim3No, eleloop), ",") - 1)) Then
                                        Input(fNum, p3)
                                        tmp_p3 = tmp_p3 + ", " + p3
                                        If UCase(tmp_p3) = UCase(GRPS$(Dim3No, eleloop)) Then
                                            p3 = eleloop
                                            bGroupFlag = False
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next eleloop
                        End If

                        'jrs 2014/08/04
                        If IsNumeric(p2) And p2 <> "-1" Then
                            ntext = VScenario$(Session("SEL")) + " (" + Trim(GRPS$(VDim2(Session("SEL")), p2)) + ", " + Trim(GRPS$(VDim3(Session("SEL")), p3)) + ")"
                        Else
                            ntext = VScenario$(Session("SEL")) + " (" + Trim(p2) + ", " + Trim(GRPS$(VDim3(Session("SEL")), p3)) + ")"
                        End If

                        Drivernode = AddChildNode(treeScenario, ntext, "d ", True, Category_Node_Index(treeScenario, CInt(VScenCat(Session("SEL")))), 0)  'CInt(VCat(SEL)) + 1,

                        curkey = "r " & V$(Session("SEL")) & "," & reg$ & "," & CStr(p2).Trim & "," & CStr(p3).Trim
                        CurNodeNo = AddChildNode(treeScenario, reg$, curkey, False, Drivernode, NColor)

                        'jrs 2021/02/16 changed from COUNTE TO tCOUNTE
                        Session("tGR1N")(Session("tCOUNTE") + 1) = reg$
                        'jrs 2014/08/04
                        If IsNumeric(p2) And p2 <> "-1" Then
                            Session("tGR2N")(Session("tCOUNTE") + 1) = GRPS$(VDim2(Session("SEL")), p2)
                        Else
                            Session("tGR2N")(Session("tCOUNTE") + 1) = p2
                        End If
                        Session("tGR3N")(Session("tCOUNTE") + 1) = GRPS(VDim3(Session("SEL")), p3)

                        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                        'JRS 07/08/03
                        If bGroupFlag Then
                            Curnode.ID = "Group"
                        End If

                        'jrs 2014/08/04
                        If Not IsNumeric(p2) And p2 <> "-1" Then
                            p2 = 1
                        End If

                        VPTR = p1 + ((p2 - 1) * (MAXCOM(VDim1(Session("SEL"))) - 1)) + (p3 - 1) * (MAXCOM(VDim1(Session("SEL"))) - 1) * (MAXCOM(VDim2(Session("SEL"))) - 1) 'JRS 2005/07/06 Dim1MAX)
                        Session("VPTR") = VPTR
                        Call HoldNodeInfo()

                        j = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
                        Session("LinePrefix")(j) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        y = j
                        j = Session("Node2Plot")(j)
                        VLine = LineInput(fNum)
                        VLine2 = VLine
                        Call ParseCustomStr(VLine$, j, Lag)

                        TNN = TNN + 1
                        TmpLinePrefix(TNN) = Scenariotype & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)
                        TmpVLine(TNN) = VLine2 'jrs 2005/04/26 LineInput(fNum)
                        TmpRealNode(TNN) = CurNodeNo
                        TmpNode2Plot(TNN) = Session("Node2Plot")(y)

                    End If 'Vscenario$(SEL) = ""
                End If
                'mti September 2004 2
                'LastVar = Var
            End If 'sel <> 0 , variable is there
            'JRS 04/08/03
EndLooopEOF:
            LastVar = Var
            'mti September 2004 2 -end

        Loop 'Wend
        'mti WebForm Changes From July 02, 2004 -start
        'correct node pointers  after all nodes are added
        'this is needed because treeindextoflatindex and  GetTreeIndexToFlatIndex uses sequential counting
        For j = 1 To MaxVariables + 1
            Session("Node2Plot")(j) = 0
        Next
        For MyCnt1 = 1 To TNN
            j = TreeIndexToFlatIndex(treeScenario.Nodes, TmpRealNode(MyCnt1))
            Session("LinePrefix")(j) = TmpLinePrefix(MyCnt1)
            Session("Node2Plot")(j) = TmpNode2Plot(MyCnt1)
            j = Session("Node2Plot")(j)
            Call ParseCustomStr(TmpVLine(MyCnt1), j, Lag)
        Next
        'mti WebForm Changes From July 02, 2004- end

        'jrs 2024/02/27
        Session("SceLag") = Lag

        FileClose(fNum)
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'mti ScenarioCreate May 2004 uncomment later
        'If lst_nonscenario.Items.Count > 0 Then
        '    mnuOther.Visible = True
        'Else
        '    mnuOther.Visible = False
        'End If


    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub InitTree()
        Dim IssueArea As Microsoft.Web.UI.WebControls.TreeNode
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2005/07/01
        If (Session("LoadVCat") Is Nothing) Then
            Session("VCatDir") = New Hashtable
        End If

        Dim nvcat As Object
        Dim Cat_Parent As Short
        Dim CatnoStr As String
        Dim Extpos As Short
        Dim TopNode As String
        treeScenario.Nodes.Clear()


        Extpos = InStr(1, Session("cur_file"), ".sce", 1) 'vbtextcompare
        If Extpos > 0 Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Scenario Development and Change" & ": " & Mid(Session("cur_file"), 1, Extpos - 1)
        Else
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Scenario Development and Change" & ": " & Session("cur_file")
        End If

        'mti WebForm Changes From July 04, 2004
        If Session("Open_File") <> "" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text + " (" + CStr(Session("Open_File")).Trim + ")"
        End If
        'mti WebForm Changes From July 04, 2004

        'JRS 08/01/02 GUIDEDUSE
        If local_Guided_use Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Step 4 - " & WinFormsToWeb_PageBar.Rows(0).Cells(1).Text
        End If

        'Dim rootNode As New TreeNode("ScenarioTree")

        ' Add a main root tree node.
        Dim RootNode As Microsoft.Web.UI.WebControls.TreeNode
        'mti August 2004 2
        RootNode = Support.TreeViewManager.AddNode(treeScenario.Nodes, , , "b topmost", "Scenario Tree")

        'jrs 2005/02/12
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, DataFileDir & VCATScenarioFile, OpenMode.Input, OpenAccess.Read)
        Totcat = 0 ''ll start from 2, since 1 is root or filename

        Do Until EOF(fNum)
            AddName = LineInput(fNum)
            If AddName <> "" Then
                Totcat = Totcat + 1
                CatnoStr = Trim(Str(Val(AddName)))
                AddName = Trim(Mid(AddName, Len(CatnoStr) + 1))

                'jrs 2005/07/01
                If (Session("LoadVCat") Is Nothing) Then
                    CType(Session("VCatDir"), Hashtable).Add(CatnoStr, AddName)
                End If

                If Val(CatnoStr) Mod 100 = 0 Then
                    'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Add' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    IssueArea = Support.TreeViewManager.AddNode(treeScenario.Nodes, , 1, "b " & CatnoStr & AddName, AddName)


                    'IssueArea = Support.TreeViewManager.AddNode(treeScenario.Nodes, , , "b " & CatnoStr & AddName, AddName)
                    'UPGRADE_WARNING: Couldn't resolve default property of object IssueArea.Index. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Cat_Parent = IssueArea.GetNodeIndex
                    'UPGRADE_WARNING: Couldn't resolve default property of object IssueArea.EnsureVisible. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'JRSVBNET IssueArea.EnsureVisible()
                Else
                    'CONVERSION_TODO: The equivalent of Method 'MSComctlLib.NodesClass.Add' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    IssueArea = Support.TreeViewManager.AddNode(treeScenario.Nodes, treeScenario.GetNodeFromIndex(Cat_Parent), 4, "i " & CatnoStr & AddName, AddName)
                    If Cat_Parent = 1 Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object IssueArea.EnsureVisible. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'JRSVBNET IssueArea.EnsureVisible()
                    End If
                End If
            End If
        Loop 'Next i2
        FileClose(fNum)

        'jrs 2017/01/04 *** begin ***
        If Dir(Session("PutFdrvRun") + "IFsForDyadicWork.db") <> "" Then
            Totcat = Totcat + 1
            Dim SubCatNo As Single
            SubCatNo = Val(CatnoStr) Mod 100
            CatnoStr = CStr(Val(CatnoStr) - SubCatNo + 100)
            AddName = "Bilateral Interaction Parameters"
            IssueArea = Support.TreeViewManager.AddNode(treeScenario.Nodes, , 1, "b " & CatnoStr & AddName, AddName)
            Cat_Parent = IssueArea.GetNodeIndex
            CatnoStr = CStr(Val(CatnoStr) + 10)
            AddName = "Bilateral Trade"
            IssueArea = Support.TreeViewManager.AddNode(treeScenario.Nodes, treeScenario.GetNodeFromIndex(Cat_Parent), 4, "i " & CatnoStr & AddName, AddName)
            Session("BilateralCat") = Val(CatnoStr)
        End If
        'jrs 2017/01/04 *** end ***


        'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
        'treeScenario.Nodes.Item(1).Selected = True 
        treeScenario.Nodes(0).Expanded = True
        'JRS 08/20/04
        Session("ScenCrTotcat") = Totcat
        'mti WebForm Changes From June 26, 2004
        treeScenario.SelectedNodeIndex = 0 '1
        Curnode = treeScenario.Nodes.Item(0) '1

        'CONVERSION_NOTE: Method 'MSComctlLib.NodeClass.EnsureVisible' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.html'.
        'treeScenario.Nodes.Item(1).Child.EnsureVisible() 
        frm_options.Visible = False
        lblJS.Visible = False
        'JRSVBNET VSc_slider.Visible = False
        'LBLSLIDERVAL.Visible = False
        pnlSlider.Visible = False
        Frm_Custom.Visible = False
        frm_time.Visible = False
        Frm_nonscenario.Visible = False
        lbl_hint.Visible = True
        pnlMaxmin.Visible = False
        'jrs 2023/07/11
        frm_VarDesc.Visible = False

        'jrs 2005/07/01
        Session("LoadVCat") = "Loaded"
    End Sub


    '=============================ScenarioCreate.Frm=============================
    'jrs 2024/02/27 added lag parameter
    Private Sub ParseCustomStr(ByRef CustStr As String, ByRef plotcol As Short, ByVal lag As Short)
        Dim FVAl As Object
        Dim nvalue As Object
        Dim mult As Object
        Dim Value As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim RTime, ThisYear, LCount As Short
        'jrs 2024/02/27 LCount = 0
        LCount = lag

        'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        RTime = Session("LastYear") - Session("FirstYear") + 1
        'jrs 2024/02/27 ThisYear = 1
        ThisYear = lag + 1

        'jrs 2023/06/09
        If Session("tVARN")(1, plotcol) = "" Then Exit Sub
        'jrs 2008/05/30 currency
        chk = IfsVars.Item(Session("tVARN")(1, plotcol)).Index
        'jrs 2010/09/01
        If chk < 0 Then
            'No variable in position plotcol
            CustStr = ""
            Exit Sub
        End If

        Dim CurrFactor As Single
        CurrFactor = 1
        'if it's a currency variable
        If VCurrency(CInt(chk)) = "True" Then
            'load the currency convertion value
            CurrFactor = Session("CurrencyVal")
        End If

        'jrs 2024/06/25
        Dim bVarIncluded As Boolean = False
        Dim DimCount As Integer = 0
        Dim DimExpected As Integer = 0
        If VDim1(chk) > 0 Then
            DimExpected = DimExpected + 1
            If VDim2(chk) > 0 Then
                DimExpected = DimExpected + 1
                If VDim3(chk) > 0 Then
                    DimExpected = DimExpected + 1
                End If
            End If
        End If

        'jrs 2005/01/18 If CustStr = "2.400001E-03:1000" Then Stop
        While Len(CustStr) > 0 'Keep going as long as non-zero line
            Value = CustStr
            If InStr(CustStr, ",") > 0 Then 'If commas in line
                Value = Mid(CustStr, 1, InStr(CustStr, ",") - 1)
                CustStr = Mid(CustStr, InStr(CustStr, ",") + 1)
            End If

            If InStr(Value, ":") <> 0 Then 'Any repeats in current string?
                mult = Val(Mid(Value, InStr(Value, ":") + 1))
                LCount = LCount + mult
                nvalue = Val(Value)
                For i2 = ThisYear To LCount
                    If i2 <= RTime Then
                        Session("tPLOT")(i2, plotcol) = Math.Round(nvalue / CurrFactor, 4)
                        Session("PlotRI")(i2, plotcol) = "REPEAT"
                        ThisYear = ThisYear + 1
                    End If
                Next i2
            ElseIf InStr(Value, "@") <> 0 Then 'Any interpolations?
                mult = Val(Mid(Value, InStr(Value, "@") + 1))
                LCount = LCount + mult
                Value = VB.Left(Value, InStr(Value, "@") - 1)
                nvalue = Val(Value)
                For i2 = ThisYear To LCount
                    If i2 <= RTime Then 'Year falls within range for interp
                        Session("tPLOT")(i2, plotcol) = FVAl + (((nvalue - FVAl) / mult) * (mult - (LCount - ThisYear)))
                        'jrs 2008/05/30 currency
                        If CurrFactor <> 1 Then
                            Session("tPLOT")(i2, plotcol) = Math.Round(Session("tPLOT")(i2, plotcol) / CurrFactor, 4)
                        End If
                        Session("PlotRI")(i2, plotcol) = "INTERPOLATE"
                        ThisYear = ThisYear + 1
                    End If
                Next i2
            Else 'Then must only be single value
                'jrs 2024/06/25 added And (Not bVarIncluded Or DimCount = DimExpected) 
                'jrs 2022/01/22
                If IsNumeric(Value) And (Not bVarIncluded Or DimCount = DimExpected) Then
                    LCount = LCount + 1
                    nvalue = Val(Value)
                    If ThisYear <= RTime Then
                        'jrs 2024/02/27 added if statement
                        If ThisYear > 0 Then
                            Session("tPLOT")(ThisYear, plotcol) = Math.Round(nvalue / CurrFactor, 4)
                            Session("PlotRI")(ThisYear, plotcol) = "REPEAT"
                        End If
                        ThisYear = ThisYear + 1
                    End If
                Else 'jrs 2024/06/25
                    If Value = V(CInt(chk)) Then
                        bVarIncluded = True
                    ElseIf bVarIncluded Then
                        DimCount = DimCount + 1
                    End If
                End If
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object nvalue. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object FVAl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            FVAl = nvalue
            If LCount >= RTime Then 'Have we counted up to year yet?
                CustStr = ""
            End If
        End While
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub DelAdjust_NodePlot(ByVal DeleteNodeNo As Short, ByRef TotalNode As Short, ByRef plotcol As Short)
        Dim iloop As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim tval As Object
        'this proc is before deletion

        'the double swap should not break Session("Node2Plot") mapping
        'because node indexes are used
        'and nodes are indexed as added
        'rather than the visible serial within the tree


        'Adjust Node Pointers
        For i2 = DeleteNodeNo To TotalNode
            '    Session("Node2Plot")(i2) = Session("Node2Plot")(i2 + 1)
            If Session("Node2Plot")(i2 + 1) = 0 Then
                tval = Session("Node2Plot")(i2)
                Session("Node2Plot")(i2) = Session("Node2Plot")(i2 + 1)
                Session("Node2Plot")(i2 + 1) = tval
            End If
            Session("LinePrefix")(i2) = Session("LinePrefix")(i2 + 1)
        Next i2
        Session("Node2Plot")(TotalNode) = 0
        Session("LinePrefix")(TotalNode) = ""

        'ADjust Session("tPLOT")
        Session("tCOUNTE") = Session("tCOUNTE") - 1 ''Do later?
        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
            For ICount = plotcol To Session("tCOUNTE")
                Session("tPLOT")(i2, ICount) = Session("tPLOT")(i2, ICount + 1)
                Session("PlotRI")(i2, ICount) = Session("PlotRI")(i2, ICount + 1)
            Next ICount
            Session("tPLOT")(i2, Session("tCOUNTE") + 1) = 0
            Session("PlotRI")(i2, Session("tCOUNTE") + 1) = ""
        Next i2

        ' Take care of Grid Display Variables Shifting
        For iloop = plotcol To Session("tCOUNTE") Step 1
            Session("FileNumbr")(iloop) = Session("FileNumbr")(iloop + 1)
            Session("tVARN")(1, iloop) = Session("tVARN")(1, iloop + 1)
            Session("tVARN")(2, iloop) = Session("tVARN")(2, iloop + 1)
            Session("tVARN")(3, iloop) = Session("tVARN")(3, iloop + 1)
            Session("tGR1N")(iloop) = Session("tGR1N")(iloop + 1)
            Session("tGR2N")(iloop) = Session("tGR2N")(iloop + 1)
            Session("tGR3N")(iloop) = Session("tGR3N")(iloop + 1)
            Session("tGR4N")(iloop) = Session("tGR4N")(iloop + 1)
            Session("VL")(iloop) = Session("VL")(iloop + 1)
            Session("tFinYear")(iloop) = Session("tFinYear")(iloop + 1)

        Next iloop

        Session("tVARN")(1, Session("tCOUNTE") + 1) = ""
        Session("tVARN")(2, Session("tCOUNTE") + 1) = ""
        Session("tVARN")(3, Session("tCOUNTE") + 1) = ""
        Session("tGR1N")(Session("tCOUNTE") + 1) = ""
        Session("tGR2N")(Session("tCOUNTE") + 1) = ""
        Session("tGR3N")(Session("tCOUNTE") + 1) = ""
        Session("tGR4N")(Session("tCOUNTE") + 1) = ""
        Session("VL")(Session("tCOUNTE") + 1) = System.DBNull.Value
        Session("tFinYear")(Session("tCOUNTE") + 1) = 0

        'jrs 2005/04/25
        updateTreeIndices(treeScenario.Nodes, Trim(Str(DeleteNodeNo)))
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub DelAdjust_NodeOnly(ByRef DeleteNodeNo As Short, ByRef TotalNode As Short)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'this proc is before deletion


        'Adjust Node Pointers
        For i2 = DeleteNodeNo To TotalNode - 1
            Session("Node2Plot")(i2) = Session("Node2Plot")(i2 + 1)
            Session("LinePrefix")(i2) = Session("LinePrefix")(i2 + 1)
        Next i2
        Session("Node2Plot")(TotalNode) = 0
        Session("LinePrefix")(TotalNode) = ""

        'jrs 2005/04/25
        updateTreeIndices(treeScenario.Nodes, Trim(Str(DeleteNodeNo)))
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Function PStr(ByRef PNode As Microsoft.Web.UI.WebControls.TreeNode) As String
        Dim k As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim tsce, tdat, ctdat As String
        Dim yr As Short ', lentdat%, divtdat%
        'Dim tdat$, tsce$, ctdat$  ', lentdat%, divtdat%
        tdat = ""
        'tdat$ = BuildCustomStr(Session("Node2Plot")(PNode.Index))
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(PNode.Index). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
        k = Session("Node2Plot")(PNode.GetNodeIndex)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
        tsce = GetFirstToken(CStr(Session("LinePrefix")(PNode.GetNodeIndex)), ",")

        chk = IfsVars.Item(Session("tVARN")(1, k)).Index

        tdat = BuildCustomStr(k)
        ctdat = ""
        Do While True
            ctdat = ctdat & Mid(tdat, 1, 50)
            If Len(tdat) <= 50 Then
                Exit Do
            End If
            tdat = Mid(tdat, 51)
            Do While True
                If VB.Right(ctdat, 1) <> "," And Len(tdat) > 0 Then
                    ctdat = ctdat & Mid(tdat, 1, 1)
                    tdat = Mid(tdat, 2)
                Else
                    Exit Do
                End If
            Loop
            'tdat$ = Mid(tdat$, Len(ctdat$) + 1)
            ctdat = ctdat & "<br>" & Space(77) '45
        Loop 'Next i2
        tdat = ctdat
        yr = Val(VB.Right(tsce, 4))
        If yr > 0 Then
            tsce = Mid(tsce, 1, Len(tsce) - 4) & " (Shift " & Trim(Str(yr)) & " years)"
        End If
        PStr = tsce & "  =  " & tdat

    End Function
    '=============================ScenarioCreate.Frm=============================
    Private Sub InitVars()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("tCOUNTE") = 0

        'erase Session("tVARN")$, Gr1$ etc.

        Erase Session("tPLOT")
        Erase Session("PlotRI")

        'mti WebForm Changes From June 21, 2004  -start
        'Erase Session("LinePrefix")
        'mti WebForm Changes From June 21, 2004  - end 

        'Erase PLOTCust()
        Erase Session("Node2Plot")
        'Erase CustomStr()

        ReDim Session("tPLOT")(MaxYears, MaxVariables)

        ReDim Session("PlotRI")(MaxYears, MaxVariables)

        ReDim Session("LinePrefix")(MaxVariables + 20)

        ReDim Session("Node2Plot")(MaxVariables + 20)

        Erase Session("tFinYear")
        Erase Session("tVARN")
        Erase Session("tGR1N")
        Erase Session("tGR2N")
        Erase Session("tGR3N")
        Erase Session("tGR4N")

        'mti WebForm Changes From June 26, 2004
        ReDim Session("tVARN")(6, MaxVariables)

        ReDim Session("tGR1N")(MaxVariables + 1)
        ReDim Session("tGR2N")(MaxVariables + 1)
        ReDim Session("tGR3N")(MaxVariables + 1)
        ReDim Session("tGR4N")(MaxVariables + 1)
        ReDim Session("tFinYear")(MaxVariables)

        blnScenarioDirty = False
    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Sub Repaint_Right()
        'mtiwebformsMarch2004 (2)ScenarioCreate
        Dim j As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'If Session("LinePrefix")(Curnode.Index) <> "" Then ' otRegion Then
        lbl_hint.Visible = False
        pnlMaxmin.Visible = True
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.Index' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Index.html'.
        'j = Curnode.GetNodeIndex
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CurNodeNo = j '???
        'JRS 03/09/04
        'mtiwebformsMarch2004 (2)ScenarioCreate
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        j = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        'j = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'jrs 2024/12/10
        If Session("LinePrefix") Is Nothing Then Exit Sub

        Scenariotype = Session("LinePrefix")(j)

        _Opt_highlow_0.Checked = False
        _Opt_highlow_1.Checked = False
        _Opt_highlow_2.Checked = False

        Select Case UCase(Mid(Scenariotype, 1, 3)) 'jrs 2006/08/01 one of the scenarios was created manually and read: Custom, instead of CUSTOM
            Case "MED"
                'mtiwebformsMarch2004 (2)ScenarioCreate
                _Opt_highlow_2.Checked = True
                'cmbShiftYears.ListIndex =Val (Mid(Scenariotype, 7, 4)) - 1
                Call Prepare_Medium()
            Case "HIG"
                blnSettingYears = True
                'mti August 2004
                cmbShiftYears.SelectedIndex = Val(Mid(Scenariotype, 5, 4)) '- 1
                blnSettingYears = False
                'mtiwebformsMarch2004 (2)ScenarioCreate
                _Opt_highlow_0.Checked = True
                Call Prepare_high()
            Case "LOW"
                blnSettingYears = True
                'mti August 2004
                cmbShiftYears.SelectedIndex = Val(Mid(Scenariotype, 4, 4)) '- 1
                blnSettingYears = False
                'mtiwebformsMarch2004 (2)ScenarioCreate
                _Opt_highlow_1.Checked = True
                Call Prepare_low()
                    'mti 19/09- Scenario Slider
            Case "SLI"
                'mtiwebformsMarch2004 (2)ScenarioCreate
                _Opt_highlow_0.Checked = False
                _Opt_highlow_1.Checked = False
                _Opt_highlow_2.Checked = False
                'end mtiwebformsMarch2004 (2)ScenarioCreate
                '
                blnSettingYears = True
                'mti August 2004
                cmbShiftYears.SelectedIndex = Val(Mid(Scenariotype, 7, 4)) '- 1
                blnSettingYears = False
                Call Prepare_Slider() 'contains graphupdate
                    'slider slides in graphupdate routine


            Case "CUS"
                Call Prepare_Custom()
            Case Else
        End Select
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VShiftDown(Session("SEL")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VShiftUp(Session("SEL")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

        'mtiwebformsMarch2004 (2)ScenarioCreate
        If VShiftUp(Session("SEL")) = 0 And VShiftDown(Session("SEL")) = 0 Then 'Only allow custom
            _Opt_highlow_0.Visible = False
            _Opt_highlow_1.Visible = False
            _Opt_highlow_2.Text = "Base"
        Else
            _Opt_highlow_0.Visible = True
            _Opt_highlow_1.Visible = True
            _Opt_highlow_2.Text = "Base"
        End If
        'end mtiwebformsMarch2004 (2)ScenarioCreate

        'jrs 2023/07/11
        lblDesc.Text = V(Session("SEL")) & " - " & Curnode.Parent.Text & " - " & VUnits(Session("SEL"))
        'jrs 2025/01/20
        lblDesc.Text = lblDesc.Text & " - " & Curnode.Text
        'jrs 2026/04/17
        If VExpl(Session("SEL")) > "" Then
            lblDesc.Text = lblDesc.Text & "<br><br>Explanation: " & VExpl(Session("SEL"))
        End If

        'JRS 04/27/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VType(Session("SEL")) <> 2 Then
            frm_options.Visible = True
            frm_time.Visible = True
            Frm_Custom.Visible = True
            'JRSVBNET VSc_slider.Visible = True
            'LBLSLIDERVAL.Visible = True
            pnlSlider.Visible = True
            'jrs 2023/07/11
            frm_VarDesc.Visible = True

        Else
            frm_options.Visible = False
            frm_time.Visible = False
            Frm_Custom.Visible = False
            'JRSVBNET VSc_slider.Visible = False
            'LBLSLIDERVAL.Visible = False
            pnlSlider.Visible = False
            'jrs 2023/07/11
            frm_VarDesc.Visible = False

        End If

    End Sub

    '=============================ScenarioCreate.Frm=============================
    Function BuildCustomStr(ByRef j As Short) As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'jrs 2008/05/30 Currency
        Dim CurrFactor As Single
        CurrFactor = 1
        'if it's a currency variable
        If VCurrency(CInt(Session("SEL"))) = "True" Then
            'load the currency convertion value
            CurrFactor = Session("CurrencyVal")
        End If

        'mtiwebformsMarch2004 (2)ScenarioCreate
        Dim TempStr As String

        TempStr = Trim(Str(Math.Round(Session("tPLOT")(1, j) * CurrFactor, 4))) 'jrs 2008/05/30 added * currFactor

        For i2 = 2 To Session("LastYear") - Session("FirstYear") + 1
            'jrs 2009/09/24 TempStr = TempStr & "," & Trim(Str(Math.Round(Session("tPLOT")(i2, j) * CurrFactor, 4))) 'jrs 2008/05/30 added * currFactor
            TempStr = TempStr & "," & Trim(Str(Math.Round(Session("tPLOT")(i2, j) * CurrFactor, 5)))
        Next i2

        'cut last comma and return
        BuildCustomStr = TempStr

    End Function

    '=============================ScenarioCreate.Frm=============================
    'Function Category_Node_Index(ByRef TreeView As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByRef CategoryNo As Short) As String
    Function Category_Node_Index(ByRef TargetTree As TreeView, ByRef CategoryNo As Short) As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mtiwebformsMarch2004 (2)ScenarioCreate - whole proc
        'First two places of key reserved for nodetype
        'e.g. "i " =issuearea, "d " = driver etc.
        Category_Node_Index = -1

        'If Not TreeView Is Nothing Then
        Dim index, index1 As Integer
        Dim CatFound As Boolean = False
        For index = 0 To TargetTree.Nodes.Count - 1  '.Count - 1
            'Val(Mid(SelNode.NodeData, 3))
            If Val(Mid(TargetTree.Nodes(index).NodeData, 3)) = CategoryNo Then
                Category_Node_Index = TargetTree.Nodes(index).GetNodeIndex
                CatFound = True
                'Else
                '    Category_Node_Index = Category_Node_Index(TreeView(index).Nodes, CategoryNo)
                '    If Category_Node_Index > -1 Then
                '        Exit Function
                '    End If
            Else
                If Not TargetTree.Nodes(index).Nodes Is Nothing Then
                    For index1 = 0 To TargetTree.Nodes(index).Nodes.Count - 1
                        If Val(Mid(TargetTree.Nodes(index).Nodes(index1).NodeData, 3)) = CategoryNo Then
                            Category_Node_Index = TargetTree.Nodes(index).Nodes(index1).GetNodeIndex
                            CatFound = True
                            Exit For
                        End If
                    Next
                End If

            End If
            If CatFound Then Exit For
        Next
        'End If

    End Function

    '=============================ScenarioCreate.Frm=============================
    Public Sub ReadMedium(ByRef plotcol As Short)
        Dim VARNUM As Object
        Dim NumberW As Single
        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        On Error GoTo TmpErr
        Dim Number As Single
        If Session("mnuSQLite") = "True" Then
            finam = Session("PutFdrvRun") & Session("ActiveBase") + ".DB" 'jrs 2005/05/26 "IFSBASE.RUN"
        Else
            finam = Session("PutFdrvRun") & Session("ActiveBase")
        End If
        Dim dfbase As IDataframe = Nothing 'dsb 2026/08/11
        'jrs 2017/01/04 Bilateral Trade
        'jrs 2018/01/06 If VDim3(SEL) = 2 Then
        If Session("SEL") > Session("orgMaxVars") And Session("orgMaxVars") <> Nothing Then
            For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("tPLOT")(i2, plotcol) = VarPoint(Session("SEL"))
            Next i2
        Else
            'jrs 2006/05/31 Centralize
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(V$(Session("SEL")), Session("SEL"), Session("tGR1N")(plotcol), -1, Session("tGR2N")(plotcol), -1, Session("tGR3N")(plotcol), -1,
                finam$, 1, True, True, Curnode.ID = "Group", 1, Session("tPLOT"), plotcol, 1, Session("FirstYear"), Session("LastYear"),
                0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"), dfVar:=dfbase) 'dsb 2026/08/11 Added  dfVar:=dfbase. Dataframe is needed to check if the parameter exists in the global parameters table and has a default/base value
            Else
                gLoadForDisplay(V$(Session("SEL")), Session("SEL"), Session("tGR1N")(plotcol), -1, Session("tGR2N")(plotcol), -1, Session("tGR3N")(plotcol), -1,
                finam$, 1, True, True, Curnode.ID = "Group", 1, Session("tPLOT"), plotcol, 1, Session("FirstYear"), Session("LastYear"),
                0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        End If

        'dsb 2026/08/11 When there is no default/base value for the parameter in the globalparameters table, make the values over all years to 0
        If dfbase Is Nothing OrElse dfbase.RowCount = 0 Then
            For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("tPLOT")(i2, plotcol) = 0
            Next
        End If
        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
            If i2 = 1 Or Session("tPLOT")(i2, plotcol) = Session("tPLOT")(i2 - 1, plotcol) Then
                Session("PlotRI")(i2, plotcol) = "REPEAT"
                If Session("PlotRI")(i2 - 1, plotcol) = "INTERPOLATE" Then
                    Session("PlotRI")(i2 - 1, plotcol) = "REPEAT"
                End If
            Else
                Session("PlotRI")(i2, plotcol) = "INTERPOLATE"
            End If
        Next i2


        Exit Sub
TmpErr:

        LoadMessage("In TmpErr", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


    End Sub

    '=============================ScenarioCreate.Frm=============================
    Private Sub setslider(ByRef plotcol As Short)
        'mti WebForm Changes From July 04, 2004
        Dim NewLargeChange As Short
        Dim Number As Single
        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim HighVal, LowVal As Object
        If Session("mnuSQLite") = "True" Then
            finam = Session("PutFdrvRun") & Session("ActiveBase") & ".DB"  'jrs 2005/05/26 "IFSBASE.RUN"
        Else
            finam = Session("PutFdrvRun") & Session("ActiveBase")
        End If
        '    Close  'This fixed error if have created grouping; now seems to be fixed
        'properly
        'jrs 2005/02/12
        'Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, finam, OpenMode.Binary)
        ''UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Session("RECORD") = (Session("VL")(plotcol)) * 4 - 3
        ''UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'FileGet(fNum, Number, Session("RECORD"))
        'FileClose(fNum)
        'jrs 2020/09/17 use Vals instead of session("tPLOT") causing problems
        'when doing customization
        Dim Vals(1) As Single

        'jrs 2021/03/01
        If Session("orgMaxVars") Is Nothing OrElse Session("SEL") <= Session("orgMaxVars") Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(V$(Session("SEL")), Session("SEL"), Session("tGR1N")(plotcol), -1, Session("tGR2N")(plotcol), -1, Session("tGR3N")(plotcol), -1,
                finam$, 1, True, True, Curnode.ID = "Group", 1, Vals, plotcol, 1, Session("FirstYear"), Session("FirstYear"),
                0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay(V$(Session("SEL")), Session("SEL"), Session("tGR1N")(plotcol), -1, Session("tGR2N")(plotcol), -1, Session("tGR3N")(plotcol), -1,
                finam$, 1, True, True, Curnode.ID = "Group", 1, Vals, plotcol, 1, Session("FirstYear"), Session("FirstYear"),
                0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        End If

        'Number = Session("tPLOT")(1, plotcol)
        Number = Vals(1)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("VarVal") = System.Math.Round(Number, 4) 'If more than 7- does e : 1.2209873245E-02 etc.

        If IsDBNull(VShiftUp(Session("SEL"))) Then VShiftUp(Session("SEL")) = 0 'protect against holes in IFsVar.mdb
        If IsDBNull(VShiftDown(Session("SEL"))) Then VShiftDown(Session("SEL")) = 0
        If VShiftUp(Session("SEL")) < 0 Then VShiftUp(Session("SEL")) = 0
        If VShiftDown(Session("SEL")) > 0 Then VShiftDown(Session("SEL")) = 0
        If IsDBNull(VMax(Session("SEL"))) Then VMax(Session("SEL")) = 0
        If IsDBNull(VMin(Session("SEL"))) Then VMin(Session("SEL")) = 0
        If VMax(Session("SEL")) <= Session("VarVal") Then VMax(Session("SEL")) = Session("VarVal") + 1
        If VMin(Session("SEL")) >= Session("VarVal") Then VMin(Session("SEL")) = Session("VarVal") 'also assures max <> min
        'mti August 2004
        'HighVal = Session("VarVal") + VShiftUp(Session("SEL"))
        'LowVal = Session("VarVal") + VShiftDown(Session("SEL"))
        'If VShiftUp(Session("SEL")) = 0 And VShiftDown(Session("SEL")) = 0 Then 'Use Max and Min
        HighVal = VMax(Session("SEL"))
        LowVal = VMin(Session("SEL"))
        'End If
        'mti August 2004

        txtMax.Text = CType(HighVal, String).ToString
        txtMin.Text = CType(LowVal, String).ToString
        'If txtMin.Text > 1 Then
        '    txtSliderVal.Text = String.Format("{0:0.00}", Val(txtMin.Text))
        'Else
        '    txtSliderVal.Text = String.Format("{0:0.000}", Val(txtMin.Text))
        'End If
        If Session("VarVal") > 1 Then
            txtSliderVal.Text = String.Format("{0:0.00}", Val(Session("VarVal")))
        Else
            txtSliderVal.Text = String.Format("{0:0.000}", Val(Session("VarVal")))
        End If

    End Sub

    '=============================ScenarioCreate.Frm=============================
    'UPGRADE_NOTE: VSc_slider.Change was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2010"'
    'UPGRADE_WARNING: VScrollBar event VSc_slider.Change has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    Private Sub VSc_slider_Change(ByVal newScrollValue As Integer)
        Dim VLine As String
        Dim Number As Single
        Dim finam As String
        Dim j As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If blnSettingSlider Then
            Exit Sub
        End If

        Dim SliderVal As Object
        Dim k As Short

        'JRS 03/09/04 k = CurNodeNo 'node pointer
        k = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("LinePrefix")(k) = "SLIDER" & VB6.Format(Trim(cmbShiftYears.SelectedValue), "0000") & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        j = Session("Node2Plot")(k) 'Session("tPLOT") pointer
        'Session("SEL") = FindVarNumb(Session("tVARN")$(1, j))
        chk = IfsVars.Item(Session("tVARN")(1, j)).Index
        Session("SEL") = chk


        SliderVal = newScrollValue
        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        SliderVal = SliderVal / ValMult

        'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'LBLSLIDERVAL.Text = VB6.Format(SliderVal, "0.0##") '"general number") '"standard")
        'txtSliderVal.Text() = VB6.Format(SliderVal, "0.0##") '"general number") '"standard")

        '_Opt_highlow_2.Value = True
        'mtiwebformsMarch2004 (2)ScenarioCreate
        _Opt_highlow_0.Checked = False
        _Opt_highlow_1.Checked = False
        _Opt_highlow_2.Checked = False
        'end mtiwebformsMarch2004 (2)ScenarioCreate
        If Session("mnuSQLite") = "True" Then
            finam = Session("PutFdrvRun") & Session("ActiveBase") & ".DB" 'jrs 2005/05/26 "IFSBASE.RUN"
        Else
            finam = Session("PutFdrvRun") & Session("ActiveBase")
        End If
        'jrs 2005/02/12
        'Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, finam, OpenMode.Binary)
        ''UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Session("RECORD") = (Session("VL")(j)) * 4 - 3
        ''VARNUM = PlotCol
        ''UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'FileGet(fNum, Number, Session("RECORD"))
        'FileClose(fNum)
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(V$(Session("SEL")), Session("SEL"), Session("tGR1N")(j), -1, Session("tGR2N")(j), -1, Session("tGR3N")(j), -1,
        finam$, 1, True, True, Curnode.ID = "Group", 1, Session("tPLOT"), j, 1, Session("FirstYear"), Session("LastYear"),
        0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Else
            gLoadForDisplay(V$(Session("SEL")), Session("SEL"), Session("tGR1N")(j), -1, Session("tGR2N")(j), -1, Session("tGR3N")(j), -1,
        finam$, 1, True, True, Curnode.ID = "Group", 1, Session("tPLOT"), j, 1, Session("FirstYear"), Session("LastYear"),
        0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
        Number = Session("tPLOT")(1, j)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("VarVal") = System.Math.Round(Number, 4) 'If more than 7- does e : 1.2209873245E-02 etc.
        'does not convert to val properly if more than 4 digits after decimal

        If cmbShiftYears.SelectedValue = "" Then 'Meant to be 0 years
            'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            VLine = Trim(Str(SliderVal)) & ":200"
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            VLine = Trim(Str(Session("VarVal"))) & "," & Trim(Str(SliderVal)) & "@" & Trim(cmbShiftYears.SelectedValue) & "," & Trim(Str(SliderVal)) & ":200"
        End If

        '         Trim(Str(cmbShiftYears.Text)) + "," + _
        '
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Call ParseCustomStr(VLine, CShort(j), Session("SceLag"))


        Call GraphUpdate()


        Scenariotype = "HIGH"
        ValStr = ""

        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.ForeColor' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.ForeColor.html'.
        'JRSVBNET treeScenario.SelectedItem.ForeColor = System.Convert.ToUInt32(QBColor(HIGHCOLOR))

        cmbShiftYears.Enabled = True
        lblFullyCustomized.Visible = False
        lblCustomized.Visible = True

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


    End Sub

    '=============================ScenarioCreate.Frm=============================
    Private Sub Prepare_Slider()
        'mti August 2004
        Dim j As Short 'Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim k As Short

        'JRS 03/09/04 k = CurNodeNo 'node pointer
        'mtiwebforms May 2004 (2)ScenarioCreate
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        'the following will be used as k in frm_scenariocreatePego
        Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        k = Session("Scenario_CurNodeFlatIndex")

        'k = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        j = Session("Node2Plot")(k) 'Session("tPLOT") pointer
        chk = IfsVars.Item(Session("tVARN")(1, j)).Index
        Session("SEL") = chk

        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'mti August 2004
        setslider(j)
        txtSliderVal.Text = Session("tPLOT")(cmbShiftYears.SelectedIndex + 1, j)
        'mti August 2004 -end 

        lblJS.Visible = True
        Call GraphUpdate()

        blnSettingSlider = True
        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET VSc_slider.Value = Session("VarVal") * ValMult
        blnSettingSlider = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'LBLSLIDERVAL.Text = VB6.Format(Session("VarVal"), "0.0##") '"general number") '"Standard")
        'txtSliderVal.Text = VB6.Format(Session("VarVal"), "0.0##") '"general number") '"Standard")
        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.ForeColor' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.ForeColor.html'.
        'JRSVBNET treeScenario.SelectedItem.ForeColor = System.Convert.ToUInt32(OFColor)

        cmbShiftYears.Enabled = True
        lblFullyCustomized.Visible = False
        lblCustomized.Visible = True

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    '=============================ScenarioCreate.Frm=============================
    Private Function DelMsg(ByRef j As Short) As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim k As Short
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        k = Session("Node2Plot")(j)
        If Session("tVARN")(1, k) <> "" Then
            chk = IfsVars.Item(Session("tVARN")(1, k)).Index
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object chk. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("SEL") = chk
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VDim1(Session("SEL")) = 1 Then 'Assuming Region is the First SubDim
            DelMsg = "Countries"
        Else
            DelMsg = "Dimensions"
        End If
    End Function


    '=============================ScenarioCreate.Frm=============================
    Private Function CheckGroups(ByRef G As String) As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/4/01 GROUPING
        'Dim dbG As DAO.Database
        'Dim tblG As DAO.Recordset
        'mti Scenario ADO.net May 2004
        Dim Rcnt As Short
        Dim dbG As String
        Dim tblG As DataTable

        'dbG = DAODBEngine_definst.OpenDatabase(IFs_MDB2Active)
        dbG = Session("IFs_MDB2Active")

        'mti WebForm Changes From July 02, 2004
        'SetDataTablefromAccess(dbG, "SELECT * FROM " + GroupIDNew, "", tblG)
        'jrs 2023/06/09 added UPPER
        SetDataTablefromAccess(dbG, "SELECT * FROM " & GroupIDNew & " ORDER BY UPPER(GroupName)", "", tblG)

        'tblG = dbG.OpenRecordset(GroupIDNew)
        CheckGroups = "False"
        For Rcnt = 0 To tblG.Rows.Count - 1
            'Do
            'JRS 5/25/02
            'If UCase(G) = UCase(tblG.Fields("GroupName").Value) Then
            'jrs 2023/06/09
            If UCase(Trim(G)) = UCase(Trim(tblG.Rows(Rcnt).Item("GroupName"))) Then
                CheckGroups = "True"
                Exit For 'Do
            End If
            'tblG.MoveNext()
            'If tblG.EOF Then Exit Do
            'Loop
        Next
        tblG = Nothing
    End Function


    '=============================ScenarioCreate.Frm=============================
    Private Sub LoadCountries(ByRef GroupName As String)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -



        'JRS 11/2/01 GROUPING
        'mti Scenario ADO.net May 2004
        Dim RCnt As Short

        Dim dbG As String 'DAO.Database
        Dim valtblG As DataTable 'DAO.Recordset
        Dim resG As DataTable 'DAO.Recordset
        'dbG = DAODBEngine_definst.OpenDatabase(IFs_MDB2Active)
        dbG = Session("IFs_MDB2Active")
        'jrs 2022/12/21
        If UseOnlySQLite Then
            SetDataTablefromSQLite(dbG, "SELECT * FROM " & GroupIDNew & " WHERE upper(GroupName) = """ & GroupName.ToUpper & """", "", resG)
        Else
            SetDataTablefromAccess(dbG, "SELECT * FROM " & GroupIDNew & " WHERE GroupName = """ & GroupName & """", "", resG)
        End If
        'resG = dbG.OpenRecordset("SELECT * FROM " & GroupIDNew & " WHERE GroupName = '" & GroupName & "'")
        'GroupNum = resG("GroupNum")
        'If Not (resG.BOF And resG.EOF) Then
        If resG.Rows.Count > 0 Then
            'GroupNum = resG.Fields("GroupNum").Value
            GroupNum = resG.Rows(0).Item("GroupNum")
        Else
            GroupNum = -1
            GroupTot = 0
            'resG.Close()
            'dbG.Close()
            Exit Sub
        End If
        'resG.Close()
        resG = Nothing

        '    Dim valtbl As Recordset
        'valtblG = dbG.OpenRecordset(Grouping)
        SetDataTablefromAccess(dbG, "SELECT * FROM " + Grouping + " order by RegionNo", "", valtblG)
        'valtblG.Index = "RegionNo"
        Dim iTotal As Short
        iTotal = 0

        '   Load Group Member Names List Box
        If valtblG.Rows.Count > 0 Then
            'If Not valtblG.BOF Then
            'valtblG.MoveFirst()
            'JRS 2/15/02 For ICount% = 1 To valtblG.RecordCount
            For RCnt = 0 To valtblG.Rows.Count - 1
                'Do While Not valtblG.EOF
                'JRS 2/15/02 For JCount% = 1 To valtblG.Fields("TotalGroups")
                'JRS 2/15/02 TmpStr$ = "Group" + Trim(Str(JCount%))
                'JRS 2/15/02 If valtblG.Fields(TmpStr$) = GroupNum Then
                'If valtblG.Fields("GroupNo").Value = GroupNum Then
                If valtblG.Rows(RCnt).Item("GroupNo") = GroupNum Then
                    iTotal = iTotal + 1
                    'CountryGroup(iTotal) = valtblG.Fields("Region").Value 'RegName$(ICount%)
                    CountryGroup(iTotal) = valtblG.Rows(RCnt).Item("Region")
                End If
                'JRS 2/15/02 Next JCount%
                'valtblG.MoveNext()
                'JRS 2/15/02 Next ICount%
                'Loop
            Next
        End If
        'valtblG.Close()
        valtblG = Nothing
        'dbG.Close()
        GroupTot = iTotal

    End Sub

    '=============================ScenarioCreate.Frm=============================
    Private Function FindCountryGroup(ByRef C As String) As Boolean
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/27/01 GROUPING
        FindCountryGroup = False
        Dim i As Short
        For i = 1 To GroupTot
            If CountryGroup(i) = C Then
                FindCountryGroup = True
                Exit For
            End If
        Next i
    End Function

    Private Function RegionIdx(ByRef sReg As String) As Short
        Dim eleloop As Short
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For eleloop = 1 To MaxVal(1) 'Up to number of regions
            If UCase(sReg) = UCase(GRPS(1, eleloop)) Then
                RegionIdx = eleloop
                Exit For
            End If
        Next eleloop
    End Function

    '=============================ScenarioCreate.Frm=============================
    Private Sub UpdateWorkingFile(ByRef sFileName As String, ByRef sFileTitle As String)
        'jrs 2005/06/14
        If Dir(sFileName) = "" Then Exit Sub
        'mtiwebformsMarch2004 (2)ScenarioCreate
        Dim k As Short
        Dim j As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        On Error GoTo er_Save
        Dim intfile As Short
        Dim intNotefile As Short
        Dim NoteTxt As String
        Dim WriteStr As String
        intfile = FreeFile()
        FileOpen(intfile, sFileName, OpenMode.Output)
        For j = 1 To Support.TreeViewManager.CountNodes(treeScenario.Nodes)
            'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            WriteStr = Session("LinePrefix")(j)
            If WriteStr <> "" Then 'write

                'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                k = Session("Node2Plot")(j)

                chk = IfsVars.Item(Session("tVARN")(1, k)).Index
                Session("SEL") = chk


                Select Case Mid(WriteStr, 1, 3)
                    Case "MED"
                        WriteStr = WriteStr & "," & BuildCustomStr(k) '+ Trim(VMEDIUM(Session("SEL")))   ' + ":" + Trim(Str(frm_MainMenu.IFsGUIctrl1.Session("LastYear") - frm_MainMenu.IFsGUIctrl1.Session("FirstYear") + 1))
                    Case "HIG"
                        WriteStr = WriteStr & "," & BuildCustomStr(k) 'Trim(VHIGH(Session("SEL")))   '+ ":" + Trim(Str(frm_MainMenu.IFsGUIctrl1.Session("LastYear") - frm_MainMenu.IFsGUIctrl1.Session("FirstYear") + 1))
                    Case "LOW"
                        WriteStr = WriteStr & "," & BuildCustomStr(k) 'Trim(VLOW(Session("SEL"))) '+ ":" + Trim(Str(frm_MainMenu.IFsGUIctrl1.Session("LastYear") - frm_MainMenu.IFsGUIctrl1.Session("FirstYear") + 1))
                            'mti 19/09- Scenario Slider
                    Case "SLI"
                        WriteStr = WriteStr & "," & BuildCustomStr(k) 'Trim(VLOW(Session("SEL"))) '+ ":" + Trim(Str(frm_MainMenu.IFsGUIctrl1.Session("LastYear") - frm_MainMenu.IFsGUIctrl1.Session("FirstYear") + 1))
                    Case "CUS"
                        WriteStr = WriteStr & "," & BuildCustomStr(k)
                End Select
                PrintLine(intfile, WriteStr)
            End If
        Next j

        'write nonScenario
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        For i2 = 0 To lst_nonscenario.Items.Count - 1
            'JRSVBNET PrintLine(intfile, "CUSTOM," & VB6.GetItemString(lst_nonscenario, i2))
        Next i2
        '    Close #intfile
        '    If sFileTitle$ <> "Working.Sce" Then

        'jrs 2005/06/14
        If Dir(Session("ScenarioFileDir") & "working.txt") <> "" Then
            PrintLine(intfile, "COMMENT," & "START")
            intNotefile = FreeFile()
            FileOpen(intNotefile, Session("ScenarioFileDir") & "working.txt", OpenMode.Input)
            NoteTxt = InputString(intNotefile, LOF(intNotefile))
            PrintLine(intfile, NoteTxt)
            FileClose(intNotefile)
        End If
        '    End If

        FileClose(intfile)


        Exit Sub
er_Save:
    End Sub



    'JRS 07/10/03
    ReadOnly Property GroupCountry(ByVal idx As Short) As Object
        Get
            GroupCountry = CountryGroup(idx)
        End Get
    End Property
    'CONVERSION_ISSUE: Class 'System.Windows.Forms.VScrollBar' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.VScrollBar.Scroll' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.VScrollBar.html'.
    'Private Sub VSc_slider_Scroll(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ScrollEventArgs)
    'CONVERSION_ISSUE: Class System.Windows.Forms.ScrollEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'CONVERSION_ISSUE: Class System.Windows.Forms.ScrollEventType from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'Select Case eventArgs.Type
    'Case System.Windows.Forms.ScrollEventType.EndScroll
    'CONVERSION_ISSUE: Class System.Windows.Forms.ScrollEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '               VSc_slider_Change(eventArgs.NewValue)
    'End Select
    'End Sub


    Private Sub mnuDefine_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '  MsgBox (V$(Session("SEL")) + " - " + VDef$(Session("SEL")))
        Dim Definition As String
        'Dim DbMemo As DAO.Database
        'Dim ValTblMemo As DAO.Recordset
        'mtiwebformsMarch2004 (2)ScenarioCreate
        Dim DbMemo As String
        Dim ValTblMemo As DataTable

        lst_Varname_SelectedIndexChanged1()

        'DbMemo = DAODBEngine_definst.OpenDatabase(PutFdrv & "RUNFILES\IFSVar.MDB")
        DbMemo = Session("FdrvRun") & "IFSVar.MDB"
        'ValTblMemo = DbMemo.OpenRecordset("IFSVAR")
        SetDataTablefromAccess(DbMemo, "Select * from IfsVar", "Name", ValTblMemo)
        'ValTblMemo.Index = "NAME"
        'ValTblMemo.MoveFirst()
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Definition = Trim(" " & SeekValue(ValTblMemo, V(Session("SEL")), "Parameter Explanation"))
        'ValTblMemo.Seek("=", V(Session("SEL"))) 'XLCountry_name
        'If ValTblMemo.NoMatch Then
        If Definition = "" Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Definition = V(Session("SEL")) & " - " & VDef(Session("SEL"))
            'Else
            '    Definition = Trim(" " & ValTblMemo.Fields("Parameter Explanation").Value)
            '    If Definition = "" Then
            '        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            '        Definition = V(Session("SEL")) & " - " & VDef(Session("SEL"))
            '    End If
        End If

        LoadMessage(Definition, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    Private Sub mnuDrivers_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'JRS 03/31/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET getDrivers(V(Session("SEL")))
        '=============================ScenarioCreate.Frm=============================


    End Sub

    Private Sub mnuExplain_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_ISSUE: Form property frm_ScenarioCreate.HelpContextID was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET Me.HelpContextID = VHelpID(Session("SEL"))
        'CONVERSION_ISSUE: Class System.Windows.Forms.SendKeys from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'lst_Varname_SelectedIndexChanged1()
        'CStr(VHelpID(Session("SEL")))

        'System.Windows.Forms.SendKeys.Send("{F1}")

    End Sub


    Private Sub mnuSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'jrs 2023/07/11
        frm_VarDesc.Visible = False

        'mti Agust 2004
        Dim SCurnodeNo As String 'Microsoft.Web.UI.WebControls.TreeNode
        'mti August 2004

        Dim msg As String
        Dim SELN As String
        Dim CalcType As Object
        Dim CalcRunning As Object
        Dim SumOn As Object
        Dim ii As Short

        'jrs 2005/07/05
        If IsNothing(Session("LoadSearchIdx")) Then
            lst_Varname_SelectedIndexChanged1()
        End If

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti ScenarioCreate May 2004 Uncomment and replace with following later
        'If mnuSelect.Text = "De&lete" Then
        If False Then
            'Deleteing from nonScenario list box
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If False Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("Would you like to delete the selected parameter/initial condition?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                lst_nonscenario.Items.RemoveAt((lst_nonscenario.SelectedIndex))
                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                If lst_nonscenario.Items.Count = 0 Then
                    Frm_nonscenario.Visible = False
                    'mti ScenarioCreate May 2004 Uncomment later
                    'mnuOther.Visible = False
                End If
            End If
            Exit Sub
        End If
        Dim ntext As String
        Dim curkey As String

        '   Session("SEL") holds # of variable; SELN$ holds name of variable
        '   -----------------------------------------------------

        '   Varsel Code omitted

        '   Check for Regionalized Variable to be Displayed in Groupings
        '   GroupOn will flag Gridput to sum across regions of the group
        '   ------------------------------------------------------------

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VDim1(Session("SEL")) = 1 And Session("FlagGroup") = True Then
            groupon = True
        Else
            groupon = False
        End If
        '
        '   Check for Attempt to Compute WW variable across groups
        '   ------------------------------------------------------
        ' Code Omitted

        '   Initialize Dimensions, Reset Variable Pointer
        '   ---------------------------------------------
        'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label1 = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object Label2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label2 = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label3 = ""
        SEL1 = ""
        sel2 = ""

        VPTR = 0
        Session("VPTR") = VPTR

        'JRS 2007/05/15
        Session("SEL1") = SEL1

        '   Check if variable has subdimensions
        '   -----------------------------------
        SubDimNum = 0
        If VDim1(Session("SEL")) > 0 Then
            SubDimNum = 1 'have at least 1 dimension
        End If
        If VDim2(Session("SEL")) > 0 Then
            SubDimNum = 2 'have both dimensions
        End If
        'jrs 2006/05/30 dim3
        If VDim3(Session("SEL")) > 0 Then
            SubDimNum = 3
        End If
        'JRS 03/04/04
        Session("SubDimNum") = SubDimNum

        If SubDimNum = 0 Then 'no sub dimension
            'UPGRADE_WARNING: Couldn't resolve default property of object VPTR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            VPTR = 1
            'JRS 03/04/04
            Session("VPTR") = VPTR
            'add driver with no subdim
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ntext = VScenario(Session("SEL"))

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            curkey = "d " & V(Session("SEL"))

            i2 = Support.TreeViewManager.CountNodes(treeScenario.Nodes)

            CurNodeNo = AddChildNode(treeScenario, ntext, curkey, False, Category_Node_Index(treeScenario, SaveVCat)) 'Savevcat + 1  : '1 for root node
            'Mohammod 08 October, 2001
            'CONVERSION_NOTE: Property 'MSComctlLib.NodeClass.Selected' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.Selected.html'.
            'treeScenario.Nodes.Item(CurNodeNo).Selected = True 
            Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
            If Support.TreeViewManager.CountNodes(treeScenario.Nodes) > i2 Then
                'new node added
                'mti Agust 2004
                SCurnodeNo = CurNodeNo

                Call HoldNodeInfo()

                'blank subdim display grid strings
                Session("tGR1N")(Session("tCOUNTE")) = ""
                Session("tGR2N")(Session("tCOUNTE")) = ""

                Call Prepare_Right()
                Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")
                Session("cur_file") = "Working.Sce"
                Call InitTree()
                Call InitVars() 'initializes blnscenariodirty
                Session("cur_file") = "Working.Sce"
                blnScenarioDirty = True 'Reset after InitVars
                Call BuildDrivers(Session("ScenarioFileDir") & "Working.Sce")
                CurNodeNo = SCurnodeNo
                'Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
                Curnode = Support.TreeViewManager.GetNode(treeScenario.Nodes, CurNodeNo)
                Support.TreeViewManager.SetSelectedNode(treeScenario, Curnode)
                'mti August 2004 end
                Call Prepare_Right()

            End If

            frm_varname.Visible = False

            'CONVERSION_NOTE: Method 'MSComctlLib.NodeClass.EnsureVisible' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.html'.
            'treeScenario.Nodes.Item(1).EnsureVisible() 
            'CONVERSION_NOTE: Method 'AxMSComctlLib.AxTreeView.CtlRefresh' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSComctlLib.AxTreeView.CtlRefresh().html'.
            'treeScenario.CtlRefresh() 
            'UPGRADE_WARNING: Couldn't resolve default property of object SumOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ElseIf SumOn = True And SubDimNum = 1 Then 'Sum Across Single Dim
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DimNum = VDim1(Session("SEL"))
            'JRS 03/04/04
            Session("DimNum") = DimNum
            'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label1 = "ALL"
            'UPGRADE_WARNING: Couldn't resolve default property of object Label2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label2 = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label3 = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For ii = 1 To MaxVal(DimNum) - 1
                'UPGRADE_WARNING: Couldn't resolve default property of object VPTR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                VPTR = ii 'Variable pointer shift reflecting dimensionality
                'JRS 03/04/04
                Session("VPTR") = VPTR
                'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If ii = MaxVal(DimNum) - 1 Then 'Finished with summation across all
                    'UPGRADE_WARNING: Couldn't resolve default property of object SumOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    SumOn = False
                    'UPGRADE_WARNING: Couldn't resolve default property of object CalcType. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object CalcRunning. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If CalcType = "None" Then CalcRunning = False
                End If
            Next ii
            'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            WinFormsToWeb_FocusControl.Focus("lst_varname")
        Else 'There are two dimensions; start with 1st
            SubDimDone = "False" 'remember to come back to 2nd dimension
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DimNum = VDim1(Session("SEL")) 'identify 1st dimension
            'JRS 03/04/04
            Session("DimNum") = DimNum
            Session("SubDimDone") = SubDimDone
            Call DimLoad()
        End If
        Exit Sub


BadVariable:
        msg = SELN & " is an incorrect variable name"

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


    End Sub
    Private Sub mnuViewEquations_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'JRS 03/31/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_ISSUE: ListBox property lst_varname.HelpContextID was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET lst_varname.HelpContextID = getHelpId(Int(Val(Session("SEL"))))
        'CONVERSION_ISSUE: Class System.Windows.Forms.SendKeys from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.SendKeys.Send("{F1}")

    End Sub


    'JRS 03/05/04
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)


        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'mti August 2004 2 -start
        If lst_varname.SelectedIndex = -1 Then Exit Sub
        'mti August 2004 2 -end 

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Select"
                mnuSelect_Click(sender, e)
            Case "Drivers"
            Case "Explain (Pop-Up)"

            Case "View Equations (Pop-Up)"
            Case "Define"
                mnuDefine_Click(sender, e)
        End Select
    End Sub

    'JRS 03/08/04
    Private Function TreeIndexToFlatIndex(ByVal TreeView As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByVal TreeIndex As String) As Integer
        'jrs 2005/04/25
        TreeIndexToFlatIndex = CInt(Val(Support.TreeViewManager.GetNode(TreeView, TreeIndex).Target))
        'Dim index As Integer
        'Dim counter As Integer = 0
        'Dim flag As Boolean = False
        'GetTreeIndexToFlatIndex(TreeView, counter, TreeIndex, flag)
        'TreeIndexToFlatIndex = counter - 1
    End Function

    'JRS 03/08/04
    Private Sub GetTreeIndexToFlatIndex(ByVal Nodes As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByRef count As Integer, ByVal TreeIndex As String, ByRef FoundFlag As Boolean)
        If Not Nodes Is Nothing Then
            Dim index As Integer

            For index = 0 To Nodes.Count - 1
                If FoundFlag Then Exit Sub
                count = count + 1
                If Nodes(index).GetNodeIndex = TreeIndex Then
                    FoundFlag = True
                    Exit Sub
                End If
                If Not Nodes(index).Nodes Is Nothing Then
                    GetTreeIndexToFlatIndex(Nodes(index).Nodes, count, TreeIndex, FoundFlag)
                End If
            Next
        End If
    End Sub
    'JRS 03/09/04
    Private Function TreeToList(ByVal TreeView As Microsoft.Web.UI.WebControls.TreeNodeCollection) As Microsoft.Web.UI.WebControls.TreeNodeCollection
        Dim index As Integer
        Dim NodeList As New Microsoft.Web.UI.WebControls.TreeNodeCollection
        GetTreeToList(TreeView, NodeList)
        TreeToList = NodeList
    End Function

    'JRS 03/09/04
    Private Sub GetTreeToList(ByVal Nodes As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByRef NodeList As Microsoft.Web.UI.WebControls.TreeNodeCollection)
        If Not Nodes Is Nothing Then
            Dim index As Integer

            For index = 0 To Nodes.Count - 1
                NodeList.Add(Nodes(index).Clone)
                NodeList(NodeList.Count - 1).NodeData = Nodes(index).GetNodeIndex
                If Not Nodes(index).Nodes Is Nothing Then
                    GetTreeToList(Nodes(index).Nodes, NodeList)
                End If
            Next
        End If
    End Sub

    Private Sub _Opt_highlow_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Opt_highlow_1.CheckedChanged

    End Sub
    Sub ItemClickHandlerMain(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Scenario Files"
            Case "Clear Tree"
                mnuNew_Click(sender, e)
                    'jrs 2005/04/06
                    'jrs 2006/11/02 removing file picker
                    'Case "Other"
                    'mnuOpen_Click(sender, e)
            Case "Add"
                mnuAdd_Click(sender, e)
            Case "Name and Save"
                mnuSave_Click(sender, e)
            Case "Export to IFs Standard CSV Format" 'jrs 2024/08/19
                mnuExportIFsToStandCSV_Click()
            Case "Print"
                mnuPrint_Click(sender, e)
            Case "Delete" '"Delete Selected Driver/Dimension"
                MnuDelete_Click(sender, e)
            Case "Use Groups"
                mnuGroups_Click(sender, e)
            Case "Groups", "Countries"
                mnuGroups_Click(sender, e)
                    'SetGroupReg(sender, e,objEA.Item.Caption)
            Case "Annotate" '"Annotate Scenario"
                'ReadFormControlCaption(Me, Session("PutFdrvRun"))
                mnuNote_Click(sender, e)
            Case "Run" 'JRS 08/20/04
                mnuRunScenario_Click()
            Case "Search" 'jrs 2005/07/01
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_SearchParam.aspx")
            Case "Export Working File" 'jrs 2012/05/25
                mnuExportWorkSce_Click()
            Case Else 'jrs 2005/04/06
                'cdv 2021/04/27 Changed from using ScenarioOpen to ScenarioAdd to preserve previously added scenarios.
                'jrs 2022/01/14 changed from .ID to .URL
                If objEA.Item.URL = "ADD" Then
                    ScenarioAdd(objEA.Item.Caption & ".sce", objEA.Item.UserData)
                ElseIf objEA.Item.URL = "IMP" Then 'jrs 2024/08/21
                    ImportCSVtoSCE(objEA.Item.Caption, objEA.Item.UserData)
                Else
                    ScenarioOpen(objEA.Item.Caption, objEA.Item.UserData) 'cdv 2021/11/22 Changed to open scenarios, not add
                End If
        End Select
    End Sub
    '    Private Sub SetDataTablefromAccess(ByVal AccessDBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable) 'mtiwebforms April 2004 ADO.Net
    '        Dim objAdapter As New OleDbDataAdapter _
    '               (strSQL, _
    '               "Provider=Microsoft.Jet.OLEDB.4.0;" & _
    '               "Data Source=" & AccessDBPath)

    '        Dim objDataSet As New DataSet

    '        ' Copy the selected data into a dataset.
    '        objAdapter.Fill(objDataSet)
    '        DT = objDataSet.Tables(0)
    '        DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
    '        'dt.Rows(0).Item(resultfieldname))

    '    End Sub
    '    Private Function SeekValue(ByVal DT As DataTable, ByVal LookupValue As Object, ByVal ResultFieldName As String) As String
    '        'mtiwebforms April 2004 ADO.Net
    '        Dim row As DataRow = DT.Rows.Find(LookupValue)
    '        SeekValue = ""
    '        If Not row.HasErrors Then
    '            SeekValue = CType(row.Item(ResultFieldName), String)
    '        End If

    '    End Function







    Private Sub btnSlider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlider.Click
        Dim VLine As String
        Dim Number As Single
        Dim finam As String
        Dim j As Short 'Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'If blnSettingSlider Then
        '    Exit Sub
        'End If

        Dim SliderVal As Object
        Dim k As Short


        'mti September 2004 2 
        Dim TScenPref As String

        TScenPref = Session("LinePrefix")(TreeIndexToFlatIndex(treeScenario.Nodes, treeScenario.SelectedNodeIndex.ToString))

        If TScenPref = "" Then Exit Sub
        'mti September 2004 2 -end 

        'mtiwebforms May 2004 (2)ScenarioCreate
        Curnode = Support.TreeViewManager.GetSelectedNode(treeScenario)
        Session("Scenario_CurNodeFlatIndex") = TreeIndexToFlatIndex(treeScenario.Nodes, Curnode.GetNodeIndex)
        k = Session("Scenario_CurNodeFlatIndex")

        ''JRS 03/09/04 k = CurNodeNo 'node pointer
        'k = TreeIndexToFlatIndex(treeScenario.Nodes, CurNodeNo)



        'UPGRADE_WARNING: Couldn't resolve default property of object Session("LinePrefix")(k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("LinePrefix")(k) = "SLIDER" & VB6.Format(Trim(cmbShiftYears.SelectedValue), "0000") & "," & Mid(Support.TreeViewManager.GetSelectedNode(treeScenario).NodeData, 3)

        ''UPGRADE_WARNING: Couldn't resolve default property of object Session("Node2Plot")(k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        j = Session("Node2Plot")(k) 'Session("tPLOT") pointer

        chk = IfsVars.Item(Session("tVARN")(1, j)).Index
        Session("SEL") = chk


        'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'SliderVal = newScrollValue
        SliderVal = Val(txtSliderVal.Text) '* (VMax(Session("SEL")) - VMin(Session("SEL"))) + VMin(Session("SEL"))
        'txtSliderVal.Text = txtMin.Text
        'UPGRADE_WARNING: Couldn't resolve default property of object ValMult. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'SliderVal = SliderVal / ValMult

        'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'LBLSLIDERVAL.Text = VB6.Format(SliderVal, "0.0##") '"general number") '"standard")
        'txtSliderVal.Text = VB6.Format(SliderVal, "0.0##") '"general number") '"standard")

        '_Opt_highlow_2.Value = True
        'mtiwebformsMarch2004 (2)ScenarioCreate
        _Opt_highlow_0.Checked = False
        _Opt_highlow_1.Checked = False
        _Opt_highlow_2.Checked = False
        'end mtiwebformsMarch2004 (2)ScenarioCreate

        'jrs 2020/10/20 added ".db"
        If Session("mnuSQLite") = "True" Then
            finam = Session("PutFdrvRun") & Session("ActiveBase") & ".db" 'jrs 2005/05/26  "IFSBASE.RUN"
        Else
            finam = Session("PutFdrvRun") & Session("ActiveBase")
        End If
        'jrs 2005/02/12
        'Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, finam, OpenMode.Binary)
        ''UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Session("RECORD") = (Session("VL")(j)) * 4 - 3
        ''VARNUM = PlotCol
        ''UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'FileGet(fNum, Number, Session("RECORD"))
        'FileClose(fNum)
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(V$(Session("SEL")), Session("SEL"), Session("tGR1N")(j), -1, Session("tGR2N")(j), -1, Session("tGR3N")(j), -1,
        finam$, 1, True, True, Curnode.ID = "Group", 1, Session("tPLOT"), j, 1, Session("FirstYear"), Session("LastYear"),
        0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Else
            gLoadForDisplay(V$(Session("SEL")), Session("SEL"), Session("tGR1N")(j), -1, Session("tGR2N")(j), -1, Session("tGR3N")(j), -1,
        finam$, 1, True, True, Curnode.ID = "Group", 1, Session("tPLOT"), j, 1, Session("FirstYear"), Session("LastYear"),
        0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))

        End If
        Number = Session("tPLOT")(1, j)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("VarVal") = System.Math.Round(Number, 4) 'If more than 7- does e : 1.2209873245E-02 etc.
        'does not convert to val properly if more than 4 digits after decimal

        If cmbShiftYears.SelectedValue = "" Then 'Meant to be 0 years
            'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            VLine = Trim(Str(SliderVal)) & ":200"
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object SliderVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            VLine = Trim(Str(Session("VarVal"))) & "," & Trim(Str(SliderVal)) & "@" & Trim(cmbShiftYears.SelectedValue) & "," & Trim(Str(SliderVal)) & ":200"
        End If

        '         Trim(Str(cmbShiftYears.Text)) + "," + _
        '
        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Call ParseCustomStr(VLine, CShort(j), Session("SceLag"))


        Call GraphUpdate()


        Scenariotype = "HIGH"
        ValStr = ""

        'CONVERSION_ISSUE: Property 'MSComctlLib.NodeClass.ForeColor' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.MSComctlLib.NodeClass.ForeColor.html'.
        'JRSVBNET treeScenario.SelectedItem.ForeColor = System.Convert.ToUInt32(QBColor(HIGHCOLOR))

        cmbShiftYears.Enabled = True
        lblFullyCustomized.Visible = False
        lblCustomized.Visible = True

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'mti WebForm Changes From June 26, 2004
        Call SaveScenario(Session("ScenarioFileDir") & "Working.Sce", "Working.Sce")

        'mti August 2004
        cmbShiftYears.Enabled = True

    End Sub
    Sub SetSliderMaxMin() 'not being used
        txtMax.Text = CType(VMax(Session("SEL")), String).ToString
        txtMin.Text = CType(VMin(Session("SEL")), String).ToString
        txtSliderVal.Text = txtMin.Text
    End Sub


    Private Sub lstDIM1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDIM1.SelectedIndexChanged
        Dim old As Integer
        old = cmb_DIM1.SelectedIndex
        cmb_DIM1.SelectedIndex = lstDIM1.SelectedIndex + 1
        If old <> cmb_DIM1.SelectedIndex Then
            Call cmb_Dim1_SelectedIndexChanged(sender, e)
        End If
    End Sub


    Private Sub mnuRunScenario_Click()
        'BBH 7/18/04  V5_01 Begin

        Session("Reload") = "True"
        Dim msg As String
        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
        'If it doesn't exist
        If Not fs.FileExists(Session("ScenarioFileDir") & "Working.Sce") Then
            msg = "You have neither opened nor created a scenario. You need to do so"
            msg = msg + " before you can run the scenario."
            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If


        '  A scenario exists and will proceed to run it in QuickScenario mode
        Call SaveScenario(Session("ScenarioFileDir") + "Working.Sce", "Working.Sce")

        Session("QuickScenario") = True

        Session("Scenarioflag") = True
        Session("SensitivityFlag") = False
        'JRS 08/20/04 NOT THE CASE IN WEB VERSION NR = MaxVal(1) - 1 'Reset in case of Rebuilding base
        'JRS 07/24/02

        Dim InFile As String, OutFile As String
        'JRS 08/20/04 DIRECTLY GO msg = "Do you wish to process the parameters in the tree and proceed to running the scenario?"
        'JRS 08/20/04 If MsgBox(msg$, vbYesNo, "Run Scenario?") = vbYes Then
        If Session("HistoricBase") = True Then
            ' FileCopy PutFdrvRun$ + "IFsBaseHist.Run", PutFdrvRun$ + "working.run"
            If Session("mnuSQLite") = "True" Then
                InFile = Session("PutFdrvRun") + "IFsBaseHist.Run.db"
                OutFile = Session("PutFdrvRun") + "working.run.db"
            Else
                InFile = Session("PutFdrvRun") + "IFsBaseHist.Run"
                OutFile = Session("PutFdrvRun") + "working.run"
            End If
            'JRS 2006/07/21
            FarmFileAllocator("HIST", OutFile, Session("mnuSQLite"))
            FileCopyCheck(InFile, OutFile)
        Else
            'FileCopy PutFdrvRun$ + "IFsBase.Run", PutFdrvRun$ + "working.run"
            If Session("mnuSQLite") = "True" Then
                InFile = Session("PutFdrvRun") + "IFsBase.Run.db"
                OutFile = Session("PutFdrvRun") + "working.run.db"
            Else
                InFile = Session("PutFdrvRun") + "IFsBase.Run"
                OutFile = Session("PutFdrvRun") + "working.run"
            End If
            'JRS 2006/07/21
            FarmFileAllocator("BASE", OutFile, Session("mnuSQLite"))
            FileCopyCheck(InFile, OutFile)
        End If
        'jrs 2020/07/03 changed from .mdb to .db
        'JRS 10/16/02 SAM COPY
        If Session("mnuSQLite") = "True" Then
            InFile = PutDataFileDir$ + "SAMBase.db"
            OutFile = Session("PutFdrvRun") + "SAMWorking.db"
        Else
            InFile = PutDataFileDir$ + "SAMBase.mdb"
            OutFile = Session("PutFdrvRun") + "SAMWorking.mdb"
        End If
        'JRS 2006/07/21
        FarmFileAllocator("SAM", OutFile, Session("mnuSQLite"))
        FileCopyCheck(InFile, OutFile)

        'jrs 2008/06/18 no need to copy IFsPop(Hist).mdb because we start with the empty
        'version of IFsPopWork.mdb from DATA in ModelRun

        'jrs 2021/02/24 this section was missing
        'jrs 2017/01/06
        InFile$ = DataFileDir$ + "IFsForDyadic.db"
        If Dir(InFile$) > "" Then
            OutFile$ = Session("PutFdrvRun") + "IFsForDyadicWork.db"
            FileCopyCheck(InFile$, OutFile$)
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_run.aspx")
        'JRS 08/20/04 End If


        'JRSVBNET Scenarioflag = False

        'BBH 7/18/04  V5_01 End

    End Sub

    Private Sub ScenarioOpen(ByVal s As String, ByVal sDir As String)

        Dim DestinationFile$, SourceFile$

        If s = "" Then Exit Sub

        Dim sDirSess As String = "\Session"
        If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
            sDirSess = sDirSess & "0"
        End If
        sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sFileName As String
        'jrs 2005/04/26 sFileName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text) & "World Integrated Scenario Sets - NIC 2020\" & s & ".sce"
        'jrs 2006/11/02
        Dim Dir As String
        If sDir > "" Then
            Dir = sDir
        Else
            Dir = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text)
        End If
        sFileName = FindFile(Dir, s & ".sce", "sce")
        If sFileName Is Nothing Then Exit Sub

        Session("fpSelFilePath") = sFileName

        DestinationFile = Session("ScenarioFileDir") & "Working.Sce"
        SourceFile = Session("fpSelFilePath")
        FileCopyCheck(SourceFile, DestinationFile)
        'mti WebForm Changes From July 04, 2004
        Session("Open_File") = s
        'jrs 2013/09/11
        Session("ScenarioName") = s
        'jrs 2023/07/04
        Session("Saved_file") = ""


        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
    End Sub



    'jrs 2005/04/13
    Public Sub ScenarioAdd(ByVal s As String, ByVal d As String) 'jrs 2006/07/31 added D for Directory where to start looking
        Dim intfile As Short
        Dim intWfile As Short
        Dim intCfile As Short
        Dim intNfile As Short
        Dim RLine As String

        Dim DestinationFile$, SourceFile$

        If s = "" Then Exit Sub

        'jrs 2006/08/01
        'Dim sDirSess As String = "\Session"
        'If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
        '    sDirSess = sDirSess & "0"
        'End If
        'sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sFileName As String
        'sFileName = FindFile(Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text), s, "sce")
        sFileName = FindFile(d, s, "sce")
        'jrs 2005/04/15
        If sFileName Is Nothing Then Exit Sub

        Session("fpSelFilePath") = sFileName

        SourceFile = Session("fpSelFilePath")

        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(Session("ScenarioFileDir") & "Working.Add") <> "" Then
            Kill(Session("ScenarioFileDir") & "Working.Add")
        End If

        intfile = FreeFile()
        'temporary  file where working and the newly selected one are added
        FileOpen(intfile, Session("ScenarioFileDir") & "Working.add", OpenMode.Append)

        intWfile = FreeFile() 'working.sce
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
            FileOpen(intWfile, Session("ScenarioFileDir") & "Working.sce", OpenMode.Input)
        Else
            FileOpen(intWfile, Session("ScenarioFileDir") & "Working.sce", OpenMode.Append)
        End If

        intCfile = FreeFile()
        FileOpen(intCfile, SourceFile, OpenMode.Input) 'file selected from dialog for adding


        intNfile = FreeFile() 'working comment file
        FileOpen(intNfile, Session("ScenarioFileDir") & "Working.txt", OpenMode.Append)

        'Changes MTI 02 July,2002
        PrintLine(intNfile)
        PrintLine(intNfile, New String("=", 50))
        'mti WebForm Changes From July 04, 2004
        'PrintLine(intNfile, "Added Scenario: ")  'JRSVBNET & cmnDlg1.FileTitle & " on " & VB6.Format(Today, "dddd, mmm d yyyy") & " at " & VB6.Format(TimeOfDay, "hh:mm:ss AMPM"))
        PrintLine(intNfile, "Added Scenario: " & s & " on " & DateTime.Now.ToLocalTime)
        'mti WebForm Changes From July 04, 2004
        PrintLine(intNfile, New String("=", 50))
        'end Changes MTI 02 July,2002

        Do While Not EOF(intWfile)
            RLine = LineInput(intWfile)
            If UCase(Mid(RLine, 1, 7)) <> "COMMENT" Then
                PrintLine(intfile, RLine)
            Else
                'Comment, proceed on- working comment is already in working.txt
                Exit Do
            End If
        Loop

        Do While Not EOF(intCfile)
            RLine = LineInput(intCfile)
                'jrs 2024/02/27
                If RLine.Length > 7 AndAlso UCase(Mid(RLine, 1, 8)) = "BASEYEAR" Then
                    If UCase(Mid(RLine, 10, 4)) = FirstYearForecast.ToString Then
                        PrintLine(intfile, RLine)
                        RLine = LineInput(intCfile)
                    Else
                        FileClose(intfile)
                        FileClose(intWfile)
                        FileClose(intCfile)
                        FileClose(intNfile)

                        'Can't mix different baseyears
                        Dim msg = "Can't mix different base years."
                        frm_varname.Visible = False
                        LoadMessage(msg, Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If
                End If

            Select Case UCase(Mid(RLine, 1, 3)) ' <> "COMMENT" Then
                Case "LOW", "HIG", "MED", "CUS", "SLI"
                    PrintLine(intfile, RLine)
                Case "COM" 'Comment, write to working.txt
                    PrintLine(intNfile)
                    PrintLine(intNfile, New String("=", 40))
                    PrintLine(intNfile)
                    'mti September 2004 2
                    'If Not EOF(intCfile) Then
                    Do While Not EOF(intCfile) 'True
                        RLine = LineInput(intCfile)
                        PrintLine(intNfile, RLine)
                        'If EOF(intCfile) Then
                        '    Exit Do
                        'End If
                    Loop
                    'End If
                    Exit Do
                Case Else 'garbage written in file
                    'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

                    FileClose(intfile)
                    FileClose(intWfile)
                    FileClose(intCfile)
                    FileClose(intNfile)
                    'mti commented messagebox 
                    'WinFormsToWeb_MessageBox.Show("File Type Mismatch!", MsgBoxStyle.OKOnly, "IFS - Add Scenario")
                    Exit Sub
            End Select
        Loop

        'must close here - or might not be able to copy
        FileClose(intfile)
        FileClose(intWfile)
        FileClose(intCfile)
        FileClose(intNfile)

        FileCopy(Session("ScenarioFileDir") & "Working.add", Session("ScenarioFileDir") & "Working.sce")



        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
    End Sub

    'JRS 2005/04/25
    Private Sub updateTreeIndices(ByVal Nodes As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByVal DeleteTarget As String)
        If Not Nodes Is Nothing Then
            Dim index As Integer

            For index = 0 To Nodes.Count - 1
                If Val(Nodes(index).Target) > Val(DeleteTarget) Then
                    Nodes(index).Target = Trim(Str(CInt(Val(Nodes(index).Target) - 1)))
                End If
                If Not Nodes(index).Nodes Is Nothing Then
                    updateTreeIndices(Nodes(index).Nodes, DeleteTarget)
                End If
            Next
        End If
    End Sub

    'jrs 2005/05/09
    Private Sub LoadSceDesc(ByVal FileName As String)
        Dim Var As String
        Dim fNum2 As Integer
        fNum2 = FreeFile()
        'jrs 2005/05/09
        FileOpen(fNum2, Session("ScenarioFileDir") & "working.txt", OpenMode.Append)
        Var = BuildSceDesc(FileName, Session("SEL"), Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FdrvRun"), Session("PutFdrvRun"),
        Session("RunFiles"), Session("IFs_MDB2Active"), Session("FinYear"), Session("PercentOn"), Session("ActiveBase"),
        Session("bLoadForDisplaySuccess"), Me, Session("ShortSce"), Session("FileforDisplay"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"), Session("FinYearHis"),
        Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        PrintLine(fNum2, Var)
        FileClose(fNum2)
    End Sub

    'jrs 2005/05/24
    Private Sub cmdFileName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileName.Click
        Dim Message As String
        If txt_FileName.Text = "" Then
            Message = "Please type a File Name."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Dim sDirSess As String = "\Session"
        If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
            sDirSess = sDirSess & "0"
        End If
        sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sDirName As String
        sDirName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text) & "a User Defined Scenarios\"

        'jrs 2023/06/23
        If Not StandAloneMode Then
            'jrs 2005/06/08
            DeleteOldSceUsrDef(sDirName)

            'jrs 2005/06/14

            'jrs 2005/05/27
            Dim fs
            Dim size As Single
            fs = CreateObject("Scripting.FileSystemObject")
            size = fs.GetFile(Session("ScenarioFileDir") & "Working.sce").Size() + CheckDirSize(sDirName)

            If size > 10485760 Then '10 MB in bytes 
                Message = "There is not enough space to save this file, please contact your system administrator."
                LoadMessage(Message, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If


        'jrs 2005/06/13
        Try
            If txt_DirName.Text > "" Then
                sDirName = sDirName & txt_DirName.Text
                If Dir(sDirName & "\") = "" Then
                    MkDir(sDirName)
                End If
                sDirName = sDirName & "\"
            Else
                Message = "Please type a Folder Name."
                LoadMessage(Message, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

            FileCopy(Session("ScenarioFileDir") & "Working.sce", sDirName & txt_FileName.Text & ".sce")
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Scenario Development and Change" & ": " & txt_FileName.Text
            'jrs 2023/06/23
            Session("cur_file") = txt_FileName.Text
            Session("Open_File") = ""
            'jrs 2023/07/04
            'LoadMenu() doesn't refresh new menu trying to reload the form
            Session("Saved_file") = txt_FileName.Text
            'jrs 2024/08/19
            Session("fpSelFilePath") = sDirName & txt_FileName.Text & ".sce"

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
        Catch ThisExcep As System.IO.IOException
            Message = "Incorrect Folder or File Name."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End Try


        frm_FileName.Visible = False
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        'jrs 2005/05/24
        frm_FileName.Visible = False
    End Sub

    'jrs 2005/06/30
    Public Sub LoadSearch(ByVal idx As Integer)
        If idx > 0 Then
            'DoEvents()
            Session("SEL") = idx
            SaveVCat = VScenCat(idx)
            Session("SaveVCat") = SaveVCat

            Session("Curnode") = Support.TreeViewManager.SearchNode(treeScenario.Nodes, CType(Session("VCatDir"), Hashtable).Item(Trim(Str((SaveVCat \ 100) * 100))))
            'jrs 2021/03/01
            If Not (Session("Curnode") Is Nothing) Then
                CType(Session("Curnode"), Microsoft.Web.UI.WebControls.TreeNode).Expanded = True
                'Support.TreeViewManager.SetSelectedNode(treeScenario, Session("Curnode"))
                'jrs 2005/07/25 Session("Curnode") = Support.TreeViewManager.SearchNode(treeScenario.Nodes, CType(Session("VCatDir"), Hashtable).Item(SaveVCat.ToString))
                Session("Curnode") = Support.TreeViewManager.SearchNode(CType(Session("Curnode"), Microsoft.Web.UI.WebControls.TreeNode).Nodes, CType(Session("VCatDir"), Hashtable).Item(SaveVCat.ToString))
                Support.TreeViewManager.SetSelectedNode(treeScenario, Session("Curnode"))
            End If

            'Dim i As Integer
            'For i = 1 To treeScenario.Nodes.Count - 1
            '    If Val(Mid(treeScenario.Nodes(i).NodeData, 3)) = VScenCat(idx) Then
            '        treeScenario.SelectedNodeIndex = i
            '        '                Set SelNode = treeScenario.Nodes(i)
            '        '                Set Curnode = SelNode
            '        '                mnuDriver_Click
            '        '                Ind = idx
            '        '                lst_Varname_Click
            '        Exit For
            '    End If
            'Next
            mnuSelect_Click(Me, New EventArgs)
            'jrs 2023/11/16
            If Session("SubDimNum") = 0 Then
                Session("LoadSearchIdx") = Nothing
                treeScenario_NodeClick(Me, New TreeViewSelectEventArgs("0", CurNodeNo))
            End If
        End If
    End Sub

    Private Sub mnuExportWorkSce_Click()
        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()
        Response.Buffer = True
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=working.sce")
        Dim strURL As String = Session("ScenarioFileDir") & "Working.Sce"
        Response.WriteFile(strURL)
        Response.End()
    End Sub

    'jrs 2024/08/19
    Private Sub mnuExportIFsToStandCSV_Click()
        If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
            Dim FileToTransalte As String
            If Session("fpSelFilePath") > "" Then
                FileToTransalte = Session("fpSelFilePath")
            Else
                FileToTransalte = Session("ScenarioFileDir") & "Working.Sce"
            End If
            ExportScenarioToStandCSV(FileToTransalte)
        End If
    End Sub

    'jrs 2024/08/19
    Private Sub ExportScenarioToStandCSV(ByRef sFileName As String)
        Dim k As Integer
        Dim j As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        On Error GoTo er_Save
        Dim intfile As Short, outfile As Short
        Dim sFileNameTrans As String
        sFileNameTrans = sFileName.Replace(".sce", ".csv")
        Dim WriteStr As String
        intfile = FreeFile()
        FileOpen(intfile, sFileName, OpenMode.Input)
        outfile = FreeFile()
        FileOpen(outfile, sFileNameTrans, OpenMode.Output)

        WriteStr = "Parameter, CountryRegionA, CountryRegionB, Dimension1, Dimension2"
        For j = Session("FirstYear") To Session("LastYear")
            WriteStr = WriteStr & ", " & j.ToString
        Next
        PrintLine(outfile, WriteStr)

        'JRS 03/09/04
        Dim n As Microsoft.Web.UI.WebControls.TreeNode
        Dim NodeList As Microsoft.Web.UI.WebControls.TreeNodeCollection
        'THE INDEX INFORMATION IS STORED IN NODE DATA
        NodeList = TreeToList(treeScenario.Nodes)
        'JRS 2005/04/25 For Each n In NodeList
        'JRS 2005/04/25 j = TreeIndexToFlatIndex(treeScenario.Nodes, n.NodeData)
        For j = 1 To Support.TreeViewManager.CountNodes(treeScenario.Nodes)
            WriteStr = Session("LinePrefix")(j)
            If WriteStr <> "" Then 'write

                WriteStr = WriteStr.Replace("MEDIUM,", "")
                WriteStr = WriteStr.Replace("HIGH0000,", "")
                'jrs 2025/02/20
                WriteStr = WriteStr.Replace("HIGH,", "")
                'jrs 2025/02/20 WriteStr = WriteStr.Replace("LOW", "")
                WriteStr = WriteStr.Replace("LOW,", "")
                WriteStr = WriteStr.Replace("SLIDER,", "")
                WriteStr = WriteStr.Replace("CUSTOM,", "")
                'jrs 2025/02/20 
                If WriteStr.Contains("SLIDER0") Then
                    'Remove all variations of Slider
                    Dim pos As Integer
                    pos = WriteStr.IndexOf(",") + 1
                    WriteStr = WriteStr.Substring(pos)
                End If
                If WriteStr.Contains("HIGH0") Then
                    'Remove all variations of Slider
                    Dim pos As Integer
                    pos = WriteStr.IndexOf(",") + 1
                    WriteStr = WriteStr.Substring(pos)
                End If

                k = Session("Node2Plot")(j)

                chk = IfsVars.Item(Session("tVARN")(1, k)).Index
                Session("SEL") = chk

                If VDim1(chk) = 0 Then
                    WriteStr = WriteStr & ", null, null, null, null"
                ElseIf VDim2(chk) = 0 Then
                    If VDim1(chk) = 1 Then
                        'Check for commas in name
                        Dim x As String() = Split(WriteStr, ",")
                        If x.Length > 2 Then
                            WriteStr = WriteStr.Replace(x(1) & "," & x(2), x(1) & ";" & x(2))
                        End If
                        WriteStr = WriteStr & ", null, null, null"
                    Else
                        Dim x As String() = Split(WriteStr, ",")
                        If IsNumeric(x(1)) Then
                            WriteStr = WriteStr.Replace(CStr(x(1)), " null, null, " & GRPS(VDim1(chk), x(1))).Replace(",", ";") & ", null"
                        End If
                    End If
                ElseIf VDim3(chk) = 0 Then
                    If VDim1(chk) = 1 And VDim2(chk) = 1 Then
                        Dim x As String() = Split(WriteStr, ",")
                        If x.Length > 3 Then
                            WriteStr = WriteStr.Replace(x(1) & "," & x(2), x(1) & ";" & x(2))
                        End If
                        If IsNumeric(x(2)) Then
                            WriteStr = WriteStr.Replace(CStr(x(2)), GRPS(VDim2(chk), x(2)).Replace(",", ";"))
                        End If
                        WriteStr = WriteStr & ", null, null"
                    Else
                        If VDim1(chk) = 1 Then
                            Dim x As String() = Split(WriteStr, ",")
                            If IsNumeric(x(2)) Then
                                WriteStr = WriteStr.Replace(CStr(x(2)), " null, " & GRPS(VDim2(chk), x(2)).Replace(",", ";"))
                            Else
                                WriteStr = WriteStr.Replace(CStr(x(2)), " null, " & CStr(x(2)))
                                If x.Length > 3 Then
                                    WriteStr = WriteStr.Replace(x(2) & "," & x(3), x(2) & ";" & x(3))
                                End If
                            End If
                            WriteStr = WriteStr & ", null"
                        Else
                            Dim x As String() = Split(WriteStr, ",")
                            If IsNumeric(x(1)) Then
                                WriteStr = WriteStr.Replace(CStr(x(1)), " null, null, " & GRPS(VDim1(chk), x(1)).Replace(",", ";"))
                            End If
                            If IsNumeric(x(2)) Then
                                WriteStr = WriteStr.Replace(CStr(x(2)), GRPS(VDim2(chk), x(2)).Replace(",", ";"))
                            End If
                        End If
                    End If
                Else
                    Dim x As String() = Split(WriteStr, ",")
                    If x.Length > 4 Then
                        WriteStr = WriteStr.Replace(x(1) & "," & x(2), x(1) & ";" & x(2))
                        x = Split(WriteStr, ",")
                    End If
                    If IsNumeric(x(1)) Then
                        WriteStr = WriteStr.Replace(CStr(x(1)), GRPS(VDim1(chk), x(1)).Replace(",", ";"))
                    End If
                    If VDim2(chk) > 1 Then
                        WriteStr = WriteStr.Replace(CStr(x(2)), " null, " & x(2))
                    Else
                        WriteStr = WriteStr & ", null"
                    End If
                    If IsNumeric(x(2)) Then
                        WriteStr = WriteStr.Replace(CStr(x(2)), GRPS(VDim2(chk), x(2)).Replace(",", ";"))
                    End If
                    If IsNumeric(x(3)) Then
                        WriteStr = WriteStr.Replace(CStr(x(3)), GRPS(VDim3(chk), x(3)))
                    End If
                End If

                WriteStr = WriteStr & "," & BuildCustomStr(k)
                PrintLine(outfile, WriteStr)
            End If
        Next j 'JRS 2005/04/25 n

        'write nonScenario
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        For i2 = 0 To lst_nonscenario.Items.Count - 1
            PrintLine(intfile, "CUSTOM," & lst_nonscenario.Items(i2).Text)
        Next i2

        FileClose(intfile)
        FileClose(outfile)

        'Reload the tree so that the menus are updated
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")

        Exit Sub
er_Save:
        'JRS 2/11/03
        FileClose(intfile)
        FileClose(outfile)
    End Sub

    'jrs 2024/08/21
    Private Sub ImportCSVtoSCE(ByVal s As String, ByVal sDir As String)
        Dim k As Integer
        Dim j As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        On Error GoTo er_Save

        If s = "" Then Exit Sub

        Dim sDirSess As String = "\Session"
        If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
            sDirSess = sDirSess & "0"
        End If
        sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sFileName As String
        Dim Dir As String
        If sDir > "" Then
            Dir = sDir
        Else
            Dir = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text)
        End If

        Dim intfile As Short, outfile As Short
        Dim sFileNameTrans As String
        sFileName = FindFile(Dir, s & ".csv", "csv")
        sFileNameTrans = sFileName.Replace(".csv", ".sce")
        Dim WriteStr As String
        intfile = FreeFile()
        FileOpen(intfile, sFileName, OpenMode.Input)
        outfile = FreeFile()
        FileOpen(outfile, sFileNameTrans, OpenMode.Output)

        Dim Var As String = ""
        Dim Actor As String = ""
        Dim Partner As String = ""
        Dim Dim2 As String = ""
        Dim Dim3 As String = ""
        Dim sLine As String = ""
        'reading headers
        sLine = LineInput(intfile)

        Do While Not EOF(intfile)
            Input(intfile, Var)
            If Var <> "" Then 'write
                WriteStr = "CUSTOM," & Var
                chk = IfsVars.Item(Var).Index
                If chk < 0 Then
                    GoTo er_Save
                End If

                Session("SEL") = chk
                'Actor
                Input(intfile, Actor)
                Actor = Actor.Replace(";", ",")
                'partner
                Input(intfile, Partner)
                Partner = Partner.Replace(";", ",")
                'Dim2
                Input(intfile, Dim2)
                'Dim3
                Input(intfile, Dim3)
                'Rest of the line
                sLine = LineInput(intfile)

                If VDim1(chk) = 0 Then
                    WriteStr = WriteStr & "," & sLine
                ElseIf VDim2(chk) = 0 Then
                    If VDim1(chk) = 1 Then
                        WriteStr = WriteStr & "," & Actor & "," & sLine
                    Else
                        WriteStr = WriteStr & "," & Dim2 & "," & sLine
                    End If
                ElseIf VDim3(chk) = 0 Then
                    If VDim1(chk) = 1 And VDim2(chk) = 1 Then
                        WriteStr = WriteStr & "," & Actor & "," & Partner & "," & sLine
                    Else
                        If VDim1(chk) = 1 Then
                            WriteStr = WriteStr & "," & Actor & "," & Dim2 & "," & sLine
                        Else
                            WriteStr = WriteStr & "," & Dim2 & "," & Dim3 & "," & sLine
                        End If
                    End If
                Else
                    If VDim2(chk) = 1 Then
                        WriteStr = WriteStr & "," & Actor & "," & Partner & "," & Dim2 & "," & sLine
                    Else
                        WriteStr = WriteStr & "," & Actor & "," & Dim2 & "," & Dim3 & "," & sLine
                    End If
                End If

                PrintLine(outfile, WriteStr)
            End If
        Loop

        FileClose(intfile)
        FileClose(outfile)

        'Copy the new sce file into working
        FileCopy(sFileNameTrans, Session("ScenarioFileDir") & "Working.Sce")

        Session("Open_File") = s
        Session("ScenarioName") = s
        Session("Saved_file") = ""

        'Reload the form to load the newly imported file
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")

        Exit Sub
er_Save:
        'JRS 2/11/03
        FileClose(intfile)
        FileClose(outfile)

        'Send message about the error
        'Response.Write("<script>alert('There is a problem with the format of this file and it couldn't be imported.');</script>")
        LoadMessage("There is a problem with the format of this file and it couldn't be imported.", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    'jrs 2025/05/20
    Private Sub DeleteRowFromWorkingSCE(sDelCriteria As String)
        'Open File
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Input)
        Dim sline As String
        Dim sLines() As String, sComments() As String
        Dim length As Integer = 0, lengthC As Integer = 0
        'Load all lines exept Deletion Criteria
        Do While Not EOF(fNum)
            sline = LineInput(fNum)
            If Not sline.Contains(sDelCriteria) Then
                ReDim Preserve sLines(length)
                sLines(length) = sline
                length = length + 1
            End If
        Loop
        FileClose(fNum)
        'Rewrite the file with te loaded lines
        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Sce", OpenMode.Output)
        For i = 0 To length - 1
            PrintLine(fNum, sLines(i))
        Next
        FileClose(fNum)
    End Sub

End Class
