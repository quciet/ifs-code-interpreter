Option Strict Off
Option Explicit On
Imports System.Data.OleDb 'mtiwebforms April 2004 ADO.Net

Partial Class frm_CountSel
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        If Not Page.IsPostBack Then
            Dim Msg As String
            Dim TableName As String
            Dim icount2 As Short
            Dim NoData As String
            Dim ICount As Short
            'mtiwebform June 2004 Longitudinal
            Dim db2 As String 'Object
            Dim Rcnt As Short
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'mtiwebform June 2004 Longitudinal - commented out form design
            'jrs 2008/05/15 Session("CountryTot") = 0

            '   Read Regionalization Table for Country Names
            '   --------------------------------------------
            lst_Countries.Items.Clear()
            'jrs 2012/02/06 On Error GoTo ErrorHandler
            'db = DAODBEngine_definst.OpenDatabase(session("IFs_MDB2Active"))
            db = Session("IFs_MDB2Active")
            'JRS 11/2/1

            'jrs 2011/12/20
            ''mtiwebform June 2004 Longitudinal
            ''If Not bGrouping Then
            'If Not Session("bCOuntSelGrouping") Then
            '    'Valtbl = db.OpenRecordset(Regionalization)

            '    'Valtbl.index = "Country" '"PrimaryKey"
            '    SetDataTablefromAccess(db, "Select * from " + Regionalization + " order by Country", "", Valtbl)
            'Else
            '    'Valtbl = db.OpenRecordset(GroupIDNew)
            '    'Valtbl.index = "GroupName"
            '    SetDataTablefromAccess(db, "Select * from " + GroupIDNew + " order by GroupName", "", Valtbl)
            'End If
            SetDataTablefromAccess(db, "Select * from " + Regionalization + " order by Country", "", Valtbl)

            '   Open Cohort Wave Table
            '   ----------------------

            'db2 = DAODBEngine_definst.OpenDatabase(IFsWVSCohort_MDB)
            'Dim valtbl2 As DAO.Recordset

            db2 = Session("IFsWVSCohort_MDB")
            Dim valtbl2 As DataTable 'DAO.Recordset
            '   Count countries and load list box with those that have data for any wave
            '   ------------------------------------------------------------------------
            'Valtbl.MoveFirst()
            ICount = 1

            'jrs 2022/08/16
            'Declare a Datatable object
            Dim dt As DataTable
            Dim conn As OleDb.OleDbConnection
            If UseOnlySQLite Then
                'using sqlite_master instead of sqlite_schema
                SetDataTablefromSQLite(db2, "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'", "", dt)
            Else
                'JRS 08/27/04
                conn = CreateConnection(db2)
                conn.Open()
                'Query for getting the list of all tables from the schema, no restriction
                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
            End If
            dt.PrimaryKey = New DataColumn() {dt.Columns("TABLE_NAME")}

            'jrs 2008/12/08
            ReDim CountryName(Valtbl.Rows.Count)

            Dim sTable As String
            'Do
            For Rcnt = 0 To Valtbl.Rows.Count - 1
                'jrs 2011/12/20
                ''JRS 11/2/01 GROUPING
                ''If Not bGrouping Then
                'If Not Session("bCOuntSelGrouping") Then
                '    'CountryName(ICount) = Valtbl.Fields("country").Value
                '    CountryName(ICount) = Valtbl.Rows(Rcnt).Item("country")
                'Else
                '    'CountryName(ICount) = Valtbl.Fields("GroupName").Value
                '    CountryName(ICount) = Valtbl.Rows(Rcnt).Item("GroupName")
                'End If
                CountryName(ICount) = Valtbl.Rows(Rcnt).Item("country")
                If CBool(Session("WVSWave")) = True Then
                    NoData = CStr(True)
                Else
                    NoData = CStr(False)
                End If
                If CBool(Session("WVSWave")) = True Then


                    For icount2 = 1 To 5 '5 waves
                        'KIA 11/06/09 replace the following line, becuase we are not using  Session("VarNameDep") anymore. So replace it with Session("LV_lbl_Dep") 
                        TableName = Session("LV_lbl_Dep") & "W" & Trim(Str(icount2))
                        'JRS 01/29/04
                        'jrs 2012/02/06 On Error GoTo TryAnotherWave 'No data table for this wave
                        'valtbl2 = db2.OpenRecordset(TableName)
                        'valtbl2.index = "Country"
                        sTable = SeekValue(dt, TableName, "TABLE_NAME")
                        If sTable > "" Then
                            'jrs 2012/02/06 SetDataTablefromAccess(db2, "Select * from " + TableName, "Country", valtbl2)
                            Try
                                valtbl2 = IFsTables.Item("Select * from " & TableName)
                            Catch ex As ArgumentException
                                'jrs 2012/01/31 avoid duplicate keys
                                SyncLock IFsTables
                                    If Not IFsTables.Contains("Select * from " & TableName) Then
                                        SetDataTablefromAccess(db2, "Select * from " & TableName, "Country", valtbl2)
                                        IFsTables.Add(valtbl2, "Select * from " & TableName)
                                    Else
                                        valtbl2 = IFsTables.Item("Select * from " & TableName)
                                    End If
                                End SyncLock
                            End Try

                            'valtbl2.Seek("=", CountryName(ICount))
                            '                Target$ = "Country = " + CountryList$(ICount%)
                            '                valtbl2.FindFirst Target$

                            'If Not (IsDBNull(valtbl2.Fields("Total").Value)) Then
                            If SeekValue(valtbl2, CountryName(ICount), "Total") <> "" Then
                                NoData = CStr(False)
                            End If
                        End If
                        ' valtbl2.Close()
                        'jrs 2012/02/06 TryAnotherWave:
                    Next icount2
                    'jrs 2022/08/16
                    If Not UseOnlySQLite Then
                        conn.Close()
                    End If
                End If

                If CBool(NoData) = False Then
                    lst_Countries.Items.Add(New ListItem(CountryName(ICount)))
                    'jrs 2011/12/20
                    lst_selections.Items.Add(New ListItem(CountryName(ICount)))
                    ICount = ICount + 1
                End If
                'Valtbl.MoveNext()
                'If Valtbl.EOF = True Then Exit Do
                'Loop
            Next Rcnt
            Session("CCount") = ICount - 1
            'Valtbl.Close()
            'db.Close()
            'db2.Close()

            'jrs 2011/12/20
            Dim i As Integer
            For i = 1 To GCount
                lst_Countries.Items.Add(New ListItem(GroupNames(i)))
                lst_selections.Items.Add(New ListItem(GroupNames(i)))
            Next i
            txtSelLocations.Value = ""

            'jrs 2007/10/02
            If Session("CountryTot") > 0 Then
                For i = 1 To Session("CountryTot")
                    'jrs 2011/12/20
                    '        lst_selections.Items.Add(Session("CountryList")(i))
                    If txtSelLocations.Value > "" Then
                        txtSelLocations.Value = txtSelLocations.Value & ";"
                    End If
                    txtSelLocations.Value = txtSelLocations.Value & Session("CountryList")(i)
                Next i
            End If

            Session("CountriesSelected") = "None"
            'jrs 2011/12/20
            Session("CountryName") = CountryName

            Exit Sub
            'jrs 2012/02/06
            'ErrorHandler:
            '                Select Case Err.Number
            '                    Case 3024
            '                        Msg = "Professional features require Access data files - "
            '                        Msg = Msg & "It appears that you do not have IFS_MDB "
            '                        Msg = Msg & " or it is not located on DATA subdirectory.  "
            '                        Msg = Msg & "You may not have the Professional Edition."
            '                    Case Else : Msg = "Error " & Str(Err.Number) & " occurred."
            '                End Select
            '                LoadMessage(Msg, Session("Message"))
            '                Support.FormManager.StateStore(Me)
            '                Support.FormManager.Show(Me, "frm_Message.aspx")
            '                Exit Sub

        Else 'jrs 2011/12/20
            CountryName = Session("CountryName")
        End If


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
    Protected WithEvents WinFormsToWeb_MessageBox As ArtinSoft.WinFormsToWeb.WebControls.MessageBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_CountSelPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_CountSel
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_CountSel()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    '
    '------------------------------------------CountSel.Frm------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '   Copyright by Barry B. Hughes 1988-1998
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last changed by BBH on 8/18/99
    '

    'jrs 2008/12/08 flex size
    Dim CountryName() As String 'jrs 2008/05/14 up from 200 to 250
    'mtiwebform June 2004 Longitudinal - do whole page
    Dim db As String
    Dim Valtbl As DataTable
    'JRS 11/2/01 GROUPING

    '   'mtiwebform June 2004 Longitudinal -take it to global.asax (to be changed from Longvar)
    'Dim bGrouping As Boolean


    '=============================CountSel.Frm==============================
    Private Sub cmd_Exit2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Exit2.Click
        'jrs 2011/12/20
        Session("CountryTot") = 0
        Dim locs() As String
        locs = Split(txtSelLocations.Value, ";")
        Dim i As Integer

        For i = 0 To UBound(locs)
            'lst_Countries.Items.s()
            If locs(i) > "" Then
                Session("CountryTot") = Session("CountryTot") + 1
                Session("CountryList")(Session("CountryTot")) = locs(i) 'CountryName(RegInd)
                'jrs 2011/12/21
                'If Session("bCOuntSelGrouping") Then
                If IFsCtrys.Item(locs(i)).Index < 0 Then
                    Session("CountryListGroup")(Session("CountryTot")) = 1
                Else
                    Session("CountryListGroup")(Session("CountryTot")) = 0
                End If
            End If
        Next

        If Session("CountryTot") > 0 Then
            Session("CountriesSelected") = "Yes"
            Session("LV_lbl_CountSel") = "Yes"
        Else
            Session("CountriesSelected") = "No"
            Session("LV_lbl_CountSel") = "No"
        End If

        Support.FormManager.Close(Me)

    End Sub


    '=============================CountSel.Frm==============================
    'CONVERSION_NOTE: The code of Event 'frm_CountSel.frm_CountSel_Load' was moved to Method 'frm_CountSel.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_CountSel_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'jrs 2011/12/21
        PackJScritpBuilder()
    End Sub


    '=============================CountSel.Frm==============================
    'UPGRADE_WARNING: Event lst_Countries.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    'Private Sub lst_Countries_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lst_Countries1.SelectedIndexChanged, lst_Countries.SelectedIndexChanged
    '    Dim RegInd As Short
    '    Dim Msg As String
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    '   Bound countries for scattergram to one
    '    '   --------------------------------------
    '    '    If VarNameInd$ <> "TIME" And Session("CountryTot") >= 1 Then
    '    '        msg$ = "With both independent and dependent variables, you may"
    '    '        msg$ = msg$ + " select only one country for analysis."
    '    '        MsgBox msg$
    '    '        Exit Sub
    '    '    End If

    '    '   Add to Selection List Box
    '    '   -------------------------
    '    'UPGRADE_WARNING: Couldn't resolve default property of object Session("CountryTot"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    If Session("CountryTot") >= 10 Then
    '        Msg = "You may not select more than 10 countries for analysis."
    '        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
    '        WinFormsToWeb_MessageBox.Show(Msg)
    '    End If
    '    RegInd = lst_Countries.SelectedIndex + 1
    '    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
    '    lst_selections.Items.Add(New ListItem(CountryName(RegInd)))
    '    'UPGRADE_WARNING: Couldn't resolve default property of object Session("CountryTot"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    Session("CountryTot") = Session("CountryTot") + 1
    '    'UPGRADE_WARNING: Couldn't resolve default property of object Session("CountryTot"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    CountryList(Session("CountryTot")) = CountryName(RegInd)
    '    'CONVERSION_ISSUE: Property 'IFsGUI.frm_LongVarS.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
    '    'frm_LongVarS.DefInstance.lbl_CountSel.Text = "Yes"
    '    CountriesSelected = "Yes"

    'End Sub

    'JRS 11/2/01 GROUPING

    Property GroupingLocal() As Boolean
        Get
            GroupingLocal = Session("bCOuntSelGrouping")
        End Get
        Set(ByVal Value As Boolean)
            Session("bCOuntSelGrouping") = Value
        End Set
    End Property

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.ComboBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.KeyPress.html'.
    Private Sub lst_Countries_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        ''CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        ''JRS 1/22/02
        'Dim i As Short
        'If KeyAscii = 13 Then
        '    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '    For i = 0 To lst_Countries.Items.Count - 1
        '        If UCase(VB.Left(VB6.GetItemString(lst_Countries, i), Len(Support.ComboboxManager.getText(lst_Countries)))) = UCase(Support.ComboboxManager.getText(lst_Countries)) Then
        '            Exit For
        '        End If
        '    Next i
        '    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '    If i < lst_Countries.Items.Count Then lst_Countries.SelectedIndex = i
        'End If
        'If KeyAscii = 0 Then
        '    'CONVERSION_ISSUE: Class System.Windows.Forms.KeyPressEventArgs from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        '    eventArgs.Handled = True
        'End If
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        'jrs 2011/12/21
        'Changed only to load decomposed groups

        'Dim i As Integer
        Dim Msg As String
        Dim li As ListItem
        Dim locs() As String
        locs = Split(txtSelLocations.Value, ";")
        Dim i As Integer
        Session("CountryTot") = 0
        For i = 0 To UBound(locs)
            'lst_Countries.Items.s()
            If locs(i) > "" Then
                Session("CountryTot") = Session("CountryTot") + 1
                Session("CountryList")(Session("CountryTot")) = locs(i) 'CountryName(RegInd)
                'jrs 2011/12/21
                'If Session("bCOuntSelGrouping") Then
                If IFsCtrys.Item(locs(i)).Index < 0 Then
                    Session("CountryListGroup")(Session("CountryTot")) = 1
                Else
                    Session("CountryListGroup")(Session("CountryTot")) = 0
                End If
            End If
        Next

        If Session("CountryTot") >= NR Then
            Dim Message As String
            Message = "You may not select more than " & NR.ToString & " countries for analysis."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Dim sql As String, rsGD As DataTable, j As Integer

        For Each li In lst_Countries.Items
            'lst_Countries.Items.s()
            If li.Selected Then
                sql = "SELECT * FROM " & GroupIDNew & " i, " &
                Grouping & " d, " & Regionalization$ & " r WHERE i.GroupNum = d.GroupNo AND d.RegionNo = r.NewRegion AND i.GroupName = """ &
                li.Text & """ ORDER BY Country"
                SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "", rsGD, "")
                'lst_selections.Items.Add(li)
                If Session("CountryTot") + rsGD.Rows.Count > NR Then
                    Dim Message As String
                    Message = "You may not select more than " & NR.ToString & " countries for analysis."
                    LoadMessage(Message, Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If
                txtLastDecGroup.Value = li.Text

                For j = 1 To rsGD.Rows.Count
                    Session("CountryTot") = Session("CountryTot") + 1
                    Session("CountryList")(Session("CountryTot")) = rsGD.Rows(j - 1).Item("Country") ' li.Text 'CountryName(RegInd)
                    'jrs 2011/12/21
                    ''jrs 2008/05/15
                    'If Session("bCOuntSelGrouping") Then
                    '    Session("CountryListGroup")(Session("CountryTot")) = 1
                    'Else
                    '    Session("CountryListGroup")(Session("CountryTot")) = 0
                    'End If
                    Session("CountryListGroup")(Session("CountryTot")) = 0
                    If txtSelLocations.Value > "" Then
                        txtSelLocations.Value = txtSelLocations.Value & ";"
                    End If
                    txtSelLocations.Value = txtSelLocations.Value & rsGD.Rows(j - 1).Item("Country") 'li.Text

                Next
            End If
        Next

        If Session("CountryTot") > 0 Then
            Session("CountriesSelected") = "Yes"
            Session("LV_lbl_CountSel") = "Yes"
        Else
            Session("CountriesSelected") = "No"
            Session("LV_lbl_CountSel") = "No"
        End If

    End Sub

    Private Sub btnUnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'jrs 2008/05/15
        Dim i As Integer
        Dim bFound As Boolean

        Dim itemRemoved As Short
        Dim li As ListItem
        Do While True
            itemRemoved = 0
            For Each li In lst_selections.Items
                'lst_Countries.Items.s()
                If li.Selected Then
                    For i = 1 To Session("CountryTot")
                        If Session("CountryList")(i) = li.Text And Not bFound Then
                            bFound = True
                            Session("CountryTot") = Session("CountryTot") - 1
                        End If

                        If bFound Then
                            Session("CountryList")(i) = Session("CountryList")(i + 1)
                            Session("CountryListGroup")(i) = Session("CountryListGroup")(i + 1)
                        End If
                    Next i

                    lst_selections.Items.Remove(li)
                    itemRemoved = itemRemoved + 1
                    Exit For
                    'If lst_selections.Items.Count = 10 Then Exit For
                End If
            Next
            If itemRemoved = 0 Then Exit Do
        Loop

        If Session("CountryTot") > 0 Then
            Session("CountriesSelected") = "Yes"
            Session("LV_lbl_CountSel") = "Yes"
        Else
            Session("CountriesSelected") = "No"
            Session("LV_lbl_CountSel") = "No"
        End If

    End Sub

    'jrs 2011/12/21
    'Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
    '    lst_selections.Items.Clear()
    '    Session("CountryTot") = 0
    '    Session("CountriesSelected") = "No"
    '    Session("LV_lbl_CountSel") = "No"
    'End Sub

    Private Sub PackJScritpBuilder()
        Dim sJscript As String, i As Integer
        'Grouping variables
        sJscript = "   var bUseReg=" & LCase(Not Session("bCOuntSelGrouping")) & ";" & vbCr
        sJscript = sJscript & vbCr & "   var bUseGroup=" & LCase(Session("bCOuntSelGrouping")) & ";" & vbCr
        sJscript = sJscript & "   var bUseGroupDec=" & LCase(optGroupDec.Checked) & ";" & vbCr
        sJscript = sJscript & "   var NR=" & NR.ToString & ";" & vbCr

        sJscript = sJscript & vbCr & "   var CountryName=[];" & vbCr
        For i = 1 To Session("CCount")
            sJscript = sJscript & "    CountryName[" & i.ToString & "] = """ & CountryName(i) & """;" & vbCr
        Next
        sJscript = sJscript & "    var iCCount=" & Session("CCount") & ";" & vbCr
        sJscript = sJscript & vbCr & "   var Groups=[];" & vbCr
        For i = 1 To GCount
            sJscript = sJscript & "    Groups[" & i.ToString & "] = """ & GroupNames$(i) & """;" & vbCr
        Next
        sJscript = sJscript & "    var iGroupCount=" & GCount.ToString & ";" & vbCr

        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadLocations",
        sJscript, True)

        optGroup.Attributes.Add("onClick", "javascript:optGroup_Click()")
        optGroupDec.Attributes.Add("onClick", "javascript:optGroupDec_Click()")
        optReg.Attributes.Add("onClick", "javascript:optReg_Click()")

    End Sub
End Class

