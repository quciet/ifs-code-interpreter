Option Strict Off
Option Explicit On

Partial Class frm_AboutModel
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
        Me.[Continue] = New ArtinSoft.WinFormsToWeb.WebControls.MenuItem()
        Me.[Continue].Name = "Continue"
        Me.Print_Renamed = New ArtinSoft.WinFormsToWeb.WebControls.MenuItem()
        Me.Print_Renamed.Name = "Print_Renamed"
        MainMenu1.Name = "MainMenu1"
        Me.[Continue].Text = "Continue"
        Me.[Continue].Enabled = True
        Me.[Continue].Visible = True
        Me.Print_Renamed.Text = "Print"
        Me.Print_Renamed.Enabled = True
        Me.Print_Renamed.Visible = True
        Me.[Continue].Index = 0
        Me.Print_Renamed.Index = 1
        MainMenu1.AddRange(New ArtinSoft.WinFormsToWeb.WebControls.MenuItem() {Me.[Continue], Me.Print_Renamed})
        Me.MainMenu1.RegisterFunction(Print_Renamed.Name, AddressOf Print_Renamed_Click)
        MainMenu1.SetActive(True)
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            'mti August 2004 5 -start
            Select Case Session("AboutParent")
                Case "frm_MainMenu.aspx"
                    WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "About This Software"


                    Carret = "<br>" & Chr(10)
                    carret2 = Carret + Carret

                    msg = "This software is based on the International Futures (IFs) platform," + Carret
                    msg = msg & " copyright &copy; 1989-" & CStr(Year(Now)) & " by Barry B. Hughes." & carret2
                    msg = msg & " Web implementation by Jos&eacute; R. Sol&oacute;rzano and Jaime Melendez. " & carret2
                    msg = msg & " Model developed by Barry Hughes, Mohammod Irfan, Jos&eacute; Sol&oacute;rzano, Jonathan Moyer and others. " & carret2
                    msg = msg & " Data support from Yutang Xiong. " & carret2
                    msg = msg & "Contact the Pardee Institue at: " + Carret
                    msg = msg & "     Frederick S. Pardee Institute for International Futures" + Carret
                    msg = msg & "     Josef Korbel School of International Studies" + Carret
                    msg = msg & "     University of Denver" + Carret
                    msg = msg & "     Denver, Colorado  80208 " + Carret
                    msg = msg & "     <a href='mailto:pardee.institute@du.edu'>pardee.institute@du.edu</a>. See also <a href='https://korbel.du.edu/pardee'>korbel.du.edu/pardee</a>" & carret2

                    msg = msg & "Version Information:" + Carret
                    msg = msg & "     " & ModelName + Carret
                    msg = msg & "     " & ModelVersion
                    If ModelLoad = "LoadStudent" Then
                        msg = msg + Carret + "     " + "Student Edition"
                    Else
                        msg = msg + Carret + "     " + "Professional Edition"
                    End If
                Case "frm_WVSDisplay.aspx"
                    WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "About World Value Survey"

                    Carret = "<br>" & Chr(10)
                    carret2 = Carret + Carret


                    msg$ = "The World Value Survey (WVS) is a global project, based at the Institute for Social Research" + Carret
                    msg$ = msg & "at the University of Michigan.  WVS was founded and is directed by Ronald Inglehart." & carret2
                    msg$ = msg & "The implementation of WVS forecasting in IFs was undertaken in collaboration with" + Carret
                    msg$ = msg & "Professor Inglehart.  For more information on WVS see" & carret2
                    msg$ = msg & "     Ronald Inglehart, Modernization and Postmodernization:  " + Carret
                    msg$ = msg & "     Cultural, Economic, and Political Change in 43 Societies. " & carret2
                    msg$ = msg & "     Princeton, NJ: Princeton University Press, 1997.  "
            End Select

            'mti August 2004 5 -end 
            lbl_header.Text = msg
            msg = ""

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
    'CONVERSION_NOTE: Class ''JRSVBNET System.Windows.Forms.ToolTip' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public ToolTip1 As 'JRSVBNET System.Windows.Forms.ToolTip 
    Public [Continue] As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public Print_Renamed As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_AboutModelPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_AboutModel
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6Form.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_AboutModel()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    '--------------------------------------AboutModel.Frm-----------------------------
    '------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last Changed:  December 23, 2000



    '=============================  AboutModel.Frm  ==============
    'CONVERSION_NOTE: The code of Event 'frm_AboutModel.frm_AboutModel_Load' was moved to Method 'frm_AboutModel.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_AboutModel_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_fa05"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_MainMenu.aspx"
        If Session("AboutParent") = "frm_WVSDisplay.aspx" Then
            If Not IsNothing(Request.QueryString("Country")) Then
                cswmGroup1.OnMenuBarClick = "location.href='frm_WVSDisplay.aspx?Country=" & PrepareForURL(Request.QueryString("Country")) & "';"
            ElseIf Not IsNothing(Request.QueryString("Group")) Then
                cswmGroup1.OnMenuBarClick = "location.href='frm_WVSDisplay.aspx?Group=" & PrepareForURL(Request.QueryString("Group")) & "';"
            Else
                cswmGroup1.OnMenuBarClick = "location.href='frm_WVSDisplay.aspx';"
            End If
        Else
            cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        End If
    End Sub

    Public Sub Print_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN ***
        '      Dim msg As String
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        ''JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor

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

        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = "This software is based on the International Futures (IFs) platform." + Carret
        'msg = msg & " Developed by and copyright © 1988-2003 by Barry B. Hughes" & carret2
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "Contact Barry B. Hughes at: " + Carret
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "     Graduate School of International Studies" + Carret
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "     University of Denver" + Carret
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "     Denver, Colorado  80208" + Carret
        'msg = msg & "     bhughes@du.edu     www.du.edu/~bhughes" & carret2
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "Warning: All components of International Futures (IFs) are" + Carret
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "     protected by copyright law and international treaties." + Carret
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "     Unauthorized reproduction or distribution of any portion of" + Carret
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "     International Futures may result in severe civil and " + Carret
        'msg = msg & "     criminal penalties." & carret2
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "Version Information:" + Carret
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'msg = msg & "     " & ModelName + Carret
        'msg = msg & "     " & ModelVersion
        'If ModelLoad = "LoadStudent" Then
        '	'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '	msg = msg + Carret + "     " + "Student Edition"
        'Else
        '	'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '	msg = msg + Carret + "     " + "Professional Edition"
        'End If

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print(msg)
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()
        ''UPGRADE_ISSUE: Printer method Printer.NewPage was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.NewPage()
        ''UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.EndDoc()

        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        ''JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        'JRSVBNET *** END ***
    End Sub
End Class

