Option Strict Off
Option Explicit On


'Namespace IFsWeb

Public Module Tools
' Round the value with fixed number of digits
	Public ReadOnly Property LogRound(ByVal Value_Renamed As Double, ByVal Digits As Integer, Optional ByVal Up As Boolean = False) As Double
		Get 
			Dim I, N As Integer
			Dim NumberString, C As String
			Dim Rounded As Boolean
			Dim Delta As String
			NumberString = CStr(Value_Renamed)
			Delta = NumberString
			For I = 1 To Len(NumberString)
				C = Mid(NumberString, I, 1)
				Select Case UCase(C)
					Case "0" To "9"
						N = N + 1
						Select Case N - Digits
							Case  Is  < 0
								Mid(Delta, I, 1) = "0"
							Case 0
								Mid(Delta, I, 1) = "1"
							Case  Is  > 0
								Mid(Delta, I, 1) = "0"
								Rounded = True
								' Round down (more than Digits figure into number)
								Mid(NumberString, I, 1) = "0"
						End Select 
					Case "E"
						' Power of number do not round
						Exit For 
				End Select 
			Next I
			' Return rounded value
			LogRound = Val(Replace(NumberString, ",", "."))
			' Round up
			If Up And Rounded Then 
				LogRound = LogRound + System.Math.Abs(Val(Replace(Delta, ",", ".")))
			End If 
		End Get 
	End Property 
	' Get portion of items into array (Data), that lower or equal Value
	Public ReadOnly Property Percentile(ByVal Data() As Double, ByVal Value_Renamed As Double) As Double
		Get 
			Dim I, MinIndex, MaxIndex, N As Integer
			MinIndex = LBound(Data)
			MaxIndex = UBound(Data)
			' If Value less than minimal item into Data, return 0
			If Value_Renamed > Data(MinIndex) Then 
				' If Value more than maximal item into Data, return 1 (100%)
				If Value_Renamed >= Data(MaxIndex) Then 
					Percentile = 1
				Else 
					' Value is between minimum and maximum
					For I = MinIndex + 1 To MaxIndex
						Select Case Value_Renamed - Data(I)
							Case  Is  < 0
								If N = 0 Then 
									Percentile = (I - 1 + (Value_Renamed - Data(I - 1)) / (Data(I) - Data(I - 1)) - MinIndex) / (MaxIndex - MinIndex)
								Else 
									Percentile = (I - (N + 1) / 2 - MinIndex) / (MaxIndex - MinIndex)
								End If 
								Exit For 
							Case 0
								N = N + 1
						End Select 
					Next I
				End If 
			End If 
		End Get 
	End Property 
	' Get value, corresponding PerCent's item into sorted array (Data)
	Public ReadOnly Property Quantile(ByVal Data() As Double, ByVal PerCent As Double) As Double
		Get 
			Dim MaxIndex, MinIndex, I As Integer
			Dim P As Double
			MinIndex = LBound(Data)
			MaxIndex = UBound(Data)
			' Compute index of item
			P = MinIndex + (MaxIndex - MinIndex) * PerCent
			If P < MinIndex Then P = MinIndex
			If P > MaxIndex Then P = MaxIndex
			I = Int(P)
			' If index is integer number - return the item(index)
			If I = P Then 
				Quantile = Data(I)
				' If index is not integer - return a linear combination of the nearest items
			Else 
				P = P - I
				Quantile = (1 - P) * Data(I) + P * Data(I + 1)
			End If 
		End Get 
	End Property 
	' Quick sort number's array (Return number of rearrangement)
	Public Function SortDouble(ByRef A() As Double, ByVal L As Integer, ByVal R As Integer) As Integer
		Dim I, M, j As Integer
		Dim X, C As Double
		Dim N As Integer
		' If more then one unit contain into our interval, then make sort
		If L < R Then 
			' The middle unit's value will be divide array on 2 parts
			X = A(L + (R - L) \ 2)
			I = L - 1
			j = R + 1
			Do 
				Do 
					j = j - 1
					' Right part contain numbers, greater then X
				Loop Until A(j) <= X
				Do 
					I = I + 1
					' Left part contain numbers, not greater then X
				Loop Until A(I) >= X
				If I < j Then 
					' Exchange, if left number greater, then right
					If A(I) > A(j) Then 
						C = A(I)
						A(I) = A(j)
						A(j) = C
						' Increment of count of rearrangement
						N = N + 1
					End If 
				Else 
					' Rearrangement is finished, if i=j
					M = j
					Exit Do 
				End If 
			Loop 
			' Recursive call of sorting procedure for left and right parts
			SortDouble = SortDouble(A, L, M) + SortDouble(A, M + 1, R) + N
		End If 
	End Function 
End Module

'End Namespace
