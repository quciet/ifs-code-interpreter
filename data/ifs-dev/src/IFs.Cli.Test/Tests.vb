Imports System
Imports System.Diagnostics
Imports Microsoft.Extensions.DependencyInjection
Imports Xunit
Imports IFs.Cli
Imports IFs.Core
Imports IFs.Data

<Collection("IFs.Test.Cli")>
Public Class CliTestSuite
    Implements IClassFixture(Of ServiceFixture)

    Private ReadOnly Property services As IServiceProvider

    Sub New(fixture As ServiceFixture)
        Me.services = fixture.Services 
    End Sub

    <Fact>
    Sub TestContextService_init_params()
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            ' init params
            Assert.False(context.HistoricMode)
            Assert.Equal(2015, context.FirstYear)
            Assert.Equal(2100, context.LastYear)
        End Using
    End Sub

    <Fact>
    Sub TestContextService_hist_series()
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            context.PutAggregator("POP", ContextService.SUM(CoalescePolicy.Null))
            context.PutAggregator("EDBASENRG", ContextService.AVG(CoalescePolicy.Null))
            ' historical series
            Dim s1 As IDictionary(Of Short(), Single?) = context.GetHistSeries("POP")
            Assert.Equal(189, s1.Count)
            Dim s2 As IDictionary(Of Short(), Single?) = context.GetHistSeries("EDBASENRG", "Male")
            Assert.Equal(189, s2.Count)
        End Using
    End Sub

    <Fact>
    Sub TestContextService_variables_total()
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            Assert.Equal(1489, context.VariableNames.Count)
            Assert.Equal(1489, context.Variables.Count())
        End Using
    End Sub

    <Theory>
    <InlineData("POP", "Population", 2, {0, 1})>
    <InlineData("EDBASENRG", "Gross Basic Education Enrollment Rate", 2, {0, 1, 23})>
    Sub TestContextService_variables_info(name As String, displayName As String, type As Integer, dimSeq() As Integer)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            ' variables
            Dim v As IVariable = context.GetVariable(name)
            Assert.Equal(name, v.Name)
            Assert.Equal(displayName, v.DisplayName)
            Assert.Equal(type, v.Type)
            Assert.Equal(dimSeq, v.Dimensions)
        End Using
    End Sub

    <Fact>
    Sub TestContextService_dimensions()
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            ' dimensions
            Assert.Equal(53, context.DimensionIds.Count)
            Assert.Equal(53, context.Dimensions.Count())
            Assert.Equal(2100 - 2015 + 1, context.GetDimension(0).Buckets.Count)
            Assert.Equal(189, context.GetDimension(1).Buckets.Count)
            Assert.Equal(6, context.GetDimension(2).Buckets.Count)
        End Using
    End Sub

    <Theory>
    <InlineData("POP", "Population", 2, {0, 1})>
    <InlineData("EDBASENRG", "Gross Basic Education Enrollment Rate", 2, {0, 1, 23})>
    Sub TestStorageService_variables_info(name As String, displayName As String, type As Integer, dimSeq() As Integer)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Dim storageSvc As IStorageService = Me.services.GetService(Of IStorageService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            ' variables
            Using working As IStorage = storageSvc.Open(True, context)
                Dim v As IVariable = working.GetVariable(name)
                Assert.Equal(name, v.Name)
                Assert.Equal(displayName, v.DisplayName)
                Assert.Equal(type, v.Type)
                Assert.Equal(dimSeq, v.Dimensions)
            End Using
        End Using
    End Sub

    <Fact>
    Sub TestStorageService_dimensions()
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Dim storageSvc As IStorageService = Me.services.GetService(Of IStorageService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            ' dimensions
            Using working As IStorage = storageSvc.Open(True, context)
                Assert.Equal(53, working.DimensionIds.Count)
                Assert.Equal(53, working.Dimensions.Count())
                Assert.Equal(2100 - 2015 + 1, working.GetDimension(0).Buckets.Count)
                Assert.Equal(189, working.GetDimension(1).Buckets.Count)
                Assert.Equal(6, working.GetDimension(2).Buckets.Count)
            End Using
        End Using
    End Sub

    <Fact>
    Sub TestForecastService() 
        Dim forecastSvc As IForecastService = Me.services.GetService(Of IForecastService)()
        ' TODO
    End Sub

    <Theory>
    <InlineData("3", "2100", "-1", "false", "false", "1")> ' invalid mode
    <InlineData("5", "22000", "-1", "false", "false", "1")> ' invalid horizon
    <InlineData("5", "2100", "-1", "false", "false", "3")> ' invalid process
    Sub TestArguments_invalid(ParamArray args() As String)
        Assert.Throws(Of ArgumentException)(Function () Arguments.Parse(args, False))
    End Sub

    <Theory>
    <InlineData({"5", "2050", "-1", "false", "false", "0"}, 5, 2050, -1, False, False, 0)> ' RebBase Forecast (2015-2050)
    <InlineData({"5", "2050", "-1", "false", "false", "0", "--log", "debug.log"}, 5, 2050, -1, False, False, 0, False, "debug.log")> ' RebBase Forecast (2015-2050) with Logging
    <InlineData({"5", "2100", "-1", "false", "false", "0", "false"}, 5, 2100, -1, False, False, 0, False)> ' RebBase Forecast (2015-2100)
    <InlineData({"5", "2000", "-1", "false", "false", "0", "true"}, 5, 2000, -1, False, False, 0, True)> ' RebBase Historic (1960-2000)
    <InlineData({"5", "2015", "-1", "false", "false", "0", "true"}, 5, 2015, -1, False, False, 0, True)> ' RebBase Historic (1960-2015)
    <InlineData({"5", "2050", "-1", "false", "false", "1"}, 5, 2050, -1, False, False, 1)> ' ModelRun Forecast (2015-2050)
    <InlineData({"5", "2100", "-1", "false", "false", "1", "false"}, 5, 2100, -1, False, False, 1, False)> ' ModelRun Forecast (2015-2100)
    <InlineData({"5", "2000", "-1", "false", "false", "1", "true"}, 5, 2000, -1, False, False, 1, True)> ' ModelRun Historic (1960-2000)
    <InlineData({"5", "2015", "-1", "false", "false", "1", "true"}, 5, 2015, -1, False, False, 1, True)> ' ModelRun Historic (1960-2015)
    Sub TestArguments_good(args() As String, mode As Integer, horizon As Integer, session As Integer, monitor As Boolean, verbose As Boolean, process As Integer, Optional historic As Boolean = False, Optional logfile As String = Nothing)
        Dim input As Arguments = Arguments.Parse(args, False)
        Assert.Equal(mode, input.Mode)
        Assert.Equal(horizon, input.Horizon)
        Assert.Equal(session, input.Session)
        Assert.Equal(monitor, input.Monitor)
        Assert.Equal(verbose, input.Verbose)
        Assert.Equal(process, input.Process)
        Assert.Equal(historic, input.Historic)
        If input.Log IsNot Nothing Then
            Assert.Equal(logfile, input.Log.Name)
        End If
    End Sub

    <Fact>
    Sub TestApplication()
        Dim vanilla As Application = New Application(Me.services)
        ' TODO
    End Sub

End Class
