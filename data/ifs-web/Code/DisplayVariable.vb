Imports Microsoft.VisualBasic

'Namespace IFsWeb
Public Class DisplayVariable
        'jrs 2010/02/10
        'Created to display variables in a Grid
        'based on Performance and Risk analysis form
        Public text As String
        Public bTitle As Boolean
        Public VarName As String 'Variable Name
        Public VarIdx As Integer 'Index of variable name
        Public Alert As Single
        Public AlertRel As Single 'units in Std dev. terms
        Public Goal As Single
        Public GoalRel As Single 'units in Std Dev. terms
        Public GoalAbs As Single 'limit which will overwright GoalRel, INFMOR<10 always green
        Public FunctionName As String
        Public StdDev As Single
        Public Min As Single
        Public Max As Single
        Public bAsc As Boolean 'true if ascending order is used to sort
        Public bPopPct As Boolean
        Public bPopPort As Boolean
        Public bUseRelEval As Boolean
        Public Description As String
        Public Literature As String
        Public RedCount As Integer
        Public GreenCount As Integer
        'jrs 2010/07/13
        Public Weight As Single
        'jrs 2010/07/21
        Public MeanYr1 As Single
        Public StdDevYr1 As Single
        'jrs 2010/07/23
        Public MinYr1 As Single
        Public MaxYr1 As Single

        Dim VarVals() As DisplayColumn

        Public bFormula As Boolean
        Public f As Formula

        Private Sub Class_Initialize_Renamed()
            ReDim VarVals(NR)
            bUseRelEval = True
        End Sub

        Public Sub VarValsSet(ByVal x As Object, ByVal i As Integer)
            VarVals(i) = x
        End Sub

        Public Function VarValsGet(ByVal i As Integer) As DisplayColumn
            VarValsGet = VarVals(i)
        End Function

        Public Function VarValsGetAll() As DisplayColumn()
            VarValsGetAll = VarVals
        End Function

        Public Sub FormulaSet(ByVal s As String)
            f = New Formula
            f.Init(s)
        End Sub

        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub
    End Class

'End Namespace
