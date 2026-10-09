Imports Microsoft.VisualBasic

Public Class MortType
    Public NumSubTypes As Integer
    Public sTitle As String
    Private SubTypeNames() As String
    Private SubTypeFieldNames() As String
    Private SubTypeColor() As Double
    Private TypeIdx() As Integer 'jrs 2009/06/12 added for Health outcome display


    Public Sub setNumSubTypes(ByVal i As Integer)
        NumSubTypes = i
        ReDim SubTypeNames(NumSubTypes)
        ReDim SubTypeFieldNames(NumSubTypes)
        ReDim SubTypeColor(NumSubTypes)
        ReDim TypeIdx(NumSubTypes)
    End Sub

    Public Sub setSubTypeName(ByVal S As String, ByVal i As Integer)
        SubTypeNames(i) = S
    End Sub

    Public Function getSubTypeName(ByVal i As Integer) As String
        getSubTypeName = SubTypeNames(i)
    End Function

    Public Sub setSubTypeFieldName(ByVal S As String, ByVal i As Integer)
        SubTypeFieldNames(i) = S
    End Sub

    Public Function getSubTypeFieldName(ByVal i As Integer) As String
        getSubTypeFieldName = SubTypeFieldNames(i)
    End Function

    Public Sub setSubTypeColor(ByVal S As Double, ByVal i As Integer)
        SubTypeColor(i) = S
    End Sub

    Public Function getSubTypeColor(ByVal i As Integer) As Double
        getSubTypeColor = SubTypeColor(i)
    End Function

    Public Sub setTypeIdx(ByVal s As Integer, ByVal i As Integer)
        TypeIdx(i) = s
    End Sub

    Public Function getTypeIdx(ByVal i As Integer) As Double
        getTypeIdx = TypeIdx(i)
    End Function


End Class
