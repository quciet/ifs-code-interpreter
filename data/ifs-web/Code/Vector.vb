Option Strict Off
Option Explicit On


'Namespace IFsWeb

'UPGRADE_WARNING: Class instancing was changed to public. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1043"'
'JRSVBNET <System.Runtime.InteropServices.ProgId("Vector_NET.Vector")> 
Public Class Vector
	Dim size As Short
	Dim data() As String
    'jrs 2006/01/20 collection didn't work because we might need repeated values
    'at least temporaly ex. a, ab, abc, or 1, 10, 100 or 100 twice in formula
    ''jrs 2005/12/14
    'Private dataDet As New Collection
    Private dataDet() As String
    'jrs 2006/05/23 keep dimensions apart from variables
    Private dataDet1() As String
    Private dataDet2() As String
    Private dataDet3() As String


	Public Sub push_back(ByRef x As String)
		size = size + 1
		ReDim Preserve data(size)
		data(size) = x
        'jrs 2006/01/20
        '    'jrs 2005/12/18
        'On Error Resume Next
        '    'jrs 2005/12/14
        '    dataDet.Add ("", x)
        ReDim Preserve dataDet(size)
        'jrs 2006/05/23 keep dimensions apart from variables
        ReDim Preserve dataDet1(size)
        ReDim Preserve dataDet2(size)
        ReDim Preserve dataDet3(size)
    End Sub

    Public Sub pop_back()
        'jrs 2006/01/20
        ''jrs 2005/12/19
        'dataDet.Remove(data(size))

        size = size - 1
        ReDim Preserve data(size)
        ReDim Preserve dataDet(size)
        'jrs 2006/05/23 keep dimensions apart from variables
        ReDim Preserve dataDet1(size)
        ReDim Preserve dataDet2(size)
        ReDim Preserve dataDet3(size)
    End Sub

    Public Sub append(ByRef x As String, ByRef i As Short)
        'jrs 2006/01/20
        ''jrs 2005/12/19
        'dataDet.Remove(data(i))

        data(i) = data(i) & x

        'jrs 2006/01/20
        ''jrs 2005/12/19
        'dataDet.Add("", data(i))
    End Sub

    Public Function disSize() As Short
        disSize = size
    End Function

    Public Function display(ByVal i As Short) As String
        display = data(i)
    End Function

    'jrs 2005/12/19
    Public Function displayDet(ByVal i As Integer) As String
        'jrs 2006/01/20 displayDet = dataDet.Item(data(i))
        displayDet = dataDet(i)
    End Function

    Public Function displayDet1(ByVal i As Integer) As String
        'jrs 2006/01/20 displayDet = dataDet.Item(data(i))
        displayDet1 = dataDet1(i)
    End Function

    Public Function displayDet2(ByVal i As Integer) As String
        'jrs 2006/01/20 displayDet = dataDet.Item(data(i))
        displayDet2 = dataDet2(i)
    End Function

    Public Function displayDet3(ByVal i As Integer) As String
        'jrs 2006/01/20 displayDet = dataDet.Item(data(i))
        displayDet3 = dataDet3(i)
    End Function

    'jrs 2005/12/19
    Public Function displayDetS(ByVal S As String) As String
        Dim i As Integer
        For i = 1 To size
            If S = data(i) Then
                Exit For
            End If
        Next i
        If i <= size Then
            displayDetS = dataDet(i)
        End If
        'jrs 2006/01/20 displayDetS = dataDet.Item(S)
    End Function

    Public Function displayDet1S(ByVal S As String) As String
        Dim i As Integer
        For i = 1 To size
            If S = data(i) Then
                Exit For
            End If
        Next i
        If i <= size Then
            displayDet1S = dataDet1(i)
        End If
        'jrs 2006/01/20 displayDetS = dataDet.Item(S)
    End Function

    Public Function displayDet2S(ByVal S As String) As String
        Dim i As Integer
        For i = 1 To size
            If S = data(i) Then
                Exit For
            End If
        Next i
        If i <= size Then
            displayDet2S = dataDet2(i)
        End If
        'jrs 2006/01/20 displayDetS = dataDet.Item(S)
    End Function

    Public Function displayDet3S(ByVal S As String) As String
        Dim i As Integer
        For i = 1 To size
            If S = data(i) Then
                Exit For
            End If
        Next i
        If i <= size Then
            displayDet3S = dataDet3(i)
        End If
        'jrs 2006/01/20 displayDetS = dataDet.Item(S)
    End Function

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    Private Sub Class_Initialize_Renamed()
        size = 0
    End Sub

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    Public Sub sort(ByRef fst As Integer, ByRef lst As Integer)
        QuickSort(data, fst, lst) ' Sort the data
        'jrs 2006/01/20 remember that when sorting the pointers between data and datadet get messed up,
        'better sort before load datadet.
    End Sub

    Public Sub unique(ByRef fst As Short, ByRef lst As Short)
        Dim i, j As Object
        Dim k As Short
        k = 0
        For i = fst + 1 To lst
            If data(i - 1) = data(i) Then
                k = k + 1
                If i < size Then
                    If data(i) <> data(i + 1) Then
                        'jrs 2008/01/02 For j = i - k + 1 To size - 1
                        For j = i - k + 1 To size - k
                            data(j) = data(j + k)
                        Next j
                        size = size - k
                        'jrs 2025/08/26 This is not working for formulas with more than one repetition
                        'for example: 2.71828^(-0.6128+(1.8340*a)+(2.2198*b))
                        '/(2.71828^(-0.6128+(1.8340*a)+(2.2198*b))
                        '+2.71828^(-0.9064-(1.4878*a)-(2.5714*b)))
                        i = i - k

                        k = 0
                        'jrs 2006/01/20
                        If i >= size Then 'jrs 2007/12/13 added >
                            Exit For
                        End If
                    End If
                Else
                    For j = i - k + 1 To size - 1
                        data(j) = data(j + k)
                    Next j
                    size = size - k
                End If
            End If
            'jrs 2007/11/07
            If i >= size Then 'jrs 2007/12/13 added >
                Exit For
            End If
        Next i
        ReDim Preserve data(size)
        'jrs 2006/01/20
        ReDim Preserve dataDet(size)
        'jrs 2006/05/23 keep dimensions apart from variables
        ReDim Preserve dataDet1(size)
        ReDim Preserve dataDet2(size)
        ReDim Preserve dataDet3(size)
    End Sub

    Public Sub remove(ByRef idx As Short)
        Dim i As Short
        If idx <= size Then
            'jrs 2006/01/20
            ''jrs 2005/12/19
            'dataDet.Remove(data(idx))

            For i = idx To size - 1
                data(i) = data(i + 1)
                dataDet(i) = dataDet(i + 1)
                'jrs 2006/05/23 keep dimensions apart from variables
                dataDet1(i) = dataDet1(i + 1)
                dataDet2(i) = dataDet2(i + 1)
                dataDet3(i) = dataDet3(i + 1)
            Next i
            If size > 0 Then
                size = size - 1
                ReDim Preserve data(size)
                ReDim Preserve dataDet(size)
                'jrs 2006/05/23 keep dimensions apart from variables
                ReDim Preserve dataDet1(size)
                ReDim Preserve dataDet2(size)
                ReDim Preserve dataDet3(size)
            End If
        End If
    End Sub

    'jrs 2005/12/19
    Public Sub setDataDetS(ByVal sIdx As String, ByVal S As String)
        'jrs 2006/01/20
        'dataDet.Remove(sIdx)
        'dataDet.Add(S, sIdx)
        Dim i As Integer
        For i = 1 To size
                If UCase(sIdx) = UCase(data(i)) Then 'jrs 2008/01/04 added ucase
                    Exit For
                End If
        Next i
        If i <= size Then
            dataDet(i) = S
        End If
    End Sub

    Public Sub setDataDet1S(ByVal sIdx As String, ByVal S As String)
        'jrs 2006/01/20
        'dataDet.Remove(sIdx)
        'dataDet.Add(S, sIdx)
        Dim i As Integer
        For i = 1 To size
                If UCase(sIdx) = UCase(data(i)) Then 'jrs 2008/01/04 added ucase
                    Exit For
                End If
        Next i
        If i <= size Then
            dataDet1(i) = S
        End If
    End Sub

    Public Sub setDataDet2S(ByVal sIdx As String, ByVal S As String)
        'jrs 2006/01/20
        'dataDet.Remove(sIdx)
        'dataDet.Add(S, sIdx)
        Dim i As Integer
        For i = 1 To size
                If UCase(sIdx) = UCase(data(i)) Then 'jrs 2008/01/04 added ucase
                    Exit For
                End If
        Next i
        If i <= size Then
            dataDet2(i) = S
        End If
    End Sub

    Public Sub setDataDet3S(ByVal sIdx As String, ByVal S As String)
        'jrs 2006/01/20
        'dataDet.Remove(sIdx)
        'dataDet.Add(S, sIdx)
        Dim i As Integer
        For i = 1 To size
                If UCase(sIdx) = UCase(data(i)) Then 'jrs 2008/01/04 added ucase
                    Exit For
                End If
        Next i
        If i <= size Then
            dataDet3(i) = S
        End If
    End Sub

    Public Sub setDataDet(ByVal idx As Integer, ByVal S As String)
        'jrs 2006/01/20
        'dataDet.Remove(data(idx))
        'dataDet.Add(S, data(idx))
        dataDet(idx) = S
    End Sub

    Public Sub setDataDet1(ByVal idx As Integer, ByVal S As String)
        dataDet1(idx) = S
    End Sub

    Public Sub setDataDet2(ByVal idx As Integer, ByVal S As String)
        dataDet2(idx) = S
    End Sub

    Public Sub setDataDet3(ByVal idx As Integer, ByVal S As String)
        dataDet3(idx) = S
    End Sub

    Public Function getDataDetS(ByVal sIdx As String) As String
        'jrs 2006/01/20 getDataDetS = dataDet.Item(sIdx)
        Dim i As Integer
        For i = 1 To size
                If UCase(sIdx) = UCase(data(i)) Then 'jrs 2008/01/04 added ucase
                    Exit For
                End If
        Next i
        If i <= size Then
            getDataDetS = dataDet(i)
        End If
    End Function

    Public Function getDataDet1S(ByVal sIdx As String) As String
        'jrs 2006/01/20 getDataDetS = dataDet.Item(sIdx)
        Dim i As Integer
        For i = 1 To size
                If UCase(sIdx) = UCase(data(i)) Then 'jrs 2008/01/04 added ucase
                    Exit For
                End If
        Next i
        If i <= size Then
            getDataDet1S = dataDet1(i)
        End If
    End Function

    Public Function getDataDet2S(ByVal sIdx As String) As String
        'jrs 2006/01/20 getDataDetS = dataDet.Item(sIdx)
        Dim i As Integer
        For i = 1 To size
                If UCase(sIdx) = UCase(data(i)) Then 'jrs 2008/01/04 added ucase
                    Exit For
                End If
        Next i
        If i <= size Then
            getDataDet2S = dataDet2(i)
        End If
    End Function

    Public Function getDataDet3S(ByVal sIdx As String) As String
        'jrs 2006/01/20 getDataDetS = dataDet.Item(sIdx)
        Dim i As Integer
        For i = 1 To size
                If UCase(sIdx) = UCase(data(i)) Then 'jrs 2008/01/04 added ucase
                    Exit For
                End If
        Next i
        If i <= size Then
            getDataDet3S = dataDet3(i)
        End If
    End Function

    Public Function getDataDet(ByVal idx As Integer) As String
        'jrs 2006/01/20 getDataDet = dataDet.Item(data(idx))
        getDataDet = dataDet(idx)
    End Function

    Public Function getDataDet1(ByVal idx As Integer) As String
        getDataDet1 = dataDet1(idx)
    End Function

    Public Function getDataDet2(ByVal idx As Integer) As String
        getDataDet2 = dataDet2(idx)
    End Function

    Public Function getDataDet3(ByVal idx As Integer) As String
        getDataDet3 = dataDet3(idx)
    End Function
End Class

'End Namespace
