Partial Class frm_SDGEditTarget
    Inherits Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("SDGEditTargetParent") = "frm_SDGset.aspx" Then
                optAbsolute.Checked = Not Session("TargetIsRelative")(Session("rowidx"))
                optRelative.Checked = Session("TargetIsRelative")(Session("rowidx"))
                If IsDBNull(Session("Target")(Session("rowidx"))) Then
                    txtTargetVal.Text = ""
                Else
                    If Session("Target")(Session("rowidx")) <> -9999 Then
                        txtTargetVal.Text = Session("Target")(Session("rowidx"))
                    Else
                        txtTargetVal.Text = ""
                    End If
                End If
                If Not IsDBNull(Session("TargetYear")(Session("rowidx"))) Then
                    If Session("TargetYear")(Session("rowidx")) > Session("FirstYear") Then
                        txtTargetYear.Text = Session("TargetYear")(Session("rowidx"))
                    Else
                        txtTargetYear.Text = 2030
                    End If
                Else
                    txtTargetYear.Text = 2030
                End If
                'frm_SDGEditTarget.sIndicatorName = IndicatorName(iIndex)
                chkValence.Checked = Session("Valence")(Session("rowidx"))
                Session("MinTargetValEdit") = Session("VarMins")(Session("rowidx"))
                Session("MaxTargetValEdit") = Session("VarMaxs")(Session("rowidx"))
            Else
                optAbsolute.Checked = Not Session("SDGIndicator_TargetValueIsRelative")(Session("SDGiIndex"))
                optRelative.Checked = Session("SDGIndicator_TargetValueIsRelative")(Session("SDGiIndex"))
                If IsDBNull(Session("SDGIndicator_TargetValue")(Session("SDGiIndex"))) Then
                    txtTargetVal.Text = ""
                Else
                    If Session("SDGIndicator_TargetValue")(Session("SDGiIndex")) <> -9999 Then
                        txtTargetVal.Text = Session("SDGIndicator_TargetValue")(Session("SDGiIndex"))
                    Else
                        txtTargetVal.Text = ""
                    End If
                End If
                If Not IsDBNull(Session("SDGIndicator_TargetYear")(Session("SDGiIndex"))) Then
                    If Session("SDGIndicator_TargetYear")(Session("SDGiIndex")) > Session("FirstYear") Then
                        txtTargetYear.Text = Session("SDGIndicator_TargetYear")(Session("SDGiIndex"))
                    Else
                        txtTargetYear.Text = 2030
                    End If
                Else
                    txtTargetYear.Text = 2030
                End If
                'frm_SDGEditTarget.sIndicatorName = IndicatorName(iIndex)
                chkValence.Checked = Session("SDGIndicator_Valence")(Session("SDGiIndex"))
                Session("MinTargetValEdit") = Session("SDGIndicator_Min")(Session("SDGiIndex"))
                Session("MaxTargetValEdit") = Session("SDGIndicator_Max")(Session("SDGiIndex"))
            End If
        End If

        'RangeValidatorTargetYear.MinimumValue = FirstYearForecast + 1
        'RangeValidatorTargetYear.MaximumValue = Session("LastYearForecast")
        'RangeValidatorTargetYear.ErrorMessage = "Target Year needs to be between " & CStr(FirstYearForecast + 1) & " and " & Session("LastYearForecast")

        If IsNumeric(Session("MaxTargetValEdit")) Then
            txtMax.Value = Session("MaxTargetValEdit")
        Else
            txtMax.Value = 0
        End If
        If IsNumeric(Session("MinTargetValEdit")) Then
            txtMin.Value = Session("MinTargetValEdit")
        Else
            txtMin.Value = 0
        End If
        txtFirstYear.Value = FirstYearForecast + 1
        txtLastYear.Value = Session("LastYearForecast")

        'RangeValidatorTargetVal.MinimumValue = Session("MinTargetValEdit")
        'RangeValidatorTargetVal.MaximumValue = Session("MaxTargetValEdit")
        'RangeValidatorTargetVal.ErrorMessage = "Target Value needs to be between " & Session("MinTargetValEdit") & " and " & Session("MaxTargetValEdit")

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        'Validate values
        If txtTargetVal.Text = "" Then
            txtTargetVal.Text = "-9999"
        End If
        'If Not IsNumeric(txtTargetVal.Text) Then
        '    MsgBox("Target Value needs to be a numerical value", vbCritical, "SDG Edit Target")
        '    Exit Sub
        'End If
        'If Not IsNumeric(txtTargetYear.Text) Then
        '    MsgBox("Target Year needs to be a numerical value", vbCritical, "SDG Edit Target")
        '    Exit Sub
        'End If
        'txtTargetYear.Text = CInt(txtTargetYear.Text)
        'If Val(txtTargetYear.Text) < FirstYearForecast + 1 Or Val(txtTargetYear.Text) > LastYearForecast Then
        '    MsgBox("Target Year needs to be between " & CStr(FirstYearForecast + 1) & " and " & CStr(LastYearForecast), vbCritical, "SDG Edit Target")
        '    Exit Sub
        'End If
        'Save changes to arrays
        Session("SDGIndicator_TargetValueIsRelative")(Session("SDGiIndex")) = optRelative.Checked
        If txtTargetVal.Text = "-9999" Or txtTargetVal.Text = "" Then
            Session("SDGIndicator_TargetValue")(Session("SDGiIndex")) = DBNull.Value
        Else
            Session("SDGIndicator_TargetValue")(Session("SDGiIndex")) = Val(txtTargetVal.Text)
        End If
        Session("SDGIndicator_TargetYear")(Session("SDGiIndex")) = txtTargetYear.Text
        'frm_SDGEditTarget.sIndicatorName = IndicatorName(iIndex)
        Session("SDGIndicator_Valence")(Session("SDGiIndex")) = chkValence.Checked
        'frm_SDG.ChangeTargetVal(Val(txtTargetVal.Text), CInt(txtTargetYear.Text), bRelative, bValence)
        'frm_SDG.NeedFileWrite = True

        'If Session("SDGEditTargetParent") = "frm_SDGset.aspx" Then
        'jrs 2017/09/07
        If Session("SDGHistoryOnly") Is Nothing Then
            Session("SDGHistoryOnly") = False
        End If

        If Session("SDGHistoryOnly") Then
            Session("TargetIsRelativeH")(Session("rowidx")) = optRelative.Checked
            If txtTargetVal.Text = "-9999" Or txtTargetVal.Text = "" Then
                Session("TargetH")(Session("rowidx")) = DBNull.Value
            Else
                Session("TargetH")(Session("rowidx")) = txtTargetVal.Text
            End If
            Session("TargetYearH")(Session("rowidx")) = txtTargetYear.Text
            Session("ValenceH")(Session("rowidx")) = chkValence.Checked

        Else
            Session("TargetIsRelative")(Session("rowidx")) = optRelative.Checked
            If txtTargetVal.Text = "-9999" Or txtTargetVal.Text = "" Then
                Session("Target")(Session("rowidx")) = DBNull.Value
            Else
                Session("Target")(Session("rowidx")) = txtTargetVal.Text
            End If
            Session("TargetYear")(Session("rowidx")) = txtTargetYear.Text
            Session("Valence")(Session("rowidx")) = chkValence.Checked
        End If
        'End If

        Support.FormManager.Show(Me, Session("SDGEditTargetParent"))
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Support.FormManager.Show(Me, Session("SDGEditTargetParent"))
    End Sub
End Class
