Option Strict Off
Option Explicit On
Imports System.Data.SQLite
Imports EO.Web.Internal
Imports Microsoft.Ajax.Utilities

'jam 2021/03/31
Imports Microsoft.AspNet.FriendlyUrls

Partial Class frm_PopCohortDisp
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        InitializeComponent()
        'mti WebForm Changes From July 05, 2004
        If Not Page.IsPostBack Then
            'cdv 2021/03/08 Changes to every reference of mnuSQLite to properly reference it as a string instead of a boolean. Also added this init to avoid empty file load calls
            If Session("mnuSQLite") = "True" And Session("db2Path") = "" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.db"
            ElseIf Session("db2Path") = "" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
            End If
            'popConn = CreateConnection(Session("db2Path"))
            'Session("popConn") = popConn
        End If
        'mti WebForm Changes From July 05, 2004
        'jrs 205/02/20
        'WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")

    End Sub

    Protected WithEvents cmd_ScriptFile As System.Web.UI.WebControls.Label
    Protected WithEvents WinFormsToWeb_MenuManager As ArtinSoft.WinFormsToWeb.WebControls.MenuManager
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region

    '------------------------------------------CohortDisp.Frm-----------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last modified by BBH on 8/8/02

    'mti WebForm Changes From June 21, 2004 
    'Dim Session("GraphType") As Short
    'Dim year_run As Short
    'Dim tbl2 As datatable
    'Dim tempyear As Short
    'UPGRADE_WARNING: Lower bound of array TableName was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
    Dim TableName(3) As String
    ''mti Ifs Win April 25, 2004
    'Dim Session("PC_TempDataBase")(, ) As Single
    Dim FieldName(3, 2) As String
    'Dim regionname As String
    'Dim tempregion As Short
    Dim Case_of_Work As Short
    Dim CaseName As String
    Dim valtbl As DataTable
    'JAM 2019-11-17
    Dim vSQLiteCon As SQLiteConnection
    Dim vSQLiteCommand As SQLiteCommand
    Dim vSQLiteReader As SQLiteDataReader
    Dim vConnString As String
    'jam 2024/09/10
    Dim vSQLiteCon2 As SQLiteConnection
    Dim vSQLiteCommand2 As SQLiteCommand
    Dim vSQLiteReader2 As SQLiteDataReader
    Dim vConnString2 As String
    'jam 2026/01/18 Declare Maximum cohort
    Dim MaxCohortGroup As Integer


    'mti WebForm Changes From July 05, 2004
    'Dim popConn As OleDb.OleDbConnection

    'JRS 07/26/02 GROUP
    'Dim CountryGroup(200) As String
    'Dim RegionIdGroup(200) As Short
    'Dim GroupTot As Short
    'Dim sAggregationI As String
    'Dim sAggregationD As String

    '==========================frm_CohoDisp=============================
    Private Sub Graph_Draw(ByRef index As Short)

        On Error GoTo ErrHandler01

        Dim GDone As Boolean
        Dim Rcnt As Integer
        Dim l As Short
        Dim GraphTitle As String
        Dim tbl2 As DataTable
        'jrs 2008/06/24
        Dim tbl3 As DataTable



        '--------------------------------------------------------------
        'start mti june, 2002
        Dim Tindex As Short
        'end mti june, 2002

        ReDim Session("PC_TempDataBase")(NCohorts, 2)

        '   Specify the graph control to be used
        '   ------------------------------------
        'mti Ifs Win April 25, 2004
        '_gphAgedst_1.Visible = False


        'gphAgedst(1).Visible = False
        'gphAgedst(2).Visible = False
        'gphAgedst(3).Visible = False

        ''start mti june, 2002
        'gphAgedst(4).Visible = False
        frameData.Visible = False
        'jrs 2008/06/25
        grdcohort.Visible = False
        lbldata.Visible = False

        lblJS.Visible = True

        Select Case index
            Case 1
                Tindex = 1
            Case 2
                Tindex = 2
            Case 3
                Tindex = 3
            Case 4
                Tindex = 1
        End Select

        '   Change the data source
        '   ----------------------
        'JRS 07/26/02 GROUP
        Dim rId As Short
        Dim i As Integer
        i = 0
        ''mti CohoDisp May 2004 - check this
        'If mnuGroups.Text = "Use Groups" Then  '"Use &Groups" Then 
        'mti September 2004 1
        'If wmMain.Groups("Set Group or Country").Items("Countries/Regions").IconImage = "checkbox.gif" Then
        If Session("PopCohort_Grouping") = False Then
            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("PopCohort_tempregion")) & " and Years = " & Str(Session("PopCohort_tempyear")), "", tbl2, "")
                Do While Not i = tbl2.Rows.Count
                    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = tbl2.Rows(i).Item(FieldName(Tindex, 1))
                    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = tbl2.Rows(i).Item(FieldName(Tindex, 2))
                    i = i + 1
                Loop
                'jrs 2008/04/17
                If Tindex > 1 Then
                    ReDim Session("POPM")(NCohorts)
                    ReDim Session("POPF")(NCohorts)
                    SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(1) &
                        " Where Region = " & Str(Session("PopCohort_tempregion")) &
                        " and Years = " + Str(Session("PopCohort_tempyear")), "", tbl3, "")
                    i = 0
                    Do While Not i = tbl3.Rows.Count
                        Session("POPM")(tbl3.Rows(i).Item("NCohorts")) = Session("POPM")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 1))
                        Session("POPF")(tbl3.Rows(i).Item("NCohorts")) = Session("POPF")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 2))

                        i = i + 1
                    Loop
                End If


            Else
                'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("PopCohort_tempregion")) & " and Years = " & Str(Session("PopCohort_tempyear")), "", tbl2, "")
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) & " Where RegionId = " & Str(Session("PopCohort_tempregion")) & " and TimeId = " & Str(Session("PopCohort_tempyear"))
                'vSQLiteCommand.CommandText = "SELECT * FROM pop_base_case Where RegionId = " & Str(Session("PopCohort_tempregion")) & " and TimeId = " & Str(Session("PopCohort_tempyear"))
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'Do While Not i = tbl2.Rows.Count
                '    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = tbl2.Rows(i).Item(FieldName(Tindex, 1))
                '    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = tbl2.Rows(i).Item(FieldName(Tindex, 2))
                '    i = i + 1
                'Loop
                While (vSQLiteReader.Read())

                    Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
                    Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))

                End While

                'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                vSQLiteReader.Close()
                vSQLiteReader.Dispose()
                vSQLiteCommand.Dispose()


                'jrs 2008/04/17
                If Tindex > 1 Then
                    ReDim Session("POPM")(NCohorts)
                    ReDim Session("POPF")(NCohorts)
                    'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(1) &
                    '" Where Region = " & Str(Session("PopCohort_tempregion")) &
                    '" and Years = " + Str(Session("PopCohort_tempyear")), "", tbl3, "")

                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(1) &
                    " Where RegionId = " & Str(Session("PopCohort_tempregion")) &
                    " and TimeId = " + Str(Session("PopCohort_tempyear"))

                    vSQLiteReader = vSQLiteCommand.ExecuteReader()

                    'i = 0
                    'Do While Not i = tbl3.Rows.Count
                    '    Session("POPM")(tbl3.Rows(i).Item("NCohorts")) = Session("POPM")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 1))
                    '    Session("POPF")(tbl3.Rows(i).Item("NCohorts")) = Session("POPF")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 2))

                    '    i = i + 1
                    'Loop

                    While (vSQLiteReader.Read())


                        Session("POPM")(vSQLiteReader("CohortId")) = Session("POPM")(vSQLiteReader("CohortId")) + vSQLiteReader(FieldName(1, 1))
                        Session("POPF")(vSQLiteReader("CohortId")) = Session("POPF")(vSQLiteReader("CohortId")) + vSQLiteReader(FieldName(1, 2))


                    End While

                    'jrs 2024/12/06 avoids error when running the model, still using ifsPopWork.db
                    vSQLiteReader.Close()
                    vSQLiteReader.Dispose()
                    vSQLiteCommand.Dispose()

                End If

            End If


            'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("PopCohort_tempregion")) & " and Years = " & Str(Session("PopCohort_tempyear")), "", tbl2, "")
            'Do While Not i = tbl2.Rows.Count
            '    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = tbl2.Rows(i).Item(FieldName(Tindex, 1))
            '    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = tbl2.Rows(i).Item(FieldName(Tindex, 2))
            '    i = i + 1
            'Loop
            ''jrs 2008/04/17
            'If Tindex > 1 Then
            '    ReDim Session("POPM")(NCohorts)
            '    ReDim Session("POPF")(NCohorts)
            '    SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(1) &
            '        " Where Region = " & Str(Session("PopCohort_tempregion")) &
            '        " and Years = " + Str(Session("PopCohort_tempyear")), "", tbl3, "")
            '    i = 0
            '    Do While Not i = tbl3.Rows.Count
            '        Session("POPM")(tbl3.Rows(i).Item("NCohorts")) = Session("POPM")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 1))
            '        Session("POPF")(tbl3.Rows(i).Item("NCohorts")) = Session("POPF")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 2))

            '        i = i + 1
            '    Loop
            'End If

        Else
            LoadCountries((Session("PopCohort_regionname")))
            'jrs 2008/04/17
            ReDim Session("POPM")(NCohorts)
            ReDim Session("POPF")(NCohorts)
            ReDim Session("POPMales")(Session("GroupTot"), NCohorts)
            ReDim Session("POPFemales")(Session("GroupTot"), NCohorts)

            Dim sRegs As String
            sRegs = ""
            For rId = 1 To Session("GroupTot")
                If sRegs = "" Then
                    sRegs = Str(Session("RegionIdGroup")(rId))
                Else
                    sRegs = sRegs & "," & Str(Session("RegionIdGroup")(rId))
                End If
            Next rId

            'jrs 2014/01/29
            If Session("bReadGrpMaxs") Then
                SetUpMaxsTime()
                Session("bReadGrpMaxs") = False
            End If

            'mti WebForm Changes From July 05, 2004
            'Session("popConn").open()


            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(Tindex) &
                " Where Region in ( " & sRegs & ") and Years = " & Str(Session("PopCohort_tempyear")) &
                " order by Region , NCohorts", "", tbl2, "")
                Rcnt = tbl2.Rows.Count

                'jrs 2008/04/17 Population weigthed average for Mortality and Fertility
                If Tindex = 1 Then
                    i = 0
                    Do While i < Rcnt 'True 'Not i = tbl2.Rows.Count
                        Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) + tbl2.Rows(i).Item(FieldName(Tindex, 1))
                        Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) + tbl2.Rows(i).Item(FieldName(Tindex, 2))
                        i = i + 1
                    Loop
                Else
                    SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " + TableName(1) &
                " Where Region in ( " + sRegs + ") and Years = " & Str(Session("PopCohort_tempyear")) &
                " order by Region , NCohorts", "", tbl3, "")
                    i = 0
                    Do While Not i = tbl3.Rows.Count
                        For rId = 1 To Session("GroupTot")
                            If Session("RegionIdGroup")(rId) = tbl3.Rows(i).Item("Region") Then
                                Exit For
                            End If
                        Next rId

                        Session("POPMales")(rId, tbl3.Rows(i).Item("NCohorts")) = tbl3.Rows(i).Item(FieldName(1, 1))
                        Session("POPFemales")(rId, tbl3.Rows(i).Item("NCohorts")) = tbl3.Rows(i).Item(FieldName(1, 2))
                        Session("POPM")(tbl3.Rows(i).Item("NCohorts")) = Session("POPM")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 1))
                        Session("POPF")(tbl3.Rows(i).Item("NCohorts")) = Session("POPF")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 2))

                        i = i + 1
                    Loop

                    i = 0
                    Do While i < Rcnt 'True 'Not i = tbl2.Rows.Count
                        For rId = 1 To Session("GroupTot")
                            If Session("RegionIdGroup")(rId) = tbl2.Rows(i).Item("Region") Then
                                Exit For
                            End If
                        Next rId

                        Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) + tbl2.Rows(i).Item(FieldName(Tindex, 1)) * Session("POPMales")(rId, tbl2.Rows(i).Item("NCohorts"))
                        Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) + tbl2.Rows(i).Item(FieldName(Tindex, 2)) * Session("POPFemales")(rId, tbl2.Rows(i).Item("NCohorts"))
                        i = i + 1
                    Loop
                End If

                'mti WebForm Changes From July 05, 2004
                'Session("popConn").close()
                'if its fertility or mortality get average
                If Tindex > 1 And Session("GroupTot") > 1 Then
                    For rId = 0 To NCohorts 'jrs 2005/11/29 changed from 1 to 0
                        'jrs 2008/04/17
                        'Session("PC_TempDataBase")(rId, 1) = Session("PC_TempDataBase")(rId, 1) / Session("GroupTot")
                        'Session("PC_TempDataBase")(rId, 2) = Session("PC_TempDataBase")(rId, 2) / Session("GroupTot")
                        Session("PC_TempDataBase")(rId, 1) = Session("PC_TempDataBase")(rId, 1) / Session("POPM")(rId)
                        Session("PC_TempDataBase")(rId, 2) = Session("PC_TempDataBase")(rId, 2) / Session("POPF")(rId)
                    Next rId
                End If

            Else 'JAM 2019-11-17

                'SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " & TableName(Tindex) &
                '                    " Where Region in ( " & sRegs & ") and Years = " & Str(Session("PopCohort_tempyear")) &
                '                    " order by Region , NCohorts", "", tbl2, "")
                'Rcnt = tbl2.Rows.Count

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) &
                                    " Where RegionId in ( " & sRegs & ") and TimeId = " & Str(Session("PopCohort_tempyear")) &
                                    " order by RegionId , CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                'jrs 2008/04/17 Population weigthed average for Mortality and Fertility
                If Tindex = 1 Then
                    'i = 0
                    'Do While i < Rcnt 'True 'Not i = tbl2.Rows.Count
                    '    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) + tbl2.Rows(i).Item(FieldName(Tindex, 1))
                    '    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) + tbl2.Rows(i).Item(FieldName(Tindex, 2))
                    '    i = i + 1
                    'Loop


                    While (vSQLiteReader.Read())


                        Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) = Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) + vSQLiteReader(FieldName(Tindex, 1))
                        Session("PC_TempDataBase")(vSQLiteReader.Item("CohortId"), 2) = Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) + vSQLiteReader(FieldName(Tindex, 2))


                    End While


                Else
                    '    SetDataTablefromAccess(Session("db2Path"), "SELECT * FROM " + TableName(1) &
                    '" Where Region in ( " + sRegs + ") and Years = " & Str(Session("PopCohort_tempyear")) &
                    '" order by Region , NCohorts", "", tbl3, "")

                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM " + TableName(1) &
                            " Where RegionId in ( " + sRegs + ") and TimeId = " & Str(Session("PopCohort_tempyear")) &
                            " order by RegionId , CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    'i = 0
                    'Do While Not i = tbl3.Rows.Count
                    '    For rId = 1 To Session("GroupTot")
                    '        If Session("RegionIdGroup")(rId) = tbl3.Rows(i).Item("Region") Then
                    '            Exit For
                    '        End If
                    '    Next rId

                    '    Session("POPMales")(rId, tbl3.Rows(i).Item("NCohorts")) = tbl3.Rows(i).Item(FieldName(1, 1))
                    '    Session("POPFemales")(rId, tbl3.Rows(i).Item("NCohorts")) = tbl3.Rows(i).Item(FieldName(1, 2))
                    '    Session("POPM")(tbl3.Rows(i).Item("NCohorts")) = Session("POPM")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 1))
                    '    Session("POPF")(tbl3.Rows(i).Item("NCohorts")) = Session("POPF")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 2))

                    '    i = i + 1
                    'Loop

                    While (vSQLiteReader.Read())

                        For rId = 1 To Session("GroupTot")
                            If Session("RegionIdGroup")(rId) = vSQLiteReader("RegionId") Then
                                Exit For
                            End If
                        Next rId

                        Session("POPMales")(rId, vSQLiteReader("CohortId")) = vSQLiteReader(FieldName(1, 1))
                        Session("POPFemales")(rId, vSQLiteReader("CohortId")) = vSQLiteReader(FieldName(1, 2))
                        Session("POPM")(vSQLiteReader("CohortId")) = Session("POPM")(vSQLiteReader("CohortId")) + vSQLiteReader(FieldName(1, 1))
                        Session("POPF")(vSQLiteReader("CohortId")) = Session("POPF")(vSQLiteReader("CohortId")) + vSQLiteReader(FieldName(1, 2))



                    End While



                    'i = 0
                    'Do While i < Rcnt 'True 'Not i = tbl2.Rows.Count
                    '    For rId = 1 To Session("GroupTot")
                    '        If Session("RegionIdGroup")(rId) = tbl2.Rows(i).Item("Region") Then
                    '            Exit For
                    '        End If
                    '    Next rId

                    '    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) + tbl2.Rows(i).Item(FieldName(Tindex, 1)) * Session("POPMales")(rId, tbl2.Rows(i).Item("NCohorts"))
                    '    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) + tbl2.Rows(i).Item(FieldName(Tindex, 2)) * Session("POPFemales")(rId, tbl2.Rows(i).Item("NCohorts"))
                    '    i = i + 1
                    'Loop

                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) &
                                        " Where RegionId in ( " & sRegs & ") and TimeId = " & Str(Session("PopCohort_tempyear")) &
                                        " order by RegionId , CohortId"
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    While (vSQLiteReader.Read())

                        For rId = 1 To Session("GroupTot")
                            If Session("RegionIdGroup")(rId) = vSQLiteReader("RegionId") Then
                                Exit For
                            End If
                        Next rId

                        'jrs 2024/04/24
                        If Not IsDBNull(vSQLiteReader(FieldName(Tindex, 1))) Then
                            Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) = Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) + vSQLiteReader(FieldName(Tindex, 1)) * Session("POPMales")(rId, vSQLiteReader("CohortId"))
                        End If
                        If Not IsDBNull(vSQLiteReader(FieldName(Tindex, 2))) Then
                            Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) = Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) + vSQLiteReader(FieldName(Tindex, 2)) * Session("POPFemales")(rId, vSQLiteReader("CohortId"))
                        End If
                        i = i + 1



                    End While



                End If

                'mti WebForm Changes From July 05, 2004
                'Session("popConn").close()
                'if its fertility or mortality get average
                If Tindex > 1 And Session("GroupTot") > 1 Then
                    For rId = 0 To NCohorts 'jrs 2005/11/29 changed from 1 to 0
                        'jrs 2008/04/17
                        'Session("PC_TempDataBase")(rId, 1) = Session("PC_TempDataBase")(rId, 1) / Session("GroupTot")
                        'Session("PC_TempDataBase")(rId, 2) = Session("PC_TempDataBase")(rId, 2) / Session("GroupTot")
                        Session("PC_TempDataBase")(rId, 1) = Session("PC_TempDataBase")(rId, 1) / Session("POPM")(rId)
                        Session("PC_TempDataBase")(rId, 2) = Session("PC_TempDataBase")(rId, 2) / Session("POPF")(rId)
                    Next rId
                End If


            End If





        End If


        'jrs 2014/02/05
        If (index = 1 Or index = 4) And Session("mnuUsePctPop") Then
            ConvertToPctPop()
        End If

        FillDataGrid(Tindex)

        'end mti june, 2002


        '   Draw the graph
        '   --------------
        '    gphAgedst(Index).AutoInc = 1
        '    gphAgedst(Index).DataReset = 1

        Dim TMax As Short
        Session("PopCG_Index") = index
        Session("POPCG_Regionname") = Session("PopCohort_regionname")
        'Session("POPCG_Casename") = CaseName
        Session("POPCG_tempyear") = Session("PopCohort_tempyear")

        'mti Ifs Win April 25, 2004
        If (index = 1) Then 'Age distribution; split across males/females



            GraphTitle = "Population"

            'jrs 2014/02/14
            If Session("mnuUsePctPop") = False Then
                Session("POPCB_Units") = "Population in Millions"
            Else
                Session("POPCB_Units") = "Percent of Total Population"
            End If

        ElseIf (index = 2) Then 'Fertility

            GraphTitle = "Fertility"
            Session("POPCB_Units") = "Births/1000 Women per Year"



        ElseIf (index = 3) Then 'Mortality


            GraphTitle = "Mortality"
            Session("POPCB_Units") = "Deaths/1000 People per Year"
            'Start mti june, 2002
        ElseIf (index = 4) Then 'Age distribution; split across males/females

            GraphTitle = "Population"

            'jrs 2014/02/14
            If Session("mnuUsePctPop") = False Then
                Session("POPCB_Units") = "Population in Millions"
            Else
                Session("POPCB_Units") = "Percent of Total Population"
            End If
            'end mti june, 2002

        End If

        'Start mti june, 2002
        lbldata.Text = GraphTitle & " Distribution for " & Session("PopCohort_regionname") & " in Year " & Str(Session("PopCohort_tempyear")) & " [" & Session("POPCG_Casename") & "]" & " - " & Session("POPCB_Units")
        'end mti june, 2002
        PackJScritpBuilder()

ErrHandler01:
        Exit Sub

    End Sub

    Public Sub mnuAboutPopulation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "~\frm_AboutPopulation.aspx")
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuAdvance_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim end_year As Object
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        '--------------------------------------------------------------
        Session("PopCohort_tempyear") = Session("PopCohort_tempyear") + 1

        If Case_of_Work = 0 Then
            'jrs 2005/01/10 end_year = Session("LastYear")
            'mti July 2006 end_year = LastYearForecast
            end_year = FindLastYear()
            If end_year = 0 Then
                end_year = Session("LastYearForecast")
            End If
        Else
            end_year = Session("PopCohort_year_run")
        End If

        If Session("PopCohort_tempyear") <= end_year Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If
        If Session("PopCohort_tempyear") >= end_year Then
            objSender.Groups("Time").Items("Advance").Enabled = False
            Session("PopCohort_tempyear") = end_year
        End If
        objSender.Groups("Time").Items("Regress").Enabled = True


    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Support.FormManager.Close(Me)

    End Sub


    '==========================frm_CohoDisp=============================
    Public Sub mnuRegress_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'jrs 2008/06/06 If Session("PopCohort_tempyear") <= FirstYearForecast Then
        If Session("PopCohort_tempyear") <= Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            Session("PopCohort_tempyear") = Session("PopCohort_tempyear") - 1
            If Session("PopCohort_tempyear") <= Session("FirstYearSce") Then
                Session("PopCohort_tempyear") = Session("FirstYearSce")
                objSender.Groups("Time").Items("Regress").Enabled = False
            End If
            objSender.Groups("Time").Items("Advance").Enabled = True
            Graph_Draw((Session("PopCohort_GraphType")))
        End If

    End Sub

    '==========================frm_CohoDisp=============================
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        '--------------------------------------------------------------

        'JAM 2019-11-17
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") Then
                vSQLiteCon.Close()
            End If
        End If


        'mti june, 2002 - Add this procedure
        frameData.Visible = False
        'jrs 2008/06/25
        grdcohort.Visible = False
        lbldata.Visible = False
        'jrs 2011/11/16
        lblJS.Visible = True

    End Sub


    '==========================frm_CohoDisp=============================
    Private Sub cmdGridSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGridSave.Click
        Dim ii As Short
        Dim l As Short
        Dim CDERR_CANCEL As Object
        Dim GraphTitle As String
        'jrs 2012/01/27
        '            Dim DataOutputFileName As Object
        '            '--------------------------------------------------------------

        '            'mti june, 2002 - Add this procedure
        '            '   Get filename
        '            '   --------------

        '            DataOutputFileName = "*.CSV"

        '            On Error GoTo SelDataFileError_Line
        '            'jrs 2008/06/25 
        '            'cmd_ScriptFile.DefaultExt = "CSV"
        '            'cmd_ScriptFile.Filename = DataOutputFileName
        '            'cmd_ScriptFile.DialogTitle = "Save " & GraphTitle & " Data"
        '            'cmd_ScriptFile.InitDir = PutFdrvRun2
        '            'cmd_ScriptFile.Action = 1
        '            DataOutputFileName = Session("PutFdrvRun") & "Save.csv" 'jrs 2008/06/25 cmd_ScriptFile.Filename
        '            '        On Error GoTo 0
        '            If VB.Left(DataOutputFileName, 1) = "*" Then Exit Sub
        '            GoTo WriteDataFile_Line

        'SelDataFileError_Line:
        '            If Err.Number = CDERR_CANCEL Then
        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            Else
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            End If

        'WriteDataFile_Line:
        '            Dim fNum As Integer
        '            fNum = FreeFile()
        '            FileOpen(fNum, DataOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        'jrs 2012/01/27
        'PrintLine(fNum, lbldata.Text)
        'PrintLine(fNum, New String("-", Len(lbldata.Text)))
        Context.Response.Write(lbldata.Text)
        Context.Response.Write(Environment.NewLine)
        Context.Response.Write(New String("-", Len(lbldata.Text)))
        Context.Response.Write(Environment.NewLine)

        For l = 0 To grdcohort.TotalRows - 1
            For ii = 0 To grdcohort.Columns.Count - 1
                'jrs 2012/01/27 Print(fNum, grdcohort(ii, l) & ",")
                Context.Response.Write(grdcohort(ii, l) & ",")
            Next ii
            'jrs 2012/01/27 PrintLine(fNum, TAB)
            Context.Response.Write(Environment.NewLine)
        Next l
        'jrs 2012/01/27 FileClose(fNum)

        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(DataOutputFileName)
        Response.End()
        '**************************************

    End Sub

    '==========================frm_CohoDisp=============================
    'UPGRADE_WARNING: Event cntrylist.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cntrylist_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cntrylist.SelectedIndexChanged
        '--------------------------------------------------------------

        '  Specify the country selected
        '  ----------------------------
        If cntrylist.SelectedIndex = -1 Then
            Session("PopCohort_tempregion") = 1
        Else
            Session("PopCohort_tempregion") = cntrylist.SelectedIndex + 1
        End If

        Session("PopCohort_regionname") = GRPS(1, Session("PopCohort_tempregion"))
        cntrylist.Visible = False

        'jrs 2014/01/29
        If Session("mnuFixCteScTime") Then
            SetUpMaxsTime()
        End If

        'jrs 2014/02/14
        If Session("bCompare") = False Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If
    End Sub

    '==========================frm_CohoDisp=============================
    Private Sub frm_PopCohortDisp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'If Not Page.IsPostBack Then
        'jrs 2024/11/29 not useful and producing errors in web server
        'If Support.FormManager.LoadState Then
        '    Support.FormManager.StateRestore(Me)
        '    Support.FormManager.LoadState = False
        'End If
        Dim l As Short
        Dim ButSpace As Short
        Dim SumWidth As Object
        Dim HalInch As Object
        '--------------------------------------------------------------


        'jam 2021/03/31
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim othYear As String
        Dim othFile As String
        Dim qtyPAram As Integer


        On Error GoTo ErrHandler02


        'jam 2021/03/31
        If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Request.QueryString("Year")) Or Not IsNothing(Request.QueryString("File")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                'jrs 2024/04/23
                If Not IsNothing(Request.QueryString("Group")) And Not Page.IsPostBack Then
                    othCountry = Request.QueryString("Group")
                Else
                    othCountry = ""
                End If

            End If

            If Not IsNothing(Request.QueryString("Year")) And Not Page.IsPostBack Then

                othYear = Request.QueryString("Year")

            Else
                othYear = ""
            End If

            If Not IsNothing(Request.QueryString("File")) And Not Page.IsPostBack Then

                othFile = Request.QueryString("File")

            Else
                othFile = ""
            End If



            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")


            If othCountry <> "" And othYear <> "" And othFile <> "" Then
                vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", PopPrepareForFriendlyURL(othCountry), othYear, othFile)

                Response.Redirect(vurl)

            Else
                If othCountry <> "" And othYear = "" And othFile = "" Then
                    vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", PopPrepareForFriendlyURL(othCountry))

                    Response.Redirect(vurl)

                Else
                    If othCountry <> "" And othYear <> "" And othFile = "" Then
                        vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", PopPrepareForFriendlyURL(othCountry), othYear)

                        Response.Redirect(vurl)
                    End If

                End If


            End If


        End If




        'JAM 2019-11-17
        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") Then
                'JAM 2020-05-04
                If Not Session("SQLiteDB") Is Nothing Then 'JAM 2020-04-13
                    vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                Else
                    vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                End If

                'jam 2025/05/30 Changing connection mode
                'vSQLiteCon = New SQLiteConnection(vConnString) '& "New=true;"
                'vSQLiteCon.Open()

                vSQLiteCon = OpenJournalSQLite(vConnString)

            End If
        End If









        'end WebMenu


        '   Initializing the form with Base Case
        '   ------------------------------------


        '   Initializing the table names and field names
        '   --------------------------------------------
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            TableName(1) = "PopBaseCase"
            TableName(2) = "FerBaseCase"
            TableName(3) = "MorBaseCase"
            FieldName(1, 1) = "PopulationMales"
            FieldName(1, 2) = "PopulationFemales"
            FieldName(2, 1) = "Fertility"
            FieldName(2, 2) = "Fertility"
            FieldName(3, 1) = "MortalityMales"
            FieldName(3, 2) = "MortalityFemales"
        Else

            'JAM 2019-11-30
            TableName(1) = "Pop_Base_Case"
            TableName(2) = "Fer_Base_Case"
            TableName(3) = "Mor_Base_Case"
            FieldName(1, 1) = "PopulationMales"
            FieldName(1, 2) = "PopulationFemales"
            FieldName(2, 1) = "Fertility"
            FieldName(2, 2) = "Fertility"
            FieldName(3, 1) = "MortalityMales"
            FieldName(3, 2) = "MortalityFemales"
        End If

        'jam 2021/03/31
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String
        Dim vYear As String
        Dim vScenario As String

        If segments.Count = 1 Then

            vCountry = segments(0)
            vYear = Nothing
            vScenario = Nothing

        ElseIf segments.Count = 2 Then
            vCountry = segments(0)
            'jrs 2024/04/23
            If IsNumeric(segments(1)) Then
                vYear = segments(1)
                vScenario = Nothing
            Else
                vYear = Nothing
                vScenario = segments(1)
            End If
        ElseIf segments.Count = 3 Then
            vCountry = segments(0)
            vYear = segments(1)
            vScenario = segments(2)

        ElseIf segments.Count > 3 Then 'jrs 2025/02/04
            vCountry = segments(0)
            Session("vCountry") = vCountry
            vYear = Nothing
            vScenario = Nothing
        End If

        'jrs 2025/02/04 added if > 0
        If segments.Count > 0 Then
            Session("vCountry") = vCountry
            Session("vYear") = vYear
            Session("vScenario") = vScenario
        End If




        'JRS 2/8/03 mnuRegress.Enabled = False
        'mti CohoDisp May 2004 uncomment later
        'mnuRegress.Enabled = False
        If Not Page.IsPostBack Then
            'jrs 2014/06/24
            Session("bRedoMaxScale") = False
            Session("bReLoadCompare") = False

            If Session("POPCG_Case_of_Work") Is Nothing Then
                'jrs 2026/01/26 changed default to base case
                Case_of_Work = 1
                'jrs 2008/09/16
                Session("POPCG_Case_of_Work") = Case_of_Work
            Else
                Case_of_Work = Session("POPCG_Case_of_Work")
            End If


            If Session("POPCG_Casename") Is Nothing Then
                'jrs 2026/01/26 changed default to base case
                'jrs 2024/04/24
                CaseName = "Base Case"

                Session("POPCG_Casename") = CaseName
            Else
                CaseName = Session("POPCG_Casename")
            End If

            'mti WebForm Changes From June 21, 2004 -start
            Dim TempDatabase(NCohorts, 2) As Single 'has to be session var
            Dim CountryGroup(250) As String
            Dim RegionIdGroup(250) As Short

            Session("PC_TempDataBase") = TempDatabase  '(NCohorts, 2)
            'jrs 2014/02/14 Session("CountryGroup") = CountryGroup
            Session("RegionIdGroup") = RegionIdGroup


            'mti WebForm Changes From June 21, 2004 -end
            'jrs 2024/09/06 changed from = Nothing to is Nothing
            'jrs 2011/05/05
            If Session("PopCohort_Grouping") Is Nothing Then
                Session("PopCohort_Grouping") = False
                Session("PopCohort_tempregion") = 1
                Session("PopCohort_regionname") = GRPS(1, 1)
            End If

            'jrs 2024/04/24
            ''JAM 2019-10-09
            'If Session("mnuSQLite") Is Nothing Then
            '    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            'Else

            '    If Not Session("mnuSQLite") = "True" Then
            '        Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
            '    Else
            '        'Session("db2Path") = Session("PutFdrvRun") & "ifsPop.db"

            '        Session("SQLiteDB") = "IFsPop.db"

            '        Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

            '    End If
            'End If



            ' Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
            Session("PopCohort_year_run") = Session("LastYear")

            '   Initializing the graph with population graph for First Region, Session("FirstYear")
            '   ------------------------------------------------------------------------
            'jrs 2005/01/10 Session("PopCohort_tempyear") = AMAX(Session("FirstYear"), 1995)
            If Session("PopCohort_tempyear") Is Nothing Then
                Session("PopCohort_tempyear") = AMAX(FirstYearForecast, 1995)
            End If

            If CStr(Session("PopCohort_tempyear")) = "" Then
                Session("PopCohort_tempyear") = AMAX(FirstYearForecast, 1995)
            End If

            If Session("PopCohort_GraphType") Is Nothing Then
                'jrs 2026/05/13 Session("PopCohort_GraphType") = 1
                'Changed default to pyramid
                Session("PopCohort_GraphType") = 4
            End If


            '   Code which takes the region names from GRPS$ vector
            '   ---------------------------------------------------
            cntrylist.Items.Clear()
            For l = 1 To NR
                cntrylist.Items.Add(New ListItem(GRPS(1, l)))
            Next l
            '   Code which takes the group names from Groupnames vector
            '   ---------------------------------------------------
            lstGroups.Items.Clear()
            'SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " Order by GroupName", "", valtbl)
            ''JRS 5/21/02
            'Dim i As Integer
            'For i = 0 To valtbl.Rows.Count - 1
            '    lstGroups.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
            'Next i
            'lstGroups.Items.Add(New ListItem("ALL"))

            'jrs 2024/09/06 *** start ***
            If Session("bCheckHideNonCoreGroups") Is Nothing Then
                Session("bCheckHideNonCoreGroups") = bCoreGroupCheck 'cdv 2021/09/01
            End If

            If Session("bCheckHideNonCoreGroups") Then
                For l = 1 To CGCount
                    lstGroups.Items.Add(New ListItem(CoreGroupNames(l)))
                Next l
            Else
                For l = 1 To GCount
                    lstGroups.Items.Add(New ListItem(GroupNames(l)))
                Next l
            End If
            'jrs 2024/09/06 *** end ***

            lstYear.Items.Clear()
            For l = Session("FirstYear") To Session("LastYear") 'GCount
                lstYear.Items.Add(New ListItem(l.ToString))
            Next l

            'jrs 2008/05/07
            Session("AboutParent") = "frm_PopCohortDisp.aspx"

            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            Graph_Draw((Session("PopCohort_GraphType")))

            'jrs 2008/06/06
            Session("FirstYearSce") = Session("FirstYear")
        Else 'jrs 2008/09/16
            Case_of_Work = Session("POPCG_Case_of_Work")
        End If

        'webmenu
        'mti WebForm Changes to be taken by Jose
        wmMain.UserData = "jsolorza::976739975"

        wmMain.MenuBar.ShowOnMouseOver = True

        'jam 2021/03/31
        'wmMain.ClearPixelImage = "blank.gif"
        'wmMain.PopupIcon = "./images/Popup.gif"
        'wmMain.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'wmMain.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'wmMain.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'wmMain.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'wmMain.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/03/31
        wmMain.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        wmMain.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        wmMain.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        wmMain.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        wmMain.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        wmMain.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        wmMain.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")



        wmMain.ScrollEnabled = True
        wmMain.HideTimer = 1000
        'jrs 2012/01/17
        wmMain.AccessorKeyCode = 27
        'jrs 2012/01/24
        wmMain.ButtonFont.Size = "12px"
        wmMain.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_e2bf"
        'jrs 2005/02/20
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        'jrs 2008/06/24 need some code to remove session variables cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"
        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"
        'jrs 2011/05/02
        If InStr(Session("DisplayParent"), "frm_CountryProfile.aspx") > 0 Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Main Menu"
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Distribution Type"
        cswmGroup1.ID = "Group_2e54"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Age Distribution"
        'jrs 2014/10/15
        'cswmItem.IconImage = "blank.gif"
        'jam 2021/03/31
        cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fertility Distribution"
        'jrs 2014/10/15
        'cswmItem.IconImage = "blank.gif"
        'jam 2021/03/31
        cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")



        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Mortality Distribution"
        'jrs 2014/10/15
        'cswmItem.IconImage = "blank.gif"
        'jam 2021/03/31
        cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")



        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Population Pyramid"
        'jrs 2014/10/15
        'cswmItem.IconImage = "blank.gif"

        'jam 2021/03/31
        cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")


        'jrs 2014/02/11
        cswmItem = cswmGroup1.Items.Add
        If Session("PopCohort_Grouping") = True Then
            cswmItem.Caption = "Compare Groups"
        Else
            cswmItem.Caption = "Compare Countries"
        End If
        cswmItem.ID = "mnuCompare"
        'jrs 2014/10/15
        If Session("bCompare") Then
            'cswmItem.IconImage = "checkbox.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")


        Else
            'cswmItem.IconImage = "blank.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")

        End If
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Show Numbers"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Numbers"


        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Set Group or Country/Region"
        cswmGroup1.ID = "mnuGroups"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Countries/Regions"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"
        If Session("PopCohort_Grouping") = True Then
            'cswmItem.IconImage = "blank.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")

        Else
            'cswmItem.IconImage = "checkbox.gif"


            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")

        End If
        'mti Augut 2004 3 end

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Groups"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'mti Augut 2004 3 -start
        'cswmItem.IconImage = "blank.gif"
        If Session("PopCohort_Grouping") = True Then
            'cswmItem.IconImage = "checkbox.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")


        Else
            'cswmItem.IconImage = "blank.gif"


            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")


        End If

        'jrs 2024/09/06
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Hide Non Core Groups"
        cswmItem.IconHeight = 16
        If Session("bCheckHideNonCoreGroups") Then
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")
        End If

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Change Display"
        cswmGroup1.ID = "Group_1113"

        cswmItem = cswmGroup1.Items.Add()
        If Session("PopCohort_Grouping") = True Then
            cswmItem.Caption = "Change Groups"
        Else
            cswmItem.Caption = "Change Countries/Regions"
        End If

        'jrs 2011/11/14
        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()

        If Session("PopCohort_Grouping") = False Then
            'jrs 2026/02/02
            LoadRegionAlphaGroups(5, cswmGroup2, cswmItem)

            'For l = 1 To NR
            '    cswmItem = cswmGroup2.Items.Add()
            '    cswmItem.Caption = GRPS(1, l)
            '    cswmItem.UserData = "Reg"
            '    'cswmItem.OnClick = "cntrymnu_click('" & IFsRegs.Item(GRPS(1, l)).ISO3166_2Code & "');"
            '    If l < NR Then
            '        If GRPS(1, l).Chars(0) <> GRPS(1, l + 1).Chars(0) Then
            '            cswmItem = cswmGroup2.Items.Add()
            '            cswmItem.Caption = "--shadow--"
            '            cswmItem.DividerColor = "#808080"
            '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
            '        End If
            '    End If
            'Next
        Else
            'jrs 2024/09/06
            If Session("bCheckHideNonCoreGroups") Then
                'jrs 2026/02/03
                LoadCoreGroupsAlphaGroups(4, cswmGroup2, cswmItem)
                'For l = 1 To CGCount
                '    cswmItem = cswmGroup2.Items.Add()
                '    cswmItem.Caption = CoreGroupNames(l)
                '    cswmItem.UserData = "Grp"
                '    If l < CGCount Then
                '        If CoreGroupNames(l).Chars(0) <> CoreGroupNames(l + 1).Chars(0) Then
                '            cswmItem = cswmGroup2.Items.Add()
                '            cswmItem.Caption = "--shadow--"
                '            cswmItem.DividerColor = "#808080"
                '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                '        End If
                '    End If
                'Next
            Else
                'jrs 2026/02/03
                LoadAllGroupsAlphaGroups(20, cswmGroup2, cswmItem)

                'For l = 1 To GCount
                '    cswmItem = cswmGroup2.Items.Add()
                '    cswmItem.Caption = GroupNames(l)
                '    cswmItem.UserData = "Grp"
                '    If l < GCount Then
                '        If GroupNames(l).Chars(0) <> GroupNames(l + 1).Chars(0) Then
                '            cswmItem = cswmGroup2.Items.Add()
                '            cswmItem.Caption = "--shadow--"
                '            cswmItem.DividerColor = "#808080"
                '            cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                '        End If
                '    End If
                'Next
            End If
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Year"

        'jrs 2011/11/14
        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Please select a year"
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "--shadow--"
        cswmItem.DividerColor = "#808080"
        cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider

        For l = Session("FirstYear") To Session("LastYear")
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = l.ToString
            cswmItem.UserData = "Yr"
            'cswmItem.BackgroundImage = "./images/itemBackground.gif"
            If l < Session("LastYear") Then
                If l Mod 10 = 9 Then
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = "--shadow--"
                    cswmItem.DividerColor = "#808080"
                    cswmItem.Style = Coalesys.WebMenu.StyleConstants.Divider
                End If
            End If
        Next

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Scenario"

        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Base Case"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        If Session("POPCG_Casename") = "Base Case" Then
            'cswmItem.IconImage = "checkbox.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")


        Else
            'cswmItem.IconImage = "blank.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")

        End If
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Work Case"
        cswmItem.IconHeight = 16
        'cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'mti WebForm Changes From June 21, 2004 
        If Session("POPCG_Casename") = "Work Case" Then
            'cswmItem.IconImage = "checkbox.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")

        Else
            'cswmItem.IconImage = "blank.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")


        End If
        'jrs 2008/06/16
        If UCase(Session("ActiveBase")) <> "IFSBASEHIST.RUN" Then
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Historical Forecast"
            cswmItem.IconHeight = 16
            If Session("POPCG_Casename") = "Historical Forecast" Then
                'cswmItem.IconImage = "checkbox.gif"

                'jam 2021/03/31
                cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")

            Else
                'cswmItem.IconImage = "blank.gif"

                'jam 2021/03/31
                cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")

            End If
        End If

        'jrs 2008/06/05
        'Load other Scenario Menu options for display
        LoadSceMenus(cswmGroup2)

        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Time"
        cswmGroup1.ID = "Group_5ca8"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Advance"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regress"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"

        'jrs 2014/02/14
        If Session("bCompare") = True Then
            wmMain.Groups("Change Display").ShowMenuBarItem = False
            wmMain.Groups("Time").ShowMenuBarItem = False
            wmMain.Groups("mnuGroups").ShowMenuBarItem = False
        End If


        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Advance"
        'cswmGroup1.ID = "Group_5ca8"

        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Regress"
        'cswmGroup1.ID = "Group_a83d"

        'mti enable this later 
        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Print"
        'cswmGroup1.ID = "Group_f3d2"

        'mti enable this later 
        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "Save"
        'cswmGroup1.ID = "Group_3f67"

        'mti enable this later 
        'cswmGroup1 = wmMain.Groups.Add()
        'cswmGroup1.Caption = "About Population"
        'cswmGroup1.ID = "Group_8afc"

        'jrs 2014/02/13
        If Session("mnuFixCtSc") Is Nothing Then
            Session("mnuFixCtSc") = False
            Session("mnuFixCteScTime") = False
            Session("mnuFixCtScMan") = False
            Session("mnuUsePctPop") = False
            Session("bReadGrpMaxs") = False
        End If
        ReDim Preserve Session("MaxScale")(4)
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Change Scale"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale across Time and Regions"
        If Session("mnuFixCtSc") = False Then
            'cswmItem.IconImage = "blank.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")


        Else
            'cswmItem.IconImage = "checkbox.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")

        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale across Time"
        If Session("mnuFixCteScTime") = False Then
            'jam 2021/04/03
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Else
            'cswmItem.IconImage = "checkbox.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")


        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Fix Constant Scale Manually"
        If Session("mnuFixCtScMan") = False Then
            'cswmItem.IconImage = "blank.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")


        Else
            'cswmItem.IconImage = "checkbox.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")

        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Percent of Population"
        If Session("mnuUsePctPop") = False Then
            'cswmItem.IconImage = "blank.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif")


        Else
            'cswmItem.IconImage = "checkbox.gif"

            'jam 2021/03/31
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif")


        End If

        'jrs 2006/12/21
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16004);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jrs 2008/05/07
        cswmGroup1 = wmMain.Groups.Add()
        cswmGroup1.Caption = "About"
        'jrs 2025/02/04 cswmGroup1.OnMenuBarClick = "location.href='frm_AboutPopulation.aspx';"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "About"



        AddHandler wmMain.ServerItemClick, AddressOf ItemClickHandlerMain
        'If Session("PopCohort_Grouping") Then
        '    wmMain.Groups("Use Group or Region").Items("Groups").IconImage = "checkbox.gif"
        '    wmMain.Groups("Use Group or Region").Items("Regions").IconImage = "blank.gif"
        '    'Session("PopCohort_Grouping") = True
        '    'lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
        'Else
        '    wmMain.Groups("Use Group or Region").Items("Regions").IconImage = "checkbox.gif"
        '    wmMain.Groups("Use Group or Region").Items("Groups").IconImage = "blank.gif"
        '    'Session("PopCohort_Grouping") = False
        '    'cntrylist_SelectedIndexChanged(cntrylist, New System.EventArgs)
        'End If
        'End If


        'jrs 2011/05/02
        'If Not IsNothing(Request.QueryString("Country")) Then

        'jam 2021/04/02
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(vCountry)) And Not Session("IsGroup") Then
            'jam 2021/04/02
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> vCountry Or Not Page.IsPostBack Then
                'jam 2021/04/02
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = vCountry


                'jrs 2011/09/15
                If Session("PopCohort_Grouping") Then
                    'need to fix menus
                    'jam 2021/03/31
                    wmMain.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    wmMain.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                    If Session("bCompare") = False Then
                        wmMain.Groups("Change Display").Items(0).Caption = "Change Countries/Regions"
                    End If
                End If
                Session("PopCohort_Grouping") = False
                'Validate
                Dim rs As DataTable
                SetDataTablefromAccess(Session("FdrvRun") & "IFs.mdb", "SELECT NewRegion FROM [Country Translation] as c, " &
                Regionalization & " as r where c.Country = r.Country AND [ISO3166-2 Codes]=""" & Session("MMCountry") & """", "", rs)
                If rs.Rows.Count = 0 Then
                    Session("MMCountry") = ""
                    rs = Nothing
                Else
                    Session("cNumber") = rs.Rows(0).Item("NewRegion")
                End If
            End If
            'jam 2021/04/02
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(vCountry)) And Session("IsGroup") Then
            'jam 2021/04/02
            'If Session("PopCohort_regionname") <> Request.QueryString("Group") Then
            If Session("PopCohort_regionname") <> vCountry Then 'JAM 2020-10-10
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jrs 2024/09/06
                If Session("bCheckHideNonCoreGroups") Then
                    'jam 2021/04/02
                    If Request.QueryString("Group") Is Nothing Then
                        gIdx = gGetCoreGroupIdx(vCountry, False, Me)
                    Else
                        gIdx = gGetCoreGroupIdx(Request.QueryString("Group"), False, Me)
                    End If
                Else
                    'jam 2021/04/02
                    If Request.QueryString("Group") Is Nothing Then
                        gIdx = gGetGroupIdx(vCountry, False, Me)
                    Else
                        gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                    End If
                End If

                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                If gIdx > 0 Then
                    lstGroups.SelectedIndex = gIdx - 1

                    'jam 2021-04-02
                    'Session("MMGroup") = Request.QueryString("Group")

                    Session("MMGroup") = vCountry
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("PopCohort_Grouping") = True Then
                    If Not (Session("MMGroup") Is Nothing) Then
                        lstGroups.SelectedValue = Session("MMGroup")
                    End If
                    lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
                Else
                    mnuGroups_Click(wmMain, New Coalesys.WebMenu.ServerItemClickEventArgs(wmMain.Groups("mnuGroups").Items(0)))
                End If
            End If
        End If
        'jrs 2012/05/03
        'If Not IsNothing(Request.QueryString("Year")) Then
        'jam 2021/04/02
        If Not IsNothing(Request.QueryString("Year")) Or Not IsNothing(vYear) Then
            'jam 2021/04/02
            'If Session("PopCohort_tempyear") <> Request.QueryString("Year") Then
            'jrs 2024/07/08 added Not IsNothing(vYear) andaslo
            If Not IsNothing(vYear) AndAlso Session("PopCohort_tempyear") <> vYear Then
                'jam 2021/04/02
                'Session("PopCohort_tempyear") = Request.QueryString("Year")
                Session("PopCohort_tempyear") = vYear
                If Session("PopCohort_tempyear") < Session("FirstYearSce") Then Session("PopCohort_tempyear") = Session("FirstYearSce")
                If Session("PopCohort_tempyear") > Session("PopCohort_year_run") Then Session("PopCohort_tempyear") = Session("PopCohort_year_run")
                'jrs 2013/01/02
                Session("PYear") = Session("PopCohort_tempyear") - Session("FirstYearSce") + 1
                'jrs 2012/06/27
                If Session("PopCohort_Grouping") = True Then
                    If Not (Session("MMGroup") Is Nothing) Then
                        lstGroups.SelectedValue = Session("MMGroup")
                    End If
                    lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
                End If
            End If
        End If
        'jrs 2012/05/05
        'If Not IsNothing(Request.QueryString("File")) Then
        'jam 2021/04/02
        If Not IsNothing(Request.QueryString("File")) Or Not IsNothing(vScenario) Then
            'jam 2021/04/02
            'If Session("POPCG_Casename") <> Request.QueryString("File") Then
            If Session("POPCG_Casename") <> vScenario Then
                'Session("POPCG_Case_of_Work") = Case_of_Work
                'jam 2021/04/02
                'CaseName = Request.QueryString("File")

                CaseName = vScenario
                'jrs 2014/02/21
                'If Request.QueryString("File") = "Base Case" Then
                'jam 2021/04/02
                If vScenario = "Base Case" Then
                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                        'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                        'JAM 2019-01-14
                        If Session("mnuSQLite") Is Nothing Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                        Else

                            If Not Session("mnuSQLite") = "True" Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                            Else
                                'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.db"

                                Session("SQLiteDB") = "IFsPop.db"

                                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                            End If
                        End If


                    Else
                        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                        'JAM 2019-01-14
                        If Session("mnuSQLite") Is Nothing Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                        Else

                            If Not Session("mnuSQLite") = "True" Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                            Else
                                'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"
                                'JAM 2020-05-04 
                                Session("SQLiteDB") = "IFsPopHist.db"

                                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                            End If
                        End If



                    End If
                    'jam 2021/04/02
                    'ElseIf Request.QueryString("File") = "Work Case" Then
                ElseIf vScenario = "Work Case" Then
                    'JAM 2019-11-17
                    If Session("mnuSQLite") Is Nothing Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

                    Else

                        'jam 2021/04/05 commented in order to merge previous change
                        'If Not Session("mnuSQLite") Then

                        If Not Session("mnuSQLite") = "True" Then

                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"
                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "ifsPopWork.db"

                            'JAM 2020-05-04 
                            Session("SQLiteDB") = "ifsPopWork.db"

                            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                        End If
                    End If


                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb" 
                    Case_of_Work = 0
                    'jam 2021/04/02
                    'ElseIf Request.QueryString("File") = "Historical Forecast" Then
                ElseIf vScenario = "Historical Forecast" Then
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                    'JAM 2019-01-14
                    If Session("mnuSQLite") Is Nothing Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                    Else

                        If Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                            'JAM 2020-05-04 
                            Session("SQLiteDB") = "IFsPopHist.db"

                            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                        End If
                    End If


                    Case_of_Work = 1
                Else
                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"

                    'JAM 2019-01-14
                    If Session("mnuSQLite") Is Nothing Then
                        Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"

                    Else

                        If Not Session("mnuSQLite") = "True" Then
                            Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".mdb"
                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & CaseName & ".db"


                            'JAM 2020-05-04 
                            Session("SQLiteDB") = "IFsPop" & CaseName & ".db"

                            Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                        End If
                    End If


                    If Dir(Session("db2Path")) = "" Then
                        CaseName = "Base Case"
                        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                            'JAM 2019-01-14
                            If Session("mnuSQLite") Is Nothing Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

                            Else

                                If Not Session("mnuSQLite") = "True" Then
                                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                                Else
                                    ' Session("db2Path") = Session("PutFdrvRun") & "IFsPop.db"

                                    'JAM 2020-05-04 
                                    Session("SQLiteDB") = "IFsPop.db"

                                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")


                                End If
                            End If


                        Else
                            'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                            'JAM 2019-01-14
                            If Session("mnuSQLite") Is Nothing Then
                                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

                            Else

                                If Not Session("mnuSQLite") = "True" Then
                                    Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                                Else
                                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                                    'JAM 2020-05-04 
                                    Session("SQLiteDB") = "IFsPopHist.db"

                                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                                End If
                            End If

                        End If
                    End If
                    'jrs 2014/10/15
                    For i = 2 To wmMain.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
                        If wmMain.Groups("Change Display").Items("Change Scenario").Group.Items(i).Caption = CaseName Then
                            Case_of_Work = i
                        End If
                    Next

                End If

                'jrs 2024/04/24
                If Session("mnuSQLite") = "True" Then
                    If Not Session("SQLiteDB") Is Nothing Then
                        vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                    Else
                        vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                    End If

                    'jam 2025/05/30 Changing connection mode
                    'vSQLiteCon = New SQLiteConnection(vConnString) '& "New=true;"
                    'vSQLiteCon.Open()

                    vSQLiteCon = OpenJournalSQLite(vConnString)



                End If

                'jrs 2014/02/21 uncheck previous sce and check new one
                'jam 2021/03/31
                wmMain.Groups("Change Display").Items("Change Scenario").Group.Items(Session("POPCG_Casename")).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                Session("POPCG_Casename") = CaseName
                wmMain.Groups("Change Display").Items("Change Scenario").Group.Items(Session("POPCG_Casename")).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"

                Session("PopCohort_year_run") = FindLastYear()
                Session("PopCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("PopCohort_tempyear"))
                Session("FirstYearSce") = FindFirstYear()

                If Session("PopCohort_year_run") <= FirstYearForecast Then
                    Session("PopCohort_tempyear") = FirstYearHistory
                ElseIf Session("PopCohort_tempyear") < FirstYearForecast Then
                    Session("PopCohort_tempyear") = FirstYearForecast
                End If
                'jrs 2024/04/24
                ''jrs 2012/06/27
                'If Session("PopCohort_Grouping") = True Then
                '    lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
                'End If

                'jrs 2008/09/16
                Session("POPCG_Case_of_Work") = Case_of_Work

                'jrs 2024/04/24 moved it up to be there for FindFirstYear
                ''jrs 2024/03/07
                'If Not Session("mnuSQLite") Is Nothing Then
                '        If Session("mnuSQLite") Then
                '            'JAM 2020-05-04
                '            If Not Session("SQLiteDB") Is Nothing Then 'JAM 2020-04-13
                '                vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";" '"ifsPopWork.db;"
                '            Else
                '                vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPopWork.db;"
                '            End If

                '            vSQLiteCon.Close()
                '            vSQLiteCon = New SqliteConnection(vConnString) '& "New=true;"
                '            vSQLiteCon.Open()
                '        End If
                '    End If

            End If
        End If

        'jrs 2014/01/31
        If Not Page.IsPostBack Then
            ReDim Session("dbSCEs")(3)
            ReDim Session("year_runs")(3)
            ReDim Session("tempyears")(3)
            ReDim Session("FirstYearSces")(3)
            ReDim Session("CaseNames")(3)
            For l = 0 To 3
                Session("dbSCEs")(l) = Session("db2Path")
                Session("CaseNames")(l) = CaseName
            Next l
            Session("bCompare") = False
            For l = Session("FirstYearSce") To Session("PopCohort_year_run")
                cmbYr1.Items.Add(l)
                cmbYr2.Items.Add(l)
                cmbYr3.Items.Add(l)
                cmbYr4.Items.Add(l)
            Next
            cmbYr1.SelectedIndex = 0
            If cmbYr2.Items.Count > 5 Then
                cmbYr2.SelectedIndex = 5
            End If
            If cmbYr3.Items.Count > 10 Then
                cmbYr3.SelectedIndex = 10
            End If
            If cmbYr4.Items.Count > 15 Then
                cmbYr4.SelectedIndex = 15
            End If
        End If


        'KIA 63008 ADD THE FOLLOWING IF BLOCK
        If Not Session("cNumber") Is Nothing And Not Session("bCompare") Then 'jrs 2014/02/14 added and not bCompare
            lstGroups.Visible = False
            cntrylist.Visible = True
            cntrylist.SelectedIndex = CInt(Session("cNumber")) - 1
            Session.Remove("cNumber")
            cntrylist_SelectedIndexChanged(Me, New System.EventArgs)
        Else 'jrs 2024/04/24
            If Not Page.IsPostBack Then
                Graph_Draw(Session("PopCohort_GraphType"))
            End If
            PackJScritpBuilder()
        End If


ErrHandler02:

        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) And Not Page.IsPostBack Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

        Exit Sub
    End Sub


    Private Sub lstGroups_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstGroups.SelectedIndexChanged
        'JRS 07/26/02 GROUP
        Session("PopCohort_regionname") = Support.ListBoxSelectedIndexCollectionManager.GetText(lstGroups)
        'JRS 08/09/02
        If Session("PopCohort_regionname") = "" Then Session("PopCohort_regionname") = lstGroups.Items(0).Text
        lstGroups.Visible = False
        'jrs 2014/01/29
        Session("GroupTot") = Nothing
        If Session("mnuFixCteScTime") Then
            Session("bReadGrpMaxs") = True
        End If
        'jrs 2014/02/14
        If Session("bCompare") = False Then
            Graph_Draw((Session("PopCohort_GraphType")))
        End If
    End Sub

    Public Sub MnuBaseCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'MnuBaseCase.Checked = True 
        'mnuWorkCase.Checked = False 
        On Error GoTo ErrHandler03

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'If objEA.Item.Caption = "Base Case" Then
        'jrs 2006/02/24 objSender.Groups("Change Scenario").Items("Base Case").IconImage = "checkbox.gif"
        'jrs 2006/02/24 objSender.Groups("Change Scenario").Items("Work Case").IconImage = "blank.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        'jrs 2008/06/06
        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Next

        Case_of_Work = 0
        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        'jrs 2008/06/18
        If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            'Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            'JAM 2020-01-14
            If Session("mnuSQLite") Is Nothing Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"

            Else

                If Not Session("mnuSQLite") = "True" Then
                    Session("db2Path") = Session("PutFdrvRun") & "IFsPop.mdb"
                Else
                    'Session("db2Path") = Session("PutFdrvRun") & "ifsPop.db"

                    'JAM 2020-05-04 
                    Session("SQLiteDB") = "ifsPop.db"

                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                End If
            End If

        Else
            'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

            'JAM 2020-01-14
            If Session("mnuSQLite") Is Nothing Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

            Else

                If Not Session("mnuSQLite") = "True" Then
                    Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
                Else
                    'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                    'JAM 2020-05-04 
                    Session("SQLiteDB") = "IFsPopHist.db"

                    Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

                End If
            End If


        End If


        'mti WebForm Changes From July 05, 2004
        'popConn = CreateConnection(Session("db2Path"))
        'Session("popConn") = popConn
        'mti WebForm Changes From July 05, 2004

        'jrs 2008/06/16 changed from FirstYearForecast to FirstYear
        'jrs 2008/06/05
        If Session("PopCohort_tempyear") < Session("FirstYear") Then
            Session("PopCohort_tempyear") = Session("FirstYear")
        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And Session("PopCohort_tempyear") > LastYearHistory Then
            Session("PopCohort_tempyear") = FirstYearHistory
        End If
        Session("FirstYearSce") = Session("FirstYear")

        '    Session("PopCohort_year_run") = Session("LastYear")%
        '    Session("PopCohort_tempyear") = Session("FirstYear")%
        '    Session("PopCohort_tempregion") = 1
        '    Session("PopCohort_regionname") = GRPS$(1, 1)
        '    Session("PopCohort_GraphType") = 1
        CaseName = "Base Case"
        Session("POPCG_Casename") = CaseName

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'End If
        If Session("PopCohort_tempyear") = Session("FirstYear") Then objSender.Groups("Time").Items("Regress").Enabled = False
        If Session("PopCohort_tempyear") < Session("LastYear") Then objSender.Groups("Time").Items("Advance").Enabled = True

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("PopCohort_GraphType")))
ErrHandler03:
        Exit Sub
    End Sub

    Public Sub mnuchngcntry_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'JRS 07/26/02 GROUP
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/04/02
        If objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") Then '"checkbox.gif" Then

            'If mnuGroups.Text = "Use &Groups" Then
            cntrylist.Visible = True
            lstGroups.Visible = False
        Else
            lstGroups.Visible = True
            cntrylist.Visible = False
        End If
    End Sub

    Public Sub mnuchngyr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        lstYear.Visible = True
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnufertgph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'jrs 2014/06/16
        Dim bCompare As Boolean
        bCompare = Session("bCompare")
        Session("bCompare") = False

        Session("PopCohort_GraphType") = 2

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("PopCohort_GraphType")))

        'jrs 2014/06/16
        If bCompare Then
            mnuCompare_Click()
        End If
    End Sub

    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/03/06
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If objEA.Item.Caption = "Groups" Then
            'jam 2021/03/31
            objSender.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            '"Change Display"
            If Session("bCompare") = False Then
                objSender.Groups("Change Display").Items(0).Caption = "Change Groups" '"Change Countries/Regions"
            End If
            Session("PopCohort_Grouping") = True

            'jam 2021/04/02
            Session("IsGroup") = True


            'jrs 2014/02/14
            If lstGroups.SelectedIndex = -1 Then
                lstGroups.SelectedValue = "World"
            End If
            lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
            cntrylist.Visible = False
            lstYear.Visible = False
            'jrs 2014/02/14
            Session("sDefReg") = Session("MMCountry")
            'jrs 2011/05/05
            Session.Remove("MMCountry")
            'jrs 2011/09/22
            Session("MMGroup") = Session("PopCohort_regionname")

            'jam 2021/04/02
            'Support.FormManager.Show(Me, "frm_PopCohortDisp.aspx?Group=" & PrepareForURL(Session("PopCohort_regionname")))


            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")


            vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", PopPrepareForFriendlyURL(Session("PopCohort_regionname")))

            Response.Redirect(vurl)



        Else
            objSender.Groups("Set Group or Country/Region").Items("Countries/Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            objSender.Groups("Set Group or Country/Region").Items("Groups").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            If Session("bCompare") = False Then
                objSender.Groups("Change Display").Items(0).Caption = "Change Countries/Regions"
            End If
            Session("PopCohort_Grouping") = False


            'jam 2021/04/02
            Session("IsGroup") = False


            'jrs 2014/02/06
            Dim sCtryCode As String = "AF"
            If Session("sDefReg") > "" Then
                sCtryCode = Session("sDefReg")
            ElseIf Session("PopCohort_tempregion") > 0 And cntrylist.SelectedIndex = -1 And IFsRegs.Item(Session("PopCohort_regionname")).Index = Session("PopCohort_tempregion") Then
                cntrylist.SelectedIndex = Session("PopCohort_tempregion") - 1
                sCtryCode = IFsRegs.Item(Session("PopCohort_regionname")).ISO3166_2Code
            End If

            cntrylist_SelectedIndexChanged(cntrylist, New System.EventArgs)
            lstGroups.Visible = False
            lstYear.Visible = False

            'jam 2021/04/02
            'Support.FormManager.Show(Me, "frm_PopCohortDisp.aspx?Country=" & sCtryCode)


            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
            vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", sCtryCode)
            Response.Redirect(vurl)
        End If


    End Sub

    'jrs 2024/09/06
    Public Sub mnuHideNonCoreGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        'jam 2021/03/06
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If Session("PopCohort_Grouping") = True Then
            'lstGroups_SelectedIndexChanged(lstGroups, New System.EventArgs)
            cntrylist.Visible = False
            lstYear.Visible = False
            Session("sDefReg") = Session("MMCountry")
            Session.Remove("MMCountry")
            Session("MMGroup") = Session("PopCohort_regionname")

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
            vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", PopPrepareForFriendlyURL(Session("PopCohort_regionname")))

            Response.Redirect(vurl)

        Else
            Dim sCtryCode As String = "AF"
            If Session("sDefReg") > "" Then
                sCtryCode = Session("sDefReg")
            ElseIf Session("PopCohort_tempregion") > 0 And cntrylist.SelectedIndex = -1 And IFsRegs.Item(Session("PopCohort_regionname")).Index = Session("PopCohort_tempregion") Then
                cntrylist.SelectedIndex = Session("PopCohort_tempregion") - 1
                sCtryCode = IFsRegs.Item(Session("PopCohort_regionname")).ISO3166_2Code
            ElseIf Session("PopCohort_tempregion") > 0 And IFsRegs.Item(Session("PopCohort_regionname")).Index = Session("PopCohort_tempregion") Then
                sCtryCode = IFsRegs.Item(Session("PopCohort_regionname")).ISO3166_2Code
            End If

            cntrylist_SelectedIndexChanged(cntrylist, New System.EventArgs)
            lstGroups.Visible = False
            lstYear.Visible = False

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
            vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", sCtryCode)
            Response.Redirect(vurl)
        End If


    End Sub


    '==========================frm_CohoDisp=============================
    Public Sub mnumortgph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'jrs 2014/06/16
        Dim bCompare As Boolean
        bCompare = Session("bCompare")
        Session("bCompare") = False

        Session("PopCohort_GraphType") = 3
        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If
        Graph_Draw((Session("PopCohort_GraphType")))

        'jrs 2014/06/16
        If bCompare Then
            mnuCompare_Click()
        End If

    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnupopgph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'jrs 2014/06/16
        Dim bCompare As Boolean
        bCompare = Session("bCompare")
        Session("bCompare") = False

        Session("PopCohort_GraphType") = 1
        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If
        Graph_Draw((Session("PopCohort_GraphType")))

        'jrs 2014/06/16
        If bCompare Then
            mnuCompare_Click()
        End If
    End Sub


    '==========================frm_CohoDisp=============================
    Public Sub mnupoppyr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------
        'jrs 2014/06/16
        Dim bCompare As Boolean
        bCompare = Session("bCompare")
        Session("bCompare") = False

        'mti june, 2002 - Add this procedure
        Session("PopCohort_GraphType") = 4
        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If
        Graph_Draw((Session("PopCohort_GraphType")))

        'jrs 2014/06/16
        If bCompare Then
            mnuCompare_Click()
        End If

    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuShowData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '--------------------------------------------------------------

        'mti june, 2002 - Add this procedure
        'frameData.BringToFront() 
        frameData.Visible = True
        'jrs 2008/06/25
        grdcohort.Visible = True
        lbldata.Visible = True
        hfTitle.Value = lbldata.Text
        'jrs 2011/11/16
        lblJS.Visible = False
    End Sub

    '==========================frm_CohoDisp=============================
    Public Sub mnuWorkCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '--------------------------------------------------------------
        On Error GoTo ErrHandler04
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs
        'jam 2021/04/02
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        'jrs 2008/06/06
        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            'jam 2021/04/02
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Next

        Case_of_Work = 1
        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

        'JAM 2020-01-14
        If Session("mnuSQLite") Is Nothing Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"

        Else

            If Not Session("mnuSQLite") = "True" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPopWork.mdb"
            Else
                'Session("db2Path") = Session("PutFdrvRun") & "ifsPopWork.db"


                'JAM 2020-05-04 
                Session("SQLiteDB") = "ifsPopWork.db"

                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

            End If
        End If


        Session("PopCohort_year_run") = FindLastYear()
        Session("PopCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("PopCohort_tempyear"))
        If Session("PopCohort_tempyear") < FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearForecast
        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And Session("PopCohort_tempyear") > LastYearHistory Then
            Session("PopCohort_tempyear") = FirstYearHistory
        End If
        Session("FirstYearSce") = Session("FirstYear")

        CaseName = "Working Scenario"
        Session("POPCG_Casename") = CaseName
        If Session("PopCohort_tempyear") = Session("FirstYear") Then
            'mnuRegress.Enabled = False
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("PopCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("PopCohort_GraphType")))

ErrHandler04:
        Exit Sub

    End Sub

    '==========================frm_CohoDisp=============================
    Private Sub FillDataGrid(ByRef index As Short)
        Dim cc As Short
        ''--------------------------------------------------------------

        'jrs 2008/06/24
        'This only works for 1-dimensional arrays
        'grdcohort.DataSource = Session("PC_TempDataBase")            
        'grdcohort.DataBind()

        grdcohort.Columns.Clear()
        Dim myCol As TMSWebControls.Column

        'jrs 2026/01/26
        ''jrs 2008/04/17
        'If index = 3 Then
        '    grdcohort.TotalRows = 23
        '    grdcohort.RowCount = 23
        'Else
        '    grdcohort.TotalRows = 22
        '    grdcohort.RowCount = 22
        'End If
        If index = 3 Then
            grdcohort.TotalRows = NCohorts + 2
            grdcohort.RowCount = NCohorts + 2
        Else
            grdcohort.TotalRows = NCohorts + 1
            grdcohort.RowCount = NCohorts + 1
        End If

        If index = 2 Then 'Fertility
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            myCol.Width = 100 '1500
            grdcohort.Columns.Add(myCol)

            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            myCol.Width = 200 '2500
            grdcohort.Columns.Add(myCol)

        Else
            myCol = New TMSWebControls.Column
            myCol.WidthType = TMSWebControls.WType.Absolute
            myCol.Width = 100 '1100
            grdcohort.Columns.Add(myCol)

            For cc = 1 To 3
                myCol = New TMSWebControls.Column
                myCol.WidthType = TMSWebControls.WType.Absolute
                myCol.Width = 150 '1300
                grdcohort.Columns.Add(myCol)
            Next
        End If


        ''grdcohort.CtlRefresh() 

        'jrs 2008/04/17 added infants
        Dim FstCoh As Integer, rr As Integer
        If index = 3 Then
            FstCoh = 0
        Else
            FstCoh = 1
        End If

        For cc = NCohorts To FstCoh Step -1

            'jrs 2008/04/17 added infants, added + 1
            If index = 3 Then
                rr = cc + 1
            Else
                rr = cc
            End If

            If cc = NCohorts Then
                grdcohort(0, rr) = NAges.ToString & "+" 'jrs 2026/01/26
            ElseIf cc = 0 Then 'jrs 2008/04/17
                grdcohort(0, rr) = "Infants"
            ElseIf cc = 1 And index = 3 Then
                grdcohort(0, rr) = "1-4"
                'grdcohort.CellAlignment = flexAlignLeftCenter
            Else
                grdcohort(0, rr) = Str(cc * 5 - 5) & "-" & Str(cc * 5 - 1)
            End If

            'jrs 2026/01/26 added Round(, 3)
            'jrs 2013/01/03
            'cdv 2021/01/12 ****START**** Changes order of values based on gender order, but doesn't affect if fertility
            If index = 2 Then
                If IsDBNull(Session("PC_TempDataBase")(cc, 1)) Then
                    grdcohort(1, rr) = ""
                Else
                    grdcohort(1, rr) = Math.Round(Session("PC_TempDataBase")(cc, 1), 3)
                End If
            Else
                If bMaleFirst Then
                    If IsDBNull(Session("PC_TempDataBase")(cc, 1)) Then
                        grdcohort(1, rr) = ""
                    Else
                        grdcohort(1, rr) = Math.Round(Session("PC_TempDataBase")(cc, 1), 3)
                    End If
                Else
                    If IsDBNull(Session("PC_TempDataBase")(cc, 2)) Then
                        grdcohort(1, rr) = ""
                    Else
                        grdcohort(1, rr) = Math.Round(Session("PC_TempDataBase")(cc, 2), 3)
                    End If
                End If
            End If
            'cdv 2021/01/12 ****END****
            If index <> 2 Then 'not fertility
                'jrs 2013/01/02
                'cdv 2021/01/12 ****START**** Changes order of values based on gender order
                If bMaleFirst Then
                    If IsDBNull(Session("PC_TempDataBase")(cc, 2)) Then
                        grdcohort(2, rr) = ""
                    Else
                        grdcohort(2, rr) = Math.Round(Session("PC_TempDataBase")(cc, 2), 3)
                    End If
                Else
                    If IsDBNull(Session("PC_TempDataBase")(cc, 1)) Then
                        grdcohort(2, rr) = ""
                    Else
                        grdcohort(2, rr) = Math.Round(Session("PC_TempDataBase")(cc, 1), 3)
                    End If
                End If
                'cdv 2021/01/12 ****END****

                If index = 1 Then
                    grdcohort(3, rr) = Math.Round(Session("PC_TempDataBase")(cc, 1) + Session("PC_TempDataBase")(cc, 2), 3)
                Else
                    'jrs 2013/01/03
                    If IsDBNull(Session("PC_TempDataBase")(cc, 1)) Or IsDBNull(Session("PC_TempDataBase")(cc, 2)) Then
                        grdcohort(3, rr) = ""
                    Else
                        grdcohort(3, rr) = Math.Round((Session("PC_TempDataBase")(cc, 1) * Session("POPM")(cc) +
                        Session("PC_TempDataBase")(cc, 2) * Session("POPF")(cc)) /
                        (Session("POPM")(cc) + Session("POPF")(cc)), 3)
                    End If
                End If
            End If

        Next cc

        grdcohort(0, 0) = "Age Cohort"
        If index = 2 Then
            grdcohort(1, 0) = "Fertility"
            Exit Sub
        Else
            'cdv 2021/01/12 
            If bMaleFirst Then
                grdcohort(1, 0) = "Male"
                grdcohort(2, 0) = "Female"
                grdcohort(3, 0) = "Total"
            Else
                grdcohort(1, 0) = "Female"
                grdcohort(2, 0) = "Male"
                grdcohort(3, 0) = "Total"
            End If
        End If

    End Sub


    Private Sub LoadCountries(ByRef GroupName As String)
        'jrs 2012/06/27
        If GroupName = "" Then
            GroupName = GroupNames(1)
        End If
        'JRS 07/26/02 GROUP
        Dim valtblG As DataTable
        Dim resG As DataTable
        Dim resR As DataTable
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " WHERE GroupName = """ & GroupName & """", "", resG, "")
        Dim GroupNum As Short
        GroupNum = resG.Rows(0).Item("GroupNum")
        '    Dim valtbl As Recordset
        'SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Grouping, "RegionNo", valtblG, "")
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Grouping & " WHERE GroupNo = " & GroupNum & " Order by RegionNo", "", valtblG, "") 'jrs 2014/02/14
        Dim iTotal As Short
        iTotal = 0
        Dim i As Integer
        Dim j As Integer
        i = 0
        '   Load Group Member Names List Box
        'JRS 2/15/02 For ICount% = 1 To valtblG.RecordCount
        Do While Not i = valtblG.Rows.Count
            'JRS 2/15/02 For JCount% = 1 To valtblG.Fields("TotalGroups")
            'JRS 2/15/02 TmpStr$ = "Group" + Trim(Str(JCount%))
            'JRS 2/15/02 If valtblG.Fields(TmpStr$) = GroupNum Then
            'jrs 2014/02/14 
            'If valtblG.Rows(i).Item("GroupNo") = GroupNum Then
            '    SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT DISTINCT Country, NewRegion FROM " & Regionalization & ", " & Grouping & " WHERE NewRegion = RegionNo AND Region = """ & valtblG.Rows(i).Item("Region") & """", "", resR, "")
            '    j = 0
            '    Do While Not j = resR.Rows.Count
            '        iTotal = iTotal + 1
            '        'jrs 2014/02/14 Session("CountryGroup")(iTotal) = resR.Rows(j).Item("Country")
            '        Session("RegionIdGroup")(iTotal) = resR.Rows(j).Item("NewRegion")
            '        j = j + 1
            '    Loop

            'End If
            Session("RegionIdGroup")(i + 1) = valtblG.Rows(i).Item("RegionNo")
            'JRS 2/15/02 Next JCount%
            i = i + 1
            'JRS 2/15/02 Next ICount%
        Loop

        Session("GroupTot") = i 'jrs 2014/02/14 iTotal
    End Sub

    Sub ItemClickHandlerMain(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'jam 2021/04/03
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue" 'jrs 2008/06/27
                mnuExit_Click()
            Case "Main Menu" 'jrs 2011/05/02
                Session("DisplayParent") = "frm_MainMenu.aspx"
                mnuExit_Click()
            Case "Scenario Files"
            Case "New"
                    'mnuNew_Click(sender, e)
            Case "Age Distribution"
                'jrs 2014/06/16
                ''jrs 2014/02/13
                'Session("bCompare") = False
                'pnlCompare.Visible = False
                'PackJScritpBuilder()
                'wmMain.Groups("Change Display").ShowMenuBarItem = True
                'wmMain.Groups("Time").ShowMenuBarItem = True
                'wmMain.Groups("mnuGroups").ShowMenuBarItem = True

                mnupopgph_Click(sender, e)
            Case "Fertility Distribution"
                'jrs 2014/06/16
                ''jrs 2014/02/13
                'Session("bCompare") = False
                'pnlCompare.Visible = False
                'PackJScritpBuilder()
                'wmMain.Groups("Change Display").ShowMenuBarItem = True
                'wmMain.Groups("Time").ShowMenuBarItem = True
                'wmMain.Groups("mnuGroups").ShowMenuBarItem = True

                mnufertgph_Click(sender, e)
            Case "Mortality Distribution"
                'jrs 2014/06/16
                ''jrs 2014/02/13
                'Session("bCompare") = False
                'pnlCompare.Visible = False
                'PackJScritpBuilder()
                'wmMain.Groups("Change Display").ShowMenuBarItem = True
                'wmMain.Groups("Time").ShowMenuBarItem = True
                'wmMain.Groups("mnuGroups").ShowMenuBarItem = True

                mnumortgph_Click(sender, e)
            Case "Population Pyramid"
                'jrs 2014/06/16
                ''jrs 2014/02/13
                'Session("bCompare") = False
                'pnlCompare.Visible = False
                'PackJScritpBuilder()
                'wmMain.Groups("Change Display").ShowMenuBarItem = True
                'wmMain.Groups("Time").ShowMenuBarItem = True
                'wmMain.Groups("mnuGroups").ShowMenuBarItem = True

                mnupoppyr_Click(sender, e)
            Case "Compare Countries", "Compare Groups" 'jrs 2014/02/11
                mnuCompare_Click()
            Case "Show Numbers"
                mnuShowData_Click(sender, e)
            Case "Use Groups"
            Case "Groups", "Countries/Regions"
                mnuGroups_Click(sender, e)
                    'SetGroupReg(sender, e,objEA.Item.Caption)
            Case "Hide Non Core Groups" 'jrs 2024/09/06
                If Session("bCheckHideNonCoreGroups") = 0 Then
                    Session("bCheckHideNonCoreGroups") = 1
                Else
                    Session("bCheckHideNonCoreGroups") = 0
                End If
                mnuHideNonCoreGroups_Click(sender, e)
            Case "Change Display"
            Case "Change Countries/Regions", "Change Groups" ', "Change Region"
                    'jrs 2011/11/15 mnuchngcntry_Click(sender, e)
            Case "Set Year"
                    'jrs 2011/11/15 mnuchngyr_Click(sender, e)
            Case "Change Scenario"
                    'jrs 2012/05/04
                    'Case "Base Case"
                    '    MnuBaseCase_Click(sender, e)
                    'Case "Work Case"
                    '    mnuWorkCase_Click(sender, e)
                    'Case "Historical Forecast" 'jrs 2008/06/16
                    '    mnuHistSce_Click(objSender)
                    'Case "Advance"
                    '    mnuAdvance_Click(sender, e)
                    'Case "Regress"
                    '    mnuRegress_Click(sender, e)
            Case "Fix Constant Scale across Time and Regions" 'jrs 2014/02/14
                mnuFixCtSc_Click(sender, e)
            Case "Fix Constant Scale across Time" 'jrs 2014/02/14
                mnuFixCteScTime_Click(sender, e)
            Case "Fix Constant Scale Manually" 'jrs 2014/02/14
                mnuFixCtScMan_Click(sender, e)
            Case "Use Percent of Population" 'jrs 2014/02/14
                mnuUsePctPop_Click(sender, e)
            Case "Print"
            Case "Save"
            Case "About" 'jrs 2025/02/04 Population"
                mnuAboutPopulation_Click(sender, e)
            Case Else
                'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
                If Not (vSQLiteCon Is Nothing) Then
                    vSQLiteCon.Close()
                    vSQLiteCon.Dispose()
                End If

                'jrs 2011/11/14
                Dim idx As Integer, sUrl As String, sParY As String, sParF As String, sParG As String, pos As Integer

                'If Not IsNothing(Request.QueryString("Year")) Then
                'jam 2021/04/02
                If Not IsNothing(Session("vYear")) Then
                    'jam 2021/04/02
                    'sParY = "&Year=" & Request.QueryString("Year")
                    sParY = Session("vYear")
                Else
                    sParY = ""
                End If
                'If Not IsNothing(Request.QueryString("File")) Then
                'jam 2021/04/02
                If Not IsNothing(Session("vScenario")) Then
                    'jam 2021/04/02
                    'sParF = "&File=" & Request.QueryString("File")
                    sParF = Session("vScenario")
                Else
                    sParF = ""
                End If
                'If Not IsNothing(Request.QueryString("Country")) Then
                'jam 2021/04/02
                If Not IsNothing(Session("vCountry")) And Not Session("IsGroup") Then
                    'jam 2021/04/02
                    'sParG = "Country=" & Request.QueryString("Country")
                    sParG = Session("vCountry")
                    'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2012/06/27
                    'jam 021/04/02
                ElseIf Not IsNothing(Session("vCountry")) And Session("IsGroup") Then
                    'sParG = "Group=" & Request.QueryString("Group")
                    sParG = Session("vCountry")
                ElseIf Session("PopCohort_Grouping") = False Then
                    sParG = "Country=" & IFsRegs.Item(Session("PopCohort_regionname")).ISO3166_2Code
                Else
                    sParG = "Group=" & Session("PopCohort_regionname")
                End If


                If objEA.Item.UserData = "Reg" Then
                    'Server Transfer doesn't change the URL, which we want to allow to be saved by user
                    'Response.Redirect("frm_PopCohortDisp.aspx?Country=" & IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code & sParY & sParF)

                    'jam 2021/04/03
                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", IFsRegs.Item(objEA.Item.Caption).ISO3166_2Code, sParY, sParF)
                    Response.Redirect(vurl)


                ElseIf objEA.Item.UserData = "Grp" Then
                    'Response.Redirect("frm_PopCohortDisp.aspx?Group=" & PrepareForURL(objEA.Item.Caption) & sParY & sParF)
                    'jam 2021/04/03
                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", PopPrepareForFriendlyURL(objEA.Item.Caption), sParY, sParF)
                    Response.Redirect(vurl)

                ElseIf objEA.Item.UserData = "Yr" Then
                    'jrs 2012/05/4
                    'If IsNumeric(objEA.Item.Caption) Then
                    '    lstYear.SelectedIndex = CInt(CInt(Trim(objEA.Item.Caption)) - Session("FirstYear"))
                    '    lstYear_SelectedIndexChanged(objSender, New EventArgs)
                    'End If

                    'jam 2021/04/03
                    'Response.Redirect("frm_PopCohortDisp.aspx?" & sParG & "&Year=" & objEA.Item.Caption & sParF)

                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", sParG, objEA.Item.Caption, sParF)
                    Response.Redirect(vurl)



                ElseIf objEA.Item.Caption = "Advance" Then
                    If Session("PopCohort_tempyear") Mod 5 = 0 Then
                        Session("PopCohort_tempyear") = Session("PopCohort_tempyear") + 5
                    ElseIf (Session("PopCohort_tempyear") + 1) Mod 5 = 0 Then
                        Session("PopCohort_tempyear") = Session("PopCohort_tempyear") + 1
                    Else
                        While Session("PopCohort_tempyear") Mod 5 <> 0
                            Session("PopCohort_tempyear") = Session("PopCohort_tempyear") + 1
                        End While
                    End If

                    If Session("PopCohort_tempyear") > Session("PopCohort_year_run") Then Session("PopCohort_tempyear") = Session("PopCohort_year_run")

                    'jam 2021/04/03
                    'Response.Redirect("frm_PopCohortDisp.aspx?" & sParG & "&Year=" & Session("PopCohort_tempyear") & sParF)

                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", sParG, Session("PopCohort_tempyear"), sParF)
                    Response.Redirect(vurl)



                ElseIf objEA.Item.Caption = "Regress" Then
                    If Session("PopCohort_tempyear") Mod 5 = 0 Then
                        Session("PopCohort_tempyear") = Session("PopCohort_tempyear") - 5
                    ElseIf (Session("PopCohort_tempyear") - 1) Mod 5 = 0 Then
                        Session("PopCohort_tempyear") = Session("PopCohort_tempyear") - 1
                    Else
                        While Session("PopCohort_tempyear") Mod 5 <> 0
                            Session("PopCohort_tempyear") = Session("PopCohort_tempyear") - 1
                        End While
                    End If
                    If Session("PopCohort_tempyear") < Session("FirstYearSce") Then Session("PopCohort_tempyear") = Session("FirstYearSce")

                    'jam 2021/04/03
                    'Response.Redirect("frm_PopCohortDisp.aspx?" & sParG & "&Year=" & Session("PopCohort_tempyear") & sParF)

                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", sParG, Session("PopCohort_tempyear"), sParF)
                    Response.Redirect(vurl)

                ElseIf objEA.Item.Caption = "Historical Forecast" Then

                    'jam 2021/04/03
                    'Response.Redirect("frm_PopCohortDisp.aspx?" & sParG & sParY & "&File=Historical Forecast")

                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", sParG, sParY, "Historical Forecast")
                    Response.Redirect(vurl)

                Else
                    'jrs 2012/05/04 mnuSce_Click(objEA.Item.Caption, objSender)

                    'jam 2021/04/03
                    'Response.Redirect("frm_PopCohortDisp.aspx?" & sParG & sParY & "&File=" & objEA.Item.Caption)

                    Vpath = Request.GetFriendlyUrlFileVirtualPath
                    Vext = Request.GetFriendlyUrlFileExtension
                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_PopCohortDisp.aspx")
                    vurl = FriendlyUrl.Href("~/frm_PopCohortDisp", sParG, sParY, objEA.Item.Caption)
                    Response.Redirect(vurl)

                End If
        End Select

        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

    End Sub

    Private Sub lstYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstYear.SelectedIndexChanged
        '--------------------------------------------------------------

        '  Specify the country selected
        '  ----------------------------
        'jrs 2008/06/06 changed to FirstYearSce
        If lstYear.SelectedIndex = -1 Then
            Session("PopCohort_tempyear") = Session("FirstYearSce") '1
        Else
            Session("PopCohort_tempyear") = Session("FirstYearSce") + lstYear.SelectedIndex 'cntrylist.SelectedIndex + 1
        End If

        If Session("PopCohort_tempyear") < Session("FirstYearSce") Then Session("PopCohort_tempyear") = Session("FirstYearSce")
        If Session("PopCohort_tempyear") > Session("PopCohort_year_run") Then Session("PopCohort_tempyear") = Session("PopCohort_year_run")
        lstYear.Visible = False

        '   Allow or disallow forward and backward movement
        '   -----------------------------------------------

        If Session("PopCohort_tempyear") > Session("FirstYearSce") Then
            wmMain.Groups("Time").Items("Regress").Enabled = True
        Else
            wmMain.Groups("Time").Items("Regress").Enabled = False
        End If
        If Session("PopCohort_tempyear") < Session("PopCohort_year_run") Then
            wmMain.Groups("Time").Items("Advance").Enabled = True
        Else
            wmMain.Groups("Time").Items("Advance").Enabled = False
        End If
        Graph_Draw((Session("PopCohort_GraphType")))
    End Sub

    Private Function FindLastYear()
        'mti July 2006 - new routine
        Dim rsLY As DataTable
        On Error GoTo ErrHandler05
        'JAM 2019-11-17
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)
            If Not rsLY.Rows.Count = 0 Then
                FindLastYear = rsLY.Rows(0).Item("MaxYear")
            Else
                FindLastYear = 0
            End If
        Else
            'SetDataTablefromAccess(Session("db2Path"), "SELECT Max(Years) As MaxYear FROM " + TableName(1), "", rsLY)

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT Max(TimeId) As MaxYear FROM " + TableName(1)
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            FindLastYear = 0
            While (vSQLiteReader.Read())
                FindLastYear = vSQLiteReader("MaxYear")

            End While


        End If
ErrHandler05:
        Exit Function

    End Function

    'jrs 2008/06/06
    Private Function FindFirstYear()
        'mti July 2006 - new routine
        Dim rsFY As DataTable
        On Error GoTo ErrHandler06
        'JAM 2019-11-17
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            SetDataTablefromAccess(Session("db2Path"), "SELECT Min(Years) As MinYear FROM " + TableName(1), "", rsFY)
            If Not rsFY.Rows.Count = 0 Then
                FindFirstYear = rsFY.Rows(0).Item("MinYear")
            Else
                FindFirstYear = 0
            End If
        Else
            'SetDataTablefromAccess(Session("db2Path"), "SELECT Min(Years) As MinYear FROM " + TableName(1), "", rsFY)


            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT Min(TimeId) As MinYear FROM " + TableName(1)
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            FindFirstYear = 0
            While (vSQLiteReader.Read())
                FindFirstYear = vSQLiteReader("MinYear")

            End While


        End If
ErrHandler06:
        Exit Function

    End Function

    'jrs 2008/06/05
    Private Sub LoadSceMenus(ByRef cswmGroup2 As Coalesys.WebMenu.Group)
        'Read all files with format ifsPopXXX.mdb located in runfiles
        Dim sfile As String
        sfile = Dir(Session("PutFdrvRun2") + "\IFsPop*.db")
        Dim cswmItem As Coalesys.WebMenu.Item
        'jrs 2014/01/31
        Dim bLoadSces As Boolean
        bLoadSces = (cmbSce1.Items.Count = 0)
        If bLoadSces Then
            cmbSce1.Items.Add("Base Case")
            cmbSce2.Items.Add("Base Case")
            cmbSce3.Items.Add("Base Case")
            cmbSce4.Items.Add("Base Case")
            cmbSce1.Items.Add("Work Case")
            cmbSce2.Items.Add("Work Case")
            cmbSce3.Items.Add("Work Case")
            cmbSce4.Items.Add("Work Case")
            cmbSce1.Items.Add("Historical Forecast")
            cmbSce2.Items.Add("Historical Forecast")
            cmbSce3.Items.Add("Historical Forecast")
            cmbSce4.Items.Add("Historical Forecast")
            cmbSce1.SelectedIndex = 0
            cmbSce2.SelectedIndex = 0
            cmbSce3.SelectedIndex = 0
            cmbSce4.SelectedIndex = 0
        End If

        Do While (sfile <> "")
            If UCase(sfile) <> "IFSPOPWORK.DB" And UCase(sfile) <> "IFSPOP.DB" And UCase(sfile) <> "IFSPOPHIST.DB" Then 'jrs 2008/06/16
                If Session("IFsRunFiles").item(Mid(Replace(Replace(sfile, ".db", ""), ".DB", ""), 7) & ".run.db").Visible Or StandAloneMode Then 'jrs 2024/03/20
                    cswmItem = cswmGroup2.Items.Add()
                    cswmItem.Caption = Mid(Replace(Replace(sfile, ".db", ""), ".DB", ""), 7)
                    cswmItem.IconHeight = 16
                    If Session("POPCG_Casename") = cswmItem.Caption Then
                        'jam 2021/04/03
                        cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                    Else
                        'jam 2021/04/03
                        cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
                    End If
                    'jrs 2014/02/12
                    If bLoadSces Then
                        cmbSce1.Items.Add(cswmItem.Caption)
                        cmbSce2.Items.Add(cswmItem.Caption)
                        cmbSce3.Items.Add(cswmItem.Caption)
                        cmbSce4.Items.Add(cswmItem.Caption)
                    End If
                End If
            End If
            sfile = Dir()
        Loop
    End Sub

    'jrs 2008/06/05
    Private Sub mnuSce_Click(ByVal sSceName As String, ByRef objSender As Coalesys.WebMenu.WebMenu)
        'jam 2021/04/03
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Base Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        objSender.Groups("Change Display").Items("Change Scenario").Group.Items("Work Case").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"

        On Error GoTo ErrHandler07
        Dim i As Integer
        For i = 2 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            If objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).Caption = sSceName Then
                'jam 2021/04/03
                objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
                Case_of_Work = i + 2
            Else
                'jam 2021/04/03
                objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            End If
        Next

        'jrs 2008/09/16
        Session("POPCG_Case_of_Work") = Case_of_Work

        CaseName = sSceName
        Session("POPCG_Casename") = CaseName

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"


        'JAM 2019-12-08
        If Session("mnuSQLite") Is Nothing Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"

        Else

            If Not Session("mnuSQLite") = "True" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".mdb"
            Else
                'Session("db2Path") = Session("PutFdrvRun") & "IFsPop" & sSceName & ".db"


                'JAM 2020-05-04 
                Session("SQLiteDB") = "IFsPop" & sSceName & ".db"

                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")

            End If
        End If


        Session("PopCohort_year_run") = FindLastYear()
        Session("PopCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("PopCohort_tempyear"))
        Session("FirstYearSce") = FindFirstYear()

        If Session("PopCohort_year_run") <= FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearHistory
        ElseIf Session("PopCohort_tempyear") < FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearForecast
        End If

        If Session("PopCohort_tempyear") = Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("PopCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("PopCohort_GraphType")))
ErrHandler07:
        Exit Sub
    End Sub

    'jrs 2008/06/05
    Private Sub mnuHistSce_Click(ByRef objSender As Coalesys.WebMenu.WebMenu)

        On Error GoTo ErrHandler08

        Dim i As Integer
        For i = 0 To objSender.Groups("Change Display").Items("Change Scenario").Group.Items.Count - 1
            'jam 2021/04/03
            objSender.Groups("Change Display").Items("Change Scenario").Group.Items(i).IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Next

        CaseName = "Historical Forecast"
        Session("POPCG_Casename") = CaseName

        'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"


        'JAM 2020-01-14
        If Session("mnuSQLite") Is Nothing Then
            Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"

        Else

            If Not Session("mnuSQLite") = "True" Then
                Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.mdb"
            Else
                'Session("db2Path") = Session("PutFdrvRun") & "IFsPopHist.db"

                'JAM 2020-05-04 
                Session("SQLiteDB") = "IFsPopHist.db"

                Session("db2Path") = Session("PutFdrvRun") & Session("SQLiteDB")


            End If
        End If

        Session("PopCohort_year_run") = FindLastYear()
        Session("PopCohort_tempyear") = Amin(Session("PopCohort_year_run"), Session("PopCohort_tempyear"))
        Session("FirstYearSce") = FindFirstYear()

        If Session("PopCohort_year_run") <= FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearHistory
        ElseIf Session("PopCohort_tempyear") < FirstYearForecast Then
            Session("PopCohort_tempyear") = FirstYearForecast
        End If

        If Session("PopCohort_tempyear") = Session("FirstYearSce") Then
            objSender.Groups("Time").Items("Regress").Enabled = False
        Else
            objSender.Groups("Time").Items("Regress").Enabled = True
        End If
        If Session("PopCohort_tempyear") >= Session("PopCohort_year_run") Then
            objSender.Groups("Time").Items("Advance").Enabled = False
        Else
            objSender.Groups("Time").Items("Advance").Enabled = True
        End If

        'jrs 2014/02/13
        For i = 0 To 3
            Session("dbSCEs")(i) = Session("db2Path")
            Session("CaseNames")(i) = CaseName
        Next i

        'jrs 2014/01/29
        If Session("mnuFixCtSc") = True Then
            SetUpMaxs()
        ElseIf Session("mnuFixCteScTime") = True Then
            SetUpMaxsTime()
        End If

        Graph_Draw((Session("PopCohort_GraphType")))
ErrHandler08:
        Exit Sub
    End Sub

    'jrs 2008/06/24
    Private Sub mnuExit_Click()
        'jrs 2024/12/06 avoid error running the model because ifsPopWork.db is still being used
        If Not (vSQLiteCon Is Nothing) Then
            vSQLiteCon.Close()
            vSQLiteCon.Dispose()
        End If

        'jam 2021/04/03
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Session.Remove("PC_TempDataBase")
        'jam 2024/09/10
        Session.Remove("EC_TempPopDatabase_Comp")
        'jrs 2026/01/12 Session.Remove("MMCountry")
        Session.Remove("MMGroup")
        Session.Remove("GroupTot")
        ''''''''''
        Session.Remove("POPM")
        Session.Remove("POPF")
        Session.Remove("POPMales")
        Session.Remove("POPFemales")
        Session.Remove("POPCG_Case_of_Work")



        'jam 202/04/03
        Session.Remove("IsGroup")

        'jam 2021/04/03
        Session.Remove("vCountry")
        Session.Remove("vYear")
        Session.Remove("vScenario")



        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If

        'jam 2021/04/03
        'Support.FormManager.Show(Me, Session("DisplayParent"))



        'jam 2021/04/03
        If Session("DisplayParent") = Nothing Or Session("DisplayParent") = "frm_MainMenu.aspx" Then
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_MainMenu.aspx")

            vurl = FriendlyUrl.Href("~/frm_MainMenu")
        Else
            vfriendlyurl = FriendlyUrl.Resolve("~/" & Session("DisplayParent"))
            vurl = FriendlyUrl.Href("~/" & Replace(Session("DisplayParent"), ".aspx", ""))
        End If

        Response.Redirect(vurl)

    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        Dim iNCohortsWD As Integer = NCohorts
        iNCohortsWD = FindLastCohortWithData(Session("PC_TempDataBase"), 2, 0)

        Dim iNCohorts As Integer
        iNCohorts = iNCohortsWD 'jrs2013/08/16 NCohorts

        Dim Ages() As String
        ReDim Ages(iNCohorts)
        If iNCohorts = NCohorts Then
            Ages(iNCohorts) = CStr((NCohorts - 1) * 5) & "+" 'jrs 2013/02/22 "100+"
        Else
            Ages(iNCohorts) = CStr(iNCohorts * 5 - 5) & "-" & CStr(iNCohorts * 5 - 1)
        End If

        Dim l As Integer
        For l = 2 To iNCohorts - 1
            Ages(l) = Str(l * 5 - 5) & "-" & Str(l * 5 - 1)
        Next l
        Ages(0) = "Infants"
        Ages(1) = "0-4"

        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "            var mycategories = [" & vbCr
        For l = 1 To iNCohorts
            If l > 1 Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & "'" & Ages(l) & "'"
        Next
        sJscript2 = sJscript2 & "];" & vbCr

        If Session("bCompare") Then
            For l = 1 To 4
                sJscript2 = sJscript2 & PackJScritpBuilderContainter(l.ToString, iNCohortsWD)
            Next
        Else
            sJscript2 = sJscript2 & PackJScritpBuilderContainter("", iNCohortsWD)
        End If

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2


    End Sub

    Private Function PackJScritpBuilderContainter(ByVal sContainterIdx As String, ByVal iNCohortsWD As Integer) As String
        Dim sJscript2 As String = ""

        On Error GoTo ErrHandler09


        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        'jam 2025/12/06 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        'sJscript2 = sJscript2 & "fontFamily: 'Arial, sans-serif'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        'sJscript2 = sJscript2 & "color: '#333333' " & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''




        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container" & sContainterIdx & "').highcharts({" & vbCr

        'jam 2025/03/01 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "type: 'bar'," & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        sJscript2 = sJscript2 & "events: {"
        sJscript2 = sJscript2 & "load: function(event) {"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.visibility = 'hidden';"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.height = 0;"
        sJscript2 = sJscript2 & "}}"
        sJscript2 = sJscript2 & "}," & vbCr

        Dim gphIdx As Integer = 0
        If sContainterIdx > "" Then
            gphIdx = CInt(sContainterIdx)
        End If

        Dim Index As Short
        Dim graphtitle As String = ""
        Dim regionname As String
        Dim CaseName As String
        Dim TempYear As Short
        Dim subtitle As String = ""
        Index = Session("PopCG_Index")
        regionname = Session("POPCG_Regionname")
        CaseName = Session("POPCG_Casename")
        TempYear = Session("POPCG_tempyear")
        If Index = 1 Or Index = 4 Then
            graphtitle = "Population"
        ElseIf Index = 2 Then
            graphtitle = "Fertility"
        ElseIf Index = 3 Then
            graphtitle = "Mortality"
        End If
        graphtitle = graphtitle & " Distribution "

        'jrs 2014/02/11
        If Session("bCompare") Then
            If gphIdx < 5 Then
                graphtitle = ""
                subtitle = Session("EC_Reg")(gphIdx) & " in year " & Str(Session("EC_Yr")(gphIdx)) & " [" + Session("CaseNames")(gphIdx - 1) + "]"
            Else
                'graphtitle = graphtitle & " [" + CaseName$ + "]"
                subtitle = ""
            End If
        Else
            subtitle = "for " & regionname & " in year " & Str(TempYear) & " [" + CaseName$ + "]"
        End If

        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06  Increasing font size for legends
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr


        'jam 2025/12/06  Increasing font size for legends
        sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '24px'}," & vbCr
        ''''''''''''''''''''''''''''


        If Index = 4 Then
            sJscript2 = sJscript2 & "x: 25 //center" & vbCr
        Else
            sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "subtitle: {" & vbCr

        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "text:       """ & subtitle & """," & vbCr
        If Index = 4 Then
            sJscript2 = sJscript2 & "x: 25 //center" & vbCr
        Else
            sJscript2 = sJscript2 & "x: 0 //center" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'cdv 2021/01/12 Reversed Legend order  ****START****
        If Index = 4 Then
            sJscript2 = sJscript2 & "legend: {" & vbCr
            sJscript2 = sJscript2 & "x:       60," & vbCr
            sJscript2 = sJscript2 & "reversed:       true" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        Else
            sJscript2 = sJscript2 & "legend: {" & vbCr
            sJscript2 = sJscript2 & "reversed:       true" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If
        'cdv 2021/01/12 ****END****
        sJscript2 = sJscript2 & "xAxis: {" & vbCr

        'jam 2025/12/06 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr


        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Age Categories'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06 Increasing font for x axis
        sJscript2 = sJscript2 & "fontSize: '22px'," & vbCr
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        'jrs 2025/07/21
        If gphIdx = 0 Then
            sJscript2 = sJscript2 & "labels: {" & vbCr
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "fontSize: '22px'," & vbCr '22
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "categories: mycategories, " & vbCr
        sJscript2 = sJscript2 & "reversed: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxis As String = ""
        If Index = 1 Or Index = 4 Then
            'jrs 2014/02/14
            If Session("mnuUsePctPop") = False Then
                sAxis = "Population in Millions"
            Else
                sAxis = "Percent of Total Population"
            End If
        ElseIf Index = 2 Then
            sAxis = "Births/1000 Women per Year"
        ElseIf Index = 3 Then 'jrs 2014/02/14
            sAxis = "Deaths/1000 People per Year"
        End If

        Dim sMax As String = "", sMin As String = ""
        If Session("mnuFixCtSc") = True Or Session("mnuFixCteScTime") = True Or Session("mnuFixCtScMan") = True Then
            sMax = "max: " & Session("MaxScale")(Index)
            If Index = 4 Then
                sMin = "min: " & CStr(-1 * Session("MaxScale")(Index))
            Else
                sMin = "min: 0"
            End If
        End If

        sJscript2 = sJscript2 & "yAxis: {" & vbCr

        'jam 2025/12/06 Increasing font for y axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr


        If sMax > "" Then
            sJscript2 = sJscript2 & sMax & ", " & vbCr
            sJscript2 = sJscript2 & sMin & ", " & vbCr
        End If
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/12/06 Increasing font for y axis
        'sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr 
        sJscript2 = sJscript2 & "fontSize: '22px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        If Index = 4 Then
            sJscript2 = sJscript2 & "x: 0," & vbCr
        Else
            sJscript2 = sJscript2 & "x: -50," & vbCr
        End If
        sJscript2 = sJscript2 & "text:       '" & sAxis & "'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        'jrs 2025/07/21
        If gphIdx = 0 Then
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "fontSize: '18px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return Math.abs(this.value);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Index = 4 Then
            sJscript2 = sJscript2 & "plotOptions: {" & vbCr
            sJscript2 = sJscript2 & "series: {" & vbCr
            sJscript2 = sJscript2 & "stacking:   'normal'" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<b>' + this.series.name + ', age ' + this.point.category + '</b><br/>' +"
        sJscript2 = sJscript2 & "'Population: ' + Highcharts.numberFormat(Math.abs(this.point.y), 2);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        If Session("bCompare") Then
            iNCohortsWD = FindLastCohortWithData(Session("EC_TempDataBase_Comp"), 2, gphIdx)
            'cdv 2021/01/12 ****Start****
            If bMaleFirst Or Index = 2 Then
                sJscript2 = sJscript2 & PlotColToJSSeries(2, Session("EC_TempDataBase_Comp"), True, Index, Nothing, gphIdx, iNCohortsWD, True)
            Else
                sJscript2 = sJscript2 & PlotColToJSSeries(1, Session("EC_TempDataBase_Comp"), True, Index, Nothing, gphIdx, iNCohortsWD, True)
            End If
            If Index <> 2 Then
                If bMaleFirst Then
                    sJscript2 = sJscript2 & "," & PlotColToJSSeries(1, Session("EC_TempDataBase_Comp"), True, Index, Nothing, gphIdx, iNCohortsWD, True)
                Else
                    sJscript2 = sJscript2 & "," & PlotColToJSSeries(2, Session("EC_TempDataBase_Comp"), True, Index, Nothing, gphIdx, iNCohortsWD, True)
                End If

            End If
            'cdv 2021/01/12 ****END****

        Else
            'If bMaleFirst Then Males
            'cdv 2021/01/12 Added Gender Value results for deciding data order
            If bMaleFirst Or Index = 2 Then
                sJscript2 = sJscript2 & PlotColToJSSeries(2, Session("PC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
            Else
                sJscript2 = sJscript2 & PlotColToJSSeries(1, Session("PC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
            End If

            'Females stored in Index 1 for Fertility
            If Index <> 2 Then
                'If bMaleFirst Then Females
                'cdv 2021/01/12 Added Gender Value results for deciding data order
                If bMaleFirst Then
                    sJscript2 = sJscript2 & "," & PlotColToJSSeries(1, Session("PC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
                Else
                    sJscript2 = sJscript2 & "," & PlotColToJSSeries(2, Session("PC_TempDataBase"), True, Index, Nothing, 0, iNCohortsWD, True)
                End If
            End If
        End If

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        'jam 2023/01/11
        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".css({"
        sJscript2 = sJscript2 & "cursor: 'pointer'"
        sJscript2 = sJscript2 & "})"
        sJscript2 = sJscript2 & ".add();" & vbCr

        'If Not (Session("VLYear") Is Nothing) Then
        '    sJscript2 = sJscript2 & "chart.renderer.path(['m', " & CStr((Session("VLYear") - Session("FirstYear")) * 70 + 100) & ", 0, 'V', 800])" & vbCr
        '    sJscript2 = sJscript2 & ".attr({" & vbCr
        '    sJscript2 = sJscript2 & "'stroke-width': 1," & vbCr
        '    sJscript2 = sJscript2 & "stroke:         'red'," & vbCr
        '    sJscript2 = sJscript2 & "zIndex:1000" & vbCr
        '    sJscript2 = sJscript2 & "})" & vbCr
        '    sJscript2 = sJscript2 & ".add();" & vbCr
        'End If


        sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95 - 50;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 50;" & vbCr

        If Session("bCompare") Then
            sJscript2 = sJscript2 & "myWidth = myWidth * 0.49;" & vbCr
            sJscript2 = sJscript2 & "myHeight = myHeight * 0.49;" & vbCr
        End If

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr

        PackJScritpBuilderContainter = sJscript2

ErrHandler09:
        Exit Function

    End Function

    'jrs 2014/02/11
    Private Sub mnuCompare_Click()
        'jrs 2014/06/16
        If Session("bCompare") = True Then
            Session("bCompare") = False
            pnlCompare.Visible = False

            wmMain.Groups("Change Display").ShowMenuBarItem = True
            wmMain.Groups("Time").ShowMenuBarItem = True
            wmMain.Groups("mnuGroups").ShowMenuBarItem = True
            'jrs 2014/10/15
            'jam 2021/04/03
            wmMain.Groups("Distribution Type").Items("mnuCompare").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"

            Select Case Session("PopCohort_GraphType")
                Case 1
                    mnupopgph_Click(Me, New EventArgs)
                Case 2
                    mnufertgph_Click(Me, New EventArgs)
                Case 3
                    mnumortgph_Click(Me, New EventArgs)
                Case 4
                    mnupoppyr_Click(Me, New EventArgs)
            End Select
            PackJScritpBuilder()
            Exit Sub
        End If

        If Session("bCompare") = False Then
            wmMain.Groups("Change Display").ShowMenuBarItem = False
            wmMain.Groups("Time").ShowMenuBarItem = False
            wmMain.Groups("mnuGroups").ShowMenuBarItem = False
            'jrs 2014/10/15
            'jam 2021/04/03
            wmMain.Groups("Distribution Type").Items("mnuCompare").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        End If

        'Clear Region combos
        cmbReg1.Items.Clear()
        cmbReg2.Items.Clear()
        cmbReg3.Items.Clear()
        cmbReg4.Items.Clear()

        If Session("PopCohort_Grouping") = True Then
            For l = 1 To GCount
                cmbReg1.Items.Add(New ListItem(GroupNames(l)))
                cmbReg2.Items.Add(New ListItem(GroupNames(l)))
                cmbReg3.Items.Add(New ListItem(GroupNames(l)))
                cmbReg4.Items.Add(New ListItem(GroupNames(l)))
            Next l

            cmbReg1.SelectedValue = Session("PopCohort_regionname")
            cmbReg2.SelectedValue = Session("PopCohort_regionname")
            cmbReg3.SelectedValue = Session("PopCohort_regionname")
            cmbReg4.SelectedValue = Session("PopCohort_regionname")

        Else
            For l = 1 To NR
                cmbReg1.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg2.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg3.Items.Add(New ListItem(GRPS(1, l)))
                cmbReg4.Items.Add(New ListItem(GRPS(1, l)))
            Next l

            cmbReg1.SelectedIndex = cntrylist.SelectedIndex
            cmbReg2.SelectedIndex = cntrylist.SelectedIndex
            cmbReg3.SelectedIndex = cntrylist.SelectedIndex
            cmbReg4.SelectedIndex = cntrylist.SelectedIndex

        End If

        'jrs 2014/10/15
        'Connect all 4 to selected scenario
        cmbSce1.SelectedIndex = Session("POPCG_Case_of_Work")
        cmbSce2.SelectedIndex = Session("POPCG_Case_of_Work")
        cmbSce3.SelectedIndex = Session("POPCG_Case_of_Work")
        cmbSce4.SelectedIndex = Session("POPCG_Case_of_Work")
        'cdv 2021/05/19
        While Int(cmbYr2.Text) Mod 5 <> 0
            cmbYr2.SelectedIndex = cmbYr2.SelectedIndex - 1
            cmbYr3.SelectedIndex = cmbYr3.SelectedIndex - 1
            cmbYr4.SelectedIndex = cmbYr4.SelectedIndex - 1
        End While

        'jrs 2014/10/15
        If Session("PopCohort_tempyear") > cmbYr4.SelectedValue Then
            Dim diff As Integer, diff2 As Integer
            diff = Session("PopCohort_tempyear") - Session("FirstYear")
            cmbYr4.SelectedIndex = Session("PopCohort_tempyear") - Session("FirstYear")
            diff2 = ((diff \ 5) \ 3) * 5
            cmbYr2.SelectedIndex = diff2
            cmbYr3.SelectedIndex = diff2 * 2
        End If

        'Turn on bCompare flag
        Session("bCompare") = True
        'to Show comparison graphs (4) and hide single graph
        'Show Comparisson panel with 4 sets of region, year textbox and 1 refresh button
        pnlCompare.Visible = True
        cmdRefreshComp_Click(Me, New EventArgs)
    End Sub

    'jrs 2014/02/11
    Protected Sub cmdRefreshComp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRefreshComp.Click


        On Error GoTo ErrHandler10

        ReDim Session("EC_TempPopDatabase_Comp")(NCohorts, 2, 4)
        ReDim Session("EC_TempDatabase_Comp")(NCohorts, 8, 4)
        ReDim Session("EC_Reg")(4)
        ReDim Session("EC_Yr")(4)
        Dim i As Integer
        Dim GT As Integer
        If Session("PopCohort_GraphType") = 4 Then
            GT = 1
        Else
            GT = Session("PopCohort_GraphType")
        End If

        Session("CaseNames")(0) = cmbSce1.SelectedValue
        Session("CaseNames")(1) = cmbSce2.SelectedValue
        Session("CaseNames")(2) = cmbSce3.SelectedValue
        Session("CaseNames")(3) = cmbSce4.SelectedValue

        Select Case cmbSce1.SelectedIndex
            Case 0
                'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.mdb"

                'JAM 2020-01-14
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        'Session("dbsces")(0) = Session("PutFdrvRun") & "ifsPop.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPop.db"

                        Session("dbsces")(0) = Session("PutFdrvRun") & Session("SQLiteDB")



                    End If
                End If


            Case 1
                'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                'JAM 2019-11-17
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        'Session("dbsces")(0) = Session("PutFdrvRun") & "ifsPopWork.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPopWork.db"

                        Session("dbsces")(0) = Session("PutFdrvRun") & Session("SQLiteDB")



                    End If
                End If


            Case 2
                'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                'JAM 2020-01-14
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        'Session("dbsces")(0) = Session("PutFdrvRun") & "ifsPopHist.db"


                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPopHist.db"

                        Session("dbsces")(0) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

            Case Else
                'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".mdb"

                'JAM 2019-11-17
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".mdb"
                    Else
                        'Session("dbsces")(0) = Session("PutFdrvRun") & "IFsPop" & cmbSce1.SelectedValue & ".db"


                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "IFsPop" & cmbSce1.SelectedValue & ".db"

                        Session("dbsces")(0) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

        End Select
        Select Case cmbSce2.SelectedIndex
            Case 0
                'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.mdb"

                'JAM 2020-01-14
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        Session("dbsces")(1) = Session("PutFdrvRun") & "ifsPop.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPop.db"

                        Session("dbsces")(1) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If



            Case 1
                'JAM 2019-11-17
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        'Session("dbsces")(1) = Session("PutFdrvRun") & "ifsPopWork.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPopWork.db"

                        Session("dbsces")(1) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If
                    'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopWork.mdb"
            Case 2
                'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                'JAM 2020-01-14
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        'Session("dbsces")(1) = Session("PutFdrvRun") & "ifsPopHist.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPopHist.db"

                        Session("dbsces")(1) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

            Case Else
                'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".mdb"

                'JAM 2020-01-14
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".mdb"
                    Else
                        'Session("dbsces")(1) = Session("PutFdrvRun") & "IFsPop" & cmbSce2.SelectedValue & ".db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "IFsPop" & cmbSce2.SelectedValue & ".db"

                        Session("dbsces")(1) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If


        End Select
        Select Case cmbSce3.SelectedIndex
            Case 0
                'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.mdb"

                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        Session("dbsces")(2) = Session("PutFdrvRun") & "ifsPop.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPop.db"

                        Session("dbsces")(2) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If
            Case 1
                'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                'JAM 2019-11-17
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        'Session("dbsces")(2) = Session("PutFdrvRun") & "ifsPopWork.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPopWork.db"

                        Session("dbsces")(2) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If
            Case 2
                'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        'Session("dbsces")(2) = Session("PutFdrvRun") & "ifsPopHist.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPopHist.db"

                        Session("dbsces")(2) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

            Case Else
                'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".mdb"

                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".mdb"
                    Else
                        'Session("dbsces")(2) = Session("PutFdrvRun") & "IFsPop" & cmbSce3.SelectedValue & ".db"



                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "IFsPop" & cmbSce3.SelectedValue & ".db"

                        Session("dbsces")(2) = Session("PutFdrvRun") & Session("SQLiteDB")



                    End If
                End If

        End Select
        Select Case cmbSce4.SelectedIndex
            Case 0
                'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.mdb"

                'JAM 2020-01-14
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop.mdb"
                    Else
                        ' Session("dbsces")(3) = Session("PutFdrvRun") & "ifsPop.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPop.db"

                        Session("dbsces")(3) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If

            Case 1
                'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                'JAM 2019-11-17
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.mdb"




                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopWork.mdb"
                    Else
                        Session("dbsces")(3) = Session("PutFdrvRun") & "ifsPopWork.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPopWork.db"

                        Session("dbsces")(3) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If


            Case 2
                'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                'JAM 2020-01-14
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPopHist.mdb"
                    Else
                        'Session("dbsces")(3) = Session("PutFdrvRun") & "ifsPopHist.db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "ifsPopHist.db"

                        Session("dbsces")(3) = Session("PutFdrvRun") & Session("SQLiteDB")


                    End If
                End If


            Case Else
                'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".mdb"


                'JAM 2020-01-14
                If Session("mnuSQLite") Is Nothing Then
                    Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".mdb"

                Else

                    If Not Session("mnuSQLite") = "True" Then
                        Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".mdb"
                    Else
                        'Session("dbsces")(3) = Session("PutFdrvRun") & "IFsPop" & cmbSce4.SelectedValue & ".db"

                        'JAM 2020-05-04 
                        Session("SQLiteDB") = "IFsPop" & cmbSce4.SelectedValue & ".db"

                        Session("dbsces")(3) = Session("PutFdrvRun") & Session("SQLiteDB")

                    End If
                End If

        End Select

        If Session("PopCohort_Grouping") = True Then
            If Session("PopCohort_regionname") <> cmbReg1.SelectedValue Then
                Session("GroupTot") = Nothing
            End If
        End If

        If Session("mnuFixCteScTime") Then
            If Session("PopCohort_Grouping") = False Then
                Session("PopCohort_tempregion") = cmbReg1.SelectedIndex + 1
            Else
                Session("PopCohort_regionname") = cmbReg1.SelectedValue
            End If
            SetUpMaxsTime()
        End If

        For i = 1 To 4
            Compare_Graph_Draw(i, GT, (Session("PopCohort_GraphType") = 4))
        Next

        'jrs 2024/04/22
        vSQLiteCon.Close()
        If Not Session("SQLiteDB") Is Nothing Then
            vConnString = "DataSource=" + Session("PutFdrvRun") & Session("SQLiteDB") & ";"
        Else
            vConnString = "DataSource=" + Session("PutFdrvRun") & "ifsPop.db;"
        End If

        'jam 2025/05/31 Changing connection mode
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()


        vSQLiteCon = OpenJournalSQLite(vConnString)


        Session("EC_Reg")(1) = cmbReg1.SelectedValue
        Session("EC_Reg")(2) = cmbReg2.SelectedValue
        Session("EC_Reg")(3) = cmbReg3.SelectedValue
        Session("EC_Reg")(4) = cmbReg4.SelectedValue

        Session("EC_Yr")(1) = cmbYr1.SelectedValue
        Session("EC_Yr")(2) = cmbYr2.SelectedValue
        Session("EC_Yr")(3) = cmbYr3.SelectedValue
        Session("EC_Yr")(4) = cmbYr4.SelectedValue

        'jrs 2014/02/14
        Dim GraphTitle As String = ""
        Select Case GT
            Case 1
                GraphTitle = "Population"
            Case 2
                GraphTitle = "Fertility"
            Case 3
                GraphTitle = "Mortality"
        End Select
        lbldata.Text = GraphTitle & " Distribution for " & Session("EC_Reg")(4) & " in Year " & Str(Session("EC_Yr")(4)) & " [" & Session("CaseNames")(3) & "]" & " - " & Session("POPCB_Units")

        PackJScritpBuilder()

ErrHandler10:
        Exit Sub

    End Sub

    Private Sub Compare_Graph_Draw(gphIndex As Integer, Tindex As Integer, bPyramid As Boolean)
        '--------------------------------------------------------------
        Dim tbl2 As DataTable
        Dim tbl3 As DataTable
        Dim i As Integer
        Dim rId As Integer
        Dim Rcnt As Integer

        i = 0

        ReDim Session("PC_TempDataBase")(NCohorts, 2)

        On Error GoTo ErrHandler11

        '   Specify the graph control to be used
        '   ------------------------------------
        frameData.Visible = False

        Select Case gphIndex
            Case 1
                Session("PopCohort_tempregion") = cmbReg1.SelectedIndex + 1
                Session("PopCohort_regionname") = cmbReg1.SelectedValue
                Session("PopCohort_tempyear") = cmbYr1.SelectedValue
            Case 2
                Session("PopCohort_tempregion") = cmbReg2.SelectedIndex + 1
                Session("PopCohort_regionname") = cmbReg2.SelectedValue
                Session("PopCohort_tempyear") = cmbYr2.SelectedValue
            Case 3
                Session("PopCohort_tempregion") = cmbReg3.SelectedIndex + 1
                Session("PopCohort_regionname") = cmbReg3.SelectedValue
                Session("PopCohort_tempyear") = cmbYr3.SelectedValue
            Case 4
                Session("PopCohort_tempregion") = cmbReg4.SelectedIndex + 1
                Session("PopCohort_regionname") = cmbReg4.SelectedValue
                Session("PopCohort_tempyear") = cmbYr4.SelectedValue
        End Select

        'jrs 2024/04/22
        vSQLiteCon.Close()
        vConnString = "DataSource=" + Session("dbsces")(gphIndex - 1) & ";"

        'jam 2025/05/31
        'vSQLiteCon = New SQLiteConnection(vConnString)
        'vSQLiteCon.Open()

        'jam 2025/05/31 Changing connection mode
        vSQLiteCon = OpenJournalSQLite(vConnString)




        '   Change the data source
        '   ----------------------
        'JRS 07/26/02 GROUP
        If Session("PopCohort_Grouping") = False Then

            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                SetDataTablefromAccess(Session("dbSCEs")(gphIndex - 1), "SELECT * FROM " & TableName(Tindex) & " Where Region = " & Str(Session("PopCohort_tempregion")) & " and Years = " & Str(Session("PopCohort_tempyear")) & " and NCohorts <= " & Str(NCohorts), "", tbl2, "")
                Do While Not i = tbl2.Rows.Count
                    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = tbl2.Rows(i).Item(FieldName(Tindex, 1))
                    Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = tbl2.Rows(i).Item(FieldName(Tindex, 2))
                    i = i + 1
                Loop
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) & " Where RegionId = " & Str(Session("PopCohort_tempregion")) & " and TimeId = " & Str(Session("PopCohort_tempyear")) & " and CohortId <= " & Str(NCohorts)
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                Do While (vSQLiteReader.Read())
                    Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) = vSQLiteReader(FieldName(Tindex, 1))
                    Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) = vSQLiteReader(FieldName(Tindex, 2))

                Loop

            End If

            'jrs 2008/04/17
            If Tindex > 1 Then
                ReDim Session("POPM")(NCohorts)
                ReDim Session("POPF")(NCohorts)
                'JAM 2019-11-17
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    SetDataTablefromAccess(Session("dbSCEs")(gphIndex - 1), "SELECT * FROM " & TableName(1) &
                        " Where Region = " & Str(Session("PopCohort_tempregion")) &
                        " and Years = " + Str(Session("PopCohort_tempyear")), "", tbl3, "")
                    i = 0
                    Do While Not i = tbl3.Rows.Count
                        Session("POPM")(tbl3.Rows(i).Item("NCohorts")) = Session("POPM")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 1))
                        Session("POPF")(tbl3.Rows(i).Item("NCohorts")) = Session("POPF")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 2))

                        i = i + 1
                    Loop
                Else
                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(1) &
                        " Where RegionId = " & Str(Session("PopCohort_tempregion")) &
                        " and TimeId = " + Str(Session("PopCohort_tempyear"))
                    vSQLiteReader = vSQLiteCommand.ExecuteReader()

                    Do While (vSQLiteReader.Read())


                        Session("POPM")(vSQLiteReader("CohortId")) = Session("POPM")(vSQLiteReader("CohortId")) + vSQLiteReader(FieldName(1, 1))
                        Session("POPF")(vSQLiteReader("CohortId")) = Session("POPF")(vSQLiteReader("CohortId")) + vSQLiteReader(FieldName(1, 2))

                    Loop

                End If
            End If
        Else
            LoadCountries(Session("PopCohort_regionname"))
            'jrs 2008/04/17
            ReDim Session("POPM")(NCohorts)
            ReDim Session("POPF")(NCohorts)
            ReDim Session("POPMales")(Session("GroupTot"), NCohorts)
            ReDim Session("POPFemales")(Session("GroupTot"), NCohorts)

            Dim sRegs As String
            sRegs = ""
            For rId = 1 To Session("GroupTot")
                If sRegs = "" Then
                    sRegs = Str(Session("RegionIdGroup")(rId))
                Else
                    sRegs = sRegs & "," & Str(Session("RegionIdGroup")(rId))
                End If
            Next rId

            'jrs 2014/01/29
            If Session("bReadGrpMaxs") Then
                SetUpMaxsTime()
                Session("bReadGrpMaxs") = False
            End If

            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                SetDataTablefromAccess(Session("dbSCEs")(gphIndex - 1), "SELECT * FROM " & TableName(Tindex) &
                " Where Region in ( " & sRegs & ") and Years = " & Str(Session("PopCohort_tempyear")) &
                " and NCohorts <= " & Str(NCohorts) & " order by Region , NCohorts", "", tbl2, "")
                Rcnt = tbl2.Rows.Count
            Else
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & TableName(Tindex) &
                " Where RegionId in ( " & sRegs & ") and TimeId = " & Str(Session("PopCohort_tempyear")) &
                " and CohortId <= " & Str(NCohorts) & " order by RegionId , CohortId"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

            End If

            'jrs 2008/04/17 Population weigthed average for Mortality and Fertility
            If Tindex = 1 Then

                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    i = 0
                    Do While i < Rcnt 'True 'Not i = tbl2.Rows.Count
                        Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) + tbl2.Rows(i).Item(FieldName(Tindex, 1))
                        Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) + tbl2.Rows(i).Item(FieldName(Tindex, 2))
                        i = i + 1
                    Loop
                Else

                    Do While (vSQLiteReader.Read())

                        Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) = Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) + vSQLiteReader(FieldName(Tindex, 1))
                        Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) = Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) + vSQLiteReader(FieldName(Tindex, 2))


                    Loop


                End If
            Else
                'SetDataTablefromAccess(Session("dbSCEs")(gphIndex - 1), "SELECT * FROM " + TableName(1) & _
                '" Where Region in ( " & sRegs & ") and Years = " & Str(Session("PopCohort_tempyear")) & _
                '" and NCohorts <= " & Str(NCohorts) & " order by Region , NCohorts", "", tbl3, "")
                'i = 0
                'Do While Not i = tbl3.Rows.Count
                '    For rId = 1 To Session("GroupTot")
                '        If Session("RegionIdGroup")(rId) = tbl3.Rows(i).Item("Region") Then
                '            Exit For
                '        End If
                '    Next rId

                '    Session("POPMales")(rId, tbl3.Rows(i).Item("NCohorts")) = tbl3.Rows(i).Item(FieldName(1, 1))
                '    Session("POPFemales")(rId, tbl3.Rows(i).Item("NCohorts")) = tbl3.Rows(i).Item(FieldName(1, 2))
                '    Session("POPM")(tbl3.Rows(i).Item("NCohorts")) = Session("POPM")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 1))
                '    Session("POPF")(tbl3.Rows(i).Item("NCohorts")) = Session("POPF")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 2))

                '    i = i + 1
                'Loop
                'JAM 2019-11-17
                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    SetDataTablefromAccess(Session("dbSCEs")(gphIndex - 1), "SELECT * FROM " + TableName(1) &
                    " Where Region in ( " & sRegs & ") and Years = " & Str(Session("PopCohort_tempyear")) &
                    " and NCohorts <= " & Str(NCohorts) & " order by Region , NCohorts", "", tbl3, "")
                    i = 0
                    Do While Not i = tbl3.Rows.Count
                        For rId = 1 To Session("GroupTot")
                            If Session("RegionIdGroup")(rId) = tbl3.Rows(i).Item("Region") Then
                                Exit For
                            End If
                        Next rId

                        Session("POPMales")(rId, tbl3.Rows(i).Item("NCohorts")) = tbl3.Rows(i).Item(FieldName(1, 1))
                        Session("POPFemales")(rId, tbl3.Rows(i).Item("NCohorts")) = tbl3.Rows(i).Item(FieldName(1, 2))
                        Session("POPM")(tbl3.Rows(i).Item("NCohorts")) = Session("POPM")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 1))
                        Session("POPF")(tbl3.Rows(i).Item("NCohorts")) = Session("POPF")(tbl3.Rows(i).Item("NCohorts")) + tbl3.Rows(i).Item(FieldName(1, 2))

                        i = i + 1
                    Loop
                Else


                    'jam 2024/09/10
                    vSQLiteCommand2 = vSQLiteCon.CreateCommand()
                    vSQLiteCommand2.CommandText = "SELECT * FROM " + TableName(1) &
                        " Where RegionId in ( " & sRegs & ") and TimeId = " & Str(Session("PopCohort_tempyear")) &
                        " and CohortId <= " & Str(NCohorts) & " order by RegionId , CohortId"
                    vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()



                    Do While (vSQLiteReader2.Read())


                        For rId = 1 To Session("GroupTot")
                            If Session("RegionIdGroup")(rId) = vSQLiteReader2("RegionId") Then
                                Exit For
                            End If
                        Next rId

                        Session("POPMales")(rId, vSQLiteReader2("CohortId")) = vSQLiteReader2(FieldName(1, 1))
                        Session("POPFemales")(rId, vSQLiteReader2("CohortId")) = vSQLiteReader2(FieldName(1, 2))
                        Session("POPM")(vSQLiteReader2("CohortId")) = Session("POPM")(vSQLiteReader2("CohortId")) + vSQLiteReader2(FieldName(1, 1))
                        Session("POPF")(vSQLiteReader2("CohortId")) = Session("POPF")(vSQLiteReader2("CohortId")) + vSQLiteReader2(FieldName(1, 2))

                        'i = i + 1



                    Loop


                End If


                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    i = 0
                    Do While i < Rcnt 'True 'Not i = tbl2.Rows.Count
                        For rId = 1 To Session("GroupTot")
                            If Session("RegionIdGroup")(rId) = tbl2.Rows(i).Item("Region") Then
                                Exit For
                            End If
                        Next rId

                        Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 1) + tbl2.Rows(i).Item(FieldName(Tindex, 1)) * Session("POPMales")(rId, tbl2.Rows(i).Item("NCohorts"))
                        Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) = Session("PC_TempDataBase")(tbl2.Rows(i).Item("NCohorts"), 2) + tbl2.Rows(i).Item(FieldName(Tindex, 2)) * Session("POPFemales")(rId, tbl2.Rows(i).Item("NCohorts"))
                        i = i + 1
                    Loop

                Else

                    Do While (vSQLiteReader.Read())


                        For rId = 1 To Session("GroupTot")
                            If Session("RegionIdGroup")(rId) = vSQLiteReader("RegionId") Then
                                Exit For
                            End If
                        Next rId

                        'jam 2024/09/10
                        'If vSQLiteReader("CohortId") = 6 Then Stop
                        'jam 2024/09/10

                        If Not IsDBNull(vSQLiteReader(FieldName(Tindex, 1))) Then 'jam 2024/09/12
                            Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) = Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 1) + vSQLiteReader(FieldName(Tindex, 1)) * Session("POPMales")(rId, vSQLiteReader("CohortId"))
                        End If

                        If Not IsDBNull(vSQLiteReader(FieldName(Tindex, 2))) Then 'jam 2024/09/12
                            Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) = Session("PC_TempDataBase")(vSQLiteReader("CohortId"), 2) + vSQLiteReader(FieldName(Tindex, 2)) * Session("POPFemales")(rId, vSQLiteReader("CohortId"))
                        End If


                        'i = i + 1



                    Loop


                End If
            End If
            'if its fertility or mortality get average
            If Tindex > 1 And Session("GroupTot") > 1 Then
                For rId = 0 To NCohorts 'jrs 2005/11/29 changed from 1 to 0
                    'jrs 2008/04/17
                    'session("PC_TempDataBase")(rId, 1) = session("PC_TempDataBase")(rId, 1) / GroupTot
                    'session("PC_TempDataBase")(rId, 2) = session("PC_TempDataBase")(rId, 2) / GroupTot
                    If Session("POPM")(rId) > 0 Then
                        Session("PC_TempDataBase")(rId, 1) = Session("PC_TempDataBase")(rId, 1) / Session("POPM")(rId)
                    Else
                        Session("PC_TempDataBase")(rId, 1) = 0
                    End If
                    If Session("POPF")(rId) > 0 Then
                        Session("PC_TempDataBase")(rId, 2) = Session("PC_TempDataBase")(rId, 2) / Session("POPF")(rId)
                    Else
                        Session("PC_TempDataBase")(rId, 2) = 0
                    End If
                Next rId
            End If
        End If

        'jrs 2014/02/05
        If (Session("PopCohort_GraphType") = 1 Or Session("PopCohort_GraphType") = 4) And Session("mnuUsePctPop") Then
            ConvertToPctPop()
        End If

        FillDataGrid(Tindex)

        'Load result to Compare array
        Dim j As Integer
        For i = 0 To NCohorts
            If (Tindex > 1 And Tindex < 4) Then
                Session("EC_TempPopDatabase_Comp")(i, 1, gphIndex) = Session("POPM")(i)
                Session("EC_TempPopDatabase_Comp")(i, 2, gphIndex) = Session("POPF")(i)
            Else
                For j = 1 To 2
                    Session("EC_TempPopDatabase_Comp")(i, j, gphIndex) = Session("PC_TempDataBase")(i, j)
                Next
            End If

            For j = 1 To 2
                Session("EC_TempDatabase_Comp")(i, j, gphIndex) = Session("PC_TempDataBase")(i, j)
            Next
        Next

ErrHandler11:
        Exit Sub

    End Sub

    'jrs 2014/01/28
    Private Sub mnuFixCtSc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = "checkbox.gif" Then
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtSc") = False
        Else
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Session("mnuFixCtSc") = True
        End If

        If Session("mnuFixCtSc") = True Then
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCteScTime") = False
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtScMan") = False

            SetUpMaxs()
        End If
        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
        Else
            Graph_Draw(Session("PopCohort_GraphType"))
        End If
    End Sub

    'jrs 2014/02/14
    Private Sub mnuFixCteScTime_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = "checkbox.gif" Then
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCteScTime") = False
        Else
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Session("mnuFixCteScTime") = True
        End If

        If Session("mnuFixCteScTime") = True Then
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") ' "blank.gif"
            Session("mnuFixCtSc") = False
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtScMan") = False

            SetUpMaxsTime()
        End If
        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
        Else
            Graph_Draw(Session("PopCohort_GraphType"))
        End If
    End Sub

    'jrs 2014/02/14
    Private Sub mnuFixCtScMan_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = "checkbox.gif" Then
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtScMan") = False
        Else
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale Manually").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
            Session("mnuFixCtScMan") = True
        End If

        If Session("mnuFixCtScMan") = True Then
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time and Regions").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCtSc") = False
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Fix Constant Scale across Time").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
            Session("mnuFixCteScTime") = False

            txtPop.Text = Session("MaxScale")(1)
            txtFert.Text = Session("MaxScale")(2)
            txtMort.Text = Session("MaxScale")(3)
            lblJS.Visible = False
            pnlMaxScale.Visible = True
            pnlCompare.Visible = False
        End If

        PackJScritpBuilder()
    End Sub

    'jrs 2014/02/06
    Private Sub mnuUsePctPop_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("mnuUsePctPop") = Not Session("mnuUsePctPop")

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        If Session("mnuUsePctPop") = False Then
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Use Percent of Population").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        Else
            'jam 2021/04/03
            objSender.Groups("Change Scale").Items("Use Percent of Population").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        End If

        If Session("PopCohort_GraphType") = 1 Or Session("PopCohort_GraphType") = 4 Then
            If Session("bCompare") Then
                mnuCompare_Click()
            Else
                Graph_Draw(Session("PopCohort_GraphType"))
            End If
        End If
    End Sub

    'jrs 2014/01/29
    Private Sub SetUpMaxs()

        On Error GoTo ErrHandler12

        If (Session("PopCohort_GraphType") = 1 Or Session("PopCohort_GraphType") = 4) And Session("mnuUsePctPop") = True Then
            If Session("PopCohort_GraphType") = 1 Then
                Session("MaxScale")(Session("PopCohort_GraphType")) = 13
            Else
                Session("MaxScale")(Session("PopCohort_GraphType")) = 25
            End If
            Exit Sub
        End If

        Dim Margin As Single
        Margin = 1.2   '-> 20%

        'All Types of Mortality
        '======================
        Dim sql As String
        Dim rs As DataTable

        If Session("PopCohort_Grouping") = False Then

            If Session("PopCohort_GraphType") < 4 Then
                sql = "SELECT MAX(" & FieldName(Session("PopCohort_GraphType"), 1) & ") as MaxValue, MAX(" & FieldName(Session("PopCohort_GraphType"), 2) & ") as MaxValue2" &
                " FROM " & TableName(Session("PopCohort_GraphType"))
            Else
                sql = "SELECT MAX(" & FieldName(1, 1) & ") as MaxValue , MAX(" & FieldName(1, 2) & ") as MaxValue2" &
                " FROM " & TableName(1)
            End If


            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then




                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                    Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(0)
                Else
                    Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(1)
                End If

            Else

                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                While (vSQLiteReader.Read())

                    If vSQLiteReader("MaxValue") > vSQLiteReader("MaxValue2") Then
                        Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("MaxValue")
                    Else
                        Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("MaxValue2")
                    End If

                End While


            End If

        Else



            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                If Session("PopCohort_GraphType") = 2 Then
                    sql = "SELECT SUM(" & FieldName(Session("PopCohort_GraphType"), 2) & "*" & FieldName(1, 2) & ")/SUM(" & FieldName(1, 2) & ") as Firstvalue " &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " as m, " & TableName(1) & " as p WHERE m.Region = p.Region AND m.NCohorts = p.NCohorts "
                    sql = sql & " AND m.Years = " & CStr(Session("FirstYearSce")) & " AND m.NCohorts = 5" 'jrs looking at the world in 2010 this is the age with highest fertility
                ElseIf Session("PopCohort_GraphType") = 3 Then
                    sql = "SELECT SUM(" & FieldName(Session("PopCohort_GraphType"), 1) & "*" & FieldName(1, 1) & ")/SUM(" & FieldName(1, 1) & ") as Firstvalue, " &
                "SUM(" & FieldName(Session("PopCohort_GraphType"), 2) & "*" & FieldName(1, 2) & ")/SUM(" & FieldName(1, 2) & ") as Secondvalue " &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " as m, " & TableName(1) & " as p WHERE m.Region = p.Region AND m.NCohorts = p.NCohorts "
                    sql = sql & " AND m.Years = " & CStr(Session("FirstYearSce")) & " AND m.NCohorts = " & NCohorts
                Else 'Graph 4, population pyramid or Graph 1, Population distribution
                    sql = "SELECT SUM(" & FieldName(1, 1) & ") as Firstvalue, SUM(" & FieldName(1, 2) & ") as Secondvalue" &
                "FROM " & TableName(1) & " WHERE Years = " & CStr(Session("FirstYearSce"))
                    sql = sql & " AND NCohorts = 1" 'jrs looking at the world in 2100 this is the age with more people (cohort 11) 'jrs 2014/06/16 nop, looking at 2010 infants
                End If


                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                If Session("PopCohort_GraphType") = 4 Then
                    Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)
                ElseIf Session("PopCohort_GraphType") = 1 Then
                    If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                        Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(0)
                    Else
                        Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(1)
                    End If
                Else
                    Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(0)
                End If
            Else

                If Session("PopCohort_GraphType") = 2 Then
                    sql = "SELECT SUM(" & FieldName(Session("PopCohort_GraphType"), 2) & "*" & FieldName(1, 2) & ")/SUM(" & FieldName(1, 2) & ") as Firstvalue " &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " as m, " & TableName(1) & " as p WHERE m.RegionId = p.RegionId AND m.CohortId = p.CohortId "
                    sql = sql & " AND m.TimeId = " & CStr(Session("FirstYearSce")) & " AND m.CohortId = 5" 'jrs looking at the world in 2010 this is the age with highest fertility
                ElseIf Session("PopCohort_GraphType") = 3 Then
                    sql = "SELECT SUM(" & FieldName(Session("PopCohort_GraphType"), 1) & "*" & FieldName(1, 1) & ")/SUM(" & FieldName(1, 1) & ") as Firstvalue, " &
                "SUM(" & FieldName(Session("PopCohort_GraphType"), 2) & "*" & FieldName(1, 2) & ")/SUM(" & FieldName(1, 2) & ") as Secondvalue " &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " as m, " & TableName(1) & " as p WHERE m.RegionId = p.RegionId AND m.CohortId = p.CohortId "
                    sql = sql & " AND m.TimeId = " & CStr(Session("FirstYearSce")) & " AND m.CohortId = " & NCohorts
                Else 'Graph 4, population pyramid or Graph 1, Population distribution
                    sql = "SELECT SUM(" & FieldName(1, 1) & ") as Firstvalue, SUM(" & FieldName(1, 2) & ") as Secondvalue" &
                "FROM " & TableName(1) & " WHERE TimeId = " & CStr(Session("FirstYearSce"))
                    sql = sql & " AND CohortId = 1" 'jrs looking at the world in 2100 this is the age with more people (cohort 11) 'jrs 2014/06/16 nop, looking at 2010 infants
                End If


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                While (vSQLiteReader.Read())


                    If Session("PopCohort_GraphType") = 4 Then
                        Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("Firstvalue") + vSQLiteReader("Secondvalue")
                    ElseIf Session("PopCohort_GraphType") = 1 Then
                        If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                            Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("Firstvalue")
                        Else
                            Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("Secondvalue")
                        End If
                    Else
                        Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("Firstvalue")
                    End If



                End While





            End If



        End If

        For i = 1 To 4
            Session("MaxScale")(i) = Val(Session("MaxScale")(i)) * Margin
        Next i

ErrHandler12:
        Exit Sub
    End Sub

    'jrs 2014/01/28
    'Maxs across Time, specific per Region
    Private Sub SetUpMaxsTime()

        On Error GoTo ErrHandler13


        If (Session("PopCohort_GraphType") = 1 Or Session("PopCohort_GraphType") = 4) And Session("mnuUsePctPop") = True Then
            If Session("PopCohort_GraphType") = 1 Then
                Session("MaxScale")(Session("PopCohort_GraphType")) = 13
            Else
                Session("MaxScale")(Session("PopCohort_GraphType")) = 25
            End If
            Exit Sub
        End If

        Dim Margin As Single
        Margin = 1.2   '-> 20%

        'All Types of Mortality
        '======================
        Dim sql As String
        Dim rs As DataTable

        If Session("PopCohort_Grouping") = False Then
            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                If Session("PopCohort_GraphType") < 4 Then
                    sql = "SELECT MAX(" & FieldName(Session("PopCohort_GraphType"), 1) & ") as MaxValue, MAX(" & FieldName(Session("PopCohort_GraphType"), 2) & ") as MaxValue2" &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " WHERE Region "
                Else
                    sql = "SELECT MAX(" & FieldName(1, 1) & ") as MaxValue, MAX(" & FieldName(1, 2) & ") as MaxValue2" &
                "FROM " & TableName(1) & " WHERE Region "
                End If
                sql = sql & "= " & Str(Session("PopCohort_tempregion"))



                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                    Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(0)
                Else
                    Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(1)
                End If

            Else

                If Session("PopCohort_GraphType") < 4 Then
                    sql = "SELECT MAX(" & FieldName(Session("PopCohort_GraphType"), 1) & ") as MaxValue, MAX(" & FieldName(Session("PopCohort_GraphType"), 2) & ") as MaxValue2" &
                    " FROM " & TableName(Session("PopCohort_GraphType")) & " WHERE RegionId "
                Else
                    sql = "SELECT MAX(" & FieldName(1, 1) & ") as MaxValue, MAX(" & FieldName(1, 2) & ") as MaxValue2" &
                    " FROM " & TableName(1) & " WHERE RegionId "
                End If
                sql = sql & "= " & Str(Session("PopCohort_tempregion"))



                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                While (vSQLiteReader.Read())

                    If vSQLiteReader("MaxValue") > vSQLiteReader("MaxValue2") Then
                        Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("MaxValue")
                    Else
                        Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("MaxValue2")
                    End If


                End While


            End If


        Else
            If Session("GroupTot") Is Nothing Then
                LoadCountries(Session("PopCohort_regionname"))
            End If
            Dim sRegs As String = ""
            Dim rId As Integer
            For rId = 1 To Session("GroupTot")
                If sRegs = "" Then
                    sRegs = Str(Session("RegionIdGroup")(rId))
                Else
                    sRegs = sRegs & "," & Str(Session("RegionIdGroup")(rId))
                End If
            Next rId



            'JAM 2019-11-17
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                If Session("PopCohort_GraphType") = 2 Then
                    sql = "SELECT MAX(" & FieldName(Session("PopCohort_GraphType"), 2) & ") as MaxValue " &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " as m WHERE "
                    sql = sql & " m.Years = " & CStr(Session("FirstYearSce")) & " AND m.NCohorts = 5" 'jrs looking at the world in 2010 this is the age with highest fertility
                ElseIf Session("PopCohort_GraphType") = 3 Then
                    sql = "SELECT SUM(" & FieldName(Session("PopCohort_GraphType"), 1) & "*" & FieldName(1, 1) & ")/SUM(" & FieldName(1, 1) & ") as MaxValue, " &
                "SUM(" & FieldName(Session("PopCohort_GraphType"), 2) & "*" & FieldName(1, 2) & ")/SUM(" & FieldName(1, 2) & ") as MaxValue2" &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " as m, " & TableName(1) & " as p WHERE m.Region = p.Region AND m.NCohorts = p.NCohorts "
                    sql = sql & " AND m.Years = " & CStr(Session("FirstYearSce")) & " AND m.NCohorts = " & NCohorts
                Else 'Graph 4, population pyramid or Graph 1, Population distribution
                    sql = "SELECT SUM(" & FieldName(1, 1) & ") as MaxValue, SUM(" & FieldName(1, 2) & ") as MaxValue2" &
                "FROM " & TableName(1) & " as m WHERE m.Years = " & CStr(Session("PopCohort_year_run"))
                    sql = sql & " AND m.NCohorts = 11" 'jrs looking at the world in 2100 this is the age with more people
                End If
                sql = sql & " AND m.Region IN (" & sRegs & ")"

                SetDataTablefromAccess(Session("db2Path"), sql, "", rs, "")
                If Session("PopCohort_GraphType") = 4 Then
                    Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(0) + rs.Rows(0).Item(1)
                ElseIf Session("PopCohort_GraphType") = 1 Or Session("PopCohort_GraphType") = 3 Then
                    If rs.Rows(0).Item(0) > rs.Rows(0).Item(1) Then
                        Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(0)
                    Else
                        Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(1)
                    End If
                Else
                    Session("MaxScale")(Session("PopCohort_GraphType")) = rs.Rows(0).Item(0)
                End If

            Else
                If Session("PopCohort_GraphType") = 2 Then
                    sql = "SELECT MAX(" & FieldName(Session("PopCohort_GraphType"), 2) & ") as MaxValue " &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " as m WHERE "
                    sql = sql & " m.TimeId = " & CStr(Session("FirstYearSce")) & " AND m.CohortId = 5" 'jrs looking at the world in 2010 this is the age with highest fertility
                ElseIf Session("PopCohort_GraphType") = 3 Then
                    sql = "SELECT SUM(" & FieldName(Session("PopCohort_GraphType"), 1) & "*" & FieldName(1, 1) & ")/SUM(" & FieldName(1, 1) & ") as MaxValue, " &
                "SUM(" & FieldName(Session("PopCohort_GraphType"), 2) & "*" & FieldName(1, 2) & ")/SUM(" & FieldName(1, 2) & ") as MaxValue2" &
                "FROM " & TableName(Session("PopCohort_GraphType")) & " as m, " & TableName(1) & " as p WHERE m.RegionId = p.RegionId AND m.CohortId = p.CohortId "
                    sql = sql & " AND m.TimeId = " & CStr(Session("FirstYearSce")) & " AND m.CohortId = " & NCohorts
                Else 'Graph 4, population pyramid or Graph 1, Population distribution
                    sql = "SELECT SUM(" & FieldName(1, 1) & ") as MaxValue, SUM(" & FieldName(1, 2) & ") as MaxValue2" &
                "FROM " & TableName(1) & " as m WHERE m.TimeId = " & CStr(Session("PopCohort_year_run"))
                    sql = sql & " AND m.CohortId = 11" 'jrs looking at the world in 2100 this is the age with more people
                End If
                sql = sql & " AND m.RegionId IN (" & sRegs & ")"


                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = sql
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                While (vSQLiteReader.Read())



                    If Session("PopCohort_GraphType") = 4 Then
                        Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("MaxValue") + vSQLiteReader("MaxValue2")
                    ElseIf Session("PopCohort_GraphType") = 1 Or Session("PopCohort_GraphType") = 3 Then
                        If vSQLiteReader("MaxValue") > vSQLiteReader("MaxValue2") Then
                            Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("MaxValue")
                        Else
                            Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("MaxValue2")
                        End If
                    Else
                        Session("MaxScale")(Session("PopCohort_GraphType")) = vSQLiteReader("MaxValue")
                    End If



                End While

            End If
        End If

        For i = 1 To 4
            Session("MaxScale")(i) = Val(Session("MaxScale")(i)) * Margin
        Next i
ErrHandler13:
        Exit Sub
    End Sub

    'jrs 2014/02/14
    Protected Sub cmdOKMaxScale_Click(sender As Object, e As System.EventArgs) Handles cmdOKMaxScale.Click
        Session("MaxScale")(1) = txtPop.Text
        Session("MaxScale")(2) = txtFert.Text
        Session("MaxScale")(3) = txtMort.Text
        Session("MaxScale")(4) = txtPop.Text

        If Session("bCompare") Then
            cmdRefreshComp_Click(Me, New EventArgs)
            pnlCompare.Visible = True
        Else
            Graph_Draw(Session("PopCohort_GraphType"))
        End If

        lblJS.Visible = True
        pnlMaxScale.Visible = False
    End Sub

    'jrs 2014/02/14
    Protected Sub cmdCancleMaxScale_Click(sender As Object, e As System.EventArgs) Handles cmdCancleMaxScale.Click
        lblJS.Visible = True
        pnlMaxScale.Visible = False
    End Sub

    'jrs 2014/02/05
    'Converts Population Array to Percent of Population
    Public Sub ConvertToPctPop()
        'Find Total Population
        Dim TotPop As Single
        Dim j As Integer
        For j = 1 To NCohorts
            TotPop = TotPop + Session("PC_TempDataBase")(j, 1) + Session("PC_TempDataBase")(j, 2)
        Next j
        'Convert to Percent
        If TotPop > 0 Then
            For j = 1 To NCohorts
                Session("PC_TempDataBase")(j, 1) = Session("PC_TempDataBase")(j, 1) / TotPop * 100
                Session("PC_TempDataBase")(j, 2) = Session("PC_TempDataBase")(j, 2) / TotPop * 100
            Next j
        End If
    End Sub

    Private Function FindLastCohortWithData(ByRef Vals As Object, ByVal lastCol As Integer, ByVal gphIdx As Integer) As Integer
        'jrs 2013/03/29
        FindLastCohortWithData = 21

        'jam 2026/01/18 MAxCohort
        MaxCohortGroup = NCohorts

        If gphIdx > 4 Then
            Exit Function
        End If
        'jrs 2014/02/21 If NCohorts > 21 Then
        'Find Last cohort with data
        Dim k As Integer
        'jrs 2014/02/21
        If gphIdx > 0 Then
            For l% = NCohorts To 1 Step -1
                For k = 1 To lastCol
                    'jrs 2014/10/15
                    If Not IsDBNull(Vals(l%, k, gphIdx)) Then
                        If Vals(l%, k, gphIdx) > 0.00005 Then

                            FindLastCohortWithData = l
                            Exit For
                        End If
                    End If
                Next k
                If k < lastCol + 1 Then
                    Exit For
                End If
            Next l%

            'jam 2026/01/18 The graph with the max Cohort in compare Option
            If FindLastCohortWithData > MaxCohortGroup Then
                MaxCohortGroup = FindLastCohortWithData
            End If
        Else
            For l% = NCohorts To 1 Step -1
                For k = 1 To lastCol
                    'jrs 2014/10/15
                    If Not IsDBNull(Vals(l%, k)) Then
                        If Vals(l%, k) > 0.00005 Then
                            FindLastCohortWithData = l
                            Exit For
                        End If
                    End If
                Next k
                If k < lastCol + 1 Then
                    Exit For
                End If
            Next l%
        End If
        'jam 2026/01/18 With Compare option, return the maximum category with data
        'jrs 2014/02/21 End If
        'Return FindLastCohortWithData

        If Not Session("bCompare") Then
            Return FindLastCohortWithData
        Else
            If FindLastCohortWithData > MaxCohortGroup Then
                Return FindLastCohortWithData
            Else
                Return MaxCohortGroup
            End If

        End If


    End Function


    Private Function PlotColToJSSeries(ByVal i As Integer, ByRef Vals As Object, ByVal bShow100 As Boolean, ByVal Idx As Integer, _
                                ByVal gphType As MortType, ByVal CompIdx As Integer, ByVal iNCohortsWD As Integer, _
                                ByVal bAllZero As Boolean) As String
        Dim iNCohorts As Integer
        If bShow100 Then
            iNCohorts = iNCohortsWD 'jrs2013/08/16 NCohorts
        Else
            iNCohorts = iNCohortsWD - 1 'jrs2013/08/16NCohorts - 1
        End If

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i, Idx, gphType) & """, color:""" & getColor(i, Idx, gphType) & """, data: [" & vbCr

        Dim bLeftPyramid As Boolean = False, colIdx As Integer, iLowCohort As Integer
        colIdx = i
        'iLowCohort = 0
        iLowCohort = 1
        'cdv 2021/01/12 ****START**** Logic to make pyramid match gender order
        If Idx = 4 And i = 1 And bMaleFirst Then
            bLeftPyramid = True
        ElseIf Idx = 4 And i = 2 And Not bMaleFirst Then
            bLeftPyramid = True
        End If
        'cdv 2021/01/12 ****END****

        Dim j As Integer
        Dim bFirst As Boolean = True
        If Session("bCompare") And CompIdx > 0 And CompIdx < 5 And (Session("AboutParent") = "frm_EdnCohoDisp.aspx" Or Session("AboutParent") = "frm_MortCohorts.aspx" Or Session("AboutParent") = "frm_PopCohortDisp.aspx") Then 'jrs 2014/02/12
            For j = iLowCohort To iNCohorts
                'jrs 2014/10/15
                If Not IsDBNull(Vals(j, colIdx, CompIdx)) Then
                    'jrs 2013/03/29 health pyramid, for comm dis. is messed up when aids is 0 in higher ages, it shows up big
                    'it might only happen in stacked pyramids
                    If Vals(j, colIdx, CompIdx) > 0 Then
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & "," & vbCr
                        End If
                        If Not bLeftPyramid Then
                            sJscript = sJscript & CStr(Vals(j, colIdx, CompIdx))
                        Else
                            sJscript = sJscript & CStr(-1 * Vals(j, colIdx, CompIdx))
                        End If
                    End If
                End If
            Next j
        Else
            For j = iLowCohort To iNCohorts
                'jrs 2013/01/03
                If Not IsDBNull(Vals(j, colIdx)) Then
                    'jrs 2018/05/01 added or (j<5) showing wrong labels for fertility pyramid
                    'jrs 2014/02/21 added condition to check if all are 0
                    'jrs 2013/03/29 
                    If Vals(j, colIdx) > 0 Or (j = iNCohorts And bAllZero) Or (j < 5) Then 'jrs 2013/08/19 added or j=iNCohorts, problems when Males are all 0s
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & "," & vbCr
                        End If
                        If Not bLeftPyramid Then
                            sJscript = sJscript & CStr(Vals(j, colIdx))
                        Else
                            sJscript = sJscript & CStr(-1 * Vals(j, colIdx))
                        End If
                    End If
                Else
                    'jrs 2013/03/29
                    'sJscript = sJscript & "<point name = """ & Ages(j) & _
                    '    """ y=""0""/>" & vbCr
                End If
            Next j
        End If

        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Private Function getSeriesName(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Legend As String
        'cdv 2021/01/12 added Gender Label Logic
        If idx = 2 Then
            Legend = "Female"
        ElseIf (ii = 1 And idx <> 2) Then
            Legend = "Males"
        Else
            Legend = "Females"
        End If
        getSeriesName = Legend
    End Function

    Private Function getColor(ByVal ii As Integer, ByVal idx As Integer, ByRef gphType As MortType) As String
        Dim Color As String
        If ii = 1 And idx <> 2 Then
            Color = "Blue"
        Else
            Color = "Lime"
        End If


        getColor = Color
    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/02/04
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter">URL Parameters</param>
    ''' <returns>Parameters transformed in a valid URL format</returns>
    Public Function PopPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        PopPrepareForFriendlyURL = tmp
    End Function
End Class
