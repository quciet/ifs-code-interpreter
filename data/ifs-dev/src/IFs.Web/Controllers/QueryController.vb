Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Mvc.Core
Imports Newtonsoft.Json
Imports IFs.Core
Imports IFs.Data

<Route("api/[controller]")>
<ApiController>
Public Class QueryController
    Inherits ControllerBase

    Private ReadOnly Property storageSvc As IStorageService

    Private ReadOnly Property querySvc As IQueryService

    Public Sub New (storage As IStorageService, query As IQueryService)
        Me.storageSvc = storage
        Me.querySvc = query
    End Sub

    ' GET api/query/{sVar}/_meta
    <HttpGet("{sVar}/_meta")>
    <ProducesResponseType(200)>
    <ProducesResponseType(404)>
    Public Function Probe(sVar As String) As ActionResult(Of String)
        Dim variable As IVariable
        Dim dimensions As IList(Of IDimension)
        ' query variable info
        Using working As IStorage = Me.storageSvc.Open(False)
            If Not working.VariableNames.Contains(sVar) Then
                Return NotFound()
            End If
            variable = working.GetVariable(sVar)
            dimensions = (From i In variable.Dimensions Select working.GetDimension(i)).ToList()
        End Using
        ' prepare response
        Dim response As IDictionary(Of String, Object) = New Dictionary(Of String, Object) From {
            { "variable", variable.Name },
            { "dimensions", (From d In dimensions Select New Dictionary(Of String, Object) From {
                    { "id", d.Id },
                    { "buckets", d.Buckets.ToDictionary(Of String, Integer)(Function (kv) kv.Value, Function (kv) kv.Key) }
                })
            },
            { "dimensionCount", variable.Dimensions.Count }
        }
        ' serialize to JSON and return
        Return JsonConvert.SerializeObject(response)
    End Function

    ' GET api/query/{sVar}/{iFirstYear},{iLastYear}/?sDim={sDim(0)},{sDim(1)},...
    <HttpGet("{sVar}/{iFirstYear},{iLastYear}")>
    <ProducesResponseType(200)>
    <ProducesResponseType(400)>
    <ProducesResponseType(404)>
    Public Function Query(sVar As String, iFirstYear As Short, iLastYear As Short, <FromQuery> sDim As String) As ActionResult(Of String)
        Dim terms As String()
        ' prepare query terms
        Using working As IStorage = Me.storageSvc.Open(False)
            If Not working.VariableNames.Contains(sVar) Then
                Return NotFound()
            End If
            If sDim IsNot Nothing Then
                terms = sDim.Split(",").ToArray()
            Else
                terms = {}
            End If
        End Using
        ' query dataframe
        Dim df As IDataframe = Me.querySvc.Query(sVar, iFirstYear, iLastYear, terms)
        ' prepare response
        Dim response As IDictionary(Of String, Object) = New Dictionary(Of String, Object) From {
            { "variable", df.Variable.Name },
            { "dimensions", (From d In df.Dimensions Select d.Id) },
            { "content", df },
            { "rowCount", df.RowCount },
            { "query", New Dictionary(Of String, Object) From {
                    { "iFirstYear", iFirstYear },
                    { "iLastYear", iLastYear },
                    { "sDim", terms }
                }
            }
        }
        ' serialize to JSON and return
        Return JsonConvert.SerializeObject(response)
    End Function

End Class
