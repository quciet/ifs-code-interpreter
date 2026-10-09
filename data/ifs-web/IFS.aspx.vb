'===========================================================================
' This file was modified as part of an ASP.NET 2.0 Web project conversion.
' The class name was changed and the class modified to inherit from the abstract base class 
' in file 'App_Code\Migrated\Stub_IFS_aspx_vb.vb'.
' During runtime, this allows other classes in your web application to bind and access 
' the code-behind page using the abstract base class.
' The associated content page 'IFS.aspx' was also modified to refer to the new class name.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================

Option Strict Off
Option Explicit On 
'mtiwebformsMarch2004 (2)ScenarioCreate
Imports System.IO

Imports VB = Microsoft.VisualBasic


'Namespace IFsWeb


'Partial Class frm_IFS
Partial Class Migrated_frm_IFS

        Inherits frm_IFS
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
            'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.
            If Not Page.IsPostBack Then
                If Support.FormManager.LoadState Then
                    Support.FormManager.StateRestore(Me)
                    Support.FormManager.LoadState = False
                End If

                'jrs 2007/11/29
                'check for query string, in case sending variables from other pages
                If Not IsNothing(Request.QueryString("LastYearForecast")) Then
                    Session("LastYearForecast") = Request.QueryString("LastYearForecast")
                End If
                If Not IsNothing(Request.QueryString("VersionName")) Then
                    Session("VersionName") = Request.QueryString("VersionName")
                End If
                'jrs 2011/05/20
                If Not IsNothing(Request.QueryString("DisplayLastYear")) Then
                    Session("DisplayLastYear") = Request.QueryString("DisplayLastYear")
                    If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                        Session("DisplayLastYear") = 2030
                    End If
                Else
                    'jrs 2011/12/05
                    If Session("DisplayLastYear") > Session("LastYearForecast") Then
                        Session.Remove("DisplayLastYear")
                    End If
                End If

                'JRS 02/08/04
                MyLoad()

            Else
                'JRS 02/11/04
                MyReLoad()
            End If
            'jrs 2011/12/05 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
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
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
#End Region
        'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
        '#Region "Upgrade Support "
        'Private Shared m_vb6FormDefInstance As frm_IFSPrivate Shared m_InitializingDefInstance As Boolean
        'Public Shared Property DefInstance() As frm_IFS
        'Get 
        'If m_vb6FormDefInstance Is Nothing OrElse m_vb6Form.IsDisposed Then 
        'm_InitializingDefInstance = True
        'm_vb6FormDefInstance = New frm_IFS()
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
        '------------------------------------------IFs.Frm-----------------------------------------
        '------------------------------------------------------------------------------------------
        '
        '
        '
        '   Copyright © by Barry B. Hughes 1988-2003
        '   Model code available for review and education only
        '   Reproduction or incorporation in other software by permission only
        '   Last modified by BBH on November 17, 2001
        '

        'It's not the critic who counts, not the ones who point out where the strong
        'stumbled, or how the doer could have done better.  The credit belongs to the
        'one who is in the arena, whose face is marred by dust and sweat and blood, who
        'strives valiantly, who errs and falls short again and again.  There is no effort
        'without error.

        'But the one who tries, who knows the great enthusiams, the great devotions,
        'who is spent in a worthy cause, at best knows the triumph of achievement, and
        'at the worst, fails while daring.  Their place shall never be with those cold and
        'timid souls who know neither victory or defeat.

        'Theodore Roosevelt


        Dim LocalDirect As String

        'Commented code is VB 3.0 Version
        'Private Declare Function GetPrivateProfileString Lib "Kernel" _
        '(ByVal lpApplicationName As String, _
        'ByVal lpKeyName As String, _
        'ByVal lpDefault As String, _
        'ByVal lpReturnedString As String, _
        'ByVal nSize As Integer, _
        'ByVal lpFileName As String) As Integer
        'Private Declare Function GetWindowsDirectory Lib "Kernel" _
        '(ByVal lpBuffer As String, _
        'ByVal nSize As Integer) As Integer

        'CONVERSION_TODO: The following code was converted according to the value of the conditional statement.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1024.html'.
        'JRS 02/05/04 #If Win32 Then ' 32-bit VB uses this Declare
        'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1016"'
        'JRS 01/24/04
        Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
        'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1016"'
        'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1016"'
        'JRS 01/24/04
        Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
        Private Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Integer) As Integer
        'JRS 02/05/04 #Else '16-bit VB uses this Declare
        'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Else did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1035"'
        'Private Declare Function GetPrivateProfileString Lib "Kernel.dll" _
        '        (ByVal lpApplicationName As String, _
        '        ByVal lpKeyName As Any, _
        '        ByVal lpDefault As String, _
        '        ByVal lpReturnedString As String, _
        '        ByVal nSize As Integer, _
        '        ByVal lpFileName As String) As Integer
        'Private Declare Function WritePrivateProfileString Lib "Kernel.dll" _
        '        (ByVal lpApplicationName As String, _
        '        ByVal lpKeyName As Any, _
        '        ByVal lpString As Any, _
        '        ByVal lplFileName As String) As Integer
        'Private Declare Function GetWindowsDirectory Lib "Kernel.dll" _
        '        (ByVal lpBuffer As String, _
        '        ByVal nSize As Integer) As Integer
        'JRS 02/05/04 #End If
        'JRS 8/18/01 GUI
        Dim valtblInit As DataTable 'DAOJR.Recordset
        Dim valtbl As DataTable


        '==========================frm_IFs=============================
        Private Sub Command1_Click()
            '--------------------------------------------------------------

            cmd_Exit.Enabled = 0
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_MainMenu.aspx")
            'frm_MainMenu.Show() 

        End Sub

        '==========================frm_IFs=============================
        'jrs 2004/11/23
        Public Sub cmd_Exit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit.Click
            '--------------------------------------------------------------

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
            cmd_Exit.Enabled = 0
            cmd_Exit.Text = "Please wait.."

            '   Read all initialization files
            '   -----------------------------

            'JRS 02/06/04
            'jrs 2011/03/24 Call IFSinit()
            'jrs 2008/07/30
            Session.Remove("sessionMap")
            'kia 2008/07/28
            Dim resval As String
            resval = Request.Form("txtResValueHolder").ToString
            'JRS 02/24/04
            'Call GUIInit(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"))

            'JRS 8/18/07 GUI
            'JRS 04/24/04 valtblInit.Close()
            'JRS 04/24/04 dbInit.Close()

            'jrs 2004/11/23
            'jrs 2005/08/04 If Session("SessionIdx") = -1 Then
            'JRS 01/24/04
            'CONVERSION_NOTE: Property 'System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.WindowState.html'.
            'frm_MainMenu.WindowState = FormWindowState.Normal 
            'CONVERSION_ISSUE: Property 'IFs.frm_MainMenu.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            'JRS 02/06/04
            Support.FormManager.StateStore(Me)
            'kia 2008/07/28
            Dim findex As Integer

            Dim width = resval.Substring(0, resval.IndexOf(","))
            findex = resval.IndexOf(",") + 1
            Dim height = resval.Substring(findex, resval.Length - resval.IndexOf(",") - 1)

            Support.FormManager.Show(Me, "frm_MainMenu.aspx?W=" & width & "&H=" & height)
            'Support.FormManager.Show(Me, "frm_MainMenu.aspx")
            'frm_MainMenu.Show() 
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            'JRS 02/06/04
            Support.FormManager.Close(Me)
            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
            'jrs 2005/08/04  End If
        End Sub


        '==========================frm_IFs=============================
        'CONVERSION_NOTE: The code of Event 'frm_IFS_Load' was moved to Method 'Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
        Public Sub frm_IFS_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
            'mti WebForm Changes to be taken by Jose
            cmd_Exit.Attributes.Add("onClick", "showhg();")
        End Sub

        '==========================frm_IFs=============================
        Private Function GetWindowsDir() As String
            Dim xtemp As Object
            Dim temp As String
            '--------------------------------------------------------------

            temp = New String(Chr(0), 145) ' Size Buffer
            'UPGRADE_WARNING: Couldn't resolve default property of object xtemp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            xtemp = GetWindowsDirectory(temp, 145) ' Make API Call
            'UPGRADE_WARNING: Couldn't resolve default property of object xtemp. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            temp = VB.Left(temp, xtemp) ' Trim Buffer

            If VB.Right(temp, 1) <> "\" Then ' Add \ if necessary
                GetWindowsDir = temp & "\"
            Else
                GetWindowsDir = temp
            End If

        End Function

        'JRS 02/11/04
        Public Overrides Sub MyReLoad()
            valtblInit = Session("valtblInit")
        End Sub

        'JRS 02/08/04
        'jrs 2004/11/23
        Public Overrides Sub MyLoad(Optional ByVal SessionIdx As Integer = -1)
            If SessionIdx >= 0 Then
                Session("SessionIdx") = SessionIdx
            End If

            Dim msg As String
            'Dim ErrMsg As String
            ''jrs 2008/05/08 Dim CurrencyCountry As String
            'Dim IFsCaption As String

            ''JRS 01/24/04
            'Dim loadtbl As DataTable 'JRS 04/24/04 DAOJR.Recordset 'Object
            ''--------------------------------------------------------------

            ''   In reality this kind of work is never finished.
            ''   One has to declare it finished when,
            ''   in accord with time and circumstance,
            ''   one has done the utmost.
            ''          Goethe

            ''   Set model version parameters
            ''   ----------------------------

            ''JRS 8/16/01 GUI BEGIN
            ''   Open File to retrive path parameters
            ''   -----------------------------------------------------------------
            ''JRS 8/28/01 GUI Save the location of the init file in the registry
            'Dim sIP As String
            ''JRS 02/08/04 sIP = VB6.GetPath & "\IFsInit.mdb"
            'Dim DInf As New DirectoryInfo(Me.MapPathSecure(TemplateSourceDirectory))
            'sIP = DInf.Parent.FullName & "\IFsInit.mdb"

            ''jrs 2005/01/18 SaveSetting("IFS", "GUI", "InitPath", sIP)

            'SetDataTablefromAccess(sIP, "SELECT * FROM IFsInit", "Variable", valtblInit)
            ''JRS 8/16/01 GUI END

            ''JRS 02/11/04 *** Store Session Variables *****
            'Session("valtblInit") = valtblInit
            '' ********************************************

            ''   Determine model load
            ''   --------------------

            'If SeekValue(valtblInit, "BaseModelCoVaTra", "Value") Then ModelLoad = "LoadCoVaTrA"
            'If SeekValue(valtblInit, "BaseModelHowHappy", "Value") Then ModelLoad = "LoadHowHappy"
            'If SeekValue(valtblInit, "BaseModelProfessional", "Value") Then ModelLoad = "LoadProfessional"
            'If SeekValue(valtblInit, "BaseModelSAG", "Value") Then ModelLoad = "LoadSAG"
            'If SeekValue(valtblInit, "BaseModelStudent", "Value") Then ModelLoad = "LoadStudent"
            'If SeekValue(valtblInit, "BaseModelTERRA", "Value") Then ModelLoad = "LoadTERRA"
            'If SeekValue(valtblInit, "BaseModelFull", "Value") Then ModelLoad = "LoadFull"
            ''JRS 05/22/03 RAND PARDEE
            'If SeekValue(valtblInit, "BaseModelRANDPardee", "Value") Then ModelLoad = "LoadRANDPardee"

            'SetDataTablefromAccess(sIP, "SELECT * FROM " & ModelLoad, "Variable", loadtbl)

            ''   Read options specific to loads
            ''   ------------------------------


            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'DemandFile = SeekValue(loadtbl, "DemandFile$", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'GroupIDNew = SeekValue(loadtbl, "GroupIDNew$", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'Grouping = SeekValue(loadtbl, "Grouping$", "Value")
            ''if true, HistoricBase$ records whether or not actually used
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'HistoricOption = SeekValue(loadtbl, "HistoricOption$", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'IFsCaption = SeekValue(loadtbl, "IFsCaption", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'ModelName = SeekValue(loadtbl, "ModelName$", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'ModelVersion = SeekValue(loadtbl, "ModelVersion$", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'Regionalization = SeekValue(loadtbl, "Regionalization$", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'RegionIDNew = SeekValue(loadtbl, "RegionIDNew$", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'VCATFile = SeekValue(loadtbl, "VCATFile$", "Value")
            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'RegionalIOCoefTabl = SeekValue(loadtbl, "RegionalIOCoefTabl$", "Value")

            ''jrs 2008/05/08 changed to a session variable
            'Session("CurrencyCountry") = SeekValue(loadtbl, "CurrencyCountry$", "Value")

            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'SupplementalDataFile = SeekValue(loadtbl, "SupplementalDataFile$", "Value")

            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'SupplementalHistoricDataFile = SeekValue(loadtbl, "SupplementalHistoricDataFile$", "Value")

            ''UPGRADE_WARNING: Couldn't resolve default property of object loadtbl(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'MaxReg = SeekValue(loadtbl, "MaxReg", "Value")


            ''   Read generic, not-load specific options
            ''   ---------------------------------------

            'DevelopmentMode = SeekValue(valtblInit, "DevelopmentMode$", "Value")
            ''False means distribution mode
            ''Must set to false before compiling
            'AbbCodeMode = SeekValue(valtblInit, "AbbCodeMode$", "Value")
            ''True means simplified version for code distribution

            ''    If ModelLoad$ = "LoadStudent" Then     'When switch between Prof/Student
            ''        valtblInit.Seek "=", "MaxRegStudent"  'must rebuild common and base
            ''        MaxReg% = valtblInit("Value")
            ''    Else
            ''        valtblInit.Seek "=", "MaxRegNoStudent"
            ''        MaxReg% = valtblInit("Value")     'Leave room for "ALL" regions
            ''    End If

            ''JRS 8/16/01 GUI ChartControl = "PROESSENTIAL"  '"ORIGINAL" (BPS/Pinnacle),"MSCHART", "PROESSENTIAL"
            ''    ChartControl = "ORIGINAL"

            ''One or the other should be true
            'ESRIMap = SeekValue(valtblInit, "ESRIMap$", "Value")
            'BMMap = SeekValue(valtblInit, "BMMap$", "Value")

            ''   Set run horizon
            ''   ---------------

            'FirstYearForecast = SeekValue(valtblInit, "FirstYearForecast%", "Value")
            ''jrs 2011/03/23
            'Dim iLastYearForecast As Integer
            'iLastYearForecast = SeekValue(valtblInit, "LastYearForecast%", "Value")
            ''jrs 2005/06/20
            'If Not IsNothing(Session("LastYearForecast")) Then
            '    'validate
            '    If FirstYearForecast > Session("LastYearForecast") Or iLastYearForecast < Session("LastYearForecast") Then
            '        Session("LastYearForecast") = iLastYearForecast
            '    End If
            'Else
            '    Session("LastYearForecast") = iLastYearForecast
            'End If
            'FirstYearHistory = SeekValue(valtblInit, "FirstYearHistory%", "Value")
            'LastYearHistory = SeekValue(valtblInit, "LastYearHistory%", "Value")
            'Session("FirstYear") = FirstYearForecast 'Default to forecast, not history
            Session("LastYear") = Session("LastYearForecast")
            'jrs 2011/05/20 Session("DisplayLastYear") = Nothing
            Session("oldLastYear") = Nothing

            ''UPGRADE_WARNING: Couldn't resolve default property of object MODYEARI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''JRSVBNET MODYEARI = FirstYearForecast
            ''UPGRADE_WARNING: Couldn't resolve default property of object RunYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'Session("RunYears") = 0 'For Cohort population data plot

            ''   Set parameters that keep track of whether base has been rebuilt or model run
            ''   ----------------------------------------------------------------------------

            'RebuiltBase = SeekValue(valtblInit, "RebuiltBase$", "Value")
            'RunModel = SeekValue(valtblInit, "RunModel$", "Value")

            ''   Read Password for data file
            ''   ---------------------------

            ''JRS 05/07/04 DataPW = "[REDACTED]" + SeekValue(valtblInit, "DataPW$", "Value") + ";"
            'DataPW = SeekValue(valtblInit, "DataPW$", "Value")
            ''jrs 2008/01/03
            'DataPW = DataPW & "12"
            ''DataPW$ = "[REDACTED]"

            ''   Establish start directory so that can check with settings in .INI
            ''   -----------------------------------------------------------------

            ''jrs 2005/01/18 
            ''If CBool(DevelopmentMode) = True Then
            ''    StartDirec = SeekValue(valtblInit, "StartDirec$", "Value")
            ''Else
            ''    StartDirec = CurDir() 'Standard execution mode for user after install
            ''End If

            ''mtiwebformsMarch2004 (2)ScenarioCreate
            ''jrs 2005/01/18 Dim DInfo As New DirectoryInfo(Me.MapPathSecure(""))
            'StartDirec = DInf.Parent.FullName

            ''mtiwebformsMarch2004 (2)ScenarioCreate

            'If VB.Right(StartDirec, 1) <> "\" Then StartDirec = StartDirec & "\"

            ''    msg$ = "Passed StartDirec$, which is " + StartDirec$
            ''    MsgBox msg$
            ''    msg$ = "Demand File is " + DemandFile$
            ''    MsgBox msg$


            ''   Check for existence of IFs on StartDirec$ except when running from My Documents
            ''   -------------------------------------------------------------------------------

            'ErrMsg = "IFs does not exist on " & StartDirec & ", please specify path."
            'On Error GoTo StartDirecProblem
            ''JRS 2005/01/18 FileOpen(1, StartDirec & DemandFile, OpenMode.Input)
            ''JRS 2005/01/18 FileClose(1)
            ''Note:  During development, it is necessary to have the appropriate .exe file
            ''on the My Documents path, e.g. IFs.exe in order to get past this test when
            ''DevelopmentMode has been set to False in preparation for the creation of a
            ''distribution version.

            ''   Read Settings from IFS.INI File
            ''   -------------------------------

            'On Error GoTo IniError
            'WinDir = UCase(GetWindowsDir())
            'IFSINIFile = WinDir & "IFS.INI"
            ''    msg$ = "Ready to read .ini file, IFsINiFile$ is " + IFSINIFile$
            ''    MsgBox msg$

            'Read_INI_File()

            ''    msg$ = "Back from reading .ini file, IFsINiFile$ is " + IFSINIFile$
            ''    MsgBox msg$

            ''jrs 2008/05/08 UpdateCurrencyCountry((CurrencyCountry))

            ''    msg$ = "Back from Updating Currency "
            ''    MsgBox msg$

            ''CONVERSION_NOTE: Property 'System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Top.html'.
            ''Top = 0 
            ''CONVERSION_NOTE: Property 'System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Left.html'.
            ''Left = 0 
            ''CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            ''JRSVBNET Height = 'JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
            ''CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
            ''JRSVBNET Width = 'JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
            ''
            ''   Size and Position Map:  Size First Because Graphics File Determines
            ''   -------------------------------------------------------------------

            ''JRSVBNET WorldMap.Height = System.Web.UI.WebControls.Unit.Pixel((Inch * 3 + HalfInch - QuarterInch))
            ''JRSVBNET WorldMap.Width = System.Web.UI.WebControls.Unit.Pixel((Inch * 6))

            ''CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            ''JRSVBNET WorldMap.Style.Item("TOP") = (((768) - (WorldMap.Height) - (lbl_IFSExplain.Height) - HalfInch) / 2)
            ''CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            ''JRSVBNET WorldMap.Style.Item("LEFT") = (((1024) - (WorldMap.Width)) / 2)
            ''CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.PictureBox.Image' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            ''JRSVBNET Support.ControlManager.SetControlImage(WorldMap, WorldMapLocation)
            '
            '   Position and Size Continue Button
            '   ---------------------------------

            'JRSVBNET cmd_Exit.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Web.UI.WebControls.Unit.Parse(WorldMap.Style.Item("TOP"))) + VB6.PixelsToTwipsY(WorldMap.Height) + HalfInch)
            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                'JRSVBNET cmd_Exit.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(WorldMap.Style.Item("LEFT"))) + VB6.PixelsToTwipsX(lbl_IFSExplain.Width) + Inch * 9 / 10)
            Else
                'JRSVBNET cmd_Exit.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(WorldMap.Style.Item("LEFT"))) + VB6.PixelsToTwipsX(lbl_IFSExplain.Width) + Inch * 5 / 10)
            End If
            '
            '   Position and File Labels
            '   ------------------------

            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Carret = "<br>" & Chr(10)
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            carret2 = Carret + Carret

            'JRSVBNET lbl_IFSExplain.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Web.UI.WebControls.Unit.Parse(WorldMap.Style.Item("TOP"))) + VB6.PixelsToTwipsY(WorldMap.Height))
            'JRSVBNET lbl_IFSExplain.Style.Item("LEFT") = VB6.TwipsToPixelsX(Inch)
            lbl_IFSExplain.Style.Item("LEFT") = System.Web.UI.WebControls.Unit.Parse(WorldMap.Style.Item("LEFT")).Value
            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = "Based on International Futures (IFs)" + Carret
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Created by Barry B. Hughes" + Carret
                msg = msg & "Copyright &copy; 1989-" & Trim(CStr(Year(Now()))) & carret2
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Using World Value Survey Data" + Carret
                msg = msg & "Created by Ronald Inglehart" & carret2
                msg = msg & ModelVersion
                lbl_IFSExplain.Text = msg
            ElseIf ModelLoad = "LoadTERRA" Then
                '        Msg$ = "Based on International Futures (IFs)" + Carret
                '        Msg$ = Msg$ + "Created by Barry B. Hughes" + Carret
                '        Msg$ = Msg$ + "Copyright &copy; 1989-2004" + carret2
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = "IFs for TERRA" + Carret
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Europe and the Networked Society" + Carret
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Sponsored by the European Commission" + Carret
                '        Msg$ = Msg$ + "Research Directorates General"
                '        Msg$ = Msg$ + ModelVersion$
                lbl_IFSExplain.Text = msg
                'JRS 05/27/03 RAND PARDEE
            ElseIf ModelLoad = "LoadRANDPardee" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = "IFs for RAND Pardee" + Carret
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Based on International Futures (IFs)" + Carret
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Created by Barry B. Hughes" + Carret
                lbl_IFSExplain.Text = msg
            ElseIf ModelLoad = "LoadSAG" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = "IFs for SAG" + Carret
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Sponsored by the" + Carret + "Strategic Assessments Group" + Carret
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Based on International Futures (IFs)" + Carret
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = msg & "Created by Barry B. Hughes" + Carret
                msg = msg & "Copyright &copy; 1989-" & Trim(CStr(Year(Now()))) & carret2
                lbl_IFSExplain.Text = msg
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                msg = Carret + "Created by Barry B. Hughes" + Carret
                msg = msg & "Copyright &copy; 1989-" & Trim(CStr(Year(Now()))) & carret2
                msg = msg & ModelVersion
                lbl_IFSExplain.Text = msg
            End If

            'JRSVBNET lbl_IFsName.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Web.UI.WebControls.Unit.Parse(WorldMap.Style.Item("TOP"))) + VB6.PixelsToTwipsY(WorldMap.Height))
            'JRSVBNET lbl_IFsName.Style.Item("LEFT") = VB6.TwipsToPixelsX(Inch * 3 + HalfInch)
            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                'JRSVBNET lbl_IFsName.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(WorldMap.Style.Item("LEFT"))) + VB6.PixelsToTwipsX(lbl_IFSExplain.Width) + QuarterInch)
            Else
                'JRSVBNET lbl_IFsName.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(WorldMap.Style.Item("LEFT"))) + VB6.PixelsToTwipsX(lbl_IFSExplain.Width))
            End If

            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                lbl_IFsName.Text = "Consumer Values Trends Analysis"
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                lbl_IFsName.Text = lbl_IFsName.Text + Carret + "      CoVaTrA (Sponsored by GM)" '" Professional Edition"
            ElseIf ModelLoad = "LoadTERRA" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                lbl_IFsName.Text = "&nbsp;&nbsp;&nbsp;Welcome to IFs for TERRA" + Carret + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ModelVersion
                'JRS 05/27/03
            ElseIf ModelLoad = "LoadRANDPardee" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                lbl_IFsName.Text = "&nbsp;&nbsp;&nbsp;Welcome to IFs for RAND Pardee" + Carret + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ModelVersion
            ElseIf ModelLoad = "LoadSAG" = True Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                lbl_IFsName.Text = "&nbsp;&nbsp;&nbsp;Welcome to IFs for SAG" + Carret + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ModelVersion
            ElseIf ModelLoad = "LoadFull" = True Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                '08/12/04 lbl_IFsName.Text = "&nbsp;&nbsp;&nbsp;Welcome to International Futures (IFs)" + Carret + "&nbsp;&nbsp;&nbsp;Full Country Set for NIC"
                'jrs 2005/08/13 lbl_IFsName.Text = "&nbsp;&nbsp;&nbsp;This Site Sponsored by:" + Carret + "&nbsp;&nbsp;&nbsp;U.S. National Intelligence Council" + Carret + Carret + "&nbsp;&nbsp;&nbsp;Welcome to International Futures (IFs)"
                lbl_IFsName.Text = "&nbsp;&nbsp;&nbsp;Welcome to International Futures (IFs)"
            Else
                lbl_IFsName.Text = "&nbsp;&nbsp;&nbsp;Welcome to International Futures (IFs)"
                If ModelLoad = "LoadProfessional" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    lbl_IFsName.Text = lbl_IFsName.Text + Carret + "&nbsp;&nbsp;&nbsp;Professional Edition"
                ElseIf ModelLoad = "LoadStudent" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    lbl_IFsName.Text = lbl_IFsName.Text + Carret + "&nbsp;&nbsp;&nbsp;Student Edition"
                End If

            End If

            'JRSVBNET Support.ControlManager.SetControlFont(lbl_IFsName.Font, VB6.FontChangeSize(lbl_IFsName.Font, 10))
            'JRSVBNET             Support.ControlManager.SetControlFont(lbl_IFsName.Font, VB6.FontChangeBold(lbl_IFsName.Font, True))
            '
            '   Display opening screen with world map only
            '   ------------------------------------------

            'CONVERSION_ISSUE: Class System.Windows.Forms.FormWindowState from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.WindowState.html'.
            'JRSVBNET WindowState = 'JRSVBNET System.Windows.Forms.FormWindowState.Maximized
            'JRS 05/22/03 RAND PARDEE Show 'Display Window before init for user
            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            lbl_IFSExplain.Visible = False
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            lbl_IFsName.Visible = False
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            cmd_Exit.Visible = False
            '    msg$ = "Ready to read init"
            '    MsgBox msg$
            '
            '   Read all initialization files
            '   -----------------------------

            '    Call init


            '   Display labels and continue button
            '   ----------------------------------
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            lbl_IFSExplain.Visible = True
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            lbl_IFsName.Visible = True
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            cmd_Exit.Text = "Load and Continue"
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
            cmd_Exit.Visible = True
            cmd_Exit.Enabled = True

            '    cmd_Exit.Enabled = False
            '    SetFocus
            '    WindowState = 2

            'JRS 05/22/03 RAND PARDEE cmd_Exit.SetFocus

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

            ' raman feb'02 initializing Guided_use (defined in variables.bas
            Guided_use = False

            'Exit Sub

            'StartDirecProblem:

            '            StartDirec = InputBox(ErrMsg, "Specify location for IFs", "")
            '            If VB.Right(StartDirec, 1) <> "\" Then StartDirec = StartDirec & "\"
            '            Resume
            '            Exit Sub

            'IniError:

            '            msg = "Error occurred processing IFs.ini file.  It is probable that "
            '            msg = msg & "the local path specified there, " & LocalDirect
            '            msg = msg & " is invalid. Correct it or disable by specifying c:\DUMMY\"
            '            Session("Error") = msg
            '            ' Clear error.
            '            Server.ClearError()
            '            ' Transfer back to this page.
            '            Server.Transfer("ErrorHandler.aspx")
        End Sub
        '==========================frm_IFs=============================
        'JRS 02/06/04 it was init()
        Public Overrides Sub IFSinit()
            Static regcount As Short
            Static gloop As Object
            Static actreg As Short
            Static VarName As String
            Static TLoop As Object
            Static tfrecount As Object
            Static eleloop As Object
            Static valtbl6 As DataTable
            Static msg As String
            Static REDCD As Object
            Static VideoOkay As String
            Static OutFile As String
            Static InFile As String
            Static ErrMsg As String
            Static Basefile As String
            Static iloop As Object
            Static NL As String
            '--------------------------------------------------------------

            Static REGS As REGTYPE
            Static INREGS As REGTYPE
            Static OUTREGS As REGTYPE
            Static lpage(50) As Object
            Static ACTF(10) As Object

            NL = "<br>" & Chr(10)
            ACTF(0) = "*"

            For iloop = 1 To 10
                ACTF(iloop) = ""
            Next iloop
            On Error GoTo InitFileReadError

            '   Identify Start Files
            '   --------------------

            StartFile = SeekValue(valtblInit, "StartFile$", "Value")
            'JRS 8/24/01 GUI StartFile$ = "IFS"
            Basefile = SeekValue(valtblInit, "Basefile$", "Value")
            'JRS 8/24/01 GUI Basefile$ = "IFSBASE"
            Session("Reload") = SeekValue(valtblInit, "Reload$", "Value")
            'JRS 8/24/01 GUI Reload$ = "True"
            Session("COUNTE") = 0
            'jrs 2011/03/23
            Session("DisplayLastYear") = Nothing
            Session("oldLastYear") = Nothing
            

            '   Set up indices for scattergram control
            '   --------------------------------------

            ReDim Session("bI")(8)
            'JRS 8/17/01 GUI aI = 0
            'JRS 8/17/01 GUI aD = 0
            'JRS 8/17/01 GUI For ILOOP = 1 To 8
            'JRS 8/17/01 GUI bI(ILOOP) = 0
            'JRS 8/17/01 GUIbD(ILOOP) = 0
            'JRS 8/17/01 GUI Next ILOOP

            '   Set global parameters
            '   ---------------------

            '   Directions for changing MaxReg% (read below from IFS.GRP), which sets the maximum
            '       number of regions
            '       1.  Edit IFS.GRP and change regional line; total number is regions + 1 (for ALL);
            '              "Empty1" alerts IFs to stop looking for additional named regions
            '       2.  Rebuild Common so that new size of MaxReg% is reflected in Fill and Retrieve
            '       3.  Use information from Rebuild Common on new value of FILEN to change Declare2
            '           May need to Rebuild Common again
            '       4.  Exit from VB and re-enter (to pick up Fill, Retriev, Declare)
            '       5.  Rebuild base so that data get filled into .RUN properly
            '       6.  Run IFs through longest year and save results into base case.

            NCohorts = SeekValue(valtblInit, "NCohorts", "Value")
            NWVSWaves = SeekValue(valtblInit, "NWVSWaves", "Value")
            'JRS 8/19/01 GUI NCohorts = 21
            WVSCohorts = SeekValue(valtblInit, "WVSCohorts", "Value")
            'JRS 8/19/01 GUI WVSCohorts = 7
            NumbCulReg = SeekValue(valtblInit, "NumbCulReg", "Value")
            'JRS 8/19/01 GUI NumbCulReg = 11
            NAges = SeekValue(valtblInit, "NAges", "Value")
            'JRS 8/19/01 GUI NAges = 100

            'UPGRADE_WARNING: Couldn't resolve default property of object NIOMatrices. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NIOMatrices = SeekValue(valtblInit, "NIOMatrices", "Value")

            NER = SeekValue(valtblInit, "NER%", "Value")
            'JRS 8/19/01 GUI NER% = 4 '2  Number of conventional energy types with resource bases
            NEU = SeekValue(valtblInit, "NEU%", "Value")
            'JRS 8/19/01 GUI NEU% = 5 '3  Number of energy types with resources, including unconventional
            NEP = SeekValue(valtblInit, "NEP%", "Value")
            'JRS 8/19/01 GUI NEP% = 6 '4  Number of conventional energy production types, total
            NET = SeekValue(valtblInit, "NET%", "Value")
            'JRS 8/19/01 GUI NET% = 7 '5  Number of conventional and unconventional energy types, total

            NGovExp = SeekValue(valtblInit, "NGovExp%", "Value")

            NSec = SeekValue(valtblInit, "NSec%", "Value")

            NWBTar = SeekValue(valtblInit, "NWBTar%", "Value")

            NHHTypes = SeekValue(valtblInit, "NHHTypes%", "Value")

            '    msg$ = "Ready to start checking files in init"
            '    MsgBox msg$

            '   Set Sensitivity and Scenario Flags
            '   ----------------------------------

            Session("SensitivityFlag") = SeekValue(valtblInit, "SensitivityFlag$", "Value")
            'JRS 8/19/01 GUI SensitivityFlag$ = False
            Session("SetLoopChange") = CStr(False)
            Session("Scenarioflag") = False

            '   check if filename.RUN exists
            '   ----------------------------

            'JRS MULTISESSION 06/02/04
            'IF .RUN FILE IS NOT THERE TRY TO COPY FROM RUNFILES
            Dim sRunDir As String
            If InStr(Session("FdrvRun"), "Session") > 0 Then
                sRunDir = Mid(Session("FdrvRun"), 1, InStr(Session("FdrvRun"), "Session") - 1)
            Else
                sRunDir = Session("FdrvRun")
            End If

            ErrMsg = "The file IFSBASE.RUN must exist on the RUNFILES subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(sRunDir & Basefile & ".run", ErrMsg, Session("Error"), Me)
            ErrMsg = ""

            FileCopyCheck(sRunDir & Basefile & ".run", Session("FdrvRun") & Basefile & ".run")

            '   check if filenameHIST.RUN exists
            '   ----------------------------

            ErrMsg = "The file IFSBASEHIST.RUN must exist on the RUNFILES subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            If ModelLoad <> "LoadStudent" And CBool(DevelopmentMode) = False Then 'Not used for students
                Call FileCheck(sRunDir & Basefile & "HIST.run", ErrMsg, Session("Error"), Me)
            End If
            ErrMsg = ""

            FileCopyCheck(sRunDir & Basefile & "HIST.run", Session("FdrvRun") & Basefile & "HIST.run")

            '   Check if TablFunc.MDB exists
            '   ----------------------------

            ErrMsg = "The file TablFunc.mdb must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory"
            ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(DataFileDir & "TablFunc.mdb", ErrMsg, Session("Error"), Me)
            ErrMsg = ""
            '    msg$ = "Have checked TablFunc existence"
            '    MsgBox msg$

            '   Check if AnalFunc.MDB exists
            '   ----------------------------

            ErrMsg = "The file AnalFunc.mdb must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory"
            ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(DataFileDir & "AnalFunc.mdb", ErrMsg, Session("Error"), Me)
            ErrMsg = ""

            '   Check if IFS.GRP exists
            '   ----------------------------

            ErrMsg = "The file IFS.GRP must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory"
            ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(DataFileDir & "IFS.GRP", ErrMsg, Session("Error"), Me)
            ErrMsg = ""
            '   msg$ = "Have checked IFs.grp existence"
            '    MsgBox msg$

            '   Check if IFsVar.mdb exists
            '   ----------------------------

            ErrMsg = "The file IFsVar.mdb must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory"
            ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(DataFileDir & "IFsVar.mdb", ErrMsg, Session("Error"), Me)
            ErrMsg = ""
            '    msg$ = "Have checked IFsVar.mdb existence"
            '    MsgBox msg$


            '   Check if VCAT.DAT exists
            '   ----------------------------

            ErrMsg = "The file VCAT.DAT must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory"
            ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(DataFileDir & VCATFile, ErrMsg, Session("Error"), Me)
            ErrMsg = ""
            '    msg$ = "Have checked vcat.dat existence"
            '    MsgBox msg$

            'Scenario Changes August 28, 2001-------
            '   Check if VCAT.DAT exists
            '   ----------------------------

            ErrMsg = "The file VCATFULL.DAT must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory"
            ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(DataFileDir & VCATScenarioFile, ErrMsg, Session("Error"), Me)
            ErrMsg = ""

            '   Check if IFs_MDB exists
            '   ----------------------------

            ErrMsg = "The file IFS.MDB must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(IFs_MDB, ErrMsg, Session("Error"), Me)
            ErrMsg = ""
            '    msg$ = "Have checked Ifs.mdb existence"
            '    MsgBox msg$

            '   Check if IFsHist_MDB exists
            '   ----------------------------

            ErrMsg = "The file IFSHist.MDB must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(IFsHist_MDB, ErrMsg, Session("Error"), Me)
            ErrMsg = ""


            '   Check if IFsCoVaTrA_MDB exists
            '   ----------------------------

            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                ErrMsg = "The file IFSCoVaTrA.MDB must exist on the DATA subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(IFsCoVaTrA_MDB, ErrMsg, Session("Error"), Me)
                ErrMsg = ""
            End If
            '    msg$ = "Have checked IFsCoVaTra.mdb existence"
            '    MsgBox msg$


            'jrs 2011/01/25
            ''   Check if IFSHELP.HLP exists
            ''   ----------------------------

            'ErrMsg = "The file IFSHELP.HLP must exist on the HELP subdirectory"
            'ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            'ErrMsg = ErrMsg & "You may need to re-install IFs."
            'Call FileCheck(FDRV & "HELP\IFSHELP.HLP", ErrMsg, Session("Error"), Me)
            'ErrMsg = ""
            '    msg$ = "Have checked IfsHelp existence"
            '    MsgBox msg$

            'JRS 10/08/02 GUIDED USE
            '   Check if GuidedUse.mdb exists
            '   ----------------------------

            ErrMsg = "The file GuidedUse.mdb must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(FDRV & "DATA\GuidedUse.mdb", ErrMsg, Session("Error"), Me)
            ErrMsg = ""
            '    msg$ = "Have checked GuidedUse existence"
            '    MsgBox msg$

            '   check if filename.RUN exists
            '   ----------------------------

            ErrMsg = "The file SAMBase.mdb must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(PutDataFileDir & "SAMBase.mdb", ErrMsg, Session("Error"), Me)
            ErrMsg = ""

            InFile = PutDataFileDir & "SAMBase.mdb"
            OutFile = sRunDir & "SAMBase.mdb"
            FileCopyCheck(InFile, OutFile)

            FileCopyCheck(sRunDir & "SAMBase.mdb", Session("FdrvRun") & "SAMBase.mdb")

            'jrs 10/31/04
            ' check if run files for the web exist
            ' ------------------------------------
            Dim sIP As String
            Dim Files As DataTable
            Dim DInf As New DirectoryInfo(Me.MapPathSecure(TemplateSourceDirectory))
            sIP = DInf.Parent.FullName & "\IFsInit.mdb"
            'jrs 2005/06/30
            If IsNothing(Session("VersionName")) Then
                SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r, IFsInit as i WHERE r.VersionName = i.Value AND i.Variable = 'VersionName' ORDER BY FileName", "", Files)
            Else
                SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r WHERE r.VersionName = '" & Session("VersionName") & "' ORDER BY FileName", "", Files)
            End If

            'jrs 2005/06/24
            Session("IFsRunFiles") = New MyFiles
            LoadRunFiles(Session("IFsRunFiles"), Session("PutFdrvRun2"), Files, Session("mnuSQLite"))

            'jrs 2005/02/09
            ReDim Session("ShortSce")(Files.Rows.Count + 1)
            Session("ShortSce")(0) = "Working"
            Session("ShortSce")(1) = "Base"
            'jrs 2005/02/25
            ReDim ShortSce(Files.Rows.Count - 1)
            ReDim LongSce(Files.Rows.Count - 1)

            Dim j As Integer
            Dim s As String
            For j = 0 To Files.Rows.Count - 1
                s = Files.Rows(j).Item("FileName")
                ErrMsg = "The file " & UCase(s) & ".RUN must exist on the RUNFILES subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(sRunDir & s & ".run", ErrMsg, Session("Error"), Me)
                ErrMsg = ""

                FileCopyCheck(sRunDir & s & ".run", Session("FdrvRun") & s & ".run")

                ErrMsg = "The file SAM" & UCase(s) & ".MDB must exist on the RUNFILES subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(sRunDir & "SAM" & s & ".mdb", ErrMsg, Session("Error"), Me)
                ErrMsg = ""

                FileCopyCheck(sRunDir & "SAM" & s & ".mdb", Session("FdrvRun") & "SAM" & s & ".mdb")
                'jrs 2005/02/09
                Session("ShortSce")(j + 2) = Files.Rows(j).Item("ShortName")
                'JRS 2005/02/25
                ShortSce(j) = Files.Rows(j).Item("ShortName")
                LongSce(j) = s
                'jrs 2005/06/24
                Session("IFsRunFiles").Item(s & ".run").Visible = True
            Next

            'jrs 2007/01/15
            '   Check if Provinces.mdb exists
            '   ----------------------------

            ErrMsg$ = "The file Provinces.mdb must exist on the DATA subdirectory"
            ErrMsg$ = ErrMsg$ & " of the directory from which you attempt to run IFS.  "
            ErrMsg$ = ErrMsg$ & "You may need to re-install IFs."
            Call FileCheck(FDRV$ + "DATA\Provinces.mdb", ErrMsg, Session("Error"), Me)
            ErrMsg$ = ""

            '   Copy TablFunc.MDB, IFs.MDB, IFsVar.MDB, DataDict.MDB, IFsPop.MDB
            '   to Working Location in local Runfiles directory (for network)
            '   -------------------------------------------------------------------------------

            On Error GoTo FileCopyError
            InFile = DataFileDir & "TablFunc.mdb"
            OutFile = Session("PutFdrvRun") & "TablFunc.mdb"
            FileCopyCheck(InFile, OutFile)

            InFile = DataFileDir & "AnalFunc.mdb"
            OutFile = Session("PutFdrvRun") & "AnalFunc.mdb"
            FileCopyCheck(InFile, OutFile)

            InFile = DataFileDir & "IFsVar.mdb"
            OutFile = Session("PutFdrvRun") & "IFsVar.mdb"
            FileCopyCheck(InFile, OutFile)

            InFile = DataFileDir & "IFsPop.mdb"
            OutFile = Session("PutFdrvRun") & "IFsPop.mdb"
            'FileCopyCheck(InFile, OutFile)
            'JAM 2020-07-21
            If Dir(InFile) > "" Then
                FileCopyCheck(InFile, OutFile)
            End If


            'JAM 2020-07-20
            InFile = DataFileDir & "IFsPop.db"
            OutFile = Session("PutFdrvRun") & "IFsPop.db"
            'JAM 2020-07-21
            If Dir(InFile) > "" Then
                FileCopyCheck(InFile, OutFile)
            End If

            InFile = DataFileDir & "DataDict.mdb"
            OutFile = Session("PutFdrvRun") & "DataDict.mdb"
            FileCopyCheck(InFile, OutFile)

            InFile = IFs_MDB
            OutFile = Session("IFs_MDB2")
            FileCopyCheck(InFile, OutFile)

            'JRS 10/08/02 GUIDED USE
            InFile = DataFileDir & "GuidedUse.mdb"
            OutFile = Session("PutFdrvRun") & "GuidedUse.mdb"
            FileCopyCheck(InFile, OutFile)

            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                InFile = DataFileDir & "IFsCoVaTrA.MDB"
                OutFile = Session("PutFdrvRun") & "IFsCoVaTrA.MDB"
                FileCopyCheck(InFile, OutFile)
            End If

            'jrs 2007/01/15
            InFile$ = DataFileDir$ + "Provinces.mdb"
            OutFile$ = Session("PutFdrvRun") + "Provinces.mdb"
            FileCopyCheck(InFile$, OutFile$)

            'jrs 2008/11/20
            InFile$ = DataFileDir$ + "ifsSmoking.mdb"
            OutFile$ = Session("PutFdrvRun") + "ifsSmoking.mdb"
            FileCopyCheck(InFile$, OutFile$)


START:
            GVAR = "" : GoalsBoolean = "FALSE" : VideoOkay = "TRUE"
            Static OP(9, 2) As Object
            Static GOP(5, 2) As Object
            Static GOALS(11, 5) As Object
            Static CMOP(3) As Object
            'UPGRADE_WARNING: Couldn't resolve default property of object REDCD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            REDCD = 0

            '   Open TablFunc.MDB and Fill Variable Description Rows(i).Item
            '   ----------------------------------------------------

            '    msg$ = "About to open TablFunc"
            '    MsgBox msg$
            On Error GoTo InitError
            msg = "There is an error in init, probably associated with opening TablFunc.mdb"

            InFile = DataFileDir & "TablFunc.mdb"
            '    InFile$ = PutFdrvRun$ + "TablFunc.mdb"
            '    msg$ = "InFile$ is " + InFile$
            '    MsgBox msg$
            'Following approach to database seems overly dependent on Access or DAOJR version
            'May require Access 2000 to install properly on all machines
            '    Dim db6 As Database
            '    msg$ = "Have dimensioned db6"
            '    MsgBox msg$

            '    Dim valtbl6 As Recordset
            '    msg$ = "Have dimensioned valtbl6"
            '    MsgBox msg$

            '    Set db6 = DBEngine.OpenDatabase(InFile$)
            '    msg$ = "Have opened TablFunc"
            '    MsgBox msg$

            '   Compute Number of Variables in TablFunc.mdb
            '   -----------------------------------------

            '    Set valtbl6 = db6.OpenRecordset("Select count(*) As Expr1 from TablFunc")
            '    TFCount = valtbl6.Rows(i).Item("Expr1")
            '    TFCountFull = TFCount
            '    msg$ = "Opened Recordset"
            '    MsgBox msg$
            '    Msg$ = "About to open TablFunc Database"
            '    MsgBox Msg$

            'Important Note to Save:  True is read-only, to override the False (read-write)
            '  default
            '(InFile$, , True) does not work, even though False is supposedly the default option
            '   for database access (meaning shared rather than exclusive)
            'That is, it seems necessary to spell out both options to allow read-only access

            '    Msg$ = "About to open TablFunc Recordset"
            '    MsgBox Msg$
            'UPGRADE_WARNING: Couldn't resolve default property of object db6.OpenRecordset. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

            SetDataTablefromAccess(InFile, "SELECT * FROM TablFunc", "", valtbl6)
            '    Msg$ = "Have opened TablFunc"
            '    MsgBox Msg$
            'UPGRADE_WARNING: Couldn't resolve default property of object eleloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            eleloop = valtbl6.Rows.Count
            'UPGRADE_WARNING: Couldn't resolve default property of object eleloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            TFCount = eleloop 'May later be reduced to eliminate CoVaTrA functions
            'UPGRADE_WARNING: Couldn't resolve default property of object TFCountFull. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("TFCountFull") = TFCount 'Includes CoVaTrA Functions
            '    Msg$ = "Have closed TablFunc"
            '    MsgBox Msg$

            '   Read and Process TablFunc.mdb
            '   ---------------------------

            'JRS 8/17/01 GUI ReDim CoVaTrAFun$(TFCount), Session("Category")$(TFCount), UsedRun$(TFCount), UsedHisRun$(TFCount)
            ReDim FuncName(TFCount)
            ReDim XVariable(TFCount)
            ReDim YVariable(TFCount)
            ReDim Analytic(TFCount)
            ReDim Session("UsedRun")(TFCount)
            ReDim Session("UsedHisRun")(TFCount)
            ReDim Session("CoVaTrAFun")(TFCount)
            ReDim Session("Category")(TFCount)
            ReDim Session("BPoints")(TFCount, 8)
            ReDim Session("XPoints")(TFCount, 10)
            ReDim Session("YPoints")(TFCount, 10)
            ReDim Session("TFuncPoints")(TFCount)
            ReDim Session("APoint")(TFCount)
            ReDim Session("MinPoint")(TFCount)
            ReDim Session("MaxPoint")(TFCount)

            '    Set valtbl6 = db6.OpenRecordset("Select * from TablFunc")
            '    msg$ = "Selected"
            '    MsgBox msg$
            '    valtbl6.MoveFirst
            '    msg$ = "Moved"
            '    MsgBox msg$
            SetDataTablefromAccess(InFile, "SELECT * FROM TablFunc ORDER BY [Function Name]", "", valtbl6)

            Dim i As Integer
            i = 0
            'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            tfrecount = 0
            For TLoop = 1 To TFCount 'Probably should replace this with logic that separates
                'CoVaTrA table functions into separate file
                'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or valtbl6.Rows(i).Item("CoVaTrA") = "No" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    tfrecount = tfrecount + 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    FuncName(tfrecount) = valtbl6.Rows(i).Item("Function Name")
                    Session("UsedRun")(tfrecount) = valtbl6.Rows(i).Item("UsedRun")
                    Session("UsedHisRun")(tfrecount) = valtbl6.Rows(i).Item("UsedHisRun")
                    Session("CoVaTrAFun")(tfrecount) = valtbl6.Rows(i).Item("CoVaTrA")
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    XVariable(tfrecount) = valtbl6.Rows(i).Item("XVariable")
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    YVariable(tfrecount) = valtbl6.Rows(i).Item("YVariable")
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Analytic(tfrecount) = valtbl6.Rows(i).Item("Analytic")
                    If Not (IsDBNull(valtbl6.Rows(i).Item("Category"))) Then
                        Session("Category")(tfrecount) = valtbl6.Rows(i).Item("Category")
                    Else
                        Session("Category")(tfrecount) = ""
                    End If
                    For iloop = 1 To 8
                        'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        VarName = Trim("b" & Trim(Str(iloop)))
                        'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("BPoints")(tfrecount, iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("BPoints")(tfrecount, iloop) = valtbl6.Rows(i).Item(VarName)
                        'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("BPoints")(tfrecount, iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        If IsDBNull(Session("BPoints")(tfrecount, iloop)) Then Session("BPoints")(tfrecount, iloop) = 0
                    Next iloop
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("APoint")(tfrecount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("APoint")(tfrecount) = valtbl6.Rows(i).Item("a")
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("APoint")(tfrecount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If IsDBNull(Session("APoint")(tfrecount)) Then Session("APoint")(tfrecount) = 0
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object session("MinPoint")(tfrecount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("MinPoint")(tfrecount) = valtbl6.Rows(i).Item("Minimum")
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object session("MaxPoint")(tfrecount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("MaxPoint")(tfrecount) = valtbl6.Rows(i).Item("Maximum")
                    'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("TFuncPoints")(tfrecount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("TFuncPoints")(tfrecount) = valtbl6.Rows(i).Item("TFuncPoints")
                    For iloop = 1 To 10
                        'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        VarName = Trim("X" & Trim(Str(iloop)))
                        'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("XPoints")(tfrecount, iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("XPoints")(tfrecount, iloop) = valtbl6.Rows(i).Item(VarName)
                    Next iloop
                    For iloop = 1 To 10
                        'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        VarName = Trim("Y" & Trim(Str(iloop)))
                        'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("YPoints")(tfrecount, iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("YPoints")(tfrecount, iloop) = valtbl6.Rows(i).Item(VarName)
                    Next iloop
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.MoveNext. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                i = i + 1
            Next TLoop
            'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            TFCount = tfrecount 'May be reduced by CoVaTrA functions
            Session("FuncInd") = 0 'Set for entrance into frm_TablFunc
            '    msg$ = "Have closed TablFunc"
            '    MsgBox msg$

            '   On Network, copy IFS.GRP to local runfiles directory
            '   ---------------------------------------------------
            '    On Error GoTo FileCopyError
            '    InFile$ = DataFileDir$ + "IFS.GRP"
            '    OutFile$ = PutDataFileDir$ + "IFS.GRP"
            '    If DataFileDir$ <> PutDataFileDir$ Then FileCopy InFile$, OutFile$

            '   Dimension dynamic display arrays
            '   --------------------------------
            actreg = MaxReg + 1
            'JRS 8/17/01 GUI ReDim RegNamePop$(actreg%), RegNumPop(actreg%)
            ReDim RegName(actreg)
            'JRSVBNET ReDim RegNum(actreg)
            ReDim RegMembs(actreg)
            ReDim RegHold(actreg)
            ReDim RegWeight(actreg)
            'JRSVBNET ReDim salphas(actreg, NSec)

            '   Compute Maximum Valid and Read Dimension Session("Category") Labels, Except Regions
            '   ------------------------------------------------------------------------
            'jrs 2005/02/12
            Dim fNum As Integer
            fNum = FreeFile()
            msg = "There is an error in init, probably associated with opening IFs.GRP"
            FileOpen(fNum, DataFileDir & "IFS.GRP", OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
            Input(fNum, KNT) 'Read number of dimensions; correspond to lines in .grp and to
            'specifications for each variable in IFsVar.mdb
            'Have removed former region line from top of file; so have KNT-1 dimensions in file
            ReDim MaxVal(KNT)
            ReDim MAXCOM(KNT)
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            MaxVal(1) = MaxReg + 1 'Add room for "ALL"
            'UPGRADE_WARNING: Couldn't resolve default property of object MAXCOM(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            MAXCOM(1) = MaxReg + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object AMAX(20, MaxVal(1)). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ReDim GRPS(KNT, AMAX(20, MaxVal(1)))
            'UPGRADE_WARNING: Couldn't resolve default property of object KNT. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For iloop = 2 To KNT 'Loop from 2 because have KNT-1 dimensions in file
                'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Input(fNum, MaxVal(iloop))
                '        If ILOOP = 1 Then
                '            MaxReg% = MaxVal(ILOOP) - 1
                '            ReDim GRPS$(KNT, MaxReg% + 1)
                '        End If
                'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object MAXCOM(iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                MAXCOM(iloop) = MaxVal(iloop)
                'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                For gloop = 1 To MaxVal(iloop)
                    'UPGRADE_WARNING: Couldn't resolve default property of object gloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Input(fNum, GRPS(iloop, gloop))
                    '           Skip empty spots in region/country vector
                    '            If UCase$(GRPS$(ILOOP, gloop)) = "EMPTY1" Then
                    '                Line Input #1, Dummy$
                    '                MaxVal(ILOOP) = gloop
                    '                GRPS$(ILOOP, gloop) = "ALL"
                    '            Exit For
                    '            End If
                Next gloop
            Next iloop
            FileClose(fNum)

            '   Read Region Names and Determine Number of Regions
            '   -------------------------------------------------

            regcount = 0
            msg = "There is an error in init, probably associated with opening IFs.mdb"
            'mtiwebforms April 2004 ADO.Net
            'db = DAODBEngine_definst.OpenDatabase(IFs_MDB2)
            'valtbl = db.OpenRecordset(RegionIDNew)
            Dim db1 As String
            Dim valtbl1 As DataTable
            Dim RCnt As Integer

            db1 = Session("IFs_MDB2")

            SetDataTablefromAccess(db1, "Select * from " + RegionIDNew, "PrimaryKey", valtbl1)
            'valtbl.Index = "PrimaryKey"
            'If Not valtbl.BOF Then
            '    valtbl.MoveFirst()
            '    Do
            '        regcount = regcount + 1
            '        GRPS(1, regcount) = valtbl.Rows(i).Item("regionname")
            '        RegName(regcount) = GRPS(1, regcount)
            '        valtbl.MoveNext()
            '        If valtbl.EOF = True Then Exit Do
            '    Loop
            'End If
            'valtbl.Close()
            'db.Close()
            'can also use datareader
            For RCnt = 0 To valtbl1.Rows.Count - 1
                'valtbl.MoveFirst()
                'Do
                regcount = regcount + 1
                GRPS(1, regcount) = valtbl1.Rows(RCnt).Item("regionname")
                RegName(regcount) = GRPS(1, regcount)
                'jrs 2005/04/07
                IFsRegs.Add(RegName$(regcount), regcount, "", "", valtbl1.Rows(RCnt).Item("regionalias")) 'jrs 2016/05/27
                'valtbl.MoveNext()
                'If valtbl.EOF = True Then Exit Do
                'Loop
            Next
            'valtbl.Close()
            'db.Close()
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            MaxVal(1) = regcount + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            GRPS(1, MaxVal(1)) = "ALL"

            '   Now that know maximum region numbers, dimension dynamic display arrays
            '   ----------------------------------------------------------------------

            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NR = MaxVal(1) - 1 'Recomputed in FRM.RUN Load
            'MaxVariables = AMAX(500, MaxReg * 3) 'Sets number of slots in the grid that saves variable
            MaxVariables = AMAX(750, MaxReg * 5) 'Sets number of slots in the grid that saves variable
            'names for display
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            MaxYears = 300 'jrs 2005/06/29 200
            ReDim Session("FileNumbr")(MaxVariables)
            ReDim Session("VARN")(6, MaxVariables)
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ReDim Session("GR1N")(MaxVariables + 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ReDim Session("GR2N")(MaxVariables + 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ReDim Session("GR3N")(MaxVariables + 1)
            ReDim Session("GR4N")(MaxVariables + 1) 'jrs 2006/06/16
            ReDim Session("PLOT")(MaxYears, MaxVariables)
            ReDim Session("GeoRegion")(MaxVariables), Session("GeoValue")(MaxVariables)
            ReDim Session("FinYear")(MaxVariables)
            'jrs 2006/09/25
            ReDim Session("FinYearHis")(MaxVariables)
            ReDim Session("VL")(MaxVariables)
            'JRS 8/17/01 GUI ReDim MaxValC(MaxVariables), MinValC(MaxVariables), DiffValC(MaxVariables) 'Used in Common Display
            ReDim Session("PLOTComp")(MaxYears)
            ReDim Session("PLOTAll")(MaxYears, MaxVariables)
            ReDim Preserve Session("aFormulas")(1)
            Session("bComputation") = False

            'jrs 10/27/04 Flexible Dimension Names for Graphs
            ReDim Session("FDNG1")(MaxVariables + 1)
            ReDim Session("FDNG2")(MaxVariables + 1)
            ReDim Session("FDNG3")(MaxVariables + 1)
            ReDim Session("FDNG4")(MaxVariables + 1) 'jrs 2006/06/16

            '   Read Maximum in COMMON
            '   ----------------------
            '    Open PutDataFileDir$ + "IFS.GRP" For Input As #1
            '    Input #1, KNT
            '    For ILOOP = 1 To KNT
            '        Input #1, MAXCOM(ILOOP)
            '        Line Input #1, Dummy$
            '    Next ILOOP
            '    Close #1

            '   Read Region Names
            '   ----------------------
            '    Open PutDataFileDir$ + "IFS.GRP" For Input As #1
            '    Input #1, KNT
            '    Input #1, MAXCOM(1)
            '    For ILOOP = 1 To actreg%
            '        Input #1, RegName$(ILOOP)
            '    Next ILOOP
            '    Close #1

            '   Read Country/Region Groupings
            '   -----------------------------

            msg = "There is an error in init, probably associated with opening IFs.mdb, 2nd"
            'JRS 8/16/01 GUI Set db = OpenDatabase(IFs_MDB2)
            '    Dim valtbl As Recordset

            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "", valtbl)
            'jrs 2008/12/08
            ReDim Preserve GroupNames(valtbl.Rows.Count + 1)
            ReDim Preserve GroupCodes(valtbl.Rows.Count + 1)

            eleloop = 0
            Do
                eleloop = eleloop + 1
                GroupNames(eleloop) = valtbl.Rows(eleloop - 1).Item("GroupName")
                GroupCodes(eleloop) = valtbl.Rows(eleloop - 1).Item("GroupNum")
                If eleloop = valtbl.Rows.Count Then Exit Do
            Loop
            GCount = eleloop
            GroupNames(eleloop + 1) = "ALL"

            'jrs 2011/02/10
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM GListId ORDER BY Description", "", valtbl)
            ReDim Preserve GListNames(valtbl.Rows.Count + 1)

            eleloop = 0
            Do
                eleloop = eleloop + 1
                GListNames(eleloop) = valtbl.Rows(eleloop - 1).Item("Description")
                If eleloop = valtbl.Rows.Count Then Exit Do
            Loop
            GListCount = eleloop

            'JRS 9/5/01 MOVING THE CODE TO A MODULE
            IFsVarIni(Session("PutFdrvRun"), Session("VDisplayUnitsName"))


            'NOTE:  Following copying of IFS.MDB and IFsHistSeries to local
            '   runfiles directory was to avoid protections against writing
            '   to those files on network server.  Given movement of Access
            '   away from .ldb files, is that still necessary?  They take
            '   much room on local machines, and that can be a problem
            '   in labs.
            '   I am not copying DataDict.MDB to Runfiles before use in cross-
            '   sectional analysis;  perhaps that could serve as test on a server

            '  Copy data files to local on network
            '       - so that .LDB read/write not a problem
            '   -------------------------------------------
            On Error GoTo FileCopyError

            Session("HistoricBase") = CStr(False) 'Default to forecasts
            Session("ActiveBase") = "IFSBASE.RUN"
            Session("IFs_MDB2Active") = Session("IFs_MDB2")

            InFile = IFsHist_MDB
            OutFile = Session("IFsHist_MDB2")
            FileCopyCheck(InFile, OutFile)

            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                InFile = IFsCoVaTrA_MDB
                OutFile = Session("IFsCoVaTrA_MDB2")
                FileCopyCheck(InFile, OutFile)
            End If

            '   Copy HistSeries File to Working Location in local Runfiles directory
            '   --------------------------------------------------------------------

            InFile = "IFsHistSeries_MDB" 'In case of error, need for error message
            OutFile = "IFsHistSeries_MDB"
            FileCopyCheck(DataFileDir & "IFsHistSeries.mdb", Session("PutFdrvRun") & "IFsHistSeries.mdb")
            Session("IFsHistSeries_MDB") = Session("PutFdrvRun") & "IFsHistSeries.mdb"
            Session("IFsCoVaTrASeries_MDB") = Session("PutFdrvRun") & "IFsCoVaTrASeries.mdb"

            '    Set db = OpenDatabase(IFsHistSeries_MDB, False, True, DataPW$)

            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                InFile = "IFsCoVaTrASeries_MDB"
                OutFile = "IFsCoVaTrASeries_MDB"
                FileCopyCheck(DataFileDir & "IFsCoVaTrASeries.mdb", Session("PutFdrvRun") & "IFsCoVaTrASeries.mdb")
            End If

            '   Copy Cohort File to Working Location in local Runfiles directory
            '   --------------------------------------------------------------------

            InFile = "IFsWVSCohort_MDB"
            OutFile = "IFsWVSCohort_MDB"
            FileCopyCheck(DataFileDir & "IFsWVSCohort.mdb", Session("PutFdrvRun") & "IFsWVSCohort.mdb")

            'mti WebForm Changes to be taken by Jose
            'Session("IFsHistSeries_MDB") = Session("PutFdrvRun") & "IFsWVSCohort.mdb"
            Session("IFsWVSCohort_MDB") = Session("PutFdrvRun") & "IFsWVSCohort.mdb"

            '   Copy Pop Base Case to Work Case to Hold Cohort Data
            '   ---------------------------------------------------

            InFile = "IFsPopWork_MDB"
            OutFile = "IFsPopWork_MDB"
            'jrs 2006/09/15 FileCopyCheck(DataFileDir & "IFsPopWork.mdb", Session("PutFdrvRun") & "IFsPopWork.mdb")

            'JAM 2020-07-21
            If Dir(DataFileDir & "IFsPop.mdb") > "" Then
                FileCopyCheck(DataFileDir & "IFsPop.mdb", Session("PutFdrvRun") & "IFsPopWork.mdb")
            End If
            'FileCopyCheck(DataFileDir & "IFsPop.mdb", Session("PutFdrvRun") & "IFsPopWork.mdb")

            'JAM 2020-07-21
            If Dir(DataFileDir & "IFsPop.db") > "" Then
                'JAM 2020-07-20
                FileCopyCheck(DataFileDir & "IFsPop.db", Session("PutFdrvRun") & "IFsPopWork.db")
            End If

            '   Copy Statistics File to Working Location in local Runfiles directory
            '   --------------------------------------------------------------------

            InFile = "IFsStat_MDB"
            OutFile = "IFsStat_MDB"
            FileCopyCheck(DataFileDir & "IFsStat.mdb", Session("PutFdrvRun") & "IFsStat.mdb")
            Session("IFsStat_MDB") = Session("PutFdrvRun") & "IFsStat.mdb"

            '   On Network, copy IFSBASE.RUN to local directory
            '   -----------------------------------------------
            InFile = Session("FdrvRun") & Basefile & ".RUN"
            OutFile = Session("PutFdrvRun") & Basefile & ".RUN"
            If Session("FdrvRun") <> Session("PutFdrvRun") Then
                FileCopyCheck(InFile, OutFile)
            End If


            '   On Network, copy IFSBASEHIST.RUN to local directory
            '   -----------------------------------------------
            If ModelLoad <> "LoadStudent" And CBool(DevelopmentMode) = False Then 'Historical run not available for students
                InFile = Session("FdrvRun") & Basefile & "HIST.RUN"
                OutFile = Session("PutFdrvRun") & Basefile & "HIST.RUN"
                If Session("FdrvRun") <> Session("PutFdrvRun") Then
                    FileCopyCheck(InFile, OutFile)
                End If
            End If

            '   Copy IFSBASE.RUN to working.run
            '   -----------------------------
            InFile = Session("PutFdrvRun") & Basefile & ".RUN.DB"
            OutFile = Session("PutFdrvRun") & "working.run.db"
            FileCopyCheck(InFile, OutFile)
            Session("WorkingFile") = Session("PutFdrvRun") & "IFSBASE.RUN.DB"

            'JRS 10/16/02 SAM COPY
            '   Copy SAMBASE.MDB to SAMWORKING.MDB
            '   -----------------------------
            InFile = PutDataFileDir & "SAMBase.db"
            OutFile = Session("PutFdrvRun") & "SAMWorking.db"
            FileCopyCheck(InFile, OutFile)

            '   Establish Files Selected
            '   ------------------------
            Session("FilesSelected") = 1
            ReDim Session("RunFiles")(10)
            Session("RunFiles")(1) = Session("PutFdrvRun") & "IFSBASE.RUN"


            '   Table Title
            '   -----------
            'JRS 8/17/01 GUI Session("TableTitle") = "Title"

            '   Redifining Size of Buffer
            '   -------------------------
            'JRS 8/20/01
            Static Y As Short
            'UPGRADE_WARNING: Couldn't resolve default property of object filen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            Y = filen Mod 5000
            ReDim FILEBUF3.elem3(Y)

            '   Defining iGDP and iPOP
            '   ----------------------
            'JRS 2/9/02
            iGDP = IfsVars.Item("GDP").Index
            iPOP = IfsVars.Item("POP").Index

            'mti WebForm Changes From June 21, 2004 -start - variable no for run graph variables
            iWGDP = IfsVars.Item("WGDP").Index
            iWPOP = IfsVars.Item("WPOP").Index
            iWFORST = IfsVars.Item("WFORST").Index
            iCO2PPM = IfsVars.Item("CO2PPM").Index
            'mti WebForm Changes From June 21, 2004 - end

            Session("Fil_Min_Level") = 1
            Session("Fil_Max_Level") = 1
            Session("Fil_Rate") = 10
            Session("Fil_Range_Min") = 1
            Session("Fil_Range_Max") = 10

            POPidx = IfsVars.Item("POP").Index
            GDPidx = IfsVars.Item("GDP").Index

            'jrs 2009/01/23
            For iloop = 2 To UBound(GRPS, 1)
                If UCase(GRPS(iloop, 1)) = "MALE" Then
                    GenderIdx = iloop
                ElseIf UCase(GRPS(iloop, 1)) = "AGE18_24" Then
                    Age18_65Idx = iloop
                ElseIf UCase(GRPS(iloop, 1)) = "AGE30_44" Then
                    Age30_70Idx = iloop
                ElseIf UCase(GRPS(iloop, 1)) = "AGE15_29" Then
                    Age15_80Idx = iloop
                End If
            Next iloop

            'jrs 2005/12/09
            SetDataTablefromAccess(Session("FdrvRun") & "IFS.MDB", "SELECT * FROM [Currency] WHERE [Currency Flag]=True", "", rsCurr)
            'jrs 2006/06/20
            Session("bFillHis") = False
            Session("gHistoricAnalog") = False
            'jrs 2006/07/21
            Session("FarmFlag") = False
            Session("FarmFlagHist") = False
            'jrs 2006/09/25
            Session("buseAllHis") = False
            'jrs 2007/11/30
            Session("DisplayTime") = 1

            Exit Sub

InitError:
            'jrs 2005/02/12
            msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
            Session("Error") = msg
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")

InitFileReadError:

            msg = "There is an error in init, probably a missing file."
            'jrs 2005/02/12
            msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
            Session("Error") = msg
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")

FileCopyError:
            msg = "An error has arisen, probably during an attempt to copy "
            msg = msg & InFile & " to " & OutFile & NL
            msg = msg & "Check existence of source file and space availability."
            'jrs 2005/02/12
            msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
            Session("Error") = msg
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")

        End Sub

        '==========================frm_IFs=============================
        Private Sub Read_INI_File()
            'jrs 2005/02/12
            Dim fNum As Integer

            Dim msg As String
            Dim directory As String
            Dim ICount As Short
            Dim Dummy As Object
            Dim Returned As String
            Dim Size_Renamed As Object
            Dim Def As String
            Dim Key As String
            Dim Section As String
            Dim ErrMsg As String
            Dim HelpFileSubDir As String
            Dim DataHistFile As String
            Dim DataFile As String
            Dim WorldMapFile As String
            Dim NL As String
            '--------------------------------------------------------------

            NL = "<br>" & Chr(10)

            '   Set basic Default File Names/Locations/Time Horizons
            '   ----------------------------------------------------

            WorldMapFile = SeekValue(valtblInit, "WorldMapFile$", "Value")
            'JRS 8/18/01 GUI WorldMapFile$ = "World_ma.bmp"
            WorldMapLocation = StartDirec & WorldMapFile

            DataFileSubDir = SeekValue(valtblInit, "DataFileSubDir$", "Value")
            'JRS 8/18/01 GUI DataFileSubDir$ = "DATA\"
            DataFile = SeekValue(valtblInit, "DataFile$", "Value")
            'JRS 8/18/01 GUI DataFile$ = "IFS.MDB"
            DataHistFile = SeekValue(valtblInit, "DataHistFile$", "Value")
            'JRS 8/18/01 GUI DataHistFile$ = "IFS.MDB" '"IFsHistBase.MDB"
            Session("ResFileSubDir") = SeekValue(valtblInit, "ResFileSubDir$", "Value")
            'JRS 8/18/01 GUI ResFileSubDir$ = "RUNFILES\"
            HelpFileSubDir = SeekValue(valtblInit, "HelpFileSubDir$", "Value")
            'JRS 8/18/01 GUI HelpFileSubDir$ = "HELP\"
            Session("ScenarioFileSubDir") = SeekValue(valtblInit, "ScenarioFileSubDir$", "Value")
            'JRS 8/18/01 GUI ScenarioFileSubDir$ = "SCENARIO\"
            Session("SensitivityFileSubDir") = SeekValue(valtblInit, "SensitivityFileSubDir$", "Value")
            'JRS 8/18/01 GUI SensitivityFileSubDir$ = "SENSITIVITY\"

            'JRS MULTISESSION 06/02/04 ***
            If Session("SessionIdx") <> -1 Then
                Session("ResFileSubDir") = Session("ResFileSubDir") & "Session"
                Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & "Session"
                Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & "Session"
                If Session("SessionIdx") < 10 Then
                    Session("ResFileSubDir") = Session("ResFileSubDir") & "0"
                    Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & "0"
                    Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & "0"
                End If
                Session("ResFileSubDir") = Session("ResFileSubDir") & Trim(Session("SessionIdx")) & "\"
                Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & Trim(Session("SessionIdx")) & "\"
                Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & Trim(Session("SessionIdx")) & "\"
            End If
            '*****************************

            '   Check for existence of .INI file
            '   --------------------------------

            ErrMsg = "The WINDOWS\IFs.INI file is missing; reinstall IFs or fix path to .INI"
            Call FileCheck(IFSINIFile, ErrMsg, Session("Error"), Me)
            ErrMsg = ""

            '   Read local directory from .INI file
            '      The .INI file is necessary for a network installation, because in that case
            '           it carries the path for local files;  see Read.Me for explanation.
            '   ------------------------------------------------------------------------------

            Section = SeekValue(valtblInit, "Section$", "Value")
            'JRS 8/24/01 GUI Section$ = "FILES"
            Key = "LocalDirectory" : Def = "C:\IFS\"
            'UPGRADE_WARNING: Couldn't resolve default property of object Size. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Size_Renamed = 40
            'jrs 2011/03/25 
            'Returned = Space(Size_Renamed)
            'Dummy = GetPrivateProfileString(Section, Key, Def, Returned, Size_Renamed, IFSINIFile)
            Returned = "c:\DUMMY\"
            'For ICount = 40 To 1 Step -1  'remove trailing blanks, but leave internal ones
            '    If InStr(ICount, Returned, " ") = ICount Then
            '        Returned = VB.Left(Returned, ICount - 1) 'removes trailing blanks
            '    ElseIf InStr(ICount, Returned, Chr(0)) = ICount Then
            '        Returned = VB.Left(Returned, ICount - 1) 'removes first blank
            '    Else
            '        Exit For
            '    End If
            'Next ICount
            '    Returned$ = Left$(Returned$, InStr(Returned$, Chr$(0)) - 1)
            LocalDirect = Returned
            '    msg$ = "Have read .INI for LocalDirect, which is " + LocalDirect$
            '   MsgBox msg$

            '   Set Start-up (Possibly Network) Directory
            '   -----------------------------------------

            FDRV = StartDirec 'Was read in Form.Load
            If VB.Right(FDRV, 1) <> "\" Then FDRV = FDRV & "\"
            '    msg$ = "Have read StartDirect$ for FDRV$, which is " + FDRV$
            '    MsgBox msg$

            '   Set and Check Write on Local Directory - May be the Same as "Network"
            '   ---------------------------------------------------------------------

            If UCase(LocalDirect) = UCase("c:\DUMMY\") Then
                PutFdrv = FDRV 'LocalDirectory = NetworkDirectory; No Network
            Else
                PutFdrv = LocalDirect 'LocalDirectory taken from .INI
            End If
            If VB.Right(PutFdrv, 1) <> "\" Then PutFdrv = PutFdrv & "\"

            '   Check for all Subdirectories on Target Directory
            '   ------------------------------------------------

            On Error GoTo PutDirNotExist 'Make Sure Local Directory Exists
            ChDir(PutFdrv)
            On Error GoTo ResDirNotExist 'Make Sure Local Result Subdirectory Exists
            ChDir(PutFdrv & Session("ResFileSubDir"))
            On Error GoTo DataDirNotExist 'Make Sure Local Data Subdirectory Exists
            ChDir(PutFdrv & DataFileSubDir)
            On Error GoTo ScenarioDirNotExist 'Make Sure Local Scenario Subdirectory Exists
            ChDir(PutFdrv & Session("ScenarioFileSubDir"))
            On Error GoTo SensitivityDirNotExist 'Make Sure Local Sensitivity Subdirectory Exists
            ChDir(PutFdrv & Session("SensitivityFileSubDir"))

            '    msg$ = "Have fed PutFdrv$, which is " + PutFdrv$
            '    MsgBox msg$

            '   Establish run files directory paths
            '   -----------------------------------

            Session("FdrvRun") = FDRV & Session("ResFileSubDir")
            Session("PutFdrvRun") = PutFdrv & Session("ResFileSubDir")
            Session("PutFdrvRun2") = Session("PutFdrvRun") 'Used for file management with dialog box
            If VB.Right(Session("PutFdrvRun2"), 1) = "\" Then
                Session("PutFdrvRun2") = VB.Left(Session("PutFdrvRun2"), Len(Session("PutFdrvRun2")) - 1)
            End If
            '    msg$ = "Have determinded PutFdrvRun$, which is " + PutFdrvRun$
            '    MsgBox msg$

            '   Copy Date.Dat to Result Directory and Write a Line to it
            '       This will test whether network installation will allow local write
            '       Because this capability is essential, model will stop if not
            '       and alert user to put writable path in .INI file
            '   ----------------------------------------------------------------------

            On Error GoTo StartWriteProblem 'Add Extra Line to Date.Dat to Check Write
            DataFileDir = FDRV & DataFileSubDir
            PutDataFileDir = PutFdrv & DataFileSubDir
            If DataFileDir <> PutDataFileDir Then
                FileCopyCheck(DataFileDir & "Date.Dat", PutDataFileDir & "Date.Dat")
            End If
            'JRS 02/08/04 FileOpen(1, PutDataFileDir & "Date.Dat", OpenMode.Append)
            'JRS 02/08/04 WriteLine(1, "Test")
            'JRS 02/08/04 FileClose(1)
            '    msg$ = "Have tested StartWrite Problem, which is " + PutFdrvRun$
            '    MsgBox msg$

            '   Establish help file
            '   -------------------

            'UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'JRSVBNET App.HelpFile = FDRV & HelpFileSubDir & "IFSHELP.HLP"

            '   Data file locations
            '   --------------------

            IFs_MDB = StartDirec & DataFileSubDir & DataFile
            IFsHist_MDB = StartDirec & DataFileSubDir & DataHistFile
            IFsCoVaTrA_MDB = StartDirec & DataFileSubDir + SeekValue(valtblInit, "IFsCoVaTrA_MDB", "Value")
            'JRS 8/24/01 IFsCoVaTrA_MDB = StartDirec$ + DataFileSubDir$ + "IFsCoVaTrA.mdb"
            '    IFsGroups_MDB = StartDirec$ + DataFileSubDir$ + "IFsGroups.mdb"
            Session("IFs_MDB2") = Session("PutFdrvRun") & DataFile
            Session("IFsHist_MDB2") = Session("PutFdrvRun") & DataHistFile
            Session("IFsCoVaTrA_MDB2") = Session("PutFdrvRun") + SeekValue(valtblInit, "IFsCoVaTrA_MDB2", "Value")
            'JRS 8/24/01 IFsCoVaTrA_MDB2 = PutFdrvRun$ + "IFsCoVaTrA.mdb"
            VCATFile = SeekValue(valtblInit, "VCATFile$", "Value")
            'JRS 8/24/01 VCATFile$ = "VCat.Dat"
            If ModelLoad = "LoadTERRA" Then
                VCATFile = SeekValue(valtblInit, "VCATFileTerra$", "Value")
                'JRS 8/24/01 VCATFile$ = "VCATTERRA.DAT"
            End If
            VCATScenarioFile = SeekValue(valtblInit, "VCATScenarioFile$", "Value")
            'JRS 8/29/01 VCATScenarioFile$ = "VCatfull.Dat"

            '   Change to Start Directory
            '   -------------------------

            DefaultDrive = VB.Left(FDRV, 2)
            ChDrive(DefaultDrive)
            directory = FDRV
            If VB.Right(directory, 1) = "\" Then
                directory = VB.Left(directory, Len(directory) - 1)
            End If
            ChDir(directory)
            '    msg$ = "Have changed to Start directory, which is " + directory$
            '    MsgBox msg$


            '   Delete Working Scenario at the load of Model
            '   --------------------------------------------

            'establish scenario directory
            Session("ScenarioFileDir") = PutFdrv & Session("ScenarioFileSubDir")
            Session("SensitivityFileDir") = PutFdrv & Session("SensitivityFileSubDir")
            'Delete Working Scenario at the load of Model
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
                Kill(Session("ScenarioFileDir") & "Working.Sce")
            End If
            'JRS 07/15/03 BEGIN
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(Session("ScenarioFileDir") & "WorkingGroup.Sce") <> "" Then
                Kill(Session("ScenarioFileDir") & "WorkingGroup.Sce")
            End If
            'JRS 07/15/03 END
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(Session("ScenarioFileDir") & "Working.Txt") <> "" Then
                Kill(Session("ScenarioFileDir") & "Working.Txt")
            End If
            'write a blank file
            'jrs 2005/02/12
            If Session("SessionIdx") > -1 Then
                'jrs 2005/02/12
                fNum = FreeFile()
                FileOpen(fNum, Session("ScenarioFileDir") & "Working.Txt", OpenMode.Output)
                FileClose(fNum)
            End If


            '    msg$ = "Have Killed Working Scenario, if exists "
            '    MsgBox msg$


            'Vensim Integration  - Start

            'Vensim Integration  - Replace with MSAccess Field Later
            VensimFileSubDir = "Vensim\"

            'Vensim Integration : delete working.rgg
            VenSimFileDir = PutFdrv & VensimFileSubDir
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(VenSimFileDir & "Working.Rgg") <> "" Then
                Kill(VenSimFileDir & "Working.Rgg")
            End If
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(VenSimFileDir & "Data.Dat") <> "" Then
                Kill(VenSimFileDir & "Data.Dat")
            End If

            'Vensim Integration  - End

            'Vensim Integration- Late November 2003
            VensimOpened = 0
            'End Vensim Integration- Late November 2003


            '   Check if DATE.DAT exists and create if it does not
            '   --------------------------------------------------

            If DataFileDir = PutDataFileDir Then Exit Sub 'No Network; Date.Dat not needed
            '    msg$ = "Still in Read_InI_File to check Date.dat "
            '    MsgBox msg$
            On Error GoTo nonexist1
            'jrs 2005/02/12
            fNum = FreeFile()
            FileOpen(fNum, PutDataFileDir & "DATE.DAT", OpenMode.Input)
            Input(fNum, Session("FirstYear"))
            Input(fNum, Session("LastYear")) 'Use DATE.DAT if exists
            Session("FirstYear") = FirstYearForecast 'In case values are history, replace
            Session("LastYear") = Session("LastYearForecast")
            FileClose(fNum) 'DATE.DAT needed only if Rebuild Base and change end year
            '    msg$ = "Leaving Read_Ini"
            '    MsgBox msg$
            On Error GoTo 0
            Exit Sub

nonexist1:
            'jrs 2005/02/12
            fNum = FreeFile()
            FileOpen(fNum, PutDataFileDir & "DATE.DAT", OpenMode.Append)
            WriteLine(fNum, FirstYearForecast, Session("LastYearForecast")) 'Fill DATE.DAT if does not exist
            FileClose(fNum)
            Exit Sub

PutDirNotExist:
            MkDir(PutFdrv)
            Resume Next
            Exit Sub

ResDirNotExist:
            MkDir(PutFdrv & Session("ResFileSubDir"))
            Resume Next
            Exit Sub

DataDirNotExist:
            MkDir(PutFdrv & DataFileSubDir)
            Resume Next
            Exit Sub


ScenarioDirNotExist:
            MkDir(PutFdrv & Session("ScenarioFileSubDir"))
            Resume Next
            Exit Sub

SensitivityDirNotExist:
            MkDir(PutFdrv & Session("SensitivityFileSubDir"))
            Resume Next
            Exit Sub

StartWriteProblem:
            msg = "IFs cannot write to " & PutDataFileDir & "  .  Make sure that " & NL
            msg = msg & "the IFS.INI file contains a valid path to which "
            msg = msg & "IFs can write local files.  " & NL
            msg = msg & "Execution will now cease."
            'jrs 2005/02/10
            msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
            Session("Error") = msg
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")

        End Sub


        '==========================frm_IFs=============================
        Private Sub UpdateCurrencyCountry(ByRef sCountry As String)
            Dim msg As String
            '--------------------------------------------------------------

            On Error GoTo UpdateCurrencyError
            '    msg$ = "Have entered UpdateCurrency with sCountry = " + sCountry
            '    MsgBox msg$

            'mtiwebforms April 2004 ADO.Net
            'Dim db As DAOJR.Database
            Dim db As String 'DAOJR.Database

            Dim sUpdate As String
            If sCountry > "" Then
                '    msg$ = "Ready to dimension db"
                '    MsgBox msg$
                '    msg$ = "Ready to set db with PutFdrv$ = " + PutFdrv$
                '    MsgBox msg$
                'mtiwebforms April 2004 ADO.Net
                'db = DAODBEngine_definst.OpenDatabase(PutFdrv & "DATA\IFS.MDB")
                db = PutFdrv & "DATA\IFS.MDB"
                '    msg$ = "Ready to dimension sUpdate"
                '    MsgBox msg$
                '    msg$ = "Ready to set sUpdate "
                '    MsgBox msg$
                'jrs 2005/01/24
                Dim dt As DataTable
                SetDataTablefromAccess(db, "SELECT COUNT(*) FROM [Currency] WHERE [Currency Flag] = 1 AND [Currency Value] > 0 AND [Country] = """ & sCountry & """", "", dt)
                If dt.Rows.Count = 0 Then

                    sUpdate = "Update [Currency] SET [Currency Flag] = 0 WHERE [Currency Value]>0 and [Currency Flag] <> 0"
                    updateAccessWith1Command(db, sUpdate)
                    '    msg$ = "sUpdate= " + sUpdate
                    '    MsgBox msg$
                    'db.Execute(sUpdate)
                    '    msg$ = "Ready to set sUpdate"
                    '    MsgBox msg$
                    sUpdate = "Update [Currency] SET [Currency Flag] = 1 WHERE [Currency Value]>0 and [Country] = """ & sCountry & """"
                    updateAccessWith1Command(db, sUpdate)
                    '    msg$ = "sUpdate= " + sUpdate
                    '    MsgBox msg$
                    'db.Execute(sUpdate)
                    '    msg$ = "Have executed sUpdate"
                    '    MsgBox msg$
                    'db.Close()
                End If
            End If
            Exit Sub

UpdateCurrencyError:
            msg = "Error occurred processing currency update.  Make sure "
            msg = msg & "that IFs.mdb exists on \DATA directory"
            Session("Error") = msg
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")

        End Sub

        'jrs 2005/01/18
        Public Overrides Sub SessionLoad(Optional ByVal SessionIdx As Integer = -1)
            Dim directory As String
            If SessionIdx >= 0 Then
                Session("SessionIdx") = SessionIdx
            End If

            Dim msg As String
            '--------------------------------------------------------------

            '   Set basic Default File Names/Locations/Time Horizons
            '   ----------------------------------------------------

            'jrs 2005/01/18
            'Session("ResFileSubDir") = SeekValue(valtblInit, "ResFileSubDir$", "Value")
            'Session("ScenarioFileSubDir") = SeekValue(valtblInit, "ScenarioFileSubDir$", "Value")
            'Session("SensitivityFileSubDir") = SeekValue(valtblInit, "SensitivityFileSubDir$", "Value")

            'JRS MULTISESSION 06/02/04 ***
            If Session("SessionIdx") <> -1 Then
                Session("ResFileSubDir") = Session("ResFileSubDir") & "Session"
                Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & "Session"
                Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & "Session"
                If Session("SessionIdx") < 10 Then
                    Session("ResFileSubDir") = Session("ResFileSubDir") & "0"
                    Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & "0"
                    Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & "0"
                End If
                Session("ResFileSubDir") = Session("ResFileSubDir") & Trim(Session("SessionIdx")) & "\"
                Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & Trim(Session("SessionIdx")) & "\"
                Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & Trim(Session("SessionIdx")) & "\"
            End If
            '*****************************

            '   Check for all Subdirectories on Target Directory
            '   ------------------------------------------------

            On Error GoTo ResDirNotExist 'Make Sure Local Result Subdirectory Exists
            ChDir(PutFdrv & Session("ResFileSubDir"))
            On Error GoTo ScenarioDirNotExist 'Make Sure Local Scenario Subdirectory Exists
            ChDir(PutFdrv & Session("ScenarioFileSubDir"))
            On Error GoTo SensitivityDirNotExist 'Make Sure Local Sensitivity Subdirectory Exists
            ChDir(PutFdrv & Session("SensitivityFileSubDir"))


            '   Establish run files directory paths
            '   -----------------------------------

            Session("FdrvRun") = FDRV & Session("ResFileSubDir")
            Session("PutFdrvRun") = PutFdrv & Session("ResFileSubDir")
            Session("PutFdrvRun2") = Session("PutFdrvRun") 'Used for file management with dialog box
            If VB.Right(Session("PutFdrvRun2"), 1) = "\" Then
                Session("PutFdrvRun2") = VB.Left(Session("PutFdrvRun2"), Len(Session("PutFdrvRun2")) - 1)
            End If

            Dim DataHistFile As String
            Dim DataFile As String
            Dim NL As String
            '--------------------------------------------------------------

            NL = "<br>" & Chr(10)

            On Error GoTo StartWriteProblem 'Add Extra Line to Date.Dat to Check Write

            '   Data file locations
            '   --------------------
            valtblInit = Session("valtblInit")
            DataFile = SeekValue(valtblInit, "DataFile$", "Value")
            DataHistFile = SeekValue(valtblInit, "DataHistFile$", "Value")

            Session("IFs_MDB2") = Session("PutFdrvRun") & DataFile
            Session("IFsHist_MDB2") = Session("PutFdrvRun") & DataHistFile
            Session("IFsCoVaTrA_MDB2") = Session("PutFdrvRun") + SeekValue(valtblInit, "IFsCoVaTrA_MDB2", "Value")

            '   Change to Start Directory
            '   -------------------------

            DefaultDrive = VB.Left(FDRV, 2)
            ChDrive(DefaultDrive)
            directory = FDRV
            If VB.Right(directory, 1) = "\" Then
                directory = VB.Left(directory, Len(directory) - 1)
            End If
            ChDir(directory)

            '   Delete Working Scenario at the load of Model
            '   --------------------------------------------

            'establish scenario directory
            Session("ScenarioFileDir") = PutFdrv & Session("ScenarioFileSubDir")
            Session("SensitivityFileDir") = PutFdrv & Session("SensitivityFileSubDir")
            'Delete Working Scenario at the load of Model
            If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
                Kill(Session("ScenarioFileDir") & "Working.Sce")
            End If
            'JRS 07/15/03 BEGIN
            If Dir(Session("ScenarioFileDir") & "WorkingGroup.Sce") <> "" Then
                Kill(Session("ScenarioFileDir") & "WorkingGroup.Sce")
            End If
            'JRS 07/15/03 END
            If Dir(Session("ScenarioFileDir") & "Working.Txt") <> "" Then
                Kill(Session("ScenarioFileDir") & "Working.Txt")
            End If
            'jrs 2005/02/12
            Dim fNum As Integer
            fNum = FreeFile()

            'write a blank file
            FileOpen(fNum, Session("ScenarioFileDir") & "Working.Txt", OpenMode.Output)
            FileClose(fNum)

            Static Basefile As String
            Static InFile As String
            Static OutFile As String
            Static ErrMsg As String
            '--------------------------------------------------------------

            On Error GoTo InitFileReadError

            '   Identify Start Files
            '   --------------------
            Basefile = SeekValue(valtblInit, "Basefile$", "Value")

            '   check if filename.RUN exists
            '   ----------------------------

            'JRS MULTISESSION 06/02/04
            'IF .RUN FILE IS NOT THERE TRY TO COPY FROM RUNFILES
            Dim sRunDir As String
            If InStr(Session("FdrvRun"), "Session") > 0 Then
                sRunDir = Mid(Session("FdrvRun"), 1, InStr(Session("FdrvRun"), "Session") - 1)
            Else
                sRunDir = Session("FdrvRun")
            End If

            ErrMsg = "The file IFSBASE.RUN must exist on the RUNFILES subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(sRunDir & Basefile & ".run", ErrMsg, Session("Error"), Me)
            ErrMsg = ""

            FileCopyCheck(sRunDir & Basefile & ".run", Session("FdrvRun") & Basefile & ".run")

            '   check if filenameHIST.RUN exists
            '   ----------------------------

            ErrMsg = "The file IFSBASEHIST.RUN must exist on the RUNFILES subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            If ModelLoad <> "LoadStudent" And CBool(DevelopmentMode) = False Then 'Not used for students
                Call FileCheck(sRunDir & Basefile & "HIST.run", ErrMsg, Session("Error"), Me)
            End If
            ErrMsg = ""

            FileCopyCheck(sRunDir & Basefile & "HIST.run", Session("FdrvRun") & Basefile & "HIST.run")

            '   check if filename.RUN exists
            '   ----------------------------

            InFile = PutDataFileDir & "SAMBase.mdb"
            OutFile = sRunDir & "SAMBase.mdb"
            FileCopyCheck(InFile, OutFile)

            FileCopyCheck(sRunDir & "SAMBase.mdb", Session("FdrvRun") & "SAMBase.mdb")

            'jrs 10/31/04
            ' check if run files for the web exist
            ' ------------------------------------
            Dim sIP As String
            Dim Files As DataTable
            Dim DInf As New DirectoryInfo(Me.MapPathSecure(""))
            sIP = DInf.Parent.FullName & "\IFsInit.mdb"
            'jrs 2005/04/18
            'jrs 2005/06/27
            'If IsNothing(Session("VersionName")) Then
            '    SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r, IFsInit as i WHERE r.VersionName = i.Value AND i.Variable = 'VersionName' ORDER BY FileName", "", Files)
            'Else
            '    SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r WHERE r.VersionName = '" & Session("VersionName") & "' ORDER BY FileName", "", Files)
            'End If

            Dim j As Integer
            Dim s As String
            'jrs 2005/06/27
            Dim f As MyFile
            For Each f In Session("IFsRunFiles").colMyFiles
                If f.Visible Then
                    s = Mid(f.Name, 1, Len(f.Name) - 4)
                    ErrMsg = "The file " & UCase(s) & ".RUN must exist on the RUNFILES subdirectory"
                    ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                    ErrMsg = ErrMsg & "You may need to re-install IFs."
                    Call FileCheck(sRunDir & s & ".run", ErrMsg, Session("Error"), Me)
                    ErrMsg = ""

                    FileCopyCheck(sRunDir & s & ".run", Session("FdrvRun") & s & ".run")

                    ErrMsg = "The file SAM" & UCase(s) & ".MDB must exist on the RUNFILES subdirectory"
                    ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                    ErrMsg = ErrMsg & "You may need to re-install IFs."
                    Call FileCheck(sRunDir & "SAM" & s & ".mdb", ErrMsg, Session("Error"), Me)
                    ErrMsg = ""

                    FileCopyCheck(sRunDir & "SAM" & s & ".mdb", Session("FdrvRun") & "SAM" & s & ".mdb")

                End If
            Next
            'For j = 0 To Files.Rows.Count - 1
            '    s = Files.Rows(j).Item("FileName")
            '    ErrMsg = "The file " & UCase(s) & ".RUN must exist on the RUNFILES subdirectory"
            '    ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            '    ErrMsg = ErrMsg & "You may need to re-install IFs."
            '    Call FileCheck(sRunDir & s & ".run", ErrMsg, Session("Error"), Me)
            '    ErrMsg = ""

            '    FileCopyCheck(sRunDir & s & ".run", Session("FdrvRun") & s & ".run")

            '    ErrMsg = "The file SAM" & UCase(s) & ".MDB must exist on the RUNFILES subdirectory"
            '    ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            '    ErrMsg = ErrMsg & "You may need to re-install IFs."
            '    Call FileCheck(sRunDir & "SAM" & s & ".mdb", ErrMsg, Session("Error"), Me)
            '    ErrMsg = ""

            '    FileCopyCheck(sRunDir & "SAM" & s & ".mdb", Session("FdrvRun") & "SAM" & s & ".mdb")

            'Next

            'jrs 2007/01/15
            '   Check if Provinces.mdb exists
            '   ----------------------------

            ErrMsg$ = "The file Provinces.mdb must exist on the DATA subdirectory"
            ErrMsg$ = ErrMsg$ & " of the directory from which you attempt to run IFS.  "
            ErrMsg$ = ErrMsg$ & "You may need to re-install IFs."
            Call FileCheck(FDRV$ + "DATA\Provinces.mdb", ErrMsg, Session("Error"), Me)
            ErrMsg$ = ""

            '   Copy TablFunc.MDB, IFs.MDB, IFsVar.MDB, DataDict.MDB, IFsPop.MDB
            '   to Working Location in local Runfiles directory (for network)
            '   -------------------------------------------------------------------------------

            On Error GoTo FileCopyError
            InFile = DataFileDir & "TablFunc.mdb"
            OutFile = Session("PutFdrvRun") & "TablFunc.mdb"
            FileCopyCheck(InFile, OutFile)

            InFile = DataFileDir & "AnalFunc.mdb"
            OutFile = Session("PutFdrvRun") & "AnalFunc.mdb"
            FileCopyCheck(InFile, OutFile)

            InFile = DataFileDir & "IFsVar.mdb"
            OutFile = Session("PutFdrvRun") & "IFsVar.mdb"
            FileCopyCheck(InFile, OutFile)

            InFile = DataFileDir & "IFsPop.mdb"
            OutFile = Session("PutFdrvRun") & "IFsPop.mdb"
            'JAM 2020-07-21
            If Dir(InFile) > "" Then
                FileCopyCheck(InFile, OutFile)
            End If

            'JAM 2020-07-20
            InFile = DataFileDir & "IFsPop.db"
            OutFile = Session("PutFdrvRun") & "IFsPop.db"
            'JAM 2020-07-21
            If Dir(InFile) > "" Then
                FileCopyCheck(InFile, OutFile)
            End If


            InFile = DataFileDir & "DataDict.mdb"
            OutFile = Session("PutFdrvRun") & "DataDict.mdb"
            FileCopyCheck(InFile, OutFile)

            InFile = IFs_MDB
            OutFile = Session("IFs_MDB2")
            FileCopyCheck(InFile, OutFile)

            'JRS 10/08/02 GUIDED USE
            InFile = DataFileDir & "GuidedUse.mdb"
            OutFile = Session("PutFdrvRun") & "GuidedUse.mdb"
            FileCopyCheck(InFile, OutFile)

            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                InFile = DataFileDir & "IFsCoVaTrA.MDB"
                OutFile = Session("PutFdrvRun") & "IFsCoVaTrA.MDB"
                FileCopyCheck(InFile, OutFile)
            End If

            'jrs 2007/01/15
            InFile$ = DataFileDir$ + "Provinces.mdb"
            OutFile$ = Session("PutFdrvRun") + "Provinces.mdb"
            FileCopyCheck(InFile$, OutFile$)

            'jrs 2008/11/20
            InFile$ = DataFileDir$ + "ifsSmoking.mdb"
            OutFile$ = Session("PutFdrvRun") + "ifsSmoking.mdb"
            FileCopyCheck(InFile$, OutFile$)


            '  Copy data files to local on network
            '       - so that .LDB read/write not a problem
            '   -------------------------------------------
            On Error GoTo FileCopyError

            Session("HistoricBase") = CStr(False) 'Default to forecasts
            Session("ActiveBase") = "IFSBASE.RUN"
            Session("IFs_MDB2Active") = Session("IFs_MDB2")

            InFile = IFsHist_MDB
            OutFile = Session("IFsHist_MDB2")
            FileCopyCheck(InFile, OutFile)

            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                InFile = IFsCoVaTrA_MDB
                OutFile = Session("IFsCoVaTrA_MDB2")
                FileCopyCheck(InFile, OutFile)
            End If

            '   Copy HistSeries File to Working Location in local Runfiles directory
            '   --------------------------------------------------------------------

            InFile = "IFsHistSeries_MDB" 'In case of error, need for error message
            OutFile = "IFsHistSeries_MDB"
            FileCopyCheck(DataFileDir & "IFsHistSeries.mdb", Session("PutFdrvRun") & "IFsHistSeries.mdb")
            Session("IFsHistSeries_MDB") = Session("PutFdrvRun") & "IFsHistSeries.mdb"
            Session("IFsCoVaTrASeries_MDB") = Session("PutFdrvRun") & "IFsCoVaTrASeries.mdb"

            '    Set db = OpenDatabase(IFsHistSeries_MDB, False, True, DataPW$)

            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
                InFile = "IFsCoVaTrASeries_MDB"
                OutFile = "IFsCoVaTrASeries_MDB"
                FileCopyCheck(DataFileDir & "IFsCoVaTrASeries.mdb", Session("PutFdrvRun") & "IFsCoVaTrASeries.mdb")
            End If

            '   Copy Cohort File to Working Location in local Runfiles directory
            '   --------------------------------------------------------------------

            InFile = "IFsWVSCohort_MDB"
            OutFile = "IFsWVSCohort_MDB"
            FileCopyCheck(DataFileDir & "IFsWVSCohort.mdb", Session("PutFdrvRun") & "IFsWVSCohort.mdb")

            Session("IFsWVSCohort_MDB") = Session("PutFdrvRun") & "IFsWVSCohort.mdb"

            '   Copy Pop Base Case to Work Case to Hold Cohort Data
            '   ---------------------------------------------------

            InFile = "IFsPopWork_MDB"
            OutFile = "IFsPopWork_MDB"
            'jrs 2008/06/18
            FarmFileAllocator("POP", Session("PutFdrvRun") & "IFsPopWork.mdb", Session("mnuSQLite"))
            'jrs 2006/09/15 FileCopyCheck(DataFileDir & "IFsPopWork.mdb", Session("PutFdrvRun") & "IFsPopWork.mdb")


            'JAM 2020-07-21
            If Dir(DataFileDir & "IFsPop.mdb") > "" Then
                FileCopyCheck(DataFileDir & "IFsPop.mdb", Session("PutFdrvRun") & "IFsPopWork.mdb")
            End If


            'JAM 2020-07-21
            If Dir(DataFileDir & "IFsPop.db") > "" Then
                'JAM 2020-07-20
                FileCopyCheck(DataFileDir & "IFsPop.db", Session("PutFdrvRun") & "IFsPopWork.db")
            End If

            '   Copy Statistics File to Working Location in local Runfiles directory
            '   --------------------------------------------------------------------

            InFile = "IFsStat_MDB"
            OutFile = "IFsStat_MDB"
            FileCopyCheck(DataFileDir & "IFsStat.mdb", Session("PutFdrvRun") & "IFsStat.mdb")
            Session("IFsStat_MDB") = Session("PutFdrvRun") & "IFsStat.mdb"

            '   On Network, copy IFSBASE.RUN to local directory
            '   -----------------------------------------------
            InFile = Session("FdrvRun") & Basefile & ".RUN"
            OutFile = Session("PutFdrvRun") & Basefile & ".RUN"
            If Session("FdrvRun") <> Session("PutFdrvRun") Then
                FileCopyCheck(InFile, OutFile)
            End If


            '   On Network, copy IFSBASEHIST.RUN to local directory
            '   -----------------------------------------------
            If ModelLoad <> "LoadStudent" And CBool(DevelopmentMode) = False Then 'Historical run not available for students
                InFile = Session("FdrvRun") & Basefile & "HIST.RUN"
                OutFile = Session("PutFdrvRun") & Basefile & "HIST.RUN"
                If Session("FdrvRun") <> Session("PutFdrvRun") Then
                    FileCopyCheck(InFile, OutFile)
                End If
            End If

            '   Copy IFSBASE.RUN to working.run
            '   -----------------------------
            InFile = Session("PutFdrvRun") & Basefile & ".RUN.DB"
            OutFile = Session("PutFdrvRun") & "working.run.db"
            'jrs 2006/07/20 FARM
            FarmFileAllocator("BASE", OutFile, Session("mnuSQLite"))
            FileCopyCheck(InFile, OutFile)
            Session("WorkingFile") = Session("PutFdrvRun") & "IFSBASE.RUN.DB"

            'JRS 10/16/02 SAM COPY
            '   Copy SAMBASE.MDB to SAMWORKING.MDB
            '   -----------------------------
            InFile = PutDataFileDir & "SAMBase.mdb"
            OutFile = Session("PutFdrvRun") & "SAMWorking.mdb"
            'jrs 2006/07/20 FARM
            FarmFileAllocator("SAM", OutFile, Session("mnuSQLite"))
            FileCopyCheck(InFile, OutFile)

            '   Establish Files Selected
            '   ------------------------
            Session("FilesSelected") = 1
            ReDim Session("RunFiles")(10)
            Session("RunFiles")(1) = Session("PutFdrvRun") & "IFSBASE.RUN"


            Exit Sub



InitFileReadError:

            msg = "There is an error in init, probably a missing file."
            'jrs 2005/02/12
            msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
            Session("Error") = msg
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")

FileCopyError:
            msg = "An error has arisen, probably during an attempt to copy "
            msg = msg & InFile & " to " & OutFile & NL
            msg = msg & "Check existence of source file and space availability."
            'jrs 2005/02/12
            msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
            Session("Error") = msg
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")


ResDirNotExist:
            MkDir(PutFdrv & Session("ResFileSubDir"))
            Resume Next
            Exit Sub


ScenarioDirNotExist:
            MkDir(PutFdrv & Session("ScenarioFileSubDir"))
            Resume Next
            Exit Sub

SensitivityDirNotExist:
            MkDir(PutFdrv & Session("SensitivityFileSubDir"))
            Resume Next
            Exit Sub

StartWriteProblem:
            msg = "IFs cannot write to " & PutDataFileDir & "  .  Make sure that " & NL
            msg = msg & "the IFS.INI file contains a valid path to which "
            msg = msg & "IFs can write local files.  " & NL
            msg = msg & "Execution will now cease."
            'jrs 2005/02/12
            msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
            Session("Error") = msg
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")
            Exit Sub

        End Sub

    End Class

'End Namespace
