Option Strict Off
Option Explicit On
Imports System.Data.SQLite
'jam 2021/06/22
Imports Microsoft.AspNet.FriendlyUrls


Partial Class frm_CohoInfDisp
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
        'mti WebForm Changes From July 05, 2004
        If Not Page.IsPostBack Then
            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
            'popConn = CreateConnection(Session("db2Path"))
            'Session("popConn") = popConn
        End If
        'mti WebForm Changes From July 05, 2004
        'jrs 205/02/20
        'WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")

    End Sub

    Protected WithEvents cmd_ScriptFile As System.Web.UI.WebControls.Label
    Protected WithEvents WinFormsToWeb_MenuManager As ArtinSoft.WinFormsToWeb.WebControls.MenuManager
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region

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
    Dim TableName(6) As String
    ''mti Ifs Win April 25, 2004
    'Dim Session("PC_TempDataBase")(, ) As Single
    Dim FieldName(3, 2) As String

    'jrs 2012/06/21
    'Dim popCohoInfoNames(5) As String
    'Dim popCohoInfoDesc(5) As String
    Dim allYears() As Integer
    'Dim popCohoNum As Integer
    Dim PopByAge As Single
    Dim PopAcrossAge As Single
    Dim sRegs As String
    Dim previousRegion As String
    Dim preCName As String
    Dim DataVector(300) As Object

    'Dim regionname As String
    'Dim tempregion As Short
    Dim Case_of_Work As Short
    Dim CaseName As String
    Dim valtbl As DataTable
    'JAM 2019-12-07
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteCommand2 As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vSQLiteReader2 As SqliteDataReader
    Dim vConnString As String
    'mti WebForm Changes From July 05, 2004
    'Dim popConn As OleDb.OleDbConnection

    'JRS 07/26/02 GROUP
    'Dim CountryGroup(200) As String
    'Dim RegionIdGroup(200) As Short
    'Dim GroupTot As Short
    'Dim sAggregationI As String
    'Dim sAggregationD As String

    '==========================frm_CohoDisp=============================
    Private Sub Graph_Draw(ByRef index As Short)
        Dim GDone As Boolean
        Dim Rcnt As Integer
        Dim l As Short
        Dim GraphTitle As String
        Dim tbl2 As DataTable
        'jrs 2008/06/24
        Dim tbl3 As DataTable

        On Error GoTo ErrHandler01


        '--------------------------------------------------------------
        'start mti june, 2002
        Dim Tindex As Short
        'end mti june, 2002

        ReDim Session("PC_TempDataBase")(Session("popCohoNum"), 2)

        '   Specify the graph control to be used
        '   ------------------------------------
        'mti Ifs Win April 25, 2004
        '_gphAgedst_1.Visible = False


        'gphAgedst(1).Visible = False
        'gphAgedst(2).Visible = False
        'gphAgedst(3).Visible = False

        ''start mti june, 2002
        'gphAgedst(4).Visible = False
        frameData.Visible = False
        'jrs 2008/06/25
        grdcohort.Visible = False
        lbldata.Visible = False

        lblJS.Visible = True

        Select Case index
            Case 1
                Tindex = 1
            Case 2
                Tindex = 2
                DrawConsumptionGraph()
                Exit Sub
            Case 3
                Tindex = 3
                DrawAvgEducationGraph()
                Exit Sub
            Case 4
                Tindex = 1
        End Select

        '   Change the data source
        '   ----------------------
        'JRS 07/26/02 GROUP
        Dim rId As Short
        Dim i As Integer
        i = 0
        ''mti CohoDisp May 2004 - check this
        'If mnuGroups.Text = "Use Groups" Then  '"Use &Groups" Then 
        'mti September 2004 1
        'If wmMain.Groups("Set Group or Country").Items("Countries/Regions").IconImage = "checkbox.gif" Then
        Dim nc As Integer, rr As Integer
        Dim MTotal As Object, FTotal As Object
        For nc = 0 To Session("popCohoNum") - 1
            MTotal = SUMArray(Session("PopCohoInfo"), 4, nc, 8)
            FTotal = SUMArray(Session("PopCohoInfo"), 5, nc, 8)
            If Not IsDBNull(MTotal) And Not IsDBNull(FTotal) Then
                Session("PC_TempDataBase")(nc + 1, 1) = MTotal
                Session("PC_TempDataBase")(nc + 1, 2) = FTotal
            End If
        Next

        FillDataGrid(Tindex)

        'end mti june, 2002


        '   Draw the graph
        '   --------------
        '    gphAgedst(Index).AutoInc = 1
        '    gphAgedst(Index).DataReset = 1

        Dim TMax As Short
        Session("PopCG_Index") = index
        Session("POPCG_Regionname") = Session("PopCohort_regionname")
        Session("POPCG_tempyear") = Session("PopCohort_tempyear")

        'mti Ifs Win April 25, 2004
        If (index = 1) Then 'Age distribution; split across males/females



            GraphTitle = "Birth Cohort"

            Session("POPCB_Units") = "Population in Millions"

        ElseIf (index = 2) Then 'Fertility

            GraphTitle = "Birth Cohort Consumption"
            Session("POPCB_Units") = "Consumption in Billions"



        ElseIf (index = 3) Then 'Mortality


            GraphTitle = "Birth Cohort Average Educaton"
            Session("POPCB_Units") = "Average Years of Education of those 15 and older"
            'Start mti june, 2002
        End If

        'Start mti june, 2002
        lbldata.Text = GraphTitle & " Distribution for " & Session("PopCohort_regionname") & " in Year " & Str(Session("PopCohort_tempyear")) & " [" & Session("POPCG_Casename") & "]" & " - " & Session("POPCB_Units")
        'end mti june, 2002
        PackJScritpBuilder()

ErrHandler01:
        Exit Sub

    End Sub

    Private Function SUMArray(ByVal ArrayName As Object, ByVal iField As Integer, ByVal NCohort As Integer, ByVal iCohortIdx As Integer) As Single
        Dim i As Integer, Tot As Single = 0
        For i = 0 To UBound(ArrayName, 1)
            If ArrayName(i, iCohortIdx) = NCohort Then
                Tot = Tot + ArrayName(i, iField)
            End If
        Next
        SUMArray = Tot
    End Function

    Public Sub DrawConsumptionGraph()

        'ReDim Session("PC_TempDataBase")(session("popCohoNum"), 2)
        Dim regCount As Integer

        regCount = 1

        Dim Cvalues() As Single
        Dim rn As Integer

        'jam 2021/06/22
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String



        On Error GoTo errinConsumption
        Dim ind As Integer
        Dim sql As String
        Dim tbl2 As DataTable
        Dim rr As Integer = 0
        Dim Nume As Single
        For ind = 0 To Session("popCohoNum") - 1
            'sql = "SELECT sum([Consumption MDCs]) as Nume FROM " + TableName(4) + _
            '   " Where NCohorts=" + Str(ind)
            'SetDataTablefromAccess(Session("db2Path"), sql, "", tbl2, "")
            Nume = SUMArray(Session("PopCohoInfo"), 6, ind, 8)

            If Not IsDBNull(Nume) And PopByAge > 0 Then
                Session("PC_TempDataBase")(ind + 1, 1) = Math.Round((DataVector(Session("PopCohort_tempyear") - Session("FirstYear") + 1) * Nume) / PopByAge, 3)  ' Assigned portion for each cohort
            Else
                Session("PC_TempDataBase")(ind + 1, 1) = 0
            End If
        Next ind

        FillDataGrid(Session("PopCohort_GraphType"))

        Session("PopCG_Index") = Session("PopCohort_GraphType")
        Session("POPCG_Regionname") = Session("PopCohort_regionname")
        Session("POPCG_tempyear") = Session("PopCohort_tempyear")

        'GraphTitle$ = "Population"
        Dim BottomTitle As String
        BottomTitle = Mid("|Consumption in Billions|", 2, Len("|Consumption in Billions|") - 2)

        lbldata.Text = "Birth Cohort Consumption Value for " +
            Session("PopCohort_regionname") + " in Year " + Str(Session("PopCohort_tempyear")) + " [" + Session("POPCG_Casename") + "]" +
             " - " + BottomTitle

        PackJScritpBuilder()

        Exit Sub
errinConsumption:
        LoadMessage(Err.Description, Session("Message"))
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")

        'jam 2021/06/22
        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
        vurl = FriendlyUrl.Href("~/frm_Message")
        Response.Redirect(vurl)


    End Sub


    Public Sub DrawAvgEducationGraph()

        'ReDim Session("PC_TempDataBase")(session("popCohoNum"), 2)
        ReDim Session("AvgEduMFToalValues")(Session("popCohoNum"), 2)
        DataVector(297) = 0
        DataVector(298) = 0
        DataVector(299) = 0
        DataVector(300) = 0

        'jam 2021/06/23
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Dim rn As Integer
        On Error GoTo errinAvgEdu
        Dim ind As Integer
        Dim Sql As String
        Dim tbl2 As DataTable
        Dim rr As Integer = 0
        Dim AvgM As Single, AvgF As Single, PopMT As Single, PopFT As Single
        For ind = 0 To Session("popCohoNum") - 1
            'Sql = "SELECT sum([EdnYears15Males]) as AvgM, sum([EdnYears15Females]) as AvgF, sum(PopMTotal) as PopMT, sum(PopFTotal) as PopFT FROM EdnYears15AvgCase" + _
            '   " Where NCohorts=" + Str(ind)
            'SetDataTablefromAccess(Session("db2Path"), Sql, "", tbl2, "")
            AvgM = SUMArray(Session("TmpEdnYears15AvgCase"), 3, ind, 7)
            AvgF = SUMArray(Session("TmpEdnYears15AvgCase"), 4, ind, 7)
            PopMT = SUMArray(Session("TmpEdnYears15AvgCase"), 5, ind, 7)
            PopFT = SUMArray(Session("TmpEdnYears15AvgCase"), 6, ind, 7)

            If Not IsDBNull(AvgM) And Not IsDBNull(AvgF) Then
                If PopMT > 0 Then
                    Session("PC_TempDataBase")(ind + 1, 1) = Math.Round(AvgM / PopMT, 5)
                Else
                    Session("PC_TempDataBase")(ind + 1, 1) = 0
                End If
                If PopFT > 0 Then
                    Session("PC_TempDataBase")(ind + 1, 2) = Math.Round(AvgF / PopFT, 5)
                Else
                    Session("PC_TempDataBase")(ind + 1, 2) = 0
                End If
            End If
        Next ind
        For ind = 0 To Session("popCohoNum") - 1
            'Sql = "SELECT sum(PopMTotal) as PopMT, sum(PopFTotal) as PopFT FROM EdnYears15AvgCase" + _
            '   " Where NCohorts=" + Str(ind)
            'SetDataTablefromAccess(Session("db2Path"), Sql, "", tbl2, "")
            PopMT = SUMArray(Session("TmpEdnYears15AvgCase"), 5, ind, 7)
            PopFT = SUMArray(Session("TmpEdnYears15AvgCase"), 6, ind, 7)

            ' using the last 4 indexes of DataVector array to store the total average education years for male and female
            If Not IsDBNull(PopMT) Or Not IsDBNull(PopFT) Then
                Session("AvgEduMFToalValues")(ind + 1, 1) = PopMT
                Session("AvgEduMFToalValues")(ind + 1, 2) = PopFT
                DataVector(297) = DataVector(297) + PopMT
                DataVector(298) = DataVector(298) + PopFT
                DataVector(299) = DataVector(299) + (Session("PC_TempDataBase")(ind + 1, 1) * PopMT)
                DataVector(300) = DataVector(300) + (Session("PC_TempDataBase")(ind + 1, 2) * PopFT)
            End If
        Next ind
        DataVector(299) = DataVector(299) / DataVector(297)
        DataVector(300) = DataVector(300) / DataVector(298)

        FillDataGrid(Session("PopCohort_GraphType"))

        Session("PopCG_Index") = Session("PopCohort_GraphType")
        Session("POPCG_Regionname") = Session("PopCohort_regionname")
        Session("POPCG_tempyear") = Session("PopCohort_tempyear")

        'GraphTitle$ = "Population"
        Dim BottomTitle As String
        BottomTitle = Mid("|Average Years of Education of those 15 Years and Older|", 2, Len("|Average Years of Education of those 15 Years and Older|") - 2)

        lbldata.Text = "Birth Cohort Average Education Value for " +
            Session("PopCohort_regionname") + " in Year " + Str(Session("PopCohort_tempyear")) + " [" + Session("POPCG_Casename") + "]" +
             " - " + BottomTitle

        Session("DataVector") = DataVector

        PackJScritpBuilder()

        Exit Sub
errinAvgEdu:
        LoadMessage(Err.Description, Session("Message"))
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")
        'jam 2021/06/23
        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
        vurl = FriendlyUrl.Href("~/frm_Message")
        Response.Redirect(vurl)

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
        If Session("PopCohort_tempyear") Mod 5 = 0 Then
            Session("PopCohort_tempyear") = Session("PopCohort_tempyear") + 5
        Else
            While Session("PopCohort_tempyear") Mod 5 <> 0
                Session("PopCohort_tempyear") += 1
            End While
        End If

        If Case_of_Work = 0 Then
            'jrs 2005/01/10 end_year = Session("LastYear")
            'mti July 2006 end_year = LastYearForecast
            end_year = FindLastYear()
            If end_year = 0 Then
                end_year = Session("LastYearForecast")
            End If
        Else
            end_year = Session("PopCohort_year_run")
        End If

        If Session("PopCohort_tempyear") <= end_year Then
            If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
                Graph_Draw((Session("PopCohort_GraphType")))
            End If
        End If
        If Session("PopCohort_tempyear") >= end_year Then
            objSender.Groups("Time").Items("Advance").Enabled = False
            Session("PopCohort_tempyear") = end_year
        End If
        objSender.Groups("Time").Items("Regress").Enabled = True


    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------

        'jam 2021/06/23
        'Support.FormManager.Close(Me)

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension


        vfriendlyurl = FriendlyUrl.Resolve("~/frm_MainMenu.aspx")

        vurl = FriendlyUrl.Href("~/frm_MainMenu")

        Response.Redirect(vurl)
    End Sub


    '==========================frm_CohoDisp=============================
    Public Sub mnuRegress_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'jrs 2008/06/06 If Session("PopCohort_tempyear") <= FirstYearForecast Then
        If Session("PopCohort_tempyear") <= Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            Session("PopCohort_tempyear") = Session("PopCohort_tempyear") - 5
            If Session("PopCohort_tempyear") <= Session("FirstYearSce") Then
                Session("PopCohort_tempyear") = Session("FirstYearSce")
                objSender.Groups("Time").Items("Regress").Enabled = False
            End If
            objSender.Groups("Time").Items("Advance").Enabled = True
            If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
                Graph_Draw((Session("PopCohort_GraphType")))
            End If
        End If

    End Sub

    '==========================frm_CohoDisp=============================
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        '--------------------------------------------------------------

        'JAM 2019-12-21
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
    Private Sub cntrylist_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------

        '  Specify the country selected
        '  ----------------------------
        'If cntrylist.SelectedIndex = -1 Then
        '    Session("PopCohort_tempregion") = 1
        'Else
        '    Session("PopCohort_tempregion") = cntrylist.SelectedIndex + 1
        'End If

        Session("PopCohort_regionname") = GRPS(1, Session("PopCohort_tempregion"))
        'cntrylist.Visible = False
        If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If
    End Sub

    '==========================frm_CohoDisp=============================
    Private Sub frm_CohoInfDisp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'If Not Page.IsPostBack Then
        If Support.FormManager.LoadState Then
            Support.FormManager.StateRestore(Me)
            Support.FormManager.LoadState = False
        End If
        Dim l As Short
        Dim ButSpace As Short
        Dim SumWidth As Object
        Dim HalInch As Object

        'jam 2021/06/24
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim othYear As String
        Dim othFile As String
        Dim qtyPAram As Integer


        '--------------------------------------------------------------

        On Error GoTo ErrHandler02


        'end WebMenu


        'jam 2021/06/24
        If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Request.QueryString("Year")) Or Not IsNothing(Request.QueryString("File")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                'jrs 2025/02/21
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

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_CohoInfDisp.aspx")


            If othCountry <> "" And othYear <> "" And othFile <> "" Then
                vurl = FriendlyUrl.Href("~/frm_CohoInfDisp", CohoInfPrepareForFriendlyURL(othCountry), othYear, othFile)

                Response.Redirect(vurl)

            Else
                If othCountry <> "" And othYear = "" And othFile = "" Then
                    vurl = FriendlyUrl.Href("~/frm_CohoInfDisp", CohoInfPrepareForFriendlyURL(othCountry))

                    Response.Redirect(vurl)

                Else
                    If othCountry <> "" And othYear <> "" And othFile = "" Then
                        vurl = FriendlyUrl.Href("~/frm_CohoInfDisp", CohoInfPrepareForFriendlyURL(othCountry), othYear)

                        Response.Redirect(vurl)
                    End If

                End If


            End If


        End If





        'JAM 2019-12-07
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                'vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db" + ";" 'Version=3;"

                'JAM 2020-05-04
                If Not Session("SQLiteDB") Is Nothing Then 'JAM 2020-04-13
                    vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                Else
                    vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                End If

                'jam 2025/05/31 Changing connection mode
                'vSQLiteCon = New SqliteConnection(vConnString) ' & "New=true;")
                'vSQLiteCon.Open()

                'jam 2025/05/31 Changing connection mode
                vSQLiteCon = OpenJournalSQLite(vConnString)



            End If
        End If


        '   Initializing the form with Base Case
        '   ------------------------------------


        '   Initializing the table names and field names
        '   --------------------------------------------

        'JAM 2019-12-14
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            TableName(1) = "PopBaseCase"
            TableName(2) = "FerBaseCase"
            TableName(3) = "MorBaseCase"
            FieldName(1, 1) = "PopulationMales"
            FieldName(1, 2) = "PopulationFemales"
            FieldName(2, 1) = "Fertility"
            FieldName(2, 2) = "Fertility"
            FieldName(3, 1) = "MortalityMales"
            FieldName(3, 2) = "MortalityFemales"
        Else

            TableName(1) = "Pop_Base_Case"
            TableName(2) = "Fer_Base_Case"
            TableName(3) = "Mor_Base_Case"
            TableName(4) = "PopCohoInfo"
            TableName(5) = "FerCohoInfo"
            TableName(6) = "MorCohoInfo"

            FieldName(1, 1) = "PopulationMales"
            FieldName(1, 2) = "PopulationFemales"
            FieldName(2, 1) = "Fertility"
            FieldName(2, 2) = "Fertility"
            FieldName(3, 1) = "MortalityMales"
            FieldName(3, 2) = "MortalityFemales"

        End If

        If Session("popCohoNum") Is Nothing Then
            Session("popCohoNum") = 5

            ReDim Session("popCohoInfoNames")(9)
            Session("popCohoInfoNames")(0) = "Before 1945"
            Session("popCohoInfoNames")(1) = "1945-1964"
            Session("popCohoInfoNames")(2) = "1965-1984"
            Session("popCohoInfoNames")(3) = "1985-2010"
            Session("popCohoInfoNames")(4) = "2011+"

            ReDim Session("popCohoInfoDesc")(9)
            Session("popCohoInfoDesc")(0) = "Pre Baby Boom"
            Session("popCohoInfoDesc")(1) = "Baby Boom"
            Session("popCohoInfoDesc")(2) = "Generation X"
            Session("popCohoInfoDesc")(3) = "Generation Y"
            Session("popCohoInfoDesc")(4) = "Generation Z"
        End If

        ReDim Session("txtCatName")(9)
        ReDim Session("txtCatRange")(9)

        Session("txtCatName")(0) = txtCatName1
        Session("txtCatName")(1) = txtCatName2
        Session("txtCatName")(2) = txtCatName3
        Session("txtCatName")(3) = txtCatName4
        Session("txtCatName")(4) = txtCatName5
        Session("txtCatName")(5) = txtCatName6
        Session("txtCatName")(6) = txtCatName7
        Session("txtCatName")(7) = txtCatName8
        Session("txtCatName")(8) = txtCatName9
        Session("txtCatName")(9) = txtCatName10

        Session("txtCatRange")(0) = txtCatRange1
        Session("txtCatRange")(1) = txtCatRange2
        Session("txtCatRange")(2) = txtCatRange3
        Session("txtCatRange")(3) = txtCatRange4
        Session("txtCatRange")(4) = txtCatRange5
        Session("txtCatRange")(5) = txtCatRange6
        Session("txtCatRange")(6) = txtCatRange7
        Session("txtCatRange")(7) = txtCatRange8
        Session("txtCatRange")(8) = txtCatRange9
        Session("txtCatRange")(9) = txtCatRange10


        'jam 2021/06/28
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String
        Dim vYear As String
        Dim vScenario As String

        If segments.Count = 1 Then

            vCountry = segments(0)
            vYear = Nothing
            vScenario = Nothing

        ElseIf segments.Count = 2 Then
            vCountry = segments(0)
            vYear = segments(1)
            vScenario = Nothing
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


            If Session("POPCG_Casename") Is Nothing Then
                CaseName = "Base Case"

                Session("POPCG_Casename") = CaseName
            Else
                CaseName = Session("POPCG_Casename")
            End If

            'mti WebForm Changes From June 21, 2004 -start
            Dim TempDatabase(NCohorts, 2) As Single 'has to be session var
            Dim CountryGroup(250) As String
            Dim RegionIdGroup(250) As Short

            Session("PC_TempDataBase") = TempDatabase  '(NCohorts, 2)
            Session("CountryGroup") = CountryGroup
            Session("RegionIdGroup") = RegionIdGroup


            'mti WebForm Changes From June 21, 2004 -end
            'jrs 2011/05/05
            If Session("PopCohort_Grouping") Is Nothing Then
                Session("PopCohort_Grouping") = False
                Session("PopCohort_tempregion") = 1
                Session("PopCohort_regionname") = GRPS(1, 1)

                'jam 2021/06/30
                Session("IsGroup") = False

            End If

            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                'JAM 2019-12-08
                If Session("mnuSQLite") Is Nothing Then
                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.db"

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
            Session("PopCohort_year_run") = Session("LastYear")

            '   Initializing the graph with population graph for First Region, Session("FirstYear")
            '   ------------------------------------------------------------------------
            'jrs 2005/01/10 Session("PopCohort_tempyear") = AMAX(Session("FirstYear"), 1995)
            If Session("PopCohort_tempyear") Is Nothing Then
                Session("PopCohort_tempyear") = AMAX(FirstYearForecast, 1995)
            End If

            If Session("PopCohort_GraphType") Is Nothing Then
                Session("PopCohort_GraphType") = 1
            End If

            'jrs 2008/05/07
            Session("AboutParent") = "frm_CohoInfDisp.aspx"

            'jrs 2012/06/21
            Call LoadIncConSavTable()
            allYears = RetrieveYearsFromCagegories(Session("popCohoNum"), Session("popCohoInfoNames"))
            Session("allYears") = allYears
            If Session("PopCohort_Grouping") = True Then
                LoadRegions()
            End If

            If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
                Graph_Draw((Session("PopCohort_GraphType")))
            End If

            'jrs 2008/06/06
            Session("FirstYearSce") = Session("FirstYear")
        Else 'jrs 2008/09/16
            Case_of_Work = Session("POPCG_Case_of_Work")
            PopByAge = Session("PopByAge")
            allYears = Session("allYears")
            If Not Session("DataVector") Is Nothing Then
                DataVector = Session("DataVector")
            End If
            If Not Session("sRegs") Is Nothing Then
                sRegs = Session("sRegs")
            End If
        End If

        'webmenu
        'mti WebForm Changes to be taken by Jose
        wmMain.UserData = "jsolorza::976739975"

        wmMain.MenuBar.ShowOnMouseOver = True
        'jam 2021/06/30
        'wmMain.ClearPixelImage = "blank.gif"
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
        cswmItem.Caption = "Population Distribution"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Consumption Distribution"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Average Education Distribution"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Show Numbers"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Numbers"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Set Group or Country/Region"
        cswmGroup1.ID = "Group_78c2"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Groups"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'mti Augut 2004 3 -start
        'cswmItem.IconImage = "blank.gif"
        If Session("PopCohort_Grouping") = True Then
            'jam 2021/07/02
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/07/02
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Countries/Regions"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"
        If Session("PopCohort_Grouping") = True Then
            'jam 2021/07/02
            'cswmItem.IconImage = "blank.gif"

            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")

        Else
            'jam 2021/07/02
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")

        End If
        'mti Augut 2004 3 end


        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Change Display"
        cswmGroup1.ID = "Group_1113"

        cswmItem = cswmGroup1.Items.Add()
        If Session("PopCohort_Grouping") = True Then
            cswmItem.Caption = "Change Groups"
        Else
            cswmItem.Caption = "Change Countries/Regions"
        End If

        'jrs 2011/11/14
        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()
        If Session("PopCohort_Grouping") = False Then
            For l = 1 To NR
                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = GRPS(1, l)
                cswmItem.UserData = "Reg"
                'cswmItem.OnClick = "cntrymnu_click('" & IFsRegs.Item(GRPS(1, l)).ISO3166_2Code & "');"
                If l < NR Then
                    If GRPS(1, l).Chars(0) <> GRPS(1, l + 1).Chars(0) Then
                        cswmItem = cswmGroup2.Items.Add()
                        cswmItem.Caption = "--shadow--"
                        cswmItem.DividerColor = "#808080"
                        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                    End If
                End If
            Next
        Else
            For l = 1 To GCount
                cswmItem = cswmGroup2.Items.Add()
                cswmItem.Caption = GroupNames(l)
                cswmItem.UserData = "Grp"
                If l < GCount Then
                    If GroupNames(l).Chars(0) <> GroupNames(l + 1).Chars(0) Then
                        cswmItem = cswmGroup2.Items.Add()
                        cswmItem.Caption = "--shadow--"
                        cswmItem.DividerColor = "#808080"
                        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                    End If
                End If
            Next
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
        If Session("POPCG_Casename") = "Base Case" Then
            'jam 2021/07/02
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/07/03
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Work Case"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'mti WebForm Changes From June 21, 2004 
        If Session("POPCG_Casename") = "Work Case" Then
            'jam 2021/07/03
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            'jam 2021/07/03
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If
        'jrs 2008/06/16
        If UCase(Session("ActiveBase")) <> "IFSBASEHIST.RUN" Then
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Historical Forecast"
            cswmItem.IconHeight = 16
            If Session("POPCG_Casename") = "Historical Forecast" Then
                'jam 2021/07/03
                'cswmItem.IconImage = "checkbox.gif"
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
            Else
                'jam 2021/07/03
                'cswmItem.IconImage = "blank.gif"
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
            End If
        End If

        'jrs 2008/06/05
        'Load other Scenario Menu options for display
        LoadSceMenus(cswmGroup2)

        'jrs 2012/06/29
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Birth Cohort Category"

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Time"
        cswmGroup1.ID = "Group_5ca8"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Advance"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regress"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"

        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Advance"
        'cswmGroup1.ID = "Group_5ca8"

        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Regress"
        'cswmGroup1.ID = "Group_a83d"

        'mti enable this later 
        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Print"
        'cswmGroup1.ID = "Group_f3d2"

        'mti enable this later 
        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Save"
        'cswmGroup1.ID = "Group_3f67"

        'mti enable this later 
        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "About Population"
        'cswmGroup1.ID = "Group_8afc"

        'jrs 2006/12/21
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/03 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16004);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jrs 2008/05/07
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "About"
        'jrs 2025/02/04 cswmGroup1.OnMenuBarClick = "location.href='frm_AboutPopulation.aspx';"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "About"

        AddHandler wmMain.ServerItemClick, AddressOf ItemClickHandlerMain
        'If Session("PopCohort_Grouping") Then
        '    wmMain.Groups("Use Group or Region").Items("Groups").IconImage = "checkbox.gif"
        '    wmMain.Groups("Use Group or Region").Items("Regions").IconImage = "blank.gif"
        '    'Session("PopCohort_Grouping") = True
        '    'lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
        'Else
        '    wmMain.Groups("Use Group or Region").Items("Regions").IconImage = "checkbox.gif"
        '    wmMain.Groups("Use Group or Region").Items("Groups").IconImage = "blank.gif"
        '    'Session("PopCohort_Grouping") = False
        '    'cntrylist_SelectedIndexChanged(cntrylist, New System.EventArgs)
        'End If
        'End If

        'jrs 2011/05/02
        'If Not IsNothing(Request.QueryString("Country")) Then
        'jam 2021/07/03
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(vCountry)) And Not Session("IsGroup") Then
            'jam 2021/07/03
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> vCountry Or Not Page.IsPostBack Then
                'jam 2021/07/03
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = vCountry

                'jrs 2011/09/15
                If Session("PopCohort_Grouping") Then
                    'need to fix menus
                    'jam 2021/07/03
                    wmMain.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    wmMain.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                    wmMain.Groups("Change Display").Items(0).Caption = "Change Countries/Regions"
                End If
                Session("PopCohort_Grouping") = False
                'Validate
                Dim rs As DataTable
                SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT NewRegion FROM [Country Translation] as c, " & Regionalization & " as r where c.Country = r.Country AND [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs)

                If rs.Rows.Count = 0 Then
                    Session("MMCountry") = ""
                    rs = Nothing
                Else
                    Session("cNumber") = rs.Rows(0).Item("NewRegion")
                End If
            End If
            'jam 2021/07/03
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(vCountry)) And Session("IsGroup") Then
            'jam 2021/07/03
            'If Session("PopCohort_regionname") <> Request.QueryString("Group") Then
            If Session("PopCohort_regionname") <> vCountry Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/07/03
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(vCountry, False, Me)
                If gIdx > 0 Then
                    'jam 2021/07/03
                    'Session("MMGroup") = Request.QueryString("Group")
                    Session("MMGroup") = vCountry
                    Session("PopCohort_regionname") = Session("MMGroup")
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("PopCohort_Grouping") = True Then
                    lstGroups_SelectedIndexChanged(Nothing, New System.EventArgs)
                Else
                    mnuGroups_Click(wmMain, New Coalesys.WebMenu.ServerItemClickEventArgs(wmMain.Groups("Group_78c2").Items(0)))
                End If
            End If
        End If
        'jrs 2012/05/03
        'If Not IsNothing(Request.QueryString("Year")) Then
        'jam 2021/07/03
        If Not IsNothing(Request.QueryString("Year")) Or Not IsNothing(vYear) Then
            'jam 2021/07/03
            'If Session("PopCohort_tempyear") <> Request.QueryString("Year") Then
            If Session("PopCohort_tempyear") <> vYear Then
                'jam 2021/07/03
                'Session("PopCohort_tempyear") = Request.QueryString("Year")
                Session("PopCohort_tempyear") = vYear

                If Session("PopCohort_tempyear") < Session("FirstYearSce") Then Session("PopCohort_tempyear") = Session("FirstYearSce")
                If Session("PopCohort_tempyear") > Session("PopCohort_year_run") Then Session("PopCohort_tempyear") = Session("PopCohort_year_run")
                'jrs 2012/06/27
                If Session("PopCohort_Grouping") = True Then
                    lstGroups_SelectedIndexChanged(Nothing, New System.EventArgs)
                End If
            End If
        End If
        'jam 2021/07/03
        If Not IsNothing(Request.QueryString("File")) Or Not IsNothing(vScenario) Then
            'jrs 2012/05/05
            ' If Not IsNothing(Request.QueryString("File")) Then
            'jam 2021/07/03
            If Session("POPCG_Casename") <> vScenario Then
                'If Session("POPCG_Casename") <> Request.QueryString("File") Then
                'Session("POPCG_Case_of_Work") = Case_of_Work
                'jam 2021/07/03
                'CaseName = Request.QueryString("File")
                CaseName = vScenario
                'jam 2021/07/03
                'If Request.QueryString("File") <> "Base Case" And Request.QueryString("File") <> "Historical Forecast" Then
                If vScenario <> "Base Case" And vScenario <> "Historical Forecast" Then
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"


                    'JAM 2020-02-12
                    If Session("mnuSQLite") Is Nothing Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"

                    Else

                        If Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"
                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".db"

                            Session("SQLiteDB") = "IFsPop" & CaseName & ".db"

                            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                        End If
                    End If



                    If Dir(Session("db2Path")) = "" Then
                        CaseName = "Base Case"
                        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                            'JAM 2019-12-08
                            If Session("mnuSQLite") Is Nothing Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                            Else

                                If Not Session("mnuSQLite") = "True" Then
                                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                                Else
                                    'Session("db2Path") = Session("PutFdrvRun") & "ifsPop.db"

                                    'JAM 2020-05-04
                                    Session("SQLiteDB") = "ifsPop.db"

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


                                    'JAM 2020-05-04
                                    Session("SQLiteDB") = "IFsPopHist.db"

                                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                                End If
                            End If


                        End If
                    End If
                    'jam 2021/07/03
                    'ElseIf Request.QueryString("File") = "Historical Forecast" Then
                ElseIf vScenario = "Historical Forecast" Then
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"


                    'JAM 2020-02-12
                    If Session("mnuSQLite") Is Nothing Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                    Else

                        If Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"


                            'JAM 2020-05-04
                            Session("SQLiteDB") = "IFsPopHist.db"

                            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                        End If
                    End If

                Else
                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                        'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                        'JAM 2019-12-08
                        If Session("mnuSQLite") Is Nothing Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                        Else

                            If Not Session("mnuSQLite") = "True" Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                            Else
                                'Session("db2Path") = Session("PutFdrvRun") & "ifsPop.db"


                                'JAM 2020-05-04
                                Session("SQLiteDB") = "ifsPop.db"

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


                                'JAM 2020-05-04
                                Session("SQLiteDB") = "IFsPopHist.db"

                                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                            End If
                        End If

                    End If
                End If
                Session("POPCG_Casename") = CaseName

                Session("PopCohort_year_run") = FindLastYear()
                Session("PopCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("PopCohort_tempyear"))
                Session("FirstYearSce") = FindFirstYear()

                If Session("PopCohort_year_run") <= FirstYearForecast Then
                    Session("PopCohort_tempyear") = FirstYearHistory
                ElseIf Session("PopCohort_tempyear") < FirstYearForecast Then
                    Session("PopCohort_tempyear") = FirstYearForecast
                End If
                'jrs 2012/06/27
                If Session("PopCohort_Grouping") = True Then
                    lstGroups_SelectedIndexChanged(Nothing, New System.EventArgs)
                End If
            End If
        End If


        'KIA 63008 ADD THE FOLLOWING IF BLOCK
        If Not Session("cNumber") Is Nothing Then
            'cntrylist.SelectedIndex = CInt(Session("cNumber")) - 1
            Session("PopCohort_tempregion") = Session("cNumber")
            Session.Remove("cNumber")
            cntrylist_SelectedIndexChanged(Me, New System.EventArgs)
        Else
            PackJScritpBuilder()
        End If

ErrHandler02:
        Exit Sub

    End Sub

    Private Sub lstGroups_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'Session("PopCohort_regionname") = Support.ListBoxSelectedIndexCollectionManager.GetText(lstGroups)
        'If Session("PopCohort_regionname") = "" Then Session("PopCohort_regionname") = lstGroups.Items(0).Text
        'lstGroups.Visible = False
        LoadRegions()

        If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If
    End Sub

    Public Sub MnuBaseCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'MnuBaseCase.Checked = True 
        'mnuWorkCase.Checked = False 
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        On Error GoTo ErrHandler03


        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'If objEA.Item.Caption = "Base Case" Then
        'jrs 2006/02/24 objSender.Groups("Change Scenario").Items("Base Case").IconImage = "checkbox.gif"
        'jrs 2006/02/24 objSender.Groups("Change Scenario").Items("Work Case").IconImage = "blank.gif"
        'jam 2021/07/03
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        'jrs 2008/06/06
        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            'jam 2021/07/03
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Next

        Case_of_Work = 0
        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        'jrs 2008/06/18
        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            'JAM 2019-12-08
            If Session("mnuSQLite") Is Nothing Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            Else

                If Not Session("mnuSQLite") = "True" Then
                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                Else
                    'Session("db2Path") = Session("PutFdrvRun") & "ifsPop.db"


                    'JAM 2020-05-04
                    Session("SQLiteDB") = "ifsPop.db"

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

                    'JAM 2020-05-04
                    Session("SQLiteDB") = "IFsPopHist.db"

                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                End If
            End If

        End If
        'mti WebForm Changes From July 05, 2004
        'popConn = CreateConnection(Session("db2Path"))
        'Session("popConn") = popConn
        'mti WebForm Changes From July 05, 2004

        'jrs 2008/06/16 changed from FirstYearForecast to FirstYear
        'jrs 2008/06/05
        If Session("PopCohort_tempyear") < Session("FirstYear") Then
            Session("PopCohort_tempyear") = Session("FirstYear")
        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And Session("PopCohort_tempyear") > LastYearHistory Then
            Session("PopCohort_tempyear") = FirstYearHistory
        End If
        Session("FirstYearSce") = Session("FirstYear")

        '    Session("PopCohort_year_run") = Session("LastYear")%
        '    Session("PopCohort_tempyear") = Session("FirstYear")%
        '    Session("PopCohort_tempregion") = 1
        '    Session("PopCohort_regionname") = GRPS$(1, 1)
        '    Session("PopCohort_GraphType") = 1
        CaseName = "Base Case"
        Session("POPCG_Casename") = CaseName
        'End If
        If Session("PopCohort_tempyear") = Session("FirstYear") Then objSender.Groups("Time").Items("Regress").Enabled = False
        If Session("PopCohort_tempyear") < Session("LastYear") Then objSender.Groups("Time").Items("Advance").Enabled = True
        If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If

ErrHandler03:
        Exit Sub

    End Sub


    '==========================frm_CohoDisp=============================
    Public Sub mnuConsgph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Session("PopCohort_GraphType") = 2
        GeneratePop_CohoInfo(Session("PopCohort_GraphType"))
        DrawConsumptionGraph()

    End Sub

    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/07/03
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        If objEA.Item.Caption = "Groups" Then
            'jam 2021/07/03
            objSender.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            '"Change Display"
            objSender.Groups("Change Display").Items(0).Caption = "Change Groups" '"Change Countries/Regions"
            Session("PopCohort_Grouping") = True

            'jam 2021/07/03
            Session("IsGroup") = True

            'lstGroups_SelectedIndexChanged(Nothing, New System.EventArgs)
            'jrs 2014/08/22
            Session("sDefReg") = Session("MMCountry")

            Session("PopCohort_regionname") = "World" 'jrs 2014/08/22 GroupNames(1)
            Session("PopCohort_tempregion") = 1
            'jrs 2011/05/05
            Session.Remove("MMCountry")

            'jrs 2011/09/22
            Session("MMGroup") = Session("PopCohort_regionname")
            'Support.FormManager.Show(Me, "frm_CohoInfDisp.aspx?Group=" & PrepareForURL(Session("PopCohort_regionname")))

            'jam 2021/07/03
            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_CohoInfDisp.aspx")
            vurl = FriendlyUrl.Href("~/frm_CohoInfDisp", CohoInfPrepareForFriendlyURL(Session("PopCohort_regionname")))
            Response.Redirect(vurl)




        Else

            'jam 2021/07/05
            objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")  '"checkbox.gif"
            objSender.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif")  '"blank.gif"
            objSender.Groups("Change Display").Items(0).Caption = "Change Countries/Regions"
            Session("PopCohort_Grouping") = False


            'jam 2021/07/03
            Session("IsGroup") = False

            'cntrylist_SelectedIndexChanged(Nothing, New System.EventArgs)
            'jrs 2014/08/21
            Dim sCtryCode As String = "AF"
            If Session("sDefReg") > "" Then
                sCtryCode = Session("sDefReg")
            ElseIf Session("PopCohort_tempregion") > 0 And IFsRegs.Item(Session("PopCohort_regionname")).Index = Session("PopCohort_tempregion") Then
                sCtryCode = IFsRegs.Item(Session("PopCohort_regionname")).ISO3166_2Code
            End If

            Session("PopCohort_regionname") = "Afghanistan"
            Session("PopCohort_tempregion") = 1

            'Support.FormManager.Show(Me, "frm_CohoInfDisp.aspx?Country=" & sCtryCode)

            'jam 2021/07/03
            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_CohoInfDisp.aspx")
            vurl = FriendlyUrl.Href("~/frm_CohoInfDisp", sCtryCode)
            Response.Redirect(vurl)


        End If


    End Sub


    '==========================frm_CohoDisp=============================
    Public Sub mnuAvgEdgph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Session("PopCohort_GraphType") = 3
        If GenerateEducationData() Then
            DrawAvgEducationGraph()
        End If
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnupopgph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Session("PopCohort_GraphType") = 1
        If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If
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
        hfTitle.Value = lbldata.Text
        'jrs 2011/11/16
        lblJS.Visible = False
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuWorkCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '--------------------------------------------------------------

        On Error GoTo ErrHandler04

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'jam 2021/07/03
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        'jrs 2008/06/06
        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            'jam 2021/07/03
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") ' "blank.gif"
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Next

        Case_of_Work = 1
        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

        'JAM 2019-12-15
        If Session("mnuSQLite") Is Nothing Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

        Else

            If Not Session("mnuSQLite") = "True" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"
            Else
                'Session("db2Path") = Session("PutFdrvRun") & "ifsPopWork.db"


                'JAM 2020-05-04
                Session("SQLiteDB") = "ifsPopWork.db"

                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

            End If
        End If

        Session("PopCohort_year_run") = FindLastYear()
        Session("PopCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("PopCohort_tempyear"))
        If Session("PopCohort_tempyear") < FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearForecast
        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And Session("PopCohort_tempyear") > LastYearHistory Then
            Session("PopCohort_tempyear") = FirstYearHistory
        End If
        Session("FirstYearSce") = Session("FirstYear")

        CaseName = "Working Scenario"
        Session("POPCG_Casename") = CaseName
        If Session("PopCohort_tempyear") = Session("FirstYear") Then
            'mnuRegress.Enabled = False
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("PopCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If
        If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If

ErrHandler04:
        Exit Sub

    End Sub

    '==========================frm_CohoDisp=============================
    Private Sub FillDataGrid(ByRef index As Short)
        Dim cc As Short
        Dim mfTotal As Double
        Dim McolTotal As Double
        Dim FcolTotal As Double
        McolTotal = 0
        FcolTotal = 0
        mfTotal = 0
        ''--------------------------------------------------------------

        'jrs 2008/06/24
        'This only works for 1-dimensional arrays
        'grdcohort.DataSource = Session("PC_TempDataBase")            
        'grdcohort.DataBind()

        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        grdcohort.TotalRows = 11
        grdcohort.RowCount = 11

        If index = 2 Then 'Consumption
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


        ''grdcohort.CtlRefresh() 

        'jrs 2008/04/17 added infants
        Dim FstCoh As Integer, rr As Integer
        FstCoh = 1
        Dim Denominator As Single

        For cc = FstCoh To Session("popCohoNum")

            rr = cc

            grdcohort(0, rr) = Session("popCohoInfoNames")(Session("popCohoNum") - cc)
            'cdv 2021/02/01 ****START**** Flips Data
            If bMaleFirst Then
                grdcohort(1, rr) = Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 1)

                grdcohort(2, rr) = Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 2)
            Else
                grdcohort(2, rr) = Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 1)

                grdcohort(1, rr) = Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 2)
            End If
            'cdv 2021/02/01 ****END****
            If index = 3 Then
                Denominator = Session("AvgEduMFToalValues")(Session("popCohoNum") - cc + 1, 1) + Session("AvgEduMFToalValues")(Session("popCohoNum") - cc + 1, 2)
                If Denominator > 0 Then
                    mfTotal = Math.Round(((Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 1) * Session("AvgEduMFToalValues")(Session("popCohoNum") - cc + 1, 1)) +
                        (Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 2) * Session("AvgEduMFToalValues")(Session("popCohoNum") - cc + 1, 2))) / Denominator, 2)
                Else
                    mfTotal = 0
                End If
                grdcohort(3, rr) = mfTotal
            Else
                mfTotal = mfTotal + (Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 1) + Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 2))
                McolTotal = McolTotal + Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 1)
                FcolTotal = FcolTotal + Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 2)
                grdcohort(3, rr) = Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 1) + Session("PC_TempDataBase")(Session("popCohoNum") - cc + 1, 2)
            End If

        Next cc

        grdcohort(0, 0) = "Age Cohort"
        If index = 2 Then
            grdcohort(1, 0) = "Consumption"
            grdcohort(1, Session("popCohoNum") + 2) = CStr(Math.Round(McolTotal, 2))
            Exit Sub
        Else
            'cdv 2021/02/01 ****START**** Flips Labels
            If bMaleFirst Then
                grdcohort(1, 0) = "Male"
                grdcohort(2, 0) = "Female"
                grdcohort(3, 0) = "Total"
            Else
                grdcohort(1, 0) = "Female"
                grdcohort(2, 0) = "Male"
                grdcohort(3, 0) = "Total"
            End If
            'cdv 2021/02/01 ****END****
        End If

        grdcohort(0, Session("popCohoNum") + 2) = "Grand Total"
        If index = 3 Then
            'cdv 2021/02/01 ****START**** Flips Data
            If bMaleFirst Then
                grdcohort(1, Session("popCohoNum") + 2) = CStr(Math.Round(DataVector(299), 2))
                grdcohort(2, Session("popCohoNum") + 2) = CStr(Math.Round(DataVector(300), 2))
            Else
                grdcohort(2, Session("popCohoNum") + 2) = CStr(Math.Round(DataVector(299), 2))
                grdcohort(1, Session("popCohoNum") + 2) = CStr(Math.Round(DataVector(300), 2))
            End If
            mfTotal = DataVector(299) * DataVector(297) + DataVector(298) * DataVector(300)
            grdcohort(3, Session("popCohoNum") + 2) = CStr(Math.Round(mfTotal / (DataVector(297) + DataVector(298)), 2))
        Else
            If bMaleFirst Then
                grdcohort(1, Session("popCohoNum") + 2) = CStr(Math.Round(McolTotal, 2))
                grdcohort(2, Session("popCohoNum") + 2) = CStr(Math.Round(FcolTotal, 2))
            Else
                grdcohort(2, Session("popCohoNum") + 2) = CStr(Math.Round(McolTotal, 2))
                grdcohort(1, Session("popCohoNum") + 2) = CStr(Math.Round(FcolTotal, 2))
                'cdv 2021/02/01 ****END****
            End If
            grdcohort(3, Session("popCohoNum") + 2) = CStr(Math.Round(mfTotal, 2))
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
        Session("PopCohort_tempregion") = GroupNum
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

        'jam 2021/07/03
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

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
            Case "Population Distribution"
                mnupopgph_Click(sender, e)
            Case "Consumption Distribution"
                mnuConsgph_Click(sender, e)
            Case "Average Education Distribution"
                mnuAvgEdgph_Click(sender, e)
            Case "Show Numbers"
                mnuShowData_Click(sender, e)
            Case "Use Groups"
            Case "Groups", "Countries/Regions"
                mnuGroups_Click(sender, e)
                    'SetGroupReg(sender, e,objEA.Item.Caption)
            Case "Change Display"
            Case "Change Countries/Regions", "Change Groups" ', "Change Region"
                    'jrs 2011/11/15 mnuchngcntry_Click(sender, e)
            Case "Set Year"
                    'jrs 2011/11/15 mnuchngyr_Click(sender, e)
            Case "Change Scenario"
                    'jrs 2012/05/04
                    'Case "Base Case"
                    '    MnuBaseCase_Click(sender, e)
                    'Case "Work Case"
                    '    mnuWorkCase_Click(sender, e)
                    'Case "Historical Forecast" 'jrs 2008/06/16
                    '    mnuHistSce_Click(objSender)
            'Case "Advance"
            '    mnuAdvance_Click(sender, e)
            'Case "Regress"
            '    mnuRegress_Click(sender, e)
            Case "Change Birth Cohort Category" 'jrs 2012/06/29
                mnuChngCategory_Click()
            Case "Print"
            Case "Save"
            Case "About" 'jrs 2025/02/04 Population"
                mnuAboutPopulation_Click(sender, e)
            Case Else
                'jrs 2011/11/14
                Dim idx As Integer, sUrl As String, sParY As String, sParF As String, sParG As String, pos As Integer
                If Not IsNothing(Request.QueryString("Year")) Then
                    sParY = "&Year=" & Request.QueryString("Year")
                Else
                    'jam 2021/07/03
                    sParY = ""


                    If Not IsNothing(Session("vYear")) Then

                        sParY = "&Year=" & Session("vYear")
                    Else
                        sParY = ""
                    End If

                End If
                If Not IsNothing(Request.QueryString("File")) Then
                    sParF = "&File=" & Request.QueryString("File")
                Else
                    'jam 2021/07/03
                    'sParF = ""

                    If Not IsNothing(Session("vScenario")) Then

                        sParF = "&File=" & Session("vScenario")
                    Else
                        sParF = ""
                    End If

                End If
                If Not IsNothing(Request.QueryString("Country")) Then
                    sParG = "Country=" & Request.QueryString("Country")
                ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2012/06/27
                    sParG = "Group=" & Request.QueryString("Group")
                ElseIf Session("PopCohort_Grouping") = False Then
                    sParG = "Country=" & IFsRegs.Item(Session("PopCohort_regionname")).ISO3166_2Code
                Else
                    sParG = "Group=" & Session("PopCohort_regionname")
                End If

                If objEA.Item.UserData = "Reg" Then
                    'Server Transfer doesn't change the URL, which we want to allow to be saved by user
                    Response.Redirect("frm_CohoInfDisp.aspx?Country=" & IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code & sParY & sParF)
                ElseIf objEA.Item.UserData = "Grp" Then
                    Response.Redirect("frm_CohoInfDisp.aspx?Group=" & PrepareForURL(objEA.Item.Caption) & sParY & sParF)
                ElseIf objEA.Item.UserData = "Yr" Then
                    'jrs 2012/05/4
                    'If IsNumeric(objEA.Item.Caption) Then
                    '    lstYear.SelectedIndex = CInt(CInt(Trim(objEA.Item.Caption)) - Session("FirstYear"))
                    '    lstYear_SelectedIndexChanged(objSender, New EventArgs)
                    'End If
                    Response.Redirect("frm_CohoInfDisp.aspx?" & sParG & "&Year=" & objEA.Item.Caption & sParF)
                ElseIf objEA.Item.Caption = "Advance" Then
                    'cdv 2021/07/06
                    If Session("PopCohort_tempyear") Mod 5 = 0 Then
                        Session("PopCohort_tempyear") = Session("PopCohort_tempyear") + 5
                    Else
                        While Session("PopCohort_tempyear") Mod 5 <> 0
                            Session("PopCohort_tempyear") += 1
                        End While
                    End If
                    If Session("PopCohort_tempyear") > Session("PopCohort_year_run") Then Session("PopCohort_tempyear") = Session("PopCohort_year_run")
                    Response.Redirect("frm_CohoInfDisp.aspx?" & sParG & "&Year=" & Session("PopCohort_tempyear") & sParF)
                ElseIf objEA.Item.Caption = "Regress" Then
                    'cdv 2021/07/06
                    If Session("PopCohort_tempyear") Mod 5 = 0 Then
                        Session("PopCohort_tempyear") = Session("PopCohort_tempyear") - 5
                    Else
                        While Session("PopCohort_tempyear") Mod 5 <> 0
                            Session("PopCohort_tempyear") -= 1
                        End While
                    End If
                    If Session("PopCohort_tempyear") < Session("FirstYearSce") Then Session("PopCohort_tempyear") = Session("FirstYearSce")
                    Response.Redirect("frm_CohoInfDisp.aspx?" & sParG & "&Year=" & Session("PopCohort_tempyear") & sParF)
                ElseIf objEA.Item.Caption = "Historical Forecast" Then
                    Response.Redirect("frm_CohoInfDisp.aspx?" & sParG & sParY & "&File=Historical Forecast")
                Else
                    'jrs 2012/05/04 mnuSce_Click(objEA.Item.Caption, objSender)
                    'jam 2021/07/03
                    'Response.Redirect("frm_CohoInfDisp.aspx?" & sParG & sParY & "&File=" & objEA.Item.Caption)


                    If Not IsNothing(Session("vYear")) Then

                        sParY = Session("vYear")
                    Else
                        sParY = ""
                    End If

                    'jam 2021/07/05
                    If Not IsNothing(objEA.Item.Caption) Then
                        'If Not IsNothing(Session("vScenario")) Then

                        sParF = objEA.Item.Caption

                        'sParF = Session("vScenario")
                        Session("POPCG_Casename") = sParF

                    Else
                        sParF = ""
                    End If


                    If Not IsNothing(Session("vCountry")) Then

                        sParG = Session("vCountry")
                    Else
                        sParG = ""
                    End If

                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_CohoInfDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_CohoInfDisp", sParG, sParY, objEA.Item.Caption)
                    Response.Redirect(vurl)

                End If
        End Select
    End Sub

    'jrs 2012/06/29
    Private Sub mnuChngCategory_Click()
        frameData.Visible = False
        grdcohort.Visible = False
        lbldata.Visible = False
        lblJS.Visible = False
        frameCategories.Visible = True
        'Load Categories
        Dim cc As Integer
        For cc = 0 To Session("popCohoNum") - 1
            Session("txtCatName")(cc).Text = Session("popCohoInfoDesc")(cc)
            Session("txtCatRange")(cc).Text = Session("popCohoInfoNames")(cc)
        Next

    End Sub

    Private Sub lstYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '--------------------------------------------------------------

        '  Specify the country selected
        '  ----------------------------
        'If lstYear.SelectedIndex = -1 Then
        '    Session("PopCohort_tempyear") = Session("FirstYearSce") '1
        'Else
        '    Session("PopCohort_tempyear") = Session("FirstYearSce") + lstYear.SelectedIndex 'cntrylist.SelectedIndex + 1
        'End If

        If Session("PopCohort_tempyear") < Session("FirstYearSce") Then Session("PopCohort_tempyear") = Session("FirstYearSce")
        If Session("PopCohort_tempyear") > Session("PopCohort_year_run") Then Session("PopCohort_tempyear") = Session("PopCohort_year_run")

        '   Allow or disallow forward and backward movement
        '   -----------------------------------------------

        If Session("PopCohort_tempyear") > Session("FirstYearSce") Then
            wmMain.Groups("Time").Items("Regress").Enabled = True
        Else
            wmMain.Groups("Time").Items("Regress").Enabled = False
        End If
        If Session("PopCohort_tempyear") < Session("PopCohort_year_run") Then
            wmMain.Groups("Time").Items("Advance").Enabled = True
        Else
            wmMain.Groups("Time").Items("Advance").Enabled = False
        End If
        Graph_Draw((Session("PopCohort_GraphType")))
    End Sub

    Private Function FindLastYear()
        'mti July 2006 - new routine
        Dim rsLY As DataTable
        'SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)
        'If Not rsLY.Rows.Count = 0 Then
        '    FindLastYear = rsLY.Rows(0).Item("MaxYear")
        'Else
        '    FindLastYear = 0
        'End If

        On Error GoTo ErrHandler05

        'JAM 2019-12-19
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

ErrHandler05:
        Exit Function



    End Function

    'jrs 2008/06/06
    Private Function FindFirstYear()
        'mti July 2006 - new routine
        Dim rsFY As DataTable
        'SetDataTablefromAccess(Session("db2Path"), "SELECT Min(Years) As MinYear FROM " + TableName(1), "", rsFY)
        'If Not rsFY.Rows.Count = 0 Then
        '    FindFirstYear = rsFY.Rows(0).Item("MinYear")
        'Else
        '    FindFirstYear = 0
        'End If

        On Error GoTo ErrHandler06
        'JAM 2019-12-19
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
ErrHandler06:
        Exit Function

    End Function

    'jrs 2008/06/05
    Private Sub LoadSceMenus(ByRef cswmGroup2 As Coalesys.WebMenu.Group)
        'Read all files with format ifsPopXXX.mdb located in runfiles
        Dim sfile As String
        sfile = Dir(Session("PutFdrvRun2") + "\IFsPop*.db")
        Dim cswmItem As Coalesys.WebMenu.Item

        Do While (sfile <> "")
            If UCase(sfile) <> "IFSPOPWORK.DB" And UCase(sfile) <> "IFSPOP.DB" And UCase(sfile) <> "IFSPOPHIST.DB" Then 'jrs 2008/06/16
                If Session("IFsRunFiles").item(Mid(Replace(Replace(sfile, ".db", ""), ".DB", ""), 7) & ".run.db").Visible Then
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = Mid(Replace(Replace(sfile, ".db", ""), ".DB", ""), 7)
                    cswmItem.IconHeight = 16
                    If Session("POPCG_Casename") = cswmItem.Caption Then
                        'jam 2021/07/03
                        cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") ' "checkbox.gif"
                    Else
                        'jam 2021/07/05
                        cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                    End If
                End If
            End If
            sfile = Dir()
        Loop
    End Sub

    'jrs 2008/06/05
    Private Sub mnuSce_Click(ByVal sSceName As String, ByRef objSender As Coalesys.WebMenu.WebMenu)
        'jam 2021/07/03
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"

        On Error GoTo ErrHandler05

        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            If objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).Caption = sSceName Then
                'jam 2021/07/03
                objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                Case_of_Work = i + 2
            Else
                'jam 2021/07/05
                objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            End If
        Next

        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        CaseName = sSceName
        Session("POPCG_Casename") = CaseName

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"


        'JAM 2020-02-12
        If Session("mnuSQLite") Is Nothing Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"

        Else

            If Not Session("mnuSQLite") = "True" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"
            Else
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".db"

                'JAM 2020-05-04
                Session("SQLiteDB") = "IFsPop" & sSceName & ".db"

                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

            End If
        End If



        Session("PopCohort_year_run") = FindLastYear()
        Session("PopCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("PopCohort_tempyear"))
        Session("FirstYearSce") = FindFirstYear()

        If Session("PopCohort_year_run") <= FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearHistory
        ElseIf Session("PopCohort_tempyear") < FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearForecast
        End If

        If Session("PopCohort_tempyear") = Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("PopCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If

        If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If


ErrHandler05:
        Exit Sub
    End Sub

    'jrs 2008/06/05
    Private Sub mnuHistSce_Click(ByRef objSender As Coalesys.WebMenu.WebMenu)

        Dim i As Integer
        On Error GoTo ErrHandler06

        For i = 0 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            'jam 2021/07/03
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Next

        CaseName = "Historical Forecast"
        Session("POPCG_Casename") = CaseName

        ' Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"


        'JAM 2020-02-12
        If Session("mnuSQLite") Is Nothing Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

        Else

            If Not Session("mnuSQLite") = "True" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
            Else
                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                'JAM 2020-05-04
                Session("SQLiteDB") = "IFsPopHist.db"

                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

            End If
        End If


        Session("PopCohort_year_run") = FindLastYear()
        Session("PopCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("PopCohort_tempyear"))
        Session("FirstYearSce") = FindFirstYear()

        If Session("PopCohort_year_run") <= FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearHistory
        ElseIf Session("PopCohort_tempyear") < FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearForecast
        End If

        If Session("PopCohort_tempyear") = Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("PopCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If
        If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If

ErrHandler06:
        Exit Sub

    End Sub

    'jrs 2008/06/24
    Private Sub mnuExit_Click()
        'jam 2021/07/03
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        Session.Remove("PC_TempDataBase")
        Session.Remove("POPM")
        Session.Remove("POPF")
        Session.Remove("POPMales")
        Session.Remove("POPFemales")
        Session.Remove("POPCG_Case_of_Work")

        Session.Remove("GroupTot")
        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If

        'jam 2021/07/03
        'Support.FormManager.Show(Me, Session("DisplayParent"))


        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/" & Session("DisplayParent"))

        vurl = FriendlyUrl.Href("~/" & Session("DisplayParent"))
        Response.Redirect(vurl)

    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()

        On Error GoTo ErrHandler07

        Dim iNCohortsWD As Integer = NCohorts
        iNCohortsWD = Session("popCohoNum")

        Dim iNCohorts As Integer

        Dim Ages() As String
        iNCohorts = Session("popCohoNum")
        ReDim Ages(iNCohorts)
        Dim cc As Integer
        For cc = 1 To iNCohorts
            Ages(cc) = "[" & Session("popCohoInfoDesc")(cc - 1) & "] " & Session("popCohoInfoNames")(cc - 1)
        Next

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

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/12/06 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        'sJscript2 = sJscript2 & "fontFamily: 'Arial, sans-serif'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        'sJscript2 = sJscript2 & "color: '#333333' " & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''


        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/04 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "chart: {" & vbCr
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

        Dim Index As Short
        Dim graphtitle As String = ""
        Dim regionname As String
        Dim CaseName As String
        Dim TempYear As Short
        Index = Session("PopCG_Index")
        regionname = Session("POPCG_Regionname")
        CaseName = Session("POPCG_Casename")
        TempYear = Session("POPCG_tempyear")
        If Index = 1 Then
            graphtitle = "Birth Cohort"
        ElseIf Index = 2 Then
            graphtitle = "Consumption"
        ElseIf Index = 3 Then
            graphtitle = "Average Education"
        End If
        graphtitle = graphtitle & " Distribution for " & regionname & " in Year " & Str(TempYear) & " [" & CaseName & "]"

        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06  Increasing font size for legends
        'sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr
        If Index = 4 Then
            sJscript2 = sJscript2 & "x: 25 //center" & vbCr
        Else
            sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr

        'jam 2025/12/05 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr



        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Age Categories'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06 Increasing font for x axis
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "categories: mycategories, " & vbCr
        sJscript2 = sJscript2 & "reversed: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxis As String = ""
        If Index = 2 Then
            sAxis = "Consumption in Billions"
        ElseIf Index = 3 Then
            sAxis = "Average Years of Education of those 15 Years and Older"
        Else
            sAxis = "Population in Millions"
        End If

        sJscript2 = sJscript2 & "yAxis: {" & vbCr

        'jam 2025/12/06 Increasing font for y axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr


        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06 Increasing font for x axis
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "x: -50," & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxis & "'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return Math.abs(this.value);" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/12/06 Increasing font for y axis
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        'cdv 2021/02/01 flip legend
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "reversed: true"
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<b>' + this.series.name + ', age ' + this.point.category + '</b><br/>' +"
        sJscript2 = sJscript2 & "'Population: ' + Highcharts.numberFormat(Math.abs(this.point.y), 2);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        'cdv 2021/02/01 ****START**** flip data entry for gender

        'Males
        If bMaleFirst And Index <> 2 Then
            sJscript2 = sJscript2 & PlotColToJSSeries(2, Session("PC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
        Else
            sJscript2 = sJscript2 & PlotColToJSSeries(1, Session("PC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
        End If
        'Females stored in Index 1 for Fertility
        If Index <> 2 And bMaleFirst Then
            'Females
            sJscript2 = sJscript2 & "," & PlotColToJSSeries(1, Session("PC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
        ElseIf Index <> 2 Then
            sJscript2 = sJscript2 & "," & PlotColToJSSeries(2, Session("PC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
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

        'If Not (Session("VLYear") Is Nothing) Then
        '    sJscript2 = sJscript2 & "chart.renderer.path(['m', " & CStr((Session("VLYear") - Session("FirstYear")) * 70 + 100) & ", 0, 'V', 800])" & vbCr
        '    sJscript2 = sJscript2 & ".attr({" & vbCr
        '    sJscript2 = sJscript2 & "'stroke-width': 1," & vbCr
        '    sJscript2 = sJscript2 & "stroke:         'red'," & vbCr
        '    sJscript2 = sJscript2 & "zIndex:1000" & vbCr
        '    sJscript2 = sJscript2 & "})" & vbCr
        '    sJscript2 = sJscript2 & ".add();" & vbCr
        'End If


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
        End If

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

ErrHandler07:
        Exit Sub

    End Sub

    Private Function LoadIncConSavTable() As Boolean
        Dim resSrc As DataTable
        Dim tmpDB As String
        Dim ind As Integer
        ind = 0
        tmpDB = Session("FdrvRun") & "IFs.mdb"
        SetDataTablefromAccess(tmpDB, "SELECT * FROM IncConSav", "", resSrc, "")

        Dim rr As Integer = 0
        If resSrc.Rows.Count > 0 Then

            ReDim Session("IncConSav")(resSrc.Rows.Count, 7)
            While ind < resSrc.Columns.Count
                Session("IncConSav")(0, ind) = resSrc.Columns(ind).Caption
                If ind = 0 Then
                    ind = ind + 2
                Else
                    ind = ind + 1
                End If
            End While
            ind = 1
            rr = rr + 1

            While Not resSrc.Rows.Count = rr
                Session("IncConSav")(ind, 0) = resSrc.Rows(rr).Item("Age Category")
                Session("IncConSav")(ind, 1) = resSrc.Rows(rr).Item("Income MDCs")
                Session("IncConSav")(ind, 2) = resSrc.Rows(rr).Item("Consumption  MDCs")
                Session("IncConSav")(ind, 3) = resSrc.Rows(rr).Item("Savings  MDCs")
                Session("IncConSav")(ind, 4) = resSrc.Rows(rr).Item("Income LDCs")
                Session("IncConSav")(ind, 5) = resSrc.Rows(rr).Item("Consumption  LDCs")
                Session("IncConSav")(ind, 6) = resSrc.Rows(rr).Item("Savings  LDCs")
                ' Denominator = Denominator + IncConSav(ind, 2)
                ind = ind + 1
                rr = rr + 1
            End While
            ' Denominator = 5 * Denominator
        End If
        LoadIncConSavTable = resSrc.Rows.Count > 0
    End Function

    'KIA 11/02/2009 ADDED THE FOLLOWING UITILITY METHOD TO start and end years from a string
    Public Function RetrieveYearsFromCagegories(ByVal currentCatNums As Integer, ByVal cohoNames As Object) As Integer()
        ReDim allYears(((currentCatNums - 2) * 2) + 2)
        Dim indx, counter As Integer
        counter = 0
        For indx = 0 To currentCatNums - 1
            If indx = 0 Then
                allYears(counter) = CInt(Right(cohoNames(indx), 4))

            ElseIf indx = Session("popCohoNum") - 1 Then
                If Len(cohoNames(indx)) > 5 Or InStr(cohoNames(indx), "+") = 0 Then
                    allYears(counter) = CInt(Right(cohoNames(indx), 4))
                Else
                    allYears(counter) = CInt(Left(cohoNames(indx), 4))
                End If
            Else
                allYears(counter) = CInt(Left(cohoNames(indx), 4))
                counter = counter + 1
                allYears(counter) = CInt(Right(cohoNames(indx), 4))
            End If
            counter = counter + 1
        Next indx
        RetrieveYearsFromCagegories = allYears
    End Function

    Private Function GeneratePop_CohoInfo(ByVal idx As Integer) As Boolean
        Dim resSrc, resDst As DataTable

        Dim indx, tmpindx As Integer

        Dim v_count As Integer

        'jam 2021/07/03
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        PopByAge = 0
        v_count = 0
        On Error GoTo trapped
        'JAM 2019-12-20
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            If Session("PopCohort_Grouping") = False Then
                SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopBaseCase where Years = " &
                Str(Session("PopCohort_tempyear")) & " and NCohorts > 0 and Region = " & Str(Session("PopCohort_tempregion")),
                "", resSrc, "")
            Else
                SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopBaseCase where Region in (" & sRegs & ") and Years = " &
                Str(Session("PopCohort_tempyear")) & " and NCohorts > 0",
                "", resSrc, "")
            End If
            'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopCohoInfo", "", resDst, "")
            ReDim Session("PopCohoInfo")(resSrc.Rows.Count * Session("popCohoNum"), 8)


        Else 'JAM 2019-12-19

            If Session("PopCohort_Grouping") = False Then
                '    SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopBaseCase where Years = " &
                'Str(Session("PopCohort_tempyear")) & " and NCohorts > 0 and Region = " & Str(Session("PopCohort_tempregion")),
                '"", resSrc, "")

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                'jrs 2025/02/04
                vSQLiteCommand.CommandText = "SELECT COUNT(*) FROM Pop_Base_Case where TimeId = " &
                        Str(Session("PopCohort_tempyear")) & " and CohortId > 0 and RegionId = " & Str(Session("PopCohort_tempregion"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                vSQLiteReader.Read()
                v_count = vSQLiteReader(0)
                vSQLiteReader.Close()

                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case where TimeId = " &
                        Str(Session("PopCohort_tempyear")) & " and CohortId > 0 and RegionId = " & Str(Session("PopCohort_tempregion"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                'jrs 2025/02/04 this moves the reader to the end, need to put it back at the begining
                'Do While (vSQLiteReader.Read())
                '    v_count = v_count + 1
                'Loop


            Else
                '    SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopBaseCase where Region in (" & sRegs & ") and Years = " &
                'Str(Session("PopCohort_tempyear")) & " and NCohorts > 0",
                '"", resSrc, "")

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                'jrs 2025/02/04
                vSQLiteCommand.CommandText = "SELECT COUNT(*) FROM Pop_Base_Case where RegionId in (" & sRegs & ") and TimeId = " &
                            Str(Session("PopCohort_tempyear")) & " and CohortId > 0"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                vSQLiteReader.Read()
                v_count = vSQLiteReader(0)
                vSQLiteReader.Close()

                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case where RegionId in (" & sRegs & ") and TimeId = " &
                            Str(Session("PopCohort_tempyear")) & " and CohortId > 0"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                'jrs 2025/02/04 this moves the reader to the end, need to put it back at the begining
                'Do While (vSQLiteReader.Read())
                '    v_count = v_count + 1
                'Loop


            End If
            'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopCohoInfo", "", resDst, "")
            ReDim Session("PopCohoInfo")(v_count * Session("popCohoNum"), 8)

        End If



        Dim cats, catE, N, cury, chN As Integer
        Dim rr1 As Integer = 0, rr2 As Integer = 0
        'jrs 2013/05/21
        Dim chN2 As Integer



        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            Do
                If resSrc.Rows.Count = rr1 Then
                    Exit Do
                End If
                cats = resSrc.Rows(rr1).Item("Years")
                chN = resSrc.Rows(rr1).Item("NCohorts")
                'jrs 2013/05/21
                If chN > 21 Then
                    chN2 = 21
                Else
                    chN2 = chN
                End If

                PopByAge = PopByAge + (Session("IncConSav")(chN2, 2) * (resSrc.Rows(rr1).Item("PopulationMales") + resSrc.Rows(rr1).Item("PopulationFemales"))) 'jrs 2013/05/21
                N = (chN - 1) * 5
                catE = ((chN - 1) * 5 + 4)
                While N <= catE
                    cury = cats - N

                    Session("PopCohoInfo")(rr2, 1) = cats 'Years
                    Session("PopCohoInfo")(rr2, 2) = cury 'YearBorn
                    Session("PopCohoInfo")(rr2, 3) = resSrc.Rows(rr1).Item("Region") 'Region
                    Session("PopCohoInfo")(rr2, 4) = resSrc.Rows(rr1).Item("PopulationMales") / 5.0 'PopulationMales
                    Session("PopCohoInfo")(rr2, 5) = resSrc.Rows(rr1).Item("PopulationFemales") / 5.0 'PopulationFemales
                    Session("PopCohoInfo")(rr2, 6) = Session("IncConSav")(chN2, 2) * (Session("PopCohoInfo")(rr2, 4) + Session("PopCohoInfo")(rr2, 5)) 'Consumption MDCs 'jrs 2013/05/21

                    If cury < allYears(0) Then
                        Session("PopCohoInfo")(rr2, 7) = Session("popCohoInfoNames")(0) 'CohortName
                        Session("PopCohoInfo")(rr2, 8) = 0 'NCohorts
                    ElseIf cury >= allYears(UBound(allYears) - 1) Then
                        Session("PopCohoInfo")(rr2, 7) = Session("popCohoInfoNames")(Session("popCohoNum") - 1) 'CohortName
                        Session("PopCohoInfo")(rr2, 8) = Session("popCohoNum") - 1 'NCohorts
                    Else
                        tmpindx = 1
                        For indx = 1 To UBound(allYears) - 2
                            If cury >= allYears(indx) And cury <= allYears(indx + 1) Then
                                Session("PopCohoInfo")(rr2, 7) = Session("popCohoInfoNames")(tmpindx) 'CohortName
                                Session("PopCohoInfo")(rr2, 8) = tmpindx 'NCohorts
                                Exit For
                            End If
                            tmpindx = tmpindx + 1
                            indx = indx + 1
                        Next indx
                    End If
                    rr2 = rr2 + 1
                    N = N + 1
                End While
                rr1 = rr1 + 1
            Loop



        Else 'JAM 2019-12-20
            ''vSQLiteReader = vSQLiteCommand.ExecuteReader()

            Do While (vSQLiteReader.Read())

                'If resSrc.Rows.Count = rr1 Then
                '    Exit Do
                'End If
                'cats = resSrc.Rows(rr1).Item("Years")
                'chN = resSrc.Rows(rr1).Item("NCohorts")
                cats = vSQLiteReader("TimeId")
                chN = vSQLiteReader("CohortId")

                'jrs 2013/05/21
                If chN > 21 Then
                    chN2 = 21
                Else
                    chN2 = chN
                End If

                PopByAge = PopByAge + (Session("IncConSav")(chN2, 2) * (vSQLiteReader("PopulationMales") + vSQLiteReader("PopulationFemales"))) 'jrs 2013/05/21
                N = (chN - 1) * 5
                catE = ((chN - 1) * 5 + 4)
                While N <= catE
                    cury = cats - N

                    Session("PopCohoInfo")(rr2, 1) = cats 'Years
                    Session("PopCohoInfo")(rr2, 2) = cury 'YearBorn
                    Session("PopCohoInfo")(rr2, 3) = vSQLiteReader("RegionId") 'Region
                    Session("PopCohoInfo")(rr2, 4) = vSQLiteReader("PopulationMales") / 5.0 'PopulationMales
                    Session("PopCohoInfo")(rr2, 5) = vSQLiteReader("PopulationFemales") / 5.0 'PopulationFemales
                    Session("PopCohoInfo")(rr2, 6) = Session("IncConSav")(chN2, 2) * (Session("PopCohoInfo")(rr2, 4) + Session("PopCohoInfo")(rr2, 5)) 'Consumption MDCs 'jrs 2013/05/21

                    If cury < allYears(0) Then
                        Session("PopCohoInfo")(rr2, 7) = Session("popCohoInfoNames")(0) 'CohortName
                        Session("PopCohoInfo")(rr2, 8) = 0 'NCohorts
                    ElseIf cury >= allYears(UBound(allYears) - 1) Then
                        Session("PopCohoInfo")(rr2, 7) = Session("popCohoInfoNames")(Session("popCohoNum") - 1) 'CohortName
                        Session("PopCohoInfo")(rr2, 8) = Session("popCohoNum") - 1 'NCohorts
                    Else
                        tmpindx = 1
                        For indx = 1 To UBound(allYears) - 2
                            If cury >= allYears(indx) And cury <= allYears(indx + 1) Then
                                Session("PopCohoInfo")(rr2, 7) = Session("popCohoInfoNames")(tmpindx) 'CohortName
                                Session("PopCohoInfo")(rr2, 8) = tmpindx 'NCohorts
                                Exit For
                            End If
                            tmpindx = tmpindx + 1
                            indx = indx + 1
                        Next indx
                    End If
                    rr2 = rr2 + 1
                    N = N + 1
                End While
                rr1 = rr1 + 1
            Loop





        End If

        GeneratePop_CohoInfo = True
        If (Session("PopCohort_tempregion") <> previousRegion Or preCName <> CaseName) And Session("PopCohort_GraphType") = 2 Then
            FetchConsumptionVariable()
        End If
        Session("PopByAge") = PopByAge
        Exit Function
trapped:
        GeneratePop_CohoInfo = False
        LoadMessage(Err.Description, Session("Message"))

        'jam 2021/07/03
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")


        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
        vurl = FriendlyUrl.Href("~/frm_Message")
        Response.Redirect(vurl)


    End Function

    Private Function GenerateEducationData() As Boolean
        Dim resSrc, resSrc2 As DataTable
        Dim indx, tmpindx As Integer
        Dim v_count As Integer

        'jam 2021/07/03
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        PopAcrossAge = 0
        v_count = 0

        On Error GoTo trapped_edu

        'JAM 2019-12-21
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            If Session("PopCohort_Grouping") = False Then
                SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM EdnYears15BaseCase where Years = " &
                    Str(Session("PopCohort_tempyear")) & " and NCohorts > 3 and Region = " & Str(Session("PopCohort_tempregion")),
                    "", resSrc, "")

                SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopBaseCase where Years = " &
                     Str(Session("PopCohort_tempyear")) & " and NCohorts > 3 and Region = " & Str(Session("PopCohort_tempregion")),
                      "", resSrc2, "")
            Else
                SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM EdnYears15BaseCase where Region in (" & sRegs & ") and Years = " &
                    Str(Session("PopCohort_tempyear")) & " and NCohorts > 3",
                    "", resSrc, "")

                SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopBaseCase where Region in (" & sRegs & ") and Years = " &
                    Str(Session("PopCohort_tempyear")) & " and NCohorts > 3",
                    "", resSrc2, "")
            End If
            'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM EdnYears15AvgCase", "", resDst, "")

        Else
            If Session("PopCohort_Grouping") = False Then
                'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM EdnYears15BaseCase where Years = " &
                'Str(Session("PopCohort_tempyear")) & " and NCohorts > 3 and Region = " & Str(Session("PopCohort_tempregion")),
                '"", resSrc, "")

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM EdnYears15BaseCase where TimeId = " &
                            Str(Session("PopCohort_tempyear")) & " and CohortId > 3 and RegionId = " & Str(Session("PopCohort_tempregion"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())

                    v_count = v_count + 1
                Loop
                'jrs 2026/01/09
                vSQLiteReader.Close()
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopBaseCase where Years = " &
                'Str(Session("PopCohort_tempyear")) & " and NCohorts > 3 and Region = " & Str(Session("PopCohort_tempregion")),
                '"", resSrc2, "")


                vSQLiteCommand2 = vSQLiteCon.CreateCommand()
                vSQLiteCommand2.CommandText = "SELECT * FROM Pop_Base_Case where TimeId = " &
                            Str(Session("PopCohort_tempyear")) & " and CohortId > 3 and RegionId = " & Str(Session("PopCohort_tempregion"))
                vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()


            Else
                'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM EdnYears15BaseCase where Region in (" & sRegs & ") and Years = " &
                'Str(Session("PopCohort_tempyear")) & " and NCohorts > 3",
                '"", resSrc, "")

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM EdnYears15BaseCase where RegionId in (" & sRegs & ") and TimeId = " &
                                Str(Session("PopCohort_tempyear")) & " and CohortId > 3"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    v_count = v_count + 1
                Loop
                'jrs 2026/01/09
                vSQLiteReader.Close()
                vSQLiteReader = vSQLiteCommand.ExecuteReader()



                'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM PopBaseCase where Region in (" & sRegs & ") and Years = " &
                'Str(Session("PopCohort_tempyear")) & " and NCohorts > 3",
                '"", resSrc2, "")

                vSQLiteCommand2 = vSQLiteCon.CreateCommand()
                vSQLiteCommand2.CommandText = "SELECT * FROM Pop_Base_Case where RegionId in (" & sRegs & ") and TimeId = " &
                            Str(Session("PopCohort_tempyear")) & " and CohortId > 3"
                vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()

            End If
        End If

        Dim cats, catE, N, cury, chN As Integer
        Dim rr1 As Integer = 0, rr2 As Integer = 0, rr3 As Integer = 0

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            ReDim Session("TmpEdnYears15AvgCase")(resSrc.Rows.Count * 5, 7)

            'If resDst.RecordCount > 0 Then
            '    While Not resDst.EOF
            '        resDst.Edit()
            '        resDst.Delete()
            '        resDst.MoveNext()
            '    End While
            'End If
            'allYears = RetrieveYearsFromCagegories

            Do
                If resSrc.Rows.Count = rr1 Then
                    Exit Do
                End If
                cats = resSrc.Rows(rr1).Item("Years") ' selected year year
                chN = resSrc.Rows(rr1).Item("NCohorts")

                N = (chN - 1) * 5
                catE = ((chN - 1) * 5 + 4)
                While N <= catE
                    cury = cats - N

                    Session("TmpEdnYears15AvgCase")(rr3, 1) = cury 'YearBorn
                    Session("TmpEdnYears15AvgCase")(rr3, 2) = resSrc.Rows(rr1).Item("Region") 'Region
                    Session("TmpEdnYears15AvgCase")(rr3, 3) = resSrc.Rows(rr1).Item("EdnYears15Males") * resSrc2.Rows(rr2).Item("PopulationMales") / 5.0 'EdnYears15Males
                    ' PopAcrossAge = PopAcrossAge + (resSrc2("PopulationMales").value / 5#) + (resSrc2("PopulationFemales").value / 5#)
                    Session("TmpEdnYears15AvgCase")(rr3, 4) = resSrc.Rows(rr1).Item("EdnYears15Females") * resSrc2.Rows(rr2).Item("PopulationFemales") / 5.0 'EdnYears15Females
                    Session("TmpEdnYears15AvgCase")(rr3, 5) = (resSrc2.Rows(rr2).Item("PopulationMales") / 5.0) 'PopMTotal
                    Session("TmpEdnYears15AvgCase")(rr3, 6) = (resSrc2.Rows(rr2).Item("PopulationFemales") / 5.0) 'PopFTotal

                    If cury < allYears(0) Then
                        Session("TmpEdnYears15AvgCase")(rr3, 7) = 0 'NCohorts
                    ElseIf cury >= allYears(UBound(allYears) - 1) Then
                        Session("TmpEdnYears15AvgCase")(rr3, 7) = Session("popCohoNum") - 1 'NCohorts
                    Else
                        tmpindx = 1
                        For indx = 1 To UBound(allYears) - 2
                            If cury >= allYears(indx) And cury <= allYears(indx + 1) Then
                                Session("TmpEdnYears15AvgCase")(rr3, 7) = tmpindx 'NCohorts
                                Exit For
                            End If
                            tmpindx = tmpindx + 1
                            indx = indx + 1
                        Next indx
                    End If

                    '      If cury > 2010 Then
                    '
                    '        resDst("NCohorts").value = 4
                    '      ElseIf cury >= 1985 And cury <= 2010 Then
                    '
                    '        resDst("NCohorts").value = 3
                    '      ElseIf cury >= 1965 And cury <= 1984 Then
                    '
                    '       resDst("NCohorts").value = 2
                    '      ElseIf cury >= 1945 And cury <= 1964 Then
                    '
                    '       resDst("NCohorts").value = 1
                    '      ElseIf cury < 1945 Then
                    '
                    '       resDst("NCohorts").value = 0
                    '      End If


                    N = N + 1
                    rr3 = rr3 + 1
                End While
                rr1 = rr1 + 1
                rr2 = rr2 + 1
            Loop

        Else 'JAM 2019-12-21
            ReDim Session("TmpEdnYears15AvgCase")(v_count * 5, 7)
            Dim x As Integer
            Dim y As Integer

            x = 0
            y = 0

            'vSQLiteReader = vSQLiteCommand.ExecuteReader()
            'vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()

            While (vSQLiteReader.Read())
                x = x + 1
                y = 0
                While (vSQLiteReader2.Read())
                    y = y + 1
                    'If resSrc.Rows.Count = rr1 Then
                    '    Exit Do
                    'End If
                    If x = y Then
                        cats = vSQLiteReader("TimeId") ' selected year year
                        chN = vSQLiteReader("CohortId")

                        N = (chN - 1) * 5
                        catE = ((chN - 1) * 5 + 4)
                        While N <= catE
                            cury = cats - N

                            Session("TmpEdnYears15AvgCase")(rr3, 1) = cury 'YearBorn
                            Session("TmpEdnYears15AvgCase")(rr3, 2) = vSQLiteReader("RegionId") 'Region
                            Session("TmpEdnYears15AvgCase")(rr3, 3) = vSQLiteReader("EdnYears15Males") * vSQLiteReader2.Item("PopulationMales") / 5.0 'EdnYears15Males
                            ' PopAcrossAge = PopAcrossAge + (resSrc2("PopulationMales").value / 5#) + (resSrc2("PopulationFemales").value / 5#)
                            Session("TmpEdnYears15AvgCase")(rr3, 4) = vSQLiteReader("EdnYears15Females") * vSQLiteReader2("PopulationFemales") / 5.0 'EdnYears15Females
                            Session("TmpEdnYears15AvgCase")(rr3, 5) = (vSQLiteReader2("PopulationMales") / 5.0) 'PopMTotal
                            Session("TmpEdnYears15AvgCase")(rr3, 6) = (vSQLiteReader2("PopulationFemales") / 5.0) 'PopFTotal

                            If cury < allYears(0) Then
                                Session("TmpEdnYears15AvgCase")(rr3, 7) = 0 'NCohorts
                            ElseIf cury >= allYears(UBound(allYears) - 1) Then
                                Session("TmpEdnYears15AvgCase")(rr3, 7) = Session("popCohoNum") - 1 'NCohorts
                            Else
                                tmpindx = 1
                                For indx = 1 To UBound(allYears) - 2
                                    If cury >= allYears(indx) And cury <= allYears(indx + 1) Then
                                        Session("TmpEdnYears15AvgCase")(rr3, 7) = tmpindx 'NCohorts
                                        Exit For
                                    End If
                                    tmpindx = tmpindx + 1
                                    indx = indx + 1
                                Next indx
                            End If



                            N = N + 1
                            rr3 = rr3 + 1
                        End While
                        rr1 = rr1 + 1
                        rr2 = rr2 + 1
                    Else

                    End If
                End While
                'jrs 2026/01/09
                vSQLiteReader2.Close()
                vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()
            End While


        End If

        GenerateEducationData = True
        Exit Function
trapped_edu:
        GenerateEducationData = False
        LoadMessage(Err.Description, Session("Message"))
        'jam 2021/07/03
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")

        'jam 2021/07/03
        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
        vurl = FriendlyUrl.Href("~/frm_Message")
        Response.Redirect(vurl)


    End Function

    Private Sub LoadRegions()
        sRegs = ""
        LoadCountries(Session("PopCohort_regionname"))

        ReDim Session("POPM")(NCohorts)
        ReDim Session("POPF")(NCohorts)
        ReDim Session("POPMales")(Session("GroupTot"), NCohorts)
        ReDim Session("POPFemales")(Session("GroupTot"), NCohorts)

        Dim rId As Integer
        For rId = 1 To Session("GroupTot")
            If sRegs = "" Then
                sRegs = Str(Session("RegionIdGroup")(rId))
            Else
                sRegs = sRegs & "," & Str(Session("RegionIdGroup")(rId))
            End If
        Next rId
        Session("sRegs") = sRegs
    End Sub

    Private Sub FetchConsumptionVariable()


        Dim fileIndex
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

        Dim isGroup As Boolean
        isGroup = Session("PopCohort_Grouping")
        previousRegion = Session("PopCohort_tempregion")
        preCName = CaseName
        fileIndex = 1
        If Case_of_Work >= 0 And Case_of_Work <= 2 Then
            fileIndex = 1
        Else
            fileIndex = Case_of_Work
        End If
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("C", -1, Session("PopCohort_regionname"), -1, "", -1, "", -1, "", fileIndex, True, True, isGroup, 1, DataVector,
                Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"),
                Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Else
            gLoadForDisplay("C", -1, Session("PopCohort_regionname"), -1, "", -1, "", -1, "", fileIndex, True, True, isGroup, 1, DataVector,
                Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"),
                Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
        Session("DataVector") = DataVector
        ' always considering first run file

    End Sub

    'jrs 2012/06/29
    Protected Sub cmdCancelCats_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelCats.Click
        lblJS.Visible = True
        frameCategories.Visible = False
    End Sub

    'jrs 2012/06/29
    Protected Sub cmdSaveCats_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveCats.Click
        If CheckData() Then
            lblJS.Visible = True
            frameCategories.Visible = False

            Dim cc As Integer
            Session("popCohoNum") = 10
            For cc = 0 To 9
                Session("popCohoInfoDesc")(cc) = Session("txtCatName")(cc).Text
                Session("popCohoInfoNames")(cc) = Session("txtCatRange")(cc).Text

                If Session("txtCatName")(cc).Text = "" Then
                    Session("popCohoNum") = cc
                    Exit For
                End If
            Next

            allYears = RetrieveYearsFromCagegories(Session("popCohoNum"), Session("popCohoInfoNames"))
            If Session("PopCohort_GraphType") = 1 Or Session("PopCohort_GraphType") = 2 Then
                If GeneratePop_CohoInfo(Session("PopCohort_GraphType")) Then
                    Graph_Draw(Session("PopCohort_GraphType"))
                End If
            Else
                If GenerateEducationData() Then
                    Graph_Draw(Session("PopCohort_GraphType"))
                End If
            End If

        End If
    End Sub

    'jrs 2012/06/29
    Private Function CheckData() As Boolean
        Dim x As Integer
        Dim col1, col2 As String
        For x = 0 To 9
            col1 = Session("txtCatName")(x).Text
            col2 = Session("txtCatRange")(x).Text
            If col1 <> "" Or col2 <> "" Then
                If col1 <> "" And col2 <> "" Then
                    If Not CheckIfYear(x, col2) Then
                        CheckData = False
                        Exit Function
                    ElseIf Not CheckDuplicateNames(x, col1) Then
                        lblCatMsg.Text = "Category names can not be duplicate."
                        CheckData = False
                        Exit Function
                    End If
                Else
                    lblCatMsg.Text = "For Category " + CStr(x) + " Both Category Name and Correct Value Range Required."
                    CheckData = False
                    Exit Function
                End If
            End If
        Next x
        CheckData = True

    End Function

    Private Function CheckIfYear(ByVal rn As Integer, ByVal col2 As String) As Boolean

        Dim indx As Integer
        Dim LastYear
        Dim comLogic As Boolean
        lblCatMsg.Text = ""
        Dim iLastRow As Integer = 9
        If rn < 9 Then
            If Session("txtCatRange")(rn + 1).text = "" Then
                iLastRow = rn
            End If
        End If

        If rn = 0 Then
            indx = InStr(Trim(col2), " ")
            If Not IsInteger(Right(col2, Len(col2) - indx)) Then
                lblCatMsg.Text = "Year For Category " & CStr(rn) & " Is Not Valid."
                CheckIfYear = False
                Exit Function
            End If
        ElseIf rn = iLastRow Then
            If Not IsInteger(Right(Trim(Session("txtCatRange")(rn - 1).text), 4)) Then
                lblCatMsg.Text = "Year For Category " & CStr(rn) & " Must Be A Valid Year Value."
                CheckIfYear = False
                Exit Function
            End If
            LastYear = CInt(Right(Trim(Session("txtCatRange")(rn - 1).text), 4))
            If Len(col2) > 5 Then
                If InStr(col2, "-") = 0 Then
                    lblCatMsg.Text = "Year For Category " & CStr(rn) & " Must Be In A Format Of xxxx-xxxx or xxxx+ if it is the last category. "
                    CheckIfYear = False
                    Exit Function
                End If
            End If
            If Len(col2) = 5 Then
                If Right(col2, 1) <> "+" Then
                    lblCatMsg.Text = "Year For Category " & CStr(rn) & " Must Be In A Format Of xxxx+"
                    CheckIfYear = False
                    Exit Function
                End If
            End If

            If Not IsInteger(Left(col2, 4)) Then
                lblCatMsg.Text = "Year For Category " + CStr(rn) + " Must Be A Valid Year Value."
                CheckIfYear = False
                Exit Function
            End If

            If CInt(Left(col2, 4)) <= LastYear Then
                lblCatMsg.Text = "Year For Category " + CStr(rn) + " Must Be Greater Than End Year of Category " + CStr(rn - 1)
                CheckIfYear = False
                Exit Function
            End If
        ElseIf InStr(col2, "-") = 0 Then
            lblCatMsg.Text = "Year For Category " + CStr(rn) + " Must Be Seperated By A Dash (-)."
            CheckIfYear = False
            Exit Function
        Else
            indx = InStr(Trim(col2), "-")
            If Not IsInteger(Left(col2, indx - 1)) Or Not IsInteger(Right(col2, Len(col2) - indx)) Then
                lblCatMsg.Text = "Start and End Year For Category " + CStr(rn) + " Must Be A Valid Year Value."
                CheckIfYear = False
                Exit Function
            Else
                LastYear = CInt(Right(Trim(Session("txtCatRange")(rn - 1).text), 4))
                indx = InStr(Trim(col2), "-")
                If rn = 1 Then
                    comLogic = CInt(Left(col2, indx - 1)) < LastYear
                Else
                    comLogic = CInt(Left(col2, indx - 1)) <= LastYear
                End If
                If comLogic Then
                    lblCatMsg.Text = "Start Year For Category " + CStr(rn) + " Must Be Greater Than End Year of Category " + CStr(rn - 1)
                    CheckIfYear = False
                    Exit Function
                End If

            End If
        End If

        CheckIfYear = True
    End Function

    Public Function IsInteger(ByVal str As String) As Boolean
        Dim i As Byte
        Dim CharAscii As Integer
        Dim Count As Byte

        For i = 1 To Len(str)
            CharAscii = Asc(Mid(str, i, 1))
            If (CharAscii > 47 And CharAscii < 58) Then
                Count = Count + 1
            Else
                If i = 1 Then
                    If CharAscii <> 43 And CharAscii <> 45 Then
                        IsInteger = False
                        Exit For
                    End If
                Else
                    IsInteger = False
                    Exit For
                End If
                Count = Count + 1
            End If
        Next

        If Count = Len(str) Then
            If Val(str) > 32767 Or Val(str) < -32768 Then
                IsInteger = False
            Else
                IsInteger = True
            End If
        End If
    End Function

    Private Function CheckDuplicateNames(ByVal rn As Integer, ByVal cname As String) As Boolean
        Dim ind As Integer
        ind = 0
        While ind < 9
            If ind <> rn And UCase(Session("txtCatName")(ind).text) = UCase(cname) Then
                CheckDuplicateNames = False
                Exit Function
            End If
            ind = ind + 1
        End While

        CheckDuplicateNames = True
    End Function

    Private Function PlotColToJSSeries(ByVal i As Integer, ByRef Vals As Object, ByVal bShow100 As Boolean, ByVal Idx As Integer,
                              ByVal gphType As MortType, ByVal CompIdx As Integer, ByVal iNCohortsWD As Integer,
                              ByVal bAllZero As Boolean) As String
        Dim iNCohorts As Integer
        If bShow100 Then
            iNCohorts = iNCohortsWD 'jrs2013/08/16 NCohorts
        Else
            iNCohorts = iNCohortsWD - 1 'jrs2013/08/16NCohorts - 1
        End If

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i, Idx, gphType) & """, color:""" & getColor(i, Idx, gphType)
        If Idx = 2 Then
            sJscript = sJscript & """, showInLegend: false, data:[" & vbCr
        Else
            sJscript = sJscript & """, data:[" & vbCr
        End If

        Dim bLeftPyramid As Boolean = False, colIdx As Integer, iLowCohort As Integer
        colIdx = i
        iLowCohort = 1
        bLeftPyramid = False

        Dim j As Integer
        Dim bFirst As Boolean = True
        If Session("bCompare") And CompIdx > 0 And CompIdx < 5 And (Session("AboutParent") = "frm_EdnCohoDisp.aspx" Or Session("AboutParent") = "frm_MortCohorts.aspx" Or Session("AboutParent") = "frm_PopCohortDisp.aspx") Then 'jrs 2014/02/12
            For j = iLowCohort To iNCohorts
                'jrs 2014/10/15
                If Not IsDBNull(Vals(j, colIdx, CompIdx)) Then
                    'jrs 2013/03/29 health pyramid, for comm dis. is messed up when aids is 0 in higher ages, it shows up big
                    'it might only happen in stacked pyramids
                    If Vals(j, colIdx, CompIdx) > 0 Then
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
            For j = iLowCohort To iNCohorts
                'jrs 2013/01/03
                If Not IsDBNull(Vals(j, colIdx)) Then
                    'jrs 2014/02/21 added condition to check if all are 0
                    'jrs 2013/03/29 
                    If Vals(j, colIdx) > 0 Or (j = iNCohorts And bAllZero) Then 'jrs 2013/08/19 added or j=iNCohorts, problems when Males are all 0s
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
                    'jrs 2013/03/29
                    'sJscript = sJscript & "<point name = """ & Ages(j) & _
                    '    """ y=""0""/>" & vbCr
                End If
            Next j
        End If

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Private Function getSeriesName(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Legend As String
        If idx <> 2 Then
            If ii = 1 Then
                Legend = "Males"
            Else
                Legend = "Females"
            End If
        Else
            Legend = ""
        End If
        getSeriesName = Legend
    End Function

    Private Function getColor(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Color As String
        If ii = 1 Then
            Color = "Blue"
        Else
            Color = "Lime"
        End If

        getColor = Color
    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/06/27
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function CohoInfPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        CohoInfPrepareForFriendlyURL = tmp
    End Function
End Class
