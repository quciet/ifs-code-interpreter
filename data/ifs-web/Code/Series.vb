Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

'Namespace IFsWeb

Public Class Series

        Dim WinFormsToWeb_Messagebox As ArtinSoft.WinFormsToWeb.WebControls.MessageBox
        Dim Names As String ' Identification of series
        'JRS 01/23/04
        Dim DataArray(0, 0) As Object ' 2-dimension data array
        Public MinValue As Double ' Minimum of data's value
        Public MaxValue As Double ' Maximum of data's value
        ' Add next experimental data to this object
        Public Function Add(ByRef TestingName As String, ByRef RowData() As Double) As Boolean
            Dim N, i, M As Integer
            ' If this object is empty, initialize it
            If Names = "" Then
                Names = TestingName
                N = UBound(RowData)
                M = 1
                ReDim DataArray(N, M)
                For i = 1 To N
                    'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(i, 0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    DataArray(i, 0) = " " & CStr(i)
                Next i
                MinValue = RowData(1)
                MaxValue = MinValue
            Else
                ' If this object have other name, return 'False'
                If Names <> TestingName Then Exit Function
                N = UBound(DataArray, 1)
                M = UBound(DataArray, 2) + 1
                ReDim Preserve DataArray(N, M)
            End If
            ' Fill DataArray by next experiment's data
            'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(0, M). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DataArray(0, M) = " " & CStr(RowData(0))
            For i = 1 To N
                'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(i, M). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                DataArray(i, M) = RowData(i)
                If MinValue > RowData(i) Then MinValue = RowData(i)
                If MaxValue < RowData(i) Then MaxValue = RowData(i)
            Next i
            ' Return 'True'
            Add = True
        End Function
        ' Return 1, 2 or 3 component of complex name of object
        Public ReadOnly Property Name(ByVal Index As Integer) As String
            Get
                On Error Resume Next
                Return Split(Names, ":")(Index - 1)
            End Get
        End Property
        ' Get DataArray for MSChart object
        Public ReadOnly Property Data(Optional ByVal ConfidenceBounds As String = "") As Object
            Get
                On Error GoTo Out
                Dim LastExperiment, Session("LastYear"), k As Integer
                Dim BaseLinePercent As Double
                'UPGRADE_NOTE: Year was upgraded to Year_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
                Dim Experiment, Bound, Year_Renamed As Integer
                Dim CB As Object
                Dim ss() As String
                Dim Buffer() As Double
                If ConfidenceBounds = "" Then
                    ' If argument is empty, return DataArray as is
                    'UPGRADE_WARNING: Couldn't resolve default property of object Data. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'JRS 01/23/04 DataControl = ..
                    Data = VB6.Support.CopyArray(DataArray)
                Else
                    ' ConfidenceBounds="50,75,95,100"
                    ss = Split(ConfidenceBounds, ",")
                    Session("LastYear") = UBound(DataArray, 1)
                    LastExperiment = UBound(DataArray, 2)
                    k = UBound(ss) * 2 + 3
                    ReDim CB(Session("LastYear"), k)
                    ' Row titles
                    For Year_Renamed = 1 To Session("LastYear")
                        'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(Year_Renamed, 0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CB(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        CB(Year_Renamed, 0) = DataArray(Year_Renamed, 0)
                    Next Year_Renamed
                    ' Column titles
                    'UPGRADE_WARNING: Couldn't resolve default property of object CB(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    CB(0, k \ 2 + 1) = " Base"
                    For Bound = 0 To UBound(ss)
                        'UPGRADE_WARNING: Couldn't resolve default property of object CB(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        CB(0, k \ 2 - Bound) = " " & ss(Bound) & "%"
                        'UPGRADE_WARNING: Couldn't resolve default property of object CB(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        CB(0, k \ 2 + Bound + 2) = " " & ss(Bound) & "%"
                    Next Bound
                    ' Points values
                    For Year_Renamed = 1 To Session("LastYear")
                        ' Create sorted array of values

                        '         ReDim Buffer(1 To LastExperiment - 1)
                        '         For Experiment = 1 To LastExperiment - 1
                        '            Buffer(Experiment) = DataArray(Year, Experiment)
                        '         Next Experiment
                        '         SortDouble Buffer, 1, LastExperiment - 1
                        'UPGRADE_WARNING: Lower bound of array Buffer was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
                        ReDim Buffer(LastExperiment)
                        For Experiment = 1 To LastExperiment
                            'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(Year_Renamed, Experiment). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Buffer(Experiment) = DataArray(Year_Renamed, Experiment)
                        Next Experiment
                        SortDouble(Buffer, 1, LastExperiment)

                        ' Base line
                        'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(Year_Renamed, LastExperiment). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object CB(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        CB(Year_Renamed, k \ 2 + 1) = DataArray(Year_Renamed, LastExperiment)
                        'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        BaseLinePercent = Percentile(Buffer, DataArray(Year_Renamed, LastExperiment))
                        For Bound = 0 To UBound(ss)
                            ' Top bounds
                            'UPGRADE_WARNING: Couldn't resolve default property of object CB(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            CB(Year_Renamed, k \ 2 - Bound) = Quantile(Buffer, BaseLinePercent * (100 - Val(ss(Bound))) / 100)
                            ' Bottom bounds
                            'UPGRADE_WARNING: Couldn't resolve default property of object CB(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            CB(Year_Renamed, k \ 2 + Bound + 2) = Quantile(Buffer, BaseLinePercent + (1 - BaseLinePercent) * Val(ss(Bound)) / 100)
                        Next Bound
                    Next Year_Renamed
                    ' CB array is ready!
                    'UPGRADE_WARNING: Couldn't resolve default property of object CB. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Data. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'JRS 01/23/04 DataControl = CB
                    Data = CB
                End If
                Exit Property
Out:

                Err.Raise(vbObjectError + 1051, Nothing, "Error " & Err.Number & " - " & Err.Description)
                Resume
            End Get
        End Property
        ' Fill charts data table for histogram
        'UPGRADE_NOTE: Year was upgraded to Year_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Public ReadOnly Property Section(ByVal Year_Renamed As Integer) As Object
            Get
                Dim V As Object
                Dim Session("LastYear"), LastExperiment As Integer
                Dim NumberOfBars, Experiment, Bar As Integer
                Dim MinY, Value, MaxY, Delta As Double
                On Error GoTo Out
                Session("LastYear") = UBound(DataArray, 1)
                LastExperiment = UBound(DataArray, 2)
                ' Compute bounds of values
                For Experiment = 1 To LastExperiment - 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(Year_Renamed, Experiment). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Value = DataArray(Year_Renamed, Experiment)
                    If (Experiment = 1) Or (MaxY < Value) Then MaxY = Value
                    If (Experiment = 1) Or (MinY > Value) Then MinY = Value
                Next Experiment
                NumberOfBars = 5
                MinY = LogRound(MinY, 4)
                Delta = LogRound((MaxY - MinY) / NumberOfBars, 2, True)
                MaxY = LogRound(MaxY, 4, True)
                If Delta > 0 Then
                    'NumberOfBars = -Int(-(MaxY - MinY) / Delta)
                    MaxY = MinY + NumberOfBars * Delta
                Else
                    NumberOfBars = 1
                End If
                ' Fill titles
                ReDim V(NumberOfBars, 1)
                'UPGRADE_WARNING: Couldn't resolve default property of object V(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                V(0, 1) = "Year = " & Year_Renamed
                For Bar = 1 To NumberOfBars
                    Value = MinY + Delta * (Bar - 1)
                    'UPGRADE_WARNING: Couldn't resolve default property of object V(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    V(Bar, 0) = "> " & LogRound(Value, 5) '& " - " '& LogRound(Value + Delta, 5, True)
                Next Bar
                ' Fill bar's height
                Bar = 1
                For Experiment = 1 To LastExperiment - 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object DataArray(Year_Renamed, Experiment). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Value = DataArray(Year_Renamed, Experiment)
                    If Delta > 0 Then
                        Bar = Int((Value - MinY) / Delta) + 1
                    End If
                    'UPGRADE_WARNING: Couldn't resolve default property of object V(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    V(Bar, 1) = V(Bar, 1) + 1
                Next Experiment
                'UPGRADE_WARNING: Couldn't resolve default property of object V. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Section. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Section = V
Out:
            End Get
        End Property
    End Class

'End Namespace
