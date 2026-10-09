Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'                   JAM 2011-01-29      Parameter form to define the Saturation Value
'                                       
'

Partial Class frm_SaturParam
    Inherits Page
    Dim InitialSat As Double
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ' InitializeComponent()
        If Not Page.IsPostBack Then
            InitialSat = Session("SaturationValue")
            txtSaturValue.Text = Session("SaturationValue")

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
        Session("ChangeSatPoint") = False
        frm_SaturParam_Closed()
        Support.FormManager.Show(Me, Session("ScatterLogistic"))


    End Sub

    'CONVERSION_NOTE: The code of Event 'frm_DisplayControl.frm_DisplayControl_Load' was moved to Method 'frm_DisplayControl.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_SaturParam_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'jrs 2012/05/31
        txtSaturValue.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdOk.ClientID + "')")
    End Sub

    Private Sub frm_SaturParam_Closed()

        'Dim a As Double

        'If Not IsNumeric(txtSaturValue.Text) Then

        '    Session("PowerDegree") = InitialPower

        'Else
        '    a = CDbl(txtSaturValue.Text)
        '    Session("PowerDegree") = CDbl(txtSaturValue.Text)


        'End If

        'If InitialPower = Session("PowerDegree") Then
        '    Session("IsChangedNum") = False
        'Else
        '    Session("IsChangedNum") = True
        'End If
        'Session("SourcePower") = "PowerDegree"
        'Session("bDisplayControl") = True
    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.TextBox.Leave' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Leave.html'.
    Private Sub Txt_SaturParam_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Not IsNumeric(txtSaturValue.Text) Then
            txtSaturValue.Text = InitialSat
        End If
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If txtSaturValue.Text <> "" And IsNumeric(txtSaturValue.Text) Then
            If ValidSatDep() Then
                Session("ChangeSatPoint") = True
                Session("mnuLogistic") = True
                Session("SaturationValue") = txtSaturValue.Text
                frm_SaturParam_Closed()
                Support.FormManager.Show(Me, Session("ScatterLogistic"))

            End If

            'Else
            '    msg$ = "You must enter only numeric values"
            '    MsgBox(msg$, vbCritical, "Saturation Point")

        End If

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '   JAM 2010-11-29      Validation that It's a valid Saturation Point
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function ValidSatDep() As Boolean
        Dim i, limit As Integer
        Dim isvalid As Boolean

        'jrs 2017/07/31
        If Session("ScatterLogistic") = "frm_SDG.aspx" Then
            isvalid = False

            For i = 1 To Session("LastYearForecast") - FirstYearHistory + 1
                If IsNumeric(Session("SDGPLOTSDGH")(i, 1)) Then
                    If Session("SDGPLOTSDGH")(i, 1) <> 0 Then
                        If ((txtSaturValue.Text / Session("SDGPLOTSDGH")(i, 1)) - 1) > 0 Then
                            isvalid = True
                            Exit For
                        End If
                    End If
                End If
            Next i

            ValidSatDep = isvalid

        ElseIf Session("ScatterLogistic") = "Scatter.aspx" Or
            Session("ScatterLogistic") = "TransformPy.aspx" Then
            If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                limit = Session("TotCountries")
            Else
                limit = Session("CCount")
            End If

            isvalid = False

            For i = 1 To limit
                If Session("WholeCountries") And Session("IsaGlist") And Not Session("IsaGroup") Then
                    'jrs 2013/12/16 If IsNumeric(Session("OrigDep")(i)) And Session("OrigDep")(i) <> 0 Then
                    If IsNumeric(Session("OrigDep")(i)) Then
                        If Session("OrigDep")(i) <> 0 Then
                            If ((txtSaturValue.Text) / (Session("OrigDep")(i)) - 1) > 0 Then
                                isvalid = True
                                Exit For
                            End If
                        End If
                    End If
                Else
                    'jrs 2013/12/16 If IsNumeric(Session("CVarD")(i)) And Session("CVarD")(i) <> 0 Then
                    If IsNumeric(Session("CVarD")(i)) Then
                        If Session("CVarD")(i) <> 0 Then
                            If ((txtSaturValue.Text) / (Session("CVarD")(i)) - 1) > 0 Then
                                isvalid = True
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next i

            ValidSatDep = isvalid
        Else 'JAM 2009-11-08 Code added for Saturation Point in Proessential Form

            limit = Session("COUNTE") / 2

            isvalid = False

            For i = 1 To limit
                'jrs 2013/12/16
                'If IsNumeric(Session("PLOT")(Session("PYear"), i)) And Session("PLOT")(Session("PYear"), i) <> 0 And _
                'IsNumeric(txtSaturValue.Text) Then
                If IsNumeric(Session("PLOT")(Session("PYear"), i)) And IsNumeric(txtSaturValue.Text) Then
                    If Session("PLOT")(Session("PYear"), i) <> 0 Then
                        If (CInt(txtSaturValue.Text) / Session("PLOT")(Session("PYear"), i) - 1) > 0 Then
                            isvalid = True
                            Exit For
                        End If
                    End If
                End If
            Next i

            ValidSatDep = isvalid

        End If

    End Function

End Class
