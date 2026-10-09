Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports Microsoft.Extensions.DependencyInjection
Imports Xunit
Imports IFs.Core
Imports IFs.Data

<Collection("IFs.Test.Core")>
Public Class ForecastTestSuite
    Implements IClassFixture(Of ServiceFixture)

    Private ReadOnly Property services As IServiceProvider

    Public Sub New(fixture As ServiceFixture)
        Me.services = fixture.Services
    End Sub

    ' <Theory>
    ' <InlineData("POP", 189)>
    ' <InlineData("EDBASENRG", 567)>
    ' <InlineData("LIFEXP", 567)> ' <time, region, gender, value>
    ' <InlineData("WPOP", 1)> ' <time, value>
    ' <InlineData("GDP", 189)> ' <time, region, value>
    ' <InlineData("EDTERENRG", 567)> ' <time, region, ?, value>
    ' <InlineData("HLDALY", 8505)> ' <time, region, ?, ?, value>
    ' Sub TestForecastService_rebuild_base(variableName As String, rowCount As Integer)
    '     Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
    '     Dim storageSvc As IStorageService = Me.services.GetService(Of IStorageService)()
    '     Dim forecastSvc As IForecastService = Me.services.GetService(Of IForecastService)()
    '     Using run As IStorage = storageSvc.Open(True)
    '         run.SaveDataframe(forecastSvc.Rebuild(variableName))
    '         run.Flush()
    '         Dim df As IDataframe = run.LoadDataframe(variableName)
    '         If variableName = "POP" Then
    '             ' get the first row (POP of Afghanistan)
    '             Dim row1 As IDataframeRecord = df.First()
    '             Assert.Equal({2015, 1}, row1.Key)
    '             Assert.True(row1.Value.HasValue)
    '             Assert.InRange(row1.Value.Value, 33.73f, 33.74f)
    '             ' get the last row (POP of Zimbabwe)
    '             Dim rowN As IDataframeRecord = df.Skip(df.RowCount - 1).First()
    '             Assert.Equal({2015, 189}, rowN.Key)
    '             Assert.True(rowN.Value.HasValue)
    '             Assert.InRange(rowN.Value.Value, 15.77f, 15.78f)
    '             ' get an array of rows for (POP of Algeria, Angola)
    '             Dim rows As IDataframeRecord() = df.Skip(2).Take(2).ToArray()
    '             Assert.Equal(2, rows.Length)
    '             Assert.Equal({2015, 3}, rows(0).Key)
    '             Assert.True(rows(0).Value.HasValue)
    '             Assert.InRange(rows(0).Value.Value, 39f, 40f)
    '             ' search for USA
    '             Dim country As IDimension = run.GetDimension(1)
    '             Dim regionUSA As Short = country.Buckets.ToDictionary(Function (b) b.Value, Function (b) b.Key)("USA")
    '             Dim rowUSA As IDataframeRecord = df.Filter(country.EQ(regionUSA)).Execute().First()
    '             Assert.Equal({2015, 182}, rowUSA.Key)
    '             Assert.True(rowUSA.Value.HasValue)
    '             Assert.InRange(rowUSA.Value.Value, 319f, 320f)
    '         Else If variableName = "LIFEXP" Then
    '             Assert.True(df(2015S, 1S, 1S).HasValue)
    '             Assert.True(df(2015S, 1S, 2S).HasValue)
    '             Assert.False(df(2015S, 1S, 3S).HasValue)
    '         End If
    '         Assert.Equal(rowCount, df.RowCount)
    '     End Using
    ' End Sub

    ' <Theory>
    ' <InlineData("POP", 2100S, 16254)>
    ' <InlineData("EDBASENRG", 2100S, 48762)>
    ' <InlineData("WPOP", 2100S, 86)> ' <time, value>
    ' <InlineData("GDP", 2100S, 16254)> ' <time, region, value>
    ' <InlineData("EDTERENRG", 2100S, 48762)> ' <time, region, ?, value>
    ' <InlineData("HLDALY", 2100S, 731430)> ' <time, region, ?, ?, value>
    ' Sub TestForecastService_run_model(variableName As String, horizonYear As Short, rowCount As Integer)
    '     Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
    '     Dim storageSvc As IStorageService = Me.services.GetService(Of IStorageService)()
    '     Dim forecastSvc As IForecastService = Me.services.GetService(Of IForecastService)()
    '     Using run As IStorage = storageSvc.Open(True)
    '         run.SaveDataframe(forecastSvc.Rebuild(variableName))
    '         run.Flush()
    '         run.SaveDataframe(forecastSvc.Forecast(variableName, horizonYear, run))
    '         run.Flush()
    '         Dim df As IDataframe = run.LoadDataframe(variableName)
    '         Assert.Equal(rowCount, df.RowCount)
    '     End Using
    ' End Sub

    ' <Theory>
    ' <InlineData("POP", 2100S, {"2015"}, 189)>
    ' <InlineData("POP", 2100S, {"2015", "USA"}, 1)>
    ' <InlineData("EDBASENRG", 2100S, {"2020"}, 567)>
    ' <InlineData("EDBASENRG", 2100S, {"2020", "USA"}, 3)>
    ' <InlineData("EDBASENRG", 2100S, {"2020", "USA", "Male"}, 1)>
    ' Sub TestForecastService_query(variableName As String, horizonYear As Short, terms As String(), rowCount As Integer)
    '     Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
    '     Dim storageSvc As IStorageService = Me.services.GetService(Of IStorageService)()
    '     Dim forecastSvc As IForecastService = Me.services.GetService(Of IForecastService)()
    '     Using run As IStorage = storageSvc.Open(True)
    '         run.SaveDataframe(forecastSvc.Rebuild(variableName))
    '         run.Flush()
    '         run.SaveDataframe(forecastSvc.Forecast(variableName, horizonYear, run))
    '         run.Flush()
    '     End Using
    '     Using run As IStorage = storageSvc.Open(False)
    '         Dim query As IQueryBuilder = run.Query(variableName)
    '         For Each term In terms.Select(Function (value, index) (value, index))
    '             Dim dimension As IDimension = query.Dimensions(term.Item2)
    '             Dim operand As Short = dimension.Buckets.ToDictionary(Function (kv) kv.Value, Function (kv) kv.Key)(term.Item1)
    '             query = query.Filter(dimension.EQ(operand))
    '         Next
    '         Dim result As IDataframe = query.Execute()
    '         Assert.Equal(rowCount, result.RowCount)
    '     End Using
    ' End Sub

End Class
