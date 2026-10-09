Imports System.Data.SQLite

Imports IFs.Data
Imports System.Drawing


Partial Class frm_GrowthAccounting
    Inherits Page

#Region " Web Form Designer Generated Code "
    'JAM 2019-12-26
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vConnString As String
    'Dim TotalGraphs As Integer
    'jrs 2009/07/02
    Dim bSuccess As Boolean
    'Dim totYears As Integer

    'Dim bComparisonOn As Boolean
    Dim salphas(1, NSec) As Single
    'Dim Session("CompBackup")(30, 30) As Integer
    'Dim CompLabel(30, 30) As Integer

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        If Session("ChartType") Is Nothing Then
            Session("ChartType") = "line"
        End If
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

        cswmGroup1 = cswmWebMenu.Groups.Add() '==
        cswmGroup1.Caption = "Continue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = cswmWebMenu.Groups.Add() '=========
        cswmGroup1.Caption = "Save"
        cswmGroup1.ID = "mnuSaveHdr"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save to File"
        cswmItem.ID = "mnuSave"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save to Clipboard"
        cswmItem.ID = "mnuSaveClip"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Print"
        cswmGroup1.ID = "mnuPrint"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Format" '===============
        cswmGroup1.ID = "mnuDisplayFormat"
        If Session("bUseDisplaySUMMFP") Is Nothing Then
            Session("bUseDisplayAdj") = False
            Session("bUseDisplayAnnual") = True 'Default
            Session("bUseDisplaySUMMFP") = False

            'etc
        End If
        'ttt 2020/06/09
        If Session("bUseDisplayMFPDec") Is Nothing Then
            Session("bUseDisplayMFPDec") = True 'Default
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change to Bar Chart"
        cswmItem.ID = "mnuGraphBar"
        If Session("ChartType") = "bar" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change to Line Graph"
        cswmItem.ID = "mnuGraphLine"
        If Session("ChartType") = "line" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"
        cswmItem.ID = "mnuDispCtrl"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display/Run Horizon"
        cswmItem.ID = "mnuDisplayRunHorizon"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Annual Growth Rate (off would present full interval growth rate)"
        cswmItem.ID = "mnuAnnual"
        If Session("bUseDisplayAnnual") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Adjustment to Actual Growth"
        cswmItem.ID = "mnuAdjToGrowth"
        If Session("bUseDisplayAdj") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Decompose MFP"
        cswmItem.ID = "mnuMFPDec"
        If Session("bUseDisplayMFPDec") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Sum MFP Components for Validation"
        cswmItem.ID = "mnuSUMMFPComp4Val"

        If Session("bUseDisplaySUMMFP") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Grouping Options" '============
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
        cswmGroup1.Caption = "Label Options" '=================
        cswmGroup1.ID = "mnuLblOpt"
        If Session("DimLbl") Is Nothing Then
            Session("DimLbl") = ""
        End If
        If Session("bAlias") Is Nothing Then
            Session("bAlias") = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use First Dimension Only"
        cswmItem.ID = "mnulblDim1"
        If Session("DimLbl") = "1" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Sector Only"
        cswmItem.ID = "mnulblDim2"
        If Session("DimLbl") = "2" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Scenario Names Only"
        cswmItem.ID = "mnulblSce"
        If Session("DimLbl") = "sce" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Components Only"
        cswmItem.ID = "mnulblComp"
        If Session("DimLbl") = "comp" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Alias for Group Names"
        cswmItem.ID = "mnulblAlias"
        If Session("bAlias") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Reset Labels"
        cswmItem.ID = "mnulblReset"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Comparison Options" '=================
        cswmGroup1.ID = "mnuComp"
        If Session("bComparisonOn") Is Nothing Then
            Session("bComparisonOn") = False
            Session("bCompRatAB") = False
            Session("bCompRatBA") = False
            Session("bCompDiffAB") = False
            Session("bCompDiffBA") = False
            cswmGroup1.ShowMenuBarItem = False 'Invisible
        ElseIf Session("bCompRatAB") Is Nothing Then
            Session("bCompRatAB") = False
            Session("bCompRatBA") = False
            Session("bCompDiffAB") = False
            Session("bCompDiffBA") = False
        End If

        If Session("CompLabel") Is Nothing Then
            Dim mCompLabel(30, 30) As String
            Session("CompLabel") = mCompLabel
        End If
        If Session("CompBackup") Is Nothing Then
            Dim mCompBackup(30, 30) As Decimal
            Session("CompBackup") = mCompBackup
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Ratio Comparison (a/b)"
        cswmItem.ID = "mnuCompRatAB"
        If Session("bCompRatAB") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Ratio Comparison (b/a)"
        cswmItem.ID = "mnuCompRatBA"
        If Session("bCompRatBA") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Difference Comparison (a-b)"
        cswmItem.ID = "mnuCompDiffAB"
        If Session("bCompDiffAB") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Difference Comparison (b-a)"
        cswmItem.ID = "mnuCompDiffBA"
        If Session("bCompDiffBA") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Reset"
        cswmItem.ID = "mnuCompReset"


        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Go to Development Profile" '===
        cswmItem.ID = "mnuDevProfGo"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Go to Development Profile"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Explanatory Note" '===
        cswmItem.ID = "mnuExpNote"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explanatory Note"


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
        If Not Page.IsPostBack Then '-----------------------------------------------------------
            'Redimension array to fit all cohorts, and as many region,gender,type combinations as possible
            ReDim Session("mLegends")(4, MaxVariables)
            'Load Geografic List
            LoadDim1s()
            'Load Types
            Dim idx As Integer
            idx = IfsVars.Item("VADD").Index
            Dim DimNum As Integer
            DimNum = VDim2(idx)
            Dim i As Integer
            If lstDim2.Items.Count = 0 Then
                For i = 1 To MaxVal(DimNum) - 1
                    lstDim2.Items.Add(GRPS$(DimNum, i))
                Next i
                lstDim2.Items.Add("Total")
                lstDim2.Items(lstDim2.Items.Count - 1).Selected = True
            End If
            'ttt 2020-06-08 ----------------------
            'Debug.WriteLine("zz2Session(""bUseDisplayMFPDec"")=" & Session("bUseDisplayMFPDec").ToString)

            lstVars.Items.Clear()
            lstVars.Items.Add("Capital")
            lstVars.Items.Add("Labor")
            lstVars.Items.Add("MFP")
            lstVars.Items.Add("Capacity Use")
            lstVars.Items.Add("Climate Impact")
            lstVars.Items.Add("GDP Growth (for comparison)")
            For i = 0 To 5
                lstVars.Items(i).Selected = True
            Next
            'ttt 2020/06/09
            If Session("bUseDisplayMFPDec") Then
                lstVars.Items.Add("Technology Base MFP")
                lstVars.Items.Add("Knowledge MFP")
                lstVars.Items.Add("Human Capital MFP")
                lstVars.Items.Add("Social Capital MFP")
                lstVars.Items.Add("Physical Capital MFP")
                lstVars.Items.Add("Correction MFP")
                For i = 6 To 11
                    lstVars.Items(i).Selected = True
                Next
            End If
            '---------------------------------------

            'Load Variable Names
            'Load Files
            LoadFiles()
            'Reset first year if History plus forecast
            If Session("FirstYear") = FirstYearHistory And UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
            End If

            'Load Years
            'jrs 2020/06/06
            Session("iLastYear") = Session("LastYear")
            'Dim db As String
            'db = Session("PutFdrvRun") & "ifsPopWork.mdb"
            'Dim rs As DataTable = New DataTable

            ''JAM 2020-05-19
            'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then
            '    'ttt 2020-06-04 Add Try
            '    Try
            '        SetDataTablefromAccess(db, "SELECT MAX(Years) as MaxYrs FROM MorDetBaseCase", "", rs)
            '        Session("iLastYear") = rs.Rows(0).Item("MaxYrs")
            '    Catch ex As Exception
            '        Session("iLastYear") = 2100
            '    End Try


            'Else
            '    vSQLiteCommand = vSQLiteCon.CreateCommand()
            '    vSQLiteCommand.CommandText = "SELECT MAX(TimeId) as MaxYrs FROM Mor_Det_Base_Case"
            '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


            '    While (vSQLiteReader.Read())
            '        Session("iLastYear") = vSQLiteReader("MaxYrs")

            '    End While


            'End If

            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").Enabled = False
            'ttt 2020/06/09
            cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False

            ReDim Session("mPLOT")(Session("iLastYear") - Session("FirstYear") + 1, MaxVariables)

            LoadGlobalVariables()
            '------

            Session("DimLbl") = ""
            Session("bAlias") = False
            Session("bComparisonOn") = False
            Session("bCompRatAB") = False
            Session("bCompRatBA") = False
            Session("bCompDiffAB") = False
            Session("bCompDiffBA") = False
            '-----
            Dim sVar As String, VarIdx As Integer, X As Integer
            ReDim salphas(1, NSec)
            sVar = "salpha"
            VarIdx = IfsVars.Item(sVar).Index
            Dim MyFileforDisplay As IStorage
            Dim MyDF As IDataframe
            For X = 1 To NSec
                '            gLoadForDisplayNET(sVar, VarIdx, "", X, "", -1, "", -1, "", 1,
                '                               True, True, False, 1, salphas, X, 1,
                'FirstYear, FirstYear, 0, False, sTitle, False)
                ' Session("PutFdrvRun") & sBinFile, BinFileIdx, ??
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(sVar, VarIdx, "", X, "", -1, "", -1, "", 1,
                           False, False, False, 1, salphas, X, 1,
           Session("FirstYear"), Session("FirstYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
           Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay1"), Session("df" + sVar + "1"))
                Else
                    gLoadForDisplay(sVar, VarIdx, "", X, "", -1, "", -1, "", 1,
                           True, False, False, 1, salphas, X, 1,
           Session("FirstYear"), Session("FirstYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileForDisplay1"), Session("FinYear"),
           Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If
            Next X
            'jrs 2025/07/28
            Session("GAsalphas") = salphas
            'ttt 2020/06/08 - remove
            'mnuMFPDec_Click(sender, e)
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
        Else 'jrs 2025/07/28 post back
            salphas = Session("GAsalphas")
        End If

        lstFiles.Attributes.Add("onclick", "lstFiles_Click();")
        lstFiles.Attributes.Add("onmousedown", "lstFiles_ClickDown();")
        lstFiles.Attributes.Add("onmouseup", "lstFiles_ClickUp();")

        'If lblJS.Visible Then
        '    PackJScritpBuilder()
        'End If



    End Sub

    Private Sub LoadFiles()
        'jrs 2005/02/21 changed First Year to First Year Forecast, if History plus Forecast
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            Session("VarSelCheck") = Nothing
        End If
        If Session("FirstYear") = FirstYearHistory AndAlso Session("LastYear") = Session("LastYearForecast") Then
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
                Session("VarSelCheckHis") = Nothing 'jrs 2005/10/12
            Else
                Session("LastYear") = LastYearHistory
            End If
        End If

        cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").Enabled = False

        'jrs 2009/05/12
        Session("ChartType") = "line"
        Session("Interval") = 1
        'jrs 2024/10/09 Session("DisplayLastYear") = 2100
        'jrs 2025/03/17 Session("DisplayLastYear") = AMAX(2100, Session("LastYearForecast"))
        Session("DisplayLastYear") = Amin(2100, Session("LastYearForecast"))
        Session("TotYears") = CInt((Session("DisplayLastYear") - Session("FirstYear")) / Session("Interval"))

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim fs 'As New FileSystemObject
        Dim BASE 'As File
        Dim currRun 'As File
        fs = CreateObject("Scripting.FileSystemObject")
        Dim sWorkingFile As String
        sWorkingFile = Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))

        'JRS 05/20/03
        If Session("FirstYear") = FirstYearHistory AndAlso Session("LastYear") = LastYearHistory Then
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBaseHist.run.db") 'jrs 2021/02/10
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBaseHist.run")
            End If
        Else
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run.db") 'jrs 2021/02/10
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
        sRunFile = Dir(Session("PutFdrvRun2") + "\*.run.db") 'jrs 2021/02/10

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
            If Not Session("bComparisonOn") Then
                Dim mCompBackup(UBound(Session("mPLOT"), 1), Session("TotalGraphs")) As Decimal
                Session("CompBackup") = mCompBackup
                Dim mCompLabel(4, Session("TotalGraphs")) As String
                Session("CompLabel") = mCompLabel
                cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True

                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
                Session("bCompRatAB") = False
                Session("bCompRatBA") = False
                Session("bCompDiffAB") = False
                Session("bCompDiffBA") = False
            End If
        End If

        cmdDraw.Enabled = True
        cmdTable.Enabled = False
    End Sub


    Private Sub LoadGrid()
        Dim j As Integer, i As Integer, r(4) As TableRow, ce As TableCell
        cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").Enabled = True
        Dim bSUM As Boolean = False
        bSUM = (cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").IconImage = "checkbox.gif")
        If bSUM Then
            mnuSUMMFPComp4Val_Click(grdGrowth, New EventArgs)
        End If

        'Load Legends (Column Header)
        For j = 1 To Session("TotalGraphs")
            For i = 1 To 4
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

        '   Fill in top of table
        '   --------------------
        grdHL.Rows.Clear()
        For i = 1 To 4
            grdHL.Rows.Add(r(i - 1))
        Next

        'Load Years
        Session("TotYears") = CInt((Session("DisplayLastYear") - Session("FirstYear")) / Session("Interval"))

        ReDim Preserve r(Session("TotYears") + 4)

        'Row Header Texts
        j = 4
        For i = Session("Interval") To Session("FinYear")(1) - 1 Step Session("Interval")
            If j > Session("TotYears") + 4 Then Exit For
            r(j) = New TableRow
            ce = New TableCell
            Dim YearAdjustment As Integer
            YearAdjustment = 0
            If Session("Interval") = 5 Then
                If (Session("FirstYear") + i) Mod 5 <> 0 Then
                    While (Session("FirstYear") + i + YearAdjustment) Mod 5 <> 0
                        YearAdjustment -= 1
                    End While
                End If
            End If
            ce.Text = CStr(Session("FirstYear") + i + YearAdjustment)
            ce.BackColor = System.Drawing.Color.Gray
            r(j).Cells.Add(ce)
            j += 1
        Next i
        'Add Years
        'Add Rows With First Col= Years
        For i = 1 + 4 To Session("TotYears") + 4
            grdHL.Rows.Add(r(i - 1))
        Next

        'End Row Header Texts
        'Load Growth Values
        'Add more Col of Value to rows above
        For j = 1 To Session("TotalGraphs")
            For i = 1 To Session("TotYears")
                'If j = 1 Then
                '    r(i + 3) = New TableRow
                'End If
                ce = New TableCell
                'jrs 2020/06/06 ce.Text = Format(NoNullDec(Session("mPLOT")(i, j)), "###,##0.00")
                ce.Text = Format((Session("mPLOT")(i, j)), "###,##0.00")
                ce.HorizontalAlign = HorizontalAlign.Right
                r(i + 3).Cells.Add(ce)
            Next i
        Next j

        Dim dt As New DataTable()
        Dim dcol As New DataColumn()

        For j = 0 To Session("TotalGraphs")  'One for header, then for data
            dcol = New DataColumn(Str(j))
            dt.Columns.Add(dcol)
        Next j

        For i = 4 To Session("TotYears") + 3
            Dim dr As DataRow = dt.NewRow
            For j = 0 To Session("TotalGraphs")
                If j = 0 Then
                    dr(Str(j)) = CStr(Session("FirstYear") + (i - 3) * Session("Interval"))
                Else
                    dr(Str(j)) = grdHL.Rows(i).Cells(j).Text 'Not j-1
                End If
            Next
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next i

        grdGrowth.DataSource = dt
        Session("DataForGrid") = dt
        grdGrowth.DataBind()


        For j = 0 To Session("TotalGraphs")
            If j = 0 Then
                'JAM 2012-05-13	Adding blank space because cell borders had a different look
                grdGrowth.HeaderRow.Cells(j).Text = Server.HtmlDecode("&nbsp;</br>") '""
                ' grdGrowth.Columns(j).HeaderText = "Year"
            Else
                grdGrowth.HeaderRow.Cells(j).Text = Server.HtmlDecode(grdHL.Rows(0).Cells(j).Text & "</br>" & grdHL.Rows(1).Cells(j).Text & "</br>" & grdHL.Rows(2).Cells(j).Text & "</br>" & grdHL.Rows(3).Cells(j).Text) ' & "</br>" & grdHL.Rows(4).Cells(j).Text)
            End If
        Next j

        For i = 4 To (Session("TotYears") + 3)
            For j = 0 To 0 'ttt Only One
                grdGrowth.Rows(i - 4).Cells(j).BackColor = Color.LightGray
                'Bold for Row header
                grdGrowth.Rows(i - 4).Cells(j).Text = Server.HtmlDecode("<span style='color: #000000;'><b>" & grdHL.Rows(i).Cells(j).Text & "</b></span>")
            Next j
        Next i

        If Session("TotalGraphs") <= 3 Then
            If Session("TotalGraphs") = 3 Then
                grdGrowth.Width = Unit.Percentage(35)
            Else
                grdGrowth.Width = Unit.Percentage(25)
            End If
        End If
        grdGrowth.Visible = True
        cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = True

        'ClientScript.RegisterStartupScript(Me.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'grdGrowth', 'HeaderDiv');</script>")
    End Sub

    'jrs 2005/10/13
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

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

            Case "Save to File"
                mnuSave_Click()

            Case "Save to Clipboard"
                mnuSaveClip_Click()
            Case "Change to Bar Chart"
                mnuBar_Click()
            Case "Change to Line Graph"
                mnuLine_Click()
            Case "Display/Run Horizon"
                mnuDisplayRunHorizon_Click(sender, e)
            Case "Annual Growth Rate (off would present full interval growth rate)"
                mnuAnnual_Click(sender, e)
            Case "Adjustment to Actual Growth"
                mnuAdjToGrowth_Click(sender, e)
            Case "Decompose MFP"
                mnuMFPDec_Click(sender, e)
            Case "Sum MFP Components for Validation"
                mnuSUMMFPComp4Val_Click(sender, e)
            Case "Set Title, Display Interval, or Year" 'jrs 2005/10/19
                'jrs 2012/07/23 
                Session("DisplayControlParent") = Request.Url.AbsoluteUri
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DisplayControl.aspx")

            Case "Go to Development Profile" '===
                mnuDevProfGo_Click()

            Case "Explanatory Note"
                mnuExpNote_Click()

            Case "Use First Dimension Only"
                Session("DimLbl") = "1"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Sector Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Components Only").IconImage = "blank.gif"

            Case "Use Sector Only"
                Session("DimLbl") = "2"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Sector Only").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Components Only").IconImage = "blank.gif"

            Case "Use Scenario Names Only"
                Session("DimLbl") = "sce"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Sector Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "checkbox.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Components Only").IconImage = "blank.gif"

            Case "Use Components Only"
                Session("DimLbl") = "comp"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Sector Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Components Only").IconImage = "checkbox.gif"

            Case "Reset Labels"
                Session.Remove("DimLbl")
                cswmWebMenu.Groups("mnuLblOpt").Items("Use First Dimension Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Sector Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Scenario Names Only").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuLblOpt").Items("Use Components Only").IconImage = "blank.gif"

            Case "Use Alias for Group Names"
                mnuAlias_Click()
            Case "Ratio Comparison (a/b)"
                mnuCompRatAB_Click()
            Case "Ratio Comparison (b/a)"
                mnuCompRatBA_Click()
            Case "Difference Comparison (a-b)"
                mnuCompDiffAB_Click()
            Case "Difference Comparison (b-a)"
                mnuCompDiffBA_Click()
            Case "Reset"
                mnuCompReset_Click()

        End Select

        'If lblJS.Visible Then
        '    PackJScritpBuilder()
        'End If
    End Sub

    Private Sub mnuLine_Click()
        cswmWebMenu.Groups("mnuDisplayFormat").Items("Change to Line Graph").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuDisplayFormat").Items("Change to Bar Chart").IconImage = "blank.gif"
        Session("ChartType") = "line"
        Session("Interval") = 1
        'jrs 2024/10/09 Session("DisplayLastYear") = 2100
        Session("TotYears") = CInt((Session("DisplayLastYear") - Session("FirstYear")) / Session("Interval"))
        If Not grdGrowth.Visible Then
            LoadSet()
        Else
            grdGrowth.Visible = False
            cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False

        End If
        If Session("TotalGraphs") > 0 Then
            DrawGraph()
            pnlMort.Visible = False
            cmdDraw.Enabled = False

            If Not Session("bComparisonOn") Then
                Dim mCompBackup(UBound(Session("mPLOT"), 1), Session("TotalGraphs")) As Decimal
                Session("CompBackup") = mCompBackup
                Dim mCompLabel(4, Session("TotalGraphs")) As String
                Session("CompLabel") = mCompLabel
                cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
                Session("bCompRatAB") = False
                Session("bCompRatBA") = False
                Session("bCompDiffAB") = False
                Session("bCompDiffBA") = False
            End If
        End If
        cmdTable.Enabled = True
    End Sub

    Private Sub mnuBar_Click()
        cswmWebMenu.Groups("mnuDisplayFormat").Items("Change to Bar Chart").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuDisplayFormat").Items("Change to Line Graph").IconImage = "blank.gif"
        Session("ChartType") = "bar"
        Session("Interval") = 5
        'jrs 2024/10/09 Session("DisplayLastYear") = 2050
        Session("TotYears") = CInt((Session("DisplayLastYear") - Session("FirstYear")) / Session("Interval"))

        If Not grdGrowth.Visible Then
            LoadSet()
        Else
            grdGrowth.Visible = False
            cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False

        End If
        If Session("TotalGraphs") > 0 Then
            DrawGraph()
            pnlMort.Visible = False
            cmdDraw.Enabled = False

            If Not Session("bComparisonOn") Then
                Dim mCompBackup(UBound(Session("mPLOT"), 1), Session("TotalGraphs")) As Decimal
                Session("CompBackup") = mCompBackup
                Dim mCompLabel(4, Session("TotalGraphs")) As String
                Session("CompLabel") = mCompLabel
                cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
                Session("bCompRatAB") = False
                Session("bCompRatBA") = False
                Session("bCompDiffAB") = False
                Session("bCompDiffBA") = False
            End If
        End If
        cmdTable.Enabled = True
    End Sub

    Private Sub mnuCompReset_Click()
        If Session("bComparisonOn") Then
            Session("bComparisonOn") = False
            ResetForComparison()
            cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
            Session("bCompRatAB") = False
            Session("bCompRatBA") = False
            Session("bCompDiffAB") = False
            Session("bCompDiffBA") = False
            If grdGrowth.Visible Then
                LoadGrid()
            Else
                DrawGraph()
            End If
        End If

    End Sub

    Private Sub mnuCompDiffBA_Click()
        If Session("bUseDisplaySUMMFP") Then
            mnuSUMMFPComp4Val_Click(Me, New EventArgs)
        End If

        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                Session("mPLOT")(i, j) = Session("CompBackup")(i, 2 * j) - Session("CompBackup")(i, 2 * j - 1)
            Next i

            LabelComparison(j, "-", False)

        Next j
        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "checkbox.gif"
        Session("bCompRatAB") = False
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = True
        If grdGrowth.Visible Then
            LoadGrid()
        Else
            DrawGraph()
        End If

    End Sub

    Private Sub mnuCompDiffAB_Click()
        If Session("bUseDisplaySUMMFP") Then
            mnuSUMMFPComp4Val_Click(Me, New EventArgs)
        End If

        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If
        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                Session("mPLOT")(i, j) = Session("CompBackup")(i, 2 * j - 1) - Session("CompBackup")(i, 2 * j)
            Next i

            LabelComparison(j, "-", True)
        Next j
        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        Session("bCompRatAB") = False
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = True
        Session("bCompDiffBA") = False

        If grdGrowth.Visible Then
            LoadGrid()
        Else
            DrawGraph()
        End If

    End Sub


    Private Sub mnuCompRatBA_Click()
        If Session("bUseDisplaySUMMFP") Then
            mnuSUMMFPComp4Val_Click(Me, New EventArgs)
        End If

        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                If Session("CompBackup")(i, 2 * j - 1) <> 0 Then
                    Session("mPLOT")(i, j) = Session("CompBackup")(i, 2 * j) / Session("CompBackup")(i, 2 * j - 1)
                Else
                    Session("mPLOT")(i, j) = Nothing
                End If
            Next i

            LabelComparison(j, "/", False)
        Next j

        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        Session("bCompRatAB") = False
        Session("bCompRatBA") = True
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = False
        If grdGrowth.Visible Then
            LoadGrid()
        Else
            DrawGraph()
        End If

    End Sub


    Private Sub mnuCompRatAB_Click()
        If Session("bUseDisplaySUMMFP") Then
            mnuSUMMFPComp4Val_Click(Me, New EventArgs)
        End If

        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                If Session("CompBackup")(i, 2 * j) <> 0 Then
                    Session("mPLOT")(i, j) = Session("CompBackup")(i, 2 * j - 1) / Session("CompBackup")(i, 2 * j)
                Else
                    Session("mPLOT")(i, j) = Nothing
                End If
            Next i

            LabelComparison(j, "/", True)
        Next j
        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        Session("bCompRatAB") = True
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = False

        If grdGrowth.Visible Then
            LoadGrid()
        Else
            DrawGraph()
        End If

    End Sub
    Private Sub LabelComparison(j As Integer, sOp As String, bOrder As Boolean)
        'j is the column to load
        'sOp is the operation to load - or /
        'bOrder is true if it's a sOp b, false if it's b sOp a
        Dim pos As Integer
        pos = -1
        Dim j1 As Integer, j2 As Integer
        If bOrder Then
            j1 = 2 * j - 1
            j2 = 2 * j
        Else
            j1 = 2 * j
            j2 = 2 * j - 1
        End If

        If Session("CompLabel")(1, j1) = Session("CompLabel")(1, j2) Then
            Session("mLegends")(1, j) = Session("CompLabel")(1, j2)
        Else
            pos = InStr(Session("CompLabel")(1, j1), "[")
            If pos = -1 Then
                pos = Len(Session("CompLabel")(1, j1))
            End If
            If Mid(Session("CompLabel")(1, j1), 1, pos) = Mid(Session("CompLabel")(1, j2), 1, pos) Then
                Session("mLegends")(1, j) = Replace(Session("CompLabel")(1, j1), "]", sOp & Mid(Session("CompLabel")(1, j2), pos + 1))
            Else
                Session("mLegends")(1, j) = Session("CompLabel")(1, j1) & sOp & Session("CompLabel")(1, j2)
            End If
        End If
        If Session("CompLabel")(2, j1) = Session("CompLabel")(2, j2) Then
            Session("mLegends")(2, j) = Session("CompLabel")(2, j2)
        Else
            If pos > -1 Then
                Session("mLegends")(2, j) = Mid(Session("CompLabel")(2, j2), 1, pos - 1)
            Else
                Session("mLegends")(2, j) = Session("CompLabel")(2, j1) & sOp & Session("CompLabel")(2, j2)
            End If
        End If
        If Session("CompLabel")(3, j1) = Session("CompLabel")(3, j2) Then
            Session("mLegends")(3, j) = Session("CompLabel")(3, j2)
        Else
            Session("mLegends")(3, j) = Session("CompLabel")(3, j1) & sOp & Session("CompLabel")(3, j2)
        End If
        If Session("CompLabel")(4, j1) = Session("CompLabel")(4, j2) Then
            Session("mLegends")(4, j) = Session("CompLabel")(4, j2)
        Else
            Session("mLegends")(4, j) = Session("CompLabel")(4, j1) & sOp & Session("CompLabel")(4, j2)
        End If
    End Sub

    Private Sub ResetForComparison()
        Session("TotalGraphs") = Session("TotalGraphs") * 2
        Dim i As Integer, j As Integer
        For j = 1 To UBound(Session("CompBackup"), 2) 'jrs 2009/02/24 COUNTE
            For i = 0 To UBound(Session("mPLOT"), 1)
                Session("mPLOT")(i, j) = Session("CompBackup")(i, j)
            Next i
            Session("mLegends")(1, j) = Session("CompLabel")(1, j)
            Session("mLegends")(2, j) = Session("CompLabel")(2, j)
            Session("mLegends")(3, j) = Session("CompLabel")(3, j)
            Session("mLegends")(4, j) = Session("CompLabel")(4, j)
        Next j
    End Sub
    Private Sub SaveForComparison()
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                'jrs 2020/06/06 Session("CompBackup")(i, j) = NoNullDec(Session("mPLOT")(i, j))
                Session("CompBackup")(i, j) = (Session("mPLOT")(i, j))
            Next i
            Session("CompLabel")(1, j) = Session("mLegends")(1, j)
            Session("CompLabel")(2, j) = Session("mLegends")(2, j)
            Session("CompLabel")(3, j) = Session("mLegends")(3, j)
            Session("CompLabel")(4, j) = Session("mLegends")(4, j)
        Next j
        Session("TotalGraphs") = Session("TotalGraphs") / 2
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
        If (Session("bUseGroup") OrElse Session("bUseGroupDec")) Then
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
        If Not grdGrowth.Visible Then
            LoadSet()
        Else
            grdGrowth.Visible = False
            cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False

        End If
        If Session("TotalGraphs") > 0 Then
            If Session("ChartType") = "line" Then
                mnuLine_Click()
            Else
                mnuBar_Click()
            End If

            pnlMort.Visible = False
            cmdDraw.Enabled = False

            If Not Session("bComparisonOn") Then
                Dim mCompBackup(UBound(Session("mPLOT"), 1), Session("TotalGraphs")) As Decimal
                Session("CompBackup") = mCompBackup
                Dim mCompLabel(4, Session("TotalGraphs")) As String
                Session("CompLabel") = mCompLabel
                cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
                Session("bCompRatAB") = False
                Session("bCompRatBA") = False
                Session("bCompDiffAB") = False
                Session("bCompDiffBA") = False
            End If
        End If
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
        Dim rsG As DataTable = New DataTable
        Dim sDim1 As String
        Dim iDim1 As Integer
        Dim rr As Integer
        'jrs 2009/09/15
        Dim v As Integer

        'jrs 2012/07/30 dbG = Session("IFs_MDB2Active")
        dbG = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))

        Session("TotalGraphs") = 0
        'sFileName = ""
        'jrs 2014/07/29
        Dim bCapital As Boolean, bLabor As Boolean, bMFP As Boolean

        If Session("bUseDisplayAdj") Then
            bCapital = lstVars.Items(0).Selected
            bLabor = lstVars.Items(1).Selected
            bMFP = lstVars.Items(2).Selected

            If Not bCapital Then
                lstVars.Items(0).Selected = True
            End If
            If Not bLabor Then
                lstVars.Items(1).Selected = True
            End If
            If Not bMFP Then
                lstVars.Items(2).Selected = True
            End If
        End If
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
                    If Session("bUseGList") OrElse Session("bUseGroupDec") Then
                        sDim1 = rsG.Rows(rr).Item("Region")
                        iDim1 = IFsRegs.Item(sDim1).Index
                        If Session("bUseGList") Then
                            bGrp = rsG.Rows(rr).Item("Group")
                        End If
                    End If
                    'loop through variables
                    For v = 0 To lstVars.Items.Count - 1
                        If lstVars.Items(v).Selected Then
                            'loop through sectors
                            For T = 0 To lstDim2.Items.Count - 1
                                If lstDim2.Items(T).Selected Then
                                    'loop through files
                                    For f = 0 To lstFiles.Items.Count - 1
                                        If lstFiles.Items(f).Selected Then
                                            If Session("TotalGraphs") < MaxVariables Then
                                                Session("TotalGraphs") = Session("TotalGraphs") + 1
                                                LoadGrowthContribution(Session("TotalGraphs"), f, sDim1, iDim1, bGrp,
                                        lstDim2.Items(T).Text, lstVars.Items(v).Text)
                                                If Not Session("bLoadForDisplaySuccess") Then
                                                    Session("TotalGraphs") = Session("TotalGraphs") - 1
                                                End If
                                            End If
                                        End If 'if file selected
                                    Next f
                                End If 'if sector selected
                            Next T
                        End If 'if variable is selected
                    Next v
                    If Session("bUseGList") OrElse Session("bUseGroupDec") Then
                        rr = rr + 1
                        If rsG.Rows.Count = rr Then Exit Do

                    Else
                        Exit Do
                    End If
                Loop
                If Session("bUseGList") OrElse Session("bUseGroupDec") Then
                    'rsG.Close
                End If
            End If 'if region selected
        Next r


        dbG.Close()

        'jrs 2014/07/29
        If Session("bUseDisplayAdj") Then
            AdjustToActualGrowth(bCapital, bLabor, bMFP)
        End If

        'jrs 2008/11/11
        cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False
        cswmWebMenu.Groups("mnuPrint").ShowMenuBarItem = False
    End Sub

    Private Sub DrawGraph()
        'mnuSUMMFPComp4Val.Visible = False -> At PackJScritpBuilder
        lblJS.Visible = True
        cswmWebMenu.Groups("mnuLblOpt").ShowMenuBarItem = True
        PackJScritpBuilder()
    End Sub

    Private Sub PackJScritpBuilder()
        cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").Enabled = False
        Dim graphtitle As String = ""
        Dim subtitle As String = ""
        graphtitle = "Growth Accounting"
        'Loading Legends
        If Session("TotalGraphs") = 1 Then
            subtitle = Session("mLegends")(1, 1) & ", " & Session("mLegends")(2, 1) & ", " & Session("mLegends")(3, 1) & ", " & Session("mLegends")(4, 1)
        End If
        Dim liYears As New List(Of String)
        For i = 0 To Session("TotYears") - 1
            liYears.Add(CStr(Session("FirstYear") + (i + 1) * Session("Interval")))
        Next i

        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/11 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Session("ChartType") = "bar" Then
            sJscript2 = sJscript2 & "chart: {type: 'column'," & vbCr
        ElseIf Session("ChartType") = "line" Then
            sJscript2 = sJscript2 & "chart: {type: 'line'," & vbCr
        End If
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
        sJscript2 = sJscript2 & "fontSize:   '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight:  'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If subtitle > "" Then
            sJscript2 = sJscript2 & "subtitle: {" & vbCr
            sJscript2 = sJscript2 & "text:       """ & subtitle & """," & vbCr
            sJscript2 = sJscript2 & "x: -20, //center" & vbCr
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "fontSize:   '14px'," & vbCr
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
        sAxisYLabel = "Growth Rate"

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'Load Years
        '----------
        'cdv 2021/05/27 Fix Interval to be adjusted for when first year is not Mod 5 ****START****
        sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

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
            sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
            sJscript2 = sJscript2 & "align:      'center'," & vbCr
            sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
            sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        Else
            sJscript2 = sJscript2 & "legend: {" & vbCr
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

        Dim YearAdjustment As Integer
        Dim YearAdjustmentUp As Integer
        Dim YearAdjustmentDown As Integer

        YearAdjustment = 0
        YearAdjustmentUp = 0
        YearAdjustmentDown = 0

        For j = 1 To Session("TotYears")
            If Session("ChartType") = "bar" Then
                While (Session("FirstYear") + (j * Session("Interval")) + YearAdjustmentUp) Mod 5 <> 0
                    YearAdjustmentUp += 1
                End While
                While (Session("FirstYear") + (j * Session("Interval")) + YearAdjustmentDown) Mod 5 <> 0
                    YearAdjustmentDown -= 1
                End While

                If YearAdjustmentUp < Math.Abs(YearAdjustmentDown) Then
                    YearAdjustment = YearAdjustmentUp
                Else
                    YearAdjustment = YearAdjustmentDown
                End If
            End If
            Dim yr As String
            yr = CStr((Session("FirstYear") + (j * Session("Interval")) + YearAdjustment))

            If Not IsDBNull(Session("mPLOT")(j, i)) Then
                If bFirst Then
                    bFirst = False
                Else
                    sJscript = sJscript & "," & vbCr
                End If
                'sJscript = sJscript & CStr(Math.Round(NoNullDec(Vals(j, i)), 2))
                '[2020,x] 
                'jrs 2020/06/06 sJscript = sJscript & "[" & yr & "," & CStr(Math.Round(NoNullDec(Vals(j, i)), 2) & "]")
                sJscript = sJscript & "[" & yr & "," & CStr(Math.Round((Vals(j, i)), 2) & "]")
            End If
        Next j

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function


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
    Protected Sub cmdCloseHorizon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseHorizon.Click
        frm_Horizon.Visible = False
        cmb_Horizon.Visible = False
    End Sub
    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Session.Remove("DimLbl")

        'JAM 2019-12-26
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") = "True" Then
                vSQLiteCon.Close()
            End If
        End If
        '-----
        cswmWebMenu.Groups("mnuDisplayFormat").Items("Sum MFP Components for Validation").Enabled = False
        cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False
        cswmWebMenu.Groups("mnuPrint").ShowMenuBarItem = False
        '-------------
        Session("bComparisonOn") = False
        cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = False

        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"

        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").Enabled = False
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").Enabled = False
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").Enabled = False
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").Enabled = False

        Session("bCompRatAB") = False
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = False

        If lblJS.Visible OrElse grdGrowth.Visible Then
            lblJS.Visible = False
            grdGrowth.Visible = False
            cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False
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
        If Not If(Session("bAlias"), False) Then
            Session("bAlias") = True
            cswmWebMenu.Groups("mnuLblOpt").Items("Use Alias for Group Names").IconImage = "checkbox.gif"
            'Read and load all aliases
            Dim Tbl As DataTable
            Dim s As String
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT GroupName, Alias FROM " & GroupIDNew, "GroupName", Tbl)
            For i = 1 To Session("TotalGraphs")
                s = SeekValue(Tbl, Session("mLegends")(1, i), "Alias")
                If s > "" Then
                    Session("mLegends")(1, i) = s
                End If
            Next i
        Else
            Session("bAlias") = False
            cswmWebMenu.Groups("mnuLblOpt").Items("Use Alias for Group Names").IconImage = "blank.gif"
            For i = 1 To Session("TotalGraphs")
                Session("mLegends")(1, i) = Session("mLegends")(0, i)
            Next i
        End If
    End Sub
    Private Sub mnuSave_Click()
        'ttt 2020-06-08
        If (Not grdGrowth.Visible) OrElse (Session("DataForGrid") Is Nothing) OrElse (DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0) Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)
        Response.Clear()
        Response.ContentType = "application/octet-stream"

        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        '   Write Table to .CSV File
        '   ------------------------
        'Headers
        For i = 0 To 3
            Dim aVal = ""
            For j = 0 To dt.Columns.Count - 1
                Dim sVal = grdGrowth.HeaderRow.Cells(j).Text
                If j = 0 Then
                    aVal = "".PadLeft(15) & ","
                Else
                    sVal = sVal.Replace("</br>", "|")
                    aVal &= sVal.Split("|")(i)
                    aVal &= If(j = dt.Columns.Count - 1, "", ",")
                End If
            Next
            Context.Response.Write(aVal)
            Context.Response.Write(Environment.NewLine)
        Next
        'Data
        For r = 0 To dt.Rows.Count - 1
            For c = 0 To dt.Columns.Count - 1
                Dim sVal = If(dt.Rows(r).Item(c) Is Nothing, "".PadLeft(15), CDec(dt.Rows(r).Item(c)).ToString.PadLeft(15))
                sVal &= If(c = dt.Columns.Count - 1, "", ",")
                Context.Response.Write(sVal)
            Next
            Context.Response.Write(Environment.NewLine)
        Next

        Response.End()
    End Sub

    Private Sub mnuSaveClip_Click()
        'ttt 2020-06-09
        If Session("DataForGrid") Is Nothing OrElse DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0 Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)
        '   Write Table to Clipboard
        '   ------------------------
        Dim sb As StringBuilder = New StringBuilder()
        'Headers
        For i = 0 To 3
            Dim aVal = ""
            For j = 0 To dt.Columns.Count - 1
                Dim sVal = grdGrowth.HeaderRow.Cells(j).Text
                If j = 0 Then
                    aVal = "".PadLeft(15) & "\t"
                Else
                    sVal = sVal.Replace("</br>", "|")
                    aVal &= sVal.Split("|")(i)
                    aVal &= If(j = dt.Columns.Count - 1, "", "\t")
                End If
            Next
            sb.Append(aVal)
            sb.Append("\r\n")
        Next
        'Data
        For r = 0 To dt.Rows.Count - 1
            For c = 0 To dt.Columns.Count - 1
                Dim sVal = If(dt.Rows(r).Item(c) Is Nothing, "".PadLeft(15), CDec(dt.Rows(r).Item(c)).ToString.PadLeft(15))
                sVal &= If(c = dt.Columns.Count - 1, "", "\t")
                sb.Append(sVal)
            Next
            sb.Append("\r\n")
        Next

        Dim script As String = "<script type=text/javascript>gcopy('" & sb.ToString & "');</script>"
        Dim csname1 As String = "PopupScript"
        Dim cstype As Type = Me.[GetType]()
        Dim cs As ClientScriptManager = Page.ClientScript
        If Not cs.IsStartupScriptRegistered(cstype, csname1) Then
            cs.RegisterStartupScript(cstype, csname1, script)
        End If

    End Sub

    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String
        Select Case Session("DimLbl")
            Case "1"
                'USA
                Legend = Session("mLegends")(1, ii)
            Case "2"
                'Sector
                Legend = Session("mLegends")(2, ii)
            Case "sce"
                'Working
                Legend = Session("mLegends")(3, ii)
            Case "comp"
                'Capital
                Legend = Session("mLegends")(4, ii)
            Case Else
                Legend = Session("mLegends")(1, ii) & ", " & Session("mLegends")(2, ii) & ", " &
                    Session("mLegends")(3, ii) & ", " & Session("mLegends")(4, ii)
        End Select
        getSeriesName = Legend
    End Function

    Private Function getColor(ByVal i As Integer) As String
        Dim sColor As String = ""
        'jrs 2020/06/07 If i < 9 Then
        Select Case i
            Case 1
                sColor = """Lime"""
            Case 2
                sColor = """Blue"""
            Case 3
                sColor = """Red"""
            Case 4
                sColor = """Yellow"""
            Case 5 'blue violet
                sColor = """RGB(138,43,226)"""
            Case 6 'aqua 
                sColor = """RGB(0,255,255)"""
            Case 7 'deep pink
                sColor = """RGB(255,20,147)"""
            Case 8 'medium spring green
                sColor = """RGB(0,250,154)"""
            Case Else
                'jrs 2020/06/07
                Dim rd As New Random(i)
                Dim r1, r2, r3 As Short
                r1 = CShort(rd.NextDouble * 1000)
                r2 = CShort(rd.NextDouble * 1000)
                r3 = CShort(rd.NextDouble * 1000)
                sColor = """RGB(" & (r1 Mod 256).ToString & "," & (r2 Mod 256).ToString & "," & (r3 Mod 256).ToString & ")"""
        End Select
        'jrs 2020/06/07 End If
        getColor = sColor
    End Function

    Private Sub mnuExpNote_Click()
        Dim msg As String
        msg = "The components of the growth accounting form will not sum to economic growth (the sixth column) in the early forecast years because"
        msg = msg & " IFs uses exogenous growth data and forecasts from the IMF to override its own computations in those years (through 2022 as of late 2019 IMF values availability)."
        msg = msg & " Thereafter the sums of capital, labor, MFP (multifactor productivity), and capacity use (utilization) should sum quite closely to the value added (VADD) growth"
        msg = msg & " for the sectors that IFs represents only in value terms in its computable general equilibrium model system (those sectors other than agriculture and energy). "
        msg = msg & " For agriculture and energy, calculated growth in their respective partial equilibrium physical models (then fed to the general equilibrium model) does not have the same structure"
        msg = msg & " based on Cobb-Douglas equations with endogenous MFP as the non-physical sectors and the sums will be less useful.  For drill-down analysis into MFP, for the sectors represented in value terms only, the sums of the Technology Base"
        msg = msg & " MFP column (basic catch-up with productivity of the global leader) and the next five columns representing terms that vary with a wide variety of country-specific productivity drivers should sum to a column called SUM_MFPs"
        msg = msg & " that can be turned on with a Display Format sub-option and that sum should also be very close to the MFP column.  Small discrepancies are attributable to the complexities of the full MFP representation system."

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub
    Private Sub mnuDevProfGo_Click()
        'Always first
        Dim sDefRegion As String = lstDim1.SelectedValue
        'Load code
        Dim rs As DataTable
        SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT [ISO3166-2 Codes] FROM [Country Translation] as c, " &
                Regionalization & " as r where c.Country = r.Country AND NewRegion = " & CStr(lstDim1.SelectedIndex + 1), "", rs)
        If rs.Rows.Count = 0 Then
            Session("MMCountry") = ""
            rs = Nothing
        Else
            Session("MMCountry") = rs.Rows(0).Item(0)
            sDefRegion = Session("MMCountry")
        End If

        'Select scenario Name
        Dim sDefSceName As String = lstFiles.SelectedValue
        'Dim sCtryCode As String = "AF"
        Support.FormManager.StateStore(Me)
        'ttt= Afghanistan, how to get AF?
        'jrs 2020/06/06 Support.FormManager.Show(Me, "frm_DevelopmentProfile.aspx?Country=" & PrepareForURL(sDefRegion))
        Support.FormManager.Show(Me, "frm_DevelopmentProfile.aspx?Country=" & PrepareForURL(sDefRegion))
    End Sub
    Private Sub mnuAdjToGrowth_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2019/01/19
        Dim bSUM As Boolean
        bSUM = (cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").IconImage = "checkbox.gif")

        If bSUM Then
            mnuSUMMFPComp4Val_Click(eventSender, New System.EventArgs)
        End If
        If Session("bUseDisplayAdj") Then
            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuAdjToGrowth").IconImage = "blank.gif"
            Session("bUseDisplayAdj") = False
        Else
            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuAdjToGrowth").IconImage = "checkbox.gif"
            Session("bUseDisplayAdj") = True
        End If

        If lblJS.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdDraw_Click(Me, New EventArgs)
        ElseIf grdGrowth.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdTable_Click(Me, New EventArgs)
        End If
        If bSUM Then
            mnuSUMMFPComp4Val_Click(eventSender, New EventArgs)
        End If
    End Sub
    'jrs 2019/01/18
    Private Sub mnuSUMMFPComp4Val_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Session("bUseDisplaySUMMFP") Then
            Session("bUseDisplaySUMMFP") = False
            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").IconImage = "blank.gif"
        Else
            Session("bUseDisplaySUMMFP") = True
            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").IconImage = "checkbox.gif"
        End If
        'ttt Add 1 line below----
        If grdGrowth.Rows.Count = 0 Then Return
        If cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").IconImage = "checkbox.gif" Then
            Dim dt = DirectCast(Session("DataForGrid"), DataTable)
            Dim Headers As New List(Of String)
            Dim HasCol As Boolean = False
            For Each column As DataColumn In dt.Columns
                If column.ColumnName = "SUMMFP" Then
                    HasCol = True
                    Exit For
                End If
            Next
            If HasCol Then Return

            For j = 0 To Session("TotalGraphs")
                Headers.Add(grdGrowth.HeaderRow.Cells(j).Text)
            Next j
            Dim col As New DataColumn
            col.ColumnName = "SUMMFP"
            dt.Columns.Add("SUMMFP")
            'dt.Rows(4).Item("SUMMFP") = 55
            For j = 1 To Session("TotalGraphs")
                If Session("mLegends")(4, j) = "Knowledge MFP" Or Session("mLegends")(4, j) = "Human Capital MFP" Or
                    Session("mLegends")(4, j) = "Social Capital MFP" Or Session("mLegends")(4, j) = "Physical Capital MFP" Or
                    Session("mLegends")(4, j) = "Correction MFP" Or Session("mLegends")(4, j) = "Technology Base MFP" Then

                    For i = 1 To Session("TotYears")
                        'jrs 2026/03/09
                        If IsDBNull(dt.Rows(i - 1).Item(Session("TotalGraphs") + 1)) Then
                            dt.Rows(i - 1).Item(Session("TotalGraphs") + 1) = 0
                        End If
                        'ttt VB6: .Text = Format(Val(.Text) + mPLOT(i, j), "0.00")
                        'jrs 2020/06/06 dt.Rows(i - 1).Item(j - 1) = (NoNullDec(dt.Rows(i - 1).Item(j - 1)) + NoNullDec(Session("mPLOT")(i, j))).ToString("N2")
                        'jrs 2026/03/09 added CDbl() the "N2" was giving an error
                        dt.Rows(i - 1).Item(Session("TotalGraphs") + 1) = CDbl(((dt.Rows(i - 1).Item(Session("TotalGraphs") + 1)) + (Session("mPLOT")(i, j)))).ToString("N2")
                    Next i
                End If
            Next j
            grdGrowth.DataSource = dt
            Session("DataForGrid") = dt
            grdGrowth.DataBind()
            '--Format----
            For j = 0 To Session("TotalGraphs")
                grdGrowth.HeaderRow.Cells(j).Text = Headers(j)
            Next j
            For i = 0 To Session("TotYears") - 1
                grdGrowth.Rows(i).Cells(0).BackColor = Color.LightGray
                'bold
                grdGrowth.Rows(i).Cells(0).Text = Server.HtmlDecode("<span style='color: #000000;'><b>" & grdGrowth.Rows(i).Cells(0).Text & "</b></span>")
            Next

            'Change the fourth line of header for new Col
            grdGrowth.HeaderRow.Cells(Session("TotalGraphs") + 1).Text = Headers(Session("TotalGraphs")).Substring(0, Headers(Session("TotalGraphs")).LastIndexOf("</br>")) & "</br>" & "SUM_MFPs"


            'Add One Col
            'grdGrowth.cols = grdGrowth.cols + 1
            'With grdGrowth
            '    'Load Legends
            '    j = grdGrowth.cols - 1
            '    .col = j
            '    Dim i As Integer
            '    For i = 1 To 3
            '        .row = i - 1
            '        .Text = mLegends(i, j - 1)
            '    Next i
            '    .row = 3
            '    .Text = "SUM_MFPs"
            '    .CellAlignment = flexAlignLeftCenter
            '    .ColWidth(j) = 0

            '    'Load Growth Values
            '    For j = 1 To TotalGraphs
            '        If mLegends(4, j) = "Knowledge MFP" Or mLegends(4, j) = "Human Capital MFP" Or
            '        mLegends(4, j) = "Social Capital MFP" Or mLegends(4, j) = "Physical Capital MFP" Or
            '        mLegends(4, j) = "Correction MFP" Or mLegends(4, j) = "Technology Base MFP" Then
            '            For i = 1 To totYears
            '                .row = i + 3
            '                .Text = Format(Val(.Text) + mPLOT(i, j), "0.00")
            '            Next i
            '        End If
            '    Next j
            '    .ColWidth(j) = 2000
            'End With

            cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = False
        Else
            'Remove Col
            Dim dt = DirectCast(Session("DataForGrid"), DataTable)

            For Each column As DataColumn In dt.Columns
                If column.ColumnName = "SUMMFP" Then
                    Dim Headers As New List(Of String)
                    For j = 0 To Session("TotalGraphs")
                        'jrs 2026/03/09 added check to avoid error when unchecking the option more than once
                        If j < grdGrowth.HeaderRow.Cells.Count Then
                            Headers.Add(grdGrowth.HeaderRow.Cells(j).Text)
                        End If
                    Next j
                    dt.Columns.Remove(column)
                    grdGrowth.DataSource = dt
                    Session("DataForGrid") = dt
                    grdGrowth.DataBind()
                    '--Format----
                    For j = 0 To Session("TotalGraphs")
                        grdGrowth.HeaderRow.Cells(j).Text = Headers(j)
                    Next j
                    For i = 0 To Session("TotYears") - 1
                        grdGrowth.Rows(i).Cells(0).BackColor = Color.LightGray
                        'bold
                        grdGrowth.Rows(i).Cells(0).Text = Server.HtmlDecode("<span style='color: #000000;'><b>" & grdGrowth.Rows(i).Cells(0).Text & "</b></span>")
                    Next
                    Exit For
                End If
            Next
            cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True
        End If
    End Sub
    Private Sub LoadGrowthContribution(ByVal TotalGraphs As Integer, ByVal iFile As Integer,
        ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
        ByVal sVarDesc As String)
        '    (ByVal TotalGraphs As Integer, ByVal sFileName As String, ByRef dbM As OleDb.OleDbConnection,
        'ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sType As String,
        'ByVal sVarDesc As String)
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
        'myCMD.Connection = dbM
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
        '===
        Dim sVar As String = "", VarVals() As Single, VarIdx As Integer, TypeIdx As Integer, CDALFS() As Single
        ReDim VarVals(Session("LastYear") - Session("FirstYear") + 1)
        ReDim CDALFS(Session("LastYear") - Session("FirstYear") + 1)
        Dim VADDs(1, NSec) As Single
        Dim sVar2 As String, VarIdx2 As Integer, SumVadds As Single, SumProd As Single
        Dim Y As Integer
        Dim X As Integer, prevY As Integer
        If sVarDesc = "Capital" Then
            sVar = "KS"
        ElseIf sVarDesc = "Labor" Then
            sVar = "LABS"
        ElseIf sVarDesc = "MFP" Then
            'jrs 2014/07/29 sVar = "MFPRATE"
            sVar = "TEFF"
        ElseIf sVarDesc = "Capacity Use" Then 'jrs 2019/03/26
            sVar = "ECGRCAPUTGDP"
        ElseIf sVarDesc = "Climate Impact" Then 'jrs 2019/03/26
            sVar = "ECGRCLIMGDP"
        ElseIf sVarDesc = "Technology Base MFP" Then
            If sType = "Total" Then
                sVar = "MFPBAS"
            Else
                sVar = "MFPRATE"
            End If
        ElseIf sVarDesc = "Knowledge MFP" Then
            sVar = "MFPKN"
        ElseIf sVarDesc = "Human Capital MFP" Then
            sVar = "MFPHC"
        ElseIf sVarDesc = "Social Capital MFP" Then
            sVar = "MFPSC"
        ElseIf sVarDesc = "Physical Capital MFP" Then
            sVar = "MFPPC"
        ElseIf sVarDesc = "Correction MFP" Then
            If sType = "Total" Then
                sVar = "MFPCOR"
            Else
                sVar = "MFPCORS"
            End If
        ElseIf sVarDesc = "GDP Growth (for comparison)" Then 'jrs 2018/09/13
            'jrs 2019/03/06 added and sType = "Total"
            If sType = "Total" Then
                sVar = "GDPR"
            Else
                sVar = "VADD"
            End If
        End If
        bSuccess = False
        VarIdx = IfsVars.Item(sVar).Index
        TypeIdx = gGetDimIdx(VDim2(VarIdx), sType, False, Me)
        If TypeIdx = -1 AndAlso sType <> "Total" Then
            TypeIdx = gGetDimIdx(2, sType, False, Me)
        End If
        'Dim iFile As Integer = 1
        'jrs 2020/06/06
        'Dim sBinFile As String
        'Dim BinFileIdx As Integer
        'BinFileIdx = -1
        ''ttt Add line
        'Dim sFileName = "ifsPopWork.mdb"
        ''loop through files
        'For f = 0 To lstFiles.Items.Count - 1
        '    If lstFiles.Items(f).Selected Then
        '        Select Case f
        '            Case 0
        '                sFileName = "ifsPopWork.mdb"
        '            Case 1
        '                sFileName = "ifsPop.mdb"
        '            Case Else
        '                Dim pos As Integer
        '                pos = InStr(lstFiles.Items(f).Text, "-") + 1
        '                sFileName = "ifsPop" & Trim(Mid(lstFiles.Items(f).Text, pos))
        '                sFileName = Replace(sFileName, ".run", ".mdb")
        '        End Select
        '    End If
        'Next
        'If sFileName = "ifsPopWork.mdb" Then

        '    'sBinFile = "Working.run"
        '    'BinFileIdx = 0

        '    'JAM 2020-05-25
        '    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then
        '        sBinFile = "Working.run"
        '        BinFileIdx = 0
        '    Else
        '        sBinFile = "Working.run.db"
        '        BinFileIdx = 0
        '    End If


        'ElseIf sFileName = "ifsPop.mdb" Then
        '    'sBinFile = "ifsBase.run"
        '    'BinFileIdx = 1

        '    'JAM 2020-05-25
        '    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then
        '        sBinFile = "ifsBase.run"
        '        BinFileIdx = 1
        '    Else
        '        sBinFile = "ifsBase.run.db"
        '        BinFileIdx = 1
        '    End If

        'Else
        '    'JAM 2020-05-24
        '    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") Then

        '        sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN"), "IFSPOP", "")
        '        For k = 2 To lstFiles.Items.Count - 1
        '            If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
        '                BinFileIdx = k
        '                Exit For
        '            End If
        '        Next k


        '    Else
        '        sBinFile = Replace(Replace(UCase(sFileName), ".MDB", ".RUN.DB"), "IFSPOP", "")
        '        For k = 2 To lstFiles.Items.Count - 1
        '            If sFileName = "ifsPop" & lstFiles.Items(k).Text & ".mdb" Then
        '                BinFileIdx = k
        '                Exit For
        '            End If
        '        Next k
        '    End If

        'End If
        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        Session("bLoadForDisplaySuccess") = False
        'jrs 2020/06/06 
        'gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sType, TypeIdx, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
        '                   True, True, bGrp, 1, VarVals, 1, 1,
        '    Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        '    Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sType, TypeIdx, "", -1, "", iFile,
                           False, False, bGrp, 1, VarVals, 1, 1,
            Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay" + iFile.ToString), Session("df" + sVar + iFile.ToString))
        Else
            gLoadForDisplay(sVar, VarIdx, sReg, iReg, sType, TypeIdx, "", -1, "", iFile, True, True, bGrp, 1, VarVals, 1,
            1, Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"),
            Session("PutFdrvRun"), Session("ShortSce"), Session("FileForDisplay" + iFile.ToString), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
        If sVarDesc = "MFP" Then
            X = 1
            For Y = 1 + Session("Interval") To Session("FinYear")(1) Step Session("Interval")
                Session("mPLOT")(X, TotalGraphs) = VarVals(Y) / VarVals(Y - Session("Interval"))
                'mnuAnnual.Checked
                If Session("bUseDisplayAnnual") Then
                    Session("mPLOT")(X, TotalGraphs) = ((Session("mPLOT")(X, TotalGraphs) ^ (1 / Session("Interval"))) - 1) * 100
                Else
                    Session("mPLOT")(X, TotalGraphs) = (Session("mPLOT")(X, TotalGraphs) - 1) * 100
                End If
                X = X + 1
                If X > Session("TotYears") Then Exit For
            Next Y
        ElseIf sVar = "MFPCOR" Then
            ReDim VADDs(Session("LastYear") - Session("FirstYear") + 1, NSec)
            Dim VADDTotal(Session("LastYear") - Session("FirstYear") + 1) As Single
            sVar2 = "VADD"
            VarIdx2 = IfsVars.Item(sVar2).Index
            TypeIdx = gGetDimIdx(VDim2(VarIdx2), sType, False, Me)
            If TypeIdx > 0 Then
                For X = 1 To NSec
                    'jrs 2020/06/06
                    '        gLoadForDisplayNET(sVar2, VarIdx2, sReg, iReg, "", X, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
                    '               True, True, bGrp, 1, VADDs, X, 1,
                    'Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    '  Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(sVar2, VarIdx2, sReg, iReg, "", X, "", -1, "", iFile,
                           False, False, bGrp, 1, VADDs, X, 1,
            Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
              Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay" + iFile.ToString), Session("df" + sVar + iFile.ToString))
                    Else
                        gLoadForDisplay(sVar2, VarIdx2, sReg, iReg, "", X, "", -1, "", iFile,
                           False, False, bGrp, 1, VADDs, X, 1,
            Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileForDisplay" + iFile.ToString), Session("FinYear"),
              Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    End If

                    For Y = 1 To Session("FinYear")(1)
                        VADDTotal(Y) = VADDTotal(Y) + VADDs(Y, X)
                    Next Y
                Next X
                For Y = 1 To Session("FinYear")(1)
                    VarVals(Y) = VarVals(Y) * VADDs(Y, TypeIdx) / VADDTotal(Y)
                Next Y
            End If


            X = 1
            Session("mPLOT")(X, TotalGraphs) = 1
            For Y = 1 To Session("FinYear")(1)
                Session("mPLOT")(X, TotalGraphs) = Session("mPLOT")(X, TotalGraphs) * (1 + VarVals(Y) / 100) '* CAPUT(y))
                If Y Mod Session("Interval") = 0 Then
                    If Session("bUseDisplayAnnual") Then
                        Session("mPLOT")(X, TotalGraphs) = ((Session("mPLOT")(X, TotalGraphs) ^ (1 / Session("Interval"))) - 1) * 100
                    Else
                        Session("mPLOT")(X, TotalGraphs) = (Session("mPLOT")(X, TotalGraphs) - 1) * 100
                    End If
                    X = X + 1
                    If X > Session("TotYears") Then Exit For
                    Session("mPLOT")(X, TotalGraphs) = 1
                End If
            Next Y
        ElseIf sVar = "MFPRATE" Then
            X = 1
            Session("mPLOT")(X, TotalGraphs) = 1
            For Y = 1 To Session("FinYear")(1)
                Session("mPLOT")(X, TotalGraphs) = Session("mPLOT")(X, TotalGraphs) * (1 + VarVals(Y))  '* CAPUT(y))
                If Y Mod Session("Interval") = 0 Then
                    If Session("bUseDisplayAnnual") Then
                        Session("mPLOT")(X, TotalGraphs) = ((Session("mPLOT")(X, TotalGraphs) ^ (1 / Session("Interval"))) - 1) * 100
                    Else
                        Session("mPLOT")(X, TotalGraphs) = (Session("mPLOT")(X, TotalGraphs) - 1) * 100
                    End If
                    X = X + 1
                    If X > Session("TotYears") Then Exit For
                    Session("mPLOT")(X, TotalGraphs) = 1
                End If
            Next Y
        ElseIf Mid(sVar, 1, 3) = "MFP" Then
            X = 1
            Session("mPLOT")(X, TotalGraphs) = 1
            For Y = 2 To Session("FinYear")(1)
                Session("mPLOT")(X, TotalGraphs) = Session("mPLOT")(X, TotalGraphs) * (1 + VarVals(Y) / 100) '* CAPUT(y))
                If Y Mod Session("Interval") = 0 Then
                    If Session("bUseDisplayAnnual") Then
                        Session("mPLOT")(X, TotalGraphs) = ((Session("mPLOT")(X, TotalGraphs) ^ (1 / Session("Interval"))) - 1) * 100
                    Else
                        Session("mPLOT")(X, TotalGraphs) = (Session("mPLOT")(X, TotalGraphs) - 1) * 100
                    End If
                    X = X + 1
                    If X > Session("TotYears") Then Exit For
                    Session("mPLOT")(X, TotalGraphs) = 1
                End If
            Next Y

        ElseIf sVar = "GDPR" Or sVar = "ECGRCAPUTGDP" Or sVar = "ECGRCLIMGDP" Then 'jrs 2019/03/26 'JRS 2018/09/13
            X = 1
            For Y = 1 + Session("Interval") To Session("FinYear")(1) Step Session("Interval")
                Session("mPLOT")(X, TotalGraphs) = VarVals(Y)
                'jrs 2019/03/26
                If Session("Interval") > 1 Then
                    Session("mPLOT")(X, TotalGraphs) = Session("mPLOT")(X, TotalGraphs) / 100 + 1
                    For i = 1 To Session("Interval") - 1
                        Session("mPLOT")(X, TotalGraphs) = Session("mPLOT")(X, TotalGraphs) * (1 + VarVals(Y - i) / 100)
                    Next
                    If Session("bUseDisplayAnnual") Then
                        Session("mPLOT")(X, TotalGraphs) = Session("mPLOT")(X, TotalGraphs) ^ (1 / Session("Interval"))  'jrs 2019/03/29
                    End If
                    Session("mPLOT")(X, TotalGraphs) = (Session("mPLOT")(X, TotalGraphs) - 1) * 100
                End If
                X = X + 1
                If X > Session("TotYears") Then Exit For
            Next Y
        ElseIf sVar = "VADD" Then 'JRS 2019/03/06
            X = 1
            For Y = 1 + Session("Interval") To Session("FinYear")(1) Step Session("Interval")
                prevY = Y - Session("Interval")
                If VarVals(prevY) <> 0 Then
                    If Session("bUseDisplayAnnual") Then
                        Session("mPLOT")(X, TotalGraphs) = (((VarVals(Y) - VarVals(prevY)) / VarVals(prevY) + 1) ^ (1 / Session("Interval")) - 1) * 100 'jrs 2019/03/29
                    Else
                        Session("mPLOT")(X, TotalGraphs) = (VarVals(Y) - VarVals(prevY)) / VarVals(prevY) * 100
                    End If
                End If
                X = X + 1
                If X > Session("TotYears") Then Exit For
            Next Y
        Else
            If sVarDesc <> "Capital" AndAlso sVarDesc <> "Labor" Then
                X = 0
                For Y = 1 + Session("Interval") To Session("FinYear")(1) Step Session("Interval")
                    X = X + 1
                    If X > Session("TotYears") Then Exit For
                    prevY = Y - Session("Interval")
                    If VarVals(prevY) <> 0 Then
                        Session("mPLOT")(X, TotalGraphs) = (VarVals(Y) - VarVals(prevY)) / VarVals(prevY) '* CAPUT(y)
                    End If
                Next Y
            End If
            ReDim VADDs(1, NSec)
            Dim Alpha As Single = 1
            'jrs 2019/03/05
            Dim Alpha_Prev As Single = 1
            sVar2 = "VADD"
            VarIdx2 = IfsVars.Item(sVar2).Index
            For X = 1 To NSec
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(sVar2, VarIdx2, sReg, iReg, "", X, "", -1, "", iFile,
       False, False, bGrp, 1, VADDs, X, 1,
Session("FirstYear"), Session("FirstYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay" + iFile.ToString), Session("df" + sVar2 + iFile.ToString))
                Else
                    gLoadForDisplay(sVar2, VarIdx2, sReg, iReg, "", X, "", -1, "", iFile,
                                       True, True, bGrp, 1, VADDs, X, 1,
                                       Session("FirstYear"), Session("FirstYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileForDisplay" + iFile.ToString), Session("FinYear"),
Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If


                'jrs 2020/06/06
                'gLoadForDisplayNET(sVar2, VarIdx2, sReg, iReg, "", X, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
                '               True, True, bGrp, 1, VADDs, X, 1,
                'Session("FirstYear"), Session("FirstYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                '  Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)


                SumProd = SumProd + salphas(1, X) * VADDs(1, X)
                SumVadds = SumVadds + VADDs(1, X)
            Next X
            'Load alpha
            sVar = "CDALF"
            'gLoadForDisplayNET(sVar, -1, sReg, iReg, sType, -1, "", -1, "", iFile,
            '                   True, True, bGrp, 1, CDALFS, 1, 1,
            'FirstYear, LastYear, 0, True, sTitle, False)

            'jrs 2020/06/06
            'gLoadForDisplayNET(sVar, -1, sReg, iReg, sType, -1, "", -1, Session("PutFdrvRun") & sBinFile, BinFileIdx,
            '                   True, True, bGrp, 1, CDALFS, 1, 1,
            '    Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
            '      Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sVar, -1, sReg, iReg, sType, -1, "", -1, "", iFile,
                               False, False, bGrp, 1, CDALFS, 1, 1,
                Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                  Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay" + iFile.ToString), Session("df" + sVar + iFile.ToString))
            Else
                gLoadForDisplay(sVar, -1, sReg, iReg, sType, -1, "", -1, "", iFile,
                True, True, bGrp, 1, CDALFS, 1, 1,
                Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay" + iFile.ToString), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If

            X = 0
            For Y = 1 + Session("Interval") To Session("FinYear")(1) Step Session("Interval")
                X = X + 1
                If X > Session("TotYears") Then Exit For
                'jrs 2019/03/05 *** begin ***
                Alpha_Prev = Alpha
                prevY = Y - Session("Interval")
                'jrs 2019/03/05 *** end ***

                If TypeIdx > 0 Then
                    Alpha = SumVadds / SumProd * salphas(1, TypeIdx)
                    'jrs 2019/03/13
                    If Y = 1 + Session("Interval") Then
                        Alpha_Prev = Alpha * CDALFS(1)
                    End If
                    Alpha = Alpha * CDALFS(Y)
                Else
                    Alpha = CDALFS(Y)
                    'jrs 2019/03/13
                    If Y = 1 + Session("Interval") Then
                        Alpha_Prev = CDALFS(1)
                    End If
                End If
                'ttt Add Temp IF to escape IsNaN value
                If Double.IsNaN(Alpha) OrElse Alpha = 0 Then
                    Alpha = 1
                End If
                If Double.IsNaN(Alpha_Prev) OrElse Alpha_Prev = 0 Then
                    Alpha_Prev = 1
                End If
                If sVarDesc = "Capital" Then
                    Dim valu = (VarVals(Y) ^ Alpha) / (VarVals(prevY) ^ Alpha_Prev) - 1
                    Session("mPLOT")(X, TotalGraphs) = valu
                    'Debug.WriteLine(valu)
                ElseIf sVarDesc = "Labor" Then

                    Session("mPLOT")(X, TotalGraphs) = (VarVals(Y) ^ (1 - Alpha)) / (VarVals(prevY) ^ (1 - Alpha_Prev)) - 1
                End If
                'jrs 2020/06/07
                If Session("bUseDisplayAnnual") Then
                    Session("mPLOT")(X, TotalGraphs) = ((Session("mPLOT")(X, TotalGraphs) + 1) ^ (1 / Session("Interval")) - 1) * 100
                Else
                    Session("mPLOT")(X, TotalGraphs) = Session("mPLOT")(X, TotalGraphs) * 100
                End If
                'Session("mLegends")(1, TotalGraphs) = "GROWTH"
                'jrs 2010/02/09
                'Session("mLegends")(1, TotalGraphs) = sReg 'USA
                'Session("mLegends")(2, TotalGraphs) = "All Sectors"
                'Session("mLegends")(3, TotalGraphs) = "Working"
                'Session("mLegends")(4, TotalGraphs) = sVarDesc 'Capital
            Next Y
        End If
        Session("mLegends")(1, TotalGraphs) = sReg 'USA
        Session("mLegends")(2, TotalGraphs) = If(sType = "Total", "All Sectors", sType)
        If iFile = 0 Then 'JAM 2020-05-24
            Session("mLegends")(3, TotalGraphs) = "Working"
        ElseIf iFile = 1 Then
            Session("mLegends")(3, TotalGraphs) = "Base"
        Else
            'jrs 2020/06/06 Session("mLegends")(3, TotalGraphs) = Replace(Replace(sFileName, "ifsPop", ""), ".mdb", "")
            Session("mLegends")(3, TotalGraphs) = Replace(Replace(lstFiles.Items(iFile).Text, iFile & " - ", ""), ".run.db", "")
        End If
        Session("mLegends")(4, TotalGraphs) = sVarDesc 'Capital
    End Sub
    Private Sub mnuAnnual_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim bSUM As Boolean = False
        bSUM = (cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuSUMMFPComp4Val").IconImage = "checkbox.gif")
        If bSUM Then
            mnuSUMMFPComp4Val_Click(eventSender, New System.EventArgs)
        End If
        If Session("bUseDisplayAnnual") Then
            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuAnnual").IconImage = "blank.gif"
            Session("bUseDisplayAnnual") = False
        Else
            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuAnnual").IconImage = "checkbox.gif"
            Session("bUseDisplayAnnual") = True
        End If
        If lblJS.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdDraw_Click(Me, New EventArgs)
        ElseIf grdGrowth.Visible Then
            cmdClear_Click(Me, New EventArgs)
            cmdTable_Click(Me, New EventArgs)
        End If
        If bSUM Then
            mnuSUMMFPComp4Val_Click(eventSender, New System.EventArgs)
        End If
    End Sub
    Public Sub mnuDisplayRunHorizon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If CBool(Session("HistoricBase")) = True Then
            Msg = "You cannot change the horizon for historical analysis."
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
            Exit Sub
        End If
        cmb_Horizon.Visible = True
        frm_Horizon.Visible = True
        'jrs 2005/12/11
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If
        cmb_Horizon.Items.Clear()
        cmb_Horizon.Items.Add("")
        'jrs 2008/12/18
        cmb_Horizon.Items.Add(New ListItem(Session("FirstYear").ToString))

        For ICount = Session("FirstYear") + Session("Interval") To Session("LastYearForecast") Step Session("Interval") 'jrs 2005/12/11 added step interval
            cmb_Horizon.Items.Add(New ListItem(CStr(ICount)))
        Next ICount


    End Sub
    Private Sub cmb_Horizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged

        Dim Ind As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2008/12/22 changed from 0 to -1, added FirstYear
        Ind = cmb_Horizon.SelectedIndex - 1
        'jrs 2005/12/11
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 5
        End If

        'JRS 11/03/03 CHANGE LASTYEAR WITH DISPLAYLASTYEAR
        If Ind >= 1 Then
            Session("DisplayLastYear") = Session("FirstYear") + Ind * Session("Interval") 'jrs 2005/12/11
        Else
            Session("DisplayLastYear") = Val(cmb_Horizon.SelectedItem)
            'jrs 2008/12/18 If Session("DisplayLastYear") > LastYearForecast Or Session("DisplayLastYear") < FirstYearForecast + 1 Then Exit Sub
            If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                Session("DisplayLastYear") = Session("LastYear")
                LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If

        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False
    End Sub
    Private Sub mnuMFPDec_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If Session("bUseDisplaySUMMFP") Then
            mnuSUMMFPComp4Val_Click(eventSender, New System.EventArgs)
        End If
        'jrs 2014/07/31

        If Session("bUseDisplayMFPDec") Is Nothing OrElse Not Session("bUseDisplayMFPDec") Then
            Session("bUseDisplayMFPDec") = True
            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuMFPDec").IconImage = "checkbox.gif"

        Else
            Session("bUseDisplayMFPDec") = False
            cswmWebMenu.Groups("mnuDisplayFormat").Items("mnuMFPDec").IconImage = "blank.gif"
        End If


        If Session("bUseDisplayMFPDec") Then
            'jrs 2018/09/13
            '        If lstVars.List(lstVars.ListCount - 1) = "GDP Growth (for comparison)" Then
            '            lstVars.RemoveItem (lstVars.ListCount - 1)
            '        End If
            'ADD MFP components
            If Not lstVars.Items.Contains(New ListItem("Technology Base MFP")) Then
                lstVars.Items.Add("Technology Base MFP")
                lstVars.Items.Add("Knowledge MFP")
                lstVars.Items.Add("Human Capital MFP")
                lstVars.Items.Add("Social Capital MFP")
                lstVars.Items.Add("Physical Capital MFP")
                lstVars.Items.Add("Correction MFP")
                For i = 6 To 11
                    lstVars.Items(i).Selected = True
                Next
            End If


        Else
            'Remove MFP components
            lstVars.Items.Clear()
            lstVars.Items.Add("Capital")
            lstVars.Items.Add("Labor")
            lstVars.Items.Add("MFP")
            lstVars.Items.Add("Capacity Use")
            lstVars.Items.Add("Climate Impact")
            lstVars.Items.Add("GDP Growth (for comparison)")
            For i = 0 To 5
                lstVars.Items(i).Selected = True
            Next
        End If
        If lblJS.Visible Then
            cmdClear_Click(Me, New System.EventArgs)
            cmdDraw_Click(Me, New System.EventArgs)
        ElseIf grdGrowth.Visible Then
            cmdClear_Click(Me, New System.EventArgs)
            cmdTable_Click(Me, New System.EventArgs)
        End If

    End Sub
    Private Sub AdjustToActualGrowth(bCapital As Boolean, bLabor As Boolean, bMFP As Boolean)
        Dim PotGrowthRate(Session("TotYears") + 1, Session("TotalGraphs")) As Single
        Dim i As Integer, j As Integer, k As Integer
        Dim colIdxs = New List(Of String)

        'Sum all 3 elements of growth
        '============================
        For j = 1 To Session("TotalGraphs")
            If Session("mLegends")(4, j) = "Capital" Then
                colIdxs.Add(Session("mLegends")(1, j) & Session("mLegends")(2, j) & Session("mLegends")(3, j))
                k = colIdxs.Count
            Else
                k = colIdxs.IndexOf(Session("mLegends")(1, j) & Session("mLegends")(2, j) & Session("mLegends")(3, j))
                k += 1
            End If
            For i = 1 To Session("TotYears")
                PotGrowthRate(i, k) = PotGrowthRate(i, k) + Session("mPLOT")(i, j)
            Next i
        Next j

        'Load VADD to compute actual growth rate
        '=======================================

        Dim ActGrowthRate(Session("TotYears") + 1, Session("TotalGraphs")) As Single
        Dim VarVals(Session("LastYear") - Session("FirstYear") + 1) As Single
        Dim sVar As String, VarIdx As Integer, sSectors As String
        sVar = "VADD"
        VarIdx = IfsVars.Item(sVar).Index
        'Dim fileIdx As Integer
        For j = 1 To Session("TotalGraphs")
            If Session("mLegends")(4, j) = "Capital" Then
                'fileIdx = getFileNum(Session("mLegends")(3, j))
                k = colIdxs.IndexOf(Session("mLegends")(1, j) & Session("mLegends")(2, j) & Session("mLegends")(3, j))
                k += 1
                If Session("mLegends")(2, j) = "All Sectors" Then
                    sSectors = "Total"
                Else
                    sSectors = Session("mLegends")(2, j)
                End If
                Dim MyFileforDisplay As IStorage
                Dim MyDF As IDataframe
                If Session("mnuSQLite") = "True" Then
                    'jrs 2026/03/09 flag for groups should be Nothing to allow groups
                    gLoadForDisplayNET(sVar, VarIdx, Session("mLegends")(1, j), -1, sSectors, -1, "", -1, "", 1,
   False, False, Nothing, 1, VarVals, 1, 1,
Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay1"), Session("df" + sVar + "1"))
                Else
                    gLoadForDisplay(sVar, VarIdx, Session("mLegends")(1, j), -1, sSectors, -1, "", -1, "", 1,
   False, False, False, 1, VarVals, 1, 1,
Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileForDisplay1"), Session("FinYear"),
Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If
                ' gLoadForDisplay(sVar, VarIdx, Session("mLegends")(0, j), -1, sSectors, -1, "", -1, "", fileIdx,
                'True, True, Null, 1, VarVals, 1, 1,
                'Session("FirstYear"), Session("LastYear"), 0, True, sTitle, False)
                'ttt -> Session("mLegends")(1, j) -OLD= (0,j)


                Dim X As Integer = 1
                For i = Session("Interval") + 1 To Session("FinYear")(1) Step Session("Interval")
                    If X > Session("TotYears") + 1 Then Exit For
                    If VarVals(i - Session("Interval")) <> 0 Then
                        If Session("bUseDisplayAnnual") Then
                            ActGrowthRate(X, k) = (((VarVals(i) / VarVals(i - Session("Interval"))) ^ (1 / Session("Interval"))) - 1) * 100
                        Else
                            ActGrowthRate(X, k) = (VarVals(i) / VarVals(i - Session("Interval")) - 1) * 100
                        End If
                    End If
                    X = X + 1
                Next i
            End If
        Next j

        'Remove unnecessary columns
        '==========================
        If Not bCapital OrElse Not bLabor OrElse Not bMFP Then
            For j = 1 To Session("TotalGraphs")
                If Session("mLegends")(4, j) = "" Then Exit For
                If (Session("mLegends")(4, j) = "Capital" AndAlso Not bCapital) _
        OrElse (Session("mLegends")(4, j) = "Labor" AndAlso Not bLabor) _
        OrElse (Session("mLegends")(4, j) = "MFP" AndAlso Not bMFP) Then
                    DeleteCol(j)
                End If
            Next j
        End If

        'Compute Adjustment Factors = ActGrowthRate / PotGrowthRate
        '==========================================================

        'Apply Adjustment Factors
        '=========================
        For j = 1 To Session("TotalGraphs")
            'jrs 2018/09/13 no adjustment for GDPR
            If Session("mLegends")(4, j) <> "GDP Growth (for comparison)" Then
                k = colIdxs.IndexOf(Session("mLegends")(1, j) & Session("mLegends")(2, j) & Session("mLegends")(3, j))
                k += 1
                For i = 1 To Session("TotYears")
                    Session("mPLOT")(i, j) = Session("mPLOT")(i, j) * ActGrowthRate(i, k) / PotGrowthRate(i, k)
                Next i
            End If
        Next j
    End Sub
    Private Sub DeleteCol(col As Integer)
        Dim j As Integer, i As Integer
        For j = col To Session("TotalGraphs") - 1
            For i = 0 To 4
                Session("mLegends")(i, j) = Session("mLegends")(i, j + 1)
            Next i
            For i = 1 To Session("TotYears")
                Session("mPLOT")(i, j) = Session("mPLOT")(i, j + 1)
            Next i
        Next j
        'Delete last column
        For i = 0 To 4
            Session("mLegends")(i, Session("TotalGraphs")) = ""
        Next i
        For i = 1 To Session("TotYears")
            Session("mPLOT")(i, j) = 0
        Next i
        Session("TotalGraphs") = Session("TotalGraphs") - 1
    End Sub
    Public Function NoNullDec(o As Object) As Decimal
        'ttt-Temp function to adjust values to viewable area
        'Please edit or delete this function
        Try
            Dim res = CDec(o)
            Return If(Math.Abs(res) < 10, res, res Mod 10)
        Catch ex As Exception

        End Try
        Return 0
    End Function
End Class
