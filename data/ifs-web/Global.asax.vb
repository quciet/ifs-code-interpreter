Imports VB = Microsoft.VisualBasic
Imports IFs.Data

'JAM 2020-08-05
Imports Microsoft.AspNet.FriendlyUrls
'jrs 2022/07/20
Imports System.Data.SQLite
'jrs 2023/02/21
Imports System.Globalization
Imports System.Threading
Imports NLog
Imports System.Reflection
'jam 2025/05/28
Imports System.Net.Mail
'jam 2025/06/13
Imports System.Diagnostics
Imports System.IO
'jam 2026/05/15 - Required for per-IP concurrency limiter
Imports System.Collections.Concurrent
Imports System.Configuration

Public Class Global_asax
    Inherits HttpApplication
    'jam 2026/04/03 It's better a lower limit for memory 2.8 GB 
    Const MemoryThresholdBytes As Long = 3_006_477_107L  ' 2.8 GB 
#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Private Shared _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

    'jam 2026/04/09 Semaphore for concurrent sessions in IFSInit, Initial value is about 20 sessions
    ' Right value is sesiones_max = (RAM_available_MB) / (MB_per_IFSinit)
    Private Shared _initSemaphore As New System.Threading.SemaphoreSlim(20, 20)

    'jam 2026/05/15 Per-IP concurrency limiter: max N simultaneous requests per client IP
	'jam 2026/06/21 Switched from SemaphoreSlim to IpSlotTracker — self-healing against stuck slots.
    ' Previous SemaphoreSlim could leak slots if a request aborted mid-pipeline (unstable connections,
    ' antivirus, corporate proxies). Tracker auto-resets stale entries.
    Private Shared ReadOnly _ipSlots As New ConcurrentDictionary(Of String, IpSlotTracker)()
    'jam 2026/05/15 Read from Web.config with safe defaults
    'jam 2026/06/21 Default raised from 10 to 30 (real IFs requests use many parallel AJAX/aspx fragments)
    Private Shared ReadOnly MAX_CONNECTIONS_PER_IP As Integer = GetAppSettingInt("MaxConnectionsPerIP", 30)
    'jam 2026/06/21 Timeout lowered from 30000 to 5000 ms — fail fast, no need to wait long
    Private Shared ReadOnly IP_ACQUIRE_TIMEOUT_MS As Integer = GetAppSettingInt("IpAcquireTimeoutMs", 5000)
    'jam 2026/06/21 If a tracker has not seen activity in this many seconds, force-reset on next access
    Private Shared ReadOnly IP_SLOT_STALE_SECONDS As Integer = GetAppSettingInt("IpSlotStaleSeconds", 60)
    'jam 2026/05/15 Keys used to track per-request state in HttpContext.Items
    Private Const HTTP_CTX_IP_ACQUIRED As String = "vifs_IpSemAcquired"
    Private Const HTTP_CTX_IP_VALUE As String = "vifs_ClientIp"
    Private Const HTTP_CTX_IP_RELEASED As String = "vifs_IpSemReleased"
    'jam 2026/05/15 Timer reference kept alive for the lifetime of the AppDomain
    Private Shared _ipCleanupTimer As System.Threading.Timer
    'jam 2026/05/15 Cleanup interval in minutes (configurable via Web.config, default 60)
    Private Shared ReadOnly IP_CLEANUP_INTERVAL_MIN As Integer = GetAppSettingInt("IpCleanupIntervalMin", 60)

    'jam 2026/06/21 Self-healing per-IP slot tracker. Replaces SemaphoreSlim with a counter that
    ' tracks last-activity timestamp. If slots are held but no activity for IP_SLOT_STALE_SECONDS,
    ' they are assumed leaked (aborted requests) and reset automatically on next access.
    Private Class IpSlotTracker
        Public ActiveCount As Integer
        Public LastActivityUtc As DateTime
        Public ReadOnly LockObj As New Object()

        Public Sub New()
            ActiveCount = 0
            LastActivityUtc = DateTime.UtcNow
        End Sub
    End Class
	
	
	Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        Dim t As Type = GetType(Global_asax)
        Dim assemFromType As Assembly = t.Assembly

        _logger.Info($"Application started - {assemFromType.GetName().Version.ToString()}")
        _logger.Info($"Application directory - {System.AppContext.BaseDirectory}")
        _logger.Info($"Application cultureInfo - {CultureInfo.CurrentCulture.EnglishName} - {CultureInfo.CurrentCulture.Name}")
        _logger.Info("------------------------------------------------------------------------------")

        'JAM 2020-08-05
        RouteTable.Routes.EnableFriendlyUrls
        'jrs 2023/02/21
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        'SQLitePCL.Batteries_V2.Init()
        'jam 2025/08/19
        If Not StandAloneMode Then
            'jam 2025/08/10 Memory Health when the application starts
            MonitorMemoryHealth("Application_Start")
        End If

        'jam 2026/05/15 Start periodic cleanup of idle per-IP semaphores
        Try
            Dim interval As TimeSpan = TimeSpan.FromMinutes(IP_CLEANUP_INTERVAL_MIN)
            _ipCleanupTimer = New System.Threading.Timer(
                AddressOf CleanupIdleIpSemaphores, Nothing, interval, interval)
            _logger.Info($"Per-IP throttle cleanup timer started (interval: {IP_CLEANUP_INTERVAL_MIN} min)")
        Catch ex As Exception
            _logger.Error(ex, "Failed to start per-IP cleanup timer")
        End Try
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        'jam 2026/05/15 Circuit breaker: reject new sessions if memory is critically high.
        ' Threshold is 90% of MemoryThresholdBytes (default ~2.5 GB), configurable via Web.config.
        ' This prevents the cascade pattern where OOM in one session causes 658 cascading failures.
        Try
            Dim circuitPct As Integer = GetAppSettingInt("MemoryCircuitBreakerPct", 90)
            Dim circuitThreshold As Long = CLng(MemoryThresholdBytes * circuitPct / 100L)
            Dim procMem As Process = Process.GetCurrentProcess()
            procMem.Refresh()
            If procMem.PrivateMemorySize64 > circuitThreshold Then
                Dim mbUsed As Long = procMem.PrivateMemorySize64 \ (1024L * 1024L)
                Dim mbLimit As Long = circuitThreshold \ (1024L * 1024L)
                _logger.Warn($"Session_Start CIRCUIT BREAKER tripped: {mbUsed} MB > {mbLimit} MB threshold. Rejecting new session {Session.SessionID}.")
                Try
                    Session.Abandon()
                Catch
                End Try
                If HttpContext.Current IsNot Nothing AndAlso HttpContext.Current.Response IsNot Nothing Then
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 503
                    HttpContext.Current.Response.StatusDescription = "Service Temporarily Unavailable"
                    HttpContext.Current.Response.AddHeader("Retry-After", "60")
                    HttpContext.Current.Response.ContentType = "text/html"
                    HttpContext.Current.Response.Write(
                        "<html><body style='font-family:sans-serif;padding:40px;text-align:center'>" &
                        "<h2>Service Temporarily Unavailable</h2>" &
                        "<p>The server is at capacity. Please try again in a minute.</p>" &
                        "</body></html>")
                    HttpContext.Current.Response.End()
                End If
                Return
            End If
        Catch thEx As System.Threading.ThreadAbortException
            'jam 2026/05/15 Response.End() throws this — rethrow to keep pipeline correct
            Throw
        Catch ex As Exception
            _logger.Error(ex, "Error in Session_Start circuit breaker — continuing with session start")
        End Try

        _logger.Info($"Session ({Session.SessionID}) started")
        'jrs 2011/04/28
        'check if not restarting from timeout
        Session("bTimeOut") = isTimeOut()
        'jrs 2012/08/14
        If Session("bTimeOut") = True Then
            Session("TimeOutTime") = DateTime.Now
        End If

        ' Fires when the session is started
        'JRS 06/05/04 MULTISESSION
        Dim i As Integer
        'SessionList(0) = False

        Session("SessionIdx") = -1
        'jrs 2005/01/27
        Session("RuningProgress") = False

        'jrs 2004/11/23
        'For i = 0 To MaxFullSessions - 1
        '    If Not SessionList(i) Then
        '        SessionList(i) = True
        '        Session("SessionIdx") = i
        '        Exit For
        '    End If
        'Next

        'mti WebForm Changes to be taken by Jose - START
        Dim VarGroupNames(100) As String
        Dim FilledVar(4) As Object
        Dim SCVarI(200, 4) As Object
        Dim SCVarD(200) As Object
        Dim NewVal(200) As Object

        'longitudinal
        'jrs 2007/05/01 changed to 250 for provinces
        Dim CountryList(250) As String

        Session("Scat_VarGroupNames") = VarGroupNames
        Session("Scat_FilledVar") = FilledVar
        Session("Scat_SCVarI") = SCVarI
        Session("Scat_SCVarD") = SCVarD
        Session("Scat_NewVal") = NewVal

        Session("CountryList") = CountryList
        'jrs 2008/05/15
        ReDim Session("CountryListGroup")(250) '0 country, 1 group, 2 province

        'mti August 2004 3 - start
        Dim CVarI(200) As Object
        Session("CVarI") = CVarI
        Dim CVarD(200) As Object
        Session("CVarD") = CVarD
        Dim CVarC(200) As Object
        Session("CVarC") = CVarC

        Dim CVarName(200) As Object
        Session("CVarName") = CVarName

        Dim VarNameI(5) As String
        Session("VarNameI") = VarNameI
        'mti August 2004 3 -end

        'mti WebForm Changes to be taken by Jose -End 

        ''mti WebForm Changes From June 21, 2004 -start
        'Dim TempDatabase(NCohorts, 2) As Single 'has to be session var
        'Dim CountryGroup(200) As String
        'Dim RegionIdGroup(200) As Short

        'Session("PC_TempDataBase") = TempDatabase  '(NCohorts, 2)
        'Session("CountryGroup") = CountryGroup
        'Session("RegionIdGroup") = RegionIdGroup


        'jam 2026/04/09 Preventive Garbage Collector GC before loading session under memory pressure
        ' If the process is higher, only the available memory is cleaning

        Try
            Dim procMem As Process = Process.GetCurrentProcess()
            procMem.Refresh()
            Dim bytesBefore As Long = procMem.PrivateMemorySize64
            Dim mbBefore As Long = bytesBefore \ (1024L * 1024L)
            If bytesBefore > MemoryThresholdBytes Then
                _logger.Warn($"Session_Start ({Session.SessionID}) — Higher Memory ({mbBefore} MB) before IFsLoad, Garbage Collector is forced")
                GC.Collect(2, GCCollectionMode.Forced, blocking:=True)
                GC.WaitForPendingFinalizers()
            End If
        Catch ex As Exception
            _logger.Error(ex, "Session_Start - Error in Garbage Collector")
        End Try
        '''''''''''''''''''''''''



        ''mti WebForm Changes From June 21, 2004 -end

        'jrs 2011/03/24
        IFsLoad()

        _logger.Debug("Session_Start - IFsLoad done")

        'jrs 2011/04/25
        Dim s As String
        s = Request.Url.AbsoluteUri
        If InStr(s, "AnyChartDataforPE") > 0 Then
            Session("PE_CommonMenu") = "Display"
            Dim s2 As String, pos As Integer, pos2 As Integer, pos3 As Integer
            s2 = s 'jrs 2011/10/20 Request.UrlReferrer.AbsoluteUri
            s2 = Replace(s2, "%20", " ")
            s2 = Replace(s2, "%22", """")
            'jrs 2012/01/12
            s2 = Replace(s2, "%26", "&")
            s2 = Replace(s2, "%5C", "")
            'Read querystring
            Dim sListNames As String, sHistFor As String, GrpOp As Integer
            pos = InStr(s2, "ListNames") + 10
            pos2 = InStr(pos, s2, "&")
            If pos > 10 And pos2 > 0 Then
                sListNames = Mid(s2, pos, pos2 - pos)
            End If
            Session("ListLoaded") = sListNames
            pos = InStr(pos2, s2, "HistFor") + 8
            pos2 = InStr(pos, s2, "&")
            If pos > 10 And pos2 > 0 Then
                sHistFor = Mid(s2, pos, pos2 - pos)
            End If
            pos = InStr(pos2, s2, "GrpOp") + 6
            pos2 = InStr(pos, s2, "&")
            If pos > 10 And pos2 > 0 Then
                GrpOp = CInt(Mid(s2, pos, pos2 - pos))
            End If
            Dim sDim1 As String, sDim2 As String, sDim3 As String, sFiles As String
            pos = InStr(pos2, s2, "Dim1") + 5
            pos2 = InStr(pos, s2, "&")
            pos3 = InStr(pos, s2, "-")
            If pos > 10 And pos2 > 0 Then
                'jrs 2012/01/12
                If IsNumeric(Mid(s2, pos, pos2 - pos)) Or (pos3 > 0 And pos3 < pos2) Then
                    sDim1 = Mid(s2, pos, pos2 - pos)
                Else
                    sDim1 = """" & Mid(s2, pos, pos2 - pos) & """"
                End If
            End If
            pos = InStr(pos2, s2, "Dim2") + 5
            pos2 = InStr(pos, s2, "&")
            If pos > 10 And pos2 > 0 Then
                sDim2 = Mid(s2, pos, pos2 - pos)
            End If
            pos = InStr(pos2, s2, "Dim3") + 5
            pos2 = InStr(pos, s2, "&")
            If pos > 10 And pos2 > 0 Then
                sDim3 = Mid(s2, pos, pos2 - pos)
            End If
            pos = InStr(pos2, s2, "File") + 5
            pos2 = InStr(pos, s2, "&")
            'jrs 2012/01/12
            'If pos > 10 And pos2 > 0 Then
            '    sFiles = Mid(s2, pos, pos2 - pos)
            'End If
            If pos > 10 Then
                sFiles = Mid(s2, pos)
            End If

            Dim rs As DataTable
            rs = getVarLists(sListNames, sHistFor, Session("PutFdrvRun"))
            Session("PackagedDispRs") = rs
            Dim p As Integer
            'jrs 2012/04/24
            'p = InStr(2, sListNames, "'")
            'If p > 1 Then
            '    Session("TableTitle") = Mid(sListNames, 2, p - 2)
            'Else
            '    Session("TableTitle") = sListNames
            'End If
            p = InStr(sListNames, """,""")
            If p > 0 Then
                'Multiple lists
                Session("TableTitle") = sListNames
            Else
                'Single list
                Session("TableTitle") = Mid(sListNames, 2, Len(sListNames) - 2)
            End If

            Session("GType") = "LINE"
            Dim IfsPars As New myParameters
            IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))
            'jrs 2011/08/25 Session("ProEssentialParent") = "frm_PackagedDisplaysFlex.aspx"
            Session("GraphDisplayParent") = "frm_PackagedDisplaysFlex.aspx"
            'Load list boxes
            Dim lstDim As New ListBox, lstDim2 As New ListBox, lstDim3 As New ListBox
            Dim lstFiles As New ListBox
            lstDim.ID = "lstDim"
            lstDim2.ID = "lstDim2"
            lstDim3.ID = "lstDim3"
            LoadAllDims(lstDim, Session("AllDimsIdx"))
            LoadAllDims(lstDim2, Session("AllDimsIdx"))
            LoadAllDims(lstDim3, Session("AllDimsIdx"))

            If sDim1 = "" Then
                sDim1 = Trim(CStr(lstDim.Items.Count - 1))
            End If
            If sDim2 = "" Then
                sDim2 = Trim(CStr(lstDim2.Items.Count - 1))
            End If
            If sDim3 = "" Then
                sDim3 = Trim(CStr(lstDim3.Items.Count - 1))
            End If

            ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
            ReDim Preserve Session("DimIdxs2")(lstDim2.Items.Count - 1)
            ReDim Preserve Session("DimIdxs3")(lstDim3.Items.Count - 1)

            LoadFiles(Session("VarSelCheck"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("ActiveBase"), Session("VarSelCheckHis"), Session("PutFdrvRun2"), Session("WorkingFile"),
            Session("FilesSelected"), Session("IFsRunFiles"), Session("RunFiles"), Session("ShortSce"), lstFiles)
            If sFiles > "" Then
                lstFiles.Items(0).Selected = False
            End If

            ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)

            'Set Region options
            Select Case GrpOp
                Case 0 'Regions
                    Session("bUseReg") = True
                    Session("bUseGroup") = False
                    Session("bUseGroupDec") = False
                    Session("bUseGList") = False
                    Session("bUseCtryAndGroup") = False
                    Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                Case 1 'Groups
                    Session("bUseReg") = False
                    Session("bUseGroup") = True
                    Session("bUseGroupDec") = False
                    Session("bUseGList") = False
                    Session("bUseCtryAndGroup") = False
                    Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                Case 2 'Dec Groups
                    Session("bUseReg") = False
                    Session("bUseGroup") = False
                    Session("bUseGroupDec") = True
                    Session("bUseGList") = False
                    Session("bUseCtryAndGroup") = False
                    Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                Case 3 'G-Lists
                    Session("bUseReg") = False
                    Session("bUseGroup") = False
                    Session("bUseGroupDec") = False
                    Session("bUseGList") = True
                    Session("bUseCtryAndGroup") = False
                    Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                Case 4 'Country and Groups
                    Session("bUseReg") = False
                    Session("bUseGroup") = False
                    Session("bUseGroupDec") = False
                    Session("bUseGList") = False
                    Session("bUseCtryAndGroup") = True
                    Session("bUseCtryAndGroupDec") = False 'jrs 2025/05/30
                Case 5 'Country and Dec Groups 'jrs 2025/05/30
                    Session("bUseReg") = False
                    Session("bUseGroup") = False
                    Session("bUseGroupDec") = False
                    Session("bUseGList") = False
                    Session("bUseCtryAndGroup") = False
                    Session("bUseCtryAndGroupDec") = True
            End Select

            'Set List Boxes
            URLToDimLst(sDim1, lstDim)
            URLToDimLst(sDim2, lstDim2)
            URLToDimLst(sDim3, lstDim3)
            URLToFileLst(sFiles, lstFiles, "", Session("IFsRunFiles"), Session("PDFilesIdxs"), Session("FilesSelected"),
            Session("RunFiles"), Session("PutFdrvRun2"), Session("ShortSce"), Session("ScenarioFileDir"))

            'Load Package
            'JAM 2020-07-18
            'gLoadPackage(rs, Session("COUNTE"), Nothing, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            'Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, Session("TableTitle"), Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
            'Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
            'Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
            'Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
            'Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
            'Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
            'Session("PopMales"), Session("PopFemales"), sListNames)

            gLoadPackage(rs, Session("COUNTE"), Nothing, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, Session("TableTitle"), Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
            Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("bUseCtryAndGroupDec"), Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
            Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
            Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
            Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
            Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
            Session("PopMales"), Session("PopFemales"), sListNames, Session("sUsrDefCats"), vmnuSQLITE:=("mnuSQLite"))



            'jrs 2011/06/16
            'save original name to display
            ReDim Session("oVARN0")(Session("COUNTE"))
            ReDim Session("RestorePos")(Session("COUNTE"))
            For i = 1 To Session("COUNTE")
                Session("oVARN0")(i) = Session("VARN")(0, i)
                'jrs 2010/11/01
                Session("RestorePos")(i) = i
            Next i
            ReDim Session("oGR1N")(Session("COUNTE"))
            For i = 1 To Session("COUNTE")
                Session("oGR1N")(i) = Session("GR1N")(i)
            Next i

            'jrs 2011/04/01
            pos = InStr(Session("VARN")(1, 1), "[")
            If pos > 0 Then
                Dim sVar As String
                sVar = Left(Session("VARN")(1, 1), pos - 1)
                If IfsVars.Item(sVar).Index > 0 Then
                    Session("MMVarName") = sVar
                    Session("MMDim2") = Session("GR2N")(1)
                    Session("MMDim3") = Session("GR3N")(1)
                End If
            End If

            'jrs 2011/07/04
            Session("bAlias") = False
            Session("PE_CommonMenu") = "Display"
            Session("PE_CommonPlot") = "False"
            Session("Interval") = 1

            Dim MinYear As Short
            Dim MaxYear As Short
            Dim ii As Short
            '   Set number of points, interval
            '   ------------------------------

            MaxYear = 1
            MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            For ii = 1 To Session("COUNTE")
                If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
                If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
            Next ii

            If Session("Interval") > MaxYear Then
                Session("Interval") = MaxYear - 1
            End If

            If Session("DisplayTime") = 0 Then
                Session("DisYear") = MinYear
            Else
                'jrs 2007/11/30
                If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                    Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
                Else
                    Session("DisYear") = MaxYear
                End If
            End If

        ElseIf InStr(s, "AnyChartDataforLorenz") > 0 Then 'entering from Lorenz Curve
            Dim s2 As String, pos As Integer, pos2 As Integer
            s2 = s 'jrs 2011/10/20 Request.UrlReferrer.AbsoluteUri
            's2 = Request.RawUrl
            s2 = Replace(s2, "%20", " ")
            s2 = Replace(s2, "%22", """")
            'Read querystring
            Dim sVarName As String, idx As Integer, DefaultFile As Integer
            Dim sDim2 As String = "", sDim3 As String = "", idxDim2 As Integer = -1, idxDim3 As Integer = -1
            pos = InStr(s2, "VarName") + 8
            pos2 = InStr(pos, s2, "&")
            If pos > 8 And pos2 > 0 Then
                sVarName = Mid(s2, pos, pos2 - pos)
            End If
            pos = InStr(pos2, s2, "File") + 5
            pos2 = InStr(pos, s2, "&")
            If pos > 13 And pos2 = 0 Then
                DefaultFile = CInt(Mid(s2, pos))
            Else
                DefaultFile = CInt(Mid(s2, pos, pos2 - pos))
                pos = InStr(pos2, s2, "Dim2") + 5
                pos2 = InStr(pos, s2, "&")
                If pos > 18 And pos2 = 0 Then
                    sDim2 = Mid(s2, pos)
                Else
                    sDim2 = Mid(s2, pos, pos2 - pos)
                    pos = InStr(pos2, s2, "Dim3") + 5
                    pos2 = InStr(pos, s2, "&")
                    If pos > 23 And pos2 > 0 Then
                        sDim3 = Mid(s2, pos, pos2 - pos)
                    End If
                End If
            End If
            Session("DefaultFile") = DefaultFile

            'Load Data
            Dim DimNum As Integer
            Dim RunCount As Integer
            Dim ii As Integer
            DimNum = 1
            RunCount = MaxVal(DimNum) - 1

            Dim MyFileforDisplay As IStorage
            Dim MyDF As IDataframe

            For ii = 1 To RunCount
                Session("COUNTE") = Session("COUNTE") + 1
                gLoadForDisplayNET(sVarName, idx, GRPS(1, ii), ii, sDim2, idxDim2, sDim3, idxDim3, "", DefaultFile, ii = 1, ii = RunCount, False, 1,
                Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Nothing, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                'gLoadForDisplay(sVarName, idx, GRPS(1, ii), ii, sDim2, idxDim2, sDim3, idxDim3, "", DefaultFile, ii = 1, ii = RunCount, False, 1,
                'Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                'Session("bLoadForDisplaySuccess"), Nothing, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                gLoadForDisplaySaveLbls(Session("COUNTE"), idx, GRPS(1, ii), "", "", False, DefaultFile, "",
                Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
            Next ii
            Session("WorldVaropt") = "optLorenz"

            'jrs 2006/09/25
            Dim IfsPars As New myParameters
            IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

            GiniSetup(IfsPars, Session("COUNTE"), Nothing, Session("VARN"), Session("LastYear"), Session("FirstYear"),
            Session("PutFdrvRun"), Session("ActiveBase"), Session("RunFiles"), Session("bLoadForDisplaySuccess"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"),
            Session("CurrencyVal"), Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis"),
            Session("LastYearForecast"), Session("bComputation"), Session("bFillHisOnlyHoles"),
            Session("LorenzX_Year"), Session("LorenzY_Year"), Session("PLOT"), Session("LorenzX"),
            Session("LorenzY"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("YAxisLabel"),
            Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))

            Session("LorenzParent") = "frm_WorldVariables.aspx"
        End If 'if entering from Proessentials elseif from Lorenz

        'jrs 2021/02/10 added False to deactivate Timeout message
        'jrs 2015/08/26 added And InStr(s, "frm_CyberDashboard.aspx") = 0
        'jrs 2011/04/29
        If False And Session("bTimeOut") And InStr(s, "frm_MainMenu.aspx") = 0 And InStr(s, "frm_CyberDashboard.aspx") = 0 And InStr(s, "=") = 0 Then
            Session("Error") = "There has not been any activity for more than " & Context.Session.Timeout & " minutes, thus your session has expired."
            'jrs 2015/08/25
            If InStr(Request.Url.AbsoluteUri, "frm_CyberDashboard") > 0 Then
                Session("bError4CyberDashboard") = True
            Else
                Session("bError4CyberDashboard") = False
            End If
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")
        End If
        _logger.Debug("Session_Start - end")

    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        ' only execute when the loglevel is set to be logger, otherwise this whole operation is useless

        'jam 2026/05/15 Enforce max N concurrent requests per client IP (runs BEFORE existing logging logic)
		'jam 2026/06/21 Self-healing logic: stale trackers (no activity in IP_SLOT_STALE_SECONDS) auto-reset
        Try
            If ShouldThrottle(HttpContext.Current) Then
                Dim clientIp As String = GetClientIp(HttpContext.Current)
                HttpContext.Current.Items(HTTP_CTX_IP_VALUE) = clientIp

                Dim tracker As IpSlotTracker = _ipSlots.GetOrAdd(
                    clientIp,
                    Function(k) New IpSlotTracker())

                Dim accepted As Boolean = False
                Dim activeAtReject As Integer = 0
                SyncLock tracker.LockObj
                    Dim staleSeconds As Double = (DateTime.UtcNow - tracker.LastActivityUtc).TotalSeconds
                    If staleSeconds > IP_SLOT_STALE_SECONDS AndAlso tracker.ActiveCount > 0 Then
                        'jam 2026/06/21 Self-heal: stale slots detected → reset before evaluating limit
                        _logger.Warn($"Per-IP throttle: resetting stale tracker for {clientIp} (was {tracker.ActiveCount} active, idle {CInt(staleSeconds)}s)")
                        tracker.ActiveCount = 0
                    End If

                    If tracker.ActiveCount < MAX_CONNECTIONS_PER_IP Then
                        tracker.ActiveCount += 1
                        tracker.LastActivityUtc = DateTime.UtcNow
                        accepted = True
                    Else
                        activeAtReject = tracker.ActiveCount
                    End If
                End SyncLock

                If accepted Then
                    HttpContext.Current.Items(HTTP_CTX_IP_ACQUIRED) = True
                Else
                    'jam 2026/05/15 Reject with HTTP 429 when IP exceeds its slot quota
                    _logger.Warn($"Per-IP throttle: rejecting request from {clientIp} (active={activeAtReject}, limit={MAX_CONNECTIONS_PER_IP})")
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 429
                    HttpContext.Current.Response.StatusDescription = "Too Many Requests"
                    HttpContext.Current.Response.AddHeader("Retry-After", "30")
                    HttpContext.Current.Response.ContentType = "text/plain"
                    HttpContext.Current.Response.Write("Too many concurrent requests from your IP. Please retry shortly.")
                    HttpContext.Current.Response.SuppressContent = False
                    HttpContext.Current.Response.End()
                End If
            End If
        Catch thEx As System.Threading.ThreadAbortException
            'jam 2026/05/15 Response.End() throws this — rethrow to keep ASP.NET pipeline correctness
            Throw
        Catch exThrottle As Exception
            'jam 2026/05/15 Never break the pipeline due to a throttle bug — log and continue
            _logger.Error(exThrottle, "Per-IP throttle error in Application_BeginRequest")
        End Try

        Try
            'jam 2025/08/21 Commented for a new properly tracking
            'jam 2025/08/19
            'If Not StandAloneMode Then
            '    'jam 2025/08/10 Memory monitoring with each request
            '    MonitorMemoryHealth("BeginRequest")
            'End If
            If _logger.IsTraceEnabled Then

                Dim qry As NameValueCollection = sender.Request.QueryString
                Dim queryString As New StringBuilder
                For Each q In qry.AllKeys
                    queryString.Append($"[{q}|{HttpUtility.UrlEncode(qry(q))}]")
                Next q

                Dim frm As NameValueCollection = sender.Request.Form
                Dim frmString As New StringBuilder
                For Each f In frm.AllKeys
                    'jrs 2024/04/25
                    If Not (f Is Nothing) Then
                        ' we do not want all webform system parameters in the log
                        ' allow if you need them 
                        If Not f.StartsWith("__") Then
                            frmString.Append($"[{f}|{HttpUtility.UrlEncode(frm(f))}]")
                        End If
                    End If

                Next f

                _logger.Trace($"{sender.Request.HttpMethod} - {sender.Request.FilePath} - query:({queryString}) - form:({frmString})")
            End If



            ' catch and log all errors - we do not want the application to fail in case there is a logging error
        Catch ex As Exception
            _logger.Error(ex, "Unexpected error in Application_BeginRequest logging handler")
        End Try

    End Sub
	
	'jam 2026/05/15 Release per-IP semaphore slot at end of every request
    Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim ctx As HttpContext = HttpContext.Current
            If ctx Is Nothing Then Return

            Dim acquiredObj As Object = ctx.Items(HTTP_CTX_IP_ACQUIRED)
            If acquiredObj Is Nothing OrElse Not CBool(acquiredObj) Then Return

            'jam 2026/05/15 Guard against double-release if Application_Error already released
            Dim releasedObj As Object = ctx.Items(HTTP_CTX_IP_RELEASED)
            If releasedObj IsNot Nothing AndAlso CBool(releasedObj) Then Return

            Dim clientIp As String = TryCast(ctx.Items(HTTP_CTX_IP_VALUE), String)
            If String.IsNullOrEmpty(clientIp) Then Return

            'jam 2026/06/21 Decrement tracker counter (was SemaphoreSlim.Release)
            Dim tracker As IpSlotTracker = Nothing
            If _ipSlots.TryGetValue(clientIp, tracker) Then
                SyncLock tracker.LockObj
                    If tracker.ActiveCount > 0 Then
                        tracker.ActiveCount -= 1
                    End If
                    tracker.LastActivityUtc = DateTime.UtcNow
                End SyncLock
                ctx.Items(HTTP_CTX_IP_RELEASED) = True
            End If
        Catch ex As Exception
            'jam 2026/05/15 Swallow on purpose: never break response pipeline on cleanup
            _logger.Error(ex, "Per-IP throttle release error in Application_EndRequest")
        End Try
    End Sub

	
	

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub
    'jam 2026/04/03 Improving Application error management
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        'jam 2026/05/15 Defensive release of per-IP slot in case the pipeline skips EndRequest
       'jam 2026/06/21 Updated to use IpSlotTracker instead of SemaphoreSlim
        Try
            Dim ctxErr As HttpContext = HttpContext.Current
            If ctxErr IsNot Nothing Then
                Dim acquiredObj As Object = ctxErr.Items(HTTP_CTX_IP_ACQUIRED)
                Dim releasedObj As Object = ctxErr.Items(HTTP_CTX_IP_RELEASED)
                If acquiredObj IsNot Nothing AndAlso CBool(acquiredObj) AndAlso
                   (releasedObj Is Nothing OrElse Not CBool(releasedObj)) Then
                    Dim ipErr As String = TryCast(ctxErr.Items(HTTP_CTX_IP_VALUE), String)
                    Dim trackerErr As IpSlotTracker = Nothing
                    If Not String.IsNullOrEmpty(ipErr) AndAlso _ipSlots.TryGetValue(ipErr, trackerErr) Then
                        SyncLock trackerErr.LockObj
                            If trackerErr.ActiveCount > 0 Then
                                trackerErr.ActiveCount -= 1
                            End If
                            trackerErr.LastActivityUtc = DateTime.UtcNow
                        End SyncLock
                        ctxErr.Items(HTTP_CTX_IP_RELEASED) = True
                    End If
                End If
            End If
        Catch
            'jam 2026/05/15 Suppress — never break the error pipeline
        End Try

        ' Fires when an error occurs
        'jrs 10/05/04
        ' Get error.
        Dim ex As Exception = Server.GetLastError()

        'jam 2026/04/03 Unwrap HttpUnhandledException para exponer la excepción real
        If TypeOf ex Is HttpUnhandledException AndAlso ex.InnerException IsNot Nothing Then
            ex = ex.InnerException
        End If

        'jam 2026/04/03 Detects Out of Memory in every level
        Dim isOOM As Boolean = TypeOf ex Is OutOfMemoryException OrElse
                           TypeOf ex.GetBaseException() Is OutOfMemoryException

        'jam 2025/05/28
        ''If TypeOf ex Is OutOfMemoryException Then
        ''    ' Log the error
        ''    System.Diagnostics.EventLog.WriteEntry("Application", "Out of Memory Error: " & ex.Message, System.Diagnostics.EventLogEntryType.Error)

        ''    ' Send Email Notification
        ''    SendEmail("Out of Memory error detected!", ex.Message)

        ''    'jam 2025/06/13
        ''    LogExtendedSystemUsage()

        ''End If

        _logger.Error(ex, "An unexpected application error has occured")

        'jam 2026/04/03 Another extra file calling is canceled
        If isOOM Then
            GC.Collect()
            GC.WaitForPendingFinalizers()
            Try
                Session("Error") = "The application ran out of memory. Please try again in a few moments."
            Catch
            End Try
            'jrs 2005/02/11
            Try
                If Session("SessionIdx") > -1 Then
                    SyncLock SessionList
                        SessionList(Session("SessionIdx")) = False
                    End SyncLock
                    If Session("RuningProgress") = True Then
                        SyncLock RunControl1
                            If Not RunControl1.RunsZero Then
                                RunControl1.DecreaseCounter()
                                Session("RuningProgress") = False
                            End If
                        End SyncLock
                    End If
                End If
            Catch

            End Try
            'jrs 2005/01/27
            Session("SessionIdx") = -1
            ' Clear error.
            Server.ClearError()
            ' Transfer back to this page.
            Server.Transfer("ErrorHandler.aspx")
            Exit Sub
        End If

        'jrs 2012/08/14
        Dim elapsed_time As TimeSpan
        Dim stop_time As DateTime
        stop_time = DateTime.Now
        If Session("bTimeOut") Then
            elapsed_time = stop_time.Subtract(CType(Session("TimeOutTime"), DateTime))
        Else
            elapsed_time = stop_time.Subtract(stop_time)
        End If
        'jrs 2021/02/10 added False to deactivate TimeOut error temporarily
        'jrs 2005/01/08 
        'if it is a time out
        If False And Session("bTimeOut") And elapsed_time.TotalSeconds < 5 Then
            Session("Error") = "There has not been any activity for more than " & Context.Session.Timeout & " minutes, thus your session has expired."
        Else 'if it is not a time out
            ' Store the error message.
            'jam 2026/04/03 InnerException is protected with null-check (avoids NullReferenceException)
            Try
                Dim innerMsg As String = If(ex.InnerException IsNot Nothing, ex.InnerException.ToString(), "(no inner exception)")
                Session("Error") = ex.Message() & "<br>" & innerMsg
            Catch
                Session("Error") = ex.Message()
            End Try
            'jrs 2005/01/16
            Dim errLogger As ExceptionLogger
            Dim exceptionID As Integer
            'jam 2026/04/03 Protecting DB opening witg Try 
            Try
                Dim innerEx As Exception = If(ex.InnerException IsNot Nothing, ex.InnerException, ex)
                errLogger = New ExceptionLogger(DataFileDir & "ExceptionLog.mdb", innerEx, CStr(Context.Request.UserHostAddress))
                exceptionID = errLogger.LogException()
            Catch logEx As Exception
                _logger.Error(logEx, "Error logging exception to ExceptionLog.mdb")
            End Try

        End If

        'jrs 2015/08/25
        If InStr(Context.Request.UserHostAddress, "frm_CyberDashboard") > 0 Then
            Session("bError4CyberDashboard") = True
        Else
            Session("bError4CyberDashboard") = False
        End If

        'jrs 2005/02/11
        If Session("SessionIdx") > -1 Then
            SyncLock SessionList
                SessionList(Session("SessionIdx")) = False
            End SyncLock
            If Session("RuningProgress") = True Then
                SyncLock RunControl1
                    If Not RunControl1.RunsZero Then
                        RunControl1.DecreaseCounter()
                        Session("RuningProgress") = False
                    End If
                End SyncLock
            End If
        End If
        'jrs 2005/01/27
        Session("SessionIdx") = -1
        ' Clear error.
        Server.ClearError()
        ' Transfer back to this page.
        Server.Transfer("ErrorHandler.aspx")

    End Sub


    'Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Fires when an error occurs
    '    'jrs 10/05/04
    '    ' Get error.
    '    Dim ex As Exception = Server.GetLastError()

    '    'jam 2025/05/28
    '    ''If TypeOf ex Is OutOfMemoryException Then
    '    ''    ' Log the error
    '    ''    System.Diagnostics.EventLog.WriteEntry("Application", "Out of Memory Error: " & ex.Message, System.Diagnostics.EventLogEntryType.Error)

    '    ''    ' Send Email Notification
    '    ''    SendEmail("Out of Memory error detected!", ex.Message)

    '    ''    'jam 2025/06/13
    '    ''    LogExtendedSystemUsage()

    '    ''End If


    '    _logger.Error(ex, "An unexpected application error has occured")

    '    'jrs 2012/08/14
    '    Dim elapsed_time As TimeSpan
    '    Dim stop_time As DateTime
    '    stop_time = DateTime.Now
    '    If Session("bTimeOut") Then
    '        elapsed_time = stop_time.Subtract(CType(Session("TimeOutTime"), DateTime))
    '    Else
    '        elapsed_time = stop_time.Subtract(stop_time)
    '    End If
    '    'jrs 2021/02/10 added False to deactivate TimeOut error temporarily
    '    'jrs 2005/01/08 
    '    'if it is a time out
    '    If False And Session("bTimeOut") And elapsed_time.TotalSeconds < 5 Then
    '        Session("Error") = "There has not been any activity for more than " & Context.Session.Timeout & " minutes, thus your session has expired."
    '    Else 'if it is not a time out
    '        ' Store the error message.
    '        Session("Error") = ex.Message() & "<br>" & Context.Server.GetLastError.InnerException.ToString
    '        'jrs 2005/01/16
    '        Dim errLogger As ExceptionLogger
    '        Dim exceptionID As Integer

    '        errLogger = New ExceptionLogger(DataFileDir & "ExceptionLog.mdb", Context.Server.GetLastError.InnerException, CStr(Context.Request.UserHostAddress))
    '        exceptionID = errLogger.LogException()

    '    End If

    '    'jrs 2015/08/25
    '    If InStr(Context.Request.UserHostAddress, "frm_CyberDashboard") > 0 Then
    '        Session("bError4CyberDashboard") = True
    '    Else
    '        Session("bError4CyberDashboard") = False
    '    End If

    '    'jrs 2005/02/11
    '    If Session("SessionIdx") > -1 Then
    '        SyncLock SessionList
    '            SessionList(Session("SessionIdx")) = False
    '        End SyncLock
    '        If Session("RuningProgress") = True Then
    '            SyncLock RunControl1
    '                If Not RunControl1.RunsZero Then
    '                    RunControl1.DecreaseCounter()
    '                    Session("RuningProgress") = False
    '                End If
    '            End SyncLock
    '        End If
    '    End If
    '    'jrs 2005/01/27
    '    Session("SessionIdx") = -1
    '    ' Clear error.
    '    Server.ClearError()
    '    ' Transfer back to this page.
    '    Server.Transfer("ErrorHandler.aspx")

    'End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        _logger.Info($"Session ({Session.SessionID}) ended")

        ' Clear any model run sessions
        RunManager.Instance.RemoveSession(Session.SessionID)

        ' Fires when the session ends
        If Session("SessionIdx") >= 0 Then
            'jrs 2006/07/21 
            'Create New Files for the FARM
            If Session("FarmFlag") Then
                FarmCreateFile("BASE", Session("SessionIdx"))
                'jrs 2008/06/18
                FarmCreateFile("POP", Session("SessionIdx"))
            End If
            If Session("FarmFlagHist") Then
                FarmCreateFile("HIST", Session("SessionIdx"))
                'jrs 2008/06/18
                FarmCreateFile("POPHIST", Session("SessionIdx"))
            End If
            If Session("FarmFlag") Or Session("FarmFlagHist") Then
                FarmCreateFile("SAM", Session("SessionIdx"))
            End If

            'jrs 2005/01/18
            SyncLock SessionList
                SessionList(Session("SessionIdx")) = False
            End SyncLock
            'jrs 2005/01/27
            If Session("RuningProgress") = True Then
                SyncLock RunControl1
                    If Not RunControl1.RunsZero Then
                        RunControl1.DecreaseCounter()
                        Session("RuningProgress") = False
                    End If
                End SyncLock
            End If
        End If

        'jam 2026/05/15 Explicit cleanup of large session objects to break retention chains
        ' Even though ASP.NET should free these automatically, explicit Nothing assignment
        ' forces immediate dereference and helps GC reclaim memory faster on session end.
        Try
            Dim heavyKeys() As String = {
                "Scat_VarGroupNames", "Scat_FilledVar", "Scat_SCVarI", "Scat_SCVarD", "Scat_NewVal",
                "CountryList", "CountryListGroup",
                "CVarI", "CVarD", "CVarC", "CVarName", "VarNameI",
                "PC_TempDataBase", "CountryGroup", "RegionIdGroup"
            }
            For Each k As String In heavyKeys
                Try
                    Session(k) = Nothing
                Catch
                    ' Key may not exist for every session, ignore
                End Try
            Next
        Catch ex As Exception
            _logger.Error(ex, "Error clearing heavy session keys in Session_End")
        End Try

        'jam 2026/05/15 Optional: hint GC after a session frees significant memory.
        ' Only triggers if process memory is above threshold to avoid GC thrashing on low load.
        Try
            Dim procMem As Process = Process.GetCurrentProcess()
            procMem.Refresh()
            If procMem.PrivateMemorySize64 > MemoryThresholdBytes Then
                GC.Collect(2, GCCollectionMode.Optimized, blocking:=False)
            End If
        Catch
            ' Suppress
        End Try

        _logger.Info("Session end ended")
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        'jam 2026/05/15 Stop and dispose the per-IP cleanup timer
        Try
            If _ipCleanupTimer IsNot Nothing Then
                _ipCleanupTimer.Dispose()
                _ipCleanupTimer = Nothing
            End If
        Catch ex As Exception
            _logger.Error(ex, "Error disposing per-IP cleanup timer")
        End Try

        ' Remove all model run sessions
        RunManager.Instance.ClearAll()

        ' Fires when the application ends
        _logger.Info("Application ends")
        _logger.Info("------------------------------------------------------------------------------")
    End Sub

    Public Sub IFsLoad(Optional ByVal SessionIdx As Integer = -1)
        _logger.Info("IFsLoad - start")
        If SessionIdx >= 0 Then
            Session("SessionIdx") = SessionIdx
        End If

        Dim msg As String
        Dim ErrMsg As String
        'jrs 2008/05/08 Dim CurrencyCountry As String
        Dim IFsCaption As String

        'JRS 01/24/04
        Dim loadtbl As DataTable 'JRS 04/24/04 DAOJR.Recordset 'Object
        '--------------------------------------------------------------

        '   In reality this kind of work is never finished.
        '   One has to declare it finished when,
        '   in accord with time and circumstance,
        '   one has done the utmost.
        '          Goethe

        '   Set model version parameters
        '   ----------------------------

        'JRS 8/16/01 GUI BEGIN
        '   Open File to retrive path parameters
        '   -----------------------------------------------------------------
        'JRS 8/28/01 GUI Save the location of the init file in the registry
        Dim sIP As String
        sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")

        Dim valtblInit As DataTable

        'jrs 2022/07/15
        UseOnlySQLite = False
        If Dir(sIP) = "" Then
            UseOnlySQLite = True
        End If



        'jam 2025/10/30 Unifying Database opening mode
        'jam 2025/07/19 Changing Sqlite opening mode
        SetDataTablefromAccess(sIP, "SELECT * FROM IFsInit", "Variable", valtblInit, "", True)
        'SetDataTablefromAccessJournal(sIP, "SELECT * FROM IFsInit", "Variable", valtblInit)

        'JRS 02/11/04 *** Store Session Variables *****
        Session("valtblInit") = valtblInit
        ' ********************************************

        '   Determine model load
        '   --------------------

        If SeekValue(valtblInit, "BaseModelCoVaTra", "Value") Then ModelLoad = "LoadCoVaTrA"
        If SeekValue(valtblInit, "BaseModelHowHappy", "Value") Then ModelLoad = "LoadHowHappy"
        If SeekValue(valtblInit, "BaseModelProfessional", "Value") Then ModelLoad = "LoadProfessional"
        If SeekValue(valtblInit, "BaseModelSAG", "Value") Then ModelLoad = "LoadSAG"
        If SeekValue(valtblInit, "BaseModelStudent", "Value") Then ModelLoad = "LoadStudent"
        If SeekValue(valtblInit, "BaseModelTERRA", "Value") Then ModelLoad = "LoadTERRA"
        If SeekValue(valtblInit, "BaseModelFull", "Value") Then ModelLoad = "LoadFull"
        'JRS 05/22/03 RAND PARDEE
        If SeekValue(valtblInit, "BaseModelRANDPardee", "Value") Then ModelLoad = "LoadRANDPardee"
        'jrs 2022/05/31 make compatible with older versions
        Dim sVal As String
        'jrs 2023/06/02
        sVal = SeekValue(valtblInit, "OpenNewTabs", "Value")
        If sVal > "" Then
            Session("mnuTabs") = CBool(sVal)
        End If

        'jam 2025/10/30 Unifying Database opening mode
        'jam 2025/07/19 Changing SQLite opening mode
        SetDataTablefromAccess(sIP, "SELECT * FROM " & ModelLoad, "Variable", loadtbl, "", True)
        'SetDataTablefromAccessJournal(sIP, "SELECT * FROM " & ModelLoad, "Variable", loadtbl)

        '   Read options specific to loads
        '   ------------------------------


        DemandFile = SeekValue(loadtbl, "DemandFile$", "Value")
        'jrs 2022/08/03
        If UseOnlySQLite Then
            'DemandFile = DemandFile.Replace(".exe", ".dll")
            'DemandFile = "rundll32.exe"
            Dim sPF As String
            sPF = Environment.ExpandEnvironmentVariables("%ProgramW6432%")
            'DemandFile = sPF & "\dotnet\dotnet.exe"
            DemandFile = "IFs.exe "
        End If
        GroupIDNew = SeekValue(loadtbl, "GroupIDNew$", "Value")
        Grouping = SeekValue(loadtbl, "Grouping$", "Value")
        'if true, HistoricBase$ records whether or not actually used
        HistoricOption = SeekValue(loadtbl, "HistoricOption$", "Value")
        IFsCaption = SeekValue(loadtbl, "IFsCaption", "Value")
        ModelName = SeekValue(loadtbl, "ModelName$", "Value")
        ModelVersion = SeekValue(loadtbl, "ModelVersion$", "Value")
        'jrs 2023/07/24 for compatiblity with old versions added if > version 8
        'jrs 2203/05/11
        If ModelVersion > "Version 8" Then
            SubVersionNumber = SeekValue(loadtbl, "SubVersionNumber", "Value")
        End If
        Regionalization = SeekValue(loadtbl, "Regionalization$", "Value")
        RegionIDNew = SeekValue(loadtbl, "RegionIDNew$", "Value")
        VCATFile = SeekValue(loadtbl, "VCATFile$", "Value")
        RegionalIOCoefTabl = SeekValue(loadtbl, "RegionalIOCoefTabl$", "Value")
        'jrs 2008/05/08 changed to a session variable
        Session("CurrencyCountry") = SeekValue(loadtbl, "CurrencyCountry$", "Value")

        SupplementalDataFile = SeekValue(loadtbl, "SupplementalDataFile$", "Value")

        SupplementalHistoricDataFile = SeekValue(loadtbl, "SupplementalHistoricDataFile$", "Value")

        MaxReg = SeekValue(loadtbl, "MaxReg", "Value")

        'cdv 2021/01/12
        'jrs 2022/05/31 bMaleFirst = SeekValue(loadtbl, "MaleFirst$", "Value")
        'jrs 2022/05/31 bCoreGroupCheck = SeekValue(loadtbl, "CoreGroups$", "Value")
        sVal = SeekValue(loadtbl, "MaleFirst$", "Value")
        If sVal > "" Then
            bMaleFirst = CBool(sVal)
        End If
        sVal = SeekValue(loadtbl, "CoreGroups$", "Value")
        If sVal > "" Then
            bCoreGroupCheck = CBool(sVal)
        End If




        '   Read generic, not-load specific options
        '   ---------------------------------------

        DevelopmentMode = SeekValue(valtblInit, "DevelopmentMode$", "Value")

        'jrs 2019/12/26
        'jrs 2022/05/31 StandAloneMode = SeekValue(valtblInit, "StandAloneMode$", "Value")
        sVal = SeekValue(valtblInit, "StandAloneMode$", "Value")
        If sVal > "" Then
            StandAloneMode = CBool(sVal)
        Else
            StandAloneMode = True
        End If

        'False means distribution mode
        'Must set to false before compiling
        AbbCodeMode = SeekValue(valtblInit, "AbbCodeMode$", "Value")
        'True means simplified version for code distribution

        'One or the other should be true
        ESRIMap = SeekValue(valtblInit, "ESRIMap$", "Value")
        BMMap = SeekValue(valtblInit, "BMMap$", "Value")

        '   Set run horizon
        '   ---------------

        FirstYearForecast = SeekValue(valtblInit, "FirstYearForecast%", "Value")
        'jrs 2011/03/23
        Dim iLastYearForecast As Integer
        iLastYearForecast = SeekValue(valtblInit, "LastYearForecast%", "Value")
        'jrs 2005/06/20
        If Not IsNothing(Session("LastYearForecast")) Then
            'validate
            If FirstYearForecast > Session("LastYearForecast") Or iLastYearForecast < Session("LastYearForecast") Then
                Session("LastYearForecast") = iLastYearForecast
            End If
        Else
            Session("LastYearForecast") = iLastYearForecast
        End If
        FirstYearHistory = SeekValue(valtblInit, "FirstYearHistory%", "Value")
        LastYearHistory = SeekValue(valtblInit, "LastYearHistory%", "Value")
        Session("FirstYear") = FirstYearForecast 'Default to forecast, not history
        Session("LastYear") = Session("LastYearForecast")
        Session("RunYears") = 0 'For Cohort population data plot

        'jrs 2016/03/23
        Dim iFirstYear As Integer
        If Session("FirstYear") Mod 5 = 0 Then
            iFirstYear = Session("FirstYear")
        Else
            Dim Diff As Integer
            Diff = Session("FirstYear") Mod 5
            iFirstYear = Session("FirstYear") + 5 - Diff
        End If

        'jrs 2024/02/02
        ''jrs 2020/09/04
        'If StandAloneMode Then
        '    Session("DisplayLastYear") = SeekValue(valtblInit, "DisplayLastYear", "Value")
        'Else
        '    'jrs 2011/05/20
        '    'JAM 2012-10-05 Modify DisplayLastYear Calculation
        '    'Session("DisplayLastYear") = 2030
        '    'jrs 2016/03/23 Session("DisplayLastYear") = Session("FirstYear") + 20
        '    Session("DisplayLastYear") = iFirstYear + 20
        '    If Session("DisplayLastYear") > Session("LastYear") Then
        '        Session("DisplayLastYear") = Session("LastYear")
        '    End If
        'End If
        Session("DisplayLastYear") = SeekValue(valtblInit, "DisplayLastYear", "Value")
        'jrs 2026/01/12
        Session("DefRegion") = SeekValue(valtblInit, "DefRegion", "Value")

        'jrs 2025/09/04 remove if not Development mode 
        ''jrs 2019/07/26
        'If Not CBool(DevelopmentMode) Then
        DefaultFlexDisplayFilter = SeekValue(valtblInit, "DefaultFlexDisplayFilter", "Value")
        'End If

        '   Set parameters that keep track of whether base has been rebuilt or model run
        '   ----------------------------------------------------------------------------

        RebuiltBase = SeekValue(valtblInit, "RebuiltBase$", "Value")
        RunModel = SeekValue(valtblInit, "RunModel$", "Value")

        '   Read Password for data file
        '   ---------------------------

        'JRS 05/07/04 DataPW = "[REDACTED]" + SeekValue(valtblInit, "DataPW$", "Value") + ";"
        DataPW = SeekValue(valtblInit, "DataPW$", "Value")
        'jrs 2008/01/03
        DataPW = DataPW & "12"
        'DataPW$ = "[REDACTED]"

        'jrs 2021/06/23
        sLanguage = SeekValue(valtblInit, "Language", "Value")
        If sLanguage = "English" Then sLanguage = ""

        '   Establish start directory so that can check with settings in .INI
        '   -----------------------------------------------------------------

        'jrs 2005/01/18 Dim DInfo As New DirectoryInfo(Me.MapPathSecure(""))
        StartDirec = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\")

        'jrs 2019/07/11
        If DevelopmentMode Then
            'jrs 2019/07/11
            bHidePrivateVars = False
            'bHidePrivateVars = (UCase(SeekValue(valtblInit, "HidePrivateVars$", "Value")) = "TRUE")
        Else
            'jrs 2019/07/11
            bHidePrivateVars = (UCase(SeekValue(valtblInit, "HidePrivateVars$", "Value")) = "TRUE")
        End If

        'jrs 2025/10/03
        'jrs 2025/10/31 
        Dim sConstantDollarsYear As String
        sConstantDollarsYear = SeekValue(valtblInit, "ConstantDollarsYear", "Value")
        If IsNumeric(sConstantDollarsYear) Then
            ConstantDollarsYear = CInt(sConstantDollarsYear)
        Else
            ConstantDollarsYear = 2011
        End If
        PovLevelsByPPPYear = New Dictionary(Of Integer, Dictionary(Of String, Single))
        SetDataTablefromAccessJournal(sIP, "SELECT * FROM PovertyLevelPerPPPYear", "", loadtbl)
        'jrs 2025/10/31 added not is nothing to check for older versions that do not have this table
        If Not IsNothing(loadtbl) Then
            For rr = 0 To loadtbl.Rows.Count - 1
                Dim iPPPYear As Integer
                Dim sPovLevel As String
                Dim dPovValue As Double
                iPPPYear = CInt(loadtbl.Rows(rr).Item("PPPYear"))
                sPovLevel = CStr(loadtbl.Rows(rr).Item("Description"))
                dPovValue = CDbl(loadtbl.Rows(rr).Item("PovertyLevel"))
                If Not PovLevelsByPPPYear.ContainsKey(iPPPYear) Then
                    PovLevelsByPPPYear.Add(iPPPYear, New Dictionary(Of String, Single))
                End If
                PovLevelsByPPPYear(iPPPYear).Add(sPovLevel, dPovValue)
            Next
        End If

        '   Read Settings from IFS.INI File
        '   -------------------------------

        On Error GoTo IniError
        Dim LocalDirect As String

        Read_INI_File(valtblInit, LocalDirect)

        'jrs 2021/06/14
        UpdateLanguage()

        'Instead of clicking on Continue
        'Initialize already

        'jam 2026/04/09 Commented because was include in semaphore delay after 20 sessions
        'IFSinit(valtblInit)


        'jam 2026/04/09 Calling IFsInit considering Memory limitations 
        IFSinitLimited(valtblInit)


        '            '
        '            '   Display opening screen with world map only
        '            '   ------------------------------------------

        '            'CONVERSION_ISSUE: Class System.Windows.Forms.FormWindowState from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.WindowState' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.WindowState.html'.
        '            'JRSVBNET WindowState = 'JRSVBNET System.Windows.Forms.FormWindowState.Maximized
        '            'JRS 05/22/03 RAND PARDEE Show 'Display Window before init for user
        '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        '            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        '            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
        '            lbl_IFSExplain.Visible = False
        '            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
        '            lbl_IFsName.Visible = False
        '            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
        '            cmd_Exit.Visible = False
        '            '    msg$ = "Ready to read init"
        '            '    MsgBox msg$
        '            '
        '            '   Read all initialization files
        '            '   -----------------------------

        '            '    Call init


        '            '   Display labels and continue button
        '            '   ----------------------------------
        '            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
        '            lbl_IFSExplain.Visible = True
        '            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
        '            lbl_IFsName.Visible = True
        '            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
        '            cmd_Exit.Text = "Load and Continue"
        '            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.html'.
        '            cmd_Exit.Visible = True
        '            cmd_Exit.Enabled = True

        '            '    cmd_Exit.Enabled = False
        '            '    SetFocus
        '            '    WindowState = 2

        '            'JRS 05/22/03 RAND PARDEE cmd_Exit.SetFocus

        '            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        '            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

        '            ' raman feb'02 initializing Guided_use (defined in variables.bas
        '            Guided_use = False

        Exit Sub

IniError:
        _logger.Error(Err.GetException, "IFsLoad - iniError")

        msg = "Error occurred processing IFs.ini file.  It is probable that "
        msg = msg & "the local path specified there, " & LocalDirect
        msg = msg & " is invalid. Correct it or disable by specifying c:\DUMMY\"
        Session("Error") = msg
        ' Clear error.
        Server.ClearError()
        ' Transfer back to this page.
        Server.Transfer("ErrorHandler.aspx")
    End Sub
    ''' <summary>
    '''  
    ''' 
    ''' jam 2026/04/09 Procedure for IFSinit in different sessions 
    ''' 
    ''' 
    ''' </summary>
    Private Sub IFSinitLimited(ByRef valtblInit As DataTable)
        Dim semAcquired As Boolean = False

        'Memory mesure for right sessions quantity
        Dim procAntes As Process = Process.GetCurrentProcess()
        procAntes.Refresh()
        Dim mbAntes As Long = procAntes.PrivateMemorySize64 \ (1024L * 1024L)
        Dim swInit As New System.Diagnostics.Stopwatch()

        Try
            'Max wait availabe slot: 60 sec
            semAcquired = _initSemaphore.Wait(60000)
            If Not semAcquired Then
                _logger.Warn($"IFSinitLimited - semaphore isn't accepted for this session {Session.SessionID} after 60 secs — in progress")
            End If

            swInit.Start()
            IFSinit(valtblInit)
            swInit.Stop()

            Dim procDespues As Process = Process.GetCurrentProcess()
            procDespues.Refresh()
            Dim mbDespues As Long = procDespues.PrivateMemorySize64 \ (1024L * 1024L)
            _logger.Info($"IFSinit completed — session {Session.SessionID} — duration: {swInit.ElapsedMilliseconds}ms — before memory: {mbAntes}MB — after: {mbDespues}MB — delta: {mbDespues - mbAntes}MB")

        Finally
            If semAcquired Then
                _initSemaphore.Release()
            End If
        End Try
    End Sub
    Private Sub Read_INI_File(ByRef valtblInit As DataTable, ByRef LocalDirect As String)
        'jrs 2005/02/12
        Dim fNum As Integer

        Dim msg As String
        Dim directory As String
        Dim ICount As Short
        Dim Dummy As Object
        Dim Returned As String
        Dim Size_Renamed As Object
        Dim Def As String
        Dim Key As String
        Dim Section As String
        Dim ErrMsg As String
        Dim HelpFileSubDir As String
        Dim DataHistFile As String
        Dim DataFile As String
        Dim WorldMapFile As String
        Dim NL As String
        '--------------------------------------------------------------

        NL = "<br>" & Chr(10)

        '   Set basic Default File Names/Locations/Time Horizons
        '   ----------------------------------------------------

        WorldMapFile = SeekValue(valtblInit, "WorldMapFile$", "Value")
        'JRS 8/18/01 GUI WorldMapFile$ = "World_ma.bmp"
        WorldMapLocation = StartDirec & WorldMapFile

        DataFileSubDir = SeekValue(valtblInit, "DataFileSubDir$", "Value")
        'JRS 8/18/01 GUI DataFileSubDir$ = "DATA\"
        DataFile = SeekValue(valtblInit, "DataFile$", "Value")
        'JRS 8/18/01 GUI DataFile$ = "IFS.MDB"
        DataHistFile = SeekValue(valtblInit, "DataHistFile$", "Value")
        'jrs 2022/07/15
        If UseOnlySQLite Then
            DataFile = DataFile.Replace(".MDB", ".db")
            DataHistFile = DataHistFile.Replace(".MDB", ".db")
        End If
        'JRS 8/18/01 GUI DataHistFile$ = "IFS.MDB" '"IFsHistBase.MDB"
        Session("ResFileSubDir") = SeekValue(valtblInit, "ResFileSubDir$", "Value")
        'JRS 8/18/01 GUI ResFileSubDir$ = "RUNFILES\"
        HelpFileSubDir = SeekValue(valtblInit, "HelpFileSubDir$", "Value")
        'JRS 8/18/01 GUI HelpFileSubDir$ = "HELP\"
        Session("ScenarioFileSubDir") = SeekValue(valtblInit, "ScenarioFileSubDir$", "Value")
        'JRS 8/18/01 GUI ScenarioFileSubDir$ = "SCENARIO\"
        Session("SensitivityFileSubDir") = SeekValue(valtblInit, "SensitivityFileSubDir$", "Value")
        'JRS 8/18/01 GUI SensitivityFileSubDir$ = "SENSITIVITY\"

        'JRS MULTISESSION 06/02/04 ***
        If Session("SessionIdx") <> -1 Then
            Session("ResFileSubDir") = Session("ResFileSubDir") & "Session"
            Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & "Session"
            Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & "Session"
            If Session("SessionIdx") < 10 Then
                Session("ResFileSubDir") = Session("ResFileSubDir") & "0"
                Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & "0"
                Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & "0"
            End If
            Session("ResFileSubDir") = Session("ResFileSubDir") & Trim(Session("SessionIdx")) & "\"
            Session("ScenarioFileSubDir") = Session("ScenarioFileSubDir") & Trim(Session("SessionIdx")) & "\"
            Session("SensitivityFileSubDir") = Session("SensitivityFileSubDir") & Trim(Session("SessionIdx")) & "\"
        End If
        '*****************************

        '   Read local directory from .INI file
        '      The .INI file is necessary for a network installation, because in that case
        '           it carries the path for local files;  see Read.Me for explanation.
        '   ------------------------------------------------------------------------------

        Section = SeekValue(valtblInit, "Section$", "Value")
        'JRS 8/24/01 GUI Section$ = "FILES"
        Key = "LocalDirectory" : Def = "C:\IFS\"
        'UPGRADE_WARNING: Couldn't resolve default property of object Size. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Size_Renamed = 40
        'jrs 2011/03/25 
        'Returned = Space(Size_Renamed)
        'Dummy = GetPrivateProfileString(Section, Key, Def, Returned, Size_Renamed, IFSINIFile)
        Returned = "c:\DUMMY\"
        'For ICount = 40 To 1 Step -1  'remove trailing blanks, but leave internal ones
        '    If InStr(ICount, Returned, " ") = ICount Then
        '        Returned = VB.Left(Returned, ICount - 1) 'removes trailing blanks
        '    ElseIf InStr(ICount, Returned, Chr(0)) = ICount Then
        '        Returned = VB.Left(Returned, ICount - 1) 'removes first blank
        '    Else
        '        Exit For
        '    End If
        'Next ICount
        '    Returned$ = Left$(Returned$, InStr(Returned$, Chr$(0)) - 1)
        LocalDirect = Returned
        '    msg$ = "Have read .INI for LocalDirect, which is " + LocalDirect$
        '   MsgBox msg$

        '   Set Start-up (Possibly Network) Directory
        '   -----------------------------------------

        FDRV = StartDirec 'Was read in Form.Load
        If VB.Right(FDRV, 1) <> "\" Then FDRV = FDRV & "\"
        '    msg$ = "Have read StartDirect$ for FDRV$, which is " + FDRV$
        '    MsgBox msg$

        '   Set and Check Write on Local Directory - May be the Same as "Network"
        '   ---------------------------------------------------------------------

        If UCase(LocalDirect) = UCase("c:\DUMMY\") Then
            PutFdrv = FDRV 'LocalDirectory = NetworkDirectory; No Network
        Else
            PutFdrv = LocalDirect 'LocalDirectory taken from .INI
        End If
        If VB.Right(PutFdrv, 1) <> "\" Then PutFdrv = PutFdrv & "\"

        '   Check for all Subdirectories on Target Directory
        '   ------------------------------------------------

        On Error GoTo PutDirNotExist 'Make Sure Local Directory Exists
        ChDir(PutFdrv)
        On Error GoTo ResDirNotExist 'Make Sure Local Result Subdirectory Exists
        ChDir(PutFdrv & Session("ResFileSubDir"))
        On Error GoTo DataDirNotExist 'Make Sure Local Data Subdirectory Exists
        ChDir(PutFdrv & DataFileSubDir)
        On Error GoTo ScenarioDirNotExist 'Make Sure Local Scenario Subdirectory Exists
        ChDir(PutFdrv & Session("ScenarioFileSubDir"))
        On Error GoTo SensitivityDirNotExist 'Make Sure Local Sensitivity Subdirectory Exists
        ChDir(PutFdrv & Session("SensitivityFileSubDir"))

        '    msg$ = "Have fed PutFdrv$, which is " + PutFdrv$
        '    MsgBox msg$

        '   Establish run files directory paths
        '   -----------------------------------

        Session("FdrvRun") = FDRV & Session("ResFileSubDir")
        Session("PutFdrvRun") = PutFdrv & Session("ResFileSubDir")
        Session("PutFdrvRun2") = Session("PutFdrvRun") 'Used for file management with dialog box
        If VB.Right(Session("PutFdrvRun2"), 1) = "\" Then
            Session("PutFdrvRun2") = VB.Left(Session("PutFdrvRun2"), Len(Session("PutFdrvRun2")) - 1)
        End If
        '    msg$ = "Have determinded PutFdrvRun$, which is " + PutFdrvRun$
        '    MsgBox msg$

        '   Copy Date.Dat to Result Directory and Write a Line to it
        '       This will test whether network installation will allow local write
        '       Because this capability is essential, model will stop if not
        '       and alert user to put writable path in .INI file
        '   ----------------------------------------------------------------------

        On Error GoTo StartWriteProblem 'Add Extra Line to Date.Dat to Check Write
        DataFileDir = FDRV & DataFileSubDir
        PutDataFileDir = PutFdrv & DataFileSubDir
        If DataFileDir <> PutDataFileDir Then
            FileCopyCheck(DataFileDir & "Date.Dat", PutDataFileDir & "Date.Dat")
        End If
        'JRS 02/08/04 FileOpen(1, PutDataFileDir & "Date.Dat", OpenMode.Append)
        'JRS 02/08/04 WriteLine(1, "Test")
        'JRS 02/08/04 FileClose(1)
        '    msg$ = "Have tested StartWrite Problem, which is " + PutFdrvRun$
        '    MsgBox msg$

        '   Establish help file
        '   -------------------

        'UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'JRSVBNET App.HelpFile = FDRV & HelpFileSubDir & "IFSHELP.HLP"

        '   Data file locations
        '   --------------------

        IFs_MDB = StartDirec & DataFileSubDir & DataFile
        IFsHist_MDB = StartDirec & DataFileSubDir & DataHistFile
        IFsCoVaTrA_MDB = StartDirec & DataFileSubDir + SeekValue(valtblInit, "IFsCoVaTrA_MDB", "Value")
        'JRS 8/24/01 IFsCoVaTrA_MDB = StartDirec$ + DataFileSubDir$ + "IFsCoVaTrA.mdb"
        '    IFsGroups_MDB = StartDirec$ + DataFileSubDir$ + "IFsGroups.mdb"
        Session("IFs_MDB2") = Session("PutFdrvRun") & DataFile
        Session("IFsHist_MDB2") = Session("PutFdrvRun") & DataHistFile
        Session("IFsCoVaTrA_MDB2") = Session("PutFdrvRun") + SeekValue(valtblInit, "IFsCoVaTrA_MDB2", "Value")
        'jrs 2022/07/27
        If UseOnlySQLite Then
            IFsCoVaTrA_MDB = IFsCoVaTrA_MDB.Replace(".mdb", ".db")
            Session("IFsCoVaTrA_MDB2") = Session("IFsCoVaTrA_MDB2").Replace(".mdb", ".db")
        End If
        'JRS 8/24/01 IFsCoVaTrA_MDB2 = PutFdrvRun$ + "IFsCoVaTrA.mdb"
        VCATFile = SeekValue(valtblInit, "VCATFile$", "Value")
        'JRS 8/24/01 VCATFile$ = "VCat.Dat"
        If ModelLoad = "LoadTERRA" Then
            VCATFile = SeekValue(valtblInit, "VCATFileTerra$", "Value")
            'JRS 8/24/01 VCATFile$ = "VCATTERRA.DAT"
        End If
        VCATScenarioFile = SeekValue(valtblInit, "VCATScenarioFile$", "Value")
        'JRS 8/29/01 VCATScenarioFile$ = "VCatfull.Dat"

        '   Change to Start Directory
        '   -------------------------

        DefaultDrive = VB.Left(FDRV, 2)
        ChDrive(DefaultDrive)
        directory = FDRV
        If VB.Right(directory, 1) = "\" Then
            directory = VB.Left(directory, Len(directory) - 1)
        End If
        ChDir(directory)
        '    msg$ = "Have changed to Start directory, which is " + directory$
        '    MsgBox msg$


        '   Delete Working Scenario at the load of Model
        '   --------------------------------------------

        'establish scenario directory
        Session("ScenarioFileDir") = PutFdrv & Session("ScenarioFileSubDir")
        Session("SensitivityFileDir") = PutFdrv & Session("SensitivityFileSubDir")
        'Delete Working Scenario at the load of Model
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(Session("ScenarioFileDir") & "Working.Sce") <> "" Then
            Kill(Session("ScenarioFileDir") & "Working.Sce")
        End If
        'JRS 07/15/03 BEGIN
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(Session("ScenarioFileDir") & "WorkingGroup.Sce") <> "" Then
            Kill(Session("ScenarioFileDir") & "WorkingGroup.Sce")
        End If
        'JRS 07/15/03 END
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(Session("ScenarioFileDir") & "Working.Txt") <> "" Then
            Kill(Session("ScenarioFileDir") & "Working.Txt")
        End If
        'write a blank file
        'jrs 2005/02/12
        If Session("SessionIdx") > -1 Then
            'jrs 2005/02/12
            fNum = FreeFile()
            FileOpen(fNum, Session("ScenarioFileDir") & "Working.Txt", OpenMode.Output)
            FileClose(fNum)
        End If


        '    msg$ = "Have Killed Working Scenario, if exists "
        '    MsgBox msg$


        'Vensim Integration  - Start

        'Vensim Integration  - Replace with MSAccess Field Later
        VensimFileSubDir = "Vensim\"

        'Vensim Integration : delete working.rgg
        VenSimFileDir = PutFdrv & VensimFileSubDir
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(VenSimFileDir & "Working.Rgg") <> "" Then
            Kill(VenSimFileDir & "Working.Rgg")
        End If
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(VenSimFileDir & "Data.Dat") <> "" Then
            Kill(VenSimFileDir & "Data.Dat")
        End If

        'Vensim Integration  - End

        'Vensim Integration- Late November 2003
        VensimOpened = 0
        'End Vensim Integration- Late November 2003


        '   Check if DATE.DAT exists and create if it does not
        '   --------------------------------------------------

        If DataFileDir = PutDataFileDir Then Exit Sub 'No Network; Date.Dat not needed
        '    msg$ = "Still in Read_InI_File to check Date.dat "
        '    MsgBox msg$
        On Error GoTo nonexist1
        'jrs 2005/02/12
        fNum = FreeFile()
        FileOpen(fNum, PutDataFileDir & "DATE.DAT", OpenMode.Input)
        Input(fNum, Session("FirstYear"))
        Input(fNum, Session("LastYear")) 'Use DATE.DAT if exists
        Session("FirstYear") = FirstYearForecast 'In case values are history, replace
        Session("LastYear") = Session("LastYearForecast")
        FileClose(fNum) 'DATE.DAT needed only if Rebuild Base and change end year
        '    msg$ = "Leaving Read_Ini"
        '    MsgBox msg$
        On Error GoTo 0
        Exit Sub

nonexist1:
        _logger.Error(Err.GetException, "Read_INI_File nonexist1 error")
        'jrs 2005/02/12
        fNum = FreeFile()
        FileOpen(fNum, PutDataFileDir & "DATE.DAT", OpenMode.Append)
        WriteLine(fNum, FirstYearForecast, Session("LastYearForecast")) 'Fill DATE.DAT if does not exist
        FileClose(fNum)
        Exit Sub

PutDirNotExist:
        _logger.Error(Err.GetException, "Read_INI_File PutDirNotExist error")
        MkDir(PutFdrv)
        Resume Next
        Exit Sub

ResDirNotExist:
        _logger.Error(Err.GetException, "Read_INI_File ResDirNotExist error")
        MkDir(PutFdrv & Session("ResFileSubDir"))
        Resume Next
        Exit Sub

DataDirNotExist:
        _logger.Error(Err.GetException, "Read_INI_File DataDirNotExist error")
        MkDir(PutFdrv & DataFileSubDir)
        Resume Next
        Exit Sub


ScenarioDirNotExist:
        _logger.Error(Err.GetException, "Read_INI_File ScenarioDirNotExist error")
        MkDir(PutFdrv & Session("ScenarioFileSubDir"))
        Resume Next
        Exit Sub

SensitivityDirNotExist:
        _logger.Error(Err.GetException, "Read_INI_File SensitivityDirNotExist error")
        MkDir(PutFdrv & Session("SensitivityFileSubDir"))
        Resume Next
        Exit Sub

StartWriteProblem:
        _logger.Error(Err.GetException, "Read_INI_File StartWriteProblem error")
        msg = "IFs cannot write to " & PutDataFileDir & "  .  Make sure that " & NL
        msg = msg & "the IFS.INI file contains a valid path to which "
        msg = msg & "IFs can write local files.  " & NL
        msg = msg & "Execution will now cease."
        'jrs 2005/02/10
        msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
        Session("Error") = msg
        ' Clear error.
        Server.ClearError()
        ' Transfer back to this page.
        Server.Transfer("ErrorHandler.aspx")

    End Sub

    Public Sub IFSinit(ByRef valtblInit As DataTable)
        Static regcount As Short
        Static gloop As Object
        Static actreg As Short
        Static VarName As String
        Static TLoop As Object
        Static tfrecount As Object
        'Static eleloop As Object
        Static eleloop As Integer

        'Static valtbl6 As DataTable
        'Static msg As String

        'jam 2026/04/03 The variables are reassigned in each calling, Itsn't neccesary that they are declared like Static
        Dim valtbl6 As DataTable
        Dim msg As String

        Static REDCD As Object
        Static VideoOkay As String
        Static OutFile As String
        Static InFile As String

        'jam 2026/04/03 The variables are reassigned in each calling, Itsn't neccesary that they are declared like Static
        'Static ErrMsg As String
        Dim ErrMsg As String
        Static Basefile As String
        Static iloop As Object

        'jam 2026/04/03 The variables are reassigned in each calling, Itsn't neccesary that they are declared like Static
        'Static NL As String
        Dim NL As String
        '--------------------------------------------------------------

        Static REGS As REGTYPE
        Static INREGS As REGTYPE
        Static OUTREGS As REGTYPE
        Static lpage(50) As Object
        Static ACTF(10) As Object

        _logger.Debug("IFsInit start")

        NL = "<br>" & Chr(10)
        ACTF(0) = "*"

        For iloop = 1 To 10
            ACTF(iloop) = ""
        Next iloop
        On Error GoTo InitFileReadError

        '   Identify Start Files
        '   --------------------
        StartFile = SeekValue(valtblInit, "StartFile$", "Value")
        'JRS 8/24/01 GUI StartFile$ = "IFS"
        Basefile = SeekValue(valtblInit, "Basefile$", "Value")
        'JRS 8/24/01 GUI Basefile$ = "IFSBASE"
        Session("Reload") = SeekValue(valtblInit, "Reload$", "Value")
        'JRS 8/24/01 GUI Reload$ = "True"
        Session("COUNTE") = 0
        'jrs 2011/03/23
        'jrs 2011/05/20 Session("DisplayLastYear") = Nothing
        Session("oldLastYear") = Nothing

        '   Set up indices for scattergram control
        '   --------------------------------------

        ReDim Session("bI")(8)
        'JRS 8/17/01 GUI aI = 0
        'JRS 8/17/01 GUI aD = 0
        'JRS 8/17/01 GUI For ILOOP = 1 To 8
        'JRS 8/17/01 GUI bI(ILOOP) = 0
        'JRS 8/17/01 GUIbD(ILOOP) = 0
        'JRS 8/17/01 GUI Next ILOOP

        '   Set global parameters
        '   ---------------------

        '   Directions for changing MaxReg% (read below from IFS.GRP), which sets the maximum
        '       number of regions
        '       1.  Edit IFS.GRP and change regional line; total number is regions + 1 (for ALL);
        '              "Empty1" alerts IFs to stop looking for additional named regions
        '       2.  Rebuild Common so that new size of MaxReg% is reflected in Fill and Retrieve
        '       3.  Use information from Rebuild Common on new value of FILEN to change Declare2
        '           May need to Rebuild Common again
        '       4.  Exit from VB and re-enter (to pick up Fill, Retriev, Declare)
        '       5.  Rebuild base so that data get filled into .RUN properly
        '       6.  Run IFs through longest year and save results into base case.

        NCohorts = SeekValue(valtblInit, "NCohorts", "Value")
        NWVSWaves = SeekValue(valtblInit, "NWVSWaves", "Value")
        'jrs 2022/09/20
        ModelRunningWith = SeekValue(valtblInit, "CurrentlyRunningWith", "Value")
        'JRS 8/19/01 GUI NCohorts = 21
        WVSCohorts = SeekValue(valtblInit, "WVSCohorts", "Value")
        'JRS 8/19/01 GUI WVSCohorts = 7
        NumbCulReg = SeekValue(valtblInit, "NumbCulReg", "Value")
        'JRS 8/19/01 GUI NumbCulReg = 11
        NAges = SeekValue(valtblInit, "NAges", "Value")
        'JRS 8/19/01 GUI NAges = 100

        'UPGRADE_WARNING: Couldn't resolve default property of object NIOMatrices. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        NIOMatrices = SeekValue(valtblInit, "NIOMatrices", "Value")

        NER = SeekValue(valtblInit, "NER%", "Value")
        'JRS 8/19/01 GUI NER% = 4 '2  Number of conventional energy types with resource bases
        NEU = SeekValue(valtblInit, "NEU%", "Value")
        'JRS 8/19/01 GUI NEU% = 5 '3  Number of energy types with resources, including unconventional
        NEP = SeekValue(valtblInit, "NEP%", "Value")
        'JRS 8/19/01 GUI NEP% = 6 '4  Number of conventional energy production types, total
        NET = SeekValue(valtblInit, "NET%", "Value")
        'JRS 8/19/01 GUI NET% = 7 '5  Number of conventional and unconventional energy types, total

        NGovExp = SeekValue(valtblInit, "NGovExp%", "Value")

        NSec = SeekValue(valtblInit, "NSec%", "Value")

        NWBTar = SeekValue(valtblInit, "NWBTar%", "Value")

        NHHTypes = SeekValue(valtblInit, "NHHTypes%", "Value")

        'jrs 2025/06/30
        Session("VersionName") = SeekValue(valtblInit, "VersionName", "Value")

        '    msg$ = "Ready to start checking files in init"
        '    MsgBox msg$

        '   Set Sensitivity and Scenario Flags
        '   ----------------------------------
        Session("SensitivityFlag") = SeekValue(valtblInit, "SensitivityFlag$", "Value")
        'JRS 8/19/01 GUI SensitivityFlag$ = False
        Session("SetLoopChange") = CStr(False)
        Session("Scenarioflag") = False

        '   check if filename.RUN exists
        '   ----------------------------

        'JRS MULTISESSION 06/02/04
        'IF .RUN FILE IS NOT THERE TRY TO COPY FROM RUNFILES
        Dim sRunDir As String
        If InStr(Session("FdrvRun"), "Session") > 0 Then
            sRunDir = Mid(Session("FdrvRun"), 1, InStr(Session("FdrvRun"), "Session") - 1)
        Else
            sRunDir = Session("FdrvRun")
        End If

        'jrs 2025/10/31
        If UseOnlySQLite Then
            'jrs 2023/06/21
            Session("mnuSQLite") = "True"
            ''cdv 2021/02/18 Change SQLite session var to match stand alone mode
            'If StandAloneMode = True Then
            '    Session("mnuSQLite") = "True"
            'Else
            '    Session("mnuSQLite") = "False"
            'End If
        Else
            Session("mnuSQLite") = "False"
        End If


        'cdv 2021/02/18 ****START**** Rebuild all file error checking to match our SQLite settings
        If Session("mnuSQLite") = "True" Then
            ErrMsg = "The file IFSBASE.RUN.DB must exist on the RUNFILES subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(sRunDir & Basefile & ".run.db", ErrMsg, Session("Error"), Nothing)
            FileCopyCheck(sRunDir & Basefile & ".run.db", Session("FdrvRun") & Basefile & ".run.db")
        Else
            ErrMsg = "The file IFSBASE.RUN must exist on the RUNFILES subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(sRunDir & Basefile & ".run", ErrMsg, Session("Error"), Nothing)
            FileCopyCheck(sRunDir & Basefile & ".run", Session("FdrvRun") & Basefile & ".run")
        End If
        ErrMsg = ""



        '   check if filenameHIST.RUN exists
        '   ----------------------------
        If Session("mnuSQLite") = "True" Then
            ErrMsg = "The file IFSBASEHIST.RUN.DB must exist on the RUNFILES subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            If ModelLoad <> "LoadStudent" And CBool(DevelopmentMode) = False Then 'Not used for students
                Call FileCheck(sRunDir & Basefile & "HIST.run.db", ErrMsg, Session("Error"), Nothing)
            End If
            FileCopyCheck(sRunDir & Basefile & "HIST.run.db", Session("FdrvRun") & Basefile & "HIST.run.db")
        Else
            ErrMsg = "The file IFSBASEHIST.RUN must exist on the RUNFILES subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            If ModelLoad <> "LoadStudent" And CBool(DevelopmentMode) = False Then 'Not used for students
                Call FileCheck(sRunDir & Basefile & "HIST.run", ErrMsg, Session("Error"), Nothing)
            End If
            FileCopyCheck(sRunDir & Basefile & "HIST.run", Session("FdrvRun") & Basefile & "HIST.run")
        End If
        ErrMsg = ""



        '   Check if TablFunc.MDB exists
        '   ----------------------------
        'jrs 2022/07/15
        Dim ext As String
        If UseOnlySQLite Then
            ext = "db"
        Else
            ext = "mdb"
        End If

        ErrMsg = "The file TablFunc." & ext & " must exist on the DATA subdirectory"
        ErrMsg = ErrMsg & " of the directory"
        ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
        ErrMsg = ErrMsg & "You may need to re-install IFs."
        Call FileCheck(DataFileDir & "TablFunc." & ext, ErrMsg, Session("Error"), Nothing)
        ErrMsg = ""
        '    msg$ = "Have checked TablFunc existence"
        '    MsgBox msg$

        '   Check if AnalFunc.MDB exists
        '   ----------------------------

        ErrMsg = "The file AnalFunc." & ext & " must exist on the DATA subdirectory"
        ErrMsg = ErrMsg & " of the directory"
        ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
        ErrMsg = ErrMsg & "You may need to re-install IFs."
        Call FileCheck(DataFileDir & "AnalFunc." & ext, ErrMsg, Session("Error"), Nothing)
        ErrMsg = ""

        '   Check if IFS.GRP exists
        '   ----------------------------

        ErrMsg = "The file IFS.GRP must exist on the DATA subdirectory"
        ErrMsg = ErrMsg & " of the directory"
        ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
        ErrMsg = ErrMsg & "You may need to re-install IFs."
        Call FileCheck(DataFileDir & "IFS.GRP", ErrMsg, Session("Error"), Nothing)
        ErrMsg = ""
        '   msg$ = "Have checked IFs.grp existence"
        '    MsgBox msg$

        '   Check if IFsVar.mdb exists
        '   ----------------------------
        ErrMsg = "The file IFsVar." & ext & " must exist on the DATA subdirectory"
        ErrMsg = ErrMsg & " of the directory"
        ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
        ErrMsg = ErrMsg & "You may need to re-install IFs."
        Call FileCheck(DataFileDir & "IFsVar." & ext, ErrMsg, Session("Error"), Nothing)
        ErrMsg = ""
        '    msg$ = "Have checked IFsVar.mdb existence"
        '    MsgBox msg$


        '   Check if VCAT.DAT exists
        '   ----------------------------

        ErrMsg = "The file VCAT.DAT must exist on the DATA subdirectory"
        ErrMsg = ErrMsg & " of the directory"
        ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
        ErrMsg = ErrMsg & "You may need to re-install IFs."
        Call FileCheck(DataFileDir & VCATFile, ErrMsg, Session("Error"), Nothing)
        ErrMsg = ""
        '    msg$ = "Have checked vcat.dat existence"
        '    MsgBox msg$

        'Scenario Changes August 28, 2001-------
        '   Check if VCAT.DAT exists
        '   ----------------------------

        ErrMsg = "The file VCATFULL.DAT must exist on the DATA subdirectory"
        ErrMsg = ErrMsg & " of the directory"
        ErrMsg = ErrMsg & " from which you attempt to run IFS.  "
        ErrMsg = ErrMsg & "You may need to re-install IFs."
        Call FileCheck(DataFileDir & VCATScenarioFile, ErrMsg, Session("Error"), Nothing)
        ErrMsg = ""

        '   Check if IFs_MDB exists
        '   ----------------------------
        If Session("mnuSQLite") = "True" Then
            IFs_MDB = IFs_MDB.Replace(".mdb", ".db")
            ErrMsg = "The file IFS.DB must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(IFs_MDB, ErrMsg, Session("Error"), Nothing)
        Else
            ErrMsg = "The file IFS.MDB must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(IFs_MDB, ErrMsg, Session("Error"), Nothing)
        End If
        ErrMsg = ""
        '    msg$ = "Have checked Ifs.mdb existence"
        '    MsgBox msg$

        '   Check if IFsHist_MDB exists
        '   ----------------------------
        If Session("mnuSQLite") = "True" Then
            IFsHist_MDB = IFsHist_MDB.Replace(".mdb", ".db")
            ErrMsg = "The file IFSHist.DB must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(IFsHist_MDB, ErrMsg, Session("Error"), Nothing)
        Else

            ErrMsg = "The file IFSHist.MDB must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(IFsHist_MDB, ErrMsg, Session("Error"), Nothing)
        End If
        ErrMsg = ""


        '   Check if IFsCoVaTrA_MDB exists
        '   ----------------------------

        If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
            If Session("mnuSQLite") = "True" Then
                IFsCoVaTrA_MDB = IFsCoVaTrA_MDB.Replace(".mdb", ".db")
                ErrMsg = "The file IFSCoVaTrA.DB must exist on the DATA subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(IFsCoVaTrA_MDB, ErrMsg, Session("Error"), Nothing)
            Else

                ErrMsg = "The file IFSCoVaTrA.MDB must exist on the DATA subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(IFsCoVaTrA_MDB, ErrMsg, Session("Error"), Nothing)
            End If
            ErrMsg = ""
        End If
        '    msg$ = "Have checked IFsCoVaTra.mdb existence"
        '    MsgBox msg$


        'jrs 2011/01/25
        ''   Check if IFSHELP.HLP exists
        ''   ----------------------------

        'ErrMsg = "The file IFSHELP.HLP must exist on the HELP subdirectory"
        'ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
        'ErrMsg = ErrMsg & "You may need to re-install IFs."
        'Call FileCheck(FDRV & "HELP\IFSHELP.HLP", ErrMsg, Session("Error"), Me)
        'ErrMsg = ""
        '    msg$ = "Have checked IfsHelp existence"
        '    MsgBox msg$

        'JRS 10/08/02 GUIDED USE
        '   Check if GuidedUse.mdb exists
        '   ----------------------------

        'jrs 2022/07/15
        If Not UseOnlySQLite Then
            ErrMsg = "The file GuidedUse.mdb must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(FDRV & "DATA\GuidedUse.mdb", ErrMsg, Session("Error"), Nothing)
        End If
        ErrMsg = ""
        '    msg$ = "Have checked GuidedUse existence"
        '    MsgBox msg$

        '   check if filename.RUN exists
        '   ----------------------------
        If Session("mnuSQLite") = "True" Then
            ErrMsg = "The file SAMBase.db must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(PutDataFileDir & "SAMBase.db", ErrMsg, Session("Error"), Nothing)
            InFile = PutDataFileDir & "SAMBase.db"
            OutFile = sRunDir & "SAMBase.db"
            FileCopyCheck(InFile, OutFile)

            FileCopyCheck(sRunDir & "SAMBase.db", Session("FdrvRun") & "SAMBase.db")
        Else
            ErrMsg = "The file SAMBase.mdb must exist on the DATA subdirectory"
            ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
            ErrMsg = ErrMsg & "You may need to re-install IFs."
            Call FileCheck(PutDataFileDir & "SAMBase.mdb", ErrMsg, Session("Error"), Nothing)
            InFile = PutDataFileDir & "SAMBase.mdb"
            OutFile = sRunDir & "SAMBase.mdb"
            FileCopyCheck(InFile, OutFile)

            FileCopyCheck(sRunDir & "SAMBase.mdb", Session("FdrvRun") & "SAMBase.mdb")
        End If
        ErrMsg = ""



        'jrs 10/31/04
        ' check if run files for the web exist
        ' ------------------------------------
        Dim sIP As String
        Dim Files As DataTable
        sIP = Replace(Request.PhysicalApplicationPath, "\IFsWeb\", "\IFsInit.mdb")

        'jrs 2005/06/30
        If IsNothing(Session("VersionName")) Then
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/07/19 Changing SQLite opening mode
            SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r, IFsInit as i WHERE r.VersionName = i.Value AND i.Variable = 'VersionName' ORDER BY FileName", "", Files, "", True)
            'SetDataTablefromAccessJournal(sIP, "SELECT * FROM RunFileLoad as r, IFsInit as i WHERE r.VersionName = i.Value AND i.Variable = 'VersionName' ORDER BY FileName", "", Files)
        Else
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/07/19 Changing SQLite opening mode
            SetDataTablefromAccess(sIP, "SELECT * FROM RunFileLoad as r WHERE r.VersionName = """ & Session("VersionName") & """ ORDER BY FileName", "", Files, "", True)
            'SetDataTablefromAccessJournal(sIP, "SELECT * FROM RunFileLoad as r WHERE r.VersionName = """ & Session("VersionName") & """ ORDER BY FileName", "", Files)
        End If

        'jrs 2005/06/24
        Session("IFsRunFiles") = New MyFiles
        LoadRunFiles(Session("IFsRunFiles"), Session("PutFdrvRun2"), Files, Session("mnuSQLite"))

        'jrs 2005/02/09
        ReDim Session("ShortSce")(Files.Rows.Count + 1)
        Session("ShortSce")(0) = "Working"
        Session("ShortSce")(1) = "Base"
        'jrs 2005/02/25
        ReDim ShortSce(Files.Rows.Count - 1)
        ReDim LongSce(Files.Rows.Count - 1)

        Dim j As Integer
        Dim s As String
        For j = 0 To Files.Rows.Count - 1
            s = Files.Rows(j).Item("FileName")
            If Session("mnuSQLite") = "True" Then
                ErrMsg = "The file " & UCase(s) & ".RUN.DB must exist on the RUNFILES subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(sRunDir & s & ".run.db", ErrMsg, Session("Error"), Nothing)
                FileCopyCheck(sRunDir & s & ".run.db", Session("FdrvRun") & s & ".run.db")
            Else
                ErrMsg = "The file " & UCase(s) & ".RUN must exist on the RUNFILES subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(sRunDir & s & ".run", ErrMsg, Session("Error"), Nothing)
                FileCopyCheck(sRunDir & s & ".run", Session("FdrvRun") & s & ".run")
            End If
            ErrMsg = ""


            If Session("mnuSQLite") = "True" Then
                ErrMsg = "The file SAM" & UCase(s) & ".DB must exist on the RUNFILES subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(sRunDir & "SAM" & s & ".db", ErrMsg, Session("Error"), Nothing)
                ErrMsg = ""

                FileCopyCheck(sRunDir & "SAM" & s & ".db", Session("FdrvRun") & "SAM" & s & ".db")
            Else
                ErrMsg = "The file SAM" & UCase(s) & ".MDB must exist on the RUNFILES subdirectory"
                ErrMsg = ErrMsg & " of the directory from which you attempt to run IFS.  "
                ErrMsg = ErrMsg & "You may need to re-install IFs."
                Call FileCheck(sRunDir & "SAM" & s & ".mdb", ErrMsg, Session("Error"), Nothing)
                ErrMsg = ""

                FileCopyCheck(sRunDir & "SAM" & s & ".mdb", Session("FdrvRun") & "SAM" & s & ".mdb")
            End If
            'jrs 2005/02/09
            Session("ShortSce")(j + 2) = Files.Rows(j).Item("ShortName")
            'JRS 2005/02/25
            ShortSce(j) = Files.Rows(j).Item("ShortName")
            LongSce(j) = s
            'jrs 2005/06/24
            If Session("mnuSQLite") = "True" Then
                Session("IFsRunFiles").Item(s & ".run.db").Visible = True
            Else
                Session("IFsRunFiles").Item(s & ".run").Visible = True
            End If
        Next

        'jrs 2007/01/15
        '   Check if Provinces.mdb exists
        '   ----------------------------
        ErrMsg$ = "The file Provinces." & ext & " must exist on the DATA subdirectory"
        ErrMsg$ = ErrMsg$ & " of the directory from which you attempt to run IFS.  "
        ErrMsg$ = ErrMsg$ & "You may need to re-install IFs."
        Call FileCheck(FDRV$ + "DATA\Provinces." & ext, ErrMsg, Session("Error"), Nothing)
        ErrMsg$ = ""

        'cdv 2021/02/18 ****END****

        '   Copy TablFunc.MDB, IFs.MDB, IFsVar.MDB, DataDict.MDB, IFsPop.MDB
        '   to Working Location in local Runfiles directory (for network)
        '   -------------------------------------------------------------------------------

        On Error GoTo FileCopyError
        InFile = DataFileDir & "TablFunc." & ext
        OutFile = Session("PutFdrvRun") & "TablFunc." & ext
        FileCopyCheck(InFile, OutFile)

        InFile = DataFileDir & "AnalFunc." & ext
        OutFile = Session("PutFdrvRun") & "AnalFunc." & ext
        FileCopyCheck(InFile, OutFile)

        InFile = DataFileDir & "IFsVar." & ext
        OutFile = Session("PutFdrvRun") & "IFsVar." & ext
        FileCopyCheck(InFile, OutFile)

        If Session("mnuSQLite") = "True" Then
            InFile = DataFileDir & "IFsPop.db"
            OutFile = Session("PutFdrvRun") & "IFsPop.db"
        Else
            InFile = DataFileDir & "IFsPop.mdb"
            OutFile = Session("PutFdrvRun") & "IFsPop.mdb"
        End If

        FileCopyCheck(InFile, OutFile)
        InFile = DataFileDir & "DataDict." & ext
        OutFile = Session("PutFdrvRun") & "DataDict." & ext

        FileCopyCheck(InFile, OutFile)

        InFile = IFs_MDB
        OutFile = Session("IFs_MDB2")
        FileCopyCheck(InFile, OutFile)

        'jrs 2019/12/31
        If StandAloneMode Then
            InFile = InFile.Replace(".MDB", ".db")
            OutFile = OutFile.Replace(".MDB", ".db")
            FileCopyCheck(InFile, OutFile)
        End If



        'jrs 2022/07/19
        ''JRS 10/08/02 GUIDED USE
        'InFile = DataFileDir & "GuidedUse.mdb"
        'OutFile = Session("PutFdrvRun") & "GuidedUse.mdb"
        'FileCopyCheck(InFile, OutFile)

        'If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
        '    InFile = DataFileDir & "IFsCoVaTrA.MDB"
        '    OutFile = Session("PutFdrvRun") & "IFsCoVaTrA.MDB"
        '    FileCopyCheck(InFile, OutFile)
        'End If

        'jrs 2007/01/15
        InFile$ = DataFileDir$ + "Provinces." & ext
        OutFile$ = Session("PutFdrvRun") + "Provinces." & ext
        FileCopyCheck(InFile$, OutFile$)

        'jrs 2019/12/31
        If StandAloneMode Then
            InFile$ = DataFileDir$ + "ifsSmoking.db"
            OutFile$ = Session("PutFdrvRun") + "ifsSmoking.db"
            FileCopyCheck(InFile$, OutFile$)

            'jrs 2013/11/12
            InFile$ = DataFileDir$ & "VarListTransmission." & ext
            OutFile$ = Session("PutFdrvRun") & "VarListTransmission." & ext
            FileCopyCheck(InFile$, OutFile$)

            'jrs 2016/12/02
            InFile = DataFileDir$ + "ifsHistSeriesDyadic." & ext
            If Dir(InFile) > "" Then
                OutFile = Session("PutFdrvRun") + "ifsHistSeriesDyadic." & ext
                FileCopyCheck(InFile$, OutFile$)
            End If

            'JRS 2016/12/21
            InFile = DataFileDir$ + "IFsForDyadic.db"
            If Dir(InFile) > "" Then
                OutFile = Session("PutFdrvRun") + "IFsForDyadicWork.db"
                FileCopyCheck(InFile, OutFile)
            End If

        Else
            'jrs 2008/11/20
            InFile$ = DataFileDir$ + "ifsSmoking." & ext
            OutFile$ = Session("PutFdrvRun") + "ifsSmoking." & ext
            FileCopyCheck(InFile$, OutFile$)
        End If

        'jrs 2017/07/11
        InFile = DataFileDir$ + "IFsMDG." & ext
        If Dir(InFile) > "" Then
            OutFile = Session("PutFdrvRun") + "IFsMDG." & ext
            FileCopyCheck(InFile, OutFile)
        End If

START:
        GVAR = "" : GoalsBoolean = "FALSE" : VideoOkay = "TRUE"
        Static OP(9, 2) As Object
        Static GOP(5, 2) As Object
        Static GOALS(11, 5) As Object
        Static CMOP(3) As Object
        'UPGRADE_WARNING: Couldn't resolve default property of object REDCD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        REDCD = 0

        '   Open TablFunc.MDB and Fill Variable Description Rows(i).Item
        '   ----------------------------------------------------

        '    msg$ = "About to open TablFunc"
        '    MsgBox msg$
        On Error GoTo InitError
        msg = "There is an error in init, probably associated with opening TablFunc.mdb"

        InFile = DataFileDir & "TablFunc.mdb"

        '    InFile$ = PutFdrvRun$ + "TablFunc.mdb"
        '    msg$ = "InFile$ is " + InFile$
        '    MsgBox msg$
        'Following approach to database seems overly dependent on Access or DAOJR version
        'May require Access 2000 to install properly on all machines
        '    Dim db6 As Database
        '    msg$ = "Have dimensioned db6"
        '    MsgBox msg$

        '    Dim valtbl6 As Recordset
        '    msg$ = "Have dimensioned valtbl6"
        '    MsgBox msg$

        '    Set db6 = DBEngine.OpenDatabase(InFile$)
        '    msg$ = "Have opened TablFunc"
        '    MsgBox msg$

        '   Compute Number of Variables in TablFunc.mdb
        '   -----------------------------------------

        '    Set valtbl6 = db6.OpenRecordset("Select count(*) As Expr1 from TablFunc")
        '    TFCount = valtbl6.Rows(i).Item("Expr1")
        '    TFCountFull = TFCount
        '    msg$ = "Opened Recordset"
        '    MsgBox msg$
        '    Msg$ = "About to open TablFunc Database"
        '    MsgBox Msg$

        'Important Note to Save:  True is read-only, to override the False (read-write)
        '  default
        '(InFile$, , True) does not work, even though False is supposedly the default option
        '   for database access (meaning shared rather than exclusive)
        'That is, it seems necessary to spell out both options to allow read-only access

        '    Msg$ = "About to open TablFunc Recordset"
        '    MsgBox Msg$
        'UPGRADE_WARNING: Couldn't resolve default property of object db6.OpenRecordset. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'

        'jam 2025/10/30 Unifying Database opening mode
        'jam 2025/07/19 Changing SQLite opening mode
        SetDataTablefromAccess(InFile, "SELECT * FROM TablFunc", "", valtbl6, "", True)
        'SetDataTablefromAccessJournal(InFile, "SELECT * FROM TablFunc", "", valtbl6)

        '    Msg$ = "Have opened TablFunc"
        '    MsgBox Msg$
        'UPGRADE_WARNING: Couldn't resolve default property of object eleloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        eleloop = valtbl6.Rows.Count
        'UPGRADE_WARNING: Couldn't resolve default property of object eleloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TFCount = eleloop 'May later be reduced to eliminate CoVaTrA functions
        'UPGRADE_WARNING: Couldn't resolve default property of object TFCountFull. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("TFCountFull") = TFCount 'Includes CoVaTrA Functions
        '    Msg$ = "Have closed TablFunc"
        '    MsgBox Msg$

        '   Read and Process TablFunc.mdb
        '   ---------------------------

        'JRS 8/17/01 GUI ReDim CoVaTrAFun$(TFCount), Session("Category")$(TFCount), UsedRun$(TFCount), UsedHisRun$(TFCount)
        ReDim FuncName(TFCount)
        ReDim XVariable(TFCount)
        ReDim YVariable(TFCount)
        ReDim Analytic(TFCount)
        ReDim Session("UsedRun")(TFCount)
        ReDim Session("UsedHisRun")(TFCount)
        'jrs 2013/11/18
        ReDim Session("UsedPre")(TFCount)
        ReDim Session("UsedPreHis")(TFCount)
        ReDim Session("TFuncR2")(TFCount)
        ReDim Session("TFuncSE")(TFCount)

        ReDim Session("CoVaTrAFun")(TFCount)
        ReDim Session("Category")(TFCount)
        ReDim Session("BPoints")(TFCount, 9) 'JAM 2009-09-12 Changing BPoints Second Dimension from 8 to 9
        ReDim Session("XPoints")(TFCount, 10)
        ReDim Session("YPoints")(TFCount, 10)
        ReDim Session("TFuncPoints")(TFCount)
        ReDim Session("APoint")(TFCount)
        ReDim Session("MinPoint")(TFCount)
        ReDim Session("MaxPoint")(TFCount)

        '    Set valtbl6 = db6.OpenRecordset("Select * from TablFunc")
        '    msg$ = "Selected"
        '    MsgBox msg$
        '    valtbl6.MoveFirst
        '    msg$ = "Moved"
        '    MsgBox msg$

        'jam 2025/10/30 Unifying Database opening mode
        'jam 2025/07/19 Changing SQLite opening mode
        SetDataTablefromAccess(InFile, "SELECT * FROM TablFunc ORDER BY [Function Name]", "", valtbl6, "", True)
        'SetDataTablefromAccessJournal(InFile, "SELECT * FROM TablFunc ORDER BY [Function Name]", "", valtbl6)

        Dim i As Integer
        i = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        tfrecount = 0
        'jrs 2025/10/31
        Dim bContainsStdDev As Boolean
        bContainsStdDev = valtbl6.Columns.Contains("StdDev")
        Dim bContainsb9 As Boolean
        bContainsb9 = valtbl6.Columns.Contains("b9")
        Dim CoeffTotal As Integer
        If bContainsb9 Then
            CoeffTotal = 9
        Else
            CoeffTotal = 8
        End If

        For TLoop = 1 To TFCount 'Probably should replace this with logic that separates
            'Debug.Print(TLoop.ToString)
            'If TLoop = 49 Then Stop
            'CoVaTrA table functions into separate file
            'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or valtbl6.Rows(i).Item("CoVaTrA") = "No" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                tfrecount = tfrecount + 1
                'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                FuncName(tfrecount) = valtbl6.Rows(i).Item("Function Name")
                Session("UsedRun")(tfrecount) = valtbl6.Rows(i).Item("UsedRun")
                Session("UsedHisRun")(tfrecount) = valtbl6.Rows(i).Item("UsedHisRun")
                'jrs 2013/11/18
                Session("UsedPre")(tfrecount) = valtbl6.Rows(i).Item("UsedPre")
                Session("UsedPreHis")(tfrecount) = valtbl6.Rows(i)("UsedHisPre")
                Session("TFuncR2")(tfrecount) = valtbl6.Rows(i)("RSquared")
                'jrs 2025/10/31 to handle older databases without StdDev field
                If bContainsStdDev Then
                    Session("TFuncSE")(tfrecount) = valtbl6.Rows(i)("StdDev")
                End If

                Session("CoVaTrAFun")(tfrecount) = valtbl6.Rows(i).Item("CoVaTrA")
                'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                XVariable(tfrecount) = valtbl6.Rows(i).Item("XVariable")
                'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                YVariable(tfrecount) = valtbl6.Rows(i).Item("YVariable")
                'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object valtbl6.Rows(i).Item. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Analytic(tfrecount) = valtbl6.Rows(i).Item("Analytic")
                If Not (IsDBNull(valtbl6.Rows(i).Item("Category"))) Then
                    Session("Category")(tfrecount) = valtbl6.Rows(i).Item("Category")
                Else
                    Session("Category")(tfrecount) = ""
                End If
                For iloop = 1 To CoeffTotal 'JAM 2009-09-12 Changing BPoints Second Dimension from 8 to 9
                    VarName = Trim("b" & Trim(Str(iloop)))
                    Session("BPoints")(tfrecount, iloop) = valtbl6.Rows(i).Item(VarName)
                    If IsDBNull(Session("BPoints")(tfrecount, iloop)) Then Session("BPoints")(tfrecount, iloop) = 0
                Next iloop
                Session("APoint")(tfrecount) = valtbl6.Rows(i).Item("a")
                If IsDBNull(Session("APoint")(tfrecount)) Then Session("APoint")(tfrecount) = 0
                Session("MinPoint")(tfrecount) = valtbl6.Rows(i).Item("Minimum")
                Session("MaxPoint")(tfrecount) = valtbl6.Rows(i).Item("Maximum")
                Session("TFuncPoints")(tfrecount) = valtbl6.Rows(i).Item("TFuncPoints")
                For iloop = 1 To 10
                    VarName = Trim("X" & Trim(Str(iloop)))
                    Session("XPoints")(tfrecount, iloop) = valtbl6.Rows(i).Item(VarName)
                Next iloop
                For iloop = 1 To 10
                    VarName = Trim("Y" & Trim(Str(iloop)))
                    Session("YPoints")(tfrecount, iloop) = valtbl6.Rows(i).Item(VarName)
                Next iloop
            End If
            i = i + 1
        Next TLoop
        'UPGRADE_WARNING: Couldn't resolve default property of object tfrecount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TFCount = tfrecount 'May be reduced by CoVaTrA functions
        Session("FuncInd") = 0 'Set for entrance into frm_TablFunc
        '    msg$ = "Have closed TablFunc"
        '    MsgBox msg$

        '   On Network, copy IFS.GRP to local runfiles directory
        '   ---------------------------------------------------
        '    On Error GoTo FileCopyError
        '    InFile$ = DataFileDir$ + "IFS.GRP"
        '    OutFile$ = PutDataFileDir$ + "IFS.GRP"
        '    If DataFileDir$ <> PutDataFileDir$ Then FileCopy InFile$, OutFile$

        '   Dimension dynamic display arrays
        '   --------------------------------
        actreg = MaxReg + 1
        'JRS 8/17/01 GUI ReDim RegNamePop$(actreg%), RegNumPop(actreg%)
        ReDim RegName(actreg)
        'JRSVBNET ReDim RegNum(actreg)
        ReDim RegMembs(actreg)
        ReDim RegHold(actreg)
        ReDim RegWeight(actreg)
        'JRSVBNET ReDim salphas(actreg, NSec)

        '   Compute Maximum Valid and Read Dimension Session("Category") Labels, Except Regions
        '   ------------------------------------------------------------------------
        'jrs 2005/02/12
        Dim fNum As Integer
        fNum = FreeFile()
        msg = "There is an error in init, probably associated with opening IFs.GRP"
        Debug.WriteLine("IFsInit Before fileOpen")

        FileOpen(fNum, DataFileDir & "IFS.GRP", OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
        Input(fNum, KNT) 'Read number of dimensions; correspond to lines in .grp and to
        'specifications for each variable in IFsVar.mdb
        'Have removed former region line from top of file; so have KNT-1 dimensions in file
        ReDim MaxVal(KNT)
        ReDim MAXCOM(KNT)
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MaxVal(1) = MaxReg + 1 'Add room for "ALL"
        'UPGRADE_WARNING: Couldn't resolve default property of object MAXCOM(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MAXCOM(1) = MaxReg + 1
        'UPGRADE_WARNING: Couldn't resolve default property of object AMAX(20, MaxVal(1)). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ReDim GRPS(KNT + 1, AMAX(20, MaxVal(1))) 'jrs 2021/05/28 for compatibility
        'jrs 2025/10/06
        ExtraRegionSetsLoaded = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object KNT. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'jrs 2013/10/01
        GDimsCount = 0
        For iloop = 2 To KNT 'Loop from 2 because have KNT-1 dimensions in file
            'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Input(fNum, MaxVal(iloop))
            '        If ILOOP = 1 Then
            '            MaxReg% = MaxVal(ILOOP) - 1
            '            ReDim GRPS$(KNT, MaxReg% + 1)
            '        End If
            'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MAXCOM(iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            MAXCOM(iloop) = MaxVal(iloop)
            'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(iloop). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For gloop = 1 To MaxVal(iloop)
                'UPGRADE_WARNING: Couldn't resolve default property of object gloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object iloop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Input(fNum, GRPS(iloop, gloop))
                '           Skip empty spots in region/country vector
                '            If UCase$(GRPS$(ILOOP, gloop)) = "EMPTY1" Then
                '                Line Input #1, Dummy$
                '                MaxVal(ILOOP) = gloop
                '                GRPS$(ILOOP, gloop) = "ALL"
                '            Exit For
                '            End If
            Next gloop
            'jrs 2013/10/01
            GDimsCount = GDimsCount + MaxVal(iloop) - 1
            'jrs 2016/01/11 used in Flex Displays
            If GRPS(iloop, gloop - 2) <> "Total" Then
                GDimsCount = GDimsCount + 1
            End If
        Next iloop
        Debug.WriteLine("IFsInit Before fileClose")
        FileClose(fNum)

        '   Read Region Names and Determine Number of Regions
        '   -------------------------------------------------

        regcount = 0
        msg = "There is an error in init, probably associated with opening IFs.mdb"
        'mtiwebforms April 2004 ADO.Net
        'db = DAODBEngine_definst.OpenDatabase(IFs_MDB2)
        'valtbl = db.OpenRecordset(RegionIDNew)
        Dim db1 As String
        Dim valtbl1 As DataTable
        Dim RCnt As Integer

        db1 = Session("IFs_MDB2")

        'jrs 2011/04/01 SetDataTablefromAccess(db1, "Select * from " + RegionIDNew, "PrimaryKey", valtbl1)
        Dim sISO As String
        'jrs 2011/08/02 added ESRI
        Dim sESRI As String
        'jrs 2022/06/23

        'jam 2025/10/30 Unifying Database opening mode
        'jam 2025/07/19 Changing Sqlite opening mode
        SetDataTablefromAccess(db1, "Select * FROM [Country Translation]", "", valtbl1, "", True)
        'SetDataTablefromAccessJournal(db1, "Select * FROM [Country Translation]", "", valtbl1)


        Dim bISO As Boolean = False
        bISO = valtbl1.Columns.Contains("ISO3166-2 Codes")

        If bISO Then
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/07/19 Changing Sqlite opening mode
            SetDataTablefromAccess(db1, "Select *, (SELECT MAX([ISO3166-2 Codes]) FROM [Country Translation] c, " &
            Regionalization & " r WHERE c.Country = r.Country AND r.NewRegion = RegionNum) as ISO, " &
            "(SELECT MAX([ESRI Countries]) FROM [Country Translation] c, " &
            Regionalization & " r WHERE c.Country = r.Country AND r.NewRegion = RegionNum) as ESRI FROM " & RegionIDNew &
            " ORDER BY RegionNum", "", valtbl1, "", True)

            '    SetDataTablefromAccessJournal(db1, "Select *, (SELECT MAX([ISO3166-2 Codes]) FROM [Country Translation] c, " &
            'Regionalization & " r WHERE c.Country = r.Country AND r.NewRegion = RegionNum) as ISO, " &
            '"(SELECT MAX([ESRI Countries]) FROM [Country Translation] c, " &
            'Regionalization & " r WHERE c.Country = r.Country AND r.NewRegion = RegionNum) as ESRI FROM " & RegionIDNew &
            '" ORDER BY RegionNum", "", valtbl1)

        Else
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/07/19 Changing Sqlite opening mode
            SetDataTablefromAccess(db1, "Select * FROM " & RegionIDNew &
        " ORDER BY RegionNum", "", valtbl1, "", True)

            '     SetDataTablefromAccessJournal(db1, "Select * FROM " & RegionIDNew &
            '" ORDER BY RegionNum", "", valtbl1)

        End If

        'jrs 2011/11/25
        'Dim valtblCent As DataTable, CentRow As DataRow
        'SetDataTablefromAccess(db1, "Select * From [Country Google]", "country", valtblCent)

        'valtbl.Index = "PrimaryKey"
        'If Not valtbl.BOF Then
        '    valtbl.MoveFirst()
        '    Do
        '        regcount = regcount + 1
        '        GRPS(1, regcount) = valtbl.Rows(i).Item("regionname")
        '        RegName(regcount) = GRPS(1, regcount)
        '        valtbl.MoveNext()
        '        If valtbl.EOF = True Then Exit Do
        '    Loop
        'End If
        'valtbl.Close()
        'db.Close()
        'can also use datareader
        Debug.WriteLine("IFSInit before region loop")

        For RCnt = 0 To valtbl1.Rows.Count - 1
            'valtbl.MoveFirst()
            'Do
            regcount = regcount + 1
            GRPS(1, regcount) = valtbl1.Rows(RCnt).Item("regionname")
            RegName(regcount) = GRPS(1, regcount)
            'jrs 2022/06/23
            If bISO Then
                'jrs 2011/04/01
                sISO = IIf(IsDBNull(valtbl1.Rows(RCnt).Item("iso")), "", valtbl1.Rows(RCnt).Item("iso"))
                'jrs 2011/08/02
                sESRI = IIf(IsDBNull(valtbl1.Rows(RCnt).Item("ESRI")), "", valtbl1.Rows(RCnt).Item("ESRI"))
                'jrs 2026/01/12
                If GRPS(1, regcount) = Session("DefRegion") Then
                    Session("MMCountry") = sISO
                End If
            Else
                sISO = ""
                sESRI = ""
            End If

            'jrs 2024/03/07
            If IFsRegs.Count < regcount Then
                'jrs 2022/06/15 added for compatibility with old code (version 7.06)
                If (valtbl1.Columns.Contains("regionalias")) Then
                    'jrs 2022/03/28 added iif(isDBNULL
                    'jrs 2005/04/07
                    IFsRegs.Add(RegName$(regcount), regcount, sISO, sESRI, IIf(IsDBNull(valtbl1.Rows(RCnt).Item("regionalias")), "", valtbl1.Rows(RCnt).Item("regionalias"))) 'jrs 2016/05/27
                Else
                    IFsRegs.Add(RegName$(regcount), regcount, sISO, sESRI, "") 'jrs 2016/05/27
                End If
            End If
            'valtbl.MoveNext()
            'If valtbl.EOF = True Then Exit Do
            'Loop
            'jrs 2011/11/25
            'CentRow = SeekRow(valtblCent, sISO)
            'If Not (CentRow Is Nothing) Then
            '    IFsRegs.setCentroid(RegName$(regcount), CentRow.Item("longitude"), CentRow.Item("latitude"))
            '    IFsRegs.setZoom(RegName$(regcount), CentRow.Item("zoomlevel"))
            'End If
        Next
        Debug.WriteLine("IFsInit after region loop")

        'valtbl.Close()
        'db.Close()
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MaxVal(1) = regcount + 1
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        GRPS(1, MaxVal(1)) = "ALL"

        Debug.WriteLine("IFsInit Before countries")
        'jrs 2024/03/07
        If IFsCtrys.Count = 0 Then
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/07/19 Changing Sqlite opening mode
            'jrs 2011/09/29
            SetDataTablefromAccess(db1, "SELECT * FROM Economic ORDER BY country", "", valtbl1, "", True)
            'SetDataTablefromAccessJournal(db1, "SELECT * FROM Economic ORDER BY country", "", valtbl1)

            For RCnt = 0 To valtbl1.Rows.Count - 1
                'jrs 2012/05/01 added + 1 so that the index starts at 1
                IFsCtrys.Add(valtbl1.Rows(RCnt).Item("Country"), RCnt + 1, "", "", "") 'jrs 2016/05/27
            Next RCnt
        End If

        '   Now that know maximum region numbers, dimension dynamic display arrays
        '   ----------------------------------------------------------------------

        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        NR = MaxVal(1) - 1 'Recomputed in FRM.RUN Load
        'jrs 2013/10/01
        GDimsCount = GDimsCount + NR
        'MaxVariables = AMAX(500, MaxReg * 3) 'Sets number of slots in the grid that saves variable
        'jrs 2021/02/19 MaxVariables = AMAX(750, MaxReg * 5) 'Sets number of slots in the grid that saves variable
        'jrs 2024/11/25
        'If StandAloneMode Then
        '    'This produces out of memory errors when trying to Rank, even if you haven't loaded much
        '    'We're making lots of backup copies of arrays when Ranking
        '    'I tried this with my 64 GB of RAM in my machine, maybe in a powerful server
        '    MaxVariables = CInt(NR) * CInt(NR) * 2 + 1
        'Else
        '    MaxVariables = AMAX(750, MaxReg * 10) 'Sets number of slots in the grid that saves variable
        'End If
        MaxVariables = AMAX(750, MaxReg * 10) 'Sets number of slots in the grid that saves variable

        Debug.WriteLine("IFsInit Before UpdateRunFiles")

        'jrs 2021/05/31
        If Session("mnuSQLite") = "True" Then
            UpdateRunFiles(Session("IFsRunFiles"), Session("PutFdrvRun2"))
        End If

        'names for display
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxYears. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'jrs 2015/06/05 MaxYears = 300 'jrs 2005/06/29 200
        MaxYears = 305
        ReDim Session("FileNumbr")(MaxVariables)
        ReDim Session("VARN")(6, MaxVariables)
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ReDim Session("GR1N")(MaxVariables + 1)
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ReDim Session("GR2N")(MaxVariables + 1)
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ReDim Session("GR3N")(MaxVariables + 1)
        ReDim Session("GR4N")(MaxVariables + 1) 'jrs 2006/06/16
        ReDim Session("PLOT")(MaxYears, MaxVariables)
        ReDim Session("GeoRegion")(MaxVariables), Session("GeoValue")(MaxVariables)
        ReDim Session("FinYear")(MaxVariables)
        'jrs 2006/09/25
        ReDim Session("FinYearHis")(MaxVariables)
        'jrs 2015/03/02
        ReDim Session("StaYearHis")(MaxVariables)
        ReDim Session("VL")(MaxVariables)
        'jrs 2006/11/20
        ReDim Session("sUsrDefCats")(MaxVariables)
        'JRS 8/17/01 GUI ReDim MaxValC(MaxVariables), MinValC(MaxVariables), DiffValC(MaxVariables) 'Used in Common Display
        ReDim Session("PLOTComp")(MaxYears)
        ReDim Session("PLOTAll")(MaxYears, MaxVariables)
        ReDim Preserve Session("aFormulas")(1)
        Session("bComputation") = False

        'jrs 10/27/04 Flexible Dimension Names for Graphs
        ReDim Session("FDNG1")(MaxVariables + 1)
        ReDim Session("FDNG2")(MaxVariables + 1)
        ReDim Session("FDNG3")(MaxVariables + 1)
        ReDim Session("FDNG4")(MaxVariables + 1) 'jrs 2006/06/16

        '   Read Maximum in COMMON
        '   ----------------------
        '    Open PutDataFileDir$ + "IFS.GRP" For Input As #1
        '    Input #1, KNT
        '    For ILOOP = 1 To KNT
        '        Input #1, MAXCOM(ILOOP)
        '        Line Input #1, Dummy$
        '    Next ILOOP
        '    Close #1

        '   Read Region Names
        '   ----------------------
        '    Open PutDataFileDir$ + "IFS.GRP" For Input As #1
        '    Input #1, KNT
        '    Input #1, MAXCOM(1)
        '    For ILOOP = 1 To actreg%
        '        Input #1, RegName$(ILOOP)
        '    Next ILOOP
        '    Close #1

        '   Read Country/Region Groupings
        '   -----------------------------
        Debug.WriteLine("IFsInit Before country/region group")

        msg = "There is an error in init, probably associated with opening IFs.mdb, 2nd"
        'JRS 8/16/01 GUI Set db = OpenDatabase(IFs_MDB2)
        '    Dim valtbl As Recordset

        Dim valtbl As DataTable
        'jrs 2022/07/19
        If UseOnlySQLite Then
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/07/19 Changing Sqlite opening mode
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " ORDER BY UPPER(GroupName)", "", valtbl, "", True)
            'SetDataTablefromAccessJournal(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " ORDER BY UPPER(GroupName)", "", valtbl)

        Else
            'jam 2025/10/30 Unifying Database opening mode
            'jam 2025/07/19 Changing Sqlite opening mode
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "", valtbl, "", True)
            'SetDataTablefromAccessJournal(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " ORDER BY UPPER(GroupName)", "", valtbl)

        End If
        'jrs 2008/12/08
        ReDim Preserve GroupNames(valtbl.Rows.Count + 1)
        ReDim Preserve GroupCodes(valtbl.Rows.Count + 1)

        Debug.WriteLine("IFsInit Before grouping loop")
        eleloop = 0
        Do
            eleloop = eleloop + 1
            GroupNames(eleloop) = valtbl.Rows(eleloop - 1).Item("GroupName")
            GroupCodes(eleloop) = valtbl.Rows(eleloop - 1).Item("GroupNum")
            If eleloop = valtbl.Rows.Count Then Exit Do
        Loop
        GCount = eleloop
        GroupNames(eleloop + 1) = "ALL"
        'jrs 2023/05/23 added UPPER (for sqlite, non-oecd was showing up at the bottom)
        'cdv 2021/08/20
        'jam 2025/07/19 Changing Sqlite opening mode
        'jam 2025/10/30 Unifying Database opening mode
        SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " WHERE Core = TRUE ORDER BY UPPER(GroupName)", "", valtbl, "", True)
        'SetDataTablefromAccessJournal(Session("IFs_MDB2"), "SELECT * FROM " & GroupIDNew & " WHERE Core = TRUE ORDER BY UPPER(GroupName)", "", valtbl)


        'jrs 2022/05/31 added for compatibility with old files
        If Not IsNothing(valtbl) Then
            ReDim Preserve CoreGroupNames(valtbl.Rows.Count + 1)
            eleloop = 0
            Do
                eleloop += 1
                CoreGroupNames(eleloop) = valtbl.Rows(eleloop - 1).Item("GroupName")
                If eleloop = valtbl.Rows.Count Then Exit Do
            Loop
            CGCount = eleloop
            CoreGroupNames(eleloop + 1) = "ALL"
        End If
        'jrs 2011/09/29
        ReDim Session("GroupingColor")(GCount + 1)
        'jam 2025/10/30 Unifying Database opening mode
        'jrs 2011/02/10
        'jam 2025/07/19 Changing Sqlite opening mode
        SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT * FROM GListId ORDER BY Description", "", valtbl, "", True)
        'SetDataTablefromAccessJournal(Session("IFs_MDB2"), "SELECT * FROM GListId ORDER BY Description", "", valtbl)

        ReDim Preserve GListNames(valtbl.Rows.Count + 1)

        Debug.WriteLine("IFsInit Before Description loop")

        eleloop = 0
        Do
            eleloop = eleloop + 1
            GListNames(eleloop) = CType(valtbl.Rows(eleloop - 1).Item("Description"), String)
            If eleloop = valtbl.Rows.Count Then Exit Do
        Loop
        GListCount = eleloop

        Debug.WriteLine("IFsInit Before IFsVarIni")

        'JRS 9/5/01 MOVING THE CODE TO A MODULE
        IFsVarIni(Session("PutFdrvRun"), Session("VDisplayUnitsName"))

        Debug.WriteLine("IFsInit Before file copying")

        'NOTE:  Following copying of IFS.MDB and IFsHistSeries to local
        '   runfiles directory was to avoid protections against writing
        '   to those files on network server.  Given movement of Access
        '   away from .ldb files, is that still necessary?  They take
        '   much room on local machines, and that can be a problem
        '   in labs.
        '   I am not copying DataDict.MDB to Runfiles before use in cross-
        '   sectional analysis;  perhaps that could serve as test on a server

        '  Copy data files to local on network
        '       - so that .LDB read/write not a problem
        '   -------------------------------------------
        On Error GoTo FileCopyError

        Session("HistoricBase") = CStr(False) 'Default to forecasts
        Session("ActiveBase") = "IFSBASE.RUN"
        Session("IFs_MDB2Active") = Session("IFs_MDB2")

        InFile = IFsHist_MDB
        OutFile = Session("IFsHist_MDB2")
        FileCopyCheck(InFile, OutFile)

        'If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
        InFile = IFsCoVaTrA_MDB
        OutFile = Session("IFsCoVaTrA_MDB2")
        FileCopyCheck(InFile, OutFile)
        'End If

        '   Copy HistSeries File to Working Location in local Runfiles directory
        '   --------------------------------------------------------------------
        'jrs 2022/07/20
        'If UseOnlySQLite Then
        'Dim conn As SqliteConnection
        'conn = New SqliteConnection("Data Source=" & DataFileDir & "IFsHistSeries." & ext & ";Version=3;")
        'conn.SetPassword("password")

        'Dim connectionString = New SqliteConnectionStringBuilder("Data Source=" & DataFileDir & "IFsHistSeries." & ext & ";Version=3;") With {
        '    .Mode = SqliteOpenMode.ReadWriteCreate,
        '    .Password = password
        '}.ToString()
        'End If

        InFile = "IFsHistSeries_MDB" 'In case of error, need for error message
        OutFile = "IFsHistSeries_MDB"
        FileCopyCheck(DataFileDir & "IFsHistSeries." & ext, Session("PutFdrvRun") & "IFsHistSeries." & ext)
        Session("IFsHistSeries_MDB") = Session("PutFdrvRun") & "IFsHistSeries." & ext
        Session("IFsCoVaTrASeries_MDB") = Session("PutFdrvRun") & "IFsCoVaTrASeries." & ext

        '    Set db = OpenDatabase(IFsHistSeries_MDB, False, True, DataPW$)

        'If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then
        InFile = "IFsCoVaTrASeries_MDB"
        OutFile = "IFsCoVaTrASeries_MDB"
        FileCopyCheck(DataFileDir & "IFsCoVaTrASeries." & ext, Session("PutFdrvRun") & "IFsCoVaTrASeries." & ext)
        'End If

        '   Copy Cohort File to Working Location in local Runfiles directory
        '   --------------------------------------------------------------------

        InFile = "IFsWVSCohort_MDB"
        OutFile = "IFsWVSCohort_MDB"
        FileCopyCheck(DataFileDir & "IFsWVSCohort." & ext, Session("PutFdrvRun") & "IFsWVSCohort." & ext)

        'mti WebForm Changes to be taken by Jose
        'Session("IFsHistSeries_MDB") = Session("PutFdrvRun") & "IFsWVSCohort.mdb"
        Session("IFsWVSCohort_MDB") = Session("PutFdrvRun") & "IFsWVSCohort." & ext

        '   Copy Pop Base Case to Work Case to Hold Cohort Data
        '   ---------------------------------------------------

        InFile = "IFsPopWork_MDB"
        OutFile = "IFsPopWork_MDB"
        'jrs 2006/09/15 FileCopyCheck(DataFileDir & "IFsPopWork.mdb", Session("PutFdrvRun") & "IFsPopWork.mdb")
        If Session("mnuSQLite") = "True" Then
            FileCopyCheck(DataFileDir & "IFsPop.db", Session("PutFdrvRun") & "IFsPopWork.db")
        Else
            FileCopyCheck(DataFileDir & "IFsPop.mdb", Session("PutFdrvRun") & "IFsPopWork.mdb")
        End If

        '   Copy Statistics File to Working Location in local Runfiles directory
        '   --------------------------------------------------------------------
        'jrs 2022/07/19
        'InFile = "IFsStat_MDB"
        'OutFile = "IFsStat_MDB"
        'FileCopyCheck(DataFileDir & "IFsStat.mdb", Session("PutFdrvRun") & "IFsStat.mdb")
        'Session("IFsStat_MDB") = Session("PutFdrvRun") & "IFsStat.mdb"

        '   On Network, copy IFSBASE.RUN to local directory
        '   -----------------------------------------------
        'jrs 2022/07/19
        'InFile = Session("FdrvRun") & Basefile & ".RUN"
        'OutFile = Session("PutFdrvRun") & Basefile & ".RUN"
        'If Session("FdrvRun") <> Session("PutFdrvRun") Then
        '    FileCopyCheck(InFile, OutFile)
        'End If


        '   On Network, copy IFSBASEHIST.RUN to local directory
        '   -----------------------------------------------
        'jrs 2022/07/19
        'If ModelLoad <> "LoadStudent" And CBool(DevelopmentMode) = False Then 'Historical run not available for students
        '    InFile = Session("FdrvRun") & Basefile & "HIST.RUN"
        '    OutFile = Session("PutFdrvRun") & Basefile & "HIST.RUN"
        '    If Session("FdrvRun") <> Session("PutFdrvRun") Then
        '        FileCopyCheck(InFile, OutFile)
        '    End If
        'End If

        '   Copy IFSBASE.RUN to working.run
        '   -----------------------------
        'cdv 2021/02/18 Flip what in and out file should be based off msnuSQLite ****START****
        If Session("mnuSQLite") = "True" Then
            InFile = Session("PutFdrvRun") & Basefile & ".run.db"
            OutFile = Session("PutFdrvRun") & "working.run.db"
        Else
            InFile = Session("PutFdrvRun") & Basefile & ".run"
            OutFile = Session("PutFdrvRun") & "working.run"
        End If
        FileCopyCheck(InFile, OutFile)
        If Session("mnuSQLite") = "True" Then
            Session("WorkingFile") = Session("PutFdrvRun") & "IFsBase.run.db"
        Else
            Session("WorkingFile") = Session("PutFdrvRun") & "IFsBase.run"
        End If

        'JRS 10/16/02 SAM COPY
        '   Copy SAMBASE.MDB to SAMWORKING.MDB
        '   -----------------------------
        If Session("mnuSQLite") = "True" Then
            InFile = PutDataFileDir & "SAMBase.db"
            OutFile = Session("PutFdrvRun") & "SAMWorking.db"
        Else
            InFile = PutDataFileDir & "SAMBase.mdb"
            OutFile = Session("PutFdrvRun") & "SAMWorking.mdb"
        End If
        FileCopyCheck(InFile, OutFile)

        '   Establish Files Selected
        '   ------------------------
        Session("FilesSelected") = 1
        ReDim Session("RunFiles")(10)

        If Session("mnuSQLite") = "True" Then
            Session("RunFiles")(1) = Session("PutFdrvRun") & "IFsBase.run.db"
        Else
            Session("RunFiles")(1) = Session("PutFdrvRun") & "IFsBase.run"
        End If
        'cdv 2021/02/18 ****END****
        '   Table Title
        '   -----------
        'JRS 8/17/01 GUI Session("TableTitle") = "Title"

        '   Redifining Size of Buffer
        '   -------------------------
        'JRS 8/20/01
        Static Y As Short
        'UPGRADE_WARNING: Couldn't resolve default property of object filen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        Y = filen Mod 5000
        ReDim FILEBUF3.elem3(Y)

        '   Defining iGDP and iPOP
        '   ----------------------
        'JRS 2/9/02
        iGDP = IfsVars.Item("GDP").Index
        iPOP = IfsVars.Item("POP").Index

        'mti WebForm Changes From June 21, 2004 -start - variable no for run graph variables
        iWGDP = IfsVars.Item("WGDP").Index
        iWPOP = IfsVars.Item("WPOP").Index
        iWFORST = IfsVars.Item("WFORST").Index
        iCO2PPM = IfsVars.Item("CO2PPM").Index
        'mti WebForm Changes From June 21, 2004 - end

        Session("Fil_Min_Level") = 1
        Session("Fil_Max_Level") = 1
        Session("Fil_Rate") = 10
        Session("Fil_Range_Min") = 1
        Session("Fil_Range_Max") = 10
        'jrs 2012/01/16
        Session("bFiltLoadedValues") = True
        'Session("bFiltDisplayCount") = False

        POPidx = IfsVars.Item("POP").Index
        GDPidx = IfsVars.Item("GDP").Index

        'jrs 2009/01/23
        For iloop = 2 To UBound(GRPS, 1)
            If UCase(GRPS(iloop, 1)) = "MALE" Then
                GenderIdx = iloop
            ElseIf UCase(GRPS(iloop, 1)) = "AGE18_24" Then
                Age18_65Idx = iloop
            ElseIf UCase(GRPS(iloop, 1)) = "AGE30_44" Then
                Age30_70Idx = iloop
            ElseIf UCase(GRPS(iloop, 1)) = "AGE15_29" Then
                Age15_80Idx = iloop
            End If
        Next iloop

        'jam 2025/10/30 Unifying Database opening mode
        'jrs 2005/12/09
        'jam 2025/07/19 Changing Sqlite opening mode
        SetDataTablefromAccess(Session("FdrvRun") & "IFS.MDB", "SELECT * FROM [Currency] WHERE [Currency Flag] = TRUE", "", rsCurr, "", True)
        'SetDataTablefromAccessJournal(Session("FdrvRun") & "IFS.MDB", "SELECT * FROM [Currency] WHERE [Currency Flag] = TRUE", "", rsCurr)

        'jrs 2006/06/20
        Session("bFillHis") = False
        Session("gHistoricAnalog") = False
        'jrs 2006/07/21
        Session("FarmFlag") = False
        Session("FarmFlagHist") = False
        'jrs 2006/09/25
        Session("buseAllHis") = False
        'jrs 2007/11/30
        Session("DisplayTime") = 1

        Exit Sub

InitError:
        _logger.Error(Err.GetException(), "IFSInit - InitError")
        'jrs 2005/02/12
        msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
        Session("Error") = msg
        ' Clear error.
        Server.ClearError()
        ' Transfer back to this page.
        Server.Transfer("ErrorHandler.aspx")
        Exit Sub 'jam 2026/04/03 Catching error in proper exception


InitFileReadError:
        _logger.Error(Err.GetException(), "IFSInit - InitFileReadError")
        msg = "There is an error in init, probably a missing file."
        'jrs 2005/02/12
        msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description & Err.GetException.StackTrace
        Session("Error") = msg
        ' Clear error.
        Server.ClearError()
        ' Transfer back to this page.
        Server.Transfer("ErrorHandler.aspx")
        Exit Sub 'jam 2026/04/03 Catching error in proper exception

FileCopyError:
        _logger.Error(Err.GetException(), "IFSInit - FileCopyError")
        msg = "An error has arisen, probably during an attempt to copy "
        msg = msg & InFile & " to " & OutFile & NL
        msg = msg & "Check existence of source file and space availability."
        'jrs 2005/02/12
        msg = msg & "<br><br><br><b>Error Description:</b> " & Err.Description
        Session("Error") = msg
        ' Clear error.
        Server.ClearError()
        ' Transfer back to this page.
        Server.Transfer("ErrorHandler.aspx")
        Exit Sub  'jam 2026/04/09 Catching error in proper excepction 
    End Sub

    'jrs 2011/04/29
    Public Function isTimeOut() As Boolean
        isTimeOut = False
        'It appears from testing that the Request and Response both share the 
        'same cookie collection.  If I set a cookie myself in the Reponse, it is 
        'also immediately visible to the Request collection.  This just means that 
        'since the ASP.Net_SessionID is set in the Session HTTPModule (which 
        'has already run), thatwe can't use our own code to see if the cookie was 
        'actually sent by the agent with the request using the collection. Check if 
        'the given page supports session or not (this tested as reliable indicator 
        'if EnableSessionState is true), should not care about a page that does 
        'not need session
        If Not (Context.Session Is Nothing) Then
            'Tested and the IsNewSession is more advanced then simply checking if 
            'a cookie is present, it does take into account a session timeout, because 
            'I tested a timeout and it did show as a new session
            If Session.IsNewSession Then
                'If it says it is a new session, but an existing cookie exists, then it must 
                'have timed out (can't use the cookie collection because even on first 
                'request it already contains the cookie (request and response
                'seem to share the collection)
                If Not (Request.Headers("Cookie") Is Nothing) Then
                    Dim szCookieHeader As String = Request.Headers("Cookie")
                    If (szCookieHeader.IndexOf("ASP.NET_SessionId") > 0) Then 'jrs 2015/08/25 it used to be >=, but I don't think the session id should be 0
                        isTimeOut = True
                    End If
                End If

            End If
        End If
    End Function

    Private Sub UpdateLanguage()
        If sLanguage > "" Then
            'Check if there's files to use for the selected language
            If Dir(PutDataFileDir$ & "ifs" & sLanguage & ".db") > "" Then
                Dim WordCount As Long
                Dim x() As String
                WordCount = 0
                'Create a backup of the original files in (English)
                FileCopyCheck(PutDataFileDir$ & "ifs.db", PutDataFileDir$ & "ifsEnglish.db")
                FileCopyCheck(PutDataFileDir$ & "ifsVar.db", PutDataFileDir$ & "ifsVarEnglish.db")
                'Updated files with translation
                Dim dbTrans, db As SQLiteConnection
                dbTrans = New SQLiteConnection("DataSource=" & PutDataFileDir$ & "ifs" & sLanguage & ".db;")
                dbTrans.Open()

                db = New SQLiteConnection("DataSource=" & PutDataFileDir$ & "ifs.db;")
                db.Open()

                Dim tblTrans As SQLiteDataReader
                Dim dbTransCmd, dbCmd As SQLiteCommand
                dbTransCmd = dbTrans.CreateCommand
                dbCmd = db.CreateCommand

                dbTransCmd.CommandText = "SELECT * FROM GListId"
                tblTrans = dbTransCmd.ExecuteReader

                dbCmd.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
                dbCmd.ExecuteNonQuery()

                Do While tblTrans.Read
                    dbCmd.CommandText = "UPDATE [GListId] SET [Description] = """ & tblTrans("Description") & """ WHERE Code = " & tblTrans("Code")
                    dbCmd.ExecuteNonQuery()
                Loop

                tblTrans.Close()

                Dim sUpd As String
                '            Set tblTrans = dbTrans.OpenRecordset("GroupIDNewFull")
                '            Set Tbl = db.OpenRecordset("GroupIDNewFull")
                '            tblTrans.Index = "PrimaryKey"
                '            Tbl.Index = "PrimaryKey"
                '
                '
                '            tblTrans.MoveFirst
                '            Tbl.MoveFirst
                '            Do While Not tblTrans.EOF And Not Tbl.EOF
                '                If Tbl.Fields("GroupNum") = tblTrans.Fields("GroupNum") Then
                '                    Tbl.Edit
                '                    sUpd = "UPDATE [GList] SET Region = """ & tblTrans.Fields("GroupName") & """ WHERE Region = """ & Tbl.Fields("GroupName") & """"
                '                    x = Split(tblTrans.Fields("GroupName"), " ")
                '                    WordCount = WordCount + UBound(x)
                '                    Tbl.Fields("GroupName") = tblTrans.Fields("GroupName")
                '                    x = Split(tblTrans.Fields("LongGroupName"), " ")
                '                    WordCount = WordCount + UBound(x)
                '                    Tbl.Fields("LongGroupName") = tblTrans.Fields("LongGroupName")
                '                    x = Split(tblTrans.Fields("Alias"), " ")
                '                    WordCount = WordCount + UBound(x)
                '                    Tbl.Fields("Alias") = tblTrans.Fields("Alias")
                '                    Tbl.Update
                '                    tblTrans.MoveNext
                '                    Tbl.MoveNext
                '                    db.Execute (sUpd)
                '                ElseIf Tbl.Fields("GroupNum") < tblTrans.Fields("GroupNum") Then
                '                    Tbl.MoveNext
                '                Else
                '                    tblTrans.MoveNext
                '                End If
                '            Loop
                '
                '            tblTrans.Close
                '            Tbl.Close

                db.Close()
                dbTrans.Close()

                'ifsVAR
                dbTrans = New SQLiteConnection("DataSource=" & PutDataFileDir$ & "ifsVar" & sLanguage & ".db;")
                dbTrans.Open()

                db = New SQLiteConnection("DataSource=" & PutDataFileDir$ & "ifsVar.db;")
                db.Open()

                dbTransCmd = dbTrans.CreateCommand
                dbCmd = db.CreateCommand

                dbTransCmd.CommandText = "SELECT * FROM FilterCategories"
                tblTrans = dbTransCmd.ExecuteReader

                dbCmd.CommandText = "PRAGMA foreign_keys = OFF; PRAGMA ignore_check_constraints = ON; PRAGMA journal_mode = MEMORY; PRAGMA synchronous = OFF;"
                dbCmd.ExecuteNonQuery()


                Do While tblTrans.Read
                    dbCmd.CommandText = "UPDATE [FilterCategories] SET [Description] = """ & tblTrans("Description") & """ WHERE FilterID = """ & tblTrans("FilterID") & """"
                    dbCmd.ExecuteNonQuery()
                Loop

                tblTrans.Close()

                dbTransCmd.CommandText = "SELECT * FROM ifsVar"
                tblTrans = dbTransCmd.ExecuteReader

                Do While tblTrans.Read
                    Dim bFirst As Boolean = True
                    dbCmd.CommandText = "UPDATE [ifsVar] SET "
                    If Not IsDBNull(tblTrans("DISPLAY NAME")) Then
                        dbCmd.CommandText = dbCmd.CommandText & "[DISPLAY NAME] = """ & tblTrans("DISPLAY NAME") & """"
                        bFirst = False
                    End If
                    If Not IsDBNull(tblTrans("SPECIALIZED DISPLAY NAME")) Then
                        If Not bFirst Then
                            dbCmd.CommandText = dbCmd.CommandText & ", "
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & "[SPECIALIZED DISPLAY NAME] = """ & tblTrans("SPECIALIZED DISPLAY NAME") & """"
                        bFirst = False
                    End If
                    If Not IsDBNull(tblTrans("SCENARIO NAME")) Then
                        If Not bFirst Then
                            dbCmd.CommandText = dbCmd.CommandText & ", "
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & "[SCENARIO NAME] = """ & tblTrans("SCENARIO NAME") & """"
                        bFirst = False
                    End If
                    If Not IsDBNull(tblTrans("DEFINITION")) Then
                        If Not bFirst Then
                            dbCmd.CommandText = dbCmd.CommandText & ", "
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & "[DEFINITION] = """ & tblTrans("DEFINITION") & """"
                        bFirst = False
                    End If
                    If Not IsDBNull(tblTrans("SUBMODULE NAME")) Then
                        If Not bFirst Then
                            dbCmd.CommandText = dbCmd.CommandText & ", "
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & "[SUBMODULE NAME] = """ & tblTrans("SUBMODULE NAME") & """"
                        bFirst = False
                    End If
                    If Not IsDBNull(tblTrans("UNITS")) Then
                        If Not bFirst Then
                            dbCmd.CommandText = dbCmd.CommandText & ", "
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & "[UNITS] = """ & tblTrans("UNITS") & """"
                        bFirst = False
                    End If
                    If Not IsDBNull(tblTrans("DISPLAY UNITS")) Then
                        If Not bFirst Then
                            dbCmd.CommandText = dbCmd.CommandText & ", "
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & "[DISPLAY UNITS] = """ & tblTrans("DISPLAY UNITS") & """"
                        bFirst = False
                    End If
                    If Not IsDBNull(tblTrans("Parameter Explanation")) Then
                        If Not bFirst Then
                            dbCmd.CommandText = dbCmd.CommandText & ", "
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & "[Parameter Explanation] = """ & tblTrans("Parameter Explanation").ToString.Replace("""", "'") & """"
                        bFirst = False
                    End If
                    If Not IsDBNull(tblTrans("UsrDefCat")) Then
                        If Not bFirst Then
                            dbCmd.CommandText = dbCmd.CommandText & ", "
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & "[UsrDefCat] = """ & tblTrans("UsrDefCat") & """"
                        bFirst = False
                    End If
                    If Not bFirst Then
                        'Debug.Print(tblTrans("NAME"))
                        'If tblTrans("NAME") = "EDYRSAG15" Then Stop
                        dbCmd.CommandText = dbCmd.CommandText & " WHERE [NAME] = """ & tblTrans("NAME") & """"
                        dbCmd.ExecuteNonQuery()
                    End If

                Loop

                tblTrans.Close()

                dbTransCmd.CommandText = "SELECT * FROM VarLists ORDER BY ListNameOrg, VarName, ListOrder"
                tblTrans = dbTransCmd.ExecuteReader

                Try
                    Do While tblTrans.Read
                        dbCmd.CommandText = "UPDATE [VarListsIndices] SET [ListName] = """ & tblTrans("ListName") & """ WHERE ListName = """ & tblTrans("ListNameOrg") & """"
                        dbCmd.ExecuteNonQuery()

                        dbCmd.CommandText = "UPDATE [VarLists] SET [ListName] = """ & tblTrans("ListName") & """"
                        If Not IsDBNull(tblTrans("Category")) Then
                            dbCmd.CommandText = dbCmd.CommandText & ", [Category] = """ & tblTrans("Category") & """"
                        End If
                        If Not IsDBNull(tblTrans("DisplayTitle")) Then
                            dbCmd.CommandText = dbCmd.CommandText & ", [DisplayTitle] = """ & tblTrans("DisplayTitle") & """"
                        End If
                        If Not IsDBNull(tblTrans("UsrDefCat")) Then
                            dbCmd.CommandText = dbCmd.CommandText & ", [UsrDefCat] = """ & tblTrans("UsrDefCat") & """"
                        End If
                        If Not IsDBNull(tblTrans("DisplayVarName")) Then
                            dbCmd.CommandText = dbCmd.CommandText & ", [DisplayVarName] = """ & tblTrans("DisplayVarName") & """"
                        End If
                        If Not IsDBNull(tblTrans("DisplayUnits")) Then
                            dbCmd.CommandText = dbCmd.CommandText & ", [DisplayUnits] = """ & tblTrans("DisplayUnits") & """"
                        End If
                        dbCmd.CommandText = dbCmd.CommandText & " WHERE ListName = """ & tblTrans("ListNameOrg").ToString.Trim & """"
                        dbCmd.ExecuteNonQuery()
                    Loop

                    tblTrans.Close()

                    dbTransCmd.CommandText = "SELECT * FROM VarListsFormulasHdr"
                    tblTrans = dbTransCmd.ExecuteReader

                    Do While tblTrans.Read
                        dbCmd.CommandText = "UPDATE [VarListsFormulasHdr] SET "
                        Dim bFirst As Boolean = True
                        If Not IsDBNull(tblTrans("FormulaDefinition")) Then
                            dbCmd.CommandText = dbCmd.CommandText & "[FormulaDefinition] = """ & tblTrans("FormulaDefinition").ToString.Replace("""", "'") & """"
                            bFirst = False
                        End If
                        If Not IsDBNull(tblTrans("Units")) Then
                            If Not bFirst Then
                                dbCmd.CommandText = dbCmd.CommandText & ", "
                            End If
                            dbCmd.CommandText = dbCmd.CommandText & "[Units] = """ & tblTrans("Units") & """"
                            bFirst = False
                        End If
                        If Not bFirst Then
                            'Debug.Print(tblTrans("FormulaId").ToString)
                            dbCmd.CommandText = dbCmd.CommandText & " WHERE [FormulaId] = " & tblTrans("FormulaId")
                            dbCmd.ExecuteNonQuery()
                        End If
                    Loop

                    tblTrans.Close()

                    dbTransCmd.CommandText = "SELECT DISTINCT ListNameOrg, ListName FROM VarLists ORDER BY ListNameOrg"
                    tblTrans = dbTransCmd.ExecuteReader

                    Do While tblTrans.Read
                        dbCmd.CommandText = "UPDATE [VarListsHdr] SET [ListName] = """ & tblTrans("ListName") & """ WHERE ListName = """ & tblTrans("ListNameOrg") & """"
                        dbCmd.ExecuteNonQuery()
                    Loop


                    tblTrans.Close()

                Catch ex As Exception
                    'Send error message
                    Session("Message") = ex.Message
                    Support.FormManager.Show(New Page, "frm_Message.aspx")
                End Try


                db.Close()
                dbTrans.Close()

                'Debug.Print "Words translated: " & CStr(WordCount)

                'Copy files to RUNFILES
                FileCopy(PutDataFileDir$ & "ifs.db", Session("PutFdrvRun") & "ifs.db")
                FileCopy(PutDataFileDir$ & "ifsVar.db", Session("PutFdrvRun") & "ifsVar.db")

                'Rename Language Files, to indicate they've been already translated
                Rename(PutDataFileDir$ & "ifs" & sLanguage & ".db", PutDataFileDir$ & "ifs" & sLanguage & "Translated.db")
                Rename(PutDataFileDir$ & "ifsVar" & sLanguage & ".db", PutDataFileDir$ & "ifsVar" & sLanguage & "Translated.db")
                Session("JSNeedsUpdate") = True

            End If
        Else 'Restore back to English
            'Check if there's English backup files
            If Dir(PutDataFileDir$ & "ifsEnglish.db") > "" Then
                'Remove Updated files with translation
                Kill(PutDataFileDir$ & "ifs.db")
                Kill(PutDataFileDir$ & "ifsVar.db")
                'Restore backup files
                Rename(PutDataFileDir$ & "ifsEnglish.db", PutDataFileDir$ & "ifs.db")
                Rename(PutDataFileDir$ & "ifsVarEnglish.db", PutDataFileDir$ & "ifsVar.db")
                'Copy files to RUNFILES
                FileCopy(PutDataFileDir$ & "ifs.db", Session("PutFdrvRun") & "ifs.db")
                FileCopy(PutDataFileDir$ & "ifsVar.db", Session("PutFdrvRun") & "ifsVar.db")
                'Restore original name to Translation files
                Dim sLang As String
                'Need to find loaded Language
                sLang = Dir(PutDataFileDir$ & "ifsVar*Translated.db")
                sLang = Replace(sLang, "Translated.db", "")
                sLang = Mid(sLang, 7)
                If sLang > "" Then
                    Rename(PutDataFileDir$ & "ifs" & sLang & "Translated.db", PutDataFileDir$ & "ifs" & sLang & ".db")
                    Rename(PutDataFileDir$ & "ifsVar" & sLang & "Translated.db", PutDataFileDir$ & "ifsVar" & sLang & ".db")
                End If
                Session("JSNeedsUpdate") = True

            End If
        End If


        Exit Sub

    End Sub

    'jam 2025/05/28
    Private Sub SendEmail(ByVal subject As String, ByVal message As String)
        Try
            Dim smtpServer As String = "smtp.gmail.com"
            Dim smtpPort As Integer = 587
            Dim senderEmail As String = " instifsnotifications@gmail.com"
            Dim password As String = "[REDACTED]"
            'Dim recipientEmail As String = "jose.solorzano@du.edu"

            ' Define multiple recipients
            Dim recipients As String() = {"jose.solorzano@du.edu", "jaime.melendez@gmail.com"}

            Dim mail As New MailMessage()
            mail.From = New MailAddress(senderEmail)
            'mail.To.Add(recipientEmail)

            ' Add recipients to the email
            For Each recipient As String In recipients
                mail.To.Add(recipient)
            Next

            mail.Subject = subject
            mail.Body = message
            mail.IsBodyHtml = False

            Dim smtp As New SmtpClient(smtpServer)
            smtp.Port = smtpPort
            smtp.Credentials = New System.Net.NetworkCredential(senderEmail, password)
            smtp.EnableSsl = True

            smtp.Send(mail)
        Catch ex As Exception
            System.Diagnostics.EventLog.WriteEntry("Application", "Email sending error: " & ex.Message, System.Diagnostics.EventLogEntryType.Error)
        End Try
    End Sub

    ''' <summary>
    ''' '''''''''''''''''''''''''''''''
    ''' 
    '''  jam 2026/05/15 Helpers for per-IP concurrency limiter
    ''' 
    ''' ''''''''''''''''''''''''''''''''
    ''' </summary>

    'jam 2026/05/15 Safe parse of integer appSettings with fallback
    Private Shared Function GetAppSettingInt(ByVal key As String, ByVal fallback As Integer) As Integer
        Try
            Dim raw As String = ConfigurationManager.AppSettings(key)
            Dim parsed As Integer
            If Integer.TryParse(raw, parsed) AndAlso parsed > 0 Then
                Return parsed
            End If
        Catch
            ' Suppress — return fallback
        End Try
        Return fallback
    End Function

    'jam 2026/05/15 Resolve real client IP honoring X-Forwarded-For when behind proxy/LB
    Private Shared Function GetClientIp(ByVal ctx As HttpContext) As String
        If ctx Is Nothing OrElse ctx.Request Is Nothing Then Return "unknown"
        Dim xff As String = ctx.Request.Headers("X-Forwarded-For")
        If Not String.IsNullOrWhiteSpace(xff) Then
            Dim comma As Integer = xff.IndexOf(","c)
            If comma > 0 Then Return xff.Substring(0, comma).Trim()
            Return xff.Trim()
        End If
        Return If(ctx.Request.UserHostAddress, "unknown")
    End Function

    'jam 2026/05/15 Skip static assets so per-IP limit only applies to dynamic IFs requests
    Private Shared Function ShouldThrottle(ByVal ctx As HttpContext) As Boolean
        If ctx Is Nothing OrElse ctx.Request Is Nothing Then Return False
        Dim path As String = ctx.Request.Path
        If String.IsNullOrEmpty(path) Then Return False
        path = path.ToLowerInvariant()
        Dim skipExt() As String = {".css", ".js", ".png", ".jpg", ".jpeg", ".gif",
                                   ".ico", ".svg", ".woff", ".woff2", ".ttf",
                                   ".eot", ".map", ".webp"}
        For Each ext As String In skipExt
            If path.EndsWith(ext) Then Return False
        Next
        Return True
    End Function

   'jam 2026/05/15 Periodic cleanup: remove per-IP trackers with no active requests.
    'jam 2026/06/21 Updated for IpSlotTracker — also resets stale slots even if count > 0 (safety net)
    Private Shared Sub CleanupIdleIpSemaphores(ByVal state As Object)
        Try
            Dim removed As Integer = 0
            Dim reset As Integer = 0
            Dim snapshot = _ipSlots.ToArray()
            For Each kvp In snapshot
                Dim tracker As IpSlotTracker = kvp.Value
                SyncLock tracker.LockObj
                    Dim idleSec As Double = (DateTime.UtcNow - tracker.LastActivityUtc).TotalSeconds
                    If tracker.ActiveCount = 0 AndAlso idleSec > IP_SLOT_STALE_SECONDS Then
                        ' Safe to evict — no in-flight requests and idle long enough
                        Dim outTracker As IpSlotTracker = Nothing
                        If _ipSlots.TryRemove(kvp.Key, outTracker) Then
                            removed += 1
                        End If
                    ElseIf tracker.ActiveCount > 0 AndAlso idleSec > IP_SLOT_STALE_SECONDS Then
                        ' Stale slots detected — reset proactively to unblock the IP
                        tracker.ActiveCount = 0
                        tracker.LastActivityUtc = DateTime.UtcNow
                        reset += 1
                    End If
                End SyncLock
            Next
            If removed > 0 OrElse reset > 0 Then
                _logger.Info($"Per-IP throttle cleanup: removed {removed} idle entries, reset {reset} stale trackers, {_ipSlots.Count} remain")
            End If
        Catch ex As Exception
            _logger.Error(ex, "Error in CleanupIdleIpSemaphores")
        End Try
    End Sub

    ''' <summary>
    ''' '''''''''''''''''''''''''''''''
    ''' 
    '''  jam 2025/08/10 Monitoring Memory Health
    ''' 
    ''' ''''''''''''''''''''''''''''''''
    ''' </summary>

    Private Sub MonitorMemoryHealth(ByVal context As String)
        Try
            Dim proc As Process = Process.GetCurrentProcess()
            Dim bytesUsed As Long = proc.PrivateMemorySize64
            Dim mbUsed As Long = bytesUsed \ (1024L * 1024L)  'jam 2026/04/03 Only for logging

            If bytesUsed > MemoryThresholdBytes Then
                Dim alertMessage As String = $"[{context}] Memory usage: {mbUsed} MB (threshold: {MemoryThresholdBytes \ (1024L * 1024L)} MB)"
                _logger.Warn(alertMessage)
                LogExtendedSystemUsage(context)
            End If
        Catch ex As Exception
            _logger.Error(ex, "Error in MonitorMemoryHealth")
        End Try
    End Sub

    ''' <summary>
    ''' '''''''''''''''''''''''''''''''
    ''' 
    '''  jam 2025/06/13 Function to dump the memory usage
    ''' 
    ''' ''''''''''''''''''''''''''''''''
    ''' </summary>

    Private Sub LogExtendedSystemUsage(ByVal reason As String)
        'jam 2025/08/10 Optimizing function about out of memory
        Try
            Dim logPath As String = "E:\Logs\ExtendedMemoryUsageLog.txt"
            Dim process As Process = Process.GetCurrentProcess()
            Dim memoryUsed As Long = process.PrivateMemorySize64 / (1024 * 1024)
            Dim cpuUsage As Double = GetCpuUsage(process)
            Dim threadCount As Integer = process.Threads.Count
            Dim gcInfo As String = $"Gen0: {GC.CollectionCount(0)}, Gen1: {GC.CollectionCount(1)}, Gen2: {GC.CollectionCount(2)}"
            Dim handleCount As Integer = process.HandleCount
            Dim stackTrace As String = Environment.StackTrace

            Dim logContent As String = $"Time: {DateTime.Now}" & vbCrLf &
                                       $"Reason: {reason}" & vbCrLf &
                                       $"Memory Used: {memoryUsed} MB" & vbCrLf &
                                       $"CPU Usage: {cpuUsage}% (approx.)" & vbCrLf &
                                       $"Active Threads: {threadCount}" & vbCrLf &
                                       $"GC Info: {gcInfo}" & vbCrLf &
                                       $"Handle Count: {handleCount}" & vbCrLf &
                                       $"Stack Trace: {stackTrace}" & vbCrLf &
                                       "------------------------------------------------------" & vbCrLf

            File.AppendAllText(logPath, logContent)
        Catch ex As Exception
            EventLog.WriteEntry("Application", "Error writing extended memory log: " & ex.Message, EventLogEntryType.Error)
        End Try
    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''
    ''' 
    '''  jam 2025/06/13 Function to get cpu usage
    ''' 
    ''' ''''''''''''''''''''''''''''''''
    ''' </summary>
    Private Function GetCpuUsage(ByVal process As Process) As Double
        Try
            Dim cpuCounter As New PerformanceCounter("Process", "% Processor Time", process.ProcessName, True)
            cpuCounter.NextValue()
            Threading.Thread.Sleep(1000) ' Allow time for CPU reading
            Return Math.Round(cpuCounter.NextValue() / Environment.ProcessorCount, 2) ' Adjust for multi-core usage
        Catch ex As Exception
            Return -1 ' Return -1 if there's an issue retrieving CPU usage
        End Try
    End Function


End Class

' this module contains a shared variable that is visible from 
' any other page of the application



