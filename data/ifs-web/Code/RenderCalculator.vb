Imports AspMap


'Namespace IFsWeb


Public Class RenderCalculator

    Public Enum enumLegendType
        mcEqualCount = 1
        mcEqualInterval = 2
    End Enum

    Private _layer As AspMap.Layer
    Private _legendType As enumLegendType
    Private _layerValues As ArrayList
    Private _field As String
    Private _numberOfClasses As Integer

    Public Sub New(ByVal layer As AspMap.Layer, ByVal legendType As enumLegendType, ByVal field As String, ByVal numberOfClasses As Integer)
        _layer = layer
        _legendType = legendType
        _field = field
        _numberOfClasses = numberOfClasses
        _layerValues = GetLayerValues(_layer, _field)
    End Sub

    Public Property Layer() As AspMap.Layer
        Get
            Return _layer
        End Get
        Set(ByVal Value As AspMap.Layer)
            _layer = Value
        End Set
    End Property

    Public Property LegendType() As enumLegendType
        Get
            Return _legendType
        End Get
        Set(ByVal Value As enumLegendType)
            _legendType = Value
        End Set
    End Property

    Public Property Field() As String
        Get
            Return _field
        End Get
        Set(ByVal Value As String)
            _field = Value
        End Set
    End Property

    Public Property NumberOfClasses() As Integer
        Get
            Return _numberOfClasses
        End Get
        Set(ByVal Value As Integer)
            _numberOfClasses = Value
        End Set
    End Property

    Private Function GetLayerValues(ByVal layer As AspMap.Layer, ByVal field As String) As ArrayList
        Dim rs As AspMap.Recordset = layer.Recordset
        Dim layerValues As New ArrayList
        rs.MoveFirst()
        While Not rs.EOF
            If Not TypeOf (rs(field)) Is DBNull Then
                If rs(field) > "" And rs(field) <> "NO DATA" Then
                    layerValues.Add(rs(field))
                End If
            End If
            rs.MoveNext()
        End While

        Return layerValues
    End Function

    Private Function GetMax(ByVal layerValues As ArrayList) As String
        Dim i As Integer
        Dim max As Double

        max = 0
        For i = 0 To layerValues.Count - 1
            If IsNumeric(layerValues(i)) Then
                Dim test As Double = layerValues(i)
                If test > max Then
                    max = test
                End If
            End If
        Next
        Return max
    End Function

    Private Function GetMin(ByVal layerValues As ArrayList) As String
        Dim i As Integer
        Dim min As Double = GetMax(layerValues)

        For i = 0 To layerValues.Count - 1
            If IsNumeric(layerValues(i)) Then
                Dim test As Double = layerValues(i)
                If test < min Then
                    min = test
                End If
            End If
        Next
        Return min
    End Function

    Private Function FindClassBreaks(ByVal legendType As enumLegendType, ByVal numberOfClasses As Integer, ByVal layerValues As ArrayList) As ArrayList
        Dim classBreaks As New ArrayList

        Select Case legendType
            Case enumLegendType.mcEqualCount

            Case enumLegendType.mcEqualInterval
                Dim min, max, range, interval, break As Double
                min = CDbl(GetMin(layerValues))
                max = CDbl(GetMax(layerValues))
                range = Math.Abs(max - min)
                interval = CDbl(range / numberOfClasses)
                break = min
                classBreaks.Add(break)
                Dim classCounter As Integer
                For classCounter = 1 To numberOfClasses
                    break += interval
                    classBreaks.Add(break)
                Next
            Case Else
                Throw New Exception("LegendCalculator.FindClassBreaks: invalid legend type")
        End Select
        Return classBreaks
    End Function

    Private Sub BuildEqualIntervalRenderer(ByVal classBreaks As ArrayList, ByVal layer As AspMap.Layer, ByVal field As String)
        Dim eiRenderer As FeatureRenderer = layer.Renderer

        Dim break, nextBreak As Double
        Dim breakCounter As Integer
        Dim breakFeature As Feature

        Randomize()
        For breakCounter = 0 To classBreaks.Count - 2
            break = classBreaks(breakCounter)
            nextBreak = classBreaks(breakCounter + 1)
            breakFeature = New Feature
            breakFeature = eiRenderer.Add
            breakFeature.Expression = String.Format("{0} >= {1} AND {0} <= {2}", field, break, nextBreak)
            Dim redValue As Integer = CInt((255 * Rnd()) + 1)
            Dim blueValue As Integer = CInt((255 * Rnd()) + 1)
            Dim greenValue As Integer = CInt((255 * Rnd()) + 1)

            breakFeature.Symbol.FillColor = RGB(redValue, greenValue, blueValue)
            breakFeature.Symbol.Size = 1
        Next
        'Symbolize "NO DATA" values
        Dim noDataFeature As Feature
        noDataFeature = eiRenderer.Add
        noDataFeature.Expression = String.Format("{0} = NO DATA", field)
        noDataFeature.Symbol.FillColor = RGB(255, 255, 255)
        noDataFeature.Symbol.Size = 1
    End Sub

    Public Sub GenerateRenderer()
        Dim classBreaks As ArrayList
        Select Case _legendType
            Case enumLegendType.mcEqualInterval
                classBreaks = FindClassBreaks(_legendType, _numberOfClasses, _layerValues)
                BuildEqualIntervalRenderer(classBreaks, _layer, _field)
            Case Else
                Throw New Exception("LegendCalculator.GenerateRenderer: invalid legend type")
        End Select
    End Sub

End Class

'End Namespace
