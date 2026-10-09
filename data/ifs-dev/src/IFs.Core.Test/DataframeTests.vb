Imports Microsoft.Extensions.DependencyInjection
Imports Xunit
Imports IFs.Data

<Collection("IFs.Test.Core")>
Public Class DataframeTestSuite
    Implements IClassFixture(Of ServiceFixture)

    Private ReadOnly Property services As IServiceProvider

    Public Sub New(fixture As ServiceFixture)
        Me.services = fixture.Services
    End Sub

    <Theory>
    <InlineData("POP", {2015S, 1S}, 3.5F)>
    <InlineData("EDBASENRG", {2015S, 1S, 1S}, 0.5F)>
    <InlineData("WPOP", {2015S}, 59.0F)> ' <time, value>
    Sub TestDataframe_append(variableName As String, key As Short(), value As Single?)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            Dim df As IDataframe = Dataframe.Create(variableName, context)
            df = df.Append(key.Cast(Of Object)().Concat({value}))
            Assert.Equal(1, df.RowCount)
            Assert.Equal(key, df.First().Key.AsEnumerable())
            Assert.Equal(value, df.First().Value)
            If df.Variable.Dimensions.Count = 1 Then
                df = df.Append({2016S, 60.0F}, {2017S, 61.0F})
            ElseIf df.Variable.Dimensions.Count = 2 Then
                df = df.Append({2015S, 2S, 0.1F}, {2015S, 3S, 0.2F})
            ElseIf df.Variable.Dimensions.Count = 3 Then
                df = df.Append(2015S, 3S, 4S, 0.15F)
                df = df.Append(2015S, 2S, 3S, 0.1F)
            End If
            Assert.Equal(2015S, df.First().First())
            Assert.Equal(3, df.RowCount)
        End Using
    End Sub

    <Theory>
    <InlineData("POP")>
    <InlineData("EDBASENRG")>
    <InlineData("WPOP")> ' <time, value>
    <InlineData("GDP")> ' <time, region, value>
    <InlineData("EDTERENRG")> ' <time, region, ?, value>
    <InlineData("HLDALY")> ' <time, region, ?, ?, value>
    Sub TestDataframe_empty(variableName As String)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Dim storageSvc As IStorageService = Me.services.GetService(Of IStorageService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            ' build dataframe
            Dim df1 As IDataframe = Dataframe.Create(variableName, context)
            Assert.Equal(0, df1.RowCount)
            ' save to storage
            Using working As IStorage = storageSvc.Open(True, context)
                working.SaveDataframe(df1)
                working.Flush()
                ' load back for verification
                Dim df2 As IDataframe = working.LoadDataframe(variableName)
                Assert.Equal(0, df2.RowCount)
            End Using
        End Using
    End Sub

    <Theory>
    <InlineData("POP", 189)>
    <InlineData("EDBASENRG", 567)>
    Sub TestDataframe_base_year(variableName As String, rowCount As Integer)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Dim storageSvc As IStorageService = Me.services.GetService(Of IStorageService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            ' build dataframe
            Dim year As IDimension = context.GetDimension(0)
            Dim dfOut As IDataframe = context.Query(variableName).Filter(
                year.EQ(context.FirstYear)).Execute()
            Assert.Equal(rowCount, dfOut.RowCount)
            ' save to storage
            Using working As IStorage = storageSvc.Open(True, context)
                working.SaveDataframe(dfOut)
                working.Flush()
                ' load back for verification
                Dim dfIn As IDataframe = working.LoadDataframe(variableName)
                Assert.Equal(dfOut.Variable.Name, dfIn.Variable.Name)
                Assert.Equal(rowCount, dfIn.RowCount)
            End Using
        End Using
    End Sub

End Class
