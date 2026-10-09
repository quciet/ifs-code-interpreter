Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports IFs.Data
Imports IFs.Core

Public Partial Class ForecastService
    Inherits ForecastServiceBase

    Public Sub New()
        MyBase.New()
        Me.Load(GetType(Models.Commons))
        Me.Load(GetType(Models.Population))
        Me.Load(GetType(Models.Economy))
        Me.Load(GetType(Models.Agriculture))
        Me.Load(GetType(Models.Energy))
        Me.Load(GetType(Models.Education))
        Me.Load(GetType(Models.Infrastructure))
        Me.Load(GetType(Models.Environment))
        Me.Load(GetType(Models.Water))
        Me.Load(GetType(Models.SocioPol))
        Me.Load(GetType(Models.IntlConf))
        Me.Load(GetType(Models.StFailure))
        Me.Load(GetType(Models.ValuesData))
        Me.Load(GetType(Models.Automobile))
        Me.Load(GetType(Models.HealthMod))
        Me.Load(GetType(Models.Technology))
        ' TODO load additional models HERE AFTER
    End Sub

    Public Overrides Sub Dispose()
        ' NOP
    End Sub



End Class
