Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Threading.Tasks
Imports Microsoft.Extensions.DependencyInjection
Imports IFs.Core
Imports IFs.Data
Imports NLog
Imports System.Reflection
Imports System.Globalization
Imports NLog.Targets

Public Module Vanilla

    Dim _logger As Logger = LogManager.GetCurrentClassLogger()

    ' IFs/Vanilla
    Public Class Application

        Const EX_OK As Integer = 0
        Const EX_USAGE As Integer = 64
        Const EX_FAILURE As Integer = 1

        Private ReadOnly Property services As IServiceProvider

        ' Application Constructor
        Public Sub New(services As IServiceProvider)
            Me.services = services
        End Sub

        ' Application Entry Point
        Public Function Launch(args As String()) As Integer
            Try
                Dim input As IArguments = Arguments.Parse(args)
                ' enable logging
                ' gvdb 2024/04/05
                ' remove custom logging and hook it to the added NLog system
                'If input.Log IsNot Nothing Then
                '    Dim fs As FileStream = input.Log.Open(FileMode.OpenOrCreate)
                '    Trace.Listeners.Add(New TextWriterTraceListener(fs))
                '    Trace.AutoFlush = True
                'End If
                ' dispatch task
                Dim task As Task = Nothing
                'jrs 2022/03/11
                'Regular mode is 5, Big Batch Mode is 6
                If input.Mode = 6 Then
                    'Load list of Scnearios
                    Dim fileList() As String

                    Dim FilesSelected As Integer
                    FilesSelected = -1

                    Dim sDir As String
                    Dim ScenarioFileDir = AppDomain.CurrentDomain.BaseDirectory.Replace("Code.Ifs-Translation\bin\Debug\net471\", "")
                    ScenarioFileDir = ScenarioFileDir.Replace("net471\", "")
                    'jrs 2022/08/04
                    ' gvdb 2024/04/05
                    ' update paths to new .Net8
                    ScenarioFileDir = ScenarioFileDir.Replace("Code.Ifs-Translation\bin\Debug\net8\", "")
                    ScenarioFileDir = ScenarioFileDir.Replace("net8\", "")

                    'jrs 2024/10/08
                    StartDirec = ScenarioFileDir

                    ScenarioFileDir = ScenarioFileDir & "Scenario"
                    Dim PutFdrvRun = ScenarioFileDir.Replace("Scenario", "RUNFILES\")

                    If ScenarioFileDir.Contains("Session") Then
                        sDir = ScenarioFileDir.Substring(0, ScenarioFileDir.IndexOf("Session") - 1)
                    Else
                        sDir = ScenarioFileDir
                    End If
                    ReadSceFiles(sDir, FilesSelected, fileList)
                    Dim X As String()
                    Dim P As Integer

                    If input.Monitor Then
                        Console.WriteLine("Scenario Batch Monitor")
                        Console.WriteLine("International Futures is running one or more scenarios/cases.")
                    End If

                    'jrs 2024/06/04
                    Dim spth As String = ""
                    If input.Session > -1 Then
                        spth = "Session"
                        If input.Session < 10 Then
                            spth = spth & "0"
                        End If
                        spth = spth & input.Session.ToString.Trim & "\"
                    End If

                    'Process each Scneario
                    Dim i As Integer
                    For i = 0 To FilesSelected
                        If input.Monitor Then
                            Console.WriteLine((i + 1).ToString + " of " + (FilesSelected + 1).ToString)
                            Console.WriteLine(fileList(i))
                            FileCopy(fileList(i), ScenarioFileDir$ + "\" + spth + "Working.sce") 'jrs 2024/10/11 added \spth
                            'jrs 2024/06/04
                            'Move Copy base case to working here, in other cases the GUI is doing it, but for big batch is needed
                            If input.Historic = True Then
                                FileCopyCheck(StartDirec + "RUNFILES\" & spth & "IFsBaseHist.Run.db", StartDirec + "RUNFILES\" & spth & "working.run.db")
                            Else
                                FileCopyCheck(StartDirec + "RUNFILES\" & spth & "IFsBase.Run.db", StartDirec + "RUNFILES\" & spth & "working.run.db")
                            End If
                            FileCopyCheck(StartDirec + "DATA\SAMBase.db", StartDirec + "RUNFILES\" & spth & "SAMWorking.db")

                            task = Task.Factory.StartNew(Sub() ForecastAll(input))
                            ' wait till task completes
                            task.Wait()

                            'jrs 2024/10/11 replace PutFdrvRun$ with StartDirec + "RUNFILES\" & spth
                            'Save working.run and Samworking.mdb with the scenario name jrs 2005/06/22 replace is not case sensitive
                            FileCopy(StartDirec & "RUNFILES\" & spth & "working.run.db", fileList(i).Substring(0, fileList(i).Length - 3) & "run.db")
                            X = fileList(i).Split(New Char() {"\"c})
                            P = fileList(i).Length - X(X.Length - 1).Length
                            FileCopy(StartDirec & "RUNFILES\" & spth & "SAMWorking.db", fileList(i).Replace(".sce", ".db").Substring(0, P) & "SAM" & X(X.Length - 1).Substring(0, X(X.Length - 1).Length - 3) & "db")
                            'jrs 2008/07/29 I was copying to RUNFILES instead of Scenario, which is not right for the Web Batch run
                            'jrs 2008/06/05 save ifsPopWork.mdb for Cohort Dispalys (population, education, and health).
                            FileCopy(StartDirec & "RUNFILES\" & spth & "IFsPopWork.db", fileList(i).Replace(".sce", ".db").Substring(0, P) & "IFsPop" & X(X.Length - 1).Substring(0, X(X.Length - 1).Length - 3) & "db")
                        End If
                    Next i

                Else
                    Select Case input.Process
                        Case 0
                            task = Task.Factory.StartNew(Sub() RebuildAll(input))
                        Case 1
                            task = Task.Factory.StartNew(Sub() ForecastAll(input))
                        Case 2 'jrs 2019/06/21
                            task = Task.Factory.StartNew(Sub() RebuildCommon(input))
                        Case 3 'jrs 2022/05/31
                            'Rebuild Empty Base to help with translation of old vb6 files
                            task = Task.Factory.StartNew(Sub() RebuildEmptyBase(input))
                    End Select
                End If
                ' wait till task completes
                task.Wait()
            Catch ex As ArgumentException
                Console.WriteLine(ex.Message)
                'gvdb 2024/04/05
                ' added error logging
                _logger.Error(ex, "Unexpected argument error during launch")
                Return EX_USAGE
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                'gvdb 2024/04/05
                ' added error logging
                _logger.Error(ex, "Unexpected error during launch")
                Return EX_FAILURE
            End Try
            Return EX_OK
        End Function

        ' Rebuild Base
        <MTAThread()>
        Public Sub RebuildAll(input As IArguments)
            Dim contextSvc As IContextService = services.GetService(Of IContextService)()
            Dim storageSvc As IStorageService = services.GetService(Of IStorageService)()
            Dim forecastSvc As IForecastService = Me.services.GetService(Of IForecastService)()
            Dim mode As ContextMode = If(input.Historic = True, ContextMode.Historic, ContextMode.Future)
            Using context As IRebuildContext = contextSvc.ForRebuild(mode),
            working As IStorage = storageSvc.Open(True, context)
                For Each df As IDataframe In forecastSvc.RebuildAll(context)
                    If input.Verbose Then
                        Console.WriteLine(df.Variable.Name)
                    End If
                    working.SaveDataframe(df)
                Next
                working.Flush()
            End Using

        End Sub

        ' Run Model
        <MTAThread()>
        Public Sub ForecastAll(input As IArguments)
            Dim spth As String = ""
            If input.Session > -1 Then
                spth = "Session"
                If input.Session < 10 Then
                    spth = spth & "0"
                End If
                spth = spth & input.Session.ToString.Trim & "\"
            End If

            Dim contextSvc As IContextService = services.GetService(Of IContextService)()
            'jrs 2020/04/03 Dim storageSvc As IStorageService = services.GetService(Of IStorageService)()
            Dim storageSvc As IStorageService
            If input.Session > -1 Then
                storageSvc = New StorageService(Nothing, spth & "Working.run.db")
            Else
                storageSvc = services.GetService(Of IStorageService)()
            End If
            Dim forecastSvc As IForecastService = Me.services.GetService(Of IForecastService)()

            Dim StartDirec = AppDomain.CurrentDomain.BaseDirectory.Replace("Code.Ifs-Translation\bin\Debug\net471\", "")
            StartDirec = StartDirec.Replace("net471\", "")
            'jrs 2022/08/04
            StartDirec = StartDirec.Replace("Code.Ifs-Translation\bin\Debug\net8\", "")
            StartDirec = StartDirec.Replace("net8\", "")
            ' gvdb 2024/04/05
            ' update paths to new .Net8
            StartDirec = StartDirec.Replace("bin\Debug\net8\", "")

            Dim bSceFlag As Boolean = False
            If System.IO.File.Exists(StartDirec + "Scenario\" & spth & "Working.Sce") Then
                bSceFlag = True
                'jrs 2024/06/04
                'Move out for big batch, otherwise the GUI takes care of this
                'If input.Historic = True Then
                '    FileCopyCheck(StartDirec + "RUNFILES\" & spth & "IFsBaseHist.Run.db", StartDirec + "RUNFILES\" & spth & "working.run.db")
                'Else
                '    FileCopyCheck(StartDirec + "RUNFILES\" & spth & "IFsBase.Run.db", StartDirec + "RUNFILES\" & spth & "working.run.db")
                'End If
                'FileCopyCheck(StartDirec + "DATA\SAMBase.db", StartDirec + "RUNFILES\" & spth & "SAMWorking.db")
            End If

            'jrs 2019/06/19            
            Dim mode As ContextMode = If(input.Historic = True, ContextMode.Historic, ContextMode.Future)
            Using working As IStorage = storageSvc.Open(False),
                    context As IForecastContext = contextSvc.ForForecast(working, CShort(input.Horizon), CShort(input.Session), input.Verbose, mode)
                If input.Horizon < context.FirstYear OrElse input.Horizon > context.LastYear Then
                    Throw New ArgumentException($"Invalid input: horizon={input.Horizon}")
                End If

                'jrs 2019/07/22 from LIU
                'context.RegressionData.Include(reg >= reg.Coefficients).ToList() ' to load all coefficients for all regressions

                'jrs 2019/06/13 *** begin ***
                '1. Need to read all dataframes
                Dim IFsResults As Dictionary(Of String, Dataframe) = New Dictionary(Of String, Dataframe)
                For Each sVarName In context.VariableNames
                    'jrs 2025/05/23 added condition to avoid extra work when bilateral is off
                    If System.IO.File.Exists(context.PutFdrvRun$ + "IFsForDyadicWork.db") OrElse context.GetVariable(sVarName).Bilat <> 1 Then
                        IFsResults.Add(sVarName, Dataframe.Create(sVarName, context, context.FirstYear, context.LastYear, 0))
                    End If
                Next
                Dim IFsResults2 As Dictionary(Of String, IDataframe) = working.LoadAllDataframes() _
                        .ToDictionary(Of String, IDataframe)(Function(df) df.Variable.Name, Function(df) df)
                '2. Compute All Forecast Modules
                forecastSvc.ForecastAllStatic(context, IFsResults, IFsResults2)
                'jrs 2024/05/31 
                Debug.Print("Before Save All Dataframes")
                '3. Save all dataframes
                For Each sVarName In context.VariableNames
                    'If sVarName = "MIGRANTSOUT" Then Stop
                    'jrs 2020/01/07 need to save parameters for scenarios
                    'jrs 2019/09/27 don't save parameters again
                    'jrs 2020/03/31 added Or (context.HistoricMode And sVarName = "gdprext") 
                    If context.GetVariable(sVarName).Type <> 3 Or bSceFlag Or (context.HistoricMode And sVarName = "gdprext") Then
                        'jrs 2025/05/23 added condition to avoid extra work when bilateral is off
                        If System.IO.File.Exists(context.PutFdrvRun$ + "IFsForDyadicWork.db") OrElse context.GetVariable(sVarName).Bilat <> 1 Then
                            working.SaveDataframe(IFsResults(sVarName))
                        End If
                    End If
                Next
                working.Flush()
                Debug.Print("After Save All Dataframes")
                'jrs 2019/06/13 *** end ***

                ''yuanjia 2019/08/25 *** create a new IDataframe to store all intermediate results : begin ***
                'Dim df_test As IDataframe = Dataframe.Create("POP", context, context.FirstYear, Nothing)
                'Console.WriteLine(df_test.Variable.Name)
                ''yuanjia 2019/08/25 **** end *****************

                'For y As Short = context.FirstYear To context.LastYear
                '    For Each df As IDataframe In forecastSvc.ForecastAll(y, context)
                '        If input.Verbose Then
                '            Console.WriteLine(df.Variable.Name)
                '        End If
                '        'IFsResults2(df.Variable.Name) = IFsResults2(df.Variable.Name).Append(df)

                '        'yuanjia 2019/08/25 *** append intermediate result to df_test : begin ***
                '        If df.Variable.Name = df_test.Variable.Name Then
                '            'jrs 2019/10/07
                '            If y = context.FirstYear Then
                '                df_test = df
                '            Else
                '                df_test = df_test.Append(df)
                '            End If
                '            'jrs 2019/10/07
                '            If y = context.LastYear Then
                '                df = df_test
                '            End If
                '            Console.WriteLine("append something: {0}", df_test.RowCount)
                '        End If
                '        'yuanjia 2019/08/25 **** end *****************

                '        working.SaveDataframe(df)
                '    Next
                '    'jrs 2019/06/25
                '    working.Flush()
                'Next
                'jrs 2019/06/25 working.Flush()
            End Using
        End Sub

        'jrs 2019/06/21
        ' Rebuild Common
        <MTAThread()>
        Public Sub RebuildCommon(input As IArguments)
            Dim contextSvc As IContextService = services.GetService(Of IContextService)()
            Dim forecastSvc As IForecastService = Me.services.GetService(Of IForecastService)()
            Dim mode As ContextMode = If(input.Historic = True, ContextMode.Historic, ContextMode.Future)
            Using context As IRebuildContext = contextSvc.ForRebuild(mode)
                forecastSvc.RebuildCommon(context)
            End Using
        End Sub

        'jrs 2022/05/31
        ' Rebuild Empty Base to help with translation of old vb6 files
        <MTAThread()>
        Public Sub RebuildEmptyBase(input As IArguments)
            Dim contextSvc As IContextService = services.GetService(Of IContextService)()
            Dim storageSvc As IStorageService = services.GetService(Of IStorageService)()
            Dim forecastSvc As IForecastService = Me.services.GetService(Of IForecastService)()
            Dim mode As ContextMode = If(input.Historic = True, ContextMode.Historic, ContextMode.Future)
            Using context As IRebuildContext = contextSvc.ForRebuild(mode),
                    working As IStorage = storageSvc.Open(True, context)
                For Each Var As String In context.VariableNames
                    Dim df As Dataframe = Dataframe.Create(Var, context, context.FirstYear, context.LastYear, 0)

                    If input.Verbose Then
                        Console.WriteLine(df.Variable.Name)
                    End If
                    working.SaveDataframe(df)
                Next
                working.Flush()
            End Using
        End Sub
    End Class


    ' Bootstrap
    <MTAThread()>
    Function Main(args As String()) As Integer

        ' gvdb 2024/04/05
        ' Add logging at startup
        Dim root As AppDomain = AppDomain.CurrentDomain
        Console.WriteLine($"Starting with base directory:{root.BaseDirectory}")

        Trace.Listeners.Clear()
        Trace.Listeners.Add(New NLog.NLogTraceListener())

        Dim t As Type = GetType(Vanilla)
        Dim assemFromType As Assembly = t.Assembly

        'gvdb 2024/04/12
        ' add websessionid to log
        If args.Length > 7 Then
            If "--websessionid".Equals(args(args.Length - 2).ToLower()) Then
                LogManager.Configuration.Variables("webSessionID") = args(args.Length - 1)
            Else
                LogManager.Configuration.Variables("webSessionID") = "NoSessionID"
            End If
        End If

        _logger.Info($"Application started - {assemFromType.GetName().Version.ToString()}")
        _logger.Info($"Application directory - {System.AppContext.BaseDirectory}")
        _logger.Info($"Application cultureInfo - {CultureInfo.CurrentCulture.EnglishName} - {CultureInfo.CurrentCulture.Name}")
        _logger.Info("------------------------------------------------------------------------------")

        'jrs 2022/08/30
        If args.Count = 0 Then
            InitVars()
            'if not arguments create a console menu
            Do While True
                Dim opt = CreateConsoleMenu()

                Select Case opt
                    Case "1"
                        SearchRebuildFiles()
                        MarkVarsUsedAsHistAnalog()
                        MarkVarsUsedInFx()
                    Case "2"
                        SearchModelFiles4Fx()
                    Case "3"
                        BuildDocumentation()
                    Case "x"
                        Exit Do
                    Case Else
                        Console.WriteLine("Option doesn't exist")
                End Select
            Loop
        Else
            'jrs 2022/10/18
            'Adding Provinces
            If args(0) = "9" Then
                InitVars()
                AddProvinces(args(1))
                DeleteCountriesFromRUNFILES(args(1))
            ElseIf args(0) = "8" Then 'jrs 2023/06/13
                InitVars()
                If args(1) = "a" Then
                    AddCountry(args(2), args(3))
                ElseIf args(1) = "u" Then
                    UpdCountry(args(2), args(3), args(4))
                Else
                    DelCountry(args(2))
                End If
            Else
                'Default arguments: 5 2100 -1 true true 1 false --log jrs.txt
                'Testing Provinces: 9 "South Africa"
                'Testing Add country: 8 a "Vatican", "JRS"
                '5 is default run, 6 is for batch
                '2100 run horizon
                '-1 session id
                'true: monitor flag
                'true: verbose flag
                '1: 1 is model run, 0 is rebuild of base, 2 rebuild common, etc
                'false: historic flag

                ' Instantiate a registry of services for dependency lookup
                Dim services As IServiceProvider = New ServiceCollection() _
            .AddScoped(Of IContextService, ContextService)() _
            .AddScoped(Of IStorageService, StorageService)() _
            .AddScoped(Of IForecastService, ForecastService)() _
            .BuildServiceProvider()
                ' Instantiate IFs/Vanilla application
                Dim vanilla As Application = New Application(services)
                Return vanilla.Launch(args)
            End If
        End If
    End Function



End Module
