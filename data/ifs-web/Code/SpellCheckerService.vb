Imports NHunspell

Public Class SpellCheckerService
    Private hunspell As Hunspell

    Public Sub New()
        Dim affPath As String = HttpContext.Current.Server.MapPath("~/Dictionary/en_US.aff")
        Dim dicPath As String = HttpContext.Current.Server.MapPath("~/Dictionary/en_US.dic")
        hunspell = New Hunspell(affPath, dicPath)
    End Sub

    Public Function CheckText(ByVal input As String) As List(Of String)
        Dim words As String() = input.Split(" "c)
        Dim misspelled As New List(Of String)

        For Each word In words
            Dim cleaned As String = System.Text.RegularExpressions.Regex.Replace(word, "[^\w\s]", "")

            If Not hunspell.Spell(cleaned) Then
                'Check if not a variable or parameter name
                If IfsVars.Item(cleaned).Index < 1 Then
                    misspelled.Add(cleaned)
                End If
            End If
        Next

        Return misspelled
    End Function

    Public Function SuggestCorrections(ByVal word As String) As List(Of String)
        Return hunspell.Suggest(word)
    End Function
End Class
