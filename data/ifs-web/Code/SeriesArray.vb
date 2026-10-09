Option Strict Off
Option Explicit On


'Namespace IFsWeb

Public Class SeriesArray
	'UPGRADE_WARNING: Arrays can't be declared with New. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1045"'
	'JRS 01/23/04 AS NEW SERIES
	Dim Seriess() As Object' Series array
	' Initialize array
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
	Private Sub Class_Initialize_Renamed()
		ReDim Seriess(0)
	End Sub 
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	' Syntax parsing next line (from "SensitivityOutput.dat")
	Public Sub Add(ByRef Line As String)
		' If line is empty - return
		If Trim(Line) = "" Then Exit Sub 
		Dim Fields() As String
		Dim i, N As Integer
		Dim Data() As Double
		Dim TestingName As String
		' Comma is delimiter of items into line
		Fields = Split(Line, ",")
		' Data's parameters names
		TestingName = Trim(Fields(1)) & ":" & Trim(Fields(2)) & ":" & Trim(Fields(3))
		N = UBound(Fields) - 3
		' Data array
		ReDim Data(N)
		Data(0) = CDbl(Fields(0))
		For i = 1 To N
			Data(i) = Val(Fields(i + 3))
		Next i
		' Add data (next experiment) to corresponding item of collection
		For i = 1 To UBound(Seriess)
			If Seriess(i).Add(TestingName, Data) Then Exit Sub 
		Next i
		' If corresponding item of collection is absent, that create it
		ReDim Preserve Seriess(UBound(Seriess) + 1)
		Seriess(UBound(Seriess)).Add(TestingName, Data)
	End Sub 
	' Size of collection (number of items)
	Public ReadOnly Property Count() As Integer
		Get 
			Count = UBound(Seriess)
		End Get 
	End Property 
	' Output one item from collection
	Default Public ReadOnly Property Item(ByVal Index As Integer) As Series
		Get 
			Item = Seriess(Index)
		End Get 
	End Property 
End Class

'End Namespace
