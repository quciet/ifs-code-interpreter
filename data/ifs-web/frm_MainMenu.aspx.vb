'===========================================================================
' This file was modified as part of an ASP.NET 2.0 Web project conversion.
' The class name was changed and the class modified to inherit from the abstract base class 
' in file 'App_Code\Migrated\Stub_frm_MainMenu_aspx_vb.vb'.
' During runtime, this allows other classes in your web application to bind and access 
' the code-behind page using the abstract base class.
' The associated content page 'frm_MainMenu.aspx' was also modified to refer to the new class name.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
Option Strict Off
Option Explicit On
Imports System.Data.SQLite
Imports System.Globalization
Imports System.Media
Imports System.Security.Policy
Imports System.Web.UI.WebControls.Expressions
Imports System.Xml
Imports EO.Web.Internal
Imports IFs.Data
Imports Microsoft.Ajax.Utilities
'JAM 2020-08-12
Imports Microsoft.AspNet.FriendlyUrls
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic


Partial Class Migrated_frm_MainMenu
    Inherits frm_MainMenu
    Dim tmpCname As String

#Region " Web Form Designer Generated Code "

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

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

            'JAM 2020/05/19 
            If Session("mnuSQLite") = Nothing Then
                'JAM 2020-05-05
                If StandAloneMode Then
                    Session("mnuSQLite") = "True"
                Else
                    Session("mnuSQLite") = "False"
                End If
            End If

            'JAM 2022/07/02
            If Session("mnuTabs") = Nothing Then

                'If StandAloneMode Then
                '    Session("mnuTabs") = "True"
                'Else
                Session("mnuTabs") = False 'jam 2022/07/04
                'End If
            End If



            'JAM 2020-04-13
            If Session("SQLiteDB") = Nothing Then
                Session("SQLiteDB") = "ifsPopWork.db"

            End If

            'JRS 02/11/04
            PrepareForm()
            'JRS 03/25/04
            Session("DisplayParent") = "frm_MainMenu.aspx"
            'jrs 09/15/04
            Session("RefreshGraph") = "true"
            'jrs 09/29/04
            Session("SAMBasePct") = False
            'jrs 09/30/04
            Session("mapParent") = "frm_MainMenu.aspx"
            'jrs 2005/03/15
            Session("ReportFileIndex") = Nothing
            'jrs 2005/07/08
            Session("RunParent") = "frm_MainMenu.aspx"
            'jrs 2011/07/26 going from Flex Displays to Country Profile was showing the same title
            Session("TableTitle") = ""
            'jrs 2012/01/06 if you enter from Ctry Prof then add a pre-run file when you exit the success msg you go back to Ctry Prof
            Session("MsgParent") = Nothing
            'jrs 2014/02/24
            Session.Remove("EdCohort_GraphType")
            Session.Remove("EC_TempPopDatabase")
            Session.Remove("EC_TempDatabase")
            Session.Remove("EC_TempPopDatabase_Comp")
            Session.Remove("EC_TempDatabase_Comp")
            Session.Remove("PopCohort_GraphType")
            Session.Remove("PC_TempDataBase")
            Session.Remove("POPM")
            Session.Remove("POPF")
            Session.Remove("POPMales")
            Session.Remove("POPFemales")
            Session.Remove("bCompare")
            'jrs 2015/03/04
            Session.Remove("YAxisLabel")
            Session.Remove("XAxisLabel")
            'JRS 2015/09/16
            Session.Remove("pCOUNTE")
            'jrs 2015/10/21
            Session.Remove("mnuFixCtSc")
            Session.Remove("mnuFixCteScTime")
            Session.Remove("mnuFixCtScMan")
            'jrs 2016/03/21
            Session.Remove("mnu2005PPP")
            Session.Remove("RegIdxs")
            'jrs 2016/03/28
            Session.Remove("bPctTot")
            'jrs 2016/03/30
            Session.Remove("bUseStartYrOrg")
            'jrs 2016/04/05
            Session.Remove("bComparisonOn")
            Session.Remove("sComparison")
            'jrs 2017/06/09
            Session.Remove("SDGsetGroup")
            Session.Remove("VarsInGrid")
            'jrs 2024/11/01 this was producing that labels were not cleared if you go to self, Qatar, Longitudinal, Qatar, Self Pakistan
            Session.Remove("bVARN0Loaded")
            Session.Remove("oGR1N") 'this one is needed to avoid reloading in Display Menu

            'jrs 2005/11/14
            Dim BASE
            Dim fs
            fs = CreateObject("Scripting.FileSystemObject")
            If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
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
            Session("BaseSize") = BASE.size
        End If
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " " & ModelVersion 'jrs 2011/04/29 "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
        'lblTitle.Text = lblTitle.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
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
    'CONVERSION_NOTE: Class ''JRSVBNET System.Windows.Forms.ToolTip' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public ToolTip1 As 'JRSVBNET System.Windows.Forms.ToolTip 
    'CONVERSION_NOTE: Class 'AxMSComDlg.AxCommonDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents 'JRSVBNET CMDialog1 As AxMSComDlg.AxCommonDialog 
    ''mti webforms June 2004 Mainmenu
    'Public ExitModel As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public Report As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public CohortsofPopulation As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public CohortsofWorldValueSurvey As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuWorldVariables As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public SocialAccountingMatrix As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public WorldBankFinancialFlows As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public AdvancedSustainabilityAnalysis As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public SpecializedDisplays As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public SelfManagedDisplay As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public Display As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public GuidedUse As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public SelfManagedScenarios As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public Open As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public SAVEAS As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public DELETE As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public SELECT_Renamed As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuFileManagement As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public ScenarioAnalysis As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public CountryMap As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public CountryMap2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public AnalyzeAcrossCountries As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public ShowFunctions As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public AnalyzeLongitudinal As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public CountryData As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public EditCountryData As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public DisplayProcessedCountryData As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public ImportDatafromXLSFile As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuEarliestData As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuMostRecentData As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public EliminateYearsData As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public IdentifySaveYears As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public AddDeleteCountries As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public HistoricDataFileProcessing As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public ManageCountryData As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public RegionMembers As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public ChangeRegionMembership As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public DisplayRegionallyAggregatedData As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public ManageRegionalization As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public RebuildBase As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public RebuildHistoricBase As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public RebuildModelBase As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuVariable As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public AddorDeleteVariableandRebuild As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public RebuildWithoutVariableChange As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public RebuildCommon As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuDocumentation As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public mnuModelIntegration As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public Register As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public ModelDevelopment As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public Contents As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public AboutThisSoftware As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'Public Help As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'CONVERSION_NOTE: Class 'IFsGUI.IFsGUIctrl' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Friend WithEvents IFsGUIctrl1 As IFsGUI.IFsGUIctrl 
    'end 'mti webforms June 2004 Mainmenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_MainMenuPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_MainMenu
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6Form.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_MainMenu()
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
    '------------------------------------------MainMenu.Frm------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last changed by BBH on August 8, 2002


    'Commented Code From VB 3.0
    'Private Declare Function WinHelp Lib "USER" _
    '(ByVal hWnd As Integer, _
    'ByVal lpzFileName As String, _
    'ByVal WCmd As Integer, _
    'dwData As Any) As Integer

    'CONVERSION_TODO: The following code was converted according to the value of the conditional statement.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1024.html'.
    'JRS 02/05/04 #If Win32 Then ' 32-bit VB uses this Declare
    'Private Declare Function WinHelp Lib "user32" Alias "WinHelpA" (ByVal HWND As Integer, ByVal lpHelpFile As String, ByVal wCommand As Integer, ByVal dwData As Integer) As Integer
    'JRS 02/05/04 #Else '16-bit VB uses this Declare
    'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Else did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1035"'
    'mti webforms June 2004 LongPlot - comment declaration
    'Private Declare Function WinHelp Lib "User.dll" _
    '        (ByVal HWND As Integer, _
    '        ByVal lpHelpFile As String, _
    '        ByVal wCommand As Integer, _
    '        ByVal dwData As Any) As Integer
    'JRS 02/05/04 #End If

    'Driver Code Changes June 2003
    ''mti webforms June 2004 Mainmenu
    'Dim LeftSide(5000) As String 'Left side of an eqn
    'Dim Rightside(5000) As String 'Right Side
    'Dim CodeBlock(5000) As Object 'blocks obtained by parsing modules
    'Dim ModuleName(5000) As Object 'keeps track of module name for each block
    'Dim CodeArray(3000) As String
    'end 'mti webforms June 2004 Mainmenu

    'this is the most important array saving relevant codeblock() pointers
    'this is sorted while being taken, so that vbcode is in proper order
    ''mti webforms June 2004 Mainmenu
    'Dim CodeArray2Block(3000) As Short
    'Dim VCLn, UVCLn As Short 'VCLn keeps track of codearray() upper limit
    'Dim ModName(20) As Object
    'Dim ModStart(20) As Object
    'Dim ModEnd(20) As Object
    'end 'mti webforms June 2004 Mainmenu
    'As the equations for a IFSVar variable(in the Left Hand Side) is sought
    'CodeArray(1), CodeArray(2) etc. are filled with Codeblock() populated from ParseCodeFile
    'VCLn is the upper limit of CodeArray when the search ends
    'Driver Code Changes June 2003 - End


    'Dim LeftSide(5000) As String
    'Dim Rightside(5000) As String
    'Dim CodeBlock(5000)
    ''mti 07 Feb 03
    'Dim ModuleName(5000)
    ''mti 07 Feb 03
    'Dim CodeArray(3000, 1) As String
    'Dim VCLn%, UVCLn%
    ''mti webforms June 2004 Mainmenu
    'Dim codelines As Short
    'Dim vbKW() As String ' vb Key Words
    'Public vbKWCount As Integer
    'Dim Tempright(3000, 1) As Object
    ' end 'mti webforms June 2004 Mainmenu
    Dim valtbl As DataTable
    Dim IY As Short
    Private lstPts As New ArrayList
    Private lstEvts As New ArrayList



    '=============================MainMenu.Frm===============================
    Private Sub AnalyzeCountry_Click()
        Dim frm_ScatVarS As Object
        Dim ScatGraphTyp As Object
        Dim ScatGraphStyl As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphStyl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ScatGraphStyl = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphTyp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ScatGraphTyp = 9
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphStyl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphStyle") = ScatGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphTyp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = ScatGraphTyp
        'UPGRADE_WARNING: Couldn't resolve default property of object frm_ScatVarS.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        frm_ScatVarS.Show()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub AboutThisSoftware_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AboutThisSoftware.Popup
    'JRSVBNET AboutThisSoftware_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub AboutThisSoftware_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.

        'mti August 2004 5
        Session("AboutParent") = "frm_MainMenu.aspx"
        'mti August 2004 5 -end 

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "AboutModel.aspx")
        'frm_AboutModel.Show() 

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub AddDeleteCountries_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AddDeleteCountries.Popup
    'JRSVBNET AddDeleteCountries_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub AddDeleteCountries_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRS 8/7/01 GUI frm_AddDelRawD.Show
        'JRSVBNET IFsGUIctrl1.AddDelRawD()

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub AddorDeleteVariableandRebuild_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AddorDeleteVariableandRebuild.Popup
    'JRSVBNET AddorDeleteVariableandRebuild_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub AddorDeleteVariableandRebuild_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If CBool(DevelopmentMode) = True Then 'Form Load should prevent ever
            'reaching this sub unless Mode is True; redundant protection
            'JRSVBNET frm_AddDelIFsVar.Show()
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Carret = "<br>" & Chr(10)
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            carret2 = Carret + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = "Rebuilding the common block requires that you have both" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & " Visual Basic and the source code of the model.  This " + Carret
            msg = msg & " option is therefore available "
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "only in model development mode.  " + Carret
            msg = msg & "This copy of the model is not licensed for development mode."

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub AnalFunc_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRSVBNET frm_AnalFunc.Show()
    End Sub

    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub AdvancedSustainabilityAnalysis_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AdvancedSustainabilityAnalysis.Popup
    'JRSVBNET AdvancedSustainabilityAnalysis_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub AdvancedSustainabilityAnalysis_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET IFsGUIctrl1.ASA()
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub AnalyzeAcrossCountries_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AnalyzeAcrossCountries.Popup
    'JRSVBNET AnalyzeAcrossCountries_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub AnalyzeAcrossCountries_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRS 8/7/01 GUI ScatGraphStyl = 0
        'JRS 8/7/01 GUI ScatGraphTyp = 9
        'JRS 8/7/01 GUI Session("GraphStyle") = ScatGraphStyl
        'JRS 8/7/01 GUI Session("GraphType") = ScatGraphTyp
        'JRS 8/7/01 GUI frm_ScatVarS.Show
        'JRSVBNET IFsGUIctrl1.ScatVarS()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

        'mti WebForm Changes to be taken by Jose - start
        'mtiwebform June 2004 Cross-Sectional


        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "ScatVars.aspx")
        'end mtiwebform June 2004 Cross-Sectional
        'mti WebForm Changes to be taken by Jose - end 

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub AnalyzeLongitudinal_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AnalyzeLongitudinal.Popup
    'JRSVBNET AnalyzeLongitudinal_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub AnalyzeLongitudinal_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRS 8/7/01 GUI ScatGraphStyl = 0
        'JRS 8/7/01 GUI ScatGraphTyp = 9
        'JRS 8/7/01 GUI Session("GraphStyle") = ScatGraphStyl
        'JRS 8/7/01 GUI Session("GraphType") = ScatGraphTyp
        'JRS 8/7/01 GUI frm_LongVarS.Show
        'JRSVBNET IFsGUIctrl1.LongVarS()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

        'mti WebForm Changes to be taken by Jose - start
        'mtiwebform June 2004 Longitudinal
        Session("LV_Chk_Indep") = 1
        Session("LV_Chk_Time") = 0
        Session("LV_Chk_Points") = 0
        Session("LV_lbl_Dep") = "None"

        'mti August 2004 4
        Session("LV_lbl_Ind") = "None"
        'mti August 2004 4

        Session("CountryTot") = 0
        Session("LV_lbl_CountSel") = "No"

        Session("VarNameInd") = "TIME"
        Session("VarNameDep") = ""
        Session("VarNameCon") = ""

        Session("MaxDepIndYears") = 500
        ReDim Session("DepVarYears")(Session("MaxDepIndYears"))
        ReDim Session("IndVarYears")(Session("MaxDepIndYears"))
        ReDim Session("ScatVarYears")(Session("MaxDepIndYears"))

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "LongVarS.aspx")
        'end mtiwebform June 2004 Longitudinal
        'mti WebForm Changes to be taken by Jose - end 

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub Change_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '    frm_Change.Show
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub ChangeRegionMembership_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChangeRegionMembership.Popup
    'JRSVBNET ChangeRegionMembership_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub ChangeRegionMembership_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET 'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Carret = "<br>" & Chr(10)
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        carret2 = Carret + Carret
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        msg = "The Professional Features allow you to:" + Carret
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        msg = msg & "   1. Change the data base of the model" + Carret
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        msg = msg & "   2. Change the regionalization of the model" + Carret
        msg = msg & "   3. Add/remove variables to the model" & carret2
        '    msg$ = msg$ + "Professional Features require Microsoft Access." + carret2
        msg = msg & "This copy of the model does not have Professional Features."
        '    MsgBox msg$

        'JRS 8/7/01 GUI frm_ChgRegMb.Show
        'JRSVBNET 'JRSVBNET IFsGUIctrl1.ChgRegMb()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET 'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub
    '=============================MainMenu.Frm===============================

    Public Overrides Sub CohortsofPopulation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2011/05/04
        Dim sPar As String
        If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
            Session("MMCountry") = "AF"
        End If
        If Session("MMCountry") = Nothing Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If

        'JRS 8/7/01 GUI frm_TablFunc2.Show
        'mtiwebforms April 2004 Population Cohort
        Support.FormManager.StateStore(Me)
        'jrs 2012/05/04 added Year
        Support.FormManager.Show(Me, "frm_PopCohortDisp.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub CohortsofWorldValueSurvey_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CohortsofWorldValueSurvey.Popup
    'JRSVBNET CohortsofWorldValueSurvey_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub CohortsofWorldValueSurvey_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2011/05/04
        Dim sPar As String
        If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
            Session("MMCountry") = "AF"
        End If
        If Session("MMCountry") = Nothing Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If

        'JRSVBNET IFsGUIctrl1.WorldValueSurveyCohorts()
        'mtiwebformsMarch2004 (2)ScenarioCreate
        Session("GraphType") = 3 'BarGraphTyp
        Session("GType") = "COHORT"
        Session("PYear") = 1
        Session("TableTitle") = "" 'txt_TableTitle

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_WVSDisplay.aspx?" & PrepareForURL(sPar))
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub Contents_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Contents.Popup
    'JRSVBNET Contents_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub Contents_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Dummy As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'JRSVBNET Dummy = WinHelp(Handle.ToInt32, App.HelpFile, HELP_FINDER, CInt(0))

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub CountryMap_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CountryMap.Popup
    'JRSVBNET     CountryMap_Click(eventSender, eventArgs)
    'JRSVBNET End Sub

    Public Overrides Sub CountryMap_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Path to ESRI MapObjects

        'JRS 8/16/01 GUI RecordSourceDef$ = "Social"
        'JRS 8/16/01 GUI DataFieldDef$ = "Freedom"
        'JRS 8/16/01 GUI frm_Map.Show

    End Sub

    Private Sub DataAnalysis_Click()

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub CountryMap2_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CountryMap2.Popup
    'JRSVBNET CountryMap2_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub CountryMap2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Path to Blue Marble GeoObjects Map
        'jrs 11/11/04
        Session("MapChosenTable") = "TimeSeries"
        Session("RecordSourceDef") = "Social"
        'JRS 8/7/01 GUI DataFieldDef$ = "Freedom"
        'JRS 8/7/01 GUI frm_Map2.Show
        'JRSVBNET IFsGUIctrl1.Map2()
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_mapViewer.aspx")

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub Delete_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles DELETE.Popup
    'JRSVBNET Delete_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub Delete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        FileClose()
        'JRSVBNET CMDialog1.Filename = ""

        '   CancelError is True
        '   -------------------
        On Error GoTo ErrHandler6

        'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        'JRSVBNET CMDialog1.DialogTitle = "DeleteRUN"
        'JRSVBNET CMDialog1.filter = "Run Files (*.run)|*.run"
        'JRSVBNET CMDialog1.Action = 1
        'JRSVBNET DeleteFile(('JRSVBNET CMDialog1.Filename))

ErrHandler6:  ' User Pressed Cancel
        Exit Sub


    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub DeleteSRU_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        FileClose()
        'JRSVBNET CMDialog1.Filename = ""

        '   CancelError is True
        '   -------------------
        On Error GoTo ErrHandler7

        'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        'JRSVBNET CMDialog1.DialogTitle = "DeleteSRU"
        'JRSVBNET CMDialog1.filter = "Run Files (*.sru)|*.sru"
        'JRSVBNET CMDialog1.Action = 1
        'JRSVBNET DeleteFile(('JRSVBNET CMDialog1.Filename))

ErrHandler7:  ' User Pressed Cancel
        Exit Sub

    End Sub
    '=============================MainMenu.Frm===============================
    Private Sub DeleteFile(ByRef Filename As String)
        Dim msg As String
        Dim j As Object
        Dim Topf As Short
        Dim ICount As Short
        Dim i2 As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If InStr(StrConv(Filename, VbStrConv.Uppercase), "IFSBASE.RUN") > 0 Then GoTo ProtectBase
        If InStr(StrConv(Filename, VbStrConv.Uppercase), "IFSBASE.RUN.DB") > 0 Then GoTo ProtectBase
        Kill(Filename)

        'jrs 2009/06/30
        Dim sName As String
        Dim X As String()
        X = Split(Filename$, "\")
        Dim iSize As Integer
        iSize = UBound(X)
        sName = X(iSize)
        sName = Replace(UCase(sName), ".RUN.DB", "")
        Dim PathToDelete As String
        'Ignore error if there's no file
        On Error Resume Next
        'SAM
        PathToDelete = Replace(UCase(Filename$), sName & ".RUN.DB", "SAM" & sName & ".DB")
        Kill(PathToDelete)
        'IFsPop
        PathToDelete = Replace(UCase(Filename$), sName & ".RUN.DB", "IFSPOP" & sName & ".DB")
        Kill(PathToDelete)
        'jrs 2018/08/14
        'IFsForDyadic
        PathToDelete = Replace(UCase(Filename$), sName & ".RUN.DB", "IFSFORDYADIC" & sName & ".DB")
        If Dir(PathToDelete) > "" Then
            Kill(PathToDelete)
        End If

        'Restore ignoring error handler
        On Error GoTo 0

        '   Eliminate variables for display because file name number and sequence change
        '   ----------------------------------------------------------------------------
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For i2 = 1 To MaxVariables
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VARN")(1, i2) = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VARN")(2, i2) = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VARN")(3, i2) = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("GR1N")(i2) = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("GR2N")(i2) = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("GR3N")(i2) = ""
        Next i2
        Session("COUNTE") = 0
        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For ICount = 1 To MaxVariables
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("PLOT")(i2, ICount) = 0
            Next ICount
        Next i2

        Session("PercentOn") = "False"

        '   Shift Files Selected down 1 as necessary
        '   ----------------------------------------

        If Session("FilesSelected") > 1 Then
            Topf = Session("FilesSelected")
            For j = Topf To 2 Step -1
                'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.cRunFiles(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Filename = Session("RunFiles")(j) Then
                    Session("FilesSelected") = Session("FilesSelected") - 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If j < Topf Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        For i2 = j To Topf - 1
                            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.cRunFiles(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Session("RunFiles")(i2) = Session("RunFiles")(i2 + 1)
                        Next i2
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.cRunFiles(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("RunFiles")(Topf) = ""
                    End If
                End If
            Next j
        End If

        msg = "File successfully deleted: " & sName

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


        Exit Sub

ProtectBase:  ' Protect IFsBase
        msg = "You may not delete IFsBase"

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub Display_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Display.Popup
    'JRSVBNET Display_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub Display_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRS 8/5/01 GUI frm_DisplayMenu.Show
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_DisplayMenu.aspx")

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub EditData_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '    frm_EditData.Show

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub DisplayProcessedCountryData_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles DisplayProcessedCountryData.Popup
    'JRSVBNET DisplayProcessedCountryData_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub DisplayProcessedCountryData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRS 8/7/01 GUI frm_DispProC.Show
        'JRSVBNET IFsGUIctrl1.DispProcC()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub DisplayRegionallyAggregatedData_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles DisplayRegionallyAggregatedData.Popup
    'JRSVBNET DisplayRegionallyAggregatedData_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub DisplayRegionallyAggregatedData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRS 8/7/01 GUI frm_DispProR.Show
        'JRSVBNET IFsGUIctrl1.DisProR()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub DisplaySelfManaged_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_DisplayMenu.aspx")

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub EditCountryData_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles EditCountryData.Popup
    'JRSVBNET EditCountryData_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub EditCountryData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRS 8/7/01 GUI frm_EditRawD.Show
        'JRSVBNET IFsGUIctrl1.EditRawD()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub EliminateYearsData_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles EliminateYearsData.Popup
    'JRSVBNET EliminateYearsData_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub EliminateYearsData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRS 8/7/01 GUI frm_ProcDelColYr.Show
        'JRSVBNET IFsGUIctrl1.ProDelCoYear()

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub End_Click()
        Dim response As Object
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        msg = "Do you wish to exit?"
        'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET response = WinFormsToWeb_MessageBox.Show(msg, 1)
        'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If response = 1 Then response.End()

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub ExitModel_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ExitModel.Popup
    'JRSVBNET ExitModel_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub ExitModel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        msg = "Do you wish to exit?"
        'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 2008/07/29 WinFormsToWeb_MessageBox.Show(msg, MsgBoxStyle.YesNo)
        'UPGRADE_WARNING: Couldn't resolve default property of object response. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If MsgBoxResult.Yes Then Response.End()

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub Flexible_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

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

        Session("COUNTE") = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object LongFileOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("LongFileOn") = True
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "CHANGE.aspx")
        'frm_change.Show() 
        'Modal
        'JRS 8/30/01 GUI 'JRSVBNET IFsGUIctrl1.ChangeFlexible
    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub frm_MainMenu_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.GeneratedPrefix = "cswmWebMenu"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Exit Model"
        cswmGroup1.ID = "Group_e0e1"

        Dim cswmItem As Coalesys.WebMenu.Item
        'jrs 2024/08/15 enabeld again
        'jrs 2013/10/02 
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Exit"
        'cswmItem.OnClick = "window.close();"
        'jrs 2014/03/25 cswmGroup1.OnMenuBarClick = "location.href='Index.aspx';"
        'jrs 2024/08/15 cswmGroup1.OnMenuBarClick = "location.href='http://pardee.du.edu/access-ifs';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display"
        cswmGroup1.ID = "Group_812c"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Flexible Displays"
        cswmItem.OnClick = "showhg();"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Geographically-fixed Displays"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmItem = cswmGroup1.Items.Add()
        'jrs 2023/02/15
        'cswmItem.Enabled = False
        cswmItem.Caption = "Basic Report for Countries/Regions or Groupings"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Specialized Displays for Issues"

        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "POPULATION"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Population by Age and Sex"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2012/06/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Birth Cohort Information"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2022/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "EDUCATION"

        'jrs 2005/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Education by Age, Sex and Level"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2012/07/06
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Cost of Education Goals"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2022/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "HEALTH"

        'jrs 2008/09/16
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Mortality by Age, Sex and Cause"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2015/10/22 enabling
        'jrs 2014/04/22 disabling
        'jrs 2013/02/25 adding
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Morbidity by Age, Sex and Cause"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2010/06/01
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Mortality Display: J-Curve"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2012/08/08
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Mortality and Disability Years"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2022/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "INFRASTRUCTURE"

        cswmItem = cswmGroup2.Items.Add() 'JAM 2018-01-14 Adding Infrastructure Profile
        cswmItem.Caption = "Infrastructure Profile"
        cswmItem.OnClick = "showhg();"

        'jrs 2022/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "DEVELOPMENT GOALS"

        'jrs 2017/05/31
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "SDG Overview Table"
        cswmItem.OnClick = "showhg();"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "SDG Graph"
        cswmItem.OnClick = "showhg();"

        'jrs 2007/03/16
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Poverty Level Display"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2024/04/09 including Performance Risk in general version
        'jrs 2013/08/21
        'If DevelopmentMode Then
        'jrs 2022/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "GENERAL PERFORMANCE ANALYSIS"

        'jrs 2013/05/31
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Performance Risk Evaluation"
        cswmItem.OnClick = "showhg();"
        'End If

        'jrs 2022/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2024/11/25
        If StandAloneMode Then
            'jrs 2022/01/28
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "BILATERAL"

            'jrs 2022/01/28 moved out of Data Analysis into Specialized displays
            'jrs 2020/07/06
            'jrs 2022/01/28 If DevelopmentMode Then
            'cswmItem = cswmGroup1.Items.Add()
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Bilateral History and Forecast"
            cswmItem.OnClick = "alert('Warning: Bilateral Analysis still under development, use cautiously'); showhg();" 'JAM 2013-05-18 Adding Hour Glass
            'jrs 2022/01/28 End If

            'jrs 2022/05/30
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "--shadow--"
            cswmItem.DividerColor = "#808080"
            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
        End If

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "ECONOMY"

        'mti August 2004 5
        'cswmItem.Enabled = False
        'mti August 2004 5 -end

        'jrs 2005/11/18
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Development Profile"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2020/06/06
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Growth Accounting"
        cswmItem.OnClick = "showhg();"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Social Accounting Matrix"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "World Bank Financial Flows"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2022/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "WORLD MAP AND ALL-VARIABLE DISPLAYS"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "World Map, Lorenz Curve, Gini, Histogram"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2005/08/25
        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Millennium Development Goals"
        'cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2022/05/30
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "WORLD VALUES"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "World Value Survey by Age"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'jrs 2019/07/31
        If Not bHidePrivateVars Then
            'jrs 2022/05/30
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "--shadow--"
            cswmItem.DividerColor = "#808080"
            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "ENVIRONMENT"

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Advanced Sustainability Analysis"
            cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass
        End If


        'jrs 2005/04/07
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Activate Pre-Run Scenario for Display"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        Dim sDirSess As String = "\Session"
        If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
            sDirSess = sDirSess & "0"
        End If
        sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sDirName As String
        sDirName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text)
        cswmGroup2 = cswmItem.AddGroup()
        LoadMenuFiles(sDirName, cswmItem, cswmGroup2, "run.db", Session("BaseSize"), "") 'jrs 2005/11/14 added Session("BaseSize")
        'jrs 2005/08/05
        If cswmGroup2.Items.Count = 0 Then
            cswmGroup1.Items.Remove("Activate Pre-Run Scenario for Display")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Self-Managed Display (Complete Flexibility)"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Scenario Analysis"
        cswmGroup1.ID = "Group_2177"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Guided Scenario Analysis"
        'cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Quick Scenario Analysis with Tree"
        cswmItem.OnClick = "showhg();"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Selected Functions"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        Dim cswmGroup3 As Coalesys.WebMenu.Group
        cswmGroup3 = cswmItem.AddGroup()

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "Bivariate Functions"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmItem = cswmGroup3.Items.Add()
        cswmItem.Caption = "Multivariate Functions"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Run"
        cswmItem.OnClick = "showhg();"

        'jrs 2020/01/07
        If StandAloneMode Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Batch Run"
            cswmItem.OnClick = "showhg();"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "File Management"

            Dim cswmGroup4 As Coalesys.WebMenu.Group
            cswmGroup4 = cswmItem.AddGroup()

            sDirName = Replace(Session("PutFdrvRun"), sDirSess, "", 1, -1, CompareMethod.Text)

            'jrs 2020/08/27
            cswmItem = cswmGroup4.Items.Add()
            cswmItem.Caption = "OPEN run file as working file"
            cswmItem.OnClick = "showhg();"

            cswmGroup2 = cswmItem.AddGroup()
            LoadMenuFiles(sDirName, cswmItem, cswmGroup2, "run.db", 0, "Open") 'jrs 2005/11/14 added Session("BaseSize")

            'Adding ifsBase and ifsBaseHist to swtich from Main Menu
            If Session("HistoricBase") = CStr(True) Then
                If Dir(sDirName + "\IFsBase.run.db") > "" Then
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = "IFsBase"
                    cswmItem.UserData = sDirName.Substring(0, sDirName.Length - 1)
                    cswmItem.URL = "Open"
                End If
            Else
                If Dir(sDirName + "\IFsBaseHist.run.db") > "" Then
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = "IFsBaseHist"
                    cswmItem.UserData = sDirName.Substring(0, sDirName.Length - 1)
                    cswmItem.URL = "Open"
                End If
            End If

            If cswmGroup2.Items.Count = 0 Then
                cswmGroup4.Items.Remove("OPEN run file as working file")
            End If

            cswmItem = cswmGroup4.Items.Add()
            cswmItem.Caption = "SAVE working file as ..."
            cswmItem.OnClick = "hidehg();"

            cswmItem = cswmGroup4.Items.Add()
            cswmItem.Caption = "DELETE run file"
            cswmItem.OnClick = "RUsureDEL();" 'jrs 2020/08/28

            cswmGroup2 = cswmItem.AddGroup()
            LoadMenuFiles(sDirName, cswmItem, cswmGroup2, "run.db", 0, "") 'jrs 2005/11/14 added Session("BaseSize")
            If cswmGroup2.Items.Count = 0 Then
                cswmGroup4.Items.Remove("DELETE run file")
            End If

            'jrs 2021/10/20
            If Session("DefaultFlexDisplayFilterNew") > "" AndAlso Session("DefaultFlexDisplayFilterNew") <> DefaultFlexDisplayFilter And DevelopmentMode = False Then
                Dim sIP As String
                sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")
                Dim sSQL As String = "UPDATE IFsInit SET [Value] = """ & Session("DefaultFlexDisplayFilterNew") & """ WHERE [Variable] = 'DefaultFlexDisplayFilter'"
                updateAccessWith1Command(sIP, sSQL)
            End If
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Reload Base"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "File Management"

        'cswmGroup2 = cswmItem.AddGroup()

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "OPEN run file as working file"

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "SAVE working file as ..."

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "DELETE run file"

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "SELECT file for display"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Data Analysis"
        cswmGroup1.ID = "Group_c1c2"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "World Map"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Analyze Across Countries (Cross-Sectional)"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Analyze Across Time (Longitudinal)"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Computed Functions"
        cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass


        'mti September 2004 4
        'cswmItem.Enabled = False


        'jrs 2019/12/26
        If StandAloneMode Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Extended Features"
            cswmGroup1.ID = "mnuExtendeedFeatures"

            'JRS 2006/11/21
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Identify Group or Country/Region Members"
            cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Edit Group or Country/Region Members"
            cswmItem.OnClick = "showhg();"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Rebuild Base"
            cswmItem.OnClick = "showhg();"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Rebuild Historical Base"
            cswmItem.OnClick = "showhg();"

            'jrs 2023/01/17
            cswmItem = cswmGroup1.Items.Add()
            'jrs 2023/02/15
            'cswmItem.Enabled = False
            cswmItem.Caption = "Import Data (Single Series)"
            cswmItem.OnClick = "showhg();"

            'jrs 2026/07/22
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Import Provinces Data (Single Series)"
            cswmItem.OnClick = "showhg();"


            'jrs 2023/03/28
            cswmItem = cswmGroup1.Items.Add()
            'jrs 2023/02/15
            'cswmItem.Enabled = False
            'jrs 2024/01/09 added and Merge
            cswmItem.Caption = "Vet and Merge Imported Data"
            cswmItem.OnClick = "showhg();"


            'jrs 2022/09/09
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Import Population Data"
            cswmItem.OnClick = "showhg();"

            'jrs 2023/06/06 
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Earliest Data, Add or Update"
            cswmItem.OnClick = "checkToAddMostRecent();"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Most Recent Data, Add or Update"
            cswmItem.OnClick = "checkToAddMostRecent();"


            'jrs 2022/09/13
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Most Recent Adjusted Data, Add or Update"
            cswmItem.OnClick = "showhg();"

            'jrs 2022/09/14
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Project Data"

            cswmGroup2 = cswmItem.AddGroup()

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Load Project Data"
            cswmItem.OnClick = "showhg();"

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Unload Project Data"
            cswmItem.OnClick = "showhg();"

            'jrs 2022/09/20
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Add or Delete SubRegions"

            'jrs 2022/05/31 moved from Scenario menu
            If DevelopmentMode Then
                'jrs 2022/12/12
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Guided Base Year Change"
                cswmItem.OnClick = "GBYChangeCheck(" & FirstYearForecast.ToString & ", " & FirstYearHistory.ToString & ", 2100);"

                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Compare VB6 vs .NET"
                cswmItem.OnClick = "showhg();"

                'jrs 2022/05/19 
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Translate Base Case from VB6 To .NET"
                cswmItem.OnClick = "showhg();"

                'jrs 2013/05/31
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Create Code To Add Tables In .NET"
                cswmItem.OnClick = "showhg();"

                'jrs 2023/10/11 
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Create Code to Regionalize Variables in .NET"
                cswmItem.OnClick = "showhg();"

                'jrs 2025/10/03 
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Standarize Legacy VarNames in run files"
                cswmItem.OnClick = "showhg();"


                'jrs 2023/09/06
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Create New Analytical Functions Using Constant USD$"
                cswmItem.OnClick = "showhg();"

                'jrs 2025/05/19
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Create Report of Missing Groups in Scenarios"
                cswmItem.OnClick = "showhg();"

                'jrs 2026/07/28
                cswmItem = cswmGroup1.Items.Add()
                cswmItem.Caption = "Create Report of Spelling Errors in Flex Displays"
                cswmItem.OnClick = "showhg();"

            End If

            'jrs 2023/05/29
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Merge Migration series from UNPD and Abel"
            If Dir(DataFileDir$ + "IFsHistSeriesDyadic.db") = "" Then
                cswmItem.OnClick = "checkToMergeMigration();"
            Else
                cswmItem.OnClick = "showhg();"
            End If

            'jrs 2023/06/13
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Add or Delete Countries"
            'jrs 2023/07/03
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Recreate Scenario File"
            'jrs 2023/08/22
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Clear Session"
        Else
            'JRS 2006/11/21
            cswmItem = cswmGroup1.Items.Add()
            'jrs 2025/05/16 I was using Or instead of or
            cswmItem.Caption = "Identify Group or Country/Region Members"
            cswmItem.OnClick = "showhg();" 'JAM 2013-05-18 Adding Hour Glass
        End If

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Model Change"
        'cswmGroup1.ID = "Group_620d"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Manage Country Data"

        'cswmGroup2 = cswmItem.AddGroup()

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Edit Country Data"

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Display Processed Country Data"

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Historic Data File Processing"

        'Dim cswmGroup3 As Coalesys.WebMenu.Group
        'cswmGroup3 = cswmItem.AddGroup()

        'cswmItem = cswmGroup3.Items.Add()
        'cswmItem.Caption = "Import Data from XLS File"

        'cswmItem = cswmGroup3.Items.Add()
        'cswmItem.Caption = "Earliest Data, Add Or Update"

        'cswmItem = cswmGroup3.Items.Add()
        'cswmItem.Caption = "Most Recent Data, Add Or Update "

        'cswmItem = cswmGroup3.Items.Add()
        'cswmItem.Caption = "Remove Years from Data"

        'cswmItem = cswmGroup3.Items.Add()
        'cswmItem.Caption = "Identify And Save Years"

        'cswmItem = cswmGroup3.Items.Add()
        'cswmItem.Caption = "Add Or Delete Countries"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Manage Regionalization"

        'cswmGroup2 = cswmItem.AddGroup()

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Identify Country/Region Or Group Members"

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Change Regionalization/Grouping"
        'cswmItem.Enabled = False

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Display Regionally Aggregated Data"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Provide Feedback"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        cswmGroup1.ID = "Group_0258"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Help System" 'jrs 2026/04/27 "Contents"
        'jrs 09/17/04 cswmItem.OnMouseOver = "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT, 0);"
        'jrs 2013/09/04 cswmItem.OnClick = "showHelp()"

        'jrs 2026/04/29 cswmItem.OnClick = LoadHelpJS("Main Menu", "")
        If IsConnectedToInternet() Then
            cswmItem.OnClick = "window.open('https://pardeewiki.du.edu/index.php?title=International_Futures_%28IFs%29');"
        Else
            cswmItem.OnClick = LoadHelpJS("Main Menu", "")
        End If

        'jrs 2006/12/21 cswmItem.OnMouseOver = "javascript:RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT, 16000);"

        'jrs 2025/01/16 added and not isconnected to internet
        'jrs 2021/11/11
        If StandAloneMode = True And Not IsConnectedToInternet() Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Help System (Web)" 'jrs 2026/04/27 "Contents (Web)"
            cswmItem.OnClick = LoadHelpJS("Main Menu", "", True)
        End If

        'jrs 2026/04/27
        If StandAloneMode = False Or IsConnectedToInternet() Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Network Diagram"
            cswmItem.OnClick = "window.open('https://ifsnetworkdiagram.du.edu/');"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "About This Software"

        'jrs 2008/11/25.sh
        If DevelopmentMode And Session("SessionIdx") = -1 Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Clear all Sessions"
        End If

        'JAM 2014-06-10 For Javascript variables Generation
        If DevelopmentMode And Session("SessionIdx") = -1 Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Javascript Variables Generation"
        End If

        'JAM 2019-10-08 For Javascript variables Generation
        If DevelopmentMode And Session("SessionIdx") = -1 Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Use Data from SQLite"

            If Session("mnuSQLite") = "True" Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If
        End If



        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Open New Tabs for Selected options"



        If Session("mnuTabs") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If



        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Your Feedback"
        'cswmItem.OnClick = "self.location='mailto:helpdesk@nic2020.org?subject=IFS Model Feedback';"
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Your Feedback"
        'jrs 2005/11/14 cswmGroup1.DownLevelURL = "mailto:helpdesk@nic2020.org?subject=IFS Model Feedback"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "mailto:bhughes@du.edu?subject=IFS Model Feedback"
        cswmGroup1.OnMenuBarClick = "location.href='mailto:jose.solorzano@du.edu?subject=IFS Model Feedback';"

        'jam 2022/07/02
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = “Open New Tabs for Selected options”







        'jrs 2011/04/29
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "IFs Home"
        cswmGroup1.OnMenuBarClick = "location.href='https://korbel.du.edu/pardee';" 'jrs 2022/03/03

        cswmWebMenu.MenuBar.AbsoluteDockEnabled = False
        cswmWebMenu.MenuBar.AbsoluteDragEnabled = False
        cswmWebMenu.MenuBar.BorderSize = 1
        cswmWebMenu.MenuBar.OuterHighlightColor = "#ffffff"
        cswmWebMenu.MenuBar.OuterShadowColor = "#808080"
        cswmWebMenu.MenuBar.SelectedColor = "navy"
        cswmWebMenu.MenuBar.SelectedHighlightColor = "navy"
        cswmWebMenu.MenuBar.SelectedShadowColor = "navy"
        cswmWebMenu.MenuBar.SelectedTextColor = "white"
        cswmWebMenu.ButtonFont.PaddingLeft = 10
        cswmWebMenu.ButtonFont.PaddingRight = 10
        cswmWebMenu.ItemFont.PaddingLeft = 4

        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        'jrs 2011/04/01
        PackJScritpBuilder()

        'jrs 2012/11/20
        If Session("txtChromeMap") = Nothing Then
            If txtChromeMap.Value = "x" Then
                Session("txtChromeMap") = "x"
            End If
        Else
            txtChromeMap.Value = "x"
        End If

        'jrs 2024/05/27
        Session("RebuiltBase") = False
    End Sub


    'UPGRADE_WARNING: Form event Unload has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.Form.Closed' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Closed.html'.
    'JRSVBNET Private Sub frm_MainMenu_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Closed
    'JRS 05/22/03 RAND PARDEE End
    'JRSVBNET End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub GuidedUse_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles GuidedUse.Popup
    'JRSVBNET GuidedUse_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub GuidedUse_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'raman  feb ' 02
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_GuidedUse0.aspx")
        'frm_GuidedUse0.Show() 
        'UPGRADE_WARNING: Form method frm_GuidedUse0.ZOrder has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.BringToFront' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.BringToFront().html'.
        'frm_GuidedUse0.BringToFront() 
        Guided_use = True

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub Help_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Help.Popup
    'JRSVBNET Help_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub Help_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '    Dummy% = WinHelp(hWnd, App.HelpFile, HELP_FINDER, CLng(0))
        '    Dummy% = WinHelp(hWnd, App.HelpFile, HELP_INDEX, CLng(0))
        'Help_Index brings up Windows 3.1 help format - 1st topic
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub IdentifySaveYears_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles IdentifySaveYears.Popup
    'JRSVBNET IdentifySaveYears_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub IdentifySaveYears_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ProcUpdRawVarYear.aspx")
        'frm_ProcUpdRawVarYear.Show() 
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub ImportDatafromXLSFile_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ImportDatafromXLSFile.Popup
    'JRSVBNET ImportDatafromXLSFile_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub ImportDatafromXLSFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '    MsgBox "Entering Import Data Sub of Main Menu"
        'JRSVBNET CMDialog1.FileName = ""
        '    MsgBox "Completed filename"

        'JRSVBNET CMDialog1.DialogTitle = "Import Time Series"
        '    MsgBox "Completed DialogTitle"


        '   CancelError is True
        '   -------------------

        On Error GoTo ErrHandler3

        'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        '    MsgBox "Completed InitDir"

        'JRSVBNET CMDialog1.Filter = "All Files (*.*)|*.*| Microsoft Excel Files (*.xls)|*.xls| Microsoft Access Files (*.mdb)|*.mdb"
        '    MsgBox "Completed Filter"

        'JRSVBNET CMDialog1.Action = 1


        '    MsgBox "Finished with common dialog setup"
        'Screen.MousePointer = 11

        'ImportFile ('JRSVBNET CMDialog1.Filename)

        'UPGRADE_WARNING: Couldn't resolve default property of object ImportFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET ImportFileName = 'JRSVBNET CMDialog1.FileName

        'UPGRADE_WARNING: Couldn't resolve default property of object ImportFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("ImportFileName") <> "" Then

            'Screen.MousePointer = 0

            'Take Care of Cancel here
            '    MsgBox "Ready to show importxlsnew"

            'JRSVBNET Frm_ImportXLSNew.Show()
            '        'JRSVBNET IFsGUIctrl1.ImportXls (ImportFileName)
        End If
        Exit Sub


ErrHandler3:  ' User Pressed Cancel
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        Exit Sub

    End Sub

    Private Sub mnuSAM_Click()
        'JRSVBNET IFsGUIctrl1.SocialAccountingMatrix()
    End Sub



    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET *** BEGIN ***
    'Public Sub mnuModelIntegration_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuModelIntegration.Popup
    '    mnuModelIntegration_Click(eventSender, eventArgs)
    'End Sub
    'JRSVBNET *** END ***

    Public Overrides Sub mnuModelIntegration_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'Msg$ = "This is feature is under development and temporarily disabled."
        'MsgBox Msg$
        'Exit Sub

        'Vensim Integration- Late November 2003
        If VensimOpened > 0 Then
            LoadMessage("Unfortunately you have to restart IFs to be able to run integration.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.html'.
        If Session("HistoricBase") = True Then
            LoadMessage("Please close the historical base to be able to run integration.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
        'End Vensim Integration- Late November 2003

        'Vensim Integration- whole procedure

        '    MsgBox "Entering Import Data Sub of Main Menu"
        'JRSVBNET CMDialog1.FileName = ""
        '    MsgBox "Completed filename"

        'JRSVBNET CMDialog1.DialogTitle = "Call External Model"
        '    MsgBox "Completed DialogTitle"


        '   CancelError is True
        '   -------------------

        On Error GoTo ErrHandler3

        'JRSVBNET CMDialog1.InitDir = VenSimFileDir 'PutFdrv$ + "Vensim\" 'hdi.vmf"
        '    MsgBox "Completed InitDir"

        'JRSVBNET CMDialog1.Filter = "Vensim Model Binary (*.vmf)|*.vmf"
        '    MsgBox "Completed Filter"

        'JRSVBNET CMDialog1.Action = 1


        '    MsgBox "Finished with common dialog setup"
        'Screen.MousePointer = 11

        'ImportFile ('JRSVBNET CMDialog1.Filename)

        'UPGRADE_WARNING: Couldn't resolve default property of object ImportFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET ImportFileName = 'JRSVBNET CMDialog1.FileName

        'UPGRADE_WARNING: Couldn't resolve default property of object ImportFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("ImportFileName") <> "" Then

            'Screen.MousePointer = 0

            'Take Care of Cancel here
            '    MsgBox "Ready to show importxlsnew"

            'JRSVBNET ImportVensim.Show() '1
            '        'JRSVBNET IFsGUIctrl1.ImportXls (ImportFileName)
        End If
        Exit Sub


ErrHandler3:  ' User Pressed Cancel
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        Exit Sub



    End Sub
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub mnuVariable_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVariable.Popup
    'JRSVBNET mnuVariable_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub mnuVariable_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET frm_AddDelIFsVar.Show()
    End Sub

    'JRS 08/21/03
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub mnuWorldVariables_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuWorldVariables.Popup
    'JRSVBNET mnuWorldVariables_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub mnuWorldVariables_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_WorldVariables.aspx")
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub Open_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Open.Popup
    'JRSVBNET Open_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub Open_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '            Dim msg As String
        '            Dim histfile As String
        '            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        '            'JRSVBNET CMDialog1.FileName = ""
        '            'JRSVBNET CMDialog1.DialogTitle = "Open File"


        '            '   CancelError is True
        '            '   -------------------

        '            On Error GoTo ErrHandler3

        '            'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        '            'JRSVBNET CMDialog1.Filter = "Run Files (*.run)|*.run"
        '            'JRSVBNET CMDialog1.Action = 1

        '            histfile = Session("PutFdrvRun2") & "\" & "IFsBaseHist.run"
        '            If CBool(HistoricOption) = False And FilePicker1.Text = histfile Then
        '                msg = "The student version of the model is unable to access the historic "
        '                msg = msg & "run of the model."

        '                LoadMessage(msg, Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")

        '            End If
        '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        '            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.html'.
        '            Function2.OpenFile(FilePicker1.Text, Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"), _
        '        Session("PutFdrvRun"), Session("WorkingFile"), Session("RunFiles"), Session("HistoricBase"), Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), _
        '        Session("FilesSelected"), Session("DisplayLastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("ScenarioFileDir"), Session("FinYear"), _
        '        Session("SessionIdx"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"))
        '            Session("WorkingFile") = FilePicker1.Text
        '            'JRS 1/6/01 DISPLAY
        '            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.html'.
        '            Session("Reload") = "True"
        '            Session("COUNTE") = 0
        '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        '            Exit Sub

        'ErrHandler3:  ' User Pressed Cancel
        '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        '            Exit Sub

    End Sub

    '=============================MainMenu.Frm===============================
    'Private Sub OpenFile(Filename$)
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


    '            Open finam$ For Binary As #1
    '            Session("RECORD") = VarPoint(PosModYear)
    '            Session("RECORD") = Session("RECORD") * 4 - 3
    '            Get #1, Session("RECORD"), Number!
    '            Session("FinYear")(j) = Number! - 'JRSVBNET IFsGUIctrl1.Session("FirstYear") + 1



    '   CancelError is True
    '    On Error GoTo ErrHandler
    '   -------------------------

    '    InFile$ = Filename$
    '    If InStr(StrConv(InFile$, vbUpperCase), "IFSBASEHIST.RUN") > 0 Then
    '        'JRSVBNET IFsGUIctrl1.Session("FirstYear") = FirstYearHistory%
    '        'JRSVBNET IFsGUIctrl1.Session("LastYear") = LastYearHistory%
    '        'JRSVBNET IFsGUIctrl1.HistoricBase = True
    '        'JRSVBNET IFsGUIctrl1.cActiveBase = "IFSBASEHIST.RUN" 'IFsHist_MDB2
    '        'JRSVBNET IFsGUIctrl1.cRunFiles(1) = PutFdrvRun$ + "IFSBASEHIST.RUN"
    '    ElseIf InStr(StrConv(InFile$, vbUpperCase), "IFSBASE.RUN") > 0 Then
    '        'JRSVBNET IFsGUIctrl1.Session("FirstYear") = FirstYearForecast%
    '        'JRSVBNET IFsGUIctrl1.Session("LastYear") = LastYearForecast%
    '        'JRSVBNET IFsGUIctrl1.HistoricBase = False
    '        'JRSVBNET IFsGUIctrl1.cActiveBase = "IFSBASE.RUN"
    '        'JRSVBNET IFsGUIctrl1.cRunFiles(1) = PutFdrvRun$ + "IFSBASE.RUN"
    '    Else
    '        Open InFile$ For Binary As #2 'Could be history or forecast scenario
    '        Session("RECORD") = VarPoint(PosModYearI)
    '        Session("RECORD") = Session("RECORD") * 4 - 3
    '        Get #2, Session("RECORD"), Number!
    '        'JRSVBNET IFsGUIctrl1.Session("FirstYear") = Number!
    '        Close #2

    '        If 'JRSVBNET IFsGUIctrl1.Session("FirstYear") = FirstYearForecast% Then
    '            'JRSVBNET IFsGUIctrl1.Session("LastYear") = LastYearForecast%
    '            'JRSVBNET IFsGUIctrl1.HistoricBase = False
    '            'JRSVBNET IFsGUIctrl1.cActiveBase = "IFSBASE.RUN"
    '            'JRSVBNET IFsGUIctrl1.cRunFiles(1) = PutFdrvRun$ + "IFSBASE.RUN"
    '        Else
    '            'JRSVBNET IFsGUIctrl1.Session("LastYear") = LastYearHistory%
    '            'JRSVBNET IFsGUIctrl1.HistoricBase = True
    '            'JRSVBNET IFsGUIctrl1.cActiveBase = "IFSBASEHIST.RUN"
    '            'JRSVBNET IFsGUIctrl1.cRunFiles(1) = PutFdrvRun$ + "IFSBASEHIST.RUN"
    '        End If

    '    End If
    '    OutFile$ = PutFdrvRun$ + "working.run"
    '    FileCopy InFile$, OutFile$
    '    WorkingFile$ = InFile$
    '    Session("FilesSelected") = 1  'Must eliminate other selected files because may be
    'incompatible with new working file in initial year (i.e, historical/forecast)
    '    Session("COUNTE") = 0
    '    'JRSVBNET 'JRSVBNET IFsGUIctrl1.XClearPlot

    'ErrHandler: ' User Pressed Cancel
    '    Exit Sub

    'JRS 8/23/01 GUI Updating the properties
    'of the control
    '    'JRSVBNET IFsGUIctrl1.Session("FirstYear") = 'JRSVBNET IFsGUIctrl1.Session("FirstYear")
    '    'JRSVBNET IFsGUIctrl1.Session("LastYear") = 'JRSVBNET IFsGUIctrl1.Session("LastYear")
    '    'JRSVBNET IFsGUIctrl1.HistoricBase = 'JRSVBNET IFsGUIctrl1.HistoricBase
    '    'JRSVBNET IFsGUIctrl1.cActiveBase = 'JRSVBNET IFsGUIctrl1.cActiveBase
    '    'JRSVBNET IFsGUIctrl1.cRunFiles(1) = 'JRSVBNET IFsGUIctrl1.cRunFiles(1)

    'End Sub

    '=============================MainMenu.Frm===============================
    Private Sub ProfFeatures_Click()
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Check existence of full IFs.mdb data file
        '   -----------------------------------------

        On Error GoTo ErrorHandler
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Regionalization, "Country", valtbl)

        Exit Sub

ErrorHandler:
        Select Case Err.Number
            Case 3024
                msg = "Professional features require full Access data files - "
                msg = msg & "It appears that you do not have the full IFS_MDB "
                msg = msg & " or it is not located on DATA subdirectory.  "
                msg = msg & "You may not have the Professional Edition."
            Case Else : msg = "Error " & Str(Err.Number) & " occurred."
        End Select

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

        Exit Sub

    End Sub
    '=============================MainMenu.Frm===============================
    Private Sub PlotAcrossCountries_Click()
        Dim frm_ScatVarS As Object
        Dim ScatGraphTyp As Object
        Dim ScatGraphStyl As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphStyl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ScatGraphStyl = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphTyp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ScatGraphTyp = 9
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphStyl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphStyle") = ScatGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphTyp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = ScatGraphTyp
        'UPGRADE_WARNING: Couldn't resolve default property of object frm_ScatVarS.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        frm_ScatVarS.Show()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub PlotAcrossTime_Click()
        Dim frm_LongVarS As Object
        Dim ScatGraphTyp As Object
        Dim ScatGraphStyl As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphStyl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ScatGraphStyl = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphTyp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ScatGraphTyp = 9
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphStyl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphStyle") = ScatGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphTyp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = ScatGraphTyp
        'UPGRADE_WARNING: Couldn't resolve default property of object frm_LongVarS.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        frm_LongVarS.Show()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub RebuildBase_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles RebuildBase.Popup
    'JRSVBNET RebuildBase_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub RebuildBase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If ModelLoad <> "LoadStudent" Or CBool(DevelopmentMode) = True Then
            'Form Load should prevent ever
            'reaching this sub unless ProfEdition; redundant protection
            Session("IFs_MDB2Active") = Session("IFs_MDB2")
            Session("HistoricBase") = False
            'JRSVBNET IFsGUIctrl1.cActiveBase = "IFSBASE.RUN"
            'JRSVBNET IFsGUIctrl1.Session("FirstYear") = FirstYearForecast
            'JRSVBNET IFsGUIctrl1.Session("LastYear") = LastYearForecast
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_RebuildBase.aspx")
            'frm_RebBase.Show() 
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Carret = "<br>" & Chr(10)
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            carret2 = Carret + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = "Rebuilding the base requires that you have " + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "the Professional Edition of the model. " + Carret
            msg = msg & "This is not the Professional Edition."

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'JRS 8/23/01 GUI Updating the properties
        'of the control
        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.Session("FirstYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.Session("FirstYear") = 'JRSVBNET IFsGUIctrl1.Session("FirstYear")
        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.Session("LastYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.Session("LastYear") = 'JRSVBNET IFsGUIctrl1.Session("LastYear")
        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.HistoricBase. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.HistoricBase = 'JRSVBNET IFsGUIctrl1.HistoricBase
        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.cActiveBase. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.cActiveBase = 'JRSVBNET IFsGUIctrl1.cActiveBase
        'JRSVBNET IFsGUIctrl1.cIFs_MDB2Active = IFs_MDB2Active

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub RebuildCommon_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles RebuildCommon.Popup
    'JRSVBNET RebuildCommon_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub RebuildCommon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


    End Sub

    Private Sub Regionalization_Click()

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub RebuildHistoricBase_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles RebuildHistoricBase.Popup
    'JRSVBNET RebuildHistoricBase_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub RebuildHistoricBase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If ModelLoad <> "LoadStudent" Or CBool(DevelopmentMode) = True Then
            'Form Load should prevent ever
            'reaching this sub unless ProfEdition; redundant protection
            'JRSVBNET IFsGUIctrl1.Session("FirstYear") = FirstYearHistory
            'JRSVBNET IFsGUIctrl1.Session("LastYear") = LastYearHistory
            'JRSVBNET IFsGUIctrl1.HistoricBase = True
            Session("IFs_MDB2Active") = Session("IFsHist_MDB2")
            'JRSVBNET IFsGUIctrl1.cActiveBase = "IFSBASEHIST.RUN"
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_RebBase.aspx")
            'frm_RebBase.Show() 
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Carret = "<br>" & Chr(10)
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            carret2 = Carret + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = "Rebuilding the base requires that you have " + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "the Professional Edition of the model. " + Carret
            msg = msg & "This is not the Professional Edition."

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'JRS 8/23/01 GUI Updating the properties
        'of the control
        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.Session("FirstYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.Session("FirstYear") = 'JRSVBNET IFsGUIctrl1.Session("FirstYear")
        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.Session("LastYear"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.Session("LastYear") = 'JRSVBNET IFsGUIctrl1.Session("LastYear")
        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.HistoricBase. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.HistoricBase = 'JRSVBNET IFsGUIctrl1.HistoricBase
        'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.cActiveBase. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.cActiveBase = 'JRSVBNET IFsGUIctrl1.cActiveBase
        'JRSVBNET IFsGUIctrl1.cIFs_MDB2Active = IFs_MDB2Active
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub RebuildWithoutVariableChange_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles RebuildWithoutVariableChange.Popup
    'JRSVBNET RebuildWithoutVariableChange_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub RebuildWithoutVariableChange_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If CBool(DevelopmentMode) = True Then 'Form Load should prevent ever
            'reaching this sub unless Mode is True; redundant protection
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_RebComm.aspx")
            'frm_RebComm.Show() 
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Carret = "<br>" & Chr(10)
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            carret2 = Carret + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = "Rebuilding the common block requires that you have both" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & " Visual Basic and the source code of the model.  This " + Carret
            msg = msg & " option is therefore available "
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "only in model development mode.  " + Carret
            msg = msg & "This copy of the model is not licensed for development mode."

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub RegionData_Click()
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Check existence of full IFs.mdb data file
        '   -----------------------------------------

        On Error GoTo ErrorHandler

        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Regionalization, "Country", valtbl)

        Exit Sub

ErrorHandler:
        Select Case Err.Number
            Case 3024
                msg = "Professional features require full Access data files - "
                msg = msg & "It appears that you do not have the full IFS_MDB "
                msg = msg & " or it is not located on DATA subdirectory.  "
                msg = msg & "You may not have the Professional Edition."
            Case Else : msg = "Error " & Str(Err.Number) & " occurred."
        End Select

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub RegionMembers_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles RegionMembers.Popup
    'JRSVBNET RegionMembers_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub RegionMembers_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_RegMemb.aspx")

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub Register_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Register.Popup
    'JRSVBNET Register_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub Register_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Register.aspx")
        'frm_Register.Show() 

    End Sub

    '=============================MainMenu.Frm===============================
    Public Overrides Sub Report_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2011/05/04
        Dim sPar As String
        If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
            Session("MMCountry") = "AF"
        End If
        If Session("MMCountry") = Nothing Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Report.aspx?" & PrepareForURL(sPar))
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    End Sub


    Public Sub ShowReport(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs, ByVal cNum As Integer)
        Support.FormManager.StateStore(Me)

        Support.FormManager.Show(Me, "frm_Report.aspx")
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    End Sub


    Public Sub WBFinancialFlows_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2011/05/04
        Dim sPar As String
        If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
            Session("MMCountry") = "AF"
        End If
        If Session("MMCountry") = Nothing Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_WorldBankFinancialFlows.aspx?" & PrepareForURL(sPar))
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    End Sub

    Public Sub mnuASA_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2011/05/04
        Dim sPar As String
        If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
            Session("MMCountry") = "AF"
        End If
        If Session("MMCountry") = Nothing Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ASA.aspx?" & PrepareForURL(sPar))
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub Retrieve_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRSVBNET CMDialog1.FileName = ""

        '   CancelError is True
        '   -------------------

        On Error GoTo CancelError

        'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        'JRSVBNET CMDialog1.DialogTitle = "Retrieve and expand scenario file"
        'JRSVBNET CMDialog1.Filter = "Scenario Files (*.sru)|*.sru"
        'JRSVBNET CMDialog1.Action = 1
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRSVBNET If CMDialog1.FileName <> "" Then RetrieveFile(('JRSVBNET CMDialog1.FileName))
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

CancelError:  ' User Pressed Cancel
        Exit Sub

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub Run_Click()

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub SaveAs_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SAVEAS.Popup
    'JRSVBNET SaveAs_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub SaveAs_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        '            'JRSVBNET CMDialog1.FileName = ""
        '            'JRSVBNET CMDialog1.DialogTitle = "Save As .."

        '            '   CancelError is True
        '            '   -------------------

        '            On Error GoTo ErrHandler2

        '            'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        '            'JRSVBNET CMDialog1.Filter = "Run Files (*.run)|*.run"
        '            'JRSVBNET CMDialog1.Action = 2
        '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        '            SaveFile((FilePicker1.Text))
        '            Session("WorkingFile") = FilePicker1.Text
        '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

        'ErrHandler2:  ' User Pressed Cancel
        '            Exit Sub

    End Sub


    '=============================MainMenu.Frm===============================
    Private Sub RetrieveFile(ByRef Filename As String)
        Dim OutFile As String
        Dim msg As String
        Dim FILEBUF As Object
        Dim rps As Object
        Dim Number As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   CancelError is True
        '   -------------------
        On Error GoTo ErrHandler5

        '   Open files and read length of run
        '   ---------------------------------
        'jrs 2005/02/14
        Dim fNum As Integer, fNum2 As Integer
        fNum = FreeFile()
        FileOpen(fNum, Filename, OpenMode.Binary)
        fNum2 = FreeFile()
        FileOpen(fNum2, Session("PutFdrvRun") & "working.run", OpenMode.Binary)
        'UPGRADE_WARNING: Couldn't resolve default property of object PosModYear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VarPoint(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("RECORD") = VarPoint(PosModYear) * 4 - 3
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FileGet(fNum, Number, Session("RECORD"))
        'UPGRADE_WARNING: Couldn't resolve default property of object RunYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("RunYears") = Number - Session("FirstYear") + 1
        'JRS 8/19/01 GUI
        'UPGRADE_WARNING: Couldn't resolve default property of object RunYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.cRunYears = RunYears

        '   Transfer from saved scenario file to working file
        '   -------------------------------------------------
        'UPGRADE_WARNING: Couldn't resolve default property of object RunYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For IY = 1 To Session("RunYears")
            'UPGRADE_WARNING: Couldn't resolve default property of object filen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object rps. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            rps = (IY - 1) * filen * 4 + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object rps. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Get was upgraded to FileGetObject and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            FileGetObject(fNum, rps, FILEBUF)
            'UPGRADE_WARNING: Put was upgraded to FilePutObject and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            FilePutObject(fNum2, CInt(rps), FILEBUF)
        Next IY

        FileClose(fNum)
        FileClose(fNum2)

        Exit Sub

ErrHandler5:  ' Disk Full
        If Err.Number = 61 Then
            msg = "Error: Disk Full - Save not completed"

            Kill(OutFile)

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
        Exit Sub


    End Sub
    '=============================MainMenu.Frm===============================
    Private Sub StoreFile(ByRef Filename As String)
        Dim OutFile As String
        Dim msg As String
        Dim FILEBUF As Object
        Dim rps As Object
        Dim Number As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   CancelError is true; or effort made to save over base case
        '   ----------------------------------------------------------
        On Error GoTo ErrHandler5
        If (InStr(1, Filename, "IFSBASE.RUN", 1) > 0) And (CBool(DevelopmentMode) <> True) Then GoTo SaveBase


        '   Open files and read length of run from working.run
        '   ------------------------------------------------
        'jrs 2005/02/14
        Dim fNum As Integer, fNum2 As Integer
        fNum = FreeFile()
        FileOpen(fNum, Session("PutFdrvRun") & "working.run", OpenMode.Binary)
        fNum2 = FreeFile()
        FileOpen(fNum2, Filename, OpenMode.Binary)
        'UPGRADE_WARNING: Couldn't resolve default property of object PosModYear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VarPoint(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("RECORD") = VarPoint(PosModYear) * 4 - 3
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FileGet(fNum, Number, Session("RECORD"))
        'UPGRADE_WARNING: Couldn't resolve default property of object RunYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("RunYears") = Number - Session("FirstYear") + 1
        'JRS 8/19/01 GUI
        'UPGRADE_WARNING: Couldn't resolve default property of object RunYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET IFsGUIctrl1.cRunYears = RunYears


        '   Transfer from working file to saved scenario file
        '   -------------------------------------------------
        'UPGRADE_WARNING: Couldn't resolve default property of object RunYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For IY = 1 To Session("RunYears")
            'UPGRADE_WARNING: Couldn't resolve default property of object filen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object rps. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            rps = (IY - 1) * filen * 4 + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object rps. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Get was upgraded to FileGetObject and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            FileGetObject(fNum, rps, FILEBUF)
            'UPGRADE_WARNING: Put was upgraded to FilePutObject and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            FilePutObject(fNum2, CInt(rps), FILEBUF)
        Next IY

        FileClose(fNum)
        FileClose(fNum2)

        Exit Sub


ErrHandler5:  ' Disk Full
        If Err.Number = 61 Then
            msg = "Error: Disk Full - Save not completed"

            Kill(OutFile)

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
        Exit Sub


SaveBase:   ' Protect IFsBase
        msg = "You may not overwrite IFsBase"

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub
    '=============================MainMenu.Frm===============================
    Private Sub SaveFile(ByRef Filename As String)
        Dim msg As String
        Dim OutFile As String
        Dim InFile As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   CancelError is True
        '   -------------------

        On Error GoTo ErrHandler4

        If (ModelLoad = "LoadStudent") Or (CBool(RebuiltBase) = False) Or (CBool(RunModel) = False) Then
            If (InStr(1, Filename, "IFSBASE.RUN.DB", 1) > 0) And (CBool(DevelopmentMode) <> True) Then GoTo SaveBase
        End If 'Allow replacement of base only for professional edition when
        'model is rebuilt and rerun

        InFile = Session("PutFdrvRun") & "working.run.db"
        OutFile = Filename
        FileCopy(InFile, OutFile)
        Session("WorkingFile") = OutFile

        'JRS 10/16/02 SAM COPY
        '    InFile$ = PutFdrvRun$ + "SAMWorking.mdb"
        '    OutFile$ = "SAM" + Filename$
        '    If (Right(OutFile$, 4) = ".run") Then
        '        OutFile$ = Left(OutFile$, Len(OutFile$) - 4)
        '    End If
        '    OutFile$ = OutFile$ + ".mdb"
        '    FileCopy InFile$, OutFile$
        InFile = Session("PutFdrvRun") & "SAMWorking.db"
        Dim sFileName() As String
        sFileName = Split(Filename, "\")
        Dim idx As Short
        OutFile = ""
        For idx = 0 To UBound(sFileName) - 1
            OutFile = OutFile & sFileName(idx) & "\"
        Next idx
        If UCase(sFileName(idx)) = "IFSBASE.RUN.DB" Then
            OutFile = OutFile & "SAMBase"
        Else
            OutFile = OutFile & "SAM" & sFileName(idx)
        End If
        If (VB.Right(OutFile, 7) = ".run.db") Then
            OutFile = VB.Left(OutFile, Len(OutFile) - 7)
        End If
        OutFile = OutFile & ".db"
        FileCopy(InFile, OutFile)

        'Copy ifsPopWork.db
        InFile$ = Session("PutFdrvRun") & "IFsPopWork.db"
        If UCase(sFileName(idx)) = "IFSBASE.RUN.DB" Then
            OutFile = DataFileDir$ & "IFsPop.db"
            FileCopy(InFile$, OutFile$) 'Save in DATA
            OutFile = Session("PutFdrvRun") & "IFsPop.db"
        Else
            OutFile = Replace(OutFile, "SAM" & Replace(sFileName(idx), ".run", ""), "IFsPop" & Replace(sFileName(idx), ".run", ""))
        End If
        FileCopy(InFile$, OutFile$)

        'jrs 2009/03/05 moving out of previous if statement, it was messing up logic
        'Noticed that InFile and OutFile were getting changed before using them outside if statement
        If UCase(sFileName(idx)) = "IFSBASE.RUN.DB" Then
            'jrs 2008/11/21
            InFile$ = Session("PutFdrvRun") & "IFsSmoking.db"
            OutFile = DataFileDir$ & "IFsSmoking.db"
            FileCopy(InFile$, OutFile$) 'Save in DATA
            'jrs 2011/06/09
            '        InFile$ = PutFdrvRun$ & "IFsDyadic.mdb"
            '        OutFile = DataFileDir$ & "IFsDyadic.mdb"
            '        FileCopy InFile$, OutFile$ 'Save in DATA
        End If

        'jrs 2016/09/20
        If Dir(Session("PutFdrvRun") + "IFsForDyadicWork.db") <> "" Then
            InFile$ = Session("PutFdrvRun") & "IFsForDyadicWork.db"
            If UCase(sFileName(idx)) = "IFSBASE.RUN.DB" Then
                OutFile = DataFileDir$ & "IFsForDyadic.db"
            Else
                OutFile = Session("PutFdrvRun") & "IFsForDyadic" & Replace(sFileName(idx), ".run.db", ".db")
            End If
            FileCopy(InFile$, OutFile$) 'Save in DATA
        End If

        Exit Sub

ErrHandler4:  ' Disk Full
        If Err.Number = 61 Then
            msg = "Error: Disk Full - Save not completed"

            Kill(OutFile)

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
        Exit Sub


SaveBase:   ' Protect IFsBase
        msg = "You may not overwrite IFsBase"

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub Scenario_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'jrs 2004/11/23
        SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
            Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
            Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
            Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
            Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
            Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
            Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
        'frm_ScenarioCreate.Show() 
        'JRS 8/30/01 GUI 'JRSVBNET IFsGUIctrl1.ScenarioCreate

    End Sub

    Private Sub SAVE_Click()

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub SELECT_Renamed_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SELECT_Renamed.Popup
    'JRSVBNET SELECT_Renamed_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub SELECT_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRSVBNET CMDialog1.FileName = ""

        '   CancelError is True
        '   -------------------

        On Error GoTo CancelError

        'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        'JRSVBNET CMDialog1.DialogTitle = "Select for Display"
        'JRSVBNET CMDialog1.Filter = "Run Files (*.run)|*.run"
        'JRSVBNET CMDialog1.Action = 1
        'JRSVBNET If CMDialog1.FileName <> "" Then SelectNewFile(('JRSVBNET CMDialog1.FileName))

CancelError:  ' User Pressed Cancel
        Exit Sub

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub SelectFile(ByRef Filename As String)
        Dim msg As String
        Dim firstyearfile As Short
        Dim Number As Single
        Dim i2 As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        For i2 = 1 To Session("FilesSelected")
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object 'JRSVBNET IFsGUIctrl1.cRunFiles(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If InStr(Filename, Session("RunFiles")(i2)) Then GoTo AlreadySelected
        Next i2
        If Session("ActiveBase") = "IFSBASE.RUN" And InStr(UCase(Filename), "IFSBASE.RUN") > 0 Then GoTo AlreadySelected
        If Session("ActiveBase") = "IFSBASEHIST.RUN" And InStr(UCase(Filename), "IFSBASEHIST.RUN") > 0 Then GoTo AlreadySelected

        'jrs 2005/02/14
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, Filename, OpenMode.Binary)
        'UPGRADE_WARNING: Couldn't resolve default property of object PosModYearI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VarPoint(PosModYearI). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("RECORD") = VarPoint(PosModYearI)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("RECORD") = Session("RECORD") * 4 - 3
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FileGet(fNum, Number, Session("RECORD"))
        firstyearfile = Number 'FirstYearForecast%
        FileClose(fNum)
        If Session("ActiveBase") = "IFSBASE.RUN" And firstyearfile <> FirstYearForecast Then
            msg = "You are working with forecast files and this is not a forecast file."

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
        If Session("ActiveBase") = "IFSBASEHIST.RUN" And firstyearfile <> FirstYearHistory Then
            msg = "You are working with history files and this is not a history file."

            LoadMessage(msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Session("FilesSelected") = Session("FilesSelected") + 1
        If Session("FilesSelected") > 10 Then Session("FilesSelected") = 10
        Session("RunFiles")(Session("FilesSelected")) = Filename
        msg = "You have now selected '" & Filename
        msg = msg & "' as file number " & Str(Session("FilesSelected"))

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

AlreadySelected:
        msg = "The file indicated has already been selected for display"

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub SelfManagedDisplay_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SelfManagedDisplay.Popup
    'JRSVBNET SelfManagedDisplay_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub SelfManagedDisplay_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Support.FormManager.StateStore(Me)
        If Session("COUNTE") > 0 Then
            Support.FormManager.Show(Me, "frm_DisplayMenu.aspx")
        Else
            Support.FormManager.Show(Me, "frm_Varsel.aspx")
        End If

    End Sub

    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub SelfManagedScenarios_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SelfManagedScenarios.Popup
    'JRSVBNET SelfManagedScenarios_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub SelfManagedScenarios_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScenarioManage.aspx")
        'frm_ScenarioManage.Show() 
    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub SensitivityAnalysis_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Sensitivity.aspx")
        'frm_Sensitivity.Show() 

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub ShowFunctions_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ShowFunctions.Popup
    'JRSVBNET ShowFunctions_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub ShowFunctions_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'mti September 2004 4 -start
        'JRS 8/7/01 GUI frm_TablFunc2.Show
        'JRSVBNET IFsGUIctrl1.TableFunc2()
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_TablFunc2.aspx")
        'mti September 2004 4 -end 

    End Sub

    'jrs 2020/07/06
    Public Sub BilateralInteractionDisplayWGravity_Click(ByVal eventsender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2021/01/14 *** start ***

        'Run the Procedure, getting any error messages.
        'jrs 2022/12/20
        If Not UseOnlySQLite Then
            're-link tables
            Dim strMsg As String = ""

            strMsg = RefreshTableLinks()

            'strMsg will be a zero-length string if there is no error message.
            If Len(strMsg & "") = 0 Then
                Debug.Print("All; Tables; were; successfully; relinked.")
            Else
                'Notify the user of the errors.
                Session("Message") = strMsg
                LoadMessage("Bilateral Interaction Display with Gravity", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            'jrs 2021/01/14 *** end ***
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_BilateralInteractionDisplay.aspx")
    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub SingleRun_Click()
        'jrs 2004/11/23
        SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
            Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
            Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
            Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
            Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
            Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
            Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))

        Dim msg As String
        Dim fs As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 8/23/01 BEGIN
        'Code introduced to Run Scenario Analysis
        fs = CreateObject("Scripting.FileSystemObject")
        'If it exist
        'UPGRADE_WARNING: Couldn't resolve default property of object fs.FileExists. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Dim ans As Short
        If fs.FileExists(Session("ScenarioFileDir") & "Working.Sce") Then
            msg = "Do you want to run the model with the working scenario from "
            msg = msg & "the scenario tree?"
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 2005/11/04 WinFormsToWeb_MessageBox.Show(msg, MsgBoxStyle.YesNo, "Run-Scenario")
            If True Then 'jrs 2005/11/04 If MsgBoxResult.Yes Then
                'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
                'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
                Session("Scenarioflag") = True
                Session("SensitivityFlag") = CStr(False)
                'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                NR = MaxVal(1) - 1 'Reset in case of Rebuilding base
                'JRS 07/24/02
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.html'.
                If Session("HistoricBase") = True Then
                    FileCopy(Session("PutFdrvRun") & "IFsBaseHist.Run", Session("PutFdrvRun") & "working.run")
                Else
                    FileCopy(Session("PutFdrvRun") & "IFsBase.Run", Session("PutFdrvRun") & "working.run")
                End If
                'JRS 10/16/02 SAM COPY
                FileCopy(PutDataFileDir & "SAMBase.mdb", Session("PutFdrvRun") & "SAMWorking.mdb")

                'JRSVBNET ProcessTimeSeries((0)) 'icount%
                'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
                'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
                'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Run.aspx")
                'frm_Run.Show() 
                Session("Scenarioflag") = False
            Else
                msg = "Do you want to see and/or edit the scenario tree and working scenario?"
                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'jrs 2005/11/04 WinFormsToWeb_MessageBox.Show(msg, MsgBoxStyle.YesNo, "Run-Scenario")
                If True Then 'jrs 2005/11/04 If MsgBoxResult.Yes Then
                    'JRS 8/30/01 GUI 'JRSVBNET IFsGUIctrl1.ScenarioCreate
                    'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                    'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
                    'frm_ScenarioCreate.Show() 
                Else
                    msg = "We will proceed to the run form without a working scenario  "
                    msg = msg & "from the tree, but with whatever custom "
                    msg = msg & "parameter changes you have made."
                    'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                    'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Run.aspx")
                    'frm_Run.Show() 
                End If
            End If
        Else
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Run.aspx")
            'frm_Run.Show() 
        End If
        'JRS 8/23/01 END

    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub SocialAccountingMatrix_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SocialAccountingMatrix.Popup
    'JRSVBNET SocialAccountingMatrix_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub SocialAccountingMatrix_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2011/05/04
        Dim sPar As String
        If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
            Session("MMCountry") = "AF"
        End If
        If Session("MMCountry") = Nothing Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_SAM.aspx?" & PrepareForURL(sPar))

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub Standard_Click()
        Dim GridFull As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

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

        Session("COUNTE") = 0

        GridFull = "False"

        'UPGRADE_WARNING: Couldn't resolve default property of object LongFileOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("LongFileOn") = False
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "CHANGE.aspx")
        'frm_change.Show() 
        'Modal
        'JRS 8/30/01 GUI 'JRSVBNET IFsGUIctrl1.Change

    End Sub

    Private Sub Table_Click()

    End Sub


    '=============================MainMenu.Frm===============================
    Private Sub StatisticsACrossCountries_Click()
        Dim frm_ScatVarS As Object
        Dim ScatGraphTyp As Object
        Dim ScatGraphStyl As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphStyl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ScatGraphStyl = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphTyp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ScatGraphTyp = 9
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphStyl. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphStyle") = ScatGraphStyl
        'UPGRADE_WARNING: Couldn't resolve default property of object ScatGraphTyp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("GraphType") = ScatGraphTyp
        'UPGRADE_WARNING: Couldn't resolve default property of object frm_ScatVarS.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        frm_ScatVarS.Show()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub Store_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRSVBNET CMDialog1.FileName = ""
        'JRSVBNET CMDialog1.DialogTitle = "Store scenario file (abbreviated)"

        '   CancelError is True
        '   -------------------

        On Error GoTo ErrHandler3

        'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        'JRSVBNET CMDialog1.Filter = " Store scenario files (*.sru)|*.sru"
        'JRSVBNET CMDialog1.Action = 2
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRSVBNET StoreFile(('JRSVBNET CMDialog1.FileName))
        'JRSVBNET WorkingFile = 'JRSVBNET CMDialog1.FileName
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

ErrHandler3:  ' User Pressed Cancel
        Exit Sub


    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub TablFunc_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRS 8/8/01 GUI frm_TablFunc.Show
        'JRSVBNET IFsGUIctrl1.TablFunc()

    End Sub

    '=============================MainMenu.Frm===============================
    Private Sub VariableChange_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '     frm_VarChg.Show


    End Sub

    '=============================MainMenu.Frm===============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'JRSVBNET Public Sub mnuEarliestData_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEarliestData.Popup
    'JRSVBNET mnuEarliestData_Click(eventSender, eventArgs)
    'JRSVBNET End Sub
    Public Overrides Sub mnuEarliestData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2009/06/16
        'Changes on this date where due to SeriesInfMort not showing MostRecent value
        'I realized that looping 1 year at a time, for the number of total fields
        'didn't work when table has just a few fields and the years are not consecutive
        'Also noticed that SeriesKyotoSignStatusNonNum was empty because the values are not numeric
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If txtConfirm.Value = 0 Then Exit Sub

        Dim tdef As DataTable
        Dim TableName$, TCount%
        Dim RangeofYears%
        Dim ReadEarliest As Single?
        Dim FCount%, TotFields%, ThisYear%, LatestYear%, ReadYear$, BaseYear4Tbl%, MinDiff%, MinDiffYear%

        'jrs 2021/07/21
        'If Not Session("bGuidedBaseYearChange") Then
        '    Dim msg$ = "This process will add a Most Recent column to all Series of the Historical "
        '    msg$ = msg$ + " data file that do not have such a column.  Do you wish to continue?"
        '    Response = MsgBox(msg$, 4)
        '    If Response <> 6 Then Exit Sub
        'End If

        RangeofYears = 20

'CEDPRIINT(ICount%) = ReadEarliest(EDPriIntTbl, "1995", 5)
'Dim Definition$

'Dim DbMemo As Database
'Dim ValTblMemo As Recordset

Dim db As SqliteConnection
        db = New SqliteConnection("DataSource=" & PutFdrv$ + "DATA\IFSHistSeries.db;")
        db.Open()
        Dim vSQLiteCommand As SqliteCommand
Dim vSQLiteReader As SqliteDataReader
        Dim dtSchema As DataTable
        dtSchema = New DataTable
        vSQLiteCommand = db.CreateCommand()

        'jrs 2022/12/13
        vSQLiteCommand.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
        vSQLiteCommand.ExecuteNonQuery()

        'Dim TotalTables = vSQLiteReader.RecordsAffected

        Dim rsDD As DataTable
        'SetDataTablefromSQLite(PutFdrv$ + "DATA\DataDict.db", "SELECT * FROM DataDict", "", rsDD)
        SetDataTablefromSQLite(PutFdrv$ + "DATA\IFSHistSeries.db", "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'", "", rsDD)

        'With frm_Progress
        '    '.ZOrder 0
        '    .Caption = "Processing History Series (recent). Please Wait..."
        '    .ProgressBar1.MIN = 1
        '    .ProgressBar1.Max = TotalTables
        '    .ProgressBar1.value = .ProgressBar1.MIN
        'End With
        'frm_Progress.Show 0 'JRS 8/25/01 GUI , frm_ScenarioCreate
        TCount% = 0
        'jrs 2009/06/16
        Dim bText As Boolean

        While (TCount% < rsDD.Rows.Count)
            TableName = rsDD.Rows(TCount%).Item("TABLE_NAME")
            'jrs 2009/06/16
            bText = False
            'If tdef.Attributes = dbSystemObject  Then
            If UCase(Mid(TableName, 1, 6)) = "SERIES" Then

                vSQLiteCommand.CommandText = "SELECT * FROM [" & TableName & "]"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                dtSchema = vSQLiteReader.GetSchemaTable

                TotFields% = vSQLiteReader.FieldCount
                LatestYear% = 100
                'jrs 2009/06/16
                Dim EarliestYear% = 3000

                For FCount% = 0 To TotFields - 1
                    'JRS 06/24/03
                    If Not dtSchema.Rows(FCount%).Item(0).Contains("-") Then
                        ThisYear% = Val(dtSchema.Rows(FCount%).Item(0))
                    End If
                    'jrs 2009/06/16
                    If ThisYear% < EarliestYear% And ThisYear% > 100 Then
                        EarliestYear% = ThisYear%
                        'jrs 2024/04/04 changed from (13) to (24)
                        'jrs 2009/06/16
                        bText = (dtSchema.Rows(FCount%).Item(24) = "VARCHAR")
                    End If
                    'jrs 2009/02/19 If ThisYear% > LatestYear% And ThisYear% < 3000 Then
                    'there are variables with forecast saved in the series (SmokingImpact, DiabetesPrev)
                    'forecast shouldn't be saved as most recent
                    If ThisYear% > LatestYear% And ThisYear% <= AMAX(Year(Now()), FirstYearForecast) Then 'jrs 2016/01/13 added AMAX(xxx, BaseYear)
                        LatestYear% = ThisYear%
                    End If

                Next FCount%

                'If LatestYear% = 0 Then Loop
                'On Error Resume Next
                Dim bAdd As Boolean
                bAdd = (dtSchema.Select("ColumnName = 'Earliest'").Count = 0)
                'tdef.Fields.Append tdef.CreateField("Country", dbText)
                'JRS 06/24/03
                If LatestYear% > 100 Then 'valid year columns
                    'jrs 2009/06/16
                    If bAdd Then
                        Dim vSQLiteCommand2 As SqliteCommand
                        vSQLiteCommand2 = db.CreateCommand()

                        If Not bText Then
                            vSQLiteCommand2.CommandText = "ALTER TABLE " & TableName & " ADD COLUMN ""MostRecent"" DOUBLE(53);"
                        Else
                            vSQLiteCommand2.CommandText = "ALTER TABLE " & TableName & " ADD COLUMN ""MostRecent"" VARCHAR(255);"
                        End If
                        vSQLiteCommand2.ExecuteReader()

                    End If

                    'Set valtbl = db.OpenRecordset(TableName)

                    'Using trn = db.BeginTransaction()
                    'Using stmt = db.CreateCommand()
                    Dim stmt = db.CreateCommand()
                    stmt.CommandText = "UPDATE [" & TableName & "] SET [Earliest] = $Earliest WHERE Country = $Country;"
                    'jrs 2024/03/27 AddParameter(stmt, "$Earliest", SqliteType.Real)
                    AddParameter(stmt, "$Earliest", DbType.String)
                    AddParameter(stmt, "$Country", DbType.String)

                    'valtbl.MoveFirst

                    'For each country
                    Do While vSQLiteReader.Read
                        ReadEarliest = Nothing
                        If Not IsDBNull(vSQLiteReader((EarliestYear%).ToString.Trim)) AndAlso IsNumeric(vSQLiteReader((EarliestYear%).ToString.Trim)) Then
                            ReadEarliest = CSng(vSQLiteReader((EarliestYear%).ToString.Trim))
                        End If

                        'jrs 2009/06/16 For i2 = 1 To TotFields 'jrs 2007/05/03 RangeofYears
                        For i2 = EarliestYear% To LatestYear%

                            If Not ReadEarliest.HasValue Then
                                'jrs 2009/06/16 ReadEarliest = valtbl.Fields(Trim(Str(LatestYear% - i2)))
                                If dtSchema.Select("ColumnName = '" & (i2).ToString & "'").Count > 0 AndAlso IsNumeric(vSQLiteReader((i2).ToString.Trim)) AndAlso Not (vSQLiteReader((i2).ToString.Trim) = Nothing) Then
                                    ReadEarliest = CSng(vSQLiteReader((i2).ToString.Trim))
                                End If
                            End If


                            If ReadEarliest.HasValue Then
                                'Dim vSQLiteCommand3 As SqliteCommand
                                'vSQLiteCommand3 = db.CreateCommand()

                                'vSQLiteCommand3.CommandText = "UPDATE " & TableName & " SET MostRecent = " & ReadEarliest & " WHERE Country = """ & vSQLiteReader("Country") & """;"
                                'vSQLiteCommand3.ExecuteReader()
                                'jrs 2024/03/27 added toString to avoid extra decimals
                                stmt.Parameters(0).Value = ReadEarliest.ToString
                                'jrs 2023/06/06 stmt.Parameters(1).Value = vSQLiteReader("Country")
                                'wow, sometimes the table is defined as country with lower case, and that produces a blow up, like with SeriesForecastFertilityConstUNPD 
                                stmt.Parameters(1).Value = vSQLiteReader(0)
                                stmt.ExecuteNonQuery()
                                Exit For
                            End If

                        Next i2
                    Loop 'Next country

                    'trn.Commit()
                    'JRS 06/24/03End If
                    'End Using
                    'End Using 'Begin Transaction
                End If 'LatestYear% > 100 Then 'valid year columns

                vSQLiteReader.Close()

            End If 'Series

            TCount% = TCount% + 1
            'frm_Progress.ProgressBar1.value = TCount%
            'frm_Progress.Refresh

        End While

        'Unload frm_Progress
        db.Close()

        'jrs 2023/02/11
        'Copy File to RUNFILES
        Dim InFile = DataFileDir$ + "ifsHistSeries.db"
        Dim OutFile As String = Session("PutFdrvRun") + "ifsHistSeries.db"
        FileCopyCheck(InFile$, OutFile$)
    End Sub

    'jrs 2023/06/06
    Public Overrides Sub mnuMostRecentData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2009/06/16
        'Changes on this date where due to SeriesInfMort not showing MostRecent value
        'I realized that looping 1 year at a time, for the number of total fields
        'didn't work when table has just a few fields and the years are not consecutive
        'Also noticed that SeriesKyotoSignStatusNonNum was empty because the values are not numeric
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If txtConfirm.Value = 0 Then Exit Sub

        Dim tdef As DataTable
        Dim TableName$, TCount%
        Dim RangeofYears%
        Dim ReadRecent As Single?
        Dim FCount%, TotFields%, ThisYear%, LatestYear%, ReadYear$, BaseYear4Tbl%, MinDiff%, MinDiffYear%

        'jrs 2021/07/21
        'If Not Session("bGuidedBaseYearChange") Then
        '    Dim msg$ = "This process will add a Most Recent column to all Series of the Historical "
        '    msg$ = msg$ + " data file that do not have such a column.  Do you wish to continue?"
        '    Response = MsgBox(msg$, 4)
        '    If Response <> 6 Then Exit Sub
        'End If

        RangeofYears = 10

        'CEDPRIINT(ICount%) = ReadRecent(EDPriIntTbl, "1995", 5)
        'Dim Definition$

        'Dim DbMemo As Database
        'Dim ValTblMemo As Recordset

        Dim db As SqliteConnection
db = New SqliteConnection("DataSource=" & PutFdrv$ + "DATA\IFSHistSeries.db;")
db.Open()

        Dim vSQLiteCommand As SqliteCommand
Dim vSQLiteReader As SqliteDataReader
Dim dtSchema As DataTable
        dtSchema = New DataTable

        vSQLiteCommand = db.CreateCommand()

        'jrs 2022/12/13
        vSQLiteCommand.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
        vSQLiteCommand.ExecuteNonQuery()

        'Dim TotalTables = vSQLiteReader.RecordsAffected

        Dim rsDD As DataTable
        'SetDataTablefromSQLite(PutFdrv$ + "DATA\DataDict.db", "SELECT * FROM DataDict", "", rsDD)
        SetDataTablefromSQLite(PutFdrv$ + "DATA\IFSHistSeries.db", "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'", "", rsDD)

        'With frm_Progress
        '    '.ZOrder 0
        '    .Caption = "Processing History Series (recent). Please Wait..."
        '    .ProgressBar1.MIN = 1
        '    .ProgressBar1.Max = TotalTables
        '    .ProgressBar1.value = .ProgressBar1.MIN
        'End With
        'frm_Progress.Show 0 'JRS 8/25/01 GUI , frm_ScenarioCreate
        TCount% = 0
        'jrs 2009/06/16
        Dim bText As Boolean

        While (TCount% < rsDD.Rows.Count)
            TableName = rsDD.Rows(TCount%).Item("TABLE_NAME")
            'jrs 2009/06/16
            bText = False
            'If tdef.Attributes = dbSystemObject  Then
            If UCase(Mid(TableName, 1, 6)) = "SERIES" Then

                vSQLiteCommand.CommandText = "SELECT * FROM [" & TableName & "]"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                dtSchema = vSQLiteReader.GetSchemaTable

                TotFields% = vSQLiteReader.FieldCount
                LatestYear% = 100
                'jrs 2009/06/16
                Dim EarliestYear% = 3000

                For FCount% = 0 To TotFields - 1
                    'JRS 06/24/03
                    If Not dtSchema.Rows(FCount%).Item(0).Contains("-") Then
                        ThisYear% = Val(dtSchema.Rows(FCount%).Item(0))
                    End If
                    'jrs 2009/02/19 If ThisYear% > LatestYear% And ThisYear% < 3000 Then
                    'there are variables with forecast saved in the series (SmokingImpact, DiabetesPrev)
                    'forecast shouldn't be saved as most recent
                    If ThisYear% > LatestYear% And ThisYear% <= AMAX(Year(Now()), FirstYearForecast) Then 'jrs 2016/01/13 added AMAX(xxx, BaseYear)
                        LatestYear% = ThisYear%
                        'jrs 2024/04/04 changed from (13) to (24)
                        'jrs 2009/06/16
                        bText = (dtSchema.Rows(FCount%).Item(24) = "VARCHAR")
                    End If
                    'jrs 2009/06/16
                    If ThisYear% < EarliestYear% And ThisYear% > 100 Then
                        EarliestYear% = ThisYear%
                    End If

                Next FCount%

                'If LatestYear% = 0 Then Loop
                'On Error Resume Next
                Dim bAdd As Boolean
                bAdd = (dtSchema.Select("ColumnName = 'MostRecent'").Count = 0)
                'tdef.Fields.Append tdef.CreateField("Country", dbText)
                'JRS 06/24/03
                If LatestYear% > 100 Then 'valid year columns
                    'jrs 2009/06/16
                    If bAdd Then
                        Dim vSQLiteCommand2 As SqliteCommand
                        vSQLiteCommand2 = db.CreateCommand()

                        If Not bText Then
                            vSQLiteCommand2.CommandText = "ALTER TABLE " & TableName & " ADD COLUMN ""MostRecent"" DOUBLE(53);"
                        Else
                            vSQLiteCommand2.CommandText = "ALTER TABLE " & TableName & " ADD COLUMN ""MostRecent"" VARCHAR(255);"
                        End If
                        vSQLiteCommand2.ExecuteReader()

                    End If

                    'Set valtbl = db.OpenRecordset(TableName)

                    'Using trn = db.BeginTransaction()
                    'Using stmt = db.CreateCommand()
                    Dim stmt = db.CreateCommand()
                    stmt.CommandText = "UPDATE [" & TableName & "] SET [MostRecent] = $MostRecent WHERE Country = $Country;"
                    'jrs 2024/03/27 AddParameter(stmt, "$MostRecent", SqliteType.Real)
                    AddParameter(stmt, "$MostRecent", DbType.String)
                    AddParameter(stmt, "$Country", DbType.String)

                    'valtbl.MoveFirst

                    'For each country
                    Do While vSQLiteReader.Read
                        ReadRecent = Nothing
                        If Not IsDBNull(vSQLiteReader((LatestYear).ToString.Trim)) AndAlso IsNumeric(vSQLiteReader((LatestYear).ToString.Trim)) Then
                            ReadRecent = CSng(vSQLiteReader((LatestYear).ToString.Trim))
                        End If

                        'jrs 2009/06/16 For i2 = 1 To TotFields 'jrs 2007/05/03 RangeofYears
                        For i2 = 1 To LatestYear% - EarliestYear% + 1

                            If Not ReadRecent.HasValue And LatestYear% - i2 >= EarliestYear% Then
                                'jrs 2009/06/16 ReadRecent = valtbl.Fields(Trim(Str(LatestYear% - i2)))
                                If dtSchema.Select("ColumnName = '" & (LatestYear% - i2).ToString & "'").Count > 0 AndAlso IsNumeric(vSQLiteReader((LatestYear% - i2).ToString.Trim)) AndAlso Not (vSQLiteReader((LatestYear% - i2).ToString.Trim) = Nothing) Then
                                    ReadRecent = CSng(vSQLiteReader((LatestYear% - i2).ToString.Trim))
                                End If
                            End If


                            If ReadRecent.HasValue Then
                                'Dim vSQLiteCommand3 As SqliteCommand
                                'vSQLiteCommand3 = db.CreateCommand()

                                'vSQLiteCommand3.CommandText = "UPDATE " & TableName & " SET MostRecent = " & ReadRecent & " WHERE Country = """ & vSQLiteReader("Country") & """;"
                                'vSQLiteCommand3.ExecuteReader()
                                'jrs 2024/03/27 added .ToString to avoid extra decimals
                                stmt.Parameters(0).Value = ReadRecent.ToString
                                'jrs 2023/06/06 stmt.Parameters(1).Value = vSQLiteReader("Country")
                                'wow, sometimes the table is defined as country with lower case, and that produces a blow up, like with SeriesForecastFertilityConstUNPD 
                                stmt.Parameters(1).Value = vSQLiteReader(0)
                                stmt.ExecuteNonQuery()
                                Exit For
                            End If

                        Next i2
                    Loop 'Next country

                    'trn.Commit()
                    'JRS 06/24/03End If
                    'End Using
                    'End Using 'Begin Transaction
                End If 'LatestYear% > 100 Then 'valid year columns

                vSQLiteReader.Close()

            End If 'Series

            TCount% = TCount% + 1
            'frm_Progress.ProgressBar1.value = TCount%
            'frm_Progress.Refresh

        End While

        'Unload frm_Progress
        db.Close()

        'jrs 2023/02/11
        'Copy File to RUNFILES
        Dim InFile = DataFileDir$ + "ifsHistSeries.db"
        Dim OutFile As String = Session("PutFdrvRun") + "ifsHistSeries.db"
        FileCopyCheck(InFile$, OutFile$)
    End Sub


    '=============================MainMenu.Frm===============================
    'JRSVBNET *** BEGIN ***
    'Sub AddRow2VarLink(ByRef rstTemp As DAOJR.Recordset, ByRef strVar As String, ByRef strUsesVar As String)
    '    Dim i2 As Object
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    'mti Documentation - whole proc
    '    Dim vbcode As Object
    '    Dim strVarFound As Boolean
    '    Dim strUsesVarFound As Boolean

    '    'mti dec 18 '02 - take care to check duplication

    '    'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    vbcode = ""
    '    'check for vcln>0
    '    If VCLn = UVCLn Then
    '        'Driver Code Changes June 2003
    '        'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        vbcode = vbcode + CodeArray(VCLn) + "<br>"
    '    Else
    '        For i2 = VCLn To UVCLn + 1 Step -1
    '            'Driver Code Changes June 2003
    '            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            vbcode = vbcode + CodeArray(i2) + "<br>"
    '        Next i2
    '    End If

    '    ' Adds a new record to a Recordset using the data passed
    '    ' by the calling procedure. The new record is then made
    '    ' the current record.

    '    If strVar <> strUsesVar Then

    '        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        For i2 = 1 To NumbVars
    '            'mti 23 jan, 2003
    '            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            If UCase(V(i2)) = UCase(strVar) Then
    '                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                strVar = V(i2)
    '                strVarFound = True
    '                Exit For
    '            End If
    '            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            If "S" & UCase(V(i2)) = UCase(strVar) Then
    '                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                strVar = "S" & V(i2)
    '                strVarFound = True
    '                Exit For
    '            End If
    '        Next i2

    '        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        For i2 = 1 To NumbVars
    '            'mti 23 jan, 2003
    '            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            If UCase(V(i2)) = UCase(strUsesVar) Then
    '                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                strUsesVar = V(i2)
    '                strUsesVarFound = True
    '                Exit For
    '            End If
    '            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            If "S" & UCase(V(i2)) = UCase(strUsesVar) Then
    '                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                strUsesVar = "S" & V(i2)
    '                strUsesVarFound = True
    '                Exit For
    '            End If
    '        Next i2

    '        If strVarFound And strUsesVarFound Then
    '            With rstTemp
    '                .AddNew()
    '                .Fields("Variable").Value = strVar
    '                .Fields("UsesVariable").Value = strUsesVar
    '                'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                .Fields("Code").Value = vbcode
    '                .Update()
    '                .Bookmark = VB6.CopyArray(.LastModified)
    '            End With
    '            UVCLn = VCLn
    '        End If
    '    End If

    'End Sub '=============================MainMenu.Frm===============================
    'JRSVBNET *** END ***

    '=============================MainMenu.Frm===============================
    '=============================MainMenu.Frm===============================
    ''mti webforms June 2004 Mainmenu- comment out build documentation 
    '    Private Sub ParseCodeFile(ByRef Filename As String, ByRef ModName As String)
    '        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '        'Build Documentation 11/11/2003: replace this procedure
    '        'Driver Code Changes June 2003 - whole proc

    '        On Error GoTo closefile
    '        Dim strcode, tstrcode As String
    '        Dim cBlock As Object
    '        Dim pos, ACnt As Short
    '        Dim ISVBStructure As Boolean 'Structure: IF..THEN, FOR.NEXT etc
    '        Dim i As Short
    '        Dim StartFor(10) As Short
    '        Dim StartIf(10) As Short
    '        Dim StartDo(10) As Short
    '        Dim StartWhile(10) As Short
    '        Dim StartSelect(10) As Short
    '        Dim StartRoutine As Short

    '        Dim Added2CBlock As Short

    '        'keeps track of nesting within structures
    '        Dim WhileNest, IFNest, ForNest, DoNest, SelectNest As Short

    '        Dim pos1, pos2 As Short

    '        Dim TChar As Short
    '        Dim IFStarting As Boolean

    '        For i = 0 To 10
    '            StartFor(i) = 0
    '            StartIf(i) = 0
    '            StartDo(i) = 0
    '            StartWhile(i) = 0
    '            StartSelect(i) = 0
    '        Next i
    '        StartRoutine = 0

    '        ForNest = 0
    '        IFNest = 0
    '        DoNest = 0
    '        WhileNest = 0
    '        SelectNest = 0

    '        'ACnt is the counter for the main array where parsed code is taken
    '        'Leftside() of the eqn., Rightside() of the eqn., and the corresponding Codeblock()
    '        ACnt = codelines

    '        FileOpen(1, Filename, OpenMode.Input)

    '        'If InStr(Filename, "SOCIOPOL.bas") <> 0 Then Stop

    '        'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        cBlock = "" 'holds code block temporarily as it is built


    '        Do While Not EOF(1)
    '            strcode = LineInput(1)

    '            'If Trim(strcode) = "If IY% <> 1 Then POP(R%) = POP(R%) + agedst(j%, R%, 1) + agedst(j%, R%, 2)" Then Stop

    '            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            cBlock = cBlock + "<br>" + strcode
    '            Added2CBlock = Len(strcode) + 1
    '            'Added2CBlock this is used for the length of the latest string
    '            'in case it needs to be subtracted from the block

    '            Do While VB.Right(strcode, 1) = "_" 'handle line continuation in VB code
    '                strcode = Mid(strcode, 1, Len(strcode) - 1)
    '                tstrcode = LineInput(1)
    '                strcode = strcode & Trim(tstrcode)

    '                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                cBlock = cBlock + "<br>" + tstrcode
    '                Added2CBlock = Added2CBlock + Len(tstrcode) + 1
    '            Loop

    '            If strcode <> "" Then '
    '                pos = InStr(UCase(Trim(strcode)), " ")
    '                If pos > 1 Then
    '                    Select Case Mid(UCase(Trim(strcode)), 1, pos - 1)
    '                        Case "PRIVATE", "PUBLIC", "SUB", "FUNCTION", "STATIC"
    '                            ISVBStructure = True
    '                            StartRoutine = ACnt + 1
    '                        Case "IF"

    '                            IFStarting = False
    '                            pos1 = InStr(Trim(strcode), "Then")

    '                            'see if IF.THEN structure is completed in the same line
    '                            If Len(Trim(strcode)) > pos1 + 4 Then
    '                                TChar = Asc(Mid(Trim(strcode), pos1 + 5, 1))
    '                                If TChar = 32 Or TChar = 39 Then '" " or "'"
    '                                    IFStarting = True
    '                                End If
    '                            Else
    '                                IFStarting = True
    '                            End If
    '                            If IFStarting Then
    '                                ISVBStructure = True
    '                                IFNest = IFNest + 1
    '                                StartIf(IFNest) = ACnt + 1
    '                            Else 'same line If.Then
    '                                ISVBStructure = False
    '                            End If


    '                        Case "FOR"

    '                            ISVBStructure = True
    '                            ForNest = ForNest + 1
    '                            StartFor(ForNest) = ACnt + 1
    '                        Case "SELECT"
    '                            'add code with current block

    '                            ISVBStructure = True
    '                            SelectNest = SelectNest + 1
    '                            StartSelect(SelectNest) = ACnt + 1


    '                        Case "DO"

    '                            ISVBStructure = True
    '                            DoNest = DoNest + 1
    '                            StartDo(DoNest) = ACnt + 1


    '                        Case "WHILE"

    '                            ISVBStructure = True
    '                            WhileNest = WhileNest + 1
    '                            StartWhile(WhileNest) = ACnt + 1


    '                        Case "ELSE", "ELSEIF", "CASE" 'continue taking structure

    '                            ISVBStructure = True
    '                            'below are ends of structures


    '                        Case "END"
    '                            If Mid(UCase(Trim(strcode)), 1, 6) = "END IF" Then
    '                                HandleStructure(ACnt, StartIf(IFNest), cBlock, strcode, Added2CBlock)
    '                                IFNest = IFNest - 1
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                cBlock = ""
    '                                ISVBStructure = True
    '                            ElseIf Mid(UCase(Trim(strcode)), 1, 10) = "END SELECT" Then
    '                                HandleStructure(ACnt, StartSelect(SelectNest), cBlock, strcode, Added2CBlock)
    '                                SelectNest = SelectNest - 1
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                cBlock = ""
    '                                ISVBStructure = True
    '                            Else 'end sub, end function etc.
    '                                'proc/function completed
    '                                HandleStructure(ACnt, StartRoutine, cBlock, strcode, Added2CBlock)
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                cBlock = ""
    '                                ISVBStructure = True

    '                            End If
    '                        Case "NEXT" 'for.next
    '                            HandleStructure(ACnt, StartFor(ForNest), cBlock, strcode, Added2CBlock)
    '                            ForNest = ForNest - 1
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            cBlock = ""
    '                            ISVBStructure = True


    '                        Case "LOOP" 'do.loop

    '                            HandleStructure(ACnt, StartDo(DoNest), cBlock, strcode, Added2CBlock)
    '                            DoNest = DoNest - 1
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            cBlock = ""
    '                            ISVBStructure = True


    '                        Case "WEND" 'while.wend

    '                            HandleStructure(ACnt, StartWhile(WhileNest), cBlock, strcode, Added2CBlock)
    '                            WhileNest = WhileNest - 1
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            cBlock = ""
    '                            ISVBStructure = True
    '                        Case Else
    '                            ISVBStructure = False
    '                    End Select
    '                End If
    '                If Not ISVBStructure Then
    '                    If Mid(Trim(strcode), 1, 1) <> "'" Then 'omit comment line
    '                        If InStr(strcode, "Call XYTABL(") > 0 Then
    '                            'parse call xytbl function
    '                            'Call XYTABL(Title$, GDPPCP(R%), carscomp)
    '                            '                       right, left
    '                            'new block
    '                            ACnt = ACnt + 1
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            CodeBlock(ACnt) = cBlock 'strcode
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            cBlock = ""

    '                            'check for trailing comment
    '                            pos = InStr(Trim(strcode), "'")
    '                            If pos > 0 Then
    '                                strcode = Mid(strcode, 1, pos - 1)
    '                            End If

    '                            pos = 0 'InStr(Trim(strcode), ")")
    '                            pos1 = 0
    '                            Do While True
    '                                pos = InStr(pos + 1, strcode, ",")
    '                                If pos = 0 Then
    '                                    Exit Do
    '                                Else
    '                                    pos2 = pos1
    '                                    pos1 = pos
    '                                End If
    '                            Loop

    '                            LeftSide(ACnt) = Mid(strcode, pos1 + 1)
    '                            'cut down the last ")"
    '                            LeftSide(ACnt) = Mid(LeftSide(ACnt), 1, Len(LeftSide(ACnt)) - 1)

    '                            Rightside(ACnt) = Trim(Mid(strcode, pos2 + 1, pos1 - pos2 - 1))

    '                            'mti 07 Feb 03
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object ModuleName(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            ModuleName(ACnt) = ModName
    '                            'Build Documentation 11/11/2003
    '                        ElseIf InStr(strcode, "'JRSVBNET Call ADJSTR(") > 0 Then
    '                            'parse call ADJSTR function
    '                            ''JRSVBNET Call ADJSTR(wstbase, Diff1, Diff2, elenpr, elenpr2, mulwst)
    '                            '---------------------------------right-------------, left(mul)
    '                            'new block
    '                            ACnt = ACnt + 1
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            CodeBlock(ACnt) = cBlock 'strcode
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            cBlock = ""

    '                            'check for trailing comment
    '                            pos = InStr(Trim(strcode), "'")
    '                            If pos > 0 Then
    '                                strcode = Mid(strcode, 1, pos - 1)
    '                            End If

    '                            ''JRSVBNET Call ADJSTR(wstbase, Diff1, Diff2, elenpr, elenpr2, mulwst)
    '                            pos = InStr(strcode, "(") '0
    '                            pos1 = InStr(strcode, "(") '0
    '                            Rightside(ACnt) = ""
    '                            Do While True
    '                                pos = InStr(pos + 1, strcode, ",")
    '                                If pos = 0 Then
    '                                    Exit Do
    '                                Else
    '                                    pos2 = pos1
    '                                    pos1 = pos

    '                                    Rightside(ACnt) = Rightside(ACnt) & Mid(strcode, pos2 + 1, pos1 - pos2) '+ " + " 'add a dummy operator

    '                                End If
    '                            Loop

    '                            LeftSide(ACnt) = Mid(strcode, pos1 + 1)
    '                            'cut down the last ")"
    '                            LeftSide(ACnt) = Mid(LeftSide(ACnt), 1, Len(LeftSide(ACnt)) - 1)

    '                            'cut down the last dummy "," '" + " operator
    '                            Rightside(ACnt) = Mid(Rightside(ACnt), 1, Len(Rightside(ACnt)) - 1)

    '                            'mti 07 Feb 03
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object ModuleName(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            ModuleName(ACnt) = ModName
    '                        ElseIf InStr(strcode, "Call AnalFunc(") > 0 Then
    '                            'parse Call AnalFunc function
    '                            'Call AnalFunc("World Bank Loans as % of Recipient GDP", _
    '                            ''    Array(GDPPCP(R%), GINIDOM(R%), AIDPERGDP(R%)), Prediction)
    '                            '---------------------------------right-------------, left(mul)
    '                            'new block
    '                            ACnt = ACnt + 1
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            CodeBlock(ACnt) = cBlock 'strcode
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            cBlock = ""

    '                            'check for trailing comment
    '                            pos = InStr(Trim(strcode), "'")
    '                            If pos > 0 Then
    '                                strcode = Mid(strcode, 1, pos - 1)
    '                            End If

    '                            'Call AnalFunc("World Bank Loans as % of Recipient GDP", _
    '                            ''    Array(GDPPCP(R%), GINIDOM(R%), AIDPERGDP(R%)), Prediction)
    '                            pos = InStr(strcode, """,") + 1 'search for ",  '0
    '                            pos1 = InStr(strcode, """,") + 1 '0
    '                            Rightside(ACnt) = ""
    '                            Do While True
    '                                pos = InStr(pos + 1, strcode, ",")
    '                                If pos = 0 Then
    '                                    Exit Do
    '                                Else
    '                                    pos2 = pos1
    '                                    pos1 = pos

    '                                    Rightside(ACnt) = Rightside(ACnt) & Mid(strcode, pos2 + 1, pos1 - pos2) '+ " + " 'add a dummy operator

    '                                End If
    '                            Loop

    '                            LeftSide(ACnt) = Mid(strcode, pos1 + 1)
    '                            'cut down the last ")"
    '                            LeftSide(ACnt) = Mid(LeftSide(ACnt), 1, Len(LeftSide(ACnt)) - 1)

    '                            'cut down the last dummy "," operator
    '                            Rightside(ACnt) = Mid(Rightside(ACnt), 1, Len(Rightside(ACnt)) - 1)

    '                            'mti 07 Feb 03
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object ModuleName(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                            ModuleName(ACnt) = ModName
    '                            'End Build Documentation 11/11/2003
    '                        ElseIf Mid(UCase(Trim(strcode)), 1, 3) = "IF " Then
    '                            'take care of single line IF.Then
    '                            pos1 = InStr(strcode, "Then")
    '                            pos1 = pos1 + 5 'eqn starts here
    '                            pos = InStr(pos1, strcode, " = ")
    '                            If pos > 0 Then
    '                                ACnt = ACnt + 1
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                CodeBlock(ACnt) = cBlock 'strcode
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                cBlock = ""
    '                                LeftSide(ACnt) = Mid(strcode, pos1, pos - pos1)
    '                                Rightside(ACnt) = Mid(strcode, pos + 3)

    '                                'check for trailing comment
    '                                pos = InStr(Trim(Rightside(ACnt)), "'")
    '                                If pos > 0 Then
    '                                    Rightside(ACnt) = Mid(Rightside(ACnt), 1, pos - 1)
    '                                End If


    '                                'UPGRADE_WARNING: Couldn't resolve default property of object ModuleName(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                ModuleName(ACnt) = ModName

    '                            End If
    '                        Else 'all other cases (non structure)


    '                            pos = InStr(strcode, " = ")
    '                            If pos > 0 Then
    '                                ACnt = ACnt + 1
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                CodeBlock(ACnt) = cBlock 'strcode
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                cBlock = ""

    '                                LeftSide(ACnt) = VB.Left(strcode, pos - 1)
    '                                Rightside(ACnt) = Mid(strcode, pos + 3)

    '                                'check for trailing comment
    '                                pos = InStr(Trim(Rightside(ACnt)), "'")
    '                                If pos > 0 Then
    '                                    Rightside(ACnt) = Mid(Rightside(ACnt), 1, pos - 1)
    '                                End If

    '                                'mti 07 Feb 03
    '                                'UPGRADE_WARNING: Couldn't resolve default property of object ModuleName(ACnt). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                                ModuleName(ACnt) = ModName

    '                            End If 'pos > 0
    '                        End If 'InStr(strcode, "Call XYTABL(") = 1
    '                    Else ''Mid(strcode, 1, 1) <> "'" comment
    '                        'comment line keeps on adding up
    '                    End If 'Mid(strcode, 1, 1) <> "'"
    '                End If 'not isvbStructure


    '            End If 'strcode <> ""

    '        Loop
    '        codelines = ACnt ' + 1
    'closefile:
    '        FileClose(1)

    '        'this helps debug

    '        'Dim FN$, spos%
    '        '
    '        'FN$ = Mid(Filename, Len(PutFdrv$ + "Code\") + 1, 5) + ".txt"
    '        '
    '        'Open "C:\My Documents\" + FN$ For Output As #2
    '        'For i2 = 206 To 696 'replace these
    '        'Print #2, "==== " + Trim(Str(i2)) + " =========================="
    '        'Print #2, LeftSide(i2) + " = " + Rightside(i2)
    '        'Print #2, CodeBlock(i2)
    '        'Next
    '        'Close #2

    '    End Sub
    'Function cVarnames(ByRef strExpression As String) As Collection
    '    'mti Documentation - whole proc
    '    Dim thisVar As String
    '    Dim i As Integer
    '    Dim varStart As Integer
    '    Dim varWord As Object
    '    Dim res As String

    '    cVarnames = New Collection

    '    ' ignore duplicate words - or duplication of collection keys
    '    On Error Resume Next

    '    ' extract all words from the text
    '    For i = 1 To Len(strExpression)
    '        Select Case Asc(Mid(strExpression, i, 1))
    '            Case 65 To 90, 97 To 122
    '                ' variable always start with an alpha char
    '                If varStart = 0 Then varStart = i
    '            Case 48 To 57, 95 '95="_"
    '                ' digits can be suffix of a Varname
    '            Case Else
    '                If varStart Then
    '                    ' variable name will be extracted now,
    '                    'automatically exclude array index etc
    '                    thisVar = UCase(Mid(strExpression, varStart, i - varStart))
    '                    'check if vb keyword
    '                    If Not IsKeyWord(thisVar) Then
    '                        ' add to collection, ignore if already there
    '                        cVarnames.Add(thisVar, thisVar)
    '                    End If
    '                    ' reset varstart
    '                    varStart = 0
    '                End If
    '        End Select
    '    Next

    '    ' account for the last var, if any
    '    If varStart Then
    '        ' extract the word
    '        thisVar = UCase(Mid(strExpression, varStart, i - varStart))
    '        If Not IsKeyWord(thisVar) Then
    '            ' add to collection, ignore if already there
    '            cVarnames.Add(thisVar, thisVar)
    '        End If
    '    End If

    'End Function

    ''mti Documentation -whole proc
    'Public Function IsKeyWord(ByVal txt As String) As Boolean
    '    Dim i As Integer

    '    For i = 0 To vbKWCount
    '        If txt = vbKW(i) Then IsKeyWord = True : Exit Function
    '    Next i
    'End Function
    ''mti Documentation - whole proc
    '' loads the keywordlist at the start of the program
    'Public Sub GetKeyWordList()
    '    Dim ch As Integer
    '    Dim txt As String
    '    Dim file As String
    '    Dim k As New VB6.FixedLengthString(1)
    '    Dim Part As String
    '    Dim P, i As Integer
    '    ReDim vbKW(10)

    '    file = VB6.GetPath & "\VBKeyW.txt"
    '    ch = FreeFile()
    '    FileOpen(ch, file, OpenMode.Input)
    '    txt = InputString(ch, FileLen(file))
    '    FileClose(ch)

    '    For P = 1 To Len(txt)
    '        k.Value = Mid(txt, P, 1)
    '        Select Case k.Value
    '            Case ","
    '                If Part <> "" Then
    '                    vbKW(i) = UCase(Part)
    '                    If i = vbKWCount Then vbKWCount = vbKWCount + 10 : ReDim Preserve vbKW(vbKWCount)
    '                    i = i + 1
    '                    Part = ""
    '                End If
    '            Case Is < " "
    '            Case Else
    '                Part = Part & k.Value
    '        End Select
    '    Next P
    '    vbKWCount = i
    'End Sub


    ''=============================MainMenu.Frm===============================
    ''CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    ''JRSVBNET Public Sub mnudocumentation_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuDocumentation.Popup
    ''JRSVBNET mnudocumentation_Click(eventSender, eventArgs)
    ''JRSVBNET End Sub
    'Public Sub mnudocumentation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    'JRSVBNET *** BEGIN ***
    '    '        Dim MatchIFSVar As Object
    '    '        Dim i2 As Object
    '    '        Dim ICount As Short
    '    '        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    '        'Driver Code Changes June 2003 - take whole proc

    '    '        On Error GoTo restorepointer

    '    '        Dim TmpArr() As Double
    '    '        Dim TStruct(20) As String
    '    '        Dim TStructNo(20) As Short
    '    '        Dim UnfinishedNo As Short
    '    '        Dim TCBlock As Object

    '    '        Dim TNo As Short

    '    '        Dim TStr As String
    '    '        Dim cbpointer As Short
    '    '        Dim AddCode As Boolean

    '    '        Dim qsql As String

    '    '        Dim TmpVar As String

    '    '        Dim CurModule As String

    '    '        Dim MStart, MEnd As Short

    '    '        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    '    '        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor

    '    '        Dim varfound(500) As String



    '    '        Dim vbcode As Object

    '    '        Dim pos, VFCounter, pos1 As Short
    '    '        Dim LeftVar, strcode, RightVar As String
    '    '        Dim CodeFileSubDir, CodeFileDir, Filename As String

    '    '        Dim db1 As DAOJR.Database
    '    '        Dim db2 As DAOJR.Database
    '    '        Dim valtbl As DAOJR.Recordset
    '    '        Dim valtbl1 As DAOJR.Recordset

    '    '        Dim txtcnt, k2, j2, l2, RightCnt As Short

    '    '        Dim DownLimit, UpLimit, m2 As Short

    '    '        Dim cVarNames1 As Collection
    '    '        'Dim cVarNames2 As Collection
    '    '        Dim ModCount As Short
    '    '        'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '        System.Array.Clear(ModName, 0, ModName.Length)

    '    '        'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '        System.Array.Clear(ModStart, 0, ModStart.Length)
    '    '        'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '        System.Array.Clear(ModEnd, 0, ModEnd.Length)
    '    '        'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '        System.Array.Clear(CodeBlock, 0, CodeBlock.Length)
    '    '        'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '        System.Array.Clear(CodeArray, 0, CodeArray.Length)
    '    '        'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '        System.Array.Clear(CodeArray2Block, 0, CodeArray2Block.Length)




    '    '        CodeFileSubDir = "Code"
    '    '        CodeFileDir = PutFdrv & CodeFileSubDir
    '    '        'Filename = CodeFileDir + "\" + "modelrun.txt"
    '    '        'ParseCodeFile (Filename) 'add file into array

    '    '        GetKeyWordList()
    '    '        codelines = 0

    '    '        '----------parse the model code and take them in one array----
    '    '        '----Leftside() of eqn., Rightside() of eqn., corresponding codeblock()--

    '    '        ModCount = 0
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModName(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModName(ModCount) = "AUTOMOBILE.BAS"
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModStart(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModStart(ModCount) = codelines + 1
    '    '        Filename = CodeFileDir & "\" & "AUTOMOBILE.bas"
    '    '        Call ParseCodeFile(Filename, "AUTOMOBILE.BAS") 'add file into array
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModEnd(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModEnd(ModCount) = codelines
    '    '        ModCount = ModCount + 1

    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModName(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModName(ModCount) = "ENVIRONMENT.BAS"
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModStart(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModStart(ModCount) = codelines + 1
    '    '        Filename = CodeFileDir & "\" & "ENVIRONMENT.bas"
    '    '        Call ParseCodeFile(Filename, "ENVIRONMENT.BAS") 'add file into array
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModEnd(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModEnd(ModCount) = codelines
    '    '        ModCount = ModCount + 1


    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModName(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModName(ModCount) = "SOCIOPOL.BAS"
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModStart(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModStart(ModCount) = codelines + 1
    '    '        Filename = CodeFileDir & "\" & "SOCIOPOL.bas"
    '    '        Call ParseCodeFile(Filename, "SOCIOPOL.BAS") 'add file into array
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModEnd(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModEnd(ModCount) = codelines
    '    '        ModCount = ModCount + 1

    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModName(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModName(ModCount) = "ENERGY.BAS"
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModStart(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModStart(ModCount) = codelines + 1
    '    '        Filename = CodeFileDir & "\" & "energy.bas"
    '    '        Call ParseCodeFile(Filename, "ENERGY.BAS") 'add file into array
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModEnd(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModEnd(ModCount) = codelines
    '    '        ModCount = ModCount + 1

    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModName(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModName(ModCount) = "AGRICUL.BAS"
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModStart(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModStart(ModCount) = codelines + 1
    '    '        Filename = CodeFileDir & "\" & "agricul.bas"
    '    '        Call ParseCodeFile(Filename, "AGRICUL.BAS") 'add file into array
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModEnd(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModEnd(ModCount) = codelines
    '    '        ModCount = ModCount + 1

    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModName(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModName(ModCount) = "ECONOMY.BAS"
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModStart(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModStart(ModCount) = codelines + 1
    '    '        Filename = CodeFileDir & "\" & "ECONOMY.bas"
    '    '        Call ParseCodeFile(Filename, "ECONOMY.BAS") 'add file into array
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModEnd(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModEnd(ModCount) = codelines
    '    '        ModCount = ModCount + 1

    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModName(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModName(ModCount) = "POPULAT.BAS"
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModStart(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModStart(ModCount) = codelines + 1
    '    '        Filename = CodeFileDir & "\" & "populat.bas"
    '    '        Call ParseCodeFile(Filename, "POPULAT.BAS") 'add file into array
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object ModEnd(ModCount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        ModEnd(ModCount) = codelines
    '    '        ModCount = ModCount + 1




    '    '        db1 = DAODBEngine_definst.OpenDatabase(PutFdrvRun & "IFsVar.mdb")
    '    '        'Set valtbl = db1.OpenRecordset("VarLink")
    '    '        valtbl1 = db1.OpenRecordset("IFSVar")
    '    '        'recreate varlink table
    '    '        'JRSVBNET Call InitVarLink(db1, valtbl1)

    '    '        valtbl1.Index = "NAME"

    '    '        valtbl = db1.OpenRecordset("VarLink")

    '    '        VFCounter = 0

    '    '        CodeArray2Block(0) = 0

    '    '        '--------Look for IFSVar variables in the array built above by parsecodefile(s)
    '    '        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '        For ICount = 1 To NumbVars * 2
    '    '            If ICount Mod 2 = 1 Then
    '    '                LeftVar = UCase(V(ICount \ 2 + 1))
    '    '            Else
    '    '                LeftVar = "S" & UCase(V(ICount / 2))
    '    '            End If


    '    '            'If LeftVar$ = "POP" Then Stop

    '    '            VCLn = 0
    '    '            UVCLn = 0
    '    '            'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '            System.Array.Clear(CodeArray, 0, CodeArray.Length)
    '    '            'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '            System.Array.Clear(CodeArray2Block, 0, CodeArray2Block.Length)
    '    '            'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '            System.Array.Clear(Tempright, 0, Tempright.Length)
    '    '            'Main loop for codelines
    '    '            CurModule = ""

    '    '            MEnd = codelines
    '    '            MStart = 1

    '    '            'start searching all members of the array (built from parsecode)
    '    '            For i2 = MEnd To MStart Step -1  'UBound(LeftSide) - 1

    '    '                '    For i2 = codelines To 1 Step -1 'UBound(LeftSide) - 1
    '    '                '     If ModuleName(i2) <> CurModule And CurModule <> "" Then
    '    '                '        Exit For
    '    '                '     End If

    '    '                'should not terminate this loop even if ifsvar found
    '    '                'even if dimensions done separately

    '    '                'LeftVar$ = UCase$(V$(ICount%))
    '    '                'If MatchIFSVarInString(LeftSide(i2), LeftVar$) Then

    '    '                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                If True Then 'JRSVBNET MatchIFSVarInString(LeftSide(i2), LeftVar) Then
    '    '                    'IFSVar variable founds in a left hand side of eqn.

    '    '                    'If UCase(LeftVar$) = "POWER" Then Stop

    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object ModuleName(i2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    CurModule = ModuleName(i2)

    '    '                    For k2 = 0 To ModCount - 1
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object ModName(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        If Trim(ModName(k2)) = Trim(CurModule) Then
    '    '                            'UPGRADE_WARNING: Couldn't resolve default property of object ModStart(k2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                            MStart = ModStart(k2)
    '    '                            'UPGRADE_WARNING: Couldn't resolve default property of object ModEnd(k2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                            MEnd = ModEnd(k2)
    '    '                            Exit For
    '    '                        End If
    '    '                    Next k2
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    strcode = Rightside(i2)


    '    '                    'take relevant codeblock (and block pointer in a sorted array)
    '    '                    TakeSorted(Int(CDbl(i2)), VCLn) 'routine increases VCLn
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(i2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    CodeArray(VCLn) = CodeBlock(i2) 'LeftSide(i2) + " = " + Rightside(i2)


    '    '                    'collect individual variable names from the Rightside of the eqn.
    '    '                    cVarNames1 = cVarnames(strcode)


    '    '                    RightCnt = 0

    '    '                    'populate a temporary array with rightside variables for subsequent search
    '    '                    For j2 = 0 To cVarNames1.Count() - 1
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object cVarNames1.Item(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        TmpVar = cVarNames1.Item(j2 + 1)
    '    '                        'If Mid(TmpVar, 1, 6) = "AGEDST" Then Stop
    '    '                        If TmpVar <> LeftVar Then '?? OK
    '    '                            'UPGRADE_WARNING: Couldn't resolve default property of object Tempright(RightCnt, 0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                            Tempright(RightCnt, 0) = TmpVar
    '    '                            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                            'UPGRADE_WARNING: Couldn't resolve default property of object Tempright(RightCnt, 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                            Tempright(RightCnt, 1) = i2 'pointer to current code line
    '    '                            RightCnt = RightCnt + 1
    '    '                        End If
    '    '                    Next j2
    '    '                    'RightCnt = j2
    '    '                    l2 = 0
    '    '                    Do While True
    '    '                        'handle collections
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object Tempright(l2, 0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        RightVar = Tempright(l2, 0)

    '    '                        If RightVar = "" Then Exit Do 'tempright() exhausted

    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object MatchIFSVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        MatchIFSVar = False
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        For k2 = 1 To NumbVars
    '    '                            'mti 23 jan, 2003
    '    '                            If UCase(V(k2)) = UCase(RightVar) Or "S" & UCase(V(k2)) = UCase(RightVar) Then
    '    '                                'JRSVBNET AddRow2VarLink(valtbl, LeftVar, RightVar)
    '    '                                'UVCLn = VCLn
    '    '                                'UPGRADE_WARNING: Couldn't resolve default property of object MatchIFSVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                                MatchIFSVar = True
    '    '                                Exit For
    '    '                            End If
    '    '                        Next k2
    '    '                        If Not MatchIFSVar Then
    '    '                            'Rightside Variable is not a IFSVar variable: it is an intermediate variable
    '    '                            'search for this intermediate variable from current line backwards
    '    '                            'to the beginning of this module
    '    '                            'UPGRADE_WARNING: Couldn't resolve default property of object Tempright(l2, 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                            For k2 = Tempright(l2, 1) - 1 To MStart Step -1  'MEnd

    '    '                                If True Then 'JRSVBNET MatchIFSVarInString(LeftSide(k2), RightVar) Then

    '    '                                    'intermediate variable found in leftside: take codeblock
    '    '                                    TakeSorted(k2, VCLn) 'routine increases VCLn
    '    '                                    'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(k2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                                    CodeArray(VCLn) = CodeBlock(k2) 'LeftSide(k2) + " = " + Rightside(k2)
    '    '                                    strcode = Rightside(k2)
    '    '                                    cVarNames1 = cVarnames(strcode)
    '    '                                    'If cVarNames1.Count =0 Then do nothing

    '    '                                    UpLimit = RightCnt + cVarNames1.Count() - 1
    '    '                                    DownLimit = RightCnt '+ 1
    '    '                                    For j2 = DownLimit To UpLimit
    '    '                                        'UPGRADE_WARNING: Couldn't resolve default property of object cVarNames1.Item(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                                        TmpVar = cVarNames1.Item(j2 - DownLimit + 1)
    '    '                                        'check if intermediate/ifs variable already listed
    '    '                                        For m2 = 0 To j2 - 1
    '    '                                            'If Mid(TmpVar, 1, 10) = "LIFEXPTERM" Then Stop
    '    '                                            If TmpVar <> RightVar Then 'takes care of x=x+1, includes again
    '    '                                                'rightvar is an intermediate var
    '    '                                                'UPGRADE_WARNING: Couldn't resolve default property of object Tempright(m2, 0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                                                If TmpVar = Tempright(m2, 0) Then
    '    '                                                    TmpVar = ""
    '    '                                                    Exit For
    '    '                                                End If
    '    '                                            Else
    '    '                                                Exit For
    '    '                                            End If
    '    '                                        Next m2
    '    '                                        If Len(TmpVar) > 0 Then
    '    '                                            'UPGRADE_WARNING: Couldn't resolve default property of object Tempright(RightCnt, 0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                                            Tempright(RightCnt, 0) = TmpVar 'cVarNames1.Item(j2 - (DownLimit - 1))
    '    '                                            'UPGRADE_WARNING: Couldn't resolve default property of object Tempright(RightCnt, 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                                            Tempright(RightCnt, 1) = k2
    '    '                                            RightCnt = RightCnt + 1
    '    '                                        End If
    '    '                                    Next j2

    '    '                                    'problem here - if x=x+1, see above
    '    '                                    Exit For 'added 01/04/03 : For k2 = Tempright(l2, 1) To 1 Step -1
    '    '                                    'RightCnt = j2
    '    '                                End If 'MatchIFSVarInString(LeftSide(k2), RightVar)
    '    '                            Next k2
    '    '                        End If 'Not MatchIFSVar
    '    '                        l2 = l2 + 1
    '    '                    Loop

    '    '                End If 'MatchIFSVarInString(LeftSide(i2)
    '    '                'do not exit for loop here, will miss 2nd of the 2nd sub dim(e.g., crop vs meat)

    '    '            Next i2


    '    '            'Driver Code Changes June 2003

    '    '            'Write Code for driver: code is displayed at 'View Code' Menu

    '    '            CodeArray(VCLn + 1) = "" 'insert a dummy blank, for loop convenience
    '    '            CodeArray2Block(VCLn + 1) = 5001
    '    '            'CodeArray(VCLn + 1) = 5001 'dummy upper limit
    '    '            'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '            vbcode = ""

    '    '            'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '            vbcode = vbcode + "'----Module " + CurModule + " ---------" + "<br>" + "<br>"


    '    '            'The following are the arrays used to complete unfinished structures
    '    '            'unfinished structures arise when structure > 10 lines
    '    '            'a marker saying complete structure at the designated line is added to the startblock
    '    '            'please see handlestructure routine
    '    '            'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '            System.Array.Clear(TStructNo, 0, TStructNo.Length)
    '    '            'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    '    '            System.Array.Clear(TStruct, 0, TStruct.Length)

    '    '            CodeArray2Block(0) = 0
    '    '            UnfinishedNo = 0
    '    '            For i2 = 1 To VCLn ' To 1 Step -1
    '    '                AddCode = True
    '    '                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(CodeArray2Block(i2 - 1)). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(CodeArray2Block(i2)). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                If CodeBlock(CodeArray2Block(i2)) = CodeBlock(CodeArray2Block(i2 - 1)) Then
    '    '                    AddCode = False
    '    '                End If
    '    '                If AddCode Then
    '    '                    'COMPLETE STRUCTURE:    Next tar%:477|COMPLETE STRUCTURE:        Next act%:477|


    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(CodeArray2Block()). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object TCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    TCBlock = CodeBlock(CodeArray2Block(i2))

    '    '                    pos1 = 0
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object TCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    pos = InStr(TCBlock, "COMPLETE STRUCTURE:")

    '    '                    Do While pos > 0
    '    '                        UnfinishedNo = UnfinishedNo + 1
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object TCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        pos = InStr(pos + 1, TCBlock, ":")
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object TCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        pos1 = InStr(pos + 1, TCBlock, ":")
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object TCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        TStruct(UnfinishedNo) = Mid(TCBlock, pos + 1, pos1 - pos - 1)
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object TCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        pos = InStr(pos1, TCBlock, "|") '"|"=End of marker
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object TCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        TStructNo(UnfinishedNo) = Val(Trim(Mid(TCBlock, pos1 + 1, pos - pos1 - 1)))


    '    '                        pos1 = pos 'save end of marker

    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object TCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        pos = InStr(pos + 1, TCBlock, "COMPLETE STRUCTURE:")
    '    '                    Loop


    '    '                    'discard complete structure marker , if any
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                    vbcode = vbcode + Mid(CodeBlock(CodeArray2Block(i2)), pos1 + 1) + "<br>" '+ "<br>"
    '    '                    Do While UnfinishedNo > 0
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        If CodeArray2Block(i2) >= TStructNo(UnfinishedNo) Then
    '    '                            'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                            vbcode = vbcode + "<br>" + TStruct(UnfinishedNo)
    '    '                            UnfinishedNo = UnfinishedNo - 1
    '    '                        Else
    '    '                            Exit Do
    '    '                        End If
    '    '                    Loop
    '    '                End If

    '    '            Next i2

    '    '            Do While UnfinishedNo > 0
    '    '                'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                vbcode = vbcode + "<br>" + TStruct(UnfinishedNo)
    '    '                UnfinishedNo = UnfinishedNo - 1
    '    '            Loop

    '    '            'Add the Code to the Access Table
    '    '            If VCLn > 0 Then

    '    '                With valtbl1

    '    '                    .MoveFirst()
    '    '                    .Seek("=", LeftVar)
    '    '                    If Not .NoMatch Then
    '    '                        .Edit()
    '    '                        'UPGRADE_WARNING: Couldn't resolve default property of object vbcode. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    '                        .Fields("vbcode").Value = vbcode
    '    '                        .Update()
    '    '                    End If

    '    '                End With

    '    '            End If


    '    '        Next ICount

    '    '        'valtbl1.Update
    '    '        valtbl.Close()
    '    '        valtbl1.Close()
    '    '        db1.Close()
    '    '        'UPGRADE_NOTE: Object db1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
    '    '        db1 = Nothing

    '    '        'msgbox here

    '    '        '    InFile$ = DataFileDir$ + "IFsVar.mdb"
    '    '        '    OutFile$ = PutFdrvRun$ + "IFsVar.mdb"

    '    '        db1 = DAODBEngine_definst.OpenDatabase(DataFileDir & "IFsVar.mdb")
    '    '        valtbl1 = db1.OpenRecordset("IFSVar")
    '    '        'recreate varlink table
    '    '        'JRSVBNET Call InitVarLink(db1, valtbl1)
    '    '        'Set valtbl = db1.OpenRecordset("VarLink")

    '    '        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    '    '        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    '    '        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
    '    '        If True Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("Copy documentation from work to data folder?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '    '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    '    '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor


    '    '            qsql = " INSERT INTO " & "VarLink" & " (Variable, UsesVariable, Code) " & "SELECT Variable, UsesVariable, Code From " & "VarLink In '" & PutFdrvRun & "IFsVar.mdb" & "';"

    '    '            db1.Execute(qsql) 'db = IfsHistSeries.mdb




    '    '            db2 = DAODBEngine_definst.OpenDatabase(PutFdrvRun & "IFsVar.mdb")
    '    '            'debug the following line: change db1 to db2
    '    '            valtbl = db2.OpenRecordset("IFSVar")


    '    '            With valtbl1
    '    '                .MoveFirst()
    '    '                valtbl.MoveFirst()
    '    '                Do While Not .EOF
    '    '                    'If UCase(valtbl.Fields("name").Value) = "AGDEM" Then Stop
    '    '                    .Edit()
    '    '                    .Fields("vbcode").Value = valtbl.Fields("VBCODE").Value
    '    '                    .Update()
    '    '                    .MoveNext()
    '    '                    valtbl.MoveNext()
    '    '                Loop
    '    '                '.Index = "NAME"
    '    '            End With

    '    '            valtbl.Close()
    '    '            db2.Close()

    '    '            'UPGRADE_NOTE: Object db2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
    '    '            db2 = Nothing
    '    '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    '    '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
    '    '        End If

    '    '        valtbl1.Close()
    '    '        db1.Close()
    '    '        'UPGRADE_NOTE: Object db1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
    '    '        db1 = Nothing

    '    '        Exit Sub
    '    'restorepointer:
    '    '        'Debug.Print Err.Description, Err.Number, Err.Source, Err.HelpFile
    '    '        'For l2 = 1 To 2999
    '    '        '   Debug.Print Tempright(l2, 0)
    '    '        'Next l2
    '    '        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    '    '        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    '        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
    '    'JRSVBNET *** END ***
    'End Sub
    'end mti webforms June 2004 Mainmenu  - comment documentation code
    'JRSVBNET *** BEGIN ***
    'Private Sub InitVarLink(ByRef db1 As DAOJR.Database, ByRef valtbl1 As DAOJR.Recordset)
    '    'recreate varlink table
    '    Dim tdef As DAOJR.TableDef

    '    For Each tdef In db1.TableDefs
    '        If UCase(tdef.Name) = UCase("VarLink") Then
    '            db1.TableDefs.Delete((tdef.Name))
    '            Exit For
    '        End If
    '    Next tdef

    '    tdef = db1.CreateTableDef("VarLink")

    '    With tdef
    '        .Fields.Append(tdef.CreateField("Variable", DAOJR.DataTypeEnum.dbText))
    '        .Fields.Append(tdef.CreateField("UsesVariable", DAOJR.DataTypeEnum.dbText))
    '        .Fields.Append(tdef.CreateField("Code", DAOJR.DataTypeEnum.dbMemo))
    '    End With

    '    db1.TableDefs.Append(tdef)


    '    'delete code lines from ifsvar table
    '    With valtbl1
    '        .MoveFirst()
    '        Do While Not .EOF
    '            .Edit()
    '            .Fields("vbcode").Value = " "
    '            .Update()
    '            .MoveNext()
    '        Loop

    '    End With

    'End Sub
    'JRSVBNET *** END ***

    '=============================MainMenu.Frm===============================
    'mti webforms June 2004 LongPlot - comment documentation code
    'Private Sub HandleStructure(ByRef EqnNo As Short, ByRef START As Short, ByRef cBlock As Object, ByRef strcode As String, ByRef LenLastAddition As Short)
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    'Driver Code Changes June 2003 - whole proc

    '    'Handles VB structures found in VB Code
    '    'merges everything in the block if structure less than certain length
    '    'otherwise discards end of structure and puts marker at the start of structure

    '    Dim i As Short
    '    Dim LastCBlock As Object
    '    Dim SaveCBlock As Object


    '    If EqnNo - START <= 10 Then
    '        'complete Structure if <= 10 lines

    '        'add last part of the structure
    '        'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        'UPGRADE_WARNING: Couldn't resolve default property of object SaveCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        SaveCBlock = cBlock
    '        'CodeBlock(EqnNo) = CodeBlock(EqnNo) + cBlock
    '        'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        cBlock = ""
    '        'merge structure in same block,
    '        'duplication taken care of during building vbcode
    '        'merge all blocks from the beginning to the end of structure
    '        'UPGRADE_WARNING: Couldn't resolve default property of object LastCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        LastCBlock = ""
    '        For i = START To EqnNo '- 1
    '            'UPGRADE_WARNING: Couldn't resolve default property of object LastCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            If CodeBlock(i) <> LastCBlock Then 'check duplicate blocks
    '                'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                cBlock = cBlock + CodeBlock(i)
    '                'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                'UPGRADE_WARNING: Couldn't resolve default property of object LastCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '                LastCBlock = CodeBlock(i)
    '            End If
    '        Next i
    '        'UPGRADE_WARNING: Couldn't resolve default property of object SaveCBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        cBlock = cBlock + SaveCBlock
    '        'assign (same) merged code block to each eqn within the structure
    '        For i = START To EqnNo '- 1
    '            'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            CodeBlock(i) = cBlock
    '        Next i
    '    Else

    '        'unfinished structures arise when structure > 10 lines
    '        'a marker saying complete structure at the designated line is added to the startblock
    '        'it is reparsed while taking VBCode for IFSVar variables

    '        'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        cBlock = Mid(cBlock, 1, Len(cBlock) - LenLastAddition)
    '        'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(EqnNo). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        CodeBlock(EqnNo) = CodeBlock(EqnNo) + cBlock
    '        'UPGRADE_WARNING: Couldn't resolve default property of object CodeBlock(START). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        CodeBlock(START) = "COMPLETE STRUCTURE:" & strcode & ":" & Trim(Str(EqnNo)) & "|" + CodeBlock(START)
    '        'UPGRADE_WARNING: Couldn't resolve default property of object cBlock. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '        cBlock = ""

    '    End If



    'End Sub
    ''=============================MainMenu.Frm===============================
    'Private Function TakeSorted(ByRef CurLineNo As Short, ByRef LinesTaken As Short) As Object
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    'Driver Code Changes June 2003 - whole proc

    '    'sorts the array CodeArray2Block() while populating it
    '    'array poulated as seek (and find) of an IFSVAR variable in the parsed VBcode goes on
    '    'array sorted so that VBcode is written in proper order

    '    Dim k2, j2, sline As Short

    '    sline = CurLineNo

    '    LinesTaken = LinesTaken + 1

    '    If LinesTaken = 1 Then
    '        CodeArray2Block(1) = CurLineNo
    '        Exit Function
    '    End If

    '    For j2 = 1 To LinesTaken - 1
    '        If CodeArray2Block(j2) > CurLineNo Then
    '            sline = CodeArray2Block(j2)
    '            CodeArray2Block(j2) = CurLineNo
    '            Exit For
    '        End If
    '    Next j2

    '    For k2 = LinesTaken To j2 + 2 Step -1
    '        CodeArray2Block(k2) = CodeArray2Block(k2 - 1)
    '    Next k2

    '    If j2 + 1 <= LinesTaken Then
    '        CodeArray2Block(j2 + 1) = sline
    '    End If
    'End Function
    'end mti webforms June 2004 LongPlot - comment documentation code



    'JRS 01/24/04
    Public Overrides Sub PrepareForm()
        ''mti webforms June 2004 Mainmenu - whole proc
        Dim msg As String
        'JRS 02/05/04
        Dim Width As Integer
        Dim Height As Integer
        '   Set screeen
        '   -----------

        'CONVERSION_ISSUE: Class  System.Windows.Forms.FormWindowState from  System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.WindowState.html'.
        'JRSVBNET WindowState = System.Windows.Forms.FormWindowState.Maximized
        'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.Width.html'.
        Width = VB6.Support.TwipsToPixelsX(Inch * 6)
        'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.Height.html'.
        Height = VB6.Support.TwipsToPixelsY(Inch * 3.4)
        'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.Top.html'.
        'CONVERSION_ISSUE: Class  System.Windows.Forms.Screen from  System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.Height.html'.
        'JRSVBNET Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2)
        'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.Left.html'.
        'CONVERSION_ISSUE: Class  System.Windows.Forms.Screen from  System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.Width.html'.
        'JRSVBNET Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Main Menu: " & ModelName
        'lblTitle.Text = "Main Menu: " & ModelName

        '   Set menu items
        '   --------------

        WelcomeMessage = CStr(False)
        'mti Documentation
        If CBool(DevelopmentMode) = True Then
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            'mnuDocumentation.Visible = True
        Else
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            'mnuDocumentation.Visible = False
        End If

        If ModelLoad <> "LoadStudent" Or CBool(DevelopmentMode) = True Then
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'EditCountryData.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'DisplayProcessedCountryData.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'DisplayRegionallyAggregatedData.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'ChangeRegionMembership.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'RebuildBase.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'RebuildHistoricBase.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'HistoricDataFileProcessing.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'ModelDevelopment.Visible = True
            '        ImportDatafromXLSFile.Visible = True
            '        frm_mnuEarliestData.Visible = True
            '        frm_mnuMostRecentData.Visible = True
            '        EliminateYearsData.Visible = True
            '        IdentifySaveYears.Visible = True
            '        AddDeleteCountries.Visible = True
            If PutFdrv = FDRV Then
                'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
                'ImportDatafromXLSFile.Visible = True
            Else 'Do not allow data import on network; files cannot be written
                'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
                'ImportDatafromXLSFile.Visible = False
            End If
        Else
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'EditCountryData.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'DisplayProcessedCountryData.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'DisplayRegionallyAggregatedData.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'ChangeRegionMembership.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'RebuildModelBase.Visible = False
            ''RebuildBase.Visible = False 'suboption
            ''RebuildHistoricBase.Visible = False 'suboption
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'ManageCountryData.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'ImportDatafromXLSFile.Visible = False
            ''HistoricDataFileProcessing.Visible = False
            ''Control doesn't exist for students
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'ModelDevelopment.Visible = True 'In all modes
            '        ImportDatafromXLSFile.Visible = False
            '        frm_mnuEarliestData.Visible = False
            '        frm_mnuMostRecentData.Visible = False
            '        IdentifySaveYears.Visible = False
            '        AddDeleteCountries.Visible = False
            '        EliminateYearsData.Visible = False
        End If
        If CBool(AbbCodeMode) = True Then
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'EditCountryData.Visible = False
            ''calls frm_EditRawData
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'DisplayProcessedCountryData.Visible = False
            ''calls frm_DispProC
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'ImportDatafromXLSFile.Visible = False
            ''calls frm_ImportXLSNew
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'CountryMap.Visible = False
            ''calls frm_Map
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'CountryMap2.Visible = False
            ''calls frm_Map2
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'AnalyzeAcrossCountries.Visible = False
            ''calls frm_ScatVarS
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'AnalyzeLongitudinal.Visible = False
            ''calls frm_LongVarS
            ''        ShowFunctions.Visible = False
            ''calls frm_TablFunc2

            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'RegionMembers.Visible = False
            ''calls frm_RegionMembers
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'DisplayRegionallyAggregatedData.Visible = False
            ''calls frm_DispProR
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'ChangeRegionMembership.Visible = False
            ''calls frm_ChgRegMb
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'RebuildBase.Visible = False
            ''calls frm_RebBase
            ''        RebuildHistoricBase.Visible = False
            ''calls frm_RebBase

            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'RebuildCommon.Visible = False
            ''calls frm_RebComm

            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'CountryData.Visible = False
        End If
        If CBool(DevelopmentMode) = True And CBool(AbbCodeMode) = False Then
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'RebuildCommon.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'mnuVariable.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'AddDeleteCountries.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'mnuEarliestData.Visible = True
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'mnuMostRecentData.Visible = True
            '        RebuildHistoricBase.Visible = True
        Else
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'RebuildCommon.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'mnuVariable.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'AddDeleteCountries.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'mnuEarliestData.Visible = False
            ''CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'mnuMostRecentData.Visible = False
            '        RebuildHistoricBase.Visible = False
        End If

        '   Set menu items
        '   --------------

        If CBool(ESRIMap) = True Then
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'CountryMap.Visible = True
            'JRS 8/6/01 GUI frm_DisplayMenu.Map.Visible = True
        Else
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'CountryMap.Visible = False
            'JRS 8/6/01 GUI frm_DisplayMenu.Map.Visible = False
        End If
        If CBool(BMMap) = True Then
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'CountryMap2.Visible = True
            'JRS 8/6/01 GUI frm_DisplayMenu.Map2.Visible = True
        Else
            'CONVERSION_ISSUE: Property ' System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb. System.Windows.Forms.Form.html'.
            'CountryMap2.Visible = False
            'JRS 8/6/01 GUI frm_DisplayMenu.Map2.Visible = False
        End If


        '   Display instructions
        '   --------------------

        Carret = "<br>" & Chr(10)
        carret2 = Carret + Carret

        'jrs 2008/07/29
        'Dim sTab As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"

        If CBool(WelcomeMessage) = True Then
            msg = "Welcome to International Futures (IFs)!" & carret2
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "IFs is a tool for thinking about the future, for helping you" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "     - explore large-scale, long-term global trends" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "         (you may also focus on a particular country or region)" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "     - develop alternative scenarios of global development" + Carret
            msg = msg & "     - investigate key relationships and dynamics of global change" & carret2
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "It will take you a while to learn all that IFs can do," + Carret
            msg = msg & "     but there is considerable help for you in the learning process." & carret2
            'msg = msg & "     Select the Help option or touch the F1 key at any time." & carret2
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "There are typical steps in model use:" + Carret
            msg = msg & "     1. Investigate the 'Base Case' (select DISPLAY "
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "and explore at length)" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "     2. Later, change model assumptions (select CHANGE)" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "     3. Then compute a forecast with new assumptions (select RUN)" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "     4. Compare the new forecast with 'Base Case' (select DISPLAY)" + Carret
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            msg = msg & "     5. Save forecast or retrieve earlier one (select FILE)" + Carret
            msg = msg & "     6. Evaluate your findings" & carret2
            msg = msg & "     You may wish to use the Guided Use option to take you through "
            msg = msg & "such steps." & carret2
            msg = msg & "Again, welcome.  Please enjoy your investigation of international futures!"

        Else
            'msg = "Welcome to the Main Menu of International Futures (IFs)." & carret2
            'msg = msg & "The typical modes of use of the IFs modeling system are:" & carret2
            ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'msg = msg & "     1. Investigate alternative States of the World" + Carret
            'msg = msg & "           (select DISPLAY "
            'msg = msg & "to explore full, already-run scenarios)" & carret2
            ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'msg = msg & "     2. Undertake scenario analysis to create Alternative Futures" + Carret
            'msg = msg & "           (select SCENARIO ANALYSIS to generate your own scenarios)" & carret2
            ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'msg = msg & "     3. Explore extensive historic data to see trends and relationships" + Carret
            'msg = msg & "           (select COUNTRY DATA ANALYSIS)" & carret2

            'msg = msg & "Remember that you can select the Help option or touch the F1 key at any time." & carret2
            'KIA 06-10-2008 MODIFICATION TO MAKE THE MAIN MENU FORM INDENTICAL TO DESKTOP VERSION
            msg = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If you wish primarily to explore possible futures of a particular country, click on that country. " &
            carret2 'jrs 2008/07/29 "(right-click for features that help you control the map, including zooming)." + carret2
            'jrs 2024/12/02 msg = msg & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To access the full functionality of IFs, including creating multi-country displays " &
            'jrs 2024/12/02 "in specific issue areas, building new scenarios, and analyzing historical data, use the menu options." + carret2
            'jrs 2024/12/02 msg = msg & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Remember that you can select the Help option at any time." 'jrs 2008/07/29 or touch the F1 key + carret2
            msg = msg & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sign up <a href=""https://forms.office.com/Pages/ResponsePage.aspx?id=N3A8b8KF5kCd7BiwLSiSiAabQPS0TfVGq22zkCelI51UMko4UFpVRk5BQTVRSTU5NExSV0JBV1BYUSQlQCN0PWcu&j=171916&sfmc_sub=40395859&l=1024_HTML&u=2430862&mid=546004464&jb=1&utm_source=&utm_medium=email&utm_campaign=&utm_content=https%3a%2f%2fforms.office.com%2fPages%2fResponsePage.aspx%3fid%3dN3A8b8KF5kCd7BiwLSiSiAabQPS0TfVGq22zkCelI51UMko4UFpVRk5BQTVRSTU5NExSV0JBV1BYUSQlQCN0PWcu&utm_id=40395859"">here</a> to receive our newsletter."
        End If

        lbl_scenanal.Text = msg

        WelcomeMessage = CStr(False)

        'JRSVBNET IFsGUIctrl1.Session("FirstYear") = Session("FirstYear") 'Default to forecast, not history
        'JRSVBNET IFsGUIctrl1.Session("LastYear") = Session("LastYear")
        'JRSVBNET IFsGUIctrl1.cActiveBase = ActiveBase
        'JRSVBNET IFsGUIctrl1.HistoricBase = HistoricBase
        'UPGRADE_WARNING: Couldn't resolve default property of object  IFsGUIctrl1.cRunFiles(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

    End Sub

    'JRS 03/10/04
    'JRS 03/05/04
    Overrides Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        'jrs 2011/05/04
        Dim sPar As String

        'jam 2022/07/07
        Dim randTabs As Integer
        Dim rnd As New Random()


        'jrs 2012/08/16
        Dim bMDGSkip As Boolean = False
        If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
            'jrs 2026/01/26 Session("MMCountry") = "KE" 'from Afghanistan to Kenya           
            Session("MMCountry") = "AF" 'back to Afghanistan 
            bMDGSkip = True
        End If
        If Session("MMCountry") = Nothing Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Exit"
                'JRS 2005/01/08 Response.End()
                Context.Session.Abandon()
                'jrs 2024/08/15
                'jrs 2025/05/14 Support.FormManager.Show(Me, "https://pardee.du.edu/access-ifs")
                'jrs 2026/08/07 Support.FormManager.Show(Me, "https://korbel.du.edu/pardee/download-ifs/")
                Support.FormManager.Show(Me, "https://korbel.du.edu/pardee/access-ifs/")
            Case "Basic Report for Countries/Regions or Groupings"
                Report_Click(sender, e)
            Case "Geographically-fixed Displays"
                Support.FormManager.Show(Me, "frm_PackagedDisplays.aspx")
            Case "Flexible Displays"
                'jam 2022/07/02
                'Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")


                If Not Session("mnuTabs") Then 'jam 2022/07/02
                    'jrs 2011/08/30 Support.FormManager.Show(Me, "frm_ProEssential.aspx")
                    Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
                Else 'jam 2022/07/02
                    randTabs = rnd.Next(1, 20)

                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow", " window.open('frm_PackagedDisplaysFlex.aspx','_newtab');", True)
                    'jam 2022/07/07
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "OpenWindow", " window.open('frm_PackagedDisplaysFlex.aspx','_newtab" + CStr(randTabs) + " '); ", True)

                End If




            Case "Education by Age, Sex and Level"
                'JAM 2020-09-27
                'Support.FormManager.Show(Me, "frm_EdnCohoDisp.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")
                Call ShowEdnCohoDisp(sPar)
            Case "Cost of Education Goals" 'jrs 2012/07/06
                Support.FormManager.Show(Me, "frm_EdnCostDisplay.aspx")
            Case "Population by Age and Sex"
                'CohortsofPopulation_Click(sender, e)
                'jam 2021/04/03
                Call ShowPopCohoDisp()
            Case "Birth Cohort Information"
                Support.FormManager.Show(Me, "frm_CohoInfDisp.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")
            Case "Mortality by Age, Sex and Cause" 'jrs 2008/09/16
                'Support.FormManager.Show(Me, "frm_MortCohorts.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")
                Call ShowMortCohoDisp(sPar) 'jam 2021/02/03
            Case "Morbidity by Age, Sex and Cause" 'jrs 2013/02/25
                'jam 2021/05/29
                'Support.FormManager.Show(Me, "frm_MorbCohorts.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")
                Call ShowMorbCohoDisp(sPar)
            Case "Mortality Display: J-Curve" 'jrs 2010/06/01
                Support.FormManager.Show(Me, "frm_MortDisplay.aspx")
            Case "Mortality and Disability Years" 'jrs 2012/08/03
                Support.FormManager.Show(Me, "frm_HealthMeassures.aspx")
            Case "World Value Survey by Age"
                CohortsofWorldValueSurvey_Click(sender, e)
            Case "Development Profile" 'jrs 2005/11/18
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DevelopmentProfile.aspx?" & PrepareForURL(sPar))
            Case "Growth Accounting" 'jrs 2020/06/06
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_GrowthAccounting.aspx")
            Case "World Map, Lorenz Curve, Gini, Histogram"
                mnuWorldVariables_Click(sender, e)
            Case "Social Accounting Matrix"
                SocialAccountingMatrix_Click(sender, e)
            Case "World Bank Financial Flows"
                WBFinancialFlows_Click(sender, e)
            Case "Advanced Sustainability Analysis" 'jrs 2012/08/15
                mnuASA_Click(sender, e)
            Case "Millennium Development Goals"
                If bMDGSkip Then
                    Support.FormManager.Show(Me, "frm_MDG.aspx")
                Else
                    Support.FormManager.Show(Me, "frm_MDG.aspx?" & PrepareForURL(sPar))
                End If
            Case "SDG Graph" 'jrs 2017/05/31
                If bMDGSkip Then
                    Support.FormManager.Show(Me, "frm_SDG.aspx")
                Else
                    Support.FormManager.Show(Me, "frm_SDG.aspx?" & PrepareForURL(sPar))
                End If
            Case "SDG Overview Table" 'jrs 2017/06/06
                If bMDGSkip Then
                    Support.FormManager.Show(Me, "frm_SDGset.aspx")
                Else
                    Support.FormManager.Show(Me, "frm_SDGset.aspx?" & PrepareForURL(sPar))
                End If
            Case "Poverty Level Display" 'jrs 2007/03/16
                Support.FormManager.Show(Me, "frm_PovertyDisplay.aspx")
            Case "Performance Risk Evaluation" 'jrs 2013/05/31
                Support.FormManager.Show(Me, "frm_PerfRiskAnalysis.aspx?" & PrepareForURL(sPar))
            Case "Advanced Sustainability Analysis"
                AdvancedSustainabilityAnalysis_Click(sender, e)
            Case "Self-Managed Display (Complete Flexibility)"
                SelfManagedDisplay_Click(sender, e)
            Case "Infrastructure Profile" 'JAM 2018-01-14
                Support.FormManager.Show(Me, "frm_ICTProfile.aspx?" & PrepareForURL(sPar))

            Case "Quick Scenario Analysis with Tree" 'JRS 08/20/04
                'jrs 2004/11/23
                SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
                Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
                Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
                Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
                Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
                Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
                Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))
                Session("QuickScenario") = True
                'jrs 2011/05/16
                Session("LoadSceDesc") = False

                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_ScenarioCreate.aspx")
            Case "Guided Scenario Analysis"
                GuidedUse_Click(sender, e)
                    'jrs 2005/12/02
                    'Case "Change Selected Functions"
                    '    Session("QuickScenario") = False
                    '    SelfManagedScenarios_Click(sender, e)
            Case "Bivariate Functions"
                SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
            Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
            Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
            Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
            Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
            Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
            Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_TablFunc.aspx")
            Case "Multivariate Functions"
                SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
            Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
            Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
            Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
            Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
            Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
            Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_AnalFunc.aspx")
            ' gvdb 2024/04/19
            'Case "Run" 'jrs 2005/07/08
            '    SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
            'Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
            'Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
            'Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
            'Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
            'Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
            'Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))
            '    Support.FormManager.StateStore(Me)
            '    Support.FormManager.Show(Me, "frm_Run.aspx")
                    'Case "OPEN run file as working file"
                    '    lblMode.Text = "Open File"
                    '    pnlFilePicker.Visible = True
                    'Case "SAVE working file as ..."
                    '    lblMode.Text = "Save As .."
                    '    pnlFilePicker.Visible = True
                    'Case "DELETE run file"
                    '    Delete_Click(sender, e)
                    'Case "SELECT file for display"
                    '    SELECT_Renamed_Click(sender, e)

            Case "Run" 'gvdb 2024/04/17
                SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
                    Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
                    Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
                    Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
                    Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
                    Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
                    Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Run.aspx")

            Case "Batch Run" 'jrs 2020/01/08
                mnuBatchRun_Click()
            Case "Compare VB6 vs .NET"
                RunCompareReportVb6vsNET()
            Case "Translate Base Case from VB6 To .NET" 'jrs 2022/05/19
                RunTranslateVb6toNET()
            Case "Create Code to Add Tables in .NET" 'jrs 2020/07/15
                CreateCodeToAddTableInNET()
            Case "Create Code to Regionalize Variables in .NET" 'jrs 2023/10/11
                CreateCode4Regionalization()
            Case "Create New Analytical Functions Using Constant USD$" 'jrs 2023/09/06
                ReEstimateFunctionsWithUSD(ConstantDollarsYear, True) 'jrs 2025/10/03
                Response.Write("<script>alert('Process Completed. New Functions only in RUNFILES.');</script>")
            Case "Standarize Legacy VarNames in run files" 'jrs 2025/10/03
                UpdateVarNamesInLegacyRunFiles(Session("PutFdrvRun2"))
                Response.Write("<script>alert('Process Completed. Updated VarNames in Legacy run files');</script>")
            Case "Create Report of Missing Groups in Scenarios" 'jrs 2025/05/19
                'Open Output File
                Dim Reportfile As Integer
                Reportfile = FreeFile()
                Dim sFileName As String
                sFileName = Session("ScenarioFileDir") & "MissingGroupsInSCEs.csv"
                FileOpen(Reportfile, sFileName, OpenMode.Output)

                Check4MissingGroups(Session("ScenarioFileDir"), Reportfile)
                'Close Output File
                FileClose(Reportfile)
            Case "Create Report of Spelling Errors in Flex Displays" 'jrs 2026/07/28
                SpellingErrorsInFlex(Session("PutFdrvRun2"))
                Response.Write("<script>alert('Process Completed. Spelling Errors Saved in IFsVar.db');</script>")
            Case "Merge Migration series from UNPD and Abel" 'jrs 2023/05/29
                If Dir(DataFileDir$ + "IFsHistSeriesDyadic.db") > "" Then
                    Dim sErrMsg As String = ""
                    generateMergedMigrationSeriesAbelPlusUNPD(Session("IFs_MDB2Active"), sErrMsg, Session("PutFdrvRun"), Session("IFsHistSeries_MDB"))
                    If sErrMsg > "" Then
                        Response.Write("<script>alert('" & sErrMsg & "');</script>")
                    End If
                End If
            Case "Add or Delete Countries" 'jrs 2023/06/13
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_AddDelRawD.aspx")
            Case "Recreate Scenario File" 'jrs 2023/07/03
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_RecreateSce.aspx")
            Case "Clear Session" 'jrs 2023/08/22
                Session.Abandon()
            Case "World Map"
                CountryMap2_Click(sender, e)
            Case "Analyze Across Countries (Cross-Sectional)"
                AnalyzeAcrossCountries_Click(sender, e)
            Case "Show Computed Functions"
                ShowFunctions_Click(sender, e)
            Case "Bilateral History and Forecast" 'jrs 2020/07/06
                BilateralInteractionDisplayWGravity_Click(sender, e)
            Case "Analyze Across Time (Longitudinal)"
                AnalyzeLongitudinal_Click(sender, e)
            Case "Edit Country Data"
                EditCountryData_Click(sender, e)
            Case "Display Processed Country Data"
                DisplayProcessedCountryData_Click(sender, e)
            Case "Import Data from XLS File"
                ImportDatafromXLSFile_Click(sender, e)
            Case "Earliest Data, Add or Update"
                mnuEarliestData_Click(sender, e)
            Case "Most Recent Data, Add or Update"
                mnuMostRecentData_Click(sender, e)
            Case "Remove Years from Data"
                EliminateYearsData_Click(sender, e)
            Case "Identify and Save Years"
                IdentifySaveYears_Click(sender, e)
            Case "Add or Delete Countries"
                AddDeleteCountries_Click(sender, e)
            Case "Identify Group or Country/Region Members"
                RegionMembers_Click(sender, e)
            Case "Edit Group or Country/Region Members"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_ChangeRegisteredMembers.aspx")
            Case "Change Regionalization/Grouping"
                ChangeRegionMembership_Click(sender, e)
            Case "Display Regionally Aggregated Data"
                DisplayRegionallyAggregatedData_Click(sender, e)
            Case "Provide Feedback"
                Register_Click(sender, e)
                'jrs 2026/04/27 Case "Contents", "Contents (Web)" 'jrs 2023/10/11 added Contents (Web)
            Case "Help System", "Help System (Web)" 'jrs 2023/10/11 added Contents (Web)
                Contents_Click(sender, e)
            Case "About This Software"
                AboutThisSoftware_Click(sender, e)
            Case "Reload Base"
                mnuReload_Click()
            Case "Clear all Sessions" 'jrs 2008/11/25
                ClearAllSessions()
            Case "Javascript Variables Generation" 'JAM 2014-06-10
                'ReadFormControlCaption(Me, Session("PutFdrvRun"))
                JavaScriptGeneration()
            Case "Use Data from SQLite" 'JAM 2019-10-08
                mnuSQLiteData(sender, e)

                If Session("mnuSQLite") Then
                    objSender.Groups("Help").Items("Use Data from SQLite").IconImage = "checkbox.gif"

                Else
                    objSender.Groups("Help").Items("Use Data from SQLite").IconImage = "blank.gif"
                End If
            Case "Open New Tabs for Selected options" 'JAM 2022/07/02
                mnuOpenTabs(sender, e)
                'jam 2022/07/04
                If Session("mnuTabs") Then
                    objSender.Groups("Help").Items("Open New Tabs for Selected options").IconImage = "checkbox.gif"

                Else
                    objSender.Groups("Help").Items("Open New Tabs for Selected options").IconImage = "blank.gif"
                End If

            Case "Any Map" 'jrs 2011/07/19
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_AnyMap.aspx")
            Case "SAVE working file as ..." 'jrs 2020/01/07
                lblJS.Visible = False
                lbl_scenanal.Visible = False
                frm_FileName.Visible = True

                If Session("Open_File") > "" Then
                    txt_FileName.Text = Session("Open_File")
                ElseIf Session("Saved_file") > "" Then 'jrs 2023/07/04
                    txt_FileName.Text = Session("Saved_file")
                End If
            Case "Rebuild Base" 'jrs 2019/12/26
                Session("HistoricBase") = "False"
                Session("ActiveBase") = "IFSBASE.RUN"
                Session("WorkingFile") = "IFSBASE.RUN.DB"
                Session("RunFiles")(1) = Session("PutFdrvRun") + "IFSBASE.RUN.DB"
                Session("FirstYear") = FirstYearForecast
                Session("LastYear") = Session("LastYearForecast")

                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_RebuildBase.aspx")
            Case "Rebuild Historical Base" 'jrs 2019/12/26
                'Form Load should prevent ever
                'reaching this sub unless ProfEdition; redundant protection
                Session("HistoricBase") = "True"
                Session("ActiveBase") = "IFSBASEHIST.RUN"
                Session("WorkingFile") = "IFSBASEHIST.RUN.DB"
                Session("RunFiles")(1) = Session("PutFdrvRun") + "IFSBASEHIST.RUN.DB"
                Session("FirstYear") = FirstYearHistory
                Session("LastYear") = LastYearHistory

                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_RebuildBase.aspx")
            Case "Import Data (Single Series)" 'jrs 2023/01/17
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "Frm_ImportXLSNew.aspx")
            Case "Import Provinces Data (Single Series)" 'jrs 2026/07/22
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "Frm_ImportXLSNewProvinces.aspx")
            Case "Vet and Merge Imported Data" 'jrs 2023/03/28
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "Frm_ViewDataBatch.aspx")
            Case "Import Population Data" 'jrs 2022/09/09
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_ImportPopData.aspx")
            Case "Most Recent Adjusted Data, Add or Update" 'jrs 2022/09/13
                mnuMostRecentAdjData_Click()
            Case "Load Project Data" 'jrs 2022/09/14
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_SelectProjectFile.aspx")
            Case "Unload Project Data" 'jrs 2022/09/14
                Session("Message") = ""
                UnLoadProjectFile(Session("Message"), Session("PutFdrvRun"))
                If Session("Message") > "" Then
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                Else
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_RebuildBase.aspx")
                End If
            Case "Add or Delete SubRegions" 'jrs 2022/09/20
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_AddDelProvinces.aspx")
            Case "Guided Base Year Change" 'jrs 2022/12/12
                mnuGuidedBaseYearChange_Click()
            Case Else 'jrs 2005/06/27
                If objEA.Item.UserData + "\" = Session("PutFdrvRun") Then
                    If objEA.Item.URL = "Open" Then
                        Dim sFileName As String = Session("PutFdrvRun") + objEA.Item.Caption + ".run.db"

                        Function2.OpenFile(sFileName, Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                        Session("PutFdrvRun"), Session("WorkingFile"), Session("RunFiles"), Session("HistoricBase"), Session("ActiveBase"), Session("FirstYear"),
                        Session("LastYear"), Session("LastYearForecast"), Session("FilesSelected"), Session("DisplayLastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                        Session("ScenarioFileDir"), Session("FinYear"), Session("SessionIdx"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))
                        Session("WorkingFile") = sFileName
                        'jrs 2020/08/28
                        Session("FileForDisplay0") = Nothing
                        DeleteDFInCache(0, Session)
                    Else
                        DeleteFile(Session("PutFdrvRun") + objEA.Item.Caption + ".run.db")
                    End If
                Else
                    LoadPreRunSce(objEA.Item.Caption, objEA.Item.UserData) 'jrs 2006/08/22 added objEA.Item.UserData
                End If
        End Select
    End Sub

    'jrs 2008/11/25
    Private Sub ClearAllSessions()
        Dim i As Integer
        SyncLock SessionList
            For i = 0 To MaxFullSessions - 1
                If SessionList(i) Then
                    SessionList(i) = False
                End If
            Next
        End SyncLock
    End Sub

    'JRS 04/15/04
    'Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    'pnlFilePicker.Visible = False
    'End Sub

    'JRS 04/15/04
    'Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    'If (FilePicker1.Text.Length > 0) Then
    '    If lblMode.Text = "Open File" Then
    '        Open_Click(sender, e)
    '    ElseIf lblMode.Text = "Save As .." Then
    '        SaveAs_Click(sender, e)
    '    End If
    '    pnlFilePicker.Visible = False
    'End If

    'End Sub

    'JRS 08/16/04
    Public Overrides Sub mnuReload_Click()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 04/25/02
        Dim sFileName As String
        'cdv 2021/12/13 ****START****
        If Session("HistoricBase") = CStr(True) Then
            If Session("mnuSQLite") = "True" Then
                sFileName = Session("PutFdrvRun2") & "\IFsBaseHist.run.db"
            Else
                sFileName = Session("PutFdrvRun2") & "\IFsBaseHist.run"
            End If
            Function2.OpenFile(sFileName, Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("PutFdrvRun"), Session("WorkingFile"), Session("RunFiles"), Session("HistoricBase"), Session("ActiveBase"), Session("FirstYear"),
            Session("LastYear"), Session("LastYearForecast"), Session("FilesSelected"), Session("DisplayLastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
            Session("ScenarioFileDir"), Session("FinYear"), Session("SessionIdx"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))
            Session("WorkingFile") = sFileName
        Else
            If Session("mnuSQLite") = "True" Then
                sFileName = Session("PutFdrvRun2") & "\IFsBase.run.db"
            Else
                sFileName = Session("PutFdrvRun2") & "\IFsBase.run"
            End If
            Function2.OpenFile(sFileName, Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("PutFdrvRun"), Session("WorkingFile"), Session("RunFiles"), Session("HistoricBase"), Session("ActiveBase"), Session("FirstYear"),
            Session("LastYear"), Session("LastYearForecast"), Session("FilesSelected"), Session("DisplayLastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
            Session("ScenarioFileDir"), Session("FinYear"), Session("SessionIdx"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))
            Session("WorkingFile") = sFileName
        End If
        'jrs 2020/08/28
        Session("FileForDisplay0") = Nothing
        DeleteDFInCache(0, Session)

        LoadMessage("Base Reloaded", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
        'cdv 2021/12/13 ****END****
    End Sub

    'jrs 2005/06/27
    Private Sub LoadPreRunSce(ByVal sFile As String, ByVal d As String) 'jrs 2006/08/22 added D for Directory where to start looking
        'jrs 2006/08/22
        'Dim sDirSess As String = "\Session"
        'If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
        '    sDirSess = sDirSess & "0"
        'End If
        'sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sFileName As String
        'jrs 2006/08/22 sFileName = FindFile(Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text), sFile & ".run", "run")
        sFileName = FindFile(d, sFile & ".run.db", "run.db")
        If Not IsNothing(sFileName) Then
            FileCopyCheck(sFileName, Session("PutFdrvRun") & sFile & ".run.db")
            FileCopyCheck(sFileName.Replace(sFile & ".run.db", "SAM" & sFile & ".db"), Session("PutFdrvRun") & "SAM" & sFile & ".db")
            'jrs 2008/06/16
            FileCopyCheck(sFileName.Replace(sFile & ".run.db", "IFsPop" & sFile & ".db"), Session("PutFdrvRun") & "IFsPop" & sFile & ".db")

            Session("IFsRunFiles").Add(sFile & ".run.db", True)
            'jrs 2017/06/29
            Session("SDGsetcboSce2Idx") = 2
            Dim i As Integer
            For i = 1 To Session("IFsRunFiles").count
                If Session("IFsRunFiles").item(i).visible Then
                    If sFile & ".run" > Session("IFsRunFiles").item(i).name Then
                        Session("SDGsetcboSce2Idx") = Session("SDGsetcboSce2Idx") + 1
                    End If
                End If
            Next

            Dim Message As String
            Message = "Scenario successfully loaded - Results can be displayed."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

    End Sub

    Sub ItemClickHandlerContext(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        Dim xpoint As Double
        Dim ypoint As Double
        Dim cName As String
        objSender = sender
        Dim url As String

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e
        'strs = Split(Request.Form("txtDataHolder"), ",")
        'xpoint = CDbl(strs(0))
        'ypoint = CDbl(strs(1))
        cName = Request.Form("txtDataHolder")
        'jrs 2011/03/30
        If cName = "dontshowmenu" Or cName = "" Then
            cName = Request.Form("txtGoogleCtry")
            'jrs 2011/04/01 handle Greenland
            If cName = "GL" Then
                cName = "DK"
                'jrs 2013/10/14 ElseIf cName = "SS" Then 'jrs 2011/10/28
                'jrs 2013/10/14 cName = "SD"
                'jrs 2013/10/14 ElseIf cName = "KO" Then 'jrs 2011/10/28
                'jrs 2013/10/14 cName = "RS"
            ElseIf cName = "WB" Then 'jrs 2011/11/03
                cName = "PS"
            ElseIf Len(cName) > 2 Then
                cName = Left(cName, 2)
            End If
            'From Google code to Esri Country
            Dim valtblR As DataTable
            SetDataTablefromAccess(IFs_MDB, "SELECT ct.Country, cg.latitude, cg.longitude, cg.zoomlevel FROM [Country Translation] ct, [Country Google] cg WHERE [ISO3166-2 Codes]=cg.country AND [ISO3166-2 Codes]=""" & cName & """", "", valtblR)

            Dim sTCtry As String = ""
            If valtblR.Rows.Count > 0 Then
                Session("MMCountry") = cName
                sTCtry = IIf(IsDBNull(valtblR.Rows(0).Item(0)), cName, valtblR.Rows(0).Item(0))

                'jrs 2011/11/24
                If valtblR.Rows(0).Item("zoomlevel") > 1 Then
                    Session("CentroidX") = valtblR.Rows(0).Item("longitude")
                    Session("CentroidY") = valtblR.Rows(0).Item("latitude")
                    Session("ZoomLevel") = valtblR.Rows(0).Item("zoomlevel")
                Else
                    Session.Remove("CentroidX")
                    Session.Remove("CentroidY")
                    Session.Remove("ZoomLevel")
                End If

                SetDataTablefromAccess(IFs_MDB, "SELECT NewRegion FROM " & Regionalization$ &
                " AS R, " & RegionIDNew & " AS I Where I.RegionNum = R.NewRegion AND R.Country = """ &
                sTCtry & """", "", valtblR)

                If valtblR.Rows.Count > 0 Then
                    Session("cNumber") = valtblR.Rows(0).Item("NewRegion")
                End If

            ElseIf cName > "" And cName <> "dontshowmenu" Then
                Session("MMCountry") = cName
            End If

            'jrs 2026/01/12
            If StandAloneMode And sTCtry.Length > 2 Then
                Dim sIP As String
                sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")
                Dim sSQL As String = "UPDATE IFsInit SET [Value] = """ & sTCtry & """ WHERE [Variable] = 'DefRegion'"
                updateAccessWith1Command(sIP, sSQL)
            End If


        End If

            'KIA 02/09/2009 commented the following line    
            'Session.Add("cNumber", cName)
            Select Case objEA.Item.Caption
            Case "Country Profile"
                'jam 2021/04/18
                'Support.FormManager.StateStore(Me)
                ''jrs 2011/03/24
                'If Not Session("MMCountry") Is Nothing Then
                '    Support.FormManager.Show(Me, "frm_CountryProfile.aspx?Country=" & PrepareForURL(Session("MMCountry")))
                'Else
                '    Support.FormManager.Show(Me, "frm_CountryProfile.aspx")
                'End If

                'jam 2021/04/18
                Call ShowCountryProfile()


            Case "Basic Report for Countries/Regions or Groupings"
                Report_Click(Me, New System.EventArgs) 'done
            Case "Birth Cohort Information" 'jrs 2012/06/28
                'jrs 2025/11/18
                Session("PopCohort_Grouping") = Nothing
                Support.FormManager.Show(Me, "frm_CohoInfDisp.aspx?Country=" & PrepareForURL(Session("MMCountry")) & "&Year=" & Session("FirstYear") & "&File=Base Case")
            Case "Education by Age, Sex and Level"
                'Support.FormManager.Show(Me, "frm_EdnCohoDisp.aspx?Country=" & PrepareForURL(Session("MMCountry")) & "&Year=" & Session("FirstYear") & "&File=Base Case")
                'JAM 2020-10-14
                Call ShowEdnCohoDisp(Session("MMCountry"))
            Case "Population by Age and Sex"
                'CohortsofPopulation_Click(sender, e)
                'jam 2021/04/03
                Call ShowPopCohoDisp()
            Case "World Value Survey by Age"
                CohortsofWorldValueSurvey_Click(sender, e)
            Case "Mortality by Age, Sex and Cause" 'jrs 2008/09/16
                'jrs 2024/04/24 changed default to Base Case
                Support.FormManager.Show(Me, "frm_MortCohorts.aspx?Country=" & PrepareForURL(Session("MMCountry")) & "&Year=" & Session("FirstYear") & "&File=Base Case")
            Case "Morbidity by Age, Sex and Cause" 'jrs 2013/02/25
                Support.FormManager.Show(Me, "frm_MorbCohorts.aspx?Country=" & PrepareForURL(Session("MMCountry")) & "&Year=" & Session("FirstYear") & "&File=Base Case")
            Case "Growth Accounting" 'jrs 2020/06/06
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_GrowthAccounting.aspx")
            Case "World Map, Lorenz Curve, Gini"
                mnuWorldVariables_Click(sender, e)
            Case "Social Accounting Matrix"
                SocialAccountingMatrix_Click(sender, e) 'done
            Case "World Bank Financial Flows"
                WBFinancialFlows_Click(sender, e)
            Case "Advanced Sustainability Analysis" 'jrs 2012/08/15
                mnuASA_Click(sender, e)
            Case "Performance Risk Evaluation" 'jrs 2013/05/31
                Support.FormManager.Show(Me, "frm_PerfRiskAnalysis.aspx?Country=" & PrepareForURL(Session("MMCountry")))
            Case "Millennium Development Goals"
                Support.FormManager.Show(Me, "frm_MDG.aspx?Country=" & PrepareForURL(Session("MMCountry")))
            Case "SDG Graph" 'jrs 2017/05/31
                Support.FormManager.Show(Me, "frm_SDG.aspx?Country=" & PrepareForURL(Session("MMCountry")))
            Case "SDG Overview Table" 'jrs 2017/06/07
                Support.FormManager.Show(Me, "frm_SDGset.aspx?Country=" & PrepareForURL(Session("MMCountry")))
            Case "Development Profile" 'jrs 2023/10/02
                Support.FormManager.Show(Me, "frm_DevelopmentProfile.aspx?Country=" & PrepareForURL(Session("MMCountry")))
            Case "Zoom Out" 'jrs 2011/11/25
                Session.Remove("CentroidX")
                Session.Remove("CentroidY")
                Session.Remove("ZoomLevel")
        End Select
        Response.Redirect(Request.Url.AbsolutePath)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CreateMenu()

        If Not Session("OpenedFile#") Is Nothing Then
            FileClose(CInt(Session("OpenedFile#")))
            Session.Remove("OpenedFile#")
        End If
        tmpCname = Request.Form("txtDataHolder")

        'jrs 2021/09/14
        If Not Page.IsPostBack Then
            WriteFormControlCaption(Me, Session("PutFdrvRun"), sLanguage)
        End If
        'cdv 2022/06/13
        If Session("JSNeedsUpdate") = True Then
            'jrs 2022/12/14
            LoadCategoriesFlexDisp(False)
            CreateJSforFlexDisplays("IFSBASE.RUN", Session("PutFdrvRun"), Session("CategoryIds"))

            GenerateVarsforVarSelList(Session("PutFdrvRun"))
            GenerateVarsforVarSelListH(Session("PutFdrvRun"))
            GenerateVarsforVarSelListHF(Session("PutFdrvRun"))

            Session("JSNeedsUpdate") = False
        End If

    End Sub
    Private Sub RepaintMap()
        lstPts = Session("maplstPts")
        lstEvts = Session("maplstEvts")
        Dim mMap As AspMap.Map

        mMap = Session("sessionMap")
        If lstPts.Count > 0 Then
            Dim i As Integer
            For i = 0 To lstPts.Count - 1
                Select Case lstEvts.Item(i)
                    Case "IN"
                        mMap.ZoomIn(lstPts.Item(i))
                    Case "OUT"
                        mMap.ZoomOut(lstPts.Item(i))
                    Case "PAN"
                        mMap.CenterAt(lstPts.Item(i))
                End Select
            Next
        End If
    End Sub
    Private Sub CreateMenu()
        wmPopUp.UserData = "jsolorza::976739975"
        wmPopUp.ClearPixelImage = "blank.gif"
        wmPopUp.PopupIcon = "./images/Popup.gif"
        wmPopUp.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmPopUp.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmPopUp.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmPopUp.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmPopUp.SelectedPopupIcon = "./images/SelectedPopup.gif"
        wmPopUp.ScrollEnabled = True
        AddHandler wmPopUp.ServerItemClick, AddressOf ItemClickHandlerContext
        wmPopUp.MenuBar.ShowOnMouseOver = True
        'wmPopUp.GeneratedPrefix = "mm"

        'jrs 2012/01/26
        wmPopUp.ButtonFont.Size = "12px"
        wmPopUp.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp.Groups.Add()

        cswmGroup1.ID = "Group_bc88"

        Dim cswmItem As Coalesys.WebMenu.Item

        'jrs 2022/01/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "POPULATION"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Population by Age and Sex"
        cswmItem.OnClick = "showhg();"

        'jrs 2012/06/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Birth Cohort Information"
        cswmItem.OnClick = "showhg();"

        'jrs 2022/05/30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "EDUCATION"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Education by Age, Sex and Level"
        cswmItem.OnClick = "showhg();"

        'jrs 2022/05/30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "HEALTH"

        'jrs 2008/09/16
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Mortality by Age, Sex and Cause"
        cswmItem.OnClick = "showhg();"

        'jrs 21013/02/25
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Morbidity by Age, Sex and Cause"
        cswmItem.OnClick = "showhg();"

        'jrs 2022/05/30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "DEVELOPMENT GOALS"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "SDG Overview Table"
        cswmItem.OnClick = "showhg();"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "SDG Graph"
        cswmItem.OnClick = "showhg();"

        'jrs 2022/05/30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "GENERAL PERFORMANCE ANALYSIS"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Country Profile"
        cswmItem.OnClick = "showhg();"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Basic Report for Countries/Regions or Groupings"
        cswmItem.OnClick = "showhg();"

        'jrs 2024/04/09
        'jrs 2013/08/21
        'If DevelopmentMode Then
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Performance Risk Evaluation"
        cswmItem.OnClick = "showhg();"
        'End If

        'jrs 2022/05/30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "ECONOMY"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Development Profile"
        cswmItem.OnClick = "showhg();"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Social Accounting Matrix"
        cswmItem.OnClick = "showhg();"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "World Bank Financial Flows"
        cswmItem.OnClick = "showhg();"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Millennium Development Goals"
        'cswmItem.OnClick = "showhg();"

        'jrs 2022/05/30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'jrs 2022/01/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "WORLD VALUES"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "World Value Survey by Age"
        cswmItem.OnClick = "showhg();"


        'jrs 2019/07/31
        If Not bHidePrivateVars Then
            'jrs 2022/05/30
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "--shadow--"
            cswmItem.DividerColor = "#808080"
            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

            'jrs 2022/01/28
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "ENVIRONMENT"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Advanced Sustainability Analysis"
            cswmItem.OnClick = "showhg();"
        End If

        'jrs 2011/11/25
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "--shadow--"
        'cswmItem.DividerColor = "#808080"
        'cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Zoom In"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Zoom Out"

        'jrs 2013/06/19 wmPopUp.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp.ItemFont.PaddingLeft = 4

        ' wmPopUp.LoadState("Web\MainPopupMenu.wms")
    End Sub


    'Protected Function Identify(ByVal X As Integer, ByVal Y As Integer) As Integer
    '    Dim mMap As AspMap.Map
    '    Dim pPoint As AspMap.Point
    '    Dim identRS As AspMap.Recordset
    '    Dim cStrig As Integer

    '    'jrs 2008/07/29
    '    Session("sESRICountry") = ""

    '    mMap = Session("sessionMap")
    '    pPoint = mMap.ToMapPoint(X, Y)
    '    'jrs 10/01/04
    '    lstPts = Session("maplstPts")
    '    lstEvts = Session("maplstEvts")
    '    lstPts.Add(pPoint)


    '    identRS = IdentifyFeature(mMap, pPoint)
    '    If Not identRS.EOF Then
    '        identRS.MoveFirst()
    '        'JRSVBNET txtIDResults.Text = ""
    '        While Not identRS.EOF
    '            Dim i As Integer
    '            For i = 0 To identRS.FieldCount - 1
    '                If identRS.FieldName(i) = "CINDEX" Then
    '                    If Not TypeOf (identRS(i)) Is DBNull Then
    '                        cStrig = CInt(identRS(i))
    '                        Identify = cStrig
    '                        'JRS 2008/07/05
    '                        Session("sESRICountry") = identRS("NAME")
    '                        Exit For
    '                    End If
    '                End If
    '            Next
    '            identRS.MoveNext()
    '        End While

    '    End If

    '    'jrs 10/01/04
    '    Session("maplstPts") = lstPts
    '    Session("maplstEvts") = lstEvts
    '    'Response.Write("<script> ShowContextMenu(event);</script>")
    'End Function

    'Private Function IdentifyFeature(ByVal mMap As AspMap.Map, ByVal pPoint As AspMap.Point) As AspMap.Recordset
    '    Dim identRS As AspMap.Recordset
    '    Dim layerCount, Distance As Integer
    '    Dim pLayer As AspMap.Layer
    '    Dim pRectangle As AspMap.Rectangle

    '    identRS = New AspMap.Recordset
    '    layerCount = 0

    '    While (layerCount <= mMap.LayerCount - 1) And (identRS.EOF)
    '        pLayer = mMap.Layer(layerCount)
    '        Select Case pLayer.LayerType
    '            Case LayerType.mcPointLayer
    '                identRS = pLayer.SearchByDistance(pPoint, mMap.ToMapDistance(pLayer.Symbol.Size / 2))
    '            Case LayerType.mcPolygonLayer
    '                identRS = pLayer.SearchShape(pPoint, SearchMethod.mcPointInPolygon)
    '            Case LayerType.mcLineLayer
    '                Distance = mMap.ToMapDistance(pLayer.Symbol.Size + 2)
    '                pRectangle = New AspMap.Rectangle
    '                pRectangle.Top = pPoint.Y + Distance / 2
    '                pRectangle.Left = pPoint.X - Distance / 2
    '                pRectangle.Right = pPoint.X + Distance / 2
    '                pRectangle.Bottom = pPoint.Y - Distance / 2
    '                identRS = pLayer.SearchShape(pRectangle, SearchMethod.mcIntersect)
    '        End Select
    '        layerCount += 1
    '    End While
    '    Return identRS
    'End Function





    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            If tmpCname <> "" And Not tmpCname Is Nothing Then

                If tmpCname <> "donotshowmenu" Then
                    'Response.Write("<script> alert('" + tmpCname + "');</script>")

                End If
            End If
        Catch

        End Try
    End Sub



    'jrs 2011/04/01
    'Procedure to build array with countries and values for Main Menu map
    Private Sub PackJScritpBuilder()
        'jrs 2026/07/17
        ClientScript.RegisterStartupScript(Me.GetType(), "resetGeoMode", "sessionStorage.removeItem('geoMode');", True)

        'Check if variable already loaded
        Dim i As Integer
        Dim VarIdx As Integer
        If Session("MMVarName") Is Nothing Then
            Session("MMVarName") = "HDI"
            Session("MMDim2") = ""
            Session("MMDim3") = ""
            Session("MMVarNameLoaded") = "HDI"
            'Load Values
            ReDim Session("MMValues")(NR)
            LoadMMValues(VarIdx)
        ElseIf Session("MMVarName") <> Session("MMVarNameLoaded") Then
            ReDim Session("MMValues")(NR)
            Session("MMVarNameLoaded") = Session("MMVarName")
            LoadMMValues(VarIdx)
        Else
            VarIdx = IfsVars.Item(Session("MMVarName")).Index
        End If

        'jam 2025/09/08 Changing concatenation process
        Dim sJscript As String = ""

        Dim sb As New StringBuilder("")


        'sJscript = "    var RegCount=" & CStr(NR + 1) & ";" & vbCr
        'sJscript = sJscript & "    var VarName= """ & VDisplayName(VarIdx) & " (" & VDisplayUnitsName(VarIdx) & ")"";" & vbCr
        'sJscript = sJscript & "    var RegCodes = [];" & vbCr
        'sJscript = sJscript & "    var RegVals = [];" & vbCr
        'sJscript = sJscript & "    var RegNames = [];" & vbCr
        'For i = 0 To NR - 1
        '    sJscript = sJscript & "    RegCodes[" & i.ToString & "] = """ & IFsRegs.Item(GRPS(1, i + 1)).ISO3166_2Code & """;" & vbCr
        '    sJscript = sJscript & "    RegVals[" & i.ToString & "] = " & CStr(Session("MMValues")(i + 1)) & ";" & vbCr
        '    sJscript = sJscript & "    RegNames[" & i.ToString & "] = """ & GRPS(1, i + 1) & """;" & vbCr
        'Next

        'jam 2025/09/08 Changing concatenation process
        'sJscript = sJscript & "    var dataX = [" & vbCr
        'sJscript = sJscript & PrepareDataForMap()
        'sJscript = sJscript & "];" & vbCr


        sb.Append("    var dataX = [" & vbCr)
        sb.Append(PrepareDataForMap())
        sb.Append("];" & vbCr)


        'jam 2025/09/01 Assign final value
        sJscript = sb.ToString()



        'Add Greenland with Denmarks data
        'sJscript = sJscript & "    RegCodes[" & NR.ToString & "] = ""GL"";" & vbCr
        'sJscript = sJscript & "    RegVals[" & NR.ToString & "] = " & CStr(Session("MMValues")(IFsRegs.Item("Denmark").Index)) & ";" & vbCr
        'sJscript = sJscript & "    RegNames[" & NR.ToString & "] = ""Denmark"";" & vbCr

        'jam 2025/09/08 Changing concatenation process
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadMMMapVars",
        sJscript, True)


        Dim sJscript2 As String

        Dim sb2 As New StringBuilder("")


        'jam 2025/09/08 Changing concatenation process
        'sJscript2 = "<script type=""text/javascript"">" & vbCr




        'sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        'sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        ''Non-IE
        'sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        'sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        ''//IE 6+ in 'standards compliant mode'
        'sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        'sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        ''//IE 4 compatible
        'sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        'sJscript2 = sJscript2 & "{" & vbCr
        'sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        'sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        'sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 100;" & vbCr

        'sJscript2 = sJscript2 & "$(function () {" & vbCr

        'sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        'sJscript2 = sJscript2 & "lang: {" & vbCr
        'sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "});" & vbCr





        'sJscript2 = sJscript2 & "$('#container').highcharts('Map', {" & vbCr
        'sJscript2 = sJscript2 & "chart: {" & vbCr





        'sJscript2 = sJscript2 & "height:   myHeight," & vbCr
        'sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        'sJscript2 = sJscript2 & "panning: true," & vbCr
        'sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        'sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        'sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        'sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        'sJscript2 = sJscript2 & "events: {"
        'sJscript2 = sJscript2 & "load: function(event) {"
        'sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.visibility = 'hidden';"
        'sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.height = 0;"

        ''jam 2025/06/28 Zoom in based in Africa
        'sJscript2 = sJscript2 & " var chart = this; " & vbCr

        ''''''''''''''''''''''''''''''''''''''''''''''''''
        sb2.Append("<script type=""text/javascript"">" & vbCr)




        sb2.Append("var myWidth = 0, myHeight = 0;" & vbCr)
        sb2.Append("if( typeof( window.innerWidth ) == 'number' ) {" & vbCr)
        'Non-IE
        sb2.Append("myWidth = window.innerWidth;" & vbCr)
        sb2.Append("myHeight = window.innerHeight;" & vbCr)
        sb2.Append("} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr)
        '//IE 6+ in 'standards compliant mode'
        sb2.Append("myWidth = document.documentElement.clientWidth;" & vbCr)
        sb2.Append("myHeight = document.documentElement.clientHeight;" & vbCr)
        sb2.Append("} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr)
        '//IE 4 compatible
        sb2.Append("myWidth = document.body.clientWidth;" & vbCr)
        sb2.Append("myHeight = document.body.clientHeight;" & vbCr)
        sb2.Append("}" & vbCr)
        sb2.Append("if (myWidth == 0)" & vbCr)
        sb2.Append("{" & vbCr)
        sb2.Append("myWidth = screen.width;" & vbCr)
        sb2.Append("myHeight = screen.height;" & vbCr)
        sb2.Append("}" & vbCr)

        sb2.Append("myWidth = myWidth * 0.95;" & vbCr)
        sb2.Append("myHeight = myHeight * 0.90 - 100;" & vbCr)

        sb2.Append("$(function () {" & vbCr)

        sb2.Append("Highcharts.setOptions({" & vbCr)
        sb2.Append("lang: {" & vbCr)
        sb2.Append("contextButtonTitle: 'Options for Saving/Using'" & vbCr)
        sb2.Append("}" & vbCr)
        sb2.Append("});" & vbCr)





        sb2.Append("$('#container').highcharts('Map', {" & vbCr)
        sb2.Append("chart: {" & vbCr)





        sb2.Append("height:   myHeight," & vbCr)
        sb2.Append("zoomType: 'xy'," & vbCr)
        sb2.Append("panning: true," & vbCr)
        sb2.Append("panKey: 'shift'," & vbCr)
        sb2.Append("borderWidth: 3," & vbCr)
        sb2.Append("borderRadius: 15," & vbCr)
        sb2.Append("spacingLeft: 60," & vbCr)
        sb2.Append("events: {")
        sb2.Append("load: function(event) {")
        sb2.Append("document.getElementById(""firstloading"").style.visibility = 'hidden';")
        sb2.Append("document.getElementById(""firstloading"").style.height = 0;")

        'jam 2025/06/28 Zoom in based in Africa
        sb2.Append(" var chart = this; " & vbCr)



        'jrs 2025/06/30 
        If (Session("VersionName") = "NEPAD") Then
            'jam 2025/06/28 The parameters are:
            ' 1: zoom value: under 1 is Zoom in, above 1 is Zoom out
            ' 2: latitude value: Mali was taken for reference, its latitude is 17
            ' 3. longitude value: Mali was taken for reference, its longitude is 2

            'jam 2025/09/08 Changing concatenation process
            'sJscript2 = sJscript2 & " chart.mapZoom(0.52, 17, 2); " & vbCr
            sb2.Append(" chart.mapZoom(0.52, 17, 2); " & vbCr)


        End If

        'jam 2025/09/08 Changing concatenation process
        'sJscript2 = sJscript2 & "}}"
        'sJscript2 = sJscript2 & "}," & vbCr


        'sJscript2 = sJscript2 & "title: {" & vbCr
        'sJscript2 = sJscript2 & "text:       ''" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        'sJscript2 = sJscript2 & "credits: {" & vbCr
        'sJscript2 = sJscript2 & "enabled:       false" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        'sJscript2 = sJscript2 & "        mapNavigation: {" & vbCr
        'sJscript2 = sJscript2 & "enabled: true," & vbCr
        'sJscript2 = sJscript2 & "enableMouseWheelZoom: false," & vbCr
        'sJscript2 = sJscript2 & "buttonOptions: {" & vbCr
        'sJscript2 = sJscript2 & "align: 'right'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        'sJscript2 = sJscript2 & "                legend: {" & vbCr
        'sJscript2 = sJscript2 & "title: {" & vbCr
        'sJscript2 = sJscript2 & "text:       '" & VDisplayName(VarIdx) & " (" & Session("VDisplayUnitsName")(VarIdx) & ")'" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        'sJscript2 = sJscript2 & "layout:  'vertical'," & vbCr
        'sJscript2 = sJscript2 & "align:  'left'," & vbCr
        'sJscript2 = sJscript2 & "verticalAlign:  'bottom'," & vbCr
        'sJscript2 = sJscript2 & "floating:  true," & vbCr
        'sJscript2 = sJscript2 & "backgroundColor:  '#ffffff'" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        'Dim MMValLimits(5) As Single, j As Integer
        'Dim MMValRange As Single = (Session("MaxMMValues") - Session("MinMMValues")) / 6
        'MMValLimits(3) = Math.Round((Session("MinMMValues") + Session("MaxMMValues")) / 2, Session("iRound"))
        'MMValLimits(2) = Math.Round(MMValLimits(3) - MMValRange, Session("iRound"))
        'MMValLimits(1) = Math.Round(MMValLimits(2) - MMValRange, Session("iRound"))
        'MMValLimits(4) = Math.Round(MMValLimits(3) + MMValRange, Session("iRound"))
        'MMValLimits(5) = Math.Round(MMValLimits(4) + MMValRange, Session("iRound"))

        'sJscript2 = sJscript2 & "                colorAxis: {" & vbCr
        'sJscript2 = sJscript2 & "                dataClasses: [" & vbCr
        'sJscript2 = sJscript2 & "                {to: " & MMValLimits(1).ToString(CultureInfo.InvariantCulture) & "}," & vbCr
        'For j = 2 To 5
        '    sJscript2 = sJscript2 & "                {from: " & MMValLimits(j - 1).ToString(CultureInfo.InvariantCulture) & ", to: " & MMValLimits(j).ToString(CultureInfo.InvariantCulture) & "}," & vbCr
        'Next
        'sJscript2 = sJscript2 & "                {from: " & MMValLimits(5).ToString(CultureInfo.InvariantCulture) & "}" & vbCr
        'sJscript2 = sJscript2 & "]," & vbCr
        'sJscript2 = sJscript2 & "minColor: '#f7dcc8'," & vbCr
        'sJscript2 = sJscript2 & "maxColor: '#f47200'" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr

        'sJscript2 = sJscript2 & "series : [{" & vbCr
        ''jam 2025/06/28
        'sJscript2 = sJscript2 & "mapData: Highcharts.maps['custom/world']},{" & vbCr
        ''jam 2025/06/28
        ''sJscript2 = sJscript2 & "mapData: africanMapData},{" & vbCr

        'sJscript2 = sJscript2 & "data: dataX," & vbCr
        'sJscript2 = sJscript2 & "joinBy: ['ISO_2DIGIT', 'key']," & vbCr
        ''jam 2025/06/28
        'sJscript2 = sJscript2 & "mapData: Highcharts.maps['custom/world'], " & vbCr
        ''jam 2025/06/28
        ''sJscript2 = sJscript2 & "mapData: africanMapData, " & vbCr

        'sJscript2 = sJscript2 & "name:   """ & VDef(VarIdx) & """, " & vbCr
        'sJscript2 = sJscript2 & "states: {" & vbCr
        'sJscript2 = sJscript2 & "hover: {" & vbCr
        'sJscript2 = sJscript2 & "color: '#ffff00'" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        'sJscript2 = sJscript2 & "dataLabels: {" & vbCr
        'sJscript2 = sJscript2 & "enabled: true," & vbCr
        'sJscript2 = sJscript2 & "allowOverlap: false," & vbCr
        'sJscript2 = sJscript2 & "padding: 10," & vbCr
        'sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        'sJscript2 = sJscript2 & "if (this.point.value > 0)" & vbCr
        'sJscript2 = sJscript2 & "return this.point.properties['CNTRY_NAME'];" & vbCr
        'sJscript2 = sJscript2 & "else" & vbCr
        'sJscript2 = sJscript2 & "return '';" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr 'end of data Labels
        'sJscript2 = sJscript2 & "tooltip: {" & vbCr
        'sJscript2 = sJscript2 & "pointFormatter: function () {" & vbCr
        'sJscript2 = sJscript2 & "return this.properties['UNSHRTNAM']+': '+this.value;" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        'sJscript2 = sJscript2 & ", point: {" & vbCr
        'sJscript2 = sJscript2 & "events: {" & vbCr
        'sJscript2 = sJscript2 & "// On click, show popup menu" & vbCr
        'sJscript2 = sJscript2 & "click: function () {" & vbCr
        'sJscript2 = sJscript2 & "document.getElementById('txtGoogleCtry').value=this.properties['ISO_2DIGIT'];" & vbCr
        'sJscript2 = sJscript2 & "cswmShow('Group_bc88', '', 'below', tempX, tempY, 1);" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        'sJscript2 = sJscript2 & "}]" & vbCr
        'sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr
        'sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        'sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        'sJscript2 = sJscript2 & "})" & vbCr
        'sJscript2 = sJscript2 & ".attr({" & vbCr
        'sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        'sJscript2 = sJscript2 & "})" & vbCr
        'sJscript2 = sJscript2 & ".css({"
        'sJscript2 = sJscript2 & "cursor: 'pointer'"
        'sJscript2 = sJscript2 & "})"
        'sJscript2 = sJscript2 & ".add();" & vbCr

        ''''''''''''''''''''''''''''''''''''''''''
        sb2.Append("}}")
        sb2.Append("}," & vbCr)


        sb2.Append("title: {" & vbCr)
        sb2.Append("text:       ''" & vbCr)
        sb2.Append("}," & vbCr)

        sb2.Append("credits: {" & vbCr)
        sb2.Append("enabled:       false" & vbCr)
        sb2.Append("}," & vbCr)

        sb2.Append("        mapNavigation: {" & vbCr)
        sb2.Append("enabled: true," & vbCr)
        sb2.Append("enableMouseWheelZoom: false," & vbCr)
        sb2.Append("buttonOptions: {" & vbCr)
        sb2.Append("align: 'right'" & vbCr)
        sb2.Append("}" & vbCr)
        sb2.Append("}," & vbCr)

        sb2.Append("                legend: {" & vbCr)
        sb2.Append("title: {" & vbCr)
        sb2.Append("text:       '" & VDisplayName(VarIdx) & " (" & Session("VDisplayUnitsName")(VarIdx) & ")'" & vbCr)
        sb2.Append("}," & vbCr)
        sb2.Append("layout:  'vertical'," & vbCr)
        sb2.Append("align:  'left'," & vbCr)
        sb2.Append("verticalAlign:  'bottom'," & vbCr)
        sb2.Append("floating:  true," & vbCr)
        sb2.Append("backgroundColor:  '#ffffff'" & vbCr)
        sb2.Append("}," & vbCr)

        Dim MMValLimits(5) As Single, j As Integer
        Dim MMValRange As Single = (Session("MaxMMValues") - Session("MinMMValues")) / 6
        MMValLimits(3) = Math.Round((Session("MinMMValues") + Session("MaxMMValues")) / 2, Session("iRound"))
        MMValLimits(2) = Math.Round(MMValLimits(3) - MMValRange, Session("iRound"))
        MMValLimits(1) = Math.Round(MMValLimits(2) - MMValRange, Session("iRound"))
        MMValLimits(4) = Math.Round(MMValLimits(3) + MMValRange, Session("iRound"))
        MMValLimits(5) = Math.Round(MMValLimits(4) + MMValRange, Session("iRound"))

        sb2.Append("                colorAxis: {" & vbCr)
        sb2.Append("                dataClasses: [" & vbCr)
        sb2.Append("                {to: " & MMValLimits(1).ToString(CultureInfo.InvariantCulture) & "}," & vbCr)
        For j = 2 To 5
            sb2.Append("                {from: " & MMValLimits(j - 1).ToString(CultureInfo.InvariantCulture) & ", to: " & MMValLimits(j).ToString(CultureInfo.InvariantCulture) & "}," & vbCr)
        Next
        sb2.Append("                {from: " & MMValLimits(5).ToString(CultureInfo.InvariantCulture) & "}" & vbCr)
        sb2.Append("]," & vbCr)
        sb2.Append("minColor: '#f7dcc8'," & vbCr)
        sb2.Append("maxColor: '#f47200'" & vbCr)
        sb2.Append("}," & vbCr)

        sb2.Append("series : [{" & vbCr)
        'jam 2025/06/28
        sb2.Append("mapData: Highcharts.maps['custom/world']},{" & vbCr)
        'jam 2025/06/28
        'sJscript2 = sJscript2 & "mapData: africanMapData},{" & vbCr

        sb2.Append("data: dataX," & vbCr)
        sb2.Append("joinBy: ['ISO_2DIGIT', 'key']," & vbCr)
        'jam 2025/06/28
        sb2.Append("mapData: Highcharts.maps['custom/world'], " & vbCr)
        'jam 2025/06/28
        'sJscript2 = sJscript2 & "mapData: africanMapData, " & vbCr

        sb2.Append("name:   """ & VDef(VarIdx) & """, " & vbCr)
        sb2.Append("states: {" & vbCr)
        sb2.Append("hover: {" & vbCr)
        sb2.Append("color: '#ffff00'" & vbCr)
        sb2.Append("}" & vbCr)
        sb2.Append("}," & vbCr)
        sb2.Append("dataLabels: {" & vbCr)
        sb2.Append("enabled: true," & vbCr)
        sb2.Append("allowOverlap: false," & vbCr)
        sb2.Append("padding: 10," & vbCr)
        sb2.Append("formatter: function () {" & vbCr)
        sb2.Append("if (this.point.value > 0)" & vbCr)
        sb2.Append("return this.point.properties['CNTRY_NAME'];" & vbCr)
        sb2.Append("else" & vbCr)
        sb2.Append("return '';" & vbCr)
        sb2.Append("}" & vbCr)
        sb2.Append("}," & vbCr) 'end of data Labels
        sb2.Append("tooltip: {" & vbCr)
        sb2.Append("pointFormatter: function () {" & vbCr)
        sb2.Append("return this.properties['UNSHRTNAM']+': '+this.value;" & vbCr)
        sb2.Append("}" & vbCr)
        sb2.Append("}" & vbCr)

        sb2.Append(", point: {" & vbCr)
        sb2.Append("events: {" & vbCr)
        sb2.Append("// On click, show popup menu" & vbCr)
        sb2.Append("click: function () {" & vbCr)
        sb2.Append("document.getElementById('txtGoogleCtry').value=this.properties['ISO_2DIGIT'];" & vbCr)
        sb2.Append("cswmShow('Group_bc88', '', 'below', tempX, tempY, 1);" & vbCr)
        sb2.Append("}" & vbCr)
        sb2.Append("}" & vbCr)
        sb2.Append("}" & vbCr)

        sb2.Append("}]" & vbCr)
        sb2.Append("}, function(chart){" & vbCr)

        sb2.Append("chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr)
        sb2.Append(".on('click', function () {" & vbCr)
        sb2.Append("location.href = 'https://korbel.du.edu/pardee/';" & vbCr)
        sb2.Append("})" & vbCr)
        sb2.Append(".attr({" & vbCr)
        sb2.Append("zIndex: 7" & vbCr)
        sb2.Append("})" & vbCr)
        sb2.Append(".css({")
        sb2.Append("cursor: 'pointer'")
        sb2.Append("})")
        sb2.Append(".add();" & vbCr)



        'jrs 2025/06/19
        'sJscript2 = sJscript2 & "chart.mapView.setView([10, 0], 2);" & vbCr

        'jrs 2025/06/30
        If (Session("VersionName") = "NEPAD") Then
            'jam 2025/09/08 Changing concatenation process
            'parameters seem to be x, y, width, height
            'sJscript2 = sJscript2 & "chart.renderer.image('./Images/Logo_AUDA-NEPAD.png', 500, 310, 200, 100)" & vbCr
            'sJscript2 = sJscript2 & ".add();" & vbCr
            'sJscript2 = sJscript2 & "chart.renderer.image('./Images/RwandaFlag.png', 500, 450, 200, 150)" & vbCr
            'sJscript2 = sJscript2 & ".add();" & vbCr

            sb2.Append("chart.renderer.image('./Images/Logo_AUDA-NEPAD.png', 500, 310, 200, 100)" & vbCr)
            sb2.Append(".add();" & vbCr)
            sb2.Append("chart.renderer.image('./Images/RwandaFlag.png', 500, 450, 200, 150)" & vbCr)
            sb2.Append(".add();" & vbCr)

        End If
        'jam 2025/09/08 Changing concatenation process
        'sJscript2 = sJscript2 & "});" & vbCr
        'sJscript2 = sJscript2 & "});" & vbCr
        'sJscript2 = sJscript2 & "</script>"

        sb2.Append("});" & vbCr)
        sb2.Append("});" & vbCr)
        sb2.Append("</script>")

        'jam 2025/09/08 Assign final value
        'lblJS.Text = sJscript2

        lblJS.Text = sb2.ToString()


    End Sub

    Private Sub LoadMMValues(ByRef VarIdx)
        Dim tmpVal() As Single
        Dim Yr As Integer = Year(Now)
        ReDim tmpVal(Yr - FirstYearForecast + 1)
        Dim Dim2Idx As Integer = -1, Dim3Idx As Integer = -1
        Session("MaxMMValues") = -1000000000
        Session("MinMMValues") = 1000000000

        'jrs 2022/03/28
        Dim bIncompatibleRegionFile As Boolean = False
        If Session("mnuSQLite") = "True" Then
            bIncompatibleRegionFile = Session("IFsRunFiles").item("IFsBase.run.db").RegionsHaveChanged
        End If

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        Dim i As Integer
        For i = 1 To NR
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(Session("MMVarName"), VarIdx, GRPS(1, i), i, Session("MMDim2"), Dim2Idx, Session("MMDim3"), Dim3Idx,
                Session("PutFdrvRun") & "ifsBase.run.db", 1, i = 1, i = NR, False, 1, tmpVal, 1, 1, FirstYearForecast, Yr, 0, True,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF, bIncompatibleRegionFile)
            Else
                gLoadForDisplay(Session("MMVarName"), VarIdx, GRPS(1, i), i, Session("MMDim2"), Dim2Idx, Session("MMDim3"), Dim3Idx,
                Session("PutFdrvRun") & "ifsBase.run", 1, i = 1, i = NR, False, 1, tmpVal, 1, 1, FirstYearForecast, Yr, 0, True,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If



            Session("MMValues")(i) = Math.Round(tmpVal(Yr - FirstYearForecast + 1), 2)
            If Session("MMValues")(i) > Session("MaxMMValues") Then
                Session("MaxMMValues") = Session("MMValues")(i)
            End If
            If Session("MMValues")(i) < Session("MinMMValues") Then
                Session("MinMMValues") = Session("MMValues")(i)
            End If
        Next
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2014-06-10 Routine for Javascript variables used in combo in Forms:
    '       Flex Displays, ScatVars, LongVars and VarSelect                
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub JavaScriptGeneration()
        Dim vScatVarFiltName, viFilterYear, viFilterDataPoints As String

        '2014-07-17
        vScatVarFiltName = "ALL"
        'Session("PutFdrvRun") = PutFdrv & Session("ResFileSubDir")
        viFilterYear = LastYearHistory
        viFilterDataPoints = 10
        '2014-07-17 parameter isn't used
        'Session("Scat_GroupNames") = "Automobile"



        'JAM 2014-06-14
        'GenerateVarsforScatvars(Session("ScatVarFiltName"), Session("PutFdrvRun"), Session("iFilterYear"), Session("iFilterDataPoints"), Session("Scat_GroupNames"))
        '2014-07-17 parameter Scat_groupnames isn't used
        GenerateVarsforScatvars(vScatVarFiltName, Session("PutFdrvRun"), viFilterYear, viFilterDataPoints, "")

        'GenerateVarsforScatvars(Session("ScatVarFiltName"), Session("PutFdrvRun"), Session("iFilterYear"), Session("iFilterDataPoints"), "")

        'JAM 2014-06-14
        GenerateVarsforLongvars(vScatVarFiltName, Session("PutFdrvRun"), viFilterYear, viFilterDataPoints, "")

        'GenerateVarsforLongvars(Session("LongVarFiltName"), Session("PutFdrvRun"), Session("iFilterYear"), Session("iFilterDataPoints"), "")

        'JAM 2014-07-20
        GenerateVarsforEmpVarSel(Session("PutFdrvRun"))
        GenerateVarsforForeVarSel(Session("PutFdrvRun"))
        'jrs 2023/05/18
        GenerateVarsforVarSelList(Session("PutFdrvRun"))
        GenerateVarsforVarSelListH(Session("PutFdrvRun"))
        GenerateVarsforVarSelListHF(Session("PutFdrvRun"))

        'JAM 2014-07-27
        LoadCategoriesFlexDisp(True) 'jrs 2019/08/22 added historic parameter
        'jrs 2015/03/25 need to generate both for history and forecast
        'JAM 2014-07-20
        'CreateJSforFlexDisplays(Session("ActiveBase"), Session("PutFdrvRun"), Session("CategoryIds"))
        CreateJSforFlexDisplays("IFSBASEHIST.RUN", Session("PutFdrvRun"), Session("CategoryIds"))
        LoadCategoriesFlexDisp(False) 'jrs 2019/08/22 added historic parameter
        CreateJSforFlexDisplays("IFSBASE.RUN", Session("PutFdrvRun"), Session("CategoryIds"))

        'jrs 2015/08/13
        GenerateVarsforCyberDasboard(Session("PutFdrvRun"), Session("ScenarioFileDir"))
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2019-10-08 Defining Global Variable for SQLite Data                
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuSQLiteData(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Session("mnuSQLite") Then
            Session("mnuSQLite") = False
        Else
            Session("mnuSQLite") = True
        End If

    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2022/07/02 Defining Global Variable for Opening Tabs                
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuOpenTabs(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Session("mnuTabs") Then
            Session("mnuTabs") = False
        Else
            Session("mnuTabs") = True
        End If
        'jrs 2023/06/02
        If StandAloneMode Then
            UpdateIfsInit()
        End If

    End Sub



    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' JAM 2014-07-27 Adding Categories Definition
    ''' 
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub LoadCategoriesFlexDisp(bHistoric As Boolean)

        Dim rsL As DataTable
        Dim str As String
        str = "SELECT DISTINCT Category FROM VarLists"
        'JRS 2005/11/16
        'If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
        If Not bHistoric Then
            str = str + " WHERE HisCompEmpFor = False AND Type = 'V'" 'jrs 2006/01/18 'ListName NOT IN (SELECT V1.ListName FROM VarLists as V1, VarLists as V2 WHERE V1.ListName = V2.ListName AND V1.History = True AND V2.History = False)"
        Else
            str = str + " WHERE HistoryplusForecast = False AND Type = 'V'" 'jrs 2013/09/06 added and HistoryPlusForecast = False
        End If
        SetDataTablefromAccess(Session("PutFdrvRun") + "IFSVAR.mdb", str, "", rsL)

        'jrs 2012/10/26
        Dim CatCol As Collection
        CatCol = New Collection
        Dim x As Object, i As Integer, j As Integer
        Do While (Not rsL.Rows.Count = j)
            str = IIf(IsDBNull(rsL.Rows(j).Item("Category")), "", rsL.Rows(j).Item("Category"))
            x = Split(rsL.Rows(j).Item("Category"), ",")
            'On Error Resume Next
            For i = 0 To UBound(x)
                If Not CatCol.Contains(Trim(x(i))) Then
                    CatCol.Add(Trim(x(i)), Trim(x(i)))
                End If
            Next i
            'On Error GoTo 0
            If UBound(x) = -1 Then
                CatCol.Add(Trim(str), Trim(str))
            End If
            j = j + 1
        Loop

        'jrs 2012/10/26 need to sort categories
        Dim Cat As Object
        Dim CatArr() As String
        ReDim CatArr(CatCol.Count)
        i = 0
        For Each Cat In CatCol
            i = i + 1
            CatArr(i) = Cat
        Next Cat
        'lstCategories.Sorted = True
        QuickSort(CatArr, 1, CatCol.Count)

        i = 0
        'jrs 2012/03/29
        ''Dim bReloadCats As Boolean
        ''bReloadCats = (Not Session("PDFlexCategoriesIdxs") Is Nothing)
        ''If Not bReloadCats Then
        ''    'jrs 2013/03/20 ReDim Session("PDFlexCategoriesIdxs")(rsL.Rows.Count)
        ''    ReDim Session("PDFlexCategoriesIdxs")(CatCol.Count)
        Session("CategoryIds") = New Collection
        ''Else
        ''lstCategories.Items(0).Selected = CBool(Session("PDFlexCategoriesIdxs")(0))
        ' ''jrs 2013/05/10
        ''If CatCol.Count > UBound(Session("PDFlexCategoriesIdxs")) Then
        ''    ReDim Preserve Session("PDFlexCategoriesIdxs")(CatCol.Count)
        ''End If
        ''End If


        For i = 1 To CatCol.Count



            ''If Not bReloadCats Then
            'jrs 2012/12/06 CType(Session("CategoryIds"), Collection).Add(i, str)
            CType(Session("CategoryIds"), Collection).Add(i, CStr(CatArr(i)))
            ''End If
            'Loop
        Next i
        ''If lstCategories.Items.Count > 0 Then
        ''    'jrs 2012/03/29
        ''    ''jrs 2005/04/20
        ''    'If Not (Session("PDFlexCategoriesIdx") Is Nothing) Then
        ''    '    lstCategories.SelectedIndex = Session("PDFlexCategoriesIdx")
        ''    'Else
        ''    '    lstCategories.SelectedIndex = 0
        ''    'End If
        ''    If Not bReloadCats Then
        ''        lstCategories.Items(0).Selected = True
        ''        Session("PDFlexCategoriesIdxs")(0) = True
        ''    End If

        'jrs 2005/10/13
        ''Dim sT As String
        ''sT = CStr(Session("TableTitle"))
        ''Call lstCategories_SelectedIndexChanged(Me, New System.EventArgs)
        ''If sT > "" Then
        ''    Session("TableTitle") = sT
        ''End If
        ''End If
    End Sub

    Private Function PrepareDataForMap() As String
        Dim jScript As String = "", i As Integer
        Dim iRound As Integer = 4
        Select Case ABS_(Session("MaxMMValues"))
            Case Is > 1000
                iRound = 0
            Case Is > 100
                iRound = 1
            Case Is > 10
                iRound = 2
            Case Is > 1
                iRound = 3
            Case Else
                iRound = 4
        End Select
        Session("iRound") = iRound
        For i = 1 To NR
            'jrs 2023/04/17
            If GRPS(1, i) <> "Kiribati" Then
                If i > 1 Then
                    jScript = jScript & "," & vbCr
                End If
                jScript = jScript & "{" & vbCr
                'jrs 2015/05/13 jScript = jScript & """code"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
                jScript = jScript & """key"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
                jScript = jScript & """value"": " & CType(Math.Round(Session("MMValues")(i), iRound), Double).ToString(CultureInfo.InvariantCulture) & vbCr
                jScript = jScript & "}"
            End If
        Next
        'GreenLand
        jScript = jScript & "," & vbCr
        jScript = jScript & "{" & vbCr
        jScript = jScript & """key"": ""GL""," & vbCr
        jScript = jScript & """value"": " & CType(Math.Round(Session("MMValues")(IFsRegs.Item("Denmark").Index), iRound), Double).ToString(CultureInfo.InvariantCulture) & vbCr
        jScript = jScript & "}"
        jScript = jScript & vbCr
        'WestBank
        jScript = jScript & "," & vbCr
        jScript = jScript & "{" & vbCr
        jScript = jScript & """key"": ""WB""," & vbCr
        jScript = jScript & """value"": " & CType(Math.Round(Session("MMValues")(IFsRegs.Item("Palestine").Index), iRound), Double).ToString(CultureInfo.InvariantCulture) & vbCr
        jScript = jScript & "}"
        jScript = jScript & vbCr
        PrepareDataForMap = jScript
    End Function

    Private Sub cmdFileName_Click(sender As Object, e As EventArgs) Handles cmdFileName.Click
        Dim Message As String
        If txt_FileName.Text = "" Then
            Message = "Please type a File Name."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If


        'jrs 2005/06/13
        Try
            SaveFile(Session("PutFdrvRun") & txt_FileName.Text & ".run.db")
            'jrs 2022/05/25
            Session("IFsRunFiles").Add(txt_FileName.Text & ".run.db", True)
            'jrs 2020/08/28
            If txt_FileName.Text.ToUpper = "IFSBASE" Then
                Session("FileForDisplay1") = Nothing
                DeleteDFInCache(1, Session)
            End If

            'jrs 2023/07/03
            Dim sIP As String
            Dim Files As DataTable
            sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")

            If IsNothing(Session("VersionName")) Then
                SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r, IFsInit as i WHERE r.VersionName = i.Value AND i.Variable = 'VersionName' ORDER BY FileName", "", Files)
            Else
                SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r WHERE r.VersionName = """ & Session("VersionName") & """ ORDER BY FileName", "", Files)
            End If

            Session("IFsRunFiles") = New MyFiles
            LoadRunFiles(Session("IFsRunFiles"), Session("PutFdrvRun2"), Files, Session("mnuSQLite"))

            'jrs 2024/03/05
            'Clear any session that might be preloaded
            'First need to find the index of the file just saved
            Dim FileIdx As Integer
            For FileIdx = 2 To Session("IFsRunFiles").Count - 1
                If txt_FileName.Text & ".run.db" = Session("IFsRunFiles").item(FileIdx).Name Then
                    Exit For
                End If
            Next
            For i = 1 To NumbVars
                Session("df" & V(i) & FileIdx.ToString) = Nothing
            Next
            Session("dfCol") = Nothing
            Session("FileForDisplay" & FileIdx.ToString) = Nothing
            Session("FilesCol") = Nothing
        Catch ThisExcep As System.IO.IOException
            Message = "Incorrect File Name."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End Try


        frm_FileName.Visible = False
        lblJS.Visible = True
        lbl_scenanal.Visible = True

        'jrs 2024/08/12 
        'Need to reload form, because Main Menu changes after adding new scenario file
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        frm_FileName.Visible = False
        lblJS.Visible = True
        lbl_scenanal.Visible = True
    End Sub

    Private Sub mnuBatchRun_Click()
        Session("runTimeHorizon") = Session("LastYear")

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_SelectSceDir.aspx")
    End Sub

    Private Sub RunCompareReportVb6vsNET()
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        'gLoadCompareReport(Session("FirstYear"), Session("FirstYear") + 1, 0.0005, 10, False, Session("IFs_MDB2Active"), Session("FinYear"), IfsPars, Session("PutFdrvRun"), Session("FileNumbr"), Session("RunFiles"))
        gLoadCompareReport(Session("FirstYear"), Session("FirstYear") + 10, 0.0005, 100000, False, Session("IFs_MDB2Active"), Session("FinYear"), IfsPars, Session("PutFdrvRun"), Session("FileNumbr"), Session("RunFiles"))

        Dim msg As String
        msg = "Comparisson Report Created under RUNFILES."

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    'jrs 2022/05/19
    Private Sub RunTranslateVb6toNET()
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))


        gLoadTranslateFromVb6TodotNET(Session("PutFdrvRun") & "ifsBase.run", Session("PutFdrvRun") & "ifsBaseX.XX.run.db",
                                      Session("IFs_MDB2Active"), Session("FinYear"), IfsPars, Session("PutFdrvRun"),
                                      Session("FileNumbr"), Session("RunFiles"), Session("FirstYear"), Session("LastYear"))

        Dim msg As String
        msg = "Translated file ifsBaseX.XX.run.db Created under RUNFILES."

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    'jrs 2020/07/15
    Private Sub CreateCodeToAddTableInNET()
        gCreateCodeToAddTableInNET(Session("PutFdrvRun"), Session("IFs_MDB2Active"))

        Dim msg As String
        msg = "Code to add Tables in .NET Created under RUNFILES."

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''       'JAM 2020-09-10 Calling EdnCohoDisp with friendly URL
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>

    Private Sub ShowEdnCohoDisp(ByVal sParameter As String)

        'JAM 2020-09-10
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")

        'Response.Redirect(vurl)
        If sParameter.IndexOf("Group") >= 0 Then
            Session("IsGroup") = True
        Else
            Session("IsGroup") = False
        End If

        'jrs 2024/04/24 changed default to Base Case
        'Change REgion
        vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", PrepareForFriendlyURL(sParameter), Session("FirstYear"), "Base Case")

        Response.Redirect(vurl)

        '  Support.FormManager.Show(Me, "frm_EdnCohoDisp.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")


    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      JAM 2020-10-10
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function PrepareForFriendlyURL(ByVal sParameter As String) As String
        'Remember to Update JavaScript Function too
        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")



        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If


        PrepareForFriendlyURL = tmp
    End Function

    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''       'jam 2021/02/03 Calling frm_MortCohorts with friendly URL
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>

    Private Sub ShowMortCohoDisp(ByVal sParameter As String)


        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_MortCohorts.aspx")

        'Response.Redirect(vurl)
        If sParameter.IndexOf("Group") >= 0 Then
            Session("IsGroup") = True
        Else
            Session("IsGroup") = False
        End If

        'jrs 2024/04/24 change to use Base Case as default
        'Change REgion
        vurl = FriendlyUrl.Href("~/frm_MortCohorts", PrepareForFriendlyURL(sParameter), Session("FirstYear"), "Base Case")

        'vurl = FriendlyUrl.Href("~/frm_MortCohorts", "KE", Session("FirstYear"), "Base Case")


        'http://www.ifs.du.edu/ifs/frm_MortCohorts.aspx?Country=KE&Year=2015&File=Base%20Case

        Response.Redirect(vurl)

        '  Support.FormManager.Show(Me, "frm_EdnCohoDisp.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")


    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''       'jam 2021/04/03 Calling PopCohortDisp with friendly URL
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>

    Private Sub ShowPopCohoDisp()

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")




        'jrs 2011/05/04
        Dim sPar As String

        If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
            Session("MMCountry") = "AF"
        End If
        If Session("MMCountry") = Nothing Then
            sPar = "Group=" & Session("MMGroup")

        Else
            sPar = "Country=" & Session("MMCountry")

        End If

        If sPar.IndexOf("Group") >= 0 Then
            Session("IsGroup") = True
        Else
            Session("IsGroup") = False
        End If


        'jrs 2024/04/24 changed default to Base Case
        vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", PrepareForFriendlyURL(sPar), Session("FirstYear"), "Base Case")



        Response.Redirect(vurl)


        'jrs 2011/05/04
        'Dim sPar As String
        'If Session("MMCountry") = Nothing And Session("MMGroup") = Nothing Then
        '    Session("MMCountry") = "AF"
        'End If
        'If Session("MMCountry") = Nothing Then
        '    sPar = "Group=" & Session("MMGroup")
        'Else
        '    sPar = "Country=" & Session("MMCountry")
        'End If

        ''JRS 8/7/01 GUI frm_TablFunc2.Show
        ''mtiwebforms April 2004 Population Cohort
        'Support.FormManager.StateStore(Me)
        ''jrs 2012/05/04 added Year
        'Support.FormManager.Show(Me, "frm_PopCohortDisp.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")



    End Sub


    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''       'jam 2021/04/18 Calling CountryProfiel with friendly URL
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>

    Private Sub ShowCountryProfile()

        'JAM 2020-09-10
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_CountryProfile.aspx")


        If Not Session("MMCountry") Is Nothing Then
            'Support.FormManager.Show(Me, "frm_CountryProfile.aspx?Country=" & PrepareForURL(Session("MMCountry")))
            vurl = FriendlyUrl.Href("~/frm_CountryProfile", PrepareForFriendlyURL(Session("MMCountry")))

        Else
            'Support.FormManager.Show(Me, "frm_CountryProfile.aspx")
            vurl = FriendlyUrl.Href("~/frm_CountryProfile")

        End If


        Response.Redirect(vurl)


    End Sub

    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''       'jam 2021/05/29 Calling frm_MorbCohorts with friendly URL
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>

    Private Sub ShowMorbCohoDisp(ByVal sParameter As String)


        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_MorbCohorts.aspx")

        'Response.Redirect(vurl)
        If sParameter.IndexOf("Group") >= 0 Then
            Session("IsGroup") = True
        Else
            Session("IsGroup") = False
        End If

        'Change REgion
        vurl = FriendlyUrl.Href("~/frm_MorbCohorts", PrepareForFriendlyURL(sParameter), Session("FirstYear"), "Base Case")

        'vurl = FriendlyUrl.Href("~/frm_MortCohorts", "KE", Session("FirstYear"), "Base Case")


        'http://www.ifs.du.edu/ifs/frm_MorbCohorts.aspx?Country=KE&Year=2015&File=Base%20Case

        Response.Redirect(vurl)

        '  Support.FormManager.Show(Me, "frm_EdnCohoDisp.aspx?" & PrepareForURL(sPar) & "&Year=" & Session("FirstYear") & "&File=Base Case")


    End Sub

    'jrs 2018/12/18 new procedure to fill "MostRecent" with values close to Base Year
    'Idea related to using a base year of 2003, when we currently have a base year of 2015
    Private Sub mnuMostRecentAdjData_Click()

        Dim tdef As DataTable
        Dim TableName$, TCount%
        Dim RangeofYears%
        Dim ReadRecent As Single?
        Dim FCount%, TotFields%, ThisYear%, LatestYear%, ReadYear$, BaseYear4Tbl%, MinDiff%, MinDiffYear%

        'jrs 2021/07/21
        'If Not Session("bGuidedBaseYearChange") Then
        '    Dim msg$ = "This process will add a Most Recent column to all Series of the Historical "
        '    msg$ = msg$ + " data file that do not have such a column.  Do you wish to continue?"
        '    Response = MsgBox(msg$, 4)
        '    If Response <> 6 Then Exit Sub
        'End If

        RangeofYears = 10

        'CEDPRIINT(ICount%) = ReadRecent(EDPriIntTbl, "1995", 5)
        'Dim Definition$

        'Dim DbMemo As Database
        'Dim ValTblMemo As Recordset

        Dim db As SqliteConnection
        db = New SqliteConnection("DataSource=" & PutFdrv$ + "DATA\IFSHistSeries.db;")
        db.Open()
Dim vSQLiteCommand As SqliteCommand
Dim vSQLiteReader As SqliteDataReader
        Dim dtSchema As DataTable
        dtSchema = New DataTable
vSQLiteCommand = db.CreateCommand()

'jrs 2022/12/13
        vSQLiteCommand.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
        vSQLiteCommand.ExecuteNonQuery()

        'Dim TotalTables = vSQLiteReader.RecordsAffected

        Dim rsDD As DataTable
        SetDataTablefromSQLite(PutFdrv$ + "DATA\DataDict.db", "SELECT * FROM DataDict WHERE UsedInPreprocessor = 1", "", rsDD)

        'With frm_Progress
        '    '.ZOrder 0
        '    .Caption = "Processing History Series (recent). Please Wait..."
        '    .ProgressBar1.MIN = 1
        '    .ProgressBar1.Max = TotalTables
        '    .ProgressBar1.value = .ProgressBar1.MIN
        'End With
        'frm_Progress.Show 0 'JRS 8/25/01 GUI , frm_ScenarioCreate
        TCount% = 0
        'jrs 2009/06/16
        Dim bText As Boolean

        While (TCount% < rsDD.Rows.Count)
            TableName = rsDD.Rows(TCount%).Item("TABLE")
            'jrs 2009/06/16
            bText = False
            'If tdef.Attributes = dbSystemObject  Then
            If UCase(Mid(TableName, 1, 6)) = "SERIES" Then

                If TCount% > 0 Then
                    vSQLiteReader.Close()
                End If
                vSQLiteCommand.CommandText = "SELECT * FROM [" & TableName & "]"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                dtSchema = vSQLiteReader.GetSchemaTable

                TotFields% = vSQLiteReader.FieldCount
                LatestYear% = 100
                BaseYear4Tbl = 100
                'jrs 2009/06/16
                Dim EarliestYear% = 3000
                MinDiff% = 2000
                MinDiffYear% = 100

                For FCount% = 0 To TotFields - 1
                    'JRS 06/24/03
                    If Not dtSchema.Rows(FCount%).Item(0).Contains("-") Then
                        ThisYear% = Val(dtSchema.Rows(FCount%).Item(0))
                    End If
                    'jrs 2009/02/19 If ThisYear% > LatestYear% And ThisYear% < 3000 Then
                    'there are variables with forecast saved in the series (SmokingImpact, DiabetesPrev)
                    'forecast shouldn't be saved as most recent
                    If ThisYear% > LatestYear% And ThisYear% <= AMAX(Year(Now()), FirstYearForecast) Then 'jrs 2016/01/13 added AMAX(xxx, BaseYear)
                        LatestYear% = ThisYear%
                        'jrs 2024/04/04 changed from (13) to (24)
                        'jrs 2009/06/16
                        bText = (dtSchema.Rows(FCount%).Item(24) = "VARCHAR")
                        If Math.Abs(LatestYear% - FirstYearForecast) < MinDiff Then
                            MinDiff = Math.Abs(LatestYear% - FirstYearForecast)
                            MinDiffYear% = LatestYear%
                        End If
                    End If
                    'jrs 2009/06/16
                    If ThisYear% < EarliestYear% And ThisYear% > 100 Then
                        EarliestYear% = ThisYear%
                    End If

                    If ThisYear% = FirstYearForecast Then
                        BaseYear4Tbl = ThisYear%
                    End If
                Next FCount%

                If BaseYear4Tbl = 100 Then
                    BaseYear4Tbl = MinDiffYear
                End If
                'If LatestYear% = 0 Then Loop
                'On Error Resume Next
                Dim bAdd As Boolean
                bAdd = (dtSchema.Select("ColumnName = 'MostRecent'").Count = 0)
                'tdef.Fields.Append tdef.CreateField("Country", dbText)
                'JRS 06/24/03
                If LatestYear% > 100 Then 'valid year columns
                    'jrs 2009/06/16
                    If bAdd Then
                        Dim vSQLiteCommand2 As SqliteCommand
                        vSQLiteCommand2 = db.CreateCommand()
                        If Not bText Then
                            vSQLiteCommand2.CommandText = "ALTER TABLE " & TableName & " ADD COLUMN ""MostRecent"" DOUBLE(53);"
                        Else
                            vSQLiteCommand2.CommandText = "ALTER TABLE " & TableName & " ADD COLUMN ""MostRecent"" VARCHAR(255);"
                        End If
                        vSQLiteCommand2.ExecuteReader()

                    End If

                    'Set valtbl = db.OpenRecordset(TableName)

                    'Using trn = db.BeginTransaction()
                    'Using stmt = db.CreateCommand()
                    Dim stmt = db.CreateCommand()
                    stmt.CommandText = "UPDATE [" & TableName & "] SET [MostRecent] = $MostRecent WHERE Country = $Country;"
                    'jrs 2024/03/26 AddParameter(stmt, "$MostRecent", SqliteType.Real)
                    AddParameter(stmt, "$MostRecent", DbType.String)
                    AddParameter(stmt, "$Country", DbType.String)

                    'valtbl.MoveFirst

                    'For each country
                    Do While vSQLiteReader.Read
                        ReadRecent = Nothing
                        'jrs 2022/09/19 Only do this for old years, more than 7 years 
                        If FirstYearForecast < Year(Now()) - 7 Then
                            If Not IsDBNull(vSQLiteReader((BaseYear4Tbl).ToString.Trim)) Then
                                ReadRecent = CSng(vSQLiteReader((BaseYear4Tbl).ToString.Trim))
                            End If
                        Else 'jrs 2022/09/19 Use the regular Most Recent concept
                            If Not IsDBNull(vSQLiteReader((LatestYear).ToString.Trim)) Then
                                ReadRecent = CSng(vSQLiteReader((LatestYear).ToString.Trim))
                            End If
                        End If

                        'jrs 2009/06/16 For i2 = 1 To TotFields 'jrs 2007/05/03 RangeofYears
                        For i2 = 1 To LatestYear% - EarliestYear% + 1
                            'jrs 2022/09/19 Only do this for old years, more than 7 years 
                            If FirstYearForecast < Year(Now()) - 7 Then
                                If Not ReadRecent.HasValue And BaseYear4Tbl + i2 <= LatestYear% Then
                                    'jrs 2009/06/16 ReadRecent = valtbl.Fields(Trim(Str(LatestYear% - i2)))
                                    If dtSchema.Select("ColumnName = '" & (BaseYear4Tbl + i2).ToString & "'").Count > 0 AndAlso IsNumeric(vSQLiteReader((BaseYear4Tbl + i2).ToString.Trim)) AndAlso Not (vSQLiteReader((BaseYear4Tbl + i2).ToString.Trim) = Nothing) Then
                                        ReadRecent = CSng(vSQLiteReader((BaseYear4Tbl + i2).ToString.Trim))
                                    End If
                                End If

                                If Not ReadRecent.HasValue And BaseYear4Tbl - i2 >= EarliestYear% Then
                                    'jrs 2009/06/16 ReadRecent = valtbl.Fields(Trim(Str(LatestYear% - i2)))
                                    If dtSchema.Select("ColumnName = '" & (BaseYear4Tbl - i2).ToString & "'").Count > 0 AndAlso IsNumeric(vSQLiteReader((BaseYear4Tbl - i2).ToString.Trim)) AndAlso Not (vSQLiteReader((BaseYear4Tbl - i2).ToString.Trim) = Nothing) Then
                                        ReadRecent = CSng(vSQLiteReader((BaseYear4Tbl - i2).ToString.Trim))
                                    End If
                                End If
                            Else 'jrs 2022/09/19 Use the regular Most Recent concept
                                If Not ReadRecent.HasValue And LatestYear% - i2 >= EarliestYear% Then
                                    'jrs 2009/06/16 ReadRecent = valtbl.Fields(Trim(Str(LatestYear% - i2)))
                                    If dtSchema.Select("ColumnName = '" & (LatestYear% - i2).ToString & "'").Count > 0 AndAlso IsNumeric(vSQLiteReader((LatestYear% - i2).ToString.Trim)) AndAlso Not (vSQLiteReader((LatestYear% - i2).ToString.Trim) = Nothing) Then
                                        ReadRecent = CSng(vSQLiteReader((LatestYear% - i2).ToString.Trim))
                                    End If
                                End If
                            End If

                            If ReadRecent.HasValue Then
                                'Dim vSQLiteCommand3 As SqliteCommand
                                'vSQLiteCommand3 = db.CreateCommand()

                                'vSQLiteCommand3.CommandText = "UPDATE " & TableName & " SET MostRecent = " & ReadRecent & " WHERE Country = """ & vSQLiteReader("Country") & """;"
                                'vSQLiteCommand3.ExecuteReader()
                                'jrs 2024/03/26  convert to string to avoid extra decimals
                                stmt.Parameters(0).Value = ReadRecent.ToString
                                stmt.Parameters(1).Value = vSQLiteReader("Country")
                                stmt.ExecuteNonQuery()
                                Exit For
                            End If

                            If BaseYear4Tbl + i2 > LatestYear% And BaseYear4Tbl - i2 < EarliestYear% Then Exit For
                        Next i2
                    Loop 'Next country

                    'trn.Commit()
                    'JRS 06/24/03End If
                    'End Using
                    'End Using 'Begin Transaction
                End If 'LatestYear% > 100 Then 'valid year columns
            End If 'Series

            TCount% = TCount% + 1
            'frm_Progress.ProgressBar1.value = TCount%
            'frm_Progress.Refresh

        End While

        'Unload frm_Progress
        db.Close()

        'jrs 2023/02/11
        'Copy File to RUNFILES
        Dim InFile = DataFileDir$ + "ifsHistSeries.db"
        Dim OutFile As String = Session("PutFdrvRun") + "ifsHistSeries.db"
        FileCopyCheck(InFile$, OutFile$)

    End Sub

    'jrs 2022/12/12
    Private Sub mnuGuidedBaseYearChange_Click()
        If txtConfirm.Value = 0 Then Exit Sub
        '1. change to ifsinit
        Dim dbInit As SqliteConnection
        dbInit = New SqliteConnection("DataSource=" & Replace(DataFileDir$, "DATA\", "") & "ifsInit.db;")
        dbInit.Open()
Dim dbInitCmd As SqliteCommand = dbInit.CreateCommand
        dbInitCmd.CommandText = "UPDATE IfsInit SET [Value] = """ & txtNewBaseYear.Value & """ WHERE [Variable] = ""FirstYearForecast%"""
dbInitCmd.ExecuteNonQuery()
        dbInitCmd.CommandText = "UPDATE IfsInit SET [Value] = """ & txtNewBaseYear.Value & """ WHERE [Variable] = ""LastYearHistory%"""
        dbInitCmd.ExecuteNonQuery()
        dbInit.Close()
        FirstYearForecast = CInt(txtNewBaseYear.Value)
        LastYearHistory = CInt(txtNewBaseYear.Value)
        'BaseYear = NewYear

        '2. pulls population data
        LoadImpPopData(Session("IFs_MDB2Active"), Session("PutFdrvRun"))

        '3. adjusts most recent data
        mnuMostRecentAdjData_Click()

        '4. reruns the preprocessor
        Session("HistoricBase") = "False"
        Session("ActiveBase") = "IFSBASE.RUN"
        Session("WorkingFile") = "IFSBASE.RUN.DB"
        Session("RunFiles")(1) = Session("PutFdrvRun") + "IFSBASE.RUN.DB"
        Session("FirstYear") = FirstYearForecast
        Session("LastYear") = Session("LastYearForecast")

        Session("GuidedBaseYear") = 1

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_RebuildBase.aspx")

    End Sub

    'jrs 2023/06/02
    Private Sub UpdateIfsInit()
        Dim sIP As String
        sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.db")
        Dim sSQL As String = "UPDATE IFsInit SET [Value] = """ & CStr(Session("mnuTabs")) & """ WHERE [Variable] = 'OpenNewTabs'"
        updateSQLiteWith1Command(sIP, sSQL)
    End Sub

    'jrs 2025/05/19 
    Private Sub Check4MissingGroups(sDir As String, Reportfile As Integer)
        'check if last character is "\" then remove it
        If sDir.Substring(sDir.Length - 1) = "\" Then
            sDir = sDir.Substring(0, sDir.Length - 1)
        End If
        'If sDir = "C:\My Documents\IFs\SCENARIO\EdMFP Scenarios\QuantSce" Then Stop
        'Check content of each sce file 
        Dim sceFile As String
        ' Returns filename with specified extension. If more than one *.ini
        ' file exists, the first file found is returned.
        sceFile = Dir(sDir + "\*.sce")
        Do While (sceFile <> "")
            'If sDir = "C:\My Documents\IFs\SCENARIO\World Integrated Scenario Sets\COVID Tests" And sceFile = "WExp20PerDown6Sec.sce" Then Stop
            CheckIndividualSce4MissingGroups(sDir & "\" & sceFile, Reportfile)
            ' Call Dir again without arguments to return the next *.sce file in the
            ' same directory.
            sceFile = Dir()
        Loop
        'and then go to folders 
        Dim sceFolder As String
        sceFolder = Dir(sDir + "\*", vbDirectory)
        Dim sFolders As List(Of String) = New List(Of String)
        Do While (sceFolder <> "")
            sFolders.Add(sceFolder)
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            sceFolder = Dir()
        Loop

        For Each fld In sFolders
            Check4MissingGroups(sDir & "\" & fld, Reportfile)
        Next

    End Sub

    'jrs 2025/05/19
    Private Sub CheckIndividualSce4MissingGroups(sceFile As String, Reportfile As Integer)
        'Open File
        Dim fNum = FreeFile()
        FileOpen(fNum, sceFile, OpenMode.Input)
        Dim Scenariotype As String, Var As String
        Dim SceBaseYear As Integer, Lag As Integer
        Dim LastVar As String, VLine As String
        Dim chk As Integer
        Dim Dim1 As String, Dim2 As String, OutputLine As String

        'Loop through file
        Do While Not EOF(fNum)
            'Identify parameter
            Input(fNum, Scenariotype)

            'Comments start
            If Scenariotype = "" Then
                Exit Do
            End If

            Input(fNum, Var)

            If Scenariotype.Substring(0, 3) = "COM" Then
                Exit Do
            End If

            If Scenariotype = "BASEYEAR" Then
                SceBaseYear = CInt(Var)
                Input(fNum, Scenariotype)
                Input(fNum, Var)
                Lag = SceBaseYear - FirstYearForecast
            End If

            If Var <> LastVar Then
                'search to match variable number       
                chk = IfsVars.Item(Var).Index
                LastVar = Var
            End If

            If chk > 0 Then
                If VDim1(chk) = 1 Then
                    Input(fNum, Dim1)
                    'Check if not Region
                    If Not IsNumeric(Dim1) And IFsRegs.Item(Dim1).Index = -1 Then
                        'Check for Region with commas
                        Input(fNum, Dim2)
                        If IFsRegs.Item(Dim1 & ", " & Dim2).Index = -1 Then
                            'and not a Group
                            If gGetGroupIdx(Dim1, False, Me) = -1 Then
                                'Write down if not found in Groups
                                OutputLine = sceFile & "," & Dim1
                                PrintLine(Reportfile, OutputLine)
                            End If
                        End If
                    End If
                End If
            Else
                'Write down if not found in Groups
                OutputLine = sceFile & "," & Var
                PrintLine(Reportfile, OutputLine)
            End If
            VLine = LineInput(fNum) 'read rest of line
            If VLine.Substring(0, 3) = "COM" Then Exit Do
        Loop
        'Close File
        FileClose(fNum)
    End Sub
End Class


