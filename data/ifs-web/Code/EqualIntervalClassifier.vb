'Namespace IFsWeb

Public Class EqualIntervalClassifier
    Inherits Classifier

    Public Function FindClassBreaks(ByVal layerValues() As Double, ByVal numberOfClasses As Integer) As Double()
        Dim classBreaks(numberOfClasses) As Double
        Array.Sort(layerValues)
        Dim min, max, range, interval, break As Double
        min = layerValues(0)
        max = layerValues(layerValues.Length - 1)
        'min = CDbl(GetMin(layerValues))
        'max = CDbl(GetMax(layerValues))
        range = Math.Abs(max - min)
        interval = CDbl(range / numberOfClasses)
        break = min
        classBreaks(0) = break
        Dim i As Integer
        For i = 1 To numberOfClasses
            break += interval
            classBreaks(i) = break
        Next
        Return classBreaks
    End Function

End Class

'End Namespace
