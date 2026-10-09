Option Strict Off
Option Explicit On
'jam 2023/04/29
Imports System.Data.SQLite
Imports Microsoft.VisualBasic.Compatibility
Imports NLog

Partial Class frm_Availability
    Inherits Page

    'jam 2023/04/29
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCon2 As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vConnString As String
    Dim vConnString2 As String
    Dim vSQLiteCommand2 As SqliteCommand
    Dim vSQLiteReader2 As SqliteDataReader
    Dim vConnString3 As String
    Dim vSQLiteCommand3 As SqliteCommand
    Dim vSQLiteReader3 As SqliteDataReader

    Private _logger As NLog.Logger = LogManager.GetCurrentClassLogger()

#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'KIA 10/04/09 change all Session("Scat_SE") to Scat_SE
        'CODEGEN: This method call is required by the Web Form Designer.
        'Do not modify it using the code editor.
        'CONVERSION_NOTE:  'Code' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
        'If m_vb6FormDefInstance Is Nothing Then 
        'If m_InitializingDefInstance Then 
        'm_vb6FormDefInstance = Me
        'Else 
        'Try 
        ''For the start-up form, the first instance created is the default instance.
        'If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then 
        'm_vb6FormDefInstance = Me
        'End If 
        'Catch 
        'End Try 
        'End If 
        'End If  
        InitializeComponent()
        'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.

    End Sub
    'CONVERSION_NOTE: Method 'Dispose' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    'If Disposing Then 
    'If Not components Is Nothing Then 
    'components.Dispose()
    'End If 
    'End If 
    'MyBase.Dispose(Disposing)
    'End Sub  
    'CONVERSION_NOTE: Field 'components' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Private components As System.ComponentModel.IContainer 
    'CONVERSION_NOTE: Class 'System.Windows.Forms.ToolTip' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public ToolTip1 As System.Windows.Forms.ToolTip 
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_AvailabilityPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_Availability
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_Availability()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    'Public RawVarName As String
    'mti August 2004 -changed to session
    'Dim YearAv(100) As Object

    'UPGRADE_WARNING: Event cmbYear.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cmbYear_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbYear.SelectedIndexChanged
        'UPGRADE_WARNING: Couldn't resolve default property of object YearAv(cmbYear.ListIndex). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        lblYearAvailability.Text = Session("YearAv")(cmbYear.SelectedIndex)
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click

        Dim sAvPrt As String
        sAvPrt = Session("AvailabilityParent")
        If Session("AvailabilityParent") = "frm_ScatVarSearch.aspx" Then
            Session("AvailabilityParent") = "ScatVarS.aspx"
            Support.FormManager.Close(Me)
        ElseIf Session("AvailabilityParent") = "frm_LongVarSearch.aspx" Then
            Session("AvailabilityParent") = "LongVarS.aspx"
            Support.FormManager.Close(Me)
        Else
            Support.FormManager.Show(Me, sAvPrt)
        End If

        'Support.FormManager.Close(Me)
        'Response.Redirect(CStr(ViewState("RefUrl")))
    End Sub

    'CONVERSION_NOTE: The code of Event 'frm_Availability.frm_Availability_Load' was moved to Method 'frm_Availability.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_Availability_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim Message As String
            'KIA 10/04/09 declare  the following variable
            Dim Scat_SE As String
            Dim db As String
            Dim Valtbl As DataTable
            Dim Rcnt As Integer

            Dim Msg As String

            'jam 2023/04/29
            Dim row As DataRow

            'mti August 2004
            'Dim ECount, Session("CCount"), YCount, TECount As Short
            ReDim Session("YearAv")(100)
            Dim ECount, YCount, TECount As Short
            Dim EarliestYear, TotFields, FCount, ThisYear, LatestYear As Short
            Dim MaxYearAvailability, MaxCountryAvailability As Object
            Dim MaxYear, MaxCountry As String

            'jam 2023/05/01
            Dim i As Integer

            On Error GoTo FileNotFound

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'

            'mti August 2004
            'System.Array.Clear(Session("YearAv"), 0, Session("YearAv").Length)

            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmbYear.Items.Clear()

            'db = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "DataDict.MDB")
            'KIA 10/04/09 comment the following line as we don't need to open datatict table always since we are putting the whole table in Session
            'db = Session("PutFdrvRun") & "DataDict.MDB"
            'valtbl = db.OpenRecordset("DataDict")
            'mti August 2004
            'KIA 10/04/09 comment the following line as we don't need to open datatict table always since we are putting the whole table in Session
            'SetDataTablefromAccess(db, "SELECT * FROM Datadict", "Variable", Valtbl)

            'valtbl.index = "Variable"
            ' Valtbl.MoveFirst()
            'Valtbl.Seek("=", RawVarName)
            'KIA 10/04/09 add the following 4 line to load the DataDict Table into valtbl and to get the dep/idep variable
            'jam 2023/05/28
            If Not (Not (Session("mnuSQLite") Is Nothing) And Session("mnuSQLite")) Then
                Valtbl = Session("DataDictVars")
            End If
            'jam 2023/04/29
            If Not (Not (Session("mnuSQLite") Is Nothing) And Session("mnuSQLite")) Then

                Valtbl.PrimaryKey = New DataColumn() {Valtbl.Columns("Variable")}
            End If
            Scat_SE = Request.QueryString("Scat_SE")
            Scat_SE = Scat_SE.Replace(" ", "+")
            'ViewState("RefUrl") = Request.ServerVariables("HTTP_REFERER")

            'jam 2023/04/29
            If Not (Not (Session("mnuSQLite") Is Nothing) And Session("mnuSQLite")) Then
                row = Valtbl.Rows.Find(Scat_SE)

                'SeekValue = ""
                'If Not row.HasErrors Then
                '    SeekValue = CType(row.Item(LookupFieldName), String)
                'End If

                'If SeekValue(Valtbl, Scat_SE, "Variable") = "" Then
                If row.HasErrors Then
                    Message = "Series " & Scat_SE & " not found!"
                    LoadMessage(Message, Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If
            End If
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & "-" & Scat_SE
            'jam 2023/04/29
            If Not (Not (Session("mnuSQLite") Is Nothing) And Session("mnuSQLite")) Then

                lblSeriesNameVal.Text = row.Item("Table") ' Valtbl.Fields("Table").Value
                lblSeriesDefnVal.Text = row.Item("Definition")
                lblSourceVal.Text = IIf(IsDBNull(row.Item("Source")), "", row.Item("Source")) 'jrs 2006/08/01 handling NULLs, GasPrice

                lblOriginalSourceVal.Text = IIf(IsDBNull(row.Item("Original Source")), "", row.Item("Original Source"))
            Else
                'jam 2023/05/28
                Scat_SE = Request.QueryString("Scat_SE")
                Scat_SE = Scat_SE.Replace(" ", "+")

                'jam 2023/04/29
                vConnString2 = "DataSource=" + Session("PutFdrvRun") & "DataDict.db;"
                vSQLiteCon2 = New SqliteConnection(vConnString2)
                vSQLiteCon2.Open()
                vSQLiteCommand2 = vSQLiteCon2.CreateCommand()
                'jrs 2023/10/05 added upper() and ucase()
                vSQLiteCommand2.CommandText = "Select * from DataDict where upper(Variable) = " + "'" + UCase(Scat_SE) + "'" '+ " order by Variable"
                vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()

                While (vSQLiteReader2.Read())

                    lblSeriesNameVal.Text = vSQLiteReader2("Table") ' Valtbl.Fields("Table").Value
                    lblSeriesDefnVal.Text = vSQLiteReader2("Definition")
                    lblSourceVal.Text = IIf(IsDBNull(vSQLiteReader2("Source")), "", vSQLiteReader2("Source")) 'jrs 2006/08/01 handling NULLs, GasPrice
                    lblOriginalSourceVal.Text = IIf(IsDBNull(vSQLiteReader2("Original Source")), "", vSQLiteReader2("Original Source"))
                    'jrs 2025/05/27 adding hyper link
                    If lblOriginalSourceVal.Text.Contains("http") Then
                        'if there's more text
                        If lblOriginalSourceVal.Text.Contains(" ") Then
                            Dim x = lblOriginalSourceVal.Text.Split(" ")
                            For i = 0 To x.Length - 1
                                If x(i).Contains("http") Then
                                    lblOriginalSourceVal.Text = lblOriginalSourceVal.Text.Replace(x(i), "<a href=" & x(i) & ">" & x(i) & "</a>")
                                    Exit For
                                End If
                            Next
                        Else
                            lblOriginalSourceVal.Text = "<a href=" & lblOriginalSourceVal.Text & ">" & lblOriginalSourceVal.Text & "</a>"
                        End If
                    End If
                    'jrs 2023/10/05
                    lblLastIFsUPdateVal.Text = IIf(IsDBNull(vSQLiteReader2("Last IFs Update")), "", vSQLiteReader2("Last IFs Update"))
                    'jrs 2025/05/27
                    lblNotesVal.Text = IIf(IsDBNull(vSQLiteReader2("Notes")), "", vSQLiteReader2("Notes"))
                    'jrs 2025/05/27 adding hyper link
                    If lblNotesVal.Text.Contains("http") Then
                        'if there's more text
                        If lblNotesVal.Text.Contains(" ") Then
                            Dim x = lblNotesVal.Text.Split(" ")
                            For i = 0 To x.Length - 1
                                If x(i).Contains("http") Then
                                    lblNotesVal.Text = lblNotesVal.Text.Replace(x(i), "<a href=" & x(i) & ">" & x(i) & "</a>")
                                    Exit For
                                End If
                            Next
                        Else
                            lblNotesVal.Text = "<a href=" & lblNotesVal.Text & ">" & lblNotesVal.Text & "</a>"
                        End If
                    End If
                End While



                vSQLiteCon2.Close()


            End If
            'JRS 1/28/03
            'lblOriginalSourceVal.Caption = valtbl.Fields("Original Source")
            'JRS 3/24/03
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            'jam 2023/04/29
            'lblOriginalSourceVal.Text = IIf(IsDBNull(row.Item("Original Source")), "", row.Item("Original Source"))

            '    WVSYesOrNo$ = valtbl.Fields("Cohort")

            'Valtbl.Close()
            'db.Close()
            'jam 2023/04/29
            If Not (Not (Session("mnuSQLite") Is Nothing) And Session("mnuSQLite")) Then
                'db = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "IFSHISTSERIES.MDB", False, False, DataPW)
                db = Session("PutFdrvRun") & "IFSHISTSERIES.MDB"
                'Valtbl = db.OpenRecordset(lblSeriesNameVal.Text)
                SetDataTablefromAccess(db, "SELECT * FROM [" & lblSeriesNameVal.Text & "]", "", Valtbl, DataPW) 'jrs 2006/02/23 added []


                Session("CCount") = Valtbl.Rows.Count 'Valtbl.RecordCount
                TotFields = Valtbl.Columns.Count  ' Valtbl.Fields.Count
                YCount = 0
                EarliestYear = 3000
                LatestYear = 100
                'UPGRADE_WARNING: Couldn't resolve default property of object MaxYearAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                MaxYearAvailability = 0
                MaxYear = ""
                TECount = 0
                For FCount = 0 To TotFields - 1
                    'ThisYear = Val(Valtbl.Fields(FCount).Name)
                    ThisYear = Val(Valtbl.Columns(FCount).ColumnName)
                    If ThisYear > 0 Then
                        YCount = YCount + 1
                        If ThisYear < EarliestYear Then
                            EarliestYear = ThisYear
                        End If
                        If ThisYear > LatestYear Then
                            LatestYear = ThisYear
                        End If
                        'Valtbl.MoveFirst()
                        ECount = 0
                        'Do While Not Valtbl.EOF
                        For Rcnt = 0 To Valtbl.Rows.Count - 1
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                            'If Not IsDBNull(Valtbl.Fields(FCount).Value) Then
                            If Not IsDBNull(Valtbl.Rows(Rcnt).Item(FCount)) Then
                                ECount = ECount + 1
                            End If
                            'Valtbl.MoveNext()
                            'Loop
                        Next Rcnt
                        TECount = TECount + ECount
                        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        cmbYear.Items.Add(New ListItem(Trim(Str(ThisYear))))
                        'UPGRADE_WARNING: Couldn't resolve default property of object YearAv(cmbYear.ListCount - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        Session("YearAv")(cmbYear.Items.Count - 1) = VB6.Support.Format((ECount / Session("CCount")) * 100, "standard") & "%"
                        'UPGRADE_WARNING: Couldn't resolve default property of object MaxYearAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        'jrs 2024/06/12 added = to get the most recent year with higher value
                        If ECount >= MaxYearAvailability Then
                            'UPGRADE_WARNING: Couldn't resolve default property of object MaxYearAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            MaxYearAvailability = ECount
                            MaxYear = Str(ThisYear)
                        End If
                    End If
                Next FCount

                cmbYear.SelectedIndex = 0
                lblYearAvailability.Text = Session("YearAv")(cmbYear.SelectedIndex)
                'lblYearAvailability.Caption = Str(YearAv(0))
                lblCountriesVal.Text = Trim(Str(Session("CCount")))
                lblYearsVal.Text = Trim(Str(YCount))
                lblFirstYearVal.Text = Trim(Str(EarliestYear))
                lblLastYearVal.Text = Trim(Str(LatestYear))

                'UPGRADE_WARNING: Couldn't resolve default property of object MaxYearAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                lblBestYearVal.Text = MaxYear & " (" & VB6.Support.Format((MaxYearAvailability / Session("CCount")) * 100, "standard") & "%)"

                lblTotPointsVal.Text = Str(Session("CCount") * YCount)
                lblAvaPointsVal.Text = Str(TECount)
                lblPercentAvaVal.Text = VB6.Support.Format((TECount / (Session("CCount") * YCount)) * 100, "standard") & "%"


                'UPGRADE_WARNING: Couldn't resolve default property of object MaxCountryAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                MaxCountryAvailability = 0
                MaxCountry = ""
                'Valtbl.MoveFirst()
                'Do While Not Valtbl.EOF
                For Rcnt = 0 To Valtbl.Rows.Count - 1
                    ECount = 0
                    For FCount = 2 To TotFields - 1
                        'ThisYear = Val(Valtbl.Fields(FCount).Name)
                        ThisYear = Val(Valtbl.Columns(FCount).ColumnName)
                        If ThisYear > 0 Then
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                            'If Not IsDBNull(Valtbl.Fields(FCount).Value) Then
                            If Not IsDBNull(Valtbl.Rows(Rcnt).Item(FCount)) Then
                                ECount = ECount + 1
                            End If
                        End If
                    Next FCount
                    'UPGRADE_WARNING: Couldn't resolve default property of object MaxCountryAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If ECount > MaxCountryAvailability Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object MaxCountryAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        MaxCountryAvailability = ECount
                        MaxCountry = Valtbl.Rows(Rcnt).Item("Country")
                    End If
                    'Valtbl.MoveNext()
                    'Loop
                Next Rcnt

            Else
                ''db = DAODBEngine_definst.OpenDatabase(session("PutFdrvRun") & "IFSHISTSERIES.MDB", False, False, DataPW)
                'db = Session("PutFdrvRun") & "IFSHISTSERIES.MDB"
                ''Valtbl = db.OpenRecordset(lblSeriesNameVal.Text)
                'SetDataTablefromAccess(db, "SELECT * FROM [" & lblSeriesNameVal.Text & "]", "", Valtbl, DataPW) 'jrs 2006/02/23 added []

                'jam 2023/04/29
                vConnString2 = "DataSource=" + Session("PutFdrvRun") & "IFSHISTSERIES.DB;"
                vSQLiteCon2 = New SqliteConnection(vConnString2)
                vSQLiteCon2.Open()

                vSQLiteCommand2 = vSQLiteCon2.CreateCommand()
                vSQLiteCommand2.CommandText = "SELECT Count(*) as vCount FROM [" & lblSeriesNameVal.Text & "]"
                vSQLiteReader = vSQLiteCommand2.ExecuteReader()

                While (vSQLiteReader.Read())

                    Session("CCount") = vSQLiteReader("vCount")

                End While

                vSQLiteCommand2 = vSQLiteCon2.CreateCommand()
                vSQLiteCommand2.CommandText = "SELECT Count(*) as vCountcolumns FROM pragma_table_info('" & lblSeriesNameVal.Text & "')"
                vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()

                While (vSQLiteReader2.Read())

                    TotFields = vSQLiteReader2("vCountcolumns")

                End While


                vSQLiteCommand3 = vSQLiteCon2.CreateCommand()
                'jrs 2024/06/12
                'vSQLiteCommand3.CommandText = "SELECT * FROM [" & lblSeriesNameVal.Text & "]"
                'vSQLiteReader3 = vSQLiteCommand3.ExecuteReader()




                'Session("CCount") = Valtbl.Rows.Count 'Valtbl.RecordCount
                ' TotFields = Valtbl.Columns.Count  ' Valtbl.Fields.Count
                YCount = 0
                EarliestYear = 3000
                LatestYear = 100
                'UPGRADE_WARNING: Couldn't resolve default property of object MaxYearAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                MaxYearAvailability = 0
                MaxYear = ""
                TECount = 0


                vSQLiteCommand2 = vSQLiteCon2.CreateCommand()
                vSQLiteCommand2.CommandText = "SELECT * FROM [" & lblSeriesNameVal.Text & "] ORDER BY ROWID ASC LIMIT 1"
                vSQLiteReader2 = vSQLiteCommand2.ExecuteReader()

                'jrs 2023/08/18
                If TotFields > 100 Then
                    ReDim Session("YearAv")(TotFields)
                End If
                'jam 2023/06/18

                'While (vSQLiteReader2.Read())
                For FCount = 0 To TotFields - 1
                    'Debug.Print(FCount.ToString)
                    'If FCount = 103 Then Stop
                    'ThisYear = Val(Valtbl.Fields(FCount).Name)
                    If IsNumeric(vSQLiteReader2.GetName(FCount)) Then
                        ThisYear = vSQLiteReader2.GetName(FCount)

                        If ThisYear > 0 Then
                            YCount = YCount + 1
                            If ThisYear < EarliestYear Then
                                EarliestYear = ThisYear
                            End If
                            If ThisYear > LatestYear Then
                                LatestYear = ThisYear
                            End If
                            'Valtbl.MoveFirst()
                            ECount = 0
                            'Do While Not Valtbl.EOF
                            'jam 2023/05/14
                            'For Rcnt = 0 To Valtbl.Rows.Count - 1
                            '    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                            '    'If Not IsDBNull(Valtbl.Fields(FCount).Value) Then
                            '    If Not IsDBNull(Valtbl.Rows(Rcnt).Item(FCount)) Then
                            '        ECount = ECount + 1
                            '    End If
                            '    'Valtbl.MoveNext()
                            '    'Loop
                            'Next Rcnt

                            'jrs 2024/06/12
                            vSQLiteCommand3.CommandText = "SELECT [" & ThisYear & "] FROM [" & lblSeriesNameVal.Text & "]"
                            vSQLiteReader3 = vSQLiteCommand3.ExecuteReader()


                            While (vSQLiteReader3.Read())

                                If Not IsDBNull(vSQLiteReader3.Item(0)) Then

                                    ECount = ECount + 1
                                End If

                            End While

                            'jrs 2024/06/12
                            vSQLiteReader3.Close()

                            TECount = TECount + ECount
                            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                            cmbYear.Items.Add(New ListItem(Trim(Str(ThisYear))))
                            'UPGRADE_WARNING: Couldn't resolve default property of object YearAv(cmbYear.ListCount - 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                            Session("YearAv")(cmbYear.Items.Count - 1) = VB6.Format((ECount / Session("CCount")) * 100, "standard") & "%"
                            'UPGRADE_WARNING: Couldn't resolve default property of object MaxYearAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'jrs 2024/06/12 added = to get the most recent year with max availability
                            If ECount >= MaxYearAvailability Then
                                'UPGRADE_WARNING: Couldn't resolve default property of object MaxYearAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                MaxYearAvailability = ECount
                                MaxYear = Str(ThisYear)
                            End If
                        End If
                    End If
                Next FCount

                'End While
                cmbYear.SelectedIndex = 0
                lblYearAvailability.Text = Session("YearAv")(cmbYear.SelectedIndex)
                'lblYearAvailability.Caption = Str(YearAv(0))
                lblCountriesVal.Text = Trim(Str(Session("CCount")))
                lblYearsVal.Text = Trim(Str(YCount))
                lblFirstYearVal.Text = Trim(Str(EarliestYear))
                lblLastYearVal.Text = Trim(Str(LatestYear))

                'UPGRADE_WARNING: Couldn't resolve default property of object MaxYearAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                lblBestYearVal.Text = MaxYear & " (" & VB6.Support.Format((MaxYearAvailability / Session("CCount")) * 100, "standard") & "%)"

                lblTotPointsVal.Text = Str(Session("CCount") * YCount)
                lblAvaPointsVal.Text = Str(TECount)
                lblPercentAvaVal.Text = VB6.Support.Format((TECount / (Session("CCount") * YCount)) * 100, "standard") & "%"


                'UPGRADE_WARNING: Couldn't resolve default property of object MaxCountryAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                MaxCountryAvailability = 0
                MaxCountry = ""
                'Valtbl.MoveFirst()
                'Do While Not Valtbl.EOF

                'jrs 2024/06/12
                vSQLiteCommand3.CommandText = "SELECT * FROM [" & lblSeriesNameVal.Text & "]"
                vSQLiteReader3 = vSQLiteCommand3.ExecuteReader()

                'jam 2023/06/18
                While (vSQLiteReader3.Read())
                    'For Rcnt = 0 To Valtbl.Rows.Count - 1
                    ECount = 0
                    For FCount = 2 To TotFields - 1
                        'ThisYear = Val(Valtbl.Fields(FCount).Name)
                        'jam 2023/06/18
                        If IsNumeric(vSQLiteReader2.GetName(FCount)) Then
                            'ThisYear = Val(Valtbl.Columns(FCount).ColumnName)
                            ThisYear = Val(vSQLiteReader2.GetName(FCount))
                            If ThisYear > 0 Then
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                                'If Not IsDBNull(Valtbl.Fields(FCount).Value) Then
                                'If Not IsDBNull(Valtbl.Rows(Rcnt).Item(FCount)) Then

                                If Not IsDBNull(vSQLiteReader3.Item(FCount)) Then

                                    ECount = ECount + 1
                                End If
                            End If
                        End If
                    Next FCount
                    'UPGRADE_WARNING: Couldn't resolve default property of object MaxCountryAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If ECount > MaxCountryAvailability Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object MaxCountryAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        MaxCountryAvailability = ECount
                        'MaxCountry = Valtbl.Rows(Rcnt).Item("Country")
                        MaxCountry = vSQLiteReader3("Country")

                    End If
                    'Valtbl.MoveNext()
                    'Loop
                    'Next Rcnt
                End While

                'jrs 2024/06/12
                vSQLiteReader3.Close()

            End If


            'UPGRADE_WARNING: Couldn't resolve default property of object MaxCountryAvailability. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            lblBestCountryVal.Text = MaxCountry & " (" & VB6.Support.Format((MaxCountryAvailability / YCount) * 100, "standard") & "%)"

            'Valtbl.Close()
            'db.Close()

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            'JRS 08/27/02 ERROR TRAPPING
            Exit Sub

FileNotFound:
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Message = "Data not found in availability search.  Please try a different variable."
            _logger.Error(Err.GetException(), $"Data not found in availability search.")
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
            Exit Sub


        End If


    End Sub
End Class
