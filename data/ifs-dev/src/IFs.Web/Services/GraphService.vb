Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Core
Imports IFs.Data

Public Class GraphService
    Implements IGraphService

    Private ReadOnly Property forecastSvc As IForecastService

    Public Sub New(forecast As IForecastService)
        MyBase.New()
        Me.forecastSvc = forecast
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' NOP
    End Sub

End Class
