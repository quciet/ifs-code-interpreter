Imports System
Imports IFs.Data

Namespace Models

<IFsModel("Economy")>
Public Partial Module Economy

    <IFsModelInit(ExecutionStage.Rebuild)>
    Public Sub Configure(context As IRebuildContext)
        ' NOP
    End Sub

    <IFsModelInit(ExecutionStage.Forecast)>
    Public Sub Configure(context As IForecastContext)
        ' NOP
    End Sub

End Module

End Namespace
