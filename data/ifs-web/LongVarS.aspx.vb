Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility 'mtiwebforms April 2004 ADO.Net



Partial Class frm_LongVarS
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        InitializeComponent()
        'cmb_VarName.EnableViewState = False
        'JAM 2011-11-19 Defining default button for txtVarSearch field
        txtVarSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + btnInvs.ClientID + "')")

        'JAM 2013-11-16
        cmb_VCAT.Attributes.Add("onchange", "cmb_VCAT_click();")

        'JAM 2013-11-16
        cmb_OthFilt.Attributes.Add("onchange", "cmb_OthFilt_click();")


    End Sub

    Protected WithEvents WinFormsToWeb_FocusControl As ArtinSoft.WinFormsToWeb.WebControls.FocusControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        dataString = ""

    End Sub
#End Region

    Dim VarGroupNames(100) As String
    Dim VarNames() As String
    Dim VarNameNum As Object
    Dim dataString As String
    Dim GroupName As String
    'jrs 2012/02/28
    Dim sCoVaTra As String

    Dim sCurrentDisplay As String
    Dim iCurrentIdx As Short
    Dim bLastIndep As Boolean
    'JRS 11/2/01 GROUPING
    'mti WebForm Changes From July 02, 2004
    Dim CountryGroup(200, 200) As String
    'mti WebForm Changes From July 02, 2004

    'mti WebForm Changes From July 04, 2004 -start
    Dim GroupTot(200) As Short
    'Dim sAggregationI As String 'changed to Session("SC_sAggregationI")
    'Dim sAggregationD AS String ' changed to Session("SC_sAggregationD") 
    'mti WebForm Changes From July 04, 2004
    'mtiwebform June 2004 Longitudinal
    Dim db As String
    Dim Valtbl As DataTable
    'mti WebForm Changes From July 02, 2004
    Dim cgcount As Short
    'jrs 2005/04/06
    Dim bInd As Boolean
    'JRS 11/17/02
    'JRS 01/29/04

    '   'mtiwebform June 2004 Longitudinal - take declarations to global
    'Dim Session("PlotLong")(0, 0) As Object
    'Dim Session("PlotLabels")(0, 0) As String

    'Dim Session("PlotSize") As Short
    '   Dim sUnits As String
    '   'end mti webform June 2004 Longitudinal - take declarations to global


    '=============================LongVarS.Frm==============================
    'UPGRADE_WARNING: Event chk_ControlOff.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub chk_ControlOff_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chk_ControlOff.CheckedChanged
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        opt_Dependent.Checked = True
        opt_Independent.Checked = False
        opt_Control.Checked = False
        lbl_Cont.Text = "None"
        Session("VarNameCon") = ""
        Msg = "Select Dependent Variable:"
        lbl_VarName.Text = Msg
        chk_ControlOff.Checked = False
        chk_ControlOff.Visible = False
        'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

    End Sub

    Private Sub Chk_Indep_CheckStateChanged()
        Dim Msg As String

        lbl_Independ.Visible = False
        lbl_Ind.Visible = False
        Session("LV_lbl_Ind") = "TIME"
        opt_Independent.Visible = False
        lbl_Ind.Text = Session("LV_lbl_Ind")
        lbl_Dep.Text = Session("LV_lbl_Dep")
        opt_Independent.Checked = False
        Msg = "Select Dependent Variable:"
        lbl_VarName.Text = Msg
        opt_Dependent.Checked = True

        cmdTable.Enabled = True


        If Session("VarComp") = 3 Then
            Session("VarComp") = 1
        End If

        'jrs 2010/09/02
        Session("LV_Chk_Indep") = 1  'Chk_Indep.Checked
        Session("LV_Chk_Points") = 0 'chk_Points.Checked
        Session("LV_Chk_Time") = 0 'Chk_time.Checked

    End Sub

    '=============================LongVarS.Frm==============================
    'UPGRADE_WARNING: Event chk_Points.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub chk_Points_CheckStateChanged()


        lbl_Independ.Visible = True
        lbl_Ind.Visible = True
        opt_Independent.Visible = True
        opt_Independent.Checked = True
        opt_Dependent.Checked = False
        If UCase(lbl_Ind.Text) = "TIME" Then
            Session("LV_lbl_Ind") = "None"
        End If

        If Session("LV_lbl_Dep") <> Nothing Then
            lbl_Dep.Text = Session("LV_lbl_Dep")
        End If
        If Session("LV_lbl_Ind") <> Nothing Then
            lbl_Ind.Text = Session("LV_lbl_Ind")
        End If
        cmdTable.Enabled = False

        'jrs 2010/09/02
        Session("LV_Chk_Indep") = 0  'Chk_Indep.Checked
        Session("LV_Chk_Points") = 1 'chk_Points.Checked
        Session("LV_Chk_Time") = 0 'Chk_time.Checked

    End Sub
    '=============================LongVarS.Frm==============================
    'UPGRADE_WARNING: Event Chk_time.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Chk_time_CheckStateChanged()

        lbl_Independ.Visible = True
        lbl_Ind.Visible = True
        opt_Independent.Visible = True
        opt_Independent.Checked = True
        opt_Dependent.Checked = False
        If UCase(lbl_Ind.Text) = "TIME" Then
            Session("LV_lbl_Ind") = "None"
        End If
        lbl_Dep.Text = Session("LV_lbl_Dep")
        lbl_Ind.Text = Session("LV_lbl_Ind")
        cmdTable.Enabled = False

        'jrs 2010/09/02
        Session("LV_Chk_Indep") = 0  'Chk_Indep.Checked
        Session("LV_Chk_Points") = 0 'chk_Points.Checked
        Session("LV_Chk_Time") = 1 'Chk_time.Checked

        'jrs 2011/09/08 clearing labels
        Session("FDNG1")(1) = ""
        Session("FDNG1")(2) = ""
    End Sub



    '=============================LongVarS.Frm==============================
    'UPGRADE_WARNING: Event cmb_vcat.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    'KIA 09/22/2009 REPLACE THE FOLLOWING METHOD. Lot of changes have been made due to the new category type and also two new ifs blocks added.
    Private Sub cmb_vcat_SelectedIndexChanged1(ByVal catIndx As Integer, ByVal catName As String)
        Dim AddName As String
        Dim Definition As String
        Dim VarName As String
        Dim TableName As String
        Dim GroupSelect As String
        'jrs 2011/07/06
        Dim sPreprocessor As String, sYears As String, bInclude As Boolean

        'mtiwebform June 2004 Longitudinal
        'Dim db As String
        'Dim Valtbl As DataTable
        Dim Rcnt As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '
        '   Reload Variable Name Combo Box based on Group Selected by User
        '   --------------------------------------------------------------

        'jrs 2005/03/04

        Session("LongVarVCat") = catIndx
        Session("LongVarSelName") = catName
        lstVarName.Items.Clear()
        dataString = ""
        'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        GroupSelect = catName
        Valtbl = Session("DataDictVars")

        Dim tCohort As String
        Dim tSeries As String
        Dim sUnits As String
        Dim Groups As Array
        Dim pos1 As Integer
        'jrs 2012/02/27
        Dim sCoVaTra As String

        If UCase(GroupSelect) = "ALL" Then
            For Rcnt = 0 To Valtbl.Rows.Count - 1
                TableName = Valtbl.Rows(Rcnt).Item("Table")
                GroupName = Valtbl.Rows(Rcnt).Item("Group")
                VarName = Valtbl.Rows(Rcnt).Item("Variable")
                tSeries = Valtbl.Rows(Rcnt).Item("Series")
                tCohort = Valtbl.Rows(Rcnt).Item("Cohort")
                'Definition = Valtbl.Rows(Rcnt).Item("Definition")
                'JAM 2011-09-25 Replacing two and three blank spaces
                Definition = Replace(Replace(Valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

                'jrs 2011/07/08
                sPreprocessor = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", Valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
                sYears = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Years")), "", Valtbl.Rows(Rcnt).Item("Years"))
                'JRS 10/13/03
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                'sUnits = IIf(IsDBNull(Valtbl.Fields("Units").Value), "", Valtbl.Fields("Units").Value)
                sUnits = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Units")), "", Valtbl.Rows(Rcnt).Item("Units"))
                'jrs 2012/02/27
                sCoVaTra = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("CoVaTra")), "No", Valtbl.Rows(Rcnt).Item("CoVaTra"))

                'jrs 2005/03/28
                If GroupName = GroupSelect Or Session("LongVarVCat") < 1 Then
                    If tSeries = "Yes" Or tCohort = "Yes" Then
                        'jrs 2011/07/06
                        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
                        If bInclude Then
                            If ModelLoad = "LoadCoVaTrA" Or UCase(sCoVaTra) = "NO" Then 'jrs 2012/02/27 GroupName <> "Automobile" Then
                                AddName = VarName & " - " & Definition & " - " & sUnits

                                dataString = dataString + AddName + "|"
                                If AddName <> "" Then
                                    lstVarName.Items.Add(New ListItem(AddName))
                                End If
                            End If
                        End If
                    End If
                End If
            Next Rcnt
        Else
            For Rcnt = 0 To Valtbl.Rows.Count - 1
                pos1 = 0
                TableName = Valtbl.Rows(Rcnt).Item("Table")
                GroupName = Valtbl.Rows(Rcnt).Item("Group")
                Groups = Split(GroupName, ",")
                VarName = Valtbl.Rows(Rcnt).Item("Variable")
                tSeries = Valtbl.Rows(Rcnt).Item("Series")
                tCohort = Valtbl.Rows(Rcnt).Item("Cohort")
                'Definition = Valtbl.Rows(Rcnt).Item("Definition")
                'JAM 2011-09-25 Replacing two and three blank spaces
                Definition = Replace(Replace(Valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

                'jrs 2011/07/08
                sPreprocessor = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", Valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
                sYears = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Years")), "", Valtbl.Rows(Rcnt).Item("Years"))
                'JRS 10/13/03
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                'sUnits = IIf(IsDBNull(Valtbl.Fields("Units").Value), "", Valtbl.Fields("Units").Value)
                sUnits = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Units")), "", Valtbl.Rows(Rcnt).Item("Units"))
                'jrs 2012/02/27
                sCoVaTra = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("CoVaTra")), "No", Valtbl.Rows(Rcnt).Item("CoVaTra"))

                'jrs 2005/03/28
                While pos1 < Groups.Length
                    GroupName = Trim(Groups(pos1))
                    If GroupName = GroupSelect Then
                        If tSeries = "Yes" Or tCohort = "Yes" Then
                            'jrs 2011/07/06
                            bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
                            If bInclude Then
                                If ModelLoad = "LoadCoVaTrA" Or UCase(sCoVaTra) = "NO" Then 'jrs 2012/02/27 GroupName <> "Automobile" Then
                                    AddName = VarName & "-" & Definition & " - " & sUnits

                                    dataString = dataString + AddName + "|"
                                    If AddName <> "" Then
                                        lstVarName.Items.Add(New ListItem(AddName))
                                    End If
                                End If
                            End If
                        End If
                    End If
                    pos1 += 1
                End While
            Next Rcnt
        End If

        txtVarSearch.Text = ""

    End Sub

    'jrs 2011/07/06
    Private Sub cmb_OthFilt_SelectedIndexChanged(ByVal catIndx As Integer, ByVal catName As String)
        Dim AddName As String
        Dim Definition As String
        Dim VarName As String
        Dim TableName As String
        Dim GroupSelect As String
        'jrs 2011/07/06
        Dim sPreprocessor As String, sYears As String, bInclude As Boolean

        'mtiwebform June 2004 Longitudinal
        'Dim db As String
        'Dim Valtbl As DataTable
        Dim Rcnt As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '
        '   Reload Variable Name Combo Box based on Group Selected by User
        '   --------------------------------------------------------------

        'jrs 2005/03/04

        Session("LongVarFilt") = catIndx
        Session("LongVarFiltName") = catName
        lstVarName.Items.Clear()
        dataString = ""
        GroupSelect = Session("LongVarSelName")
        Valtbl = Session("DataDictVars")

        Dim tCohort As String
        Dim tSeries As String
        Dim sUnits As String
        Dim Groups As Array
        Dim pos1 As Integer

        If UCase(GroupSelect) = "ALL" Then
            For Rcnt = 0 To Valtbl.Rows.Count - 1
                TableName = Valtbl.Rows(Rcnt).Item("Table")
                GroupName = Valtbl.Rows(Rcnt).Item("Group")
                VarName = Valtbl.Rows(Rcnt).Item("Variable")
                tSeries = Valtbl.Rows(Rcnt).Item("Series")
                tCohort = Valtbl.Rows(Rcnt).Item("Cohort")
                'Definition = Valtbl.Rows(Rcnt).Item("Definition")
                'JAM 2011-09-25 Replacing two and three blank spaces
                Definition = Replace(Replace(Valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

                'jrs 2011/07/08
                sPreprocessor = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", Valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
                sYears = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Years")), "", Valtbl.Rows(Rcnt).Item("Years"))
                'JRS 10/13/03
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                'sUnits = IIf(IsDBNull(Valtbl.Fields("Units").Value), "", Valtbl.Fields("Units").Value)
                sUnits = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Units")), "", Valtbl.Rows(Rcnt).Item("Units"))
                'jrs 2005/03/28
                If GroupName = GroupSelect Or Session("LongVarVCat") < 1 Then
                    If tSeries = "Yes" Or tCohort = "Yes" Then
                        'jrs 2011/07/06
                        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
                        If bInclude Then
                            If ModelLoad = "LoadCoVaTrA" Or GroupName <> "Automobile" Then
                                AddName = VarName & " - " & Definition & " - " & sUnits

                                dataString = dataString & AddName & "|"
                                If AddName <> "" Then
                                    lstVarName.Items.Add(New ListItem(AddName))
                                End If
                            End If
                        End If
                    End If
                End If
            Next Rcnt
        Else
            For Rcnt = 0 To Valtbl.Rows.Count - 1
                pos1 = 0
                TableName = Valtbl.Rows(Rcnt).Item("Table")
                GroupName = Valtbl.Rows(Rcnt).Item("Group")
                Groups = Split(GroupName, ",")
                VarName = Valtbl.Rows(Rcnt).Item("Variable")
                tSeries = Valtbl.Rows(Rcnt).Item("Series")
                tCohort = Valtbl.Rows(Rcnt).Item("Cohort")
                'Definition = Valtbl.Rows(Rcnt).Item("Definition")
                'JAM 2011-09-25 Replacing two and three blank spaces
                Definition = Replace(Replace(Valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

                'jrs 2011/07/08
                sPreprocessor = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", Valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
                sYears = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Years")), "", Valtbl.Rows(Rcnt).Item("Years"))
                'JRS 10/13/03
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                'sUnits = IIf(IsDBNull(Valtbl.Fields("Units").Value), "", Valtbl.Fields("Units").Value)
                sUnits = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Units")), "", Valtbl.Rows(Rcnt).Item("Units"))
                'jrs 2005/03/28
                While pos1 < Groups.Length
                    GroupName = Trim(Groups(pos1))
                    If GroupName = GroupSelect Then
                        If tSeries = "Yes" Or tCohort = "Yes" Then
                            'jrs 2011/07/06
                            bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
                            If bInclude Then
                                If ModelLoad = "LoadCoVaTrA" Or GroupName <> "Automobile" Then
                                    AddName = VarName & "-" & Definition & " - " & sUnits

                                    dataString = dataString & AddName & "|"
                                    If AddName <> "" Then
                                        lstVarName.Items.Add(New ListItem(AddName))
                                    End If
                                End If
                            End If
                        End If
                    End If
                    pos1 += 1
                End While
            Next Rcnt
        End If

        txtVarSearch.Text = ""

    End Sub

    '=============================LongVarS.Frm==============================
    Private Sub cmd_Countries_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Countries.Click
        'jrs 2011/12/05
        'jrs 2014/03/05 Session("bCOuntSelGrouping") = (cmdUseGroups.Text = "Use Countries")
        If Session("bCOuntSelGrouping") Is Nothing Then
            Session("bCOuntSelGrouping") = False
        End If
        'jrs 2006/05/09 clearing flexible labels
        If Session("COUNTE") > 0 Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        Else
            Dim i As Integer
            For i = 1 To Session("PlotSize")
                Session("FDNG1")(i) = ""
            Next i
        End If

        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '
        '   Check for at least one variable
        '   -------------------------------
        'Check for Independent Variable when as time 'ask BBH

        If Session("LV_lbl_Dep") = "" Then
            Msg = "You must select a dependent variable before selecting countries."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'jrs 2023/09/22
            'LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('" & Msg & "');</script>")
        End If
        '
        '   Check for Consistency of WVS or Year variables
        '   ----------------------------------------------

        '    If Session("DepVarType")$ = "WVS" Or Session("IndVarType")$ = "WVS" Or Session("ConVarType")$ = "WVS" Then
        '        If Session("DepVarType")$ = "NoWVS" Or (Session("IndVarType")$ = "NoWVS" And VarNameInd$ <> "") _
        ''            Or (Session("ConVarType")$ = "NoWVS" And ConVarDep$ <> "") Then
        '            msg$ = "You cannot mix WVS variables (across waves) with others (across years)"
        '            MsgBox msg$
        '            Exit Sub
        '        End If
        '    End If

        If Session("LV_lbl_Ind") <> "TIME" Then
            If (Session("DepVarType") <> Session("IndVarType")) Or (Session("VarNameCon") <> "" And Session("ConVarType") <> Session("DepVarType")) Then
                Msg = "You cannot mix WVS variables (across waves) with others (across years)"

                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

                'jrs 2023/09/22
                'LoadMessage(Msg, Session("Message"))
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")
                Response.Write("<script>alert('" & Msg & "');</script>")

            End If
        End If


        'JAM 2013-11-16 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            If Session("LongVarVCat") Is Nothing Then
                Session("LongVarVCat") = 0
            Else
                fSubSetchosen.Value = Session("LongVarVCat")
            End If
        End If
        If fOthFiltchosen.Value <> "" Then
            Session("LongVarFilt") = fOthFiltchosen.Value
        Else
            If Session("LongVarFilt") Is Nothing Then
                Session("LongVarFilt") = 3  'All Value Selected
            Else
                fOthFiltchosen.Value = Session("LongVarFilt")
            End If
        End If



        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'mti WebForm Changes From July 02, 2004
        'jrs 2008/05/15 lbl_CountSel.Text = "None"
        'JRS 12/06/02
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.ShowDialog().html'.
        'frm_CountSel.DefInstance.ShowDialog() 'Only use by IFs of this form

        'mtiwebform June 2004 Longitudinal
        Support.FormManager.StateStore(Me)

        'AJAXRedirect("Redirect|CountSel.aspx")
        Support.FormManager.Show(Me, "CountSel.aspx")
        'end mtiwebform June 2004 Longitudinal

    End Sub
    Private Sub sendAJAXResponse(ByVal data As String)

        Response.Clear()
        Response.Write(data)
        Response.ContentType = "text/txt"
        Response.End()
    End Sub
    '=============================LongVarS.Frm==============================
    Private Sub cmd_Exit2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit2.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'mtiwebform June 2004 Longitudinal - disable computation
        'JRS 9/15/01 COMPUTATIONS
        If cmdComputations.Visible Then
            ''CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'Support.FormManager.Close(frm_computations.DefInstance)
            'If sOriginForm = Me.ID Then 
            '	sOriginForm = ""
            '	'just for Longitudinal analysis
            '	Session("bCompAll") = False
            'End If 
        End If

        'JRS 9/18/01 COMPUTATIONS
        If Session("bComputation") Then

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")

        Else
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.

            ClearSessionVars()
            'jrs 09/27/04
            Support.FormManager.Show(Me, "frm_MainMenu.aspx")

        End If

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub


    '=============================LongVarS.Frm==============================
    Private Sub cmd_Plot_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Plot.Click
        'jrs 10/25/04
        On Error GoTo LongHandler

        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '
        '   Check for Completed Selection
        '   -----------------------------
        If UCase(Session("LV_lbl_Ind")) = "NONE" Or UCase(Session("LV_lbl_Dep")) = "NONE" Then
            Msg = "You must choose both independent and dependent variables."
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'jrs 2023/09/22
            'LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('" & Msg & "');</script>")
        End If

        '
        '   Check for Consistency of WVS or Year variables
        '   ----------------------------------------------

        '    If Session("DepVarType")$ = "WVS" Or Session("IndVarType")$ = "WVS" Or Session("ConVarType")$ = "WVS" Then
        '        If Session("DepVarType")$ = "NoWVS" Or (Session("IndVarType")$ = "NoWVS" And VarNameInd$ <> "") _
        ''            Or (Session("ConVarType")$ = "NoWVS" And VarNameCon$ <> "") Then
        '            msg$ = "You cannot mix WVS variables (across waves) with others (across years)"
        '            MsgBox msg$
        '            Screen.MousePointer = 0
        '            cmb_VarName.SetFocus
        '            Exit Sub
        '        End If
        '    End If

        If Session("LV_lbl_Ind") <> "TIME" Then
            If (Session("DepVarType") <> Session("IndVarType")) Or (Session("VarNameCon") <> "" And Session("ConVarType") <> Session("DepVarType")) Then
                Msg = "You cannot mix WVS variables (across waves) with others (across years)"
                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                'jrs 2023/09/22
                'LoadMessage(Msg, Session("Message"))
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")
                Response.Write("<script>alert('" & Msg & "');</script>")
            End If
        End If

        '
        '   Check for at least one country specfication
        '   -------------------------------------------

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CountryTot"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("CountryTot") < 1 Then
            Msg = "You must select at least one country for display"
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'jrs 2023/09/22
            'LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('" & Msg & "');</script>")
        End If
        '
        '   Select Data and Labels for Year-Based or Cohort-Based Varibles
        '   --------------------------------------------------------------
        'JRS 11/10/01 COMPUTATIONS
        Dim aYears() As Object
        Dim Compfieldsfound As Short
        Dim CompVarName As String
        Dim vc As Short
        Dim bFirstTime As Boolean
        'jrs 2005/12/19 RAM for Formulas Dim rsL As DataTable
        'mtiwebform June 2004 Longitudinal - ADO
        'Dim rsT As DAO.Recordset
        Dim rsT As DataTable

        'jrs 2005/12/19 RAM for Formulas Dim dbL As String
        'Dim dbT As DAO.Database
        Dim dbT As String

        Dim LocalIdx = 0, NumbCompVar As Object
        Dim CurrCompVar As Short
        If Session("bComputationActive") Then
            vc = Session("VarComp")
            If vc <> 3 Then
                LocalIdx = vc
            Else
                LocalIdx = 1
            End If

            bFirstTime = True


            'jrs 2005/12/19 RAM for Formulas dbL = Session("PutFdrvRun") & "IFSVAR.MDB"
            dbT = Session("PutFdrvRun") & "datadict.MDB"
        End If

        'JRS 10/10/01 COMPUTATION
        Dim bFoundField As Boolean
        Dim i1 As Object
        Dim i2 As Short
        If Not Session("bComputationActive") Then
            If Session("LV_Cohort") = "Yes" Then
                Call CohortBasedData()
            Else
                Call YearBasedData()
            End If
        Else
            Do
                NumbCompVar = Session("aFormulas")(LocalIdx).disVar.disSize()
                'JRS 11/26/02 Reading several countries
                If rbTreatments.SelectedIndex = 0 Then
                    Session("aFormulas")(LocalIdx).endAll = Session("CountryTot")
                End If
                Session("f") = Session("aFormulas")(LocalIdx)

                For CurrCompVar = 1 To NumbCompVar
                    CompVarName = Session("aFormulas")(LocalIdx).disVar().display(CurrCompVar)
                    'jrs 2005/12/19 RAM for Formulas SetDataTablefromAccess(dbL, "SELECT [VarName] AS L FROM [FORMULAS] WHERE [CODE]=" & Str(LocalIdx - 1) & " AND [CODEVAR] = '" & CompVarName & "'", "", rsL)
                    SetDataTablefromAccess(dbT, "SELECT * FROM [datadict]", "Variable", rsT)

                    If LocalIdx = 2 Then
                        Session("LV_lbl_Ind") = Session("aFormulas")(LocalIdx).disVar.displayDetS(CompVarName) 'jrs 2005/12/19 RAM for Formulas rsL.Rows(0).Item("L")
                        'jrs 2010/08/09
                        Session("SC_sAggregationI") = IIf(IsDBNull(rsT.Rows(0).Item("Aggregation")), "", rsT.Rows(0).Item("Aggregation"))
                    Else
                        Session("LV_lbl_Dep") = Session("aFormulas")(LocalIdx).disVar.displayDetS(CompVarName) 'jrs 2005/12/19 RAM for Formulas rsL.Rows(0).Item("L")
                        'jrs 2010/08/09
                        Session("SC_sAggregationD") = IIf(IsDBNull(rsT.Rows(0).Item("Aggregation")), "", rsT.Rows(0).Item("Aggregation"))
                        'jrs 2010/09/01
                        If vc = 3 Then
                            Session("LV_lbl_Ind") = Session("aFormulas")(2).disVar.displayDet(1)
                        End If
                    End If

                    If Session("LV_Cohort") = "Yes" Then
                        Call CohortBasedData()
                        'Assign Session("PLOTAll") to dataAll() variable
                        Session("dataAll")(CurrCompVar) = VB6.CopyArray(Session("PLOTAll"))
                    Else
                        Call YearBasedData()

                        If bFirstTime Then
                            Compfieldsfound = Session("DisYear")
                            If rbTreatments.SelectedIndex <> 0 Then
                                aYears = VB6.CopyArray(Session("ScatVarYears"))
                            Else
                                aYears = VB6.CopyArray(Session("DepVarYears"))
                            End If
                            bFirstTime = False
                        Else
                            For i1 = 1 To Session("DisYear")
                                If i1 > Session("DisYear") Then Exit For
                                bFoundField = False
                                For i2 = 1 To Compfieldsfound
                                    If rbTreatments.SelectedIndex <> 0 Then
                                        If aYears(i2) = Session("ScatVarYears")(i1) Then
                                            bFoundField = True
                                            Exit For
                                        End If
                                    Else
                                        If aYears(i2) = Session("DepVarYears")(i1) Then
                                            bFoundField = True
                                            Exit For
                                        End If
                                    End If
                                Next i2
                                If Not bFoundField Then
                                    For i2 = i1 To Session("DisYear") - 1
                                        If rbTreatments.SelectedIndex <> 0 Then
                                            Session("ScatVarYears")(i2) = Session("ScatVarYears")(i2 + 1)
                                        Else
                                            Session("DepVarYears")(i2) = Session("DepVarYears")(i2 + 1)
                                        End If
                                    Next i2
                                    If rbTreatments.SelectedIndex <> 0 Then
                                        Session("ScatVarYears")(i2) = ""
                                    Else
                                        Session("DepVarYears")(i2) = ""
                                    End If
                                    Session("DisYear") = Session("DisYear") - 1
                                    i1 = i1 - 1
                                End If
                            Next i1
                            Compfieldsfound = Session("DisYear")
                            If rbTreatments.SelectedIndex <> 0 Then
                                aYears = VB6.CopyArray(Session("ScatVarYears"))
                            Else
                                aYears = VB6.CopyArray(Session("DepVarYears"))
                            End If
                        End If
                    End If ' CLOSING THAT IS NOT COHORT
                Next CurrCompVar

                If vc = 3 And LocalIdx = 1 Then
                    LocalIdx = 2
                Else
                    Exit Do
                End If

            Loop While (True)


            If vc = 3 Then
                LocalIdx = 1
            End If

            If Session("LV_Cohort") <> "Yes" Then
                LoadData((LocalIdx))
            End If

        End If
        'end JRS 10/10/01

        If Session("LV_Cohort") <> "Yes" Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PlotPossible"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("PlotPossible") = False Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PlotPossible"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("PlotPossible") = True
                Exit Sub
            End If
        End If

        'JRS 10/10/01 COMPUTATIONS
        Dim oldInd As String
        Dim oldDep As String
        If Session("bComputationActive") Then
            Session("LongVarSTag") = ""
            Select Case LocalIdx
                Case 2
                    Session("f") = Session("aFormulas")(2)
                    opt_Independent.Checked = True
                    oldInd = Session("VarNameInd")
                    Session("oldInd") = oldInd
                    Session("oldDep") = Nothing
                    Session("VarNameInd") = Session("f").name 'jrs 2006/10/24 lbl_Ind.Text
                    'jrs 2006/02/23
                    opt_Dependent.Checked = False
                    Session("LongVarSopt_Independent") = opt_Independent.Checked
                    Session("LongVarSopt_Dependent") = opt_Dependent.Checked
                Case 1
                    Session("f") = Session("aFormulas")(1)
                    opt_Dependent.Checked = True
                    oldDep = Session("LV_lbl_Dep")
                    Session("oldDep") = oldDep
                    Session("oldInd") = Nothing
                    'jrs 2010/09/03 Session("LV_lbl_Dep") = Session("f").name 'jrs 2006/10/24 lbl_Dep.Text
                    'jrs 2006/02/23
                    opt_Independent.Checked = False
                    Session("LongVarSopt_Independent") = opt_Independent.Checked
                    Session("LongVarSopt_Dependent") = opt_Dependent.Checked
            End Select
            'cmb_VarName.SelectedIndex = -1
            'jrs 2006/02/23
            Session("sOriginForm") = "frm_LongVarS"
            'jrs 2005/06/23
            'Dim frm_computations As New frm_computations
            'frm_computations.UpdateIdx()
            Dim i As Integer
            i = LocalIdx
            If vc = 3 Then
                i = 2
            End If

            If rbTreatments.SelectedIndex > 0 Then
                Session("f").itotYears = Session("DisYear") * Session("CountryTot")
            Else
                Session("f").itotYears = Session("DisYear")
            End If
            Dim FinCalc() As Object
            Dim tmp As Int16
            If rbTreatments.SelectedIndex = 0 Then
                tmp = 1
            Else
                tmp = 0
            End If
            ReDim FinCalc(Session("DisYear") * Session("CountryTot"))
                UpdateIdx(Session("sOriginForm"), Session("idx"), Session("LongVarScmb_Varname"), _
            Session("LongVarSopt_Dependent"), Session("LongVarSopt_Independent"), Session("LongVarSTag"))

            ComputeFormulasWAll(Session("f"), -1, -1, Session("data"), Session("idx"), FinCalc, _
            False, Session("aFormulas"), Me, "frm_LongVarS", tmp, Session("DisYear"), _
            Session("CountryTot"), Session("bCompAll"), Session("dataAll"), Session("LongVarSTag"), _
            Session("LongVarScmb_Varname"), Session("LongVarSopt_Dependent"), Session("LongVarSopt_Independent"))
            LoadDataPlotLong(FinCalc, Session("aFormulas")(Session("idx")), Session("idx"))
            FormatFormAfterLoad(Session("aFormulas")(Session("idx")))
            If vc = 3 Then
                'jrs 2010/09/06
                oldDep = Session("LV_lbl_Dep")
                Session("LV_lbl_Dep") = Session("LV_lbl_Ind")

                Session("bCompAll") = True
                LoadData((2))
                Session("f") = Session("aFormulas")(2)
                Session("LongVarSTag") = ""
                opt_Independent.Checked = True
                Session("LongVarSopt_Independent") = True
                Session("LongVarSopt_Dependent") = False
                oldInd = Session("LV_lbl_Ind")

                'jrs 2010/09/06
                Session("LV_lbl_Dep") = oldDep

                'jrs 2010/09/03 Session("LV_lbl_Ind") = Session("f").name 'jrs 2006/10/24 lbl_Ind.Text
                'cmb_VarName.SelectedIndex = -1
                    UpdateIdx(Session("sOriginForm"), Session("idx"), Session("LongVarScmb_Varname"), _
                Session("LongVarSopt_Dependent"), Session("LongVarSopt_Independent"), Session("LongVarSTag"))
                ComputeFormulasWAll(Session("f"), -1, -1, Session("data"), Session("idx"), FinCalc, _
                False, Session("aFormulas"), Me, "frm_LongVarS", tmp, Session("DisYear"), _
                Session("CountryTot"), Session("bCompAll"), Session("dataAll"), Session("LongVarSTag"), _
                Session("LongVarScmb_Varname"), Session("LongVarSopt_Dependent"), Session("LongVarSopt_Independent"))
                LoadDataPlotLong(FinCalc, Session("aFormulas")(Session("idx")), Session("idx"))
                FormatFormAfterLoad(Session("aFormulas")(Session("idx")))
            End If
            If rbTreatments.SelectedIndex = 2 Then
                Session("TableTitle") = Session("CountryList")(1) & " (" & Session("LV_lbl_Dep") & " and " & Session("LV_lbl_Ind") & ")"
            End If
            If Session("LV_Cohort") = "Yes" And Session("LV_lbl_Ind") = "" Then
                Session("TableTitle") = lbl_Dep.Text & " versus WVS Wave"
            End If
            Session("bCompAll") = True
        End If

        'JAM 2013-11-16 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            If Session("LongVarVCat") Is Nothing Then
                Session("LongVarVCat") = 0
            Else
                fSubSetchosen.Value = Session("LongVarVCat")
            End If
        End If
        If fOthFiltchosen.Value <> "" Then
            Session("LongVarFilt") = fOthFiltchosen.Value
        Else
            If Session("LongVarFilt") Is Nothing Then
                Session("LongVarFilt") = 3  'All Value Selected
            Else
                fOthFiltchosen.Value = Session("LongVarFilt")
            End If
        End If


        '
        '   Scale Data for Graph
        '   --------------------
        'mti WebForm Changes From July 02, 2004
        'Call ScaledGraph()

        '
        '   Check for at least one year for display
        '   ---------------------------------------
        'JRS 9/28/01, Correction in order to avoid abort when just one point, before "<1"
        'jrs 2007/02/23 I think single points should be plotted
        If Session("DisYear") < 2 And Not (Session("DisYear") = 1 And rbTreatments.SelectedIndex = 1) Then
            'jrs 2007/02/20
            '        Msg$ = "There are not at least 2 points in this data series, so it "
            '        Msg$ = Msg$ + "cannot be plotted.  Please try another."
            '        MsgBox Msg$
            If Session("DisYear") = 1 Then
                Msg$ = "There is only one point in this data series:"
                Dim i As Integer
                If rbTreatments.SelectedIndex = 0 Then
                    For i = 1 To Session("CountryTot")
                        If IsNumeric(Session("PlotLong")(1, i)) Then
                            Msg$ = Msg$ & "<br>" & Session("CountryList")(i) & " - " & Session("PlotLabels")(1, 1) & " = " & Session("PlotLong")(1, i)
                        End If
                    Next i
                ElseIf rbTreatments.SelectedIndex = 2 Then
                    If IsNumeric(Session("PlotLong")(1, 1)) Then
                        Msg$ = Msg$ & "<br>" & Session("LV_lbl_Ind") & " - " & Session("CountryList")(1) & " - " & Session("ScatVarYears")(1) & " = " & Session("PlotLong")(1, 1)
                    End If
                    If IsNumeric(Session("PlotLong")(1, 2)) Then
                        Msg$ = Msg$ & "<br>" & Session("LV_lbl_Dep") & " - " & Session("CountryList")(1) & " - " & Session("ScatVarYears")(1) & " = " & Session("PlotLong")(1, 2)
                    End If
                    'jrs 2007/02/23 not necessary anymore since we're plotting single points
                    '            Else
                    '                For i = 1 To CountryTot
                    '                    If IsNumeric(session("PlotLong")(i, 1)) Then
                    '                        Msg$ = Msg$ & "<br>" & VarNameInd & " - " & session("CountryList")(i) & " - " & ScatVarYears(1) & " = " & session("PlotLong")(i, 1)
                    '                    End If
                    '                    If IsNumeric(session("PlotLong")(i, 2)) Then
                    '                        Msg$ = Msg$ & "<br>" & VarNameDep & " - " & session("CountryList")(i) & " - " & ScatVarYears(1) & " = " & session("PlotLong")(i, 2)
                    '                    End If
                    '                Next i
                End If
                Msg$ = Msg$ + "<br>" + "So it cannot be plotted.  Please try another."
                'jrs 2023/09/22
                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            Else
                Msg$ = "There are no points in this data series, so it "
                Msg$ = Msg$ + "cannot be plotted.  Please try another."
                'jrs 2023/09/22
                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If

        If Session("DisYear") < 2 Then
            Msg = "There are not at least 2 points in this data series, so it "
            Msg = Msg & "cannot be plotted.  Please try another."
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'jrs 2023/09/22
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If


        If UCase(lbl_Cont.Text) <> "NONE" Then
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "Control.aspx")
            'frm_Control.DefInstance.Show() 
        Else
            'JRS 2/22/02 frm_LongPlot.Show
            'UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1039"'
            'JRS 01/28/04 Load(frm_LongPlotProEssential)
            'Dim frm_LongPlotProEssential As New frm_LongPlotProEssential

            'CONVERSION_ISSUE: Property 'IFsGUI.frm_LongPlotProEssential.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'frm_LongPlotProEssential.DefInstance.LoadPlotVec(Session("PlotLong"), Session("PlotSize"), Session("PlotLabels"))
            'mtiwebform June 2004 Longitudinal - took it to function.vb
            LoadPlotVec(Session("PlotLong"), Session("PlotSize"), Session("PlotLabels"))

            Session("LongPlotFore") = False
            Support.FormManager.StateStore(Me)

            AssignTreatmentIndex()

            If Session("LongVarTable") Then
                mnuTable_Click(Me, New EventArgs)
                Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

            Else
                'jrs 2005/06/23
                'Load(frm_LongPlotProEssential)
                'frm_LongPlotProEssential.LoadPlotVec(session("PlotLong")(), PlotSize, PlotLabels())
                'frm_LongPlotProEssential.DisplayGraph()
                'frm_LongPlotProEssential.Pego1.SubTitle = sUnits
                'frm_LongPlotProEssential.Show()

                Support.FormManager.Show(Me, "frm_LongPlot.aspx")
            End If


        End If


        Exit Sub

LongHandler:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub
    Private Sub AssignTreatmentIndex()
        If rbTreatments.SelectedIndex = 0 Then
            Session("LV_Chk_Indep") = 1
            Session("LV_Chk_Points") = 0
            Session("LV_Chk_Time") = 0
        ElseIf rbTreatments.SelectedIndex = 1 Then
            Session("LV_Chk_Indep") = 0
            Session("LV_Chk_Points") = 1
            Session("LV_Chk_Time") = 0
        Else
            Session("LV_Chk_Indep") = 0
            Session("LV_Chk_Points") = 0
            Session("LV_Chk_Time") = 1
        End If
    End Sub

    '=============================LongVarS.Frm  =======================
    Private Sub ScaledGraph()

        Dim RescalerC As Object
        Dim RescalerD As Object
        Dim RescalerI As Object
        Dim ICount As Short
        Dim MaxC As Object
        Dim MaxD As Object
        Dim MaxI As Object
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
        For ICount = 1 To Session("CCount")
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("CVarI")(ICount) > MaxI Then MaxI = Session("CVarI")(ICount)
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("CVarD")(ICount) > MaxD Then MaxD = Session("CVarD")(ICount)
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("CVarC")(ICount) > MaxC Then MaxC = Session("CVarC")(ICount)
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

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("CCount"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For ICount = 1 To Session("CCount")
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerI. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CVarI")(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("CVarI")(ICount) = Session("CVarI")(ICount) / 10 ^ RescalerI
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarD(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("CVarD")(ICount) = Session("CVarD")(ICount) / 10 ^ RescalerD
            'UPGRADE_WARNING: Couldn't resolve default property of object RescalerC. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object CVarC(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("CVarC")(ICount) = Session("CVarC")(ICount) / 10 ^ RescalerC
        Next ICount

        '    ScaleLabel$ = GraphScaleLabel$(VarNameI$(0), RescalerI)

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub cmdComputations_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdComputations.Click
        'jrs 2006/02/22
        Session("LongVarSopt_Dependent") = opt_Dependent.Checked
        Session("LongVarSopt_Independent") = opt_Independent.Checked

        'jrs 2005/12/20 RAM for Formulas
        ''jrs 2005/06/16
        'SessionAllocator(Session("SessionIdx"), Me)
        'JRS 10/05/01 COMPUTATIONS
        Session("bCompAll") = True
        Session("bComputationActive") = True
        Dim i As Short
        Dim sId As String
        sId = "frm_LongVarS"

        If Session("sOriginForm") = "" Or Session("sOriginForm") = sId Then
            Session("sOriginForm") = sId
            'cmb_VarName.SelectedIndex = -1
            If rbTreatments.SelectedIndex <> 0 Then
                If (opt_Dependent.Checked And Session("VarComp") = 2) Or (opt_Independent.Checked And Session("VarComp") = 1) Then
                    Session("LongVarSTag") = ""
                End If
            End If
            UpdateCurrentDisplay()
            If sCurrentDisplay = "" Or sCurrentDisplay = "None" Then
                ResetComputationsFrm()
            Else
                i = iCurrentIdx
                If i <= UBound(Session("aFormulas")) Then
                    If Not (Session("aFormulas")(i) Is Nothing) Then
                        If sCurrentDisplay = Session("aFormulas")(i).Name Then
                            'JRS 01/28/04
                            Session("ComptxtFormula") = Session("aFormulas")(i).Formula_Renamed
                            Session("ComplblFormulaName") = Session("aFormulas")(i).Name
                            Session("ComplblDimensionality") = Session("aFormulas")(i).Dimension
                            Session("ComplblUnits") = Session("aFormulas")(i).Unit
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

            'JAM 2013-11-16 Keeping chosen filters
            If fSubSetchosen.Value <> "" Then
                If Session("LongVarVCat") Is Nothing Then
                    Session("LongVarVCat") = 0
                Else
                    fSubSetchosen.Value = Session("LongVarVCat")
                End If
            End If
            If fOthFiltchosen.Value <> "" Then
                Session("LongVarFilt") = fOthFiltchosen.Value
            Else
                If Session("LongVarFilt") Is Nothing Then
                    Session("LongVarFilt") = 3  'All Value Selected
                Else
                    fOthFiltchosen.Value = Session("LongVarFilt")
                End If
            End If


            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")
        Else

            LoadMessage("Another Form is using this option, please close forms that you are not using anymore", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

    End Sub


    'JRS 2/6/03 TABLE OF ALL COUNTRIES
    Private Sub cmdTable_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTable.Click
        'jrs 10/25/04
        On Error GoTo LongHandler
        If Session("WVSWave") = "True" Then 'jrs 2004/12/02 Session("LV_lbl_Ind") = "TIME" Then

            'jrs 2023/09/22
            'LoadMessage("No Table Display Available for Wave Variables.", Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('No Table Display Available for Wave Variables.');</script>")

        End If

        'jrs 08/30/04
        Dim Msg As String
        '   Check for Completed Selection
        '   -----------------------------
        If UCase(Session("LV_lbl_Dep")) = "NONE" Then
            Msg = "You must choose a dependent variable."
            'jrs 2023/09/22
            'LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('" & Msg & "');</script>")
        End If


        'JAM 2013-11-16 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            If Session("LongVarVCat") Is Nothing Then
                Session("LongVarVCat") = 0
            Else
                fSubSetchosen.Value = Session("LongVarVCat")
            End If
        End If
        If fOthFiltchosen.Value <> "" Then
            Session("LongVarFilt") = fOthFiltchosen.Value
        Else
            If Session("LongVarFilt") Is Nothing Then
                Session("LongVarFilt") = 3  'All Value Selected
            Else
                fOthFiltchosen.Value = Session("LongVarFilt")
            End If
        End If



        'mtiwebform June 2004 Longitudinal
        'Dim db As String
        'Dim Valtbl As DataTable
        Dim Rcnt As Short
        Dim f As Object
        'ReDim CountryList(200)
        Dim bGrouping As Boolean
        bGrouping = Not (cmdUseGroups.Text = "Use Groups")
        'db = DAODBEngine_definst.OpenDatabase(session("IFs_MDB2Active"))
        db = Session("IFs_MDB2Active")

        'Do
        'cdv 2022/06/30 ****START****
        If Session("CountryTot") = 0 Then
            'JRS 11/2/1
            If Not bGrouping Then
                'Valtbl = db.OpenRecordset(Regionalization)
                'Valtbl.index = "Country" '"PrimaryKey"

                SetDataTablefromAccess(db, "SELECT * FROM " + Regionalization + " order by Country", "", Valtbl)

            Else
                'Valtbl = db.OpenRecordset(GroupIDNew)
                'Valtbl.index = "GroupName"

                SetDataTablefromAccess(db, "SELECT * FROM " + GroupIDNew + " order by GroupName", "", Valtbl)
            End If

            'Valtbl.MoveFirst()
            Session("CountryTot") = 0
            For Rcnt = 0 To Valtbl.Rows.Count - 1
                'Do
                Session("CountryTot") = Session("CountryTot") + 1
                If Not bGrouping Then
                    Session("CountryList")(Session("CountryTot")) = Valtbl.Rows(Rcnt).Item("country")
                Else
                    Session("CountryList")(Session("CountryTot")) = Valtbl.Rows(Rcnt).Item("GroupName")
                End If
                'Valtbl.MoveNext()
            Next Rcnt
        End If
        'cdv 2022/06/30 ****END****
        'Loop Until (Valtbl.EOF)
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_LongPlotProEssential.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'Support.FormManager.Close(frm_LongPlotProEssential.DefInstance)
        'jrs 08/30/04
        Session("LongVarTable") = True
        cmd_Plot_Click(cmd_Plot, New System.EventArgs)

        'UPGRADE_ISSUE: Forms collection was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        'JRS 01/29/04
        If Session("bfrm_LongPlotProEssential") Then
            'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Hide().html'.
            'frm_LongPlotProEssential.DefInstance.Hide() 
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_LongPlotProEssential.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.

            'mtiwebform June 2004 Longitudinal - Unresolved - commenting for now
            'frm_LongPlotProEssential.DefInstance.mnuTable_Click(frm_LongPlotProEssential.DefInstance.mnuTable, New System.EventArgs)

            'CONVERSION_ISSUE: Property 'IFsGUI.frm_LongPlotProEssential.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'Support.FormManager.Close(frm_LongPlotProEssential.DefInstance)
            Support.FormManager.Close(Me)
        End If

        Exit Sub

LongHandler:

        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    'UPGRADE_WARNING: Form event frm_LongVarS.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Activated' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Activated.html'.
    Private Sub frm_LongVarS_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) 'Handles frm_LongVarS.Activated
        'JRS 10/09/01 COMPUTATIONS
        'JRS 2/6/03
        'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

    End Sub

    '=============================LongVarS.Frm==============================
    'CONVERSION_NOTE: The code of Event 'frm_LongVarS.frm_LongVarS_Load' was moved to Method 'frm_LongVarS.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub CreateMenu()

        wmContext.UserData = "jsolorza::976739975"

        wmContext.ClearPixelImage = "blank.gif"
        wmContext.PopupIcon = "./images/Popup.gif"
        wmContext.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmContext.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmContext.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmContext.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmContext.SelectedPopupIcon = "./images/SelectedPopup.gif"
        wmContext.ScrollEnabled = True
        wmContext.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group

        cswmGroup1 = wmContext.Groups.Add()
        cswmGroup1.ID = "Group_0bd4"


        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Data Information"


        cswmGroup1 = wmContext.Groups.Add()
        cswmGroup1.ID = "Group_0cd4"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select"



        ''jrs 2008/10/08
        'If Not IsNothing(Session("LoadSearchVar")) Then
        '    LoadSearch(Session("LoadSearchVar"))
        '    Session("LoadSearchVar") = Nothing
        'End If
        If Not IsPostBack Then
            Dim arl As New ArrayList()
            Dim k As Integer = 1

            While k < cmb_VCAT.Items.Count

                arl.Add(cmb_VCAT.Items(k).Value)
                k += 1
            End While

            arl.Sort()
            arl.Insert(0, "All")
            cmb_VCAT.Items.Clear()
            cmb_VCAT.DataSource = arl
            cmb_VCAT.DataBind()

            'jrs 2011/07/08
            Dim arl2 As New ArrayList()
            k = 0
            While k < cmb_OthFilt.Items.Count

                arl2.Add(cmb_OthFilt.Items(k).Value)
                k += 1
            End While

            cmb_OthFilt.Items.Clear()
            cmb_OthFilt.DataSource = arl2
            cmb_OthFilt.DataBind()
        End If
        If Not IsNothing(Session("LoadSearchVar")) Then
            LoadSearch(Session("LoadSearchVar"))
            Session("LoadSearchVar") = Nothing
        End If
    End Sub

    '=============================LongVarS.Frm==============================
    'UPGRADE_WARNING: Form event frm_LongVarS.Unload has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Closed' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Closed.html'.
    Private Sub frm_LongVarS_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) 'Handles frm_LongVarS.Closed
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/18/02 COUNTE = 0
        'JRS 11/18/02 For i2 = 1 To Session("LastYear")% - Session("FirstYear")% + 1
        'JRS 11/18/02 For ICount% = 1 To 40
        'JRS 11/18/02 PLOT(i2, ICount%) = 0
        'JRS 11/18/02 Next ICount%
        'JRS 11/18/02 Next i2
        'JRS 11/18/02 For iloop% = 1 To MaxVariables
        'JRS 11/18/02 VARN$(1, iloop%) = ""
        'JRS 11/18/02 VARN$(2, iloop%) = ""
        'JRS 11/18/02 VARN$(3, iloop%) = ""
        'mti Validation changes July 28, 2002
        'JRS 11/18/02 VARN$(4, iloop%) = ""
        'mti Validation changes July 28, 2002
        'mti Validation changes 19 August, 2002
        'JRS 11/18/02 VARN$(5, iloop%) = ""
        'mti Validation changes 19 August, 2002

        'JRS 11/18/02 GR1N$(iloop%) = ""
        'JRS 11/18/02 GR2N$(iloop%) = ""
        'JRS 11/18/02 GR3N$(iloop%) = ""
        'JRS 11/18/02 Next iloop%
        'JRS 11/18/02 Label1 = ""
        'JRS 11/18/02 Label2 = ""
        'JRS 11/18/02 Label3 = ""
        'JRS 10/24/01 COMPUTATIONS
        Session("bCompAll") = False
        Session("bComputationActive") = False
        'CONVERSION_NOTE: Property 'System.Windows.Forms.Form.Tag' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Tag.html'.
        'Session("LongVarSTag") = "" 

    End Sub

    '=============================LongVarS.Frm==============================
    'UPGRADE_WARNING: Event opt_Control.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub opt_Control_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles opt_Control.CheckedChanged
        If eventSender.Checked Then
            Dim Msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Msg = "Select Control Variable:"
            lbl_VarName.Text = Msg
            chk_ControlOff.Visible = True

        End If
    End Sub

    '=============================LongVarS.Frm==============================
    'UPGRADE_WARNING: Event opt_Dependent.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub opt_Dependent_CheckedChanged(ByVal val As Boolean)
        If val Then
            Dim Msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Msg = "Select Dependent Variable:"
            lbl_VarName.Text = Msg
            '    cmb_VarName.SetFocus
            Session("LongVarSopt_Dependent") = True
            Session("LongVarSopt_Independent") = False
        End If
    End Sub

    '=============================LongVarS.Frm==============================
    'UPGRADE_WARNING: Event opt_Independent.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub opt_Independent_CheckedChanged(ByVal val As Boolean)
        If val Then
            Dim Msg As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Msg = "Select Independent Variable:"
            lbl_VarName.Text = Msg
            '    cmb_VarName.SetFocus
            Session("LongVarSopt_Dependent") = False
            Session("LongVarSopt_Independent") = True

        End If
    End Sub

    '=============================LongVarS.Frm==============================
    Private Sub YearBasedData()
        Dim index As Object
        Dim TempValD As Object
        Dim TempValI As Object
        Dim icount2 As Short
        Dim accessfilename As String
        Dim ILoop2 As Object
        Dim iloop As Object
        Dim snumpoints As Object
        Dim inumpoints As Object
        Dim dnumpoints As Object
        Dim Msg As String


        Dim tempval As Object

        Dim ICount As Short
        'mtiwebform June 2004 Longitudinal
        Dim f As DataColumn 'Object
        Dim fieldsfound As Short 'Object
        'mtiwebform June 2004 Longitudinal
        'Dim db As String
        'Dim Valtbl As DataTable
        Dim Rcnt As Short

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 10/05/01 TO AVOID NULLS AS 0.
        Dim CountriesFound As Short
        'JRS 11/03/01 GROUPING
        Dim bGrouping As Boolean
        'mti WebForm Changes From July 02, 2004 - start
        bGrouping = Session("bCOuntSelGrouping") 'Not (cmdUseGroups.Text = "Use Groups")


        'jrs 2008/05/15 If bGrouping Then
        For ICount = 1 To Session("CountryTot")
            If Session("CountryListGroup")(ICount) = 1 Then
                'mti WebForm Changes From July 02, 2004 -start
                cgcount = ICount
                'mti WebForm Changes From July 02, 2004 -end
                LoadCountries((Session("CountryList")(ICount))) 'JRS 11/3/01 GROUPING
            End If
        Next
        'End If
        'mti WebForm Changes From July 02, 2004
        '   Put Years of Data for Dependent Variable into Session("DepVarYears")
        '   ---------------------------------------------------------

        On Error GoTo FileNotFoundYearSearch
        If GroupName = "Automobile" Then
            db = Session("IFsCoVaTrASeries_MDB")
            SetDataTablefromAccess(db, "SELECT * FROM Series" & Session("LV_lbl_Dep"), "Country", Valtbl)
        Else
            db = Session("IFsHistSeries_MDB")
            SetDataTablefromAccess(db, "SELECT * FROM [Series" & Session("LV_lbl_Dep") & "]", "Country", Valtbl, DataPW)
        End If

        'For Rcnt = 0 To Valtbl.Rows.Count - 1
        'Valtbl = db.OpenRecordset("Series" & VarNameDep)
        'Valtbl.Index = "Country"
        fieldsfound = 0
        'For Each f In Valtbl.Fields

        For Each f In Valtbl.Columns
            'If f.Name <> "Country" And f.Name <> "FIPS_CODE" And f.Name <> "Cum" And f.Name <> "FIPS_Code" Then
            'jrs 2006/10/25 If f.ColumnName <> "Country" And f.ColumnName <> "FIPS_CODE" And f.ColumnName <> "Cum" And f.ColumnName <> "FIPS_Code" Then
            If IsNumeric(f.ColumnName) Then
                'JRS 10/05/01 TO AVOID NULLS AS 0.
                CountriesFound = 0
                For ICount = 1 To Session("CountryTot") 'Remove nulls from data points
                    'JRS 11/3/01 GROUPING
                    'jrs 2008/05/15 If Not bGrouping Then
                    If Session("CountryListGroup")(ICount) <> 1 Then
                        'JRS 2/6/03
                        'Valtbl.Seek("=", CountryList(ICount))
                        'tempval = Valtbl.Fields(f.Name).Value
                        tempval = SeekValue(Valtbl, Session("CountryList")(ICount), f.ColumnName)
                    Else
                        'mti WebForm Changes From July 02, 2004
                        'LoadCountries((Session("CountryList")(ICount))) 'JRS 11/3/01 GROUPING
                        'mti WebForm Changes From July 02, 2004
                        'tempval = RetrGroupVal(f.Name, Session("SC_sAggregationD")) 'JRS 11/3/01 GROUPING
                        'mti WebForm Changes From July 02, 2004 -start
                        cgcount = ICount
                        'mti WebForm Changes From July 02, 2004 -end
                        tempval = RetrGroupVal(f.ColumnName, Session("SC_sAggregationD")) 'JRS 11/3/01 GROUPING
                    End If
                    'JRS 6/10/02 NULLS AND ZEROS
                    'mti Augut 2004 4
                    If optNullstoZeros.Checked And (IsDBNull(tempval) Or tempval.ToString = "") Then
                        '    If optNullstoZeros.Checked And IsDBNull(tempval) Then
                        tempval = 0
                    End If
                    'JRS 10/01/01 REPETITION OF POINTS If Not (IsNull(TempVal)) Then
                    If Not (IsDBNull(tempval) Or tempval.ToString = "") And Session("DepVarYears")(fieldsfound) <> f.ColumnName Then
                        'If Not (IsDBNull(tempval)) And Session("DepVarYears")(fieldsfound) <> f.ColumnName Then
                        CountriesFound = CountriesFound + 1
                        'JRS 10/05/01 This condition is to count only points
                        'available in all countries
                        'JRS 5/8/02 adding or not b grouping
                        'If CountriesFound = Session("CountryTot") Or Not bGrouping Then
                        fieldsfound = fieldsfound + 1
                        If fieldsfound >= Session("MaxDepIndYears") Then
                            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            Msg = "You have chosen more countries and/or variables with more "
                            Msg = Msg & "data points than allowed.  Please reduce."
                            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PlotPossible"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Session("PlotPossible") = False
                            'Valtbl.Close()
                            'jrs 2023/09/22
                            'LoadMessage(Msg, Session("Message"))
                            'Support.FormManager.StateStore(Me)
                            'Support.FormManager.Show(Me, "frm_Message.aspx")
                            Response.Write("<script>alert('" & Msg & "');</script>")
                        End If
                        Session("DepVarYears")(fieldsfound) = f.ColumnName '.Name
                        'End If 'end JRS 10/05/01
                    End If
                Next ICount
            End If
        Next 'f
        dnumpoints = fieldsfound
        Session("DisYear") = fieldsfound
        'Valtbl.Close()
        'db.Close()


        '   Put Years of Data for Independent Variable into Session("IndVarYears") (if Not Years)
        '   --------------------------------------------------------------------------
        If Session("LV_lbl_Ind") <> "TIME" Then 'Scattergram with time points

            'If GroupName = "Automobile" Then
            '    db = DAODBEngine_definst.OpenDatabase(session("IFsCoVaTrASeries_MDB"))
            'Else
            '    db = DAODBEngine_definst.OpenDatabase(session("IFsHistSeries_MDB"), False, False, DataPW)
            'End If
            'Valtbl = db.OpenRecordset("Series" & VarNameInd)
            'Valtbl.Index = "Country"

            If GroupName = "Automobile" Then
                'db = DAODBEngine_definst.OpenDatabase(session("IFsCoVaTrASeries_MDB"))
                db = Session("IFsCoVaTrASeries_MDB")
                SetDataTablefromAccess(db, "SELECT * FROM [Series" & Session("LV_lbl_Ind") & "]", "Country", Valtbl) 'jrs 2014/03/04 added [] to help with vars like PopulationUrban%
            Else
                'db = DAODBEngine_definst.OpenDatabase(session("IFsHistSeries_MDB"), False, False, DataPW)
                db = Session("IFsHistSeries_MDB")
                SetDataTablefromAccess(db, "SELECT * FROM [Series" & Session("LV_lbl_Ind") & "]", "Country", Valtbl, DataPW) 'jrs 2014/03/04 added [] to help with vars like PopulationUrban%
            End If

            fieldsfound = 0
            For Each f In Valtbl.Columns '.Fields
                'jrs 2006/10/25 If f.ColumnName <> "Country" And f.ColumnName <> "FIPS_CODE" And f.ColumnName <> "Cum" And f.ColumnName <> "FIPS_Code" Then
                If IsNumeric(f.ColumnName) Then
                    'JRS 10/24/01 TO AVOID NULLS AS 0.
                    CountriesFound = 0
                    For ICount = 1 To Session("CountryTot") 'Remove nulls from data points
                        'JRS 11/3/01 GROUPING
                        'jrs 2008/05/15 If Not bGrouping Then
                        If Session("CountryListGroup")(ICount) <> 1 Then
                            'Valtbl.Seek("=", CountryList(ICount))
                            'tempval = Valtbl.Fields(f.Name).Value
                            tempval = SeekValue(Valtbl, Session("CountryList")(ICount), f.ColumnName)
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = ICount
                            'mti WebForm Changes From July 02, 2004 -end
                            tempval = RetrGroupVal(f.ColumnName, Session("SC_sAggregationI")) 'JRS 11/3/01 GROUPING
                        End If
                        'JRS 6/10/02 NULLS AND ZEROS
                        If optNullstoZeros.Checked And (IsDBNull(tempval) Or tempval.ToString = "") Then
                            tempval = 0
                        End If
                        'JRS 10/01/01 REPETITION OF POINTS If Not (IsNull(TempVal)) Then
                        If Not (IsDBNull(tempval) Or tempval.ToString = "") And Session("IndVarYears")(fieldsfound) <> f.ColumnName Then
                            CountriesFound = CountriesFound + 1
                            'JRS 10/05/01 This condition is to count only points
                            'available in all countries
                            'jrs 2007/02/23 to be consistent with logic for Dependent Variable If CountriesFound = Session("CountryTot") Then
                            fieldsfound = fieldsfound + 1
                            If fieldsfound >= Session("MaxDepIndYears") Then
                                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                                Msg = "You have chosen more countries and/or variables with "
                                Msg = Msg & "more data points than allowed.  Please reduce."

                                Session("PlotPossible") = False
                                'Valtbl.Close()
                                'db.Close()
                                'jrs 2023/09/22
                                'LoadMessage(Msg, Session("Message"))
                                'Support.FormManager.StateStore(Me)
                                'Support.FormManager.Show(Me, "frm_Message.aspx")
                                Response.Write("<script>alert('" & Msg & "');</script>")
                            End If
                            Session("IndVarYears")(fieldsfound) = f.ColumnName '.Name
                            'jrs 2007/02/23 End If 'end JRS 10/24/01
                        End If
                    Next ICount
                End If
            Next 'f
            inumpoints = fieldsfound
            'Valtbl.Close()
            'db.Close()

            '       Match up Years for Dependent and Independent Variables (if Not Years)
            '       ---------------------------------------------------------------------
            snumpoints = 0
            'jrs 2007/02/22
            'If doing Vars vs Time print all point available
            If rbTreatments.SelectedIndex = 2 Then  'MTI
                iloop = 1
                ILoop2 = 1
                Do While True
                    If iloop > dnumpoints And ILoop2 > inumpoints Then Exit Do

                    snumpoints = snumpoints + 1
                    If iloop > dnumpoints Then
                        Session("ScatVarYears")(snumpoints) = Session("IndVarYears")(ILoop2)
                        ILoop2 = ILoop2 + 1
                    ElseIf ILoop2 > inumpoints Then
                        Session("ScatVarYears")(snumpoints) = Session("DepVarYears")(iloop)
                        iloop = iloop + 1
                    ElseIf Session("DepVarYears")(iloop) = Session("IndVarYears")(ILoop2) Then
                        Session("ScatVarYears")(snumpoints) = Session("DepVarYears")(iloop)
                        iloop = iloop + 1
                        ILoop2 = ILoop2 + 1
                    ElseIf Session("DepVarYears")(iloop) < Session("IndVarYears")(ILoop2) Then
                        Session("ScatVarYears")(snumpoints) = Session("DepVarYears")(iloop)
                        iloop = iloop + 1
                    ElseIf Session("DepVarYears")(iloop) > Session("IndVarYears")(ILoop2) Then
                        Session("ScatVarYears")(snumpoints) = Session("IndVarYears")(ILoop2)
                        ILoop2 = ILoop2 + 1
                    End If
                Loop
            Else
                For iloop = 1 To dnumpoints
                    For ILoop2 = 1 To inumpoints
                        If Session("DepVarYears")(iloop) = Session("IndVarYears")(ILoop2) Then
                            snumpoints = snumpoints + 1
                            If snumpoints > Session("MaxDepIndYears") Then
                                'exceeds dimensionality of Session("ScatVarYears")
                                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                                Msg = "You have chosen more countries and/or variables with more "
                                Msg = Msg & "data points than allowed.  Please reduce."
                                Session("PlotPossible") = False
                                'jrs 2023/09/22
                                'LoadMessage(Msg, Session("Message"))
                                'Support.FormManager.StateStore(Me)
                                'Support.FormManager.Show(Me, "frm_Message.aspx")
                                Response.Write("<script>alert('" & Msg & "');</script>")
                            End If
                            Session("ScatVarYears")(snumpoints) = Session("DepVarYears")(iloop)
                        End If
                    Next ILoop2
                Next iloop
            End If

            If snumpoints * Session("CountryTot") * 2 > MaxVariables Then 'exceeds dimensionality of PLOT
                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Msg = "You have chosen more countries and/or variables with more "
                Msg = Msg & "data points than allowed.  Please reduce."
                Session("PlotPossible") = False
                'jrs 2023/09/22
                'LoadMessage(Msg, Session("Message"))
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")
                Response.Write("<script>alert('" & Msg & "');</script>")
            End If
            Session("DisYear") = snumpoints
        End If

        '   Search data files for data to plot - scattergram
        '   ------------------------------------------------

        '    AccessFileName$ = PutFdrv$ + "Data\IFsHistSeries.MDB"
        If GroupName <> "Automobile" Then
            accessfilename = Session("IFsHistSeries_MDB")
        Else
            accessfilename = Session("IFsCoVaTrASeries_MDB")
        End If

        If Session("LV_lbl_Ind") <> "TIME" Then 'Scattergram with time points
            'MTI
            If rbTreatments.Items(2).Selected Then  'MTI

                Session("GraphStyle") = 0
                Session("GraphType") = 6
                Session("GType") = "LINE"
                'JRS 11/18/02 COUNTE = 2
                Session("PlotSize") = 2
                Session("TableTitle") = Session("CountryList")(1) & " (" & Session("LV_lbl_Dep") & " and " & Session("LV_lbl_Ind") & ")"

                'Set db = OpenDatabase(accessfilename$)
                If GroupName <> "Automobile" Then
                    'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                    db = accessfilename
                    'jrs 2024/02/09 added []
                        SetDataTablefromAccess(db, "SELECT * FROM [" & "Series" + Session("LV_lbl_Ind") & "]", "Country", Valtbl, DataPW)
                Else
                    'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                    db = accessfilename
                    SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Ind"), "Country", Valtbl)
                End If


                ReDim Session("PlotLong")(snumpoints * Session("CountryTot"), 2)
                ReDim Session("PlotLabels")(2, snumpoints)
                For icount2 = 1 To snumpoints 'matched dep. and indep. years in Session("ScatVarYears")
                    'valtbl.Seek "=", CountryList$(ICount%)
                    '                Target$ = "Country = " + CountryList$(ICount%)
                    '               valtbl.FindFirst Target$
                    'JRS 11/03/01 GROUPING

                    'jrs 2008/05/19
                    If bCheckField(Session("ScatVarYears")(icount2), Valtbl) Then
                        'jrs 2008/05/15 If Not bGrouping Then
                        If Session("CountryListGroup")(1) <> 1 Then
                            'tempval = Valtbl.Fields(Session("ScatVarYears")(icount2)).Value
                            tempval = SeekValue(Valtbl, Session("CountryList")(1), CStr(Session("ScatVarYears")(icount2)))
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = 1
                            'mti WebForm Changes From July 02, 2004 -end
                            tempval = RetrGroupVal(Session("ScatVarYears")(icount2), Session("SC_sAggregationI")) 'JRS 11/3/01 GROUPING
                        End If
                    Else
                        tempval = DBNull.Value
                    End If

                    'line graph (Session("ScatVarYears") obtained from previous code)
                    If (IsDBNull(tempval) Or tempval.ToString = "") And optNullstoZeros.Checked Then tempval = 0 'Null Data Check
                    'JRS 11/17/02 PLOT(icount2%, 1) = TempVal
                    Session("PlotLong")(icount2, 1) = tempval
                    '1st dim carries x values; 2nd carries y values
                    Session("PlotLabels")(1, icount2) = Session("ScatVarYears")(icount2)
                Next icount2
                'Valtbl.Close()

                ' Dependent Variable = Plot(i,2)
                ' ------------------------------
                'Valtbl = db.OpenRecordset("Series" + Session("LV_lbl_Dep"))
                'Valtbl.Index = "Country"

                If GroupName <> "Automobile" Then
                    'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                    db = accessfilename
                    'jrs 2024/02/09 added []
                        SetDataTablefromAccess(db, "SELECT * FROM [" & "Series" + Session("LV_lbl_Dep") & "]", "Country", Valtbl, DataPW)
                Else
                    'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                    db = accessfilename
                    SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Dep"), "Country", Valtbl)
                End If

                'JRS 11/3/01 GROUPING
                'If Not bGrouping Then
                'Valtbl.Seek("=", CountryList(1))
                'Else
                'mti WebForm Changes From July 02, 2004 - start
                'LoadCountries((Session("CountryList")(1))) 'JRS 11/3/01 GROUPING
                'mti WebForm Changes From July 02, 2004 -end
                'End If

                For icount2 = 1 To snumpoints 'matched dep. and indep. years in Session("ScatVarYears")
                    '                Target$ = "Country = " + CountryList$(ICount%)
                    '               valtbl.FindFirst Target$
                    'JRS 11/03/01 GROUPING

                    'jrs 2008/05/19
                    If bCheckField(Session("ScatVarYears")(icount2), Valtbl) Then
                        'jrs 2008/05/15 If Not bGrouping Then
                        If Session("CountryListGroup")(1) <> 1 Then
                            'tempval = Valtbl.Fields(Session("ScatVarYears")(icount2)).Value
                            tempval = SeekValue(Valtbl, Session("CountryList")(1), CStr(Session("ScatVarYears")(icount2)))
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = 1
                            'mti WebForm Changes From July 02, 2004 -end
                            tempval = RetrGroupVal(Session("ScatVarYears")(icount2), Session("SC_sAggregationD")) 'JRS 11/3/01 GROUPING
                        End If
                    Else
                        tempval = DBNull.Value
                    End If

                    'line graph (Session("ScatVarYears") obtained from previous code)
                    If (IsDBNull(tempval) Or tempval.ToString = "") And optNullstoZeros.Checked Then tempval = 0 'Null Data Check
                    'JRS 11/17/02 PLOT(icount2%, 2) = TempVal
                    Session("PlotLong")(icount2, 2) = tempval
                    '1st dim carries x values; 2nd carries y values
                    Session("PlotLabels")(2, icount2) = Session("ScatVarYears")(icount2)
                Next icount2
                'Valtbl.Close()
                'JRS 2006/05/08
                ReOrderYearsForTimeGraph(snumpoints)

            Else 'MTI
                'MTI
                Session("GraphStyle") = 0
                Session("GraphType") = 9
                Session("GType") = "SCAT"
                'JRS 11/18/02 COUNTE = Session("DisYear")% * 2 * Session("CountryTot")
                'JRS 11/18/02 COUNTE = Session("DisYear")% * 2 * Session("CountryTot")
                Session("PlotSize") = Session("DisYear") * 2 'jrs 2008/05/16 * Session("CountryTot")
                Session("TableTitle") = Session("LV_lbl_Dep") & " and " & Session("LV_lbl_Ind") & " (Panel)"
                'JRS 11/17/02
                'jrs 2008/05/16
                'ReDim Session("PlotLong")(1, 2 * Session("CountryTot") * snumpoints)
                'ReDim Session("PlotLabels")(1, 2 * Session("CountryTot") * snumpoints)
                ReDim Session("PlotLong")(Session("CountryTot"), 2 * snumpoints)
                ReDim Session("PlotLabels")(Session("CountryTot"), 2 * snumpoints)

                For icount2 = 1 To Session("CountryTot")
                    'Session("TableTitle") = Session("TableTitle") & " " & Session("CountryList")(icount2)
                    'If icount2 < Session("CountryTot") Then
                    '    Session("TableTitle") = Session("TableTitle") & ","
                    'End If

                    If GroupName <> "Automobile" Then
                        'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                        db = accessfilename
                        SetDataTablefromAccess(db, "SELECT * FROM " & "[Series" + Session("LV_lbl_Ind") & "]", "Country", Valtbl, DataPW) 'jrs 2014/03/04 added [] to help with vars like PopulationUrban%
                    Else
                        'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                        db = accessfilename
                        SetDataTablefromAccess(db, "SELECT * FROM " & "[Series" + Session("LV_lbl_Ind") & "]", "Country", Valtbl) 'jrs 2014/03/04 added [] to help with vars like PopulationUrban%
                    End If

                    'JRS 11/3/01 GROUPING
                    'If Not bGrouping Then
                    'Valtbl.Seek("=", CountryList(icount2))
                    'Else
                    'mti WebForm Changes From July 02, 2004 -start
                    'LoadCountries((Session("CountryList")(icount2))) 'JRS 11/3/01 GROUPING
                    'mti WebForm Changes From July 02, 2004 -end
                    'End If

                    '               Target$ = "Country = " + CountryList$(icount2%)
                    '               valtbl.FindFirst Target$

                    For ICount = 1 To snumpoints
                        'JRS 11/03/01 GROUPING
                        'jrs 2008/05/15 If Not bGrouping Then
                        If Session("CountryListGroup")(icount2) <> 1 Then
                            'TempValI = Valtbl.Fields(Session("ScatVarYears")(ICount)).Value
                            TempValI = SeekValue(Valtbl, Session("CountryList")(icount2), CStr(Session("ScatVarYears")(ICount)))
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = icount2
                            'mti WebForm Changes From July 02, 2004 - end
                            TempValI = RetrGroupVal(Session("ScatVarYears")(ICount), Session("SC_sAggregationI")) 'JRS 11/3/01 GROUPING
                        End If
                        If (IsDBNull(TempValI) Or TempValI.ToString = "") And optNullstoZeros.Checked Then TempValI = 0 'Null Data Check
                        'JRS 11/17/02 PLOT(1, (icount2% - 1) * snumpoints + ICount%) = TempValI
                        'jrs 2008/05/16 Session("PlotLong")(1, (icount2 - 1) * snumpoints + ICount) = TempValI
                        Session("PlotLong")(icount2, ICount) = TempValI
                        '1st dim carries x values; 2nd carries y values
                        'jrs 2008/05/16 Session("PlotLabels")(1, (icount2 - 1) * snumpoints + ICount) = Session("CountryList")(icount2) & "(" + Session("ScatVarYears")(ICount) + ")"
                        If Val(Session("ScatVarYears")(ICount)) > 0 Then
                            Session("PlotLabels")(icount2, ICount) = Mid(Session("ScatVarYears")(ICount), 3, 2)
                        Else
                            Select Case UCase(Session("ScatVarYears")(ICount))
                                Case "EARLIEST"
                                    Session("PlotLabels")(icount2, ICount) = "Earl."
                                Case "MOSTRECENT"
                                    Session("PlotLabels")(icount2, ICount) = "Rcnt."
                                Case Else
                                    Session("PlotLabels")(icount2, ICount) = Mid(Session("ScatVarYears")(ICount), 1, 2) '+ ")"
                            End Select

                        End If

                    Next ICount
                    'Valtbl.Close()

                    'Set db = OpenDatabase(accessfilename$)
                    'If GroupName <> "Automobile" Then
                    '    db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                    'Else
                    '    db = DAODBEngine_definst.OpenDatabase(accessfilename)
                    'End If

                    'Valtbl = db.OpenRecordset("Series" + Session("LV_lbl_Dep")) 'Dependent Variable Read
                    'Valtbl.Index = "Country" '"PrimaryKey"

                    If GroupName <> "Automobile" Then
                        'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                        db = accessfilename
                        SetDataTablefromAccess(db, "SELECT * FROM [" & "Series" + Session("LV_lbl_Dep") & "]", "Country", Valtbl, DataPW)
                    Else
                        'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                        db = accessfilename
                        SetDataTablefromAccess(db, "SELECT * FROM [" & "Series" + Session("LV_lbl_Dep") & "]", "Country", Valtbl)
                    End If

                    'JRS 11/3/01 GROUPING
                    'If Not bGrouping Then
                    'Valtbl.Seek("=", CountryList(icount2))
                    'Else
                    'mti WebForm Changes From July 02, 2004-start
                    'LoadCountries((Session("CountryList")(icount2))) 'JRS 11/3/01 GROUPING
                    'mti WebForm Changes From July 02, 2004 -end 
                    'End If

                    '               Target$ = "Country = " + CountryList$(icount2%)
                    '               valtbl.FindFirst Target$
                    For ICount = 1 To snumpoints
                        'JRS 11/03/01 GROUPING
                        'jrs 2008/05/15 If Not bGrouping Then
                        If Session("CountryListGroup")(icount2) <> 1 Then
                            'TempValD = Valtbl.Fields(Session("ScatVarYears")(ICount)).Value
                            TempValD = SeekValue(Valtbl, Session("CountryList")(icount2), CStr(Session("ScatVarYears")(ICount)))
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = icount2
                            'mti WebForm Changes From July 02, 2004 -end
                            TempValD = RetrGroupVal(Session("ScatVarYears")(ICount), Session("SC_sAggregationD")) 'JRS 11/3/01 GROUPING
                        End If
                        If (IsDBNull(TempValD) Or TempValD.ToString = "") And optNullstoZeros.Checked Then TempValD = 0
                        'jrs 2008/05/16 index = snumpoints * Session("CountryTot") + (icount2 - 1) * snumpoints + ICount
                        index = snumpoints + ICount
                        'JRS 11/17/02 PLOT(1, index) = TempValD
                        'jrs 2008/05/16 Session("PlotLong")(1, index) = TempValD
                        Session("PlotLong")(icount2, index) = TempValD
                        '1st dim carries x values; 2nd carries y values
                    Next ICount
                    'Valtbl.Close()
                Next icount2
            End If
        Else

            '   Search data files for data to plot - against time
            '   -------------------------------------------------
            Session("GraphStyle") = 0
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("GraphType"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("GraphType") = 6
            Session("GType") = "LINE"
            'JRS 11/18/02 COUNTE = Session("CountryTot")
            Session("PlotSize") = Session("CountryTot")
            Session("TableTitle") = Session("LV_lbl_Dep") & " versus " & Session("LV_lbl_Ind")

            If dnumpoints > MaxYears Then 'Exceeds dimensionality of PLOT
                Msg = "You have chosen more countries and/or variables with more "
                Msg = Msg & "data points than allowed.  Please reduce."
                Session("PlotPossible") = False
                'jrs 2023/09/22
                'LoadMessage(Msg, Session("Message"))
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")
                Response.Write("<script>alert('" & Msg & "');</script>")
            End If

            'JRS 3/17/02
            ReOrderYears((dnumpoints))

            'Set db = OpenDatabase(accessfilename$)
            'If GroupName <> "Automobile" Then
            '    db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
            'Else
            '    db = DAODBEngine_definst.OpenDatabase(accessfilename)
            'End If

            'Valtbl = db.OpenRecordset("Series" + Session("LV_lbl_Dep"))
            'Valtbl.Index = "Country"

            If GroupName <> "Automobile" Then
                'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                db = accessfilename
                SetDataTablefromAccess(db, "SELECT * FROM [" & "Series" + Session("LV_lbl_Dep") & "]", "Country", Valtbl, DataPW)
            Else
                'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                db = accessfilename
                SetDataTablefromAccess(db, "SELECT * FROM [" & "Series" + Session("LV_lbl_Dep") & "]", "Country", Valtbl)
            End If

            'JRS 11/17/02
            ReDim Session("PlotLong")(dnumpoints, Session("CountryTot"))
            ReDim Session("PlotLabels")(1, dnumpoints)

            For ICount = 1 To Session("CountryTot")
                'JRS 11/3/01 GROUPING
                'If Not bGrouping Then
                'Valtbl.Seek("=", CountryList(ICount))
                'Else
                'mti WebForm Changes From July 02, 2004 -start
                'LoadCountries((Session("CountryList")(ICount))) 'JRS 11/3/01 GROUPING
                'mti WebForm Changes From July 02, 2004 -end 
                'End If

                For icount2 = 1 To dnumpoints
                    '                Target$ = "Country = " + CountryList$(ICount%)
                    '               valtbl.FindFirst Target$
                    'JRS 11/03/01 GROUPING
                    'jrs 2008/05/15 If Not bGrouping Then
                    If Session("CountryListGroup")(ICount) <> 1 Then
                        'tempval = Valtbl.Fields(Session("DepVarYears")(icount2)).Value
                        tempval = SeekValue(Valtbl, Session("CountryList")(ICount), CStr(Session("DepVarYears")(icount2)))
                    Else
                        'mti WebForm Changes From July 02, 2004-start
                        cgcount = ICount
                        'mti WebForm Changes From July 02, 2004 -end
                        tempval = RetrGroupVal(Session("DepVarYears")(icount2), Session("SC_sAggregationD")) 'JRS 11/3/01 GROUPING
                    End If

                    If (IsDBNull(tempval) Or tempval.ToString = "") And optNullstoZeros.Checked Then tempval = 0 'Null Data Check
                    'JRS 11/17/02 PLOT(icount2%, ICount%) = TempVal
                    Session("PlotLong")(icount2, ICount) = tempval
                    '1st dim carries x values; 2nd carries y values
                    Session("PlotLabels")(1, icount2) = Session("DepVarYears")(icount2)
                Next icount2
            Next ICount
            'Valtbl.Close()
            'JRS 10/17/01
            'db.Close()
        End If
        Exit Sub

FileNotFoundYearSearch:
        Msg = "The selected variable was not found.  Please try another."
        'JRS 11/26/03
        Session("PlotPossible") = False
        'jrs 2023/09/22
        'LoadMessage(Msg, Session("Message"))
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")
        Response.Write("<script>alert('" & Msg & "');</script>")
    End Sub

    '=============================LongVarS.Frm==============================
    Private Sub CohortBasedData()
        Dim tempval As Object
        Dim index As Object
        Dim TempValD As Object
        Dim TempValI As Object
        Dim TableName As String
        Dim ICount As Short
        Dim icount2 As Short
        Dim snumpoints As Object
        Dim accessfilename As String

        'mtiwebform June 2004 Longitudinal
        'Dim db As String
        'Dim Valtbl As DataTable
        Dim Rcnt As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        accessfilename = Session("IFsWVSCohort_MDB")
        Session("DisYear") = NWVSWaves '4 waves
        snumpoints = NWVSWaves '4 waves

        'jrs 2008/05/19
        'Grouping
        'jrs 2008/05/15 If bGrouping Then
        For ICount = 1 To Session("CountryTot")
            If Session("CountryListGroup")(ICount) = 1 Then
                'mti WebForm Changes From July 02, 2004 -start
                cgcount = ICount
                'mti WebForm Changes From July 02, 2004 -end
                LoadCountries((Session("CountryList")(ICount))) 'JRS 11/3/01 GROUPING
            End If
        Next


        '   Search data files for data to plot - scattergram
        '   ------------------------------------------------
        'check if as time MTI
        If Session("LV_lbl_Ind") <> "TIME" Then 'Scattergram with WVS Wave points
            'jrs 2024/04/29
            If rbTreatments.Items(2).Selected Then
                Session("WVSTIME") = True
            Else
                Session("WVSTIME") = False
            End If
            Session("GraphStyle") = 0
            Session("GraphType") = 9
            Session("GType") = "SCAT"
            'JRS 11/18/02 COUNTE = Session("DisYear")% * 2 * Session("CountryTot")
            'JRS 11/18/02 COUNTE = Session("DisYear")% * 2 * Session("CountryTot")
            Session("PlotSize") = Session("DisYear") * 2 * Session("CountryTot")
            Session("TableTitle") = ""
            'JRS 11/17/02
            ReDim Session("PlotLong")(1, 2 * Session("CountryTot") * snumpoints)
            ReDim Session("PlotLabels")(1, 2 * Session("CountryTot") * snumpoints)
            For icount2 = 1 To Session("CountryTot")
                Session("TableTitle") = Session("TableTitle") & " " & Session("CountryList")(icount2)
                If icount2 < Session("CountryTot") Then
                    Session("TableTitle") = Session("TableTitle") & ","
                End If
                'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                db = accessfilename
                For ICount = 1 To snumpoints 'Waves
                    'jrs 2024/04/29 TableName = "Series" + Session("LV_lbl_Ind") & "W" & Trim(Str(ICount))
                    TableName = Session("LV_lbl_Ind") & "W" & Trim(Str(ICount))
                    On Error GoTo TableNotFound1
                    'valtbl = db.OpenRecordset(TableName) 'Independent Variable/Wave Read
                    'Valtbl.Index = "Country" '"PrimaryKey"
                    SetDataTablefromAccess(db, "SELECT * FROM " & TableName, "Country", Valtbl)
                    'valtbl.Seek("=", CountryList(icount2))
                    '                Target$ = "Country = " + CountryList$(icount2%)
                    '                valtbl.FindFirst Target$

                    'TempValI = Valtbl.Fields("Total").Value
                    'jrs 2008/05/19
                    If Session("CountryListGroup")(icount2) <> 1 Then
                        TempValI = SeekValue(Valtbl, Session("CountryList")(icount2), "Total")
                    Else
                        cgcount = icount2
                        TempValI = RetrGroupVal("Total", Session("SC_sAggregationI"))
                    End If
                    'jrs 2008/05/19 added optNullstoZeros
                    If optNullstoZeros.Checked And (IsDBNull(TempValI) Or TempValI.ToString = "") Then TempValI = 0 'Null Data Check

                    If Session("bComputationActive") Then
                        Session("PLOTAll")(1, (icount2 - 1) * snumpoints + ICount) = TempValI
                    Else
                        'JRS 11/17/02 PLOT(1, (icount2% - 1) * snumpoints + ICount%) = TempValI
                        Session("PlotLong")(1, (icount2 - 1) * snumpoints + ICount) = TempValI
                    End If
                    '1st dim carries x values; 2nd carries y values
                    Session("PlotLabels")(1, (icount2 - 1) * snumpoints + ICount) = Session("CountryList")(icount2) & "(" & "W" & Trim(Str(ICount)) & ")"
                    'valtbl.Close()

                    'JRS 01/29/04 *** BEGIN ***
                    GoTo TryNextWave1
TableNotFound1:
                    'jrs 2008/05/19
                    If optNullstoZeros.Checked Then
                        TempValI = 0
                    Else
                        TempValI = System.DBNull.Value
                    End If
                    If Session("bComputationActive") Then
                        Session("PLOTAll")(1, (icount2 - 1) * snumpoints + ICount) = TempValI
                    Else
                        'JRS 11/17/02 PLOT(1, (icount2% - 1) * snumpoints + ICount%) = TempValI
                        Session("PlotLong")(1, (icount2 - 1) * snumpoints + ICount) = TempValI
                    End If
                    '1st dim carries x values; 2nd carries y values
                    Session("PlotLabels")(1, (icount2 - 1) * snumpoints + ICount) = Session("CountryList")(icount2) & "(" & "W" & Trim(Str(ICount)) & ")"
                    Resume TryNextWave1
                    'JRS 01/29/04 *** END ***
TryNextWave1:
                Next ICount

                'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                db = accessfilename
                For ICount = 1 To snumpoints 'Waves
                    TableName = Session("LV_lbl_Dep") & "W" & Trim(Str(ICount))
                    On Error GoTo TableNotFound2
                    'valtbl = db.OpenRecordset(TableName) 'Dependent Variable/Wave Read
                    'Valtbl.Index = "Country" '"PrimaryKey"
                    SetDataTablefromAccess(db, "SELECT * FROM " & TableName, "Country", Valtbl)


                    'valtbl.Seek("=", CountryList(icount2))
                    '                Target$ = "Country = " + CountryList$(icount2%)
                    '                valtbl.FindFirst Target$

                    'TempValD = Valtbl.Fields("Total").Value
                    'jrs 2008/05/19
                    If Session("CountryListGroup")(icount2) <> 1 Then
                        TempValD = SeekValue(Valtbl, Session("CountryList")(icount2), "Total")
                    Else
                        cgcount = icount2
                        TempValD = RetrGroupVal("Total", Session("SC_sAggregationD"))
                    End If
                    'jrs 2008/05/19
                    If optNullstoZeros.Checked And (IsDBNull(TempValD) Or TempValD.ToString = "") Then TempValD = 0

                    index = snumpoints * Session("CountryTot") + (icount2 - 1) * snumpoints + ICount
                    If Session("bComputationActive") Then
                        Session("PLOTAll")(1, index) = TempValD
                    Else
                        'JRS 11/17/02 PLOT(1, index) = TempValD
                        Session("PlotLong")(1, index) = TempValD
                    End If
                    '1st dim carries x values; 2nd carries y values
                    'Valtbl.Close()

                    'JRS 01/29/04 *** BEGIN ***
                    GoTo TryNextWave2
TableNotFound2:
                    'jrs 2008/05/19
                    If optNullstoZeros.Checked Then
                        TempValD = 0
                    Else
                        TempValD = System.DBNull.Value
                    End If
                    index = snumpoints * Session("CountryTot") + (icount2 - 1) * snumpoints + ICount
                    If Session("bComputationActive") Then
                        Session("PLOTAll")(1, index) = TempValD
                    Else
                        'JRS 11/17/02 PLOT(1, index) = TempValD
                        Session("PlotLong")(1, index) = TempValD
                    End If
                    Resume TryNextWave2
                    'JRS 01/29/04 *** END ***
TryNextWave2:
                Next ICount
            Next icount2
        Else

            '   Search data files for data to plot - against waves
            '   --------------------------------------------------
            Session("GraphStyle") = 0
            Session("GraphType") = 6
            Session("GType") = "LINE"
            'JRS 11/18/02 COUNTE = Session("CountryTot")
            Session("PlotSize") = Session("CountryTot")
            Session("TableTitle") = Session("LV_lbl_Dep") & " versus " & "WVS Wave"

            'db = DAODBEngine_definst.OpenDatabase(accessfilename)
            db = accessfilename
            'JRS 11/17/02
            ReDim Session("PlotLong")(snumpoints, Session("CountryTot"))
            'ReDim Session("PlotLabels")(snumpoints, Session("CountryTot")) As String
            ReDim Session("PlotLabels")(Session("CountryTot"), snumpoints) 'bbh 12/10/02
            For icount2 = 1 To snumpoints '4 waves
                TableName = Session("LV_lbl_Dep") & "W" & Trim(Str(icount2))
                On Error GoTo TableNotFound3
                'valtbl = db.OpenRecordset(TableName)
                'Valtbl.Index = "Country"
                SetDataTablefromAccess(db, "SELECT * FROM " & TableName, "Country", Valtbl)
                'jrs 2022/08/16
                If IsNothing(Valtbl) Then GoTo TableNotFound3
                For ICount = 1 To Session("CountryTot")
                    'valtbl.Seek("=", CountryList(ICount))
                    '               Target$ = "Country = " + CountryList$(ICount%)
                    '               valtbl.FindFirst Target$

                    'tempval = Valtbl.Fields("Total").Value
                    'jrs 2008/05/19
                    If Session("CountryListGroup")(ICount) <> 1 Then
                        tempval = SeekValue(Valtbl, Session("CountryList")(ICount), "Total")
                    Else
                        cgcount = icount2
                        tempval = RetrGroupVal("Total", Session("SC_sAggregationD"))
                    End If
                    If optNullstoZeros.Checked And (IsDBNull(tempval) Or tempval.ToString = "") Then tempval = 0 'Null Data Check

                    'JRS 11/10/01 COMPUTATIONS
                    If Session("bComputationActive") Then
                        Session("PLOTAll")(icount2, ICount) = tempval
                    Else
                        'JRS 11/17/01 PLOT(icount2%, ICount%) = TempVal
                        Session("PlotLong")(icount2, ICount) = tempval
                    End If
                    '1st dim carries x values (i.e. WVS wave);
                    '2nd carries y values (i.e. country)
                    Session("PlotLabels")(1, icount2) = "W" & Trim(Str(icount2))
                Next ICount
                'valtbl.Close()
                'JRS 01/29/04 *** BEGIN ***
                GoTo TryNextWave3
TableNotFound3:
                'jrs 2008/05/18
                If optNullstoZeros.Checked Then
                    tempval = 0
                Else
                    tempval = System.DBNull.Value
                End If
                For ICount = 1 To Session("CountryTot")
                    If Session("bComputationActive") Then
                        Session("PLOTAll")(1, index) = TempValD
                    Else
                        'JRS 11/17/02 PLOT(icount2%, ICount%) = TempVal
                        Session("PlotLong")(icount2, ICount) = tempval
                    End If
                    '1st dim carries x values (i.e. WVS wave);
                    '2nd carries y values (i.e. country)
                    Session("PlotLabels")(1, icount2) = "W" & Trim(Str(icount2))
                Next ICount
                Resume TryNextWave3
                'JRS 01/29/04 *** END ***
TryNextWave3:
            Next icount2
        End If

    End Sub

    Private Sub ResetComputationsFrm()
        'JRS 9/25/01 COMPUTATIONS
        Session("ComptxtFormula") = ""
        Session("ComplblFormulaName") = "Formula Name?"
        Session("ComplblDimensionality") = "Dimensionality?"
        Session("ComplblUnits") = "Units?"
    End Sub

    Private Sub UpdateCurrentDisplay()
        'JRS 9/25/01 COMPUTATIONS
        Select Case True
            Case Me.opt_Independent.Checked
                sCurrentDisplay = lbl_Ind.Text
                iCurrentIdx = 2
            Case Me.opt_Dependent.Checked
                sCurrentDisplay = lbl_Dep.Text
                iCurrentIdx = 1
        End Select

    End Sub

    Public Sub ReLoadYearBaseData(ByVal sUnit As String)
        Dim index As Object
        Dim TempValD As Object
        Dim TempValI As Object
        Dim ICount As Short
        Dim tempval As Object
        Dim icount2 As Short
        Dim snumpoints As Object
        Dim dnumpoints As Object
        Dim accessfilename As String
        'JRS 11/03/01 GROUPING
        Dim bGrouping As Boolean
        'mti WebForm Changes From July 02, 2004 - start
        bGrouping = Session("bCOuntSelGrouping") 'Not (cmdUseGroups.Text = "Use Groups")


        'jrs 2008/05/16 If bGrouping Then
        For ICount = 1 To Session("CountryTot")
            If Session("CountryListGroup")(ICount) = 1 Then
                'mti WebForm Changes From July 02, 2004 -start
                cgcount = ICount
                'mti WebForm Changes From July 02, 2004 -end
                LoadCountries((Session("CountryList")(ICount))) 'JRS 11/3/01 GROUPING
            End If
        Next
        'End If
        'mti WebForm Changes From July 02, 2004        '   Search data files for data to plot - scattergram
        '   ------------------------------------------------

        '    AccessFileName$ = PutFdrv$ + "Data\IFsHistSeries.MDB"
        If GroupName <> "Automobile" Then
            accessfilename = Session("IFsHistSeries_MDB") '+ ", False, False," + DataPW$
        Else
            accessfilename = Session("IFsCoVaTrASeries_MDB")
        End If



        dnumpoints = Session("DisYear")
        snumpoints = Session("DisYear")
        Dim iVarComp As Short

        'mtiwebform June 2004 Longitudinal - commented computation related code
        iVarComp = Session("VarComp")
        ''JRS 11/26/02
        'sUnits = frm_computations.DefInstance.Formula.Unit
        'end - mti webform June 2004 Longitudinal - commented computation related code
        'jrs 2006/05/09
        'JRS 11/26/02
        Session("LV_sUnits") = sUnit

        If Session("LV_lbl_Ind") <> "TIME" Then 'Scattergram with time points
            If rbTreatments.SelectedIndex = 2 Then


                If GroupName <> "Automobile" Then
                    'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                    db = accessfilename
                    SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Ind"), "Country", Valtbl, DataPW)
                Else
                    'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                    db = accessfilename
                    SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Ind"), "Country", Valtbl)
                End If

                'JRS 11/3/01 GROUPING
                'If Not bGrouping Then
                'valtbl.Seek("=", CountryList(1)) '1 country only
                'Else
                'mti WebForm Changes From July 02, 2004 -start
                'LoadCountries((Session("CountryList")(1))) 'JRS 11/3/01 GROUPING
                'End If
                '            Target$ = "Country = " + CountryList$(icount2%)
                '            valtbl.FindFirst Target$
                'JRS 11/17/02
                'jrs 2006/10/24 added preserve
                ReDim Preserve Session("PlotLong")(snumpoints * Session("CountryTot"), 2)
                ReDim Session("PlotLabels")(2, snumpoints)
                For icount2 = 1 To snumpoints 'matched dep. and indep. years in Session("ScatVarYears")
                    'valtbl.Seek "=", CountryList$(ICount%)
                    '                Target$ = "Country = " + CountryList$(ICount%)
                    '               valtbl.FindFirst Target$

                    'jrs 2008/05/20
                    If bCheckField(Session("ScatVarYears")(icount2), Valtbl) Then
                        'JRS 11/03/01 GROUPING                        
                        'jrs 2008/05/16 If Not bGrouping Then
                        If Session("CountryListGroup")(1) <> 1 Then
                            'tempval = valtbl.Fields(Session("ScatVarYears")(icount2)).Value
                            tempval = SeekValue(Valtbl, Session("CountryList")(1), CStr(Session("ScatVarYears")(icount2)))
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = 1 'ICount
                            'mti WebForm Changes From July 02, 2004 -end
                            tempval = RetrGroupVal(Session("ScatVarYears")(icount2), Session("SC_sAggregationI")) 'JRS 11/3/01 GROUPING
                        End If
                    Else
                        tempval = System.DBNull.Value
                    End If
                    'line graph (Session("ScatVarYears") obtained from previous code)

                    'jrs 2008/05/15 added optNullstoZeros.Checked And 
                    If optNullstoZeros.Checked And (IsDBNull(tempval) Or tempval.ToString = "") Then tempval = 0 'Null Data Check
                    If iVarComp <> 1 Then
                        Session("PLOTAll")(icount2, 2) = tempval
                    Else
                        'JRS 11/17/02 PLOT(icount2%, 1) = TempVal
                        Session("PlotLong")(icount2, 1) = tempval
                    End If
                    '1st dim carries x values; 2nd carries y values
                    Session("PlotLabels")(1, icount2) = Session("ScatVarYears")(icount2)
                Next icount2
                'valtbl.Close()

                ' Dependent Variable = Plot(i,2)
                ' ------------------------------
                'valtbl = db.OpenRecordset("Series" + Session("LV_lbl_Dep"))
                'valtbl.Index = "Country"
                If GroupName <> "Automobile" Then
                    'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                    db = accessfilename
                    SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Dep"), "Country", Valtbl, DataPW)
                Else
                    'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                    db = accessfilename
                    SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Dep"), "Country", Valtbl)
                End If

                'JRS 11/3/01 GROUPING
                'If Not bGrouping Then
                'valtbl.Seek("=", CountryList(1))
                'Else
                'mti WebForm Changes From July 02, 2004 -start
                'LoadCountries((Session("CountryList")(1))) 'JRS 11/3/01 GROUPING
                'End If

                For icount2 = 1 To snumpoints 'matched dep. and indep. years in Session("ScatVarYears")
                    '                Target$ = "Country = " + CountryList$(ICount%)
                    '               valtbl.FindFirst Target$

                    'jrs 2008/05/20
                    If bCheckField(Session("ScatVarYears")(icount2), Valtbl) Then
                        'JRS 11/03/01 GROUPING
                        'jrs 2008/05/16 If Not bGrouping Then
                        If Session("CountryListGroup")(1) <> 1 Then
                            'tempval = valtbl.Fields(Session("ScatVarYears")(icount2)).Value
                            tempval = SeekValue(Valtbl, Session("CountryList")(1), CStr(Session("ScatVarYears")(icount2)))
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = 1 'ICount
                            'mti WebForm Changes From July 02, 2004 -end
                            tempval = RetrGroupVal(Session("ScatVarYears")(icount2), Session("SC_sAggregationD")) 'JRS 11/3/01 GROUPING
                        End If
                    Else
                        'jrs 2010/09/06
                        tempval = System.DBNull.Value
                    End If

                    'line graph (Session("ScatVarYears") obtained from previous code)
                    'jrs 2008/05/16 added optNullstoZeros.Checked 
                    If optNullstoZeros.Checked And (IsDBNull(tempval) Or tempval.ToString = "") Then tempval = 0 'Null Data Check
                    If iVarComp <> 2 Then
                        Session("PLOTAll")(icount2, 1) = tempval
                    Else
                        'JRS 11/17/02 PLOT(icount2%, 2) = TempVal
                        Session("PlotLong")(icount2, 2) = tempval
                    End If
                    '1st dim carries x values; 2nd carries y values
                    Session("PlotLabels")(2, icount2) = Session("ScatVarYears")(icount2)
                Next icount2
                'valtbl.Close()
                'jrs 2006/05/09
                ReOrderYearsForTimeGraph(snumpoints)
            Else
                'JRS 11/18/02 COUNTE = Session("DisYear")% * 2 * Session("CountryTot")
                Session("PlotSize") = Session("DisYear") * 2 'jrs 2008/05/16 * Session("CountryTot")
                Session("TableTitle") = ""
                'JRS 11/17/02
                'JRS 2006/10/24 added preserve, we were loosing data when using points with formulas both in dep and Indep vars.
                'jrs 2008/05/16
                'ReDim Preserve Session("PlotLong")(1, 2 * Session("CountryTot") * snumpoints)
                'ReDim Session("PlotLabels")(1, 2 * Session("CountryTot") * snumpoints)
                ReDim Preserve Session("PlotLong")(Session("CountryTot"), 2 * snumpoints)
                ReDim Session("PlotLabels")(Session("CountryTot"), 2 * snumpoints)

                For icount2 = 1 To Session("CountryTot")
                    Session("TableTitle") = Session("TableTitle") & " " & Session("CountryList")(icount2)
                    If icount2 < Session("CountryTot") Then
                        Session("TableTitle") = Session("TableTitle") & ","
                    End If
                    'Set db = OpenDatabase(accessfilename$)
                    'If GroupName <> "Automobile" Then
                    '    db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                    'Else
                    '    db = DAODBEngine_definst.OpenDatabase(accessfilename)
                    'End If

                    'valtbl = db.OpenRecordset("Series" + Session("LV_lbl_Ind")) 'Independent Variable Read
                    'valtbl.Index = "Country" '"PrimaryKey"

                    If GroupName <> "Automobile" Then
                        'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                        db = accessfilename
                        SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Ind"), "Country", Valtbl, DataPW)
                    Else
                        'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                        db = accessfilename
                        SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Ind"), "Country", Valtbl)
                    End If
                    'JRS 11/3/01 GROUPING
                    'If Not bGrouping Then
                    'valtbl.Seek("=", CountryList(icount2))
                    'Else
                    'mti WebForm Changes From July 02, 2004 -start
                    'LoadCountries((Session("CountryList")(icount2))) 'JRS 11/3/01 GROUPING
                    'End If

                    '               Target$ = "Country = " + CountryList$(icount2%)
                    '               valtbl.FindFirst Target$

                    For ICount = 1 To snumpoints
                        'JRS 11/03/01 GROUPING
                        'jrs 2008/05/16 If Not bGrouping Then
                        If Session("CountryListGroup")(icount2) <> 1 Then
                            'TempValI = valtbl.Fields(Session("ScatVarYears")(ICount)).Value
                            TempValI = SeekValue(Valtbl, Session("CountryList")(icount2), CStr(Session("ScatVarYears")(ICount)))
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = icount2
                            'mti WebForm Changes From July 02, 2004 -end
                            TempValI = RetrGroupVal(Session("ScatVarYears")(ICount), Session("SC_sAggregationI")) 'JRS 11/3/01 GROUPING
                        End If
                        'jrs 2008/05/16 added optNullstoZeros
                        If optNullstoZeros.Checked And (IsDBNull(TempValI) Or TempValI.ToString = "") Then TempValI = 0 'Null Data Check
                        If iVarComp <> 1 Then
                            Session("PLOTAll")((icount2 - 1) * snumpoints + ICount, 2) = TempValI
                        Else
                            'JRS 11/17/02 PLOT(1, (icount2% - 1) * snumpoints + ICount%) = TempValI
                            'jrs 2008/05/16 Session("PlotLong")(1, (icount2 - 1) * snumpoints + ICount) = TempValI
                            Session("PlotLong")(icount2, ICount) = TempValI
                        End If
                        '1st dim carries x values; 2nd carries y values
                        'jrs 2008/05/16 Session("PlotLabels")(1, (icount2 - 1) * snumpoints + ICount) = Session("CountryList")(icount2) & "(" + Session("ScatVarYears")(ICount) + ")"
                        If Val(Session("ScatVarYears")(ICount)) > 0 Then
                            Session("PlotLabels")(icount2, ICount) = Mid(Session("ScatVarYears")(ICount), 3, 2)
                        Else
                            Select Case UCase(Session("ScatVarYears")(ICount))
                                Case "EARLIEST"
                                    Session("PlotLabels")(icount2, ICount) = "Earl."
                                Case "MOSTRECENT"
                                    Session("PlotLabels")(icount2, ICount) = "Rcnt."
                                Case Else
                                    Session("PlotLabels")(icount2, ICount) = Mid(Session("ScatVarYears")(ICount), 1, 2) '+ ")"
                            End Select

                        End If

                    Next ICount
                    'valtbl.Close()

                    'Set db = OpenDatabase(accessfilename$)
                    'If GroupName <> "Automobile" Then
                    '    db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                    'Else
                    '    db = DAODBEngine_definst.OpenDatabase(accessfilename)
                    'End If

                    'valtbl = db.OpenRecordset("Series" + Session("LV_lbl_Dep")) 'Dependent Variable Read
                    'valtbl.Index = "Country" '"PrimaryKey"

                    If GroupName <> "Automobile" Then
                        'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                        db = accessfilename
                        SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Dep"), "Country", Valtbl, DataPW)
                    Else
                        'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                        db = accessfilename
                        SetDataTablefromAccess(db, "SELECT * FROM " & "Series" + Session("LV_lbl_Dep"), "Country", Valtbl)
                    End If

                    'JRS 11/3/01 GROUPING
                    'If Not bGrouping Then
                    'valtbl.Seek("=", CountryList(icount2))
                    'Else
                    'mti WebForm Changes From July 02, 2004 -start
                    'LoadCountries((Session("CountryList")(icount2))) 'JRS 11/3/01 GROUPING
                    'End If
                    '               Target$ = "Country = " + CountryList$(icount2%)
                    '               valtbl.FindFirst Target$
                    For ICount = 1 To snumpoints
                        'JRS 11/03/01 GROUPING
                        'jrs 2008/05/16 If Not bGrouping Then
                        If Session("CountryListGroup")(icount2) <> 1 Then
                            'TempValD = valtbl.Fields(Session("ScatVarYears")(ICount)).Value
                            TempValD = SeekValue(Valtbl, Session("CountryList")(icount2), CStr(Session("ScatVarYears")(ICount)))
                        Else
                            'mti WebForm Changes From July 02, 2004 -start
                            cgcount = icount2
                            'mti WebForm Changes From July 02, 2004 -end
                            TempValD = RetrGroupVal(Session("ScatVarYears")(ICount), Session("SC_sAggregationD")) 'JRS 11/3/01 GROUPING
                        End If
                        'jrs 2008/05/16 added optNullstoZeros
                        If optNullstoZeros.Checked And (IsDBNull(TempValD) Or TempValD.ToString = "") Then TempValD = 0
                        'jrs 2008/05/16 index = snumpoints * Session("CountryTot") + (icount2 - 1) * snumpoints + ICount
                        index = snumpoints + ICount
                        If iVarComp <> 2 Then
                            Session("PLOTAll")((icount2 - 1) * snumpoints + ICount, 1) = TempValD
                        Else
                            'JRS 11/17/02 PLOT(1, index) = TempValD
                            'jrs 2008/05/16 Session("PlotLong")(1, index) = TempValD
                            Session("PlotLong")(icount2, index) = TempValD
                        End If
                        '1st dim carries x values; 2nd carries y values
                    Next ICount
                    'valtbl.Close()
                Next icount2

            End If
        Else

            'Set db = OpenDatabase(accessfilename$)
            'If GroupName <> "Automobile" Then
            '    db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
            'Else
            '    db = DAODBEngine_definst.OpenDatabase(accessfilename)
            'End If

            'valtbl = db.OpenRecordset("Series" + Session("LV_lbl_Dep"))
            'valtbl.Index = "Country"

            If GroupName <> "Automobile" Then
                'db = DAODBEngine_definst.OpenDatabase(accessfilename, False, False, DataPW)
                db = accessfilename
                SetDataTablefromAccess(db, "SELECT * FROM [" & "Series" + Session("LV_lbl_Dep") & "]", "Country", Valtbl, DataPW)
            Else
                'db = DAODBEngine_definst.OpenDatabase(accessfilename)
                db = accessfilename
                SetDataTablefromAccess(db, "SELECT * FROM [" & "Series" + Session("LV_lbl_Dep") & "]", "Country", Valtbl)
            End If

            'JRS 11/25/02
            ReDim Session("PlotLabels")(2, snumpoints)
            'JRS 11/17/02
            For ICount = 1 To Session("CountryTot")
                'JRS 11/3/01 GROUPING
                'If Not bGrouping Then
                'valtbl.Seek("=", CountryList(ICount))
                'Else
                'mti WebForm Changes From July 02, 2004 -start
                'LoadCountries((Session("CountryList")(ICount))) 'JRS 11/3/01 GROUPING
                'End If
                For icount2 = 1 To dnumpoints
                    '                Target$ = "Country = " + CountryList$(ICount%)
                    '               valtbl.FindFirst Target$
                    'JRS 11/03/01 GROUPING
                    'jrs 2008/05/16 If Not bGrouping Then
                    If Session("CountryListGroup")(ICount) <> 1 Then
                        'tempval = valtbl.Fields(Session("DepVarYears")(icount2)).Value
                        tempval = SeekValue(Valtbl, Session("CountryList")(ICount), CStr(Session("DepVarYears")(icount2)))
                    Else
                        'mti WebForm Changes From July 02, 2004 -start
                        cgcount = ICount
                        'mti WebForm Changes From July 02, 2004 -end 
                        tempval = RetrGroupVal(Session("DepVarYears")(icount2), Session("SC_sAggregationD")) 'JRS 11/3/01 GROUPING
                    End If
                    'jrs 2008/05/16 added optNullstoZeros
                    If optNullstoZeros.Checked And (IsDBNull(tempval) Or tempval.ToString = "") Then tempval = 0 'Null Data Check
                    'JRS 10/05/01 Changes to add Computations
                    Session("PLOTAll")(icount2, ICount) = tempval
                    '1st dim carries x values; 2nd carries y values
                    Session("PlotLabels")(1, icount2) = Session("DepVarYears")(icount2)
                Next icount2
            Next ICount
            'valtbl.Close()
            'JRS 10/17/01
            'db.Close()

            Session("TableTitle") = lbl_Dep.Text & " versus " & Session("LV_lbl_Ind")
        End If
    End Sub

    Private Sub LocalSetVarComp()


        Dim bIndepCond As Boolean
        Dim iVarComp As Short
        Dim final As Short
        bIndepCond = (rbTreatments.SelectedIndex <> 0 And opt_Dependent.Checked And bLastIndep)
        iVarComp = Session("VarComp")

        If iVarComp <> 0 Then
            If bIndepCond Then
                Select Case iVarComp
                    Case 3
                        final = 1
                    Case 2
                        final = 0
                    Case Else
                        final = iVarComp
                End Select
            Else
                Select Case iVarComp
                    Case 3
                        final = 2
                    Case 1
                        final = 0
                    Case Else
                        final = iVarComp
                End Select
            End If
            Session("VarComp") = final

            If final = 0 Then
                Session("bComputationActive") = False
            End If
        End If
    End Sub

    ReadOnly Property LastInd() As Boolean
        Get
            LastInd = bLastIndep
        End Get
    End Property

    Private Sub LoadData(ByRef LocalIdx As Short)
        Dim CompVarName As Object
        Dim CurrCompVar As Object
        Dim NumbCompVar As Object
        'JRS COMPUTATION
        'jrs 2005/12/19 RAM for Formulas Dim dbL As String 'Object
        Dim dbT As String 'DAO.Database
        'jrs 2005/12/19 RAM for Formulas Dim rsL As DataTable 'Object
        Dim rsT As DataTable 'DAO.Recordset

        'dbL = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "IFSVAR.MDB")
        'dbT = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "datadict.MDB")

        'jrs 2005/12/19 RAM for Formulas dbL = Session("PutFdrvRun") & "IFSVAR.MDB"
        dbT = Session("PutFdrvRun") & "datadict.MDB"

        'UPGRADE_WARNING: Couldn't resolve default property of object NumbCompVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        NumbCompVar = Session("aFormulas")(LocalIdx).disVar.disSize()
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'mtiwebform June 2004 Longitudinal - comment out computation
        'frm_computations.DefInstance.Formula = Session("aFormulas")(LocalIdx)

        'Now that we know the years to use
        'load the Session("PLOTAll") vector
        'UPGRADE_WARNING: Couldn't resolve default property of object NumbCompVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For CurrCompVar = 1 To NumbCompVar
            CompVarName = Session("aFormulas")(LocalIdx).disVar().display(CurrCompVar)
            'jrs 2005/12/19 RAM for Formulas SetDataTablefromAccess(dbL, "SELECT [VarName] AS L FROM [FORMULAS] WHERE [CODE]=" & Str(LocalIdx - 1) & " AND [CODEVAR] = '" & CompVarName & "'", "", rsL)

            'rsT = dbT.OpenRecordset("datadict")
            'rsT.Index = "Variable"

            SetDataTablefromAccess(dbT, "SELECT * from DataDict", "Variable", rsT)

            If LocalIdx = 2 Then
                Session("LV_lbl_Ind") = Session("aFormulas")(LocalIdx).disVar.displayDetS(CompVarName) 'jrs 2005/12/19 RAM for Formulas rsL.Rows(0).Item("L")
                'rsT.Seek("=", VarNameInd)
                'jrs 2010/08/09
                Session("SC_sAggregationI") = IIf(IsDBNull(rsT.Rows(0).Item("Aggregation")), "", rsT.Rows(0).Item("Aggregation"))

            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object rsL(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'VarNameDep = rsL("L")
                'rsT.Seek("=", VarNameDep)
                '"Series" + Session("LV_lbl_Dep") = rsT.Fields("Table").Value

                Session("LV_lbl_Dep") = Session("aFormulas")(LocalIdx).disVar.displayDetS(CompVarName) 'jrs 2005/12/15 RAM for Formulas rsL.Rows(0).Item("L")
                'jrs 2010/08/09
                Session("SC_sAggregationD") = IIf(IsDBNull(rsT.Rows(0).Item("Aggregation")), "", rsT.Rows(0).Item("Aggregation"))

            End If

            Call ReLoadYearBaseData(Session("aFormulas")(LocalIdx).Unit)
            'Assign Session("PLOTAll") to dataAll() variable
            'UPGRADE_WARNING: Couldn't resolve default property of object CurrCompVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.

            'mti webform June 2004 Longitudinal - commented Computation code
            'frm_computations.DefInstance.LoadIdxDataAll = CurrCompVar
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'frm_computations.DefInstance.LoadDataAll = VB6.CopyArray(Session("PLOTAll"))

            'UPGRADE_WARNING: Couldn't resolve default property of object rsL.Close. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'rsL.Close()
            'rsT.Close()
            'jrs 2005/07/13
            Session("dataAll")(CurrCompVar) = VB6.CopyArray(Session("PLOTAll"))
        Next CurrCompVar

        'UPGRADE_WARNING: Couldn't resolve default property of object dbL.Close. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'dbL.Close()
        'dbT.Close()
    End Sub


    Private Sub LoadCountries(ByRef GroupName As String)

        'JRS 11/2/01 GROUPING
        Dim dbG As String 'DAO.Database
        Dim valtblG As DataTable 'DAO.Recordset
        Dim resG As DataTable 'DAO.Recordset
        Dim resR As DataTable 'DAO.Recordset
        Dim Rcnt As Short
        Dim Rcnt1 As Short
        'dbG = DAODBEngine_definst.OpenDatabase(session("IFs_MDB2Active"))
        dbG = Session("IFs_MDB2Active")
        'resG = dbG.OpenRecordset("SELECT * FROM " & GroupIDNew & " WHERE GroupName = '" & GroupName & "'")
        SetDataTablefromAccess(dbG, "SELECT * FROM " & GroupIDNew & " WHERE GroupName = """ & GroupName & """", "", resG)
        Dim GroupNum As Short
        'JRS 2/6/03


        If resG.Rows.Count > 0 Then
            GroupNum = resG.Rows(0).Item("GroupNum")
            'resG.Close()
        Else
            'mti WebForm Changes From July 04, 2004
            '            GroupTot = 0
            GroupTot(cgcount) = 0
            'mti WebForm Changes From July 04, 2004
            'resG.Close()
            Exit Sub
        End If

        '    Dim valtbl As Recordset
        'valtblG = dbG.OpenRecordset(Grouping)
        'valtblG.Index = "RegionNo"

        SetDataTablefromAccess(dbG, "Select * from " + Grouping + " order by RegionNo", "", valtblG)
        Dim iTotal As Short
        iTotal = 0

        '   Load Group Member Names List Box
        'If Not valtblG.BOF Then
        '    valtblG.MoveFirst()
        '    'JRS 2/15/02 For ICount% = 1 To valtblG.RecordCount
        '    Do While Not valtblG.EOF
        For Rcnt = 0 To valtblG.Rows.Count - 1
            'JRS 2/15/02 For JCount% = 1 To valtblG.Fields("TotalGroups")
            'JRS 2/15/02 TmpStr$ = "Group" + Trim(Str(JCount%))
            'JRS 2/15/02 If valtblG.Fields(TmpStr$) = GroupNum Then
            If valtblG.Rows(Rcnt).Item("GroupNo") = GroupNum Then
                'resR = dbG.OpenRecordset("SELECT DISTINCT Country FROM " & Regionalization & ", " & Grouping & " WHERE NewRegion = RegionNo AND Region = '" + valtblG.Fields("Region").Value + "'")
                SetDataTablefromAccess(dbG, "SELECT DISTINCT Country FROM " & Regionalization & ", " & Grouping & " WHERE NewRegion = RegionNo AND Region = """ & valtblG.Rows(Rcnt).Item("Region") & """", "", resR)
                'Do While Not resR.EOF
                For Rcnt1 = 0 To resR.Rows.Count - 1
                    iTotal = iTotal + 1
                    'mti WebForm Changes From July 02, 2004
                    CountryGroup(cgcount, iTotal) = resR.Rows(Rcnt1).Item("Country")
                    'mti WebForm Changes From July 02, 2004
                    'resR.MoveNext()
                Next Rcnt1
                'Loop
                'resR.Close()
            End If
            'JRS 2/15/02 Next JCount%
            'valtblG.MoveNext()
        Next Rcnt
        'JRS 2/15/02 Next ICount%
        '    Loop
        'End If
        'valtblG.Close()
        'dbG.Close()
        'mti WebForm Changes From July 04, 2004
        'GroupTot = iTotal
        GroupTot(cgcount) = iTotal
        'mti WebForm Changes From July 04, 2004
    End Sub


    Private Function RetrGroupVal(ByVal FieldName As String, ByRef Oper As String) As Object
        'mti WebForm Changes From July 02, 2004  - defn 

        'JRS 11/3/01 GROUPING
        Dim i As Object
        Dim CurrTotal As Short
        Dim TEMP As Object
        Dim BASE As Double

        'mtiwebform June 2004 Longitudinal
        Dim TmpV As String
        Dim TmpR As String
        'end mti webform June 2004 Longitudinal

        'UPGRADE_WARNING: Couldn't resolve default property of object TEMP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TEMP = 0
        BASE = 0
        Dim bNull As Boolean
        bNull = True
        'mti WebForm Changes From July 04, 2004
        'CurrTotal = GroupTot
        CurrTotal = GroupTot(cgcount)
        'mti WebForm Changes From July 04, 2004
        Dim rsWeight As DataTable  'DAO.Recordset
        'Opens the correct access file in case of aggregation GDP or POP

        Dim sOperTable As String = ""
        'mti WebForm Changes From July 04, 2004 -start
        If GroupName = "Automobile" Then
            If Oper = "GDP" Then
                'rsWeight = db.OpenRecordset("SeriesGDP95")
                SetDataTablefromAccess(db, "Select * from SeriesGDP95", "Country", rsWeight)
            ElseIf Oper = "POP" Then
                'rsWeight = db.OpenRecordset("SeriesPopulation")
                SetDataTablefromAccess(db, "Select * from SeriesPopulation", "Country", rsWeight)
            End If
        Else
            'jrs 2025/01/03
            If IFsAggRules.Item(Oper).HisEmpVar > "" Then
                sOperTable = "Series" & IFsAggRules.Item(Oper).HisEmpVar
                SetDataTablefromAccess(db, "Select * from " & sOperTable, "Country", rsWeight, DataPW)
            End If
            'If Oper = "GDP" Then
            '    'rsWeight = db.OpenRecordset("SeriesGDP95")
            '    SetDataTablefromAccess(db, "Select * from SeriesGDP95", "Country", rsWeight, DataPW)
            'ElseIf Oper = "POP" Then
            '    'rsWeight = db.OpenRecordset("SeriesPopulation")
            '    SetDataTablefromAccess(db, "Select * from SeriesPopulation", "Country", rsWeight, DataPW)
            'End If
        End If
        'mti WebForm Changes From July 04, 2004 - end 


        Dim idxMaxWgt As Short
        Dim idxMinWgt As Short
        Dim idxMax As Short
        Dim sWeightFieldName As String
        Dim idxMin As Short
        'jrs 2025/01/03 sOperTable
        'If Oper = "GDP" Or Oper = "POP" Then
        If sOperTable > "" Then
            'mtiwebform June 2004 Longitudinal - taken within setdatatablefromaccess
            'rsWeight.Index = "Country"

            'Validates that the fieldname is in range
            'Checking upper limit
            'For idxMaxWgt = rsWeight.Fields.Count - 1 To 1 Step -1
            For idxMaxWgt = rsWeight.Columns.Count - 1 To 1 Step -1
                'If IsNumeric(rsWeight.Fields(idxMaxWgt).Name) Then
                'mtiwebform June 2004 Longitudinal - might need to change this
                If IsNumeric(rsWeight.Columns(idxMaxWgt).ColumnName) Then
                    Exit For
                End If
            Next idxMaxWgt
            'JRS 09/09/02 If IsNumeric(FieldName) And Val(rsWeight.Fields(idxMaxWgt).Name) < _
            'Val(FieldName) Then
            'JRS 09/09/02     RetrGroupVal = Null
            'JRS 09/09/02     Exit Function
            'JRS 09/09/02 End If
            'Checking lower limit
            'For idxMinWgt = 1 To rsWeight.Fields.Count - 1
            For idxMinWgt = 1 To rsWeight.Columns.Count - 1
                'If IsNumeric(rsWeight.Fields(idxMinWgt).Name) Then
                If IsNumeric(rsWeight.Columns(idxMinWgt).ColumnName) Then
                    Exit For
                End If
            Next idxMinWgt

            'jrs 2015/06/05
            'If IsNumeric(FieldName) And Val(rsWeight.Fields(idxMinWgt).Name) > Val(FieldName) Then
            'If IsNumeric(FieldName) And Val(rsWeight.Columns(idxMinWgt).ColumnName) > Val(FieldName) Then
            '    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            '    'UPGRADE_WARNING: Couldn't resolve default property of object RetrGroupVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            '    RetrGroupVal = System.DBNull.Value
            '    Exit Function
            'End If
            'In case of aggregation using a GDP or POP calculate most recent
            'and earliest fields in the weight recordset
            'For idxMax = Valtbl.Fields.Count - 1 To 1 Step -1
            '    If IsNumeric(Valtbl.Fields(idxMax).Name) Then
            '        Exit For
            '    End If
            'Next idxMax
            'mti WebForm Changes From July 04, 2004 - bug in upgare edit fixed
            For idxMax = Valtbl.Columns.Count - 1 To 1 Step -1
                'mtiwebform June 2004 Longitudinal - might need to change this
                If IsNumeric(Valtbl.Columns(idxMax).ColumnName) Then
                    Exit For
                End If
            Next idxMax
            'mti WebForm Changes From July 04, 2004 - bug in upgare edit fixed -end

            'If IsNumeric(FieldName) And Val(Valtbl.Fields(idxMax).Name) < Val(FieldName) Then
            If IsNumeric(FieldName) And Val(Valtbl.Columns(idxMax).ColumnName) < Val(FieldName) Then

                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                'UPGRADE_WARNING: Couldn't resolve default property of object RetrGroupVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RetrGroupVal = System.DBNull.Value
                Exit Function
            End If
            If FieldName = "MostRecent" Then
                'sWeightFieldName = Valtbl.Fields(idxMax).Name
                sWeightFieldName = Valtbl.Columns(idxMax).ColumnName
                'If Val(sWeightFieldName) > Val(rsWeight.Fields(idxMaxWgt).Name) Then
                If Val(sWeightFieldName) > Val(rsWeight.Columns(idxMaxWgt).ColumnName) Then
                    'jrs 2005/10/25
                    sWeightFieldName = rsWeight.Columns(idxMaxWgt).ColumnName
                    'RetrGroupVal = System.DBNull.Value
                    'Exit Function
                End If
            ElseIf FieldName = "Earliest" Then
                'For idxMin = 1 To Valtbl.Fields.Count - 1
                For idxMin = 1 To Valtbl.Columns.Count - 1
                    'If IsNumeric(Valtbl.Fields(idxMin).Name) Then
                    If IsNumeric(Valtbl.Columns(idxMin).ColumnName) Then
                        'sWeightFieldName = Valtbl.Fields(idxMin).Name
                        sWeightFieldName = Valtbl.Columns(idxMin).ColumnName
                        'If Val(sWeightFieldName) < Val(rsWeight.Fields(idxMinWgt).Name) Then
                        If Val(sWeightFieldName) < Val(rsWeight.Columns(idxMinWgt).ColumnName) Then
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object RetrGroupVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            RetrGroupVal = System.DBNull.Value
                            Exit Function
                        End If
                        Exit For
                    End If
                Next idxMin
                'JRS 09/09/02
                'ElseIf Val(rsWeight.Fields(idxMaxWgt).Name) < Val(FieldName) Then
            ElseIf Val(rsWeight.Columns(idxMaxWgt).ColumnName) < Val(FieldName) Then
                'sWeightFieldName = rsWeight.Fields(idxMaxWgt).Name
                sWeightFieldName = rsWeight.Columns(idxMaxWgt).ColumnName
            ElseIf Val(rsWeight.Columns(idxMinWgt).ColumnName) > Val(FieldName) Then
                'sWeightFieldName = rsWeight.Fields(idxMaxWgt).Name
                sWeightFieldName = rsWeight.Columns(idxMinWgt).ColumnName
            Else
                'JRS 07/03/03 COMPOUND FIELDS (1984-88)
                If InStr(FieldName, "-") > 0 Then
                    sWeightFieldName = Mid(FieldName, 1, InStr(FieldName, "-") - 1)
                Else
                    sWeightFieldName = FieldName
                End If
            End If
        End If

        'mti WebForm Changes From July 04, 2004
        'Checks all countries in Group
        'For i = 1 To GroupTot
        For i = 1 To GroupTot(cgcount)
            'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'Valtbl.Seek("=", CountryGroup(i))
            TmpV = SeekValue(Valtbl, CountryGroup(cgcount, i), FieldName)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            'If Not (Valtbl.NoMatch) And (Not IsDBNull(Valtbl.Fields(FieldName).Value) Or optNullstoZeros.Checked) Then
            If TmpV <> "" Or optNullstoZeros.Checked Then
                'Depending on the aggregation rule
                'jrs 2025/01/03
                'Select Case Oper 
                Select Case True
                    'jrs 2025/01/03
                    'Case "GDP", "POP"
                    Case sOperTable > ""
                        'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'rsWeight.Seek("=", CountryGroup(i))
                        'mti WebForm Changes From July 02, 2004  - defn 
                        TmpR = SeekValue(rsWeight, CountryGroup(cgcount, i), sWeightFieldName)
                        'mti WebForm Changes From July 02, 2004  - defn 
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                        'If Not (rsWeight.NoMatch) And (Not IsDBNull(rsWeight.Fields(sWeightFieldName).Value) Or optNullstoZeros.Checked) Then
                        If TmpR <> "" Or optNullstoZeros.Checked Then
                            bNull = False
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                            'UPGRADE_WARNING: Couldn't resolve default property of object TEMP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'TEMP = TEMP + (IIf(IsDBNull(Valtbl.Fields(FieldName).Value), 0, Valtbl.Fields(FieldName).Value) * IIf(IsDBNull(rsWeight.Fields(sWeightFieldName).Value), 0, rsWeight.Fields(sWeightFieldName).Value))
                            TEMP = TEMP + (IIf(TmpV = "", 0, Val(TmpV)) * IIf(TmpR = "", 0, Val(TmpR)))
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                            'BASE = BASE + IIf(IsDBNull(rsWeight.Fields(sWeightFieldName).Value), 0, rsWeight.Fields(sWeightFieldName).Value)
                            BASE = BASE + IIf(TmpR = "", 0, Val(TmpR))
                        Else
                            CurrTotal = CurrTotal - 1
                        End If
                    Case Else
                        bNull = False
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object TEMP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'TEMP = TEMP + IIf(IsDBNull(Valtbl.Fields(FieldName).Value), 0, Valtbl.Fields(FieldName).Value)
                        TEMP = TEMP + IIf(TmpV = "", 0, Val(TmpV))
                End Select
            Else
                CurrTotal = CurrTotal - 1
            End If
        Next i
        'Finishes the aggregation rule calculation
        If Oper = "AVG" And CurrTotal <> 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object TEMP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            TEMP = TEMP / CurrTotal
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            'jrs 2025/01/03
            'ElseIf (Oper = "GDP" Or Oper = "POP") And (Not IsDBNull(BASE) And BASE <> 0) Then
        ElseIf (sOperTable > "") And (Not IsDBNull(BASE) And BASE <> 0) Then
            'UPGRADE_WARNING: Couldn't resolve default property of object TEMP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            TEMP = TEMP / BASE
        End If
        'Checks if the null flag was cleared
        If bNull Then
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            'UPGRADE_WARNING: Couldn't resolve default property of object RetrGroupVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RetrGroupVal = System.DBNull.Value
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object TEMP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object RetrGroupVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RetrGroupVal = TEMP
        End If
    End Function


    'jrs 2006/05/08
    Private Sub ReOrderYearsForTimeGraph(ByVal NumPts As Integer)
        Dim i As Integer, j As Integer
        Dim v1 As Object, v2 As Object
        For i = NumPts To 1 Step -1
            If Session("PlotLabels")(1, i) = "Earliest" Then
                v1 = Session("PlotLong")(i, 1)
                v2 = Session("PlotLong")(i, 2)
                For j = i To 2 Step -1
                    Session("PlotLabels")(1, j) = Session("PlotLabels")(1, j - 1)
                    Session("PlotLabels")(2, j) = Session("PlotLabels")(2, j - 1)
                    Session("PlotLong")(j, 1) = Session("PlotLong")(j - 1, 1)
                    Session("PlotLong")(j, 2) = Session("PlotLong")(j - 1, 2)
                Next j
                Session("PlotLabels")(1, 1) = "Earliest"
                Session("PlotLabels")(2, 1) = "Earliest"
                Session("PlotLong")(1, 1) = v1
                Session("PlotLong")(1, 2) = v2
                Exit For
            End If
        Next i
    End Sub


    Private Sub ReOrderYears(ByRef totpoints As Short)
        'put Earliest first if it exists
        Dim i As Object
        Dim j As Short
        For i = 1 To totpoints
            'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("DepVarYears")(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("DepVarYears")(i) = "Earliest" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                For j = i To 2 Step -1
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("DepVarYears")(j - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("DepVarYears")(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("DepVarYears")(j) = Session("DepVarYears")(j - 1)
                Next j
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("DepVarYears")(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("DepVarYears")(1) = "Earliest"
                'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                i = totpoints
            End If
        Next i
    End Sub

    'JRS 11/19/02
    Public Sub LoadPlotLong(ByVal i As Short, ByVal j As Short, ByRef V As Object)
        'UPGRADE_WARNING: Couldn't resolve default property of object V. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("PlotLong")(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("PlotLong")(i, j) = V
    End Sub

    'mtiwebform June 2004 Longitudinal - bring longplotPE sub to Function
    Sub LoadPlotVec(ByRef V As Object, ByRef i As Short, ByRef v2 As Object)
        '------------------------------------------------------------------------------------------
        Dim PlotLoad(UBound(V), UBound(V, 2)) As Object
        ReDim Session("PlotLabelsPE")(UBound(v2), UBound(v2, 2))
        'UPGRADE_WARNING: Couldn't resolve default property of object V. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("PlotLongPE") = V
        'UPGRADE_WARNING: Couldn't resolve default property of object v2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("PlotLabelsPE") = v2
        Session("PlotSizePE") = i
    End Sub

    Private Sub ClearSessionVars()
        'mti WebForm Changes From July 02, 2004 - start
        Session("bCOuntSelGrouping") = False
        Session("CountryTot") = 0
        Session.Remove("LongVarVCat")
        Session.Remove("LongVarSelName")
        Session.Remove("LV_Cohort")
        Session.Remove("WVSWave")
        Session("CountriesSelected") = "No"
        Session.Remove("LV_Chk_Indep")
        Session.Remove("LV_Chk_Points")
        Session.Remove("LV_Chk_Time")
        Session.Remove("LV_lbl_Dep")

        Session.Remove("TreatmentSelectedIndex")
        Session.Remove("LV_lbl_Ind")
        Session.Remove("DataDictVars")
        'mti WebForm Changes From July 02, 2004 - end
        'jrs 2006/01/04
        Session("bCompAll") = False
        Session("bComputationActive") = False
        'JRS 2006/05/09
        Session("TableTitle") = ""
        Session("YAxisLabel") = ""
        'KIA --------- removing huge multi array session variables which weren't delteting before
        Session.Remove("PlotLong")
        Session.Remove("PlotLongPE")
        Session.Remove("PlotLabelsPE")
        Session.Remove("PlotSizePE")
        Session.Remove("PLOTAll")
        Session.Remove("PlotLabels")
        'jrs 2011/07/08
        Session.Remove("iFilterDataPoints")
        Session.Remove("iFilterYear")
        Session.Remove("LongVarFilt")
        Session.Remove("LongVarFiltName")



    End Sub



    Public Sub SelectVar()
        cmdComputations.Visible = False
        cmd_Plot.Enabled = False
        'cmb_VarName.SelectedIndex = -1
        'jrs 2006/01/04
        cmdTable.Enabled = False
        'jrs 2006/02/22
        opt_Dependent.Checked = Session("LongVarSopt_Dependent")
        opt_Independent.Checked = Session("LongVarSopt_Independent")

    End Sub

    'jrs 2006/10/23
    Private Sub LoadDataPlotLong(ByRef finCalc As Object, ByRef f As Formula, ByVal idx As Integer)
        Dim NotIdx As Short
        If idx = 1 Then
            NotIdx = 2
        Else
            NotIdx = 1
        End If

        Dim k As Integer
        For k = 1 To f.itotYears
            If rbTreatments.SelectedIndex = 1 Then
                If Session("LongVarSopt_Independent") Then
                    'jrs 2008/05/20 Session("PlotLong")(1, k) = finCalc(k)
                    Session("PlotLong")(((k - 1) \ Session("DisYear")) + 1, ((k - 1) Mod Session("DisYear")) + 1) = finCalc(k)
                Else
                    'jrs 2008/05/20 Session("PlotLong")(1, k + f.itotYears) = finCalc(k)
                    Session("PlotLong")(((k - 1) \ Session("DisYear")) + 1, ((k - 1) Mod Session("DisYear")) + 1 + Session("DisYear")) = finCalc(k)
                End If
            Else
                'Independent
                If rbTreatments.SelectedIndex = 0 Then
                    Session("PlotLong")(k, idx) = finCalc(k)
                Else
                    Session("PlotLong")(k, NotIdx) = finCalc(k)
                End If
            End If
        Next k
    End Sub

    'jrs 2006/10/23

    Private Sub FormatFormAfterLoad(ByRef f As Formula)
        If Session("LongVarSopt_Independent") Then
            Session("LV_lbl_Ind") = f.Name
        Else
            Session("LV_lbl_Dep") = f.Name
        End If

    End Sub

    'jrs 2008/05/19
    Private Function bCheckField(ByVal sFld As String, ByRef tbl As DataTable) As Boolean
        'Only for numeric Fields
        If Not IsNumeric(sFld) Then
            'jrs 2023/09/22
            'LoadMessage("Numeric Fields Only.", Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")
            Response.Write("<script>alert('Numeric Fields Only.');</script>")
        End If

        'Search for first numeric field
        Dim iFst As Integer
        Dim i As Integer
        For i = 0 To tbl.Columns.Count - 1
            If IsNumeric(tbl.Columns(i).Caption) Then
                iFst = i
                Exit For
            End If
        Next i
        'Search for last numeric field
        Dim iLst As Integer
        Dim j As Integer
        For j = tbl.Columns.Count - 1 To 0 Step -1
            If IsNumeric(tbl.Columns(j).Caption) Then
                iLst = j
                Exit For
            End If
        Next j
        'Search Function
        bCheckField = bSearchField(sFld, tbl, iFst, iLst)
    End Function

    'jrs 2008/05/19
    Private Function bSearchField(ByVal sFld As String, ByRef tbl As DataTable, ByVal iFst As Integer, ByVal iLst As Integer) As Boolean
        Dim m As Integer
        m = (iLst + iFst) / 2
        If tbl.Columns(m).Caption = sFld Then
            bSearchField = True
            Exit Function
        ElseIf iFst >= iLst Then
            bSearchField = False
            Exit Function
        ElseIf tbl.Columns(m).Caption > sFld Then
            bSearchField = bSearchField(sFld, tbl, iFst, m - 1)
        ElseIf tbl.Columns(m).Caption < sFld Then
            bSearchField = bSearchField(sFld, tbl, m + 1, iLst)
        End If
    End Function
    'jrs 2008/10/08
    Public Sub LoadSearch(ByVal sVar As String)
        lstVarName.SelectedValue = sVar
        Dim var As String
        var = sVar.Substring(0, sVar.IndexOf("-")).Trim
        If opt_Dependent.Checked Then
            Session("LV_lbl_Dep") = var
        Else
            Session("LV_lbl_Ind") = var
        End If
        FireTreatmentTimeEvents(Session("TreatmentSelectedIndex"))
        'cmb_VarName_SelectedIndexChanged(Me, New EventArgs)
        'jrs 2012/02/06
        If opt_Dependent.Checked Then
            SearchAggregationAndSet(1)
        Else
            SearchAggregationAndSet(0)
        End If
    End Sub
    Private Sub LoadWorks()
        lstVarName.EnableViewState = False

        'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.
        If Not Page.IsPostBack Then
            'jrs 2024/04/29
            Session("WVSTIME") = False

            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            'jrs 09/28/04
            If Session("oldLastYear") <> Nothing Then
                Session("LastYear") = Session("oldLastYear")
                Session("FirstYear") = Session("oldFirstYear")
                Session("oldLastYear") = Nothing
            End If

            Dim NameFound As String
            Dim AddName As String
            Dim Definition As String
            Dim VarName As String
            Dim TableName As String
            Dim GroupNameNum As Object
            Dim Msg As String
            Dim SubOnPrev As Object
            Dim AddOnPrev As Object
            Dim MulOnPrev As Object
            Dim DivOnPrev As Object
            Dim ICount As Short
            Dim Rcnt As Short
            'jrs 2011/07/06
            Dim sPreprocessor As String, sYears As String, bInclude As Boolean

            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Session("MaxDepIndYears") = 500
            ReDim Session("DepVarYears")(Session("MaxDepIndYears"))
            ReDim Session("IndVarYears")(Session("MaxDepIndYears"))
            ReDim Session("ScatVarYears")(Session("MaxDepIndYears"))
            'Width = Inch * 7
            'Height = Inch * 4.5
            'top = (Screen.Height - Height) / 2
            'left = (Screen.Width - Width) / 2
            Carret = Chr(13) & Chr(10)
            VarGroupNames(1) = "ALL"
            'jrs 2006/08/05
            cmb_VCAT.Items.Clear()
            'JAM 2013-11-16 Commented
            'KIA 09/22/2009 COMMENT THE FOLLOWING LINE
            'cmb_VCAT.Items.Add(New ListItem("ALL"))
            For ICount = 2 To 100
                VarGroupNames(ICount) = ""
            Next ICount
            DivOnPrev = False
            MulOnPrev = False
            AddOnPrev = False
            SubOnPrev = False
            Session("PlotPossible") = True

            'jrs 2011/07/06
            Session("iFilterDataPoints") = 10
            Session("iFilterYear") = LastYearHistory
            LoadFilter()

            '   Set up labels
            '   -------------
            Msg = "Select Dependent Variable:"
            lbl_VarName.Text = Msg
            lbl_vcat.Text = "Show Variable Name Subset:"
            If Session("LV_lbl_Ind") <> Nothing Then
                lbl_Ind.Text = Session("LV_lbl_Ind")
            End If


            If Session("LV_lbl_Dep") <> Nothing Then
                lbl_Dep.Text = Session("LV_lbl_Dep")
            End If
            lbl_Cont.Text = "None"

            '   Turn on variable group/Session("Category") combination box
            '   -----------------------------------------------
            cmb_VCAT.Visible = True
            lbl_vcat.Visible = True
            If Not Session("TreatmentSelectedIndex") Is Nothing Then
                rbTreatments.SelectedIndex = Session("TreatmentSelectedIndex")
            Else
                rbTreatments.SelectedIndex = 0
                Session("TreatmentSelectedIndex") = 0
            End If
            FireTreatmentTimeEvents(rbTreatments.SelectedIndex)

            '
            '   Check box to turn off control invisible
            '   ---------------------------------------
            chk_ControlOff.Visible = False
            '
            '   Turn off control for now; probably not use
            '   ------------------------------------------
            lbl_Control.Visible = False
            lbl_Cont.Visible = False
            opt_Control.Visible = False
            '
            '   No countries yet selected
            '   -------------------------
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("CountryTot"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'Session("CountryTot") = 0
            lbl_CountSel.Text = Session("LV_lbl_CountSel") '"No"
            '
            '   Load Table Name Combo Box and Variable Name Combo Box
            '   -----------------------------------------------------
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'mti Augut 2004 3
            'cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text = ""
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'cmb_VarName.Items.Clear()
            'lstVarName.Items.Clear()

            AddName = ""
            'cmb_VarName.Items.Add(New ListItem(AddName))

            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'cmb_VarName.Items(cmb_VarName.SelectedIndex).Text = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            GroupNameNum = 0
            'UPGRADE_WARNING: Couldn't resolve default property of object VarNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            VarNameNum = 0

            '    Set db = OpenDatabase(FDRV$ + "Data\datadict.MDB", , True)
            'db = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "datadict.MDB")
            db = Session("PutFdrvRun") & "datadict.MDB"
            'valtbl = db.OpenRecordset("datadict")
            'Valtbl.Index = "Variable"
            SetDataTablefromAccess(db, "Select * from DataDict order by Variable", "", Valtbl)

            '''''''''''''''''''''''JAM 2013-11-16''''''''''''''''''''''''''''''''''''

            'JAM 2013-08-31
            Session("DataDictVars") = Valtbl
            'Dim Groups As Array
            ' Dim tmpStr As String
            'Dim pos1 As Integer
            'jrs 2012/02/28
            Dim sCoVaTra As String

            'JAM 2013-09-07 Based on code developed by Jose in frm_PackagedDisplayFlex
            Dim Str As String
            Dim GrpCol As Collection
            GrpCol = New Collection
            Dim x As Object, ii As Integer, j As Integer
            Do While (Not Valtbl.Rows.Count = j)
                Str = IIf(IsDBNull(Valtbl.Rows(j).Item("Group")), "", Valtbl.Rows(j).Item("Group"))
                x = Split(Valtbl.Rows(j).Item("Group"), ",")
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



            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            'valtbl.MoveFirst()
            'Do 
            '	'UPGRADE_WARNING: Couldn't resolve default property of object VarNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            '	VarNameNum = VarNameNum + 1
            '	valtbl.MoveNext()
            '	If valtbl.EOF = True Then Exit Do 
            'Loop 
            'valtbl.Close()
            '         db.Close()
            VarNameNum = Valtbl.Rows.Count
            ReDim VarNames(VarNameNum)
            'UPGRADE_WARNING: Couldn't resolve default property of object VarNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            VarNameNum = 0

            '    Set db = OpenDatabase(FDRV$ + "Data\datadict.MDB", , True)
            'db = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "datadict.MDB")
            'valtbl = db.OpenRecordset("datadict")
            'valtbl.Index = "Variable"
            '         Valtbl.MoveFirst()

            'db = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "datadict.MDB")

            'JAM 2013-11-16 Commented load Variable
            ''db = Session("PutFdrvRun") & "datadict.MDB"
            ''SetDataTablefromAccess(db, "Select * from DataDict order by Variable", "", Valtbl)

            If Session("LongVarVCat") Is Nothing Then
                Session("LongVarVCat") = 0
                Session("LongVarSelName") = "All"
            ElseIf Session("LongVarVCat") > -1 Then 'jrs 2006/08/01
                cmb_VCAT.SelectedIndex = Session("LongVarVCat")
            End If

            'jrs 2011/07/08
            If Session("LongVarFilt") Is Nothing Then
                Session("LongVarFilt") = 3
                Session("LongVarFiltName") = "ALL"
            ElseIf Session("LongVarFilt") > -1 Then
                cmb_OthFilt.SelectedIndex = Session("LongVarFilt")
            End If

            '''''''''''''''''''''JAM 2013-11-16 Commented lstVarname'''''''''''''''''''''''''''''''''
            ''Dim sUnits As String
            ' ''mti August 2004 4
            ''Dim tCohort As String
            ''Dim tSeries As String
            ' ''KIA 09/22/2009 declare the following 2 variabels
            ''Dim pos1 As Integer
            ''Dim Groups As Array
            ''lstVarName.Items.Clear()
            ' ''Dim pk(1) As DataColumn
            ' ''pk(0) = New DataColumn("Variable")
            ' ''Valtbl.PrimaryKey = pk
            ''If Session("LongVarSelName") = "All" Then
            ''    For Rcnt = 0 To Valtbl.Rows.Count - 1

            ''        TableName = Valtbl.Rows(Rcnt).Item("Table")
            ''        GroupName = Valtbl.Rows(Rcnt).Item("Group")
            ''        VarName = Valtbl.Rows(Rcnt).Item("Variable")
            ''        tSeries = Valtbl.Rows(Rcnt).Item("Series")
            ''        tCohort = Valtbl.Rows(Rcnt).Item("Cohort")
            ''        'Definition = Valtbl.Rows(Rcnt).Item("Definition")
            ''        'JAM 2011-09-25 Replacing two and three blank spaces
            ''        Definition = Replace(Replace(Valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

            ''        'jrs 2011/07/08
            ''        sPreprocessor = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", Valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
            ''        sYears = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Years")), "", Valtbl.Rows(Rcnt).Item("Years"))                        'JRS 08/29/03
            ''        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            ''        If IsDBNull(Valtbl.Rows(Rcnt).Item("Units")) Then
            ''            sUnits = ""
            ''        Else
            ''            sUnits = Valtbl.Rows(Rcnt).Item("Units")
            ''        End If
            ''        'jrs 2012/02/28
            ''        sCoVaTra = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("CoVaTra")), "No", Valtbl.Rows(Rcnt).Item("CoVaTra"))

            ''        If tSeries = "Yes" Or tCohort = "Yes" Then
            ''            'jrs 2011/07/06
            ''            bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
            ''            If bInclude Then
            ''                If ModelLoad = "LoadCoVaTrA" Or UCase(sCoVaTra) = "NO" Then 'jrs 2012/02/28 Or GroupName <> "Automobile" Then
            ''                    'JRS 08/29/03
            ''                    AddName = VarName & " - " & Definition & " - " & sUnits
            ''                    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            ''                    dataString = dataString & AddName & "|"
            ''                    lstVarName.Items.Add(New ListItem(AddName))
            ''                    'UPGRADE_WARNING: Couldn't resolve default property of object VarNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''                    VarNames(VarNameNum) = VarName

            ''                    '            TableNameBase$ = Right(TableName$, Len(TableName$) - 6)
            ''                    '            TableNameSave$(VarNameNum) = TableNameBase$
            ''                    'UPGRADE_WARNING: Couldn't resolve default property of object VarNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''                    VarNameNum = VarNameNum + 1
            ''                    'KIA 09/22/2009 take and replace until you find end
            ''                End If
            ''            End If
            ''        End If
            ''        Groups = Split(GroupName, ",")
            ''        pos1 = 0
            ''        While pos1 <= UBound(Groups)
            ''            GroupName = Trim(Groups(pos1))
            ''            pos1 = pos1 + 1
            ''            NameFound = CStr(False)

            ''            For ICount = 1 To 100
            ''                If GroupName = VarGroupNames(ICount) Then NameFound = CStr(True)
            ''            Next ICount
            ''            If CBool(NameFound) = False Then
            ''                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            ''                cmb_VCAT.Items.Add(New ListItem(GroupName))
            ''                'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''                GroupNameNum = GroupNameNum + 1
            ''                'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''                VarGroupNames(GroupNameNum) = GroupName
            ''            End If
            ''        End While
            ''        'KIA end

            ''    Next Rcnt
            ''Else
            ''    For Rcnt = 0 To Valtbl.Rows.Count - 1

            ''        TableName = Valtbl.Rows(Rcnt).Item("Table")
            ''        GroupName = Valtbl.Rows(Rcnt).Item("Group")
            ''        VarName = Valtbl.Rows(Rcnt).Item("Variable")
            ''        tSeries = Valtbl.Rows(Rcnt).Item("Series")
            ''        tCohort = Valtbl.Rows(Rcnt).Item("Cohort")
            ''        'Definition = Valtbl.Rows(Rcnt).Item("Definition")
            ''        'JAM 2011-09-25 Replacing two and three blank spaces
            ''        Definition = Replace(Replace(Valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

            ''        'jrs 2011/07/08
            ''        sPreprocessor = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", Valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
            ''        sYears = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Years")), "", Valtbl.Rows(Rcnt).Item("Years"))                        'JRS 08/29/03
            ''        'JRS 08/29/03
            ''        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            ''        If IsDBNull(Valtbl.Rows(Rcnt).Item("Units")) Then
            ''            sUnits = ""
            ''        Else
            ''            sUnits = Valtbl.Rows(Rcnt).Item("Units")
            ''        End If
            ''        'jrs 2012/02/28
            ''        sCoVaTra = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("CoVaTra")), "No", Valtbl.Rows(Rcnt).Item("CoVaTra"))

            ''        Groups = Split(GroupName, ",")
            ''        pos1 = 0
            ''        While pos1 <= UBound(Groups)
            ''            GroupName = Trim(Groups(pos1))
            ''            pos1 = pos1 + 1
            ''            NameFound = CStr(False)
            ''            If tSeries = "Yes" Or tCohort = "Yes" Then
            ''                'jrs 2011/07/06
            ''                bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
            ''                If bInclude Then
            ''                    If (ModelLoad = "LoadCoVaTrA" Or UCase(sCoVaTra) = "NO") And (GroupName = Session("LongVarSelName")) Then 'jrs 2012/02/28 Or GroupName <> "Automobile"
            ''                        'JRS 08/29/03
            ''                        AddName = VarName & " - " & Definition & " - " & sUnits
            ''                        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            ''                        dataString = dataString & AddName & "|"
            ''                        lstVarName.Items.Add(New ListItem(AddName))
            ''                        'UPGRADE_WARNING: Couldn't resolve default property of object VarNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''                        VarNames(VarNameNum) = VarName

            ''                        '            TableNameBase$ = Right(TableName$, Len(TableName$) - 6)
            ''                        '            TableNameSave$(VarNameNum) = TableNameBase$
            ''                        'UPGRADE_WARNING: Couldn't resolve default property of object VarNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''                        VarNameNum = VarNameNum + 1
            ''                        'KIA 09/22/2009 take and replace until you find end
            ''                    End If
            ''                End If
            ''            End If
            ''            For ICount = 1 To 100
            ''                If GroupName = VarGroupNames(ICount) Then NameFound = CStr(True)
            ''            Next ICount
            ''            If CBool(NameFound) = False Then
            ''                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            ''                cmb_VCAT.Items.Add(New ListItem(GroupName))
            ''                'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''                GroupNameNum = GroupNameNum + 1
            ''                'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ''                VarGroupNames(GroupNameNum) = GroupName
            ''            End If
            ''        End While
            ''        'KIA end

            ''    Next Rcnt
            ''End If
            Session("DataDictVars") = Valtbl
            If Session("bCOuntSelGrouping") = True Then 'jrs 2011/12/06 And rbTreatments.SelectedIndex > 0 Then
                cmd_Countries.Text = "Select Groups"
                cmd_Countries.ToolTip = "Select Groups (Alt+E)"
                cmdUseGroups.Text = "Use Countries"
                cmdUseGroups.ToolTip = "Use Countries (Alt+R)"
                lbl_Countries.Text = "Groups Selected?"
                'jrs 2008/05/14
                cmdTable.Enabled = False
            Else
                cmd_Countries.Text = "Select Countries"
                cmd_Countries.ToolTip = "Select Countries (Alt+E)"
                cmdUseGroups.Text = "Use Groups"
                cmdUseGroups.ToolTip = "Use Groups (Alt+R)"
                lbl_Countries.Text = "Countries Selected?"
                'jrs 2008/05/14
                cmdTable.Enabled = True
            End If
            'If cmd_Countries.Text = "Select Countries" And rbTreatments.SelectedIndex = 0 Then
            '    cmdTable.Enabled = True
            'Else
            '    cmdTable.Enabled = False
            'End If

            If Session("CountryTot") > 0 Then
                lbl_CountSel.Text = "Yes"
            Else
                lbl_CountSel.Text = "None"
            End If


            'mti August 2004 4 - end 
            'mti WebForm Changes From July 02, 2004 -end 
            'jrs 08/30/04
            Session("LongVarTable") = False
            Session("TableDisplayParent") = "LongVarS.aspx"
            'jrs 2012/01/30
            Session("AvailabilityParent") = "LongVarS.aspx"

            'jrs 09/15/04
            Session("RefreshGraph") = "true"
            If Session("bComputation") Then
                SelectVar()

            ElseIf Session("bComputationActive") Or rbTreatments.SelectedIndex > 0 Then
                '
                opt_Dependent.Checked = Session("LongVarSopt_Dependent")
                opt_Independent.Checked = Session("LongVarSopt_Independent")
            End If

        Else

            bLastIndep = Session("bLastIndep")
            If Session("CountriesSelected") <> "Yes" Then
                lbl_CountSel.Text = "None"
            Else
                lbl_CountSel.Text = "Yes"
            End If

        End If


        'jrs 2011/07/08 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub
    Private Sub SearchAggregationAndSet(ByVal optind As Int16)
        Valtbl = Session("DataDictVars")
        Dim dataRow() As DataRow

        If optind = 1 Then 'dependent variable
            'jrs 2025/02/27
            Dim pos As Integer
            pos = Session("LV_lbl_Dep").ToString.IndexOf("-")
            Dim DepVar As String
            If pos > 0 Then
                DepVar = Session("LV_lbl_Dep").ToString.Substring(0, pos - 1)
            Else
                DepVar = Session("LV_lbl_Dep")
            End If

            'jrs 2025/02/27 dataRow = Valtbl.Select("Variable='" + Session("LV_lbl_Dep") + "'")
            dataRow = Valtbl.Select("Variable='" + DepVar + "'")
            Session("SC_sAggregationD") = dataRow(0).Item("Aggregation")
            Session("LongVarSopt_Dependent") = True
            Session("LongVarSopt_Independent") = False

        Else
            'jrs 2025/02/27
            Dim pos As Integer
            pos = Session("LV_lbl_Ind").ToString.IndexOf("-")
            Dim IndVar As String
            If pos > 0 Then
                IndVar = Session("LV_lbl_Ind").ToString.Substring(0, pos - 1)
            Else
                IndVar = Session("LV_lbl_Ind")
            End If

            'jrs 2025/02/27 dataRow = Valtbl.Select("Variable='" + Session("LV_lbl_Ind") + "'")
            dataRow = Valtbl.Select("Variable='" + IndVar + "'")
            Session("SC_sAggregationI") = dataRow(0).Item("Aggregation")
            Session("LongVarSopt_Dependent") = False
            Session("LongVarSopt_Independent") = True
        End If
        Session("LV_Cohort") = dataRow(0).Item("Cohort")
        If Session("LV_Cohort") = "Yes" Then
            Session("WVSWave") = CStr(True)
        Else
            Session("WVSWave") = CStr(False)
        End If
        'jrs 2015/09/24
        Session("LV_sUnits") = dataRow(0).Item("Units")
        'jrs 2010/09/02
        If Session("bComputation") Then
            ComputationSet(optind)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("LongVarTable") = False 'cdv 2022/07/01
        Dim selectedAction As String
        Dim indx As Int16 = 0
        'THIS block if recieves and hadles all AJAX XML HTTP requests

        'JAM 2013-11-16 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            If Session("LongVarVCat") Is Nothing Then
                Session("LongVarVCat") = 0
            Else
                fSubSetchosen.Value = Session("LongVarVCat")
            End If
        End If
        If fOthFiltchosen.Value <> "" Then
            Session("LongVarFilt") = fOthFiltchosen.Value
        Else
            If Session("LongVarFilt") Is Nothing Then
                Session("LongVarFilt") = 3  'All Value Selected
            Else
                fOthFiltchosen.Value = Session("LongVarFilt")
            End If
        End If



        If Request.QueryString("SelectedAction") <> Nothing Then
            selectedAction = Request.QueryString("SelectedAction").ToString
            Select Case selectedAction
                Case "checkIDP"
                Case "selectGroupClicked" 'jrs 2011/12/06 no longer being used, button click was triggering a postback in IE
                    Session("bCOuntSelGrouping") = CBool(Request.QueryString("gVal").ToString)

                Case "varSelected"
                    Session("LV_lbl_Dep") = Request.QueryString("dpv").ToString.Trim().Replace(" ", "+")
                    'jrs 2025/03/04
                    Dim pos As Integer
                    pos = Session("LV_lbl_Dep").ToString.IndexOf("-")
                    If pos > 0 Then
                        Session("LV_lbl_Dep") = Session("LV_lbl_Dep").ToString.Substring(0, pos - 1)
                    End If
                    'jrs 2023/09/28
                    Session("LV_lbl_Dep") = Session("LV_lbl_Dep").ToString.Replace("9999AND", "&")
                    Session("LV_lbl_Ind") = Request.QueryString("idpv").ToString.Trim().Replace(" ", "+")
                    'jrs 2025/03/04
                    pos = Session("LV_lbl_Ind").ToString.IndexOf("-")
                    If pos > 0 Then
                        Session("LV_lbl_Ind") = Session("LV_lbl_Ind").ToString.Substring(0, pos - 1)
                    End If

                    'jrs 2023/09/28
                    Session("LV_lbl_Ind") = Session("LV_lbl_Ind").ToString.Replace("9999AND", "&")
                    Session("LV_lbl_Dep") = Session("LV_lbl_Dep").ToString.Replace(" ", "+")
                    Session("LV_lbl_Ind") = Session("LV_lbl_Ind").ToString.Replace(" ", "+")
                    FireTreatmentTimeEvents(CInt(Request.QueryString("sindex").ToString.Trim()))
                    SearchAggregationAndSet(Int16.Parse(Request.QueryString("optind").ToString.Trim()))
                Case "selectDepIndClicked"
                    If Request.QueryString("cname").ToString = "dep" Then
                        opt_Dependent_CheckedChanged(Request.QueryString("gVal").ToString.Trim())

                    Else
                        opt_Dependent_CheckedChanged(Request.QueryString("gVal").ToString.Trim())
                    End If
                Case "categoryChanged"
                    cmb_vcat_SelectedIndexChanged1(CInt(Request.QueryString("catIndx").ToString.Trim()), Request.QueryString("catName").ToString.Trim())
                    sendAJAXResponse(dataString)
                Case "othFiltChanged"
                    cmb_OthFilt_SelectedIndexChanged(CInt(Request.QueryString("catIndx").ToString.Trim()), Request.QueryString("catName").ToString.Trim())
                    sendAJAXResponse(dataString)
            End Select
        Else
            If Page.IsPostBack And Not (Session("DataDictVars") Is Nothing) Then 'jrs 2012/12/12 added and not Session("DataDictVars") is nothing, hitting back from your browser
                cmb_vcat_SelectedIndexChanged1(cmb_VCAT.SelectedIndex, cmb_VCAT.SelectedItem.Text)
                'jrs 2011/12/06
                lbl_Ind.Text = Session("LV_lbl_Ind")
                lbl_Dep.Text = Session("LV_lbl_Dep")
            Else

                LoadWorks()

            End If

            CreateMenu()
            'jrs 2011/12/06 cmdUseGroups.Attributes.Add("onclick", "javascript:return groupClicked();")
            opt_Dependent.Attributes.Add("onclick", "javascript:selectDepInd('dep',this.checked);")
            opt_Independent.Attributes.Add("onclick", "javascript:selectDepInd('indp',this.checked);")
        End If


        'jrs 2013/11/02
        If Not CBool(DevelopmentMode) Then
            cmdGeneratejs.Visible = False
        End If



    End Sub
    Private Sub FireTreatmentTimeEvents(ByVal sid As Integer)
        If sid = 0 Then
            Chk_Indep_CheckStateChanged()
        ElseIf sid = 1 Then
            chk_Points_CheckStateChanged()

        Else
            Chk_time_CheckStateChanged()
        End If
        Session("TreatmentSelectedIndex") = sid
    End Sub
    Protected Sub rbTreatments_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbTreatments.SelectedIndexChanged

        FireTreatmentTimeEvents(rbTreatments.SelectedIndex)
        'If rbTreatments.SelectedIndex = 0 Then
        '    cmdTable.Enabled = True
        'Else
        '    cmdTable.Enabled = False
        'End If

    End Sub


    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        'JAM 2013-11-16 Keeping chosen filters
        If fSubSetchosen.Value <> "" Then
            If Session("LongVarVCat") Is Nothing Then
                Session("LongVarVCat") = 0
            Else
                fSubSetchosen.Value = Session("LongVarVCat")
            End If
        End If
        If fOthFiltchosen.Value <> "" Then
            Session("LongVarFilt") = fOthFiltchosen.Value
        Else
            If Session("LongVarFilt") Is Nothing Then
                Session("LongVarFilt") = 3  'All Value Selected
            Else
                fOthFiltchosen.Value = Session("LongVarFilt")
            End If
        End If


        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_LongVarSearch.aspx")
    End Sub

    'jrs 2010/09/02
    'Only used for computations now
    Private Sub ComputationSet(ByVal optind As Int16)
        Session("YAxisLabel") = ""

        If optind = 1 Then 'dependent variable
            Session("LongVarScmb_Varname") = Session("LV_lbl_Dep")
        Else
            Session("LongVarScmb_Varname") = Session("LV_lbl_Ind")
        End If


        bInd = opt_Independent.Checked

        Dim Msg As String

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If CBool(Session("LongVarSopt_Independent")) = True Then 'Have completed independent
            'JRS 10/27/01 COMPUTATIONS
            If Session("bComputationActive") Then
                bLastIndep = True
            End If
        Else 'have completed dependent or control
            'JRS 10/27/01 COMPUTATIONS
            If Session("bComputationActive") Then
                bLastIndep = False
            End If
        End If
        Session("bLastIndep") = bLastIndep

        'jrs 2010/09/02 advance for computations
        If Session("LV_Chk_Indep") = 0 Then
            Session("LongVarSopt_Dependent") = Not Session("LongVarSopt_Dependent")
            Session("LongVarSopt_Independent") = Not Session("LongVarSopt_Dependent")
        End If

    End Sub

    'jrs 2010/09/02
    Protected Sub btnInvs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInvs.Click
        Dim Msg As String

        '   Track Whether Variable is WVS Wave-Based
        '   ----------------------------------------

        If Session("LV_Cohort") = "Yes" Then
            'jrs 2008/05/16
            If Session("bComputation") Then
                Msg = "Cohort Variables are not allowed with Formulas."
                Session("LV_Cohort") = "No"
                'jrs 2023/09/22
                'LoadMessage(Msg, Session("Message"))
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")
                Response.Write("<script>alert('" & Msg & "');</script>")
            End If

            Session("WVSWave") = CStr(True)
        Else
            Session("WVSWave") = CStr(False)
        End If

        On Error GoTo FileProblem

        'JRS 10/05/01 COMPUTATIONS
        If Session("bComputation") Then 'And Series$ <> "Yes" And Cohort$ <> "Yes" Then
            'Me.Hide() 
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")
            'frm_computations.DefInstance.Show() 
            'frm_computations.DefInstance.Activate() 
            Exit Sub
        Else
            If Session("bComputationActive") Then
                LocalSetVarComp()
            End If
            Exit Sub
        End If

FileProblem:
        Msg = "Sorry. An error has arisen during an attempt to read " & Session("LongVarScmb_Varname")
        Msg = Msg & " from Series" & Session("LongVarScmb_Varname") & " - the variable or data for it "
        Msg = Msg & "doesn't exist in file. Please try another variable name."
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    'jrs 2011/07/06
    Private Function bIncludedByOthrFilter(ByVal sPreprocessor As String, ByVal sYears As String) As Boolean
        Dim bInclude As Boolean, iCountYears As Integer
        Dim xYears As Object, pos As Integer
        Dim yr1 As Integer, yr2 As Integer, i As Integer
        'jrs 2011/07/06
        'Other filters
        bInclude = False
        If Session("LongVarFiltName") = "ALL" Then
            bInclude = True
        ElseIf (Session("LongVarFiltName") = "Used for Initialization" And sPreprocessor = "True") Then
            bInclude = True
        ElseIf (Session("LongVarFiltName") = "At least " & CStr(Session("iFilterDataPoints")) & " Data Points") Then
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
                            yr2 = CInt(Mid(xYears(i), pos + 1))
                        End If
                        iCountYears = iCountYears + yr2 - yr1
                        If iCountYears >= Session("iFilterDataPoints") Then
                            bInclude = True
                            Exit For
                        End If
                    End If
                Next i
            End If
        ElseIf (Session("LongVarFiltName") = "At least through year " & CStr(Session("iFilterYear"))) Then
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

    'jrs 2011/09/09
    Public Sub mnuTable_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------------------------------------
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        Session("COUNTE") = Session("PlotSizePE")
        Dim year_Renamed As Short
        Dim country As Short
        Dim i As Short

        'jrs 2005/06/29
        Dim MinYr As Integer, MaxYr As Integer
        MinYr = FirstYearHistory
        MaxYr = LastYearHistory
        'jrs 2007/05/01 use first element, since we're not storing Earliest anymore
        'we were using second element
        If IsNumeric(Session("PlotLabelsPE")(1, 1)) Then
            If CInt(Session("PlotLabelsPE")(1, 1)) < MinYr Then
                MinYr = CInt(Session("PlotLabelsPE")(1, 1))
            End If
        End If

        'jrs 2007/02/20 If IsNumeric(PlotLabels(1, UBound(PlotLabels, 2) - 1)) Then
        'removed most recent at the end
        If IsNumeric(Session("PlotLabelsPE")(1, UBound(Session("PlotLabelsPE"), 2))) Then
            If CInt(Session("PlotLabelsPE")(1, UBound(Session("PlotLabelsPE"), 2))) > MaxYr Then
                MaxYr = CInt(Session("PlotLabelsPE")(1, UBound(Session("PlotLabelsPE"), 2)))
            End If
        End If

        'jrs 2007/02/20 For i = 2 To UBound(PlotLabels, 2) - 1
        'jrs 2007/05/01 change I = 2 to I = 1, since we're not storing the Earliest field anymore, only numeric ones
        For i = 1 To UBound(Session("PlotLabelsPE"), 2)
            'JRS 12/08/03
            If CShort(Session("PlotLabelsPE")(1, 1)) < FirstYearHistory Then
                year_Renamed = CShort(Session("PlotLabelsPE")(1, i)) - CShort(Session("PlotLabelsPE")(1, 1)) + 1
            Else
                year_Renamed = CShort(Session("PlotLabelsPE")(1, i)) - FirstYearHistory + 1
            End If
            'Session("TableTitle")
            For country = 1 To Session("COUNTE")  'COUNTE

                'mti Augut 2004 3

                'Session("TableTitle")
                'VARN(1, country) = Mid(Pego1.MainTitle, 1, InStr(Pego1.MainTitle, "versus") - 2)
                'Session("VARN")(1, country) = Mid(PegoNet1.PeString.MainTitle, 1, InStr(PegoNet1.PeString.MainTitle, "versus") - 2)
                Session("VARN")(1, country) = Mid(Session("TableTitle"), 1, InStr(Session("TableTitle"), "versus") - 2)
                Session("GR1N")(country) = Session("CountryList")(country)

                'jrs 10/27/04 Flexible Dimension Names for Graphs
                Session("FDNG1")(country) = Session("GR1N")(country)

                'jrs 2012/09/26 Session("FinYear")(country) = LastYearHistory
                'jrs 2012/09/26
                'Using MaxYr instead of LastYearHistory, in case where there's extra data afterwards
                'jrs 2012/02/22
                If CInt(Session("PlotLabelsPE")(1, 1)) < FirstYearHistory Then
                    'jrs 2012/09/26 FinYear(Country) = LastYearHistory - CInt(PlotLabels(1, 1)) + 1
                    Session("FinYear")(country) = MaxYr - CInt(Session("PlotLabelsPE")(1, 1)) + 1
                Else
                    'jrs 2012/09/26 FinYear(Country) = LastYearHistory - FirstYearHistory + 1
                    Session("FinYear")(country) = MaxYr - FirstYearHistory + 1
                End If

                Session("PLOT")(year_Renamed, country) = Session("PlotLongPE")(i, country)
            Next country
        Next i
        'Update variables
        'mti August 2004 3
        'Dim oldLastYear As Short
        'Dim oldFirstYear As Short
        Session("oldLastYear") = Session("LastYear")
        Session("oldFirstYear") = Session("FirstYear")
        Session("LastYear") = MaxYr 'jrs 2005/06/30 LastYearHistory
        Session("FirstYear") = MinYr 'jrs 2005/06/30 FirstYearHistory
        Session("PercentOn") = "False"
        'jrs 2005/10/25
        Session("DisplayLastYear") = Session("LastYear")

    End Sub

    Protected Sub cmdUseGroups_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUseGroups.Click

        If Not Session("bCOuntSelGrouping") Then
            cmdUseGroups.Text = "Use Countries"
            cmdUseGroups.ToolTip = "Use Countries (Alt+R)"
            lbl_Countries.Text = "Groups Selected?"
            cmd_Countries.Text = "Select Groups"
            cmd_Countries.ToolTip = "Select Groups (Alt+E)"
            cmdTable.Enabled = False

            Session("bCOuntSelGrouping") = True
        Else
            cmdUseGroups.Text = "Use Groups"
            cmdUseGroups.ToolTip = "Use Groups (Alt+R)"
            lbl_Countries.Text = "Countries Selected?"
            cmd_Countries.Text = "Select Countries"
            cmd_Countries.ToolTip = "Select Countries (Alt+E)"
            cmdTable.Enabled = True

            Session("bCOuntSelGrouping") = False
        End If

    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '   JAM 2013-10-28 Based on CreateJSforFlexDisplays developed by Jose
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub CreateJSforLongVars()
        'Dim rsL As DataTable
        'Dim sql As String
        ''Dim Rcnt As Short
        ''Dim Varname As String
        ''Dim VarDefinition As String
        ''Dim sUnits As String
        Dim Groupnames As String
        ''Dim divGroups As Array
        ''Dim idivGroups As Short
        ''Dim oneGroup As String
        Dim sJscript As String
        ''Dim FullName As String
        ''Dim sCoVaTra As String
        ''Dim sPreprocessor As String, sYears As String, bInclude As Boolean
        Dim vFilter As String


        'JAM 2013-08-25 For other filter recovering
        vFilter = Session("LongVarFiltName")

        Groupnames = ""

        'JAM 2014-06-14
        GenerateVarsforLongvars(CStr(Session("LongVarFiltName")), CStr(Session("PutFdrvRun")), CStr(Session("iFilterYear")), CStr(Session("iFilterDataPoints")), Groupnames)

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '       JAM 2014-06-12 Commented due centralization code in Function2
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ''db = Session("PutFdrvRun") & "DataDict.MDB"
        ''SetDataTablefromAccess(db, "SELECT * FROM datadict order by Variable", "", Valtbl)
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


        ' ''JAM 2013-11-16
        ''If Not (GroupName Is Nothing) Then
        ''    If Not (GroupName = "") Then
        ''        sJscript = sJscript & "  jScat_GroupNames = """ & GroupName & """;" & vbCr
        ''    Else
        ''        sJscript = sJscript & "  jScat_GroupNames = """"; " & vbCr
        ''    End If
        ''Else
        ''    sJscript = sJscript & "  jScat_GroupNames = """"; " & vbCr
        ''End If


        ' ''If Not (Session("Scat_GroupNames") Is Nothing) Then
        ' ''    sJscript = sJscript & "  jScat_GroupNames = """ & Session("Scat_GroupNames") & """;" & vbCr
        ' ''Else
        ' ''    sJscript = sJscript & "  jScat_GroupNames = """"; " & vbCr
        ' ''End If

        ''For Rcnt = 0 To Valtbl.Rows.Count - 1
        ''    'JAM 2013-08-20 Adding CoVatra filter
        ''    sCoVaTra = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("CoVaTra")), "No", Valtbl.Rows(Rcnt).Item("CoVaTra"))

        ''    If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(sCoVaTra) = "NO" Then
        ''        Varname = Valtbl.Rows(Rcnt).Item("Variable")
        ''        'JAM 2013-09-10
        ''        'VarDefinition = valtbl.Rows(Rcnt).Item("Definition")
        ''        VarDefinition = Replace(Replace(Valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

        ''        'JAM 2013-08-10 Remove new lines
        ''        VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")

        ''        Groupnames = Valtbl.Rows(Rcnt).Item("Group")
        ''        If IsDBNull(Valtbl.Rows(Rcnt).Item("Units")) Then
        ''            sUnits = ""
        ''        Else
        ''            sUnits = Valtbl.Rows(Rcnt).Item("Units")
        ''        End If

        ''        'JAM 2013-08-20
        ''        sPreprocessor = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", Valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
        ''        sYears = IIf(IsDBNull(Valtbl.Rows(Rcnt).Item("Years")), "", Valtbl.Rows(Rcnt).Item("Years"))

        ''        'JAM 2013-08-25
        ''        Session("LongVarFiltName") = "At least through year " & CStr(Session("iFilterYear"))
        ''        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
        ''        sJscript = sJscript & "  jbIncludeTroughYear[" & Rcnt.ToString & "]= """ & bInclude.ToString & """; " & vbCr


        ''        Session("LongVarFiltName") = "At least " & CStr(Session("iFilterDataPoints")) & " Data Points"
        ''        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
        ''        sJscript = sJscript & "  jbIncludeNPoint[" & Rcnt.ToString & "]= """ & bInclude.ToString & """; " & vbCr

        ''        Session("LongVarFiltName") = "Used for Initialization"
        ''        bInclude = bIncludedByOthrFilter(sPreprocessor, sYears)
        ''        sJscript = sJscript & "  jbIncludeInit[" & Rcnt.ToString & "]= """ & bInclude.ToString & """; " & vbCr


        ''        Session("LongVarFiltName") = "ALL"
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

        ''sJscript = sJscript & "     NumVars = " & Valtbl.Rows.Count.ToString & ";" & vbCr


        ''Dim intfile As Short, sFileName As String
        ''intfile = FreeFile()
        ''sFileName = PutFdrv & "IFsWeb\Scripts\"
        ' ''If UCase(ActiveBase) = "IFSBASEHIST.RUN" Then
        ' ''    'write to historic file
        ' ''    sFileName = sFileName & "FlexDisplaysVarsH.js"
        ' ''Else
        ' ''    'write to forecast file
        ''sFileName = sFileName & "LongVar_Vars.js"
        ' '' End If

        ''FileOpen(intfile, sFileName, OpenMode.Output)
        ''PrintLine(intfile, sJscript)
        ''FileClose(intfile)

        'JAM 2013-08-25 For other filter recovering
        Session("LongVarFiltName") = vFilter

        'JAM 2013-07-29
        sJscript = "   loadjsfile(""Scripts/LongVar_Vars.js"");" & vbCr
        cmb_VCAT.Attributes.Add("onchange", "cmb_VCAT_click();")

    End Sub

    Protected Sub cmdGeneratejs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGeneratejs.Click
        CreateJSforLongVars()
    End Sub

End Class

