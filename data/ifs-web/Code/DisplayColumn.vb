Imports Microsoft.VisualBasic

Public Class DisplayColumn
    'jrs 2008/03/21
    'created to sort columns in the plot vector, ordered by a given year

    Public Index As Integer
    Public Value As Object
    'jrs 2010/02/03
    Public Value2 As String 'used to order columns in case there's a tie in value1
    'jrs 2010/07/13
    Public Index2 As Integer 'used to handle the original ranking with no ties, while Index has the same value for all tied values

End Class
