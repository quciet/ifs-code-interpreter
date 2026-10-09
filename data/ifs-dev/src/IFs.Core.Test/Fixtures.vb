Imports System
Imports Microsoft.Extensions.DependencyInjection
Imports Xunit
Imports IFs.Core
Imports IFs.Data

Public Class ServiceFixture
    Implements IDisposable

    Public ReadOnly Property Services As IServiceProvider

    Public Sub New()
        Me.Services = New ServiceCollection().
            AddScoped(Of IContextService, ContextService)().
            AddScoped(Of IStorageService, StorageService)().
            AddScoped(Of IForecastService, ForecastService)().
            BuildServiceProvider()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' NOP
    End Sub

End Class
