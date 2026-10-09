Imports System
Imports IFs.Data

Namespace Models

    <IFsModel("Environment")>
    Partial Public Module Environment

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

