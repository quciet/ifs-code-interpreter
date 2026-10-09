Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Core
Imports IFs.Data

Public Class QueryService
    Implements IQueryService

    Private ReadOnly Property storageSvc As IStorageService

    Private ReadOnly Property termIndices As IDictionary(Of Integer, IReadOnlyDictionary(Of String, Short))

    Public Sub New(storage As IStorageService)
        MyBase.New()
        Me.storageSvc = storage
        Me.termIndices = New Dictionary(Of Integer, IReadOnlyDictionary(Of String, Short))()
    End Sub

    Function Query(sVar As String, iFirstYear As Short, iLastYear As Short, ParamArray sDim() As String) As IDataframe Implements IQueryService.Query
        Using working As IStorage = storageSvc.Open(False)
            Dim builder As IQueryBuilder = working.Query(sVar)
            Dim dimension As IDimension = builder.Dimensions(0)
            builder = builder.Filter(dimension.BTWN(iFirstYear, iLastYear))
            For Each term In sDim.Select(Function (value, seq) (value, seq + 1))
                If term.Item2 >= builder.Dimensions.Count Then
                    Throw New ArgumentException($"Invalid dimension at position {term.Item2}.")
                End If
                dimension = builder.Dimensions(term.Item2)
                Dim index As IReadOnlyDictionary(Of String, Short) = Nothing
                If Not termIndices.TryGetValue(dimension.Id, index) Then
                    index = dimension.Buckets.ToDictionary(Function (kv) kv.Value, Function (kv) kv.Key)
                    termIndices.Add(dimension.Id, index)
                End If
                Dim operand As Short = Nothing
                If Not index.TryGetValue(term.Item1, operand) Then
                    Throw New ArgumentException($"Invalid query term for dimension {term.Item2}: '{term.Item1}'.")
                End If
                builder = builder.Filter(dimension.EQ(operand))
            Next
            Return builder.Execute()
        End Using
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        ' NOP
    End Sub

End Class
