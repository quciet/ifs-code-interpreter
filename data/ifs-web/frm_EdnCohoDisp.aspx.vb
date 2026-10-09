'jrs 2005/05/30 Education Cohort
'
Option Strict Off
Option Explicit On
Imports System.Data.SQLite
Imports System.Data.OleDb
'JAM 2020-09-10
Imports Microsoft.AspNet.FriendlyUrls
Imports System.Runtime.Remoting

Partial Class frm_EdnCohoDisp
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'CODEGEN: This method call is required by the Web Form Designer.
        'Do not modify it using the code editor.
        InitializeComponent()
        'jrs 2005/05/30
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
        End If
        'jrs 205/02/20
        'jrs 2011/05/02 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")

    End Sub

    Protected WithEvents WinFormsToWeb_MessageBox As ArtinSoft.WinFormsToWeb.WebControls.MessageBox
    Protected WithEvents ToolTip1 As System.Web.UI.WebControls.Label
    Protected WithEvents cmd_ScriptFile As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents WinFormsToWeb_MenuManager As ArtinSoft.WinFormsToWeb.WebControls.MenuManager
    Protected WithEvents MainMenu1 As ArtinSoft.WinFormsToWeb.WebControls.MainMenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    '#End Region  

    '------------------------------------------CohortDisp.Frm-----------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last modified by BBH on 8/8/02

    'mti WebForm Changes From June 21, 2004 
    'Dim Session("GraphType") As Short
    'Dim year_run As Short
    'Dim tbl2 As datatable
    'Dim tempyear As Short
    'UPGRADE_WARNING: Lower bound of array TableName was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
    Dim TableName(5) As String
    ''mti Ifs Win April 25, 2004
    'Dim Session("EC_TempDatabase")(, ) As Single
    Dim FieldName(5, 2) As String
    'Dim Session("EdCohort_regionname") As String
    'Dim tempregion As Short
    Dim Case_of_Work As Short
    Dim CaseName As String
    Dim valtbl As DataTable
    'mti WebForm Changes From July 05, 2004
    'Dim popConn As OleDb.OleDbConnection

    'JRS 07/26/02 GROUP
    'Dim CountryGroup(200) As String
    'Dim RegionIdGroup(200) As Short
    'Dim GroupTot As Short
    'Dim sAggregationI As String
    'Dim sAggregationD As String

    'jrs 2008/09/08
    Dim sDefGrp As String
    Dim sDefReg As String
    'JAM 2019-12-21
    Dim vSQLiteCon As SQLiteConnection
    Dim vSQLiteCommand As SQLiteCommand
    Dim vSQLiteCommand2 As SQLiteCommand
    Dim vSQLiteReader As SQLiteDataReader
    Dim vSQLiteReader2 As SQLiteDataReader
    Dim vConnString As String
    'jam 2026/01/22 Declare Maximum cohort
    Dim MaxCohortGroup As Integer





    '==========================frm_CohoDisp=============================
    Private Sub Graph_Draw(ByVal Index As Integer)
        'jrs 2025/02/04 added andalso cntrylist.SelectedIndex > -1 
        'jrs 2014/02/20 need it in case coming from Compare
        If Session("EdCohort_Grouping") = False AndAlso cntrylist.SelectedIndex > -1 Then
            Session("EdCohort_tempregion") = cntrylist.SelectedIndex + 1
        End If
        '--------------------------------------------------------------
        'for now
        '   gphAgedst(4).DataReset = 1 '9 '0

        Select Case Index
            Case 0
                CompositeGraph_Draw(Session("db2Path"), Index, 4, 1, 2, 3, 4)
            Case 1, 2, 3, 4, 5, 6, 7
                SeparateGraph_Draw(Index, 4, 1, 2, 3, 4)
        End Select

        Session("EdCG_Index") = Index
        Session("EdCG_Regionname") = Session("EdCohort_regionname")
        Session("EdCG_tempyear") = Session("EdCohort_tempyear")

        PackJScritpBuilder()

    End Sub

    Public Sub mnuAboutPopulation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "~\frm_AboutPopulation.aspx")
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuAdvance_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim end_year As Object
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        '--------------------------------------------------------------
        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") + 5

        If Case_of_Work = 0 Then
            'jrs 2005/01/10 end_year = Session("LastYear")
            'end_year = LastYearForecast
            end_year = FindLastYear()
            If end_year = 0 Then
                end_year = Session("LastYearForecast")
            End If
        Else
            end_year = Session("PopCohort_year_run")
        End If

        If Session("EdCohort_tempyear") <= end_year Then
            Graph_Draw((Session("EdCohort_GraphType")))
        End If
        If Session("EdCohort_tempyear") >= end_year Then
            objSender.Groups("Time").Items("Advance").Enabled = False
            Session("EdCohort_tempyear") = end_year
        End If
        objSender.Groups("Time").Items("Regress").Enabled = True



    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Support.FormManager.Show(Me, Session("DisplayParent"))
        '    frm_DisplayMenu.Show

    End Sub



    Public Sub mnuRegress_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jrs 2008/06/06 changed to FirstYearSce
        If Session("EdCohort_tempyear") <= Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            Session("EdCohort_tempyear") = Session("EdCohort_tempyear") - 5
            If Session("EdCohort_tempyear") <= Session("FirstYearSce") Then
                Session("EdCohort_tempyear") = Session("FirstYearSce")
                objSender.Groups("Time").Items("Regress").Enabled = False
            End If
            objSender.Groups("Time").Items("Advance").Enabled = True
            Graph_Draw((Session("EdCohort_GraphType")))
        End If

    End Sub


    '==========================frm_CohoDisp=============================
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        '--------------------------------------------------------------


        'JAM 2019-12-22
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                vSQLiteCon.Close()
            End If
        End If

        'mti june, 2002 - Add this procedure
        frameData.Visible = False
        'jrs 2008/06/25
        grdcohort.Visible = False
        lbldata.Visible = False
        'jrs 2011/11/16
        lblJS.Visible = True

    End Sub

    '==========================frm_CohoDisp=============================

    '==========================frm_CohoDisp=============================
    Private Sub cmdGridSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGridSave.Click
        Dim ii As Short
        Dim l As Short
        Dim CDERR_CANCEL As Object
        Dim GraphTitle As String
        'jrs 2012/01/27
        '            Dim DataOutputFileName As Object
        '            '--------------------------------------------------------------

        '            'mti june, 2002 - Add this procedure
        '            '   Get filename
        '            '   --------------

        '            DataOutputFileName = "*.CSV"

        '            On Error GoTo SelDataFileError_Line
        '            'jrs 2008/06/25 
        '            'cmd_ScriptFile.DefaultExt = "CSV"
        '            'cmd_ScriptFile.Filename = DataOutputFileName
        '            'cmd_ScriptFile.DialogTitle = "Save " & GraphTitle & " Data"
        '            'cmd_ScriptFile.InitDir = PutFdrvRun2
        '            'cmd_ScriptFile.Action = 1
        '            DataOutputFileName = Session("PutFdrvRun") & "Save.csv" 'jrs 2008/06/25 cmd_ScriptFile.Filename
        '            '        On Error GoTo 0
        '            If VB.Left(DataOutputFileName, 1) = "*" Then Exit Sub
        '            GoTo WriteDataFile_Line

        'SelDataFileError_Line:
        '            If Err.Number = CDERR_CANCEL Then
        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            Else
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            End If

        'WriteDataFile_Line:
        '            Dim fNum As Integer
        '            fNum = FreeFile()
        '            FileOpen(fNum, DataOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        'jrs 2012/01/27
        'PrintLine(fNum, lbldata.Text)
        'PrintLine(fNum, New String("-", Len(lbldata.Text)))
        Context.Response.Write(lbldata.Text)
        Context.Response.Write(Environment.NewLine)
        Context.Response.Write(New String("-", Len(lbldata.Text)))
        Context.Response.Write(Environment.NewLine)

        For l = 0 To grdcohort.TotalRows - 1
            For ii = 0 To grdcohort.Columns.Count - 1
                'jrs 2012/01/27 Print(fNum, grdcohort(ii, l) & ",")
                Context.Response.Write(grdcohort(ii, l) & ",")
            Next ii
            'jrs 2012/01/27 PrintLine(fNum, TAB)
            Context.Response.Write(Environment.NewLine)
        Next l
        'jrs 2012/01/27 FileClose(fNum)

        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(DataOutputFileName)
        Response.End()
        '**************************************

    End Sub

    '==========================frm_CohoDisp=============================
    Private Sub cntrylist_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cntrylist.SelectedIndexChanged
        '--------------------------------------------------------------

        '  Specify the country selected
        '  ----------------------------
        If cntrylist.SelectedIndex = -1 Then
            Session("EdCohort_tempregion") = 1
        Else
            Session("EdCohort_tempregion") = cntrylist.SelectedIndex + 1
        End If

        Session("EdCohort_regionname") = GRPS(1, Session("EdCohort_tempregion"))
        cntrylist.Visible = False

        'jrs 2014/01/29
        If Session("mnuFixCteScTime") Then
            SetUpMaxsTime()
        End If

        'jrs 2014/02/14
        If Session("bCompare") = False Then
            Graph_Draw((Session("EdCohort_GraphType")))
        End If
    End Sub

    '==========================frm_CohoDisp=============================

    Private Sub frm_EdnCohoDisp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'jrs 2008/09/08
        sDefGrp = "World" '"UNESCO S&W Asia"
        sDefReg = "Afghanistan"

        On Error GoTo ErrHandler16

        Dim l As Short
        '--------------------------------------------------------------


        'end WebMenu


        'JAM 2020-10-26
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim othYear As String
        Dim othFile As String
        Dim qtyPAram As Integer


        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Set form and graph dimensions
        '   -----------------------------

        'frm_CohoDisp.DefInstance.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - HalfInch + 200)
        'frm_CohoDisp.DefInstance.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - HalfInch / 2)
        'frm_CohoDisp.DefInstance.Left = 0 
        'frm_CohoDisp.DefInstance.Top = 0 

        'mti Ifs Win April 25, 2004
        'gphAgedst(1).Top = 0
        'gphAgedst(1).Left = VB6.TwipsToPixelsX(HalfInch)


        'JAM 2020/10/26
        If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Request.QueryString("Year")) Or Not IsNothing(Request.QueryString("File")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                'jrs 2024/04/23
                If Not IsNothing(Request.QueryString("Group")) And Not Page.IsPostBack Then
                    othCountry = Request.QueryString("Group")
                Else
                    othCountry = ""
                End If

            End If

            If Not IsNothing(Request.QueryString("Year")) And Not Page.IsPostBack Then

                othYear = Request.QueryString("Year")

            Else
                othYear = ""
            End If

            If Not IsNothing(Request.QueryString("File")) And Not Page.IsPostBack Then

                othFile = Request.QueryString("File")

            Else
                othFile = ""
            End If



            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")


            If othCountry <> "" And othYear <> "" And othFile <> "" Then
                vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(othCountry), othYear, othFile)

                Response.Redirect(vurl)

            Else
                If othCountry <> "" And othYear = "" And othFile = "" Then
                    vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(othCountry))

                    Response.Redirect(vurl)

                Else
                    If othCountry <> "" And othYear <> "" And othFile = "" Then
                        vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(othCountry), othYear)

                        Response.Redirect(vurl)
                    End If

                End If


            End If


        End If

        'jam 2026/02/27 Initializing Labels global variable
        If Not Page.IsPostBack Then
            If Session("EdnLabelChanged") Is Nothing Then
                Session("EdnLabelChanged") = False

            End If
        Else ' For keeping values in Advance, Previous or Change Year
            If Session("EdnLabelChanged") Then
                Session("EdnLabelChanged") = False

            End If

        End If


        '   Initializing the form with Base Case
        '   ------------------------------------

        'JAM 2019-12-07
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                If Not Session("SQLiteDB") Is Nothing Then 'JAM 2020-04-13
                    vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                Else
                    vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                End If

                'jam 2025/05/31 Changing connection mode
                'vSQLiteCon = New SqliteConnection(vConnString)
                'vSQLiteCon.Open()

                'jam 2025/05/31 Changing connection mode
                vSQLiteCon = OpenJournalSQLite(vConnString)



            End If
        End If


        'JAM 2020-09-15
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String
        Dim vYear As String
        Dim vScenario As String

        If segments.Count = 1 Then

            'jrs 2024/09/03 this is wrong, it should use segments(0)
            'JAM 2024/07/23 Bug fix related to GET parameters
            'jrs 2024/07/08
            'If segments(0) = "RoboHelp_CSH.js" Then
            'jrs 2024/09/03 vCountry = Session("vCountry")
            'Else
            vCountry = segments(0)
            'End If
            vYear = Nothing
            vScenario = Nothing

        ElseIf segments.Count = 2 Then
            vCountry = segments(0)
            'jrs 2024/04/23
            If IsNumeric(segments(1)) Then
                vYear = segments(1)
                vScenario = Nothing
            Else
                vYear = Nothing
                vScenario = segments(1)
            End If
        ElseIf segments.Count = 3 Then
            vCountry = segments(0)
            vYear = segments(1)
            vScenario = segments(2)

        ElseIf segments.Count > 3 Then 'jrs 2025/02/04
            vCountry = segments(0)
            Session("vCountry") = vCountry
            vYear = Nothing
            vScenario = Nothing
        End If

        'jrs 2025/02/04 added if > 0
        If segments.Count > 0 Then
            Session("vCountry") = vCountry
            Session("vYear") = vYear
            Session("vScenario") = vScenario
        End If

        '   Initializing the table names and field names
        '   --------------------------------------------
        'JAM 2019-12-22
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            TableName(1) = "EdnGradsBaseCase"
            TableName(2) = "EdnGradsBaseCase"
            TableName(3) = "EdnGradsBaseCase"
            TableName(4) = "EdnGradsBaseCase"

            TableName(5) = "PopBaseCase"

            'FieldName(1, 1) = "PopulationMales"
            'FieldName(1, 2) = "PopulationFemales"
            FieldName(2, 1) = "PriGradMales"
            FieldName(2, 2) = "PriGradFemales"
            FieldName(3, 1) = "SecGradMales"
            FieldName(3, 2) = "SecGradFemales"
            FieldName(4, 1) = "TerGradMales"
            FieldName(4, 2) = "TerGradFemales"

            FieldName(5, 1) = "PopulationMales"
            FieldName(5, 2) = "PopulationFemales"

        Else
            TableName(1) = "EdnGradsBaseCase"
            TableName(2) = "EdnGradsBaseCase"
            TableName(3) = "EdnGradsBaseCase"
            TableName(4) = "EdnGradsBaseCase"

            TableName(5) = "Pop_Base_Case"

            FieldName(2, 1) = "PriGradMales"
            FieldName(2, 2) = "PriGradFemales"
            FieldName(3, 1) = "SecGradMales"
            FieldName(3, 2) = "SecGradFemales"
            FieldName(4, 1) = "TerGradMales"
            FieldName(4, 2) = "TerGradFemales"

            FieldName(5, 1) = "PopulationMales"
            FieldName(5, 2) = "PopulationFemales"
        End If

        'JRS 2/8/03 mnuRegress.Enabled = False
        'mti CohoDisp May 2004 uncomment later
        'mnuRegress.Enabled = False
        If Not Page.IsPostBack Then

            If Session("POPCG_Case_of_Work") Is Nothing Then
                Case_of_Work = 0
                'jrs 2008/09/16
                Session("POPCG_Case_of_Work") = Case_of_Work
            Else
                Case_of_Work = Session("POPCG_Case_of_Work")
            End If


            If Session("EdCG_Casename") Is Nothing Then
                'jrs 2024/04/24
                CaseName = "Work Case"

                Session("EdCG_Casename") = CaseName
            Else
                CaseName = Session("EdCG_Casename")
            End If

            'mti WebForm Changes From June 21, 2004 -start
            Dim TempDatabase(NCohorts, 8) 'As Single 'has to be session var
            Dim CountryGroup(250) As String
            Dim RegionIdGroup(250) As Short

            TempDatabase = Session("EC_TempDatabase")  '(NCohorts, 2)
            Session("CountryGroup") = CountryGroup
            Session("RegionIdGroup") = RegionIdGroup

            'jrs 2024/09/06 changed from = Nothing to is Nothing
            'mti WebForm Changes From June 21, 2004 -end
            'jrs 2011/05/05
            If Session("EdCohort_Grouping") Is Nothing Then
                Session("EdCohort_Grouping") = False
                Session("EdCohort_tempregion") = 1
                Session("EdCohort_regionname") = GRPS(1, 1)

                'JAM 2020-10-10
                Session("IsGroup") = False
            End If

            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"


            'JAM 2020-02-12
            If Session("mnuSQLite") Is Nothing Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            Else

                If Not Session("mnuSQLite") = "True" Then
                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                Else

                    Session("SQLiteDB") = "IFsPop.db"

                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                End If
            End If


            Session("PopCohort_year_run") = Session("LastYear")

            'jrs 2014/02/19
            ReDim Session("dbSCEs")(3)
            ReDim Session("year_runs")(3)
            ReDim Session("tempyears")(3)
            ReDim Session("FirstYearSces")(3)
            ReDim Session("CaseNames")(3)
            For l = 0 To 3
                Session("dbSCEs")(l) = Session("db2Path")
                Session("CaseNames")(l) = CaseName
            Next l

            '   Initializing the graph with population graph for First Region, Session("FirstYear")
            '   ------------------------------------------------------------------------
            'jrs 2005/01/10 Session("EdCohort_tempyear") = AMAX(Session("FirstYear"), 1995)
            If Session("EdCohort_tempyear") Is Nothing Then
                Session("EdCohort_tempyear") = AMAX(FirstYearForecast, 1995)
            End If

            If Session("EdCohort_GraphType") Is Nothing Then
                Session("EdCohort_GraphType") = 0
            End If

            '   Code which takes the region names from GRPS$ vector
            '   ---------------------------------------------------
            cntrylist.Items.Clear()
            For l = 1 To NR
                cntrylist.Items.Add(New ListItem(GRPS(1, l)))
            Next l
            '   Code which takes the group names from Groupnames vector
            '   ---------------------------------------------------
            lstGroups.Items.Clear()
            'jrs 2006/07/14
            'SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " Order by GroupName", "", valtbl)
            ''JRS 5/21/02
            'Dim i As Integer
            'For i = 0 To valtbl.Rows.Count - 1
            '    lstGroups.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
            'Next i
            'lstGroups.Items.Add(New ListItem("ALL"))

            'jrs 2024/09/06 *** start ***
            If Session("bCheckHideNonCoreGroups") Is Nothing Then
                Session("bCheckHideNonCoreGroups") = bCoreGroupCheck 'cdv 2021/09/01
            End If

            If Session("bCheckHideNonCoreGroups") Then
                For l = 1 To CGCount
                    lstGroups.Items.Add(New ListItem(CoreGroupNames(l)))
                Next l
            Else
                For l = 1 To GCount
                    lstGroups.Items.Add(New ListItem(GroupNames(l)))
                Next l
            End If
            'jrs 2024/09/06 *** end ***


            lstYear.Items.Clear()
            'jrs 2012/08/17
            cmbYr1.Items.Clear()
            cmbYr2.Items.Clear()
            cmbYr3.Items.Clear()
            cmbYr4.Items.Clear()
            'jrs 2024/07/05
            If Session("bCompare") = Nothing Then
                Session("bCompare") = False
            End If

            For l = Session("FirstYear") To Session("LastYear") 'GCount
                lstYear.Items.Add(New ListItem(l.ToString))
                'jrs 2012/08/17
                cmbYr1.Items.Add(New ListItem(l.ToString))
                cmbYr2.Items.Add(New ListItem(l.ToString))
                cmbYr3.Items.Add(New ListItem(l.ToString))
                cmbYr4.Items.Add(New ListItem(l.ToString))
            Next l
            cmbYr1.SelectedIndex = 0
            If cmbYr2.Items.Count > 5 Then
                cmbYr2.SelectedIndex = 5
            End If
            If cmbYr3.Items.Count > 10 Then
                cmbYr3.SelectedIndex = 10
            End If
            If cmbYr4.Items.Count > 15 Then
                cmbYr4.SelectedIndex = 15
            End If

            'jrs 2008/05/07
            Session("AboutParent") = "frm_EdnCohoDisp.aspx"

            'jrs 2020/06/02 Graph_Draw((Session("EdCohort_GraphType")))
            'jrs 2008/06/06
            Session("FirstYearSce") = Session("FirstYear")
        Else
            'jrs 2008/09/16
            Case_of_Work = Session("POPCG_Case_of_Work")
        End If

        'webmenu
        'mti WebForm Changes to be taken by Jose
        wmMain.UserData = "jsolorza::976739975"

        wmMain.MenuBar.ShowOnMouseOver = True

        'jam 2020/10/18
        'wmMain.ClearPixelImage = "/Images/blank.gif"
        'wmMain.PopupIcon = "./images/Popup.gif"
        'wmMain.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmMain.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmMain.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmMain.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmMain.SelectedPopupIcon = "./images/SelectedPopup.gif"


        wmMain.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        wmMain.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmMain.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmMain.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmMain.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmMain.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmMain.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")

        wmMain.ScrollEnabled = True
        wmMain.HideTimer = 1000
        'jrs 2012/01/17
        wmMain.AccessorKeyCode = 27
        'jrs 2012/01/24
        wmMain.ButtonFont.Size = "12px"
        wmMain.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_e2bf"
        'jrs 2005/02/20
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")

        'jrs 2008/06/24 need some code to remove session variables cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"
        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        'jrs 2011/05/02
        If InStr(Session("DisplayParent"), "frm_CountryProfile.aspx") > 0 Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Main Menu"
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Distribution Type"
        cswmGroup1.ID = "Group_2e54"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "All Levels of Education all age cohorts"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "No completed formal education"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Primary or more education"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Secondary or more education"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Tertiary Education"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Labor cohorts by level of education (Male)"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Labor cohorts by level of education (Female)"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Labor cohorts by level of education (Both)"

        'jrs 2012/08/17
        cswmItem = cswmGroup1.Items.Add
        If Session("EdCohort_Grouping") = True Then
            cswmItem.Caption = "Compare Groups"
        Else
            cswmItem.Caption = "Compare Countries"
        End If
        cswmItem.ID = "mnuCompare"

        'mti commented enable this later 
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Show Numbers"
        cswmGroup1.ID = "Group_79e9"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Numbers"

        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Use Group or Region"
        'cswmGroup1.ID = "Group_c57e"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Groups"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'cswmItem.IconImage = "blank.gif"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Regions"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"


        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Set Group or Country/Region"
        cswmGroup1.ID = "mnuGroups"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Countries/Regions"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"
        If Session("EdCohort_Grouping") = True Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If
        'mti Augut 2004 3 end

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Groups"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'mti Augut 2004 3 -start
        'cswmItem.IconImage = "blank.gif"
        If Session("EdCohort_Grouping") = True Then
            'JAM 2020-10-15
            'cswmItem.IconImage = "/Images/checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")

        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        'jrs 2024/09/06
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Hide Non Core Groups"
        cswmItem.IconHeight = 16
        If Session("bCheckHideNonCoreGroups") Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Change Display"
        cswmGroup1.ID = "Group_1113"

        cswmItem = cswmGroup1.Items.Add()
        If Session("EdCohort_Grouping") = True Then
            cswmItem.Caption = "Change Groups"
        Else
            cswmItem.Caption = "Change Countries/Regions"
        End If

        'jrs 2011/11/14
        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()
        If Session("EdCohort_Grouping") = False Then
            'jrs 2026/02/03
            LoadRegionAlphaGroups(5, cswmGroup2, cswmItem)

            'For l = 1 To NR
            '    cswmItem = cswmGroup2.Items.Add()
            '    cswmItem.Caption = GRPS(1, l)
            '    cswmItem.UserData = "Reg"
            '    'cswmItem.OnClick = "cntrymnu_click('" & IFsRegs.Item(GRPS(1, l)).ISO3166_2Code & "');"
            '    If l < NR Then
            '        If GRPS(1, l).Chars(0) <> GRPS(1, l + 1).Chars(0) Then
            '            cswmItem = cswmGroup2.Items.Add()
            '            cswmItem.Caption = "--shadow--"
            '            cswmItem.DividerColor = "#808080"
            '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
            '        End If
            '    End If
            'Next
        Else
            'jrs 2024/09/06
            If Session("bCheckHideNonCoreGroups") Then
                'jrs 2026/02/03
                LoadCoreGroupsAlphaGroups(4, cswmGroup2, cswmItem)

                'For l = 1 To CGCount
                '    cswmItem = cswmGroup2.Items.Add()
                '    cswmItem.Caption = CoreGroupNames(l)
                '    cswmItem.UserData = "Grp"
                '    If l < CGCount Then
                '        If CoreGroupNames(l).Chars(0) <> CoreGroupNames(l + 1).Chars(0) Then
                '            cswmItem = cswmGroup2.Items.Add()
                '            cswmItem.Caption = "--shadow--"
                '            cswmItem.DividerColor = "#808080"
                '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                '        End If
                '    End If
                'Next
            Else
                'jrs 2026/02/03
                LoadAllGroupsAlphaGroups(20, cswmGroup2, cswmItem)

                'For l = 1 To GCount
                '    cswmItem = cswmGroup2.Items.Add()
                '    cswmItem.Caption = GroupNames(l)
                '    cswmItem.UserData = "Grp"
                '    If l < GCount Then
                '        If GroupNames(l).Chars(0) <> GroupNames(l + 1).Chars(0) Then
                '            cswmItem = cswmGroup2.Items.Add()
                '            cswmItem.Caption = "--shadow--"
                '            cswmItem.DividerColor = "#808080"
                '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                '        End If
                '    End If
                'Next
            End If
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Year"

        'jrs 2011/11/14
        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Please select a year"
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        For l = Session("FirstYear") To Session("LastYear")
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = l.ToString
            cswmItem.UserData = "Yr"
            'cswmItem.BackgroundImage = "./images/itemBackground.gif"
            If l < Session("LastYear") Then
                If l Mod 10 = 9 Then
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = "--shadow--"
                    cswmItem.DividerColor = "#808080"
                    cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                End If
            End If
        Next

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Scenario"

        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Base Case"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        If Session("EdCG_Casename") = "Base Case" Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Work Case"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'mti WebForm Changes From June 21, 2004 
        'jrs 2024/04/23
        If Session("EdCG_Casename") = "Work Case" Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        If UCase(Session("ActiveBase")) <> "IFSBASEHIST.RUN" Then
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Historical Forecast"
            cswmItem.IconHeight = 16
            'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
            If Session("EdCG_Casename") = "Historical Forecast" Then
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            Else
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
            End If
        End If

        'jrs 2008/06/05
        'Load other Scenario Menu options for display
        LoadSceMenus(cswmGroup2)

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Title and Subtitle Labels"


        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Time"
        cswmGroup1.ID = "Group_5ca8"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Advance"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regress"

        'jrs 2014/02/14
        If Session("bCompare") = True Then
            wmMain.Groups("Change Display").ShowMenuBarItem = False
            wmMain.Groups("Time").ShowMenuBarItem = False
            wmMain.Groups("mnuGroups").ShowMenuBarItem = False
        End If

        'jrs 2014/02/13
        If Session("mnuFixCtSc") Is Nothing Then
            Session("mnuFixCtSc") = False
            Session("mnuFixCteScTime") = False
            Session("mnuFixCtScMan") = False
            Session("mnuUsePctPop") = False
            Session("bReadGrpMaxs") = False
        End If
        ReDim Preserve Session("MaxScale")(7)

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Change Scale"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale across Time and Regions"
        If Session("mnuFixCtSc") = False Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale across Time"
        If Session("mnuFixCteScTime") = False Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale Manually"
        If Session("mnuFixCtScMan") = False Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Percent of Population"
        If Session("mnuUsePctPop") = False Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        End If

        'jrs 2006/12/21
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16005);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jrs 2008/05/07
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "About"
        'jrs 2025/02/04 cswmGroup1.OnMenuBarClick = "location.href='frm_AboutPopulation.aspx';"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "About"

        AddHandler wmMain.ServerItemClick, AddressOf ItemClickHandlerMain


        'JAM 2020-09-28
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(vCountry)) And Not Session("IsGroup") Then 'JAM 2020-09-28
            'jrs 2011/05/02
            'If Not IsNothing(Request.QueryString("Country")) Then
            If Session("MMCountry") <> vCountry Or Not Page.IsPostBack Then
                'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then

                'Session("MMCountry") = Request.QueryString("Country")

                'JAM 2020-09-28
                Session("MMCountry") = vCountry


                'jrs 2011/09/14
                If Session("EdCohort_Grouping") Then
                    'need to fix menus
                    wmMain.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"
                    wmMain.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif")
                    wmMain.Groups("Change Display").Items(0).Caption = "Change Countries/Regions"
                End If
                Session("EdCohort_Grouping") = False
                'Validate
                Dim rs As DataTable
                SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT NewRegion FROM [Country Translation] as c, " &
                Regionalization & " as r where c.Country = r.Country AND [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs)
                If rs.Rows.Count = 0 Then
                    Session("MMCountry") = ""
                    rs = Nothing
                Else
                    Session("cNumber") = rs.Rows(0).Item("NewRegion")
                End If
            End If
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(vCountry)) And Session("IsGroup") Then 'JAM 2020-10-10
            'If Session("EdCohort_regionname") <> Request.QueryString("Group") Then
            If Session("EdCohort_regionname") <> vCountry Then 'JAM 2020-10-10
                Session.Remove("cNumber")
                Dim gIdx As Integer

                'jrs 2024/09/06
                If Session("bCheckHideNonCoreGroups") Then
                    'jam 2024/08/21
                    If Not IsNothing(Request.QueryString("Group")) Then
                        gIdx = gGetCoreGroupIdx(Request.QueryString("Group"), False, Me)
                    Else
                        'jrs 2024/09/03 gIdx = gGetGroupIdx(Session("EdCohort_regionname"), False, Me)
                        gIdx = gGetCoreGroupIdx(vCountry, False, Me)
                    End If
                Else
                    'jam 2024/08/21
                    If Not IsNothing(Request.QueryString("Group")) Then
                        gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                    Else
                        'jrs 2024/09/03 gIdx = gGetGroupIdx(Session("EdCohort_regionname"), False, Me)
                        gIdx = gGetGroupIdx(vCountry, False, Me)
                    End If
                End If

                If gIdx > 0 Then
                    lstGroups.SelectedIndex = gIdx - 1
                    'Session("MMGroup") = Request.QueryString("Group")

                    Session("MMGroup") = vCountry

                Else
                    Session.Remove("MMGroup")
                End If
                If Session("EdCohort_Grouping") = True Then
                    lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
                Else
                    mnuGroups_Click(wmMain, New Coalesys.WebMenu.ServerItemClickEventArgs(wmMain.Groups("mnuGroups").Items(0)))
                End If
            End If
        End If

        'JAM 2020-09-28
        If Not IsNothing(Request.QueryString("Year")) Or Not IsNothing(vYear) Then
            'jrs 2012/05/03
            'If Not IsNothing(Request.QueryString("Year")) Then
            'jrs 2024/07/08 added Not IsNothing(vYear) andalso
            If Not IsNothing(vYear) AndAlso Session("EdCohort_tempyear") <> vYear Then
                'If Session("EdCohort_tempyear") <> Request.QueryString("Year") Then
                'Session("EdCohort_tempyear") = Request.QueryString("Year")

                'JAM 2020-08-15
                Session("EdCohort_tempyear") = vYear

                If Session("EdCohort_tempyear") < Session("FirstYearSce") Then Session("EdCohort_tempyear") = Session("FirstYearSce")
                If Session("EdCohort_tempyear") > Session("PopCohort_year_run") Then Session("EdCohort_tempyear") = Session("PopCohort_year_run")
            End If
        End If



        'JAM 2020-09-28
        If Not IsNothing(Request.QueryString("File")) Or Not IsNothing(vScenario) Then
            'jrs 2012/05/05
            'If Not IsNothing(Request.QueryString("File")) Then

            'JAM 2020-09-28
            If Session("EdCG_Casename") <> vScenario Then
                'If Session("EdCG_Casename") <> Request.QueryString("File") Then
                'Session("POPCG_Case_of_Work") = Case_of_Work
                'CaseName = Request.QueryString("File")
                'JAM 2020-09-28
                CaseName = vScenario


                'jrs 2014/02/21
                'jrs 2024/03/07 If Request.QueryString("File") = "Base Case" Then
                If vScenario = "Base Case" Then
                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                        'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                        'JAM 2019-12-22
                        If Session("mnuSQLite") Is Nothing Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                        Else

                            If Not Session("mnuSQLite") = "True" Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                            Else

                                Session("SQLiteDB") = "IFsPop.db"

                                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                            End If
                        End If


                    Else
                        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"



                        'JAM 2020-02-12
                        If Session("mnuSQLite") Is Nothing Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                        Else

                            If Not Session("mnuSQLite") = "True" Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                            Else
                                ' Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                                Session("SQLiteDB") = "IFsPopHist.db"

                                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                            End If
                        End If

                    End If
                    'jrs 2024/03/07 ElseIf Request.QueryString("File") = "Work Case" Then
                ElseIf vScenario = "Work Case" Then
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

                    'JAM 2019-12-22
                    If Session("mnuSQLite") Is Nothing Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

                    Else

                        If Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"
                        Else
                            ' Session("db2Path") = Session("PutFdrvRun") & "ifsPopWork.db"

                            Session("SQLiteDB") = "ifsPopWork.db"

                            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                        End If
                    End If



                    'jrs 2024/03/07 ElseIf Request.QueryString("File") = "Historical Forecast" Then
                ElseIf vScenario = "Historical Forecast" Then
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"


                    'JAM 2020-02-12
                    If Session("mnuSQLite") Is Nothing Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                    Else

                        If Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                        Else
                            ' Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                            Session("SQLiteDB") = "IFsPopHist.db"

                            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                        End If
                    End If

                Else
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"


                    'JAM 2020-02-12
                    If Session("mnuSQLite") Is Nothing Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"

                    Else

                        If Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"
                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".db"

                            'jrs 2024/03/20 missing "IFsPop" & 
                            Session("SQLiteDB") = "IFsPop" & CaseName & ".db"

                            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                        End If
                    End If


                    If Dir(Session("db2Path")) = "" Then
                        CaseName = "Base Case"
                        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                            'JAM 2019-12-22
                            If Session("mnuSQLite") Is Nothing Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                            Else

                                If Not Session("mnuSQLite") = "True" Then
                                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                                Else
                                    'Session("db2Path") = Session("PutFdrvRun") & "ifsPop.db"


                                    Session("SQLiteDB") = "IFsPop.db"

                                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                                End If
                            End If


                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"


                            'JAM 2020-02-12
                            If Session("mnuSQLite") Is Nothing Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                            Else

                                If Not Session("mnuSQLite") = "True" Then
                                    Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                                Else
                                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                                    Session("SQLiteDB") = "IFsPopHist.db"

                                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                                End If
                            End If

                        End If
                    End If
                End If

                'jrs 2024/04/24
                If Session("mnuSQLite") = "True" Then
                    If Not Session("SQLiteDB") Is Nothing Then
                        vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                    Else
                        vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                    End If

                    'jam 2025/05/31 Changing connection mode
                    'vSQLiteCon = New SQLiteConnection(vConnString) '& "New=true;"
                    'vSQLiteCon.Open()


                    'jam 2025/05/31 Changing connection mode
                    vSQLiteCon = OpenJournalSQLite(vConnString)


                End If

                'jrs 2014/02/21 uncheck previous sce and check new one
                wmMain.Groups("Change Display").Items("Change Scenario").Group.Items(Session("EdCG_Casename")).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
                Session("EdCG_Casename") = CaseName
                wmMain.Groups("Change Display").Items("Change Scenario").Group.Items(Session("EdCG_Casename")).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"

                Session("PopCohort_year_run") = FindLastYear()
                Session("EdCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("EdCohort_tempyear"))
                Session("FirstYearSce") = FindFirstYear()

                If Session("PopCohort_year_run") <= FirstYearForecast Then
                    Session("EdCohort_tempyear") = FirstYearHistory
                ElseIf Session("EdCohort_tempyear") < FirstYearForecast Then
                    Session("EdCohort_tempyear") = FirstYearForecast
                End If

                'jrs 2024/03/07
                If Not Session("mnuSQLite") Is Nothing Then
                    If Session("mnuSQLite") Then
                        'JAM 2020-05-04
                        If Not Session("SQLiteDB") Is Nothing Then 'JAM 2020-04-13
                            vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                        Else
                            vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                        End If

                        vSQLiteCon.Close()

                        'jam 2025/05/31 Changing connection mode
                        'vSQLiteCon = New SQLiteConnection(vConnString) '& "New=true;"
                        'vSQLiteCon.Open()


                        'jam 2025/05/31 Changing connection mode
                        vSQLiteCon = OpenJournalSQLite(vConnString)



                    End If
                End If
            End If
        End If


        'KIA 63008 ADD THE FOLLOWING IF BLOCK
        If Not Session("cNumber") Is Nothing Then
            lstGroups.Visible = False
            cntrylist.Visible = True
            cntrylist.SelectedIndex = CInt(Session("cNumber")) - 1
            Session.Remove("cNumber")
            cntrylist_SelectedIndexChanged(Me, New System.EventArgs)
        Else
            'jrs 2020/06/02
            If Not Page.IsPostBack Then
                Graph_Draw((Session("EdCohort_GraphType")))
            End If
            PackJScritpBuilder()
        End If

ErrHandler16:
        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) And Not Page.IsPostBack Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

        Exit Sub

    End Sub


    Private Sub lstGroups_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstGroups.SelectedIndexChanged
        'JRS 07/26/02 GROUP
        Session("EdCohort_regionname") = lstGroups.SelectedValue
        'JRS 08/09/02
        If Session("EdCohort_regionname") = "" Then Session("EdCohort_regionname") = lstGroups.Items(0).Text
        lstGroups.Visible = False

        'jrs 2014/01/29
        Session("GroupTot") = Nothing
        If Session("mnuFixCteScTime") Then
            Session("bReadGrpMaxs") = True
        End If
        'jrs 2014/02/14
        If Session("bCompare") = False Then
            Graph_Draw((Session("EdCohort_GraphType")))
        End If
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub MnuBaseCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'MnuBaseCase.Checked = True 
        'mnuWorkCase.Checked = False 
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        On Error GoTo ErrHandler17
        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'If objEA.Item.Caption = "Base Case" Then
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
        'jrs 2008/06/06
        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
        Next
        Case_of_Work = 0
        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        'jrs 2008/06/18
        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            'JAM 2019-12-22
            If Session("mnuSQLite") Is Nothing Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            Else

                If Not Session("mnuSQLite") = "True" Then
                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                Else
                    'Session("db2Path") = Session("PutFdrvRun") & "ifsPop.db"

                    Session("SQLiteDB") = "IFsPop.db"

                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                End If
            End If

        Else
            'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"


            'JAM 2020-02-12
            If Session("mnuSQLite") Is Nothing Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

            Else

                If Not Session("mnuSQLite") = "True" Then
                    Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                Else
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                    Session("SQLiteDB") = "IFsPopHist.db"

                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                End If
            End If


        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        'mti WebForm Changes From July 05, 2004
        'popConn = CreateConnection(Session("db2Path"))
        'Session("popConn") = popConn
        'mti WebForm Changes From July 05, 2004
        'jrs 2008/06/16 changed from FirstYearForecast to FirstYear
        'jrs 2008/06/05
        If Session("EdCohort_tempyear") < Session("FirstYear") Then
            Session("EdCohort_tempyear") = Session("FirstYear")
        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And Session("EdCohort_tempyear") > LastYearHistory Then
            Session("EdCohort_tempyear") = FirstYearHistory
        End If
        Session("FirstYearSce") = Session("FirstYear")

        '    Session("PopCohort_year_run") = Session("LastYear")%
        '    Session("EdCohort_tempyear") = Session("FirstYear")%
        '    Session("EdCohort_tempregion") = 1
        '    Session("EdCohort_regionname") = GRPS$(1, 1)
        '    Session("EdCohort_GraphType") = 1
        CaseName = "Base Case"
        Session("EdCG_Casename") = CaseName
        'End If
        If Session("EdCohort_tempyear") = Session("FirstYear") Then objSender.Groups("Time").Items("Regress").Enabled = False
        If Session("EdCohort_tempyear") < Session("LastYear") Then objSender.Groups("Time").Items("Advance").Enabled = True
        Graph_Draw((Session("EdCohort_GraphType")))

ErrHandler17:
        Exit Sub

    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuchngcntry_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'JRS 07/26/02 GROUP
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") Then

            'If mnuGroups.Text = "Use &Groups" Then
            cntrylist.Visible = True
            lstGroups.Visible = False
        Else
            lstGroups.Visible = True
            cntrylist.Visible = False
        End If
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuchngyr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        lstYear.Visible = True

    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub PrimaryGrad()
        '--------------------------------------------------------------
        Session("EdCohort_GraphType") = 2
        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs


        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String



        If objEA.Item.Caption = "Groups" Then
            objSender.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"
            objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif")
            '"Change Display"
            objSender.Groups("Change Display").Items(0).Caption = "Change Groups" '"Change Countries/Regions"
            'jrs 2012/08/24
            objSender.Groups("Distribution Type").Items("mnuCompare").Caption = "Compare Groups" '"Change Countries/Regions"

            Session("EdCohort_Grouping") = True
            'JAM 2020-10-10
            Session("IsGroup") = True

            'jrs 2008/09/05
            'Search for default group
            Dim i As Integer
            For i = 0 To lstGroups.Items.Count - 1
                If lstGroups.Items(i).Text = sDefGrp Then
                    lstGroups.Items(i).Selected = True
                    'jrs 2020/06/02 lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
                    Session("EdCohort_regionname") = lstGroups.SelectedValue
                    If Session("EdCohort_regionname") = "" Then Session("EdCohort_regionname") = lstGroups.Items(0).Text
                    lstGroups.Visible = False
                    Session("GroupTot") = Nothing
                    If Session("mnuFixCteScTime") Then
                        Session("bReadGrpMaxs") = True
                    End If
                    Exit For
                End If
            Next i

            cntrylist.Visible = False
            lstYear.Visible = False
            'jrs 2014/02/14
            Session("sDefReg") = Session("MMCountry")

            'jrs 2011/05/05
            Session.Remove("MMCountry")
            'jrs 2011/09/22
            Session("MMGroup") = Session("EdCohort_regionname")

            'JAM 2020-10-10
            'Support.FormManager.Show(Me, "frm_EdnCohoDisp.aspx?Group=" & PrepareForURL(Session("EdCohort_regionname")))

            'JAM 2020-10-10
            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")


            vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(Session("EdCohort_regionname")))

            Response.Redirect(vurl)



        Else
            objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"
            objSender.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            objSender.Groups("Change Display").Items(0).Caption = "Change Countries/Regions"
            'jrs 2012/08/24
            objSender.Groups("Distribution Type").Items("mnuCompare").Caption = "Compare Countries" '"Change Countries/Regions"

            Session("EdCohort_Grouping") = False
            'JAM 2020-10-10
            Session("IsGroup") = False


            'jrs 2014/02/06
            Dim sCtryCode As String = "AF"
            If Session("sDefReg") > "" Then
                sCtryCode = Session("sDefReg")
            ElseIf Session("EdCohort_tempregion") > 0 And cntrylist.SelectedIndex = -1 And IFsRegs.Item(Session("EdCohort_regionname")).Index = Session("EdCohort_tempregion") Then
                cntrylist.SelectedIndex = Session("EdCohort_tempregion") - 1
                sCtryCode = IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
            End If

            cntrylist_SelectedIndexChanged(cntrylist, New System.EventArgs)
            lstGroups.Visible = False
            lstYear.Visible = False
            'jrs 2011/05/05
            'JAM 2020-10-14
            'Support.FormManager.Show(Me, "frm_EdnCohoDisp.aspx?Country=" & sCtryCode)



            'JAM 2020-10-10
            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")


            vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(sCtryCode))

            Response.Redirect(vurl)




        End If


    End Sub

    'jrs 2024/09/06
    Public Sub mnuHideNonCoreGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/03/06
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If Session("EdCohort_Grouping") = True Then
            'lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
            cntrylist.Visible = False
            lstYear.Visible = False
            Session("sDefReg") = Session("MMCountry")
            Session.Remove("MMCountry")
            Session("MMGroup") = Session("EdCohort_regionname")

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
            vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(Session("EdCohort_regionname")))

            Response.Redirect(vurl)

        Else
            Dim sCtryCode As String = "AF"
            If Session("sDefReg") > "" Then
                sCtryCode = Session("sDefReg")
            ElseIf Session("EdCohort_tempregion") > 0 And cntrylist.SelectedIndex = -1 And IFsRegs.Item(Session("EdCohort_regionname")).Index = Session("EdCohort_tempregion") Then
                cntrylist.SelectedIndex = Session("EdCohort_tempregion") - 1
                sCtryCode = IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
            ElseIf Session("EdCohort_tempregion") > 0 And IFsRegs.Item(Session("EdCohort_regionname")).Index = Session("EdCohort_tempregion") Then
                sCtryCode = IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
            End If

            cntrylist_SelectedIndexChanged(cntrylist, New System.EventArgs)
            lstGroups.Visible = False
            lstYear.Visible = False

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
            vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(sCtryCode))
            Response.Redirect(vurl)
        End If


    End Sub

    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      JAM 2020-10-10
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function EdnPrepareForFriendlyURL(ByVal sParameter As String) As String
        'Remember to Update JavaScript Function too
        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        EdnPrepareForFriendlyURL = tmp
    End Function



    Public Sub SecondaryGrad()
        '--------------------------------------------------------------
        Session("EdCohort_GraphType") = 3
        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    Public Sub TertiaryGrad()
        '--------------------------------------------------------------
        Session("EdCohort_GraphType") = 4
        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    Public Sub NoEduc()
        '--------------------------------------------------------------
        Session("EdCohort_GraphType") = 1

        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    Public Sub EdAllyrs()
        Session("EdCohort_GraphType") = 0
        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuShowData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------

        'mti june, 2002 - Add this procedure
        'frameData.BringToFront() 
        frameData.Visible = True
        'jrs 2008/06/25
        grdcohort.Visible = True
        lbldata.Visible = True
        'jrs 2008/09/18 hfTitle generates an error if we pass a string with <br/> in it
        'some security validation
        hfTitle.Value = lbldata.Text.Replace("<br/>", "")
        'jrs 2011/11/16
        lblJS.Visible = False

    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuWorkCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '--------------------------------------------------------------
        'mnuWorkCase.Checked = True 
        'MnuBaseCase.Checked = False 

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        On Error GoTo ErrHandler18

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
        'jrs 2008/06/06
        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
        Next

        Case_of_Work = 1
        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

        'JAM 2019-12-22
        If Session("mnuSQLite") Is Nothing Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

        Else

            If Not Session("mnuSQLite") = "True" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"
            Else
                ' Session("db2Path") = Session("PutFdrvRun") & "ifsPopWork.db"


                Session("SQLiteDB") = "ifsPopWork.db"

                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

            End If
        End If


        Session("PopCohort_year_run") = FindLastYear()
        Session("EdCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("EdCohort_tempyear"))

        'jrs 2008/06/16 changed from FirstYearForecast to FirstYear
        'jrs 2008/06/05
        If Session("EdCohort_tempyear") < FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearForecast
        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And Session("EdCohort_tempyear") > LastYearHistory Then
            Session("EdCohort_tempyear") = FirstYearHistory
        End If
        Session("FirstYearSce") = Session("FirstYear")

        CaseName = "Working Scenario"
        Session("EdCG_Casename") = CaseName
        If Session("EdCohort_tempyear") = Session("FirstYear") Then
            'mnuRegress.Enabled = False
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("EdCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If
        If Session("PopCohort_year_run") = Session("FirstYear") Then
            Msg = "You must run the model to create a working case"
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("EdCohort_GraphType")))

ErrHandler18:
        Exit Sub
    End Sub

    '==========================frm_CohoDisp=============================
    Private Sub FillDataGrid(ByRef index As Short)
        Dim cc As Short
        '--------------------------------------------------------------
        'mti june, 2002 - Add this Procedure
        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        grdcohort.TotalRows = NCohorts + 1 'jrs 2026/02/02 22
        grdcohort.RowCount = NCohorts + 1 'jrs 2026/02/02 22

        If index = 2 Then 'Fertility
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            myCol.Width = 100 '1500
            grdcohort.Columns.Add(myCol)

            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            myCol.Width = 200 '2500
            grdcohort.Columns.Add(myCol)

        Else
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            myCol.Width = 100 '1100
            grdcohort.Columns.Add(myCol)

            For cc = 1 To 3
                myCol = New TMSWebControls.Column
                myCol.WidthType = TMSWebControls.WType.Absolute
                myCol.Width = 150 '1300
                grdcohort.Columns.Add(myCol)
            Next
        End If


        'grdcohort.CtlRefresh() 



        For cc = NCohorts To 1 Step -1

            If cc = NCohorts Then
                grdcohort(0, cc) = NAges.ToString & "+" 'jrs 2026/02/02
            Else
                grdcohort(0, cc) = Str(cc * 5 - 5) & "-" & Str(cc * 5 - 1)
            End If

            grdcohort(1, cc) = CStr(Session("EC_TempDatabase")(cc, 1))
            If index <> 2 Then 'not fertility
                grdcohort(2, cc) = CStr(Session("EC_TempDatabase")(cc, 2))

                grdcohort(3, cc) = CStr(Session("EC_TempDatabase")(cc, 1) + Session("EC_TempDatabase")(cc, 2))
            End If

        Next cc

        grdcohort(0, 0) = "Age Cohort"
        If index = 2 Then
            grdcohort(1, 0) = "Fertility"
            Exit Sub
        Else
            If bMaleFirst Then
                grdcohort(1, 0) = "Male"
                grdcohort(2, 0) = "Female"
                grdcohort(3, 0) = "Total"
            Else
                grdcohort(1, 0) = "Female"
                grdcohort(2, 0) = "Male"
                grdcohort(3, 0) = "Total"
            End If
        End If


    End Sub


    Private Sub LoadCountries(ByRef GroupName As String)
        'JRS 07/26/02 GROUP
        Dim valtblG As DataTable
        Dim resG As DataTable
        Dim resR As DataTable
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " WHERE GroupName = """ & GroupName & """", "", resG, "")
        Dim GroupNum As Short
        GroupNum = resG.Rows(0).Item("GroupNum")
        '    Dim valtbl As Recordset
        'SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Grouping, "RegionNo", valtblG, "")
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Grouping & " Order by RegionNo", "", valtblG, "")
        Dim iTotal As Short
        iTotal = 0
        Dim i As Integer
        Dim j As Integer
        i = 0
        '   Load Group Member Names List Box
        'JRS 2/15/02 For ICount% = 1 To valtblG.RecordCount
        Do While Not i = valtblG.Rows.Count
            'JRS 2/15/02 For JCount% = 1 To valtblG.Fields("TotalGroups")
            'JRS 2/15/02 TmpStr$ = "Group" + Trim(Str(JCount%))
            'JRS 2/15/02 If valtblG.Fields(TmpStr$) = GroupNum Then
            If valtblG.Rows(i).Item("GroupNo") = GroupNum Then
                SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT DISTINCT Country, NewRegion FROM " & Regionalization & ", " & Grouping & " WHERE NewRegion = RegionNo AND Region = """ & valtblG.Rows(i).Item("Region") & """", "", resR, "")
                j = 0
                Do While Not j = resR.Rows.Count
                    iTotal = iTotal + 1
                    Session("CountryGroup")(iTotal) = resR.Rows(j).Item("Country")
                    Session("RegionIdGroup")(iTotal) = resR.Rows(j).Item("NewRegion")
                    j = j + 1
                Loop

            End If
            'JRS 2/15/02 Next JCount%
            i = i + 1
            'JRS 2/15/02 Next ICount%
        Loop

        Session("GroupTot") = iTotal
    End Sub

    Sub ItemClickHandlerMain(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'JAM 2020-10-10
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'jam 2024/08/07
        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        'jrs 2024/12/06 vfriendlyurl = FriendlyUrl.Resolve("~/frm_MorbCohorts.aspx")

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue" 'jrs 2008/06/27
                mnuExit_Click()
            Case "Main Menu" 'jrs 2011/05/02
                Session("DisplayParent") = "frm_MainMenu.aspx"
                mnuExit_Click()
            Case "Scenario Files"
            Case "New"
                    'mnuNew_Click(sender, e)
            Case "No completed formal education"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True

                'Turn on bCompare flag
                'jam 2024/09/28
                'Session("bCompare") = False
                'pnlCompare.Visible = False
                NoEduc()
                PackJScritpBuilder()
            Case "Primary or more education"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                PrimaryGrad()
                PackJScritpBuilder()
            Case "Secondary or more education"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                SecondaryGrad()
                PackJScritpBuilder()
            Case "Tertiary Education"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                TertiaryGrad()
                PackJScritpBuilder()
            Case "All Levels of Education all age cohorts"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                EdAllyrs()
                PackJScritpBuilder()
            Case "Labor cohorts by level of education (Male)"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                mnuLaborMale_Click()
                PackJScritpBuilder()
            Case "Labor cohorts by level of education (Female)"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                mnuLaborFemale_Click()
                PackJScritpBuilder()
            Case "Labor cohorts by level of education (Both)"
                'jrs 2014/02/13
                wmMain.Groups("Change Display").ShowMenuBarItem = True
                wmMain.Groups("Time").ShowMenuBarItem = True
                wmMain.Groups("mnuGroups").ShowMenuBarItem = True
                'Turn on bCompare flag
                Session("bCompare") = False
                pnlCompare.Visible = False
                mnuLaborBoth_Click()
                PackJScritpBuilder()
            Case "Compare Countries", "Compare Groups" 'jrs 2012/08/17
                mnuCompare_Click()
            Case "Show Numbers"
                mnuShowData_Click(sender, e)
            Case "Use Groups"
            Case "Groups", "Countries/Regions"
                mnuGroups_Click(sender, e)
                    'SetGroupReg(sender, e,objEA.Item.Caption)
            Case "Hide Non Core Groups" 'jrs 2024/09/06
                If Session("bCheckHideNonCoreGroups") = 0 Then
                    Session("bCheckHideNonCoreGroups") = 1
                Else
                    Session("bCheckHideNonCoreGroups") = 0
                End If
                mnuHideNonCoreGroups_Click(sender, e)
            Case "Change Display"
            Case "Change Countries/Regions", "Change Groups" ', "Change Region"
                    'jrs 2011/11/16 mnuchngcntry_Click(sender, e)
            Case "Set Year"
                    'jrs 2011/11/16 mnuchngyr_Click(sender, e)
            Case "Change Scenario"
                    'jrs 2012/05/04
                    'Case "Base Case"
                    '    MnuBaseCase_Click(sender, e)
                    'Case "Work Case"
                    '    mnuWorkCase_Click(sender, e)
                    'Case "Advance"
                    '    mnuAdvance_Click(sender, e)
                    'Case "Regress"
                    '    mnuRegress_Click(sender, e)
            Case "Change Title and Subtitle Labels"
                'mnuLabels_Click() 'jam 2026/02/27

                'jam 2026/02/27


                Vpath = Request.GetFriendlyUrlFileVirtualPath
                Vext = Request.GetFriendlyUrlFileExtension
                vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohortLabels.aspx")
                vurl = FriendlyUrl.Href("~/frm_EdnCohortLabels")
                Response.Redirect(vurl)




            Case "Fix Constant Scale across Time and Regions" 'jrs 2014/02/14
                mnuFixCtSc_Click(sender, e)
            Case "Fix Constant Scale across Time" 'jrs 2014/02/14
                mnuFixCteScTime_Click(sender, e)
            Case "Fix Constant Scale Manually" 'jrs 2014/02/14
                mnuFixCtScMan_Click(sender, e)
            Case "Use Percent of Population" 'jrs 2014/02/14
                mnuUsePctPop_Click(sender, e)
            Case "Print"
            Case "Save"
            Case "About" 'jrs 2025/02/04 Population"
                mnuAboutPopulation_Click(sender, e)
            Case Else
                'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
                If Not (vSQLiteCon Is Nothing) Then
                    vSQLiteCon.Close()
                    vSQLiteCon.Dispose()
                End If

                'jrs 2011/11/14
                Dim idx As Integer, sUrl As String, sParY As String, sParF As String, sParG As String, pos As Integer
                'JAM 2020-10-10 
                If Not IsNothing(Session("vYear")) Then
                    'If Not IsNothing(Request.QueryString("Year")) Then
                    'JAM 2020-10-10
                    'sParY = "&Year=" & Request.QueryString("Year")
                    sParY = Session("vYear")
                Else
                    sParY = ""
                End If
                'JAM 2020-10-10
                If Not IsNothing(Session("vScenario")) Then
                    ' If Not IsNothing(Request.QueryString("File")) Then
                    'JAM 2020-10-10
                    'sParF = "&File=" & Request.QueryString("File")
                    sParF = Session("vScenario")
                Else
                    sParF = ""
                End If
                'JAM 2020-10-10
                If Not IsNothing(Session("vCountry")) And Not Session("IsGroup") Then
                    'JAM 2020-10-10
                    'If Not IsNothing(Request.QueryString("Country")) Then
                    'sParG = "Country=" & Request.QueryString("Country")
                    sParG = Session("vCountry")
                ElseIf Not IsNothing(Session("vCountry")) And Session("IsGroup") Then 'JAM 2020-10-10
                    'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2012/06/27
                    'sParG = "Group=" & Request.QueryString("Group")
                    sParG = Session("vCountry")
                    'jrs 2024/04/24 it was using the wrong flag
                ElseIf Session("EdCohort_Grouping") = False Then
                    'sParG = "Country=" & IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
                    sParG = IFsRegs.Item(Session("EdCohort_regionname")).ISO3166_2Code
                Else
                    'sParG = "Group=" & Session("EdCohort_regionname")
                    sParG = Session("EdCohort_regionname")
                End If

                If objEA.Item.UserData = "Reg" Then
                    'Server Transfer doesn't change the URL, which we want to allow to be saved by user
                    'Response.Redirect("frm_EdnCohoDisp.aspx?Country=" & IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code & sParY & sParF)

                    'JAM 2020-10-10
                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code, sParY, sParF)
                    Response.Redirect(vurl)


                ElseIf objEA.Item.UserData = "Grp" Then
                    'Response.Redirect("frm_EdnCohoDisp.aspx?Group=" & PrepareForURL(objEA.Item.Caption) & sParY & sParF)

                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", EdnPrepareForFriendlyURL(objEA.Item.Caption), sParY, sParF)
                    Response.Redirect(vurl)


                ElseIf objEA.Item.UserData = "Yr" Then
                    'jrs 2012/05/04
                    'If IsNumeric(objEA.Item.Caption) Then
                    '    lstYear.SelectedIndex = CInt(CInt(Trim(objEA.Item.Caption)) - Session("FirstYear"))
                    '    lstYear_SelectedIndexChanged(objSender, New EventArgs)
                    'End If

                    'Response.Redirect("frm_EdnCohoDisp.aspx?" & sParG & "&Year=" & objEA.Item.Caption & sParF)

                    'JAM 2020-10-10
                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", sParG, objEA.Item.Caption, sParF)
                    Response.Redirect(vurl)


                ElseIf objEA.Item.Caption = "Advance" Then
                    'cdv 2021/05/19 Readjust to be a year divisible by 5
                    If (Session("EdCohort_tempyear") + 1) Mod 5 = 0 Then
                        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") + 1
                    ElseIf Session("EdCohort_tempyear") Mod 5 = 0 Then
                        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") + 5
                    Else
                        While Session("EdCohort_tempyear") Mod 5 <> 0
                            Session("EdCohort_tempyear") = Session("EdCohort_tempyear") + 1
                        End While
                    End If
                    If Session("EdCohort_tempyear") > Session("PopCohort_year_run") Then Session("EdCohort_tempyear") = Session("PopCohort_year_run")

                    'Response.Redirect("frm_EdnCohoDisp.aspx?" & sParG & "&Year=" & Session("EdCohort_tempyear") & sParF)

                    'JAM 2020-10-10
                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", sParG, Session("EdCohort_tempyear"), sParF)
                    Response.Redirect(vurl)


                ElseIf objEA.Item.Caption = "Regress" Then
                    'cdv 2021/05/19 Get year change to be divisble by 5
                    If Session("EdCohort_tempyear") Mod 5 = 0 Then
                        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") - 5
                    ElseIf (Session("EdCohort_tempyear") - 1) Mod 5 = 0 Then
                        Session("EdCohort_tempyear") = Session("EdCohort_tempyear") - 1
                    Else
                        While Session("EdCohort_tempyear") Mod 5 <> 0
                            Session("EdCohort_tempyear") = Session("EdCohort_tempyear") - 1
                        End While
                    End If
                    If Session("EdCohort_tempyear") < Session("FirstYearSce") Then Session("EdCohort_tempyear") = Session("FirstYearSce")
                    'Response.Redirect("frm_EdnCohoDisp.aspx?" & sParG & "&Year=" & Session("EdCohort_tempyear") & sParF)


                    'JAM 2020-10-10
                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", sParG, Session("EdCohort_tempyear"), sParF)
                    Response.Redirect(vurl)

                Else
                    'jrs 2012/05/04 mnuSce_Click(objEA.Item.Caption, objSender)
                    'Response.Redirect("frm_EdnCohoDisp.aspx?" & sParG & sParY & "&File=" & objEA.Item.Caption)

                    'JAM 2020-10-10
                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_EdnCohoDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_EdnCohoDisp", sParG, sParY, objEA.Item.Caption)
                    Response.Redirect(vurl)

                End If
        End Select

        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       jam 2026/02/26      Labels Definition
    '
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub mnuLabels_Click()
        On Error GoTo mnuLabels_Error 'JAM 2016-11-24
        If Session("ThirdDimOk") Then
            If Session("mnuOtherChecked") Then
                Session("SubTitleLabel") = "Using " + Session("VarNameI")(1) + " as diameter for bubbles"
            Else
                If Session("mnuPopulationChecked") Then
                    Session("SubTitleLabel") = "Using Population(MOSTRECENT) as diameter for bubbles"
                Else
                    If Session("mnuGdpChecked") Then
                        Session("SubTitleLabel") = "Using GDP2000(MOSTRECENT) as diameter for bubbles"
                    Else
                        Session("SubTitleLabel") = ""

                    End If

                End If


            End If
        End If


        Session("AxisLabelsParent") = "Scatter.aspx"
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ScatterAxisLabels.aspx")

        lblJS.Enabled = True
        lblJS.Visible = True

        Exit Sub
mnuLabels_Error:
        'MsgBox("Error in mnuNone_Click Routine")
        Exit Sub

    End Sub


    Private Sub lstYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstYear.SelectedIndexChanged
        '--------------------------------------------------------------

        '  Specify the country selected
        '  ----------------------------
        'jrs 2008/06/06 changed to FirstYearSce
        If lstYear.SelectedIndex = -1 Then
            Session("EdCohort_tempyear") = Session("FirstYearSce") '1
        Else
            Session("EdCohort_tempyear") = Session("FirstYearSce") + lstYear.SelectedIndex 'cntrylist.SelectedIndex + 1
        End If

        If Session("EdCohort_tempyear") < Session("FirstYearSce") Then Session("EdCohort_tempyear") = Session("FirstYearSce")
        If Session("EdCohort_tempyear") > Session("PopCohort_year_run") Then Session("EdCohort_tempyear") = Session("PopCohort_year_run")
        lstYear.Visible = False

        '   Allow or disallow forward and backward movement
        '   -----------------------------------------------

        If Session("EdCohort_tempyear") > Session("FirstYearSce") Then
            wmMain.Groups("Time").Items("Regress").Enabled = True
        Else
            wmMain.Groups("Time").Items("Regress").Enabled = False
        End If
        If Session("EdCohort_tempyear") < Session("PopCohort_year_run") Then
            wmMain.Groups("Time").Items("Advance").Enabled = True
        Else
            wmMain.Groups("Time").Items("Advance").Enabled = False
        End If
        Graph_Draw((Session("EdCohort_GraphType")))
    End Sub

    Private Sub CompositeGraph_Draw(ByVal db2Path As String, ByVal gphIndex As Integer, ByVal tblindex As Integer, ByVal findex1 As Integer,
    Optional ByVal findex2 As Integer = 0, Optional ByVal findex3 As Integer = 0,
    Optional ByVal findex4 As Integer = 0)
        '--------------------------------------------------------------
        '4,1,2,3
        Dim tbl2 As DataTable
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dsNew = New DataSet()

        Dim TCohort As Integer
        Dim GraphTitle As String
        'start mti june, 2002
        Dim Tindex As Integer
        'end mti june, 2002
        Dim rId As Integer

        On Error GoTo ErrHandler01

        ReDim Session("EC_TempPopDatabase")(NCohorts, 2)

        ReDim Session("EC_TempDatabase")(NCohorts, 8)



        DTsqlite = New DataTable
        DTsqlite2 = New DataTable

        '   Specify the graph control to be used
        '   ------------------------------------
        Dim i2 As Integer
        'jrs 2005/05/30 
        'For i2 = 0 To 4
        '    gphAgedst(i2).Visible = False
        'Next
        frameData.Visible = False
        'jrs 2008/06/25
        grdcohort.Visible = False
        lbldata.Visible = False

        'JAM 2020-11-01
        'JAM 2020-02-29 Temp Definition, ask
        'db2Path = Session("PutFdrvRun") & "IFsPopWork.mdb"

        'Session("SQLiteDB") = "ifsPopWork.db"

        'Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")


        'jrs 2006/07/14 create connecion
        Dim Conn As OleDb.OleDbConnection

        'JAM 2020-11-01
        'JAM 2020-02-29
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            db2Path = Session("PutFdrvRun") & "IFsPopWork.mdb"
            Conn = ADO_OleDB_CreateConnection(db2Path)
            Conn.Open()

        End If

        'jrs 2023/08/24 Dim IfsConn As OleDb.OleDbConnection
        Dim IfsConn As SQLiteConnection = New SQLiteConnection
        'jrs 2023/08/24 IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"
        'tbl2 = New DataTable
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'JAM 2020-11-01
        'JAM 2020-02-29
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            myCMD.Connection = Conn
        End If

        Dim strSQL As String
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        regDS = New DataSet
        'jrs 2023/08/24
        regDS.EnforceConstraints = False

        Dim bFirst As Boolean
        Dim i As Integer


        'JAM 2020-04-06
        Dim dtnew As DataTable

        'take population
        '   Change the data source
        '   ----------------------
        'JRS 07/26/02 GROUP
        Tindex = 5

        'JAM 2019-11-17
        ''If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then

        If Session("EdCohort_Grouping") = False Then
            'jrs 2006/07/14 SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
            'ADO_OleDB_ReadDataTable(Conn, "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2)
            'JAM 2020 - 3 - 3
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
                i2 = 0
                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) = tbl2.Rows(i2).Item(FieldName(Tindex, 1))
                    'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) = tbl2.Rows(i2).Item(FieldName(Tindex, 2))
                    'i2 = i2 + 1
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) = rdr.Item(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) = rdr.Item(FieldName(Tindex, 2))
                Loop
                rdr.Close()
            Else  'JAM 2020-04-05

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(5) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())


                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))

                End While
                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()
            End If
        Else
            'jrs 2006/07/17 LoadCountries(Session("EdCohort_regionname"))   'do once!
            'jrs 2006/07/15
            'strSQL = "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("RegionIdGroup")(1)) & " and Years = " & Str(Session("EdCohort_tempyear"))
            'For rId = 2 To Session("GroupTot")
            '    strSQL = strSQL & "; SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear"))
            'Next
            'rdr = ADO_OleDB_GetReader(myCMD, strSQL)


            'jam 2025/05/31 Changing connection mode
            'IFsConn.Open()
            If IfsConn.State <> ConnectionState.Open Then
                IfsConn.Open()
            End If


            'jrs 2023/08/24 Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim rdGrpCmd As SQLiteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SQLiteDataReader
            rdGrp = rdGrpCmd.ExecuteReader


            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            'jrs 2023/08/24 Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim rdRegCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SQLiteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            'jrs 2023/08/24 Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim rdRegIdCmd As SQLiteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SQLiteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            Dim popDA As OleDb.OleDbDataAdapter

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            Else

                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                'jrs 2023/10/17 vSQLiteCommand.CommandText = "SELECT TimeId,  cast(RegionId AS int2) RegionId,CohortId,PopulationMales,PopulationFemales FROM " & TableName(Tindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteCommand.CommandText = "SELECT TimeId,RegionId,CohortId,PopulationMales,PopulationFemales FROM " & TableName(Tindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)

                dtnew = DTsqlite.Clone
                'jrs 2023/08/24 no longer necessary
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")
                'For Each row As DataRow In DTsqlite.Rows
                'dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)


            End If

            '-JAM 2020-03-03
            'While (vSQLiteReader.Read())


            '        Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
            '        Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))

            'End While





            'jrs 2023/08/24 grpDA.Fill(regDS, "Groups")
            'jrs 2023/08/24 regDA.Fill(regDS, "Regions")
            'jrs 2023/08/24 regIDDA.Fill(regDS, "RegionIDs")

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                popDA.Fill(regDS, "Population")
            End If
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                             regDS.Tables("RegionIDs").Columns("RegionNum"),
                                             regDS.Tables("Regions").Columns("RegionNo"))

            Dim regPopRel As DataRelation

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))
            Else
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))
            End If

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                bFirst = True
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    If bFirst Then
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = cRow.Item(FieldName(Tindex, 2))
                                        bFirst = False
                                    Else
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) + cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) + cRow.Item(FieldName(Tindex, 2))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next
            Else 'JAM 2020-04-06
                bFirst = True
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    If bFirst Then
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) = cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) = cRow.Item(FieldName(Tindex, 2))
                                        bFirst = False
                                    Else
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) + cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) + cRow.Item(FieldName(Tindex, 2))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            '        Dim rId As Integer
            'For rId = 1 To Session("GroupTot")
            '    'jrs 2006/07/14 SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
            '    'ADO_OleDB_ReadDataTable(Conn, "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2)
            '    'rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear")))
            '    i2 = 0
            '    Do While rdr.Read 'Not tbl2.Rows.Count = i2
            '        If rId = 1 Then
            '            'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) = tbl2.Rows(i2).Item(FieldName(Tindex, 1))
            '            'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) = tbl2.Rows(i2).Item(FieldName(Tindex, 2))
            '            Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) = rdr.Item(FieldName(Tindex, 1))
            '            Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) = rdr.Item(FieldName(Tindex, 2))
            '        Else
            '            'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) + tbl2.Rows(i2).Item(FieldName(Tindex, 1))
            '            'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) + tbl2.Rows(i2).Item(FieldName(Tindex, 2))
            '            Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) + rdr.Item(FieldName(Tindex, 1))
            '            Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) + rdr.Item(FieldName(Tindex, 2))
            '        End If
            '        'i2 = i2 + 1
            '    Loop
            '    'rdr.Close()
            '    rdr.NextResult()
            'Next rId
            'rdr.Close()

        End If


        ''    Else  'JAM 2019-12-23

        ''If Session("EdCohort_Grouping") = False Then
        ''    'jrs 2006/07/14 SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
        ''    'ADO_OleDB_ReadDataTable(Conn, "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2)
        ''    ''rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))

        ''    ''i2 = 0
        ''    ''Do While rdr.Read 'Not tbl2.Rows.Count = i2
        ''    ''    'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) = tbl2.Rows(i2).Item(FieldName(Tindex, 1))
        ''    ''    'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) = tbl2.Rows(i2).Item(FieldName(Tindex, 2))
        ''    ''    'i2 = i2 + 1
        ''    ''    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) = rdr.Item(FieldName(Tindex, 1))
        ''    ''    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) = rdr.Item(FieldName(Tindex, 2))
        ''    ''Loop
        ''    ''rdr.Close()


        ''    'JAM 2019-12-23
        ''    vSQLiteCommand = vSQLiteCon.CreateCommand()
        ''    vSQLiteCommand.CommandText = "Select * FROM " & TableName(5) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " And TimeId = " & Str(Session("EdCohort_tempyear"))
        ''    vSQLiteReader = vSQLiteCommand.ExecuteReader()


        ''    While (vSQLiteReader.Read())


        ''        Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
        ''        Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))

        ''    End While



        ''Else
        ''    'jrs 2006/07/17 LoadCountries(Session("EdCohort_regionname"))   'do once!
        ''    'jrs 2006/07/15
        ''    'strSQL = "Select * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("RegionIdGroup")(1)) & " And Years = " & Str(Session("EdCohort_tempyear"))
        ''    'For rId = 2 To Session("GroupTot")
        ''    '    strSQL = strSQL & "; Select * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " And Years = " & Str(Session("EdCohort_tempyear"))
        ''    'Next
        ''    'rdr = ADO_OleDB_GetReader(myCMD, strSQL)
        ''    IfsConn.Open()
        ''    Dim PopDT As DataTable
        ''    Dim vrow As Integer
        ''    'Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("Select * FROM " & GroupIDNew, IfsConn)
        ''    'Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("Select * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
        ''    'Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
        ''    'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)


        ''    'JAM 2020-02-16
        ''    SetDataTablefromAccess(Session("IFs_MDB2Active"), "Select RegionNum, GroupName From " & GroupIDNew & "," & Grouping & "," & RegionIDNew & " Where GroupNum = GroupNo And RegionNo = RegionNum", "", PopDT, "")


        ''    'JAM 2019-12-23
        ''    vSQLiteCommand = vSQLiteCon.CreateCommand()
        ''    vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
        ''    vSQLiteReader = vSQLiteCommand.ExecuteReader()

        ''            i = 1
        ''            While (vSQLiteReader.Read())

        ''                i = i + 1

        ''            End While

        ''            ReDim Session("vRegions")(i + 1, 4)

        ''            'vSQLiteReader = vSQLiteCommand.ExecuteReader()

        ''            i = 1
        ''            While (vSQLiteReader.Read())


        ''                Session("vRegions")(i, 1) = vSQLiteReader("TimeId")
        ''                Session("vRegions")(i, 2) = vSQLiteReader("RegionId")
        ''                Session("vRegions")(i, 3) = vSQLiteReader("CohortId")

        ''                i = i + 1
        ''            End While





        ''    'regDS = New DataSet
        ''    'grpDA.Fill(regDS, "Groups")
        ''    'regDA.Fill(regDS, "Regions")
        ''    'regIDDA.Fill(regDS, "RegionIDs")
        ''    'popDA.Fill(regDS, "Population")
        ''    'grpRegRel = regDS.Relations.Add("GrpRegion",
        ''    '                         regDS.Tables("Groups").Columns("GroupNum"),
        ''    '                         regDS.Tables("Regions").Columns("GroupNo"))
        ''    'regRegIdRel = regDS.Relations.Add("RegRegId",
        ''    '                         regDS.Tables("RegionIDs").Columns("RegionNum"),
        ''    '                         regDS.Tables("Regions").Columns("RegionNo"))
        ''    'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
        ''    '                         regDS.Tables("RegionIDs").Columns("RegionNum"),
        ''    '                         regDS.Tables("Population").Columns("Region"))
        ''    'bFirst = True
        ''    'For Each pppRow In regDS.Tables("Groups").Rows '2019-12-23 Ask Jose
        ''    '    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
        ''    '        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
        ''    '            For Each pRow In ppRow.GetParentRows(regRegIdRel)
        ''    '                'Console.WriteLine(pRow("CustomerID").ToString())
        ''    '                For Each cRow In pRow.GetChildRows(regPopRel)
        ''    '                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
        ''    '                    If bFirst Then
        ''    '                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = cRow.Item(FieldName(Tindex, 1))
        ''    '                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = cRow.Item(FieldName(Tindex, 2))
        ''    '                        bFirst = False
        ''    '                    Else
        ''    '                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) + cRow.Item(FieldName(Tindex, 1))
        ''    '                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) + cRow.Item(FieldName(Tindex, 2))
        ''    '                    End If
        ''    '                Next


        ''    '            Next
        ''    '        Next
        ''    '    End If
        ''    'Next

        ''    bFirst = True
        ''    If PopDT.Rows.Count > 0 Then
        ''        vrow = 1
        ''        While Not PopDT.Rows.Count = vrow
        ''            i = 1
        ''            While (vSQLiteReader.Read())
        ''                If PopDT.Rows(vrow).Item("GroupName") = Session("EdCohort_regionname") Then
        ''                    If Session("vRegions")(i, 2) = PopDT.Rows(vrow).Item("RegionNum") Then
        ''                        If bFirst Then
        ''                            Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
        ''                            Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))
        ''                            bFirst = False
        ''                        Else
        ''                            Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) + vSQLiteReader(FieldName(Tindex, 1))
        ''                            Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) + vSQLiteReader(FieldName(Tindex, 2))
        ''                        End If


        ''                    End If
        ''                End If
        ''                i = i + 1
        ''            End While

        ''            vrow = vrow + 1

        ''        End While

        ''    End If






        ''End If




        ''End If

        'End take population
        'MsgBox db2.Name

        '   Change the data source
        '   ----------------------
        'jrs 2006/07/14
        'tbl2 = New DataTable
        'JRS 07/26/02 GROUP



        If Session("EdCohort_Grouping") = False Then

            'JAM 2019-12-20
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                'jrs 2006/07/14 SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
                'ADO_OleDB_ReadDataTable(Conn, "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2)

                'JAM 2020-02-16
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))





                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    'males - tertiary to No completed formal education
                    'TCohort = tbl2.Rows(i2).Item("NCohorts") '4=tertiary Male,3=Sec Male, 2=Pri Male, 1 = No Edn Male
                    'Session("EC_TempDatabase")(TCohort, 4) = tbl2.Rows(i2).Item(FieldName(findex4, 1))
                    'Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, tbl2.Rows(i2).Item(FieldName(findex3, 1)) - Session("EC_TempDatabase")(TCohort, 4))
                    'Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, tbl2.Rows(i2).Item(FieldName(findex2, 1)) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                    'Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))

                    ''females '8=tertiary Female,7=Sec Female, 6=Pri female, 5 = No Edn female
                    'Session("EC_TempDatabase")(TCohort, 8) = tbl2.Rows(i2).Item(FieldName(findex4, 2))
                    'Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, tbl2.Rows(i2).Item(FieldName(findex3, 2)) - Session("EC_TempDatabase")(TCohort, 8))
                    'Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, tbl2.Rows(i2).Item(FieldName(findex2, 2)) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                    'Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))

                    ''            Session("EC_TempDatabase")(TCohort, 4) = tbl2.Rows(i2).Item(FieldName(findex2, 2))
                    ''            Session("EC_TempDatabase")(TCohort, 5) = tbl2.Rows(i2).Item(FieldName(findex3, 2))
                    ''            Session("EC_TempDatabase")(TCohort, 6) = tbl2.Rows(i2).Item(FieldName(findex4, 2))

                    'i2 = i2 + 1

                    'males - tertiary to No completed formal education
                    TCohort = rdr.Item("NCohorts") '4=tertiary Male,3=Sec Male, 2=Pri Male, 1 = No Edn Male
                    Session("EC_TempDatabase")(TCohort, 4) = rdr.Item(FieldName(findex4, 1))
                    Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, rdr.Item(FieldName(findex3, 1)) - Session("EC_TempDatabase")(TCohort, 4))
                    Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, rdr.Item(FieldName(findex2, 1)) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                    Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))

                    'females '8=tertiary Female,7=Sec Female, 6=Pri female, 5 = No Edn female
                    Session("EC_TempDatabase")(TCohort, 8) = rdr.Item(FieldName(findex4, 2))
                    Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, rdr.Item(FieldName(findex3, 2)) - Session("EC_TempDatabase")(TCohort, 8))
                    Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, rdr.Item(FieldName(findex2, 2)) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                    Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))

                Loop
                'rdr.Close()
            Else

                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()




                While (vSQLiteReader.Read())



                    TCohort = vSQLiteReader("CohortId") '4=tertiary Male,3=Sec Male, 2=Pri Male, 1 = No Edn Male
                    Session("EC_TempDatabase")(TCohort, 4) = vSQLiteReader(FieldName(findex4, 1))
                    Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, vSQLiteReader(FieldName(findex3, 1)) - Session("EC_TempDatabase")(TCohort, 4))
                    Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, vSQLiteReader(FieldName(findex2, 1)) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                    Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))

                    'females '8=tertiary Female,7=Sec Female, 6=Pri female, 5 = No Edn female
                    Session("EC_TempDatabase")(TCohort, 8) = vSQLiteReader(FieldName(findex4, 2))
                    Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, vSQLiteReader(FieldName(findex3, 2)) - Session("EC_TempDatabase")(TCohort, 8))
                    Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, vSQLiteReader(FieldName(findex2, 2)) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                    Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))


                End While

                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()


            End If



        Else
            'jrs 2006/07/14 Countries already loaded!!! LoadCountries(Session("EdCohort_regionname"))
            '        Dim rId As Integer
            'jrs 2006/07/15
            'strSQL = "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("RegionIdGroup")(1)) & " and Years = " & Str(Session("EdCohort_tempyear"))
            'For rId = 2 To Session("GroupTot")
            '    strSQL = strSQL & "; SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear"))
            'Next
            'rdr = ADO_OleDB_GetReader(myCMD, strSQL)

            'JAM 2020-02-16





            bFirst = True
            'JAM 2019-12-23
            'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then

            Dim edDA As OleDb.OleDbDataAdapter

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                edDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(tblindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                edDA.Fill(regDS, "Education")
            Else
                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT TimeId,RegionId,CohortId,PriGradMales,PriGradFemales, SecGradMales,SecGradFemales,TerGradMales,TerGradFemales FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2023/08/24
                'No longer necessary
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")

                'For Each row As DataRow In DTsqlite2.Rows
                'dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)

            End If


            Dim regEdRel As DataRelation

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regEdRel = regDS.Relations.Add("RegEducation",
                               regDS.Tables("RegionIDs").Columns("RegionNum"),
                               regDS.Tables("Education").Columns("Region"))
            Else
                ''regEdRel = regDS.Relations.Add("RegEducation",
                ''regDS.Tables("RegionIDs").Columns("RegionNum"),
                ''DTsqlite.Columns("RegionId"))
                regEdRel = regDS.Relations.Add("RegEducation",
                               regDS.Tables("RegionIDs").Columns("RegionNum"),
                             regDS.Tables("EdnGradsBaseCase").Columns(1))

            End If

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regEdRel)
                                    TCohort = cRow.Item("NCohorts")
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    If bFirst Then
                                        'males
                                        Session("EC_TempDatabase")(TCohort, 4) = cRow.Item(FieldName(findex4, 1))
                                        Session("EC_TempDatabase")(TCohort, 3) = cRow.Item(FieldName(findex3, 1))
                                        Session("EC_TempDatabase")(TCohort, 2) = cRow.Item(FieldName(findex2, 1))
                                        'females
                                        Session("EC_TempDatabase")(TCohort, 8) = cRow.Item(FieldName(findex4, 2))
                                        Session("EC_TempDatabase")(TCohort, 7) = cRow.Item(FieldName(findex3, 2))
                                        Session("EC_TempDatabase")(TCohort, 6) = cRow.Item(FieldName(findex2, 2))
                                        bFirst = False
                                    Else
                                        Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + cRow.Item(FieldName(findex4, 1))
                                        Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + cRow.Item(FieldName(findex3, 1))
                                        Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + cRow.Item(FieldName(findex2, 1))

                                        Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + cRow.Item(FieldName(findex4, 2))
                                        Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + cRow.Item(FieldName(findex3, 2))
                                        Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + cRow.Item(FieldName(findex2, 2))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            Else 'JAM 2020-04-07

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regEdRel)
                                    TCohort = cRow.Item("CohortId")
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    If bFirst Then
                                        'males
                                        Session("EC_TempDatabase")(TCohort, 4) = cRow.Item(FieldName(findex4, 1))
                                        Session("EC_TempDatabase")(TCohort, 3) = cRow.Item(FieldName(findex3, 1))
                                        Session("EC_TempDatabase")(TCohort, 2) = cRow.Item(FieldName(findex2, 1))
                                        'females
                                        Session("EC_TempDatabase")(TCohort, 8) = cRow.Item(FieldName(findex4, 2))
                                        Session("EC_TempDatabase")(TCohort, 7) = cRow.Item(FieldName(findex3, 2))
                                        Session("EC_TempDatabase")(TCohort, 6) = cRow.Item(FieldName(findex2, 2))
                                        bFirst = False
                                    Else
                                        Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + cRow.Item(FieldName(findex4, 1))
                                        Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + cRow.Item(FieldName(findex3, 1))
                                        Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + cRow.Item(FieldName(findex2, 1))

                                        Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + cRow.Item(FieldName(findex4, 2))
                                        Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + cRow.Item(FieldName(findex3, 2))
                                        Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + cRow.Item(FieldName(findex2, 2))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next


            End If

            ''Else 'JAM 2018-12-23 Ask Jose

            ''    'JAM 2020-02-16
            ''    'For Each pppRow In regDS.Tables("Groups").Rows
            ''    '    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
            ''    '        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
            ''    '            For Each pRow In ppRow.GetParentRows(regRegIdRel)
            ''    '                'Console.WriteLine(pRow("CustomerID").ToString())
            ''    '                For Each cRow In pRow.GetChildRows(regEdRel)
            ''    '                    TCohort = cRow.Item("NCohorts")
            ''    '                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
            ''    '                    If bFirst Then
            ''    '                        'males
            ''    '                        Session("EC_TempDatabase")(TCohort, 4) = cRow.Item(FieldName(findex4, 1))
            ''    '                        Session("EC_TempDatabase")(TCohort, 3) = cRow.Item(FieldName(findex3, 1))
            ''    '                        Session("EC_TempDatabase")(TCohort, 2) = cRow.Item(FieldName(findex2, 1))
            ''    '                        'females
            ''    '                        Session("EC_TempDatabase")(TCohort, 8) = cRow.Item(FieldName(findex4, 2))
            ''    '                        Session("EC_TempDatabase")(TCohort, 7) = cRow.Item(FieldName(findex3, 2))
            ''    '                        Session("EC_TempDatabase")(TCohort, 6) = cRow.Item(FieldName(findex2, 2))
            ''    '                        bFirst = False
            ''    '                    Else
            ''    '                        Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + cRow.Item(FieldName(findex4, 1))
            ''    '                        Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + cRow.Item(FieldName(findex3, 1))
            ''    '                        Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + cRow.Item(FieldName(findex2, 1))

            ''    '                        Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + cRow.Item(FieldName(findex4, 2))
            ''    '                        Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + cRow.Item(FieldName(findex3, 2))
            ''    '                        Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + cRow.Item(FieldName(findex2, 2))
            ''    '                    End If
            ''    '                Next
            ''    '            Next
            ''    '        Next
            ''    '    End If
            ''    'Next


            ''    'JAM 2020-02-16
            ''    Dim PopDt2 As DataTable
            ''Dim vrow2 As Integer


            ''SetDataTablefromAccess(Session("IFs_MDB2Active"), "Select GroupName,RegionNum From " & GroupIDNew & "," & Grouping & "," & RegionIDNew & " Where GroupNum = GroupNo And RegionNo = RegionNum", "", PopDt2, "")

            '' 'JAM 2019-12-23
            ''vSQLiteCommand = vSQLiteCon.CreateCommand()
            ''vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
            ''vSQLiteReader = vSQLiteCommand.ExecuteReader()


            ''bFirst = True
            ''If PopDt2.Rows.Count > 0 Then
            ''    vrow2 = 1
            ''    While Not PopDt2.Rows.Count = vrow2
            ''        i = 1
            ''        While (vSQLiteReader.Read())
            ''            If PopDt2.Rows(vrow2).Item("GroupName") = Session("EdCohort_regionname") Then
            ''                If vSQLiteReader("RegionId") = PopDt2.Rows(vrow2).Item("RegionNum") Then
            ''                    TCohort = vSQLiteReader("CohortId")
            ''                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
            ''                    If bFirst Then
            ''                        'males
            ''                        Session("EC_TempDatabase")(TCohort, 4) = vSQLiteReader(FieldName(findex4, 1))
            ''                        Session("EC_TempDatabase")(TCohort, 3) = vSQLiteReader(FieldName(findex3, 1))
            ''                        Session("EC_TempDatabase")(TCohort, 2) = vSQLiteReader(FieldName(findex2, 1))
            ''                        'females
            ''                        Session("EC_TempDatabase")(TCohort, 8) = vSQLiteReader(FieldName(findex4, 2))
            ''                        Session("EC_TempDatabase")(TCohort, 7) = vSQLiteReader(FieldName(findex3, 2))
            ''                        Session("EC_TempDatabase")(TCohort, 6) = vSQLiteReader(FieldName(findex2, 2))
            ''                        bFirst = False
            ''                    Else
            ''                        Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + vSQLiteReader(FieldName(findex4, 1))
            ''                        Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + vSQLiteReader(FieldName(findex3, 1))
            ''                        Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + vSQLiteReader(FieldName(findex2, 1))

            ''                        Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + vSQLiteReader(FieldName(findex4, 2))
            ''                        Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + vSQLiteReader(FieldName(findex3, 2))
            ''                        Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + vSQLiteReader(FieldName(findex2, 2))
            ''                    End If

            ''                End If
            ''            End If
            ''            i = i + 1
            ''        End While

            ''        vrow2 = vrow2 + 1

            ''    End While

            ''End If





            ''End If

            ''If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then
            IfsConn.Close()

            ''End If
            'For rId = 1 To Session("GroupTot")
            '    'jrs 2006/07/14 SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
            '    'ADO_OleDB_ReadDataTable(Conn, "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2)
            '    'rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear")))
            '    i2 = 0
            '    Do While rdr.Read 'Not tbl2.Rows.Count = i2
            '        'TCohort = tbl2.Rows(i2).Item("NCohorts")
            '        TCohort = rdr.Item("NCohorts")
            '        If rId = 1 Then
            '            ''males
            '            'Session("EC_TempDatabase")(TCohort, 4) = tbl2.Rows(i2).Item(FieldName(findex4, 1))
            '            'Session("EC_TempDatabase")(TCohort, 3) = tbl2.Rows(i2).Item(FieldName(findex3, 1))
            '            'Session("EC_TempDatabase")(TCohort, 2) = tbl2.Rows(i2).Item(FieldName(findex2, 1))
            '            ''females
            '            'Session("EC_TempDatabase")(TCohort, 8) = tbl2.Rows(i2).Item(FieldName(findex4, 2))
            '            'Session("EC_TempDatabase")(TCohort, 7) = tbl2.Rows(i2).Item(FieldName(findex3, 2))
            '            'Session("EC_TempDatabase")(TCohort, 6) = tbl2.Rows(i2).Item(FieldName(findex2, 2))

            '            'males
            '            Session("EC_TempDatabase")(TCohort, 4) = rdr.Item(FieldName(findex4, 1))
            '            Session("EC_TempDatabase")(TCohort, 3) = rdr.Item(FieldName(findex3, 1))
            '            Session("EC_TempDatabase")(TCohort, 2) = rdr.Item(FieldName(findex2, 1))
            '            'females
            '            Session("EC_TempDatabase")(TCohort, 8) = rdr.Item(FieldName(findex4, 2))
            '            Session("EC_TempDatabase")(TCohort, 7) = rdr.Item(FieldName(findex3, 2))
            '            Session("EC_TempDatabase")(TCohort, 6) = rdr.Item(FieldName(findex2, 2))

            '        Else

            '            'Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + tbl2.Rows(i2).Item(FieldName(findex4, 1))
            '            'Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + tbl2.Rows(i2).Item(FieldName(findex3, 1))
            '            'Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + tbl2.Rows(i2).Item(FieldName(findex2, 1))

            '            'Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + tbl2.Rows(i2).Item(FieldName(findex4, 2))
            '            'Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + tbl2.Rows(i2).Item(FieldName(findex3, 2))
            '            'Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + tbl2.Rows(i2).Item(FieldName(findex2, 2))

            '            Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + rdr.Item(FieldName(findex4, 1))
            '            Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + rdr.Item(FieldName(findex3, 1))
            '            Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + rdr.Item(FieldName(findex2, 1))

            '            Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + rdr.Item(FieldName(findex4, 2))
            '            Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + rdr.Item(FieldName(findex3, 2))
            '            Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + rdr.Item(FieldName(findex2, 2))

            '        End If

            '        'i2 = i2 + 1
            '    Loop
            '    'rdr.Close()
            '    rdr.NextResult()
            'Next rId
            'rdr.Close()

            For TCohort = 1 To 21
                'adjust group calc for sec=sec-ter, pri=pri-(sec+ter) etc.
                'Session("EC_TempDatabase")(TCohort, 4) = tbl2.Rows(i2).Item(FieldName(findex4, 1))
                Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, Session("EC_TempDatabase")(TCohort, 3) - Session("EC_TempDatabase")(TCohort, 4))
                Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, Session("EC_TempDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))
                'females
                'Session("EC_TempDatabase")(TCohort, 8) = tbl2.Rows(i2).Item(FieldName(findex4, 2))
                Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, Session("EC_TempDatabase")(TCohort, 7) - Session("EC_TempDatabase")(TCohort, 8))
                Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, Session("EC_TempDatabase")(TCohort, 6) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))
            Next TCohort

            '               tbl2.MoveNext
            '            Loop
            '        Next rId
            'if its fertility or mortality get average
            '        If Tindex > 1 And Session("GroupTot") > 1 Then
            '            For rId = 1 To NCohorts
            '                Session("EC_TempDatabase")(rId, 1) = Session("EC_TempDatabase")(rId, 1) / Session("GroupTot")
            '                Session("EC_TempDatabase")(rId, 2) = Session("EC_TempDatabase")(rId, 2) / Session("GroupTot")
            '                Session("EC_TempDatabase")(rId, 3) = Session("EC_TempDatabase")(rId, 3) / Session("GroupTot")
            '
            '                Session("EC_TempDatabase")(rId, 4) = Session("EC_TempDatabase")(rId, 4) / Session("GroupTot")
            '                Session("EC_TempDatabase")(rId, 5) = Session("EC_TempDatabase")(rId, 5) / Session("GroupTot")
            '                Session("EC_TempDatabase")(rId, 6) = Session("EC_TempDatabase")(rId, 6) / Session("GroupTot")
            '
            '            Next rId
            '        End If

            'jrs 2014/01/29
            If Session("bReadGrpMaxs") Then
                SetUpMaxsTime()
                Session("bReadGrpMaxs") = False
            End If

        End If

        'jrs 2014/02/05
        If Session("mnuUsePctPop") Then
            ConvertToPctPop()
        End If

        'FillDataGrid Tindex
        CompositeFillDataGrid() '1, 2, 3
        'end mti june, 2002


        '   Draw the graph
        '   --------------
        '    gphAgedst(Index).AutoInc = 1
        '    gphAgedst(Index).DataReset = 1

        ''If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then

        'jrs 2022/05/30
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            'jrs 2006/07/14
            Conn.Close()
        End If

        ''End If
        'JRS 2008/07/04
        lbldata.Text = "Human Capital Distribution for " +
           Session("EdCohort_regionname") + " in Year " + Session("EdCohort_tempyear").ToString + " [" + Session("EdCG_Casename") + "]" +
            " - " + "Graduates (mil.) by Educational Level (only) and Gender " 'gphAgedst(gphIndex).BottomTitle

ErrHandler01:
        Dim smsg As String = Err.Description
        Exit Sub
    End Sub

    Private Sub CompositeGraph_Draw_Compare(ByVal gphIndex As Integer)
        '--------------------------------------------------------------
        On Error GoTo ErrHandler02
        Select Case gphIndex
            Case 1
                Session("EdCohort_tempregion") = cmbReg1.SelectedIndex + 1
                Session("EdCohort_tempyear") = cmbYr1.SelectedValue
            Case 2
                Session("EdCohort_tempregion") = cmbReg2.SelectedIndex + 1
                Session("EdCohort_tempyear") = cmbYr2.SelectedValue
            Case 3
                Session("EdCohort_tempregion") = cmbReg3.SelectedIndex + 1
                Session("EdCohort_tempyear") = cmbYr3.SelectedValue
            Case 4
                Session("EdCohort_tempregion") = cmbReg4.SelectedIndex + 1
                Session("EdCohort_tempyear") = cmbYr4.SelectedValue
        End Select

        'jrs 2024/04/22
        vSQLiteCon.Close()
        vConnString = "DataSource=" + Session("dbsces")(gphIndex - 1) & ";"

        'jam 2025/05/31
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()


        'jam 2025/05/31 Changing connection mode
        vSQLiteCon = OpenJournalSQLite(vConnString)



        'jam 2024/10/14
        CompositeGraph_Draw(Session("dbsces")(gphIndex - 1), gphIndex, 4, 1, 2, 3, 4)




        'Load result to Compare array
        Dim i As Integer, j As Integer
        For i = 0 To NCohorts
            For j = 1 To 2
                Session("EC_TempPopDatabase_Comp")(i, j, gphIndex) = Session("EC_TempPopDatabase")(i, j)
            Next
            For j = 1 To 8
                Session("EC_TempDatabase_Comp")(i, j, gphIndex) = Session("EC_TempDatabase")(i, j)
            Next
        Next
ErrHandler02:
        Exit Sub

    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' For Different Types of Graphs
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="gphIndex"> Parameter for the graph type</param>


    Private Sub SeparateGraph_Draw_Compare(ByVal gphIndex As Integer)
        '--------------------------------------------------------------
        On Error GoTo ErrHandler02
        Select Case gphIndex
            Case 1
                Session("EdCohort_tempregion") = cmbReg1.SelectedIndex + 1
                Session("EdCohort_tempyear") = cmbYr1.SelectedValue
            Case 2
                Session("EdCohort_tempregion") = cmbReg2.SelectedIndex + 1
                Session("EdCohort_tempyear") = cmbYr2.SelectedValue
            Case 3
                Session("EdCohort_tempregion") = cmbReg3.SelectedIndex + 1
                Session("EdCohort_tempyear") = cmbYr3.SelectedValue
            Case 4
                Session("EdCohort_tempregion") = cmbReg4.SelectedIndex + 1
                Session("EdCohort_tempyear") = cmbYr4.SelectedValue
        End Select

        'jrs 2024/04/22
        vSQLiteCon.Close()
        vConnString = "DataSource=" + Session("dbsces")(gphIndex - 1) & ";"


        'jam 2025/05/31 Changing connection mode
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()


        'jam 2025/05/31 Changing connection mode
        vSQLiteCon = OpenJournalSQLite(vConnString)




        'jam 2024/10/14
        'SeparateGraph_Draw(gphIndex, 4, 1, 2, 3, 4)
        'jam 2024/10/27
        SeparateGraph_Draw(Session("EdCG_Index"), 4, 1, 2, 3, 4)




        'Load result to Compare array
        Dim i As Integer, j As Integer
        For i = 0 To NCohorts
            'jam 2024/10/27
            'For j = 1 To 2
            '    Session("EC_TempPopDatabase_Comp")(i, j, gphIndex) = Session("EC_TempPopDatabase")(i, j)
            'Next
            'For j = 1 To 8
            '    Session("EC_TempDatabase_Comp")(i, j, gphIndex) = Session("EC_TempDatabase")(i, j)
            'Next


            For j = 1 To 2
                Session("EC_TempPopDatabase_Comp")(i, j, gphIndex) = Session("EC_TempPopDatabase")(i, j)
            Next
            For j = 1 To 8
                Session("EC_TempDatabase_Comp")(i, j, gphIndex) = Session("EC_TempDatabase")(i, j)
            Next

        Next
ErrHandler02:
        Exit Sub

    End Sub
    Private Sub SeparateGraph_Draw(ByVal gphIndex As Integer, ByVal tblindex As Integer, ByVal findex1 As Integer,
    Optional ByVal findex2 As Integer = 0, Optional ByVal findex3 As Integer = 0,
    Optional ByVal findex4 As Integer = 0)
        '--------------------------------------------------------------
        Dim tbl2 As DataTable
        Dim DTsqlite As DataTable
        Dim TCohort As Integer
        '4,1,2,3
        'start mti june, 2002
        Dim Tindex As Integer
        'end mti june, 2002
        Dim rId As Integer

        On Error GoTo ErrHandler03

        ReDim Session("EC_TempPopDatabase")(NCohorts, 2)

        ReDim Session("EC_TempDatabase")(NCohorts, 8)

        Dim i2 As Integer
        DTsqlite = New DataTable

        '   Specify the graph control to be used
        '   ------------------------------------

        frameData.Visible = False
        'jrs 2008/06/25
        grdcohort.Visible = False
        lbldata.Visible = False

        Dim db2Path As String

        'JAM 2020-11-01
        'JAM 2020-02-29 Temp Definition, ask
        'db2Path = Session("PutFdrvRun") & "IFsPopWork.mdb"

        'Session("SQLiteDB") = "ifsPopWork.db"

        'Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

        'jrs 2006/07/14 create connecion
        Dim Conn As OleDb.OleDbConnection

        'JAM 2020-02-29 Temp Definition, ask
        'Conn = ADO_OleDB_CreateConnection(Session("db2Path"))
        'JAM 2020-11-01
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            db2Path = Session("PutFdrvRun") & "IFsPopWork.mdb"
            Conn = ADO_OleDB_CreateConnection(db2Path)
            Conn.Open()

        End If
        'jrs 2023/08/25
        'Dim IfsConn As OleDb.OleDbConnection
        Dim IFsConn As SQLiteConnection = New SQLiteConnection
        'jrs 2023/08/25 IFsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        IFsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"
        'tbl2 = New DataTable
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'JAM 2020-11-01
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            myCMD.Connection = Conn

        End If
        Dim strSQL As String
        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim bFirst As Boolean

        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        regDS = New DataSet
        'jrs 2023/08/24
        regDS.EnforceConstraints = False


        DTsqlite2 = New DataTable



        'take population
        '   Change the data source
        '   ----------------------
        'JRS 07/26/02 GROUP
        Tindex = 5 'TableName(5) =Population
        If Session("EdCohort_Grouping") = False Then
            'jrs 2006/07/17 SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
            ''rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
            i2 = 0

            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(5) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))


                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) = tbl2.Rows(i2).Item(FieldName(Tindex, 1))
                    'Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) = tbl2.Rows(i2).Item(FieldName(Tindex, 2))
                    'i2 = i2 + 1
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 1) = rdr.Item(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(rdr.Item("NCohorts"), 2) = rdr.Item(FieldName(Tindex, 2))
                Loop

                rdr.Close()
            Else 'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(5) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())

                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
                    Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))

                End While

                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()

            End If

        Else
            'jrs 2006/07/17 LoadCountries(Session("EdCohort_regionname"))   'do once!

            'jam 2025/05/31 Changing connection mode
            'IFsConn.Open()
            If IFsConn.State <> ConnectionState.Open Then
                IFsConn.Open()
            End If

            'JAM 2020-02-16
            ''If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then


            'jrs 2023/08/25 Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim rdGrpCmd As SQLiteCommand = IFsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SQLiteDataReader
            rdGrp = rdGrpCmd.ExecuteReader


            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            'jrs 2023/08/25 Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim rdRegCmd As SQLiteCommand = IFsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SQLiteDataReader
            rdReg = rdRegCmd.ExecuteReader
            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            'jrs 2023/08/25 Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim rdRegIdCmd As SQLiteCommand = IFsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SQLiteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader
            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            Dim popDA As OleDb.OleDbDataAdapter

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                popDA.Fill(regDS, "Population")

            Else

                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)

                'jrs 2023/08/25
                'dtnew = DTsqlite.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")
                'For Each row As DataRow In DTsqlite.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite)
            End If
            Dim PopDT As DataTable
            Dim myDA As OleDbDataAdapter = New OleDbDataAdapter()
            Dim myDS As DataSet = New DataSet()
            PopDT = New DataTable



            'jrs 2023/08/25 grpDA.Fill(regDS, "Groups")
            'jrs 2023/08/25 regDA.Fill(regDS, "Regions")
            'jrs 2023/08/25 regIDDA.Fill(regDS, "RegionIDs")
            'popDA.Fill(regDS, "Population")
            grpRegRel = regDS.Relations.Add("GrpRegion",
regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegionId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
regDS.Tables("Regions").Columns("RegionNo"))
            ''Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            ''                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
            ''                                 regDS.Tables("Population").Columns("Region"))


            Dim regPopRel As DataRelation

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
regDS.Tables("RegionIDs").Columns("RegionNum"),
regDS.Tables("Population").Columns("Region"))
            Else
                regPopRel = regDS.Relations.Add("RegPopulation",
regDS.Tables("RegionIDs").Columns("RegionNum"),
regDS.Tables(TableName(Tindex)).Columns(1))
            End If

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                bFirst = True
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    If bFirst Then
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = cRow.Item(FieldName(Tindex, 2))
                                        bFirst = False
                                    Else
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) + cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) + cRow.Item(FieldName(Tindex, 2))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next
            Else
                bFirst = True
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    If bFirst Then
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) = cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) = cRow.Item(FieldName(Tindex, 2))
                                        bFirst = False
                                    Else
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 1) + cRow.Item(FieldName(Tindex, 1))
                                        Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) = Session("EC_TempPopDatabase")(cRow.Item("CohortId"), 2) + cRow.Item(FieldName(Tindex, 2))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            '        Dim rId As Integer
            'For rId = 1 To Session("GroupTot")
            '    SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
            '    i2 = 0
            '    Do While Not tbl2.Rows.Count = i2
            '        If rId = 1 Then
            '            Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) = tbl2.Rows(i2).Item(FieldName(Tindex, 1))
            '            Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) = tbl2.Rows(i2).Item(FieldName(Tindex, 2))
            '        Else
            '            Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 1) + tbl2.Rows(i2).Item(FieldName(Tindex, 1))
            '            Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(tbl2.Rows(i2).Item("NCohorts"), 2) + tbl2.Rows(i2).Item(FieldName(Tindex, 2))
            '        End If
            '        i2 = i2 + 1
            '    Loop
            'Next rId

            ''Else 'JAM 2020-02-16

            ''    'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(Tindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)

            ''    Dim PopDT3 As DataTable
            ''    Dim vrow3 As Integer
            ''    Dim i As Integer


            ''    SetDataTablefromAccess(Session("IFs_MDB2Active"), "Select Case RegionNum From " & GroupIDNew & "," & Grouping & "," & RegionIDNew & " Where GroupNum = GroupNo And RegionNo = RegionNum", "", PopDT3, "")


            ''    'JAM 2019-12-23
            ''    vSQLiteCommand = vSQLiteCon.CreateCommand()
            ''    vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
            ''    vSQLiteReader = vSQLiteCommand.ExecuteReader()


            ''    bFirst = True
            ''    If PopDT3.Rows.Count > 0 Then
            ''        vrow3 = 1
            ''        While Not PopDT3.Rows.Count = vrow3
            ''            i = 1
            ''            While (vSQLiteReader.Read())
            ''                If PopDT3.Rows(vrow3).Item("GroupName") = Session("EdCohort_regionname") Then
            ''                    If vSQLiteReader("RegionId") = PopDT3.Rows(vrow3).Item("RegionNum") Then
            ''                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
            ''                        If bFirst Then
            ''                            Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
            ''                            Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))
            ''                            bFirst = False
            ''                        Else
            ''                            Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) = Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 1) + vSQLiteReader(FieldName(Tindex, 1))
            ''                            Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) = Session("EC_TempPopDatabase")(vSQLiteReader("CohortId"), 2) + vSQLiteReader(FieldName(Tindex, 2))
            ''                        End If

            ''                    End If
            ''                End If
            ''                i = i + 1
            ''            End While

            ''            vrow3 = vrow3 + 1

            ''        End While

            ''    End If




            ''    'JAM 2020-02-16
            ''    'regDS = New DataSet
            ''    'grpDA.Fill(regDS, "Groups")
            ''    'regDA.Fill(regDS, "Regions")
            ''    'regIDDA.Fill(regDS, "RegionIDs")
            ''    'popDA.Fill(regDS, "Population")
            ''    'grpRegRel = regDS.Relations.Add("GrpRegion",
            ''    '                                 regDS.Tables("Groups").Columns("GroupNum"),
            ''    '                                 regDS.Tables("Regions").Columns("GroupNo"))
            ''    'regRegIdRel = regDS.Relations.Add("RegRegionId",
            ''    '                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
            ''    '                                 regDS.Tables("Regions").Columns("RegionNo"))
            ''    'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            ''    '                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
            ''    '                                 regDS.Tables("Population").Columns("Region"))
            ''    'bFirst = True
            ''    'For Each pppRow In regDS.Tables("Groups").Rows 'JAM 2019-12-23 Ask Jose
            ''    '    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
            ''    '        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
            ''    '            For Each pRow In ppRow.GetParentRows(regRegIdRel)
            ''    '                'Console.WriteLine(pRow("CustomerID").ToString())
            ''    '                For Each cRow In pRow.GetChildRows(regPopRel)
            ''    '                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
            ''    '                    If bFirst Then
            ''    '                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = cRow.Item(FieldName(Tindex, 1))
            ''    '                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = cRow.Item(FieldName(Tindex, 2))
            ''    '                        bFirst = False
            ''    '                    Else
            ''    '                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 1) + cRow.Item(FieldName(Tindex, 1))
            ''    '                        Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) = Session("EC_TempPopDatabase")(cRow.Item("NCohorts"), 2) + cRow.Item(FieldName(Tindex, 2))
            ''    '                    End If
            ''    '                Next
            ''    '            Next
            ''    '        Next
            ''    '    End If
            ''    'Next




            ''End If


            'jrs 2014/01/29
            If Session("bReadGrpMaxs") Then
                SetUpMaxsTime()
                Session("bReadGrpMaxs") = False
            End If


        End If
        'End take population
        'MsgBox db2.Name

        '   Change the data source
        '   ----------------------
        'JRS 07/26/02 GROUP
        If Session("EdCohort_Grouping") = False Then
            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                'jrs 2006/07/17 SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))
                i2 = 0
                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    ''males - tertiary to No completed formal education
                    'TCohort = tbl2.Rows(i2).Item("NCohorts") '4=tertiary Male,3=Sec Male, 2=Pri Male, 1 = No Edn Male
                    'Session("EC_TempDatabase")(TCohort, 4) = tbl2.Rows(i2).Item(FieldName(findex4, 1))
                    'Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, tbl2.Rows(i2).Item(FieldName(findex3, 1)) - Session("EC_TempDatabase")(TCohort, 4))
                    'Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, tbl2.Rows(i2).Item(FieldName(findex2, 1)) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                    'Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))

                    ''females '8=tertiary Female,7=Sec Female, 6=Pri female, 5 = No Edn female
                    'Session("EC_TempDatabase")(TCohort, 8) = tbl2.Rows(i2).Item(FieldName(findex4, 2))
                    'Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, tbl2.Rows(i2).Item(FieldName(findex3, 2)) - Session("EC_TempDatabase")(TCohort, 8))
                    'Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, tbl2.Rows(i2).Item(FieldName(findex2, 2)) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                    'Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))

                    'i2 = i2 + 1

                    'males - tertiary to No completed formal education
                    TCohort = rdr.Item("NCohorts") '4=tertiary Male,3=Sec Male, 2=Pri Male, 1 = No Edn Male
                    If gphIndex < 5 Then
                        Session("EC_TempDatabase")(TCohort, 4) = rdr.Item(FieldName(findex4, 1))
                        Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, rdr.Item(FieldName(findex3, 1))) 'jrs 2011/09/14 - Session("EC_TempDatabase")(TCohort, 4))
                        Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, rdr.Item(FieldName(findex2, 1))) 'jrs 2011/09/14 - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                        Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - Session("EC_TempDatabase")(TCohort, 2)) 'jrs 2011/09/14 + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))

                        'females '8=tertiary Female,7=Sec Female, 6=Pri female, 5 = No Edn female
                        Session("EC_TempDatabase")(TCohort, 8) = rdr.Item(FieldName(findex4, 2))
                        Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, rdr.Item(FieldName(findex3, 2))) 'jrs 2011/09/14 - Session("EC_TempDatabase")(TCohort, 8))
                        Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, rdr.Item(FieldName(findex2, 2))) 'jrs 2011/09/14 - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                        Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - Session("EC_TempDatabase")(TCohort, 6)) 'jrs 2011/09/14 + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))
                    Else
                        Session("EC_TempDatabase")(TCohort, 4) = rdr.Item(FieldName(findex4, 1))
                        Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, rdr.Item(FieldName(findex3, 1)) - Session("EC_TempDatabase")(TCohort, 4))
                        Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, rdr.Item(FieldName(findex2, 1)) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                        Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))

                        'females '8=tertiary Female,7=Sec Female, 6=Pri female, 5 = No Edn female
                        Session("EC_TempDatabase")(TCohort, 8) = rdr.Item(FieldName(findex4, 2))
                        Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, rdr.Item(FieldName(findex3, 2)) - Session("EC_TempDatabase")(TCohort, 8))
                        Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, rdr.Item(FieldName(findex2, 2)) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                        Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))
                    End If
                Loop
                rdr.Close()

            Else

                'jrs 2006/07/17 SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
                ''rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("EdCohort_tempregion")) & " and Years = " & Str(Session("EdCohort_tempyear")))

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where RegionId = " & Str(Session("EdCohort_tempregion")) & " and TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                i2 = 0
                While (vSQLiteReader.Read()) 'Not tbl2.Rows.Count = i2

                    'males - tertiary to No completed formal education
                    TCohort = vSQLiteReader("CohortId") '4=tertiary Male,3=Sec Male, 2=Pri Male, 1 = No Edn Male
                    If gphIndex < 5 Then
                        Session("EC_TempDatabase")(TCohort, 4) = vSQLiteReader(FieldName(findex4, 1))
                        Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, vSQLiteReader(FieldName(findex3, 1))) 'jrs 2011/09/14 - Session("EC_TempDatabase")(TCohort, 4))
                        Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, vSQLiteReader(FieldName(findex2, 1))) 'jrs 2011/09/14 - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                        Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - Session("EC_TempDatabase")(TCohort, 2)) 'jrs 2011/09/14 + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))

                        'females '8=tertiary Female,7=Sec Female, 6=Pri female, 5 = No Edn female
                        Session("EC_TempDatabase")(TCohort, 8) = vSQLiteReader(FieldName(findex4, 2))
                        Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, vSQLiteReader(FieldName(findex3, 2))) 'jrs 2011/09/14 - Session("EC_TempDatabase")(TCohort, 8))
                        Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, vSQLiteReader(FieldName(findex2, 2))) 'jrs 2011/09/14 - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                        Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - Session("EC_TempDatabase")(TCohort, 6)) 'jrs 2011/09/14 + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))
                    Else
                        Session("EC_TempDatabase")(TCohort, 4) = vSQLiteReader(FieldName(findex4, 1))
                        Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, vSQLiteReader(FieldName(findex3, 1)) - Session("EC_TempDatabase")(TCohort, 4))
                        Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, vSQLiteReader(FieldName(findex2, 1)) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                        Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))

                        'females '8=tertiary Female,7=Sec Female, 6=Pri female, 5 = No Edn female
                        Session("EC_TempDatabase")(TCohort, 8) = vSQLiteReader(FieldName(findex4, 2))
                        Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, vSQLiteReader(FieldName(findex3, 2)) - Session("EC_TempDatabase")(TCohort, 8))
                        Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, vSQLiteReader(FieldName(findex2, 2)) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                        Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))
                    End If
                End While

                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()

            End If
        Else
            'jrs 2006/07/17 LoadCountries(Session("EdCohort_regionname"))


            'JAM 2020-02-16
            'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then
            ''Dim edDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(tblindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            ''edDA.Fill(regDS, "Education")

            Dim edDA As OleDb.OleDbDataAdapter

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                edDA = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(tblindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
                edDA.Fill(regDS, "Education")

            Else

                'JAM 2020-02-29
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite2.Load(vSQLiteReader)

                'jrs 2023/08/25
                'dtnew = DTsqlite2.Clone
                'Dim d2 As DataColumn = dtnew.Columns("RegionId")
                'd2.DataType = System.Type.GetType("System.Int32")
                ''at last used table's importrow method to fill the new datatable.  
                'For Each row As DataRow In DTsqlite2.Rows
                '    dtnew.ImportRow(row)
                'Next
                'regDS.Tables.Add(dtnew)
                regDS.Tables.Add(DTsqlite2)

            End If


            ''Dim regEdRel As DataRelation = regDS.Relations.Add("RegEducation",
            ''                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
            ''                                 regDS.Tables("Education").Columns("Region"))

            Dim regEdRel As DataRelation

            'JAM 2020-02-29
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regEdRel = regDS.Relations.Add("RegEducation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Education").Columns("Region"))
            Else
                'regEdRel = regDS.Relations.Add("RegEducation",
                '                             regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                             DTsqlite.Columns("RegionId"))

                regEdRel = regDS.Relations.Add("RegEducation",
                                             regDS.Tables("RegionIDs").Columns("RegionNum"),
                                             regDS.Tables(TableName(tblindex)).Columns(1))

            End If

            'JAM 2020-04-10
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                bFirst = True
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regEdRel)
                                    TCohort = cRow.Item("NCohorts")
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    If bFirst Then
                                        'males
                                        Session("EC_TempDatabase")(TCohort, 4) = cRow.Item(FieldName(findex4, 1))
                                        Session("EC_TempDatabase")(TCohort, 3) = cRow.Item(FieldName(findex3, 1))
                                        Session("EC_TempDatabase")(TCohort, 2) = cRow.Item(FieldName(findex2, 1))
                                        'females
                                        Session("EC_TempDatabase")(TCohort, 8) = cRow.Item(FieldName(findex4, 2))
                                        Session("EC_TempDatabase")(TCohort, 7) = cRow.Item(FieldName(findex3, 2))
                                        Session("EC_TempDatabase")(TCohort, 6) = cRow.Item(FieldName(findex2, 2))
                                        bFirst = False
                                    Else
                                        Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + cRow.Item(FieldName(findex4, 1))
                                        Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + cRow.Item(FieldName(findex3, 1))
                                        Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + cRow.Item(FieldName(findex2, 1))

                                        Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + cRow.Item(FieldName(findex4, 2))
                                        Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + cRow.Item(FieldName(findex3, 2))
                                        Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + cRow.Item(FieldName(findex2, 2))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            Else

                bFirst = True
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regEdRel)
                                    TCohort = cRow.Item("CohortId")
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    If bFirst Then
                                        'males
                                        Session("EC_TempDatabase")(TCohort, 4) = cRow.Item(FieldName(findex4, 1))
                                        Session("EC_TempDatabase")(TCohort, 3) = cRow.Item(FieldName(findex3, 1))
                                        Session("EC_TempDatabase")(TCohort, 2) = cRow.Item(FieldName(findex2, 1))
                                        'females
                                        Session("EC_TempDatabase")(TCohort, 8) = cRow.Item(FieldName(findex4, 2))
                                        Session("EC_TempDatabase")(TCohort, 7) = cRow.Item(FieldName(findex3, 2))
                                        Session("EC_TempDatabase")(TCohort, 6) = cRow.Item(FieldName(findex2, 2))
                                        bFirst = False
                                    Else
                                        Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + cRow.Item(FieldName(findex4, 1))
                                        Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + cRow.Item(FieldName(findex3, 1))
                                        Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + cRow.Item(FieldName(findex2, 1))

                                        Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + cRow.Item(FieldName(findex4, 2))
                                        Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + cRow.Item(FieldName(findex3, 2))
                                        Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + cRow.Item(FieldName(findex2, 2))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

            End If

            ''Else  'JAM 2020-02-16

            ''    Dim PopDT4 As DataTable
            ''    Dim vrow4 As Integer
            ''    Dim i4 As Integer

            ''    'JAM 2020-02-16
            ''    SetDataTablefromAccess(Session("IFs_MDB2Active"), "Select Case RegionNum From " & GroupIDNew & "," & Grouping & "," & RegionIDNew & " Where GroupNum = GroupNo And RegionNo = RegionNum", "", PopDT4, "")


            ''    'JAM 2020-02-16
            ''    vSQLiteCommand = vSQLiteCon.CreateCommand()
            ''    vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(tblindex) & " Where TimeId = " & Str(Session("EdCohort_tempyear"))
            ''    vSQLiteReader = vSQLiteCommand.ExecuteReader()

            ''    bFirst = True
            ''    If PopDT4.Rows.Count > 0 Then
            ''        vrow4 = 1
            ''        While Not PopDT4.Rows.Count = vrow4
            ''            i4 = 1
            ''            While (vSQLiteReader.Read())
            ''                If PopDT4.Rows(vrow4).Item("GroupName") = Session("EdCohort_regionname") Then
            ''                    If vSQLiteReader("RegionId") = PopDT4.Rows(vrow4).Item("RegionNum") Then
            ''                        TCohort = vSQLiteReader("CohortId")
            ''                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
            ''                        If bFirst Then
            ''                            'males
            ''                            Session("EC_TempDatabase")(TCohort, 4) = vSQLiteReader(FieldName(findex4, 1))
            ''                            Session("EC_TempDatabase")(TCohort, 3) = vSQLiteReader(FieldName(findex3, 1))
            ''                            Session("EC_TempDatabase")(TCohort, 2) = vSQLiteReader(FieldName(findex2, 1))
            ''                            'females
            ''                            Session("EC_TempDatabase")(TCohort, 8) = vSQLiteReader(FieldName(findex4, 2))
            ''                            Session("EC_TempDatabase")(TCohort, 7) = vSQLiteReader(FieldName(findex3, 2))
            ''                            Session("EC_TempDatabase")(TCohort, 6) = vSQLiteReader(FieldName(findex2, 2))
            ''                            bFirst = False
            ''                        Else
            ''                            Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + vSQLiteReader(FieldName(findex4, 1))
            ''                            Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + vSQLiteReader(FieldName(findex3, 1))
            ''                            Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + vSQLiteReader(FieldName(findex2, 1))

            ''                            Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + vSQLiteReader(FieldName(findex4, 2))
            ''                            Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + vSQLiteReader(FieldName(findex3, 2))
            ''                            Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + vSQLiteReader(FieldName(findex2, 2))
            ''                        End If

            ''                    End If
            ''                End If
            ''                i4 = i4 + 1
            ''            End While

            ''            vrow4 = vrow4 + 1

            ''        End While

            ''    End If
            ''    'JAM 2020-02-16
            ''    'Dim edDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & TableName(tblindex) & " Where Years = " & Str(Session("EdCohort_tempyear")), Conn)
            ''    '    edDA.Fill(regDS, "Education")
            ''    '    Dim regEdRel As DataRelation = regDS.Relations.Add("RegEducation",
            ''    '                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
            ''    '                                 regDS.Tables("Education").Columns("Region"))
            ''    '    bFirst = True
            ''    '    For Each pppRow In regDS.Tables("Groups").Rows
            ''    '        If pppRow.Item("GroupName") = Session("EdCohort_regionname") Then
            ''    '            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
            ''    '                For Each pRow In ppRow.GetParentRows(regRegIdRel)
            ''    '                    'Console.WriteLine(pRow("CustomerID").ToString())
            ''    '                    For Each cRow In pRow.GetChildRows(regEdRel)
            ''    '                        TCohort = cRow.Item("NCohorts")
            ''    '                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
            ''    '                        If bFirst Then
            ''    '                            'males
            ''    '                            Session("EC_TempDatabase")(TCohort, 4) = cRow.Item(FieldName(findex4, 1))
            ''    '                            Session("EC_TempDatabase")(TCohort, 3) = cRow.Item(FieldName(findex3, 1))
            ''    '                            Session("EC_TempDatabase")(TCohort, 2) = cRow.Item(FieldName(findex2, 1))
            ''    '                            'females
            ''    '                            Session("EC_TempDatabase")(TCohort, 8) = cRow.Item(FieldName(findex4, 2))
            ''    '                            Session("EC_TempDatabase")(TCohort, 7) = cRow.Item(FieldName(findex3, 2))
            ''    '                            Session("EC_TempDatabase")(TCohort, 6) = cRow.Item(FieldName(findex2, 2))
            ''    '                            bFirst = False
            ''    '                        Else
            ''    '                            Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + cRow.Item(FieldName(findex4, 1))
            ''    '                            Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + cRow.Item(FieldName(findex3, 1))
            ''    '                            Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + cRow.Item(FieldName(findex2, 1))

            ''    '                            Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + cRow.Item(FieldName(findex4, 2))
            ''    '                            Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + cRow.Item(FieldName(findex3, 2))
            ''    '                            Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + cRow.Item(FieldName(findex2, 2))
            ''    '                        End If
            ''    '                    Next
            ''    '                Next
            ''    '            Next
            ''    '        End If
            ''    '    Next




            ''End If


            IFsConn.Close()

            '        Dim rId As Integer
            'For rId = 1 To Session("GroupTot")
            '    SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(tblindex) & " Where Region = " & Str(Session("RegionIdGroup")(rId)) & " and Years = " & Str(Session("EdCohort_tempyear")), "", tbl2, "")
            '    i2 = 0
            '    Do While Not tbl2.Rows.Count = i2
            '        TCohort = tbl2.Rows(i2).Item("NCohorts")
            '        If rId = 1 Then
            '            'males
            '            Session("EC_TempDatabase")(TCohort, 4) = tbl2.Rows(i2).Item(FieldName(findex4, 1))
            '            Session("EC_TempDatabase")(TCohort, 3) = tbl2.Rows(i2).Item(FieldName(findex3, 1))
            '            Session("EC_TempDatabase")(TCohort, 2) = tbl2.Rows(i2).Item(FieldName(findex2, 1))

            '            'females
            '            Session("EC_TempDatabase")(TCohort, 8) = tbl2.Rows(i2).Item(FieldName(findex4, 2))
            '            Session("EC_TempDatabase")(TCohort, 7) = tbl2.Rows(i2).Item(FieldName(findex3, 2))
            '            Session("EC_TempDatabase")(TCohort, 6) = tbl2.Rows(i2).Item(FieldName(findex2, 2))

            '        Else

            '            Session("EC_TempDatabase")(TCohort, 4) = Session("EC_TempDatabase")(TCohort, 4) + tbl2.Rows(i2).Item(FieldName(findex4, 1))
            '            Session("EC_TempDatabase")(TCohort, 3) = Session("EC_TempDatabase")(TCohort, 3) + tbl2.Rows(i2).Item(FieldName(findex3, 1))
            '            Session("EC_TempDatabase")(TCohort, 2) = Session("EC_TempDatabase")(TCohort, 2) + tbl2.Rows(i2).Item(FieldName(findex2, 1))

            '            Session("EC_TempDatabase")(TCohort, 8) = Session("EC_TempDatabase")(TCohort, 8) + tbl2.Rows(i2).Item(FieldName(findex4, 2))
            '            Session("EC_TempDatabase")(TCohort, 7) = Session("EC_TempDatabase")(TCohort, 7) + tbl2.Rows(i2).Item(FieldName(findex3, 2))
            '            Session("EC_TempDatabase")(TCohort, 6) = Session("EC_TempDatabase")(TCohort, 6) + tbl2.Rows(i2).Item(FieldName(findex2, 2))

            '        End If

            '        i2 = i2 + 1
            '    Loop
            'Next rId

            If gphIndex < 5 Then
                For TCohort = 1 To 21
                    Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - Session("EC_TempDatabase")(TCohort, 2))
                    'females
                    Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - Session("EC_TempDatabase")(TCohort, 6))
                Next TCohort
            Else
                For TCohort = 1 To 21
                    'adjust group calc for sec=sec-ter, pri=pri-(sec+ter) etc.
                    'Session("EC_TempDatabase")(TCohort, 4) = tbl2.Rows(i2).Item(FieldName(findex4, 1))
                    Session("EC_TempDatabase")(TCohort, 3) = AMAX(0, Session("EC_TempDatabase")(TCohort, 3) - Session("EC_TempDatabase")(TCohort, 4))
                    Session("EC_TempDatabase")(TCohort, 2) = AMAX(0, Session("EC_TempDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3)))
                    Session("EC_TempDatabase")(TCohort, 1) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 1) - (Session("EC_TempDatabase")(TCohort, 4) + Session("EC_TempDatabase")(TCohort, 3) + Session("EC_TempDatabase")(TCohort, 2)))
                    'females
                    'Session("EC_TempDatabase")(TCohort, 8) = tbl2.Rows(i2).Item(FieldName(findex4, 2))
                    Session("EC_TempDatabase")(TCohort, 7) = AMAX(0, Session("EC_TempDatabase")(TCohort, 7) - Session("EC_TempDatabase")(TCohort, 8))
                    Session("EC_TempDatabase")(TCohort, 6) = AMAX(0, Session("EC_TempDatabase")(TCohort, 6) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7)))
                    Session("EC_TempDatabase")(TCohort, 5) = AMAX(0, Session("EC_TempPopDatabase")(TCohort, 2) - (Session("EC_TempDatabase")(TCohort, 8) + Session("EC_TempDatabase")(TCohort, 7) + Session("EC_TempDatabase")(TCohort, 6)))
                Next TCohort

            End If

        End If

        'jrs 2014/02/05
        If Session("mnuUsePctPop") Then
            ConvertToPctPop()
        End If

        SeparateFillDataGrid(gphIndex)

        'jrs 2023/08/25
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            'jrs 2006/07/17
            Conn.Close()
        End If

        'jrs 2008/07/04
        Dim GraphTitle As String = ""
        If gphIndex = 4 Then ' tertiary Education
            GraphTitle$ = "Tertiary educated"
        ElseIf gphIndex = 3 Then ' secondary Education
            GraphTitle$ = "People who completed at least Secondary"
        ElseIf gphIndex = 2 Then ' Primary Education
            GraphTitle$ = "People who completed at least Primary"
        ElseIf gphIndex = 1 Then ' No Education
            GraphTitle$ = "People with no completed formal education"
        ElseIf gphIndex = 5 Then 'jrs 2008/09/22
            GraphTitle$ = "Labor by levels of education (male)"
        ElseIf gphIndex = 6 Then 'jrs 2008/09/22
            GraphTitle$ = "Labor by levels of education (female)"
        ElseIf gphIndex = 7 Then 'jrs 2008/09/22
            GraphTitle$ = "Labor by levels of education (both genders)"
        End If



        If gphIndex < 5 Then
            lbldata.Text = GraphTitle & " by gender <br/>" & Session("EdCohort_regionname") &
            " in year " & Session("EdCohort_tempyear").ToString & ", (Million) [" & Session("EdCG_Casename") & "]"
        Else
            lbldata.Text = GraphTitle & "<br/>" & Session("EdCohort_regionname") &
            " in year " & Session("EdCohort_tempyear").ToString & ", (Million) [" & Session("EdCG_Casename") & "]"

            'jrs 2011/09/14
            lblJS.Text = Replace(lblJS.Text, "AnyChartDataforCohorts.aspx", "AnyChartDashboardLayout.aspx")
        End If
ErrHandler03:
        Exit Sub

    End Sub

    Private Function FindLastYear()
        'mti July 2006 - new routine

        On Error GoTo ErrHandler04

        Dim rsLY As DataTable
        'SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)
        'If Not rsLY.Rows.Count = 0 Then
        '    FindLastYear = rsLY.Rows(0).Item("MaxYear")
        'Else
        '    FindLastYear = 0
        'End If

        'JAM 2019-12-23
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)
            If Not rsLY.Rows.Count = 0 Then
                FindLastYear = rsLY.Rows(0).Item("MaxYear")
            Else
                FindLastYear = 0
            End If
        Else
            'SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT Max(TimeId) As MaxYear FROM " + TableName(1)
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            FindLastYear = 0
            While (vSQLiteReader.Read())
                FindLastYear = vSQLiteReader("MaxYear")

            End While


        End If

ErrHandler04:
        Exit Function
    End Function

    'jrs 2008/06/06
    Private Function FindFirstYear()

        On Error GoTo ErrHandler05
        'mti July 2006 - new routine
        Dim rsFY As DataTable
        'SetDataTablefromAccess(Session("db2Path"), "SELECT Min(Years) As MinYear FROM " + TableName(1), "", rsFY)
        'If Not rsFY.Rows.Count = 0 Then
        '    FindFirstYear = rsFY.Rows(0).Item("MinYear")
        'Else
        '    FindFirstYear = 0
        'End If


        'JAM 2019-12-23
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            SetDataTablefromAccess(Session("db2Path"), "SELECT Min(Years) As MinYear FROM " + TableName(1), "", rsFY)
            If Not rsFY.Rows.Count = 0 Then
                FindFirstYear = rsFY.Rows(0).Item("MinYear")
            Else
                FindFirstYear = 0
            End If
        Else
            'SetDataTablefromAccess(Session("db2Path"), "SELECT Min(Years) As MinYear FROM " + TableName(1), "", rsFY)


            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT Min(TimeId) As MinYear FROM " + TableName(1)
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            FindFirstYear = 0
            While (vSQLiteReader.Read())
                FindFirstYear = vSQLiteReader("MinYear")

            End While


        End If

ErrHandler05:
        Exit Function

    End Function

    'jrs 2008/06/05
    Private Sub LoadSceMenus(ByRef cswmGroup2 As Coalesys.WebMenu.Group)
        'Read all files with format ifsPopXXX.mdb located in runfiles
        Dim sfile As String
        sfile = Dir(Session("PutFdrvRun2") + "\IFsPop*.db")
        Dim cswmItem As Coalesys.WebMenu.Item

        'jrs 2014/01/31
        Dim bLoadSces As Boolean
        bLoadSces = (cmbSce1.Items.Count = 0)
        If bLoadSces Then
            cmbSce1.Items.Add("Base Case")
            cmbSce2.Items.Add("Base Case")
            cmbSce3.Items.Add("Base Case")
            cmbSce4.Items.Add("Base Case")
            cmbSce1.Items.Add("Work Case")
            cmbSce2.Items.Add("Work Case")
            cmbSce3.Items.Add("Work Case")
            cmbSce4.Items.Add("Work Case")
            cmbSce1.Items.Add("Historical Forecast")
            cmbSce2.Items.Add("Historical Forecast")
            cmbSce3.Items.Add("Historical Forecast")
            cmbSce4.Items.Add("Historical Forecast")
            cmbSce1.SelectedIndex = 0
            cmbSce2.SelectedIndex = 0
            cmbSce3.SelectedIndex = 0
            cmbSce4.SelectedIndex = 0
        End If

        Do While (sfile <> "")
            If UCase(sfile) <> "IFSPOPWORK.DB" And UCase(sfile) <> "IFSPOP.DB" And UCase(sfile) <> "IFSPOPHIST.DB" Then 'jrs 2008/06/16
                If Session("IFsRunFiles").item(Mid(Replace(Replace(sfile, ".db", ""), ".DB", ""), 7) & ".run.db").Visible Or StandAloneMode Then 'jrs 2024/03/20 added or Standalone
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = Mid(Replace(Replace(sfile, ".db", ""), ".DB", ""), 7)
                    cswmItem.IconHeight = 16
                    If Session("EdCG_Casename") = cswmItem.Caption Then
                        cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"
                    Else
                        cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
                    End If
                    'jrs 2014/02/12
                    If bLoadSces Then
                        cmbSce1.Items.Add(cswmItem.Caption)
                        cmbSce2.Items.Add(cswmItem.Caption)
                        cmbSce3.Items.Add(cswmItem.Caption)
                        cmbSce4.Items.Add(cswmItem.Caption)
                    End If
                End If
            End If
            sfile = Dir()
        Loop
    End Sub

    'jrs 2008/06/05
    Private Sub mnuSce_Click(ByVal sSceName As String, ByRef objSender As Coalesys.WebMenu.WebMenu)
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"

        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            If objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).Caption = sSceName Then
                objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"
                Case_of_Work = i + 2
            Else
                objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            End If
        Next

        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        CaseName = sSceName
        Session("EdCG_Casename") = CaseName

        Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"

        Session("PopCohort_year_run") = FindLastYear()
        Session("EdCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("EdCohort_tempyear"))
        Session("FirstYearSce") = FindFirstYear()

        If Session("PopCohort_year_run") <= FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearHistory
        ElseIf Session("EdCohort_tempyear") < FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearForecast
        End If

        If Session("EdCohort_tempyear") = Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("EdCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("EdCohort_GraphType")))

    End Sub

    'jrs 2008/06/05
    Private Sub mnuHistSce_Click(ByRef objSender As Coalesys.WebMenu.WebMenu)

        Dim i As Integer
        For i = 0 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
        Next

        CaseName = "Historical Base"
        Session("EdCG_Casename") = CaseName

        Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

        Session("PopCohort_year_run") = FindLastYear()
        Session("EdCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("EdCohort_tempyear"))
        Session("FirstYearSce") = FindFirstYear()

        If Session("PopCohort_year_run") <= FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearHistory
        ElseIf Session("EdCohort_tempyear") < FirstYearForecast Then
            Session("EdCohort_tempyear") = FirstYearForecast
        End If

        If Session("EdCohort_tempyear") = Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("EdCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("EdCohort_GraphType")))

    End Sub

    'jrs 2008/06/24
    Private Sub mnuExit_Click()
        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

        Session.Remove("EC_TempDatabase")
        Session.Remove("EC_TempPopDatabase")
        Session.Remove("POPCG_Case_of_Work")
        Session.Remove("TotLabCohort")
        Session.Remove("EC_TempPopDatabase_Comp")
        Session.Remove("EC_TempDatabase_Comp")
        Session.Remove("EC_Reg")
        Session.Remove("EC_Yr")

        'JAM 2020-10-10
        Session.Remove("IsGroup")

        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If

        'JAM 2020-09-28
        'Support.FormManager.Show(Me, Session("DisplayParent"))

        'jam 2026/02/27
        'Session.Remove("EdnXAxisLabel")
        'Session.Remove("EdnYAxisLabel")
        Session.Remove("EdnTitleLabel")
        Session.Remove("EdnSubTitleLabel")
        Session.Remove("EdnLabelChanged")
        Session.Remove("EdnLabelsParent")



        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        'vfriendlyurl = FriendlyUrl.Resolve("~/frm_MainMenu.aspx")

        ''Change REgion
        'vurl = FriendlyUrl.Href("~/frm_MainMenu")

        'Response.Redirect(vurl)


        'jam 2021/05/04
        If Session("DisplayParent") = Nothing Or Session("DisplayParent") = "frm_MainMenu.aspx" Then
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_MainMenu.aspx")

            vurl = FriendlyUrl.Href("~/frm_MainMenu")
        Else
            vfriendlyurl = FriendlyUrl.Resolve("~/" & Session("DisplayParent"))
            vurl = FriendlyUrl.Href("~/" & Session("DisplayParent"))
        End If

        Response.Redirect(vurl)



    End Sub

    Private Sub CompositeFillDataGrid()
        '--------------------------------------------------------------
        '4,1,2,3
        'cdv 2021/01/14 ****START**** Modifiers based on bMaleFirst to change grid order
        Dim GenderIndex1 As Integer
        Dim GenderIndex2 As Integer

        If bMaleFirst Then
            GenderIndex1 = 0
            GenderIndex2 = 0
        Else
            GenderIndex1 = 4
            GenderIndex2 = -4
        End If


        Dim i As Integer
        grdcohort.TotalRows = NCohorts + 1 'jrs 2026/02/02 22
        grdcohort.RowCount = NCohorts + 1 'jrs 2026/02/02 22

        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        For i = 1 To 13
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            If i > 1 And i < 11 Then
                myCol.Width = 150 '1300
            Else
                myCol.Width = 125 '1100
            End If

            grdcohort.Columns.Add(myCol)
        Next



        Dim l As Integer

        For l = NCohorts To 1 Step -1

            If l = NCohorts Then
                grdcohort(0, l) = NAges.ToString & "+" 'jrs 2026/02/02
            Else
                grdcohort(0, l) = Str$(l * 5 - 5) + "-" + Str$(l * 5 - 1)
            End If

            'male
            grdcohort(1 + GenderIndex1, l) = Format(Session("EC_TempDatabase")(l%, 1), "0.0000")

            'Primary
            grdcohort(2 + GenderIndex1, l) = Format(Session("EC_TempDatabase")(l%, 2), "0.0000")

            'secondary
            grdcohort(3 + GenderIndex1, l) = Format(Session("EC_TempDatabase")(l%, 3), "0.0000")

            grdcohort(4 + GenderIndex1, l) = Format(Session("EC_TempDatabase")(l%, 4), "0.0000")

            'female
            grdcohort(5 + GenderIndex2, l) = Format(Session("EC_TempDatabase")(l%, 5), "0.0000")

            'Primary
            grdcohort(6 + GenderIndex2, l) = Format(Session("EC_TempDatabase")(l%, 6), "0.0000")

            'secondary
            grdcohort(7 + GenderIndex2, l) = Format(Session("EC_TempDatabase")(l%, 7), "0.0000")

            'tertiary
            grdcohort(8 + GenderIndex2, l) = Format(Session("EC_TempDatabase")(l%, 8), "0.0000")

            'total
            'no
            grdcohort(9, l) = Format(Session("EC_TempDatabase")(l%, 1) + Session("EC_TempDatabase")(l%, 5), "0.0000")

            'Primary
            grdcohort(10, l) = Format(Session("EC_TempDatabase")(l%, 2) + Session("EC_TempDatabase")(l%, 6), "0.0000")

            'secondary
            grdcohort(11, l) = Format(Session("EC_TempDatabase")(l%, 3) + Session("EC_TempDatabase")(l%, 7), "0.0000")

            'tertiary
            grdcohort(12, l) = Format(Session("EC_TempDatabase")(l%, 4) + Session("EC_TempDatabase")(l%, 8), "0.0000")



        Next l%

        grdcohort(0, 0) = "Age Cohort"
        grdcohort(1 + GenderIndex1, 0) = "No Edn Male"
        grdcohort(2 + GenderIndex1, 0) = "Pri Male"
        grdcohort(3 + GenderIndex1, 0) = "Sec Male"
        grdcohort(4 + GenderIndex1, 0) = "Ter Male"
        grdcohort(5 + GenderIndex2, 0) = "No Edn Female"
        grdcohort(6 + GenderIndex2, 0) = "Pri Female"
        grdcohort(7 + GenderIndex2, 0) = "Sec Female"
        grdcohort(8 + GenderIndex2, 0) = "Ter Female"
        'cdv 2021/01/14 ****END****
        grdcohort(9, 0) = "No Edn Total"
        grdcohort(10, 0) = "Pri Total"
        grdcohort(11, 0) = "Sec Total"
        grdcohort(12, 0) = "Ter Total"

    End Sub

    Private Sub SeparateFillDataGrid(ByVal gphIndex As Integer)
        '--------------------------------------------------------------
        On Error GoTo ErrHandler06
        Dim TotLabCohort(4)
        Dim iNCohorts As Integer
        Dim ExcludeBottom As Integer
        Dim TotCols As Integer

        'cdv 2021/01/14 ****START**** Gender Indexs to change data order
        Dim GenderIndex1 As Integer
        Dim GenderIndex2 As Integer

        If bMaleFirst Then
            GenderIndex1 = 0
            GenderIndex2 = 4
        Else
            GenderIndex1 = 4
            GenderIndex2 = 0
        End If
        'cdv 2021/01/14 ****END**** 
        If gphIndex > 4 Then
            iNCohorts = 14
            ExcludeBottom = 3
            TotCols = 5
        Else
            iNCohorts = NCohorts
            ExcludeBottom = 0
            TotCols = 3
        End If

        Dim i As Integer
        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        grdcohort.TotalRows = iNCohorts - ExcludeBottom + 1
        grdcohort.RowCount = iNCohorts - ExcludeBottom + 1

        myCol = New TMSWebControls.Column
        myCol.WidthType = TMSWebControls.WType.Absolute
        myCol.Width = 100 '1100
        grdcohort.Columns.Add(myCol)

        For i = 1 To TotCols
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            myCol.Width = 150
            grdcohort.Columns.Add(myCol)
        Next

        Dim gndrAdd As Integer, gndrPop As Integer
        Select Case gphIndex
            Case 5
                gndrAdd% = 0 'male
                gndrPop% = 1
            Case 6
                gndrAdd% = 4 'female
                gndrPop% = 2
            Case 7
                gndrAdd% = 4 'female
                gndrPop% = 2
        End Select

        Dim l As Integer
        For l = iNCohorts To ExcludeBottom + 1 Step -1

            If l = NCohorts Then
                grdcohort(0, l) = "100+"
            Else
                grdcohort(0, l - ExcludeBottom) = CStr(l * 5 - 5) & "-" & CStr(l * 5 - 1)
            End If

            If gphIndex < 5 Then
                'male
                grdcohort(1, l) = Format(Session("EC_TempDatabase")(l, gphIndex + GenderIndex1), "0.0000")

                'female
                grdcohort(2, l) = Format(Session("EC_TempDatabase")(l, gphIndex + GenderIndex2), "0.0000")
                'cdv 2021/01/14 ****END****
                'total
                grdcohort(3, l) = Format(Session("EC_TempDatabase")(l, gphIndex) + Session("EC_TempDatabase")(l, gphIndex + 4), "0.0000")
            Else
                Dim Tot As Single, SetNo As Integer
                Tot = 0
                For SetNo% = 1 To 4
                    Select Case gphIndex
                        Case 5, 6
                            grdcohort(SetNo, l - ExcludeBottom) = Format(Session("EC_TempDatabase")(l%, SetNo% + gndrAdd%), "0.0000")
                            TotLabCohort(SetNo%) = TotLabCohort(SetNo%) + Session("EC_TempDatabase")(l%, SetNo% + gndrAdd%)
                        Case 7
                            grdcohort(SetNo, l - ExcludeBottom) = Format(Session("EC_TempDatabase")(l%, SetNo%) + Session("EC_TempDatabase")(l%, SetNo% + gndrAdd%), "0.0000")
                            TotLabCohort(SetNo%) = TotLabCohort(SetNo%) + Session("EC_TempDatabase")(l%, SetNo%) + Session("EC_TempDatabase")(l%, SetNo% + gndrAdd%)
                    End Select
                Next

                'total
                Select Case gphIndex
                    Case 5, 6
                        grdcohort(5, l - ExcludeBottom) = Format(Session("EC_TempPopDatabase")(l%, gndrPop%), "0.0000")
                    Case 7
                        grdcohort(5, l - ExcludeBottom) = Format(Session("EC_TempPopDatabase")(l%, 1) + Session("EC_TempPopDatabase")(l%, 2), "0.0000")
                End Select

            End If
        Next l

        grdcohort(0, 0) = "Age Cohort"

        If gphIndex < 5 Then
            'cdv 2021/01/14 ****START**** Change labels to match order
            If bMaleFirst Then
                grdcohort(1, 0) = "Male"
                grdcohort(2, 0) = "Female"
                grdcohort(3, 0) = "Total"
            Else
                grdcohort(1, 0) = "Female"
                grdcohort(2, 0) = "Male"
                grdcohort(3, 0) = "Total"
            End If
            'cdv 2021/01/14 ****END****
        Else
            grdcohort(1, 0) = "No Education"
            grdcohort(2, 0) = "Primary Only"
            grdcohort(3, 0) = "Upto Secondary"
            grdcohort(4, 0) = "Tertiary Completed"
            grdcohort(5, 0) = "Total"

            iNCohorts = iNCohorts + 1

            grdcohort.TotalRows = iNCohorts - ExcludeBottom + 1
            grdcohort.RowCount = iNCohorts - ExcludeBottom + 1

            Dim TotLab As Single = 0
            'total total
            grdcohort(0, iNCohorts - ExcludeBottom) = "Total"
            'no ed
            grdcohort(1, iNCohorts - ExcludeBottom) = Format(TotLabCohort(1), "0.0000")
            TotLab = TotLab + TotLabCohort(1)
            'pri
            grdcohort(2, iNCohorts - ExcludeBottom) = Format(TotLabCohort(2), "0.0000")
            TotLab = TotLab + TotLabCohort(2)
            'sec
            grdcohort(3, iNCohorts - ExcludeBottom) = Format(TotLabCohort(3), "0.0000")
            TotLab = TotLab + TotLabCohort(3)
            'ter
            grdcohort(4, iNCohorts - ExcludeBottom) = Format(TotLabCohort(4), "0.0000")
            TotLab = TotLab + TotLabCohort(4)

            grdcohort(5, iNCohorts - ExcludeBottom) = Format(TotLab, "0.0000")
            'jrs 2008/09/25
            Session("TotLabCohort") = TotLabCohort
        End If

ErrHandler06:
        Exit Sub

    End Sub

    Private Sub mnuLaborMale_Click()
        Session("EdCohort_GraphType") = 5
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub mnuLaborFemale_Click()
        Session("EdCohort_GraphType") = 6
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    Private Sub mnuLaborBoth_Click()
        Session("EdCohort_GraphType") = 7
        Graph_Draw(Session("EdCohort_GraphType"))
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()

        On Error GoTo ErrHandler07
        Dim iNCohortsWD As Integer = NCohorts
        'iNCohortsWD = FindLastCohortWithData(Session("EC_TempDataBase_Comp"), 6, gphIdx)

        'jam 2026/07/21 Keeping Max scale only for cohorts with data 
        If Session("bCompare") AndAlso MaxCohortGroup > 0 Then
            iNCohortsWD = MaxCohortGroup
        End If
        '''''''''''''''''''

        Dim iNCohorts As Integer
        iNCohorts = iNCohortsWD 'jrs2013/08/16 NCohorts

        Dim Ages() As String
        ReDim Ages(iNCohorts)
        If iNCohorts = NCohorts Then
            Ages(iNCohorts) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
        Else
            Ages(iNCohorts) = CStr(iNCohorts * 5 - 5) & "-" & CStr(iNCohorts * 5 - 1)
        End If

        Dim l As Integer
        For l = 2 To iNCohorts - 1
            Ages(l) = Str(l * 5 - 5) & "-" & Str(l * 5 - 1)
        Next l
        Ages(0) = "Infants"
        Ages(1) = "0-4"

        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "            var mycategories = [" & vbCr
        For l = 1 To iNCohorts
            If l > 1 Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & "'" & Ages(l) & "'"
        Next
        sJscript2 = sJscript2 & "];" & vbCr

        sJscript2 = sJscript2 & "            var myPositions = [" & vbCr
        For l = 0 To iNCohorts
            If l > 0 Then
                sJscript2 = sJscript2 & ", "
            End If
            sJscript2 = sJscript2 & l.ToString
        Next
        sJscript2 = sJscript2 & "];" & vbCr

        If Session("bCompare") Then
            For l = 1 To 4
                sJscript2 = sJscript2 & PackJScritpBuilderContainter(l.ToString, iNCohortsWD)
            Next
        Else
            sJscript2 = sJscript2 & PackJScritpBuilderContainter("", iNCohortsWD)
        End If

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2
ErrHandler07:
        Exit Sub

    End Sub

    Private Function PackJScritpBuilderContainter(ByVal sContainterIdx As String, ByVal iNCohortsWD As Integer) As String
        Dim sJscript2 As String = ""
        On Error GoTo ErrHandler08
        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        'jam 2026/01/22 Reducing fonts for Comparison
        If Not Session("bCompare") Then
            'jam 2025/12/05 Increasing font size for legends
            sJscript2 = sJscript2 & "legend: {" & vbCr
            sJscript2 = sJscript2 & "itemStyle: {" & vbCr
            'sJscript2 = sJscript2 & "fontFamily: 'Arial, sans-serif'," & vbCr
            sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            'sJscript2 = sJscript2 & "color: '#333333' " & vbCr
            sJscript2 = sJscript2 & "}, " & vbCr
            sJscript2 = sJscript2 & "} " & vbCr
            '''''''''''''''''''''''
        Else
            'jam 2025/12/05 Increasing font size for legends
            sJscript2 = sJscript2 & "legend: {" & vbCr
            'jrs 2026/04/30
            sJscript2 = sJscript2 & "enabled:        false," & vbCr
            If Session("EdCG_Index") = 0 Or Session("EdCG_Index") > 4 Then
                imgEdCats.ImageUrl = "~/Images/EducationPyramidCats.png"
                imgEdCats.Width = 750
            Else
                imgEdCats.ImageUrl = "~/Images/FemalesAndMales.png"
                imgEdCats.Width = 300
            End If

            sJscript2 = sJscript2 & "itemStyle: {" & vbCr
            'sJscript2 = sJscript2 & "fontFamily: 'Arial, sans-serif'," & vbCr
            'jrs 2026/04/23 Reducing font size for legends in comparison mode
            'sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
            sJscript2 = sJscript2 & "fontSize: '12px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            'sJscript2 = sJscript2 & "color: '#333333' " & vbCr
            sJscript2 = sJscript2 & "}, " & vbCr
            sJscript2 = sJscript2 & "} " & vbCr
            '''''''''''''''''''''''
        End If




        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container" & sContainterIdx & "').highcharts({" & vbCr

        'jam 2025/03/01 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "chart: {" & vbCr
        'sJscript2 = sJscript2 & "events: {" & vbCr
        'sJscript2 = sJscript2 & "load: function () {" & vbCr
        'sJscript2 = sJscript2 & "this.xAxis[0].tickInterval = 1;" & vbCr
        'sJscript2 = sJscript2 & "this.redraw();" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "type: 'bar'," & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        sJscript2 = sJscript2 & "events: {"
        sJscript2 = sJscript2 & "load: function(event) {"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.visibility = 'hidden';"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.height = 0;"
        sJscript2 = sJscript2 & "}}"
        sJscript2 = sJscript2 & "}," & vbCr

        Dim gphIdx As Integer = 0
        If sContainterIdx > "" Then
            gphIdx = CInt(sContainterIdx)
        End If

        Dim Index As Short
        Dim graphtitle As String = ""
        Dim regionname As String
        Dim CaseName As String
        Dim TempYear As Short
        Dim subtitle As String = ""
        Index = Session("EdCG_Index")
        regionname = Session("EdCG_Regionname")
        CaseName = Session("EdCG_Casename")
        TempYear = Session("EdCG_tempyear")
        'jrs 2014/02/21
        Dim iNCohorts As Integer = iNCohortsWD
        If Session("bShow100") = False Then
            iNCohorts = NCohorts - 1
        End If

        Select Case Index
            Case 0
                graphtitle = "Human Capital Distribution"
            Case 1
                graphtitle = "People with no completed formal education"
            Case 2
                graphtitle = "People who completed at least Primary"
            Case 3
                graphtitle = "People who completed at least Secondary"
            Case 4
                graphtitle = "Tertiary educated"
            Case 5
                graphtitle = "Labor by levels of education (male)"
            Case 6
                graphtitle = "Labor by levels of education (female)"
            Case 7
                graphtitle = "Labor by levels of education (both genders)"
        End Select

        If Index > 1 And Index < 5 Then
            graphtitle = graphtitle & " by gender"
        End If

        'jrs 2014/02/11
        If Session("bCompare") Then
            If gphIdx < 5 Then
                graphtitle = ""
                subtitle = Session("EC_Reg")(gphIdx) & " in year " & Str(Session("EC_Yr")(gphIdx)) & " [" + Session("CaseNames")(gphIdx - 1) + "]"
            Else
                'graphtitle = graphtitle & " [" + CaseName$ + "]"
                subtitle = ""
            End If
        Else
            subtitle = "for " & regionname & " in year " & Str(TempYear) & " [" + CaseName$ + "] "
        End If

        'jam 2026/02/27 Changing Titles labels
        If Session("EdnLabelChanged") And Not Session("bCompare") Then
            If graphtitle <> Session("EdnTitleLabel") Then

                graphtitle = Session("EdnTitleLabel")
            End If

            If subtitle <> Session("EdnSubTitleLabel") Then
                subtitle = Session("EdnSubTitleLabel")
            End If
        Else
            Session("EdnTitleLabel") = graphtitle
            Session("EdnSubTitleLabel") = subtitle
        End If




        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        'jrs 2026/04/23 Reducing font size for title in comparison mode
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        Else
            sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
        End If
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr


        'jam 2026/01/22 Reducing fonts for Comparison
        If Not Session("bCompare") Then
            'jam 2025/12/05  Increasing font size for legends
            sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '24px'}," & vbCr
            ''''''''''''''''''''''''''''
        Else
            'jrs 2026/04/23 sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '20px'}," & vbCr
            sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '16px'}," & vbCr
        End If


        If (Index = 0) Then
            sJscript2 = sJscript2 & "x: 25 //center" & vbCr
        Else
            sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "subtitle: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        'jrs 2026/04/23 Reducing fonts for Comparison
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        Else
            sJscript2 = sJscript2 & "fontSize: '14px'," & vbCr
        End If
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       """ & subtitle & """," & vbCr
        If (Index = 0) Then
            sJscript2 = sJscript2 & "x: 25 //center" & vbCr
        Else
            sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If (Index = 0) Then
            sJscript2 = sJscript2 & "legend: {" & vbCr
            sJscript2 = sJscript2 & "x:       60" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
            'cdv 2021/01/14 Reverse legend if female first
        Else
            If bMaleFirst = False Then
                sJscript2 = sJscript2 & "legend: {" & vbCr
                sJscript2 = sJscript2 & "reversed:       true" & vbCr
                sJscript2 = sJscript2 & "}," & vbCr
            End If
        End If

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        If Index > 4 Then
            sJscript2 = sJscript2 & "max: " & iNCohorts & "," & vbCr
        End If

        'jam 2026/01/22 Reducing fonts for Comparison
        If Not Session("bCompare") Then
            'jam 2025/12/05 Increasing font for x axis
            sJscript2 = sJscript2 & "labels: {" & vbCr
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "color: 'black'," & vbCr
            sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
            sJscript2 = sJscript2 & "}}," & vbCr
        Else

            sJscript2 = sJscript2 & "labels: {" & vbCr
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "color: 'black'," & vbCr
            sJscript2 = sJscript2 & "fontSize: '16px'" & vbCr
            sJscript2 = sJscript2 & "}}," & vbCr
        End If



        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Age Categories'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/05 Increasing font for x axis
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        'jam 2026/01/22 Reducing fonts for Comparison
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        Else
            sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
        End If


        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "categories: mycategories, " & vbCr
        sJscript2 = sJscript2 & "reversed: false, " & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        'jrs 2025/07/21
        If gphIdx = 0 Then
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        'jrs 2026/04/23
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "step: 1" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr
        'jrs 2026/04/23
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "tickPositions: myPositions" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxis As String = ""
        If Session("mnuUsePctPop") = False Then
            sAxis = "Millions"
        Else
            sAxis = "Percent of Total Population"
        End If

        'jam 2026/02/27 Changing XAxis Label labels
        'If Session("EdnLabelChanged") And Not Session("bCompare") Then
        '    If Session("EdnXAxisLabel") <> sAxis Then

        '        sAxis = Session("EdnXAxisLabel")
        '    End If


        'End If


        Dim sMax As String = "", sMin As String = ""
        If Session("mnuFixCtSc") = True Or Session("mnuFixCteScTime") = True Or Session("mnuFixCtScMan") = True Then
            sMax = "max: " & Session("MaxScale")(Index)
            If (Index = 0 Or Index > 3) Then
                sMin = "min: " & CStr(-1 * Session("MaxScale")(Index))
            Else
                sMin = "min: 0"
            End If
        End If

        sJscript2 = sJscript2 & "yAxis: {" & vbCr

        'jam 2025/12/05 Increasing font for y axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        'jam 2026/01/22 Reducing fonts for Comparison
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        Else
            'jrs 2026/04/23 sJscript2 = sJscript2 & "fontSize: '18px'" & vbCr
            sJscript2 = sJscript2 & "fontSize: '16px'" & vbCr
        End If

        sJscript2 = sJscript2 & "}}," & vbCr


        sJscript2 = sJscript2 & "reversedStacks: false," & vbCr
        If sMax > "" Then
            sJscript2 = sJscript2 & sMax & ", " & vbCr
            sJscript2 = sJscript2 & sMin & ", " & vbCr
        End If
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/05 Increasing font for y axis
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        'jam 2026/01/22 Reducing fonts for Comparison
        If Not Session("bCompare") Then
            sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        Else
            sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
        End If
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        If Index = 0 Then
            sJscript2 = sJscript2 & "x: 0," & vbCr
        Else
            sJscript2 = sJscript2 & "x: -50," & vbCr
        End If
        sJscript2 = sJscript2 & "text:       '" & sAxis & "'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        'jrs 2025/07/21
        If gphIdx = 0 Then
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return Math.abs(this.value);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Index = 0 Then
            sJscript2 = sJscript2 & "plotOptions: {" & vbCr
            sJscript2 = sJscript2 & "series: {" & vbCr
            sJscript2 = sJscript2 & "stacking:   'normal'," & vbCr

            sJscript2 = sJscript2 & "events: {" & vbCr
            sJscript2 = sJscript2 & "legendItemClick: function () {" & vbCr
            sJscript2 = sJscript2 & "return false;" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr

            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<b>' + this.series.name + ', age ' + this.point.category + '</b><br/>' +"
        sJscript2 = sJscript2 & "'Population: ' + Highcharts.numberFormat(Math.abs(this.point.y), 2);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        If Session("bCompare") Then

            'jam 2026/07/21 Only keeping cohorts with data 
            'jrs 2013/08/16
            'iNCohortsWD = FindLastCohortWithData(Session("EC_TempDataBase_Comp"), 8, gphIdx)

            If MaxCohortGroup > 0 Then
                iNCohortsWD = MaxCohortGroup
            End If

            ''''''''''''''''''''''''''''''''''''''''''



            'jam 2024/10/18 commented
            'If gphIdx > 0 And gphIdx < 5 Then
            '    'Males 1 to 5, Females 6 to 10
            '    For i = 1 To 10
            '        If i > 1 Then
            '            sJscript2 = sJscript2 & ","
            '        End If
            '        sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("EC_TempDataBase_Comp"), True, 0, Nothing, gphIdx, iNCohortsWD, True)
            '    Next
            'End If


            'jam 2024/10/18


            If Index = 0 Then
                'Males 1 to 5, Females 6 to 10
                For i = 1 To 10
                    If i > 1 Then
                        sJscript2 = sJscript2 & ","
                    End If

                    sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("EC_TempDataBase_Comp"), True, Index, Nothing, gphIdx, iNCohortsWD, True)
                Next

            ElseIf Index < 5 Then
                'Males
                sJscript2 = sJscript2 & PlotColToJSSeries(Index + 1, Session("EC_TempDataBase_Comp"), True, Index, Nothing, gphIdx, iNCohortsWD, True)
                'Females
                sJscript2 = sJscript2 & "," & PlotColToJSSeries(Index + 6, Session("EC_TempDataBase_Comp"), True, Index, Nothing, gphIdx, iNCohortsWD, True)
            Else
                Dim iStart As Integer, iEnd As Integer, bFirst As Boolean = True
                If Index = 5 Then
                    iStart = 5
                    iEnd = 2
                Else
                    iStart = 10
                    iEnd = 7
                End If
                If Index = 7 Then
                    'jam 2024/10/27
                    For i = 5 To 2 Step -1
                        'For i = 4 To 2 Step -1
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript2 = sJscript2 & ","
                        End If
                        'jam 2024/10/27
                        If Session("bCompare") = False Then

                            sJscript2 = sJscript2 & PlotColSUMToJSSeries(i, Session("EC_TempDataBase_Comp"))

                        Else
                            sJscript2 = sJscript2 & PlotColSUMToJSSeriesComp(i, Session("EC_TempDataBase_Comp"), gphIdx)
                        End If
                    Next
                Else
                    For i = iStart To iEnd Step -1
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript2 = sJscript2 & ","
                        End If
                        sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("EC_TempDataBase_Comp"), True, Index, Nothing, gphIdx, iNCohortsWD, True)
                    Next
                End If
            End If

            '''''''''''''''''




        Else
            'jrs 2013/08/16
            iNCohortsWD = FindLastCohortWithData(Session("EC_TempDataBase"), 8, 0)

            If Index = 0 Then
                'Males 1 to 5, Females 6 to 10
                For i = 1 To 10
                    If i > 1 Then
                        sJscript2 = sJscript2 & ","
                    End If

                    sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("EC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
                Next

            ElseIf Index < 5 Then
                'Males
                sJscript2 = sJscript2 & PlotColToJSSeries(Index + 1, Session("EC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
                'Females
                sJscript2 = sJscript2 & "," & PlotColToJSSeries(Index + 6, Session("EC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
            Else
                Dim iStart As Integer, iEnd As Integer, bFirst As Boolean = True
                If Index = 5 Then
                    iStart = 5
                    iEnd = 2
                Else
                    iStart = 10
                    iEnd = 7
                End If
                If Index = 7 Then
                    For i = 5 To 2 Step -1
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript2 = sJscript2 & ","
                        End If
                        'jam 2024/10/27
                        ' sJscript2 = sJscript2 & PlotColSUMToJSSeries(i, Session("EC_TempDataBase"))
                        If Session("bCompare") = False Then

                            sJscript2 = sJscript2 & PlotColSUMToJSSeries(i, Session("EC_TempDataBase"))

                        Else
                            sJscript2 = sJscript2 & PlotColSUMToJSSeries(i, Session("EC_TempDataBase_Comp"))
                        End If
                    Next
                Else
                    For i = iStart To iEnd Step -1
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript2 = sJscript2 & ","
                        End If
                        sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("EC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
                    Next
                End If
            End If
        End If

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        'jam 2023/01/11
        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".css({"
        sJscript2 = sJscript2 & "cursor: 'pointer'"
        sJscript2 = sJscript2 & "})"
        sJscript2 = sJscript2 & ".add();" & vbCr

        sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95 - 50;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 50;" & vbCr

        If Session("bCompare") Then
            sJscript2 = sJscript2 & "myWidth = myWidth * 0.49;" & vbCr
            sJscript2 = sJscript2 & "myHeight = myHeight * 0.49;" & vbCr
        ElseIf Index > 4 Then
            sJscript2 = sJscript2 & "myWidth = myWidth * 0.6;" & vbCr
        End If

        If Index = 0 Then
            Dim tmalemax As Single, tfemalemax As Single, TVal As Single
            tmalemax = 0
            Dim k As Integer = 0, iMed As Integer = 0, iEnd As Integer = 0

            If Session("bCompare") = False Then
                For l% = NCohorts To 0 Step -1
                    TVal = Session("EC_TempDataBase")(l%, 1) + Session("EC_TempDataBase")(l%, 2) + Session("EC_TempDataBase")(l%, 3) + Session("EC_TempDataBase")(l%, 4)
                    If TVal > tmalemax Then
                        tmalemax = TVal
                    End If
                Next l%
                tmalemax = Math.Round(tmalemax, 2)

                tfemalemax = 0
                For l% = NCohorts To 0 Step -1
                    TVal = Session("EC_TempDataBase")(l%, 5) + Session("EC_TempDataBase")(l%, 6) + Session("EC_TempDataBase")(l%, 7) + Session("EC_TempDataBase")(l%, 8)
                    If TVal > tfemalemax Then
                        tfemalemax = TVal
                    End If
                Next l%
            Else
                For l% = NCohorts To 0 Step -1
                    TVal = Session("EC_TempDataBase_Comp")(l%, 1, gphIdx) + Session("EC_TempDataBase_Comp")(l%, 2, gphIdx) + Session("EC_TempDataBase_Comp")(l%, 3, gphIdx) + Session("EC_TempDataBase_Comp")(l%, 4, gphIdx)
                    If TVal > tmalemax Then
                        tmalemax = TVal
                    End If
                Next l%
                tmalemax = Math.Round(tmalemax, 2)

                tfemalemax = 0
                For l% = NCohorts To 0 Step -1
                    TVal = Session("EC_TempDataBase_Comp")(l%, 5, gphIdx) + Session("EC_TempDataBase_Comp")(l%, 6, gphIdx) + Session("EC_TempDataBase_Comp")(l%, 7, gphIdx) + Session("EC_TempDataBase_Comp")(l%, 8, gphIdx)
                    If TVal > tfemalemax Then
                        tfemalemax = TVal
                    End If
                Next l%
            End If

            sJscript2 = sJscript2 & "var point = chart.series[0].data[1], text = chart.renderer.text(" & vbCr
            'cdv 2021/01/14 ****START**** Change Label to match gender order
            If bMaleFirst Then
                sJscript2 = sJscript2 & "'Males Max (" & CStr(Math.Round(tmalemax, 1)) & ")', " & vbCr



            Else
                sJscript2 = sJscript2 & "'Females Max (" & CStr(Math.Round(tfemalemax, 1)) & ")'," & vbCr
            End If
            'sJscript2 = sJscript2 & "2.25 * chart.plotLeft + 10," & vbCr
            'sJscript2 = sJscript2 & "chart.plotTop - 10"
            'sJscript2 = sJscript2 & ").attr({" & vbCr
            'sJscript2 = sJscript2 & "zIndex: 5" & vbCr
            'sJscript2 = sJscript2 & "}).add();" & vbCr

            'jrs 2026/04/23
            If Session("bCompare") = False Then
                'jam 2025/12/06 Improving font size
                sJscript2 = sJscript2 & "2.25 * chart.plotLeft + 10," & vbCr
                sJscript2 = sJscript2 & "chart.plotTop - 10"
                sJscript2 = sJscript2 & ").attr({" & vbCr
                sJscript2 = sJscript2 & "zIndex: 5" & vbCr
                sJscript2 = sJscript2 & "}) " & vbCr

                sJscript2 = sJscript2 & ".css({" & vbCr
                sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
                sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
                sJscript2 = sJscript2 & "})" & vbCr
                sJscript2 = sJscript2 & ".add();" & vbCr
            Else
                'jam 2025/12/06 Improving font size
                sJscript2 = sJscript2 & "2.25 * chart.plotLeft," & vbCr
                sJscript2 = sJscript2 & "chart.plotTop"
                sJscript2 = sJscript2 & ").attr({" & vbCr
                sJscript2 = sJscript2 & "zIndex: 5" & vbCr
                sJscript2 = sJscript2 & "}) " & vbCr

                sJscript2 = sJscript2 & ".css({" & vbCr
                sJscript2 = sJscript2 & "fontSize: '12px'," & vbCr
                sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
                sJscript2 = sJscript2 & "})" & vbCr
                sJscript2 = sJscript2 & ".add();" & vbCr
            End If


            sJscript2 = sJscript2 & "var point2 = chart.series[" & iMed.ToString & "].data[1], text2 = chart.renderer.text(" & vbCr
            If bMaleFirst Then
                sJscript2 = sJscript2 & "'Females Max (" & CStr(Math.Round(tfemalemax, 1)) & ")'," & vbCr
            Else
                sJscript2 = sJscript2 & "'Males Max (" & CStr(Math.Round(tmalemax, 1)) & ")', " & vbCr
            End If
            'cdv 2021/01/14 ****END****
            'sJscript2 = sJscript2 & "myWidth - 200," & vbCr
            'sJscript2 = sJscript2 & "chart.plotTop - 10"
            'sJscript2 = sJscript2 & ").attr({" & vbCr
            'sJscript2 = sJscript2 & "zIndex: 5" & vbCr
            'sJscript2 = sJscript2 & "}).add();" & vbCr

            'jrs 2026/04/23
            If Session("bCompare") = False Then
                'jam 2025/12/06 Improving font size
                sJscript2 = sJscript2 & "myWidth - 200," & vbCr
                sJscript2 = sJscript2 & "chart.plotTop - 10"
                sJscript2 = sJscript2 & ").attr({" & vbCr
                sJscript2 = sJscript2 & "zIndex: 5" & vbCr
                sJscript2 = sJscript2 & "})" & vbCr

                sJscript2 = sJscript2 & ".css({" & vbCr
                sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
                sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
                sJscript2 = sJscript2 & "})" & vbCr
                sJscript2 = sJscript2 & ".add();" & vbCr
            Else
                'jam 2025/12/06 Improving font size
                sJscript2 = sJscript2 & "myWidth - 198," & vbCr
                sJscript2 = sJscript2 & "chart.plotTop"
                sJscript2 = sJscript2 & ").attr({" & vbCr
                sJscript2 = sJscript2 & "zIndex: 5" & vbCr
                sJscript2 = sJscript2 & "})" & vbCr

                sJscript2 = sJscript2 & ".css({" & vbCr
                sJscript2 = sJscript2 & "fontSize: '12px'," & vbCr
                sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
                sJscript2 = sJscript2 & "})" & vbCr
                sJscript2 = sJscript2 & ".add();" & vbCr
            End If





        End If

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr

        If Index > 4 Then
            'jam 2024/10/27
            'sJscript2 = sJscript2 & PackJScritpBuilderContainterColumn()
            If Index <> 4 And Not Session("bCompare") Then

                sJscript2 = sJscript2 & PackJScritpBuilderContainterColumn()
            End If

        End If

        PackJScritpBuilderContainter = sJscript2
ErrHandler08:
        Exit Function
    End Function

    Private Function PackJScritpBuilderContainterColumn() As String
        Dim sJscript2 As String = ""

        On Error GoTo ErrHandler09

        sJscript2 = sJscript2 & "$('#containerColumn').highcharts({" & vbCr
        sJscript2 = sJscript2 & "chart: {" & vbCr
        'sJscript2 = sJscript2 & "events: {" & vbCr
        'sJscript2 = sJscript2 & "load: function () {" & vbCr
        'sJscript2 = sJscript2 & "this.xAxis[0].tickInterval = 1;" & vbCr
        'sJscript2 = sJscript2 & "this.redraw();" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "type: 'column'," & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        Dim Index As Short
        Dim graphtitle As String = ""
        Dim regionname As String
        Dim CaseName As String
        Dim TempYear As Short
        Dim subtitle As String = ""
        Index = Session("EdCG_Index")
        regionname = Session("EdCG_Regionname")
        CaseName = Session("EdCG_Casename")
        TempYear = Session("EdCG_tempyear")

        Select Case Index
            Case 5
                graphtitle = "Total Labor (Male)"
            Case 6
                graphtitle = "Total Labor (Female)"
            Case 7
                graphtitle = "Total Labor (both genders)"
        End Select

        If Session("mnuUsePctPop") Then
            subtitle = "for " & regionname & " in year " & Str(TempYear) & " [" + CaseName$ + "] Percent of Total Population"
        Else
            subtitle = "for " & regionname & " in year " & Str(TempYear) & " [" + CaseName$ + "] Millions"
        End If

        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr
        sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "subtitle: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       """ & subtitle & """," & vbCr
        sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "enabled: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "tickLength: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxis As String = ""
        sAxis = ""


        Dim sMax As String = "", sMin As String = ""
        If Session("mnuFixCtSc") = True Or Session("mnuFixCteScTime") = True Or Session("mnuFixCtScMan") = True Then
            sMax = "max: " & Session("MaxScale")(Index)
            If (Index = 0 Or Index > 3) Then
                sMin = "min: " & CStr(-1 * Session("MaxScale")(Index))
            Else
                sMin = "min: 0"
            End If
        End If

        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        If sMax > "" Then
            sJscript2 = sJscript2 & sMax & ", " & vbCr
            sJscript2 = sJscript2 & sMin & ", " & vbCr
        End If
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        If Index = 0 Then
            sJscript2 = sJscript2 & "x: 0," & vbCr
        Else
            sJscript2 = sJscript2 & "x: -50," & vbCr
        End If
        sJscript2 = sJscript2 & "text:       '" & sAxis & "'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return Math.abs(this.value);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<b>' + this.series.name + ', age ' + this.point.category + '</b><br/>' +"
        sJscript2 = sJscript2 & "'Population: ' + Highcharts.numberFormat(Math.abs(this.point.y), 2);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim bFirst As Boolean = True
        For i = 1 To 4
            If bFirst Then
                bFirst = False
            Else
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & PlotColToJSSeriesColumn(i, Session("TotLabCohort"))
        Next

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        'jam 2024/08/06 Bug fix related to GET parameters
        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".css({"
        sJscript2 = sJscript2 & "cursor: 'pointer'"
        sJscript2 = sJscript2 & "})"
        sJscript2 = sJscript2 & ".add();" & vbCr

        sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "myWidth = (myWidth * 0.95 - 50) * 0.38;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 50;" & vbCr


        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr

        PackJScritpBuilderContainterColumn = sJscript2

ErrHandler09:
        Exit Function

    End Function

    'jrs 2012/08/17
    Private Sub mnuCompare_Click()
        If Session("bCompare") = False Then
            wmMain.Groups("Change Display").ShowMenuBarItem = False
            wmMain.Groups("Time").ShowMenuBarItem = False
            wmMain.Groups("mnuGroups").ShowMenuBarItem = False
        End If

        'jam 2024/10/16
        'EdAllyrs()
        Session("EdCG_Index") = Session("EdCohort_GraphType")
        ''''''''''''''''''''

        'Clear Region combos
        cmbReg1.Items.Clear()
        cmbReg2.Items.Clear()
        cmbReg3.Items.Clear()
        cmbReg4.Items.Clear()

        If Session("EdCohort_Grouping") = True Then
            For l = 1 To GCount
                cmbReg1.Items.Add(New ListItem(GroupNames(l)))
                cmbReg2.Items.Add(New ListItem(GroupNames(l)))
                cmbReg3.Items.Add(New ListItem(GroupNames(l)))
                cmbReg4.Items.Add(New ListItem(GroupNames(l)))
            Next l

            cmbReg1.SelectedValue = Session("EdCohort_regionname")
            cmbReg2.SelectedValue = Session("EdCohort_regionname")
            cmbReg3.SelectedValue = Session("EdCohort_regionname")
            cmbReg4.SelectedValue = Session("EdCohort_regionname")

        Else
            For l = 1 To NR
                cmbReg1.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg2.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg3.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg4.Items.Add(New ListItem(GRPS(1, l)))
            Next l

            cmbReg1.SelectedIndex = cntrylist.SelectedIndex
            cmbReg2.SelectedIndex = cntrylist.SelectedIndex
            cmbReg3.SelectedIndex = cntrylist.SelectedIndex
            cmbReg4.SelectedIndex = cntrylist.SelectedIndex

        End If
        'cdv 2021/05/19
        While Int(cmbYr2.Text) Mod 5 <> 0
            cmbYr2.SelectedIndex = cmbYr2.SelectedIndex - 1
            cmbYr3.SelectedIndex = cmbYr3.SelectedIndex - 1
            cmbYr4.SelectedIndex = cmbYr4.SelectedIndex - 1
        End While

        'jam 2024/09/28
        If Session("bCompare") Then
            'Turn off bCompare flag
            Session("bCompare") = False
            pnlCompare.Visible = False
        Else

            'Turn on bCompare flag
            Session("bCompare") = True
            'to Show comparison graphs (4) and hide single graph
            'Show Comparisson panel with 4 sets of region, year textbox and 1 refresh button
            pnlCompare.Visible = True
        End If

        cmdRefreshComp_Click(Me, New EventArgs)
    End Sub

    'jrs 2012/08/17
    Protected Sub cmdRefreshComp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRefreshComp.Click

        On Error GoTo ErrHandler10
        ReDim Session("EC_TempPopDatabase_Comp")(NCohorts, 2, 4)
        ReDim Session("EC_TempDatabase_Comp")(NCohorts, 8, 4)
        ReDim Session("EC_Reg")(4)
        ReDim Session("EC_Yr")(4)

        Session("CaseNames")(0) = cmbSce1.SelectedValue
        Session("CaseNames")(1) = cmbSce2.SelectedValue
        Session("CaseNames")(2) = cmbSce3.SelectedValue
        Session("CaseNames")(3) = cmbSce4.SelectedValue

        Select Case cmbSce1.SelectedIndex
            Case 0
                'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.mdb"


                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.db"

                        Session("SQLiteDB") = "IFsPop.db"

                        Session("dbsces")(0) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If


            Case 1
                'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.mdb"


                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.db"

                        Session("SQLiteDB") = "IFsPopWork.db"

                        Session("dbsces")(0) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If



            Case 2
                'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.mdb"


                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.db"

                        Session("SQLiteDB") = "IFsPopHist.db"

                        Session("dbsces")(0) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If


            Case Else
                'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".mdb"

                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".mdb"
                    Else
                        'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".db"

                        Session("SQLiteDB") = "IFsPop" & cmbSce1.SelectedValue & ".db"

                        Session("dbsces")(0) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If


        End Select
        Select Case cmbSce2.SelectedIndex
            Case 0
                'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.mdb"


                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.db"


                        Session("SQLiteDB") = "IFsPop.db"

                        Session("dbsces")(1) = Session("PutFdrvRun") & Session("SQLiteDB")
                    End If
                End If

            Case 1
                'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.mdb"


                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.db"

                        Session("SQLiteDB") = "IFsPopWork.db"

                        Session("dbsces")(1) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

            Case 2
                'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.db"

                        Session("SQLiteDB") = "IFsPopHist.db"

                        Session("dbsces")(1) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

            Case Else
                'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".mdb"


                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".mdb"
                    Else
                        'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".db"

                        Session("SQLiteDB") = "IFsPop" & cmbSce2.SelectedValue & ".db"

                        Session("dbsces")(1) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If


        End Select
        Select Case cmbSce3.SelectedIndex
            Case 0
                'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.mdb"


                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.db"


                        Session("SQLiteDB") = "IFsPop.db"

                        Session("dbsces")(2) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

            Case 1
                'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.db"

                        Session("SQLiteDB") = "IFsPopWork.db"

                        Session("dbsces")(2) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If

            Case 2
                'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        ' Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.db"

                        Session("SQLiteDB") = "IFsPopHist.db"

                        Session("dbsces")(2) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If

            Case Else
                Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".mdb"

                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".mdb"
                    Else
                        'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".db"

                        Session("SQLiteDB") = "IFsPop" & cmbSce3.SelectedValue & ".db"

                        Session("dbsces")(2) = Session("PutFdrvRun") & Session("SQLiteDB")



                    End If
                End If


        End Select
        Select Case cmbSce4.SelectedIndex
            Case 0
                'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.mdb"


                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.db"


                        Session("SQLiteDB") = "IFsPop.db"

                        Session("dbsces")(3) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

            Case 1
                'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.db"

                        Session("SQLiteDB") = "IFsPopWork.db"

                        Session("dbsces")(3) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If

            Case 2
                'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.db"


                        Session("SQLiteDB") = "IFsPopHist.db"

                        Session("dbsces")(3) = Session("PutFdrvRun") & Session("SQLiteDB")



                    End If
                End If

            Case Else
                'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".mdb"

                'JAM 2020-02-12
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".mdb"
                    Else
                        'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".db"

                        Session("SQLiteDB") = "IFsPop" & cmbSce4.SelectedValue & ".db"

                        Session("dbsces")(3) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If


        End Select

        'jrs 2024/04/24 it was using the wrong flag
        If Session("EdCohort_Grouping") = True Then
            If Session("PopCohort_regionname") <> cmbReg1.SelectedValue Then
                Session("GroupTot") = Nothing
            End If
        End If

        If Session("mnuFixCteScTime") Then
            If Session("EdCohort_Grouping") = False Then
                Session("EdCohort_tempregion") = cmbReg1.SelectedIndex + 1
            Else
                Session("EdCohort_regionname") = cmbReg1.SelectedValue
            End If
            SetUpMaxsTime()
        End If

        Dim i As Integer
        Dim a As String

        'jam 2024/10/15
        'For i = 1 To 4
        '    CompositeGraph_Draw_Compare(i)
        'Next

        'jam 2024/10/15
        Select Case Session("EdCohort_GraphType")
            Case 0
                For i = 1 To 4
                    CompositeGraph_Draw_Compare(i)
                Next
            Case 1, 2, 3, 4, 5, 6, 7
                For i = 1 To 4
                    SeparateGraph_Draw_Compare(i)
                Next

        End Select
        ''''''''''''''''''''''''''''''




        'jrs 2024/04/22
        vSQLiteCon.Close()
        If Not Session("SQLiteDB") Is Nothing Then
            vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";"
        Else
            vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPop.db;"
        End If

        'jam 2025/05/31 Changing connection mode
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()


        'jam 2025/05/31 Changing connection mode
        vSQLiteCon = OpenJournalSQLite(vConnString)




        Session("EC_Reg")(1) = cmbReg1.SelectedValue
        Session("EC_Reg")(2) = cmbReg2.SelectedValue
        Session("EC_Reg")(3) = cmbReg3.SelectedValue
        Session("EC_Reg")(4) = cmbReg4.SelectedValue

        Session("EC_Yr")(1) = cmbYr1.SelectedValue
        Session("EC_Yr")(2) = cmbYr2.SelectedValue
        Session("EC_Yr")(3) = cmbYr3.SelectedValue
        Session("EC_Yr")(4) = cmbYr4.SelectedValue

        lbldata.Text = "Human Capital Distribution for " + _
   Session("EdCohort_regionname") + " in Year " + Session("EdCohort_tempyear").ToString + " [" + Session("CaseNames")(3) + "]" + _
    " - " + "Graduates (mil.) by Educational Level (only) and Gender " 'gphAgedst(gphIndex).BottomTitle


        'jam 2026/07/21 Only showing scales with data for comparison mode
        Dim iNCohortsWD As Integer = NCohorts

        If Session("bCompare") AndAlso Not Session("EC_TempDataBase_Comp") Is Nothing Then
            Dim iMax As Integer = 0
            Dim g As Integer, cohL As Integer, col As Integer
            For g = 1 To 4
                For cohL = NCohorts To 1 Step -1
                    Dim bexists As Boolean = False
                    For col = 1 To 8
                        If Not IsDBNull(Session("EC_TempDataBase_Comp")(cohL, col, g)) Then
                            If Session("EC_TempDataBase_Comp")(cohL, col, g) > 0.00005 Then
                                bexists = True
                                Exit For
                            End If
                        End If
                    Next
                    If bexists Then
                        If cohL > iMax Then iMax = cohL
                        Exit For
                    End If
                Next
            Next
            If iMax > 0 Then MaxCohortGroup = iMax
        End If
        ''''''''''''''''''''''''''

        PackJScritpBuilder()

ErrHandler10:
        Exit Sub

    End Sub

    'jrs 2014/01/28
    Private Sub mnuFixCtSc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCtSc") = False
        Else
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            Session("mnuFixCtSc") = True
        End If

        If Session("mnuFixCtSc") = True Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCteScTime") = False
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCtScMan") = False

            SetUpMaxs()
        End If
        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
        Else
            Graph_Draw(Session("EdCohort_GraphType"))
        End If
    End Sub

    'jrs 2014/01/29
    Private Sub SetUpMaxs()

        On Error GoTo ErrHandler11

        If Session("mnuUsePctPop") = True Then
            Dim i As Integer
            Session("MaxScale")(0) = 25
            For i = 1 To 7
                Session("MaxScale")(i) = 13
            Next

            Exit Sub
        End If

        Dim Margin As Single
        Margin = 1.2   '-> 20%

        'All Types of Mortality
        '======================
        Dim sql As String
        Dim rs As DataTable
        Dim MaxScale As Single = 0

        'jam 2024/12/12
        Dim vSQLiteCon3 As SQLiteConnection
        Dim vSQLiteCommand3 As SQLiteCommand
        Dim vSQLiteReader3 As SQLiteDataReader
        Dim vConnString3 As String



        'jam 2024/12/12
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then



                If Not Session("SQLiteDB") Is Nothing Then
                    vConnString3 = "DataSource=" + Session("db2Path") & ";"
                Else
                    vConnString3 = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                End If

                'vSQLiteCon3 = New SQLiteConnection(vConnString3)
                'vSQLiteCon3.Open()


                'jam 2025/05/31 Changing connection mode
                vSQLiteCon3 = OpenJournalSQLite(vConnString3)



            End If
        End If




        If Session("EdCohort_Grouping") = False Then

            'JAM 2019-12-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                sql = "SELECT MAX(" & FieldName(5, 1) & "), MAX(" & FieldName(5, 2) & ") " &
                "FROM " & TableName(5)

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                    MaxScale = rs.Rows(0).Item(0)
                Else
                    MaxScale = rs.Rows(0).Item(1)
                End If


            Else

                sql = "SELECT MAX(" & FieldName(5, 1) & ") as MaxValue, MAX(" & FieldName(5, 2) & ") as MaxValue2 " &
                     "FROM " & TableName(5)


                vSQLiteCommand3 = vSQLiteCon3.CreateCommand()
                vSQLiteCommand3.CommandText = sql
                vSQLiteReader3 = vSQLiteCommand3.ExecuteReader()

                While (vSQLiteReader3.Read())

                    If vSQLiteReader3("MaxValue") > vSQLiteReader3("MaxValue2") Then
                        'jam 2024/12/12
                        'Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader3("MaxValue")

                        MaxScale = vSQLiteReader3("MaxValue")

                    Else
                        'jam 2024/12/12
                        'Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader3("MaxValue2")

                        MaxScale = vSQLiteReader3("MaxValue2")
                    End If

                End While
            End If
        Else
            'JAM 2019-12-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                sql = "SELECT SUM(" & FieldName(5, 1) & "), SUM(" & FieldName(5, 2) & ") " &
                "FROM " & TableName(5) & " WHERE Years = " & CStr(Session("FirstYearSce"))
                'sql = sql & " AND NCohorts = 11" 'jrs looking at the world in 2100 this is the age with more people
                sql = sql & " AND NCohorts = 1" 'jam 2025/01/09 Defining new Max Cohort because the first cohort is commonly the maximum

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                    MaxScale = rs.Rows(0).Item(0)
                Else
                    MaxScale = rs.Rows(0).Item(1)
                End If
            Else
                sql = "SELECT SUM(" & FieldName(5, 1) & ") as MaxValue, SUM(" & FieldName(5, 2) & ") as MaxValue2 " &
                "FROM " & TableName(5) & " WHERE TimeId = " & CStr(Session("FirstYearSce"))
                'sql = sql & " AND CohortId = 11" 'jam 2025/01/09 Defining new Max Cohort because the first cohort is commonly the maximum
                sql = sql & " AND CohortId = 1"

                vSQLiteCommand3 = vSQLiteCon3.CreateCommand()
                vSQLiteCommand3.CommandText = sql
                vSQLiteReader3 = vSQLiteCommand3.ExecuteReader()

                While (vSQLiteReader3.Read())

                    If vSQLiteReader3("MaxValue") > vSQLiteReader3("MaxValue2") Then
                        MaxScale = vSQLiteReader3("MaxValue")
                    Else
                        MaxScale = vSQLiteReader3("MaxValue2")
                    End If

                End While






            End If
        End If

        For i = 0 To 6
            Session("MaxScale")(i) = MaxScale * Margin
        Next
        Session("MaxScale")(7) = MaxScale * Margin * 2

        'JAM 2019-12-22
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                vSQLiteCon3.Close()
            End If
        End If

ErrHandler11:
        Exit Sub

    End Sub

    'jrs 2014/02/14
    Private Sub mnuFixCteScTime_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCteScTime") = False
        Else
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            Session("mnuFixCteScTime") = True
        End If

        If Session("mnuFixCteScTime") = True Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCtSc") = False
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCtScMan") = False

            SetUpMaxsTime()
        End If
        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
        Else
            Graph_Draw(Session("EdCohort_GraphType"))
        End If
    End Sub

    'jrs 2014/01/28
    'Maxs across Time, specific per Region
    Private Sub SetUpMaxsTime()

        On Error GoTo ErrHandler12

        If Session("mnuUsePctPop") = True Then
            Dim i As Integer
            Session("MaxScale")(0) = 25
            For i = 1 To 7
                Session("MaxScale")(i) = 13
            Next

            Exit Sub
        End If

        Dim Margin As Single
        Margin = 1.2   '-> 20%

        'All Types of Mortality
        '======================
        Dim sql As String
        Dim rs As DataTable
        Dim MaxScale As Single = 0

        If Session("EdCohort_Grouping") = False Then

            'JAM 2019-12-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sql = "SELECT MAX(" & FieldName(5, 1) & "), MAX(" & FieldName(5, 2) & ") " &
                    "FROM " & TableName(5) & " WHERE Region "
                sql = sql & "= " & Str(Session("EdCohort_tempregion"))
                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                    MaxScale = rs.Rows(0).Item(0)
                Else
                    MaxScale = rs.Rows(0).Item(1)
                End If
            Else
                sql = "SELECT MAX(" & FieldName(5, 1) & ") as MaxValue, MAX(" & FieldName(5, 2) & ") as MaxValue2" &
                    "FROM " & TableName(5) & " WHERE RegionId "
                sql = sql & "= " & Str(Session("EdCohort_tempregion"))

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                While (vSQLiteReader.Read())

                    If vSQLiteReader("MaxValue") > vSQLiteReader("MaxValue2") Then
                        MaxScale = vSQLiteReader("MaxValue")
                    Else
                        MaxScale = vSQLiteReader("MaxValue2")
                    End If

                End While

            End If
        Else
            If Session("GroupTot") Is Nothing Then
                LoadCountries(Session("EdCohort_regionname"))
            End If
            Dim sRegs As String = ""
            Dim rId As Integer
            Dim yr As Integer


            For rId = 1 To Session("GroupTot")
                If sRegs = "" Then
                    sRegs = Str(Session("RegionIdGroup")(rId))
                Else
                    sRegs = sRegs & "," & Str(Session("RegionIdGroup")(rId))
                End If
            Next rId

            'jam 2025/01/14, recommended by jrs
            yr = Math.Min(Integer.Parse(Session("PopCohort_year_run")), 2050)



            'JAM 2019-12-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sql = "SELECT SUM(" & FieldName(5, 1) & "), SUM(" & FieldName(5, 2) & ") " &
                "FROM " & TableName(5) & " as m WHERE m.Years = " & CStr(Session("PopCohort_year_run"))
                'sql = sql & " AND m.NCohorts = 11" 'jrs looking at the world in 2100 this is the age with more people
                sql = sql & " AND m.NCohorts = 1" 'jam 2025/01/13 Defining new Max Cohort because the first cohort is commonly the maximum
                sql = sql & " AND m.Region IN (" & sRegs & ")"

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                    MaxScale = rs.Rows(0).Item(0)
                Else
                    MaxScale = rs.Rows(0).Item(1)
                End If

            Else
                sql = "SELECT SUM(" & FieldName(5, 1) & ") as MaxValue, SUM(" & FieldName(5, 2) & ") as MaxValue2 " &
                     "FROM " & TableName(5) & " as m WHERE m.TimeId = " & CStr(yr) 'jam 2025/01/14, recommended by jrs
                ' "FROM " & TableName(5) & " as m WHERE m.TimeId = " & CStr(Session("PopCohort_year_run"))
                'sql = sql & " AND m.CohortId = 11" 'jrs looking at the world in 2100 this is the age with more people
                sql = sql & " AND m.CohortId = 1" 'jam 2025/01/13 Defining new Max Cohort because the first cohort is commonly the maximum
                sql = sql & " AND m.RegionId IN (" & sRegs & ")"

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                While (vSQLiteReader.Read())

                    If vSQLiteReader("MaxValue") > vSQLiteReader("MaxValue2") Then
                        MaxScale = vSQLiteReader("MaxValue")
                    Else
                        MaxScale = vSQLiteReader("MaxValue2")
                    End If

                End While
            End If
        End If

        For i = 0 To 6
            Session("MaxScale")(i) = MaxScale * Margin
        Next
        Session("MaxScale")(7) = MaxScale * Margin * 2

ErrHandler12:
        Exit Sub

    End Sub

    'jrs 2014/02/14
    Private Sub mnuFixCtScMan_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCtScMan") = False
        Else
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            Session("mnuFixCtScMan") = True
        End If

        If Session("mnuFixCtScMan") = True Then
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCtSc") = False
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
            Session("mnuFixCteScTime") = False

            txtPop.Text = Session("MaxScale")(1)
            lblJS.Visible = False
            pnlMaxScale.Visible = True
            pnlCompare.Visible = False
        Else
            PackJScritpBuilder()
        End If
    End Sub

    'jrs 2014/02/06
    Private Sub mnuUsePctPop_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("mnuUsePctPop") = Not Session("mnuUsePctPop")

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = EventArgs

        If Session("mnuUsePctPop") = False Then
            objSender.Groups("Change Scale").Items("Use Percent of Population").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"/Images/blank.gif"
        Else
            objSender.Groups("Change Scale").Items("Use Percent of Population").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"/Images/checkbox.gif"
        End If

        If Session("bCompare") Then
            mnuCompare_Click()
        Else
            Graph_Draw(Session("EdCohort_GraphType"))
        End If
    End Sub

    'jrs 2014/02/14
    Protected Sub cmdOKMaxScale_Click(sender As Object, e As System.EventArgs) Handles cmdOKMaxScale.Click
        Dim i As Integer
        If Session("EdCohort_GraphType") <> 7 Then
            For i = 0 To 6
                Session("MaxScale")(i) = txtPop.Text
            Next
            Session("MaxScale")(7) = Val(txtPop.Text) * 2
        Else
            For i = 0 To 6
                Session("MaxScale")(i) = Val(txtPop.Text) / 2
            Next
            Session("MaxScale")(7) = txtPop.Text
        End If

        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
            pnlCompare.Visible = True
        Else
            Graph_Draw(Session("EdCohort_GraphType"))
        End If

        lblJS.Visible = True
        pnlMaxScale.Visible = False
    End Sub

    'jrs 2014/02/14
    Protected Sub cmdCancleMaxScale_Click(sender As Object, e As System.EventArgs) Handles cmdCancleMaxScale.Click
        lblJS.Visible = True
        pnlMaxScale.Visible = False
    End Sub

    'jrs 2014/02/05
    'Converts Population Array to Percent of Population
    Public Sub ConvertToPctPop()
        'Find Total Population
        Dim TotPop As Single
        Dim j As Integer, k As Integer
        For j = 1 To NCohorts
            TotPop = TotPop + Session("EC_TempPopDatabase")(j, 1) + Session("EC_TempPopDatabase")(j, 2)
        Next j
        'Convert to Percent
        If TotPop > 0 Then
            For j = 1 To NCohorts
                For k = 1 To 8
                    Session("EC_TempDatabase")(j, k) = Session("EC_TempDatabase")(j, k) / TotPop * 100
                Next
            Next j
        End If
    End Sub

    Private Function FindLastCohortWithData(ByRef Vals As Object, ByVal lastCol As Integer, ByVal gphIdx As Integer) As Integer
        'jrs 2013/03/29
        FindLastCohortWithData = 21

        'jam 2026/01/22 MAxCohort
        MaxCohortGroup = NCohorts


        If gphIdx > 4 Then
            Exit Function
        End If
        'jrs 2014/02/21 If NCohorts > 21 Then
        'Find Last cohort with data
        Dim k As Integer
        'jrs 2014/02/21
        If gphIdx > 0 Then
            For l% = NCohorts To 1 Step -1
                For k = 1 To lastCol
                    'jrs 2014/10/15
                    If Not IsDBNull(Vals(l%, k, gphIdx)) Then
                        If Vals(l%, k, gphIdx) > 0.00005 Then
                            FindLastCohortWithData = l
                            Exit For
                        End If
                    End If
                Next k
                If k < lastCol + 1 Then
                    Exit For
                End If
            Next l%

            'jam 2026/01/22 The graph with the max Cohort in compare Option
            If FindLastCohortWithData > MaxCohortGroup Then
                MaxCohortGroup = FindLastCohortWithData
            End If

        Else
            For l% = NCohorts To 1 Step -1
                For k = 1 To lastCol
                    'jrs 2014/10/15
                    If Not IsDBNull(Vals(l%, k)) Then
                        If Vals(l%, k) > 0.00005 Then
                            FindLastCohortWithData = l
                            Exit For
                        End If
                    End If
                Next k
                If k < lastCol + 1 Then
                    Exit For
                End If
            Next l%
        End If

        'jam 2026/01/22 With Compare option, return the maximum category with data
        'jrs 2014/02/21 End If
        'Return FindLastCohortWithData

        If Not Session("bCompare") Then
            Return FindLastCohortWithData
        Else
            If FindLastCohortWithData > MaxCohortGroup Then
                Return FindLastCohortWithData
            Else
                Return MaxCohortGroup
            End If

        End If


    End Function

    Private Function PlotColToJSSeries(ByVal i As Integer, ByRef Vals As Object, ByVal bShow100 As Boolean, ByVal Idx As Integer, _
                ByVal gphType As MortType, ByVal CompIdx As Integer, ByVal iNCohortsWD As Integer, _
                ByVal bAllZero As Boolean) As String

        On Error GoTo ErrHandler13

        Dim iNCohorts As Integer
        If bShow100 Then
            iNCohorts = iNCohortsWD 'jrs2013/08/16 NCohorts
        Else
            iNCohorts = iNCohortsWD - 1 'jrs2013/08/16NCohorts - 1
        End If

        Dim bLeftPyramid As Boolean = False, colIdx As Integer, iLowCohort As Integer
        colIdx = i
        iLowCohort = 0
        'Labor
        If Idx > 4 Then
            iNCohorts = 14
            iLowCohort = 4
            Select Case Idx
                Case 5, 7
                    colIdx = i - 1
                Case 6
                    colIdx = i - 2
            End Select
        Else
            If i = 1 Or i = 6 Then
                iNCohorts = 3
            End If
            If (i = 2 Or i = 7) And Idx = 0 Then
                iLowCohort = 4
            Else
                iLowCohort = 1
            End If
            'cdv 2021/01/15 ****START**** Switch which side of pyramid to draw to based off gender order
            If i < 6 And Idx = 0 And bMaleFirst Then
                bLeftPyramid = True
            ElseIf bMaleFirst = False And i >= 6 And Idx = 0 Then
                bLeftPyramid = True
            End If
            'cdv 2021/01/15 ****END****
            If (i > 1 And i < 7) Then
                colIdx = i - 1
            ElseIf i > 6 Then
                colIdx = i - 2
            End If
        End If

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i, Idx, gphType) & """, color:""" & getColor(i, Idx, gphType)
        'cdv 2021/01/15 Added logic to preserve legend text when gender order changes
        If (Idx = 0 And Not bLeftPyramid And bMaleFirst = True) Or (Idx = 0 And bLeftPyramid And bMaleFirst = False) Then
            sJscript = sJscript & """, showInLegend: false, data:[" & vbCr
        Else
            sJscript = sJscript & """, data:[" & vbCr
        End If

        Dim j As Integer
        Dim bFirst As Boolean = True
        If Session("bCompare") And CompIdx > 0 And CompIdx < 5 And (Session("AboutParent") = "frm_EdnCohoDisp.aspx" Or Session("AboutParent") = "frm_MortCohorts.aspx" Or Session("AboutParent") = "frm_PopCohortDisp.aspx") Then 'jrs 2014/02/12
            If iLowCohort > 1 Then
                For j = 1 To iLowCohort - 1
                    If bFirst Then
                        bFirst = False
                    Else
                        sJscript = sJscript & "," & vbCr
                    End If
                    sJscript = sJscript & "0"
                Next
            End If
            For j = iLowCohort To iNCohorts
                'jrs 2014/10/15
                If Not IsDBNull(Vals(j, colIdx, CompIdx)) Then
                    'jrs 2013/03/29 health pyramid, for comm dis. is messed up when aids is 0 in higher ages, it shows up big
                    'it might only happen in stacked pyramids
                    If Vals(j, colIdx, CompIdx) >= 0 Then
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & "," & vbCr
                        End If
                        If Not bLeftPyramid Then
                            sJscript = sJscript & CStr(Vals(j, colIdx, CompIdx))
                        Else
                            sJscript = sJscript & CStr(-1 * Vals(j, colIdx, CompIdx))
                        End If
                    End If
                End If
            Next j
        Else
            If iLowCohort > 1 Then
                For j = 1 To iLowCohort - 1
                    If bFirst Then
                        bFirst = False
                    Else
                        sJscript = sJscript & "," & vbCr
                    End If
                    sJscript = sJscript & "0"
                Next
            End If
            For j = iLowCohort To iNCohorts
                'jrs 2013/01/03
                If Not IsDBNull(Vals(j, colIdx)) Then
                    'jrs 2014/02/21 added condition to check if all are 0
                    'jrs 2013/03/29 
                    If Vals(j, colIdx) >= 0 Or (j = iNCohorts And bAllZero) Then 'jrs 2013/08/19 added or j=iNCohorts, problems when Males are all 0s
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & "," & vbCr
                        End If
                        If Not bLeftPyramid Then
                            sJscript = sJscript & CStr(Vals(j, colIdx))
                        Else
                            sJscript = sJscript & CStr(-1 * Vals(j, colIdx))
                        End If
                    End If
                Else
                    If bFirst Then
                        bFirst = False
                    Else
                        sJscript = sJscript & "," & vbCr
                    End If
                    sJscript = sJscript & "0"
                End If
            Next j
        End If

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
ErrHandler13:
        Exit Function
    End Function

    Private Function PlotColToJSSeriesColumn(ByVal i As Integer, ByRef Vals As Object) As String
        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesNameColumn(i) & """, color:""" & getColorColumn(i) & """, data:[" & vbCr

        sJscript = sJscript & CStr(Vals(i))

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeriesColumn = sJscript
    End Function

    Private Function PlotColSUMToJSSeries(ByVal i As Integer, ByRef Vals As Object) As String
        Dim iNCohorts As Integer
        iNCohorts = NCohorts

        On Error GoTo ErrHandler14

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i, 5, Nothing) & """, color:""" & getColor(i, 5, Nothing) & """, data: [" & vbCr

        Dim colIdx As Integer, iLowCohort As Integer
        colIdx = i
        iLowCohort = 0
        'Labor
        iNCohorts = 14
        iLowCohort = 4
        colIdx = i - 1

        Dim j As Integer, bFirst As Boolean = True
        If iLowCohort > 1 Then
            For j = 1 To iLowCohort - 1
                If bFirst Then
                    bFirst = False
                Else
                    sJscript = sJscript & "," & vbCr
                End If
                sJscript = sJscript & "0"
            Next
        End If

        For j = iLowCohort To iNCohorts
            If bFirst Then
                bFirst = False
            Else
                sJscript = sJscript & "," & vbCr
            End If

            sJscript = sJscript & CStr(Vals(j, colIdx) + Vals(j, colIdx + 4))
        Next

        sJscript = sJscript & "]}" & vbCr

        PlotColSUMToJSSeries = sJscript

ErrHandler14:
        Exit Function
    End Function
    'jam 2024/10/27
    Private Function PlotColSUMToJSSeriesComp(ByVal i As Integer, ByRef Vals As Object, ByVal NumGrph As Integer) As String
        Dim iNCohorts As Integer
        iNCohorts = NCohorts

        On Error GoTo ErrHandler14

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i, 5, Nothing) & """, color:""" & getColor(i, 5, Nothing) & """, data: [" & vbCr

        Dim colIdx As Integer, iLowCohort As Integer
        colIdx = i
        iLowCohort = 0
        'Labor
        iNCohorts = 14
        iLowCohort = 4
        colIdx = i - 1

        Dim j As Integer, bFirst As Boolean = True
        If iLowCohort > 1 Then
            For j = 1 To iLowCohort - 1
                If bFirst Then
                    bFirst = False
                Else
                    sJscript = sJscript & "," & vbCr
                End If
                sJscript = sJscript & "0"
            Next
        End If

        For j = iLowCohort To iNCohorts
            If bFirst Then
                bFirst = False
            Else
                sJscript = sJscript & "," & vbCr
            End If

            sJscript = sJscript & CStr(Vals(j, colIdx, NumGrph) + Vals(j, colIdx + 4, NumGrph))
        Next

        sJscript = sJscript & "]}" & vbCr

        PlotColSUMToJSSeriesComp = sJscript

ErrHandler14:
        Exit Function
    End Function

    Private Function getSeriesName(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Legend As String
        If Session("AboutParent") = "frm_PopCohortDisp.aspx" Then
            If ii = 1 And idx <> 2 Then
                Legend = "Males"
            Else
                Legend = "Females"
            End If
        ElseIf Session("AboutParent") = "frm_CohoInfDisp.aspx" Then 'jrs 2012/06/22
            If idx <> 2 Then
                If ii = 1 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            Else
                Legend = ""
            End If
        ElseIf Session("AboutParent") = "frm_EdnCohoDisp.aspx" Then
            If idx = 0 Or idx > 4 Then
                Select Case ii
                    Case 1, 6
                        Legend = "Children"
                    Case 2, 7
                        Legend = "No education or incomplete primary education"
                    Case 3, 8
                        Legend = "Completed primary education only"
                    Case 4, 9
                        Legend = "Completed through secondary education"
                    Case 5, 10
                        Legend = "Completed through tertiary education"
                End Select
                If ii > 5 Then
                    Legend = Legend & "(F)"
                End If
            ElseIf idx < 5 Then
                If ii < 6 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            End If
        ElseIf Session("AboutParent") = "frm_MortCohorts.aspx" Then 'Health
            If idx = 0 Then
                Select Case ii
                    Case 1
                        Legend = "Communicable Disease"
                    Case 2
                        Legend = "Non Communicable Disease"
                    Case 3
                        Legend = "Injury"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            Else
                If ii < gphType.NumSubTypes / 2 + 1 Then
                    Legend = gphType.getSubTypeName(ii * 2 - 1)
                Else
                    Legend = gphType.getSubTypeName((ii - gphType.NumSubTypes / 2) * 2 - 1) & "(F)"
                End If
            End If
        ElseIf Session("AboutParent") = "frm_MorbCohorts.aspx" Then 'jrs 2013/02/22
            If idx = 0 Then
                Select Case ii
                    Case 1
                        Legend = "Communicable Disease"
                    Case 2
                        Legend = "Non Communicable Disease"
                    Case 3
                        Legend = "Injury"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    Legend = "Males"
                Else
                    Legend = "Females"
                End If
            End If
        Else 'WVS
            Dim VN As String, D1 As String
            '           Get legend components
            '           ---------------------

            VN = LTrim(RTrim(Session("VARN")(0, ii)))
            'jrs 10/27/04 Flexible Dimension Names for Graphs
            D1 = LTrim(RTrim(Session("FDNG1")(ii)))

            '          Compose legend and assign legend
            '           --------------------------------

            Legend = VN
            If Len(D1) > 0 Then
                Legend = Legend & "(" & D1 & ")"
            End If
        End If
        getSeriesName = Legend
    End Function

    Private Function getSeriesNameColumn(ByVal ii As Integer) As String
        Dim Legend As String = ""
        Select Case ii
            Case 1
                Legend = "No education or incomplete primary education"
            Case 2
                Legend = "Completed primary education only"
            Case 3
                Legend = "Completed through secondary education"
            Case 4
                Legend = "Completed through tertiary education"
        End Select
        getSeriesNameColumn = Legend
    End Function

    Private Function getColor(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Color As String
        If Session("AboutParent") = "frm_PopCohortDisp.aspx" Then
            If ii = 1 And idx <> 2 Then
                Color = "Blue"
            Else
                Color = "Lime"
            End If
        ElseIf Session("AboutParent") = "frm_CohoInfDisp.aspx" Then 'jrs 2012/06/25
            If ii = 1 Then
                Color = "Blue"
            Else
                Color = "Lime"
            End If
        ElseIf Session("AboutParent") = "frm_EdnCohoDisp.aspx" Then
            If idx = 0 Or idx > 4 Then
                Select Case ii
                    Case 1, 6
                        Color = "Gray"
                    Case 2, 7
                        Color = "Red"
                    Case 3, 8
                        Color = "Lime"
                    Case 4, 9
                        Color = "RGB(255, 215, 0)"
                    Case 5, 10
                        Color = "Blue"
                End Select
            ElseIf idx < 5 Then
                If ii < 6 Then
                    'Males
                    Color = "Blue"
                Else
                    Color = "Lime"
                End If
            End If
        ElseIf Session("AboutParent") = "frm_MortCohorts.aspx" Then  'Health
            If idx = 0 Then
                Select Case ii
                    Case 1, 4
                        Color = "RGB(255, 215, 0)"
                    Case 2, 5
                        Color = "Blue"
                    Case 3, 6
                        Color = "Red"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    'Males
                    Color = "Blue"
                Else
                    Color = "Lime"
                End If
            Else
                ' Return:
                '     iRed    contains the red component
                '     iGreen  the green component
                '     iBlue   the blue component
                '
                Dim lColor As Long, iRed As Integer, iGreen As Integer, iBlue As Integer
                If ii < gphType.NumSubTypes / 2 + 1 Then
                    lColor = gphType.getSubTypeColor(ii * 2 - 1)      'work long
                Else
                    lColor = gphType.getSubTypeColor((ii - gphType.NumSubTypes / 2) * 2 - 1)      'work long
                End If
                iRed = lColor Mod &H100  'get red component
                lColor = lColor \ &H100  'divide
                iGreen = lColor Mod &H100 'get green component
                lColor = lColor \ &H100  'divide
                iBlue = lColor Mod &H100 'get blue component

                Color = "RGB(" & iRed.ToString & "," & iGreen.ToString & "," & iBlue.ToString & ")"
            End If
        ElseIf Session("AboutParent") = "frm_MorbCohorts.aspx" Then  'jrs 2013/02/22 Morbidity
            If idx = 0 Then
                Select Case ii
                    Case 1, 4
                        Color = "RGB(255, 215, 0)"
                    Case 2, 5
                        Color = "Blue"
                    Case 3, 6
                        Color = "Red"
                End Select
            ElseIf idx < 4 Then
                If ii < 4 Then
                    'Males
                    Color = "Blue"
                Else
                    Color = "Lime"
                End If
            End If
        Else
            Color = "Lime"
        End If
        getColor = Color
    End Function

    Private Function getColorColumn(ByVal ii As Integer) As String
        Dim Color As String = ""
        Select Case ii
            Case 1
                Color = "Red"
            Case 2
                Color = "Lime"
            Case 3
                Color = "RGB(255, 215, 0)"
            Case 4
                Color = "Blue"
        End Select
        getColorColumn = Color
    End Function


    'JAM 2020-02-02
    ''Public Function SQLite_OleDB_CreateConnection() As OleDbConnection
    ''    Dim cnnStr As String

    ''    cnnStr = "MSDASQL.1;" &
    ''           "Data Source=" & Session("db2Path") & "IFsPopWork.db;" &
    ''           "Mode=Read;" &
    ''           "Catalog=IfsPopWork;"

    ''    SQLite_OleDB_CreateConnection = New OleDbConnection(cnnStr)


    ''    'Provider = MSDASQL.1;Persist Security Info=False;Data Source=##ODBC Name##;Mode=Read;Initial Catalog=##Database Name##

    ''End Function


End Class
