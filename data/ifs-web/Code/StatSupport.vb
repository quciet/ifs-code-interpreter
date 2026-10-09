Option Compare Binary
Option Explicit On
Option Strict Off

Imports VB = Microsoft.VisualBasic
Imports System.Math
Imports System.IO
Imports NLog

'Namespace IFsWeb

Public Module StatSupport
    'Support the functions from the SLATEC library.
    'Copyright 1996-2000 Entisoft
    ''EntisoftTools VBProjectToHTMLFiles NoDocument

    Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

    Public Function ABS_(
        ByRef vX As Object
        ) As Object
        ''Generic

        ABS_ = Abs(vX)
    End Function

    Public Function EXP_(
        ByRef vX As Object
        ) As Object
        ''Generic

        EXP_ = Exp(vX)
    End Function


    Public Function INT_(
        ByRef vX As Object
        ) As Long
        ''Generic

        INT_ = Fix(vX)
    End Function

    Public Function LOG_(
        ByRef vX As Object
        ) As Object
        ''Generic

        LOG_ = Log(vX)
    End Function


    Public Function REAL(
        ByRef vX As Object
        ) As Single
        ''Generic

        REAL = vX
    End Function

    Public Function SIGN_(
          ByRef vX As Object _
        , ByRef vY As Object
        ) As Object
        'Transfer of Sign
        ''Generic

        'Definition:
        '   Abs(X) if Y >= 0
        '  -Abs(X) if Y < 0

        If vY >= 0 Then
            If vX >= 0 Then
                SIGN_ = vX
            Else
                SIGN_ = -vX
            End If
        Else
            If vX >= 0 Then
                SIGN_ = -vX
            Else
                SIGN_ = vX
            End If
        End If
    End Function


    Public Function SQRT_(
        ByRef vX As Object
        ) As Object
        ''Generic

        SQRT_ = Sqrt(vX)
    End Function



    ''Wrapped
    ''EntisoftTools VBProjectToHTMLFiles Skip
    Public Function DERF(
        ByRef X As Double
        ) As Double

        '  DECK DERF
        '  ***BEGIN PROLOGUE  DERF
        '  ***PURPOSE  Compute the error function.
        '  ***LIBRARY   SLATEC (FNLIB)
        '  ***CATEGORY  C8A, L5A1E
        '  ***TYPE      DOUBLE PRECISION (ERF-S, DERF-D)
        '  ***KEYWORDS  ERF, ERROR FUNCTION, FNLIB, SPECIAL FUNCTIONS
        '  ***AUTHOR  Fullerton, W., (LANL)
        '  ***DESCRIPTION
        '
        '   DERF(X) calculates the double precision error function for double
        '   precision argument X.
        '
        '   Series for ERF        on the interval  0.          to  1.00000E+00
        '                                          with weighted error   1.28E-32
        '                                           log weighted error  31.89
        '                                 significant figures required  31.05
        '                                      decimal places required  32.55
        '
        '  ***REFERENCES  (NONE)
        '  ***ROUTINES CALLED  D1MACH, DCSEVL, DERFC, INITDS
        '  ***REVISION HISTORY  (YYMMDD)
        '     770701  DATE WRITTEN
        '     890531  Changed all specific intrinsics to generic.  (WRB)
        '     890531  REVISION DATE from Version 3.2
        '     891214  Prologue converted to Version 4.0 format.  (BAB)
        '     900727  Added EXTERNAL statement.  (WRB)
        '     920618  Removed space from variable name.  (RWC, WRB)
        '  ***END PROLOGUE  DERF
        '  ***FIRST EXECUTABLE STATEMENT  DERF

        Static ERFCS#(0 To 21), SQEPS#, SQRTPI#, XBIG#, Y#
        Static NTERF&
        Dim RTN_VAL#

        Static bInitDone As Boolean
        If Not bInitDone Then
            ERFCS(1) = -0.0490461212346918
            ERFCS(2) = -0.142261205103714
            ERFCS(3) = 0.0100355821875998
            ERFCS(4) = -0.000576876469976749
            ERFCS(5) = 0.0000274199312521961
            ERFCS(6) = -0.00000110431755073445
            ERFCS(7) = 0.000000038488755420345
            ERFCS(8) = -0.00000000118085825338755
            ERFCS(9) = 0.0000000000323342158260509
            ERFCS(10) = -0.000000000000799101594700455
            ERFCS(11) = 0.0000000000000179907251139615
            ERFCS(12) = -0.000000000000000371863548781869
            ERFCS(13) = 7.10359900371425E-18
            ERFCS(14) = -1.26124551191552E-19
            ERFCS(15) = 2.09164069417693E-21
            ERFCS(16) = -3.25397310293141E-23
            ERFCS(17) = 4.76686720979767E-25
            ERFCS(18) = -6.59801207828513E-27
            ERFCS(19) = 8.65501146996376E-29
            ERFCS(20) = -1.07889251774981E-30
            ERFCS(21) = 1.2811883993017E-32

            SQRTPI = 1.77245385090552

            NTERF = INITDS(ERFCS, 21, 0.1 * REAL(D1MACH(3)))
            XBIG = SQRT_(-LOG_(SQRTPI * D1MACH(3)))
            SQEPS = SQRT_(2.0# * D1MACH(3))

            bInitDone = True
        End If

        Y = ABS_(X)
        If Y > 1.0# Then GoTo 20
        ''
        ''   ERF(X) = 1.0 - ERFC(X)  FOR  -1.0 .LE. X .LE. 1.0
        ''
        If Y <= SQEPS Then RTN_VAL = 2.0# * X * X / SQRTPI
        If Y > SQEPS Then RTN_VAL = X * (1.0# + DCSEVL(2.0# * X * X - 1.0#, ERFCS, NTERF))
        DERF = RTN_VAL
        Exit Function
        ''
        ''   ERF(X) = 1.0 - ERFC(X) FOR ABS(X) .GT. 1.0
        ''
20:
        If Y <= XBIG Then RTN_VAL = SIGN_(1.0# - DERFC(Y), X)
        If Y > XBIG Then RTN_VAL = SIGN_(1.0#, X)
        ''
        DERF = RTN_VAL
    End Function

    ''EntisoftTools VBProjectToHTMLFiles Skip
    Public Function INITDS(
          ByRef OS() As Double _
        , ByRef NOS As Long _
        , ByRef ETA As Single
        ) As Long

        '  DECK INITDS
        '  ***BEGIN PROLOGUE  INITDS
        '  ***PURPOSE  Determine the number of terms needed in an orthogonal
        '              polynomial series so that it meets a specified accuracy.
        '  ***LIBRARY   SLATEC (FNLIB)
        '  ***CATEGORY  C3A2
        '  ***TYPE      DOUBLE PRECISION (INITS-S, INITDS-D)
        '  ***KEYWORDS  CHEBYSHEV, FNLIB, INITIALIZE, ORTHOGONAL POLYNOMIAL,
        '               ORTHOGONAL SERIES, SPECIAL FUNCTIONS
        '  ***AUTHOR  Fullerton, W., (LANL)
        '  ***DESCRIPTION
        '
        '    Initialize the orthogonal series, represented by the array OS, so
        '    that INITDS is the number of terms needed to insure the error is no
        '    larger than ETA.  Ordinarily, ETA will be chosen to be one-tenth
        '    machine precision.
        '
        '               Input Arguments --
        '     OS     double precision array of NOS coefficients in an orthogonal
        '            series.
        '     NOS    number of coefficients in OS.
        '     ETA    single precision scalar containing requested accuracy of
        '            series.
        '
        '  ***REFERENCES  (NONE)
        '  ***ROUTINES CALLED  XERMSG
        '  ***REVISION HISTORY  (YYMMDD)
        '     770601  DATE WRITTEN
        '     890531  Changed all specific intrinsics to generic.  (WRB)
        '     890831  Modified array declarations.  (WRB)
        '     891115  Modified error message.  (WRB)
        '     891115  REVISION DATE from Version 3.2
        '     891214  Prologue converted to Version 4.0 format.  (BAB)
        '     900315  CALLs to XERROR changed to CALLs to XERMSG.  (THJ)
        '  ***END PROLOGUE  INITDS
        '  ***FIRST EXECUTABLE STATEMENT  INITDS

        Dim RTN_VAL&
        If NOS < 1 Then XERMSG(30128, "SLATEC", "INITDS", "Number of coefficients is less than 1", 2, 1)
        ''
        Static ERR_!
        ERR_ = 0.0#
        Static II&
        Static I&
        For II = 1 To NOS
            I = NOS + 1 - II
            ERR_ = ERR_ + ABS_(REAL(OS(I)))
            If ERR_ > ETA Then GoTo 20
10:
        Next II
        ''
20:
        If I = NOS Then XERMSG(30129, "SLATEC", "INITDS", "Chebyshev series too short for specified accuracy", 1, 1)
        RTN_VAL = I
        ''
        INITDS = RTN_VAL
    End Function

    Public Function D1MACH(
        ByVal vI As Byte
        ) As Double
        'Return floating point machine dependent constants.

        'D1MACH can be used to obtain machine-dependent parameters for the
        ' local machine environment.  It is a function subprogram with one
        ' (input) argument, and can be referenced as follows:
        '     D = D1Mach(I)
        'where I=1,...,5.  The (output) value of D above is determined by
        ' the (input) value of I.  The results for various values of I are
        ' discussed below.

        '  D1MACH( 1) = B**(EMIN-1), the smallest positive magnitude.
        '  D1MACH( 2) = B**EMAX*(1 - B**(-T)), the largest magnitude.
        '  D1MACH( 3) = B**(-T), the smallest relative spacing.
        '  D1MACH( 4) = B**(1-T), the largest relative spacing.
        '  D1MACH( 5) = LOG10(B)

        Static bInitDone As Boolean
        Static DMach(0 To 5) As Double

        If Not bInitDone Then

            DMach(1) = I1MACH(10) ^ (I1MACH(15) - 1)
            DMach(2) = 1.79769313486231E+308
            DMach(3) = I1MACH(10) ^ (-I1MACH(14))
            DMach(4) = I1MACH(10) ^ (1 - I1MACH(14))
            DMach(5) = Log(I1MACH(10)) / Log(10)

            bInitDone = True
        End If

        D1MACH = DMach(vI)

        'Assume double precision numbers are represented in the T-digit, base-B form
        '     sign (B**E)*( (X(1)/B) + ... + (X(T)/B**T) )
        'where 0 <= X(I) < B for I=1,...,T, 0 .LT. X(1), and EMIN <= E <= EMAX.

        'The values of B, T, EMIN and EMAX are provided in I1MACH as follows:
        '  I1MACH(10) = B, the base.
        '  I1MACH(14) = T, the number of base-B digits.
        '  I1MACH(15) = EMIN, the smallest exponent E.
        '  I1MACH(16) = EMAX, the largest exponent E.

        'References:
        ' P. A. Fox, A. D. Hall and N. L. Schryer, Framework for a portable library,
        ' ACM Transactions on Mathematical Software 4, 2 (June 1978), pp. 177-188.
        'Authors:
        '  Fox, P. A., (Bell Labs)
        '  Hall, A. D., (Bell Labs)
        '  Schryer, N. L., (Bell Labs)
    End Function

    Public Function I1MACH(
        ByVal vI As Byte
        ) As Long
        ' Return integer machine dependent constants.

        'I1MACH can be used to obtain machine-dependent parameters for the
        ' local machine environment.  It is a function subprogram with one
        ' (input) argument and can be referenced as follows:
        '     K = I1Mach(I)
        'where I=1,...,16.  The (output) value of K above is determined by
        ' the (input) value of I.  The results for various values of I are
        ' discussed below.

        Static bInitDone As Boolean
        Static IMach(0 To 16) As Long
        If Not bInitDone Then
            bInitDone = True


            '  I/O unit numbers:
            '     I1MACH( 1) = the standard input unit.
            '     I1MACH( 2) = the standard output unit.
            '     I1MACH( 3) = the standard punch unit.
            '     I1MACH( 4) = the standard error message unit.
            IMach(1) = 5
            IMach(2) = 6
            IMach(3) = 0
            IMach(4) = 0

            '  Words:
            '     I1MACH( 5) = the number of bits per integer storage unit.
            '     I1MACH( 6) = the number of characters per integer storage unit.
            IMach(5) = 32
            IMach(6) = 4

            '  Integers:
            '     assume integers are represented in the S-digit, base-A form
            '        sign ( X(S-1)*A**(S-1) + ... + X(1)*A + X(0) )
            '     where 0 <= X(I) < A for I=0,...,S-1.
            '
            '     I1MACH( 7) = A, the base.
            '     I1MACH( 8) = S, the number of base-A digits.
            '     I1MACH( 9) = A**S - 1, the largest magnitude.
            IMach(7) = 2
            IMach(8) = 31
            IMach(9) = 2147483647

            '  Floating-Point Numbers:
            '     Assume floating-point numbers are represented in the T-digit, base-B form
            '        sign (B**E)*( (X(1)/B) + ... + (X(T)/B**T) )
            '     where 0 <= X(I) < B for I=1,...,T, 0 < X(1), and EMIN <= E <= EMAX.
            '
            '     I1MACH(10) = B, the base.
            IMach(10) = 2

            '  Single-Precision:
            '     I1MACH(11) = T, the number of base-B digits.
            '     I1MACH(12) = EMIN, the smallest exponent E.
            '     I1MACH(13) = EMAX, the largest exponent E.
            IMach(11) = 24
            IMach(12) = -125
            IMach(13) = 128

            '  Double-Precision:
            '     I1MACH(14) = T, the number of base-B digits.
            '     I1MACH(15) = EMIN, the smallest exponent E.
            '     I1MACH(16) = EMAX, the largest exponent E.
            IMach(14) = 53
            IMach(15) = -1021
            IMach(16) = 1024
        End If

        I1MACH = IMach(vI)

        'References:
        ' P. A. Fox, A. D. Hall and N. L. Schryer, Framework for a portable library,
        ' ACM Transactions on Mathematical Software 4, 2 (June 1978), pp. 177-188.
        'Authors:
        '  Fox, P. A., (Bell Labs)
        '  Hall, A. D., (Bell Labs)
        '  Schryer, N. L., (Bell Labs)
    End Function


    Public Function LogNormalPDF(
          ByVal vX As Object _
        , Optional ByVal vMean As Object = 0.0# _
        , Optional ByVal vStdDev As Object = 1.0#
        ) As Object
        'Lognormal Probability Density Function

        'See also:
        '  LogNormalCDF Function
        '  LogNormalInverse Function
        '  LogNormalRandom Function
        '  Declarations Topic
        'vX: Function returns dbnull.value if vX cannot be fixed up to a Double precision floating point number.
        'vMean: Mean of the distribution.
        ' Function returns dbnull.value if vMean cannot be fixed up to a Double precision floating point number.
        ' Argument defaults to a value of zero (0) if it is missing.
        'vStdDev: Standard deviation of the distribution.
        ' Function returns dbnull.value if vStdDev is less than or equal to zero (<=0).
        ' Argument defaults to a value of one (1) if it is missing.

        On Error GoTo ErrSub

        vX = CDbl(vX)
        LogNormalPDF = NormalPDF(Log(vX), vMean, vStdDev) / vX

EndSub:
        Exit Function

ErrSub:
        _logger.Error(Err.GetException, "LogNormalPDF - exception")
        LogNormalPDF = DBNull.Value
        Resume EndSub
    End Function

    Public Function LogNormalCDF(
          ByVal vX As Object _
        , Optional ByVal vMean As Object = 0.0# _
        , Optional ByVal vStdDev As Object = 1.0#
        ) As Object
        'Lognormal Cumulative Distribution Function

        'See also:
        '  LogNormalInverse Function
        '  LogNormalPDF Function
        '  LogNormalRandom Function
        '  Declarations Topic
        '  LOGNORMDIST Function (Microsoft Excel)
        'vX: Function returns dbnull.value if vX cannot be fixed up to a Double precision floating point number.
        'vMean: Mean of the distribution.
        ' Function returns dbnull.value if vMean cannot be fixed up to a Double precision floating point number.
        ' Argument defaults to a value of zero (0) if it is missing.
        'vStdDev: Standard deviation of the distribution.
        ' Function returns dbnull.value if vStdDev is less than or equal to zero (<=0).
        ' Argument defaults to a value of one (1) if it is missing.

        On Error GoTo ErrSub

        LogNormalCDF = NormalCDF(Log(CDbl(vX)), vMean, vStdDev)

EndSub:
        Exit Function

ErrSub:
        _logger.Error(Err.GetException, "LogNormalCDF - exception")
        LogNormalCDF = DBNull.Value
        Resume EndSub
    End Function

    Public Function LogNormalInverse(
          ByVal vProbability As Object _
        , Optional ByVal vMean As Object = 0.0# _
        , Optional ByVal vStdDev As Object = 1.0#
        ) As Object
        'Inverse Lognormal Cumulative Distribution Function

        'See also:
        '  LogNormalCDF Function
        '  LogNormalRandom Function
        '  LogNormalPDF Function
        '  Declarations Topic
        '  LOGINV Function (Microsoft Excel)
        'vProbability: Function returns dbnull.value if vProbability is not between 0 and 1 inclusive.
        'vMean: Mean of the distribution.
        ' Function returns dbnull.value if vMean cannot be fixed up to a Double precision floating point number.
        ' Argument defaults to a value of zero (0) if it is missing.
        'vStdDev: Standard deviation of the distribution.
        ' Function returns dbnull.value if vStdDev is less than or equal to zero (<=0).
        ' Argument defaults to a value of one (1) if it is missing.

        On Error GoTo ErrSub

        Dim dblLow#, dblHigh#, dblEpsilon#, dblGuess#, intTry%

        If vProbability < 0 _
        Or vProbability > 1 _
        Or vStdDev <= 0 Then
            LogNormalInverse = DBNull.Value
            Exit Function
        End If

        dblLow = 0
        dblHigh = 10000000000.0# '' 1E10
        dblEpsilon = D1MACH(4)

        For intTry = 1 To 256
            dblGuess = (dblLow + dblHigh) / 2.0#
            If LogNormalCDF(dblGuess, vMean, vStdDev) < vProbability Then
                dblLow = dblGuess
            Else
                dblHigh = dblGuess
            End If

            If Abs((dblHigh - dblLow) / dblHigh) < dblEpsilon Then
                LogNormalInverse = dblGuess
                Exit Function
            End If
        Next intTry

EndSub:
        LogNormalInverse = DBNull.Value
        Exit Function

ErrSub:
        _logger.Error(Err.GetException, "LogNormalInverse - exception")
        Resume EndSub
    End Function


    Public Function NormalCDF(
          ByVal vX As Object _
        , Optional ByVal vMean As Object = 0 _
        , Optional ByVal vStdDev As Object = 1
        ) As Object
        'Normal Cumulative Distribution Function

        'See also:
        '  NormalInverse Function
        '  NormalRandom Function
        '  NormalPDF Function
        '  Declarations Topic
        '  NORMDIST Function (Microsoft Excel)
        '  NORMSDIST Function (Microsoft Excel)
        'vX: Function returns dbnull.value if vX cannot be fixed up to a Double precision floating point number.
        'vMean: Mean of the distribution.
        ' Function returns dbnull.value if vMean cannot be fixed up to a Double precision floating point number.
        ' Argument defaults to a value of zero (0) if it is missing.
        'vStdDev: Standard deviation of the distribution.
        ' Function returns dbnull.value if vStdDev is less than or equal to zero (<=0).
        ' Argument defaults to a value of one (1) if it is missing.

        On Error GoTo ErrSub

        If vStdDev <= 0 Then
            NormalCDF = DBNull.Value
            Exit Function
        End If

        vX = CDbl(vX)
        vMean = CDbl(vMean)
        vStdDev = CDbl(vStdDev)

        NormalCDF = (1.0# + DERF((vX - vMean) / (Sqrt(2.0#) * vStdDev))) / 2.0#

EndSub:
        Exit Function

ErrSub:
        _logger.Error(Err.GetException, "NormalCDF - exception")
        NormalCDF = DBNull.Value
        Resume EndSub
    End Function

    Public Function NormalInverse(
          ByVal vProbability As Object _
        , Optional ByVal vMean As Object = 0 _
        , Optional ByVal vStdDev As Object = 1
        ) As Object
        'Inverse Normal Cumulative Distribution Function

        'See also:
        '  NormalCDF Function
        '  NormalRandom Function
        '  NormalPDF Function
        '  Declarations Topic
        '  NORMINV Function (Microsoft Excel)
        '  NORMSINV Function (Microsoft Excel)
        'vProbability: Function returns dbnull.value if vProbability is not between 0 and 1 inclusive.
        'vMean: Mean of the distribution.
        ' Function returns dbnull.value if vMean cannot be fixed up to a Double precision floating point number.
        ' Argument defaults to a value of zero (0) if it is missing.
        'vStdDev: Standard deviation of the distribution.
        ' Function returns dbnull.value if vStdDev is less than or equal to zero (<=0).
        ' Argument defaults to a value of one (1) if it is missing.

        On Error GoTo ErrSub

        Dim dblLow#, dblHigh#, dblEpsilon#, dblGuess#, intTry%

        If vProbability < 0 _
        Or vProbability > 1 _
        Or vStdDev <= 0 Then
            NormalInverse = DBNull.Value
            Exit Function
        End If

        dblLow = -vStdDev * 10.0# + vMean
        dblHigh = vStdDev * 10.0# + vMean
        dblEpsilon = D1MACH(4)

        For intTry = 1 To 256
            dblGuess = (dblLow + dblHigh) / 2.0#
            If NormalCDF(dblGuess, vMean, vStdDev) < vProbability Then
                dblLow = dblGuess
            Else
                dblHigh = dblGuess
            End If

            If dblHigh = 0 Then
                If dblHigh - dblLow < dblEpsilon Then
                    NormalInverse = dblGuess
                    Exit Function
                End If
            Else
                If Abs((dblHigh - dblLow) / dblHigh) < dblEpsilon Then
                    NormalInverse = dblGuess
                    Exit Function
                End If
            End If
        Next intTry

EndSub:
        NormalInverse = DBNull.Value
        Exit Function

ErrSub:
        _logger.Error(Err.GetException, "NormalInverse - exception")
        Resume EndSub
    End Function

    ''EntisoftTools VBProjectToHTMLFiles Skip
    Public Function DCSEVL(
          ByRef X As Double _
        , ByRef CS() As Double _
        , ByRef N As Long
        ) As Double

        '  DECK DCSEVL
        '  ***BEGIN PROLOGUE  DCSEVL
        '  ***PURPOSE  Evaluate a Chebyshev series.
        '  ***LIBRARY   SLATEC (FNLIB)
        '  ***CATEGORY  C3A2
        '  ***TYPE      DOUBLE PRECISION (CSEVL-S, DCSEVL-D)
        '  ***KEYWORDS  CHEBYSHEV SERIES, FNLIB, SPECIAL FUNCTIONS
        '  ***AUTHOR  Fullerton, W., (LANL)
        '  ***DESCRIPTION
        '
        '    Evaluate the N-term Chebyshev series CS at X.  Adapted from
        '    a method presented in the paper by Broucke referenced below.
        '
        '         Input Arguments --
        '    X    value at which the series is to be evaluated.
        '    CS   array of N terms of a Chebyshev series.  In evaluating
        '         CS, only half the first coefficient is summed.
        '    N    number of terms in array CS.
        '
        '  ***REFERENCES  R. Broucke, Ten subroutines for the manipulation of
        '                   Chebyshev series, Algorithm 446, Communications of
        '                   the A.C.M. 16, (1973) pp. 254-256.
        '                 L. Fox and I. B. Parker, Chebyshev Polynomials in
        '                   Numerical Analysis, Oxford University Press, 1968,
        '                   page 56.
        '  ***ROUTINES CALLED  D1MACH, XERMSG
        '  ***REVISION HISTORY  (YYMMDD)
        '     770401  DATE WRITTEN
        '     890831  Modified array declarations.  (WRB)
        '     890831  REVISION DATE from Version 3.2
        '     891214  Prologue converted to Version 4.0 format.  (BAB)
        '     900315  CALLs to XERROR changed to CALLs to XERMSG.  (THJ)
        '     900329  Prologued revised extensively and code rewritten to allow
        '             X to be slightly outside interval (-1,+1).  (WRB)
        '     920501  Reformatted the REFERENCES section.  (WRB)
        '  ***END PROLOGUE  DCSEVL
        '  ***FIRST EXECUTABLE STATEMENT  DCSEVL

        Static B0#, B1#, B2#, ONEPL#, TWOX#
        Dim RTN_VAL#

        Static bInitDone As Boolean
        If Not bInitDone Then
            ONEPL = 1.0# + D1MACH(4)

            bInitDone = True
        End If

        If N < 1 Then XERMSG(30090, "SLATEC", "DCSEVL", "NUMBER OF TERMS .LE. 0", 2, 2)
        If N > 1000 Then XERMSG(30091, "SLATEC", "DCSEVL", "NUMBER OF TERMS .GT. 1000", 3, 2)
        If ABS_(X) > ONEPL Then XERMSG(30092, "SLATEC", "DCSEVL", "X OUTSIDE THE INTERVAL (-1,+1)", 1, 1)
        ''
        B1 = 0.0#
        B0 = 0.0#
        TWOX = 2.0# * X
        Static I&
        For I = 1 To N
            B2 = B1
            B1 = B0
            Static NI&
            NI = N + 1 - I
            B0 = TWOX * B1 - B2 + CS(NI)
10:
        Next I
        ''
        RTN_VAL = 0.5 * (B0 - B2)
        ''
        DCSEVL = RTN_VAL
    End Function

    ''Wrapped
    ''EntisoftTools VBProjectToHTMLFiles Skip
    Public Function DERFC(
        ByRef X As Double
        ) As Double

        '  DECK DERFC
        '  ***BEGIN PROLOGUE  DERFC
        '  ***PURPOSE  Compute the complementary error function.
        '  ***LIBRARY   SLATEC (FNLIB)
        '  ***CATEGORY  C8A, L5A1E
        '  ***TYPE      DOUBLE PRECISION (ERFC-S, DERFC-D)
        '  ***KEYWORDS  COMPLEMENTARY ERROR FUNCTION, ERFC, FNLIB,
        '               SPECIAL FUNCTIONS
        '  ***AUTHOR  Fullerton, W., (LANL)
        '  ***DESCRIPTION
        '
        '   DERFC(X) calculates the double precision complementary error function
        '   for double precision argument X.
        '
        '   Series for ERF        on the interval  0.          to  1.00000E+00
        '                                          with weighted Error   1.28E-32
        '                                           log weighted Error  31.89
        '                                 significant figures required  31.05
        '                                      decimal places required  32.55
        '
        '   Series for ERC2       on the interval  2.50000E-01 to  1.00000E+00
        '                                          with weighted Error   2.67E-32
        '                                           log weighted Error  31.57
        '                                 significant figures required  30.31
        '                                      decimal places required  32.42
        '
        '   Series for ERFC       on the interval  0.          to  2.50000E-01
        '                                          with weighted error   1.53E-31
        '                                           log weighted error  30.82
        '                                 significant figures required  29.47
        '                                      decimal places required  31.70
        '
        '  ***REFERENCES  (NONE)
        '  ***ROUTINES CALLED  D1MACH, DCSEVL, INITDS, XERMSG
        '  ***REVISION HISTORY  (YYMMDD)
        '     770701  DATE WRITTEN
        '     890531  Changed all specific intrinsics to generic.  (WRB)
        '     890531  REVISION DATE from Version 3.2
        '     891214  Prologue converted to Version 4.0 format.  (BAB)
        '     900315  CALLs to XERROR changed to CALLs to XERMSG.  (THJ)
        '     920618  Removed space from variable names.  (RWC, WRB)
        '  ***END PROLOGUE  DERFC
        '  ***FIRST EXECUTABLE STATEMENT  DERFC

        Static ERFCS#(0 To 21), ERFCCS#(0 To 59), ERC2CS#(0 To 49), SQEPS#, SQRTPI#, XMAX#, TXMAX#, XSML#, Y#
        Static NTERF&, NTERFC&, NTERC2&
        Dim RTN_VAL#

        Static bInitDone As Boolean
        If Not bInitDone Then
            ERFCS(1) = -0.0490461212346918
            ERFCS(2) = -0.142261205103714
            ERFCS(3) = 0.0100355821875998
            ERFCS(4) = -0.000576876469976749
            ERFCS(5) = 0.0000274199312521961
            ERFCS(6) = -0.00000110431755073445
            ERFCS(7) = 0.000000038488755420345
            ERFCS(8) = -0.00000000118085825338755
            ERFCS(9) = 0.0000000000323342158260509
            ERFCS(10) = -0.000000000000799101594700455
            ERFCS(11) = 0.0000000000000179907251139615
            ERFCS(12) = -0.000000000000000371863548781869
            ERFCS(13) = 7.10359900371425E-18
            ERFCS(14) = -1.26124551191552E-19
            ERFCS(15) = 2.09164069417693E-21
            ERFCS(16) = -3.25397310293141E-23
            ERFCS(17) = 4.76686720979767E-25
            ERFCS(18) = -6.59801207828513E-27
            ERFCS(19) = 8.65501146996376E-29
            ERFCS(20) = -1.07889251774981E-30
            ERFCS(21) = 1.2811883993017E-32

            ERC2CS(1) = -0.0696013466023095
            ERC2CS(2) = -0.0411013393626209
            ERC2CS(3) = 0.00391449586668963
            ERC2CS(4) = -0.000490639565054898
            ERC2CS(5) = 0.0000715747900137704
            ERC2CS(6) = -0.0000115307163413123
            ERC2CS(7) = 0.000001994670590202
            ERC2CS(8) = -0.000000364266647159922
            ERC2CS(9) = 0.0000000694437261000501
            ERC2CS(10) = -0.0000000137122090210437
            ERC2CS(11) = 0.00000000278838966100714
            ERC2CS(12) = -0.000000000581416472433116
            ERC2CS(13) = 0.000000000123892049175275
            ERC2CS(14) = -0.0000000000269063914530674
            ERC2CS(15) = 0.00000000000594261435084791
            ERC2CS(16) = -0.00000000000133238673575812
            ERC2CS(17) = 0.000000000000302804680617713
            ERC2CS(18) = -0.0000000000000696664881494103
            ERC2CS(19) = 0.0000000000000162085454105392
            ERC2CS(20) = -0.00000000000000380993446525049
            ERC2CS(21) = 0.000000000000000904048781597883
            ERC2CS(22) = -0.000000000000000216400619508961
            ERC2CS(23) = 5.22210223399586E-17
            ERC2CS(24) = -1.26972960236456E-17
            ERC2CS(25) = 3.1091455042762E-18
            ERC2CS(26) = -7.6637629203203895E-19
            ERC2CS(27) = 1.90081925136275E-19
            ERC2CS(28) = -4.74220727906904E-20
            ERC2CS(29) = 1.18964920007653E-20
            ERC2CS(30) = -3.00003559032578E-21
            ERC2CS(31) = 7.60299345304325E-22
            ERC2CS(32) = -1.93590944760687E-22
            ERC2CS(33) = 4.95139912477334E-23
            ERC2CS(34) = -1.27180748133637E-23
            ERC2CS(35) = 3.28004960046951E-24
            ERC2CS(36) = -8.4923201768229E-25
            ERC2CS(37) = 2.20691789280756E-25
            ERC2CS(38) = -5.75561724569653E-26
            ERC2CS(39) = 1.50619153363923E-26
            ERC2CS(40) = -3.9545029590188E-27
            ERC2CS(41) = 1.0415297041515E-27
            ERC2CS(42) = -2.75148779527877E-28
            ERC2CS(43) = 7.29005820549756E-29
            ERC2CS(44) = -1.93693964591595E-29
            ERC2CS(45) = 5.16035711205149E-30
            ERC2CS(46) = -1.37841932219309E-30
            ERC2CS(47) = 3.69132679310707E-31
            ERC2CS(48) = -9.90938959062437E-32
            ERC2CS(49) = 2.66649170519539E-32

            ERFCCS(1) = 0.0715179310202925
            ERFCCS(2) = -0.0265324343376067
            ERFCCS(3) = 0.00171115397792086
            ERFCCS(4) = -0.000163751663458518
            ERFCCS(5) = 0.000019871293500552
            ERFCCS(6) = -0.00000284371241276656
            ERFCCS(7) = 0.000000460616130896313
            ERFCCS(8) = -0.0000000822775302587921
            ERFCCS(9) = 0.000000015921418727709
            ERFCCS(10) = -0.00000000329507136225284
            ERFCCS(11) = 0.000000000722343976040056
            ERFCCS(12) = -0.000000000166485581339873
            ERFCCS(13) = 0.0000000000401039258823766
            ERFCCS(14) = -0.0000000000100481621442573
            ERFCCS(15) = 0.00000000000260827591330033
            ERFCCS(16) = -0.000000000000699111056040402
            ERFCCS(17) = 0.000000000000192949233326171
            ERFCCS(18) = -0.0000000000000547013118875433
            ERFCCS(19) = 0.000000000000015896633097627
            ERFCCS(20) = -0.00000000000000472689398019756
            ERFCCS(21) = 0.00000000000000143587337678498
            ERFCCS(22) = -0.000000000000000444951056181736
            ERFCCS(23) = 0.000000000000000140481088476823
            ERFCCS(24) = -4.51381838776421E-17
            ERFCCS(25) = 1.47452154104513E-17
            ERFCCS(26) = -4.89262140694578E-18
            ERFCCS(27) = 1.64761214141065E-18
            ERFCCS(28) = -5.62681717632941E-19
            ERFCCS(29) = 1.94744338223208E-19
            ERFCCS(30) = -6.82630564294842E-20
            ERFCCS(31) = 2.42198888729865E-20
            ERFCCS(32) = -8.69341413350307E-21
            ERFCCS(33) = 3.15518034622809E-21
            ERFCCS(34) = -1.15737232404961E-21
            ERFCCS(35) = 4.28894716160565E-22
            ERFCCS(36) = -1.60503074205762E-22
            ERFCCS(37) = 6.0632987574538E-23
            ERFCCS(38) = -2.31140425169796E-23
            ERFCCS(39) = 8.88877854066189E-24
            ERFCCS(40) = -3.44726057665138E-24
            ERFCCS(41) = 1.34786546020697E-24
            ERFCCS(42) = -5.31179407112502E-25
            ERFCCS(43) = 2.10934105861978E-25
            ERFCCS(44) = -8.43836558792379E-26
            ERFCCS(45) = 3.39998252494521E-26
            ERFCCS(46) = -1.37945238807324E-26
            ERFCCS(47) = 5.63449031183325E-27
            ERFCCS(48) = -2.31649043447707E-27
            ERFCCS(49) = 9.58446284460181E-28
            ERFCCS(50) = -3.99072288033011E-28
            ERFCCS(51) = 1.67212922594448E-28
            ERFCCS(52) = -7.04599152276601E-29
            ERFCCS(53) = 2.97976840286421E-29
            ERFCCS(54) = -1.26252246646062E-29
            ERFCCS(55) = 5.39543870454249E-30
            ERFCCS(56) = -2.38099288253146E-30
            ERFCCS(57) = 1.09905283010276E-30
            ERFCCS(58) = -4.86771374164497E-31
            ERFCCS(59) = 1.52587726411036E-31

            SQRTPI = 1.77245385090552

            Static ETA!
            ETA = 0.1 * REAL(D1MACH(3))
            NTERF = INITDS(ERFCS, 21, ETA)
            NTERFC = INITDS(ERFCCS, 59, ETA)
            NTERC2 = INITDS(ERC2CS, 49, ETA)
            ''
            XSML = -SQRT_(-LOG_(SQRTPI * D1MACH(3)))
            TXMAX = SQRT_(-LOG_(SQRTPI * D1MACH(1)))
            XMAX = TXMAX - 0.5 * LOG_(TXMAX) / TXMAX - 0.01
            SQEPS = SQRT_(2.0# * D1MACH(3))

            bInitDone = True
        End If

        If X > XSML Then GoTo 20
        ''
        ''   ERFC(X) = 1.0 - ERF(X)  FOR  X .LT. XSML
        ''
        RTN_VAL = 2.0#
        DERFC = RTN_VAL
        Exit Function
        ''
20:
        If X > XMAX Then GoTo 40
        Y = ABS_(X)
        If Y > 1.0# Then GoTo 30
        ''
        ''   ERFC(X) = 1.0 - ERF(X)  FOR ABS(X) .LE. 1.0
        ''
        If Y < SQEPS Then RTN_VAL = 1.0# - 2.0# * X / SQRTPI
        If Y >= SQEPS Then RTN_VAL = 1.0# - X * (1.0# + DCSEVL(2.0# * X * X - 1.0#, ERFCS, NTERF))
        DERFC = RTN_VAL
        Exit Function
        ''
        ''   ERFC(X) = 1.0 - ERF(X)  FOR  1.0 .LT. ABS(X) .LE. XMAX
        ''
30:
        Y = Y * Y
        If Y <= 4.0# Then RTN_VAL = EXP_(-Y) / ABS_(X) * (0.5 + DCSEVL((8.0# / Y - 5.0#) / 3.0#, ERC2CS, NTERC2))
        If Y > 4.0# Then RTN_VAL = EXP_(-Y) / ABS_(X) * (0.5 + DCSEVL(8.0# / Y - 1.0#, ERFCCS, NTERFC))
        If X < 0.0# Then RTN_VAL = 2.0# - RTN_VAL
        DERFC = RTN_VAL
        Exit Function
        ''
40:
        XERMSG(30096, "SLATEC", "DERFC", "X SO BIG ERFC UNDERFLOWS", 1, 1)
        RTN_VAL = 0.0#
        DERFC = RTN_VAL
    End Function


    'future - consider rewriting this fn.
    Public Sub XERMSG(
          ByRef vNum As Long _
        , ByRef vLibrary As String _
        , ByRef vSubroutine As String _
        , ByRef vMessage As String _
        , ByRef vErrorNumber As Long _
        , ByRef vLevel As Long
        )
        'Debug.Print vLibrary & ": " & vSubroutine & ": Error #" & vErrorNumber & " Severity " & vLevel & ": " & vMessage
        If vLevel >= 1 Then
            Err.Raise(vNum, vLibrary & "." & vSubroutine, vMessage)
        End If
    End Sub

    Public Function NormalPDF(
          ByVal vX As Object _
        , Optional ByVal vMean As Object = 0 _
        , Optional ByVal vStdDev As Object = 1
        ) As Object
        'Normal Probability Density Function

        'See also:
        '  NormalCDF Function
        '  NormalInverse Function
        '  NormalRandom Function
        '  Declarations Topic
        '  NORMDIST Function (Microsoft Excel)
        '  NORMSDIST Function (Microsoft Excel)
        'vX: Function returns dbnull.value if vX cannot be fixed up to a Double precision floating point number.
        'vMean: Mean of the distribution.
        ' Function returns dbnull.value if vMean cannot be fixed up to a Double precision floating point number.
        ' Argument defaults to a value of zero (0) if it is missing.
        'vStdDev: Standard deviation of the distribution.
        ' Function returns dbnull.value if vStdDev is less than or equal to zero (<=0).
        ' Argument defaults to a value of one (1) if it is missing.

        On Error GoTo ErrSub

        Dim dblSqr2Pi#, dblTmp#

        If vStdDev <= 0 Then
            NormalPDF = DBNull.Value
            Exit Function
        End If

        vX = CDbl(vX)
        vMean = CDbl(vMean)
        vStdDev = CDbl(vStdDev)

        dblSqr2Pi = Sqrt(2.0# * Atan(1.0#) * 4.0#)
        dblTmp = vX - vMean
        NormalPDF = Exp(-(dblTmp * dblTmp / (vStdDev * 2.0# * vStdDev))) / (dblSqr2Pi * vStdDev)

EndSub:
        Exit Function

ErrSub:
        _logger.Error(Err.GetException, "NormalPDF - exception")
        NormalPDF = DBNull.Value
        Resume EndSub
    End Function

End Module

'End Namespace
