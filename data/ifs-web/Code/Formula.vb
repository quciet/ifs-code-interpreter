Option Strict Off
Option Explicit On



'UPGRADE_WARNING: Class instancing was changed to public. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1043"'
'JRSVBNET <System.Runtime.InteropServices.ProgId("Formula_NET.Formula")> 
    Public Class Formula
        '**************************************************************
        '   Jose Roberto Solorzano
        '   September 2001
        '**************************************************************
        Dim sName As String
        Dim sDimension As String
        Dim sDimension2 As String 'jrs 2005/08/17
        Dim sDimension3 As String 'jrs 2006/06/14
        Dim sUnit As String
        Dim sFormula As String
        Dim sFormula2 As String 'jrs 2005/10/05
        Dim validoperators As Object
        Dim monoper As String
        Dim Variables As Vector
        Dim VariablesHis As Vector 'jrs 2005/12/19 RAM for Formulas
        Dim postfixexp As Vector
        Dim root As New Node
        Dim totYears As Short
        Dim ibegAll As Short
        Dim iendAll As Short
        'jrs 2015/01/20
        Dim iMaxendAll As Short

        Dim NumVars() As Object
        Dim iDimensionSize As Short
        'jrs 2008/01/16
        Dim sIndexVarListName As String
        'jrs 2008/01/29
        Dim iFirstYear As Integer 'jrs used for Reference operation (REF = ?)
        'jrs 2008/03/06
        Dim sDefinition As String
        'jrs 2008/05/21
        Dim bUseNullsAsZeros As Boolean
        Public Function get_size() 'cdv 2022/05/24 just a getter for formula creation
            Return NumVars.Length
        End Function

        Public Function get_formula_as_string() 'cdv 2022/06/02 needed getter for formula format
            Return sFormula
        End Function
        Public Sub init(ByRef x As String)
            sFormula = x
            ComputePostFix()
            BuildTree()
            Variables.sort(1, Variables.disSize)
            Variables.unique(1, Variables.disSize)
        End Sub

        Public Sub ComputePostFix()
            Dim stackop As Vector
            stackop = New Vector
            'Initializing variables
            postfixexp = New Vector
            Variables = New Vector
            'Checks for Format in the formula
            Checking()
            'Checks every character in the original expression
            Dim iLen As Short
            Dim exp As String
            iLen = Len(sFormula)
            exp = sFormula
            Dim i As Object
            Dim j As Short
            j = 1
            For i = 1 To iLen
                'If it's a valid operator
                'UPGRADE_WARNING: Couldn't resolve default property of object validoperators. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If (find_first_of(Mid(exp, i, 1), validoperators, 1) = 1) Then
                    ' If the stack of operators is not empty
                    ' and we are not dealing with a "("
                    ' and the precedence of the current operator is bigger or equal
                    ' to the precedence of the last element in the stack
                    If (stackop.disSize() > 0) Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Do While (Mid(exp, i, 1) <> "(" And stackop.display(stackop.disSize()) <> "(" And Precedence(Mid(exp, i, 1)) >= Precedence(stackop.display(stackop.disSize())))
                            'Adds the last operator in the stack into the postfix expression
                            postfixexp.push_back((stackop.display(stackop.disSize())))
                            'Removes the last operator from the stack
                            stackop.pop_back()
                            'Exits the loop if the stack is empty
                            If (stackop.disSize() = 0) Then Exit Do
                        Loop
                    End If
                    'If we are dealing with a ")"
                    'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If (Mid(exp, i, 1) = ")") Then
                        stackop.pop_back() ' Insert it to the stack
                    Else ' If it's another operand
                        'Insert it to the stack
                        'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        stackop.push_back((Mid(exp, i, 1)))
                        'Fix the operand name if it's cos or sin
                        'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        If (i + 2 < Len(exp) And ((Mid(exp, i, 3) = "cos" Or Mid(exp, i, 3) = "sin"))) Then
                            'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            stackop.append(Mid(exp, i + 1, 2), stackop.disSize())
                            'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            i = i + 2
                        End If
                    End If
                Else ' If it isn't a valid operator, should be an operand (constant or x)
                    'If the previous character in the original expression was also a number
                    'append this number to the previous one.
                    'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If (i > 1) Then
                        'If the last one was not a valid operator, it was part of the same variable
                        'UPGRADE_WARNING: Couldn't resolve default property of object validoperators. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        If (find_first_of(Mid(exp, i - 1, 1), validoperators, 1) = -1) Then
                            'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            postfixexp.append(Mid(exp, i, 1), postfixexp.disSize())
                            'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Variables.append(Mid(exp, i, 1), Variables.disSize())
                        Else
                            'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            postfixexp.push_back((Mid(exp, i, 1))) 'Insert a new operand into the postfix expression
                            'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            Variables.push_back((Mid(exp, i, 1))) 'Just for variables
                        End If
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        postfixexp.push_back((Mid(exp, i, 1))) 'Insert a new operand into the postfix expression
                        'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        Variables.push_back((Mid(exp, i, 1))) 'Just for variables
                    End If
                End If
            Next i

            'After checking all characters in original expression
            'empty the stack of operators into the postfix expression
            Do While (stackop.disSize() > 0)
                postfixexp.push_back((stackop.display(stackop.disSize())))
                stackop.pop_back()
            Loop
            'Remove all numeric values from the vector variable
            Dim n As Short
            For n = 1 To Variables.disSize()
                If n <= Variables.disSize() Then
                    If IsNumeric(Variables.display(n)) Then
                        Variables.remove((n))
                        'JRS 1/25/03
                        n = n - 1
                        'jrs 2014/01/17 also remove Year to create a constant
                    ElseIf UCase(Variables.display(n)) = "YEAR" Then
                        Variables.remove(n)
                        n = n - 1
                    End If
                End If
            Next n
        End Sub

        'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Private Sub Class_Initialize_Renamed()
            'Assigns the valid operators
            validoperators = "+-*/%^()!&?#" '!!!!!!!!!!!!!!-> READ:
            'If you add a new operand you'll also need to check the "Checking" procedure
            'the implicit multiplication needs to know about the new operand
            'do a search on implicit * with parenthesis and add the new operand there too
            'There's 2 places where you'll need to add it.

            'Assigns the unary operators
            monoper = "!&#"
            '"!" stands for the monooperand minus
            '"&" stands for the monooperand log
            '"#" returns 1 if result is null

            'jrs 2007/12/12
            'added operator ? for reference operation
            'REF(GDP, 2000) will return the GDP value in year 2000

            'jrs 2014/01/06
            'added the operator # for initialization operation
            'returns 1 if parameter is null
            '#(b/(a+c)) = 1
        End Sub
        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub

        Private Function find_first_of(ByRef S As String, ByVal x As String, ByVal ist As Short) As Short
            Dim j As Object
            Dim i As Object
            For i = ist To Len(S)
                For j = 1 To Len(x)
                    'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If (Mid(S, i, 1) = Mid(x, j, 1)) Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        find_first_of = i
                        Exit Function
                    End If
                Next j
            Next i
            find_first_of = -1
        End Function

        Private Function Precedence(ByRef x As String) As Short
            Select Case x
                Case "^", "&", "!" 'jrs 2008/04/29 moved ! (minus) from 3 to 1, ex. a*-1 was not working
                    Precedence = 1
                Case "*", "/", "%"
                    Precedence = 2
                Case "+", "-"
                    Precedence = 3
                Case ")"
                    Precedence = 4
            End Select
        End Function

        Public Sub BuildTree()
            ' Stack of subtrees
            Dim exprtree As VectorNodes
            exprtree = New VectorNodes
            'Check all operators and operands in the postfix expression
            Dim it As Short
            Dim NewNode As Node 'creates a new node
            For it = 1 To postfixexp.disSize()
                NewNode = New Node
                NewNode.sData = postfixexp.display(it) 'adds the symbol of the operand or operator to the node
                'if it's not a valid operator, should be and operand (constant or x)
                'UPGRADE_WARNING: Couldn't resolve default property of object validoperators. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If (find_first_of(postfixexp.display(it), validoperators, 1) = -1) Then
                    exprtree.push_back(NewNode) 'Inserts a subtree just with the operand (constant or x)
                Else 'if it's a valid operator
                    'asign the right subtree, which is the last in the stack of subtrees
                    NewNode.nRight = exprtree.display(exprtree.disSize())
                    NewNode.nRight.nParent = NewNode
                    exprtree.pop_back() 'removes the last subtree in the stack
                    'If it's not a unary operator, (there is another subtree)
                    If (find_first_of(postfixexp.display(it), monoper, 1) = -1) Then
                        NewNode.nLeft = exprtree.display(exprtree.disSize()) 'assign the left subtree, which is the last in the stack
                        NewNode.nLeft.nParent = NewNode
                        exprtree.pop_back() 'removes the last subtree in the stack
                    Else 'if it's unary
                        NewNode.nLeft = NewNode.nRight 'move it to the left subtree
                        'UPGRADE_NOTE: Object NewNode.nRight may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
                        NewNode.nRight = Nothing 'and remove the right subtre.
                    End If
                    exprtree.push_back(NewNode) 'Insert the resulting subtree into the stack
                End If
            Next it
            root = exprtree.display(1) 'Assigns the root to the only subtree left in the stack
        End Sub

        Public Function disVar() As Vector
            disVar = Variables
        End Function

        'jrs 2005/12/19 RAM for Formulas
        Public Function disVarHis() As Vector
            disVarHis = VariablesHis
        End Function

        Public Sub LoadNumData(ByRef x As Object)
            LoadNodes(root)
            ReDim NumVars(Variables.disSize())
            Dim i As Short
            For i = 1 To Variables.disSize()
                'UPGRADE_WARNING: Couldn't resolve default property of object x(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object NumVars(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                NumVars(i) = x(i)
            Next i
        End Sub

        Public Function RetriveNumData() As Object
            'UPGRADE_WARNING: Couldn't resolve default property of object RetriveNumData. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RetriveNumData = VB6.CopyArray(NumVars)
        End Function

        Public Sub LoadNodes(ByRef n As Node)
            'UPGRADE_WARNING: Couldn't resolve default property of object validoperators. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Dim i As Short
            If find_first_of((n.sData), validoperators, 1) = -1 Then
                i = 1
                Do While (i <= Variables.disSize())
                    If n.sData = Variables.display(i) Then
                        n.pPlot = i
                        Exit Do
                    Else
                        i = i + 1
                    End If
                Loop
            Else
                If Not (n.nLeft Is Nothing) Then
                    LoadNodes((n.nLeft))
                End If
                If Not (n.nRight Is Nothing) Then
                    LoadNodes((n.nRight))
                End If
            End If
        End Sub

        Public Function RetrArr(ByRef n As Node) As Object
            Dim arrConst() As Double
            Dim m As Short
            Dim al, ar As Object
            Dim j As Short

            Dim k As Integer 'jrs 2007/12/12 year index for reference operation
            Dim finCalc() As Object
            If (find_first_of((n.sData), validoperators, 1) = -1) Then
                If IsNumeric(n.sData) Then
                    ReDim arrConst(totYears)
                    For m = 1 To totYears
                        arrConst(m) = Val(n.sData)
                    Next m
                    RetrArr = VB6.CopyArray(arrConst)
                ElseIf UCase(n.sData) = "YEAR" Then 'jrs 2014/01/17
                    For m = 1 To totYears
                        arrConst(m) = FirstYear + m - 1
                    Next m
                    RetrArr = VB6.CopyArray(arrConst)
                Else
                    RetrArr = NumVars(n.pPlot)
                End If
            Else
                al = RetrArr((n.nLeft))
                'jrs 2008/05/22
                Dim bRight As Boolean = False
                If (find_first_of((n.sData), monoper, 1) = -1) Then
                    ar = RetrArr((n.nRight))
                    bRight = True
                End If
                ReDim finCalc(totYears)
                'jrs 2008/05/23
                Dim alj, arj As Object 'changing the array directly changes the original data in .net
                For j = 1 To totYears
                    'jrs 2008/05/22 replace null with 0
                    alj = al(j)
                    If bRight Then
                        arj = ar(j)
                    End If
                    If bUseNullsAsZeros Then
                        'jrs 2011/11/15 adding condition:
                        'We treat nulls as zeros ONLY when doing addition or subtraction between a null and a NON-null value.
                        If n.sData = "+" Or n.sData = "-" Then
                            If (Not IsDBNull(alj)) Or (Not IsDBNull(arj)) Then
                                If IsDBNull(alj) Or IsNothing(alj) Then alj = 0 'jrs 2019/09/16 added or IsNothing
                                If bRight Then
                                    If IsDBNull(arj) Or IsNothing(arj) Then arj = 0 'jrs 2019/09/16 added or IsNothing
                                End If
                            End If
                        End If

                    End If

                    'jrs 2014/01/06
                    If n.sData = "#" Then
                        If Not IsNumeric(alj) Then
                            alj = 1
                        End If
                    End If


                    'jrs 2014/01/17 added or N.sData = "?" to evaluate the reference operation, even if it's null in this year
                    If (IsNumeric(alj)) Or n.sData = "?" Then
                        Select Case n.sData
                            'jrs 2007/06/11
                            Case "%"
                                If IsNumeric(arj) Then
                                    If arj <> 0 Then
                                        finCalc(j) = alj / arj * 100
                                    ElseIf Not (IsNothing(arj)) Then
                                        'jrs 2008/12/23
                                        'j = totYears
                                        'Err.Raise(vbObjectError + 1051, Nothing, "There is a division by zero in this operation.")
                                    End If
                                End If
                            Case "/"
                                'jrs 2005/08/22 when it is Null it was blowing up
                                If IsNumeric(arj) Then
                                    If arj <> 0 Then
                                        finCalc(j) = alj / arj
                                    ElseIf Not (IsNothing(arj)) Then
                                        'jrs 2008/12/23
                                        'j = totYears
                                        ''jrs 2005/11/14
                                        ''Dim frm_msg As New frm_Message
                                        ''frm_msg.LoadMessage("There is a division by zero in this operation")
                                        'Err.Raise(vbObjectError + 1051, Nothing, "There is a division by zero in this operation.")
                                    End If
                                End If
                            Case "*"
                                If IsNumeric(arj) Then
                                    finCalc(j) = alj * arj
                                End If
                            Case "+"
                                If IsNumeric(arj) Then
                                    finCalc(j) = Val(alj) + Val(arj)
                                    'jrs 2008/05/23
                                    'Else 'jrs 2008/05/01
                                    '    finCalc(j) = alj
                                End If
                            Case "-"
                                If IsNumeric(arj) Then
                                    finCalc(j) = alj - arj
                                    'jrs 2008/05/24
                                    'Else 'jrs 2008/05/01
                                    '    finCalc(j) = alj
                                End If
                            Case "^"
                                If IsNumeric(arj) Then
                                    finCalc(j) = alj ^ arj
                                End If
                            Case "!"
                                finCalc(j) = -1 * alj
                            Case "&"
                                If alj > 0 Then
                                    finCalc(j) = System.Math.Log(alj)
                                    'jrs 2007/06/11 change ar to al
                                ElseIf Not (IsNothing(alj)) Then
                                    'jrs 2008/12/23
                                    'j = totYears
                                    ''jrs 2005/11/14
                                    'Err.Raise(vbObjectError + 1051, Nothing, "There is an invalid log operation.")
                                End If
                            Case "?" 'jrs 2007/12/12 added reference operation
                                'find reference index
                                k = arj - iFirstYear + 1
                                If k < 1 Then k = 1
                                If k > totYears Then k = totYears
                                'check when:
                                'using history + forecast
                                'diff formula for hist and forecast
                                'historic analog formula
                                finCalc(j) = al(k)
                            Case "#" 'jrs 2014/01/06
                                finCalc(j) = alj
                        End Select
                        'jrs 2008/05/23
                        '    'jrs 2008/05/01
                        'Else 'if left is null but right has data + and - should work
                        '    Select Case n.sData
                        '        Case "+"
                        '            If IsNumeric(arj) Then
                        '                finCalc(j) = arj
                        '            End If
                        '        Case "-"
                        '            If IsNumeric(arj) Then
                        '                finCalc(j) = -1 * arj
                        '            End If
                        '    End Select
                    End If
                Next j
                RetrArr = VB6.CopyArray(finCalc)
            End If
        End Function

        Public Function disRoot() As Node
            disRoot = root
        End Function


        Public Property itotYears() As Short
            Get
                itotYears = totYears
            End Get
            Set(ByVal Value As Short)
                totYears = Value
            End Set
        End Property


        Public Property begAll() As Short
            Get
                'UPGRADE_WARNING: Couldn't resolve default property of object ibegAll. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                begAll = ibegAll
            End Get
            Set(ByVal Value As Short)
                'UPGRADE_WARNING: Couldn't resolve default property of object ibegAll. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                ibegAll = Value
            End Set
        End Property


        Public Property endAll() As Short
            Get
                endAll = iendAll
            End Get
            Set(ByVal Value As Short)
                iendAll = Value
                'jrs 2015/01/20
                If iendAll > iMaxendAll Then
                    iMaxendAll = iendAll
                End If
            End Set
        End Property

        'jrs 2015/01/21
        Public Property MaxendAll() As Short
            Get
                MaxendAll = iMaxendAll
            End Get
            Set(ByVal Value As Short)
                iMaxendAll = Value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return sName
            End Get
            Set(ByVal Value As String)
                sName = Value
            End Set
        End Property


        Public Property Dimension() As String
            Get
                Dimension = sDimension
            End Get
            Set(ByVal Value As String)
                sDimension = Value
            End Set
        End Property

        Public Property Dimension2() As String
            Get
                Dimension2 = sDimension2
            End Get
            Set(ByVal Value As String)
                sDimension2 = Value
            End Set
        End Property

        Public Property Dimension3() As String
            Get
                Dimension3 = sDimension3
            End Get
            Set(ByVal Value As String)
                sDimension3 = Value
            End Set
        End Property

        Public Property DimensionSize() As Short
            Get
                DimensionSize = iDimensionSize
            End Get
            Set(ByVal Value As Short)
                iDimensionSize = Value
            End Set
        End Property


        Public Property Unit() As String
            Get
                Unit = sUnit
            End Get
            Set(ByVal Value As String)
                sUnit = Value
            End Set
        End Property

        Public ReadOnly Property Formula_Renamed() As String
            Get
                'JRS 01/29/04
                Formula_Renamed = sFormula
            End Get
        End Property

        'jrs 2005/10/06
        Public Property Formula2() As String
            Get
                Formula2 = sFormula2
            End Get
            Set(ByVal Value As String)
                sFormula2 = Value
            End Set
        End Property

        Private Sub Checking()
            'jrs 2007/12/11
            '**************** remove blanks ************************
            sFormula = Replace(sFormula, " ", "")

            'jrs 2014/02/26
            'Consider exponential power exp(1) = 2.71828182846
            sFormula = Replace(UCase(sFormula), "E^", "2.7182^")

            '**************** replace scientific format with regular format 2E-1 = 0.2
            Dim Es As Object, sExp As String, sBase As String, sNewBase As String, sSign As String
            Dim k As Object
            Dim i As Short
            Es = Split(sFormula, "E")
            For k = 0 To UBound(Es) - 1
                'if last character is numeric then is scientific format
                'jrs 2016/10/03 added And (IsNumeric(left(Es(k + 1), 1)) Or left(Es(k + 1), 1) = "-")
                If IsNumeric(Right(Es(k), 1)) And (IsNumeric(Left(Es(k + 1), 1)) Or Left(Es(k + 1), 1) = "-") Then
                    'Need to find base number
                    sBase = ""
                    For i = Len(Es(k)) To i = 1 Step -1
                        If Not IsNumeric(Mid(Es(k), i, 1)) And Mid(Es(k), i, 1) <> "." Then
                            Exit For
                        End If
                        sBase = Mid(Es(k), i, 1) & sBase
                    Next i
                    'Need to find exponent
                    sExp = ""
                    For i = 1 To Len(Es(k + 1))
                        If Not IsNumeric(Mid(Es(k + 1), i, 1)) And Mid(Es(k + 1), i, 1) <> "-" Then
                            Exit For
                        End If
                        sExp = sExp & Mid(Es(k + 1), i, 1)
                    Next i
                    'Build string of zeros based on sExp
                    sSign = Left(sExp, 1)
                    If sSign = "-" Then
                        'Add zeros to the left
                        sNewBase = "0."
                        For i = 1 To Math.Abs(Val(sExp)) - 1
                            sNewBase = sNewBase & "0"
                        Next
                        sNewBase = sNewBase & Replace(sBase, ".", "")
                    Else
                        'Add zeros to the right
                        sNewBase = Mid(sBase, 1, 1)
                        For i = 1 To Val(sExp)
                            If i + 2 <= Len(sBase) Then
                                sNewBase = sNewBase & Mid(sBase, i + 2, 1)
                            Else
                                sNewBase = sNewBase & "0"
                            End If
                        Next i
                        If i + 2 <= Len(sBase) Then
                            sNewBase = sNewBase & "." & Mid(sBase, i + 2)
                        End If
                    End If

                    sFormula = Replace(UCase(sFormula), sBase & "E" & sExp, sNewBase)
                End If
            Next k

            '**************** mono operand "-" **********************
            ' Looks for a "-" with a previous operator to turn it into "!"
            For k = 1 To Len(sFormula)
                i = find_first_of(sFormula, "-", k)

                If (i >= 1 And i <= Len(sFormula)) Then
                    If (i = 1) Then
                        sFormula = Left(sFormula, i - 1) & "!" & Right(sFormula, Len(sFormula) - i)
                    ElseIf (find_first_of(Mid(sFormula, i - 1, 1), "+-*/^(", 1) = 1) Then
                        sFormula = Left(sFormula, i - 1) & "!" & Right(sFormula, Len(sFormula) - i)
                    End If
                End If
                If (i >= 1) Then
                    k = i
                Else
                    k = Len(sFormula)
                End If
            Next k

            '**************** look for LOG **********************
            ' Looks for a "LOG" and change it into &
            sFormula = Replace(UCase(sFormula), "LN", "&")
            '**************** look for Log **********************
            ' Looks for a "LOG base 10" and change it into &
            sFormula = Replace(UCase(sFormula), "LOG", "1/&(10)*&")
            '**************** look for reference operation ******
            Dim iRef As Integer, iOp As Integer
            iRef = InStr(1, UCase(sFormula), "REF")
            Do While iRef > 0
                iOp = InStr(iRef, UCase(sFormula), ",")
                sFormula = Replace(UCase(sFormula), "REF", "", 1, 1)
                sFormula = Replace(UCase(sFormula), ",", "?", 1, 1)
                iRef = InStr(1, UCase(sFormula), "REF")
            Loop

            '**************** implicit * with parenthesis **********************
            ' Looks for a "(" with a previous operand to insert a *
            For k = 1 To Len(sFormula)
                i = find_first_of(sFormula, "(", k)

                If (i > 1 And i <= Len(sFormula)) Then
                    If (find_first_of(Mid(sFormula, i - 1, 1), "+-*/%^(?" & monoper, 1) <> 1) Then
                        sFormula = Left(sFormula, i - 1) & "*" & Right(sFormula, Len(sFormula) - i + 1)
                    End If
                End If
                If (i > 1) Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    k = i
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    k = Len(sFormula)
                End If
            Next k
            ' Looks for a ")" with a following operand to insert a *
            For k = 1 To Len(sFormula)
                'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                i = find_first_of(sFormula, ")", k)

                If (i > 1 And i < Len(sFormula)) Then
                    If (find_first_of(Mid(sFormula, i + 1, 1), "+-*/%^)?" & monoper, 1) <> 1) Then
                        sFormula = Left(sFormula, i) & "*" & Right(sFormula, Len(sFormula) - i)
                    End If
                End If
                If (i > 1) Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    k = i
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object k. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    k = Len(sFormula)
                End If
            Next k
            'jrs 2006/01/19
            'remove blanks
            'sFormula = Replace(sFormula, " ", "")



        End Sub

        'jrs 2005/12/19 RAM for Formulas
        Public Sub InitializeVarsHis(ByRef V As Vector)
            VariablesHis = V
        End Sub

        'jrs 2008/01/16
        Public Property IndexVarListName() As String
            Set(ByVal X As String)
                sIndexVarListName = X
            End Set

            Get
                IndexVarListName = sIndexVarListName
            End Get
        End Property

        'jrs 2008/01/29
        Public Property FirstYear() As Integer
            Set(ByVal X As Integer)
                iFirstYear = X
            End Set

            Get
                FirstYear = iFirstYear
            End Get
        End Property

        'jrs 2008/03/06
        Public Property Definition() As String
            Set(ByVal X As String)
                sDefinition = X
            End Set

            Get
                Definition = sDefinition
            End Get
        End Property

        'jrs 2008/05/23
        Public Property UseNullsAsZeros() As Boolean
            Set(ByVal X As Boolean)
                bUseNullsAsZeros = X
            End Set

            Get
                UseNullsAsZeros = bUseNullsAsZeros
            End Get
        End Property

    End Class

