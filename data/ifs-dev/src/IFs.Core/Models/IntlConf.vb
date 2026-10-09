Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data

Namespace Models

    <IFsModel("IntlConf")>
    Partial Public Module IntlConf

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
