Namespace IFsWeb

Public Class EqualCountClassifier
    Inherits Classifier

    'FindClassBreaks
    'Find the class breaks in a list of data values so that there is an approximately an equal number of 
    'cases in each class.

    Public Function FindClassBreaks(ByVal layerValues() As Double, ByVal numberofClasses As Integer) As Double()
        Dim valuesLength As Double = layerValues.Length
        
        'Find the closest integer count for each class
        Dim roughCount As Integer = Math.Floor(CDbl(valuesLength / numberofClasses))
        'Find the remainder of the number of cases divided by the number of classes
        'jrs 09/29/04 Dim remainder As Integer = layerValues.Length Mod roughCount
        Dim remainder As Integer = valuesLength Mod roughCount

        'Create an array for holding the number of cases in each class
        Dim classCounts(numberofClasses) As Integer
        Dim i As Integer
        'Assign an equal number of cases to each class
        'Initialize each array member to the closest integer count
        For i = 0 To classCounts.Length - 1
            classCounts(i) = roughCount
        Next
        If remainder > 0 Then
            'Add 1 to random positions until the remainder is used.
            Randomize()
            Dim randomPosition As Integer
            While remainder > 0
                randomPosition = CInt((classCounts.Length - 2) * Rnd() + 1)
                classCounts(randomPosition) += 1
                remainder -= 1
            End While
        End If
        'Sort the list
        Array.Sort(layerValues)
        'Build the list of class breaks based on the number of cases in each class
        Dim breaks(numberofClasses) As Double
        Dim break As Double = layerValues(0)
        breaks(0) = break
        Dim interval As Integer
        Dim currentPosition As Integer = 0
        Dim j As Integer
        For j = 1 To classCounts.Length - 1
            interval = classCounts(j)
            currentPosition += interval
            break = layerValues(currentPosition - 1)
            breaks(j) = break
        Next
        'Return a list of class breaks.
        Return breaks
    End Function

End Class

End Namespace
