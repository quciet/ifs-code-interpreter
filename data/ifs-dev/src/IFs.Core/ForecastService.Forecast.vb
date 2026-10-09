Imports System.Data.Common
'jrs 2023/09/29
Imports System.Globalization
Imports System.Threading.Tasks.Dataflow
Imports IFs.Core.Models
Imports IFs.Data
Imports Microsoft.EntityFrameworkCore

Public Partial Class ForecastService
    Inherits ForecastServiceBase

    Dim planner As IModelPlanner(Of IForecastContext)

    Public Overrides Iterator Function ForecastModule(moduleName As String, year As Short, context As IForecastContext) As IEnumerable(Of IDataframe)
        If year = context.FirstYear Then
            Me.planner = Me.GetPlanner(context)
        End If
        For Each job As VariableProducer In Me.planner.Plan(year)
            ' For Each df As IDataframe In Job()
            '     ' TODO
            ' Next
        Next
    End Function

    Public Overrides Iterator Function ForecastAll(year As Short, context As IForecastContext) As IEnumerable(Of IDataframe)
        If year = context.FirstYear Then
            Me.planner = Me.GetPlanner(context)
        End If
        For Each job As VariableProducer In Me.planner.Plan(year)
            For Each df As IDataframe In job()
                ' TODO
                'jrs 2019/06/10
                Yield df
            Next
        Next
    End Function

    'jrs 2019/06/13
    Public Overrides Sub ForecastAllStatic(context As IForecastContext, IFsResults As Dictionary(Of String, Dataframe), IFsResults2 As Dictionary(Of String, IDataframe))
        'IFsResults is to store all final forecast results
        'IFsResults2 is to pre-load parameters for ProcessTimeSeries
        'jrs 2023/09/29
        CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")

        Commons.Configure(context)

        '2020/02/18
        dfBaseGDPADJ = IFsResults2("GDPADJ")
        'jrs 2021/06/01
        dfBaseWPOP = IFsResults2("WPOP")
        dfBaseigdpr = IFsResults2("igdpr")
        dfBaseXWORKREMIT = IFsResults2("XWORKREMIT")
        dfBasePOPWORK = IFsResults2("POPWORKING")
        dfBaseHLYLDWORK = IFsResults2("HLYLDWORK")

        'If it exist
        Dim IFsResults3 As Dictionary(Of String, Dataframe) = New Dictionary(Of String, Dataframe)
        Dim IFsResults4 As Dictionary(Of String, IDataframe) = New Dictionary(Of String, IDataframe)
        If System.IO.File.Exists(ScenarioFileDir$ + "Working.Sce") Then
            Scenarioflag = True
            'frm_MainMenu.IFsGUIctrl1.cSensitivityFlag = False

            'If context.HistoricMode Then
            '    FileCopyCheck(PutFdrvRun$ + "IFsBaseHist.Run.db", PutFdrvRun$ + "working.run.db")
            'Else
            '    FileCopyCheck(PutFdrvRun$ + "IFsBase.Run.db", PutFdrvRun$ + "working.run.db")
            'End If
            'FileCopyCheck(context.DataFileDir + "SAMBase.db", PutFdrvRun$ + "SAMWorking.db")
            Call ProcessTimeSeriesNew(0, 1, context, IFsResults2, IFsResults3)

            For Each item In IFsResults3
                IFsResults4.Add(item.Key, item.Value)
            Next
            For Each item In IFsResults2
                If Not IFsResults4.ContainsKey(item.Key) Then
                    IFsResults4.Add(item.Key, item.Value)
                End If
            Next
        End If


        'Using db As IBasecase = BasecaseContext.Open(True, context)
        '    db.Clear(GetType(PopBasecase))
        '    'db.Clear(GetType(FerBasecase))
        '    'db.Clear(GetType(MorBasecase))
        '    'db.Clear(GetType(EdnGradsBaseCase))
        '    'db.Clear(GetType(EdnYears15BaseCase))
        '    db.Flush()
        'End Using



        Dim FirstYear As Short, LastYear As Short
        FirstYear = context.FirstYear
        LastYear = context.LastYear

        'jrs 2019/11/20
        ifs_buffer = New BufferBlock(Of Action)()
        Dim thread = SaveAllAsync(ifs_buffer)

        'jrs 2020/04/03
        If PutFdrvRun$.Contains("Session") Then
            Dim SessionFolder As String
            Dim pos As Integer
            pos = PutFdrvRun.IndexOf("Session")
            SessionFolder = PutFdrvRun.Substring(pos)
            sSAMFile = SessionFolder & "SAMWorking.db"
            sSmokingFile = SessionFolder & "IFsSmoking.db"
            sIFsPopFile = SessionFolder & "ifsPopWork.db"
        End If

        'jrs 2020/04/03
        Using db As IBasecase = BasecaseContext.Open(True, context, sIFsPopFile)
            'Reset the file
        End Using

        Dim sql As String
        Using db3 As BasecaseContext = New BasecaseContext(sIFsPopFile)
            conn3 = db3.Database.GetDbConnection
            If conn3.State = 0 Then 'closed
                conn3.Open()
            End If

            'jrs 2019/11/18 *** begin ***
            Using cmd2 = conn3.CreateCommand()
                cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                cmd2.ExecuteNonQuery()
            End Using
            db3.Database.AutoTransactionsEnabled = False
            db3.ChangeTracker.AutoDetectChangesEnabled = False
            db3.ChangeTracker.LazyLoadingEnabled = False
            'jrs 2019/11/18 *** end ***

            'db3.BulkDeletePop(FirstYear, LastYear, context.HistoricMode)            

            'jrs 2019/12/23 Commented out all Delete's because now the entire file is being reset, and I will have a different file for Historical mode
            'jrs 2019/11/18
            'Using tx = conn3.BeginTransaction()
            '    Using cmd = conn3.CreateCommand()
            '        If Not context.HistoricMode Then
            '            sql = "DELETE FROM pop_base_case WHERE TimeId >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM fer_base_case WHERE TimeId >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM mor_base_case WHERE TimeId >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM StuntingByAge WHERE Years >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM EdnGradsBaseCase WHERE TimeId >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM EdnYears25BaseCase WHERE TimeId >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM EdnYears15BaseCase WHERE TimeId >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM mor_det_base_case WHERE TimeId >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM morb_det_base_case WHERE TimeId >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM HLPROXIMATEDRIVERS WHERE Year >= " & CStr(FirstYear)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()
            '        Else
            '            sql = "DELETE FROM pop_base_case WHERE TimeId >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM fer_base_case WHERE TimeId >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM mor_base_case WHERE TimeId >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM StuntingByAge WHERE Years >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM EdnGradsBaseCase WHERE TimeId >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM EdnYears25BaseCase WHERE TimeId >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM EdnYears15BaseCase WHERE TimeId >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM mor_det_base_case WHERE TimeId >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM morb_det_base_case WHERE TimeId >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()

            '            sql = "DELETE FROM HLPROXIMATEDRIVERS WHERE Year >= " & CStr(FirstYear) & " AND Years < " & CStr(context.LastYearHistory)
            '            cmd.CommandText = sql
            '            cmd.ExecuteNonQuery()
            '        End If

            '        sql = "DELETE FROM mor_det_factors"
            '        cmd.CommandText = sql
            '        cmd.ExecuteNonQuery()

            '        sql = "DELETE FROM mor_det_norm_factors"
            '        cmd.CommandText = sql
            '        cmd.ExecuteNonQuery()

            '        'jrs 2019/11/18                    
            '        tx.Commit()
            '    End Using
            'End Using
            'jrs 2019/11/18 db3.Flush()
            'jrs 2019/11/18         End Using

            'Saving Smoking Forecast
            If Not context.HistoricMode Then
                Using dbS As IFsSmokingContext = New IFsSmokingContext(sSmokingFile)
                    Dim conn = dbS.Database.GetDbConnection
                    If conn.State = 0 Then 'closed
                        conn.Open()
                    End If
                    Using cmd = conn.CreateCommand()
                        If FirstYear > BaseYear Then
                            sql = "DELETE FROM SmokingRateEstimatesReg WHERE Year >= " & CStr(FirstYear)
                        Else
                            sql = "DELETE FROM SmokingRateEstimatesReg WHERE Year > " & CStr(FirstYear)
                        End If
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                    dbS.Flush()
                End Using
            End If

            'jrs 2019/11/19 
            ifsPopWorkDB = db3
            For y As Short = FirstYear To LastYear
                'jrs 2026/05/20
                'Dim sw As Stopwatch = Stopwatch.StartNew()

                'jrs 2019/11/18             Using db3 = BasecaseContext.Open(False, context)
                'ifsPopWorkDB = db3
                Console.WriteLine("Year " & y.ToString)

                IY = y - FirstYear + 1S
                'jrs 2020/01/02
                If Scenarioflag Then
                    Retriev(IFsResults4, y, FirstYear)
                Else
                    Retriev(IFsResults2, y, FirstYear)
                End If

                'jrs 2014/08/27 only in first year
                If IY% = 1 Then
                    LoadTableSET4ExogenousForecast(CInt(ifsexomodelid), context)
                End If

                If y > FirstYear Then

                    '
                    'Begin IFs model calls  'mti 2025/07/14
                    'Parsing Note: to be captured by parsing the name of all of these called routines need to start with three letters - IFs 

                    ifsPOPAllYears(context)

                    ifsECO1AllbutFirstYear(context)

                    ifsECOSAM1AllbutFirstYear(context)

                    IfsAg1AllButFirstYear(context)

                    IFsInfra1AllbutFirstYear(context)

                    IFsEn1AllbutFirstYear(context)

                    ifsECO2AllbutFirstYear(context)

                    ifsECOSAM2AllbutFirstYear(context)

                    IFsEcoIntl1AllbutFirstYear(context)

                    ifsECO3AllbutFirstYear(context)

                    ifsECOSAM3AllbutFirstYear(context)

                    IFsDomPol1AllButFirstYear(context) 'Polyarchy here

                    IfsAg2AllbutFirstYear(context)

                    ifsECOSAM4AllbutFirstYear(context)

                    ifsECO4AllbutFirstYear(context)

                    ifsEcoDyadic1AllButFirstYear(context)

                    IFsEcoIntl1AllYears(context)

                    'mti - Infrastructure calls are taken ahead of IfsIntlPol to be able to use this year's fund demand
                    IFsInfra2AllbutFirstYear(context)

                    IFsInfraWATSAN1AllbutFirstYear(context)

                    'mti 2022/06/21 start
                    IFsInfraWATSANNewAllbutFirst(context)
                    'mti 2022/06/21 End

                    IFsIntlPol1AllButFirstYear(context) 'Needs WTRADE from Eco(3) 'dyadic LDRTRIP needs to go after/in here.

                    ifsPOP2AllYears(context) 'Needs GDS health from IFsIntlPol

                    'mti 2020/09/01 start

                    If edmodelsw = 0 Then
                        Call IFsEduc1AllButFirstYear(context) 'Needs age distribution from IFsPop(2)
                        'Needs GDS educ from IFsIntlPol
                    Else
                        Call IFsEduc_new_init(1, context)
                    End If
                    'mti 2020/09/01 End

                    'Needs GDS educ from IFsIntlPol

                    'budget impact on all infra
                    IFsInfra3AllbutFirstYear(context)

                    'budget impact on watsan; needs impact calculation at IFsinfra (3)
                    IFsInfraWATSAN2AllbutFirstYear(context)

                    'mti 2022/08/17 Start
                    IFsInfraWATSANNew2AllbutFirstYear(context)
                    'mti 2022/08/17 End

                    IfsIntlPol2AllButFirstYear(context) 'Needs EDCost WTRADE from IFsEduc(1)
                    'When budget is being forced

                    ifsAuto1AllbutFirstYear(context)

                    ifsHealthAllYears(context)

                    ifsPOP3AllYears(context)  'Needs EdYrs25 from IFsEduc(1)

                    IfsWater1AllbutFirstYear(context)

                    ifsECO6AllbutFirstYear(context)

                    ifsECOSAM5AllbutFirstYear(context)

                    IFsEcoIntl2AllYears(context)

                    IFsEn2AllYears(context)

                    IfsDomPol2AllButFirstYear(context)

                    IFsEnviron1AllbutFirstYear(context)

                    ifsAgAllYears(3, context)

                    IFsRiskIndexAllYears(context)

                    IFsGovIndexAllYears(context)

                    ifsECO7AllbutFirstYear(context)

                    IFsIntlConflictAllYears(context)

                    ifsTechAllbutFirstYear(context)

                    'jrs 2007/05/18
                    'jrs 2018/12/04 Call IFsPop(4) 'Advances population cohorts to next year
                    ifsPOP4AllYears(context)

                    'mti 2009/12/14
                    ifsKnowledge1AllYears(context)

                    'End IFs model calls  'mti 2025/07/14

                Else 'Firt Year


                    'Begin IFs model firstYear  'mti 2025/07/14
                    ifsPOP1FirstYear(context)
                    Debug.WriteLine("POP1 Completed")

                    ifsECO1FirstYear(context)
                    Debug.WriteLine("ECO1 Completed")

                    ifsECOSAM1FirstYear(context)
                    Debug.WriteLine("ECOSAM1 Completed")

                    IFsEcoIntl1FirstYear(context)
                    Debug.WriteLine("ECOINTL1 Completed")

                    ifsPOP2AllYears(context)
                    Debug.WriteLine("POP2 Completed")

                    'mti 2020/09/01 start



                    If edmodelsw = 0 Then
                        Call IFsEduc1FirstYear(context) 'Needs age distribution from IFsPop(2)
                        'Needs GDS educ from IFsIntlPol
                    Else
                        Call IFsEduc_new_init(1, context)
                    End If
                    'mti 2020/09/01 End

                    Debug.WriteLine("EDU1 Completed")

                    IFsInfraWATSAN1FirstYear(context)
                    Debug.WriteLine("WATSAN1 Completed")

                    'mti 2022/06/21 start
                    IFsInfraWATSANNewFirstYear(context)
                    Debug.WriteLine("WATSAN1NEW Completed")
                    'mti 2022/06/21 End

                    ifsPOP3AllYears(context)  'Needs EdYrs25 from IFsEduc(1)
                    Debug.WriteLine("POP3 Completed")

                    IFsDomPol1FirstYear(context)
                    Debug.WriteLine("DOMPOL1 Completed")

                    IFsIntlPol1FirstYear(context)
                    Debug.WriteLine("INTPOL1 Completed")

                    ifsEcoDyadic1FirstYear(context) '
                    Debug.WriteLine("ECODYA1 Completed")

                    IFsInfra1FirstYear(context)
                    Debug.WriteLine("INF1 Completed")

                    IFsEn1FirstYear(context)
                    Debug.WriteLine("ENE1 Completed")

                    IFsInfra2FirstYear(context)
                    Debug.WriteLine("INF2 Completed")

                    IFsEn2AllYears(context)
                    Debug.WriteLine("ENE2 Completed")

                    ifsAuto1FirstYear(context)
                    Debug.WriteLine("AUT1 Completed")

                    'jrs 2007/10/18 needs Auto information
                    ifsHealth1FirstYear(context)
                    Debug.WriteLine("HEA1 Completed")

                    ifsECO2FirstYear(context) ''
                    Debug.WriteLine("ECO2 Completed")

                    IfsAg1FirstYear(context)
                    Debug.WriteLine("AGR1 Completed")

                    IfsWater1FirstYear(context)
                    Debug.WriteLine("WAT1 Completed")

                    ifsECOSAM2FirstYear(context)
                    Debug.WriteLine("ECOSAM2 Completed")

                    IFsDomPol2FirstYear(context)
                    Debug.WriteLine("DOMPOL2 Completed")

                    IFsEnviron1FirstYear(context)
                    Debug.WriteLine("ENV2 Completed")

                    IFsRiskIndexAllYears(context)
                    Debug.WriteLine("RISKIDX Completed")

                    IFsGovIndexAllYears(context)
                    Debug.WriteLine("GOVIDX Completed")

                    ifsECO3FirstYear(context)
                    Debug.WriteLine("ECO3 Completed")

                    IFsIntlConflictAllYears(context) 'JAM 2019-09-15 Method was renamed
                    Debug.WriteLine("INTCONF Completed")

                    ifsTechFirstYear(context)
                    Debug.WriteLine("TECH Completed")

                    ifsPOP4AllYears(context)
                    Debug.WriteLine("POP4 Completed")

                    'mti 2009/12/14
                    ifsKnowledge1AllYears(context)
                    Debug.WriteLine("KNOW1 Completed")
                    'End IFs model firstYear  'mti 2025/07/14
                End If

                'If sMode = "5" Then
                'FORMAT: YEAR, POP, GDP, FORST, CO2PPM
                Dim sLine As String
                sLine = y.ToString & "," & WPOP.ToString & "," & WGDP.ToString & "," & WFORST.ToString & "," & CO2PPM.ToString
                Dim bAppend As Boolean = True
                If y = FirstYear Then
                    bAppend = False
                End If
                Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=bAppend)
                objWriter.WriteLine(sLine)
                objWriter.Close()
                'End If

                'Save Results to Dataframe
                Fillit(IFsResults, y, FirstYear)
                'jrs 2025/02/24
                'Check4Nan(IFsResults, y, FirstYear)
                'jrs 2019/11/18             End Using

                'jrs 2026/05/20
                'sw.Stop()
                'Console.WriteLine($"Iteration {y}: {sw.Elapsed.TotalMilliseconds} ms")
            Next y

            'jrs 2019/11/19 
        End Using

        'jrs 2023/11/06 Adding Try to catch problems saving NaN
        Try
            'jrs 2019/11/20
            ifs_buffer.Complete()
            thread.Wait()
        Catch ex As Exception
            'jrs 2023/11/06 Adding Try to catch problems saving NaN
            Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "ErrorSce.txt", append:=True)
            objWriter.WriteLine(ex.Message)
            objWriter.Close()
        End Try

        'jrs 2023/11/06 Adding Try to catch problems saving NaN
        Try
            Using dbSAM As SAMContext = New SAMContext(sSAMFile)
                Dim connSAM As DbConnection
                connSAM = dbSAM.Database.GetDbConnection

                If connSAM.State = 0 Then 'closed
                    connSAM.Open()
                End If

                Using cmdSAM = connSAM.CreateCommand()
                    cmdSAM.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                    cmdSAM.ExecuteNonQuery()
                End Using
                dbSAM.Database.AutoTransactionsEnabled = False
                dbSAM.ChangeTracker.AutoDetectChangesEnabled = False
                dbSAM.ChangeTracker.LazyLoadingEnabled = False

                Using tx = connSAM.BeginTransaction()
                    Using cmd = connSAM.CreateCommand()
                        If Not context.HistoricMode Then
                            sql = "DELETE FROM AX WHERE Year >= " & CStr(FirstYear)
                        Else
                            sql = "DELETE FROM AX WHERE Year >= " & CStr(FirstYear) & " AND Year <= " & CStr(context.LastYearHistory)
                        End If
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()

                        tx.Commit()
                    End Using
                End Using

                dbSAM.BulkInsertAX(gSAM, NR, NSec, FirstYear, LastYear - FirstYear + 1)
            End Using

        Catch ex As Exception
            'jrs 2023/11/06 Adding Try to catch problems saving NaN
            Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "ErrorSce.txt", append:=True)
            objWriter.WriteLine(ex.Message)
            objWriter.Close()

        End Try


    End Sub

    'jrs 2019/11/20
    Private Shared Async Function SaveAllAsync(ByVal source As ISourceBlock(Of Action)) As Task
        While Await source.OutputAvailableAsync()
            Dim saveOne As Action = source.Receive()
            saveOne()
        End While
    End Function
End Class
