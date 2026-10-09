Option Strict Off
Option Explicit On

Partial Class frm_GraphLabels
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
        'JAM 2015-03-07



        InitializeComponent()
        If Not Page.IsPostBack Then

            Dim i As Short
            Dim s As String
            For i = 1 To Session("COUNTE")
                s = Session("VARN")(0, i)
                If Session("FDNG1")(i) > "" Then
                    s = s & " - " & Session("FDNG1")(i)
                    If Session("FDNG2")(i) > "" Then
                        s = s & " - " & Session("FDNG2")(i)
                    End If
                End If
                'JAM 2014-08-23 Adding value for each option in order to get a different value for Common Dimmensions Options
                lstDim1.Items.Add(New ListItem(s, i.ToString))
                ''lstDim1.Items.Add(New ListItem(s))

            Next (i)
            'jrs 2006/02/24
            lstDim1.SelectedIndex = 0
            lstDim1_SelectedIndexChanged(Me, New EventArgs)
        End If
    End Sub

    'CHANGE DISPLAY LABELS FOR GRAPHS

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
    'CONVERSION_NOTE: Following code was added for support purposes.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1016.html'.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_GraphLabelsPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_GraphLabels
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_GraphLabels()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    'JRS 2/12/02
    'CHANGE DISPLAY LABELS FOR GRAPHS

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, Session("GraphLabelsParent"))
    End Sub


    Private Sub cmdUpdVar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdVar.Click
        Dim i, j, k As Integer

        Dim stemp1, stemp2, stemp3 As String
        Dim stitle1 As String

        Dim stest As String
        Dim a, b As Integer

        On Error GoTo ErrLabels

        i = lstDim1.SelectedIndex + 1

        'JAM 2015-02-03 Changing subsets values with Third dimmensions
        'jrs 2023/10/04 If Session("gThirdDimmOk") Then
        'JAM 2015-03-31
        ''If Not Session("LabelChanged") Then
        ''    ReDim Session("NewSetLegends")(Session("SplitGraph"))
        ''    'JAM 2015-03-22 Changing points labels
        ''    ReDim Session("NewCGroupLabels")(Session("ItemsbySet"))
        ''    'JAM 2015-03-31
        ''    ReDim Session("ChangedCGroupLabels")(Session("ItemsbySet"))
        ''Else
        ''    'JAM 2015-03-31 Changing points labels
        ''    For k = 1 To Session("ItemsbySet")
        ''        Session("CGroupLabels")(k) = Session("NewCGroupLabels")(k)
        ''    Next

        ''    'JAM 2015-04-01 Changing subsets titles
        ''    For j = 1 To Session("SplitGraph")
        ''        Session("SetLegends")(j) = Session("NewSetLegends")(j)
        ''    Next
        ''End If

        Session("LabelChanged") = True






        ''Dim a, b, c, d, ex As String
        ''b = Session("SetLegends")(i)
        ''c = Session("FDNG1")(i)
        ''d = txtDim1.Text
        ''ex = ""
        ''a = Replace(Session("SetLegends")(i), Session("FDNG1")(i), txtDim1.Text)




        ''For j = 1 To Session("SplitGraph")
        ''    '' b = Session("SetLegends")(j)


        ''    'JAM 2015-03-30 Searching substring without [] characters
        ''    If InStr(Session("FDNG1")(i).ToString, "[") <> 0 Then
        ''        stemp1 = Session("FDNG1")(i).ToString.Substring(0, InStr(Session("FDNG1")(i).ToString, "[") - 1)
        ''    Else
        ''        stemp1 = Session("FDNG1")(i)
        ''    End If
        ''    If InStr(Session("VARN")(0, i).ToString, "[") <> 0 Then
        ''        stemp2 = Session("VARN")(0, i).ToString.Substring(0, InStr(Session("VARN")(0, i).ToString, "[") - 1)
        ''    Else
        ''        stemp2 = Session("VARN")(0, i)
        ''    End If

        ''    If InStr(Session("FDNG2")(i).ToString, "[") <> 0 Then
        ''        stemp3 = Session("FDNG2")(i).ToString.Substring(0, InStr(Session("FDNG2")(i).ToString, "[") - 1)
        ''    Else
        ''        stemp3 = Session("FDNG2")(i)
        ''    End If

        ''    'JAM 2015-04-01
        ''    If InStr(txtVarName.Text.ToString, "[") <> 0 And InStr(txtVarName.Text.ToString, "]") <> 0 Then
        ''        stitle1 = txtVarName.Text
        ''        ''a = InStr(txtVarName.Text.ToString, "[") + 1
        ''        ''b = InStr(txtVarName.Text.ToString, "]") - (InStr(txtVarName.Text.ToString, "[") + 1)
        ''        stitle1 = stitle1.Substring(InStr(txtVarName.Text.ToString, "["), InStr(txtVarName.Text.ToString, "]") - (InStr(txtVarName.Text.ToString, "[") + 1))

        ''        stemp2 = Session("VARN")(0, i).ToString.Substring(InStr(Session("VARN")(0, i).ToString, "["), InStr(Session("VARN")(0, i).ToString, "]") - (InStr(Session("VARN")(0, i).ToString, "[") + 1))

        ''    Else
        ''        stitle1 = txtVarName.Text

        ''    End If

        ''    Session("SetLegends")(j) = Replace(Session("SetLegends")(j), stemp1, txtDim1.Text)
        ''    Session("SetLegends")(j) = Replace(Session("SetLegends")(j), stemp2, stitle1)
        ''    Session("SetLegends")(j) = Replace(Session("SetLegends")(j), stemp3, txtDim2.Text)
        ''    Session("NewSetLegends")(j) = Session("SetLegends")(j) 'JAM 2015-03-07
        ''    '' ex = Session("SetLegends")(j)

        ''Next

        ' ''JAM 2015-03-22 Changing points labels
        ''For k = 1 To Session("ItemsbySet")

        ''    ''Session("CGroupLabels")(k) = Replace(Session("CGroupLabels")(k), Session("FDNG1")(i), txtDim1.Text)
        ''    ''Session("CGroupLabels")(k) = Replace(Session("CGroupLabels")(k), Session("VARN")(0, i), txtVarName.Text)
        ''    ''Session("CGroupLabels")(k) = Replace(Session("CGroupLabels")(k), Session("FDNG2")(i), txtDim2.Text)


        ''    'JAM 2015-03-30 Searching substring without [] characters
        ''    If InStr(Session("FDNG1")(i).ToString, "[") <> 0 Then
        ''        stemp1 = Session("FDNG1")(i).ToString.Substring(0, InStr(Session("FDNG1")(i).ToString, "[") - 1)
        ''    Else
        ''        stemp1 = Session("FDNG1")(i)
        ''    End If
        ''    If InStr(Session("VARN")(0, i).ToString, "[") <> 0 Then
        ''        stemp2 = Session("VARN")(0, i).ToString.Substring(0, InStr(Session("VARN")(0, i).ToString, "[") - 1)
        ''    Else
        ''        stemp2 = Session("VARN")(0, i)
        ''    End If

        ''    If InStr(Session("FDNG2")(i).ToString, "[") <> 0 Then
        ''        stemp3 = Session("FDNG2")(i).ToString.Substring(0, InStr(Session("FDNG2")(i).ToString, "[") - 1)
        ''    Else
        ''        stemp3 = Session("FDNG2")(i)
        ''    End If

        ''    'JAM 2015-03-31 Replacing substring without [] characters 
        ''    If InStr(Session("CGroupLabels")(k).ToString, "[") <> 0 Then

        ''        Session("CGroupLabels")(k) = Session("CGroupLabels")(k).ToString().Substring(0, InStr(Session("CGroupLabels")(k).ToString, "[") - 1)

        ''    End If


        ''    'JAM 2015-03-30 Searching substring without [] characters
        ''    Session("CGroupLabels")(k) = Replace(Session("CGroupLabels")(k), stemp1, txtDim1.Text)
        ''    Session("CGroupLabels")(k) = Replace(Session("CGroupLabels")(k), stemp2, txtVarName.Text)
        ''    Session("CGroupLabels")(k) = Replace(Session("CGroupLabels")(k), stemp3, txtDim2.Text)

        ''    Session("NewCGroupLabels")(k) = Session("CGroupLabels")(k) 'JAM 2015-03-07


        ''    '' ex = Session("SetLegends")(j)



        ''Next



        'jrs 2023/10/04 End If



        Session("VARN")(0, i) = txtVarName.Text
        Session("FDNG1")(i) = txtDim1.Text
        Session("FDNG2")(i) = txtDim2.Text




        Dim s As String
        s = Session("VARN")(0, i)
        If Session("FDNG1")(i) > "" Then
            s = s & " - " & Session("FDNG1")(i)
            If Session("FDNG2")(i) > "" Then
                s = s & " - " & Session("FDNG2")(i)
            End If
        End If
        lstDim1.Items(i - 1).Text = s
        'jrs 2023/10/03
        ''jrs 2012/07/23
        'If Session("bAlias") = True Then
        '    Session.Remove("bAlias")
        '    Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "Alias=1", "Alias=0")
        '    Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "&Leg=0", "")
        '    Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "&Leg=1", "")
        '    Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "&Leg=2", "")
        '    Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "&Leg=3", "")
        '    Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "&Leg=4", "")
        '    Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "&Leg=5", "")
        '    Session("GraphLabelsParent") = Replace(Session("GraphLabelsParent"), "&Leg=9", "")
        'End If
        Session("GraphLabelsParent") = "frm_GraphicalDisplay.aspx"
        Session("bAlias") = False
ErrLabels:

        Exit Sub
    End Sub


    Private Sub lstDim1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDim1.SelectedIndexChanged
        Dim i As Integer
        i = lstDim1.SelectedIndex + 1
        txtVarName.Text = Session("VARN")(0, i)
        txtDim1.Text = Session("FDNG1")(i)
        txtDim2.Text = Session("FDNG2")(i)
    End Sub
End Class
