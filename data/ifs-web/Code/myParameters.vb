'jrs 2006/09/25
'This class was created to pass a set of parameters
'some subroutines (ex. gLoadComp) had too many parameters
'for some reason when using more than 32 parameters, 
'we loose the ability to display a variable value when debugin step by step
'the intention is to pass only one object containing all of the required parameters.
'Namespace IFsWeb

Public Class myParameters
        Public currencyVal As Single
        Public vShortSce As Object
        Public RunFiles As Object
        Public Ifs_MDB2Active As String
        Public IFsCoVaTrASeries_MDB As String
        Public IFsHistSeries_MDB As String
        Public PutFdrvRun As String
        'jrs 2008/05/09
        Public currencySym As String

        'procedure to load parameters
        Public Sub LoadPars(ByVal cv As Single, ByRef vss As Object,
        ByRef rf As Object, ByVal im2a As String, ByVal icvts As String,
        ByVal ihs As String, ByVal pfr As String, ByVal cs As String)
            currencyVal = cv
            If Not (vss Is Nothing) Then
                vShortSce = vss
            End If
            If Not (rf Is Nothing) Then
                RunFiles = rf
            End If
            Ifs_MDB2Active = im2a
            IFsCoVaTrASeries_MDB = icvts
            IFsHistSeries_MDB = ihs
            PutFdrvRun = pfr
            currencySym = cs
        End Sub

    End Class

'End Namespace
