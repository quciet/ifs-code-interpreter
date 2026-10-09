Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_NameDisp
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
            Dim JCount As Short
            Dim VarFoundPrev As String
            Dim finam As String
            Dim FileNum As Object
            Dim ICount As Short
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            '   Set up screen
            '   -------------

            'CONVERSION_ISSUE: Class System.Windows.Forms.FormWindowState from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.WindowState.html'.
            'JRSVBNET WindowState = System.Windows.Forms.FormWindowState.Normal
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - Inch)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            'JRSVBNET Height = VB6.TwipsToPixelsY(Inch * 2.5)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Top.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            'JRSVBNET Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Left.html'.
            'JRSVBNET Left = VB6.TwipsToPixelsX(Inch / 2)
            lst_Defns.Style.Item("LEFT") = VB6.Support.TwipsToPixelsX(0.25 * Inch)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
            'JRSVBNET lst_Defns.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Width) - 0.5 * Inch))
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Carret = "<br>" & Chr(10)
            'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            carret2 = Carret + Carret

            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            lst_Defns.Items.Clear()
            For ICount = 1 To Session("COUNTE")
                If InStr(1, Session("VARN")(1, ICount), "[") = 0 Then '[ not found
                    'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    FileNum = 0
                    finam = "Working File"
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    FileNum = Mid(Session("VARN")(1, ICount), InStr(1, Session("VARN")(1, ICount), "[") + 1, 1)
                    'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If FileNum = "-" Then FileNum = -1
                    'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If FileNum = -1 Then
                        finam = "History Data from IFS_HistSeries.MDB"
                        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf FileNum = 0 Then
                        finam = "Working File, based on: " & Session("WorkingFile")
                        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf FileNum = 1 Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        finam = "Base Case: " & Session("RunFiles")(FileNum)
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        finam = Session("RunFiles")(FileNum)
                    End If
                End If
                finam = "[" & finam & "]"
                If ICount = 1 Then
                    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    lst_Defns.Items.Add(New ListItem(Session("VARN")(1, ICount) & " = " & Session("VARN")(2, ICount) & finam))
                Else
                    VarFoundPrev = CStr(False)
                    For JCount = 1 To ICount - 1
                        If Session("VARN")(1, JCount) = Session("VARN")(1, ICount) Then
                            VarFoundPrev = CStr(True)
                        End If
                    Next JCount
                    If CBool(VarFoundPrev) = False Then
                        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        lst_Defns.Items.Add(New ListItem(Session("VARN")(1, ICount) & " = " & Session("VARN")(2, ICount) & finam))
                    End If
                End If
            Next ICount

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_NameDispPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_NameDisp
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6Form.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_NameDisp()
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
    '------------------------------------------NameDisp.Frm-------------------------------------
    '------------------------------------------------------------------------------------------
    '

    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last changed by BBH on 12/13/99

    '=============================NameDisp.Frm=======================
    Private Sub but_Print_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles but_Print.Click
        'JRSVBNET *** BEGIN ***
        '      Dim JCount As Short
        'Dim VarFoundPrev As String
        'Dim msg As String
        'Dim ICount As Short
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Carret = "<br>" & Chr(10)
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'carret2 = Carret + Carret

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()

        ''lst_Defns.Clear
        'For ICount = 1 To Session("COUNTE")
        '	If ICount = 1 Then
        '		msg = Session("VARN")(1, ICount) & " = " & Session("VARN")(2, ICount)
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print(msg)
        '	Else
        '		VarFoundPrev = CStr(False)
        '		For JCount = 1 To ICount - 1
        '			If Session("VARN")(1, JCount) = Session("VARN")(1, ICount) Then
        '				VarFoundPrev = CStr(True)
        '			End If
        '		Next JCount
        '		If CBool(VarFoundPrev) = False Then
        '			msg = Session("VARN")(1, ICount) & " = " & Session("VARN")(2, ICount)
        '			'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '			'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '			Printer.Print(msg)
        '		End If
        '	End If
        '	'shift this line up in the loop, or same thing repeated for all dimensions in first variable
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print()
        'Next ICount

        ''UPGRADE_ISSUE: Printer method Printer.NewPage was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.NewPage()
        ''UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.EndDoc()
        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'JRSVBNET *** END ***
    End Sub


    '=============================NameDisp.Frm=======================
    Private Sub cmd_Exit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form..html'.
        'JRS 02/05/04 ME=FORMNAME
        'jrs 09/22/04
        Support.FormManager.Show(Me, Session("NameDispParent"))

    End Sub


    '=============================NameDisp.Frm=======================
    'CONVERSION_NOTE: The code of Event 'frm_NameDisp.frm_NameDisp_Load' was moved to Method 'frm_NameDisp.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_NameDisp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    End Sub

End Class

