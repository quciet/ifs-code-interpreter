'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''          JAM 2010-03-15 GList/Groups Selection
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   Copyright by Barry B. Hughes 1988-2003
'   Model code available for review and education only
'   Reproduction or incorporation in other software by permission only
'   Last revision by BBH on July 17, 2002 - added MTI's validation code
Option Strict Off
Option Explicit On

Partial Class frm_ScarVarGlist
    Inherits Page
    Dim ListSelected As Boolean
    Dim YearSelected As Boolean
    Dim db As String 'DAO.Database
    Dim table As DataTable 'DAO.TableDef
    Dim tbl As DataTable 'DAO.Recordset
    Dim valtbl As DataTable
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        If Not Page.IsPostBack Then


        End If

    End Sub
    Private Sub frm_ScarVarGlist_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ScarVarGlist_Load_Error 'JAM 2017-01-01

        'JAM 2016-09-19
        cmbYear.Visible = False
        Session("YearChosen") = 0
        YearSelected = False

        If Session("AddGlistMode") Then
            'JAM 2016-09-19  For Cohorts Variables Years selection isn't valid
            If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
                Call LoadTime()
                'JAM 2016-09-19
                ''If Session("vAddYear") Then
                cmbYear.Visible = True
                'cmbYear.SelectedIndex = Session("YearsLabels")(Session("TotMulGroups") - 1, 1)
                ''End If
            End If
        End If



        Dim i As Integer
        If Not Page.IsPostBack Then
            If Not Session("IsaGroup") Then
                If lstDim.Items.Count = 0 Then
                    db = Session("IFs_MDB2Active")
                    SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)

                    For i = 0 To valtbl.Rows.Count - 1
                        lstDim.Items.Add((valtbl.Rows(i).Item("Description")))
                    Next i
                    ListSelected = False
                End If
            Else    'JAM 2010-07-11 Addin groups
                If lstDim.Items.Count = 0 Then
                    db = Session("IFs_MDB2Active")
                    'jrs 2024/09/17 LongGroupName is empty for a few groups SetDataTablefromAccess(db, "SELECT DISTINCT * FROM GroupIDNewFull ORDER BY LongGroupName", "", valtbl)
                    SetDataTablefromAccess(db, "SELECT DISTINCT * FROM GroupIDNewFull ORDER BY GroupName", "", valtbl)

                    For i = 0 To valtbl.Rows.Count - 1
                        'jrs 2024/09/17 LongGroupName is empty for a few groups lstDim.Items.Add((valtbl.Rows(i).Item("LongGroupName")))
                        lstDim.Items.Add((valtbl.Rows(i).Item("GroupName")))
                    Next i
                    ListSelected = False
                End If



            End If
        Else 'jrs 2023/05/24
            ListSelected = Session("ListSelected")
        End If

        'JAM 2016-12-11
        If Session("AddingYear") Then
            lstDim.Visible = False
            'JAM 2017-01-31 Default First Year
            If cmbYear.SelectedIndex < 0 Then
                cmbYear.SelectedIndex = 0
            End If
        Else
            'jrs 2023/05/24 added and Year.count>0
            'JAM 2017-01-31 Default First Year
            If cmbYear.SelectedIndex < 0 And cmbYear.Items.Count > 0 Then
                cmbYear.SelectedIndex = 0
            End If

            'JAM 2017-01-31 Default First Year
            If lstDim.SelectedIndex < 0 And lstDim.Visible = True Then 'JAM 2017-02-03 Setting Default only Regions combo is displayed
                lstDim.SelectedIndex = 0
                'jrs 2023/05/24
                lstDim_SelectedIndexChanged(Me, New EventArgs)
            End If


        End If



        'For i = 0 To lstYears.Items.Count - 1

        '    'If lstYears.GetSelectedIndices(i) Then
        '    If lstYears.Items(i).Selected Then
        '        Session("YearsSel")(j) = i + 1 'lstYears.List(i)
        '        j = j + 1
        '    End If
        'Next i

        'For i = 0 To Session("NumYearsSel") - 1
        '    'lstYears.GetSelectedIndices(Session("YearsSel")(i) - 1) = True
        '    a = Session("YearsSel")(i)
        '    lstYears.Items(Session("YearsSel")(i) - 1).Selected = Session("YearsSel")(i)
        'Next i
        Exit Sub
ScarVarGlist_Load_Error:
        Exit Sub

    End Sub

    Protected Sub cmd_GListOpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_GListOpt.Click

        On Error GoTo cmd_GListOpt_Error 'JAM 2017-01-01

        Dim GCount, i As Integer
        'JAM 2010-07-18 Only it's a GList
        If ListSelected And Not Session("IsaGroup") Then
            db = Session("IFs_MDB2Active")

            'JAM 2016-12-11 Only adding year mode
            If lstDim.Text = "" Then
                SetDataTablefromAccess(db, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & Session("GListName") & """ ORDER BY Region", "", valtbl, DataPW)
            Else
                SetDataTablefromAccess(db, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & lstDim.Text & """ ORDER BY Region", "", valtbl, DataPW)
            End If


            For GCount = 0 To valtbl.Rows.Count - 1
                Session("GListMember")(GCount + 1) = (valtbl.Rows(GCount).Item("Region"))
                Session("GListIsGroup")(GCount + 1) = (valtbl.Rows(GCount).Item("Group"))

            Next GCount

            Session("IsaGroup") = False
            ''jam 2008-07-24
            'If FormOrig = "ScatVar" Then
            '    frm_ScatVarS.mnucountry.Checked = True
            '    frm_ScatVarS.mnuCodes.Checked = False
            '    frm_ScatVarS.mnuNolabels.Checked = False
            '    frm_ScatVarS.mnuGeographic.Checked = True
            '    frm_ScatVarS.mnuGroup.Checked = False
            '    frm_ScatVarS.mnuCountries.Checked = False

            'Else
            '    If FormOrig = "Scatter" Then
            '        frm_Scatter.mnuNames.Checked = True
            '        frm_Scatter.mnuCodes.Checked = False
            '        frm_Scatter.mnuNolabels.Checked = False
            '        frm_Scatter.mnuGeographic.Checked = True
            '        frm_Scatter.mnuGroup.Checked = False
            '        frm_Scatter.mnuCountries.Checked = False
            '    End If


            'End If


            'Else    'JAM 2008-04-27 Option added for using Groups

            '    rsGL = dbGL.OpenRecordset("SELECT NewRegion,Country FROM GroupIDNewFull a, GroupingFull b,RegionalizationFull c WHERE a.GroupNum = b.GroupNo AND b.RegionNo = c.NewRegion  AND a.LongGroupName = '" & lstDim.Text & "' ORDER BY a.LongGroupName")
            '    rsGL.MoveFirst()
            '    GCount = 1
            '    Do While (Not rsGL.EOF)
            '        GListMember(GCount) = rsGL.Fields("Country")
            '        rsGL.MoveNext()
            '        GCount = GCount + 1
            '    Loop
            '    IsaGroup = True
            '    'jam 2008-07-24
            '    If FormOrig = "ScatVar" Then
            '        frm_ScatVarS.mnucountry.Checked = True
            '        frm_ScatVarS.mnuCodes.Checked = False
            '        frm_ScatVarS.mnuNolabels.Checked = False
            '        frm_ScatVarS.mnuGeographic.Checked = False
            '        frm_ScatVarS.mnuGroup.Checked = True
            '        frm_ScatVarS.mnuCountries.Checked = False

            '    Else
            '        If FormOrig = "Scatter" Then
            '            frm_Scatter.mnuNames.Checked = True
            '            frm_Scatter.mnuCodes.Checked = False
            '            frm_Scatter.mnuNolabels.Checked = False
            '            frm_Scatter.mnuGeographic.Checked = False
            '            frm_Scatter.mnuGroup.Checked = True
            '            frm_Scatter.mnuCountries.Checked = False
            '        End If
            '    End If

            'End If

            If lstDim.Text <> "" Then 'JAM 2016-12-11
                Session("GlistCount") = GCount
                Session("GListName") = CStr(lstDim.Text)

                'JAM 2017-01-28  Keeping Original GList Value
                Session("GlistCountChosen") = GCount
            End If


            'jrs 2012/08/14
            If Session("mnuNamesChecked") = False Then
                'jrs 2011/12/23
                Session("mnuNamesChecked") = CBool(GCount < 21)
                Session("mnuNolabelsChecked") = Not Session("mnuNamesChecked")
                Session("mnuCodesChecked") = False
            End If


            ''JAM 2016-09-19 Resetting Total Glists Variable
            If Not Session("AddGlistMode") Then
                Session("TotMulGroups") = 1
            End If



            Session("IsaGlist") = True
            Session("GChanged") = True

            Dim a As String
            Dim b As String

            a = Session("YearChosen")
            b = Session("GListName")

            'JAM 2016-11-12
            If Session("AddingYear") Then
                Session("GChanged") = True

            End If






            Support.FormManager.Show(Me, Session("ScatVarGlistParent"))
        Else    'JAM 2010-07-11    Choosing a Group
            db = Session("IFs_MDB2Active")

            If lstDim.Text = "" Then 'JAM 2016-11-12
                SetDataTablefromAccess(db, "SELECT NewRegion,Country FROM GroupIDNewFull a, GroupingFull b,RegionalizationFull c WHERE a.GroupNum = b.GroupNo AND b.RegionNo = c.NewRegion  AND a.LongGroupName = """ & Session("GListName") & """ ORDER BY a.LongGroupName", "", valtbl, DataPW)

            Else
                SetDataTablefromAccess(db, "SELECT NewRegion,Country FROM GroupIDNewFull a, GroupingFull b,RegionalizationFull c WHERE a.GroupNum = b.GroupNo AND b.RegionNo = c.NewRegion  AND a.LongGroupName = """ & CStr(lstDim.Text) & """ ORDER BY a.LongGroupName", "", valtbl, DataPW)
            End If
            For GCount = 0 To valtbl.Rows.Count - 1
                Session("GListMember")(GCount + 1) = (valtbl.Rows(GCount).Item("Country"))
            Next GCount

            'JAM 2016-11-12
            If lstDim.Text <> "" Then
                Session("GlistCount") = GCount
                Session("GListName") = CStr(lstDim.Text)

                'JAM 2017-01-28  Keeping Original GList Value
                Session("GlistCountChosen") = GCount
            End If
            'jrs 2012/08/14
            If Session("mnuNamesChecked") = False Then
                'jrs 2011/12/23
                Session("mnuNamesChecked") = CBool(GCount < 21)
                Session("mnuNolabelsChecked") = Not Session("mnuNamesChecked")
                Session("mnuCodesChecked") = False
            End If

            Session("IsaGroup") = True
            Session("IsaGlist") = True
            Session("GChanged") = True

            ''JAM 2016-09-19 Resetting Total Glists Variable
            If Not Session("AddGlistMode") Then
                Session("TotMulGroups") = 1
            End If

            Dim a As String
            Dim b As String

            a = Session("YearChosen")
            b = Session("GListName")

            'JAM 2016-12-11
            If Session("AddingYear") Then
                Session("GChanged") = True

            End If




            Support.FormManager.Show(Me, Session("ScatVarGlistParent"))
            'If Not AddGlistMode Then
            '    msg$ = "You must select a G-List or press the continue Button"
            'Else
            '    msg$ = "You must select a G-List and Year or press the continue Button"
            'End If
            'MsgBox(msg$)
            'Screen.MousePointer = 0

        End If

        Exit Sub
cmd_GListOpt_Error:
        Exit Sub

    End Sub

    Protected Sub cmd_GListExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_GListExit.Click
        ' Dim LineSet As Integer
        On Error GoTo cmd_GListExit_Error 'JAM 2017-01-01

        If Not Session("IsaGlist") And Not Session("IsaGroup") Then
            'If FormOrig = "Scatter" Then

            '    frm_Scatter.mnuCountries.Checked = True
            '    frm_Scatter.mnuNames.Checked = True
            '    frm_Scatter.mnuCodes.Checked = False
            '    frm_Scatter.mnuNolabels.Checked = False
            '    frm_Scatter.mnuGeographic.Checked = False
            '    frm_Scatter.mnuGroup.Checked = False


            'Else
            '    If FormOrig = "ScatVar" Then
            '        frm_ScatVarS.mnuCountries.Checked = True
            '        frm_ScatVarS.mnucountry.Checked = True
            '        frm_ScatVarS.mnuCodes.Checked = False
            '        frm_ScatVarS.mnuNolabels.Checked = False
            '        frm_ScatVarS.mnuGeographic.Checked = False
            '        frm_ScatVarS.mnuGroup.Checked = False
            '    End If
            'End If
            'OptLabel = "Country Names"

            '            IsaGlist = False
            '            IsaGroup = False
            Session("CCount") = Session("TotCountries")

            ''''''''''''''''
            'For LineSet% = 1 To CCount
            '    CVarName$(LineSet%) = CStr(CVarCounName$(LineSet%))
            '    CVarCode$(LineSet%) = CStr(CVarCounCode$(LineSet%)) 'Code Added by Jaime
            '   CVarAlias(LineSet) = ""
            'Next LineSet%

        Else
            If Not Session("IsaGroup") Then
                'If FormOrig = "Scatter" Then

                '    frm_Scatter.mnuCountries.Checked = False
                '    frm_Scatter.mnuNames.Checked = False
                '    frm_Scatter.mnuCodes.Checked = False
                '    frm_Scatter.mnuNolabels.Checked = False
                '    frm_Scatter.mnuGeographic.Checked = True
                '    frm_Scatter.mnuGroup.Checked = False


                'Else
                '    If FormOrig = "ScatVar" Then
                '        frm_ScatVarS.mnuCountries.Checked = False
                '        frm_ScatVarS.mnucountry.Checked = False
                '        frm_ScatVarS.mnuCodes.Checked = False
                '        frm_ScatVarS.mnuNolabels.Checked = False
                '        frm_ScatVarS.mnuGeographic.Checked = True
                '        frm_ScatVarS.mnuGroup.Checked = False
                '    End If
                'End If
                'IsaGroup = False
                'Else
                '    If FormOrig = "Scatter" Then

                '        frm_Scatter.mnuCountries.Checked = False
                '        frm_Scatter.mnuNames.Checked = False
                '        frm_Scatter.mnuCodes.Checked = False
                '        frm_Scatter.mnuNolabels.Checked = False
                '        frm_Scatter.mnuGeographic.Checked = False
                '        frm_Scatter.mnuGroup.Checked = True


                '    Else
                '        If FormOrig = "ScatVar" Then
                '            frm_ScatVarS.mnuCountries.Checked = False
                '            frm_ScatVarS.mnucountry.Checked = False
                '            frm_ScatVarS.mnuCodes.Checked = False
                '            frm_ScatVarS.mnuNolabels.Checked = False
                '            frm_ScatVarS.mnuGeographic.Checked = False
                '            frm_ScatVarS.mnuGroup.Checked = True
                '        End If
                '    End If
                'IsaGroup = True
            End If

        End If
        'frm_Scatter.mnuGeographic.Checked = False

        Session("GChanged") = False

        Support.FormManager.Show(Me, Session("ScatVarGlistParent"))

        Exit Sub
cmd_GListExit_Error:  'JAM 2017-01-01
        Exit Sub

    End Sub

    Protected Sub lstDim_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDim.SelectedIndexChanged
        On Error GoTo lstDim_SelectedIndex_Error 'JAM 2017-01-01

        If Not Session("AddGlistMode") Then 'JAM 2016-09-04

            ListSelected = True
        Else
            If Not Session("IsCohDep") And Not Session("IsCoh1Ind") And Not Session("IsCoh2Ind") Then
                cmbYear.Visible = True

                ListSelected = True 'JAM 2016-12-26 
            Else
                ListSelected = True

            End If

        End If
        'jrs 2023/05/24
        Session("ListSelected") = ListSelected

        Exit Sub
lstDim_SelectedIndex_Error:
        Exit Sub
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        On Error GoTo Page_Unload_Error 'JAM 2017-01-01
        'JAM 2010-07-18
        If Session("IsaGlist") And Not Session("IsaGroup") Then
            Session("mnuGroupChecked") = False
            Session("mnuGeographicChecked") = True
            Session("mnuCountriesChecked") = False
        Else
            If Session("IsaGroup") Then
                Session("mnuGroupChecked") = True
                Session("mnuGeographicChecked") = False
                Session("mnuCountriesChecked") = False
            Else
                Session("mnuGroupChecked") = False
                Session("mnuGeographicChecked") = False
                Session("mnuCountriesChecked") = True
            End If
        End If

        'JAM 2016-12-11
        Session("AddingYear") = False

        Exit Sub
Page_Unload_Error:
        Exit Sub
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2016-09-04      Loading Years ComboBox
    '
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub LoadTime()

        On Error GoTo LoadTime_Error 'JAM 2017-01-01
        Dim i As Integer

        'cmbYear.SelText = ""
        'cmbYear.SelectedValue =

        If Session("GLbLagDep")(1) = Session("GLbLagInd")(1) Then
            For i = 0 To UBound(Session("GLbLagDep"))
                If Not IsNothing(Session("GLbLagDep")(i)) And Session("GLbLagDep")(i) <> "0" Then 'JAM 2017-01-01
                    cmbYear.Items.Add((Session("GLbLagDep")(i)))

                End If
            Next i
        Else
            For i = 0 To UBound(Session("GLbLagDep"))
                If Not IsNothing(Session("GLbLagDep")(i)) And Session("GLbLagDep")(i) <> "0" And Not IsNothing(Session("GLbLagInd")(i)) And Session("GLbLagInd")(i) <> "0" And UCase(Session("GLbLagDep")(i)) <> "MOSTRECENT" And UCase(Session("GLbLagDep")(i)) <> "EARLIEST" And UCase(Session("GLbLagInd")(i)) <> "MOSTRECENT" And UCase(Session("GLbLagInd")(i)) <> "EARLIEST" Then
                    cmbYear.Items.Add(Session("GLbLagDep")(i) + "," + Session("GLbLagInd")(i))
                End If
            Next i

        End If

        'JAM 2016-09-04    Default value for combo
        'cmbYear.ListIndex = Session("YearsLabels")(Session("TotMulGroups") - 1, 1)
        'cmbYear.SelectedIndex = Session("YearsLabels")(Session("TotMulGroups") - 1, 1)

        Exit Sub
LoadTime_Error:
        Exit Sub

    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''             JAM 2016-09-17 Adding code for choosing year
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmbYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbYear.SelectedIndexChanged
        Dim m As Integer
        Dim msg As String

        On Error GoTo cmbYear_Error 'JAM 2017-01-01

        ListSelected = True
        YearSelected = True
        For m = 0 To Session("TotMulGroups") - 1
            'JAM 2016-09-17  Both conditions must be OK
            If lstDim.Text = Session("GLNameJoinPrev")(m) And cmbYear.SelectedIndex = Session("YearsLabels")(m, 1) Then
                ListSelected = False
                YearSelected = False
                Exit For
            End If
        Next m


        'JAM 2016-09-17 Adding Year Validation

        If Not ListSelected And Not YearSelected Then
            If Session("IsaGlist") And Not Session("IsaGroup") Then
                msg = "This G-List and Year have already been chosen, please select another G-List"
            Else
                msg = "This Group and Year have already been chosen, please select another Group"
            End If

            'JAM 2016-11-28
            'MsgBox(msg)

        Else
            Session("YearChosen") = cmbYear.SelectedIndex

            ListSelected = True
            YearSelected = True

        End If
        'cmbYear.Visible = False

        Exit Sub
cmbYear_Error:
        Exit Sub

    End Sub
End Class

