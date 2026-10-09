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
Public Class GraphController
    Inherits ControllerBase

    Private ReadOnly Property graphSvc As IGraphService

    Public Sub New (graph As IGraphService)
        Me.graphSvc = graph
    End Sub

    ' GET api/graph/{stage}
    <HttpGet("{sStage}")>
    <ProducesResponseType(200)>
    <ProducesResponseType(404)>
    Public Function Rebuild(sStage As String) As ActionResult(Of String)
        ' prepare response
        Dim response As IDictionary(Of String, Object) = New Dictionary(Of String, Object) From {
            { "stage", sStage}
        }
        ' serialize to JSON and return
        Return JsonConvert.SerializeObject(response)
    End Function

End Class
