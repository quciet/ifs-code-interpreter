'Namespace IFsWeb

Public Class UniqueValueClassifier
    Inherits Classifier

    Public Overloads Function FindClassBreaks(ByVal inputList() As Double) As Double()
        Dim outputArrayList As New ArrayList
        Dim i As Integer
        For i = 0 To inputList.Length - 1
            If Not outputArrayList.Contains(inputList(i)) Then
                outputArrayList.Add(inputList(i))
            End If
        Next
        Dim classBreaks(outputArrayList.Count - 1) As Double
        Dim k As Integer
        For k = 0 To classBreaks.Length - 1
            classBreaks(k) = outputArrayList(k)
        Next
        Array.Sort(classBreaks)
        Return classBreaks
    End Function

    Public Overloads Function FindClassBreaks(ByVal inputList() As String) As String()
        Dim outputArrayList As New ArrayList
        Dim i As Integer
        For i = 0 To inputList.Length - 1
            If Not outputArrayList.Contains(inputList(i)) Then
                outputArrayList.Add(inputList(i))
            End If
        Next
        Dim classBreaks(outputArrayList.Count - 1) As String
        Dim k As Integer
        For k = 0 To classBreaks.Length - 1
            classBreaks(k) = outputArrayList(k)
        Next
        Return classBreaks
    End Function

End Class

'End Namespace
