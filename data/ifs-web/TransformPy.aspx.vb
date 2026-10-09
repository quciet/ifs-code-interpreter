Option Strict Off
Option Explicit On
Imports NLog

Partial Class frm_TransformPy
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
            Dim IndLabel As String
            Dim jloop As Short

            Dim tRow As Integer
            '----------------------------------------------------------------------------------------

            '   Size the window according to the screen
            '   ---------------------------------------
            'CONVERSION_ISSUE: Class System.Windows.Forms.FormWindowState from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.WindowState.html'.
            'WindowState = System.Windows.Forms.FormWindowState.Normal
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
            'Width = VB6.TwipsToPixelsX(Inch * 6.6)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            'Height = VB6.TwipsToPixelsY(Inch * 4.5)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Top.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            'Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Left.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Screen from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
            'Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            'cmd_Return.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(frm_Transform.DefInstance.Height) - VB6.PixelsToTwipsY(cmd_Return.Height.Value) * 2)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            'cmd_Continue.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(frm_Transform.DefInstance.Height) - VB6.PixelsToTwipsY(cmd_Continue.Height.Value) * 2)

            '   Initialize according to the Number of Independent Variables
            '   -----------------------------------------------------------
            '    Session("Total_Var") = 1
            For jloop = 1 To Session("Total_Var")
                Independent(jloop - 1).Visible = True
            Next jloop

            For jloop = 2 To Session("Total_Var")
                Frames(jloop - 2).Visible = True
            Next jloop

            Dependent(4).Checked = True
            Independent1(4).Checked = True
            Independent2(4).Checked = True
            Independent3(4).Checked = True
            Independent4(4).Checked = True
            Independent5(4).Checked = True

            'JAM 2009-12-28     Disabling Polynomial Option with multiple independents variables
            If Session("Total_Var") > 1 Then
                Independent1(6).Enabled = False
                Independent1(7).Enabled = False

                Independent1(6).Visible = False
                Independent1(7).Visible = False
            End If

            'JAM 2009-12-28
            Session.Remove("vTransformation")

            'tble10 = db10.OpenRecordset("Var_Names")
            'tble10.MoveFirst()
            Dim pos As Integer, DepVar As String, IndVar As String
            pos = InStr(Session("VarNameD"), "(")
            If pos > 0 And Not Session("bFormula") Then
                DepVar = Mid(Session("VarNameD"), 1, pos - 1)
            Else
                DepVar = Session("VarNameD")
            End If

            lbl_dependent.Text = "Dependent = " + DepVar
            For jloop = 1 To Session("Total_Var")
                pos = InStr(Session("VarNameI")(jloop - 1), "(")
                If pos > 0 And Not Session("bFormula") Then
                    IndVar = Mid(Session("VarNameI")(jloop - 1), 1, pos - 1)
                Else
                    IndVar = Session("VarNameI")(jloop - 1)
                End If

                'tble10.MoveNext()
                tRow = tRow + 1
                IndLabel = "Independent" & Trim(Str(jloop))
                Independent(jloop - 1).Text = IndLabel & " = " + IndVar
            Next jloop
            'db10.Close()

            'JAM 2009-08-26     Resetting Logistic Variables
            If Session("ChangeSatPoint") = Nothing Then
                Session("ChangeSatPoint") = False
                Session("SaturationValue") = 100
            End If
        End If
        'jrs 09/20/04
        'jrs 2012/01/30 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
        'jrs 2013/09/04
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
    ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)

        'jrs 2020/10/02
        If Session("ChangeSatPoint") And Session("TransformationDep") = 5 And Session("ScatterLogistic") = "TransformPy.aspx" Then
            Session("ScatterLogistic") = Nothing
            Dependent(5).Checked = True
            cmd_Continue_Click(Me, New EventArgs)
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
    Public Dependent As System.Collections.ArrayList
    Public Frames As System.Collections.ArrayList
    Public Independent As System.Collections.ArrayList
    Public Independent1 As System.Collections.ArrayList
    Public Independent2 As System.Collections.ArrayList
    Public Independent3 As System.Collections.ArrayList
    Public Independent4 As System.Collections.ArrayList
    Public Independent5 As System.Collections.ArrayList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Sub ControlArrayInit()
        Me.Dependent = New System.Collections.ArrayList
        Me.Frames = New System.Collections.ArrayList
        Me.Independent = New System.Collections.ArrayList
        Me.Independent1 = New System.Collections.ArrayList
        Me.Independent2 = New System.Collections.ArrayList
        Me.Independent3 = New System.Collections.ArrayList
        Me.Independent4 = New System.Collections.ArrayList
        Me.Independent5 = New System.Collections.ArrayList
        Me.Dependent.Add(_Dependent_0)
        Me.Dependent.Add(_Dependent_1)
        Me.Dependent.Add(_Dependent_2)
        Me.Dependent.Add(_Dependent_3)
        Me.Dependent.Add(_Dependent_4)
        Me.Dependent.Add(_Dependent_5)
        Me.Dependent.Add(_Dependent_6)
        Me.Dependent.Add(_Dependent_7)
        Me.Frames.Add(_Frames_2)
        Me.Frames.Add(_Frames_3)
        Me.Frames.Add(_Frames_4)
        Me.Frames.Add(_Frames_5)
        Me.Independent.Add(_Independent_0)
        Me.Independent.Add(_Independent_1)
        Me.Independent.Add(_Independent_2)
        Me.Independent.Add(_Independent_3)
        Me.Independent.Add(_Independent_4)
        Me.Independent1.Add(_Independent1_0)
        Me.Independent1.Add(_Independent1_1)
        Me.Independent1.Add(_Independent1_2)
        Me.Independent1.Add(_Independent1_3)
        Me.Independent1.Add(_Independent1_4)
        Me.Independent1.Add(_Independent1_5)
        Me.Independent1.Add(_Independent1_6)
        Me.Independent1.Add(_Independent1_7)
        Me.Independent2.Add(_Independent2_0)
        Me.Independent2.Add(_Independent2_1)
        Me.Independent2.Add(_Independent2_2)
        Me.Independent2.Add(_Independent2_3)
        Me.Independent2.Add(_Independent2_4)
        Me.Independent2.Add(_Independent2_5)
        Me.Independent2.Add(_Independent2_6)
        Me.Independent2.Add(_Independent2_7)
        Me.Independent3.Add(_Independent3_0)
        Me.Independent3.Add(_Independent3_1)
        Me.Independent3.Add(_Independent3_2)
        Me.Independent3.Add(_Independent3_3)
        Me.Independent3.Add(_Independent3_4)
        Me.Independent3.Add(_Independent3_5)
        Me.Independent3.Add(_Independent3_6)
        Me.Independent3.Add(_Independent3_7)
        Me.Independent4.Add(_Independent4_0)
        Me.Independent4.Add(_Independent4_1)
        Me.Independent4.Add(_Independent4_2)
        Me.Independent4.Add(_Independent4_3)
        Me.Independent4.Add(_Independent4_4)
        Me.Independent4.Add(_Independent4_5)
        Me.Independent4.Add(_Independent4_6)
        Me.Independent4.Add(_Independent4_7)
        Me.Independent5.Add(_Independent5_0)
        Me.Independent5.Add(_Independent5_1)
        Me.Independent5.Add(_Independent5_2)
        Me.Independent5.Add(_Independent5_3)
        Me.Independent5.Add(_Independent5_4)
        Me.Independent5.Add(_Independent5_5)
        Me.Independent5.Add(_Independent5_6)
        Me.Independent5.Add(_Independent5_7)
        AddHandler CType(Dependent.Item(0), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Dependent_CheckedChanged
        AddHandler CType(Dependent.Item(1), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Dependent_CheckedChanged
        AddHandler CType(Dependent.Item(2), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Dependent_CheckedChanged
        AddHandler CType(Dependent.Item(3), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Dependent_CheckedChanged
        AddHandler CType(Dependent.Item(4), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Dependent_CheckedChanged
        AddHandler CType(Dependent.Item(5), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Dependent_CheckedChanged
        AddHandler CType(Dependent.Item(6), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Dependent_CheckedChanged
        AddHandler CType(Dependent.Item(7), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Dependent_CheckedChanged
        AddHandler CType(Independent1.Item(0), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent1_CheckedChanged
        AddHandler CType(Independent1.Item(1), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent1_CheckedChanged
        AddHandler CType(Independent1.Item(2), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent1_CheckedChanged
        AddHandler CType(Independent1.Item(3), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent1_CheckedChanged
        AddHandler CType(Independent1.Item(4), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent1_CheckedChanged
        AddHandler CType(Independent1.Item(5), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent1_CheckedChanged
        AddHandler CType(Independent1.Item(6), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent1_CheckedChanged
        AddHandler CType(Independent1.Item(7), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent1_CheckedChanged
        AddHandler CType(Independent2.Item(0), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent2_CheckedChanged
        AddHandler CType(Independent2.Item(1), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent2_CheckedChanged
        AddHandler CType(Independent2.Item(2), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent2_CheckedChanged
        AddHandler CType(Independent2.Item(3), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent2_CheckedChanged
        AddHandler CType(Independent2.Item(4), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent2_CheckedChanged
        AddHandler CType(Independent2.Item(5), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent2_CheckedChanged
        AddHandler CType(Independent2.Item(6), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent2_CheckedChanged
        AddHandler CType(Independent2.Item(7), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent2_CheckedChanged
        AddHandler CType(Independent3.Item(0), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent3_CheckedChanged
        AddHandler CType(Independent3.Item(1), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent3_CheckedChanged
        AddHandler CType(Independent3.Item(2), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent3_CheckedChanged
        AddHandler CType(Independent3.Item(3), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent3_CheckedChanged
        AddHandler CType(Independent3.Item(4), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent3_CheckedChanged
        AddHandler CType(Independent3.Item(5), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent3_CheckedChanged
        AddHandler CType(Independent3.Item(6), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent3_CheckedChanged
        AddHandler CType(Independent3.Item(7), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent3_CheckedChanged
        AddHandler CType(Independent4.Item(0), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent4_CheckedChanged
        AddHandler CType(Independent4.Item(1), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent4_CheckedChanged
        AddHandler CType(Independent4.Item(2), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent4_CheckedChanged
        AddHandler CType(Independent4.Item(3), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent4_CheckedChanged
        AddHandler CType(Independent4.Item(4), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent4_CheckedChanged
        AddHandler CType(Independent4.Item(5), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent4_CheckedChanged
        AddHandler CType(Independent4.Item(6), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent4_CheckedChanged
        AddHandler CType(Independent4.Item(7), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent4_CheckedChanged
        AddHandler CType(Independent5.Item(0), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent5_CheckedChanged
        AddHandler CType(Independent5.Item(1), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent5_CheckedChanged
        AddHandler CType(Independent5.Item(2), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent5_CheckedChanged
        AddHandler CType(Independent5.Item(3), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent5_CheckedChanged
        AddHandler CType(Independent5.Item(4), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent5_CheckedChanged
        AddHandler CType(Independent5.Item(5), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent5_CheckedChanged
        AddHandler CType(Independent5.Item(6), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent5_CheckedChanged
        AddHandler CType(Independent5.Item(7), System.Web.UI.WebControls.RadioButton).CheckedChanged, AddressOf Independent5_CheckedChanged
    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_TransformPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_Transform
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_Transform()
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
    '------------------------------------------Transform.Frm-------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last modified by BBH on 9/11/99

    Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()
    Dim ValueB As Double

    '==========================frm_Transform=============================================
    Private Function Transformed(ByRef Value_Number As Double, ByRef transtype As Short) As Double
        '----------------------------------------------------------------------------------------

        Select Case transtype
            Case 0
                '   Apply LOG function
                If Value_Number > 0 Then
                    Transformed = System.Math.Log(Value_Number)
                End If
            Case 1
                '   Apply 2nd Power
                Transformed = Value_Number ^ 2
            Case 2
                '   Apply bth Power
                Transformed = Value_Number ^ ValueB
            Case 3
                '   Apply e^x
                'JRS 11/03/03
                If Value_Number < 710 Then
                    Transformed = System.Math.Exp(Value_Number)
                End If
            Case 4
                '   No Transformation
                Transformed = Value_Number
            Case 5  'JAM 2009-08-26 Code added for Logit Option
                If Session("ChangeSatPoint") Then
                    If IsNumeric(Value_Number) And IsNumeric(Session("SaturationValue")) Then
                        If Value_Number <> 0 Then
                            If ((Session("SaturationValue") / Value_Number) - 1) > 0 Then
                                Transformed = Math.Log((Session("SaturationValue") / Value_Number) - 1)
                            End If
                        End If
                    End If
                End If
            Case 6 'JAM 2009-12-26
                '   No Transformation is required
                Transformed = Value_Number
            Case 7 'JAM 2009-12-26
                '   No Transformation is required
                Transformed = Value_Number
        End Select

    End Function

    '==========================frm_Transform=============================================
    Private Sub cmd_Continue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Continue.Click
        If Not Session("ChangeSatPoint") And Dependent(5).Checked Then
            Session("TransformationDep") = 5
            Session("ScatterLogistic") = "TransformPy.aspx"
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_SaturParam.aspx")
        End If

        Dim ICount As Short
        Dim IndLabel As String
        Dim jloop As Short
        Dim CreateQuery As String
        Dim i As Integer
        Dim nrow As DataRow
        Dim tRow As Integer
        Dim newtbl As DataTable
        Dim nrow1 As DataRow
        '----------------------------------------------------------------------------------------

        '   Open the database and delete the previous Statistics table
        '   ----------------------------------------------------------
        'db10.TableDefs.Delete(("Statistics"))

        '   Create a new table Statistics
        '   -----------------------------
        'table10 = db10.CreateTableDef("Statistics")
        'With table10
        '    .Fields.append(.CreateField("Region", DAO.DataTypeEnum.dbText))
        '    .Fields.append(.CreateField("Dependent", DAO.DataTypeEnum.dbDouble))
        '    For jloop = 1 To Session("Total_Var")
        '        IndLabel = "Independent" & Trim(Str(jloop))
        '        .Fields.append(.CreateField(IndLabel, DAO.DataTypeEnum.dbDouble))
        '    Next jloop
        'End With
        'db10.TableDefs.append(table10)
        'db10.Close()

        '   Apply the transformations
        '   -------------------------

        On Error GoTo ErrorInStatistics
        'tble8 = db10.OpenRecordset("Var_Names")
        'tble9.MoveFirst()
        'While Not tble9.EOF

        ReDim Session("TransformedData")(Session("AllValidPoints"), 6)

        For i = 0 To Session("AllValidPoints") - 1

            '   Copy the data after applying transformations
            '   --------------------------------------------

            'JAM 2009-12-28 Changing index from 5 to 7 for quadratic and third dimension transformation
            'JAM 2009-08-25 Changing index from 4 to 5 for new Logit Transformation
            For jloop = 0 To 7  'Loop on possible transformations, including none
                'tble8.MoveFirst()
                tRow = 0
                'jrs 2024/06/14 added and also and checking for Nothing
                If Dependent(jloop).Checked = True AndAlso (Not IsDBNull(Session("ValidData")(i, 1)) AndAlso Not IsNothing(Session("ValidData")(i, 1)) AndAlso Session("ValidData")(i, 1).ToString <> "") Then
                    ValueB = Val(lbl_Dependentb.Text)
                    'nrow.Item("Dependent") = Transformed(Session("ValidData")(i, 1), jloop)
                    'JAM 2010-02-15 If value is not null, transformation is required
                    If IsNumeric(Session("ValidData")(i, 1)) Then
                        If jloop <> 5 Then 'JAM 2010-02-16 Exception when user choose logistic
                            Session("TransformedData")(i, 1) = Transformed(Session("ValidData")(i, 1), jloop)
                        Else
                            If Session("ValidData")(i, 1) <> 0 Then
                                Session("TransformedData")(i, 1) = Transformed(Session("ValidData")(i, 1), jloop)
                            Else
                                Session("TransformedData")(i, 1) = DBNull.Value
                            End If
                        End If
                    Else
                        Session("TransformedData")(i, 1) = Session("ValidData")(i, 1)
                    End If
                    'tble8.Edit()
                    Session("TransformationDep") = jloop
                    Session("ValueB_Dep") = ValueB
                    'tble8.Update()
                    'jrs 10/10/04
                    'nrow1 = newtbl.NewRow
                    'nrow1.ItemArray = tble8.Rows(tRow).ItemArray
                    'newtbl.Rows.Add(nrow1)
                End If
                For ICount = 1 To Session("Total_Var") 'Loop on 5 independent variables
                    'tble8.MoveNext()
                    tRow = tRow + 1
                    Select Case ICount
                        Case 1
                            If Independent1(jloop).Checked = True And (Not IsDBNull(Session("ValidData")(i, 2)) And Session("ValidData")(i, 2).ToString <> "") Then
                                ValueB = Val(lbl_Independent1b.Text)
                                'Session("TransformedData")(i, 2) = Transformed(Session("ValidData")(i, 2), jloop)
                                'JAM 2010-02-15 If value is not null, transformation is required
                                If IsNumeric(Session("ValidData")(i, 2)) Then
                                    Session("TransformedData")(i, 2) =
                                        Transformed(Session("ValidData")(i, 2), jloop)
                                Else
                                    Session("TransformedData")(i, 2) =
                                        Session("ValidData")(i, 2)
                                End If

                                'tble8.Edit()
                                Session("TransformationInd1") = jloop
                                Session("ValueB_Ind1") = ValueB
                                'tble8.Update()
                                'jrs 10/10/04
                                'nrow1 = newtbl.NewRow
                                'nrow1.ItemArray = tble8.Rows(tRow).ItemArray
                                'newtbl.Rows.Add(nrow1)
                            End If
                        Case 2
                            If Independent2(jloop).Checked = True And (Not IsDBNull(Session("ValidData")(i, 3)) And Session("ValidData")(i, 3).ToString <> "") Then
                                ValueB = Val(lbl_Independent2b.Text)
                                Session("TransformedData")(i, 3) = Transformed(Session("ValidData")(i, 3), jloop)
                                'tble8.Edit()
                                Session("TransformationInd2") = jloop
                                Session("ValueB_Ind2") = ValueB
                                'tble8.Update()
                                'jrs 10/10/04
                                'nrow1 = newtbl.NewRow
                                'nrow1.ItemArray = tble8.Rows(tRow).ItemArray
                                'newtbl.Rows.Add(nrow1)
                            End If
                        Case 3
                            If Independent3(jloop).Checked = True And (Not IsDBNull(Session("ValidData")(i, 4)) And Session("ValidData")(i, 4).ToString <> "") Then
                                ValueB = Val(lbl_Independent3b.Text)
                                Session("TransformedData")(i, 4) = Transformed(Session("ValidData")(i, 4), jloop)
                                'tble8.Edit()
                                Session("TransformationInd3") = jloop
                                Session("ValueB_Ind3") = ValueB
                                'tble8.Update()
                                'jrs 10/10/04
                                'nrow1 = newtbl.NewRow
                                'nrow1.ItemArray = tble8.Rows(tRow).ItemArray
                                'newtbl.Rows.Add(nrow1)
                            End If
                        Case 4
                            If Independent4(jloop).Checked = True And (Not IsDBNull(Session("ValidData")(i, 5)) And Session("ValidData")(i, 5).ToString <> "") Then
                                ValueB = Val(lbl_Independent4b.Text)
                                Session("TransformedData")(i, 5) = Transformed(Session("ValidData")(i, 5), jloop)
                                'tble8.Edit()
                                Session("TransformationInd4") = jloop
                                Session("ValueB_Ind4") = ValueB
                                'tble8.Update()
                                'jrs 10/10/04
                                'nrow1 = newtbl.NewRow
                                'nrow1.ItemArray = tble8.Rows(tRow).ItemArray
                                'newtbl.Rows.Add(nrow1)
                            End If
                        Case 5
                            If Independent5(jloop).Checked = True And (Not IsDBNull(Session("ValidData")(i, 6)) And Session("ValidData")(i, 6).ToString <> "") Then
                                ValueB = Val(lbl_Independent5b.Text)
                                Session("TransformedData")(i, 6) = Transformed(Session("ValidData")(i, 6), jloop)
                                'tble8.Edit()
                                Session("TransformationInd5") = jloop
                                Session("ValueB_Ind5") = ValueB
                                'tble8.Update()
                                'jrs 10/10/04
                                'nrow1 = newtbl.NewRow
                                'nrow1.ItemArray = tble8.Rows(tRow).ItemArray
                                'newtbl.Rows.Add(nrow1)
                            End If
                    End Select

                Next ICount
            Next jloop
            'tble9.MoveNext()
        Next
        'End While
        'db10.Close()
        'jrs 10/10/04

        '   Direct to the Statistics Form
        '   -----------------------------

        'jrs 2011/04/07 Session("Statistics_Type") = 1

        'Support.FormManager.Close(Me)
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "StatisticsPy.aspx")
        'frm_statistics.DefInstance.Show() 
        Exit Sub

ErrorInStatistics:


        Dependent(4).Checked = True
        Independent1(4).Checked = True
        Independent2(4).Checked = True
        Independent3(4).Checked = True
        Independent4(4).Checked = True
        Independent5(4).Checked = True
        'db10.Close()

        Dim Message As String
        Message = "An Overflow occured while transformations were applied. Please choose a set of " & "different transformations"
        _logger.Error(Err.GetException, $"cmd_Continue_Click - {Message}")
        LoadMessage(Message, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '==========================frm_Transform=============================================
    Private Sub cmd_Return_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Return.Click
        '----------------------------------------------------------------------------------------

        '   Return to selecting other variables
        '   -----------------------------------
        Session.Remove("vTransformation")

        If Session("Statistics_Type") = 1 Then
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "ScatVarS.aspx")
            'frm_ScatVarS.DefInstance.Show() 
        Else
            If Session("VarsCount") > 3 Then
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "DISPMENU.aspx")
            Else 'jrs 2020/12/22
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
            End If
            'frm_DisplayMenu.DefInstance.Show() 
        End If
        Support.FormManager.Close(Me)
    End Sub

    '==========================frm_Transform=============================================
    Private Sub Dependent_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Dim index As Short = Dependent.IndexOf(eventSender)
            Dim ValueBStr As String
            '----------------------------------------------------------------------------------------

            If index = 2 Then
                'jrs 09/27/04 ValueBStr = InputBox("Enter a value for b", , CStr(3))
                lblBTitle.Visible = True
                txt_Dependentb.Visible = True
                butDep.Visible = True
            Else

                Dim a As Integer
                If index = 5 Then   'JAM 2009-08-25  Adding functionality for Logit
                    For a = 0 To 4
                        Dependent(a).Checked = False
                    Next a
                Else
                    lbl_Dependentb.Visible = False
                    'jrs 09/27/04
                    lblBTitle.Visible = False
                    txt_Dependentb.Visible = False
                    butDep.Visible = False
                End If
            End If

            'JAM 2009-08-25  Adding functionality for Logit
            If index <> 5 Then
                Dependent(5).Checked = False
            End If

        End If
    End Sub



    '==========================frm_Transform=============================================
    'UPGRADE_WARNING: Event Independent1.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Independent1_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Dim index As Short = Independent1.IndexOf(eventSender)
            Dim ValueBStr As String
            '----------------------------------------------------------------------------------------

            If index = 2 Then
                'jrs 09/27/04 ValueBStr = InputBox("Enter a value for b", , CStr(3))
                lblBTitle.Visible = True
                txt_Independent1b.Visible = True
                butInd1.Visible = True
            Else
                lbl_Independent1b.Visible = False
                'jrs 09/27/04
                lblBTitle.Visible = False
                txt_Independent1b.Visible = False
                butInd1.Visible = False
            End If

            'JAM 2009-12-28 It's a polynomial transformation
            If index = 6 Then
                Session("vTransformation") = "SEC"
            Else
                If index = 7 Then
                    Session("vTransformation") = "THI"
                Else
                    Session.Remove("vTransformation")
                End If
            End If

        End If
    End Sub

    '==========================frm_Transform=============================================
    'UPGRADE_WARNING: Event Independent2.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Independent2_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Dim index As Short = Independent2.IndexOf(eventSender)
            Dim ValueBStr As String
            '----------------------------------------------------------------------------------------

            If index = 2 Then
                'jrs 09/27/04 ValueBStr = InputBox("Enter a value for b", , CStr(3))
                lblBTitle.Visible = True
                txt_Independent2b.Visible = True
                butInd2.Visible = True
            Else
                lbl_Independent2b.Visible = False
                'jrs 09/27/04
                lblBTitle.Visible = False
                txt_Independent2b.Visible = False
                butInd2.Visible = False
            End If

        End If
    End Sub

    '==========================frm_Transform=============================================
    'UPGRADE_WARNING: Event Independent3.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Independent3_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Dim index As Short = Independent3.IndexOf(eventSender)
            Dim ValueBStr As String
            '----------------------------------------------------------------------------------------

            If index = 2 Then
                'jrs 09/27/04 ValueBStr = InputBox("Enter a value for b", , CStr(3))
                lblBTitle.Visible = True
                txt_Independent3b.Visible = True
                butInd3.Visible = True
            Else
                lbl_Independent3b.Visible = False
                'jrs 09/27/04
                lblBTitle.Visible = False
                txt_Independent3b.Visible = False
                butInd3.Visible = False
            End If

        End If
    End Sub

    '==========================frm_Transform=============================================
    'UPGRADE_WARNING: Event Independent4.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Independent4_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Dim index As Short = Independent4.IndexOf(eventSender)
            Dim ValueBStr As String
            '----------------------------------------------------------------------------------------

            If index = 2 Then
                'jrs 09/27/04 ValueBStr = InputBox("Enter a value for b", , CStr(3))
                lblBTitle.Visible = True
                txt_Independent4b.Visible = True
                butInd4.Visible = True
            Else
                lbl_Independent4b.Visible = False
                'jrs 09/27/04
                lblBTitle.Visible = False
                txt_Independent4b.Visible = False
                butInd4.Visible = False
            End If

        End If
    End Sub

    '==========================frm_Transform=============================================
    'UPGRADE_WARNING: Event Independent5.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Independent5_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If eventSender.Checked Then
            Dim index As Short = Independent5.IndexOf(eventSender)
            Dim ValueBStr As String
            '----------------------------------------------------------------------------------------

            If index = 2 Then
                'jrs 09/27/04 ValueBStr = InputBox("Enter a value for b", , CStr(3))
                lblBTitle.Visible = True
                txt_Independent5b.Visible = True
                butInd5.Visible = True
            Else
                lbl_Independent5b.Visible = False
                'jrs 09/27/04
                lblBTitle.Visible = False
                txt_Independent5b.Visible = False
                butInd5.Visible = False
            End If

        End If
    End Sub


    Private Sub txt_Dependentb_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Dependentb.TextChanged
        'jrs 09/27/04
        If txt_Dependentb.Text = "" Then
            Dependent(4).Checked = True
        Else
            lbl_Dependentb.Visible = True
            lbl_Dependentb.Text = txt_Dependentb.Text
        End If
        lblBTitle.Visible = False
        txt_Dependentb.Visible = False
        butDep.Visible = False
    End Sub


    Private Sub txt_Independent1b_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Independent1b.TextChanged
        'jrs 09/27/04
        If txt_Independent1b.Text = "" Then
            Independent1(4).Checked = True
        Else
            lbl_Independent1b.Visible = True
            lbl_Independent1b.Text = txt_Independent1b.Text
        End If
        lblBTitle.Visible = False
        txt_Independent1b.Visible = False
        butInd1.Visible = False
    End Sub

    Private Sub txt_Independent2b_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Independent2b.TextChanged
        'jrs 09/27/04
        If txt_Independent2b.Text = "" Then
            Independent2(4).Checked = True
        Else
            lbl_Independent2b.Visible = True
            lbl_Independent2b.Text = txt_Independent2b.Text
        End If
        lblBTitle.Visible = False
        txt_Independent2b.Visible = False
        butInd2.Visible = False
    End Sub

    Private Sub txt_Independent3b_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Independent3b.TextChanged
        'jrs 09/27/04
        If txt_Independent3b.Text = "" Then
            Independent3(4).Checked = True
        Else
            lbl_Independent3b.Visible = True
            lbl_Independent3b.Text = txt_Independent3b.Text
        End If
        lblBTitle.Visible = False
        txt_Independent3b.Visible = False
        butInd3.Visible = False
    End Sub

    Private Sub txt_Independent4b_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Independent4b.TextChanged
        'jrs 09/27/04
        If txt_Independent4b.Text = "" Then
            Independent4(4).Checked = True
        Else
            lbl_Independent4b.Visible = True
            lbl_Independent4b.Text = txt_Independent4b.Text
        End If
        lblBTitle.Visible = False
        txt_Independent4b.Visible = False
        butInd4.Visible = False
    End Sub

    Private Sub txt_Independent5b_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Independent5b.TextChanged
        'jrs 09/27/04
        If txt_Independent5b.Text = "" Then
            Independent5(4).Checked = True
        Else
            lbl_Independent5b.Visible = True
            lbl_Independent5b.Text = txt_Independent5b.Text
        End If
        lblBTitle.Visible = False
        txt_Independent5b.Visible = False
        butInd5.Visible = False
    End Sub

End Class
