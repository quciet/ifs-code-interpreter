'Drawing Line Chart in ASP.NET 
'By  Steve Hall January 18, 2002 
'http://www.c-sharpcorner.com/UploadFile/steve_hall/drawinglinechart02072007053420AM/drawinglinechart.aspx

'Translated to VB.NET by José R. Solórzano
'jrs 2008/12/02

Imports Microsoft.VisualBasic

Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Public Class LineChart
    Public b As Bitmap
    Public Title As String = "Default Title"
    Public chartValues As ArrayList = New ArrayList
    Public Xorigin As Single = 0, Yorigin As Single = 0
    Public ScaleX As Single, ScaleY As Single
    Public Xdivs As Single = 2, Ydivs As Single = 2
    Private Width As Integer, Height As Integer

    Private g As Graphics
    Private p As Page

    'initialize
    Public Sub New(ByVal myWidth As Integer, ByVal myHeight As Integer, ByVal myPage As Page)
        Width = myWidth
        Height = myHeight

        ScaleX = myWidth
        ScaleY = myHeight

        b = New Bitmap(myWidth, myHeight)

        g = Graphics.FromImage(b)

        p = myPage
    End Sub

    Public Sub AddValue(ByVal x As Integer, ByVal y As Integer)
        Dim myPoint As datapoint = New datapoint

        myPoint.x = x
        myPoint.y = y
        myPoint.valid = True

        chartValues.Add(myPoint)

    End Sub

    Public Sub Draw()
        Dim i As Integer
        Dim x, y, x0, y0 As Single
        Dim myLabel As String

        Dim blackPen As Pen = New Pen(Color.Black, 1)
        Dim bluePen As Pen = New Pen(Color.Blue, 2)

        Dim blackBrush As Brush = New SolidBrush(Color.Black)

        Dim axesFont As Font = New Font("arial", 10)

        'first establish working area
        p.Response.ContentType = "image/jpeg"

        g.FillRectangle(New SolidBrush(Color.White), 0, 0, Width, Height)

        Dim ChartInset As Integer = 50
        Dim ChartWidth As Integer = Width - (2 * ChartInset)
        Dim ChartHeight As Integer = Height - (2 * ChartInset)

        g.DrawRectangle(New Pen(Color.Black, 1), ChartInset, ChartInset, ChartWidth, ChartHeight)

        'must draw all text items before doing the rotate below
        g.DrawString(Title, New Font("arial", 11), blackBrush, Width / 5, 10)

        'draw X axis labels
        For i = 0 To Xdivs
            x = ChartInset + (i * ChartWidth) / Xdivs
            y = ChartHeight + ChartInset
            myLabel = (Xorigin + (ScaleX * i / Xdivs)).ToString()
            g.DrawString(myLabel, axesFont, blackBrush, x - 4, y + 10)
            g.DrawLine(blackPen, x, y + 2, x, y - 2)
        Next i

        Dim RoundVal As Single
        Dim Zeros As Integer = 0, Signif As Integer = 1
        Do While Signif * 10 < (ScaleY + Yorigin)
            Signif = Signif * 10
            Zeros = Zeros + 1
        Loop
        Zeros = Zeros - (Zeros \ 2)
        Signif = 10 ^ Zeros

        'draw Y axis labels
        For i = 1 To Ydivs 'don't print 0
            x = ChartInset
            If Zeros > 0 Then
                RoundVal = ((Yorigin + (ScaleY * i / Ydivs)) \ Signif) * Signif
            Else
                RoundVal = (Yorigin + (ScaleY * i / Ydivs))
            End If
            'y = ChartHeight + ChartInset - (i * ChartHeight / Ydivs)
            y = ChartHeight + ChartInset - (ChartHeight * (RoundVal - Yorigin) / ScaleY)
            myLabel = RoundVal.ToString()
            If RoundVal > Yorigin Then
                g.DrawString(myLabel, axesFont, blackBrush, 5, y - 6)
                g.DrawLine(blackPen, x + 2, y, x - 2, y)
            End If
        Next i

        'transform drawing coords to lower-left (0,0)
        g.RotateTransform(180)
        g.TranslateTransform(0, -Height)
        g.TranslateTransform(-ChartInset, ChartInset)
        g.ScaleTransform(-1, 1)

        'draw chart data
        Dim prevPoint As datapoint = New datapoint()
        prevPoint.valid = False
        Dim myPoint As datapoint
        For Each myPoint In chartValues
            If (prevPoint.valid) Then
                x0 = ChartWidth * (prevPoint.x - Xorigin) / ScaleX
                y0 = ChartHeight * (prevPoint.y - Yorigin) / ScaleY
                x = ChartWidth * (myPoint.x - Xorigin) / ScaleX
                y = ChartHeight * (myPoint.y - Yorigin) / ScaleY
                g.DrawLine(bluePen, x0, y0, x, y)
                'jrs 2008/12/05 no mark on data points
                'g.FillEllipse(blackBrush, x0 - 2, y0 - 2, 4, 4)
                'g.FillEllipse(blackBrush, x - 2, y - 2, 4, 4)
            End If

            prevPoint = myPoint

        Next myPoint

        'finally send graphics to browser
        b.Save(p.Response.OutputStream, ImageFormat.Jpeg)

        g.Dispose()
        b.Dispose()

    End Sub

    Private Class datapoint
        Public x As Single
        Public y As Single
        Public valid As Boolean
    End Class

End Class


