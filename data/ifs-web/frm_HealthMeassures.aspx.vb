Imports System.Data.SQLite

Imports IFs.Data
Imports System.Drawing

Partial Class frm_HealthMeassures
    Inherits Page

#Region " Web Form Designer Generated Code "
    'JAM 2019-12-26
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vConnString As String

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        'jrs 2005/04/22
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
        End If

        'jrs 11/08/04
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub


#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'jrs 2005/10/13
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Grouping Options"
        cswmGroup1.ID = "mnuGroups"
        If Session("bUseReg") Is Nothing Then
            Session("bUseReg") = True
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Countries/Regions"
        cswmItem.ID = "mnuReg"
        If Session("bUseReg") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Groups"
        cswmItem.ID = "mnuGroup"
        If Session("bUseGroup") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using Decomposed Groups"
        cswmItem.ID = "mnuGroupDec"
        If Session("bUseGroupDec") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Using G-Lists"
        cswmItem.ID = "mnuGList"
        If Session("bUseGList") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Label Options"
        cswmGroup1.ID = "mnuLblOpt"
        If lblJS.Visible Then
            cswmGroup1.ShowMenuBarItem = True
        Else
            cswmGroup1.ShowMenuBarItem = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use First Dimension Only"
        If Session("DimLbl") = "1" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Second Dimension Only"
        If Session("DimLbl") = "2" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Third Dimension Only"
        If Session("DimLbl") = "3" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Scenario Names Only"
        If Session("DimLbl") = "sce" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Alias for Group Names"
        If Session("bAlias") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Edit Labels"
        If Session("DimLbl") = "cust" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Reset Labels"
        cswmItem.IconImage = "blank.gif"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Change Discount Rate"
        cswmGroup1.ID = "mnuDiscRt"

        If Session("DiscountRt") Is Nothing Then
            Session("DiscountRt") = 0.03
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Discount Rate (" & CStr(Val(Session("DiscountRt")) * 100) & "%)"
        cswmItem.ID = "mnuDiscRtChg"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Age Weighting"
        cswmGroup1.ID = "mnuAgeWg"
        If Session("bUseAgeWg") Is Nothing Then
            Session("bUseAgeWg") = True
            Session("AWBeta") = 0.04
            Session("AWC") = 0.1658
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuAgeWgOn"
        If Session("bUseAgeWg") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuAgeWgOff"
        If Not Session("bUseAgeWg") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Beta (" & Session("AWBeta") & ", " & Session("AWC") & ")"
        cswmItem.IconImage = "blank.gif"
        cswmItem.ID = "mnuAgeWgBeta"

        'jrs 2013/09/04
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")


        'JAM 2019-12-26
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                ' vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db" + ";" 'Version=3;

                If Not Session("SQLiteDB") Is Nothing Then 'JAM 2020-04-13
                    vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                Else
                    vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                End If


                vSQLiteCon = New SqliteConnection(vConnString) '& "New=true;"
                vSQLiteCon.Open()
            End If
        End If

        'JAM 2019-12-07
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                If Not Session("SQLiteDB") Is Nothing Then 'JAM 2020-04-13
                    vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                Else
                    vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                End If

                vSQLiteCon = New SqliteConnection(vConnString)
                vSQLiteCon.Open()
            End If
        End If



        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            'Redimension array to fit all cohorts, and as many region,gender,type combinations as possible
            ReDim Session("mLegends")(7, MaxVariables)
            Session("DimLbl") = ""
            'Load Geografic List
            LoadDim1s()
            'Load Mortality Types
            Dim idx As Integer
            idx = IfsVars.Item("DEATHCAT").Index
            Dim DimNum As Integer
            DimNum = VDim2(idx)
            Dim i As Integer
            For i = 1 To MaxVal(DimNum) - 1
                lstDim2.Items.Add(GRPS$(DimNum, i))
            Next i
            lstDim2.Items.Add("TotComDis")
            lstDim2.Items.Add("TotNonComDis")
            lstDim2.Items.Add("TotInjuries")
            lstDim2.Items.Add("Total")
            lstDim2.Items(lstDim2.Items.Count - 1).Selected = True
            'Load Gender
            DimNum = VDim3(idx)
            'cdv 2021/02/01 Flip Gender Load Order for labels
            If bMaleFirst Then
                For i = 1 To MaxVal(DimNum) - 1
                    lstDim3.Items.Add(GRPS$(DimNum, i))
                Next i
            Else
                lstDim3.Items.Add(GRPS$(DimNum, 2))
                lstDim3.Items.Add(GRPS$(DimNum, 1))
                lstDim3.Items.Add(GRPS$(DimNum, 3))
            End If
            lstDim3.Items(lstDim3.Items.Count - 1).Selected = True

            lstVars.Items.Add("HLYLL")
            lstVars.Items.Add("HLYLD")
            lstVars.Items.Add("HLDALY")
            lstVars.Items.Add("HLYLLWORK")
            lstVars.Items.Add("HLYLDWORK")
            lstVars.Items.Add("HLDALYWORK")
            lstVars.Items(0).Selected = True

            'Load Files
            LoadFiles()
            'Load Years
            Dim db As String

            'JAM 2020-06-01
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                db = Session("PutFdrvRun") & "ifsPopWork.mdb"
            End If
            Dim rs As DataTable

            'JAM 2020-05-19
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                SetDataTablefromAccess(db, "SELECT MAX(Years) as MaxYrs FROM MorDetBaseCase", "", rs)
                Session("iLastYear") = rs.Rows(0).Item("MaxYrs")

            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT MAX(TimeId) as MaxYrs FROM Mor_Det_Base_Case"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())
                    Session("iLastYear") = vSQLiteReader("MaxYrs")

                End While


            End If

            ReDim Session("mPLOT")(Session("iLastYear") - Session("FirstYear") + 1, MaxVariables)
            'grdHL.Rows = Session("iLastYear") - Session("FirstYear") + 6

            LoadGlobalVariables()

            'jrs 2009/12/14
            'jrs 2012/08/01 Session("AWBeta") = 0.04
            'jrs 2012/08/01 Session("AWC") = 0.1658
            '94.13521, integral from 0 to 100
            'Int(CXe^(-BX))dX = (-X/B-1/B^2)*e^(-BX)*C
            Session("AWArea") = (-100 / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * 100) * Session("AWC")
            Session("AWArea") = Session("AWArea") - (-0 / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-1 * Session("AWBeta") * 0) * Session("AWC")


            'jrs 2005/04/19
            ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)
            If Not (Session("PDFilesIdxs")(0) Is Nothing) Then
                Dim fileIdx As Integer
                For fileIdx = 0 To lstFiles.Items.Count - 1
                    lstFiles.Items(fileIdx).Selected = Session("PDFilesIdxs")(fileIdx)
                Next fileIdx
            End If

            'jrs 2011/11/04
            If Not Session("txtFiles") Is Nothing Then
                txtFiles.Value = Session("txtFiles")
            End If

        End If
        lstFiles.Attributes.Add("onclick", "lstFiles_Click();")
        lstFiles.Attributes.Add("onmousedown", "lstFiles_ClickDown();")
        lstFiles.Attributes.Add("onmouseup", "lstFiles_ClickUp();")

        If lblJS.Visible Then
            PackJScritpBuilder()
        End If

        txtEditLbl.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdUpdEditLbl.ClientID + "')")
        txtAWBeta.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdSaveAWBeta.ClientID + "')")
        txtDiscRt.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdSaveDiscRt.ClientID + "')")


    End Sub

    Private Sub LoadFiles()
        'jrs 2005/02/21 changed First Year to First Year Forecast, if History plus Forecast
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            Session("VarSelCheck") = Nothing
        End If
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = Session("LastYearForecast") Then
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
                Session("VarSelCheckHis") = Nothing 'jrs 2005/10/12
            Else
                Session("LastYear") = 2050 'LastYearHistory
            End If
        End If

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim fs 'As New FileSystemObject
        Dim BASE 'As File
        Dim currRun 'As File
        fs = CreateObject("Scripting.FileSystemObject")
        Dim sWorkingFile As String
        sWorkingFile = Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))

        'JRS 05/20/03
        'cdv 2021/02/02 ****START****  .run and .run.db now load correctly
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = LastYearHistory Then
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBaseHist.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBaseHist.run")
            End If
        Else
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run")
            End If
        End If
        'jrs 2005/05/09
        lstFiles.Items.Clear()

        lstFiles.Items.Add("0 - " + "Working File, based on " + sWorkingFile)
        lstFiles.Items.Add("1 - " + BASE.Name)
        'jrs 2005/02/28
        Session("FilesSelected") = 1

        Dim sRunFile As String
        ' Returns filename with specified extension. If more than one *.ini
        ' file exists, the first file found is returned.
        sRunFile = Dir(Session("PutFdrvRun2") + "\*.run")

        Do While (sRunFile <> "")
            If UCase(sRunFile) <> UCase(BASE.Name) And
            UCase(sRunFile) <> UCase(sWorkingFile) And
            UCase(sRunFile) <> "WORKING.RUN" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") + "\" + sRunFile)
                'If BASE.size = currRun.size Then
                'jrs 2022/04/08 added Or StandAloneMode
                If Session("IFsRunFiles").item(sRunFile).Visible Or StandAloneMode Then 'jrs 2005/06/27
                    lstFiles.Items.Add(Trim(Str(lstFiles.Items.Count)) + " - " + sRunFile)
                    Session("FilesSelected") = Session("FilesSelected") + 1
                    ReDim Preserve Session("RunFiles")(Session("FilesSelected"))
                    Session("RunFiles")(Session("FilesSelected")) = Session("PutFdrvRun2") + "\" + sRunFile
                    'jrs 2005/02/25
                    If Session("FilesSelected") > UBound(Session("ShortSce")) Then
                        ReDim Preserve Session("ShortSce")(Session("FilesSelected"))
                    End If
                    Dim k As Integer
                    k = getLongSceIdx(sRunFile)
                    If k > -1 Then
                        Session("ShortSce")(Session("FilesSelected")) = ShortSce(k)
                    Else
                        'jrs 2008/05/13 Session("ShortSce")(Session("FilesSelected")) = Left(sRunFile, 8)
                        Session("ShortSce")(Session("FilesSelected")) = TruncateSceFileName(sRunFile)
                    End If
                End If
                'End If
            End If
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            sRunFile = Dir()
        Loop
        'jrs 2026/01/26 changed default to base
        lstFiles.SelectedIndex = 1
        lstFiles.Items(1).Selected = True
    End Sub



    Private Sub cmdTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        If Not lblJS.Visible Then
            LoadSet()
        Else
            lblJS.Visible = False
        End If
        pnlMort.Visible = False
        cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = False
        If Session("TotalGraphs") > 0 Then
            LoadGrid()
        End If

        cmdDraw.Enabled = True
        cmdTable.Enabled = False
    End Sub


    Private Sub LoadGrid()
        Dim j As Integer, i As Integer, r(5) As TableRow, ce As TableCell

        'Load Legends
        For j = 1 To Session("TotalGraphs")
            For i = 1 To 5
                If j = 1 Then
                    r(i - 1) = New TableRow
                    ce = New TableCell
                    ce.Text = ""
                    ce.BackColor = System.Drawing.Color.Gray
                    r(i - 1).Cells.Add(ce)
                End If
                ce = New TableCell
                ce.Text = Session("mLegends")(i, j)
                ce.BackColor = System.Drawing.Color.Gray
                r(i - 1).Cells.Add(ce)
            Next i
            '.row = 3
            '.CellAlignment = flexAlignLeftCenter
        Next j

        With grdHL
            '   Fill in top of table
            '   --------------------
            .Rows.Clear()
            For i = 1 To 5
                .Rows.Add(r(i - 1))
            Next

            'Load Years
            ReDim Preserve r(Session("iLastYear") - Session("FirstYear") + 1 + 5)
            For i = Session("FirstYear") To Session("iLastYear")
                r(i - Session("FirstYear") + 5) = New TableRow
                ce = New TableCell
                ce.Text = Str(i)
                ce.BackColor = System.Drawing.Color.Gray
                r(i - Session("FirstYear") + 5).Cells.Add(ce)
            Next i

            'Load Mortality Values
            For j = 1 To Session("TotalGraphs")
                For i = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    ce = New TableCell
                    Select Case Session("mPLOT")(i, j)
                        Case 100 To 1000
                            ce.Text = Format(Session("mPLOT")(i, j), "###,###.0")
                        Case 10 To 100
                            ce.Text = Format(Session("mPLOT")(i, j), "###,###.00")
                        Case 0 To 10
                            ce.Text = Format(Session("mPLOT")(i, j), "###,###.000")
                        Case Else
                            ce.Text = Format(Session("mPLOT")(i, j), "###,###,###,###")
                    End Select
                    ce.HorizontalAlign = HorizontalAlign.Right
                    r(i - 1 + 5).Cells.Add(ce)
                Next i
            Next j

            For i = 1 + 5 To Session("iLastYear") - Session("FirstYear") + 1 + 5
                .Rows.Add(r(i - 1))
            Next

            '.Visible = True
        End With

        Dim dt As New DataTable()
        Dim dcol As New DataColumn()

        'If (GridView1.Rows.Count > 0) Then
        '    ClientScript.RegisterStartupScript(Me.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'GridView1', 'HeaderDiv');</script>")
        'End If

        For j = 0 To Session("TotalGraphs")
            dcol = New DataColumn(Str(j))
            dt.Columns.Add(dcol)
        Next j

        For i = 5 To Session("iLastYear") - Session("FirstYear") + 5
            Dim dr As DataRow = dt.NewRow

            For j = 0 To Session("TotalGraphs")
                dr(Str(j)) = grdHL.Rows(i).Cells(j).Text
            Next
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next i

        GridView1.DataSource = dt
        GridView1.DataBind()
        For j = 0 To Session("TotalGraphs")
            If j = 0 Then
                'JAM 2012-05-13	Adding blank space because cell borders had a different look
                GridView1.HeaderRow.Cells(j).Text = Server.HtmlDecode("&nbsp;</br>") '""
                ' GridView1.Columns(j).HeaderText = "Year"
            Else
                GridView1.HeaderRow.Cells(j).Text = Server.HtmlDecode(grdHL.Rows(0).Cells(j).Text & "</br>" & grdHL.Rows(1).Cells(j).Text & "</br>" & grdHL.Rows(2).Cells(j).Text & "</br>" & grdHL.Rows(3).Cells(j).Text & "</br>" & grdHL.Rows(4).Cells(j).Text)
            End If
        Next j

        'JAM 2012-04-03 Adding Formatting for control
        For i = 5 To (Session("iLastYear") - Session("FirstYear") + 5)
            For j = 0 To Session("TotalGraphs")
                'JAM 2012-04-04 
                If j = 0 Then
                    GridView1.Rows(i - 5).Cells(j).BackColor = Color.LightGray
                Else
                    If grdHL.Rows(i).Cells(j).Font.Bold Then
                        GridView1.Rows(i - 5).Cells(j).Text = Server.HtmlDecode("<span style='color: #000000;'><b>" & grdHL.Rows(i).Cells(j).Text & "</b></span>")
                    End If
                End If
            Next j
        Next i

        If Session("TotalGraphs") <= 3 Then
            If Session("TotalGraphs") = 3 Then
                GridView1.Width = Unit.Percentage(35)
            Else
                GridView1.Width = Unit.Percentage(25)
            End If
        End If
        GridView1.Visible = True

        'ClientScript.RegisterStartupScript(Me.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'GridView1', 'HeaderDiv');</script>")
    End Sub

    'jrs 2005/10/13
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        Dim sAWBeta As String
        sAWBeta = "Change Beta (" & Session("AWBeta") & ", " & Session("AWC") & ")"
        Dim sDiscRt As String
        sDiscRt = "Discount Rate (" & CStr(Val(Session("DiscountRt")) * 100) & "%)"

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                cmdClear_Click(Me, New EventArgs)
            Case "Using Countries/Regions"
                mnuReg_Click()
            Case "Using Groups"
                mnuGroup_Click()
            Case "Using Decomposed Groups"
                mnuGroupDec_Click()
            Case "Using G-Lists"
                mnuGList_Click()
            Case "On"
                mnuAgeWgOn_Click()
            Case "Off"
                mnuAgeWgOff_Click()
            Case sAWBeta
                mnuAgeWBeta_Click()
            Case sDiscRt
                mnuDiscRt_Click()
            Case "Use First Dimension Only"
                Session("DimLbl") = "1"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Second Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Third Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Edit Labels").IconImage = "blank.gif"
            Case "Use Second Dimension Only"
                Session("DimLbl") = "2"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Second Dimension Only").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Third Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Edit Labels").IconImage = "blank.gif"
            Case "Use Third Dimension Only"
                Session("DimLbl") = "3"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Second Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Third Dimension Only").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Edit Labels").IconImage = "blank.gif"
            Case "Use Scenario Names Only"
                Session("DimLbl") = "sce"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Second Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Third Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Edit Labels").IconImage = "blank.gif"
            Case "Reset Labels"
                Session.Remove("DimLbl")
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Second Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Third Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Edit Labels").IconImage = "blank.gif"
            Case "Use Alias for Group Names"
                mnuAlias_Click()
            Case "Edit Labels"
                mnulblEdit_Click()
        End Select

        If lblJS.Visible Then
            PackJScritpBuilder()
        End If
    End Sub


    Protected Sub lstFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        'jrs 2011/11/04
        Session("txtFiles") = txtFiles.Value
    End Sub

    Private Sub LoadDim1s()
        lstDim1.Items.Clear()
        Dim db As String
        Dim valtbl As DataTable
        Dim rr As Integer
        If (Session("bUseGroup") Or Session("bUseGroupDec")) Then
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "", valtbl)
            rr = 0
            While Not valtbl.Rows.Count = rr
                lstDim1.Items.Add(valtbl.Rows(rr).Item("GroupName"))
                rr = rr + 1
            End While
        ElseIf Session("bUseGList") Then
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)
            rr = 0
            While Not valtbl.Rows.Count = rr
                lstDim1.Items.Add(valtbl.Rows(rr).Item("Description"))
                rr = rr + 1
            End While
        Else
            Dim i As Integer
            For i = 1 To MaxVal(1) - 1
                lstDim1.Items.Add(GRPS$(1, i))
            Next i
        End If
        If lstDim1.Items.Count > 0 Then
            lstDim1.Items(0).Selected = True
        End If
    End Sub

    Private Sub LoadGlobalVariables()
        'Find Maximum Life Expectancy
        '----------------------------------------
        'jrs 2010/03/19
        '    MaxLIFEXP = 0
        '    Dim r As Integer
        '    Dim LIFEXP() As Single
        '    ReDim LIFEXP(1) As Single
        '    For r = 1 To NR
        '        gLoadForDisplay "LIFEXP", -1, GRPS(1, r), r, "Total", -1, "", -1, "", 1, _
        '        r = 1, r = NR, False, 1, LIFEXP, 1, 1, FirstYear, FirstYear, 0, _
        '        True, "", False
        '
        '        If MaxLIFEXP < LIFEXP(1) Then
        '            MaxLIFEXP = LIFEXP(1)
        '        End If
        '    Next r

        'Compute Average Age per Cohort
        '-----------------------------------------
        ReDim Session("AvgAgePerCohort")(NCohorts)
        Session("AvgAgePerCohort")(0) = 0.1 'jrs 2010/03/19 used to be 0.5
        Session("AvgAgePerCohort")(1) = 2.6 'jrs 2010/03/19 used to be 2.5
        Session("AvgAgePerCohort")(2) = 7.5 'jrs 2010/03/19 used to be 7
        Session("AvgAgePerCohort")(NCohorts - 2) = 92
        Session("AvgAgePerCohort")(NCohorts - 1) = 96.5
        Session("AvgAgePerCohort")(NCohorts) = 102.5
        Dim j As Integer
        For j = 3 To NCohorts - 3
            'jrs 2010/03/19
            '        If MaxLIFEXP >= (j - 1) * 5 And MaxLIFEXP < j * 5 Then
            '            AvgAgePerCohort(j) = (MaxLIFEXP + (j - 1) * 5) / 2
            '            MaxLifeExpCohort = j
            '            MaxLifeExpCohortPortion = (MaxLIFEXP - (j - 1) * 5) / 5
            '            Exit For
            '        Else
            Session("AvgAgePerCohort")(j) = Session("AvgAgePerCohort")(j - 1) + 5
            '        End If
        Next j

        'mnuDiscount.Caption = "Change Discount Rate (" & CStr(Session("DiscountRt") * 100) & "%)"


        'jrs 2010/03/19
        ReDim Session("StdLifeExp")(NCohorts, 2)

        'Loading Standard Life Expectancy for Males
        '------------------------------------------
        Session("StdLifeExp")(0, 1) = 79.94
        Session("StdLifeExp")(1, 1) = 77.77
        Session("StdLifeExp")(2, 1) = 72.89
        Session("StdLifeExp")(3, 1) = 67.91
        Session("StdLifeExp")(4, 1) = 62.93
        Session("StdLifeExp")(5, 1) = 57.95
        Session("StdLifeExp")(6, 1) = 52.99
        Session("StdLifeExp")(7, 1) = 48.04
        Session("StdLifeExp")(8, 1) = 43.1
        Session("StdLifeExp")(9, 1) = 38.2
        Session("StdLifeExp")(10, 1) = 33.38
        Session("StdLifeExp")(11, 1) = 28.66
        Session("StdLifeExp")(12, 1) = 24.07
        Session("StdLifeExp")(13, 1) = 19.65
        Session("StdLifeExp")(14, 1) = 15.54
        Session("StdLifeExp")(15, 1) = 11.87
        Session("StdLifeExp")(16, 1) = 8.81
        Session("StdLifeExp")(17, 1) = 6.34
        Session("StdLifeExp")(18, 1) = 4.39
        Session("StdLifeExp")(19, 1) = 3.05
        Session("StdLifeExp")(20, 1) = 2.06
        Session("StdLifeExp")(21, 1) = 1.06

        'Loading Standard Life Expectancy for Females
        '--------------------------------------------
        Session("StdLifeExp")(0, 2) = 82.43
        Session("StdLifeExp")(1, 2) = 80.28
        Session("StdLifeExp")(2, 2) = 75.47
        Session("StdLifeExp")(3, 2) = 70.51
        Session("StdLifeExp")(4, 2) = 65.55
        Session("StdLifeExp")(5, 2) = 60.63
        Session("StdLifeExp")(6, 2) = 55.72
        Session("StdLifeExp")(7, 2) = 50.83
        Session("StdLifeExp")(8, 2) = 45.96
        Session("StdLifeExp")(9, 2) = 41.13
        Session("StdLifeExp")(10, 2) = 36.36
        Session("StdLifeExp")(11, 2) = 31.68
        Session("StdLifeExp")(12, 2) = 27.1
        Session("StdLifeExp")(13, 2) = 22.64
        Session("StdLifeExp")(14, 2) = 18.32
        Session("StdLifeExp")(15, 2) = 14.24
        Session("StdLifeExp")(16, 2) = 10.59
        Session("StdLifeExp")(17, 2) = 7.56
        Session("StdLifeExp")(18, 2) = 5.24
        Session("StdLifeExp")(19, 2) = 3.71
        Session("StdLifeExp")(20, 2) = 2.62
        Session("StdLifeExp")(21, 2) = 1.55

        'Load Mortality Types
        '---------------------

        'Communicable Diseases
        Session("CommDis") = New MortType
        Session("CommDis").setNumSubTypes(10)
        Session("CommDis").sTitle = "Communicable Diseases"
        Session("CommDis").setSubTypeFieldName("ComDisNoAIDSMales", 1)
        Session("CommDis").setSubTypeName("Other Communicable Diseases", 1)
        Session("CommDis").setSubTypeColor(RGB(255, 215, 0), 1)
        Session("CommDis").setTypeIdx(1, 1)
        Session("CommDis").setSubTypeFieldName("ComDisNoAIDSFemales", 2)
        Session("CommDis").setSubTypeColor(RGB(255, 215, 0), 2)
        Session("CommDis").setTypeIdx(1, 2)
        Session("CommDis").setSubTypeFieldName("AIDSMales", 3)
        Session("CommDis").setSubTypeName("AIDS", 3)
        Session("CommDis").setSubTypeColor(RGB(255, 0, 0), 3)
        Session("CommDis").setTypeIdx(11, 3)
        Session("CommDis").setSubTypeFieldName("AIDSFemales", 4)
        Session("CommDis").setSubTypeColor(RGB(255, 0, 0), 4)
        Session("CommDis").setTypeIdx(11, 4)
        'jrs 2009/08/14
        Session("CommDis").setSubTypeFieldName("DiarrheaMales", 5)
        Session("CommDis").setSubTypeName("Diarrhea", 5)
        Session("CommDis").setSubTypeColor(RGB(133, 94, 66), 5) 'Dark Wood
        Session("CommDis").setTypeIdx(12, 5)
        Session("CommDis").setSubTypeFieldName("DiarrheaFemales", 6)
        Session("CommDis").setSubTypeColor(RGB(133, 94, 66), 6)
        Session("CommDis").setTypeIdx(12, 6)
        Session("CommDis").setSubTypeFieldName("MalariaMales", 7)
        Session("CommDis").setSubTypeName("Malaria", 7)
        Session("CommDis").setSubTypeColor(RGB(0, 255, 0), 7)
        Session("CommDis").setTypeIdx(13, 7)
        Session("CommDis").setSubTypeFieldName("MalariaFemales", 8)
        Session("CommDis").setSubTypeColor(RGB(0, 255, 0), 8)
        Session("CommDis").setTypeIdx(13, 8)
        Session("CommDis").setSubTypeFieldName("RespInfMales", 9)
        Session("CommDis").setSubTypeName("Respiratory Infections", 9)
        Session("CommDis").setSubTypeColor(RGB(65, 105, 225), 9)
        Session("CommDis").setTypeIdx(14, 9)
        Session("CommDis").setSubTypeFieldName("RespInfFemales", 10)
        Session("CommDis").setSubTypeColor(RGB(65, 105, 225), 10)
        Session("CommDis").setTypeIdx(14, 10)

        'Non Communicable Diseases
        Session("NonCommDis") = New MortType
        Session("NonCommDis").setNumSubTypes(12)
        Session("NonCommDis").sTitle = "Non Communicable Diseases"
        Session("NonCommDis").setSubTypeFieldName("MaligNeoPlasMales", 1)
        Session("NonCommDis").setSubTypeName("Malignant Neoplasms", 1)
        Session("NonCommDis").setSubTypeColor(RGB(255, 215, 0), 1)
        Session("NonCommDis").setTypeIdx(2, 1)
        Session("NonCommDis").setSubTypeFieldName("MaligNeoPlasFemales", 2)
        Session("NonCommDis").setSubTypeColor(RGB(255, 215, 0), 2)
        Session("NonCommDis").setTypeIdx(2, 2)
        Session("NonCommDis").setSubTypeFieldName("CardioVascMales", 3)
        Session("NonCommDis").setSubTypeName("Cardio Vascular", 3)
        Session("NonCommDis").setSubTypeColor(RGB(65, 105, 225), 3)
        Session("NonCommDis").setTypeIdx(3, 3)
        Session("NonCommDis").setSubTypeFieldName("CardioVascFemales", 4)
        Session("NonCommDis").setSubTypeColor(RGB(65, 105, 225), 4)
        Session("NonCommDis").setTypeIdx(3, 4)
        Session("NonCommDis").setSubTypeFieldName("DigestiveMales", 5)
        Session("NonCommDis").setSubTypeName("Digestive", 5)
        Session("NonCommDis").setSubTypeColor(RGB(205, 0, 0), 5)
        Session("NonCommDis").setTypeIdx(4, 5)
        Session("NonCommDis").setSubTypeFieldName("DigestiveFemales", 6)
        Session("NonCommDis").setSubTypeColor(RGB(205, 0, 0), 6)
        Session("NonCommDis").setTypeIdx(4, 6)
        Session("NonCommDis").setSubTypeFieldName("RespiratoryMales", 7)
        Session("NonCommDis").setSubTypeName("Respiratory", 7)
        Session("NonCommDis").setSubTypeColor(RGB(0, 255, 0), 7)
        Session("NonCommDis").setTypeIdx(5, 7)
        Session("NonCommDis").setSubTypeFieldName("RespiratoryFemales", 8)
        Session("NonCommDis").setSubTypeColor(RGB(0, 255, 0), 8)
        Session("NonCommDis").setTypeIdx(5, 8)
        Session("NonCommDis").setSubTypeFieldName("OtherG2Males", 9)
        Session("NonCommDis").setSubTypeName("Other Non Comm", 9)
        Session("NonCommDis").setSubTypeColor(RGB(155, 0, 155), 9)
        Session("NonCommDis").setTypeIdx(6, 9)
        Session("NonCommDis").setSubTypeFieldName("OtherG2Females", 10)
        Session("NonCommDis").setSubTypeColor(RGB(155, 0, 155), 10)
        Session("NonCommDis").setTypeIdx(6, 10)
        Session("NonCommDis").setSubTypeFieldName("DiabetesMales", 11)
        Session("NonCommDis").setSubTypeName("Diabetes", 11)
        Session("NonCommDis").setSubTypeColor(RGB(255, 165, 0), 11)
        Session("NonCommDis").setTypeIdx(10, 11)
        Session("NonCommDis").setSubTypeFieldName("DiabetesFemales", 12)
        Session("NonCommDis").setSubTypeColor(RGB(255, 165, 0), 12)
        Session("NonCommDis").setTypeIdx(10, 12)

        'Injuries
        Session("Injuries") = New MortType
        Session("Injuries").setNumSubTypes(6)
        Session("Injuries").sTitle = "Injuries"
        Session("Injuries").setSubTypeFieldName("RoadTrafficAccMales", 1)
        Session("Injuries").setSubTypeName("Road Traffic Accidents", 1)
        Session("Injuries").setSubTypeColor(RGB(255, 215, 0), 1)
        Session("Injuries").setTypeIdx(7, 1)
        Session("Injuries").setSubTypeFieldName("RoadTrafficAccFemales", 2)
        Session("Injuries").setSubTypeColor(RGB(255, 215, 0), 2)
        Session("Injuries").setTypeIdx(7, 2)
        Session("Injuries").setSubTypeFieldName("OtherunintInjMales", 3)
        Session("Injuries").setSubTypeName("Other Unintentional", 3)
        Session("Injuries").setSubTypeColor(RGB(65, 105, 225), 3)
        Session("Injuries").setTypeIdx(8, 3)
        Session("Injuries").setSubTypeFieldName("OtherunintInjFemales", 4)
        Session("Injuries").setSubTypeColor(RGB(65, 105, 225), 4)
        Session("Injuries").setTypeIdx(8, 4)
        Session("Injuries").setSubTypeFieldName("IntenInjMales", 5)
        Session("Injuries").setSubTypeName("Intentional", 5)
        Session("Injuries").setSubTypeColor(RGB(205, 0, 0), 5)
        Session("Injuries").setTypeIdx(9, 5)
        Session("Injuries").setSubTypeFieldName("IntenInjFemales", 6)
        Session("Injuries").setSubTypeColor(RGB(205, 0, 0), 6)
        Session("Injuries").setTypeIdx(9, 6)

        ReDim Session("BigTypes")(3)
        Session("BigTypes")(1) = Session("CommDis")
        Session("BigTypes")(2) = Session("NonCommDis")
        Session("BigTypes")(3) = Session("Injuries")
    End Sub

    Protected Sub cmdDraw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDraw.Click
        If Not GridView1.Visible Then
            LoadSet()
        Else
            GridView1.Visible = False
        End If
        If Session("TotalGraphs") > 0 Then
            DrawGraph(Session("TotalGraphs"))
            pnlMort.Visible = False
        End If

        cmdDraw.Enabled = False
        cmdTable.Enabled = True
    End Sub

    Private Sub LoadSet()
        Dim f As Integer
        Dim sFileName As String
        Dim r As Integer, T As Integer, g As Integer, y As Integer
        'jrs 2012/07/30 Dim dbM As String
        Dim dbM As OleDb.OleDbConnection
        Dim bGrp As Boolean
        'jrs 2012/07/30 Dim dbG As String
        Dim dbG As OleDb.OleDbConnection
        Dim rsG As DataTable
        Dim sDim1 As String
        Dim iDim1 As Integer
        Dim rr As Integer
        'jrs 2009/09/15
        Dim v As Integer

        'jrs 2012/07/30 dbG = Session("IFs_MDB2Active")
        dbG = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))

        Session("TotalGraphs") = 0
        sFileName = ""

        'loop through files
        For f = 0 To lstFiles.Items.Count - 1
            If lstFiles.Items(f).Selected Then
                Select Case f
                    Case 0
                        'JAM 2020-05-25

                        'sFileName = "ifsPopWork.mdb"


                        'JAM 2020-06-01
                        If Session("mnuSQLite") Is Nothing Then
                            sFileName = "ifsPopWork.mdb"

                        Else

                            If Not Session("mnuSQLite") = "True" Then
                                sFileName = "ifsPopWork.mdb"
                            Else
                                sFileName = "ifsPopWork.db"
                            End If
                        End If


                    Case 1
                        'sFileName = "ifsPop.mdb"

                        'JAM 2020-06-01
                        If Session("mnuSQLite") Is Nothing Then
                            sFileName = "ifsPop.mdb"

                        Else

                            If Not Session("mnuSQLite") = "True" Then
                                sFileName = "ifsPop.mdb"
                            Else
                                sFileName = "ifsPopWork.db"
                            End If
                        End If


                    Case Else
                        Dim pos As Integer
                        pos = InStr(lstFiles.Items(f).Text, "-") + 1
                        sFileName = "ifsPop" & Trim(Mid(lstFiles.Items(f).Text, pos))

                        'sFileName = Replace(sFileName, ".run", ".db")

                        sFileName = Replace(sFileName, ".run", ".mdb")

                        'JAM 2020-06-01
                        If Not Session("mnuSQLite") = "True" Then
                            sFileName = Replace(sFileName, ".run", ".mdb")
                        Else
                            sFileName = Replace(sFileName, ".run", ".db")
                        End If


                End Select
                'jrs 2012/07/30
                'dbM = Session("PutFdrvRun") & sFileName
                'JAM 2020-06-01
                'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then
                '    dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
                '    dbM.Open()
                '    'Else
                '    '    dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
                '    '    dbM.Open()
                'End If


                'loop through regions
                For r = 0 To lstDim1.Items.Count - 1
                    If lstDim1.Items(r).Selected Then
                        sDim1 = lstDim1.Items(r).Text
                        iDim1 = r + 1
                        'for later: consider g-lists and decomposed groups
                        Select Case True
                            Case Session("bUseGroup")
                                bGrp = True
                            Case Session("bUseGroupDec")
                                bGrp = False
                                SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " i, " &
                            Grouping & " d WHERE i.GroupNum = d.GroupNo AND i.GroupName = """ &
                            sDim1 & """ ORDER BY Region", "", rsG)
                                rr = 0
                            Case Session("bUseGList")
                                SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ &
                            sDim1 & """ ORDER BY MemberOrder, Region", "", rsG)
                                rr = 0
                            Case Else 'Regions
                                bGrp = False
                        End Select

                        Do While True 'loop for G-Lists and Dec. Groups
                            If Session("bUseGList") Or Session("bUseGroupDec") Then
                                sDim1 = rsG.Rows(rr).Item("Region")
                                iDim1 = IFsRegs.Item(sDim1).Index
                                If Session("bUseGList") Then
                                    bGrp = rsG.Rows(rr).Item("Group")
                                End If
                            End If
                            'loop through types
                            For T = 0 To lstDim2.Items.Count - 1
                                If lstDim2.Items(T).Selected Then
                                    'loop through gender
                                    For g = 0 To lstDim3.Items.Count - 1
                                        If lstDim3.Items(g).Selected Then
                                            'loop through variables
                                            For v = 0 To lstVars.Items.Count - 1
                                                If lstVars.Items(v).Selected Then
                                                    If Session("TotalGraphs") < MaxVariables Then
                                                        Session("TotalGraphs") = Session("TotalGraphs") + 1

                                                        Select Case lstVars.Items(v).Text
                                                            Case "HLYLL"
                                                                'LoadYLL(Session("TotalGraphs"), sFileName, dbM, sDim1, iDim1, bGrp,
                                                                'lstDim2.Items(T).Text, lstDim3.Items(g).Text)
                                                                'JAM 2020-06-01
                                                                LoadYLL(Session("TotalGraphs"), sFileName, sDim1, iDim1, bGrp,
                                                                lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                            Case "HLYLD"
                                                                'LoadYLD(Session("TotalGraphs"), sFileName, dbM, sDim1, iDim1, bGrp,
                                                                'lstDim2.Items(T).Text, lstDim3.Items(g).Text, dbG)

                                                                LoadYLD(Session("TotalGraphs"), sFileName, sDim1, iDim1, bGrp,
                                                                lstDim2.Items(T).Text, lstDim3.Items(g).Text, dbG)


                                                            Case "HLDALY"
                                                                '    LoadDALY(Session("TotalGraphs"), sFileName, dbM, sDim1, iDim1, bGrp,
                                                                'lstDim2.Items(T).Text, lstDim3.Items(g).Text, dbG)

                                                                LoadDALY(Session("TotalGraphs"), sFileName, sDim1, iDim1, bGrp,
                                                               lstDim2.Items(T).Text, lstDim3.Items(g).Text, dbG)

                                                            Case "HLYLLWORK"
                                                                '    LoadYLLWORK(Session("TotalGraphs"), sFileName, dbM, sDim1, iDim1, bGrp,
                                                                'lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                                LoadYLLWORK(Session("TotalGraphs"), sFileName, sDim1, iDim1, bGrp,
                                                               lstDim2.Items(T).Text, lstDim3.Items(g).Text)

                                                            Case "HLYLDWORK"
                                                                'LoadYLDWORK(Session("TotalGraphs"), sFileName, dbM, sDim1, iDim1, bGrp,
                                                                'lstDim2.Items(T).Text, lstDim3.Items(g).Text, dbG)

                                                                LoadYLDWORK(Session("TotalGraphs"), sFileName, sDim1, iDim1, bGrp,
                                                               lstDim2.Items(T).Text, lstDim3.Items(g).Text, dbG)


                                                            Case "HLDALYWORK"
                                                                '    LoadDALYWORK(Session("TotalGraphs"), sFileName, dbM, sDim1, iDim1, bGrp,
                                                                'lstDim2.Items(T).Text, lstDim3.Items(g).Text, dbG)

                                                                LoadDALYWORK(Session("TotalGraphs"), sFileName, sDim1, iDim1, bGrp,
                                                                lstDim2.Items(T).Text, lstDim3.Items(g).Text, dbG)

                                                        End Select
                                                    End If 'IF Session("TotalGraphs") < MAX VARIABLES
                                                End If 'IF VARIABLE SELECTED
                                            Next v
                                        End If 'if gender selected
                                    Next g
                                End If 'if type selected
                            Next T
                            If Session("bUseGList") Or Session("bUseGroupDec") Then
                                rr = rr + 1
                                If rsG.Rows.Count = rr Then Exit Do
                            Else
                                Exit Do
                            End If
                        Loop
                    End If 'if region selected
                Next r
                'dbM.Close()
            End If 'if file selected
        Next f
    End Sub

    Private Sub DrawGraph(ByVal TotalGraphs As Integer)
        lblJS.Visible = True
        cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = True
        PackJScritpBuilder()
    End Sub

    Private Sub PackJScritpBuilder()

        Dim graphtitle As String = ""
        Dim subtitle As String = ""

        If Session("TotalGraphs") = 1 Then
            Select Case Session("mLegends")(1, 1)
                Case "HLYLL"
                    graphtitle = "Years of Life Lost"
                Case "HLYLD"
                    graphtitle = "Years of Life Lost to Disability"
                Case "HLDALY"
                    graphtitle = "Disability Adjusted Life Years"
                Case "HLYLLWORK"
                    graphtitle = "Working Years of Life Lost"
                Case "HLYLDWORK"
                    graphtitle = "Working Years of Life Lost to Disability"
                Case "HLDALYWORK"
                    graphtitle = "Disability Adjusted Working Life Years"
            End Select
        End If

        'Loading Legends
        If Session("TotalGraphs") = 1 Then
            subtitle = Session("mLegends")(1, 1) & ", " & Session("mLegends")(2, 1) & ", " & Session("mLegends")(3, 1) & ", " & Session("mLegends")(4, 1)
        End If

        Select Case Session("DimLbl")
            Case "1"
                subtitle = Session("mLegends")(1, 1) & ", " & Session("mLegends")(3, 1) & ", " & Session("mLegends")(4, 1) & ", " & Session("mLegends")(5, 1)
            Case "2"
                subtitle = Session("mLegends")(1, 1) & ", " & Session("mLegends")(2, 1) & ", " & Session("mLegends")(4, 1) & ", " & Session("mLegends")(5, 1)
            Case "3"
                subtitle = Session("mLegends")(1, 1) & ", " & Session("mLegends")(2, 1) & ", " & Session("mLegends")(3, 1) & ", " & Session("mLegends")(5, 1)
            Case "sce"
                subtitle = Session("mLegends")(1, 1) & ", " & Session("mLegends")(2, 1) & ", " & Session("mLegends")(3, 1) & ", " & Session("mLegends")(4, 1)
            Case "cust"
                subtitle = Session("mLegends")(7, 1)
        End Select

        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "            var mycategories = [" & vbCr
        For l = 1 To Session("LastYear") - Session("FirstYear") + 1
            If l > 1 Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & "'" & CStr(Session("FirstYear") + l - 1) & "'"
        Next
        sJscript2 = sJscript2 & "];" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/12/06 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        'sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''


        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/11 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr
        sJscript2 = sJscript2 & "x: -20,  //center" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06 Increasing fonts
        'sJscript2 = sJscript2 & "fontSize:   '18px'," & vbCr
        sJscript2 = sJscript2 & "fontSize:   '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight:  'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If subtitle > "" Then
            sJscript2 = sJscript2 & "subtitle: {" & vbCr
            sJscript2 = sJscript2 & "text:       """ & subtitle & """," & vbCr
            sJscript2 = sJscript2 & "x: -20, //center" & vbCr
            sJscript2 = sJscript2 & "style: {" & vbCr
            'jam 2025/12/06 Increasing fonts
            'sJscript2 = sJscript2 & "fontSize:   '14px'," & vbCr
            sJscript2 = sJscript2 & "fontSize:   '16px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight:  'bold'" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxisXLabel As String
        sAxisXLabel = "Year"

        Dim sAxisYLabel As String
        sAxisYLabel = "Million Years"

        sJscript2 = sJscript2 & "xAxis: {" & vbCr

        'jam 2025/12/06 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr



        sJscript2 = sJscript2 & "title: {" & vbCr

        'jam 2025/12/06 Increasing fonts
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'Load Years
        '----------

        sJscript2 = sJscript2 & ", categories: mycategories" & vbCr

        'cdv 2021/05/27 Fix Interval to be adjusted for when first year is not Mod 5 ****START****
        If Session("FirstYear") Mod 5 = 0 Then
            sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr
        Else
            Dim YearAdjust As Integer
            YearAdjust = 0

            While (Session("FirstYear") + YearAdjust) Mod 5 <> 0
                YearAdjust += 1
            End While

            Dim TickPositions(0) As Integer
            Dim CurrentYear As Integer

            CurrentYear = YearAdjust

            TickPositions(0) = CurrentYear
            sJscript2 = sJscript2 & ", tickPositions: ["
            While TickPositions.Length < 60
                CurrentYear += 5
                ReDim Preserve TickPositions(TickPositions.Length)
                TickPositions(TickPositions.Length - 1) = CurrentYear
            End While

            For Each TickPosition In TickPositions
                If TickPosition = TickPositions(0) Then
                    sJscript2 = sJscript2 & CStr(TickPosition)
                Else
                    sJscript2 = sJscript2 & ", " & CStr(TickPosition)
                End If
            Next

            sJscript2 = sJscript2 & "]" & vbCr
            '****END****
        End If

        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr

        'jam 2025/12/06 Increasing font for y axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr



        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

        'jam 2025/12/06 Increasing fonts
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr



        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr


        'jrs 2015/09/08
        sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Session("TotalGraphs") > 1 Then
            sJscript2 = sJscript2 & "legend: {" & vbCr


            'jam 2025/12/06 Increasing fonts
            sJscript2 = sJscript2 & "itemStyle: {" & vbCr
            sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
            'sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}, " & vbCr

            sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
            sJscript2 = sJscript2 & "align:      'center'," & vbCr
            sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
            sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        Else
            sJscript2 = sJscript2 & "legend: {" & vbCr

            'jam 2025/12/06 Increasing fonts
            sJscript2 = sJscript2 & "itemStyle: {" & vbCr
            sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
            'sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}, " & vbCr


            sJscript2 = sJscript2 & "enabled:     false" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If
        sJscript2 = sJscript2 & "series: [" & vbCr

        For i = 1 To Session("TotalGraphs")
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("mPLOT"))
        Next i

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr
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

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub

    Private Function PlotColToJSSeries(ByVal i As Integer, ByRef Vals As Object) As String

        Dim colIdx As Integer
        colIdx = i

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i) & """, color: " & getColor(i) & ", data:[" & vbCr

        Dim j As Integer
        Dim bFirst As Boolean = True
        'cdv 2021/02/02 changed LastYear -> iLastYear
        For j = 1 To Session("iLastYear") - Session("FirstYear") + 1
            If Not IsDBNull(Session("mPLOT")(j, i)) Then
                If bFirst Then
                    bFirst = False
                Else
                    sJscript = sJscript & "," & vbCr
                End If
                sJscript = sJscript & CStr(Math.Round(Vals(j, i), 2))
            End If
        Next j

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function
    'Private Sub LoadYLL(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    'ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    'ByVal sGender As String)

    Private Sub LoadYLL(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String)

        Dim Tindex As Integer
        Dim SetNo%
        Dim rId As Integer
        Dim tbl2 As DataTable
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        Dim RegionIdGroup() As Integer
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single
        Dim sRegs As String

        Dim iYr As Integer

        'create connecion
        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        Dim dbM As OleDb.OleDbConnection
        'JAM 2020-06-01
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM

        End If


        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow


        'JAM 2020-05-23
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDS = New DataSet 'JAM 2020-05-23


        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"
            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"
            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"
            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"
            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"
            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"
            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                sFemaleMortFld = "RespInfFeMales"
            Case "MentalHealth"
                    'do nothing, just load labels
            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                sFemaleMortFld = "MortPerFeMalesComDis"
            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                sFemaleMortFld = "MortPerFeMalesNonComDis"
            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                sFemaleMortFld = "MortPerFeMalesInjuries"
            Case "Total"
                'need to read all 6 fields
        End Select

        ReDim TempPopDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)

        'jrs 2009/08/19
        If sType <> "MentalHealth" Then
            'take population
            '   Change the data source
            '   ----------------------
            Tindex = 2
            If Not bGrp Then

                'JAM 2019-12-26
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
                            Str(iReg))

                    Do While rdr.Read 'Not tbl2.Rows.Count = i2
                        iYr = rdr.Item("Years") - Session("FirstYear") + 1
                        TempPopDatabase(rdr.Item("NCohorts"), 1, iYr) = rdr.Item("PopulationMales")
                        TempPopDatabase(rdr.Item("NCohorts"), 2, iYr) = rdr.Item("PopulationFemales")
                    Loop
                    rdr.Close()
                Else
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " & Str(iReg)
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    While (vSQLiteReader.Read())


                        iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                        TempPopDatabase(vSQLiteReader("CohortId"), 1, iYr) = vSQLiteReader("PopulationMales")
                        TempPopDatabase(vSQLiteReader("CohortId"), 2, iYr) = vSQLiteReader("PopulationFemales")

                    End While


                End If
            Else
                'LoadRegs(sReg, RegionIdGroup, GroupTot)
                ReDim POPMales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
                ReDim POPFemales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)

                IfsConn.Open()
                Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
                Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
                Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)

                Dim popDA As OleDb.OleDbDataAdapter
                regDS = New DataSet
                'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
                '" ORDER BY Region, NCohorts", dbM)

                'JAM 2020-05-23
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" & " ORDER BY Region, NCohorts", dbM)
                Else

                    'JAM 2019-12-23
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "Select * FROM Pop_Base_Case" & " ORDER BY RegionId, CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()
                    DTsqlite.Load(vSQLiteReader)
                    'regDS.Tables.Add(DTsqlite)


                    dtnew = DTsqlite.Clone
                    Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    d2.DataType = System.Type.GetType("System.Int32")

                    For Each row As DataRow In DTsqlite.Rows
                        dtnew.ImportRow(row)
                    Next
                    regDS.Tables.Add(dtnew)


                End If






                grpDA.Fill(regDS, "Groups")
                regDA.Fill(regDS, "Regions")
                regIDDA.Fill(regDS, "RegionIDs")

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    popDA.Fill(regDS, "Population")
                End If








                grpRegRel = regDS.Relations.Add("GrpRegion",
                                                     regDS.Tables("Groups").Columns("GroupNum"),
                                                     regDS.Tables("Regions").Columns("GroupNo"))
                regRegIdRel = regDS.Relations.Add("RegRegId",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Regions").Columns("RegionNo"))
                'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Population").Columns("Region"))

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


                'JAM 2020-05-23
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    For Each pppRow In regDS.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    'Console.WriteLine(pRow("CustomerID").ToString())
                                    For Each cRow In pRow.GetChildRows(regPopRel)
                                        iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                        TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) =
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) + cRow.Item("PopulationMales")
                                        TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) =
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) + cRow.Item("PopulationFemales")

                                        POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                    cRow.Item("PopulationMales")
                                        POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                    cRow.Item("PopulationFemales")
                                    Next
                                Next
                            Next
                        End If
                    Next

                Else
                    For Each pppRow In regDS.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    'Console.WriteLine(pRow("CustomerID").ToString())
                                    For Each cRow In pRow.GetChildRows(regPopRel)
                                        iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                        TempPopDatabase(cRow.Item("CohortId"), 1, iYr) =
                                    TempPopDatabase(cRow.Item("CohortId"), 1, iYr) + cRow.Item("PopulationMales")
                                        TempPopDatabase(cRow.Item("CohortId"), 2, iYr) =
                                    TempPopDatabase(cRow.Item("CohortId"), 2, iYr) + cRow.Item("PopulationFemales")

                                        POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                    cRow.Item("PopulationMales")
                                        POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                    cRow.Item("PopulationFemales")
                                    Next
                                Next
                            Next
                        End If
                    Next



                End If


            End If
            'End take population

            'jrs 2009/12/14
            'jrs 2010/03/18 Dim years As Single
            Dim yearsM As Single, yearsF As Single

            '   Change the data source
            '   ----------------------
            If Not bGrp Then

                'JAM 2019-12-26
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    rdr = ADO_OleDB_GetReader(myCMD, "Select * FROM MorDetBaseCase Where Region = " & Str(iReg))

                    Do While rdr.Read 'Not tbl2.Rows.Count = i2
                        TCohort = rdr.Item("NCohorts")
                        iYr = rdr.Item("Years") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            'males
                            TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                            yearsM
                            'females
                            TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                            yearsF
                        Else
                            'males
                            TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item("MortPerMalesComDis") +
                            rdr.Item("MortPerMalesNonComDis") + rdr.Item("MortPerMalesInjuries")) *
                            TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM

                            'females
                            TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item("MortPerFeMalesComDis") +
                            rdr.Item("MortPerFeMalesNonComDis") + rdr.Item("MortPerFeMalesInjuries")) *
                            TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF
                        End If
                    Loop
                    rdr.Close()
                Else


                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "Select * FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg)
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    While (vSQLiteReader.Read())
                        TCohort = vSQLiteReader("CohortId")
                        iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            'males
                            TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                            yearsM
                            'females
                            TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                            yearsF
                        Else
                            'males
                            TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader("MortPerMalesComDis") +
                            vSQLiteReader("MortPerMalesNonComDis") + vSQLiteReader("MortPerMalesInjuries")) *
                            TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM

                            'females
                            TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader("MortPerFeMalesComDis") +
                            vSQLiteReader("MortPerFeMalesNonComDis") + vSQLiteReader("MortPerFeMalesInjuries")) *
                            TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF
                        End If
                    End While

                End If

            Else 'Groups  
                'Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("Select * FROM MorDetBaseCase ORDER BY Years, Region, NCohorts", dbM)

                'mortDA.Fill(regDS, "Mortality")
                'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Mortality").Columns("Region"))

                Dim mortDA As OleDb.OleDbDataAdapter

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    mortDA = New OleDb.OleDbDataAdapter("Select * FROM MorDetBaseCase ORDER BY Years, Region, NCohorts", dbM)
                    mortDA.Fill(regDS, "Mortality")
                Else
                    'JAM 2019-12-23
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "Select * FROM Mor_Det_Base_Case ORDER BY TimeId, RegionId, CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()
                    DTsqlite2.Load(vSQLiteReader)

                    dtnew = DTsqlite2.Clone
                    Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    d2.DataType = System.Type.GetType("System.Int32")

                    For Each row As DataRow In DTsqlite2.Rows
                        dtnew.ImportRow(row)
                    Next
                    regDS.Tables.Add(dtnew)

                End If




                Dim regMortRel As DataRelation

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mortality").Columns("Region"))
                Else


                    regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


                End If

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    For Each pppRow In regDS.Tables("Groups").Rows '2020-01-05 Ask Jose
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("NCohorts")
                                        rId = cRow.Item("Region")
                                        iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        cRow.Item(sMaleMortFld) *
                                        POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        cRow.Item(sFemaleMortFld) *
                                        POPFemales(rId, TCohort, iYr) * 0.001 * yearsF
                                        Else
                                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                        cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, iYr) * 0.001 *
                                        yearsM

                                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                        cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                        yearsF
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next

                Else

                    For Each pppRow In regDS.Tables("Groups").Rows '2020-01-05 Ask Jose
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("CohortId")
                                        rId = cRow.Item("RegionId")
                                        iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        cRow.Item(sMaleMortFld) *
                                        POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        cRow.Item(sFemaleMortFld) *
                                        POPFemales(rId, TCohort, iYr) * 0.001 * yearsF
                                        Else
                                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                        cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, iYr) * 0.001 *
                                        yearsM

                                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                        cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                        yearsF
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next




                End If

                IfsConn.Close()
            End If 'if not groups, else groups
        End If 'if not Mental health

        'JAM 2020-05-23
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()
        End If

        'Load Final Array
        Dim y As Integer
        For y = 1 To Session("iLastYear") - Session("FirstYear") + 1
            Session("mPLOT")(y, TotalGraphs) = 0
            For TCohort = 0 To NCohorts
                'If y = 34 Then Debug.Print TempDatabase(TCohort, 2, y)
                If sGender = "Male" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 1, y)
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 2, y)
                Else
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + (TempDatabase(TCohort, 1, y) +
                    TempDatabase(TCohort, 2, y))
                End If
            Next TCohort
        Next y
        Session("mLegends")(1, TotalGraphs) = "HLYLL"
        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        If bGrp And False Then 'mnulblAlias.Checked Then
            'Read and load all aliases
            Dim db As String, s As String
            Dim Tbl As DataTable
            db = Session("IFs_MDB2")
            SetDataTablefromAccess(db, "Select * FROM " & GroupIDNew, "GroupName", Tbl)
            s = SeekValue(Tbl, sReg, "Alias")
            If s > "" Then
                Session("mLegends")(2, TotalGraphs) = s
            End If
        Else
            Session("mLegends")(2, TotalGraphs) = sReg
        End If

        If sType <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sType
        Else
            Session("mLegends")(3, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(4, TotalGraphs) = sGender
        Else
            Session("mLegends")(4, TotalGraphs) = "All Genders"
        End If

        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then 'JAM 2020-05-24
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            ''JAM 2020-05-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")

            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If
    End Sub

    Private Sub LoadYLLWORK(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String)

        Dim Tindex As Integer
        Dim SetNo%
        Dim rId As Integer
        Dim tbl2 As DataTable
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        Dim RegionIdGroup() As Integer
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single
        Dim sRegs As String

        Dim iYr As Integer
        Dim regDs As DataSet


        'JAM 2020-05-24
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable
        regDs = New DataSet 'JAM 2020-05-23


        'create connecion
        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        'myCMD.Connection = dbM

        'JAM 2020-06-01
        Dim dbM As OleDb.OleDbConnection
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM

        End If


        'Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow


        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"
            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"
            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"
            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"
            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"
            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"
            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                sFemaleMortFld = "RespInfFeMales"
            Case "MentalHealth"
                    'do nothing, just load labels
            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                sFemaleMortFld = "MortPerFeMalesComDis"
            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                sFemaleMortFld = "MortPerFeMalesNonComDis"
            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                sFemaleMortFld = "MortPerFeMalesInjuries"
            Case "Total"
                'need to read all 6 fields
        End Select

        ReDim TempPopDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)

        'jrs 2009/08/19
        If sType <> "MentalHealth" Then
            'take population
            '   Change the data source
            '   ----------------------
            Tindex = 2
            If Not bGrp Then

                'JAM 2020-01-05
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    rdr = ADO_OleDB_GetReader(myCMD, "Select * FROM PopBaseCase Where NCohorts > 4 And NCohorts < 14 And Region = " &
                Str(iReg))

                    Do While rdr.Read 'Not tbl2.Rows.Count = i2
                        iYr = rdr.Item("Years") - Session("FirstYear") + 1
                        TempPopDatabase(rdr.Item("NCohorts"), 1, iYr) = rdr.Item("PopulationMales")
                        TempPopDatabase(rdr.Item("NCohorts"), 2, iYr) = rdr.Item("PopulationFemales")
                    Loop
                    rdr.Close()
                Else
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "Select * FROM Pop_Base_Case Where CohortId > 4 And CohortId < 14 And RegionId = " &
                                                Str(iReg)
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()



                    While (vSQLiteReader.Read())


                        iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                        TempPopDatabase(vSQLiteReader("CohortId"), 1, iYr) = vSQLiteReader("PopulationMales")
                        TempPopDatabase(vSQLiteReader("CohortId"), 2, iYr) = vSQLiteReader("PopulationFemales")


                    End While

                End If

            Else
                'LoadRegs(sReg, RegionIdGroup, GroupTot)
                ReDim POPMales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
                ReDim POPFemales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)

                IfsConn.Open()
                regDs = New DataSet

                Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("Select * FROM " & GroupIDNew, IfsConn)
                Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("Select * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
                Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
                'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
                '" WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Region, NCohorts", dbM)

                Dim popDA As OleDb.OleDbDataAdapter

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
                            " WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Region, NCohorts", dbM)
                Else

                    'JAM 2019-12-23
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case" &
                            " WHERE CohortId > 4 AND CohortId < 14 ORDER BY RegionId, CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()
                    DTsqlite.Load(vSQLiteReader)
                    'regDS.Tables.Add(DTsqlite)


                    dtnew = DTsqlite.Clone
                    Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    d2.DataType = System.Type.GetType("System.Int32")

                    For Each row As DataRow In DTsqlite.Rows
                        dtnew.ImportRow(row)
                    Next
                    regDs.Tables.Add(dtnew)


                End If

                grpDA.Fill(regDs, "Groups")
                regDA.Fill(regDs, "Regions")
                regIDDA.Fill(regDs, "RegionIDs")
                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    popDA.Fill(regDs, "Population")
                End If
                grpRegRel = regDs.Relations.Add("GrpRegion",
                                                     regDs.Tables("Groups").Columns("GroupNum"),
                                                     regDs.Tables("Regions").Columns("GroupNo"))
                regRegIdRel = regDs.Relations.Add("RegRegId",
                                                     regDs.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDs.Tables("Regions").Columns("RegionNo"))
                'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Population").Columns("Region"))

                Dim regPopRel As DataRelation

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    regPopRel = regDs.Relations.Add("RegPopulation",
                                                     regDs.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDs.Tables("Population").Columns("Region"))
                Else


                    regPopRel = regDs.Relations.Add("RegPopulation",
                                                 regDs.Tables("RegionIDs").Columns("RegionNum"),
                                                regDs.Tables("pop_base_case").Columns(1))


                End If



                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    For Each pppRow In regDs.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    'Console.WriteLine(pRow("CustomerID").ToString())
                                    For Each cRow In pRow.GetChildRows(regPopRel)
                                        iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                        TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) =
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) + cRow.Item("PopulationMales")
                                        TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) =
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) + cRow.Item("PopulationFemales")

                                        POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                    cRow.Item("PopulationMales")
                                        POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                    cRow.Item("PopulationFemales")
                                    Next
                                Next
                            Next
                        End If
                    Next

                Else
                    For Each pppRow In regDs.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    'Console.WriteLine(pRow("CustomerID").ToString())
                                    For Each cRow In pRow.GetChildRows(regPopRel)
                                        iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                        'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                        TempPopDatabase(cRow.Item("CohortId"), 1, iYr) =
                                    TempPopDatabase(cRow.Item("CohortId"), 1, iYr) + cRow.Item("PopulationMales")
                                        TempPopDatabase(cRow.Item("CohortId"), 2, iYr) =
                                    TempPopDatabase(cRow.Item("CohortId"), 2, iYr) + cRow.Item("PopulationFemales")

                                        POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                    cRow.Item("PopulationMales")
                                        POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                    cRow.Item("PopulationFemales")
                                    Next
                                Next
                            Next
                        End If
                    Next

                End If

            End If
            'End take population

            'jrs 2009/12/14
            'jrs 2010/03/18 Dim years As Single
            Dim yearsM As Single, yearsF As Single

            '   Change the data source
            '   ----------------------
            If Not bGrp Then

                'JAM 2020-01-05
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where NCohorts > 4 AND NCohorts < 14 AND Region = " & Str(iReg))

                    Do While rdr.Read 'Not tbl2.Rows.Count = i2
                        TCohort = rdr.Item("NCohorts")
                        iYr = rdr.Item("Years") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            'males
                            TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                        yearsM
                            'females
                            TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                        yearsF
                        Else
                            'males
                            TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item("MortPerMalesComDis") +
                        rdr.Item("MortPerMalesNonComDis") + rdr.Item("MortPerMalesInjuries")) *
                        TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM

                            'females
                            TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item("MortPerFeMalesComDis") +
                        rdr.Item("MortPerFeMalesNonComDis") + rdr.Item("MortPerFeMalesInjuries")) *
                        TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF
                        End If
                    Loop
                    rdr.Close()

                Else 'JAM 2020-01-05

                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where CohortId > 4 AND CohortId < 14 AND RegionId = " & Str(iReg)
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    While (vSQLiteReader.Read())
                        TCohort = vSQLiteReader("CohortId")
                        iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            'males
                            TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                        yearsM
                            'females
                            TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                        yearsF
                        Else
                            'males
                            TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader("MortPerMalesComDis") +
                        vSQLiteReader("MortPerMalesNonComDis") + vSQLiteReader("MortPerMalesInjuries")) *
                        TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM

                            'females
                            TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader("MortPerFeMalesComDis") +
                        vSQLiteReader("MortPerFeMalesNonComDis") + vSQLiteReader("MortPerFeMalesInjuries")) *
                        TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF
                        End If
                    End While

                End If

            Else 'Groups


                'Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Years, Region, NCohorts", dbM)
                'mortDA.Fill(regDS, "Mortality")
                'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Mortality").Columns("Region"))


                Dim mortDA As OleDb.OleDbDataAdapter

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Years, Region, NCohorts", dbM)
                    mortDA.Fill(regDs, "Mortality")
                Else
                    'JAM 2019-12-23
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case WHERE CohortId > 4 AND CohortId < 14 ORDER BY TimeId, RegionId, CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()
                    DTsqlite2.Load(vSQLiteReader)

                    dtnew = DTsqlite2.Clone
                    Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    d2.DataType = System.Type.GetType("System.Int32")

                    For Each row As DataRow In DTsqlite2.Rows
                        dtnew.ImportRow(row)
                    Next
                    regDs.Tables.Add(dtnew)

                End If



                Dim regMortRel As DataRelation

                'JAM 2020-02-29
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    regMortRel = regDs.Relations.Add("RegMortality",
                                                     regDs.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDs.Tables("Mortality").Columns("Region"))
                Else


                    regMortRel = regDs.Relations.Add("RegMortality",
                                                 regDs.Tables("RegionIDs").Columns("RegionNum"),
                                                regDs.Tables("Mor_Det_Base_Case").Columns("RegionId"))


                End If



                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    For Each pppRow In regDs.Tables("Groups").Rows 'JAM 2020-01-05 Ask Jose
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("NCohorts")
                                        rId = cRow.Item("Region")
                                        iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        cRow.Item(sMaleMortFld) *
                                        POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        cRow.Item(sFemaleMortFld) *
                                        POPFemales(rId, TCohort, iYr) * 0.001 * yearsF
                                        Else
                                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                        cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, iYr) * 0.001 *
                                        yearsM

                                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                        cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                        yearsF
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next

                Else
                    For Each pppRow In regDs.Tables("Groups").Rows 'JAM 2020-01-05 Ask Jose
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("CohortId")
                                        rId = cRow.Item("RegionId")
                                        iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        cRow.Item(sMaleMortFld) *
                                        POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        cRow.Item(sFemaleMortFld) *
                                        POPFemales(rId, TCohort, iYr) * 0.001 * yearsF
                                        Else
                                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        (cRow.Item("MortPerMalesComDis") + cRow.Item("MortPerMalesNonComDis") +
                                        cRow.Item("MortPerMalesInjuries")) * POPMales(rId, TCohort, iYr) * 0.001 *
                                        yearsM

                                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        (cRow.Item("MortPerFeMalesComDis") + cRow.Item("MortPerFeMalesNonComDis") +
                                        cRow.Item("MortPerFeMalesInjuries")) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                        yearsF
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next


                End If

                IfsConn.Close()
            End If 'if not groups, else groups
        End If 'if not Mental health

        'JAM 2020-06-01
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()


        End If

        'Load Final Array
        Dim y As Integer
        For y = 1 To Session("iLastYear") - Session("FirstYear") + 1
            Session("mPLOT")(y, TotalGraphs) = 0
            For TCohort = 0 To NCohorts
                'If y = 34 Then Debug.Print TempDatabase(TCohort, 2, y)
                If sGender = "Male" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 1, y)
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 2, y)
                Else
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + (TempDatabase(TCohort, 1, y) +
                    TempDatabase(TCohort, 2, y))
                End If
            Next TCohort
        Next y
        Session("mLegends")(1, TotalGraphs) = "HLYLLWORK"
        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        If bGrp And False Then 'mnulblAlias.Checked Then
            'Read and load all aliases
            Dim db As String, s As String
            Dim Tbl As DataTable
            db = Session("IFs_MDB2")
            SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew, "GroupName", Tbl)
            s = SeekValue(Tbl, sReg, "Alias")
            If s > "" Then
                Session("mLegends")(2, TotalGraphs) = s
            End If
        Else
            Session("mLegends")(2, TotalGraphs) = sReg
        End If

        If sType <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sType
        Else
            Session("mLegends")(3, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(4, TotalGraphs) = sGender
        Else
            Session("mLegends")(4, TotalGraphs) = "All Genders"
        End If

        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            'JAM 2020-05-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")
            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")


            End If

        End If
    End Sub

    'Private Sub LoadYLD(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    'ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    'ByVal sGender As String, ByRef db As OleDb.OleDbConnection)

    Private Sub LoadYLD(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String, ByRef db As OleDb.OleDbConnection)

        'dbM is the database to read Mortality results
        'db is the database to read YLDtoYLL ratios

        Dim Tindex As Integer
        Dim SetNo%
        Dim rId As Integer
        Dim tbl2 As OleDb.OleDbDataReader
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        Dim RegionIdGroup() As Integer
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single
        Dim sRegs As String
        Dim YLDtoYLLratio(0, 0, 0) As Single
        Dim iType As Integer
        Dim iBigType As Integer
        Dim YLDtoPOPratio(0, 0) As Single
        'jrs 2009/09/02
        Dim SaveLastYearMort(0, 0, 0, 0) As Single
        Dim SaveLastYearMorb(0, 0, 0, 0) As Single
        Dim YLDtoYLLratioGrowthPortion(0, 0) As Single
        ReDim YLDtoYLLratioGrowthPortion(UBound(Session("PLOT"), 1), 14)

        Dim iYr As Integer

        'JAM 2020-05-24
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable



        DTsqlite = New DataTable
        DTsqlite2 = New DataTable



        'create connecion
        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        IfsConn.Open()
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        Dim ifsCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        ifsCMD.Connection = IfsConn

        'JAM 2020-06-01
        Dim dbM As OleDb.OleDbConnection
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM

        End If


        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim i As Integer, j As Integer
        Dim RegsInGrp As Collection

        regDS = New DataSet 'JAM 2020-05-24

        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"
                iType = 1
            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"
                iType = 2
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"
                iType = 3
            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"
                iType = 4
            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"
                iType = 5
            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"
                iType = 6
            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"
                iType = 7
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"
                iType = 8
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"
                iType = 9
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"
                iType = 10
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"
                iType = 11
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"
                iType = 12
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"
                iType = 13
            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                sFemaleMortFld = "RespInfFeMales"
                iType = 14
            Case "MentalHealth"
                    'jrs 2009/08/18
                    'different logic, not from yll, but from ratio with population
            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                sFemaleMortFld = "MortPerFeMalesComDis"
                iBigType = 1
            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                sFemaleMortFld = "MortPerFeMalesNonComDis"
                iBigType = 2
            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                sFemaleMortFld = "MortPerFeMalesInjuries"
                iBigType = 3
            Case "Total"
                'need to read all 6 fields
        End Select

        ReDim TempPopDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)

        'jrs 2009/09/02
        'Set tbl2 = db.OpenRecordset("HealthRatioYLDtoYLLGrowthPortion")
        'tbl2.Index = "DiseaseType"
        Dim k As Integer
        Dim morbtomortidx As Integer
        morbtomortidx = -1
        Dim sBinFile As String
        Dim BinFileIdx As Integer
        BinFileIdx = -1
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            'JAM 2020-05-25
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = "Working.run"
                BinFileIdx = 0
            Else
                sBinFile = "Working.run.db"
                BinFileIdx = 0
            End If

        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            '    sBinFile = "ifsBase.run"
            'BinFileIdx = 1

            'JAM 2020-05-25
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = "ifsBase.run"
                BinFileIdx = 1
            Else
                sBinFile = "ifsBase.run.db"
                BinFileIdx = 1
            End If


        Else

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN"), "IFSPOP", "")
            Else
                sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN.DB"), "IFSPOP", "")
            End If

            For k = 2 To lstFiles.Items.Count - 1
                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
                        BinFileIdx = k
                        Exit For
                    End If
                Else
                    If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".db" Then
                        BinFileIdx = k
                        Exit For
                    End If
                End If


            Next k





        End If

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For k = 1 To 14
            'jrs 2010/01/18
            'tbl2.Seek "=", GRPS(27, k)
            'YLDtoYLLratioGrowthPortion(k) = tbl2.Fields("GrowthPortionTarget")
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("hlmorbtomortgthport", morbtomortidx, GRPS(27, k), k, "", -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
            k = 1, k = 14, False, 1, YLDtoYLLratioGrowthPortion, k, 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            Else
                gLoadForDisplay("hlmorbtomortgthport", morbtomortidx, GRPS(27, k), k, "", -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
                k = 1, k = 14, False, 1, YLDtoYLLratioGrowthPortion, k, 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        Next k

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then

            'JAM 2020-01-05
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
            Str(iReg))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    iYr = rdr.Item("Years") - Session("FirstYear") + 1
                    TempPopDatabase(rdr.Item("NCohorts"), 1, iYr) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2, iYr) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()

                'jrs 2009/08/18
                If sType <> "MentalHealth" Then
                    ReDim YLDtoYLLratio(1, 14, 2)
                    tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthRatioYLDYLLRegion Where RegionId = " &
                Str(iReg) & " AND code <> 15") 'jrs 2020/05/26 added and code <> 15
                    Do While tbl2.Read
                        YLDtoYLLratio(1, tbl2.Item("Code"), tbl2.Item("Gender")) =
                    tbl2.Item("RatioYLDYLL")
                    Loop
                    tbl2.Close()
                End If

                'jrs 2009/08/18
                If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                    ReDim YLDtoPOPratio(1, 2)
                    tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthMentalHealthReg Where Cohort = -1 AND RegionId = " &
                Str(iReg))
                    Do While tbl2.Read
                        YLDtoPOPratio(1, tbl2.Item("Gender")) =
                    tbl2.Item("YLDtoPOP")
                    Loop
                    tbl2.Close()
                End If

            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " & Str(iReg)
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                While (vSQLiteReader.Read())
                    iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                    TempPopDatabase(vSQLiteReader("CohortId"), 1, iYr) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2, iYr) = vSQLiteReader("PopulationFemales")

                End While




                'jrs 2009/08/18
                If sType <> "MentalHealth" Then
                    ReDim YLDtoYLLratio(1, 14, 2)
                    tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthRatioYLDYLLRegion Where RegionId = " &
                Str(iReg) & " AND code <> 15") 'jrs 2019/08/26 added and code <> 15
                    Do While tbl2.Read
                        YLDtoYLLratio(1, tbl2.Item("Code"), tbl2.Item("Gender")) =
                    tbl2.Item("RatioYLDYLL")
                    Loop
                    tbl2.Close()



                End If

                'jrs 2009/08/18
                If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                    ReDim YLDtoPOPratio(1, 2)
                    tbl2 = ADO_OleDB_GetReader(ifsCMD, "Select * FROM HealthMentalHealthReg Where Cohort = -1 And RegionId = " &
                Str(iReg))
                    Do While tbl2.Read
                        YLDtoPOPratio(1, tbl2.Item("Gender")) =
                    tbl2.Item("YLDtoPOP")
                    Loop
                    tbl2.Close()
                End If

            End If
        Else 'using groups
            'LoadRegs(sReg, RegionIdGroup, GroupTot)
            ReDim POPMales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
            ReDim POPFemales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
            RegsInGrp = New Collection

            Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("Select * FROM " & GroupIDNew, IfsConn)
            Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("Select * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)
            regDS = New DataSet

            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'JAM 2020-05-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
                                                    " ORDER BY Region, NCohorts", dbM)
            Else

                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case" &
                                                    " ORDER BY RegionId, CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)


                dtnew = DTsqlite.Clone
                Dim d2 As DataColumn = dtnew.Columns("RegionId")
                d2.DataType = System.Type.GetType("System.Int32")

                For Each row As DataRow In DTsqlite.Rows
                    dtnew.ImportRow(row)
                Next
                regDS.Tables.Add(dtnew)


            End If



            grpDA.Fill(regDS, "Groups")
            regDA.Fill(regDS, "Regions")
            regIDDA.Fill(regDS, "RegionIDs")

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                popDA.Fill(regDS, "Population")
            End If

            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

            Dim regPopRel As DataRelation

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))


            End If


            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                RegsInGrp.Add(pRow.Item("RegionNum"), pRow.Item("RegionNum"))
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                RegsInGrp.Add(pRow.Item("RegionNum"), pRow.Item("RegionNum"))
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1, iYr) =
                                TempPopDatabase(cRow.Item("CohortId"), 1, iYr) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2, iYr) =
                                TempPopDatabase(cRow.Item("CohortId"), 2, iYr) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next

            End If

            'jrs 2009/08/18
            If sType <> "MentalHealth" Then
                ReDim YLDtoYLLratio(NR, 14, 2)
                Dim ratDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM HealthRatioYLDYLLRegion" &
                " WHERE code <> 15 ORDER BY RegionID", db) 'jrs 2020/05/26
                ratDA.Fill(regDS, "YLDYLLRatio")
                Dim regRatRel As DataRelation = regDS.Relations.Add("RegYLDYLLRatio",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("YLDYLLRatio").Columns("RegionID"))
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                rId = pRow.Item("RegionNum")
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regRatRel)
                                    YLDtoYLLratio(rId, cRow.Item("Code"), cRow.Item("Gender")) =
                                cRow.Item("RatioYLDYLL")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            'jrs 2009/08/18
            If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                ReDim YLDtoPOPratio(NR, 2)
                Dim ratDA2 As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM HealthMentalHealthReg Where Cohort = -1 ORDER BY RegionID", db)
                ratDA2.Fill(regDS, "YLDPOPRatio")
                Dim regRat2Rel As DataRelation = regDS.Relations.Add("RegYLDPOPRatio",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("YLDPOPRatio").Columns("RegionId"))

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                rId = pRow.Item("RegionNum")
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regRat2Rel)
                                    YLDtoPOPratio(rId, cRow.Item("Gender")) =
                                cRow.Item("YLDtoPOP")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

        End If
        'End take population

        'jrs 2009/09/02
        Dim MortGrowthRt As Single
        If bGrp Then
            ReDim SaveLastYearMort(NCohorts, 2, NR, 14)
            ReDim SaveLastYearMorb(NCohorts, 2, NR, 14)
        Else
            ReDim SaveLastYearMort(NCohorts, 2, 1, 14)
            ReDim SaveLastYearMorb(NCohorts, 2, 1, 14)
        End If

        'jrs 2009/12/14
        'jrs 2010/03/18 Dim years As Single
        Dim yearsM As Single, yearsF As Single

        'JRS 2009/08/18
        If sType <> "MentalHealth" Then
            '   Change the data source
            '   ----------------------
            If Not bGrp Then
                'JAM 2020-01-05
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where Region = " & Str(iReg) & " ORDER BY Years")

                    Do While rdr.Read 'Not tbl2.Rows.Count = i2
                        TCohort = rdr.Item("NCohorts")
                        iYr = rdr.Item("Years") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                If iYr = 1 Then
                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * YLDtoYLLratio(1, iType, 1)
                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * YLDtoYLLratio(1, iType, 2)
                                    'jrs 2009/09/02
                                    SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                    SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                Else
                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    End If

                                    'males
                                    TempDatabase(TCohort, 1, iYr) = SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    End If

                                    'females
                                    TempDatabase(TCohort, 2, iYr) = SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                End If 'if yr = 1, else
                                'jrs 2009/09/02
                                SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                'jrs 2010/03/19 End If 'if last cohort, else
                            Else 'Other totals
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    'jrs 2009/09/02
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1, else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i 'for each type
                            End If 'if not subtotals for the 3 big
                        Else 'Total (all types)
                            For j = 1 To 3
                                iBigType = j
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, rdr.Item(sMaleMortFld)) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, rdr.Item(sFemaleMortFld)) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1 else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i
                            Next j
                        End If
                    Loop
                    rdr.Close()

                Else 'JAM 2020-01-05

                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg) & " ORDER BY TimeId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()





                    While (vSQLiteReader.Read())
                        TCohort = vSQLiteReader("CohortId")
                        iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                If iYr = 1 Then
                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * YLDtoYLLratio(1, iType, 1)
                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * YLDtoYLLratio(1, iType, 2)
                                    'jrs 2009/09/02
                                    SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                    SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                Else
                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    End If

                                    'males
                                    TempDatabase(TCohort, 1, iYr) = SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    End If

                                    'females
                                    TempDatabase(TCohort, 2, iYr) = SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                End If 'if yr = 1, else
                                'jrs 2009/09/02
                                SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                'jrs 2010/03/19 End If 'if last cohort, else
                            Else 'Other totals
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    'jrs 2009/09/02
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1, else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i 'for each type
                            End If 'if not subtotals for the 3 big
                        Else 'Total (all types)
                            For j = 1 To 3
                                iBigType = j
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, vSQLiteReader(sMaleMortFld)) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, vSQLiteReader(sFemaleMortFld)) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1 else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i
                            Next j
                        End If
                    End While



                End If



            Else 'Groups




                Dim mortDA As OleDb.OleDbDataAdapter

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase ORDER BY Years, Region, NCohorts", dbM)
                    mortDA.Fill(regDS, "Mortality")
                Else
                    'JAM 2019-12-23
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case ORDER BY TimeId, RegionId, CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()
                    DTsqlite2.Load(vSQLiteReader)

                    dtnew = DTsqlite2.Clone
                    Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    d2.DataType = System.Type.GetType("System.Int32")

                    For Each row As DataRow In DTsqlite2.Rows
                        dtnew.ImportRow(row)
                    Next
                    regDS.Tables.Add(dtnew)

                End If



                'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Mortality").Columns("Region"))


                Dim regMortRel As DataRelation

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mortality").Columns("Region"))
                Else


                    regMortRel = regDS.Relations.Add("RegMortality",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


                End If



                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    For Each pppRow In regDS.Tables("Groups").Rows 'JAM 2020-01-05 Ask Jose
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("NCohorts")
                                        rId = cRow.Item("Region")
                                        iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                                If iYr = 1 Then
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                YLDtoYLLratio(rId, iType, 1)

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                YLDtoYLLratio(rId, iType, 2)
                                                    'jrs 2009/09/02
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                Else
                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    End If

                                                    'Males
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    End If

                                                    'Females
                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                End If 'if yr=1, else
                                                'jrs 2009/09/02
                                                SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                'jrs 2010/03/19 End If 'if last cohort, else
                                            Else 'other totals
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            End If

                                        Else 'Total (all types)
                                            For j = 1 To 3
                                                iBigType = j
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            Next j
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next

                Else 'JAM 2020-05-24
                    For Each pppRow In regDS.Tables("Groups").Rows 'JAM 2020-01-05 Ask Jose
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("CohortId")
                                        rId = cRow.Item("RegionId")
                                        iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                                If iYr = 1 Then
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                YLDtoYLLratio(rId, iType, 1)

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                YLDtoYLLratio(rId, iType, 2)
                                                    'jrs 2009/09/02
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                Else
                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    End If

                                                    'Males
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    End If

                                                    'Females
                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                End If 'if yr=1, else
                                                'jrs 2009/09/02
                                                SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                'jrs 2010/03/19 End If 'if last cohort, else
                                            Else 'other totals
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            End If

                                        Else 'Total (all types)
                                            For j = 1 To 3
                                                iBigType = j
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            Next j
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next


                End If
            End If 'if not groups, else groups
        End If 'if not mental health

        IfsConn.Close()

        'jrs 2009/08/18
        If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
            'Countries
            If Not bGrp Then
                For iYr = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    For TCohort = 1 To NCohorts
                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + TempPopDatabase(TCohort, 1, iYr) * YLDtoPOPratio(1, 1)
                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + TempPopDatabase(TCohort, 2, iYr) * YLDtoPOPratio(1, 2)
                    Next TCohort
                Next iYr
            Else 'Groups
                For iYr = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    For TCohort = 1 To NCohorts
                        For Each rId In RegsInGrp
                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + POPMales(rId, TCohort, iYr) * YLDtoPOPratio(rId, 1)
                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + POPFemales(rId, TCohort, iYr) * YLDtoPOPratio(rId, 2)
                        Next rId
                    Next TCohort
                Next iYr
            End If 'if countries else groups
        End If 'if Not Mental Health, else

        'Load Final Array
        Dim y As Integer
        For y = 1 To Session("iLastYear") - Session("FirstYear") + 1
            Session("mPLOT")(y, TotalGraphs) = 0
            For TCohort = 0 To NCohorts
                'If y = 34 Then Debug.Print TempDatabase(TCohort, 2, y)
                If sGender = "Male" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 1, y)
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 2, y)
                Else
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + (TempDatabase(TCohort, 1, y) +
                    TempDatabase(TCohort, 2, y))
                End If
            Next TCohort
        Next y



        Session("mLegends")(1, TotalGraphs) = "HLYLD"
        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        If bGrp And False Then 'mnulblAlias.Checked Then
            'Read and load all aliases
            Dim db2 As String, s As String
            Dim Tbl As DataTable
            db2 = Session("IFs_MDB2")
            SetDataTablefromAccess(db2, "SELECT * FROM " & GroupIDNew, "GroupName", Tbl)
            s = SeekValue(Tbl, sReg, "Alias")
            If s > "" Then
                Session("mLegends")(2, TotalGraphs) = s
            End If
        Else
            Session("mLegends")(2, TotalGraphs) = sReg
        End If

        If sType <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sType
        Else
            Session("mLegends")(3, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(4, TotalGraphs) = sGender
        Else
            Session("mLegends")(4, TotalGraphs) = "All Genders"
        End If

        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")

            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()
        End If


    End Sub

    Private Sub LoadYLDWORK(ByVal TotalGraphs As Integer, ByVal sFileName As String,
    ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    ByVal sGender As String, ByRef db As OleDb.OleDbConnection)
        'dbM is the database to read Mortality results
        'db is the database to read YLDtoYLL ratios

        Dim Tindex As Integer
        Dim SetNo%
        Dim rId As Integer
        Dim tbl2 As OleDb.OleDbDataReader
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        Dim RegionIdGroup() As Integer
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single
        Dim sRegs As String
        Dim YLDtoYLLratio(0, 0, 0) As Single
        Dim iType As Integer
        Dim iBigType As Integer
        Dim YLDtoPOPratio(0, 0) As Single
        'jrs 2009/09/02
        Dim SaveLastYearMort(0, 0, 0, 0) As Single
        Dim SaveLastYearMorb(0, 0, 0, 0) As Single
        Dim YLDtoYLLratioGrowthPortion(0, 0) As Single
        ReDim YLDtoYLLratioGrowthPortion(UBound(Session("PLOT"), 1), 14)

        Dim iYr As Integer

        'JAM 2020-05-24
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable


        DTsqlite = New DataTable
        DTsqlite2 = New DataTable


        'create connecion
        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        IfsConn.Open()
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        Dim ifsCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        ifsCMD.Connection = IfsConn
        'myCMD.Connection = dbM

        'JAM 2020-06-01
        Dim dbM As OleDb.OleDbConnection
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM

        End If

        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim i As Integer, j As Integer
        Dim RegsInGrp As Collection

        regDS = New DataSet

        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"
                iType = 1
            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"
                iType = 2
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"
                iType = 3
            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"
                iType = 4
            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"
                iType = 5
            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"
                iType = 6
            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"
                iType = 7
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"
                iType = 8
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"
                iType = 9
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"
                iType = 10
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"
                iType = 11
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"
                iType = 12
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"
                iType = 13
            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                sFemaleMortFld = "RespInfFeMales"
                iType = 14
            Case "MentalHealth"
                    'jrs 2009/08/18
                    'different logic, not from yll, but from ratio with population
            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                sFemaleMortFld = "MortPerFeMalesComDis"
                iBigType = 1
            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                sFemaleMortFld = "MortPerFeMalesNonComDis"
                iBigType = 2
            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                sFemaleMortFld = "MortPerFeMalesInjuries"
                iBigType = 3
            Case "Total"
                'need to read all 6 fields
        End Select

        ReDim TempPopDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)

        'jrs 2009/09/02
        'Set tbl2 = db.OpenRecordset("HealthRatioYLDtoYLLGrowthPortion")
        'tbl2.Index = "DiseaseType"
        Dim k As Integer
        Dim morbtomortidx As Integer
        morbtomortidx = -1
        Dim sBinFile As String
        Dim BinFileIdx As Integer
        BinFileIdx = -1
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then

            'sBinFile = "Working.run"
            'BinFileIdx = 0

            'JAM 2020-05-25
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = "Working.run"
                BinFileIdx = 0
            Else
                sBinFile = "Working.run.db"
                BinFileIdx = 0
            End If


        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            'sBinFile = "ifsBase.run"
            'BinFileIdx = 1

            'JAM 2020-05-25
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = "ifsBase.run"
                BinFileIdx = 1
            Else
                sBinFile = "ifsBase.run.db"
                BinFileIdx = 1
            End If

        Else
            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN"), "IFSPOP", "")
                For k = 2 To lstFiles.Items.Count - 1
                    If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
                        BinFileIdx = k
                        Exit For
                    End If
                Next k


            Else
                sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN.DB"), "IFSPOP", "")
                For k = 2 To lstFiles.Items.Count - 1
                    If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
                        BinFileIdx = k
                        Exit For
                    End If
                Next k
            End If

        End If


        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For k = 1 To 14
            'jrs 2010/01/18
            'tbl2.Seek "=", GRPS(27, k)
            'YLDtoYLLratioGrowthPortion(k) = tbl2.Fields("GrowthPortionTarget")
            gLoadForDisplayNET("hlmorbtomortgthport", morbtomortidx, GRPS(27, k), k, "", -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
            k = 1, k = 14, False, 1, YLDtoYLLratioGrowthPortion, k, 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            'gLoadForDisplay("hlmorbtomortgthport", morbtomortidx, GRPS(27, k), k, "", -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
            'k = 1, k = 14, False, 1, YLDtoYLLratioGrowthPortion, k, 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            'Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Next k

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then

            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where NCohorts > 4 AND NCohorts < 14 AND Region = " &
            Str(iReg))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    iYr = rdr.Item("Years") - Session("FirstYear") + 1
                    TempPopDatabase(rdr.Item("NCohorts"), 1, iYr) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2, iYr) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()
            Else


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where CohortId > 4 AND CohortId < 14 AND RegionId = " &
                                            Str(iReg)
                vSQLiteReader = vSQLiteCommand.ExecuteReader()



                While (vSQLiteReader.Read())
                    iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                    TempPopDatabase(vSQLiteReader("CohortId"), 1, iYr) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2, iYr) = vSQLiteReader("PopulationFemales")
                End While

            End If
            'jrs 2009/08/18
            If sType <> "MentalHealth" Then
                ReDim YLDtoYLLratio(1, 14, 2)
                tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthRatioYLDYLLRegion Where RegionId = " &
                Str(iReg) & " AND code <> 15") 'jrs 2019/08/26 added and code <> 15
                Do While tbl2.Read
                    YLDtoYLLratio(1, tbl2.Item("Code"), tbl2.Item("Gender")) =
                    tbl2.Item("RatioYLDYLL")
                Loop
                tbl2.Close()
            End If

            'jrs 2009/08/18
            If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                ReDim YLDtoPOPratio(1, 2)
                tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthMentalHealthReg Where Cohort = -1 AND RegionId = " &
                Str(iReg))
                Do While tbl2.Read
                    YLDtoPOPratio(1, tbl2.Item("Gender")) =
                    tbl2.Item("YLDtoPOP")
                Loop
                tbl2.Close()
            End If

        Else
            'LoadRegs(sReg, RegionIdGroup, GroupTot)
            ReDim POPMales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
            ReDim POPFemales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
            RegsInGrp = New Collection

            Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Region, NCohorts", dbM)

            regDS = New DataSet


            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'JAM 2020-05-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
                        " WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Region, NCohorts", dbM)
            Else

                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case" &
                                            " WHERE CohortId > 4 AND CohortId < 14 ORDER BY RegionId, CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)


                dtnew = DTsqlite.Clone
                Dim d2 As DataColumn = dtnew.Columns("RegionId")
                d2.DataType = System.Type.GetType("System.Int32")

                For Each row As DataRow In DTsqlite.Rows
                    dtnew.ImportRow(row)
                Next
                regDS.Tables.Add(dtnew)


            End If








            grpDA.Fill(regDS, "Groups")
            regDA.Fill(regDS, "Regions")
            regIDDA.Fill(regDS, "RegionIDs")
            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                popDA.Fill(regDS, "Population")
            End If

            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

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


            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                For Each pppRow In regDS.Tables("Groups").Rows 'JAM 2020-01-05 Ask Jose
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                RegsInGrp.Add(pRow.Item("RegionNum"), pRow.Item("RegionNum"))
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows 'JAM 2020-01-05 Ask Jose
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                RegsInGrp.Add(pRow.Item("RegionNum"), pRow.Item("RegionNum"))
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1, iYr) =
                                TempPopDatabase(cRow.Item("CohortId"), 1, iYr) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2, iYr) =
                                TempPopDatabase(cRow.Item("CohortId"), 2, iYr) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next


            End If


            'jrs 2009/08/18
            If sType <> "MentalHealth" Then
                ReDim YLDtoYLLratio(NR, 14, 2)
                Dim ratDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM HealthRatioYLDYLLRegion" &
                " WHERE code <> 15 ORDER BY RegionID", db)
                ratDA.Fill(regDS, "YLDYLLRatio")
                Dim regRatRel As DataRelation = regDS.Relations.Add("RegYLDYLLRatio",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("YLDYLLRatio").Columns("RegionID"))
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                rId = pRow.Item("RegionNum")
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regRatRel)
                                    YLDtoYLLratio(rId, cRow.Item("Code"), cRow.Item("Gender")) =
                                    cRow.Item("RatioYLDYLL")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            'jrs 2009/08/18
            If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                ReDim YLDtoPOPratio(NR, 2)
                Dim ratDA2 As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM HealthMentalHealthReg Where Cohort = -1 ORDER BY RegionID", db)
                ratDA2.Fill(regDS, "YLDPOPRatio")
                Dim regRat2Rel As DataRelation = regDS.Relations.Add("RegYLDPOPRatio",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("YLDPOPRatio").Columns("RegionID"))

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                rId = pRow.Item("RegionNum")
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regRat2Rel)
                                    YLDtoPOPratio(rId, cRow.Item("Gender")) =
                                    cRow.Item("YLDtoPOP")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

        End If
        'End take population

        'jrs 2009/09/02
        Dim MortGrowthRt As Single
        If bGrp Then
            ReDim SaveLastYearMort(NCohorts, 2, NR, 14)
            ReDim SaveLastYearMorb(NCohorts, 2, NR, 14)
        Else
            ReDim SaveLastYearMort(NCohorts, 2, 1, 14)
            ReDim SaveLastYearMorb(NCohorts, 2, 1, 14)
        End If

        'jrs 2009/12/14
        'jrs 2010/03/18 Dim years As Single
        Dim yearsM As Single, yearsF As Single

        'JRS 2009/08/18
        If sType <> "MentalHealth" Then
            '   Change the data source
            '   ----------------------
            If Not bGrp Then
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where NCohorts > 4 AND NCohorts < 14 AND Region = " & Str(iReg) & " ORDER BY Years")

                    Do While rdr.Read 'Not tbl2.Rows.Count = i2
                        TCohort = rdr.Item("NCohorts")
                        iYr = rdr.Item("Years") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                If iYr = 1 Then
                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * YLDtoYLLratio(1, iType, 1)
                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * YLDtoYLLratio(1, iType, 2)
                                    'jrs 2009/09/02
                                    SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                    SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                Else
                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    End If

                                    'males
                                    TempDatabase(TCohort, 1, iYr) = SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    End If

                                    'females
                                    TempDatabase(TCohort, 2, iYr) = SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                End If 'if yr = 1, else
                                'jrs 2009/09/02
                                SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                'jrs 2010/03/19 End If 'if last cohort, else
                            Else 'Other totals
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    'jrs 2009/09/02
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1, else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i 'for each type
                            End If 'if not subtotals for the 3 big
                        Else 'Total (all types)
                            For j = 1 To 3
                                iBigType = j
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, rdr.Item(sMaleMortFld)) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, rdr.Item(sFemaleMortFld)) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1 else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i
                            Next j
                        End If
                    Loop
                    rdr.Close()

                Else


                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where CohortId > 4 AND CohortId < 14 AND RegionId = " & Str(iReg) & " ORDER BY TimeId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    While (vSQLiteReader.Read())
                        TCohort = vSQLiteReader("CohortId")
                        iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                If iYr = 1 Then
                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * YLDtoYLLratio(1, iType, 1)
                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * YLDtoYLLratio(1, iType, 2)
                                    'jrs 2009/09/02
                                    SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                    SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                Else
                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    End If

                                    'males
                                    TempDatabase(TCohort, 1, iYr) = SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    End If

                                    'females
                                    TempDatabase(TCohort, 2, iYr) = SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                End If 'if yr = 1, else
                                'jrs 2009/09/02
                                SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                'jrs 2010/03/19 End If 'if last cohort, else
                            Else 'Other totals
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    'jrs 2009/09/02
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1, else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i 'for each type
                            End If 'if not subtotals for the 3 big
                        Else 'Total (all types)
                            For j = 1 To 3
                                iBigType = j
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, vSQLiteReader(sMaleMortFld)) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, vSQLiteReader(sFemaleMortFld)) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * YLDtoYLLratio(1, iType, 2)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1 else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i
                            Next j
                        End If
                    End While




                End If

            Else 'Groups
                'Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Years, Region, NCohorts", dbM)
                'mortDA.Fill(regDS, "Mortality")

                Dim mortDA As OleDb.OleDbDataAdapter

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Years, Region, NCohorts", dbM)
                    mortDA.Fill(regDS, "Mortality")
                Else
                    'JAM 20120-05-24
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case WHERE CohortId > 4 AND CohortId < 14 ORDER BY TimeId, RegionId, CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()
                    DTsqlite2.Load(vSQLiteReader)

                    dtnew = DTsqlite2.Clone
                    Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    d2.DataType = System.Type.GetType("System.Int32")

                    For Each row As DataRow In DTsqlite2.Rows
                        dtnew.ImportRow(row)
                    Next
                    regDS.Tables.Add(dtnew)

                End If




                'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Mortality").Columns("Region"))


                Dim regMortRel As DataRelation

                'JAM 2020-02-29
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mortality").Columns("Region"))
                Else


                    regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


                End If

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then


                    For Each pppRow In regDS.Tables("Groups").Rows 'JAM 2020-01-05 Ask Jose
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("NCohorts")
                                        rId = cRow.Item("Region")
                                        iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                                If iYr = 1 Then
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                YLDtoYLLratio(rId, iType, 1)

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                YLDtoYLLratio(rId, iType, 2)
                                                    'jrs 2009/09/02
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                Else
                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    End If

                                                    'Males
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    End If

                                                    'Females
                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                End If 'if yr=1, else
                                                'jrs 2009/09/02
                                                SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                'jrs 2010/03/19 End If 'if last cohort, else
                                            Else 'other totals
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            End If

                                        Else 'Total (all types)
                                            For j = 1 To 3
                                                iBigType = j
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            Next j
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next

                Else

                    For Each pppRow In regDS.Tables("Groups").Rows 'JAM 2020-01-05 Ask Jose
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("CohortId")
                                        rId = cRow.Item("RegionId")
                                        iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                                If iYr = 1 Then
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                YLDtoYLLratio(rId, iType, 1)

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                YLDtoYLLratio(rId, iType, 2)
                                                    'jrs 2009/09/02
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                Else
                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    End If

                                                    'Males
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    End If

                                                    'Females
                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                End If 'if yr=1, else
                                                'jrs 2009/09/02
                                                SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                'jrs 2010/03/19 End If 'if last cohort, else
                                            Else 'other totals
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            End If

                                        Else 'Total (all types)
                                            For j = 1 To 3
                                                iBigType = j
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * YLDtoYLLratio(rId, iType, 2)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            Next j
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next



                End If

            End If 'if not groups, else groups
        End If 'if not mental health

        IfsConn.Close()

        'jrs 2009/08/18
        If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
            'Countries
            If Not bGrp Then
                For iYr = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    For TCohort = 1 To NCohorts
                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + TempPopDatabase(TCohort, 1, iYr) * YLDtoPOPratio(1, 1)
                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + TempPopDatabase(TCohort, 2, iYr) * YLDtoPOPratio(1, 2)
                    Next TCohort
                Next iYr
            Else 'Groups
                For iYr = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    For TCohort = 1 To NCohorts
                        For Each rId In RegsInGrp
                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + POPMales(rId, TCohort, iYr) * YLDtoPOPratio(rId, 1)
                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + POPFemales(rId, TCohort, iYr) * YLDtoPOPratio(rId, 2)
                        Next rId
                    Next TCohort
                Next iYr
            End If 'if countries else groups
        End If 'if Not Mental Health, else

        'Load Final Array
        Dim y As Integer
        For y = 1 To Session("iLastYear") - Session("FirstYear") + 1
            Session("mPLOT")(y, TotalGraphs) = 0
            For TCohort = 0 To NCohorts
                'If y = 34 Then Debug.Print TempDatabase(TCohort, 2, y)
                If sGender = "Male" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 1, y)
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 2, y)
                Else
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + (TempDatabase(TCohort, 1, y) +
                    TempDatabase(TCohort, 2, y))
                End If
            Next TCohort
        Next y

        'JAM 2020-06-01
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()

        End If

        Session("mLegends")(1, TotalGraphs) = "HLYLDWORK"
        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        If bGrp And False Then 'mnulblAlias.Checked Then
            'Read and load all aliases
            Dim db2 As String, s As String
            Dim Tbl As DataTable
            db2 = Session("IFs_MDB2")
            SetDataTablefromAccess(db2, "SELECT * FROM " & GroupIDNew, "GroupName", Tbl)
            s = SeekValue(Tbl, sReg, "Alias")
            If s > "" Then
                Session("mLegends")(2, TotalGraphs) = s
            End If
        Else
            Session("mLegends")(2, TotalGraphs) = sReg
        End If

        If sType <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sType
        Else
            Session("mLegends")(3, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(4, TotalGraphs) = sGender
        Else
            Session("mLegends")(4, TotalGraphs) = "All Genders"
        End If

        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            'JAM 2020-06-01
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")
            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If
    End Sub
    'Private Sub LoadDALY(ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
    '        ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
    '        ByVal sGender As String, ByRef db As OleDb.OleDbConnection)


    Private Sub LoadDALY(ByVal TotalGraphs As Integer, ByVal sFileName As String,
            ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
            ByVal sGender As String, ByRef db As OleDb.OleDbConnection)
        'dbM is the database to read Mortality results
        'db is the database to read YLDtoYLL ratios

        Dim Tindex As Integer
        Dim SetNo%
        Dim rId As Integer
        Dim tbl2 As OleDb.OleDbDataReader
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        Dim RegionIdGroup() As Integer
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single
        Dim sRegs As String
        Dim YLDtoYLLratio(0, 0, 0) As Single
        Dim iType As Integer
        Dim iBigType As Integer
        Dim YLDtoPOPratio(0, 0) As Single
        'jrs 2009/09/02
        Dim SaveLastYearMort(0, 0, 0, 0) As Single
        Dim SaveLastYearMorb(0, 0, 0, 0) As Single
        Dim YLDtoYLLratioGrowthPortion(0, 0) As Single
        ReDim YLDtoYLLratioGrowthPortion(UBound(Session("PLOT"), 1), 14)

        Dim iYr As Integer

        'JAM 2020-05-24
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable





        'create connecion
        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        Dim ifsCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        IfsConn.Open()
        ifsCMD.Connection = IfsConn
        '            myCMD.Connection = dbM


        'JAM 2020-06-01
        Dim dbM As OleDb.OleDbConnection
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM

        End If


        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim i As Integer, j As Integer
        Dim RegsInGrp As Collection

        regDS = New DataSet




        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"
                iType = 1
            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"
                iType = 2
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"
                iType = 3
            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"
                iType = 4
            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"
                iType = 5
            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"
                iType = 6
            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"
                iType = 7
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"
                iType = 8
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"
                iType = 9
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"
                iType = 10
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"
                iType = 11
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"
                iType = 12
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"
                iType = 13
            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                sFemaleMortFld = "RespInfFeMales"
                iType = 14
            Case "MentalHealth"
                    'jrs 2009/08/18
                    'different logic, not from yll, but from ratio with population
            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                sFemaleMortFld = "MortPerFeMalesComDis"
                iBigType = 1
            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                sFemaleMortFld = "MortPerFeMalesNonComDis"
                iBigType = 2
            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                sFemaleMortFld = "MortPerFeMalesInjuries"
                iBigType = 3
            Case "Total"
                'need to read all 6 fields
        End Select

        ReDim TempPopDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)

        'jrs 2009/09/02
        'Set tbl2 = db.OpenRecordset("HealthRatioYLDtoYLLGrowthPortion")
        'tbl2.Index = "DiseaseType"
        Dim k As Integer
        Dim morbtomortidx As Integer
        morbtomortidx = -1
        Dim sBinFile As String
        Dim BinFileIdx As Integer
        BinFileIdx = -1
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            'sBinFile = "Working.run"
            'BinFileIdx = 0

            'JAM 2020-05-25
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = "Working.run"
                BinFileIdx = 0



            Else
                sBinFile = "Working.run.db"
                BinFileIdx = 0
            End If


        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            'sBinFile = "ifsBase.run"
            'BinFileIdx = 1

            'JAM 2020-05-25
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = "ifsBase.run"
                BinFileIdx = 1
            Else
                sBinFile = "ifsBase.run.db"
                BinFileIdx = 1
            End If


        Else


            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN"), "IFSPOP", "")
                For k = 2 To lstFiles.Items.Count - 1
                    If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
                        BinFileIdx = k
                        Exit For
                    End If
                Next k

            Else
                sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN.DB"), "IFSPOP", "")
                For k = 2 To lstFiles.Items.Count - 1
                    If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
                        BinFileIdx = k
                        Exit For
                    End If
                Next k

            End If
        End If

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For k = 1 To 14
            'jrs 2010/01/18
            'tbl2.Seek "=", GRPS(27, k)
            'YLDtoYLLratioGrowthPortion(k) = tbl2.Fields("GrowthPortionTarget")
            gLoadForDisplayNET("hlmorbtomortgthport", morbtomortidx, GRPS(27, k), k, "", -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
            k = 1, k = 14, False, 1, YLDtoYLLratioGrowthPortion, k, 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            'gLoadForDisplay("hlmorbtomortgthport", morbtomortidx, GRPS(27, k), k, "", -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
            'k = 1, k = 14, False, 1, YLDtoYLLratioGrowthPortion, k, 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            'Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Next k

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where Region = " &
            Str(iReg))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    iYr = rdr.Item("Years") - Session("FirstYear") + 1
                    TempPopDatabase(rdr.Item("NCohorts"), 1, iYr) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2, iYr) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where RegionId = " & Str(iReg)
                vSQLiteReader = vSQLiteCommand.ExecuteReader()



                While (vSQLiteReader.Read())

                    iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                    TempPopDatabase(vSQLiteReader("CohortId"), 1, iYr) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2, iYr) = vSQLiteReader("PopulationFemales")

                End While



            End If

            'jrs 2009/08/18
            If sType <> "MentalHealth" Then
                ReDim YLDtoYLLratio(1, 14, 2)
                tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthRatioYLDYLLRegion Where RegionId = " &
                Str(iReg) & " AND code <> 15") 'jrs 2020/05/26 added and code <> 15
                Do While tbl2.Read
                    YLDtoYLLratio(1, tbl2.Item("Code"), tbl2.Item("Gender")) =
                    tbl2.Item("RatioYLDYLL") + 1
                Loop
                tbl2.Close()
            End If

            'jrs 2009/08/18
            If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                ReDim YLDtoPOPratio(1, 2)
                tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthMentalHealthReg Where Cohort = -1 AND RegionId = " &
                Str(iReg))
                Do While tbl2.Read
                    YLDtoPOPratio(1, tbl2.Item("Gender")) =
                    tbl2.Item("YLDtoPOP")
                Loop
                tbl2.Close()
            End If
        Else
            'LoadRegs(sReg, RegionIdGroup, GroupTot)
            ReDim POPMales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
            ReDim POPFemales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
            RegsInGrp = New Collection

            Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)
            regDS = New DataSet


            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'JAM 2020-05-23
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
                                                 " ORDER BY Region, NCohorts", dbM)
            Else

                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case" &
                                             " ORDER BY RegionId, CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)


                dtnew = DTsqlite.Clone
                Dim d2 As DataColumn = dtnew.Columns("RegionId")
                d2.DataType = System.Type.GetType("System.Int32")

                For Each row As DataRow In DTsqlite.Rows
                    dtnew.ImportRow(row)
                Next
                regDS.Tables.Add(dtnew)


            End If



            grpDA.Fill(regDS, "Groups")
            regDA.Fill(regDS, "Regions")
            regIDDA.Fill(regDS, "RegionIDs")

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                popDA.Fill(regDS, "Population")
            End If
            'popDA.Fill(regDS, "Population")
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

            Dim regPopRel As DataRelation

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))
            Else


                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                regDS.Tables("pop_base_case").Columns(1))


            End If

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                RegsInGrp.Add(pRow.Item("RegionNum"), pRow.Item("RegionNum"))
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next

            Else 'JAM 2020-05-24
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                RegsInGrp.Add(pRow.Item("RegionNum"), pRow.Item("RegionNum"))
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1, iYr) =
                                TempPopDatabase(cRow.Item("CohortId"), 1, iYr) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2, iYr) =
                                TempPopDatabase(cRow.Item("CohortId"), 2, iYr) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            'jrs 2009/08/18
            If sType <> "MentalHealth" Then
                ReDim YLDtoYLLratio(NR, 14, 2)
                Dim ratDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM HealthRatioYLDYLLRegion" &
                " WHERE code <> 15 ORDER BY RegionID", db)
                ratDA.Fill(regDS, "YLDYLLRatio")
                Dim regRatRel As DataRelation = regDS.Relations.Add("RegYLDYLLRatio",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("YLDYLLRatio").Columns("RegionID"))
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                rId = pRow.Item("RegionNum")
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regRatRel)
                                    YLDtoYLLratio(rId, cRow.Item("Code"), cRow.Item("Gender")) =
                                    cRow.Item("RatioYLDYLL") + 1
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            'jrs 2009/08/18
            If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                ReDim YLDtoPOPratio(NR, 2)
                Dim ratDA2 As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM HealthMentalHealthReg Where Cohort = -1 ORDER BY RegionID", db)
                ratDA2.Fill(regDS, "YLDPOPRatio")
                Dim regRat2Rel As DataRelation = regDS.Relations.Add("RegYLDPOPRatio",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("YLDPOPRatio").Columns("RegionID"))

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                rId = pRow.Item("RegionNum")
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regRat2Rel)
                                    YLDtoPOPratio(rId, cRow.Item("Gender")) =
                                    cRow.Item("YLDtoPOP")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

        End If
        'End take population

        'jrs 2009/09/02
        Dim MortGrowthRt As Single
        If bGrp Then
            ReDim SaveLastYearMort(NCohorts, 2, NR, 14)
            ReDim SaveLastYearMorb(NCohorts, 2, NR, 14)
        Else
            ReDim SaveLastYearMort(NCohorts, 2, 1, 14)
            ReDim SaveLastYearMorb(NCohorts, 2, 1, 14)
        End If

        'jrs 2009/12/14
        'jrs 2010/03/18 Dim years As Single
        Dim yearsM As Single, yearsF As Single

        'JRS 2009/08/18
        If sType <> "MentalHealth" Then
            '   Change the data source
            '   ----------------------
            If Not bGrp Then

                'JAM 2020-01-05
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where Region = " & Str(iReg) & " ORDER BY Years")

                    Do While rdr.Read 'Not tbl2.Rows.Count = i2
                        TCohort = rdr.Item("NCohorts")
                        iYr = rdr.Item("Years") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                If iYr = 1 Then
                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * YLDtoYLLratio(1, iType, 1)
                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * YLDtoYLLratio(1, iType, 2)
                                    'jrs 2009/09/02
                                    SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                    SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                Else
                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    End If

                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM

                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    End If

                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF

                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                End If 'if yr = 1, else
                                'jrs 2009/09/02
                                SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                'jrs 2010/03/19 End If 'if last cohort, else
                            Else 'Other totals
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    'jrs 2009/09/02
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                        yearsM * YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                        yearsF * YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                        yearsM

                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                        yearsM * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                        yearsF

                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                        yearsF * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1, else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i 'for each type
                            End If 'if not subtotals for the 3 big
                        Else 'Total (all types)
                            For j = 1 To 3
                                iBigType = j
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        AMAX(0, rdr.Item(sMaleMortFld)) *
                                        TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                        YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        AMAX(0, rdr.Item(sFemaleMortFld)) *
                                        TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                        YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        AMAX(0, rdr.Item(sMaleMortFld)) *
                                        TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM

                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        SaveLastYearMorb(TCohort, 1, 1, iType) *
                                        TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                        MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        AMAX(0, rdr.Item(sFemaleMortFld)) *
                                        TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF

                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        SaveLastYearMorb(TCohort, 2, 1, iType) *
                                        TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                        MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1 else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i
                            Next j
                        End If
                    Loop
                    rdr.Close()


                Else

                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where RegionId = " & Str(iReg) & " ORDER BY TimeId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()



                    While (vSQLiteReader.Read())
                        TCohort = vSQLiteReader("CohortId")
                        iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                If iYr = 1 Then
                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * YLDtoYLLratio(1, iType, 1)
                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * YLDtoYLLratio(1, iType, 2)
                                    'jrs 2009/09/02
                                    SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                    SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                Else
                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    End If

                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM

                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    End If

                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF

                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                End If 'if yr = 1, else
                                'jrs 2009/09/02
                                SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                'jrs 2010/03/19 End If 'if last cohort, else
                            Else 'Other totals
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    'jrs 2009/09/02
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                        yearsM * YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                        yearsF * YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                        yearsM

                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                        yearsM * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                        yearsF

                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                        yearsF * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1, else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i 'for each type
                            End If 'if not subtotals for the 3 big
                        Else 'Total (all types)
                            For j = 1 To 3
                                iBigType = j
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        AMAX(0, vSQLiteReader(sMaleMortFld)) *
                                        TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                        YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        AMAX(0, vSQLiteReader(sFemaleMortFld)) *
                                        TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                        YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        AMAX(0, vSQLiteReader(sMaleMortFld)) *
                                        TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM

                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                        SaveLastYearMorb(TCohort, 1, 1, iType) *
                                        TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                        MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        AMAX(0, vSQLiteReader(sFemaleMortFld)) *
                                        TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF

                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                        SaveLastYearMorb(TCohort, 2, 1, iType) *
                                        TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                        MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1 else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i
                            Next j
                        End If
                    End While
                End If

            Else 'Groups
                'Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase ORDER BY Years, Region, NCohorts", dbM)
                'mortDA.Fill(regDS, "Mortality")

                Dim mortDA As OleDb.OleDbDataAdapter

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase ORDER BY Years, Region, NCohorts", dbM)
                    mortDA.Fill(regDS, "Mortality")
                Else
                    'JAM 2020-05-24
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case ORDER BY TimeId, RegionId, CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()
                    DTsqlite2.Load(vSQLiteReader)

                    dtnew = DTsqlite2.Clone
                    Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    d2.DataType = System.Type.GetType("System.Int32")

                    For Each row As DataRow In DTsqlite2.Rows
                        dtnew.ImportRow(row)
                    Next
                    regDS.Tables.Add(dtnew)

                End If




                'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Mortality").Columns("Region"))

                Dim regMortRel As DataRelation

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mortality").Columns("Region"))
                Else


                    regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


                End If




                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    For Each pppRow In regDS.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'JAM 2020-01-05 Ask Jose
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("NCohorts")
                                        rId = cRow.Item("Region")
                                        iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                                If iYr = 1 Then
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                YLDtoYLLratio(rId, iType, 1)

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                YLDtoYLLratio(rId, iType, 2)
                                                    'jrs 2009/09/02
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                Else
                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    End If

                                                    'Males
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    End If

                                                    'Females
                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                End If 'if yr=1, else
                                                'jrs 2009/09/02
                                                SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                'jrs 2010/03/19 End If 'if last cohort, else
                                            Else 'other totals
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            End If

                                        Else 'Total (all types)
                                            For j = 1 To 3
                                                iBigType = j
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM

                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            Next j
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next

                Else

                    For Each pppRow In regDS.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel) 'JAM 2020-01-05 Ask Jose
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("CohortId")
                                        rId = cRow.Item("RegionId")
                                        iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                                If iYr = 1 Then
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                YLDtoYLLratio(rId, iType, 1)

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                YLDtoYLLratio(rId, iType, 2)
                                                    'jrs 2009/09/02
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                Else
                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    End If

                                                    'Males
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    End If

                                                    'Females
                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                End If 'if yr=1, else
                                                'jrs 2009/09/02
                                                SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                'jrs 2010/03/19 End If 'if last cohort, else
                                            Else 'other totals
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            End If

                                        Else 'Total (all types)
                                            For j = 1 To 3
                                                iBigType = j
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM

                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            Next j
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next


                End If



            End If 'if not groups, else groups
        End If 'if not mental health

        IfsConn.Close()

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            dbM.Close()

        End If


        'jrs 2009/08/18
        If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
            'Countries
            If Not bGrp Then
                For iYr = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    For TCohort = 1 To NCohorts
                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + TempPopDatabase(TCohort, 1, iYr) * YLDtoPOPratio(1, 1)
                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + TempPopDatabase(TCohort, 2, iYr) * YLDtoPOPratio(1, 2)
                    Next TCohort
                Next iYr
            Else 'Groups
                For iYr = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    For TCohort = 1 To NCohorts
                        For Each rId In RegsInGrp
                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + POPMales(rId, TCohort, iYr) * YLDtoPOPratio(rId, 1)
                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + POPFemales(rId, TCohort, iYr) * YLDtoPOPratio(rId, 2)
                        Next rId
                    Next TCohort
                Next iYr
            End If 'if countries else groups
        End If 'if Not Mental Health, else

        'Load Final Array
        Dim y As Integer
        For y = 1 To Session("iLastYear") - Session("FirstYear") + 1
            Session("mPLOT")(y, TotalGraphs) = 0
            For TCohort = 0 To NCohorts
                'If y = 34 Then Debug.Print TempDatabase(TCohort, 2, y)
                If sGender = "Male" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 1, y)
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 2, y)
                Else
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + (TempDatabase(TCohort, 1, y) +
                    TempDatabase(TCohort, 2, y))
                End If
            Next TCohort
        Next y
        Session("mLegends")(1, TotalGraphs) = "HLDALY"
        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        If bGrp And False Then 'mnulblAlias.Checked Then
            'Read and load all aliases
            Dim db2 As String, s As String
            Dim Tbl As DataTable
            db2 = Session("IFs_MDB2")
            SetDataTablefromAccess(db2, "SELECT * FROM " & GroupIDNew, "GroupName", Tbl)
            s = SeekValue(Tbl, sReg, "Alias")
            If s > "" Then
                Session("mLegends")(2, TotalGraphs) = s
            End If
        Else
            Session("mLegends")(2, TotalGraphs) = sReg
        End If

        If sType <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sType
        Else
            Session("mLegends")(3, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(4, TotalGraphs) = sGender
        Else
            Session("mLegends")(4, TotalGraphs) = "All Genders"
        End If

        If sFileName = "ifsPopWork.mdb" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")
        End If
    End Sub

    Private Sub LoadDALYWORK(ByVal TotalGraphs As Integer, ByVal sFileName As String,
                ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
                ByVal sGender As String, ByRef db As OleDb.OleDbConnection)
        'dbM is the database to read Mortality results
        'db is the database to read YLDtoYLL ratios

        Dim Tindex As Integer
        Dim SetNo%
        Dim rId As Integer
        Dim tbl2 As OleDb.OleDbDataReader
        Dim TCohort As Integer
        Dim sMaleMortFld As String, sFemaleMortFld As String

        Dim TempDatabase(0, 0, 0) As Single
        Dim TempPopDatabase(0, 0, 0) As Single
        Dim RegionIdGroup() As Integer
        'Weighted Average for Population
        Dim POPMales(0, 0, 0) As Single
        Dim POPFemales(0, 0, 0) As Single
        Dim sRegs As String
        Dim YLDtoYLLratio(0, 0, 0) As Single
        Dim iType As Integer
        Dim iBigType As Integer
        Dim YLDtoPOPratio(0, 0) As Single
        'jrs 2009/09/02
        Dim SaveLastYearMort(0, 0, 0, 0) As Single
        Dim SaveLastYearMorb(0, 0, 0, 0) As Single
        Dim YLDtoYLLratioGrowthPortion(0, 0) As Single
        ReDim YLDtoYLLratioGrowthPortion(UBound(Session("PLOT"), 1), 14)

        Dim iYr As Integer


        'JAM 2020-05-24
        Dim DTsqlite As DataTable
        Dim DTsqlite2 As DataTable
        Dim dtnew As DataTable

        DTsqlite = New DataTable
        DTsqlite2 = New DataTable





        'create connecion
        Dim IfsConn As OleDb.OleDbConnection
        IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
        IfsConn.Open()
        Dim rdr As OleDb.OleDbDataReader
        Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        Dim ifsCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
        ifsCMD.Connection = IfsConn
        'myCMD.Connection = dbM
        Dim dbM As OleDb.OleDbConnection
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            dbM = ADO_OleDB_CreateConnection(Session("PutFdrvRun") & sFileName)
            dbM.Open()
            myCMD.Connection = dbM

        End If

        Dim regDS As DataSet
        Dim grpRegRel As DataRelation
        Dim regRegIdRel As DataRelation
        Dim pppRow, ppRow, pRow, cRow As DataRow
        Dim i As Integer, j As Integer
        Dim RegsInGrp As Collection

        regDS = New DataSet


        Select Case sType
            Case "OthCommumDis"
                sMaleMortFld = "ComDisNoAIDSMales"
                sFemaleMortFld = "ComDisNoAIDSFemales"
                iType = 1
            Case "MaligNeoPl"
                sMaleMortFld = "MaligNeoPlasMales"
                sFemaleMortFld = "MaligNeoPlasFemales"
                iType = 2
            Case "CardioVasc"
                sMaleMortFld = "CardioVascMales"
                sFemaleMortFld = "CardioVascFemales"
                iType = 3
            Case "Digestive"
                sMaleMortFld = "DigestiveMales"
                sFemaleMortFld = "DigestiveFemales"
                iType = 4
            Case "Respiratory"
                sMaleMortFld = "RespiratoryMales"
                sFemaleMortFld = "RespiratoryFemales"
                iType = 5
            Case "OtherNonComm"
                sMaleMortFld = "OtherG2Males"
                sFemaleMortFld = "OtherG2Females"
                iType = 6
            Case "TrafficAcc"
                sMaleMortFld = "RoadTrafficAccMales"
                sFemaleMortFld = "RoadTrafficAccFemales"
                iType = 7
            Case "UnIntInj"
                sMaleMortFld = "OtherunintInjMales"
                sFemaleMortFld = "OtherunintInjFemales"
                iType = 8
            Case "IntInj"
                sMaleMortFld = "IntenInjMales"
                sFemaleMortFld = "IntenInjFemales"
                iType = 9
            Case "Diabetes"
                sMaleMortFld = "DiabetesMales"
                sFemaleMortFld = "DiabetesFemales"
                iType = 10
            Case "AIDS"
                sMaleMortFld = "AIDSMales"
                sFemaleMortFld = "AIDSFemales"
                iType = 11
            Case "Diarrhea" 'jrs 2009/08/14
                sMaleMortFld = "DiarrheaMales"
                sFemaleMortFld = "DiarrheaFemales"
                iType = 12
            Case "Malaria"
                sMaleMortFld = "MalariaMales"
                sFemaleMortFld = "MalariaFemales"
                iType = 13
            Case "RespInfec"
                sMaleMortFld = "RespInfMales"
                sFemaleMortFld = "RespInfFeMales"
                iType = 14
            Case "MentalHealth"
                    'jrs 2009/08/18
                    'different logic, not from yll, but from ratio with population
            Case "TotComDis"
                sMaleMortFld = "MortPerMalesComDis"
                sFemaleMortFld = "MortPerFeMalesComDis"
                iBigType = 1
            Case "TotNonComDis"
                sMaleMortFld = "MortPerMalesNonComDis"
                sFemaleMortFld = "MortPerFeMalesNonComDis"
                iBigType = 2
            Case "TotInjuries"
                sMaleMortFld = "MortPerMalesInjuries"
                sFemaleMortFld = "MortPerFeMalesInjuries"
                iBigType = 3
            Case "Total"
                'need to read all 6 fields
        End Select

        ReDim TempPopDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)
        ReDim TempDatabase(NCohorts, 2, Session("iLastYear") - Session("FirstYear") + 1)

        'jrs 2009/09/02
        'Set tbl2 = db.OpenRecordset("HealthRatioYLDtoYLLGrowthPortion")
        'tbl2.Index = "DiseaseType"
        Dim k As Integer
        Dim morbtomortidx As Integer
        morbtomortidx = -1
        Dim sBinFile As String
        Dim BinFileIdx As Integer
        BinFileIdx = -1
        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            'sBinFile = "Working.run"
            'BinFileIdx = 0

            'JAM 2020-05-25
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = "Working.run"
                BinFileIdx = 0
            Else
                sBinFile = "Working.run.db"
                BinFileIdx = 0
            End If


        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            'sBinFile = "ifsBase.run"
            'BinFileIdx = 1

            'JAM 2020-05-25
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = "ifsBase.run"
                BinFileIdx = 1
            Else
                sBinFile = "ifsBase.run.db"
                BinFileIdx = 1
            End If


        Else

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN"), "IFSPOP", "")
                For k = 2 To lstFiles.Items.Count - 1
                    If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
                        BinFileIdx = k
                        Exit For
                    End If
                Next k

            Else
                sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN.DB"), "IFSPOP", "")
                For k = 2 To lstFiles.Items.Count - 1
                    If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
                        BinFileIdx = k
                        Exit For
                    End If
                Next k

            End If
        End If

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For k = 1 To 14
            'jrs 2010/01/18
            'tbl2.Seek "=", GRPS(27, k)
            'YLDtoYLLratioGrowthPortion(k) = tbl2.Fields("GrowthPortionTarget")
            gLoadForDisplayNET("hlmorbtomortgthport", morbtomortidx, GRPS(27, k), k, "", -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
            k = 1, k = 14, False, 1, YLDtoYLLratioGrowthPortion, k, 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            'gLoadForDisplay("hlmorbtomortgthport", morbtomortidx, GRPS(27, k), k, "", -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
            'k = 1, k = 14, False, 1, YLDtoYLLratioGrowthPortion, k, 1, Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            'Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Next k

        'take population
        '   Change the data source
        '   ----------------------
        Tindex = 2
        If Not bGrp Then

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM PopBaseCase Where NCohorts > 4 AND NCohorts < 14 AND Region = " &
            Str(iReg))

                Do While rdr.Read 'Not tbl2.Rows.Count = i2
                    iYr = rdr.Item("Years") - Session("FirstYear") + 1
                    TempPopDatabase(rdr.Item("NCohorts"), 1, iYr) = rdr.Item("PopulationMales")
                    TempPopDatabase(rdr.Item("NCohorts"), 2, iYr) = rdr.Item("PopulationFemales")
                Loop
                rdr.Close()
            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case Where CohortId > 4 AND CohortId < 14 AND RegionId = " &
            Str(iReg)
                vSQLiteReader = vSQLiteCommand.ExecuteReader()



                While (vSQLiteReader.Read())
                    iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                    TempPopDatabase(vSQLiteReader("CohortId"), 1, iYr) = vSQLiteReader("PopulationMales")
                    TempPopDatabase(vSQLiteReader("CohortId"), 2, iYr) = vSQLiteReader("PopulationFemales")

                End While
            End If

            'jrs 2009/08/18
            If sType <> "MentalHealth" Then
                ReDim YLDtoYLLratio(1, 14, 2)
                tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthRatioYLDYLLRegion Where RegionId = " &
                Str(iReg) & " AND code <> 15") 'jrs 2020/05/26 added and code <> 15
                Do While tbl2.Read
                    YLDtoYLLratio(1, tbl2.Item("Code"), tbl2.Item("Gender")) =
                    tbl2.Item("RatioYLDYLL") + 1
                Loop
                tbl2.Close()
            End If

            'jrs 2009/08/18
            If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                ReDim YLDtoPOPratio(1, 2)
                tbl2 = ADO_OleDB_GetReader(ifsCMD, "SELECT * FROM HealthMentalHealthReg Where Cohort = -1 AND RegionId = " &
                Str(iReg))
                Do While tbl2.Read
                    YLDtoPOPratio(1, tbl2.Item("Gender")) =
                    tbl2.Item("YLDtoPOP")
                Loop
                tbl2.Close()
            End If
        Else
            'LoadRegs(sReg, RegionIdGroup, GroupTot)
            ReDim POPMales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
            ReDim POPFemales(NR, NCohorts, Session("iLastYear") - Session("FirstYear") + 1)
            RegsInGrp = New Collection

            Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Region, NCohorts", dbM)
            regDS = New DataSet

            Dim popDA As OleDb.OleDbDataAdapter
            regDS = New DataSet
            'Dim popDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
            '" ORDER BY Region, NCohorts", dbM)

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                popDA = New OleDb.OleDbDataAdapter("SELECT * FROM PopBaseCase" &
                    " WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Region, NCohorts", dbM)
            Else

                'JAM 2019-12-23
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM Pop_Base_Case" &
                                     " WHERE CohortId > 4 AND CohortId < 14 ORDER BY RegionId, CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DTsqlite.Load(vSQLiteReader)
                'regDS.Tables.Add(DTsqlite)


                dtnew = DTsqlite.Clone
                Dim d2 As DataColumn = dtnew.Columns("RegionId")
                d2.DataType = System.Type.GetType("System.Int32")

                For Each row As DataRow In DTsqlite.Rows
                    dtnew.ImportRow(row)
                Next
                regDS.Tables.Add(dtnew)


            End If




            grpDA.Fill(regDS, "Groups")
            regDA.Fill(regDS, "Regions")
            regIDDA.Fill(regDS, "RegionIDs")

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                popDA.Fill(regDS, "Population")

            End If
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            'Dim regPopRel As DataRelation = regDS.Relations.Add("RegPopulation",
            '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
            '                                     regDS.Tables("Population").Columns("Region"))

            Dim regPopRel As DataRelation


            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Population").Columns("Region"))
            Else
                regPopRel = regDS.Relations.Add("RegPopulation",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("pop_base_case").Columns("RegionId"))
            End If

            'JAM 2020-05-24
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                RegsInGrp.Add(pRow.Item("RegionNum"), pRow.Item("RegionNum"))
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) =
                                TempPopDatabase(cRow.Item("NCohorts"), 1, iYr) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) =
                                TempPopDatabase(cRow.Item("NCohorts"), 2, iYr) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("Region"), cRow.Item("NCohorts"), iYr) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next

            Else
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                RegsInGrp.Add(pRow.Item("RegionNum"), pRow.Item("RegionNum"))
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regPopRel)
                                    iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                    'Console.WriteLine(vbTab & cRow("OrderID").ToString())
                                    TempPopDatabase(cRow.Item("CohortId"), 1, iYr) =
                                TempPopDatabase(cRow.Item("CohortId"), 1, iYr) + cRow.Item("PopulationMales")
                                    TempPopDatabase(cRow.Item("CohortId"), 2, iYr) =
                                TempPopDatabase(cRow.Item("CohortId"), 2, iYr) + cRow.Item("PopulationFemales")

                                    POPMales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                cRow.Item("PopulationMales")
                                    POPFemales(cRow.Item("RegionId"), cRow.Item("CohortId"), iYr) =
                                cRow.Item("PopulationFemales")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            'jrs 2009/08/18
            If sType <> "MentalHealth" Then
                ReDim YLDtoYLLratio(NR, 14, 2)
                Dim ratDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM HealthRatioYLDYLLRegion" &
                " WHERE code <> 15 ORDER BY RegionID", db)
                ratDA.Fill(regDS, "YLDYLLRatio")
                Dim regRatRel As DataRelation = regDS.Relations.Add("RegYLDYLLRatio",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("YLDYLLRatio").Columns("RegionID"))
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                rId = pRow.Item("RegionNum")
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regRatRel)
                                    YLDtoYLLratio(rId, cRow.Item("Code"), cRow.Item("Gender")) =
                                    cRow.Item("RatioYLDYLL") + 1
                                Next
                            Next
                        Next
                    End If
                Next
            End If

            'jrs 2009/08/18
            If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
                ReDim YLDtoPOPratio(NR, 2)
                Dim ratDA2 As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM HealthMentalHealthReg Where Cohort = -1 ORDER BY RegionID", db)
                ratDA2.Fill(regDS, "YLDPOPRatio")
                Dim regRat2Rel As DataRelation = regDS.Relations.Add("RegYLDPOPRatio",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("YLDPOPRatio").Columns("RegionID"))

                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = sReg Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                rId = pRow.Item("RegionNum")
                                'Console.WriteLine(pRow("CustomerID").ToString())
                                For Each cRow In pRow.GetChildRows(regRat2Rel)
                                    YLDtoPOPratio(rId, cRow.Item("Gender")) =
                                    cRow.Item("YLDtoPOP")
                                Next
                            Next
                        Next
                    End If
                Next
            End If

        End If
        'End take population

        'jrs 2009/09/02
        Dim MortGrowthRt As Single
        If bGrp Then
            ReDim SaveLastYearMort(NCohorts, 2, NR, 14)
            ReDim SaveLastYearMorb(NCohorts, 2, NR, 14)
        Else
            ReDim SaveLastYearMort(NCohorts, 2, 1, 14)
            ReDim SaveLastYearMorb(NCohorts, 2, 1, 14)
        End If

        'jrs 2009/12/14
        'jrs 2010/03/18 Dim years As Single
        Dim yearsM As Single, yearsF As Single

        'JRS 2009/08/18
        If sType <> "MentalHealth" Then
            '   Change the data source
            '   ----------------------
            If Not bGrp Then
                'JAM 2020-01-05
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    rdr = ADO_OleDB_GetReader(myCMD, "SELECT * FROM MorDetBaseCase Where NCohorts > 4 AND NCohorts < 14 AND Region = " & Str(iReg) & " ORDER BY Years")

                    Do While rdr.Read 'Not tbl2.Rows.Count = i2
                        TCohort = rdr.Item("NCohorts")
                        iYr = rdr.Item("Years") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                If iYr = 1 Then
                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * YLDtoYLLratio(1, iType, 1)
                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * YLDtoYLLratio(1, iType, 2)
                                    'jrs 2009/09/02
                                    SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                    SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                Else
                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    End If

                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM

                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    End If

                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF

                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                End If 'if yr = 1, else
                                'jrs 2009/09/02
                                SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                'jrs 2010/03/19 End If 'if last cohort, else
                            Else 'Other totals
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    'jrs 2009/09/02
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, rdr.Item(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM

                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, rdr.Item(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF

                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1, else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i 'for each type
                            End If 'if not subtotals for the 3 big
                        Else 'Total (all types)
                            For j = 1 To 3
                                iBigType = j
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, rdr.Item(sMaleMortFld)) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, rdr.Item(sFemaleMortFld)) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, rdr.Item(sMaleMortFld)) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM

                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, rdr.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, rdr.Item(sFemaleMortFld)) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF

                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1 else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, rdr.Item(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, rdr.Item(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i
                            Next j
                        End If
                    Loop
                    rdr.Close()

                Else


                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case Where CohortId > 4 AND CohortId < 14 AND RegionId = " & Str(iReg) & " ORDER BY TimeId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()

                    While (vSQLiteReader.Read())
                        TCohort = vSQLiteReader("CohortId")
                        iYr = vSQLiteReader("TimeId") - Session("FirstYear") + 1
                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                        Else
                            'jrs 2009/12/14
                            If Session("bUseAgeWg") = False Then
                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                If Session("DiscountRt") <> 0 Then
                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                End If
                            Else
                                'jrs 2010/03/19
                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                            End If
                        End If

                        If Not (sType = "Total") Then
                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                If iYr = 1 Then
                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * YLDtoYLLratio(1, iType, 1)
                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * YLDtoYLLratio(1, iType, 2)
                                    'jrs 2009/09/02
                                    SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                    SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                Else
                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    End If

                                    'males
                                    TempDatabase(TCohort, 1, iYr) = AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM

                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                yearsM * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                    'jrs 2009/09/02 retrieve last years mortality
                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)                                    'compute mortality rate
                                    If MortGrowthRt <> 0 Then
                                        MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                    Else
                                        MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    End If

                                    'females
                                    TempDatabase(TCohort, 2, iYr) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF

                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                yearsF * MortGrowthRt

                                    SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                End If 'if yr = 1, else
                                'jrs 2009/09/02
                                SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                'jrs 2010/03/19 End If 'if last cohort, else
                            Else 'Other totals
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    'jrs 2009/09/02
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, vSQLiteReader(sMaleMortFld)) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM

                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) * TempPopDatabase(TCohort, 1, iYr) * 0.001 *
                                    yearsM * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, vSQLiteReader(sFemaleMortFld)) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF

                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) * TempPopDatabase(TCohort, 2, iYr) * 0.001 *
                                    yearsF * MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1, else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i 'for each type
                            End If 'if not subtotals for the 3 big
                        Else 'Total (all types)
                            For j = 1 To 3
                                iBigType = j
                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                    If iYr = 1 Then
                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, vSQLiteReader(sMaleMortFld)) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    YLDtoYLLratio(1, iType, 1)
                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, vSQLiteReader(sFemaleMortFld)) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    YLDtoYLLratio(1, iType, 2)
                                        'jrs 2009/09/02
                                        SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld)) * (YLDtoYLLratio(1, iType, 1) - 1)
                                        SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld)) * (YLDtoYLLratio(1, iType, 2) - 1)
                                    Else
                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 1)
                                            SaveLastYearMorb(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                        End If

                                        'males
                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    AMAX(0, vSQLiteReader(sMaleMortFld)) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM

                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                    SaveLastYearMorb(TCohort, 1, 1, iType) *
                                    TempPopDatabase(TCohort, 1, iYr) * 0.001 * yearsM *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 1, 1, iType) = SaveLastYearMorb(TCohort, 1, 1, iType) * MortGrowthRt

                                        'jrs 2009/09/02
                                        'compute mortality rate
                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, 1, iType)
                                        If MortGrowthRt <> 0 Then
                                            MortGrowthRt = ((AMAX(0, vSQLiteReader(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                        Else
                                            MortGrowthRt = YLDtoYLLratio(1, iType, 2)
                                            SaveLastYearMorb(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                        End If

                                        'females
                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    AMAX(0, vSQLiteReader(sFemaleMortFld)) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF

                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                    SaveLastYearMorb(TCohort, 2, 1, iType) *
                                    TempPopDatabase(TCohort, 2, iYr) * 0.001 * yearsF *
                                    MortGrowthRt

                                        SaveLastYearMorb(TCohort, 2, 1, iType) = SaveLastYearMorb(TCohort, 2, 1, iType) * MortGrowthRt
                                    End If 'if yr=1 else
                                    'jrs 2009/09/02
                                    SaveLastYearMort(TCohort, 1, 1, iType) = AMAX(0, vSQLiteReader(sMaleMortFld))
                                    SaveLastYearMort(TCohort, 2, 1, iType) = AMAX(0, vSQLiteReader(sFemaleMortFld))
                                    'jrs 2010/03/19 End If 'if last cohort, else
                                Next i
                            Next j
                        End If
                    End While
                End If

            Else 'Groups
                'Dim mortDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Years, Region, NCohorts", dbM)
                'mortDA.Fill(regDS, "Mortality")

                Dim mortDA As OleDb.OleDbDataAdapter

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    mortDA = New OleDb.OleDbDataAdapter("SELECT * FROM MorDetBaseCase WHERE NCohorts > 4 AND NCohorts < 14 ORDER BY Years, Region, NCohorts", dbM)
                    mortDA.Fill(regDS, "Mortality")
                Else
                    'JAM 2019-12-23
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM Mor_Det_Base_Case WHERE CohortId > 4 AND CohortId < 14 ORDER BY TimeId, RegionId, CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()
                    DTsqlite2.Load(vSQLiteReader)

                    dtnew = DTsqlite2.Clone
                    Dim d2 As DataColumn = dtnew.Columns("RegionId")
                    d2.DataType = System.Type.GetType("System.Int32")

                    For Each row As DataRow In DTsqlite2.Rows
                        dtnew.ImportRow(row)
                    Next
                    regDS.Tables.Add(dtnew)

                End If


                'Dim regMortRel As DataRelation = regDS.Relations.Add("RegMortality",
                '                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                '                                     regDS.Tables("Mortality").Columns("Region"))

                Dim regMortRel As DataRelation

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mortality").Columns("Region"))
                Else


                    regMortRel = regDS.Relations.Add("RegMortality",
                                                     regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                     regDS.Tables("Mor_Det_Base_Case").Columns("RegionId"))


                End If

                'JAM 2020-05-24
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                    For Each pppRow In regDS.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("NCohorts")
                                        rId = cRow.Item("Region")
                                        iYr = cRow.Item("Years") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                                If iYr = 1 Then
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                YLDtoYLLratio(rId, iType, 1)

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                YLDtoYLLratio(rId, iType, 2)
                                                    'jrs 2009/09/02
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                Else
                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    End If

                                                    'Males
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    End If

                                                    'Females
                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                End If 'if yr=1, else
                                                'jrs 2009/09/02
                                                SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                'jrs 2010/03/19 End If 'if last cohort, else
                                            Else 'other totals
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            End If

                                        Else 'Total (all types)
                                            For j = 1 To 3
                                                iBigType = j
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM

                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            Next j
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next

                Else
                    For Each pppRow In regDS.Tables("Groups").Rows
                        If pppRow.Item("GroupName") = sReg Then
                            For Each ppRow In pppRow.GetChildRows(grpRegRel)
                                For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                    For Each cRow In pRow.GetChildRows(regMortRel)
                                        TCohort = cRow.Item("CohortId")
                                        rId = cRow.Item("RegionId")
                                        iYr = cRow.Item("TimeId") - Session("FirstYear") + 1
                                        If Session("DiscountRt") <> 0 And Session("bUseAgeWg") Then
                                            'jrs 2010/03/19 years = -Session("AWC") * exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (exp(-(Session("AWBeta") + Session("DiscountRt")) * (MaxLIFEXP - Session("AvgAgePerCohort")(TCohort))) * (1 + (Session("AWBeta") + Session("DiscountRt")) * MaxLIFEXP) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsM = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 1)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                            yearsF = -Session("AWC") * Math.Exp(-1 * Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) / (Session("AWBeta") + Session("DiscountRt")) ^ 2 * (Math.Exp(-(Session("AWBeta") + Session("DiscountRt")) * Session("StdLifeExp")(TCohort, 2)) * (1 + (Session("AWBeta") + Session("DiscountRt")) * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) - (1 + (Session("AWBeta") + Session("DiscountRt")) * Session("AvgAgePerCohort")(TCohort)))
                                        Else
                                            'jrs 2009/12/14
                                            If Session("bUseAgeWg") = False Then
                                                'jrs 2010/03/19 years = MaxLIFEXP - Session("AvgAgePerCohort")(TCohort)
                                                yearsM = Session("StdLifeExp")(TCohort, 1)
                                                yearsF = Session("StdLifeExp")(TCohort, 2)
                                                If Session("DiscountRt") <> 0 Then
                                                    'years = years * (1/(1 + Session("DiscountRt")) ^ years)
                                                    'jrs 2010/03/19 years = (1 - exp(-1 * Session("DiscountRt") * years)) / Session("DiscountRt")
                                                    yearsM = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsM)) / Session("DiscountRt")
                                                    yearsF = (1 - Math.Exp(-1 * Session("DiscountRt") * yearsF)) / Session("DiscountRt")
                                                End If
                                            Else
                                                'jrs 2010/03/19
                                                '                        years = (-MaxLIFEXP / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * MaxLIFEXP) * Session("AWC")
                                                '                        years = years - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsM = (-(Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 1) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsM = yearsM - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                                yearsF = (-(Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort)) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * (Session("StdLifeExp")(TCohort, 2) + Session("AvgAgePerCohort")(TCohort))) * Session("AWC")
                                                yearsF = yearsF - (-Session("AvgAgePerCohort")(TCohort) / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-Session("AWBeta") * Session("AvgAgePerCohort")(TCohort)) * Session("AWC")
                                            End If
                                        End If

                                        If Not (sType = "Total") Then
                                            If Not (Mid(sType, 1, 3) = "Tot") Then
                                                If iYr = 1 Then
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                YLDtoYLLratio(rId, iType, 1)

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                YLDtoYLLratio(rId, iType, 2)
                                                    'jrs 2009/09/02
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                Else
                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    End If

                                                    'Males
                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                                    TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                    'jrs 2009/09/02
                                                    'compute mortality rate
                                                    MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                    If MortGrowthRt <> 0 Then
                                                        MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                    Else
                                                        MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    End If

                                                    'Females
                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF

                                                    TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                MortGrowthRt

                                                    SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                End If 'if yr=1, else
                                                'jrs 2009/09/02
                                                SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                'jrs 2010/03/19 End If 'if last cohort, else
                                            Else 'other totals
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + cRow.Item(sMaleMortFld) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM

                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + SaveLastYearMorb(TCohort, 1, rId, iType) *
                                                    POPMales(rId, TCohort, iYr) * 0.001 * yearsM *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + cRow.Item(sFemaleMortFld) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + SaveLastYearMorb(TCohort, 2, rId, iType) *
                                                    POPFemales(rId, TCohort, iYr) * 0.001 * yearsF *
                                                    MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            End If

                                        Else 'Total (all types)
                                            For j = 1 To 3
                                                iBigType = j
                                                For i = 1 To Session("BigTypes")(iBigType).NumSubTypes / 2
                                                    sMaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i - 1)
                                                    sFemaleMortFld = Session("BigTypes")(iBigType).getSubTypeFieldName(2 * i)
                                                    iType = Session("BigTypes")(iBigType).getTypeIdx(2 * i - 1)
                                                    If iYr = 1 Then
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * YLDtoYLLratio(rId, iType, 1)

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * YLDtoYLLratio(rId, iType, 2)
                                                        'jrs 2009/09/02
                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld)) * (YLDtoYLLratio(rId, iType, 1) - 1)
                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld)) * (YLDtoYLLratio(rId, iType, 2) - 1)
                                                    Else
                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 1, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sMaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 1)
                                                            SaveLastYearMorb(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                        End If

                                                        'Males
                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    cRow.Item(sMaleMortFld) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM

                                                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) +
                                                    SaveLastYearMorb(TCohort, 1, rId, iType) * POPMales(rId, TCohort, iYr) * 0.001 *
                                                    yearsM * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 1, rId, iType) = SaveLastYearMorb(TCohort, 1, rId, iType) * MortGrowthRt

                                                        'jrs 2009/09/02
                                                        'compute mortality rate
                                                        MortGrowthRt = SaveLastYearMort(TCohort, 2, rId, iType)
                                                        If MortGrowthRt <> 0 Then
                                                            MortGrowthRt = ((AMAX(0, cRow.Item(sFemaleMortFld)) - MortGrowthRt) / MortGrowthRt) * YLDtoYLLratioGrowthPortion(iYr, iType) + 1
                                                        Else
                                                            MortGrowthRt = YLDtoYLLratio(rId, iType, 2)
                                                            SaveLastYearMorb(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                        End If

                                                        'Females
                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    cRow.Item(sFemaleMortFld) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF

                                                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) +
                                                    SaveLastYearMorb(TCohort, 2, rId, iType) * POPFemales(rId, TCohort, iYr) * 0.001 *
                                                    yearsF * MortGrowthRt

                                                        SaveLastYearMorb(TCohort, 2, rId, iType) = SaveLastYearMorb(TCohort, 2, rId, iType) * MortGrowthRt
                                                    End If 'if yr=1, else
                                                    'jrs 2009/09/02
                                                    SaveLastYearMort(TCohort, 1, rId, iType) = AMAX(0, cRow.Item(sMaleMortFld))
                                                    SaveLastYearMort(TCohort, 2, rId, iType) = AMAX(0, cRow.Item(sFemaleMortFld))
                                                    'jrs 2010/03/19 End If 'if last cohort, else
                                                Next i
                                            Next j
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    Next


                End If

            End If 'if not groups, else groups
        End If 'if not mental health

        IfsConn.Close()

        'jrs 2009/08/18
        If sType = "MentalHealth" Or sType = "TotNonComDis" Or sType = "Total" Then
            'Countries
            If Not bGrp Then
                For iYr = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    For TCohort = 1 To NCohorts
                        TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + TempPopDatabase(TCohort, 1, iYr) * YLDtoPOPratio(1, 1)
                        TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + TempPopDatabase(TCohort, 2, iYr) * YLDtoPOPratio(1, 2)
                    Next TCohort
                Next iYr
            Else 'Groups
                For iYr = 1 To Session("iLastYear") - Session("FirstYear") + 1
                    For TCohort = 1 To NCohorts
                        For Each rId In RegsInGrp
                            TempDatabase(TCohort, 1, iYr) = TempDatabase(TCohort, 1, iYr) + POPMales(rId, TCohort, iYr) * YLDtoPOPratio(rId, 1)
                            TempDatabase(TCohort, 2, iYr) = TempDatabase(TCohort, 2, iYr) + POPFemales(rId, TCohort, iYr) * YLDtoPOPratio(rId, 2)
                        Next rId
                    Next TCohort
                Next iYr
            End If 'if countries else groups
        End If 'if Not Mental Health, else

        'Load Final Array
        Dim y As Integer
        For y = 1 To Session("iLastYear") - Session("FirstYear") + 1
            Session("mPLOT")(y, TotalGraphs) = 0
            For TCohort = 0 To NCohorts
                'If y = 34 Then Debug.Print TempDatabase(TCohort, 2, y)
                If sGender = "Male" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 1, y)
                ElseIf sGender = "Female" Then
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + TempDatabase(TCohort, 2, y)
                Else
                    Session("mPLOT")(y, TotalGraphs) = Session("mPLOT")(y, TotalGraphs) + (TempDatabase(TCohort, 1, y) +
                    TempDatabase(TCohort, 2, y))
                End If
            Next TCohort
        Next y
        Session("mLegends")(1, TotalGraphs) = "HLDALYWORK"
        'jrs 2010/02/09
        Session("mLegends")(0, TotalGraphs) = sReg
        If bGrp And False Then 'mnulblAlias.Checked Then
            'Read and load all aliases
            Dim db2 As String, s As String
            Dim Tbl As DataTable
            db2 = Session("IFs_MDB2")
            SetDataTablefromAccess(db2, "SELECT * FROM " & GroupIDNew, "GroupName", Tbl)
            s = SeekValue(Tbl, sReg, "Alias")
            If s > "" Then
                Session("mLegends")(2, TotalGraphs) = s
            End If
        Else
            Session("mLegends")(2, TotalGraphs) = sReg
        End If

        If sType <> "Total" Then
            Session("mLegends")(3, TotalGraphs) = sType
        Else
            Session("mLegends")(3, TotalGraphs) = "All Types"
        End If
        If sGender <> "Total" Then
            Session("mLegends")(4, TotalGraphs) = sGender
        Else
            Session("mLegends")(4, TotalGraphs) = "All Genders"
        End If

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            dbM.Close()
        End If


        If sFileName = "ifsPopWork.mdb" Or sFileName = "ifsPopWork.db" Then
            Session("mLegends")(5, TotalGraphs) = "Working"
        ElseIf sFileName = "ifsPop.mdb" Or sFileName = "ifsPop.db" Then
            Session("mLegends")(5, TotalGraphs) = "Base"
        Else
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")

            Else
                Session("mLegends")(5, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".db", "")
            End If
        End If
    End Sub


    Private Sub mnuReg_Click()
        Session("bUseReg") = True
        Session("bUseGroup") = False
        Session("bUseGroupDec") = False
        Session("bUseGList") = False

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        LoadDim1s()
    End Sub

    Private Sub mnuGroup_Click()
        Dim bSkip As Boolean
        bSkip = Session("bUseGroupDec")

        Session("bUseReg") = False
        Session("bUseGroup") = True
        Session("bUseGroupDec") = False
        Session("bUseGList") = False

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        If Not bSkip Then
            LoadDim1s()
        End If
    End Sub

    Private Sub mnuGroupDec_Click()
        Dim bSkip As Boolean
        bSkip = Session("bUseGroup")

        Session("bUseReg") = False
        Session("bUseGroup") = False
        Session("bUseGroupDec") = True
        Session("bUseGList") = False

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "blank.gif"

        If Not bSkip Then
            LoadDim1s()
        End If
    End Sub

    Private Sub mnuGList_Click()
        Session("bUseReg") = False
        Session("bUseGroup") = False
        Session("bUseGroupDec") = False
        Session("bUseGList") = True

        cswmWebMenu.Groups("mnuGroups").Items("mnuReg").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroup").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGroupDec").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuGroups").Items("mnuGList").IconImage = "checkbox.gif"

        LoadDim1s()
    End Sub

    Private Sub mnuAgeWgOn_Click()
        Session("bUseAgeWg") = True

        cswmWebMenu.Groups("mnuAgeWg").Items("mnuAgeWgOn").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuAgeWg").Items("mnuAgeWgOff").IconImage = "blank.gif"

        If GridView1.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdTable_Click(Me, New EventArgs)
        ElseIf lblJS.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdDraw_Click(Me, New EventArgs)
        End If

    End Sub

    Private Sub mnuAgeWgOff_Click()
        Session("bUseAgeWg") = False

        cswmWebMenu.Groups("mnuAgeWg").Items("mnuAgeWgOn").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuAgeWg").Items("mnuAgeWgOff").IconImage = "checkbox.gif"

        If GridView1.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdTable_Click(Me, New EventArgs)
        ElseIf lblJS.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdDraw_Click(Me, New EventArgs)
        End If
    End Sub

    Private Sub mnuAgeWBeta_Click()
        pnlMort.Visible = False
        pnlButtons.Visible = False
        GridView1.Visible = False
        lblJS.Visible = False
        cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = False
        cswmWebMenu.Visible = False

        txtAWBeta.Text = Session("AWBeta")
        lblMsg.Visible = False
        pnlAWBeta.Visible = True

    End Sub


    Private Sub mnuDiscRt_Click()
        pnlMort.Visible = False
        pnlButtons.Visible = False
        GridView1.Visible = False
        lblJS.Visible = False
        cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = False
        cswmWebMenu.Visible = False

        txtDiscRt.Text = Session("DiscountRt") * 100
        lblMsgDiscRt.Visible = False
        pnlDiscRt.Visible = True
    End Sub

    Protected Sub cmdSaveAWBeta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveAWBeta.Click
        Dim sVal As String, BetaVal As Single

        sVal = txtAWBeta.Text
        If IsNumeric(sVal) Then
            If Val(sVal) <> 0 Then
                BetaVal = Val(sVal)
            End If
        End If
        If sVal = "" Or BetaVal = 0 Then
            txtAWBeta.Text = Session("AWBeta")
            lblMsg.Text = "Sorry, but Beta can't be 0, try another value."
            lblMsg.Visible = True
            Exit Sub
        End If

        Session("AWBeta") = BetaVal
        Session("AWC") = Session("AWArea") / ((-100 / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-1 * Session("AWBeta") * 100) - (-0 / Session("AWBeta") - 1 / (Session("AWBeta") ^ 2)) * Math.Exp(-1 * Session("AWBeta") * 0))

        Dim sAWBeta As String
        sAWBeta = "Change Beta (" & Session("AWBeta") & ", " & Session("AWC") & ")"
        cswmWebMenu.Groups("mnuAgeWg").Items("mnuAgeWgBeta").Caption = sAWBeta

        pnlAWBeta.Visible = False
        pnlButtons.Visible = True
        cswmWebMenu.Visible = True

        If Session("TotalGraphs") = 0 Then
            lblJS.Visible = False
            GridView1.Visible = False
            cmdDraw.Enabled = True
            cmdTable.Enabled = True
            pnlMort.Visible = True
            cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = False
        Else
            cmdTable_Click(Me, New EventArgs)
        End If

    End Sub


    Protected Sub cmdCancelAWBeta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelAWBeta.Click
        pnlAWBeta.Visible = False
        If Session("TotalGraphs") = 0 Then
            pnlMort.Visible = True
        Else
            GridView1.Visible = True
        End If
        pnlButtons.Visible = True
        cswmWebMenu.Visible = True
    End Sub

    Protected Sub cmdSaveDiscRt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveDiscRt.Click
        Dim sVal As String, DiscRtVal As Single = -1

        sVal = txtDiscRt.Text
        If IsNumeric(sVal) Then
            If Val(sVal) >= 0 And Val(sVal) < 100 Then
                DiscRtVal = Val(sVal) / 100
            End If
        End If
        If sVal = "" Or DiscRtVal < 0 Then
            txtDiscRt.Text = Session("DiscountRt")
            lblMsgDiscRt.Text = "Sorry, but the discount rate needs to be higher or equal than 0 and lower than 100, try another value."
            lblMsgDiscRt.Visible = True
            Exit Sub
        End If

        Session("DiscountRt") = DiscRtVal

        Dim sDiscRt As String
        sDiscRt = "Discount Rate (" & CStr(Val(Session("DiscountRt")) * 100) & "%)"
        cswmWebMenu.Groups("mnuDiscRt").Items("mnuDiscRtChg").Caption = sDiscRt

        pnlDiscRt.Visible = False
        pnlButtons.Visible = True
        cswmWebMenu.Visible = True

        If Session("TotalGraphs") = 0 Then
            lblJS.Visible = False
            GridView1.Visible = False
            cmdDraw.Enabled = True
            cmdTable.Enabled = True
            pnlMort.Visible = True
            cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = False
        Else
            cmdTable_Click(Me, New EventArgs)
        End If

    End Sub

    Protected Sub cmdCancelDiscRt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelDiscRt.Click
        pnlDiscRt.Visible = False
        If Session("TotalGraphs") = 0 Then
            pnlMort.Visible = True
        Else
            GridView1.Visible = True
        End If
        pnlButtons.Visible = True
        cswmWebMenu.Visible = True
    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Session.Remove("DimLbl")

        'JAM 2019-12-26
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                vSQLiteCon.Close()
            End If
        End If

        If lblJS.Visible Or GridView1.Visible Then
            lblJS.Visible = False
            GridView1.Visible = False
            cmdDraw.Enabled = True
            cmdTable.Enabled = True
            pnlMort.Visible = True
            cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = False
            Session("TotalGraphs") = 0
        Else
            Session.Remove("mPLOT")
            Session.Remove("mLegends")
            If Session("DisplayParent") = Nothing Then
                Session("DisplayParent") = "frm_MainMenu.aspx"
            End If

            Support.FormManager.Show(Me, Session("DisplayParent"))
        End If
        'pnlButtons.Visible = True
    End Sub

    Private Sub mnuAlias_Click()
        Dim i As Integer
        If Not Session("bAlias") Then
            Session("bAlias") = True
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Alias for Group Names").IconImage = "checkbox.gif"
            End If

            'Read and load all aliases
            Dim Tbl As DataTable
            Dim s As String
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT GroupName, Alias FROM " & GroupIDNew, "GroupName", Tbl)
            For i = 1 To Session("TotalGraphs")
                s = SeekValue(Tbl, Session("mLegends")(2, i), "Alias")
                If s > "" Then
                    Session("mLegends")(2, i) = s
                End If
            Next i
        Else
            Session("bAlias") = False
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Alias for Group Names").IconImage = "blank.gif"
            End If

            For i = 1 To Session("TotalGraphs")
                Session("mLegends")(2, i) = Session("mLegends")(0, i)
            Next i
        End If
    End Sub

    Private Sub mnulblEdit_Click()
        pnlMort.Visible = False
        pnlButtons.Visible = False
        GridView1.Visible = False
        lblJS.Visible = False
        cswmWebMenu.Visible = False

        txtEditLbl.Text = ""
        Dim i As Integer
        lstEditLbl.Items.Clear()
        For i = 1 To Session("TotalGraphs")
            lstEditLbl.Items.Add(getSeriesName(i))
        Next
        pnlEditLbl.Visible = True
    End Sub

    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String
        Select Case Session("DimLbl")
            Case "1"
                Legend = Session("mLegends")(2, ii)
            Case "2"
                Legend = Session("mLegends")(3, ii)
            Case "3"
                Legend = Session("mLegends")(4, ii)
            Case "sce"
                Legend = Session("mLegends")(5, ii)
            Case "cust"
                Legend = Session("mLegends")(6, ii)
            Case Else
                Legend = Session("mLegends")(1, ii) & ", " & Session("mLegends")(2, ii) & ", " &
                    Session("mLegends")(3, ii) & ", " & Session("mLegends")(4, ii) & ", " & Session("mLegends")(5, ii)
        End Select
        getSeriesName = Legend
    End Function

    Private Function getColor(ByVal i As Integer) As String
        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = """Lime"""
                Case 2
                    sColor = """Blue"""
                Case 3
                    sColor = """Red"""
                Case 4 'blue violet
                    sColor = """RGB(138,43,226)"""
                Case 5 'aqua 
                    sColor = """RGB(0,255,255)"""
                Case 6 'deep pink
                    sColor = """RGB(255,20,147)"""
                Case 7 'medium spring green
                    sColor = """RGB(0,250,154)"""
                Case Else
                    sColor = """RGB(" & (RandD Mod 256).ToString & "," & (RandD Mod 256).ToString & "," & (RandD Mod 256).ToString & ")"""
            End Select
        End If
        getColor = sColor
    End Function

    Protected Sub lstEditLbl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstEditLbl.SelectedIndexChanged
        txtEditLbl.Text = lstEditLbl.Items(lstEditLbl.SelectedIndex).Text
    End Sub

    Protected Sub cmdUpdEditLbl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdEditLbl.Click
        lstEditLbl.Items(lstEditLbl.SelectedIndex).Text = txtEditLbl.Text
    End Sub

    Protected Sub cmdCancelEditLbl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelEditLbl.Click
        pnlEditLbl.Visible = False
        lblJS.Visible = True
        cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = True
        pnlButtons.Visible = True
        cswmWebMenu.Visible = True
    End Sub

    Protected Sub cmdSaveEditLbl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveEditLbl.Click
        Select Case Session("DimLbl")
            Case "1"
                Session("mLegends")(7, 1) = Session("mLegends")(1, 1) & ", " & Session("mLegends")(3, 1) & ", " & Session("mLegends")(4, 1) & ", " & Session("mLegends")(5, 1)
            Case "2"
                Session("mLegends")(7, 1) = Session("mLegends")(1, 1) & ", " & Session("mLegends")(2, 1) & ", " & Session("mLegends")(4, 1) & ", " & Session("mLegends")(5, 1)
            Case "3"
                Session("mLegends")(7, 1) = Session("mLegends")(1, 1) & ", " & Session("mLegends")(2, 1) & ", " & Session("mLegends")(3, 1) & ", " & Session("mLegends")(5, 1)
            Case "sce"
                Session("mLegends")(7, 1) = Session("mLegends")(1, 1) & ", " & Session("mLegends")(2, 1) & ", " & Session("mLegends")(3, 1) & ", " & Session("mLegends")(4, 1)
            Case "cus"
                Session("mLegends")(7, 1) = Session("mLegends")(7, 1)
            Case Else
                Session("mLegends")(7, 1) = ""
        End Select

        Session("DimLbl") = "cust"
        Dim i As Integer
        For i = 1 To Session("TotalGraphs")
            Session("mLegends")(6, i) = lstEditLbl.Items(i - 1).Text
        Next

        cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuLblOpt").Items("Use Second Dimension Only").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuLblOpt").Items("Use Third Dimension Only").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuLblOpt").Items("Edit Labels").IconImage = "checkbox.gif"

        pnlEditLbl.Visible = False
        pnlButtons.Visible = True
        cswmWebMenu.Visible = True

        lblJS.Visible = True
        cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = True
    End Sub
End Class
