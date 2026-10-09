Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports IFs.Data
'jam 2021/06/01
Imports Microsoft.AspNet.FriendlyUrls
'jrs 2023/06/16
Imports System.Data.SQLite
Imports Microsoft.VisualBasic.Compatibility

'Namespace IFsWeb

Partial Class frm_Report
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

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

        'jrs 09/06/04
        'grdReport.EnableViewState = False


        'Me.DbConnection1.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database L" & "ocking Mode=0;Jet OLEDB:Database Password=;Data Source=""" & Session("PutFdrvRun2") & "\IFsVar.mdb"";Password=;Jet OLEDB:Engine Type=4;Jet OLEDB:Global Bulk Transac" & "tions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:" & "SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Passw" & "ord=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compa" & "ct=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:" & "Encrypt Database=False"
        'Me.DbConnection1.ConnectionString = "DataSource=" & Session("PutFdrvRun") & "IFsVar.db;"

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If


            'JRS 10/25/04 REMOVE VARS FROM REPORT IF NOT IN IFSVAR
            'jrs 2005/01/16 updateAccessWith1Command(Session("PutFdrvRun") + "IFsVAR.MDB", "DELETE * FROM REPORT WHERE NAME NOT IN (SELECT NAME FROM IFSVAR)")

            'JRS 12/08/03
            If Session("DisplayLastYear") Is Nothing Or Session("DisplayLastYear") < FirstYearForecast Then
                Session("DisplayLastYear") = Session("LastYear")
            End If
            'JRS 2/22/03 GRID
            bSingleGrid = True
            'JRS 1/18/02
            bReport_1_2 = True
            'jrs 09/29/04
            If Not Session("bReport_1_2") Is Nothing Then
                bReport_1_2 = Session("bReport_1_2")
            End If
            'LOADING DATA BASE NAME
            'JRS 08/29/03
            sRecSource1 = "SELECT (DEFINITION & """ & " - """ & "& UNITS) AS DEF, REPORTVALUE, NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3  FROM IFSVAR WHERE REPORT = TRUE AND REPORTDIMENSION2 = NULL AND SUBMODULE="
            sRecSource1_Rpt2 = "SELECT (DEFINITION & """ & " - """ & "& UNITS) AS DEF, REPORTVALUE, NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3  FROM IFSVAR WHERE REPORT2 = TRUE AND REPORT2DIMENSION2 = NULL AND SUBMODULE="
            sRecSource3 = "SELECT (DEFINITION & """ & " - """ & "& UNITS) AS DEF, REPORTVALUE, NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3  FROM IFSVAR WHERE REPORT = TRUE AND REPORTDIMENSION2 = NULL AND (SUBMODULE=7 OR SUBMODULE=8 OR SUBMODULE=6)"
            sRecSource3_Rpt2 = "SELECT (DEFINITION & """ & " - """ & "& UNITS) AS DEF, REPORTVALUE, NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3  FROM IFSVAR WHERE REPORT2 = TRUE AND REPORT2DIMENSION2 = NULL AND (SUBMODULE=7 OR SUBMODULE=8 OR SUBMODULE=6)"
            sRecSource = "SELECT (REPORTDIMENSION2 & """
            sRecSource_Rpt2 = "SELECT (REPORT2DIMENSION2 & """
            sRecSource = sRecSource & " - """
            sRecSource_Rpt2 = sRecSource_Rpt2 & " - """
            sRecSource2 = sRecSource
            sRecSource2_Rpt2 = sRecSource_Rpt2
            sRecSource = sRecSource & "& DEFINITION & """ & " - """ & "& UNITS) AS DEF, REPORTVALUE, NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 FROM IFSVAR WHERE REPORT = TRUE AND REPORTDIMENSION2 <> NULL AND SUBMODULE="
            sRecSource_Rpt2 = sRecSource_Rpt2 & "& DEFINITION & """ & " - """ & "& UNITS) AS DEF, REPORTVALUE, NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 FROM IFSVAR WHERE REPORT2 = TRUE AND REPORT2DIMENSION2 <> NULL AND SUBMODULE="
            sRecSource2 = sRecSource2 & "& DEFINITION & """ & " - """ & "& UNITS) AS DEF, REPORTVALUE, NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 FROM IFSVAR WHERE REPORT = TRUE AND REPORTDIMENSION2 <> NULL AND (SUBMODULE=7 OR SUBMODULE=8 OR SUBMODULE=6)"
            sRecSource2_Rpt2 = sRecSource2_Rpt2 & "& DEFINITION & """ & " - """ & "& UNITS) AS DEF, REPORTVALUE, NAME, DIMENSION2, REPORTDIMENSION2, REPORT2DIMENSION2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 FROM IFSVAR WHERE REPORT2 = TRUE AND REPORT2DIMENSION2 <> NULL AND (SUBMODULE=7 OR SUBMODULE=8 OR SUBMODULE=6)"
            'JRS 2/22/03 GRID
            sRecSourceGrd = "SELECT (DEFINITION) AS DEF, * FROM Report WHERE REPORT = TRUE ORDER BY [SUBMODULE NAME], NAME, DEFINITION"
            sRecSourceGrd2 = "SELECT (DEFINITION) AS DEF, * FROM Report WHERE REPORT2 = TRUE ORDER BY [SUBMODULE NAME], NAME, DEFINITION"

            Dim multGridColNum As Integer
            multGridColNum = DsPopulation1.IFSVAR.Columns.Count - 1
            'JRS 2/22/03 GRID
            Dim singGridColNum As Integer
            singGridColNum = DsReport1.Report.Columns.Count - 1
            Dim i As Short

            With grdReport
                For i = 7 To singGridColNum
                    .Columns(i).Visible = False
                Next i
                .Columns(0).ItemStyle.Width = New System.Web.UI.WebControls.Unit(250)
            End With

            With grdAgriculture
                For i = 2 To multGridColNum
                    .Columns(i).Visible = False
                Next i
            End With
            With grdEnergy
                For i = 2 To multGridColNum
                    .Columns(i).Visible = False
                Next i
            End With
            With grdEnvironmental
                For i = 2 To multGridColNum
                    .Columns(i).Visible = False
                Next i
            End With
            With grdPolitical
                For i = 2 To multGridColNum
                    .Columns(i).Visible = False
                Next i
            End With
            With grdPopulation
                For i = 2 To multGridColNum
                    .Columns(i).Visible = False
                Next i
            End With
            With grdEconomic
                For i = 2 To multGridColNum
                    .Columns(i).Visible = False
                Next i
            End With


            '   Load Region Names Combo (Dropdown List) Box
            '   -----------------------------------------
            'JRS 3/20/02
            FileNum = 0
            If Session("ReportGroup") = True Then
                LoadGroups()
            Else
                LoadRegions()
            End If

            '   Establish Year List Box
            For i2 = Session("FirstYear") To Session("LastYear")
                Cmb_Year.Items.Add(New ListItem(LTrim(Str(i2))))
            Next i2
            RegInd = 1
            'JRS 3/19/02 DISPLAY
            LoadFiles()

            If Cmb_Year.Items.Count > 0 Then
                Cmb_Year.SelectedIndex = 0 ' Display First Item
            End If
            ' goes into Procedure summary
            If cmb_Country.Items.Count > 0 Then
                cmb_Country.SelectedIndex = 0 ' Display First Item
            End If


            'JRS 3/21/02
            iGDPSel = GDPidx
            iPopSel = POPidx

            'Guided Use
            If Session("bGuidedUse") Then
                'JRSVBNET mnuNext.Visible = True
                WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = "Step 1 - Explore the world with Base Case - " & WinFormsToWeb_PageBar.Rows(0).Cells(0).Text
                cboFiles.SelectedIndex = 1
                cboFiles.Visible = False
                Lbl_File.Visible = False
                'JRSVBNET mnuFileOpt.Visible = False
                'JRSVBNET mnuDefineReport.Visible = False
            End If

            'JRS 03/23/04
            Session("mnuView") = "mnuViewNormal"
            Session("bSingleGrid") = bSingleGrid
            If Session("bReport_1_2") Is Nothing Then
                Session("bReport_1_2") = bReport_1_2
            End If
            If Session("ReportGroup") Is Nothing Then
                Session("ReportGroup") = False
            End If
            Session("iGDPSel") = iGDPSel
            Session("iPopSel") = iPopSel
            'jrs 2023/08/23 repConn = CreateConnection(Session("PutFdrvRun") & "IFsVAR.MDB")
            repConn = CreateConnectionSQLite(Session("PutFdrvRun") & "IFsVAR.DB")
            Session("repConn") = repConn
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'jrs 2004/11/26
            LoadDS()
            'jrs 2005/02/23
            If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
                Session("VarSelCheck") = Nothing
            End If
            'jrs 2005/11/28
            Session("RptFileNum") = 0
        Else
            'jrs 2004/26/11
            DsReport1 = Session("DsReport1")
            bSingleGrid = Session("bSingleGrid")
            bReport_1_2 = Session("bReport_1_2")
            iGDPSel = Session("iGDPSel")
            iPopSel = Session("iPopSel")
            repConn = Session("repConn")
            'jrs 09/06/04
            'If Not Session("grdReport") Is Nothing Then
            '    grdReport = Session("grdReport")
            'End If
            'jrs 2005/11/28
            FileNum = Session("RptFileNum")
        End If
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
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
    'CONVERSION_NOTE: Class 'AxMSComDlg.AxCommonDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents 'JRSVBNET CMDialog1 As AxMSComDlg.AxCommonDialog 
    'CONVERSION_NOTE: Class 'IFsGUI.dsReport' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.

    'jrs 2023/06/16
    'Protected WithEvents rdAgriculture As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdAgriculture As SqliteDataReader
    'Protected WithEvents DbSelectCommand1 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand1 As SqliteCommand
    'Protected WithEvents DbConnection1 As System.Data.OleDb.OleDbConnection
    Protected WithEvents DbConnection1 As SqliteConnection
    Protected WithEvents DsAgriculture1 As dsAgriculture
    'Protected WithEvents OleDbDAEconomic As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdEconomic As SqliteDataReader
    'Protected WithEvents OleDbSelectCommand2 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand2 As SqliteCommand
    Protected WithEvents DsEconomic1 As dsEconomic
    'Protected WithEvents OleDbDAEnergy As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdEnergy As SqliteDataReader
    'Protected WithEvents OleDbSelectCommand3 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand3 As SqliteCommand
    Protected WithEvents DsEnergy1 As dsEnergy
    'Protected WithEvents OleDbDAPolitical As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdPolitical As SqliteDataReader
    'Protected WithEvents OleDbSelectCommand4 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand4 As SqliteCommand
    Protected WithEvents DsPolitical1 As dsPolitical
    'Protected WithEvents OleDbDAPopulation As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdPopulation As SqliteDataReader
    'Protected WithEvents OleDbSelectCommand5 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand5 As SqliteCommand
    Protected WithEvents DsPopulation1 As dsPopulation
    'Protected WithEvents OleDbDAReport As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdReport As SqliteDataReader
    'Protected WithEvents DbSelectCommand6 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand6 As SqliteCommand
    Protected WithEvents DsReport1 As dsReport
    'Protected WithEvents OleDbDASocial As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdSocial As SqliteDataReader
    'Protected WithEvents DbSelectCommand7 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand7 As SqliteCommand
    Protected WithEvents DsSocial1 As dsSocial

    '<System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        'Me.rdAgriculture = New System.Data.OleDb.OleDbDataAdapter
        'Me.DbSelectCommand1 = New System.Data.OleDb.OleDbCommand
        Me.DbSelectCommand1 = New SqliteCommand
        'Me.DbConnection1 = New System.Data.OleDb.OleDbConnection
        Me.DbConnection1 = New SqliteConnection
        Me.DsAgriculture1 = New dsAgriculture
        'Me.OleDbDAEconomic = New System.Data.OleDb.OleDbDataAdapter
        'Me.OleDbSelectCommand2 = New System.Data.OleDb.OleDbCommand
        Me.DsEconomic1 = New dsEconomic
        'Me.OleDbDAEnergy = New System.Data.OleDb.OleDbDataAdapter
        'Me.OleDbSelectCommand3 = New System.Data.OleDb.OleDbCommand
        Me.DsEnergy1 = New dsEnergy
        'Me.OleDbDAPolitical = New System.Data.OleDb.OleDbDataAdapter
        'Me.OleDbSelectCommand4 = New System.Data.OleDb.OleDbCommand
        Me.DsPolitical1 = New dsPolitical
        'Me.OleDbDAPopulation = New System.Data.OleDb.OleDbDataAdapter
        'Me.OleDbSelectCommand5 = New System.Data.OleDb.OleDbCommand
        Me.DsPopulation1 = New dsPopulation
        'Me.OleDbDAReport = New System.Data.OleDb.OleDbDataAdapter
        'Me.DbSelectCommand6 = New System.Data.OleDb.OleDbCommand
        Me.DsReport1 = New dsReport
        'Me.OleDbDASocial = New System.Data.OleDb.OleDbDataAdapter
        'Me.DbSelectCommand7 = New System.Data.OleDb.OleDbCommand
        Me.DsSocial1 = New dsSocial
        CType(Me.DsAgriculture1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsEconomic1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsEnergy1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsPolitical1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsPopulation1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsReport1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSocial1, System.ComponentModel.ISupportInitialize).BeginInit()

        '
        'DbConnection1
        '
        Me.DbConnection1.ConnectionString = "DataSource=" & Session("PutFdrvRun") & "IFsVar.db;"
        Me.DbConnection1.Open()
        '
        'DbSelectCommand1
        '
        Me.DbSelectCommand1 = Me.DbConnection1.CreateCommand
        Me.DbSelectCommand1.CommandText = "SELECT DEFINITION + ' - ' + UNITS AS DEF, ReportValue, NAME, DIMENSION2, ReportDi" &
        "mension2, Report2Dimension2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3  FROM IFSVAR WH" &
        "ERE (Report = ? OR Report = ?) AND (Report2 = ? OR Report2 = ?) AND (SUBMODULE =" &
        " 4)"

        Me.DbSelectCommand1.Parameters.Add(New SQLiteParameter("Report", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand1.Parameters.Add(New SQLiteParameter("Report1", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand1.Parameters.Add(New SQLiteParameter("Report2", DbType.Int32, 2, "Report2"))
        Me.DbSelectCommand1.Parameters.Add(New SQLiteParameter("Report21", DbType.Int32, 2, "Report2"))
        '
        'rdAgriculture
        '
        'Me.rdAgriculture = Me.DbSelectCommand1.ExecuteReader
        'Me.rdAgriculture.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "IFSVAR", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DEF", "DEF"), New System.Data.Common.DataColumnMapping("ReportValue", "ReportValue"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("DIMENSION2", "DIMENSION2"), New System.Data.Common.DataColumnMapping("ReportDimension2", "ReportDimension2"), New System.Data.Common.DataColumnMapping("Report2Dimension2", "Report2Dimension2"), New System.Data.Common.DataColumnMapping("AGGREGATION", "AGGREGATION"), New System.Data.Common.DataColumnMapping("Pct1stYear", "Pct1stYear"), New System.Data.Common.DataColumnMapping("PctBaseCase", "PctBaseCase")})})

        'Me.DbConnection1.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database L" &
        '"ocking Mode=0;Data Source=""" & Session("PutFdrvRun") & "IFsVar.mdb"";Jet OLEDB:En" &
        '"gine Type=4;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OL" &
        '"EDB:SFP=False;persist security info=False;Extended Properties=;Mode=ReadWrite;Je" &
        '"t OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:" &
        '"Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=Fals" &
        '"e;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1"

        '
        'DsAgriculture1
        '
        Me.DsAgriculture1.DataSetName = "dsAgriculture"
        Me.DsAgriculture1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDAEconomic
        '
        Me.DbSelectCommand2 = Me.DbConnection1.CreateCommand
        '
        'OleDbSelectCommand2
        '
        Me.DbSelectCommand2.CommandText = "SELECT DEFINITION + ' - ' + UNITS AS DEF, ReportValue, NAME, DIMENSION2, ReportDi" &
        "mension2, Report2Dimension2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 FROM IFSVAR WH" &
        "ERE (Report = ? OR Report = ?) AND (Report2 = ? OR Report2 = ?) AND (ReportDimen" &
        "sion2 IS NULL) AND (SUBMODULE = 2)"
        Me.DbSelectCommand2.Connection = Me.DbConnection1
        Me.DbSelectCommand2.Parameters.Add(New SQLiteParameter("Report", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand2.Parameters.Add(New SQLiteParameter("Report1", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand2.Parameters.Add(New SQLiteParameter("Report2", DbType.Int32, 2, "Report2"))
        Me.DbSelectCommand2.Parameters.Add(New SQLiteParameter("Report21", DbType.Int32, 2, "Report2"))
        '
        'DsEconomic1
        '
        Me.DsEconomic1.DataSetName = "dsEconomic"
        Me.DsEconomic1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDAEnergy
        '
        Me.DbSelectCommand3 = Me.DbConnection1.CreateCommand
        'Me.OleDbDAEnergy.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "IFSVAR", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DEF", "DEF"), New System.Data.Common.DataColumnMapping("ReportValue", "ReportValue"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("DIMENSION2", "DIMENSION2"), New System.Data.Common.DataColumnMapping("ReportDimension2", "ReportDimension2"), New System.Data.Common.DataColumnMapping("Report2Dimension2", "Report2Dimension2"), New System.Data.Common.DataColumnMapping("AGGREGATION", "AGGREGATION"), New System.Data.Common.DataColumnMapping("Pct1stYear", "Pct1stYear"), New System.Data.Common.DataColumnMapping("PctBaseCase", "PctBaseCase")})})
        '
        'OleDbSelectCommand3
        '
        Me.DbSelectCommand3.CommandText = "SELECT DEFINITION + ' - ' + UNITS AS DEF, ReportValue, NAME, DIMENSION2, ReportDi" &
        "mension2, Report2Dimension2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3   FROM IFSVAR WH" &
        "ERE (Report = ? OR Report = ?) AND (Report2 = ? OR Report2 = ?) AND (SUBMODULE =" &
        " 3)"
        Me.DbSelectCommand3.Connection = Me.DbConnection1
        Me.DbSelectCommand3.Parameters.Add(New SQLiteParameter("Report", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand3.Parameters.Add(New SQLiteParameter("Report1", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand3.Parameters.Add(New SQLiteParameter("Report2", DbType.Int32, 2, "Report2"))
        Me.DbSelectCommand3.Parameters.Add(New SQLiteParameter("Report21", DbType.Int32, 2, "Report2"))
        '
        'DsEnergy1
        '
        Me.DsEnergy1.DataSetName = "dsEnergy"
        Me.DsEnergy1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDAPolitical
        '
        Me.DbSelectCommand4 = Me.DbConnection1.CreateCommand
        'Me.OleDbDAPolitical.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "IFSVAR", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DEF", "DEF"), New System.Data.Common.DataColumnMapping("ReportValue", "ReportValue"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("DIMENSION2", "DIMENSION2"), New System.Data.Common.DataColumnMapping("ReportDimension2", "ReportDimension2"), New System.Data.Common.DataColumnMapping("Report2Dimension2", "Report2Dimension2"), New System.Data.Common.DataColumnMapping("AGGREGATION", "AGGREGATION"), New System.Data.Common.DataColumnMapping("Pct1stYear", "Pct1stYear"), New System.Data.Common.DataColumnMapping("PctBaseCase", "PctBaseCase")})})
        '
        'OleDbSelectCommand4
        '
        Me.DbSelectCommand4.CommandText = "SELECT DEFINITION + ' - ' + UNITS AS DEF, ReportValue, NAME, DIMENSION2, ReportDi" &
        "mension2, Report2Dimension2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 FROM IFSVAR WH" &
        "ERE (Report = ? OR Report = ?) AND (Report2 = ? OR Report2 = ?) AND (SUBMODULE =" &
        " 7 OR SUBMODULE = 8 OR SUBMODULE = 6)"
        Me.DbSelectCommand4.Connection = Me.DbConnection1
        Me.DbSelectCommand4.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand4.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report1", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand4.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report2", DbType.Int32, 2, "Report2"))
        Me.DbSelectCommand4.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report21", DbType.Int32, 2, "Report2"))
        '
        'DsPolitical1
        '
        Me.DsPolitical1.DataSetName = "dsPolitical"
        Me.DsPolitical1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDAPopulation
        '
        Me.DbSelectCommand5 = Me.DbConnection1.CreateCommand
        'Me.DbSelectCommand5.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "IFSVAR", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DEF", "DEF"), New System.Data.Common.DataColumnMapping("ReportValue", "ReportValue"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("DIMENSION2", "DIMENSION2"), New System.Data.Common.DataColumnMapping("ReportDimension2", "ReportDimension2"), New System.Data.Common.DataColumnMapping("Report2Dimension2", "Report2Dimension2"), New System.Data.Common.DataColumnMapping("AGGREGATION", "AGGREGATION"), New System.Data.Common.DataColumnMapping("Pct1stYear", "Pct1stYear"), New System.Data.Common.DataColumnMapping("PctBaseCase", "PctBaseCase")})})
        '
        'OleDbSelectCommand5
        '
        Me.DbSelectCommand5.CommandText = "SELECT DEFINITION + ' - ' + UNITS AS DEF, ReportValue, NAME, DIMENSION2, ReportDi" &
        "mension2, Report2Dimension2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 FROM IFSVAR WH" &
        "ERE (Report = ? OR Report = ?) AND (Report2 = ? OR Report2 = ?) AND (SUBMODULE =" &
        " 1)"
        Me.DbSelectCommand5.Connection = Me.DbConnection1
        Me.DbSelectCommand5.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand5.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report1", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand5.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report2", DbType.Int32, 2, "Report2"))
        Me.DbSelectCommand5.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report21", DbType.Int32, 2, "Report2"))
        '
        'DsPopulation1
        '
        Me.DsPopulation1.DataSetName = "dsPopulation"
        Me.DsPopulation1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDAReport
        '
        Me.DbSelectCommand6 = Me.DbConnection1.CreateCommand
        'Me.OleDbDAReport.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Report", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DEF", "DEF"), New System.Data.Common.DataColumnMapping("ReportValue1", "ReportValue1"), New System.Data.Common.DataColumnMapping("ReportValue2", "ReportValue2"), New System.Data.Common.DataColumnMapping("ReportValue3", "ReportValue3"), New System.Data.Common.DataColumnMapping("ReportValue4", "ReportValue4"), New System.Data.Common.DataColumnMapping("ReportValue5", "ReportValue5"), New System.Data.Common.DataColumnMapping("ReportValue6", "ReportValue6"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("DIMENSION2", "DIMENSION2"), New System.Data.Common.DataColumnMapping("ReportDimension2", "ReportDimension2"), New System.Data.Common.DataColumnMapping("Report2Dimension2", "Report2Dimension2"), New System.Data.Common.DataColumnMapping("AGGREGATION", "AGGREGATION"), New System.Data.Common.DataColumnMapping("Pct1stYear1", "Pct1stYear1"), New System.Data.Common.DataColumnMapping("Pct1stYear2", "Pct1stYear2"), New System.Data.Common.DataColumnMapping("Pct1stYear3", "Pct1stYear3"), New System.Data.Common.DataColumnMapping("Pct1stYear4", "Pct1stYear4"), New System.Data.Common.DataColumnMapping("Pct1stYear5", "Pct1stYear5"), New System.Data.Common.DataColumnMapping("Pct1stYear6", "Pct1stYear6"), New System.Data.Common.DataColumnMapping("PctBaseCase1", "PctBaseCase1"), New System.Data.Common.DataColumnMapping("PctBaseCase2", "PctBaseCase2"), New System.Data.Common.DataColumnMapping("PctBaseCase3", "PctBaseCase3"), New System.Data.Common.DataColumnMapping("PctBaseCase4", "PctBaseCase4"), New System.Data.Common.DataColumnMapping("PctBaseCase5", "PctBaseCase5"), New System.Data.Common.DataColumnMapping("PctBaseCase6", "PctBaseCase6")})})
        '
        'DbSelectCommand6
        '
        Me.DbSelectCommand6.CommandText = "SELECT DEFINITION AS DEF, ReportValue1, ReportValue2, ReportValue3, ReportValue4," &
        " ReportValue5, ReportValue6, NAME, DIMENSION2, ReportDimension2, Report2Dimensio" &
        "n2, AGGREGATION, Pct1stYear1, Pct1stYear2, Pct1stYear3, Pct1stYear4, Pct1stYear5" &
        ", Pct1stYear6, PctBaseCase1, PctBaseCase2, PctBaseCase3, PctBaseCase4, PctBaseCa" &
        "se5, PctBaseCase6, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3  FROM Report WHERE (Report = $Report OR Report = $Report1) AND (Report2 = $Report2 " &
        "OR Report2 = $Report21) ORDER BY [SUBMODULE NAME], NAME, DEFINITION"
        Me.DbSelectCommand6.Connection = Me.DbConnection1
        Me.DbSelectCommand6.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand6.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report1", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand6.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report2", DbType.Int32, 2, "Report2"))
        Me.DbSelectCommand6.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report21", DbType.Int32, 2, "Report2"))
        '
        'DsReport1
        '
        Me.DsReport1.DataSetName = "dsReport"
        Me.DsReport1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDASocial
        '
        Me.DbSelectCommand7 = Me.DbConnection1.CreateCommand
        'Me.OleDbDASocial.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "IFSVAR", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DEF", "DEF"), New System.Data.Common.DataColumnMapping("ReportValue", "ReportValue"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("DIMENSION2", "DIMENSION2"), New System.Data.Common.DataColumnMapping("ReportDimension2", "ReportDimension2"), New System.Data.Common.DataColumnMapping("Report2Dimension2", "Report2Dimension2"), New System.Data.Common.DataColumnMapping("AGGREGATION", "AGGREGATION"), New System.Data.Common.DataColumnMapping("Pct1stYear", "Pct1stYear"), New System.Data.Common.DataColumnMapping("PctBaseCase", "PctBaseCase")})})
        '
        'DbSelectCommand7
        '
        Me.DbSelectCommand7.CommandText = "SELECT DEFINITION + ' - ' + UNITS AS DEF, ReportValue, NAME, DIMENSION2, ReportDi" &
        "mension2, Report2Dimension2, AGGREGATION, Pct1stYear, PctBaseCase, DIMENSION3, REPORTDIMENSION3, REPORT2DIMENSION3 FROM IFSVAR WH" &
        "ERE (Report = ? OR Report = ?) AND (Report2 = ? OR Report2 = ?) AND (SUBMODULE =" &
        " 5)"
        Me.DbSelectCommand7.Connection = Me.DbConnection1
        Me.DbSelectCommand7.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand7.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report1", DbType.Int32, 2, "Report"))
        Me.DbSelectCommand7.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report2", DbType.Int32, 2, "Report2"))
        Me.DbSelectCommand7.Parameters.Add(New System.Data.SQLite.SQLiteParameter("Report21", DbType.Int32, 2, "Report2"))
        '
        'DsSocial1
        '
        Me.DsSocial1.DataSetName = "dsSocial"
        Me.DsSocial1.Locale = New System.Globalization.CultureInfo("en-US")
        CType(Me.DsAgriculture1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsEconomic1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsEnergy1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsPolitical1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsPopulation1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsReport1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSocial1, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_Report3Private Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_Report3
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_Report3()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    Dim DimNum, i, FileNum, SEL, i2, FiNum As Object
    Dim FinalYear, RegInd As Short
    'JRS 11/2/01 GROUPING
    Dim CountryGroup(200) As String
    Dim GroupTot As Short
    'JRS 3/21/02
    Dim iGDPSel As Short
    Dim iPopSel As Short
    Dim fbasePOP, basePOP, baseGDP, fbaseGDP, bbaseGDP As Object
    Dim bbasePOP As Single
    Dim FilesSelected As Short
    'JRS 1/18/03
    Dim bReport_1_2 As Boolean

    Dim sRecSource As String
    Dim sRecSource2 As String
    'JRS 1/18/03
    Dim sRecSource_Rpt2 As String
    Dim sRecSource2_Rpt2 As String
    Dim sRecSource1_Rpt2 As String
    Dim sRecSource3_Rpt2 As String
    'JRS 10/01/02
    Dim sRecSource1 As String
    Dim sRecSource3 As String
    'JRS 2/22/03 GRID
    Dim sRecSourceGrd As String
    Dim sRecSourceGrd2 As String
    Dim bSingleGrid As Boolean
    'JRS 3/25/03 GROUPING IN SINGLE GRID
    Dim baseGDP_Arr(6) As Single
    Dim basePOP_Arr(6) As Single
    'JRS 09/09/04 Dim fbaseGDP_Arr(6) As Single
    'JRS 09/09/04 Dim fbasePOP_Arr(6) As Single
    Dim bbaseGDP_Arr(6) As Single
    Dim bbasePOP_Arr(6) As Single
    'JRS 06/22/04
    'jrs 2023/08/23 Dim repConn As OleDb.OleDbConnection
    Dim repConn As SqliteConnection
    'jrs 2005/01/15
    Dim RECORD As Object
    'JRS 11/29/03
    'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'JRSVBNET Dim tsAg As DataGridTableStyle = New DataGridTableStyle
    'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'JRSVBNET Dim tsEc As DataGridTableStyle = New DataGridTableStyle
    'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'JRSVBNET Dim tsEn As DataGridTableStyle = New DataGridTableStyle
    'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'JRSVBNET Dim tsEy As DataGridTableStyle = New DataGridTableStyle
    'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'JRSVBNET Dim tsPo As DataGridTableStyle = New DataGridTableStyle
    'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'JRSVBNET Dim tsPp As DataGridTableStyle = New DataGridTableStyle
    'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'JRSVBNET Dim tsRp As DataGridTableStyle = New DataGridTableStyle


    '=============================Frm_report.Frm=======================
    'UPGRADE_WARNING: Event cboFiles.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub cboFiles_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFiles.SelectedIndexChanged
        Dim Number As Single
        Dim finam As String
        Dim DefaultFile As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 3/19/02 DISPLAY
        DefaultFile = cboFiles.SelectedIndex
        FileNum = DefaultFile
        'jrs 2005/11/28
        Session("RptFileNum") = FileNum
        '    If FileNum > 0 Then
        '        finam$ = PutFdrvRun$ + Mid(cboFiles.Text, InStr(cboFiles.Text, "-") + 2, Len(cboFiles.Text) - 1 - InStr(cboFiles.Text, "-"))
        '    Else
        '        finam$ = PutFdrvRun$ + Mid(cboFiles.Text, 28, Len(cboFiles.Text) - 27)
        '    End If
        If FileNum > 0 Then
            finam = Session("PutFdrvRun") & Mid(cboFiles.SelectedValue, InStr(cboFiles.SelectedValue, "-") + 2, Len(cboFiles.SelectedValue) - 1 - InStr(cboFiles.SelectedValue, "-"))
        Else
            finam = Session("PutFdrvRun") & "Working.run.db"
        End If

        'jrs 2005/02/03
        'Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
        ''RECORD = VarPoint(PosModYear)
        ''RECORD = RECORD * 4 - 3
        'RECORD = VarPoint(PosModYear) * 4 - 3
        ''UPGRADE_WARNING: Couldn't resolve default property of object RECORD. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'FileGet(fNum, Number, RECORD)
        'FinalYear = Number
        'FileClose(fNum)
        'Dim working As IStorage
        'Dim storageSvc As IStorageService = New StorageService(Nothing, finam.Replace(Session("PutFdrvRun"), ""))
        'working = storageSvc.Open(False)
        'FinalYear = working.LastYear
        ''JRS 2005/03/14
        'If Session("DisplayLastYear") = 0 Or Session("DisplayLastYear") > FinalYear Or Session("DisplayLastYear") < Session("FirstYear") Then  'jrs 2005/03/14 DisplayLastYear < FirstYearForecast Then
        '    Session("DisplayLastYear") = FinalYear
        'End If

        'If InStr(StrConv(finam, VbStrConv.Uppercase), "IFSBASEHIST.RUN") > 0 Or UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
        '    'In case of History file change First Year
        '    Session("FirstYear") = FirstYearHistory
        '    Session("LastYear") = FinalYear 'jrs 2005/03/14 LastYearHistory
        'Else
        '    Session("FirstYear") = FirstYearForecast
        '    Session("LastYear") = FinalYear 'jrs 2005/03/14 LastYearForecast
        'End If

        'JRS 2/22/03
        Dim oldyear As Short
        Dim colidx As Short
        Dim iYr As Short
        Dim stepInc As Short
        Dim startYr As Short
        Dim icol As Short
        If Not bSingleGrid Then
            'JRS 11/5/02
            oldyear = Cmb_Year.SelectedIndex
            Cmb_Year.Items.Clear()
            For i2 = Session("FirstYear") To FinalYear
                Cmb_Year.Items.Add(New ListItem(LTrim(Str(i2))))
            Next i2
            If Cmb_Year.Items.Count >= oldyear Then
                Cmb_Year.SelectedIndex = oldyear ' Display First Item
            ElseIf Cmb_Year.Items.Count > 0 Then
                Cmb_Year.SelectedIndex = 0
            End If
        Else 'DEFINE COLUMNS AND YEARS TO USE 2/22/03
            'RULES:
            'IF LAST YEAR < 2050, EVERY 10 YEARS
            'IF LAST YEAR > 2050, EVERY 20 YEARS
            'IF USING HISTORY USE EVERY 10 YEARS
            With grdReport
                .Columns(1).HeaderText = Str(Session("FirstYear"))
                .Columns(12).HeaderText = Str(Session("FirstYear"))
                .Columns(18).HeaderText = Str(Session("FirstYear"))
                .Columns(6).HeaderText = Str(Session("DisplayLastYear"))
                .Columns(17).HeaderText = Str(Session("DisplayLastYear"))
                .Columns(23).HeaderText = Str(Session("DisplayLastYear"))
                If Session("FirstYear") = Session("DisplayLastYear") Then
                    .Columns(6).Visible = False
                End If

                'jrs 2005/03/22
                Dim iInterVal As Integer
                iInterVal = Session("DisplayLastYear") - Session("FirstYear")
                If (iInterVal < 50 And iInterVal > 25) Then
                    stepInc = 10
                ElseIf (iInterVal < 26 And iInterVal > 5) Then
                    stepInc = 5
                ElseIf (iInterVal < 6) Then
                    stepInc = 1
                Else
                    stepInc = 20
                End If
                'use 10 years
                'If (Session("FirstYear") = FirstYearHistory Or (Session("DisplayLastYear") < 2050 And Session("DisplayLastYear") > 2025)) Then
                '    stepInc = 10
                '    'use 10 years
                'ElseIf (Session("FirstYear") = FirstYearHistory Or (Session("DisplayLastYear") < 2026 And Session("DisplayLastYear") > 2005)) Then
                '    stepInc = 5
                '    'use 1 year
                'ElseIf (Session("FirstYear") = FirstYearHistory Or Session("DisplayLastYear") < 2006) Then
                '    stepInc = 1
                '    'use 20 years
                'Else
                '    stepInc = 20
                'End If

                If (Session("FirstYear") Mod 10 = 0) Or
                Int(Session("FirstYear") / 10) * 10 + stepInc <= Session("FirstYear") Then 'jrs 2008/07/08
                    startYr = Session("FirstYear") + stepInc
                Else
                    startYr = Int(Session("FirstYear") / 10) * 10 + stepInc
                End If
                colidx = 2
                'check all years
                For iYr = startYr To Session("DisplayLastYear") - 1 Step stepInc
                    .Columns(colidx).HeaderText = Str(iYr)
                    'JRS 09/01/04
                    .Columns(colidx + 11).HeaderText = Str(iYr)
                    .Columns(colidx + 17).HeaderText = Str(iYr)
                    .Columns(colidx).Visible = True
                    colidx = colidx + 1
                Next iYr
                'hide rest of the columns
                For icol = colidx To 5
                    .Columns(icol).Visible = False
                Next icol
            End With
        End If
        'jrs 2005/01/30
        If cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = "checkbox.gif" Then
            mnuViewNormal_Click(Me, New System.EventArgs)
            mnuFirstYear_Click(Me, New System.EventArgs)
        ElseIf cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = "checkbox.gif" Then
            mnuViewNormal_Click(Me, New System.EventArgs)
            MnuBaseCase_Click(Me, New System.EventArgs)
        End If

        'jrs 2020/07/31 don't do it if loading the first time
        If Page.IsPostBack Then
            'display item
            cmb_Country_SelectedIndexChanged(cmb_Country, New System.EventArgs)
        End If

        cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").Enabled = Not (FileNum = 1)
        ' goes into Procedure summary
        'jrs 09/06/04
        'Session("grdReport") = grdReport
    End Sub


    '=============================Frm_report.Frm=======================
    'UPGRADE_WARNING: Event Cmb_Year.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Cmb_Year_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Cmb_Year.SelectedIndexChanged
        'JRS 06/22/04
        repConn.Open()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 3/20/02 GROUPING 'Exclude 'ALL'
        Dim j As Object
        Dim k As Short
        If cmb_Country.SelectedValue <> "" Then
            Summary(RegInd, (Val(Cmb_Year.SelectedValue) - Session("FirstYear")))
        End If
        'JRS 04/02/03 ROUND
        RoundValues()
        RefreshDisplay()
        'JRS 06/22/04
        repConn.Close()
    End Sub

    Private Sub frm_Report_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'jam 2021/06/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim othYear As String
        Dim othFile As String
        Dim qtyPAram As Integer

        cswmWebMenu.UserData = "jsolorza::976739975"

        'jam 2021/06/02
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/06/02
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")

        'jam 2021/06/19
        If Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Request.QueryString("Group")) Then


            If Not IsNothing(Request.QueryString("Country")) And Not Page.IsPostBack Then

                othCountry = Request.QueryString("Country")

            Else
                'jrs 2023/08/28 for groups
                If Not IsNothing(Request.QueryString("Group")) And Not Page.IsPostBack Then
                    othCountry = Request.QueryString("Group")
                Else
                    othCountry = ""
                End If
            End If



            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Report.aspx")


            If othCountry <> "" Then
                vurl = FriendlyUrl.Href("~/frm_Report", RepPrepareForFriendlyURL(othCountry))

                Response.Redirect(vurl)


            End If


        End If




        cswmWebMenu.ScrollEnabled = True
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_fa05"
        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"
        End If

        'jam 2021/06/20
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"
        cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl(Session("DisplayParent")) & "';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "View"
        cswmGroup1.ID = "mnuView"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Normal"
        cswmItem.ID = "mnuViewNormal"
        If Not Page.IsPostBack Or Session("mnuView") = "mnuViewNormal" Then
            'jam 2021/06/05
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/06/05
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Change from First Year"
        cswmItem.ID = "mnuFirstYear"
        If Session("mnuView") = "mnuFirstYear" Then
            'jam 2021/06/05
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Change From Base Case"
        cswmItem.ID = "mnuBaseCase"
        If Session("mnuView") = "mnuBaseCase" Then
            'jam 2021/06/02
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"
        Else
            'jam 2021/06/05
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        End If
        If FileNum = 1 Then
            cswmItem.Enabled = False
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        'jrs 2005/03/07
        cswmGroup1.Caption = "Explain Scenarios"
        cswmGroup1.ID = "Group_a0b3"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain Selected Scenario"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain File Numbering"

        '*******************************************
        Dim Msg As String
        Carret = "\n"
        carret2 = Carret + Carret
        Msg = "When you start IFs, 2 files are available:" + Carret
        Msg = Msg & "   0. The \'Working File\' - identical to IFsBase until you:" + Carret
        Msg = Msg & "      a. Change a parameter" + Carret
        Msg = Msg & "          (changes affect the working file)" + Carret
        Msg = Msg & "      b. Open a new file (Main Menu-File-Open)" + Carret
        Msg = Msg & "         (a newly opened file becomes the working file)" + Carret
        Msg = Msg & "   1. IFsBase (Base case, never changed)" & carret2
        Msg = Msg & "When you change parameters and run the model, save " + Carret
        Msg = Msg & "   the results under new file names" + Carret
        Msg = Msg & "   (Main Menu-File-Save)" & carret2
        Msg = Msg & "To display the results of saved files," + Carret
        Msg = Msg & "    select them first (Main Menu-File-Select)." + Carret
        Msg = Msg & "    Selection will assign them " + Carret
        Msg = Msg & "    sequential numbers (2,3...) for display." & carret2
        Msg = Msg & "Display variables by name[file number] (e.g. POP[3])." & carret2

        'WinFormsToWeb_MessageBox.Show(Msg, "File Explanation")
        cswmItem.OnClick = "alert('" & Msg & "');"
        '*******************************************

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Attach More Files"
        'cswmItem.Enabled = False

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Switch"
        cswmGroup1.ID = "mnuSwitch"

        cswmItem = cswmGroup1.Items.Add()
        If Session("ReportGroup") Then
            cswmItem.Caption = "Use Country"
        Else
            cswmItem.Caption = "Use Groups"
        End If
        cswmItem.ID = "mnuGroups"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuReport2"
        If bReport_1_2 Then
            If ModelLoad = "LoadSAG" Then
                cswmItem.Caption = "Switch to SAG Report"
            Else
                cswmItem.Caption = "Switch to Report 2"
            End If
        Else
            cswmItem.Caption = "Switch to Report 1"
        End If

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Switch to Multiple Grids"
        'cswmItem.ID = "mnuSingleGrid"
        'cswmItem.Enabled = False

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Extras"
        'cswmGroup1.ID = "Group_ee0f"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Display Report for Printing"
        'cswmItem.Enabled = False

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Define Report"
        'cswmItem.Enabled = False

        'jrs 2006/12/18
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,6501);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'cswmGroup1.OnMenuBarClick = "window.open('./WebHelp/ifshelp.htm#ifs_help_for_web_version/display/basic_report_web_version.htm', '__webCshStub');"



        'jam 2021/06/19
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String
        Dim vYear As String
        Dim vScenario As String

        If segments.Count = 1 Then

            vCountry = segments(0)

        Else
            vCountry = segments(0)
            Session("vCountry") = vCountry

        End If

        Session("vCountry") = vCountry

        'jam 2021/06/19
        If Session("IsGroup") = Nothing Then

            Session("IsGroup") = False

        End If



        If Not Page.IsPostBack Then
            'jrs 2005/03/15
            Session("AnnotationFile") = Nothing
            'jrs 2005/03/15
            If Not Session("ReportFileIndex") = Nothing Then
                cboFiles.SelectedIndex = Session("ReportFileIndex")
            Else 'jrs 2026/01/26
                cboFiles.SelectedIndex = 1
            End If
            cboFiles_SelectedIndexChanged(Me, New System.EventArgs)
        Else
            RefreshDisplay()
        End If

        'jam 2021/06/19
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(vCountry)) And Not Session("IsGroup") Then
            'jrs 2011/05/06
            'If Not IsNothing(Request.QueryString("Country")) Then

            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then

            If Session("MMCountry") <> vCountry Or Not Page.IsPostBack Then

                'jam 2021/06/19
                'Session("MMCountry") = Request.QueryString("Country")

                Session("MMCountry") = vCountry

                'jrs 2012/08/15 Session("ReportGroup") = False
                If Session("ReportGroup") = True Then
                    cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions()
                    Session("ReportGroup") = False
                End If

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
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
            'jam 2021/06/19
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(vCountry)) And Session("IsGroup") Then
            'jam 2021/06/19
            'If cmb_Country.SelectedValue <> Request.QueryString("Group") Then
            If cmb_Country.SelectedValue <> vCountry Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/06/19
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(vCountry, False, Me)
                If gIdx > 0 Then
                    cmb_Country.SelectedIndex = gIdx - 1
                    Session("ReportCountryIdx") = cmb_Country.SelectedIndex
                    'jam 2021/06/19
                    'Session("MMGroup") = Request.QueryString("Group")
                    Session("MMGroup") = vCountry
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("ReportGroup") = True Then
                    cmb_Country_SelectedIndexChanged(cmb_Country, New System.EventArgs)
                Else
                    mnuGroups_Click(cswmWebMenu, New Coalesys.WebMenu.ServerItemClickEventArgs(cswmWebMenu.Groups("mnuSwitch").Items(0)))
                End If
            End If
        End If
        PackJScritpBuilder()

        'KIA 63008 ADD THE FOLLOWING IF BLOCK
        If Not Session("cNumber") Is Nothing Then
            cmb_Country.SelectedIndex = CInt(Session("cNumber")) - 1
            Session.Remove("cNumber")
            'RefreshDisplay()
            Session("ReportCountryIdx") = cmb_Country.SelectedIndex
            cmb_Country_SelectedIndexChanged(Me, New System.EventArgs)
        End If

    End Sub

    Private Sub LoadFiles()
        'JRS 11/24/01 DISPLAY
        Dim fs 'As New Scripting.FileSystemObject
        Dim BASE 'As Scripting.File
        Dim currRun 'As Scripting.File
        'Dim fRunFile As Folder
        fs = Server.CreateObject("Scripting.FileSystemObject")
        'Set fRunFile = fs.GetFolder(PutFdrvRun2$)
        'fRunFile.Files returns collection of files
        'jrs 2005/03/17
        'cdv 2021/02/26 change extensions for scenario files based off mnuSQL
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = LastYearHistory Then
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run")
            End If
        Else
            If Session("mnuSQLite") = "True" Then
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run")
            End If
        End If
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboFiles.Items.Clear()
        FilesSelected = 1
        Dim sWorkingFile As String
        sWorkingFile = VB.Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboFiles.Items.Add(New ListItem("0 - " & "Working File, based on " & sWorkingFile))
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'cdv 2021/02/26 Addd some changes to how the files get named
        cboFiles.Items(cboFiles.SelectedIndex).Text = "0 - " & "Working File, based on " & sWorkingFile
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboFiles.Items.Add(New ListItem("1 - " & BASE.Name))
        FilesSelected = 1

        Dim sRunFile As String
        ' Returns filename with specified extension. If more than one *.ini
        ' file exists, the first file found is returned.
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Session("mnuSQLite") = "True" Then
            sRunFile = Dir(Session("PutFdrvRun2") & "\*.run.db")
        Else
            sRunFile = Dir(Session("PutFdrvRun2") & "\*.run")
        End If

        Do While (sRunFile <> "")
            If UCase(sRunFile) <> UCase(BASE.Name) And UCase(sRunFile) <> UCase(sWorkingFile) And UCase(sRunFile) <> "WORKING.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") & "\" & sRunFile)
                'UPGRADE_WARNING: Couldn't resolve default property of object currRun.size. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object BASE.size. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'jrs 2022/04/08 added Or StandAloneMode
                If Session("IFsRunFiles").item(sRunFile).Visible Or StandAloneMode Then 'jrs 2005/06/27
                    'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    cboFiles.Items.Add(New ListItem(Trim(Str(cboFiles.Items.Count)) & " - " & sRunFile))
                    FilesSelected = FilesSelected + 1
                    'JRS RUNFILES 5/11/02
                    ReDim Preserve Session("RunFiles")(FilesSelected)
                    Session("RunFiles")(FilesSelected) = Session("PutFdrvRun2") & "\" & sRunFile
                    'jrs 2005/02/25
                    If FilesSelected > UBound(Session("ShortSce")) Then
                        ReDim Preserve Session("ShortSce")(FilesSelected)
                    End If
                    Dim k As Integer
                    k = getLongSceIdx(sRunFile)
                    If k > -1 Then
                        Session("ShortSce")(FilesSelected) = ShortSce(k)
                    Else
                        'jrs 2008/05/13 Session("ShortSce")(FilesSelected) = Left(sRunFile, 8)
                        Session("ShortSce")(FilesSelected) = TruncateSceFileName(sRunFile)
                    End If
                End If
            End If
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            sRunFile = Dir()
        Loop

    End Sub

    Private Sub LoadRegions()
        Dim EleLoop As Object
        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        cmb_Country.Items.Clear()
        DimNum = 1

        For EleLoop = 1 To MaxVal(DimNum) - 1 'Exclude 'ALL'
            cmb_Country.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
        Next EleLoop
    End Sub

    Private Sub LoadGroups()
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_Country.Items.Clear()
        Dim valtbl As DataTable
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " ORDER BY upper(GroupName)", "GroupName", valtbl)
        Dim i As Integer
        For i = 0 To valtbl.Rows.Count - 1
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_Country.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
        Next i
    End Sub

    Private Sub LoadCountries(ByRef GroupName As String)
        'JRS 11/2/01 GROUPING
        Dim valtblG As DataTable
        Dim resG As DataTable
        'Dim resR As Recordset
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " WHERE GroupName = """ & GroupName & """", "", resG)
        Dim GroupNum As Short
        GroupNum = resG.Rows(0).Item("GroupNum")
        '    Dim valtbl As Recordset
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Grouping, "", valtblG)
        Dim iTotal As Short
        iTotal = 0

        '   Load Group Member Names List Box
        Dim i As Integer
        For i = 0 To valtblG.Rows.Count - 1
            'JRS 2/15/02 For ICount% = 1 To valtblG.RecordCount

            'JRS 2/15/02 For JCount% = 1 To valtblG.Rows(i).Item("TotalGroups")
            'JRS 2/15/02 TmpStr$ = "Group" + Trim(Str(JCount%))
            'JRS 2/15/02 If valtblG.Rows(i).Item(TmpStr$) = GroupNum Then
            If valtblG.Rows(i).Item("GroupNo") = GroupNum Then
                'Set resR = dbG.OpenRecordset("SELECT DISTINCT Country FROM " _
                '+ Regionalization$ + ", " + Grouping$ + _
                '" WHERE NewRegion = RegionNo AND Region = '" + valtblG.Rows(i).Item("Region") + "'")
                'Do While Not resR.EOF
                iTotal = iTotal + 1
                CountryGroup(iTotal) = valtblG.Rows(i).Item("Region") 'resR.Rows(i).Item("Country")
                'resR.MoveNext
                'Loop
                'resR.Close
            End If

        Next i
        GroupTot = iTotal
    End Sub


    Private Sub CleanLabels()
        Dim i As Integer
        'JRS 2/22/03
        If Not bSingleGrid Then
            'jrs 2004/11/26 updateAccessWith1Command(Session("PutFdrvRun") & "IFsVAR.MDB", ("UPDATE [IFsVar] SET [ReportValue] = 0, [Pct1stYear] = 0, PctBaseCase = 0"))
        Else
            'jrs 2004/11/26 updateAccessWith1Command(Session("PutFdrvRun") & "IFsVAR.MDB", ("UPDATE [Report] SET [ReportValue1] = 0, [ReportValue2] = 0, [ReportValue3] = 0," & "[ReportValue4] = 0, [ReportValue5] = 0, [ReportValue6] = 0, Pct1stYear1 = 0, Pct1stYear2 = 0, " & "Pct1stYear3 = 0, Pct1stYear4 = 0,Pct1stYear5 = 0,Pct1stYear6 = 0,PctBaseCase1 = 0, PctBaseCase2 = 0," & "PctBaseCase3 = 0,PctBaseCase4 = 0,PctBaseCase5 = 0,PctBaseCase6 = 0 WHERE NAME <> NULL"))
            For i = 0 To DsReport1.Tables(0).Rows.Count - 1
                If Not IsDBNull(DsReport1.Tables(0).Rows(i).Item("Name")) Then
                    DsReport1.Tables(0).Rows(i).Item("ReportValue1") = 0
                    DsReport1.Tables(0).Rows(i).Item("ReportValue2") = 0
                    DsReport1.Tables(0).Rows(i).Item("ReportValue3") = 0
                    DsReport1.Tables(0).Rows(i).Item("ReportValue4") = 0
                    DsReport1.Tables(0).Rows(i).Item("ReportValue5") = 0
                    DsReport1.Tables(0).Rows(i).Item("ReportValue6") = 0

                    DsReport1.Tables(0).Rows(i).Item("Pct1stYear1") = 0
                    DsReport1.Tables(0).Rows(i).Item("Pct1stYear2") = 0
                    DsReport1.Tables(0).Rows(i).Item("Pct1stYear3") = 0
                    DsReport1.Tables(0).Rows(i).Item("Pct1stYear4") = 0
                    DsReport1.Tables(0).Rows(i).Item("Pct1stYear5") = 0
                    DsReport1.Tables(0).Rows(i).Item("Pct1stYear6") = 0

                    DsReport1.Tables(0).Rows(i).Item("PctBaseCase1") = 0
                    DsReport1.Tables(0).Rows(i).Item("PctBaseCase2") = 0
                    DsReport1.Tables(0).Rows(i).Item("PctBaseCase3") = 0
                    DsReport1.Tables(0).Rows(i).Item("PctBaseCase4") = 0
                    DsReport1.Tables(0).Rows(i).Item("PctBaseCase5") = 0
                    DsReport1.Tables(0).Rows(i).Item("PctBaseCase6") = 0
                End If
            Next
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object baseGDP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        baseGDP = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object basePOP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        basePOP = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object fbaseGDP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        fbaseGDP = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object fbasePOP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        fbasePOP = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object bbaseGDP. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        bbaseGDP = 0
        bbasePOP = 0
        'JRS 3/25/03 GROUPING WITH SINGLE GRID
        For i = 0 To 5
            baseGDP_Arr(i) = 0
            basePOP_Arr(i) = 0
            'JRS 09/09/04 fbaseGDP_Arr(i) = 0
            'JRS 09/09/04 fbasePOP_Arr(i) = 0
            bbaseGDP_Arr(i) = 0
            bbasePOP_Arr(i) = 0
        Next i
    End Sub



    '=============================Frm_report.Frm=======================
    'UPGRADE_WARNING: Event cmb_Country.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Public Sub cmb_Country_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
        'JRS 06/22/04
        repConn.Open()

        Dim w As Object
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'System.Windows.Forms.Application.DoEvents()
        'JRS 2/24/03 GRID
        Dim iYr As Short
        Dim iFrstCol As Short
        If bSingleGrid Then
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
            Select Case (True)
                Case Session("mnuView") = "mnuViewNormal"
                    iFrstCol = 1
                Case Session("mnuView") = "mnuFirstYear"
                    iFrstCol = 12
                Case Session("mnuView") = "mnuBaseCase"
                    iFrstCol = 18
            End Select
        End If
        'JRS 03/25/04 SYNCHRONIZING WITH LAST REGION/GROUP
        If Not Page.IsPostBack And Not IsNothing(Session("ReportCountryIdx")) Then
            cmb_Country.SelectedIndex = Session("ReportCountryIdx")
        End If

        'JRS 3/20/02 GROUPING 'Exclude 'ALL'
        Dim j As Object
        Dim k As Short
        RegInd = cmb_Country.SelectedIndex + 1
        If Cmb_Year.SelectedValue <> "" Then
            If Not bSingleGrid Then
                Summary(RegInd, (Val(Cmb_Year.SelectedValue) - Session("FirstYear")))
            Else
                For iYr = 0 To 5
                    With grdReport
                        If (.Columns(iFrstCol + iYr).Visible) Then
                            Summary(RegInd, (Val(.Columns(iYr + 1).HeaderText) - Session("FirstYear")))
                        End If
                    End With
                Next iYr
            End If
        End If
        'JRS 04/02/03 ROUND
        RoundValues()
        RefreshDisplay()
        'JRS 03/25/04
        Session("ReportCountryIdx") = cmb_Country.SelectedIndex
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'JRS 06/22/04
        repConn.Close()
    End Sub

    Public Sub MnuBaseCase_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2006/07/21
        If Session("mnuView") = "mnuBaseCase" Then Exit Sub

        'JRS 2/24/03 GRID
        If Not bSingleGrid Then
            grdAgriculture.Columns(1).Visible = False
            grdAgriculture.Columns(7).Visible = False
            grdAgriculture.Columns(8).Visible = True
            grdEconomic.Columns(1).Visible = False
            grdEconomic.Columns(7).Visible = False
            grdEconomic.Columns(8).Visible = True
            grdEnvironmental.Columns(1).Visible = False
            grdEnvironmental.Columns(7).Visible = False
            grdEnvironmental.Columns(8).Visible = True
            grdEnergy.Columns(1).Visible = False
            grdEnergy.Columns(7).Visible = False
            grdEnergy.Columns(8).Visible = True
            grdPolitical.Columns(1).Visible = False
            grdPolitical.Columns(7).Visible = False
            grdPolitical.Columns(8).Visible = True
            grdPopulation.Columns(1).Visible = False
            grdPopulation.Columns(7).Visible = False
            grdPopulation.Columns(8).Visible = True
        Else
            With grdReport
                For i = 1 To 6
                    .Columns(i + 17).Visible = (.Columns(i).Visible Or .Columns(i + 11).Visible)
                    .Columns(i).Visible = False
                    .Columns(i + 11).Visible = False
                Next i
            End With
        End If
        'jam 2021/06/19
        'cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = "blank.gif"
        'cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = "blank.gif"
        'cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = "checkbox.gif"

        cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"



        Session("mnuView") = "mnuBaseCase"

    End Sub

    Public Sub mnuDefineReport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_DefineReport.aspx")
        'frm_DefineReport.DefInstance.Show() 
    End Sub

    Public Sub mnuDispRepPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim RptIFsVar2 As Object
        Dim RptIFsVar As Object
        'CONVERSION_ISSUE: Class System.Windows.Forms.Application from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Application.DoEvents()
        'RELOADING CONNECTION DATA BASE FOR DATA SOURCE
        Dim sConn As String
        sConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
        sConn = sConn & Session("PutFdrvRun2") & "\IFsVar.mdb;Persist Security Info=False"

        'JRS 08/23/03
        If bReport_1_2 Then
            'UPGRADE_ISSUE: Unload deIFs was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2029"'
            'JRSVBNET Unload(deIFs)
            'UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1039"'
            'JRSVBNET Load(deIFs)
            'JRSVBNET deIFs.deConIFs.ConnectionString = sConn
        Else
            'UPGRADE_ISSUE: Unload deIFs2 was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2029"'
            'JRSVBNET Unload(deIFs2)
            'UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1039"'
            'JRSVBNET Load(deIFs2)
            'JRSVBNET deIFs2.deConIFs.ConnectionString = sConn
        End If

        '    If deIFs.deConIFs.State = 0 Then
        '        deIFs.deConIFs.ConnectionString = sConn
        '    End If
        '
        '    If deIFs.rsSubModule.State <> 0 Then
        '        deIFs.rsSubModule.Requery
        '        DoEvents
        '    End If

        'JRS 08/22/03
        If bReport_1_2 Then
            'UPGRADE_ISSUE: Unload RptIFsVar was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2029"'
            'JRSVBNET Unload(RptIFsVar)
            'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar.Title. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RptIFsVar.Title = cmb_Country.SelectedValue & ", " & Cmb_Year.SelectedValue
            'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar.DataSource. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET RptIFsVar.DataSource = deIFs
            'JRS 08/09/02
            'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar.Refresh. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RptIFsVar.Refresh()
            If Session("bGuidedUse") Then
                'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RptIFsVar.Show(VB6.FormShowConstants.Modal)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RptIFsVar.Show()
            End If
        Else
            'UPGRADE_ISSUE: Unload RptIFsVar2 was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2029"'
            'JRSVBNET Unload(RptIFsVar2)
            'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar2.Title. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RptIFsVar2.Title = cmb_Country.SelectedValue & ", " & Cmb_Year.SelectedValue
            'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar2.DataSource. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET RptIFsVar2.DataSource = deIFs2
            'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar2.Refresh. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RptIFsVar2.Refresh()
            If Session("bGuidedUse") Then
                'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar2.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RptIFsVar2.Show(VB6.FormShowConstants.Modal)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object RptIFsVar2.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RptIFsVar2.Show()
            End If
        End If
    End Sub

    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Support.FormManager.Close(Me)
    End Sub

    Public Sub mnuFileOptAttach_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET CMDialog1.FileName = ""

        '   CancelError is True
        '   -------------------

        On Error GoTo CancelError

        'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        'JRSVBNET CMDialog1.DialogTitle = "Select for Display"
        'JRSVBNET CMDialog1.Filter = "Run Files (*.run)|*.run"
        'JRSVBNET CMDialog1.Action = 1
        If False Then 'JRSVBNET CMDialog1.FileName <> "" Then
            'JRSVBNET SelectFile(('JRSVBNET CMDialog1.FileName))
            '        msg$ = "Selection of " + 'JRSVBNET CMDialog1.Filename + " complete.  You may now "
            '        msg$ = msg$ + "choose variables from it for display."
            '        MsgBox msg$
        End If

CancelError:  ' User Pressed Cancel
        Exit Sub

    End Sub


    Public Sub mnuFirstYear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2006/07/21
        If Session("mnuView") = "mnuFirstYear" Then Exit Sub

        'JRS 2/24/03 GRID        
        If Not bSingleGrid Then
            grdAgriculture.Columns(1).Visible = False
            grdAgriculture.Columns(7).Visible = True
            grdAgriculture.Columns(8).Visible = False
            grdEconomic.Columns(1).Visible = False
            grdEconomic.Columns(7).Visible = True
            grdEconomic.Columns(8).Visible = False
            grdEnvironmental.Columns(1).Visible = False
            grdEnvironmental.Columns(7).Visible = True
            grdEnvironmental.Columns(8).Visible = False
            grdEnergy.Columns(1).Visible = False
            grdEnergy.Columns(7).Visible = True
            grdEnergy.Columns(8).Visible = False
            grdPolitical.Columns(1).Visible = False
            grdPolitical.Columns(7).Visible = True
            grdPolitical.Columns(8).Visible = False
            grdPopulation.Columns(1).Visible = False
            grdPopulation.Columns(7).Visible = True
            grdPopulation.Columns(8).Visible = False
        Else
            With grdReport
                For i = 1 To 6
                    .Columns(i + 11).Visible = (.Columns(i).Visible Or .Columns(i + 17).Visible)
                    .Columns(i).Visible = False
                    .Columns(i + 17).Visible = False
                Next i
            End With
        End If
        'jam 2021/06/19
        'cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = "blank.gif"
        'cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = "blank.gif"
        'cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = "checkbox.gif"

        cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = Page.ResolveUrl("~/Images/blank.gif") '"blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") '"checkbox.gif"


        Session("mnuView") = "mnuFirstYear"
    End Sub

    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        'jam 2021/06/19
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String



        If cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups" Then
            cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Country"
            lbl_countries.Text = "Groups"
            LoadGroups()
            Session("ReportGroup") = True
            'jam 2021/06/19
            Session("IsGroup") = True
            'jrs 2023/08/28
            If Session("MMGroup") = Nothing Then
                Session("MMGroup") = "World"
            End If
        Else
            cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"
            lbl_countries.Text = "Countries or Regions"
            LoadRegions()
            Session("ReportGroup") = False
            'jam 2021/06/19
            Session("IsGroup") = False


        End If
        'jrs 2023/08/28 cmb_Country.SelectedIndex = 0 ' Display First Item
        'jrs 2023/08/28 Session("ReportCountryIdx") = 0
        'jrs 2023/08/28 cboFiles_SelectedIndexChanged(Me, New System.EventArgs)
        'jrs 2011/05/06
        If Session("ReportGroup") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                'jam 2021/06/19
                'Support.FormManager.Show(Me, "frm_Report.aspx?Group=" & PrepareForURL(cmb_Country.SelectedValue))

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Report.aspx")


                vurl = FriendlyUrl.Href("~/frm_Report", RepPrepareForFriendlyURL(cmb_Country.SelectedValue))

                Response.Redirect(vurl)



            Else
                'Support.FormManager.Show(Me, "frm_Report.aspx?Group=" & PrepareForURL(Session("MMGroup")))



                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Report.aspx")


                vurl = FriendlyUrl.Href("~/frm_Report", RepPrepareForFriendlyURL(Session("MMGroup")))

                Response.Redirect(vurl)

            End If
        Else
            'jam 2021/06/19
            'Support.FormManager.Show(Me, "frm_Report.aspx?Country=AF")

            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Report.aspx")


            vurl = FriendlyUrl.Href("~/frm_Report", "AF")

            Response.Redirect(vurl)


        End If
        'jrs 2023/08/28
        cboFiles_SelectedIndexChanged(Me, New System.EventArgs)
    End Sub

    '=============================Frm_report.Frm=======================
    Private Sub Summary(ByRef RegionPTR As Short, ByRef YearOffset As Short)
        'jrs 2006/04/24
        Dim Vals(1) As Single, BASE(1) As Single, FYear(1) As Single

        Dim NUMBER2 As Single
        Dim Number As Single
        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Region number and Offset from First Year as parameters

        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If FileNum = 0 Then
            If Session("mnuSQLite") = "True" Then
                finam = Session("PutFdrvRun") & "working.run.db"
            Else
                finam = Session("PutFdrvRun") & "working.run"
            End If
            'JRS 11/17/02 ElseIf FileNum = 1 Then
            'JRS 11/17/02   finam$ = PutFdrvRun$ + "IFSBASE.RUN"
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            finam = Session("RunFiles")(FileNum)
        End If

        'jrs 2004/11/26 Dim rs As DataTable
        'JRS 2/22/03
        Dim srepcol As String
        srepcol = ""
        Dim icol As Short
        If Not bSingleGrid Then
            'jrs 2004/11/26 SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVAR.MDB", "SELECT * FROM IFsVar", "Name", rs)
        Else
            'jrs 2004/11/26 SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVAR.MDB", "SELECT * FROM Report", "", rs)
            'calculate report column to use
            For icol = 1 To 6
                If grdReport.Columns(icol).HeaderText = Str(Session("FirstYear") + YearOffset) Then
                    srepcol = Trim(Str(icol))
                    Exit For
                End If
            Next icol
        End If

        'JRS 12/01/03
        Dim idx As Short
        idx = 1

        Dim FirstYearVal As Object
        Dim BaseCase As Single
        Dim dim2, k2 As Object
        Dim s As String
        Dim i As Integer
        'jrs 2006/07/25
        Dim bFirstTime As Boolean = True
        Dim bLastTime As Boolean = False
        'jrs 2009/01/29
        Dim dim3 As Integer

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe
        'jrs 2020/07/31
        If Not Session("FileForDisplay" + CStr(FileNum)) Is Nothing Then
            MyFileforDisplay = Session("FileForDisplay" + CStr(FileNum))
            bFirstTime = False
        End If

        Dim MyFileforDisplay1 As IStorage
        Dim MyDF1 As IDataframe
        'jrs 2020/07/31
        If Not Session("FileForDisplay1") Is Nothing Then
            MyFileforDisplay1 = Session("FileForDisplay1")
        End If

        With DsReport1.Tables(0)
            For i = 0 To .Rows.Count - 1 'jrs 2004/11/26 .Rows.Count - 1
                'JRS 2/26/03
                'jrs 2009/02/25 If bSingleGrid And Not IsDBNull(.Rows(i).Item("Name")) Then
                If Not IsDBNull(.Rows(i).Item("Name")) Then
                    s = .Rows(i).Item("Name")
                    idx = IfsVars.Item(s).Index
                End If
                'JRS 1/18/03
                'jrs 2004/11/26 If (.Rows(i).Item("Report") Or .Rows(i).Item("Report2")) And Not IsDBNull(.Rows(i).Item("Name")) Then
                If Not IsDBNull(.Rows(i).Item("Name")) Then
                    Dim sDim2 As String, idxDim2 As Integer
                    'jrs 2009/01/29 dim2 = .Rows(i).Item("Dimension2")
                    sDim2 = ""
                    idxDim2 = -1
                    If bSingleGrid Then
                        dim2 = VDim2(idx)
                    Else
                        dim2 = .Rows(i).Item("Dimension2")
                    End If

                    If dim2 > 0 Then
                        If bReport_1_2 Then
                            'jrs 2009/01/29 sDim2 = .Rows(i).Item("ReportDimension2")
                            sDim2 = IIf(IsDBNull(.Rows(i).Item("ReportDimension2")), "", .Rows(i).Item("ReportDimension2"))
                        Else
                            'jrs 2009/01/29 sDim2 = .Rows(i).Item("Report2Dimension2")
                            sDim2 = IIf(IsDBNull(.Rows(i).Item("Report2Dimension2")), "", .Rows(i).Item("Report2Dimension2"))
                        End If
                        'jrs 2009/01/29 idxDim2 = gGetDimIdx(.Rows(i).Item("Dimension2"), sDim2, True, Me)
                        idxDim2 = gGetDimIdx(dim2, sDim2, True, Me)
                    End If
                    'jrs 2006/05/17 Dim3
                    Dim sDim3 As String, idxDim3 As Integer
                    sDim3 = ""
                    idxDim3 = -1
                    If bSingleGrid Then
                        dim3 = VDim3(idx)
                    Else
                        dim3 = .Rows(i).Item("Dimension3")
                    End If

                    'jrs 2009/01/29 If .Rows(i).Item("Dimension3") > 0 Then
                    If dim3 > 0 Then
                        If bReport_1_2 Then
                            'jrs 2009/01/29 sDim3 = .Rows(i).Item("ReportDimension3")
                            sDim3 = IIf(IsDBNull(.Rows(i).Item("ReportDimension3")), "", .Rows(i).Item("ReportDimension3"))
                        Else
                            'jrs 2009/01/29 sDim3 = .Rows(i).Item("Report2Dimension3")
                            sDim3 = IIf(IsDBNull(.Rows(i).Item("Report2Dimension3")), "", .Rows(i).Item("Report2Dimension3"))
                        End If
                        'jrs 2009/01/29 idxDim3 = gGetDimIdx(.Rows(i).Item("Dimension3"), sDim3, True, Me)
                        idxDim3 = gGetDimIdx(dim3, sDim3, True, Me)
                    End If

                    'jrs 2006/07/25
                    If i = .Rows.Count - 1 And FileNum = 1 And YearOffset = 0 Then
                        'jrs 2020/07/31 bLastTime = True
                    End If
                    'jrs 2006/04/24
                    'Load Value
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(.Rows(i).Item("Name"), idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, finam$, FileNum, bFirstTime, bLastTime,
                        Session("ReportGroup"), 1, Vals, 1, 1, Session("FirstYear") + YearOffset, Session("FirstYear") + YearOffset, YearOffset, True,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, Session("df" + .Rows(i).Item("Name") + CStr(FileNum)))
                    Else
                        gLoadForDisplay(.Rows(i).Item("Name"), idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, finam$, FileNum, bFirstTime, bLastTime,
                        Session("ReportGroup"), 1, Vals, 1, 1, Session("FirstYear") + YearOffset, Session("FirstYear") + YearOffset, YearOffset, True,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    End If
                    'Load Base
                    If FileNum <> 1 Then
                        'jrs 2006/07/25
                        If i = .Rows.Count - 1 And YearOffset = 0 Then
                            'jrs 2020/07/31 bLastTime = True
                        End If
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(.Rows(i).Item("Name"), idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, "", 1, bFirstTime, bLastTime,
                            Session("ReportGroup"), 1, BASE, 1, 1, Session("FirstYear") + YearOffset, Session("FirstYear") + YearOffset, YearOffset, True,
                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                            Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay1, MyDF1)
                        Else
                            gLoadForDisplay(.Rows(i).Item("Name"), idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, "", 1, False, bLastTime,
                            Session("ReportGroup"), 1, BASE, 1, 1, Session("FirstYear") + YearOffset, Session("FirstYear") + YearOffset, YearOffset, True,
                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                            Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        End If
                    Else
                        BASE(1) = Vals(1)
                    End If

                    bFirstTime = False
                    'Load First Year
                    If YearOffset > 0 Then
                        'jrs 2006/07/25
                        If i = .Rows.Count - 1 Then
                            'jrs 2020/07/31 bLastTime = True
                        End If
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(.Rows(i).Item("Name"), idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, finam$, FileNum, False, bLastTime,
                        Session("ReportGroup"), 1, FYear, 1, 1, Session("FirstYear"), Session("FirstYear"), 0, True,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, Session("df" + .Rows(i).Item("Name") + CStr(FileNum)))
                        Else
                            gLoadForDisplay(.Rows(i).Item("Name"), idx, cmb_Country.SelectedValue, RegionPTR, sDim2, idxDim2, sDim3, idxDim3, finam$, FileNum, False, bLastTime,
                            Session("ReportGroup"), 1, FYear, 1, 1, Session("FirstYear"), Session("FirstYear"), 0, True,
                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                            Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        End If
                    Else
                        FYear(1) = Vals(1)
                    End If

                    .Rows(i).Item("ReportValue" + srepcol) = Vals(1)
                    If FYear(1) <> 0 Then
                        .Rows(i).Item("Pct1stYear" + srepcol) = 100 * (Vals(1) - FYear(1)) / FYear(1)
                    Else
                        .Rows(i).Item("Pct1stYear" + srepcol) = 0
                    End If
                    If BASE(1) <> 0 Then
                        .Rows(i).Item("PctBaseCase" + srepcol) = 100 * (Vals(1) - BASE(1)) / BASE(1)
                    Else
                        .Rows(i).Item("PctBaseCase" + srepcol) = 0
                    End If

                End If
                idx = idx + 1
            Next i
        End With

        'jrs 2020/07/31
        If Session("FileForDisplay" + CStr(FileNum)) Is Nothing And Not (MyFileforDisplay Is Nothing) Then
            Session("FileForDisplay" + CStr(FileNum)) = MyFileforDisplay
        End If
        If Session("FileForDisplay1") Is Nothing And Not (MyFileforDisplay1 Is Nothing) Then
            Session("FileForDisplay1") = MyFileforDisplay1
        End If


    End Sub

    Private Sub RefreshDisplay()
        'JRS 2/22/03
        If Not bSingleGrid Then
            If bReport_1_2 Then
                With DbSelectCommand1
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 1
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 0
                End With
                With DbSelectCommand2
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 1
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 0
                End With
                With DbSelectCommand3
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 1
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 0
                End With
                With DbSelectCommand4
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 1
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 0
                End With
                With DbSelectCommand5
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 1
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 0
                End With
                With DbSelectCommand6
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 1
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 0
                End With
            Else
                With DbSelectCommand1
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 0
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 1
                End With
                With DbSelectCommand2
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 0
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 1
                End With
                With DbSelectCommand3
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 0
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 1
                End With
                With DbSelectCommand4
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 0
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 1
                End With
                With DbSelectCommand5
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 0
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 1
                End With
                With DbSelectCommand7
                    .Parameters(0).Value = 1
                    .Parameters(1).Value = 0
                    .Parameters(2).Value = 1
                    .Parameters(3).Value = 1
                End With
            End If
            DsAgriculture1.Clear()
            rdAgriculture = DbSelectCommand1.ExecuteReader

            Dim myTbl As DataTable = New DataTable
            DsAgriculture1.EnforceConstraints = False
            DsAgriculture1.Tables.Add(myTbl)
            DsAgriculture1.Tables(0).Load(rdAgriculture)

            grdAgriculture.DataBind()
            DsEconomic1.Clear()
            rdEconomic = DbSelectCommand2.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            DsEconomic1.EnforceConstraints = False
            DsEconomic1.Tables.Add(myTbl2)
            DsEconomic1.Tables(0).Load(rdEconomic)

            grdEconomic.DataBind()
            DsEnergy1.Clear()
            rdEnergy = DbSelectCommand3.ExecuteReader

            Dim myTbl3 As DataTable = New DataTable
            DsEnergy1.EnforceConstraints = False
            DsEnergy1.Tables.Add(myTbl3)
            DsEnergy1.Tables(0).Load(rdEnergy)

            grdEnergy.DataBind()
            DsPolitical1.Clear()
            rdPolitical = DbSelectCommand4.ExecuteReader

            Dim myTbl4 As DataTable = New DataTable
            DsPolitical1.EnforceConstraints = False
            DsPolitical1.Tables.Add(myTbl4)
            DsPolitical1.Tables(0).Load(rdPolitical)

            grdPolitical.DataBind()
            DsPopulation1.Clear()
            rdPopulation = DbSelectCommand5.ExecuteReader

            Dim myTbl5 As DataTable = New DataTable
            DsPopulation1.EnforceConstraints = False
            DsPopulation1.Tables.Add(myTbl5)
            DsPopulation1.Tables(0).Load(rdPopulation)

            grdPopulation.DataBind()
            DsSocial1.Clear()
            rdSocial = DbSelectCommand7.ExecuteReader

            Dim myTbl7 As DataTable = New DataTable
            DsSocial1.EnforceConstraints = False
            DsSocial1.Tables.Add(myTbl7)
            DsSocial1.Tables(0).Load(rdSocial)

            grdEnvironmental.DataBind()

            'dataPopulation.Refresh()
            'dataAgriculture.Refresh()
            'dataEconomic.Refresh()
            'dataEnergy.Refresh()
            'dataPolitical.Refresh()
            'dataSocial.Refresh()
            'grdAgriculture.CtlRefresh()
            'grdEconomic.CtlRefresh()
            'grdEnergy.CtlRefresh()
            'grdPolitical.CtlRefresh()
            'grdPopulation.CtlRefresh()
            'grdEnvironmental.CtlRefresh()
        Else
            'jrs 2004/11/26
            'If bReport_1_2 Then
            '    With OleDbDAReport.SelectCommand
            '        .Parameters(0).Value = True
            '        .Parameters(1).Value = True
            '        .Parameters(2).Value = True
            '        .Parameters(3).Value = False
            '    End With
            'Else
            '    With OleDbDAReport.SelectCommand
            '        .Parameters(0).Value = True
            '        .Parameters(1).Value = False
            '        .Parameters(2).Value = True
            '        .Parameters(3).Value = True
            '    End With
            'End If
            'DsReport1.Clear()
            'OleDbDAReport.Fill(DsReport1)

            'jrs 2009/02/26
            Dim i As Integer
            With DsReport1.Tables(0)
                For i = 0 To .Rows.Count - 1
                    If bReport_1_2 And Not IsDBNull(.Rows(i).Item("ReportDimension2")) Then
                        If InStr(.Rows(i).Item("DEF"), .Rows(i).Item("ReportDimension2")) <> 1 And .Rows(i).Item("ReportDimension2") <> "null" Then
                            .Rows(i).Item("DEF") = .Rows(i).Item("ReportDimension2") & " - " & .Rows(i).Item("DEF")
                        End If
                    ElseIf Not bReport_1_2 And Not IsDBNull(.Rows(i).Item("Report2Dimension2")) Then
                        If InStr(.Rows(i).Item("DEF"), .Rows(i).Item("Report2Dimension2")) <> 1 And .Rows(i).Item("Report2Dimension2") <> "null" Then
                            .Rows(i).Item("DEF") = .Rows(i).Item("Report2Dimension2") & " - " & .Rows(i).Item("DEF")
                        End If
                    End If
                Next i
            End With

            grdReport.DataBind()

            'DataReport.Refresh()
            'grdReport.CtlRefresh()
        End If
    End Sub

    'jrs 2004/11/26
    Private Sub LoadDS()
        If bReport_1_2 Then
            With DbSelectCommand6
                .Parameters(0).Value = 1
                .Parameters(1).Value = 1
                .Parameters(2).Value = 1
                .Parameters(3).Value = 0
            End With
        Else
            With DbSelectCommand6
                .Parameters(0).Value = 1
                .Parameters(1).Value = 0
                .Parameters(2).Value = 1
                .Parameters(3).Value = 1
            End With
        End If
        DsReport1.Clear()
        rdReport = DbSelectCommand6.ExecuteReader
        'Dim myTables() As DataTable
        'ReDim myTables(0)
        'myTables(0) = New DataTable
        Dim myTbl As DataTable = New DataTable
        DsReport1.EnforceConstraints = False
        'DsReport1.Tables.Add(myTables(0))
        DsReport1.Tables.Add(myTbl)
        DsReport1.Tables(0).Load(rdReport)
        'DsReport1.Load(rdReport, LoadOption.OverwriteChanges, myTables)

        Session("DsReport1") = DsReport1
    End Sub


    Public Sub LoadVarAllYrs(ByRef sName As String, ByRef idim2 As Short, ByRef sReportDimension2 As String, ByVal sReportDimension3 As String, ByRef sAggregation As String, ByRef bAddCol As Boolean)
        'JRS 1/13/02
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'jrs 2005/03/18
        If UCase(sReportDimension2) = "NULL" Then
            sReportDimension2 = ""
        End If
        'jrs 2006/05/17 Dim3
        If UCase(sReportDimension3) = "NULL" Then
            sReportDimension3 = ""
        End If
        'JRS 4/1/02
        Dim idx As Short
        idx = IfsVars.Item(sName).Index 'Exclude 'ALL'
        If bAddCol Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        End If
        Session("COUNTE") = Session("COUNTE") + 1
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sName, idx, cmb_Country.SelectedValue, RegInd, sReportDimension2, -1, sReportDimension3, -1, "", FileNum, True, True, Session("ReportGroup"),
        1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))       'JRS 1/13/03
        Else
            gLoadForDisplay(sName, idx, cmb_Country.SelectedValue, RegInd, sReportDimension2, -1, sReportDimension3, -1, "", FileNum, True, True, Session("ReportGroup"),
            1, Session("PLOT"), Session("COUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
        'JRS 1/13/03
        gLoadForDisplaySaveLbls(Session("COUNTE"), idx, cmb_Country.SelectedValue, sReportDimension2, sReportDimension3, Session("ReportGroup"), FileNum, "",
        Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'JRSVBNET Load(frm_TableDisplay)
    End Sub

    '=============================Frm_report.Frm=======================
    Private Sub SelectFile(ByRef Filename As String)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jam 2021/06/20
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        For i2 = 1 To FilesSelected
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If InStr(Filename, Session("RunFiles")(i2)) Then GoTo AlreadySelected
        Next i2
        If InStr(Filename, "IFSBASE.RUN") > 0 Then GoTo AlreadySelected
        FilesSelected = FilesSelected + 1
        'JRS RUNFILES 5/11/02 If FilesSelected > 10 Then FilesSelected = 10
        'JRS RUNFILES 5/11/02
        ReDim Preserve Session("RunFiles")(FilesSelected)

        Session("RunFiles")(FilesSelected) = Filename
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboFiles.Items.Add(New ListItem(Str(FilesSelected) & " - " & Filename))
        Msg = "You have now selected '" & Filename
        Msg = Msg & "' as file number " & Str(FilesSelected)

        LoadMessage(Msg, Session("Message"))

        'jam 2021/06/20
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")


        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
        vurl = FriendlyUrl.Href("~/frm_Message")
        Response.Redirect(vurl)


AlreadySelected:
        Msg = "The file indicated has already been selected for display"

        LoadMessage(Msg, Session("Message"))
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")

        'jam 2021/06/20
        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
        vurl = FriendlyUrl.Href("~/frm_Message")
        Response.Redirect(vurl)


    End Sub


    Public Sub mnuNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("iAction") = 1
        Support.FormManager.Close(Me)
    End Sub


    Public Sub mnuReport2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRS 1/18/03
        If bReport_1_2 Then
            cswmWebMenu.Groups("mnuSwitch").Items("mnuReport2").Caption = "Switch to Report 1"
            bReport_1_2 = False
            'JRS 2/24/03 GRID
            'UPGRADE_ISSUE: Data property dataAgriculture.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataAgriculture.RecordSource = sRecSource_Rpt2 & "4 UNION " & sRecSource1_Rpt2 & "4"
            ''UPGRADE_ISSUE: Data property dataEconomic.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataEconomic.RecordSource = sRecSource_Rpt2 & "2 UNION " & sRecSource1_Rpt2 & "2"
            ''UPGRADE_ISSUE: Data property dataEnergy.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataEnergy.RecordSource = sRecSource_Rpt2 & "3 UNION " & sRecSource1_Rpt2 & "3"
            ''UPGRADE_ISSUE: Data property dataPolitical.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataPolitical.RecordSource = sRecSource2_Rpt2 & " UNION " & sRecSource3_Rpt2
            ''UPGRADE_ISSUE: Data property dataPopulation.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataPopulation.RecordSource = sRecSource_Rpt2 & "1 UNION " & sRecSource1_Rpt2 & "1"
            ''UPGRADE_ISSUE: Data property dataSocial.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataSocial.RecordSource = sRecSource_Rpt2 & "5 UNION " & sRecSource1_Rpt2 & "5"
            ''UPGRADE_ISSUE: Data property DataReport.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'DataReport.RecordSource = sRecSourceGrd2
        Else
            If ModelLoad = "LoadSAG" Then
                cswmWebMenu.Groups("mnuSwitch").Items("mnuReport2").Caption = "Switch to SAG Report"
            Else
                cswmWebMenu.Groups("mnuSwitch").Items("mnuReport2").Caption = "Switch to Report 2"
            End If
            bReport_1_2 = True
            'JRS 2/24/03 GRID
            'UPGRADE_ISSUE: Data property dataAgriculture.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataAgriculture.RecordSource = sRecSource & "4 UNION " & sRecSource1 & "4"
            ''UPGRADE_ISSUE: Data property dataEconomic.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataEconomic.RecordSource = sRecSource & "2 UNION " & sRecSource1 & "2"
            ''UPGRADE_ISSUE: Data property dataEnergy.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataEnergy.RecordSource = sRecSource & "3 UNION " & sRecSource1 & "3"
            ''UPGRADE_ISSUE: Data property dataPolitical.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataPolitical.RecordSource = sRecSource2 & " UNION " & sRecSource3
            ''UPGRADE_ISSUE: Data property dataPopulation.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataPopulation.RecordSource = sRecSource & "1 UNION " & sRecSource1 & "1"
            ''UPGRADE_ISSUE: Data property dataSocial.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'dataSocial.RecordSource = sRecSource & "5 UNION " & sRecSource1 & "5"
            ''UPGRADE_ISSUE: Data property DataReport.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'DataReport.RecordSource = sRecSourceGrd
        End If
        'jrs 2004/11/26
        LoadDS()
        cmb_Country_SelectedIndexChanged(cmb_Country, New System.EventArgs)
        'JRS 03/24/04
        Session("bReport_1_2") = bReport_1_2
    End Sub

    Public Sub mnuSingleGrid_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '    Msg$ = "This option is still in testing and development."
        '    MsgBox Msg$

        'JRS 2/24/03 GRID
        If Not bSingleGrid Then
            cswmWebMenu.Groups("mnuSwitch").Items("mnuSingleGrid").Caption = "Switch to Multiple Grids"
            'JRSVBNET mnuDispRepPrint.Enabled = False
        Else
            cswmWebMenu.Groups("mnuSwitch").Items("mnuSingleGrid").Caption = "Switch to Single Grid"
            'JRSVBNET mnuDispRepPrint.Enabled = True
        End If
        grdAgriculture.Visible = Not grdAgriculture.Visible
        grdEconomic.Visible = Not grdEconomic.Visible
        grdEnvironmental.Visible = Not grdEnvironmental.Visible
        grdEnergy.Visible = Not grdEnergy.Visible
        grdPolitical.Visible = Not grdPolitical.Visible
        grdPopulation.Visible = Not grdPopulation.Visible
        grdReport.Visible = Not grdReport.Visible
        bSingleGrid = Not bSingleGrid
        Cmb_Year.Visible = Not Cmb_Year.Visible
        Lblyear.Visible = Not Lblyear.Visible
        cboFiles_SelectedIndexChanged(cboFiles, New System.EventArgs)
        'JRS 03/24/04
        Session("bSingleGrid") = bSingleGrid
    End Sub

    Public Sub mnuViewNormal_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2006/07/21
        If Session("mnuView") = "mnuViewNormal" Then Exit Sub

        'JRS 2/24/03 GRID
        If Not bSingleGrid Then
            grdAgriculture.Columns(1).Visible = True
            grdAgriculture.Columns(7).Visible = False
            grdAgriculture.Columns(8).Visible = False
            grdEconomic.Columns(1).Visible = True
            grdEconomic.Columns(7).Visible = False
            grdEconomic.Columns(8).Visible = False
            grdEnvironmental.Columns(1).Visible = True
            grdEnvironmental.Columns(7).Visible = False
            grdEnvironmental.Columns(8).Visible = False
            grdEnergy.Columns(1).Visible = True
            grdEnergy.Columns(7).Visible = False
            grdEnergy.Columns(8).Visible = False
            grdPolitical.Columns(1).Visible = True
            grdPolitical.Columns(7).Visible = False
            grdPolitical.Columns(8).Visible = False
            grdPopulation.Columns(1).Visible = True
            grdPopulation.Columns(7).Visible = False
            grdPopulation.Columns(8).Visible = False
        Else
            With grdReport
                For i = 1 To 6
                    .Columns(i).Visible = (.Columns(i + 11).Visible Or .Columns(i + 17).Visible)
                    .Columns(i + 11).Visible = False
                    .Columns(i + 17).Visible = False
                Next i
            End With
        End If
        cswmWebMenu.Groups("mnuView").Items("mnuBaseCase").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuFirstYear").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuView").Items("mnuViewNormal").IconImage = "checkbox.gif"

        Session("mnuView") = "mnuViewNormal"
    End Sub

    'Function to round values
    'JRS 04/02/03
    Private Sub RoundValues()
        Dim sUpd1 As String
        Dim sUpd2 As String
        Dim sUpd3 As String
        Dim sUpd4 As String
        Dim sUpd5 As String
        'JRS 09/09/04
        Dim sUpd6 As String
        If Not bSingleGrid Then
            sUpd1 = "UPDATE [IFSVAR] SET [ReportValue] = Round([ReportValue], 0)"
            sUpd1 = sUpd1 & " WHERE Abs([ReportValue]) >= 1000 AND ([Report] = true OR [Report2] = true)"
            sUpd2 = "UPDATE [IFSVAR] SET [ReportValue] = Round([ReportValue], 1)"
            sUpd2 = sUpd2 & " WHERE Abs([ReportValue]) >= 100 AND Abs([ReportValue]) < 1000 AND ([Report] = true OR [Report2] = true)"
            sUpd3 = "UPDATE [IFSVAR] SET [ReportValue] = Round([ReportValue], 2)"
            sUpd3 = sUpd3 & " WHERE Abs([ReportValue]) >= 10 AND Abs([ReportValue]) < 100 AND ([Report] = true OR [Report2] = true)"
            sUpd4 = "UPDATE [IFSVAR] SET [ReportValue] = Round([ReportValue], 3)"
            sUpd4 = sUpd4 & " WHERE Abs([ReportValue]) >= 1 AND Abs([ReportValue]) < 10 AND ([Report] = true OR [Report2] = true)"
            sUpd5 = "UPDATE [IFSVAR] SET [ReportValue] = Round([ReportValue], 4)"
            sUpd5 = sUpd5 & " WHERE Abs([ReportValue]) < 1 AND ([Report] = true OR [Report2] = true)"
            'JRS 09/09/04
            sUpd6 = "UPDATE [IFSVAR] SET [Pct1stYear] = Round([Pct1stYear], 2)"
            sUpd6 = sUpd6 + ", [PctBaseCase] = Round([PctBaseCase], 2) WHERE ([Report] = true OR [Report2] = true)"
        Else
            'jrs 2004/11/26
            With DsReport1.Tables(0)
                For i = 0 To .Rows.Count - 1
                    If Not IsDBNull(.Rows(i).Item("Name")) Then
                        If Math.Abs(.Rows(i).Item("ReportValue1")) >= 1000 Then
                            .Rows(i).Item("ReportValue1") = Math.Round(.Rows(i).Item("ReportValue1"), 0)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue1")) >= 100 Then
                            .Rows(i).Item("ReportValue1") = Math.Round(.Rows(i).Item("ReportValue1"), 1)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue1")) >= 10 Then
                            .Rows(i).Item("ReportValue1") = Math.Round(.Rows(i).Item("ReportValue1"), 2)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue1")) >= 1 Then
                            .Rows(i).Item("ReportValue1") = Math.Round(.Rows(i).Item("ReportValue1"), 3)
                        ElseIf .Rows(i).Item("ReportValue1") < 1 Then
                            .Rows(i).Item("ReportValue1") = Math.Round(.Rows(i).Item("ReportValue1"), 4)
                        End If
                        If Math.Abs(.Rows(i).Item("ReportValue2")) >= 1000 Then
                            .Rows(i).Item("ReportValue2") = Math.Round(.Rows(i).Item("ReportValue2"), 0)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue2")) >= 100 Then
                            .Rows(i).Item("ReportValue2") = Math.Round(.Rows(i).Item("ReportValue2"), 1)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue2")) >= 10 Then
                            .Rows(i).Item("ReportValue2") = Math.Round(.Rows(i).Item("ReportValue2"), 2)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue2")) >= 1 Then
                            .Rows(i).Item("ReportValue2") = Math.Round(.Rows(i).Item("ReportValue2"), 3)
                        ElseIf .Rows(i).Item("ReportValue2") < 1 Then
                            .Rows(i).Item("ReportValue2") = Math.Round(.Rows(i).Item("ReportValue2"), 4)
                        End If
                        If Math.Abs(.Rows(i).Item("ReportValue3")) >= 1000 Then
                            .Rows(i).Item("ReportValue3") = Math.Round(.Rows(i).Item("ReportValue3"), 0)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue3")) >= 100 Then
                            .Rows(i).Item("ReportValue3") = Math.Round(.Rows(i).Item("ReportValue3"), 1)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue3")) >= 10 Then
                            .Rows(i).Item("ReportValue3") = Math.Round(.Rows(i).Item("ReportValue3"), 2)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue3")) >= 1 Then
                            .Rows(i).Item("ReportValue3") = Math.Round(.Rows(i).Item("ReportValue3"), 3)
                        ElseIf .Rows(i).Item("ReportValue3") < 1 Then
                            .Rows(i).Item("ReportValue3") = Math.Round(.Rows(i).Item("ReportValue3"), 4)
                        End If
                        If Math.Abs(.Rows(i).Item("ReportValue4")) >= 1000 Then
                            .Rows(i).Item("ReportValue4") = Math.Round(.Rows(i).Item("ReportValue4"), 0)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue4")) >= 100 Then
                            .Rows(i).Item("ReportValue4") = Math.Round(.Rows(i).Item("ReportValue4"), 1)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue4")) >= 10 Then
                            .Rows(i).Item("ReportValue4") = Math.Round(.Rows(i).Item("ReportValue4"), 2)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue4")) >= 1 Then
                            .Rows(i).Item("ReportValue4") = Math.Round(.Rows(i).Item("ReportValue4"), 3)
                        ElseIf .Rows(i).Item("ReportValue4") < 1 Then
                            .Rows(i).Item("ReportValue4") = Math.Round(.Rows(i).Item("ReportValue4"), 4)
                        End If
                        If Math.Abs(.Rows(i).Item("ReportValue5")) >= 1000 Then
                            .Rows(i).Item("ReportValue5") = Math.Round(.Rows(i).Item("ReportValue5"), 0)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue5")) >= 100 Then
                            .Rows(i).Item("ReportValue5") = Math.Round(.Rows(i).Item("ReportValue5"), 1)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue5")) >= 10 Then
                            .Rows(i).Item("ReportValue5") = Math.Round(.Rows(i).Item("ReportValue5"), 2)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue5")) >= 1 Then
                            .Rows(i).Item("ReportValue5") = Math.Round(.Rows(i).Item("ReportValue5"), 3)
                        ElseIf .Rows(i).Item("ReportValue5") < 1 Then
                            .Rows(i).Item("ReportValue5") = Math.Round(.Rows(i).Item("ReportValue5"), 4)
                        End If
                        If Math.Abs(.Rows(i).Item("ReportValue6")) >= 1000 Then
                            .Rows(i).Item("ReportValue6") = Math.Round(.Rows(i).Item("ReportValue6"), 0)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue6")) >= 100 Then
                            .Rows(i).Item("ReportValue6") = Math.Round(.Rows(i).Item("ReportValue6"), 1)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue6")) >= 10 Then
                            .Rows(i).Item("ReportValue6") = Math.Round(.Rows(i).Item("ReportValue6"), 2)
                        ElseIf Math.Abs(.Rows(i).Item("ReportValue6")) >= 1 Then
                            .Rows(i).Item("ReportValue6") = Math.Round(.Rows(i).Item("ReportValue6"), 3)
                        ElseIf .Rows(i).Item("ReportValue6") < 1 Then
                            .Rows(i).Item("ReportValue6") = Math.Round(.Rows(i).Item("ReportValue6"), 4)
                        End If
                        .Rows(i).Item("Pct1stYear2") = Math.Round(.Rows(i).Item("Pct1stYear2"), 2)
                        .Rows(i).Item("Pct1stYear3") = Math.Round(.Rows(i).Item("Pct1stYear3"), 2)
                        .Rows(i).Item("Pct1stYear4") = Math.Round(.Rows(i).Item("Pct1stYear4"), 2)
                        .Rows(i).Item("Pct1stYear5") = Math.Round(.Rows(i).Item("Pct1stYear5"), 2)
                        .Rows(i).Item("Pct1stYear6") = Math.Round(.Rows(i).Item("Pct1stYear6"), 2)
                        .Rows(i).Item("PctBaseCase1") = Math.Round(.Rows(i).Item("PctBaseCase1"), 2)
                        .Rows(i).Item("PctBaseCase2") = Math.Round(.Rows(i).Item("PctBaseCase2"), 2)
                        .Rows(i).Item("PctBaseCase3") = Math.Round(.Rows(i).Item("PctBaseCase3"), 2)
                        .Rows(i).Item("PctBaseCase4") = Math.Round(.Rows(i).Item("PctBaseCase4"), 2)
                        .Rows(i).Item("PctBaseCase5") = Math.Round(.Rows(i).Item("PctBaseCase5"), 2)
                        .Rows(i).Item("PctBaseCase6") = Math.Round(.Rows(i).Item("PctBaseCase6"), 2)
                    End If
                Next
            End With
            'jrs 2004/11/26
            '    sUpd1 = "UPDATE [Report] SET " & "[ReportValue1] = Round([ReportValue1], 0)" & ", [ReportValue2] = Round([ReportValue2], 0)" & ", [ReportValue3] = Round([ReportValue3], 0)" & ", [ReportValue4] = Round([ReportValue4], 0)" & ", [ReportValue5] = Round([ReportValue5], 0)" & ", [ReportValue6] = Round([ReportValue6], 0)"
            '    sUpd1 = sUpd1 & " WHERE (Abs([ReportValue1]) >= 1000 " & "OR Abs([ReportValue2]) >= 1000 " & "OR Abs([ReportValue3]) >= 1000 " & "OR Abs([ReportValue4]) >= 1000 " & "OR Abs([ReportValue5]) >= 1000 " & "OR Abs([ReportValue6]) >= 1000) " & "AND ([Report] = true OR [Report2] = true)"

            '    sUpd2 = "UPDATE [Report] SET " & "[ReportValue1] = Round([ReportValue1], 1)" & ", [ReportValue2] = Round([ReportValue2], 1)" & ", [ReportValue3] = Round([ReportValue3], 1)" & ", [ReportValue4] = Round([ReportValue4], 1)" & ", [ReportValue5] = Round([ReportValue5], 1)" & ", [ReportValue6] = Round([ReportValue6], 1)"
            '    sUpd2 = sUpd2 & " WHERE (Abs([ReportValue1]) >= 100 " & "OR Abs([ReportValue2]) >= 100 " & "OR Abs([ReportValue3]) >= 100 " & "OR Abs([ReportValue4]) >= 100 " & "OR Abs([ReportValue5]) >= 100 " & "OR Abs([ReportValue6]) >= 100) " & "AND (Abs([ReportValue1]) < 1000 " & "AND Abs([ReportValue2]) < 1000 " & "AND Abs([ReportValue3]) < 1000 " & "AND Abs([ReportValue4]) < 1000 " & "AND Abs([ReportValue5]) < 1000 " & "AND Abs([ReportValue6]) < 1000) " & "AND ([Report] = true OR [Report2] = true)"

            '    sUpd3 = "UPDATE [Report] SET " & "[ReportValue1] = Round([ReportValue1], 2)" & ", [ReportValue2] = Round([ReportValue2], 2)" & ", [ReportValue3] = Round([ReportValue3], 2)" & ", [ReportValue4] = Round([ReportValue4], 2)" & ", [ReportValue5] = Round([ReportValue5], 2)" & ", [ReportValue6] = Round([ReportValue6], 2)"
            '    sUpd3 = sUpd3 & " WHERE (Abs([ReportValue1]) >= 10 " & "OR Abs([ReportValue2]) >= 10 " & "OR Abs([ReportValue3]) >= 10 " & "OR Abs([ReportValue4]) >= 10 " & "OR Abs([ReportValue5]) >= 10 " & "OR Abs([ReportValue6]) >= 10) " & "AND (Abs([ReportValue1]) < 100 " & "AND Abs([ReportValue2]) < 100 " & "AND Abs([ReportValue3]) < 100 " & "AND Abs([ReportValue4]) < 100 " & "AND Abs([ReportValue5]) < 100 " & "AND Abs([ReportValue6]) < 100) " & "AND ([Report] = true OR [Report2] = true)"

            '    sUpd4 = "UPDATE [Report] SET " & "[ReportValue1] = Round([ReportValue1], 3)" & ", [ReportValue2] = Round([ReportValue2], 3)" & ", [ReportValue3] = Round([ReportValue3], 3)" & ", [ReportValue4] = Round([ReportValue4], 3)" & ", [ReportValue5] = Round([ReportValue5], 3)" & ", [ReportValue6] = Round([ReportValue6], 3)"
            '    sUpd4 = sUpd4 & " WHERE (Abs([ReportValue1]) >= 1 " & "OR Abs([ReportValue2]) >= 1 " & "OR Abs([ReportValue3]) >= 1 " & "OR Abs([ReportValue4]) >= 1 " & "OR Abs([ReportValue5]) >= 1 " & "OR Abs([ReportValue6]) >= 1) " & "AND (Abs([ReportValue1]) < 10 " & "AND Abs([ReportValue2]) < 10 " & "AND Abs([ReportValue3]) < 10 " & "AND Abs([ReportValue4]) < 10 " & "AND Abs([ReportValue5]) < 10 " & "AND Abs([ReportValue6]) < 10) " & "AND ([Report] = true OR [Report2] = true)"

            '    sUpd5 = "UPDATE [Report] SET " & "[ReportValue1] = Round([ReportValue1], 4)" & ", [ReportValue2] = Round([ReportValue2], 4)" & ", [ReportValue3] = Round([ReportValue3], 4)" & ", [ReportValue4] = Round([ReportValue4], 4)" & ", [ReportValue5] = Round([ReportValue5], 4)" & ", [ReportValue6] = Round([ReportValue6], 4)"
            '    sUpd5 = sUpd5 & " WHERE (Abs([ReportValue1]) < 1 " & "AND Abs([ReportValue2]) < 1 " & "AND Abs([ReportValue3]) < 1 " & "AND Abs([ReportValue4]) < 1 " & "AND Abs([ReportValue5]) < 1 " & "AND Abs([ReportValue6]) < 1) " & "AND ([Report] = true OR [Report2] = true)"

            '    'JRS 09/09/04
            '    sUpd6 = "UPDATE [Report] SET " + _
            '    "[Pct1stYear2] = Round([Pct1stYear2], 2)" + _
            '    ", [Pct1stYear3] = Round([Pct1stYear3], 2)" + _
            '    ", [Pct1stYear4] = Round([Pct1stYear4], 2)" + _
            '    ", [Pct1stYear5] = Round([Pct1stYear5], 2)" + _
            '    ", [Pct1stYear6] = Round([Pct1stYear6], 2)" + _
            '    ", [PctBaseCase1] = Round([PctBaseCase1], 2)" + _
            '    ", [PctBaseCase2] = Round([PctBaseCase2], 2)" + _
            '    ", [PctBaseCase3] = Round([PctBaseCase3], 2)" + _
            '    ", [PctBaseCase4] = Round([PctBaseCase4], 2)" + _
            '    ", [PctBaseCase5] = Round([PctBaseCase5], 2)" + _
            '    ", [PctBaseCase6] = Round([PctBaseCase6], 2)"
            '    sUpd6 = sUpd6 + " WHERE NAME <> NULL"
        End If
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFsVAR.MDB", sUpd1)
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFsVAR.MDB", sUpd2)
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFsVAR.MDB", sUpd3)
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFsVAR.MDB", sUpd4)
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFsVAR.MDB", sUpd5)
        ''JRS 09/09/04
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFsVAR.MDB", sUpd6)
    End Sub


    'CONVERSION_ISSUE: Event 'System.Windows.Forms.DataGrid.DoubleClick' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.DoubleClick.html'.
    Private Sub grdPopulation_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fileIdx As Short
        Dim currRow As Integer
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentRowIndex' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentRowIndex.html'.
        'JRSVBNET currRow = grdPopulation.CurrentRowIndex
        'JRS 12/1/03
        Dim sReportDim2 As String, sReportDim3 As String
        Dim sAgg As String
        With DsPopulation1.IFSVAR
            'JRS 12/1/03
            If bReport_1_2 Then
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORTDIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORTDIMENSION3")
                End If
            Else
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORT2DIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORT2DIMENSION3")
                End If
            End If

            If IsDBNull(.Rows(currRow)("AGGREGATION")) Then
                sAgg = ""
            Else
                sAgg = .Rows(currRow)("AGGREGATION")
            End If
            'JRS 11/14/02
            For fileIdx = 0 To cboFiles.Items.Count - 1
                FileNum = fileIdx
                LoadVarAllYrs(.Rows(currRow)("NAME"), .Rows(currRow)("DIMENSION2"), sReportDim2, sReportDim3, sAgg, (fileIdx = 0))
            Next fileIdx
            FileNum = cboFiles.SelectedIndex
            WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = .Rows(currRow)(0)
        End With
    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.DataGrid.DoubleClick' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.DoubleClick.html'.
    Private Sub grdPolitical_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fileIdx As Short
        Dim currRow As Integer
        'JRSVBNET currRow = grdPolitical.CurrentRowIndex
        'JRS 12/1/03
        Dim sReportDim2 As String, sReportDim3 As String
        Dim sAgg As String
        With DsPolitical1.IFSVAR
            'JRS 12/1/03
            If bReport_1_2 Then
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORTDIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORTDIMENSION3")
                End If
            Else
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORT2DIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORT2DIMENSION3")
                End If
            End If
            If IsDBNull(.Rows(currRow)("AGGREGATION")) Then
                sAgg = ""
            Else
                sAgg = .Rows(currRow)("AGGREGATION")
            End If
            'JRS 11/14/02
            For fileIdx = 0 To cboFiles.Items.Count - 1
                FileNum = fileIdx
                LoadVarAllYrs(.Rows(currRow)("NAME"), .Rows(currRow)("DIMENSION2"), sReportDim2, sReportDim3, sAgg, (fileIdx = 0))
            Next fileIdx
            FileNum = cboFiles.SelectedIndex
            WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = .Rows(currRow)(0)
        End With
    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.DataGrid.DoubleClick' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.DoubleClick.html'.
    Private Sub grdEnvironmental_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fileIdx As Short
        Dim currRow As Integer
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentRowIndex' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentRowIndex.html'.
        'JRSVBNET currRow = grdEnvironmental.CurrentRowIndex
        'JRS 12/1/03
        Dim sReportDim2 As String, sReportDim3 As String
        Dim sAgg As String
        With DsSocial1.IFSVAR
            'JRS 12/1/03
            If bReport_1_2 Then
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORTDIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORTDIMENSION3")
                End If
            Else
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORT2DIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORT2DIMENSION3")
                End If
            End If
            If IsDBNull(.Rows(currRow)("AGGREGATION")) Then
                sAgg = ""
            Else
                sAgg = .Rows(currRow)("AGGREGATION")
            End If
            'JRS 11/14/02
            For fileIdx = 0 To cboFiles.Items.Count - 1
                FileNum = fileIdx
                LoadVarAllYrs(.Rows(currRow)("NAME"), .Rows(currRow)("DIMENSION2"), sReportDim2, sReportDim3, sAgg, (fileIdx = 0))
            Next fileIdx
            FileNum = cboFiles.SelectedIndex
            WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = .Rows(currRow)(0)
        End With
    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.DataGrid.DoubleClick' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.DoubleClick.html'.
    Private Sub grdEnergy_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fileIdx As Short
        Dim currRow As Integer
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentRowIndex' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentRowIndex.html'.
        'JRSVBNET currRow = grdEnergy.CurrentRowIndex
        'JRS 12/1/03
        Dim sReportDim2 As String, sReportDim3 As String
        Dim sAgg As String
        With DsEnergy1.IFSVAR
            'JRS 12/1/03
            If bReport_1_2 Then
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORTDIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORTDIMENSION3")
                End If
            Else
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORT2DIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORT2DIMENSION3")
                End If
            End If
            If IsDBNull(.Rows(currRow)("AGGREGATION")) Then
                sAgg = ""
            Else
                sAgg = .Rows(currRow)("AGGREGATION")
            End If
            'JRS 11/14/02
            For fileIdx = 0 To cboFiles.Items.Count - 1
                FileNum = fileIdx
                LoadVarAllYrs(.Rows(currRow)("NAME"), .Rows(currRow)("DIMENSION2"), sReportDim2, sReportDim3, sAgg, (fileIdx = 0))
            Next fileIdx
            FileNum = cboFiles.SelectedIndex
            WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = .Rows(currRow)(0)
        End With
    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.DataGrid.DoubleClick' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.DoubleClick.html'.
    Private Sub grdEconomic_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fileIdx As Short
        Dim currRow As Integer
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentRowIndex' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentRowIndex.html'.
        'JRSVBNET currRow = grdEconomic.CurrentRowIndex
        'JRS 12/1/03
        Dim sReportDim2 As String, sReportDim3 As String
        Dim sAgg As String
        With DsEconomic1.IFSVAR
            'JRS 12/1/03
            If bReport_1_2 Then
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORTDIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORTDIMENSION3")
                End If
            Else
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORT2DIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORT2DIMENSION3")
                End If
            End If
            If IsDBNull(.Rows(currRow)("AGGREGATION")) Then
                sAgg = ""
            Else
                sAgg = .Rows(currRow)("AGGREGATION")
            End If
            'JRS 11/14/02
            For fileIdx = 0 To cboFiles.Items.Count - 1
                FileNum = fileIdx
                LoadVarAllYrs(.Rows(currRow)("NAME"), .Rows(currRow)("DIMENSION2"), sReportDim2, sReportDim3, sAgg, (fileIdx = 0))
            Next fileIdx
            FileNum = cboFiles.SelectedIndex
            WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = .Rows(currRow)(0)
        End With
    End Sub

    'CONVERSION_ISSUE: Event 'System.Windows.Forms.DataGrid.DoubleClick' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.DoubleClick.html'.
    Private Sub grdAgriculture_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fileIdx As Short
        Dim currRow As Integer
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentRowIndex' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentRowIndex.html'.
        'JRSVBNET currRow = grdAgriculture.CurrentRowIndex
        'JRS 12/1/03
        Dim sReportDim2 As String, sReportDim3 As String
        Dim sAgg As String
        With DsAgriculture1.IFSVAR
            'JRS 12/1/03
            If bReport_1_2 Then
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORTDIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORTDIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORTDIMENSION3")
                End If
            Else
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION2")) Then
                    sReportDim2 = ""
                Else
                    sReportDim2 = .Rows(currRow)("REPORT2DIMENSION2")
                End If
                If IsDBNull(.Rows(currRow)("REPORT2DIMENSION3")) Then
                    sReportDim3 = ""
                Else
                    sReportDim3 = .Rows(currRow)("REPORT2DIMENSION3")
                End If
            End If
            If IsDBNull(.Rows(currRow)("AGGREGATION")) Then
                sAgg = ""
            Else
                sAgg = .Rows(currRow)("AGGREGATION")
            End If
            'JRS 11/14/02
            For fileIdx = 0 To cboFiles.Items.Count - 1
                FileNum = fileIdx
                LoadVarAllYrs(.Rows(currRow)("NAME"), .Rows(currRow)("DIMENSION2"), sReportDim2, sReportDim3, sAgg, (fileIdx = 0))
            Next fileIdx
            FileNum = cboFiles.SelectedIndex
            WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = .Rows(currRow)(0)
        End With
    End Sub

    Private Sub grdReport_DoubleClick(ByVal sVarName As String, ByVal sDim2Rep1 As String, ByVal sDim2Rep2 As String,
    ByVal sDim3Rep1 As String, ByVal sDim3Rep2 As String, ByVal iDim2 As Integer, ByVal sAgg As String, ByVal sDesc As String)

        Dim fileIdx As Short
        Dim dim2 As String, Dim3 As String 'jrs 2006/05/17

        'jam 2021/06/20
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'JRS 11/14/02
        'JRS 05/19/03
        For fileIdx = 0 To cboFiles.Items.Count - 1
            'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            FileNum = fileIdx
            'JRS 05/19/03
            If bReport_1_2 Then
                dim2 = sDim2Rep1
                Dim3 = sDim3Rep1
            Else
                dim2 = sDim2Rep2
                Dim3 = sDim3Rep2
            End If
            LoadVarAllYrs(sVarName, iDim2, dim2, Dim3, sAgg, (fileIdx = 0))
        Next fileIdx
        FileNum = cboFiles.SelectedIndex
        WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = sDesc
        'jrs 2011/05/06
        Dim sPar As String
        If Session("ReportGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        'JRS 03/24/04
        Session("TableDisplayParent") = "frm_Report.aspx?" & PrepareForURL(sPar)

        'jam 2021/06/20
        'Support.FormManager.StateStore(Me)

        'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


        vurl = FriendlyUrl.Href("~/frm_TableDisplay")

        Response.Redirect(vurl)


    End Sub

    Private Sub grdReport_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdReport.PageIndexChanged
        'JRSVBNET Support.DataGridManager.manager.RefreshData(grdReport)
        grdReport.CurrentPageIndex = e.NewPageIndex
        grdReport.DataBind()
    End Sub

    Private Sub grdEnvironmental_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdEnvironmental.PageIndexChanged
        'JRSVBNET Support.DataGridManager.manager.RefreshData(grdEnvironmental)
        grdEnvironmental.CurrentPageIndex = e.NewPageIndex
        grdEnvironmental.DataBind()
    End Sub

    Private Sub grdEnergy_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdEnergy.PageIndexChanged
        'JRSVBNET Support.DataGridManager.manager.RefreshData(grdEnergy)
        grdEnergy.CurrentPageIndex = e.NewPageIndex
        grdEnergy.DataBind()
    End Sub

    Private Sub grdEconomic_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdEconomic.PageIndexChanged
        'JRSVBNET Support.DataGridManager.manager.RefreshData(grdEconomic)
        grdEconomic.CurrentPageIndex = e.NewPageIndex
        grdEconomic.DataBind()
    End Sub

    Private Sub grdPolitical_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdPolitical.PageIndexChanged
        'JRSVBNET Support.DataGridManager.manager.RefreshData(grdPolitical)
        grdPolitical.CurrentPageIndex = e.NewPageIndex
        grdPolitical.DataBind()
    End Sub

    Private Sub grdPopulation_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        'JRSVBNET Support.DataGridManager.manager.RefreshData(grdPopulation)
        grdPopulation.CurrentPageIndex = e.NewPageIndex
        'grdPopulation.DataBind()
    End Sub

    Private Sub grdAgriculture_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdAgriculture.PageIndexChanged
        'JRSVBNET Support.DataGridManager.manager.RefreshData(grdAgriculture)
        grdAgriculture.CurrentPageIndex = e.NewPageIndex
        grdAgriculture.DataBind()
    End Sub

    'JRS 03/10/04
    'JRS 03/05/04
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Normal"
                mnuViewNormal_Click(sender, e)
            Case "Percent Change from First Year"
                mnuFirstYear_Click(sender, e)
            Case "Percent Change From Base Case"
                MnuBaseCase_Click(sender, e)

                    'jrs 2005/03/07
            Case "Explain Selected Scenario"
                Annotation()
            Case "Attach More Files"
                mnuFileOptAttach_Click(sender, e)
            Case "Use Groups"
                mnuGroups_Click(sender, e)
            Case "Use Country"
                mnuGroups_Click(sender, e)
            Case "Switch to Report 1"
                mnuReport2_Click(sender, e)
            Case "Switch to Report 2"
                mnuReport2_Click(sender, e)
            Case "Switch to Multiple Grids"
                mnuSingleGrid_Click(sender, e)
            Case "Switch to Single Grid"
                mnuSingleGrid_Click(sender, e)
            Case "Display Report for Printing"
                mnuDispRepPrint_Click(sender, e)
            Case "Define Report"
                mnuDefineReport_Click(sender, e)
        End Select
    End Sub

    'JRS 03/23/04
    Private Sub grdReport_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdReport.ItemCommand
        Dim sVarName As String
        Dim sDim2Rep1 As String = ""
        Dim sDim2Rep2 As String = ""
        Dim sDim3Rep1 As String = ""
        Dim sDim3Rep2 As String = ""
        Dim sAgg As String
        Dim sDesc As String

        Dim iDim2 As Integer
        sVarName = e.Item.Cells(24).Text
        If e.Item.Cells(25).Text <> "null" Then
            sDim2Rep1 = e.Item.Cells(25).Text
        End If
        If e.Item.Cells(26).Text <> "null" Then
            sDim2Rep2 = e.Item.Cells(26).Text
        End If
        'need to check where to find these values
        If e.Item.Cells(29).Text <> "null" Then
            sDim3Rep1 = e.Item.Cells(29).Text
        End If
        If e.Item.Cells(30).Text <> "null" Then
            sDim3Rep2 = e.Item.Cells(30).Text
        End If

        iDim2 = e.Item.Cells(27).Text
        sAgg = e.Item.Cells(28).Text

        sDesc = e.CommandSource.Text
        If sVarName <> "&nbsp;" Then
            grdReport_DoubleClick(sVarName, sDim2Rep1, sDim2Rep2, sDim3Rep1, sDim3Rep2, iDim2, sAgg, sDesc)
        End If
    End Sub

    'jrs 2005/03/07
    Public Sub Annotation()
        Dim sDirSess As String = "\Session"
        If Session("SessionIdx") > -1 And Session("SessionIdx") < 10 Then
            sDirSess = sDirSess & "0"
        End If
        sDirSess = sDirSess & Trim(Str(Session("SessionIdx")))
        Dim sFileName As String
        Select Case cboFiles.SelectedIndex
            Case 0
                'jrs 2005/03/24
                sFileName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text) & "WorkingAnnotation.txt"
            Case 1
                sFileName = Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text) & "IFsBase.sce"
            Case Else
                'jrs 2005/04/12
                sFileName = FindFile(Replace(Session("ScenarioFileDir"), sDirSess, "", 1, -1, CompareMethod.Text), Replace(Mid(cboFiles.SelectedValue, InStr(cboFiles.SelectedValue, "-") + 2, Len(cboFiles.SelectedValue) - 1 - InStr(cboFiles.SelectedValue, "-")), ".run", ".sce", 1, -1, vbTextCompare), "sce")
        End Select

        Session("AnnotationFile") = sFileName
        Session("ReportFileIndex") = cboFiles.SelectedIndex

        If sFileName = "" Then
            Session("AnnotationFile") = Nothing
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "Notepad.aspx")
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        If Session("ReportGroup") = False Then
            Dim sJscript As String, i As Integer
            sJscript = "    var RegCode = [];" & vbCr
            For i = 1 To NR
                sJscript = sJscript & "    RegCode[" & i.ToString & "] = """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """;" & vbCr
            Next

            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadRegCode", sJscript, True)

            cmb_Country.Attributes.Add("onchange", "cntrylist_click();")
        Else
            cmb_Country.Attributes.Add("onchange", "lstGroups_click();")
        End If
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/06/19
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter"></param>
    ''' <returns></returns>
    Public Function RepPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        RepPrepareForFriendlyURL = tmp
    End Function
End Class
