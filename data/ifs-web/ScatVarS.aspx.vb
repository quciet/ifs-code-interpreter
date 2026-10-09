Option Strict Off
Option Explicit On
Imports System.Data.OleDb 'mtiwebforms April 2004 ADO.Net
Imports VB = Microsoft.VisualBasic

Partial Class frm_ScatVarS
    Inherits Page

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
        'mti this artinsoft generated code handles the converted array of optionbuttons
        Me.ControlArrayInit()
        'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.

        lstVarName.EnableViewState = False

        'JAM 2011-11-19 Defining default button for txtVarSearch field
        txtVarSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + btnEnterKey.ClientID + "')")

        'JAM 2013-08-10
        cmb_VCAT.Attributes.Add("onchange", "cmb_VCAT_click();")

        'JAM 2013-08-25
        cmb_OthFilt.Attributes.Add("onchange", "cmb_OthFilt_click();")

    End Sub

    'jrs 2006/01/02
    Public Sub SelectVar()
        cmdComputations.Visible = False
        cmd_Plot.Enabled = False

        cmd_Statistics.Enabled = False

        opt_Dependent.Enabled = False
        _opt_Independent_0.Enabled = False
        _opt_Independent_1.Enabled = False
        _opt_Independent_2.Enabled = False
        _opt_Independent_3.Enabled = False
        _opt_Independent_4.Enabled = False
        opt_Control.Enabled = False

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
    Public lbl_Ind As System.Collections.ArrayList
    Public opt_Independent As System.Collections.ArrayList
    'CONVERSION_NOTE: Following code was added for support purposes.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1016.html'.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        dataString = ""
    End Sub
    Sub ControlArrayInit()
        Me.lbl_Ind = New System.Collections.ArrayList
        Me.opt_Independent = New System.Collections.ArrayList
        Me.lbl_Ind.Add(_lbl_Ind_0)
        Me.lbl_Ind.Add(_lbl_Ind_1)
        Me.lbl_Ind.Add(_lbl_Ind_2)
        Me.lbl_Ind.Add(_lbl_Ind_3)
        Me.lbl_Ind.Add(_lbl_Ind_4)
        Me.opt_Independent.Add(_opt_Independent_0)
        Me.opt_Independent.Add(_opt_Independent_1)
        Me.opt_Independent.Add(_opt_Independent_2)
        Me.opt_Independent.Add(_opt_Independent_3)
        Me.opt_Independent.Add(_opt_Independent_4)
        AddHandler CType(opt_Independent.Item(0), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf opt_Independent_CheckedChanged
        AddHandler CType(opt_Independent.Item(1), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf opt_Independent_CheckedChanged
        AddHandler CType(opt_Independent.Item(2), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf opt_Independent_CheckedChanged
        AddHandler CType(opt_Independent.Item(3), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf opt_Independent_CheckedChanged
        AddHandler CType(opt_Independent.Item(4), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf opt_Independent_CheckedChanged
    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_ScatVarSPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_ScatVarS
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_ScatVarS()
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
    '------------------------------------------ScatVarS.Frm-------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last edit by BBH March 5, 2000

    Dim popupchoice As String
    Dim dataString As String
    'Added by Pranav
    '---------------
    Dim db9 As String 'DAO.Database
    Dim table9 As DataTable 'DAO.TableDef
    Dim tbl9 As DataTable 'DAO.Recordset
    Dim valtbl As DataTable
    Dim rsl As DataTable 'JAM 2013-08-31

    Dim db As String
    'End for Pranav
    'mtiwebform June 2004 Cross-Sectional - declarations changed for session 
    'form (page) variables replaced with session("scat_oldname") etc.
    Dim Scat_VarGroupNames(100) As String

    'JAM 2014-02-22 for using all years in statistics form
    'Dim AllVarI()
    'Dim AllVarD()
    Dim AllVarName()
    Dim AllYear()
    ''Dim ValidData()
    ''Dim ArrayAllVarYears()
    ''Dim ArrayAllVarYearsComp()
    Dim Totx, Toty As Integer
    Dim AllNumYears As Integer
    Public AllVars As Boolean
    Public AllVarYears As Boolean

    'Dim VarGroupNames(100) As String
    'Dim SubOnPrev, MulOnPrev, DivOnPrev, AddOnPrev
    'Dim NVal As Object

    'Dim VarName, TableName As String

    'Dim GroupName As String
    'Dim SubOn, MulOn, DivOn, AddOn, NumVal As Object
    'Dim SE, Cohort As String

    'Dim WaveNumb, CohortSel, YearSel, CohortName, WaveDone As String
    'Dim WaveNumb, YearSel, CohortName, WaveDone As String
    'Dim IndIndex As Short
    'Dim FilledVar(4) As Object
    'Dim SCVarI(200, 4) As Object
    'Dim SCVarD As Object
    'Dim NewVal(200) As Object
    'JRS 9/25/01 COMPUTATIONS
    'Dim sCurrentDisplay As String
    'Dim iCurrentIdx As Short


    '=============================ScatVarS.Frm==============================
    'UPGRADE_WARNING: Event cmb_VarName.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cmb_VarName_SelectedIndexChanged1() '(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_VarName.SelectedIndexChanged
        'Dim VarTable As String
        Dim FullName As String
        Dim Msg As String
        Dim Definition As String
        Dim PosMul As Object
        Dim PosDiv As Object
        Dim SELNLen As Object
        Dim ICount As Short
        Dim ValSelection As String
        'Dim Series As String

        Dim Rcnt As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        '   Variable Name Typed by User - Strip off Operator and Check Name Validity
        '   ------------------------------------------------------------------------

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("Scat_DivOn") = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("Scat_MulOn") = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("Scat_AddOn") = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("Scat_SubOn") = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NumVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("Scat_NumVal") = False
        Session("Scat_Series") = "No"
        Session("Scat_Cohort") = "No"
        Dim sUnits As String
        If txtVarSearch.Text = "" Then
            ValSelection = CStr(False)
            If IsNumeric(txtVarSearch.Text) Then 'Numeric scaling value
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NumVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_NumVal") = True
                'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_NVal") = Val(txtVarSearch.Text)
            End If
            Session("Scat_SE") = UCase(txtVarSearch.Text) 'SE$ picks up the Variable name typed
            If Session("Scat_SE") = "NONE" Then
                'mtiwebform June 2004 Cross-Sectional
                If opt_Dependent.Checked Then
                    lbl_Dep.Text = "None"
                    'jrs 2006/02/22
                    Session("frm_ScatVarSlbl_Dep") = lbl_Dep.Text
                    Exit Sub
                ElseIf _opt_Independent_0.Checked Then
                    _lbl_Ind_0.Text = "None"
                    Session("Scat_FilledVar")(0) = False
                    Exit Sub
                ElseIf _opt_Independent_1.Checked Then
                    _lbl_Ind_1.Text = "None"
                    Session("Scat_FilledVar")(1) = False
                    Exit Sub
                ElseIf _opt_Independent_2.Checked Then
                    _lbl_Ind_2.Text = "None"
                    Exit Sub
                ElseIf _opt_Independent_3.Checked Then
                    _lbl_Ind_3.Text = "None"
                    Exit Sub
                ElseIf _opt_Independent_4.Checked Then
                    _lbl_Ind_4.Text = "None"
                    Exit Sub
                ElseIf opt_Control.Checked Then
                    lbl_Cont.Text = "None"
                    Exit Sub

                End If

            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SELNLen = Len(Session("Scat_SE"))
            'UPGRADE_WARNING: Couldn't resolve default property of object PosDiv. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            PosDiv = InStr(1, Session("Scat_SE"), "/")
            'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object PosDiv. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If PosDiv = SELNLen Then 'Check for Division
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_DivOn") = True
                'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_SE") = VB.Left(Session("Scat_SE"), SELNLen - 1)
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            PosMul = InStr(1, Session("Scat_SE"), "*")
            'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If PosMul = SELNLen Then ' Check for Multiplication
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_MulOn") = True
                'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_SE") = VB.Left(Session("Scat_SE"), SELNLen - 1)
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            PosMul = InStr(1, Session("Scat_SE"), "+")
            'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If PosMul = SELNLen Then ' Check for Addition
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_AddOn") = True
                'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_SE") = VB.Left(Session("Scat_SE"), SELNLen - 1)
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            PosMul = InStr(1, Session("Scat_SE"), "-")
            'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If PosMul = SELNLen Then ' Check for Subtraction
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_SubOn") = True
                'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_SE") = VB.Left(Session("Scat_SE"), SELNLen - 1)
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NumVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_NumVal") <> True Then 'Make sure that variable name is valid one
                valtbl = Session("DataDictVars")
                For Rcnt = 0 To valtbl.Rows.Count - 1
                    'Do 'Search all variables for name
                    'TableName = valtbl.Fields("Table").Value
                    Session("Scat_TableName") = valtbl.Rows(Rcnt).Item("Table")
                    Session("Scat_GroupNames") = valtbl.Rows(Rcnt).Item("Group")
                    Session("Scat_VarName") = valtbl.Rows(Rcnt).Item("Variable")
                    Session("Scat_Series") = valtbl.Rows(Rcnt).Item("Series")
                    Session("Scat_Cohort") = valtbl.Rows(Rcnt).Item("Cohort")
                    'JAM 2011-09-25 Replacing two and three blank spaces
                    Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")
                    'jrs 2023/05/05
                    Definition = Replace(Replace(Replace(Definition, Chr(10), ""), Chr(13), ""), """", "")


                    If (Session("Scat_SE") = UCase(Session("Scat_VarName") & " - " & Session("Scat_GroupNames"))) Or (Session("Scat_SE") = UCase(Session("Scat_VarName"))) Then
                        ValSelection = CStr(True)
                        'Dim x
                        Exit For
                    End If
                    'valtbl.MoveNext()
                    'If valtbl.EOF = True Then Exit Do
                    'Loop
                Next Rcnt
                'valtbl.Close()
                'db.Close()
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NumVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If CBool(ValSelection) = False And Session("Scat_NumVal") = False Then 'If VarName$ not found
                Msg = txtVarSearch.Text & " is not a valid selection."
                Msg = Msg & "  Please try again."

                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NumVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_NumVal") = True Then
                'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_VarName") = Str(Session("Scat_NVal"))
            End If
        Else

            '   Variable Name Selected by Click of User
            '   ----------------------------------------
            'jrs 2023/09/28
            'Remove &amp;
            txtVarSearch.Text = txtVarSearch.Text.Replace("&amp;", "&")
            'jrs 2025/07/30
            Dim pos As Integer
            pos = InStr(txtVarSearch.Text, ChrW(65533))
            If pos > 0 Then
                txtVarSearch.Text = txtVarSearch.Text.Substring(0, pos - 1)
            End If
            '? AscW(txtVarSearch.Text.Substring(167, 1))
            '65533

            'mtiwebform June 2004 Cross-Sectional
            'none selected 
            Session("Scat_SE") = UCase(txtVarSearch.Text) 'SE$ picks up the Variable name typed
            If Session("Scat_SE") = "NONE" Then
                'mtiwebform June 2004 Cross-Sectional
                If opt_Dependent.Checked Then
                    lbl_Dep.Text = "None"
                    'jrs 2006/02/22
                    Session("frm_ScatVarSlbl_Dep") = lbl_Dep.Text
                    Exit Sub
                ElseIf _opt_Independent_0.Checked Then
                    _lbl_Ind_0.Text = "None"
                    Session("Scat_FilledVar")(0) = False
                    Exit Sub
                ElseIf _opt_Independent_1.Checked Then
                    _lbl_Ind_1.Text = "None"
                    Session("Scat_FilledVar")(1) = False
                    Exit Sub
                ElseIf _opt_Independent_2.Checked Then
                    _lbl_Ind_2.Text = "None"
                    Exit Sub
                ElseIf _opt_Independent_3.Checked Then
                    _lbl_Ind_3.Text = "None"
                    Exit Sub
                ElseIf _opt_Independent_4.Checked Then
                    _lbl_Ind_4.Text = "None"
                    Exit Sub
                ElseIf opt_Control.Checked Then
                    lbl_Cont.Text = "None"
                    Exit Sub

                End If

            End If
            valtbl = Session("DataDictVars")
            For Rcnt = 0 To valtbl.Rows.Count - 1
                Session("Scat_TableName") = valtbl.Rows(Rcnt).Item("Table")
                Session("Scat_GroupNames") = valtbl.Rows(Rcnt).Item("Group")
                Session("Scat_VarName") = valtbl.Rows(Rcnt).Item("Variable")
                Session("Scat_Series") = valtbl.Rows(Rcnt).Item("Series")
                Session("Scat_Cohort") = valtbl.Rows(Rcnt).Item("Cohort")

                'Definition = valtbl.Rows(Rcnt).Item("Definition")
                'JAM 2011-09-25 Replacing two and three blank spaces
                Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")
                'jrs 2023/05/05
                Definition = Replace(Replace(Replace(Definition, Chr(10), ""), Chr(13), ""), """", "")


                'JAM 2010-03-07 Loading Aggregation rule for gList
                If IsDBNull(valtbl.Rows(Rcnt).Item("Aggregation")) Then
                    Session("sAggregation") = ""
                Else
                    Session("sAggregation") = valtbl.Rows(Rcnt).Item("Aggregation")

                End If
                'JRS 08/29/03
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
                    sUnits = ""
                Else
                    sUnits = valtbl.Rows(Rcnt).Item("Units")
                End If
                FullName = Session("Scat_VarName") & " - " & Definition & " - " & sUnits
                'jrs 2025/07/30 added Or InStr(1, FullName, txtVarSearch.Text) > 0 
                If InStr(1, txtVarSearch.Text, FullName) > 0 Or InStr(1, FullName, txtVarSearch.Text) > 0 Then
                    'use InStr because year may be added to name in combo box,
                    'so the two strings may not be identical
                    Session("Scat_SE") = UCase(Session("Scat_VarName"))
                    'identify the click selection
                    Exit For
                End If
                'valtbl.MoveNext()
                'If valtbl.EOF = True Then Exit Do
                'Loop
            Next Rcnt
            'valtbl.Close()
            'db.Close()

            'added 14 March 2002
            'only in case of click

            If UCase(Session("Scat_Cohort")) <> "YES" Then 'Not cohort data
                popupchoice = ""
                'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                '                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                'UPGRADE_ISSUE: Form method frm_ScatVarS.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                'JRSVBNET Me.PopupMenu(mnuPOP)
                'Session("ClickedOnVarName") = True

                'mti WebForm Changes From June 26, 2004
                'wmContext.Visible = True
                'mti WebForm Changes From June 26, 2004
                'cmb_VarName.Attributes.Add("onclick", "cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);")


                Exit Sub
                'MsgBox "PopupChoice = " & popupchoice$
                'Select Case popupchoice
                '    Case "Data Availability"
                '        'mtiwebform June 2004 Cross-Sectional - commented data availability
                '        'CONVERSION_ISSUE: Property 'IFsGUI.frm_Availability.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                '        'frm_Availability.DefInstance.RawVarName = SE
                '        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.ShowDialog().html'.
                '        'frm_Availability.DefInstance.ShowDialog() 
                '        Exit Sub
                '    Case "Select"
                '        'GO AHEAD
                'End Select
            End If
            'added 14 March 2002

        End If

        'jrs 2024/04/30 *** start ***
        If opt_Dependent.Checked Then

            Session("TblDepYear")(0) = Session("Scat_VarName")
            Session("TblDepYear")(1) = Session("Scat_TableName")
            'JAM 2008-05-31 Adding Cohort Type
            If UCase$(Session("Scat_Cohort")) = "YES" Then
                Session("IsCohDep") = True
            Else
                Session("IsCohDep") = False
            End If
            'jrs 2013/03/06
            'bCurrencyY = bCurrency
            'jrs 2014/09/17
            'sDepGroup = GroupName$
            'sDepUnits = sUnits
            'sDepAggregation = sAggregation

        Else
            If (Session("Scat_IndIndex") <= 1) Then 'And (opt_Independent(IndIndex).value = True) Then
                If Session("Scat_IndIndex") = 0 Then
                    'Code reviewed JAM 2007/02/14
                    Session("TblIndYear")(0) = Session("Scat_VarName")
                    Session("TblIndYear")(1) = Session("Scat_TableName")
                    'JAM 2008-05-31 Adding Cohort Type
                    If UCase$(Session("Scat_Cohort")) = "YES" Then
                        Session("IsCoh1Ind") = True
                    Else
                        Session("IsCoh1Ind") = False
                    End If
                    'jrs 2013/03/06
                    'bCurrencyX = bCurrency
                Else
                    Session("TblSecIndYear")(0) = Session("Scat_VarName")
                    Session("TblSecIndYear")(1) = Session("Scat_TableName")
                    'JAM 2008-05-31 Adding Cohort Type
                    If UCase$(Session("Scat_Cohort")) = "YES" Then
                        Session("IsCoh2Ind") = True
                    Else
                        Session("IsCoh2Ind") = False
                    End If

                End If
            End If
        End If
        'jrs 2024/04/30 *** end ***

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        On Error GoTo FileProblem

        '   Pick up Information if the Variable is a Time Series or Cohort
        '   --------------------------------------------------------------

        If Session("Scat_Series") = "Yes" Then 'Series variable and field is year
            Session("Scat_Cohort") = ""
            Session("Scat_YearSel") = CStr(True)
            Session("Scat_CohortSel") = CStr(False)
            Call YearLoad()
        ElseIf Session("Scat_Cohort") = "Yes" Then 'Cohort variable and field is cohort
            Session("Scat_YearSel") = CStr(False)
            Session("Scat_CohortSel") = CStr(True)
            Session("Scat_WaveDone") = CStr(False)
            Session("Scat_SE") = ""
            Call WaveLoad()
        Else
            Session("Scat_YearSel") = CStr(False)
            Session("Scat_CohortSel") = CStr(False)
            Session("Scat_SE") = ""
            Session("Scat_Cohort") = ""
            Call FillData()
        End If

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'JRS 9/16/01 COMPUTATIONS
        If Session("bComputation") And Session("Scat_Series") <> "Yes" And Session("Scat_Cohort") <> "Yes" Then
            'jrs 2006/01/02
            Session("frm_ScatVarSlbl_Dep") = lbl_Dep.Text
            Session("frm_ScatVarSlbl_Ind0") = _lbl_Ind_0.Text
            Session("frm_ScatVarSlbl_Ind1") = _lbl_Ind_1.Text
            Session("frm_ScatVarSlbl_Ind2") = _lbl_Ind_2.Text
            Session("frm_ScatVarSlbl_Ind3") = _lbl_Ind_3.Text
            Session("frm_ScatVarSlbl_Ind4") = _lbl_Ind_4.Text
            Session("frm_ScatVarSlbl_Cont") = lbl_Cont.Text

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")
            'frm_computations.DefInstance.Show() 
            'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Activate' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Activate().html'.
            'frm_computations.DefInstance.Activate() 
            Exit Sub
        Else
            Exit Sub
        End If



FileProblem:
        Msg = "Sorry. An error has arisen during an attempt to read " & Session("Scat_VarName")
        Msg = Msg & " from " & Session("Scat_ VarTable") & " - the variable or data for it "
        Msg = Msg & "doesn't exist in the file.  Please try another variable."

        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub
    '=============================ScatVarS.Frm==============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ComboBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.KeyPress.html'.
    'Private Sub cmb_VarName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) 'Handles cmb_VarName.KeyPress
    '    'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '    'mti WebForm Changes From June 26, 2004
    '    If KeyAscii = 13 Then Call cmb_VarName_SelectedIndexChanged1() '(cmb_VarName, New System.EventArgs) 'Enter
    '    If KeyAscii >= 97 And KeyAscii <= 146 Then
    '        'UPGRADE_ISSUE: Assignment not supported: KeyAscii to a non-zero value Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1058"'
    '        KeyAscii = KeyAscii - 32
    '    End If
    '    If KeyAscii = 27 Then 'Escape
    '        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
    '        Support.FormManager.Close(Me)
    '    End If
    '    If KeyAscii = 0 Then
    '        'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '        eventArgs.Handled = True
    '    End If
    'End Sub
    '=============================ScatVarS.Frm==============================
    Private Sub YearLoad()
        Dim Msg As String
        'mtiwebform June 2004 Cross-Sectional
        'Dim f As Object
        Dim f As DataColumn 'Object

        Dim fieldsfound As Short 'Object
        Dim Init As Integer
        Dim isvalid As Boolean 'JAM 2016-08-06
        Dim scount As Integer 'JAM 2016-08-06
        Dim g As DataRow 'JAM 2016-08-06
        Dim fieldsfound2 As Integer 'JAM 2016-08-06
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Make Combination Box for Year Identification Visible and Fill
        '   -------------------------------------------------------------
        cmb_YearSelection.Visible = True
        lstDIM1.Visible = True
        cmb_YearSelection.Focus()
        txtVarSearch.Enabled = False
        lstVarName.Enabled = False
        lstVarName.Visible = False
        lbl_Year.Visible = True
        lbl_VarName.Visible = False
        lbl_vcat.Visible = False
        cmb_VCAT.Visible = False
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_YearSelection.Items.Clear()
        lstDIM1.Items.Clear()
        cmb_YearSelection.Items.Add(New ListItem(""))
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cmb_YearSelection.Items(cmb_YearSelection.SelectedIndex).Text = ""

        '       Load element names into Combo Box
        '       ---------------------------------

        'JAM 2016-08-06 Initializing Lag Arrays
        For Init = 0 To UBound(Session("LDepYear"))
            If opt_Dependent.Checked Then
                Session("LDepYear")(Init) = Nothing
            Else
                'If (IndIndex <= 1) And (opt_Independent(IndIndex).value = True) Then
                If Session("Scat_IndIndex") <= 1 Then
                    If Session("Scat_IndIndex") = 0 Then
                        Session("LIndYear")(Init) = Nothing
                    Else
                        Session("LSecIndYear")(Init) = Nothing
                    End If
                End If
            End If
        Next Init

        'JAM 2016-08-06
        If Session("Scat_IndIndex") = 0 Then
            Session("IndTotYears") = 0
        ElseIf Session("Scat_IndIndex") = 1 Then
            Session("SecIndTotYears") = 0
        End If

        If opt_Dependent.Checked Then
            Session("TotYearsLoaded") = 0
        End If

        Dim abc, def As String



        'JAM 2016-10-29 Saving variables in order to loading years in scatter web form
        If Session("Scat_IndIndex") = 0 And Not opt_Dependent.Checked Then
            Session("TblIndYear")(0) = Session("Scat_VarName")
            Session("TblIndYear")(1) = "Series" & Session("Scat_VarName")
        ElseIf Session("Scat_IndIndex") = 1 And Not opt_Dependent.Checked Then
            Session("TblSecIndYear")(0) = Session("Scat_VarName")
            Session("TblSecIndYear")(1) = "Series" & Session("Scat_VarName")
        End If

        If opt_Dependent.Checked Then
            Session("TblDepYear")(0) = Session("Scat_VarName")
            Session("TblDepYear")(1) = "Series" & Session("Scat_VarName")

        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        On Error GoTo FileNotFound
        '        Set db = OpenDatabase(FDRV$ + "Data\IFSHistSeries.MDB")
        'If GroupName = "Automobile" Then 
        '          db = DAODBEngine_definst.OpenDatabase(session("IFsCoVaTrASeries_MDB"))

        'Else 
        '	'Set db = OpenDatabase(session("IFsHistSeries_MDB"))
        '	db = DAODBEngine_definst.OpenDatabase(session("IFsHistSeries_MDB"), False, False, DataPW)
        '      End If

        If Session("Scat_GroupNames") = "Automobile" Then
            'db = DAODBEngine_definst.OpenDatabase(session("IFsCoVaTrASeries_MDB"))
            db = Session("IFsCoVaTrASeries_MDB")
            SetDataTablefromAccess(db, "SELECT * FROM [Series" & Session("Scat_VarName") & "]", "", valtbl)
        Else
            'db = DAODBEngine_definst.OpenDatabase(session("IFsHistSeries_MDB"), False, False, DataPW)
            db = Session("IFsHistSeries_MDB")
            SetDataTablefromAccess(db, "SELECT * FROM [Series" & Session("Scat_VarName") & "]", "", valtbl, DataPW)
        End If




        'valtbl = db.OpenRecordset("Series" & VarName)
        'UPGRADE_WARNING: Couldn't resolve default property of object fieldsfound. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        fieldsfound = 0
        fieldsfound2 = 0

        'For Each f In valtbl.Fields
        For Each f In valtbl.Columns
            'UPGRADE_WARNING: Couldn't resolve default property of object f.Name. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If f.ColumnName <> "Country" And f.ColumnName <> "FIPS_CODE" And f.ColumnName <> "FIPS_Code" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object fieldsfound. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                fieldsfound = fieldsfound + 1
                'UPGRADE_WARNING: Couldn't resolve default property of object f.ColumnName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                cmb_YearSelection.Items.Add(New ListItem(f.ColumnName))
                lstDIM1.Items.Add(New ListItem(f.ColumnName))
                'UPGRADE_WARNING: Couldn't resolve default property of object f.ColumnName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_SE") = f.ColumnName


                'JAM 2016-06-08 
                isvalid = False
                For Each g In valtbl.Rows

                    If Not IsNothing(Session("Scat_SE")) Then
                        isvalid = True
                        Exit For
                    End If

                Next g

                If isvalid Then

                    If opt_Dependent.Checked Then
                        'jrs 2024/04/23 added if statement, content was already there
                        If fieldsfound2 < UBound(Session("LDepYear")) Then
                            Session("LDepYear")(fieldsfound2) = Session("Scat_SE")
                        End If
                        Session("TotYearsLoaded") = Session("TotYearsLoaded") + 1

                    Else
                        'If (IndIndex <= 1) And (opt_Independent(IndIndex).value = True) Then
                        If (Session("Scat_IndIndex") <= 1) Then
                            If Session("Scat_IndIndex") = 0 Then
                                'jrs 2024/04/23 added if statement, content was already there
                                If fieldsfound2 < UBound(Session("LIndYear")) Then
                                    Session("LIndYear")(fieldsfound2) = Session("Scat_SE")
                                End If

                                Session("IndTotYears") = Session("IndTotYears") + 1

                            Else
                                'jrs 2024/04/23 added if statement, content was already there
                                If fieldsfound2 < UBound(Session("LSecIndYear")) Then
                                    Session("LSecIndYear")(fieldsfound2) = Session("Scat_SE")
                                End If

                                Session("SecIndTotYears") = Session("SecIndTotYears") + 1
                            End If
                        End If
                    End If
                    fieldsfound2 = fieldsfound2 + 1
                End If

            End If
        Next f
        'valtbl.Close()
        'db.Close()
        '        If FieldsFound > 1 Then 'if only one field, just use it
        '            Call CohortLoad
        '            Exit Sub
        '        End If

        '        Open FDRV$ + "Data\IFsHistSeries.Dat" For Input As #2
        '        Do While Not EOF(2)
        '            Input #2, seriesname$  'Search for series to match variable name
        '            If UCase$(seriesname$) = UCase$(VarName$) Then 'Series found; add years to box
        '                Input #2, NumPoints
        '                For ILOOP = 1 To NumPoints
        '                    Input #2, SeriesYearAlpha$
        '                    cmb_YearSelection.AddItem SeriesYearAlpha$
        '                Next ILOOP
        '                Exit Do
        '                Else  'Series not found; read rest of line
        '                    Line Input #2, Dummy$
        '               End If
        '        Loop
        '        Close #2

        Exit Sub

FileNotFound:
        'mti August 2004 4 
        'If Error, - reset to variable Combo
        '   Make Combination Box Invisible
        '   ------------------------------
        cmb_YearSelection.Visible = False
        lstDIM1.Visible = False
        txtVarSearch.Enabled = True
        lstVarName.Enabled = True
        lstVarName.Visible = True
        lbl_Year.Visible = False
        lbl_VarName.Visible = True
        lbl_vcat.Visible = True
        cmb_VCAT.Visible = True
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_YearSelection.Items.Clear()
        lstDIM1.Items.Clear()
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cmb_YearSelection.Items(cmb_YearSelection.SelectedIndex).Text = ""

        'mti August 2004 4

        Msg = "Data not found in year load.  Please try a different variable."

        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")



    End Sub
    '=============================ScatVarS.Frm==============================
    'UPGRADE_WARNING: Event cmb_vcat.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    'KIA 09/22/2009 REPLACE THE FOLLOWING METHOD. Lot of changes have been made due to the new category type and also two new ifs blocks added.

    'JAM 2013-08-10 Name Changed  in order to test javascript implementation
    'Private Sub cmb_vcat_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_VCAT.SelectedIndexChanged

    'Private Sub cmb_vcat_SelectedIndexChangedBK(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_VCAT.SelectedIndexChanged
    '    Dim AddName As String
    '    Dim Definition As String
    '    Dim Series As String
    '    Dim GroupSelect As String
    '    Dim Rcnt As Short
    '    'jrs 2011/07/06
    '    Dim sPreprocessor As String, sYears As String, bInclude As Boolean

    '    If cmb_VCAT.Items.Count > 0 Then
    '        Session("ScatVarVCat") = cmb_VCAT.SelectedIndex
    '    End If

    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '    '
    '    '   Reload Variable Name Combo Box Based on Group Selected by User
    '    '   --------------------------------------------------------------
    '    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

    '    lstVarName.Items.Clear()

    '    AddName = ""
    '    dataString = ""

    '    If Session("ScatVarVCat") > 0 Then

    '        GroupSelect = cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text
    '    Else
    '        GroupSelect = "All"
    '    End If

    '    valtbl = Session("DataDictVars")
    '    Dim sUnits As String
    '    Dim GroupNames As String
    '    Dim VarName As String
    '    Dim Groups As Array
    '    Dim pos1 As Integer
    '    For Rcnt = 0 To valtbl.Rows.Count - 1
    '        'Do
    '        'JRSVBNET Session("Scat_TableName") = valtbl.Rows(Rcnt).Item("Table")

    '        GroupNames = Trim(valtbl.Rows(Rcnt).Item("Group"))
    '        If UCase(GroupSelect) = UCase("All") Then
    '            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or Session("Scat_GroupNames") <> "Automobile" Then
    '                VarName = valtbl.Rows(Rcnt).Item("Variable")
    '                Series = valtbl.Rows(Rcnt).Item("Series")
    '                'Definition = valtbl.Rows(Rcnt).Item("Definition")
    '                'JAM 2011-09-25 Replacing two and three blank spaces
    '                Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

    '                'jrs 2011/07/08
    '                sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
    '                sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))
    '                'JRS 10/14/03
    '                If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
    '                    sUnits = ""
    '                Else
    '                    sUnits = valtbl.Rows(Rcnt).Item("Units")
    '                End If
    '                'jrs 2011/07/06
    '                bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
    '                If bInclude Then
    '                    If GroupNames = GroupSelect Or Session("ScatVarVCat") < 1 Then
    '                        AddName = VarName & " - " & Definition & " - " & sUnits
    '                        ' AddName = AddName.Trim()
    '                        dataString = dataString + AddName + "|"
    '                        lstVarName.Items.Add(New ListItem(AddName))
    '                    End If
    '                End If
    '            End If
    '        Else
    '            Groups = Split(GroupNames, ",")
    '            pos1 = 0
    '            While pos1 < Groups.Length
    '                GroupNames = Trim(Groups(pos1))
    '                If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or Session("Scat_GroupNames") <> "Automobile" Then
    '                    VarName = valtbl.Rows(Rcnt).Item("Variable")
    '                    Series = valtbl.Rows(Rcnt).Item("Series")
    '                    'Definition = valtbl.Rows(Rcnt).Item("Definition")
    '                    'JAM 2011-09-25 Replacing two and three blank spaces
    '                    Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

    '                    'jrs 2011/07/08
    '                    sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
    '                    sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))
    '                    'JRS 10/14/03
    '                    If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
    '                        sUnits = ""
    '                    Else
    '                        sUnits = valtbl.Rows(Rcnt).Item("Units")
    '                    End If
    '                    'jrs 2011/07/06
    '                    bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
    '                    If bInclude Then
    '                        If GroupNames = GroupSelect Or Session("ScatVarVCat") < 1 Then
    '                            AddName = VarName & " - " & Definition & " - " & sUnits
    '                            'AddName = AddName.Trim()
    '                            dataString = dataString & AddName & "|"
    '                            lstVarName.Items.Add(New ListItem(AddName))
    '                        End If
    '                    End If
    '                End If
    '                pos1 += 1
    '            End While
    '            'valtbl.MoveNext()
    '            'If valtbl.EOF = True Then Exit Do
    '        End If         'Loop
    '    Next Rcnt
    '    Session("DataString") = dataString
    '    'valtbl.Close()
    '    'db.Close()

    'End Sub
    Public Sub LoadListData()

        lstVarName.Items.Clear()
        Dim indx = 0
        Dim sarr() As String
        sarr = Session("DataString").ToString().Split("|")

        While indx < sarr.Length
            lstVarName.Items.Add(New ListItem(Trim(sarr(indx))))
            indx = indx + 1
        End While

    End Sub
    '=============================ScatVarS.Frm==============================
    'UPGRADE_WARNING: Event cmb_YearSelection.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cmb_YearSelection_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_YearSelection.SelectedIndexChanged
        'jrs 10/13/04
        lstDIM1.SelectedIndex = cmb_YearSelection.SelectedIndex - 1
        'mtiwebform June 2004 Cross-Sectional

        'Dim f As Object
        Dim f As DataColumn 'Object
        Dim fieldsfound As Short 'Object
        Dim Msg As String
        Dim ValSelection As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Year, Cohort, or Wave Typed by User
        '   -----------------------------------
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If cmb_YearSelection.SelectedIndex = -1 Then
            ValSelection = CStr(False)
            Session("Scat_SE") = UCase(cmb_YearSelection.SelectedValue) 'SE$ picks up the Variable name typed
        Else

            '   Year, Cohort, or Wave Selected by Click of User
            '   -----------------------------------------------
            Session("Scat_SE") = UCase(cmb_YearSelection.SelectedValue)


            'JAM 2016-08-04 Keeping Original Selected Year
            If opt_Dependent.Checked Then
                'CurrDepYear = SE$
                Session("OrigDepYear") = Session("Scat_SE")
                'jrs 2011/01/26
                'ScaleLabelD = ""
            Else
                'If (IndIndex <= 1) And (opt_Independent(IndIndex).value = True) Then
                If Session("Scat_IndIndex") <= 1 Then
                    If Session("Scat_IndIndex") = 0 Then
                        'CurrIndYear = SE$
                        Session("OrigIndYear") = Session("Scat_SE")
                        'jrs 2011/01/26
                        'ScaleLabelI = ""
                    Else
                        'CurrSecIndYear = SE$
                        Session("OrigSecIndYear") = Session("Scat_SE")
                    End If
                End If
            End If




            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        End If
        If Session("Scat_SE") = "" Then
            Msg = "You did not select a dimension option.  Please try again."

            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If


        '   Make Combination Box Invisible
        '   ------------------------------
        cmb_YearSelection.Visible = False
        lstDIM1.Visible = False
        txtVarSearch.Enabled = True
        lstVarName.Enabled = True
        lstVarName.Visible = True
        lbl_Year.Visible = False
        lbl_VarName.Visible = True
        lbl_vcat.Visible = True
        cmb_VCAT.Visible = True
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_YearSelection.Items.Clear()
        lstDIM1.Items.Clear()
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cmb_YearSelection.Items(cmb_YearSelection.SelectedIndex).Text = ""

        '   If Wave Selected then Reload Combo Box with Cohort/Total Options
        '   ----------------------------------------------------------------

        If CBool(Session("Scat_CohortSel")) = True Then
            If CBool(Session("Scat_WaveDone")) <> True Then 'Select Cohort/Total
                Session("Scat_WaveNumb") = Session("Scat_SE") 'Wave has been selected and saved
                Session("Scat_WaveDone") = CStr(True)
                'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                On Error GoTo FileNotFound
                'db = DAODBEngine_definst.OpenDatabase(session("IFsWVSCohort_MDB"))
                'valtbl = db.OpenRecordset(VarName & WaveNumb)
                db = Session("IFsWVSCohort_MDB")
                SetDataTablefromAccess(db, "SELECT * FROM " & Session("Scat_VarName") & Session("Scat_WaveNumb"), "", valtbl)
                'UPGRADE_WARNING: Couldn't resolve default property of object fieldsfound. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                fieldsfound = 0
                'For Each f In valtbl.Fields
                For Each f In valtbl.Columns
                    'UPGRADE_WARNING: Couldn't resolve default property of object f.Name. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If f.ColumnName <> "Country" And f.ColumnName <> "FIPS_CODE" Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object fieldsfound. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        fieldsfound = fieldsfound + 1
                        'UPGRADE_WARNING: Couldn't resolve default property of object f.ColumnName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("Scat_SE") = f.ColumnName
                    End If
                Next f
                'valtbl.Close()
                'db.Close()
                'UPGRADE_WARNING: Couldn't resolve default property of object fieldsfound. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If fieldsfound > 1 Then 'if only one field, just use it
                    Call CohortLoad()
                    Exit Sub
                End If
            End If
        End If


        '   Go to Procedure to Put Data Into Independent, Dependent, Control Variables
        '   --------------------------------------------------------------------------

        If CBool(Session("Scat_CohortSel")) = True Then
            Session("Scat_TableName") = Session("Scat_VarName")
            Session("Scat_VarName") = Session("Scat_VarName") & Session("Scat_WaveNumb") & "(" & Session("Scat_SE") & ")"
        Else
            Session("Scat_VarName") = Session("Scat_VarName") & "(" & Session("Scat_SE") & ")"
        End If

        'jrs 2008/09/02
        If Not Session("bComputation") Then
            If opt_Dependent.Checked Then
                Session("bCompSVtmp")(1) = False
            ElseIf Session("Scat_IndIndex") < 2 Then
                Session("bCompSVtmp")(Session("Scat_IndIndex") + 2) = False
            End If
        End If

        Call FillData()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'JRS 9/16/01 COMPUTATIONS
        If Session("bComputation") Then
            'jrs 2006/01/02
            Session("frm_ScatVarSlbl_Dep") = lbl_Dep.Text
            Session("frm_ScatVarSlbl_Ind0") = _lbl_Ind_0.Text
            Session("frm_ScatVarSlbl_Ind1") = _lbl_Ind_1.Text
            Session("frm_ScatVarSlbl_Ind2") = _lbl_Ind_2.Text
            Session("frm_ScatVarSlbl_Ind3") = _lbl_Ind_3.Text
            Session("frm_ScatVarSlbl_Ind4") = _lbl_Ind_4.Text
            Session("frm_ScatVarSlbl_Cont") = lbl_Cont.Text

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")
            'frm_computations.DefInstance.Show() 
            'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Activate' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Activate().html'.
            'frm_computations.DefInstance.Activate() 
            Exit Sub
        Else
            Exit Sub
        End If

FileNotFound:
        'mti August 2004 4 
        'If Error, - reset to variable Combo
        '   Make Combination Box Invisible
        '   ------------------------------
        cmb_YearSelection.Visible = False
        lstDIM1.Visible = False
        txtVarSearch.Enabled = True
        lstVarName.Enabled = True
        lstVarName.Visible = True
        lbl_Year.Visible = False
        lbl_VarName.Visible = True
        lbl_vcat.Visible = True
        cmb_VCAT.Visible = True
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_YearSelection.Items.Clear()
        lstDIM1.Items.Clear()
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cmb_YearSelection.Items(cmb_YearSelection.SelectedIndex).Text = ""
        'mti August 2004 4

        Msg = "Data not found in year selection.  Please try a different variable."

        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


    End Sub

    '=============================ScatVarS.Frm==============================
    Private Sub cmd_Exit2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit2.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 10/18/04
        Dim ICount As Integer
        For ICount = 0 To 4
            Session("Scat_FilledVar")(ICount) = False
        Next
        Session.Remove("DataDictVars")
        Session.Remove("ScatVarVCat")
        Session.Remove("DataString")

        'jrs 2010/09/06
        Session.Remove("frm_ScatVarSlbl_Dep")
        Session.Remove("frm_ScatVarSlbl_Ind0")
        Session.Remove("frm_ScatVarSlbl_Ind1")
        Session.Remove("frm_ScatVarSlbl_Ind2")
        Session.Remove("frm_ScatVarSlbl_Ind3")
        Session.Remove("frm_ScatVarSlbl_Ind4")
        Session.Remove("frm_ScatVarSlbl_Cont")

        'JAM 2010-01-21 Removing Third Dimension Variables
        Session.Remove("CVArISec")
        Session.Remove("CVarIPop")
        Session.Remove("CVarIGdp")
        Session.Remove("IPopOrig")
        Session.Remove("IGdpOrig")
        'JAM 2010-01-30 
        Session.Remove("XAxisLabel")
        Session.Remove("YAxisLabel")
        Session.Remove("TitleLabel")
        Session.Remove("SubTitleLabel")
        Session.Remove("LabelChanged")
        Session.Remove("ScatterParent")
        Session.Remove("AxisLabelsParent")

        'JAM 2010-03-18
        Session.Remove("IsaGroup")
        Session.Remove("IsaGlist")
        Session.Remove("ScatVarGListParent")
        Session.Remove("TotCountries")
        Session.Remove("GChanged")
        Session.Remove("GdpAggRule")
        Session.Remove("PopAggRule")
        Session.Remove("SCVarIGlob")
        Session.Remove("SCVarDGlob")
        Session.Remove("GListMember")
        Session.Remove("GListIsGroup")
        Session.Remove("GlistCount")
        Session.Remove("GListName")
        Session.Remove("AuxArray")
        Session.Remove("OrigTemp")
        Session.Remove("OrigDep")
        Session.Remove("FullDep")
        Session.Remove("OrigIndFir")
        Session.Remove("FullInd")
        Session.Remove("OrigIndSec")
        Session.Remove("gCountGroupMembers")
        Session.Remove("CountryRegress")
        Session.Remove("ResultArray")
        'jrs 2010/09/07
        Session.Remove("OrigAgg")
        Session.Remove("ExcludeCtry")
        Session.Remove("bCompSVtmp")
        Session.Remove("bNewFormula")
        Session.Remove("CCount")
        ''''''''''''''''''''''''''''''''''''''''''
        'JAM 2010-12-15
        Session.Remove("WholeCountries")
        Session.Remove("SaturCVarD")
        Session.Remove("mnuExcludeUnselectedChecked")
        Session.Remove("ShowPoints")
        Session.Remove("CountryRegress")
        Session.Remove("ShowPoints")
        Session.Remove("CountryRegress")
        Session.Remove("PowerDegree")
        Session.Remove("mnuTurnOffCaption")
        Session.Remove("mnuLinear")
        Session.Remove("mnuLogarithmic")
        Session.Remove("mnuExponential")
        Session.Remove("mnuLogistic")
        Session.Remove("mnuPower")
        Session.Remove("ChangeSatPoint")
        Session.Remove("SaturationValue")
        Session.Remove("mnuUnderlyingChecked")
        Session.Remove("mnuEqTurnChecked")
        Session.Remove("mnuEqTurnEnabled")
        Session.Remove("RegressType")
        Session.Remove("ScatterPower")  'JAM 2011-0129
        Session.Remove("ScatterLogistic")
        ''''''''''''''''''''''''''''''''''
        If Not (Session("ThirdDimOk") Is Nothing) Then
            Session.Remove("ThirdDimOk")
        End If

        'JAM 2018-04-29 Removing Bubble variable
        If Not (Session("bBubble") Is Nothing) Then
            Session.Remove("bBubble")
        End If

        'JAM 2018-04-29 Resetting Second chosen Variable
        If Not (InStr(Session("VarNameI")(1), "NONE") <> 0) And Session("VarNameI")(1) <> "" Then
            Session("VarNameI")(1) = ""
        End If

        'JAM 2011-01-29
        Session.Remove("TotMulGroups")
        Session.Remove("ChangeSatPoint")
        Session.Remove("SaturationValue")

        'jrs 2011/07/11
        Session.Remove("ScatVarFilt")
        Session.Remove("ScatVarFiltName")

        'jrs 2012/08/14
        Session.Remove("CVarCode")
        Session.Remove("CVarCounCode")

        'JAM 2013-09-21
        If Not (Session("ScatVarVCatName") Is Nothing) Then
            Session.Remove("ScatVarVCatName")
        End If
        'JAM 2013-09-21
        'fSubSetchosen.Value = ""
        'fOthFiltchosen.Value = ""
        'fSubSetchosenName.Value = ""
        'fOthFiltchosenName.Value = ""

        'jrs 2014/03/26
        Session.Remove("ScatVArAllYears")


        'JAM 2016-10-29
        Session.Remove("TblIndYear")
        Session.Remove("TblSecIndYear")
        Session.Remove("TblSecIndYear")

        'jrs 2006/01/03 Support.FormManager.Close(Me)
        If Session("bComputation") Then
            Support.FormManager.Close(Me)
        Else
            Support.FormManager.Show(Me, "frm_MainMenu.aspx")
        End If

    End Sub


    '=============================ScatVarS.Frm==============================
    Private Sub cmd_Plot_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Plot.Click
        Dim ICount As Short
        Dim validcase As Object
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '
        '   Check for Completed Selection
        '   -----------------------------
        If UCase(lbl_Ind(0).Text) = "NONE" Or UCase(lbl_Dep.Text) = "NONE" Then
            Msg = "You must choose a dependent and one independent variable."

            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'cmb_VarName.SetFocus
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        Else
            'db = DAODBEngine_definst.OpenDatabase(session("IFs_MDB2Active"))
            'valtbl = db.OpenRecordset("Economic") 'Just arbitrary choice of table
            ''so that can pick up country names from field - country
            'valtbl.index = "Country" '"PrimaryKey"
            'valtbl.MoveFirst()

            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM Economic Order by Country", "", valtbl)

            'UPGRADE_WARNING: Couldn't resolve default property of object validcase. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            validcase = False
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For ICount = 1 To Session("CCount")
                'UPGRADE_WARNING: Couldn't resolve default property of object SCVarI(ICount, 0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("CVarI")(ICount) = Session("Scat_SCVarI")(ICount, 0)
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SCVarD")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("CVarD")(ICount) = Session("Scat_SCVarD")(ICount)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                'jrs 2025/01/28 added or isNothing
                If Not (IsDBNull(Session("CVarI")(ICount)) Or IsDBNull(Session("CVarD")(ICount)) Or
                    IsNothing(Session("CVarI")(ICount)) Or IsNothing(Session("CVarD")(ICount))) Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object validcase. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    validcase = True 'Both values are non-null, so case is valid
                End If
                'CVarName(ICount) = valtbl.Fields("Country").Value 'These to to Excel;
                Session("CVarName")(ICount) = valtbl.Rows(ICount - 1).Item("Country") 'These to to Excel;
                Session("CVarCounName")(ICount) = Session("CVarName")(ICount)
                'someday want ability to label points on scatterplot
                'jrs 2012/08/14
                Session("CVarCode")(ICount) = valtbl.Rows(ICount - 1).Item("FIPS_CODE") 'Code Added by Jaime
                Session("CVarCounCode")(ICount) = Session("CVarCode")(ICount)

                'valtbl.MoveNext()
            Next ICount
            'UPGRADE_WARNING: Couldn't resolve default property of object validcase. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If validcase = False Then
                Msg = "No country has data on both independent and dependent variables.  "
                Msg = Msg & "Therefore a scatterplot is impossible."

                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            'valtbl.Close()
        End If

        'JAM 2010-01-23 Loading Third Dimension Variables
        Call Load_Population()
        Call Load_Gdp()

        'JAM 2010-01-30
        Session("XAxisLabel") = Session("VarNameI")(0)
        Session("YAxisLabel") = Session("VarNameD")
        Session("TitleLabel") = ""
        Session("SubTitleLabel") = ""

        'jrs 2007/08/31
        'Aggregate when just countries loaded
        'need a second condition, to avoid doing it when glist already loaded.

        '2008-05-04 Recalculation is required only is a G-List
        If Session("IsaGlist") And Not Session("IsaGroup") Then
            If Session("bNewFormula") Then
                Dim II As Integer
                For II = 1 To 3
                    If Session("bCompSVtmp")(II) Then
                        Select Case II
                            Case 1
                                    'AssignVal(Session("OrigDep"), Session("GlistCount"), 0, True, Session("OrigAgg")(II - 1), "O")
                            Case 2
                                    'AssignVal(Session("OrigIndFir"), Session("GlistCount"), 0, False, Session("OrigAgg")(II - 1), "O")
                            Case 3
                                'AssignVal(Session("OrigIndSec"), Session("GlistCount"), 1, False, Session("OrigAgg")(II - 1), "O")
                        End Select
                    End If
                Next II
                Session("bNewFormula") = False
            End If
        End If




        'JAM 2013-09-15 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            Session("ScatVarVCat") = fSubSetchosen.Value
        Else
            If Session("ScatVarVCat") Is Nothing Then
                Session("ScatVarVCat") = 0
            Else
                fSubSetchosen.Value = Session("ScatVarVCat")
            End If
        End If
        If fOthFiltchosen.Value <> "" Then
            Session("ScatVarFilt") = fOthFiltchosen.Value
        Else
            If Session("ScatVarFilt") Is Nothing Then
                Session("ScatVarFilt") = 3  'All Value Selected
            Else
                fOthFiltchosen.Value = Session("ScatVarFilt")
            End If
        End If

        ReDim Session("ExcludeCtry")(Session("TotCountries"))

        'JAM 2016-10-30
        Session("FilterReq") = 1


        '
        '   Scale Data for Graph
        '   --------------------


        Call ScaledGraph()

        Session("AxisLabelsParent") = "Scatter.aspx"

        'JAM 2016-10-07
        Session("MoreScatterLoading") = False

        'JAM 2016-12-11
        Session("AddingYear") = False

        'JAM 2018-04-29 ThirdDimok
        ' If Session("CVarISec")(1) Then
        Session("ThirdDimOk") = False

        'End If



        If UCase(lbl_Cont.Text) <> "NONE" Then
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "Control.aspx")
            'frm_Control.DefInstance.Show() 
        Else
            Session("ScatterParent") = "ScatVarS.aspx"
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "Scatter.aspx")
            'frm_Scatter.DefInstance.Show() 
        End If



        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub


    '=============================ScatVarS.Frm  =======================
    Private Sub ScaledGraph()
        Dim RescalerC As Object
        Dim RescalerD As Object
        Dim RescalerI As Object
        Dim ICount As Short
        Dim MaxC As Object
        Dim MaxD As Object
        Dim MaxI As Object
        Dim MaxI2 As Object 'JAM 2010-01-21
        Dim MaxP As Object  'JAM 2010-01-21
        Dim MaxG As Object  'JAM 2010-01-21
        Dim RescalerI2 As Object
        Dim RescalerP As Object
        Dim RescalerG As Object

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Find maximum values
        '   -------------------

        'UPGRADE_WARNING: Couldn't resolve default property of object MaxI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MaxI = -10 ^ 10
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MaxD = -10 ^ 10
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MaxC = -10 ^ 10
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MaxI2 = -10 ^ 10    'JAM 2010-01-21 Rescaling
        MaxP = -10 ^ 10
        MaxG = -10 ^ 10
        For ICount = 1 To Session("CCount")
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRS 01/29/04
            If Not IsDBNull(Session("CVarI")(ICount)) Then
                If Session("CVarI")(ICount) > MaxI Then MaxI = Session("CVarI")(ICount)
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRS 01/29/04
            If Not IsDBNull(Session("CVarD")(ICount)) Then
                If Session("CVarD")(ICount) > MaxD Then MaxD = Session("CVarD")(ICount)
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRS 01/29/04
            If Not IsDBNull(Session("CVarC")(ICount)) Then
                If Session("CVarC")(ICount) > MaxC Then MaxC = Session("CVarC")(ICount)
            End If
            'JAM 2010-01-21 Rescaling
            If Not IsDBNull(Session("CVarISec")(ICount)) Then
                If Session("CVarISec")(ICount) > MaxI2 Then MaxI2 = Session("CVarISec")(ICount)
            End If
            If Not IsDBNull(Session("CVarIPop")(ICount)) Then
                If Session("CVarIPop")(ICount) > MaxP Then MaxP = Session("CVarIPop")(ICount)
            End If
            If Not IsDBNull(Session("CVarIGdp")(ICount)) Then
                If Session("CVarIGdp")(ICount) > MaxG Then MaxG = Session("CVarIGdp")(ICount)
            End If
        Next ICount

        '   Rescale values
        '   --------------

        'UPGRADE_WARNING: Couldn't resolve default property of object MaxI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If MaxI < 10 ^ 6 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerI = 0
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerI = 12
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Do While MaxI < (10 ^ RescalerI)
                'UPGRADE_WARNING: Couldn't resolve default property of object RescalerI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RescalerI = RescalerI - 1
            Loop
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerI = RescalerI - 2
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If MaxD < 10 ^ 6 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerD = 0
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerD = 12
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Do While MaxD < (10 ^ RescalerD)
                'UPGRADE_WARNING: Couldn't resolve default property of object RescalerD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RescalerD = RescalerD - 1
            Loop
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerD = RescalerD - 2
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If MaxC < 10 ^ 6 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerC = 0
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerC = 12
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Do While MaxC < (10 ^ RescalerC)
                'UPGRADE_WARNING: Couldn't resolve default property of object RescalerC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RescalerC = RescalerC - 1
            Loop
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RescalerC = RescalerC - 2
        End If

        'JAM 2010-01-21
        If MaxI2 < 10 ^ 6 Then
            RescalerI2 = 0
        Else
            RescalerI2 = 12
            Do While MaxI2 < (10 ^ RescalerI2)
                RescalerI2 = RescalerI2 - 1
            Loop
            RescalerI2 = RescalerI2 - 2
        End If

        If MaxG < 10 ^ 6 Then
            RescalerG = 0
        Else
            RescalerG = 12
            Do While MaxG < (10 ^ RescalerG)
                RescalerG = RescalerG - 1
            Loop
            RescalerG = RescalerG - 2
        End If

        If MaxP < 10 ^ 6 Then
            RescalerP = 0
        Else
            RescalerP = 12
            Do While MaxP < (10 ^ RescalerP)
                RescalerP = RescalerP - 1
            Loop
            RescalerP = RescalerP - 2
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For ICount = 1 To Session("CCount")
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRS 01/28/04
            If Not IsDBNull(Session("CVarI")(ICount)) Then
                Session("CVarI")(ICount) = Session("CVarI")(ICount) / 10 ^ RescalerI
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRS 01/28/04
            If Not IsDBNull(Session("CVarD")(ICount)) Then
                Session("CVarD")(ICount) = Session("CVarD")(ICount) / 10 ^ RescalerD
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRS 01/28/04
            If Not IsDBNull(Session("CVarC")(ICount)) Then
                Session("CVarC")(ICount) = Session("CVarC")(ICount) / 10 ^ RescalerC
            End If

            If Not IsDBNull(Session("CVarISec")(ICount)) Then
                Session("CVarISec")(ICount) = Session("CVarISec")(ICount) / 10 ^ RescalerI2
            End If
            If Not IsDBNull(Session("CVarIPop")(ICount)) Then
                Session("CVarIPop")(ICount) = Session("CVarIPop")(ICount) / 10 ^ RescalerP
            End If
            If Not IsDBNull(Session("CVarIGdp")(ICount)) Then
                Session("CVarIGdp")(ICount) = Session("CVarIGdp")(ICount) / 10 ^ RescalerG
            End If
        Next ICount



        '    ScaleLabel$ = GraphScaleLabel$(VarNameI$(0), RescalerI)

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    '=============================ScatVarS.Frm  =======================
    Private Sub cmd_Statistics_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Statistics.Click
        'jrs 2004/11/27
        SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
                Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
                Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
                Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
                Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
                Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
                Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))
        'mti August 2004 5 whole procedure
        Dim indvars As Short 'Object
        Dim aloop As Short
        Dim IndLabel As String
        Dim ICount As Short
        Dim Msg As String

        Dim CreateQuery As String
        Dim nRow As DataRow
        Dim tRow As Integer
        Dim da9 As OleDbDataAdapter
        Dim newtbl As DataTable
        Dim i As Integer

        Dim Depvar As String
        Dim pos As Integer
        Dim IndVar(4) As String 'JAM 2014-02-16
        Dim NumInd As Integer 'JAM 2014-01-30
        Dim test As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '
        '   Check for Completed Selection
        '   -----------------------------
        If UCase(lbl_Ind(0).Text) = "NONE" Or UCase(lbl_Dep.Text) = "NONE" Then
            Msg = "You must choose a dependent and at least one independent variable."

            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'JAM 2014-03-09 Adding condition for AllYears
        If cmb_AllYears.SelectedValue.ToString() <> "0" Then

            ''Session("bFormula") = CheckAndConvertOldFormulasToNewFormat(Session("VarNameD"), 1, False)

            pos = InStr(Session("VarNameD"), "(")
            If pos > 0 And Not Session("bFormula") Then
                Depvar = Mid(Session("VarNameD"), 1, pos - 1)
            Else
                Depvar = Session("VarNameD")
            End If

            'JAM 2014-02-12
            indvars = 0
            For ICount = 0 To 4
                If Session("Scat_FilledVar")(ICount) = True Then
                    indvars = indvars + 1
                End If
            Next ICount

            'jrs 2014/02/27
            If IsFormula(1) Then
                Call LoadAllYearsArrayComp(0, indvars + 1)
            Else
                'JAM 2014-02-12
                Call LoadAllYearsArray(Depvar, 0, indvars + 1)
            End If

            For ICount = 0 To indvars - 1
                If Session("Scat_FilledVar")(ICount) = True Then
                    'Check for old formula method
                    ''Session("bFormula") = CheckAndConvertOldFormulasToNewFormat(Session("VarNameI")(ICount), ICount + 2, False)

                    pos = InStr(Session("VarNameI")(ICount), "(")
                    If pos > 0 And Not Session("bFormula") Then
                        IndVar(ICount) = Mid(Session("VarNameI")(ICount), 1, pos - 1)
                    Else
                        IndVar(ICount) = Session("VarNameI")(ICount)
                    End If
                    'jrs 2014/02/27
                    If IsFormula(ICount + 2) Then
                        Call LoadAllYearsArrayComp(ICount + 1, indvars + 1)
                    Else
                        Call LoadAllYearsArray(IndVar(ICount), ICount + 1, indvars + 1)
                    End If
                End If
            Next ICount

            'JAM 2014-02-15
            Call ValidAllYearsArray(indvars)

            'JAM 2014-03-09
            If cmb_AllYears.SelectedValue.ToString() <> "0" Then
                If cmb_AllYears.SelectedValue.ToString() = "1" Then
                    NumInd = indvars
                Else
                    If cmb_AllYears.SelectedValue.ToString() = "2" Then
                        NumInd = indvars + 1 'JAM 2014-02-15 Adding "year" like and independent variable
                    End If
                End If
            End If

            'jrs 2022/08/10 added And not UseOnlySQLite 
            'jrs 2020/09/18
            If Not StandAloneMode And Not UseOnlySQLite Then
                db9 = Session("IFsStat_MDB")
                updateAccessWith1Command(db9, "DROP TABLE Statistics_Temp")

                CreateQuery = "Create table Statistics_Temp ([Region] Text, [Dependent] Double"
                For ICount = 1 To NumInd 'Establish 5 fields, just in case
                    CreateQuery = CreateQuery + ", [Independent" + Trim(Str(ICount)) + "] Double"
                Next
                CreateQuery = CreateQuery + ")"
                updateAccessWith1Command(db9, CreateQuery)

                SetDataTablefromAccess(db9, "SELECT * FROM Statistics_Temp", "", tbl9)

                For aloop = 1 To Session("AllValidPoints")
                    nRow = tbl9.NewRow
                    nRow.BeginEdit()
                    nRow.Item("Region") = aloop - 1
                    If IsNumeric(Session("ValidData")(aloop - 1, 1)) Then
                        nRow.Item("Dependent") = Session("ValidData")(aloop - 1, 1)
                    End If

                    For ICount = 0 To NumInd - 1
                        'JAM 2014-03-11 
                        ' If Session("Scat_FilledVar")(ICount) = True Then 'Possible that user will have skipped field
                        If ICount = 1 Then
                            test = Session("ValidData")(aloop - 1, ICount + 2)

                        End If

                        IndLabel = "Independent" & Trim(Str(ICount + 1))
                        If IsNumeric(Session("ValidData")(aloop - 1, ICount + 2)) Then

                            nRow.Item(IndLabel) = Session("ValidData")(aloop - 1, ICount + 2)
                        End If
                        'End If
                    Next ICount
                    nRow.EndEdit()
                    tbl9.Rows.Add(nRow)
                Next aloop

                WriteDataTable2Access(db9, "Select * from Statistics_Temp", tbl9, "Insert") 'mtiwebforms April 2004 ADO.Net

                SetDataTableforUpdate(db9, "SELECT * FROM Var_Names", "Delete FROM Var_Names", tbl9, newtbl) 'mtiwebforms April 2004 ADO.Net
                tRow = 0
                tbl9.Rows(tRow).Item("Var_Name") = Depvar
                tbl9.Rows(tRow).Item("Var_Type") = "Dependent"
                nRow = newtbl.NewRow
                nRow.ItemArray = tbl9.Rows(tRow).ItemArray
                newtbl.Rows.Add(nRow)

                For aloop = 1 To NumInd
                    tRow = tRow + 1


                    If cmb_AllYears.SelectedValue.ToString() = "2" Then
                        If aloop = (NumInd) Then
                            tbl9.Rows(tRow).Item("Var_Name") = "Year"
                        Else
                            tbl9.Rows(tRow).Item("Var_Name") = IndVar(aloop - 1)
                        End If
                    Else
                        tbl9.Rows(tRow).Item("Var_Name") = IndVar(aloop - 1)
                    End If


                    tbl9.Rows(tRow).Item("Var_Type") = "Independent"

                    nRow = newtbl.NewRow
                    nRow.ItemArray = tbl9.Rows(tRow).ItemArray
                    newtbl.Rows.Add(nRow)
                Next aloop


                ''For aloop = 0 To NumInd - 1  'tbl9.Rows.Count - 1
                ''    tRow = tRow + 1

                ''    nRow = newtbl.NewRow
                ''    nRow.ItemArray = tbl9.Rows(tRow).ItemArray
                ''    newtbl.Rows.Add(nRow)
                ''Next

                For aloop = NumInd + 1 To tbl9.Rows.Count - 1
                    tRow = tRow + 1

                    nRow = newtbl.NewRow
                    nRow.ItemArray = tbl9.Rows(tRow).ItemArray
                    newtbl.Rows.Add(nRow)
                Next

                WriteDataTable2Access(db9, "Select * from Var_Names", newtbl, "Insert") 'mtiwebforms April 2004 ADO.Net

                Session("Total_Records") = Session("AllValidPoints")

                Session("Total_Var") = NumInd
            End If

        Else

            'jrs 2022/08/10 added And not UseOnlySQLite 
            If Not StandAloneMode And Not UseOnlySQLite Then
                '   Open the database and delete the temporary table
                '   ------------------------------------------------

                'db9 = DAODBEngine_definst.OpenDatabase(session("IFsStat_MDB"))
                'db9.TableDefs.Delete("Statistics_Temp")
                db9 = Session("IFsStat_MDB")
                updateAccessWith1Command(db9, "DROP TABLE Statistics_Temp")
                'mtiwebform June 2004 Cross-Sectional -uncomment later
                'SetDataTablefromAccess(db, "SELECT * FROM Economic Order by Country", "", valtbl)
                '   Create the temporary table again
                '   --------------------------------
                '"CREATE TABLE allfields (f1 varchar)"
                'table9 = db9.CreateTableDef("Statistics_Temp")
                'With table9
                '    .Fields.append(.CreateField("Region", DAO.DataTypeEnum.dbText))
                '    .Fields.append(.CreateField("Dependent", DAO.DataTypeEnum.dbDouble))
                '    For ICount = 1 To 5 'Establish 5 fields, just in case
                '        IndLabel = "Independent" & Trim(Str(ICount))
                '        .Fields.append(.CreateField(IndLabel, DAO.DataTypeEnum.dbDouble))
                '    Next ICount
                'End With
                'db9.TableDefs.append(table9)
                'db9.Close()
                CreateQuery = "Create table Statistics_Temp ([Region] Text, [Dependent] Double"
                For ICount = 1 To 5 'Establish 5 fields, just in case
                    CreateQuery = CreateQuery + ", [Independent" + Trim(Str(ICount)) + "] Double"
                Next
                CreateQuery = CreateQuery + ")"
                updateAccessWith1Command(db9, CreateQuery)
                ''   Open the database again and add the data from SCVAR
                ''   ---------------------------------------------------

                'db9 = DAODBEngine_definst.OpenDatabase(session("IFsStat_MDB"))
                'tbl9 = db9.OpenRecordset("Statistics_Temp")
                SetDataTablefromAccess(db9, "SELECT * FROM Statistics_Temp", "", tbl9)

                For aloop = 1 To Session("CCount")
                    '    tbl9.AddNew()
                    'nRow = ds9.Tables("Statistics_Temp").NewRow 'tbl9.NewRow
                    nRow = tbl9.NewRow
                    nRow.BeginEdit()
                    nRow.Item("Region") = aloop
                    'jrs 2006/01/03
                    If IsNumeric(Session("Scat_SCVarD")(aloop)) Then
                        nRow.Item("Dependent") = Session("Scat_SCVarD")(aloop)
                    End If

                    indvars = 0
                    For ICount = 0 To 4
                        '        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_FilledVar")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        If Session("Scat_FilledVar")(ICount) = True Then 'Possible that user will have skipped field
                            '            'UPGRADE_WARNING: Couldn't resolve default property of object indvars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            indvars = indvars + 1
                            '            'UPGRADE_WARNING: Couldn't resolve default property of object indvars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            IndLabel = "Independent" & Trim(Str(indvars))
                            '            'UPGRADE_WARNING: Couldn't resolve default property of object SCVarI(aloop, ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'jrs 2014/03/26
                            If IsNumeric(Session("Scat_SCVarI")(aloop, ICount)) Then
                                nRow.Item(IndLabel) = Session("Scat_SCVarI")(aloop, ICount)
                            End If
                        End If
                    Next ICount
                    '    tbl9.Update()
                    nRow.EndEdit()
                    tbl9.Rows.Add(nRow)
                    'ds9.Tables("Statistics_Temp").Rows.Add(nRow)
                Next aloop

                WriteDataTable2Access(db9, "Select * from Statistics_Temp", tbl9, "Insert") 'mtiwebforms April 2004 ADO.Net


                ''   Add the variable names
                ''   ----------------------
                'db9 = DAODBEngine_definst.OpenDatabase(session("IFsStat_MDB"))
                'tbl9 = db9.OpenRecordset("Var_Names")
                'SetDataTablefromAccess(db9, "SELECT * FROM Var_Names", "", tbl9)
                SetDataTableforUpdate(db9, "SELECT * FROM Var_Names", "Delete FROM Var_Names", tbl9, newtbl) 'mtiwebforms April 2004 ADO.Net
                tRow = 0
                'tbl9.MoveFirst()
                'tbl9.Edit()
                tbl9.Rows(tRow).Item("Var_Name") = Session("VarNameD")
                tbl9.Rows(tRow).Item("Var_Type") = "Dependent"
                nRow = newtbl.NewRow
                nRow.ItemArray = tbl9.Rows(tRow).ItemArray
                newtbl.Rows.Add(nRow)
                'tbl9.Rows(tRow).AcceptChanges()
                'tbl9.Update()
                'tbl9.AcceptChanges()
                ''UPGRADE_WARNING: Couldn't resolve default property of object indvars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                For aloop = 1 To indvars
                    '    tbl9.MoveNext()
                    tRow = tRow + 1
                    '    tbl9.Edit()
                    tbl9.Rows(tRow).Item("Var_Name") = Session("VarNameI")(aloop - 1)
                    tbl9.Rows(tRow).Item("Var_Type") = "Independent"
                    '    tbl9.Update()

                    nRow = newtbl.NewRow
                    nRow.ItemArray = tbl9.Rows(tRow).ItemArray
                    newtbl.Rows.Add(nRow)
                Next aloop

                'jrs 11/11/04 Var_Names should always have the same number of rows
                For aloop = indvars + 1 To tbl9.Rows.Count - 1
                    tRow = tRow + 1

                    nRow = newtbl.NewRow
                    nRow.ItemArray = tbl9.Rows(tRow).ItemArray
                    newtbl.Rows.Add(nRow)
                Next

                'tbl9.AcceptChanges()
                'WriteDataTable2Access(db9, "Select * from Var_Names", tbl9, "Update", "Delete * from Var_Names") 'mtiwebforms April 2004 ADO.Net
                WriteDataTable2Access(db9, "Select * from Var_Names", newtbl, "Insert") 'mtiwebforms April 2004 ADO.Net

                'db9.Close()
                'end mtiwebform June 2004 Cross-Sectional -uncomment later

                '   Set the temporary variables needed for statistics
                '   -------------------------------------------------

                'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Total_Records") = Session("CCount")
            Else
                Session("AllValidPoints") = Session("CCount")
                ReDim Session("ValidData")(Session("CCount"), 6)
                For aloop = 1 To Session("CCount")
                    Session("ValidData")(aloop - 1, 1) = Session("Scat_SCVarD")(aloop)
                    For ICount = 0 To 4
                        If Session("Scat_FilledVar")(ICount) = True Then
                            Session("ValidData")(aloop - 1, ICount + 2) = Session("Scat_SCVarI")(aloop, ICount)
                        End If
                    Next
                Next

                indvars = 0
                For ICount = 0 To 4
                    If Session("Scat_FilledVar")(ICount) = True Then 'Possible that user will have skipped field
                        indvars = indvars + 1
                    End If
                Next ICount
            End If
            Session("Total_Var") = indvars

        End If

        'JAM 2009-03-12 Assign the properly value
        Session("Statistics_Type") = 1
        'JAM 2009-12-07 Scatter Origin for Saturation Point
        Session("SaturOrigin") = "Scatter"

        'JAM 2013-09-15 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            Session("ScatVarVCat") = fSubSetchosen.Value
        Else
            If Session("ScatVarVCat") Is Nothing Then
                Session("ScatVarVCat") = 0
            Else
                fSubSetchosen.Value = Session("ScatVarVCat")
            End If
        End If
        If fOthFiltchosen.Value <> "" Then
            Session("ScatVarFilt") = fOthFiltchosen.Value
        Else
            If Session("ScatVarFilt") Is Nothing Then
                Session("ScatVarFilt") = 3  'All Value Selected
            Else
                fOthFiltchosen.Value = Session("ScatVarFilt")
            End If
        End If


        '   Load the transformation form
        '   ----------------------------

        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        If Not StandAloneMode Then
            'jrs 2023/09/27 Support.FormManager.Show(Me, "Transform.aspx")
            Support.FormManager.Show(Me, "TransformPy.aspx")
        Else
            Support.FormManager.Show(Me, "TransformPy.aspx")
        End If
        'frm_Transform.DefInstance.Show() 

    End Sub

    Private Sub cmdComputations_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdComputations.Click
        'JRS 9/25/01 COMPUTATIONS
        Dim i As Short
        'jrs 2006/01/02
        Dim sId As String
        sId = "frm_ScatVarS"

        'JAM 2013-09-15 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            Session("ScatVarVCat") = fSubSetchosen.Value
        Else
            If Session("ScatVarVCat") Is Nothing Then
                Session("ScatVarVCat") = 0
            Else
                fSubSetchosen.Value = Session("ScatVarVCat")
            End If
        End If
        If fOthFiltchosen.Value <> "" Then
            Session("ScatVarFilt") = fOthFiltchosen.Value
        Else
            If Session("ScatVarFilt") Is Nothing Then
                Session("ScatVarFilt") = 3  'All Value Selected
            Else
                fOthFiltchosen.Value = Session("ScatVarFilt")
            End If
        End If


        If Session("sOriginForm") = "" Or Session("sOriginForm") = sId Then
            Session("sOriginForm") = sId


            UpdateCurrentDisplay()
            'jrs 2007/07/30
            If Session("Scat_iCurrentIdx") < 4 Then
                Session("bCompSVtmp")(Session("Scat_iCurrentIdx")) = True
                'mnuGeographic.Enabled = False
            End If

            If Session("Scat_sCurrentDisplay") = "" Then
                ResetComputationsFrm()
            Else
                i = Session("Scat_iCurrentIdx")
                If i <= UBound(Session("aFormulas")) Then
                    If Not (Session("aFormulas")(i) Is Nothing) Then
                        If Session("Scat_sCurrentDisplay") = Session("aFormulas")(i).Name Then

                            Session("ComptxtFormula") = Session("aFormulas")(i).Formula_Renamed
                            Session("ComplblFormulaName") = Session("aFormulas")(i).Name
                            Session("ComplblDimensionality") = Session("aFormulas")(i).Dimension
                            Session("ComplblUnits") = Session("aFormulas")(i).Unit


                            'jrs 2008/09/02
                            If Session("CCount") <> Session("TotCountries") Then
                                'Reset countries formula
                                Session("CCount") = Session("TotCountries")
                                If i <= UBound(Session("FullaFormulas")) Then
                                    If Not (Session("FullaFormulas")(i) Is Nothing) Then
                                        If Session("Scat_sCurrentDisplay") = Session("FullaFormulas")(i).Name Then
                                            Session("aFormulas")(i) = Session("FullaFormulas")(i)
                                        End If
                                    End If
                                End If
                            End If

                            Session("CompLoadFormula") = Session("aFormulas")(i)

                            'DeleteVar (i)
                        Else
                            ResetComputationsFrm()
                        End If
                    Else
                        ResetComputationsFrm()
                    End If
                Else
                    ResetComputationsFrm()
                End If
            End If
            'jrs 2007/09/03
            Session("bNewFormula") = True

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")

        Else

            LoadMessage("Another Form is using this option, please close forms that you are not using anymore", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If


    End Sub

    '=============================ScatVarS.Frm==============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.GotFocus' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.GotFocus.html'.
    Private Sub frm_ScatVarS_GotFocus(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) 'Handles frm_ScatVarS.GotFocus
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 9/16/01 COMPUTATIONS
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'If frm_ScatVarS.DefInstance.Visible Then WinFormsToWeb_FocusControl.Focus("cmb_VarName")
    End Sub

    '=============================ScatVarS.Frm==============================
    'CONVERSION_NOTE: The code of Event 'frm_ScatVarS.frm_ScatVarS_Load' was moved to Method 'frm_ScatVarS.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_ScatVarS_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'mti IfsWeb Menu license
        wmContext.UserData = "jsolorza::976739975"

        wmContext.ClearPixelImage = "/ClearPixel.gif"
        wmContext.PopupIcon = "/Popup.gif"
        wmContext.ScrollDownActiveImage = "/ScrollDownActive.gif"
        wmContext.ScrollDownEnabledImage = "/ScrollDownEnabled.gif"
        wmContext.ScrollUpActiveImage = "/ScrollUpActive.gif"
        wmContext.ScrollUpEnabledImage = "/ScrollUpEnabled.gif"
        wmContext.SelectedPopupIcon = "/SelectedPopup.gif"
        wmContext.MenuBar.ShowOnMouseOver = True
        'jrs 2012/01/24
        wmContext.ButtonFont.Size = "12px"
        wmContext.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group

        cswmGroup1 = wmContext.Groups.Add()
        'cswmGroup1.Caption = " Options  "
        cswmGroup1.ID = "Group_0bd4"


        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Data Information"

        'mti August 2004 2 -start
        cswmGroup1 = wmContext.Groups.Add()
        'cswmGroup1.Caption = " Options  "
        cswmGroup1.ID = "Group_0cd4"


        'Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select"
        'mti August 2004 2 -end

        AddHandler wmContext.ServerItemClick, AddressOf ItemClickHandlerContext

        'mti WebForm Changes From June 26, 2004
        'JRSVBNET cmb_VarName.Attributes.Add("onchange", "ShowContextMenuat();")

        'If Session("ClickedOnVarName") = True Then
        'wmContext.Visible = False 'Session("ClickedOnVarName")

        'cmb_VarName.Attributes.Add("onchange", "cswmShow('Group_0bd4', 'cmb_VarName', 'below', 0,0, false);")

        'cmb_VarName.Attributes.Add("onchange", "if document.getElementById('cmb_VarName').value != 'NONE' " & vbCrLf & " { cswmShow('Group_0bd4', 'cmb_VarName', 'below', 0,0, false)};")
        'cmb_VarName.Attributes.Add("onchange", "ShowContextMenuat('cmb_VarName',0,0);")
        ''mti WebForm Changes From June 26, 2004
        'cButton.Attributes.Add("onclick", "cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);")

        cmd_Statistics.Attributes.Add("onClick", "showhg();")


        'JAM 2013-09-21 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            Session("ScatVarVCatName") = fSubSetchosenName.Value
        Else
            If Session("ScatVarVCatName") Is Nothing Then
                Session("ScatVarVCatName") = "All"
            Else
                fSubSetchosenName.Value = Session("ScatVarVCatName")

            End If
        End If

        If fOthFiltchosen.Value <> "" Then
            Session("ScatVarFiltName") = fOthFiltchosenName.Value
        Else
            If Session("ScatVarFiltName") Is Nothing Then
                Session("ScatVarFiltName") = "ALL"  'All Value Selected
            Else
                fOthFiltchosenName.Value = Session("ScatVarFiltName")
            End If
        End If
        '''''''''''''''''''''''

        'jrs 2006/01/02
        'If Not Page.IsPostBack then
        'JAM 2013-09-21 Only retrieve values if filters haven't chosen
        If Not Page.IsPostBack Then

            If Session("frm_ScatVarSCtrlsLd") Then
                opt_Dependent.Checked = Session("frm_ScatVarSopt_DependentChecked")
                _opt_Independent_0.Checked = Session("frm_ScatVarSopt_Independent0")
                _opt_Independent_1.Checked = Session("frm_ScatVarSopt_Independent1")
                _opt_Independent_2.Checked = Session("frm_ScatVarSopt_Independent2")
                _opt_Independent_3.Checked = Session("frm_ScatVarSopt_Independent3")
                _opt_Independent_4.Checked = Session("frm_ScatVarSopt_Independent4")
                opt_Control.Checked = Session("frm_ScatVarSopt_Control")

                lbl_Dep.Text = Session("frm_ScatVarSlbl_Dep")
                _lbl_Ind_0.Text = Session("frm_ScatVarSlbl_Ind0")
                _lbl_Ind_1.Text = Session("frm_ScatVarSlbl_Ind1")
                _lbl_Ind_2.Text = Session("frm_ScatVarSlbl_Ind2")
                _lbl_Ind_3.Text = Session("frm_ScatVarSlbl_Ind3")
                _lbl_Ind_4.Text = Session("frm_ScatVarSlbl_Ind4")
                lbl_Cont.Text = Session("frm_ScatVarSlbl_Cont")

                Session("frm_ScatVarSCtrlsLd") = Nothing
                'jrs 2006/02/21 
                Select Case True
                    Case _opt_Independent_0.Checked
                        Session("Scat_IndIndex") = 0
                    Case _opt_Independent_1.Checked
                        Session("Scat_IndIndex") = 1
                    Case _opt_Independent_2.Checked
                        Session("Scat_IndIndex") = 2
                    Case _opt_Independent_3.Checked
                        Session("Scat_IndIndex") = 3
                    Case _opt_Independent_4.Checked
                        Session("Scat_IndIndex") = 4
                End Select

            Else
                Session("frm_ScatVarSopt_DependentChecked") = opt_Dependent.Checked
                Session("frm_ScatVarSopt_Independent0") = _opt_Independent_0.Checked
                Session("frm_ScatVarSopt_Independent1") = _opt_Independent_1.Checked
                Session("frm_ScatVarSopt_Independent2") = _opt_Independent_2.Checked
                Session("frm_ScatVarSopt_Independent3") = _opt_Independent_3.Checked
                Session("frm_ScatVarSopt_Independent4") = _opt_Independent_4.Checked
                Session("frm_ScatVarSopt_Control") = opt_Control.Checked
                'jrs 2006/02/22
                Session("frm_ScatVarSlbl_Cont") = lbl_Cont.Text

                'jrs 2010/09/06
                If Not IsNothing(Session("frm_ScatVarSlbl_Dep")) Then
                    lbl_Dep.Text = Session("frm_ScatVarSlbl_Dep")
                    _lbl_Ind_0.Text = Session("frm_ScatVarSlbl_Ind0")
                    _lbl_Ind_1.Text = Session("frm_ScatVarSlbl_Ind1")
                    _lbl_Ind_2.Text = Session("frm_ScatVarSlbl_Ind2")
                    _lbl_Ind_3.Text = Session("frm_ScatVarSlbl_Ind3")
                    _lbl_Ind_4.Text = Session("frm_ScatVarSlbl_Ind4")
                    lbl_Cont.Text = Session("frm_ScatVarSlbl_Cont")
                End If



            End If

            'JAM 2013-09-15 Else from PostBack

            'If Not Session("SubSetchosen") Is Nothing Then
            '    cmb_VCAT.SelectedIndex = Session("SubSetchosen")

            '    'fSubSetchosen.Value
            'End If

            'Else 'JAM 2013-09-15 Else from PostBack



        End If


        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim AddName As String
            Dim NameFound As String
            Dim Definition As String
            Dim Series As String
            Dim GroupNameNum As Object
            Dim Msg As String
            Dim ICount, ix As Short
            Dim Rcnt As Short
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Carret = "<br>" & Chr(10)

            'mti taken to session_on Start
            'mtiwebform June 2004 Cross-Sectional - commented out

            Session("Scat_VarGroupNames")(1) = "ALL"

            'jrs 2006/08/04
            cmb_VCAT.Items.Clear()
            'KIA 09/22/2009 comment the following line
            ' cmb_VCAT.Items.Add(New ListItem("ALL"))
            For ICount = 2 To 100
                Session("Scat_VarGroupNames")(ICount) = ""
            Next ICount
            'end 'mti taken to session_on Start

            'jrs 2010/09/08 Session("CCount") = 200
            Session("Scat_DivOnPrev") = False
            Session("Scat_MulOnPrev") = False
            Session("Scat_AddOnPrev") = False
            Session("Scat_SubOnPrev") = False
            'jrs 2012/01/30
            Session("AvailabilityParent") = "ScatVarS.aspx"

            '   Set up labels and options
            '   -------------------------
            Msg = "Select Independent Variable and Data Table (e.g. GDP - Economic):"
            lbl_VarName.Text = Msg
            lbl_vcat.Text = "Show Variable Name Subset:"

            'mtiwebform June 2004 Cross-Sectional -commented
            'lbl_Dep.Text = "None"
            'lbl_Cont.Text = "None"

            'jrs 2010/09/06
            'opt_Dependent.Checked = True
            'For ICount = 0 To 4
            '    opt_Independent(ICount).Checked = False
            '    opt_Independent(ICount).Text = "Independent " & Str(ICount + 1)
            'Next ICount


            '   Make Year Selection Invisible
            '   -----------------------------
            cmb_YearSelection.Visible = False
            lstDIM1.Visible = False
            lbl_Year.Visible = False

            '   Load Group Name Combo Box
            '   ----------------------------
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

            'mti Augut 2004 3
            'cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text = ""

            'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            GroupNameNum = 0

            'jrs 2011/07/06
            Session("iFilterDataPoints") = 10
            Session("iFilterYear") = LastYearHistory
            LoadFilter()
            'jrs 2011/07/06
            Dim sPreprocessor As String, sYears As String, bInclude As Boolean
            If Session("ScatVarFilt") Is Nothing Then
                Session("ScatVarFilt") = 3
                Session("ScatVarFiltName") = "ALL"
            End If

            '    Set db = OpenDatabase(FDRV$ + "Data\DataDict.MDB", , True)
            'db = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "DataDict.MDB")
            'valtbl = db.OpenRecordset("DataDict")
            'valtbl.index = "Variable"
            '         valtbl.MoveFirst()
            db = Session("PutFdrvRun") & "DataDict.MDB"

            'JAM 2013-08-31
            'SetDataTablefromAccess(db, "SELECT * FROM datadict order by Variable", "", valtbl)

            SetDataTablefromAccess(db, "SELECT * FROM datadict order by Variable", "", valtbl)
            'Do
            'KIA 09/22/2009 declare the following variables and replace the code until you find end
            'JAM 2013-08-31
            Session("DataDictVars") = valtbl
            Dim Groups As Array
            Dim tmpStr As String
            Dim pos1 As Integer
            'jrs 2012/02/28
            Dim sCoVaTra As String

            'JAM 2013-09-07 Based on code developed by Jose in frm_PackagedDisplayFlex
            Dim Str As String
            Dim GrpCol As Collection
            GrpCol = New Collection
            Dim x As Object, ii As Integer, j As Integer
            Do While (Not valtbl.Rows.Count = j)
                Str = IIf(IsDBNull(valtbl.Rows(j).Item("Group")), "", valtbl.Rows(j).Item("Group"))
                x = Split(valtbl.Rows(j).Item("Group"), ",")
                'On Error Resume Next
                For ii = 0 To UBound(x)
                    If Not GrpCol.Contains(Trim(x(ii))) Then
                        If Trim(x(ii)) <> "Automobile" Then 'JAM 2013-09-07
                            GrpCol.Add(Trim(x(ii)), Trim(x(ii)))
                        End If
                    End If
                Next ii
                'On Error GoTo 0
                If UBound(x) = -1 Then
                    GrpCol.Add(Trim(Str), Trim(Str))
                End If
                j = j + 1
            Loop

            Dim Grp As Object
            Dim GrpArr() As String
            ReDim GrpArr(GrpCol.Count)
            ii = 0
            'CatArr(ii) = "All"
            'ii = 1
            For Each Grp In GrpCol
                ii = ii + 1
                GrpArr(ii) = Grp
            Next Grp
            QuickSort(GrpArr, 1, GrpCol.Count)


            cmb_VCAT.Items.Add("All")
            For ii = 1 To GrpCol.Count
                cmb_VCAT.Items.Add(CStr(GrpArr(ii)))
                GroupNameNum = GroupNameNum + 1
                'jrs 2013/09/10 
                'Session("Scat_VarGroupNames")(GroupNameNum) = Session("Scat_GroupName")

            Next ii
            ' rsl.Clear()

            'JAM 2013-09-07
            'For Rcnt = 0 To valtbl.Rows.Count - 1
            '    'JAM 2013-08-31
            '    'Session("Scat_TableName") = valtbl.Rows(Rcnt).Item("Table")
            '    'jrs 2012/02/28
            '    'JAM 2013-08-31
            '    'sCoVaTra = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("CoVaTra")), "No", valtbl.Rows(Rcnt).Item("CoVaTra"))
            '    tmpStr = Trim(valtbl.Rows(Rcnt).Item("Group"))
            '    Groups = Split(tmpStr, ",")
            '    pos1 = 0
            '    While pos1 <= UBound(Groups)
            '        Session("Scat_GroupName") = Trim(Groups(pos1))
            '        pos1 = pos1 + 1

            '        'JAM 2013-0831
            '        'If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(sCoVaTra) = "NO" Then 'jrs 2012/02/28 Session("Scat_GroupName") <> "Automobile" Then
            '        '    Session("Scat_VarName") = valtbl.Rows(Rcnt).Item("Variable")
            '        '    Series = valtbl.Rows(Rcnt).Item("Series")
            '        'Definition = valtbl.Rows(Rcnt).Item("Definition")
            '        'JAM 2011-09-25 Replacing two and three blank spaces
            '        'JAM 2013-08-31
            '        'Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")
            '        'NameFound = CStr(False)
            '        'JAM 2013-08-31
            '        'For ICount = 1 To 100
            '        '    If Session("Scat_GroupName") = Session("Scat_VarGroupNames")(ICount) Then NameFound = CStr(True)
            '        'Next ICount
            '        'JAM 2013-08-31
            '        ' If CBool(NameFound) = False Then
            '        cmb_VCAT.Items.Add(New ListItem(Session("Scat_GroupName")))
            '        GroupNameNum = GroupNameNum + 1
            '        Session("Scat_VarGroupNames")(GroupNameNum) = Session("Scat_GroupName")
            '        'JAM 2013-08-31
            '        'End If
            '        'End If
            '        'valtbl.MoveNext()
            '        '        If valtbl.EOF = True Then Exit Do
            '        'Loop
            '    End While
            '    'KIA 09/22/2009 END
            'Next Rcnt
            'SortItems()
            'If lstVarName.Items.Count <= 0 Then 'JAM 2013-09-18

            'JAM 2013-09-21

            'If (Session("ScatVarVCatName") = "All" And Session("ScatVarFiltName") = "ALL") Then


            'JAM 2013-10-19
            ' lstVarName.Items.Clear()
            '''''''''''''''''''''''''''''''



            AddName = ""


            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'cmb_VarName.Items(cmb_VarName.SelectedIndex).Text = ""
            '    Set db = OpenDatabase(FDRV$ + "Data\DataDict.MDB", , True)
            'db = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "DataDict.MDB")
            'valtbl = db.OpenRecordset("DataDict")
            'valtbl.index = "Variable"
            'valtbl.MoveFirst()
            'JRS 08/29/03
            Dim sUnits As String

            'JAM 2013-08-31
            'SetDataTablefromAccess(db, "SELECT * FROM datadict order by Variable", "", valtbl)
            'valtbl = Session("DataDictVars")


            'Do


            'If cmb_OthFilt.Items.Count > 0 Then
            '    Session("ScatVarFiltName") = cmb_OthFilt.SelectedValue
            '    Session("ScatVarFilt") = cmb_OthFilt.SelectedIndex
            'End If


            'If Session("ScatVarVCat") > 0 Then

            '    GroupSelect = cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text
            'Else
            '    GroupSelect = "All"
            'End If


            '''''''''''''''''''''''''''''JAM 2013-10-26'''''''''''''''''''''''''
            ''For Rcnt = 0 To valtbl.Rows.Count - 1
            ''    Session("Scat_TableName") = valtbl.Rows(Rcnt).Item("Table")
            ''    Session("Scat_GroupName") = valtbl.Rows(Rcnt).Item("Group")
            ''    'jrs 2012/02/28
            ''    sCoVaTra = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("CoVaTra")), "No", valtbl.Rows(Rcnt).Item("CoVaTra"))
            ''    If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(sCoVaTra) = "NO" Then 'jrs 2012/02/28 Session("Scat_GroupName") <> "Automobile" Then
            ''        Session("Scat_VarName") = valtbl.Rows(Rcnt).Item("Variable")
            ''        Series = valtbl.Rows(Rcnt).Item("Series")
            ''        'Definition = valtbl.Rows(Rcnt).Item("Definition")
            ''        'JAM 2011-09-25 Replacing two and three blank spaces
            ''        Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

            ''        'jrs 2011/07/08
            ''        sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
            ''        sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))

            ''        'JRS 08/29/03
            ''        If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
            ''            sUnits = ""
            ''        Else
            ''            sUnits = valtbl.Rows(Rcnt).Item("Units")
            ''        End If

            ''        'JAM 2013-09-07 Commented binclude an if clauses
            ''        'jrs 2011/07/06
            ''        'bInclude = bIncludedByOthrFilter(sPreprocessor, sYears) 'JAM 2013-09-18
            ''        'If bInclude Then
            ''        AddName = Session("Scat_VarName") & " - " & Definition & " - " & sUnits
            ''        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            ''        'mti August 2004 2 start
            ''        'AddName = AddName.Trim()
            ''        dataString = dataString & AddName & "|"
            ''        'cmb_VarName.Items.Add(New ListItem(AddName, valtbl.Rows(Rcnt).Item("Cohort") + " - " + Str(Rcnt)))

            ''        'JAM 2013-10-19

            ''        'lstVarName.Items.Add(New ListItem(AddName))

            ''        ''''''''''''''''''''''''''''
            ''        Session("DataString") = dataString
            ''        'mti August 2004 2 end
            ''        'End If
            ''    End If
            ''    '    valtbl.MoveNext()
            ''    '    If valtbl.EOF = True Then Exit Do
            ''    'Loop
            ''Next Rcnt


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            ' End If
            'End If'JAM 2013-09-21
            'valtbl.Close()
            'db.Close()

            '   Count Countries
            '   ---------------

            'db = DAODBEngine_definst.OpenDatabase(session("IFs_MDB2Active"))
            'valtbl = db.OpenRecordset("Economic") 'Arbitrary
            'valtbl.index = "Country"
            'valtbl.MoveFirst()
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM Economic", "", valtbl)
            'ICount = 0
            'Do
            '    ICount = ICount + 1
            '    valtbl mlk.MoveNext()
            '    If valtbl.EOF = True Then Exit Do
            'Loop

            'JAM 2011-11-01 Adding ->or Session("CCount") conditions
            If (Session("CCount") Is Nothing) Or (Session("CCount") <> Session("TotCountries")) Then
                Session("CCount") = valtbl.Rows.Count   'ICount
                Session("TotCountries") = Session("CCount")
            End If

            'valtbl.Close()
            'db.Close()
            Session("ClickedOnVarName") = False

            'jrs 2011/07/11
            If Session("ScatVarFilt") > -1 Then
                cmb_OthFilt.SelectedIndex = Session("ScatVarFilt")
                If Session("ScatVarVCat") = -1 Then
                    'jrs 2013/09/10 cmb_OthFilt_SelectedIndexChangedBK(Me, New EventArgs)
                End If
            End If

            'mti August 2004 2
            'If Session("Scat_IndIndex") Is Nothing Then
            '    Session("Scat_IndIndex") = 0
            'End If
            'mti August 2004 2
            If Session("ScatVarVCat") Is Nothing Then
                Session("ScatVarVCat") = -1
            ElseIf Session("ScatVarVCat") > -1 Then 'jrs 2006/08/01
                If cmb_VCAT.Items.Count >= Session("ScatVarVCat") Then
                    cmb_VCAT.SelectedIndex = Session("ScatVarVCat")
                End If
                'JAM 2013-08-28 Keeping index selected
                'JAM 2013-08-10 Commented in order to test javascript implementation
                'jrs 2013/09/10 Call cmb_vcat_SelectedIndexChangedBK(eventSender, New EventArgs)
            End If 'jrs 09/15/04

            'jrs 2014/03/26
            If Session("ScatVArAllYears") Is Nothing Then
                Session("ScatVArAllYears") = -1
            ElseIf Session("ScatVArAllYears") > -1 Then
                If cmb_AllYears.Items.Count >= Session("ScatVArAllYears") Then
                    cmb_AllYears.SelectedIndex = Session("ScatVArAllYears")
                End If
            End If

            Session("RefreshGraph") = "true"
            'jrs 2006/01/02
            If Session("bComputation") Then
                SelectVar()
            End If

            If Session("ThirdDimOk") Is Nothing Then
                ReDim Session("CVArISec")(Session("CCount"))
                ReDim Session("CVarIPop")(Session("CCount"))
                ReDim Session("CVarIGdp")(Session("CCount"))
                ReDim Session("IPopOrig")(Session("CCount"))
                ReDim Session("IGdpOrig")(Session("CCount"))
                ReDim Session("OrigSec")(Session("CCount"))

            End If

            'JAM 2010-01-30
            Session("XAxisLabel") = ""
            Session("YAxisLabel") = ""
            Session("TitleLabel") = ""
            Session("SubTitleLabel") = ""
            'JAM 2010-03-07
            Session("sAggregation") = ""

            'jrs 2010/09/06
            If Session("OrigAgg") Is Nothing Then
                ReDim Session("OrigAgg")(3)

                'JAM 2010-03-07 Aggregation Arrays for gLists
                For ICount = 0 To 2
                    Session("OrigAgg")(ICount) = ""
                Next ICount

                '''''''''''''''JAM 2011-02-19   Code moved into this IF clause

                'JAM 2010-03-18 GList Variables
                Session("IsaGroup") = False
                Session("IsaGlist") = False
                Session("ScatVarGListParent") = ""
                Session("GChanged") = False

                'JAM 2010-03-20 GList Variables
                ReDim Session("GListMember")(Session("TotCountries"))
                ReDim Session("GListIsGroup")(Session("TotCountries"))
                Session("GlistCount") = 0
                Session("GListName") = ""
                ReDim Session("AuxArray")(Session("TotCountries"))

                ReDim Session("OrigTemp")(Session("TotCountries"))
                ReDim Session("OrigDep")(Session("TotCountries"))
                ReDim Session("FullDep")(Session("TotCountries"))
                ReDim Session("OrigIndFir")(Session("TotCountries"))
                ReDim Session("FullInd")(Session("TotCountries"))
                ReDim Session("OrigIndSec")(Session("TotCountries"))
                ReDim Session("CountryRegress")(Session("TotCountries"))
                Session("gCountGroupMembers") = 0

                'JAM 2016-08-05 Redim Lag Arrays
                'ReDim Session("LDepYear")(GCount + 1), LIndYear(GCount + 1), LSecIndYear(GCount + 1)
                ReDim Session("LDepYear")(Session("TotCountries"))
                ReDim Session("LIndYear")(Session("TotCountries"))
                ReDim Session("LSecIndYear")(Session("TotCountries"))

                'JAM 2016-10-29 Arrays for loading variables from chosen year
                ReDim Session("TblDepYear")(2)
                ReDim Session("TblIndYear")(2)
                ReDim Session("TblSecIndYear")(2)



                'JAM 2016-07-02 Initializing arrays
                For i = 1 To Session("TotCountries")
                    Session("OrigTemp")(i) = 0
                    Session("OrigDep")(i) = 0
                    Session("FullDep")(i) = 0
                    Session("OrigIndFir")(i) = 0
                    Session("FullInd")(i) = 0
                    Session("OrigIndSec")(i) = 0
                    Session("CountryRegress")(i) = 0

                Next





                'JAM 2010-07-18
                ReDim Session("CVarCounName")(Session("TotCountries"))
                '''''''''''''''''''''''''''''''''''''''''''''
                'jrs 2012/08/14
                ReDim Session("CVarCode")(Session("TotCountries"))
                ReDim Session("CVarCounCode")(Session("TotCountries"))

            End If

            'JAM 2011-02-19 Setting flags properly
            If Not Session("IsaGlist") And Not Session("IsaGroup") Then
                Session("mnuCountriesChecked") = True
                Session("mnuGeographicChecked") = False
                Session("mnuGroupChecked") = False
            Else
                If Session("IsaGlist") And Session("IsaGroup") Then
                    Session("mnuCountriesChecked") = False
                    Session("mnuGeographicChecked") = False
                    Session("mnuGroupChecked") = True
                Else
                    Session("mnuCountriesChecked") = False
                    Session("mnuGeographicChecked") = True
                    Session("mnuGroupChecked") = False


                End If

            End If


            ''''''''''''''''''''''JAM 2010-12-15
            Session("WholeCountries") = True
            ReDim Session("SaturCVarD")(Session("TotCountries"))
            Session("mnuExcludeUnselectedChecked") = False
            ReDim Session("ShowPoints")(Session("TotCountries"))
            For ix = 0 To (Session("TotCountries")) - 1
                Session("ShowPoints")(ix) = True
            Next
            'JAM 2019-02-10
            ReDim Session("ShowLabels")(Session("TotCountries"))
            For ix = 0 To (Session("TotCountries")) - 1
                Session("ShowLabels")(ix) = False
            Next

            Session("PowerDegree") = 1
            Session("mnuTurnOffCaption") = False
            Session("mnuLinear") = False
            Session("mnuLogarithmic") = False
            Session("mnuExponential") = False
            Session("mnuPower") = False
            Session("ChangeSatPoint") = False
            Session("SaturationValue") = 100
            Session("mnuUnderlyingChecked") = True
            Session("mnuUnderlyingVisible") = True
            Session("mnuEqTurnChecked") = False
            Session("mnuEqTurnEnabled") = False
            Session("RegressType") = ""
            Session("TotMulGroups") = 1
            ''''''''''''''''''''''''''''''''''''''''''''''''

            'JAM 2016-07-02 Initializing SubSetLabel Array
            ReDim Session("SubsetLabels")(Session("TotMulGroups"))


            For ix = 1 To Session("TotMulGroups")
                Session("SubsetLabels")(ix) = ""

            Next ix



            'JAM 2010-03-18 Gdp and Population Aggregation Rules
            If Session("GdpAggRule") <> "" And Session("PopAggRule") <> "" Then
                db = Session("PutFdrvRun") & "DataDict.MDB"
                SetDataTablefromAccess(db, "SELECT Variable FROM datadict WHERE variable = " & "'" & "GDP2000" & "'", "", valtbl)

                For Rcnt = 0 To valtbl.Rows.Count - 1
                    Session("GdpAggRule") = valtbl.Rows(Rcnt).Item("Variable")
                Next Rcnt

                SetDataTablefromAccess(db, "SELECT Variable FROM datadict WHERE variable = " & "'" & "Population" & "'", "", valtbl)

                For Rcnt = 0 To valtbl.Rows.Count - 1
                    Session("PopAggRule") = valtbl.Rows(Rcnt).Item("Variable")
                Next Rcnt
            End If

            'jrs 2010/09/07
            If Session("bCompSVtmp") Is Nothing Then
                ReDim Session("bCompSVtmp")(3)
                Dim i As Integer
                For i = 0 To 3
                    Session("bCompSVtmp")(i) = False
                Next
                Session("bNewFormula") = False
                'jrs 2008/09/02
                ReDim Session("FullaFormulas")(1)
            End If

        Else

            'JAM 2013-10-19'''''''''''''''''''
            'LoadListData()
            '''''''''''''''''''''''''''''''''''


            'jrs 08/31/04
            'cmb_vcat_SelectedIndexChanged(Me, New EventArgs)

            'JAM 2013-09-15
            'If Not Session("ScatVarVCat") Is Nothing Then

            '    If cmb_VCAT.Items.Count >= Session("ScatVarVCat") Then
            '        cmb_VCAT.SelectedIndex = Session("ScatVarVCat")
            '    End If

            'End If

        End If
        'JRS 09/03/04
        'jrs 2011/07/11 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = "Select Variables to Analyze Across Countries (with Scattergram)" & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")

        'JAM 2010-01-30
        Session("LabelChanged") = False
        'jrs 2012/01/19
        If Not IsNothing(Session("LoadSearchVar")) Then
            LoadSearch(Session("LoadSearchVar"))
            Session("LoadSearchVar") = Nothing
        End If



        'jrs 2013/09/03
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
            ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)


        'jrs 2013/09/10
        If Not CBool(DevelopmentMode) Then
            cmdGeneratejs.Visible = False
        End If

        'JAM 2018-06-30 Managing None Code
        If Page.IsPostBack Then
            If UCase(txtVarSearch.Text) = "NONE" Then
                Session("Scat_SE") = ""
                If opt_Dependent.Checked Then
                    lbl_Dep.Text = "None"
                    Session("frm_ScatVarSlbl_Dep") = lbl_Dep.Text
                    Exit Sub
                ElseIf _opt_Independent_0.Checked Then
                    _lbl_Ind_0.Text = "None"
                    Session("Scat_FilledVar")(0) = False
                    Exit Sub
                ElseIf _opt_Independent_1.Checked Then
                    _lbl_Ind_1.Text = "None"
                    Session("Scat_FilledVar")(1) = False

                    'JAM 2018-07-08 Resetting Third Variable
                    Session("frm_ScatVarSlbl_Ind1") = "None"
                    Session("VarNameI")(1) = ""
                    'Session("ThirdDimOk") = False
                    'Session("thirdimsel") = "N"



                    Exit Sub
                ElseIf _opt_Independent_2.Checked Then
                    _lbl_Ind_2.Text = "None"


                    Exit Sub
                ElseIf _opt_Independent_3.Checked Then
                    _lbl_Ind_3.Text = "None"
                    Exit Sub
                ElseIf _opt_Independent_4.Checked Then
                    _lbl_Ind_4.Text = "None"
                    Exit Sub
                ElseIf opt_Control.Checked Then
                    lbl_Cont.Text = "None"
                    Exit Sub

                End If
            End If

        End If


    End Sub

    Private Function SortItems()
        Dim arl As New ArrayList()
        Dim k As Integer = 0

        While k < cmb_VCAT.Items.Count

            arl.Add(cmb_VCAT.Items(k).Value)
            k += 1
        End While

        arl.Sort()
        arl.Insert(0, "All")
        cmb_VCAT.Items.Clear()
        cmb_VCAT.DataSource = arl
        cmb_VCAT.DataBind()
    End Function

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'Public Sub mnuAvailability_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAvailability.Popup
    '    mnuAvailability_Click(eventSender, eventArgs)
    'End Sub
    Public Sub mnuAvailability_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        cmb_VarName_SelectedIndexChanged1()
        'jrs 2005/04/01
        lstVarName.Enabled = True
        lstVarName.Visible = True
        lbl_vcat.Visible = True
        cmb_VCAT.Visible = True
        'jrs 2006/08/05
        If Session("Scat_SE") = "" Then Exit Sub

        'jrs 2025/05/27
        Dim sURL As String = Session("Scat_SE").ToString.Replace("%", "%25")

        popupchoice = "Data Availability" 'mnuAvailability.Caption
        'mtiwebform June 2004 Cross-sectional
        Support.FormManager.StateStore(Me)
        'jrs 2025/05/27 Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & Session("Scat_SE").ToString())
        Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & sURL)
        'end mtiwebform June 2004 Cross-sectional
    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'Public Sub mnuSelect_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuSelect.Popup
    '    mnuSelect_Click(eventSender, eventArgs)
    'End Sub,m
    Public Sub mnuSelect_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'Session("ClickedOnVarName") = True
        Dim Msg As String
        'wmContext.Visible = False

        cmb_VarName_SelectedIndexChanged1()
        popupchoice = "Select" 'mnuSelect.Caption
        'WmContext.Visible = False
        On Error GoTo FileProblem

        '   Pick up Information if the Variable is a Time Series or Cohort
        '   --------------------------------------------------------------

        If Session("Scat_Series") = "Yes" Then 'Series variable and field is year
            Session("Scat_Cohort") = ""
            Session("Scat_YearSel") = CStr(True)
            Session("Scat_CohortSel") = CStr(False)
            Call YearLoad()
        ElseIf Session("Scat_Cohort") = "Yes" Then 'Cohort variable and field is cohort
            Session("Scat_YearSel") = CStr(False)
            Session("Scat_CohortSel") = CStr(True)
            Session("Scat_WaveDone") = CStr(False)
            Session("Scat_SE") = ""
            Call WaveLoad()
        Else
            Session("Scat_YearSel") = CStr(False)
            Session("Scat_CohortSel") = CStr(False)
            Session("Scat_SE") = ""
            Session("Scat_Cohort") = ""
            Call FillData()
        End If

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'JRS 9/16/01 COMPUTATIONS
        If Session("bComputation") And Session("Scat_Series") <> "Yes" And Session("Scat_Cohort") <> "Yes" Then
            'jrs 2006/01/02
            Session("frm_ScatVarSlbl_Dep") = lbl_Dep.Text
            Session("frm_ScatVarSlbl_Ind0") = _lbl_Ind_0.Text
            Session("frm_ScatVarSlbl_Ind1") = _lbl_Ind_1.Text
            Session("frm_ScatVarSlbl_Ind2") = _lbl_Ind_2.Text
            Session("frm_ScatVarSlbl_Ind3") = _lbl_Ind_3.Text
            Session("frm_ScatVarSlbl_Ind4") = _lbl_Ind_4.Text
            Session("frm_ScatVarSlbl_Cont") = lbl_Cont.Text

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")
            'frm_computations.DefInstance.Show() 
            'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Activate' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Activate().html'.
            'frm_computations.DefInstance.Activate() 
            Exit Sub
        Else
            'jrs 2007/08/18
            If Not Session("bComputation") Then
                'jrs 2007/07/30
                If opt_Dependent.Checked Then
                    Session("bCompSVtmp")(1) = False
                ElseIf Session("Scat_IndIndex") < 2 Then
                    Session("bCompSVtmp")(Session("Scat_IndIndex") + 2) = False
                End If
            End If

            Exit Sub
        End If



FileProblem:
        Msg = "Sorry. An error has arisen during an attempt to read " & Session("Scat_VarName")
        Msg = Msg & " from " & Session("Scat_VarTable") & " - the variable or data for it "
        Msg = Msg & "doesn't exist in the file.  Please try another variable."

        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '=============================ScatVarS.Frm==============================
    'UPGRADE_WARNING: Event opt_Control.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub opt_Control_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles opt_Control.CheckedChanged
        If eventSender.Checked Then
            Dim Msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Msg = "Select Control Variable and Data Table (e.g. GDP - Economic):"
            lbl_VarName.Text = Msg

            'jrs 2005/12/19 RAM for Formulas
            ''    cmb_VarName.SetFocus
            ''JRS 9/16/01 COMPUTATIONS
            'If cmdComputations.Visible And lbl_Cont.Text <> "" Then
            '    DeletePrevVar((7))
            'End If
            'jrs 2006/01/02
            Session("frm_ScatVarSopt_Control") = opt_Control.Checked
        End If
    End Sub

    '=============================ScatVarS.Frm==============================
    'UPGRADE_WARNING: Event opt_Dependent.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub opt_Dependent_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles opt_Dependent.CheckedChanged
        If eventSender.Checked Then
            Dim Msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Msg = "Select Dependent Variable and Data Table (e.g. GDP - Economic):"
            lbl_VarName.Text = Msg
            'jrs 2005/12/19 RAM for Formulas
            ''    cmb_VarName.SetFocus
            ''JRS 9/16/01 COMPUTATIONS
            'If cmdComputations.Visible And lbl_Dep.Text <> "" Then
            '    DeletePrevVar((1))
            'End If
            'jrs 2006/01/02
            Session("frm_ScatVarSopt_DependentChecked") = opt_Dependent.Checked
        End If
    End Sub

    '=============================ScatVarS.Frm==============================
    'UPGRADE_WARNING: Event opt_Independent.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub opt_Independent_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Dim index As Short = opt_Independent.IndexOf(eventSender)
            Dim Msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Msg = "Select Independent Variable" & RTrim(Str(index + 1)) & ":"
            lbl_VarName.Text = Msg
            Session("Scat_IndIndex") = index
            '    cmb_VarName.SetFocus
        End If
    End Sub

    '=============================ScatVarS.Frm==============================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ComboBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.KeyPress.html'.
    'Private Sub cmb_YearSelection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) 'Handles cmb_YearSelection.KeyPress
    '    'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    If KeyAscii = 13 Then Call cmb_YearSelection_SelectedIndexChanged(cmb_YearSelection, New System.EventArgs)
    '    If KeyAscii >= 97 And KeyAscii <= 146 Then
    '        'UPGRADE_ISSUE: Assignment not supported: KeyAscii to a non-zero value Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1058"'
    '        KeyAscii = KeyAscii - 32
    '    End If
    '    'JRS 2/5/02
    '    'CONVERSION_ISSUE: Class System.Windows.Forms.Keys from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    If KeyAscii = System.Windows.Forms.Keys.Escape Then
    '        '   Make Combination Box Invisible
    '        '   ------------------------------
    '        cmb_YearSelection.Visible = False
    '        lstDIM1.Visible = False
    '        txtVarSearch.Enabled = True
    '        lstVarName.Enabled = True
    '        lstVarName.Visible = True
    '        lbl_Year.Visible = False
    '        lbl_VarName.Visible = True
    '        lbl_vcat.Visible = True
    '        cmb_VCAT.Visible = True
    '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
    '        cmb_YearSelection.Items.Clear()
    '        lstDIM1.Items.Clear()
    '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
    '        'cmb_YearSelection.Items(cmb_YearSelection.SelectedIndex).Text = ""
    '    End If

    '    If KeyAscii = 0 Then
    '        'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '        eventArgs.Handled = True
    '    End If
    'End Sub
    '=============================ScatVarS.Frm==============================

    Private Sub FillData()
        Dim MsgBox_Renamed As String
        Dim Msg As String
        Dim ICount As Short
        'mtiwebform June 2004 Cross-Sectional - new var declared for password
        Dim DBPassWord As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Re-establish non-adjusted Ind and Dep variables
        '       (Perhaps changed in control)
        '   -----------------------------------------------

        On Error GoTo VariableNotFoundFill
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For ICount = 1 To Session("CCount") 'Re-establish non-adjusted I and D variables
            '        Session("CVarI")(ICount%) = SCVarI(ICount%, 0)
            'UPGRADE_WARNING: Couldn't resolve default property of object SCVarI(ICount, IndIndex). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JAM 2010-01-23 Loading values in CVarISec
            If Session("Scat_IndIndex") <> 1 Then
                Session("CVarI")(ICount) = Session("Scat_SCVarI")(ICount, Session("Scat_IndIndex"))
            Else
                Session("CVarISec")(ICount) = Session("Scat_SCVarI")(ICount, Session("Scat_IndIndex"))

            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SCVarD")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("CVarD")(ICount) = Session("Scat_SCVarD")(ICount)
        Next ICount

        '   Read new data across countries (may be I, D, or C variable or constant)
        '   -----------------------------------------------------------------------

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NumVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        DBPassWord = ""

        If Session("Scat_NumVal") = True Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For ICount = 1 To Session("CCount")
                'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_NewVal")(ICount) = Session("Scat_NVal")
            Next ICount
        Else
            If Session("Scat_SE") <> "" Then 'Time series or cohort data used (SE$ carries year/cohort)
                If CBool(Session("Scat_YearSel")) = True Then
                    '                Dim tdf As TableDef
                    'If GroupName <> "Automobile" Then
                    '    'Set db = OpenDatabase(session("IFsHistSeries_MDB"))
                    '    db = DAODBEngine_definst.OpenDatabase(session("IFsHistSeries_MDB"), False, False, DataPW)
                    'Else
                    '    db = DAODBEngine_definst.OpenDatabase(session("IFsCoVaTrASeries_MDB"))
                    'End If

                    If Session("Scat_GroupNames") <> "Automobile" Then
                        'db = DAODBEngine_definst.OpenDatabase(session("IFsHistSeries_MDB"), False, False, DataPW)
                        db = Session("IFsHistSeries_MDB")
                        DBPassWord = DataPW
                        'SetDataTablefromAccess(db, "SELECT * FROM Series" & VarName, "", valtbl, DataPW)

                    Else

                        'db = DAODBEngine_definst.OpenDatabase(session("IFsCoVaTrASeries_MDB"))
                        db = Session("IFsCoVaTrASeries_MDB")
                        'SetDataTablefromAccess(db, "SELECT * FROM Series" & VarName, "", valtbl)
                    End If
                    '                Set tdf = db.CreateTableDef("NewSeriesCarsHigh", 0, "SeriesCarsHigh", _
                    '";DATABASE=C:\My Documents\CoVaTrA\RUNFILES\IFsCoVaTrASeries.mdb")
                    '                Set tdf = dbs.CreateTableDef("SeriesCarsHigh")
                    '                tdf.Connect = ";DATABASE=C:\My Documents\CoVaTrA\RUNFILES\IFsCoVaTrASeries.mdb"
                    '                tdf.SourceTableName = "SeriesCarsHigh"
                    '                db.TableDefs.Append tdf
                    '                TableName$ = "NewSeriesCarsHigh"

                ElseIf CBool(Session("Scat_CohortSel")) = True Then
                    'db = DAODBEngine_definst.OpenDatabase(session("IFsWVSCohort_MDB"))
                    db = Session("IFsWVSCohort_MDB")
                    Session("Scat_TableName") = Session("Scat_TableName") & Session("Scat_WaveNumb")
                End If
            Else '"Regular" data, rather than time series
                'db = DAODBEngine_definst.OpenDatabase(session("IFs_MDB2Active"))
                db = Session("IFs_MDB2Active")
            End If
            '        Set ValTbl = db.OpenRecordset(TableName$)
            'valtbl = db.OpenRecordset(TableName)
            'valtbl.index = "Country"
            'valtbl.MoveFirst()
            SetDataTablefromAccess(db, "SELECT * FROM [" & Session("Scat_TableName") & "] Order by Country", "", valtbl, DBPassWord)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For ICount = 1 To Session("CCount")
                If Session("Scat_SE") <> "" Then 'There is time series year or cohort
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'Session("Scat_NewVal")(ICount) = valtbl.Fields(SE).Value
                    Session("Scat_NewVal")(ICount) = valtbl.Rows(ICount - 1).Item(Session("Scat_SE"))
                Else 'Normal variable and field is variable name
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("Scat_NewVal")(ICount) = valtbl.Rows(ICount - 1).Item(Session("Scat_VarName"))
                End If
                'valtbl.MoveNext()
                'jrs 2007/08/31
                If Not Session("bComputation") Then
                    'JAM 2010-03-20 Original Values
                    If opt_Dependent.Checked Then
                        Session("OrigDep")(ICount) = Session("Scat_NewVal")(ICount)
                        Session("FullDep")(ICount) = Session("Scat_NewVal")(ICount)

                    Else
                        If (Session("Scat_IndIndex") <= 1) And (opt_Independent(Session("Scat_IndIndex")).Checked = True) Then
                            If Session("Scat_IndIndex") = 0 Then
                                Session("OrigIndFir")(ICount) = Session("Scat_NewVal")(ICount)
                                Session("FullInd")(ICount) = Session("Scat_NewVal")(ICount)
                            Else
                                Session("OrigIndSec")(ICount) = Session("Scat_NewVal")(ICount)
                            End If
                        End If
                    End If
                End If
            Next ICount
            'valtbl.Close()
        End If

        '   Process new values into appropriate I, D, or C running calculation
        '   ------------------------------------------------------------------

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For ICount = 1 To Session("CCount")
            If opt_Independent(Session("Scat_IndIndex")).Checked = True Then 'Working on independent
                If Session("Scat_IndIndex") <> 1 Then   'JAM 2010-01-23
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                    If IsDBNull(Session("Scat_NewVal")(ICount)) Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarI")(ICount) = System.DBNull.Value
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        If Session("Scat_DivOnPrev") = False And Session("Scat_MulOnPrev") = False And Session("Scat_AddOnPrev") = False And Session("Scat_SubOnPrev") = False Then
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Session("CVarI")(ICount) = Session("Scat_NewVal")(ICount)
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        ElseIf Session("Scat_DivOnPrev") = True Then
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            If Session("Scat_NewVal")(ICount) <> 0 Then Session("CVarI")(ICount) = Session("CVarI")(ICount) / Session("Scat_NewVal")(ICount)
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        ElseIf Session("Scat_MulOnPrev") = True Then
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Session("CVarI")(ICount) = Session("CVarI")(ICount) * Session("Scat_NewVal")(ICount)
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        ElseIf Session("Scat_AddOnPrev") = True Then
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Session("CVarI")(ICount) = Session("CVarI")(ICount) + Session("Scat_NewVal")(ICount)
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        ElseIf Session("Scat_SubOnPrev") = True Then
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Session("CVarI")(ICount) = Session("CVarI")(ICount) - Session("Scat_NewVal")(ICount)
                        End If
                    End If
                Else
                    If IsDBNull(Session("Scat_NewVal")(ICount)) Then
                        Session("CVarISec")(ICount) = System.DBNull.Value
                    Else
                        If Session("Scat_DivOnPrev") = False And Session("Scat_MulOnPrev") = False And Session("Scat_AddOnPrev") = False And Session("Scat_SubOnPrev") = False Then
                            Session("CVarISec")(ICount) = Session("Scat_NewVal")(ICount)
                        ElseIf Session("Scat_DivOnPrev") = True Then
                            If Session("Scat_NewVal")(ICount) <> 0 Then Session("CVarISec")(ICount) = Session("CVarISec")(ICount) / Session("Scat_NewVal")(ICount)
                        ElseIf Session("Scat_MulOnPrev") = True Then
                            Session("CVarISec")(ICount) = Session("CVarISec")(ICount) * Session("Scat_NewVal")(ICount)
                        ElseIf Session("Scat_AddOnPrev") = True Then
                            Session("CVarISec")(ICount) = Session("CVarISec")(ICount) + Session("Scat_NewVal")(ICount)
                        ElseIf Session("Scat_SubOnPrev") = True Then
                            Session("CVarISec")(ICount) = Session("CVarISec")(ICount) - Session("Scat_NewVal")(ICount)
                        End If
                    End If
                End If
            ElseIf opt_Dependent.Checked = True Then 'Working on dependent
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                If IsDBNull(Session("Scat_NewVal")(ICount)) Then
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("CVarD")(ICount) = System.DBNull.Value
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If Session("Scat_DivOnPrev") = False And Session("Scat_MulOnPrev") = False And Session("Scat_AddOnPrev") = False And Session("Scat_SubOnPrev") = False Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarD")(ICount) = Session("Scat_NewVal")(ICount)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf Session("Scat_DivOnPrev") = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        If Session("Scat_NewVal")(ICount) <> 0 Then Session("CVarD")(ICount) = Session("CVarD")(ICount) / Session("Scat_NewVal")(ICount)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf Session("Scat_MulOnPrev") = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarD")(ICount) = Session("CVarD")(ICount) * Session("Scat_NewVal")(ICount)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf Session("Scat_AddOnPrev") = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarD")(ICount) = Session("CVarD")(ICount) + Session("Scat_NewVal")(ICount)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf Session("Scat_SubOnPrev") = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarD")(ICount) = Session("CVarD")(ICount) - Session("Scat_NewVal")(ICount)
                    End If
                End If
            ElseIf opt_Control.Checked = True Then 'Working on control
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                If IsDBNull(Session("Scat_NewVal")(ICount)) Then
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("CVarC")(ICount) = System.DBNull.Value
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If Session("Scat_DivOnPrev") = False And Session("Scat_MulOnPrev") = False And Session("Scat_AddOnPrev") = False And Session("Scat_SubOnPrev") = False Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarC")(ICount) = Session("Scat_NewVal")(ICount)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf Session("Scat_DivOnPrev") = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        If Session("Scat_NewVal")(ICount) <> 0 Then Session("CVarC")(ICount) = Session("CVarC")(ICount) / Session("Scat_NewVal")(ICount)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf Session("Scat_MulOnPrev") = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarC")(ICount) = Session("CVarC")(ICount) * Session("Scat_NewVal")(ICount)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf Session("Scat_AddOnPrev") = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarC")(ICount) = Session("CVarC")(ICount) + Session("Scat_NewVal")(ICount)
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ElseIf Session("Scat_SubOnPrev") = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Session("CVarC")(ICount) = Session("CVarC")(ICount) - Session("Scat_NewVal")(ICount)
                    End If
                End If
            End If
        Next ICount

        'JRS 9/16/01 Changes to add Computations
        If Session("bComputation") Then

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For ICount = 1 To Session("CCount")
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_NewVal")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOTComp")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("PLOTComp")(ICount) = Session("Scat_NewVal")(ICount)
            Next ICount

        Else

            FillLocalData()

        End If
        '   Variable Name Recorded; Move to Next Appropriate Option
        '   -------------------------------------------------------

        If opt_Independent(Session("Scat_IndIndex")).Checked = True Then 'Working on independent

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_DivOnPrev") = False And Session("Scat_MulOnPrev") = False And Session("Scat_AddOnPrev") = False And Session("Scat_SubOnPrev") = False Then
                Session("VarNameI")(Session("Scat_IndIndex")) = Session("Scat_VarName") '+ " - " + Session("Scat_GroupNames")$
            Else
                Session("VarNameI")(Session("Scat_IndIndex")) = Session("VarNameI")(Session("Scat_IndIndex")) & Session("Scat_VarName") '+ " - " + GroupName$
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_DivOn") = True Then
                Session("VarNameI")(Session("Scat_IndIndex")) = Session("VarNameI")(Session("Scat_IndIndex")) & "/"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_MulOn") = True Then
                Session("VarNameI")(Session("Scat_IndIndex")) = Session("VarNameI")(Session("Scat_IndIndex")) & "*"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_AddOn") = True Then
                Session("VarNameI")(Session("Scat_IndIndex")) = Session("VarNameI")(Session("Scat_IndIndex")) & "+"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_SubOn") = True Then
                Session("VarNameI")(Session("Scat_IndIndex")) = Session("VarNameI")(Session("Scat_IndIndex")) & "-"
            End If

            lbl_Ind(Session("Scat_IndIndex")).Text = Session("VarNameI")(Session("Scat_IndIndex"))
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JAM 2011-01-26     Keeping Independent values between Scatvars and Scatter Forms
            If Session("Scat_IndIndex") = 0 Then
                _lbl_Ind_0.Text = Session("VarNameI")(0)
                Session("frm_ScatVarSlbl_Ind0") = _lbl_Ind_0.Text
            Else
                If Session("Scat_IndIndex") = 1 Then
                    _lbl_Ind_1.Text = Session("VarNameI")(1)
                    Session("frm_ScatVarSlbl_Ind1") = _lbl_Ind_1.Text
                Else
                    If Session("Scat_IndIndex") = 2 Then
                        _lbl_Ind_2.Text = Session("VarNameI")(2)
                        Session("frm_ScatVarSlbl_Ind2") = _lbl_Ind_2.Text
                    Else
                        If Session("Scat_IndIndex") = 3 Then
                            _lbl_Ind_3.Text = Session("VarNameI")(3)
                            Session("frm_ScatVarSlbl_Ind3") = _lbl_Ind_3.Text
                        Else
                            If Session("Scat_IndIndex") = 4 Then
                                _lbl_Ind_4.Text = Session("VarNameI")(4)
                                Session("frm_ScatVarSlbl_Ind4") = _lbl_Ind_4.Text
                            Else
                                _lbl_Ind_0.Text = Session("VarNameI")(0)
                                Session("frm_ScatVarSlbl_Ind0") = _lbl_Ind_0.Text
                            End If
                        End If
                    End If
                End If
            End If
            ''''''''''''''''''''''''''''''''''''''
            If Session("Scat_DivOn") = False And Session("Scat_MulOn") = False And Session("Scat_AddOn") = False And Session("Scat_SubOn") = False Then 'done with this independent
                opt_Independent(Session("Scat_IndIndex")).Checked = False
                'jrs 2006/01/02
                Session("frm_ScatVarSopt_Independent" & Trim(Session("Scat_IndIndex"))) = False
                Session("frm_ScatVarSopt_DependentChecked") = False
                Session("frm_ScatVarSopt_Independent0") = False
                Session("frm_ScatVarSopt_Independent1") = False
                Session("frm_ScatVarSopt_Independent2") = False
                Session("frm_ScatVarSopt_Independent3") = False
                Session("frm_ScatVarSopt_Independent4") = False
                Session("frm_ScatVarSopt_Control") = False

                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_FilledVar")(Session("Scat_IndIndex")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("Scat_FilledVar")(Session("Scat_IndIndex")) = True
                If Session("Scat_IndIndex") < 4 Then 'Move to another independent, if possible
                    Session("Scat_IndIndex") = Session("Scat_IndIndex") + 1
                    opt_Independent(Session("Scat_IndIndex")).Checked = True
                    'jrs 2006/01/02
                    Session("frm_ScatVarSopt_Independent" & Trim(Session("Scat_IndIndex"))) = True
                Else 'move to dependent when independent all filled
                    opt_Dependent.Checked = True
                    'jrs 2006/01/02
                    Session("frm_ScatVarSopt_DependentChecked") = opt_Dependent.Checked
                End If
                Msg = "Select Dependent Variable and Data Table (e.g. GDP - Economic):"
                lbl_VarName.Text = Msg
            End If

        ElseIf opt_Dependent.Checked = True Then 'Working on dependent
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_DivOnPrev") = False And Session("Scat_MulOnPrev") = False And Session("Scat_AddOnPrev") = False And Session("Scat_SubOnPrev") = False Then
                Session("VarNameD") = Session("Scat_VarName") '+ " - " + GroupName$
            Else
                Session("VarNameD") = Session("VarNameD") & Session("Scat_VarName") '+ " - " + GroupName$
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_DivOn") = True Then
                Session("VarNameD") = Session("VarNameD") & "/"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_MulOn") = True Then
                Session("VarNameD") = Session("VarNameD") & "*"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_AddOn") = True Then
                Session("VarNameD") = Session("VarNameD") & "+"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_SubOn") = True Then
                Session("VarNameD") = Session("VarNameD") & "-"
            End If

            lbl_Dep.Text = Session("VarNameD")
            'jrs 2006/02/22
            Session("frm_ScatVarSlbl_Dep") = lbl_Dep.Text

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_DivOn") = False And Session("Scat_MulOn") = False And Session("Scat_AddOn") = False And Session("Scat_SubOn") = False Then
                opt_Independent(0).Checked = True 'Move from Dependent to 1st independent
                'jrs 2006/01/02
                Session("frm_ScatVarSopt_DependentChecked") = False
                Session("frm_ScatVarSopt_Independent0") = False
                Session("frm_ScatVarSopt_Independent1") = False
                Session("frm_ScatVarSopt_Independent2") = False
                Session("frm_ScatVarSopt_Independent3") = False
                Session("frm_ScatVarSopt_Independent4") = False
                Session("frm_ScatVarSopt_Control") = False

                Session("frm_ScatVarSopt_Independent0") = opt_Independent(0).Checked

                Session("Scat_IndIndex") = 0
                Msg = "Select Independent Variable and Data Table (e.g. GDP - Economic):"
                lbl_VarName.Text = Msg
            End If
        ElseIf opt_Control.Checked = True Then 'Working on control
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_DivOnPrev") = False And Session("Scat_MulOnPrev") = False And Session("Scat_AddOnPrev") = False And Session("Scat_SubOnPrev") = False Then
                Session("VarNameC") = Session("Scat_VarName") '+ " - " + GroupName$
            Else
                Session("VarNameC") = Session("VarNameC") & Session("Scat_VarName") '+ " - " + GroupName$
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_DivOn") = True Then
                Session("VarNameC") = Session("VarNameC") & "/"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_MulOn") = True Then
                Session("VarNameC") = Session("VarNameC") & "*"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_AddOn") = True Then
                Session("VarNameC") = Session("VarNameC") & "+"
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ElseIf Session("Scat_SubOn") = True Then
                Session("VarNameC") = Session("VarNameC") & "-"
            End If

            lbl_Cont.Text = Session("VarNameC")
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("Scat_DivOn") = False And Session("Scat_MulOn") = False And Session("Scat_AddOn") = False And Session("Scat_SubOn") = False Then 'done with control; go to dependent
                opt_Control.Checked = False
                opt_Dependent.Checked = True
                'jrs 2006/01/02
                Session("frm_ScatVarSopt_DependentChecked") = False
                Session("frm_ScatVarSopt_Independent0") = False
                Session("frm_ScatVarSopt_Independent1") = False
                Session("frm_ScatVarSopt_Independent2") = False
                Session("frm_ScatVarSopt_Independent3") = False
                Session("frm_ScatVarSopt_Independent4") = False
                Session("frm_ScatVarSopt_Control") = False

                Session("frm_ScatVarSopt_Control") = opt_Control.Checked
                Session("frm_ScatVarSopt_DependentChecked") = opt_Dependent.Checked

                Msg = "Select Dependent Variable and Data Table (e.g. GDP - Economic):"
                lbl_VarName.Text = Msg
            End If

        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("Scat_DivOn") = True Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("Scat_DivOnPrev") = True
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_DivOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("Scat_DivOnPrev") = False
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("Scat_MulOn") = True Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("Scat_MulOnPrev") = True
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_MulOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("Scat_MulOnPrev") = False
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("Scat_AddOn") = True Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("Scat_AddOnPrev") = True
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_AddOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("Scat_AddOnPrev") = False
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOn"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("Scat_SubOn") = True Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("Scat_SubOnPrev") = True
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("Scat_SubOnPrev"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("Scat_SubOnPrev") = False
        End If

        'JAM 2010-03-07      GList Aggregation


        If opt_Dependent.Checked Then
            Session("OrigAgg")(0) = Session("sAggregation")

        Else
            If (Session("Scat_IndIndex") <= 2) Then ' And (Session("frm_ScatVarSopt_Independent0")) Then
                If Session("Scat_IndIndex") = 1 Then
                    Session("OrigAgg")(1) = Session("sAggregation")

                Else
                    If Session("Scat_IndIndex") = 2 Then
                        Session("OrigAgg")(2) = Session("sAggregation")

                    End If
                End If
            End If
        End If


        Exit Sub

VariableNotFoundFill:
        Msg = "The selected variable could not be found.  Please try another."
        'JRS 01/28/04
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub
    '=============================ScatVarS.Frm==============================
    Private Sub CohortLoad()
        'mtiwebform June 2004 Cross-Sectional

        'Dim f As Object
        Dim f As DataColumn 'Object
        Dim fieldsfound As Short 'Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Make Combination Box for Cohort Identification Visible and Fill
        '   -------------------------------------------------------------
        cmb_YearSelection.Visible = True
        lstDIM1.Visible = True
        cmb_YearSelection.Focus()
        txtVarSearch.Enabled = False
        lstVarName.Enabled = False
        lstVarName.Visible = False
        lbl_Year.Visible = True
        lbl_VarName.Visible = False
        lbl_vcat.Visible = False
        cmb_VCAT.Visible = False
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_YearSelection.Items.Clear()
        lstDIM1.Items.Clear()
        'JRS 09/08/04
        cmb_YearSelection.Items.Add(New ListItem(""))
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cmb_YearSelection.Items(cmb_YearSelection.SelectedIndex).Text = ""

        '   Load element names into Combo Box
        '   ---------------------------------

        'db = DAODBEngine_definst.OpenDatabase(session("IFsWVSCohort_MDB"))
        'valtbl = db.OpenRecordset(VarName & WaveNumb)
        db = Session("IFsWVSCohort_MDB")
        SetDataTablefromAccess(db, "SELECT * FROM " & Session("Scat_VarName") & Session("Scat_WaveNumb"), "", valtbl)
        'For Each f In valtbl.Fields
        For Each f In valtbl.Columns
            'UPGRADE_WARNING: Couldn't resolve default property of object f.Name. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If f.ColumnName <> "Country" And f.ColumnName <> "FIPS_CODE" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object f.Name. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                cmb_YearSelection.Items.Add(New ListItem(f.ColumnName))
                lstDIM1.Items.Add(New ListItem(f.ColumnName))
            End If
        Next f
        'valtbl.Close()
        'db.Close()

    End Sub
    '=============================ScatVarS.Frm==============================
    Private Sub WaveLoad()
        Dim tablenamewave As String
        Dim iloop As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Make Combination Box for Cohort Identification Visible and Fill
        '   -------------------------------------------------------------
        cmb_YearSelection.Visible = True
        lstDIM1.Visible = True
        cmb_YearSelection.Focus()
        txtVarSearch.Enabled = False
        lstVarName.Enabled = False
        lstVarName.Visible = False
        lbl_Year.Visible = True
        lbl_VarName.Visible = False
        lbl_vcat.Visible = False
        cmb_VCAT.Visible = False
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_YearSelection.Items.Clear()
        lstDIM1.Items.Clear()
        'jrs 09/08/04
        cmb_YearSelection.Items.Add(New ListItem(""))
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cmb_YearSelection.Items(cmb_YearSelection.SelectedIndex).Text = ""


        '       Load element names into Combo Box
        '       ---------------------------------
        'db = DAODBEngine_definst.OpenDatabase(session("IFsWVSCohort_MDB"))
        db = Session("IFsWVSCohort_MDB")
        'UPGRADE_WARNING: Couldn't resolve default property of object NWVSWaves. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For iloop = 1 To Convert.ToInt16(NWVSWaves)
            'JRS 01/28/04 On Error GoTo TableNotFound
            On Error GoTo TryNextWave
            'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            tablenamewave = Session("Scat_VarName") & "W" & Trim(Str(iloop))
            'valtbl = db.OpenRecordset(tablenamewave)
            SetDataTablefromAccess(db, "SELECT * FROM " & Session("Scat_VarName") & "W" & Trim(Str(iloop)), "", valtbl)
            'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_YearSelection.Items.Add(New ListItem("W" & Trim(Str(iloop))))
            lstDIM1.Items.Add(New ListItem("W" & Trim(Str(iloop))))
            'valtbl.Close()
TryNextWave:
        Next iloop
        'db.Close()
        Exit Sub


    End Sub

    'jrs 2005/12/19 RAM for Formulas
    'Private Sub DeletePrevVar(ByRef X As Short)
    '    'JRS 9/16/01 COMPUTATIONS
    '    Dim dbV As String 'DAO.Database
    '    'dbV = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "IFSVAR.MDB")
    '    'dbV.Execute("DELETE * FROM [Formulas] WHERE [Code] = " & Str(X))

    '    dbV = Session("PutFdrvRun") & "IFSVAR.MDB"
    '    updateAccessWith1Command(dbV, "DELETE * FROM [Formulas] WHERE [Code] = " & Str(X))
    '    'dbV.Close()
    'End Sub

    Public Sub FillLocalData()
        Dim ICount As Short

        'JAM 2010-03-18
        ReDim Session("SCVarIGlob")(Session("CCount"), 4) 'jrs 2012/07/20
        ReDim Session("SCVarDGlob")(Session("CCount"))


        'JRS 9/16/01 COMPUTATIONS
        For ICount = 1 To Session("CCount")
            If Session("Scat_IndIndex") = 1 Then
                Session("Scat_SCVarI")(ICount, Session("Scat_IndIndex")) = Session("CVarISec")(ICount)
            Else
                Session("Scat_SCVarI")(ICount, Session("Scat_IndIndex")) = Session("CVarI")(ICount)
            End If
            Session("Scat_SCVarD")(ICount) = Session("CVarD")(ICount)

            If Session("IsaGlist") Then

                'jrs 2012/07/20 Session("SCVarIGlob")(ICount, Session("Scat_IndIndex")) = Session("SCVarI")(ICount, Session("Scat_IndIndex"))
                Session("SCVarIGlob")(ICount, Session("Scat_IndIndex")) = Session("Scat_SCVarI")(ICount, Session("Scat_IndIndex"))
                'jrs 2012/07/20 Session("SCVarDGlob")(ICount) = Session("SCVarD")(ICount)
                Session("SCVarDGlob")(ICount) = Session("Scat_SCVarD")(ICount)
            End If

        Next ICount
    End Sub

    Private Sub ResetComputationsFrm()
        'mtiwebform June 2004 Cross-Sectional - commented for computation
        ''JRS 9/25/01 COMPUTATIONS
        ''CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'frm_computations.DefInstance.txtFormula.Text = ""
        ''CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'frm_computations.DefInstance.lblFormulaName.Text = "Formula Name?"
        ''CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'frm_computations.DefInstance.lblDimensionality.Text = "Dimensionality?"
        ''CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'frm_computations.DefInstance.lblUnits.Text = "Units?"
        ''CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'WinFormsToWeb_FocusControl.Focus("txtFormula")
    End Sub

    Private Sub UpdateCurrentDisplay()
        'JRS 9/25/01 COMPUTATIONS
        Select Case True
            Case Session("frm_ScatVarSopt_Independent0")
                Session("Scat_sCurrentDisplay") = Session("frm_ScatVarSlbl_Ind0")
                Session("Scat_iCurrentIdx") = 2
            Case Session("frm_ScatVarSopt_Independent1")
                Session("Scat_sCurrentDisplay") = Session("frm_ScatVarSlbl_Ind1")
                Session("Scat_iCurrentIdx") = 3
            Case Session("frm_ScatVarSopt_Independent2")
                Session("Scat_sCurrentDisplay") = Session("frm_ScatVarSlbl_Ind2")
                Session("Scat_iCurrentIdx") = 4
            Case Session("frm_ScatVarSopt_Independent3")
                Session("Scat_sCurrentDisplay") = Session("frm_ScatVarSlbl_Ind3")
                Session("Scat_iCurrentIdx") = 5
            Case Session("frm_ScatVarSopt_Independent4")
                Session("Scat_sCurrentDisplay") = Session("frm_ScatVarSlbl_Ind4")
                Session("Scat_iCurrentIdx") = 6
                    'Case frm_ScatVarS.DefInstance.opt_Control.Checked
            Case opt_Control.Checked
                Session("Scat_sCurrentDisplay") = Session("frm_ScatVarSlbl_Cont")
                Session("Scat_iCurrentIdx") = 7
            Case Else
                Session("Scat_sCurrentDisplay") = Session("frm_ScatVarSlbl_Dep")
                Session("Scat_iCurrentIdx") = 1
        End Select
    End Sub

    Public ReadOnly Property optIndex() As Object
        Get
            'JRS 9/26/01 COMPUTATIONS, To be able to read this variable
            'outside the form
            'UPGRADE_WARNING: Couldn't resolve default property of object optIndex. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            optIndex = Session("Scat_IndIndex")
        End Get
    End Property

    Sub ItemClickHandlerContext(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Select"
                mnuSelect_Click(sender, e)
            Case "Data Information"
                mnuAvailability_Click(sender, e)
                'Case "Explain"
                'Case "View Equations"
                'Case "Define"
                '    'mnuDefine_Click(sender, e)
        End Select
    End Sub




    'jrs 10/13/04
    Private Sub lstDIM1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDIM1.SelectedIndexChanged
        Dim old As Integer
        If cmb_YearSelection.Items.Count > 0 And lstDIM1.Items.Count > 0 Then
            old = cmb_YearSelection.SelectedIndex
            cmb_YearSelection.SelectedIndex = lstDIM1.SelectedIndex + 1
            If old <> cmb_YearSelection.SelectedIndex Then
                Call cmb_YearSelection_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub

    'jrs 2006/01/02
    Private Sub _opt_Independent_0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _opt_Independent_0.CheckedChanged
        Session("frm_ScatVarSopt_Independent0") = _opt_Independent_0.Checked
        'jrs 2006/02/22
        Session("Scat_IndIndex") = 0
    End Sub

    Private Sub _opt_Independent_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _opt_Independent_1.CheckedChanged
        Session("frm_ScatVarSopt_Independent1") = _opt_Independent_1.Checked
        'jrs 2006/02/22
        Session("Scat_IndIndex") = 1
    End Sub

    Private Sub _opt_Independent_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _opt_Independent_2.CheckedChanged
        Session("frm_ScatVarSopt_Independent2") = _opt_Independent_2.Checked
        'jrs 2006/02/22
        Session("Scat_IndIndex") = 2
    End Sub

    Private Sub _opt_Independent_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _opt_Independent_3.CheckedChanged
        Session("frm_ScatVarSopt_Independent3") = _opt_Independent_3.Checked
        'jrs 2006/02/22
        Session("Scat_IndIndex") = 3
    End Sub

    Private Sub _opt_Independent_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _opt_Independent_4.CheckedChanged
        Session("frm_ScatVarSopt_Independent4") = _opt_Independent_4.Checked
        'jrs 2006/02/22
        Session("Scat_IndIndex") = 4
    End Sub

    Protected Sub btnInvs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInvs.Click

        'txtVarSearch.Text = Request.QueryString("selCommand").ToString()
        'JAM 2011-09-28   TFR exception isn't necessary for blank spaces managing
        'If txtVarSearch.Text = "TFR - Total fertility rate: children per woman - Number" Then

        '    txtVarSearch.Text = "TFR - Total fertility rate:  children per woman - Number"
        'End If
        mnuSelect_Click(Nothing, Nothing)


    End Sub
    'JAM 2011-11-19 EnterKey button only to override Enter Key for txtVarSearch field
    Protected Sub btnEnterKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInvs.Click

    End Sub
    'JAM 2010-01-23 Loading GDP Variables
    Private Sub Load_Gdp()
        Dim i As Short


        db = Session("IFsHistSeries_MDB")
        SetDataTablefromAccess(db, "SELECT * FROM [SeriesGDP2000] Order by Country", "", valtbl, DataPW)

        ' Session("GdpTbl") = "SeriesGDP2000"

        'valtbl = Session("GdpTbl") 'Open GDP table
        'valtbl = Session("DataDictVars")
        For i = 0 To valtbl.Rows.Count - 1

            Session("CVarIGdp")(i + 1) = valtbl.Rows(i).Item("MostRecent")
            Session("IGdpOrig")(i + 1) = valtbl.Rows(i).Item("MostRecent")
            '  Session("Scat_GroupNames") = valtbl.Rows(Rcnt).Item("Group")

        Next i

    End Sub
    'JAM 2010-01-23 Loading Population Variables
    Private Sub Load_Population()
        Dim i As Short
        Dim a As Double


        db = Session("IFsHistSeries_MDB")
        SetDataTablefromAccess(db, "SELECT * FROM [SeriesPopulation] Order by Country", "", valtbl, DataPW)

        ' Session("GdpTbl") = "SeriesPopulation"

        'valtbl = Session("GdpTbl") 'Open GDP table
        'valtbl = Session("DataDictVars")
        For i = 0 To valtbl.Rows.Count - 1

            Session("CVarIPop")(i + 1) = valtbl.Rows(i).Item("MostRecent")
            Session("IPopOrig")(i + 1) = valtbl.Rows(i).Item("MostRecent")
            '  Session("Scat_GroupNames") = valtbl.Rows(Rcnt).Item("Group")
            'a = Session("CVarIPop")(i)
        Next i

    End Sub

    'jrs 2011/07/06
    Private Function bIncludedByOthrFilter(ByVal sPreprocessor As String, ByVal sYears As String) As Boolean
        Dim bInclude As Boolean, iCountYears As Integer
        Dim xYears As Object, pos As Integer
        Dim yr1 As Integer, yr2 As Integer, i As Integer
        'jrs 2011/07/06
        'Other filters
        bInclude = False
        If Session("ScatVarFiltName") = "ALL" Then
            bInclude = True
        ElseIf (Session("ScatVarFiltName") = "Used for Initialization" And sPreprocessor = "True") Then
            bInclude = True
        ElseIf (Session("ScatVarFiltName") = "At least " & CStr(Session("iFilterDataPoints")) & " Data Points") Then
            'Count years
            xYears = Split(sYears, ",")
            iCountYears = UBound(xYears) + 1
            If iCountYears >= Session("iFilterDataPoints") Then
                bInclude = True
            Else
                For i = 0 To UBound(xYears)
                    'JAM 2013-11-20
                    If IsNumeric(xYears(i)) Then
                        pos = InStr(xYears(i), "-")
                    End If
                    If pos > 0 Then
                        'JAM 2013-11-20
                        If IsNumeric(xYears(i)) Then
                            yr1 = CInt(Mid(xYears(i), 1, pos - 1))
                            'jrs 2021/03/12
                            If IsNumeric(Mid(xYears(i), pos + 1)) Then
                                yr2 = CInt(Mid(xYears(i), pos + 1))
                            End If
                        End If
                        iCountYears = iCountYears + yr2 - yr1
                        If iCountYears >= Session("iFilterDataPoints") Then
                            bInclude = True
                            Exit For
                        End If
                    End If
                Next i
            End If
        ElseIf (Session("ScatVarFiltName") = "At least through year " & CStr(Session("iFilterYear"))) Then
            'Find last year
            xYears = Split(sYears, ",")
            i = UBound(xYears)
            If i > -1 Then
                'JAM 2013-11-20
                If IsNumeric(xYears(i)) Then
                    pos = InStr(xYears(i), "-")
                End If
                If pos > 0 Then
                    'JAM 2013-11-20
                    If IsNumeric(xYears(i)) Then
                        yr2 = CInt(Mid(xYears(i), pos + 1))
                    End If
                Else
                    If IsNumeric(xYears(i)) Then
                        yr2 = CInt(xYears(i))
                    End If
                End If
                If yr2 >= Session("iFilterYear") Then
                    bInclude = True
                End If
            End If
        End If 'Other Filters

        bIncludedByOthrFilter = bInclude
    End Function

    'jrs 2011/07/06
    Private Sub LoadFilter()
        cmb_OthFilt.Items.Clear()
        cmb_OthFilt.Items.Add("Used for Initialization")
        cmb_OthFilt.Items.Add("At least " & CStr(Session("iFilterDataPoints")) & " Data Points")
        cmb_OthFilt.Items.Add("At least through year " & CStr(Session("iFilterYear")))
        cmb_OthFilt.Items.Add("ALL")
        cmb_OthFilt.SelectedIndex = 3
    End Sub

    'Protected Sub cmb_OthFilt_SelectedIndexChangedBK(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_OthFilt.SelectedIndexChanged
    '    Dim AddName As String
    '    Dim Definition As String
    '    Dim Series As String
    '    Dim GroupSelect As String
    '    Dim Rcnt As Short
    '    'jrs 2011/07/06
    '    Dim sPreprocessor As String, sYears As String, bInclude As Boolean

    '    If cmb_OthFilt.Items.Count > 0 Then
    '        Session("ScatVarFiltName") = cmb_OthFilt.SelectedValue
    '        Session("ScatVarFilt") = cmb_OthFilt.SelectedIndex
    '    End If

    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '    '
    '    '   Reload Variable Name Combo Box Based on Group Selected by User
    '    '   --------------------------------------------------------------
    '    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

    '    lstVarName.Items.Clear()

    '    AddName = ""
    '    dataString = ""

    '    If Session("ScatVarVCat") > 0 Then

    '        GroupSelect = cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text
    '    Else
    '        GroupSelect = "All"
    '    End If

    '    valtbl = Session("DataDictVars")
    '    Dim sUnits As String
    '    Dim GroupNames As String
    '    Dim VarName As String
    '    Dim Groups As Array
    '    Dim pos1 As Integer
    '    'jrs 2012/02/28
    '    Dim sCoVaTra As String
    '    For Rcnt = 0 To valtbl.Rows.Count - 1
    '        'Do
    '        'JRSVBNET Session("Scat_TableName") = valtbl.Rows(Rcnt).Item("Table")

    '        GroupNames = Trim(valtbl.Rows(Rcnt).Item("Group"))
    '        'jrs 2012/02/28
    '        sCoVaTra = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("CoVaTra")), "No", valtbl.Rows(Rcnt).Item("CoVaTra"))

    '        If UCase(GroupSelect) = UCase("All") Then
    '            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(sCoVaTra) = "NO" Then 'jrs 2012/02/28 Session("Scat_GroupNames") <> "Automobile" Then
    '                VarName = valtbl.Rows(Rcnt).Item("Variable")
    '                Series = valtbl.Rows(Rcnt).Item("Series")
    '                'Definition = valtbl.Rows(Rcnt).Item("Definition")
    '                'JAM 2011-09-25 Replacing two and three blank spaces
    '                Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

    '                'jrs 2011/07/08
    '                sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
    '                sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))
    '                'JRS 10/14/03
    '                If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
    '                    sUnits = ""
    '                Else
    '                    sUnits = valtbl.Rows(Rcnt).Item("Units")
    '                End If
    '                'jrs 2011/07/06
    '                bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
    '                If bInclude Then
    '                    If GroupNames = GroupSelect Or Session("ScatVarVCat") < 1 Then
    '                        AddName = VarName & " - " & Definition & " - " & sUnits
    '                        ' AddName = AddName.Trim()
    '                        dataString = dataString & AddName & "|"
    '                        lstVarName.Items.Add(New ListItem(AddName))
    '                    End If
    '                End If
    '            End If
    '        Else
    '            Groups = Split(GroupNames, ",")
    '            pos1 = 0
    '            While pos1 < Groups.Length
    '                GroupNames = Trim(Groups(pos1))
    '                If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(sCoVaTra) = "NO" Then 'jrs 2012/02/28 Session("Scat_GroupNames") <> "Automobile" Then
    '                    VarName = valtbl.Rows(Rcnt).Item("Variable")
    '                    Series = valtbl.Rows(Rcnt).Item("Series")
    '                    'Definition = valtbl.Rows(Rcnt).Item("Definition")
    '                    'JAM 2011-09-25 Replacing two and three blank spaces
    '                    Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

    '                    'jrs 2011/07/08
    '                    sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
    '                    sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))
    '                    'JRS 10/14/03
    '                    If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
    '                        sUnits = ""
    '                    Else
    '                        sUnits = valtbl.Rows(Rcnt).Item("Units")
    '                    End If
    '                    'jrs 2011/07/06
    '                    bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
    '                    If bInclude Then
    '                        If GroupNames = GroupSelect Or Session("ScatVarVCat") < 1 Then
    '                            AddName = VarName & " - " & Definition & " - " & sUnits
    '                            'AddName = AddName.Trim()
    '                            dataString = dataString & AddName & "|"
    '                            lstVarName.Items.Add(New ListItem(AddName))
    '                        End If
    '                    End If
    '                End If
    '                pos1 += 1
    '            End While
    '            'valtbl.MoveNext()
    '            'If valtbl.EOF = True Then Exit Do
    '        End If         'Loop
    '    Next Rcnt
    '    Session("DataString") = dataString
    '    'valtbl.Close()
    '    'db.Close()


    'End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        'jrs 2012/01/19
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScatVarSearch.aspx")
    End Sub

    'jrs 2012/01/19
    Public Sub LoadSearch(ByVal sVar As String)
        txtVarSearch.Text = sVar
        mnuSelect_Click(Me, New EventArgs)
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''JAM 2013-07-23 Building filters
    ''' </summary>
    ''' <remarks></remarks>

    Public Sub PackJScritpBuilder()
        Dim sJscript As String

        Dim AddName As String
        Dim Definition As String
        Dim Series As String
        Dim GroupSelect As String
        Dim Rcnt As Short
        'jrs 2011/07/06
        Dim sPreprocessor As String, sYears As String, bInclude As Boolean

        If cmb_VCAT.Items.Count > 0 Then
            Session("ScatVarVCat") = cmb_VCAT.SelectedIndex
        End If

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '
        '   Reload Variable Name Combo Box Based on Group Selected by User
        '   --------------------------------------------------------------
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

        lstVarName.Items.Clear()

        AddName = ""
        dataString = ""

        If Session("ScatVarVCat") > 0 Then

            GroupSelect = cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text
        Else
            GroupSelect = "All"
        End If

        valtbl = Session("DataDictVars")
        Dim sUnits As String
        Dim GroupNames As String
        Dim VarName As String
        Dim Groups As Array
        Dim pos1 As Integer
        For Rcnt = 0 To valtbl.Rows.Count - 1
            'Do
            'JRSVBNET Session("Scat_TableName") = valtbl.Rows(Rcnt).Item("Table")

            GroupNames = Trim(valtbl.Rows(Rcnt).Item("Group"))
            If UCase(GroupSelect) = UCase("All") Then
                If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or Session("Scat_GroupNames") <> "Automobile" Then
                    VarName = valtbl.Rows(Rcnt).Item("Variable")
                    Series = valtbl.Rows(Rcnt).Item("Series")
                    'Definition = valtbl.Rows(Rcnt).Item("Definition")
                    'JAM 2011-09-25 Replacing two and three blank spaces
                    Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

                    'jrs 2011/07/08
                    sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
                    sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))
                    'JRS 10/14/03
                    If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
                        sUnits = ""
                    Else
                        sUnits = valtbl.Rows(Rcnt).Item("Units")
                    End If
                    'jrs 2011/07/06
                    bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
                    If bInclude Then
                        If GroupNames = GroupSelect Or Session("ScatVarVCat") < 1 Then
                            AddName = VarName & " - " & Definition & " - " & sUnits
                            ' AddName = AddName.Trim()
                            dataString = dataString + AddName + "|"
                            lstVarName.Items.Add(New ListItem(AddName))
                        End If
                    End If
                End If
            Else
                Groups = Split(GroupNames, ",")
                pos1 = 0
                While pos1 < Groups.Length
                    GroupNames = Trim(Groups(pos1))
                    If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or Session("Scat_GroupNames") <> "Automobile" Then
                        VarName = valtbl.Rows(Rcnt).Item("Variable")
                        Series = valtbl.Rows(Rcnt).Item("Series")
                        'Definition = valtbl.Rows(Rcnt).Item("Definition")
                        'JAM 2011-09-25 Replacing two and three blank spaces
                        Definition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

                        'jrs 2011/07/08
                        sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
                        sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))
                        'JRS 10/14/03
                        If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
                            sUnits = ""
                        Else
                            sUnits = valtbl.Rows(Rcnt).Item("Units")
                        End If
                        'jrs 2011/07/06
                        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
                        If bInclude Then
                            If GroupNames = GroupSelect Or Session("ScatVarVCat") < 1 Then
                                AddName = VarName & " - " & Definition & " - " & sUnits
                                'AddName = AddName.Trim()
                                dataString = dataString & AddName & "|"
                                lstVarName.Items.Add(New ListItem(AddName))
                            End If
                        End If
                    End If
                    pos1 += 1
                End While
                'valtbl.MoveNext()
                'If valtbl.EOF = True Then Exit Do
            End If         'Loop
        Next Rcnt
        Session("DataString") = dataString
        'valtbl.Close()
        'db.Close()
    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '   JAM 2013-07-24 Based on CreateJSforFlexDisplays developed by Jose
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '  Public Sub CreateJSforScatVars(ByVal ActiveBase As String, ByVal PutFdrvRun As String, ByRef CategoryIds As Collection)
    Public Sub CreateJSforScatVars()
        'Dim rsL As DataTable
        'Dim sql As String

        '       JAM 2014-06-12 Commented due centralization code in Function2
        ''Dim Rcnt As Short
        ''Dim Varname As String
        ''Dim VarDefinition As String
        ''Dim sUnits As String
        ''Dim Groupnames As String
        ''Dim divGroups As Array
        ''Dim idivGroups As Short
        ''Dim oneGroup As String
        Dim sJscript As String
        ''Dim FullName As String
        ''Dim sCoVaTra As String
        ''Dim sPreprocessor As String, sYears As String, bInclude As Boolean
        Dim vFilter As String




        ' ''JAM 2013-08-25 For other filter recovering
        vFilter = Session("ScatVarFiltName")

        'JAM 2014-06-12
        GenerateVarsforScatvars(Session("ScatVarFiltName"), Session("PutFdrvRun"), Session("iFilterYear"), Session("iFilterDataPoints"), Session("Scat_GroupNames"))

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '       JAM 2014-06-12 Commented due centralization code in Function2
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ''db = Session("PutFdrvRun") & "DataDict.MDB"
        ''SetDataTablefromAccess(db, "SELECT * FROM datadict order by Variable", "", valtbl)
        ''sJscript = "   var VarList = [];" & vbCr
        ''sJscript = sJscript & "   var FullGroups = [];" & vbCr
        ''sJscript = sJscript & "   var SingleGroup = [];" & vbCr
        ''sJscript = sJscript & "   var NumGroups = [];" & vbCr
        ''sJscript = sJscript & "   var NumVars = [];" & vbCr
        ''sJscript = sJscript & "   var jbInclude = [];" & vbCr
        ''sJscript = sJscript & "   var jbIncludeInit = [];" & vbCr
        ''sJscript = sJscript & "   var jbIncludeNPoint = [];" & vbCr
        ''sJscript = sJscript & "   var jbIncludeTroughYear = [];" & vbCr
        ''sJscript = sJscript & "   var jModelLoad;" & vbCr
        ''sJscript = sJscript & "   var jScat_GroupNames;" & vbCr
        ''sJscript = sJscript & "   var jFilterYear;" & vbCr
        ''sJscript = sJscript & "   var jFilterPoints;" & vbCr


        ''sJscript = sJscript & "  jModelLoad = """ & UCase(ModelLoad) & """;" & vbCr
        ''sJscript = sJscript & "  jFilterYear = """ & CStr(Session("iFilterYear")) & """;" & vbCr
        ''sJscript = sJscript & "  jFilterPoints = """ & CStr(Session("iFilterDataPoints")) & """;" & vbCr


        ''If Not (Session("Scat_GroupNames") Is Nothing) Then
        ''    sJscript = sJscript & "  jScat_GroupNames = """ & Session("Scat_GroupNames") & """;" & vbCr
        ''Else
        ''    sJscript = sJscript & "  jScat_GroupNames = """"; " & vbCr
        ''End If

        ''For Rcnt = 0 To valtbl.Rows.Count - 1
        ''    'JAM 2013-08-20 Adding CoVatra filter
        ''    sCoVaTra = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("CoVaTra")), "No", valtbl.Rows(Rcnt).Item("CoVaTra"))

        ''    If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(sCoVaTra) = "NO" Then
        ''        Varname = valtbl.Rows(Rcnt).Item("Variable")
        ''        'JAM 2013-09-10
        ''        'VarDefinition = valtbl.Rows(Rcnt).Item("Definition")
        ''        VarDefinition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

        ''        'JAM 2013-08-10 Remove new lines
        ''        VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")

        ''        Groupnames = valtbl.Rows(Rcnt).Item("Group")
        ''        If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
        ''            sUnits = ""
        ''        Else
        ''            sUnits = valtbl.Rows(Rcnt).Item("Units")
        ''        End If

        ''        'JAM 2013-08-20
        ''        sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
        ''        sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))

        ''        'JAM 2013-08-25
        ''        Session("ScatVarFiltName") = "At least through year " & CStr(Session("iFilterYear"))
        ''        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
        ''        sJscript = sJscript & "  jbIncludeTroughYear[" & Rcnt.ToString & "]= """ & bInclude.ToString & """; " & vbCr


        ''        Session("ScatVarFiltName") = "At least " & CStr(Session("iFilterDataPoints")) & " Data Points"
        ''        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
        ''        sJscript = sJscript & "  jbIncludeNPoint[" & Rcnt.ToString & "]= """ & bInclude.ToString & """; " & vbCr

        ''        Session("ScatVarFiltName") = "Used for Initialization"
        ''        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
        ''        sJscript = sJscript & "  jbIncludeInit[" & Rcnt.ToString & "]= """ & bInclude.ToString & """; " & vbCr


        ''        Session("ScatVarFiltName") = "ALL"
        ''        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
        ''        sJscript = sJscript & "  jbInclude[" & Rcnt.ToString & "]= """ & bInclude.ToString & """ ;" & vbCr


        ''        If bInclude Then

        ''            FullName = Varname & " - " & VarDefinition & " - " & sUnits

        ''            sJscript = sJscript & "  VarList[" & Rcnt.ToString & "] = new Array(""" & FullName & """);" & vbCr

        ''            divGroups = Split(Groupnames, ",")

        ''            idivGroups = 0
        ''            sJscript = sJscript & "  FullGroups[" & Rcnt.ToString & "]= new Array("""
        ''            While idivGroups <= UBound(divGroups)
        ''                oneGroup = Trim(divGroups(idivGroups))
        ''                idivGroups = idivGroups + 1

        ''                If idivGroups = 1 Then
        ''                    sJscript = sJscript & oneGroup & """"

        ''                Else
        ''                    sJscript = sJscript & "," & """" & oneGroup & """"
        ''                End If

        ''                'sJscript = sJscript & "    SingleGroup[" & idivGroups.ToString & "] = new Array(""" & oneGroup & """);" & vbCr

        ''            End While
        ''            'JAM 2013-08-18

        ''            sJscript = sJscript & ");" & vbCr


        ''            sJscript = sJscript & "  NumGroups[" & Rcnt.ToString & "] = " & idivGroups.ToString & ";" & vbCr

        ''            'JAM 2013-07-25 Multidimensional arrays for group
        ''            'sJscript = sJscript & "    FullGroups[" & Rcnt.ToString & "] =SingleGroup;" & vbCr

        ''            'JAM 2013-08-18
        ''        End If
        ''    End If
        ''Next Rcnt

        ''sJscript = sJscript & "     NumVars = " & valtbl.Rows.Count.ToString & ";" & vbCr


        ''Dim intfile As Short, sFileName As String
        ''intfile = FreeFile()
        ''sFileName = PutFdrv & "IFsWeb\Scripts\"
        ' ''If UCase(ActiveBase) = "IFSBASEHIST.RUN" Then
        ' ''    'write to historic file
        ' ''    sFileName = sFileName & "FlexDisplaysVarsH.js"
        ' ''Else
        ' ''    'write to forecast file
        ''sFileName = sFileName & "ScatVars_Vars.js"
        ' '' End If

        ''FileOpen(intfile, sFileName, OpenMode.Output)
        ''PrintLine(intfile, sJscript)
        ''FileClose(intfile)



        'JAM 2013-08-25 For other filter recovering
        Session("ScatVarFiltName") = vFilter


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'JAM 2013-07-29
        sJscript = "   loadjsfile(""Scripts/ScatVars_Vars.js"");" & vbCr
        cmb_VCAT.Attributes.Add("onchange", "cmb_VCAT_click();")

    End Sub



    Protected Sub cmdGeneratejs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGeneratejs.Click
        CreateJSforScatVars()
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM  2014-02-22 Loading all years for chosen variable
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '=============================ScatVarS.Frm==============================
    Private Sub LoadAllYearsArray(ByVal varNamefull As String, ByVal NumVar As Integer, ByVal MaxInd As Integer)


        On Error GoTo FileNotFound

        Dim vfield, strSQL As String
        Dim i, j, k As Integer


        Dim test As Double


        db = Session("IFsHistSeries_MDB")
        SetDataTablefromAccess(db, "SELECT * FROM [Series" & varNamefull & "] ORDER BY Country", "", valtbl, DataPW)


        'JAM 2014-02-22 Dimmensions for Data array
        If NumVar = 0 Then


            ' valtbl.MoveFirst()

            ReDim Session("ArrayAllVarYears")(valtbl.Rows.Count, valtbl.Columns.Count, MaxInd + 1) 'Because there is an extra data for the dependent variable
            ReDim AllYear(valtbl.Columns.Count)
            ReDim AllVarName(valtbl.Rows.Count)
        End If





        'JAM 2014-02-10
        If NumVar = 0 Then
            AllNumYears = 0
            'JAM 2014-01-25 Dependent Variable
            i = 0
            For Each f In valtbl.Columns
                vfield = f.ColumnName
                If (f.ColumnName <> "Country" And f.ColumnName <> "FIPS_CODE" _
                    And f.ColumnName <> "FIPS_Code") And (IsNumeric(f.ColumnName)) Then
                    AllYear(i - 2) = vfield
                End If
                'valtbl.MoveFirst()
                For j = 0 To valtbl.Rows.Count - 1
                    If f.ColumnName = "Country" Then
                        AllVarName(j) = valtbl.Rows(j).Item(vfield)


                    Else
                        If (f.ColumnName <> "Country" And f.ColumnName <> "FIPS_CODE" _
                               And f.ColumnName <> "FIPS_Code") And (IsNumeric(f.ColumnName)) Then
                            If Not IsDBNull(valtbl.Rows(j).Item(vfield)) Then

                                Session("ArrayAllVarYears")(j, i - 2, 0) = valtbl.Rows(j).Item(vfield)

                                test = Session("ArrayAllVarYears")(j, i - 2, 0)
                            End If
                        End If
                    End If
                    'valtbl.Rows.MoveNext()
                Next j
                i = i + 1
            Next f
            Totx = valtbl.Rows.Count
            Toty = i - 3 'Because the index has an extra value
            AllNumYears = Toty
            valtbl.Clear()

            ReDim Session("ValidData")(Totx * Toty, 2 + MaxInd)
            'jrs 2020/09/16
            ReDim Session("TransformedData")(Totx * Toty, 2 + MaxInd)

        Else    'JAM 2014-02-22 It's independent variable


            SetDataTablefromAccess(db, "SELECT * FROM [Series" + varNamefull + "] ORDER BY Country", "", valtbl, DataPW)
            i = 0
            For Each f In valtbl.Columns
                vfield = f.ColumnName
                'valtbl.MoveFirst()
                If IsNumeric(f.ColumnName) Then
                    'jrs 2014/02/28
                    i = IsValidYear(vfield)
                    If i > -1 Then 'jrs 2014/02/28 If IsValidYear(vfield) Then 'jrs 2014/02/27 AllYear(i - 2)
                        For j = 0 To valtbl.Rows.Count - 1
                            If Not IsDBNull(valtbl.Rows(j).Item(vfield)) Then
                                'jrs 2014/02/28 ArrayAllVarYears(j, i - 2, NumVar) = valtbl.Fields(vfield) 'JAM 2014-01-23 Due to first two fields
                                Session("ArrayAllVarYears")(j, i, NumVar) = valtbl.Rows(j).Item(vfield)  'JAM 2014-01-23 Due to first two fields
                                test = Session("ArrayAllVarYears")(j, i, NumVar)
                            End If
                            'valtbl.MoveNext()
                        Next j
                    End If
                End If
                i = i + 1
            Next f
            valtbl.Clear()
            ' db.close

        End If







        Exit Sub

FileNotFound:
        'Msg$ = "Data not found for chosen variable.  Please try a different variable."
        'MsgBox(Msg$)
        'Screen.MousePointer = 0
        Exit Sub


    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    'jrs 2014/02/27 Loading all years for chosen variable when using formulas
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '=============================ScatVarS.Frm==============================
    Private Sub LoadAllYearsArrayComp(ByVal NumVar As Integer, ByVal MaxInd As Integer)


        On Error GoTo FileNotFound

        Dim varNamefull As String
        Dim vfield As String
        Dim i, j, k As Integer


        db = Session("IFsHistSeries_MDB")


        ''sOriginForm = "Global"
        Dim ff As Formula
        ff = Session("aFormulas")(NumVar + 1)
        ''Dim frm_comp As New frm_computations
        ''Load(frm_comp)
        ''frm_comp.bCompVisible = False
        ''frm_comp.txtFormula = ff.Formula
        ''frm_comp.lblFormulaName = ff.Name
        ''frm_comp.lblDimensionality = ff.Dimension
        ''frm_comp.lblUnits = ff.Unit
        ''frm_comp.iOffsetPosition = 0
        ''frm_comp.LoadFormula(ff)
        ''frm_comp.RedimData()
        ''frm_comp.RefreshForm()
        Dim xData(ff.disVar.disSize) As Object

        Dim T As Integer, pos As Integer
        For T = 1 To ff.disVar.disSize
            varNamefull = ff.disVar.displayDet(T)
            pos = InStr(varNamefull, "(")
            If pos > 0 Then
                varNamefull = Mid(varNamefull, 1, pos - 1)
            End If

            SetDataTablefromAccess(db, "SELECT * FROM [Series" & varNamefull & "] ORDER BY Country", "", valtbl, DataPW)

            If T = 1 Then
                ReDim Session("ArrayAllVarYearsComp")(valtbl.Rows.Count, valtbl.Columns.Count, ff.disVar.disSize)
            End If

            If NumVar = 0 Then
                If T = 1 Then
                    ReDim Session("ArrayAllVarYears")(valtbl.Rows.Count, valtbl.Columns.Count, MaxInd + 1) 'Because there is an extra data for the dependent variable
                    ReDim AllYear(valtbl.Columns.Count)
                    ReDim AllVarName(valtbl.Rows.Count)
                    AllNumYears = 0
                End If

                i = 0
                For Each f In valtbl.Columns
                    vfield = f.ColumnName
                    If IsNumeric(vfield) And T = 1 Then 'do it for the first element of the formula
                        AllYear(i - 2) = vfield
                    End If
                    For j = 0 To valtbl.Rows.Count - 1
                        If f.ColumnName = "Country" Then
                            If T = 1 Then
                                AllVarName(j) = valtbl.Rows(j).Item(vfield)
                            End If
                        Else
                            If (f.ColumnName <> "Country" And f.ColumnName <> "FIPS_CODE" _
                                   And f.ColumnName <> "FIPS_Code") And (IsNumeric(f.ColumnName)) Then
                                If Not IsDBNull(valtbl.Rows(j).Item(vfield)) Then
                                    If T = 1 Then
                                        Session("ArrayAllVarYearsComp")(j, i - 2, T) = valtbl.Rows(j).Item(vfield)
                                    Else
                                        i = IsValidYear(vfield)
                                        If i > -1 Then
                                            Session("ArrayAllVarYearsComp")(j, i, T) = valtbl.Rows(j).Item(vfield)
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    Next j
                    i = i + 1
                Next f
                If T = 1 Then
                    Totx = valtbl.Rows.Count
                    Toty = i - 3 'Because the index has an extra value
                    AllNumYears = Toty
                    ReDim Session("ValidData")(Totx * Toty, 2 + MaxInd)
                    'jrs 2020/09/16
                    ReDim Session("TransformedData")(Totx * Toty, 2 + MaxInd)
                End If
                ''valtbl.Close()

            Else    'JAM 2014-02-10 It's independent variable

                SetDataTablefromAccess(db, "SELECT * FROM [Series" + varNamefull + "] ORDER BY Country", "", valtbl, DataPW)


                i = 0
                For Each f In valtbl.Columns
                    vfield = f.ColumnName
                    If IsNumeric(vfield) Then
                        i = IsValidYear(vfield)
                        If i > -1 Then
                            For j = 0 To valtbl.Rows.Count - 1
                                If Not IsDBNull(valtbl.Rows(j).Item(vfield)) Then
                                    Session("ArrayAllVarYearsComp")(j, i, T) = valtbl.Rows(j).Item(vfield)
                                End If
                            Next j
                        End If
                    End If

                    i = i + 1
                Next f
                '' valtbl.Close()
            End If
        Next T 'next element of the formula
        ''db.Close()

        'For each country pass data and compute formula
        For j = 1 To Totx
            'For each term in the formula
            For T = 1 To ff.disVar.disSize
                Dim xPLOTComp() As Object
                ReDim xPLOTComp(MaxYears)
                'Load each year
                For i = 0 To Toty - 1
                    xPLOTComp(i + 1) = Session("ArrayAllVarYearsComp")(j, i, T)
                Next i
                'Pass it to the formula
                xData(T) = xPLOTComp
            Next T

            'Compute formula value
            ''frm_comp.cmdExit_Click()
            'Reload
            ''frm_comp.LoadFormula(ff)
            ComputeFormulas(ff, Toty, AllYear(0), xData, 0, Session("PLOT"),
                False, Session("aFormulas"), Me, 0) 'jrs 2008/09/12 added iOffsetposition

            ff = Session("aFormulas")(NumVar + 1)

            'Move result to target arrays
            For i = 0 To Toty - 1
                Session("ArrayAllVarYears")(j, i, NumVar) = Session("PLOT")(i + 1, 0)
            Next i
        Next j

        'sOriginForm = ""

        Exit Sub

FileNotFound:
        'Msg$ = "Data not found for chosen variable.  Please try a different variable."
        'MsgBox(Msg$)
        'Screen.MousePointer = 0
        'sOriginForm = ""

        Exit Sub


    End Sub


    'JAM 2014-02-15 Validating all arrays
    Private Sub ValidAllYearsArray(ByVal NumIndVar As Integer)
        On Error GoTo ValidError
        Dim validind As Boolean
        Dim i, j, k, m As Integer
        Dim test, testy As Integer

        k = 0
        For i = 0 To Totx - 1
            For j = 0 To Toty - 1
                validind = False
                For l = 1 To NumIndVar
                    If IsNumeric(Session("ArrayAllVarYears")(i, j, l)) Then
                        validind = True
                    Else
                        validind = False
                    End If
                Next l
                If IsNumeric(Session("ArrayAllVarYears")(i, j, 0)) And validind Then 'JAM 2014-03-11 Adding Index validation
                    'test = j
                    'If test > 50 Then
                    '    testy = 0
                    'End If
                    Session("ValidData")(k, 0) = AllVarName(i) + Str(AllYear(j))
                    Session("ValidData")(k, 1) = Session("ArrayAllVarYears")(i, j, 0)
                    For m = 1 To NumIndVar
                        Session("ValidData")(k, m + 1) = Session("ArrayAllVarYears")(i, j, m)
                    Next m
                    Session("ValidData")(k, NumIndVar + 2) = AllYear(j)
                    k = k + 1
                End If
            Next j
        Next i

        Session("AllValidPoints") = k - 1


        Exit Sub

ValidError:
        'Msg$ = "There is an error in validation process for chosen variables"
        'MsgBox(Msg$)
        'Screen.MousePointer = 0
        Exit Sub



    End Sub


    'JAM 2014-02-11 Looking for year in dependent data
    Private Function IsValidYear(ByVal chkyear As String) As Integer 'jrs 2014/02/28 Boolean
        On Error GoTo ValidError

        Dim i As Integer
        Dim v_exists As Integer


        v_exists = -1
        For i = 0 To AllNumYears - 1
            If chkyear <> "" And Not IsDBNull(chkyear) Then
                If chkyear <> "" Then
                    If chkyear = AllYear(i) Then
                        v_exists = i 'jrs 2014/02/28 True
                        Exit For
                    End If
                End If
            End If
        Next i

        IsValidYear = v_exists
        Exit Function
ValidError:
        'Msg$ = "There is an error in validation process for years"
        'MsgBox(Msg$)
        'Screen.MousePointer = 0
        IsValidYear = -1 'jrs 2014/02/28 False

    End Function

    'jrs 2014/02/27 added i as index instead of doing for all
    'JAM 2014-02-15 Using Formulas
    Private Function IsFormula(ByVal i As Integer) As Boolean
        On Error GoTo ValidError

        Dim v_exists As Boolean

        v_exists = False
        'jrs 2014/02/27 For i = 1 To UBound(aFormulas)

        If i = 1 Then
            If Not (Session("aFormulas")(i) Is Nothing) Then
                If Session("VarNameD") = Session("aFormulas")(1).Name Then 'JAM 2014-02-25
                    v_exists = True
                    'Exit For
                End If
            End If
        Else
            If i <= UBound(Session("aFormulas")) Then
                If Not (Session("aFormulas")(i) Is Nothing) Then
                    If Session("VarNameI")(i - 2) = Session("aFormulas")(i).Name Then 'JAM 2014-02-25
                        v_exists = True
                        'Exit For
                    End If
                End If
            End If
        End If

        'jrs 2014/02/27 Next i



        IsFormula = v_exists
        Exit Function
ValidError:
        'Msg$ = "There is an error in validation process for calculation fields"
        'MsgBox(Msg$)
        'Screen.MousePointer = 0
        IsFormula = True

    End Function



    'jrs 2014/02/28
    Private Function CheckAndConvertOldFormulasToNewFormat(ByVal sFormulaName As String, ByVal idx As Integer, ByVal bWithYear As Boolean) As Boolean
        'idx = 1 for DV, 2 IV1, 3 IV2 and so on
        Dim bOldForm As Boolean
        bOldForm = False
        If InStr(sFormulaName, "/") > 0 Then
            bOldForm = True
        ElseIf InStr(sFormulaName, "*") > 0 Then
            bOldForm = True
        ElseIf InStr(sFormulaName, "+") > 0 Then
            bOldForm = True
        ElseIf InStr(sFormulaName, "-") > 0 Then
            bOldForm = True
        End If

        If bOldForm Then
            Dim pos As Integer, pos2 As Integer, syear As String
            Dim sNewFormula As String
            sNewFormula = sFormulaName
            pos = InStr(sNewFormula, "(")
            Do While pos > 0
                pos2 = InStr(sNewFormula, ")")
                syear = Mid(sNewFormula, pos + 1, pos2 - pos - 1)
                sNewFormula = Replace(sNewFormula, "(" & syear & ")", "")
                pos = InStr(sNewFormula, "(")
            Loop
            Dim f As Formula
            f = New Formula
            f.init(sNewFormula)
            f.Name = sFormulaName
            Dim i As Integer
            For i = 1 To f.disVar.disSize
                If bWithYear Then
                    f.disVar.setDataDet(i, f.disVar.display(i) & "(" & syear & ")")
                Else
                    f.disVar.setDataDet(i, f.disVar.display(i))
                End If
            Next i
            If idx > UBound(Session("aFormulas")) Then
                ReDim Preserve Session("aFormulas")(idx)
            End If
            Session("aFormulas")(idx) = f
        End If

        CheckAndConvertOldFormulasToNewFormat = bOldForm
    End Function


    Protected Sub cmb_AllYears_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmb_AllYears.SelectedIndexChanged
        'jrs 2014/03/26
        If cmb_AllYears.Items.Count > 0 Then
            Session("ScatVArAllYears") = cmb_AllYears.SelectedIndex
        End If
    End Sub



End Class
