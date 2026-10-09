'Namespace IFsWeb

Public MustInherit Class Classifier

    Protected Overloads Function GetMax(ByVal layerValues() As Double) As Double
        Dim i As Integer
        Dim max As Double

        max = layerValues(0)
        For i = 1 To layerValues.Length - 1
            Dim test As Double = layerValues(i)
            If test > max Then
                max = test
            End If
        Next
        Return max
    End Function

    Protected Overloads Function GetMin(ByVal layerValues() As Double) As Double
        Dim i As Integer
        Dim min As Double = GetMax(layerValues)

        For i = 0 To layerValues.Length - 1
            If IsNumeric(layerValues(i)) Then
                Dim test As Double = layerValues(i)
                If test < min Then
                    min = test
                End If
            End If
        Next
        Return min
    End Function

    Protected Overloads Function GetMax(ByVal layerValues() As Integer) As Integer
        Dim i As Integer
        Dim max As Integer

        max = layerValues(0)
        For i = 1 To layerValues.Length - 1
            Dim test As Integer = layerValues(i)
            If test > max Then
                max = test
            End If
        Next
        Return max
    End Function

    Protected Overloads Function getmin(ByVal layerValues() As Integer) As Integer
        Dim i As Integer
        Dim min As Integer = layerValues(0)

        For i = 1 To layerValues.Length - 1
            Dim test As Integer = layerValues(i)
            If test < min Then
                min = test
            End If
        Next
        Return min
    End Function
End Class

'End Namespace
