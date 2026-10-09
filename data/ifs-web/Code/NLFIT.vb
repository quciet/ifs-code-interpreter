Option Strict Off
Option Explicit On


'Namespace IFsWeb

Public Module NLFIT
 
	' Copyright (c) 1997 C. David Eagle Jr.
	' Development by International Futurs System under license
	 
	'UPGRADE_NOTE: DefInt I-N statement was removed. Variables were explicitly declared as type Short. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1005"'
	'UPGRADE_NOTE: DefDbl A-Z statement was removed. Variables were explicitly declared as type Double. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1005"'
	 
	'JRS 01/29/04
	Sub DECOMPOSE(ByRef n As Object, ByRef EMATRIX() As Object, ByRef BMATRIX As Object)
		Static Flip As Double
		Static STETA As Double
		Static CTETA As Double
		Static TETA As Double
		Static WW As Double
		Static AC As Double
		Static XLE As Double
		Static WIH As Double
		Static WIJ As Double
		Static i As Short
		Static XHLONG As Double
		Static XJLONG As Double
		Static SCALPROD As Double
		Static H As Double
		Static j As Short
		Static KI As Short
		Static NI As Short
		Static it As Short
		Static ITMAX As Short
		Static EPS As Double
		 
		' Decompose eigenvector matrix subroutine
		 
		EPS = 1E-08
		ITMAX = 10
		it = 0
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		NI = n * (n - 1) / 2
		KI = NI
		 
		Do 
			it = it + 1
			 
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For j = 1 To n - 1
				'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				For H = j + 1 To n
					SCALPROD = 0.0#
					XJLONG = 0.0#
					XHLONG = 0.0#
					 
					'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					For i = 1 To n
						'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						WIJ = BMATRIX(i, j)
						'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(i, H). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						WIH = BMATRIX(i, H)
						SCALPROD = SCALPROD + WIJ * WIH
						XJLONG = XJLONG + WIJ * WIJ
						XHLONG = XHLONG + WIH * WIH
					Next i
					 
					XLE = System.Math.Sqrt(XHLONG * XJLONG)
					 
					If (XLE > 0.0#) Then 
						AC = System.Math.Abs(SCALPROD / XLE)
					Else 
						AC = 0.0#
					End If 
					 
					SCALPROD = SCALPROD + SCALPROD
					WW = XJLONG - XHLONG
					If (AC > EPS Or WW < 0.0#) Then 
						If (System.Math.Abs(WW) >= System.Math.Abs(SCALPROD)) Then 
							TETA = System.Math.Abs(SCALPROD / WW)
							CTETA = 1.0# / System.Math.Sqrt(1.0# + TETA * TETA)
							STETA = TETA * CTETA
						Else 
							TETA = System.Math.Abs(WW / SCALPROD)
							STETA = 1.0# / System.Math.Sqrt(1.0# + TETA * TETA)
							CTETA = STETA * TETA
						End If 
						 
						CTETA = System.Math.Sqrt((1.0# + CTETA) / 2.0#)
						STETA = STETA / (CTETA + CTETA)
						 
						If (WW < 0.0#) Then 
							Flip = CTETA
							CTETA = STETA
							STETA = Flip
						End If 
						 
						If (SCALPROD < 0.0#) Then STETA =  - STETA
						 
						'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						For i = 1 To n
							'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
							Flip = BMATRIX(i, j)
							'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
							'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
							BMATRIX(i, j) = BMATRIX(i, H) * STETA + Flip * CTETA
							'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
							'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(i, H). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
							BMATRIX(i, H) = BMATRIX(i, H) * CTETA - Flip * STETA
						Next i
						KI = NI
					Else 
						KI = KI - 1
					End If 
				Next H
			Next j
		Loop Until ((KI <= 0) Or (it = ITMAX))
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For j = 1 To n
			'UPGRADE_WARNING: Couldn't resolve default property of object EMATRIX(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			EMATRIX(j) = 0.0#
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For i = 1 To n
				'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				'UPGRADE_WARNING: Couldn't resolve default property of object EMATRIX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				'UPGRADE_WARNING: Couldn't resolve default property of object EMATRIX(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				EMATRIX(j) = EMATRIX(j) + BMATRIX(i, j) * BMATRIX(i, j)
			Next i
			'UPGRADE_WARNING: Couldn't resolve default property of object EMATRIX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object EMATRIX(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			EMATRIX(j) = System.Math.Sqrt(EMATRIX(j))
		Next j
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For j = 1 To n
			If (EMATRIX(j) <> 0.0#) Then 
				'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				For i = 1 To n
					'UPGRADE_WARNING: Couldn't resolve default property of object EMATRIX(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					BMATRIX(i, j) = BMATRIX(i, j) / EMATRIX(j)
				Next i
			End If 
		Next j
		 
	End Sub 
	 
	'JRS 01/29/04
	Sub EXPLORE(ByRef n As Object, ByRef m As Object, ByRef X() As Double, ByRef Y() As Double, ByRef p() As Object, ByRef GMATRIX() As Object, ByRef D() As Object, ByRef XINFO As Object, ByRef EMATRIX() As Object, ByRef CMATRIX As Object, ByRef BMATRIX As Object, ByRef IRESP As Object)
		Static j As Short
		Static i As Short
		 
		Call FILL(n, m, X, Y, p, GMATRIX, D, XINFO, IRESP)
		 
		If (IRESP) Then 
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For i = 1 To n
				'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				For j = i To n
					'UPGRADE_WARNING: Couldn't resolve default property of object D(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object D(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object XINFO(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object CMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					CMATRIX(i, j) = XINFO(i, j) / D(i) / D(j)
					'UPGRADE_WARNING: Couldn't resolve default property of object CMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object CMATRIX(j, i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					CMATRIX(j, i) = CMATRIX(i, j)
				Next j
			Next i
			 
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For i = 1 To n
				'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				For j = 1 To n
					'UPGRADE_WARNING: Couldn't resolve default property of object CMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					BMATRIX(i, j) = CMATRIX(i, j)
				Next j
			Next i
			 
			Call DECOMPOSE(n, EMATRIX, BMATRIX)
		End If 
		 
	End Sub 
	 
	'JRS 01/29/04
	Sub FILL(ByRef n As Object, ByRef m As Object, ByRef X() As Double, ByRef y1() As Double, ByRef p() As Object, ByRef GMATRIX() As Object, ByRef D() As Object, ByRef XINFO As Object, ByRef IRESP As Object)
		Static DELTA As Double
		Static Y As Double
		Static k As Short
		Static j As Short
		Static i As Short
		 
		' Fill working matrices subroutine
		 
		Static DIF(m) As Object
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object IRESP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		IRESP = 1
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For j = i To n
				'UPGRADE_WARNING: Couldn't resolve default property of object XINFO(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				XINFO(i, j) = 0.0#
			Next j
			'UPGRADE_WARNING: Couldn't resolve default property of object GMATRIX(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			GMATRIX(i) = 0.0#
		Next i
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For k = 1 To m
			Call PARDIF(n, k, X, p, DIF)
			Call USERFUNC(k, X, p, Y)
			 
			'UPGRADE_WARNING: Couldn't resolve default property of object y1(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			DELTA = Y - y1(k)
			 
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For i = 1 To n
				'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				For j = i To n
					'UPGRADE_WARNING: Couldn't resolve default property of object DIF(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object DIF(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object XINFO(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object XINFO(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					XINFO(i, j) = XINFO(i, j) + DIF(i) * DIF(j)
				Next j
				'UPGRADE_WARNING: Couldn't resolve default property of object DIF(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				'UPGRADE_WARNING: Couldn't resolve default property of object GMATRIX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				'UPGRADE_WARNING: Couldn't resolve default property of object GMATRIX(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				GMATRIX(i) = GMATRIX(i) + DIF(i) * DELTA
			Next i
		Next k
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			'UPGRADE_WARNING: Couldn't resolve default property of object XINFO(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object D(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			D(i) = System.Math.Sqrt(XINFO(i, i))
			'UPGRADE_WARNING: Couldn't resolve default property of object IRESP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			If (D(i) = 0.0#) Then IRESP = 0
			For j = 1 To i - 1
				'UPGRADE_WARNING: Couldn't resolve default property of object XINFO(j, i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				'UPGRADE_WARNING: Couldn't resolve default property of object XINFO(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				XINFO(i, j) = XINFO(j, i)
			Next j
		Next i
		 
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(DIF, 0, DIF.Length)
		 
	End Sub 
	 
	'JRS 01/29/04
	Sub FIND(ByRef n As Object, ByRef EMATRIX() As Object, ByRef BMATRIX As Object, ByRef D() As Object, ByRef XINV As Object, ByRef GMATRIX() As Object, ByRef p() As Object, ByRef P1() As Object, ByRef XMY As Object)
		Static k As Short
		Static A As Double
		Static j As Short
		Static i As Short
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For j = i To n
				A = 0.0#
				'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				For k = 1 To n
					If (XMY > 0.0# Or ((XMY = 0.0#) And (EMATRIX(k) > 1E-06))) Then 
						'UPGRADE_WARNING: Couldn't resolve default property of object XMY. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						'UPGRADE_WARNING: Couldn't resolve default property of object EMATRIX(k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						'UPGRADE_WARNING: Couldn't resolve default property of object D(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						'UPGRADE_WARNING: Couldn't resolve default property of object D(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(j, k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						'UPGRADE_WARNING: Couldn't resolve default property of object BMATRIX(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						A = A + BMATRIX(i, k) * BMATRIX(j, k) / D(i) / D(j) / (EMATRIX(k) + XMY)
					End If 
				Next k
				'UPGRADE_WARNING: Couldn't resolve default property of object XINV(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				XINV(i, j) = A
				'UPGRADE_WARNING: Couldn't resolve default property of object XINV(j, i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				XINV(j, i) = A
			Next j
		Next i
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			A = 0.0#
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For j = 1 To n
				'UPGRADE_WARNING: Couldn't resolve default property of object GMATRIX(j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				'UPGRADE_WARNING: Couldn't resolve default property of object XINV(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				A = A - XINV(i, j) * GMATRIX(j)
			Next j
			'UPGRADE_WARNING: Couldn't resolve default property of object p(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object P1(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			P1(i) = p(i) + A
		Next i
		 
	End Sub 
	 
	'JRS 01/29/04
	Sub NLFIT(ByRef n As Object, ByRef m As Object, ByRef X() As Double, ByRef Y() As Double, ByRef PI() As Double, ByRef PU() As Double, ByRef PL() As Double, ByRef POPT() As Double, ByRef SDev() As Double, ByRef NITER As Object)
		Static IRD As Short
		Static ICONVER As Short
		Static F1 As Double
		Static INSIDE As Short
		Static IRESP As Short
		Static ISIG As Short
		Static ISC As Short
		Static FOLD As Double
		Static f As Double
		Static i As Short
		Static XMY As Double
		Static XMYMAX As Double
		Static EPS As Double
		Static MAXITER As Short
		 
		' Levenberg-Marquardt non-linear curve-fit subroutine
		 
		' Input
		 
		'  N    = number of fitting parameters
		'  M    = number of X and Y data points
		'  X()  = array of X data points ( M rows by 1 column )
		'  Y()  = array of Y data points ( M rows by 1 column )
		'  PI() = array of initial guesses for fitting parameters
		'         ( N rows by 1 column )
		'  PU() = array of upper bounds for fitting parameters
		'         ( N rows by 1 column )
		'  PL() = array of lower bounds for fitting parameters
		'         ( N rows by 1 column )
		 
		' Output
		 
		'  POPT() = array of optimized fitting parameters
		'           ( N rows by 1 column )
		'  SDEV() = array of standard deviations
		'           ( N rows by 1 column )
		'  NITER  = number of algorithm iterations
		 
		Static p(n) As Object
		Static POLD(n) As Object
		Static P1(n) As Object
		Static EMATRIX(n) As Object
		Static GMATRIX(n) As Object
		Static D(n) As Object
		Static BMATRIX(n, n) As Object
		Static XINV(n, n) As Object
		Static CMATRIX(n, n) As Object
		Static XINFO(n, n) As Object
		 
		MAXITER = 20
		EPS = 0.0001
		XMYMAX = 1000000.0#
		XMY = 0.01
		'UPGRADE_WARNING: Couldn't resolve default property of object NITER. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		NITER = 0
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			'UPGRADE_WARNING: Couldn't resolve default property of object PI(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object p(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			p(i) = PI(i)
			'UPGRADE_WARNING: Couldn't resolve default property of object p(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object POLD(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			POLD(i) = p(i)
		Next i
		 
		Call SQUARESUM(m, p, X, Y, f)
		 
		FOLD = f
		 
		Do 
			ISC = 0
			Call EXPLORE(n, m, X, Y, p, GMATRIX, D, XINFO, EMATRIX, CMATRIX, BMATRIX, IRESP)
			'UPGRADE_WARNING: Couldn't resolve default property of object NITER. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			NITER = NITER + 1
			If (IRESP) Then 
				While (XMY <= XMYMAX) And (ISC = 0)
					Call FIND(n, EMATRIX, BMATRIX, D, XINV, GMATRIX, p, P1, XMY)
					INSIDE = 1
					'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					For i = 1 To n
						'UPGRADE_WARNING: Couldn't resolve default property of object PL(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						'UPGRADE_WARNING: Couldn't resolve default property of object P1(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						'UPGRADE_WARNING: Couldn't resolve default property of object PU(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
						If (P1(i) >= PU(i) Or P1(i) <= PL(i)) Then INSIDE = 0
					Next i
					If (INSIDE = 1) Then 
						Call SQUARESUM(m, P1, X, Y, F1)
						If (F1 < f) Then 
							ISC = 1
							FOLD = f
							f = F1
							XMY = XMY / 100.0#
							'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
							For i = 1 To n
								'UPGRADE_WARNING: Couldn't resolve default property of object p(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
								'UPGRADE_WARNING: Couldn't resolve default property of object POLD(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
								POLD(i) = p(i)
								'UPGRADE_WARNING: Couldn't resolve default property of object P1(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
								'UPGRADE_WARNING: Couldn't resolve default property of object p(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
								p(i) = P1(i)
							Next i
						End If 
					End If 
					XMY = XMY * 10.0#
				End While 
			End If 
			Call SCHECK(n, p, POLD, f, FOLD, ISIG)
			'UPGRADE_WARNING: Couldn't resolve default property of object NITER. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		Loop Until ((NITER = MAXITER) Or (ISC = 0) Or (ISIG = 0))
		 
		If (ISC = 1) Then Call EXPLORE(n, m, X, Y, p, GMATRIX, D, XINFO, EMATRIX, CMATRIX, BMATRIX, IRESP)
		 
		If (IRESP = 0) Then 
			ICONVER = 0
		Else 
			IRD = 0
			 
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			For i = 1 To n
				'UPGRADE_WARNING: Couldn't resolve default property of object EMATRIX(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				If (EMATRIX(i) < EPS) Then IRD = 1
			Next i
			 
			Call FIND(n, EMATRIX, BMATRIX, D, XINV, GMATRIX, p, P1, XMY)
			 
			'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			If (m > n) Then 
				'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				For i = 1 To n
					'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object XINV(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					'UPGRADE_WARNING: Couldn't resolve default property of object SDev(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
					SDev(i) = System.Math.Sqrt(XINV(i, i) * f / (m - n))
				Next i
				 
				Call SCHECK(n, P1, p, f, F1, ISIG)
				 
				ICONVER = ISIG
				 
			End If 
		End If 
		 
		' load optimal parameter array
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			'UPGRADE_WARNING: Couldn't resolve default property of object p(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object POPT(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			POPT(i) = p(i)
		Next i
		 
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(p, 0, p.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(POLD, 0, POLD.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(P1, 0, P1.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(EMATRIX, 0, EMATRIX.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(GMATRIX, 0, GMATRIX.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(D, 0, D.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(BMATRIX, 0, BMATRIX.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(XINV, 0, XINV.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(CMATRIX, 0, CMATRIX.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(XINFO, 0, XINFO.Length)
		 
	End Sub 
	 
	'JRS 01/29/04
	Sub PARDIF(ByRef n As Object, ByRef k As Object, ByRef X() As Double, ByRef p() As Object, ByRef DIF() As Object)
		Static FDEC As Double
		Static FINC As Double
		Static DEL As Double
		Static PS As Double
		Static i As Short
		Static XINC As Double
		 
		' Numerical partial derivatives subroutine
		 
		Static XINCP(n) As Object
		Static XDECP(n) As Object
		 
		XINC = 0.01
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			'UPGRADE_WARNING: Couldn't resolve default property of object p(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object XINCP(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			XINCP(i) = p(i)
			'UPGRADE_WARNING: Couldn't resolve default property of object p(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object XDECP(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			XDECP(i) = p(i)
		Next i
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			'UPGRADE_WARNING: Couldn't resolve default property of object p(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			PS = p(i)
			DEL = XINC * PS
			If (PS = 0.0#) Then DEL = XINC
			'UPGRADE_WARNING: Couldn't resolve default property of object XINCP(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			XINCP(i) = PS + DEL
			'UPGRADE_WARNING: Couldn't resolve default property of object XDECP(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			XDECP(i) = PS - DEL
			Call USERFUNC(k, X, XINCP, FINC)
			Call USERFUNC(k, X, XDECP, FDEC)
			'UPGRADE_WARNING: Couldn't resolve default property of object DIF(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			DIF(i) = (FINC - FDEC) / 2.0# / DEL
			'UPGRADE_WARNING: Couldn't resolve default property of object XINCP(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			XINCP(i) = PS
			'UPGRADE_WARNING: Couldn't resolve default property of object XDECP(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			XDECP(i) = PS
		Next i
		 
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(XINCP, 0, XINCP.Length)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		System.Array.Clear(XDECP, 0, XDECP.Length)
		 
	End Sub 
	 
	Sub SCHECK(ByRef n As Object, ByRef Q1() As Object, ByRef Q2() As Object, ByRef FQ1 As Object, ByRef FQ2 As Object, ByRef ISIG As Object)
		Static i As Short
		Static EPS As Double
		 
		' Check for significant change in fit parameter subroutine
		 
		EPS = 1E-05
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object ISIG. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		ISIG = 0
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object n. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For i = 1 To n
			If (Q1(i) <> 0.0#) Then 
				'UPGRADE_WARNING: Couldn't resolve default property of object Q1(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				'UPGRADE_WARNING: Couldn't resolve default property of object Q2(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				'UPGRADE_WARNING: Couldn't resolve default property of object ISIG. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
				If (System.Math.Abs(1.0# - Q2(i) / Q1(i)) > EPS) Then ISIG = 1
			End If 
		Next i
		 
		If (System.Math.Abs(CInt(FQ2 < 1E-16))) Then 
			'UPGRADE_WARNING: Couldn't resolve default property of object ISIG. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			ISIG = 0
			'UPGRADE_WARNING: Couldn't resolve default property of object FQ1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object FQ2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		ElseIf (System.Math.Abs(1.0# - FQ2 / FQ1) > EPS) Then 
			'UPGRADE_WARNING: Couldn't resolve default property of object ISIG. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			ISIG = 1
		End If 
		 
	End Sub 
	 
	'JRS 01/29/04
	Sub SQUARESUM(ByRef m As Object, ByRef p() As Object, ByRef X() As Double, ByRef y1() As Double, ByRef SQRSUM As Object)
		Static Y As Double
		Static k As Short
		 
		' Sum of squares subroutine
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object SQRSUM. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		SQRSUM = 0.0#
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		For k = 1 To m
			Call USERFUNC(k, X, p, Y)
			'UPGRADE_WARNING: Couldn't resolve default property of object y1(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			'UPGRADE_WARNING: Couldn't resolve default property of object SQRSUM. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
			SQRSUM = SQRSUM + (Y - y1(k)) ^ 2
		Next k
		 
	End Sub 
	 
	'JRS 01/29/04
	Sub USERFUNC(ByRef k As Object, ByRef X() As Double, ByRef p() As Object, ByRef Y As Object)
		Static b3 As Double
		Static b2 As Double
		Static b1 As Double
		 
		' Non-linear fitting function subroutine
		 
		' Input
		 
		'  K   = index of X data array
		'  X() = array of X data points
		'  P() = array of fitting parameters
		 
		' Output
		 
		'  Y = function value at X(K)
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object p(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		b1 = p(1)
		'UPGRADE_WARNING: Couldn't resolve default property of object p(2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		b2 = p(2)
		'UPGRADE_WARNING: Couldn't resolve default property of object p(3). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		b3 = p(3)
		 
		'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		'UPGRADE_WARNING: Couldn't resolve default property of object X(k). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		'UPGRADE_WARNING: Couldn't resolve default property of object Y. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		Y = b1 / (1.0# + System.Math.Exp( - (System.Math.Log(81) / b2) * (X(k) - b3)))
		 
	End Sub 
End Module

'End Namespace
