Option Strict Off
Option Explicit On
Partial Class frm_DataSeries
    Inherits Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim valtbl As DataTable
        Dim fname As String
        Dim ISOcode As String = Request.QueryString.Get("cname")
        If ISOcode = "GL" Then
            ISOcode = "DK"
        ElseIf Len(ISOcode) > 2 Then
            ISOcode = Left(ISOcode, 2)
        End If
        SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT Country FROM [Country Translation] WHERE [ISO3166-2 Codes]=""" & ISOcode & """", "", valtbl)
        fname = valtbl.Rows(0).Item("Country")
        Dim dr As DataRow
        Dim dc As DataColumn
        Dim arl As New ArrayList
        Dim mTable As String = Session("MapChosenTable")
        If mTable = "Map" Then

            SetDataTablefromAccess(Session("DataBaseFile"), "SELECT * FROM ESRICountry WHERE [NAME]='" & Replace(fname, "'", "''") & "'", "", valtbl)
            For Each dr In valtbl.Rows

                lstFeatList.Items.Add("ABBREVNAME = " & dr.Item("ABBREVNAME"))
                lstFeatList.Items.Add("NAME = " & dr.Item("NAME"))
                lstFeatList.Items.Add("CONTINENT = " & dr.Item("CONTINENT"))
                lstFeatList.Items.Add("REGION = " & dr.Item("REGION"))

            Next dr


        Else


            'On Error GoTo AposProb  'was quick fix for apostrophe problem; not needed now
            'srchstring$ = "Country = '" & fname & "'"
            'This can not be used for Korea, Democratic People's Republic of
            SetDataTablefromAccess(Session("DataBaseFile"), "SELECT * FROM [" & mTable & "] WHERE [Country]='" & Replace(fname, "'", "''") & "'", "", valtbl, DataPW)


            For Each dr In valtbl.Rows
                For Each dc In valtbl.Columns
                    arl.Add(dc.ColumnName & "=" & dr.Item(dc.ColumnName))

                Next dc
            Next dr
            arl.Sort()
            lstFeatList.DataSource = arl
            lstFeatList.DataBind()
        End If
AposProb:
        Exit Sub


    End Sub

    'Printer.Print
    '    '    Printer.Print txt_Characteristics.Text
    'Printer.Print
    'For printloop% = 0 To lstFeatList.ListCount - 1
    '    Printer.Print lstFeatList.List(printloop%)
    'Next printloop%
    'Printer.NewPage
    'Printer.EndDoc

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim GraphOutputFileName As String
        GraphOutputFileName = Session("PutFdrvRun") & "Save.csv"

        Dim fNum As Integer
        fNum = FreeFile()

        FileOpen(fNum, GraphOutputFileName, OpenMode.Output)

        Dim i As Integer
        For i = 0 To lstFeatList.Items.Count - 1
            PrintLine(fNum, lstFeatList.Items(i).Text)
        Next

        FileClose(fNum)

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        Response.Flush()
        Response.WriteFile(GraphOutputFileName)
        Response.End()
    End Sub
End Class
