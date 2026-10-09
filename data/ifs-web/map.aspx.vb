Imports AspMap

Partial Class map
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mMap As AspMap.Map
        Dim mLegend As AspMap.Legend

        If Not Session("sessionMap") Is Nothing Then
            mMap = Session("sessionMap")
            mLegend = BuildLegend(mMap)
            mMap.DrawLegend(mLegend, mMap.Width - mLegend.Width - 10, mMap.Height - mLegend.Height - 10)
            mMap.ImageFormat = AspMap.ImageFormat.mcPNG
            mLegend.ImageFormat = AspMap.ImageFormat.mcPNG
            Response.ContentType = "image/png"
            Response.BinaryWrite(mMap.Image)
        Else
            Response.Write("Session map or legend not found.")
        End If
    End Sub

    Private Function BuildLegend(ByVal sessionMap As AspMap.Map) As AspMap.Legend
        Dim outputLegend As New Legend
        Dim layer As AspMap.Layer
        Dim i As Integer
        For i = 0 To sessionMap.LayerCount - 1
            layer = sessionMap.Layer(i)
            'If the layer is visible or not transparent then 
            'add it to the legend.
            If layer.Opacity > 0 Then
                Dim layerFeature As Feature
                Dim featureCounter, legendItem As Integer
                Dim maxLabelWidth As Integer = 0

                For featureCounter = 0 To layer.Renderer.Count - 1
                    layerFeature = layer.Renderer.Feature(featureCounter)
                    legendItem = outputLegend.AddItem
                    outputLegend.ItemType(legendItem) = layer.LayerType
                    Dim textItems() As String = Split(layerFeature.Expression, " ")
                    Dim labelText As String
                    If textItems.Length = 7 Then
                        Dim lowerValue As Double = CDbl(textItems(2))
                        Dim upperValue As Double = CDbl(textItems(6))
                        labelText = String.Format("{0} {1:0.#####} and {2} {3:0.#####}", textItems(1), lowerValue, textItems(5), upperValue)
                    ElseIf textItems.Length = 3 Then
                        labelText = String.Format("{0:0.#####}", textItems(2))
                    Else
                        labelText = layerFeature.Expression
                    End If
                    outputLegend.ItemText(legendItem) = labelText
                    outputLegend.ItemSymbol(legendItem).Size = 1
                    outputLegend.Font.Size = 12
                    outputLegend.ItemSymbol(legendItem).FillColor = layerFeature.Symbol.FillColor
                    outputLegend.ItemSymbol(legendItem).PointStyle = PointStyle.mcSquarePoint
                Next
            End If
        Next
        outputLegend.CalculateSize()
        Return outputLegend
    End Function
End Class
