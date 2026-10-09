Option Strict Off
Option Explicit On


'Namespace IFsWeb

Public Class RegressionObject
	'timeseries extrapolation- new  class
	 
	Private Const MaxO As Integer = 25
	Private GlobalO As Integer'"Ordnung" = degree of the polynom expected
	Private Finished As Boolean
	 
	Private SumX(2 * MaxO) As Double
	Private SumYX(MaxO) As Double
	Private M(MaxO, MaxO + 1) As Double
	Private C(MaxO) As Double'coefficients in: Y = C(0)*X^0 + C(1)*X^1 + C(2)*X^2 + ...
	 
	Private Sub GaussSolve(ByRef O As Integer)
		'gauss algorithm implementation,
		'following R.Sedgewick's "Algorithms in C", Addison-Wesley, with minor modifications
		Dim k, i, j, iMax As Integer
		Dim T, O1 As Double
		O1 = O + 1
		'first triangulize the matrix
		For i = 0 To O
			iMax = i : T = System.Math.Abs(M(iMax, i))
			For j = i + 1 To O'find the line with the largest absvalue in this row
				If T < System.Math.Abs(M(j, i)) Then iMax = j : T = System.Math.Abs(M(iMax, i))
			Next j
			If i < iMax Then 'exchange the two lines
				For k = i To O1
					T = M(i, k)
					M(i, k) = M(iMax, k)
					M(iMax, k) = T
				Next k
			End If 
			For j = i + 1 To O'scale all following lines to have a leading zero
				T = M(j, i) / M(i, i)
				M(j, i) = 0.0#
				For k = i + 1 To O1
					M(j, k) = M(j, k) - M(i, k) * T
				Next k
			Next j
		Next i
		'then substitute the coefficients
		For j = O To 0 Step  - 1
			T = M(j, O1)
			For k = j + 1 To O
				T = T - M(j, k) * C(k)
			Next k
			C(j) = T / M(j, j)
		Next j
		Finished = True
	End Sub 
	 
	Private Sub BuildMatrix(ByRef O As Integer)
		Dim k, i, O1 As Integer
		O1 = O + 1
		For i = 0 To O
			For k = 0 To O
				M(i, k) = SumX(i + k)
			Next k
			M(i, O1) = SumYX(i)
		Next i
	End Sub 
	 
	Private Sub FinalizeMatrix(ByRef O As Integer)
		Dim i, O1 As Integer
		O1 = O + 1
		For i = 0 To O
			M(i, O1) = SumYX(i)
		Next i
	End Sub 
	 
	Private Sub Solve()
		Dim O As Integer
		O = GlobalO
		If XYCount <= O Then O = XYCount - 1
		If O < 0 Then Exit Sub 
		BuildMatrix(O)
		On Error Resume Next 
		GaussSolve((O))
		While (Err.Number <> 0) And (1 < O)
			Err.Clear()
			C(0) = 0.0#
			O = O - 1
			FinalizeMatrix((O))
		End While 
		On Error GoTo 0
	End Sub 
	 
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
	Private Sub Class_Initialize_Renamed()
		Init()
		GlobalO = 2
	End Sub 
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	 
	Public Sub Init()
		Dim i As Integer
		Finished = False
		For i = 0 To MaxO
			SumX(i) = 0.0#
			SumX(i + MaxO) = 0.0#
			SumYX(i) = 0.0#
			C(i) = 0.0#
		Next i
	End Sub 
	 
	Public ReadOnly Property Coeff(ByVal Exponent As Integer) As Double
		Get 
			Dim Ex, O As Integer
			If Not Finished Then Solve()
			Ex = System.Math.Abs(Exponent)
			O = GlobalO
			If XYCount <= O Then O = XYCount - 1
			If O < Ex Then Coeff = 0.0# Else Coeff = C(Ex)
		End Get 
	End Property 
	 
	Public Property Degree() As Integer
		Get 
			Degree = GlobalO
		End Get 
		Set (ByVal Value As Integer)
			If Value < 0 Or MaxO < Value Then 
				Err.Raise(6000, "RegressionObject", Value & " is an invalid property value! Use 0<= Degree <= " & MaxO)
				Exit Property 
			End If 
			Init()
			GlobalO = Value
		End Set 
	End Property 
	 
	Public ReadOnly Property XYCount() As Integer
		Get 
			XYCount = CInt(SumX(0))
		End Get 
	End Property 
	 
	Public Function XYAdd(ByVal NewX As Double, ByVal NewY As Double) As Object
		Dim j, i, Max2O As Integer
		Dim TX As Double
		Finished = False
		Max2O = 2 * GlobalO
		TX = 1.0#
		SumX(0) = SumX(0) + 1
		SumYX(0) = SumYX(0) + NewY
		For i = 1 To GlobalO
			TX = TX * NewX
			SumX(i) = SumX(i) + TX
			SumYX(i) = SumYX(i) + NewY * TX
		Next i
		For i = GlobalO + 1 To Max2O
			TX = TX * NewX
			SumX(i) = SumX(i) + TX
		Next i
	End Function 
	 
	Public Function RegVal(ByRef X As Double) As Double
		Dim i, O As Integer
		If Not Finished Then Solve()
		RegVal = 0.0#
		O = GlobalO
		If XYCount <= O Then O = XYCount - 1
		For i = 0 To O
			RegVal = RegVal + C(i) * X ^ i
		Next i
	End Function 
End Class

'End Namespace
