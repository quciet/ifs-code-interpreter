Option Strict Off
Option Explicit On
Imports NLog

Imports System.IO

Partial Class frm_RebuildBaseNew
    Inherits Page

    Private Shared _logger As NLog.Logger = LogManager.GetCurrentClassLogger()
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        'If Session("RunningYear") >= 2 Then
        '    Debug.Print("Run Init: " & Session("RunningYear"))
        'End If

        'CODEGEN: This method call is required by the Web Form Designer.
        'Do not modify it using the code editor.
        'CONVERSION_NOTE:  'Code' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
        'If m_vb6FormDefInstance Is Nothing Then 
        'If m_InitializingDefInstance Then 
        'm_vb6FormDefInstance = Me
        'Else 
        'Try 
        ''For the start-up form, the first instance created is the default instance.
        'If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then 
        'm_vb6FormDefInstance = Me
        'End If 
        'Catch 
        'End Try 
        'End If 
        'End If  
        InitializeComponent()


        If Not Page.IsPostBack Then
            'jrs 2005/05/25 changed First Year to First Year Forecast, if History plus Forecast
            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                    Session("FirstYear") = FirstYearForecast
                    ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
                End If
            End If

            Dim msg As String
            Dim Number As Single
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '-----------raman 17th March
            '    General_Run   ' setting of the base file and run file
            '-----------------------------------

            '----------raman 23rd April-----------
            ran_guided_use = False


            txt_RunYears.Visible = True
            txt_RunYears.Enabled = True

            txt_RunYears.Text = Str(Session("LastYear"))

            'jrs 2024/04/23 Remove timer control
            'JRS 05/29/04
            'Timer1.Visible = False
            'jrs 2011/10/09
            'lblJSReb.Visible = False

            'jrs 2018/08/14
            chkBilateralData.Visible = False
            If Dir(DataFileDir$ + "IFsForDyadic.db") <> "" Then
                chkBilateralData.Checked = True
            Else
                chkBilateralData.Checked = False
            End If
            chkBilateralData.Visible = True
            'jrs 2021/05/04
            lbl_RunYears.Text = "Change Last Year of model (Base Year is " & CStr(FirstYearForecast) & "):"

            'jrs 2023/01/16
            If Not (Session("GuidedBaseYear") = Nothing) Then
                txtAutomaticRun.Value = "1"
            End If
        End If
    End Sub

    Protected WithEvents IfsVB60 As System.Diagnostics.Process


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.IfsVB60 = New System.Diagnostics.Process

    End Sub
#End Region

    '------------------------------------------Run.Frm-----------------------------
    '------------------------------------------------------------------------------
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last revision by BBH on April 28, 2001


    '   Whatever you can do, or dream you can, begin it.  Boldness has genius,
    '   power and magic in it.
    '   Johann Wolfgang von Goethe

    Dim reset_bool As Boolean
    Dim rtim As Short
    '------raman 23rd April-----------
    Public ran_guided_use As Boolean
    'JRS 10/08/02 GUIDED USE
    'this is used to validate that we're really in step 4
    Dim local_Guided_use As Boolean

    '=============================Run.Frm=======================
    Private Sub cmd_Exit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit.Click
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub


    '=============================Run.Frm=======================
    'CONVERSION_NOTE: The code of Event 'frm_Run.frm_Run_Load' was moved to Method 'frm_Run.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_RebBase_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'PackJS()
        'jrs 2013/09/04
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
            ShowHelpJS("Running IFs", ""), True)

    End Sub

    Private Sub PackJS()
        'jrs 2011/09/20
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        For l = 1 To 4
            sJscript2 = sJscript2 & PackJScritpBuilderContainter(l.ToString)
        Next

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        'lblJSReb.Text = sJscript2
    End Sub




    Private Sub cmd_RebBase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_RebBase.Click
        'jrs 2024/04/24 let javascript do it
        'jrs 2006/03/23 Validation of Year to Run
        'cmd_Exit.Enabled = False
        'cmd_Exit.Visible = False
        chkBilateralData.Visible = False

        Dim Message As String
        If Not (IsNumeric(txt_RunYears.Text)) Then
            Message = "Use a valid number for Year to Run."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Dim bHist As Boolean = False

        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            If Not (Val(txt_RunYears.Text) >= FirstYearForecast And Val(txt_RunYears.Text) <= Session("LastYearForecast")) Then
                Message = "Year to Run must be between " & FirstYearForecast & " and " & Session("LastYearForecast") & "."
                LoadMessage(Message, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            'jrs 2006/07/21
            Session("FarmFlag") = True
        End If

        If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            bHist = True
            If Not (Val(txt_RunYears.Text) >= FirstYearHistory And Val(txt_RunYears.Text) <= LastYearHistory) Then
                Message = "Year to Run must be between " & FirstYearHistory & " and " & LastYearHistory & "."
                LoadMessage(Message, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            'jrs 2006/07/21
            Session("FarmFlagHist") = True
        End If

        'jrs 2021/01/14 *** start ***
        'If Dir(DataFileDir$ + "IFsHistSeriesDyadic.mdb") > "" Then
        'jam 2022/12/18
        If Dir(DataFileDir$ + "IFsHistSeriesDyadic.mdb") > "" Then
            're-link tables
            Dim strMsg As String

            'Run the Procedure, getting any error messages.
            strMsg = RefreshTableLinks()

            'strMsg will be a zero-length string if there is no error message.
            If Len(strMsg & "") = 0 Then
                Debug.Print("All; Tables; were; successfully; relinked.")
            Else
                'Notify the user of the errors.
                Session("Message") = strMsg
                LoadMessage("Bilateral Interaction Display with Gravity", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If
        'jrs 2021/01/14 *** end ***

        'jrs 2005/01/27
        Dim bContinue As Boolean = True
        'jrs 2024/04/24 Not necessary for Rebuild Base
        'SyncLock RunControl1
        '    If RunControl1.RunsAvailable Then
        '        RunControl1.IncreaseCounter()
        '        bContinue = True
        '        Session("RuningProgress") = True
        '    End If
        'End SyncLock

        Dim runManager As RunManager = RunManager.Instance

        If runManager.CanStartRun(Session.SessionID) And runManager.PrepareRun(Session.SessionID, DemandFile, txt_RunYears.Text, StandAloneMode, bHist, Session("PutFdrvRun"), Session("SessionIdx"), True, CBool(DevelopmentMode)) Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
                Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))

            If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
                Kill(Session("ScenarioFileDir") & "Working.Sce")
            End If

            'jrs 2022/01/06
            Dim sFileName, sWorkDir As String
            'jrs 2022/08/03
            Dim sExeDir As String
            If UseOnlySQLite Then
                sExeDir = "net8"
            Else
                sExeDir = "net471"
            End If

            If DevelopmentMode = True And Dir(StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\") > "" Then
                'jrs 2022/08/08
                If UseOnlySQLite Then
                    sFileName = DemandFile
                Else
                    sFileName = StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\" & DemandFile
                End If
                sWorkDir = StartDirec & "Code.Ifs-Translation\"
            Else
                sWorkDir = StartDirec & sExeDir & "\"
                'jrs 2022/08/08
                If UseOnlySQLite Then
                    sFileName = DemandFile
                Else
                    sFileName = sWorkDir & DemandFile
                End If
            End If

            'Process.Start("C:\My Documents\IFs\IFs.exe", "3")
            'Process.Start("Notepad.exe")
            'Support.FormManager.Show(Me, "frm_ScenarioManage.aspx")
            'IfsVB60.StartInfo.FileName = StartDirec & "net471\" & DemandFile
            'IfsVB60.StartInfo.WorkingDirectory = StartDirec & "net471\"
            IfsVB60.StartInfo.FileName = sFileName
            IfsVB60.StartInfo.WorkingDirectory = sWorkDir
            'jrs 2008/04/02 added 4th parameter, False to avoid messages that stop the process when there's an error
            'jrs 2022/08/05
            If UseOnlySQLite Then
                'jrs 2022/08/08
                If DevelopmentMode = True And Dir(StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\") > "" Then
                    IfsVB60.StartInfo.Arguments = """" & StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\ifs.dll"" 5 " & Trim(txt_RunYears.Text) & " " & Trim(Session("SessionIdx")) & " False False 0 " & bHist.ToString
                Else
                    IfsVB60.StartInfo.Arguments = """" & StartDirec & sExeDir & "\ifs.dll"" 5 " & Trim(txt_RunYears.Text) & " " & Trim(Session("SessionIdx")) & " False False 0 " & bHist.ToString
                End If
            Else
                IfsVB60.StartInfo.Arguments = "5 " & Trim(txt_RunYears.Text) & " " & Trim(Session("SessionIdx")) & " False False 0 " & bHist.ToString
            End If
            'IfsVB60.StartInfo.FileName = StartDirec & "Notepad.exe"

            Dim fs As Object
            fs = CreateObject("Scripting.FileSystemObject")
            If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
                Kill(Session("PutFdrvRun") & "progress.txt")
            End If

            'jrs 2023/04/20
            If Not (Session("FileForDisplay0") Is Nothing) Then
                Session("FileForDisplay0").dispose()
            End If
            If Not (Session("FileForDisplay1") Is Nothing) Then
                Session("FileForDisplay1").dispose()
            End If

            'jrs 2020/08/28
            Session("FileForDisplay0") = Nothing
            Session("FileForDisplay1") = Nothing
            DeleteDFInCache(0, Session)
            DeleteDFInCache(1, Session)

            'jrs 2023/04/20
            'System.GC.Collect()

            'jrs 2005/03/14
            Session("DisplayLastYear") = Val(txt_RunYears.Text)

            IfsVB60.Start()
            Session("IfsVB60") = IfsVB60
            'Timer1.Visible = True
            'lblJSReb.Visible = True
            'lblJSReb.Text = "Rebuild Base has begun - Please Wait"
            Session("LastYear") = txt_RunYears.Text
            'Do While Not IfsVB60.HasExited
            '    IfsVB60.WaitForExit(3000)
            'Loop

            'cmd_Start.Visible = False
            'cmd_Cancel.Visible = False
            'cmd_continue.Visible = True
        Else
            Message = "Other users are running the model, please try later."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

    End Sub


    Private Sub Timer1_Elapsed(ByVal sender As System.Object, ByVal e As MarkItUp.WebControls.TimerElapsedEventArgs) 'Handles Timer1.Elapsed
        ''mti WebForm Changes From June 21, 2004  - this proc edited 

        'mti WebForm Changes From June 21, 2004 -commented for testing
        IfsVB60 = Session("IfsVB60")

        Session("RunYears") = Val(txt_RunYears.Text)

        'mti WebForm Changes From June 21, 2004  - inserted for testing 
        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")

        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Dim sLine, prevLine As String

            Using reader As StreamReader = New StreamReader(CStr(Session("PutFdrvRun") & "progress.txt"))
                ' Read last line from file
                sLine = reader.ReadLine
                prevLine = sLine
                Do While Not sLine = Nothing
                    prevLine = sLine
                    sLine = reader.ReadLine
                Loop

                'lblJSReb.Text = prevLine
            End Using

        End If
        'mti WebForm Changes From June 21, 2004  -end commented for testing

        If IfsVB60.HasExited() Then
            'jrs 2023/06/06
            'If lblJSReb.Text <> "Table Functions were updated - Need to Restart" Then
            '    lblJSReb.Text = "VERY IMPORTANT: PLEASE READ. Rebuild of Base is complete.<br>"
            '    lblJSReb.Text = lblJSReb.Text & "IFsBase is usable but MODEL MUST BE RERUN to recompute <br>"
            '    lblJSReb.Text = lblJSReb.Text & "all calculated variables. RUN MODEL THROUGH LONGEST TIME HORIZON <br>"
            'End If

            cmd_Exit.Enabled = True
            cmd_Exit.Visible = True
            chkBilateralData.Visible = True
            'Timer1.Visible = False
            'mti WebForm Changes From June 21, 2004  - start

            'grf_WPOP.Visible = False

            '    'mti WebForm Changes From June 21, 2004 -end 

            RebuiltBase = True

            '   Eliminate working.sce so that doesn't contaminate new base run
            '   --------------------------------------------------------------

            Dim ScenarioFileDir$ = PutFdrv$ + Session("ScenarioFileSubDir")
            If Dir(ScenarioFileDir$ + "Working.Sce") <> "" Then
                Kill(ScenarioFileDir$ + "Working.sce")
            End If

            Dim InFile, OutFile As String
            If Session("HistoricBase") = False Then
                InFile = Session("IFs_MDB2Active").ToString.Replace(".MDB", ".db")
                OutFile = IFs_MDB.ToString.Replace(".MDB", ".db")
                FileCopy(InFile, OutFile)
                'jrs 2008/11/20
                FileCopy(Session("PutFdrvRun") & "ifsSmoking.db", DataFileDir$ & "ifsSmoking.db")
                If Dir(Session("PutFdrvRun") & "ifsForDyadicWork.db") <> "" Then
                    FileCopy(Session("PutFdrvRun") & "ifsForDyadicWork.db", DataFileDir$ & "ifsForDyadic.db")
                    'jam 2022/12/19
                    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                        FileCopy(Session("PutFdrvRun") & "ifsHistSeriesDyadic.mdb", DataFileDir$ & "ifsHistSeriesDyadic.mdb")
                    End If
                End If
                'jrs 2020/06/17
                'Remove ALL files from the FARM
                Dim sRunFile As String = Dir(PutFdrv & "RUNFILES\FARM\" & "*")
                Do While (sRunFile <> "")
                    Kill(PutFdrv & "RUNFILES\FARM\" & sRunFile)
                    sRunFile = Dir()
                Loop

                'jrs 2020/08/27
                InFile = Session("PutFdrvRun") & "Working.run.db"
                OutFile = Session("PutFdrvRun") & "IFsBase.run.db"
                FileCopy(InFile, OutFile)

            Else
                InFile = Session("IFs_MDB2Active").ToString.Replace(".MDB", ".db")
                OutFile = IFsHist_MDB.ToString.Replace(".MDB", ".db")
                FileCopy(InFile, OutFile)

                'jrs 2020/08/27
                InFile = Session("PutFdrvRun") & "Working.run.db"
                OutFile = Session("PutFdrvRun") & "IFsBaseHist.run.db"
                FileCopy(InFile, OutFile)
            End If

            'jrs 2024/04/10 added if statement, so that these buttons don't change if need to run again
            'If lblJSReb.Text <> "Table Functions were updated - Need to Restart" Then
            '    cmd_GotoRun.Visible = True
            '    cmd_RebBase.Visible = False
            'End If

            'jrs 2005/01/27
            If Session("RuningProgress") = True Then
                SyncLock RunControl1
                    If Not RunControl1.RunsZero Then
                        RunControl1.DecreaseCounter()
                        Session("RuningProgress") = False
                    End If
                End SyncLock
            End If

            'jrs 2023/01/16
            If Not (Session("GuidedBaseYear") = Nothing) Then
                'jrs 2023/08/08
                'If lblJSReb.Text <> "Table Functions were updated - Need to Restart" Then
                '    cmd_GotoRun_Click(Me, New EventArgs)
                'Else
                '    cmd_RebBase_Click(Me, New EventArgs)
                'End If
            End If

        End If

    End Sub

    Private Function PackJScritpBuilderContainter(ByVal sContainterIdx As String) As String
        If Session("RunningYear") < 2 Then
            PackJScritpBuilderContainter = ""
            Exit Function
        End If

        Dim sJscript2 As String = ""
        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container" & sContainterIdx & "').highcharts({" & vbCr
        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 75" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim gphIdx As Integer = 0
        gphIdx = CInt(sContainterIdx)
        Dim sVar As String = "", graphtitle As String = ""
        Dim Vals As Object
        Select Case gphIdx
            Case 1
                sVar = "POP"
                graphtitle = "World Population (Millions)"
                Vals = Session("rgWPOP")
            Case 2
                sVar = "GDP"
                graphtitle = "World GDP (Billion Currency)"
                Vals = Session("rgWGDP")
            Case 3
                sVar = "FST"
                graphtitle = "World Forest (Million Hectares)"
                Vals = Session("rgWFORST")
            Case 4
                sVar = "CO2"
                graphtitle = "Atmospheric CO2 (PPM)"
                Vals = Session("rgCO2PPM")
        End Select


        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "enabled: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "type: 'category'," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & ", min: " & Session("FirstYear") & vbCr
        'If Session("rgYear") Is Nothing Then
        '    sJscript2 = sJscript2 & ", max: " & CStr(Session("LastYear")) & vbCr
        'Else
        '    sJscript2 = sJscript2 & ", max: " & CStr(Session("FirstYear") + Session("RunningYear") - 1) & vbCr
        'End If
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        sJscript2 = sJscript2 & PlotColToJSSeries(gphIdx, sVar, Vals)

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

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.49;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.49;" & vbCr

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr

        sJscript2 = sJscript2 & "});" & vbCr

        PackJScritpBuilderContainter = sJscript2
    End Function

    Private Function PlotColToJSSeries(idx As Integer, sVar As String, Vals As Object) As String
        Dim sJscript As String = ""
        sJscript = sJscript & "{name: '" & sVar & "'," & vbCr

        Dim sColor As String = ""
        sColor = "Blue"
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr

        sJscript = sJscript & "data: ["

        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim Diff As Single, Min As Single, Max As Single
        Min = 10 ^ 10
        Max = -10 ^ 10
        Dim iNumDec As Integer = 0

        If IsNumeric(Vals(1)) Then
            If Math.Abs(Vals(1)) < 1 Then
                iNumDec = 3
            ElseIf Math.Abs(Vals(1)) < 10 Then
                iNumDec = 2
            ElseIf Math.Abs(Vals(1)) < 100 Then
                iNumDec = 1
            End If
        Else
            iNumDec = 2
        End If

        For j = 1 To Session("RunningYear")
            If IsNumeric(Vals(j - 1)) Then
                sJscript = sJscript & "[" & CStr(Session("FirstYear") + j - 1) & ", "
                sJscript = sJscript & CStr(Math.Round(Vals(j - 1), iNumDec)) & "]"
            End If

            If j < Session("RunningYear") Then
                sJscript = sJscript & ", "
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Protected Sub chkBilateralData_CheckedChanged(sender As Object, e As EventArgs) Handles chkBilateralData.CheckedChanged
        If chkBilateralData.Visible = False Then Exit Sub

        If chkBilateralData.Checked Then 'Add Bilateral Data
            If Dir(DataFileDir$ + "Bilateral\IFsForDyadic.db") = "" Then
                'if files have not been downloaded
                Dim msg As String
                msg = "Files DataDictDyadic.db, ifsHistSeriesDyadic.db, DataDictDyadicForecast.db and IFsForDyadic.db need to be located in the DATA folder in order to use this option. Please download them from <br><br>https://ifs02.du.edu/BilateralInteractionData/<br><br>"
                msg = msg & "You will need to restart the model, or make a copy of DATA\IFsForDyadic.mdb into RUNFILES\ifsForDyadicWork.mdb"

                LoadMessage(msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            Else
                File.Move(DataFileDir$ + "Bilateral\IFsForDyadic.db", DataFileDir$ + "IFsForDyadic.db")
                'jrs 2020/06/16
                'Consider the possibility that .mdb files have already been moved by vb6
                If Dir(DataFileDir$ + "IFsHistSeriesDyadic.*") = "" Then

                    'jrs 2022/12/20
                    If UseOnlySQLite Then
                        File.Move(DataFileDir$ + "Bilateral\IFsHistSeriesDyadic.db", DataFileDir$ + "IFsHistSeriesDyadic.db")
                        File.Move(DataFileDir$ + "Bilateral\DataDictDyadic.db", DataFileDir$ + "DataDictDyadic.db")
                        File.Move(DataFileDir$ + "Bilateral\DataDictDyadicForecast.db", DataFileDir$ + "DataDictDyadicForecast.db")

                        File.Copy(DataFileDir$ + "IFsHistSeriesDyadic.db", Session("PutFdrvRun") + "IFsHistSeriesDyadic.db")
                    Else
                        File.Move(DataFileDir$ + "Bilateral\IFsHistSeriesDyadic.mdb", DataFileDir$ + "IFsHistSeriesDyadic.mdb")
                        File.Move(DataFileDir$ + "Bilateral\DataDictDyadic.mdb", DataFileDir$ + "DataDictDyadic.mdb")
                        File.Move(DataFileDir$ + "Bilateral\DataDictDyadicForecast.mdb", DataFileDir$ + "DataDictDyadicForecast.mdb")

                        'jrs 2021/01/14
                        File.Move(DataFileDir$ + "Bilateral\IFsHistSeriesDyadic_FBIC.mdb", DataFileDir$ + "IFsHistSeriesDyadic_FBIC.mdb")

                        'jrs 2021/01/14 *** start ***
                        're-link tables
                        Dim strMsg As String

                        'Run the Procedure, getting any error messages.
                        strMsg = RefreshTableLinks()

                        'strMsg will be a zero-length string if there is no error message.
                        If Len(strMsg & "") = 0 Then
                            Debug.Print("All; Tables; were; successfully; relinked.")
                        Else
                            'Notify the user of the errors.
                            Session("Message") = strMsg
                            LoadMessage("Bilateral Interaction Display with Gravity", Session("Message"))
                            Support.FormManager.StateStore(Me)
                            Support.FormManager.Show(Me, "frm_Message.aspx")
                        End If
                        'jrs 2021/01/14 *** end ***

                        'jrs 2019/09/25
                        File.Copy(DataFileDir$ + "IFsHistSeriesDyadic.mdb", Session("PutFdrvRun") + "IFsHistSeriesDyadic.mdb")
                    End If
                End If

                File.Copy(DataFileDir$ + "IFsForDyadic.db", Session("PutFdrvRun") + "ifsForDyadicWork.db")
            End If
        Else 'Remove biltateral data
            File.Move(DataFileDir$ + "IFsForDyadic.db", DataFileDir$ + "Bilateral\IFsForDyadic.db")
            Kill(Session("PutFdrvRun") + "ifsForDyadicWork.db")
            'jam 2022/12/19
            'jrs 2019/09/25
            'jam 2022/12/19
            If Not UseOnlySQLite Then
                Kill(Session("PutFdrvRun") + "IFsHistSeriesDyadic.mdb")
                File.Move(DataFileDir$ + "IFsHistSeriesDyadic.mdb", DataFileDir$ + "Bilateral\IFsHistSeriesDyadic.mdb")
                File.Move(DataFileDir$ + "DataDictDyadic.mdb", DataFileDir$ + "Bilateral\DataDictDyadic.mdb")
                File.Move(DataFileDir$ + "DataDictDyadicForecast.mdb", DataFileDir$ + "Bilateral\DataDictDyadicForecast.mdb")
                'jrs 2021/01/14
                File.Move(DataFileDir$ + "IFsHistSeriesDyadic_FBIC.mdb", DataFileDir$ + "Bilateral\IFsHistSeriesDyadic_FBIC.mdb")
            Else
                Kill(Session("PutFdrvRun") + "IFsHistSeriesDyadic.db")
                File.Move(DataFileDir$ + "IFsHistSeriesDyadic.db", DataFileDir$ + "Bilateral\IFsHistSeriesDyadic.db")
                File.Move(DataFileDir$ + "DataDictDyadic.db", DataFileDir$ + "Bilateral\DataDictDyadic.db")
                File.Move(DataFileDir$ + "DataDictDyadicForecast.db", DataFileDir$ + "Bilateral\DataDictDyadicForecast.db")
            End If
        End If
    End Sub

    Private Sub cmd_GotoRun_Click(sender As Object, e As EventArgs) Handles cmd_GotoRun.Click
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Run.aspx")
    End Sub
End Class

