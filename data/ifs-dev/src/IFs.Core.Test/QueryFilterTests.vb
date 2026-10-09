Imports Microsoft.Extensions.DependencyInjection
Imports Xunit
Imports IFs.Data

<Collection("IFs.Test.Core")>
Public Class QueryFilterTestSuite
    Implements IClassFixture(Of ServiceFixture)

    Private ReadOnly Property services As IServiceProvider

    Public Sub New(fixture As ServiceFixture)
        Me.services = fixture.Services
    End Sub

    <Theory>
    <InlineData(0, FilterType.Equal, 2015S)>
    <InlineData(0, FilterType.NotEqual, 2015S)>
    <InlineData(0, FilterType.ElementOf, 2015S, 2016S, 2100S)>
    Sub TestFilter_create(dimensionId As Integer, filterType As FilterType, ParamArray operands() As Short)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            Dim dimension As IDimension = context.GetDimension(dimensionId)
            Dim pred As IQueryFilter = QueryFilter.Create(filterType, dimension, operands)
            Assert.Equal(filterType, pred.Type)
            Assert.Equal(dimension.Id, pred.Dimension.Id)
            Assert.Equal(operands, pred.Operands)
        End Using
    End Sub

    <Theory>
    <InlineData(0, 2015S, 2016S, 2017S)>
    <InlineData(1, 1S, 2S, 3S)>
    Sub TestFilter_logical(dimensionId As Integer, v1 As Short, v2 As Short, v3 As Short)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()

            Dim dimension As IDimension = context.GetDimension(dimensionId)

            Dim D As HashSet(Of Short) = New HashSet(Of Short)(dimension.Buckets.Keys)

            Dim EMPTY As IQueryFilter = dimension.EMPTY()
            Assert.Equal({}, EMPTY.Operands)
            Dim EQ_v1 As IQueryFilter = dimension.EQ(v1)
            Dim EQ_v2 As IQueryFilter = dimension.EQ(v2)
            Dim EQ_v3 As IQueryFilter = dimension.EQ(v3)
            Dim NEQ_v1 As IQueryFilter = dimension.NEQ(v1)
            Dim NEQ_v2 As IQueryFilter = dimension.NEQ(v2)
            Dim ANY_v1_v2 As IQueryFilter = dimension.ANY(v1, v2)
            Dim ANY_v2_v3 As IQueryFilter = dimension.ANY(v3, v3)
            Assert.Equal({ v1, v2 }, ANY_v1_v2.Operands)
            Dim BTWN As IQueryFilter = dimension.BTWN(v1, v3)
            Assert.Equal({ v1, v3 }, BTWN.Operands)

            Assert.True(EQ_v1.Equal(EQ_v1.Conjunct(EQ_v1)))    ' EQ(v1) & EQ(v1) = EQ(v1)
            Assert.True(NEQ_v1.Equal(NEQ_v1.Conjunct(NEQ_v1))) ' NEQ(v1) & NEQ(v1) = NEQ(v1)
            Assert.True(EMPTY.Equal(EQ_v1.Conjunct(NEQ_v1)))   ' EQ(v1) & NEQ(v1) = EMPTY
            Assert.True(EMPTY.Equal(NEQ_v1.Conjunct(EQ_v1)))   ' NEQ(v1) & EQ(v1) = EMPTY

            Assert.Equal({}, EQ_v1.Conjunct(EQ_v2).Operands)      ' EQ(v1) & EQ(v2) = {}
            Assert.Equal({ v2 }, NEQ_v1.Conjunct(EQ_v2).Operands) ' NEQ(v1) & EQ(v2) = { v2 }
            Assert.Equal({ v1 }, EQ_v1.Conjunct(NEQ_v2).Operands) ' EQ(v1) & NEQ(v2) = { v1 }
            Assert.Equal({ v3, D.Max() }, NEQ_v1.Conjunct(NEQ_v2).Operands) ' NEQ(v1) & NEQ(v2) = D \ { v1, v2} = BTWN(v3, max)

            Assert.True(EQ_v1.Equal(EQ_v1.Disjunct(EQ_v1)))    ' EQ(v1) | EQ(v1) = EQ(v1)
            Assert.True(NEQ_v1.Equal(NEQ_v1.Disjunct(NEQ_v1))) ' NEQ(v1) | NEQ(v1) = NEQ(v1)
            Assert.Null(EQ_v1.Disjunct(NEQ_v1))                ' EQ(v1) | NEQ(v1) = Nothing (unlimited)
            Assert.Null(NEQ_v1.Disjunct(EQ_v1))                ' NEQ(v1) | EQ(v1) = Nothing (unlimited)

            Assert.True(ANY_v1_v2.Equal(EQ_v1.Disjunct(EQ_v2))) ' EQ(v1) | EQ(v2) = {v1, v2}
            Assert.True(NEQ_v1.Equal(NEQ_v1.Disjunct(EQ_v2)))   ' NEQ(v1) | EQ(v2) = NEQ(v1)
            Assert.True(NEQ_v2.Equal(EQ_v1.Disjunct(NEQ_v2)))   ' EQ(v1) | NEQ(v2) = NEQ(v2)
            Assert.Null(NEQ_v1.Disjunct(NEQ_v2))                ' NEQ(v1) | NEQ(v2) = Nothing (unlimited)

            Assert.True(BTWN.Equal(EQ_v3.Disjunct(ANY_v1_v2)))  ' EQ(v3) | ANY(v1, v2) = BTWN(v1, v3)
            Assert.True(BTWN.Equal(EQ_v1.Disjunct(EQ_v2).Disjunct(EQ_v3)))  ' EQ(v1) | EQ(v2) | EQ(v3) = BTWN(v1, v3)
            Assert.True(BTWN.Equal(EQ_v1.Disjunct(EQ_v2).Disjunct(EQ_v3)))  ' EQ(v1) | EQ(v2) | EQ(v3) = BTWN(v1, v3)
            Assert.True(BTWN.Equal(ANY_v1_v2.Disjunct(ANY_v2_v3)))  ' ANY(v1, v2) | ANY(v2, v3) = BTWN(v1, v3)
        End Using
    End Sub

    <Theory>
    <InlineData(0, 2015S, 2015S, True)>
    <InlineData(0, 2015S, 2016S, False)>
    <InlineData(0, 2015S, 2017S, False)>
    <InlineData(1, 1S, 1S, True)>
    <InlineData(1, 1S, 2S, False)>
    <InlineData(1, 1S, 3S, False)>
    Sub TestFilter_EQ_matcher(dimensionId As Integer, target As Short, value As Short, result As Boolean)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            Dim dimension As IDimension = context.GetDimension(dimensionId)
            Dim matcher As Func(Of Short, Boolean) = dimension.EQ(target).Matcher
            Assert.Equal(result, matcher(value))
        End Using
    End Sub

    <Theory>
    <InlineData(0, 2015S, 2015S, False)>
    <InlineData(0, 2015S, 2016S, True)>
    <InlineData(0, 2015S, 2017S, True)>
    <InlineData(1, 1S, 1S, False)>
    <InlineData(1, 1S, 2S, True)>
    <InlineData(1, 1S, 3S, True)>
    Sub TestFilter_NEQ_matcher(dimensionId As Integer, target As Short, value As Short, result As Boolean)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            Dim dimension As IDimension = context.GetDimension(dimensionId)
            Dim matcher As Func(Of Short, Boolean) = dimension.NEQ(target).Matcher
            Assert.Equal(result, matcher(value))
        End Using
    End Sub

    <Theory>
    <InlineData(0, {2015S, 2016S, 2017S}, 2015S, True)>
    <InlineData(0, {2015S, 2016S, 2017S}, 2020S, False)>
    <InlineData(0, {2015S}, 2015S, True)> ' reduce to EQ
    <InlineData(0, {2015S}, 2016S, False)> ' reduce to EQ
    <InlineData(1, {1S, 2S}, 1S, True)>
    <InlineData(1, {1S, 2S}, 3S, False)>
    <InlineData(1, {3S}, 3S, True)> ' reduce to EQ
    <InlineData(1, {3S}, 2S, False)> ' reduce to EQ
    Sub TestFilter_ANY_matcher(dimensionId As Integer, targets As Short(), value As Short, result As Boolean)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            Dim dimension As IDimension = context.GetDimension(dimensionId)
            Dim matcher As Func(Of Short, Boolean) = dimension.ANY(targets).Matcher
            Assert.Equal(result, matcher(value))
        End Using
    End Sub

    <Theory>
    <InlineData(0, 2015S, False)>
    <InlineData(1, 1S, False)>
    Sub TestFilter_EMPTY_matcher(dimensionId As Integer, value As Short, result As Boolean)
        Dim contextSvc As IContextService = Me.services.GetService(Of IContextService)()
        Using context As IRebuildContext = contextSvc.ForRebuild()
            Dim dimension As IDimension = context.GetDimension(dimensionId)
            Dim matcher As Func(Of Short, Boolean) = dimension.EMPTY().Matcher
            Assert.Equal(result, matcher(value))
        End Using
    End Sub

End Class
