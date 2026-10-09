Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data

Namespace Models
    Partial Public Module Technology

        <RebuildsVariable("AITASK")>
        <RebuildsVariable("AITASKGR")>
        Public Iterator Function RebuildTechnology(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Using db As IFsContext = New IFsContext()
                Dim df As Dataframe = Dataframe.Create("AITASK", context, context.FirstYear, Nothing)
                Dim df2 As Dataframe = Dataframe.Create("AITASKGR", context, context.FirstYear, Nothing)
                For Each pos As AIInit In db.AIInit
                    df(context.FirstYear, CShort(pos.AITypeIdx)) = pos.Value
                    df2(context.FirstYear, CShort(pos.AITypeIdx)) = pos.GrowthRate
                Next
                Yield df
                Yield df2
            End Using
        End Function

        Class TechAggregator
            Inherits DefaultSumAggregator

            Private Property AITASK As IDictionary(Of Integer, Single?)

            Private Property processed As Boolean

            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Technology - Please Wait")
                objWriter.Close()

                AITASK = New Dictionary(Of Integer, Single?)

                Using db As IFsContext = New IFsContext()
                    For Each pos As AIInit In db.AIInit
                        AITASK.Add(pos.AITypeIdx, pos.Value)
                    Next
                End Using

                Me.processed = True
            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)

                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim weightSeries As IDictionary(Of String, Single?)
                'weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single, bLimits As Boolean = False
                Dim bSum As Boolean = False

                Dim Vals As IDictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)

                Select Case seriesName
                    Case "AITASK_Mac_Learn"
                        Vals.Add(1, AITASK(1))
                        Return Vals
                    Case "AITASK_Comp_Vis"
                        Vals.Add(2, AITASK(2))
                        Return Vals
                    Case "AITASK_Lang_Proc"
                        Vals.Add(3, AITASK(3))
                        Return Vals
                    Case "AITASK_IoT"
                        Vals.Add(4, AITASK(4))
                        Return Vals
                    Case "AITASK_Robotics"
                        Vals.Add(5, AITASK(5))
                        Return Vals
                    Case "AITASK_Reason"
                        Vals.Add(6, AITASK(6))
                        Return Vals
                    Case Else
                        'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, Nothing)
                        Next
                End Select

                If bLimits Then
                    If bSum Then
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.TotalVal, MinVal, MaxVal)
                           )
                    Else
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, MinVal, MaxVal)
                           )
                    End If
                Else
                    If bSum Then
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.TotalVal
                           )
                    Else
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                           )
                    End If
                End If

            End Function
        End Class

    End Module
End Namespace
