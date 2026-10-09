'Namespace IFsWeb

Public Class MyHistAnalogs
    'jrs 2006/06/05
    'Class created originally to handle History plus forecast with 3 dimensions
    Private colHistAnalog As New Collection

    ' Add method creates a new HistAnalog in
    ' ---       the collection.
    '
    Public Function Add(ByVal Name As String, _
    ByVal Dim2 As Object, ByVal Dim3 As Object, ByVal HistAna As String) As MyHistAnalog
        Dim haNew As New MyHistAnalog
        'Static intEmpNum As Integer
        ' Using With shortens property references (.ID instead of empNew.ID)
        ' and speeds up execution.
        With haNew
            ' Generate a unique ID for the new employee.
            .VarName = Name
            If Not (Dim2 Is Nothing) Then
                If Dim2 > "" Then
                    .Dim2 = Dim2
                    If Not (Dim3 Is Nothing) Then
                        If Dim3 > "" Then
                            .Dim3 = Dim3
                        End If
                    End If
                End If
            End If
            .sHistAna = HistAna

            Dim sKey As String
            sKey = BuildKey(Name, Dim2, Dim3)

            'jrs 2005/04/12 handle repeated keys
            On Error Resume Next
            ' Add the Variable object reference to the collection, using its
            ' name property as the key.
            colHistAnalog.Add(haNew, sKey)
        End With
        ' It is good practice to return a reference to the new member.
        Add = haNew
    End Function

    Public Function Count() As Long
        Count = colHistAnalog.Count
    End Function

    Public Sub Delete(ByVal Index As Object)
        colHistAnalog.remove(Index)
    End Sub

    ' To see how the Item method was made the
    '   default method for the Employees
    '   collection, select Procedure Attributes
    '   from the Tools menu.  In the Name box,
    '   select Item, then click the Advanced
    '   button.  You can see that (Default)
    '   has been selected in the Procedure ID
    '   box.
    Public Function Item(ByVal Name As String, ByVal Dim2 As String, ByVal Dim3 As String) As MyHistAnalog
        Dim sKey As String
        sKey = BuildKey(Name, Dim2, Dim3)

        'jrs 2005/05/19
        On Error GoTo EmptyHdl
        Item = colHistAnalog.Item(sKey)
        Exit Function
EmptyHdl:
        Item = Me.Add(Name, Dim2, Dim3, "")
    End Function

    Private Function BuildKey(ByVal sVar As String, ByVal Dim2 As Object, ByVal Dim3 As Object) As String
        BuildKey = sVar
        If Not (Dim2 Is Nothing) Then
            If Dim2 > "" Then
                BuildKey = BuildKey & "," & Dim2
                If Not (Dim3 Is Nothing) Then
                    If Dim3 > "" Then
                        BuildKey = BuildKey & "," & Dim3
                    End If
                End If
            End If
        End If
    End Function

End Class

'End Namespace
