Option Strict Off
Option Explicit On

Partial Class frm_ForecastGroupAggregation
    Inherits Page

    Private Sub frm_ForecastGroupAggregation_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'jrs 2023/09/08
            lstAgg.Items.Clear()
            lstHist.Items.Clear()

            lstAgg.Items.Add("SUM")
            lstAgg.Items.Add("AVG")
            'jrs 2011/01/18
            lstHist.Items.Add("SUM")
            lstHist.Items.Add("AVG")
            'Load aggregation rules
            Dim AggRule As myAggRule
            For Each AggRule In IFsAggRules.colAggRules
                lstAgg.Items.Add(AggRule.AggName)
                'jrs 2011/01/18
                lstHist.Items.Add(AggRule.AggName)
            Next
            'Dim dbAgg As Database
            Dim rsAgg As DataTable
            Dim rsAggIdx As Integer
            'Select current one
            Dim i As Integer, sAgg As String, sAggH As String
            Dim SEL As Integer, DoingHistory As Boolean
            SEL = Session("SEL")
            DoingHistory = Session("DoingHistory")
            Dim sVar As String
            sVar = Session("sVar")

            Dim rs As DataTable
            rs = Session("PackagedDispRs")

            If SEL > 0 And Not DoingHistory Then
                sAgg = VAggregation(SEL)
                'jrs 2025/11/03 added and Not IsDBNull(VHistoricAnalog(SEL))
                'GDPR for example doesn't have historic analog
                If rs.Rows(0).Item("HistoryplusForecast") And Not IsDBNull(VHistoricAnalog(SEL)) Then
                    Dim sList As String
                    Dim HistVar As String
                    HistVar = VHistoricAnalog(SEL)
                    'Read historic option too
                    'Single variable
                    If UCase(HistVar) <> "IFSVARHISTANA" And UCase(HistVar) <> "FORMULA" And InStr(HistVar, ",") = 0 Then
                        'jrs 2025/04/25
                        'Considering Bilateral
                        If VDim1(SEL) = 1 And VDim2(SEL) = 1 Then
                            SetDataTablefromAccess(Session("PutFdrvRun") + "datadictDyadic.db", "SELECT Aggregation FROM DataDict WHERE Variable = """ & HistVar & """", "", rsAgg)
                        Else
                            SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.db", "SELECT Aggregation FROM DataDict WHERE Variable = """ & HistVar & """", "", rsAgg)
                        End If
                    ElseIf InStr(HistVar, ",") > 0 Then 'Using commas to separate multiple dimensions
                        Dim x
                        x = Split(HistVar, ",")
                        sList = ""
                        For i = 0 To UBound(x)
                            HistVar = Trim(x(i))
                            sList = sList & """" & HistVar & """"
                            If i < UBound(x) Then
                                sList = sList & ","
                            End If
                        Next i
                        SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.db", "SELECT DISTINCT Aggregation FROM DataDict WHERE Variable IN (" & sList & ")", "", rsAgg)
                    ElseIf UCase(HistVar) = "IFSVARHISTANA" Then
                        Dim j As Integer, Total3 As Integer
                        If VDim3(SEL) > 0 Then
                            Total3 = MAXCOM(VDim3(SEL)) - 1
                        Else
                            Total3 = 1
                        End If
                        sList = ""
                        For i = 1 To MAXCOM(VDim2(SEL)) - 1
                            For j = 1 To Total3
                                HistVar = IFsHistAnalogs.Item(sVar, GRPS(VDim2(SEL), i), GRPS(VDim3(SEL), j)).sHistAna
                                sList = sList & """" & HistVar & """"
                                If i < MAXCOM(VDim2(SEL)) - 1 Or j < Total3 Then
                                    sList = sList & ","
                                End If
                            Next j
                        Next i
                        SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.db", "SELECT DISTINCT Aggregation FROM DataDict WHERE Variable IN (" & sList & ")", "", rsAgg)
                    ElseIf UCase(HistVar) = "FORMULA" Then
                        SetDataTablefromAccess(Session("PutFdrvRun") + "ifsVar.db", "SELECT VarName FROM IFSVARHISTANAFORMULASDET WHERE ForVarName = """ & sVar & """", "", rsAgg)
                        sList = ""
                        rsAggIdx = 0
                        Do While rsAggIdx < rsAgg.Rows.Count
                            HistVar = rsAgg.Rows(rsAggIdx).Item("VarName")
                            sList = sList & """" & HistVar & """"
                            rsAggIdx = rsAggIdx + 1
                            If rsAggIdx < rsAgg.Rows.Count Then
                                sList = sList & ","
                            End If
                        Loop
                        SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.db", "SELECT DISTINCT Aggregation FROM DataDict WHERE Variable IN (" & sList & ")", "", rsAgg)
                    End If

                    lstHist.Visible = True
                    lblHis.Visible = True

                    Session("HistVar") = HistVar
                    Session("sList") = sList

                    'Different Agg rules
                    If rsAgg.Rows.Count > 1 Then
                        lblMixed.Visible = True
                        Session("bMatchFH") = False
                        sAggH = ""
                    ElseIf rsAgg.Rows(0).Item("Aggregation") = sAgg Then
                        sAggH = sAgg
                        Session("bMatchFH") = True
                    Else
                        sAggH = rsAgg.Rows(0).Item("Aggregation")
                    End If
                End If 'Doing history plus forecast
            Else 'Doing History
                'Read ifsVar
                SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.db", "SELECT Aggregation FROM DataDict WHERE Variable = """ & sVar & """", "", rsAgg)
                sAgg = rsAgg.Rows(0).Item("Aggregation")
            End If
            'jrs 2023/09/08
            lstAgg.SelectedIndex = -1
            lstHist.SelectedIndex = -1

            For i = 0 To lstAgg.Items.Count - 1
                If lstAgg.Items(i).Text = sAgg Then
                    lstAgg.Items(i).Selected = True
                    If Session("bMatchFH") Then
                        lstHist.Items(i).Selected = True
                    Else
                        lstHist.Items(i).Selected = False
                    End If
                ElseIf lstHist.Items(i).Text = sAggH Then
                    lstHist.Items(i).Selected = True
                Else
                    lstAgg.Items(i).Selected = False
                    lstHist.Items(i).Selected = False
                End If
            Next i
            If lstAgg.SelectedIndex = -1 Then
                Dim sMsg = "<script>alert('Variable doesn't have any Aggregation rule associated with it.');</script>"
                Response.Write(sMsg)
            End If

        End If
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Session("frm_ForecastGroupAggregation.sAgg") = ""
        'Support.FormManager.Close(Me)
        Support.FormManager.Show(Me, Session("GroupAggrParent"))
    End Sub

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Session("frm_ForecastGroupAggregation.sAgg") = lstAgg.Items(lstAgg.SelectedIndex).Text

        If Session("frm_ForecastGroupAggregation.sAgg") > "" Then
            Dim SEL As Integer, DoingHistory As Boolean
            Dim sAgg As String = Session("frm_ForecastGroupAggregation.sAgg")

            SEL = Session("SEL")
            DoingHistory = Session("DoingHistory")

            If SEL > 0 And Not DoingHistory Then
                VAggregation(SEL) = sAgg
                Dim rs As DataTable
                rs = Session("PackagedDispRs")
                'check if doing history plus forecast
                'jrs 2024/01/08 handling nulls in historic analog field
                If rs.Rows(0).Item("HistoryplusForecast") = 1 And If(IsDBNull(VHistoricAnalog(SEL)), "", VHistoricAnalog(SEL)) > "" Then
                    If Not Session("bMatchFH") And lstHist.SelectedIndex >= 0 Then
                        sAgg = lstHist.Items(lstHist.SelectedIndex).Text
                    End If
                    'Single variable
                    If UCase(VHistoricAnalog(SEL)) <> "IFSVARHISTANA" And UCase(VHistoricAnalog(SEL)) <> "FORMULA" And
                        InStr(VHistoricAnalog(SEL), ",") = 0 Then
                        'jrs 2025/04/22
                        If VDim1(SEL) = 1 And VDim2(SEL) = 1 Then
                            updateAccessWith1Command(Session("PutFdrvRun") + "datadictDyadic.db", "UPDATE DataDict SET Aggregation = """ & sAgg & """ WHERE Variable = """ & Session("HistVar") & """")
                        Else
                            updateAccessWith1Command(Session("PutFdrvRun") + "datadict.db", "UPDATE DataDict SET Aggregation = """ & sAgg & """ WHERE Variable = """ & Session("HistVar") & """")
                        End If
                    Else
                        updateAccessWith1Command(Session("PutFdrvRun") + "datadict.db", "UPDATE DataDict SET Aggregation = """ & sAgg & """ WHERE Variable IN (" & Session("sList") & ")")
                    End If
                End If
            Else
                'Update ifsVar
                updateAccessWith1Command(Session("PutFdrvRun") + "datadict.db", "UPDATE DataDict SET Aggregation = """ & sAgg & """ WHERE Variable = """ & Session("sVar") & """")
            End If
            'jrs 2023/09/08
            SyncLock IFsTables
                If IFsTables.Contains("SELECT * FROM DataDict") Then
                    IFsTables.Remove("SELECT * FROM DataDict")
                End If
            End SyncLock

        End If

        'Support.FormManager.Close(Me)
        Support.FormManager.Show(Me, Session("GroupAggrParent"))
    End Sub

    Private Sub lstAgg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAgg.SelectedIndexChanged
        If lstHist.Visible And Session("bMatchFH") Then
            'jrs 2023/09/08
            lstHist.SelectedIndex = -1
            lstHist.Items(lstAgg.SelectedIndex).Selected = True
        End If
    End Sub
End Class

