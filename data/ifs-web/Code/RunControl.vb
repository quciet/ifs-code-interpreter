'Namespace IFsWeb

Public Class RunControl
    Private Const MaxSimultRuns As Integer = 4
    Private SimultRuns As Integer

    Public Sub New()
        SimultRuns = 0
    End Sub

    Public Function RunsAvailable() As Boolean
        RunsAvailable = (SimultRuns < MaxSimultRuns)
    End Function

    Public Function RunsZero() As Boolean
        RunsZero = (SimultRuns = 0)
    End Function

    Public Function RunCount() As Integer
        RunCount = SimultRuns
    End Function

    Public Sub IncreaseCounter()
        SimultRuns = SimultRuns + 1
    End Sub

    Public Sub DecreaseCounter()
        SimultRuns = SimultRuns - 1
    End Sub
End Class

'End Namespace
