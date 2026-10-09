Imports System
Imports Microsoft.Data.Sqlite

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")

        Dim con As SqliteConnection
        con = New SqliteConnection("DataSource=/DUPardee/Model03/Runfiles/TablFunc.db")
        con.Open()

        Dim func_name As String = "GDP/Capita (PPP 2017) Versus Labor Coeff(lab/$mil) (Agriculture)(Unskilled) (2017) (Power)"
        Dim cmd As SqliteCommand = con.CreateCommand

        'cmd.CommandText = "SELECT COUNT(*) FROM TablFunc WHERE [Function Name] = '" & func_name & "'"
        'Dim rd As SqliteDataReader = cmd.ExecuteReader

        'Dim s As String = rd(0).ToString

        cmd.CommandText = "SELECT COUNT(*) As c FROM TablFunc WHERE [Function Name] = $funcname"
        cmd.CommandText = "SELECT RSquared FROM TablFunc WHERE [Function Name] = $funcname"
        cmd.Parameters.Clear()


        cmd.Parameters.Add("$funcname", SqliteType.Text, 90).Value = func_name

        Dim o As Double = CDbl(cmd.ExecuteScalar)
        'Dim rd As SqliteDataReader = cmd.ExecuteReader

        'Dim s As String = rd(0).ToString

    End Sub
End Module
