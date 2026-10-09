Imports System.Collections.ObjectModel
Imports NLog

'Namespace IFsWeb

Public Class MyFiles

        Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

        Public colMyFiles As New Collection

        ' Add method creates a new Region in
        ' ---       the collection.
        '
        Public Function Add(ByVal Name As String,
                        ByVal Visible As Boolean) As MyFile
            Dim FileNew As New MyFile
            Try
                'Static intEmpNum As Integer
                ' Using With shortens property references (.ID instead of empNew.ID)
                ' and speeds up execution.
                With FileNew
                    ' Generate a unique ID for the new employee.
                    .Name = Name
                    .Visible = Visible
                    'jrs 2005/04/12 handle repeated keys
                    'On Error Resume Next
                    ' Add the Region object reference to the collection, using its
                    ' name property as the key.
                    If (colMyFiles.Contains(.Name)) Then
                        _logger.Debug("MyFiles.Add - file already there " + .Name)
                        colMyFiles.Item(Name).Visible = Visible 'override when already there
                        'jrs 2021/05/28
                        colMyFiles.Item(Name).RegionsHaveChanged = False
                    Else
                        colMyFiles.Add(FileNew, .Name)
                    End If
                End With
                ' It is good practice to return a reference to the new member.
                Add = FileNew
            Catch Ex As Exception
                _logger.Error(Ex, "Myfiles.Add exception" + Name + " - " + Visible.ToString())
                Return Nothing
            End Try
        End Function

        Public Function Count() As Long
            Count = colMyFiles.Count
        End Function

        Public Sub Delete(ByVal Index As Object)
            colMyFiles.Remove(Index)
        End Sub

        ' To see how the Item method was made the
        '   default method for the Employees
        '   collection, select Procedure Attributes
        '   from the Tools menu.  In the Name box,
        '   select Item, then click the Advanced
        '   button.  You can see that (Default)
        '   has been selected in the Procedure ID
        '   box.

        'jrs 2014/01/14 added ScenarioDir and PutFdrvRun parameters
        Public Function Item(ByVal Index As Object, Optional ByVal ScenarioFileDir As String = "", Optional ByVal PutFdrvRun As String = "") As MyFile
            On Error GoTo addFile
            Item = colMyFiles.Item(Index)
            Exit Function
addFile:
            Item = Me.Add(Index, False)
            'jrs 2014/01/14
            If ScenarioFileDir > "" And PutFdrvRun > "" Then
                'Not only add the name, but check if necessary to copy
                Dim sSource As String
                Dim sTarget As String
                'Source is in Scenario Folder
                sSource = FindFile(ScenarioFileDir, Index, "run")
                'Target is in Runfiles 
                sTarget = PutFdrvRun & "\" & Index
                FileCopyCheck(sSource, sTarget)
                'jrs 2021/05/28
                If System.IO.File.Exists(sSource.Replace(Index, "SAM" & Index).Substring(0, sSource.Length) & "mdb") Then
                    'jrs 2021/01/04 also copy SAM and ifsPOP files
                    FileCopyCheck(sSource.Replace(Index, "SAM" & Index).Substring(0, sSource.Length) & "mdb", PutFdrvRun & "\SAM" & Index.replace(".run", ".mdb"))
                    FileCopyCheck(sSource.Replace(Index, "IFsPop" & Index).Substring(0, sSource.Length + 3) & "mdb", PutFdrvRun & "\IFsPop" & Index.replace(".run", ".mdb"))
                End If
                'jrs 2021/05/28
                If System.IO.File.Exists(sSource.Replace(Index, "SAM" & Index).Substring(0, sSource.Length) & "db") Then
                    'jrs 2021/01/04 also copy SAM and ifsPOP files
                    FileCopyCheck(sSource.Replace(Index, "SAM" & Index).Substring(0, sSource.Length) & "db", PutFdrvRun & "\SAM" & Index.replace(".run.db", ".db"))
                    FileCopyCheck(sSource.Replace(Index, "IFsPop" & Index).Substring(0, sSource.Length + 3) & "db", PutFdrvRun & "\IFsPop" & Index.replace(".run.db", ".db"))
                End If
            End If
        End Function

        Public Overrides Function ToString() As String

            Try
                ' first check if there is anything in the collection
                If colMyFiles.Count < 1 Then Return ""

                Dim out(colMyFiles.Count - 1) As String
                For i As Integer = 0 To colMyFiles.Count - 1
                    out(i) = CType(colMyFiles.Item(i + 1), MyFile).Name
                Next
                Return String.Join(", ", out)
            Catch Ex As Exception
                _logger.Error(Ex, "MyFiles.ToString() error")
                Return "ToString() ERROR"
            End Try
        End Function

    End Class

'End Namespace
