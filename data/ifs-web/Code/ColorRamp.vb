'Namespace IFsWeb

Public Class ColorRamp

    'BuildColorRamp
    'Generate a color ramp based on the number of classes and the color desired

    Public Function BuildColorRamp(ByVal numberOfColors As Integer, ByVal startColor As Integer, ByVal endColor As Integer) As Integer()
        Dim colorList(numberOfColors) As Integer
        'Define the starting color
        Dim stepColor As Integer = startColor
        colorList(0) = startColor
        'Define the step based on the range
        Dim range As Integer = endColor - startColor
        Dim colorStep As Integer = range / numberOfColors
        'Build a list of colors
        Dim i As Integer
        For i = 1 To colorList.Length - 2
            stepColor += colorStep
            colorList(i) = stepColor
        Next
        'Assign the end color to the last position in the array
        colorList(colorList.Length - 1) = endColor
        'Return the color list
        Return colorList
    End Function
End Class

'End Namespace
