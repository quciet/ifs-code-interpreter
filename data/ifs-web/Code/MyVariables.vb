Imports NLog

'Namespace IFsWeb

Public Class MyVariables
        Private colVariables As New Collection

        Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

        ' Add method creates a new Variable in
        ' ---       the collection.
        '
        Public Function Add(ByVal Name As String,
                    ByVal Index As Integer) As MyVariable
            Try
                Dim varNew As New MyVariable
                'Static intEmpNum As Integer
                ' Using With shortens property references (.ID instead of empNew.ID)
                ' and speeds up execution.
                With varNew
                    ' Generate a unique ID for the new employee.
                    .Name = Name
                    .Index = Index
                    'jrs 2005/04/12 handle repeated keys
                    'On Error Resume Next
                    ' Add the Variable object reference to the collection, using its
                    ' name property as the key.
                    colVariables.Add(varNew, .Name)
                End With
                ' It is good practice to return a reference to the new member.
                Add = varNew
            Catch Ex As Exception
                _logger.Error(Ex, "MyVariables.Add exception" + Name)
                Return Nothing
            End Try
        End Function

        Public Function Count() As Long
            Count = colVariables.Count
        End Function

        Public Sub Delete(ByVal Index As Object)
            colVariables.Remove(Index)
        End Sub

        ' To see how the Item method was made the
        '   default method for the Employees
        '   collection, select Procedure Attributes
        '   from the Tools menu.  In the Name box,
        '   select Item, then click the Advanced
        '   button.  You can see that (Default)
        '   has been selected in the Procedure ID
        '   box.
        Public Function Item(ByVal Index As Object) As MyVariable
            'jrs 2021/04/29 removing on error
            ''jrs 2005/05/19
            'On Error GoTo EmptyHdl
            'jrs 2021/04/29 added contains to avoid exception
            If colVariables.Contains(Index) Then
                Item = colVariables.Item(Index)
            Else
                Item = Me.Add(Index, -1)
            End If
            '            Exit Function
            'EmptyHdl:
            '        Item = Me.Add(Index, -1)
        End Function

    End Class

'End Namespace
