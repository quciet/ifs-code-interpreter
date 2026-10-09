Imports System
Imports System.Collections.Generic
Imports System.Dynamic
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports IFs.Core.Models
'jrs 2023/09/29
Imports System.Globalization
Imports System.Threading

Public Partial Class ForecastService
    Implements IForecastService

    Public Overrides Iterator Function RebuildModule(moduleName As String, context As IRebuildContext) As IEnumerable(Of IDataframe)
        Dim planner As IModelPlanner(Of IRebuildContext) = Me.GetPlanner(context)
        For Each job As VariableProducer In planner.Plan(context.FirstYear)
            For Each df As IDataframe In job()
                Yield df
            Next
        Next
    End Function

    Public Overrides Iterator Function RebuildAll(context As IRebuildContext) As IEnumerable(Of IDataframe)
        'jrs 2023/09/29
        CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")

        Dim objWriter = New System.IO.StreamWriter(context.PutFdrvRun + "progress.txt", append:=False)
        objWriter.WriteLine("Rebuild Base")
        objWriter.Close()

        'jrs 2019/10/02
        Dim IFsResults As Dictionary(Of String, IDataframe) = New Dictionary(Of String, IDataframe)
        Dim IFsResults2 As Dictionary(Of String, Dataframe) = New Dictionary(Of String, Dataframe)

        Dim planner As IModelPlanner(Of IRebuildContext) = Me.GetPlanner(context)

        'jrs 2022/09/16
        LoadAugmentFile()

        For Each job As VariableProducer In planner.Plan(context.FirstYear)
            For Each df As IDataframe In job()
                Yield df
                'jrs 2019/10/02
                IFsResults.Add(df.Variable.Name, df)
            Next
        Next

        Dim objWriter2 = New System.IO.StreamWriter(context.PutFdrvRun + "progress.txt", append:=True)
        objWriter2.WriteLine("Processing Global Parameters - Please Wait")
        objWriter2.Close()

        'jrs 2019/06/12 Saving Parameters
        Using db As IFsContext = New IFsContext()
            Dim gp As GlobalParameters
            Dim Var As String, ValHold As Single?
            For Each gp In db.GlobalParameters
                Var$ = gp.ParameterName
                If Not context.VariableNames.Contains(Var$) Then
                    Var$ = Var.ToLower
                End If
                If gp.Value.HasValue Then
                    ValHold = CSng(gp.Value)
                Else
                    ValHold = Nothing
                End If
                Dim df As Dataframe = Dataframe.Create(Var, context, context.FirstYear, context.LastYear, ValHold)
                Yield df

                'jrs 2019/10/02
                IFsResults.Add(Var$, df)

            Next

            If context.HistoricMode Then
                Dim gpH As GlobalParametersHist
                For Each gpH In db.GlobalParametersHist
                    Var$ = gpH.ParameterName
                    If Not context.VariableNames.Contains(Var$) Then
                        Var$ = Var.ToLower
                    End If
                    If gpH.Value.HasValue Then
                        ValHold = CSng(gpH.Value)
                    Else
                        ValHold = Nothing
                    End If

                    Dim df As Dataframe = Dataframe.Create(Var, context, context.FirstYear, context.LastYear, ValHold)
                    Yield df

                    'jrs 2020/03/20
                    If IFsResults.ContainsKey(Var$) Then
                        IFsResults.Remove(Var$)
                    End If
                    'jrs 2019/10/02
                    IFsResults.Add(Var$, df)
                Next
            End If
        End Using

        Dim objWriter3 = New System.IO.StreamWriter(context.PutFdrvRun + "progress.txt", append:=True)
        objWriter3.WriteLine("Processing Time Series - Please Wait")
        objWriter3.Close()

        'jrs 2019/10/02
        Call ProcessTimeSeriesNew(1, 1, context, IFsResults, IFsResults2)
        For Each item In IFsResults2
            'No need to save igdpr twice, it will be saved for all years next
            If item.Key <> "igdpr" Then
                Yield item.Value
            End If
        Next

        'jrs 2019/11/01
        'Save igdpr which is initalized by country in preprocessor, but need to save across all years
        Dim dfigdpr As Dataframe = Dataframe.Create("igdpr", context, context.FirstYear, context.LastYear, Nothing)
        For R = 1 To NR
            For y = FirstYear To context.LastYear
                dfigdpr(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = IFsResults("igdpr")(getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR)))
            Next
        Next
        Yield dfigdpr

    End Function

    Public Overrides Sub RebuildCommon(context As IRebuildContext)
        '   Development Note on Flexible Regionalization using Rebuild Base:
        '       In order to be able to distribute only .EXE, Fill, Retr,
        '       and Declare must be fixed. Thus it is necessary to distribute
        '       them with a fixed-maximum region world possible and to use Rebuild only
        '       to determine how many regions (up to the max) are actually
        '       used.
        '   Development Note on Common Rebuilding:
        '       Common rebuilding should follow only changes to the .VAR file
        '       (e.g. additions of variables), not flexible regionalization.
        '       Common rebuilding alters the Fill, Retr and Declare files and
        '       therefore renders any IFs.EXE file obsolete.   Therefore common
        '       rebuilding cannot be provided as an option to students -
        '       only in Site License versions where all code is available.
        '       After rebuilding common it is necessary to exit and restart
        '       the model (and Visual Basic) and then to rebuild the base fully
        '       (with initial conditions and IFs.Dat processing).


        '  This routine creates the subroutines retriev.BAS,
        '  FILL.BAS, and Declare.BAS.  It reads
        '  variables from the .VAR and .GRP files.
        '  Each time a variable is added, a variable is deleted or the dimensions
        '  are changed, this program should be run  AFTER  the variable has been
        '  added to/deleted from the .VAR file.

        '   May need something like this if go to standalone program
        '        fil$ = Command$
        '        INPUT " Enter the drive:\directory for the data files =>  ", in.drive$
        '        Print
        '        INPUT " Enter the drive:\directory for the basic files => ", out.drive$
        '        If Len(in.drive$) > 2 And Right$(in.drive$, 1) <> "\" Then _
        '            in.drive$ = in.drive$ + "\"
        '        If Len(out.drive$) > 2 And Right$(out.drive$, 1) <> "\" Then _
        '            out.drive$ = out.drive$ + "\"
        '        Cls

        '   Note:  On October 1, 1999, changed dimenion index in FILL and RETRIEVE from z%
        '   to ZHold (integer to real), because found that values z% + 32706 through
        '   z% + 32768, when z% = 62, caused overflow of index of N().  Bug in VB or
        '   processor?

        'Only allowed in Development Mode
        If context.DevelopmentMode Then
            'Create Declaration File
            Dim sPath As String, sFileName As String
            sPath = context.PutFdrvRun.Replace("RUNFILES", "Code.Ifs-Translation\src\IFs.Core\Models")
            sFileName = "DeclareVars.vb"
            ' Create or overwrite the file.
            Dim fs As FileStream = File.Create(sPath & sFileName)
            fs.Close()
            Dim objWriter As System.IO.StreamWriter
            objWriter = New System.IO.StreamWriter(sPath & sFileName, append:=True)
            objWriter.WriteLine("Namespace Models")
            objWriter.WriteLine("")
            objWriter.WriteLine("    Public Module DeclareVars")
            objWriter.WriteLine("")

            Dim sLine As String, i As Integer, sExtraTab As String
            For Each v In context.Variables
                sLine = "        Public " & v.Name
                If v.Dimensions.Count > 1 Then
                    sLine = sLine & "("
                    For i = 3 To v.Dimensions.Count
                        sLine = sLine & ","
                    Next
                    sLine = sLine & ")"
                End If
                sLine = sLine & " As Single"
                objWriter.WriteLine(sLine)
            Next

            objWriter.WriteLine("")
            objWriter.WriteLine("    End Module")
            objWriter.WriteLine("")
            objWriter.WriteLine("End Namespace")
            objWriter.Close()


            'Create Retrieve File
            sFileName = "RETR.vb"
            fs = File.Create(sPath & sFileName)
            fs.Close()
            objWriter = New System.IO.StreamWriter(sPath & sFileName, append:=True)

            objWriter.WriteLine("Imports IFs.Data")
            objWriter.WriteLine("")
            objWriter.WriteLine("Namespace Models")
            objWriter.WriteLine("")
            objWriter.WriteLine("    Public Module RETR")
            objWriter.WriteLine("")
            objWriter.WriteLine("        Public Sub Retriev(IFsResults As Dictionary(Of String, IDataframe), Year As Short, ByVal FirstYear As Short)")
            objWriter.WriteLine("")
            'objWriter.WriteLine("            Dim forCurrYear As IQueryFilter = context.GetDimension(0).EQ(Year)")
            objWriter.WriteLine("            Dim Dim1 as Short, Dim2 as Short, Dim3 as Short, sNR As Short")
            objWriter.WriteLine("            sNR = CShort(NR)")
            'objWriter.WriteLine("            Dim curr As IDataframe")
            objWriter.WriteLine("")

            Dim DimCount As Integer
            'jrs 2022/11/16
            Dim sNR As String, sDimCount As String
            sNR = context.GetDimension(1).Buckets.Count.ToString

            For Each v In context.Variables
                DimCount = 1
                'jrs 2022/11/16
                sDimCount = "(1"
                If v.Dimensions.Count > 1 Then
                    DimCount = DimCount * context.GetDimension(v.Dimensions(1)).Buckets.Count
                    'jrs 2022/11/16
                    sDimCount = sDimCount & " * " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString
                    If v.Dimensions.Count > 2 Then
                        DimCount = DimCount * context.GetDimension(v.Dimensions(2)).Buckets.Count
                        'jrs 2022/11/16
                        sDimCount = sDimCount & " * " & context.GetDimension(v.Dimensions(2)).Buckets.Count.ToString
                        If v.Dimensions.Count > 3 Then
                            DimCount = DimCount * context.GetDimension(v.Dimensions(3)).Buckets.Count
                            'jrs 2022/11/16
                            sDimCount = sDimCount & " * " & context.GetDimension(v.Dimensions(3)).Buckets.Count.ToString
                        End If
                    End If
                End If
                'jrs 2022/11/16
                sDimCount = sDimCount & ")"

                If v.Dimensions.Count > 1 Then
                    sLine = "            ReDim " & v.Name & "(" & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString
                    If v.Dimensions.Count > 2 Then
                        sLine = sLine & ", " & context.GetDimension(v.Dimensions(2)).Buckets.Count.ToString
                        If v.Dimensions.Count > 3 Then
                            sLine = sLine & ", " & context.GetDimension(v.Dimensions(3)).Buckets.Count.ToString
                        End If
                    End If
                    sLine = sLine & ")"
                    'jrs 2022/11/16
                    sLine = sLine.Replace(sNR, "sNR")

                    objWriter.WriteLine(sLine)
                End If

                'Only load Computed variables in the first year
                If v.Type = 1 Then
                    sLine = "            if Year = FirstYear then"
                    objWriter.WriteLine(sLine)
                    sExtraTab = "    "
                Else
                    sExtraTab = ""
                End If

                'sLine = "                curr = context.Query(""" & v.Name & """).Filter(forCurrYear).Execute()"
                'objWriter.WriteLine(sLine)
                'sLine = sExtraTab & "            If curr.Count > 0 Then"
                sLine = sExtraTab & "            If IFsResults(""" & v.Name & """).RowCount > (Year - FirstYear) * " & sDimCount & " Then"
                'jrs 2022/11/16
                sLine = sLine.Replace(sNR, "sNR")

                objWriter.WriteLine(sLine)
                If v.Dimensions.Count = 1 Then
                    'sLine = sExtraTab & "                " & v.Name & " = CSng(curr(Year))"
                    sLine = sExtraTab & "                " & v.Name & " = CSng(IFsResults(""" & v.Name & """)(getDataFrameIdxFromDims(Year, FirstYear)))"
                    objWriter.WriteLine(sLine)
                    sLine = sExtraTab & "            else"
                    objWriter.WriteLine(sLine)
                    sLine = sExtraTab & "                " & v.Name & " = 0"
                    objWriter.WriteLine(sLine)
                Else
                    'jrs 2025/05/23
                    'Check if it's bilateral
                    If v.Bilat = 1 Then
                        sLine = "             If bilateralsw = 1 Then"
                        objWriter.WriteLine(sLine)
                    End If

                    'Will need at least one loop, maybe 3
                    sLine = sExtraTab & "                For Dim1 = 1 to " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString
                    'jrs 2022/11/16
                    sLine = sLine.Replace(sNR, "sNR")

                    objWriter.WriteLine(sLine)
                    If v.Dimensions.Count = 2 Then
                        'sLine = sExtraTab & "                    " & v.Name & "(Dim1) = CSng(curr(Year, Dim1))"
                        sLine = sExtraTab & "                    " & v.Name & "(Dim1) = CSng(IFsResults(""" & v.Name & """)(getDataFrameIdxFromDims(Year, FirstYear, Dim1, " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString & ")))"
                        'jrs 2022/11/16
                        sLine = sLine.Replace(sNR, "sNR")

                        objWriter.WriteLine(sLine)
                    Else
                        sLine = sExtraTab & "                    For Dim2 = 1 to " & context.GetDimension(v.Dimensions(2)).Buckets.Count.ToString
                        'jrs 2022/11/16
                        sLine = sLine.Replace(sNR, "sNR")

                        objWriter.WriteLine(sLine)
                        If v.Dimensions.Count = 3 Then
                            'sLine = sExtraTab & "                        " & v.Name & "(Dim1, Dim2) = CSng(if(curr(Year, Dim1, Dim2),0))"
                            sLine = sExtraTab & "                        " & v.Name & "(Dim1, Dim2) = CSng(if(IFsResults(""" & v.Name & """)(getDataFrameIdxFromDims(Year, FirstYear, Dim1, " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString & ", Dim2, " & context.GetDimension(v.Dimensions(2)).Buckets.Count.ToString & ")),0))"
                            'jrs 2022/11/16
                            sLine = sLine.Replace(sNR, "sNR")

                            objWriter.WriteLine(sLine)
                        Else
                            sLine = sExtraTab & "                        For Dim3 = 1 to " & context.GetDimension(v.Dimensions(3)).Buckets.Count.ToString
                            'jrs 2022/11/16
                            sLine = sLine.Replace(sNR, "sNR")

                            objWriter.WriteLine(sLine)
                            'sLine = sExtraTab & "                            " & v.Name & "(Dim1, Dim2, Dim3) = CSng(if(curr(Year, Dim1, Dim2, Dim3),0))"
                            sLine = sExtraTab & "                            " & v.Name & "(Dim1, Dim2, Dim3) = CSng(if(IFsResults(""" & v.Name & """)(getDataFrameIdxFromDims(Year, FirstYear, Dim1, " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString & ", Dim2, " & context.GetDimension(v.Dimensions(2)).Buckets.Count.ToString & ", Dim3, " & context.GetDimension(v.Dimensions(3)).Buckets.Count.ToString & ")),0))"
                            'jrs 2022/11/16
                            sLine = sLine.Replace(sNR, "sNR")

                            objWriter.WriteLine(sLine)
                            sLine = sExtraTab & "                        Next Dim3"
                            objWriter.WriteLine(sLine)
                        End If
                        sLine = sExtraTab & "                    Next Dim2"
                        objWriter.WriteLine(sLine)
                    End If

                    sLine = sExtraTab & "                Next Dim1"
                    objWriter.WriteLine(sLine)

                    'jrs 2025/05/23
                    'Check if it's bilateral
                    If v.Bilat = 1 Then
                        sLine = "             End If"
                        objWriter.WriteLine(sLine)
                    End If

                End If
                sLine = sExtraTab & "            End If"
                objWriter.WriteLine(sLine)

                If v.Type = 1 Then
                    If v.Dimensions.Count = 1 Then
                        sLine = "            Else"
                        objWriter.WriteLine(sLine)
                        sLine = "                " & v.Name & " = 0"
                        objWriter.WriteLine(sLine)
                    End If

                    sLine = "            End if"
                    objWriter.WriteLine(sLine)
                End If

                objWriter.WriteLine("")
            Next

            objWriter.WriteLine("        End Sub")
            objWriter.WriteLine("")
            objWriter.WriteLine("    End Module")
            objWriter.WriteLine("")
            objWriter.WriteLine("End Namespace")

            objWriter.Close()

            'Create Fill File
            sFileName = "FILL.vb"
            fs = File.Create(sPath & sFileName)
            fs.Close()
            objWriter = New System.IO.StreamWriter(sPath & sFileName, append:=True)

            objWriter.WriteLine("Imports IFs.Data")
            objWriter.WriteLine("")
            objWriter.WriteLine("Namespace Models")
            objWriter.WriteLine("")
            objWriter.WriteLine("    Public Module FILL")
            objWriter.WriteLine("")
            objWriter.WriteLine("        Public Sub Fillit(ByRef IFsResults As Dictionary(Of String, Dataframe), Year As Short, FirstYear As Short)")
            objWriter.WriteLine("")
            objWriter.WriteLine("            Dim Dim1 as Short, Dim2 as Short, Dim3 as Short, sNR as Short")
            objWriter.WriteLine("            sNR = CShort(NR)")
            objWriter.WriteLine("")

            For Each v In context.Variables
                DimCount = 1
                'jrs 2022/11/16 not needed
                'If v.Dimensions.Count > 1 Then
                '    DimCount = DimCount * context.GetDimension(v.Dimensions(1)).Buckets.Count
                '    If v.Dimensions.Count > 2 Then
                '        DimCount = DimCount * context.GetDimension(v.Dimensions(2)).Buckets.Count
                '        If v.Dimensions.Count > 3 Then
                '            DimCount = DimCount * context.GetDimension(v.Dimensions(3)).Buckets.Count
                '        End If
                '    End If
                'End If

                If v.Dimensions.Count = 1 Then
                    sLine = "            IFsResults(""" & v.Name & """)(getDataFrameIdxFromDims(Year, FirstYear)) = " & v.Name
                    objWriter.WriteLine(sLine)
                Else
                    'jrs 2025/05/23
                    'Check if it's bilateral
                    If v.Bilat = 1 Then
                        sLine = "         If bilateralsw = 1 Then"
                        objWriter.WriteLine(sLine)
                    End If
                    'Will need at least one loop, maybe 3
                    sLine = "            For Dim1 = 1 to " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString
                    'jrs 2022/11/16
                    sLine = sLine.Replace(sNR, "sNR")

                    objWriter.WriteLine(sLine)
                    If v.Dimensions.Count = 2 Then
                        sLine = "                IFsResults(""" & v.Name & """)(getDataFrameIdxFromDims(Year, FirstYear, Dim1, " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString & ")) = " & v.Name & "(Dim1)"
                        'jrs 2022/11/16
                        sLine = sLine.Replace(sNR, "sNR")

                        objWriter.WriteLine(sLine)
                    Else
                        sLine = "                For Dim2 = 1 to " & context.GetDimension(v.Dimensions(2)).Buckets.Count.ToString
                        'jrs 2022/11/16
                        sLine = sLine.Replace(sNR, "sNR")

                        objWriter.WriteLine(sLine)
                        If v.Dimensions.Count = 3 Then
                            sLine = "                    IFsResults(""" & v.Name & """)(getDataFrameIdxFromDims(Year, FirstYear, Dim1, " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString & ", Dim2, " & context.GetDimension(v.Dimensions(2)).Buckets.Count.ToString & ")) = " & v.Name & "(Dim1, Dim2)"
                            'jrs 2022/11/16
                            sLine = sLine.Replace(sNR, "sNR")

                            objWriter.WriteLine(sLine)
                        Else
                            sLine = "                    For Dim3 = 1 to " & context.GetDimension(v.Dimensions(3)).Buckets.Count.ToString
                            'jrs 2022/11/16
                            sLine = sLine.Replace(sNR, "sNR")

                            objWriter.WriteLine(sLine)
                            sLine = "                        IFsResults(""" & v.Name & """)(getDataFrameIdxFromDims(Year, FirstYear, Dim1, " & context.GetDimension(v.Dimensions(1)).Buckets.Count.ToString & ", Dim2, " & context.GetDimension(v.Dimensions(2)).Buckets.Count.ToString & ", Dim3, " & context.GetDimension(v.Dimensions(3)).Buckets.Count.ToString & ")) = " & v.Name & "(Dim1, Dim2, Dim3)"
                            'jrs 2022/11/16
                            sLine = sLine.Replace(sNR, "sNR")

                            objWriter.WriteLine(sLine)
                            sLine = "                    Next Dim3"
                            objWriter.WriteLine(sLine)
                        End If
                        sLine = "                Next Dim2"
                        objWriter.WriteLine(sLine)
                    End If

                    sLine = "            Next Dim1"
                    objWriter.WriteLine(sLine)

                    'jrs 2025/05/23
                    'Check if it's bilateral
                    If v.Bilat = 1 Then
                        sLine = "         End If"
                        objWriter.WriteLine(sLine)
                    End If

                End If
                objWriter.WriteLine("")
            Next

            objWriter.WriteLine("        End Sub")
            objWriter.WriteLine("")
            objWriter.WriteLine("    End Module")
            objWriter.WriteLine("")
            objWriter.WriteLine("End Namespace")

            objWriter.Close()
        End If

    End Sub

End Class
