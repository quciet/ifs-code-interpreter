Option Strict Off
Option Explicit On

Partial Class frm_NotePad
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
            'jrs 2005/02/16
            Dim fNum As Integer
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'jrs 2005/03/15
            If Not Session("AnnotationFile") = Nothing Then
                If Dir(Session("AnnotationFile")) <> "" Then
                    fNum = FreeFile()
                    FileOpen(fNum, Session("AnnotationFile"), OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
                    Dim Scenariotype As String, Var As String
                    Do While Not EOF(fNum)
                        Input(fNum, Scenariotype)
                        Input(fNum, Var)
                        If Mid(Scenariotype, 1, 3) = "COM" Then
                            Var = ""
                            Do While Not EOF(fNum)
                                Var = Var & InputString(fNum, 1)
                            Loop
                            Exit Do
                        Else
                            Var = LineInput(fNum)
                        End If
                    Loop
                    rtf1.Text = Var
                    FileClose(fNum)
                    'jrs 2005/05/05
                    rtf1.Text = Replace(rtf1.Text, vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf, vbCrLf)
                    rtf1.Text = Replace(rtf1.Text, vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf, vbCrLf)
                    'jrs 2005/05/04
                    rtf1.Text = rtf1.Text & BuildSceDesc(Session("AnnotationFile"), Session("SEL"), Session("COUNTE"),
                    Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"),
                    Session("FDNG4"), Session("FdrvRun"), Session("PutFdrvRun"), Session("RunFiles"), Session("IFs_MDB2Active"),
                    Session("FinYear"), Session("PercentOn"), Session("ActiveBase"),
                    Session("bLoadForDisplaySuccess"), Me, Session("ShortSce"), Session("FileforDisplay"), Session("CurrencyVal"), Session("CurrencySym"),
                    Session("SensitivityFlag"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                End If

            ElseIf Dir(Session("ScenarioFileDir") & "working.txt") <> "" Then
                fNum = FreeFile()
                FileOpen(fNum, Session("ScenarioFileDir") & "working.txt", OpenMode.Input)
                rtf1.Text = InputString(fNum, LOF(fNum))
                FileClose(fNum)
            End If

            If StandAloneMode Then
                cmdSave.Visible = True
                cmdClear.Visible = True
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
    'CONVERSION_NOTE: Class ''JRSVBNET System.Windows.Forms.ToolTip' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public ToolTip1 As 'JRSVBNET System.Windows.Forms.ToolTip 
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_NotePadPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_NotePad
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6Form.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_NotePad()
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
    '------------------------------------------NotePad.Frm-------------------------------------
    '------------------------------------------------------------------------------------------
    '

    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last changed by BBH on November 18, 2001

    '=============================NotePad.Frm=======================
    Private Sub cmdClear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClear.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'CONVERSION_NOTE: Property 'AxRichTextLib.AxRichTextBox.Text' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.AxHost.Text.html'.
        rtf1.Text = ""
        Kill(Session("ScenarioFileDir") & "Working.txt")
        'jrs 2005/02/16
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, Session("ScenarioFileDir") & "Working.Txt", OpenMode.Output)
        FileClose(fNum)

    End Sub

    '=============================NotePad.Frm=======================
    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2005/03/14
        ''jrs 2005/02/16
        'Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, Session("ScenarioFileDir") & "\working.txt", OpenMode.Output)

        ''CONVERSION_ISSUE: Property 'AxRichTextLib.AxRichTextBox.Text' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.AxHost.Text.html'.
        'PrintLine(fNum, rtf1.Text)
        'FileClose(fNum)

        'jrs 2015/01/09 Support.FormManager.Close(Me)
        If Session("GraphDisplayParent") = "frm_PackagedDisplaysFlex.aspx" Then
            Support.FormManager.Show(Me, Session("GraphDisplayParent"))
        Else
            Support.FormManager.Close(Me)
        End If

    End Sub

    '=============================NotePad.Frm=======================
    Private Sub cmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        Dim msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2005/02/16
        Dim fNum As Integer
        fNum = FreeFile()
        FileOpen(fNum, Session("ScenarioFileDir") & "working.txt", OpenMode.Output)

        'CONVERSION_ISSUE: Property 'AxRichTextLib.AxRichTextBox.Text' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.AxHost.Text.html'.
        PrintLine(fNum, rtf1.Text)
        FileClose(fNum)
        'JRS 10/30/03
        'CONVERSION_ISSUE: Property 'IFs.frm_ScenarioCreate.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form..html'.
        'frm_ScenarioCreate.blnScenarioDirty = True

        msg = "Your annotation has been saved to a temporary working file.  "
        msg = msg & "In order to save it with a named scenario that you are "
        msg = msg & "annotating, save the working file into "
        msg = msg & "the named file "
        msg = msg & "from the Scenario Development window (using the "
        msg = msg & "Scenario Files menu option).  "
        msg = msg & "Named files retain content (parameters and annotations) "
        msg = msg & "after you exit IFs."
        lblMsg.Text = msg
        lblMsg.Visible = True

        'jrs 2020/05/07 need to copy to working2.txt too
        FileCopy(Session("ScenarioFileDir") & "Working.txt", Session("ScenarioFileDir") & "Working2.txt")

        ''CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'WinFormsToWeb_Messagebox.show(msg)
        'open
    End Sub

    '=============================NotePad.Frm=======================
    'CONVERSION_NOTE: The code of Event 'frm_NotePad.frm_NotePad_Load' was moved to Method 'frm_NotePad.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_NotePad_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'jrs 2013/09/04
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
        ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)
    End Sub

End Class
