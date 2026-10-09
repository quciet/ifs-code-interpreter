Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_Scatter
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
        'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.
        If Not Page.IsPostBack Then
            Dim Msg As String
            Dim ii As Short
            Dim NextLeft As Short
            Dim ButSpace As Short
            Dim SumWidth As Object
            '------------------------------------------------------------

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            On Error GoTo FileProblem

            '   Set count for scattegram and call scaling routine
            '   -------------------------------------------------

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("SCATCOUNTE") = Session("CCount") * 2

            '    Call ScaledGraph

            '   Set form and graph dimensions
            '   -----------------------------

            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
            'frm_Scatter.DefInstance.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - HalfInch + 200)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            'frm_Scatter.DefInstance.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - HalfInch / 2)
            'CONVERSION_NOTE: Property 'System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Left.html'.
            'frm_Scatter.DefInstance.Left = 0 
            'CONVERSION_NOTE: Property 'System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Top.html'.
            'frm_Scatter.DefInstance.Top = 0 
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.ClientRectangle.html'.
            'lblJS.Width = System.Web.UI.WebControls.Unit.Pixel(frm_Scatter.DefInstance.ClientRectangle.Width)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.ClientRectangle.html'.
            'lblJS.Height = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(frm_Scatter.DefInstance.ClientRectangle.Height) - HalfInch))
            'lblJS.Style.Item("LEFT") = 0
            'lblJS.Style.Item("TOP") = 0

            '   Set graph title, type and style
            '   -------------------------------

            'If Len(Session("TableTitle")) > 49 Then Session("TableTitle") = VB.Left(Session("TableTitle"), 49)
            'lblJS.GraphTitle = Session("TableTitle")
            'lblJS.GraphTitle = ""
            ''UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'lblJS.Session("GraphType") = Session("GraphType")
            'lblJS.Session("GraphStyle") = Session("GraphStyle")

            '   Position buttons
            '   ----------------

            'UPGRADE_WARNING: Couldn't resolve default property of object SumWidth. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SumWidth = VB6.PixelsToTwipsX(cmd_Print.Width.Value) + VB6.PixelsToTwipsX(cmd_Exit.Width.Value) + VB6.PixelsToTwipsX(Cmd_Save.Width.Value) + VB6.PixelsToTwipsX(cmd_Plot.Width.Value)
            'UPGRADE_WARNING: Couldn't resolve default property of object SumWidth. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.ClientRectangle.html'.

            'mtiwebform June 2004 Cross-Sectional 
            ButSpace = 500 '(VB6.PixelsToTwipsX(frm_Scatter.DefInstance.ClientRectangle.Width) - SumWidth) / 5

            'mtiwebform June 2004 Cross-Sectional  - comment the following 
            'cmd_Print.Style.Item("LEFT") = VB6.TwipsToPixelsX(ButSpace)
            'NextLeft = VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(cmd_Print.Style.Item("LEFT")).value) + VB6.PixelsToTwipsX(cmd_Print.Width.Value) + ButSpace
            'cmd_Plot.Style.Item("LEFT") = VB6.TwipsToPixelsX(NextLeft)
            'NextLeft = VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(cmd_Plot.Style.Item("LEFT")).value) + VB6.PixelsToTwipsX(cmd_Plot.Width.Value) + ButSpace
            'Cmd_Save.Style.Item("LEFT") = VB6.TwipsToPixelsX(NextLeft)
            'NextLeft = VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(Cmd_Save.Style.Item("LEFT")).value) + VB6.PixelsToTwipsX(Cmd_Save.Width.Value) + ButSpace
            'cmd_Exit.Style.Item("LEFT") = VB6.TwipsToPixelsX(NextLeft)
            ''CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.ClientRectangle.html'.
            '         'cmd_Print.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblJS.Height.Value) + (VB6.PixelsToTwipsY(frm_Scatter.DefInstance.ClientRectangle.Height) - VB6.PixelsToTwipsY(lblJS.Height.Value) - VB6.PixelsToTwipsY(cmd_Print.Height.Value)) / 2)
            'Cmd_Save.Style.Item("TOP") = System.Web.UI.WebControls.Unit.Parse(cmd_Print.Style.Item("TOP")).value
            'cmd_Exit.Style.Item("TOP") = System.Web.UI.WebControls.Unit.Parse(cmd_Print.Style.Item("TOP")).value
            'cmd_Plot.Style.Item("TOP") = System.Web.UI.WebControls.Unit.Parse(cmd_Print.Style.Item("TOP")).value
            'end comment ' mtiwebform June 2004 Cross-Sectional	

            '   Prepare graph Labels
            '   --------------------

            'lblJS.ThickLines = 1
            'If Len(VarNameI(0)) > 70 Then 
            '	VarNameI(0) = VB.Left(VarNameI(0), 70)
            'End If 
            'lblJS.BottomTitle = VarNameI(0)
            'If Len(VarNameD) > 70 Then 
            '	VarNameD = VB.Left(VarNameD, 70)
            'End If 
            'lblJS.LeftTitle = VarNameD

            Session("GType") = "SCAT"

            '   Set Dimensions of Graph and Grid
            '   --------------------------------

            'lblJS.NumSets = COUNTE / 2
            'lblJS.NumPoints = 2
            ''CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'grd_Scatter.Rows = COUNTE
            ''CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.cols is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'grd_Scatter.Cols = 2
            'Support.DataGridManager.manager.SetCol(grd_Scatter, 0)

            '   Place labels
            '   ------------

            'For ii = 1 To lblJS.NumPoints
            '	lblJS.ThisPoint = ii
            'Next ii

            '   Fill in data
            '   ------------

            'Call FillData()


            ''   Graph values
            ''   ------------

            'Call GraphData()
            GoTo EndofSub

FileProblem:
            Msg = "Sorry. An error has arisen during an attempt to read " & Session("VarNameI")(0)
            Msg = Msg & " or "
            Msg = Msg & "an error has arisen during an attempt to read " & Session("VarNameD")
            Msg = Msg & " - a variable or data for it doesn't "
            Msg = Msg & "exist in the file.  Please try another variable."

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

EndofSub:

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

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
    'Public WithEvents lblJS As AxGraphLib.AxGraph 
    'CONVERSION_NOTE: Class 'AxMSComDlg.AxCommonDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents cmd_FileAccess As AxMSComDlg.AxCommonDialog 
    Protected WithEvents WinFormsToWeb_MessageBox As ArtinSoft.WinFormsToWeb.WebControls.MessageBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_ScatterPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_Scatter
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_Scatter()
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
    '------------------------------------------Scatter.Frm------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-1998
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '

    'jrs 2011/09/29
    'JAM 2009-04-11  GList color pattern used for countries
    Dim GListColor As String
    Dim GListColorCount As Integer
    Dim PairColor(0, 0)
    Dim ExtpType$
    Dim RSQR As Double
    Dim vreg As New RegressionObject
    Dim IndIndex As Integer

    '=============================Scatter.Frm  =======================
    Private Sub FillData()
        'Dim cc As Short
        'Dim ICount As Short
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        ''CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        ''CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''   Fill in data
        ''   ------------

        'ICount = 0
        'For cc = 1 To lblJS.NumSets

        '	'       Get yearly values
        '	'       -----------------

        '	Support.DataGridManager.manager.SetRow(grd_Scatter, ICount)
        '	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
        '	If Not IsDbNull(Session("CVarI")(cc)) And Not IsDbNull(CVarD(cc)) Then 
        '		Support.DataGridManager.manager.SetCol(grd_Scatter, 0)
        '		'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '		Support.DataGridManager.SetText(grd_Scatter, Str(Session("CVarI")(cc)))
        '		Support.DataGridManager.manager.SetCol(grd_Scatter, 1)
        '		'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '		Support.DataGridManager.SetText(grd_Scatter, Str(CVarD(cc)))
        '		ICount = ICount + 1
        '	End If 
        'Next cc
        'lblJS.NumSets = ICount
        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        ''CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        ''CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


    End Sub



    '==========================frm_Scatter===================
    Private Sub cmd_Exit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        On Error GoTo Exit_Click_Error


        Session("SCATCOUNTE") = 0

        'JAM 2010-01-23
        Session.Remove("mnuNamesChecked")
        Session.Remove("mnuCodesChecked")
        Session.Remove("mnuNolabelsChecked")
        Session.Remove("mnuPopulationChecked")
        Session.Remove("mnuGdpChecked")
        Session.Remove("mnuOtherChecked")
        Session.Remove("mnuNoneChecked")
        Session.Remove("thirdimsel")
        'jrs 2012/01/05
        Session.Remove("RegressType")
        Session.Remove("TableTitle")

        'JAM 2016-10-07
        Session("OnceScatterLoad") = False

        'JAM 2016-10-07
        Session.Remove("AddGlistMode")

        'JAM 2016-11-21
        Session.Remove("MulRegressTitle")

        'JAM 2016-11-22 Removing variables used for Add GList/Group/Year
        Session.Remove("RegressType")
        Session.Remove("IsaGlist")
        Session.Remove("IsaGroup")
        Session.Remove("PrevRemove")
        Session.Remove("MoreScatterLoading")
        Session.Remove("OrigPrevCVarI")
        Session.Remove("OrigPrevCVarISec")
        Session.Remove("OrigPrevCVarD")
        Session.Remove("OrigPrevCVarName")
        Session.Remove("OrigPrevCVarCode")
        Session.Remove("OrigPrevCVarIPop")
        Session.Remove("OrigPrevCVarIGdp")
        Session.Remove("MulRegressY")
        Session.Remove("MulRegressX")
        Session.Remove("RegressTitle")
        Session.Remove("MulRCountryRegress")
        Session.Remove("JoinPrevCVarI")
        Session.Remove("JoinPrevCVarISec")
        Session.Remove("JoinPrevCVarD")
        Session.Remove("JoinPrevCVarName")
        Session.Remove("JoinPrevCVarCode")
        Session.Remove("JoinPrevCVarIPop")
        Session.Remove("JoinPrevCVarIGdp")
        Session.Remove("DimJoinPrev")
        Session.Remove("GLNameJoinPrev")
        Session.Remove("JoinPrevCVarAlias")
        Session.Remove("GLDecompIndex")
        Session.Remove("YearsLabels")
        Session.Remove("GLbLagDep")
        Session.Remove("GLbLagInd")
        Session.Remove("LagDepent")
        Session.Remove("LagFirInd")
        Session.Remove("LagSecInd")
        Session.Remove("LagIPop")
        Session.Remove("LagIGdp")
        Session.Remove("vAddYear")
        Session.Remove("LagDepChs")
        Session.Remove("LagIndChs")
        Session.Remove("LagScIChs")
        Session.Remove("FirstPrevious")
        Session.Remove("StepInd")
        Session.Remove("StepDep")
        Session.Remove("StepEqual")
        Session.Remove("isLagInd")
        Session.Remove("IsLagDep")
        Session.Remove("IsLagEqual")
        Session.Remove("LaGlobal")
        Session.Remove("VMatch")
        Session.Remove("LastValue")
        Session.Remove("StepIndSec")
        Session.Remove("StepDepSec")
        Session.Remove("StepEqualSec")
        Session.Remove("isLagIndSec")
        Session.Remove("IsLagDepSec")
        Session.Remove("IsLagEqualSec")
        Session.Remove("GChanged")
        Session.Remove("AuxArray")
        Session.Remove("OrigTemp")
        Session.Remove("SubSetLabelsChanged")
        Session.Remove("PrevCVarI")
        Session.Remove("PrevCVarISec")
        Session.Remove("PrevCVarD")
        Session.Remove("PrevCVarName")
        Session.Remove("PrevCVarCode")
        Session.Remove("PrevCVarIPop")
        Session.Remove("PrevCVarIGdp")
        Session.Remove("PrevCVarAlias")
        Session.Remove("CoefB1")
        Session.Remove("CoefB2")
        Session.Remove("CoefB3")
        Session.Remove("CoefB4")
        Session.Remove("RegressTitle")
        Session.Remove("MulRMinVI")
        Session.Remove("MulRMaxVI")
        Session.Remove("ExcludeCtry")
        ''''''''''''''''''''''''''''''''''''''''''''''
        'JAM 2016-12-11
        Session.Remove("AddingYear")
        Session.Remove("FirstCVarCounName")
        Session.Remove("FirstCVarCounCode")
        Session.Remove("FirstOrigIndFir")
        Session.Remove("FirstOrigDep")
        Session.Remove("FirstOrigIndSec")
        Session.Remove("FirstIPopOrig")
        Session.Remove("FirstIGdpOrig")
        Session.Remove("SingleDimJoinPrev")

        'JAM 2017-01-28
        Session.Remove("GlistCountChosen")
        Session.Remove("GlistCountChanged")

        'JAM 2018-04-29
        ''Session.Remove("bBubble")
        ''Session.Remove("ThirdDimOk")


        'Session.Remove("ThirdDimOk")
        'Support.FormManager.Close(Me)

        'JAM 2019-02-10
        Session.Remove("ShowPoints")
        Session.Remove("ShowLabels")


        Support.FormManager.Show(Me, Session("ScatterParent"))

        Exit Sub

Exit_Click_Error:
        'MsgBox("Error on exit action")
        Exit Sub


    End Sub

    '==========================frm_Scatter===================
    Private Sub cmd_Plot_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Plot.Click
        Dim AnsVal As Object
        Dim TryAgain As String
        Dim NL As String
        Dim Msg As String
        Dim rr As Short
        Dim InFile2 As String
        Dim OutFile As String
        Dim InFile1 As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Carret = Chr(13) & Chr(10)
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        carret2 = Carret + Carret


        '   Copy permanent pre-scattergram file to working
        '   ----------------------------------------------

        On Error GoTo FileCopyError1
        InFile1 = Session("FdrvRun") & "PreScat.xls" 'File Students should not touch - template
        OutFile = Session("PutFdrvRun") & "Scatter.xls" 'File IFs fills with data
        FileCopy(InFile1, OutFile) 'Filecopy does not generate error whether or not
        'target file exists

        '    GoTo startole:
        '   Use Excel in OLE automation to edit scatterplot
        '   -----------------------------------------------

        On Error GoTo FileWriteError
        Dim x As Object
        InFile2 = Session("PutFdrvRun") & "Scatter.xls"
        OutFile = Session("PutFdrvRun") & "Scatter2.xls" 'Used so can save as new file
        'and avoid question about overwriting file
        On Error Resume Next 'Just skip next statement if file does not exist
        Kill(OutFile) 'Delete because .SaveAs otherwise generates question
        On Error GoTo FileWriteError
        x = CreateObject("Excel.Application") '("Excel.Application.8")
        '    X.Visible = True
        'UPGRADE_WARNING: Couldn't resolve default property of object x.Workbooks. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        x.Workbooks.Open(Filename:=InFile2)
        'UPGRADE_WARNING: Couldn't resolve default property of object x.Range. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        x.Range("A1").Select() 'If graph object is selected, that would block value assign;
        'A1 Select will give focus to data cell
        'UPGRADE_WARNING: Couldn't resolve default property of object x.Cells. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        x.Cells(1, 1).Value = Session("VarNameI")(0)
        'UPGRADE_WARNING: Couldn't resolve default property of object x.Cells. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        x.Cells(1, 2).Value = Session("VarNameD")
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For rr = 1 To Session("CCount")
            'UPGRADE_WARNING: Couldn't resolve default property of object x.Cells. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(rr). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            x.Cells(rr + 1, 1).Value = Session("CVarI")(rr)
            'UPGRADE_WARNING: Couldn't resolve default property of object x.Cells. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(rr). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            x.Cells(rr + 1, 2).Value = Session("CVarD")(rr)
            'UPGRADE_WARNING: Couldn't resolve default property of object x.Cells. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            x.Cells(rr + 1, 3).Value = Session("CVarName")(rr)
        Next rr
        '    X.Visible = True
        Msg = "Please answer 'yes' to the following question(s)."
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 2005/11/03 WinFormsToWeb_MessageBox.Show(Msg)
        '    On Error GoTo SaveAsFailure
        '    X.Parent.SaveAs OutFile$ 'Excel 5.0 will not write to
        'Windows 95 filename with spaces in it - must trap
        On Error GoTo QuitApplicationError
        'UPGRADE_WARNING: Couldn't resolve default property of object x.Application. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        x.Application.Quit()
        'UPGRADE_NOTE: Object x may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
        x = Nothing
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '   Open Excel with OLE to display graph and trend line
        '   ------------------------------------------
        'startole:
        Msg = "Hello.  You will now proceed to Excel.  Please exit"
        Msg = Msg & " from Excel when you wish to continue with IFs."
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 2005/11/04 WinFormsToWeb_MessageBox.Show(Msg) 'If do not display such a message, the program
        'rushes ahead to OLE before completing quit of above Automation
        'and then generates an error message.  Must interupt it.
        On Error GoTo ActivateExcelError
        'UPGRADE_ISSUE: Ole method OLE1.CreateLink was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'JRSVBNET OLE1.CreateLink(InFile2) 'OutFile$
        'UPGRADE_ISSUE: Ole method OLE1.DoVerb was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'JRSVBNET OLE1.DoVerb() 'Linked to Scatter; allows me to edit figure
        Exit Sub

ActivateExcelError:
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Msg = "An error has arisen during the attempt to activate Excel.  " & NL
        Msg = Msg & "Is Excel available?"

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


FileCopyError1:
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Msg = "An error has arisen during an attempt to copy "
        Msg = Msg & InFile1 & " to " & OutFile & NL
        Msg = Msg & " - Check existence of source file and space availability."

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


FileCopyError2:
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Msg = "An error has arisen during an attempt to copy "
        Msg = Msg & InFile2 & " to " & OutFile & NL
        Msg = Msg & " - Check existence of source file and space availability."
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


SaveAsFailure:
        On Error GoTo FileWriteError
        If InStr(OutFile, " ") > 0 Then
            TryAgain = CStr(True)
            Msg = "Problem with file " & OutFile & ".  If you are using Excel 5,"
            Msg = Msg & " you cannot access the scattergram from a path with a "
            Msg = Msg & "blank space in it.  Please indicate a path without a space "
            Msg = Msg & "for Scatter2.xls."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            While CBool(TryAgain) = True
                'mtiwebform June 2004 Cross-Sectional - go to Excel, Commented
                'cmd_FileAccess.DefaultExt = "xls" 'use CommonDialog to get new path
                'cmd_FileAccess.Filename = "Scatter2.xls"
                'cmd_FileAccess.DialogTitle = "Save Scatter2 File for Excel Analysis in .."
                'cmd_FileAccess.InitDir = session("PutFdrvRun")
                'cmd_FileAccess.Action = 1
                'OutFile = cmd_FileAccess.Filename
                'If InStr(OutFile, " ") > 0 Then
                '    Msg = OutFile & " still contains a blank space.  Do you wish to "
                '    Msg = Msg & "try again?"
                '    'UPGRADE_WARNING: Couldn't resolve default property of object AnsVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                '    'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                '    AnsVal = WinFormsToWeb_MessageBox.Show(Msg, MsgBoxStyle.YesNo)
                '    If AnsVal = MsgBoxResult.No Then
                '        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
                '        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                '        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                '        Exit Sub
                '    End If
                'Else
                '    TryAgain = CStr(False)
                'End If
                'end mtiwebform June 2004 Cross-Sectional - go to Excel, Commented
            End While
            FileCopy(InFile1, OutFile) 'copy prescat simply so can kill it without error
            Kill(OutFile) 'if file exists when hit SaveAs asks question and causes error
            'if answer no
            'UPGRADE_WARNING: Couldn't resolve default property of object x.parent. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            x.parent.SaveAs(OutFile)

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        Else
            If Err.Number = 0 Then Resume Next

            GoTo FileWriteError
        End If
        Resume Next

FileWriteError:  'If someone answers "no" to save of scatter file, the
        'Excel analysis of it would be meaningless.
        'Coincidentally, "No" invokes an error - VB bug?
        'The following uses the error to escape.
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Msg = "Error " & Err.Number & ": " & Err.Description & "." & carret2
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg & "Common errors in invoking Excel include: " + Carret
        Msg = Msg & "1. You declined to replace the file 'SCATTER.XLS' "
        Msg = Msg & "and therefore Excel cannot be invoked to analyze it"
        Msg = Msg & " (error 1004, SaveAs Failed) - answer 'yes' next time;"
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg + Carret
        Msg = Msg & "2.  IFs found Excel, but Excel on your machine does not "
        Msg = Msg & "expose an interface for another application (error 430) - "
        Msg = Msg & "your version of Excel may be too old; "
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg + Carret
        Msg = Msg & "3. Excel does not exist on your machine; "
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg + Carret
        Msg = Msg & "4. You have insufficient memory to start Excel (error 7); "
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg + Carret
        Msg = Msg & "5.  Error generated by Excel itself (error 440);"
        Msg = Msg & "6.  Excel is already running - Exit from it and retry."
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


QuitApplicationError:
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Msg = "Error quitting Excel File Modification"
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '==========================frm_Scatter===================
    Private Sub cmd_Print_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Print.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        ''    lblJS.DrawStyle = 1
        ''    lblJS.DrawMode = 5

        ''   Temporary Workaround DrawMode Problem
        ''   -------------------------------------

        ''    Printer.PaintPicture lblJS.Picture, Printer.Width / 4, _
        ''      Printer.Height / 4, Printer.Width / 2, Printer.Height / 2
        ''        'Suggested by Microsoft, but cuts Y-axis label
        ''    Printer.PaintPicture lblJS.Picture, Printer.Width / 5, _
        ''      Printer.Height / 4, Printer.Width / 1.5, Printer.Height / 2

        ''    Printer.EndDoc

        'lblJS.PrintStyle = GraphLib.PrintStyleConstants.gphColorWithBorder
        ''UPGRADE_ISSUE: VBControlExtender method lblJS.DrawMode was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'lblJS.DrawMode = 5
        Dim Msg As String
        Msg = "To print graph please select File Menu and Print option of the Browser."
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


    End Sub

    '==========================frm_Scatter===================
    Private Sub Cmd_Save_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Cmd_Save.Click
        '		Dim CDERR_CANCEL As Object
        '		Dim GraphOutputFileName As Object
        '		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        '		'   Get graph file
        '		'   --------------
        '		'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '		GraphOutputFileName = "*.WMF"
        '		On Error GoTo SelGraphFileError_Line
        '		cmd_FileAccess.DefaultExt = "WMF"

        '		'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '		cmd_FileAccess.Filename = GraphOutputFileName
        '		cmd_FileAccess.DialogTitle = "Save Graphic File"
        '		cmd_FileAccess.InitDir = PutFdrvRun2
        '		cmd_FileAccess.Action = 1
        '		'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '		GraphOutputFileName = cmd_FileAccess.Filename
        '		'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '		If VB.Left(GraphOutputFileName, 1) = "*" Then Exit Sub 
        '		GoTo WriteGraphFile_Line

        'SelGraphFileError_Line: 
        '		'UPGRADE_WARNING: Couldn't resolve default property of object CDERR_CANCEL. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '		If Err.Number = CDERR_CANCEL Then 
        '			'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '			WinFormsToWeb_Messagebox.show("Cancel", 0, "No File Saved")
        '			On Error GoTo 0
        '			Exit Sub 
        '		Else 
        '			'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '			WinFormsToWeb_Messagebox.show(ErrorToString(Err.Number), 0, "No File Saved")
        '			Exit Sub 
        '		End If 

        'WriteGraphFile_Line: 
        '		'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '		lblJS.ImageFile = GraphOutputFileName
        '		'UPGRADE_ISSUE: VBControlExtender method lblJS.DrawStyle was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        '		lblJS.DrawStyle = 1'0
        '		'UPGRADE_ISSUE: VBControlExtender method lblJS.DrawMode was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        '		lblJS.DrawMode = 6
        Dim Msg As String
        Msg = "To Save graph please right click on the image and select Save Picture As."
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '==========================frm_Scatter===================
    'CONVERSION_NOTE: The code of Event 'frm_Scatter.frm_Scatter_Load' was moved to Method 'frm_Scatter.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_Scatter_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo Scatter_Load_Error 'JAM 2016-11-24

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
        Dim i As Short


        'JAM 2016-12-11
        Session("AddingYear") = False

        'JAM 2010-01-24 
        If Not Page.IsPostBack Then
            'jrs 2011/12/23
            If Session("mnuNamesChecked") = Nothing Then
                Session("mnuNamesChecked") = False
                Session("mnuCodesChecked") = False
                Session("mnuNolabelsChecked") = True
            End If
            'JAM 2010-01-24 
            'If Session("mnuCodesChecked") Then
            '    cswmItem.IconImage = "checkbox.gif"
            'Else
            '    cswmItem.IconImage = "blank.gif"
            'End If

            Session("mnuPopulationChecked") = False
            Session("mnuGdpChecked") = False
            Session("mnuOtherChecked") = False
            Session("mnuNoneChecked") = False


            'JAM 2010-01-23 If a Second Independent Dimmension has been chosen
            If InStr(Session("VarNameI")(1), "NONE") <> 0 Or Session("VarNameI")(1) = "" Then
                Session("ThirdDimOk") = False
                Session("thirdimsel") = "N"
            Else
                For i = 1 To Session("CCount")
                    If Not IsDBNull(Session("CVarISec")(i)) Then
                        Session("ThirdDimOk") = True
                        Session("thirdimsel") = "O"
                        Session("mnuOtherChecked") = True
                        Exit For
                    End If
                Next i
            End If

            'JAM 2016-10-09
            If Session("TotMulGroups") = 1 Then
                Session("MulRegressTitle") = ""

                'jrs 2011/09/30
                If Session("RegressType") <> "" Then
                    If Session("RegressType") = "LIN" And Not Session("ChangeSatPoint") Then
                        RegressionPlot(1, "NONE")
                    Else
                        If Session("RegressType") = "LOG" Then
                            RegressionPlot(1, "LOG")
                        Else
                            If Session("RegressType") = "EXP" Then
                                RegressionPlot(1, "EXP")
                            Else
                                If Session("RegressType") = "POW" And Session("IsChangedNum") Then
                                    RegressionPlot(1, "POW")
                                Else
                                    If Session("RegressType") = "POLYNOMIAL(Second Degree)" Then
                                        RegressionPlot(2, "NONE")
                                    Else
                                        If Session("RegressType") = "POLYNOMIAL(Third Degree)" Then
                                            RegressionPlot(3, "NONE")
                                        Else
                                            If Session("RegressType") = "LIN" And Session("ChangeSatPoint") Then
                                                TransSatDep()
                                                RegressionPlot(1, "NONE")
                                                Call RevokeSatDep()
                                                Session("ChangeSatPoint") = False
                                                'Session("SaturationValue") = 100
                                            End If

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If
                End If

            Else  'JAM 2016-10-09 For Multiple Groups or GList Regression
                If Session("RegressType") <> "" Then
                    If Session("RegressType") = "LIN" And Not Session("ChangeSatPoint") Then
                        For i = 0 To Session("TotMulGroups") - 1
                            MulRegressPlot(1, "NONE", i)
                        Next i

                    Else
                        If Session("RegressType") = "LOG" Then
                            For i = 0 To Session("TotMulGroups") - 1
                                MulRegressPlot(1, "LOG", i)
                            Next i

                        Else
                            If Session("RegressType") = "EXP" Then
                                For i = 0 To Session("TotMulGroups") - 1
                                    MulRegressPlot(1, "EXP", i)
                                Next i
                            Else
                                If Session("RegressType") = "POW" And Session("IsChangedNum") Then
                                    For i = 0 To Session("TotMulGroups") - 1
                                        MulRegressPlot(1, "POW", i)
                                    Next i
                                Else
                                    If Session("RegressType") = "POLYNOMIAL(Second Degree)" Then
                                        For i = 0 To Session("TotMulGroups") - 1
                                            MulRegressPlot(2, "NONE", i)
                                        Next i
                                    Else
                                        If Session("RegressType") = "POLYNOMIAL(Third Degree)" Then
                                            For i = 0 To Session("TotMulGroups") - 1
                                                MulRegressPlot(3, "NONE", i)
                                            Next i
                                        Else
                                            If Session("RegressType") = "LIN" And Session("ChangeSatPoint") Then
                                                TransSatDep()
                                                For i = 0 To Session("TotMulGroups") - 1
                                                    MulRegressPlot(1, "NONE", i)
                                                Next i
                                                Call RevokeSatDep()
                                                Session("ChangeSatPoint") = False
                                                'Session("SaturationValue") = 100
                                            End If

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If
                End If



            End If 'End if TotMulGroups


            'jrs 2011/09/30
            If Session("IsaGlist") Then
                If Not Session("IsaGroup") Then
                    Call PIsaGlist()
                Else
                    Call PIsaGroup()
                End If
            End If


            'JAM 2016-08-28 If and Extra Glist is chosen
            If Session("AddGlistMode") Then

                'JAM 2017-01-08 Moved to Else
                'JAM 2016-12-11
                'If Session("TotMulGroups") = 1 Then
                '    Session("SingleDimJoinPrev")(Session("TotMulGroups")) = Session("GlistCount")
                'End If

                'JAM 2016-09-26 After GList selection
                Session("TotMulGroups") = Session("TotMulGroups") + 1

                'JAM 2016-12-11
                Session("SingleDimJoinPrev")(Session("TotMulGroups")) = Session("GlistCount")


                Call PostGlist()

            End If


        End If

        'JAM 2016-07-02 
        Session("PrevRemove") = False

        'JAM 2016-10-07
        If Not Session("MoreScatterLoading") Then

            'JAM 2016-12-11 Resetting Multiple Regression Title
            Session("MulRegressTitle") = ""

            'JAM 2016-07-10 Redefining GCount
            Session("GCount") = Session("TotCountries")

            'JAM 2016-12-11
            ReDim Session("SingleDimJoinPrev")(Session("GCount"))
            For i = 1 To Session("GCount")
                Session("SingleDimJoinPrev")(i) = 0
            Next

            'JAM 2016-12-11
            ReDim Session("FirstCVarCounName")(Session("TotCountries"))
            ReDim Session("FirstCVarCounCode")(Session("TotCountries"))
            ReDim Session("FirstOrigIndFir")(Session("TotCountries"))
            ReDim Session("FirstOrigDep")(Session("TotCountries"))
            ReDim Session("FirstOrigIndSec")(Session("TotCountries"))
            ReDim Session("FirstIPopOrig")(Session("TotCountries"))
            ReDim Session("FirstIGdpOrig")(Session("TotCountries"))
            'JAM 2016-12-11
            For LineSet% = 1 To Session("TotCountries")
                Session("FirstCVarCounName")(LineSet) = Session("CVarCounName")(LineSet)
                Session("FirstCVarCounCode")(LineSet) = Session("CVarCounCode")(LineSet)
                Session("FirstOrigIndFir")(LineSet) = Session("OrigIndFir")(LineSet)
                Session("FirstOrigDep")(LineSet) = Session("OrigDep")(LineSet)
                Session("FirstOrigIndSec")(LineSet) = Session("OrigIndSec")(LineSet)
                Session("FirstIPopOrig")(LineSet) = Session("IPopOrig")(LineSet)
                Session("FirstIGdpOrig") = Session("IGdpOrig")(LineSet)
            Next LineSet%



            'JAM 2016-07-03    Arrays for Saving Original Data for every GList selected
            If Not (Session("GCount") Is Nothing) And IsNumeric(Session("GCount")) Then 'JAM 2016-11-28 Checking is numeric
                ReDim Session("OrigPrevCVarI")(4, Session("GCount") + 1)
                ReDim Session("OrigPrevCVarISec")(4, Session("GCount") + 1)
                ReDim Session("OrigPrevCVarD")(4, Session("GCount") + 1)
                ReDim Session("OrigPrevCVarName")(4, Session("GCount") + 1)
                ReDim Session("OrigPrevCVarCode")(4, Session("GCount") + 1)
                ReDim Session("OrigPrevCVarIPop")(4, Session("GCount") + 1)
                ReDim Session("OrigPrevCVarIGdp")(4, Session("GCount") + 1)

                'JAM 2016-10-08 Dynamic RegressData
                'ReDim Session("MulRegressData")(Session("CCount"), 4)
                ReDim Session("MulRegressY")(4, Session("CCount"))
                ReDim Session("MulRegressX")(4, Session("CCount"))
                ReDim Session("RegressTitle")(4)


                'JAM 2016-10-08
                ReDim Session("MulRCountryRegress")(4, Session("GCount") + 1)
                For i = 0 To 3
                    For j = 0 To Session("GCount")
                        Session("MulRCountryRegress")(i, j) = False
                    Next j

                Next i

                'JAM 2016-10-08 Saving regression disabled in multiple regressions
                Session(" mnuSaveRegress.Enabled") = True
                'jrs 2014/09/17
                Session("mnuSaveRes.Enabled") = True



                'JAM 2017-07-10 redim Join Variable Arrays
                ReDim Session("JoinPrevCVarI")(4, Session("GCount") + 1)
                ReDim Session("JoinPrevCVarISec")(4, Session("GCount") + 1)
                ReDim Session("JoinPrevCVarD")(4, Session("GCount") + 1)
                ReDim Session("JoinPrevCVarName")(4, Session("GCount") + 1)
                ReDim Session("JoinPrevCVarCode")(4, Session("GCount") + 1)
                ReDim Session("JoinPrevCVarIPop")(4, Session("GCount") + 1)
                ReDim Session("JoinPrevCVarIGdp")(4, Session("GCount") + 1)
                ReDim Session("DimJoinPrev")(4)
                ReDim Session("GLNameJoinPrev")(4)
                ReDim Session("JoinPrevCVarAlias")(4, Session("GCount") + 1)
                'JAM 2016-07-17
                ReDim Session("GLDecompIndex")(4)

                'JAM 2016-07-17
                ReDim Session("YearsLabels")(4, 2)
                For i = 0 To 3
                    Session("DimJoinPrev")(i) = 0
                    Session("GLNameJoinPrev")(i) = ""
                    Session("GLDecompIndex")(i) = 0 'JAM 2009-07-29 Code added to decompose
                    Session("YearsLabels")(i, 0) = ""
                    Session("YearsLabels")(i, 1) = -1
                Next i

                'JAM 2016-08-02
                ReDim Session("GLbLagDep")(Session("GCount") + 1)
                ReDim Session("GLbLagInd")(Session("GCount") + 1)

                'JAM 2016-08-02
                ReDim Session("LagDepent")(Session("GCount") + 1)
                ReDim Session("LagFirInd")(Session("GCount") + 1)
                ReDim Session("LagSecInd")(Session("GCount") + 1)
                ReDim Session("LagIPop")(Session("GCount") + 1)
                ReDim Session("LagIGdp")(Session("GCount") + 1)


                'JAM 2016-08-02
                For i = 0 To UBound(Session("LagDepent"))
                    Session("LagDepent")(i) = 0
                    Session("LagFirInd")(i) = 0
                    Session("LagSecInd")(i) = 0
                    Session("LagIPop")(i) = 0
                    Session("LagIGdp")(i) = 0
                Next i


                'JAM 2016-09-24
                Session("vAddYear") = False

                'JAM 2016-08-02
                If IsNothing(Session("IsCohDep")) And IsNothing(Session("IsCoh1Ind")) And IsNothing(Session("IsCoh2Ind")) Then

                    ''mnuDynamic.Enabled = True

                    Session("LagDepChs") = Session("OrigDepYear")
                    Session("LagIndChs") = Session("OrigIndYear")
                    If Session("ThirdDimOk") Then
                        Session("LagScIChs") = Session("OrigSecIndYear")
                    End If

                    Session("FirstPrevious") = True
                    Session("StepInd") = 0
                    Session("StepDep") = 0
                    Session("StepEqual") = 0
                    Session("isLagInd") = False
                    Session("IsLagDep") = False
                    Session("IsLagEqual") = False

                    'JAM 2016-08-04
                    If IsNothing(Session("IsCohDep")) And IsNothing(Session("IsCoh1Ind")) And IsNothing(Session("IsCoh2Ind")) Then

                        ''mnuDynamic.Enabled = True
                        Session("LagDepChs") = Session("OrigDepYear")
                        Session("LagIndChs") = Session("OrigIndYear")
                        If Session("ThirdDimOk") Then
                            Session("LagScIChs") = Session("OrigSecIndYear")
                        End If
                    End If
                    If (Session("LagIndChs") > Session("LagDepChs")) Then
                        Session("isLagInd") = True

                        'JAM 2016-08-02 For MostRecent and Earliest change LaGlobal Calculate
                        If UCase(Session("LagIndChs")) = "MOSTRECENT" Then
                            If UCase(Session("LagDepChs")) = "MOSTRECENT" Then
                                Session("LaGlobal") = 0
                            Else
                                If UCase(Session("LagDepChs")) = "EARLIEST" Then
                                    Session("LaGlobal") = 1
                                Else
                                    Session("LaGlobal") = (Session("LIndYear")(Session("IndTotYears") - 3) - Session("LagDepChs")) + 2
                                End If
                            End If
                        Else
                            If UCase(Session("LagIndChs")) = "EARLIEST" Then
                                If UCase(Session("LagDepChs")) = "EARLIEST" Then
                                    Session("LaGlobal") = 0
                                Else
                                    Session("LaGlobal") = (Session("LIndYear")(Session("IndTotYears") - 3) - Session("LagDepChs")) + 1
                                End If
                            Else
                                Session("LaGlobal") = Session("LagIndChs") - Session("LagDepChs")
                            End If

                        End If




                        Session("SeqInd") = 0
                        Session("IndValue") = 0
                        Session("DepValue") = 0
                        Session("VMatch") = -1
                        For i = 0 To UBound(Session("LDepYear"))
                            If Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" Then 'JAM 2017-01-01
                                Session("VMatch") = -1
                                For j = 0 To UBound(Session("LIndYear"))
                                    If Not IsNothing(Session("LIndYear")(j)) And Session("LIndYear")(j) <> "0" Then 'JAM 2017-01-01
                                        'JAM 2016-08-02    MostRecent And Earliest require an special operation
                                        If UCase(Session("LIndYear")(j)) = "MOSTRECENT" Then
                                            Session("IndValue") = Session("LIndYear")(Session("IndTotYears") - 3) + 2
                                        Else
                                            If UCase(Session("LIndYear")(j)) = "EARLIEST" Then
                                                Session("IndValue") = Session("LIndYear")(Session("IndTotYears") - 3) + 1
                                            Else
                                                Session("IndValue") = Session("LIndYear")(j)
                                            End If
                                        End If

                                        If UCase(Session("LDepYear")(i)) = "MOSTRECENT" Then
                                            Session("DepValue") = Session("LDepYear")(Session("TotYearsLoaded") - 3) + 2
                                        Else
                                            If UCase(Session("LDepYear")(i)) = "EARLIEST" Then
                                                Session("DepValue") = Session("LDepYear")(Session("TotYearsLoaded") - 3) + 1
                                            Else
                                                Session("DepValue") = Session("LDepYear")(i)
                                            End If
                                        End If

                                        Session("VMatch") = (Session("DepValue") + Session("LaGlobal")) - Session("IndValue")

                                        'JAM 2016-08-07
                                        If IsNumeric(Session("VMatch")) And Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" And Not IsNothing(Session("LIndYear")(j)) And Session("LIndYear")(j) <> "0" Then
                                            If (Session("VMatch") = 0) Then 'And (Session("LDepYear")(i) <> 0) And (Session("LIndYear")(j) <> 0) Then 'JAM 2017-01-01
                                                Session("LagDepent")(Session("SeqInd")) = Session("LDepYear")(i)
                                                Session("LagFirInd")(Session("SeqInd")) = Session("LIndYear")(j)
                                                If Session("ThirdDimOk") Then
                                                    Session("LagSecInd")(Session("SeqInd")) = Session("LSecIndYear")(Session("SeqInd"))  'Loading variables without lag
                                                End If
                                                Session("SeqInd") = Session("SeqInd") + 1
                                                If Session("LIndYear")(j) >= Session("OrigIndYear") Then
                                                    Session("StepInd") = Session("StepInd") + 1
                                                End If

                                                Exit For
                                            End If
                                        End If
                                    End If 'JAM 2017-01-01
                                Next j
                            End If 'JAM 2017-01-01
                        Next i

                        Session("LastValue") = Session("LagFirInd")(Session("SeqInd") - 1)

                        'JAM 2016-08-02   Defining the LastValue like a numeric value
                        If Session("SeqInd") >= 3 Then  'JAM 2016-08-02 It Must be a valid numeric Last Value
                            If UCase(Session("LagFirInd")(Session("SeqInd") - 1)) = "MOSTRECENT" Then
                                Session("LastValue") = Session("LagFirInd")(Session("SeqInd") - 3)
                            Else
                                If UCase(Session("LagFirInd")(Session("SeqInd") - 1)) = "EARLIEST" Then
                                    Session("LastValue") = Session("LagFirInd")(Session("SeqInd") - 2)
                                End If
                            End If
                        Else
                            Session("LastValue") = "1900"
                        End If

                        'If SeqInd = 1 Then
                        '    mnuDynamic.Enabled = False
                        'End If
                    Else
                        If (Session("LagIndChs") < Session("LagDepChs")) Then
                            Session("IsLagDep") = True

                            'JAM 2016-08-02 For MostRecent and Earliest change LaGlobal Calculate
                            If UCase(Session("LagDepChs")) = "MOSTRECENT" Then
                                If UCase(Session("LagIndChs")) = "MOSTRECENT" Then
                                    Session("LaGlobal") = 0
                                Else
                                    If UCase(Session("LagIndChs")) = "EARLIEST" Then
                                        Session("LaGlobal") = 1
                                    Else
                                        Session("LaGlobal") = (Session("LDepYear")(Session("TotYearsLoaded") - 3) - Session("LagIndChs")) + 2
                                    End If
                                End If
                            Else
                                If UCase(Session("LagDepChs")) = "EARLIEST" Then
                                    If UCase(Session("LagDepChs")) = "EARLIEST" Then
                                        Session("LaGlobal") = 0
                                    Else
                                        Session("LaGlobal") = (Session("LDepYear")(Session("TotYearsLoaded") - 3) - Session("LagDepChs")) + 1
                                    End If
                                Else
                                    Session("LaGlobal") = Session("LagDepChs") - Session("LagIndChs")
                                End If

                            End If


                            Session("SeqDep") = 0
                            Session("IndValue") = 0
                            Session("DepValue") = 0
                            Session("VMatch") = -1
                            For i = 0 To UBound(Session("LDepYear"))
                                If Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" Then 'JAM 2017-01-01
                                    Session("VMatch") = -1
                                    For j = 0 To UBound(Session("LIndYear"))
                                        If Not IsNothing(Session("LIndYear")(j)) And Session("LIndYear")(j) <> "0" Then 'JAM 2017-01-01
                                            'JAM 2016-08-02    MostRecent And Earliest require an special operation
                                            If UCase(Session("LIndYear")(j)) = "MOSTRECENT" Then
                                                Session("IndValue") = Session("LIndYear")(Session("IndTotYears") - 3) + 2
                                            Else
                                                If UCase(Session("LIndYear")(j)) = "EARLIEST" Then
                                                    Session("IndValue") = Session("LIndYear")(Session("IndTotYears") - 3) + 1
                                                Else
                                                    Session("IndValue") = Session("LIndYear")(j)
                                                End If
                                            End If

                                            If UCase(Session("LDepYear")(i)) = "MOSTRECENT" Then
                                                Session("DepValue") = Session("LDepYear")(Session("TotYearsLoaded") - 3) + 2
                                            Else
                                                If UCase(Session("LDepYear")(i)) = "EARLIEST" Then
                                                    Session("DepValue") = Session("LDepYear")(Session("TotYearsLoaded") - 3) + 1
                                                Else
                                                    Session("DepValue") = Session("LDepYear")(i)
                                                End If
                                            End If

                                            Session("VMatch") = (Session("IndValue") + Session("LaGlobal")) - Session("DepValue")

                                            If IsNumeric(Session("VMatch")) And Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" And Not IsNothing(Session("LIndYear")(j)) And Session("LIndYear")(j) <> "0" Then
                                                If Session("VMatch") = 0 Then 'And (Session("LDepYear")(i) <> 0) And (Session("LIndYear")(j) <> 0) Then 'JAM 2017-01-01
                                                    Session("LagDepent")(Session("SeqDep")) = Session("LDepYear")(i)
                                                    Session("LagFirInd")(Session("SeqDep")) = Session("LIndYear")(j)
                                                    If Session("ThirdDimOk") Then
                                                        Session("LagSecInd")(Session("SeqDep")) = Session("LSecIndYear")(Session("SeqDep"))
                                                    End If

                                                    Session("SeqDep") = Session("SeqDep") + 1
                                                    If Session("LDepYear")(i) >= Session("OrigDepYear") Then
                                                        Session("StepDep") = Session("StepDep") + 1
                                                    End If

                                                    Exit For
                                                End If
                                            End If
                                        End If 'JAM 2017-01-01
                                    Next j
                                End If 'JAM 2017-01-01
                            Next i

                            Session("LastValue") = Session("LagDepent")(Session("SeqDep") - 1)

                            'JAM 2016-08-02   Defining the LastValue like a numeric value
                            If Session("SeqDep") >= 3 Then 'JAM 2016-08-02 It Must be a valid numeric Last Value
                                If UCase(Session("LagDepent")(Session("SeqDep") - 1)) = "MOSTRECENT" Then
                                    Session("LastValue") = Session("LagDepent")(Session("SeqDep") - 3)
                                Else
                                    If UCase(Session("LagDepent")(Session("SeqDep") - 1)) = "EARLIEST" Then
                                        Session("LastValue") = Session("LagDepent")(Session("SeqDep") - 2)
                                    End If
                                End If
                            Else
                                Session("LastValue") = "1900"
                            End If


                            'If SeqDep = 1 Then
                            '    mnuDynamic.Enabled = False
                            'End If

                        Else  ' (LagIndChs = LagDepChs)
                            Session("LaGlobal") = 0
                            Session("IsLagEqual") = True
                            Session("SeqSec") = 0
                            Session("IndValue") = 0
                            Session("DepValue") = 0
                            Session("VMatch") = -1


                            For i = 0 To UBound(Session("LDepYear"))
                                If Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" Then 'JAM 2017-01-01
                                    Session("VMatch") = -1
                                    For j = 0 To UBound(Session("LIndYear"))
                                        If Not IsNothing(Session("LIndYear")(j)) And Session("LIndYear")(j) <> "0" Then 'JAM 2017-01-01
                                            'JAM 2016-08-02  Using MostRecent and Earliest values



                                            If UCase(Session("LIndYear")(j)) <> "MOSTRECENT" And UCase(Session("LDepYear")(i)) <> "MOSTRECENT" And UCase(Session("LIndYear")(j)) <> "EARLIEST" And UCase(Session("LDepYear")(i)) <> "EARLIEST" Then
                                                Session("VMatch") = Session("LIndYear")(j) - Session("LDepYear")(i)
                                            End If

                                            If UCase(Session("LIndYear")(j)) = "MOSTRECENT" And UCase(Session("LDepYear")(i)) = "MOSTRECENT" Then
                                                Session("VMatch") = 0
                                            End If

                                            If UCase(Session("LIndYear")(j)) = "EARLIEST" And UCase(Session("LDepYear")(i)) = "EARLIEST" Then
                                                Session("VMatch") = 0
                                            End If


                                            'JAM 2016-08-07
                                            If IsNumeric(Session("VMatch")) And Not IsNothing(Session("LIndYear")(j)) And Session("LIndYear")(j) <> "0" And Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" Then
                                                If (Session("VMatch") = 0) Then 'And (Session("LIndYear")(j) <> 0) And (Session("LDepYear")(i) <> 0) Then 'JAM commented 2017-01-01 Due Mostrecent vs 0 value
                                                    Session("LagDepent")(Session("SeqSec")) = Session("LDepYear")(i)

                                                    Session("LagFirInd")(Session("SeqSec")) = Session("LIndYear")(j)

                                                    If Session("ThirdDimOk") Then
                                                        Session("LagSecInd")(Session("SeqSec")) = Session("LSecIndYear")(Session("SeqSec"))
                                                    End If
                                                    Session("SeqSec") = Session("SeqSec") + 1

                                                    If Session("LDepYear")(i) >= Session("OrigDepYear") Then
                                                        Session("StepEqual") = Session("StepEqual") + 1
                                                    End If

                                                    Exit For
                                                End If
                                            End If
                                        End If
                                    Next j 'JAM 2017-01-01 Validating is not nothing Independent Variable
                                End If 'JAM 2017-01-01 Validating is not nothing Dependent Variable
                            Next i

                            Session("LastValue") = Session("LagDepent")(Session("SeqSec") - 1) ' JAM 2016-08-02 It could be independent value too
                            'JAM 2016-08-02   Defining the LastValue like a numeric value
                            If Session("SeqSec") >= 3 Then 'JAM 2016-08-02 It Must be a valid numeric Last Value
                                If UCase(Session("LagDepent")(Session("SeqSec") - 1)) = "MOSTRECENT" Then
                                    Session("LastValue") = Session("LagDepent")(Session("SeqSec") - 3)
                                Else
                                    If UCase(Session("LagDepent")(Session("SeqSec") - 1)) = "EARLIEST" Then
                                        Session("LastValue") = Session("LagDepent")(Session("SeqSec") - 2)
                                    End If
                                End If
                            Else
                                Session("LastValue") = "1900"
                            End If

                            'jrs 2008/08/28
                            'If SeqSec = 1 Then
                            '    mnuDynamic.Enabled = False
                            'End If


                        End If



                    End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '
                    '   JAM 2016-08-02 Calculating lag between Dependent variable and Second Independent variable
                    '
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                    If Session("ThirdDimOk") Then
                        If Session("isLagInd") Then
                            Session("limitsec") = Session("SeqInd")  'StepInd + 1
                        Else
                            If Session("IsLagDep") Then
                                Session("limitsec") = Session("SeqDep") 'StepDep + 1
                            Else
                                Session("limitsec") = Session("SeqSec")  'StepEqual + 1
                            End If

                        End If

                        Session("StepIndSec") = 0
                        Session("StepDepSec") = 0
                        Session("StepEqualSec") = 0
                        Session("isLagIndSec") = False
                        Session("IsLagDepSec") = False
                        Session("IsLagEqualSec") = False

                        For i = 0 To UBound(Session("LagSecInd"))
                            Session("LagSecInd")(i) = 0
                        Next i

                        If (Session("LagScIChs") > Session("LagDepChs")) Then
                            Session("isLagIndSec") = True

                            'JAM 2016-08-02 For MostRecent and Earliest change LaGlobal Calculate
                            If UCase(Session("LagScIChs")) = "MOSTRECENT" Then
                                If UCase(Session("LagDepChs")) = "MOSTRECENT" Then
                                    Session("LaGlobalSec") = 0
                                Else
                                    If UCase(Session("LagDepChs")) = "EARLIEST" Then
                                        Session("LaGlobalSec") = 1
                                    Else
                                        Session("LaGlobalSec") = Session("LSecIndYear")(Session("SecIndTotYears") - 3) - Session("LagDepChs") + 2
                                    End If
                                End If
                            Else
                                If UCase(Session("LagScIChs")) = "EARLIEST" Then
                                    If UCase(Session("LagDepChs")) = "EARLIEST" Then
                                        Session("LaGlobalSec") = 0
                                    Else
                                        Session("LaGlobalSec") = (Session("LSecIndYear")(Session("SecIndTotYears") - 3) - Session("LagDepChs")) + 1
                                    End If
                                Else
                                    Session("LaGlobalSec") = Session("LagScIChs") - Session("LagDepChs")
                                End If

                            End If

                            Session("SeqIndSec") = 0
                            Session("IndValueSec") = 0
                            Session("DepValueSec") = 0
                            Session("VMatchSec") = -1
                            For i = 0 To UBound(Session("LagDepent"))
                                Session("VMatchSec") = -1
                                For j = 0 To UBound(Session("LSecIndYear"))
                                    'JAM 2016-08-02    MostRecent And Earliest require an special operation
                                    If UCase(Session("LSecIndYear")(j)) = "MOSTRECENT" Then
                                        Session("IndValueSec") = Session("LSecIndYear")(Session("SecIndTotYears") - 3) + 2
                                    Else
                                        If UCase(Session("LSecIndYear")(j)) = "EARLIEST" Then
                                            Session("IndValueSec") = Session("LSecIndYear")(Session("SecIndTotYears") - 3) + 1
                                        Else
                                            Session("IndValueSec") = Session("LSecIndYear")(j)
                                        End If
                                    End If

                                    If UCase(Session("LagDepent")(i)) = "MOSTRECENT" Then
                                        Session("DepValueSec") = Session("LagDepent")(Session("limitsec") - 3) + 2
                                    Else
                                        If UCase(Session("LagDepent")(i)) = "EARLIEST" Then
                                            Session("DepValueSec") = Session("LagDepent")(Session("limitsec") - 3) + 1
                                        Else
                                            Session("DepValueSec") = Session("LagDepent")(i)
                                        End If
                                    End If

                                    Session("VMatchSec") = (Session("DepValueSec") + Session("LaGlobalSec")) - Session("IndValueSec")
                                    If (Session("VMatchSec") = 0) And (Session("LagDepent")(i) <> 0) And (Session("LSecIndYear")(j) <> 0) Then
                                        Session("LagSecInd")(Session("SeqIndSec")) = Session("LSecIndYear")(j)

                                        Session("SeqIndSec") = Session("SeqIndSec") + 1
                                        If Session("LSecIndYear")(j) >= Session("OrigSecIndYear") Then
                                            Session("StepIndSec") = Session("StepIndSec") + 1
                                        End If




                                        Exit For
                                    End If
                                Next j
                            Next i

                            Session("LastValueSec") = Session("LagSecInd")(Session("SeqIndSec") - 1)

                            'JAM 2016-08-02   Defining the LastValue like a numeric value
                            If Session("SeqIndSec") >= 3 Then  'JAM 2016-08-02 It Must be a valid numeric Last Value
                                If UCase(Session("LagSecInd")(Session("SeqIndSec") - 1)) = "MOSTRECENT" Then
                                    Session("LastValueSec") = Session("LagSecInd")(Session("SeqIndSec") - 3)
                                Else
                                    If UCase(Session("LagSecInd")(Session("SeqIndSec") - 1)) = "EARLIEST" Then
                                        Session("LastValueSec") = Session("LagSecInd")(Session("SeqIndSec") - 2)
                                    End If
                                End If
                            Else
                                Session("LastValueSec") = "1900"
                            End If


                            'jrs 2008/08/28
                            'If SeqIndSec = 1 Then
                            '  mnuDynamic.Enabled = False
                            'End If


                        Else
                            If (Session("LagScIChs") < Session("LagDepChs")) Then
                                Session("IsLagDepSec") = True

                                'JAM 2016-08-02 For MostRecent and Earliest change LaGlobal Calculate
                                If UCase(Session("LagDepChs")) = "MOSTRECENT" Then
                                    If UCase(Session("LagScIChs")) = "MOSTRECENT" Then
                                        Session("LaGlobalSec") = 0
                                    Else
                                        If UCase(Session("LagScIChs")) = "EARLIEST" Then
                                            Session("LaGlobalSec") = 1
                                        Else
                                            Session("LaGlobalSec") = (Session("LDepYear")(Session("TotYearsLoaded") - 3) - Session("LagScIChs")) + 2
                                        End If
                                    End If
                                Else
                                    If UCase(Session("LagScIChs")) = "EARLIEST" Then
                                        If UCase(Session("LagDepChs")) = "EARLIEST" Then
                                            Session("LaGlobalSec") = 0
                                        Else
                                            Session("LaGlobalSec") = (Session("LDepYear")(Session("TotYearsLoaded") - 3) - Session("LagScIChs")) + 1
                                        End If
                                    Else
                                        Session("LaGlobalSec") = Session("LagDepChs") - Session("LagScIChs")
                                    End If

                                End If

                                Session("SeqDepSec") = 0
                                Session("IndValueSec") = 0
                                Session("DepValueSec") = 0
                                Session("VMatchSec") = -1
                                For i = 0 To UBound(Session("LagDepent"))
                                    Session("VMatchSec") = -1
                                    For j = 0 To UBound(Session("LSecIndYear"))
                                        'JAM 2016-08-02    MostRecent And Earliest require an special operation
                                        If UCase(Session("LSecIndYear")(j)) = "MOSTRECENT" Then
                                            Session("IndValueSec") = Session("LSecIndYear")(Session("SecIndTotYears") - 3) + 2
                                        Else
                                            If UCase(Session("LSecIndYear")(j)) = "EARLIEST" Then
                                                Session("IndValueSec") = Session("LSecIndYear")(Session("SecIndTotYears") - 3) + 1
                                            Else
                                                Session("IndValueSec") = Session("LSecIndYear")(j)
                                            End If
                                        End If

                                        If UCase(Session("LagDepent")(i)) = "MOSTRECENT" Then
                                            Session("DepValueSec") = Session("LagDepent")(Session("limitsec") - 3) + 2
                                        Else
                                            If UCase(Session("LagDepent")(i)) = "EARLIEST" Then
                                                Session("DepValueSec") = Session("LagDepent")(Session("limitsec") - 3) + 1
                                            Else
                                                Session("DepValueSec") = Session("LagDepent")(i)
                                            End If
                                        End If

                                        Session("VMatchSec") = (Session("IndValueSec") + Session("LaGlobalSec")) - Session("DepValueSec")
                                        'VMatch = (LIndYear(j) + LaGlobal) - LagDepent(i)

                                        If Session("VMatchSec") = 0 And (Session("LagDepent")(i) <> 0) And (Session("LSecIndYear")(j) <> 0) Then
                                            Session("LagSecInd")(Session("SeqDepSec")) = Session("LSecIndYear")(j)
                                            Session("SeqDepSec") = Session("SeqDepSec") + 1
                                            If Session("LagDepent")(i) >= Session("OrigDepYear") Then
                                                Session("StepDepSec") = Session("StepDepSec") + 1
                                            End If

                                            Exit For
                                        End If
                                    Next j
                                Next i

                                Session("LastValueSec") = Session("LagDepent")(Session("SeqDepSec") - 1)

                                'JAM 2016-08-02   Defining the LastValue like a numeric value
                                If Session("SeqDepSec") >= 3 Then 'JAM 2016-08-02 It Must be a valid numeric Last Value
                                    If UCase(Session("LagDepent")(Session("SeqDepSec") - 1)) = "MOSTRECENT" Then
                                        Session("LastValueSec") = Session("LagDepent")(Session("SeqDepSec") - 3)
                                    Else
                                        If UCase(Session("LagDepent")(Session("SeqDepSec") - 1)) = "EARLIEST" Then
                                            Session("LastValueSec") = Session("LagDepent")(Session("SeqDepSec") - 2)
                                        End If
                                    End If
                                Else
                                    Session("LastValueSec") = "1900"
                                End If

                                'jrs 2008/08/28
                                'If SeqDepSec = 1 Then
                                '  mnuDynamic.Enabled = False
                                'End If

                            Else

                                ' (LagScIChs = LagDepChs)
                                Session("LaGlobalSec") = 0
                                Session("IsLagEqualSec") = True
                                Session("SeqSecSec") = 0
                                Session("IndValueSec") = 0
                                Session("DepValueSec") = 0
                                Session("VMatchSec") = -1
                                For i = 0 To UBound(Session("LDepYear"))
                                    If Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" Then 'JAM 2018-04-29 Validating Numeric Values
                                        Session("VMatchSec") = -1
                                        For j = 0 To UBound(Session("LSecIndYear"))
                                            If Not IsNothing(Session("LSecIndYear")(j)) And Session("LSecIndYear")(j) <> "0" Then 'JAM 2018-04-29 Validating Numeric Values
                                                'JAM 2016-08-02  Using MostRecent and Earliest values
                                                If UCase(Session("LSecIndYear")(j)) <> "MOSTRECENT" And UCase(Session("LDepYear")(i)) <> "MOSTRECENT" And UCase(Session("LSecIndYear")(j)) <> "EARLIEST" And UCase(Session("LDepYear")(i)) <> "EARLIEST" Then
                                                    Session("VMatchSec") = Session("LSecIndYear")(j) - Session("LDepYear")(i)
                                                End If

                                                If UCase(Session("LSecIndYear")(j)) = "MOSTRECENT" And UCase(Session("LDepYear")(i)) = "MOSTRECENT" Then
                                                    Session("VMatchSec") = 0
                                                End If

                                                If UCase(Session("LSecIndYear")(j)) = "EARLIEST" And UCase(Session("LDepYear")(i)) = "EARLIEST" Then
                                                    Session("VMatchSec") = 0
                                                End If



                                                'If IsNumeric(Session("VMatch")) And Not IsNothing(Session("LIndYear")(j)) And Session("LIndYear")(j) <> "0" And Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" Then
                                                'JAM 2018-04-29 Validating Numeric Values
                                                If IsNumeric(Session("VMatchSec")) And Not IsNothing(Session("LSecIndYear")(j)) And Session("LSecIndYear")(j) <> "0" And Not IsNothing(Session("LDepYear")(i)) And Session("LDepYear")(i) <> "0" Then
                                                    If (Session("VMatchSec") = 0) Then 'JAM 2018-04-29 Validating Numeric Values
                                                        'If (Session("VMatchSec") = 0) And (Session("LSecIndYear")(j) <> 0) And (Session("LagDepent")(i) <> 0) Then
                                                        'If (Session("VMatchSec") = 0) And IsNumeric(Session("LSecIndYear")(j)) And IsNumeric(Session("LagDepent")(i)) Then
                                                        'JAM 2018-04-28 Validating only ofr numeric values
                                                        'If (Session("LSecIndYear")(j) <> 0) And (Session("LagDepent")(i) <> 0) Then
                                                        Session("LagDepent")(Session("SeqSec")) = Session("LDepYear")(i)

                                                        Session("LagSecInd")(Session("SeqSecSec")) = Session("LSecIndYear")(j)

                                                        If Session("ThirdDimOk") Then
                                                            Session("LagSecInd")(Session("SeqSec")) = Session("LSecIndYear")(Session("SeqSec"))
                                                        End If

                                                        Session("SeqSecSec") = Session("SeqSecSec") + 1

                                                        'jrs 2023/05/05 added ToString
                                                        If Session("LagDepent")(i).ToString >= Session("OrigDepYear").ToString Then
                                                            Session("StepEqualSec") = Session("StepEqualSec") + 1
                                                        End If

                                                        Exit For
                                                    End If
                                                End If
                                            End If
                                        Next j
                                    End If
                                Next i

                                Session("LastValueSec") = Session("LagDepent")(Session("SeqSecSec") - 1) ' JAM 2016-08-02 It could be independent value too
                                'JAM 2016-08-02   Defining the LastValue like a numeric value
                                If Session("SeqSecSec") >= 3 Then 'JAM 2016-08-02 It Must be a valid numeric Last Value
                                    If UCase(Session("LagDepent")(Session("SeqSecSec") - 1)) = "MOSTRECENT" Then
                                        Session("LastValueSec") = Session("LagDepent")(Session("SeqSecSec") - 3)
                                    Else
                                        If UCase(Session("LagDepent")(Session("SeqSecSec") - 1)) = "EARLIEST" Then
                                            Session("LastValueSec") = Session("LagDepent")(Session("SeqSecSec") - 2)
                                        End If
                                    End If
                                Else
                                    Session("LastValueSec") = "1900"
                                End If

                                'jrs 2008/08/28
                                'If SeqSec = 1 Then
                                '  mnuDynamic.Enabled = False
                                'End If


                            End If



                        End If


                    End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''Else
                    ''mnuDynamic.Enabled = False


                End If 'End Cohorts If


                'JAM 2016-08-02  For Cohorts Variables Years selection isn't valid
                If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
                    'JAM 2016-08-02   Loading Global Arrays for Lag
                    'jrs 2024/04/23 For i = 0 To Session("TotYearsLoaded") - 1 'UBound(LagDepent)
                    For i = 0 To Amin(UBound(Session("LagDepent")), Session("TotYearsLoaded") - 1)
                        Session("GLbLagDep")(i) = Session("LagDepent")(i)
                        Session("GLbLagInd")(i) = Session("LagFirInd")(i)
                    Next i
                Else
                    'If frm_ScatVarS.DepCohortChosen <> "" Then
                    If Session("DepCohortChosen") <> "" Then

                        Session("GLbLagDep")(0) = Session("DepCohortChosen")
                        Session("WaveCohDep") = Session("DepWaveCohChosen")
                    Else
                        Session("GLbLagDep")(0) = Session("OrigDepYear")
                    End If

                    If Session("IndCohortChosen") <> "" Then
                        Session("GLbLagInd")(0) = Session("IndCohortChosen")
                        Session("WaveCohInd") = Session("IndWaveCohChosen")
                    Else
                        Session("GLbLagInd")(0) = Session("OrigIndYear")

                    End If


                End If

                'JAM 2016-08-02
                Session("AddGlistMode") = False
                Session("TotMulGroups") = 1



            End If  ''IF GCount

        Else  'JAM 2017-01-08 Assign the first GListCount for AddGListMode array
            'JAM 2017-01-08
            If Session("IsaGlist") Then
                If Session("TotMulGroups") = 1 Then
                    Session("SingleDimJoinPrev")(Session("TotMulGroups")) = Session("GlistCount")
                End If
            End If
        End If ''IF Session("MoreScatterLoading")

        'JAM 2016-10-07
        Session("MoreScatterLoading") = True

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_4a6c"


        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("ProEssentialParent") & "';"
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("BaseRadialParent") & "';"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Main Menu"
        cswmGroup1.ID = "mnuMainMenu"
        cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Labels"
        cswmGroup1.ID = "Group_DispLabel"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Country Names"

        'JAM 2010-01-23 
        If Session("mnuNamesChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Country Codes"

        'JAM 2010-01-23 
        If Session("mnuCodesChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "No Labels"

        'JAM 2010-01-23 
        If Session("mnuNolabelsChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'JAM 2010-01-30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Axis Labels"
        cswmItem.IconImage = "blank.gif"


        'JAM 2018-09-30
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select Points and Labels"
        cswmItem.IconImage = "blank.gif"



        'JAM 2010-01-23 It'll be added in a second stage
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Select Point and Labels"

        'JAM 2010-01-30 Only If We've chosen a Second Independent Variable
        If Session("ThirdDimOk") Then

            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Bubble Diameter"
            cswmGroup1.ID = "Group_Bubble"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Other"

            'JAM 2010-01-23 
            If Session("mnuOtherChecked") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Population(MOSTRECENT)"

            'JAM 2010-01-23 
            If Session("mnuPopulationChecked") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "GDP2000(MOSTRECENT)"

            'JAM 2010-01-23 
            If Session("mnuGdpChecked") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "None"

            'JAM 2010-01-23 
            If Session("mnuNoneChecked") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'JAM 2010-03-06 GList Options

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Specify Geography"
        cswmGroup1.ID = "Group_Geography"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Countries"

        If Session("mnuCountriesChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select Geographic List"

        If Session("mnuGeographicChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If




        'JAM 2010-07-11 Adding Group Options
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select Group"

        If Session("mnuGroupChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'JAM 2016-11-22

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Add Geographic List"
        If Session("mnuGeographicChecked") Then
            cswmItem.Enabled = True
        Else
            cswmItem.Enabled = False
        End If

        'JAM 2016-04-06 Adding Extra G-List 
        If Session("mnuAddGeographicChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        'JAM 2016-09-26
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Add Group"

        If Session("mnuGroupChecked") Then
            cswmItem.Enabled = True
        Else
            cswmItem.Enabled = False

        End If


        'JAM 2016-09-26 Adding Extra Group 
        If Session("mnuAddGroupChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'JAM 2016-09-26
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Add Year"

        'JAM 2016-11-22
        If Session("mnuGeographicChecked") Or Session("mnuGroupChecked") Then
            cswmItem.Enabled = True
        Else
            cswmItem.Enabled = False
        End If

        'JAM 2016-09-26 Adding Extra Group 
        If Session("mnuAddYearChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If



        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        ''''''''''''''''''''JAM 2010-12-10  Regression Options''''''''''''''''''''

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Relationships"
        cswmGroup1.ID = "Group_Relationships"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Linear"

        If Session("mnuLinear") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Logarithmic"

        If Session("mnuLogarithmic") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Exponential"

        If Session("mnuExponential") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Power"

        If Session("mnuPower") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Polynomial Second Degree"

        If Session("mnu2nd") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Polynomial Third Degree"

        If Session("mnu3nd") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'JAM 2011-01-22 Logistic option
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Logistic"

        If Session("mnuLogistic") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "No Regression"
        cswmItem.IconImage = "blank.gif"
        'If Session("mnu3nd") Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16031);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

        'jrs 2011/09/29
        Session("TableDisplayParent") = ""
        'LoadPairColors()
        UseUnescoColors()
        'JAM 2009-04-11 GList colors for countries mode too
        'If IsaGlist Then
        '    GListColor = GListName
        '    GListColorCount = GListCount
        'Else
        'JAM 2009-05-06    Predefine GListColor
        If GListColor = "" Then
            GListColor = "UN Regions" 'jrs 2015/03/04 changed from WB Regions to UN Regions'JAM 2010-04-08 Default Color using WB Regions
        End If
        'JAM 2009-04-12 Selecting Total of regions for the current GList
        'Call RegionsbyGList(GListColor)
        'End If

        Call RecoveryColors()   'JAM 2009-04-09  Recovering Regions Colors

        PackJScritpBuilder()


        Exit Sub

Scatter_Load_Error:
        'MsgBox("Scatter Loading Erro")
        Exit Sub

    End Sub

    '=============================Scatter.Frm  =======================
    Private Sub GraphData()
        'Dim LineSet As Short
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        ''   Graph values
        ''   ------------

        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        ''CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        ''CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'For LineSet = 1 To lblJS.NumSets
        '	Support.DataGridManager.manager.SetRow(grd_Scatter, LineSet - 1)
        '	lblJS.ThisSet = LineSet
        '	lblJS.ThisPoint = 1
        '	Support.DataGridManager.manager.SetCol(grd_Scatter, 0)
        '	lblJS.XPosData = Val(Support.DataGridManager.GetText(grd_Scatter))
        '	lblJS.ThisPoint = 1
        '	Support.DataGridManager.manager.SetCol(grd_Scatter, 1)
        '	lblJS.GraphData = Val(Support.DataGridManager.GetText(grd_Scatter))
        'Next LineSet
        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        ''CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        ''CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    '=============================Scatter.Frm  =======================
    Private Function GraphScaleLabel(ByRef V As String, ByRef Rescaler As Object) As String

        On Error GoTo GraphScaleLabel_Error 'JAM 2016-11-24

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


        Exit Function

GraphScaleLabel_Error:
        'MsgBox("Error occurred while calculating " + ExtpType$ + " trend.")
        Exit Function


    End Function
    Private Sub WinFormsToWeb_ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WinFormsToWeb_ButtonClose.Click
        Support.FormManager.Close(Me)
    End Sub
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)

        On Error GoTo ItemClickHandler_Error 'JAM 2016-11-24

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        Select Case objEA.Item.Caption

            Case "Save", "Print"
            Case "Country Names"
                mnuNames_Click()
                If Session("mnuNamesChecked") Then
                    objSender.Groups("Display Labels").Items("Country Names").IconImage = "checkbox.gif"
                    objSender.Groups("Display Labels").Items("Country Codes").IconImage = "blank.gif"
                    objSender.Groups("Display Labels").Items("No Labels").IconImage = "blank.gif"
                Else
                    objSender.Groups("Display Labels").Items("Country Names").IconImage = "blank.gif"
                End If
            Case "Country Codes"
                mnuCodes_Click(sender, e)
                If Session("mnuCodesChecked") Then
                    objSender.Groups("Display Labels").Items("Country Names").IconImage = "blank.gif"
                    objSender.Groups("Display Labels").Items("Country Codes").IconImage = "checkbox.gif"
                    objSender.Groups("Display Labels").Items("No Labels").IconImage = "blank.gif"
                Else
                    objSender.Groups("Display Labels").Items("Country Codes").IconImage = "blank.gif"
                End If
            Case "No Labels"
                mnuNolabels_Click(sender, e)
                If Session("mnuNolabelsChecked") Then
                    objSender.Groups("Display Labels").Items("Country Names").IconImage = "blank.gif"
                    objSender.Groups("Display Labels").Items("Country Codes").IconImage = "blank.gif"
                    objSender.Groups("Display Labels").Items("No Labels").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Display Labels").Items("No Labels").IconImage = "blank.gif"
                End If
            Case "Axis Labels"
                mnuLabels_Click()
                    'Case "Select Point and Labels"
                    '    mnuSeLabels_Click()
            Case "Select Points and Labels"
                mnuSelPointLabel_Click()
                    'Case "Select Point and Labels"
                    '    mnuSeLabels_Click()
            Case "Other"
                mnuOther_Click()
                If Session("mnuOtherChecked") Then
                    objSender.Groups("Bubble Diameter").Items("Other").IconImage = "checkbox.gif"
                    objSender.Groups("Bubble Diameter").Items("Population(MOSTRECENT)").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("GDP2000(MOSTRECENT)").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("None").IconImage = "blank.gif"
                Else
                    objSender.Groups("Bubble Diameter").Items("Other").IconImage = "blank.gif"
                End If
            Case "Population(MOSTRECENT)"
                mnuPopulation_Click()
                If Session("mnuPopulationChecked") Then
                    objSender.Groups("Bubble Diameter").Items("Other").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("Population(MOSTRECENT)").IconImage = "checkbox.gif"
                    objSender.Groups("Bubble Diameter").Items("GDP2000(MOSTRECENT)").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("None").IconImage = "blank.gif"
                Else
                    objSender.Groups("Bubble Diameter").Items("Population(MOSTRECENT)").IconImage = "blank.gif"
                End If
            Case "GDP2000(MOSTRECENT)"
                mnuGdp_Click()
                If Session("mnuGdpChecked") Then
                    objSender.Groups("Bubble Diameter").Items("Other").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("Population(MOSTRECENT)").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("GDP2000(MOSTRECENT)").IconImage = "checkbox.gif"
                    objSender.Groups("Bubble Diameter").Items("None").IconImage = "blank.gif"
                Else
                    objSender.Groups("Bubble Diameter").Items("GDP2000(MOSTRECENT)").IconImage = "blank.gif"
                End If
            Case "None"
                mnuNone_Click()
                If Session("mnuNoneChecked") Then
                    objSender.Groups("Bubble Diameter").Items("Other").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("Population(MOSTRECENT)").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("GDP2000(MOSTRECENT)").IconImage = "blank.gif"
                    objSender.Groups("Bubble Diameter").Items("None").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Bubble Diameter").Items("None").IconImage = "blank.gif"
                End If
            Case "Countries"        'JAM 2010-03-06
                'JAM 2011-02-01
                objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("No Regression").IconImage = "blank.gif"
                Session("mnuLinear") = False
                Session("mnuLogarithmic") = False
                Session("mnuExponential") = False
                Session("mnuPower") = False
                Session("mnu2nd") = False
                Session("mnu3nd") = False
                Session("mnuLogistic") = False

                'JAM 2016-11-22
                objSender.Groups("Specify Geography").Items("Add Geographic List").Enabled = False
                objSender.Groups("Specify Geography").Items("Add Group").Enabled = False
                objSender.Groups("Specify Geography").Items("Add Year").Enabled = False

                mnuCountries_Click()
                If Session("mnuCountriesChecked") Then
                    objSender.Groups("Specify Geography").Items("Countries").IconImage = "checkbox.gif"
                    objSender.Groups("Specify Geography").Items("Select Group").IconImage = "blank.gif"
                    objSender.Groups("Specify Geography").Items("Select Geographic List").IconImage = "blank.gif"
                Else
                    objSender.Groups("Specify Geography").Items("Countries").IconImage = "blank.gif"
                End If

            Case "Select Geographic List"   'JAM 2010-03-06
                'JAM 2011-02-01
                objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("No Regression").IconImage = "blank.gif"
                Session("mnuLinear") = False
                Session("mnuLogarithmic") = False
                Session("mnuExponential") = False
                Session("mnuPower") = False
                Session("mnu2nd") = False
                Session("mnu3nd") = False
                Session("mnuLogistic") = False
                'JAM 2016-10-08
                Session("AddGlistMode") = False

                mnuGeographic_Click()
                If Session("mnuGeographicChecked") Then
                    objSender.Groups("Specify Geography").Items("Countries").IconImage = "blank.gif"
                    objSender.Groups("Specify Geography").Items("Select Group").IconImage = "blank.gif"
                    objSender.Groups("Specify Geography").Items("Select Geographic List").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Specify Geography").Items("Select Geographic List").IconImage = "blank.gif"
                End If



            Case "Select Group"   'JAM 2010-03-06
                'JAM 2011-02-01
                objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("No Regression").IconImage = "blank.gif"
                Session("mnuLinear") = False
                Session("mnuLogarithmic") = False
                Session("mnuExponential") = False
                Session("mnuPower") = False
                Session("mnu2nd") = False
                Session("mnu3nd") = False
                Session("mnuLogistic") = False
                'JAM 2016-10-08
                Session("AddGlistMode") = False
                mnuGroup_Click()
                If Session("mnuGroupChecked") Then
                    objSender.Groups("Specify Geography").Items("Countries").IconImage = "blank.gif"
                    objSender.Groups("Specify Geography").Items("Select Geographic List").IconImage = "blank.gif"
                    objSender.Groups("Specify Geography").Items("Select Group").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Specify Geography").Items("Select Group").IconImage = "blank.gif"
                End If

            Case "Add Geographic List"   'JAM 2016-04-06

                'JAM 20167-01-30
                objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("No Regression").IconImage = "blank.gif"
                Session("mnuLinear") = False
                Session("mnuLogarithmic") = False
                Session("mnuExponential") = False
                Session("mnuPower") = False
                Session("mnu2nd") = False
                Session("mnu3nd") = False
                Session("mnuLogistic") = False



                mnuAddGList_Click()



                If Session("mnuAddGeographicChecked") Then
                    ''objSender.Groups("Specify Geography").Items("Countries").IconImage = "blank.gif"
                    ''objSender.Groups("Specify Geography").Items("Select Group").IconImage = "blank.gif"
                    objSender.Groups("Specify Geography").Items("Add Geographic List").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Specify Geography").Items("Add Geographic List").IconImage = "blank.gif"
                End If




            Case "Add Group"   'JAM 2016-09-26

                'JAM 20167-01-30
                objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("No Regression").IconImage = "blank.gif"
                Session("mnuLinear") = False
                Session("mnuLogarithmic") = False
                Session("mnuExponential") = False
                Session("mnuPower") = False
                Session("mnu2nd") = False
                Session("mnu3nd") = False
                Session("mnuLogistic") = False



                mnuAddGrp_Click()
                If Session("mnuAddGroupChecked") Then
                    ''objSender.Groups("Specify Geography").Items("Countries").IconImage = "blank.gif"
                    ''objSender.Groups("Specify Geography").Items("Select Group").IconImage = "blank.gif"
                    objSender.Groups("Specify Geography").Items("Add Group").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Specify Geography").Items("Add Group").IconImage = "blank.gif"
                End If





            Case "Add Year"   'JAM 2016-09-26

                'JAM 20167-01-30
                objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("No Regression").IconImage = "blank.gif"
                Session("mnuLinear") = False
                Session("mnuLogarithmic") = False
                Session("mnuExponential") = False
                Session("mnuPower") = False
                Session("mnu2nd") = False
                Session("mnu3nd") = False
                Session("mnuLogistic") = False




                mnuAddYear_Click()
                If Session("mnuAddYearChecked") Then
                    ''objSender.Groups("Specify Geography").Items("Countries").IconImage = "blank.gif"
                    ''objSender.Groups("Specify Geography").Items("Select Group").IconImage = "blank.gif"
                    objSender.Groups("Specify Geography").Items("Add Year").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Specify Geography").Items("Add Year").IconImage = "blank.gif"
                End If


            Case "Linear"   'JAM 2010-12-11
                mnuLinear_Click()
                If Session("mnuLinear") Then
                    objSender.Groups("Relationships").Items("Linear").IconImage = "checkbox.gif"
                    objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                Else
                    objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                End If
            Case "Logarithmic"   'JAM 2010-12-11
                mnuLogarit_Click()
                If Session("mnuLogarithmic") Then
                    objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logarithmic").IconImage = "checkbox.gif"
                    objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                Else
                    objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                End If
            Case "Exponential"   'JAM 2010-12-11
                mnuExpon_Click()
                If Session("mnuExponential") Then
                    objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Exponential").IconImage = "checkbox.gif"
                    objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                Else
                    objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                End If
            Case "Power"   'JAM 2010-12-11
                mnuPower_Click()
                If Session("mnuPower") Then
                    objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Power").IconImage = "checkbox.gif"
                    objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                Else
                    objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                End If
            Case "Polynomial Second Degree"   'JAM 2010-12-11
                mnu2nd_Click()
                If Session("mnu2nd") Then
                    objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "checkbox.gif"
                    objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                Else
                    objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                End If
            Case "Polynomial Third Degree"   'JAM 2010-12-11
                mnu3rd_Click()
                If Session("mnu3nd") Then
                    objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "checkbox.gif"
                    objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                Else
                    objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                End If
            Case "Logistic"   'JAM 2011-01-22
                mnuLogistic_Click()
                If Session("mnuLogistic") Then
                    objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                    objSender.Groups("Relationships").Items("Logistic").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                End If
            Case "No Regression"   'JAM 2010-12-11
                mnuNoRegress_Click()
                objSender.Groups("Relationships").Items("Linear").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logarithmic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Exponential").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Power").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Second Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Polynomial Third Degree").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("Logistic").IconImage = "blank.gif"
                objSender.Groups("Relationships").Items("No Regression").IconImage = "blank.gif"
            Case "Continue"
                cmd_Exit_Click(Me, New EventArgs)
                'mnuContinue_Click(Me, New EventArgs)
        End Select

        'JAM 2016-10-10
        If Session("TotMulGroups") = 1 Then
            'JAM 2016-12-11 Resetting Multiple Regression Title
            Session("MulRegressTitle") = ""
            'jrs 2011/09/30
            If Session("RegressType") <> "" Then
                If Session("RegressType") = "LIN" And Not Session("ChangeSatPoint") Then
                    RegressionPlot(1, "NONE")
                Else
                    If Session("RegressType") = "LOG" Then
                        RegressionPlot(1, "LOG")
                    Else
                        If Session("RegressType") = "EXP" Then
                            RegressionPlot(1, "EXP")
                        Else
                            If Session("RegressType") = "POW" Then 'jrs 2017/10/23 And Session("IsChangedNum") Then
                                RegressionPlot(1, "POW")
                            Else
                                If Session("RegressType") = "POLYNOMIAL(Second Degree)" Then
                                    RegressionPlot(2, "NONE")
                                Else
                                    If Session("RegressType") = "POLYNOMIAL(Third Degree)" Then
                                        RegressionPlot(3, "NONE")
                                    Else
                                        If Session("RegressType") = "LIN" And Session("ChangeSatPoint") Then
                                            TransSatDep()
                                            RegressionPlot(1, "NONE")
                                            Call RevokeSatDep()
                                            Session("ChangeSatPoint") = False
                                            'Session("SaturationValue") = 100
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                End If
                PackJScritpBuilder()
            End If
        Else  'JAM 2016-10-09 For Multiple Groups or GList Regression
            If Session("RegressType") <> "" Then
                If Session("RegressType") = "LIN" And Not Session("ChangeSatPoint") Then
                    For i = 0 To Session("TotMulGroups") - 1
                        MulRegressPlot(1, "NONE", i)
                    Next i

                Else
                    If Session("RegressType") = "LOG" Then
                        For i = 0 To Session("TotMulGroups") - 1
                            MulRegressPlot(1, "LOG", i)
                        Next i

                    Else
                        If Session("RegressType") = "EXP" Then
                            For i = 0 To Session("TotMulGroups") - 1
                                MulRegressPlot(1, "EXP", i)
                            Next i
                        Else
                            If Session("RegressType") = "POW" And Session("IsChangedNum") Then
                                For i = 0 To Session("TotMulGroups") - 1
                                    MulRegressPlot(1, "POW", i)
                                Next i
                            Else
                                If Session("RegressType") = "POLYNOMIAL(Second Degree)" Then
                                    For i = 0 To Session("TotMulGroups") - 1
                                        MulRegressPlot(2, "NONE", i)
                                    Next i
                                Else
                                    If Session("RegressType") = "POLYNOMIAL(Third Degree)" Then
                                        For i = 0 To Session("TotMulGroups") - 1
                                            MulRegressPlot(3, "NONE", i)
                                        Next i
                                    Else
                                        If Session("RegressType") = "LIN" And Session("ChangeSatPoint") Then
                                            TransSatDep()
                                            For i = 0 To Session("TotMulGroups") - 1
                                                MulRegressPlot(1, "NONE", i)
                                            Next i
                                            Call RevokeSatDep()
                                            Session("ChangeSatPoint") = False
                                            'Session("SaturationValue") = 100
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                End If
            End If 'End if TotMulGroups
            PackJScritpBuilder()
        End If

        Exit Sub

ItemClickHandler_Error:
        'MsgBox("Error in ItemClickHandler Procedure")
        Exit Sub


    End Sub
    'JAM 2010-01-23 Choosing Names Legends for Scatter Graph Labels
    Private Sub mnuNames_Click()
        On Error GoTo mnuNames_Error 'JAM 2016-11-24

        Session("mnuNamesChecked") = True
        Session("mnuCodesChecked") = False
        Session("mnuNolabelsChecked") = False

        'jrs 2023/05/05
        If Session("PivotShowLabels") = Nothing Then
            For i = 1 To Session("TotCountries")
                Session("ShowLabels")(i) = True
            Next
        End If

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Session("PE_CommonMenu") = "Display"
        Exit Sub

mnuNames_Error:
        'MsgBox("Error in showing point labels")
        Exit Sub

    End Sub
    'JAM 2010-01-23 Choosing No Legends for Scatter Graph Labels
    Private Sub mnuCodes_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        On Error GoTo mnuCodes_Error 'JAM 2016-11-24
        Session("mnuNamesChecked") = False
        Session("mnuCodesChecked") = True
        Session("mnuNolabelsChecked") = False

        'jrs 2023/05/05
        If Session("PivotShowLabels") = Nothing Then
            For i = 1 To Session("TotCountries")
                Session("ShowLabels")(i) = True
            Next
        End If

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnuCodes_Error:
        'MsgBox("Error in showing point labels")
        Exit Sub
    End Sub
    'JAM 2010-01-23 Choosing No Legends for Scatter Graph Labels
    Private Sub mnuNolabels_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        On Error GoTo mnuNolabels_Error 'JAM 2016-11-24
        Session("mnuNamesChecked") = False
        Session("mnuCodesChecked") = False
        Session("mnuNolabelsChecked") = True

        'jrs 2023/05/05
        For i = 1 To Session("TotCountries")
            Session("ShowLabels")(i) = False
        Next

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnuNolabels_Error:
        'MsgBox("Error in no showing point labels")
        Exit Sub

    End Sub
    'JAM 2010-01-23 Second Independent Variable is showed
    Private Sub mnuOther_Click()

        On Error GoTo mnuOther_Error 'JAM 2016-11-24
        Session("mnuPopulationChecked") = False
        Session("mnuGdpChecked") = False
        Session("mnuOtherChecked") = True
        Session("mnuNoneChecked") = False

        Session("ThirdDimOk") = True
        Session("thirdimsel") = "O"

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnuOther_Error:
        'MsgBox("Error in mnuOther_Click Routine")
        Exit Sub
    End Sub
    'JAM 2010-01-23 Population Second Independent Variable is showed
    Private Sub mnuPopulation_Click()
        On Error GoTo mnuPopulation_Error 'JAM 2016-11-24
        Session("mnuPopulationChecked") = True
        Session("mnuGdpChecked") = False
        Session("mnuOtherChecked") = False
        Session("mnuNoneChecked") = False

        Session("ThirdDimOk") = True
        Session("thirdimsel") = "P"

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub
mnuPopulation_Error:
        'MsgBox("Error in mnuPopulation_Click Routine")
        Exit Sub

    End Sub
    'JAM 2010-01-23 Gdp Second Independent Variable is showed
    Private Sub mnuGdp_Click()
        On Error GoTo mnuGdp_Error 'JAM 2016-11-24
        Session("mnuPopulationChecked") = False
        Session("mnuGdpChecked") = True
        Session("mnuOtherChecked") = False
        Session("mnuNoneChecked") = False

        Session("ThirdDimOk") = True
        Session("thirdimsel") = "G"

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub
mnuGdp_Error:
        'MsgBox("Error in mnuPopulation_Click Routine")
        Exit Sub

    End Sub
    'JAM 2010-01-23 None Second Independent Variable is showed
    Private Sub mnuNone_Click()
        On Error GoTo mnuNone_Error 'JAM 2016-11-24


        Session("mnuPopulationChecked") = False
        Session("mnuGdpChecked") = False
        Session("mnuOtherChecked") = False
        Session("mnuNoneChecked") = True

        Session("ThirdDimOk") = True 'jrs 2011/09/30 false
        Session("thirdimsel") = "N"

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub
mnuNone_Error:
        'MsgBox("Error in mnuNone_Click Routine")
        Exit Sub

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2010-01-30      Labels Definition
    '
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuLabels_Click()
        On Error GoTo mnuLabels_Error 'JAM 2016-11-24
        If Session("ThirdDimOk") Then
            If Session("mnuOtherChecked") Then
                Session("SubTitleLabel") = "Using " + Session("VarNameI")(1) + " as diameter for bubbles"
            Else
                If Session("mnuPopulationChecked") Then
                    Session("SubTitleLabel") = "Using Population(MOSTRECENT) as diameter for bubbles"
                Else
                    If Session("mnuGdpChecked") Then
                        Session("SubTitleLabel") = "Using GDP2000(MOSTRECENT) as diameter for bubbles"
                    Else
                        Session("SubTitleLabel") = ""

                    End If

                End If


            End If
        End If


        Session("AxisLabelsParent") = "Scatter.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScatterAxisLabels.aspx")

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub
mnuLabels_Error:
        'MsgBox("Error in mnuNone_Click Routine")
        Exit Sub

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2018-09-30      Choosing Points and Lables
    '
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuSelPointLabel_Click()
        On Error GoTo mnuChooseLabels_Error

        Session("ChooseLabelsParent") = "Scatter.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScatterChooseLabels.aspx")

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub
mnuChooseLabels_Error:
        'MsgBox("Error in mnuNone_Click Routine")
        Exit Sub

    End Sub



    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2010-03-06 GList Option Definition       
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuGeographic_Click()
        On Error GoTo mnuGeographic_Error 'JAM 2016-11-24

        ''JAM 2009-09-26 Disabling Showing trends
        'ShowTrends = False
        'mnuShowTrends.Enabled = False
        'mnuRemoveG.Enabled = False

        ''JAM 2009-09-26 Enabling Colors Definition Options
        'mnuGroupDefColors.Enabled = True
        'mnuLoadColors.Enabled = True

        ''JAM 2009-08-26 Resetting Logit Mode
        'ChangeSatPoint = False
        'SaturationValue = 100

        'mnuEqTurn.Checked = False
        'mnuEqTurn.Enabled = False

        'mnuHotSpot.Enabled = True
        'frm_Scatter.mnuCodes.Enabled = False

        'mnuUnderlying.Visible = True
        'mnuUnderlying.Checked = True

        ''JAM 2009-08-14 Saving regression disabled in multiple regressions
        'mnuSaveRegress.Enabled = True

        'PrevRemove = False


        ''JAM 2009-08-14  Resetting whole values, changing max limit
        'For i% = 1 To TotCountries 'CCount
        '    CountryRegress(i%) = False
        'Next i%


        'JAM 2011-02-01 Resetting Regression
        Session("RegressType") = ""
        Session("TableTitle") = ""

        'JAM 2010-03-18
        Session("IsaGroup") = False
        'JAM 2011-01-11
        Session("mnuCountriesChecked") = False
        Session("mnuGeographicChecked") = True
        Session("mnuGroupChecked") = False


        'JAM 2008-07-23 Changing G-List Selection Window Caption
        'frm_ScatVarGlist.Caption = "Select G-List"


        'JAM 2016-09-24
        Session("vAddYear") = False





        Session("ScatVarGListParent") = "Scatter.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScatVarGList.aspx")


        'JAM 2010-04-18     Reloading Scatter for GList
        lblJS.Enabled = True
        lblJS.Visible = True



        'lblJS.Enabled = True
        'lblJS.Visible = True

        Exit Sub

mnuGeographic_Error:
        'MsgBox("Error mnuGeographic_click() routine")
        Exit Sub





    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2010-07-11 Group Option Definition       
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuGroup_Click()

        On Error GoTo mnuGroup_Error 'JAM 2016-11-24

        'JAM 2011-02-01 Resetting Regression
        Session("RegressType") = ""
        Session("TableTitle") = ""

        'JAM 2016-12-11 Resetting Multiple Regression Title
        Session("MulRegressTitle") = ""

        Session("IsaGroup") = True
        'JAM 2011-01-11
        Session("mnuCountriesChecked") = False
        Session("mnuGeographicChecked") = False
        Session("mnuGroupChecked") = True

        'JAM 2016-09-24
        Session("vAddYear") = False

        Session("ScatVarGListParent") = "Scatter.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScatVarGList.aspx")
        'JAM 2010-07-18
        'If Session("IsaGlist") And Not Session("IsaGroup") Then
        '    Session("mnuGroupChecked") = False
        '    Session("mnuGeographicChecked") = True
        '    Session("mnuCountriesChecked") = False
        'Else
        '    If Session("IsaGroup") Then
        '        Session("mnuGroupChecked") = True
        '        Session("mnuGeographicChecked") = False
        '        Session("mnuCountriesChecked") = False
        '    Else
        '        Session("mnuGroupChecked") = False
        '        Session("mnuGeographicChecked") = False
        '        Session("mnuCountriesChecked") = True
        '    End If


        'End If



        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnuGroup_Error:
        'MsgBox("Error mnuGroup_click() routine")
        Exit Sub

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2010-07-18 Changing to Countries Option Definition       
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuCountries_Click()
        On Error GoTo mnuCountries_Error


        Dim LineSet As Integer

        'JAM 2011-02-01 Resetting Regression
        Session("RegressType") = ""
        Session("TableTitle") = ""

        'JAM 2016-12-11 Resetting Groups/GList
        Session("TotMulGroups") = 1


        'JAM 2016-12-11 Resetting Multiple Regression Title
        Session("MulRegressTitle") = ""

        Session("IsaGlist") = False
        Session("IsaGroup") = False

        'JAM 2016-10-07
        Session("AddGlistMode") = False

        'JAM 2011-01-11
        Session("mnuCountriesChecked") = True
        Session("mnuGeographicChecked") = False
        Session("mnuGroupChecked") = False

        Session("CCount") = Session("TotCountries")

        ''For LineSet% = 1 To Session("TotCountries")
        ''    Session("CVarName")(LineSet) = Session("CVarCounName")(LineSet)
        ''    Session("CVarCode")(LineSet) = Session("CVarCounCode")(LineSet)
        ''    Session("CVarI")(LineSet) = Session("OrigIndFir")(LineSet)
        ''    Session("CVarD")(LineSet) = Session("OrigDep")(LineSet)
        ''    Session("CVarISec")(LineSet) = Session("OrigIndSec")(LineSet)
        ''    Session("CVarIPop")(LineSet) = Session("IPopOrig")(LineSet)
        ''    Session("CVarIGdp")(LineSet) = Session("IGdpOrig")(LineSet)
        ''Next LineSet%

        'JAM 2016-12-11 Resetting to original loading values
        For LineSet% = 1 To Session("TotCountries")
            Session("CVarName")(LineSet) = Session("FirstCVarCounName")(LineSet)
            Session("CVarCode")(LineSet) = Session("FirstCVarCounCode")(LineSet)
            Session("CVarI")(LineSet) = Session("FirstOrigIndFir")(LineSet)
            Session("CVarD")(LineSet) = Session("FirstOrigDep")(LineSet)
            Session("CVarISec")(LineSet) = Session("FirstOrigIndSec")(LineSet)
            Session("CVarIPop")(LineSet) = Session("FirstIPopOrig")(LineSet)
            Session("CVarIGdp")(LineSet) = Session("FirstIGdpOrig")
        Next LineSet%


        'JAM 2016-10-24
        Session("vAddYear") = False

        'JAM 2010-07-18
        'If Session("IsaGlist") And Not Session("IsaGroup") Then
        '    Session("mnuGroupChecked") = False
        '    Session("mnuGeographicChecked") = True
        '    Session("mnuCountriesChecked") = False
        'Else
        '    If Session("IsaGroup") Then
        '        Session("mnuGroupChecked") = True
        '        Session("mnuGeographicChecked") = False
        '        Session("mnuCountriesChecked") = False
        '    Else
        '        Session("mnuGroupChecked") = False
        '        Session("mnuGeographicChecked") = False
        '        Session("mnuCountriesChecked") = True
        '    End If


        'End If

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnuCountries_Error:
        'MsgBox("Error mnuCountries_click() routine")
        Exit Sub



    End Sub
    'JAM 2010-12-11 Linear Regression
    Private Sub mnuLinear_Click()
        On Error GoTo mnuLinear_Error 'JAM 2016-11-24

        Session("mnuLinear") = True
        Session("mnuLogarithmic") = False
        Session("mnuExponential") = False
        Session("mnuPower") = False
        Session("mnu2nd") = False
        Session("mnu3nd") = False
        Session("mnuLogistic") = False

        'JAM 2010-12-11 Resetting Logit Mode
        Session("ChangeSatPoint") = False
        Session("SaturationValue") = 100

        If Session("mnuUnderlyingVisible") Then
            If Session("mnuUnderlyingChecked") Then
                Session("WholeCountries") = True

            Else
                Session("WholeCountries") = False
            End If
        Else
            Session("WholeCountries") = False
        End If

        'JAM 2008-08-27

        '

        Session("mnuEqTurnChecked") = False
        Session("mnuEqTurnEnabled") = False
        Session("RegressType") = "LIN"
        ''JAM 2009-08-04  Conditional "If" used for Multiple Regressions mode
        'If TotMulGroups = 1 Then
        '    RegressionPlot(1, "NONE")
        'Else
        '    For i = 0 To TotMulGroups - 1
        '        MulRegressPlot(1, "NONE", i)
        '    Next i

        '    'JAM 2009-09-26 Enabling Showing trends
        '    ShowTrends = True
        '    mnuShowTrends.Enabled = True
        'End If

        'mnuEqTurn.Enabled = True

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnuLinear_Error:
        'MsgBox("Error mnuLinear_click() routine")
        Exit Sub

    End Sub
    'JAM 2010-12-11 Logarit Regression
    Private Sub mnuLogarit_Click()

        On Error GoTo mnuLogarit_Error 'JAM 2016-11-24

        Session("mnuLinear") = False
        Session("mnuLogarithmic") = True
        Session("mnuExponential") = False
        Session("mnuPower") = False
        Session("mnu2nd") = False
        Session("mnu3nd") = False
        Session("mnuLogistic") = False

        'JAM 2010-12-11 Resetting Logit Mode
        Session("ChangeSatPoint") = False
        Session("SaturationValue") = 100

        If Session("mnuUnderlyingVisible") Then
            If Session("mnuUnderlyingChecked") Then
                Session("WholeCountries") = True
            Else
                Session("WholeCountries") = False
            End If
        Else
            Session("WholeCountries") = False
        End If


        Session("mnuEqTurnChecked") = False
        Session("mnuEqTurnEnabled") = False
        Session("RegressType") = "LOG"

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnuLogarit_Error:
        ' MsgBox("Error mnuLogarit_click() routine")
        Exit Sub

    End Sub
    'JAM 2010-12-11 Expon Regression
    Private Sub mnuExpon_Click()

        On Error GoTo mnuExpon_Error 'JAM 2016-11-24

        Dim i As Integer
        If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then

            'JAM  Changing Max Limit
            For i = 1 To Session("TotCountries")   'Ccount

                If IsNumeric(Session("OrigIndFir")(i)) And Session("CountryRegress")(i) Then
                    If Session("OrigIndFir")(i) > 709 Then
                        'msg$ = "Please Re-scale First Independent Variable, It exceeds the max value(709) for an exponent formulation"
                        Session("ScatterExponLimit") = "Scatter.aspx"
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_ScatterExponLimit.aspx")

                    End If
                End If
            Next i
        Else
            For i = 1 To Session("CCount")

                If IsNumeric(Session("CVarI")(i)) Then
                    If Session("CVarI")(i) > 709 Then
                        Session("ScatterExponLimit") = "Scatter.aspx"
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_ScatterExponLimit.aspx")
                    End If
                End If
            Next i

        End If


        Session("mnuLinear") = False
        Session("mnuLogarithmic") = False
        Session("mnuExponential") = True
        Session("mnuPower") = False
        Session("mnu2nd") = False
        Session("mnu3nd") = False
        Session("mnuLogistic") = False

        'JAM 2010-12-11 Resetting Logit Mode
        Session("ChangeSatPoint") = False
        Session("SaturationValue") = 100

        If Session("mnuUnderlyingVisible") Then
            If Session("mnuUnderlyingChecked") Then
                Session("WholeCountries") = True
            Else
                Session("WholeCountries") = False
            End If
        Else
            Session("WholeCountries") = False
        End If




        Session("mnuEqTurnChecked") = False
        Session("mnuEqTurnEnabled") = False
        Session("RegressType") = "EXP"

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnuExpon_Error:
        'MsgBox("Error mnuExpon_click() routine")
        Exit Sub


    End Sub
    'JAM 2010-12-11 Power Regression
    Private Sub mnuPower_Click()

        On Error GoTo mnuPower_Error 'JAM 2016-11-24
        Session("mnuLinear") = False
        Session("mnuLogarithmic") = False
        Session("mnuExponential") = False
        Session("mnuPower") = True
        Session("mnu2nd") = False
        Session("mnu3nd") = False
        Session("mnuLogistic") = False

        'JAM 2010-12-11 Resetting Logit Mode
        Session("ChangeSatPoint") = False
        Session("SaturationValue") = 100

        'PowerDegree$ = InputBox("For Formulation (aX^b), Enter a value for b. You  can use Excel to determine b", , 1)
        'If PowerDegree$ = "" Then
        '    PowerDegree$ = 1
        'End If

        If Session("mnuUnderlyingVisible") Then
            If Session("mnuUnderlyingChecked") Then
                Session("WholeCountries") = True
            Else
                Session("WholeCountries") = False
            End If
        Else
            Session("WholeCountries") = False
        End If


        Session("mnuEqTurnChecked") = False
        Session("mnuEqTurnEnabled") = False
        Session("RegressType") = "POW"

        'jrs 2017/10/23
        'Session("ScatterPower") = "Scatter.aspx"
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_ScatterPower.aspx")

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True


        'lblJS.Enabled = True
        'lblJS.Visible = True

mnuPower_Error:
        'MsgBox("Error mnuPower_click() routine")
        Exit Sub


    End Sub
    'JAM 2010-12-11 Second Grade Regression
    Private Sub mnu2nd_Click()

        On Error GoTo mnu2nd_Error 'JAM 2016-11-24


        Session("mnuLinear") = False
        Session("mnuLogarithmic") = False
        Session("mnuExponential") = False
        Session("mnuPower") = False
        Session("mnu2nd") = True
        Session("mnu3nd") = False
        Session("mnuLogistic") = False

        'JAM 2010-12-11 Resetting Logit Mode
        Session("ChangeSatPoint") = False
        Session("SaturationValue") = 100


        If Session("mnuUnderlyingVisible") Then
            If Session("mnuUnderlyingChecked") Then
                Session("WholeCountries") = True
            Else
                Session("WholeCountries") = False
            End If
        Else
            Session("WholeCountries") = False
        End If


        Session("mnuEqTurnChecked") = False
        Session("mnuEqTurnEnabled") = False
        Session("RegressType") = "POLYNOMIAL(Second Degree)"

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub

mnu2nd_Error:
        'MsgBox("Error mnu2nd_click() routine")
        Exit Sub


    End Sub
    'JAM 2010-12-11 Third Grade Regression
    Private Sub mnu3rd_Click()

        On Error GoTo mnu3rd_Error 'JAM 2016-11-24
        Session("mnuLinear") = False
        Session("mnuLogarithmic") = False
        Session("mnuExponential") = False
        Session("mnuPower") = False
        Session("mnu2nd") = False
        Session("mnu3nd") = True
        Session("mnuLogistic") = False

        'JAM 2010-12-11 Resetting Logit Mode
        Session("ChangeSatPoint") = False
        Session("SaturationValue") = 100


        If Session("mnuUnderlyingVisible") Then
            If Session("mnuUnderlyingChecked") Then
                Session("WholeCountries") = True
            Else
                Session("WholeCountries") = False
            End If
        Else
            Session("WholeCountries") = False
        End If


        Session("mnuEqTurnChecked") = False
        Session("mnuEqTurnEnabled") = False
        Session("RegressType") = "POLYNOMIAL(Third Degree)"

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub
mnu3rd_Error:
        ' MsgBox("Error mnu3rd_click() routine")
        Exit Sub


    End Sub
    Private Sub mnuNoRegress_Click()

        On Error GoTo mnuNoRegress_Error 'JAM 2016-11-24

        Session("mnuLinear") = False
        Session("mnuLogarithmic") = False
        Session("mnuExponential") = False
        Session("mnuPower") = False
        Session("mnu2nd") = False
        Session("mnu3nd") = False
        Session("mnuLogistic") = False


        Session("ChangeSatPoint") = False
        Session("SaturationValue") = 100
        Session("RegressType") = ""

        'JAM 2016-12-11 Resetting Multiple Regression Title
        Session("MulRegressTitle") = ""

        'jrs 2011/09/30
        Session("TableTitle") = ""

        PackJScritpBuilder()

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub
mnuNoRegress_Error:
        ' MsgBox("Error mnuNoRegress_click() routine")
        Exit Sub


    End Sub
    'JAM 2011-01-22 Logistic Regression
    Private Sub mnuLogistic_Click()

        On Error GoTo mnuLogistic_Error 'JAM 2016-11-24

        Session("mnuLinear") = False
        Session("mnuLogarithmic") = False
        Session("mnuExponential") = False
        Session("mnuPower") = False
        Session("mnu2nd") = False
        Session("mnu3nd") = False
        Session("mnuLogistic") = True

        'JAM 2010-12-11 Resetting Logit Mode
        Session("ChangeSatPoint") = False
        'Session("SaturationValue") = 100


        If Session("mnuUnderlyingVisible") Then
            If Session("mnuUnderlyingChecked") Then
                Session("WholeCountries") = True
            Else
                Session("WholeCountries") = False
            End If
        Else
            Session("WholeCountries") = False
        End If


        Session("mnuEqTurnChecked") = False
        Session("mnuEqTurnEnabled") = False
        Session("RegressType") = "LIN"

        Session("ScatterLogistic") = "Scatter.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_SaturParam.aspx")


        'lblJS.Enabled = True
        'lblJS.Visible = True

        Exit Sub
mnuLogistic_Error:
        'MsgBox("Error mnuLogistic_click() routine")
        Exit Sub

    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()

        On Error GoTo PackJScritpBuilder_Error 'JAM 2016-11-24


        'Dim vgraphtitle As String

        'JAM 2016-09-26
        If Session("IsaGList") And Session("TotMulGroups") > 1 Then
            Session("TableTitle") = BuildGraphTitle()
        Else 'JAM 2017-01-31
            'jrs 2024/02/13 added and Session("TableTitle") = ""
                If Session("IsaGList") And Session("TableTitle") = "" Then
                Session("TableTitle") = Session("GListName")
            End If
        End If

        'JAM 2016-11-21 Changing Title fr Multiple Regressions
        If Session("TotMulGroups") > 1 And Session("RegressType") <> "" And (Not (Session("MulRegressTitle") Is Nothing) Or Not (Session("MulRegressTitle") <> "")) Then 'JAM 2016-12-11 Only is MulRegressTitle 
            Session("TableTitle") = Session("MulRegressTitle")
        End If


        Session("bBubble") = (Session("ThirdDimOk") = True And Session("thirdimsel") <> "N")
        'jrs 2011/09/20
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        'JAM 2018-04-29 Avoiding Calling hichcharts-more.js from web due to conflict with another javascript library, the calling was included in the aspx file to js local file 
        'If Session("GType") = "SCAT" And Session("bBubble") = True Then
        'sJscript2 = "<script src=""http://code.highcharts.com/highcharts-more.js""></script>" & vbCr
        'sJscript2 = sJscript2 & "<script>" & vbCr
        'End If
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/02/25 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr



        sJscript2 = sJscript2 & "chart: {" & vbCr
        If Session("bBubble") = True Then
            sJscript2 = sJscript2 & "type: 'bubble'," & vbCr
        Else
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
        'jrs 2024/08/12
        sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '30px'}," & vbCr

        If Session("TableTitle") > "" And Session("TableTitle") <> "Title" Then
            sJscript2 = sJscript2 & "text:       '" & Session("TableTitle") & "'," & vbCr
        Else
            sJscript2 = sJscript2 & "text:       ''," & vbCr
        End If
        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxisXLabel As String
        sAxisXLabel = getXAxisLabel()
        Dim sAxisYLabel As String
        sAxisYLabel = getYAxisLabel()
        Dim sZlbl As String = ""
        If Session("bBubble") Then
            sZlbl = getZAxisVar()
        End If

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<span style=""color:' + this.point.fillColor + '"">●</span>' + ' ' + this.point.name + '<br>" & sAxisXLabel & ": ' + this.point.x + '<br>" & sAxisYLabel & ": ' + this.point.y"
        If Session("bBubble") = True Then
            sJscript2 = sJscript2 & " + '<br>" & sZlbl & ": ' + this.point.z;" & vbCr
        Else
            sJscript2 = sJscript2 & ";" & vbCr
        End If
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        'jrs 2024/08/12
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        'jrs 2024/08/12
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jrs 2024/08/12 sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        'jrs 2024/08/12
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr

        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        'jrs 2024/08/12
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'" & vbCr
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
        If Session("IsaGList") And Session("TotMulGroups") > 1 And Session("RegressType") <> "" Then  'JAM 2017-01-02
            sJscript2 = sJscript2 & "enabled:     true" & vbCr
            'sJscript2 = sJscript2 & "enabled:     true," & vbCr
            'sJscript2 = sJscript2 & "layout:     'vertical'," & vbCr
            'sJscript2 = sJscript2 & "align:     'right'," & vbCr
            'sJscript2 = sJscript2 & "verticalAlign:     'middle'," & vbCr
            'sJscript2 = sJscript2 & " borderWidth:     2" & vbCr

        Else
            sJscript2 = sJscript2 & "enabled:     false" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr
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

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim sZeros As String = ""
        sJscript2 = sJscript2 & PlotPairToJSSeries(Session("CCount"))
        'jrs 2012/05/28
        If Session("RegressType") <> "" Then
            'Plot Regression
            sJscript2 = sJscript2 & "," & PlotRegPairToJSSeries(Session("TotCountries"))
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


        Exit Sub
PackJScritpBuilder_Error:
        'MsgBox("Error PackJScritpBuilder_click() routine")
        Exit Sub

    End Sub

    Private Sub RegionsbyGList(ByVal pGListColor As String)

        On Error GoTo RegionsbyGList_Error 'JAM 2016-11-24

        GListColorCount = 0
        Dim dbGL As String
        dbGL = Session("IFs_MDB2Active")
        Dim rsGL As DataTable
        Dim vNumReg As Integer
        Dim IdGlist As Integer


        'JAM 2009-04-11 Selecting Code for the current GList
        SetDataTablefromAccess(dbGL, "SELECT Code FROM GlistId WHERE Description = """ & pGListColor & """", "", rsGL)

        If rsGL.Rows.Count > 0 Then
            IdGlist = rsGL.Rows(0).Item("Code")
        End If

        'JAM 2009-04-11 Selecting Total of regions for the current GList
        SetDataTablefromAccess(dbGL, "SELECT COUNT(*) as TotRegions FROM Glist WHERE GListCode = " & IdGlist & "", "", rsGL)

        If rsGL.Rows.Count Then
            vNumReg = rsGL.Rows(0).Item("TotRegions")
        End If

        GListColorCount = vNumReg

        Exit Sub
RegionsbyGList_Error:
        'MsgBox("Error RegionsbyGList_click() routine")
        Exit Sub

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '           JAM 2009-04-11
    '           Function created to assign country colors based on current Color GList
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub AnyRegionColors()

        Dim dbGL As String
        dbGL = Session("IFs_MDB2Active")
        Dim rsGL As DataTable
        Dim indcolor As Integer
        Dim NumGroup()
        Dim DefineColor()
        Dim countryindex As Integer
        Dim colorforcountry As Integer
        Dim NumRecords As Integer

        On Error GoTo anyregionerror

        ReDim NumGroup(GListColorCount + 1)
        ReDim DefineColor(GListColorCount + 1)


        NumRecords = 0
        Dim i As Integer
        For i = 1 To Session("TotCountries")
            'jrs 2015/09/25 Session("GroupingColor")(i) = "RGB(0, 0, 0)"
            Session("GroupingColor")(i) = "#000000"
        Next i

        For i = 1 To GListColorCount
            DefineColor(i) = PairColor(i - 1, 0)
        Next i

        'jrs 2022/08/16 added [] to Group for SQLite
        'JAM 2009-05-06  Using GList Colors, adding Regions
        Dim sql As String, a As Integer
        sql = "SELECT GroupName, GroupNum, [Group] " &
        "From GListId, Glist, GroupIDNewFull " &
        "WHERE GListId.Code = Glist.GListCode And " &
        "Glist.Region = GroupIDNewFull.GroupName AND " &
        "GListId.Description = """ & GListColor & """ AND " &
        "GList.[Group] = TRUE " &
        "UNION " &
        "SELECT RegionName,RegionNum, [Group] " &
        "FROM GListId,Glist,RegionIDNewFull " &
        "WHERE GListId.Code = Glist.GListCode And " &
        "Glist.Region = RegionIDNewFull.RegionName AND " &
        "GListId.Description = """ & GListColor & """ AND " &
        "GList.[Group] = FALSE " &
        "ORDER BY GroupName"

        SetDataTablefromAccess(dbGL, sql, "", rsGL)
        If rsGL.Rows.Count > 0 Then

            NumRecords = rsGL.Rows.Count

            a = 1
            Do While (a <= NumRecords)
                NumGroup(a) = Val(rsGL.Rows(a - 1).Item("GroupNum"))
                a = a + 1
            Loop
        End If

        If NumRecords <> 0 Then
            'jrs 2022/08/16 added [] to Group for SQLite
            'JAM 2009-05-06 Using Glist and Regions
            sql = "SELECT GroupIDNewFull.GroupName, GroupIDNewFull.GroupNum, RegionalizationFull.Country,GroupIDNewFull.Alias AS Alias " &
            "From GListId, Glist, GroupIDNewFull, GroupingFull, RegionalizationFull " &
            "WHERE GListId.Code = Glist.GListCode And " &
            "Glist.Region = GroupIDNewFull.GroupName AND " &
            "GroupIDNewFull.GroupNum = GroupingFull.GroupNo AND " &
            "GListId.Description = """ & GListColor & """ AND " &
            "GroupingFull.RegionNo = RegionalizationFull.NewRegion AND " &
            "GList.[Group] = TRUE " &
            "UNION " &
            "SELECT RegionIDNewFull.RegionName, RegionIDNewFull.RegionNum, RegionalizationFull.Country,RegionIDNewFull.RegionName AS Alias " &
            "From GListId, Glist, RegionIDNewFull,RegionalizationFull " &
            "WHERE GListId.Code = Glist.GListCode AND " &
            "Glist.Region = RegionIDNewFull.RegionName AND " &
            "RegionIDNewFull.RegionNum = RegionalizationFull.NewRegion AND " &
            "GListId.Description = """ & GListColor & """ AND " &
            "GList.[Group] = FALSE " &
            "ORDER BY RegionalizationFull.Country;"

            SetDataTablefromAccess(dbGL, sql, "", rsGL)
            Dim j As Integer
            If rsGL.Rows.Count > 0 Then    'JAM 2009-04-14 Cursor Validation
                a = 0


                Do While (a < rsGL.Rows.Count)
                    'JAM 2009-03-21 Adding properly index
                    countryindex = IFsCtrys.Item(rsGL.Rows(a).Item("Country")).Index

                    'JAM 2009-04-12 Recoverying the GList Color for every country
                    colorforcountry = 1
                    For j = 1 To GListColorCount
                        If NumGroup(j) = rsGL.Rows(a).Item("GroupNum") Then
                            colorforcountry = j
                            Exit For
                        End If
                    Next j

                    Session("GroupingColor")(countryindex) = DefineColor(colorforcountry)
                    'JAM 2010-04-08 Loading Region Name to save in Excel File
                    'GlistforRegion(countryindex) = rsGL.Fields("Alias")
                    'GlistforRegion(countryindex) = rsGL.Fields("GroupName")

                    a = a + 1

                Loop
                'JAM 2009-07-06 Closing cursor only it was opened
            End If
        End If
        '    rsGL.Close
        'JAM 2010-10-24 Adding errors managaging
        Exit Sub
anyregionerror:
        'Msg$ = "Sorry. An error has arisen during statistics parameters generation "
        'MsgBox(Msg$, vbCritical)
        Exit Sub
    End Sub

    Private Sub LoadPairColors()

        On Error GoTo LoadPairColors_Error 'JAM 2016-11-24

        'GREEN - Africa
        ReDim PairColor(10, 2)
        PairColor(0, 0) = "RGB(0, 150,0)"
        'PairColor(0, 0) = "#009600"
        PairColor(0, 1) = "RGB(110, 150, 110)"
        'PairColor(0, 1) = "#6e966e"
        'ORANGE - Asia
        PairColor(1, 0) = "RGB(255, 102, 0)"
        'PairColor(1, 0) = "#ff6600"
        PairColor(1, 1) = "RGB(255, 185, 137)"
        'PairColor(1, 1) = "#ffb989"
        'BLUE - Europe
        PairColor(2, 0) = "RGB(0, 0, 150)"
        'PairColor(2, 0) = "#000096"
        PairColor(2, 1) = "RGB(125, 125, 255)"
        'PairColor(2, 1) = "#7d7dff"
        'YELLOW - LatAm&Carib
        PairColor(3, 0) = "RGB(255, 255, 0)"
        'PairColor(3, 0) = "#ffff00"
        PairColor(3, 1) = "RGB(255, 255, 125)"
        'PairColor(3, 1) = "#ffff7d"
        'blue navy - North America
        PairColor(4, 0) = "RGB(0, 191, 255)"   'JAM 2009-03-25 Assign same Blue color for G-list and countries
        'PairColor(4, 0) = "#00bfff"
        PairColor(4, 1) = "RGB(0, 219, 214)"
        'PairColor(4, 1) = "#00dbd6"
        'red - Oceania
        PairColor(5, 0) = "RGB(255, 0, 0)"
        'PairColor(5, 0) = "#ff0000"
        PairColor(5, 1) = "RGB(255, 125, 125)"
        'PairColor(5, 1) = "#ff7d7d"
        'brown
        PairColor(6, 0) = "RGB(130, 43, 0)"
        'PairColor(6, 0) = "#822b00"
        PairColor(6, 1) = "RGB(255, 132, 41)"
        'PairColor(6, 1) = "#ff8429"
        'magenta
        PairColor(7, 0) = "RGB(128, 0, 128)"
        'PairColor(7, 0) = "#800080"
        PairColor(7, 1) = "RGB(255, 97, 255)"
        'PairColor(7, 1) = "#ff61ff"
        'pink
        PairColor(8, 0) = "RGB(255, 153, 204)"
        'PairColor(8, 0) = "#ff99cc"
        PairColor(8, 1) = "RGB(255, 209, 232)"
        'PairColor(8, 1) = "#ffd1e8"
        'gray
        PairColor(9, 0) = "RGB(128, 128, 128)"
        'PairColor(9, 0) = "#808080"
        PairColor(9, 1) = "RGB(207, 207, 207)"
        'PairColor(9, 1) = "#cfcfcf"

        Exit Sub
LoadPairColors_Error:
        'MsgBox("Error mnuGeographic_click() routine")
        Exit Sub

    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2009-04-07     Regions colors Recovery
    '
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub RecoveryColors()
        'jrs 2011/09/29 not yet implemented for G-Lists


        'JAM 2009-04-11 Recovering the number of items for a Glist
        'If IsaGlist Then
        '    GListColor = GListName
        '    GListColorCount = GlistCount
        'End If

        On Error GoTo RecoveryColors_Error 'JAM 2016-11-24

        Call RegionsbyGList(GListColor)

        Dim dbGL As String
        dbGL = Session("IFs_MDB2Active")
        Dim rsGL As DataTable
        Dim IdGlist As Integer
        Dim sql As String

        ''JAM 2009-04-09 Selecting Colors Defined by user
        'If GListColorCount > 10 Then
        '    ReDim PairColor(GListColorCount, 2)
        'Else
        '    'ReDim PairColor(10, 2)
        '    UNColorsFlag = True
        '    Call UseUnescoColors()
        'End If



        ''MsgBox "SELECT Code FROM GlistId WHERE Description = " & GListName & "", vbInformation, "Group Colors"
        'SetDataTablefromAccess(dbGL, "SELECT Code FROM GlistId WHERE Description = """ & GListColor & """", "", rsGL)

        'If rsGL.Rows.Count > 0 Then
        '    IdGlist = rsGL.Rows(0).Item("Code")
        'End If

        'rsGL.Close()
        ''dbGL.Close

        ''   Dim sql As String
        ''   Dim dbGL As Database
        ''Set dbGL = OpenDatabase(IFs_MDB2Active)

        ''JAM 2009-04-12 Recoverying the group colors in contry and Glist Mode
        'SetDataTablefromAccess(dbGL, "SELECT ColorValue,LightColorValue FROM Glist WHERE GlistCode = " & IdGlist & " ORDER BY Region ", "", rsGL)
        'i = 0
        'Do While (Not rsGL.EOF)
        '    PairColor(i, 0) = rsGL.Fields("ColorValue")
        '    PairColor(i, 1) = rsGL.Fields("LightColorValue")

        '    i = i + 1
        '    rsGL.MoveNext()
        'Loop


        'dbGL.Close()

        Call AnyRegionColors()

        Exit Sub
RecoveryColors_Error:
        ' MsgBox("Error RecoveryColors_click() routine")
        Exit Sub

    End Sub

    Public Sub UseUnescoColors()

        On Error GoTo UseUnescoColors_Error 'JAM 2016-11-24
        ReDim PairColor(10, 2)
        If False Then 'If Not UNColorsFlag Then    'JAM 2009-03-29  Changing define colors for Order by clause
            '204 UNESCO Arab States - Gray
            PairColor(0, 0) = "RGB(128, 128, 128)"
            PairColor(0, 1) = "RGB(207, 207, 207)"
            '205 UNESCO Cen&East Europe - Deep Sky Blue
            PairColor(1, 0) = "RGB(0, 191, 255)"
            PairColor(1, 1) = "RGB(0, 219, 214)"
            '206 UNESCO Central Asia - Orange
            PairColor(2, 0) = "RGB(255, 102, 0)"
            PairColor(2, 1) = "RGB(255, 185, 137)"
            '212 UNESCO E As&Pac LessDev -Magenta
            PairColor(3, 0) = "RGB(128, 0, 128)"
            PairColor(3, 1) = "RGB(255, 97, 255)"
            '213 UNESCO E As&Pac MoreDev -REd
            PairColor(4, 0) = "RGB(255, 0, 0)"
            PairColor(4, 1) = "RGB(255, 125, 125)"
            '208 UNESCO LatAm&Carib - Yellow
            PairColor(5, 0) = "RGB(255, 255, 0)"
            PairColor(5, 1) = "RGB(255, 255, 125)"
            '209 UNESCO NoAmer&WEur - Blue
            PairColor(6, 0) = "RGB(0, 0, 150)"
            PairColor(6, 1) = "RGB(125, 125, 255)"
            '214 UNESCO S&W Asia - light brown
            PairColor(7, 0) = "RGB(255, 132, 41)"
            PairColor(7, 1) = "RGB(130, 43, 0)"
            '211 UNESCO SubSah Africa - Green
            PairColor(8, 0) = "RGB(0, 150, 0)"
            PairColor(8, 1) = "RGB(110, 150, 110)"

            'Other Regions-pink
            PairColor(9, 0) = "RGB(255, 153, 204)"
            PairColor(9, 1) = "RGB(255, 209, 232)"
            'Other Regions-black
            PairColor(10, 0) = "RGB(255, 153, 204)"
            PairColor(10, 1) = "RGB(255, 209, 232)"

        Else
            'JAM 2010-04-08 Default Color using WB Regions
            'Red - WB E Asia&Pac Developing
            '        PairColor(0, 0) = "RGB(255, 0, 0)
            '        PairColor(0, 1) = "RGB(255, 125, 125)
            '       'DeepSkyBlue -WB Eur&CenAsia Developing
            '        PairColor(1, 0) = "RGB(0, 0, 150)
            '        PairColor(1, 1) = "RGB(125, 125, 255)
            '
            '        '   'YELLOW - WB LatAm&Car Developing
            '        PairColor(2, 0) = "RGB(255, 255, 0)
            '        PairColor(2, 1) = "RGB(255, 255, 125)
            '
            '         '   'ORANGE - WB MdEast&NAf Developing
            '        PairColor(3, 0) = "RGB(255, 102, 0)
            '        PairColor(3, 1) = "RGB(255, 185, 137)
            '
            '        '    'brown - WB South Asia
            '        PairColor(4, 0) = "RGB(130, 43, 0)
            '        PairColor(4, 1) = "RGB(255, 132, 41)
            '        '   'green - WB Afr-SubSahar
            '        PairColor(5, 0) = "RGB(0, 150, 0)
            '        PairColor(5, 1) = "RGB(110, 150, 110)
            '        '  'light blue- 146- High-Income Economies
            '        PairColor(6, 0) = "RGB(0, 191, 255)
            '        PairColor(6, 1) = "RGB(0, 219, 214)
            '
            '   'green - WB Afr-SubSahar
            PairColor(0, 0) = "RGB(0, 150, 0)"
            PairColor(0, 1) = "RGB(110, 150, 110)"
            'Red - WB E Asia&Pac Developing
            PairColor(1, 0) = "RGB(255, 0, 0)"
            PairColor(1, 1) = "RGB(255, 125, 125)"

            'DeepSkyBlue -WB Eur&CenAsia Developing
            PairColor(2, 0) = "RGB(0, 0, 150)"
            PairColor(2, 1) = "RGB(125, 125, 255)"

            '  'light blue- 146- High-Income Economies
            PairColor(3, 0) = "RGB(0, 191, 255)"
            PairColor(3, 1) = "RGB(0, 219, 214)"

            '   'YELLOW - WB LatAm&Car Developing
            PairColor(4, 0) = "RGB(255, 255, 0)"
            PairColor(4, 1) = "RGB(255, 255, 125)"

            '   'ORANGE - WB MdEast&NAf Developing
            PairColor(5, 0) = "RGB(255, 102, 0)"
            PairColor(5, 1) = "RGB(255, 185, 137)"

            '    'brown - WB South Asia
            PairColor(6, 0) = "RGB(130, 43, 0)"
            PairColor(6, 1) = "RGB(255, 132, 41)"





            PairColor(7, 0) = "RGB(128, 0, 128)"
            PairColor(7, 1) = "RGB(255, 97, 255)"
            'pink
            PairColor(8, 0) = "RGB(255, 153, 204)"
            PairColor(8, 1) = "RGB(255, 209, 232)"
            'gray
            PairColor(9, 0) = "RGB(128, 128, 128)"
            PairColor(9, 1) = "RGB(207, 207, 207)"

            '        'GREEN - Africa
            '        PairColor(0, 0) = "RGB(0, 150, 0)
            '        PairColor(0, 1) = "RGB(110, 150, 110)
            '        'ORANGE - Asia
            '        PairColor(1, 0) = "RGB(255, 102, 0)
            '        PairColor(1, 1) = "RGB(255, 185, 137)
            '        'BLUE - Europe
            '        PairColor(2, 0) = "RGB(0, 0, 150)
            '        PairColor(2, 1) = "RGB(125, 125, 255)
            '        'YELLOW - LatAm&Carib
            '        PairColor(3, 0) = "RGB(255, 255, 0)
            '        PairColor(3, 1) = "RGB(255, 255, 125)
            '         'blue navy - North America
            '        PairColor(4, 0) = "RGB(0, 191, 255)   'JAM 2009-03-25 Assign same Blue color for G-list and countries
            '        PairColor(4, 1) = "RGB(0, 219, 214)
            '        'red - Oceania
            '        PairColor(5, 0) = "RGB(255, 0, 0)
            '        PairColor(5, 1) = "RGB(255, 125, 125)
            '        'brown
            '        PairColor(6, 0) = "RGB(130, 43, 0)
            '        PairColor(6, 1) = "RGB(255, 132, 41)
            'magenta



        End If

        Exit Sub
UseUnescoColors_Error:
        ' MsgBox("Error mnuGeographic_click() routine")
        Exit Sub

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2010-12-12      Regression routine
    '
    '-----------------------------------------------------------------------------------
    Public Sub RegressionPlot(ByVal Degree As Integer, ByVal Transform As String)
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation-new proc
        'calculate forecast using Regression Class
        On Error GoTo Forecasterr
        Dim SaturatePoint As Double
        Dim x#, y#
        Dim a1#, B1#, a#, b#, II%, i2
        Dim Equation As String
        Dim PrevOrigInd(), PrevCVarI()
        Dim RegResult As Double
        Dim Formula As String
        Dim CoefB1, CoefB2, CoefB3, CoefB4
        Dim ThirdCoef, SecondCoef, FourthCoef, sign, sign2, sign3, sign4
        Dim B1Format, B2Format, B3Format, B4Format, RFormat
        Dim RegressFormul, RsqrFormul

        'ReDim ForeLong(UBound(V) + YearsAhead%, UBound(V, 2)) As Variant
        'ReDim ForeLabels(UBound(v2), UBound(v2, 2) + YearsAhead%) As String
        'ReDim RSQR(UBound(V, 2)) As Double
        '    ForeLong = V
        '    ForeLabels = v2


        'JAM 2009-03-31 Dynamic Array Declarations
        ReDim PrevOrigInd(Session("TotCountries"))
        ReDim PrevCVarI(Session("TotCountries"))


        'jrs 2007/06/11
        Dim MinVI As Single, MaxVI As Single
        MinVI = 1000000000
        MaxVI = -1000000000


        'JAM 2009-08-22 For Saturation Point Selected
        If Not Session("ChangeSatPoint") Then

            For II% = 1 To Session("CCount")

                'mti March 2010 - ShowPoints(II%)
                'If Not IsNull(CVarI(II%)) And Not IsEmpty(CVarI(II%)) Then
                If IsNumeric(Session("CVarI")(II%)) Then ' And(Not mnuExcludeUnselected.Checked Or ShowPoints(II%)) Then
                    'mti March 2010


                    'jrs 2007/06/11
                    If MinVI > Session("CVarI")(II%) And IsNumeric(Session("CVarD")(II%)) Then MinVI = Session("CVarI")(II%)
                    If MaxVI < Session("CVarI")(II%) And IsNumeric(Session("CVarD")(II%)) Then MaxVI = Session("CVarI")(II%)
                End If

            Next II%

        Else
            For II% = 1 To Session("CCount")

                'mti March 2010 - ShowPoints(II%)
                'If Not IsNull(CVarI(II%)) And Not IsEmpty(CVarI(II%)) Then
                If IsNumeric(Session("CVarI")(II%)) Then 'And Not IsEmpty(CVarI(II%)) And (Not mnuExcludeUnselected.Checked Or ShowPoints(II%)) Then
                    'mti March 2010
                    'jrs 2007/06/11
                    'JAM 2011-11-02 Changing SaturCVarD validation
                    'If MinVI > Session("CVarI")(II%) And IsNumeric(Session("SaturCVarD")(II%)) And IsNumeric(Session("CVarD")(II%)) Then MinVI = Session("CVarI")(II%)
                    'If MaxVI < Session("CVarI")(II%) And IsNumeric(Session("SaturCVarD")(II%)) And IsNumeric(Session("CVarD")(II%)) Then MaxVI = Session("CVarI")(II%)
                    If MinVI > Session("CVarI")(II%) And IsNumeric(Session("SaturCVarD")(II%)) And IsNumeric(Session("SaturCVarD")(II%)) Then MinVI = Session("CVarI")(II%)
                    If MaxVI < Session("CVarI")(II%) And IsNumeric(Session("SaturCVarD")(II%)) And IsNumeric(Session("SaturCVarD")(II%)) Then MaxVI = Session("CVarI")(II%)
                End If

            Next II%


        End If





        If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
            Session("CCount") = Session("TotCountries")
        End If




        With vreg
            .Degree = Degree



            For i2 = 1 To Session("CCount")
                'mti March 2010 - ShowPoints(II%)
                If Not Session("mnuExcludeUnselectedChecked") Or Session("ShowPoints")(i2) Then
                    'mti March 2010

                    If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                        If IsNumeric(Session("OrigIndFir")(i2)) And IsNumeric(Session("OrigDep")(i2)) And Session("CountryRegress")(i2) Then


                            Select Case Transform
                                Case "LOG"
                                    x# = CDbl(Math.Log(Session("OrigIndFir")(i2)))
                                Case "EXP"
                                    x# = CDbl(Math.Exp(Session("OrigIndFir")(i2))) 'CDbl(TC1 * exp(TC2 * OrigIndFir(i2)))

                                Case "POW"
                                    'jrs 2017/10/23 x# = CDbl((Session("OrigIndFir")(i2) ^ (Session("PowerDegree"))))
                                    x# = CDbl(Math.Log(Session("OrigIndFir")(i2)))
                                Case Else
                                    x# = CDbl(Session("OrigIndFir")(i2))
                            End Select


                            'jrs 2017/10/23
                            If Transform = "POW" Then
                                y# = CDbl(Math.Log(Session("OrigDep")(i2)))
                            Else
                                y# = CDbl(Session("OrigDep")(i2))  '1
                            End If



                            .XYAdd(x#, y#) 'Ln(i2), Format(PlotLong(i2, 1), "0.000")
                        End If
                    Else
                        If IsNumeric(Session("CVarI")(i2)) And IsNumeric(Session("CVarD")(i2)) Then


                            'Format(PlotLong(PYear%, cc%), "0.000")
                            Select Case Transform
                                Case "LOG"
                                    x# = CDbl(Math.Log(Session("CVarI")(i2)))
                                Case "EXP"
                                    x# = CDbl(Math.Exp(Session("CVarI")(i2))) 'CDbl(TC1 * exp(TC2 * CVarI(i2)))
                                Case "POW"
                                    'jrs 2017/10/23 x# = CDbl((Session("CVarI")(i2) ^ (Session("PowerDegree"))))
                                    x# = CDbl(Math.Log(Session("CVarI")(i2)))
                                Case Else
                                    x# = CDbl(Session("CVarI")(i2))
                            End Select


                            'jrs 2017/10/23
                            If Transform = "POW" Then
                                y# = CDbl(Math.Log(Session("CVarD")(i2)))
                            Else
                                y# = CDbl(Session("CVarD")(i2))  '1
                            End If



                            .XYAdd(x#, y#) 'Ln(i2), Format(PlotLong(i2, 1), "0.000")
                        End If

                    End If
                    'mti March 2010 - ShowPoints(II%)
                End If
                'mti March 2010

            Next i2

        End With



        'jrs 2008/08/28
        'DoEvents()


        Session("CCount") = Session("TotCountries")
        'jrs 2011/09/30
        ReDim Session("RegData")(2, Session("CCount"))

        For II% = 1 To Session("CCount")


            'JAM 2010-04-27 Max and Min have changed (Optional)
            Session("RegData")(1, II%) = MinVI + ((MaxVI - MinVI) * ((II% - 1) / (Session("CCount") - 1)))
            ' grf_Scatter.XData(1, II% - 1) = gMinX + ((gMaxX - gMinX) * ((II% - 1) / (CCount - 1)))



            If Transform = "LOG" Then
                RegResult = vreg.RegVal(CDbl(Math.Log(Session("RegData")(1, II%))))
            Else
                If Transform = "EXP" Then

                    RegResult = vreg.RegVal(CDbl(Math.Exp(Session("RegData")(1, II%))))
                Else
                    If Transform = "POW" Then
                        'jrs 2017/10/23 RegResult = vreg.RegVal(CDbl((Session("RegData")(1, II%)) ^ (Session("PowerDegree"))))
                        RegResult = vreg.RegVal(CDbl(Math.Log(Session("RegData")(1, II%))))
                        RegResult = Math.Exp(RegResult)
                    Else
                        If Not Session("ChangeSatPoint") Then  'JAM 2009-08-26 Code Added for Saturation Point
                            RegResult = vreg.RegVal(CDbl(Session("RegData")(1, II%)))
                        Else
                            RegResult = Session("SaturationValue") / (1 + Math.Exp(CDbl(vreg.Coeff(0) + vreg.Coeff(1) * Session("RegData")(1, II%))))
                        End If
                    End If
                End If
            End If

            'JAM 2009-08-17    Proessential valid data
            Session("RegData")(2, II%) = RegResult

        Next II%



        Select Case Transform
            Case "LINEAR"
                Formula = "("
            Case "LOG"
                Formula = "ln("
            Case "EXP"
                Formula = "Exp("
            Case "POW"
                Formula = "^"
            Case Else
                Formula = "("
        End Select





        If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
            Session("CCount") = Session("TotCountries")
        Else
            If Session("IsaGlist") Then
                Session("CCount") = Session("GlistCount")
            Else
                Session("CCount") = Session("TotCountries")
            End If

        End If

        CoefB1 = vreg.Coeff(0)  'Y=a + bt
        CoefB2 = vreg.Coeff(1)
        CoefB3 = -9999
        CoefB4 = -9999

        If Degree > 1 Then

            CoefB3 = vreg.Coeff(2)
            ThirdCoef = Math.Abs(CoefB3)
            If CoefB3 < 0 Then
                sign2 = " - "

            Else
                sign2 = " + "
            End If

            If Degree > 2 Then
                CoefB4 = vreg.Coeff(3)
                FourthCoef = Math.Abs(CoefB4)
                If CoefB4 < 0 Then
                    sign3 = " - "
                Else
                    sign3 = " + "
                End If

            End If

        End If

        SecondCoef = Math.Abs(CoefB2)
        If CoefB2 < 0 Then
            sign = " - "

        Else
            sign = " + "
        End If


        CalcRSQR(Transform)

        If Math.Abs(CoefB1) < 0.01 Or Math.Abs(CoefB1) > 1000000 Then
            B1Format = "0.00E+00"
        Else
            B1Format = "0.0000"
        End If

        If SecondCoef < 0.01 Or SecondCoef > 1000000 Then
            B2Format = "0.00E+00"
        Else
            B2Format = "0.0000"
        End If

        If ThirdCoef < 0.01 Or ThirdCoef > 1000000 Then
            B3Format = "0.00E+00"
        Else
            B3Format = "0.0000"
        End If

        If FourthCoef < 0.01 Or FourthCoef > 1000000 Then
            B4Format = "0.00E+00"
        Else
            B4Format = "0.0000"
        End If

        If Math.Abs(RSQR) < 0.01 Or Math.Abs(RSQR) > 1000000 Then
            RFormat = "0.00E+00"
        Else
            RFormat = "0.0000"
        End If


        If Transform = "NONE" Or Transform = "LOG" Then
            Equation = "y= " + Format(CoefB1, B1Format) + sign + Format(SecondCoef, B2Format) + "*" + Formula + "x)"
            'JAM 2009-08-22 If We've choose Saturation Option
            If Session("ChangeSatPoint") Then
                If sign = " - " Then
                    Equation = "y= " + Format(CDbl(Session("SaturationValue")), "####0.00") + "/(1 + e^(" + Format(CoefB1, B1Format) + sign + Format(SecondCoef, B2Format) + "*" + Formula + "x)))"
                Else
                    Equation = "y= " + Format(CDbl(Session("SaturationValue")), "####0.00") + "/(1 + e^(" + Format(CoefB1, B1Format) + "+" + Format(SecondCoef, B2Format) + "*" + Formula + "x)))"
                End If
            End If


        Else
            If Transform = "EXP" Then
                Equation = "y= " + Format(CoefB1, B1Format) + sign + Format(SecondCoef, B2Format) + "*e^(" + "x)" 'sign + Format(SecondCoef, B2Format) + "*x)"
            Else
                If Transform = "POW" Then
                    'jrs 2017/10/23 Equation = "y= " + Format(CoefB1, B1Format) + sign + Format(SecondCoef, B2Format) + "*x^(" + CStr(Session("PowerDegree")) + ")"
                    Equation = "y= " + Format(Math.Exp(CoefB1), B1Format) + "*x^(" + Format(CoefB2, B2Format) + ")"
                Else
                    Equation = "y= " + Format(CoefB1, B1Format) + sign + Format(SecondCoef, B2Format) + "*" + Formula + "x)"
                End If
            End If

        End If


        If CoefB3 <> -9999 Then
            Equation = Equation + sign2 + Format(ThirdCoef, B3Format) + "*" + Formula + "x^2)"
        End If

        If CoefB4 <> -9999 Then
            Equation = Equation + sign3 + Format(FourthCoef, B4Format) + "*" + Formula + "x^3)"
        End If


        'grf_Scatter.SubsetLabels(0) = "Data in bubbles format"

        'jrs 2015/09/28 Session("TableTitle") = PrepareForHTML(Equation & vbCr & "R(SQR) = " & Format(RSQR, RFormat))
        Session("TableTitle") = Equation & "<br/>R(SQR) = " & Format(RSQR, RFormat)
        RegressFormul = Equation
        RsqrFormul = Format(RSQR, RFormat)


        If Session("IsaGlist") Then
            Session("CCount") = Session("GlistCount")
            'grf_Scatter.Points = GlistCount
        End If

        Session("mnuTurnOffCaption") = "Turn On"


        '    'JAM 2008-04-13 code added for considering second independent variable for regression
        '    If ThirdDimOk Then
        '        For II% = 1 To TotCountries
        '            'Returning to the original first variable values
        '
        '            CVarI(II%) = PrevCVarI(II%)
        '            OrigIndFir(II%) = PrevOrigInd(II%)
        '
        '            PrevOrigInd(II%) = Null
        '            PrevCVarI(II%) = Null
        '        Next II%
        '    End If
        '

        Exit Sub

Forecasterr:
        'MsgBox("Error occurred while calculating " + ExtpType$ + " trend.")
        Exit Sub

    End Sub

    Private Sub CalcRSQR(ByVal vTransform As String)
        '------------------------------------------------------------------------------------------

        'timeseries extrapolation-new proc

        On Error GoTo CalcRSQR_Error 'JAM 2016-11-24

        Dim YBAR#, SUMOFYSQR#, SQROFYSUM#

        Dim SSE#, SST#

        Dim ObsNo%, Foretot, ForeMean, rr%, XReal


        Foretot = 0
        ForeMean = 0

        SSE = 0
        SST = 0
        YBAR = 0
        SUMOFYSQR = 0
        ObsNo = 0

        For rr% = 1 To Session("CCount")
            'mti March 2010 - ShowPoints(II%)

            If Not Session("mnuExcludeUnselectedChecked") Or Session("ShowPoints")(rr%) Then
                'mti March 2010
                'jrs 2007/06/12 added and isNumeric

                If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                    If IsNumeric(Session("OrigIndFir")(rr%)) And IsNumeric(Session("OrigDep")(rr%)) And Session("CountryRegress")(rr%) Then

                        Select Case vTransform
                            Case "LINEAR"
                                XReal = Session("OrigIndFir")(rr%)
                            Case "LOG"
                                XReal = Math.Log(Session("OrigIndFir")(rr%))
                            Case "EXP"
                                XReal = Math.Exp(Session("OrigIndFir")(rr%))
                            Case "POW"
                                'jrs 2017/10/23 XReal = ((Session("OrigIndFir")(rr%)) ^ (Session("PowerDegree")))
                                XReal = Math.Log(Session("OrigIndFir")(rr%))
                            Case Else
                                XReal = Session("OrigIndFir")(rr%)
                        End Select

                        'jrs 2017/10/19
                        If vTransform = "POW" Then
                            SSE = SSE + (Math.Log(Session("OrigDep")(rr%)) - (vreg.RegVal(CDbl(XReal)))) ^ 2

                            YBAR = YBAR + Math.Log(Session("OrigDep")(rr%))

                            SUMOFYSQR = SUMOFYSQR + Math.Log(Session("OrigDep")(rr%)) ^ 2
                        Else
                            SSE = SSE + (Session("OrigDep")(rr%) - (vreg.RegVal(CDbl(XReal)))) ^ 2

                            YBAR = YBAR + Session("OrigDep")(rr%)

                            SUMOFYSQR = SUMOFYSQR + Session("OrigDep")(rr%) ^ 2
                        End If

                        ObsNo = ObsNo + 1

                    End If

                Else

                    'jrs 2008/07/10 added not isempty(CVARI)

                    'IsNumeric(Empty) = true!

                    If IsNumeric(Session("CVarD")(rr%)) And IsNumeric(Session("CVarI")(rr%)) Then

                        Select Case vTransform
                            Case "LINEAR"
                                XReal = Session("CVarI")(rr%)
                            Case "LOG"
                                XReal = Math.Log(Session("CVarI")(rr%))
                            Case "EXP"
                                XReal = Math.Exp(Session("CVarI")(rr%))
                            Case "POW"
                                'jrs 2017/10/23 XReal = ((Session("CVarI")(rr%)) ^ (Session("PowerDegree")))
                                XReal = Math.Log(Session("CVarI")(rr%))
                            Case Else
                                XReal = Session("CVarI")(rr%)
                        End Select

                        'jrs 2017/10/23
                        If vTransform = "POW" Then
                            SSE = SSE + (Math.Log(Session("CVarD")(rr%)) - (vreg.RegVal(CDbl(XReal)))) ^ 2
                            YBAR = YBAR + Math.Log(Session("CVarD")(rr%))
                            SUMOFYSQR = SUMOFYSQR + Math.Log(Session("CVarD")(rr%)) ^ 2
                        Else
                            SSE = SSE + (Session("CVarD")(rr%) - (vreg.RegVal(CDbl(XReal)))) ^ 2
                            YBAR = YBAR + Session("CVarD")(rr%)
                            SUMOFYSQR = SUMOFYSQR + Session("CVarD")(rr%) ^ 2
                        End If
                        ObsNo = ObsNo + 1

                    End If
                End If
                'mti March 2010 - ShowPoints(II%)
            End If
            'mti March 2010
        Next rr%
        SQROFYSUM = YBAR ^ 2
        YBAR = YBAR / ObsNo

        For rr% = 1 To Session("CCount")
            'mti March 2010 - ShowPoints(II%)
            If Not Session("mnuExcludeUnselectedChecked") Or Session("ShowPoints")(rr%) Then
                'mti March 2010
                'jrs 2007/06/12 added and isNumeric
                If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then

                    If IsNumeric(Session("OrigIndFir")(rr%)) And IsNumeric(Session("OrigDep")(rr%)) And Session("CountryRegress")(rr%) Then
                        'jrs 2017/10/19
                        If vTransform = "POW" Then
                            SST = SST + (Math.Log(Session("OrigDep")(rr%)) - YBAR) ^ 2
                        Else
                            SST = SST + (Session("OrigDep")(rr%) - YBAR) ^ 2
                        End If
                    End If
                Else
                    'jrs 2008/07/10 added not isempty(CVARI)
                    'IsNumeric(Empty) = true!
                    If IsNumeric(Session("CVarD")(rr%)) And IsNumeric(Session("CVarI")(rr%)) Then
                        'jrs 2017/10/19
                        If vTransform = "POW" Then
                            SST = SST + (Math.Log(Session("CVarD")(rr%)) - YBAR) ^ 2
                        Else
                            SST = SST + (Session("CVarD")(rr%) - YBAR) ^ 2
                        End If
                    End If
                End If
                'mti March 2010 - ShowPoints(II%)
            End If
            'mti March 2010
        Next rr%
        '    SST = SUMOFYSQR - (SQROFYSUM / ObsNo)
        RSQR = 1 - (SSE / SST)

        Exit Sub
CalcRSQR_Error:
        'MsgBox("Error CalcRSQR_click() routine")
        Exit Sub


    End Sub
    '---------------------------------------------------------------------------
    '
    '       JAM 2011-01-20 for logistic form
    '
    '-----------------------------------------------------------------------
    Private Sub TransSatDep()

        On Error GoTo TransSatDep_Error 'JAM 2016-11-24

        Dim i, j, limit As Integer

        If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
            limit = Session("TotCountries")
        Else
            limit = Session("CCount")
        End If

        'for Multiple Groups
        If Session("TotMulGroups") = 1 Then
            For i = 1 To limit
                If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                    Session("SaturCVarD")(i) = Session("OrigDep")(i)
                    If Not IsDBNull(Session("OrigDep")(i)) Then
                        If IsNumeric(Session("OrigDep")(i)) And Session("OrigDep")(i) <> 0 Then
                            If ((Session("SaturationValue") / Session("OrigDep")(i)) - 1) > 0 Then
                                Session("OrigDep")(i) = Math.Log(((Session("SaturationValue") / Session("OrigDep")(i))) - 1)
                            Else
                                Session("OrigDep")(i) = DBNull.Value
                            End If
                        Else
                            Session("OrigDep")(i) = DBNull.Value
                        End If
                    End If
                Else
                    Session("SaturCVarD")(i) = Session("CVarD")(i)
                    If Not IsDBNull(Session("CVarD")(i)) Then
                        If IsNumeric(Session("CVarD")(i)) And Session("CVarD")(i) <> 0 Then
                            If ((Session("SaturationValue") / Session("CVarD")(i)) - 1) > 0 Then

                                Session("CVarD")(i) = Math.Log(((Session("SaturationValue") / Session("CVarD")(i))) - 1)
                            Else
                                Session("CVarD")(i) = DBNull.Value
                            End If
                        Else
                            Session("CVarD")(i) = DBNull.Value
                        End If

                    End If
                End If
            Next i
        Else
            If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                For i = 0 To Session("TotMulGroups") - 1
                    For j = 1 To Session("TotCountries")
                        'MulSaturCVarD(i, j) = OrigPrevCVarD(i, j)
                        'If IsNumeric(OrigPrevCVarD(i, j)) And OrigPrevCVarD(i, j) <> 0 Then
                        '    If ((SaturationValue / OrigPrevCVarD(i, j)) - 1) > 0 Then
                        '        OrigPrevCVarD(i, j) = Log(((SaturationValue / OrigPrevCVarD(i, j))) - 1)
                        '    Else
                        '        OrigPrevCVarD(i, j) = Null
                        '    End If
                        'Else
                        '    OrigPrevCVarD(i, j) = Null
                        'End If

                    Next j

                Next i
            Else
                For i = 0 To Session("TotMulGroups") - 1
                    'For j = 1 To DimJoinPrev(i)
                    '    MulSaturCVarD(i, j) = JoinPrevCVarD(i, j)
                    '    If IsNumeric(JoinPrevCVarD(i, j)) And JoinPrevCVarD(i, j) <> 0 Then
                    '        If ((SaturationValue / JoinPrevCVarD(i, j)) - 1) > 0 Then
                    '            JoinPrevCVarD(i, j) = Log(((SaturationValue / JoinPrevCVarD(i, j))) - 1)
                    '        Else
                    '            JoinPrevCVarD(i, j) = Null
                    '        End If
                    '    Else
                    '        JoinPrevCVarD(i, j) = Null
                    '    End If

                    'Next j

                Next i


            End If


        End If

        Exit Sub
TransSatDep_Error:
        'MsgBox("Error TransSatDep_click() routine")
        Exit Sub

    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '   JAM 2011-01-28      Dependent Variable Transformation for Saturation Point
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub RevokeSatDep()

        On Error GoTo RevokeSatDep_Error 'JAM 2016-11-24

        Dim i, limit As Integer

        If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
            limit = Session("TotCountries")
        Else
            limit = Session("CCount")
        End If

        'JAM 2011-01-28 For Multiple Groups
        If Session("TotMulGroups") = 1 Then
            For i = 1 To limit
                If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                    Session("OrigDep")(i) = Session("SaturCVarD")(i)
                Else
                    Session("CVarD")(i) = Session("SaturCVarD")(i)
                End If
            Next i
        Else
            If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                For i = 0 To Session("TotMulGroups") - 1
                    'For j = 1 To Session("TotCountries")
                    '    OrigPrevCVarD(i, j) = MulSaturCVarD(i, j)
                    'Next j
                Next i
            Else
                For i = 0 To Session("TotMulGroups") - 1
                    'For j = 1 To DimJoinPrev(i)
                    '    JoinPrevCVarD(i, j) = MulSaturCVarD(i, j)
                    'Next j
                Next i

            End If


        End If

        Exit Sub
RevokeSatDep_Error:
        'MsgBox("Error RevokeSatDep_click() routine")
        Exit Sub

    End Sub

    Private Sub PIsaGlist()

        On Error GoTo PIsaGlist_Error 'JAM 2016-11-24

        Dim i As Integer


        'JAM 2010-03-07 Review TotCountries assignation
        'Session("GListName") = "UN Regions"
        'Session("GListCount") = 6
        'Session("GChanged") = True
        'Session("IsaGlist") = True
        'Session("IsaGroup") = False

        For i = 1 To Session("TotCountries")
            Session("CVarIPop")(i) = Session("IPopOrig")(i)
            Session("CVarIGdp")(i) = Session("IGdpOrig")(i)
        Next i



        'JAM 2010-03-07 We recalculate only the group has changed
        If Session("GChanged") Then
            ''JAM 2016-10-24   Loading Year Selected
            If Session("AddGlistMode") Then
                Call AddYear(Session("GLbLagDep")(Session("YearChosen")), Session("GLbLagInd")(Session("YearChosen")), Session("LagSecInd")(Session("YearChosen")))
            End If

            ''JAM 2009-10-06 Enabling Navigation Buttons
            'cmdNext.Enabled = Session("
            'cmdPrevious.Enabled = True

            If Session("IsaGlist") And Not Session("IsaGroup") Then
                'JAM 2016-10-30
                If Session("FilterReq") = 1 Then
                    FilterVal(Session("OrigDep"), Session("OrigIndFir"))
                    Session("FilterReq") = Session("FilterReq") + 1
                End If

                'JAM 2017-01-28  Keeping Original GList Value
                Session("GlistCountChanged") = Session("GlistCount")
                Session("GlistCount") = Session("GlistCountChosen")
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Session("CCount") = Session("GlistCount")
                If Session("OrigAgg")(0) <> "" Then
                    AssignVal(Session("OrigDep"), Session("GlistCount"), 0, True, Session("OrigAgg")(0), "O")

                    'AssignVal(Session("OrigDep"), Session("GlistCount"), _
                    '0, True, Session("OrigAgg")(0), "O", _
                    'Session("TotCountries"), Session("IsaGlist"), Session("GlistCount"), Session("GListMember"), _
                    'Session("GListIsGroup"), Session("CVarIGdp"), Session("CVarIPop"), Session("OrigTemp"), _
                    'Session("IGdpOrig"), Session("IPopOrig"), Session("OrigDep"), Session("OrigIndFir"), _
                    'Session("OrigIndSec"), Session("CVarI"), Session("CVarISec"), Session("CVarD"), _
                    'Session("IFs_MDB2Active"))



                End If

                If Session("OrigAgg")(1) <> "" Then
                    '                        frm_ScatVarS.AssignVal(OrigIndFir, GlistCount, 0, False, OrigAgg(1), "O")
                    AssignVal(Session("OrigIndFir"), Session("GlistCount"), 0, False, Session("OrigAgg")(1), "O")

                    'AssignVal(Session("OrigIndFir"), Session("GlistCount"), _
                    '0, False, Session("OrigAgg")(1), "O", _
                    'Session("TotCountries"), Session("IsaGlist"), Session("GlistCount"), Session("GListMember"), _
                    'Session("GListIsGroup"), Session("CVarIGdp"), Session("CVarIPop"), Session("OrigTemp"), _
                    'Session("IGdpOrig"), Session("IPopOrig"), Session("OrigDep"), Session("OrigIndFir"), _
                    'Session("OrigIndSec"), Session("CVarI"), Session("CVarISec"), Session("CVarD"), _
                    'Session("IFs_MDB2Active"))

                End If

                If Session("OrigAgg")(2) <> "" Then
                    AssignVal(Session("OrigIndSec"), Session("GlistCount"), 1, False, Session("OrigAgg")(2), "O")

                    'AssignVal(Session("OrigIndSec"), Session("GlistCount"), _
                    '1, False, Session("OrigAgg")(2), "O", _
                    'Session("TotCountries"), Session("IsaGlist"), Session("GlistCount"), Session("GListMember"), _
                    'Session("GListIsGroup"), Session("CVarIGdp"), Session("CVarIPop"), Session("OrigTemp"), _
                    'Session("IGdpOrig"), Session("IPopOrig"), Session("OrigDep"), Session("OrigIndFir"), _
                    'Session("OrigIndSec"), Session("CVarI"), Session("CVarISec"), Session("CVarD"), _
                    'Session("IFs_MDB2Active"))
                End If


                AssignVal(Session("IGdpOrig"), Session("GlistCount"), 2, False, Session("GdpAggRule"), "G")

                'AssignVal(Session("IGdpOrig"), Session("GlistCount"), _
                '2, False, Session("GdpAggRule"), "G", _
                'Session("TotCountries"), Session("IsaGlist"), Session("GlistCount"), Session("GListMember"), _
                'Session("GListIsGroup"), Session("CVarIGdp"), Session("CVarIPop"), Session("OrigTemp"), _
                'Session("IGdpOrig"), Session("IPopOrig"), Session("OrigDep"), Session("OrigIndFir"), _
                'Session("OrigIndSec"), Session("CVarI"), Session("CVarISec"), Session("CVarD"), _
                'Session("IFs_MDB2Active"))

                AssignVal(Session("IPopOrig"), Session("GlistCount"), 3, False, Session("PopAggRule"), "P")

                'AssignVal(Session("IPopOrig"), Session("GlistCount"), _
                '3, False, Session("PopAggRule"), "P", _
                'Session("TotCountries"), Session("IsaGlist"), Session("GlistCount"), Session("GListMember"), _
                'Session("GListIsGroup"), Session("CVarIGdp"), Session("CVarIPop"), Session("OrigTemp"), _
                'Session("IGdpOrig"), Session("IPopOrig"), Session("OrigDep"), Session("OrigIndFir"), _
                'Session("OrigIndSec"), Session("CVarI"), Session("CVarISec"), Session("CVarD"), _
                'Session("IFs_MDB2Active"))


                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'JAM 2010-06-01 Filling Variables used in graph    
                For i = 1 To Session("GlistCount")

                    'Session("CVarI")(i) = Session("SCVarIGlob")(i, 0)
                    'k = Session("SCVarIGlob")(i, 0)
                    'If Session("ThirdDimOk") Then
                    '    Session("CVarISec")(i) = Session("SCVarIGlob")(i, 1)
                    'End If
                    'Session("CVarD")(i) = Session("SCVarDGlob")(i)
                    'k = Session("SCVarDGlob")(i)

                    Session("CVarName")(i) = CStr(Session("GListMember")(i))

                    Session("CVarCode")(i) = CStr(Session("GListMember")(i))
                Next i

                'JAM 2017-01-28  Keeping Original GList Value
                Session("GlistCount") = Session("GlistCountChanged")
                Session("CCount") = Session("GlistCount")
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                ''JAM 2009-07-26

                'If IsMoveYear Then
                '    mnuAddGList.Enabled = True
                '    'JAM 2009-10-21
                '    vAddYear = False
                '    'JAM 2009-11-07  For Cohorts Variables Years selection isn't valid
                '    If Not IsCohDep And Not IsCoh1Ind And Not IsCoh2Ind Then
                '        mnuAddYear.Enabled = True
                '    End If
                'Else
                '    mnuAddGList.Enabled = False
                '    'JAM 2009-10-21
                '    vAddYear = False
                '    mnuAddYear.Enabled = False
                'End If
                'mnuAddGrp.Enabled = False
                ''JAM 2009-10-06 Changing Totmulgroups conditional
                'If Not IsCohDep And Not IsCoh1Ind And Not IsCoh2Ind And Not AddGlistMode Then 'TotMulGroups = 1 Then
                '    mnuDynamic.Enabled = True
                'End If




                'Else

                '    If Not IsaGroup Then


                '        CCount = TotCountries
                '        If OrigAgg(0) <> "" Then
                '            frm_ScatVarS.AssignVal(OrigDep, TotCountries, 0, True, OrigAgg(0), "O")
                '        End If

                '        If OrigAgg(1) <> "" Then
                '            frm_ScatVarS.AssignVal(OrigIndFir, TotCountries, 0, False, OrigAgg(1), "O")
                '        End If

                '        If OrigAgg(2) <> "" Then
                '            frm_ScatVarS.AssignVal(OrigIndSec, TotCountries, 1, False, OrigAgg(2), "O")
                '        End If
                '        frm_ScatVarS.AssignVal(IGdpOrig, TotCountries, 2, False, GdpAggRule, "G")
                '        frm_ScatVarS.AssignVal(IPopOrig, TotCountries, 3, False, PopAggRule, "P")

                '        For LineSet% = 1 To TotCountries
                '            CVarName$(LineSet%) = CVarCounName$(LineSet%)
                '            CVarCode$(LineSet%) = CVarCounCode$(LineSet%)
                '            CVarI(LineSet%) = OrigIndFir(LineSet%)
                '            CVarD(LineSet%) = OrigDep(LineSet%)
                '            'JAM 2008-04-06
                '            If ThirdDimOk Then
                '                CVarISec(LineSet%) = OrigIndSec(LineSet%)
                '            End If
                '            CVarIPop(LineSet%) = IPopOrig(LineSet%)
                '            CVarIGdp(LineSet%) = IGdpOrig(LineSet%) 'Code Added by Jaime
                '        Next LineSet%
                '    Else 'JAM 2008-08-23 CCount reassigned in Group Mode
                '        CCount = GlistCount
                '    End If
            End If
        End If

        'grf_Scatter.Points = CCount

        'For II% = 1 To TotCountries
        '    ShowLabels(II%) = True
        '    ShowPoints(II%) = True
        'Next II%

        ''JAM 2007-11-04 Varaibles Initialization
        'If TraceMode Then
        '    Call InitTrace()
        'End If


        ''JAM 2010-02-07
        'Call DisMovieOp()

        ''JAM 2009-07-15  Code added to graph before Filling Array
        'If Not AddGlistMode Then

        '    Call GraphData()

        'End If

        'jrs 2008/03/10
        'frm_ScatVarS.FillLocalData()
        Exit Sub
PIsaGlist_Error:
        ' MsgBox("Error PIsaGlist() routine")
        Exit Sub


    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2010-07-11 Group Option Definition       
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub PIsaGroup()
        On Error GoTo PIsaGroup_Error 'JAM 2016-11-24


        Dim i, j As Integer







        'JAM 2010-03-07 We recalculate only the group has changed
        If Session("GChanged") Then
            ''JAM 2009-09-23    Loading Year Selected
            'If AddGlistMode Then
            '    Call AddYear(GLbLagDep(YearChosen), GLbLagInd(YearChosen), LagSecInd(YearChosen))
            'End If

            ''JAM 2009-10-06 Enabling Navigation Buttons
            'cmdNext.Enabled = True
            'cmdPrevious.Enabled = True

            If Session("IsaGlist") And Session("IsaGroup") Then
                'JAM 2008-08-24
                'If FilterReq = 1 Then
                '    FilterVal(OrigDep, OrigIndFir)
                '    FilterReq = FilterReq + 1
                'End If
                For i = 1 To Session("TotCountries")
                    Session("CVarIPop")(i) = Session("IPopOrig")(i)
                    Session("CVarIGdp")(i) = Session("IGdpOrig")(i)
                    'CVarCounName(i) = Session("CVarName")(i)
                Next i

                'JAM 2017-01-28  Keeping Original GList Value
                Session("GlistCountChanged") = Session("GlistCount")
                Session("GlistCount") = Session("GlistCountChosen")
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Session("CCount") = Session("GlistCount")




                j = 1
                For i = 1 To Session("GlistCount")
                    For j = 1 To Session("TotCountries")
                        If Session("CVarCounName")(j) = Session("GListMember")(i) Then
                            Exit For
                        End If
                    Next j
                    Session("CVarName")(i) = Session("GListMember")(i)
                    Session("CVarI")(i) = Session("OrigIndFir")(j)
                    Session("CVarCode")(i) = Session("GListMember")(i)
                    Session("CVarD")(i) = Session("OrigDep")(j)
                    If Session("ThirdDimOk") Then
                        Session("CVarISec")(i) = Session("OrigIndSec")(j)
                    End If
                    Session("CVarIPop")(i) = Session("IPopOrig")(j)
                    Session("CVarIGdp")(i) = Session("IGdpOrig")(j)
                    j = j + 1
                Next i
                i = 0

                'JAM 2017-01-28  Keeping Original GList Value
                Session("GlistCount") = Session("GlistCountChanged")
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Session("CCount") = Session("GlistCount")

                'grf_Scatter.Points = GlistCount

                'For II% = 1 To TotCountries
                '    ShowLabels(II%) = True
                '    ShowPoints(II%) = True
                'Next II%

            End If
        End If

        Exit Sub
PIsaGroup_Error:
        'MsgBox("Error PIsaGroup() routine")
        Exit Sub

    End Sub

    '''''''''''''''''''''''''''''''''''
    'JAM 2006/09/10 Function created by Jaime and is used
    'for PostProcessing data
    'ValArray: Array original that We'll operate it
    'Totar: Number of elements that we'll use to calculate
    'indexVAR: Number that indicates the order number for the variable
    'isDEpendent: flag that indicates if the variable is dependent or independent
    'sAgg: The rule agreggation for the array data
    'SecInType : Flag that fill different array depending if it contains data about dependent/independent or GDP or Population
        Public Sub AssignVal(ByRef ValArray As Object, ByVal TotVar As Integer, _
    ByVal indexVar As Integer, ByVal IsDependent As Boolean, ByVal sAgg As String, ByVal SecInType As String)


        On Error GoTo AssignVal_Error 'JAM 2016-11-24

        ReDim Session("AuxArray")(Session("TotCountries"))
        ReDim Session("OrigTemp")(Session("TotCountries"))

        Dim IndIndexOrig, i, VarCount, iCount As Integer



        IndIndexOrig = IndIndex
        If SecInType = "O" And indexVar <= 1 Then
            IndIndex = indexVar
        End If
        Session("CCount") = TotVar
        If Session("IsaGlist") Then



            'jrs 2007/08/18
            Dim ForIdx As Integer
            If IsDependent Then
                ForIdx = 1
            ElseIf indexVar < 2 Then
                ForIdx = indexVar + 2
            Else
                ForIdx = 0
            End If

            Dim a As Integer
            Dim arrayc(200)

            ' AssignVal(Session("OrigDep"), Session("GlistCount"), 0, True, Session("OrigAgg")(0), "O")
            a = Session("TotCountries")
            a = TotVar
            arrayc(0) = sAgg
            For a = 1 To Session("TotCountries")
                arrayc(a) = ValArray(a)

            Next a
            arrayc(0) = SecInType
            For a = 1 To Session("GlistCount")
                arrayc(a) = Session("GListMember")(a)
                arrayc(a + 10) = Session("GListIsGroup")(a)
            Next a

            'JAM 2010-03-20 First Version will be only AggVal
            If Not Session("bCompSVtmp")(ForIdx) Then
                AggVal(ValArray, Session("TotCountries"), Session("GlistCount"), Session("GListMember"), Session("GListIsGroup"), sAgg, SecInType)
            Else
                AggValComp(Session("aFormulas")(ForIdx), ValArray, Session("TotCountries"), Session("GlistCount"), Session("GListMember"), Session("GListIsGroup"), SecInType)
            End If
            Session("CCount") = Session("GlistCount")

            Dim am

            If SecInType = "G" Then
                For i = 1 To Session("CCount")
                    Session("AuxArray")(i) = Session("CVarIGdp")(i)
                Next i
            Else
                If SecInType = "P" Then
                    For i = 1 To Session("CCount")
                        Session("AuxArray")(i) = Session("CVarIPop")(i)
                    Next i
                Else
                    If SecInType = "O" Then
                        For i = 1 To Session("CCount")

                            am = Session("OrigTemp")(i)

                            Session("AuxArray")(i) = Session("OrigTemp")(i)
                        Next i
                    End If
                End If
            End If


        Else
            Session("CCount") = Session("TotCountries")



            If SecInType = "G" Then
                For i = 1 To Session("CCount")
                    Session("AuxArray")(i) = Session("IGdpOrig")(i)
                Next i
            Else
                If SecInType = "P" Then
                    For i = 1 To Session("CCount")
                        Session("AuxArray")(i) = Session("IPopOrig")(i)
                    Next i
                Else
                    If SecInType = "O" Then
                        If (indexVar = 0) And (IsDependent = True) Then
                            For i = 1 To Session("CCount")
                                Session("AuxArray")(i) = Session("OrigDep")(i)
                            Next i
                        Else
                            If (indexVar <= 1) And (IsDependent = False) Then
                                If indexVar = 0 Then
                                    For i = 1 To Session("CCount")
                                        Session("AuxArray")(i) = Session("OrigIndFir")(i)
                                    Next i
                                Else
                                    For i = 1 To Session("CCount")
                                        Session("AuxArray")(i) = Session("OrigIndSec")(i)
                                    Next i
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If



        Dim ax, bx
        If SecInType = "O" Then


            For VarCount = 1 To Session("CCount")
                If IsDependent = False Then 'Working on independent
                    'JAM 2008-08-05 Fixing rewriting first Ind. Variable, adding Conditional
                    If indexVar <> 1 Then
                        ax = Session("AuxArray")(VarCount)
                        Session("CVarI")(VarCount) = Session("AuxArray")(VarCount)
                    Else
                        Session("CVarISec")(VarCount) = Session("AuxArray")(VarCount)
                    End If


                Else
                    Session("CVarD")(VarCount%) = Session("AuxArray")(VarCount%)
                    bx = Session("CVarD")(VarCount%)
                End If

            Next VarCount%

            'JRS 9/16/01 Changes to add Computations
            If Session("bComputation") Then

                For iCount = 1 To Session("CCount")
                    Session("PLOTComp")(iCount) = Session("AuxArray")(iCount)
                Next iCount

            Else
                'JAM Analizar
                'FillLocalData()

            End If


            IndIndex = IndIndexOrig
            Exit Sub


        End If

        IndIndex = IndIndexOrig

        Exit Sub
AssignVal_Error:
        'MsgBox("Error AssignVal_click() routine")
        Exit Sub



    End Sub
    'JAM 2010-03-08 Function developed by JRS 
    'jrs 2006/08/24
    'Routine to Aggregate Values (Group Calculation)
    'The members of the G-LIst can be read from IFs.mdb, table G-List, and
    'you 'll see the Group Column there too.
    Public Sub AggVal(ByRef OrgArray As Object, ByRef OrgCounter As Integer, ByVal NewCounter As Integer, _
    ByRef MemberArray As Object, ByRef MemberisGroupArray As Object, ByVal sAgg As String, ByVal IndType As String)



        'Parameters:
        '1. OrgArray() is the array containing values for all countries
        '2. OrgCounter is the number of countries in the array
        '3. NewCounter number of members of the G-List
        '4. MemberArray() is the array containing the names of all the Groups/Regions-Countries members of the G-List
        '5. MemberisGroupArray(), this array specifies if the corresponding member in MemberArray is a Group or NOT.
        '6. sAgg is the Aggregation Rule for the Variable selected

        ' Declare an Array to Store Results
        ' ---------------------------------
        On Error GoTo GroupError

        ReDim Session("ResultArray")(NewCounter)





        ' Loop through all Members of the G-List
        ' --------------------------------------
        Dim i, aa, indexGr As Integer, RegIdx As Integer, bRegions As Boolean

        Dim onlyNull As Boolean


        Dim b As String

        For i = 1 To NewCounter



            If i = 5 Then
                b = i
            End If



            'check if region has countries in it
            'and load members
            If MemberisGroupArray(i) Then

                RegIdx = gGetGroupIdx(MemberArray(i), True, Me)


                'gLoadGroupMembersHis(sDim1, GroupCodes(idxDim1), gCountGroupMembers, gGroupMembrs, IfsParameters.Ifs_MDB2Active)
                gLoadGroupMembersHis(CStr(MemberArray(i)), GroupCodes(RegIdx), Session("gCountGroupMembers"), Session("gGroupMembrs"), Session("IFs_MDB2Active"))

            Else
                RegIdx = gGetDimIdx(1, MemberArray(i), True, Me)

                gLoadRegionMembersHis(CStr(MemberArray(i)), RegIdx, Session("gCountGroupMembers"), Session("gGroupMembrs"), Session("IFs_MDB2Active"))


                bRegions = (Session("gCountGroupMembers") > 1)
            End If

            'JAM 2010-03-20 Code added for Group with members with null data
            onlyNull = False



            For aa = 1 To Session("gCountGroupMembers")


                'JAM 2010-04-18 Object returns a value -1 that real position
                'jrs 2012/05/01 not anymore so I removed the + 1 
                indexGr = (IFsCtrys.Item(Session("gGroupMembrs")(aa)).Index) '+ 1


                Session("CountryRegress")(indexGr) = True

                If Not IsDBNull(OrgArray(indexGr)) Then
                    onlyNull = True
                    'Exit For
                End If
            Next aa


            If Not onlyNull Then
                Session("ResultArray")(i) = DBNull.Value
            Else
                'If it's not a GROUP just copy data
                If Not MemberisGroupArray(i) And Not bRegions Then

                    RegIdx = IFsCtrys.Item(Session("gGroupMembrs")(1)).Index
                    Session("ResultArray")(i) = OrgArray(RegIdx)
                    'If it's a GROUP do aggregation
                Else
                    Session("ResultArray")(i) = 0
                    Dim k As Integer, Aux As Single 'jrs 2008/08/27 changed from integer to single
                    Aux = 0

                    For k = 1 To Session("gCountGroupMembers")
                        'JAM 2010-04-18 Object returns a value -1 that real position
                        'jrs 2012/05/01 not anymore so I removed the + 1 
                        RegIdx = (IFsCtrys.Item(Session("gGroupMembrs")(k)).Index) '+ 1
                        Select Case sAgg
                                'jrs 2008/08/27 added and Not is empty
                            Case "SUM"
                                If IsNumeric(OrgArray(RegIdx)) And Not (OrgArray(RegIdx) Is Nothing) Then
                                    Session("ResultArray")(i) = Session("ResultArray")(i) + OrgArray(RegIdx)
                                End If
                            Case "AVG"
                                If IsNumeric(OrgArray(RegIdx)) And Not (OrgArray(RegIdx) Is Nothing) Then
                                    Session("ResultArray")(i) = Session("ResultArray")(i) + OrgArray(RegIdx)
                                    Aux = Aux + 1
                                End If
                            Case "GDP"
                                If IsNumeric(OrgArray(RegIdx)) And Not (OrgArray(RegIdx) Is Nothing) Then
                                    Session("ResultArray")(i) = Session("ResultArray")(i) + OrgArray(RegIdx) * Session("CVarIGdp")(RegIdx)
                                    Aux = Aux + Session("CVarIGdp")(RegIdx)
                                End If
                            Case "POP"
                                If IsNumeric(OrgArray(RegIdx)) And Not (OrgArray(RegIdx) Is Nothing) Then
                                    Session("ResultArray")(i) = Session("ResultArray")(i) + OrgArray(RegIdx) * Session("CVarIPop")(RegIdx)
                                    Aux = Aux + Session("CVarIPop")(RegIdx)
                                End If
                            Case "LND"
                        End Select

                    Next k
                    If Aux <> 0 Then
                        Session("ResultArray")(i) = Session("ResultArray")(i) / Aux
                    End If
                End If

            End If



        Next i




        If IndType = "I" Then
            For i = 1 To NewCounter
                Session("NewVal")(i) = Session("ResultArray")(i)
            Next i
        Else
            If IndType = "G" Then
                For i = 1 To NewCounter
                    Session("CVarIGdp")(i) = Session("ResultArray")(i)
                Next i
            Else
                If IndType = "P" Then
                    For i = 1 To NewCounter
                        Session("CVarIPop")(i) = Session("ResultArray")(i)
                    Next i
                Else
                    If IndType = "O" Then
                        For i = 1 To NewCounter
                            Session("OrigTemp")(i) = Session("ResultArray")(i)

                        Next i
                    End If
                End If
            End If
        End If

        Exit Sub

GroupError:
        'Screen.MousePointer = 0
        'Msg$ = "The Group doesn't exist.  Please try another."
        'MsgBox$ Msg$
        'Call mnuCountries_Click()
        'frm_ScatVarS.mnuCountries.Checked = False
        'frm_ScatVarS.mnucountry.Checked = False
        'frm_ScatVarS.mnuCodes.Checked = False
        'frm_ScatVarS.mnuNolabels.Checked = False
        'frm_ScatVarS.mnuGeographic.Checked = False
        Exit Sub


    End Sub
    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'JAM 2010-03-07 Function developed by JRS
    'jrs 2007/08/17
    'Routine to Aggregate Values using Formulas (Group Calculation)
    'Public Sub AggValComp(ByVal f As Formula, ByRef OrgArray As Object, ByRef OrgCounter As Integer, ByVal NewCounter As Integer, _
    'ByRef MemberArray As Object, ByRef MemberisGroupArray As Object, ByVal IndType As String)
    '    'Parameters:
    '    '1. f is the formula to be grouped
    '    '2. OrgArray is the Array that contains the original values for all countries
    '    '3. OrgCounter is the number of countries in the array
    '    '4. NewCounter number of members of the G-List
    '    '5. MemberArray() is the array containing the names of all the Groups/Regions-Countries members of the G-List
    '    '6. MemberisGroupArray(), this array specifies if the corresponding member in MemberArray is a Group or NOT.
    '    '7. IndType indicates where to copy the results

    '    ' Declare an Array to Store Results
    '    ' ---------------------------------
    '    Dim ResultArray()
    '    ReDim ResultArray(NewCounter)
    '    Dim Result As Single
    '    Dim aa, indexGr As Integer


    '    ' Loop through all Members of the G-List
    '    ' --------------------------------------
    '    Dim i As Integer, RegIdx As Integer, bRegions As Boolean
    '    Dim onlyNull As Boolean

    '    For i = 1 To NewCounter
    '        'check if region has countries in it
    '        'and load members
    '        If MemberisGroupArray(i) Then
    '            RegIdx = gGetGroupIdx(MemberArray(i), True, Me)
    '            'gLoadGroupMembersHis(sDim1, GroupCodes(idxDim1), gCountGroupMembers, gGroupMembrs, IfsParameters.Ifs_MDB2Active)
    '            gLoadGroupMembersHis(CStr(MemberArray(i)), GroupCodes(RegIdx))
    '        Else
    '            RegIdx = gGetDimIdx(1, MemberArray(i), True, Me)
    '            'gLoadRegionMembersHis(ByVal regionname As String, ByVal idxRegion As Integer, ByRef gCountGroupMembers As Integer, _
    '            '              ByRef gGroupMembrs() As String, ByRef IFs_MDB As String)
    '            gLoadRegionMembersHis(CStr(MemberArray(i)), RegIdx)
    '            bRegions = (gCountGroupMembers > 1)
    '        End If

    '        'JAM 2007-01-06 Code added for Group with members with null data
    '        onlyNull = False
    '        For aa = 1 To gCountGroupMembers
    '            indexGr = IFsCtrys.Item(gGroupMembrs(aa)).Index
    '            'JAM 2007-06-18 Code added for countries members of Groups
    '            CountryRegress(indexGr) = True
    '            If Not IsDBNull(OrgArray(indexGr)) Then
    '                onlyNull = True
    '                'Exit For
    '            End If
    '        Next aa

    '        If Not onlyNull Then
    '            ResultArray(i) = Null
    '        Else
    '            'If it's not a GROUP just copy data
    '            If Not MemberisGroupArray(i) And Not bRegions Then
    '                RegIdx = IFsCtrys.Item(gGroupMembrs(1)).Index
    '                ResultArray(i) = OrgArray(RegIdx)
    '                'If it's a GROUP do aggregation
    '            Else
    '                'jrs 2008/09/01
    '                'added ExcludeCtry for filtering

    '                'New function with Year specifics, Hist. Empirical, and Target Array
    '                gLoadCompHisEmp(Result, MemberArray(i), MemberisGroupArray(i), f, 0, True, ExcludeCtry)

    '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '                'JAM Question 7: Function gLoadCompHisEmp gLoadCompHisEmp doesn't exist


    '                'Public Sub gLoadCompHisEmp(ByRef Result As Single, ByVal sDim1 As String, ByRef FlagGroup As Object, _
    '                'ByRef ff As Formula, ByVal iYearAdv As Integer, ByVal bFilterCtry As Boolean, ByRef FilterCtry As Object)
    '                '    '1. Result stores the resulting value
    '                '2. sDim1 is the First Dimension, if empty is read from the Variable saved
    '                '3. FlagGroup is True when Dimension1 is a group, in the future will need a FlagGroup for Dim2 and Dim3, if null is passed the parameter is changed to true if Groups are found
    '                '4. ff is the Formula to be used when not saved in Display List
    '                '5. iYearAdv is the number of years to advance from the stored year (it can be negative).
    '                'jrs 2008/09/01
    '                'These options are used from Cross-Sectional, where ctries in X and Y are not selected
    '                'simultaneously and need to be matched
    '                '6. bFilterCtry is true if we need to filter members of a group (not all members to be used)
    '                '7. FilterCtry is an array of all ctries, if element is true, the ctry is to be skipped/filtered


    '                ResultArray(i) = Result
    '            End If
    '        End If
    '    Next i


    '    If IndType = "I" Then
    '        For i = 1 To NewCounter
    '            Session("NewVal")(i) = ResultArray(i)
    '        Next i
    '    Else
    '        If IndType = "G" Then
    '            For i = 1 To NewCounter
    '                Session("CVarIGdp")(i) = ResultArray(i)
    '            Next i
    '        Else
    '            If IndType = "P" Then
    '                For i = 1 To NewCounter
    '                    Session("CVarIPop")(i) = ResultArray(i)
    '                Next i
    '            Else
    '                If IndType = "O" Then
    '                    For i = 1 To NewCounter
    '                        Session("OrigTemp")(i) = ResultArray(i)
    '                    Next i
    '                End If
    '            End If
    '        End If
    '    End If


    'End Sub

    'jrs 2010/09/07 transalation to the web
    'jrs 2007/08/17
    'Routine to Aggregate Values using Formulas (Group Calculation)
        Public Sub AggValComp(ByVal f As Formula, ByRef OrgArray() As Object, ByRef OrgCounter As Integer, ByVal NewCounter As Integer, _
    ByRef MemberArray As Object, ByRef MemberisGroupArray As Object, ByVal IndType As String)

        On Error GoTo AggValComp_Error 'JAM 2016-11-24

        'Parameters:
        '1. f is the formula to be grouped
        '2. OrgArray is the Array that contains the original values for all countries
        '3. OrgCounter is the number of countries in the array
        '4. NewCounter number of members of the G-List
        '5. MemberArray() is the array containing the names of all the Groups/Regions-Countries members of the G-List
        '6. MemberisGroupArray(), this array specifies if the corresponding member in MemberArray is a Group or NOT.
        '7. IndType indicates where to copy the results

        ' Declare an Array to Store Results
        ' ---------------------------------
        Dim ResultArray()
        ReDim ResultArray(NewCounter)
        Dim Result As Object
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"), _
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"), _
        Session("PutFdrvRun"), Session("CurrencySym"))

        ' Loop through all Members of the G-List
        ' --------------------------------------
        Dim i As Integer, RegIdx As Integer, bRegions As Boolean
        Dim onlyNull As Boolean

        For i = 1 To NewCounter
            'check if region has countries in it
            'and load members
            If MemberisGroupArray(i) Then
                RegIdx = gGetGroupIdx(MemberArray(i), True, Me)
                gLoadGroupMembersHis(CStr(MemberArray(i)), GroupCodes(RegIdx), Session("gCountGroupMembers"), Session("gGroupMembrs"), Session("IFs_MDB2Active"))
            Else
                RegIdx = gGetDimIdx(1, MemberArray(i), True, Me)
                gLoadRegionMembersHis(CStr(MemberArray(i)), RegIdx, Session("gCountGroupMembers"), Session("gGroupMembrs"), Session("IFs_MDB2Active"))
                bRegions = (Session("gCountGroupMembers") > 1)
            End If

            'JAM 2007-01-06 Code added for Group with members with null data
            onlyNull = False
            Dim aa As Integer, indexGr As Integer
            For aa = 1 To Session("gCountGroupMembers")
                indexGr = IFsCtrys.Item(Session("gGroupMembrs")(aa)).Index
                'JAM 2007-06-18 Code added for countries members of Groups
                Session("CountryRegress")(indexGr) = True
                If Not IsDBNull(OrgArray(indexGr)) Then
                    onlyNull = True
                    'Exit For
                End If
            Next aa

            If Not onlyNull Then
                ResultArray(i) = System.DBNull.Value
            Else
                'If it's not a GROUP just copy data
                If Not MemberisGroupArray(i) And Not bRegions Then
                    RegIdx = IFsCtrys.Item(Session("gGroupMembrs")(1)).Index
                    ResultArray(i) = OrgArray(RegIdx)
                    'If it's a GROUP do aggregation
                Else
                    'jrs 2008/09/01
                    'added ExcludeCtry for filtering

                    'New function with Year specifics, Hist. Empirical, and Target Array
                    gLoadCompHisEmp(Result, MemberArray(i), MemberisGroupArray(i), f, 0, True,
                    Session("ExcludeCtry"), True, Session("FirstYear"), Session("LastYear"),
                    Session("LastYearForecast"), Session("aFormulas"), Me, IfsPars, IFsCtrys,
                    Session("bFillHisOnlyHoles"), Session("mnuSQLite"))
                    ResultArray(i) = Result
                End If
            End If
        Next i

        ' Code Added by Jam 2006/09/03
        If IndType = "I" Then
            For i = 1 To NewCounter
                Session("NewVal")(i) = ResultArray(i)
            Next i
        Else
            If IndType = "G" Then
                For i = 1 To NewCounter
                    Session("CVarIGdp")(i) = ResultArray(i)
                Next i
            Else
                If IndType = "P" Then
                    For i = 1 To NewCounter
                        Session("CVarIPop")(i) = ResultArray(i)
                    Next i
                Else
                    If IndType = "O" Then
                        For i = 1 To NewCounter
                            Session("OrigTemp")(i) = ResultArray(i)
                        Next i
                    End If
                End If
            End If
        End If

        Exit Sub
AggValComp_Error:
        'MsgBox("Error AggValComp() routine")
        Exit Sub

    End Sub

    Private Function getYAxisLabel() As String

        On Error GoTo getYAxisLabel_Error 'JAM 2016-11-24
        If Session("YAxisLabel") <> "" Then
            getYAxisLabel = Session("YAxisLabel")
        ElseIf Session("GType") = "SCAT" Then
            getYAxisLabel = Session("LV_lbl_Dep")
        Else
            getYAxisLabel = Session("LV_sUnits")
        End If

        Exit Function
getYAxisLabel_Error:
        'MsgBox("Error getYAxisLabel() routine")
        Exit Function

    End Function

    Private Function getXAxisLabel() As String

        On Error GoTo getXAxisLabel 'JAM 2016-11-24

        If Session("XAxisLabel") > "" Then
            getXAxisLabel = Session("XAxisLabel")
        ElseIf Session("GType") = "SCAT" Then
            getXAxisLabel = Session("LV_lbl_Ind")
        Else
            getXAxisLabel = "Year"
        End If

        Exit Function
getXAxisLabel:
        'MsgBox("Error getYAxisLabel() routine")
        Exit Function

    End Function

    Private Function getZAxisVar() As String

        On Error GoTo getZAxisVar_Error 'JAM 2016-11-24

        '// Set the PlottingMethod to bubble in case that third dimension is filled//
        If Session("bBubble") Then
            If Session("mnuOtherChecked") Then
                getZAxisVar = Session("VarNameI")(1)
            Else
                If Session("mnuPopulationChecked") Then
                    getZAxisVar = "Population(MOSTRECENT)"
                Else
                    If Session("mnuGdpChecked") Then
                        getZAxisVar = "GDP2000(MOSTRECENT)"
                    Else
                        getZAxisVar = ""
                    End If
                End If
            End If
        End If



        Exit Function
getZAxisVar_Error:
        'MsgBox("Error getZAxisVar() routine")
        Exit Function

    End Function

    Private Function PlotPairToJSSeries(ByVal CCount As Integer) As String

        On Error GoTo PlotPairToJSSeries_Error 'JAM 2016-11-24

        Dim idx As Integer = Session("DisYear")
        Dim iNumDecY As Integer = 0

        'jam 2023/07/31
        Dim PointsQty As Integer

        PointsQty = 0
        If IsNumeric(Session("CVarD")(idx)) Then
            If Math.Abs(Session("CVarD")(idx)) < 1 Then
                iNumDecY = 3
            ElseIf Math.Abs(Session("CVarD")(idx)) < 10 Then
                iNumDecY = 2
            ElseIf Math.Abs(Session("CVarD")(idx)) < 100 Then
                iNumDecY = 1
            End If
        Else
            iNumDecY = 2
        End If

        Dim iNumDecX As Integer = 0
        If IsNumeric(Session("CVarI")(idx)) Then
            If Math.Abs(Session("CVarI")(idx)) < 1 Then
                iNumDecX = 3
            ElseIf Math.Abs(Session("CVarI")(idx)) < 10 Then
                iNumDecX = 2
            ElseIf Math.Abs(Session("CVarI")(idx)) < 100 Then
                iNumDecX = 1
            End If
        Else
            iNumDecX = 2
        End If

        Dim iNumDecZ As Integer = 0, bSize As Single = 0.1
        If Session("bBubble") And Session("mnuOtherChecked") Then
            If IsNumeric(Session("CVarISec")(idx)) Then
                If Math.Abs(Session("CVarISec")(idx)) < 1 Then
                    iNumDecZ = 3
                ElseIf Math.Abs(Session("CVarISec")(idx)) < 10 Then
                    iNumDecZ = 2
                ElseIf Math.Abs(Session("CVarISec")(idx)) < 100 Then
                    iNumDecZ = 1
                End If
            Else
                iNumDecZ = 2
            End If
        End If


        Dim sJscript As String = "", sdataLabelsEnabled As String = ""
        If Session("IsaGList") And Session("TotMulGroups") > 1 And Session("RegressType") <> "" Then  'JAM 2017-01-02
            If Session("IsaGList") And Not Session("IsaGroup") Then
                sJscript = sJscript & "{name: "" ""," & vbCr 'JAM "017-01-08 Geographic
            Else
                sJscript = sJscript & "{name: "" ""," & vbCr 'JAM "017-01-08 Groups
            End If
        Else
            sJscript = sJscript & "{name: ""Scatter Name""," & vbCr
        End If

        sJscript = sJscript & "dataLabels: {" & vbCr
        If Session("mnuNamesChecked") Or Session("mnuCodesChecked") Then
            sJscript = sJscript & "enabled: true," & vbCr
            sdataLabelsEnabled = "enabled: true"
        Else
            sJscript = sJscript & "enabled: false," & vbCr
            sdataLabelsEnabled = "enabled: false"
        End If
        sJscript = sJscript & "format: '{point.name}'" & vbCr
        sJscript = sJscript & "}," & vbCr
        sJscript = sJscript & "data: ["

        Dim j As Integer
        Dim bFirst As Boolean = True
        Dim sColorRGB As String = "", sName As String = ""
        For j = 1 To CCount
            If IsNumeric(Session("CVarI")(j)) And IsNumeric(Session("CVarD")(j)) Then
                If Session("bBubble") Then
                    bSize = 0.1
                    If Session("mnuOtherChecked") And IsNumeric(Session("CVarISec")(j)) Then
                        bSize = Session("CVarISec")(j)
                    Else
                        If Session("mnuPopulationChecked") And IsNumeric(Session("CVarIPop")(j)) Then
                            bSize = Session("CVarIPop")(j)
                        Else
                            If Session("mnuGdpChecked") And IsNumeric(Session("CVarIGdp")(j)) Then
                                bSize = Session("CVarIGdp")(j)
                            End If
                        End If
                    End If
                End If

                If Session("mnuCodesChecked") Then
                    If Session("ShowLabels")(j) Then 'JAM 2019-02-17 
                        sdataLabelsEnabled = "enabled: true"
                    Else
                        sdataLabelsEnabled = "enabled: false"
                    End If
                    sName = Session("CVarCode")(j) 'jrs 2012/08/14 "Get FIPS_CODE"
                Else
                    If Session("ShowLabels")(j) Then 'JAM 2019-02-17 

                        sdataLabelsEnabled = "enabled: true"
                    Else
                        sdataLabelsEnabled = "enabled: false"
                    End If
                    sName = Session("CVarName")(j)
                End If
                'jam 2023/08/02
                If Not bFirst And PointsQty <> 0 Then
                    sJscript = sJscript & ","
                Else
                    bFirst = False
                End If

                'jam 2023/08/02
                sJscript = sJscript.Replace(",,", ",")



                sColorRGB = Session("GroupingColor")(j)
                If Session("TotMulGroups") = 1 Then 'JAM 2016-12-11
                    If Session("ShowPoints")(j) Then 'JAM 2019-02-10 Adding Showpoints Function
                        If Not Session("bBubble") Then
                            sJscript = sJscript & "{name: """ & sName &
                                """, y: " & CStr(Math.Round(Session("CVarD")(j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("CVarI")(j), iNumDecX)) &
                                ", fillColor: '" & sColorRGB & "', color: '" & sColorRGB & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorRGB & "'}}}}" & vbCr
                        Else
                            sJscript = sJscript & "{name: """ & sName &
                                    """, y: " & CStr(Math.Round(Session("CVarD")(j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("CVarI")(j), iNumDecX)) & ", z:" &
                                    CStr(Math.Round(bSize, iNumDecZ)) & ", fillColor: '" &
                                    sColorRGB & "', color: '" & sColorRGB & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorRGB & "'}}}}" & vbCr
                        End If
                        'jam 2023/07/31
                        PointsQty = PointsQty + 1
                    End If
                Else

                    'JAM 2016-12-12 Defining colors 
                    If j <= Session("SingleDimJoinPrev")(1) Then
                        sColorRGB = "#009600" 'Green
                    Else
                        If j > Session("SingleDimJoinPrev")(1) And j <= (Session("SingleDimJoinPrev")(1) + Session("SingleDimJoinPrev")(2)) Then
                            sColorRGB = "#000096" 'Blue
                        Else
                            If Session("TotMulGroups") > 2 Then
                                If j > (Session("SingleDimJoinPrev")(1) + Session("SingleDimJoinPrev")(2)) And j <= ((Session("SingleDimJoinPrev")(1) + Session("SingleDimJoinPrev")(2)) + Session("SingleDimJoinPrev")(3)) Then
                                    sColorRGB = "#ff0000" 'Red
                                End If
                                If Session("TotMulGroups") = 4 Then
                                    If j > ((Session("SingleDimJoinPrev")(1) + Session("SingleDimJoinPrev")(2)) + Session("SingleDimJoinPrev")(3)) And j <= (((Session("SingleDimJoinPrev")(1) + Session("SingleDimJoinPrev")(2)) + Session("SingleDimJoinPrev")(3)) + Session("SingleDimJoinPrev")(4)) Then
                                        sColorRGB = "#ffff00" 'Yellow
                                    Else
                                        sColorRGB = Session("GroupingColor")(j)
                                    End If

                                End If

                            End If
                        End If

                    End If

                    If Session("ShowPoints")(j) Then 'JAM 2019-02-17 Adding Showpoints Function
                        If Not Session("bBubble") Then
                            sJscript = sJscript & "{name: """ & sName &
                                """, y: " & CStr(Math.Round(Session("CVarD")(j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("CVarI")(j), iNumDecX)) &
                                ", fillColor: '" & sColorRGB & "', color: '" & sColorRGB & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorRGB & "'}}}}" & vbCr
                        Else
                            sJscript = sJscript & "{name: """ & sName &
                                    """, y: " & CStr(Math.Round(Session("CVarD")(j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("CVarI")(j), iNumDecX)) & ", z:" &
                                    CStr(Math.Round(bSize, iNumDecZ)) & ", fillColor: '" &
                                    sColorRGB & "', color: '" & sColorRGB & "', dataLabels: {" & sdataLabelsEnabled & "}, marker: {states: {hover: {fillColor: '" & sColorRGB & "'}}}}" & vbCr
                        End If
                        'jam 2023/07/31
                        PointsQty = PointsQty + 1
                    End If

                End If
            End If


        Next j



        '            }
        ',
        ']} 

        sJscript = sJscript & vbCr & "]"
        sJscript = sJscript & "} " & vbCr

        'jam 2023/08/06
        sJscript = sJscript.Replace("}" & vbCr & "," & vbCr & "]}", "}" & vbCr & "]}")


        PlotPairToJSSeries = sJscript

        Exit Function
PlotPairToJSSeries_Error:
        ' MsgBox("Error PlotPairToJSSeries() routine")
        Exit Function

    End Function

    Private Function PlotRegPairToJSSeries(ByVal CCount As Integer) As String

        On Error GoTo PlotRegPairToJSSeries_Error 'JAM 2016-11-24

        Dim sJscript As String = ""

        Dim j As Integer
        'Dim idx As Integer = Session("DisYear")
        Dim iNumDecX As Integer = 0
        Dim MulRegressName As String


        'JAM 2016-10-09 Adding Multiple Regression Graph
        If Session("TotMulGroups") = 1 Then
            If IsNumeric(Session("RegData")(1, 1)) Then
                If Math.Abs(Session("RegData")(1, 1)) < 1 Then
                    iNumDecX = 3
                ElseIf Math.Abs(Session("RegData")(1, 1)) < 10 Then
                    iNumDecX = 2
                ElseIf Math.Abs(Session("RegData")(1, 1)) < 100 Then
                    iNumDecX = 1
                End If
            Else
                iNumDecX = 2
            End If

            Dim iNumDecY As Integer = 0
            If IsNumeric(Session("RegData")(2, 1)) Then
                If Math.Abs(Session("RegData")(2, 1)) < 1 Then
                    iNumDecY = 3
                ElseIf Math.Abs(Session("RegData")(2, 1)) < 10 Then
                    iNumDecY = 2
                ElseIf Math.Abs(Session("RegData")(2, 1)) < 100 Then
                    iNumDecY = 1
                End If
            Else
                iNumDecY = 2
            End If
            sJscript = sJscript & "{name: ""Regression"", color: ""#000000""," & vbCr
            sJscript = sJscript & "data: ["

            Dim bFirst As Boolean = True
            For j = 1 To CCount
                If IsNumeric(Session("RegData")(1, j)) And IsNumeric(Session("RegData")(2, j)) Then
                    If Not bFirst Then
                        sJscript = sJscript & ","
                    Else
                        bFirst = False
                    End If
                    sJscript = sJscript & "{y:" & CStr(Math.Round(Session("RegData")(2, j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("RegData")(1, j), iNumDecX)) & "}"
                End If
            Next j
            sJscript = sJscript & vbCr & "]"
            sJscript = sJscript & "} " & vbCr

        Else 'JAM 2016-10-09 Adding Multiple Regressions



            For i = 1 To Session("TotMulGroups")

                If IsNumeric(Session("MulRegressX")(i, 1)) Then
                    If Math.Abs(Session("MulRegressX")(i, 1)) < 1 Then
                        iNumDecX = 3
                    ElseIf Math.Abs(Session("MulRegressX")(i, 1)) < 10 Then
                        iNumDecX = 2
                    ElseIf Math.Abs(Session("MulRegressX")(i, 1)) < 100 Then
                        iNumDecX = 1
                    End If
                Else
                    iNumDecX = 2
                End If

                Dim iNumDecY As Integer = 0
                If IsNumeric(Session("MulRegressY")(i, 1)) Then
                    If Math.Abs(Session("MulRegressY")(i, 1)) < 1 Then
                        iNumDecY = 3
                    ElseIf Math.Abs(Session("MulRegressY")(i, 1)) < 10 Then
                        iNumDecY = 2
                    ElseIf Math.Abs(Session("MulRegressY")(i, 1)) < 100 Then
                        iNumDecY = 1
                    End If
                Else
                    iNumDecY = 2
                End If

                MulRegressName = Session("GLNameJoinPrev")(i - 1) & "(" + Session("YearsLabels")(i - 1, 0) + ")"

                'JAM 2016-10-23 If it's the first regression
                If i = 1 Then
                    'sJscript = sJscript & "{name: ""Regression" & CStr(i) & """, color: ""#009600""," & vbCr  'Green
                    sJscript = sJscript & "{name: """ & MulRegressName & """, color: ""#009600""," & vbCr  'Green
                Else   'JAM 2016-10-23 If it's the second or more regreesion
                    If i = 2 Then  'JAM 2016-11-21
                        sJscript = sJscript & ",{name: """ & MulRegressName & """, color: ""#000096""," & vbCr 'Blue
                    Else
                        If i = 3 Then  'JAM 2016-11-21
                            sJscript = sJscript & ",{name: """ & MulRegressName & """, color: ""#ff0000""," & vbCr 'Red
                        Else
                            If i = 4 Then  'JAM 2016-11-21
                                sJscript = sJscript & ",{name: """ & MulRegressName & """, color: ""#ffff00""," & vbCr 'Yellow
                            Else
                                sJscript = sJscript & ",{name: """ & CStr(i) & """, color: ""#000000""," & vbCr 'Black

                            End If

                        End If

                    End If
                End If

                sJscript = sJscript & "data: ["

                Dim bFirst As Boolean = True
                For j = 1 To CCount
                    If IsNumeric(Session("MulRegressX")(i, j)) And IsNumeric(Session("MulRegressY")(i, j)) Then
                        If Not bFirst Then
                            sJscript = sJscript & ","
                        Else
                            bFirst = False
                        End If
                        sJscript = sJscript & "{y:" & CStr(Math.Round(Session("MulRegressY")(i, j), iNumDecY)) & ", x: " & CStr(Math.Round(Session("MulRegressX")(i, j), iNumDecX)) & "}"
                    End If
                Next j
                sJscript = sJscript & vbCr & "]"
                sJscript = sJscript & "} " & vbCr



            Next

        End If 'End if for TotMulGRoups

        PlotRegPairToJSSeries = sJscript

        Exit Function
PlotRegPairToJSSeries_Error:
        'MsgBox("Error PlotRegPairToJSSeries() routine")
        Exit Function

    End Function

    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''     JAM 2016-04-06 Adding GList
    ''' 
    ''' 
    Private Sub mnuAddGList_Click()

        On Error GoTo mnuAddGList_Error 'JAM 2016-11-24


        Call DefLabSubSet()
        Session("SubSetLabelsChanged") = False

        If Session("IsaGlist") And Not Session("IsaGroup") Then


            If Session("TotMulGroups") < 4 Then
                Session("AddGlistMode") = True
                'mnuDynamic.Enabled = False

                'JAM 2016-04-06 Adding Remove Previous
                If Session("TotMulGroups") = 1 And Not Session("PrevRemove") Then
                    Session("GLCountOrig") = Session("GListCount")
                    Call SaveCurrentData() 'Saving Previous Adding Group
                    Call FillPrevArrays() 'JAM 2016-04-09 Saving the first time

                    'JAM 2016-04-09  For Cohorts Variables Years selection isn't valid
                    If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
                        'JAM 2016-07-17 Aqui me quedé
                        Session("YearsLabels")(0, 1) = FindIndexYear()
                        If Session("GLbLagDep")(Session("YearsLabels")(0, 1)) = Session("GLbLagInd")(Session("YearsLabels")(0, 1)) Then 'JAM 2016-04-11 Build Legend for Year Selection
                            Session("YearsLabels")(0, 0) = Session("GLbLagDep")(Session("YearsLabels")(0, 1)) 'OrigDepYear
                        Else
                            Session("YearsLabels")(0, 0) = Session("GLbLagDep")(Session("YearsLabels")(0, 1)) + "," + Session("GLbLagInd")(Session("YearsLabels")(0, 1))
                        End If
                    Else
                        Session("YearsLabels")(0, 1) = 0
                        Session("YearsLabels")(0, 0) = Session("OrigDepYear") + "," + Session("OrigIndYear")

                    End If
                End If


                Call mnuGeographic_Click() 'Selecting next GList
            Else  'JAM 2016-04-17 Revisar
                'MsgBox("You can add four(4) GList only", vbInformation, "Add GList")
                Exit Sub
            End If
        End If

        Exit Sub
mnuAddGList_Error:
        'MsgBox("Error mnuAddGList() routine")
        Exit Sub


    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2016-06-04      Defining SubSetLabels
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub DefLabSubSet()


        On Error GoTo DefLabSubSet_Error 'JAM 2016-11-24

        Dim i As Integer


        ReDim Session("GlbOrigSubSet")(Session("TotMulGroups"))
        ReDim Session("GlbNewSubset")(Session("TotMulGroups"))

        For i = 1 To Session("TotMulGroups")
            Session("GlbOrigSubSet")(i - 1) = Session("SubsetLabels")(i)
            Session("GlbNewSubset")(i - 1) = Session("SubsetLabels")(i)
        Next i

        Exit Sub
DefLabSubSet_Error:
        'MsgBox("Error DefLabSubSet() routine")
        Exit Sub

    End Sub
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''           JAM 2016-04-07 Saving Current Data
    ''' 
    '''

    Private Sub SaveCurrentData()

        On Error GoTo SaveCurrentData_Error 'JAM 2016-11-24

        Dim i As Integer


        ReDim Session("PrevCVarI")(Session("GlistCount") + 1)
        ReDim Session("PrevCVarISec")(Session("GlistCount") + 1)
        ReDim Session("PrevCVarD")(Session("GlistCount") + 1)
        ReDim Session("PrevCVarName")(Session("GlistCount") + 1)
        ReDim Session("PrevCVarCode")(Session("GlistCount") + 1)
        ReDim Session("PrevCVarIPop")(Session("GlistCount") + 1)
        ReDim Session("PrevCVarIGdp")(Session("GlistCount") + 1)
        ReDim Session("PrevCVarAlias")(Session("GListCount") + 1)


        For i = 1 To UBound(Session("PrevCVarI")) - 1
            Session("PrevCVarI")(i) = Session("CVarI")(i)

            If Session("ThirdDimOk") Then
                Session("PrevCVarISec")(i) = Session("CVarISec")(i)
            End If
            Session("PrevCVarD")(i) = Session("CVarD")(i)
            Session("PrevCVarName")(i) = Session("CVarName")(i)
            Session("PrevCVarCode")(i) = Session("CVarCode")(i)
            Session("PrevCVarIPop")(i) = Session("CVarIPop")(i)
            Session("PrevCVarIGdp")(i) = Session("CVarIGdp")(i)
            'JAM 2016-07-03 Alias isn't defined
            'Session("PrevCVarAlias")(i) = Session("CVarAlias")(i)
        Next i



        'JAM 2016-04-07     Saving Original Data
        For i = 1 To Session("TotCountries")

            Session("OrigPrevCVarI")(Session("TotMulGroups") - 1, i) = Session("OrigIndFir")(i)
            If Session("ThirdDimOk") Then
                Session("OrigPrevCVarISec")(Session("TotMulGroups") - 1, i) = Session("OrigIndSec")(i)
            End If
            Session("OrigPrevCVarD")(Session("TotMulGroups") - 1, i) = Session("OrigDep")(i)
            Session("OrigPrevCVarIPop")(Session("TotMulGroups") - 1, i) = Session("IPopOrig")(i)
            Session("OrigPrevCVarIGdp")(Session("TotMulGroups") - 1, i) = Session("IGdpOrig")(i)
            Session("MulRCountryRegress")(Session("TotMulGroups") - 1, i) = Session("CountryRegress")(i)

        Next i


        Exit Sub
SaveCurrentData_Error:
        'MsgBox("Error SaveCurrentData() routine")
        Exit Sub


    End Sub

    ''' ''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''            JAM 2016-04-09 Filling Previous Adding action
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''

    Private Sub FillPrevArrays()

        On Error GoTo FillPrevArrays_Error 'JAM 2016-11-24

        Dim i, j, stepOne As Integer


        Session("DimJoinPrev")(Session("TotMulGroups") - 1) = Session("GListCount")
        Session("GLNameJoinPrev")(Session("TotMulGroups") - 1) = Session("GListName")
        If Session("GLDecompIndex")(Session("TotMulGroups") - 1) = 0 Then
            Session("GLDecompIndex")(Session("TotMulGroups") - 1) = Session("GListCount")
        End If
        For i = 1 To Session("GlistCount")

            Session("JoinPrevCVarI")(Session("TotMulGroups") - 1, i) = Session("PrevCVarI")(i)
            If Session("ThirdDimOk") Then
                Session("JoinPrevCVarISec")(Session("TotMulGroups") - 1, i) = Session("PrevCVarISec")(i)
            End If
            Session("JoinPrevCVarD")(Session("TotMulGroups") - 1, i) = Session("PrevCVarD")(i)
            Session("JoinPrevCVarName")(Session("TotMulGroups") - 1, i) = Session("PrevCVarName")(i)
            Session("JoinPrevCVarCode")(Session("TotMulGroups") - 1, i) = Session("PrevCVarCode")(i)
            Session("JoinPrevCVarIPop")(Session("TotMulGroups") - 1, i) = Session("PrevCVarIPop")(i)
            Session("JoinPrevCVarIGdp")(Session("TotMulGroups") - 1, i) = Session("PrevCVarIGdp")(i)
            'JAM 2016-07-02
            ' Session("JoinPrevCVarAlias")(Session("TotMulGroups") - 1, i) = Session("PrevCVarAlias")(i)
        Next i


        'JAM 2016-04-09 Data Normalization
        stepOne = 0
        For i = 0 To Session("TotMulGroups") - 1
            For j = 1 To Session("DimJoinPrev")(i)

                Session("CVarI")(j + stepOne) = Session("JoinPrevCVarI")(i, j)
                If Session("ThirdDimOk") Then
                    Session("CVarISec")(j + stepOne) = Session("JoinPrevCVarISec")(i, j)
                End If
                Session("CVarD")(j + stepOne) = Session("JoinPrevCVarD")(i, j)
                Session("CVarName")(j + stepOne) = Session("JoinPrevCVarName")(i, j)
                Session("CVarCode")(j + stepOne) = Session("JoinPrevCVarCode")(i, j)
                Session("CVarIPop")(j + stepOne) = Session("JoinPrevCVarIPop")(i, j)
                Session("CVarIGdp")(j + stepOne) = Session("JoinPrevCVarIGdp")(i, j)
                'JAM 2016-07-02
                'Session("CVarAlias")(j + stepOne) = Session("JoinPrevCVarAlias")(i, j)
            Next j
            stepOne = stepOne + Session("DimJoinPrev")(i)
        Next i

        'JAM 2016-04-09 Setting Graph Values
        Session("GlistCount") = stepOne 'JAM 2017-01-22
        'Session("CCount") = stepOne
        Session("CCount") = Session("GlistCount")
        'grf_Scatter.Points = CCount

        Exit Sub
FillPrevArrays_Error:
        'MsgBox("Error FillPrevArrays() routine")
        Exit Sub

    End Sub


    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''          JAM 2016-04-13      Build Legend for Year Chosen
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Function BuildLegendYear() As String

        On Error GoTo BuildLegendYear_Error 'JAM 2016-11-24
        'JAM 2016-04-13  For Cohorts Variables Years selection isn't valid
        If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
            If Session("GLbLagDep")(Session("YearChosen")) = Session("GLbLagInd")(Session("YearChosen")) Then
                BuildLegendYear = Session("GLbLagDep")(Session("YearChosen"))
            Else
                BuildLegendYear = Session("GLbLagDep")(Session("YearChosen")) + "," + Session("GLbLagInd")(Session("YearChosen"))

            End If
        Else
            BuildLegendYear = Session("OrigDepYear") + "," + Session("OrigIndYear")

        End If
        Exit Function
BuildLegendYear_Error:
        'MsgBox("Error BuildLegendYear() routine")
        Exit Function

    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''       JAM 2016-04-21 Adding Year 
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="vYearDep">Current Year for Dependent variable</param>
    ''' <param name="vYearInd">Current Year for Independent variable</param>
    ''' <param name="vYearSecInd">Current Year for SecondIndependent variable</param>
    ''' <remarks></remarks>


    Private Sub AddYear(ByVal vYearDep As String, ByVal vYearInd As String, ByVal vYearSecInd As String)

        On Error GoTo addyearerror

        'Dim DepSeries()
        ReDim Session("DepSeries")(Session("GCount"))

        'Dim IndSeries()
        ReDim Session("IndSeries")(Session("GCount"))

        'Dim SecIndSeries()
        ReDim Session("SecIndSeries")(Session("GCount"))

        Dim DepTblName As String, IndTblName As String, SecIndTblName As String
        'jrs 2016/04/21
        Dim ForIdx As Integer, i As Integer, sVar As String, pos1 As Integer, pos2 As Integer, icount As Integer, lineset As Integer, msg As String


        Dim db As String
        Dim valtbl As DataTable
        'Dependent
        ForIdx = 1

        db = Session("IFsHistSeries_MDB")

        If Not IsNothing(Session("YearChosen")) Then
            'jrs 2016/04/21
            DepTblName = Session("TblDepYear")(1)
            IndTblName = Session("TblIndYear")(1)
            If Session("ThirdDimOk") Then
                Session("SecIndTblName") = Session("TblSecIndYear")(1)
            End If
            'jrs 2016/04/21
            'Reset Filter
            Session("FilterReq") = 1

            If Not Session("bCompSVtmp")(ForIdx) Then
                If Session("GroupName") <> "Automobile" Then
                    'JAM 2009-11-07  For Cohorts Variables Years selection isn't valid
                    If Not Session("IsCohDep") Then
                        'db = OpenDatabase(IFsHistSeries_MDB, False, False, DataPW$)
                        db = Session("IFsHistSeries_MDB")
                    Else
                        'db = OpenDatabase(IFsWVSCohort_MDB)
                        db = Session("IFsWVSCohort_MDB")
                    End If
                Else
                    'db = OpenDatabase(IFsCoVaTrASeries_MDB)
                    db = Session("IFsCoVaTrASeries_MDB")
                End If




                'Loading Dependent Variable
                'JAM 2016-04-24  For Cohorts Variables Years selection isn't valid
                If Not Session("IsCohDep") Then
                    'valtbl = db.OpenRecordset(DepTblName)
                    SetDataTablefromAccess(db, "SELECT * FROM [" + DepTblName + "] ORDER BY Country", "", valtbl, DataPW)
                Else
                    'valtbl = db.OpenRecordset(DepTblName + WaveCohDep)
                    SetDataTablefromAccess(db, "SELECT * FROM [" + DepTblName + Session("WaveCohDep") + "] ORDER BY Country", "", valtbl, DataPW)
                End If
                'valtbl.Index = "Country"
                valtbl.Columns.Item(0).ColumnName = "Country"

                ''If Not valtbl.EOF Then
                'valtbl.MoveFirst()

                For i = 0 To valtbl.Rows.Count - 1


                    'For iCount% = 1 To TotCountries
                    Session("DepSeries")(i + 1) = valtbl.Rows(i).Item(vYearDep)
                    Session("CVarD")(i + 1) = Session("DepSeries")(i + 1)
                    Session("OrigDep")(i + 1) = Session("DepSeries")(i + 1)
                    Session("FullDep")(i + 1) = Session("DepSeries")(i + 1)

                    'valtbl.MoveNext()
                    'Next iCount%
                Next i
                ''End If
                'valtbl.Close()
                'db.Close()
            Else 'jrs 2012/10/01   '
                'Update Formula Year
                For i = 1 To Session("aFormulas")(ForIdx).disVar.disSize
                    sVar = Session("aFormulas")(ForIdx).disVar.displayDet(i)
                    pos1 = InStr(1, sVar, "(")
                    If pos1 > 0 Then
                        pos2 = InStr(1, sVar, ")")
                    End If
                    If pos1 > 0 And pos2 > 0 Then
                        sVar = Mid(sVar, 1, pos1) & CStr(vYearDep) & ")"
                        Session("aFormulas")(ForIdx).disVar.setDataDet(i, sVar)
                    End If
                Next i
                'Using formulas for Dep. Var
                AddYrComp(Session("aFormulas")(ForIdx), "D")
            End If


            ForIdx = 0
            If Not Session("bCompSVtmp")(ForIdx) Then
                If Session("GroupName") <> "Automobile" Then

                    If Not Session("IsCoh1Ind") Then
                        'db = OpenDatabase(IFsHistSeries_MDB, False, False, DataPW$)
                        db = Session("IFsHistSeries_MDB")
                    Else
                        'db = OpenDatabase(IFsWVSCohort_MDB)
                        db = Session("IFsWVSCohort_MDB")
                    End If
                Else
                    'db = OpenDatabase(IFsCoVaTrASeries_MDB)

                    db = Session("IFsCoVaTrASeries_MDB")
                End If

                'Loading Independent Variable
                'JAM 2016-05-14  For Cohorts Variables Years selection isn't valid
                If Not Session("IsCoh1Ind") Then  'JAM Aqui me quede
                    'valtbl = db.OpenRecordset(IndTblName)
                    SetDataTablefromAccess(db, "SELECT * FROM [" + IndTblName + "] ORDER BY Country", "", valtbl, DataPW)
                Else
                    'valtbl = db.OpenRecordset(IndTblName + WaveCohInd)
                    SetDataTablefromAccess(db, "SELECT * FROM [" + IndTblName + Session("WaveCohInd") + "] ORDER BY Country", "", valtbl, DataPW)
                End If

                'Set valtbl = db.OpenRecordset(IndTblName)
                'valtbl.Index = "Country"
                'If Not valtbl.EOF Then
                '    valtbl.MoveFirst()
                '    For icount% = 1 To TotCountries
                '        IndSeries(icount%) = valtbl.Fields(vYearInd)
                '        CVarI(icount%) = IndSeries(icount%)
                '        OrigIndFir(icount%) = IndSeries(icount%)
                '        FullInd(icount%) = IndSeries(icount%)

                '        valtbl.MoveNext()
                '    Next icount%
                'End If
                'valtbl.Close()
                'db.Close()


                For i = 0 To valtbl.Rows.Count - 1


                    'For iCount% = 1 To TotCountries
                    Session("IndSeries")(i + 1) = valtbl.Rows(i).Item(vYearInd)
                    Session("CVarI")(i + 1) = Session("IndSeries")(i + 1)
                    Session("OrigIndFir")(i + 1) = Session("IndSeries")(i + 1)
                    Session("FullDep")(i + 1) = Session("IndSeries")(i + 1)


                Next i


            Else 'jrs 2012/10/01
                'Update Formula Year
                For i = 1 To Session("aFormulas")(ForIdx).disVar.disSize
                    sVar = Session("aFormulas")(ForIdx).disVar.displayDet(i)
                    pos1 = InStr(1, sVar, "(")
                    If pos1 > 0 Then
                        pos2 = InStr(1, sVar, ")")
                    End If
                    If pos1 > 0 And pos2 > 0 Then
                        sVar = Mid(sVar, 1, pos1) & CStr(vYearInd) & ")"
                        Session("aFormulas")(ForIdx).disVar.setDataDet(i, sVar)
                    End If
                Next i
                'Using Formulas in 1st Indep. Variable
                AddYrComp(Session("aFormulas")(ForIdx), "I1")
            End If



            'Loading Second Independent Variable
            If Session("ThirdDimOk") And (Not IsDBNull(vYearSecInd) And vYearSecInd <> "0" And vYearSecInd <> "") And Not Session("IsCoh2Ind") Then
                If vYearSecInd = "0" Then
                    If vYearInd = "Earliest" Then
                        vYearSecInd = "Earliest"
                    Else
                        vYearSecInd = "MostRecent"
                    End If
                End If
                'jrs 2012/10/01
                ForIdx = 2
                If Not Session("bCompSVtmp")(ForIdx) Then
                    If Session("GroupName") <> "Automobile" Then
                        'JAM 2016-05-14  For Cohorts Variables Years selection isn't valid
                        If Not Session("IsCoh2Ind") Then
                            'db = OpenDatabase(IFsHistSeries_MDB, False, False, DataPW$)
                            db = Session("IFsHistSeries_MDB")
                        Else
                            'db = OpenDatabase(IFsWVSCohort_MDB)
                            db = Session("IFsWVSCohort_MDB")
                        End If
                    Else
                        'db = OpenDatabase(IFsCoVaTrASeries_MDB)
                        db = Session("IFsCoVaTrASeries_MDB")
                    End If


                    'valtbl = db.OpenRecordset(SecIndTblName)
                    'valtbl.Index = "Country"
                    'If Not valtbl.EOF Then
                    '    valtbl.MoveFirst()
                    '    For icount% = 1 To TotCountries
                    '        SecIndSeries(icount%) = valtbl.Fields(vYearSecInd)
                    '        CVarISec(icount%) = SecIndSeries(icount%)
                    '        OrigIndSec(icount%) = SecIndSeries(icount%)


                    '        valtbl.MoveNext()
                    '    Next icount%
                    'End If
                    'valtbl.Close()
                    'db.Close()


                    For i = 0 To valtbl.Rows.Count - 1


                        'For iCount% = 1 To TotCountries
                        Session("SecIndSeries")(i + 1) = valtbl.Rows(i).Item(vYearSecInd)
                        Session("CVarISec")(i + 1) = Session("SecIndSeries")(i + 1)
                        Session("OrigIndSec")(i + 1) = Session("SecIndSeries")(i + 1)
                        'Session("FullDep")(i) = Session("IndSeries")(i)


                    Next i



                Else 'jrs 2012/10/01
                    'Update Formula Year
                    For i = 1 To Session("aFormulas")(ForIdx).disVar.disSize
                        sVar = Session("aFormulas")(ForIdx).disVar.displayDet(i)
                        pos1 = InStr(1, sVar, "(")
                        If pos1 > 0 Then
                            pos2 = InStr(1, sVar, ")")
                        End If
                        If pos1 > 0 And pos2 > 0 Then
                            sVar = Mid(sVar, 1, pos1) & CStr(vYearSecInd) & ")"
                            Session("aFormulas")(ForIdx).disVar.setDataDet(i, sVar)
                        End If
                    Next i

                    'Using Formulas in 1st Indep. Variable
                    AddYrComp(Session("aFormulas")(ForIdx), "I2")
                End If
            End If


            For lineset = 1 To Session("TotCountries")

                Session("CVarIGdp")(lineset) = Session("IGdpOrig")(lineset)
                Session("CVarIPop")(lineset) = Session("IPopOrig")(lineset)

            Next lineset%


            'RecalculateGList


        End If
        'JAM 2010-10-21 Adding errors mangaging
        Exit Sub
addyearerror:

        'MsgBox("Error AddYear() routine")
        Exit Sub


    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '           JAM  2016-05-01
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    'jrs 2012/10/01
    'Created to load formulas in additional years for All Countries
    Public Sub AddYrComp(ByVal f As Formula, ByVal IndType As String)

        On Error GoTo AddYrComp_Error 'JAM 2016-11-24

        'Parameters:
        '1. f is the formula to be grouped
        '2. IndType indicates where to copy the results

        ' Declare an Array to Store Results
        ' ---------------------------------
        'ReDim Session("ResultArray")()
        ReDim Session("ResultArray")(Session("TotCountries"))
        Dim Result As Object

        ' Loop through all Members of the G-List
        ' --------------------------------------
        Dim i As Integer, RegIdx As Integer, bRegions As Boolean
        Dim onlyNull As Boolean

        For i = 1 To Session("TotCountries")
            'New function with Year specifics, Hist. Empirical, and Target Array
            'JAM 2016-06-01 Pendiente
            ''''gLoadCompHisEmp(Result, Session("CVarCounName")(i), False, f, 0, False, Nothing)
            Session("ResultArray")(i) = Result
        Next i

        If IndType = "D" Then
            For i = 1 To Session("TotCountries")
                Session("CVarD")(i) = Session("ResultArray")(i)
                Session("OrigDep")(i) = Session("ResultArray")(i)
                Session("FullDep")(i) = Session("ResultArray")(i)
            Next i
        ElseIf IndType = "I1" Then
            For i = 1 To Session("TotCountries")
                Session("CVarI")(i) = Session("ResultArray")(i)
                Session("OrigIndFir")(i) = Session("ResultArray")(i)
                Session("FullInd")(i) = Session("ResultArray")(i)
            Next i
        Else
            For i = 1 To Session("TotCountries")
                Session("CVarISec")(i) = Session("ResultArray")(i)
                Session("OrigIndSec")(i) = Session("ResultArray")(i)
            Next i
        End If

        Exit Sub
AddYrComp_Error:
        'MsgBox("Error AddYrComp() routine")
        Exit Sub

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2016-07-17      Finding Initial Index for Year
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function FindIndexYear() As Integer

        On Error GoTo FindIndexYear_Error 'JAM 2016-11-24

        Dim i, j As Integer

        j = 0
        For i = 0 To UBound(Session("GLbLagDep"))
            If UCase(Session("GLbLagDep")(i)) = UCase(Session("OrigDepYear")) Then 'JAM 2009-10-05 For an Equal comparison
                j = i
                Exit For
            End If
        Next i



        FindIndexYear = j

        Exit Function
FindIndexYear_Error:
        ' MsgBox("Error FindIndexYear() routine")
        Exit Function

    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''        JAM 2016-08-21  Code Post Add Glist
    ''' 
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub PostGlist()


        On Error GoTo PostGlist_Error 'JAM 2016-11-24

        ''JAM 2016-04-11 After GList selection
        'Session("TotMulGroups") = Session("TotMulGroups") + 1
        If Session("GChanged") Then

            'JAM 2016-04-11 After GList selection
            '   Session("TotMulGroups") = Session("TotMulGroups") + 1

            'REvisar
            Call SaveCurrentData() 'Saving Previous Adding Group
            Call FillPrevArrays() 'Normalization Values

            Session("YearsLabels")(Session("TotMulGroups") - 1, 0) = BuildLegendYear() 'JAM 2009-09-23 Build Legend for Year Selection


            'JAM 2016-04-13  For Cohorts Variables Years selection isn't valid
            If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
                Session("YearsLabels")(Session("TotMulGroups") - 1, 1) = Session("YearChosen") 'JAM 2016-04-13 Chosen Index Year
            Else
                Session("YearsLabels")(Session("TotMulGroups") - 1, 1) = 0
            End If

            ''Call GraphData()   'JAM 2016-04-15 
            'JAM 2016-04-15 Saving regression disabled in multiple regressions
            'mnuSaveRegress.Enabled = False
            'jrs 2016-04-15
            'mnuSaveRes.Enabled = False

            'JAM 2016-04-17  For Cohorts Variables Years selection isn't valid
            ''If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
            ''    Call AddYear(Session("OrigDepYear"), Session("OrigIndYear"), Session("OrigSecIndYear"))
            ''Else
            ''    Call AddYear(Session("GLbLagDep")(0), Session("GLbLagInd")(0), Session("LagSecInd")(0))

            ''End If


            'JAM 2016-04-17    Enabling removing Glist
            'mnuRemoveG.Enabled = True

        Else
            If Session("TotMulGroups") > 0 Then
                Session("TotMulGroups") = Session(" TotMulGroups") - 1 'The user didn't choose none value
            End If
        End If

        ' Session("AddGlistMode") = False    'JAM 2016-04-17 we need turn off AddGlist Mode

        'JAM 2009-10-06 Disabling Navigation Buttons
        If Session("TotMulGroups") > 1 Then
            'cmdNext.Enabled = False
            'cmdPrevious.Enabled = False
        End If


        Exit Sub
PostGlist_Error:
        'MsgBox("Error PostGlist() routine")
        Exit Sub

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '   Building Graph Title for AddGlist Mode
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function BuildGraphTitle() As String

        On Error GoTo BuildGraphTitle_Error 'JAM 2016-11-24

        Dim vgraphtitle As String
        Dim test As String


        test = Session("GLNameJoinPrev")(1)

        vgraphtitle = ""
        If Session("IsaGlist") = False Then
            vgraphtitle = ""

        Else
            If Not Session("AddGlistMode") Then    'JAM 2016-09-25 Managing title in AddGList Mode
                If Not Session("DefLabels") Then   'JAM 2016-09-25 Only if the subtitle label isn´t changed
                    vgraphtitle = Session("GListName")
                End If
            Else 'JAM 2016-09-25 Managing colors in AddGList Mode
                If Session("OrigDepYear") = Session("OrigIndYear") Then
                    Session("OrigLabelYear") = "(" + Session("OrigDepYear") + ")"
                Else
                    Session("OrigLabelYear") = "(" + Session("OrigDepYear") + "," + Session("OrigIndYear") + ")"
                End If
                If Session("TotMulGroups") = 1 Then
                    vgraphtitle = Session("GLNameJoinPrev")(0) + "(" + Session("YearsLabels")(0, 0) + ")"  'OrigLabelYear
                Else
                    If Session("TotMulGroups") = 2 Then
                        vgraphtitle = Session("GLNameJoinPrev")(0) + "(" + Session("YearsLabels")(0, 0) + ")" + " and " + Session("GLNameJoinPrev")(1) + "(" + Session("YearsLabels")(1, 0) + ")"
                    Else
                        If Session("TotMulGroups") = 3 Then
                            vgraphtitle = Session("GLNameJoinPrev")(0) + "(" + Session("YearsLabels")(0, 0) + ")" + ", " + Session("GLNameJoinPrev")(1) + "(" + Session("YearsLabels")(1, 0) + ") and " + Session("GLNameJoinPrev")(2) + "(" + Session("YearsLabels")(2, 0) + ")"
                        Else    'JAM 2016-09-25     Adding 4th set in GList mode
                            If Session("TotMulGroups") = 4 Then
                                vgraphtitle = Session("GLNameJoinPrev")(0) + "(" + Session("YearsLabels")(0, 0) + ")" + ", " + Session("GLNameJoinPrev")(1) + "(" + Session("YearsLabels")(1, 0) + "), " + Session("GLNameJoinPrev")(2) + "(" + Session("YearsLabels")(2, 0) + ") and " + Session("GLNameJoinPrev")(3) + "(" + Session("YearsLabels")(3, 0) + ")"
                            Else
                                vgraphtitle = Session("GListName")
                            End If
                        End If
                    End If
                End If

            End If

        End If

        BuildGraphTitle = vgraphtitle

        Exit Function
BuildGraphTitle_Error:
        ' MsgBox("Error BuildGraphTitle() routine")
        Exit Function

    End Function
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''           JAM 2016-09-26 Adding Year for GList or Group
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub mnuAddYear_Click()

        On Error GoTo mnuAddYear_Error 'JAM 2016-11-24

        'JAM 2016-09-26
        Call DefLabSubSet()
        Session("SubSetLabelsChanged") = False
        'JAM 2016-10-30
        Session("FilterReq") = 1
        'JAM 2016-09-26
        Session("vAddYear") = True

        'JAM 2016-12-11
        Session("AddingYear") = True

        Session("AddGlistMode") = True
        If Session("IsaGlist") And Session("IsaGroup") Then
            Call mnuAddGrp_Click()
        Else
            If Session("IsaGlist") Then
                Call mnuAddGList_Click()
            End If
        End If

        Exit Sub
mnuAddYear_Error:
        ' MsgBox("Error mnuAddYear() routine")
        Exit Sub

    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''     JAM 2016-09-26 Adding Groups
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub mnuAddGrp_Click()

        On Error GoTo mnuAddGrp_Error 'JAM 2016-11-24

        'JAM 2016-09-26
        Call DefLabSubSet()
        Session("SubSetLabelsChanged") = False
        If Session("IsaGlist") And Session("IsaGroup") Then
            If Session("TotMulGroups") < 4 Then

                Session("AddGlistMode") = True
                'mnuDynamic.Enabled = False

                'JAM 2016-10-06 Adding Remove Previous
                If Session("TotMulGroups") = 1 And Not Session("PrevRemove") Then
                    Session("GLCountOrig") = Session("GListCount")
                    Call SaveCurrentData() 'Saving Previous Adding Group

                    Call FillPrevArrays() 'JAM 2016-09-26 Saving the first time

                    'JAM 2016-09-26  For Cohorts Variables Years selection isn't valid
                    If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
                        Session("YearsLabels")(0, 1) = FindIndexYear() 'JAM 2016-09-26 Initial Index
                        If Session("GLbLagDep")(Session("YearsLabels")(0, 1)) = Session("GLbLagInd")(Session("YearsLabels")(0, 1)) Then 'JAM 2016-09-26 Build Legend for Year Selection
                            Session("YearsLabels")(0, 0) = Session("GLbLagDep")(Session("YearsLabels")(0, 1)) 'OrigDepYear
                        Else
                            Session("YearsLabels")(0, 0) = Session("GLbLagDep")(Session("YearsLabels")(0, 1)) + "," + Session("GLbLagInd")(Session("YearsLabels")(0, 1))
                        End If
                    Else
                        Session("YearsLabels")(0, 1) = 0
                        Session("YearsLabels")(0, 0) = Session("OrigDepYear") + "," + Session("OrigIndYear")

                    End If
                End If

                Call mnuGroup_Click() 'Selecting next GList
                'JAM 2016-09-26 After GList selection
                Session("TotMulGroups") = Session("TotMulGroups") + 1

                If Session("GChanged") Then
                    'JAM 2016-09-26 Only if Glist changed
                    'TotMulGroups = TotMulGroups + 1
                    Call SaveCurrentData() 'Saving Previous Adding Group

                    Call FillPrevArrays() 'Normalization Values

                    Session("YearsLabels")(Session("TotMulGroups") - 1, 0) = BuildLegendYear() 'JAM 2016-09-26 Build Legend for Year Selection
                    'JAM 2016-09-26  For Cohorts Variables Years selection isn't valid
                    If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
                        Session("YearsLabels")(Session("TotMulGroups") - 1, 1) = Session("YearChosen") 'JAM 2016-09-26 Chosen Index Year
                    Else
                        Session("YearsLabels")(Session("TotMulGroups") - 1, 1) = 0
                    End If
                    Call GraphData()
                    ''JAM 2009-08-14 Saving regression disabled in multiple regressions
                    ''mnuSaveRegress.Enabled = False
                    ''jrs 2014/09/17
                    ''mnuSaveRes.Enabled = False

                    'JAM 2016-09-26    Resetting to original values selected
                    'JAM 2016-09-26  For Cohorts Variables Years selection isn't valid
                    If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
                        Call AddYear(Session("OrigDepYear"), Session("OrigIndYear"), Session("OrigSecIndYear"))
                    Else
                        Call AddYear(Session("GLbLagDep")(0), Session("GLbLagInd")(0), Session("LagSecInd")(0))
                    End If

                    'JAM 2016-09-26    Enabling removing Glist
                    ''mnuRemoveG.Enabled = True

                Else
                    If Session("TotMulGroups") > 0 Then
                        Session("TotMulGroups") = Session("TotMulGroups") - 1 'The user didn't choose none value
                    End If
                End If


            Else
                'MsgBox("You can add four(4) Groups only", vbInformation, "Add Group")
                Exit Sub
            End If
        End If
        Session("AddGlistMode") = False    'JAM 2016-09-26 we need turn off AddGlist Mode
        'JAM 2016-09-26 Disabling Navigation Buttons
        If Session("TotMulGroups") > 1 Then
            Session("cmdNext.Enabled") = False
            Session("cmdPrevious.Enabled") = False
        End If

        Exit Sub
mnuAddGrp_Error:
        'MsgBox("Error mnuAddGrp() routine")
        Exit Sub

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       'JAM 2016-10-08     Function to calculate MultipleRegress
    '
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub MulRegressPlot(ByVal Degree As Integer, ByVal Transform As String, ByVal GrpIndReg As Integer)

        On Error GoTo MulRegressPlot_Error 'JAM 2016-11-24


        Dim SaturatePoint As Double
        Dim x#, y#
        Dim a1#, B1#, a#, b#
        Dim Equation As String
        Dim RegGrp As Integer
        Dim RegResult As Double
        Dim YearShowed As String
        Dim Formula As String
        'Dim sign2, sign3, sign As String
        'Dim ThirdCoef, SecondCoef, FourthCoef As String
        'Dim B1format, B2format, B3Format, B4Format, RFormat As String
        Dim CoefB1, CoefB2, CoefB3, CoefB4
        Dim ThirdCoef, SecondCoef, FourthCoef, sign, sign2, sign3, sign4
        Dim B1Format, B2Format, B3Format, B4Format, RFormat
        Dim RegressFormul, RsqrFormul

        Dim atest


        'JAM 2016-10-08
        Call FindMinMaxRegress()


        If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
            Session("CCount") = Session("TotCountries")
        Else    'JAM 2016-10-08     Using the right GList elements
            Session("CCount") = Session("DimJoinPrev")(GrpIndReg)
        End If

        With vreg
            .Degree = Degree


            For i2 = 1 To Session("CCount")

                If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                    If Not ((Session("OrigPrevCVarI")(GrpIndReg, i2)) Is Nothing) And Not (Session("OrigPrevCVarD")(GrpIndReg, i2) Is Nothing) And Session("MulRCountryRegress")(GrpIndReg, i2) And IsNumeric(Session("OrigPrevCVarI")(GrpIndReg, i2)) And IsNumeric(Session("OrigPrevCVarD")(GrpIndReg, i2)) Then 'JAM 2016-11-28



                        'Format(PlotLong(PYear%, cc%), "0.000")
                        Select Case Transform
                            Case "LOG"
                                x# = CDbl(Math.Log(Session("OrigPrevCVarI")(GrpIndReg, i2)))
                            Case "EXP"
                                x# = CDbl(Math.Exp(Session("OrigPrevCVarI")(GrpIndReg, i2))) 'CDbl(TC1 * exp(TC2 * OrigIndFir(i2)))
                            Case "POW"
                                'jrs 2017/10/23 x# = CDbl((Session("OrigPrevCVarI")(GrpIndReg, i2) ^ Session("PowerDegree")))
                                x# = CDbl(Math.Log(Session("OrigPrevCVarI")(GrpIndReg, i2)))
                            Case Else
                                x# = CDbl(Session("OrigPrevCVarI")(GrpIndReg, i2))
                        End Select

                        'jrs 2017/10/23
                        If Transform = "POW" Then
                            y# = CDbl(Math.Log(Session("OrigPrevCVarD")(GrpIndReg, i2)))
                        Else
                            y# = CDbl(Session("OrigPrevCVarD")(GrpIndReg, i2)) '1
                        End If



                        .XYAdd(x#, y#) 'Ln(i2), Format(PlotLong(i2, 1), "0.000")
                    End If
                Else
                    If Not (Session("JoinPrevCVarI")(GrpIndReg, i2) Is Nothing) And Not (Session("JoinPrevCVarD")(GrpIndReg, i2) Is Nothing) And IsNumeric(Session("JoinPrevCVarI")(GrpIndReg, i2)) And IsNumeric(Session("JoinPrevCVarD")(GrpIndReg, i2)) Then

                        Select Case Transform
                            Case "LOG"
                                x# = CDbl(Math.Log(Session("JoinPrevCVarI")(GrpIndReg, i2)))
                            Case "EXP"
                                x# = CDbl(Math.Exp(Session("JoinPrevCVarI")(GrpIndReg, i2))) 'CDbl(TC1 * exp(TC2 * CVarI(i2)))
                            Case "POW"
                                'jrs 2017/10/23 x# = CDbl((Session("JoinPrevCVarI")(GrpIndReg, i2) ^ (Session("PowerDegree"))))
                                x# = CDbl(Math.Log(Session("JoinPrevCVarI")(GrpIndReg, i2)))
                            Case Else
                                x# = CDbl(Session("JoinPrevCVarI")(GrpIndReg, i2))
                        End Select

                        'jrs 2017/10/23
                        If Transform = "POW" Then
                            y# = CDbl(Math.Log(Session("JoinPrevCVarD")(GrpIndReg, i2)))
                        Else
                            y# = CDbl(Session("JoinPrevCVarD")(GrpIndReg, i2)) '1
                        End If



                        .XYAdd(x#, y#) 'Ln(i2), Format(PlotLong(i2, 1), "0.000")
                    End If

                End If

            Next i2

        End With





        ''jrs 2008/08/28
        'DoEvents()


        'JAM 2016-10-08     Modifying subsets
        Session("MulRegSubsets") = 1 + (GrpIndReg + 1) 'TotMulGroups '2


        Session("CCount") = Session("TotCountries")
        ''grf_Scatter.Points = TotCountries


        'JAM 2007-07-08  Code Added, in order to keep the points in group one perfectly graphed
        '----------------------------------------------------------------------------------
        ''For II% = 1 To CCount
        ''    If grf_Scatter.YData(0, II%) = 0 And grf_Scatter.XData(0, II%) = 0 Then
        ''        grf_Scatter.YData(0, II%) = -9999
        ''        grf_Scatter.XData(0, II%) = -9999
        ''        grf_Scatter.DataPointLabels(0, II%) = ""
        ''    End If
        ''Next II%

        ''grf_Scatter.NullDataValueX = -9999
        ''grf_Scatter.NullDataValueY = -9999

        '----------------------------------------------------------------------------------
        'JAM 2016-10-08 Filling data for Regression groups
        RegGrp = GrpIndReg + 1

        For II% = 1 To Session("CCount")

            Session("MulRegressX")(RegGrp, II% - 1) = Session("MulRMinVI") + ((Session("MulRMaxVI") - Session("MulRMinVI")) * ((II% - 1) / (Session("CCount") - 1)))

            If Transform = "LOG" Then
                RegResult = vreg.RegVal(CDbl(Math.Log(Session("MulRegressX")(RegGrp, II% - 1))))
            Else
                If Transform = "EXP" Then
                    RegResult = vreg.RegVal(CDbl(Math.Exp(Session("MulRegressX")(RegGrp, II% - 1))))
                Else
                    If Transform = "POW" Then
                        'jrs 2017/10/23 RegResult = vreg.RegVal(CDbl((Session("MulRegressX")(RegGrp, II% - 1)) ^ (Session("PowerDegree"))))
                        RegResult = vreg.RegVal(CDbl(Math.Log(Session("MulRegressX")(RegGrp, II% - 1))))
                        RegResult = Math.Exp(RegResult)
                    Else
                        If Not Session("ChangeSatPoint") Then  'JAM 2016-10-09 Code Added for Saturation Point
                            RegResult = vreg.RegVal(CDbl(Session("MulRegressX")(RegGrp, II% - 1)))
                        Else
                            RegResult = Session("SaturationValue2") / (1 + Math.Exp(CDbl(vreg.Coeff(0) + vreg.Coeff(1) * Session("MulRegressX")(RegGrp, II% - 1))))
                        End If

                    End If
                End If
            End If

            'JAM 2016-10-09    Proessential valid data
            If Math.Abs(RegResult) >= 1.401298E-45 And Math.Abs(RegResult) <= 3.402823E+38 Then
                Session("MulRegressY")(RegGrp, II% - 1) = RegResult
            Else
                Session("MulRegressY")(RegGrp, II% - 1) = -9999
            End If


            'grf_Scatter.DataPointLabels(RegGrp, II% - 1) = ""

            'JAM 2016-10-09
            'If RegGrp = 1 Then
            '    grf_Scatter.PointColors(RegGrp, II% - 1) = grf_Scatter.PEargb(255, 0, 0, 150) 'Blue
            'Else
            '    If RegGrp = 2 Then
            '        grf_Scatter.PointColors(RegGrp, II% - 1) = grf_Scatter.PEargb(255, 0, 150, 0) 'Green
            '    Else
            '        If RegGrp = 3 Then
            '            grf_Scatter.PointColors(RegGrp, II% - 1) = QBColorPE(4) 'grf_Scatter.PEargb(255, 255, 0, 0) 'Red
            '        Else
            '            grf_Scatter.PointColors(RegGrp, II% - 1) = grf_Scatter.PEargb(255, 255, 255, 0) 'Yellow
            '        End If
            '    End If
            'End If

            'JAM code added for avoiding to use graph null Data
            'If grf_Scatter.XData(RegGrp, II% - 1) = 0 And grf_Scatter.YData(RegGrp, II% - 1) = 0 Then
            '    grf_Scatter.XData(RegGrp, II% - 1) = -9999
            '    grf_Scatter.YData(RegGrp, II% - 1) = -9999
            'End If


        Next II%



        Select Case Transform
            Case "LINEAR"
                Formula = "("
            Case "LOG"
                Formula = "ln("
            Case "EXP"
                Formula = "Exp("
            Case "POW"
                Formula = "^"
            Case Else
                Formula = "("
        End Select





        If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
            Session("CCount") = Session("TotCountries")
        Else
            If Session("IsaGlist") Then
                Session("CCount") = Session("GListCount")
            Else
                Session("CCount") = Session("TotCountries")
            End If

        End If

        Session("CoefB1") = vreg.Coeff(0)  'Y=a + bt
        Session("CoefB2") = vreg.Coeff(1)
        Session("CoefB3") = -9999
        Session("CoefB4") = -9999

        If Degree > 1 Then

            Session("CoefB3") = vreg.Coeff(2)
            ThirdCoef = Math.Abs(Session("CoefB3"))
            If Session("CoefB3") < 0 Then
                sign2 = " - "

            Else
                sign2 = " + "
            End If

            If Degree > 2 Then
                Session("CoefB4") = vreg.Coeff(3)
                FourthCoef = Math.Abs(Session("CoefB4"))
                If Session("CoefB4") < 0 Then
                    sign3 = " - "
                Else
                    sign3 = " + "
                End If

            End If

        End If

        SecondCoef = Math.Abs(Session("CoefB2"))
        If Session("CoefB2") < 0 Then
            sign = " - "

        Else
            sign = " + "
        End If


        Call MulCalcRSQR(Transform, GrpIndReg)


        If Math.Abs(Session("CoefB1")) < 0.01 Or Math.Abs(Session("CoefB1")) > 1000000 Then
            B1Format = "0.00E+00"
        Else
            B1Format = "0.0000"
        End If

        If SecondCoef < 0.01 Or SecondCoef > 1000000 Then
            B2Format = "0.00E+00"
        Else
            B2Format = "0.0000"
        End If

        If ThirdCoef < 0.01 Or ThirdCoef > 1000000 Then
            B3Format = "0.00E+00"
        Else
            B3Format = "0.0000"
        End If

        If FourthCoef < 0.01 Or FourthCoef > 1000000 Then
            B4Format = "0.00E+00"
        Else
            B4Format = "0.0000"
        End If

        If Math.Abs(RSQR) < 0.01 Or Math.Abs(RSQR) > 1000000 Then
            RFormat = "0.00E+00"
        Else
            RFormat = "0.0000"
        End If

        'Dim ac As String

        'ac = String.Format(CStr(SecondCoef), B2format)


        If Transform = "NONE" Or Transform = "LOG" Then
            Equation = "y= " + Format(Session("CoefB1"), B1Format) + sign + Format(SecondCoef, B2Format) + "*" + Formula + "x)"

            'JAM 2016-10-09 If We've choose Saturation Option
            If Session("ChangeSatPoint") Then
                If sign = " - " Then
                    Equation = "y= " + Format(Session("SaturationValue"), "0.00") + "/(1 + e^(" + Format(Session("CoefB1"), B1Format) + sign + Format(SecondCoef, B2Format) + "*" + Formula + "x)))"
                Else
                    Equation = "y= " + Format(Session("SaturationValue"), "0.00") + "/(1 + e^(" + Format(Session("CoefB1"), B1Format) + "+" + Format(SecondCoef, B2Format) + "*" + Formula + "x)))"
                End If
            End If


        Else
            If Transform = "EXP" Then
                Equation = "y= " + Format(Session("CoefB1"), B1Format) + sign + Format(SecondCoef, B2Format) + "*e^(" + "x)" 'sign + Format(SecondCoef, B2Format) + "*x)"
            Else
                If Transform = "POW" Then
                    'jrs 2017/10/23 Equation = "y= " + Format(Session("CoefB1"), B1Format) + sign + Format(SecondCoef, B2Format) + "*x^(" + Session("PowerDegree") + ")"
                    Equation = "y= " + Format(Session("CoefB1"), B1Format) + "*x^(" + Format(Session("CoefB2"), B2Format) + ")"
                Else
                    Equation = "y= " + Format(Session("CoefB1"), B1Format) + sign + Format(SecondCoef, B2Format) + "*" + Formula + "x)"
                End If
            End If

        End If


        If Session("CoefB3") <> -9999 Then
            Equation = Equation + sign2 + Format(ThirdCoef, B3Format) + "*" + Formula + "x^2)"
        End If

        If Session("CoefB4") <> -9999 Then
            Equation = Equation + sign3 + Format(FourthCoef, B4Format) + "*" + Formula + "x^3)"
        End If


        'grf_Scatter.SubsetLabels(0) = "Data in bubbles format"

        'JAM 2009-09-26 Turning off subsets Legends
        '    grf_Scatter.SubsetsToLegend(0) = -1
        '    grf_Scatter.SubsetsToLegend(1) = -1
        ''    'JAM 2009-08-07
        '    grf_Scatter.SubsetsToLegend(GrpIndReg) = -1



        'grf_Scatter.SubsetsToLegend(3) = -1


        '    grf_Scatter.MultiSubTitles(0) = "|" + Equation + "|"
        '    grf_Scatter.MultiSubTitles(1) = "|" + "R(SQR) = " + Format(RSQR, RFormat) + "|"




        Session("RegressTitle")(GrpIndReg) = "|" + Session("GLNameJoinPrev")(GrpIndReg) + "(" + Session("YearsLabels")(GrpIndReg, 0) + ")" + ": " + Equation + " , " + "R(SQR) = " + Format(RSQR, RFormat) + "|"

        '''''''''''''''''''''''''''''''''''''''''''''''''''''
        'JAM 2009-09-26 SubSets Properties
        ''If SubSetLabelsChanged Then
        ''    grf_Scatter.SubsetLabels(GrpIndReg + 1) = GlbNewSubset(GrpIndReg)
        ''Else
        ''    grf_Scatter.SubsetLabels(GrpIndReg + 1) = GLNameJoinPrev(GrpIndReg) + "(" + YearsLabels(GrpIndReg, 0) + ")"
        ''End If
        ''grf_Scatter.SubsetsToLegend(GrpIndReg + 1) = GrpIndReg + 1
        ''grf_Scatter.SubsetsToLegend(0) = -1
        ''grf_Scatter.MainTitle = ""
        ''If RegGrp = 1 Then
        ''    grf_Scatter.SubsetColors(GrpIndReg + 1) = grf_Scatter.PEargb(255, 0, 0, 150) 'Blue
        ''Else
        ''    If RegGrp = 2 Then
        ''        grf_Scatter.SubsetColors(GrpIndReg + 1) = grf_Scatter.PEargb(255, 0, 150, 0) 'Green
        ''    Else
        ''        If RegGrp = 3 Then
        ''            grf_Scatter.SubsetColors(GrpIndReg + 1) = QBColorPE(4) 'grf_Scatter.PEargb(255, 255, 0, 0) 'Red
        ''        Else
        ''            grf_Scatter.SubsetColors(GrpIndReg + 1) = grf_Scatter.PEargb(255, 255, 255, 0) 'Yellow
        ''        End If
        ''    End If
        ''End If



        ''''''''''''''''''''''''''''''''''''''''''''''''


        '    grf_Scatter.SubsetsToLegend(0) = -1
        '    grf_Scatter.SubsetsToLegend(1) = -1
        ''    'JAM 2009-08-07
        '    grf_Scatter.SubsetsToLegend(GrpIndReg) = -1


        Session("RegressFormul") = Equation
        Session("RsqrFormul") = Format(RSQR, RFormat)

        'JAM 2016-10-09
        If GrpIndReg = 0 Then
            'JAM 2016-11-21
            Session("MulRegressTitle") = "<b>" & Session("GLNameJoinPrev")(GrpIndReg) & "(" + Session("YearsLabels")(GrpIndReg, 0) + "): " & Equation & " , R(SQR) = " & Format(RSQR, RFormat) & "</b><br/>"
        Else
            'JAM 2016-11-21
            Session("MulRegressTitle") = Session("MulRegressTitle") & "<b>" & Session("GLNameJoinPrev")(GrpIndReg) & "(" + Session("YearsLabels")(GrpIndReg, 0) + "): " & Equation & " , R(SQR) = " & Format(RSQR, RFormat) & "</b><br/>"

        End If




        ''grf_Scatter.PlottingMethods(GrpIndReg + 1) = PEGPM_LINE

        ' ''JRS 2007/06/11
        ''grf_Scatter.SubsetLineTypes(GrpIndReg + 1) = PELT_MEDIUM_SOLID


        If Session("IsaGlist") Then
            Session("CCount") = Session("GListCount")

        End If

        ''grf_Scatter.AllowDataHotSpots = False
        ''grf_Scatter.AllowSubsetHotSpots = False
        ''grf_Scatter.MarkDataPoints = False
        ''grf_Scatter.HotSpotSize = PEHSS_SMALL
        ''mnuTurnOff.Caption = "Turn On"

        'JAM 2012-07-19
        ''Call KeepFormulationOff()



        Exit Sub
MulRegressPlot_Error:
        ' MsgBox("Error  MulRegressPlot() routine")
        Exit Sub

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       'JAM 2009-08-04     Function to calculate R-Squared in MultipleRegress
    '
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub MulCalcRSQR(ByVal vTransform As String, ByVal vGrpIndReg As Integer)
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation-new proc

        On Error GoTo MulCalcRSQR_Error 'JAM 2016-11-24

        Dim YBAR As Double, SUMOFYSQR As Double, SQROFYSUM As Double
        Dim SSE As Double, SST As Double
        Dim ObsNo As Integer
        Dim ForeTot, ForeMean As Double
        Dim XReal As Double


        ForeTot = 0
        ForeMean = 0

        SSE = 0
        SST = 0
        YBAR = 0
        SUMOFYSQR = 0
        ObsNo = 0




        For rr% = 1 To Session("CCount")
            'jrs 2007/06/12 added and isNumeric
            If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then

                If Not ((Session("OrigPrevCVarI")(vGrpIndReg, rr%)) Is Nothing) And Not (Session("OrigPrevCVarD")(vGrpIndReg, rr%) Is Nothing) And Session("MulRCountryRegress")(vGrpIndReg, rr%) And IsNumeric(Session("OrigPrevCVarI")(vGrpIndReg, rr%)) And IsNumeric(Session("OrigPrevCVarD")(vGrpIndReg, rr%)) Then
                    Select Case vTransform
                        Case "LINEAR"
                            XReal = Session("OrigPrevCVarI")(vGrpIndReg, rr%)
                        Case "LOG"
                            XReal = Math.Log(Session("OrigPrevCVarI")(vGrpIndReg, rr%))
                        Case "EXP"
                            XReal = Math.Exp(Session("OrigPrevCVarI")(vGrpIndReg, rr%))
                        Case "POW"
                            'jrs 2017/10/23 XReal = ((Session("OrigPrevCVarI")(vGrpIndReg, rr%)) ^ (Session("PowerDegree")))
                            XReal = Math.Log(Session("OrigPrevCVarI")(vGrpIndReg, rr%))
                        Case Else
                            XReal = Session("OrigPrevCVarI")(vGrpIndReg, rr%)
                    End Select

                    'jrs 2017/10/23
                    If vTransform = "POW" Then
                        SSE = SSE + (Math.Log(Session("OrigPrevCVarD")(vGrpIndReg, rr%)) - (vreg.RegVal(CDbl(XReal)))) ^ 2
                        YBAR = YBAR + Math.Log(Session("OrigPrevCVarD")(vGrpIndReg, rr%))
                        SUMOFYSQR = SUMOFYSQR + Math.Log(Session("OrigPrevCVarD")(vGrpIndReg, rr%)) ^ 2
                    Else
                        SSE = SSE + (Session("OrigPrevCVarD")(vGrpIndReg, rr%) - (vreg.RegVal(CDbl(XReal)))) ^ 2
                        YBAR = YBAR + Session("OrigPrevCVarD")(vGrpIndReg, rr%)
                        SUMOFYSQR = SUMOFYSQR + Session("OrigPrevCVarD")(vGrpIndReg, rr%) ^ 2
                    End If
                    ObsNo = ObsNo + 1
                End If
            Else
                'jrs 2008/07/10 added not isempty(CVARI)
                'IsNumeric(Empty) = true!
                If Not ((Session("JoinPrevCVarD")(vGrpIndReg, rr%)) Is Nothing) And Not ((Session("JoinPrevCVarI")(vGrpIndReg, rr%)) Is Nothing) And IsNumeric(Session("JoinPrevCVarD")(vGrpIndReg, rr%)) And IsNumeric(Session("JoinPrevCVarI")(vGrpIndReg, rr%)) Then
                    Select Case vTransform
                        Case "LINEAR"
                            XReal = Session("JoinPrevCVarI")(vGrpIndReg, rr%)
                        Case "LOG"
                            XReal = Math.Log(Session("JoinPrevCVarI")(vGrpIndReg, rr%))
                        Case "EXP"
                            XReal = Math.Exp(Session("JoinPrevCVarI")(vGrpIndReg, rr%))
                        Case "POW"
                            'jrs 2017/10/23 XReal = ((Session("JoinPrevCVarI")(vGrpIndReg, rr%)) ^ (Session("PowerDegree")))
                            XReal = Math.Log(Session("JoinPrevCVarI")(vGrpIndReg, rr%))
                        Case Else
                            XReal = Session("JoinPrevCVarI")(vGrpIndReg, rr%)
                    End Select

                    'jrs 2017/10/23
                    If vTransform = "POW" Then
                        SSE = SSE + (Math.Log(Session("JoinPrevCVarD")(vGrpIndReg, rr%)) - (vreg.RegVal(CDbl(XReal)))) ^ 2
                        YBAR = YBAR + Math.Log(Session("JoinPrevCVarD")(vGrpIndReg, rr%))
                        SUMOFYSQR = SUMOFYSQR + Math.Log(Session("JoinPrevCVarD")(vGrpIndReg, rr%)) ^ 2
                    Else
                        SSE = SSE + (Session("JoinPrevCVarD")(vGrpIndReg, rr%) - (vreg.RegVal(CDbl(XReal)))) ^ 2
                        YBAR = YBAR + Session("JoinPrevCVarD")(vGrpIndReg, rr%)
                        SUMOFYSQR = SUMOFYSQR + Session("JoinPrevCVarD")(vGrpIndReg, rr%) ^ 2
                    End If
                    ObsNo = ObsNo + 1
                End If


            End If

        Next rr%
        SQROFYSUM = YBAR ^ 2
        YBAR = YBAR / ObsNo


        For rr% = 1 To Session("CCount")
            'jrs 2007/06/12 added and isNumeric
            If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                If Not ((Session("OrigPrevCVarI")(vGrpIndReg, rr%)) Is Nothing) And Not (Session("OrigPrevCVarD")(vGrpIndReg, rr%) Is Nothing) And Session("MulRCountryRegress")(vGrpIndReg, rr%) And IsNumeric(Session("OrigPrevCVarI")(vGrpIndReg, rr%)) And IsNumeric(Session("OrigPrevCVarD")(vGrpIndReg, rr%)) Then
                    'jrs 2017/10/23 SST = SST + (Session("OrigPrevCVarD")(vGrpIndReg, rr%) - YBAR) ^ 2
                    If vTransform = "POW" Then
                        SST = SST + (Math.Log(Session("OrigPrevCVarD")(vGrpIndReg, rr%)) - YBAR) ^ 2
                    Else
                        SST = SST + (Session("OrigPrevCVarD")(vGrpIndReg, rr%) - YBAR) ^ 2
                    End If
                End If
            Else
                'jrs 2008/07/10 added not isempty(CVARI)
                'IsNumeric(Empty) = true!
                If Not (Session("JoinPrevCVarD")(vGrpIndReg, rr%) Is Nothing) And Not (Session("JoinPrevCVarI")(vGrpIndReg, rr%) Is Nothing) And IsNumeric(Session("JoinPrevCVarD")(vGrpIndReg, rr%)) And IsNumeric(Session("JoinPrevCVarI")(vGrpIndReg, rr%)) Then
                    'jrs 2017/10/23 SST = SST + (Session("JoinPrevCVarD")(vGrpIndReg, rr%) - YBAR) ^ 2
                    If vTransform = "POW" Then
                        SST = SST + (Math.Log(Session("JoinPrevCVarD")(vGrpIndReg, rr%)) - YBAR) ^ 2
                    Else
                        SST = SST + (Session("JoinPrevCVarD")(vGrpIndReg, rr%) - YBAR) ^ 2
                    End If
                End If
            End If


        Next rr%
        '    SST = SUMOFYSQR - (SQROFYSUM / ObsNo)
        RSQR = 1 - (SSE / SST)

        Exit Sub
MulCalcRSQR_Error:
        ' MsgBox("Error  MulRegressPlot() routine")
        Exit Sub

    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '   JAM 2016-10-08      Function to calculate Min and Max Values for selected GList
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub FindMinMaxRegress()


        On Error GoTo FindMinMaxRegress_Error 'JAM 2016-11-24

        Dim i, j As Integer





        Session("MulRMinVI") = 1000000000
        Session("MulRMaxVI") = -1000000000



        For i = 0 To Session("TotMulGroups") - 1
            For j = 1 To Session("DimJoinPrev")(i) 'CCount

                If Not (Session("JoinPrevCVarI")(i, j)) Is Nothing And IsNumeric(Session("JoinPrevCVarI")(i, j)) Then

                    'jrs 2007/06/11
                    If Session("MulRMinVI") > Session("JoinPrevCVarI")(i, j) And IsNumeric(Session("JoinPrevCVarD")(i, j)) And Not ((Session("JoinPrevCVarD")(i, j)) Is Nothing) Then Session("MulRMinVI") = Session("JoinPrevCVarI")(i, j)
                    If Session("MulRMaxVI") < Session("JoinPrevCVarI")(i, j) And IsNumeric(Session("JoinPrevCVarD")(i, j)) And Not ((Session("JoinPrevCVarD")(i, j)) Is Nothing) Then Session("MulRMaxVI") = Session("JoinPrevCVarI")(i, j)
                End If

            Next j
        Next i

        Exit Sub
FindMinMaxRegress_Error:
        ' MsgBox("Error  MulRegressPlot() routine")
        Exit Sub
    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''  JAM 2016-10-30 Including variables that matching
    ''' 
    ''' </summary>
    ''' <param name="ValArrayDep">Dependent Array </param>
    ''' <param name="ValArrayInd">Independent Array</param>
    ''' <remarks></remarks>


    Private Sub FilterVal(ByRef ValArrayDep As Object, ByRef ValArrayInd As Object)

        On Error GoTo FilterVal_Error 'JAM 2016-11-24

        'Match Dependent and Independent Values Array
        For i = 1 To Session("TotCountries")
            If Not IsNumeric(Session("FullDep")(i)) Or Not IsNumeric(Session("FullInd")(i)) Then
                ValArrayDep(i) = Nothing
                ValArrayInd(i) = Nothing
                Session("ExcludeCtry")(i) = True
            End If
        Next i

        Exit Sub
FilterVal_Error:
        'MsgBox("Error  FilterVal() routine")
        Exit Sub

    End Sub

    'jrs 2023/07/25
    Private Sub LoadAllYears()
        On Error GoTo LoadAllYears_Error

        Dim DepTblName As String, IndTblName As String, SecIndTblName As String

        Dim db As String
        Dim valtbl As DataTable
        If Session("GroupName") <> "Automobile" Then
            'Set db = OpenDatabase(IFsHistSeries_MDB)
            db = Session("IFsHistSeries_MDB")
        Else
            db = Session("IFsCoVaTrASeries_MDB")
        End If

        DepTblName = Session("TblDepYear")(1)
        IndTblName = Session("TblIndYear")(1)
        If Session("ThirdDimOk") Then
            SecIndTblName = Session("TblSecIndYear")(1)
        End If

        Dim CountDep As Integer = 0
        'Loading Dependent Variable
        SetDataTablefromSQLite(db, "SELECT * FROM [" + DepTblName + "] ORDER BY upper(Country)", "", valtbl, "")
        ReDim Session("AllDepSeries")(Session("TotCountries"), valtbl.Columns.Count - 4)
        Session("DepTblFirstYear") = valtbl.Columns(2).ColumnName

        For iCount% = 1 To Session("TotCountries")
            For j = 2 To valtbl.Columns.Count - 3
                Session("AllDepSeries")(iCount%, j - 1) = valtbl.Rows(iCount% - 1).Item(j)
            Next
            CountDep += 1
        Next iCount%

        Session("CountDep") = CountDep

        'Loading First Independent Variable
        SetDataTablefromSQLite(db, "SELECT * FROM [" + IndTblName + "] ORDER BY upper(Country)", "", valtbl, "")
        ReDim Session("AllIndSeries")(Session("TotCountries"), valtbl.Columns.Count - 4)
        Session("IndTblFirstYear") = valtbl.Columns(2).ColumnName

        For iCount% = 1 To Session("TotCountries")
            For j = 2 To valtbl.Columns.Count - 3
                Session("AllIndSeries")(iCount%, j - 1) = valtbl.Rows(iCount% - 1).Item(j)
            Next
        Next iCount%

        'Loading Second Independent Variable
        If Session("ThirdDimOk") Then
            SetDataTablefromSQLite(db, "SELECT * FROM [" + SecIndTblName + "] ORDER BY upper(Country)", "", valtbl, "")
            Session("SecIndTblFirstYear") = valtbl.Columns(2).ColumnName

            ReDim Session("AllSecIndSeries")(Session("TotCountries"), valtbl.Columns.Count - 4)

            For iCount% = 1 To Session("TotCountries")
                For j = 2 To valtbl.Columns.Count - 3
                    Session("AllSecIndSeries")(iCount%, j - 1) = valtbl.Rows(iCount% - 1).Item(j)
                Next
            Next iCount%
        End If

        Exit Sub
LoadAllYears_Error:
        MsgBox("Error occurred in LoadAllYears: " & Err.Description)

    End Sub

End Class
