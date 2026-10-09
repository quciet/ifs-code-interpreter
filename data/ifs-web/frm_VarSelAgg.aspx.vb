Imports System.Data.SQLite

Partial Class frm_VarSelAgg
    Inherits Page


    Protected Sub cmdAggSelect_Click(sender As Object, e As EventArgs) Handles cmdAggSelect.Click
        If lstAgg.SelectedIndex >= 0 Then
            Dim dbAgg = New SQLiteConnection(CType("DataSource=" & Session("PutFdrvRun") + "datadict.DB;", String))
            dbAgg.Open()
            Dim dbAggCmd = dbAgg.CreateCommand

            Dim sAgg = lstAgg.SelectedValue
            If Session("SEL") > 0 And Not Session("DoingHistory") Then
                VAggregation(Session("SEL")) = lstAgg.SelectedValue
                'jrs 2025/03/10 added andAlso to avoid error when checking on parameters that have historicanalog = NUll, like hlstuntchlm
                'check if doing history plus forecast
                If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" AndAlso VHistoricAnalog(Session("SEL")) > "" Then
                    If Not Session("bMatchFH") And lstHist.SelectedIndex >= 0 Then
                        sAgg = lstHist.SelectedValue
                    End If
                    'Single variable
                    If UCase(VHistoricAnalog(Session("SEL"))) <> "IFSVARHISTANA" And UCase(VHistoricAnalog(Session("SEL"))) <> "FORMULA" And
                        InStr(VHistoricAnalog(Session("SEL")), ",") = 0 Then
                        dbAggCmd.CommandText = "UPDATE DataDict SET Aggregation = """ & sAgg & """ WHERE Variable = """ & Session("HistVar") & """"
                    Else
                        dbAggCmd.CommandText = "UPDATE DataDict SET Aggregation = """ & sAgg & """ WHERE Variable IN (" & Session("sListAgg") & ")"
                    End If
                    dbAggCmd.ExecuteNonQuery()
                    dbAgg.Close()
                End If
            Else
                'Update ifsVar
                dbAggCmd.CommandText = "UPDATE DataDict SET Aggregation = """ & sAgg & """ WHERE Variable = """ & Session("sVar") & """"
                dbAggCmd.ExecuteNonQuery()
                dbAgg.Close()
            End If
        End If
        Support.FormManager.Show(Me, "frm_VarSel.aspx")
    End Sub

    Private Sub frm_VarSelAgg_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'jrs 2011/01/18 changed form
            lstAgg.Items.Add("SUM")
            lstAgg.Items.Add("AVG")
            'jrs 2011/01/18
            lstHist.Items.Add("SUM")
            lstHist.Items.Add("AVG")
            'Load aggregation rules
            Dim AggRule As myAggRule
            Dim i As Integer
            For Each AggRule In IFsAggRules.colAggRules
                lstAgg.Items.Add(AggRule.AggName)
                'jrs 2011/01/18
                lstHist.Items.Add(AggRule.AggName)
            Next

            For i = 0 To lstAgg.Items.Count - 1
                If lstAgg.Items(i).Text = Session("sAgg") Then
                    lstAgg.Items(i).Selected = True
                    If Session("bMatchFH") Then
                        lstHist.Items(i).Selected = True
                    Else
                        lstHist.Items(i).Selected = False
                    End If
                ElseIf lstHist.Items(i).Text = Session("sAggH") Then
                    lstHist.Items(i).Selected = True
                Else
                    lstAgg.Items(i).Selected = False
                    lstHist.Items(i).Selected = False
                End If
            Next i

            lblMixed.Visible = Session("lblMixed")
            lstHist.Visible = Session("lstHistVisible")
            lblHis.Visible = Session("lstHistVisible")
        End If
    End Sub

    Private Sub cmdAggCancel_Click(sender As Object, e As EventArgs) Handles cmdAggCancel.Click
        Support.FormManager.Show(Me, "frm_VarSel.aspx")
    End Sub
End Class
