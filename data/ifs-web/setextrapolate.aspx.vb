Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_setextrapolate
    Inherits Page
    'Mohammod .Net Win Forms Feb 2004- New form
#Region " Web Form Designer Generated Code "

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer.
        'Do not modify it using the code editor.

        InitializeComponent()


        If Not Page.IsPostBack Then
        End If
    End Sub

    'CONVERSION_NOTE: Method 'Dispose' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    'If disposing Then 
    'If Not (components Is Nothing) Then 
    'components.Dispose()
    'End If 
    'End If 
    'MyBase.Dispose(disposing)
    'End Sub  

    'CONVERSION_NOTE: Field 'components' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Private components As System.ComponentModel.IContainer 

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

#End Region

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.GroupBox.Enter' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Enter.html'.
    'Private Sub frameSetExtrapolate_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frameSetExtrapolate.Enter

    'End Sub 

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        'Me.Visible = False
        Support.FormManager.Close(Me)
    End Sub

    Private Sub cmdDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefault.Click
        Dim VS As String

        Session("InpYearsAhead") = DefYearsAhead
        txtYearsAhead.Text = Session("InpYearsAhead").ToString



        Select Case True
            Case OptPoly.Checked 'polynomial
                Session("InpPOLY1") = DefPOLY1
                txtP1.Text = Session("InpPOLY1").ToString
                VS = "Polynomial "


            Case OptExpo.Checked 'exponential
                Session("InpEXPO1") = Session("DefEXPO1")
                Session("InpEXPO2") = Session("DefEXPO2")
                txtP1.Text = VB6.Support.Format(Session("InpEXPO1"), "0.000")
                txtP2.Text = Session("InpEXPO2").ToString
                VS = "Exponential "


            Case OptSCU.Checked 's-curve
                Session("InpSLIN1") = Session("DefSLIN1")
                txtP1.Text = VB6.Support.Format(Session("InpSLIN1"), "0.000")
                VS = "S-curve "


            Case OptSCUNL.Checked 's-curve non linear
                Session("InpSCUR1") = Session("DefSCUR1")
                Session("InpSCUR2") = Session("DefSCUR2")
                Session("InpSCUR3") = Session("DefSCUR3")
                txtP1.Text = VB6.Support.Format(Session("InpSCUR1"), "0.000")
                txtP2.Text = Session("InpSCUR2").ToString
                txtP3.Text = Session("InpSCUR3").ToString
                VS = "S-curve (Non-Linear) "


            Case Else

                OptPoly.Checked = True
                Session("InpPOLY1") = DefPOLY1
                txtP1.Text = Session("InpPOLY1").ToString
                VS = "Polynomial "
        End Select

        VS = "Defaults applied to years to extrapolate and " & VS & "parameters."

        LoadMessage(VS, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    'CONVERSION_NOTE: For some handles function, parameters change on target. Then some control events has different type arguments, this may cause a compilation error when one handle function, handles events with control which has this behavior.   Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1025.html'.
    Private Sub OptPoly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptPoly.CheckedChanged, OptExpo.CheckedChanged, OptSCU.CheckedChanged, OptSCUNL.CheckedChanged
        '=============================frm_LongPlotProEssential ======================
        'UPGRADE_WARNING: Event OptTrend.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
        'If eventSender.Checked Then
        '    Dim index As Short = OptTrend.GetIndex(eventSender)
        '------------------------------------------------------------------------------------------
        Dim Formula As String

        lblP1.Visible = False
        txtP1.Visible = False
        lblP2.Visible = False
        txtP2.Visible = False
        lblP3.Visible = False
        txtP3.Visible = False


        Select Case CType(CType(sender, System.Web.UI.WebControls.RadioButton).ID, String) 'index
            Case "OptPoly" '0 'polynomial
                lblP1.Text = "Degree of Polynomial (Integer)"
                Formula = "Y = C(0) + C(1)*t^1 + C(2)*t^2 + ...+ c(n)*t^n"
                Formula = Formula & Chr(10) & "where n is the degree of polynomial,"
                Formula = Formula & Chr(10) & "t is time and C(1), C(2) etc. are co-eficients"
                lblformula.Text = Formula

                txtP1.Text = Session("InpPOLY1").ToString

                lblP1.Visible = True
                txtP1.Visible = True




            Case "OptExpo" '1 'exponential
                lblP1.Text = "Initial Value, Y(0)"
                lblP2.Text = "Co-efficient of Exponent,a"
                Formula = "Y = Y(0)*EXP(a*t), where t is time"
                lblformula.Text = Formula

                txtP1.Text = VB6.Format(Session("InpEXPO1"), "0.000")
                txtP2.Text = Session("InpEXPO2").ToString

                lblP1.Visible = True
                txtP1.Visible = True
                lblP2.Visible = True
                txtP2.Visible = True


            Case "OptSCU" '2 's-curve non linear
                lblP1.Text = "Saturation or Carrying Capacity, K"

                Formula = "Y = K / [1+ a*exp(-bt)], where t is time,"
                Formula = Formula & Chr(10) & "and a and b are growth trajectory parameters"
                Formula = Formula & Chr(10) & "estimated from regression"
                lblformula.Text = Formula

                txtP1.Text = VB6.Format(Session("InpSLIN1"), "0.000")


                lblP1.Visible = True
                txtP1.Visible = True




            Case "OptSCUNL" '3 's-curve non linear
                lblP1.Text = "Saturation or Carrying Capacity, K"
                lblP2.Text = "Characteristic duration, TC"
                lblP3.Text = "Midpoint, TM"
                Formula = "Y = K / (1 + exp(-(Log(81) / TC) * (t - TM)))"
                Formula = Formula & Chr(10) & "t is time, TC is time for 10% to 90% growth"
                Formula = Formula & Chr(10) & "and TM is midpoint of growth trajectory"
                lblformula.Text = Formula

                txtP1.Text = VB6.Format(Session("InpSCUR1"), "0.000")
                txtP2.Text = Session("InpSCUR2").ToString
                txtP3.Text = Session("InpSCUR3").ToString


                lblP1.Visible = True
                txtP1.Visible = True
                lblP2.Visible = True
                txtP2.Visible = True
                lblP3.Visible = True
                txtP3.Visible = True


                'Y = b1 / (1# + exp(-(Log(81) / b2) * (X(k) - b3)))
        End Select



    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        'timeseries extrapolation
        Dim VS As String
        txtYearsAhead.Text = ""
        txtP1.Text = ""
        txtP2.Text = ""
        txtP3.Text = ""

        'make input values to default
        Session("InpYearsAhead") = DefYearsAhead
        Session("InpPOLY1") = DefPOLY1
        Session("InpEXPO1") = Session("DefEXPO1")
        Session("InpEXPO2") = Session("DefEXPO2")
        Session("InpSLIN1") = Session("DefSLIN1")
        Session("InpSCUR1") = Session("DefSCUR1")
        Session("InpSCUR2") = Session("DefSCUR2")
        Session("InpSCUR3") = Session("DefSCUR3")

        LoadMessage("All parameters cleared.", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        '------------------------------------------------------------------------------------------
        'timeseries extrapolation Jan 16, 2004
        Dim VS As String
        If txtYearsAhead.Text = "" Then
            If Not (OptPoly.Checked Or OptExpo.Checked Or OptSCU.Checked Or OptSCUNL.Checked) Then
                LoadMessage("Please choose any of the trend types.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If

            If Val(txtP1.Text) = 0 Then
                Select Case Trim(txtP1.Text)
                    Case "0", "0.0", "0.00", "0.000"
                    Case Else
                        txtP1.Text = ""
                        LoadMessage("Please put some number as parameter.", Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")

                End Select
            End If

            If txtP2.Visible Then
                If Val(txtP2.Text) = 0 Then
                    Select Case Trim(txtP2.Text)
                        Case "0", "0.0", "0.00", "0.000"
                        Case Else
                            txtP2.Text = ""
                            LoadMessage("Please put some number as parameter.", Session("Message"))
                            Support.FormManager.StateStore(Me)
                            Support.FormManager.Show(Me, "frm_Message.aspx")

                    End Select
                End If
            End If

            If txtP3.Visible Then
                If Val(txtP3.Text) = 0 Then
                    Select Case Trim(txtP3.Text)
                        Case "0", "0.0", "0.00", "0.000"
                        Case Else
                            txtP3.Text = ""
                            LoadMessage("Please put some number as parameter.", Session("Message"))
                            Support.FormManager.StateStore(Me)
                            Support.FormManager.Show(Me, "frm_Message.aspx")

                    End Select
                End If
            End If
        End If
        If Val(txtYearsAhead.Text) <= 0 Then
            txtYearsAhead.Text = "25"
            Session("InpYearsAhead") = DefYearsAhead
        Else
            If Session("FirstYear") + Val(txtYearsAhead.Text) > 2100 Then
                txtYearsAhead.Text = CStr(2100 - Session("FirstYear"))
                Session("InpYearsAhead") = Val(txtYearsAhead.Text)
            Else
                Session("InpYearsAhead") = Int(CDbl(txtYearsAhead.Text))
                txtYearsAhead.Text = CStr(Session("InpYearsAhead"))
            End If
        End If


        Select Case True
            Case OptPoly.Checked 'polynomial
                Session("InpPOLY1") = Int(CDbl(txtP1.Text))
                If Session("InpPOLY1") <= 0 Then

                    LoadMessage("Degree of polynomial can be integer only.", Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")

                Else
                    txtP1.Text = CStr(Session("InpPOLY1"))
                End If
                VS = "Polynomial "
            Case OptExpo.Checked 'exponential
                Session("InpEXPO1") = CDbl(txtP1.Text)
                Session("InpEXPO2") = CDbl(txtP2.Text)
                VS = "Exponential "
            Case OptSCU.Checked 's-curve
                Session("InpSLIN1") = CDbl(txtP1.Text)
                VS = "S-Curve "
            Case OptSCUNL.Checked 's-curve non linear
                Session("InpSCUR1") = CDbl(txtP1.Text)
                Session("InpSCUR2") = CDbl(txtP2.Text)
                Session("InpSCUR3") = CDbl(txtP3.Text)
                VS = "S-Curve (Non-Linear) "
            Case Else
                VS = "No "
        End Select
        If VS = "No " Then
            VS = VS & "parameters saved. Number of years to extrapolate is " & Session("InpYearsAhead") & "."
        Else
            VS = "Number of years to extrapolate and " & VS & "parameters saved. Redo the trend to see changes."
            'VS = VS + Carret
            'VS = VS & " They will be applied the next time "
            'VS = VS + Carret
            'VS = VS & " any form of extrapolation is chosen."
        End If

        'frameSetExtrapolate.Visible = False
        LoadMessage(VS, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub
End Class

'End Namespace

'=============================frm_LongPlotProEssential ======================
