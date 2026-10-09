Option Strict Off
Option Explicit On

Partial Class frm_AboutMDG
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        InitializeComponent()
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


                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Carret = "<br>" & Chr(10)
                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    carret2 = Carret + Carret

                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    msg = "This software is based on the International Futures (IFs) platform," + Carret
                    msg = msg & " copyright © 1988-2024 by Barry B. Hughes." & carret2
                    msg = msg & " Web implementation by José R. Solórzano, Mohammod T. Irfan, " + Carret
                    msg = msg & " and Chris Grubb, with data support from Anwar Hossain." & carret2
                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    msg = msg & "Contact Barry B. Hughes at: " + Carret
                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    msg = msg & "     Graduate School of International Studies" + Carret
                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    msg = msg & "     University of Denver" + Carret
                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    msg = msg & "     Denver, Colorado  80208 or" + Carret
                    msg = msg & "     <a href='mailto:bhughes@du.edu'>bhughes@du.edu</a>. See also <a href='http://www.du.edu/~bhughes'>www.du.edu/~bhughes</a>" & carret2

                    msg = msg & "Version Information:" + Carret
                    'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    msg = msg & "     " & ModelName + Carret
                    msg = msg & "     " & ModelVersion
                    If ModelLoad = "LoadStudent" Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        msg = msg + Carret + "     " + "Student Edition"
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        msg = msg + Carret + "     " + "Professional Edition"
                    End If
                Case "frm_ProEssentialWVS.aspx"
                    WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = "About World Value Survey"


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


    Public Sub Continue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.html'.
        Support.FormManager.Close(Me)
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_MDG.aspx")
        'frm_MainMenu.Show() 

    End Sub

    '=============================  AboutModel.Frm  ==============
    'CONVERSION_NOTE: The code of Event 'frm_AboutModel.frm_AboutModel_Load' was moved to Method 'frm_AboutModel.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_AboutMDG_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_fa05"
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_MDG.aspx"
        cswmGroup1.OnMenuBarClick = "location.href='frm_MDG.aspx';"

        Dim intF As Integer, Line As String
        lbl_header.Text = ""
        intF = FreeFile()
        FileOpen(intF, Session("MDGAboutFile"), OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
        Do While Not EOF(intF)
            Line = LineInput(intF)
            lbl_header.Text = lbl_header.Text & Line & "<br>"
        Loop
        FileClose(intF)
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
