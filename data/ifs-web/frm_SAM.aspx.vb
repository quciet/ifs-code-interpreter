Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Imports IFs.Data
'jam 2021/09/01
Imports Microsoft.AspNet.FriendlyUrls
'jrs 2023/08/23
Imports System.Data.SQLite
Imports Microsoft.VisualBasic.Compatibility

'Namespace IFsWeb

Partial Class frm_SAM
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
        'CONVERSION_TODO: This code changes its behavior when moved into Web. Please revise it and consider keeping it or moving it to the Page_Load() method.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1005.html'.

        'jrs 2023/08/23 Me.OleDbConnection1.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database L" & "ocking Mode=0;Jet OLEDB:Database Password=;Data Source=""" & Session("PutFdrvRun") & "IFS.MDB"";Password=;Jet OLEDB:Engine Type=4;Jet OLEDB:Global Bulk Transactio" & "ns=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP" & "=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password" & "=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=" & "False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Enc" & "rypt Database=False"

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim i2 As Object
            Dim Number As Single
            Dim VLN As Object
            Dim finam As String
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            'JRS 2/4/03 SAM2
            bSAM2 = True
            'Initializing constants 09/03/02
            itypeVar = VDim2(IfsVars.Item("HHDIVINT").Index)
            isecVar = VDim2(IfsVars.Item("VADD").Index)
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            iType = MaxVal(itypeVar) - 1
            ReDim aSectors(NSec - 1)
            ReDim aTypes(iType - 1)
            Dim iDim As Short
            For iDim = 1 To NSec
                aSectors(iDim - 1) = GRPS(isecVar, iDim)
            Next iDim
            For iDim = 1 To iType
                aTypes(iDim - 1) = GRPS(itypeVar, iDim)
            Next iDim

            'Loading database
            'UPGRADE_ISSUE: Data property DataSAM.DatabaseName was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'JRS 12/06/03 DataSAM.DatabaseName = Session("PutFdrvRun") & "IFs.mdb"
            'UPGRADE_ISSUE: Data property DataSAM.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'JRS 12/06/03 DataSAM.RecordSource = "SAMValues"
            'JRS 03/30/04
            Session("iType") = iType
            dbgSAM.DataSource = DsSAMValues1

            'Load Regions
            If Session("SAMGroups") Is Nothing Or Session("SAMGroups") = False Then
                LoadRegions()
            Else
                LoadGroups()
            End If
            ' Check for Currency Convertion
            CurrencyCheck((IfsVars.Item("VADD")).Index)

            'JRS 12/01/02 FILES
            LoadFiles(gRetrieveFiles(Session("PutFdrvRun2"), Session("WorkingFile"), Session("RunFiles"), Session("FirstYear"), Session("FilesSelected"), Session("ShortSce"), Session("IFsRunFiles")), cboFiles)
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If cboFiles.Items.Count > 0 Then
                'jrs 2026/01/26 changed default to base
                cboFiles.SelectedIndex = 1
            End If

            '   Establish Year List Box
            'JRS 09/12/02
            If Session("mnuSQLite") = "True" Then
                finam = Session("PutFdrvRun") & "Working.run.db"
            Else
                finam = Session("PutFdrvRun") & "Working.run"
            End If
            'jrs 2005/02/14
            Dim fNum As Integer

            If Session("mnuSQLite") = "True" Then
                Dim working As IStorage
                Dim storageSvc As IStorageService = New StorageService(finam)
                working = storageSvc.Open()
                Session("VarVal") = working.LastYear
            Else
                fNum = FreeFile()
                FileOpen(fNum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared) 'jrs 2008/05/06 added OpenAccess.Read, OpenShare.Shared
                VLN = VarPoint(PosModYear)
                Session("RECORD") = (VLN) * 4 - 3
                FileGet(fNum, Number, Session("RECORD"))
                Session("VarVal") = Number
                FileClose(fNum)
            End If
            'JRS 2005/02/23
            If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
                Session("VarSelCheck") = Nothing
            End If
            If Session("FirstYear") = FirstYearHistory And Session("LastYear") = Session("LastYearForecast") Then
                If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                    Session("FirstYear") = FirstYearForecast
                Else
                    Session("LastYear") = LastYearHistory
                End If
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For i2 = Session("FirstYear") To Session("VarVal")
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                Cmb_Year.Items.Add(New ListItem(LTrim(Str(i2))))
            Next i2

            'Display first elements
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If Cmb_Year.Items.Count > 0 Then
                Cmb_Year.SelectedIndex = 0 ' Display First Item
            End If
            ' goes into Procedure summary
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If cmb_Country.Items.Count > 0 Then
                cmb_Country.SelectedIndex = 0 ' Display First Item
            End If

            'JRS 10/26/02 totals
            ReDim colTotal(NSec)
            ReDim rowTotal(NSec)
            ReDim acolLabels(NSec)
            ReDim arowLabels(NSec)
            bcolTot = False
            browTot = False
            'JRS 10/28/02 Dimensions big to be uble to redim preserve
            ReDim colTotalTime(200, 200)
            ReDim rowTotalTime(200, 200)
            'JRS 03/30/04
            Session("bSAM2") = bSAM2
            If Session("SAMGroups") Is Nothing Then
                Session("SAMGroups") = False
            End If
            If Session("SAMBasePct") Is Nothing Then
                Session("SAMBasePct") = False
            End If
            Session("aTypes") = aTypes
            Session("aSectors") = aSectors
            Session("itypeVar") = itypeVar
            Session("isecVar") = isecVar
            'jrs 2023/08/23 samConn = CreateConnection(Session("PutFdrvRun") & "IFs.mdb")
            samConn = CreateConnectionSQLite(Session("PutFdrvRun") & "IFs.db")
            Session("samConn") = samConn
            'jrs 2004/11/26
            LoadDs()
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", "SELECT * FROM SAMBaseValues", "", rsSAMBASEValues)
            Session("rsSAMBASEValues") = rsSAMBASEValues
            'jrs 2005/02/19
            With dbgSAM
                .Columns(0).HeaderText = "Supplied by"
                .Columns(1).HeaderText = "Used by Sectors"
                .Columns(2).HeaderText = "Used by Household"
                .Columns(3).HeaderText = "Used by Firms"
                .Columns(4).HeaderText = "Used by Capital"
                .Columns(5).HeaderText = "Used by Government"
                .Columns(6).HeaderText = "Used by ROW"
                .Columns(7).HeaderText = "Used by Total"
                .Columns(8).HeaderText = "Environment"
            End With
            'jrs 2011/11/11
            Session("MsgParent") = Nothing
        Else
            'jrs 2004/11/26
            DsSAMValues1 = Session("DsSAMValues1")
            DsSAMBasePct1 = Session("DsSAMBasePct1")
            rsSAMBASEValues = Session("rsSAMBASEValues")

            'JRS 03/30/04
            bSAM2 = Session("bSAM2")
            iType = Session("iType")
            If Session("SAMBasePct") Then
                dbgSAM.DataSource = DsSAMBasePct1
            Else
                dbgSAM.DataSource = DsSAMValues1
            End If
            aTypes = Session("aTypes")
            aSectors = Session("aSectors")
            itypeVar = Session("itypeVar")
            isecVar = Session("isecVar")
            samConn = Session("samConn")
            bWorkRemit = Session("bWorkRemit") 'jrs 2006/07/10
            If Not (Session("bDonate") Is Nothing) Then
                bDonate = Session("bDonate")
            End If
        End If
        'jrs 09/28/04
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1) & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
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
    Public mnuExit As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuGroups As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuBasePct As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuSAM2 As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuDisplayPrint As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public AboutSAM As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPopUpCell As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'CONVERSION_ISSUE: Class System.Windows.Forms.ContextMenu from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'Protected WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend mnuPopUpCellExpand As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Friend mnuPopUpCellOverTime As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Friend mnuExplain As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    'CONVERSION_NOTE: Class 'IFsGUI.dsSAMValues' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Friend WithEvents DsSAMValues1 As IFsGUI.dsSAMValues 
    'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    'jrs 2023/08/23
    'Protected WithEvents OleDbDASAMBasePct As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdSAMBasePct As SqliteDataReader
    'Protected WithEvents OleDbSelectCommand1 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand1 As SqliteCommand
    'Protected WithEvents OleDbConnection1 As System.Data.OleDb.OleDbConnection
    Protected WithEvents DbConnection1 As SqliteConnection
    Protected WithEvents DsSAMBasePct1 As dsSAMBasePct
    'Protected WithEvents OleDbDASAMLabels As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdSAMLabels As SqliteDataReader
    'Protected WithEvents OleDbSelectCommand2 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand2 As SqliteCommand
    Protected WithEvents DsSAMLabels1 As dsSAMLabels
    'Protected WithEvents OleDbDASAMLabels2 As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdSAMLabels1 As SqliteDataReader
    'Protected WithEvents OleDbSelectCommand3 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand3 As SqliteCommand
    Protected WithEvents DsSAMLabels21 As dsSAMLabels2
    'Protected WithEvents OleDbDASAMValues As System.Data.OleDb.OleDbDataAdapter
    Protected WithEvents rdSAMValues As SqliteDataReader
    'Protected WithEvents OleDbSelectCommand4 As System.Data.OleDb.OleDbCommand
    Protected WithEvents DbSelectCommand4 As SqliteCommand
    Protected WithEvents DsSAMValues1 As dsSAMValues
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        'jrs 2023/08/23
        'Me.OleDbDASAMBasePct = New System.Data.OleDb.OleDbDataAdapter
        'Me.OleDbSelectCommand1 = New System.Data.OleDb.OleDbCommand
        Me.DbSelectCommand1 = New SqliteCommand
        'Me.OleDbConnection1 = New System.Data.OleDb.OleDbConnection
        Me.DbConnection1 = New SqliteConnection
        Me.DsSAMBasePct1 = New dsSAMBasePct
        'Me.OleDbDASAMLabels = New System.Data.OleDb.OleDbDataAdapter
        'Me.OleDbSelectCommand2 = New System.Data.OleDb.OleDbCommand
        Me.DbSelectCommand2 = New SqliteCommand
        Me.DsSAMLabels1 = New dsSAMLabels
        'Me.OleDbDASAMLabels2 = New System.Data.OleDb.OleDbDataAdapter
        'Me.OleDbSelectCommand3 = New System.Data.OleDb.OleDbCommand
        Me.DbSelectCommand3 = New SqliteCommand
        Me.DsSAMLabels21 = New dsSAMLabels2
        'Me.OleDbDASAMValues = New System.Data.OleDb.OleDbDataAdapter
        'Me.OleDbSelectCommand4 = New System.Data.OleDb.OleDbCommand
        Me.DbSelectCommand4 = New SqliteCommand
        Me.DsSAMValues1 = New dsSAMValues
        CType(Me.DsSAMBasePct1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSAMLabels1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSAMLabels21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSAMValues1, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'OleDbDASAMBasePct
        '
        'Me.OleDbDASAMBasePct.SelectCommand = Me.OleDbSelectCommand1
        'Me.OleDbDASAMBasePct.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SAMBasePct", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Categories", "Categories"), New System.Data.Common.DataColumnMapping("Capital", "Capital"), New System.Data.Common.DataColumnMapping("Environment", "Environment"), New System.Data.Common.DataColumnMapping("Firms", "Firms"), New System.Data.Common.DataColumnMapping("Government", "Government"), New System.Data.Common.DataColumnMapping("Household", "Household"), New System.Data.Common.DataColumnMapping("ROW", "ROW"), New System.Data.Common.DataColumnMapping("Sectors", "Sectors"), New System.Data.Common.DataColumnMapping("Total", "Total")})})
        '
        'OleDbConnection1
        '
        Me.DbConnection1.ConnectionString = "DataSource=" & Session("PutFdrvRun") & "IFs.db;"
        Me.DbConnection1.Open()

        'Me.OleDbConnection1.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database L" &
        '"ocking Mode=0;Data Source=""C:\My Documents\IFs\RUNFILES\IFS.MDB"";Jet OLEDB:Engin" &
        '"e Type=4;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB" &
        '":SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None" &
        '";Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLE" &
        '"DB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=F" &
        '"alse;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1"

        '
        'OleDbSelectCommand1
        '
        DbSelectCommand1 = DbConnection1.CreateCommand()
        DbSelectCommand1.CommandText = "SELECT Categories, Capital, Environment, Firms, Government, Household, ROW, Secto" &
        "rs, Total FROM SAMBasePct"
        'Me.OleDbSelectCommand1.CommandText = "SELECT Categories, Capital, Environment, Firms, Government, Household, ROW, Secto" &
        '"rs, Total FROM SAMBasePct"
        'Me.OleDbSelectCommand1.Connection = Me.OleDbConnection1
        '
        'DsSAMBasePct1
        '
        Me.DsSAMBasePct1.DataSetName = "dsSAMBasePct"
        Me.DsSAMBasePct1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDASAMLabels
        '
        'Me.OleDbDASAMLabels.SelectCommand = Me.OleDbSelectCommand2
        'Me.OleDbDASAMLabels.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SAMLabels", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Categories", "Categories"), New System.Data.Common.DataColumnMapping("Capital", "Capital"), New System.Data.Common.DataColumnMapping("Environment", "Environment"), New System.Data.Common.DataColumnMapping("Firms", "Firms"), New System.Data.Common.DataColumnMapping("Government", "Government"), New System.Data.Common.DataColumnMapping("Household", "Household"), New System.Data.Common.DataColumnMapping("ROW", "ROW"), New System.Data.Common.DataColumnMapping("Sectors", "Sectors"), New System.Data.Common.DataColumnMapping("Total", "Total")})})
        '
        'OleDbSelectCommand2
        '
        DbSelectCommand2 = DbConnection1.CreateCommand()
        DbSelectCommand2.CommandText = "SELECT Categories, Capital, Environment, Firms, Government, Household, ROW, Secto" &
        "rs, Total FROM SAMLabels"

        'Me.OleDbSelectCommand2.CommandText = "SELECT Categories, Capital, Environment, Firms, Government, Household, ROW, Secto" &
        '"rs, Total FROM SAMLabels"
        'Me.OleDbSelectCommand2.Connection = Me.OleDbConnection1
        '
        'DsSAMLabels1
        '
        Me.DsSAMLabels1.DataSetName = "dsSAMLabels"
        Me.DsSAMLabels1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDASAMLabels2
        '
        'Me.OleDbDASAMLabels2.SelectCommand = Me.OleDbSelectCommand3
        'Me.OleDbDASAMLabels2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SAMLabels2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Categories", "Categories"), New System.Data.Common.DataColumnMapping("Capital", "Capital"), New System.Data.Common.DataColumnMapping("Environment", "Environment"), New System.Data.Common.DataColumnMapping("Firms", "Firms"), New System.Data.Common.DataColumnMapping("Government", "Government"), New System.Data.Common.DataColumnMapping("Household", "Household"), New System.Data.Common.DataColumnMapping("ROW", "ROW"), New System.Data.Common.DataColumnMapping("Sectors", "Sectors"), New System.Data.Common.DataColumnMapping("Total", "Total")})})
        '
        'OleDbSelectCommand3
        '
        DbSelectCommand3 = DbConnection1.CreateCommand()
        DbSelectCommand3.CommandText = "SELECT Categories, Capital, Environment, Firms, Government, Household, ROW, Secto" &
        "rs, Total FROM SAMLabels2"

        'Me.OleDbSelectCommand3.CommandText = "SELECT Categories, Capital, Environment, Firms, Government, Household, ROW, Secto" &
        '"rs, Total FROM SAMLabels2"
        'Me.OleDbSelectCommand3.Connection = Me.OleDbConnection1
        '
        'DsSAMLabels21
        '
        Me.DsSAMLabels21.DataSetName = "dsSAMLabels2"
        Me.DsSAMLabels21.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'OleDbDASAMValues
        '
        'Me.OleDbDASAMValues.SelectCommand = Me.OleDbSelectCommand4
        'Me.OleDbDASAMValues.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SAMValues", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Categories", "Categories"), New System.Data.Common.DataColumnMapping("Capital", "Capital"), New System.Data.Common.DataColumnMapping("Environment", "Environment"), New System.Data.Common.DataColumnMapping("Firms", "Firms"), New System.Data.Common.DataColumnMapping("Government", "Government"), New System.Data.Common.DataColumnMapping("Household", "Household"), New System.Data.Common.DataColumnMapping("ROW", "ROW"), New System.Data.Common.DataColumnMapping("Sectors", "Sectors"), New System.Data.Common.DataColumnMapping("Total", "Total")})})
        '
        'OleDbSelectCommand4
        '
        DbSelectCommand4 = DbConnection1.CreateCommand()
        DbSelectCommand4.CommandText = "SELECT Categories, Capital, Environment, Firms, Government, Household, ROW, Secto" &
        "rs, Total FROM SAMValues"

        'Me.OleDbSelectCommand4.CommandText = "SELECT Categories, Capital, Environment, Firms, Government, Household, ROW, Secto" &
        '"rs, Total FROM SAMValues"
        'Me.OleDbSelectCommand4.Connection = Me.OleDbConnection1
        '
        'DsSAMValues1
        '
        Me.DsSAMValues1.DataSetName = "dsSAMValues"
        Me.DsSAMValues1.Locale = New System.Globalization.CultureInfo("en-US")
        CType(Me.DsSAMBasePct1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSAMLabels1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSAMLabels21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSAMValues1, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_SAMPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_SAM
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_SAM()
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
    '------------------------------------------SAM.Frm-----------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last revision by BBH on January 2, 2003

    'JRS 08/21/02
    'Social Accounting Matrix (SAM)
    'implemented by Jose R. Solorzano
    'Instructions to add new columns and rows:
    '1. Go to the grid and add a new column
    '2. Go to the table SAMValues and SAMLabels and add a new row (IFs.mdb)
    '3. Go to LoadValues and add a line of code to fill every new cell you need
    'you can use the following subroutine to fill every cell:
    '
    'Private Sub LoadValuesVar(reg As Integer, yr As Integer, sVar As String,
    'srowName As String, scolName As String, factor As Single, iSizeDim2 As Integer)
    'Parameters:
    ' reg is the code of region
    ' yr is a year
    ' sVar is the name of the variable to retrieve values for the cell
    ' srowName label of the current row
    ' scolName label of the current column
    ' factor number to affect the value retrieved from the file (any real number)
    ' iSizeDim2 size of the second dimension, used to read n times the file, if no
    ' second dimension this parameter should be 1 to read at least once.
    '
    ' If you want to combine two variables in the same cell just call this function with
    ' the same rowname and colname but different variable name, they will add up.
    '
    ' Remember to add a loop to calculate the cells when using groups as we're doing now.
    '
    ' 4. To add the capability of expanding a cell go to function ExpandCell, there is an if statement
    ' for every cell, then add a new if statement for the cell you want to expand
    ' if it's the right cell you're double clicking you wolud want to call the following function:
    '
    ' Private Sub DisplayVAR(reg As Integer, yr As Integer, sVar As String, _
    'srowName As String, scolName As String, factor As Single, _
    'arowLabels As Variant, acolLabels As Variant, bAddRow As Boolean)
    ' Parameters:
    ' reg is the code of region
    ' yr is a year
    ' sVar is the name of the variable to retrieve values for the cell
    ' srowName label of the current row
    ' scolName label of the current column
    ' factor number to affect every value retrieved from the file (any real number)
    ' arowLabels is an array containing the labels for the rows
    ' acolLabels is an array containing the labels for the columns
    ' bAddRow means that it will create a new row in the displayed grid,
    ' instead of merging with the previous one, this is useful when grouping or using two
    ' different variables in the same grid display
    '
    ' You have to consider that we're using the size of acolLabels to read the second dimension
    ' from the file, this is in general, in some cases we're using arowLabels
    ' this depends on the following condition:
    ' bDimCol = (Not (srowName = "Sectors") And Not (srowName = "Household")) _
    'Or scolName = "Sectors"
    ' True uses columns for dimension 2 of the variable
    ' False uses rows for dimension 2 of the variable
    '
    ' 5. To add the capability to expand over time go to the function DisplayOverTime.
    '    Here there is an if statement per cell, therefore you'll have to add a new one
    '    for the new cell. You can copy one of the existing ones, there you'll see we're
    '    calling the function LoadVarAllYrs with 3 parameters:
    '    1. Variable Name
    '    2. Factor to multiply variable values
    '    3. Boolean to indicate we're starting from column 0 (true), or we're appending more
    '       columns (false).
    '
    ' 6. Totals: to expand or display over time we've implemented the following functions
    '       To Expand a total cell:
    '           a. UpdateDisplayColTotals
    '          b. UpdateDisplayRowTotals
    '       To Display over time a total cell:
    '          c. UpdateDisplayColTotalsTime (totalCols, period)
    '               totalCols: represents the total number of columns to store and accumulate
    '               period: represent how often in the Plot vector will find the same variables
    '          d. UpdateDisplayRowTotalsTime (totalCols, period, bContinuous)
    '               totalRows: represents the total number of rows to store and accumulate
    '               period: represent how often in the Plot vector will find the same variables
    '               bContinuous: is true when all values for the same row are together

    Dim CountryGroup(200) As String
    Dim GroupTot As Short
    'JRS 09/09/03

    Dim bCnvrtCurr As Boolean 'True if Currency Conversion is ON
    Dim CurrencyFct As Single 'Stores the Currency Factor for Conversion
    'Declares and opens a RecordSet with configuration values
    'jrs 2005/12/09 Dim rsCurr As DataTable
    'Labels for columns
    Dim aSectors() As String
    Dim aTypes() As String
    'Values for type variable
    Dim iType As Short
    Dim itypeVar As Short
    Dim isecVar As Short
    'Indices for aggregation calc.
    Dim POPidx As Short
    Dim GDPidx As Short
    Dim POPval As Single
    Dim POPBaseVal As Single
    Dim GDPval As Single
    Dim GDPBaseVal As Single
    'JRS 10/25/02 TOTALS
    Dim colTotal() As Single
    Dim rowTotal() As Single
    Dim bcolTot As Boolean
    Dim browTot As Boolean
    Dim acolLabels() As String
    Dim arowLabels() As String

    Dim colTotalTime(0, 0) As Single
    Dim rowTotalTime(0, 0) As Single
    Dim bDonate As Boolean
    'JRS 06/18/03
    Dim valDonate As Single
    Dim valDonateBase As Single
    'JRS 06/26/03
    Dim bWorkRemit As Boolean
    Dim valWorkRemit As Single
    Dim valWorkRemitBase As Single
    'JRS 2/4/03 SAM2
    Dim bSAM2 As Boolean
    'JRS 06/25/04
    'jrs 2023/08/23 Dim samConn As OleDb.OleDbConnection
    Dim samConn As SqliteConnection
    Dim VPTR As Long
    'jrs 2004/11/26
    Dim rsSAMBASEValues As DataTable
    'mti May 2006
    Dim ArrDesc(1, 1) As String
    'mti May 2006 - end
    'jrs 2006/07/25
    Dim bFirstTime As Boolean
    Dim bLastTime As Boolean

    'jrs 2021/04/05 using session variables instead
    'Dim MyFileforDisplay, MyFileforDisplayB As IStorage
    'Dim MyDF, MyDFB As IDataframe

    Public Sub AboutSAM_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.

        'jam 2021/09/01
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "AboutSAM.aspx")


        Vpath = Request.GetFriendlyUrlFileVirtualPath
        Vext = Request.GetFriendlyUrlFileExtension
        vfriendlyurl = FriendlyUrl.Resolve("~/AboutSAM.aspx")
        vurl = FriendlyUrl.Href("~/AboutSAM")
        Response.Redirect(vurl)



        'frm_AboutSAM.DefInstance.Show() 
    End Sub

    'JRS 12/01/02 FILES
    Private Sub cboFiles_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFiles.SelectedIndexChanged
        samConn.Open()
        If Cmb_Year.SelectedIndex = -1 Then Exit Sub
        ClearValues()
        'JRS 03/25/04 SYNCHRONIZING WITH LAST REGION/GROUP
        If Not IsNothing(Session("SAMCountryIdx")) Then
            cmb_Country.SelectedIndex = Session("SAMCountryIdx")
        End If
        Dim idxCtr As Short
        'JRS 2/4/03 SAM2
        If bSAM2 Then
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        Else
            LoadValues2(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        End If
        'JRS 2/4/03 SAM2
        If bSAM2 Then
            'Validates AID
            ValidateAID()
            ValidateWorkRemit()
        End If
        'Loads Totals
        LoadValuesTot()
        'Pct Calculation
        PctCalculation()
        'JRS 04/21/03 ROUND
        RoundValues()
        'Refresh Grid
        RefreshDisplay()
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        samConn.Close()
    End Sub


    '=============================SAM.Frm  ===========================
    Private Sub cmb_Country_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_Country.SelectedIndexChanged
        samConn.Open()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If Cmb_Year.SelectedIndex = -1 Then Exit Sub
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ClearValues()

        Dim idxCtr As Short
        'JRS 2/4/03 SAM2
        If bSAM2 Then
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        Else
            LoadValues2(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        End If
        'JRS 2/4/03 SAM2
        If bSAM2 Then
            'Validates AID
            ValidateAID()
            ValidateWorkRemit()
        End If
        'jrs 10/04/04 we need to commit, or we get bad calculations!
        'aparently the process is not done updating when it tries to calc. tots.
        'there's no commint with jet 4.0, thus close and reopen
        samConn.Close()
        samConn.Open()
        'Loads Totals
        LoadValuesTot()
        'Pct Calcultaion
        PctCalculation()
        'JRS 04/21/03 ROUND
        RoundValues()
        'Refresh Grid
        RefreshDisplay()
        'JRS 04/06/04
        Session("SAMCountryIdx") = cmb_Country.SelectedIndex
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        samConn.Close()
    End Sub


    '=============================SAM.Frm  ===========================
    'UPGRADE_WARNING: Event Cmb_Year.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub Cmb_Year_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Cmb_Year.SelectedIndexChanged
        samConn.Open()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If cmb_Country.SelectedIndex = -1 Then Exit Sub
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ClearValues()
        Dim idxCtr As Short
        'JRS 2/4/03 SAM2
        If bSAM2 Then
            LoadValues(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        Else
            LoadValues2(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
        End If
        'JRS 2/4/03 SAM2
        If bSAM2 Then
            'Validates AID
            ValidateAID()
            ValidateWorkRemit()
        End If
        'Loads Totals
        LoadValuesTot()
        'Percent Calculation
        PctCalculation()
        'JRS 04/21/03 ROUND
        RoundValues()
        'Refresh Grid
        RefreshDisplay()
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        samConn.Close()
    End Sub

    '=============================SAM.Frm  ===========================
    Private Sub ExpandCell()
        'JRS 09/11/02
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim bShow As Boolean
        Dim bTotTot As Boolean
        'Mouse Pointer to Hour Glass
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Reset Detail form
        'JRSVBNET frm_SAMDet.DefInstance.Close()
        'Load(frm_SAMDet)
        'Country index when grouping
        Dim idxCtr As Short
        Dim CompVarVal As Single
        'JRS 06/26/03
        Dim lFactor As Single
        'JRS 12/06/03
        Dim icurrRow As Integer
        Dim icurrCol As Integer


        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        With DsSAMValues1.SAMValues
            bTotTot = (Session("SAMRow") = "Total" And Session("SAMCol") = "Total")
            'Display AX for Sectors, Sectors
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors") And Not bTotTot Then
                DisplayAX(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(NSec)
                End If
                bShow = True
            End If

            'Cell Sectors, Household
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household") And Not bTotTot Then
                'JRS 2/17/03 DisplayVAR cmb_Country.ListIndex + 1, Val(Cmb_Year.Text), "CS", _
                '"Sectors", "Household", 0.5, aSectors, aTypes, True
                'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CSHHU", "Household", "Sectors", 1, New Object() {"Unskilled"}, aSectors, True)
                'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CSHHS", "Household", "Sectors", 1, New Object() {"Skilled"}, aSectors, True)
                'JRS 2/17/03
                TransposeTable()
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(2)
                End If
                bShow = True
            End If

            'Cell Sectors, Firms
            If Session("SAMRow") = "Sectors" And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") Then
                ExpandCellSectorFirms()
                bShow = True
            End If

            'Cell Sectors, Capital
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Capital") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "INVS", "Sectors", "Capital", 1, aSectors, New Object() {"Investment Origin"}, True)
                'DisplayVAR cmb_Country.ListIndex + 1, Val(Cmb_Year.Text), "CI", _
                '"Sectors", "Capital", 1, aSectors, _
                'New Object() {"Changes in Inventories"), True
                'Work with Groups
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True
            End If

            'Cell Sectors, Government
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GS", "Sectors", "Government", 1, aSectors, New Object() {"Gov Cons"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True
            End If

            'Cell Sectors, ROW
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XS", "Sectors", "ROW", 1, aSectors, New Object() {"Exports"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True
            End If

            'Cell Sectors, Environment
            If Session("SAMRow") = "Sectors" And Session("SAMCol") = "Environment" Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHINC", "Sectors", "Environment", 0, aSectors, New Object() {"CO2", "Water", "Forest"}, True)
                bShow = True
            End If

            'Cell Household, Sectors
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Sectors" And Not bTotTot Then
                'JRS 10/08/02
                'JRS 2/17/03 Call GetValue(cmb_Country.ListIndex + 1, Val(Cmb_Year.Text), "CDALF", CompVarVal)
                'JRS 2/17/03 DisplayVAR cmb_Country.ListIndex + 1, Val(Cmb_Year.Text), _
                '"VADD", "Household", "Sectors", (1 - CompVarVal) / 2, _
                'aTypes, aSectors, True

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "VADDHHU", "Household", "Sectors", 1, New Object() {"Unskilled"}, aSectors, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "VADDHHS", "Household", "Sectors", 1, New Object() {"Skilled"}, aSectors, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(NSec)
                End If
                bShow = True
            End If

            'Cell Household, Firms
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Firms" And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHDIVINT", "Household", "Firms", 1, aTypes, New Object() {"Div/Int"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True
            End If
            If Session("SAMRow") = "Total" And Session("SAMCol") = "Firms" Then
                colTotal(0) = colTotal(0) + Session("SectorsFirmsVal")
            End If

            'Cell Household, Government
            '        If (.Columns(0) = "Household" Or .Columns(0) = "Total") And .Columns(.col).datafield = "Government" And Not bTotTot Then
            '            'Work with Regions
            '            If mnuGroups.Caption = "Use &Groups" Then
            '                DisplayVAR cmb_Country.ListIndex + 1, Val(Cmb_Year.Text), "GOVHHTRN", _
            ''                    "Household", "Government", 0.25, aTypes, _
            ''                    New Object() {"Soc Sec", "Pension"), True
            '            'Work with Groups
            '            Else
            '                'Load countries in group
            '                LoadCountries (cmb_Country.Text)
            '                For idxCtr = 1 To GroupTot
            '                    DisplayVAR IFsRegs.Item(CountryGroup(idxCtr)), Val(Cmb_Year.Text), _
            ''                        "GOVHHTRN", "Household", "Government", 0.25, aTypes, _
            ''                        New Object() {"Soc Sec", "Pension"), (idxCtr = 1)
            '                Next idxCtr
            '                'JRS 09/17/02 AGGREGATION
            '                AggregateDisplayVar "GOVHHTRN", 1, UBound(aTypes) + 1, 2
            '            End If
            '            DisplayCurrencyConvertion
            '            If .Columns(0) = "Total" Then
            '                UpdateDisplayColTotals
            '            End If
            '            bShow = True
            '        End If
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Government" And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GOVHHTRNWEL", "Government", "Household", 1, New Object() {"Welfare"}, aTypes, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GOVHHTRNPEN", "Government", "Household", 1, New Object() {"Pension"}, aTypes, True)
                'JRS 1/10/03
                TransposeTable()
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True
            End If

            'Cell Household, ROW
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "ROW" And Not bTotTot Then
                If bWorkRemit Then
                    lFactor = 1
                Else
                    lFactor = 0
                End If
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWORKREMIT", "Household", "ROW", lFactor, New Object() {"Unskilled"}, New Object() {"Worker remittances"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWORKREMIT", "Household", "ROW", 0, New Object() {"Skilled"}, New Object() {"Worker remittances"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True
            End If

            'Cell Household, Total
            If Session("SAMRow") = "Household" And Session("SAMCol") = "Total" Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHINC", "Household", "Total", 1, aTypes, New Object() {"Income"}, True)
                'JRS 06/25/03
                grdSAMDet(1, 0) = "Totals"
                bShow = True
            End If

            'Cell Capital, Household
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHSAV", "Capital", "Household", 1, New Object() {"Capital"}, aTypes, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(2)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Capital, Firms
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "FIRMSAV", "Capital", "Firms", 1, New Object() {"Captial"}, New Object() {"Firms"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Capital, Government
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GOVBAL", "Capital", "Government", 1, New Object() {"Capital"}, New Object() {"Government"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Capital, ROW
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "FORSAV", "For Savings", "ROW", 1, New Object() {"Capital"}, New Object() {"Net Foreign Savings"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Firms, Sectors
            If (Session("SAMRow") = "Firms" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "VADDF", "Firms", "Sectors", 1, New Object() {"Firms"}, aSectors, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(NSec)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Firms, ROW
            If (Session("SAMRow") = "Firms" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XFDIFIN", "ROW", "Firms", 1, New Object() {"FDI Inflow"}, New Object() {"Firms"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XPORTFIN", "ROW", "Firms", 1, New Object() {"Equity Inflow"}, New Object() {"Firms"}, True)
                TransposeTable()
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell ROW, Sectors
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "MS", "ROW", "Sectors", 1, New Object() {"ROW"}, aSectors, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(NSec)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell ROW, Firms
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then
                If Not bWorkRemit Then
                    lFactor = -1
                Else
                    lFactor = 0
                End If
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XFDIFOUT", "Firms", "ROW", 1, New Object() {"FDI Outflow"}, New Object() {"ROW"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XPORTFOUT", "Firms", "ROW", 1, New Object() {"Equity Outflow"}, New Object() {"ROW"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWBSUBF", "Firms", "ROW", 1, New Object() {"WB Subscriptions"}, New Object() {"ROW"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWORKREMIT", "Firms", "ROW", lFactor, New Object() {"Worker Remittances"}, New Object() {"ROW"}, True)
                TransposeTable()
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell ROW, Government
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                If bDonate Then
                    DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "AID", "Government", "ROW", -1, New Object() {"Aid Donations"}, New Object() {"ROW"}, True)
                End If
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWBLNFOUT", "Government", "ROW", 1, New Object() {"World Bank Repay"}, New Object() {"ROW"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XIMFCRFOUT", "Government", "ROW", 1, New Object() {"IMF Repay"}, New Object() {"ROW"}, True)
                TransposeTable()
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Environment, Sectors
            If Session("SAMRow") = "Environment" And Session("SAMCol") = "Sectors" Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHINC", "Environment", "Sectors", 0, New Object() {"CO2", "Water", "Forest"}, aSectors, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(6)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If


            'Cell Government, Household
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHTAX", "Government", "Household", 1, New Object() {"Income Tx"}, aTypes, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHGOVSS", "Government", "Household", 1.0#, New Object() {"Soc Sec Tx"}, aTypes, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(2)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If



            'Cell Government, Firms
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "FIRMTAX", "Government", "Firms", 1, New Object() {"Corp/Bus Tax"}, New Object() {"Firms"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "FIRMGOVSS", "Government", "Firms", 1, New Object() {"Soc Sec Tax"}, New Object() {"Firms"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "INDIRECTTAX", "Government", "Firms", 1, New Object() {"Indirect Tax"}, New Object() {"Firms"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Government, ROW
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                If Not bDonate Then
                    DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "AID", "ROW", "Government", 1, New Object() {"Aid Receipts"}, New Object() {"Government"}, True)
                End If
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWBLNFIN", "ROW", "Government", 1, New Object() {"World Bank Loans"}, New Object() {"Government"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XIMFCRFIN", "ROW", "Government", 1, New Object() {"IMF Credits"}, New Object() {"Government"}, True)
                TransposeTable()
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

        End With

        If bShow Then
            If Session("SAMRow") = "Total" And Session("SAMCol") <> "Total" Then
                DisplayColTotals()
            End If
            If Session("SAMCol") = "Total" And Session("SAMRow") <> "Household" And Session("SAMRow") <> "Total" Then
                DisplayRowTotals()
            End If
            'JRS 04/29/03
            DisplayFixedDigits()
            dbgSAM.Visible = False
            'Frame1.Visible = False
            lbl_countries.Visible = False
            cmb_Country.Visible = False
            Lbl_File.Visible = False
            cboFiles.Visible = False
            optLabels.Visible = False
            optValues.Visible = False
            Lblyear.Visible = False
            Cmb_Year.Visible = False

            grdSAMDet.Visible = True
            cmdContinueSam.Visible = True
            'frm_SAMDet.DefInstance.Show() 
        Else
            grdSAMDet.Visible = False
            cmdContinueSam.Visible = False
            'JRS 04/18/03

            LoadMessage("No detail available for this cell.", Session("Message"))
            'jam 2021/09/01
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")


            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
            vurl = FriendlyUrl.Href("~/frm_Message")
            Response.Redirect(vurl)


        End If
        'Mouse Pointer back to normal
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub ExpandCellSectorFirms()
        'JRS 09/11/02
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Mouse Pointer to Hour Glass
        Dim CompVarVal As Single
        'JRS 06/26/03
        Dim lFactor As Single

        'Display AX for Sectors, Sectors
        With dbgSAM

            DisplayAX(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue))
            UpdateDisplayColTotals(NSec, True)
            UpdateDisplayRowTotals(NSec)

            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CSHHU",
            "Household", "Sectors", 1, New Object() {"Unskilled"}, aSectors, True)
            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CSHHS",
            "Household", "Sectors", 1, New Object() {"Skilled"}, aSectors, True)
            TransposeTable()
            UpdateDisplayRowTotals(NSec)

            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "INVS",
                "Sectors", "Capital", 1, aSectors, New Object() {"Investment Origin"}, True)
            UpdateDisplayRowTotals(NSec)

            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GS",
                "Sectors", "Government", 1, aSectors, New Object() {"Gov Cons"}, True)
            UpdateDisplayRowTotals(NSec)

            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XS",
                "Sectors", "ROW", 1, aSectors, New Object() {"Exports"}, True)
            UpdateDisplayRowTotals(NSec)

            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue),
                "VADDHHU", "Household", "Sectors", 1,
                    New Object() {"Unskilled"}, aSectors, True)
            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue),
                "VADDHHS", "Household", "Sectors", 1,
                    New Object() {"Skilled"}, aSectors, True)
            UpdateDisplayColTotals(NSec)


            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "VADDF",
                "Firms", "Sectors", 1, New Object() {"Firms"}, aSectors, True)
            UpdateDisplayColTotals(NSec)

            DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "MS", "ROW",
            "Sectors", 1, New Object() {"ROW"}, aSectors, True)
            UpdateDisplayColTotals(NSec)

            'jrs 2006/08/28 Dim bRowTot As Boolean
            browTot = (Session("SAMCol") = "Total")

        End With

        Dim i As Integer
        For i = 0 To NSec - 1
            If browTot Then
                rowTotal(i) = colTotal(i)
            Else
                rowTotal(i) = colTotal(i) - rowTotal(i)
            End If
            colTotal(i) = 0
        Next i
        If browTot Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, TOTAL: " + cmb_Country.SelectedValue + ", " +
            Cmb_Year.SelectedValue
        Else
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, Firms: " + cmb_Country.SelectedValue + ", " +
            Cmb_Year.SelectedValue
        End If
        If Not browTot Then
            DisplayRowTotals()
            grdSAMDet(1, 0) = "Firms"
        End If
    End Sub

    '=============================SAM.Frm  ===========================
    'JRS 2/4/03 SAM2
    Private Sub ExpandCell2()
        'JRS 09/11/02
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim bShow As Boolean
        Dim bTotTot As Boolean
        'Mouse Pointer to Hour Glass
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Reset Detail form
        'JRSVBNET frm_SAMDet.DefInstance.Close()
        'Load(frm_SAMDet)
        'Country index when grouping
        Dim idxCtr As Short
        Dim CompVarVal As Single
        Dim currIdxCtr As Short
        'JRS 12/06/03
        Dim icurrRow As Integer
        Dim icurrCol As Integer

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        With DsSAMValues1.SAMValues
            bTotTot = (Session("SAMRow") = "Total" And Session("SAMCol") = "Total")

            'Cell Firms, ROW
            If (Session("SAMRow") = "Firms" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XFDISTOCK", "Firms", "ROW", 1, New Object() {"FDI Inflow"}, New Object() {"ROW"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XPORTFOLIO", "Firms", "ROW", 1, New Object() {"Equity Inflow"}, New Object() {"ROW"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XFIRMDEBT", "Firms", "ROW", 1, New Object() {"Firm Intl Debt."}, New Object() {"ROW"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Government, Household
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GOVTDEBTD", "Government", "Household", 1, New Object() {"Govt Dom Debt"}, New Object() {"Household"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell Government, ROW
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XIMFCREDIT", "Government", "ROW", 1, New Object() {"IMF Credits"}, New Object() {"ROW"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWBLOANS", "Government", "ROW", 1, New Object() {"World Bank Loans"}, New Object() {"ROW"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XGOVTDEBTB", "Government", "ROW", 1, New Object() {"Govt Intl Debt."}, New Object() {"ROW"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell ROW, Firms
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XFDISTOUT", "Firms", "ROW", 1, New Object() {"FDI Outflow"}, New Object() {"Firms"}, True)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XPORTSTOUT", "Firms", "ROW", 1, New Object() {"Equity Outflow"}, New Object() {"Firms"}, True)
                TransposeTable()
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

            'Cell ROW, Government
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'DisplayVAR cmb_Country.ListIndex + 1, Val(Cmb_Year.Text), "XDEBTRPA", _
                '"ROW", "Government", 1, New Object() {"Row"), New Object() {"Government"), True
                'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XRESERVES", "ROW", "Government", 1, New Object() {"Intl Reserves"}, New Object() {"Government"}, True)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True
            End If

        End With
        If bShow Then
            If Session("SAMRow") = "Total" And Session("SAMCol") <> "Total" Then
                DisplayColTotals()
            End If
            If Session("SAMCol") = "Total" And Session("SAMRow") <> "Household" And Session("SAMRow") <> "Total" Then
                DisplayRowTotals()
            End If
            'JRS 04/29/03
            DisplayFixedDigits()
            dbgSAM.Visible = False
            'Frame1.Visible = False
            lbl_countries.Visible = False
            cmb_Country.Visible = False
            Lbl_File.Visible = False
            cboFiles.Visible = False
            optLabels.Visible = False
            optValues.Visible = False
            Lblyear.Visible = False
            Cmb_Year.Visible = False

            grdSAMDet.Visible = True
            cmdContinueSam.Visible = True
            'frm_SAMDet.DefInstance.Show() 
        Else
            grdSAMDet.Visible = False
            cmdContinueSam.Visible = False
            'JRS 04/21/03

            LoadMessage("No detail available for this cell.", Session("Message"))

            'jam 2021/09/01
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")


            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")
            vurl = FriendlyUrl.Href("~/frm_Message")
            Response.Redirect(vurl)


        End If
        'Mouse Pointer back to normal
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    '=============================SAM.Frm  ===========================
    'CONVERSION_NOTE: The code of Event 'frm_SAM.frm_SAM_Load' was moved to Method 'frm_SAM.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_SAM_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim qtyPAram As Integer


        cswmWebMenu.UserData = "jsolorza::976739975"
        'jam 2021/09/01
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"


        'jam 2021/04/19
        cswmWebMenu.ClearPixelImage = Page.ResolveUrl("~/Images/blank.gif ")
        cswmWebMenu.PopupIcon = Page.ResolveUrl("~/Images/Popup.gif")
        cswmWebMenu.ScrollDownActiveImage = Page.ResolveUrl("~/Images/ScrollDownActive.gif")
        cswmWebMenu.ScrollDownEnabledImage = Page.ResolveUrl("~/Images/ScrollDownEnabled.gif")
        cswmWebMenu.ScrollUpActiveImage = Page.ResolveUrl("~/Images/ScrollUpActive.gif")
        cswmWebMenu.ScrollUpEnabledImage = Page.ResolveUrl("~/Images/ScrollUpEnabled.gif")
        cswmWebMenu.SelectedPopupIcon = Page.ResolveUrl("~/Images/SelectedPopup.gif")




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
        cswmGroup1.ID = "Group_ce9f"

        'jrs 2011/05/04
        If Session("DisplayParent") = Nothing Then
            Session("DisplayParent") = "frm_MainMenu.aspx"

        End If

        'jrs 09/29/04
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        'jam 2021/09/05
        cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl("~/" + Session("DisplayParent")) & "';"
        'jam 2021/09/03
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("DisplayParent") & "';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Switch"
        cswmGroup1.ID = "mnuSwitch"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuGroups"
        If Session("SAMGroups") Then
            cswmItem.Caption = "Use Country"
        Else
            cswmItem.Caption = "Use Groups"
        End If
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuBasePct"
        If Session("SAMBasePct") Then
            cswmItem.Caption = "Normal"
        Else
            cswmItem.Caption = "Percent Change From Base Case"
        End If
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuSAM2"
        If bSAM2 Then
            cswmItem.Caption = "Show Stocks"
        Else
            cswmItem.Caption = "Show Flows"
        End If
        'jrs 2006/07/10
        cswmItem = cswmGroup1.Items.Add()
        'dsb 2026/07/15 ***Start*** Collapse SAM in switch
        cswmItem.ID = "mnuExpandSAM"
        'cswmItem.Caption = "Expand SAM"
        If Session("SAMExpanded") IsNot Nothing AndAlso CBool(Session("SAMExpanded")) Then
            cswmItem.Caption = "Collapse SAM"
        Else
            cswmItem.Caption = "Expand SAM"
        End If 'dsb 2026/07/15 ***End***

        'jrs 09/28/04

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "About SAM"
        'cswmGroup1.ID = "Group_d1a8"

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16009);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jrs 2008/05/07
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "About"
        'jam 2021/09/01
        'cswmGroup1.OnMenuBarClick = "location.href='AboutSAM.aspx';"
        cswmGroup1.OnMenuBarClick = "location.href='" + Page.ResolveUrl("~/AboutSAM.aspx") + "';"




        'jam 2021/09/01
        othCountry = ""
        If (Not IsNothing(Request.QueryString.Get("Country")) Or Not IsNothing(Request.QueryString.Get("Group"))) And Not Page.IsPostBack Then

            If Not IsNothing(Request.QueryString("Country")) Then

                othCountry = Request.QueryString("Country")
            Else
                If Not IsNothing(Request.QueryString("Group")) Then
                    othCountry = Request.QueryString("Group")

                Else
                    othCountry = ""


                End If
            End If
        End If



        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_CountryProfile.aspx")


        If othCountry <> "" Then
            vurl = FriendlyUrl.Href("~/frm_SAM", SAMPrepareForFriendlyURL(othCountry))

            Response.Redirect(vurl)

        Else

            vurl = FriendlyUrl.Href("~/frm_SAM")

        End If


        'jam 2021/04/19
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String

        'jam 2021/05/01
        If segments.Count = 0 Then

            vCountry = Session("MMCountry")

        Else

            If segments.Count = 1 Then

                vCountry = segments(0)


            Else
                vCountry = segments(0)
                Session("vCountry") = vCountry

            End If

        End If

        Session("vCountry") = vCountry






        If Not Page.IsPostBack Then
            cboFiles_SelectedIndexChanged(Me, New System.EventArgs)
        Else
            RefreshDisplay()
        End If

        'jam 2021/09/01
        'jrs 2011/05/06
        'If Not IsNothing(Request.QueryString("Country")) Then
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Session("vCountry"))) And Not Session("SAMGroups") Then
            'jam 2021/09/01
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> Session("vCountry") Or Not Page.IsPostBack Then
                'jam 2021/09/01
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = Session("vCountry")
                'jrs 2012/08/15 Session("SAMGroups") = False
                If Session("SAMGroups") = True Then
                    cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"
                    lbl_countries.Text = "Countries or Regions"
                    LoadRegions()
                    Session("SAMGroups") = False
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
            'jam 2021/09/01
            'ElseIf Not IsNothing(Request.QueryString("Group")) Then 'jrs 2011/05/05
            'jam 2021/09/07
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Session("vCountry"))) And Session("SAMGroups") Then 'jrs 2011/05/05

            'jam 2021/09/01
            'If cmb_Country.SelectedValue <> Request.QueryString("Group") Then
            If cmb_Country.SelectedValue <> Session("vCountry") Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/09/01
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(Session("vCountry"), False, Me)
                If gIdx > 0 Then
                    cmb_Country.SelectedIndex = gIdx - 1
                    Session("SAMCountryIdx") = cmb_Country.SelectedIndex
                    'jam 2021/09/01
                    'Session("MMGroup") = Request.QueryString("Group")
                    Session("MMGroup") = Session("vCountry")
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("SAMGroups") = True Then
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
            Session("SAMCountryIdx") = cmb_Country.SelectedIndex
            cmb_Country_SelectedIndexChanged(Me, New System.EventArgs)
        End If
    End Sub



    '=============================SAM.Frm  ===========================
    Private Sub LoadRegions()
        Dim EleLoop As Object
        Dim DimNum As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Load Region Names Combo (Dropdown List) Box
        '   -----------------------------------------
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_Country.Items.Clear()
        'UPGRADE_WARNING: Couldn't resolve default property of object DimNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        DimNum = 1

        'UPGRADE_WARNING: Couldn't resolve default property of object DimNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For EleLoop = 1 To MaxVal(DimNum) - 1 'Exclude 'ALL'
            'UPGRADE_WARNING: Couldn't resolve default property of object EleLoop. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object DimNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_Country.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
        Next EleLoop
    End Sub

    Public Sub mnuBasePct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRS 12/05/02
        If cswmWebMenu.Groups("mnuSwitch").Items("mnuBasePct").Caption = "Normal" Then
            'UPGRADE_ISSUE: Data property DataSAM.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'JRS 12/06/03 DataSAM.RecordSource = "SAMValues"
            'JRS 12/06/03 DataSAM.Refresh()
            dbgSAM.DataSource = DsSAMValues1
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            'JRS 2004/11/26 DsSAMValues1.Clear()
            'JRS 2004/11/26 OleDbDASAMValues.Fill(DsSAMValues1)
            cswmWebMenu.Groups("mnuSwitch").Items("mnuBasePct").Caption = "Percent Change From Base Case"
            Session("SAMBasePct") = False
        Else
            'UPGRADE_ISSUE: Data property DataSAM.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'JRS 12/06/03 DataSAM.RecordSource = "SAMBasePct"
            'JRS 12/06/03 DataSAM.Refresh()
            dbgSAM.DataSource = DsSAMBasePct1
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            'JRS 2004/11/26 DsSAMBasePct1.Clear()
            'JRS 2004/11/26 OleDbDASAMBasePct.Fill(DsSAMBasePct1)
            cswmWebMenu.Groups("mnuSwitch").Items("mnuBasePct").Caption = "Normal"
            Session("SAMBasePct") = True
        End If
        dbgSAM.DataBind()
    End Sub

    Public Sub mnuDisplayPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim RptSAM As Object
        'CONVERSION_ISSUE: Class System.Windows.Forms.Application from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Application.DoEvents()
        'RELOADING CONNECTION DATA BASE FOR DATA SOURCE
        Dim sConn As String
        sConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
        sConn = sConn & Session("PutFdrvRun2") & "\IFs.mdb;Persist Security Info=False"
        '    If deIFs.conIFS.State = 0 Then
        '        deIFs.conIFS.ConnectionString = sConn
        '    End If

        '    If deIFs.rsSAM.State <> 0 Then
        '        deIFs.rsSAM.Requery
        '        DoEvents
        '    End If
        'UPGRADE_ISSUE: Unload deSAM was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2029"'
        'JRS 12/06/03 Unload(deSAM)
        'UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1039"'
        'JRS 12/06/03 Load(deSAM)
        'jrs 2006/10/27 vs2k5 deSAM.conIFS.ConnectionString = sConn

        'UPGRADE_WARNING: Couldn't resolve default property of object RptSAM.Title. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        RptSAM.Title = cmb_Country.SelectedValue & ", " & Cmb_Year.SelectedValue
        'UPGRADE_WARNING: Couldn't resolve default property of object RptSAM.DataSource. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'jrs 2006/10/27 vs2k5 RptSAM.DataSource = deSAM
        'JRS 08/09/02
        'UPGRADE_WARNING: Couldn't resolve default property of object RptSAM.Refresh. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        RptSAM.Refresh()
        If Session("bGuidedUse") Then
            'UPGRADE_WARNING: Couldn't resolve default property of object RptSAM.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RptSAM.Show(VB6.FormShowConstants.Modal)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object RptSAM.Show. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            RptSAM.Show()
        End If
    End Sub

    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'jam 2021/09/01
        'Support.FormManager.Close(Me)


        'jam 2021/07/01
        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_MainMenu.aspx.aspx")


        vurl = FriendlyUrl.Href("~/frm_MainMenu.aspx.aspx")

        Response.Redirect(vurl)



    End Sub

    Public Sub mnuExplain_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuExplain_Click(eventSender, eventArgs)
    End Sub

    Public Sub mnuGroups_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)


        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups" Then
            cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Country"
            lbl_countries.Text = "Groups"
            LoadGroups()
            Session("SAMGroups") = True
        Else
            cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"
            lbl_countries.Text = "Countries or Regions"
            LoadRegions()
            Session("SAMGroups") = False
        End If
        cmb_Country.SelectedIndex = 0 ' Display First Item
        'REFRESH GRID
        Cmb_Year_SelectedIndexChanged(Cmb_Year, New System.EventArgs)
        'jrs 2011/05/06
        If Session("SAMGroups") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                'Support.FormManager.Show(Me, "frm_SAM.aspx?Group=" & cmb_Country.SelectedValue)

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SAM.aspx")


                vurl = FriendlyUrl.Href("~/frm_SAM", cmb_Country.SelectedValue)

                Response.Redirect(vurl)



            Else
                'jam 2021/09/01
                'Support.FormManager.Show(Me, "frm_SAM.aspx?Group=" & Session("MMGroup"))

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SAM.aspx")


                vurl = FriendlyUrl.Href("~/frm_SAM", Session("MMGroup"))

                Response.Redirect(vurl)



            End If
        Else
            'jam 2021/09/01
            'Support.FormManager.Show(Me, "frm_SAM.aspx?Country=AF")

            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_SAM.aspx")


            vurl = FriendlyUrl.Href("~/frm_SAM", "AF")

            Response.Redirect(vurl)

        End If

    End Sub

    '=============================SAM.Frm  ===========================
    Private Sub LoadGroups()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_Country.Items.Clear()
        Dim valtbl As DataTable
        'jrs 2023/08/24 added upper
        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " ORDER BY upper(GroupName)", "", valtbl)
        Dim i As Integer
        i = 0
        While Not i = valtbl.Rows.Count
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_Country.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
            i = i + 1
        End While

    End Sub

    '=============================SAM.Frm  ===========================
    'Subroutine to Load the Values
    'into SAM Values table
    Private Sub LoadValues(ByRef reg As Short, ByRef yr As Short)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Mouse pointer to Hour Glass
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Load Cell Sectors, Sectors
        LoadValuesAX(reg, yr)

        'jrs 2006/07/25
        bFirstTime = True
        bLastTime = False
        'Load Cell Sectors, Household
        'JRS 2/17/03 LoadValuesVar reg, yr, "CS", "Sectors", "Household", 1, NSec
        LoadValuesVar(reg, yr, "CSHHU", "Sectors", "Household", 1, NSec, Session("dfCSHHU" + cboFiles.SelectedIndex.ToString), Session("dfCSHHUB"))
        bFirstTime = False
        LoadValuesVar(reg, yr, "CSHHS", "Sectors", "Household", 1, NSec, Session("dfCSHHS" + cboFiles.SelectedIndex.ToString), Session("dfCSHHSB"))

        'Load Cell Sectors, Capital
        LoadValuesVar(reg, yr, "INVS", "Sectors", "Capital", 1, NSec, Session("dfINVS" + cboFiles.SelectedIndex.ToString), Session("dfINVSB"))
        'LoadValuesVar reg, yr, "CI", "Sectors", "Capital", 1, NSEC

        'Load Cell Sectors, Government
        LoadValuesVar(reg, yr, "GS", "Sectors", "Government", 1, NSec, Session("dfGS" + cboFiles.SelectedIndex.ToString), Session("dfGSB"))

        'Load Cell Sectors, ROW
        LoadValuesVar(reg, yr, "XS", "Sectors", "ROW", 1, NSec, Session("dfXS" + cboFiles.SelectedIndex.ToString), Session("dfXSB"))

        'Get value of CDALF and put into CompVarVal
        Dim CompVarVal As Single
        Call GetValue(reg, yr, "CDALF", CompVarVal)

        'Load Cell Household, Sectors - Firms, Sectors
        'JRS 2/17/03 LoadValuesVar reg, yr, "VADD", "Household", "Sectors", 1 - CompVarVal, NSec
        LoadValuesVar(reg, yr, "VADDHHU", "Household", "Sectors", 1, NSec, Session("dfVADDHHU" + cboFiles.SelectedIndex.ToString), Session("dfVADDHHUB"))
        LoadValuesVar(reg, yr, "VADDHHS", "Household", "Sectors", 1, NSec, Session("dfVADDHHS" + cboFiles.SelectedIndex.ToString), Session("dfVADDHHSB"))

        'JRS 06/26/03 LoadValuesVar reg, yr, "VADD", "Firms", "Sectors", CompVarVal, NSec
        LoadValuesVar(reg, yr, "VADDF", "Firms", "Sectors", 1, NSec, Session("dfVADDF" + cboFiles.SelectedIndex.ToString), Session("dfVADDFB"))

        'Load Cell Household, Firms
        LoadValuesVar(reg, yr, "HHDIVINT", "Household", "Firms", 1, iType, Session("dfHHDIVINT" + cboFiles.SelectedIndex.ToString), Session("dfHHDIVINTB"))

        'Load Cell Household, Government
        '    LoadValuesVar reg, yr, "GOVHHTRN", "Household", "Government", 1, 1
        LoadValuesVar(reg, yr, "GOVHHTRNPEN", "Household", "Government", 1, iType, Session("dfGOVHHTRNPEN" + cboFiles.SelectedIndex.ToString), Session("dfGOVHHTRNPENB"))
        LoadValuesVar(reg, yr, "GOVHHTRNWEL", "Household", "Government", 1, iType, Session("dfGOVHHTRNWEL" + cboFiles.SelectedIndex.ToString), Session("dfGOVHHTRNWELB"))

        'Load Cell Household, ROW
        LoadValuesVar(reg, yr, "XWORKREMIT", "Household", "ROW", 1, 1, Session("dfXWORKREMIT" + cboFiles.SelectedIndex.ToString), Session("dfXWORKREMITB"))

        'Load Cell Household, Total
        LoadValuesVar(reg, yr, "HHINC", "Household", "Total", 1, iType, Session("dfHHINC" + cboFiles.SelectedIndex.ToString), Session("dfHHINCB"))

        'Load Cell Capital, Household
        LoadValuesVar(reg, yr, "HHSAV", "Capital", "Household", 1, iType, Session("dfHHSAV" + cboFiles.SelectedIndex.ToString), Session("dfHHSAVB"))

        'Load Cell Capital, Firms
        LoadValuesVar(reg, yr, "FIRMSAV", "Capital", "Firms", 1, 1, Session("dfFIRMSAV" + cboFiles.SelectedIndex.ToString), Session("dfFIRMSAVB"))

        'Load Cell Capital, Government
        LoadValuesVar(reg, yr, "GOVBAL", "Capital", "Government", 1, 1, Session("dfGOVBAL" + cboFiles.SelectedIndex.ToString), Session("dfGOVBALB"))

        'Load Cell Capital, ROW
        LoadValuesVar(reg, yr, "FORSAV", "Capital", "ROW", 1, 1, Session("dfFORSAV" + cboFiles.SelectedIndex.ToString), Session("dfFORSAVB"))

        'Load Cell Government, Household
        LoadValuesVar(reg, yr, "HHTAX", "Government", "Household", 1, iType, Session("dfHHTAX" + cboFiles.SelectedIndex.ToString), Session("dfHHTAXB"))
        LoadValuesVar(reg, yr, "HHGOVSS", "Government", "Household", 1, iType, Session("dfHHGOVSS" + cboFiles.SelectedIndex.ToString), Session("dfHHGOVSSB"))

        'Load Cell Government, Firms
        LoadValuesVar(reg, yr, "FIRMTAX", "Government", "Firms", 1, 1, Session("dfFIRMTAX" + cboFiles.SelectedIndex.ToString), Session("dfFIRMTAXB"))
        LoadValuesVar(reg, yr, "FIRMGOVSS", "Government", "Firms", 1, 1, Session("dfFIRMGOVSS" + cboFiles.SelectedIndex.ToString), Session("dfFIRMGOVSSB"))
        LoadValuesVar(reg, yr, "INDIRECTTAX", "Government", "Firms", 1, 1, Session("dfINDIRECTTAX" + cboFiles.SelectedIndex.ToString), Session("dfINDIRECTTAXB"))

        'Load Cell Firms, ROW
        LoadValuesVar(reg, yr, "XFDIFIN", "Firms", "ROW", 1, 1, Session("dfXFDIFIN" + cboFiles.SelectedIndex.ToString), Session("dfXFDIFINB"))
        LoadValuesVar(reg, yr, "XPORTFIN", "Firms", "ROW", 1, 1, Session("dfXPORTFIN" + cboFiles.SelectedIndex.ToString), Session("dfXPORTFINB"))

        'Load Cell ROW, Sectors
        LoadValuesVar(reg, yr, "MS", "ROW", "Sectors", 1, NSec, Session("dfMS" + cboFiles.SelectedIndex.ToString), Session("dfMSB"))

        'Load Cell ROW, Firms
        LoadValuesVar(reg, yr, "XFDIFOUT", "ROW", "Firms", 1, 1, Session("dfXFDIFOUT" + cboFiles.SelectedIndex.ToString), Session("dfXFDIFOUTB"))
        LoadValuesVar(reg, yr, "XPORTFOUT", "ROW", "Firms", 1, 1, Session("dfXPORTFOUT" + cboFiles.SelectedIndex.ToString), Session("dfXPORTFOUTB"))
        LoadValuesVar(reg, yr, "XWBSUBF", "ROW", "Firms", 1, 1, Session("dfXWBSUBF" + cboFiles.SelectedIndex.ToString), Session("dfXWBSUBFB"))
        LoadValuesVar(reg, yr, "XWORKREMIT", "ROW", "Firms", 1, 1, Session("dfXWORKREMIT" + cboFiles.SelectedIndex.ToString), Session("dfXWORKREMITB"))

        'Load Cell ROW, Government
        LoadValuesVar(reg, yr, "AID", "ROW", "Government", 1, 1, Session("dfAID" + cboFiles.SelectedIndex.ToString), Session("dfAIDB"))
        LoadValuesVar(reg, yr, "XWBLNFOUT", "ROW", "Government", 1, 1, Session("dfXWBLNFOUT" + cboFiles.SelectedIndex.ToString), Session("dfXWBLNFOUTB"))
        LoadValuesVar(reg, yr, "XIMFCRFOUT", "ROW", "Government", 1, 1, Session("dfXIMFCRFOUT" + cboFiles.SelectedIndex.ToString), Session("dfXIMFCRFOUTB"))

        'Load Cell Total, Government
        '    LoadValuesVar reg, yr, "GOVEXP", "Total", "Government", 1, 1
        '    LoadValuesVar reg, yr, "GOVBAL", "Total", "Government", 1, 1
        '    LoadValuesVar reg, yr, "AID", "Total", "Government", 1, 1

        'Load Cell Government, ROW
        LoadValuesVar(reg, yr, "AID", "Government", "ROW", 1, 1, Session("dfAID" + cboFiles.SelectedIndex.ToString), Session("dfAIDB"))
        LoadValuesVar(reg, yr, "XWBLNFIN", "Government", "ROW", 1, 1, Session("dfXWBLNFIN" + cboFiles.SelectedIndex.ToString), Session("dfXWBLNFINB"))
        bLastTime = True
        LoadValuesVar(reg, yr, "XIMFCRFIN", "Government", "ROW", 1, 1, Session("dfXIMFCRFIN" + cboFiles.SelectedIndex.ToString), Session("dfXIMFCRFINB"))

        'Load Cell Government, Total
        '    LoadValuesVar reg, yr, "GOVREV", "Government", "Total", 1, 1
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    'JRS 2/4/03 SAM2
    'Subroutine to Load the Values
    'into SAM Values 2 table
    Private Sub LoadValues2(ByRef reg As Short, ByRef yr As Short)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Mouse pointer to Hour Glass
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'jrs 2006/07/25
        bFirstTime = True
        bLastTime = False
        'Load Cell Firms, ROW
        LoadValuesVar(reg, yr, "XFDISTOCK", "Firms", "ROW", 1, 1, Session("dfXFDISTOCK" + cboFiles.SelectedIndex.ToString), Session("dfXFDISTOCKB"))
        bFirstTime = False
        LoadValuesVar(reg, yr, "XPORTFOLIO", "Firms", "ROW", 1, 1, Session("dfXPORTFOLIO" + cboFiles.SelectedIndex.ToString), Session("dfXPORTFOLIOB"))
        LoadValuesVar(reg, yr, "XFIRMDEBT", "Firms", "ROW", 1, 1, Session("dfXFIRMDEBT" + cboFiles.SelectedIndex.ToString), Session("dfXFIRMDEBTB"))
        'Load Cell Government, Household
        LoadValuesVar(reg, yr, "GOVTDEBTD", "Government", "Household", 1, 1, Session("dfGOVTDEBTD" + cboFiles.SelectedIndex.ToString), Session("dfGOVTDEBTDB"))
        'Load Cell Government, ROW
        LoadValuesVar(reg, yr, "XIMFCREDIT", "Government", "ROW", 1, 1, Session("dfXIMFCREDIT" + cboFiles.SelectedIndex.ToString), Session("dfXIMFCREDITB"))
        LoadValuesVar(reg, yr, "XWBLOANS", "Government", "ROW", 1, 1, Session("dfXWBLOANS" + cboFiles.SelectedIndex.ToString), Session("dfXWBLOANSB"))
        LoadValuesVar(reg, yr, "XGOVTDEBTB", "Government", "ROW", 1, 1, Session("dfXGOVTDEBTB" + cboFiles.SelectedIndex.ToString), Session("dfXGOVTDEBTBB"))
        'Load Cell ROW, Firms
        LoadValuesVar(reg, yr, "XFDISTOUT", "ROW", "Firms", 1, 1, Session("dfXFDISTOUT" + cboFiles.SelectedIndex.ToString), Session("dfXFDISTOUTB"))
        LoadValuesVar(reg, yr, "XPORTSTOUT", "ROW", "Firms", 1, 1, Session("dfXPORTSTOUT" + cboFiles.SelectedIndex.ToString), Session("dfXPORTSTOUTB"))
        'Load Cell ROW, Government
        'LoadValuesVar reg, yr, "XDEBTRPA", "ROW", "Government", 1, 1
        bLastTime = True
        LoadValuesVar(reg, yr, "XRESERVES", "ROW", "Government", 1, 1, Session("dfXRESERVES" + cboFiles.SelectedIndex.ToString), Session("dfXRESERVESB"))

        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    '=============================SAM.Frm  ===========================
    'Subroutine to Load the Value
    'into Cell AX
    Private Sub LoadValuesAX(ByRef reg As Short, ByRef yr As Short)
        Dim Msg As String

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        On Error GoTo ErrorHandler
        Dim rsAX As DataTable
        'jrs 2006/08/28
        Dim rsAXBase As DataTable
        Dim TotVal As Double, TotValBase As Double
        Dim Filename As String
        If cboFiles.SelectedIndex = 0 Then
            Filename = Session("PutFdrvRun") & "SAMWorking.db"
        ElseIf cboFiles.SelectedIndex = 1 Then
            Filename = PutDataFileDir & "SAMBase.db" 'jrs 2007/01/03 move SAMBase.mdb to DATA
        Else
            Filename = Replace(Session("PutFdrvRun") & "SAM" & Mid(cboFiles.SelectedValue, InStr(cboFiles.SelectedValue, "-") + 2, Len(cboFiles.SelectedValue) - 1 - InStr(cboFiles.SelectedValue, "-")), ".run.db", ".db")
            If Dir(Filename) = "" Then

                LoadMessage("There is no SAM File for this option, using working file.", Session("Message"))

                'jam 2021/09/01
                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)


                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
        End If

        Dim sSQL As String
        If Not Session("SAMGroups") Then
            sSQL = "SELECT SUM(Value) as TotVal FROM AX WHERE Region =" & Str(reg) & " AND Year=" & Str(yr)
            'Else
            '    gLoadGroupMembers(GroupNames(reg), 0, 0, 0, Session("gCountGroupMembers"), Session("IFs_MDB2Active"), Session("gGroupMembrs"), _
            '    Session("gGroupMembersPtrs"), Session("gGroupMemberWeights"))
            '    Dim sRegs As String, j As Integer
            '    sRegs = CStr(Session("gGroupMemberWeights")(1) + 1)
            '    For j = 2 To Session("gCountGroupMembers")
            '        sRegs = sRegs & ", " & CStr(Session("gGroupMemberWeights")(j) + 1)
            '    Next
            '    sSQL = "SELECT SUM(Value) as TotVal FROM AX WHERE Region IN (" + sRegs + _
            '    ") AND Year=" + Str(yr) 
            'End If

            'JRS 09/09/03
            SetDataTablefromAccess(Filename, sSQL, "", rsAX)

            'jrs 2006/08/28
            TotVal = rsAX.Rows(0).Item("TotVal")
            If cboFiles.SelectedIndex <> 1 Then
                SetDataTablefromAccess(PutDataFileDir & "SAMBase.mdb", sSQL, "", rsAXBase) 'jrs 2007/01/03 move SAMBase.mdb to DATA
                TotValBase = rsAXBase.Rows(0).Item("TotVal")
            Else
                TotValBase = rsAX.Rows(0).Item("TotVal")
            End If
        Else 'jrs 2006/08/28 new grouping logic
            'jrs 2023/08/23 Dim Conn As OleDb.OleDbConnection
            Dim Conn As SqliteConnection = New SqliteConnection
            Conn.ConnectionString = "DataSource=" & Filename & ";"
            'Conn = ADO_OleDB_CreateConnection(Filename)
            Conn.Open()
            'jrs 2023/08/23 Dim IfsConn As OleDb.OleDbConnection
            Dim IfsConn As SqliteConnection = New SqliteConnection
            IfsConn.ConnectionString = "DataSource=" & Session("IFs_MDB2Active") & ";"
            'IfsConn = ADO_OleDB_CreateConnection(Session("IFs_MDB2Active"))
            IfsConn.Open()
            'tbl2 = New DataTable
            'jrs 2023/08/23 Dim rdr As OleDb.OleDbDataReader
            Dim rdr As SqliteDataReader
            'jrs 2023/08/23 Dim myCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand
            Dim myCMD As SqliteCommand = Conn.CreateCommand
            'myCMD.Connection = Conn
            Dim regDS As DataSet
            Dim grpRegRel As DataRelation
            Dim regRegIdRel As DataRelation
            Dim pppRow, ppRow, pRow, cRow As DataRow

            regDS = New DataSet
            regDS.EnforceConstraints = False

            'jrs 2023/08/23 Dim grpDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & GroupIDNew, IfsConn)
            Dim rdGrpCmd As SqliteCommand = IfsConn.CreateCommand
            rdGrpCmd.CommandText = "SELECT * FROM " & GroupIDNew
            Dim rdGrp As SqliteDataReader
            rdGrp = rdGrpCmd.ExecuteReader


            Dim myTbl As DataTable = New DataTable
            myTbl.TableName = "Groups"
            regDS.Tables.Add(myTbl)
            regDS.Tables(0).Load(rdGrp)

            'jrs 2023/08/23 Dim regDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & Grouping, IfsConn)  ' AND GroupName = '" & Session("EdCohort_regionname") & "'", IfsConn)
            Dim rdRegCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegCmd.CommandText = "SELECT * FROM " & Grouping
            Dim rdReg As SqliteDataReader
            rdReg = rdRegCmd.ExecuteReader

            Dim myTbl1 As DataTable = New DataTable
            myTbl1.TableName = "Regions"
            regDS.Tables.Add(myTbl1)
            regDS.Tables(1).Load(rdReg)

            'jrs 2023/08/23 Dim regIDDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM " & RegionIDNew, IfsConn)
            Dim rdRegIdCmd As SqliteCommand = IfsConn.CreateCommand
            rdRegIdCmd.CommandText = "SELECT * FROM " & RegionIDNew
            Dim rdRegId As SqliteDataReader
            rdRegId = rdRegIdCmd.ExecuteReader

            Dim myTbl2 As DataTable = New DataTable
            myTbl2.TableName = "RegionIDs"
            regDS.Tables.Add(myTbl2)
            regDS.Tables(2).Load(rdRegId)

            'jrs 2023/08/23 Dim axDA As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM AX Where Year = " & Str(yr), Conn)
            Dim rdAXCmd As SqliteCommand = Conn.CreateCommand
            rdAXCmd.CommandText = "SELECT * FROM AX Where Year = " & Str(yr)
            Dim rdAX As SqliteDataReader
            rdAX = rdRegIdCmd.ExecuteReader

            Dim myTbl3 As DataTable = New DataTable
            myTbl3.TableName = "AX"
            regDS.Tables.Add(myTbl3)
            regDS.Tables(3).Load(rdAX)

            'grpDA.Fill(regDS, "Groups")
            'regDA.Fill(regDS, "Regions")
            'regIDDA.Fill(regDS, "RegionIDs")
            'axDA.Fill(regDS, "AX")
            grpRegRel = regDS.Relations.Add("GrpRegion",
                                                 regDS.Tables("Groups").Columns("GroupNum"),
                                                 regDS.Tables("Regions").Columns("GroupNo"))
            regRegIdRel = regDS.Relations.Add("RegRegId",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("Regions").Columns("RegionNo"))
            Dim regAXRel As DataRelation = regDS.Relations.Add("RegAX",
                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                 regDS.Tables("AX").Columns("Region"))

            TotVal = 0
            For Each pppRow In regDS.Tables("Groups").Rows
                If pppRow.Item("GroupName") = GroupNames(reg) Then
                    For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                        For Each pRow In ppRow.GetParentRows(regRegIdRel)
                            For Each cRow In pRow.GetChildRows(regAXRel)
                                TotVal = TotVal + cRow.Item("Value")
                            Next
                        Next
                    Next
                End If
            Next

            If cboFiles.SelectedIndex <> 1 Then
                'jrs 2023/08/24 Dim axDABase As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM AX Where Year = " & Str(yr), Conn)
                Dim rdAXBaseCmd As SqliteCommand = Conn.CreateCommand
                rdAXBaseCmd.CommandText = "SELECT * FROM AX Where Year = " & Str(yr)
                Dim rdAXBase As SqliteDataReader
                rdAX = rdRegIdCmd.ExecuteReader

                Dim myTbl4 As DataTable = New DataTable
                myTbl4.TableName = "AXBase"
                regDS.Tables.Add(myTbl4)
                regDS.Tables(4).Load(rdAX)

                'axDABase.Fill(regDS, "AXBase")
                Dim regAXBaseRel As DataRelation = regDS.Relations.Add("RegAXBase",
                                                                 regDS.Tables("RegionIDs").Columns("RegionNum"),
                                                                 regDS.Tables("AXBase").Columns("Region"))
                TotValBase = 0
                For Each pppRow In regDS.Tables("Groups").Rows
                    If pppRow.Item("GroupName") = GroupNames(reg) Then
                        For Each ppRow In pppRow.GetChildRows(grpRegRel) 'regDS.Tables("Regions").Rows
                            For Each pRow In ppRow.GetParentRows(regRegIdRel)
                                For Each cRow In pRow.GetChildRows(regAXBaseRel)
                                    TotValBase = TotValBase + cRow.Item("Value")
                                Next
                            Next
                        Next
                    End If
                Next
            Else
                TotValBase = TotVal
            End If
            Conn.Close()
            IfsConn.Close()
        End If

        'Update Values Table
        Dim sUpdate As String
        'jrs 2004/11/26 sUpdate = "UPDATE SAMValues SET Sectors = Sectors+" & Str(System.Math.Round(rsAX.Rows(0).Item("TotVal"), 4)) & " WHERE Categories = 'Sectors'"
        'jrs 2004/11/26
        Dim i As Integer
        With DsSAMValues1.Tables(0)
            For i = 0 To .Rows.Count - 1
                If .Rows(i).Item("Categories") = "Sectors" Then
                    .Rows(i).Item("Sectors") = .Rows(i).Item("Sectors") + Math.Round(TotVal, 4)
                End If
            Next
        End With
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)
        'jrs 2004/11/26 sUpdate = "UPDATE SAMBASEValues SET Sectors = Sectors+" & Str(System.Math.Round(rsAX.Rows(0).Item("TotVal"), 4)) & " WHERE Categories = 'Sectors'"
        With rsSAMBASEValues
            For i = 0 To .Rows.Count - 1
                If .Rows(i).Item("Categories") = "Sectors" Then
                    .Rows(i).Item("Sectors") = .Rows(i).Item("Sectors") + Math.Round(TotValBase, 4)
                End If
            Next
        End With
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)
        Exit Sub

ErrorHandler:
        If Err.Number = 94 Then
            Msg = "The SAMWorking.db file doesn't have enough values to populate grid,"
            Msg = Msg & "please run model to correct "

            LoadMessage(Msg, Session("Message"))
            'jam 2021/09/01
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")

            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


            vurl = FriendlyUrl.Href("~/frm_Message")

            Response.Redirect(vurl)


        End If
    End Sub

    '=============================SAM.Frm  ===========================
    'Subroutine to Load the Value into Cell
    Private Sub LoadValuesVar(ByRef reg As Short, ByRef yr As Short, ByRef sVar As String, ByRef srowName As String, ByRef scolName As String, ByRef Factor As Single, ByRef iSizeDim2 As Short, ByRef df As IDataframe, ByRef dfB As IDataframe)
        'jrs 09/28/04
        Dim VarValBase As Single
        Dim NUMBER2BASE As Single
        Dim NUMBER2 As Single
        Dim NumberBASE As Single
        Dim Number As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim VarIdx As Short
        Dim VarVal As Single
        VarVal = 0
        'jrs 09/28/04
        VarValBase = 0

        VarIdx = IfsVars.Item(sVar).Index
        'jrs 2021/04/05
        ''jrs 2020/05/07
        'MyDF = df
        'MyDFB = dfB

        Dim Vals(1) As Single, Bases(1) As Single
        'For every Dimension2
        Dim i As Integer, Dim2Max As Integer
        If VDim2(VarIdx) > 0 Then
            Dim2Max = MAXCOM(VDim2(VarIdx)) - 1
            'jrs 2025/09/01
            'if doing total, avoid double counting
            If GRPS(VDim2(VarIdx), Dim2Max) = "Total" Then
                Dim2Max = Dim2Max - 1
            End If
        Else
            Dim2Max = 1
        End If
        'For every Dimension 3
        Dim j As Integer, Dim3Max As Integer
        If VDim3(VarIdx) > 0 Then
            Dim3Max = MAXCOM(VDim3(VarIdx)) - 1
        Else
            Dim3Max = 1
        End If

        For i = 1 To Dim2Max
            For j = 1 To Dim3Max
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", i, "", j, "",
                cboFiles.SelectedIndex, False,
                False, Session("SAMGroups"),
                1, Vals, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", i, "", j, "",
                    cboFiles.SelectedIndex, (bFirstTime And i = 1 And j = 1),
                    (bLastTime And i = Dim2Max And j = Dim3Max), Session("SAMGroups"),
                    1, Vals, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If
                VarVal = VarVal + Vals(1)
                If cboFiles.SelectedIndex <> 1 Then
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", i, "", j, "", 1, False, False, Session("SAMGroups"),
                        1, Bases, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay1"), Session("df" + sVar + "1"))
                    Else
                        gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", i, "", j, "", 1, True, True, Session("SAMGroups"),
                        1, Bases, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    End If
                    VarValBase = VarValBase + Bases(1)
                Else
                    VarValBase = VarValBase + Vals(1)
                End If
            Next j
        Next i

        'Update Values Table
        Dim sUpdate As String
        'If factor < 0 And RegName$(reg) = "United States of America" Then Stop
        'jrs 2004/11/26 sUpdate = "UPDATE SAMValues SET " & scolName & " = " & scolName & "+" & Str(System.Math.Round(VarVal * Factor, 4)) & " WHERE Categories = '" & srowName & "'"
        With DsSAMValues1.Tables(0)
            For i = 0 To .Rows.Count - 1
                If .Rows(i).Item("Categories") = srowName Then
                    .Rows(i).Item(scolName) = .Rows(i).Item(scolName) + Math.Round(VarVal * Factor, 4)
                    Exit For
                End If
            Next
        End With
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)

        'jrs 2004/11/26 sUpdate = "UPDATE SAMBASEValues SET " & scolName & " = " & scolName & "+" & Str(System.Math.Round(VarValBase * Factor, 4)) & " WHERE Categories = '" & srowName & "'"
        With rsSAMBASEValues
            For i = 0 To .Rows.Count - 1
                If .Rows(i).Item("Categories") = srowName Then
                    .Rows(i).Item(scolName) = .Rows(i).Item(scolName) + Math.Round(VarValBase * Factor, 4)
                    Exit For
                End If
            Next
        End With
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)

        'JRS 06/18/03
        If sVar = "AID" And srowName = "ROW" And scolName = "Government" Then
            valDonate = valDonate + System.Math.Round(VarVal * Factor, 4)
            valDonateBase = valDonateBase + System.Math.Round(VarValBase * Factor, 4)
        End If
        'JRS 06/26/03
        If sVar = "XWORKREMIT" And srowName = "Household" And scolName = "ROW" Then
            valWorkRemit = valWorkRemit + System.Math.Round(VarVal * Factor, 4)
            valWorkRemitBase = valWorkRemitBase + System.Math.Round(VarValBase * Factor, 4)
        End If

        'jrs 2021/04/05 df = MyDF
    End Sub


    '=============================SAM.Frm  ===========================
    'Subroutine to Set to 0 all values in
    'table SAMValues
    Private Sub ClearValues()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2004/11/26 Dim sUpdate As String
        'jrs 2004/11/26 sUpdate = "UPDATE SAMValues SET Sectors = 0, Household = 0, Firms=0, " & "Capital = 0, Government = 0, ROW = 0, Environment = 0, Total = 0"
        Dim i As Integer
        With DsSAMValues1.Tables(0)
            For i = 0 To .Rows.Count - 1
                .Rows(i).Item("Sectors") = 0
                .Rows(i).Item("Household") = 0
                .Rows(i).Item("Firms") = 0
                .Rows(i).Item("Capital") = 0
                .Rows(i).Item("Government") = 0
                .Rows(i).Item("ROW") = 0
                .Rows(i).Item("Environment") = 0
                .Rows(i).Item("Total") = 0
            Next
        End With
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)

        'jrs 2004/11/26 sUpdate = "UPDATE SAMBaseValues SET Sectors = 0, Household = 0, Firms=0, " & "Capital = 0, Government = 0, ROW = 0, Environment = 0, Total = 0"
        With rsSAMBASEValues
            For i = 0 To .Rows.Count - 1
                .Rows(i).Item("Sectors") = 0
                .Rows(i).Item("Household") = 0
                .Rows(i).Item("Firms") = 0
                .Rows(i).Item("Capital") = 0
                .Rows(i).Item("Government") = 0
                .Rows(i).Item("ROW") = 0
                .Rows(i).Item("Environment") = 0
                .Rows(i).Item("Total") = 0
            Next
        End With
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)

        'JRS 09/18/02 AGGREGATION
        POPval = 0
        GDPval = 0
        POPBaseVal = 0
        GDPBaseVal = 0
        'JRS 06/18/03
        valDonate = 0
        valDonateBase = 0
        'JRS 06/26/03
        valWorkRemit = 0
        valWorkRemitBase = 0

    End Sub

    '=============================SAM.Frm  ===========================
    'Subroutine to load countries in a group
    Private Sub LoadCountries(ByRef GroupName As String)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim valtblG As DataTable
        Dim resG As DataTable

        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew & " WHERE GroupName = """ & GroupName & """", "", resG)
        Dim GroupNum As Short
        GroupNum = resG.Rows(0).Item("GroupNum")

        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Grouping, "", valtblG)

        Dim iTotal As Short
        iTotal = 0

        Dim i As Integer
        i = 0
        '   Load Group Member Names List Box
        If Not i = valtblG.Rows.Count Then

            Do While Not i = valtblG.Rows.Count
                If valtblG.Rows(i).Item("GroupNo") = GroupNum Then
                    iTotal = iTotal + 1
                    CountryGroup(iTotal) = valtblG.Rows(i).Item("Region") 'resR.Rows(i).Item("Country")
                End If
                i = i + 1
            Loop
        End If

        GroupTot = iTotal
    End Sub


    '=============================SAM.Frm  ===========================
    'Subroutine to calculate totals in table SAMValues
    Private Sub LoadValuesTot()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2004/11/26
        'Dim sSQL As String
        'Dim sFrom As String
        'sSQL = "SELECT SUM(Sectors) as tSectors, " & "SUM(Household) as tHousehold, " & "SUM(Firms) AS tFirms, SUM(Capital) AS tCapital, " & "SUM(Government) AS tGovernment, " & "SUM(ROW) AS tROW "
        'sFrom = "FROM SAMValues WHERE Categories <> 'Total' AND Categories <> 'Environment'"
        'Dim rsTot As DataTable
        'Dim rsTotBase As DataTable
        'SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", sSQL & sFrom, "", rsTot)
        Dim i As Integer
        Dim tSectors As Double, tHousehold As Double, tFirms As Double, tCapital As Double, tGovernment As Double, tROW As Double
        With DsSAMValues1.Tables(0)
            For i = 0 To .Rows.Count - 1
                tSectors = tSectors + .Rows(i).Item("Sectors")
                tHousehold = tHousehold + .Rows(i).Item("Household")
                tFirms = tFirms + .Rows(i).Item("Firms")
                tCapital = tCapital + .Rows(i).Item("Capital")
                tGovernment = tGovernment + .Rows(i).Item("Government")
                tROW = tROW + .Rows(i).Item("ROW")
            Next
        End With
        'jrs 2004/11/26 sFrom = "FROM SAMBaseValues WHERE Categories <> 'Total' AND Categories <> 'Environment'"
        'jrs 2004/11/26 SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", sSQL & sFrom, "", rsTotBase)

        Dim btSectors As Double, btHousehold As Double, btFirms As Double, btCapital As Double, btGovernment As Double, btROW As Double
        With rsSAMBASEValues
            For i = 0 To .Rows.Count - 1
                btSectors = btSectors + .Rows(i).Item("Sectors")
                btHousehold = btHousehold + .Rows(i).Item("Household")
                btFirms = btFirms + .Rows(i).Item("Firms")
                btCapital = btCapital + .Rows(i).Item("Capital")
                btGovernment = btGovernment + .Rows(i).Item("Government")
                btROW = btROW + .Rows(i).Item("ROW")
            Next
        End With
        'jrs 2004/11/26 sFrom = "FROM SAMBaseValues WHERE Categories <> 'Total' AND Categories <> 'Environment'"
        'jrs 2004/11/26 SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", sSQL & sFrom, "", rsTotBase)
        Dim rowTotal As Integer = -1, rowTotalBase As Integer = -1
        'Update Totals
        'jrs 2004/11/26 Dim sUpdate As String
        'jrs 2004/11/26 sUpdate = "UPDATE SAMValues SET Sectors =" & Str(rsTot.Rows(0).Item("tSectors")) & ", Household=" & Str(rsTot.Rows(0).Item("tHousehold")) & ", Firms=" & Str(rsTot.Rows(0).Item("tFirms")) & ", Capital=" & Str(rsTot.Rows(0).Item("tCapital")) & ", Government=" & Str(rsTot.Rows(0).Item("tGovernment")) & ", ROW=" & Str(rsTot.Rows(0).Item("tROW")) & " WHERE Categories = 'Total'"
        With DsSAMValues1.Tables(0)
            For i = .Rows.Count - 1 To 0 Step -1
                If .Rows(i).Item("Categories") = "Total" Then
                    .Rows(i).Item("Sectors") = tSectors
                    .Rows(i).Item("Household") = tHousehold
                    .Rows(i).Item("Firms") = tFirms
                    .Rows(i).Item("Capital") = tCapital
                    .Rows(i).Item("Government") = tGovernment
                    .Rows(i).Item("ROW") = tROW
                    rowTotal = i
                    'jrs 2005/02/19
                    '.Rows(i).Item("Total") = tSectors + tHousehold + tFirms + tCapital + tGovernment + tROW
                    'Exit For
                End If
                .Rows(i).Item("Total") = .Rows(i).Item("Sectors") + .Rows(i).Item("Household") + .Rows(i).Item("Firms") + .Rows(i).Item("Capital") + .Rows(i).Item("Government") + .Rows(i).Item("ROW")
                'jrs 2006/07/07
                If .Rows(i).Item("Categories") = "Sectors" Then
                    .Rows(i).Item("Firms") = tSectors - .Rows(i).Item("Total")
                    Session("SectorsFirmsVal") = tSectors - .Rows(i).Item("Total")
                    .Rows(i).Item("Total") = .Rows(i).Item("Total") + .Rows(i).Item("Firms")
                End If
            Next
            .Rows(rowTotal).Item("Firms") = .Rows(rowTotal).Item("Firms") + Session("SectorsFirmsVal")
            .Rows(rowTotal).Item("Total") = .Rows(rowTotal).Item("Total") + Session("SectorsFirmsVal")
        End With
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)

        'jrs 2004/11/26 sUpdate = "UPDATE SAMBaseValues SET Sectors =" & Str(rsTotBase.Rows(0).Item("tSectors")) & ", Household=" & Str(rsTotBase.Rows(0).Item("tHousehold")) & ", Firms=" & Str(rsTotBase.Rows(0).Item("tFirms")) & ", Capital=" & Str(rsTotBase.Rows(0).Item("tCapital")) & ", Government=" & Str(rsTotBase.Rows(0).Item("tGovernment")) & ", ROW=" & Str(rsTotBase.Rows(0).Item("tROW")) & " WHERE Categories = 'Total'"
        With rsSAMBASEValues
            For i = .Rows.Count - 1 To 0 Step -1
                If .Rows(i).Item("Categories") = "Total" Then
                    .Rows(i).Item("Sectors") = btSectors
                    .Rows(i).Item("Household") = btHousehold
                    .Rows(i).Item("Firms") = btFirms
                    .Rows(i).Item("Capital") = btCapital
                    .Rows(i).Item("Government") = btGovernment
                    .Rows(i).Item("ROW") = btROW
                    rowTotalBase = i
                    'jrs 2005/02/19
                    '.Rows(i).Item("Total") = btSectors + btHousehold + btFirms + btCapital + btGovernment + btROW
                    'Exit For
                End If
                .Rows(i).Item("Total") = .Rows(i).Item("Sectors") + .Rows(i).Item("Household") + .Rows(i).Item("Firms") + .Rows(i).Item("Capital") + .Rows(i).Item("Government") + .Rows(i).Item("ROW")
                'jrs 2006/07/07
                If .Rows(i).Item("Categories") = "Sectors" Then
                    .Rows(i).Item("Firms") = btSectors - .Rows(i).Item("Total")
                    Session("SectorsFirmsValBase") = btSectors - .Rows(i).Item("Total")
                    .Rows(i).Item("Total") = .Rows(i).Item("Total") + .Rows(i).Item("Firms")
                End If
            Next
            .Rows(rowTotalBase).Item("Firms") = .Rows(rowTotalBase).Item("Firms") + Session("SectorsFirmsValBase")
            .Rows(rowTotalBase).Item("Total") = .Rows(rowTotalBase).Item("Total") + Session("SectorsFirmsValBase")
        End With
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)

        'jrs 2004/11/26 sUpdate = "UPDATE SAMValues SET Total = Sectors + Household " & "+ Firms + Capital + Government + ROW WHERE Categories = " & "'Capital' OR Categories = 'ROW' OR Categories = 'Sectors' " & "OR Categories = 'Firms' OR Categories = 'Government' OR Categories = 'Total'"
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)

        'jrs 2004/11/26 sUpdate = "UPDATE SAMBaseValues SET Total = Sectors + Household " & "+ Firms + Capital + Government + ROW WHERE Categories = " & "'Capital' OR Categories = 'ROW' OR Categories = 'Sectors' " & "OR Categories = 'Firms' OR Categories = 'Government' OR Categories = 'Total'"
        'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sUpdate)
        'jrs 2004/11/26 ExecuteSql(samConn, sUpdate)

    End Sub

    '=============================SAM.Frm  ===========================
    'Subroutine to load detail form with AX data
    Private Sub DisplayAX(ByRef reg As Short, ByRef yr As Short, Optional ByRef grd As Object = Nothing, Optional ByRef sCellDesc As String = "")
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim grd1 As TMSWebControls.AdvWebGrid

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        If grd Is Nothing Then
            grd1 = grdSAMDet
        Else
            grd1 = grd
        End If

        'Title
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, Sectors: " & cmb_Country.SelectedValue & ", " & Cmb_Year.SelectedValue
        'Read data to display
        Dim sSQL As String

        Dim rsAXDet As DataTable

        'jrs 2006/04/27
        If Not Session("SAMGroups") Then
            sSQL = "SELECT Row, Column, Value AS sValue FROM AX WHERE Region =" + Str(reg) +
            " AND Year=" + Str(yr)
        Else
            gLoadGroupMembers(GroupNames(reg), 0, 0, 0, Session("gCountGroupMembers"), Session("IFs_MDB2Active"), Session("gGroupMembrs"),
            Session("gGroupMembersPtrs"), Session("gGroupMemberWeights"))
            Dim sRegs As String, i As Integer
            sRegs = CStr(Session("gGroupMemberWeights")(1) + 1)
            For i = 2 To Session("gCountGroupMembers")
                sRegs = sRegs & ", " & CStr(Session("gGroupMemberWeights")(i) + 1)
            Next i
            sSQL = "SELECT Row, Column, SUM(Value) AS sValue FROM AX WHERE Region IN (" + sRegs +
            ") AND Year=" + Str(yr) + " GROUP BY Row, Column"
        End If

        'JRS 09/09/03
        Dim Filename As String
        If cboFiles.SelectedIndex = 0 Then
            SetDataTablefromAccess(Session("PutFdrvRun") & "SAMWorking.mdb", sSQL, "", rsAXDet)
        ElseIf cboFiles.SelectedIndex = 1 Then
            SetDataTablefromAccess(PutDataFileDir & "SAMBase.mdb", sSQL, "", rsAXDet) 'jrs 2007/01/03 move SAMBase.mdb to DATA
        Else
            Filename = Replace(Session("PutFdrvRun") & "SAM" & Mid(cboFiles.SelectedValue, InStr(cboFiles.SelectedValue, "-") + 2, Len(cboFiles.SelectedValue) - 1 - InStr(cboFiles.SelectedValue, "-")), ".run", ".mdb")
            If Dir(Filename) > "" Then
                SetDataTablefromAccess(Filename, sSQL, "", rsAXDet)
            Else
                SetDataTablefromAccess(Session("PutFdrvRun") & "SAMWorking.mdb", sSQL, "", rsAXDet)

                LoadMessage("There is no SAM File for this option, using working file.", Session("Message"))
                'jam 2021/09/01
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)


            End If
        End If

        'Load values into form
        Dim oldRow As Short
        Dim iRow As Short
        Dim iCol As Short
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        With grd1
            .TotalRows = NSec + 1
            .Columns.Clear()
            '.ClearRows(1, .TotalRows)
            Dim myColRH As New TMSWebControls.Column
            myColRH.WidthType = TMSWebControls.WType.Absolute
            myColRH.Width = 100
            myColRH.Color = System.Drawing.Color.Gray
            .Columns.Add(myColRH)

            Dim i As Integer
            For i = 0 To rsAXDet.Rows.Count - 1
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRS 04/17/04
                iRow = rsAXDet.Rows(i).Item("Row")
                iCol = rsAXDet.Rows(i).Item("Column")
                If iRow = 1 Then
                    Dim myCol As New TMSWebControls.Column
                    myCol.WidthType = TMSWebControls.WType.Absolute
                    myCol.Width = 100
                    .Columns.Add(myCol)
                    .Item(iCol, 0) = GRPS(2, iCol)
                End If
                .Item(iCol, iRow) = System.Math.Round(Val(.Item(iCol, iRow)) + rsAXDet.Rows(i).Item("sValue"), 5)
                'mti May 2006 - cell description for full SAM (shows up on mousedown)
                If Not (grd Is Nothing) Then
                    ArrDesc(iRow, iCol) = sCellDesc
                End If
                'mti May 2006 -end

                'Set row labels
                If iCol = 1 Then
                    .Item(0, iRow) = GRPS(2, iRow)
                End If
            Next i
        End With

    End Sub

    '=============================SAM.Frm  ===========================
    'Subroutine to load detail form with VAR data for cell
    Private Sub DisplayVAR(ByRef reg As Short, ByRef yr As Short, ByRef sVar As String,
    ByRef srowName As String, ByRef scolName As String, ByRef Factor As Single,
    ByRef arowLabels As Object, ByRef acolLabels As Object, ByRef bAddRow As Boolean,
    Optional ByRef grd As Object = Nothing, Optional ByRef sCellDesc As String = "")
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti May 2006 - changed procedure - made MSFlexGrid a parameter;  take full
        Dim grd1 As TMSWebControls.AdvWebGrid
        If grd Is Nothing Then
            grd1 = grdSAMDet
        Else
            grd1 = grd
        End If

        'Title
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = srowName & ", " & scolName & ": " & cmb_Country.SelectedValue & ", " & Cmb_Year.SelectedValue

        Dim Filename As String
        Dim FileNumber As Short
        Dim RECORD As Integer
        Dim VarIdx As Short
        Dim VarVal As Single
        Dim VarVals(1) As Single
        Dim bDimCol As Boolean

        VarVal = 0
        bDimCol = (Not (srowName = "Sectors") And Not (srowName = "Household")) Or scolName = "Sectors"

        VarIdx = IfsVars.Item(sVar).Index

        'For every Sector
        Dim icol, dim2idx, iRow, firstRow As Object
        Dim oldRow As Short
        'Load values into form
        Dim icolIdx As Short
        With grd1
            If bAddRow Then
                firstRow = .TotalRows
                .TotalRows = .TotalRows + UBound(arowLabels) + 1

                .Columns.Clear()
                .ClearRows(firstRow, .TotalRows)
                Dim I As Integer
                For I = 1 To UBound(acolLabels) + 2
                    Dim myColRH As New TMSWebControls.Column
                    myColRH.WidthType = TMSWebControls.WType.Absolute
                    myColRH.Width = 100
                    .Columns.Add(myColRH)
                Next I
            Else
                If arowLabels(0) = grdSAMDet(0, 1) Then
                    firstRow = 1
                Else
                    firstRow = .TotalRows - (UBound(arowLabels) + 1)
                End If
            End If
            'Set row labels
            'JRS 09/18/02
            If bAddRow Then
                For icolIdx = 1 To UBound(arowLabels) + 1
                    .Item(0, icolIdx + firstRow - 1) = arowLabels(icolIdx - 1)
                Next icolIdx
            End If

            Dim MyFileforDisplay As IStorage
            Dim MyDF As IDataframe

            'JRS 09/03/02
            'if Columns define dimension
            If bDimCol Then
                For dim2idx = 0 To UBound(acolLabels)
                    'Check if there is a second dimension
                    If VDim2(VarIdx) > 0 Or dim2idx = 0 Then
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", dim2idx + 1, "", -1, "", cboFiles.SelectedIndex, False, False,
                        Session("SAMGroups"), 1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
                        Else
                            gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", dim2idx + 1, "", -1, "", cboFiles.SelectedIndex, dim2idx = 0, dim2idx = UBound(acolLabels),
                            Session("SAMGroups"), 1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                            Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        End If
                    End If
                    'Update headers
                    .Item(dim2idx + 1, 0) = acolLabels(dim2idx)
                    For iRow = 1 To UBound(arowLabels) + 1
                        .Item(dim2idx + 1, iRow + firstRow - 1) = System.Math.Round(Val(.Item(dim2idx + 1, iRow + firstRow - 1)) + VarVals(1) * Factor, 4)
                        'mti May 2006 - cell description for full SAM (shows up on mousedown)
                        If Not (grd Is Nothing) Then
                            ArrDesc(iRow + firstRow - 1, icolIdx) = sCellDesc
                        End If
                        'mti May 2006 -end
                    Next iRow
                Next dim2idx
                'JRS 09/03/02
                'if Rows define dimension
            Else
                For dim2idx = 0 To UBound(arowLabels)
                    'Check if there is a second dimension
                    'UPGRADE_WARNING: Couldn't resolve default property of object dim2idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If VDim2(VarIdx) > 0 Or dim2idx = 0 Then
                        If Session("mnuSQLite") = "True" Then
                            gLoadForDisplayNET(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", dim2idx + 1, "", -1, "", cboFiles.SelectedIndex, False, False,
                        Session("SAMGroups"), 1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
                        Else
                            gLoadForDisplay(sVar, VarIdx, cmb_Country.SelectedValue, reg, "", dim2idx + 1, "", -1, "", cboFiles.SelectedIndex, dim2idx = 0, dim2idx = UBound(arowLabels),
                            Session("SAMGroups"), 1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                            Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        End If
                    End If
                    For icol = 1 To UBound(acolLabels) + 1
                        .Item(icol, dim2idx + firstRow) = System.Math.Round(Val(.Item(icol, dim2idx + firstRow)) + VarVals(1) * Factor, 4)
                        'mti May 2006 - cell description for full SAM (shows up on mousedown)
                        If Not (grd Is Nothing) Then
                            ArrDesc(dim2idx + firstRow, icol) = sCellDesc
                        End If
                        'mti May 2006 -end
                        'if it's the first row
                        If dim2idx = 0 Then
                            .Item(icol, 0) = acolLabels(icol - 1)
                        End If
                    Next icol
                Next dim2idx
            End If
        End With

    End Sub


    '=============================frm_SAM.Frm==============================
    Private Sub CurrencyCheck(ByRef SEL As Short)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   JRS 5-1-01
        '- - - - - - - - - - - - - - By Jose Solorzano - - - - - - - - - - - - - - -
        '- - - - - - - - - - - - - - RA Working for Barry Hughes - - - - - - - - - - - - - - -
        '- - - - - - - - - - - - - - 5-01-01 - - - - - - - - - - - - - - -
        'JRS 10/15/02 CURRENCY LABEL
        Dim MyPos As Integer
        Dim S As String
        Dim s2 As String
        Dim i As Integer

        'Checks if the flag for this variable is ON
        'UPGRADE_WARNING: Couldn't resolve default property of object VCurrency(SEL). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        bCnvrtCurr = VCurrency(SEL)
        'If the option is ON procede
        If bCnvrtCurr Then
            'jrs 2005/12/09 ReadCurrency()
            'If it returns information, load it to the variables
            If rsCurr.Rows.Count > 0 Then
                'Updates the Currency Factor
                CurrencyFct = rsCurr.Rows(0).Item("Currency Value")
                'JRS 6/19/01 CurrencyLableSwitch
                'Update the name of the Units
                'JRS 10/15/02 CURRENCY LABEL
                'UPGRADE_WARNING: Couldn't resolve default property of object VUnits(SEL). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                s2 = VUnits(SEL)
                MyPos = InStr(1, s2, "$", CompareMethod.Text)
                S = RTrim(rsCurr.Rows(0).Item("Symbol"))
                If MyPos > 0 Then
                    lblUnits.Text = lblUnits.Text & " " & VB.Left(s2, MyPos - 1) & S & VB.Right(s2, Len(s2) - MyPos)
                Else
                    lblUnits.Text = lblUnits.Text & " " & s2
                End If

            Else
                CurrencyFct = 1
                'JRS 10/15/02 CURRENCY LABEL
                'UPGRADE_WARNING: Couldn't resolve default property of object VUnits(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                lblUnits.Text = lblUnits.Text & " " + VUnits(SEL)
            End If

        Else
            CurrencyFct = 1
            'JRS 10/15/02 CURRENCY LABEL
            'UPGRADE_WARNING: Couldn't resolve default property of object VUnits(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            lblUnits.Text = lblUnits.Text & " " + VUnits(SEL)
        End If
        'JRS 10/15/02 CURRENCY LABEL
        lblUnits.Text = lblUnits.Text & " except Environmental cells."
    End Sub


    '=============================SAM.Frm  ===========================
    Private Sub ValidateAID()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim i As Integer

        'jrs 2004/11/26 Dim sSQL As String
        'Receipts Foreign AID
        If valDonate > 0 Then
            'jrs 2004/11/26 sSQL = "UPDATE SAMValues SET Government = Government - " & Str(valDonate) & " WHERE Categories = 'ROW'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            With DsSAMValues1.Tables(0)
                For i = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i).Item("Categories") = "ROW" Then
                        .Rows(i).Item("Government") = .Rows(i).Item("Government") - valDonate
                        Exit For
                    End If
                Next
            End With
            bDonate = False
            'Donates Foreign AID
        Else
            'jrs 2004/11/26 sSQL = "UPDATE SAMValues SET ROW = ROW + " & Str(System.Math.Abs(valDonate)) & " WHERE Categories = 'Government'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)

            'jrs 2004/11/26 sSQL = "UPDATE SAMValues SET Government = Government + " & Str(System.Math.Abs(2 * valDonate)) & " WHERE Categories = 'ROW'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)

            With DsSAMValues1.Tables(0)
                For i = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i).Item("Categories") = "Government" Then
                        .Rows(i).Item("ROW") = .Rows(i).Item("ROW") + Math.Abs(valDonate)
                    End If
                    If .Rows(i).Item("Categories") = "ROW" Then
                        .Rows(i).Item("Government") = .Rows(i).Item("Government") + Math.Abs(2 * valDonate)
                    End If
                Next
            End With

            bDonate = True
        End If
        'jrs 2006/10/27
        Session("bDonate") = bDonate

        '************************** Receipts Foreign AID ******************
        '*************************     for base case     ******************
        If valDonateBase > 0 Then
            'jrs 2004/11/26 sSQL = "UPDATE SAMBaseValues SET Government = Government - " & Str(valDonateBase) & " WHERE Categories = 'ROW'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            With rsSAMBASEValues
                For i = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i).Item("Categories") = "ROW" Then
                        .Rows(i).Item("Government") = .Rows(i).Item("Government") - valDonateBase
                        Exit For
                    End If
                Next
            End With
            'Donates Foreign AID
        Else
            'jrs 2004/11/26 sSQL = "UPDATE SAMBaseValues SET ROW = ROW + " & Str(System.Math.Abs(valDonateBase)) & " WHERE Categories = 'Government'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            'jrs 2004/11/26 sSQL = "UPDATE SAMBaseValues SET Government = Government + " & Str(System.Math.Abs(2 * valDonateBase)) & " WHERE Categories = 'ROW'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            With rsSAMBASEValues
                For i = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i).Item("Categories") = "Government" Then
                        .Rows(i).Item("ROW") = .Rows(i).Item("ROW") + Math.Abs(valDonateBase)
                    End If
                    If .Rows(i).Item("Categories") = "ROW" Then
                        .Rows(i).Item("Government") = .Rows(i).Item("Government") + Math.Abs(2 * valDonateBase)
                    End If
                Next
            End With
        End If
    End Sub

    '=============================SAM.Frm  ===========================
    'JRS 06/25/03
    Private Sub ValidateWorkRemit()
        Dim i As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2004/11/26 Dim sSQL As String
        'Receipts Foreign AID
        If valWorkRemit > 0 Then
            'jrs 2004/11/26 sSQL = "UPDATE SAMValues SET Firms = Firms - " & Str(valWorkRemit) & " WHERE Categories = 'ROW'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            With DsSAMValues1.Tables(0)
                For i = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i).Item("Categories") = "ROW" Then
                        .Rows(i).Item("Firms") = .Rows(i).Item("Firms") - valWorkRemit
                        Exit For
                    End If
                Next
            End With
            bWorkRemit = True
            'Donates Foreign AID
        Else
            'jrs 2004/11/26 sSQL = "UPDATE SAMValues SET ROW = ROW + " & Str(System.Math.Abs(valWorkRemit)) & " WHERE Categories = 'Household'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            'jrs 2004/11/26 sSQL = "UPDATE SAMValues SET Firms = Firms + " & Str(System.Math.Abs(2 * valWorkRemit)) & " WHERE Categories = 'ROW'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            With DsSAMValues1.Tables(0)
                For i = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i).Item("Categories") = "Household" Then
                        .Rows(i).Item("ROW") = .Rows(i).Item("ROW") + Math.Abs(valWorkRemit)
                    End If
                    If .Rows(i).Item("Categories") = "ROW" Then
                        .Rows(i).Item("Firms") = .Rows(i).Item("Firms") + Math.Abs(2 * valWorkRemit)
                    End If
                Next
            End With
            bWorkRemit = False
        End If
        '************************** Work Remit ********************
        '*************************     for base case     ******************
        If valWorkRemitBase > 0 Then
            'jrs 2004/11/26 sSQL = "UPDATE SAMBaseValues SET Firms = Firms - " & Str(valWorkRemitBase) & " WHERE Categories = 'ROW'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            With rsSAMBASEValues
                For i = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i).Item("Categories") = "ROW" Then
                        .Rows(i).Item("Firms") = .Rows(i).Item("Firms") - valWorkRemitBase
                        Exit For
                    End If
                Next
            End With
            'Donates Foreign AID
        Else
            'jrs 2004/11/26 sSQL = "UPDATE SAMBaseValues SET ROW = ROW + " & Str(System.Math.Abs(valWorkRemitBase)) & " WHERE Categories = 'Household'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            'jrs 2004/11/26 sSQL = "UPDATE SAMBaseValues SET Firms = Firms + " & Str(System.Math.Abs(2 * valWorkRemitBase)) & " WHERE Categories = 'ROW'"
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.mdb", sSQL)
            'jrs 2004/11/26 ExecuteSql(samConn, sSQL)
            With rsSAMBASEValues
                For i = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i).Item("Categories") = "Household" Then
                        .Rows(i).Item("ROW") = .Rows(i).Item("ROW") + Math.Abs(valWorkRemitBase)
                    End If
                    If .Rows(i).Item("Categories") = "ROW" Then
                        .Rows(i).Item("Firms") = .Rows(i).Item("Firms") + Math.Abs(2 * valWorkRemitBase)
                    End If
                Next
            End With
        End If
        'jrs 2006/07/10
        Session("bWorkRemit") = bWorkRemit
    End Sub

    Public Sub mnuPopUpCellExpand_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuPopUpCellExpand_Click(eventSender, eventArgs)
    End Sub


    Public Sub mnuPopUpCellOverTime_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mnuPopUpCellOverTime_Click(eventSender, eventArgs)
    End Sub

    Public Sub mnuSAM2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If bSAM2 Then
            cswmWebMenu.Groups("mnuSwitch").Items("mnuSAM2").Caption = "Show Flows"
            'JRS 04/16/03
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            With dbgSAM
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(0).HeaderText = "Liabilities of"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(1).HeaderText = "Assets of Sectors"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(2).HeaderText = "Assets of Household"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(3).HeaderText = "Assets of Firms"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(4).HeaderText = "Assets of Capital"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(5).HeaderText = "Assets of Government"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(6).HeaderText = "Assets of ROW"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(7).HeaderText = "Assets of Total"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(8).HeaderText = "Assets of Environment"
            End With
        Else
            cswmWebMenu.Groups("mnuSwitch").Items("mnuSAM2").Caption = "Show Stocks"
            'JRS 04/16/03
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            With dbgSAM
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(0).HeaderText = "Supplied by"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(1).HeaderText = "Used by Sectors"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(2).HeaderText = "Used by Household"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(3).HeaderText = "Used by Firms"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(4).HeaderText = "Used by Capital"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(5).HeaderText = "Used by Government"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(6).HeaderText = "Used by ROW"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(7).HeaderText = "Used by Total"
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().HeaderText. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.ColumnsCollection from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                .Columns(8).HeaderText = "Environment"
            End With
        End If
        'CHANGE FLAG
        bSAM2 = Not bSAM2
        'REFRESH GRID
        Cmb_Year_SelectedIndexChanged(Cmb_Year, New System.EventArgs)
        'JRS 03/30/04
        Session("bSAM2") = bSAM2
    End Sub

    '=============================SAM.Frm  ===========================
    'UPGRADE_WARNING: Event optLabels.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub optLabels_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optLabels.CheckedChanged
        If eventSender.Checked Then
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            cmb_Country.Enabled = False
            Cmb_Year.Enabled = False
            cboFiles.Enabled = False
            'Change table to display
            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            'JRS 2/4/03 SAM2
            If bSAM2 Then
                'UPGRADE_ISSUE: Data property DataSAM.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
                'JRS 12/06/03 DataSAM.RecordSource = "SAMLabels"
                'JRSVBNET Support.DataGridManager.manager.SetGridDataSource(dbgSAM, DsSAMLabels1)
                dbgSAM.DataMember = "SAMLabels"
                DsSAMLabels1.Clear()
                'jrs 2023/08/23
                rdSAMLabels = DbSelectCommand2.ExecuteReader
                Dim myTbl As DataTable = New DataTable
                DsSAMLabels1.EnforceConstraints = False
                DsSAMLabels1.Tables.Add(myTbl)
                DsSAMLabels1.Tables(0).Load(rdSAMLabels)
                'OleDbDASAMLabels.Fill(DsSAMLabels1)
            Else
                'UPGRADE_ISSUE: Data property DataSAM.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
                'JRS 12/06/03 DataSAM.RecordSource = "SAMLabels2"
                'JRSVBNET Support.DataGridManager.manager.SetGridDataSource(dbgSAM, DsSAMLabels21)
                dbgSAM.DataMember = "SAMLabels2"
                DsSAMLabels21.Clear()
                'jrs 2023/08/23
                rdSAMLabels1 = DbSelectCommand3.ExecuteReader
                Dim myTbl As DataTable = New DataTable
                DsSAMLabels21.EnforceConstraints = False
                DsSAMLabels21.Tables.Add(myTbl)
                DsSAMLabels21.Tables(0).Load(rdSAMLabels1)
                'OleDbDASAMLabels.Fill(DsSAMLabels21)
            End If
            'jrs 11/08/04
            dbgSAM.DataBind()

            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    '=============================SAM.Frm  ===========================
    'UPGRADE_WARNING: Event optValues.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
    Private Sub optValues_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optValues.CheckedChanged
        If eventSender.Checked Then
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            cmb_Country.Enabled = True
            Cmb_Year.Enabled = True
            cboFiles.Enabled = True
            'Change table to display
            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            'UPGRADE_ISSUE: Data property DataSAM.RecordSource was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
            'JRS 12/06/03 DataSAM.RecordSource = "SAMValues"
            'JRS 12/06/03 DataSAM.Refresh()
            'JRSVBNET Support.DataGridManager.manager.SetGridDataSource(dbgSAM, DsSAMValues1)
            dbgSAM.DataMember = "SAMValues"
            DsSAMValues1.Clear()
            'jrs 2023/08/23
            rdSAMValues = DbSelectCommand4.ExecuteReader
            Dim myTbl2 As DataTable = New DataTable
            DsSAMValues1.EnforceConstraints = False
            DsSAMValues1.Tables.Add(myTbl2)
            DsSAMValues1.Tables(0).Load(rdSAMValues)

            'If Not OleDbDASAMValues.SelectCommand Is Nothing Then
            '    OleDbDASAMValues.Fill(DsSAMValues1)
            'End If
            'jrs 11/08/04
            dbgSAM.DataBind()
            'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub


    'JRS 04/29/03
    Private Sub DisplayFixedDigits()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Update grid with currency convertion
        Dim iRow As Short
        Dim icol As Short
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        With grdSAMDet
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            For iRow = 1 To .TotalRows - 1
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetRow(frm_SAMDet.DefInstance.grdSAMDet, iRow)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                For icol = 1 To .Columns.Count - 1
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, icol)
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    Select Case Val(grdSAMDet(icol, iRow))
                        Case Is > 1000
                            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                            grdSAMDet(icol, iRow) = System.Math.Round(Val(grdSAMDet(icol, iRow)), 0)
                        Case Is > 100
                            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                            grdSAMDet(icol, iRow) = System.Math.Round(Val(grdSAMDet(icol, iRow)), 1)
                        Case Is > 10
                            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                            grdSAMDet(icol, iRow) = System.Math.Round(Val(grdSAMDet(icol, iRow)), 2)
                        Case Is > 1
                            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                            grdSAMDet(icol, iRow) = System.Math.Round(Val(grdSAMDet(icol, iRow)), 3)
                        Case Is > 0
                            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                            grdSAMDet(icol, iRow) = System.Math.Round(Val(grdSAMDet(icol, iRow)), 4)
                    End Select
                Next icol
            Next iRow
        End With

    End Sub

    '=============================SAM.Frm  ===========================
    'Subroutine to Return value
    Private Sub GetValue(ByRef reg As Short, ByRef yr As Short, ByRef sVar As String, ByRef CompVarVal As Single)
        Dim VarVals(1) As Single
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sVar, -1, cmb_Country.SelectedValue, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, False, False, Session("SAMGroups"),
        1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + sVar + CStr(cboFiles.SelectedIndex)))
        Else
            gLoadForDisplay(sVar, -1, cmb_Country.SelectedValue, reg, "", -1, "", -1, "", cboFiles.SelectedIndex, True, True, Session("SAMGroups"),
            1, VarVals, 0, 1, yr, yr, yr - Session("FirstYear"), True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
        CompVarVal = VarVals(1)
    End Sub

    ' Function to Load Values into Session("PLOT") vector, and then display them using frm_TableDisplay
    ' parameters:
    ' 1. name of the variable.
    ' 2. factor to multiply the variable values.
    ' 3. Boolean to indicate we're starting from column 0 (true), or we're appending more
    '    columns (false).
    Public Sub LoadVarAllYrs(ByRef sName As String, ByRef Factor As Single, ByRef newCols As Boolean)
        Dim RegInd As Short
        Dim i As Object
        'JRS 9/11/02
        Dim idx As Short
        Dim idim2 As Short
        Dim idim3 As Short
        Dim sAggregation As String
        Dim dim2size As Short
        Dim dim3Size As Short
        Dim initialCol As Short
        idx = IfsVars.Item(sName).Index
        idim2 = VDim2(idx)
        idim3 = VDim3(idx)
        sAggregation = VAggregation(idx)
        If idim2 > 0 Then
            dim2size = MAXCOM(idim2) - 1
        Else
            dim2size = 1
        End If
        'jrs 2006/05/29
        If VDim3(idx) > 0 Then
            dim3Size = MAXCOM(idim3) - 1
        Else
            dim3Size = 1
        End If

        'Exclude 'ALL'
        Dim j As Object
        Dim k As Short
        'JRS 04/06/04
        RegInd = cmb_Country.SelectedIndex + 1

        If newCols Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            'JRS 2/17/03
            InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))
        End If
        initialCol = Session("COUNTE")
        Session("COUNTE") = Session("COUNTE") + dim2size * dim3Size
        Session("PercentOn") = "False"
        LoadData(RegInd, idim2, idx, sAggregation, Factor, initialCol)


        'JRSVBNET frm_TableDisplay.RefreshTable()
    End Sub

    Public Sub LoadAXAllYrs()
        Dim RegInd As Short
        Dim i As Object
        Dim dim2size As Object

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'JRS 10/08/02
        'Implemented to expand over time in cell Sectors, Sectors    Dim dim2size As Integer
        Dim initialCol As Short
        'UPGRADE_WARNING: Couldn't resolve default property of object dim2size. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        dim2size = NSec * NSec
        'Exclude 'ALL'
        Dim j As Object
        Dim k As Short

        RegInd = cmb_Country.SelectedIndex + 1
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        'JRS 2/17/03
        InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))
        initialCol = Session("COUNTE")
        'UPGRADE_WARNING: Couldn't resolve default property of object dim2size. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("COUNTE") = Session("COUNTE") + dim2size
        Session("PercentOn") = "False"
        LoadDataAX(RegInd, initialCol)

        'jam 2021/09/01
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")
        'frm_TableDisplay.Show() 

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


        vurl = FriendlyUrl.Href("~/frm_TableDisplay")

        Response.Redirect(vurl)


    End Sub

    Public Sub LoadData(ByRef RegionPTR As Short, ByRef dim2 As Short, ByRef idx As Short, ByRef sAggregation As String, ByRef Factor As Single, ByRef initialCol As Short)
        'Region number and Offset from First Year as parameters
        Dim k2 As Integer, SubPtr As Integer, dim2size As Integer
        If dim2 > 0 Then
            dim2size = MAXCOM(dim2) - 1
        Else
            dim2size = 1
        End If
        'jrs 2006/05/17 Dim3
        Dim L2 As Integer, dim3Size As Integer
        If VDim3(idx) > 0 Then
            dim3Size = MAXCOM(VDim3(idx)) - 1
        Else
            dim3Size = 1
        End If

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        Dim sDim2 As String, sDim3 As String
        For k2 = initialCol + 1 To dim2size + initialCol
            For L2 = 1 To dim3Size
                sDim2 = ""
                sDim3 = ""
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(V(idx), idx, cmb_Country.SelectedValue, RegionPTR, sDim2, k2 - initialCol, sDim3, L2, "", cboFiles.SelectedIndex,
                False, False, Session("SAMGroups"), Factor,
                Session("PLOT"), initialCol + L2 + (k2 - initialCol - 1) * dim3Size, 1, Session("FirstYear"), Session("LastYear"),
                0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                Session("ShortSce"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("MyFileforDisplay"), Session("df" + V(idx) + CStr(cboFiles.SelectedIndex)))
                Else
                    gLoadForDisplay(V(idx), idx, cmb_Country.SelectedValue, RegionPTR, sDim2, k2 - initialCol, sDim3, L2, "", cboFiles.SelectedIndex,
                    k2 = initialCol + 1 And L2 = 1, k2 = dim2size + initialCol And L2 = dim3Size, Session("SAMGroups"), Factor,
                    Session("PLOT"), initialCol + L2 + (k2 - initialCol - 1) * dim3Size, 1, Session("FirstYear"), Session("LastYear"),
                    0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                    Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If
                gLoadForDisplaySaveLbls(initialCol + L2 + (k2 - initialCol - 1) * dim3Size, idx, cmb_Country.SelectedValue, sDim2, sDim3, Session("SAMGroups"), cboFiles.SelectedIndex, "",
                Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
            Next L2
        Next k2
    End Sub

    Public Sub LoadDataAX(ByRef RegionPTR As Short, ByRef initialCol As Short)
        Dim Number As Single
        Dim VLN As Object
        Dim finam As String
        Dim VarVal As Double

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 10/08/02
        'Implemented to expand over time in cell Sectors, Sectors

        'JRS 12/01/02 FILES
        If cboFiles.SelectedIndex = 0 Then
            finam = Session("PutFdrvRun") & "Working.run.db"
        ElseIf cboFiles.SelectedIndex = 1 Then
            finam = Session("PutFdrvRun") & "IFSBASE.RUN.db"
        Else
            finam = Session("PutFdrvRun") & Mid(cboFiles.SelectedValue, InStr(cboFiles.SelectedValue, "-") + 2, Len(cboFiles.SelectedValue) - 1 - InStr(cboFiles.SelectedValue, "-")) + ".db"
        End If
        'jrs 2005/02/14
        Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

        'VLN = VarPoint(PosModYear)
        'Session("RECORD") = (VLN) * 4 - 3
        'FileGet(fNum, Number, Session("RECORD"))
        'VarVal = Number
        Dim working As IStorage
        Dim storageSvc As IStorageService = New StorageService(finam)
        working = storageSvc.Open()
        VarVal = working.LastYear

        Dim LastYr As Short
        'UPGRADE_WARNING: Couldn't resolve default property of object VarVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        LastYr = VarVal - Session("FirstYear") + 1

        FileClose(fNum)

        'Read Values from file
        Dim rsAX As DataTable
        Dim sSQL As String
        sSQL = "SELECT * FROM AX WHERE Region =" & Str(RegionPTR) & " AND Year>=" & Str(Session("FirstYear")) & " AND Year <= " & Str(Session("FirstYear") + LastYr - 1)
        'JRS 09/09/03
        Dim Filename As String
        If cboFiles.SelectedIndex = 0 Then
            SetDataTablefromAccess(Session("PutFdrvRun") & "SAMWorking.mdb", sSQL, "", rsAX)
        ElseIf cboFiles.SelectedIndex = 1 Then
            SetDataTablefromAccess(PutDataFileDir & "SAMBase.mdb", sSQL, "", rsAX) 'jrs 2007/01/03 move SAMBase.mdb to DATA
        Else
            Filename = Replace(Session("PutFdrvRun") & "SAM" & Mid(cboFiles.SelectedValue, InStr(cboFiles.SelectedValue, "-") + 2, Len(cboFiles.SelectedValue) - 1 - InStr(cboFiles.SelectedValue, "-")), ".run", ".mdb")
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            If Dir(Filename) > "" Then
                SetDataTablefromAccess(Filename, sSQL, "", rsAX)
            Else
                SetDataTablefromAccess(Session("PutFdrvRun") & "SAMWorking.mdb", sSQL, "", rsAX)

                LoadMessage("There is no SAM File for this option, using working file.", Session("Message"))

                'jam 2021/09/01
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)

            End If
        End If

        Dim k2, SubPtr As Object
        Dim dim2size As Short
        Dim yr As Short
        dim2size = NSec * NSec

        Dim i As Integer
        For i = 0 To rsAX.Rows.Count - 1
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            k2 = initialCol + (Val(rsAX.Rows(i).Item("Column")) + NSec * (Val(rsAX.Rows(i).Item("Row")) - 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VARN")(0, k2) = "AX - " & cmb_Country.SelectedValue
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VARN")(1, k2) = "AX - " & cmb_Country.SelectedValue
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VARN")(2, k2) = "AX Definition"
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VARN")(3, k2) = CStr(0)
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VARN")(6, k2) = CStr(0)
            'JRS 10/15/02 CURRENCY DISPLAY
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("GR1N")(k2) = GRPS(isecVar, (k2 - initialCol - 1) \ NSec + 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            Session("GR2N")(k2) = GRPS(isecVar, (k2 - initialCol - 1) Mod NSec + 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("GR3N")(k2) = "Billion $" 'jrs 2026/03/24 "AX Units"

            'jrs 10/27/04 Flexible Dimension Names for Graphs
            Session("FDNG1")(k2) = Session("GR1N")(k2)
            Session("FDNG2")(k2) = Session("GR2N")(k2)
            Session("FDNG3")(k2) = Session("GR3N")(k2)

            'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(k2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("FinYear")(k2) = LastYr

            yr = Val(rsAX.Rows(i).Item("Year")) - Session("FirstYear") + 1

            If cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups" Then
                'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(yr, k2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("PLOT")(yr, k2) = System.Math.Round(Val(rsAX.Rows(i).Item("Value")), 4)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object k2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(yr, k2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("PLOT")(yr, k2) = Session("PLOT")(yr, k2) + System.Math.Round(Val(rsAX.Rows(i).Item("Value")), 4)
            End If
        Next i

    End Sub

    Private Sub DisplayOverTime()
        'JRS 12/28/02
        'Moved title of frm_tabledisplay just before bshow=true
        'so that load wouldn't happen with no variables selected
        'JRS 09/12/02
        'Subroutine to display variable over time
        Dim bShow As Boolean
        Dim bTotTot As Boolean
        Dim CompVarVal As Single
        Dim period As Short
        'JRS 06/26/03
        Dim lFactor As Single
        'JRS 12/06/03
        Dim icurrRow As Integer
        Dim icurrCol As Integer

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentRowIndex' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentRowIndex.html'.
        'JRSVBNET icurrRow = dbgSAM.CurrentRowIndex
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentCell' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentCell.html'.
        'JRSVBNET icurrCol = dbgSAM.CurrentCell.Column
        'Mouse Pointer to Hour Glass
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Display AX for Sectors, Sectors
        With DsSAMValues1.SAMValues
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            bTotTot = (Session("SAMRow") = "Total" And Session("SAMCol") = "Total")
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadAXAllYrs()
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(NSec, NSec)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(NSec, NSec, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, Sectors"
                bShow = True
            End If

            'Cell Sectors, Household
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'JRS 2/17/03 LoadVarAllYrs "CS", 0.5, True
                'JRS 2/17/03 LoadVarAllYrs "CS", 0.5, False
                LoadVarAllYrs("CSHHU", 1, True)
                LoadVarAllYrs("CSHHS", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(2, NSec)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(NSec, NSec, False)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, Household"
                bShow = True
            End If

            'Cell Sectors, Capital
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Capital" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("INVS", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, NSec)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(NSec, NSec, False)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, Capital"
                bShow = True
            End If

            'Cell Sectors, Government
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("GS", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, NSec)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(NSec, NSec, False)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, Government"
                bShow = True
            End If

            'Cell Sectors, ROW
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("XS", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, NSec)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(NSec, NSec, False)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, ROW"
                bShow = True
            End If

            'Cell Sectors, Environment
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If Session("SAMRow") = "Sectors" And Session("SAMCol") = "Environment" Then
                LoadVarAllYrs("HHINC", 0, True)
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, Environment"
                bShow = True
            End If

            'Cell Household, Sectors
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Sectors" And Not bTotTot Then
                'JRS 2/17/03 Call GetValue(cmb_Country.ListIndex + 1, Val(Cmb_Year.Text), "CDALF", CompVarVal)
                'JRS 2/17/03 LoadVarAllYrs "VADD", (1 - CompVarVal) / 2, True
                'JRS 2/17/03 LoadVarAllYrs "VADD", (1 - CompVarVal) / 2, False
                LoadVarAllYrs("VADDHHU", 1, True)
                LoadVarAllYrs("VADDHHS", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(NSec, NSec)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Household, Sectors"
                bShow = True
            End If

            'Cell Household, Firms
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Firms" And Not bTotTot Then
                LoadVarAllYrs("HHDIVINT", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 1)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Household, Firms"
                bShow = True
            End If

            'Cell Household, Government
            '        If (.Columns(0) = "Household" Or .Columns(0) = "Total") And .Columns(.col).datafield = "Government" And Not bTotTot Then
            '            LoadVarAllYrs "GOVHHTRN", 0.25, True
            '            LoadVarAllYrs "GOVHHTRN", 0.25, False
            '            LoadVarAllYrs "GOVHHTRN", 0.25, False
            '            LoadVarAllYrs "GOVHHTRN", 0.25, False
            '            If .Columns(0) = "Total" Then
            '                UpdateDisplayColTotalsTime 2, 2
            '            End If
            '            frm_TableDisplay.Caption = "Household, Government"
            '            bShow = True
            '        End If

            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Government" And Not bTotTot Then
                LoadVarAllYrs("GOVHHTRNPEN", 1, True)
                LoadVarAllYrs("GOVHHTRNWEL", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 2)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Household, Government"
                bShow = True
            End If

            'Cell Household, ROW
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "ROW" And Not bTotTot Then
                If bWorkRemit Then
                    lFactor = 1
                Else
                    lFactor = 0
                End If
                LoadVarAllYrs("XWORKREMIT", lFactor, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 1)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Household, ROW"
                bShow = True
            End If

            'Cell Household, Total
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If Session("SAMRow") = "Household" And Session("SAMCol") = "Total" Then
                LoadVarAllYrs("HHINC", 1, True)
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Household, Total"
                bShow = True
            End If

            'Cell Firms, Sectors
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Firms" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'JRS 06/26/03 Call GetValue(cmb_Country.ListIndex + 1, Val(Cmb_Year.Text), "CDALF", CompVarVal)
                'JRS 06/26/03 LoadVarAllYrs "VADD", CompVarVal, True
                LoadVarAllYrs("VADDF", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(NSec, NSec)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, NSec, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Firms, Sectors"
                bShow = True
            End If

            'Cell Firms, ROW
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Firms" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("XFDIFIN", 1, True)
                LoadVarAllYrs("XPORTFIN", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 2)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 2, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Firms, ROW"
                bShow = True
            End If

            'Cell ROW, Sectors
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("MS", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(NSec, NSec)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, NSec, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "ROW, Sectors"
                bShow = True
            End If

            'Cell ROW, Firms
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then
                If Not bWorkRemit Then
                    lFactor = -1
                Else
                    lFactor = 0
                End If
                LoadVarAllYrs("XFDIFOUT", 1, True)
                LoadVarAllYrs("XPORTFOUT", 1, False)
                LoadVarAllYrs("XWBSUBF", 1, False)
                LoadVarAllYrs("XWORKREMIT", lFactor, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 3)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 3, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "ROW, Firms"
                bShow = True
            End If

            'JRS 06/19/03 IT HAS 1 COLUMN, IF ITS THE LAST ONE I LOOSE THE HEADERS
            'OF THE TABLE WHEN DOING TOTALS BY COLUMN
            'Cell Capital, Government
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("GOVBAL", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 2)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 2, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Capital, Government"
                bShow = True
            End If

            'Cell ROW, Government
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                period = 2
                LoadVarAllYrs("XWBLNFOUT", 1, True)
                LoadVarAllYrs("XIMFCRFOUT", 1, False)
                If bDonate Then
                    LoadVarAllYrs("AID", -1, False)
                    period = 3
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, period)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, period, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "ROW, Government"
                bShow = True
            End If

            'Cell Environment, Sectors
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If Session("SAMRow") = "Environment" And Session("SAMCol") = "Sectors" Then
                LoadVarAllYrs("HHINC", 0, True)
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Environment, Sectors"
                bShow = True
            End If


            'Cell Capital, Household
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("HHSAV", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(2, 2)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 2, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Capital, Household"
                bShow = True
            End If

            'Cell Capital, Firms
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("FIRMSAV", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 1)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 1, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Capital, Firms"
                bShow = True
            End If


            'Cell Capital, ROW
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("FORSAV", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 1)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 1, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Capital, ROW"
                bShow = True
            End If

            'Cell Government, Household
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("HHTAX", 1, True)
                LoadVarAllYrs("HHGOVSS", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(2, 2)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 2, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Government, Household"
                bShow = True
            End If

            'Cell Government, Firms
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("FIRMTAX", 1, True)
                LoadVarAllYrs("FIRMGOVSS", 1, False)
                LoadVarAllYrs("INDIRECTTAX", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 3)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 3, False)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Sectors, Capital"
                bShow = True
            End If

            'Cell Government, ROW
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                period = 2
                LoadVarAllYrs("XWBLNFIN", 1, True)
                LoadVarAllYrs("XIMFCRFIN", 1, False)
                If Not bDonate Then
                    LoadVarAllYrs("AID", 1, False)
                    period = 3
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, period)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, period, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Government, ROW"
                bShow = True
            End If

            'JRS 10/28/02 SAM TOTALS
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If Session("SAMRow") = "Total" And Session("SAMCol") <> "Total" Then
                DisplayColTotalsTime()
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If Session("SAMCol") = "Total" And Session("SAMRow") <> "Household" And Session("SAMRow") <> "Total" Then
                DisplayRowTotalsTime()
            End If
            'JRS 1/10/03
            If bShow Then
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET frm_TableDisplay.RefreshTable()
                'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                'jam 2021/09/01
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


                vurl = FriendlyUrl.Href("~/frm_TableDisplay")

                Response.Redirect(vurl)

                'frm_TableDisplay.Show() 
                'JRS 04/18/03
            Else

                LoadMessage("No detail available for this cell.", Session("Message"))

                'jam 2021/09/01
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)

            End If
        End With
        'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2049"'
        'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2036"'
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    'JRS 2/4/03 SAM2
    Private Sub DisplayOverTime2()
        'JRS 12/28/02
        'Moved title of frm_tabledisplay just before bshow=true
        'so that load wouldn't happen with no variables selected
        'JRS 09/12/02
        'Subroutine to display variable over time
        Dim bShow As Boolean
        Dim bTotTot As Boolean
        Dim CompVarVal As Single
        'JRS 12/06/03
        Dim icurrRow As Integer
        Dim icurrCol As Integer

        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentRowIndex' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentRowIndex.html'.
        'JRSVBNET icurrRow = dbgSAM.CurrentRowIndex
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentCell' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentCell.html'.
        'JRSVBNET icurrCol = dbgSAM.CurrentCell.Column
        'Mouse Pointer to Hour Glass
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        With DsSAMValues1.SAMValues
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            bTotTot = (Session("SAMRow") = "Total" And Session("SAMCol") = "Total")

            'Cell Firms, ROW
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Firms" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("XFDISTOCK", 1, True)
                LoadVarAllYrs("XPORTFOLIO", 1, False)
                LoadVarAllYrs("XFIRMDEBT", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 1)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 3, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Firms, ROW"
                bShow = True
            End If

            'Cell Government, Household
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("GOVTDEBTD", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 1)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 1, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Government, Household"
                bShow = True
            End If

            'Cell Government, ROW
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("XIMFCREDIT", 1, True)
                LoadVarAllYrs("XWBLOANS", 1, False)
                LoadVarAllYrs("XGOVTDEBTB", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 1)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 3, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Government, ROW"
                bShow = True
            End If

            'Cell ROW, Firms
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then
                LoadVarAllYrs("XFDISTOUT", 1, True)
                LoadVarAllYrs("XPORTSTOUT", 1, False)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 2)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 2, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "ROW, Firms"
                bShow = True
            End If

            'Cell ROW, Government
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'LoadVarAllYrs "XDEBTRPA", 1, True
                LoadVarAllYrs("XRESERVES", 1, True)
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotalsTime(1, 1)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotalsTime(1, 1, True)
                End If
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "ROW, Government"
                bShow = True
            End If

            'JRS 10/28/02 SAM TOTALS
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If Session("SAMRow") = "Total" And Session("SAMCol") <> "Total" Then
                DisplayColTotalsTime()
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(0). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(dbgSAM.col).DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If Session("SAMCol") = "Total" And Session("SAMRow") <> "Household" And Session("SAMRow") <> "Total" Then
                DisplayRowTotalsTime()
            End If
            'JRS 1/10/03
            If bShow Then
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET frm_TableDisplay.RefreshTable()
                'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                'jam 2021/09/01
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


                vurl = FriendlyUrl.Href("~/frm_TableDisplay")

                Response.Redirect(vurl)

                'frm_TableDisplay.Show() 
                'JRS 04/18/03
            Else

                LoadMessage("No detail available for this cell.", Session("Message"))
                'jam 2021/09/01
                'Support.FormManager.StateStore(Me)
                'Support.FormManager.Show(Me, "frm_Message.aspx")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                vurl = FriendlyUrl.Href("~/frm_Message")

                Response.Redirect(vurl)


            End If
        End With
        'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2049"'
        'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2036"'
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    'Function to update total values for display in Expand
    Private Sub UpdateDisplayColTotals(ByRef NumberOfCols As Short, Optional ByVal bDontReset As Boolean = False)
        Dim cols As Short
        Dim rows As Short
        Dim i As Short
        Dim j As Short
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        With grdSAMDet
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            cols = .Columns.Count
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            rows = .TotalRows
            'JRS 06/25/03 If Not bcolTot Or (cols - 2) > UBound(colTotal) Then
            If Not bcolTot Then
                ReDim Preserve colTotal(NumberOfCols - 1)
                ReDim Preserve acolLabels(NumberOfCols - 1)
            End If
            For i = 1 To cols - 1
                'JRS 06/25/03
                If (i - 1) <= UBound(acolLabels) Then
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    acolLabels(i - 1) = grdSAMDet(i, 0)
                End If
                For j = 1 To rows - 1
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET Support.DataGridManager.manager.SetRow(frm_SAMDet.DefInstance.grdSAMDet, j)
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    colTotal((i - 1) Mod NumberOfCols) = colTotal((i - 1) Mod NumberOfCols) + Val(grdSAMDet(i, j))
                Next j
            Next i
            If Not bDontReset Then
                .TotalRows = 1
            End If
            bcolTot = True
        End With

    End Sub

    'Function to Display total value for column
    Private Sub DisplayColTotals()
        Dim i As Short
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        With grdSAMDet
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.Rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            .TotalRows = 2
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.Cols is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            .Columns.Clear()
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET Support.DataGridManager.manager.SetRow(frm_SAMDet.DefInstance.grdSAMDet, 1)
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, 0)
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            .Columns.Add(New TMSWebControls.Column)
            grdSAMDet(0, 1) = "Totals"
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            For i = 1 To UBound(acolLabels) + 1
                .Columns.Add(New TMSWebControls.Column)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetRow(frm_SAMDet.DefInstance.grdSAMDet, 0)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, i)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                grdSAMDet(i, 0) = acolLabels(i - 1)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetRow(frm_SAMDet.DefInstance.grdSAMDet, 1)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                grdSAMDet(i, 1) = CStr(System.Math.Round(colTotal(i - 1), 4))
                colTotal(i - 1) = 0
            Next i
            bcolTot = False
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Totals" & Mid(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, InStr(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ","))
        End With

    End Sub

    'Function to update total values for display in Expand
    Private Sub UpdateDisplayRowTotals(ByRef NumberOfRows As Short)
        Dim cols As Short
        Dim rows As Short
        Dim i As Short
        Dim j As Short
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        With grdSAMDet
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            cols = .Columns.Count
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            rows = .TotalRows
            'JRS 06/25/03 If Not browTot Or (rows - 2) > UBound(rowTotal) Then
            If Not browTot Then
                ReDim Preserve rowTotal(NumberOfRows - 1)
                ReDim Preserve arowLabels(NumberOfRows - 1)
            End If


            For i = 1 To rows - 1
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetRow(frm_SAMDet.DefInstance.grdSAMDet, i)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, 0)
                'JRS 06/25/03
                If i - 1 <= UBound(arowLabels) Then
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    arowLabels(i - 1) = grdSAMDet(0, i)
                End If
                For j = 1 To cols - 1
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, j)
                    'JRS 06/25/03 rowTotal(i - 1) = rowTotal(i - 1) + Val(.Text)
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    rowTotal((i - 1) Mod NumberOfRows) = rowTotal((i - 1) Mod NumberOfRows) + Val(grdSAMDet(j, i))
                Next j
            Next i
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.Rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            .TotalRows = 1
            browTot = True
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Mid(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, 1, InStr(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ",")) & "Totals" & Mid(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, InStr(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ":"))
        End With

    End Sub

    'Function to Display total value for column
    Private Sub DisplayRowTotals()
        Dim i As Short
        'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        With grdSAMDet
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.Rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            .TotalRows = UBound(rowTotal) + 2

            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.Cols is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            .Columns.Clear()
            .Columns.Add(New TMSWebControls.Column)
            .Columns.Add(New TMSWebControls.Column)
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET Support.DataGridManager.manager.SetRow(frm_SAMDet.DefInstance.grdSAMDet, 0)
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, 1)
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            grdSAMDet(1, 0) = "Totals"
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            For i = 1 To .TotalRows - 1
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, 0)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetRow(frm_SAMDet.DefInstance.grdSAMDet, i)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                grdSAMDet(0, i) = arowLabels(i - 1)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, 1)
                'CONVERSION_ISSUE: Property 'IFsGUI.frm_SAMDet.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET Support.DataGridManager.SetText(frm_SAMDet.DefInstance.grdSAMDet, CStr(System.Math.Round(rowTotal(i - 1), 4)))
                grdSAMDet(1, i) = CStr(System.Math.Round(rowTotal(i - 1), 4))
                rowTotal(i - 1) = 0
            Next i
            browTot = False
        End With

    End Sub

    'Function to update total values for display in Show Over Time
    Private Sub UpdateDisplayColTotalsTime(ByRef totalCols As Short, ByRef period As Short)
        'parameter totalCols: represents the total number of columns to store and accumulate
        'parameter period: represent how often in the Plot vector will find the same variables
        Dim cols As Short
        Dim rows As Short
        cols = Session("COUNTE")
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(Session("COUNTE")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        rows = Session("FinYear")(Session("COUNTE"))
        If Not bcolTot Then
            ReDim colTotalTime(rows - 1, totalCols - 1)
            'ReDim Preserve acolLabels(totalCols - 1) As String
        End If
        Dim i As Short
        Dim j As Short
        For i = 1 To rows
            'acolLabels(i - 1) = .Text
            For j = 1 To cols
                If period > totalCols Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    colTotalTime(i - 1, ((j - 1) \ period) Mod totalCols) = colTotalTime(i - 1, ((j - 1) \ period) Mod totalCols) + Session("PLOT")(i, j)
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    colTotalTime(i - 1, ((j - 1) Mod period) Mod totalCols) = colTotalTime(i - 1, ((j - 1) Mod period) Mod totalCols) + Session("PLOT")(i, j)
                End If
            Next j
        Next i
        bcolTot = True
    End Sub

    'Function to Display total value for column in show over time
    Private Sub DisplayColTotalsTime()
        Dim i As Short
        Dim j As Short

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET Support.FormManager.Close(frm_TableDisplay)
        If (Session("COUNTE") <> UBound(colTotalTime, 2) + 1) Then
            Session("COUNTE") = UBound(colTotalTime, 2) + 1
        End If

        For j = 1 To UBound(colTotalTime, 2) + 1
            Session("VARN")(0, j) = "Total"
            Session("VARN")(1, j) = "Total"
            Session("VARN")(2, j) = "Total"
            Session("VARN")(3, j) = CStr(0)
            Session("VARN")(6, j) = CStr(0)
            For i = 1 To UBound(colTotalTime, 1) + 1
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("PLOT")(i, j) = System.Math.Round(colTotalTime(i - 1, j - 1), 4)
            Next i
        Next j
        bcolTot = False
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        'jam 2021/09/01
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

        'jam 2021/09/01
        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


        vurl = FriendlyUrl.Href("~/frm_TableDisplay")

        Response.Redirect(vurl)

        'frm_TableDisplay.Show() 
    End Sub

    'Function to update total values for display in Show Over Time
    Private Sub UpdateDisplayRowTotalsTime(ByRef totalRows As Short, ByRef period As Short, ByRef bContinuous As Boolean)
        'parameter totalRows: represents the total number of rows to store and accumulate
        'parameter period: represent how often in the Plot vector will find the same variables
        'parameter bContinuous: is true when all values for the same row are together
        Dim cols As Short
        Dim rows As Short
        cols = Session("COUNTE")
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(Session("COUNTE")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        rows = Session("FinYear")(Session("COUNTE"))
        If Not browTot Then
            ReDim rowTotalTime(rows - 1, totalRows - 1)
            'ReDim Preserve acolLabels(totalCols - 1) As String
        ElseIf UBound(rowTotalTime, 2) < totalRows - 1 Then
            ReDim Preserve rowTotalTime(rows - 1, totalRows - 1)
        End If
        Dim i As Short
        Dim j As Short
        For i = 1 To rows
            'acolLabels(i - 1) = .Text
            For j = 1 To cols
                If bContinuous Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    rowTotalTime(i - 1, ((j - 1) \ period) Mod totalRows) = rowTotalTime(i - 1, ((j - 1) \ period) Mod totalRows) + Session("PLOT")(i, j)
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    rowTotalTime(i - 1, ((j - 1) Mod period) Mod totalRows) = rowTotalTime(i - 1, ((j - 1) Mod period) Mod totalRows) + Session("PLOT")(i, j)
                End If
            Next j
        Next i
        browTot = True
    End Sub

    'Function to Display total value for row in show over time
    Private Sub DisplayRowTotalsTime()
        Dim i As Short
        Dim j As Short

        'jam 2021/09/01
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        'CONVERSION_ISSUE: Property 'IFsGUI.frm_TableDisplay' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET Support.FormManager.Close(frm_TableDisplay)
        If (Session("COUNTE") <> UBound(rowTotalTime, 2) + 1) Then
            Session("COUNTE") = UBound(rowTotalTime, 2) + 1
        End If

        For j = 1 To UBound(rowTotalTime, 2) + 1
            Session("VARN")(0, j) = "Total"
            Session("VARN")(1, j) = "Total"
            Session("VARN")(2, j) = "Total"
            Session("VARN")(3, j) = CStr(0)
            Session("VARN")(6, j) = CStr(0)
            For i = 1 To UBound(rowTotalTime, 1) + 1
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i, j). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("PLOT")(i, j) = System.Math.Round(rowTotalTime(i - 1, j - 1), 4)
            Next i
        Next j
        browTot = False
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.

        'jam 2021/09/01
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_TableDisplay.aspx")

        'jam 2021/09/01
        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_TableDisplay.aspx")


        vurl = FriendlyUrl.Href("~/frm_TableDisplay")


        'frm_TableDisplay.Show() 
    End Sub

    'JRS 12/01/02 FILES
    Private Sub LoadFiles(ByRef FileList() As String, ByRef cbo As System.Web.UI.WebControls.DropDownList)
        Dim i As Short
        For i = 0 To UBound(FileList)
            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cbo.Items.Add(New ListItem(FileList(i)))
        Next i
    End Sub

    'JRS 1/09/03
    Private Sub TransposeTable(Optional ByRef grd As Object = Nothing)
        Dim col As Short
        Dim row As Short
        Dim lbl As String
        Dim lbl2 As String
        Dim i As Short
        Dim j As Short

        Dim grd1 As TMSWebControls.AdvWebGrid
        If grd Is Nothing Then
            grd1 = grdSAMDet
        Else
            grd1 = grd
        End If

        With grd1
            col = .Columns.Count
            row = .TotalRows
            'if there are at least as many columns as there are rows
            If col >= row Then
                .TotalRows = col
                'JRS 2/17/03 For j = 0 To col - 1
                For i = 0 To row - 1
                    'JRS 2/17/03 .col = j
                    'JRS 2/17/03 For i = j + 1 To row - 1
                    For j = i + 1 To col - 1
                        'JRS 2/17/03 .row = i
                        'transpose data
                        lbl = .Item(j, i)
                        lbl2 = .Item(i, j)
                        .Item(i, j) = lbl
                        .Item(j, i) = lbl2
                        'JRS 2/17/03 Next i
                    Next j
                    'JRS 2/17/03 Next j
                Next i
                For i = col - 1 To row Step -1
                    .Columns.RemoveAt(i)
                Next i
                'if there are more rows than cols
            Else
                For i = col To row - 1
                    Dim myColRH As New TMSWebControls.Column
                    myColRH.WidthType = TMSWebControls.WType.Absolute
                    myColRH.Width = 100
                    .Columns.Add(myColRH)
                Next i
                'JRS 2/17/03 For i = 0 To row - 1
                For j = 0 To col - 1
                    'JRS 2/17/03 .row = i
                    'JRSVBNET Support.DataGridManager.manager.SetCol(frm_SAMDet.DefInstance.grdSAMDet, j)
                    'JRS 2/17/03 For j = i + 1 To col - 1
                    For i = j + 1 To row - 1
                        'JRS 2/17/03 .col = j
                        'transpose data
                        lbl = .Item(j, i)
                        lbl2 = .Item(i, j)
                        'JRSVBNET Support.DataGridManager.SetText(frm_SAMDet.DefInstance.grdSAMDet, lbl)
                        .Item(i, j) = lbl
                        .Item(j, i) = lbl2
                        'JRS 2/17/03 Next j
                    Next i
                    'JRS 2/17/03 Next i
                Next j
                .TotalRows = col
            End If


        End With
        'Change title
        Dim sTitle() As String
        Dim newTitle As String
        sTitle = Split(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text)
        newTitle = sTitle(1) & " "
        newTitle = Replace(newTitle, ":", ",")
        newTitle = newTitle & Replace(sTitle(0), ",", ":")
        For i = 2 To UBound(sTitle)
            newTitle = newTitle & " " & sTitle(i)
        Next i
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = newTitle

    End Sub

    'Function to round values
    'JRS 04/21/03
    Private Sub RoundValues()
        Dim i As Integer
        Dim j As Integer

        'JRS 2004/11/26
        'Dim sUpd1 As String
        'Dim rs As DataTable
        'SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.MDB", "SELECT * FROM SAMValues", "", rs)

        'Dim s As String
        'j = 0
        's = "SAMValues"
        'Do While True
        '    Do While Not j = rs.Rows.Count
        '        For i = 1 To rs.Columns.Count - 1
        '            sUpd1 = "UPDATE " & s & " SET " & rs.Columns(i).Caption
        '            Select Case System.Math.Abs(rs.Rows(j).Item(i))
        '                Case Is > 1000
        '                    sUpd1 = sUpd1 & " = " & Str(System.Math.Round(rs.Rows(j).Item(i), 0))
        '                Case Is > 100
        '                    sUpd1 = sUpd1 & " = " & Str(System.Math.Round(rs.Rows(j).Item(i), 1))
        '                Case Is > 10
        '                    sUpd1 = sUpd1 & " = " & Str(System.Math.Round(rs.Rows(j).Item(i), 2))
        '                Case Is > 1
        '                    sUpd1 = sUpd1 & " = " & Str(System.Math.Round(rs.Rows(j).Item(i), 3))
        '                Case Is >= 0
        '                    sUpd1 = sUpd1 & " = " & Str(System.Math.Round(rs.Rows(j).Item(i), 4))
        '            End Select
        '            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
        '            If Not IsDBNull(rs.Rows(j).Item(i)) Then
        '                sUpd1 = sUpd1 & " WHERE Categories = '" + rs.Rows(j).Item(0) + "'"
        '                'updateAccessWith1Command(Session("PutFdrvRun") & "IFs.MDB", sUpd1)
        '                ExecuteSql(samConn, sUpd1)
        '            End If
        '        Next i
        '        j = j + 1
        '    Loop
        '    If s = "SAMValues" Then
        '        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.MDB", "SELECT * FROM SAMBasePct", "Variable", rs)
        '        s = "SAMBasePct"
        '    Else
        '        Exit Do
        '    End If
        'Loop

        With DsSAMValues1.Tables(0)
            For i = 0 To .Rows.Count - 1
                For j = 1 To .Columns.Count - 1
                    If Math.Abs(.Rows(i).Item(j)) >= 1000 Then
                        .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 0)
                    ElseIf Math.Abs(.Rows(i).Item(j)) >= 100 Then
                        .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 1)
                    ElseIf Math.Abs(.Rows(i).Item(j)) >= 10 Then
                        .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 2)
                    ElseIf Math.Abs(.Rows(i).Item(j)) >= 1 Then
                        .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 3)
                    ElseIf Math.Abs(.Rows(i).Item(j)) < 1 Then
                        .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 4)
                    End If
                Next
            Next
        End With
        With DsSAMBasePct1.Tables(0)
            For i = 0 To .Rows.Count - 1
                For j = 1 To .Columns.Count - 1
                    If Not IsDBNull(.Rows(i).Item(j)) Then
                        If Math.Abs(.Rows(i).Item(j)) >= 1000 Then
                            .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 0)
                        ElseIf Math.Abs(.Rows(i).Item(j)) >= 100 Then
                            .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 1)
                        ElseIf Math.Abs(.Rows(i).Item(j)) >= 10 Then
                            .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 2)
                        ElseIf Math.Abs(.Rows(i).Item(j)) >= 1 Then
                            .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 3)
                        ElseIf Math.Abs(.Rows(i).Item(j)) < 1 Then
                            .Rows(i).Item(j) = Math.Round(.Rows(i).Item(j), 4)
                        End If
                    End If
                Next
            Next
        End With
    End Sub




    Private Sub mnuExplain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'JRS 12/28/02
        Dim rsLbl As DataTable
        Dim S As String
        'JRS 12/06/03
        Dim icurrRow As Integer
        Dim icurrCol As Integer
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentRowIndex' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentRowIndex.html'.
        icurrRow = 0 'JRSVBNET dbgSAM.CurrentRowIndex
        'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.CurrentCell' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.CurrentCell.html'.
        icurrCol = 0 'JRSVBNET dbgSAM.CurrentCell.Column

        With DsSAMValues1.SAMValues
            'JRS 2/4/03
            If bSAM2 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'jrs 2023/08/01 weird, = "Sectors" not working, had to change to LIKE "Sectors%"
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", "SELECT * FROM SAMLabels WHERE Categories LIKE """ & Session("SAMRow") & "%""", "", rsLbl)
            Else
                'jrs 2023/08/01 weird, = "Sectors" not working, had to change to LIKE "Sectors%"
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", "SELECT * FROM SAMLabels2 WHERE Categories lIKE """ & Session("SAMRow") & "%""", "", rsLbl)
            End If
            'JRS 04/16/03
            'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
            If (IsDBNull(rsLbl.Rows(0).Item(Session("SAMCol")))) Then
                S = "No label on file for this cell"
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object dbgSAM.Columns().DataField. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridColumnStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Class System.Windows.Forms.DataGridTableStyle from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
                'CONVERSION_ISSUE: Property 'System.Windows.Forms.DataGrid.TableStyles' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.DataGrid.html'.
                S = rsLbl.Rows(0).Item(Session("SAMCol"))
            End If
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRSVBNET WinFormsToWeb_MessageBox.Show(S, MsgBoxStyle.OKOnly, "Explain cell content")
            lbtExplain.Attributes.Add("onclick", "alert('" & S & "');")
        End With

    End Sub

    Private Sub mnuPopUpCellExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        grdSAMDet.Clear()
        grdSAMDet.TotalRows = 1
        'JRS 2/4/03 SAM2
        If bSAM2 Then
            'JRS 09/11/02
            ExpandCell()
        Else
            ExpandCell2()
        End If
    End Sub

    Private Sub mnuPopUpCellOverTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'JRS 2/4/03
        If bSAM2 Then
            'JRS 09/12/02
            DisplayOverTime()
        Else
            DisplayOverTime2()
        End If
    End Sub

    Private Sub dbgSAM_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dbgSAM.PageIndexChanged
        'JRSVBNET Support.DataGridManager.manager.RefreshData(dbgSAM)
        dbgSAM.CurrentPageIndex = e.NewPageIndex
        dbgSAM.DataBind()
    End Sub

    'JRS 03/29/04
    Private Sub RefreshDisplay()
        'jrs 2004/11/26
        'If Session("SAMBasePct") Then
        '    DsSAMBasePct1.Clear()
        '    OleDbDASAMBasePct.Fill(DsSAMBasePct1)
        'Else
        '    DsSAMValues1.Clear()
        '    OleDbDASAMValues.Fill(DsSAMValues1)
        'End If

        dbgSAM.DataBind()

    End Sub

    'jrs 2004/11/26
    Private Sub LoadDs()
        DsSAMBasePct1.Clear()
        'jrs 2023/08/23
        rdSAMBasePct = DbSelectCommand1.ExecuteReader
        Dim myTbl As DataTable = New DataTable
        DsSAMBasePct1.EnforceConstraints = False
        DsSAMBasePct1.Tables.Add(myTbl)
        DsSAMBasePct1.Tables(0).Load(rdSAMBasePct)

        'OleDbDASAMBasePct.Fill(DsSAMBasePct1)
        Session("DsSAMBasePct1") = DsSAMBasePct1

        DsSAMValues1.Clear()
        'jrs 2023/08/23
        rdSAMValues = DbSelectCommand4.ExecuteReader
        Dim myTbl2 As DataTable = New DataTable
        DsSAMValues1.EnforceConstraints = False
        DsSAMValues1.Tables.Add(myTbl2)
        DsSAMValues1.Tables(0).Load(rdSAMValues)

        'OleDbDASAMValues.Fill(DsSAMValues1)
        Session("DsSAMValues1") = DsSAMValues1
    End Sub
    'JRS 03/29/04
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Use Groups", "Use Country"
                mnuGroups_Click(sender, e)
            Case "Percent Change From Base Case", "Normal"
                mnuBasePct_Click(sender, e)
            Case "Show Stocks", "Show Flows"
                mnuSAM2_Click(sender, e)
            Case "Expand SAM"
                mnuExpandSAM_Click()
            Case "Collapse SAM" 'dsb 2026/07/15 Collapse SAM in switch
                cmdContinueSam_Click(Nothing, EventArgs.Empty)
        End Select
    End Sub

    'JRS 04/05/04
    Private Sub dbgSAM_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dbgSAM.ItemCommand
        Session("SAMRow") = e.Item.Cells(0).Text
        Session("SAMCol") = e.CommandName
        lblRowCol.Text = "Row: " & Session("SAMRow") & ", Column: " & Session("SAMCol")
        pnlContext.Visible = True
        mnuExplain_Click(Me, e)
    End Sub

    'JRS 04/05/04
    Private Sub lbtCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtCancel.Click
        pnlContext.Visible = False
    End Sub

    'JRS 04/05/04
    Private Sub lbtExplain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtExplain.Click
        pnlContext.Visible = False
    End Sub

    'JRS 04/05/04
    Private Sub lbtShowOverTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtShowOverTime.Click
        'jrs 2011/05/06
        Dim sPar As String
        If Session("ReportGroup") = True Then
            sPar = "Group=" & Session("MMGroup")
        Else
            sPar = "Country=" & Session("MMCountry")
        End If
        'JRS 03/24/04
        Session("TableDisplayParent") = "frm_SAM.aspx?" & sPar
        mnuPopUpCellOverTime_Click(sender, e)
        pnlContext.Visible = False
    End Sub

    'JRS 04/06/04
    Private Sub lbtExpandCell_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtExpandCell.Click
        pnlContext.Visible = False
        grdSAMDet.Visible = True
        'jrs 2005/11/14
        cswmWebMenu.Groups("mnuSwitch").ShowMenuBarItem = False
        mnuPopUpCellExpand_Click(sender, e)
    End Sub

    Private Sub cmdContinueSam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinueSam.Click
        cmdContinueSam.Visible = False
        grdSAMDet.Visible = False
        'Frame1.Visible = True
        lbl_countries.Visible = True
        cmb_Country.Visible = True
        Lbl_File.Visible = True
        cboFiles.Visible = True
        'jrs 11/08/04 optLabels.Visible = True
        'jrs 11/08/04 optValues.Visible = True
        Lblyear.Visible = True
        Cmb_Year.Visible = True

        dbgSAM.Visible = True
        'jrs 2025/07/31
        lblUnits.Visible = True
        Label1.Visible = True
        lblInstructions.Visible = True
        Session("SAMExpanded") = False 'dsb 2026/07/15 Change back to Expand SAM in switch
        cswmWebMenu.Groups("mnuSwitch").Items("mnuExpandSAM").Caption = "Expand SAM"
    End Sub

    Private Sub PctCalculation()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'assuming all datasets are sorted in the same way
        Dim i As Integer
        With DsSAMBasePct1.Tables(0)
            For i = 0 To .Rows.Count - 1
                .Rows(i).Item("Sectors") = DsSAMValues1.Tables(0).Rows(i).Item("Sectors") - rsSAMBASEValues.Rows(i).Item("Sectors")
                .Rows(i).Item("Household") = DsSAMValues1.Tables(0).Rows(i).Item("Household") - rsSAMBASEValues.Rows(i).Item("Household")
                .Rows(i).Item("Firms") = DsSAMValues1.Tables(0).Rows(i).Item("Firms") - rsSAMBASEValues.Rows(i).Item("Firms")
                .Rows(i).Item("Capital") = DsSAMValues1.Tables(0).Rows(i).Item("Capital") - rsSAMBASEValues.Rows(i).Item("Capital")
                .Rows(i).Item("Government") = DsSAMValues1.Tables(0).Rows(i).Item("Government") - rsSAMBASEValues.Rows(i).Item("Government")
                .Rows(i).Item("ROW") = DsSAMValues1.Tables(0).Rows(i).Item("ROW") - rsSAMBASEValues.Rows(i).Item("ROW")
                .Rows(i).Item("Environment") = DsSAMValues1.Tables(0).Rows(i).Item("Environment") - rsSAMBASEValues.Rows(i).Item("Environment")
                .Rows(i).Item("Total") = DsSAMValues1.Tables(0).Rows(i).Item("Total") - rsSAMBASEValues.Rows(i).Item("Total")
            Next
        End With

        'jrs 2006/09/31 added * 100
        With DsSAMBasePct1.Tables(0)
            For i = 0 To .Rows.Count - 1

                If rsSAMBASEValues.Rows(i).Item("Sectors") <> 0 Then
                    .Rows(i).Item("Sectors") = .Rows(i).Item("Sectors") / rsSAMBASEValues.Rows(i).Item("Sectors") * 100
                Else
                    .Rows(i).Item("Sectors") = System.DBNull.Value
                End If
                If rsSAMBASEValues.Rows(i).Item("Household") <> 0 Then
                    .Rows(i).Item("Household") = .Rows(i).Item("Household") / rsSAMBASEValues.Rows(i).Item("Household") * 100
                Else
                    .Rows(i).Item("Household") = System.DBNull.Value
                End If
                If rsSAMBASEValues.Rows(i).Item("Firms") <> 0 Then
                    .Rows(i).Item("Firms") = .Rows(i).Item("Firms") / rsSAMBASEValues.Rows(i).Item("Firms") * 100
                Else
                    .Rows(i).Item("Firms") = System.DBNull.Value
                End If
                If rsSAMBASEValues.Rows(i).Item("Capital") <> 0 Then
                    .Rows(i).Item("Capital") = .Rows(i).Item("Capital") / rsSAMBASEValues.Rows(i).Item("Capital") * 100
                Else
                    .Rows(i).Item("Capital") = System.DBNull.Value
                End If
                If rsSAMBASEValues.Rows(i).Item("Government") <> 0 Then
                    .Rows(i).Item("Government") = .Rows(i).Item("Government") / rsSAMBASEValues.Rows(i).Item("Government") * 100
                Else
                    .Rows(i).Item("Government") = System.DBNull.Value
                End If
                If rsSAMBASEValues.Rows(i).Item("ROW") <> 0 Then
                    .Rows(i).Item("ROW") = .Rows(i).Item("ROW") / rsSAMBASEValues.Rows(i).Item("ROW") * 100
                Else
                    .Rows(i).Item("ROW") = System.DBNull.Value
                End If
                If rsSAMBASEValues.Rows(i).Item("Total") <> 0 Then
                    .Rows(i).Item("Total") = .Rows(i).Item("Total") / rsSAMBASEValues.Rows(i).Item("Total") * 100
                Else
                    .Rows(i).Item("Total") = System.DBNull.Value
                End If
            Next i
        End With
    End Sub

    'jrs 2006/07/10
    Private Sub mnuExpandSAM_Click()
        'mti May 2006 - new procedure and menu
        Dim r%, C%
        grdSAMDet.Clear()
        grdSAMDet.TotalRows = 1

        SAMDetailHeader()

        For r = 0 To 6
            If DsSAMValues1.SAMValues.Columns(r + 1).Caption = "Environment" Then
                r = r + 1
                If r > 6 Then Exit For
            End If
            Session("SAMRow") = DsSAMValues1.SAMValues.Columns(r + 1).Caption
            For C = 1 To 7
                If DsSAMValues1.SAMValues.Columns(C).Caption = "Environment" Then
                    C = C + 1
                    If C > 7 Then Exit For
                End If
                Session("SAMCol") = DsSAMValues1.SAMValues.Columns(C).Caption
                grdSAMDetPrn.Clear()
                ExpandCellFullSAM()
            Next C
        Next r

        TotalMatrix(grdSAMDet)
        CollapseTotal(grdSAMDet)
        'jrs 2006/07/07 Sectors, Firms = Sectors Total Column - Sectors Total Row
        Dim i As Integer, myVal As Double
        With grdSAMDet
            'for each sector
            For i = 1 To NSec
                'read col value
                myVal = Val(grdSAMDet(i + 1, .TotalRows - 2))
                myVal = myVal - Val(.Item(.Columns.Count - 2, i + 1))
                'reset total value for sectors
                .Item(.Columns.Count - 2, i + 1) = Val(.Item(.Columns.Count - 2, i + 1)) + Math.Round(myVal, 4)
                .Item(NSec + 4, i + 1) = Math.Round(myVal, 4)
            Next i
            'reset subtotal values
            'for rows
            .Item(.Columns.Count - 1, NSec + 1) = Val(.Item(.Columns.Count - 1, NSec + 1)) + Session("SectorsFirmsVal")
            'for columns
            .Item(NSec + 4, .TotalRows - 2) = Val(.Item(NSec + 4, .TotalRows - 2)) + Session("SectorsFirmsVal")

            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Social Accounting Matrix: " + cmb_Country.SelectedValue + ", " +
                    Cmb_Year.SelectedValue

            dbgSAM.Visible = False
            lbl_countries.Visible = False
            cmb_Country.Visible = False
            Lbl_File.Visible = False
            cboFiles.Visible = False
            optLabels.Visible = False
            optValues.Visible = False
            Lblyear.Visible = False
            Cmb_Year.Visible = False
            'jrs 2025/07/31
            lblUnits.Visible = False
            Label1.Visible = False
            lblInstructions.Visible = False

            .Visible = True
            cmdContinueSam.Visible = True
            Session("SAMExpanded") = True 'dsb 2026/07/15 Collapse SAM in switch
            cswmWebMenu.Groups("mnuSwitch").Items("mnuExpandSAM").Caption = "Collapse SAM"
        End With

    End Sub

    'jrs 2006/07/10
    Private Sub SAMDetailHeader()
        'mti May 2006 - new procedure

        'Initializing constants 09/03/02
        itypeVar = VDim2(IfsVars.Item("HHDIVINT").Index)
        isecVar = VDim2(IfsVars.Item("VADD").Index)

        iType = MaxVal(itypeVar) - 1
        ReDim aSectors(NSec - 1)
        ReDim aTypes(iType - 1)
        Dim iDim As Integer
        For iDim = 1 To NSec
            aSectors(iDim - 1) = GRPS$(isecVar, iDim)
        Next iDim
        For iDim = 1 To iType
            aTypes(iDim - 1) = GRPS$(itypeVar, iDim)
        Next iDim
        With grdSAMDet
            '.MergeCells = flexMergeFree
            .TotalRows = 50
            .RowCount = 50
            .Columns.Clear()
            Dim i As Integer
            For i = 1 To 50
                Dim myColRH As New TMSWebControls.Column
                myColRH.WidthType = TMSWebControls.WType.Absolute
                myColRH.Width = 100
                If i < 3 Then
                    myColRH.Color = System.Drawing.Color.FromArgb(187, 192, 196)
                End If
                .Columns.Add(myColRH)
            Next i

            '.FixedRows = 2
            '.FixedCols = 2
            '.ColWidth(0) = 1000
            '.ColWidth(1) = 1100
            '.RowHeight(1) = 420
            '.WordWrap = True

            'sectors: row labels
            Dim col As Integer, row As Integer, r As Integer, lastrow As Integer, C As Integer,
            lastcol As Integer, subrow As Integer, subcol As Integer, pos1 As Integer, pos2 As Integer
            col = 0
            row = 1
            For r = 1 To NSec
                row = row + 1
                If r = 1 Then
                    grdSAMDet(col, row) = "Sectors"            'starts at 0
                Else
                    '.CellBackColor = QBColor(15)
                End If

            Next r

            col = 1
            row = 1
            For r = 1 To NSec
                row = row + 1
                grdSAMDet(col, row) = aSectors(r - 1) 'starts at 0
            Next r

            lastrow = row

            'sectors: column labels

            row = 0
            col = 1
            For C = 1 To NSec
                col = col + 1
                If C = 1 Then
                    grdSAMDet(col, row) = "Sectors" 'starts at 0
                Else
                    '.CellBackColor = QBColor(15)
                End If
            Next C

            row = 1
            col = 1
            For C = 1 To NSec
                col = col + 1
                grdSAMDet(col, row) = aSectors(C - 1) 'starts at 0
            Next C

            lastcol = col

            '-------------------------------------------------

            'households : row labels

            col = 0
            row = lastrow
            For r = 1 To iType
                row = row + 1

                If r = 1 Then
                    grdSAMDet(col, row) = "Households"
                Else
                    '.CellBackColor = QBColor(15)
                End If

            Next r

            col = 1
            row = lastrow
            For r = 1 To iType
                row = row + 1
                grdSAMDet(col, row) = aTypes(r - 1) 'starts at 0
            Next r

            lastrow = row

            'Households: column labels

            row = 0
            col = lastcol
            For C = 1 To iType
                col = col + 1
                If C = 1 Then
                    grdSAMDet(col, row) = "Households"
                Else
                    '.CellBackColor = QBColor(15)
                End If
            Next C

            row = 1
            col = lastcol
            For C = 1 To iType
                col = col + 1
                grdSAMDet(col, row) = aTypes(C - 1) 'starts at 0
            Next C

            lastcol = col

            '----------Firms

            'Firms : row labels

            subrow = 0
            col = 1
            row = lastrow


            row = row + 1
            grdSAMDet(col, row) = "FDI Inflow" 'to firms
            subrow = subrow + 1

            row = row + 1
            grdSAMDet(col, row) = "Equity Inflow" 'to firms
            subrow = subrow + 1


            row = row + 1
            grdSAMDet(col, row) = "Value Added" 'to firms
            subrow = subrow + 1


            '    row = row + 1
            '    grdsamdet(col, row) = "Firms" 'from sectors (value added), from row fdi and portfolio
            '    subrow = subrow + 1
            '
            'leftmost
            col = 0
            row = lastrow
            For r = 1 To subrow
                row = row + 1
                If r = 1 Then
                    grdSAMDet(col, row) = "Firms"            'starts at 0
                Else
                    '.CellBackColor = QBColor(15)
                End If
            Next r


            lastrow = row

            'Firms: column labels


            subcol = 0
            row = 1
            col = lastcol

            col = col + 1
            grdSAMDet(col, row) = "Firms" '"Div/Int" 'to hh (wages come from sectors?)
            subcol = subcol + 1

            '    col = col + 1
            '    grdsamdet(col, row) = "Firm Savings" 'to capital change
            '    subcol = subcol + 1
            '
            '    col = col + 1
            '    grdsamdet(col, row) = "Firm Taxes" 'to govt change
            '    subcol = subcol + 1


            col = col + 1
            grdSAMDet(col, row) = "FDI Outflow" 'to ROW
            subcol = subcol + 1

            col = col + 1
            grdSAMDet(col, row) = "Equity Outflow" 'to ROW
            subcol = subcol + 1

            col = col + 1
            grdSAMDet(col, row) = "WB Subscriptions" 'to ROW
            subcol = subcol + 1


            col = col + 1
            grdSAMDet(col, row) = "Worker Remittances" 'to ROW
            subcol = subcol + 1


            row = 0  'top most row
            col = lastcol
            For C = 1 To subcol 'iType
                col = col + 1
                If C = 1 Then
                    grdSAMDet(col, row) = "Firms"
                Else
                    '.CellBackColor = QBColor(15)
                End If
            Next C


            lastcol = col

            '----------Capital

            'Capital : row labels

            col = 0
            row = lastrow
            'For r = 1 To iType
            row = row + 1
            grdSAMDet(col, row) = "Capital" 'starts at 0
            'Next r

            col = 1
            row = lastrow
            'For r = 1 To 1 'iType
            row = row + 1
            grdSAMDet(col, row) = "Capital" 'starts at 0
            'Next r

            lastrow = row

            'Capital: column labels


            subcol = 0
            row = 1
            col = lastcol

            col = col + 1
            grdSAMDet(col, row) = "Capital formation" 'to sectors : gross capital formation by origin of sector
            subcol = subcol + 1



            row = 0  'top most row
            col = lastcol
            For C = 1 To subcol 'iType
                col = col + 1
                grdSAMDet(col, row) = "Capital"  'starts at 0
            Next C


            lastcol = col

            '----------Government

            'Government : row labels


            subrow = 0
            col = 1
            row = lastrow

            row = row + 1
            grdSAMDet(col, row) = "Income Tx" 'from hh
            subrow = subrow + 1

            row = row + 1
            grdSAMDet(col, row) = "Soc Sec Tx" 'from hh, firms
            subrow = subrow + 1

            row = row + 1
            grdSAMDet(col, row) = "Corp/Bus Tax" 'from firms
            subrow = subrow + 1

            row = row + 1
            grdSAMDet(col, row) = "Indirect Tax" 'from firms
            subrow = subrow + 1

            '    row = row + 1
            '    grdsamdet(col, row) = "Foreign Receipts" 'from row change
            '    subrow = subrow + 1

            row = row + 1
            grdSAMDet(col, row) = "Aid Receipts" 'to govt
            subrow = subrow + 1

            row = row + 1
            grdSAMDet(col, row) = "World Bank Loans" 'to govt
            subrow = subrow + 1

            row = row + 1
            grdSAMDet(col, row) = "IMF Credits" 'to govt
            subrow = subrow + 1


            'leftmost
            col = 0
            row = lastrow
            For r = 1 To subrow
                row = row + 1
                If r = 1 Then
                    grdSAMDet(col, row) = "Government"            'starts at 0
                Else
                    '.CellBackColor = QBColor(15)

                End If


            Next r


            lastrow = row


            'Government: column labels


            subcol = 0
            row = 1
            col = lastcol

            col = col + 1
            grdSAMDet(col, row) = "Govt" '"Gov Cons" 'to sectors
            subcol = subcol + 1

            col = col + 1
            grdSAMDet(col, row) = "Welfare" 'to hh
            subcol = subcol + 1

            col = col + 1
            grdSAMDet(col, row) = "Pension" 'to hh
            subcol = subcol + 1

            '    col = col + 1
            '    grdsamdet(col, row) = "Government Savings" 'to capital change
            '    subcol = subcol + 1

            col = col + 1
            grdSAMDet(col, row) = "Aid Donations" 'to row
            subcol = subcol + 1


            col = col + 1
            grdSAMDet(col, row) = "World Bank Repay" 'to row
            subcol = subcol + 1

            col = col + 1
            grdSAMDet(col, row) = "IMF Repay" 'to row
            subcol = subcol + 1



            row = 0  'top most row
            col = lastcol
            For C = 1 To subcol 'iType
                col = col + 1
                If C = 1 Then
                    grdSAMDet(col, row) = "Government"            'starts at 0
                Else
                    '.CellBackColor = QBColor(15)

                End If
            Next C


            lastcol = col


            '----------ROW

            'ROW : row labels

            'Government : row labels


            subrow = 0
            col = 1
            row = lastrow



            row = row + 1
            grdSAMDet(col, row) = "ROW" 'from
            subrow = subrow + 1

            '    row = row + 1
            '    grdsamdet(col, row) = "FDI Outflow" 'from firms
            '    subrow = subrow + 1
            '
            '    row = row + 1
            '    grdsamdet(col, row) = "Equity Outflow" 'from firms
            '    subrow = subrow + 1



            'leftmost
            col = 0
            row = lastrow
            For r = 1 To subrow
                row = row + 1
                If r = 1 Then
                    grdSAMDet(col, row) = "ROW"            'starts at 0
                Else
                    '.CellBackColor = QBColor(15)

                End If
            Next r


            lastrow = row

            'ROW: column labels


            subcol = 0
            row = 1
            col = lastcol

            '    col = col + 1
            '    grdsamdet(col, row) = "Exports" 'to sectors
            '    subcol = subcol + 1

            col = col + 1
            grdSAMDet(col, row) = "ROW" 'to sectors
            subcol = subcol + 1


            '    col = col + 1
            '    grdsamdet(col, row) = "Worker Remittances" 'to hh
            '    subcol = subcol + 1

            '    col = col + 1
            '    grdsamdet(col, row) = "FDI Inflow" 'to firms
            '    subcol = subcol + 1
            '
            '    col = col + 1
            '    grdsamdet(col, row) = "Equity Inflow" 'to firms
            '    subcol = subcol + 1

            '    col = col + 1
            '    grdsamdet(col, row) = "Net Foreign Savings" 'to capital
            '    subcol = subcol + 1


            '    col = col + 1
            '    grdsamdet(col, row) = "Aid Receipts" 'to govt
            '    subcol = subcol + 1
            '
            '    col = col + 1
            '    grdsamdet(col, row) = "World Bank Loans" 'to govt
            '    subcol = subcol + 1
            '
            '    col = col + 1
            '    grdsamdet(col, row) = "IMF Credits" 'to govt
            '    subcol = subcol + 1

            row = 0  'top most row
            col = lastcol
            For C = 1 To subcol 'iType
                col = col + 1
                If C = 1 Then
                    grdSAMDet(col, row) = "ROW"
                Else
                    '.CellBackColor = QBColor(15)

                End If
            Next C


            lastcol = col

            .TotalRows = lastrow + 1
            For i = 49 To lastcol + 1 Step -1
                .Columns.RemoveAt(i)
            Next i

            row = 0
            col = 0
            grdSAMDet(col, row) = "SAM"
            '.CellBackColor = QBColor(15)

            row = 0
            col = 1
            pos1 = InStr(cboFiles.SelectedValue, "-") + 2
            pos2 = InStr(cboFiles.SelectedValue, ",")

            'jrs 2006/07/07
            If pos2 > 0 Then
                grdSAMDet(col, row) = Mid(cboFiles.SelectedValue, pos1, pos2 - pos1)
            Else
                grdSAMDet(col, row) = Mid(cboFiles.SelectedValue, pos1)
            End If
            '.CellBackColor = QBColor(15)

            row = 1
            col = 0
            grdSAMDet(col, row) = cmb_Country.SelectedValue
            '.CellBackColor = QBColor(15)

            row = 1
            col = 1
            grdSAMDet(col, row) = Cmb_Year.SelectedValue
            '.CellBackColor = QBColor(15)

            grdSAMDetDesc.TotalRows = lastrow + 3
            For i = 0 To lastcol + 3
                Dim myCol As New TMSWebControls.Column
                grdSAMDetDesc.Columns.Add(myCol)
            Next
        End With

        'frm_SAMDet.Show

    End Sub

    Private Sub ExpandCellFullSAM()
        'mti May 2006 - new procedure
        'JRS 09/11/02
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti May 2006 - new procedure
        Dim bShow As Boolean
        Dim bTotTot As Boolean
        Dim CompVarVal As Single
        'JRS 06/26/03
        Dim lFactor As Single

        grdSAMDetPrn.TotalRows = 1
        Dim i As Integer
        For i = 0 To 2
            Dim myCol As New TMSWebControls.Column
            grdSAMDetPrn.Columns.Add(myCol)
        Next
        Erase ArrDesc
        ReDim ArrDesc(20, 20)


        'Display AX for Sectors, Sectors
        With dbgSAM

            'Session("SAMRow") =rowname;Session("SAMCol") =columnname
            'sector sector comes from database,i.e., .mdb
            bTotTot = (Session("SAMRow") = "Total" And Session("SAMCol") = "Total")
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'mti May 2006 - 1 line
                'DisplayAX cmb_Country.SelectedIndex + 1, val(Cmb_Year.SelectedValue)
                DisplayAX(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), grdSAMDetPrn, "Inter-Sectoral Flows")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(NSec)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(NSec)
                End If
                bShow = True
            End If

            'Cell Sectors, Household : r,c  : consumption
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'Work with Regions

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CSHHU",
                "Household", "Sectors", 1, New Object() {"Unskilled"}, aSectors, True, grdSAMDetPrn, "Private Consumption")
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CSHHS",
                "Household", "Sectors", 1, New Object() {"Skilled"}, aSectors, True, grdSAMDetPrn, "Private Consumption")


                'JRS 2/17/03
                TransposeTable(grdSAMDetPrn)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(2)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(NSec)
                End If

                bShow = True

            End If


            'Cell Sectors, Capital : "Gross Fixed Capital Formation by Sector of Origin"
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Capital" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'Work with Regions
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "INVS",
                    "Sectors", "Capital", 1, aSectors, New Object() {"Capital formation"}, True, grdSAMDetPrn, "Gross Fixed Capital Formation by Sector of Origin")

                'DisplayVAR cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CI", _
                '"Sectors", "Capital", 1, aSectors, _
                'New Object() {"Changes in Inventories"), True
                If Session("SAMRow") = "Total" Then
                    grdSAMDet(1, 0) = "Capital"
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(NSec)
                End If
                bShow = True

            End If

            'Cell Sectors, Government : Government Consumption
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'Work with Regions
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GS",
                    "Sectors", "Government", 1, aSectors, New Object() {"Govt"}, True, grdSAMDetPrn, "Government Consumption")
                If Session("SAMRow") = "Total" Then
                    grdSAMDet(1, 0) = "Government"
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(NSec)
                End If
                bShow = True

            End If

            'Cell Sectors, ROW :Exports
            If (Session("SAMRow") = "Sectors" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                'Work with Regions
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XS",
                    "Sectors", "ROW", 1, aSectors, New Object() {"ROW"}, True, grdSAMDetPrn, "Exports")

                If Session("SAMRow") = "Total" Then
                    grdSAMDet(1, 0) = "ROW"
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(NSec)
                End If
                bShow = True

            End If

            'Cell Sectors, Environment
            If Session("SAMRow") = "Sectors" And Session("SAMCol") = "Environment" Then
                'Work with Regions
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHINC",
                    "Sectors", "Environment", 0, aSectors,
                    New Object() {"CO2", "Water", "Forest"}, True, grdSAMDetPrn, "Environmental")
                bShow = True
            End If

            'Cell Household, Sectors 'wages
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Sectors" And Not bTotTot Then

                'JRS 10/08/02
                'JRS 2/17/03 Call GetValue(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CDALF", CompVarVal)
                'JRS 2/17/03 DisplayVAR cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), _
                '"VADD", "Household", "Sectors", (1 - CompVarVal) / 2, _
                '    aTypes, aSectors, True


                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue),
                    "VADDHHU", "Household", "Sectors", 1,
                        New Object() {"Unskilled"}, aSectors, True, grdSAMDetPrn, "Wages")
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue),
                    "VADDHHS", "Household", "Sectors", 1,
                        New Object() {"Skilled"}, aSectors, True, grdSAMDetPrn, "Wages")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(NSec)
                End If
                bShow = True


            End If

            'Cell Household, Firms : "Dividend / Interest"
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Firms" And Not bTotTot Then

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHDIVINT",
                    "Household", "Firms", 1, aTypes, New Object() {"Firms"}, True, grdSAMDetPrn, "Dividend / Interest")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True


                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True

            End If

            'Cell Household, Government : Welfare, Pension
            '        If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "Government" And Not bTotTot Then
            '                DisplayVAR cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GOVHHTRN", _
            '                    "Household", "Government", 0.25, aTypes, _
            '                    New Object() {"Soc Sec", "Pension"), True
            '            If Session("SAMRow") = "Total" Then
            '                UpdateDisplayColTotals
            '            End If
            '            bShow = True
            '        End If
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") _
                And Session("SAMCol") = "Government" And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GOVHHTRNWEL",
                    "Government", "Household", 1, New Object() {"Welfare"}, aTypes,
                    True, grdSAMDetPrn, "Welfare Payment from Government to Households")
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GOVHHTRNPEN",
                    "Government", "Household", 1, New Object() {"Pension"}, aTypes,
                     True, grdSAMDetPrn, "Pension Payment from Government to Households")

                'JRS 1/10/03
                TransposeTable(grdSAMDetPrn)
                If Session("SAMRow") = "Total" Then
                    grdSAMDet(1, 0) = "Government"
                    UpdateDisplayColTotals(1)
                End If
                bShow = True

            End If

            'Cell Household, ROW : remittance from abroad
            If (Session("SAMRow") = "Household" Or Session("SAMRow") = "Total") And Session("SAMCol") = "ROW" And Not bTotTot Then

                If bWorkRemit Then
                    lFactor = 1
                Else
                    lFactor = 0
                End If
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWORKREMIT",
                    "Household", "ROW", lFactor, New Object() {"Unskilled"}, New Object() {"ROW"}, True,
                        grdSAMDetPrn, "Worker remittances received in home country of workers")
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWORKREMIT",
                    "Household", "ROW", 0, New Object() {"Skilled"}, New Object() {"ROW"}, True,
                            grdSAMDetPrn, "Worker remittances received in home country of workers")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                bShow = True


            End If

            'Cell Household, Total
            If Session("SAMRow") = "Household" And Session("SAMCol") = "Total" Then

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHINC",
                    "Household", "Total", 1, aTypes, New Object() {"Income"}, True, grdSAMDetPrn, "")
                'JRS 06/25/03
                grdSAMDet(1, 0) = "Totals"
                bShow = True

            End If

            'Cell Capital, Household : "Household Savings"
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHSAV",
                        "Capital", "Household", 1, New Object() {"Capital"}, aTypes, True, grdSAMDetPrn, "Household Savings")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(2)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True


            End If

            'Cell Capital, Firms : "Firms Savings"
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then



                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "FIRMSAV",
                        "Capital", "Firms", 1, New Object() {"Capital"}, New Object() {"Firms"}, True, grdSAMDetPrn, "Firms Savings")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True



            End If

            'Cell Capital, Government : "Government Savings"

            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "GOVBAL",
                        "Capital", "Government", 1, New Object() {"Capital"},
                        New Object() {"Govt"}, True, grdSAMDetPrn, "Government Savings")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True


            End If

            'Cell Capital, ROW : "Foreign Savings"
            If (Session("SAMRow") = "Capital" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "FORSAV",
                        "For Savings", "ROW", 1, New Object() {"Capital"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Net Foreign Savings")

                If Session("SAMRow") = "Total" Then
                    grdSAMDet(1, 0) = "ROW"
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True


            End If

            'Cell Firms, Sectors : "Value Added by Firms"

            If (Session("SAMRow") = "Firms" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors" Or Session("SAMCol") = "Total") And Not bTotTot Then

                'JRS 10/25/02
                'JRS 06/26/03 Call GetValue(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "CDALF", CompVarVal)

                'JRS 06/26/03 DisplayVAR cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "VADD", _
                '"Firms", "Sectors", CompVarVal, New Object() {"Firms"}, aSectors, True

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "VADDF",
                        "Firms", "Sectors", 1, New Object() {"Value Added"}, aSectors, True, grdSAMDetPrn, "Value Added by Firms")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(NSec)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True

            End If

            'Cell Firms, ROW : "Foreign Equity and Portfolio Investment in Domestic Firms"
            If (Session("SAMRow") = "Firms" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then

                'mti: changed the row column here for full expansion
                'Array ("Firms") thus changed to New Object() {"ROW"}
                'FDI, equity changed from column headers to ROW by commenting  transpose  afterwords

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XFDIFIN",
                    "ROW", "Firms", 1, New Object() {"FDI Inflow"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Foreign Equity and Portfolio Investment in Domestic Firms")
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XPORTFIN",
                    "ROW", "Firms", 1, New Object() {"Equity Inflow"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Foreign Equity and Portfolio Investment in Domestic Firms")

                'mti: changed the row column here for full expansion and commented transpose
                'TransposeTable
                If Session("SAMRow") = "Total" Then
                    grdSAMDet(1, 0) = "ROW" 'mti check
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True

            End If

            'Cell ROW, Sectors : "Imports"
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Sectors" Or Session("SAMCol") = "Total") And Not bTotTot Then

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "MS", "ROW",
                        "Sectors", 1, New Object() {"ROW"}, aSectors, True, grdSAMDetPrn, "Imports")

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(NSec)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True


            End If

            'Cell ROW, Firms : "Foreign equity and portfolio investment by domestic Firms"; "Worker Remittance sent to home country of workers"
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then

                If Not bWorkRemit Then
                    lFactor = -1
                Else
                    lFactor = 0
                End If

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XFDIFOUT",
                    "Firms", "ROW", 1, New Object() {"FDI Outflow"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Enterprise income paid or invested abroad")
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XPORTFOUT",
                    "Firms", "ROW", 1, New Object() {"Equity Outflow"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Enterprise income paid or invested abroad")
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWBSUBF",
                    "Firms", "ROW", 1, New Object() {"WB Subscriptions"}, New Object() {"ROW"}, True, grdSAMDetPrn, "WB Subscription")
                'mti ask bbh: this is going to cell household, row also
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWORKREMIT",
                    "Firms", "ROW", lFactor, New Object() {"Worker Remittances"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Worker Remittance sent to home country of workers")


                TransposeTable(grdSAMDetPrn)
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True




            End If

            'Cell ROW, Government : "Foreign Aid Donation"; "Debt Service"
            If (Session("SAMRow") = "ROW" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Government" Or Session("SAMCol") = "Total") And Not bTotTot Then
                If bDonate Then
                    DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "AID",
                        "Government", "ROW", -1, New Object() {"Aid Donations"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Foreign Aid Donation")

                End If
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWBLNFOUT",
                    "Government", "ROW", 1, New Object() {"World Bank Repay"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Debt Service")
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XIMFCRFOUT",
                    "Government", "ROW", 1, New Object() {"IMF Repay"}, New Object() {"ROW"}, True, grdSAMDetPrn, "Debt Service")

                TransposeTable(grdSAMDetPrn)

                If Session("SAMRow") = "Total" Then
                    grdSAMDet(1, 0) = "Government"
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True



            End If

            'Cell Environment, Sectors
            If Session("SAMRow") = "Environment" And Session("SAMCol") = "Sectors" Then

                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHINC",
                    "Environment", "Sectors", 0, New Object() {"CO2", "Water", "Forest"},
                    aSectors, True, grdSAMDetPrn, "Environment")
                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(6)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True

            End If


            'Cell Government, Household : "Income Tax and Social Security (Pension and Welfare) payment from Household to Government"
            Dim sDesc As String
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Household" Or Session("SAMCol") = "Total") And Not bTotTot Then


                sDesc = "Income tax payment from household to government"
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHTAX",
                    "Government", "Household", 1, New Object() {"Income Tx"}, aTypes, True, grdSAMDetPrn, sDesc)
                sDesc$ = "Social Security (pension and welfare) tax payment from household to government"
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "HHGOVSS",
                    "Government", "Household", 1.0#, New Object() {"Soc Sec Tx"}, aTypes, True, grdSAMDetPrn, sDesc)

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(2)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True

            End If



            'Cell Government, Firms
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "Firms" Or Session("SAMCol") = "Total") And Not bTotTot Then

                sDesc$ = "Corporate/business taxes paid to government"
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "FIRMTAX",
                    "Government", "Firms", 1, New Object() {"Corp/Bus Tax"}, New Object() {"Firms"}, True, grdSAMDetPrn, sDesc$)
                sDesc$ = "Social security contribution of firms"
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "FIRMGOVSS",
                    "Government", "Firms", 1, New Object() {"Soc Sec Tx"}, New Object() {"Firms"}, True, grdSAMDetPrn, sDesc$)
                sDesc$ = "Indirect taxes paid by firms to government"
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "INDIRECTTAX",
                    "Government", "Firms", 1, New Object() {"Indirect Tax"}, New Object() {"Firms"}, True, grdSAMDetPrn, sDesc$)

                If Session("SAMRow") = "Total" Then
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True


            End If

            'Cell Government, ROW
            If (Session("SAMRow") = "Government" Or Session("SAMRow") = "Total") And (Session("SAMCol") = "ROW" Or Session("SAMCol") = "Total") And Not bTotTot Then

                'mti changed row col: commented transpose
                If Not bDonate Then
                    sDesc$ = "Foreign Aid Receipts"
                    DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "AID",
                    "ROW", "Government", 1, New Object() {"Aid Receipts"}, New Object() {"ROW"}, True, grdSAMDetPrn, sDesc$)
                End If
                sDesc$ = "Foreign Loan and Credit"
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XWBLNFIN",
                    "ROW", "Government", 1, New Object() {"World Bank Loans"}, New Object() {"ROW"}, True, grdSAMDetPrn, sDesc$)
                DisplayVAR(cmb_Country.SelectedIndex + 1, Val(Cmb_Year.SelectedValue), "XIMFCRFIN",
                    "ROW", "Government", 1, New Object() {"IMF Credits"}, New Object() {"ROW"}, True, grdSAMDetPrn, sDesc$)

                'mti changed row col: commented transpose
                'TransposeTable
                If Session("SAMRow") = "Total" Then
                    grdSAMDet(1, 0) = "ROW" 'mti check
                    UpdateDisplayColTotals(1)
                End If
                If Session("SAMCol") = "Total" Then
                    UpdateDisplayRowTotals(1)
                End If
                bShow = True


            End If

        End With

        If bShow Then
            '        If dbgSAMSession("SAMRow") = "Total" And dbgSAM.Columns(dbgSAM.col).DataField <> "Total" Then
            '            DisplayColTotals
            '        End If
            '        If dbgSAM.Columns(dbgSAM.col).DataField = "Total" And dbgSAMSession("SAMRow") <> "Household" And dbgSAMSession("SAMRow") <> "Total" Then
            '            DisplayRowTotals
            '        End If
            '        'JRS 04/29/03
            '        DisplayFixedDigits
            '        frm_SAMDet.Show
            AddMatrix()
        Else
            '        Unload frm_SAMDet
            '        'JRS 04/18/03
            '        MsgBox "No detail available for this cell.", vbOKOnly, "SAM"
        End If
        'Mouse Pointer back to normal
        'Screen.MousePointer = vbNormal
    End Sub

    Private Sub TotalMatrix(Optional ByVal grd As TMSWebControls.AdvWebGrid = Nothing)
        'mti May 2006 - new procedure
        Dim col As Integer
        Dim row As Integer
        Dim coltot As Double
        Dim lbl As String
        Dim lbl2 As String
        Dim i As Integer
        Dim j As Integer

        Dim grd1 As TMSWebControls.AdvWebGrid
        If grd Is Nothing Then
            grd1 = grdSAMDet
        Else
            grd1 = grd
        End If

        Dim irow As Integer, icol As Integer
        Dim cname As String, cno As Integer, rno As Integer
        With grd1 'frm_SAMDet.grdSAMDet\
            'column totals
            .TotalRows = .TotalRows + 1 'add a row for putting totals

            irow = .TotalRows - 1

            icol = 0
            .Item(icol, irow) = "Column"

            icol = 1
            .Item(icol, irow) = "Total"
            For col = 2 To .Columns.Count - 1
                coltot = 0
                icol = col
                For row = 2 To .TotalRows - 2 'exclude totals row
                    irow = row
                    coltot = coltot + Val(.Item(icol, irow))
                Next row
                irow = .TotalRows - 1
                .Item(icol, irow) = coltot
                irow = 1
                cname = .Item(icol, irow)
                irow = .TotalRows - 1

                cno = icol
                rno = irow

                With grdSAMDetDesc
                    .Item(cno, rno) = "Total Expenditure : " + cname
                End With

            Next col

            'row totals
            'add a row for putting totals
            Dim myColRH As New TMSWebControls.Column
            myColRH.WidthType = TMSWebControls.WType.Absolute
            myColRH.Width = 100
            .Columns.Add(myColRH)

            icol = .Columns.Count - 1

            irow = 0
            .Item(icol, irow) = "Row"

            irow = 1
            .Item(icol, irow) = "Total"
            Dim rowtot As Double, rname As String
            For row = 2 To .TotalRows - 2 'exclude totals row
                rowtot = 0
                irow = row
                For col = 2 To .Columns.Count - 2 'exclude totals col
                    icol = col
                    rowtot = rowtot + Val(.Item(icol, irow))
                Next col
                icol = .Columns.Count - 1
                .Item(icol, irow) = rowtot

                icol = 1
                rname = .Item(icol, irow)
                icol = .Columns.Count - 1
                cno = icol
                rno = irow
                With grdSAMDetDesc
                    .Item(cno, rno) = "Total Received : " + rname
                End With

            Next row


        End With
    End Sub

    Private Sub CollapseTotal(Optional ByVal grd As TMSWebControls.AdvWebGrid = Nothing)
        'mti May 2006 - new procedure
        Dim col As Integer
        Dim row As Integer
        Dim ctot As Double

        Dim grd1 As TMSWebControls.AdvWebGrid
        If grd Is Nothing Then
            grd1 = grdSAMDet
        Else
            grd1 = grd
        End If

        Dim irow As Integer, icol As Integer
        Dim sName As String, cno As Integer, rno As Integer
        With grd1 'frm_SAMDet.grdSAMDet\
            'column totals
            .TotalRows = .TotalRows + 1 'add a row for putting totals

            irow = .TotalRows - 1
            icol = 0
            .Item(icol, irow) = "Collapsed"
            icol = 1
            .Item(icol, irow) = "Total"

            'For col = 2 To .cols - 2 'exclude totals col


            icol = 2
            Do While icol < .Columns.Count - 1
                irow = 0

                sName = .Item(icol, irow)
                ctot = 0

                Do While .Item(icol, irow) = sName Or .Item(icol, irow) = ""
                    irow = .TotalRows - 2 'totals row
                    ctot = ctot + Val(.Item(icol, irow))
                    icol = icol + 1
                    irow = 0
                Loop
                irow = .TotalRows - 1 'collapsed totals row
                icol = icol - 1
                .Item(icol, irow) = ctot 'write collapsed total

                cno = icol
                rno = irow

                With grdSAMDetDesc
                    .Item(cno, rno) = "Total Expenditure : " + sName
                End With


                icol = icol + 1
            Loop

            'row totals
            'add a row for putting totals
            Dim myColRH As New TMSWebControls.Column
            myColRH.WidthType = TMSWebControls.WType.Absolute
            myColRH.Width = 100
            .Columns.Add(myColRH)

            icol = .Columns.Count - 1
            irow = 0
            .Item(icol, irow) = "Collapsed"
            irow = 1
            .Item(icol, irow) = "Total"

            'For col = 2 To .cols - 2 'exclude totals col


            irow = 2
            Do While irow < .TotalRows - 2
                icol = 0

                sName = .Item(icol, irow)
                ctot = 0

                Do While .Item(icol, irow) = sName Or .Item(icol, irow) = ""
                    icol = .Columns.Count - 2 'totals col
                    ctot = ctot + Val(.Item(icol, irow))
                    irow = irow + 1
                    icol = 0
                Loop
                icol = .Columns.Count - 1 'collapsed totals row
                irow = irow - 1
                .Item(icol, irow) = ctot 'write collapsed total

                cno = icol
                rno = irow

                With grdSAMDetDesc
                    .Item(cno, rno) = "Total Received : " + sName
                End With

                irow = irow + 1
            Loop



        End With
    End Sub

    Private Sub AddMatrix()
        'mti May 2006 - new procedure
        Dim col As Integer
        Dim row As Integer
        Dim lbl As String
        Dim lbl2 As String
        Dim i As Integer
        Dim j As Integer
        Dim C As Integer, cno As Integer, rno As Integer, cname As String, rname As String,
        c1 As Integer, r As Integer, r1 As Integer, valtext As String
        Dim irow As Integer, icol As Integer, irow2 As Integer, icol2 As Integer
        With grdSAMDetPrn

            'loop columns of cell expansion
            For C = 1 To .Columns.Count - 1
                cno = 0
                irow = 0
                icol = C
                cname = .Item(icol, irow)
                With grdSAMDet 'total expansion
                    irow2 = 1
                    For c1 = 2 To .Columns.Count - 1
                        icol2 = c1
                        If .Item(icol2, irow2) = cname Then
                            cno = c1
                            Exit For
                        End If
                    Next c1
                End With

                If cno > 1 Then 'column exists

                    'loop rows of cell expansion

                    For r = 1 To .TotalRows - 1
                        rno = 0
                        icol = 0
                        irow = r
                        rname = .Item(icol, irow)
                        With grdSAMDet  'total expansion
                            icol2 = 1
                            For r1 = 2 To .TotalRows - 1 'loop rows of total expansion
                                irow2 = r1
                                If .Item(icol2, irow2) = rname Then
                                    rno = r1
                                    Exit For
                                End If
                            Next r1
                        End With
                        If rno > 1 Then
                            icol = C
                            irow = r
                            valtext = .Item(icol, irow)
                            With grdSAMDet
                                icol2 = cno
                                irow2 = rno
                                .Item(icol2, irow2) = valtext
                                With grdSAMDetDesc
                                    .Item(cno, rno) = ArrDesc(r, C)
                                End With
                            End With
                        End If
                    Next r
                    'loop rows of cell expansion -end
                End If  'column exists
            Next C
        End With
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        If Session("SAMGroups") = False Then
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
    '''      jam 2021/09/01
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter">URL Parameters</param>
    ''' <returns>Parameters transformed in a valid URL format</returns>
    Public Function SAMPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        SAMPrepareForFriendlyURL = tmp
    End Function

End Class
