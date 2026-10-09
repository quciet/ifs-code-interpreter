Imports System.Data.SQLite

Partial Class frm_Datadict
    Inherits Page

    Dim ErrorText As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        cmdReset.Attributes.Add("onClick", "return checkToClear();")
        cmdOK.Attributes.Add("onClick", "return checkToAdd();")

        If Not Page.IsPostBack Then
            'Load Variable Category Combo
            CmbVCat.Items.Clear()
            CmbVCat.Items.Add("New (Type in the right box)")

            Dim db As SqliteConnection
            Dim dbCmd As SqliteCommand

            db = New SqliteConnection("DataSource=" + DataFileDir$ & "DataDict.db;")
            db.Open()

            dbCmd = db.CreateCommand
            dbCmd.CommandText = "SELECT Distinct [Group] FROM DataDict"

            Dim dbRdr As SqliteDataReader
            dbRdr = dbCmd.ExecuteReader
            Dim Addname$

            While dbRdr.Read
                Addname$ = dbRdr("Group").ToString.Trim

                If UCase(Addname$) <> "ALL" _
                      And (UCase$(ModelLoad$) = UCase$("LoadCoVaTrA") Or Addname$ <> "Transportation") _
                           Then CmbVCat.Items.Add(Addname$)
            End While

            dbRdr.Close()
            dbCmd.Dispose()
            db.Close()
            db.Dispose()

            CmbVCat.SelectedIndex = 0

            'jrs 2023/08/14
            cmbSeries.Items.Add("Yes")
            cmbSeries.Items.Add("No")
            cmbSeries.Text = "Yes"

            cmbCohort.Items.Add("Yes")
            cmbCohort.Items.Add("No")
            cmbCohort.Text = "No"

            If Session("DataDictRow") <> "" Then
                'load values from database
                LoadDatadictForm(Session("DataDictRow"))
            End If

            If Session("ORVarName") <> "" Then
                txtVariable.Text = Session("ORVarName")
            End If

            If Session("ORVarDefn") <> "" Then
                txtDefinition.Text = Session("ORVarDefn")
            End If


            If Session("ORAggRule") <> "" Then
                txtAggregation.Text = Session("ORAggRule")
            End If

            If Session("ORYearsCovered") <> "" Then
                txtYears.Text = Session("ORYearsCovered")
            End If

            If Session("ORDecPlaces") <> "" Then
                txtDecimal.Text = Session("ORDecPlaces")
            End If

            'If ORCountryList <> "" Then
            '    txtCountryList.Text = ORCountryList
            '    CmbCountryList.ListIndex = 0
            'End If

            'last IFs update
            If Session("ORLastIFsUpDate") = "NEWDATE" Then
                'txtlastIFs.Text = Format(Of Date, "YYYY/mm/dd")() 'Date
                txtlastIFs.Text = Now.Date.Year.ToString & "/" & Now.Date.Month.ToString & "/" & Now.Date.Day.ToString
            End If

            If Session("frm_DatadictParent") = "frm_ViewData" Then
                cmdOK.Visible = True
            Else
                cmdOK.Visible = False
                If Session("frm_DatadictParent") = "frm_ViewDataBatch" Then
                    cmdReset.Text = "Reset Values"
                End If
            End If
        End If
    End Sub

    Private Sub ClearForm()

        '       Fields in DataDict are:
        '           Variable
        '           Table
        '           Group
        '           SubGroup
        '           Series
        '           CoVaTrA
        '           Cohort
        '           Definition
        '           Units
        '           Years
        '           Source
        '           Original Source
        '           Notes
        '           Last IFs Update
        '           Aggregation
        '           Proprietary (Yes/No field type)

        If cmbSeries.SelectedIndex = -1 Then
            'mti update for BBH December (2) 2004
            cmbSeries.SelectedIndex = 1
        End If

        'mti ImportXLSBatch Nov 2012
        'If cmbCovatra.ListIndex = -1 Then
        '    cmbCovatra.ListIndex = 0
        'End If

        If cmbCohort.SelectedIndex = -1 Then
            cmbCohort.SelectedIndex = 0
        End If

        If cmbProprietory.SelectedIndex = -1 Then
            cmbProprietory.SelectedIndex = 0
        End If

        If cmbForecast.SelectedIndex = -1 Then
            cmbForecast.SelectedIndex = 0
        End If


        txtSubGroup.Text = "Unknown"
        txtExtended.Text = "No Extended Source"
        txtUnits.Text = ""



        txtYears.Text = "Unspecified"
        'mti Sep 26, 2012 - end

        txtSource.Text = "Unknown"
        txtOriginal.Text = ""
        txtNotes.Text = ""
        'txtlastIFs.Text = Now ' + " " + Year(Date)
        txtlastIFs.Text = Now.Date.Year.ToString & "/" & Now.Date.Month.ToString & "/" & Now.Date.Day.ToString
        txtNameinSource.Text = "Unspecified"

        'mti ImportXLSBatch Nov 2012
        txtCodeInSource.Text = ""
        txtAggregation.Text = "NONE"
        txtDecimal.Text = "3"
    End Sub

    Private Sub cmdReset_Click(sender As Object, e As EventArgs) Handles cmdReset.Click
        ClearForm()
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Session("Accepted") = False
        'Support.FormManager.Show(Me, "frm_ImportXLSNew.aspx")
        Support.FormManager.Close(Me)
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        '       Fields in DataDict are:
        '           Variable
        '           Table
        '           Group
        '           SubGroup
        '           Series
        '           CoVaTrA
        '           Cohort
        '           Definition
        '           Units
        '           Years
        '           Source
        '           Original Source
        '           Notes
        '           Last IFs Update
        '           Aggregation
        '           Proprietary (Yes/No field type)

        'Check if row already exists
        Dim db As SQLiteConnection
        Dim dbCmd As SQLiteCommand

        'db = New SQLiteConnection("DataSource=" & Session("PutFdrvRun") & Session("txtDatabaseFile") & ";")
        Dim ConStr As String = "DataSource=" & Session("PutFdrvRun") & Session("txtDatabaseFile") & ";"
        db = New SQLiteConnection(ConStr)
        db.Open()

        dbCmd = db.CreateCommand
        dbCmd.CommandText = "SELECT COUNT(*) FROM DataDict WHERE upper([Variable]) = """ & txtVariable.Text.ToUpper & """"

        Dim dbRdr As SqliteDataReader
        dbRdr = dbCmd.ExecuteReader

        dbRdr.Read()

        Dim RowFound As Boolean = False
        If CInt(dbRdr(0)) > 0 Then
            RowFound = True
        End If

        dbRdr.Close()

        Dim sql, sql2 As String
        If RowFound Then
            'Update
            sql = "UPDATE DataDict SET [Table] = ""Series" & txtVariable.Text.Trim
            sql = sql & """, [Definition] = """ & txtDefinition.Text.Trim
            sql = sql & """, [Group] = """ & txtNewCategory.Text.Trim
            If txtSubGroup.Text.Trim <> "" Then
                sql = sql & """, [SubGroup] = """ & txtSubGroup.Text.Trim
            End If
            sql = sql & """, [Aggregation] = """ & txtAggregation.Text.Trim & """"
            'jrs 2023/05/10
            If txtDecimal.Text > "" AndAlso CInt(txtDecimal.Text) > 0 Then
                sql = sql & ", [Decimal Places] = " & CInt(txtDecimal.Text.Trim).ToString
            Else
                sql = sql & ", [Decimal Places] = 0"
            End If
            sql = sql & ", [Code In Source] = """ & txtCodeInSource.Text.Trim
            sql = sql & """, [Formula] = """ & txtFormula.Text.Trim
            sql = sql & """, [CompareOtherForecast] = " & If(cmbForecast.Text.Trim = "YES", "1", "0")
            sql = sql & ", [Series] = """ & cmbSeries.Text.Trim
            sql = sql & """, [CoVatra] = ""No"""
            sql = sql & ", [Cohort] = """ & cmbCohort.Text.Trim
            sql = sql & """, [Source] = """ & txtSource.Text.Trim
            sql = sql & """, [Extended Source Defn] = """ & txtExtended.Text.Trim
            sql = sql & """, [Units] = """ & txtUnits.Text.Trim
            sql = sql & """, [Years] = """ & txtYears.Text.Trim
            sql = sql & """, [Original Source] = """ & txtOriginal.Text.Trim
            sql = sql & """, [Notes] = """ & txtNotes.Text.Trim
            sql = sql & """, [Last IFs Update] = """ & txtlastIFs.Text.Trim
            'jrs 2023/05/19
            If cmbProprietory.Text.Trim = "1" Or cmbProprietory.Text.Trim.ToUpper = "YES" Or cmbProprietory.Text.Trim.ToUpper = "TRUE" Then
                sql = sql & """, [Proprietary] = ""1"
            Else
                sql = sql & """, [Proprietary] = ""0"
            End If
            If txtNameinSource.Text.Trim <> "" Then
                sql = sql & """, [Name in Source] = """ & txtNameinSource.Text.Trim
            End If
            sql = sql & """, [Country Concordance] = ""Long"""
            sql = sql & " WHERE upper([Variable]) = """ & txtVariable.Text.ToUpper.Trim & """"
        Else
            'Insert
            sql = "INSERT INTO DataDict([Variable], [Table] "
            sql2 = " VALUES(""" & txtVariable.Text.Trim & """, ""Series" & txtVariable.Text.Trim
            sql = sql & ", [Definition]"
            sql2 = sql2 & """,""" & txtDefinition.Text.Trim
            sql = sql & ", [Group]"
            sql2 = sql2 & """, """ & txtNewCategory.Text.Trim
            If txtSubGroup.Text.Trim <> "" Then
                sql = sql & ", [SubGroup]"
                sql2 = sql2 & """, """ & txtSubGroup.Text.Trim
            End If
            sql = sql & ", [Aggregation]"
            sql2 = sql2 & """, """ & txtAggregation.Text.Trim & """"
            sql = sql & ", [Decimal Places]"
            If txtDecimal.Text > "" Then
                Dim p As Integer
                If Integer.TryParse(txtDecimal.Text, p) Then
                    If CInt(txtDecimal.Text) > 0 Then
                        sql2 = sql2 & ", " & CInt(txtDecimal.Text.Trim).ToString
                    Else
                        sql2 = sql2 & ", 0"
                    End If
                Else
                    sql2 = sql2 & ", 0"
                End If
            Else
                sql2 = sql2 & ", 0"
            End If
            sql = sql & ", [Code In Source]"
            sql2 = sql2 & ", """ & txtCodeInSource.Text.Trim
            sql = sql & ", [Formula]"
            sql2 = sql2 & """, """ & txtFormula.Text.Trim
            sql = sql & ", [CompareOtherForecast]"
            sql2 = sql2 & """, " & If(cmbForecast.Text.Trim = "YES", "1", "0")
            sql = sql & ", [Series]"
            sql2 = sql2 & ", """ & cmbSeries.Text.Trim
            sql = sql & ", [CoVatra]"
            sql2 = sql2 & """, ""No"
            sql = sql & ", [Cohort]"
            sql2 = sql2 & """, """ & cmbCohort.Text.Trim
            sql = sql & ", [Source]"
            sql2 = sql2 & """, """ & txtSource.Text.Trim
            sql = sql & ", [Extended Source Defn]"
            sql2 = sql2 & """, """ & txtExtended.Text.Trim
            sql = sql & ", [Units]"
            sql2 = sql2 & """, """ & txtUnits.Text.Trim
            sql = sql & ", [Years]"
            sql2 = sql2 & """, """ & txtYears.Text.Trim
            sql = sql & ", [Original Source]"
            sql2 = sql2 & """, """ & txtOriginal.Text.Trim
            sql = sql & ", [Notes]"
            sql2 = sql2 & """, """ & txtNotes.Text.Trim
            sql = sql & ", [Last IFs Update]"
            sql2 = sql2 & """, """ & txtlastIFs.Text.Trim
            sql = sql & ", [Proprietary]"
            'jrs 2023/05/19
            If cmbProprietory.Text.Trim = "1" Or cmbProprietory.Text.Trim.ToUpper = "YES" Or cmbProprietory.Text.Trim.ToUpper = "TRUE" Then
                sql2 = sql2 & """, ""1"
            Else
                sql2 = sql2 & """, ""0"
            End If
            If txtNameinSource.Text.Trim <> "" Then
                sql = sql & ", [Name in Source]"
                sql2 = sql2 & """, """ & txtNameinSource.Text.Trim
            End If
            'jrs 2023/08/14 added TreatNullsAs0s, UsedInPreprocessor
            sql = sql & ", [Country Concordance], TreatNullsAs0s, UsedInPreprocessor)"
            sql2 = sql2 & """, ""Long"", 0, 0)"
            sql = sql & sql2
        End If

        dbCmd.CommandText = sql
        dbCmd.ExecuteNonQuery()

        db.Close()
        db.Dispose()

        Session("Accepted") = True
        'jrs 2026/07/23
        'Support.FormManager.Show(Me, "frm_ImportXLSNew.aspx")
        Support.FormManager.Show(Me, Session("ImportParent"))
    End Sub

    Private Sub LoadDatadictForm(TableName$)
        Dim db As SqliteConnection
        Dim dbCmd As SQLiteCommand

        Dim ConStr As String = "DataSource=" + Session("ViewDataDictMDB") + ";"
        db = New SQLiteConnection(ConStr)
        db.Open()

        dbCmd = db.CreateCommand
        dbCmd.CommandText = "Select * from Datadict where Variable='" + TableName$ + "'"

        Dim DicTbl As SqliteDataReader
        DicTbl = dbCmd.ExecuteReader

        While DicTbl.Read()
            txtVariable.Text = DicTbl("Variable") '= Trim(.TxtVarName.Text)
            txtDefinition.Text = DicTbl("Definition")

            'DicTbl("Table") = Trim(.txtTable.Text)

            '            If UCase(mid(CmbVCat.Text, 1, 3)) = "NEW" Then
            '                If Trim(txtNewCategory.Text) = "" Then txtNewCategory.Text = "Unspecified"
            CmbVCat.Text = Trim(" " & DicTbl("Group"))
            'jrs 2023/04/28
            CmbVCat_SelectedIndexChanged(Me, New EventArgs)
            '            Else
            '                DicTbl("Group") = Trim(CmbVCat.Text)
            '            End If

            txtAggregation.Text = Trim(" " & DicTbl("Aggregation"))

            txtDecimal.Text = Trim(" " & DicTbl("Decimal Places"))

            'mti 08/18/2017 start
            txtSubGroup.Text = Trim("" & DicTbl("SubGroup"))
            'jrs 2023/08/14
            cmbSeries.Text = Trim("" & DicTbl("Series"))



            'cmbCovatra.Text = Trim(DicTbl("CoVatra"))

            'jrs 2023/08/14
            cmbCohort.Text = Trim("" & DicTbl("Cohort"))
            'mti 08/18/2017 End

            'DicTbl("Definition") = Trim(.TxtVarDesc.Text)

            txtExtended.Text = Trim(" " & DicTbl("Extended Source Defn"))
            txtUnits.Text = Trim(" " & DicTbl("Units"))

            'If ORProvinces = "NO" Then
            txtSource.Text = Trim(" " & DicTbl("Source"))
            txtOriginal.Text = Trim(" " & DicTbl("Original Source"))
            txtNotes.Text = Trim(" " & DicTbl("Notes"))
            'Else
            '    txtSource.Text = ""
            '    txtOriginal.Text = ""
            '    txtNotes.Text = ""
            'End If





            'If UpdateYears = "" Then
            txtlastIFs.Text = Trim(" " & DicTbl("Last IFs Update"))
            txtYears.Text = Trim(" " & DicTbl("Years"))
            '            Else
            '                txtlastIFs.Text = Format(Date, "YYYY/mm/dd")
            '                txtYears.Text = UpdateYears
            '            End If

            'DicTbl("Aggregation") = Trim(.cmbaggregation.Text)

            If DicTbl("Proprietary") = "1" Then
                cmbProprietory.Text = "YES" '= CBool(UCase(cmbProprietory.Text) = "YES")
            Else
                cmbProprietory.Text = "NO"
            End If
            txtNameinSource.Text = Trim(" " & DicTbl("Name in Source"))
            'jrs 2023/10/31
            txtNameinSource.Text = txtNameinSource.Text.Replace("""", """""")

            'If Trim(" " & DicTbl("Country Concordance")) <> "" Then
            '    txtCountryList.Text = Trim(" " & DicTbl("Country Concordance"))
            '    CmbCountryList.ListIndex = 0
            'End If

            txtCodeInSource.Text = Trim(" " & DicTbl("Code in Source"))

            txtFormula.Text = Trim(" " & DicTbl("Formula"))
        End While

        DicTbl.Close()

        db.Close()
        db.Dispose()
    End Sub

    Private Sub CmbVCat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbVCat.SelectedIndexChanged
        If UCase(Mid(CmbVCat.SelectedValue, 1, 3)) = "NEW" Then
            txtNewCategory.Text = ""
        Else
            txtNewCategory.Text = CmbVCat.SelectedValue
        End If
    End Sub

    Protected Sub cmdViewDataDict_Click(sender As Object, e As EventArgs) Handles cmdViewDataDict.Click
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ViewDataDict.aspx")
    End Sub
End Class
