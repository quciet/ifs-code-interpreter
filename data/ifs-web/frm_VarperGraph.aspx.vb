Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'                   JAM 2009-06-25      Parameter form to define the Number variables per Graph
'                                       based in frm_DisplayControl.aspx developed by Jose Solorzano
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Partial Class frm_VarperGraph
    Inherits Page
    Dim InitialVarGraph
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ' InitializeComponent()
        If Not Page.IsPostBack Then
            InitialVarGraph = Session("ItemsbySet")
            txt_VarperGraph.Text = Session("ItemsbySet")
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
    '<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    'End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_DisplayControlPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_DisplayControl
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_DisplayControl()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    'JRS 5/6/02
    'Display Control for Graphics

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        frm_VarperGraph_Closed()
        'jrs 2012/12/14
        Dim pos As Integer, pos2 As Integer, i As Integer, sOldVarperG As String
        pos = InStr(Session("VarperGraphParent"), "VarPerG")
        pos2 = InStr(Session("VarperGraphParent"), "?")
        If pos > 0 Then
            For i = pos + 1 To Len(Session("VarperGraphParent"))
                If Mid(Session("VarperGraphParent"), pos, 1) = "?" Then Exit For
                sOldVarperG = sOldVarperG & Mid(Session("VarperGraphParent"), pos, 1)
            Next
            Session("VarperGraphParent") = Replace(Session("VarperGraphParent"), "VarperG=" & sOldVarperG, "VarperG=" & Session("VarperG"))
        Else
            If pos2 > 0 Then
                Session("VarperGraphParent") = Session("VarperGraphParent") & "&"
            Else
                Session("VarperGraphParent") = Session("VarperGraphParent") & "?"
            End If
            Session("VarperGraphParent") = Session("VarperGraphParent") & "VarPerG=" & Session("VarperG")
        End If
        'Support.FormManager.StateStore(Me)
        'JAM 2009-07-03 commented to return to main form
        Support.FormManager.Show(Me, Session("VarperGraphParent"))
        'Support.FormManager.Show(Me, Session("DisplayControlParent"))

    End Sub

    'CONVERSION_NOTE: The code of Event 'frm_DisplayControl.frm_DisplayControl_Load' was moved to Method 'frm_DisplayControl.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_VarperGraph_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub frm_VarperGraph_Closed()
        Dim a As Double

        If Not IsNumeric(txt_VarperGraph.Text) Then

            Session("VarperG") = InitialVarGraph

        Else
            a = CDbl(txt_VarperGraph.Text)
            Session("VarperG") = CDbl(txt_VarperGraph.Text)


        End If
        'JAM 2009-07-15
        If Session("VarperG") = Session("ItemsbySet") Then
            Session("IsChangedNum") = False
        Else
            Session("IsChangedNum") = True
        End If
        Session("SourceRadial") = "VarperGraph"
        'Session("bDisplayControl") = True
    End Sub

    Private Sub Txt_VarperGraph_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Not IsNumeric(txt_VarperGraph.Text) Then
            txt_VarperGraph.Text = InitialVarGraph
        End If
    End Sub
End Class
