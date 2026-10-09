Option Strict Off
Option Explicit On

Partial Class frm_Analytic
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
            Dim ICount As Short
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            '   Load analytic parameters for analytic function, else zeros
            '   ----------------------------------------------------------

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("TFuncPoints")(Session("FuncInd")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("TFuncPoints")(Session("FuncInd")) = 0 Then
                For ICount = 1 To 8
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("BPoints")(Session("FuncInd"), ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("bI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("bI")(ICount) = Session("BPoints")(Session("FuncInd"), ICount)
                Next ICount
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("APoint")(Session("FuncInd")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("aI"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("aI") = Session("APoint")(Session("FuncInd"))
                'UPGRADE_WARNING: Couldn't resolve default property of object session("MinPoint")(Session("FuncInd")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                txt_bottom.Text = Session("MinPoint")(Session("FuncInd"))
                'UPGRADE_WARNING: Couldn't resolve default property of object session("MaxPoint")(Session("FuncInd")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                txt_Top.Text = Session("MaxPoint")(Session("FuncInd"))
            Else
                For ICount = 1 To 8
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("bI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("bI")(ICount) = 0
                Next ICount
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("aI"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("aI") = 0
                txt_bottom.Text = CStr(0)
                txt_Top.Text = CStr(0)
            End If

            '    If Points(Session("FuncInd")%) = 0 Then
            '        For ICount% = 1 To 8
            '             Session("bI")(ICount%) = XPts(Session("FuncInd")%, ICount%)
            '        Next ICount%
            '        Session("aI") = XPts(Session("FuncInd")%, 9)
            '        txt_bottom = YPts(Session("FuncInd")%, 1)
            '        txt_Top = YPts(Session("FuncInd")%, 2)
            '    Else
            '        For ICount% = 1 To 8
            '             Session("bI")(ICount%) = 0
            '        Next ICount%
            '        Session("aI") = 0
            '        txt_bottom = 0
            '        txt_Top = 0
            '    End If


            '   Set up screen appearance
            '   ------------------------

            lbl_Eqn.Text = "Y = a + b1*X + b2*X^2 + b3* X^3 + b4*ln(X) "
            lbl_Eqn.Text = lbl_Eqn.Text & "+ b5*X^b6 + b7*e^(b8 * X)"
            For ICount = 1 To 8
                lbl_bI(ICount - 1).Text = "b" & Str(ICount)
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("bI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                txt_bI(ICount - 1).Text = Session("bI")(ICount)
            Next ICount
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("aI"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            txt_aI.Text = Session("aI")

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
    Public txt_bI As System.Collections.ArrayList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Sub ControlArrayInit()
        Me.lbl_bI = New System.Collections.ArrayList
        Me.txt_bI = New System.Collections.ArrayList
        Me.lbl_bI.Add(_lbl_bI_1)
        Me.lbl_bI.Add(_lbl_bI_2)
        Me.lbl_bI.Add(_lbl_bI_3)
        Me.lbl_bI.Add(_lbl_bI_4)
        Me.lbl_bI.Add(_lbl_bI_5)
        Me.lbl_bI.Add(_lbl_bI_6)
        Me.lbl_bI.Add(_lbl_bI_7)
        Me.lbl_bI.Add(_lbl_bI_8)
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
    'Private Shared m_vb6FormDefInstance As frm_AnalyticPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_Analytic
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_Analytic()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    '--------------------------------------Analytic.Frm-----------------------------
    '------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last Changed:  October 26, 1999


    '=============================  Analytic.Frm  ==============
    Private Sub cmd_Exit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Return to TablFunction and redisplay graph
        '   ------------------------------------------
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        Support.FormManager.Close(Me)
        'JRSVBNET frm_TablFunc.DefInstance.Show()
        'JRSVBNET CreateGraph((Session("FuncInd")))
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    '=========================Analytic.Frm=======================
    Private Sub cmd_Exit2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit2.Click
        Dim ICount As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Copy parameters from screen to analytic function
        '   ------------------------------------------------
        '    For ICount% = 1 To 8
        '        Session("bI")(ICount%) = Val(txt_bI(ICount%))
        '    Next ICount%
        '    If Val(txt_Top) < 0.01 Then txt_Top = 0.01
        '    If Val(txt_bottom) < 0 Then txt_bottom = 0
        '    If Session("bI")(6) > 5 Then Session("bI")(6) = 5
        '    If Session("bI")(8) > 10 / txt_Top Then Session("bI")(8) = 10 / txt_Top
        '    Session("aI") = txt_aI
        '    Points(Session("FuncInd")%) = 0

        For ICount = 1 To 8
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("bI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("bI")(ICount) = Val(txt_bI(ICount - 1).Text)
        Next ICount
        If Val(txt_Top.Text) < 0.01 Then txt_Top.Text = CStr(0.01)
        If Val(txt_bottom.Text) < 0 Then txt_bottom.Text = CStr(0)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("bI")(6). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("bI")(6) > 5 Then Session("bI")(6) = 5
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("bI")(8). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("bI")(8) > 10 / CDbl(txt_Top.Text) Then Session("bI")(8) = 10 / CDbl(txt_Top.Text)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("aI"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("aI") = txt_aI.Text
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("TFuncPoints")(Session("FuncInd")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("TFuncPoints")(Session("FuncInd")) = 0


        '   Load analytic function into analytic parameters
        '   -----------------------------------------------
        '    For ICount% = 1 To 8
        '        XPts(Session("FuncInd")%, ICount%) = Session("bI")(ICount%)
        '    Next ICount%
        '    XPts(Session("FuncInd")%, 9) = Session("aI")
        '    YPts(Session("FuncInd")%, 1) = Val(txt_bottom)
        '    YPts(Session("FuncInd")%, 2) = Val(txt_Top)

        For ICount = 1 To 8
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("bI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("BPoints")(Session("FuncInd"), ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("BPoints")(Session("FuncInd"), ICount) = Session("bI")(ICount)
        Next ICount
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("aI"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("APoint")(Session("FuncInd")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("APoint")(Session("FuncInd")) = Session("aI")
        'UPGRADE_WARNING: Couldn't resolve default property of object session("MinPoint")(Session("FuncInd")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("MinPoint")(Session("FuncInd")) = Val(txt_bottom.Text)
        'UPGRADE_WARNING: Couldn't resolve default property of object session("MaxPoint")(Session("FuncInd")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("MaxPoint")(Session("FuncInd")) = Val(txt_Top.Text)

        'jrs 2006/01/03
        Dim db As String, sql As String, i As Integer, VarName As String, counter As Integer = 0
        db = Session("PutFdrvRun") & "TablFunc.mdb"
        sql = "UPDATE Tablfunc SET "
        For i = 1 To 8
            If IsNumeric(Session("BPoints")(Session("FuncInd"), i)) Then
                If counter > 0 Then
                    sql = sql & ", "
                End If
                counter = counter + 1
                VarName = "b" & Trim(Str(i)) & " = " & Session("BPoints")(Session("FuncInd"), i)
                sql = sql & VarName
            End If
        Next
        If IsNumeric(Session("APoint")(Session("FuncInd"))) Then
            If counter > 0 Then
                sql = sql & ", "
            End If
            sql = sql & "a = " & Session("APoint")(Session("FuncInd"))
            counter = counter + 1
        End If
        If IsNumeric(Session("MinPoint")(Session("FuncInd"))) Then
            If counter > 0 Then
                sql = sql & ", "
            End If
            sql = sql & "Minimum = " & Session("MinPoint")(Session("FuncInd"))
            counter = counter + 1
        End If
        If IsNumeric(Session("MaxPoint")(Session("FuncInd"))) Then
            If counter > 0 Then
                sql = sql & ", "
            End If
            sql = sql & "Maximum = " & Session("MaxPoint")(Session("FuncInd"))
            counter = counter + 1
        End If
        sql = sql & " WHERE [Function Name] = """ & FuncName(Session("FuncInd")) & """"
        updateAccessWith1Command(db, sql)
        'jrs 2023/08/01
        Dim dbWork As String = Session("PutFdrvRun") & "Working.run.db"
        sql = "UPDATE [ifs_reg_coeff] SET [Value] = " & Session("aI")
        sql = sql + " WHERE  [RegressionName] = """ & FuncName(Session("FuncInd")) & """ AND [Name]=""a"""
        updateSQLiteWith1Command(dbWork, sql)
        Dim tbl As DataTable
        sql = "SELECT * FROM ifs_reg_coeff WHERE RegressionName = """ & FuncName(Session("FuncInd")) & """"
        SetDataTablefromSQLite(dbWork, sql, "", tbl)
        Dim Coeffs As List(Of String) = New List(Of String)
        For i = 1 To tbl.Rows.Count
            Coeffs.Add(tbl.Rows(i - 1).Item("Name"))
        Next
        For i = 1 To 8
            If Coeffs.Contains("b" & i.ToString) Then
                sql = "UPDATE [ifs_reg_coeff] SET [Value] = " & Session("bI")(i)
                sql = sql + " WHERE  [RegressionName] = """ & FuncName(Session("FuncInd")) & """ AND [Name]=""b" & i.ToString & """"
            Else
                sql = "INSERT INTO [ifs_reg_coeff] VALUES(""" & FuncName(Session("FuncInd")) & """, 1, ""b" & i.ToString & """," & Session("bI")(i) & ")"
            End If
            updateSQLiteWith1Command(dbWork, sql)
        Next
        Session("ChangingFunctions") = True


        '   Return to TablFunction and redisplay graph
        '   ------------------------------------------
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        Support.FormManager.Close(Me)
        'JRSVBNET frm_TablFunc.DefInstance.Show()
        'JRSVBNET CreateGraph((Session("FuncInd")))

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub


    Private Sub Exit_Click()


    End Sub


    '=========================Analytic.Frm=======================
    'CONVERSION_NOTE: The code of Event 'frm_Analytic.frm_Analytic_Load' was moved to Method 'frm_Analytic.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_Analytic_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    End Sub

















    Private Sub Text1_Change()

    End Sub


    Private Sub SSCommand1_Click()

    End Sub
End Class
