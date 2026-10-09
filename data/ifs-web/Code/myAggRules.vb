Imports NLog

'Namespace IFsWeb

Public Class myAggRules
        Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()
        'jrs 2006/09/27
        'Class created to handle generic aggregation rules
        'jrs 2022/12/16 made public to iterate
        Public colAggRules As New Collection


        ' Add method creates a new HistAnalog in
        ' ---       the collection.
        '
        Public Function Add(ByVal Name As String, ByVal ForecastVar As String,
    ByVal Dim2 As String, ByVal Dim3 As String, ByVal HisEmpVar As String) As myAggRule
            Try
                Dim arNew As New myAggRule
                'Static intEmpNum As Integer
                ' Using With shortens property references (.ID instead of empNew.ID)
                ' and speeds up execution.
                With arNew
                    .AggName = Name
                    .ForecastVar = ForecastVar
                    .HisEmpVar = HisEmpVar
                    .Dim2 = Dim2
                    .Dim3 = Dim3

                    'jrs 2005/04/12 handle repeated keys
                    ''On Error Resume Next
                    ' Add the Variable object reference to the collection, using its
                    ' name property as the key.
                    colAggRules.Add(arNew, Name)
                End With
                ' It is good practice to return a reference to the new member.
                Add = arNew
            Catch Ex As Exception
                _logger.Error(Ex, "myAggRules.Add exception" + Name + " - " + ForecastVar)
                Return Nothing
            End Try
        End Function

        Public Function Count() As Long
            Count = colAggRules.Count
        End Function

        Public Sub Delete(ByVal Index As Object)
            colAggRules.Remove(Index)
        End Sub

        ' To see how the Item method was made the
        '   default method for the Employees
        '   collection, select Procedure Attributes
        '   from the Tools menu.  In the Name box,
        '   select Item, then click the Advanced
        '   button.  You can see that (Default)
        '   has been selected in the Procedure ID
        '   box.
        Public Function Item(ByVal Name As String) As myAggRule

            'jrs 2005/05/19
            On Error GoTo EmptyHdl
            Item = colAggRules.Item(Name)
            Exit Function
EmptyHdl:
            Item = Me.Add(Name, "", "", "", "")
        End Function



    End Class

'End Namespace
