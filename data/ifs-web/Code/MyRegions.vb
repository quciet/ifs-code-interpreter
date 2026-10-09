Imports NLog

Public Class MyRegions

    Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

    Private colRegions As New Collection

    ' Add method creates a new Region in
    ' ---       the collection.
    '
    'jrs 2011/08/02 added ESRI
    Public Function Add(ByVal Name As String,
                    ByVal Index As Integer,
                        ByVal ISOCode As String,
                        ByVal sESRI As String,
                        ByVal sAlias As String) As MyRegion  'jrs 2016/05/27 added sAlias 'jrs 2011/04/01 
        Try
            Dim regNew As New MyRegion
            'Static intEmpNum As Integer
            ' Using With shortens property references (.ID instead of empNew.ID)
            ' and speeds up execution.
            With regNew
                ' Generate a unique ID for the new employee.
                .Name = Name
                .Index = Index
                'jrs 2016/05/27
                .sAlias = sAlias
                'jrs 2011/04/01
                .ISO3166_2Code = ISOCode
                'jrs 2011/08/02
                .ESRI = sESRI

                'jrs 2005/04/12 handle repeated keys
                'On Error Resume Next
                ' Add the Region object reference to the collection, using its
                ' name property as the key.
                colRegions.Add(regNew, .Name)
            End With
            ' It is good practice to return a reference to the new member.
            Add = regNew
        Catch Ex As Exception
            _logger.Error(Ex, "MyRegions.Add exception" + Name)
            Return Nothing
        End Try

    End Function


    Public Function Count() As Long
        Count = colRegions.Count
    End Function

    Public Sub Delete(ByVal Index As Object)
        colRegions.Remove(Index)
    End Sub

    ' To see how the Item method was made the
    '   default method for the Employees
    '   collection, select Procedure Attributes
    '   from the Tools menu.  In the Name box,
    '   select Item, then click the Advanced
    '   button.  You can see that (Default)
    '   has been selected in the Procedure ID
    '   box.
    Public Function Item(ByVal Index As Object) As MyRegion
        'jrs 2006/07/21
        On Error GoTo EmptyHdl
        Item = colRegions.Item(Index)
        Exit Function
EmptyHdl:
        Item = Me.Add(Index, -1, "", "", "") 'jrs 2016/05/27
    End Function

    'jrs 2011/11/25
    Public Sub setCentroid(ByVal Index As String, ByVal xLongitud As Single, ByVal yLatitude As Single)
        Item(Index).longitude = xLongitud
        Item(Index).latitude = yLatitude
    End Sub

    'jrs 2011/11/25
    Public Sub setZoom(ByVal Index As String, ByVal lZoom As Integer)
        Item(Index).zoom = lZoom
    End Sub
End Class

