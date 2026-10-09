Option Strict Off
Option Explicit On


Partial Class frm_Control
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
        Me.ControlArrayInit()
        'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim ICount As Short
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            '    Call ScaledGraph

            lbl_Eqn.Text = "Y = a + b1*X + b2*X^2 + b3* X^3 + b4*ln(X) "
            lbl_Eqn.Text = lbl_Eqn.Text & "+ b5*X^b6 + b7*e^(b8 * X)"
            For ICount = 1 To 8
                lbl_bI(ICount).Text = "b" & Str(ICount)
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                txt_bI(ICount).Text = Session("bI")(ICount)
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                txt_bD(ICount).Text = Session("bD")(ICount)
            Next ICount
            'UPGRADE_WARNING: Couldn't resolve default property of object aI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            txt_aI.Text = Session("aI")
            'UPGRADE_WARNING: Couldn't resolve default property of object aD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            txt_aD.Text = Session("aD")
            lbl_Ind.Text = Session("VarNameI")(0) & " controlled by " & Session("VarNameC")
            lbl_Dep.Text = Session("VarNameD") & " controlled by " & Session("VarNameC")
            'jrs 09/15/04
            Session("RefreshGraph") = "true"
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
    Public lbl_bI As System.Collections.ArrayList
    Public txt_bD As System.Collections.ArrayList
    Public txt_bI As System.Collections.ArrayList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Sub ControlArrayInit()
        Me.lbl_bI = New System.Collections.ArrayList()
        Me.txt_bD = New System.Collections.ArrayList()
        Me.txt_bI = New System.Collections.ArrayList()
        Me.lbl_bI.Add(_lbl_bI_1)
        Me.lbl_bI.Add(_lbl_bI_2)
        Me.lbl_bI.Add(_lbl_bI_3)
        Me.lbl_bI.Add(_lbl_bI_4)
        Me.lbl_bI.Add(_lbl_bI_5)
        Me.lbl_bI.Add(_lbl_bI_6)
        Me.lbl_bI.Add(_lbl_bI_7)
        Me.lbl_bI.Add(_lbl_bI_8)
        Me.txt_bD.Add(_txt_bD_1)
        Me.txt_bD.Add(_txt_bD_2)
        Me.txt_bD.Add(_txt_bD_3)
        Me.txt_bD.Add(_txt_bD_4)
        Me.txt_bD.Add(_txt_bD_5)
        Me.txt_bD.Add(_txt_bD_6)
        Me.txt_bD.Add(_txt_bD_7)
        Me.txt_bD.Add(_txt_bD_8)
        Me.txt_bI.Add(_txt_bI_1)
        Me.txt_bI.Add(_txt_bI_2)
        Me.txt_bI.Add(_txt_bI_3)
        Me.txt_bI.Add(_txt_bI_4)
        Me.txt_bI.Add(_txt_bI_5)
        Me.txt_bI.Add(_txt_bI_6)
        Me.txt_bI.Add(_txt_bI_7)
        Me.txt_bI.Add(_txt_bI_8)
    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_ControlPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_Control
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_Control()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    '--------------------------------------Control.Frm-----------------------------
    '------------------------------------------------------------------------------
    '





    '=========================Control.Frm=======================
    Private Sub cmd_Exit2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit2.Click
        Dim DV As Object
        Dim IV As Object
        Dim CV As Object
        Dim ICount As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Copy parameters from screen to analytic function
        '   ------------------------------------------------
        For ICount = 1 To 8
            'UPGRADE_WARNING: Couldn't resolve default property of object bI(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("bI")(ICount) = Val(txt_bI(ICount).Text)
            'UPGRADE_WARNING: Couldn't resolve default property of object bD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("bD")(ICount) = Val(txt_bD(ICount).Text)
        Next ICount
        'UPGRADE_WARNING: Couldn't resolve default property of object bI(6). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("bI")(6) > 5 Then Session("bI")(6) = 5
        'UPGRADE_WARNING: Couldn't resolve default property of object aI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("aI") = Val(txt_aI.Text)
        'UPGRADE_WARNING: Couldn't resolve default property of object bD(6). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("bD")(6) > 5 Then Session("bD")(6) = 5
        'UPGRADE_WARNING: Couldn't resolve default property of object aD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("aD") = Val(txt_aD.Text)

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For ICount = 1 To Session("CCount")
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CV = Session("CVarC")(ICount)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object IV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            IV = Session("CVarI")(ICount)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            If IsDBNull(IV) Or IsDBNull(CV) Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("CVarI")(ICount) = System.DBNull.Value
                'UPGRADE_WARNING: Couldn't resolve default property of object CV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf CV = 0 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object CV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(7). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(6). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(5). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(3). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object aI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object IV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("CVarI")(ICount) = IV - Session("aI") - Session("bI")(1) * CV - Session("bI")(2) * CV ^ 2 - Session("bI")(3) * CV ^ 3 - Session("bI")(5) * CV ^ Session("bI")(6) - Session("bI")(7) * System.Math.Exp(Session("bI")(8) * CV)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object CV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(7). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(6). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(5). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(4). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(3). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bI(2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object aI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object IV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("CVarI")(ICount) = IV - Session("aI") - Session("bI")(1) * CV - Session("bI")(2) * CV ^ 2 - Session("bI")(3) * CV ^ 3 - Session("bI")(4) * System.Math.Log(CV) - Session("bI")(5) * CV ^ Session("bI")(6) - Session("bI")(7) * System.Math.Exp(Session("bI")(8) * CV)
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CV = Session("CVarC")(ICount)
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object DV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DV = Session("CVarD")(ICount)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            If IsDBNull(DV) Or IsDBNull(CV) Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("CVarD")(ICount) = System.DBNull.Value
                'UPGRADE_WARNING: Couldn't resolve default property of object CV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf CV = 0 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object CV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(7). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(6). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(5). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(3). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object aD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object DV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("CVarD")(ICount) = DV - Session("aD") - Session("bD")(1) * CV - Session("bD")(2) * CV ^ 2 - Session("bD")(3) * CV ^ 3 - Session("bD")(5) * CV ^ Session("bD")(6) - Session("bD")(7) * System.Math.Exp(Session("bD")(8) * CV)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object CV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(7). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(6). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(5). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(4). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(3). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object bD(2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object aD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object DV. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("CVarD")(ICount) = DV - Session("aD") - Session("bD")(1) * CV - Session("bD")(2) * CV ^ 2 - Session("bD")(3) * CV ^ 3 - Session("bD")(4) * System.Math.Log(CV) - Session("bD")(5) * CV ^ Session("bD")(6) - Session("bD")(7) * System.Math.Exp(Session("bD")(8) * CV)
            End If
        Next ICount

        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        Support.FormManager.Close(Me)
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "Scatter.aspx") 'jrs 09/15/04
        'frm_Scatter.DefInstance.Show() 

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub


    '=========================Control.Frm=======================
    'CONVERSION_NOTE: The code of Event 'frm_Control.frm_Control_Load' was moved to Method 'frm_Control.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_Control_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    End Sub
End Class
